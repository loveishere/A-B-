namespace hStore.Forms
{
    partial class frmApply
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmApply));
            this.label1 = new System.Windows.Forms.Label();
            this.btnApplyAll = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.txtJhh = new System.Windows.Forms.TextBox();
            this.btnApplyOne = new System.Windows.Forms.Button();
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
            this.label1.Text = "计划管理";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // btnApplyAll
            // 
            this.btnApplyAll.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnApplyAll.Location = new System.Drawing.Point(12, 45);
            this.btnApplyAll.Name = "btnApplyAll";
            this.btnApplyAll.Size = new System.Drawing.Size(217, 38);
            this.btnApplyAll.TabIndex = 1;
            this.btnApplyAll.Text = "计划下载";
            this.btnApplyAll.Click += new System.EventHandler(this.btnApplyAll_Click);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(9, 134);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(73, 29);
            this.label2.Text = "计划号";
            // 
            // txtJhh
            // 
            this.txtJhh.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.txtJhh.Location = new System.Drawing.Point(82, 134);
            this.txtJhh.Name = "txtJhh";
            this.txtJhh.Size = new System.Drawing.Size(134, 29);
            this.txtJhh.TabIndex = 3;
            // 
            // btnApplyOne
            // 
            this.btnApplyOne.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.btnApplyOne.Location = new System.Drawing.Point(67, 186);
            this.btnApplyOne.Name = "btnApplyOne";
            this.btnApplyOne.Size = new System.Drawing.Size(98, 38);
            this.btnApplyOne.TabIndex = 4;
            this.btnApplyOne.Text = "申请";
            this.btnApplyOne.Click += new System.EventHandler(this.btnApplyOne_Click);
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
            // frmApply
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
            this.Controls.Add(this.btnApplyOne);
            this.Controls.Add(this.txtJhh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnApplyAll);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmApply";
            this.Text = "frmApply";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmApply_KeyUp);
            this.Load += new System.EventHandler(this.frmApply_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button btnApplyAll;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtJhh;
        private System.Windows.Forms.Button btnApplyOne;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picMsg;
        private System.Windows.Forms.PictureBox picCon;
    }
}