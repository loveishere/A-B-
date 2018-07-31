namespace hStore.Forms
{
    partial class frmWtd
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
            this.dgWtd = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 33);
            this.label1.Text = "委托单作业";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgWtd
            // 
            this.dgWtd.BackgroundColor = System.Drawing.Color.White;
            this.dgWtd.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgWtd.Location = new System.Drawing.Point(0, 33);
            this.dgWtd.Name = "dgWtd";
            this.dgWtd.RowHeadersVisible = false;
            this.dgWtd.Size = new System.Drawing.Size(240, 280);
            this.dgWtd.TabIndex = 16;
            this.dgWtd.CurrentCellChanged += new System.EventHandler(this.dgWtd_CurrentCellChanged);
            this.dgWtd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgWtd_KeyUp);
            // 
            // frmWtd
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.dgWtd);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWtd";
            this.Text = "frmWtd";
            this.Load += new System.EventHandler(this.frmWtd_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmWtd_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmWtd_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dgWtd;
    }
}