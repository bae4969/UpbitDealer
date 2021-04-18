using UpbitDealer.src;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using System.Windows.Forms;


namespace UpbitDealer.form
{
    public partial class Trader : Form
    {
        private React react;
        private List<string> coinList;

        private bool AllStop = false;

        private Thread thread_updater;
        private readonly object lock_updater = new object();
        private readonly object lock_select = new object();
        private Ticker ticker = new Ticker();
        private Account[] account = new Account[2] { new Account(), new Account() };
        private JArray transaction = new JArray();
        private double THMax = double.MinValue;
        private List<double>[] ob = null;

        private bool selected = true;
        private bool needTradeInit = false;
        private bool needTradeUpdate = false;
        private bool canTradeSet = false;
        private bool isBuy = true;
        private bool isPlace = true;

        private string selectedName = "";
        private double krw = 0d;
        private double currency = 0d;
        private double price = 0d;
        private double units = 0d;
        private double total = 0d;


        public Trader(string access_key, string secret_key, List<string> coinList)
        {
            InitializeComponent();
            this.react = new React(access_key, secret_key);
            this.coinList = new List<string>(coinList);
        }
        private void Trader_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < coinList.Count; i++)
                list_coinName.Items.Add(coinList[i]);

            thread_updater = new Thread(() => executeDataUpdate());
            thread_updater.Start();
        }
        private void Trader_FormClosed(object sender, FormClosedEventArgs e)
        {
            AllStop = true;
            thread_updater.Join();
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            groupBox3.Focus();
        }


        private void executeDataUpdate()
        {
            while (!AllStop)
            {
                lock (lock_select)
                    if (selectedName != "" && selected)
                    {
                        string coinName = selectedName;
                        Ticker tempTicker;
                        List<Account> tempAccount;
                        lock (((Main)Owner).lock_mainUpdater)
                        {
                            tempTicker = ((Main)Owner).ticker[coinName];
                            tempAccount = new List<Account>(((Main)Owner).account);
                        }
                        JArray tempTH = react.getTransactionData(coinName);
                        List<double>[] tempOB = react.getOrderBookData(coinName);

                        lock (lock_updater)
                        {
                            bool isHold = false;
                            ticker = tempTicker;
                            for (int i = 0; i < tempAccount.Count; i++)
                            {
                                if (tempAccount[i].coinName == "KRW")
                                {
                                    account[0].coinName = tempAccount[i].coinName;
                                    account[0].locked = tempAccount[i].locked;
                                    account[0].valid = tempAccount[i].valid;
                                }
                                else if (tempAccount[i].coinName == coinName)
                                {
                                    isHold = true;
                                    account[1].coinName = tempAccount[i].coinName;
                                    account[1].locked = tempAccount[i].locked;
                                    account[1].valid = tempAccount[i].valid;
                                    break;
                                }
                            }
                            if (!isHold)
                            {
                                account[1].coinName = coinName;
                                account[1].locked = 0;
                                account[1].valid = 0;
                            }

                            transaction = tempTH;
                            ob = tempOB;
                            if (ob != null)
                            {
                                THMax = double.MinValue;
                                for (int i = 0; i < 15; i++)
                                {
                                    THMax = Math.Max(THMax, tempOB[2][i]);
                                    THMax = Math.Max(THMax, tempOB[3][i]);
                                }
                            }
                            if (needTradeInit)
                            {
                                needTradeInit = false;
                                needTradeUpdate = true;
                            }
                        }
                    }

                for (int i = 0; !AllStop && !needTradeInit && i < 10; i++)
                    Thread.Sleep(100);
            }
        }
        private void timer_updater_Tick(object sender, EventArgs e)
        {
            if (selected)
            {
                lock (lock_updater)
                {
                    refreshTicker();
                    refreshTransactionHistory();
                    refreshOrderBook();
                    refreshTrade();
                }
            }
            else
            {
                text_price.ForeColor = Color.White;
                text_price.Text = "Delisting";
                text_change.Text = "";
                text_change_rate.Text = "";
                text_prev_close.Text = "";
                text_max.Text = "";
                text_min.Text = "";
                text_open.Text = "";
                text_close.Text = "";
                text_candle1.BackColor = Color.DarkGray;
                text_candle2.BackColor = Color.DarkGray;
                text_candle3.BackColor = Color.DarkGray;
            }
        }
        private void refreshTicker()
        {
            if (selectedName != "")
            {
                text_name.Text = selectedName;
                if (ticker.changeRate >= 0)
                {
                    text_price.ForeColor = Color.Red;
                    text_change.ForeColor = Color.Red;
                    text_change_rate.ForeColor = Color.Red;
                }
                else
                {
                    text_price.ForeColor = Color.DodgerBlue;
                    text_change.ForeColor = Color.DodgerBlue;
                    text_change_rate.ForeColor = Color.DodgerBlue;
                }
                text_price.Text = ticker.close.ToString(",0.##");
                text_change.Text = ticker.change.ToString(",0.####");
                text_change_rate.Text = ticker.changeRate.ToString("0.##") + "%";
                text_prev_close.Text = ticker.prePrice.ToString(",0.####");
                if (ticker.open <= ticker.close)
                {
                    text_candle1.BackColor = Color.Red;
                    text_candle2.BackColor = Color.Red;
                    text_candle3.BackColor = Color.Red;
                    text_open.Text = ticker.open.ToString(",0.####");
                    text_close.Text = ticker.close.ToString(",0.####");
                }
                else
                {
                    text_candle1.BackColor = Color.DodgerBlue;
                    text_candle2.BackColor = Color.DodgerBlue;
                    text_candle3.BackColor = Color.DodgerBlue;
                    text_open.Text = ticker.close.ToString(",0.####");
                    text_close.Text = ticker.open.ToString(",0.####");
                }
                text_min.Text = ticker.min.ToString(",0.####");
                text_max.Text = ticker.max.ToString(",0.####");
            }
            else
            {
                text_candle1.BackColor = Color.DarkGray;
                text_candle2.BackColor = Color.DarkGray;
                text_candle3.BackColor = Color.DarkGray;
                text_name.Text = "";
                text_change.Text = "";
                text_change_rate.Text = "";
                text_prev_close.Text = "";
                text_open.Text = "";
                text_close.Text = "";
                text_min.Text = "";
                text_max.Text = "";
            }
        }
        private void refreshTransactionHistory()
        {
            if (transaction == null) return;

            TextBox[] date = new TextBox[5] {
                text_TH0_date, text_TH1_date, text_TH2_date, text_TH3_date, text_TH4_date };
            TextBox[] unit = new TextBox[5] {
                text_TH0_unit, text_TH1_unit, text_TH2_unit, text_TH3_unit, text_TH4_unit };
            TextBox[] price = new TextBox[5] {
                text_TH0_price, text_TH1_price, text_TH2_price, text_TH3_price, text_TH4_price };
            TextBox[] total = new TextBox[5] {
                text_TH0_change, text_TH1_change, text_TH2_change, text_TH3_change, text_TH4_change };

            if (transaction.Count > 4)
            {
                for (int i = 0; i < 5; i++)
                {
                    if (transaction[i]["ask_bid"].ToString() == "BID")
                    {
                        date[i].BackColor = Color.LightPink;
                        unit[i].BackColor = Color.LightPink;
                        price[i].BackColor = Color.LightPink;
                        total[i].BackColor = Color.LightPink;
                    }
                    else
                    {
                        date[i].BackColor = Color.LightBlue;
                        unit[i].BackColor = Color.LightBlue;
                        price[i].BackColor = Color.LightBlue;
                        total[i].BackColor = Color.LightBlue;
                    }
                    string dateTime = transaction[i]["trade_date_utc"].ToString() + " " + transaction[i]["trade_time_utc"].ToString() + "Z";
                    date[i].Text = DateTime.ParseExact(dateTime, "u", null).AddHours(9).ToString("yyyy-MM-dd HH:mm:ss");
                    unit[i].Text = ((double)transaction[i]["trade_volume"]).ToString("0.####");
                    price[i].Text = ((double)transaction[i]["trade_price"]).ToString(",0.##");
                    total[i].Text = transaction[i]["change_price"].ToString();
                }
            }
            else
            {
                for (int i = 0; i < 5; i++)
                {
                    date[i].BackColor = Color.Black;
                    unit[i].BackColor = Color.Black;
                    price[i].BackColor = Color.Black;
                    total[i].BackColor = Color.Black;
                }
            }
        }
        private void refreshOrderBook()
        {
            if (ob == null) return;
            if (ob.Length > 3)
                if (ob[0].Count > 14 && ob[1].Count > 14)
                {
                    text_askPrice00.Text = ob[0][14].ToString(",0.##");
                    text_askPrice01.Text = ob[0][13].ToString(",0.##");
                    text_askPrice02.Text = ob[0][12].ToString(",0.##");
                    text_askPrice03.Text = ob[0][11].ToString(",0.##");
                    text_askPrice04.Text = ob[0][10].ToString(",0.##");
                    text_askPrice05.Text = ob[0][9].ToString(",0.##");
                    text_askPrice06.Text = ob[0][8].ToString(",0.##");
                    text_askPrice07.Text = ob[0][7].ToString(",0.##");
                    text_askPrice08.Text = ob[0][6].ToString(",0.##");
                    text_askPrice09.Text = ob[0][5].ToString(",0.##");
                    text_askPrice10.Text = ob[0][4].ToString(",0.##");
                    text_askPrice11.Text = ob[0][3].ToString(",0.##");
                    text_askPrice12.Text = ob[0][2].ToString(",0.##");
                    text_askPrice13.Text = ob[0][1].ToString(",0.##");
                    text_askPrice14.Text = ob[0][0].ToString(",0.##");

                    text_askVolume00.Text = ob[2][14].ToString("F5");
                    text_askVolume01.Text = ob[2][13].ToString("F5");
                    text_askVolume02.Text = ob[2][12].ToString("F5");
                    text_askVolume03.Text = ob[2][11].ToString("F5");
                    text_askVolume04.Text = ob[2][10].ToString("F5");
                    text_askVolume05.Text = ob[2][9].ToString("F5");
                    text_askVolume06.Text = ob[2][8].ToString("F5");
                    text_askVolume07.Text = ob[2][7].ToString("F5");
                    text_askVolume08.Text = ob[2][6].ToString("F5");
                    text_askVolume09.Text = ob[2][5].ToString("F5");
                    text_askVolume10.Text = ob[2][4].ToString("F5");
                    text_askVolume11.Text = ob[2][3].ToString("F5");
                    text_askVolume12.Text = ob[2][2].ToString("F5");
                    text_askVolume13.Text = ob[2][1].ToString("F5");
                    text_askVolume14.Text = ob[2][0].ToString("F5");

                    text_askVolume00.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][14] / THMax), 0, 0);
                    text_askVolume01.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][13] / THMax), 0, 0);
                    text_askVolume02.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][12] / THMax), 0, 0);
                    text_askVolume03.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][11] / THMax), 0, 0);
                    text_askVolume04.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][10] / THMax), 0, 0);
                    text_askVolume05.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][9] / THMax), 0, 0);
                    text_askVolume06.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][8] / THMax), 0, 0);
                    text_askVolume07.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][7] / THMax), 0, 0);
                    text_askVolume08.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][6] / THMax), 0, 0);
                    text_askVolume09.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][5] / THMax), 0, 0);
                    text_askVolume10.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][4] / THMax), 0, 0);
                    text_askVolume11.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][3] / THMax), 0, 0);
                    text_askVolume12.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][2] / THMax), 0, 0);
                    text_askVolume13.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][1] / THMax), 0, 0);
                    text_askVolume14.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][0] / THMax), 0, 0);

                    text_bidPrice00.Text = ob[1][0].ToString(",0.##");
                    text_bidPrice01.Text = ob[1][1].ToString(",0.##");
                    text_bidPrice02.Text = ob[1][2].ToString(",0.##");
                    text_bidPrice03.Text = ob[1][3].ToString(",0.##");
                    text_bidPrice04.Text = ob[1][4].ToString(",0.##");
                    text_bidPrice05.Text = ob[1][5].ToString(",0.##");
                    text_bidPrice06.Text = ob[1][6].ToString(",0.##");
                    text_bidPrice07.Text = ob[1][7].ToString(",0.##");
                    text_bidPrice08.Text = ob[1][8].ToString(",0.##");
                    text_bidPrice09.Text = ob[1][9].ToString(",0.##");
                    text_bidPrice10.Text = ob[1][10].ToString(",0.##");
                    text_bidPrice11.Text = ob[1][11].ToString(",0.##");
                    text_bidPrice12.Text = ob[1][12].ToString(",0.##");
                    text_bidPrice13.Text = ob[1][13].ToString(",0.##");
                    text_bidPrice14.Text = ob[1][14].ToString(",0.##");

                    text_bidVolume00.Text = ob[3][0].ToString("F5");
                    text_bidVolume01.Text = ob[3][1].ToString("F5");
                    text_bidVolume02.Text = ob[3][2].ToString("F5");
                    text_bidVolume03.Text = ob[3][3].ToString("F5");
                    text_bidVolume04.Text = ob[3][4].ToString("F5");
                    text_bidVolume05.Text = ob[3][5].ToString("F5");
                    text_bidVolume06.Text = ob[3][6].ToString("F5");
                    text_bidVolume07.Text = ob[3][7].ToString("F5");
                    text_bidVolume08.Text = ob[3][8].ToString("F5");
                    text_bidVolume09.Text = ob[3][9].ToString("F5");
                    text_bidVolume10.Text = ob[3][10].ToString("F5");
                    text_bidVolume11.Text = ob[3][11].ToString("F5");
                    text_bidVolume12.Text = ob[3][12].ToString("F5");
                    text_bidVolume13.Text = ob[3][13].ToString("F5");
                    text_bidVolume14.Text = ob[3][14].ToString("F5");

                    text_bidVolume00.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][0] / THMax));
                    text_bidVolume01.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][1] / THMax));
                    text_bidVolume02.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][2] / THMax));
                    text_bidVolume03.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][3] / THMax));
                    text_bidVolume04.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][4] / THMax));
                    text_bidVolume05.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][5] / THMax));
                    text_bidVolume06.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][6] / THMax));
                    text_bidVolume07.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][7] / THMax));
                    text_bidVolume08.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][8] / THMax));
                    text_bidVolume09.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][9] / THMax));
                    text_bidVolume10.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][10] / THMax));
                    text_bidVolume11.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][11] / THMax));
                    text_bidVolume12.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][12] / THMax));
                    text_bidVolume13.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][13] / THMax));
                    text_bidVolume14.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][14] / THMax));
                }
        }
        private void refreshTrade()
        {
            if (needTradeUpdate)
            {
                needTradeUpdate = false;

                isBuy = true;
                isPlace = true;
                setDefaultTrade(isBuy, isPlace);

                canTradeSet = true;
            }
        }


        private void list_coinName_MouseUp(object sender, MouseEventArgs e)
        {
            lock (lock_select)
            {
                if (list_coinName.SelectedIndex < 0 || list_coinName.SelectedIndex > list_coinName.Items.Count - 1)
                {
                    list_coinName.SelectedIndex = -1;
                    selectedName = "";

                    selected = false;
                    needTradeInit = false;
                    canTradeSet = false;
                }
                else
                {
                    selectedName = list_coinName.SelectedItem.ToString();

                    selected = true;
                    needTradeInit = true;
                    canTradeSet = false;
                }
            }

            text_trade_price.ReadOnly = true;
            text_trade_units.ReadOnly = true;
            text_trade_total.ReadOnly = true;
            text_trade_units.ForeColor = Color.DarkGray;
            text_trade_total.ForeColor = Color.DarkGray;
            text_trade_krw.Text = "";
            text_trade_price.Text = "";
            text_trade_units.Text = "";
            text_trade_total.Text = "";
            but_buy.BackColor = Color.DarkGray;
            but_sell.BackColor = Color.DarkGray;
            but_place.BackColor = Color.DarkGray;
            but_market.BackColor = Color.DarkGray;
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
        private void setDefaultTrade(bool isBuy, bool isPlace)
        {
            this.isBuy = isBuy;
            this.isPlace = isPlace;

            krw = account[0].valid;
            currency = account[1].valid;
            price = ticker.close;
            units = 0;
            total = 0;

            if (isBuy)
            {
                text_trade_type.Text = "KRW :";
                text_trade_krw.Text = krw.ToString(",0.##");
                but_buy.BackColor = Color.Red;
                but_sell.BackColor = Color.DarkGray;
            }
            else
            {
                text_trade_type.Text = selectedName + " :";
                text_trade_krw.Text = currency.ToString(",0.########");
                but_buy.BackColor = Color.DarkGray;
                but_sell.BackColor = Color.DodgerBlue;
            }

            if (isPlace)
            {
                text_trade_price.Text = price.ToString(",0.##");
                text_trade_price.ReadOnly = false;
                text_trade_price.BackColor = Color.Black;
                but_place.BackColor = Color.ForestGreen;
                but_market.BackColor = Color.DarkGray;
            }
            else
            {
                text_trade_price.Text = "";
                text_trade_price.ReadOnly = true;
                text_trade_price.BackColor = Color.DimGray;
                but_place.BackColor = Color.DarkGray;
                but_market.BackColor = Color.ForestGreen;
            }

            trackBar_price.Value = 100;
            trackBar_total.Value = 0;
            text_priceTrack_value.Text = "[   100%   ]";
            text_totalTrack_value.Text = "[   0%   ]";
            text_trade_units.Text = "Units";
            text_trade_total.Text = "Total  (5000↑)";
            text_trade_units.ForeColor = Color.DarkGray;
            text_trade_total.ForeColor = Color.DarkGray;
        }


        private void text_trade_price_Leave(object sender, EventArgs e)
        {
            if (!canTradeSet || !isPlace) return;

            double tempPrice;
            if (double.TryParse(text_trade_price.Text, out tempPrice))
            {
                price = tempPrice;
                if (price > 2000000) price = Convert.ToInt32(price / 1000) * 1000;
                else if (price > 1000000) price = Convert.ToInt32(price / 500) * 500;
                else if (price > 500000) price = Convert.ToInt32(price / 100) * 100;
                else if (price > 100000) price = Convert.ToInt32(price / 50) * 50;
                else if (price > 10000) price = Convert.ToInt32(price / 10) * 10;
                else if (price > 1000) price = Convert.ToInt32(price / 5) * 5;
                else if (price > 100) price = Math.Round(price);
                else if (price > 10) price = Math.Round(price, 1);
                else price = Math.Round(price, 2);

                if (isBuy && total > 0)
                {
                    units = total / price;
                    text_trade_units.Text = units.ToString(",0.########");
                }
                else if (!isBuy && units > 0)
                {
                    total = units * price;
                    text_trade_total.Text = total.ToString(",0.##");
                }
            }
            else
            {
                if(text_trade_price.Text != "")
                    MessageBox.Show("Only can write NUMBER.");
                price = ticker.close;
            }
            text_trade_price.Text = price.ToString(",0.##");
        }
        private void trackBar_price_Scroll(object sender, EventArgs e)
        {
            if (!canTradeSet || !isPlace) return;

            price = ticker.close * trackBar_price.Value * 0.01;
            if (price > 2000000) price = Convert.ToInt32(price / 1000) * 1000;
            else if (price > 1000000) price = Convert.ToInt32(price / 500) * 500;
            else if (price > 500000) price = Convert.ToInt32(price / 100) * 100;
            else if (price > 100000) price = Convert.ToInt32(price / 50) * 50;
            else if (price > 10000) price = Convert.ToInt32(price / 10) * 10;
            else if (price > 1000) price = Convert.ToInt32(price / 5) * 5;
            else if (price > 100) price = Math.Round(price);
            else if (price > 10) price = Math.Round(price, 1);
            else price = Math.Round(price, 2);

            text_trade_price.Text = price.ToString();
            text_priceTrack_value.Text = "[   " + trackBar_price.Value + "%   ]";
            if (isBuy && total > 0)
            {
                units = total / price;
                text_trade_units.Text = units.ToString(",0.####");
            }
            else if (!isBuy && units > 0)
            {
                total = units * price;
                text_trade_total.Text = total.ToString(",0");
            }
        }


        private void text_trade_input_MouseUp(object sender, MouseEventArgs e)
        {
            if (!canTradeSet) return;

            ((TextBox)sender).ReadOnly = false;
            double tempTotal;
            if (!double.TryParse(((TextBox)sender).Text, out tempTotal))
                ((TextBox)sender).Text = "";
            ((TextBox)sender).ForeColor = Color.White;
        }
        private void text_trade_input_Leave(object sender, EventArgs e)
        {
            if (!canTradeSet) return;

            double tempUnits = 0;
            double tempTotal = 0;
            bool unitTry = double.TryParse(text_trade_units.Text, out tempUnits);
            bool totalTry = double.TryParse(text_trade_total.Text, out tempTotal);
            bool test = (sender == text_trade_units) ? unitTry : totalTry;
            if (test)
            {
                units = (sender == text_trade_units) ? tempUnits : (double)(total / price);
                total = (sender == text_trade_units) ? price * units : tempTotal;
                if (sender == text_trade_units)
                {
                    text_trade_total.ForeColor = Color.White;
                    text_trade_total.Text = (price * units).ToString(",0");
                }
                else
                {
                    text_trade_units.ForeColor = Color.White;
                    text_trade_units.Text = (total / price).ToString(",0.####");
                }
            }
            else if(((TextBox)sender).Text != "")
            {
                units = 0;
                total = 0;
                text_trade_units.Text = "";
                text_trade_total.Text = "";
                MessageBox.Show("Only can write NUMBER.");
            }
        }
        private void trackBar_total_Scroll(object sender, EventArgs e)
        {
            if (!canTradeSet) return;

            if (isBuy)
            {
                total = (trackBar_total.Value == 100) ? krw : (krw * trackBar_total.Value * 0.01);
                if (isPlace && price > 0)
                    units = total / price;
                else if (!isPlace && ticker.close > 0)
                    units = total / ticker.close;
            }
            else
            {
                units = (trackBar_total.Value == 100) ? currency : (currency * trackBar_total.Value * 0.01);
                if (isPlace && price > 0)
                    total = units * price;
                else if (!isPlace && ticker.close > 0)
                    total = units * ticker.close;
            }
            text_trade_units.Text = units.ToString(",0.####");
            text_trade_total.Text = total.ToString(",0");
            text_trade_units.ForeColor = Color.White;
            text_trade_total.ForeColor = Color.White;
            text_totalTrack_value.Text = "[   " + trackBar_total.Value + "%   ]";
        }


        private void but_buy_Click(object sender, EventArgs e)
        {
            if (canTradeSet) setDefaultTrade(true, isPlace);
        }
        private void but_sell_Click(object sender, EventArgs e)
        {
            if (canTradeSet) setDefaultTrade(false, isPlace);
        }
        private void but_place_Click(object sender, EventArgs e)
        {
            if (canTradeSet) setDefaultTrade(isBuy, true);
        }
        private void but_market_Click(object sender, EventArgs e)
        {
            if (canTradeSet) setDefaultTrade(isBuy, false);
        }
        private void but_execute_Click(object sender, EventArgs e)
        {
            if (!canTradeSet) return;
            if (total < 5000d)
            {
                MessageBox.Show("Total value is less than 5000 KRW.");
                return;
            }

            string type = isBuy ? "Buy" : "Sell";
            string how = isPlace ? "Place " : "Market ";
            string tempPrice = isPlace ? price.ToString("0.##") : "Market Price";
            DialogResult test = MessageBox.Show(
                "Name : " + selectedName + Environment.NewLine
                + "Type : " + how + type + Environment.NewLine
                + "Unit : " + units.ToString("0.########") + Environment.NewLine
                + "Price : " + tempPrice + Environment.NewLine
                + "Total : " + total.ToString("0.##")
                , "Confirm", MessageBoxButtons.OKCancel);
            if (test != DialogResult.OK) return;

            JObject ret = react.executeDeal(isBuy, isPlace, selectedName, units, price, total);
            if (ret == null)
            {
                MessageBox.Show("API error, try again.");
                return;
            }

            lock (((Main)Owner).lock_tradeHistory)
            {
                TradeData tempData = new TradeData();
                tempData.uuid = ret["uuid"].ToString();
                tempData.date = DateTime.Now;
                tempData.coinName = selectedName;
                tempData.isBid = isBuy;
                tempData.unit = units;
                tempData.price = isPlace ? price : 0;
                tempData.fee = 0;
                ((Main)Owner).tradeHistory.addNewPending(tempData);
                ((Main)Owner).tradeHistory.saveFile();
            }
            canTradeSet = false;
            needTradeInit = true;
            ((Main)Owner).logIn(new Output(0, "Trade execution", selectedName + ", " + how + type + ", " + units + ", " + tempPrice));
            MessageBox.Show("Success!");
        }
    }
}
