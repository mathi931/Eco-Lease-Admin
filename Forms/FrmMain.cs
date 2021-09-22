using EcoLease_Admin.Data;
using EcoLease_Admin.Data.Classes;
using EcoLease_Admin.Models;
using EcoLease_Admin.UserControls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EcoLease_Admin
{
    public partial class FrmMain : Form
    {
        List<User> u = new List<User>();
        public FrmMain()
        {
            InitializeComponent();
        }


        private void FrmMain_Load(object sender, EventArgs e)
        {
            Main_Dashboard dashboard = new Main_Dashboard();
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

    }
}
