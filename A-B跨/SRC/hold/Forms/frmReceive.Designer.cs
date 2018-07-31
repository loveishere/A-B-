namespace hStore
{
    partial class frmReceive
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmReceive));
            this.lblTitle = new System.Windows.Forms.Label();
            this.picCon = new System.Windows.Forms.PictureBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJhh = new System.Windows.Forms.TextBox();
            this.btnDown = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.lblRJhs = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            this.lblRCls = new System.Windows.Forms.Label();
            this.lblZt = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.lblCCls = new System.Windows.Forms.Label();
            this.lblCJhs = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 30);
            this.lblTitle.Text = "FMA计划下载";
            // 
            // picCon
            // 
            this.picCon.BackColor = System.Drawing.SystemColors.Window;
            //this.picCon.Image = ((System.Drawing.Image)(resources.GetObject("picCon.Image")));
            this.picCon.Location = new System.Drawing.Point(4, 276);
            this.picCon.Name = "picCon";
            this.picCon.Size = new System.Drawing.Size(23, 23);
            this.picCon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(3, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(76, 29);
            this.label2.Text = "计划号";
            // 
            // txtJhh
            // 
            this.txtJhh.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtJhh.Location = new System.Drawing.Point(74, 53);
            this.txtJhh.Name = "txtJhh";
            this.txtJhh.Size = new System.Drawing.Size(90, 29);
            this.txtJhh.TabIndex = 7;
            this.txtJhh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtJhh_KeyUp);
            // 
            // btnDown
            // 
            this.btnDown.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnDown.Location = new System.Drawing.Point(169, 53);
            this.btnDown.Name = "btnDown";
            this.btnDown.Size = new System.Drawing.Size(68, 32);
            this.btnDown.TabIndex = 5;
            this.btnDown.Text = "下载";
            this.btnDown.Click += new System.EventHandler(this.btnDown_Click);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(3, 98);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(76, 29);
            this.label1.Text = "入计划";
            // 
            // lblRJhs
            // 
            this.lblRJhs.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblRJhs.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblRJhs.Location = new System.Drawing.Point(74, 98);
            this.lblRJhs.Name = "lblRJhs";
            this.lblRJhs.Size = new System.Drawing.Size(163, 29);
            this.lblRJhs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(4, 130);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(76, 29);
            this.label4.Text = "入材料";
            // 
            // lblRCls
            // 
            this.lblRCls.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblRCls.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblRCls.Location = new System.Drawing.Point(74, 130);
            this.lblRCls.Name = "lblRCls";
            this.lblRCls.Size = new System.Drawing.Size(163, 29);
            this.lblRCls.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblZt
            // 
            this.lblZt.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblZt.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblZt.Location = new System.Drawing.Point(74, 233);
            this.lblZt.Name = "lblZt";
            this.lblZt.Size = new System.Drawing.Size(163, 29);
            this.lblZt.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label11
            // 
            this.label11.BackColor = System.Drawing.SystemColors.Desktop;
            this.label11.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label11.ForeColor = System.Drawing.Color.Yellow;
            this.label11.Location = new System.Drawing.Point(4, 233);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(76, 29);
            this.label11.Text = "状态";
            // 
            // lblCCls
            // 
            this.lblCCls.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblCCls.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblCCls.Location = new System.Drawing.Point(74, 194);
            this.lblCCls.Name = "lblCCls";
            this.lblCCls.Size = new System.Drawing.Size(163, 29);
            this.lblCCls.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblCJhs
            // 
            this.lblCJhs.BackColor = System.Drawing.SystemColors.ControlLight;
            this.lblCJhs.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblCJhs.Location = new System.Drawing.Point(74, 162);
            this.lblCJhs.Name = "lblCJhs";
            this.lblCJhs.Size = new System.Drawing.Size(163, 29);
            this.lblCJhs.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // label7
            // 
            this.label7.BackColor = System.Drawing.SystemColors.Desktop;
            this.label7.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label7.ForeColor = System.Drawing.Color.Yellow;
            this.label7.Location = new System.Drawing.Point(4, 162);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(76, 29);
            this.label7.Text = "出计划";
            // 
            // label8
            // 
            this.label8.BackColor = System.Drawing.SystemColors.Desktop;
            this.label8.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label8.ForeColor = System.Drawing.Color.Yellow;
            this.label8.Location = new System.Drawing.Point(4, 194);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(76, 29);
            this.label8.Text = "出材料";
            // 
            // frmReceive
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.btnDown);
            this.Controls.Add(this.lblRJhs);
            this.Controls.Add(this.lblCJhs);
            this.Controls.Add(this.lblCCls);
            this.Controls.Add(this.txtJhh);
            this.Controls.Add(this.lblZt);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.lblRCls);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.picCon);
            this.Controls.Add(this.lblTitle);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label4);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmReceive";
            this.Text = "frmReceive";
            this.Load += new System.EventHandler(this.frmReceive_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmReceive_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.PictureBox picCon;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJhh;
        private System.Windows.Forms.Button btnDown;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblRJhs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblRCls;
        private System.Windows.Forms.Label lblZt;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label lblCCls;
        private System.Windows.Forms.Label lblCJhs;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
    }
}