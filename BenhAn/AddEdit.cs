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
            sitem = null;
            textBox1.ReadOnly = false;
        }

        public AddEdit(ListViewItem item)
        {
            InitializeComponent();
            if(item == null)
            {
                panel13.Enabled = false;
                textBox1.ReadOnly = false;
                return;
            }
            else
            {
                panel13.Enabled = true;
                sitem = item;
                textBox1.Text = item.SubItems[0].Text;
                comboBox1.SelectedItem = item.SubItems[1].Text;
                comboBox2.SelectedItem = item.SubItems[2].Text;
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
                ListViewItem item2 = sitem;
                item2.SubItems[0].Text = textBox1.Text;
                item2.SubItems[1].Text = comboBox1.SelectedItem.ToString();
                item2.SubItems[2].Text = comboBox2.SelectedItem.ToString();
                if(Dialog.MainForm.DShowDialog(MessageBoxButtons.OKCancel, "Xác nhận", "Bạn có muốn thay đổi " + ListViewItemToString(sitem) + "\nthành\n" + ListViewItemToString(item2) + "/nhay không?") == DialogResult.OK)
                {
                    SqlCommand sqlc = new SqlCommand();
                    sqlc.CommandText = "UPDATE TABLE QuanLyPhongkham.dbo.BenhAn SET MaNV = N'" + comboBox1.SelectedItem.ToString() + "', MaBN = N'" + comboBox2.SelectedItem.ToString() + "', NgayLapBAn = '" + dateTimePicker1.Value.ToString() + "' WHERE MaBA = '" + textBox1.Text + "'";
                    sqlc.CommandType = CommandType.Text;
                    try
                    {
                        if(Connection.sqlcon.State != ConnectionState.Open)
                        {
                            Connection.sqlcon.Open();
                        }
                        sqlc.Connection = Connection.sqlcon;
                        sqlc.ExecuteNonQuery();
                        Dialog.MainForm.DShowDialog(MessageBoxButtons.OK, "Thành công", "Cập nhật thành công");
                    }
                    catch(Exception ex)
                    {
                        Dialog.MainForm.DShowDialog(MessageBoxButtons.OK, "Lỗi", ex.Message);
                    }
                }
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            ListViewItem item2 = sitem;
            item2.SubItems[0].Text = textBox1.Text;
            item2.SubItems[1].Text = comboBox1.SelectedItem.ToString();
            item2.SubItems[2].Text = comboBox2.SelectedItem.ToString();
            if (Dialog.MainForm.DShowDialog(MessageBoxButtons.OKCancel, "Xác nhận", "Bạn có muốn thêm " + ListViewItemToString(item2) + "/nvào cơ sở dữ liệu hay không?") == DialogResult.OK)
            {
                SqlCommand sqlc = new SqlCommand();
                sqlc.CommandText = "INSERT INTO QuanLyPhongkham.dbo.BenhAn VALUES(N'" + comboBox1.SelectedItem.ToString() + "', N'" + comboBox2.SelectedItem.ToString() + "', '" + dateTimePicker1.Value.ToString() + "')";
                sqlc.CommandType = CommandType.Text;
                try
                {
                    if (Connection.sqlcon.State != ConnectionState.Open)
                    {
                        Connection.sqlcon.Open();
                    }
                    sqlc.Connection = Connection.sqlcon;
                    sqlc.ExecuteNonQuery();
                    Dialog.MainForm.DShowDialog(MessageBoxButtons.OK, "Thành công", "Thêm dữ liệu thành công");
                }
                catch (Exception ex)
                {
                    Dialog.MainForm.DShowDialog(MessageBoxButtons.OK, "Lỗi", ex.Message);
                }
            }
        }
    }
}
