﻿using EcoLease_Admin.Data;
using EcoLease_Admin.Data.Classes;
using EcoLease_Admin.Models;
using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using static EcoLease_Admin.UserControls.Methods.MessageBoxes;

namespace EcoLease_Admin.UserControls
{
    public partial class Vehicles_Edit : UserControl
    {
        Panel mainPnl;
        bool update = false;
        string imagePath = "";

        public Vehicles_Edit()
        {
            InitializeComponent();
            cmbStatus.DataSource = new StatusDataAccess().GetAll();
            numYear.Maximum = DateTime.Now.Year;
            imagePath = $"{Directory.GetParent(Environment.CurrentDirectory).Parent.FullName}\\Resources\\";
        }

        public Vehicles_Edit(Panel pnl, Vehicle editable = null) : this()
        {
            mainPnl = pnl;
            if (editable != null)
            {
                fillControls(editable, imagePath);
                update = true;
            }
        }

        private void fillControls(Vehicle v, string imgPath)
        {
            lbID.Text = v.VId.ToString();
            txbMake.Text = v.Make;
            txbModel.Text = v.Model;
            numYear.Value = v.Registered;
            txbPlateNo.Text = v.PlateNo;
            numKm.Value = v.Km;
            cmbStatus.SelectedIndex = cmbStatus.FindStringExact(v.Status);
            txbNotes.Text = v.Notes;
            lbIMG.Text = v.Img;
            picBox.ImageLocation = $"{imgPath}{v.Img}";
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //insert
            if (!update)
            {
                try
                {
                    //save image locally
                    saveImage(imagePath);
                    new VehicleDataAccess().Insert(createVehicle());
                    MessageBox.Show($"A new vehicle with plate number: {txbPlateNo.Text} just added!", "Successful Action!, Returning to Dashboard");
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
                    //save image locally
                    saveImage(imagePath);
                    new VehicleDataAccess().Update(createVehicle());
                    MessageBox.Show($"A vehicle with plate number: {txbPlateNo.Text} just updated!", "Returning to Dashboard");
                    //goes back to the dashboard
                    returnToDashboard();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message, "Unsuccessfull Action!");
                }
            }
        }

        private void saveImage(string imgPath)
        {
            //gets the new name
            string fileName = getNewImageName();
            //creates the full path what is in the resources folder with the new file name
            string fullPath = imagePath + fileName;

            //if there is img and not in the folder yet
            if (!String.IsNullOrEmpty(picBox.ImageLocation) && !fileExist(imagePath, picBox.ImageLocation))
            {
                //copies the uploaded image to local directory with new file name
                File.Copy(picBox.ImageLocation, fullPath);

                //changes the labels text to a new files name
                lbIMG.Text = fileName;
            }
            //if there is image but the folder already contains it
            else if (!String.IsNullOrEmpty(picBox.ImageLocation) && fileExist(imagePath, picBox.ImageLocation))
            {

                lbIMG.Text = fileName;
            }
        }

        private string getNewImageName()
        {
            var vehicles = new VehicleDataAccess().GetAll();
            //selects the highest number in the images fileName ex(img124.jpg) -> for each record targets the image's fileName -> cuts out the number -> descending them -> selects the first so the largest number;
            var lastImg = vehicles.OrderByDescending(v => Int32.Parse(Regex.Match(v.Img, @"\d+").Value)).First().Img;
            //returns the last image strings incremented by 1
            return Regex.Replace(lastImg, "\\d+",
    m => (int.Parse(m.Value) + 1).ToString(new string('0', m.Value.Length)));
        }

        private bool fileExist(string path, string selected)
        {
            //default result
            bool result = false;

            //get all files from resources folder
            string[] files = Directory.GetFiles(path);

            //get the char count of the selected file
            var selectedData = Encoding.UTF8.GetCharCount(File.ReadAllBytes(selected));


            foreach (var item in files)
            {
                //if one of the files charcount in the folders equals with the selected charcount => file is tha same => change the result to true;
                var itemData = Encoding.UTF8.GetCharCount(File.ReadAllBytes(item));

                if (itemData == selectedData)
                {
                    result = true;
                }
            }
            //returns the result
            return result;
        }

        private void returnToDashboard()
        {
            mainPnl.Controls.Clear();
            mainPnl.Controls.Add(new Vehicles_Dashboard());
        }

        private Vehicle createVehicle()
        {
            return new Vehicle
            {
                VId = Convert.ToInt32(lbID.Text),
                Make = txbMake.Text,
                Model = txbModel.Text,
                Registered = (int)numYear.Value,
                PlateNo = txbPlateNo.Text,
                Km = (int)numKm.Value,
                Notes = txbNotes.Text,
                Status = cmbStatus.SelectedItem.ToString(),
                Img = lbIMG.Text
            };
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to exit ?", "Returning to Dashboard", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                returnToDashboard();
            }
        }

        private void btnUploadImg_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();

                dialog.Filter = "JPG(*.JPG)|*.jpg";

                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    picBox.ImageLocation = dialog.FileName;
                }
            }
            catch (Exception ex)
            {
                errorMessage(ex.Message, "Upload Unsuccesfull!");
            }
        }

    }
}
