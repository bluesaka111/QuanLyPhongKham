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

namespace LoginForm
{
    public partial class LoginForm : Form
    {
        private bool fLogin = true;
        private Form called = null;
        public LoginForm()
        {
            InitializeComponent();
            pictureBox2.ImageLocation = Application.StartupPath + @"/pictures/clinic_icon.gif";
            label4.Text = this.Text;
        }

        public LoginForm(bool firstLogin, Form f)
        {
            InitializeComponent();
            pictureBox2.ImageLocation = Application.StartupPath + @"/pictures/clinic_icon.gif";
            label4.Text = this.Text;
            fLogin = firstLogin;
            called = f;
        }

        #region MoveForm
        Point oldP = new Point();
        private void label4_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                oldP = e.Location;
            }
        }

        private void label4_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                Point newP = e.Location;
                int x = newP.X - oldP.X;
                int y = newP.Y - oldP.Y;
                this.Top += y;
                this.Left += x;
            }
        }

        private void label4_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                oldP = e.Location;
            }
        }
        #endregion

        public string employeeID = null;

        private void button_close_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button_login_Click(object sender, EventArgs e)
        {
            string errors = null;
            //string userid = Login.userLogin(txt_username.Text, txt_password.Text, errors);

            //if(userid == "E404")
            //{
            //    MessageBox.Show("Login Failed due to error: " + errors);
            //    return;
            //}
            //else
            //{
            //    employeeID = userid;
            //    return;
            //}
        }

    }
}
