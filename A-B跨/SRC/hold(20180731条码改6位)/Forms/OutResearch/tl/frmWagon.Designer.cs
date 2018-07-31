namespace hStore.Forms.OutResearch.tl
{
    partial class frmWagon
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
            this.lstWagon = new System.Windows.Forms.ListBox();
            this.btnWagon = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // lstWagon
            // 
            this.lstWagon.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lstWagon.Location = new System.Drawing.Point(3, 38);
            this.lstWagon.Name = "lstWagon";
            this.lstWagon.Size = new System.Drawing.Size(234, 209);
            this.lstWagon.TabIndex = 0;
            // 
            // btnWagon
            // 
            this.btnWagon.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnWagon.Location = new System.Drawing.Point(12, 253);
            this.btnWagon.Name = "btnWagon";
            this.btnWagon.Size = new System.Drawing.Size(98, 38);
            this.btnWagon.TabIndex = 1;
            this.btnWagon.Text = "申请车皮";
            this.btnWagon.Click += new System.EventHandler(this.btnWagon_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(3, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(234, 35);
            this.label1.Text = "股道车皮清单";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnOK
            // 
            this.btnOK.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnOK.Location = new System.Drawing.Point(125, 253);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(98, 38);
            this.btnOK.TabIndex = 3;
            this.btnOK.Text = "确认";
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // frmWagon
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnWagon);
            this.Controls.Add(this.lstWagon);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWagon";
            this.Text = "frmWagon";
            this.Load += new System.EventHandler(this.frmWagon_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmWagon_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ListBox lstWagon;
        private System.Windows.Forms.Button btnWagon;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnOK;
    }
}