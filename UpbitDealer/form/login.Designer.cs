
namespace UpbitDealer.form
{
    partial class login
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(login));
            this.text_sAPI_Key = new System.Windows.Forms.TextBox();
            this.text_sAPI_Secret = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.but_login = new System.Windows.Forms.Button();
            this.checkBox_remember = new System.Windows.Forms.CheckBox();
            this.SuspendLayout();
            // 
            // text_sAPI_Key
            // 
            this.text_sAPI_Key.BackColor = System.Drawing.Color.Black;
            this.text_sAPI_Key.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_sAPI_Key.ForeColor = System.Drawing.Color.White;
            this.text_sAPI_Key.Location = new System.Drawing.Point(42, 149);
            this.text_sAPI_Key.Name = "text_sAPI_Key";
            this.text_sAPI_Key.Size = new System.Drawing.Size(350, 29);
            this.text_sAPI_Key.TabIndex = 2;
            // 
            // text_sAPI_Secret
            // 
            this.text_sAPI_Secret.BackColor = System.Drawing.Color.Black;
            this.text_sAPI_Secret.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_sAPI_Secret.ForeColor = System.Drawing.Color.White;
            this.text_sAPI_Secret.Location = new System.Drawing.Point(42, 234);
            this.text_sAPI_Secret.Name = "text_sAPI_Secret";
            this.text_sAPI_Secret.PasswordChar = '*';
            this.text_sAPI_Secret.Size = new System.Drawing.Size(350, 29);
            this.text_sAPI_Secret.TabIndex = 3;
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.Black;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox4.Font = new System.Drawing.Font("Arial Black", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(57, 22);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(320, 53);
            this.textBox4.TabIndex = 4;
            this.textBox4.Text = "Upbit Dealer";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox4.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.Black;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox7.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(23, 111);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(143, 25);
            this.textBox7.TabIndex = 7;
            this.textBox7.Text = "Access Key";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox7.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.Black;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox8.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.ForeColor = System.Drawing.Color.White;
            this.textBox8.Location = new System.Drawing.Point(23, 192);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(143, 25);
            this.textBox8.TabIndex = 8;
            this.textBox8.Text = "Secret Key";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox8.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // but_login
            // 
            this.but_login.BackColor = System.Drawing.Color.DarkGray;
            this.but_login.Font = new System.Drawing.Font("Arial Black", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.but_login.ForeColor = System.Drawing.Color.Black;
            this.but_login.Location = new System.Drawing.Point(167, 300);
            this.but_login.Name = "but_login";
            this.but_login.Size = new System.Drawing.Size(100, 36);
            this.but_login.TabIndex = 9;
            this.but_login.Text = "Login";
            this.but_login.UseVisualStyleBackColor = false;
            this.but_login.Click += new System.EventHandler(this.but_login_Click);
            // 
            // checkBox_remember
            // 
            this.checkBox_remember.AutoSize = true;
            this.checkBox_remember.CheckAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.checkBox_remember.Location = new System.Drawing.Point(303, 290);
            this.checkBox_remember.Name = "checkBox_remember";
            this.checkBox_remember.Size = new System.Drawing.Size(108, 16);
            this.checkBox_remember.TabIndex = 11;
            this.checkBox_remember.Text = "Remember me";
            this.checkBox_remember.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.checkBox_remember.UseVisualStyleBackColor = true;
            // 
            // login
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(435, 364);
            this.Controls.Add(this.text_sAPI_Secret);
            this.Controls.Add(this.text_sAPI_Key);
            this.Controls.Add(this.textBox7);
            this.Controls.Add(this.textBox8);
            this.Controls.Add(this.checkBox_remember);
            this.Controls.Add(this.but_login);
            this.Controls.Add(this.textBox4);
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximumSize = new System.Drawing.Size(451, 403);
            this.MinimumSize = new System.Drawing.Size(451, 403);
            this.Name = "login";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "login";
            this.Load += new System.EventHandler(this.login_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.TextBox text_sAPI_Key;
        private System.Windows.Forms.TextBox text_sAPI_Secret;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.Button but_login;
        private System.Windows.Forms.CheckBox checkBox_remember;
    }
}