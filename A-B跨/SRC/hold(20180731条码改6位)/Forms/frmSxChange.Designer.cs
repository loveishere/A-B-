namespace hStore.Forms
{
    partial class frmSxChange
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
            this.dgSxChange = new System.Windows.Forms.DataGrid();
            this.lblTitle = new System.Windows.Forms.Label();
            this.btnLeave = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // dgSxChange
            // 
            this.dgSxChange.BackgroundColor = System.Drawing.Color.White;
            this.dgSxChange.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgSxChange.Location = new System.Drawing.Point(0, 33);
            this.dgSxChange.Name = "dgSxChange";
            this.dgSxChange.RowHeadersVisible = false;
            this.dgSxChange.Size = new System.Drawing.Size(240, 280);
            this.dgSxChange.TabIndex = 17;
            this.dgSxChange.CurrentCellChanged += new System.EventHandler(this.dgSxChange_CurrentCellChanged);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.lblTitle.ForeColor = System.Drawing.Color.Yellow;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 33);
            this.lblTitle.Text = "行车作业指令";
            // 
            // btnLeave
            // 
            this.btnLeave.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnLeave.Location = new System.Drawing.Point(179, 0);
            this.btnLeave.Name = "btnLeave";
            this.btnLeave.Size = new System.Drawing.Size(61, 33);
            this.btnLeave.TabIndex = 18;
            this.btnLeave.Text = "离开";
            this.btnLeave.Click += new System.EventHandler(this.btnLeave_Click);
            // 
            // frmSxChange
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.btnLeave);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.dgSxChange);
            this.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSxChange";
            this.Text = "frmSxChange";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmSxChange_Load);
            this.Activated += new System.EventHandler(this.frmSxChange_Activated);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSxChange_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgSxChange;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.Button btnLeave;

    }
}