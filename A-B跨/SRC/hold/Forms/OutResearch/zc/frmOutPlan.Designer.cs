namespace hStore.Forms.OutResearch.zc
{
    partial class frmOutPlan
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
            this.lblTitle = new System.Windows.Forms.Label();
            this.dgTd = new System.Windows.Forms.DataGrid();
            this.txtWcmz = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWcjs = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.btnResearch = new System.Windows.Forms.Button();
            this.txtGdh = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.lblInfo = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblTitle
            // 
            this.lblTitle.BackColor = System.Drawing.SystemColors.Info;
            this.lblTitle.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.lblTitle.ForeColor = System.Drawing.Color.Red;
            this.lblTitle.Location = new System.Drawing.Point(0, 0);
            this.lblTitle.Name = "lblTitle";
            this.lblTitle.Size = new System.Drawing.Size(240, 29);
            this.lblTitle.Text = "装船出厂－计划";
            this.lblTitle.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgTd
            // 
            this.dgTd.BackgroundColor = System.Drawing.Color.White;
            this.dgTd.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgTd.Location = new System.Drawing.Point(0, 99);
            this.dgTd.Name = "dgTd";
            this.dgTd.RowHeadersVisible = false;
            this.dgTd.Size = new System.Drawing.Size(240, 189);
            this.dgTd.TabIndex = 1;
            this.dgTd.CurrentCellChanged += new System.EventHandler(this.dgTd_CurrentCellChanged);
            this.dgTd.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgTd_KeyUp);
            // 
            // txtWcmz
            // 
            this.txtWcmz.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWcmz.Location = new System.Drawing.Point(164, 289);
            this.txtWcmz.Name = "txtWcmz";
            this.txtWcmz.Size = new System.Drawing.Size(72, 26);
            this.txtWcmz.TabIndex = 10;
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(118, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(46, 26);
            this.label3.Text = "完毛";
            // 
            // txtWcjs
            // 
            this.txtWcjs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWcjs.Location = new System.Drawing.Point(46, 289);
            this.txtWcjs.Name = "txtWcjs";
            this.txtWcjs.Size = new System.Drawing.Size(72, 26);
            this.txtWcjs.TabIndex = 9;
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(0, 289);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(46, 26);
            this.label2.Text = "完件";
            // 
            // btnResearch
            // 
            this.btnResearch.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnResearch.Location = new System.Drawing.Point(181, 72);
            this.btnResearch.Name = "btnResearch";
            this.btnResearch.Size = new System.Drawing.Size(59, 26);
            this.btnResearch.TabIndex = 16;
            this.btnResearch.Text = "查找";
            this.btnResearch.Click += new System.EventHandler(this.btnResearch_Click);
            // 
            // txtGdh
            // 
            this.txtGdh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtGdh.Location = new System.Drawing.Point(67, 72);
            this.txtGdh.Name = "txtGdh";
            this.txtGdh.Size = new System.Drawing.Size(114, 26);
            this.txtGdh.TabIndex = 15;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(2, 72);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(65, 26);
            this.label4.Text = "关单号";
            // 
            // lblInfo
            // 
            this.lblInfo.BackColor = System.Drawing.SystemColors.ActiveCaptionText;
            this.lblInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblInfo.Location = new System.Drawing.Point(0, 29);
            this.lblInfo.Name = "lblInfo";
            this.lblInfo.Size = new System.Drawing.Size(240, 40);
            this.lblInfo.Text = "船批号：";
            // 
            // frmOutPlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.lblInfo);
            this.Controls.Add(this.btnResearch);
            this.Controls.Add(this.txtGdh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWcmz);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtWcjs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgTd);
            this.Controls.Add(this.lblTitle);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutPlan";
            this.Text = "frmOutPlan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmOutPlan_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmOutPlan_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOutPlan_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label lblTitle;
        private System.Windows.Forms.DataGrid dgTd;
        private System.Windows.Forms.TextBox txtWcmz;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWcjs;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Button btnResearch;
        private System.Windows.Forms.TextBox txtGdh;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label lblInfo;
    }
}