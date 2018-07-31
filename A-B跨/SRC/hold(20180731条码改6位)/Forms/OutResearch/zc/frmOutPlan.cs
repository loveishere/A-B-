using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;

namespace hStore.Forms.OutResearch.zc
{
    public partial class frmOutPlan : Form
    {
        private DataTable dtTd;
        private int curRow;

        public frmOutPlan()
        {
            InitializeComponent();
        }

        private void frmOutPlan_Load(object sender, EventArgs e)
        {

            lblInfo.Text = "船批号：" + Global.storage.cph + "\r\n船名：" + Global.storage.cm;
            txtGdh.Text = "";

            LoadData();
        }

        public void LoadData()
        {
            dtTd = Storage.GetExpPlanZC(Global.sKb,Global.storage.cph,Global.storage.cm,txtGdh.Text);
            dgTd.DataSource = dtTd;
            curRow = -1;
            txtWcjs.Text = "";
            txtWcmz.Text = "";

            dgTd.TableStyles.Clear();

            DataGridTableStyle tdStyle = new DataGridTableStyle();
            tdStyle.MappingName = dtTd.TableName;

            tdStyle.GridColumnStyles.Add(new ColumnStyle(0, "GDH", "关单号", 60, ""));
            tdStyle.GridColumnStyles.Add(new ColumnStyle(1, "JHH", "提单号", 120, ""));
            tdStyle.GridColumnStyles.Add(new ColumnStyle(2, "JHDD", "到港", 120, ""));
            tdStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYJS", "件数", 70, ""));
            tdStyle.GridColumnStyles.Add(new ColumnStyle(4, "WCJS", "完成件数", 70, ""));
            tdStyle.GridColumnStyles.Add(new ColumnStyle(5, "SYJZ", "净重", 70, ""));
            tdStyle.GridColumnStyles.Add(new ColumnStyle(6, "SYMZ", "毛重", 70, ""));

            dgTd.TableStyles.Add(tdStyle);
        }

        private void frmOutPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.make = "";
                Global.storage.jhh = "";
                Global.storage.gdh = "";

                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
        }

        private void dgTd_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgTd.CurrentCell.RowNumber;
            txtWcjs.Text = dtTd.Rows[curRow]["WCJS"].ToString();
            txtWcmz.Text = dtTd.Rows[curRow]["WCMZ"].ToString();
            Global.storage.make = dtTd.Rows[curRow]["Make"].ToString();
            Global.storage.jhh = dtTd.Rows[curRow]["JHH"].ToString();
            Global.storage.gdh = dtTd.Rows[curRow]["GDH"].ToString();
            dgTd.Select(dgTd.CurrentCell.RowNumber);
        }

        private void dgTd_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    frmOutPlanZF frm = new frmOutPlanZF();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
            }
            //if (e.KeyCode == Keys.F5)
            //{
            //    if (curRow >= 0)
            //    {
            //        string smake = dtTd.Rows[curRow]["Make"].ToString();
            //        string sjhh = dtTd.Rows[curRow]["JHH"].ToString();
            //        frmMessage frmMessage = new frmMessage();
            //        frmMessage.ShowDialog("出库计划" + sjhh + "是否确认删除？", "提示", "确认", "取消");
            //        bool ret = frmMessage.ret;
            //        frmMessage.Dispose();
            //        if (ret)
            //        {
            //            Storage.DeleteExpPlan(sjhh,smake);

            //            LoadData();
            //        }
            //    }
            //}
            if (e.KeyCode == Keys.F21)
            {
                if (curRow >= 0)
                {
                    if (Global.sDebug == "False")
                    {
                        frmMessage frmMessage = new frmMessage();
                        frmMessage.ShowDialog("装船计划" + dtTd.Rows[curRow]["JHH"].ToString() + "是否优先？", "选择", "是", "否");
                        bool ret = frmMessage.ret;
                        if (ret)
                        {
                            string data = dtTd.Rows[curRow]["JHH"].ToString() + ";";
                            data += Global.sUserId + ";";
                            data += System.DateTime.Now.ToString("yyyyMMddHHmmss");

                            Business.SendText(Business.msg.Package("ZDUA12", data));//作业顺序调整
                        }
                        frmMessage.Dispose();
                    }
                }
            }
        }

        private void btnResearch_Click(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frmOutPlan_Paint(object sender, PaintEventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }
    }
}