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

namespace EcoLease_Admin
{
    public partial class FrmMain : Form
    {
        List<User> u = new List<User>();
        public FrmMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //VehicleDataAccess db = new VehicleDataAccess();
            RequestDataAccess db = new RequestDataAccess();

            //Vehicle vagi = new Vehicle(1001, "Seat", "Leon", new DateTime(2020, 02, 12), "SD-222-EL", 223252, null, "On Lease");

            List<Request> r = db.GetAll();

            output.Text = $"{r[0]}\n{r[1]}";

            //var list = new List<Vehicle>(db.GetAll());

            //for (int i = 0; i < list.Count; i++)
            //{
            //    output.Text += $"{list[i].Id} {list[i].Make} {list[i].Model} {list[i].Status}";
            //}
        }
    }
}
