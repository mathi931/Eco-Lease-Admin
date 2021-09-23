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
using static EcoLease_Admin.UserControls.Methods.MessageBoxes;

namespace EcoLease_Admin.UserControls
{
    public partial class Requests : UserControl
    {
        List<Request> requests = new List<Request>();
        int selectedID = -1;
        public Requests()
        {
            InitializeComponent();
            requests = new RequestDataAccess().GetAll();
            createViews();
            fillRequests(requests);
        }

        private void createViews()
        {
            dgvRequests.Columns.Add("id", "ID");
            dgvRequests.Columns.Add("leaseFrom", "Lease From");
            dgvRequests.Columns.Add("leaseLast", "Lease Until");
            dgvRequests.Columns.Add("status", "Status");
            dgvRequests.Columns.Add("user", "User");
            dgvRequests.Columns.Add("vehicle", "Vehicle");

        }
        private void fillRequests(List<Request> r)
        {
            dgvRequests.Rows.Clear();

            foreach (var request in r)
            {
                dgvRequests.Rows.Add(new string[] { request.RId.ToString(), request.LeaseBegin.ToString(), request.LeaseLast.ToString(), request.Status.ToString(), request.User.ToString(), request.Vehicle.ToString() });
            }
        }
        private void dgvRequests_SelectionChanged(object sender, System.EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count > 0)
            {
                selectedID = int.Parse(dgvRequests.SelectedRows[0].Cells[0].Value.ToString());
            }
        }
        Request getSelected(int id)
        {
            return selectedID >= 0 ? requests.Where(r => r.RId == id).Select(x => x).FirstOrDefault() as Request : null;
        }

        private void btnConfirm_Click(object sender, System.EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count > 0)
            {
                Request selected = getSelected(selectedID);

                if (selected.Status == "Pending")
                {
                    if (dynamicQuestion("confirm the request from", selected.User.ToString(), "confirmation") == DialogResult.Yes)
                    {
                        try
                        {
                            selected.Status = "Confirmed";
                            new RequestDataAccess().Update(selected);
                            //send email here
                            if (infoMessage($"Email just sent to {selected.User}") == DialogResult.OK)
                            {
                                fillRequests(requests);

                            }
                        }
                        catch (Exception ex)
                        {
                            errorMessage(ex.Message, "Error!");
                        }
                    }
                }
                else
                {
                    errorMessage("You can't accept a request what is already processed!", "Error");
                }
            }
            else
            {
                errorMessage("You must select Request before accept it!", "Error");
            }
        }

        private void btnDecline_Click(object sender, System.EventArgs e)
        {
            if (dgvRequests.SelectedRows.Count > 0)
            {
                Request selected = getSelected(selectedID);

                if (selected.Status == "Pending")
                {
                    if (dynamicQuestion("decline the request from", selected.User.ToString(), "confirmation") == DialogResult.Yes)
                    {
                        try
                        {
                            selected.Status = "Declined";
                            new RequestDataAccess().Update(selected);
                            //send email here
                            if (infoMessage($"Email just sent to {selected.User}") == DialogResult.OK)
                            {
                                fillRequests(requests);

                            }
                        }
                        catch (Exception ex)
                        {
                            errorMessage(ex.Message, "Error!");
                        }
                    }
                }
                else
                {
                    errorMessage("You can't decline a request what is already processed!", "Error");
                }

            }
            else
            {
                errorMessage("You must select Request before decline it!", "Error");
            }
        }
    }
}
