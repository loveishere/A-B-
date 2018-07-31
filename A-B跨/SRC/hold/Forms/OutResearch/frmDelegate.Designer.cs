namespace hStore.Forms.OutResearch
{
    partial class frmDelegate
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgPlan = new System.Windows.Forms.DataGrid();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 29);
            this.lblTitle.Text = "委托单";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgPlan
            // 
            this.dgPlan.BackgroundColor = System.Drawing.Color.White;
            this.dgPlan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgPlan.Location = new System.Drawing.Point(0, 29);
            this.dgPlan.Name = "dgPlan";
            this.dgPlan.RowHeadersVisible = false;
            this.dgPlan.Size = new System.Drawing.Size(240, 286);
            this.dgPlan.TabIndex = 1;
            this.dgPlan.CurrentCellChanged += new System.EventHandler(this.dgPlan_CurrentCellChanged);
            this.dgPlan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgPlan_KeyUp);
            // 
            // frmDelegate
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.dgPlan);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDelegate";
            this.Text = "frmDelegate";
            this.Load += new System.EventHandler(this.frmOutPlan_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOutPlan_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGrid dgPlan;
    }
}