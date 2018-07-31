namespace hStore.Forms
{
    partial class FrameIn
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
            this.btnRW = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.cboQY = new System.Windows.Forms.ComboBox();
            this.textKJH = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.cboKZ = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.textZZ = new System.Windows.Forms.TextBox();
            this.btnScan = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.textTDH = new System.Windows.Forms.TextBox();
            this.textTCH = new System.Windows.Forms.TextBox();
            this.cboFX = new System.Windows.Forms.ComboBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblMsg = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.Category = new System.Windows.Forms.ComboBox();
            this.btnSelect = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnRW
            // 
            this.btnRW.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnRW.Location = new System.Drawing.Point(23, 237);
            this.btnRW.Name = "btnRW";
            this.btnRW.Size = new System.Drawing.Size(201, 40);
            this.btnRW.TabIndex = 0;
            this.btnRW.Text = "入位";
            this.btnRW.Click += new System.EventHandler(this.btnRW_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(23, 11);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(95, 29);
            this.label1.Text = "停车位号";
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(23, 48);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(95, 29);
            this.label2.Text = "车号";
            // 
            // cboQY
            // 
            this.cboQY.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.cboQY.Items.Add("");
            this.cboQY.Items.Add("宝钢");
            this.cboQY.Items.Add("沪");
            this.cboQY.Items.Add("苏");
            this.cboQY.Items.Add("浙");
            this.cboQY.Items.Add("皖");
            this.cboQY.Items.Add("鲁");
            this.cboQY.Items.Add("津");
            this.cboQY.Items.Add("渝");
            this.cboQY.Items.Add("冀");
            this.cboQY.Items.Add("豫");
            this.cboQY.Items.Add("云");
            this.cboQY.Items.Add("辽");
            this.cboQY.Items.Add("黑");
            this.cboQY.Items.Add("湘");
            this.cboQY.Items.Add("新");
            this.cboQY.Items.Add("赣");
            this.cboQY.Items.Add("鄂");
            this.cboQY.Items.Add("桂");
            this.cboQY.Items.Add("甘");
            this.cboQY.Items.Add("晋");
            this.cboQY.Items.Add("蒙");
            this.cboQY.Items.Add("陕");
            this.cboQY.Items.Add("吉");
            this.cboQY.Items.Add("闽");
            this.cboQY.Items.Add("贵");
            this.cboQY.Items.Add("粤");
            this.cboQY.Items.Add("川");
            this.cboQY.Items.Add("青");
            this.cboQY.Items.Add("藏");
            this.cboQY.Items.Add("琼");
            this.cboQY.Items.Add("宁");
            this.cboQY.Items.Add("京");
            this.cboQY.Location = new System.Drawing.Point(111, 48);
            this.cboQY.Name = "cboQY";
            this.cboQY.Size = new System.Drawing.Size(46, 29);
            this.cboQY.TabIndex = 4;
            // 
            // textKJH
            // 
            this.textKJH.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.textKJH.Location = new System.Drawing.Point(156, 48);
            this.textKJH.Name = "textKJH";
            this.textKJH.Size = new System.Drawing.Size(68, 29);
            this.textKJH.TabIndex = 5;
            this.textKJH.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textKJH_KeyUp);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label3.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(23, 122);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(52, 29);
            this.label3.Text = "状态";
            // 
            // cboKZ
            // 
            this.cboKZ.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.cboKZ.Items.Add("空");
            this.cboKZ.Items.Add("重");
            this.cboKZ.Items.Add("拼");
            this.cboKZ.Location = new System.Drawing.Point(75, 122);
            this.cboKZ.Name = "cboKZ";
            this.cboKZ.Size = new System.Drawing.Size(49, 29);
            this.cboKZ.TabIndex = 7;
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label5.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(23, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(95, 29);
            this.label5.Text = "载重";
            // 
            // textZZ
            // 
            this.textZZ.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.textZZ.Location = new System.Drawing.Point(110, 159);
            this.textZZ.Name = "textZZ";
            this.textZZ.Size = new System.Drawing.Size(114, 29);
            this.textZZ.TabIndex = 13;
            // 
            // btnScan
            // 
            this.btnScan.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnScan.Location = new System.Drawing.Point(141, 237);
            this.btnScan.Name = "btnScan";
            this.btnScan.Size = new System.Drawing.Size(83, 40);
            this.btnScan.TabIndex = 14;
            this.btnScan.Text = "扫描";
            this.btnScan.Visible = false;
            this.btnScan.Click += new System.EventHandler(this.btnScan_Click_1);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label6.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(23, 196);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(95, 29);
            this.label6.Text = "提单号";
            // 
            // textTDH
            // 
            this.textTDH.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.textTDH.Location = new System.Drawing.Point(110, 196);
            this.textTDH.Name = "textTDH";
            this.textTDH.Size = new System.Drawing.Size(62, 29);
            this.textTDH.TabIndex = 21;
            // 
            // textTCH
            // 
            this.textTCH.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.textTCH.Location = new System.Drawing.Point(111, 11);
            this.textTCH.Name = "textTCH";
            this.textTCH.Size = new System.Drawing.Size(114, 29);
            this.textTCH.TabIndex = 29;
            this.textTCH.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textTCH_KeyUp);
            // 
            // cboFX
            // 
            this.cboFX.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.cboFX.Items.Add("朝北");
            this.cboFX.Items.Add("朝南");
            this.cboFX.Location = new System.Drawing.Point(112, 85);
            this.cboFX.Name = "cboFX";
            this.cboFX.Size = new System.Drawing.Size(114, 29);
            this.cboFX.TabIndex = 36;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(23, 85);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(95, 29);
            this.label4.Text = "方向";
            // 
            // lblMsg
            // 
            this.lblMsg.Font = new System.Drawing.Font("Tahoma", 11F, System.Drawing.FontStyle.Regular);
            this.lblMsg.Location = new System.Drawing.Point(0, 288);
            this.lblMsg.Name = "lblMsg";
            this.lblMsg.Size = new System.Drawing.Size(240, 26);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label7.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(124, 122);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(59, 29);
            this.label7.Text = "种类";
            // 
            // Category
            // 
            this.Category.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.Category.Items.Add("卷");
            this.Category.Items.Add("板");
            this.Category.Location = new System.Drawing.Point(177, 122);
            this.Category.Name = "Category";
            this.Category.Size = new System.Drawing.Size(49, 29);
            this.Category.TabIndex = 45;
            // 
            // btnSelect
            // 
            this.btnSelect.Location = new System.Drawing.Point(177, 196);
            this.btnSelect.Name = "btnSelect";
            this.btnSelect.Size = new System.Drawing.Size(47, 29);
            this.btnSelect.TabIndex = 22;
            this.btnSelect.Text = "选择";
            // 
            // FrameIn
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.Category);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.lblMsg);
            this.Controls.Add(this.cboFX);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textTCH);
            this.Controls.Add(this.btnSelect);
            this.Controls.Add(this.textTDH);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.btnScan);
            this.Controls.Add(this.textZZ);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.cboKZ);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textKJH);
            this.Controls.Add(this.cboQY);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.btnRW);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.Name = "FrameIn";
            this.Text = "FrameIn";
            this.Load += new System.EventHandler(this.FrameIn_Load);
            this.Activated += new System.EventHandler(this.FrameIn_Activated);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.FrameIn_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnRW;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox cboQY;
        private System.Windows.Forms.TextBox textKJH;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.ComboBox cboKZ;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox textZZ;
        private System.Windows.Forms.Button btnScan;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textTDH;
        private System.Windows.Forms.TextBox textTCH;
        private System.Windows.Forms.ComboBox cboFX;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.ComboBox Category;
        private System.Windows.Forms.Button btnSelect;
    }
}