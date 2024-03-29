using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV.Izvestaji
{
    public partial class ArtikliDobavljaca : Form
    {
        public ArtikliDobavljaca()
        {
            InitializeComponent();
        }

        public ArtikliDobavljaca(DataTable artikliDobavljaca, string nazivDobavljaca ): this ()
        {
            ArtikliDobavljacaCrystalReport artikliDobavljacaCrystalReport = new ArtikliDobavljacaCrystalReport();
            artikliDobavljacaCrystalReport.SetDataSource(artikliDobavljaca);

            artikliDobavljacaCrystalReportViewer.ReportSource = artikliDobavljacaCrystalReport;


            artikliDobavljacaCrystalReportViewer.ShowZoomButton = false;
            artikliDobavljacaCrystalReportViewer.ShowGroupTreeButton = false;
            artikliDobavljacaCrystalReportViewer.ShowRefreshButton = false;
            artikliDobavljacaCrystalReportViewer.DisplayGroupTree = false;
            //kada se klikne na subreport nece ga otvoriti u zasebnom prozoru
            artikliDobavljacaCrystalReportViewer.EnableDrillDown = false;

            this.Text = "Spisak artikala za dobavljača - " + nazivDobavljaca;

        }
    }
}