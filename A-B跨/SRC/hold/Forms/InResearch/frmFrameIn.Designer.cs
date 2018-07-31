namespace hStore.Forms.InResearch
{
    partial class frmFrameIn
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
            this.txtKjh = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.txtClh = new System.Windows.Forms.TextBox();
            this.dgCl = new System.Windows.Forms.DataGrid();
            this.btnApply = new System.Windows.Forms.Button();
            this.lstCLH = new System.Windows.Forms.ListBox();
            this.applyTimer = new System.Windows.Forms.Timer();
            this.label3 = new System.Windows.Forms.Label();
            this.txtJs = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.txtMz = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Desktop;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Yellow;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(64, 26);
            this.label1.Text = "框架号";
            // 
            // txtKjh
            // 
            this.txtKjh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtKjh.Location = new System.Drawing.Point(64, 0);
            this.txtKjh.Name = "txtKjh";
            this.txtKjh.Size = new System.Drawing.Size(101, 26);
            this.txtKjh.TabIndex = 1;
            this.txtKjh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKjh_KeyUp);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.SystemColors.Desktop;
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(0, 27);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 26);
            this.label2.Text = "材料号";
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(64, 27);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(176, 26);
            this.txtClh.TabIndex = 5;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // dgCl
            // 
            this.dgCl.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgCl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgCl.Location = new System.Drawing.Point(0, 54);
            this.dgCl.Name = "dgCl";
            this.dgCl.RowHeadersVisible = false;
            this.dgCl.Size = new System.Drawing.Size(240, 235);
            this.dgCl.TabIndex = 6;
            this.dgCl.CurrentCellChanged += new System.EventHandler(this.dgCl_CurrentCellChanged);
            this.dgCl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgCl_KeyUp);
            // 
            // btnApply
            // 
            this.btnApply.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.btnApply.Location = new System.Drawing.Point(165, 0);
            this.btnApply.Name = "btnApply";
            this.btnApply.Size = new System.Drawing.Size(75, 26);
            this.btnApply.TabIndex = 2;
            this.btnApply.Text = "申请";
            this.btnApply.Click += new System.EventHandler(this.btnApply_Click);
            // 
            // lstCLH
            // 
            this.lstCLH.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lstCLH.Location = new System.Drawing.Point(60, 54);
            this.lstCLH.Name = "lstCLH";
            this.lstCLH.Size = new System.Drawing.Size(180, 192);
            this.lstCLH.TabIndex = 44;
            this.lstCLH.Visible = false;
            this.lstCLH.SelectedIndexChanged += new System.EventHandler(this.lstCLH_SelectedIndexChanged);
            // 
            // applyTimer
            // 
            this.applyTimer.Interval = 60000;
            this.applyTimer.Tick += new System.EventHandler(this.applyTimer_Tick);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(0, 289);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(49, 26);
            this.label3.Text = "框件";
            // 
            // txtJs
            // 
            this.txtJs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtJs.Location = new System.Drawing.Point(49, 289);
            this.txtJs.Name = "txtJs";
            this.txtJs.Size = new System.Drawing.Size(42, 26);
            this.txtJs.TabIndex = 49;
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(92, 289);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(44, 26);
            this.label4.Text = "框毛";
            // 
            // txtMz
            // 
            this.txtMz.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtMz.Location = new System.Drawing.Point(136, 289);
            this.txtMz.Name = "txtMz";
            this.txtMz.Size = new System.Drawing.Size(101, 26);
            this.txtMz.TabIndex = 52;
            // 
            // frmFrameIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.txtMz);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.txtJs);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lstCLH);
            this.Controls.Add(this.dgCl);
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.btnApply);
            this.Controls.Add(this.txtKjh);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmFrameIn";
            this.Text = "frmIn";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmFrameIn_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmFrameIn_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox txtKjh;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.DataGrid dgCl;
        private System.Windows.Forms.Button btnApply;
        private System.Windows.Forms.ListBox lstCLH;
        private System.Windows.Forms.Timer applyTimer;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox txtJs;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtMz;
    }
}