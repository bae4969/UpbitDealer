﻿
namespace UpbitDealer.form
{
    partial class Macro
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Macro));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.text_lostCut = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.text_top = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.text_limit = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.text_time = new System.Windows.Forms.TextBox();
            this.text_krw = new System.Windows.Forms.TextBox();
            this.text_yield = new System.Windows.Forms.TextBox();
            this.text_parName2 = new System.Windows.Forms.TextBox();
            this.text_parName1 = new System.Windows.Forms.TextBox();
            this.text_parName0 = new System.Windows.Forms.TextBox();
            this.textBox9 = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.text_parName3 = new System.Windows.Forms.TextBox();
            this.btn_save = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.btn_pause = new System.Windows.Forms.Button();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.check_hour1_bb = new System.Windows.Forms.CheckBox();
            this.check_week_bb = new System.Windows.Forms.CheckBox();
            this.check_day_bb = new System.Windows.Forms.CheckBox();
            this.check_hour4_bb = new System.Windows.Forms.CheckBox();
            this.check_min30_bb = new System.Windows.Forms.CheckBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.check_hour1_tl = new System.Windows.Forms.CheckBox();
            this.check_week_tl = new System.Windows.Forms.CheckBox();
            this.check_day_tl = new System.Windows.Forms.CheckBox();
            this.check_hour4_tl = new System.Windows.Forms.CheckBox();
            this.check_min30_tl = new System.Windows.Forms.CheckBox();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox4.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.text_lostCut);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.text_top);
            this.groupBox1.Controls.Add(this.textBox10);
            this.groupBox1.Controls.Add(this.text_limit);
            this.groupBox1.Controls.Add(this.textBox6);
            this.groupBox1.Controls.Add(this.text_time);
            this.groupBox1.Controls.Add(this.text_krw);
            this.groupBox1.Controls.Add(this.text_yield);
            this.groupBox1.Controls.Add(this.text_parName2);
            this.groupBox1.Controls.Add(this.text_parName1);
            this.groupBox1.Controls.Add(this.text_parName0);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(402, 254);
            this.groupBox1.TabIndex = 0;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Required setting";
            // 
            // text_lostCut
            // 
            this.text_lostCut.BackColor = System.Drawing.Color.LightGray;
            this.text_lostCut.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_lostCut.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_lostCut.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_lostCut.ForeColor = System.Drawing.Color.Black;
            this.text_lostCut.Location = new System.Drawing.Point(140, 211);
            this.text_lostCut.Name = "text_lostCut";
            this.text_lostCut.Size = new System.Drawing.Size(256, 32);
            this.text_lostCut.TabIndex = 40;
            this.text_lostCut.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(6, 214);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(128, 22);
            this.textBox2.TabIndex = 41;
            this.textBox2.Text = "Lost Cut (%)";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_top
            // 
            this.text_top.BackColor = System.Drawing.Color.LightGray;
            this.text_top.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_top.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_top.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_top.ForeColor = System.Drawing.Color.Black;
            this.text_top.Location = new System.Drawing.Point(140, 21);
            this.text_top.Name = "text_top";
            this.text_top.Size = new System.Drawing.Size(256, 32);
            this.text_top.TabIndex = 39;
            this.text_top.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.Black;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox10.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox10.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.ForeColor = System.Drawing.Color.White;
            this.textBox10.Location = new System.Drawing.Point(6, 24);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(128, 22);
            this.textBox10.TabIndex = 38;
            this.textBox10.Text = "Top";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox10.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_limit
            // 
            this.text_limit.BackColor = System.Drawing.Color.LightGray;
            this.text_limit.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_limit.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_limit.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_limit.ForeColor = System.Drawing.Color.Black;
            this.text_limit.Location = new System.Drawing.Point(140, 173);
            this.text_limit.Name = "text_limit";
            this.text_limit.Size = new System.Drawing.Size(256, 32);
            this.text_limit.TabIndex = 12;
            this.text_limit.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.Black;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox6.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.White;
            this.textBox6.Location = new System.Drawing.Point(6, 176);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(128, 22);
            this.textBox6.TabIndex = 37;
            this.textBox6.Text = "KRW Limit";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox6.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_time
            // 
            this.text_time.BackColor = System.Drawing.Color.LightGray;
            this.text_time.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_time.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_time.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_time.ForeColor = System.Drawing.Color.Black;
            this.text_time.Location = new System.Drawing.Point(140, 135);
            this.text_time.Name = "text_time";
            this.text_time.Size = new System.Drawing.Size(256, 32);
            this.text_time.TabIndex = 11;
            this.text_time.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // text_krw
            // 
            this.text_krw.BackColor = System.Drawing.Color.LightGray;
            this.text_krw.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_krw.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_krw.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_krw.ForeColor = System.Drawing.Color.Black;
            this.text_krw.Location = new System.Drawing.Point(140, 97);
            this.text_krw.Name = "text_krw";
            this.text_krw.Size = new System.Drawing.Size(256, 32);
            this.text_krw.TabIndex = 10;
            this.text_krw.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // text_yield
            // 
            this.text_yield.BackColor = System.Drawing.Color.LightGray;
            this.text_yield.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_yield.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.text_yield.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_yield.ForeColor = System.Drawing.Color.Black;
            this.text_yield.Location = new System.Drawing.Point(140, 59);
            this.text_yield.Name = "text_yield";
            this.text_yield.Size = new System.Drawing.Size(256, 32);
            this.text_yield.TabIndex = 9;
            this.text_yield.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // text_parName2
            // 
            this.text_parName2.BackColor = System.Drawing.Color.Black;
            this.text_parName2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_parName2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_parName2.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_parName2.ForeColor = System.Drawing.Color.White;
            this.text_parName2.Location = new System.Drawing.Point(6, 138);
            this.text_parName2.Name = "text_parName2";
            this.text_parName2.ReadOnly = true;
            this.text_parName2.Size = new System.Drawing.Size(128, 22);
            this.text_parName2.TabIndex = 6;
            this.text_parName2.Text = "Times";
            this.text_parName2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_parName2.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_parName1
            // 
            this.text_parName1.BackColor = System.Drawing.Color.Black;
            this.text_parName1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_parName1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_parName1.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_parName1.ForeColor = System.Drawing.Color.White;
            this.text_parName1.Location = new System.Drawing.Point(6, 100);
            this.text_parName1.Name = "text_parName1";
            this.text_parName1.ReadOnly = true;
            this.text_parName1.Size = new System.Drawing.Size(128, 22);
            this.text_parName1.TabIndex = 5;
            this.text_parName1.Text = "KRW";
            this.text_parName1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_parName1.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_parName0
            // 
            this.text_parName0.BackColor = System.Drawing.Color.Black;
            this.text_parName0.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_parName0.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_parName0.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_parName0.ForeColor = System.Drawing.Color.White;
            this.text_parName0.Location = new System.Drawing.Point(6, 62);
            this.text_parName0.Name = "text_parName0";
            this.text_parName0.ReadOnly = true;
            this.text_parName0.Size = new System.Drawing.Size(128, 22);
            this.text_parName0.TabIndex = 4;
            this.text_parName0.Text = "Yield (%)";
            this.text_parName0.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_parName0.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox9
            // 
            this.textBox9.BackColor = System.Drawing.Color.Black;
            this.textBox9.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox9.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox9.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox9.ForeColor = System.Drawing.Color.White;
            this.textBox9.Location = new System.Drawing.Point(6, 191);
            this.textBox9.Name = "textBox9";
            this.textBox9.ReadOnly = true;
            this.textBox9.Size = new System.Drawing.Size(128, 22);
            this.textBox9.TabIndex = 20;
            this.textBox9.Text = "30 Min Rate";
            this.textBox9.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox9.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.Black;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox7.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(6, 153);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(128, 22);
            this.textBox7.TabIndex = 18;
            this.textBox7.Text = "1 Hour Rate";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox7.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.Black;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox5.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.White;
            this.textBox5.Location = new System.Drawing.Point(6, 115);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(128, 22);
            this.textBox5.TabIndex = 16;
            this.textBox5.Text = "4 Hour Rate";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox5.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Black;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.textBox3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.White;
            this.textBox3.Location = new System.Drawing.Point(6, 77);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(128, 22);
            this.textBox3.TabIndex = 14;
            this.textBox3.Text = "Day Rate";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox3.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_parName3
            // 
            this.text_parName3.BackColor = System.Drawing.Color.Black;
            this.text_parName3.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.text_parName3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_parName3.Font = new System.Drawing.Font("Arial", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_parName3.ForeColor = System.Drawing.Color.White;
            this.text_parName3.Location = new System.Drawing.Point(6, 39);
            this.text_parName3.Name = "text_parName3";
            this.text_parName3.ReadOnly = true;
            this.text_parName3.Size = new System.Drawing.Size(128, 22);
            this.text_parName3.TabIndex = 7;
            this.text_parName3.Text = "Week Rate";
            this.text_parName3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_parName3.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // btn_save
            // 
            this.btn_save.BackColor = System.Drawing.Color.DarkGray;
            this.btn_save.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.ForeColor = System.Drawing.Color.Black;
            this.btn_save.Location = new System.Drawing.Point(12, 508);
            this.btn_save.Name = "btn_save";
            this.btn_save.Size = new System.Drawing.Size(130, 34);
            this.btn_save.TabIndex = 19;
            this.btn_save.Text = "Save";
            this.btn_save.UseVisualStyleBackColor = false;
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.BackColor = System.Drawing.Color.DarkGray;
            this.btn_cancel.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.ForeColor = System.Drawing.Color.Black;
            this.btn_cancel.Location = new System.Drawing.Point(148, 508);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(130, 34);
            this.btn_cancel.TabIndex = 4;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // btn_pause
            // 
            this.btn_pause.BackColor = System.Drawing.Color.DarkGray;
            this.btn_pause.Font = new System.Drawing.Font("Arial", 15.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_pause.ForeColor = System.Drawing.Color.Black;
            this.btn_pause.Location = new System.Drawing.Point(284, 508);
            this.btn_pause.Name = "btn_pause";
            this.btn_pause.Size = new System.Drawing.Size(130, 34);
            this.btn_pause.TabIndex = 20;
            this.btn_pause.Text = "Pause Buy";
            this.btn_pause.UseVisualStyleBackColor = false;
            this.btn_pause.Click += new System.EventHandler(this.btn_pause_Click);
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.groupBox3);
            this.groupBox2.Controls.Add(this.groupBox4);
            this.groupBox2.Controls.Add(this.text_parName3);
            this.groupBox2.Controls.Add(this.textBox3);
            this.groupBox2.Controls.Add(this.textBox5);
            this.groupBox2.Controls.Add(this.textBox7);
            this.groupBox2.Controls.Add(this.textBox9);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(12, 272);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(402, 230);
            this.groupBox2.TabIndex = 41;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Optional setting";
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.check_hour1_bb);
            this.groupBox4.Controls.Add(this.check_week_bb);
            this.groupBox4.Controls.Add(this.check_day_bb);
            this.groupBox4.Controls.Add(this.check_hour4_bb);
            this.groupBox4.Controls.Add(this.check_min30_bb);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(140, 21);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(49, 203);
            this.groupBox4.TabIndex = 43;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "BB";
            // 
            // check_hour1_bb
            // 
            this.check_hour1_bb.AutoSize = true;
            this.check_hour1_bb.Location = new System.Drawing.Point(17, 136);
            this.check_hour1_bb.Name = "check_hour1_bb";
            this.check_hour1_bb.Size = new System.Drawing.Size(15, 14);
            this.check_hour1_bb.TabIndex = 34;
            this.check_hour1_bb.UseVisualStyleBackColor = true;
            // 
            // check_week_bb
            // 
            this.check_week_bb.AutoSize = true;
            this.check_week_bb.Location = new System.Drawing.Point(17, 22);
            this.check_week_bb.Name = "check_week_bb";
            this.check_week_bb.Size = new System.Drawing.Size(15, 14);
            this.check_week_bb.TabIndex = 31;
            this.check_week_bb.UseVisualStyleBackColor = true;
            // 
            // check_day_bb
            // 
            this.check_day_bb.AutoSize = true;
            this.check_day_bb.Location = new System.Drawing.Point(17, 60);
            this.check_day_bb.Name = "check_day_bb";
            this.check_day_bb.Size = new System.Drawing.Size(15, 14);
            this.check_day_bb.TabIndex = 32;
            this.check_day_bb.UseVisualStyleBackColor = true;
            // 
            // check_hour4_bb
            // 
            this.check_hour4_bb.AutoSize = true;
            this.check_hour4_bb.Location = new System.Drawing.Point(17, 98);
            this.check_hour4_bb.Name = "check_hour4_bb";
            this.check_hour4_bb.Size = new System.Drawing.Size(15, 14);
            this.check_hour4_bb.TabIndex = 33;
            this.check_hour4_bb.UseVisualStyleBackColor = true;
            // 
            // check_min30_bb
            // 
            this.check_min30_bb.AutoSize = true;
            this.check_min30_bb.Location = new System.Drawing.Point(17, 174);
            this.check_min30_bb.Name = "check_min30_bb";
            this.check_min30_bb.Size = new System.Drawing.Size(15, 14);
            this.check_min30_bb.TabIndex = 35;
            this.check_min30_bb.UseVisualStyleBackColor = true;
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.check_hour1_tl);
            this.groupBox3.Controls.Add(this.check_week_tl);
            this.groupBox3.Controls.Add(this.check_day_tl);
            this.groupBox3.Controls.Add(this.check_hour4_tl);
            this.groupBox3.Controls.Add(this.check_min30_tl);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(195, 21);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(49, 203);
            this.groupBox3.TabIndex = 44;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "TL";
            // 
            // check_hour1_tl
            // 
            this.check_hour1_tl.AutoSize = true;
            this.check_hour1_tl.Location = new System.Drawing.Point(17, 136);
            this.check_hour1_tl.Name = "check_hour1_tl";
            this.check_hour1_tl.Size = new System.Drawing.Size(15, 14);
            this.check_hour1_tl.TabIndex = 34;
            this.check_hour1_tl.UseVisualStyleBackColor = true;
            // 
            // check_week_tl
            // 
            this.check_week_tl.AutoSize = true;
            this.check_week_tl.Location = new System.Drawing.Point(17, 22);
            this.check_week_tl.Name = "check_week_tl";
            this.check_week_tl.Size = new System.Drawing.Size(15, 14);
            this.check_week_tl.TabIndex = 31;
            this.check_week_tl.UseVisualStyleBackColor = true;
            // 
            // check_day_tl
            // 
            this.check_day_tl.AutoSize = true;
            this.check_day_tl.Location = new System.Drawing.Point(17, 60);
            this.check_day_tl.Name = "check_day_tl";
            this.check_day_tl.Size = new System.Drawing.Size(15, 14);
            this.check_day_tl.TabIndex = 32;
            this.check_day_tl.UseVisualStyleBackColor = true;
            // 
            // check_hour4_tl
            // 
            this.check_hour4_tl.AutoSize = true;
            this.check_hour4_tl.Location = new System.Drawing.Point(17, 98);
            this.check_hour4_tl.Name = "check_hour4_tl";
            this.check_hour4_tl.Size = new System.Drawing.Size(15, 14);
            this.check_hour4_tl.TabIndex = 33;
            this.check_hour4_tl.UseVisualStyleBackColor = true;
            // 
            // check_min30_tl
            // 
            this.check_min30_tl.AutoSize = true;
            this.check_min30_tl.Location = new System.Drawing.Point(17, 174);
            this.check_min30_tl.Name = "check_min30_tl";
            this.check_min30_tl.Size = new System.Drawing.Size(15, 14);
            this.check_min30_tl.TabIndex = 35;
            this.check_min30_tl.UseVisualStyleBackColor = true;
            // 
            // Macro
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(426, 554);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.btn_pause);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "Macro";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Macro";
            this.Load += new System.EventHandler(this.Macro_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_save;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox text_parName3;
        private System.Windows.Forms.TextBox text_parName2;
        private System.Windows.Forms.TextBox text_parName1;
        private System.Windows.Forms.TextBox text_parName0;
        private System.Windows.Forms.TextBox text_yield;
        private System.Windows.Forms.TextBox text_time;
        private System.Windows.Forms.TextBox text_krw;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox9;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.TextBox text_limit;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.Button btn_pause;
        private System.Windows.Forms.TextBox text_top;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.CheckBox check_hour1_bb;
        private System.Windows.Forms.CheckBox check_week_bb;
        private System.Windows.Forms.CheckBox check_day_bb;
        private System.Windows.Forms.CheckBox check_hour4_bb;
        private System.Windows.Forms.CheckBox check_min30_bb;
        private System.Windows.Forms.TextBox text_lostCut;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.CheckBox check_hour1_tl;
        private System.Windows.Forms.CheckBox check_week_tl;
        private System.Windows.Forms.CheckBox check_day_tl;
        private System.Windows.Forms.CheckBox check_hour4_tl;
        private System.Windows.Forms.CheckBox check_min30_tl;
    }
}