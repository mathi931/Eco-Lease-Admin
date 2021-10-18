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
using static EcoLease_Admin.UserControls.Methods.MessageBoxes;

namespace EcoLease_Admin.UserControls
{
    public partial class Vehicles : UserControl
    {
        //local variable for selected vehicle and the data processor
        Vehicle selected = new Vehicle();
        VehicleProcessor vehProcessor = new VehicleProcessor();

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

        //changes the local selected variable on gridview selection change
        public void OnSelectedVehicleChanged(object source, Vehicle selectedV)
        {
            this.selected = selectedV;
        }

        //opens the dashboard
        private void btnFleet_Click(object sender, EventArgs e)
        {
            showPanel();
        }

        //opens the edit view
        private void btnAdd_Click(object sender, EventArgs e)
        {
            showPanel(1);
        }

        //if there is selected element shows the edit view with the selected vehicle
        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                ErrorMessage("Please select a car first to edit!", "Error");
            }
            else if (selected != null)
            {
                showPanel(2, selected);
            }
        }

        //clicking on remove button, if there is selected sends the delete request and response for the user then changes the view to dashboard
        private async void btnRemove_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                ErrorMessage("Please select a car first to remove!", "Error!");
            }
            else if (selected != null && DynamicQuestion("remove", $"the {selected.Make} with ID: {selected.VId} ?", "Removing Vehicle") == DialogResult.OK)
            {
                await vehProcessor.RemoveVehicle(selected.VId);
                if (InfoMessage($"{selected.VId} Successfully removed!") == DialogResult.OK)
                {
                    showPanel();
                }
            }
        }
    }
}
