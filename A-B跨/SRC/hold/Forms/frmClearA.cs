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
    public partial class frmClearA : Form
    {
        private DataTable dtClear;
        private int curRow = -1;
        private int rowCnt = 15;
        //private delegate void setImage();
        //private delegate void setText(string msg);
        private string curKW = "";

        public frmClearA()
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

            if (e.KeyCode == Keys.Tab)
            {
                frmClear frm = new frmClear();//清盘库
                Global.frmCurrent = frm;
                frm.Owner = this;
                frm.Show();
                this.Hide();
            }
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
                string kw = "";
                int k = 0;
                for (int i = 0; i < dtClear.Rows.Count; i++)
                {
                    k = i + 1;
                    kw = dtClear.Rows[i]["KW"].ToString();
                    int length = kw.Length;
                 
                    if (dtClear.Rows[i]["STATUS"].ToString() == "True")
                    {
                        str += dtClear.Rows[i]["CLH"].ToString() + ",";
                    }
                    else
                    {
                        str += "99999999999,";
                    }

                    if (length > 4)
                    {
                        if (length == 7)
                        {
                            str += "**" + kw.Substring(0, 5) + k.ToString("00") + "1,";
                        }
                        else
                        {
                            str += kw.Substring(0, 5) + k.ToString("00") + "1,";
                        }
                    }
                    else
                    {
                        str += kw + k.ToString("00") + "1,";
                    }

                }

                if (str != "")
                {
                    gram += rowCnt.ToString() + ",";
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
                addKw(text);
            }

            if (txtKw.Text != "")
            {
                addCl(barcode);
            }

        }
        //材料号
        private void addCl(string clh)
        {
            bool bExists = false;
            for (int i = 0; i < dtClear.Rows.Count; i++)
            {
                if (dtClear.Rows[i]["CLH"].ToString() == clh)
                {
                                    
                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("是否取消材料号"+ clh +"？", "选择", "是", "否");
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
                
            txtJs.Text =cnt.ToString();
        }

        //库位
        private void addKw(string kw)
        {
            bool bExists = false;
            DataRow row;

            for (int i = 0; i < dtClear.Rows.Count; i++)
            {
                if (dtClear.Rows[i]["KW"].ToString() == kw)
                {
                    curRow = i;
                    dgClear.CurrentCell = new DataGridCell(i, 0);
                    dgClear.Select(i);
                    dgClear.Invalidate();
                    bExists = true;
                    break;
                }
            }

            if (!bExists)
            {
                bool val = true;
                if (curKW != "")
                {
                    frmMessage msg = new frmMessage();
                    msg.ShowDialog("库位" + curKW + "上没有钢卷？", "选择", "是", "否");
                    val = msg.ret;
                    msg.Dispose();
                }
                if (val)
                {
                   
                   // string iKwRowString = kw.Substring(0, 5) + "-" + Convert.ToInt32(kw.Substring(5));
                    
                   //dtClear.Rows.Add(new object[] { iKwRowString, "", "True" });
                    for (int i = 1; i <= dtClear.Rows.Count; i++)
                   {
                       row = dtClear.NewRow();
                       if (row["KW"].ToString().Equals("") && i != Convert.ToInt32(kw.Substring(5)))
                       {
                           row["KW"] = kw.Substring(0, 5) + "-" + i.ToString();
                       }
                       else
                       {
                           row["KW"] = kw.Substring(0, 5) + "-" + Convert.ToInt32(kw.Substring(5));
                           curRow = i;
                       }
                       row["CLH"] = "";
                       row["STATUS"] = "True";
                       dtClear.Rows.Add(row);
                   }

                    //curRow = dtClear.Rows.Count - 1;
                    dgClear.CurrentCell = new DataGridCell(curRow, 0);
                    dgClear.Select(curRow);
                    dgClear.Invalidate();
                    PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                    curKW = kw;
                }

            }

        }

        public void LoadData(string kw)
        {
            dtClear = new DataTable();
            dtClear.Columns.Add("KW");
            dtClear.Columns.Add("CLH");
            dtClear.Columns.Add("STATUS");

            DataRow row;

            if (kw.StartsWith("B11"))
            {
                rowCnt = 14;
            }
            else if (kw.StartsWith("B12"))
            {
                rowCnt = 15;
            }
            else if (kw.StartsWith("B10"))
            {
                rowCnt = 15;
            }
            else if (kw.StartsWith("B08"))
            {
                rowCnt = 15;
            }
            else if (kw.StartsWith("B07"))
            {
                int iKw = Convert.ToInt32(kw.Substring(3, 2));
                if (iKw<=28)
                {
                    rowCnt = 19;
                }
                else
                {
                    rowCnt = 15;
                }
            }
            else if (kw.StartsWith("B09"))
            {
                int iKw = Convert.ToInt32(kw.Substring(3, 2));
                if (iKw <= 26)
                {
                    rowCnt = 19;
                }
                else
                {
                    rowCnt = 15;
                }
            }

            for (int i = 1; i <= rowCnt; i++)
            {
                row = dtClear.NewRow();
                if (txtKw.Text.Length == 5)
                {
                    row["KW"] = txtKw.Text + "-" + i.ToString();
                }
                else
                {
                    row["KW"] = "";
                }
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

            LoadData(txtKw.Text);

            dgClear.TableStyles.Clear();
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            DataGridTableStyle clearStyle = new DataGridTableStyle();
            clearStyle.MappingName = dtClear.TableName;

            clearStyle.GridColumnStyles.Add(new ColumnStyle(0, "KW", "库位", 100, "", cellEvent));
            clearStyle.GridColumnStyles.Add(new ColumnStyle(1, "CLH", "材料号", 120, "", cellEvent));
            


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
                bitmap = new Bitmap(Global.appPath +@"\img\connect.bmp");
            }
            else
            {
                bitmap = new Bitmap(Global.appPath +@"\img\disconnect.bmp");
            }
            Global.UIThread(picCon, delegate { picCon.Image = Image.FromHbitmap(bitmap.GetHbitmap()); });
        }

        private void dgClear_CurrentCellChanged(object sender, EventArgs e)
        {
            curRow = dgClear.CurrentCell.RowNumber;
            dgClear.Select(dgClear.CurrentCell.RowNumber);
        }

        private void txtKw_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                txtKw.Text = txtKw.Text.ToUpper();
                txtKw.SelectionStart = txtKw.Text.Length;
                if (txtKw.Text.Length == 5)
                {
                    LoadData(txtKw.Text);
                }
            }
            catch { }
        }

        private void dgClear_KeyUp(object sender, KeyEventArgs e)
        {
            if(curRow>=0)
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

        private void dgClear_Click(object sender, EventArgs e)//1
        {
            txtClh.Visible = true;
            lblClh.Visible = true;
            txtClh.Focus();
        }

        private void txtClh_KeyUp(object sender, KeyEventArgs e)//2
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

    }
}