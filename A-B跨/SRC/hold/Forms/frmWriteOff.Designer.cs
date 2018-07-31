namespace hStore.Forms
{
    partial class frmWriteOff
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;
        private System.Windows.Forms.MainMenu mainMenu1;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
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
            this.picCon = new System.Windows.Forms.PictureBox();
            this.dgClear = new System.Windows.Forms.DataGrid();
            this.label4 = new System.Windows.Forms.Label();
            this.txtJs = new System.Windows.Forms.TextBox();
            this.txtKw = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblClh = new System.Windows.Forms.Label();
            this.txtClh = new System.Windows.Forms.TextBox();
            this.lstClh = new System.Windows.Forms.ListBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textHCH = new System.Windows.Forms.TextBox();
            this.lblZJ = new System.Windows.Forms.Label();
            this.textZJ = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Tahoma", 16F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 29);
            this.label1.Text = "入库销账";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // picCon
            // 
            this.picCon.BackColor = System.Drawing.SystemColors.Window;
            this.picCon.Location = new System.Drawing.Point(0, 292);
            this.picCon.Name = "picCon";
            this.picCon.Size = new System.Drawing.Size(23, 23);
            this.picCon.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            // 
            // dgClear
            // 
            this.dgClear.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgClear.Font = new System.Drawing.Font("Arial", 12F, System.Drawing.FontStyle.Regular);
            this.dgClear.Location = new System.Drawing.Point(0, 58);
            this.dgClear.Name = "dgClear";
            this.dgClear.RowHeadersVisible = false;
            this.dgClear.Size = new System.Drawing.Size(240, 205);
            this.dgClear.TabIndex = 10;
            this.dgClear.DoubleClick += new System.EventHandler(this.dgClear_DoubleClick);
            this.dgClear.CurrentCellChanged += new System.EventHandler(this.dgClear_CurrentCellChanged);
            this.dgClear.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgClear_KeyUp);
            this.dgClear.Click += new System.EventHandler(this.dgClear_Click);
            // 
            // label4
            // 
            this.label4.BackColor = System.Drawing.SystemColors.Desktop;
            this.label4.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label4.ForeColor = System.Drawing.Color.Yellow;
            this.label4.Location = new System.Drawing.Point(126, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(43, 26);
            this.label4.Text = "件数";
            // 
            // txtJs
            // 
            this.txtJs.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtJs.Location = new System.Drawing.Point(167, 29);
            this.txtJs.Name = "txtJs";
            this.txtJs.ReadOnly = true;
            this.txtJs.Size = new System.Drawing.Size(70, 26);
            this.txtJs.TabIndex = 13;
            // 
            // txtKw
            // 
            this.txtKw.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtKw.Location = new System.Drawing.Point(43, 29);
            this.txtKw.Name = "txtKw";
            this.txtKw.Size = new System.Drawing.Size(78, 26);
            this.txtKw.TabIndex = 5;
            this.txtKw.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtKw_KeyUp);
            // 
            // label3
            // 
            this.label3.BackColor = System.Drawing.SystemColors.Desktop;
            this.label3.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label3.ForeColor = System.Drawing.Color.Yellow;
            this.label3.Location = new System.Drawing.Point(0, 29);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(43, 26);
            this.label3.Text = "库位";
            // 
            // lblClh
            // 
            this.lblClh.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblClh.ForeColor = System.Drawing.Color.Yellow;
            this.lblClh.Location = new System.Drawing.Point(3, 29);
            this.lblClh.Name = "lblClh";
            this.lblClh.Size = new System.Drawing.Size(82, 26);
            this.lblClh.Text = "材料号";
            this.lblClh.Visible = false;
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(82, 29);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(155, 26);
            this.txtClh.TabIndex = 18;
            this.txtClh.Visible = false;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // lstClh
            // 
            this.lstClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lstClh.Location = new System.Drawing.Point(3, 61);
            this.lstClh.Name = "lstClh";
            this.lstClh.Size = new System.Drawing.Size(234, 192);
            this.lstClh.TabIndex = 19;
            this.lstClh.Visible = false;
            this.lstClh.SelectedIndexChanged += new System.EventHandler(this.lstClh_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(128)))), ((int)(((byte)(255)))));
            this.label2.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label2.ForeColor = System.Drawing.Color.Yellow;
            this.label2.Location = new System.Drawing.Point(0, 266);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(64, 26);
            this.label2.Text = "行车号";
            // 
            // textHCH
            // 
            this.textHCH.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.textHCH.Location = new System.Drawing.Point(66, 266);
            this.textHCH.Name = "textHCH";
            this.textHCH.Size = new System.Drawing.Size(56, 26);
            this.textHCH.TabIndex = 33;
            this.textHCH.KeyUp += new System.Windows.Forms.KeyEventHandler(this.textHCH_KeyUp);
            // 
            // lblZJ
            // 
            this.lblZJ.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblZJ.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblZJ.ForeColor = System.Drawing.Color.Yellow;
            this.lblZJ.Location = new System.Drawing.Point(122, 29);
            this.lblZJ.Name = "lblZJ";
            this.lblZJ.Size = new System.Drawing.Size(57, 26);
            this.lblZJ.Text = "质检";
            this.lblZJ.Visible = false;
            // 
            // textZJ
            // 
            this.textZJ.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.textZJ.Location = new System.Drawing.Point(180, 29);
            this.textZJ.Name = "textZJ";
            this.textZJ.Size = new System.Drawing.Size(50, 26);
            this.textZJ.TabIndex = 43;
            this.textZJ.Visible = false;
            this.textZJ.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtZJ_KeyUp);
            // 
            // frmWriteOff
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.textZJ);
            this.Controls.Add(this.lblZJ);
            this.Controls.Add(this.textHCH);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.lblClh);
            this.Controls.Add(this.txtJs);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.picCon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lstClh);
            this.Controls.Add(this.dgClear);
            this.Controls.Add(this.txtKw);
            this.Controls.Add(this.label3);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmWriteOff";
            this.Text = "frmWriteOff";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmWriteOff_Load);
            this.Closed += new System.EventHandler(this.frmWriteOff_Closed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmWriteOff_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picCon;
        private System.Windows.Forms.DataGrid dgClear;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.TextBox txtJs;
        private System.Windows.Forms.Label lblClh;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.ListBox lstClh;
        private System.Windows.Forms.TextBox txtKw;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textHCH;
        private System.Windows.Forms.Label lblZJ;
        private System.Windows.Forms.TextBox textZJ;
    }
}