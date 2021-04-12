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
        private Main ownerForm;

        private DataView btcBollinger;
        private DataView avgBollinger;
        private NameValue bbLowest;


        public Indicator(Main ownerForm)
        {
            InitializeComponent();
            this.ownerForm = ownerForm;
        }
        private void Indicator_Load(object sender, EventArgs e)
        {
            btn_min30.PerformClick();
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            groupBox1.Focus();
        }


        private void setDefaultText(int index = 0)
        {
            lock (ownerForm.lock_macro)
            {
                btcBollinger = new DataView(ownerForm.macro.indexBollinger[index].Tables[0]);
                avgBollinger = new DataView(ownerForm.macro.indexBollinger[index].Tables[1]);
                bbLowest = new NameValue(ownerForm.macro.bbLowest[index]);
            }

            setDefaultButton();
            chart1.Series["btc"].Points.DataBind(btcBollinger, "date", "value", "");
            chart1.Series["avg"].Points.DataBind(avgBollinger, "date", "value", "");
            chart1.Series["dev"].Points.DataBind(avgBollinger, "date", "dev", "");
            if (btcBollinger.Count > 0)
            {
                chart1.ChartAreas["ChartArea"].AxisX.Maximum = ((DateTime)btcBollinger[0][0]).ToOADate();
                chart1.ChartAreas["ChartArea"].AxisX.Minimum = ((DateTime)btcBollinger[btcBollinger.Count - 1][0]).ToOADate();
                text_btc.Text = ((double)btcBollinger[0]["value"]).ToString("0.##");
                text_avg.Text = ((double)avgBollinger[0]["value"]).ToString("0.##");
                text_dis.Text = ((double)avgBollinger[0]["dev"]).ToString("0.##");
                text_min_name.Text = bbLowest.coinName;
                text_min_value.Text = bbLowest.value.ToString("0.##");
            }
            switch (index)
            {
                case 0:
                    btn_min30.BackColor = Color.Red;
                    chart1.ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    chart1.ChartAreas["ChartArea"].AxisX.Interval = 3;
                    break;
                case 1:
                    btn_hour1.BackColor = Color.Red;
                    chart1.ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Hours;
                    chart1.ChartAreas["ChartArea"].AxisX.Interval = 6;
                    break;
                case 2:
                    btn_hour4.BackColor = Color.Red;
                    chart1.ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Days;
                    chart1.ChartAreas["ChartArea"].AxisX.Interval = 1;
                    break;
                case 3:
                    btn_day.BackColor = Color.Red;
                    chart1.ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Days;
                    chart1.ChartAreas["ChartArea"].AxisX.Interval = 6;
                    break;
                case 4:
                    btn_week.BackColor = Color.Red;
                    chart1.ChartAreas["ChartArea"].AxisX.IntervalType = DateTimeIntervalType.Weeks;
                    chart1.ChartAreas["ChartArea"].AxisX.Interval = 6;
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
            setDefaultText(0);
        }
        private void btn_hour1_Click(object sender, EventArgs e)
        {
            setDefaultText(1);
        }
        private void btn_hour4_Click(object sender, EventArgs e)
        {
            setDefaultText(2);
        }
        private void btn_day_Click(object sender, EventArgs e)
        {
            setDefaultText(3);
        }
        private void btn_week_Click(object sender, EventArgs e)
        {
            setDefaultText(4);
        }
    }
}
