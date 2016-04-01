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
using DTObject;

namespace MainForms
{
    public partial class Form1 : Form
    {
        private Point oldP;
        UserInfo userdata;
        DataTable patientDT = new DataTable();
        DataTable employeeDT = new DataTable();
        bool recentUpdateEmployee = false;
        bool recentUpdatePatient = false;
        int timesLeftbeforeUpdate = 30;
        bool bW1Stopped = true;
        bool bW2Stopped = true;
        bool haltAllProcess = false;
        public Form1()
        {
            InitializeComponent();
            this.label1.Text = this.Text;
            userdata = new UserInfo();
            listView1.Enabled = false;
            listView2.Enabled = false;
            ActionForm.Form1.commandRun = true;
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            if(backgroundWorker1.IsBusy || backgroundWorker2.IsBusy)
            {
                MessageBox.Show("Vui lòng chờ cho đến khi tiến trình hoàn tất trước khi thoát ứng dụng", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
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
                if (!tabControl1.Tabs.Contains(tabItem3)) { tabControl1.Tabs.Add(tabItem3); }
                tabControl1.SelectedTab = tabItem3;
                splitContainer1.Panel2Collapsed = true;
                label7.Text = "Xin chào, [ khách ]";
            }
            else if (eID.ToUpper().StartsWith("BS"))
            {
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem2;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
                //LoadAll(true);
                timesLeftbeforeUpdate = 3;
                haltAllProcess = false;
                timer2.Enabled = true;
                label7.Text = "Xin chào, [ " + userdata.employeeID + " ] " + userdata.employeeRnk + " " + userdata.StrName;
            }
            else if (eID.ToUpper().StartsWith("YT"))
            {
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem2;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
                //LoadAll(true);
                timesLeftbeforeUpdate = 3;
                haltAllProcess = false;
                timer2.Enabled = true;
                label7.Text = "Xin chào, [ " + userdata.employeeID + " ] " + userdata.employeeRnk + " " + userdata.StrName;
            }
            else if (eID.ToUpper().StartsWith("QT") || eID.ToUpper().StartsWith("VQT"))
            {
                if (!tabControl1.Tabs.Contains(tabItem1)) { tabControl1.Tabs.Add(tabItem1); }
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                tabControl1.SelectedTab = tabItem1;
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                tabControl2.SelectedTab = tabItem4;
                splitContainer1.Panel2Collapsed = false;
                //LoadAll(false);
                timesLeftbeforeUpdate = 3;
                haltAllProcess = false;
                timer2.Enabled = true;
                label7.Text = "Xin chào, [ " + userdata.employeeID + " ] " + userdata.employeeRnk + " " + userdata.StrName;
            }
            else if (eID.ToUpper().StartsWith("TST"))
            {
                if (!tabControl1.Tabs.Contains(tabItem1)) { tabControl1.Tabs.Add(tabItem1); }
                if (!tabControl1.Tabs.Contains(tabItem2)) { tabControl1.Tabs.Add(tabItem2); }
                if (!tabControl1.Tabs.Contains(tabItem3)) { tabControl1.Tabs.Add(tabItem3); }
                if (!tabControl2.Tabs.Contains(tabItem4)) { tabControl2.Tabs.Add(tabItem4); }
                splitContainer1.Panel2Collapsed = false;
                //LoadAll(false);
                timesLeftbeforeUpdate = 3;
                haltAllProcess = false;
                timer2.Enabled = true;
                label7.Text = "Xin chào, [ " + userdata.employeeID + " ] " + userdata.employeeRnk + " " + userdata.StrName;
            }
            else
            {
                MessageBox.Show("Nhóm mã người dùng của bạn hiện không được phép sử dụng hệ thống.\nVui lòng liên hệ quản trị viên hệ thống để biết thêm chi tiết", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
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
                MessageBox.Show("Đăng nhập thất bại do lỗi phát sinh từ hệ thống\nThông tin:\n" + err.errors, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                userdata.userid = String.Empty;
                userdata.passcode = String.Empty;
                userdata.employeeID = String.Empty;
                txt_password.ResetText();
                dt.Dispose();
                return;
            }
            if(err.errors == String.Empty && dt.Rows.Count == 0)
            {
                MessageBox.Show("Đăng nhập thất bại\nTài khoản hoặc mật khẩu không chính xác, vui lòng thử lại", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                userdata.userid = String.Empty;
                userdata.passcode = String.Empty;
                userdata.employeeID = String.Empty;
                txt_password.ResetText();
                dt.Dispose();
                return;
            }
            if (err.errors == String.Empty && dt.Rows.Count == 1)
            {
                MessageBox.Show("Đăng nhập thành công", "Info", MessageBoxButtons.OK, MessageBoxIcon.Information);
                userdata.employeeID = (dt.Rows[0][0].ToString().Trim());
                userdata.userid = txt_username.Text;
                userdata.passcode = txt_password.Text;
                userdata.StrName = dt.Rows[0][1].ToString().Trim();
                userdata.employeeRnk = dt.Rows[0][2].ToString().Trim();
                extracheeses(dt.Rows[0][0].ToString().Trim());
                txt_username.ResetText();
                txt_password.ResetText();
                dt.Dispose();
                return;
            }
        }

        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            haltAllProcess = true;
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
                item.Tag = employeeDT.Columns[0].ColumnName;
                for (int i = 1; i < employeeDT.Columns.Count; i++)
                {
                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                    if (dr[i] == DBNull.Value)
                        sub.Text = "NULL";
                    if (dr[i] != DBNull.Value)
                    {
                        if (dr[i].ToString() == "True" || dr[i].ToString() == "False")
                            sub.Text = (dr[i].ToString() == "True" ? "Nữ" : "Nam");
                        else
                        {
                            sub.Text = dr[i].ToString();
                        }
                        sub.Tag = employeeDT.Columns[i].ColumnName;
                    }
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
            haltAllProcess = false;
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
            if(tempvalue == 0)
            {
                cb.Items.Add("Từ 0 đến 0");
            }
            if(tempvalue != 0)
            {
                if(tempvalue / perPage == 0)
                {
                    cb.Items.Add("Từ 1 đến " + totalRows.ToString());
                }
                if(tempvalue / perPage > 0)
                {
                    while(true)
                    {
                        if (tempvalue < perPage && tempvalue != 0)
                        {
                            cb.Items.Add((i * perPage + 1).ToString() + " TO " + totalRows.ToString());
                            break;
                        }
                        else
                        {
                            cb.Items.Add((i * perPage + 1).ToString() + " TO " + ((i + 1) * perPage).ToString());
                            i++;
                            tempvalue = tempvalue - perPage;
                        }
                    }
                }
            }
            cb.SelectedIndex = (cb.Items.Count == 1 ? 0 : 1);
        }

        private void LoadAll(string userID)
        {
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            Errors err = new Errors();
            listView1.Clear();
            listView1.GridLines = false;
            listView2.Clear();
            listView2.GridLines = false;
            haltAllProcess = true;
            #region Patient
            if(userID == String.Empty)
            {
                return;
            }
            if(userID.StartsWith("YT") || userID.StartsWith("BS") || userID.StartsWith("QT") || userID.StartsWith("TST"))
            { 
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
            }
            #endregion
            #region Employee
            if (userID.StartsWith("QT") || userID.StartsWith("TST"))
            {
                int temp2 = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.NhanVien", err);
                if (err.errors == String.Empty)
                {
                    CalculatePage(comboBox1, temp2, 250);
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

        private void LoadAll(string userID, int start, int amount)
        {
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            Errors err = new Errors();
            #region Patient
            int temp = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.BenhNhan", err);
            progressBar1.Value = 0;
            listView1.Clear();
            listView1.GridLines = false;
            listView2.Clear();
            listView2.GridLines = false;
            haltAllProcess = true;
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            if (userID == String.Empty)
            {
                return;
            }
            if (userID.StartsWith("YT") || userID.StartsWith("BS") || userID.StartsWith("QT") || userID.StartsWith("TST"))
            {
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
            }
            #endregion
            #region Employee
            if (userID.StartsWith("QT") || userID.StartsWith("TST"))
            {
                int temp2 = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.NhanVien", err);
                if (err.errors == String.Empty)
                {
                    CalculatePage(comboBox1, temp2, 250);
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
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            Errors err = new Errors();
            progressBar1.Value = 0;
            #region Patient
            int temp = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.BenhNhan", err);
            listView2.Clear();
            listView2.GridLines = false;
            listView2.Enabled = false;
            haltAllProcess = true;
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
            label9.Text = "Hệ thống đang đồng bộ dữ liệu với máy chủ... xin đợi.";
            Errors err = new Errors();
            progressBar1.Value = 0;
            #region Patient
            int temp = Function.maxRecords(Connection.sqlcon, "QuanLyPhongKham.dbo.NhanVien", err);
            listView1.Clear();
            listView1.Enabled = false;
            listView1.GridLines = false;
            haltAllProcess = true;
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
            listView2.Enabled = false;
            haltAllProcess = true;
            if (bW1Stopped)
            {
                bW1Stopped = false;
                if (comboBox1.Items.Count == 0)
                {
                    LoadAll(userdata.employeeID);
                }
                else
                {
                    string[] spliter = { " đến " };
                    string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                    string[] temp = (temp2).Split(spliter, StringSplitOptions.None);
                    LoadEmployee(Convert.ToInt32(temp[0]) - 1, Convert.ToInt32(temp[1]) - Convert.ToInt32(temp[0]));
                }
                recentUpdateEmployee = true;
            }
            else
            {
                MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void listView2_RetrieveVirtualItem(object sender, RetrieveVirtualItemEventArgs e)
        {
            //e.Item = ;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            listView2.Enabled = false;
            haltAllProcess = true;
            if (bW2Stopped)
            {
                bW2Stopped = false;
                if (comboBox2.Items.Count == 0)
                {
                    LoadAll(userdata.employeeID);
                }
                else
                {
                    string[] spliter = { " đến " };
                    string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                    string[] temp = (temp2).Split(spliter, StringSplitOptions.None);
                    LoadPatient(Convert.ToInt32(temp[0]) - 1, Convert.ToInt32(temp[1]) - Convert.ToInt32(temp[0]));
                }
                recentUpdatePatient = true;
            }
            else
            {
                MessageBox.Show("Tiến trình này hiện đang bận. Vui lòng thử lại sau", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
        }

        private void backgroundWorker2_DoWork(object sender, DoWorkEventArgs e)
        {
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
                item.Tag = patientDT.Columns[0].ColumnName;
                for (int i = 1; i < patientDT.Columns.Count; i++)
                {

                    ListViewItem.ListViewSubItem sub = new ListViewItem.ListViewSubItem();
                    if (dr[i] == DBNull.Value)
                        sub.Text = "NULL";
                    if (dr[i] != DBNull.Value)
                    {
                        if (dr[i].ToString() == "True" || dr[i].ToString() == "False")
                            sub.Text = (dr[i].ToString() == "True" ? "Nữ" : "Nam");
                        else
                        {
                            sub.Text = dr[i].ToString();
                        }
                        sub.Tag = patientDT.Columns[i].ColumnName;
                    }
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
            haltAllProcess = false;
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
            if(haltAllProcess)
            {
                return;
            }
            if (recentUpdateEmployee && recentUpdatePatient)
            {
                recentUpdateEmployee = false;
                recentUpdatePatient = false;
                timesLeftbeforeUpdate = 30;
                label9.Text = "Hệ thống sẽ đồng bộ dữ liệu với máy chủ sau " + timesLeftbeforeUpdate + " giây nữa.";
                return;
            }
            else if (recentUpdatePatient)
            {
                recentUpdateEmployee = false;
                timesLeftbeforeUpdate = 30;
                label9.Text = "Hệ thống sẽ đồng bộ dữ liệu với máy chủ sau " + timesLeftbeforeUpdate + " giây nữa.";
                return;
            }
            else if(recentUpdateEmployee)
            {
                recentUpdateEmployee = false;
                timesLeftbeforeUpdate = 30;
                label9.Text = "Hệ thống sẽ đồng bộ dữ liệu với máy chủ sau " + timesLeftbeforeUpdate + " giây nữa.";
                return;
            }
            timesLeftbeforeUpdate--;
            label9.Text = "Hệ thống sẽ đồng bộ dữ liệu với máy chủ sau " + timesLeftbeforeUpdate + " giây nữa.";
            if (timesLeftbeforeUpdate == 0)
            {
                if ((userdata.employeeID.StartsWith("BS") || userdata.employeeID.StartsWith("YT")))
                {
                    if (patientDT.Columns.Count >= 1 && patientDT.Rows.Count >= 1)
                    {
                        patientDT.Reset();
                    }
                    listView2.Clear();
                    if (comboBox2.Items.Count == 0)
                    {
                        LoadAll(userdata.employeeID);
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
                if ((userdata.employeeID.StartsWith("QT") || userdata.employeeID.StartsWith("TST")))
                {
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
                    if (comboBox1.Items.Count == 0)
                    {
                        LoadAll(userdata.employeeID);
                    }
                    else
                    {
                        string[] spliter = { " đến " };
                        string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                        string[] temp = (temp2).Split(spliter, StringSplitOptions.None);
                        LoadEmployee(Convert.ToInt32(temp[0]) - 1, Convert.ToInt32(temp[1]) - Convert.ToInt32(temp[0]));
                        LoadPatient(Convert.ToInt32(temp[0]) - 1, Convert.ToInt32(temp[1]) - Convert.ToInt32(temp[0]));
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
            this.Dispose();
        }

        private void pictureBox1_MouseHover(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._1458754244_task_manager_help;
        }

        private void pictureBox1_MouseLeave(object sender, EventArgs e)
        {
            pictureBox1.Image = Properties.Resources._1458753181_task_manager;
        }

        private void listView1_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView1.SelectedItems.Count >= 1)
                {
                    Point temp = new Point();
                    temp.X = this.Location.X + 18 + 25;
                    temp.Y = this.Location.Y + 163 + listView1.SelectedItems[0].Position.X + TextRenderer.MeasureText(listView1.SelectedItems[0].Text, listView1.Font).Height;
                    haltAllProcess = true;
                    DialogResult tempDR = ActionForm.Form1.ShowMenu(listView1.SelectedItems, 1, userdata, temp);
                    string[] spliter = { " đến " };
                    string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                    string[] temp3 = (temp2).Split(spliter, StringSplitOptions.None);
                    LoadEmployee(Convert.ToInt32(temp3[0]) - 1, Convert.ToInt32(temp3[1]) - Convert.ToInt32(temp3[0]));
                    return;
                }
                else
                {
                    Point temp = new Point();
                    temp.X = this.Location.X + 18 + 25;
                    temp.Y = this.Location.Y + 165;
                    haltAllProcess = true;
                    DialogResult tempDR = ActionForm.Form1.ShowMenu(temp, 1);
                    string[] spliter = { " đến " };
                    string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                    string[] temp3 = (temp2).Split(spliter, StringSplitOptions.None);
                    LoadEmployee(Convert.ToInt32(temp3[0]) - 1, Convert.ToInt32(temp3[1]) - Convert.ToInt32(temp3[0]));
                    return;
                }
            }
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void listView2_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                if (listView2.SelectedItems.Count >= 1)
                {
                    Point temp = new Point();
                    temp.X = this.Location.X + 18 + 25;
                    temp.Y = this.Location.Y + 163 + listView2.SelectedItems[0].Position.X + TextRenderer.MeasureText(listView2.SelectedItems[0].Text, listView2.Font).Height;
                    haltAllProcess = true;
                    DialogResult tempDR = ActionForm.Form1.ShowMenu(listView2.SelectedItems, 2, userdata, temp);
                    haltAllProcess = false;
                    string[] spliter = { " đến " };
                    string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                    string[] temp3 = (temp2).Split(spliter, StringSplitOptions.None);
                    LoadPatient(Convert.ToInt32(temp3[0]) - 1, Convert.ToInt32(temp3[1]) - Convert.ToInt32(temp3[0]));
                    return;
                }
                else
                {
                    Point temp = new Point();
                    temp.X = this.Location.X + 18 + 25;
                    temp.Y = this.Location.Y + 165;
                    haltAllProcess = true;
                    DialogResult tempDR = ActionForm.Form1.ShowMenu(temp, 2);
                    haltAllProcess = false;
                    string[] spliter = { " đến " };
                    string temp2 = comboBox2.SelectedItem.ToString().Substring(comboBox2.SelectedItem.ToString().LastIndexOf("Từ ") + 3);
                    string[] temp3 = (temp2).Split(spliter, StringSplitOptions.None);
                    LoadPatient(Convert.ToInt32(temp3[0]) - 1, Convert.ToInt32(temp3[1]) - Convert.ToInt32(temp3[0]));
                    return;
                }
            }
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            About.Form1 about = new About.Form1();
            about.ShowDialog();
        }
    }
}
