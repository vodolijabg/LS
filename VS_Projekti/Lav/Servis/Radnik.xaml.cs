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
    /// Interaction logic for Radnik.xaml
    /// </summary>
    public partial class Radnik : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public Radnik()
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
                listViewRadnik.ItemsSource = dBProksi.DajSveRadnike();

                if (!listViewRadnik.Items.Count.Equals(0))
                {
                    listViewRadnik.SelectedIndex = 0;
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

        private void SelektujRadnika(int radnikID)
        {
            ObservableCollection<DB.Radnik> _radnici = (ObservableCollection<DB.Radnik>)listViewRadnik.ItemsSource;
            bool _postoji = false;

            if (!listViewRadnik.Items.Count.Equals(0))
            {
                foreach (DB.Radnik item in _radnici)
                {
                    if (item.RadnikID.Equals(radnikID))
                    {
                        listViewRadnik.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewRadnik.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - Radnik";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            RadnikDetaljno _radnikDetaljno = new RadnikDetaljno(this, false);
            //_radnikDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _radnikDetaljno.Owner = Window.GetWindow(this);
            _radnikDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _radnikDetaljno.ShowDialog();

        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.Radnik> _radnici = (ObservableCollection<DB.Radnik>)listViewRadnik.ItemsSource;
            DB.Radnik _radnik = (DB.Radnik)listViewRadnik.SelectedItem;

            if (_radnici != null && _radnik != null)
            {
                RadnikDetaljno _radnikDetaljno = new RadnikDetaljno(this, true);
                //_radnikDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _radnikDetaljno.Owner = Window.GetWindow(this);
                _radnikDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _radnikDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiRadnika((DB.Radnik)listViewRadnik.SelectedItem);
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
                    if (comboBoxRadnikKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.Radnik> _radnici = (ObservableCollection<DB.Radnik>)listViewRadnik.ItemsSource;

                        listViewRadnik.ItemsSource = dBProksi.OsveziRadnike(_radnici);

                        if (!listViewRadnik.Items.Count.Equals(0))
                        {
                            listViewRadnik.SelectedIndex = 0;
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
                        listViewRadnik.ItemsSource = dBProksi.NadjiRadnike(((ComboBoxItem)comboBoxRadnikKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewRadnik.Items.Count.Equals(0))
                        {
                            listViewRadnik.SelectedIndex = 0;
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
                DB.Radnik _trenutni = (DB.Radnik)listViewRadnik.SelectedItem;

                ObservableCollection<DB.Radnik> _radnici = (ObservableCollection<DB.Radnik>)listViewRadnik.ItemsSource;

                if (!_radnici.Count.Equals(0))
                {
                    listViewRadnik.ItemsSource = dBProksi.OsveziRadnike(_radnici);

                    if (_trenutni != null)
                    {
                        SelektujRadnika(_trenutni.RadnikID);
                    }

                    if (listViewRadnik.Items.Count.Equals(0))
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
                DB.Radnik _trenutni = (DB.Radnik)listViewRadnik.SelectedItem;
                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {
                    DajSve();
                }
                else if (comboBoxRadnikKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewRadnik.ItemsSource = dBProksi.NadjiRadnike(((ComboBoxItem)comboBoxRadnikKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewRadnik.Items.Count.Equals(0))
                    {
                        listViewRadnik.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujRadnika(_trenutni.RadnikID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.Radnik> _radnici = (ObservableCollection<DB.Radnik>)listViewRadnik.ItemsSource;
            //DB.Radnik _radnik = (DB.Radnik)listViewRadnik.SelectedItem;

            //if (_radnici != null && _radnik != null)
            //{
                RadnikDetaljno _radnikDetaljno = new RadnikDetaljno(this, true);
                //_radnikDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _radnikDetaljno.Owner = Window.GetWindow(this);
                _radnikDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _radnikDetaljno.ShowDialog();
            //}
        }

        
    }
}
