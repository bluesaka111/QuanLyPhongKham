using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PatientInfo
{
    public partial class PForm : Form
    {
        private Point oldP;
        private ListViewItem item;
        public PForm()
        {
            InitializeComponent();
        }

        public PForm(ListViewItem item)
        {
            InitializeComponent();
            this.item = item;
            label1.Text = "Thông tin bệnh nhân [" + item.SubItems[0].Text + "] " + item.SubItems[1].Text + " " + item.SubItems[2].Text;
            ResetField(item);
        }

        private void ResetField(ListViewItem item)
        {
            txt_maBN.Text = (item.SubItems[0].Text == "NULL" ? String.Empty : item.SubItems[0].Text);
            txt_HoBN.Text = (item.SubItems[1].Text == "NULL" ? String.Empty : item.SubItems[1].Text);
            txt_TenBN.Text = (item.SubItems[2].Text == "NULL" ? String.Empty : item.SubItems[2].Text);
            cb_GTBN.SelectedIndex = (item.SubItems[3].Text == "true" ? 1 : 0);
            txt_DiaChi.Text = (item.SubItems[4].Text == "NULL" ? String.Empty : item.SubItems[4].Text);
            txt_DThoai.Text = (item.SubItems[5].Text == "NULL" ? String.Empty : item.SubItems[5].Text);
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

        private void expandablePanel4_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            DevComponents.DotNetBar.ExpandablePanel expanel = (DevComponents.DotNetBar.ExpandablePanel)sender;
            if (expanel.Expanded)
            {
                expanel.Height = expanel.MaximumSize.Height;

            }
        }

        private void expandablePanel5_ExpandedChanging(object sender, DevComponents.DotNetBar.ExpandedChangeEventArgs e)
        {
            panel10.AutoScroll = (expandablePanel5.Expanded ? false : true);
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
            if (txt_maBN.Text == "NULL" || txt_maBN.Text == "" || txt_maBN.Text == String.Empty)
            {
                txt_maBN.BackColor = Color.Red;
                txt_maBN.ForeColor = Color.Yellow;
            }
            else
            {
                txt_maBN.BackColor = Color.FromName("Window");
                txt_maBN.ForeColor = Color.Black;
            }
            if (txt_HoBN.Text == "NULL" || txt_HoBN.Text == "" || txt_HoBN.Text == String.Empty)
            {
                txt_HoBN.BackColor = Color.Red;
                txt_HoBN.ForeColor = Color.Yellow;
            }
            else
            {
                txt_HoBN.BackColor = Color.FromName("Window");
                txt_HoBN.ForeColor = Color.Black;
            }
            if (txt_TenBN.Text == "NULL" || txt_TenBN.Text == "" || txt_TenBN.Text == String.Empty)
            {
                txt_TenBN.BackColor = Color.Red;
                txt_TenBN.ForeColor = Color.Yellow;
            }
            else
            {
                txt_TenBN.BackColor = Color.FromName("Window");
                txt_TenBN.ForeColor = Color.Black;
            }
            if (txt_DiaChi.Text == "NULL" || txt_DiaChi.Text == "" || txt_DiaChi.Text == String.Empty)
            {
                txt_DiaChi.BackColor = Color.Red;
                txt_DiaChi.ForeColor = Color.Yellow;
            }
            else
            {
                txt_DiaChi.BackColor = Color.FromName("Window");
                txt_DiaChi.ForeColor = Color.Black;
            }
        }

        private void PForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            timer1.Enabled = false;
        }
    }
}
