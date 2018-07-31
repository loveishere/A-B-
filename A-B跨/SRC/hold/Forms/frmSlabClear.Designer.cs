namespace hStore.Forms
{
    partial class frmSlabClear
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
            this.txtClh = new System.Windows.Forms.TextBox();
            this.lstClh = new System.Windows.Forms.ListBox();
            this.lblClh = new System.Windows.Forms.Label();
            this.txtLayer = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.comboBoxRow = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Tahoma", 14F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 2);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 29);
            this.label1.Text = "板坯清盘库";
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
            this.dgClear.Location = new System.Drawing.Point(0, 96);
            this.dgClear.Name = "dgClear";
            this.dgClear.RowHeadersVisible = false;
            this.dgClear.Size = new System.Drawing.Size(240, 196);
            this.dgClear.TabIndex = 10;
            this.dgClear.CurrentCellChanged += new System.EventHandler(this.dgClear_CurrentCellChanged);
            this.dgClear.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dgClear_KeyUp);
            this.dgClear.Click += new System.EventHandler(this.dgClear_Click);
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(91, 32);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(149, 26);
            this.txtClh.TabIndex = 20;
            this.txtClh.Visible = false;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // lstClh
            // 
            this.lstClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lstClh.Location = new System.Drawing.Point(0, 96);
            this.lstClh.Name = "lstClh";
            this.lstClh.Size = new System.Drawing.Size(240, 192);
            this.lstClh.TabIndex = 21;
            this.lstClh.Visible = false;
            this.lstClh.SelectedIndexChanged += new System.EventHandler(this.lstClh_SelectedIndexChanged);
            // 
            // lblClh
            // 
            this.lblClh.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblClh.ForeColor = System.Drawing.Color.Yellow;
            this.lblClh.Location = new System.Drawing.Point(2, 32);
            this.lblClh.Name = "lblClh";
            this.lblClh.Size = new System.Drawing.Size(90, 26);
            this.lblClh.Text = "材料号";
            this.lblClh.Visible = false;
            // 
            // txtLayer
            // 
            this.txtLayer.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtLayer.Location = new System.Drawing.Point(3, 32);
            this.txtLayer.Name = "txtLayer";
            this.txtLayer.Size = new System.Drawing.Size(130, 26);
            this.txtLayer.TabIndex = 5;
            //this.txtLayer.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtLayer_KeyUp);
            // 
            // button1
            // 
            this.button1.BackColor = System.Drawing.SystemColors.Desktop;
            this.button1.ForeColor = System.Drawing.Color.Yellow;
            this.button1.Location = new System.Drawing.Point(136, 32);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(82, 26);
            this.button1.TabIndex = 28;
            this.button1.Text = "查询";
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // comboBoxRow
            // 
            this.comboBoxRow.Items.Add("1");
            this.comboBoxRow.Items.Add("2");
            this.comboBoxRow.Items.Add("3");
            this.comboBoxRow.Items.Add("4");
            this.comboBoxRow.Items.Add("5");
            this.comboBoxRow.Items.Add("6");
            this.comboBoxRow.Location = new System.Drawing.Point(0, 67);
            this.comboBoxRow.Name = "comboBoxRow";
            this.comboBoxRow.Size = new System.Drawing.Size(145, 23);
            this.comboBoxRow.TabIndex = 32;
            this.comboBoxRow.SelectedIndexChanged += new System.EventHandler(this.comboBoxRow_SelectedIndexChanged);
            // 
            // frmSlabClear
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(96F, 96F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoScroll = true;
            this.BackColor = System.Drawing.SystemColors.Window;
            this.ClientSize = new System.Drawing.Size(640, 480);
            this.ControlBox = false;
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.comboBoxRow);
            this.Controls.Add(this.lstClh);
            this.Controls.Add(this.dgClear);
            this.Controls.Add(this.picCon);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.lblClh);
            this.Controls.Add(this.txtLayer);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmSlabClear";
            this.Text = "frmClear";
            this.WindowState = System.Windows.Forms.FormWindowState.Maximized;
            this.Load += new System.EventHandler(this.frmClear_Load);
            this.Closed += new System.EventHandler(this.frmClear_Closed);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmClear_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox picCon;
        private System.Windows.Forms.DataGrid dgClear;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.ListBox lstClh;
        private System.Windows.Forms.Label lblClh;
        private System.Windows.Forms.TextBox txtLayer;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.ComboBox comboBoxRow;
    }
}