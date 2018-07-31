namespace hStore.Forms.InResearch
{
    partial class frmInPlan
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
            this.dgPlan = new System.Windows.Forms.DataGrid();
            this.label2 = new System.Windows.Forms.Label();
            this.txtZjs = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.txtWc = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtJhh = new System.Windows.Forms.TextBox();
            this.btnResearch = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 29);
            this.label1.Text = "入库计划查询";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgPlan
            // 
            this.dgPlan.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgPlan.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgPlan.Location = new System.Drawing.Point(0, 56);
            this.dgPlan.Name = "dgPlan";
            this.dgPlan.RowHeadersVisible = false;
            this.dgPlan.Size = new System.Drawing.Size(240, 206);
            this.dgPlan.TabIndex = 1;
            this.dgPlan.CurrentCellChanged += new System.EventHandler(this.dgPlan_CurrentCellChanged);
            this.dgPlan.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgPlan_KeyUp);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(0, 271);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(61, 26);
            this.label2.Text = "总件数";
            // 
            // txtZjs
            // 
            this.txtZjs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtZjs.Location = new System.Drawing.Point(61, 271);
            this.txtZjs.Name = "txtZjs";
            this.txtZjs.Size = new System.Drawing.Size(68, 26);
            this.txtZjs.TabIndex = 3;
            this.txtZjs.Text = "30760";
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(129, 271);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 26);
            this.label3.Text = "完成";
            // 
            // txtWc
            // 
            this.txtWc.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWc.Location = new System.Drawing.Point(178, 271);
            this.txtWc.Name = "txtWc";
            this.txtWc.Size = new System.Drawing.Size(62, 26);
            this.txtWc.TabIndex = 6;
            this.txtWc.Text = "24000";
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(0, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(61, 26);
            this.label4.Text = "计划号";
            // 
            // txtJhh
            // 
            this.txtJhh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtJhh.Location = new System.Drawing.Point(61, 29);
            this.txtJhh.Name = "txtJhh";
            this.txtJhh.Size = new System.Drawing.Size(114, 26);
            this.txtJhh.TabIndex = 11;
            // 
            // btnResearch
            // 
            this.btnResearch.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnResearch.Location = new System.Drawing.Point(179, 29);
            this.btnResearch.Name = "btnResearch";
            this.btnResearch.Size = new System.Drawing.Size(59, 26);
            this.btnResearch.TabIndex = 12;
            this.btnResearch.Text = "查找";
            this.btnResearch.Click += new System.EventHandler(this.btnResearch_Click);
            // 
            // frmInPlan
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.btnResearch);
            this.Controls.Add(this.txtJhh);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtWc);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.txtZjs);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.dgPlan);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInPlan";
            this.Text = "frmInPlan";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmInPlan_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmInPlan_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmInPlan_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dgPlan;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtZjs;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtWc;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtJhh;
        private System.Windows.Forms.Button btnResearch;
    }
}