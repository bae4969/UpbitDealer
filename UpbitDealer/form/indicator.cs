using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using UpbitDealer.src;

namespace UpbitDealer.form
{
    public partial class Indicator : Form
    {
        private CoinType CT = new CoinType();
        private DataSet[] bollinger = new DataSet[5];
        private DataView[,] chartData = new DataView[5, 4];
        private NameValue[,] maxMin = new NameValue[5, 2];
        private List<string> hotList;
        private List<string> dangerList;


        public Indicator()
        {
            InitializeComponent();
        }
        private void Indicator_Load(object sender, EventArgs e)
        {
            lock (((Main)Owner).lock_mainUpdater)
            {
                hotList = new List<string>(((Main)Owner).mainUpdater.hotList);
                dangerList = new List<string>(((Main)Owner).mainUpdater.dangerList);
            }
            lock (((Main)Owner).lock_macro)
                for (int i = 0; i < 5; i++)
                    bollinger[i] = ((Main)Owner).macro.bollinger[i].Copy();

            if (bollinger[1].Tables["BTC"].Rows.Count < 1)
            {
                MessageBox.Show(
                    "Macro does not finish to load candle data.\n" +
                    "Open this form, after macro finish to load.");
                Close();
                return;
            }

            for (int i = 0; i < hotList.Count; i++) list_hotList.Items.Add(hotList[i]);
            for (int i = 0; i < dangerList.Count; i++) list_dangerList.Items.Add(dangerList[i]);

            DataTable[,] bbTable = new DataTable[5, 4];
            for (int i = 0; i < 5; i++)
            {
                bbTable[i, 0] = new DataTable();
                bbTable[i, 0].Columns.Add("date", typeof(DateTime));
                bbTable[i, 0].Columns.Add("top3", typeof(double));
                bbTable[i, 0].Columns.Add("avg", typeof(double));

                bbTable[i, 1] = new DataTable();
                bbTable[i, 1].Columns.Add("date", typeof(DateTime));
                bbTable[i, 1].Columns.Add("btc", typeof(double));
                bbTable[i, 1].Columns.Add("eth", typeof(double));
                bbTable[i, 1].Columns.Add("xrp", typeof(double));

                bbTable[i, 2] = new DataTable();
                bbTable[i, 2].Columns.Add("date", typeof(DateTime));
                bbTable[i, 2].Columns.Add("platform", typeof(double));
                bbTable[i, 2].Columns.Add("util", typeof(double));
                bbTable[i, 2].Columns.Add("pay", typeof(double));

                bbTable[i, 3] = new DataTable();
                bbTable[i, 3].Columns.Add("date", typeof(DateTime));
                bbTable[i, 3].Columns.Add("kor", typeof(double));
                bbTable[i, 3].Columns.Add("chi", typeof(double));
                bbTable[i, 3].Columns.Add("sea", typeof(double));

                maxMin[i, 0] = new NameValue("", 0);
                maxMin[i, 1] = new NameValue("", 0);

                int maxIndex = -1;
                int minIndex = -1;
                double max = double.MinValue;
                double min = double.MaxValue;
                for (int j = 0; j < bollinger[i].Tables.Count; j++)
                {
                    if (bollinger[i].Tables[j].Rows.Count > 0)
                    {
                        double temp = (double)bollinger[i].Tables[j].Rows[0]["value"];
                        if (max < temp) { maxIndex = j; max = temp; }
                        if (min > temp) { minIndex = j; min = temp; }
                    }
                }

                maxMin[i, 0].coinName = bollinger[i].Tables[maxIndex].TableName;
                maxMin[i, 0].value = max;
                maxMin[i, 1].coinName = bollinger[i].Tables[minIndex].TableName;
                maxMin[i, 1].value = min;

                for (int j = 0; j < bollinger[i].Tables["BTC"].Rows.Count; j++)
                {
                    DateTime dateTime = (DateTime)bollinger[i].Tables["BTC"].Rows[j]["date"];

                    double top3
                        = ((double)bollinger[i].Tables["BTC"].Rows[j]["value"]
                        + (double)bollinger[i].Tables["ETH"].Rows[j]["value"]
                        + (double)bollinger[i].Tables["XRP"].Rows[j]["value"]) / 3d;
                    double avg = 0;
                    double count = 0;

                    double btc = 0; double btc_count = 0;
                    double eth = 0; double eth_count = 0;
                    double xrp = 0; double xrp_count = 0;

                    double platform = 0; double platform_count = 0;
                    double util = 0; double util_count = 0;
                    double pay = 0; double pay_count = 0;

                    double kor = 0; double kor_count = 0;
                    double chi = 0; double chi_count = 0;
                    double sea = 0; double sea_count = 0;

                    for (int k = 0; k < bollinger[i].Tables.Count; k++)
                    {
                        if (bollinger[i].Tables[k].Rows.Count > j)
                        {
                            double temp = (double)bollinger[i].Tables[k].Rows[j]["value"];

                            avg += temp;
                            count += 1;

                            if (CT.Bit.Contains(bollinger[i].Tables[k].TableName)) { btc += temp; btc_count += 1; }
                            if (CT.Eth.Contains(bollinger[i].Tables[k].TableName)) { eth += temp; eth_count += 1; }
                            if (CT.Xrp.Contains(bollinger[i].Tables[k].TableName)) { xrp += temp; xrp_count += 1; }

                            if (CT.Platform.Contains(bollinger[i].Tables[k].TableName)) { platform += temp; platform_count += 1; }
                            if (CT.Util.Contains(bollinger[i].Tables[k].TableName)) { util += temp; util_count += 1; }
                            if (CT.Pay.Contains(bollinger[i].Tables[k].TableName)) { pay += temp; pay_count += 1; }

                            if (CT.Kor.Contains(bollinger[i].Tables[k].TableName)) { kor += temp; kor_count += 1; }
                            if (CT.Chi.Contains(bollinger[i].Tables[k].TableName)) { chi += temp; chi_count += 1; }
                            if (CT.Sea.Contains(bollinger[i].Tables[k].TableName)) { sea += temp; sea_count += 1; }
                        }
                    }

                    btc = btc_count == 0 ? 0 : btc / btc_count;
                    eth = eth_count == 0 ? 0 : eth / eth_count;
                    xrp = xrp_count == 0 ? 0 : xrp / xrp_count;

                    platform = platform_count == 0 ? 0 : platform / platform_count;
                    util = util_count == 0 ? 0 : util / util_count;
                    pay = pay_count == 0 ? 0 : pay / pay_count;

                    kor = kor_count == 0 ? 0 : kor / kor_count;
                    chi = chi_count == 0 ? 0 : chi / chi_count;
                    sea = sea_count == 0 ? 0 : sea / sea_count;

                    bbTable[i, 0].Rows.Add(dateTime, top3, avg / count);
                    bbTable[i, 1].Rows.Add(dateTime, btc, eth, xrp);
                    bbTable[i, 2].Rows.Add(dateTime, platform, util, pay);
                    bbTable[i, 3].Rows.Add(dateTime, kor, chi, sea);
                }

                for (int j = 0; j < 4; j++)
                    chartData[i, j] = new DataView(bbTable[i, j]);
            }

            setDefault();
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            groupBox1.Focus();
        }


