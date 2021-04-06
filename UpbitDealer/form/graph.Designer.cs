using System.Drawing;
using System.Windows.Forms.DataVisualization.Charting;

namespace UpbitDealer.form
{
    partial class graph
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
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea1 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.ChartArea chartArea2 = new System.Windows.Forms.DataVisualization.Charting.ChartArea();
            System.Windows.Forms.DataVisualization.Charting.Series series1 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series2 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series3 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series4 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series5 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series6 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.Windows.Forms.DataVisualization.Charting.Series series7 = new System.Windows.Forms.DataVisualization.Charting.Series();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(graph));
            this.trkbar_horizontal = new System.Windows.Forms.TrackBar();
            this.trkbar_vertical = new System.Windows.Forms.TrackBar();
            this.chart = new System.Windows.Forms.DataVisualization.Charting.Chart();
            this.timer_update = new System.Windows.Forms.Timer(this.components);
            this.btn_1min = new System.Windows.Forms.Button();
            this.btn_3min = new System.Windows.Forms.Button();
            this.btn_5min = new System.Windows.Forms.Button();
            this.btn_10min = new System.Windows.Forms.Button();
            this.btn_30min = new System.Windows.Forms.Button();
            this.btn_4hour = new System.Windows.Forms.Button();
            this.btn_15min = new System.Windows.Forms.Button();
            this.btn_reset = new System.Windows.Forms.Button();
            this.btn_1hour = new System.Windows.Forms.Button();
            this.btn_day = new System.Windows.Forms.Button();
            this.btn_week = new System.Windows.Forms.Button();
            this.btn_month = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.trkbar_horizontal)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbar_vertical)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).BeginInit();
            this.SuspendLayout();
            // 
            // trkbar_horizontal
            // 
            this.trkbar_horizontal.Location = new System.Drawing.Point(12, 728);
            this.trkbar_horizontal.Maximum = 150;
            this.trkbar_horizontal.Name = "trkbar_horizontal";
            this.trkbar_horizontal.Size = new System.Drawing.Size(1137, 45);
            this.trkbar_horizontal.TabIndex = 0;
            this.trkbar_horizontal.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trkbar_horizontal.Scroll += new System.EventHandler(this.trkbar_horizontal_Scroll);
            // 
            // trkbar_vertical
            // 
            this.trkbar_vertical.Location = new System.Drawing.Point(1149, 38);
            this.trkbar_vertical.Maximum = 50;
            this.trkbar_vertical.Name = "trkbar_vertical";
            this.trkbar_vertical.Orientation = System.Windows.Forms.Orientation.Vertical;
            this.trkbar_vertical.Size = new System.Drawing.Size(45, 688);
            this.trkbar_vertical.TabIndex = 1;
            this.trkbar_vertical.TickStyle = System.Windows.Forms.TickStyle.TopLeft;
            this.trkbar_vertical.Scroll += new System.EventHandler(this.trkbar_vertical_Scroll);
            // 
            // chart
            // 
            this.chart.AccessibleRole = System.Windows.Forms.AccessibleRole.None;
            this.chart.BackColor = System.Drawing.Color.Black;
            chartArea1.AxisX.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea1.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisX2.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisX2.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea1.AxisX2.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea1.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.AxisY2.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea1.AxisY2.LineColor = System.Drawing.Color.LightGray;
            chartArea1.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea1.BackColor = System.Drawing.Color.Black;
            chartArea1.Name = "ChartArea1";
            chartArea1.Position.Auto = false;
            chartArea1.Position.Height = 80F;
            chartArea1.Position.Width = 98F;
            chartArea2.AlignWithChartArea = "ChartArea1";
            chartArea2.AxisX.IsLabelAutoFit = false;
            chartArea2.AxisX.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisX.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea2.AxisX.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisX2.MajorGrid.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(32)))), ((int)(((byte)(32)))), ((int)(((byte)(32)))));
            chartArea2.AxisX2.MajorGrid.LineDashStyle = System.Windows.Forms.DataVisualization.Charting.ChartDashStyle.Dot;
            chartArea2.AxisY.LabelStyle.ForeColor = System.Drawing.Color.White;
            chartArea2.AxisY.LineColor = System.Drawing.Color.LightGray;
            chartArea2.AxisY.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea2.AxisY2.MajorGrid.LineColor = System.Drawing.Color.DimGray;
            chartArea2.BackColor = System.Drawing.Color.Black;
            chartArea2.Name = "ChartArea2";
            chartArea2.Position.Auto = false;
            chartArea2.Position.Height = 20F;
            chartArea2.Position.Width = 98F;
            chartArea2.Position.Y = 80F;
            this.chart.ChartAreas.Add(chartArea1);
            this.chart.ChartAreas.Add(chartArea2);
            this.chart.Location = new System.Drawing.Point(12, 38);
            this.chart.Name = "chart";
            series1.BorderColor = System.Drawing.Color.DarkGray;
            series1.ChartArea = "ChartArea1";
            series1.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Candlestick;
            series1.Color = System.Drawing.Color.Yellow;
            series1.CustomProperties = "PriceDownColor=Blue, PriceUpColor=Red";
            series1.IsVisibleInLegend = false;
            series1.Legend = "Legend1";
            series1.Name = "candle";
            series1.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series1.YValuesPerPoint = 4;
            series1.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series2.ChartArea = "ChartArea1";
            series2.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series2.Color = System.Drawing.Color.Green;
            series2.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            series2.IsVisibleInLegend = false;
            series2.Legend = "Legend1";
            series2.Name = "top";
            series2.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series2.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series3.ChartArea = "ChartArea1";
            series3.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series3.Color = System.Drawing.Color.Orange;
            series3.IsVisibleInLegend = false;
            series3.Legend = "Legend1";
            series3.Name = "mid";
            series3.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series3.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series4.ChartArea = "ChartArea1";
            series4.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series4.Color = System.Drawing.Color.Green;
            series4.Font = new System.Drawing.Font("Arial Narrow", 8.25F);
            series4.IsVisibleInLegend = false;
            series4.Legend = "Legend1";
            series4.Name = "bot";
            series4.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series4.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series5.ChartArea = "ChartArea2";
            series5.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series5.Color = System.Drawing.Color.Yellow;
            series5.Name = "MFI";
            series5.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series5.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series6.ChartArea = "ChartArea2";
            series6.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series6.Color = System.Drawing.Color.RoyalBlue;
            series6.Name = "Stochastic_k";
            series6.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series6.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            series7.ChartArea = "ChartArea2";
            series7.ChartType = System.Windows.Forms.DataVisualization.Charting.SeriesChartType.Line;
            series7.Color = System.Drawing.Color.Red;
            series7.Name = "Stochastic_d";
            series7.XValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.DateTime;
            series7.YValueType = System.Windows.Forms.DataVisualization.Charting.ChartValueType.Double;
            this.chart.Series.Add(series1);
            this.chart.Series.Add(series2);
            this.chart.Series.Add(series3);
            this.chart.Series.Add(series4);
            this.chart.Series.Add(series5);
            this.chart.Series.Add(series6);
            this.chart.Series.Add(series7);
            this.chart.Size = new System.Drawing.Size(1137, 688);
            this.chart.TabIndex = 2;
            this.chart.Text = "chart1";
            this.chart.MouseDown += new System.Windows.Forms.MouseEventHandler(this.chart_MouseDown);
            this.chart.MouseMove += new System.Windows.Forms.MouseEventHandler(this.chart_MouseMove);
            this.chart.MouseUp += new System.Windows.Forms.MouseEventHandler(this.chart_MouseUp);
            // 
            // timer_update
            // 
            this.timer_update.Enabled = true;
            this.timer_update.Interval = 1000;
            this.timer_update.Tick += new System.EventHandler(this.timer_update_Tick);
            // 
            // btn_1min
            // 
            this.btn_1min.BackColor = System.Drawing.Color.DarkGray;
            this.btn_1min.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_1min.Location = new System.Drawing.Point(12, 9);
            this.btn_1min.Name = "btn_1min";
            this.btn_1min.Size = new System.Drawing.Size(55, 23);
            this.btn_1min.TabIndex = 3;
            this.btn_1min.Text = "1 Min";
            this.btn_1min.UseVisualStyleBackColor = false;
            this.btn_1min.Click += new System.EventHandler(this.btn_1min_Click);
            // 
            // btn_3min
            // 
            this.btn_3min.BackColor = System.Drawing.Color.DarkGray;
            this.btn_3min.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_3min.Location = new System.Drawing.Point(67, 9);
            this.btn_3min.Name = "btn_3min";
            this.btn_3min.Size = new System.Drawing.Size(55, 23);
            this.btn_3min.TabIndex = 4;
            this.btn_3min.Text = "3 Min";
            this.btn_3min.UseVisualStyleBackColor = false;
            this.btn_3min.Click += new System.EventHandler(this.btn_3min_Click);
            // 
            // btn_5min
            // 
            this.btn_5min.BackColor = System.Drawing.Color.DarkGray;
            this.btn_5min.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_5min.Location = new System.Drawing.Point(122, 9);
            this.btn_5min.Name = "btn_5min";
            this.btn_5min.Size = new System.Drawing.Size(55, 23);
            this.btn_5min.TabIndex = 5;
            this.btn_5min.Text = "5 Min";
            this.btn_5min.UseVisualStyleBackColor = false;
            this.btn_5min.Click += new System.EventHandler(this.btn_5min_Click);
            // 
            // btn_10min
            // 
            this.btn_10min.BackColor = System.Drawing.Color.DarkGray;
            this.btn_10min.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_10min.Location = new System.Drawing.Point(177, 9);
            this.btn_10min.Name = "btn_10min";
            this.btn_10min.Size = new System.Drawing.Size(55, 23);
            this.btn_10min.TabIndex = 6;
            this.btn_10min.Text = "10 Min";
            this.btn_10min.UseVisualStyleBackColor = false;
            this.btn_10min.Click += new System.EventHandler(this.btn_10min_Click);
            // 
            // btn_30min
            // 
            this.btn_30min.BackColor = System.Drawing.Color.DarkGray;
            this.btn_30min.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_30min.Location = new System.Drawing.Point(287, 9);
            this.btn_30min.Name = "btn_30min";
            this.btn_30min.Size = new System.Drawing.Size(55, 23);
            this.btn_30min.TabIndex = 7;
            this.btn_30min.Text = "30 Min";
            this.btn_30min.UseVisualStyleBackColor = false;
            this.btn_30min.Click += new System.EventHandler(this.btn_30min_Click);
            // 
            // btn_4hour
            // 
            this.btn_4hour.BackColor = System.Drawing.Color.DarkGray;
            this.btn_4hour.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_4hour.Location = new System.Drawing.Point(397, 9);
            this.btn_4hour.Name = "btn_4hour";
            this.btn_4hour.Size = new System.Drawing.Size(55, 23);
            this.btn_4hour.TabIndex = 8;
            this.btn_4hour.Text = "4 Hour";
            this.btn_4hour.UseVisualStyleBackColor = false;
            this.btn_4hour.Click += new System.EventHandler(this.btn_4hour_Click);
            // 
            // btn_15min
            // 
            this.btn_15min.BackColor = System.Drawing.Color.DarkGray;
            this.btn_15min.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_15min.Location = new System.Drawing.Point(232, 9);
            this.btn_15min.Name = "btn_15min";
            this.btn_15min.Size = new System.Drawing.Size(55, 23);
            this.btn_15min.TabIndex = 9;
            this.btn_15min.Text = "15 Min";
            this.btn_15min.UseVisualStyleBackColor = false;
            this.btn_15min.Click += new System.EventHandler(this.btn_15min_Click);
            // 
            // btn_reset
            // 
            this.btn_reset.BackColor = System.Drawing.Color.DarkGray;
            this.btn_reset.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_reset.Location = new System.Drawing.Point(1122, 9);
            this.btn_reset.Name = "btn_reset";
            this.btn_reset.Size = new System.Drawing.Size(50, 23);
            this.btn_reset.TabIndex = 10;
            this.btn_reset.Text = "Reset";
            this.btn_reset.UseVisualStyleBackColor = false;
            this.btn_reset.Click += new System.EventHandler(this.btn_reset_Click);
            // 
            // btn_1hour
            // 
            this.btn_1hour.BackColor = System.Drawing.Color.DarkGray;
            this.btn_1hour.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_1hour.Location = new System.Drawing.Point(342, 9);
            this.btn_1hour.Name = "btn_1hour";
            this.btn_1hour.Size = new System.Drawing.Size(55, 23);
            this.btn_1hour.TabIndex = 11;
            this.btn_1hour.Text = "1 Hour";
            this.btn_1hour.UseVisualStyleBackColor = false;
            this.btn_1hour.Click += new System.EventHandler(this.btn_1hour_Click);
            // 
            // btn_day
            // 
            this.btn_day.BackColor = System.Drawing.Color.DarkGray;
            this.btn_day.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_day.Location = new System.Drawing.Point(452, 9);
            this.btn_day.Name = "btn_day";
            this.btn_day.Size = new System.Drawing.Size(55, 23);
            this.btn_day.TabIndex = 12;
            this.btn_day.Text = "Day";
            this.btn_day.UseVisualStyleBackColor = false;
            this.btn_day.Click += new System.EventHandler(this.btn_day_Click);
            // 
            // btn_week
            // 
            this.btn_week.BackColor = System.Drawing.Color.DarkGray;
            this.btn_week.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_week.Location = new System.Drawing.Point(507, 9);
            this.btn_week.Name = "btn_week";
            this.btn_week.Size = new System.Drawing.Size(55, 23);
            this.btn_week.TabIndex = 13;
            this.btn_week.Text = "Week";
            this.btn_week.UseVisualStyleBackColor = false;
            this.btn_week.Click += new System.EventHandler(this.btn_week_Click);
            // 
            // btn_month
            // 
            this.btn_month.BackColor = System.Drawing.Color.DarkGray;
            this.btn_month.Font = new System.Drawing.Font("Arial", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_month.Location = new System.Drawing.Point(562, 9);
            this.btn_month.Name = "btn_month";
            this.btn_month.Size = new System.Drawing.Size(55, 23);
            this.btn_month.TabIndex = 14;
            this.btn_month.Text = "Month";
            this.btn_month.UseVisualStyleBackColor = false;
            this.btn_month.Click += new System.EventHandler(this.btn_month_Click);
            // 
            // graph
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(1184, 761);
            this.Controls.Add(this.btn_month);
            this.Controls.Add(this.btn_week);
            this.Controls.Add(this.btn_day);
            this.Controls.Add(this.btn_1hour);
            this.Controls.Add(this.btn_reset);
            this.Controls.Add(this.btn_15min);
            this.Controls.Add(this.btn_4hour);
            this.Controls.Add(this.btn_30min);
            this.Controls.Add(this.btn_10min);
            this.Controls.Add(this.btn_5min);
            this.Controls.Add(this.btn_3min);
            this.Controls.Add(this.btn_1min);
            this.Controls.Add(this.chart);
            this.Controls.Add(this.trkbar_vertical);
            this.Controls.Add(this.trkbar_horizontal);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MinimumSize = new System.Drawing.Size(500, 400);
            this.Name = "graph";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "graph";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.graph_FormClosed);
            this.Load += new System.EventHandler(this.graph_Load);
            this.Resize += new System.EventHandler(this.graph_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.trkbar_horizontal)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trkbar_vertical)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.chart)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TrackBar trkbar_horizontal;
        private System.Windows.Forms.TrackBar trkbar_vertical;
        private System.Windows.Forms.DataVisualization.Charting.Chart chart;
        private System.Windows.Forms.Timer timer_update;
        private System.Windows.Forms.Button btn_1min;
        private System.Windows.Forms.Button btn_3min;
        private System.Windows.Forms.Button btn_5min;
        private System.Windows.Forms.Button btn_10min;
        private System.Windows.Forms.Button btn_30min;
        private System.Windows.Forms.Button btn_4hour;
        private System.Windows.Forms.Button btn_15min;
        private System.Windows.Forms.Button btn_reset;
        private System.Windows.Forms.Button btn_1hour;
        private System.Windows.Forms.Button btn_day;
        private System.Windows.Forms.Button btn_week;
        private System.Windows.Forms.Button btn_month;
    }
}