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
    public partial class AddEdit : Form
    {
        ListViewItem sitem;
        public AddEdit()
        {
            InitializeComponent();
            panel13.Enabled = false;
            txt_MaTTDT.ReadOnly = false;
            panel6.Enabled = true;
            sitem = null;
        }

        public AddEdit(ListViewItem item)
        {
            InitializeComponent();
            if(item == null)
            {
                panel13.Enabled = false;
                txt_MaTTDT.ReadOnly = false;
                panel6.Enabled = true;
                return;
            }
            else
            {
                panel13.Enabled = true;
                panel6.Enabled = false;
                sitem = item;
                txt_MaTTDT.Text = sitem.SubItems[0].Text;
                txt_MaDonThuoc.Text = sitem.SubItems[1].Text;
                txt_MaBA.Text = sitem.SubItems[2].Text;
                txt_MaNV.Text = sitem.SubItems[3].Text;
                txt_TenThuoc.Text = sitem.SubItems[4].Text;
                txt_LieuDung.Text = sitem.SubItems[5].Text;
                txt_GhiChu.Text = sitem.SubItems[6].Text;
                dtp_NgayLap.Value = Convert.ToDateTime(sitem.SubItems[7].Text);
                txt_MaTTDT.ReadOnly = true;
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
                sqlc.CommandText = "UPDATE QuanLyPhongkham.dbo.ThongTinDonThuoc SET MaTTDT = @MaTTDT, MaDonThuoc = @MaDonThuoc, MaBA = @MaBA, MaNV = @MaNV, TenThuoc = @TenThuoc, LieuDung = @LieuDung, GhiChu = @GhiChu, NgayLap = @NgayLap";
                sqlc.Parameters.Add("@MaTTDT", SqlDbType.NChar);
                sqlc.Parameters["@MaTTDT"].Value = txt_MaTTDT.Text;
                sqlc.Parameters.Add("@MaDonThuoc", SqlDbType.NChar);
                sqlc.Parameters["@MaDonThuoc"].Value = txt_MaDonThuoc.Text;
                sqlc.Parameters.Add("@MaBA", SqlDbType.NChar);
                sqlc.Parameters["@MaBA"].Value = txt_MaBA.Text;
                sqlc.Parameters.Add("@MaNV", SqlDbType.NChar);
                sqlc.Parameters["@MaNV"].Value = txt_MaNV.Text;
                sqlc.Parameters.Add("@TenThuoc", SqlDbType.NVarChar);
                sqlc.Parameters["@TenThuoc"].Value = txt_MaDonThuoc.Text;
                sqlc.Parameters.Add("@LieuDung", SqlDbType.NVarChar);
                sqlc.Parameters["@LieuDung"].Value = txt_MaDonThuoc.Text;
                sqlc.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
                sqlc.Parameters["@GhiChu"].Value = txt_MaDonThuoc.Text;
                sqlc.Parameters.Add("@NgayLap", SqlDbType.DateTime);
                sqlc.Parameters["@NgayLap"].Value = dtp_NgayLap.Value;
                sqlc.CommandType = CommandType.Text;
                try
                {
                    if (Connection.sqlcon.State != ConnectionState.Open)
                    {
                        Connection.sqlcon.Open();
                    }
                    sqlc.Connection = Connection.sqlcon;
                    sqlc.ExecuteNonQuery();
                    MessageBox.Show("Cập nhật dữ liệu thành công", "Thành công");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi");
                }
                
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(sitem != null)
            {
                return;
            }
            SqlCommand sqlc = new SqlCommand();
            sqlc.CommandText = "INSERT INTO QuanLyPhongkham.dbo.ThongTinDonThuoc VALUES(@MaTTDT, @MaDonThuoc, @MaBA, @MaNV, @TenThuoc, @LieuDung, @GhiChu, @NgayLap)";
            sqlc.Parameters.Add("@MaTTDT", SqlDbType.NChar);
            sqlc.Parameters["@MaTTDT"].Value = txt_MaTTDT.Text;
            sqlc.Parameters.Add("@MaDonThuoc", SqlDbType.NChar);
            sqlc.Parameters["@MaDonThuoc"].Value = txt_MaDonThuoc.Text;
            sqlc.Parameters.Add("@MaBA", SqlDbType.NChar);
            sqlc.Parameters["@MaBA"].Value = txt_MaBA.Text;
            sqlc.Parameters.Add("@MaNV", SqlDbType.NChar);
            sqlc.Parameters["@MaNV"].Value = txt_MaNV.Text;
            sqlc.Parameters.Add("@TenThuoc", SqlDbType.NVarChar);
            sqlc.Parameters["@TenThuoc"].Value = txt_TenThuoc.Text;
            sqlc.Parameters.Add("@LieuDung", SqlDbType.NVarChar);
            sqlc.Parameters["@LieuDung"].Value = txt_LieuDung.Text;
            sqlc.Parameters.Add("@GhiChu", SqlDbType.NVarChar);
            sqlc.Parameters["@GhiChu"].Value = txt_GhiChu.Text;
            sqlc.Parameters.Add("@NgayLap", SqlDbType.DateTime);
            sqlc.Parameters["@NgayLap"].Value = dtp_NgayLap.Value;
            sqlc.CommandType = CommandType.Text;
            try
            {
                if (Connection.sqlcon.State != ConnectionState.Open)
                {
                    Connection.sqlcon.Open();
                }
                sqlc.Connection = Connection.sqlcon;
                sqlc.ExecuteNonQuery();
                MessageBox.Show("Thêm dữ liệu thành công", "Thành công");
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
                txt_MaTTDT.ReadOnly = false;
                panel13.Visible = false;
                panel6.Visible = true;
                return;
            }
            else
            {
                panel13.Enabled = true;
                panel6.Enabled = false;
                txt_MaTTDT.ReadOnly = true;
                panel13.Visible = true;
                panel6.Visible = false;
                return;
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
