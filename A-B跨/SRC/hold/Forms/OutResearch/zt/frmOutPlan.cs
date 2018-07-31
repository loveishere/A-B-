using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;


namespace hStore.Forms.OutResearch.zt
{
    public partial class frmOutPlan : Form
    {

        private DataTable dtZt;
        private int curRow;

        public frmOutPlan()
        {
            InitializeComponent();
        }

        private void frmOutPlan_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        public void LoadData()
        {
            dtZt = Storage.GetExpPlanF(Global.sKb, txtJhh.Text, "zt");
            dgZt.DataSource = dtZt;
            txtWcjs.Text = "";
            txtWcmz.Text = "";

            dgZt.TableStyles.Clear();

            DataGridTableStyle ztStyle = new DataGridTableStyle();
            ztStyle.MappingName = dtZt.TableName;
            ztStyle.GridColumnStyles.Add(new ColumnStyle(0, "JHH", "提单号", 120, ""));
            ztStyle.GridColumnStyles.Add(new ColumnStyle(1, "SHDW", "收货单位", 120, ""));
            ztStyle.GridColumnStyles.Add(new ColumnStyle(2, "JS", "件数", 50, ""));
            ztStyle.GridColumnStyles.Add(new ColumnStyle(3, "WCJS", "完件", 50, ""));
            ztStyle.GridColumnStyles.Add(new ColumnStyle(4, "MZ", "毛重", 50, ""));
            ztStyle.GridColumnStyles.Add(new ColumnStyle(5, "JZ", "净重", 50, ""));


            dgZt.TableStyles.Add(ztStyle);
            curRow = -1;
        }

        private void frmOutPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.make = "";
                Global.storage.jhh = "";
                Global.storage.shdw = "";
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void dgZt_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgZt.CurrentCell.RowNumber;
            Global.storage.make = dtZt.Rows[curRow]["Make"].ToString();
            Global.storage.jhh = dtZt.Rows[curRow]["JHH"].ToString();
            Global.storage.shdw = dtZt.Rows[curRow]["SHDW"].ToString();
            txtWcjs.Text = dtZt.Rows[curRow]["JS"].ToString();
            txtWcmz.Text = dtZt.Rows[curRow]["WCJS"].ToString();
            dgZt.Select(dgZt.CurrentCell.RowNumber);
        }

        private void dgZt_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    hStore.Forms.OutResearch.zc.frmOutPlanZF frm = new hStore.Forms.OutResearch.zc.frmOutPlanZF();
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
            //        string smake = dtZt.Rows[curRow]["Make"].ToString();
            //        string sjhh = dtZt.Rows[curRow]["JHH"].ToString();
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
            //
            if (e.KeyCode == Keys.F21)
            {
                if (curRow >= 0)
                {
                    if (Global.sDebug == "False")
                    {
                        frmMessage frmMessage = new frmMessage();
                        frmMessage.ShowDialog("自提计划" + dtZt.Rows[curRow]["JHH"].ToString() + "是否优先？", "选择", "是", "否");
                        bool ret = frmMessage.ret;
                        if (ret)
                        {
                            string data = dtZt.Rows[curRow]["JHH"].ToString() + ";";
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

        private void txtJhh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtJhh.Text = txtJhh.Text.ToUpper();
                txtJhh.SelectionStart = txtJhh.Text.Length;

            }
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