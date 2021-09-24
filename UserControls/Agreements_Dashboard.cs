using EcoLease_Admin.Data;
using EcoLease_Admin.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLease_Admin.UserControls
{
    public partial class Agreements_Dashboard : UserControl
    {
        //create a dataTable for insert data to ADGV
        DataTable dt = new DataTable();
        List<Agreement> list = new List<Agreement>();

        //declared delegate to easily pass data through objects
        //I can see in other objects which is the selected object on the datagridview
        public delegate void SelectedAgreementChangedEventHandler(object source, Agreement selected);
        public event SelectedAgreementChangedEventHandler SelectedAgreementChanged;

        public Agreements_Dashboard()
        {
            InitializeComponent();

            //getting the data and save it locally into a list
            list = new AgreementDataAccess().GetAll();
            //create and fill dataTable because of ADGV
            dt = fillDataTable(AgreementDT(), list);
            dgvAgreements.DataSource = dt;
        }

        //on selected index change
        private void dgvAgreements_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvAgreements.SelectedRows.Count > 0)
            {
                OnSelectedAgreementChanged(selected(dgvAgreements.SelectedRows[0]));
            }
            else
            {
                OnSelectedAgreementChanged(new Agreement());
            }
        }
        protected virtual void OnSelectedAgreementChanged(Agreement selected)
        {
            if (SelectedAgreementChanged != null)
            {
                SelectedAgreementChanged(this, selected);
            }
        }

        //convert row to object
        Agreement selected(DataGridViewRow row)
        {
            return list.Find(e => e.AId == (int)row.Cells[0].Value);
        }

        DataTable AgreementDT()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Lease From", typeof(DateTime));
            dt.Columns.Add("Lease Until", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("User", typeof(User));
            dt.Columns.Add("Vehicle", typeof(Vehicle));

            return dt;
        }
        DataTable fillDataTable(DataTable dt, List<Agreement> agreements)
        {
            foreach (var agr in agreements)
            {
                dt.Rows.Add(agr.AId, agr.LeaseBegin, agr.LeaseLast, agr.Status, agr.User, agr.Vehicle);
            }
            return dt;
        }

        //filtering and sorting config
        private void dgvAgreements_FilterStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = dgvAgreements.FilterString;
        }

        private void dgvAgreements_SortStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = dgvAgreements.SortString;
        }
    }
}
