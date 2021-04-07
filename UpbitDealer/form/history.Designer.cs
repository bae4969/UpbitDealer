
namespace UpbitDealer.form
{
    partial class History
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
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle1 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle2 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle3 = new System.Windows.Forms.DataGridViewCellStyle();
            System.Windows.Forms.DataGridViewCellStyle dataGridViewCellStyle4 = new System.Windows.Forms.DataGridViewCellStyle();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(History));
            this.dataGridView1 = new System.Windows.Forms.DataGridView();
            this.timer_binding = new System.Windows.Forms.Timer(this.components);
            this.btn_order = new System.Windows.Forms.Button();
            this.btn_history = new System.Windows.Forms.Button();
            this.btn_history_get = new System.Windows.Forms.Button();
            this.btn_cancel = new System.Windows.Forms.Button();
            this.text_selectInfo = new System.Windows.Forms.TextBox();
            this.btn_macro = new System.Windows.Forms.Button();
            this.text_historyCoinName = new System.Windows.Forms.TextBox();
            this.text_page = new System.Windows.Forms.TextBox();
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).BeginInit();
            this.SuspendLayout();
            // 
            // dataGridView1
            // 
            this.dataGridView1.AllowUserToAddRows = false;
            this.dataGridView1.AllowUserToDeleteRows = false;
            this.dataGridView1.AllowUserToOrderColumns = true;
            this.dataGridView1.AllowUserToResizeRows = false;
            this.dataGridView1.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dataGridView1.BackgroundColor = System.Drawing.Color.Black;
            this.dataGridView1.CellBorderStyle = System.Windows.Forms.DataGridViewCellBorderStyle.SingleVertical;
            this.dataGridView1.ColumnHeadersBorderStyle = System.Windows.Forms.DataGridViewHeaderBorderStyle.Single;
            dataGridViewCellStyle1.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleCenter;
            dataGridViewCellStyle1.BackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle1.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.SelectionBackColor = System.Drawing.Color.Black;
            dataGridViewCellStyle1.SelectionForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle1.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle1;
            this.dataGridView1.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dataGridViewCellStyle2.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle2.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle2.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle2.ForeColor = System.Drawing.Color.White;
            dataGridViewCellStyle2.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle2.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle2.WrapMode = System.Windows.Forms.DataGridViewTriState.False;
            this.dataGridView1.DefaultCellStyle = dataGridViewCellStyle2;
            this.dataGridView1.EnableHeadersVisualStyles = false;
            this.dataGridView1.GridColor = System.Drawing.Color.White;
            this.dataGridView1.Location = new System.Drawing.Point(12, 48);
            this.dataGridView1.MultiSelect = false;
            this.dataGridView1.Name = "dataGridView1";
            this.dataGridView1.ReadOnly = true;
            dataGridViewCellStyle3.Alignment = System.Windows.Forms.DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle3.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle3.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            dataGridViewCellStyle3.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle3.SelectionForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle3.WrapMode = System.Windows.Forms.DataGridViewTriState.True;
            this.dataGridView1.RowHeadersDefaultCellStyle = dataGridViewCellStyle3;
            this.dataGridView1.RowHeadersVisible = false;
            dataGridViewCellStyle4.BackColor = System.Drawing.Color.DimGray;
            dataGridViewCellStyle4.ForeColor = System.Drawing.Color.Black;
            dataGridViewCellStyle4.SelectionBackColor = System.Drawing.Color.LightGray;
            dataGridViewCellStyle4.SelectionForeColor = System.Drawing.Color.Black;
            this.dataGridView1.RowsDefaultCellStyle = dataGridViewCellStyle4;
            this.dataGridView1.RowTemplate.Height = 23;
            this.dataGridView1.ScrollBars = System.Windows.Forms.ScrollBars.Vertical;
            this.dataGridView1.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dataGridView1.Size = new System.Drawing.Size(760, 401);
            this.dataGridView1.TabIndex = 0;
            this.dataGridView1.CellMouseClick += new System.Windows.Forms.DataGridViewCellMouseEventHandler(this.dataGridView1_CellMouseClick);
            this.dataGridView1.SelectionChanged += new System.EventHandler(this.dataGridView1_SelectionChanged);
            this.dataGridView1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.dataGridView1_MouseUp);
            // 
            // timer_binding
            // 
            this.timer_binding.Enabled = true;
            this.timer_binding.Interval = 500;
            this.timer_binding.Tick += new System.EventHandler(this.timer_binding_Tick);
            // 
            // btn_order
            // 
            this.btn_order.BackColor = System.Drawing.Color.Red;
            this.btn_order.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_order.ForeColor = System.Drawing.Color.Black;
            this.btn_order.Location = new System.Drawing.Point(12, 12);
            this.btn_order.Name = "btn_order";
            this.btn_order.Size = new System.Drawing.Size(75, 30);
            this.btn_order.TabIndex = 3;
            this.btn_order.Text = "Order";
            this.btn_order.UseVisualStyleBackColor = false;
            this.btn_order.Click += new System.EventHandler(this.btn_order_Click);
            // 
            // btn_history
            // 
            this.btn_history.BackColor = System.Drawing.Color.DarkGray;
            this.btn_history.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_history.ForeColor = System.Drawing.Color.Black;
            this.btn_history.Location = new System.Drawing.Point(88, 12);
            this.btn_history.Name = "btn_history";
            this.btn_history.Size = new System.Drawing.Size(75, 30);
            this.btn_history.TabIndex = 4;
            this.btn_history.Text = "History";
            this.btn_history.UseVisualStyleBackColor = false;
            this.btn_history.Click += new System.EventHandler(this.btn_history_Click);
            // 
            // btn_history_get
            // 
            this.btn_history_get.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_history_get.BackColor = System.Drawing.Color.DarkGray;
            this.btn_history_get.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_history_get.ForeColor = System.Drawing.Color.Black;
            this.btn_history_get.Location = new System.Drawing.Point(712, 12);
            this.btn_history_get.Name = "btn_history_get";
            this.btn_history_get.Size = new System.Drawing.Size(60, 30);
            this.btn_history_get.TabIndex = 5;
            this.btn_history_get.Text = "Get";
            this.btn_history_get.UseVisualStyleBackColor = false;
            this.btn_history_get.Visible = false;
            this.btn_history_get.Click += new System.EventHandler(this.btn_history_get_Click);
            // 
            // btn_cancel
            // 
            this.btn_cancel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.btn_cancel.BackColor = System.Drawing.Color.DarkGray;
            this.btn_cancel.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_cancel.ForeColor = System.Drawing.Color.Black;
            this.btn_cancel.Location = new System.Drawing.Point(697, 12);
            this.btn_cancel.Name = "btn_cancel";
            this.btn_cancel.Size = new System.Drawing.Size(75, 30);
            this.btn_cancel.TabIndex = 6;
            this.btn_cancel.Text = "Cancel";
            this.btn_cancel.UseVisualStyleBackColor = false;
            this.btn_cancel.Click += new System.EventHandler(this.btn_cancel_Click);
            // 
            // text_selectInfo
            // 
            this.text_selectInfo.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_selectInfo.BackColor = System.Drawing.Color.LightGray;
            this.text_selectInfo.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_selectInfo.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_selectInfo.Location = new System.Drawing.Point(272, 14);
            this.text_selectInfo.Name = "text_selectInfo";
            this.text_selectInfo.ReadOnly = true;
            this.text_selectInfo.Size = new System.Drawing.Size(419, 26);
            this.text_selectInfo.TabIndex = 7;
            this.text_selectInfo.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_selectInfo.Enter += new System.EventHandler(this.text_focus_disable);
            // 
            // btn_macro
            // 
            this.btn_macro.BackColor = System.Drawing.Color.DarkGray;
            this.btn_macro.Font = new System.Drawing.Font("Arial Black", 9.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_macro.ForeColor = System.Drawing.Color.Black;
            this.btn_macro.Location = new System.Drawing.Point(164, 12);
            this.btn_macro.Name = "btn_macro";
            this.btn_macro.Size = new System.Drawing.Size(75, 30);
            this.btn_macro.TabIndex = 8;
            this.btn_macro.Text = "Macro";
            this.btn_macro.UseVisualStyleBackColor = false;
            this.btn_macro.Click += new System.EventHandler(this.btn_macro_Click);
            // 
            // text_historyCoinName
            // 
            this.text_historyCoinName.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_historyCoinName.BackColor = System.Drawing.Color.LightGray;
            this.text_historyCoinName.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_historyCoinName.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_historyCoinName.Location = new System.Drawing.Point(421, 14);
            this.text_historyCoinName.Name = "text_historyCoinName";
            this.text_historyCoinName.Size = new System.Drawing.Size(132, 26);
            this.text_historyCoinName.TabIndex = 9;
            this.text_historyCoinName.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_historyCoinName.Visible = false;
            this.text_historyCoinName.MouseUp += new System.Windows.Forms.MouseEventHandler(this.text_clean_MouseUp);
            // 
            // text_page
            // 
            this.text_page.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.text_page.BackColor = System.Drawing.Color.LightGray;
            this.text_page.Cursor = System.Windows.Forms.Cursors.Arrow;
            this.text_page.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.text_page.Location = new System.Drawing.Point(559, 14);
            this.text_page.Name = "text_page";
            this.text_page.Size = new System.Drawing.Size(132, 26);
            this.text_page.TabIndex = 10;
            this.text_page.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.text_page.Visible = false;
            this.text_page.MouseUp += new System.Windows.Forms.MouseEventHandler(this.text_clean_MouseUp);
            // 
            // History
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Black;
            this.ClientSize = new System.Drawing.Size(784, 461);
            this.Controls.Add(this.text_page);
            this.Controls.Add(this.text_historyCoinName);
            this.Controls.Add(this.btn_macro);
            this.Controls.Add(this.text_selectInfo);
            this.Controls.Add(this.btn_cancel);
            this.Controls.Add(this.btn_history_get);
            this.Controls.Add(this.btn_history);
            this.Controls.Add(this.btn_order);
            this.Controls.Add(this.dataGridView1);
            this.Font = new System.Drawing.Font("Arial", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ForeColor = System.Drawing.Color.White;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MinimumSize = new System.Drawing.Size(800, 300);
            this.Name = "History";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "History";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.History_FormClosed);
            this.Load += new System.EventHandler(this.History_Load);
            this.Resize += new System.EventHandler(this.History_Resize);
            ((System.ComponentModel.ISupportInitialize)(this.dataGridView1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.DataGridView dataGridView1;
        private System.Windows.Forms.Timer timer_binding;
        private System.Windows.Forms.Button btn_order;
        private System.Windows.Forms.Button btn_history;
        private System.Windows.Forms.Button btn_history_get;
        private System.Windows.Forms.Button btn_cancel;
        private System.Windows.Forms.TextBox text_selectInfo;
        private System.Windows.Forms.Button btn_macro;
        private System.Windows.Forms.TextBox text_historyCoinName;
        private System.Windows.Forms.TextBox text_page;
    }
}