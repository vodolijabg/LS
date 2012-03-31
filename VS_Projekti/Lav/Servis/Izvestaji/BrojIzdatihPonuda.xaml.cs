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
using DB;
using Microsoft.Reporting.WinForms;

namespace Servis.Izvestaji
{
    /// <summary>
    /// Interaction logic for BrojIzdatihPonuda.xaml
    /// </summary>
    public partial class BrojIzdatihPonuda : Page
    {
        DB.DBProksi dBProksi;

        public BrojIzdatihPonuda()
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

        }

        private void buttonPrikazi_Click(object sender, RoutedEventArgs e)
        {
            if (datePickerDatumOd.SelectedDate != null || datePickerDatumDo.SelectedDate != null)
            {
                ObservableCollection<StampaBrojIzdatihPonuda> _lista = dBProksi.DajBrojIzdatihPonudaPoRadnicima((DateTime)datePickerDatumOd.SelectedDate, (DateTime)datePickerDatumDo.SelectedDate);

                List<StampaBrojIzdatihPonuda> _s = new List<StampaBrojIzdatihPonuda>();

                switch (((ComboBoxItem)comboBoxSortirajPo.SelectedItem).Content.ToString())
                {
                    case "Šifra":
                        _s = _lista.OrderBy(o => o.Sifra).ToList();
                        break;
                    case "Nadimak":
                        _s = _lista.OrderBy(o => o.Nadimak).ToList();
                        break;
                    case "Broj ponuda":
                        _s = _lista.OrderByDescending(o => o.BrojPonuda).ToList();
                        break;
                }


                reportViewerIzvestaj.LocalReport.ReportEmbeddedResource = "Servis.Izvestaji.BrojIzdatihPonudaStampa.rdlc";
                reportViewerIzvestaj.ProcessingMode = ProcessingMode.Local;

                ReportDataSource _reportDataSource = new ReportDataSource("DS_StampaBrojIzdatihPonuda", (bool)checkBoxVeceOdNule.IsChecked ? _s.Where(f => f.BrojPonuda > 0) : _s);

                reportViewerIzvestaj.LocalReport.DataSources.Clear();
                reportViewerIzvestaj.LocalReport.DataSources.Add(_reportDataSource);
                reportViewerIzvestaj.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewerIzvestaj.ZoomMode = ZoomMode.PageWidth;
                reportViewerIzvestaj.RefreshReport();
            }
            else
            {
                Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi vremenski opseg.");
                //_dialog.WindowStyle = WindowStyle.ToolWindow;
                _dialog.Owner = Window.GetWindow(this);
                _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _dialog.ShowDialog();
                return;
            }
        }
    }
}
