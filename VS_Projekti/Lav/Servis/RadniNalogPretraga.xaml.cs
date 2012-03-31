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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Servis
{
    /// <summary>
    /// Interaction logic for RadniNalogPretraga.xaml
    /// </summary>
    public partial class RadniNalogPretraga : Window
    {
        DB.DBProksi dBProksi;
        Servis.RadniNalog radniNalog;

        public RadniNalogPretraga(Servis.RadniNalog radniNalog)
        {
            InitializeComponent();

            this.dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.radniNalog = radniNalog;
        }
        private void SelektujRadniNalog(int radniNalogID)
        {
            ObservableCollection<DB.RadniNalog> _radniNalogLista = (ObservableCollection<DB.RadniNalog>)radniNalog.listViewRadniNalog.ItemsSource;
            bool _postoji = false;

            if (!radniNalog.listViewRadniNalog.Items.Count.Equals(0))
            {
                foreach (DB.RadniNalog item in _radniNalogLista)
                {
                    if (item.RadniNalogID.Equals(radniNalogID))
                    {
                        radniNalog.listViewRadniNalog.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    radniNalog.listViewRadniNalog.SelectedIndex = 0;
                }
            }
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                ObservableCollection<DB.Radnik> _radnici = new ObservableCollection<DB.Radnik>(dBProksi.DajSveRadnike().ToList());

                if (!_radnici.Count.Equals(0))
                {
                    _radnici.Insert(0, new DB.Radnik());
                }

                comboBoxRadnik.ItemsSource = _radnici.OrderBy(f => f.Nadimak);
                comboBoxRadnik.SelectedIndex = 0;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            Nadji();
        }

        private void buttonNadjiIZatvori_Click(object sender, RoutedEventArgs e)
        {
            Nadji();

            this.Close();
        }

        private void Nadji()
        {
            try
            {
                DB.RadniNalog _trenutni = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;
                //mora biti zadat bar jedan uslov
                if (
                    ((ComboBoxItem)comboBoxVrstaPartnera.SelectedItem).Content.ToString() != "" ||
                    textBoxID.Text.Trim() != "" ||
                    datePickerDatumOd.SelectedDate != null ||
                    datePickerDatumDo.SelectedDate != null ||
                    textBoxPartner.Text.Trim() != "" ||
                    (bool)checkBoxOtvorene.IsChecked ||
                    ((DB.Radnik)comboBoxRadnik.SelectedItem).Nadimak != null
                    )
                {
                    DateTime _datumDo;
                    DateTime? _dt = null;
                    if ((datePickerDatumDo.SelectedDate != null))
                    {
                        _datumDo = (DateTime)datePickerDatumDo.SelectedDate;
                        _dt = new DateTime(_datumDo.Year, _datumDo.Month, _datumDo.Day, DateTime.Now.Hour, DateTime.Now.Minute, DateTime.Now.Second);
                    }
                    radniNalog.listViewRadniNalog.ItemsSource = dBProksi.NadjiRadniNalog(
                                        ((ComboBoxItem)comboBoxVrstaPartnera.SelectedItem).Content.ToString() != "" ? ((ComboBoxItem)comboBoxVrstaPartnera.SelectedItem).Content.ToString() : "",
                                        textBoxID.Text.Trim() != "" ? textBoxID.Text.Trim() : "",
                                        (datePickerDatumOd.SelectedDate != null) ? datePickerDatumOd.SelectedDate : null,
                                        (datePickerDatumDo.SelectedDate != null) ? _dt : null,
                                        textBoxPartner.Text.Trim() != "" ? textBoxPartner.Text.Trim() : "",
                                        (bool)checkBoxOtvorene.IsChecked ? checkBoxOtvorene.IsChecked : null,
                                        ((DB.Radnik)comboBoxRadnik.SelectedItem).Nadimak != null ? (int?)((DB.Radnik)comboBoxRadnik.SelectedItem).RadnikID : null,
                                        Convert.ToInt32(Konfiguracija.RadniNalogStatusIDZavrsen));

                    if (!radniNalog.listViewRadniNalog.Items.Count.Equals(0))
                    {
                        radniNalog.listViewRadniNalog.SelectedIndex = 0;
                        radniNalog.UStanje(App.Stanje.Detaljno);

                        if (_trenutni != null)
                        {
                            radniNalog.SelektujRadniNalog(_trenutni.RadniNalogID);
                        }
                    }
                    else
                    {
                        radniNalog.UStanje(App.Stanje.Osnovno);
                    }
                }
                else
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi bar jedan uslov pretrage.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return;
                }

                if (_trenutni != null)
                {
                    radniNalog.SelektujRadniNalog(_trenutni.RadniNalogID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
