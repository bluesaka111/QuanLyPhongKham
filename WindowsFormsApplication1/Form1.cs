using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DataController;
using System.Data.SqlClient;
using DTObject;

namespace ActionForm
{
    public partial class Form1 : Form
    {
        private static bool runFromApp = false;
        private ListView.SelectedListViewItemCollection litem;
        private static DialogResult diaR;
        private int listv;
        public Form1()
        {
            InitializeComponent();
            litem = null;
        }

        public static bool commandRun
        {
            set { runFromApp = value; }
        }



        public static DialogResult ShowMenu(ListView.SelectedListViewItemCollection item, int listview, UserInfo employeedata, Point position)
        {
            if (!runFromApp)
            {
                return DialogResult.Cancel;
            }
            ActionForm.Form1 f = new Form1();
            f.litem = item;
            diaR = DialogResult.None;
            if (listview == 1)
            {
                f.toolTip1.SetToolTip(f.button4, "Thêm hồ sơ nhân viên");
                f.toolTip1.SetToolTip(f.button3, "Cập nhật hồ sơ nhân viên");
                f.toolTip1.SetToolTip(f.button2, "Xóa hồ sơ nhân viên");
                f.listv = listview;
            }
            else
            {
                f.toolTip1.SetToolTip(f.button4, "Thêm hồ sơ bệnh nhân");
                f.toolTip1.SetToolTip(f.button3, "Cập nhật hồ sơ bệnh nhân");
                f.toolTip1.SetToolTip(f.button2, "Xóa hồ sơ bệnh nhân");
                f.listv = listview;
            }
            f.button4.Enabled = false;
            f.button4.BackColor = Color.FromName("Grey");
            f.Location = position;
            f.ShowDialog();
            return diaR;
        }

