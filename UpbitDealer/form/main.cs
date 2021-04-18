using UpbitDealer.src;
using System;
using System.Collections.Generic;
using System.Data;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;

namespace UpbitDealer.form
{
    public partial class Main : Form
    {
        private string sAPI_Key;
        private string sAPI_Secret;

        private List<string> coinList = new List<string>();

        private bool isInit = false;
        private bool AllStop = false;

        private List<string> logList = new List<string>();
        private readonly object lock_logList = new object();

        private Thread thread_updater;
        public MainUpdater mainUpdater;
        public readonly object lock_mainUpdater = new object();
        public List<Account> account;
        public Dictionary<string, Ticker> ticker;
        private DataTable showAccount;

        private Thread thread_tradeHistory;
        public TradeHistory tradeHistory;
        public readonly object lock_tradeHistory = new object();

        private Thread thread_macro;
        public MacroSetting macro;
        public readonly object lock_macro = new object();


        public Main(string sAPI_Key, string sAPI_Secret)
        {
            InitializeComponent();
            this.sAPI_Key = sAPI_Key;
            this.sAPI_Secret = sAPI_Secret;
        }
        private void main_Load(object sender, EventArgs e)
        {
            {
                mainUpdater = new MainUpdater(sAPI_Key, sAPI_Secret);

                coinList = new List<string>(mainUpdater.setCoinList());
                if (coinList == null)
                {
                    MessageBox.Show("Init error due to API error, try again.");
                    Close();
                    return;
                }
                mainUpdater.update();
                account = mainUpdater.account;
                ticker = mainUpdater.ticker;

                for (int i = 0; i < coinList.Count; i++)
                    list_coinName.Items.Add(coinList[i]);

                showAccount = new DataTable();
                showAccount.Columns.Add("Name", typeof(string));
                showAccount.Columns.Add("Units", typeof(double));
                showAccount.Columns.Add("Price", typeof(double));
                showAccount.Columns.Add("Total", typeof(double));

                for (int i = 0; i < account.Count; i++)
                {
                    DataRow dataRow = showAccount.NewRow();
                    dataRow["Name"] = account[i].coinName;
                    dataRow["Units"] = account[i].locked + account[i].valid;
                    dataRow["Price"] = account[i].coinName == "KRW" ? 1 : ticker[account[i].coinName].close;
                    dataRow["Total"] = (double)dataRow["Units"] * (double)dataRow["Price"];
                    showAccount.Rows.Add(dataRow);
                }

                dataGridView_holdList.DataSource = showAccount;
                dataGridView_holdList.Columns["Units"].DefaultCellStyle.Format = "#,0.####";
                dataGridView_holdList.Columns["Price"].DefaultCellStyle.Format = "#,0.##";
                dataGridView_holdList.Columns["Total"].DefaultCellStyle.Format = "#,0.##";
            }

            {
                tradeHistory = new TradeHistory(sAPI_Key, sAPI_Secret);
                if (tradeHistory.loadFile() < 0)
                {
                    Close();
                    return;
                }
            }

            {
                macro = new MacroSetting(sAPI_Key, sAPI_Secret, coinList);
                if (macro.loadFile() < 0)
                {
                    Close();
                    return;
                }
            }

            {
                thread_updater = new Thread(() => executeMainUpdater());
                thread_tradeHistory = new Thread(() => executeTradeHistoryUpdate());
                thread_macro = new Thread(() => executeMacro());

                thread_updater.Start();
                thread_tradeHistory.Start();
                thread_macro.Start();
            }

            isInit = true;
        }
        private void main_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!isInit)
                return;

