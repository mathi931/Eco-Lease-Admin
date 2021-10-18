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
using EcoLease_Admin.Validators;
using FluentValidation.Results;
using static EcoLease_Admin.UserControls.Methods.MessageBoxes;
using EcoLease_Admin.UserControls.Methods;

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

        //declare the processor
        CustomerProcessor proc = new CustomerProcessor();

        public Customers()
        {
            InitializeComponent();
        }

        //fills the datatable with the customer list
        DataTable filledDataTable(DataTable dt , List<Customer> customers)
        {
            foreach (var c in customers)
            {
                dt.Rows.Add(c.CId, c.FirstName, c.LastName, c.DateOfBirth, c.Email, c.PhoneNo);
            }
            return dt;
        }

        //creates the datatable 
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

        //returns a customer object by selected row 
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

        //on confirm button click, can be : add new, update, remove customer
        private async void btnPost_Click(object sender, EventArgs e)
        {
            //instance of validator
            CustomerValidator validator = new CustomerValidator();

            //creates an object from input
            var customer = new Customer(txbFirstName.Text, txbLastName.Text, dtpDateOfBirth.Value, txbEmail.Text, txbPhoneNo.Text);

            //validation result based on input
            ValidationResult result = validator.Validate(customer);

            //on invalid input loops through the invalid inputs and notify the user
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    ErrorMessage("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage, "Validation Error");
                }
                return;
            }

            //if there is no targeted row from the table and the btn text is add new and the input is valid
            //
            if (btnPost.Text == "Add New" && dataGridView.SelectedRows.Count == 0 && result.IsValid)
            {
                //sends the post request with the new customer
                await proc.InsertCustomer(customer);

                //after a custome message refreshes the view
                if (InfoMessage($"A new customer {txbFirstName.Text} just added!") == DialogResult.OK)
                {
                    RefreshList();
                }
            }

            //if there is a targeted row from the table and the btn text is update and the input is valid
            //
            else if (btnPost.Text == "Update" && dataGridView.SelectedRows.Count > 0 && result.IsValid)
            {
                //sends the put request with the updated customer
                await proc.UpdateCustomer(customer);

                //after a custome message refreshes the view
                if (InfoMessage($"A customer ID: {lbID.Text} just updated!") == DialogResult.OK) 
                {
                    RefreshList();
                }
            }
        }
        
        //on cancel button click removes the input
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
                dtpDateOfBirth.Value = DateTime.Now.AddYears(-19);
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

        //remove button click
        private async void btnRemove_Click(object sender, EventArgs e)
        {
            //if there is a selected row and the dialog result is OK
            if (btnRemove.Visible && selected != null && DynamicQuestion("remove the", $" {selected.FirstName} with ID: {selected.CId} ?", "Removing Customer") == DialogResult.OK)
            {
                //removes the customer with delete request and refreshes the view
                await proc.RemoveCustomer(selected.CId);

                //after a custome message refreshes the view
                if (InfoMessage($"A customer ID: {selected.CId} just removed!") == DialogResult.OK)
                {
                    RefreshList();
                }
            }
        }

        private async void RefreshList()
        {
            //refresh the list
            selected = null;
            dataGridView.ClearSelection();

            dt = filledDataTable(DataTbl(), await proc.LoadCustomers());
            dataGridView.DataSource = dt;
        }

        private async void Customers_Load(object sender, EventArgs e)
        {
            //set values in DatePicker
            new SetInput().SetDatepicker(dtpDateOfBirth, DateTime.Now.AddYears(-19), -120, -18);

            //getting the data and save it locally into a list
            customers = await proc.LoadCustomers();

            //create and fill dataTable because of ADGV
            RefreshList();
        }
    }
}
