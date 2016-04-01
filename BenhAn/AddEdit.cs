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

namespace BenhAn
{
    public partial class AddEdit : Form
    {
        ListViewItem sitem;
        public AddEdit()
        {
            InitializeComponent();
            panel13.Enabled = false;
            textBox1.ReadOnly = false;
            panel6.Enabled = true;
            sitem = null;
        }

        public AddEdit(ListViewItem item)
        {
            InitializeComponent();
            if(item == null)
            {
                panel13.Enabled = false;
                textBox1.ReadOnly = false;
                panel6.Enabled = true;
                return;
            }
            else
            {
                panel13.Enabled = true;
                sitem = item;
                textBox1.Text = item.SubItems[0].Text;
                textBox2.Text = item.SubItems[1].Text;
                textBox3.Text = item.SubItems[2].Text;
                DateTime dt = Convert.ToDateTime(item.SubItems[3].Text);
                dateTimePicker1.Value = dt;
                textBox1.ReadOnly = true;
                return;
            }
        }

        private string ListViewItemToString(ListViewItem item)
        {
            string temp = null;
            foreach(ListViewItem.ListViewSubItem sub in item.SubItems)
            {
                temp += sub.Tag.ToString() + " = " + sub.Text + "\n";
            }
            return temp;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(sitem == null)
            {
                MessageBox.Show("Bạn không thể sử dụng chức năng này do không có dữ liệu mẫu.");
                return;
            }
            else
            {
                SqlCommand sqlc = new SqlCommand();
                sqlc.CommandText = "UPDATE TABLE QuanLyPhongkham.dbo.BenhAn SET MaNV = @MaNV, MaBN = @MaBN, NgayLapBAn = @NgayLap WHERE MaBA = @MaBA";
                sqlc.Parameters.Add("@MaBA", SqlDbType.NChar);
                sqlc.Parameters.Add("@MaNV", SqlDbType.NChar);
                sqlc.Parameters.Add("@MaBN", SqlDbType.NChar);
                sqlc.Parameters.Add("@NgayLapBA", SqlDbType.DateTime);
                sqlc.Parameters["@MaBA"].Value = textBox1.Text;
                sqlc.Parameters["@MaNV"].Value = textBox2.Text;
                sqlc.Parameters["@MaBN"].Value = textBox3.Text;
                sqlc.Parameters["@NgayLapBA"].Value = dateTimePicker1.Value;
                sqlc.CommandType = CommandType.Text;
                try
                {
                    if(Connection.sqlcon.State != ConnectionState.Open)
                    {
                        Connection.sqlcon.Open();
                    }
                    sqlc.Connection = Connection.sqlcon;
                    sqlc.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật thành công", "Thành công");
                    this.Close();
                }
                catch(Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi");
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            SqlCommand sqlc = new SqlCommand();
            sqlc.CommandText = "INSERT INTO QuanLyPhongkham.dbo.BenhAn VALUES(@MaBA, @MaNV, @MaBN, @NgayLapBA)";
            sqlc.Parameters.Add("@MaBA", SqlDbType.NChar);
            sqlc.Parameters.Add("@MaNV", SqlDbType.NChar);
            sqlc.Parameters.Add("@MaBN", SqlDbType.NChar);
            sqlc.Parameters.Add("@NgayLapBA", SqlDbType.DateTime);
            sqlc.Parameters["@MaBA"].Value = textBox1.Text;
            sqlc.Parameters["@MaNV"].Value = textBox2.Text;
            sqlc.Parameters["@MaBN"].Value = textBox3.Text;
            sqlc.Parameters["@NgayLapBA"].Value = dateTimePicker1.Value;
            sqlc.CommandType = CommandType.Text;
            try
            {
                if (Connection.sqlcon.State != ConnectionState.Open)
                {
                    Connection.sqlcon.Open();
                }
                sqlc.Connection = Connection.sqlcon;
                sqlc.ExecuteNonQuery();
                MessageBox.Show("Cập nhật thành công", "Thành công");
                this.Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Lỗi");
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sitem == null)
            {
                panel13.Enabled = false;
                panel6.Enabled = true;
                textBox1.ReadOnly = false;
                panel13.Visible = false;
                panel6.Visible = true;
                return;
            }
            else
            {
                panel13.Enabled = true;
                panel6.Enabled = false;
                textBox1.ReadOnly = true;
                panel13.Visible = true;
                panel6.Visible = false;
                return;
            }
        }
    }
}
