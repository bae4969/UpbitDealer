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
        private bool isInit = false;

        private string selectedName = "";
        private double krw = 0d;
        private double currency = 0d;
        private double price = 0d;
        private double units = 0d;
        private double total = 0d;

        private Thread thread_updater;
        private readonly object lock_updater = new object();
        private readonly object lock_select = new object();
        private double[] tickerData = new double[11];
        private JArray transactionData = new JArray();
        private List<double>[] ob = null;
        private double THMax = double.MinValue;
        private double[] vbalanceData = new double[7];

        private bool selected = true;
        private bool needTradeInit = false;
        private bool needTradeUpdate = false;
        private bool canTradeSet = false;
        private bool isBuy = true;
        private bool isPlace = true;


        public Trader(string access_key, string secret_key, List<string> coinList)
        {
            InitializeComponent();
            this.react = new React(access_key, secret_key);
            this.coinList = coinList;
        }
        private void Trader_Load(object sender, EventArgs e)
        {
            if (coinList.Count == 0)
            {
                MessageBox.Show("Init fail, try again.");
                Close();
            }

            for (int i = 0; i < coinList.Count; i++)
                list_coinName.Items.Add(coinList[i]);

            thread_updater = new Thread(() => executeDataUpdate());
            thread_updater.Start();
            isInit = true;
        }
        private void Trader_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (!isInit) return;

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
                        string name = selectedName;
                        bool[] isNeedUpdate = { false, false, false, false };

                        double[] tempTickerData = null;
                        JArray tempTHData = null;
                        List<double>[] tempOBData = null;
                        double tempTHMax = double.MinValue;
                        double[] tempBalacneData = null;

                        if (selected)
                        {
                            tempTickerData = react.getTickerData(name);
                            if (tempTickerData != null) isNeedUpdate[0] = true;
                            else selected = false;
                        }

                        if (selected)
                        {
                            tempTHData = react.getTransactionData(name);
                            if (tempTHData != null) isNeedUpdate[1] = true;
                            else selected = false;
                        }

                        if (selected)
                        {
                            tempOBData = react.getOrderBookData(name);
                            if (tempOBData != null)
                            {
                                for (int i = 0; i < 15; i++)
                                {
                                    tempTHMax = Math.Max(tempTHMax, tempOBData[2][i]);
                                    tempTHMax = Math.Max(tempTHMax, tempOBData[3][i]);
                                }
                                isNeedUpdate[2] = true;
                            }
                            else selected = false;
                        }

                        if (needTradeInit)
                        {
                            tempBalacneData = react.getBalanceData(name);
                            if (tempBalacneData != null)
                            {
                                isNeedUpdate[3] = true;
                                needTradeInit = false;
                            }
                        }

                        if (selected)
                        {
                            lock (lock_updater)
                            {
                                if (isNeedUpdate[0])
                                    for (int i = 0; i < 11; i++)
                                        tickerData[i] = tempTickerData[i];

                                if (isNeedUpdate[1])
                                    transactionData = tempTHData;

                                if (isNeedUpdate[2])
                                {
                                    ob = tempOBData;
                                    THMax = tempTHMax;
                                }

                                if (isNeedUpdate[3])
                                {
                                    for (int i = 0; i < 7; i++)
                                        vbalanceData[i] = tempBalacneData[i];
                                    vbalanceData[6] = tickerData[1];
                                    needTradeUpdate = true;
                                }
                            }
                        }
                    }

                for (int i = 0; i < 10; i++)
                {
                    if (AllStop || needTradeInit) break;
                    Thread.Sleep(100);
                }
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
                text_value.ForeColor = Color.White;
                text_value.Text = "Delisting";
                text_fluctate.Text = "";
                text_fluctate_rate.Text = "";
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
                if (tickerData[10] >= 0)
                {
                    text_value.ForeColor = Color.Red;
                    text_fluctate.ForeColor = Color.Red;
                    text_fluctate_rate.ForeColor = Color.Red;
                }
                else
                {
                    text_value.ForeColor = Color.DodgerBlue;
                    text_fluctate.ForeColor = Color.DodgerBlue;
                    text_fluctate_rate.ForeColor = Color.DodgerBlue;
                }
                text_fluctate.Text = tickerData[9].ToString(",0.####");
                text_fluctate_rate.Text = tickerData[10].ToString("0.##") + "%";
                text_prev_close.Text = tickerData[6].ToString(",0.####");
                if (tickerData[0] <= tickerData[1])
                {
                    text_candle1.BackColor = Color.Red;
                    text_candle2.BackColor = Color.Red;
                    text_candle3.BackColor = Color.Red;
                    text_open.Text = tickerData[0].ToString(",0.####");
                    text_close.Text = tickerData[1].ToString(",0.####");
                }
                else
                {
                    text_candle1.BackColor = Color.DodgerBlue;
                    text_candle2.BackColor = Color.DodgerBlue;
                    text_candle3.BackColor = Color.DodgerBlue;
                    text_open.Text = tickerData[1].ToString(",0.####");
                    text_close.Text = tickerData[0].ToString(",0.####");
                }
                text_min.Text = tickerData[2].ToString(",0.####");
                text_max.Text = tickerData[3].ToString(",0.####");
            }
            else
            {
                text_candle1.BackColor = Color.DarkGray;
                text_candle2.BackColor = Color.DarkGray;
                text_candle3.BackColor = Color.DarkGray;
                text_name.Text = "";
                text_fluctate.Text = "";
                text_fluctate_rate.Text = "";
                text_prev_close.Text = "";
                text_open.Text = "";
                text_close.Text = "";
                text_min.Text = "";
                text_max.Text = "";
            }
        }
        private void refreshTransactionHistory()
        {
            if (transactionData.Count > 4)
            {
                text_value.Text = transactionData[4]["trade_price"].ToString();

                {
                    if (transactionData[0]["ask_bid"].ToString() == "BID")
                    {
                        text_TH0_date.BackColor = Color.LightBlue;
                        text_TH0_unit.BackColor = Color.LightBlue;
                        text_TH0_value.BackColor = Color.LightBlue;
                        text_TH0_total.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        text_TH0_date.BackColor = Color.LightPink;
                        text_TH0_unit.BackColor = Color.LightPink;
                        text_TH0_value.BackColor = Color.LightPink;
                        text_TH0_total.BackColor = Color.LightPink;
                    }
                    text_TH0_date.Text = transactionData[0]["trade_date_utc"].ToString() + " " + transactionData[0]["trade_time_utc"].ToString();
                    text_TH0_unit.Text = ((double)transactionData[0]["trade_volume"]).ToString("0.####");
                    text_TH0_value.Text = ((double)transactionData[0]["trade_price"]).ToString(",0.##");
                    text_TH0_total.Text = transactionData[0]["change_price"].ToString();
                }
                {
                    if (transactionData[1]["ask_bid"].ToString() == "BID")
                    {
                        text_TH1_date.BackColor = Color.LightBlue;
                        text_TH1_unit.BackColor = Color.LightBlue;
                        text_TH1_value.BackColor = Color.LightBlue;
                        text_TH1_total.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        text_TH1_date.BackColor = Color.LightPink;
                        text_TH1_unit.BackColor = Color.LightPink;
                        text_TH1_value.BackColor = Color.LightPink;
                        text_TH1_total.BackColor = Color.LightPink;
                    }
                    text_TH1_date.Text = transactionData[1]["trade_date_utc"].ToString() + " " + transactionData[1]["trade_time_utc"].ToString();
                    text_TH1_unit.Text = ((double)transactionData[1]["trade_volume"]).ToString("0.####");
                    text_TH1_value.Text = ((double)transactionData[1]["trade_price"]).ToString(",0.##");
                    text_TH1_total.Text = transactionData[1]["change_price"].ToString();
                }
                {
                    if (transactionData[2]["ask_bid"].ToString() == "BID")
                    {
                        text_TH2_date.BackColor = Color.LightBlue;
                        text_TH2_unit.BackColor = Color.LightBlue;
                        text_TH2_value.BackColor = Color.LightBlue;
                        text_TH2_total.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        text_TH2_date.BackColor = Color.LightPink;
                        text_TH2_unit.BackColor = Color.LightPink;
                        text_TH2_value.BackColor = Color.LightPink;
                        text_TH2_total.BackColor = Color.LightPink;
                    }
                    text_TH2_date.Text = transactionData[2]["trade_date_utc"].ToString() + " " + transactionData[2]["trade_time_utc"].ToString();
                    text_TH2_unit.Text = ((double)transactionData[2]["trade_volume"]).ToString("0.####");
                    text_TH2_value.Text = ((double)transactionData[2]["trade_price"]).ToString(",0.##");
                    text_TH2_total.Text = transactionData[2]["change_price"].ToString();
                }
                {
                    if (transactionData[3]["ask_bid"].ToString() == "BID")
                    {
                        text_TH3_date.BackColor = Color.LightBlue;
                        text_TH3_unit.BackColor = Color.LightBlue;
                        text_TH3_value.BackColor = Color.LightBlue;
                        text_TH3_total.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        text_TH3_date.BackColor = Color.LightPink;
                        text_TH3_unit.BackColor = Color.LightPink;
                        text_TH3_value.BackColor = Color.LightPink;
                        text_TH3_total.BackColor = Color.LightPink;
                    }
                    text_TH3_date.Text = transactionData[3]["trade_date_utc"].ToString() + " " + transactionData[3]["trade_time_utc"].ToString();
                    text_TH3_unit.Text = ((double)transactionData[3]["trade_volume"]).ToString("0.####");
                    text_TH3_value.Text = ((double)transactionData[3]["trade_price"]).ToString(",0.##");
                    text_TH3_total.Text = transactionData[3]["change_price"].ToString();
                }
                {
                    if (transactionData[4]["ask_bid"].ToString() == "BID")
                    {
                        text_TH4_date.BackColor = Color.LightBlue;
                        text_TH4_unit.BackColor = Color.LightBlue;
                        text_TH4_value.BackColor = Color.LightBlue;
                        text_TH4_total.BackColor = Color.LightBlue;
                    }
                    else
                    {
                        text_TH4_date.BackColor = Color.LightPink;
                        text_TH4_unit.BackColor = Color.LightPink;
                        text_TH4_value.BackColor = Color.LightPink;
                        text_TH4_total.BackColor = Color.LightPink;
                    }
                    text_TH4_date.Text = transactionData[4]["trade_date_utc"].ToString() + " " + transactionData[4]["trade_time_utc"].ToString();
                    text_TH4_unit.Text = ((double)transactionData[4]["trade_volume"]).ToString("0.####");
                    text_TH4_value.Text = ((double)transactionData[4]["trade_price"]).ToString(",0.##");
                    text_TH4_total.Text = transactionData[4]["change_price"].ToString();
                }
            }
            else
            {
                text_TH0_date.BackColor = Color.Black;
                text_TH0_unit.BackColor = Color.Black;
                text_TH0_value.BackColor = Color.Black;
                text_TH0_total.BackColor = Color.Black;
                text_TH1_date.BackColor = Color.Black;
                text_TH1_unit.BackColor = Color.Black;
                text_TH1_value.BackColor = Color.Black;
                text_TH1_total.BackColor = Color.Black;
                text_TH2_date.BackColor = Color.Black;
                text_TH2_unit.BackColor = Color.Black;
                text_TH2_value.BackColor = Color.Black;
                text_TH2_total.BackColor = Color.Black;
                text_TH3_date.BackColor = Color.Black;
                text_TH3_unit.BackColor = Color.Black;
                text_TH3_value.BackColor = Color.Black;
                text_TH3_total.BackColor = Color.Black;
                text_TH4_date.BackColor = Color.Black;
                text_TH4_unit.BackColor = Color.Black;
                text_TH4_value.BackColor = Color.Black;
                text_TH4_total.BackColor = Color.Black;
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

                    text_askQuantity00.Text = ob[2][14].ToString("F5");
                    text_askQuantity01.Text = ob[2][13].ToString("F5");
                    text_askQuantity02.Text = ob[2][12].ToString("F5");
                    text_askQuantity03.Text = ob[2][11].ToString("F5");
                    text_askQuantity04.Text = ob[2][10].ToString("F5");
                    text_askQuantity05.Text = ob[2][9].ToString("F5");
                    text_askQuantity06.Text = ob[2][8].ToString("F5");
                    text_askQuantity07.Text = ob[2][7].ToString("F5");
                    text_askQuantity08.Text = ob[2][6].ToString("F5");
                    text_askQuantity09.Text = ob[2][5].ToString("F5");
                    text_askQuantity10.Text = ob[2][4].ToString("F5");
                    text_askQuantity11.Text = ob[2][3].ToString("F5");
                    text_askQuantity12.Text = ob[2][2].ToString("F5");
                    text_askQuantity13.Text = ob[2][1].ToString("F5");
                    text_askQuantity14.Text = ob[2][0].ToString("F5");

                    text_askQuantity00.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][0] / THMax), 0, 0);
                    text_askQuantity01.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][1] / THMax), 0, 0);
                    text_askQuantity02.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][2] / THMax), 0, 0);
                    text_askQuantity03.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][3] / THMax), 0, 0);
                    text_askQuantity04.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][4] / THMax), 0, 0);
                    text_askQuantity05.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][5] / THMax), 0, 0);
                    text_askQuantity06.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][6] / THMax), 0, 0);
                    text_askQuantity07.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][7] / THMax), 0, 0);
                    text_askQuantity08.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][8] / THMax), 0, 0);
                    text_askQuantity09.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][9] / THMax), 0, 0);
                    text_askQuantity10.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][10] / THMax), 0, 0);
                    text_askQuantity11.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][11] / THMax), 0, 0);
                    text_askQuantity12.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][12] / THMax), 0, 0);
                    text_askQuantity13.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][13] / THMax), 0, 0);
                    text_askQuantity14.BackColor = Color.FromArgb(55 + (int)(200 * ob[2][14] / THMax), 0, 0);

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

                    text_bidQuantity00.Text = ob[3][0].ToString("F5");
                    text_bidQuantity01.Text = ob[3][1].ToString("F5");
                    text_bidQuantity02.Text = ob[3][2].ToString("F5");
                    text_bidQuantity03.Text = ob[3][3].ToString("F5");
                    text_bidQuantity04.Text = ob[3][4].ToString("F5");
                    text_bidQuantity05.Text = ob[3][5].ToString("F5");
                    text_bidQuantity06.Text = ob[3][6].ToString("F5");
                    text_bidQuantity07.Text = ob[3][7].ToString("F5");
                    text_bidQuantity08.Text = ob[3][8].ToString("F5");
                    text_bidQuantity09.Text = ob[3][9].ToString("F5");
                    text_bidQuantity10.Text = ob[3][10].ToString("F5");
                    text_bidQuantity11.Text = ob[3][11].ToString("F5");
                    text_bidQuantity12.Text = ob[3][12].ToString("F5");
                    text_bidQuantity13.Text = ob[3][13].ToString("F5");
                    text_bidQuantity14.Text = ob[3][14].ToString("F5");

                    text_bidQuantity00.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][0] / THMax));
                    text_bidQuantity01.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][1] / THMax));
                    text_bidQuantity02.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][2] / THMax));
                    text_bidQuantity03.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][3] / THMax));
                    text_bidQuantity04.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][4] / THMax));
                    text_bidQuantity05.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][5] / THMax));
                    text_bidQuantity06.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][6] / THMax));
                    text_bidQuantity07.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][7] / THMax));
                    text_bidQuantity08.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][8] / THMax));
                    text_bidQuantity09.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][9] / THMax));
                    text_bidQuantity10.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][10] / THMax));
                    text_bidQuantity11.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][11] / THMax));
                    text_bidQuantity12.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][12] / THMax));
                    text_bidQuantity13.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][13] / THMax));
                    text_bidQuantity14.BackColor = Color.FromArgb(0, 0, 55 + (int)(200 * ob[3][14] / THMax));
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
        private void setDefaultTrade(bool isBuy, bool isPlace)
        {
            this.isBuy = isBuy;
            this.isPlace = isPlace;

            krw = vbalanceData[2];
            currency = vbalanceData[5];
            price = vbalanceData[6];
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
            text_trade_units.Text = "";
            text_trade_total.Text = "Total  (5000↑)";
            text_trade_units.ForeColor = Color.DarkGray;
            text_trade_total.ForeColor = Color.DarkGray;
        }


        private void text_trade_price_KeyUp(object sender, KeyEventArgs e)
        {
            if (!canTradeSet || !isPlace) return;
            if (e.KeyCode == Keys.Enter || e.KeyCode == Keys.Oemcomma || e.KeyCode == Keys.Tab) return;

            int tempPrice;
            if (int.TryParse(text_trade_price.Text, out tempPrice))
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
                if (e.KeyCode == Keys.Enter) return;
                price = vbalanceData[6];
                text_trade_price.Text = price.ToString(",0");
                MessageBox.Show("Only can write NUMBER.");
            }
        }
        private void text_trade_price_Leave(object sender, EventArgs e)
        {
            if (!canTradeSet) return;

            text_trade_price.Text = price.ToString(",0.##");
        }
        private void trackBar_price_Scroll(object sender, EventArgs e)
        {
            if (!canTradeSet || !isPlace) return;

            price = tickerData[1] * trackBar_price.Value / 100d;
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
        private void text_trade_input_KeyUp(object sender, KeyEventArgs e)
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
            else
            {
                if (e.KeyCode == Keys.Enter) return;
                units = 0;
                total = 0;
                text_trade_units.Text = "";
                text_trade_total.Text = "";
                MessageBox.Show("Only can write NUMBER.");
            }
        }
        private void text_trade_input_Leave(object sender, EventArgs e)
        {
            if (canTradeSet)
                checkUnitTotal();
        }
        private void trackBar_total_Scroll(object sender, EventArgs e)
        {
            if (!canTradeSet) return;

            if (isBuy)
            {
                total = (trackBar_total.Value == 100) ? krw : (krw * trackBar_total.Value / 100d);
                if (isPlace && price > 0)
                    units = total / price;
                else if (!isPlace && vbalanceData[6] > 0)
                    units = total / vbalanceData[6];
            }
            else
            {
                units = (trackBar_total.Value == 100) ? currency : (currency * trackBar_total.Value / 100d);
                if (isPlace && price > 0)
                    total = units * price;
                else if (!isPlace && vbalanceData[6] > 0)
                    total = units * vbalanceData[6];
            }
            text_trade_units.Text = units.ToString(",0.####");
            text_trade_total.Text = total.ToString(",0");
            text_trade_units.ForeColor = Color.White;
            text_trade_total.ForeColor = Color.White;
            text_totalTrack_value.Text = "[   " + trackBar_total.Value + "%   ]";
        }
        private void trackBar_total_MouseUp(object sender, MouseEventArgs e)
        {
            if (canTradeSet)
                if (!checkUnitTotal())
                    trackBar_total.Value = 0;
        }
        private bool checkUnitTotal()
        {
            double tempkrw = isBuy ? krw : currency;
            double tempTotal = isBuy ? total : units;
            if (text_trade_total.Text == "" || total == 0) return true;
            if (total < 5000 || tempTotal > tempkrw)
            {
                units = 0;
                total = 0;
                text_trade_units.Text = "";
                text_trade_total.Text = "";
                MessageBox.Show("Range error.");

                return false;
            }

            return true;
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
            ((Main)Owner).logIn(new output(0, "Trade execution", selectedName + ", " + how + type + ", " + units + ", " + tempPrice));
            MessageBox.Show("Success!");
        }
    }
}
