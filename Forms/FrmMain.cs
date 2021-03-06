using EcoLease_Admin.Data;
using EcoLease_Admin.Models;
using EcoLease_Admin.UserControls;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;


namespace EcoLease_Admin
{
    public partial class FrmMain : Form
    {
        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            showControl(this.container, dashboard);
        }
        public void showControl(Panel container, Control control)
        {
            container.Controls.Clear();
            control.Dock = DockStyle.Fill;
            control.BringToFront();
            control.Focus();
            container.Controls.Add(control);
        }

        private void btnDashboard_Click(object sender, EventArgs e)
        {
            Dashboard dashboard = new Dashboard();
            showControl(this.container, dashboard);
        }

        private void btnVehicles_Click(object sender, EventArgs e)
        {
            Vehicles vehicles = new Vehicles();
            showControl(this.container, vehicles);
        }

        private void btnAgreements_Click(object sender, EventArgs e)
        {
            Reservations agreements = new Reservations();
            showControl(this.container, agreements);
        }

        private void btnCustomers_Click(object sender, EventArgs e)
        {
            Customers customers = new Customers();
            showControl(this.container, customers);
        }
    }
}
