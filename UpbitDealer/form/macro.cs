using UpbitDealer.src;
using System;
using System.Windows.Forms;
using System.Collections.Generic;

namespace UpbitDealer.form
{
    public partial class Macro : Form
    {
        public Macro()
        {
            InitializeComponent();
        }
        private void Macro_Load(object sender, EventArgs e)
        {
            setDefaultSetting();
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            groupBox1.Focus();
        }


        private void setDefaultSetting()
        {
            MacroSettingData setting;
            List<BollingerAverage> ba0;
            List<BollingerAverage> ba1;
            List<BollingerAverage> ba2;
            List<BollingerAverage> bb0;
            List<BollingerAverage> bb1;
            List<BollingerAverage> bb2;

            lock (((Main)Owner).lock_macro)
            {
                setting = ((Main)Owner).macro.setting;
                ba0 = ((Main)Owner).macro.ba0;
                ba1 = ((Main)Owner).macro.ba1;
                ba2 = ((Main)Owner).macro.ba2;
                bb0 = ((Main)Owner).macro.bb0;
                bb1 = ((Main)Owner).macro.bb1;
                bb2 = ((Main)Owner).macro.bb2;
            }

            text_ba_min30_0.Text = ba0[0].avg.ToString("0.##");
            text_ba_min30_1.Text = ba1[0].avg.ToString("0.##");
            text_ba_min30_2.Text = ba2[0].avg.ToString("0.##");
            text_ba_hour1_0.Text = ba0[1].avg.ToString("0.##");
            text_ba_hour1_1.Text = ba1[1].avg.ToString("0.##");
            text_ba_hour1_2.Text = ba2[1].avg.ToString("0.##");
            text_ba_hour4_0.Text = ba0[2].avg.ToString("0.##");
            text_ba_hour4_1.Text = ba1[2].avg.ToString("0.##");
            text_ba_hour4_2.Text = ba2[2].avg.ToString("0.##");
            text_ba_day_0.Text = ba0[3].avg.ToString("0.##");
            text_ba_day_1.Text = ba1[3].avg.ToString("0.##");
            text_ba_day_2.Text = ba2[3].avg.ToString("0.##");
            text_ba_week_0.Text = ba0[4].avg.ToString("0.##");
            text_ba_week_1.Text = ba1[4].avg.ToString("0.##");
            text_ba_week_2.Text = ba2[4].avg.ToString("0.##");

            text_bb_min30_0.Text = bb0[0].avg.ToString("0.##");
            text_bb_min30_1.Text = bb1[0].avg.ToString("0.##");
            text_bb_min30_2.Text = bb2[0].avg.ToString("0.##");
            text_bb_hour1_0.Text = bb0[1].avg.ToString("0.##");
            text_bb_hour1_1.Text = bb1[1].avg.ToString("0.##");
            text_bb_hour1_2.Text = bb2[1].avg.ToString("0.##");
            text_bb_hour4_0.Text = bb0[2].avg.ToString("0.##");
            text_bb_hour4_1.Text = bb1[2].avg.ToString("0.##");
            text_bb_hour4_2.Text = bb2[2].avg.ToString("0.##");
            text_bb_day_0.Text = bb0[3].avg.ToString("0.##");
            text_bb_day_1.Text = bb1[3].avg.ToString("0.##");
            text_bb_day_2.Text = bb2[3].avg.ToString("0.##");
            text_bb_week_0.Text = bb0[4].avg.ToString("0.##");
            text_bb_week_1.Text = bb1[4].avg.ToString("0.##");
            text_bb_week_2.Text = bb2[4].avg.ToString("0.##");

            text_yield.Text = setting.yield.ToString();
            text_krw.Text = setting.krw.ToString();
            text_time.Text = setting.time.ToString();
            if (setting.week_from > -90000d) text_week_from.Text = setting.week_from.ToString();
            if (setting.week_to > -90000d) text_week_to.Text = setting.week_to.ToString();
            if (setting.day_from > -90000d) text_day_from.Text = setting.day_from.ToString();
            if (setting.day_to > -90000d) text_day_to.Text = setting.day_to.ToString();
            if (setting.hour4_from > -90000d) text_hour4_from.Text = setting.hour4_from.ToString();
            if (setting.hour4_to > -90000d) text_hour4_to.Text = setting.hour4_to.ToString();
            if (setting.hour1_from > -90000d) text_hour1_from.Text = setting.hour1_from.ToString();
            if (setting.hour1_to > -90000d) text_hour1_to.Text = setting.hour1_to.ToString();
            if (setting.min30_from > -90000d) text_min30_from.Text = setting.min30_from.ToString();
            if (setting.min30_to > -90000d) text_min30_to.Text = setting.min30_to.ToString();
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            if (text_yield.Text != "" && text_krw.Text != "" && text_time.Text != "")
            {
                MacroSettingData setting = new MacroSettingData();

                if (!double.TryParse(text_yield.Text, out setting.yield)
                    || !double.TryParse(text_krw.Text, out setting.krw)
                    || !double.TryParse(text_time.Text, out setting.time))
                {
                    MessageBox.Show("One of yield, krw and time value is not number.");
                    return;
                }

                if (text_week_from.Text == "" && text_day_from.Text == "" && text_hour4_from.Text == ""
                    && text_hour1_from.Text == "" && text_min30_from.Text == "")
                {
                    MessageBox.Show("At least one of 'from rate' parameter need.");
                    return;
                }

                if (text_week_from.Text != "")
                {
                    if (!double.TryParse(text_week_from.Text, out setting.week_from))
                    {
                        MessageBox.Show("Week rate is not number.");
                        return;
                    }
                    if (setting.week_from < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.week_from = -100000;
                if (text_week_to.Text != "")
                {
                    if (!double.TryParse(text_week_to.Text, out setting.week_to))
                    {
                        MessageBox.Show("Week rate is not number.");
                        return;
                    }
                    if (setting.week_to < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.week_to = -100000;

                if (text_day_from.Text != "")
                {
                    if (!double.TryParse(text_day_from.Text, out setting.day_from))
                    {
                        MessageBox.Show("Day rate is not number.");
                        return;
                    }
                    if (setting.day_from < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.day_from = -100000;
                if (text_day_to.Text != "")
                {
                    if (!double.TryParse(text_day_to.Text, out setting.day_to))
                    {
                        MessageBox.Show("Day rate is not number.");
                        return;
                    }
                    if (setting.day_to < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.day_to = -100000;

                if (text_hour4_from.Text != "")
                {
                    if (!double.TryParse(text_hour4_from.Text, out setting.hour4_from))
                    {
                        MessageBox.Show("4 hour rate is not number.");
                        return;
                    }
                    if (setting.hour4_from < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.hour4_from = -100000;
                if (text_hour4_to.Text != "")
                {
                    if (!double.TryParse(text_hour4_to.Text, out setting.hour4_to))
                    {
                        MessageBox.Show("4 hour rate is not number.");
                        return;
                    }
                    if (setting.hour4_to < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.hour4_to = -100000;

                if (text_hour1_from.Text != "")
                {
                    if (!double.TryParse(text_hour1_from.Text, out setting.hour1_from))
                    {
                        MessageBox.Show("1 hour rate is not number.");
                        return;
                    }
                    if (setting.hour1_from < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.hour1_from = -100000;
                if (text_hour1_to.Text != "")
                {
                    if (!double.TryParse(text_hour1_to.Text, out setting.hour1_to))
                    {
                        MessageBox.Show("1 hour rate is not number.");
                        return;
                    }
                    if (setting.hour1_to < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.hour1_to = -100000;

                if (text_min30_from.Text != "")
                {
                    if (!double.TryParse(text_min30_from.Text, out setting.min30_from))
                    {
                        MessageBox.Show("'from' 30 minute rate is not number.");
                        return;
                    }
                    if (setting.min30_from < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.min30_from = -100000;
                if (text_min30_to.Text != "")
                {
                    if (!double.TryParse(text_min30_to.Text, out setting.min30_to))
                    {
                        MessageBox.Show("'to' 30 minute rate is not number.");
                        return;
                    }
                    if (setting.min30_to < -10000)
                    {
                        MessageBox.Show("Rate value must be at least -10000.");
                        return;
                    }
                }
                else setting.min30_to = -100000;

                setting.week_bias = check_week.Checked;
                setting.day_bias = check_day.Checked;
                setting.hour4_bias = check_hour4.Checked;
                setting.hour1_bias = check_hour1.Checked;
                setting.min30_bias = check_min30.Checked;

                lock (((Main)Owner).lock_macro)
                    ((Main)Owner).macro.saveMacroSetting(setting);

                setDefaultSetting();
                MessageBox.Show("Save success.");
            }
            else
                MessageBox.Show("Check Parameters.");
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            setDefaultSetting();
        }
    }
}
