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
using static EcoLease_Admin.Data.Classes.DataAccessHelper;

namespace EcoLease_Admin.UserControls
{
    public partial class Reservations_Dashboard : UserControl
    {
        //create a dataTable for insert data to ADGV
        DataTable dt = new DataTable();
        List<Reservation> list = new List<Reservation>();

        //declared delegate to easily pass data through objects
        //I can see in other objects which is the selected object on the datagridview
        public delegate void SelectedReservationChangedEventHandler(object source, Reservation selected);
        public event SelectedReservationChangedEventHandler SelectedReservationChanged;

        public Reservations_Dashboard()
        {
            InitializeComponent();
        }

        //on selected index change
        private void dgvAgreements_SelectionChanged(object sender, EventArgs e)
        {
            if (dataGridView.SelectedRows.Count > 0)
            {
                OnSelectedReservationChanged(selected(dataGridView.SelectedRows[0]));
            }
            else
            {
                OnSelectedReservationChanged(new Reservation());
            }
        }
        protected virtual void OnSelectedReservationChanged(Reservation selected)
        {
            if (SelectedReservationChanged != null)
            {
                SelectedReservationChanged(this, selected);
            }
        }

        //convert row to object
        Reservation selected(DataGridViewRow row)
        {
            return list.Find(e => e.RId == (int)row.Cells[0].Value);
        }

        DataTable ReservationDT()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("ID", typeof(int));
            dt.Columns.Add("Lease From", typeof(DateTime));
            dt.Columns.Add("Lease Until", typeof(DateTime));
            dt.Columns.Add("Status", typeof(string));
            dt.Columns.Add("Customer", typeof(Customer));
            dt.Columns.Add("Vehicle", typeof(Vehicle));

            return dt;
        }
        DataTable fillDataTable(DataTable dt, List<Reservation> reservations)
        {
            foreach (var res in reservations)
            {
                dt.Rows.Add(res.RId, res.LeaseBegin, res.LeaseLast, res.Status, res.Customer, res.Vehicle);
            }
            return dt;
        }

        //filtering and sorting config
        private void dgvAgreements_FilterStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.RowFilter = dataGridView.FilterString;
        }

        private void dgvAgreements_SortStringChanged(object sender, EventArgs e)
        {
            dt.DefaultView.Sort = dataGridView.SortString;
        }

        //changes background color depends on status
        private void dataGridView_CellFormatting(object sender, DataGridViewCellFormattingEventArgs e)
        {
            foreach (DataGridViewRow row in dataGridView.Rows)
            {
                switch (row.Cells["Status"].Value.ToString())
                {
                    case "Pending":
                        row.DefaultCellStyle.BackColor = Color.Orange;
                        break;
                    case "Confirmed":
                        row.DefaultCellStyle.BackColor = Color.Green;
                        break;
                    case "Declined":
                        row.DefaultCellStyle.BackColor = Color.Red;
                        break;

                    default:
                        row.DefaultCellStyle.BackColor = Color.White;
                        break;
                }
            }
        }
        private async void dataGridView_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            var clickedRow = dataGridView.Rows[e.RowIndex];

            if(clickedRow.Cells["Status"].Value.ToString() == "Confirmed")
            {
                var fileName = await new AgreementProcessor().GetFileName(Convert.ToInt32(clickedRow.Cells["ID"].Value.ToString()));

                var path = $"{LocalHDDPath()}{fileName}";

                System.Diagnostics.Process.Start(path);
            }
        }

        private async void Reservations_Dashboard_Load(object sender, EventArgs e)
        {
            //getting the data and save it locally into a list
            list = await new ReservationProcessor().LoadReservations();
            //create and fill dataTable because of ADGV
            dt = fillDataTable(ReservationDT(), list);
            dataGridView.DataSource = dt;
        }
    }
}
