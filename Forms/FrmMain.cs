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
            User user = new User { Id = 1, FirstName = "Ferenc", LastName = "Kerekes", DateOfBirth = new DateTime(2015, 12, 2) };

            UserDataAccess db = new UserDataAccess();
            db.Update(user);

            u = new List<User>(db.GetAll());
            for (int i = 0; i < u.Count; i++)
            {
                output.Text += $" {u[i].FirstName}";
            }
        }
    }
}
