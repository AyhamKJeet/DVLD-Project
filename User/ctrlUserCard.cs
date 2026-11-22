using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DVLD_BLL;
namespace DVLD.User
{
    public partial class ctrlUserCard : UserControl
    {
        private clsUser _User;
        private int _UserID;
       
        public clsUser SelectedUserInfo
        {
            get { return _User; }
        }
        public int UserID
        {
            get { return _UserID; }
        }

      

        public ctrlUserCard()
        {
            InitializeComponent();
        
        }
        public void LoadUserInfo(int UserID)
        {
            _UserID = UserID;
            _User = clsUser.FindByUserID(UserID);
            if (_User == null)
            {
                MessageBox.Show("No User with UserID = " + UserID.ToString(), "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
           _FillUserInfo();
        }

        private void _FillUserInfo()
        {
         ctrlPersonCard1.LoadPersonInfo(_User.PersonID);
            lblUserID.Text = _User.UserID.ToString();
            lblUserName.Text = _User.UserName;
            if(_User.IsActive)
            {
                lblIsActive.Text = "Yes";
            }
            else
            {
                lblIsActive.Text = "No";
            }
        }
        public void ResetUserInfo()
        {
            ctrlPersonCard1.ResetPersonInfo();
            lblUserID.Text = "[????]";
            lblUserName.Text = "[????]";
            lblIsActive.Text = "[????]";
        }
        private void ctrlUserCard_Load(object sender, EventArgs e)
        {

        }

        private void ctrlPersonCard1_Load(object sender, EventArgs e)
        {

        }
    }
}