            DialogResult dialogResult =
                MessageBox.Show(
                    "Really Exit?", "Exit",
                    MessageBoxButtons.YesNo,
                    MessageBoxIcon.Question,
                    MessageBoxDefaultButton.Button1);
            if (dialogResult != DialogResult.Yes)
                e.Cancel = true;
        }
        private void main_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isInit)
            {
                MessageBox.Show("Init fail.");
                return;
            }

            savingMsg closing = new savingMsg();
            closing.Show(this);

            AllStop = true;
            thread_updater.Join();
            thread_tradeHistory.Join();
            thread_macro.Join();

            macro.saveFile();

            closing.Close();
        }
        private void main_Resize(object sender, EventArgs e)
        {
            if (this.WindowState == FormWindowState.Minimized)
            {
                Visible = false;
                ShowIcon = false;
            }
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            group_account.Focus();
        }


        private void executeMainUpdater()
        {
            while (!AllStop)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                lock (lock_mainUpdater)
                    mainUpdater.update();

                stopwatch.Stop();
                long sleepTime = 1000 - stopwatch.ElapsedMilliseconds;
                while (!AllStop && sleepTime > 0)
                {
                    Thread.Sleep(100);
                    sleepTime -= 100;
                }
            }
        }
        private void executeTradeHistoryUpdate()
        {
            while (!AllStop)
            {
                for (int i = 0; !AllStop && i < tradeHistory.pendingData.Rows.Count; i++)
                {
                    lock (lock_tradeHistory)
                    {
                        bool needSave = false;
                        if (tradeHistory.updatePendingData(i) > 0) needSave = true;

                        for (int j = 0; j < tradeHistory.executionStr.Count; j++)
                            logIn(tradeHistory.executionStr[j]);

                        tradeHistory.executionStr.Clear();
                        if (needSave) tradeHistory.saveFile();
                    }
                    Thread.Sleep(100);
                }
                for (int j = 0; !AllStop && j < 10; j++)
                    Thread.Sleep(100);
            }
        }
        private void executeMacro()
        {
            logIn(new Output(0, "Macro Exection", "Load candle data. As you possible, DO NOT TOUCH."));
            for (int i = 0; !AllStop && i < macro.getListCount(); i++)
            {
                Stopwatch stopwatch = new Stopwatch();
                stopwatch.Start();

                lock (lock_macro)
                    if (macro.initCandleData(i) < 0) i--;

                stopwatch.Stop();
                long sleepTime = 1000 - stopwatch.ElapsedMilliseconds;
                while (!AllStop && sleepTime > 0)
                {
                    Thread.Sleep(100);
                    sleepTime -= 100;
                }
            }
            logIn(new Output(0, "Macro Exection", "Finish to load, Start macro"));

            while (!AllStop)
            {
                lock (lock_macro) lock (lock_mainUpdater)
                    macro.updateCoinList(mainUpdater.coinList);
                macro.updateBollingerAvgMin();

                for (int i = 0; !AllStop && i < macro.getListCount(); i++)
                {
                    lock (lock_macro)
                    {
                        int ret;
                        lock (lock_mainUpdater)
                        {
                            macro.updateLastKrw(account);
                            ret = macro.updateQuote(i, ticker);
                        }
                        if (ret < 0)
                        {
                            logIn(new Output(0, "Macro Execution", "Fail to update quote (" + i + ")"));
                            continue;
                        }
                        macro.updateCandle(i);

                        bool needSave = false;
                        if (macro.executeMacroBuy(i) > 0) needSave = true;
                        if (macro.executeMacroSell(i) > 0) needSave = true;

                        for (int j = 0; j < macro.order.Rows.Count; j++)
                            if (macro.executeCheckResult(j) > 0)
                                needSave = true;

                        if (needSave) macro.saveFile();

                        for (int j = 0; j < macro.executionStr.Count; j++)
                            logIn(macro.executionStr[j]);
                        macro.executionStr.Clear();
                    }
                    Thread.Sleep(1000 / macro.getListCount());
                }
            }
        }
        public void logIn(Output log)
        {
            if (log.level >= 0)
                lock (lock_logList)
                    logList.Add(DateTime.Now.ToString(" [yyyy-MM-dd_HH:mm:ss] ") + log.title + " : " + log.str);

            if (Math.Abs(log.level) == 1)
            {
                notifyIcon.BalloonTipTitle = log.title;
                notifyIcon.BalloonTipText = log.str;
                notifyIcon.ShowBalloonTip(1000);
            }
            else if (Math.Abs(log.level) == 2)
            {
                notifyIcon.BalloonTipTitle = log.title;
                notifyIcon.BalloonTipText = log.str;
                notifyIcon.ShowBalloonTip(100000);
            }
        }


        private void timer_panel_Tick(object sender, EventArgs e)
        {
            text_curTime.Text = DateTime.Now.ToString("yyyy-MM-dd || HH:mm:ss");
            int index = dataGridView_holdList.FirstDisplayedCell.RowIndex;
            double totalKrw = 0;
            lock (lock_mainUpdater)
            {
                showAccount.Clear();
                for (int i = 0; i < account.Count; i++)
                {
                    DataRow dataRow = showAccount.NewRow();
                    dataRow["Name"] = account[i].coinName;
                    dataRow["Units"] = account[i].locked + account[i].valid;
                    dataRow["Price"] = account[i].coinName == "KRW" ? 1 : ticker[account[i].coinName].close;
                    dataRow["Total"] = (double)dataRow["Units"] * (double)dataRow["Price"];
                    showAccount.Rows.Add(dataRow);
                    totalKrw += (double)dataRow["Total"];
                }
            }
            text_totalKrw.Text = "Total : " + totalKrw.ToString("0,0");
            dataGridView_holdList.FirstDisplayedScrollingRowIndex = index;
        }
        private void timer_logOut_Tick(object sender, EventArgs e)
        {
            lock (lock_logList)
                while (logList.Count > 0)
                {
                    text_log.AppendText(logList[0] + Environment.NewLine);
                    logList.Remove(logList[0]);
                }
        }


        private void notifyIcon_MouseClick(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                notifyIcon.ContextMenuStrip = contextMenuStrip;
                System.Reflection.MethodInfo methodInfo =
                       typeof(NotifyIcon).GetMethod("ShowContextMenu",
                        System.Reflection.BindingFlags.Instance |
                           System.Reflection.BindingFlags.NonPublic);
                methodInfo.Invoke(notifyIcon, null);
            }
        }
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Visible = true;
            ShowIcon = true;
            Show();
            WindowState = FormWindowState.Normal;
        }
        private void toolStripTextBox_show_Click(object sender, EventArgs e)
        {
            Visible = true;
            ShowIcon = true;
            Show();
            WindowState = FormWindowState.Normal;
        }
        private void toolStripTextBox_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        private void toolStripTextBox_mouse(object sender, MouseEventArgs e)
        {
            Cursor.Current = Cursors.Arrow;
        }


        private void dataGridView_holdList_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
                showAccount.DefaultView.Sort = "";
        }
        private void text_search_TextChanged(object sender, EventArgs e)
        {
            list_coinName.Items.Clear();
            for (int i = 0; i < coinList.Count; i++)
                if (coinList[i].StartsWith(text_search.Text.ToUpper()))
                    list_coinName.Items.Add(coinList[i]);
        }
        private void btn_search_reset_Click(object sender, EventArgs e)
        {
            text_search.Text = "";
            text_search.Focus();
        }


        private void list_coinName_MouseUp(object sender, MouseEventArgs e)
        {
            if (list_coinName.SelectedIndex < 0 || list_coinName.SelectedIndex > list_coinName.Items.Count - 1)
                list_coinName.SelectedIndex = -1;
        }
        private void btn_showChart_Click(object sender, EventArgs e)
        {
            if (list_coinName.SelectedIndex > -1 && list_coinName.SelectedIndex < list_coinName.Items.Count)
            {
                string name = list_coinName.SelectedItem.ToString();
                foreach (Form fm in Application.OpenForms)
                    if (fm.Name == name + " Chart")
                    {
                        MessageBox.Show("Fail to open '" + name + " Chart', already opened.");
                        return;
                    }

                graph chartForm = new graph(name, sAPI_Key, sAPI_Secret);
                chartForm.Show(this);
                return;
            }

            MessageBox.Show("Fail to open chart, choose one from the list.");
        }
        private void btn_trader_Click(object sender, EventArgs e)
        {
            foreach (Form fm in Application.OpenForms)
                if (fm.Name == "Trader")
                {
                    MessageBox.Show("Fail to open 'Trader', already opened.");
                    return;
                }

            Trader traderForm;
            lock (lock_mainUpdater)
                traderForm = new Trader(sAPI_Key, sAPI_Secret, coinList);
            traderForm.Show(this);
        }
        private void btn_history_Click(object sender, EventArgs e)
        {
            foreach (Form fm in Application.OpenForms)
                if (fm.Name == "History")
                {
                    MessageBox.Show("Fail to open 'History', already opened.");
                    return;
                }

            History historyForm;
            lock (lock_mainUpdater)
                historyForm = new History();
            historyForm.Show(this);
        }
        private void btn_indicator_Click(object sender, EventArgs e)
        {
            foreach (Form fm in Application.OpenForms)
                if (fm.Name == "Indicator")
                {
                    MessageBox.Show("Fail to show 'Macro' window, already opened.");
                    return;
                }

            Indicator indicatorForm = new Indicator();
            indicatorForm.Show(this);
        }
        private void btn_macro_Click(object sender, EventArgs e)
        {
            foreach (Form fm in Application.OpenForms)
                if (fm.Name == "Macro")
                {
                    MessageBox.Show("Fail to show 'Macro' window, already opened.");
                    return;
                }

            Macro macroForm = new Macro();
            macroForm.Show(this);
        }
        private void btn_save_Click(object sender, EventArgs e)
        {
            timer_log.Stop();
            try
            {
                SaveFileDialog saveFileDialog = new SaveFileDialog();
                saveFileDialog.InitialDirectory =
                    Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments);
                saveFileDialog.Title = "Save BTC log";
                saveFileDialog.DefaultExt = "log";
                saveFileDialog.Filter = "Log files(*.log)|*.log";

                if (saveFileDialog.ShowDialog() == DialogResult.OK)
                {
                    string filePath = saveFileDialog.FileName.ToString();
                    System.IO.File.WriteAllText(filePath, text_log.Text);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            timer_log.Start();
        }
    }
}
