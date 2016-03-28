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
        ListViewItem item = new ListViewItem();
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
                    txt_maNV.Text = (reader[0] == DBNull.Value ? "NULL" : reader[0].ToString());
                    txt_HoNV.Text = (reader[1] == DBNull.Value ? "NULL" : reader[1].ToString());
                    txt_TenNV.Text = (reader[2] == DBNull.Value ? "NULL" : reader[2].ToString());
                    cb_GTNV.SelectedIndex = (reader[4].ToString() == "False" ? 0 : 1);
                    txt_CVNV.Text = (reader[3] == DBNull.Value ? "NULL" : reader[3].ToString());
                    txt_DThoai.Text = (reader[5] == DBNull.Value ? "NULL" : reader[5].ToString());
                    txt_DiaChi.Text = (reader[6] == DBNull.Value ? "NULL" : reader[6].ToString());
                    txt_BonusNV.Text = (reader[7] == DBNull.Value ? "NULL" : reader[7].ToString());
                    txt_wDay.Text = (reader[8] == DBNull.Value ? "NULL" : reader[8].ToString());
                    reader.Close();
                }

                sqlc.CommandText = "SELECT * FROM BenhAn WHERE MaNV = '" + txt_maNV.Text.Trim() + "'";
                sqlc.CommandType = CommandType.Text;
                reader = sqlc.ExecuteReader();
                if(reader.HasRows)
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
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi truy cập dữ liệu của nhân viên " + eID + ": \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                    if(es is Label || es is ComboBox)
                    {
                        continue;
                    }
                    if (es is TextBox)
                    {
                        if (es.Tag == null)
                        {
                            continue;
                        }
                        if (es.Tag.ToString() == "NOT NULL READ ONLY")
                        {
                            if (intentType != 1)
                            {
                                (es as TextBox).ReadOnly = userIntended;
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
                        if (es.Tag.ToString() == "NOT NULL")
                        {
                            if (es.Text == "" || es.Text == String.Empty || es.Text == "NULL")
                            {
                                c.BackColor = Color.Maroon;
                                c.ForeColor = Color.Yellow;
                            }
                            else
                            {
                                c.BackColor = Color.FromArgb(13, 77, 77);
                                c.ForeColor = Color.White;
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
                        if (es.Tag.ToString() != "NOT NULL READ ONLY")
                        {
                            if (intentType != 1)
                            {
                                (es as TextBox).ReadOnly = userIntended;
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
                        if (es.Tag.ToString() == "NOT NULL")
                        {
                            if (es.Text == "" || es.Text == String.Empty || es.Text == "NULL")
                            {
                                c.BackColor = Color.Maroon;
                                c.ForeColor = Color.Yellow;
                            }
                            else
                            {
                                c.BackColor = Color.FromArgb(13, 77, 77);
                                c.ForeColor = Color.White;
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
            if(listView3.SelectedItems.Count == 0)
            {
                BenhAn.AddEdit ae = new BenhAn.AddEdit(null);
                ae.ShowDialog();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (listView3.SelectedItems.Count == 0)
            {
                return;
            }
            BenhAn.AddEdit ae = new BenhAn.AddEdit(listView3.SelectedItems[0]);
            ae.ShowDialog();
        }
    }
}
