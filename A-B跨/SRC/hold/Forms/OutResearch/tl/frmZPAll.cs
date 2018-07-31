using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace hStore.Forms.OutResearch.tl
{
    public partial class frmZPAll : Form
    {
        private delegate void setImage();
        private delegate void getDataSource();

        private DataTable dtTroop;
        private int curRow;
        public string m_zph;
        public string m_jhdd;
        public string m_js;
        public string m_mz;

        public frmZPAll()
        {
            InitializeComponent();
        }

        private void frmZPAll_Load(object sender, EventArgs e)
        {
            showConnect();
            LoadData();
        }

        public void LoadData()
        {
            string sql = "select C.ZPH,max(A.JHDD) JHDD, ";
            sql += "max(substring(convert(nchar(16),C.YHSJ,120),6,11)) YHSJ,";
            sql += "sum(case C.WCFlag when 2 then 0 else 1 end) JS,";
            sql += "sum(case C.WCFlag when 2 then 1 else 0 end) WCJS,";
            sql += "sum(C.MZ) MZ  ";
            sql += "from ExportStoragePlan A,ExportStorageOrder B, ExportStorageAcceptOrder C ";
            sql += "where A.JHH=B.JHH and B.ZFH=C.ZFH ";
            sql += "group by C.ZPH";

            dtTroop = App.ExecuteQuery(sql);

            dgTroop.DataSource = dtTroop;
            dgTroop.TableStyles.Clear();

            DataGridTableStyle troopStyle = new DataGridTableStyle();
            troopStyle.MappingName = dtTroop.TableName;

            troopStyle.GridColumnStyles.Add(new ColumnStyle(0, "ZPH", "组批号", 70, ""));
            troopStyle.GridColumnStyles.Add(new ColumnStyle(1, "JHDD", "到站", 100, ""));
            troopStyle.GridColumnStyles.Add(new ColumnStyle(2, "JS", "件数", 50, ""));
            troopStyle.GridColumnStyles.Add(new ColumnStyle(3, "WCJS", "完件", 50, ""));
            troopStyle.GridColumnStyles.Add(new ColumnStyle(4, "YHSJ", "要货时间", 80, ""));


            dgTroop.TableStyles.Add(troopStyle);
        }

        private void frmZPAll_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                App.LinkedListForm.RemoveLast();
                App.InvokeMethod(App.LinkedListForm.Last.Value, "LoadData", new object[] { });
                this.Close();
            }

        }

        private void dgTroop_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgTroop.CurrentCell.RowNumber;
            m_zph = dtTroop.Rows[curRow]["ZPH"].ToString();
            m_jhdd = dtTroop.Rows[curRow]["JHDD"].ToString();
            m_js = dtTroop.Rows[curRow]["JS"].ToString();
            m_mz = dtTroop.Rows[curRow]["MZ"].ToString();
            dgTroop.Select(dgTroop.CurrentCell.RowNumber);
        }

        private void dgTroop_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (curRow >= 0)
                {
                    frmOutZP frm = new frmOutZP();
                    frm.Show();
                    App.LinkedListForm.AddLast(frm);
                }
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            if (txtZph.Text != "")
            {
                App.cli.SendText(WCOM.Message.Package("ZDWX52",txtZph.Text +";"+App.sBb+";"+App.sKb));
            }
            else
            {
                App.cli.SendText(WCOM.Message.Package("ZDWX52", "ALL;" + App.sBb + ";" + App.sKb));
            }

            App.ExecuteNonQuery("delete from Troop");

            dtTroop = new DataTable();
            dtTroop.Columns.Add("ZPH");
            dtTroop.Columns.Add("JS");
            dtTroop.Columns.Add("DJS");
            dtTroop.Columns.Add("ZT");
            curRow = -1;
            dgTroop.DataSource = dtTroop;

            dgTroop.TableStyles.Clear();

            DataGridTableStyle downStyle = new DataGridTableStyle();
            downStyle.MappingName = dtTroop.TableName;

            downStyle.GridColumnStyles.Add(new ColumnStyle(0, "JHH", "计划号", 95, ""));
            downStyle.GridColumnStyles.Add(new ColumnStyle(1, "JS", "件数", 45, ""));
            downStyle.GridColumnStyles.Add(new ColumnStyle(2, "DJS", "下载", 45, ""));
            downStyle.GridColumnStyles.Add(new ColumnStyle(3, "ZT", "状态", 45, ""));

            dgTroop.TableStyles.Add(downStyle);

            btnRefresh.Text = "完成";
        }

        public void showConnect()
        {
            Bitmap bitmap = null;
            if (App.cli.IsConnected)
            {
                bitmap = new Bitmap(@"\SD-MMC Card\connect.bmp");
            }
            else
            {
                bitmap = new Bitmap(@"\SD-MMC Card\disconnect.bmp");
            }
            if (picCon.InvokeRequired)
            {
                picCon.Invoke(new setImage(showConnect), new object[] { });
            }
            else
            {
                picCon.Image = Image.FromHbitmap(bitmap.GetHbitmap());
            }
        }

        public void showMessage(string msg)
        {

        }

    }
}