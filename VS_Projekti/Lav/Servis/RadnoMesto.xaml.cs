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

using System.Data.Linq;
using System.Collections.ObjectModel;

namespace Servis
{
    /// <summary>
    /// Interaction logic for RadnoMesto.xaml
    /// </summary>
    public partial class RadnoMesto : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public RadnoMesto()
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
                listViewRadnoMesto.ItemsSource = dBProksi.DajSvaRadnaMesta();

                if (!listViewRadnoMesto.Items.Count.Equals(0))
                {
                    listViewRadnoMesto.SelectedIndex = 0;
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

        private void SelektujRadnoMesto(int radnoMestoID)
        {
            ObservableCollection<DB.RadnoMesto> _radnaMesta = (ObservableCollection<DB.RadnoMesto>)listViewRadnoMesto.ItemsSource;
            bool _postoji = false;

            if (!listViewRadnoMesto.Items.Count.Equals(0))
            {
                foreach (DB.RadnoMesto item in _radnaMesta)
                {
                    if (item.RadnoMestoID.Equals(radnoMestoID))
                    {
                        listViewRadnoMesto.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewRadnoMesto.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - RadnoMesto";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            RadnoMestoDetaljno _radnoMestoDetaljno = new RadnoMestoDetaljno(this, false);
            //_radnoMestoDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _radnoMestoDetaljno.Owner = Window.GetWindow(this);
            _radnoMestoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _radnoMestoDetaljno.ShowDialog();

        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.RadnoMesto> _radnaMesta = (ObservableCollection<DB.RadnoMesto>)listViewRadnoMesto.ItemsSource;
            DB.RadnoMesto _mesto = (DB.RadnoMesto)listViewRadnoMesto.SelectedItem;

            if (_radnaMesta != null && _mesto != null)
            {
                RadnoMestoDetaljno _radnoMestoDetaljno = new RadnoMestoDetaljno(this, true);
                //_radnoMestoDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _radnoMestoDetaljno.Owner = Window.GetWindow(this);
                _radnoMestoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _radnoMestoDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiRadnoMesto((DB.RadnoMesto)listViewRadnoMesto.SelectedItem);
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
                    if (comboBoxRadnoMestoKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.RadnoMesto> _radnaMesta = (ObservableCollection<DB.RadnoMesto>)listViewRadnoMesto.ItemsSource;

                        listViewRadnoMesto.ItemsSource = dBProksi.OsveziRadnaMesta(_radnaMesta);

                        if (!listViewRadnoMesto.Items.Count.Equals(0))
                        {
                            listViewRadnoMesto.SelectedIndex = 0;
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
                        listViewRadnoMesto.ItemsSource = dBProksi.NadjiRadnaMesta(((ComboBoxItem)comboBoxRadnoMestoKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewRadnoMesto.Items.Count.Equals(0))
                        {
                            listViewRadnoMesto.SelectedIndex = 0;
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
                DB.RadnoMesto _trenutni = (DB.RadnoMesto)listViewRadnoMesto.SelectedItem;

                ObservableCollection<DB.RadnoMesto> _radnaMesta = (ObservableCollection<DB.RadnoMesto>)listViewRadnoMesto.ItemsSource;

                if (!_radnaMesta.Count.Equals(0))
                {
                    listViewRadnoMesto.ItemsSource = dBProksi.OsveziRadnaMesta(_radnaMesta);

                    if (_trenutni != null)
                    {
                        SelektujRadnoMesto(_trenutni.RadnoMestoID);
                    }

                    if (listViewRadnoMesto.Items.Count.Equals(0))
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
                DB.RadnoMesto _trenutni = (DB.RadnoMesto)listViewRadnoMesto.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxRadnoMestoKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewRadnoMesto.ItemsSource = dBProksi.NadjiRadnaMesta(((ComboBoxItem)comboBoxRadnoMestoKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewRadnoMesto.Items.Count.Equals(0))
                    {
                        listViewRadnoMesto.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujRadnoMesto(_trenutni.RadnoMestoID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.RadnoMesto> _radnaMesta = (ObservableCollection<DB.RadnoMesto>)listViewRadnoMesto.ItemsSource;
            //DB.RadnoMesto _radnoMesto = (DB.RadnoMesto)listViewRadnoMesto.SelectedItem;

            //if (_radnaMesta != null && _radnoMesto != null)
            //{
                RadnoMestoDetaljno _radnoMestoDetaljno = new RadnoMestoDetaljno(this, true);
                //_radnoMestoDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _radnoMestoDetaljno.Owner = Window.GetWindow(this);
                _radnoMestoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _radnoMestoDetaljno.ShowDialog();
            //}
        }
    }
}
