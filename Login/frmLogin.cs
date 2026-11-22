using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD.Classes;
using DVLD_BLL;
namespace DVLD.Login
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void label4_Click(object sender, EventArgs e)
        {

        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void label7_Click(object sender, EventArgs e)
        {

        }

        private void chkRememberMe_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click(object sender, EventArgs e)
        {

        }

        private void label5_Click(object sender, EventArgs e)
        {

        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void txtPassword_TextChanged(object sender, EventArgs e)
        {

        }

        private void pictureBox8_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox3_Click(object sender, EventArgs e)
        {

        }

        private void frmLogin_Load(object sender, EventArgs e)
        {
           string Username = "", Password = "";
      if(clsGlobal.GetStoredCredential(ref Username, ref Password))
            {
                txtUserName.Text = Username;
                txtPassword.Text = Password;
                chkRememberMe.Checked = true;
            }
            else
            {
                chkRememberMe.Checked = false;
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void btnLogin_Click_1(object sender, EventArgs e)
        {
            clsUser User = clsUser.FindByUsernameAndPassword(txtUserName.Text, txtPassword.Text);
            if (User != null) {

                if (chkRememberMe.Checked)
                {  
                    clsGlobal.RememberUsernameAndPassword(txtUserName.Text, txtPassword.Text);
                }
                else
                {
                    clsGlobal.RememberUsernameAndPassword("", "");
                }

                if (!User.IsActive)
                {
                    MessageBox.Show("Your account deactivated, Please contact your admin", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return;
                }
               
             
                    clsGlobal.CurrentUser = User;
                    frmMain frm = new frmMain(this);
                    frm.Show();
                    this.Hide();
            }
            else
            {
                MessageBox.Show("Invalid username/password.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void splitContainer1_Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
