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
    public partial class frmClear : Form
    {
        private DataTable dtClear;
        private int curRow = -1;
        private string curKW = "";

        public frmClear()
        {
            InitializeComponent();
        }

        private void frmClear_Load(object sender, EventArgs e)
        {
            showConnect();
            if (ScanCodeRemapping.NormalTable.Contains(0x003A))
            {
                ScanCodeRemapping.NormalTable.Remove(0x003A);
            }

            dtClear = new DataTable();
            dtClear.Columns.Add("CLH");
            dtClear.Columns.Add("KW");
            dtClear.Columns.Add("STATUS");
            dgClear.DataSource = dtClear;
            dgClear.TableStyles.Clear();

            dgClear.TableStyles.Clear();
            CheckCellEventHandler cellEvent = new CheckCellEventHandler(CheckCellColor);

            DataGridTableStyle clearStyle = new DataGridTableStyle();
            clearStyle.MappingName = dtClear.TableName;

            clearStyle.GridColumnStyles.Add(new ColumnStyle(0, "KW", "库位", 100, "", cellEvent));
            clearStyle.GridColumnStyles.Add(new ColumnStyle(1, "CLH", "材料号", 120, "", cellEvent));


            dgClear.TableStyles.Add(clearStyle);


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
                if (curKW != "")
                {
                    frmMessage msg = new frmMessage();
                    msg.ShowDialog("最后一个库位" + curKW + "上没有钢卷？", "选择", "是", "否");
                    msg.Dispose();
                }
                SendClearResult();
            }
            if (e.KeyCode == Keys.Tab)
            {
                //frmWriteOff frm = new frmWriteOff();//销账
                //Global.frmCurrent = frm;
                //frm.Owner = this;
                //frm.Show();
                //this.Hide();
            }
        }

        public void showDialogMsg(string msg)
        {
            Global.UIThread(this, delegate
            {
                frmMessage dialog = new frmMessage();
                dialog.ShowDialog(msg, "提示", "确定");
                dialog.Dispose();
            });
        }

        private void SendClearResult()
        {
            if (dtClear.Rows.Count > 0)
            {
                string gram = Global.sUserId + AllCode.stringInterfaceChar;
                gram += System.DateTime.Now.ToString("yyyyMMddHHmmss") + AllCode.stringInterfaceChar;
                gram += Global.sBb + AllCode.stringInterfaceChar;
                gram += Global.sZyq + AllCode.stringInterfaceChar;
                gram += System.DateTime.Now.ToString("yyyyMMddHHmmss") + AllCode.stringInterfaceChar;

                string str = "";

                for (int i = 0; i < dtClear.Rows.Count; i++)
                {
                    if (dtClear.Rows[i]["STATUS"].ToString() == "True")
                    {
                        str += dtClear.Rows[i]["CLH"].ToString() + AllCode.stringInterfaceChar;
                        int length = dtClear.Rows[i]["KW"].ToString().Length;
                        if (length == 7)
                        {
                            str += "**" + dtClear.Rows[i]["KW"].ToString() + "1" + AllCode.stringInterfaceChar;
                        }
                        else {
                            str += dtClear.Rows[i]["KW"].ToString() + "1" + AllCode.stringInterfaceChar;
                        }
                    }
                    else
                    {
                        str += "99999999999" + AllCode.stringInterfaceChar;
                        str += dtClear.Rows[i]["KW"].ToString() + "0" + AllCode.stringInterfaceChar;
                    }

                    

                }

                if (str != "")
                {
                    gram += dtClear.Rows.Count.ToString() + AllCode.stringInterfaceChar;
                    str = str.Substring(0, str.Length - 1);
                    if (Global.sDebug == "False")
                    {
                        Business.SendText(Business.msg.Package("ZDUA16", gram + str + AllCode.stringInterfaceChar));//清盘库材料信息
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
            else
            {
                if (curRow >= 0)
                {
                    
                    addCl(barcode);
                }
            }

        }

        private void addKw(string kw)
        {
            bool bExists = false;
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
                    msg.ShowDialog("库位"+ curKW +"上没有钢卷？", "选择", "是","否");
                    val = msg.ret;
                    msg.Dispose();
                }
                if (val)
                {
                    dtClear.Rows.Add(new object[] { "", kw, "True" });
                    curRow = dtClear.Rows.Count - 1;
                    dgClear.CurrentCell = new DataGridCell(curRow, 0);
                    dgClear.Select(curRow);
                    dgClear.Invalidate();
                    PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                    curKW = kw;
                }
 
            }

        }

        private void addCl(string clh)
        {
            if (curRow >= 0)
            {
                int index = 0;
                bool bExists = false;

                for (int i = 0; i < dtClear.Rows.Count; i++)
                {
                    if (dtClear.Rows[i]["CLH"].ToString() == clh)
                    {
                        index = i;
                        bExists = true;
                        break;
                    }
                }

                if (!bExists)
                {
                    if (dtClear.Rows[curRow]["CLH"].ToString() == "")
                    {
                        dtClear.Rows[curRow]["CLH"] = clh;
                        PsionTeklogix.Sound.Beeper.Beeper.PlayTone(5000, 500, 100);
                        //curRow = -1;
                        if (dtClear.Rows[curRow]["KW"].ToString() == curKW)
                        {
                            curKW = "";
                        }
                    }
                 }
                else
                {
                    frmMessage frmMessage = new frmMessage();
                    frmMessage.ShowDialog("是否取消"+ clh +"的库位信息？", "选择", "确定", "取消");
                    if (frmMessage.ret == true)
                    {
                        dtClear.Rows.RemoveAt(index);
                        curRow = -1;
                    }
                    frmMessage.Dispose();
                }
            }

            txtJs.Text = dtClear.Rows.Count.ToString();
        }

        private void txtKw_KeyUp(object sender, KeyEventArgs e)
        {
            try
            {
                txtKw.Text = txtKw.Text.ToUpper();
                txtKw.SelectionStart = txtKw.Text.Length;
                if (txtKw.Text.Length == 7)
                {
                    addKw(txtKw.Text);
                }
            }
            catch { }
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

        private void dgClear_KeyUp(object sender, KeyEventArgs e)
        {
            if (curRow >= 0)
            {
                if (e.KeyCode == Keys.Back)
                {
                    if (dtClear.Rows[curRow]["STATUS"].ToString() == "True")
                    {
                        dtClear.Rows[curRow]["STATUS"] = "False";
                        if (dtClear.Rows[curRow]["KW"].ToString() == curKW)
                        {
                            curKW = "";
                        }
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
                    addCl(txtClh.Text);
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