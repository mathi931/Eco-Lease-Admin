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
    public partial class Dashboard : UserControl
    {
        List<Vehicle> vehicles = new List<Vehicle>();
        List<Reservation> reservations = new List<Reservation>();

        public Dashboard()
        {
            InitializeComponent();
        }

        private void fillView(List<Vehicle> vehicles, List<Reservation> reservations)
        {
            fillVehiclePnl(vehicles);
            fillReservationsPnl(reservations);
        }

        private void fillReservationsPnl(List<Reservation> reservations)
        {
            lblActiveAgrCount.Text = reservations.Count(n => n.LeaseBegin >= DateTime.Today && n.LeaseLast < DateTime.Today).ToString();
            lblStartsSoonCount.Text = reservations.Count(n => n.LeaseBegin < DateTime.Today.AddDays(3)).ToString();
            lblEndsSoonCount.Text = reservations.Count(n => n.LeaseBegin < DateTime.Today.AddDays(3)).ToString();
            lblExpiredCount.Text = reservations.Count(n => n.LeaseLast < DateTime.Today).ToString();
        }

        private void fillVehiclePnl(List<Vehicle> vehicles)
        {
            lblOnLeaseCount.Text = vehicles.Count(n => n.Status == "On lease").ToString();
            lblReservedCount.Text = vehicles.Count(n => n.Status == "Reserved").ToString();
            lblAvailableCount.Text = vehicles.Count(n => n.Status == "Available").ToString();
            lblOutOfServiceCount.Text = vehicles.Count(n => n.Status == "Out of service").ToString();
        }

        //gets all the data and saves it locally
        private async Task getAllData()
        {
            vehicles =  await new VehicleProcessor().LoadVehicles();

            reservations = await new ReservationProcessor().LoadReservations();
        }

        private async void Dashboard_Load(object sender, EventArgs e)
        {
            //gets all the data
            await getAllData();

            //fill the data in
            fillView(vehicles, reservations);
        }
    }
}
