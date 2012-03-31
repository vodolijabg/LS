namespace LAV.Izvestaji
{
    partial class Brojac
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
            this.brojacCrystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // brojacCrystalReportViewer
            // 
            this.brojacCrystalReportViewer.ActiveViewIndex = -1;
            this.brojacCrystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.brojacCrystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.brojacCrystalReportViewer.Location = new System.Drawing.Point(0, 0);
            this.brojacCrystalReportViewer.Name = "brojacCrystalReportViewer";
            this.brojacCrystalReportViewer.SelectionFormula = "";
            this.brojacCrystalReportViewer.Size = new System.Drawing.Size(592, 566);
            this.brojacCrystalReportViewer.TabIndex = 0;
            this.brojacCrystalReportViewer.ViewTimeSelectionFormula = "";
            // 
            // Brojac
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 566);
            this.Controls.Add(this.brojacCrystalReportViewer);
            this.Name = "Brojac";
            this.ShowInTaskbar = false;
            this.Text = "Brojac";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer brojacCrystalReportViewer;
    }
}