using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using Microsoft.Reporting.WinForms;

namespace Servis.Izvestaji
{

    public partial class Porudzbenica : Page
    {
        DB.DBProksi dBProksi;

        public Porudzbenica()
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.Porudzbenica> _lista = dBProksi.DajPorudzbenicu();

            reportViewerIzvestaj.LocalReport.ReportEmbeddedResource = "Servis.Izvestaji.PorudzbenicaStampa.rdlc";
            reportViewerIzvestaj.ProcessingMode = ProcessingMode.Local;

            ReportDataSource _reportDataSource = new ReportDataSource("DS_StampaPorudzbenica", _lista);

            reportViewerIzvestaj.LocalReport.DataSources.Clear();
            reportViewerIzvestaj.LocalReport.DataSources.Add(_reportDataSource);
            reportViewerIzvestaj.SetDisplayMode(DisplayMode.PrintLayout);
            reportViewerIzvestaj.ZoomMode = ZoomMode.PageWidth;
            reportViewerIzvestaj.RefreshReport();
        }
    }
}
