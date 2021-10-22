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
    public partial class Reservations : UserControl
    {
        //local variable for the selected object from gridview
        Reservation selected = new Reservation();

        public Reservations()
        {
            InitializeComponent();
            showPanel();
        }

        private void showPanel(byte i = 0, Reservation passed = null)
        {
            switch (i)
            {   //show dashboard - default
                case 0:
                    Reservations_Dashboard dashboard = new Reservations_Dashboard();
                    dashboard.SelectedReservationChanged += this.OnSelectedAgreementChanged;
                    container.Controls.Clear();
                    container.Controls.Add(dashboard);
                    break;
                //add new agr -option 1
                case 1:
                    container.Controls.Clear();
                    container.Controls.Add(new Reservations_Edit(container));
                    btnAccept.Visible = false;
                    btnDecline.Visible = false;
                    break;
                //edit agr -option 2
                case 2:
                    container.Controls.Clear();
                    container.Controls.Add(new Reservations_Edit(container, passed));
                    btnAccept.Visible = false;
                    btnDecline.Visible = false;
                    break;
                default:
                    break;
            }
        }

        //shows the dashboard
        private void btnAgreements_Click(object sender, EventArgs e)
        {
            showPanel();
        }

        //shows the edit view
        private void btnNew_Click(object sender, EventArgs e)
        {
            showPanel(1);
        }

        //if there is selected object, shows the edit view passed with the selected object
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                showPanel(2, selected);
            }
            else
            {
                ErrorMessage("Select a Reservation to accept it!", "Error!");
            }
        }

        //removes an object
        private async void btnRemove_Click(object sender, EventArgs e)
        {
            //if there is no object selected
            if (selected == null)
            {
                InfoMessage("Please select a contract first to remove!");
            }

            else if (selected != null && DynamicQuestion("delete", $"{selected.Customer} with ID: {selected.RId} ?", "Removing Contract") == DialogResult.OK)
            {
                //sends a delete request after user confirmation
                await new ReservationProcessor().RemoveReservation(selected.RId);

                showPanel();
            }
        }

        //on selection change changes the local variable
        public void OnSelectedAgreementChanged(object source, Reservation selectedV)
        {
            this.selected = selectedV;
        }

        //event for decline a reservation
        private async void btnDecline_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                if (selected.Status == "Pending")
                {
                    //if there is selected vehicle with Pending status
                    //after user confirmation sends a put request with the selected object
                    if(DynamicQuestion("Are you sure to decline", $"{ selected.Customer.LastName}'s reservation with {selected.Vehicle} ?", "Declining Reservation") == DialogResult.OK)
                    {
                        await new ReservationProcessor().UpdateReservationStatus(selected.RId, "Declined");

                        //refreshes the view
                        if(InfoMessage($"{selected.Customer}'s reservation successfully Declined") == DialogResult.OK)
                        {
                            showPanel();
                        }

                    }
                    else
                    {
                        InfoMessage("Canceled action!");
                    }
                }
                else
                {
                    ErrorMessage("Selected Reservation must be on pending state!", "Error!");
                }
            }
            else
            {
                ErrorMessage("Select a Reservation to decline it!", "Error!");
            }
        }

        //event for accept a reservation
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                if (selected.Status == "Pending")
                {
                    if (DynamicQuestion("Are you sure to confirm", $"{ selected.Customer.LastName}'s reservation with {selected.Vehicle} ?", "Confirm Reservation") == DialogResult.OK)
                    {
                        //if there is selected vehicle with Pending status
                        //after user confirmation sends a put request with the selected object
                        await new ReservationProcessor().UpdateReservationStatus(selected.RId, "Active");

                        selected.Vehicle.Status = "On lease";
                        await new VehicleProcessor().UpdateVehicleStatus(selected.Vehicle);

                        //create a contract
                        Agreement agr = new Agreement(selected);
                        await new AgreementProcessor().InsertAgreement(agr);
                        var pdf = agr.AgreementPDF();

                        await agr.uploadPDF(pdf);


                        //refreshes the view
                        if (InfoMessage($"{selected.Customer}'s reservation successfully Accepted") == DialogResult.OK)
                        {
                            showPanel();
                        }
                    }
                    else
                    {
                        InfoMessage("Canceled action!");
                    }
                }
                else
                {
                    ErrorMessage("Selected Reservation must be on pending state!", "Error!");
                }
            }
            else
            {
                ErrorMessage("Select a Reservation to accept it!", "Error!");
            }
        }
    }
}
