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

namespace PatientInfo
{
    public partial class PForm : Form
    {
        private Point oldP;
        private string patID;
        private bool userIntended;
        private int intentType;
        public PForm()
        {
            InitializeComponent();
            userIntended = true;
            intentType = 0;
            patID = "NULL";
            return;
        }

        public PForm(string patientID, bool userintend, int type)
        {
            InitializeComponent();
            patID = patientID;
            label1.Text = "Thông tin bệnh nhân [" + patID + "] ";
            userIntended = userintend;
            ResetField(patID);
            intentType = type;
        }

        private void ResetField(string patientID)
        {
            if (patID == "NULL")
            {
                return;
            }
            txt_maBN.Text = (patID == "NULL" ? String.Empty : patID);
            
            if(Connection.sqlcon.State != ConnectionState.Closed)
            {
                Connection.sqlcon.Close();
            }
            SqlCommand sqlc = new SqlCommand();
            sqlc.Connection = Connection.sqlcon;
            sqlc.CommandText = "SELECT * FROM QuanLyPhongKham.dbo.BenhNhan WHERE MaBN = @patientID";
            sqlc.CommandType = CommandType.Text;
            if (!sqlc.Parameters.Contains("@patientID"))
            {
                sqlc.Parameters.Add("@patientID", SqlDbType.NChar);
            }
            sqlc.Parameters["@patientID"].Value = patientID.Trim();
            sqlc.Parameters["@patientID"].Size = 10;
            try
            {
                Connection.sqlcon.Open();
                SqlDataReader reader = sqlc.ExecuteReader();
                if(!reader.HasRows)
                {
                    MessageBox.Show("Bệnh nhân " + patientID + "không tồn tại trong hệ thống dữ liệu của phòng khám.Vui lòng kiểm tra hoặc liên hệ quản trị viên để biết thêm.\n", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Connection.sqlcon.Close();
                    reader.Close();
                    return;
                }
                if(reader.HasRows)
                {
                    reader.Read();
                    txt_maBN.Text = (reader["MaBN"] == DBNull.Value ? "NULL" : reader["MaBN"].ToString());
                    txt_HoBN.Text = (reader["HoBN"] == DBNull.Value ? "NULL" : reader["HoBN"].ToString());
                    txt_TenBN.Text = (reader["TenBN"] == DBNull.Value ? "NULL" : reader["TenBN"].ToString());
                    cb_GTBN.SelectedIndex = (reader["GioiTinh"].ToString() == "False" ? 0 : 1);
                    txt_DThoai.Text = (reader["SoDienThoai"] == DBNull.Value ? "NULL" : reader["SoDienThoai"].ToString());
                    txt_DiaChi.Text = (reader["DiaChi"] == DBNull.Value ? "NULL" : reader["DiaChi"].ToString());
                    reader.Close();
                }

                sqlc.Parameters.Clear();

                sqlc.CommandText = "SELECT * FROM QuanLyPhongKham.dbo.BenhAn WHERE MaBN = @patientID";
                sqlc.CommandType = CommandType.Text;
                if (!sqlc.Parameters.Contains("@patientID"))
                {
                    sqlc.Parameters.Add("@patientID", SqlDbType.NChar);
                }
                sqlc.Parameters["@patientID"].Value = patientID.Trim();
                sqlc.Parameters["@patientID"].Size = 10;
                reader = sqlc.ExecuteReader();
                if(!reader.HasRows)
                {
                    reader.Close();
                    return;
                }
                if (reader.HasRows)
                {
                    reader.Read();
                    txt_MaBA.Text = (reader["MaBA"] == DBNull.Value ? "NULL" : reader["MaBA"].ToString());
                    dtp_NgayLapBA.Value = (reader["NgayLapBAn"] == DBNull.Value ? DateTime.Now : Convert.ToDateTime(reader["NgayLapBAn"].ToString()));
                    txt_NguoiLapBA.Text = (reader["MaNV"] == DBNull.Value ? "NULL" : reader["MaNV"].ToString());
                    reader.Close();
                }

                sqlc.Parameters.Clear();

                sqlc.CommandText = "SELECT * FROM QuanLyPhongKham.dbo.ThongTinDonThuoc WHERE MaBA = @MaBA";
                sqlc.CommandType = CommandType.Text;
                if (!sqlc.Parameters.Contains("@MaBA"))
                {
                    sqlc.Parameters.Add("@MaBA", SqlDbType.NChar);
                }
                sqlc.Parameters["@MaBA"].Value = txt_MaBA.Text;
                sqlc.Parameters["@MaBA"].Size = 10;
                reader = sqlc.ExecuteReader();
                if (!reader.HasRows)
                {
                    reader.Close();
                    return;

                }
                if (reader.HasRows)
                {
                    while(reader.Read())
                    {
                        ListViewGroup g = new ListViewGroup();
                        g.Header = reader["MaDonThuoc"].ToString();
                        if(!listView1.Groups.Contains(g))
                        {
                            listView1.Groups.Add(g);
                        }
                        ListViewItem item = new ListViewItem();
                        item.Text = reader["MaTTDT"].ToString();
                        for(int i = 2; i < reader.FieldCount; i++)
                        {
                            ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                            sub.Text = reader[i].ToString();
                            item.SubItems.Add(sub);
                        }
                        if(listView1.Groups.Count >= 1)
                        {
                            listView1.Groups[listView1.Groups.Count - 1].Items.Add(item);
                        }
                        listView1.Items.Add(item);
                    }
                    reader.Close();
                }

                sqlc.Parameters.Clear();

                pictureBox2.Load(@"http://localhost/patientPTS/" + patientID.ToString());
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi truy cập dữ liệu của bệnh nhân " + patientID + ": \n" + ex.Message + " bởi " + ex.Source, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Connection.sqlcon.Close();
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

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
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

        private void expandablePanel5_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            panel10.AutoScroll = expandablePanel5.Expanded;
        }

        private void txt_TextChanged(object sender, EventArgs e)
        {
            TextBox txt = (TextBox)sender;
            if (txt.Text == "NULL" || txt.Text == "" || txt.Text == String.Empty)
            {
                txt.BackColor = Color.Red;
                txt.ForeColor = Color.Yellow;
            }
            else
            {
                txt.BackColor = Color.FromName("Window");
                txt.ForeColor = Color.Black;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            foreach (Panel c in expandablePanel1.Controls)
            {
                foreach (Control es in c.Controls)
                {
                    if (es is Label || es is ComboBox)
                    {
                        continue;
                    }
                    if (es is TextBox)
                    {
                        if (es.Tag == null)
                        {
                            continue;
                        }
                        if (es.Tag.ToString().Contains("IGNORE") && intentType == 2)
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
                    if (es is Label || es is ComboBox)
                    {
                        continue;
                    }
                    if (es is TextBox)
                    {
                        if (es.Tag == null)
                        {
                            continue;
                        }
                        if (es.Tag.ToString().Contains("IGNORE") && intentType == 2)
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
                                bt_add_NV.Enabled = false;
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
        }

        private void PForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            this.Dispose();
        }

        private void PForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void expandablePanel3_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            expandablePanel3.AutoScroll = expandablePanel3.Expanded;
        }

        private void PForm_Load(object sender, EventArgs e)
        {
            timer1.Start();
        }

        private void bt_add_NV_Click(object sender, EventArgs e)
        {
            if(txt_maBN.Text == "" || txt_maBN.Text == String.Empty || txt_maBN.Text == null)
            {
                MessageBox.Show("Vui lòng điền vào mã Bệnh Nhân trước khi tiếp tục");
                return;
            }
            if (txt_MaBA.Text == "" || txt_MaBA.Text == String.Empty || txt_MaBA.Text == null)
            {
                MessageBox.Show("Vui lòng điền vào mã Bệnh Án trước khi tiếp tục");
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
                sqlc.CommandText = "INSERT INTO QuanLyPhongKham.dbo.BenhNhan VALUES ( @MaBN, @HoBN, @TenBN, @GioiTinh, @DienThoai, @DiaChi)";
                sqlc.CommandType = CommandType.Text;
                sqlc.Parameters.Clear();
                if (!sqlc.Parameters.Contains("@MaBN")) { sqlc.Parameters.Add("@MaBN", SqlDbType.NChar); }
                if (!sqlc.Parameters.Contains("@HoBN"))
                {
                    sqlc.Parameters.Add("@HoBN", SqlDbType.NVarChar);
                }
                if (!sqlc.Parameters.Contains("@TenBN"))
                {
                    sqlc.Parameters.Add("@TenBN", SqlDbType.NVarChar);
                }
                if (!sqlc.Parameters.Contains("@GioiTinh"))
                {
                    sqlc.Parameters.Add("@GioiTinh", SqlDbType.Bit);
                }
                if (!sqlc.Parameters.Contains("@DiaChi"))
                {
                    sqlc.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
                }
                if (!sqlc.Parameters.Contains("@DienThoai"))
                {
                    sqlc.Parameters.Add("@DienThoai", SqlDbType.NChar);
                }
                sqlc.Parameters["@MaBN"].Value = txt_maBN.Text;
                sqlc.Parameters["@MaBN"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@HoBN"].Value = txt_HoBN.Text;
                sqlc.Parameters["@HoBN"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@TenBN"].Value = txt_TenBN.Text;
                sqlc.Parameters["@TenBN"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@GioiTinh"].Value = cb_GTBN.SelectedIndex;
                sqlc.Parameters["@GioiTinh"].SqlDbType = SqlDbType.Bit;
                sqlc.Parameters["@DiaChi"].Value = txt_DiaChi.Text;
                sqlc.Parameters["@DiaChi"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@DienThoai"].Value = txt_DThoai.Text;
                sqlc.Parameters["@DienThoai"].SqlDbType = SqlDbType.NChar;

                sqlc.ExecuteNonQuery();

                sqlc.CommandText = "INSERT INTO QuanLyPhongKham.dbo.BenhAn VALUES ( @MaBA, @MaNV, @MaBN, @NgayLapBAn)";
                sqlc.CommandType = CommandType.Text;
                sqlc.Connection = Connection.sqlcon;
                sqlc.Parameters.Clear();
                if (!sqlc.Parameters.Contains("@MaNV"))
                {
                    sqlc.Parameters.Add("@MaNV", SqlDbType.NChar);
                }
                if (!sqlc.Parameters.Contains("@MaBA"))
                {
                    sqlc.Parameters.Add("@MaBA", SqlDbType.NChar);
                }
                if (!sqlc.Parameters.Contains("@MaBN"))
                {
                    sqlc.Parameters.Add("@MaBN", SqlDbType.NChar);
                }
                if (!sqlc.Parameters.Contains("@NgayLapBAn"))
                {
                    sqlc.Parameters.Add("@NgayLapBAn", SqlDbType.DateTime);
                }
                sqlc.Parameters["@MaBA"].Value = txt_MaBA.Text;
                sqlc.Parameters["@MaBA"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@MaNV"].Value = txt_NguoiLapBA.Text;
                sqlc.Parameters["@MaNV"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@MaBN"].Value = txt_maBN.Text;
                sqlc.Parameters["@MaBN"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@NgayLapBAn"].Value = dtp_NgayLapBA.Value;
                sqlc.Parameters["@NgayLapBAn"].SqlDbType = SqlDbType.DateTime;

                sqlc.ExecuteNonQuery();
                MessageBox.Show("Thêm thành công");
                this.Close();
            }
            catch(Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void bt_update_NV_Click(object sender, EventArgs e)
        {
            try
            {
                if (txt_maBN.Text == "" || txt_maBN.Text == String.Empty || txt_maBN.Text == null)
                {
                    MessageBox.Show("Vui lòng điền vào mã Bệnh Nhân trước khi tiếp tục");
                    return;
                }
                if (txt_MaBA.Text == "" || txt_MaBA.Text == String.Empty || txt_MaBA.Text == null)
                {
                    MessageBox.Show("Vui lòng điền vào mã Bệnh Án trước khi tiếp tục");
                    return;
                }
                if (Connection.sqlcon.State != ConnectionState.Open)
                {
                    Connection.sqlcon.Open();
                }
                SqlCommand sqlc = new SqlCommand();
                sqlc.Connection = Connection.sqlcon;
                sqlc.CommandText = "UPDATE QuanLyPhongKham.dbo.BenhNhan SET "+ 
                    " MaBN = @MaBN, HoBN = @HoBN, TenBN = @TenBN, GioiTinh = @GioiTinh, DiaChi = @DiaChi, SoDienThoai = @DienThoai WHERE MaBN = @MaBN";
                sqlc.CommandType = CommandType.Text;
                sqlc.Connection = Connection.sqlcon;
                sqlc.Parameters.Clear();
                if (!sqlc.Parameters.Contains("@MaBN")) { sqlc.Parameters.Add("@MaBN", SqlDbType.NChar); }
                if (!sqlc.Parameters.Contains("@HoBN"))
                {
                    sqlc.Parameters.Add("@HoBN", SqlDbType.NVarChar);
                }
                if (!sqlc.Parameters.Contains("@TenBN"))
                {
                    sqlc.Parameters.Add("@TenBN", SqlDbType.NVarChar);
                }
                if (!sqlc.Parameters.Contains("@GioiTinh"))
                {
                    sqlc.Parameters.Add("@GioiTinh", SqlDbType.Bit);
                }
                if (!sqlc.Parameters.Contains("@DiaChi"))
                {
                    sqlc.Parameters.Add("@DiaChi", SqlDbType.NVarChar);
                }
                if (!sqlc.Parameters.Contains("@DienThoai"))
                {
                    sqlc.Parameters.Add("@DienThoai", SqlDbType.NChar);
                }
                sqlc.Parameters["@MaBN"].Value = txt_maBN.Text;
                sqlc.Parameters["@MaBN"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@HoBN"].Value = txt_HoBN.Text;
                sqlc.Parameters["@HoBN"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@TenBN"].Value = txt_TenBN.Text;
                sqlc.Parameters["@TenBN"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@GioiTinh"].Value = cb_GTBN.SelectedIndex;
                sqlc.Parameters["@GioiTinh"].SqlDbType = SqlDbType.Bit;
                sqlc.Parameters["@DiaChi"].Value = txt_DiaChi.Text;
                sqlc.Parameters["@DiaChi"].SqlDbType = SqlDbType.NVarChar;
                sqlc.Parameters["@DienThoai"].Value = txt_DThoai.Text;
                sqlc.Parameters["@DienThoai"].SqlDbType = SqlDbType.NChar;

                sqlc.ExecuteNonQuery();

                sqlc.CommandText = "UPDATE QuanLyPhongKham.dbo.BenhAn "
                    + "SET " +
                    "MaBA = @MaBA, MaNV = @MaNV, MaBN = @MaBN, NgayLapBAn = @NgayLapBAn WHERE MaBN = @MaBN";
                sqlc.CommandType = CommandType.Text;
                sqlc.Connection = Connection.sqlcon;
                sqlc.Parameters.Clear();
                if (!sqlc.Parameters.Contains("@MaNV"))
                {
                    sqlc.Parameters.Add("@MaNV", SqlDbType.NChar);
                }
                if (!sqlc.Parameters.Contains("@MaBA"))
                {
                    sqlc.Parameters.Add("@MaBA", SqlDbType.NChar);
                }
                if (!sqlc.Parameters.Contains("@MaBN"))
                {
                    sqlc.Parameters.Add("@MaBN", SqlDbType.NChar);
                }
                if (!sqlc.Parameters.Contains("@NgayLapBAn"))
                {
                    sqlc.Parameters.Add("@NgayLapBAn", SqlDbType.DateTime);
                }
                sqlc.Parameters["@MaBA"].Value = txt_MaBA.Text;
                sqlc.Parameters["@MaBA"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@MaNV"].Value = txt_NguoiLapBA.Text;
                sqlc.Parameters["@MaNV"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@MaBN"].Value = txt_maBN.Text;
                sqlc.Parameters["@MaBN"].SqlDbType = SqlDbType.NChar;
                sqlc.Parameters["@NgayLapBAn"].Value = dtp_NgayLapBA.Value;
                sqlc.Parameters["@NgayLapBAn"].SqlDbType = SqlDbType.DateTime;

                sqlc.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void bt_delete_NV_Click(object sender, EventArgs e)
        {
            if (txt_maBN.Text == "" || txt_maBN.Text == String.Empty || txt_maBN.Text == null)
            {
                MessageBox.Show("Vui lòng điền vào mã Bệnh Nhân trước khi tiếp tục");
                return;
            }
            if (txt_MaBA.Text == "" || txt_MaBA.Text == String.Empty || txt_MaBA.Text == null)
            {
                MessageBox.Show("Vui lòng điền vào mã Bệnh Án trước khi tiếp tục");
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

                sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.ThongTinDonThuoc WHERE MaBN = @MaBN";
                sqlc.CommandType = CommandType.Text;
                sqlc.Connection = Connection.sqlcon;
                sqlc.Parameters.Clear();
                if (!sqlc.Parameters.Contains("@MaBN"))
                {
                    sqlc.Parameters.Add("@MaBN", SqlDbType.NChar);
                }
                sqlc.Parameters["@MaBN"].Value = txt_maBN.Text;
                sqlc.Parameters["@MaBN"].SqlDbType = SqlDbType.NChar;

                sqlc.ExecuteNonQuery();

                sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.BenhAn WHERE MaBN = @MaBN";
                sqlc.CommandType = CommandType.Text;
                sqlc.Connection = Connection.sqlcon;
                sqlc.Parameters.Clear();
                if (!sqlc.Parameters.Contains("@MaBN"))
                {
                    sqlc.Parameters.Add("@MaBN", SqlDbType.NChar);
                }
                sqlc.Parameters["@MaBN"].Value = txt_maBN.Text;
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
                sqlc.Parameters["@MaBN"].Value = txt_maBN.Text;
                sqlc.Parameters["@MaBN"].SqlDbType = SqlDbType.NChar;

                sqlc.ExecuteNonQuery();
                MessageBox.Show("Xóa thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }
    }
}
