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
    /// Interaction logic for Pozicija.xaml
    /// </summary>
    public partial class Pozicija : Page
    {
                //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public Pozicija()
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
                listViewPozicija.ItemsSource = dBProksi.DajSvePozicija();

                if (!listViewPozicija.Items.Count.Equals(0))
                {
                    listViewPozicija.SelectedIndex = 0;
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

        private void SelektujPozicija(int pozicijaID)
        {
            ObservableCollection<DB.Pozicija> _pozicija = (ObservableCollection<DB.Pozicija>)listViewPozicija.ItemsSource;
            bool _postoji = false;

            if (!listViewPozicija.Items.Count.Equals(0))
            {
                foreach (DB.Pozicija item in _pozicija)
                {
                    if (item.PozicijaID.Equals(pozicijaID))
                    {
                        listViewPozicija.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewPozicija.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - Pozicija";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            PozicijaDetaljno _pozicijaDetaljno = new PozicijaDetaljno(this, false);
            //_pozicijaDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _pozicijaDetaljno.Owner = Window.GetWindow(this);
            _pozicijaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _pozicijaDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.Pozicija> _pozicijaLista = (ObservableCollection<DB.Pozicija>)listViewPozicija.ItemsSource;
            DB.Pozicija _pozicija = (DB.Pozicija)listViewPozicija.SelectedItem;

            if (_pozicijaLista != null && _pozicija != null)
            {
                PozicijaDetaljno _pozicijaDetaljno = new PozicijaDetaljno(this, true);
                //_pozicijaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _pozicijaDetaljno.Owner = Window.GetWindow(this);
                _pozicijaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _pozicijaDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiPozicija((DB.Pozicija)listViewPozicija.SelectedItem);
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
                    if (comboBoxPozicijaKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.Pozicija> _pozicija = (ObservableCollection<DB.Pozicija>)listViewPozicija.ItemsSource;

                        listViewPozicija.ItemsSource = dBProksi.OsveziPozicija(_pozicija);

                        if (!listViewPozicija.Items.Count.Equals(0))
                        {
                            listViewPozicija.SelectedIndex = 0;
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
                        listViewPozicija.ItemsSource = dBProksi.NadjiPozicija(((ComboBoxItem)comboBoxPozicijaKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewPozicija.Items.Count.Equals(0))
                        {
                            listViewPozicija.SelectedIndex = 0;
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
                DB.Pozicija _trenutni = (DB.Pozicija)listViewPozicija.SelectedItem;

                ObservableCollection<DB.Pozicija> _pozicijaLista = (ObservableCollection<DB.Pozicija>)listViewPozicija.ItemsSource;

                if (!_pozicijaLista.Count.Equals(0))
                {
                    listViewPozicija.ItemsSource = dBProksi.OsveziPozicija(_pozicijaLista);

                    if (_trenutni != null)
                    {
                        SelektujPozicija(_trenutni.PozicijaID);
                    }
                    if (listViewPozicija.Items.Count.Equals(0))
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
                DB.Pozicija _trenutni = (DB.Pozicija)listViewPozicija.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxPozicijaKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewPozicija.ItemsSource = dBProksi.NadjiPozicija(((ComboBoxItem)comboBoxPozicijaKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewPozicija.Items.Count.Equals(0))
                    {
                        listViewPozicija.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujPozicija(_trenutni.PozicijaID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.Pozicija> _pozicijaLista = (ObservableCollection<DB.Pozicija>)listViewPozicija.ItemsSource;
            //DB.Pozicija _pozicija = (DB.Pozicija)listViewPozicija.SelectedItem;

            //if (_pozicijaLista != null && _pozicija != null)
            //{
                PozicijaDetaljno _pozicijaDetaljno = new PozicijaDetaljno(this, true);
                _pozicijaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _pozicijaDetaljno.Owner = Window.GetWindow(this);
                _pozicijaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _pozicijaDetaljno.ShowDialog();
            //}
        }
    }
}
