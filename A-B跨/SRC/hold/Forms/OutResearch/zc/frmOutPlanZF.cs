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
    public partial class frmOutPlanZF : Form
    {
        private DataTable dtZf;
        private int curRow;


        public frmOutPlanZF()
        {
            InitializeComponent();
        }

        private void frmOutPlanZF_Load(object sender, EventArgs e)
        {
            cboCLType.SelectedIndex = 0;
            Global.storage.cltype = 0;

            switch (Global.storage.expflag)
            {
                case "tl":
                    lblTitle.Text = "铁路出厂";
                    txtInfo.Text = "提单号：" + Global.storage.jhh;
                    DataTable dt= Storage.GetExpZF(Global.storage.jhh,Global.storage.make, -1, "");
                    if (dt.Rows.Count > 0)
                    {
                        txtInfo2.Text = "件数："+ dt.Rows[0]["JS"].ToString() +"重量："+dt.Rows[0]["MZ"].ToString();
                    }
                    dt.Dispose();
                    break;
                case "zc":
                    lblTitle.Text = "装船出厂";
                    txtInfo.Text = "提单号：" + Global.storage.jhh;
                    if (Global.storage.gdh != "")
                    {
                        txtInfo2.Text = "关单号：" + Global.storage.gdh;
                    }
                    break;
                case "zk":
                    lblTitle.Text = "转库出库";
                    txtInfo.Text = "计划号：" + Global.storage.jhh;
                    txtInfo2.Text = "入库别：" + Global.storage.jhdd;
                    break;
                case "zt":
                    lblTitle.Text = "自提出厂";
                    txtInfo.Text = "提单号：" + Global.storage.jhh;
                    txtInfo2.Text = "收货单位：" + Global.storage.shdw;
                    break;
            }

        }

        private void frmOutPlanZF_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.storage.cltype = -1;
                Global.storage.pm = "";
                Global.storage.zfh = "";
                Global.storage.lh = "";
                Global.storage.hth = "";
                Global.storage.zph = "";

                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                Business.InvokeMethod(Global.frmCurrent, "LoadData", new object[] { });
                this.Close();
            }
        }

        private void dgZf_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgZf.CurrentCell.RowNumber;
            Global.storage.pm = dtZf.Rows[curRow]["PM"].ToString();
            txtWcjs.Text = dtZf.Rows[curRow]["WCJS"].ToString();
            txtWcmz.Text = dtZf.Rows[curRow]["WCMZ"].ToString();
            //string kw = dtZf.Rows[curRow]["KW"].ToString();
            string value = "";

            switch (cboCLType.SelectedIndex)
            {
                case 0:
                    Global.storage.zfh = dtZf.Rows[curRow]["ZFH"].ToString();
                    value = Global.storage.zfh;
                    break;
                case 1:
                    Global.storage.lh = dtZf.Rows[curRow]["LH"].ToString();
                    value = Global.storage.lh;
                    break;
                case 2:
                    Global.storage.hth = dtZf.Rows[curRow]["HTH"].ToString();
                    value = Global.storage.hth;
                    break;
                case 3:
                    Global.storage.zph = dtZf.Rows[curRow]["ZPH"].ToString();
                    value = Global.storage.zph;
                    break;
                default:
                    break;
            }

            if (Global.storage.expflag == "tl")
            {
                DataTable dt = Storage.GetExpZF(Global.storage.jhh,Global.storage.make, Global.storage.cltype, value);
                if (dt.Rows.Count > 0)
                {
                    txtInfo2.Text = "件数：" + dt.Rows[0]["JS"].ToString() + "重量：" + dt.Rows[0]["MZ"].ToString();
                }
                dt.Dispose();
            }
            dgZf.Select(dgZf.CurrentCell.RowNumber);
        }

        private void dgZf_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    frmOutPlanCL frm = new frmOutPlanCL();
                    Global.frmCurrent = frm;
                    frm.Owner = this;
                    frm.Show();
                    this.Hide();
                }
            }
        }

        public void LoadData()
        {
            Global.storage.cltype = cboCLType.SelectedIndex;
            DataGridTableStyle zfStyle = new DataGridTableStyle();

            switch (Global.storage.cltype)
            {
                case 0:
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "ZFH", "准发号", 90, ""));
                    break;
                case 1:
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "LH", "炉号", 60, ""));
                    break;
                case 2:
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "HTH", "合同号", 90, ""));
                    break;
                case 3:
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(0, "ZPH", "扎批号", 60, ""));
                    break;
                default:
                    break;
            }

            dtZf = Storage.GetExpZF(Global.storage.jhh,Global.storage.make, Global.storage.cltype);
            curRow = -1;
            txtWcjs.Text = "";
            txtWcmz.Text = "";
            dgZf.DataSource = dtZf;
            dgZf.TableStyles.Clear();

            zfStyle.MappingName = dtZf.TableName;

            switch (Global.storage.expflag)
            {
                case "tl":
                    //zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "GG", "规格", 170, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYMZ", "毛重", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYJZ", "净重", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYJS", "件数", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(5, "WCJS", "完件", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(6, "PM", "品名", 80, ""));
                    break;
                case"zc":
                    //zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "GG", "规格", 170, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYJS", "件数", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(3, "WCJS", "完件", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYMZ", "毛重", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(5, "SYJZ", "净重", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(6, "PM", "品名", 80, ""));
                    break;
                case "zk":
                    //zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 70, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "SYMZ", "毛重", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(2, "SYJZ", "净重", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(3, "GG", "规格", 170, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYJS", "件数", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(5, "WCJS", "完件", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(6, "PM", "品名", 80, ""));
                    break;
                case "zt":
                    //zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "KW", "库位", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(1, "SYJS", "件数", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(2, "WCJS", "完件", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(3, "SYMZ", "毛重", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(4, "SYJZ", "净重", 50, ""));
                    zfStyle.GridColumnStyles.Add(new ColumnStyle(5, "PM", "品名", 70, ""));
                    break;
            }




            dgZf.TableStyles.Add(zfStyle);
        }

        private void cboCLType_SelectedIndexChanged(object sender, EventArgs e)
        {
            LoadData();
        }

        private void frmOutPlanZF_Paint(object sender, PaintEventArgs e)
        {
            if (!ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
            }
        }
    }
}