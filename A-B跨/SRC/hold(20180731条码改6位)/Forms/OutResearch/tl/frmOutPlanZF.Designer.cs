namespace hStore.Forms.OutResearch.tl
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
            this.dgZf = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.txtWcmz = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWcjs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtDdmz = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtDdjs = new System.Windows.Forms.TextBox();
            this.label5 = new System.Windows.Forms.Label();
            this.cboCLType = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // dgZf
            // 
            this.dgZf.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgZf.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgZf.Location = new System.Drawing.Point(0, 76);
            this.dgZf.Name = "dgZf";
            this.dgZf.RowHeadersVisible = false;
            this.dgZf.Size = new System.Drawing.Size(240, 212);
            this.dgZf.TabIndex = 4;
            this.dgZf.CurrentCellChanged += new System.EventHandler(this.dgZf_CurrentCellChanged);
            this.dgZf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgZf_KeyUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 29);
            this.label1.Text = "铁路出厂－　　";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblInfo
            // 
            this.lblInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblInfo.Location = new System.Drawing.Point(0, 29);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(240, 20);
            // 
            // txtWcmz
            // 
            this.txtWcmz.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWcmz.Location = new System.Drawing.Point(170, 289);
            this.txtWcmz.Name = "txtWcmz";
            this.txtWcmz.ReadOnly = true;
            this.txtWcmz.Size = new System.Drawing.Size(70, 26);
            this.txtWcmz.TabIndex = 6;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(120, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 26);
            this.label3.Text = "完毛";
            // 
            // txtWcjs
            // 
            this.txtWcjs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWcjs.Location = new System.Drawing.Point(50, 289);
            this.txtWcjs.Name = "txtWcjs";
            this.txtWcjs.ReadOnly = true;
            this.txtWcjs.Size = new System.Drawing.Size(70, 26);
            this.txtWcjs.TabIndex = 5;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(0, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(50, 26);
            this.label2.Text = "完件";
            // 
            // txtDdmz
            // 
            this.txtDdmz.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtDdmz.Location = new System.Drawing.Point(170, 49);
            this.txtDdmz.Name = "txtDdmz";
            this.txtDdmz.ReadOnly = true;
            this.txtDdmz.Size = new System.Drawing.Size(70, 26);
            this.txtDdmz.TabIndex = 3;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(120, 49);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(50, 26);
            this.label4.Text = "到毛";
            // 
            // txtDdjs
            // 
            this.txtDdjs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtDdjs.Location = new System.Drawing.Point(50, 49);
            this.txtDdjs.Name = "txtDdjs";
            this.txtDdjs.ReadOnly = true;
            this.txtDdjs.Size = new System.Drawing.Size(70, 26);
            this.txtDdjs.TabIndex = 2;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Desktop;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(0, 49);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(50, 26);
            this.label5.Text = "到件";
            // 
            // cboCLType
            // 
            this.cboCLType.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.cboCLType.Items.Add("准发");
            this.cboCLType.Items.Add("炉号");
            this.cboCLType.Items.Add("合同号");
            this.cboCLType.Items.Add("扎批号");
            this.cboCLType.Location = new System.Drawing.Point(152, 0);
            this.cboCLType.Name = "cboCLType";
            this.cboCLType.Size = new System.Drawing.Size(88, 29);
            this.cboCLType.TabIndex = 1;
            this.cboCLType.SelectedIndexChanged += new System.EventHandler(this.cboCLType_SelectedIndexChanged);
            // 
            // frmOutPlanZF
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.cboCLType);
            this.Controls.Add(this.txtDdmz);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtDdjs);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.txtWcmz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWcjs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.dgZf);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutPlanZF";
            this.Text = "frmOutPlanZF";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOutPlanZF_KeyUp);
            this.Load += new System.EventHandler(this.frmOutPlanZF_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgZf;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblInfo;
        private System.Windows.Forms.TextBox txtWcmz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWcjs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtDdmz;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtDdjs;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboCLType;
    }
}