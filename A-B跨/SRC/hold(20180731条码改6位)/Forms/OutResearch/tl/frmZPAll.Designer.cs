namespace hStore.Forms.OutResearch.tl
{
    partial class frmZPAll
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(frmZPAll));
            this.label1 = new System.Windows.Forms.Label();
            this.txtZph = new System.Windows.Forms.TextBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.dgTroop = new System.Windows.Forms.DataGrid();
            this.lblMsg = new System.Windows.Forms.Label();
            this.picMsg = new System.Windows.Forms.PictureBox();
            this.picCon = new System.Windows.Forms.PictureBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(2, 7);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 26);
            this.label1.Text = "组批号";
            // 
            // txtZph
            // 
            this.txtZph.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtZph.Location = new System.Drawing.Point(63, 7);
            this.txtZph.Name = "txtZph";
            this.txtZph.Size = new System.Drawing.Size(95, 26);
            this.txtZph.TabIndex = 1;
            // 
            // btnRefresh
            // 
            this.btnRefresh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnRefresh.Location = new System.Drawing.Point(161, 3);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(76, 32);
            this.btnRefresh.TabIndex = 2;
            this.btnRefresh.Text = "申请";
            this.btnRefresh.Click += new System.EventHandler(this.btnRefresh_Click);
            // 
            // dgTroop
            // 
            this.dgTroop.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgTroop.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgTroop.Location = new System.Drawing.Point(0, 36);
            this.dgTroop.Name = "dgTroop";
            this.dgTroop.RowHeadersVisible = false;
            this.dgTroop.Size = new System.Drawing.Size(240, 255);
            this.dgTroop.TabIndex = 3;
            this.dgTroop.CurrentCellChanged += new System.EventHandler(this.dgTroop_CurrentCellChanged);
            this.dgTroop.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgTroop_KeyUp);
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
            // frmZPAll
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
            this.Controls.Add(this.dgTroop);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.txtZph);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmZPAll";
            this.Text = "frmZPAll";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmZPAll_KeyUp);
            this.Load += new System.EventHandler(this.frmZPAll_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtZph;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.DataGrid dgTroop;
        private System.Windows.Forms.Label lblMsg;
        private System.Windows.Forms.PictureBox picMsg;
        private System.Windows.Forms.PictureBox picCon;
    }
}