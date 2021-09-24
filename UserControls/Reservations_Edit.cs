using EcoLease_Admin.Data;
using EcoLease_Admin.Data.Classes;
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
using static EcoLease_Admin.UserControls.Methods.MessageBoxes;

namespace EcoLease_Admin.UserControls
{
    public partial class Reservations_Edit : UserControl
    {
        Panel mainPnl;
        bool update;
        public Reservations_Edit(Panel pnl, Reservation editable = null)
        {
            InitializeComponent();
            mainPnl = pnl;

            //fill controls
            fillControls(editable);     
        }

        private void fillControls(Reservation editable = null)
        {
            cmbStatus.DataSource = new StatusDataAccess().GetAll();
            cmbCustomer.DataSource = new CustomerDataAccess().GetAll();
            cmbVehicles.DataSource = new VehicleDataAccess().GetAll();

            if(editable != null)
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

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //insert
            if (!update)
            {
                try
                {
                    new ReservationDataAccess().Insert(getReservationObject());
                    MessageBox.Show($"A new reservation just added!", "Successful Action!, Returning to Dashboard");
                    //goes back to the dashboard
                    returnToDashboard();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message, "Unsuccessfull Action!");
                }
            }
            //edit
            else
            {
                try
                {
                    new ReservationDataAccess().Update(getReservationObject(true));
                    MessageBox.Show($"An reservation with ID: {lbID.Text} just updated!", "Returning to Dashboard");
                    //goes back to the dashboard
                    returnToDashboard();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message, "Unsuccessfull Action!");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to exit ?", "Returning to Dashboard", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                returnToDashboard();
            }
        }

        private void returnToDashboard()
        {
            mainPnl.Controls.Clear();
            mainPnl.Controls.Add(new Reservations_Dashboard
                
                ());
        }
    }
}
