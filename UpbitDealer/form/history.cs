using UpbitDealer.src;
using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;

namespace UpbitDealer.form
{
    public partial class History : Form
    {
        private bool AllStop = false;
        private bool isInit = false;
        private Main ownerForm;

        private int type = 0;
        private Thread loadData;
        private readonly object lock_load = new object();

        private DataTable pendingData = new DataTable();
        TradeData selected = new TradeData();

        private DataTable historyData = new DataTable();
        private bool isNeedUpdateHistory = false;
        private bool isNeedBindHistory = false;
        private int page = 1;

        private DataTable macroPrint = new DataTable();
        private DataTable macroPrintSwap = new DataTable();
        private MacroSettingData macroSetting;
        private DataSet macroState;


        public History(Main ownerForm)
        {
            InitializeComponent();
            this.ownerForm = ownerForm;
        }
        private void History_Load(object sender, EventArgs e)
        {
            DateTime tempDateTime = DateTime.Now.AddHours(-DateTime.Now.Hour).AddMinutes(-DateTime.Now.Minute).AddSeconds(-DateTime.Now.Second);

            pendingData.Columns.Add("uuid", typeof(string));
            pendingData.Columns.Add("coinName", typeof(string));
            pendingData.Columns.Add("date", typeof(DateTime));
            pendingData.Columns.Add("isBid", typeof(bool));
            pendingData.Columns.Add("unit", typeof(double));
            pendingData.Columns.Add("price", typeof(double));
            pendingData.Columns.Add("fee", typeof(double));

            historyData.Columns.Add("coinName", typeof(string));
            historyData.Columns.Add("date", typeof(DateTime));
            historyData.Columns.Add("isBid", typeof(bool));
            historyData.Columns.Add("unit", typeof(double));
            historyData.Columns.Add("price", typeof(int));
            historyData.Columns.Add("fee", typeof(double));

            macroPrint.Columns.Add("coinName", typeof(string));
            macroPrint.Columns.Add("date", typeof(DateTime));
            macroPrint.Columns.Add("unit", typeof(double));
            macroPrint.Columns.Add("price", typeof(double));
            macroPrint.Columns.Add("target", typeof(double));

            macroPrintSwap.Columns.Add("uuid", typeof(string));
            macroPrintSwap.Columns.Add("coinName", typeof(string));
            macroPrintSwap.Columns.Add("date", typeof(DateTime));
            macroPrintSwap.Columns.Add("unit", typeof(double));
            macroPrintSwap.Columns.Add("price", typeof(double));
            macroPrintSwap.Columns.Add("target", typeof(double));

            lock (ownerForm.lock_tradeHistory)
                pendingData = ownerForm.tradeHistory.pendingData.Copy();

            loadData = new Thread(() => executeLoadData());
            loadData.Start();

            isInit = true;
        }
        private void History_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (isInit)
            {
                AllStop = true;
                loadData.Join();
            }
        }
        private void History_Resize(object sender, EventArgs e)
        {
            dataGridView1.Size = new Size(Size.Width - 40, Height - 99);
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            text_historyCoinName.Focus();
        }


