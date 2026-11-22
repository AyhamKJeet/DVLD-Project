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

namespace DVLD.Applications.ApplicationsType
{
    public partial class frmListApplicationTypes : Form
    {

        private DataTable _dtApplicationTypes;
        public frmListApplicationTypes()
        {
            InitializeComponent();
        }

        private void _RefreshApplicationTypes()
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationType();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;
            lblRecordsCount.Text = _dtApplicationTypes.Rows.Count.ToString();

        }
        private void frmListApplicationTypes_Load(object sender, EventArgs e)
        {
            _dtApplicationTypes = clsApplicationType.GetAllApplicationType();
            dgvApplicationTypes.DataSource = _dtApplicationTypes;
            lblRecordsCount.Text = _dtApplicationTypes.Rows.Count.ToString();

            if (_dtApplicationTypes.Rows.Count > 0)
            {
                dgvApplicationTypes.Columns[0].HeaderText = "ID";
                dgvApplicationTypes.Columns[0].Width = 110;

                dgvApplicationTypes.Columns[1].HeaderText = "Title";
                dgvApplicationTypes.Columns[1].Width = 350;

                dgvApplicationTypes.Columns[2].HeaderText = "Fees";
                dgvApplicationTypes.Columns[2].Width = 110;
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void editApplicationTypeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            
            int ApplicationTypeId = (int)dgvApplicationTypes.CurrentRow.Cells[0].Value;
            frmEditApplicationTypes frm = new frmEditApplicationTypes(ApplicationTypeId);
            frm.ShowDialog();
            frmListApplicationTypes_Load(null,null);
        }

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
