namespace hStore.Forms.InResearch
{
    partial class frmInKW
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
            this.txtClh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.txtKwOri = new System.Windows.Forms.TextBox();
            this.txtKwNow = new System.Windows.Forms.TextBox();
            this.btnQd = new System.Windows.Forms.Button();
            this.txtRemain = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(17, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(92, 29);
            this.label1.Text = "材料号";
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(109, 21);
            this.txtClh.Name = "txtClh";
            this.txtClh.ReadOnly = true;
            this.txtClh.Size = new System.Drawing.Size(114, 29);
            this.txtClh.TabIndex = 1;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(17, 115);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(92, 29);
            this.label2.Text = "推荐库位";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(17, 162);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(92, 29);
            this.label4.Text = "实际库位";
            // 
            // txtKwOri
            // 
            this.txtKwOri.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtKwOri.Location = new System.Drawing.Point(109, 115);
            this.txtKwOri.Name = "txtKwOri";
            this.txtKwOri.ReadOnly = true;
            this.txtKwOri.Size = new System.Drawing.Size(114, 29);
            this.txtKwOri.TabIndex = 8;
            this.txtKwOri.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKwOri_KeyUp);
            // 
            // txtKwNow
            // 
            this.txtKwNow.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtKwNow.Location = new System.Drawing.Point(109, 163);
            this.txtKwNow.Name = "txtKwNow";
            this.txtKwNow.Size = new System.Drawing.Size(114, 29);
            this.txtKwNow.TabIndex = 10;
            this.txtKwNow.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKwNow_KeyUp);
            // 
            // btnQd
            // 
            this.btnQd.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnQd.Location = new System.Drawing.Point(71, 238);
            this.btnQd.Name = "btnQd";
            this.btnQd.Size = new System.Drawing.Size(98, 38);
            this.btnQd.TabIndex = 11;
            this.btnQd.Text = "确定";
            this.btnQd.Click += new System.EventHandler(this.btnQd_Click);
            // 
            // txtRemain
            // 
            this.txtRemain.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtRemain.Location = new System.Drawing.Point(109, 68);
            this.txtRemain.Name = "txtRemain";
            this.txtRemain.ReadOnly = true;
            this.txtRemain.Size = new System.Drawing.Size(114, 29);
            this.txtRemain.TabIndex = 16;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(17, 68);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(92, 29);
            this.label3.Text = "准发余数";
            // 
            // frmInKW
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.txtRemain);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.btnQd);
            this.Controls.Add(this.txtKwNow);
            this.Controls.Add(this.txtKwOri);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInKW";
            this.Text = "frmInKW";
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmInKW_KeyUp);
            this.Load += new System.EventHandler(this.frmInKW_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtKwOri;
        private System.Windows.Forms.TextBox txtKwNow;
        private System.Windows.Forms.Button btnQd;
        private System.Windows.Forms.TextBox txtRemain;
        private System.Windows.Forms.Label label3;
    }
}