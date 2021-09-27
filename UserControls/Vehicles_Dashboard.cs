using EcoLease_Admin.Data;
using EcoLease_Admin.Models;
using EcoLease_Admin.UserControls.Functions;
using FastMember;
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
    public partial class Vehicles_Dashboard : UserControl
    {
        //create a dataTable for insert data to ADGV
        DataTable dt = new DataTable();

        //declared delegate to easily pass data through objects -> selected is visible through different forms
        public delegate void SelectedVehicleChangedEventHandler(object source, Vehicle selected);
        public event SelectedVehicleChangedEventHandler SelectedVehicleChanged;

        public Vehicles_Dashboard()
        {
            InitializeComponent();

            //create and fill dataTable because of ADGV
            //getting the data and save it locally
            using (var reader = ObjectReader.Create(new VehicleDataAccess().GetAll()))
            {
                dt.Load(reader);
            }
            dataTableEdit(dt);
            dgvVehicles.DataSource = dt;
        }

        private void dataTableEdit(DataTable dt)
        {
            dt.Columns[0].ColumnName = "Image";
            dt.Columns[8].ColumnName = "ID";

            dt.SetColumnsOrder("ID", "Make", "Model", "Registered", "PlateNo", "Km", "Status", "Notes", "Image", "Price");
        }

        private void dgvVehicles_FilterStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = dgvVehicles.FilterString;
        }

        private void dgvVehicles_SortStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = dgvVehicles.SortString;
        }

        private void dgvVehicles_SelectionChanged(object sender, EventArgs e)
        {
            if (dgvVehicles.SelectedRows.Count > 0 && rowToObject(dgvVehicles.SelectedRows[0]) as Vehicle != null)
            {
                //Console.WriteLine(rowToObject(dgvVehicles.SelectedRows[0]));
                OnSelectedVehicleChanged(rowToObject(dgvVehicles.SelectedRows[0]));
            }
            else
            {
                OnSelectedVehicleChanged(new Vehicle());
            }
        }

        protected virtual void OnSelectedVehicleChanged(Vehicle selected)
        {
            if (SelectedVehicleChanged != null)
                SelectedVehicleChanged(this, selected);
        }

        private Vehicle rowToObject(DataGridViewRow row)
        {
            Vehicle v = new Vehicle();
            v.VId = (int)row.Cells[0].Value;
            v.Make = row.Cells[1].Value.ToString();
            v.Model = row.Cells[2].Value.ToString();
            v.Registered = (int)row.Cells[3].Value;
            v.PlateNo = row.Cells[4].Value.ToString();
            v.Km = (int)row.Cells[5].Value;
            v.Status = row.Cells[6].Value.ToString();
            v.Notes = row.Cells[7].Value.ToString();
            v.Img = row.Cells[8].Value.ToString();
            v.Price = (int)row.Cells[9].Value;
            return v;
        }
    }
}
