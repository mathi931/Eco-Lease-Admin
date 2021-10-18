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
            fillRequestPnl(reservations);
            fillEarningsPanel(reservations);
        }

        private void fillReservationsPnl(List<Reservation> reservations)
        {
            lblActiveAgrCount.Text = reservations.Count(n => n.LeaseBegin >= DateTime.Today && n.LeaseLast < DateTime.Today).ToString();
            lblStartsSoonCount.Text = reservations.Count(n => n.LeaseBegin < DateTime.Today.AddDays(3)).ToString();
            lblEndsSoonCount.Text = reservations.Count(n => n.LeaseBegin < DateTime.Today.AddDays(3)).ToString();
            lblExpiredCount.Text = reservations.Count(n => n.LeaseLast < DateTime.Today).ToString();
            lbReservationsTotalCount.Text = reservations.Count.ToString();
        }

        private void fillRequestPnl(List<Reservation> requests)
        {
            lblPendingRequestCount.Text = reservations.Count(n => n.Status == "Pending").ToString();
            lblConfirmedRequestCount.Text = reservations.Count(n => n.Status == "Confirmed").ToString();
            lblDeclinedRequestCount.Text = reservations.Count(n => n.Status == "Declined").ToString();
        }

        private void fillVehiclePnl(List<Vehicle> vehicles)
        {
            lblOnLeaseCount.Text = vehicles.Count(n => n.Status == "On lease").ToString();
            lblReservedCount.Text = vehicles.Count(n => n.Status == "Reserved").ToString();
            lblAvailableCount.Text = vehicles.Count(n => n.Status == "Available").ToString();
            lblOutOfServiceCount.Text = vehicles.Count(n => n.Status == "Out of service").ToString();
            lbVehiclesTotalCount.Text = vehicles.Count.ToString();
        }

        private void fillEarningsPanel(List<Reservation> reservations)
        {

            //get the income in this month
            int thisMonthSum = 0;

            foreach (var r in reservations)
            {
                if (r.Status == "Active")
                {
                    thisMonthSum += r.Vehicle.Price;
                }
            }
            lbEarningsMonthly.Text = thisMonthSum.ToString();

            //get the income in this year
            int thisYearSum = 0;

            foreach (var r in reservations)
            {
                if (r.Status == "Active")
                {
                    //gets the difference between the lease begin and current date in months
                    var LeasedMonths = (DateTime.Now.Year - r.LeaseBegin.Year) * 12 + DateTime.Now.Month - r.LeaseBegin.Month;
                    thisYearSum += r.Vehicle.Price * LeasedMonths;
                }
                if(r.Status == "Expired")
                {
                    //gets the difference between the lease end and current date in months
                    var leasedMonths = (r.LeaseLast.Year - DateTime.Now.Year) * 12 + r.LeaseLast.Month - DateTime.Now.Month;
                    thisYearSum += r.Vehicle.Price * leasedMonths;
                }
            }
            lbEarningsYearly.Text = thisYearSum.ToString();

            //get all the income
            int totalSum = 0;

            foreach (var r in reservations)
            {
                //gets the difference between the lease begin and current date in months
                int LeasedMonths = (DateTime.Now.Year - r.LeaseBegin.Year) * 12 + DateTime.Now.Month - r.LeaseBegin.Month;
                totalSum = +LeasedMonths * r.Vehicle.Price;
            }
            lbEarningTotal.Text = totalSum.ToString();

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
