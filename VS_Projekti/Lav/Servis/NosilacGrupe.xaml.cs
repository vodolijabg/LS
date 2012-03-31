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
using System.ComponentModel;

namespace Servis
{
    /// <summary>
    /// Interaction logic for NosilacGrupe.xaml
    /// </summary>
    public partial class NosilacGrupe : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public NosilacGrupe()
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
                listViewNosilacGrupe.ItemsSource = dBProksi.DajSveNosilacGrupe();

                if (!listViewNosilacGrupe.Items.Count.Equals(0))
                {
                    listViewNosilacGrupe.SelectedIndex = 0;
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

        private void SelektujNosilacGrupe(int nosilacGrupeID)
        {
            ObservableCollection<DB.NosilacGrupe> _nosilacGrupe = (ObservableCollection<DB.NosilacGrupe>)listViewNosilacGrupe.ItemsSource;
            bool _postoji = false;

            if (!listViewNosilacGrupe.Items.Count.Equals(0))
            {
                foreach (DB.NosilacGrupe item in _nosilacGrupe)
                {
                    if (item.NosilacGrupeID.Equals(nosilacGrupeID))
                    {
                        listViewNosilacGrupe.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewNosilacGrupe.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - NosilacGrupe";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            NosilacGrupeDetaljno _nosilacGrupeDetaljno = new NosilacGrupeDetaljno(this, false);
            //_nosilacGrupeDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _nosilacGrupeDetaljno.Owner = Window.GetWindow(this);
            _nosilacGrupeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _nosilacGrupeDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.NosilacGrupe> _nosiociGrupe = (ObservableCollection<DB.NosilacGrupe>)listViewNosilacGrupe.ItemsSource;
            DB.NosilacGrupe _nosilacGrupe = (DB.NosilacGrupe)listViewNosilacGrupe.SelectedItem;

            if (_nosiociGrupe != null && _nosilacGrupe != null)
            {
                NosilacGrupeDetaljno _nosilacGrupeDetaljno = new NosilacGrupeDetaljno(this, true);
                //_nosilacGrupeDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _nosilacGrupeDetaljno.Owner = Window.GetWindow(this);
                _nosilacGrupeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _nosilacGrupeDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiNosilacGrupe((DB.NosilacGrupe)listViewNosilacGrupe.SelectedItem);
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
                    if (comboBoxNosilacGrupeKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.NosilacGrupe> _nosilacGrupe = (ObservableCollection<DB.NosilacGrupe>)listViewNosilacGrupe.ItemsSource;

                        listViewNosilacGrupe.ItemsSource = dBProksi.OsveziNosilacGrupe(_nosilacGrupe);

                        if (!listViewNosilacGrupe.Items.Count.Equals(0))
                        {
                            listViewNosilacGrupe.SelectedIndex = 0;
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
                        listViewNosilacGrupe.ItemsSource = dBProksi.NadjiNosilacGrupe(((ComboBoxItem)comboBoxNosilacGrupeKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewNosilacGrupe.Items.Count.Equals(0))
                        {
                            listViewNosilacGrupe.SelectedIndex = 0;
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
                DB.NosilacGrupe _trenutni = (DB.NosilacGrupe)listViewNosilacGrupe.SelectedItem;

                ObservableCollection<DB.NosilacGrupe> _nosilacGrupe = (ObservableCollection<DB.NosilacGrupe>)listViewNosilacGrupe.ItemsSource;

                if (!_nosilacGrupe.Count.Equals(0))
                {
                    listViewNosilacGrupe.ItemsSource = dBProksi.OsveziNosilacGrupe(_nosilacGrupe);

                    if (_trenutni != null)
                    {
                        SelektujNosilacGrupe(_trenutni.NosilacGrupeID);
                    }
                    if (listViewNosilacGrupe.Items.Count.Equals(0))
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
                DB.NosilacGrupe _trenutni = (DB.NosilacGrupe)listViewNosilacGrupe.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxNosilacGrupeKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewNosilacGrupe.ItemsSource = dBProksi.NadjiNosilacGrupe(((ComboBoxItem)comboBoxNosilacGrupeKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewNosilacGrupe.Items.Count.Equals(0))
                    {
                        listViewNosilacGrupe.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujNosilacGrupe(_trenutni.NosilacGrupeID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.NosilacGrupe> _nosiociGrupe = (ObservableCollection<DB.NosilacGrupe>)listViewNosilacGrupe.ItemsSource;
            //DB.NosilacGrupe _nosilacGrupe = (DB.NosilacGrupe)listViewNosilacGrupe.SelectedItem;

            //if (_nosiociGrupe != null && _nosilacGrupe != null)
            //{
                NosilacGrupeDetaljno _nosilacGrupeDetaljno = new NosilacGrupeDetaljno(this, true);
                //_nosilacGrupeDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _nosilacGrupeDetaljno.Owner = Window.GetWindow(this);
                _nosilacGrupeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _nosilacGrupeDetaljno.ShowDialog();
            //}
        }

    }
}
