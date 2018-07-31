namespace hStore.Forms
{
    partial class frmDgv
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.dataGrid1 = new System.Windows.Forms.DataGrid();
            this.txtClh = new System.Windows.Forms.TextBox();
            this.lblClh = new System.Windows.Forms.Label();
            this.lstClh = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // dataGrid1
            // 
            this.dataGrid1.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGrid1.ColumnHeadersVisible = false;
            this.dataGrid1.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.dataGrid1.Location = new System.Drawing.Point(0, 0);
            this.dataGrid1.Name = "dataGrid1";
            this.dataGrid1.RowHeadersVisible = false;
            this.dataGrid1.Size = new System.Drawing.Size(240, 315);
            this.dataGrid1.TabIndex = 0;
            this.dataGrid1.DoubleClick += new System.EventHandler(this.dataGrid1_DoubleClick);
            this.dataGrid1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGrid1_KeyUp);
            this.dataGrid1.Click += new System.EventHandler(this.dataGrid1_Click);
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(79, 28);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(161, 26);
            this.txtClh.TabIndex = 22;
            this.txtClh.Visible = false;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // lblClh
            // 
            this.lblClh.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblClh.ForeColor = System.Drawing.Color.Yellow;
            this.lblClh.Location = new System.Drawing.Point(0, 28);
            this.lblClh.Name = "lblClh";
            this.lblClh.Size = new System.Drawing.Size(82, 26);
            this.lblClh.Text = "材料号";
            this.lblClh.Visible = false;
            // 
            // lstClh
            // 
            this.lstClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lstClh.Location = new System.Drawing.Point(0, 57);
            this.lstClh.Name = "lstClh";
            this.lstClh.Size = new System.Drawing.Size(240, 230);
            this.lstClh.TabIndex = 23;
            this.lstClh.Visible = false;
            this.lstClh.SelectedIndexChanged += new System.EventHandler(this.lstClh_SelectedIndexChanged);
            // 
            // frmDgv
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.lblClh);
            this.Controls.Add(this.lstClh);
            this.Controls.Add(this.dataGrid1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDgv";
            this.Text = "frmDgv";
            this.Load += new System.EventHandler(this.frmDgv_Load);
            this.Activated += new System.EventHandler(this.frmDgv_Activated);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDgv_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dataGrid1;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.Label lblClh;
        private System.Windows.Forms.ListBox lstClh;
    }
}