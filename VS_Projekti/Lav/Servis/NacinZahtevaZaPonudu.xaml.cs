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

namespace Servis
{
    /// <summary>
    /// Interaction logic for NacinZahtevaZaPonudu.xaml
    /// </summary>
    public partial class NacinZahtevaZaPonudu : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;


        public NacinZahtevaZaPonudu()
        {
            InitializeComponent();

            textBoxTraziZa.Focus();
        }

        public void UStanje(App.Stanje stanje)
        {
            buttonDodaj.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
        }

        private void DajSve()
        {
            try
            {
                listViewNacinZahtevaZaPonudu.ItemsSource = dBProksi.DajSveNacinZahtevaZaPonudu();

                if (!listViewNacinZahtevaZaPonudu.Items.Count.Equals(0))
                {
                    listViewNacinZahtevaZaPonudu.SelectedIndex = 0;
                    UStanje(App.Stanje.Detaljno);
                }
                else
                {
                    UStanje(App.Stanje.Osnovno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelektujNacinZahtevaZaPonudu(int nacinZahtevaZaPonuduID)
        {
            ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = (ObservableCollection<DB.NacinZahtevaZaPonudu>)listViewNacinZahtevaZaPonudu.ItemsSource;
            bool _postoji = false;

            if (!listViewNacinZahtevaZaPonudu.Items.Count.Equals(0))
            {
                foreach (DB.NacinZahtevaZaPonudu item in _naciniZahtevaZaPonudu)
                {
                    if (item.NacinZahtevaZaPonuduID.Equals(nacinZahtevaZaPonuduID))
                    {
                        listViewNacinZahtevaZaPonudu.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewNacinZahtevaZaPonudu.SelectedIndex = 0;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                prvoOtvaranjeStrane = false;
                DajSve();
            }

            Window.GetWindow(this).Title = "Lav - NacinZahtevaZaPonudu";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            NacinZahtevaZaPonuduDetaljno _nacinZahtevaZaPonuduDetaljno = new NacinZahtevaZaPonuduDetaljno(this, false);
            //_nacinZahtevaZaPonuduDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _nacinZahtevaZaPonuduDetaljno.Owner = Window.GetWindow(this);
            _nacinZahtevaZaPonuduDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _nacinZahtevaZaPonuduDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = (ObservableCollection<DB.NacinZahtevaZaPonudu>)listViewNacinZahtevaZaPonudu.ItemsSource;
            DB.NacinZahtevaZaPonudu _nacinZahtevaZaPonudu = (DB.NacinZahtevaZaPonudu)listViewNacinZahtevaZaPonudu.SelectedItem;

            if (_naciniZahtevaZaPonudu != null && _nacinZahtevaZaPonudu != null)
            {
                NacinZahtevaZaPonuduDetaljno _nacinZahtevaZaPonuduDetaljno = new NacinZahtevaZaPonuduDetaljno(this, true);
                //_nacinZahtevaZaPonuduDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _nacinZahtevaZaPonuduDetaljno.Owner = Window.GetWindow(this);
                _nacinZahtevaZaPonuduDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _nacinZahtevaZaPonuduDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiNacinZahtevaZaPonudu((DB.NacinZahtevaZaPonudu)listViewNacinZahtevaZaPonudu.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {
                    DajSve();
                }
                else
                {
                    //ako ima filter ali nije odabrana kolona ne prijavljuj gresku, osvezi podatke
                    if (comboBoxNacinZahtrevaZaPonuduKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = (ObservableCollection<DB.NacinZahtevaZaPonudu>)listViewNacinZahtevaZaPonudu.ItemsSource;

                        listViewNacinZahtevaZaPonudu.ItemsSource = dBProksi.OsveziNacinZahtevaZaPonudu(_naciniZahtevaZaPonudu);

                        if (!listViewNacinZahtevaZaPonudu.Items.Count.Equals(0))
                        {
                            listViewNacinZahtevaZaPonudu.SelectedIndex = 0;
                            UStanje(App.Stanje.Detaljno);
                        }
                        else
                        {
                            UStanje(App.Stanje.Osnovno);
                        }
                        //inace pretrazi
                    }
                    else
                    {
                        listViewNacinZahtevaZaPonudu.ItemsSource = dBProksi.NadjiNacinZahtevaZaPonudu(((ComboBoxItem)comboBoxNacinZahtrevaZaPonuduKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewNacinZahtevaZaPonudu.Items.Count.Equals(0))
                        {
                            listViewNacinZahtevaZaPonudu.SelectedIndex = 0;
                            UStanje(App.Stanje.Detaljno);
                        }
                        else
                        {
                            UStanje(App.Stanje.Osnovno);
                        }
                    }
                }
            }
        }

        private void buttonOsvezi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.NacinZahtevaZaPonudu _trenutni = (DB.NacinZahtevaZaPonudu)listViewNacinZahtevaZaPonudu.SelectedItem;

                ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = (ObservableCollection<DB.NacinZahtevaZaPonudu>)listViewNacinZahtevaZaPonudu.ItemsSource;

                if (!_naciniZahtevaZaPonudu.Count.Equals(0))
                {
                    listViewNacinZahtevaZaPonudu.ItemsSource = dBProksi.OsveziNacinZahtevaZaPonudu(_naciniZahtevaZaPonudu);

                    if (_trenutni != null)
                    {
                        SelektujNacinZahtevaZaPonudu(_trenutni.NacinZahtevaZaPonuduID);
                    }
                    if (listViewNacinZahtevaZaPonudu.Items.Count.Equals(0))
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Detaljno);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.NacinZahtevaZaPonudu _trenutni = (DB.NacinZahtevaZaPonudu)listViewNacinZahtevaZaPonudu.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxNacinZahtrevaZaPonuduKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewNacinZahtevaZaPonudu.ItemsSource = dBProksi.NadjiNacinZahtevaZaPonudu(((ComboBoxItem)comboBoxNacinZahtrevaZaPonuduKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewNacinZahtevaZaPonudu.Items.Count.Equals(0))
                    {
                        listViewNacinZahtevaZaPonudu.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujNacinZahtevaZaPonudu(_trenutni.NacinZahtevaZaPonuduID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = (ObservableCollection<DB.NacinZahtevaZaPonudu>)listViewNacinZahtevaZaPonudu.ItemsSource;
            //DB.NacinZahtevaZaPonudu _nacinZahtevaZaPonudu = (DB.NacinZahtevaZaPonudu)listViewNacinZahtevaZaPonudu.SelectedItem;

            //if (_naciniZahtevaZaPonudu != null && _nacinZahtevaZaPonudu != null)
            //{
                NacinZahtevaZaPonuduDetaljno _nacinZahtevaZaPonuduDetaljno = new NacinZahtevaZaPonuduDetaljno(this, true);
                //_nacinZahtevaZaPonuduDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _nacinZahtevaZaPonuduDetaljno.Owner = Window.GetWindow(this);
                _nacinZahtevaZaPonuduDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _nacinZahtevaZaPonuduDetaljno.ShowDialog();
            //}
        }

    }
}
