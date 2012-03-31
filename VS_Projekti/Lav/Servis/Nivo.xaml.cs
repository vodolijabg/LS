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
    /// Interaction logic for Nivo.xaml
    /// </summary>
    public partial class Nivo : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public Nivo()
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
                listViewNivo.ItemsSource = dBProksi.DajSveNivo();

                if (!listViewNivo.Items.Count.Equals(0))
                {
                    listViewNivo.SelectedIndex = 0;
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

        private void SelektujNivo(int nivoID)
        {
            ObservableCollection<DB.Nivo> _nivo = (ObservableCollection<DB.Nivo>)listViewNivo.ItemsSource;
            bool _postoji = false;

            if (!listViewNivo.Items.Count.Equals(0))
            {
                foreach (DB.Nivo item in _nivo)
                {
                    if (item.NivoID.Equals(nivoID))
                    {
                        listViewNivo.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewNivo.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - Nivo";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            NivoDetaljno _nivoDetaljno = new NivoDetaljno(this, false);
            //_nivoDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _nivoDetaljno.Owner = Window.GetWindow(this);
            _nivoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _nivoDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.Nivo> _nivoi = (ObservableCollection<DB.Nivo>)listViewNivo.ItemsSource;
            DB.Nivo _nivo = (DB.Nivo)listViewNivo.SelectedItem;

            if (_nivoi != null && _nivo != null)
            {
                NivoDetaljno _nivoDetaljno = new NivoDetaljno(this, true);
                //_nivoDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _nivoDetaljno.Owner = Window.GetWindow(this);
                _nivoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _nivoDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiNivo((DB.Nivo)listViewNivo.SelectedItem);
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
                    if (comboBoxNivoKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.Nivo> _nivo = (ObservableCollection<DB.Nivo>)listViewNivo.ItemsSource;

                        listViewNivo.ItemsSource = dBProksi.OsveziNivo(_nivo);

                        if (!listViewNivo.Items.Count.Equals(0))
                        {
                            listViewNivo.SelectedIndex = 0;
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
                        listViewNivo.ItemsSource = dBProksi.NadjiNivo(((ComboBoxItem)comboBoxNivoKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewNivo.Items.Count.Equals(0))
                        {
                            listViewNivo.SelectedIndex = 0;
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
                DB.Nivo _trenutni = (DB.Nivo)listViewNivo.SelectedItem;

                ObservableCollection<DB.Nivo> _nivoi = (ObservableCollection<DB.Nivo>)listViewNivo.ItemsSource;

                if (!_nivoi.Count.Equals(0))
                {
                    listViewNivo.ItemsSource = dBProksi.OsveziNivo(_nivoi);

                    if (_trenutni != null)
                    {
                        SelektujNivo(_trenutni.NivoID);
                    }
                    if (listViewNivo.Items.Count.Equals(0))
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
                DB.Nivo _trenutni = (DB.Nivo)listViewNivo.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxNivoKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewNivo.ItemsSource = dBProksi.NadjiNivo(((ComboBoxItem)comboBoxNivoKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewNivo.Items.Count.Equals(0))
                    {
                        listViewNivo.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujNivo(_trenutni.NivoID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.Nivo> _nivoi = (ObservableCollection<DB.Nivo>)listViewNivo.ItemsSource;
            //DB.Nivo _nivo = (DB.Nivo)listViewNivo.SelectedItem;

            //if (_nivoi != null && _nivo != null)
            //{
                NivoDetaljno _nivoDetaljno = new NivoDetaljno(this, true);
                //_nivoDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _nivoDetaljno.Owner = Window.GetWindow(this);
                _nivoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _nivoDetaljno.ShowDialog();
            //}
        }


    }
}
