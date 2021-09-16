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
            RequestDataAccess db = new RequestDataAccess();

            List<Request> r = db.GetAll();


            output.Text = $"{r[0]}\n{r[1]}";
        }
    }
}
