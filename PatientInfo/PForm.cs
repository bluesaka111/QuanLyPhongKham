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
            txt_maBN.Text = (patID == "NULL" ? String.Empty : patID);
            if(Connection.sqlcon.State != ConnectionState.Closed)
            {
                Connection.sqlcon.Close();
            }
            SqlCommand sqlc = new SqlCommand();
            sqlc.Connection = Connection.sqlcon;
            sqlc.CommandText = "QuanLyPhongKham.dbo.usp_GetFullPatientInfo";
            sqlc.CommandType = CommandType.StoredProcedure;
            if (!sqlc.Parameters.Contains("@patientID"))
            {
                sqlc.Parameters.Add("@patientID", SqlDbType.NChar);
                sqlc.Parameters["@patientID"].Value = patientID;
                sqlc.Parameters["@patientID"].Size = 10;
            }
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
                    txt_maBN.Text = (reader[0] == DBNull.Value ? "NULL" : reader[0].ToString());
                    txt_HoBN.Text = (reader[1] == DBNull.Value ? "NULL" : reader[1].ToString());
                    txt_TenBN.Text = (reader[2] == DBNull.Value ? "NULL" : reader[2].ToString());
                    cb_GTBN.SelectedIndex = (reader[3].ToString() == "False" ? 0 : 1);
                    txt_DThoai.Text = (reader[4] == DBNull.Value ? "NULL" : reader[4].ToString());
                    txt_DiaChi.Text = (reader[5] == DBNull.Value ? "NULL" : reader[5].ToString());
                    txt_MaBA.Text = (reader[6] == DBNull.Value ? "NULL" : reader[6].ToString());
                    txt_NgayLapBA.Text = (reader[7] == DBNull.Value ? "NULL" : reader[7].ToString());
                    txt_NguoiLapBA.Text = (reader[8] == DBNull.Value ? "NULL" : reader[8].ToString());
                    reader.Close();
                }
                
                sqlc.CommandText = "SELECT MaTTDT FROM QuanLyPhongKham.dbo.ThongTinDonThuoc";
                sqlc.CommandType = CommandType.Text;
                reader = sqlc.ExecuteReader();
                if(reader.HasRows)
                {
                    while(reader.Read())
                    {
                        comboBox2.Items.Add(reader[0].ToString().Trim());
                    }
                    reader.Close();
                }
                else
                {
                    reader.Close();
                }
            }
            catch(Exception ex)
            {
                MessageBox.Show("Lỗi khi truy cập dữ liệu của bệnh nhân " + patientID + ": \n" + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }
}
