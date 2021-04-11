using UpbitDealer.src;
using System;
using System.Windows.Forms;
using System.Collections.Generic;
using System.Data;
using System.Windows.Forms.DataVisualization.Charting;
using System.Drawing;

namespace UpbitDealer.form
{
    public partial class Macro : Form
    {
        private Main ownerForm;

        private MacroSettingData setting;


        public Macro(Main ownerForm)
        {
            InitializeComponent();
            this.ownerForm = ownerForm;
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
            lock (ownerForm.lock_macro)
                setting = ownerForm.macro.setting;

            btn_pause.BackColor = setting.pause ? Color.Red : Color.DarkGray;

            text_top.Text = setting.top.ToString();
            text_yield.Text = setting.yield.ToString();
            text_krw.Text = setting.krw.ToString();
            text_time.Text = setting.time.ToString();
            text_limit.Text = setting.limit.ToString();
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

            check_week.Checked = setting.week_bias;
            check_day.Checked = setting.day_bias;
            check_hour4.Checked = setting.hour4_bias;
            check_hour1.Checked = setting.hour1_bias;
            check_min30.Checked = setting.min30_bias;
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            if (text_yield.Text != "" && text_krw.Text != "" && text_time.Text != "")
            {
                MacroSettingData setting = new MacroSettingData();

                setting.pause = this.setting.pause;

                if (!int.TryParse(text_top.Text, out setting.top))
                {
                    if (text_top.Text == "") setting.top = 70;
                    else
                    {
                        MessageBox.Show("Top value is not number.");
                        return;
                    }
                }
                if (!double.TryParse(text_yield.Text, out setting.yield))
                {
                    MessageBox.Show("Yield value is not number.");
                    return;
                }
                if (!double.TryParse(text_krw.Text, out setting.krw))
                {
                    MessageBox.Show("KRW value is not number.");
                    return;
                }
                if (!double.TryParse(text_time.Text, out setting.time))
                {
                    MessageBox.Show("Time value is not number.");
                    return;
                }
                if (!double.TryParse(text_limit.Text, out setting.limit))
                {
                    if (text_limit.Text == "") setting.limit = 0;
                    else
                    {
                        MessageBox.Show("Limit value is not number.");
                        return;
                    }
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

                lock (ownerForm.lock_macro)
                    ownerForm.macro.saveMacroSetting(setting);

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
        private void btn_pause_Click(object sender, EventArgs e)
        {
            setting.pause = setting.pause ? false : true;
            lock (ownerForm.lock_macro)
                ownerForm.macro.saveMacroSetting(setting);
            setDefaultSetting();

            if(setting.pause)
                MessageBox.Show("Pause Macro.");
            else
                MessageBox.Show("Continue Macro.");
        }
    }
}
