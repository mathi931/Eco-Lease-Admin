
namespace EcoLease_Admin.UserControls
{
    partial class Agreements
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
            this.btnAgreements = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // container
            // 
            this.container.Dock = System.Windows.Forms.DockStyle.Right;
            this.container.Location = new System.Drawing.Point(143, 0);
            this.container.Name = "container";
            this.container.Size = new System.Drawing.Size(922, 760);
            this.container.TabIndex = 16;
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(1, 379);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(134, 47);
            this.btnRemove.TabIndex = 15;
            this.btnRemove.Text = "Remove Agreement";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnEdit
            // 
            this.btnEdit.Location = new System.Drawing.Point(1, 308);
            this.btnEdit.Name = "btnEdit";
            this.btnEdit.Size = new System.Drawing.Size(134, 47);
            this.btnEdit.TabIndex = 14;
            this.btnEdit.Text = "Edit Agreement";
            this.btnEdit.UseVisualStyleBackColor = true;
            this.btnEdit.Click += new System.EventHandler(this.btnEdit_Click);
            // 
            // btnNew
            // 
            this.btnNew.Location = new System.Drawing.Point(1, 239);
            this.btnNew.Name = "btnNew";
            this.btnNew.Size = new System.Drawing.Size(134, 47);
            this.btnNew.TabIndex = 13;
            this.btnNew.Text = "New Agreement";
            this.btnNew.UseVisualStyleBackColor = true;
            this.btnNew.Click += new System.EventHandler(this.btnNew_Click);
            // 
            // btnAgreements
            // 
            this.btnAgreements.Location = new System.Drawing.Point(1, 172);
            this.btnAgreements.Name = "btnAgreements";
            this.btnAgreements.Size = new System.Drawing.Size(134, 47);
            this.btnAgreements.TabIndex = 12;
            this.btnAgreements.Text = "Agreements";
            this.btnAgreements.UseVisualStyleBackColor = true;
            this.btnAgreements.Click += new System.EventHandler(this.btnAgreements_Click);
            // 
            // Agreements
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.container);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnEdit);
            this.Controls.Add(this.btnNew);
            this.Controls.Add(this.btnAgreements);
            this.Name = "Agreements";
            this.Size = new System.Drawing.Size(1065, 760);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel container;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnEdit;
        private System.Windows.Forms.Button btnNew;
        private System.Windows.Forms.Button btnAgreements;
    }
}
