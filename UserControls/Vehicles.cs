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
    public partial class Vehicles : UserControl
    {
        Vehicle selected = new Vehicle();

        public Vehicles()
        {
            InitializeComponent();
            //after initailized the components calls the show panel function 
            showPanel();
        }

        //loads usercontrol depends which button is clicked(edit, new or dashboard)
        private void showPanel(byte i = 0, Vehicle v = null)
        {
            switch (i)
            {   //show dashboard - default
                case 0:
                    Vehicles_Dashboard dashboard = new Vehicles_Dashboard();
                    dashboard.SelectedVehicleChanged += this.OnSelectedVehicleChanged;
                    container.Controls.Clear();
                    container.Controls.Add(dashboard);
                    break;
                //add new vehicle -option 1
                case 1:
                    container.Controls.Clear();
                    container.Controls.Add(new Vehicles_Edit(container));
                    break;
                //edit vehicle -option 2
                case 2:
                    container.Controls.Clear();
                    container.Controls.Add(new Vehicles_Edit(container, v));
                    break;
                default:
                    MessageBox.Show("Error!");
                    break;
            }
        }

        public void OnSelectedVehicleChanged(object source, Vehicle selectedV)
        {
            this.selected = selectedV;
        }

        private void btnFleet_Click(object sender, EventArgs e)
        {
            showPanel();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            showPanel(1);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a car first to edit!");
            }
            else if (selected != null)
            {
                showPanel(2, selected);
                MessageBox.Show(selected.VId.ToString());
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a car first to remove!");
            }
            else if (selected != null && MessageBox.Show($"Are you sure to remove the {selected.Make} with ID: {selected.VId} ?", "Removing Vehicle", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                new VehicleDataAccess().Remove(selected.VId);
                showPanel();
                MessageBox.Show($"{selected.VId} Successfully removed!");
            }
        }
    }
}
