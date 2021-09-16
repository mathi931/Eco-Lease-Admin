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
            //test the requests
            AgreementDataAccess db = new AgreementDataAccess();

            List<Agreement> r = db.GetAll();

            foreach (var item in r)
            {
                output.Text += $"ID:{item.AId} PERIOD:{item.LeaseBegin} - {item.LeaseLast} USER: {item.User.UId}. {item.User.FirstName} VEHICLE: {item.Vehicle.PlateNo} \n";
            }
        }
    }
}
