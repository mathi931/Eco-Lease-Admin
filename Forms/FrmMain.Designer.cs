
namespace EcoLease_Admin
{
    partial class FrmMain
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.btnDashboard = new System.Windows.Forms.Button();
            this.btnVehicles = new System.Windows.Forms.Button();
            this.btnAgreements = new System.Windows.Forms.Button();
            this.btnRequests = new System.Windows.Forms.Button();
            this.container = new System.Windows.Forms.Panel();
            this.output = new System.Windows.Forms.Label();
            this.container.SuspendLayout();
            this.SuspendLayout();
            // 
            // btnDashboard
            // 
            this.btnDashboard.Location = new System.Drawing.Point(12, 146);
            this.btnDashboard.Name = "btnDashboard";
            this.btnDashboard.Size = new System.Drawing.Size(175, 50);
            this.btnDashboard.TabIndex = 0;
            this.btnDashboard.Text = "Dashboard";
            this.btnDashboard.UseVisualStyleBackColor = true;
            // 
            // btnVehicles
            // 
            this.btnVehicles.Location = new System.Drawing.Point(12, 232);
            this.btnVehicles.Name = "btnVehicles";
            this.btnVehicles.Size = new System.Drawing.Size(175, 50);
            this.btnVehicles.TabIndex = 1;
            this.btnVehicles.Text = "Vehicles";
            this.btnVehicles.UseVisualStyleBackColor = true;
            // 
            // btnAgreements
            // 
            this.btnAgreements.Location = new System.Drawing.Point(12, 317);
            this.btnAgreements.Name = "btnAgreements";
            this.btnAgreements.Size = new System.Drawing.Size(175, 50);
            this.btnAgreements.TabIndex = 2;
            this.btnAgreements.Text = "Agreements";
            this.btnAgreements.UseVisualStyleBackColor = true;
            // 
            // btnRequests
            // 
            this.btnRequests.Location = new System.Drawing.Point(12, 406);
            this.btnRequests.Name = "btnRequests";
            this.btnRequests.Size = new System.Drawing.Size(175, 50);
            this.btnRequests.TabIndex = 3;
            this.btnRequests.Text = "Requests";
            this.btnRequests.UseVisualStyleBackColor = true;
            // 
            // container
            // 
            this.container.Controls.Add(this.output);
            this.container.Location = new System.Drawing.Point(202, 1);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(1065, 760);
            this.container.TabIndex = 4;
            // 
            // output
            // 
            this.output.AutoSize = true;
            this.output.Location = new System.Drawing.Point(114, 291);
            this.output.Name = "output";
            this.output.Size = new System.Drawing.Size(0, 20);
            this.output.TabIndex = 1;
            // 
            // FrmMain
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.ClientSize = new System.Drawing.Size(1264, 761);
            this.Controls.Add(this.container);
            this.Controls.Add(this.btnRequests);
            this.Controls.Add(this.btnAgreements);
            this.Controls.Add(this.btnVehicles);
            this.Controls.Add(this.btnDashboard);
            this.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.MinimumSize = new System.Drawing.Size(800, 600);
            this.Name = "FrmMain";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Eco Lease";
            this.Load += new System.EventHandler(this.FrmMain_Load);
            this.container.ResumeLayout(false);
            this.container.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnDashboard;
        private System.Windows.Forms.Button btnVehicles;
        private System.Windows.Forms.Button btnAgreements;
        private System.Windows.Forms.Button btnRequests;
        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.Label output;
    }
}