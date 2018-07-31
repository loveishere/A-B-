namespace hStore.Forms
{
    partial class frmWtdIn
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
            this.txtWtdInf = new System.Windows.Forms.TextBox();
            this.dgWtdCl = new System.Windows.Forms.DataGrid();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // txtWtdInf
            // 
            this.txtWtdInf.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.txtWtdInf.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtWtdInf.Location = new System.Drawing.Point(88, 0);
            this.txtWtdInf.Multiline = true;
            this.txtWtdInf.Name = "txtWtdInf";
            this.txtWtdInf.Size = new System.Drawing.Size(149, 25);
            this.txtWtdInf.TabIndex = 1;
            this.txtWtdInf.Text = "W00001";
            // 
            // dgWtdCl
            // 
            this.dgWtdCl.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgWtdCl.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgWtdCl.Location = new System.Drawing.Point(0, 61);
            this.dgWtdCl.Name = "dgWtdCl";
            this.dgWtdCl.RowHeadersVisible = false;
            this.dgWtdCl.Size = new System.Drawing.Size(240, 254);
            this.dgWtdCl.TabIndex = 12;
            this.dgWtdCl.CurrentCellChanged += new System.EventHandler(this.dgWtdCl_CurrentCellChanged);
            this.dgWtdCl.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgWtdCl_KeyUp);
            // 
            // textBox1
            // 
            this.textBox1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.textBox1.Location = new System.Drawing.Point(88, 31);
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(149, 26);
            this.textBox1.TabIndex = 13;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.Location = new System.Drawing.Point(3, 32);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(68, 25);
            this.label1.Text = "材料号";
            // 
            // label2
            // 
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.Location = new System.Drawing.Point(3, 3);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(79, 25);
            this.label2.Text = "委托单号";
            // 
            // frmWtdIn
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.BackColor = System.Drawing.Color.WhiteSmoke;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.dgWtdCl);
            this.Controls.Add(this.txtWtdInf);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWtdIn";
            this.Text = "frmWtdIn";
            this.Load += new System.EventHandler(this.frmWtdIn_Load);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmWtdIn_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TextBox txtWtdInf;
        private System.Windows.Forms.DataGrid dgWtdCl;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
    }
}