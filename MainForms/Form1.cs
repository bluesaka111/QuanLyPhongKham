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
using DevComponents.DotNetBar;
using System.Data.SqlClient;
using System.Threading;

namespace MainForms
{
    public partial class Form1 : Form
    {
        private Point oldP;
        StoredData userdata;
        DataTable patientDT = new DataTable();
        DataTable employeeDT = new DataTable();
        bool recentUpdateEmployee = false;
        bool recentUpdatePatient = false;
        int timesLeftbeforeUpdate = 30;
        bool bW1Stopped = true;
        bool bW2Stopped = true;
        public Form1()
        {
            InitializeComponent();
            this.label1.Text = this.Text;
            userdata = new StoredData();
            listView1.Enabled = false;
            listView2.Enabled = false;
            pictureBox2.Hide();
            pictureBox3.Hide();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            if(backgroundWorker1.IsBusy || backgroundWorker2.IsBusy)
            {
                MessageBox.Show("Vui lòng chờ cho đến khi tiến trình hoàn tất trước khi thoát ứng dụng");
                return;
            }
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
            Errors err = new Errors();
            if (eID == String.Empty || eID == "" || eID == "E404")
            {
                if (!tabControl1.Tabs.Contains(tabItem3)){ tabControl1.Tabs.Add(tabItem3); }
                tabControl1.SelectedTab = tabItem3;
                if (!tabControl2.Tabs.Contains(tabItem5)) { tabControl2.Tabs.Add(tabItem5); }
                tabControl2.SelectedTab = tabItem5;
                splitContainer1.Panel2Collapsed = true;
                pictureBox2.Hide();
                pictureBox3.Hide();
            }
            if (eID.ToUpper().StartsWith("BS"))
            {
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem2;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
                pictureBox2.Hide();
                pictureBox3.Hide();
                //LoadAll(true);
                timesLeftbeforeUpdate = 1;
                timer2.Enabled = true;
            }
            if (eID.ToUpper().StartsWith("YT"))
            {
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem2;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
                pictureBox2.Hide();
                pictureBox3.Hide();
                //LoadAll(true);
                timesLeftbeforeUpdate = 1;
                timer2.Enabled = true;
            }
            if (eID.ToUpper().StartsWith("DA"))
            {
                if (!tabControl1.Tabs.Contains(tabItem1)) { tabControl1.Tabs.Add(tabItem1); }
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem1;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                if (!tabControl2.Tabs.Contains(tabItem5)) { tabControl2.Tabs.Add(tabItem5); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
                pictureBox2.Show();
                pictureBox3.Show();
                //LoadAll(false);
                timesLeftbeforeUpdate = 1;
                timer2.Enabled = true;
            }
            if(eID.ToUpper().StartsWith("TST"))
            {
                if (!tabControl1.Tabs.Contains(tabItem1)) { tabControl1.Tabs.Add(tabItem1); }
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                if (!tabControl1.Tabs.Contains(tabItem3)) { tabControl1.Tabs.Add(tabItem3); }
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                if (!tabControl2.Tabs.Contains(tabItem5)) { tabControl2.Tabs.Add(tabItem5); }
                splitContainer1.Panel2Collapsed = false;
                pictureBox2.Show();
                pictureBox3.Show();
                //LoadAll(false);
                timesLeftbeforeUpdate = 1;
                timer2.Enabled = true;
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
            extracheeses(userdata.employeeID);
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            DataTable dt = new DataTable();
            userdata.userid = txt_username.Text;
            userdata.passcode = txt_password.Text;
            Errors err = new Errors();
            Login.userLogin(userdata, dt, err);
            if(err.errors != String.Empty && dt.Rows.Count == 0)
            {
                MessageBox.Show("Đăng nhập thất bại do lỗi phát sinh từ hệ thống\nThông tin:\n" + err.errors, "Error");
                userdata.userid = String.Empty;
                userdata.passcode = String.Empty;
                userdata.employeeID = String.Empty;
                txt_password.ResetText();
                return;
            }
            if(err.errors == String.Empty && dt.Rows.Count == 0)
            {
                MessageBox.Show("Đăng nhập thất bại\nTài khoản hoặc mật khẩu không chính xác, vui lòng thử lại", "Error");
                userdata.userid = String.Empty;
                userdata.passcode = String.Empty;
                userdata.employeeID = String.Empty;
                txt_password.ResetText();
                return;
            }
            else
            {
                MessageBox.Show("Đăng nhập thành công", "Info");
                label7.Text = "Xin chào, [" + dt.Rows[0][0].ToString().Trim() + "] " + dt.Rows[0][2].ToString() + " " + dt.Rows[0][1].ToString();
                extracheeses(dt.Rows[0][0].ToString().Trim());
                userdata.employeeID = dt.Rows[0][0].ToString().Trim();
                userdata.userid = txt_username.Text;
                userdata.passcode = txt_password.Text;
                txt_username.ResetText();
                txt_password.ResetText();
                return;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            bW1Stopped = false;
            timer2.Enabled = false;
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            foreach (DataColumn dc in employeeDT.Columns)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                ColumnHeader ch = new ColumnHeader();
                ch.Text = dc.ColumnName;
                ch.Width = listView1.Width / employeeDT.Columns.Count;
                backgroundWorker1.ReportProgress(0, ch);
            }
            foreach (DataRow dr in employeeDT.Rows)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                ListViewItem item = new ListViewItem();
                item.Text = dr[0].ToString();
                
                for (int i = 0; i < employeeDT.Columns.Count; i++)
                {
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                    sub.Text = dr[i].ToString();
                    item.SubItems.Add(sub);
                }
                backgroundWorker1.ReportProgress(10, item);
            }
            if (backgroundWorker1.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            if (e.ProgressPercentage == 0)
            {
                listView1.Columns.Add((e.UserState as ColumnHeader));
                Thread.Sleep(150);
            }
            if (e.ProgressPercentage == 10)
            {
                listView1.Items.Add((e.UserState as ListViewItem));
                progressBar1.PerformStep();
                Thread.Sleep(150);
            }
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listView1.GridLines = true;
            listView1.Enabled = true;
            timer2.Enabled = true;
            timesLeftbeforeUpdate = 30;
            bW1Stopped = true;
            if (progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
                progressBar1.Hide();
            }
        }

        private void CalculatePage(ComboBox cb, int totalRows, int perPage)
        {
            int tempvalue = totalRows ;
            cb.Items.Clear();
            int i = 0;
            while (true)
            {
                if (tempvalue < perPage && tempvalue == 0)
                {
                    cb.Items.Add("Từ " + (i * perPage).ToString() + " đến " + totalRows.ToString());
                    break;
                }
                if (tempvalue < perPage && tempvalue != 0)
                {
                    cb.Items.Add("Từ " + (i * perPage + 1).ToString() + " đến " + totalRows.ToString());
                    break;
                }
                else
                {
                    cb.Items.Add("Từ " + (i * perPage + 1).ToString() + " đến " + ((i + 1) * perPage).ToString());
                    i++;
                    tempvalue = tempvalue - perPage;
                }
            }
            if (!cb.Items.Contains("Từ 1 đến " + totalRows.ToString()))
            {
                cb.Items.Insert(0,"Từ 1 đến " + totalRows.ToString());
            }
            cb.SelectedIndex = (cb.Items.Count == 1 ? 0 : 1);
        }

        private void LoadAll(bool patientonly)
        {
            Errors err = new Errors();
            listView1.Clear();
            listView1.GridLines = false;
            listView2.Clear();
            listView2.GridLines = false;
            timer2.Enabled = false;
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            #region Patient
            int temp = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.BenhNhan", err);
            if (err.errors == String.Empty)
            {
                CalculatePage(comboBox2, temp, 250);
                Function.LoadPatientTableStruct(userdata, patientDT, 0, 250, err);
                if (err.errors != String.Empty) { MessageBox.Show("Fill table Patient error" + err.errors); }
                progressBar1.Maximum = employeeDT.Rows.Count;
                if (!backgroundWorker2.IsBusy) { backgroundWorker2.RunWorkerAsync(); } else { MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau"); }
            }
            else
            {
                MessageBox.Show("Unable to read Patient Info" + err.errors);
            }
            #endregion
            #region Employee
            if (!patientonly)
            {
                int temp2 = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.NhanVien", err);
                if (err.errors == String.Empty)
                {
                    CalculatePage(comboBox1, temp, 250);
                    Function.LoadEmployeeTableStruct(userdata, employeeDT, 0, 250, err);
                    if (err.errors != String.Empty) { MessageBox.Show("Fill table Patient error" + err.errors); }
                    progressBar1.Maximum = employeeDT.Rows.Count + patientDT.Rows.Count;

                    if (!backgroundWorker1.IsBusy) { backgroundWorker1.RunWorkerAsync(); } else { MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau"); }
                }
                else
                {
                    MessageBox.Show("Unable to read Employee Info" + err.errors);
                }
            }
            #endregion
        }

        private void LoadAll(bool patientonly, int start, int amount)
        {
            Errors err = new Errors();
            #region Patient
            int temp = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.BenhNhan", err);
            progressBar1.Value = 0;
            listView1.Clear();
            listView1.GridLines = false;
            listView2.Clear();
            listView2.GridLines = false;
            timer2.Enabled = false;
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            if (err.errors == String.Empty)
            {
                CalculatePage(comboBox2, temp, 250);
                Function.LoadPatientTableStruct(userdata, patientDT, start, amount, err);
                if (err.errors != String.Empty) { MessageBox.Show("Fill table Patient error" + err.errors); }
                progressBar1.Maximum = employeeDT.Rows.Count;
                if (!backgroundWorker2.IsBusy) { backgroundWorker2.RunWorkerAsync(); } else { MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau"); }
            }
            else
            {
                MessageBox.Show("Unable to read Patient Info" + err.errors);
                return;
            }
            #endregion
            #region Employee
            if (!patientonly)
            {
                temp = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.NhanVien", err);
                if (err.errors == String.Empty)
                {
                    CalculatePage(comboBox1, temp, 250);
                    Function.LoadEmployeeTableStruct(userdata, employeeDT, start, amount, err);
                    if (err.errors != String.Empty) { MessageBox.Show("Fill table Patient error" + err.errors); return; }
                    progressBar1.Maximum = employeeDT.Rows.Count + patientDT.Rows.Count;
                    if (!backgroundWorker1.IsBusy) { backgroundWorker1.RunWorkerAsync(); } else { MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau"); }
                }
                else
                {
                    MessageBox.Show("Unable to read Employee Info" + err.errors);
                    return;
                }
            }
            #endregion
        }

        private void LoadPatient(int start, int amount)
        {
            Errors err = new Errors();
            progressBar1.Value = 0;
            #region Patient
            int temp = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.BenhNhan", err);
            listView2.Clear();
            listView2.GridLines = false;
            listView2.Enabled = false;
            timer2.Enabled = false;
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            if (err.errors == String.Empty)
            {
                CalculatePage(comboBox2, temp, 250);
                Function.LoadPatientTableStruct(userdata, patientDT, start, amount, err);
                if (err.errors != String.Empty) { MessageBox.Show("Fill table Patient error" + err.errors); return; }
                progressBar1.Maximum = patientDT.Rows.Count;
                if (!backgroundWorker2.IsBusy) { backgroundWorker2.RunWorkerAsync(); } else { MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau"); }
            }
            else
            {
                MessageBox.Show("Unable to read Patient Info" + err.errors);
                return;
            }
            #endregion
        }

        private void LoadEmployee(int start, int amount)
        {
            Errors err = new Errors();
            progressBar1.Value = 0;
            #region Patient
            int temp = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.NhanVien", err);
            listView1.Clear();
            listView1.Enabled = false;
            listView1.GridLines = false;
            timer2.Enabled = false;
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            if (err.errors == String.Empty)
            {
                CalculatePage(comboBox1, temp, 250);
                Function.LoadEmployeeTableStruct(userdata, employeeDT, start, amount, err);
                if (err.errors != String.Empty) { MessageBox.Show("Fill table Patient error" + err.errors); return; }
                progressBar1.Maximum = employeeDT.Rows.Count;
                if (!backgroundWorker1.IsBusy) { backgroundWorker1.RunWorkerAsync(); } else { MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau"); }
                return;
            }
            else
            {
                MessageBox.Show("Unable to read Patient Info" + err.errors);
                return;
            }
            #endregion
        }

        private void button2_Click(object sender, EventArgs e)
        {
            string[] spliter = { " đến " };
            string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
            string[] temp = (temp2).Split(spliter, StringSplitOptions.None);
            if (bW1Stopped)
            {
                LoadEmployee(Convert.ToInt32(temp[0]) - 1, Convert.ToInt32(temp[1]) - Convert.ToInt32(temp[0]));
            }
            else
            {
                MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau");
                return;
            }
        }

        private void listView2_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //e.Item = ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            string[] spliter = { " đến " };
            string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
            string[] temp = (temp2).Split(spliter, StringSplitOptions.None);
            listView2.Enabled = false;
            timer2.Enabled = false;
            if (bW1Stopped)
            {
                LoadPatient(Convert.ToInt32(temp[0]) - 1, Convert.ToInt32(temp[1]) - Convert.ToInt32(temp[0]));
            }
            else
            {
                MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau");
                return;
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
            bW2Stopped = false;
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
            foreach(DataColumn dc in patientDT.Columns)
            {
                if (backgroundWorker2.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                ColumnHeader ch = new ColumnHeader();
                ch.Text = dc.ColumnName;
                ch.Width = listView2.Width / patientDT.Columns.Count;
                backgroundWorker2.ReportProgress(0, ch);
            }
            foreach(DataRow dr in patientDT.Rows)
            {
                if (backgroundWorker2.CancellationPending)
                {
                    e.Cancel = true;
                    break;
                }
                ListViewItem item = new ListViewItem();
                item.Text = dr[0].ToString();
                for(int i = 0; i < patientDT.Columns.Count; i++)
                {
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                    sub.Text = dr[i].ToString();
                    item.SubItems.Add(sub);
                }
                backgroundWorker2.ReportProgress(10, item);
            }
            if (backgroundWorker2.CancellationPending)
            {
                e.Cancel = true;
                return;
            }
        }

        private void backgroundWorker2_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            if (e.ProgressPercentage == 0)
            {
                listView2.Columns.Add((e.UserState as ColumnHeader));
                Thread.Sleep(150);
            }
            if(e.ProgressPercentage == 10)
            {
                listView2.Items.Add((e.UserState as ListViewItem));
                progressBar1.PerformStep();
                Thread.Sleep(150);
            }
        }

        private void backgroundWorker2_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            listView2.GridLines = true;
            listView2.Enabled = true;
            timer2.Enabled = true;
            timesLeftbeforeUpdate = 30;
            bW2Stopped = true;
            if(progressBar1.Value == progressBar1.Maximum)
            {
                progressBar1.Value = 0;
                progressBar1.Hide();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            timesLeftbeforeUpdate--;
            label9.Text = "Hệ thống sẽ đồng bộ dữ liệu với máy chủ sau " + timesLeftbeforeUpdate + " giây nữa.";
            if (timesLeftbeforeUpdate == 0)
            {
                if ((userdata.employeeID.StartsWith("BS") || userdata.employeeID.StartsWith("YT")))
                {
                    if (recentUpdatePatient)
                    {
                        recentUpdatePatient = false;
                        timesLeftbeforeUpdate = 30;
                        return;
                    }
                    if (patientDT.Columns.Count >= 1 && patientDT.Rows.Count >= 1)
                    {
                        patientDT.Reset();
                    }
                    listView2.Clear();
                    if (comboBox2.Items.Count == 0)
                    {
                        LoadAll(true);
                    }
                    else
                    {
                        string[] spliter = { " đến " };
                        string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                        string[] temp = (temp2).Split(spliter, StringSplitOptions.None);
                        LoadPatient(Convert.ToInt32(temp[0]) - 1, Convert.ToInt32(temp[1]) - Convert.ToInt32(temp[0]));
                    }

                    recentUpdatePatient = false;
                    timesLeftbeforeUpdate = 30;
                    label9.Text = "Hệ thống sẽ đồng bộ dữ liệu với máy chủ sau " + timesLeftbeforeUpdate + " giây nữa.";
                    return;
                }
                if ((userdata.employeeID.StartsWith("DA") || userdata.employeeID.StartsWith("TST")))
                {
                    if (recentUpdatePatient)
                    {
                        recentUpdatePatient = false;
                        timesLeftbeforeUpdate = 30;
                        return;
                    }
                    if (recentUpdateEmployee)
                    {
                        recentUpdateEmployee = false;
                        timesLeftbeforeUpdate = 30;
                        return;
                    }
                    if (patientDT.Columns.Count >= 1 && patientDT.Rows.Count >= 1)
                    {
                        patientDT.Reset();
                    }
                    if (employeeDT.Columns.Count >= 1 && employeeDT.Rows.Count >= 1)
                    {
                        patientDT.Reset();
                    }
                    listView1.Clear();
                    listView2.Clear();
                    if (comboBox2.Items.Count == 0)
                    {
                        LoadAll(false);
                    }
                    else
                    {
                        string[] spliter = { " đến " };
                        string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                        string[] temp = (temp2).Split(spliter, StringSplitOptions.None);
                        LoadAll(false, Convert.ToInt32(temp[0]) - 1, Convert.ToInt32(temp[1]) - Convert.ToInt32(temp[0]));
                    }
                    recentUpdatePatient = false;
                    recentUpdateEmployee = false;
                    timesLeftbeforeUpdate = 30;
                    label9.Text = "Hệ thống sẽ đồng bộ dữ liệu với máy chủ sau " + timesLeftbeforeUpdate + " giây nữa.";
                    return;
                }
            }
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
            timer2.Enabled = false;
            listView1.Clear();
            listView2.Clear();
            patientDT.Dispose();
            employeeDT.Dispose();
            Connection.sqlcon.Dispose();
        }

        private void pictureBox3_MouseDown(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources._1458324747_Streamline_75;
            return;
        }

        private void pictureBox3_MouseUp(object sender, MouseEventArgs e)
        {
            pictureBox3.Image = Properties.Resources._1458323745_settings_24;
            return;
        }
    }
}
