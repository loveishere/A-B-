namespace hStore.Forms.InResearch
{
    partial class frmInPlanZF
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
            this.dgZf = new System.Windows.Forms.DataGrid();
            this.txtInfo = new System.Windows.Forms.TextBox();
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
            this.label1.Text = "入库计划－准发";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dgZf
            // 
            this.dgZf.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgZf.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgZf.Location = new System.Drawing.Point(0, 55);
            this.dgZf.Name = "dgZf";
            this.dgZf.RowHeadersVisible = false;
            this.dgZf.Size = new System.Drawing.Size(240, 260);
            this.dgZf.TabIndex = 2;
            this.dgZf.CurrentCellChanged += new System.EventHandler(this.dgZf_CurrentCellChanged);
            this.dgZf.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgZf_KeyUp);
            // 
            // txtInfo
            // 
            this.txtInfo.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtInfo.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtInfo.Location = new System.Drawing.Point(0, 29);
            this.txtInfo.Name = "txtInfo";
            this.txtInfo.Size = new System.Drawing.Size(240, 26);
            this.txtInfo.TabIndex = 3;
            this.txtInfo.Text = "计划号：";
            // 
            // frmInPlanZF
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.txtInfo);
            this.Controls.Add(this.dgZf);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmInPlanZF";
            this.Text = "frmInPlanZF";
            this.Load += new System.EventHandler(this.frmInPlanZF_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.frmInPlanZF_Paint);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmInPlanZF_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dgZf;
        private System.Windows.Forms.TextBox txtInfo;
    }
}