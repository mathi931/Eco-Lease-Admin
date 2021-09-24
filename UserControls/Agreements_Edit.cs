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
using static EcoLease_Admin.UserControls.Methods.MessageBoxes;

namespace EcoLease_Admin.UserControls
{
    public partial class Agreements_Edit : UserControl
    {
        Panel mainPnl;
        bool update;
        public Agreements_Edit(Panel pnl, Agreement editable = null)
        {
            InitializeComponent();
            mainPnl = pnl;

            //fill controls
            fillControls(editable);     
        }

        private void fillControls(Agreement editable = null)
        {
            cmbStatus.DataSource = new StatusDataAccess().GetAll();
            cmbUsers.DataSource = new UserDataAccess().GetAll();
            cmbVehicles.DataSource = new VehicleDataAccess().GetAll();

            if(editable != null)
            {
                lbID.Text = editable.AId.ToString();
                dtpLeaseBegin.Value = editable.LeaseBegin;
                dtpLeaseLast.Value = editable.LeaseLast;
                cmbStatus.SelectedIndex = cmbStatus.FindStringExact(editable.Status);
                cmbUsers.SelectedIndex = cmbUsers.FindStringExact(editable.User.ToString());
                cmbVehicles.SelectedIndex = cmbVehicles.FindStringExact(editable.Vehicle.ToString());
                update = true;
            }
        }

        private Agreement getAgreementObject(bool update=false)
        {
            if (update)
            {
                return new Agreement(Convert.ToInt32(lbID.Text), dtpLeaseBegin.Value, dtpLeaseLast.Value, cmbStatus.SelectedItem.ToString(), cmbUsers.SelectedItem as User, cmbVehicles.SelectedItem as Vehicle);
            }
            else
            {
            return new Agreement(dtpLeaseBegin.Value, dtpLeaseLast.Value, cmbStatus.SelectedItem.ToString(), cmbUsers.SelectedItem as User, cmbVehicles.SelectedItem as Vehicle);
            }
        }

        private void btnConfirm_Click(object sender, EventArgs e)
        {
            //insert
            if (!update)
            {
                try
                {
                    new AgreementDataAccess().Insert(getAgreementObject());
                    MessageBox.Show($"A new agreement just added!", "Successful Action!, Returning to Dashboard");
                    //goes back to the dashboard
                    returnToDashboard();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message, "Unsuccessfull Action!");
                }
            }
            //edit
            else
            {
                try
                {
                    new AgreementDataAccess().Update(getAgreementObject(true));
                    MessageBox.Show($"An Agreement with ID: {lbID.Text} just updated!", "Returning to Dashboard");
                    //goes back to the dashboard
                    returnToDashboard();
                }
                catch (Exception ex)
                {
                    errorMessage(ex.Message, "Unsuccessfull Action!");
                }
            }
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show($"Are you sure to exit ?", "Returning to Dashboard", MessageBoxButtons.OKCancel, MessageBoxIcon.Error) == DialogResult.OK)
            {
                returnToDashboard();
            }
        }

        private void returnToDashboard()
        {
            mainPnl.Controls.Clear();
            mainPnl.Controls.Add(new Agreements_Dashboard());
        }
    }
}
