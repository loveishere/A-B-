namespace hStore.Forms
{
    partial class frmDgv2
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

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.mainMenu1 = new System.Windows.Forms.MainMenu();
            this.label1 = new System.Windows.Forms.Label();
            this.dataGrid2 = new System.Windows.Forms.DataGrid();
            this.txtClh = new System.Windows.Forms.TextBox();
            this.lblClh = new System.Windows.Forms.Label();
            this.lstClh = new System.Windows.Forms.ListBox();
            this.textBoxKJH = new System.Windows.Forms.TextBox();
            this.downLoad = new System.Windows.Forms.Button();
            this.label5 = new System.Windows.Forms.Label();
            this.txtJhh = new System.Windows.Forms.TextBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.BackColor = System.Drawing.SystemColors.Info;
            this.label1.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.label1.ForeColor = System.Drawing.Color.Red;
            this.label1.Location = new System.Drawing.Point(0, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(240, 25);
            this.label1.Text = "板坯入库";
            this.label1.TextAlign = System.Drawing.ContentAlignment.TopCenter;
            // 
            // dataGrid2
            // 
            this.dataGrid2.BackgroundColor = System.Drawing.SystemColors.Control;
            this.dataGrid2.ColumnHeadersVisible = false;
            this.dataGrid2.Font = new System.Drawing.Font("Arial", 18F, System.Drawing.FontStyle.Regular);
            this.dataGrid2.Location = new System.Drawing.Point(0, 90);
            this.dataGrid2.Name = "dataGrid2";
            this.dataGrid2.RowHeadersVisible = false;
            this.dataGrid2.Size = new System.Drawing.Size(240, 225);
            this.dataGrid2.TabIndex = 0;
            this.dataGrid2.DoubleClick += new System.EventHandler(this.dataGrid2_DoubleClick);
            this.dataGrid2.KeyUp += new System.Windows.Forms.KeyEventHandler(this.dataGrid2_KeyUp);
            this.dataGrid2.Click += new System.EventHandler(this.dataGrid2_Click);
            // 
            // txtClh
            // 
            this.txtClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtClh.Location = new System.Drawing.Point(79, 90);
            this.txtClh.Name = "txtClh";
            this.txtClh.Size = new System.Drawing.Size(161, 26);
            this.txtClh.TabIndex = 22;
            this.txtClh.Visible = false;
            this.txtClh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtClh_KeyUp);
            // 
            // lblClh
            // 
            this.lblClh.BackColor = System.Drawing.SystemColors.Desktop;
            this.lblClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lblClh.ForeColor = System.Drawing.Color.Yellow;
            this.lblClh.Location = new System.Drawing.Point(-1, 90);
            this.lblClh.Name = "lblClh";
            this.lblClh.Size = new System.Drawing.Size(82, 26);
            this.lblClh.Text = "材料号";
            this.lblClh.Visible = false;
            // 
            // lstClh
            // 
            this.lstClh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.lstClh.Location = new System.Drawing.Point(-1, 116);
            this.lstClh.Name = "lstClh";
            this.lstClh.Size = new System.Drawing.Size(240, 192);
            this.lstClh.TabIndex = 24;
            this.lstClh.Visible = false;
            this.lstClh.SelectedIndexChanged += new System.EventHandler(this.lstClh_SelectedIndexChanged);
            // 
            // textBoxKJH
            // 
            this.textBoxKJH.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.textBoxKJH.Location = new System.Drawing.Point(2, 54);
            this.textBoxKJH.Name = "textBoxKJH";
            this.textBoxKJH.ReadOnly = true;
            this.textBoxKJH.Size = new System.Drawing.Size(61, 26);
            this.textBoxKJH.TabIndex = 25;
            // 
            // downLoad
            // 
            this.downLoad.BackColor = System.Drawing.SystemColors.Desktop;
            this.downLoad.ForeColor = System.Drawing.Color.Yellow;
            this.downLoad.Location = new System.Drawing.Point(172, 25);
            this.downLoad.Name = "downLoad";
            this.downLoad.Size = new System.Drawing.Size(67, 26);
            this.downLoad.TabIndex = 27;
            this.downLoad.Text = "下载";
            this.downLoad.Click += new System.EventHandler(this.downLoad_Click);
            // 
            // label5
            // 
            this.label5.Location = new System.Drawing.Point(98, 54);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(100, 20);
            // 
            // txtJhh
            // 
            this.txtJhh.Font = new System.Drawing.Font("Tahoma", 12F, System.Drawing.FontStyle.Regular);
            this.txtJhh.Location = new System.Drawing.Point(3, 27);
            this.txtJhh.Name = "txtJhh";
            this.txtJhh.Size = new System.Drawing.Size(167, 26);
            this.txtJhh.TabIndex = 30;
            this.txtJhh.KeyUp += new System.Windows.Forms.KeyEventHandler(this.txtJhh_KeyUp);
            // 
            // frmDgv2
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(240, 315);
            this.ControlBox = false;
            this.Controls.Add(this.txtJhh);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.downLoad);
            this.Controls.Add(this.textBoxKJH);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtClh);
            this.Controls.Add(this.lblClh);
            this.Controls.Add(this.lstClh);
            this.Controls.Add(this.dataGrid2);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "frmDgv2";
            this.Text = "frmDgv2";
            this.Load += new System.EventHandler(this.frmDgv2_Load);
            this.Activated += new System.EventHandler(this.frmDgv2_Activated);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.frmDgv2_KeyUp);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.DataGrid dataGrid2;
        private System.Windows.Forms.TextBox txtClh;
        private System.Windows.Forms.Label lblClh;
        private System.Windows.Forms.ListBox lstClh;
        private System.Windows.Forms.TextBox textBoxKJH;
        private System.Windows.Forms.Button downLoad;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.TextBox txtJhh;

    }
}