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
    public partial class Agreements : UserControl
    {
        Agreement selected = new Agreement();

        public Agreements()
        {
            InitializeComponent();
            showPanel();
        }

        private void showPanel(byte i = 0, Agreement passed = null)
        {
            switch (i)
            {   //show dashboard - default
                case 0:
                    Agreements_Dashboard dashboard = new Agreements_Dashboard();
                    dashboard.SelectedAgreementChanged += this.OnSelectedAgreementChanged;
                    container.Controls.Clear();
                    container.Controls.Add(dashboard);
                    break;
                //add new agr -option 1
                case 1:
                    container.Controls.Clear();
                    container.Controls.Add(new Agreements_Edit(container));
                    break;
                //edit agr -option 2
                case 2:
                    container.Controls.Clear();
                    container.Controls.Add(new Agreements_Edit(container, passed));
                    break;
                default:
                    MessageBox.Show("Error!");
                    break;
            }
        }

        private void btnAgreements_Click(object sender, EventArgs e)
        {
            showPanel();
        }

        private void btnNew_Click(object sender, EventArgs e)
        {
            showPanel(1);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a contract first to edit!");
            }
            else if (selected != null)
            {
                showPanel(2, selected);
            }
        }

        private void btnRemove_Click(object sender, EventArgs e)
        {
            if (selected == null)
            {
                MessageBox.Show("Please select a contract first to remove!");
            }
            else if (selected != null && MessageBox.Show($"Are you sure to delete {selected.User} with ID: {selected.AId} ?", "Removing Contract", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {

                new AgreementDataAccess().Remove(selected);
                showPanel();
            }
        }

        public void OnSelectedAgreementChanged(object source, Agreement selectedV)
        {
            this.selected = selectedV;
            //Console.WriteLine(selected);
        }
    }
}
