using UpbitDealer.src;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace UpbitDealer.form
{
    public partial class login : Form
    {
        string path = System.IO.Directory.GetCurrentDirectory() + "/login.dat";
        public bool isGood = false;
        public string access_key;
        public string secret_key;


        public login()
        {
            InitializeComponent();
        }
        private void login_Load(object sender, EventArgs e)
        {
            try
            {
                if (System.IO.File.Exists(path))
                {
                    checkBox_remember.Checked = true;
                    string[] keyValue = System.IO.File.ReadAllLines(path);
                    if (keyValue.Length > 1)
                    {
                        text_sAPI_Key.Text = keyValue[0];
                        text_sAPI_Secret.Text = keyValue[1];
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            text_sAPI_Key.Focus();
        }

        private void but_login_Click(object sender, EventArgs e)
        {
            if (!checkBox_remember.Checked)
            {
                try
                {
                    if (System.IO.File.Exists(path))
                        System.IO.File.Delete(path);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            access_key = text_sAPI_Key.Text;
            secret_key = text_sAPI_Secret.Text;
            ApiData apiData = new ApiData(access_key, secret_key);
            int ret = apiData.checkApiKey();
            if (ret < 0)
            {
                MessageBox.Show("Invalid API key, check keys");
                return;
            }

            if (checkBox_remember.Checked)
            {
                try
                {
                    System.IO.File.WriteAllText(path, "");
                    System.IO.File.AppendAllText(path, text_sAPI_Key.Text + Environment.NewLine);
                    System.IO.File.AppendAllText(path, text_sAPI_Secret.Text + Environment.NewLine);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }

            isGood = true;
            Close();
        }
    }
}
