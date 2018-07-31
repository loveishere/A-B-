namespace hStore.Forms.OutResearch.zc
{
    partial class frmOutPlanZF
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
            this.txtInfo = new System.Windows.Forms.TextBox();
            this.dgZf = new System.Windows.Forms.DataGrid();
            this.lblTitle = new System.Windows.Forms.Label();
            this.cboCLType = new System.Windows.Forms.ComboBox();
            this.txtWcmz = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWcjs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtInfo2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // txtInfo
            // 
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtInfo.Location = new System.Drawing.Point(0, 29);
            this.txtInfo.Multiline = true;
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(240, 20);
            this.txtInfo.TabIndex = 6;
            // 
            // dgZf
            // 
            this.dgZf.BackgroundColor = System.Drawing.Color.White;
            this.dgZf.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgZf.Location = new System.Drawing.Point(0, 70);
            this.dgZf.Name = "dgZf";
            this.dgZf.RowHeadersVisible = false;
            this.dgZf.Size = new System.Drawing.Size(240, 218);
            this.dgZf.TabIndex = 5;
            this.dgZf.CurrentCellChanged += new System.EventHandler(this.dgZf_CurrentCellChanged);
            this.dgZf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgZf_KeyUp);
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Bold);
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 29);
            this.lblTitle.Text = "装船出厂";
            // 
            // cboCLType
            // 
            this.cboCLType.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.cboCLType.Items.Add("准发");
            this.cboCLType.Items.Add("炉号");
            this.cboCLType.Items.Add("合同号");
            this.cboCLType.Items.Add("轧批号");
            this.cboCLType.Location = new System.Drawing.Point(120, 0);
            this.cboCLType.Name = "cboCLType";
            this.cboCLType.Size = new System.Drawing.Size(120, 29);
            this.cboCLType.TabIndex = 8;
            this.cboCLType.SelectedIndexChanged += new System.EventHandler(this.cboCLType_SelectedIndexChanged);
            // 
            // txtWcmz
            // 
            this.txtWcmz.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWcmz.Location = new System.Drawing.Point(170, 289);
            this.txtWcmz.Name = "txtWcmz";
            this.txtWcmz.ReadOnly = true;
            this.txtWcmz.Size = new System.Drawing.Size(70, 26);
            this.txtWcmz.TabIndex = 12;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(120, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 26);
            this.label3.Text = "毛重";
            // 
            // txtWcjs
            // 
            this.txtWcjs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWcjs.Location = new System.Drawing.Point(50, 289);
            this.txtWcjs.Name = "txtWcjs";
            this.txtWcjs.ReadOnly = true;
            this.txtWcjs.Size = new System.Drawing.Size(70, 26);
            this.txtWcjs.TabIndex = 11;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(0, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 26);
            this.label2.Text = "件数";
            // 
            // txtInfo2
            // 
            this.txtInfo2.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInfo2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtInfo2.Location = new System.Drawing.Point(0, 49);
            this.txtInfo2.Multiline = true;
            this.txtInfo2.Name = "txtInfo2";
            this.txtInfo2.Size = new System.Drawing.Size(240, 20);
            this.txtInfo2.TabIndex = 16;
            // 
            // frmOutPlanZF
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.txtInfo2);
            this.Controls.Add(this.txtWcmz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWcjs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.cboCLType);
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.dgZf);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutPlanZF";
            this.Text = "frmOutPlanZF";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOutPlanZF_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmOutPlanZF_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOutPlanZF_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtInfo;
        private System.Windows.Forms.DataGrid dgZf;
        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.ComboBox cboCLType;
        private System.Windows.Forms.TextBox txtWcmz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWcjs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtInfo2;
    }
}