namespace hStore.Forms
{
    partial class frmMove
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmMove));
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClh = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtYkw = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtXkw = new System.Windows.Forms.TextBox();
            this.btnQd = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.picMsg = new System.Windows.Forms.PictureBox();
            this.picCon = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 30);
            this.label1.Text = "倒垛";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(13, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 29);
            this.label2.Text = "材料号";
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(106, 62);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(121, 29);
            this.txtClh.TabIndex = 2;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(13, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 29);
            this.label3.Text = "原库位";
            // 
            // txtYkw
            // 
            this.txtYkw.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtYkw.Location = new System.Drawing.Point(106, 122);
            this.txtYkw.Name = "txtYkw";
            this.txtYkw.Size = new System.Drawing.Size(121, 29);
            this.txtYkw.TabIndex = 5;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(13, 182);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 29);
            this.label4.Text = "新库位";
            // 
            // txtXkw
            // 
            this.txtXkw.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtXkw.Location = new System.Drawing.Point(106, 182);
            this.txtXkw.Name = "txtXkw";
            this.txtXkw.Size = new System.Drawing.Size(121, 29);
            this.txtXkw.TabIndex = 8;
            // 
            // btnQd
            // 
            this.btnQd.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnQd.Location = new System.Drawing.Point(71, 232);
            this.btnQd.Name = "btnQd";
            this.btnQd.Size = new System.Drawing.Size(98, 38);
            this.btnQd.TabIndex = 12;
            this.btnQd.Text = "确定";
            this.btnQd.Click += new System.EventHandler(this.btnQd_Click);
            // 
            // lblMsg
            // 
            this.lblMsg.BackColor = System.Drawing.SystemColors.Window;
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(46, 292);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(194, 23);
            // 
            // picMsg
            // 
            this.picMsg.BackColor = System.Drawing.SystemColors.Window;
            this.picMsg.Image = ((System.Drawing.Image)(resources.GetObject("picMsg.Image")));
            this.picMsg.Location = new System.Drawing.Point(23, 292);
            this.picMsg.Name = "picMsg";
            this.picMsg.Size = new System.Drawing.Size(23, 23);
            this.picMsg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // picCon
            // 
            this.picCon.BackColor = System.Drawing.SystemColors.Window;
            this.picCon.Image = ((System.Drawing.Image)(resources.GetObject("picCon.Image")));
            this.picCon.Location = new System.Drawing.Point(0, 292);
            this.picCon.Name = "picCon";
            this.picCon.Size = new System.Drawing.Size(23, 23);
            this.picCon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // frmMove
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.picMsg);
            this.Controls.Add(this.picCon);
            this.Controls.Add(this.btnQd);
            this.Controls.Add(this.txtXkw);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtYkw);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmMove";
            this.Text = "frmMove";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closed += new System.EventHandler(this.frmMove_Closed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmMove_KeyUp);
            this.Load += new System.EventHandler(this.frmMove_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtYkw;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtXkw;
        private System.Windows.Forms.Button btnQd;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picMsg;
        private System.Windows.Forms.PictureBox picCon;
    }
}