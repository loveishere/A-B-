﻿namespace hStore.Forms
{
    partial class frmSend
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
            this.txtJs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtMz = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtKjh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.cboStatus = new System.Windows.Forms.ComboBox();
            this.btnSend = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtJxh = new System.Windows.Forms.TextBox();
            this.cboDq = new System.Windows.Forms.ComboBox();
            this.txtDL1 = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.txtDJ = new System.Windows.Forms.TextBox();
            this.label7 = new System.Windows.Forms.Label();
            this.txtDL2 = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(17, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(93, 26);
            this.label1.Text = "件数";
            // 
            // txtJs
            // 
            this.txtJs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtJs.Location = new System.Drawing.Point(108, 5);
            this.txtJs.Name = "txtJs";
            this.txtJs.ReadOnly = true;
            this.txtJs.Size = new System.Drawing.Size(116, 26);
            this.txtJs.TabIndex = 1;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(17, 43);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(93, 26);
            this.label2.Text = "毛重";
            // 
            // txtMz
            // 
            this.txtMz.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtMz.Location = new System.Drawing.Point(108, 43);
            this.txtMz.Name = "txtMz";
            this.txtMz.ReadOnly = true;
            this.txtMz.Size = new System.Drawing.Size(116, 26);
            this.txtMz.TabIndex = 4;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(17, 81);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(93, 26);
            this.label3.Text = "车号";
            // 
            // txtKjh
            // 
            this.txtKjh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtKjh.Location = new System.Drawing.Point(156, 81);
            this.txtKjh.Name = "txtKjh";
            this.txtKjh.Size = new System.Drawing.Size(68, 26);
            this.txtKjh.TabIndex = 7;
            this.txtKjh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKjh_KeyUp);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(17, 119);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(93, 26);
            this.label4.Text = "框架状态";
            // 
            // cboStatus
            // 
            this.cboStatus.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.cboStatus.Items.Add("空");
            this.cboStatus.Items.Add("重");
            this.cboStatus.Location = new System.Drawing.Point(108, 119);
            this.cboStatus.Name = "cboStatus";
            this.cboStatus.Size = new System.Drawing.Size(116, 26);
            this.cboStatus.TabIndex = 10;
            // 
            // btnSend
            // 
            this.btnSend.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnSend.Location = new System.Drawing.Point(68, 269);
            this.btnSend.Name = "btnSend";
            this.btnSend.Size = new System.Drawing.Size(98, 38);
            this.btnSend.TabIndex = 11;
            this.btnSend.Text = "发送";
            this.btnSend.Click += new System.EventHandler(this.btnSend_Click);
            // 
            // label5
            // 
            this.label5.BackColor = System.Drawing.SystemColors.Desktop;
            this.label5.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label5.ForeColor = System.Drawing.Color.Yellow;
            this.label5.Location = new System.Drawing.Point(17, 157);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(93, 26);
            this.label5.Text = "行车号";
            // 
            // txtJxh
            // 
            this.txtJxh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtJxh.Location = new System.Drawing.Point(108, 157);
            this.txtJxh.Name = "txtJxh";
            this.txtJxh.Size = new System.Drawing.Size(116, 26);
            this.txtJxh.TabIndex = 18;
            // 
            // cboDq
            // 
            this.cboDq.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.cboDq.Items.Add("");
            this.cboDq.Items.Add("宝钢");
            this.cboDq.Items.Add("沪");
            this.cboDq.Items.Add("苏");
            this.cboDq.Items.Add("浙");
            this.cboDq.Items.Add("皖");
            this.cboDq.Items.Add("鲁");
            this.cboDq.Items.Add("津");
            this.cboDq.Items.Add("渝");
            this.cboDq.Items.Add("冀");
            this.cboDq.Items.Add("豫");
            this.cboDq.Items.Add("云");
            this.cboDq.Items.Add("辽");
            this.cboDq.Items.Add("黑");
            this.cboDq.Items.Add("湘");
            this.cboDq.Items.Add("新");
            this.cboDq.Items.Add("赣");
            this.cboDq.Items.Add("鄂");
            this.cboDq.Items.Add("桂");
            this.cboDq.Items.Add("甘");
            this.cboDq.Items.Add("晋");
            this.cboDq.Items.Add("蒙");
            this.cboDq.Items.Add("陕");
            this.cboDq.Items.Add("吉");
            this.cboDq.Items.Add("闽");
            this.cboDq.Items.Add("贵");
            this.cboDq.Items.Add("粤");
            this.cboDq.Items.Add("川");
            this.cboDq.Items.Add("青");
            this.cboDq.Items.Add("藏");
            this.cboDq.Items.Add("琼");
            this.cboDq.Items.Add("宁");
            this.cboDq.Items.Add("京");
            this.cboDq.Location = new System.Drawing.Point(110, 81);
            this.cboDq.Name = "cboDq";
            this.cboDq.Size = new System.Drawing.Size(46, 26);
            this.cboDq.TabIndex = 35;
            // 
            // txtDL1
            // 
            this.txtDL1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtDL1.Location = new System.Drawing.Point(80, 195);
            this.txtDL1.Name = "txtDL1";
            this.txtDL1.Size = new System.Drawing.Size(78, 26);
            this.txtDL1.TabIndex = 43;
            this.txtDL1.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDL1_KeyUp);
            // 
            // label6
            // 
            this.label6.BackColor = System.Drawing.SystemColors.Desktop;
            this.label6.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label6.ForeColor = System.Drawing.Color.Yellow;
            this.label6.Location = new System.Drawing.Point(17, 195);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(63, 26);
            this.label6.Text = "吊梁号";
            // 
            // txtDJ
            // 
            this.txtDJ.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtDJ.Location = new System.Drawing.Point(108, 233);
            this.txtDJ.Name = "txtDJ";
            this.txtDJ.Size = new System.Drawing.Size(116, 26);
            this.txtDJ.TabIndex = 46;
            this.txtDJ.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDJ_KeyUp);
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Desktop;
            this.label7.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(17, 233);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(93, 26);
            this.label7.Text = "吊具号";
            // 
            // txtDL2
            // 
            this.txtDL2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtDL2.Location = new System.Drawing.Point(160, 195);
            this.txtDL2.Name = "txtDL2";
            this.txtDL2.Size = new System.Drawing.Size(78, 26);
            this.txtDL2.TabIndex = 54;
            this.txtDL2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtDL2_KeyUp);
            // 
            // frmSend
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.txtDL2);
            this.Controls.Add(this.txtDJ);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.txtDL1);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.cboDq);
            this.Controls.Add(this.txtJxh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.btnSend);
            this.Controls.Add(this.cboStatus);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtKjh);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtMz);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtJs);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSend";
            this.Text = "frmSend";
            this.Load += new System.EventHandler(this.frmSend_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmSend_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtJs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtMz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtKjh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.ComboBox cboStatus;
        private System.Windows.Forms.Button btnSend;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtJxh;
        private System.Windows.Forms.ComboBox cboDq;
        private System.Windows.Forms.TextBox txtDL1;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox txtDJ;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox txtDL2;
    }
}