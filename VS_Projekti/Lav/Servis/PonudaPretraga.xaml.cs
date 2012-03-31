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
    /// Interaction logic for PonudaPretraga.xaml
    /// </summary>
    public partial class PonudaPretraga : Window
    {
        DB.DBProksi dBProksi;
        Servis.Ponuda ponuda;

        public PonudaPretraga(Servis.Ponuda ponuda)
        {
            InitializeComponent();

            this.dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.ponuda = ponuda;
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

        private void SelektujPonuda(int ponudaID)
        {
            ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)ponuda.listViewPonuda.ItemsSource;
            bool _postoji = false;

            if (!ponuda.listViewPonuda.Items.Count.Equals(0))
            {
                foreach (DB.Ponuda item in _ponude)
                {
                    if (item.PonudaID.Equals(ponudaID))
                    {
                        ponuda.listViewPonuda.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    ponuda.listViewPonuda.SelectedIndex = 0;
                }
            }
        }

        private void Nadji()
        {
            try
            {
                DB.Ponuda _trenutni = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;
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
                    ponuda.listViewPonuda.ItemsSource = dBProksi.NadjiPonude(
                                        ((ComboBoxItem)comboBoxVrstaPartnera.SelectedItem).Content.ToString() != "" ? ((ComboBoxItem)comboBoxVrstaPartnera.SelectedItem).Content.ToString() : "",
                                        textBoxID.Text.Trim() != "" ? textBoxID.Text.Trim() : "",
                                        (datePickerDatumOd.SelectedDate != null) ? datePickerDatumOd.SelectedDate : null,
                                        (datePickerDatumDo.SelectedDate != null) ? _dt : null,
                                        textBoxPartner.Text.Trim() != "" ? textBoxPartner.Text.Trim() : "",
                                        (bool)checkBoxOtvorene.IsChecked ? checkBoxOtvorene.IsChecked : null,
                                        ((DB.Radnik)comboBoxRadnik.SelectedItem).Nadimak != null ? (int?)((DB.Radnik)comboBoxRadnik.SelectedItem).RadnikID : null);

                    if (!ponuda.listViewPonuda.Items.Count.Equals(0))
                    {
                        ponuda.listViewPonuda.SelectedIndex = 0;
                        ponuda.UStanje(App.Stanje.Detaljno);

                        if (_trenutni != null)
                        {
                            ponuda.SelektujPonudu(_trenutni.PonudaID);
                        }
                    }
                    else
                    {
                        ponuda.UStanje(App.Stanje.Osnovno);
                    }
                }
                else
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi bar jedan uslov pretrage.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return ;
                }

                if (_trenutni != null)
                {
                    ponuda.SelektujPonudu(_trenutni.PonudaID);
                }

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

    }
}
