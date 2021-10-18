using EcoLease_Admin.Data;
using EcoLease_Admin.Data.Classes;
using EcoLease_Admin.Models;
using EcoLease_Admin.UserControls.Methods;
using EcoLease_Admin.Validators;
using FluentValidation.Results;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static EcoLease_Admin.UserControls.Methods.MessageBoxes;

namespace EcoLease_Admin.UserControls
{
    public partial class Reservations_Edit : UserControl
    {
        Panel mainPnl;
        bool update;
        Reservation toEdit;

        //processors
        VehicleProcessor vehicleProc = new VehicleProcessor();
        CustomerProcessor customerProc = new CustomerProcessor();
        StatusProcessor statusProc = new StatusProcessor();
        ReservationProcessor resProc = new ReservationProcessor();

        public Reservations_Edit(Panel pnl, Reservation editable = null)
        {
            InitializeComponent();
            mainPnl = pnl;
            toEdit = editable;
        }

        private async Task fillControls(Reservation editable = null)
        {
            var inputFiller = new SetInput();

            //loads the comboboxes
            cmbStatus.DataSource = await statusProc.LoadStatuses();
            cmbCustomer.DataSource = await customerProc.LoadCustomers();
            cmbVehicles.DataSource = await vehicleProc.LoadVehicles();

            //sets the datpickers
            inputFiller.SetDatepicker(dtpLeaseBegin, DateTime.Now, -7, 1);
            inputFiller.SetDatepicker(dtpLeaseLast, DateTime.Now, -7, 7);

            //if there is a passed value then its an update => load the object to input values
            if (editable != null)
            {
                lbID.Text = editable.RId.ToString();
                dtpLeaseBegin.Value = editable.LeaseBegin;
                dtpLeaseLast.Value = editable.LeaseLast;
                cmbStatus.SelectedIndex = cmbStatus.FindStringExact(editable.Status);
                cmbCustomer.SelectedIndex = cmbCustomer.FindStringExact(editable.Customer.ToString());
                cmbVehicles.SelectedIndex = cmbVehicles.FindStringExact(editable.Vehicle.ToString());
                update = true;
            }
        }

        //gets an object from input
        private Reservation getReservationObject(bool update=false)
        {
            if (update)
            {
                return new Reservation(Convert.ToInt32(lbID.Text), dtpLeaseBegin.Value, dtpLeaseLast.Value, cmbStatus.SelectedItem.ToString(), cmbCustomer.SelectedItem as Customer, cmbVehicles.SelectedItem as Vehicle);
            }
            else
            {
            return new Reservation(dtpLeaseBegin.Value, dtpLeaseLast.Value, cmbStatus.SelectedItem.ToString(), cmbCustomer.SelectedItem as Customer, cmbVehicles.SelectedItem as Vehicle);
            }
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            //instance of validator
            ReservationValidator validator = new ReservationValidator();

            //creates an object from input
            var reservation = getReservationObject();

            //validation result based on input
            ValidationResult result = validator.Validate(reservation);

            //on invalid input loops through the invalid inputs and notify the user
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    ErrorMessage("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage, "Validation Error");
                }
                return;
            }

            //insert
            if (!update)
            {
                try
                {
                    //sends a post request
                    await resProc.InsertReservation(reservation);

                    if(InfoMessage($"A new reservation just added!") == DialogResult.OK)
                    {
                        //goes back to the dashboard
                        returnToDashboard();
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.Message, "Unsuccessfull Action!");
                }
            }

            //edit
            else
            {
                try
                {
                    //sends a put Request
                    await resProc.UpdateReservation(getReservationObject(true));

                    if (InfoMessage($"An reservation with ID: {lbID.Text} just updated!") == DialogResult.OK)
                    {
                        //goes back to the dashboard
                        returnToDashboard();
                    }
                }
                catch (Exception ex)
                {
                    ErrorMessage(ex.Message, "Unsuccessfull Action!");
                }
            }
        }

        //on cancel button click, returns to the dashboard
        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (DynamicQuestion("exit ?", " ", "Returning to Dashboard") == DialogResult.OK)
            {
                returnToDashboard();
            }
        }

        //changes view to dashboard
        private void returnToDashboard()
        {
            mainPnl.Controls.Clear();
            mainPnl.Controls.Add(new Reservations_Dashboard());
        }

        //on load event
        private async void Reservations_Edit_Load(object sender, EventArgs e)
        {
            await fillControls(toEdit);
        }
    }
}