        public static DialogResult ShowMenu(Point position, int listview)
        {
            if (!runFromApp)
            {
                return DialogResult.Cancel;
            }
            ActionForm.Form1 f = new Form1();
            diaR = DialogResult.None;
            f.Location = position;
            f.button3.Enabled = false;
            f.button3.BackColor = Color.FromName("Grey");
            f.button2.Enabled = false;
            f.button2.BackColor = Color.FromName("Grey");
            f.listv = listview;
            f.ShowDialog();
            return diaR;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(!runFromApp)
            {
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(litem == null)
            {
                return;
            }
            if(listv == 2)
            {
                if (litem.Count >= 1)
                {
                    PatientInfo.PForm patIfo = new PatientInfo.PForm(litem[0].SubItems[0].Text, true, 0);
                    patIfo.ShowDialog();
                }
                this.Close();
            }
            if (listv == 1)
            {
                if (litem.Count >= 1)
                {
                    EmployeeInfo.EForm empIfo = new EmployeeInfo.EForm(litem[0].SubItems[0].Text, true, 0);
                    empIfo.ShowDialog();
                }
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (litem == null)
            {
                return;
            }
            if (listv == 2)
            {
                if (litem.Count >= 1)
                {
                    PatientInfo.PForm patIfo = new PatientInfo.PForm(litem[0].SubItems[0].Text, false, 2);
                    patIfo.ShowDialog();
                }
                this.Close();
            }
            if(listv == 1)
            {
                if (litem.Count >= 1)
                {
                    EmployeeInfo.EForm empIfo = new EmployeeInfo.EForm(litem[0].SubItems[0].Text, false, 2);
                    empIfo.ShowDialog();
                }
                this.Close();
            }
        }

        private string GetMaBA(string maBN)
        {
            try
            {
                if(Connection.sqlcon.State != ConnectionState.Open)
                {
                    Connection.sqlcon.Open();
                }
                SqlCommand c = new SqlCommand();
                c.CommandText = "SELECT MaBA FROM QuanLyPhongKham.dbo.BenhAn WHERE MaBN = @MaBN";
                c.CommandType = CommandType.Text;
                c.Parameters.Add("@MaBN");
                c.Parameters["@MaBN"].Value = maBN;
                c.Connection = Connection.sqlcon;

                return (string)c.ExecuteScalar();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(litem.Count == 0)
            {
                return;
            }
            if(litem.Count >= 1)
            {
                if(listv == 1)
                {
                    try
                    {
                        if (Connection.sqlcon.State != ConnectionState.Open)
                        {
                            Connection.sqlcon.Open();
                        }
                        SqlCommand sqlc = new SqlCommand();
                        sqlc.Connection = Connection.sqlcon;
                        int success = 0;
                        foreach (ListViewItem item in litem)
                        {
                            sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.ThongTinDonThuoc WHERE QuanLyPhongKham.dbo.ThongTinDonThuoc.MaNV = '" + item.SubItems[0].Text + "'";
                            sqlc.CommandType = CommandType.Text;
                            sqlc.ExecuteNonQuery();
                            sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.BenhAn WHERE QuanLyPhongKham.dbo.BenhAn.MaNV = '" + item.SubItems[0].Text + "'";
                            sqlc.CommandType = CommandType.Text;
                            sqlc.ExecuteNonQuery();
                            sqlc.CommandText = "DELETE FROM NhanVien WHERE MaNV = '" + item.SubItems[0].Text + "'";
                            sqlc.ExecuteNonQuery();
                            success++;
                        }
                        MessageBox.Show("Xóa thành công " + success.ToString() + "/" + litem.Count.ToString() + " hồ sơ nhân viên", "Thành công");
                    }
                    catch(Exception ex)
                    {
                        MessageBox.Show("Lỗi", ex.Message);
                    }
                    this.Close();
                }
                if (listv == 2)
                {
                    try
                    {
                        if (Connection.sqlcon.State != ConnectionState.Open)
                        {
                            Connection.sqlcon.Open();
                        }
                        SqlCommand sqlc = new SqlCommand();
                        sqlc.Connection = Connection.sqlcon;
                        int success = 0;
                        foreach (ListViewItem i in litem)
                        {
                            try
                            {
                                string maBA = GetMaBA(i.SubItems[0].Text);
                                sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.ThongTinDonThuoc WHERE MaBA = @MaBA";
                                sqlc.CommandType = CommandType.Text;
                                sqlc.Connection = Connection.sqlcon;
                                sqlc.Parameters.Clear();
                                if (!sqlc.Parameters.Contains("@MaBA"))
                                {
                                    sqlc.Parameters.Add("@MaBA", SqlDbType.NChar);
                                }
                                sqlc.Parameters["@MaBA"].Value = maBA;
                                sqlc.Parameters["@MaBA"].SqlDbType = SqlDbType.NChar;

                                sqlc.ExecuteNonQuery();

                                sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.BenhAn WHERE MaBN = @MaBN";
                                sqlc.CommandType = CommandType.Text;
                                sqlc.Connection = Connection.sqlcon;
                                sqlc.Parameters.Clear();
                                if (!sqlc.Parameters.Contains("@MaBN"))
                                {
                                    sqlc.Parameters.Add("@MaBN", SqlDbType.NChar);
                                }
                                sqlc.Parameters["@MaBN"].Value = i.SubItems[0].Text;
                                sqlc.Parameters["@MaBN"].SqlDbType = SqlDbType.NChar;

                                sqlc.ExecuteNonQuery();

                                sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.BenhNhan WHERE MaBN = @MaBN";
                                sqlc.CommandType = CommandType.Text;
                                sqlc.Connection = Connection.sqlcon;
                                sqlc.Parameters.Clear();
                                if (!sqlc.Parameters.Contains("@MaBN"))
                                {
                                    sqlc.Parameters.Add("@MaBN", SqlDbType.NChar);
                                }
                                sqlc.Parameters["@MaBN"].Value = i.SubItems[0].Text;
                                sqlc.Parameters["@MaBN"].SqlDbType = SqlDbType.NChar;

                                sqlc.ExecuteNonQuery();
                                success++;
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show(ex.Message, "Lỗi");
                            }
                        }
                        MessageBox.Show("Xóa thành công " + success.ToString() + "/" + litem.Count.ToString() + " hồ sơ bệnh nhân", "Thành công");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Lỗi", ex.Message);
                    }
                    this.Close();
                }

            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (listv == 2)
            {
                if (litem == null)
                {
                    PatientInfo.PForm patIfo = new PatientInfo.PForm("NULL",false, 1);
                    patIfo.ShowDialog();
                }
                this.Close();
            }
            if (listv == 1)
            {
                if (litem == null)
                {
                    EmployeeInfo.EForm empIfo = new EmployeeInfo.EForm("NULL", false, 1);
                    empIfo.ShowDialog();
                }
                this.Close();
            }
        }
    }
}
