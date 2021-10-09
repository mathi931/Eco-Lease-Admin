
namespace EcoLease_Admin.UserControls
{
    partial class Reservations
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

        #region Component Designer generated code

        /// <summary> 
        /// Required method for Designer support - do not modify 
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.container = new System.Windows.Forms.Panel();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnNew = new System.Windows.Forms.Button();
            this.btnReservations = new System.Windows.Forms.Button();
            this.button1 = new System.Windows.Forms.Button();
            this.btnDecline = new System.Windows.Forms.Button();
            this.btnAccept = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.Location = new System.Drawing.Point(168, 0);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(897, 671);
            this.container.TabIndex = 16;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(1, 379);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(161, 47);
            this.btnRemove.TabIndex = 15;
            this.btnRemove.Text = "Remove Reservation";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(1, 308);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(161, 47);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "Edit Reservation";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(1, 239);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(161, 47);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "New Reservation";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnReservations
            // 
            this.btnReservations.Location = new System.Drawing.Point(1, 172);
            this.btnReservations.Name = "btnReservations";
            this.btnReservations.Size = new System.Drawing.Size(161, 47);
            this.btnReservations.TabIndex = 12;
            this.btnReservations.Text = "Reservations";
            this.btnReservations.UseVisualStyleBackColor = true;
            this.btnReservations.Click += new System.EventHandler(this.btnAgreements_Click);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(27, 473);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(95, 37);
            this.button1.TabIndex = 17;
            this.button1.Text = "Get Contract";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnDecline
            // 
            this.btnDecline.Location = new System.Drawing.Point(900, 696);
            this.btnDecline.Name = "btnDecline";
            this.btnDecline.Size = new System.Drawing.Size(162, 47);
            this.btnDecline.TabIndex = 19;
            this.btnDecline.Text = "Decline Reservation";
            this.btnDecline.UseVisualStyleBackColor = true;
            this.btnDecline.Click += new System.EventHandler(this.btnDecline_Click);
            // 
            // btnAccept
            // 
            this.btnAccept.Location = new System.Drawing.Point(704, 696);
            this.btnAccept.Name = "btnAccept";
            this.btnAccept.Size = new System.Drawing.Size(174, 47);
            this.btnAccept.TabIndex = 18;
            this.btnAccept.Text = "Accept Reservation";
            this.btnAccept.UseVisualStyleBackColor = true;
            this.btnAccept.Click += new System.EventHandler(this.btnAccept_Click);
            // 
            // Reservations
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.btnDecline);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.btnAccept);
            this.Controls.Add(this.container);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnReservations);
            this.Name = "Reservations";
            this.Size = new System.Drawing.Size(1065, 760);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnReservations;
        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Button btnDecline;
        private System.Windows.Forms.Button btnAccept;
    }
}
