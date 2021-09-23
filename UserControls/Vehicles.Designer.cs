
namespace EcoLease_Admin.UserControls
{
    partial class Vehicles
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
            this.btnFleet = new System.Windows.Forms.Button();
            this.btnAdd = new System.Windows.Forms.Button();
            this.btnEdit = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.container = new System.Windows.Forms.Panel();
            this.SuspendLayout();
            // 
            // btnFleet
            // 
            this.btnFleet.Location = new System.Drawing.Point(3, 172);
            this.btnFleet.Name = "btnFleet";
            this.btnFleet.Size = new System.Drawing.Size(134, 47);
            this.btnFleet.TabIndex = 0;
            this.btnFleet.Text = "Fleet";
            this.btnFleet.UseVisualStyleBackColor = true;
            this.btnFleet.Click += new System.EventHandler(this.btnFleet_Click);
            // 
            // btnAdd
            // 
            this.btnAdd.Location = new System.Drawing.Point(3, 239);
            this.btnAdd.Name = "btnAdd";
            this.btnAdd.Size = new System.Drawing.Size(134, 47);
            this.btnAdd.TabIndex = 1;
            this.btnAdd.Text = "Add vehicle";
            this.btnAdd.UseVisualStyleBackColor = true;
            this.btnAdd.Click += new System.EventHandler(this.btnAdd_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(3, 308);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(134, 47);
            this.btnEdit.TabIndex = 2;
            this.btnEdit.Text = "Edit vehicle";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(3, 379);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(134, 47);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Remove vehicle";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // container
            // 
            this.container.Dock = System.Windows.Forms.DockStyle.Right;
            this.container.Location = new System.Drawing.Point(143, 0);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(922, 760);
            this.container.TabIndex = 11;
            // 
            // Vehicles
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.container);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnAdd);
            this.Controls.Add(this.btnFleet);
            this.DoubleBuffered = true;
            this.Name = "Vehicles";
            this.Size = new System.Drawing.Size(1065, 760);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button btnFleet;
        private System.Windows.Forms.Button btnAdd;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Panel container;
    }
}
