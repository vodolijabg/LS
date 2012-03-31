using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace LAV.Izvestaji
{
    public partial class Brojac : Form
    {
        public Brojac()
        {
            InitializeComponent();
        }

        public Brojac(DataTable brojac):this()
        {
            BrojacCrystalReport brojacCrystalReport = new BrojacCrystalReport();
            brojacCrystalReport.SetDataSource(brojac);

            brojacCrystalReportViewer.ReportSource = brojacCrystalReport;

            brojacCrystalReportViewer.ShowZoomButton = false;
            brojacCrystalReportViewer.ShowGroupTreeButton = false;
            brojacCrystalReportViewer.ShowRefreshButton = false;
            brojacCrystalReportViewer.DisplayGroupTree = false;
            //kada se klikne na subreport nece ga otvoriti u zasebnom prozoru
            brojacCrystalReportViewer.EnableDrillDown = false;
        }
    }
}