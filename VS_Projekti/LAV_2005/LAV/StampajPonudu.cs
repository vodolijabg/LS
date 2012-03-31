using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV
{
    public partial class StampajPonudu : Form
    {
        public StampajPonudu()
        {
            InitializeComponent();
           
        }
        public StampajPonudu(DataSet ponuda):this()
        {
            StampajPonuduCrystalReport stampajPonuduCrystalReport = new StampajPonuduCrystalReport();
            stampajPonuduCrystalReport.SetDataSource(ponuda);

            //stampajPonuduCrystalReport.Subreports["PodaciOkorisnikuCrystalReport.rpt"].SetDataSource(korisnikPrograma);

            stampajPonuduCrystalReportViewer.ReportSource = stampajPonuduCrystalReport;

            stampajPonuduCrystalReportViewer.ShowZoomButton = false;
            stampajPonuduCrystalReportViewer.ShowGroupTreeButton = false;
            stampajPonuduCrystalReportViewer.ShowRefreshButton = false;
            stampajPonuduCrystalReportViewer.DisplayGroupTree = false;
            //kada se klikne na subreport nece ga otvoriti u zasebnom prozoru
            stampajPonuduCrystalReportViewer.EnableDrillDown = false;
        }

    }
}