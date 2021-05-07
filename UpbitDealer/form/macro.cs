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
        private MacroSettingData setting;


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
            lock (((Main)Owner).lock_macro)
                setting = ((Main)Owner).macro.setting;

            btn_pause.BackColor = setting.pauseBuy ? Color.Red : Color.DarkGray;

            text_top.Text = setting.top.ToString();
            text_yield.Text = setting.yield.ToString();
            text_krw.Text = setting.krw.ToString();
            text_time.Text = setting.time.ToString();
            text_limit.Text = setting.limit.ToString();
            text_lostCut.Text = setting.lostCut.ToString();

            check_week_bb.Checked = setting.week_bb;
            check_day_bb.Checked = setting.day_bb;
            check_hour4_bb.Checked = setting.hour4_bb;
            check_hour1_bb.Checked = setting.hour1_bb;
            check_min30_bb.Checked = setting.min30_bb;

            check_week_tl.Checked = setting.week_tl;
            check_day_tl.Checked = setting.day_tl;
            check_hour4_tl.Checked = setting.hour4_tl;
            check_hour1_tl.Checked = setting.hour1_tl;
            check_min30_tl.Checked = setting.min30_tl;
        }


        private void btn_save_Click(object sender, EventArgs e)
        {
            if (text_yield.Text == "" || text_krw.Text == "" || text_time.Text == "")
            {
                MessageBox.Show("Check Parameters.");
                return;
            }

            MacroSettingData setting = new MacroSettingData();

            setting.pauseBuy = this.setting.pauseBuy;

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
            if (!double.TryParse(text_lostCut.Text, out setting.lostCut))
            {
                if (text_lostCut.Text == "") setting.lostCut = 0;
                else
                {
                    MessageBox.Show("Lost Cut value is not number.");
                    return;
                }
            }

            setting.week_bb = check_week_bb.Checked;
            setting.day_bb = check_day_bb.Checked;
            setting.hour4_bb = check_hour4_bb.Checked;
            setting.hour1_bb = check_hour1_bb.Checked;
            setting.min30_bb = check_min30_bb.Checked;

            setting.week_tl = check_week_tl.Checked;
            setting.day_tl = check_day_tl.Checked;
            setting.hour4_tl = check_hour4_tl.Checked;
            setting.hour1_tl = check_hour1_tl.Checked;
            setting.min30_tl = check_min30_tl.Checked;

            if (setting.top < 0 || setting.yield < 0 || setting.krw < 0 ||
                setting.time < 0 || setting.limit < 0 || setting.lostCut < 0)
            {
                MessageBox.Show("Required setting values can't be negative.");
                return;
            }
            if (!(setting.week_bb || setting.day_bb || setting.hour4_bb || setting.hour1_bb || setting.min30_bb ||
                setting.week_tl || setting.day_tl || setting.hour4_tl || setting.hour1_tl || setting.min30_tl))
            {
                MessageBox.Show("At least one of optional setting values must be checked.");
                return;
            }

            lock (((Main)Owner).lock_macro)
                ((Main)Owner).macro.saveMacroSetting(setting);

            setDefaultSetting();
            MessageBox.Show("Save success.");
        }
        private void btn_cancel_Click(object sender, EventArgs e)
        {
            setDefaultSetting();
        }
        private void btn_pause_Click(object sender, EventArgs e)
        {
            setting.pauseBuy = setting.pauseBuy ? false : true;
            lock (((Main)Owner).lock_macro)
                ((Main)Owner).macro.saveMacroSetting(setting);
            setDefaultSetting();

            if (setting.pauseBuy)
                MessageBox.Show("Pause Macro.");
            else
                MessageBox.Show("Continue Macro.");
        }
    }
}
