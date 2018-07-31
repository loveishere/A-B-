namespace hStore.Forms
{
    partial class frmQuality
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem();
            System.Windows.Forms.ListViewItem listViewItem6 = new System.Windows.Forms.ListViewItem();
            this.lblPdt = new System.Windows.Forms.Label();
            this.lstQa = new System.Windows.Forms.ListView();
            this.lblGG = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblPdt
            // 
            this.lblPdt.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblPdt.Location = new System.Drawing.Point(0, 0);
            this.lblPdt.Name = "lblPdt";
            this.lblPdt.Size = new System.Drawing.Size(240, 40);
            this.lblPdt.Text = "材料号：\r\n品名：";
            // 
            // lstQa
            // 
            this.lstQa.BackColor = System.Drawing.Color.PaleTurquoise;
            this.lstQa.CheckBoxes = true;
            this.lstQa.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lstQa.FullRowSelect = true;
            listViewItem1.BackColor = System.Drawing.Color.PaleTurquoise;
            listViewItem1.Text = "货物生锈、油污";
            listViewItem2.BackColor = System.Drawing.Color.PaleTurquoise;
            listViewItem2.Text = "包装轻微撞击擦痕";
            listViewItem3.BackColor = System.Drawing.Color.PaleTurquoise;
            listViewItem3.Text = "包装严重撞击擦痕";
            listViewItem4.BackColor = System.Drawing.Color.PaleTurquoise;
            listViewItem4.Text = "包装破损";
            listViewItem5.BackColor = System.Drawing.Color.PaleTurquoise;
            listViewItem5.Text = "产品变形";
            listViewItem6.BackColor = System.Drawing.Color.PaleTurquoise;
            listViewItem6.Text = "货物退回发货点";
            this.lstQa.Items.Add(listViewItem1);
            this.lstQa.Items.Add(listViewItem2);
            this.lstQa.Items.Add(listViewItem3);
            this.lstQa.Items.Add(listViewItem4);
            this.lstQa.Items.Add(listViewItem5);
            this.lstQa.Items.Add(listViewItem6);
            this.lstQa.Location = new System.Drawing.Point(0, 40);
            this.lstQa.Name = "lstQa";
            this.lstQa.Size = new System.Drawing.Size(240, 275);
            this.lstQa.TabIndex = 5;
            this.lstQa.View = System.Windows.Forms.View.List;
            this.lstQa.SelectedIndexChanged += new System.EventHandler(this.lstQa_SelectedIndexChanged);
            this.lstQa.KeyUp += new System.Windows.Forms.KeyEventHandler(this.lstQa_KeyUp);
            // 
            // lblGG
            // 
            this.lblGG.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblGG.Location = new System.Drawing.Point(0, 40);
            this.lblGG.Name = "lblGG";
            this.lblGG.Size = new System.Drawing.Size(240, 20);
            // 
            // frmQuality
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.lstQa);
            this.Controls.Add(this.lblGG);
            this.Controls.Add(this.lblPdt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmQuality";
            this.Text = "frmQuality";
            this.Load += new System.EventHandler(this.frmQuality_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmQuality_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblPdt;
        private System.Windows.Forms.ListView lstQa;
        private System.Windows.Forms.Label lblGG;
    }
}