        private void executeLoadData()
        {
            while (!AllStop)
            {
                lock (lock_load)
                {
                    switch (type)
                    {
                        case 0:
                            lock (ownerForm.lock_tradeHistory)
                                pendingData = ownerForm.tradeHistory.pendingData.Copy();
                            break;
                        case 1:
                            if (isNeedUpdateHistory)
                            {
                                isNeedUpdateHistory = false;
                                int ret = ownerForm.tradeHistory.updateHistoryData(text_historyCoinName.Text.ToUpper(), page);
                                if (ret < 0)
                                {
                                    MessageBox.Show("Invalid coin name.");
                                    return;
                                }
                                lock (ownerForm.lock_tradeHistory)
                                    historyData = ownerForm.tradeHistory.historyData.Copy();
                                isNeedBindHistory = true;
                            }
                            break;
                        case 2:
                            lock (ownerForm.lock_macro)
                            {
                                macroSetting = ownerForm.macro.setting;
                                macroState = ownerForm.macro.state.Copy();
                            }
                            macroPrintSwap.Rows.Clear();
                            for (int i = 0; i < macroState.Tables.Count; i++)
                            {
                                string tempCoinName = macroState.Tables[i].TableName;
                                for (int j = 0; j < macroState.Tables[i].Rows.Count; j++)
                                {
                                    DataRow dataRow = macroPrintSwap.NewRow();
                                    dataRow["uuid"] = macroState.Tables[tempCoinName].Rows[j]["uuid"];
                                    dataRow["coinName"] = tempCoinName;
                                    dataRow["date"] = macroState.Tables[tempCoinName].Rows[j]["date"];
                                    dataRow["unit"] = macroState.Tables[tempCoinName].Rows[j]["unit"];
                                    dataRow["price"] = macroState.Tables[tempCoinName].Rows[j]["price"];
                                    dataRow["target"] = (double)macroState.Tables[tempCoinName].Rows[j]["price"]
                                        * (100d + macroSetting.yield) / 100d;
                                    macroPrintSwap.Rows.Add(dataRow);
                                }
                            }
                            break;
                    }
                }

                for (int i = 0; i < 10; i++)
                {
                    if (AllStop) break;
                    Thread.Sleep(100);
                }
            }
        }
        private void timer_binding_Tick(object sender, EventArgs e)
        {
            lock (lock_load)
            {
                int index = 0;
                string sort = "";
                if (dataGridView1.Rows.Count > 0)
                {
                    index = dataGridView1.FirstDisplayedCell.RowIndex;
                    sort = ((DataTable)dataGridView1.DataSource).DefaultView.Sort;
                }
                switch (type)
                {
                    case 0:
                        {
                            dataGridView1.DataSource = pendingData;
                            dataGridView1.Columns["uuid"].Visible = false;
                            dataGridView1.Columns["date"].HeaderText = "Date";
                            dataGridView1.Columns["date"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                            dataGridView1.Columns["date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView1.Columns["coinName"].HeaderText = "Coin Name";
                            dataGridView1.Columns["coinName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView1.Columns["isBid"].HeaderText = "Is Bid";
                            dataGridView1.Columns["isBid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView1.Columns["unit"].HeaderText = "Units";
                            dataGridView1.Columns["unit"].DefaultCellStyle.Format = "#,0.####";
                            dataGridView1.Columns["unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1.Columns["price"].HeaderText = "Price";
                            dataGridView1.Columns["price"].DefaultCellStyle.Format = "#,0.##";
                            dataGridView1.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1.Columns["fee"].Visible = false;
                        }
                        break;
                    case 1:
                        if (isNeedBindHistory)
                        {
                            isNeedBindHistory = false;

                            dataGridView1.DataSource = historyData;
                            dataGridView1.Columns["date"].HeaderText = "Date";
                            dataGridView1.Columns["date"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                            dataGridView1.Columns["date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView1.Columns["coinName"].HeaderText = "Coin Name";
                            dataGridView1.Columns["coinName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView1.Columns["isBid"].HeaderText = "Is Bid";
                            dataGridView1.Columns["isBid"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView1.Columns["unit"].HeaderText = "Units";
                            dataGridView1.Columns["unit"].DefaultCellStyle.Format = "#,0.####";
                            dataGridView1.Columns["unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1.Columns["price"].HeaderText = "Price";
                            dataGridView1.Columns["price"].DefaultCellStyle.Format = "#,0.##";
                            dataGridView1.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1.Columns["fee"].HeaderText = "Fee";
                            dataGridView1.Columns["fee"].DefaultCellStyle.Format = "#,0.####";
                            dataGridView1.Columns["fee"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1.Sort(dataGridView1.Columns[0], ListSortDirection.Descending);

                            text_page.Text = "Page";
                            text_page.ForeColor = Color.Gray;
                            text_historyCoinName.Text = "Name";
                            text_historyCoinName.ForeColor = Color.Gray;
                        }
                        break;
                    case 2:
                        {
                            macroPrint = macroPrintSwap.Copy();
                            dataGridView1.DataSource = macroPrint;
                            dataGridView1.Columns["uuid"].Visible = false;
                            dataGridView1.Columns["coinName"].HeaderText = "Coin Name";
                            dataGridView1.Columns["coinName"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView1.Columns["date"].DefaultCellStyle.Format = "yyyy-MM-dd HH:mm:ss";
                            dataGridView1.Columns["date"].HeaderText = "Date";
                            dataGridView1.Columns["date"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
                            dataGridView1.Columns["unit"].HeaderText = "Units";
                            dataGridView1.Columns["unit"].DefaultCellStyle.Format = "#,0.####";
                            dataGridView1.Columns["unit"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1.Columns["price"].HeaderText = "Price";
                            dataGridView1.Columns["price"].DefaultCellStyle.Format = "#,0.##";
                            dataGridView1.Columns["price"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                            dataGridView1.Columns["target"].HeaderText = "Target Price";
                            dataGridView1.Columns["target"].DefaultCellStyle.Format = "#,0.##";
                            dataGridView1.Columns["target"].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleRight;
                        }
                        break;
                }
                if (dataGridView1.Rows.Count > 0)
                {
                    ((DataTable)dataGridView1.DataSource).DefaultView.Sort = sort;
                    dataGridView1.FirstDisplayedScrollingRowIndex = index;
                    dataGridView1.ClearSelection();
                }
            }
        }


        private void btn_order_Click(object sender, EventArgs e)
        {
            type = 0;
            dataGridView1.DataSource = null;
            btn_order.BackColor = Color.Red;
            btn_history.BackColor = Color.DarkGray;
            btn_macro.BackColor = Color.DarkGray;
            selected.uuid = "";
            text_selectInfo.Text = "";
            text_selectInfo.Visible = true;
            btn_cancel.Visible = true;
            btn_history_get.Visible = false;
            text_historyCoinName.Visible = false;
            text_page.Visible = false;
        }
        private void btn_history_Click(object sender, EventArgs e)
        {
            type = 1;
            dataGridView1.DataSource = null;
            btn_order.BackColor = Color.DarkGray;
            btn_history.BackColor = Color.Red;
            btn_macro.BackColor = Color.DarkGray;
            text_selectInfo.Visible = false;
            btn_cancel.Visible = false;
            btn_history_get.Visible = true;
            text_historyCoinName.ForeColor = Color.Gray;
            text_historyCoinName.Text = "Name";
            text_historyCoinName.Visible = true;
            text_page.ForeColor = Color.Gray;
            text_page.Text = "Page";
            text_page.Visible = true;
        }
        private void btn_macro_Click(object sender, EventArgs e)
        {
            type = 2;
            dataGridView1.DataSource = null;
            btn_order.BackColor = Color.DarkGray;
            btn_history.BackColor = Color.DarkGray;
            btn_macro.BackColor = Color.Red;
            selected.uuid = "";
            text_selectInfo.Text = "";
            text_selectInfo.Visible = true;
            btn_cancel.Visible = true;
            btn_history_get.Visible = false;
            text_historyCoinName.Visible = false;
            text_page.Visible = false;
        }


        private void dataGridView1_CellMouseClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (dataGridView1.GetCellCount(DataGridViewElementStates.Selected) < 1) return;
            lock (lock_load)
            {
                if (type == 0)
                {
                    selected.uuid = (string)dataGridView1.SelectedCells[0].Value;
                    selected.coinName = (string)dataGridView1.SelectedCells[1].Value;
                    selected.date = (DateTime)dataGridView1.SelectedCells[2].Value;
                    selected.isBid = (bool)dataGridView1.SelectedCells[3].Value;
                    selected.unit = (double)dataGridView1.SelectedCells[4].Value;
                    selected.price = (double)dataGridView1.SelectedCells[5].Value;

                    text_selectInfo.Text
                        = selected.coinName + " ll "
                        + selected.date.ToString("yy-MM-dd HH:mm:ss") + " ll "
                        + (selected.isBid ? "Bid" : "Ask") + " ll "
                        + selected.unit.ToString(",0.####") + " ll "
                        + selected.price.ToString(",0.##");
                }
                else if (type == 2)
                {
                    selected.uuid = (string)dataGridView1.SelectedCells[0].Value;
                    selected.coinName = (string)dataGridView1.SelectedCells[1].Value;
                    selected.date = (DateTime)dataGridView1.SelectedCells[2].Value;
                    selected.unit = (double)dataGridView1.SelectedCells[3].Value;
                    selected.price = (double)dataGridView1.SelectedCells[4].Value;

                    text_selectInfo.Text
                        = selected.coinName + " ll "
                        + selected.date.ToString("yy-MM-dd HH:mm:ss") + " ll "
                        + selected.unit.ToString(",0.####") + " ll "
                        + selected.price.ToString(",0.##");
                }
            }
        }
        private void dataGridView1_MouseUp(object sender, MouseEventArgs e)
        {
            dataGridView1.ClearSelection();
            if (e.Button == MouseButtons.Right)
                ((DataTable)dataGridView1.DataSource).DefaultView.Sort = "";
        }
        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            if (type == 1)
                dataGridView1.ClearSelection();
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            if (selected.uuid == "") return;
            if (type == 0)
            {
                DialogResult test = MessageBox.Show(
                      "Name : " + selected.coinName + Environment.NewLine
                      + "Type : " + (selected.isBid ? "Bid" : "Ask") + Environment.NewLine
                      + "Unit : " + selected.unit + Environment.NewLine
                      + "Price : " + selected.price + Environment.NewLine
                      , "Confirm", MessageBoxButtons.OKCancel);
                if (test == DialogResult.OK)
                {
                    int ret = ownerForm.tradeHistory.cancelPending(selected.uuid);
                    if (ret < 0)
                    {
                        MessageBox.Show("API error, try again");
                        return;
                    }
                    MessageBox.Show("Success!");
                    text_selectInfo.Text = "";
                    selected.uuid = "";
                }
            }
            else if (type == 2)
            {
                DialogResult test = MessageBox.Show(
                      "Name : " + selected.coinName + Environment.NewLine
                      + "Unit : " + selected.unit + Environment.NewLine
                      + "Price : " + selected.price + Environment.NewLine
                      , "Confirm", MessageBoxButtons.OKCancel);
                if (test == DialogResult.OK)
                {
                    int ret;
                    lock (ownerForm.lock_macro)
                        ret = ownerForm.macro.deleteMacroState(selected.coinName, selected.uuid);
                    if (ret < 0)
                    {
                        MessageBox.Show("Fail");
                        return;
                    }
                    MessageBox.Show("Success!");
                    text_selectInfo.Text = "";
                    selected.uuid = "";
                }
            }
        }


        private void text_clean_MouseUp(object sender, MouseEventArgs e)
        {
            ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.Black;
        }
        private void btn_history_get_Click(object sender, EventArgs e)
        {
            if (text_page.Text == "" || text_page.Text == "Page")
                text_page.Text = "1";
            if (!int.TryParse(text_page.Text, out page))
                MessageBox.Show("Page must be positive intger type value.");
            else if (page < 1)
                MessageBox.Show("Page must be positive intger type value.");
            else
            {
                isNeedUpdateHistory = true;
                text_historyCoinName.Text = text_historyCoinName.Text.ToUpper();
            }
        }
    }
}
