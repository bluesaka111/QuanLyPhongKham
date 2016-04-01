using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

using DataController;

namespace EmployeeInfo
{
    public partial class EForm : Form
    {
        Point oldP = new Point();
        private bool userIntended;
        private int intentType;
        private string empID;
        public EForm()
        {
            InitializeComponent();
            userIntended = true;
            intentType = 0;
            empID = "NULL";
        }

        public EForm(string employeeID, bool userintent, int type)
        {
            InitializeComponent();
            empID = employeeID;
            userIntended = userintent;
            label1.Text = "Thông tin nhân viên [" + employeeID + "] ";
            intentType = type;
            ResetField(employeeID);
        }

        private void ResetField(string eID)
        {
            if (eID == "NULL")
            {
                return;
            }
            txt_maNV.Text = (eID == "NULL" ? String.Empty : eID);
            if (Connection.sqlcon.State != ConnectionState.Closed)
            {
                Connection.sqlcon.Close();
            }
            SqlCommand sqlc = new SqlCommand();
            sqlc.Connection = Connection.sqlcon;
            sqlc.CommandText = "QuanLyPhongKham.dbo.usp_GetFullEmployeeInfo";
            sqlc.CommandType = CommandType.StoredProcedure;
            if (!sqlc.Parameters.Contains("@employeeID"))
            {
                sqlc.Parameters.Add("@employeeID", SqlDbType.NChar);
                sqlc.Parameters["@employeeID"].Value = eID;
                sqlc.Parameters["@employeeID"].Size = 10;
            }
            try
            {
                Connection.sqlcon.Open();
                SqlDataReader reader = sqlc.ExecuteReader();
                if (!reader.HasRows)
                {
                    MessageBox.Show("Nhân viên " + eID + "không tồn tại trong hệ thống dữ liệu của phòng khám.Vui lòng kiểm tra hoặc liên hệ quản trị viên để biết thêm.\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Connection.sqlcon.Close();
                    reader.Close();
                    return;
                }
                if (reader.HasRows)
                {
                    reader.Read();
                    txt_maNV.Text = (reader["MaNV"] == DBNull.Value ? "NULL" : reader["MaNV"].ToString());
                    txt_HoNV.Text = (reader["HoLotNV"] == DBNull.Value ? "NULL" : reader["HoLotNV"].ToString());
                    txt_TenNV.Text = (reader["TenNV"] == DBNull.Value ? "NULL" : reader["TenNV"].ToString());
                    cb_GTNV.SelectedIndex = (reader["GioiTinh"].ToString() == "False" ? 0 : 1);
                    txt_CVNV.Text = (reader["ChucVu"] == DBNull.Value ? "NULL" : reader["ChucVu"].ToString());
                    txt_DThoai.Text = (reader["SoDienThoai"] == DBNull.Value ? "NULL" : reader["SoDienThoai"].ToString());
                    txt_DiaChi.Text = (reader["DiaChi"] == DBNull.Value ? "NULL" : reader["DiaChi"].ToString());
                    txt_BonusNV.Text = (reader["ThuongLuong"] == DBNull.Value ? "NULL" : reader["ThuongLuong"].ToString());
                    dtp_wDay.Value = (reader["NgayVaoLam"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["NgayVaoLam"].ToString()));
                    txt_LuongNV.Text = (reader["Luong"] == DBNull.Value ? "NULL" : reader["Luong"].ToString());
                    reader.Close();
                }

                sqlc.CommandText = "SELECT * FROM BenhAn WHERE MaNV = '" + txt_maNV.Text.Trim() + "'";
                sqlc.CommandType = CommandType.Text;
                reader = sqlc.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return;
                }
                if (reader.HasRows)
                {
                    for(int i = 0; i < reader.FieldCount; i++)
                    {
                        ColumnHeader ch = new ColumnHeader();
                        ch.Text = reader.GetName(i);
                        ch.Width = listView1.Width / reader.FieldCount;
                        listView3.Columns.Add(ch);
                    }
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = reader[0].ToString();
                        item.Tag = reader.GetName(0);
                        for(int i = 1; i < reader.FieldCount; i++)
                        {
                            ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                            sub.Text = reader[i].ToString();
                            sub.Tag = reader.GetName(i);
                            item.SubItems.Add(sub);
                        }
                        listView3.Items.Add(item);
                    }
                    listView3.GridLines = true;
                }
                reader.Close();
                sqlc.CommandText = "SELECT * FROM ThongTinDonThuoc WHERE MaNV = '" + txt_maNV.Text.Trim() + "'";
                sqlc.CommandType = CommandType.Text;
                reader = sqlc.ExecuteReader();
                if(!reader.HasRows)
                {
                    reader.Close();
                    return;
                }
                if (reader.HasRows)
                {
                    for (int i = 0; i < reader.FieldCount; i++)
                    {
                        ColumnHeader ch = new ColumnHeader();
                        ch.Text = reader.GetName(i);
                        ch.Width = listView1.Width / reader.FieldCount;
                        listView1.Columns.Add(ch);
                    }
                    while (reader.Read())
                    {
                        ListViewItem item = new ListViewItem();
                        item.Text = reader[0].ToString();
                        item.Tag = reader.GetName(0);
                        for (int i = 1; i < reader.FieldCount; i++)
                        {
                            ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                            sub.Text = reader[i].ToString();
                            sub.Tag = reader.GetName(i);
                            item.SubItems.Add(sub);
                        }
                        listView1.Items.Add(item);
                    }
                    listView3.GridLines = true;
                }
                reader.Close();
                pictureBox2.Load(@"http://localhost/employeePTS/" + eID.ToString());
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy cập dữ liệu của nhân viên " + eID + ": \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Connection.sqlcon.Close();
                return;
            }
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                oldP = e.Location;
                label1.Cursor = Cursors.SizeAll;
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newP = e.Location;
                int x = newP.X - oldP.X;
                int y = newP.Y - oldP.Y;
                this.Left += x;
                this.Top += y;
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                oldP = e.Location;
                label1.Cursor = Cursors.Default;
            }
        }

        private void expandablePanel2_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            int total = 0;
            int total2 = 0;
            DevComponents.DotNetBar.ExpandablePanel expanel = (DevComponents.DotNetBar.ExpandablePanel)sender;
            foreach (Control c in panel3.Controls)
            {
                if ((c is Panel) && c.Height == 5)
                {
                    total++;
                }
            }
            foreach (Control c in panel3.Controls)
            {
                if ((c is DevComponents.DotNetBar.ExpandablePanel) && c.Name != expanel.Name)
                {
                    if ((c as DevComponents.DotNetBar.ExpandablePanel).Expanded)
                        total2 = total2 + (c as DevComponents.DotNetBar.ExpandablePanel).MaximumSize.Height;
                    else
                        total2 = total2 + 26;
                }
            }
            if (expanel.Expanded)
            {
                expanel.Height = panel3.Height - (total2 + (5 * total) + 210);
            }
            else
            {
                expanel.Height = 26;
            }
        }

        private void expandablePanel3_ExpandedChanged(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            int total = 0;
            int total2 = 0;
            DevComponents.DotNetBar.ExpandablePanel expanel = (DevComponents.DotNetBar.ExpandablePanel)sender;
            foreach (Control c in panel13.Controls)
            {
                if ((c is Panel) && c.Height == 5)
                {
                    total++;
                }
            }
            foreach (Control c in panel13.Controls)
            {
                if ((c is DevComponents.DotNetBar.ExpandablePanel) && c.Name != expanel.Name)
                {
                    if ((c as DevComponents.DotNetBar.ExpandablePanel).Expanded)
                        total2 = total2 + (c as DevComponents.DotNetBar.ExpandablePanel).MaximumSize.Height;
                    else
                        total2 = total2 + 26;
                }
            }
            if (expanel.Expanded)
            {
                expanel.Height = panel13.Height - (total2 + (5 * total));
            }
            else
            {
                expanel.Height = 26;
            }
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Panel c in expandablePanel1.Controls)
            {
                foreach (Control es in c.Controls)
                {
                    if(es is Label || es is ComboBox || es is DateTimePicker)
                    {
                        continue;
                    }
                    if (es is TextBox)
                    {
                        if (es.Tag == null)
                        {
                            continue;
                        }
                        if(es.Tag.ToString().Contains("IGNORE") && intentType == 2)
                        {
                            continue;
                        }
                        if (es.Tag.ToString() == "NOT NULL READ ONLY" || es.Tag.ToString() == "NOT NULL")
                        {
                            if (intentType != 1)
                            {
                                if((es as TextBox).Text == "NULL" || (es as TextBox).Text == "" || (es as TextBox).Text == String.Empty)
                                {
                                    (es as TextBox).ReadOnly = false;
                                }
                                else
                                {
                                    (es as TextBox).ReadOnly = userIntended;
                                }
                            }
                            else
                            {
                                (es as TextBox).ReadOnly = false;
                            }
                        }
                        else
                        {
                            if (intentType != 1)
                            {
                                (es as TextBox).ReadOnly = userIntended;
                            }
                            else
                            {
                                (es as TextBox).ReadOnly = userIntended;
                            }
                        }
                        if (es.Tag.ToString() == "NOT NULL" || es.Tag.ToString() == "NOT NULL READ ONLY")
                        {
                            if (es.Text == "" || es.Text == String.Empty || es.Text == "NULL")
                            {
                                c.BackColor = Color.Maroon;
                                c.ForeColor = Color.Yellow;
                                
                                if (intentType == 1)
                                {
                                    bt_add_NV.Enabled = true;
                                }
                                else
                                {
                                    bt_add_NV.Enabled = false;
                                }
                            }
                            else
                            {
                                c.BackColor = Color.FromArgb(13, 77, 77);
                                c.ForeColor = Color.White;
                                bt_update_NV.Enabled = true;
                                if (intentType == 1)
                                {
                                    bt_add_NV.Enabled = true;
                                }
                                else
                                {
                                    bt_add_NV.Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            c.BackColor = Color.FromArgb(13, 77, 77);
                            c.ForeColor = Color.White;
                        }
                    }
                }
            }
            foreach (Panel c in expandablePanel2.Controls)
            {
                foreach (Control es in c.Controls)
                {
                    if (es is Label || es is ComboBox || es is DateTimePicker)
                    {
                        continue;
                    }
                    if (es is TextBox)
                    {
                        if (es.Tag == null)
                        {
                            continue;
                        }
                        if (es.Tag.ToString() == "NOT NULL READ ONLY" || es.Tag.ToString() == "NOT NULL")
                        {
                            if (intentType != 1)
                            {
                                if ((es as TextBox).Text == "NULL")
                                {
                                    (es as TextBox).ReadOnly = false;
                                }
                                else
                                {
                                    (es as TextBox).ReadOnly = userIntended;
                                }
                            }
                            else
                            {
                                (es as TextBox).ReadOnly = false;
                            }
                        }
                        else
                        {
                            if (intentType != 1)
                            {
                                (es as TextBox).ReadOnly = true;
                            }
                            else
                            {
                                (es as TextBox).ReadOnly = false;
                            }
                        }
                        if (es.Tag.ToString() == "NOT NULL" || es.Tag.ToString() == "NOT NULL READ ONLY")
                        {
                            if (es.Text == "" || es.Text == String.Empty || es.Text == "NULL")
                            {
                                c.BackColor = Color.Maroon;
                                c.ForeColor = Color.Yellow;
                                es.Text = "NULL";
                                bt_update_NV.Enabled = false;
                                if (intentType == 1)
                                {
                                    bt_add_NV.Enabled = true;
                                }
                                else
                                {
                                    bt_add_NV.Enabled = false;
                                }
                            }
                            else
                            {
                                c.BackColor = Color.FromArgb(13, 77, 77);
                                c.ForeColor = Color.White;
                                es.Text = "NULL";
                                bt_update_NV.Enabled = true;
                                if (intentType == 1)
                                {
                                    bt_add_NV.Enabled = true;
                                }
                                else
                                {
                                    bt_add_NV.Enabled = false;
                                }
                            }
                        }
                        else
                        {
                            if (es.Text == "" || es.Text == String.Empty || es.Text == "NULL" || es.Text == null)
                            {
                                es.Text = "NULL";
                            }
                            c.BackColor = Color.FromArgb(13, 77, 77);
                            c.ForeColor = Color.White;
                        }
                    }
                }
            }
        }

        private void expandablePanel5_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            DevComponents.DotNetBar.ExpandablePanel exp = (DevComponents.DotNetBar.ExpandablePanel)sender;
            exp.AutoScroll = exp.Expanded;
        }

        private void expandablePanel3_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            DevComponents.DotNetBar.ExpandablePanel exp = (DevComponents.DotNetBar.ExpandablePanel)sender;
            exp.AutoScroll = exp.Expanded;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            BenhAn.AddEdit ae;
            if (listView1.Items.Count == 0)
            {
                ae = new BenhAn.AddEdit();
            }
            else
            {
                ae = new BenhAn.AddEdit(listView1.SelectedItems[0]);
            }
            ae.ShowDialog();
            ResetField(empID);
        }

        private void button5_Click(object sender, EventArgs e)
        {
            BenhAn.AddEdit ae;
            if (listView1.Items.Count == 0)
            {
                return;
            }
            else
            {
                ae = new BenhAn.AddEdit(listView1.SelectedItems[0]);
            }
            ae.ShowDialog();
            ResetField(empID);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            BenhAn.Delete del;
            if (listView1.Items.Count == 0)
            {
                return;
            }
            else
            {
                del = new BenhAn.Delete(listView1.SelectedItems);
            }
            del.ShowDialog();
            ResetField(empID);
        }

        private void button1_Click(object sender, EventArgs e)
        {
            DonThuoc.AddEdit ae;
            if (listView1.Items.Count == 0)
            {
                ae = new DonThuoc.AddEdit();
            }
            else
            {
                ae = new DonThuoc.AddEdit(listView1.SelectedItems[0]);
            }
            ae.ShowDialog();
            ResetField(empID);
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DonThuoc.AddEdit ae;
            if (listView1.Items.Count == 0)
            {
                return;
            }
            else
            {
                ae = new DonThuoc.AddEdit(listView1.SelectedItems[0]);
            }
            ae.ShowDialog();
            ResetField(empID);
        }

        private void EForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void bt_add_NV_Click(object sender, EventArgs e)
        {
            if (txt_maNV.Text == "" || txt_maNV.Text == String.Empty || txt_maNV.Text == null)
            {
                MessageBox.Show("Vui lòng điền vào mã Bệnh Nhân trước khi tiếp tục");
                return;
            }
            try
            {
                if (Connection.sqlcon.State != ConnectionState.Open)
                {
                    Connection.sqlcon.Open();
                }
                SqlCommand sqlc = new SqlCommand();
                sqlc.Connection = Connection.sqlcon;
                sqlc.CommandText = "INSERT INTO QuanLyPhongKham.dbo.NhanVien VALUES ( @MaNV, @HoNV, @TenNV, @ChucVu, @GioiTinh, @DienThoai, @DiaChi, @ThuongLuong, @NgayVaoLam, @Luong)";
                sqlc.CommandType = CommandType.Text;
                sqlc.Parameters.Clear();
                    sqlc.Parameters.Add("@MaNV", SqlDbType.NChar);
                    sqlc.Parameters.Add("@HoNV", SqlDbType.NVarChar);
                    sqlc.Parameters.Add("@TenNV", SqlDbType.NVarChar);
                    sqlc.Parameters.Add("@GioiTinh", SqlDbType.Bit);
                    sqlc.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
                    sqlc.Parameters.Add("@DienThoai", SqlDbType.NChar);
                    sqlc.Parameters.Add("@ChucVu", SqlDbType.NVarChar);
                    sqlc.Parameters.Add("@ThuongLuong", SqlDbType.Real);
                    sqlc.Parameters.Add("@NgayVaoLam", SqlDbType.DateTime);
                sqlc.Parameters.Add("@Luong", SqlDbType.Money);
                sqlc.Parameters["@MaNV"].Value = txt_maNV.Text;
                sqlc.Parameters["@MaNV"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@HoNV"].Value = txt_HoNV.Text;
                sqlc.Parameters["@HoNV"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@TenNV"].Value = txt_TenNV.Text;
                sqlc.Parameters["@TenNV"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@GioiTinh"].Value = cb_GTNV.SelectedIndex;
                sqlc.Parameters["@GioiTinh"].SqlDbType = SqlDbType.Bit;
                sqlc.Parameters["@DiaChi"].Value = txt_DiaChi.Text;
                sqlc.Parameters["@DiaChi"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@DienThoai"].Value = txt_DThoai.Text;
                sqlc.Parameters["@DienThoai"].SqlDbType = SqlDbType.NChar;

                sqlc.Parameters["@ChucVu"].Value = txt_CVNV.Text;
                sqlc.Parameters["@ChucVu"].SqlDbType = SqlDbType.NVarChar;

                sqlc.Parameters["@NgayVaoLam"].Value = dtp_wDay.Value;
                sqlc.Parameters["@NgayVaoLam"].SqlDbType = SqlDbType.DateTime;

                sqlc.Parameters["@Luong"].Value = txt_LuongNV.Text;
                sqlc.Parameters["@Luong"].SqlDbType = SqlDbType.Money;

                sqlc.Parameters["@ThuongLuong"].Value = txt_BonusNV.Text;
                sqlc.Parameters["@ThuongLuong"].SqlDbType = SqlDbType.Real;

                sqlc.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void bt_update_NV_Click(object sender, EventArgs e)
        {
            if (txt_maNV.Text == "" || txt_maNV.Text == String.Empty || txt_maNV.Text == null)
            {
                MessageBox.Show("Vui lòng điền vào mã Bệnh Nhân trước khi tiếp tục");
                return;
            }
            try
            {
                if (Connection.sqlcon.State != ConnectionState.Open)
                {
                    Connection.sqlcon.Open();
                }
                SqlCommand sqlc = new SqlCommand();
                sqlc.Connection = Connection.sqlcon;
                sqlc.CommandText = "UPDATE QuanLyPhongKham.dbo.NhanVien SET HoLotNV = @HoNV, TenNV = @TenNV, ChucVu = @ChucVu, GioiTinh = @GioiTinh, SoDienThoai = @DienThoai, DiaChi = @DiaChi, ThuongLuong = @ThuongLuong, NgayVaoLam = @NgayVaoLam, Luong = @Luong WHERE MaNV = @MaNV";
                sqlc.CommandType = CommandType.Text;
                sqlc.Parameters.Clear();
                    sqlc.Parameters.Add("@MaNV", SqlDbType.NChar);
                    sqlc.Parameters.Add("@HoNV", SqlDbType.NVarChar);
                    sqlc.Parameters.Add("@TenNV", SqlDbType.NVarChar);
                    sqlc.Parameters.Add("@GioiTinh", SqlDbType.Bit);
                    sqlc.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
                    sqlc.Parameters.Add("@DienThoai", SqlDbType.NChar);
                    sqlc.Parameters.Add("@ChucVu", SqlDbType.NVarChar);
                    sqlc.Parameters.Add("@ThuongLuong", SqlDbType.Real);
                    sqlc.Parameters.Add("@NgayVaoLam", SqlDbType.DateTime);
                    sqlc.Parameters.Add("@Luong", SqlDbType.Money);
                sqlc.Parameters["@MaNV"].Value = txt_maNV.Text;
                sqlc.Parameters["@MaNV"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@HoNV"].Value = txt_HoNV.Text;
                sqlc.Parameters["@HoNV"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@TenNV"].Value = txt_TenNV.Text;
                sqlc.Parameters["@TenNV"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@GioiTinh"].Value = cb_GTNV.SelectedIndex;
                sqlc.Parameters["@GioiTinh"].SqlDbType = SqlDbType.Bit;
                sqlc.Parameters["@DiaChi"].Value = txt_DiaChi.Text;
                sqlc.Parameters["@DiaChi"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@DienThoai"].Value = txt_DThoai.Text;
                sqlc.Parameters["@DienThoai"].SqlDbType = SqlDbType.NChar;

                sqlc.Parameters["@ChucVu"].Value = txt_CVNV.Text;
                sqlc.Parameters["@ChucVu"].SqlDbType = SqlDbType.NVarChar;

                sqlc.Parameters["@NgayVaoLam"].Value = dtp_wDay.Value;
                sqlc.Parameters["@NgayVaoLam"].SqlDbType = SqlDbType.DateTime;

                sqlc.Parameters["@Luong"].Value = txt_LuongNV.Text;
                sqlc.Parameters["@Luong"].SqlDbType = SqlDbType.Money;

                sqlc.Parameters["@ThuongLuong"].Value = txt_BonusNV.Text;
                sqlc.Parameters["@ThuongLuong"].SqlDbType = SqlDbType.Real;

                sqlc.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void bt_delete_NV_Click(object sender, EventArgs e)
        {
            if (txt_maNV.Text == "" || txt_maNV.Text == String.Empty || txt_maNV.Text == null)
            {
                MessageBox.Show("Vui lòng điền vào mã Nhân Viên trước khi tiếp tục");
                return;
            }
            try
            {
                if (Connection.sqlcon.State != ConnectionState.Open)
                {
                    Connection.sqlcon.Open();
                }
                SqlCommand sqlc = new SqlCommand();
                sqlc.Connection = Connection.sqlcon;
                sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.ThongTinDonThuoc WHERE QuanLyPhongKham.dbo.ThongTinDonThuoc.MaNV = @MaNV";
                sqlc.CommandType = CommandType.Text;
                    sqlc.Parameters.Clear();
                    sqlc.Parameters.Add("MaNV", SqlDbType.NChar);
                sqlc.Parameters["@MaNV"].Value = txt_maNV.Text;
                sqlc.Parameters["@MaNV"].SqlDbType = SqlDbType.NChar;
                sqlc.ExecuteNonQuery();
                sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.BenhAn WHERE QuanLyPhongKham.dbo.BenhAn.MaNV = @MaNV";
                sqlc.CommandType = CommandType.Text;
                    sqlc.Parameters.Clear();
                sqlc.Parameters.Add("MaNV", SqlDbType.NChar);
                sqlc.Parameters["@MaNV"].Value = txt_maNV.Text;
                sqlc.Parameters["@MaNV"].SqlDbType = SqlDbType.NChar;
                sqlc.ExecuteNonQuery();
                sqlc.CommandText = "DELETE FROM NhanVien WHERE MaNV = @MaNV";
                sqlc.CommandType = CommandType.Text;
                    sqlc.Parameters.Clear();
                    sqlc.Parameters.Add("MaNV", SqlDbType.NChar);
                sqlc.Parameters["@MaNV"].Value = txt_maNV.Text;
                sqlc.Parameters["@MaNV"].SqlDbType = SqlDbType.NChar;
                sqlc.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công");
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void txt_LuongNV_TextChanged(object sender, EventArgs e)
        {
            if(txt_BonusNV.Text == "" || txt_BonusNV.Text == "NULL" || txt_BonusNV.Text == String.Empty || txt_BonusNV.Text == null)
            {
                return;
            }
            if (txt_LuongNV.Text == "" || txt_LuongNV.Text == "NULL" || txt_LuongNV.Text == String.Empty || txt_LuongNV.Text == null)
            {
                return;
            }
            try
            {
                textBox2.Text = ((Convert.ToDouble(txt_BonusNV.Text) * Convert.ToDouble(txt_LuongNV.Text)) + Convert.ToDouble(txt_LuongNV.Text)).ToString();
            }
            catch (Exception) { return; }
        }

        private void txt_BonusNV_TextChanged(object sender, EventArgs e)
        {
            if (txt_BonusNV.Text == "" || txt_BonusNV.Text == "NULL" || txt_BonusNV.Text == String.Empty || txt_BonusNV.Text == null)
            {
                return;
            }
            if (txt_LuongNV.Text == "" || txt_LuongNV.Text == "NULL" || txt_LuongNV.Text == String.Empty || txt_LuongNV.Text == null)
            {
                return;
            }
            try
            {
                textBox2.Text = ((Convert.ToDouble(txt_BonusNV.Text) * Convert.ToDouble(txt_LuongNV.Text)) + Convert.ToDouble(txt_LuongNV.Text)).ToString();
            }
            catch (Exception) { return; }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            DonThuoc.Delete del;
            if (listView1.Items.Count == 0)
            {
                return;
            }
            else
            {
                del = new DonThuoc.Delete(listView1.SelectedItems);
            }
            del.ShowDialog();
            ResetField(empID);
        }
    }
}
