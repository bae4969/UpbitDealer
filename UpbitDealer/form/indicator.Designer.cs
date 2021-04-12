
namespace UpbitDealer.form
{
    partial class Indicator
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Indicator));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.text_min_value = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.text_min_name = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.text_dis = new System.Windows.Forms.TextBox();
            this.text_avg = new System.Windows.Forms.TextBox();
            this.text_btc = new System.Windows.Forms.TextBox();
            this.textBox3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_week = new System.Windows.Forms.Button();
            this.btn_day = new System.Windows.Forms.Button();
            this.btn_hour4 = new System.Windows.Forms.Button();
            this.btn_hour1 = new System.Windows.Forms.Button();
            this.btn_min30 = new System.Windows.Forms.Button();
            this.btn_min10 = new System.Windows.Forms.Button();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.text_min_value);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.text_min_name);
            this.groupBox1.Controls.Add(this.textBox8);
            this.groupBox1.Controls.Add(this.text_dis);
            this.groupBox1.Controls.Add(this.text_avg);
            this.groupBox1.Controls.Add(this.text_btc);
            this.groupBox1.Controls.Add(this.textBox3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.chart1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 12);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(514, 534);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Avg Bollinger Value";
            // 
            // text_min_value
            // 
            this.text_min_value.BackColor = System.Drawing.Color.Black;
            this.text_min_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_min_value.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_min_value.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_min_value.ForeColor = System.Drawing.Color.White;
            this.text_min_value.Location = new System.Drawing.Point(404, 498);
            this.text_min_value.Name = "text_min_value";
            this.text_min_value.ReadOnly = true;
            this.text_min_value.Size = new System.Drawing.Size(100, 26);
            this.text_min_value.TabIndex = 32;
            this.text_min_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.Black;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.White;
            this.textBox5.Location = new System.Drawing.Point(404, 473);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(100, 26);
            this.textBox5.TabIndex = 31;
            this.textBox5.Text = "Min Value";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // text_min_name
            // 
            this.text_min_name.BackColor = System.Drawing.Color.Black;
            this.text_min_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_min_name.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_min_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_min_name.ForeColor = System.Drawing.Color.White;
            this.text_min_name.Location = new System.Drawing.Point(305, 498);
            this.text_min_name.Name = "text_min_name";
            this.text_min_name.ReadOnly = true;
            this.text_min_name.Size = new System.Drawing.Size(100, 26);
            this.text_min_name.TabIndex = 30;
            this.text_min_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_min_name.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.Black;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.ForeColor = System.Drawing.Color.White;
            this.textBox8.Location = new System.Drawing.Point(305, 473);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(100, 26);
            this.textBox8.TabIndex = 29;
            this.textBox8.Text = "Min Name";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox8.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_dis
            // 
            this.text_dis.BackColor = System.Drawing.Color.Black;
            this.text_dis.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_dis.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_dis.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_dis.ForeColor = System.Drawing.Color.White;
            this.text_dis.Location = new System.Drawing.Point(206, 498);
            this.text_dis.Name = "text_dis";
            this.text_dis.ReadOnly = true;
            this.text_dis.Size = new System.Drawing.Size(100, 26);
            this.text_dis.TabIndex = 28;
            this.text_dis.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_dis.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_avg
            // 
            this.text_avg.BackColor = System.Drawing.Color.Black;
            this.text_avg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_avg.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_avg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_avg.ForeColor = System.Drawing.Color.White;
            this.text_avg.Location = new System.Drawing.Point(107, 498);
            this.text_avg.Name = "text_avg";
            this.text_avg.ReadOnly = true;
            this.text_avg.Size = new System.Drawing.Size(100, 26);
            this.text_avg.TabIndex = 27;
            this.text_avg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_avg.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // text_btc
            // 
            this.text_btc.BackColor = System.Drawing.Color.Black;
            this.text_btc.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.text_btc.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_btc.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_btc.ForeColor = System.Drawing.Color.White;
            this.text_btc.Location = new System.Drawing.Point(8, 498);
            this.text_btc.Name = "text_btc";
            this.text_btc.ReadOnly = true;
            this.text_btc.Size = new System.Drawing.Size(100, 26);
            this.text_btc.TabIndex = 26;
            this.text_btc.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_btc.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox3
            // 
            this.textBox3.BackColor = System.Drawing.Color.Black;
            this.textBox3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox3.ForeColor = System.Drawing.Color.White;
            this.textBox3.Location = new System.Drawing.Point(206, 473);
            this.textBox3.Name = "textBox3";
            this.textBox3.ReadOnly = true;
            this.textBox3.Size = new System.Drawing.Size(100, 26);
            this.textBox3.TabIndex = 25;
            this.textBox3.Text = "Deviation";
            this.textBox3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox3.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(107, 473);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(100, 26);
            this.textBox2.TabIndex = 24;
            this.textBox2.Text = "Average";
            this.textBox2.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox2.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox1
            // 
            this.textBox1.BackColor = System.Drawing.Color.Black;
            this.textBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox1.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox1.ForeColor = System.Drawing.Color.White;
            this.textBox1.Location = new System.Drawing.Point(8, 473);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(100, 26);
            this.textBox1.TabIndex = 23;
            this.textBox1.Text = "BTC-ETH";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // chart1
            // 
            this.chart1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.chart1.BackColor = System.Drawing.Color.Black;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea1.AxisX2.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.Interval = 100D;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.Interval = 50D;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY.Maximum = 200D;
            chartArea1.AxisY.Minimum = -200D;
            chartArea1.AxisY2.Interval = 50D;
            chartArea1.AxisY2.LabelStyle.Enabled = false;
            chartArea1.AxisY2.MajorGrid.Enabled = false;
            chartArea1.AxisY2.MajorGrid.Interval = 50D;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.MajorTickMark.Enabled = false;
            chartArea1.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea1.AxisY2.Maximum = 800D;
            chartArea1.AxisY2.Minimum = 0D;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea";
            this.chart1.ChartAreas.Add(chartArea1);
            this.chart1.Location = new System.Drawing.Point(6, 21);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.Red;
            series1.Name = "btc";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.RoyalBlue;
            series2.Name = "avg";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series3.ChartArea = "ChartArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Green;
            series3.Name = "dev";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.YAxisType = System.Windows.Forms.DataVisualization.Charting.AxisType.Secondary;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Series.Add(series3);
            this.chart1.Size = new System.Drawing.Size(502, 463);
            this.chart1.TabIndex = 22;
            this.chart1.Text = "chart1";
            // 
            // btn_week
            // 
            this.btn_week.BackColor = System.Drawing.Color.DarkGray;
            this.btn_week.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_week.ForeColor = System.Drawing.Color.Black;
            this.btn_week.Location = new System.Drawing.Point(445, 552);
            this.btn_week.Name = "btn_week";
            this.btn_week.Size = new System.Drawing.Size(81, 30);
            this.btn_week.TabIndex = 31;
            this.btn_week.Text = "Week";
            this.btn_week.UseVisualStyleBackColor = false;
            this.btn_week.Click += new System.EventHandler(this.btn_week_Click);
            // 
            // btn_day
            // 
            this.btn_day.BackColor = System.Drawing.Color.DarkGray;
            this.btn_day.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_day.ForeColor = System.Drawing.Color.Black;
            this.btn_day.Location = new System.Drawing.Point(358, 552);
            this.btn_day.Name = "btn_day";
            this.btn_day.Size = new System.Drawing.Size(81, 30);
            this.btn_day.TabIndex = 30;
            this.btn_day.Text = "Day";
            this.btn_day.UseVisualStyleBackColor = false;
            this.btn_day.Click += new System.EventHandler(this.btn_day_Click);
            // 
            // btn_hour4
            // 
            this.btn_hour4.BackColor = System.Drawing.Color.DarkGray;
            this.btn_hour4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_hour4.ForeColor = System.Drawing.Color.Black;
            this.btn_hour4.Location = new System.Drawing.Point(272, 552);
            this.btn_hour4.Name = "btn_hour4";
            this.btn_hour4.Size = new System.Drawing.Size(81, 30);
            this.btn_hour4.TabIndex = 29;
            this.btn_hour4.Text = "4 Hour";
            this.btn_hour4.UseVisualStyleBackColor = false;
            this.btn_hour4.Click += new System.EventHandler(this.btn_hour4_Click);
            // 
            // btn_hour1
            // 
            this.btn_hour1.BackColor = System.Drawing.Color.DarkGray;
            this.btn_hour1.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_hour1.ForeColor = System.Drawing.Color.Black;
            this.btn_hour1.Location = new System.Drawing.Point(185, 552);
            this.btn_hour1.Name = "btn_hour1";
            this.btn_hour1.Size = new System.Drawing.Size(81, 30);
            this.btn_hour1.TabIndex = 28;
            this.btn_hour1.Text = "1 Hour";
            this.btn_hour1.UseVisualStyleBackColor = false;
            this.btn_hour1.Click += new System.EventHandler(this.btn_hour1_Click);
            // 
            // btn_min30
            // 
            this.btn_min30.BackColor = System.Drawing.Color.DarkGray;
            this.btn_min30.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_min30.ForeColor = System.Drawing.Color.Black;
            this.btn_min30.Location = new System.Drawing.Point(99, 552);
            this.btn_min30.Name = "btn_min30";
            this.btn_min30.Size = new System.Drawing.Size(81, 30);
            this.btn_min30.TabIndex = 27;
            this.btn_min30.Text = "30 Min";
            this.btn_min30.UseVisualStyleBackColor = false;
            this.btn_min30.Click += new System.EventHandler(this.btn_min30_Click);
            // 
            // btn_min10
            // 
            this.btn_min10.BackColor = System.Drawing.Color.DarkGray;
            this.btn_min10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_min10.ForeColor = System.Drawing.Color.Black;
            this.btn_min10.Location = new System.Drawing.Point(12, 552);
            this.btn_min10.Name = "btn_min10";
            this.btn_min10.Size = new System.Drawing.Size(81, 30);
            this.btn_min10.TabIndex = 32;
            this.btn_min10.Text = "10 Min";
            this.btn_min10.UseVisualStyleBackColor = false;
            this.btn_min10.Click += new System.EventHandler(this.btn_min10_Click);
            // 
            // Indicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(538, 594);
            this.Controls.Add(this.btn_min10);
            this.Controls.Add(this.btn_week);
            this.Controls.Add(this.btn_day);
            this.Controls.Add(this.btn_hour4);
            this.Controls.Add(this.btn_hour1);
            this.Controls.Add(this.btn_min30);
            this.Controls.Add(this.groupBox1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(554, 633);
            this.MinimumSize = new System.Drawing.Size(554, 633);
            this.Name = "Indicator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Indicator";
            this.Load += new System.EventHandler(this.Indicator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Button btn_week;
        private System.Windows.Forms.Button btn_day;
        private System.Windows.Forms.Button btn_hour4;
        private System.Windows.Forms.Button btn_hour1;
        private System.Windows.Forms.Button btn_min30;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart1;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.TextBox textBox3;
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox text_dis;
        private System.Windows.Forms.TextBox text_avg;
        private System.Windows.Forms.TextBox text_btc;
        private System.Windows.Forms.TextBox text_min_name;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox text_min_value;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.Button btn_min10;
    }
}