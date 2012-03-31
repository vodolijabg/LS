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
    /// Interaction logic for RadniNalogStatus.xaml
    /// </summary>
    public partial class RadniNalogStatus : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public RadniNalogStatus()
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
                listViewRadniNalogStatus.ItemsSource = dBProksi.DajSveRadniNalogStatus();

                if (!listViewRadniNalogStatus.Items.Count.Equals(0))
                {
                    listViewRadniNalogStatus.SelectedIndex = 0;
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

        private void SelektujRadniNalogStatus(int radniNalogStatusID)
        {
            ObservableCollection<DB.RadniNalogStatus> _radniNalogStatus = (ObservableCollection<DB.RadniNalogStatus>)listViewRadniNalogStatus.ItemsSource;
            bool _postoji = false;

            if (!listViewRadniNalogStatus.Items.Count.Equals(0))
            {
                foreach (DB.RadniNalogStatus item in _radniNalogStatus)
                {
                    if (item.RadniNalogStatusID.Equals(radniNalogStatusID))
                    {
                        listViewRadniNalogStatus.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewRadniNalogStatus.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - RadniNalogStatus";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            RadniNalogStatusDetaljno _radniNalogStatusDetaljno = new RadniNalogStatusDetaljno(this, false);
            //_radniNalogStatusDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _radniNalogStatusDetaljno.Owner = Window.GetWindow(this);
            _radniNalogStatusDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _radniNalogStatusDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.RadniNalogStatus> _radniNalogStatusi = (ObservableCollection<DB.RadniNalogStatus>)listViewRadniNalogStatus.ItemsSource;
            DB.RadniNalogStatus _radniNalogStatus = (DB.RadniNalogStatus)listViewRadniNalogStatus.SelectedItem;

            if (_radniNalogStatusi != null && _radniNalogStatus != null)
            {
                RadniNalogStatusDetaljno _radniNalogStatusDetaljno = new RadniNalogStatusDetaljno(this, true);
                //_radniNalogStatusDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _radniNalogStatusDetaljno.Owner = Window.GetWindow(this);
                _radniNalogStatusDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _radniNalogStatusDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiRadniNalogStatus((DB.RadniNalogStatus)listViewRadniNalogStatus.SelectedItem);
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
                    if (comboBoxRadniNalogStatusKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.RadniNalogStatus> _radniNalogStatus = (ObservableCollection<DB.RadniNalogStatus>)listViewRadniNalogStatus.ItemsSource;

                        listViewRadniNalogStatus.ItemsSource = dBProksi.OsveziRadniNalogStatus(_radniNalogStatus);

                        if (!listViewRadniNalogStatus.Items.Count.Equals(0))
                        {
                            listViewRadniNalogStatus.SelectedIndex = 0;
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
                        listViewRadniNalogStatus.ItemsSource = dBProksi.NadjiRadniNalogStatus(((ComboBoxItem)comboBoxRadniNalogStatusKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewRadniNalogStatus.Items.Count.Equals(0))
                        {
                            listViewRadniNalogStatus.SelectedIndex = 0;
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
                DB.RadniNalogStatus _trenutni = (DB.RadniNalogStatus)listViewRadniNalogStatus.SelectedItem;

                ObservableCollection<DB.RadniNalogStatus> _radniNalogStatus = (ObservableCollection<DB.RadniNalogStatus>)listViewRadniNalogStatus.ItemsSource;

                if (!_radniNalogStatus.Count.Equals(0))
                {
                    listViewRadniNalogStatus.ItemsSource = dBProksi.OsveziRadniNalogStatus(_radniNalogStatus);

                    if (_trenutni != null)
                    {
                        SelektujRadniNalogStatus(_trenutni.RadniNalogStatusID);
                    }

                    if (listViewRadniNalogStatus.Items.Count.Equals(0))
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
                DB.RadniNalogStatus _trenutni = (DB.RadniNalogStatus)listViewRadniNalogStatus.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxRadniNalogStatusKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewRadniNalogStatus.ItemsSource = dBProksi.NadjiRadniNalogStatus(((ComboBoxItem)comboBoxRadniNalogStatusKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewRadniNalogStatus.Items.Count.Equals(0))
                    {
                        listViewRadniNalogStatus.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujRadniNalogStatus(_trenutni.RadniNalogStatusID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.RadniNalogStatus> _radniNalogStatusi = (ObservableCollection<DB.RadniNalogStatus>)listViewRadniNalogStatus.ItemsSource;
            //DB.RadniNalogStatus _radniNalogStatus = (DB.RadniNalogStatus)listViewRadniNalogStatus.SelectedItem;

            //if (_radniNalogStatusi != null && _radniNalogStatus != null)
            //{
                RadniNalogStatusDetaljno _radniNalogStatusDetaljno = new RadniNalogStatusDetaljno(this, true);
                //_radniNalogStatusDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _radniNalogStatusDetaljno.Owner = Window.GetWindow(this);
                _radniNalogStatusDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _radniNalogStatusDetaljno.ShowDialog();
            //}
        }

    }
}
