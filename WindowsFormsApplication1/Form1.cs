using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DTObject;

namespace ActionForm
{
    public partial class Form1 : Form
    {
        private static bool runFromApp = false;
        private ListViewItem litem;
        private static DialogResult diaR;
        private int listv;
        public Form1()
        {
            InitializeComponent();
        }

        public static bool commandRun
        {
            set { runFromApp = value; }
        }

        public static DialogResult ShowMenu(ListViewItem item, int listview, UserInfo employeedata, Point position)
        {
            if (!runFromApp)
            {
                return DialogResult.Cancel;
            }
            ActionForm.Form1 f = new Form1();
            f.litem = new ListViewItem();
            f.litem = item;
            diaR = DialogResult.None;
            if (listview == 1)
            {
                f.toolTip1.SetToolTip(f.button4, "Thêm hồ sơ nhân viên");
                f.toolTip1.SetToolTip(f.button3, "Cập nhật hồ sơ nhân viên");
                f.toolTip1.SetToolTip(f.button2, "Xóa hồ sơ nhân viên");
                f.listv = listview;
            }
            else
            {
                f.toolTip1.SetToolTip(f.button4, "Thêm hồ sơ bệnh nhân");
                f.toolTip1.SetToolTip(f.button3, "Cập nhật hồ sơ bệnh nhân");
                f.toolTip1.SetToolTip(f.button2, "Xóa hồ sơ bệnh nhân");
                f.listv = listview;
            }
            f.button4.Enabled = false;
            f.button4.BackColor = Color.FromName("Grey");
            f.Location = position;
            f.ShowDialog();
            return diaR;
        }

        public static DialogResult ShowMenu(Point position)
        {
            if (!runFromApp)
            {
                return DialogResult.Cancel;
            }
            ActionForm.Form1 f = new Form1();
            diaR = DialogResult.None;
            f.Location = position;
            f.button3.Enabled = false;
            f.button3.BackColor = Color.FromName("Grey"); ;
            f.button2.Enabled = false;
            f.button2.BackColor = Color.FromName("Grey"); ;
            f.ShowDialog();
            return diaR;
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            if(!runFromApp)
            {
                this.Close();
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.Dispose();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            GC.Collect();
            GC.WaitForPendingFinalizers();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(listv == 2)
            {
                PatientInfo.PForm patIfo = new PatientInfo.PForm(litem.SubItems[0].Text, true, 0);
                patIfo.ShowDialog();
                this.Close();
            }
            else
            {
                EmployeeInfo.EForm empIfo = new EmployeeInfo.EForm(litem.SubItems[0].Text, true, 0);
                empIfo.ShowDialog();
                this.Close();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (listv == 2)
            {
                PatientInfo.PForm patIfo = new PatientInfo.PForm(litem.SubItems[0].Text, false, 2);
                patIfo.ShowDialog();
                this.Close();
            }
            else
            {
                EmployeeInfo.EForm empIfo = new EmployeeInfo.EForm(litem.SubItems[0].Text, false, 2);
                empIfo.ShowDialog();
                this.Close();
            }
        }
    }
}
