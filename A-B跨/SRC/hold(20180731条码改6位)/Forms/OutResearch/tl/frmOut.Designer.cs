namespace hStore.Forms.OutResearch.tl
{
    partial class frmOut
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
            this.dgOut = new System.Windows.Forms.DataGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMz = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtJs = new System.Windows.Forms.TextBox();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtClh = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.lstCLH = new System.Windows.Forms.ListBox();
            this.SuspendLayout();
            // 
            // dgOut
            // 
            this.dgOut.BackgroundColor = System.Drawing.Color.White;
            this.dgOut.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgOut.Location = new System.Drawing.Point(0, 67);
            this.dgOut.Name = "dgOut";
            this.dgOut.RowHeadersVisible = false;
            this.dgOut.Size = new System.Drawing.Size(240, 221);
            this.dgOut.TabIndex = 8;
            this.dgOut.CurrentCellChanged += new System.EventHandler(this.dgOut_CurrentCellChanged);
            this.dgOut.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgOut_KeyUp);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(0, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(46, 26);
            this.label4.Text = "毛重";
            // 
            // txtMz
            // 
            this.txtMz.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtMz.Location = new System.Drawing.Point(46, 289);
            this.txtMz.Name = "txtMz";
            this.txtMz.ReadOnly = true;
            this.txtMz.Size = new System.Drawing.Size(73, 26);
            this.txtMz.TabIndex = 10;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Desktop;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(121, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(46, 26);
            this.label5.Text = "件数";
            // 
            // txtJs
            // 
            this.txtJs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtJs.Location = new System.Drawing.Point(167, 289);
            this.txtJs.Name = "txtJs";
            this.txtJs.ReadOnly = true;
            this.txtJs.Size = new System.Drawing.Size(73, 26);
            this.txtJs.TabIndex = 13;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(240, 40);
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(77, 40);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(163, 26);
            this.txtClh.TabIndex = 28;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(0, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(77, 26);
            this.label1.Text = "材料号";
            // 
            // lstCLH
            // 
            this.lstCLH.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lstCLH.Location = new System.Drawing.Point(60, 67);
            this.lstCLH.Name = "lstCLH";
            this.lstCLH.Size = new System.Drawing.Size(180, 192);
            this.lstCLH.TabIndex = 46;
            this.lstCLH.Visible = false;
            this.lstCLH.SelectedIndexChanged += new System.EventHandler(this.lstCLH_SelectedIndexChanged);
            // 
            // frmOut
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.lstCLH);
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.txtJs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMz);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.dgOut);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOut";
            this.Text = "frmOut";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closed += new System.EventHandler(this.frmOut_Closed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOut_KeyUp);
            this.Load += new System.EventHandler(this.frmOut_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgOut;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMz;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtJs;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ListBox lstCLH;
    }
}