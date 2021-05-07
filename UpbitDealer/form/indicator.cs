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
        private DataView[] bb_data = new DataView[5];
        private NameValue[,] bb_maxMin = new NameValue[5, 2];
        private DataView[] tl_data = new DataView[5];
        private NameValue[,] tl_maxMin = new NameValue[5, 2];


        public Indicator()
        {
            InitializeComponent();
        }
        private void Indicator_Load(object sender, EventArgs e)
        {
            List<string> hotList;
            List<string> dangerList;
            DataSet[] bollinger = new DataSet[5];
            DataSet[] trendLine = new DataSet[5];

            lock (((Main)Owner).lock_mainUpdater)
            {
                hotList = new List<string>(((Main)Owner).mainUpdater.hotList);
                dangerList = new List<string>(((Main)Owner).mainUpdater.dangerList);
            }
            lock (((Main)Owner).lock_macro)
                for (int i = 0; i < 5; i++)
                {
                    bollinger[i] = ((Main)Owner).macro.bollinger[i].Copy();
                    trendLine[i] = ((Main)Owner).macro.trendLine[i].Copy();
                }

            if (bollinger[0].Tables["BTC"].Rows.Count < 1 ||
                bollinger[0].Tables["ETH"].Rows.Count < 1 ||
                bollinger[0].Tables["XRP"].Rows.Count < 1)
            {
                MessageBox.Show(
                    "Macro does not finish to load candle data.\n" +
                    "Open this form, after macro finish to load.");
                Close();
                return;
            }

            initHotDanger(hotList, dangerList);
            initBBData(bollinger);
            initTLData(trendLine);

            setDefault();
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            groupBox1.Focus();
        }


        private void initHotDanger(List<string> hotList, List<string> dangerList)
        {
            for (int i = 0; i < hotList.Count; i++) list_hotList.Items.Add(hotList[i]);
            for (int i = 0; i < dangerList.Count; i++) list_dangerList.Items.Add(dangerList[i]);
        }
        private void initBBData(DataSet[] bollinger)
        {
            DataTable[] dataTable = new DataTable[5];
            for (int i = 0; i < 5; i++)
            {
                dataTable[i] = new DataTable();
                dataTable[i].Columns.Add("date", typeof(DateTime));
                dataTable[i].Columns.Add("top3", typeof(double));
                dataTable[i].Columns.Add("avg", typeof(double));

                bb_maxMin[i, 0] = new NameValue("", 0);
                bb_maxMin[i, 1] = new NameValue("", 0);

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

                bb_maxMin[i, 0].coinName = bollinger[i].Tables[maxIndex].TableName;
                bb_maxMin[i, 0].value = max;
                bb_maxMin[i, 1].coinName = bollinger[i].Tables[minIndex].TableName;
                bb_maxMin[i, 1].value = min;

                for (int j = 0; j < bollinger[i].Tables["BTC"].Rows.Count; j++)
                {
                    DateTime dateTime = (DateTime)bollinger[i].Tables["BTC"].Rows[j]["date"];

                    double top3
                        = ((double)bollinger[i].Tables["BTC"].Rows[j]["value"]
                        + (double)bollinger[i].Tables["ETH"].Rows[j]["value"]
                        + (double)bollinger[i].Tables["XRP"].Rows[j]["value"]) / 3d;
                    double avg = 0;
                    double count = 0;

                    for (int k = 0; k < bollinger[i].Tables.Count; k++)
                    {
                        if (bollinger[i].Tables[k].Rows.Count > j)
                        {
                            double temp = (double)bollinger[i].Tables[k].Rows[j]["value"];

                            avg += temp;
                            count += 1;
                        }
                    }

                    dataTable[i].Rows.Add(dateTime, top3, avg / count);
                }

                bb_data[i] = new DataView(dataTable[i]);
            }
        }
        private void initTLData(DataSet[] trendLine)
        {
            DataTable[] dataTable = new DataTable[5];
            for (int i = 0; i < 5; i++)
            {
                dataTable[i] = new DataTable();
                dataTable[i].Columns.Add("date", typeof(DateTime));
                dataTable[i].Columns.Add("top3", typeof(double));
                dataTable[i].Columns.Add("avg", typeof(double));

                tl_maxMin[i, 0] = new NameValue("", 0);
                tl_maxMin[i, 1] = new NameValue("", 0);

                int maxIndex = -1;
                int minIndex = -1;
                double max = double.MinValue;
                double min = double.MaxValue;
                for (int j = 0; j < trendLine[i].Tables.Count; j++)
                {
                    if (trendLine[i].Tables[j].Rows.Count > 0)
                    {
                        double temp = (double)trendLine[i].Tables[j].Rows[0]["value"];
                        if (max < temp) { maxIndex = j; max = temp; }
                        if (min > temp) { minIndex = j; min = temp; }
                    }
                }

                tl_maxMin[i, 0].coinName = trendLine[i].Tables[maxIndex].TableName;
                tl_maxMin[i, 0].value = max;
                tl_maxMin[i, 1].coinName = trendLine[i].Tables[minIndex].TableName;
                tl_maxMin[i, 1].value = min;

                for (int j = 0; j < trendLine[i].Tables["BTC"].Rows.Count; j++)
                {
                    DateTime dateTime = (DateTime)trendLine[i].Tables["BTC"].Rows[j]["date"];

                    double top3
                        = ((double)trendLine[i].Tables["BTC"].Rows[j]["value"]
                        + (double)trendLine[i].Tables["ETH"].Rows[j]["value"]
                        + (double)trendLine[i].Tables["XRP"].Rows[j]["value"]) / 3d;
                    double avg = 0;
                    double count = 0;

                    for (int k = 0; k < trendLine[i].Tables.Count; k++)
                    {
                        if (trendLine[i].Tables[k].Rows.Count > j)
                        {
                            double temp = (double)trendLine[i].Tables[k].Rows[j]["value"];

                            avg += temp;
                            count += 1;
                        }
                    }

                    dataTable[i].Rows.Add(dateTime, top3, avg / count);
                }

                tl_data[i] = new DataView(dataTable[i]);
            }
        }


        private void setDefault(int index = 0)
        {
            setDefaultButton();

            chart1.Series["bb_top3"].Points.DataBind(bb_data[index], "date", "top3", "");
            chart1.Series["bb_avg"].Points.DataBind(bb_data[index], "date", "avg", "");

            bb_top3.Text = ((double)bb_data[index][0]["top3"]).ToString("0.##");
            bb_avg.Text = ((double)bb_data[index][0]["avg"]).ToString("0.##");

            bb_max_name.Text = bb_maxMin[index, 0].coinName;
            bb_max_value.Text = bb_maxMin[index, 0].value.ToString("0.##");
            bb_min_name.Text = bb_maxMin[index, 1].coinName;
            bb_min_value.Text = bb_maxMin[index, 1].value.ToString("0.##");


            chart2.Series["tl_top3"].Points.DataBind(tl_data[index], "date", "top3", "");
            chart2.Series["tl_avg"].Points.DataBind(tl_data[index], "date", "avg", "");

            tl_top3.Text = ((double)tl_data[index][0]["top3"]).ToString("0.##");
            tl_avg.Text = ((double)tl_data[index][0]["avg"]).ToString("0.##");
            
            tl_max_name.Text = tl_maxMin[index, 0].coinName;
            tl_max_value.Text = tl_maxMin[index, 0].value.ToString("0.##");
            tl_min_name.Text = tl_maxMin[index, 1].coinName;
            tl_min_value.Text = tl_maxMin[index, 1].value.ToString("0.##");


            Chart[] chartPtr = {
                chart1,
                chart2
            };
            DateTime[] xMax = {
                (DateTime)bb_data[index][0][0],
                (DateTime)tl_data[index][0][0]
            };
            DateTime[] xMin = {
                (DateTime)bb_data[index][bb_data[index].Count - 1][0],
                (DateTime)tl_data[index][tl_data[index].Count - 1][0]
            };
            switch (index)
            {
                case 0:
                    btn_min30.BackColor = Color.Red;
                    for (int i = 0; i < 2; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax[i].AddMinutes(30).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin[i].ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Hours;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 3;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.LabelStyle.Format = "HH";
                    }
                    break;
                case 1:
                    btn_hour1.BackColor = Color.Red;
                    for (int i = 0; i < 2; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax[i].AddHours(1).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin[i].ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Hours;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 6;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.LabelStyle.Format = "dd/HH";
                    }
                    break;
                case 2:
                    btn_hour4.BackColor = Color.Red;
                    for (int i = 0; i < 2; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax[i].AddHours(4).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin[i].ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Days;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 1;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.LabelStyle.Format = "dd";
                    }
                    break;
                case 3:
                    btn_day.BackColor = Color.Red;
                    for (int i = 0; i < 2; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax[i].AddDays(1).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin[i].ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Days;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 6;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.LabelStyle.Format = "MM-dd";
                    }
                    break;
                case 4:
                    btn_week.BackColor = Color.Red;
                    for (int i = 0; i < 2; i++)
                    {
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Maximum = xMax[i].AddDays(7).ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Minimum = xMin[i].ToOADate();
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.Interval = 6;
                        chartPtr[i].ChartAreas["ChartArea"].AxisX.LabelStyle.Format = "MM-dd";
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
