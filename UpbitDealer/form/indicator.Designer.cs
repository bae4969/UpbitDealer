
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
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Indicator));
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.bb_max_value = new System.Windows.Forms.TextBox();
            this.textBox4 = new System.Windows.Forms.TextBox();
            this.bb_max_name = new System.Windows.Forms.TextBox();
            this.textBox7 = new System.Windows.Forms.TextBox();
            this.bb_min_value = new System.Windows.Forms.TextBox();
            this.textBox5 = new System.Windows.Forms.TextBox();
            this.bb_min_name = new System.Windows.Forms.TextBox();
            this.textBox8 = new System.Windows.Forms.TextBox();
            this.bb_avg = new System.Windows.Forms.TextBox();
            this.bb_top3 = new System.Windows.Forms.TextBox();
            this.textBox2 = new System.Windows.Forms.TextBox();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.chart1 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.btn_week = new System.Windows.Forms.Button();
            this.btn_day = new System.Windows.Forms.Button();
            this.btn_hour4 = new System.Windows.Forms.Button();
            this.btn_hour1 = new System.Windows.Forms.Button();
            this.btn_min30 = new System.Windows.Forms.Button();
            this.list_hotList = new System.Windows.Forms.ListBox();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.list_dangerList = new System.Windows.Forms.ListBox();
            this.groupBox4 = new System.Windows.Forms.GroupBox();
            this.tl_max_value = new System.Windows.Forms.TextBox();
            this.textBox6 = new System.Windows.Forms.TextBox();
            this.tl_max_name = new System.Windows.Forms.TextBox();
            this.textBox10 = new System.Windows.Forms.TextBox();
            this.tl_min_value = new System.Windows.Forms.TextBox();
            this.textBox12 = new System.Windows.Forms.TextBox();
            this.tl_min_name = new System.Windows.Forms.TextBox();
            this.textBox14 = new System.Windows.Forms.TextBox();
            this.tl_avg = new System.Windows.Forms.TextBox();
            this.tl_top3 = new System.Windows.Forms.TextBox();
            this.textBox17 = new System.Windows.Forms.TextBox();
            this.textBox18 = new System.Windows.Forms.TextBox();
            this.chart2 = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.groupBox1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).BeginInit();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.groupBox4.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).BeginInit();
            this.SuspendLayout();
            // 
            // groupBox1
            // 
            this.groupBox1.Controls.Add(this.bb_max_value);
            this.groupBox1.Controls.Add(this.textBox4);
            this.groupBox1.Controls.Add(this.bb_max_name);
            this.groupBox1.Controls.Add(this.textBox7);
            this.groupBox1.Controls.Add(this.bb_min_value);
            this.groupBox1.Controls.Add(this.textBox5);
            this.groupBox1.Controls.Add(this.bb_min_name);
            this.groupBox1.Controls.Add(this.textBox8);
            this.groupBox1.Controls.Add(this.bb_avg);
            this.groupBox1.Controls.Add(this.bb_top3);
            this.groupBox1.Controls.Add(this.textBox2);
            this.groupBox1.Controls.Add(this.textBox1);
            this.groupBox1.Controls.Add(this.chart1);
            this.groupBox1.ForeColor = System.Drawing.Color.White;
            this.groupBox1.Location = new System.Drawing.Point(12, 48);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(584, 363);
            this.groupBox1.TabIndex = 23;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Bollinger";
            // 
            // bb_max_value
            // 
            this.bb_max_value.BackColor = System.Drawing.Color.Black;
            this.bb_max_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bb_max_value.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bb_max_value.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bb_max_value.ForeColor = System.Drawing.Color.White;
            this.bb_max_value.Location = new System.Drawing.Point(495, 217);
            this.bb_max_value.Name = "bb_max_value";
            this.bb_max_value.ReadOnly = true;
            this.bb_max_value.Size = new System.Drawing.Size(83, 26);
            this.bb_max_value.TabIndex = 36;
            this.bb_max_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bb_max_value.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox4
            // 
            this.textBox4.BackColor = System.Drawing.Color.Black;
            this.textBox4.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox4.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox4.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox4.ForeColor = System.Drawing.Color.White;
            this.textBox4.Location = new System.Drawing.Point(495, 192);
            this.textBox4.Name = "textBox4";
            this.textBox4.ReadOnly = true;
            this.textBox4.Size = new System.Drawing.Size(83, 26);
            this.textBox4.TabIndex = 35;
            this.textBox4.Text = "Max Value";
            this.textBox4.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox4.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // bb_max_name
            // 
            this.bb_max_name.BackColor = System.Drawing.Color.Black;
            this.bb_max_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bb_max_name.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bb_max_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bb_max_name.ForeColor = System.Drawing.Color.White;
            this.bb_max_name.Location = new System.Drawing.Point(495, 160);
            this.bb_max_name.Name = "bb_max_name";
            this.bb_max_name.ReadOnly = true;
            this.bb_max_name.Size = new System.Drawing.Size(83, 26);
            this.bb_max_name.TabIndex = 34;
            this.bb_max_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bb_max_name.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox7
            // 
            this.textBox7.BackColor = System.Drawing.Color.Black;
            this.textBox7.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox7.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox7.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox7.ForeColor = System.Drawing.Color.White;
            this.textBox7.Location = new System.Drawing.Point(495, 135);
            this.textBox7.Name = "textBox7";
            this.textBox7.ReadOnly = true;
            this.textBox7.Size = new System.Drawing.Size(83, 26);
            this.textBox7.TabIndex = 33;
            this.textBox7.Text = "Max Name";
            this.textBox7.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox7.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // bb_min_value
            // 
            this.bb_min_value.BackColor = System.Drawing.Color.Black;
            this.bb_min_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bb_min_value.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bb_min_value.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bb_min_value.ForeColor = System.Drawing.Color.White;
            this.bb_min_value.Location = new System.Drawing.Point(495, 331);
            this.bb_min_value.Name = "bb_min_value";
            this.bb_min_value.ReadOnly = true;
            this.bb_min_value.Size = new System.Drawing.Size(83, 26);
            this.bb_min_value.TabIndex = 32;
            this.bb_min_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox5
            // 
            this.textBox5.BackColor = System.Drawing.Color.Black;
            this.textBox5.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox5.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox5.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox5.ForeColor = System.Drawing.Color.White;
            this.textBox5.Location = new System.Drawing.Point(495, 306);
            this.textBox5.Name = "textBox5";
            this.textBox5.ReadOnly = true;
            this.textBox5.Size = new System.Drawing.Size(83, 26);
            this.textBox5.TabIndex = 31;
            this.textBox5.Text = "Min Value";
            this.textBox5.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // bb_min_name
            // 
            this.bb_min_name.BackColor = System.Drawing.Color.Black;
            this.bb_min_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bb_min_name.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bb_min_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bb_min_name.ForeColor = System.Drawing.Color.White;
            this.bb_min_name.Location = new System.Drawing.Point(495, 274);
            this.bb_min_name.Name = "bb_min_name";
            this.bb_min_name.ReadOnly = true;
            this.bb_min_name.Size = new System.Drawing.Size(83, 26);
            this.bb_min_name.TabIndex = 30;
            this.bb_min_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bb_min_name.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox8
            // 
            this.textBox8.BackColor = System.Drawing.Color.Black;
            this.textBox8.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox8.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox8.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox8.ForeColor = System.Drawing.Color.White;
            this.textBox8.Location = new System.Drawing.Point(495, 249);
            this.textBox8.Name = "textBox8";
            this.textBox8.ReadOnly = true;
            this.textBox8.Size = new System.Drawing.Size(83, 26);
            this.textBox8.TabIndex = 29;
            this.textBox8.Text = "Min Name";
            this.textBox8.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox8.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // bb_avg
            // 
            this.bb_avg.BackColor = System.Drawing.Color.Black;
            this.bb_avg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bb_avg.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bb_avg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bb_avg.ForeColor = System.Drawing.Color.White;
            this.bb_avg.Location = new System.Drawing.Point(495, 103);
            this.bb_avg.Name = "bb_avg";
            this.bb_avg.ReadOnly = true;
            this.bb_avg.Size = new System.Drawing.Size(83, 26);
            this.bb_avg.TabIndex = 27;
            this.bb_avg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bb_avg.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // bb_top3
            // 
            this.bb_top3.BackColor = System.Drawing.Color.Black;
            this.bb_top3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bb_top3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.bb_top3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.bb_top3.ForeColor = System.Drawing.Color.White;
            this.bb_top3.Location = new System.Drawing.Point(495, 46);
            this.bb_top3.Name = "bb_top3";
            this.bb_top3.ReadOnly = true;
            this.bb_top3.Size = new System.Drawing.Size(83, 26);
            this.bb_top3.TabIndex = 26;
            this.bb_top3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.bb_top3.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // textBox2
            // 
            this.textBox2.BackColor = System.Drawing.Color.Black;
            this.textBox2.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox2.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox2.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox2.ForeColor = System.Drawing.Color.White;
            this.textBox2.Location = new System.Drawing.Point(495, 78);
            this.textBox2.Name = "textBox2";
            this.textBox2.ReadOnly = true;
            this.textBox2.Size = new System.Drawing.Size(83, 26);
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
            this.textBox1.Location = new System.Drawing.Point(495, 21);
            this.textBox1.Name = "textBox1";
            this.textBox1.ReadOnly = true;
            this.textBox1.Size = new System.Drawing.Size(83, 26);
            this.textBox1.TabIndex = 23;
            this.textBox1.Text = "Top3";
            this.textBox1.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.textBox1.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // chart1
            // 
            this.chart1.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.chart1.BackColor = System.Drawing.Color.Black;
            chartArea1.AxisX.IsLabelAutoFit = false;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Empty;
            chartArea1.AxisX.TitleForeColor = System.Drawing.Color.Empty;
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
            this.chart1.Location = new System.Drawing.Point(1, 18);
            this.chart1.Name = "chart1";
            series1.BorderWidth = 3;
            series1.ChartArea = "ChartArea";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series1.Color = System.Drawing.Color.RoyalBlue;
            series1.Name = "bb_avg";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.BorderWidth = 3;
            series2.ChartArea = "ChartArea";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Red;
            series2.Name = "bb_top3";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart1.Series.Add(series1);
            this.chart1.Series.Add(series2);
            this.chart1.Size = new System.Drawing.Size(500, 343);
            this.chart1.TabIndex = 22;
            this.chart1.Text = "chart1";
            // 
            // btn_week
            // 
            this.btn_week.BackColor = System.Drawing.Color.DarkGray;
            this.btn_week.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_week.ForeColor = System.Drawing.Color.Black;
            this.btn_week.Location = new System.Drawing.Point(484, 12);
            this.btn_week.Name = "btn_week";
            this.btn_week.Size = new System.Drawing.Size(112, 30);
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
            this.btn_day.Location = new System.Drawing.Point(366, 12);
            this.btn_day.Name = "btn_day";
            this.btn_day.Size = new System.Drawing.Size(112, 30);
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
            this.btn_hour4.Location = new System.Drawing.Point(248, 12);
            this.btn_hour4.Name = "btn_hour4";
            this.btn_hour4.Size = new System.Drawing.Size(112, 30);
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
            this.btn_hour1.Location = new System.Drawing.Point(130, 12);
            this.btn_hour1.Name = "btn_hour1";
            this.btn_hour1.Size = new System.Drawing.Size(112, 30);
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
            this.btn_min30.Location = new System.Drawing.Point(12, 12);
            this.btn_min30.Name = "btn_min30";
            this.btn_min30.Size = new System.Drawing.Size(112, 30);
            this.btn_min30.TabIndex = 27;
            this.btn_min30.Text = "30 Min";
            this.btn_min30.UseVisualStyleBackColor = false;
            this.btn_min30.Click += new System.EventHandler(this.btn_min30_Click);
            // 
            // list_hotList
            // 
            this.list_hotList.BackColor = System.Drawing.Color.Black;
            this.list_hotList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Italic);
            this.list_hotList.ForeColor = System.Drawing.Color.White;
            this.list_hotList.FormattingEnabled = true;
            this.list_hotList.ItemHeight = 19;
            this.list_hotList.Location = new System.Drawing.Point(6, 25);
            this.list_hotList.Name = "list_hotList";
            this.list_hotList.ScrollAlwaysVisible = true;
            this.list_hotList.Size = new System.Drawing.Size(98, 327);
            this.list_hotList.Sorted = true;
            this.list_hotList.TabIndex = 33;
            // 
            // groupBox2
            // 
            this.groupBox2.Controls.Add(this.list_hotList);
            this.groupBox2.ForeColor = System.Drawing.Color.White;
            this.groupBox2.Location = new System.Drawing.Point(602, 48);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(110, 363);
            this.groupBox2.TabIndex = 34;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "Rate > 10";
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.list_dangerList);
            this.groupBox3.ForeColor = System.Drawing.Color.White;
            this.groupBox3.Location = new System.Drawing.Point(602, 417);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(110, 363);
            this.groupBox3.TabIndex = 35;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Rate < -10";
            // 
            // list_dangerList
            // 
            this.list_dangerList.BackColor = System.Drawing.Color.Black;
            this.list_dangerList.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Italic);
            this.list_dangerList.ForeColor = System.Drawing.Color.White;
            this.list_dangerList.FormattingEnabled = true;
            this.list_dangerList.ItemHeight = 19;
            this.list_dangerList.Location = new System.Drawing.Point(6, 25);
            this.list_dangerList.Name = "list_dangerList";
            this.list_dangerList.ScrollAlwaysVisible = true;
            this.list_dangerList.Size = new System.Drawing.Size(98, 327);
            this.list_dangerList.Sorted = true;
            this.list_dangerList.TabIndex = 33;
            // 
            // groupBox4
            // 
            this.groupBox4.Controls.Add(this.tl_max_value);
            this.groupBox4.Controls.Add(this.textBox6);
            this.groupBox4.Controls.Add(this.tl_max_name);
            this.groupBox4.Controls.Add(this.textBox10);
            this.groupBox4.Controls.Add(this.tl_min_value);
            this.groupBox4.Controls.Add(this.textBox12);
            this.groupBox4.Controls.Add(this.tl_min_name);
            this.groupBox4.Controls.Add(this.textBox14);
            this.groupBox4.Controls.Add(this.tl_avg);
            this.groupBox4.Controls.Add(this.tl_top3);
            this.groupBox4.Controls.Add(this.textBox17);
            this.groupBox4.Controls.Add(this.textBox18);
            this.groupBox4.Controls.Add(this.chart2);
            this.groupBox4.ForeColor = System.Drawing.Color.White;
            this.groupBox4.Location = new System.Drawing.Point(12, 417);
            this.groupBox4.Name = "groupBox4";
            this.groupBox4.Size = new System.Drawing.Size(584, 363);
            this.groupBox4.TabIndex = 37;
            this.groupBox4.TabStop = false;
            this.groupBox4.Text = "Trend Line";
            // 
            // tl_max_value
            // 
            this.tl_max_value.BackColor = System.Drawing.Color.Black;
            this.tl_max_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tl_max_value.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tl_max_value.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_max_value.ForeColor = System.Drawing.Color.White;
            this.tl_max_value.Location = new System.Drawing.Point(494, 217);
            this.tl_max_value.Name = "tl_max_value";
            this.tl_max_value.ReadOnly = true;
            this.tl_max_value.Size = new System.Drawing.Size(83, 26);
            this.tl_max_value.TabIndex = 48;
            this.tl_max_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox6
            // 
            this.textBox6.BackColor = System.Drawing.Color.Black;
            this.textBox6.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox6.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox6.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox6.ForeColor = System.Drawing.Color.White;
            this.textBox6.Location = new System.Drawing.Point(494, 192);
            this.textBox6.Name = "textBox6";
            this.textBox6.ReadOnly = true;
            this.textBox6.Size = new System.Drawing.Size(83, 26);
            this.textBox6.TabIndex = 47;
            this.textBox6.Text = "Max Value";
            this.textBox6.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tl_max_name
            // 
            this.tl_max_name.BackColor = System.Drawing.Color.Black;
            this.tl_max_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tl_max_name.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tl_max_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_max_name.ForeColor = System.Drawing.Color.White;
            this.tl_max_name.Location = new System.Drawing.Point(494, 160);
            this.tl_max_name.Name = "tl_max_name";
            this.tl_max_name.ReadOnly = true;
            this.tl_max_name.Size = new System.Drawing.Size(83, 26);
            this.tl_max_name.TabIndex = 46;
            this.tl_max_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox10
            // 
            this.textBox10.BackColor = System.Drawing.Color.Black;
            this.textBox10.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox10.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox10.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox10.ForeColor = System.Drawing.Color.White;
            this.textBox10.Location = new System.Drawing.Point(494, 135);
            this.textBox10.Name = "textBox10";
            this.textBox10.ReadOnly = true;
            this.textBox10.Size = new System.Drawing.Size(83, 26);
            this.textBox10.TabIndex = 45;
            this.textBox10.Text = "Max Name";
            this.textBox10.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tl_min_value
            // 
            this.tl_min_value.BackColor = System.Drawing.Color.Black;
            this.tl_min_value.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tl_min_value.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tl_min_value.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_min_value.ForeColor = System.Drawing.Color.White;
            this.tl_min_value.Location = new System.Drawing.Point(495, 331);
            this.tl_min_value.Name = "tl_min_value";
            this.tl_min_value.ReadOnly = true;
            this.tl_min_value.Size = new System.Drawing.Size(83, 26);
            this.tl_min_value.TabIndex = 44;
            this.tl_min_value.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox12
            // 
            this.textBox12.BackColor = System.Drawing.Color.Black;
            this.textBox12.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox12.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox12.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox12.ForeColor = System.Drawing.Color.White;
            this.textBox12.Location = new System.Drawing.Point(495, 306);
            this.textBox12.Name = "textBox12";
            this.textBox12.ReadOnly = true;
            this.textBox12.Size = new System.Drawing.Size(83, 26);
            this.textBox12.TabIndex = 43;
            this.textBox12.Text = "Min Value";
            this.textBox12.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tl_min_name
            // 
            this.tl_min_name.BackColor = System.Drawing.Color.Black;
            this.tl_min_name.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tl_min_name.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tl_min_name.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_min_name.ForeColor = System.Drawing.Color.White;
            this.tl_min_name.Location = new System.Drawing.Point(494, 274);
            this.tl_min_name.Name = "tl_min_name";
            this.tl_min_name.ReadOnly = true;
            this.tl_min_name.Size = new System.Drawing.Size(83, 26);
            this.tl_min_name.TabIndex = 42;
            this.tl_min_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox14
            // 
            this.textBox14.BackColor = System.Drawing.Color.Black;
            this.textBox14.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox14.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox14.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox14.ForeColor = System.Drawing.Color.White;
            this.textBox14.Location = new System.Drawing.Point(494, 249);
            this.textBox14.Name = "textBox14";
            this.textBox14.ReadOnly = true;
            this.textBox14.Size = new System.Drawing.Size(83, 26);
            this.textBox14.TabIndex = 41;
            this.textBox14.Text = "Min Name";
            this.textBox14.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tl_avg
            // 
            this.tl_avg.BackColor = System.Drawing.Color.Black;
            this.tl_avg.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tl_avg.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tl_avg.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_avg.ForeColor = System.Drawing.Color.White;
            this.tl_avg.Location = new System.Drawing.Point(494, 103);
            this.tl_avg.Name = "tl_avg";
            this.tl_avg.ReadOnly = true;
            this.tl_avg.Size = new System.Drawing.Size(83, 26);
            this.tl_avg.TabIndex = 40;
            this.tl_avg.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // tl_top3
            // 
            this.tl_top3.BackColor = System.Drawing.Color.Black;
            this.tl_top3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.tl_top3.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.tl_top3.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.tl_top3.ForeColor = System.Drawing.Color.White;
            this.tl_top3.Location = new System.Drawing.Point(494, 46);
            this.tl_top3.Name = "tl_top3";
            this.tl_top3.ReadOnly = true;
            this.tl_top3.Size = new System.Drawing.Size(83, 26);
            this.tl_top3.TabIndex = 39;
            this.tl_top3.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox17
            // 
            this.textBox17.BackColor = System.Drawing.Color.Black;
            this.textBox17.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox17.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox17.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox17.ForeColor = System.Drawing.Color.White;
            this.textBox17.Location = new System.Drawing.Point(494, 78);
            this.textBox17.Name = "textBox17";
            this.textBox17.ReadOnly = true;
            this.textBox17.Size = new System.Drawing.Size(83, 26);
            this.textBox17.TabIndex = 38;
            this.textBox17.Text = "Average";
            this.textBox17.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // textBox18
            // 
            this.textBox18.BackColor = System.Drawing.Color.Black;
            this.textBox18.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.textBox18.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.textBox18.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.textBox18.ForeColor = System.Drawing.Color.White;
            this.textBox18.Location = new System.Drawing.Point(494, 21);
            this.textBox18.Name = "textBox18";
            this.textBox18.ReadOnly = true;
            this.textBox18.Size = new System.Drawing.Size(83, 26);
            this.textBox18.TabIndex = 37;
            this.textBox18.Text = "Top3";
            this.textBox18.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // chart2
            // 
            this.chart2.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.chart2.BackColor = System.Drawing.Color.Black;
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisX.MajorTickMark.LineColor = System.Drawing.Color.Empty;
            chartArea2.AxisX.TitleForeColor = System.Drawing.Color.Empty;
            chartArea2.AxisX2.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea2.AxisX2.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorGrid.Interval = 0D;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisY.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea2.AxisY2.Interval = 50D;
            chartArea2.AxisY2.LabelStyle.Enabled = false;
            chartArea2.AxisY2.MajorGrid.Enabled = false;
            chartArea2.AxisY2.MajorGrid.Interval = 50D;
            chartArea2.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisY2.MajorTickMark.Enabled = false;
            chartArea2.AxisY2.MajorTickMark.LineColor = System.Drawing.Color.White;
            chartArea2.AxisY2.Maximum = 800D;
            chartArea2.AxisY2.Minimum = 0D;
            chartArea2.BackColor = System.Drawing.Color.Black;
            chartArea2.Name = "ChartArea";
            this.chart2.ChartAreas.Add(chartArea2);
            this.chart2.Location = new System.Drawing.Point(1, 18);
            this.chart2.Name = "chart2";
            series3.BorderWidth = 3;
            series3.ChartArea = "ChartArea";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.RoyalBlue;
            series3.Name = "tl_avg";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series4.BorderWidth = 3;
            series4.ChartArea = "ChartArea";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Red;
            series4.Name = "tl_top3";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart2.Series.Add(series3);
            this.chart2.Series.Add(series4);
            this.chart2.Size = new System.Drawing.Size(500, 343);
            this.chart2.TabIndex = 22;
            this.chart2.Text = "chart2";
            // 
            // Indicator
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(724, 792);
            this.Controls.Add(this.groupBox4);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.groupBox2);
            this.Controls.Add(this.groupBox1);
            this.Controls.Add(this.btn_week);
            this.Controls.Add(this.btn_min30);
            this.Controls.Add(this.btn_hour1);
            this.Controls.Add(this.btn_day);
            this.Controls.Add(this.btn_hour4);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximumSize = new System.Drawing.Size(740, 831);
            this.MinimumSize = new System.Drawing.Size(740, 831);
            this.Name = "Indicator";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Indicator";
            this.Load += new System.EventHandler(this.Indicator_Load);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart1)).EndInit();
            this.groupBox2.ResumeLayout(false);
            this.groupBox3.ResumeLayout(false);
            this.groupBox4.ResumeLayout(false);
            this.groupBox4.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.chart2)).EndInit();
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
        private System.Windows.Forms.TextBox textBox2;
        private System.Windows.Forms.TextBox bb_avg;
        private System.Windows.Forms.TextBox bb_top3;
        private System.Windows.Forms.TextBox bb_min_name;
        private System.Windows.Forms.TextBox textBox8;
        private System.Windows.Forms.TextBox bb_min_value;
        private System.Windows.Forms.TextBox textBox5;
        private System.Windows.Forms.ListBox list_hotList;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.ListBox list_dangerList;
        private System.Windows.Forms.TextBox bb_max_value;
        private System.Windows.Forms.TextBox textBox4;
        private System.Windows.Forms.TextBox bb_max_name;
        private System.Windows.Forms.TextBox textBox7;
        private System.Windows.Forms.GroupBox groupBox4;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart2;
        private System.Windows.Forms.TextBox tl_max_value;
        private System.Windows.Forms.TextBox textBox6;
        private System.Windows.Forms.TextBox tl_max_name;
        private System.Windows.Forms.TextBox textBox10;
        private System.Windows.Forms.TextBox tl_min_value;
        private System.Windows.Forms.TextBox textBox12;
        private System.Windows.Forms.TextBox tl_min_name;
        private System.Windows.Forms.TextBox textBox14;
        private System.Windows.Forms.TextBox tl_avg;
        private System.Windows.Forms.TextBox tl_top3;
        private System.Windows.Forms.TextBox textBox17;
        private System.Windows.Forms.TextBox textBox18;
    }
}