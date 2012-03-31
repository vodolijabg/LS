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
    /// Interaction logic for VrstaUsluge.xaml
    /// </summary>
    public partial class VrstaUsluge : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public VrstaUsluge()
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
                listViewVrstaUsluge.ItemsSource = dBProksi.DajSveVrstaUsluge();

                if (!listViewVrstaUsluge.Items.Count.Equals(0))
                {
                    listViewVrstaUsluge.SelectedIndex = 0;
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

        private void SelektujVrstaUsluge(int vrstaUslugeID)
        {
            ObservableCollection<DB.VrstaUsluge> _vrstaUsluge = (ObservableCollection<DB.VrstaUsluge>)listViewVrstaUsluge.ItemsSource;
            bool _postoji = false;

            if (!listViewVrstaUsluge.Items.Count.Equals(0))
            {
                foreach (DB.VrstaUsluge item in _vrstaUsluge)
                {
                    if (item.VrstaUslugeID.Equals(vrstaUslugeID))
                    {
                        listViewVrstaUsluge.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewVrstaUsluge.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - VrstaUsluge";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            VrstaUslugeDetaljno _vrstaUslugeDetaljno = new VrstaUslugeDetaljno(this, false);
            //_vrstaUslugeDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _vrstaUslugeDetaljno.Owner = Window.GetWindow(this);
            _vrstaUslugeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _vrstaUslugeDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.VrstaUsluge> _vrsteUsluge = (ObservableCollection<DB.VrstaUsluge>)listViewVrstaUsluge.ItemsSource;
            DB.VrstaUsluge _vrstaUsluge = (DB.VrstaUsluge)listViewVrstaUsluge.SelectedItem;

            if (_vrsteUsluge != null && _vrstaUsluge != null)
            {
                VrstaUslugeDetaljno _vrstaUslugeDetaljno = new VrstaUslugeDetaljno(this, true);
                //_vrstaUslugeDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _vrstaUslugeDetaljno.Owner = Window.GetWindow(this);
                _vrstaUslugeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _vrstaUslugeDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiVrstaUsluge((DB.VrstaUsluge)listViewVrstaUsluge.SelectedItem);
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
                    if (comboBoxVrstaUslugeKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.VrstaUsluge> _vrstaUsluge = (ObservableCollection<DB.VrstaUsluge>)listViewVrstaUsluge.ItemsSource;

                        listViewVrstaUsluge.ItemsSource = dBProksi.OsveziVrstaUsluge(_vrstaUsluge);

                        if (!listViewVrstaUsluge.Items.Count.Equals(0))
                        {
                            listViewVrstaUsluge.SelectedIndex = 0;
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
                        listViewVrstaUsluge.ItemsSource = dBProksi.NadjiVrstaUsluge(((ComboBoxItem)comboBoxVrstaUslugeKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewVrstaUsluge.Items.Count.Equals(0))
                        {
                            listViewVrstaUsluge.SelectedIndex = 0;
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
                DB.VrstaUsluge _trenutni = (DB.VrstaUsluge)listViewVrstaUsluge.SelectedItem;

                ObservableCollection<DB.VrstaUsluge> _vrsteUsluge = (ObservableCollection<DB.VrstaUsluge>)listViewVrstaUsluge.ItemsSource;

                if (!_vrsteUsluge.Count.Equals(0))
                {
                    listViewVrstaUsluge.ItemsSource = dBProksi.OsveziVrstaUsluge(_vrsteUsluge);

                    if (_trenutni != null)
                    {
                        SelektujVrstaUsluge(_trenutni.VrstaUslugeID);
                    }

                    if (listViewVrstaUsluge.Items.Count.Equals(0))
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
                DB.VrstaUsluge _trenutni = (DB.VrstaUsluge)listViewVrstaUsluge.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxVrstaUslugeKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewVrstaUsluge.ItemsSource = dBProksi.NadjiVrstaUsluge(((ComboBoxItem)comboBoxVrstaUslugeKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewVrstaUsluge.Items.Count.Equals(0))
                    {
                        listViewVrstaUsluge.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujVrstaUsluge(_trenutni.VrstaUslugeID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.VrstaUsluge> _vrsteUsluge = (ObservableCollection<DB.VrstaUsluge>)listViewVrstaUsluge.ItemsSource;
            //DB.VrstaUsluge _vrstaUsluge = (DB.VrstaUsluge)listViewVrstaUsluge.SelectedItem;

            //if (_vrsteUsluge != null && _vrstaUsluge != null)
            //{
                VrstaUslugeDetaljno _vrstaUslugeDetaljno = new VrstaUslugeDetaljno(this, true);
                //_vrstaUslugeDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _vrstaUslugeDetaljno.Owner = Window.GetWindow(this);
                _vrstaUslugeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _vrstaUslugeDetaljno.ShowDialog();
            //}
        }
    }
}
