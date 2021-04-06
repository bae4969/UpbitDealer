using System;
using System.Windows.Forms;

namespace UpbitDealer.form
{
    public partial class savingMsg : Form
    {
        public savingMsg()
        {
            InitializeComponent();
        }
        private void text_focus_disable(object sender, EventArgs e)
        {
            textBox3.Focus();
        }
    }
}
