using DVLD_Buisness;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
namespace DVLD
{
    public partial class UserCardControl : UserControl
    {

        public event Action<int> OnSaveCompleted;

        private clsPerson _Person;
        //private int _PersonID = -1;
        public UserCardControl()
        {
            InitializeComponent();
        }

        private void txtLastName_TextChanged(object sender, EventArgs e)
        {

        }

        private void UserCardControl_Load(object sender, EventArgs e)
        {
            
            _Person = new clsPerson();

            if (radMale.Checked)
            {
                PicProfile.Image = Properties.Resources.icons8_who_skin_type_1_48;
            }
            else
            {
                PicProfile.Image = Properties.Resources.guess_who_unknown_person_silhouette_icon_vector_anonymous_mysterious_user_profile_705714_2535;
            }

            cmbCountry.DataSource = clsCountry.GetAllCountries();
            cmbCountry.DisplayMember = "CountryName";
            cmbCountry.ValueMember = "CountryID";

        }

        private void radMale_CheckedChanged(object sender, EventArgs e)
        {
            PicProfile.Image = Properties.Resources.icons8_who_skin_type_1_48;
        }

        private void radFemale_CheckedChanged(object sender, EventArgs e)
        {
            PicProfile.Image = Properties.Resources.guess_who_unknown_person_silhouette_icon_vector_anonymous_mysterious_user_profile_705714_2535;
        }

        private string _currentImagePath = "";
        private string _SaveImageToMyFolder(string imagePath)    
        {
            

        string myFolder = @"C:\Users\User\Desktop\Programming Advice\Course #19\DVLD-Image";

            if (!string.IsNullOrEmpty(_currentImagePath) && System.IO.File.Exists(_currentImagePath))
            {
                try
                {
                    System.IO.File.Delete(_currentImagePath);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("خطأ أثناء حذف الصورة القديمة: " + ex.Message);
                }
            }

            string extension = System.IO.Path.GetExtension(imagePath);
            string newFilePath = System.IO.Path.Combine(myFolder, Guid.NewGuid().ToString() + extension);
            System.IO.File.Copy(imagePath, newFilePath, true);
            _currentImagePath = newFilePath;

            return newFilePath;
        }
        private void lblSetImage_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            
            openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.bmp;*.gif";
          

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                string newImagePath = _SaveImageToMyFolder(openFileDialog1.FileName);
                PicProfile.ImageLocation = newImagePath;
                lblRemove.Visible = true;
            }
        }

        private void PicProfile_Click(object sender, EventArgs e)
        {

        }

        private void txtFirstName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtFirstName.Text))
            {
              
                errorProvider1.SetError(txtFirstName, "First Name is required.");
            }
            else
            {
               
                errorProvider1.SetError(txtFirstName, null);
            }
        }

        private void txtSecondName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSecondName.Text))
            {
                
                errorProvider1.SetError(txtSecondName, "Second Name is required.");
            }
            else
            {
               
                errorProvider1.SetError(txtSecondName, null);
            }
    }

        private void txtThirdName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtThirdName.Text))
            {
               
                errorProvider1.SetError(txtThirdName, "Third Name is required.");
            }
            else
            {
               
                errorProvider1.SetError(txtThirdName, null);
            }
        }

        private void txtLastName_Validating(object sender, CancelEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtLastName.Text))
            {
              
                errorProvider1.SetError(txtLastName, "Last Name is required.");
            }
            else
            {
                
                errorProvider1.SetError(txtLastName, null);
            }
        }

        private void txtNationalNo_Validated(object sender, EventArgs e)
        {
      
        }

        private void txtPhone_TextChanged(object sender, EventArgs e)
        {

            if (!string.IsNullOrEmpty(txtPhone.Text) && !System.Text.RegularExpressions.Regex.IsMatch(txtPhone.Text, @"^\+?\d+$"))
            {
                errorProvider1.SetError(txtPhone, "Phone number must contain only digits.");
            }
            else
            {
                errorProvider1.SetError(txtPhone, null);
            }
        }

        private void txtEmail_TextChanged(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(txtEmail.Text) && !System.Text.RegularExpressions.Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$"))
            {
                errorProvider1.SetError(txtEmail, "Invalid email format.");
            }
            else
            {
                errorProvider1.SetError(txtEmail, null);
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            
        }

        private void lblRemove_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            PicProfile.Image = null;
        }

        private void txtNationalNo_TextChanged(object sender, EventArgs e)
        {

        }

        private void txtNationalNo_Validating(object sender, CancelEventArgs e)
        {
            string nationalNo = txtNationalNo.Text.Trim();
            if (string.IsNullOrWhiteSpace(nationalNo))
            {

                errorProvider1.SetError(txtNationalNo, "National Number is required.");
                return;
            }
            if (clsPerson.isPersonExist(nationalNo))
            {
                
                errorProvider1.SetError(txtNationalNo, "This National Number already exists.");
            }
            else
            {
                
                errorProvider1.SetError(txtNationalNo, null);
            }
        }

        private void SavePerson()
        {
            if (_Person.Mode == clsPerson.enMode.AddNew)
            {
                _Person.FirstName = txtFirstName.Text.Trim();
                _Person.SecondName = txtSecondName.Text.Trim();
                _Person.ThirdName = txtThirdName.Text.Trim();
                _Person.LastName = txtLastName.Text.Trim();
                _Person.Email = txtEmail.Text.Trim();
                _Person.Phone = txtPhone.Text.Trim();
                _Person.NationalNo = txtNationalNo.Text.Trim();
                _Person.Gendor = radFemale.Checked ? (short)1 : (short)0;
                _Person.DateOfBirth = dtpDateOfBirth.Value;
                _Person.NationalityCountryID = Convert.ToInt32(cmbCountry.SelectedValue);
                _Person.Address = BoxAddress.Text.Trim();
            }
            else
            {
                txtFirstName.Text = _Person.FirstName;
                txtSecondName.Text = _Person.SecondName;
                txtThirdName.Text = _Person.ThirdName;
                txtLastName.Text = _Person.LastName;
                txtEmail.Text = _Person.Email;
                txtPhone.Text = _Person.Phone;
                txtNationalNo.Text = _Person.NationalNo;
                if(_Person.Gendor == 0 )
                    radMale.Checked = true;
                else 
                    radMale.Checked = false;
                dtpDateOfBirth.Value = _Person.DateOfBirth;
                cmbCountry.SelectedIndex = _Person.NationalityCountryID;
                BoxAddress.Text = _Person.Address;
            }
            if (_Person.Save())
            {


                MessageBox.Show("Data Saved Successfully.", "Saved", MessageBoxButtons.OK, MessageBoxIcon.Information);


                OnSaveCompleted?.Invoke(_Person.PersonID);


            }
            else
            {
                MessageBox.Show("Error: Data Is not Saved Successfully.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnSave_Click(object sender, EventArgs e)
        {

            SavePerson();

        }

        private void richTextBox1_TextChanged(object sender, EventArgs e)
        {

        }
    }
}