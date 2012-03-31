namespace LAV
{
    partial class StampajPonudu
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
            this.stampajPonuduCrystalReportViewer = new CrystalDecisions.Windows.Forms.CrystalReportViewer();
            this.SuspendLayout();
            // 
            // stampajPonuduCrystalReportViewer
            // 
            this.stampajPonuduCrystalReportViewer.ActiveViewIndex = -1;
            this.stampajPonuduCrystalReportViewer.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.stampajPonuduCrystalReportViewer.Dock = System.Windows.Forms.DockStyle.Fill;
            this.stampajPonuduCrystalReportViewer.Location = new System.Drawing.Point(0, 0);
            this.stampajPonuduCrystalReportViewer.Name = "stampajPonuduCrystalReportViewer";
            this.stampajPonuduCrystalReportViewer.SelectionFormula = "";
            this.stampajPonuduCrystalReportViewer.Size = new System.Drawing.Size(592, 566);
            this.stampajPonuduCrystalReportViewer.TabIndex = 0;
            this.stampajPonuduCrystalReportViewer.ViewTimeSelectionFormula = "";
            // 
            // StampajPonudu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(592, 566);
            this.Controls.Add(this.stampajPonuduCrystalReportViewer);
            this.Name = "StampajPonudu";
            this.Text = "Štampaj ponudu";
            this.ResumeLayout(false);

        }

        #endregion

        private CrystalDecisions.Windows.Forms.CrystalReportViewer stampajPonuduCrystalReportViewer;
    }
}