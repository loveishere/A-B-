using System;

using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms
{
    public partial class frmWtd : Form
    {
        private DataTable dtWtd;
        private int curRow;

        public frmWtd()
        {
            InitializeComponent();
        }

        private void frmWtd_Load(object sender, EventArgs e)
        {
            dtWtd = Storage.GetWtd(Global.sKb);
            dgWtd.DataSource = dtWtd;


            dgWtd.TableStyles.Clear();

            DataGridTableStyle wtdStyle = new DataGridTableStyle();
            wtdStyle.MappingName = dtWtd.TableName;
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            wtdStyle.GridColumnStyles.Add(new ColumnStyle(0, "wtdh", "委托单", 90, "", cellEvent));
            wtdStyle.GridColumnStyles.Add(new ColumnStyle(1, "hpmc", "货名", 110, "", cellEvent));
            wtdStyle.GridColumnStyles.Add(new ColumnStyle(2, "zjs", "件数", 90, "", cellEvent));
            wtdStyle.GridColumnStyles.Add(new ColumnStyle(3, "zzl", "重量", 90, "", cellEvent));

            dgWtd.TableStyles.Add(wtdStyle);
            curRow = -1;

        }

        private void frmWtd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                Storage.ClearZF2KW();
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void dgWtd_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgWtd.CurrentCell.RowNumber;
            dgWtd.Select(dgWtd.CurrentCell.RowNumber);
        }

        public void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgWtd.CurrentCell.RowNumber == e.Row && curRow != -1)
                e.MeetsCriteria = false;
            else
            {
                bool bval = Convert.ToBoolean(dtWtd.Rows[e.Row]["CRFLAG"]);
                if (bval)
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Cyan;
                    e.ForeColor = SystemColors.WindowText;
                }
                else
                {
                    e.MeetsCriteria = true;
                    e.BackColor = SystemColors.Window;
                    e.ForeColor = SystemColors.WindowText;
                }

            }
        }

        private void doOper()
        {
            if (curRow >= 0)
            {
                string sjfs = dtWtd.Rows[curRow]["SJFS"].ToString();
                string wtdh = dtWtd.Rows[curRow]["WTDH"].ToString();
                string pm = dtWtd.Rows[curRow]["HPMC"].ToString();
                bool crflag = Convert.ToBoolean(dtWtd.Rows[curRow]["CRFLAG"]);
                Global.storage.jhh = wtdh;
                Global.storage.sjfs = sjfs;
                Global.storage.pm = pm;
                if (crflag)//入库
                {
                    frmWtdIn frm = new frmWtdIn();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
                else//出库
                {
                    frmWtdOut frm = new frmWtdOut();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }

            }
        }

        private void dgWtd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.F24)
            {
                doOper();
            }

        }

        private void frmWtd_Paint(object sender, PaintEventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }

    }
}