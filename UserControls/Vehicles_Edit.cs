using EcoLease_Admin.Data;
using EcoLease_Admin.Data.Classes;
using EcoLease_Admin.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static EcoLease_Admin.UserControls.Methods.MessageBoxes;
using static EcoLease_Admin.Data.Classes.DataAccessHelper;
using static EcoLease_Admin.Models.Operations;
using System.Threading.Tasks;
using System.Collections.Generic;
using EcoLease_Admin.UserControls.Methods;
using EcoLease_Admin.Validators;
using FluentValidation.Results;
using System.Drawing;

namespace EcoLease_Admin.UserControls
{
    public partial class Vehicles_Edit : UserControl
    {
        Panel mainPnl;
        bool update = false;
        List<Vehicle> vehicles = new List<Vehicle>();

        //declare the local processor
        VehicleProcessor vehicleProc = new VehicleProcessor();

        //constructors, depends on declaration passed a value or not can be a view for update or add a new vehicle
        public Vehicles_Edit()
        {
            InitializeComponent();
            SetNumPickers();
        }


        public Vehicles_Edit(Panel pnl, Vehicle editable = null) : this()
        {
            mainPnl = pnl;
            if (editable != null)
            {
                fillControls(editable);
                update = true;
            }
        }

        //fills the input controls with the editable vehicle
        private void fillControls(Vehicle v)
        {
            lbTitle.Text = "Update Vehicle";
            lbID.Text = v.VId.ToString();
            txbMake.Text = v.Make;
            txbModel.Text = v.Model;
            numYear.Value = v.Registered;
            txbPlateNo.Text = v.PlateNo;
            numKm.Value = v.Km;
            cmbStatus.SelectedIndex = cmbStatus.FindStringExact(v.Status);
            txbNotes.Text = v.Notes;
            lbIMG.Text = v.Img;
            numPrice.Value = v.Price;
            picBox.LoadAsync($"http://localhost:12506/api/Files/{v.Img}");
        }

        private async void btnConfirm_Click(object sender, EventArgs e)
        {
            //declares the validator and the vehicle object what created from input data
            VehicleValidator validator = new VehicleValidator();
            var vehicle = createVehicle();

            //validation result based on input
            ValidationResult result = validator.Validate(vehicle);

            //on invalid input loops through the invalid inputs and notify the user
            if (!result.IsValid)
            {
                foreach (var failure in result.Errors)
                {
                    ErrorMessage("Property " + failure.PropertyName + " failed validation. Error was: " + failure.ErrorMessage, "Validation Error");
                }
                return;
            }
            //create a binary img
            var binaryImg = ImageToByteArray(picBox.Image);

            //insert if input is valid
            if (!update)
            {
                try
                {
                    //sends the post request with the valid object
                    await vehicleProc.InsertVehicle(vehicle);

                    //uploads img
                    await new FileProcessor().InsertFile(binaryImg ,vehicle.Img);

                    //after notify the user changes the view to dashboard
                    if (InfoMessage($"A new vehicle with plate number: {txbPlateNo.Text} just added!") == DialogResult.OK)
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
                    //have to check if the updated vehicles img is the same otherwise not upload it 


                    //sends put request with the valid object
                    await vehicleProc.UpdateVehicle(vehicle);

                    //uploads img
                    await new FileProcessor().InsertFile(binaryImg, vehicle.Img);

                    

                    //after notify the user changes the view to dashboard
                    if (InfoMessage($"A new vehicle with plate number: {txbPlateNo.Text} just updates!") == DialogResult.OK)
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

        //creates file name
        private string getFileName()
        {
            //selects the last id and add one to it
            var lastID = vehicles.OrderByDescending(v => v.VId).First().VId++;

            //returns the edited string
            return $"Image{lastID}.jpg";
        }


        //changes the view to Dashboard
        private void returnToDashboard()
        {
            mainPnl.Controls.Clear();
            mainPnl.Controls.Add(new Vehicles_Dashboard());
        }

        //creates a Vehicle object from input values
        private Vehicle createVehicle()
        {
            var vehicle = new Vehicle
            {
                Make = txbMake.Text,
                Model = txbModel.Text,
                Registered = (int)numYear.Value,
                PlateNo = txbPlateNo.Text,
                Km = (int)numKm.Value,
                Notes = txbNotes.Text,
                Status = cmbStatus.SelectedItem.ToString(),
                Img = lbIMG.Text,
                Price = (int)numPrice.Value
            };

            if (lbID.Text.Trim().Length > 0 && Int32.TryParse(lbID.Text, out int res))
            {
                vehicle.VId = res;
            }
            return vehicle;
            
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to exit ?", "Returning to Dashboard", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                returnToDashboard();
            }
        }

        //select image from folder
        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Filter = "JPG(*.JPG)|*.jpg";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    picBox.ImageLocation = dialog.FileName;
                    lbIMG.Text = getFileName();
                }
            }
            catch (Exception ex)
            {
                ErrorMessage(ex.Message, "Upload Unsuccesfull!");
            }
        }

        //on load event
        private async void Vehicles_Edit_Load(object sender, EventArgs e)
        {
            //set status combobox values
            var statusProc = new StatusProcessor();
            cmbStatus.DataSource = await statusProc.LoadStatuses();
            vehicles = await vehicleProc.LoadVehicles();
        }

        //sets numeric input values
        private void SetNumPickers()
        {
            int yearNow = DateTime.Now.Year;

            var inputSetter = new SetInput();
            inputSetter.SetNumUpDown(numKm, 100, 100, 200000);
            inputSetter.SetNumUpDown(numPrice, 200, 200, 1000);
            inputSetter.SetNumUpDown(numYear, yearNow, yearNow - 20, yearNow);
        }
    }
}
