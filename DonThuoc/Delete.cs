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

namespace DonThuoc
{
    public partial class Delete : Form
    {
        public Delete()
        {
            InitializeComponent();
            panel6.Enabled = false;
        }

        public Delete(ListView.SelectedListViewItemCollection items)
        {
            InitializeComponent();
            if (items != null && items.Count >= 1)
            {
                foreach(ListViewItem j in items)
                {
                    listView1.Columns.Add(j.Tag.ToString());
                }
                foreach (ListViewItem i in items)
                {
                    if (!listView1.Items.Contains(i))
                    {
                        listView1.Items.Add(i);
                    }
                }
                panel6.Enabled = true;
            }
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if(listView1.Items.Count == 0)
            {
                panel6.Enabled = false;
                return;
            }
            else
            {
                panel6.Enabled = true;
                return;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listView1.Items.Count == 0)
            {
                return;
            }
            if (MessageBox.Show("Bạn có muốn xóa " + listView1.Items.Count + " records hay không?", "Xác nhận", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                SqlCommand sqlc = new SqlCommand();
                try
                {
                    if (Connection.sqlcon.State != ConnectionState.Open)
                    {
                        Connection.sqlcon.Open();
                    }
                    foreach (ListViewItem item in listView1.Items)
                    {
                        sqlc.CommandText = "DELETE FROM QuanLyPhongKham.dbo.ThongTinDonThuoc WHERE MaTTDT = '" + item.SubItems[0].ToString() + "'";
                        sqlc.CommandType = CommandType.Text;
                        sqlc.Connection = Connection.sqlcon;
                        sqlc.ExecuteNonQuery();
                    }
                    MessageBox.Show("Xóa thành công", "Thành công");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi");
                }
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
