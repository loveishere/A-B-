namespace hStore.Forms
{
    partial class frmLogin
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmLogin));
            this.label1 = new System.Windows.Forms.Label();
            this.txtGh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMm = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboBb = new System.Windows.Forms.ComboBox();
            this.btnQd = new System.Windows.Forms.Button();
            this.btnQx = new System.Windows.Forms.Button();
            this.lblMsg = new System.Windows.Forms.Label();
            this.picMsg = new System.Windows.Forms.PictureBox();
            this.picCon = new System.Windows.Forms.PictureBox();
            this.recvTimer = new System.Windows.Forms.Timer();
            this.label5 = new System.Windows.Forms.Label();
            this.cboZyq = new System.Windows.Forms.ComboBox();
            this.sendTimer = new System.Windows.Forms.Timer();
            this.sendFailTimer = new System.Windows.Forms.Timer();
            this.lblUnion = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.lblVersion = new System.Windows.Forms.Label();
            this.aliveTimer = new System.Windows.Forms.Timer();
            this.pic = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(16, 21);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 29);
            this.label1.Text = "工号";
            // 
            // txtGh
            // 
            this.txtGh.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtGh.Location = new System.Drawing.Point(106, 21);
            this.txtGh.Name = "txtGh";
            this.txtGh.Size = new System.Drawing.Size(114, 29);
            this.txtGh.TabIndex = 1;
            this.txtGh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtGh_KeyUp);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(15, 62);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(90, 29);
            this.label2.Text = "密码";
            // 
            // txtMm
            // 
            this.txtMm.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtMm.Location = new System.Drawing.Point(105, 62);
            this.txtMm.Name = "txtMm";
            this.txtMm.PasswordChar = '*';
            this.txtMm.Size = new System.Drawing.Size(114, 29);
            this.txtMm.TabIndex = 4;
            this.txtMm.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtMm_KeyUp);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(15, 103);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(90, 29);
            this.label3.Text = "班别";
            // 
            // cboBb
            // 
            this.cboBb.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.cboBb.Items.Add("夜");
            this.cboBb.Items.Add("白");
            this.cboBb.Location = new System.Drawing.Point(105, 103);
            this.cboBb.Name = "cboBb";
            this.cboBb.Size = new System.Drawing.Size(114, 29);
            this.cboBb.TabIndex = 7;
            // 
            // btnQd
            // 
            this.btnQd.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.btnQd.Location = new System.Drawing.Point(15, 203);
            this.btnQd.Name = "btnQd";
            this.btnQd.Size = new System.Drawing.Size(98, 38);
            this.btnQd.TabIndex = 11;
            this.btnQd.Text = "登陆";
            this.btnQd.Click += new System.EventHandler(this.btnQd_Click);
            // 
            // btnQx
            // 
            this.btnQx.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.btnQx.Location = new System.Drawing.Point(123, 203);
            this.btnQx.Name = "btnQx";
            this.btnQx.Size = new System.Drawing.Size(98, 38);
            this.btnQx.TabIndex = 12;
            this.btnQx.Text = "注销";
            this.btnQx.Click += new System.EventHandler(this.btnQx_Click);
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
            //this.picMsg.Image = ((System.Drawing.Image)(resources.GetObject("picMsg.Image")));
            this.picMsg.Location = new System.Drawing.Point(23, 292);
            this.picMsg.Name = "picMsg";
            this.picMsg.Size = new System.Drawing.Size(23, 23);
            this.picMsg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // picCon
            // 
            this.picCon.BackColor = System.Drawing.SystemColors.Window;
            //this.picCon.Image = ((System.Drawing.Image)(resources.GetObject("picCon.Image")));
            this.picCon.Location = new System.Drawing.Point(0, 292);
            this.picCon.Name = "picCon";
            this.picCon.Size = new System.Drawing.Size(23, 23);
            this.picCon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // recvTimer
            // 
            this.recvTimer.Enabled = true;
            this.recvTimer.Interval = 500;
            this.recvTimer.Tick += new System.EventHandler(this.recvTimer_Tick);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Desktop;
            this.label5.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(15, 144);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(90, 29);
            this.label5.Text = "作业区";
            // 
            // cboZyq
            // 
            this.cboZyq.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.cboZyq.Items.Add("甲");
            this.cboZyq.Items.Add("乙");
            this.cboZyq.Items.Add("丙");
            this.cboZyq.Items.Add("丁");
            this.cboZyq.Location = new System.Drawing.Point(105, 144);
            this.cboZyq.Name = "cboZyq";
            this.cboZyq.Size = new System.Drawing.Size(114, 29);
            this.cboZyq.TabIndex = 27;
            // 
            // sendTimer
            // 
            this.sendTimer.Enabled = true;
            this.sendTimer.Interval = 500;
            this.sendTimer.Tick += new System.EventHandler(this.sendTimer_Tick);
            // 
            // sendFailTimer
            // 
            this.sendFailTimer.Enabled = true;
            this.sendFailTimer.Interval = 60000;
            this.sendFailTimer.Tick += new System.EventHandler(this.sendFailTimer_Tick);
            // 
            // lblUnion
            // 
            this.lblUnion.ForeColor = System.Drawing.Color.Chocolate;
            this.lblUnion.Location = new System.Drawing.Point(145, 254);
            this.lblUnion.Name = "lblUnion";
            this.lblUnion.Size = new System.Drawing.Size(73, 14);
            this.lblUnion.Text = "联合研制";
            // 
            // label6
            // 
            this.label6.ForeColor = System.Drawing.Color.Chocolate;
            this.label6.Location = new System.Drawing.Point(15, 268);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(130, 21);
            this.label6.Text = "宝信软件智能化部";
            // 
            // label8
            // 
            this.label8.ForeColor = System.Drawing.Color.Chocolate;
            this.label8.Location = new System.Drawing.Point(15, 247);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(130, 21);
            this.label8.Text = "宝钢分公司运输部";
            // 
            // lblVersion
            // 
            this.lblVersion.ForeColor = System.Drawing.Color.ForestGreen;
            this.lblVersion.Location = new System.Drawing.Point(143, 268);
            this.lblVersion.Name = "lblVersion";
            this.lblVersion.Size = new System.Drawing.Size(86, 17);
            this.lblVersion.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // aliveTimer
            // 
            this.aliveTimer.Enabled = true;
            this.aliveTimer.Interval = 15000;
            this.aliveTimer.Tick += new System.EventHandler(this.aliveTimer_Tick);
            // 
            // pic
            // 
            this.pic.Location = new System.Drawing.Point(-2, 21);
            this.pic.Name = "pic";
            this.pic.Size = new System.Drawing.Size(43, 29);
            this.pic.Click += new System.EventHandler(this.pic_Click);
            // 
            // frmLogin
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.lblVersion);
            this.Controls.Add(this.lblUnion);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.cboZyq);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.picMsg);
            this.Controls.Add(this.picCon);
            this.Controls.Add(this.btnQx);
            this.Controls.Add(this.btnQd);
            this.Controls.Add(this.cboBb);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMm);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtGh);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pic);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmLogin";
            this.Text = "frmLogin";
            this.Load += new System.EventHandler(this.frmLogin_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmLogin_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtGh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMm;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboBb;
        private System.Windows.Forms.Button btnQd;
        private System.Windows.Forms.Button btnQx;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picMsg;
        private System.Windows.Forms.PictureBox picCon;
        private System.Windows.Forms.Timer recvTimer;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ComboBox cboZyq;
        private System.Windows.Forms.Timer sendTimer;
        private System.Windows.Forms.Timer sendFailTimer;
        private System.Windows.Forms.Label lblUnion;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label lblVersion;
        private System.Windows.Forms.Timer aliveTimer;
        private System.Windows.Forms.PictureBox pic;
    }
}