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
    public partial class frmSlabClear : Form
    {
        private DataTable dtClear;
        private int curRow = -1;
        private int rowCnt = 6;
        private string curKW = "";


        public frmSlabClear()
        {
            InitializeComponent();
        }

        private void frmClear_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                Global.frmCurrent = this.Owner;
                this.Owner.Show();
                this.Owner = null;
                this.Close();
            }

            if (e.KeyCode == Keys.F21)
            {
               SendClearResult();
            }

            //if (e.KeyCode == Keys.Tab)
            //{
            //    frmClear frm = new frmClear();//清盘库
            //    Global.frmCurrent = frm;
            //    frm.Owner = this;
            //    frm.Show();
            //    this.Hide();
            //}
        }

        private void SendClearResult()
        {
            if (dtClear.Rows.Count > 0)
            {
                string gram = Global.sUserId + ",";
                gram += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ",";
                gram += Global.sBb + ",";
                gram += Global.sZyq + ",";
                gram += System.DateTime.Now.ToString("yyyyMMddHHmmss") + ",";
                //int j = 0;
                string str = "";
                int layer = 0;
                int numCHL = 0;
                int k = 0;
                for (int i = 0; i < dtClear.Rows.Count; i++)
                { 
                    if (dtClear.Rows[i]["CLH"].ToString() != "")
                    {
                  
                       layer = Convert.ToInt32(dtClear.Rows[i]["CH"]);

                   
                        str += dtClear.Rows[i]["CLH"].ToString() + ",";
                       
                       str += layer.ToString("00") +",";
                       numCHL++;
                    }

                }

                if (str != "")
                {
                    gram += numCHL.ToString() + ",";
                    str += ",";//yt
                    str = str.Substring(0, str.Length - 1);
                    if (Global.sDebug == "False")
                    {
                        Business.SendText(Business.msg.Package("ZDUA16", gram + str));//清盘库材料信息
                    }
                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("发送完毕！", "提示", "确定");
                    frmMessage.Dispose();
                }

            }
        }

        private void scanner_ScanCompleteEvent(string text)
        {
            string barcode = text;

            if (barcode.StartsWith("L")) barcode = barcode.Substring(1, barcode.Length - 1);

            if (barcode.StartsWith("S")) barcode = barcode.Substring(1, barcode.Length - 1);//新日铁热卷

            barcode = barcode.Replace("-", "");//湛江冷卷

            bool bval = false;

            if (text.Length < 5) return;

            char[] chr = barcode.ToCharArray();

            for (int i = 0; i < chr.Length; i++)
            {
                if ((chr[i] < '0' || chr[i] > '9') && (chr[i] < 'A' || chr[i] > 'Z'))
                {
                    bval = true;
                    break;
                }
            }

            if (bval) return;

            if (text.Length == 7)
            {
                //addKw(text);
            }

            if (txtLayer.Text != "")
            {
                addCl(barcode);
            }

        }
        //材料号
        private void addCl(string clh)
        {
            bool bExists = false;
          //  if (dtClear.Rows[i]["CLH"].ToString() != "") { }

            for (int i = 0; i < dtClear.Rows.Count; i++)
            {
               
                if (dtClear.Rows[i]["CLH"].ToString() == clh)
                {
                    int j=i+1;
                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("与第"+j+"层重复，是否取消" +j+"层材料号"+ clh + "？", "选择", "是", "否");
                    if (frmMessage.ret == true)
                    {
                        dtClear.Rows[i]["CLH"] = "";
                    }
                    frmMessage.Dispose();
                    bExists = true;
                    break;
                }
            }

            if (!bExists)
            {
                if (curRow >= 0)
                {
                    dtClear.Rows[curRow]["CLH"] = clh;
                    PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                }
            }
            int cnt = 0;
            for (int i = 0; i < dtClear.Rows.Count; i++)
            {
                if (dtClear.Rows[i]["CLH"].ToString() != "")
                {
                    cnt++;
                }
            }

            //txtJs.Text = cnt.ToString();
        }

        //库位
        //private void addKw(string kw)
        //{
        //    bool bExists = false;
        //    DataRow row;

        //    for (int i = 0; i < dtClear.Rows.Count; i++)
        //    {
        //        if (dtClear.Rows[i]["KW"].ToString() == kw)
        //        {
        //            curRow = i;
        //            dgClear.CurrentCell = new DataGridCell(i, 0);
        //            dgClear.Select(i);
        //            dgClear.Invalidate();
        //            bExists = true;
        //            break;
        //        }
        //    }

        //    if (!bExists)
        //    {
        //        bool val = true;
        //        if (curKW != "")
        //        {
        //            frmMessage msg = new frmMessage();
        //            msg.ShowDialog("库位" + curKW + "上没有钢卷？", "选择", "是", "否");
        //            val = msg.ret;
        //            msg.Dispose();
        //        }
        //        if (val)
        //        {

        //            // string iKwRowString = kw.Substring(0, 5) + "-" + Convert.ToInt32(kw.Substring(5));

        //            //dtClear.Rows.Add(new object[] { iKwRowString, "", "True" });
        //            for (int i = 1; i <= dtClear.Rows.Count; i++)
        //            {
        //                row = dtClear.NewRow();
        //                if (row["KW"].ToString().Equals("") && i != Convert.ToInt32(kw.Substring(5)))
        //                {
        //                    row["KW"] = kw.Substring(0, 5) + "-" + i.ToString();
        //                }
        //                else
        //                {
        //                    row["KW"] = kw.Substring(0, 5) + "-" + Convert.ToInt32(kw.Substring(5));
        //                    curRow = i;
        //                }
        //                row["CLH"] = "";
        //                row["STATUS"] = "True";
        //                dtClear.Rows.Add(row);
        //            }

        //            //curRow = dtClear.Rows.Count - 1;
        //            dgClear.CurrentCell = new DataGridCell(curRow, 0);
        //            dgClear.Select(curRow);
        //            dgClear.Invalidate();
        //            PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
        //            curKW = kw;
        //        }

        //    }

        //}

        public void LoadData()
        {
            dtClear = new DataTable();
            dtClear.Columns.Add("CH");//层号
            dtClear.Columns.Add("CLH");
            dtClear.Columns.Add("STATUS");

            DataRow row;


            for (int i = 1; i <= rowCnt; i++)
            {
                row = dtClear.NewRow();
                //if (txtLayer.Text.Length == 5)
                //{
                //    row["KW"] = txtLayer.Text + "-" + i.ToString();
                //}
                //else
                //{
                //    row["KW"] = "";
                //}
                row["CH"] = i.ToString();
                row["CLH"] = "";
                row["STATUS"] = "True";
                dtClear.Rows.Add(row);
            }

            dgClear.DataSource = dtClear;
        }

        private void frmClear_Load(object sender, EventArgs e)
        {
            showConnect();
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }

            LoadData();

            dgClear.TableStyles.Clear();
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            DataGridTableStyle clearStyle = new DataGridTableStyle();
            clearStyle.MappingName = dtClear.TableName;////映射style对应数据源的表名，很重要，否则无数据显示

            clearStyle.GridColumnStyles.Add(new ColumnStyle(0, "CH", "层号", 80, "", cellEvent));
            clearStyle.GridColumnStyles.Add(new ColumnStyle(1, "CLH", "材料号", 140, "", cellEvent));



            dgClear.TableStyles.Add(clearStyle);

        }

        private void CheckCellColor(object sender, DataGridEnableEventArgs e)
        {
            if (dgClear.CurrentCell.RowNumber == e.Row && curRow != -1)
            {
                e.MeetsCriteria = true;
                e.BackColor = Color.Aquamarine;
                e.ForeColor = SystemColors.WindowText;
            }
            else
            {
                string status = dtClear.Rows[e.Row]["STATUS"].ToString();
                if (status == "True")
                {
                    e.MeetsCriteria = true;
                    e.BackColor = SystemColors.Window;
                    e.ForeColor = SystemColors.WindowText;
                }
                else
                {
                    e.MeetsCriteria = true;
                    e.BackColor = Color.Gainsboro;
                    e.ForeColor = SystemColors.WindowText;
                }
            }

        }

        private void frmClear_Closed(object sender, EventArgs e)
        {
            ScanCodeRemapping.NormalTable.Add(0x003A, new RemappingTable.Remapping(VirtualKey.VK_F24, Function.SendCode));
        }

        public void showConnect()
        {
            Bitmap bitmap = null;
            if (Global.wwanConnected)
            {
                bitmap = new Bitmap(Global.appPath + @"\img\connect.bmp");
            }
            else
            {
                bitmap = new Bitmap(Global.appPath + @"\img\disconnect.bmp");
            }
            Global.UIThread(picCon, delegate { picCon.Image = Image.FromHbitmap(bitmap.GetHbitmap()); });
        }

        private void dgClear_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgClear.CurrentCell.RowNumber;
            dgClear.Select(dgClear.CurrentCell.RowNumber);
        }

        //private void txtLayer_KeyUp(object sender, KeyEventArgs e)
        //{
        //    try
        //    {
        //        txtLayer.Text = txtLayer.Text.ToUpper();
        //        txtLayer.SelectionStart = txtLayer.Text.Length;
        //        if (txtLayer.Text.Length == 5)
        //        {
        //            //LoadData(txtLayer.Text);
        //        }
        //    }
        //    catch { }
        //}

        private void dgClear_KeyUp(object sender, KeyEventArgs e)
        {
            if (curRow >= 0)
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (dtClear.Rows[curRow]["STATUS"].ToString() == "True")
                    {
                        dtClear.Rows[curRow]["STATUS"] = "False";
                    }
                    else
                    {
                        dtClear.Rows[curRow]["STATUS"] = "True";
                    }
                }
            }
        }

        private void dgClear_Click(object sender, EventArgs e)
        {
            txtClh.Visible = true;
            lblClh.Visible = true;
            //comboBoxRow.Visible = false;
            txtClh.Focus();
        }

        private void txtClh_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape)
            {
                lblClh.Visible = false;
                txtClh.Visible = false;
                return;
            }

            if (e.KeyCode >= Keys.A && e.KeyCode <= Keys.Z)
            {
                txtClh.Text = txtClh.Text.ToUpper();
                txtClh.SelectionStart = txtClh.Text.Length;
            }

            if (e.KeyCode == Keys.Enter)
            {
                if (txtClh.Text.Length >= 9)
                {
                    addCl(txtClh.Text);//材料号
                }
                txtClh.Text = "";
                lblClh.Visible = false;
                txtClh.Visible = false;
            }

            if (txtClh.Text.Length >= 5)
            {
                string sql = "SELECT CLH2 FROM ExportStorageAcceptOrder WHERE (CLH2 LIKE '%" + txtClh.Text + "') ";
                sql += " UNION ";
                sql += "SELECT CLH2 FROM ImportStorageAcceptOrder WHERE (CLH2 LIKE '%" + txtClh.Text + "')";


                DataTable dt = SqlCe.ExecuteQuery(sql);
                if (dt.Rows.Count == 1)
                {
                    txtClh.Text = dt.Rows[0]["CLH2"].ToString();
                    txtClh.SelectionStart = txtClh.Text.Length;
                }
                else if (dt.Rows.Count > 1)
                {
                    lstClh.Items.Clear();
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        lstClh.Items.Add(dt.Rows[i]["CLH2"].ToString());
                    }
                    lstClh.Visible = true;
                }
                dt.Dispose();
            }
        }

        private void lstClh_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtClh.Text = lstClh.Items[lstClh.SelectedIndex].ToString();
            txtClh.SelectionStart = txtClh.Text.Length;
            txtClh.Focus();
            lstClh.Visible = false;
        }


      List<List<string[]>> Columnayers = new List<List<string[]>>();
            List<string[]> layerMessage = new List<string[]>();
            //string[] slabMessage = new string[] { }; 
        private void button1_Click(object sender, EventArgs e)
        {
            
            //list初始化
            layerMessage.Clear();
            Columnayers.Clear();
        
             String[] str1 = { "1", "123123123" };
             String[] str2 = { "2", "123456789"};
             String[] str3 = { "3", "111111111"};

             layerMessage.Add(str1);
             layerMessage.Add(str2);
             layerMessage.Add(str3);
             Columnayers.Add(new List<string[]>(layerMessage));
             layerMessage.Clear();
             
             String[] str11 = { "1", "111111111" };
             String[] str22 = { "2", "222222222" };
             String[] str33 = { "3", "333333333" };
             layerMessage.Add(str11);
             layerMessage.Add(str22);
             layerMessage.Add(str33);
             Columnayers.Add(new List<string[]>(layerMessage));

        }
    

        private void comboBoxRow_SelectedIndexChanged(object sender, EventArgs e)
        {
            string aaa = "";
            //如果选中
            if (comboBoxRow.SelectedIndex> -1)
            {
                aaa = comboBoxRow.Items[comboBoxRow.SelectedIndex].ToString();//方法1
                //aaa = comboBoxRow.SelectedItem.ToString();//方法2
            }
            for (int i = 0; i < Columnayers.Count;i++ )
            {
            if(Convert.ToInt32(aaa)==i+1){
                for (int j = 0;j < Columnayers[i].Count; j++)
                {    
                    string slabClh = Convert.ToInt32(Columnayers[i][j][1]).ToString();//板坯材料号
                    dtClear.Rows[j]["CLH"] = slabClh;
                  
                }
            }
            }
        
        }



    }
}