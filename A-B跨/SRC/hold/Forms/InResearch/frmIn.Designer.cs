namespace hStore.Forms.InResearch
{
    partial class frmIn
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
            this.label2 = new System.Windows.Forms.Label();
            this.txtClh = new System.Windows.Forms.TextBox();
            this.dgCl = new System.Windows.Forms.DataGrid();
            this.lstCLH = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 26);
            this.label2.Text = "材料号";
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(64, 0);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(176, 26);
            this.txtClh.TabIndex = 5;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // dgCl
            // 
            this.dgCl.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgCl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgCl.Location = new System.Drawing.Point(0, 27);
            this.dgCl.Name = "dgCl";
            this.dgCl.RowHeadersVisible = false;
            this.dgCl.Size = new System.Drawing.Size(240, 288);
            this.dgCl.TabIndex = 6;
            this.dgCl.CurrentCellChanged += new System.EventHandler(this.dgCl_CurrentCellChanged);
            this.dgCl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgCl_KeyUp);
            // 
            // lstCLH
            // 
            this.lstCLH.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lstCLH.Location = new System.Drawing.Point(60, 27);
            this.lstCLH.Name = "lstCLH";
            this.lstCLH.Size = new System.Drawing.Size(180, 192);
            this.lstCLH.TabIndex = 45;
            this.lstCLH.Visible = false;
            this.lstCLH.SelectedIndexChanged += new System.EventHandler(this.lstCLH_SelectedIndexChanged);
            // 
            // frmIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.lstCLH);
            this.Controls.Add(this.dgCl);
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.label2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmIn";
            this.Text = "frmIn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmIn_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmIn_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.DataGrid dgCl;
        private System.Windows.Forms.ListBox lstCLH;
    }
}