namespace hStore.Forms.OutResearch.zt
{
    partial class frmOutPlanCL
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
            this.dgCl = new System.Windows.Forms.DataGrid();
            this.label1 = new System.Windows.Forms.Label();
            this.lblJhh = new System.Windows.Forms.Label();
            this.lblShdw = new System.Windows.Forms.Label();
            this.lblZfh = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // dgCl
            // 
            this.dgCl.BackgroundColor = System.Drawing.Color.White;
            this.dgCl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgCl.Location = new System.Drawing.Point(0, 90);
            this.dgCl.Name = "dgCl";
            this.dgCl.RowHeadersVisible = false;
            this.dgCl.Size = new System.Drawing.Size(240, 225);
            this.dgCl.TabIndex = 8;
            this.dgCl.CurrentCellChanged += new System.EventHandler(this.dgCl_CurrentCellChanged);
            this.dgCl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgCl_KeyUp);
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 29);
            this.label1.Text = "自提出厂－材料";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // lblJhh
            // 
            this.lblJhh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblJhh.Location = new System.Drawing.Point(0, 29);
            this.lblJhh.Name = "lblJhh";
            this.lblJhh.Size = new System.Drawing.Size(240, 20);
            this.lblJhh.Text = "计划号：";
            // 
            // lblShdw
            // 
            this.lblShdw.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblShdw.Location = new System.Drawing.Point(0, 49);
            this.lblShdw.Name = "lblShdw";
            this.lblShdw.Size = new System.Drawing.Size(240, 20);
            this.lblShdw.Text = "收货单位：";
            // 
            // lblZfh
            // 
            this.lblZfh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblZfh.Location = new System.Drawing.Point(0, 69);
            this.lblZfh.Name = "lblZfh";
            this.lblZfh.Size = new System.Drawing.Size(240, 20);
            this.lblZfh.Text = "合同号：";
            // 
            // frmOutPlanCL
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.lblZfh);
            this.Controls.Add(this.lblShdw);
            this.Controls.Add(this.lblJhh);
            this.Controls.Add(this.dgCl);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmOutPlanCL";
            this.Text = "frmOutPlanCL";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Closed += new System.EventHandler(this.frmOutPlanCL_Closed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmOutPlanCL_KeyUp);
            this.Load += new System.EventHandler(this.frmOutPlanCL_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGrid dgCl;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblJhh;
        private System.Windows.Forms.Label lblShdw;
        private System.Windows.Forms.Label lblZfh;
    }
}