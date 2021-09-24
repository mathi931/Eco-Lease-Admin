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
            if (selected == null)
            {
                MessageBox.Show("Please select a contract first to edit!");
            }
            else if (selected != null)
            {
                showPanel(2, selected);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a contract first to remove!");
            }
            else if (selected != null && MessageBox.Show($"Are you sure to delete {selected.Customer} with ID: {selected.RId} ?", "Removing Contract", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {

                new ReservationDataAccess().Remove(selected);
                showPanel();
            }
        }

        public void OnSelectedAgreementChanged(object source, Reservation selectedV)
        {
            this.selected = selectedV;
            //Console.WriteLine(selected);
        }
    }
}