        private void setDefault(int index = 0)
        {
            setDefaultButton();

            Chart[] chartPtr = new Chart[4];
            chartPtr[0] = chart1;
            chartPtr[1] = chart2;
            chartPtr[2] = chart3;
            chartPtr[3] = chart4;


            chartPtr[0].Series["top3"].Points.DataBind(chartData[index, 0], "date", "top3", "");
            chartPtr[0].Series["avg"].Points.DataBind(chartData[index, 0], "date", "avg", "");

            chartPtr[1].Series["btc"].Points.DataBind(chartData[index, 1], "date", "btc", "");
            chartPtr[1].Series["eth"].Points.DataBind(chartData[index, 1], "date", "eth", "");
            chartPtr[1].Series["xrp"].Points.DataBind(chartData[index, 1], "date", "xrp", "");

            chartPtr[2].Series["platform"].Points.DataBind(chartData[index, 2], "date", "platform", "");
            chartPtr[2].Series["util"].Points.DataBind(chartData[index, 2], "date", "util", "");
            chartPtr[2].Series["pay"].Points.DataBind(chartData[index, 2], "date", "pay", "");

            chartPtr[3].Series["kor"].Points.DataBind(chartData[index, 3], "date", "kor", "");
            chartPtr[3].Series["chi"].Points.DataBind(chartData[index, 3], "date", "chi", "");
            chartPtr[3].Series["sea"].Points.DataBind(chartData[index, 3], "date", "sea", "");


            text_top3.Text = ((double)chartData[index, 0][0]["top3"]).ToString("0.##");
            text_avg.Text = ((double)chartData[index, 0][0]["avg"]).ToString("0.##");

            text_btc.Text = ((double)chartData[index, 1][0]["btc"]).ToString("0.##");
            text_eth.Text = ((double)chartData[index, 1][0]["eth"]).ToString("0.##");
            text_xrp.Text = ((double)chartData[index, 1][0]["xrp"]).ToString("0.##");

            text_platform.Text = ((double)chartData[index, 2][0]["platform"]).ToString("0.##");
            text_util.Text = ((double)chartData[index, 2][0]["util"]).ToString("0.##");
            text_pay.Text = ((double)chartData[index, 2][0]["pay"]).ToString("0.##");

            text_kor.Text = ((double)chartData[index, 3][0]["kor"]).ToString("0.##");
            text_chi.Text = ((double)chartData[index, 3][0]["chi"]).ToString("0.##");
            text_sea.Text = ((double)chartData[index, 3][0]["sea"]).ToString("0.##");

            text_max_name.Text = maxMin[index, 0].coinName;
            text_max_value.Text = maxMin[index, 0].value.ToString("0.##");
            text_min_name.Text = maxMin[index, 1].coinName;
            text_min_value.Text = maxMin[index, 1].value.ToString("0.##");

            DateTime xMax = (DateTime)chartData[index, 0][0][0];
            DateTime xMin = (DateTime)chartData[index, 0][chartData[index, 0].Count - 1][0];
            switch (index)
            {
                case 0:
                    btn_min30.BackColor = Color.Red;
                    for (int i = 0; i < 4; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax.AddMinutes(30).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin.ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Hours;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 3;
                    }
                    break;
                case 1:
                    btn_hour1.BackColor = Color.Red;
                    for (int i = 0; i < 4; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax.AddHours(1).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin.ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Hours;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 6;
                    }
                    break;
                case 2:
                    btn_hour4.BackColor = Color.Red;
                    for (int i = 0; i < 4; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax.AddHours(4).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin.ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Days;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 1;
                    }
                    break;
                case 3:
                    btn_day.BackColor = Color.Red;
                    for (int i = 0; i < 4; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax.AddDays(1).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin.ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Days;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 6;
                    }
                    break;
                case 4:
                    btn_week.BackColor = Color.Red;
                    for (int i = 0; i < 4; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax.AddDays(7).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin.ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 6;
                    }
                    break;
            }
        }
        private void setDefaultButton()
        {
            btn_min30.BackColor = Color.DarkGray;
            btn_hour1.BackColor = Color.DarkGray;
            btn_hour4.BackColor = Color.DarkGray;
            btn_day.BackColor = Color.DarkGray;
            btn_week.BackColor = Color.DarkGray;
        }


        private void btn_min30_Click(object sender, EventArgs e)
        {
            setDefault(0);
        }
        private void btn_hour1_Click(object sender, EventArgs e)
        {
            setDefault(1);
        }
        private void btn_hour4_Click(object sender, EventArgs e)
        {
            setDefault(2);
        }
        private void btn_day_Click(object sender, EventArgs e)
        {
            setDefault(3);
        }
        private void btn_week_Click(object sender, EventArgs e)
        {
            setDefault(4);
        }
    }
}
