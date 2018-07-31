using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using PsionTeklogix.Keyboard;
using PsionTeklogix.Keyboard.NoChording;


namespace hStore.Forms.OutResearch.zk
{
    public partial class frmOutPlan : Form
    {
        private DataTable dtZk;
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
            dtZk = Storage.GetExpPlanF(Global.sKb, txtJhh.Text, "zk");
            dgZk.DataSource = dtZk;
            txtWcjs.Text = "";
            txtWcmz.Text = "";
            curRow = -1;
            dgZk.TableStyles.Clear();

            DataGridTableStyle zkStyle = new DataGridTableStyle();
            zkStyle.MappingName = dtZk.TableName;
            zkStyle.GridColumnStyles.Add(new ColumnStyle(0, "JHH", "计划号", 120, ""));
            zkStyle.GridColumnStyles.Add(new ColumnStyle(1, "JHDD", "入库别", 60, ""));
            zkStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYJS", "件数", 50, ""));
            zkStyle.GridColumnStyles.Add(new ColumnStyle(3, "WCJS", "完件", 50, ""));
            zkStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYJZ", "净重", 50, ""));
            zkStyle.GridColumnStyles.Add(new ColumnStyle(5, "SYMZ", "毛重", 50, ""));


            dgZk.TableStyles.Add(zkStyle);
        }

        private void frmOutPlan_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.make = "";
                Global.storage.jhh = "";
                Global.storage.jhdd = "";
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }
        }

        private void dgZk_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgZk.CurrentCell.RowNumber;
            Global.storage.make = dtZk.Rows[curRow]["Make"].ToString();
            Global.storage.jhh = dtZk.Rows[curRow]["JHH"].ToString();
            Global.storage.jhdd = dtZk.Rows[curRow]["JHDD"].ToString();
            txtWcjs.Text = dtZk.Rows[curRow]["WCJS"].ToString();
            txtWcmz.Text = dtZk.Rows[curRow]["WCMZ"].ToString();
            dgZk.Select(dgZk.CurrentCell.RowNumber);
        }

        private void dgZk_KeyUp(object sender, KeyEventArgs e)
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
            //        string smake = dtZk.Rows[curRow]["Make"].ToString();
            //        string sjhh = dtZk.Rows[curRow]["JHH"].ToString();
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
                        frmMessage.ShowDialog("转库计划" + dtZk.Rows[curRow]["JHH"].ToString() + "是否优先？", "选择", "是", "否");
                        bool ret = frmMessage.ret;
                        if (ret)
                        {
                            string data = dtZk.Rows[curRow]["JHH"].ToString() + ";";
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