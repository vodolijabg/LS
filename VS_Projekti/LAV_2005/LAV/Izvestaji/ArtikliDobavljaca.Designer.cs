namespace LAV.Izvestaji
{
    partial class ArtikliDobavljaca
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
            this.artikliDobavljacaCrystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // artikliDobavljacaCrystalReportViewer
            // 
            this.artikliDobavljacaCrystalReportViewer.ActiveViewIndex = -1;
            this.artikliDobavljacaCrystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.artikliDobavljacaCrystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.artikliDobavljacaCrystalReportViewer.Location = new System.Drawing.Point(0, 0);
            this.artikliDobavljacaCrystalReportViewer.Name = "artikliDobavljacaCrystalReportViewer";
            this.artikliDobavljacaCrystalReportViewer.SelectionFormula = "";
            this.artikliDobavljacaCrystalReportViewer.Size = new System.Drawing.Size(592, 566);
            this.artikliDobavljacaCrystalReportViewer.TabIndex = 0;
            this.artikliDobavljacaCrystalReportViewer.ViewTimeSelectionFormula = "";
            // 
            // ArtikliDobavljaca
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 566);
            this.Controls.Add(this.artikliDobavljacaCrystalReportViewer);
            this.Name = "ArtikliDobavljaca";
            this.Text = "Artikli dobavljača";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer artikliDobavljacaCrystalReportViewer;
    }
}