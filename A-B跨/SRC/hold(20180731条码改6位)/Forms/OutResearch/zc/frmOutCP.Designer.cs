namespace hStore.Forms.OutResearch.zc
{
    partial class frmOutCP
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
            this.label1 = new System.Windows.Forms.Label();
            this.dgCp = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 29);
            this.label1.Text = "船批选择";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgCp
            // 
            this.dgCp.BackgroundColor = System.Drawing.Color.White;
            this.dgCp.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgCp.Location = new System.Drawing.Point(0, 29);
            this.dgCp.Name = "dgCp";
            this.dgCp.RowHeadersVisible = false;
            this.dgCp.Size = new System.Drawing.Size(240, 254);
            this.dgCp.TabIndex = 4;
            this.dgCp.CurrentCellChanged += new System.EventHandler(this.dgCp_CurrentCellChanged);
            this.dgCp.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgCp_KeyUp);
            // 
            // frmOutCP
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.dgCp);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutCP";
            this.Text = "frmOutCP";
            this.Load += new System.EventHandler(this.frmOutCP_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmOutCP_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOutCP_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dgCp;
    }
}