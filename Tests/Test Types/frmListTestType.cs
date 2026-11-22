using DVLD_BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DVLD.Tests.Test_Types
{
    public partial class frmListTestType : Form
    {
       private DataTable _dtAllTestType;

        public frmListTestType()
        {
            InitializeComponent();
            
        }

        private void frmListTestType_Load(object sender, EventArgs e)
        {
            _dtAllTestType = clsTestType.GetAllTestType();
            dgvTestTypes.DataSource = _dtAllTestType;
            lblRecordsCount.Text = dgvTestTypes.RowCount.ToString();

            if (dgvTestTypes.Rows.Count > 0)
            {
                dgvTestTypes.Columns[0].HeaderText = "ID";
                dgvTestTypes.Columns[0].Width = 110;

                dgvTestTypes.Columns[1].HeaderText = "Title";
                dgvTestTypes.Columns[1].Width = 200;

                dgvTestTypes.Columns[2].HeaderText = "Description";
                dgvTestTypes.Columns[2].Width = 350;

                dgvTestTypes.Columns[3].HeaderText = "Fees";
                dgvTestTypes.Columns[3].Width = 110;

            }
        }

        private void editToolStripMenuItem_Click(object sender, EventArgs e)
        {
            frmEditTestType frmEdit = new frmEditTestType((clsTestType.enTestType)(dgvTestTypes.CurrentRow.Cells[0].Value));
            frmEdit.ShowDialog();
            frmListTestType_Load(null,null);
        }
    }
}
