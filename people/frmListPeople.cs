using DVLD.people;
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
    public partial class Form1 : Form
    {

        private static DataTable _dtAllPeople = clsPerson.GetAllPeople();
        private DataTable _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

        private clsPerson _Person = new clsPerson();
        public Form1()
        {
            InitializeComponent();
        }

        private void _RefreshPeoplList()
        {
            _dtAllPeople = clsPerson.GetAllPeople();
            _dtPeople = _dtAllPeople.DefaultView.ToTable(false, "PersonID", "NationalNo",
                                                       "FirstName", "SecondName", "ThirdName", "LastName",
                                                       "GendorCaption", "DateOfBirth", "CountryName",
                                                       "Phone", "Email");

            dvgAllPeoples.DataSource = _dtPeople;
            lblRecordsCount.Text = dvgAllPeoples.Rows.Count.ToString();
        }
      
        private void Form1_Load(object sender, EventArgs e)
        {
            dvgAllPeoples.DataSource = _dtPeople;
            cbFilterBy.SelectedIndex = 0;
            lblRecordsCount.Text = dvgAllPeoples.Rows.Count.ToString();
            if (dvgAllPeoples.Rows.Count > 0)
            {

                dvgAllPeoples.Columns[0].HeaderText = "Person ID";
                dvgAllPeoples.Columns[0].Width = 110;

                dvgAllPeoples.Columns[1].HeaderText = "National No.";
                dvgAllPeoples.Columns[1].Width = 120;


                dvgAllPeoples.Columns[2].HeaderText = "First Name";
                dvgAllPeoples.Columns[2].Width = 120;

                dvgAllPeoples.Columns[3].HeaderText = "Second Name";
                dvgAllPeoples.Columns[3].Width = 140;


                dvgAllPeoples.Columns[4].HeaderText = "Third Name";
                dvgAllPeoples.Columns[4].Width = 120;

                dvgAllPeoples.Columns[5].HeaderText = "Last Name";
                dvgAllPeoples.Columns[5].Width = 120;

                dvgAllPeoples.Columns[6].HeaderText = "Gendor";
                dvgAllPeoples.Columns[6].Width = 120;

                dvgAllPeoples.Columns[7].HeaderText = "Date Of Birth";
                dvgAllPeoples.Columns[7].Width = 140;

                dvgAllPeoples.Columns[8].HeaderText = "Nationality";
                dvgAllPeoples.Columns[8].Width = 120;


                dvgAllPeoples.Columns[9].HeaderText = "Phone";
                dvgAllPeoples.Columns[9].Width = 120;


                dvgAllPeoples.Columns[10].HeaderText = "Email";
                dvgAllPeoples.Columns[10].Width = 170;
            }
        }

        private void btnAddNewPerson_Click(object sender, EventArgs e)
        {
            
            frmAddNewPerson frmAddNewPerson = new frmAddNewPerson();
            frmAddNewPerson.ShowDialog();
        }

        private void deleteToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            _Person.PersonID = Convert.ToInt32(dvgAllPeoples.CurrentRow.Cells[0].Value);
            if (clsPerson.DeletePerson(_Person.PersonID))
            {
                MessageBox.Show("Delete Person Successfuly");
                _RefreshPeoplList();
            }
            else
            {
                MessageBox.Show("Delete Person Not Successfuly");
            }
        }

        private void dvgAllPeoples_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Person.PersonID = Convert.ToInt32(dvgAllPeoples.CurrentRow.Cells[0].Value);
         
            
            frmAddNewPerson frmAddNewPerson = new frmAddNewPerson(_Person.PersonID);
            frmAddNewPerson.ShowDialog();
            _RefreshPeoplList();
        }

        private void showPersonInfoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            _Person.PersonID = Convert.ToInt32(dvgAllPeoples.CurrentRow.Cells[0].Value);
            frmShowPersonInfo frm = new frmShowPersonInfo(_Person.PersonID);
            frm.ShowDialog();

        }

        private void addNewPersonToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmAddNewPerson frm = new frmAddNewPerson();
            frm.ShowDialog();
            _RefreshPeoplList();
        }

        private void cbFilterBy_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtFilterValue.Visible = (cbFilterBy.Text != "None");
            if (txtFilterValue.Visible)
            {
                txtFilterValue.Text = "";
                txtFilterValue.Focus();
               }
        }

        private void txtFilterValue_TextChanged(object sender, EventArgs e)
        {
            string FilterColumn = "";
            switch (cbFilterBy.Text)
            {
                case "Person ID":
                    FilterColumn = "PersonID";
                    break;

                case "National No.":
                    FilterColumn = "NationalNo";
                    break;

                case "First Name":
                    FilterColumn = "FirstName";
                    break;

                case "Second Name":
                    FilterColumn = "SecondName";
                    break;

                case "Third Name":
                    FilterColumn = "ThirdName";
                    break;

                case "Last Name":
                    FilterColumn = "LastName";
                    break;

                case "Nationality":
                    FilterColumn = "CountryName";
                    break;

                case "Gendor":
                    FilterColumn = "GendorCaption";
                    break;

                case "Phone":
                    FilterColumn = "Phone";
                    break;

                case "Email":
                    FilterColumn = "Email";
                    break;

                default:
                    FilterColumn = "None";
                    break;
            }

            if (txtFilterValue.Text.Trim() == "" || FilterColumn == "None")
            {
                _dtPeople.DefaultView.RowFilter = "";
                lblRecordsCount.Text = dvgAllPeoples.Rows.Count.ToString();
                return;
            }

            if (FilterColumn == "PersonID")
                //in this case we deal with integer not string.

                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] = {1}", FilterColumn, txtFilterValue.Text.Trim());
            else
                _dtPeople.DefaultView.RowFilter = string.Format("[{0}] LIKE '{1}%'", FilterColumn, txtFilterValue.Text.Trim());

            lblRecordsCount.Text = dvgAllPeoples.Rows.Count.ToString();
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void sendEmailToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void phoneCallToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("This Feature Is Not Implemented Yet!", "Not Ready!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void txtFilterValue_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (cbFilterBy.Text == "Person ID")
                e.Handled = !char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar);
        }
    }
}
