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
                    break;
                //edit agr -option 2
                case 2:
                    container.Controls.Clear();
                    container.Controls.Add(new Reservations_Edit(container, passed));
                    break;
                default:
                    MessageBox.Show("Error!");
                    break;
            }
        }

        private void btnAgreements_Click(object sender, EventArgs e)
        {
            showPanel();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            showPanel(1);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                showPanel(2, selected);
            }
            else
            {
                errorMessage("Select a Reservation to accept it!", "Error!");
            }
        }

        private async void btnRemove_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a contract first to remove!");
            }
            else if (selected != null && MessageBox.Show($"Are you sure to delete {selected.Customer} with ID: {selected.RId} ?", "Removing Contract", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {

                await new ReservationProcessor().RemoveReservation(selected.RId);
                showPanel();
            }
        }

        public void OnSelectedAgreementChanged(object source, Reservation selectedV)
        {
            this.selected = selectedV;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                Agreement agr = new Agreement(selected);
                agr.AgreementPDF();
                MessageBox.Show("YUHEEE" + selected.Customer.FirstName + "has a contract!");
            }
        }

        //event for decline a reservation
        private async void btnDecline_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                if (selected.Status == "Pending")
                {
                    if(dynamicQuestion("Are you sure to decline", $"{ selected.Customer.LastName}'s reservation with {selected.Vehicle} ?", "Declining Reservation") == DialogResult.OK)
                    {
                        await new ReservationProcessor().UpdateReservationStatus(selected.RId, "Declined");
                        infoMessage($"{selected.Customer}'s reservation successfully Declined");
                        //refreshes the page
                        showPanel();
                    }
                    else
                    {
                        infoMessage("Canceled action!");
                    }
                }
                else
                {
                    errorMessage("Selected Reservation must be on pending state!", "Error!");
                }
            }
            else
            {
                errorMessage("Select a Reservation to decline it!", "Error!");
            }
        }

        //event for accept a reservation
        private async void btnAccept_Click(object sender, EventArgs e)
        {
            if (selected != null)
            {
                if (selected.Status == "Pending")
                {
                    if (dynamicQuestion("Are you sure to confirm", $"{ selected.Customer.LastName}'s reservation with {selected.Vehicle} ?", "Confirm Reservation") == DialogResult.OK)
                    {
                        //changes the status
                        await new ReservationProcessor().UpdateReservationStatus(selected.RId, "Confirmed");
                        infoMessage($"{selected.Customer}'s reservation successfully Accepted");
                        //create a contract
                        Agreement agr = new Agreement(selected);
                        await new AgreementProcessor().InsertAgreement(agr);
                        agr.savePDF(agr.AgreementPDF());
                        //refreshes the page
                        showPanel();
                    }
                    else
                    {
                        infoMessage("Canceled action!");
                    }
                }
                else
                {
                    errorMessage("Selected Reservation must be on pending state!", "Error!");
                }
            }
            else
            {
                errorMessage("Select a Reservation to accept it!", "Error!");
            }
        }
    }
}
