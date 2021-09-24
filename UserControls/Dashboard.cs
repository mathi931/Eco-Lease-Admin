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
        List<Reservation> agreements = new List<Reservation>();
        List<Request> requests = new List<Request>();

        public Dashboard()
        {
            InitializeComponent();
            //get all data
            getAllData();
            //fill the data in
            fillView(vehicles, agreements, requests);
        }

        private void fillView(List<Vehicle> vehicles, List<Reservation> agreements, List<Request> requests)
        {
            fillVehiclePnl(vehicles);
            fillAgreementsPnl(agreements);
            fillRequestsPnl(requests);
        }

        private void fillRequestsPnl(List<Request> requests)
        {
            lblPendingRequestCount.Text = requests.Count(n => n.Status == "Pending").ToString();
            lblConfirmedRequestCount.Text = requests.Count(n => n.Status == "Confirmed").ToString();
            lblDeclinedRequestCount.Text = requests.Count(n => n.Status == "Declined").ToString();
        }

        private void fillAgreementsPnl(List<Reservation> agreements)
        {
            lblActiveAgrCount.Text = agreements.Count(n => n.LeaseBegin >= DateTime.Today && n.LeaseLast < DateTime.Today).ToString();
            lblStartsSoonCount.Text = agreements.Count(n => n.LeaseBegin < DateTime.Today.AddDays(3)).ToString();
            lblEndsSoonCount.Text = agreements.Count(n => n.LeaseBegin < DateTime.Today.AddDays(3)).ToString();
            lblExpiredCount.Text = agreements.Count(n => n.LeaseLast < DateTime.Today).ToString();
        }

        private void fillVehiclePnl(List<Vehicle> vehicles)
        {
            lblOnLeaseCount.Text = vehicles.Count(n => n.Status == "On lease").ToString();
            lblReservedCount.Text = vehicles.Count(n => n.Status == "Reserved").ToString();
            lblAvailableCount.Text = vehicles.Count(n => n.Status == "Available").ToString();
            lblOutOfServiceCount.Text = vehicles.Count(n => n.Status == "Out of service").ToString();
        }

        //gets all the data and saves it locally
        private void getAllData()
        {
            vehicles = new VehicleDataAccess().GetAll();
            agreements = new ReservationDataAccess().GetAll();
            requests = new RequestDataAccess().GetAll();

        }
    }
}
