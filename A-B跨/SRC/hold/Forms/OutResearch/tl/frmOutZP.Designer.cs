namespace hStore.Forms.OutResearch.tl
{
    partial class frmOutZP
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
            this.dgCl = new System.Windows.Forms.DataGrid();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtJs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.txtMz = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.txtClh = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // dgCl
            // 
            this.dgCl.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgCl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgCl.Location = new System.Drawing.Point(0, 67);
            this.dgCl.Name = "dgCl";
            this.dgCl.RowHeadersVisible = false;
            this.dgCl.Size = new System.Drawing.Size(240, 221);
            this.dgCl.TabIndex = 9;
            this.dgCl.CurrentCellChanged += new System.EventHandler(this.dgCl_CurrentCellChanged);
            this.dgCl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgCl_KeyUp);
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.SystemColors.Window;
            this.lblInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblInfo.Location = new System.Drawing.Point(0, 0);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(240, 40);
            this.lblInfo.Text = "到站：";
            // 
            // txtJs
            // 
            this.txtJs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtJs.Location = new System.Drawing.Point(52, 289);
            this.txtJs.Name = "txtJs";
            this.txtJs.Size = new System.Drawing.Size(69, 26);
            this.txtJs.TabIndex = 17;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Desktop;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(0, 289);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(52, 26);
            this.label5.Text = "件数";
            // 
            // txtMz
            // 
            this.txtMz.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtMz.Location = new System.Drawing.Point(168, 289);
            this.txtMz.Name = "txtMz";
            this.txtMz.Size = new System.Drawing.Size(72, 26);
            this.txtMz.TabIndex = 16;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(119, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(49, 26);
            this.label4.Text = "毛重";
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(0, 40);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(66, 26);
            this.label1.Text = "材料号";
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(66, 40);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(174, 26);
            this.txtClh.TabIndex = 22;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // frmOutZP
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtJs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtMz);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgCl);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutZP";
            this.Text = "frmOutZP";
            this.Closed += new System.EventHandler(this.frmOutZP_Closed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOutZP_KeyUp);
            this.Load += new System.EventHandler(this.frmOutZP_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgCl;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtJs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtMz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClh;
    }
}