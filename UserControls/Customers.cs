using ADGV;
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
    public partial class Customers : UserControl
    {
        //create a dataTable for insert data to ADGV
        DataTable dt;

        //local list variable of customers
        List<Customer> customers = new List<Customer>();

        //selectedID depends on datagridview selection
        Customer selected = new Customer();
        public Customers()
        {
            InitializeComponent();

            //getting the data and save it locally into a list
            customers = new CustomerDataAccess().GetAll();

            //create and fill dataTable because of ADGV
            RefreshList();
        }

        DataTable filledDataTable(DataTable dt , List<Customer> customers)
        {
            foreach (var c in customers)
            {
                dt.Rows.Add(c.CId, c.FirstName, c.LastName, c.DateOfBirth, c.Email, c.PhoneNo);
            }
            return dt;
        }

        DataTable DataTbl()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("First Name", typeof(string));
            dt.Columns.Add("Last Name", typeof(string));
            dt.Columns.Add("Date Of Birth", typeof(DateTime));
            dt.Columns.Add("E-mail", typeof(string));
            dt.Columns.Add("Telephone", typeof(string));

            return dt;
        }

        Customer getSelected(DataGridViewRow row)
        {
            return customers.Find(c => c.CId == (int)row.Cells[0].Value);
        }

        private void dataGridView_SelectionChanged(object sender, EventArgs e)
        {
            if(dataGridView.SelectedRows.Count > 0)
            {
                selected = getSelected(dataGridView.SelectedRows[0]);
                btnPost.Text = "Update";
                btnRemove.Visible = true;
                fillInputs(true, selected);
            }
            else
            {
                btnPost.Text = "Add New";
                btnRemove.Visible = false;
                fillInputs();
            }
        }

        //filtering and sorting config
        private void dataGridView_SortStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = dataGridView.SortString;
        }

        private void dataGridView_FilterStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = dataGridView.FilterString;
        }


        private void btnPost_Click(object sender, EventArgs e)
        {
            if(btnPost.Text == "Add New" && dataGridView.SelectedRows.Count == 0)
            {
                new CustomerDataAccess().Insert(new Customer(txbFirstName.Text, txbLastName.Text, dtpDateOfBirth.Value, txbEmail.Text, txbPhoneNo.Text));

                MessageBox.Show($"A new customer {txbFirstName.Text} just added!", "Successful Action!, Returning to Dashboard");
                RefreshList();
            }
            else if(btnPost.Text == "Update" && dataGridView.SelectedRows.Count > 0)
            {
                new CustomerDataAccess().Update(new Customer(Convert.ToInt32(lbID.Text), txbFirstName.Text, txbLastName.Text, dtpDateOfBirth.Value, txbEmail.Text, txbPhoneNo.Text));

                MessageBox.Show($"A customer ID: {lbID.Text} just updated!", "Successful Action!, Returning to Dashboard");
                RefreshList();
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            dataGridView.ClearSelection();
        }

        private void fillInputs(bool update = false, Customer selected = null)
        {
            //new
            if (!update)
            {
                lbID.Text = "";
                txbFirstName.Text = "";
                txbLastName.Text = "";
                dtpDateOfBirth.Value = DateTime.Now;
                txbEmail.Text = "";
                txbPhoneNo.Text = "";
            }
            //update
            else
            {
                lbID.Text = selected.CId.ToString();
                txbFirstName.Text = selected.FirstName;
                txbLastName.Text = selected.LastName;
                dtpDateOfBirth.Value = selected.DateOfBirth;
                txbEmail.Text = selected.Email;
                txbPhoneNo.Text = selected.PhoneNo;
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (btnRemove.Visible && selected != null && MessageBox.Show($"Are you sure to remove the {selected.FirstName} with ID: {selected.CId} ?", "Removing Customer", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                new CustomerDataAccess().Remove(selected);
                MessageBox.Show($"{selected.FirstName} Successfully removed!");
                RefreshList();
            }
        }

        private void RefreshList()
        {
            //refresh the list
            selected = null;
            dataGridView.ClearSelection();

            dt = filledDataTable(DataTbl(), new CustomerDataAccess().GetAll());
            dataGridView.DataSource = dt;
        }
    }
}
