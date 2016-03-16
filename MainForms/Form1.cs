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

namespace MainForms
{
    public partial class Form1 : Form
    {
        private string employeeID = null;
        private bool firstTime = true;
        private Point oldP;
        public Form1()
        {
            InitializeComponent();
            this.label1.Text = this.Text;
            this.pictureBox1.ImageLocation = Application.StartupPath + @"/pictures/clinic_icon.gif";
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void label1_MouseDown(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                oldP = e.Location;
                if ((sender is Label))
                {
                    label1.Cursor = Cursors.SizeAll;
                    label7.Cursor = Cursors.SizeAll;
                }
                else
                {
                    panel4.Cursor = Cursors.SizeAll;
                    foreach(Panel p in panel4.Controls)
                    {
                        p.Cursor = Cursors.SizeAll;
                    }
                }
            }
        }

        private void label1_MouseMove(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                Point newP = e.Location;
                int x = newP.X - oldP.X;
                int y = newP.Y - oldP.Y;
                this.Left += x;
                this.Top += y;
            }
        }

        private void extracheeses(string eID)
        {
            tabControl1.Tabs.Clear();
            tabControl2.Tabs.Clear();
            if (eID == null || eID == "" || eID == "E404")
            {
                if (!tabControl1.Tabs.Contains(tabItem3)){ tabControl1.Tabs.Add(tabItem3); }
                tabControl1.SelectedTab = tabItem3;
                if (!tabControl2.Tabs.Contains(tabItem5)) { tabControl2.Tabs.Add(tabItem5); }
                tabControl2.SelectedTab = tabItem5;
                splitContainer1.Panel2Collapsed = true;
            }
            if (eID.ToUpper().StartsWith("BS"))
            {
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem2;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
            }
            if (eID.ToUpper().StartsWith("YT"))
            {
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem2;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
            }
            if (eID.ToUpper().StartsWith("DA"))
            {
                if (!tabControl1.Tabs.Contains(tabItem1)) { tabControl1.Tabs.Add(tabItem1); }
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem6;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                if (!tabControl1.Tabs.Contains(tabItem6)) { tabControl1.Tabs.Add(tabItem6); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
            }
            if(eID.ToUpper().StartsWith("TST"))
            {
                if (!tabControl1.Tabs.Contains(tabItem1)) { tabControl1.Tabs.Add(tabItem1); }
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                if (!tabControl1.Tabs.Contains(tabItem3)) { tabControl1.Tabs.Add(tabItem3); }
                if (!tabControl1.Tabs.Contains(tabItem6)) { tabControl1.Tabs.Add(tabItem6); }
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                if (!tabControl2.Tabs.Contains(tabItem5)) { tabControl2.Tabs.Add(tabItem5); }
                splitContainer1.Panel2Collapsed = false;
            }
        }

        private void label1_MouseUp(object sender, MouseEventArgs e)
        {
            if(e.Button == MouseButtons.Left)
            {
                oldP = e.Location;
                if ((sender is Label))
                {
                    label1.Cursor = Cursors.Default;
                    label7.Cursor = Cursors.Default;
                }
                else
                {
                    panel4.Cursor = Cursors.Default;
                    foreach (Panel p in panel4.Controls)
                    {
                        p.Cursor = Cursors.Default;
                    }
                }
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            extracheeses(employeeID);
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            DataController.Errors err = DataController.Login.userLogin(txt_username.Text, txt_password.Text, dt);
            if(err.errors != null && dt.Rows.Count == 0)
            {
                MessageBox.Show("Đăng nhập thất bại do lỗi phát sinh từ hệ thống\nThông tin:\n" + err.errors, "Error");
                return;
            }
            if(err.errors == null && dt.Rows.Count == 0)
            {
                MessageBox.Show("Đăng nhập thất bại\nTài khoản hoặc mật khẩu không chính xác, vui lòng thử lại", "Error");
                return;
            }
            else
            {
                MessageBox.Show("Đăng nhập thành công", "Info");
                label7.Text = "Chào mừng,  [" + dt.Rows[0][0].ToString() + "]";
                extracheeses(dt.Rows[0][0].ToString().Trim());
                employeeID = dt.Rows[0][0].ToString().Trim();
                return;
            }
        }
    }
}
