
namespace EcoLease_Admin.UserControls
{
    partial class Agreements_Dashboard
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
            this.dgvAgreements = new ADGV.AdvancedDataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgreements)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvAgreements
            // 
            this.dgvAgreements.AllowUserToAddRows = false;
            this.dgvAgreements.AllowUserToDeleteRows = false;
            this.dgvAgreements.AllowUserToResizeRows = false;
            this.dgvAgreements.AutoGenerateContextFilters = true;
            this.dgvAgreements.AutoSizeColumnsMode = System.Windows.Forms.DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvAgreements.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.dgvAgreements.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvAgreements.DateWithTime = false;
            this.dgvAgreements.Dock = System.Windows.Forms.DockStyle.Fill;
            this.dgvAgreements.EditMode = System.Windows.Forms.DataGridViewEditMode.EditProgrammatically;
            this.dgvAgreements.Location = new System.Drawing.Point(0, 0);
            this.dgvAgreements.MultiSelect = false;
            this.dgvAgreements.Name = "dgvAgreements";
            this.dgvAgreements.ReadOnly = true;
            this.dgvAgreements.RowHeadersVisible = false;
            this.dgvAgreements.SelectionMode = System.Windows.Forms.DataGridViewSelectionMode.FullRowSelect;
            this.dgvAgreements.Size = new System.Drawing.Size(919, 754);
            this.dgvAgreements.TabIndex = 1;
            this.dgvAgreements.TimeFilter = false;
            this.dgvAgreements.SortStringChanged += new System.EventHandler(this.dgvAgreements_SortStringChanged);
            this.dgvAgreements.FilterStringChanged += new System.EventHandler(this.dgvAgreements_FilterStringChanged);
            this.dgvAgreements.SelectionChanged += new System.EventHandler(this.dgvAgreements_SelectionChanged);
            // 
            // Agreements_Dashboard
            // 
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.None;
            this.Controls.Add(this.dgvAgreements);
            this.Name = "Agreements_Dashboard";
            this.Size = new System.Drawing.Size(919, 754);
            ((System.ComponentModel.ISupportInitialize)(this.dgvAgreements)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private ADGV.AdvancedDataGridView dgvAgreements;
    }
}
