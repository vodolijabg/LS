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
    /// Interaction logic for Bod.xaml
    /// </summary>
    public partial class Bod : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public Bod()
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
                listViewBod.ItemsSource = dBProksi.DajSveBod();

                if (!listViewBod.Items.Count.Equals(0))
                {
                    listViewBod.SelectedIndex = 0;
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

        private void SelektujBod(int bodID)
        {
            ObservableCollection<DB.Bod> _bod = (ObservableCollection<DB.Bod>)listViewBod.ItemsSource;
            bool _postoji = false;

            if (!listViewBod.Items.Count.Equals(0))
            {
                foreach (DB.Bod item in _bod)
                {
                    if (item.BodID.Equals(bodID))
                    {
                        listViewBod.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewBod.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - Bod";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            BodDetaljno _bodDetaljno = new BodDetaljno(this, false);
            //_bodDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _bodDetaljno.Owner = Window.GetWindow(this);
            _bodDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _bodDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.Bod> _bodovi = (ObservableCollection<DB.Bod>)listViewBod.ItemsSource;
            DB.Bod _bod = (DB.Bod)listViewBod.SelectedItem;

            if (_bodovi != null && _bod != null)
            {
                BodDetaljno _bodDetaljno = new BodDetaljno(this, true);
                //_bodDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _bodDetaljno.Owner = Window.GetWindow(this);
                _bodDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _bodDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiBod((DB.Bod)listViewBod.SelectedItem);
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
                    if (comboBoxBodKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.Bod> _bod = (ObservableCollection<DB.Bod>)listViewBod.ItemsSource;

                        listViewBod.ItemsSource = dBProksi.OsveziBod(_bod);

                        if (!listViewBod.Items.Count.Equals(0))
                        {
                            listViewBod.SelectedIndex = 0;
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
                        listViewBod.ItemsSource = dBProksi.NadjiBod(((ComboBoxItem)comboBoxBodKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewBod.Items.Count.Equals(0))
                        {
                            listViewBod.SelectedIndex = 0;
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
                DB.Bod _trenutni = (DB.Bod)listViewBod.SelectedItem;

                ObservableCollection<DB.Bod> _bodovi = (ObservableCollection<DB.Bod>)listViewBod.ItemsSource;

                if (!_bodovi.Count.Equals(0))
                {
                    listViewBod.ItemsSource = dBProksi.OsveziBod(_bodovi);

                    if (_trenutni != null)
                    {
                        SelektujBod(_trenutni.BodID);
                    }
                    if (listViewBod.Items.Count.Equals(0))
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
                DB.Bod _trenutni = (DB.Bod)listViewBod.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxBodKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewBod.ItemsSource = dBProksi.NadjiBod(((ComboBoxItem)comboBoxBodKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewBod.Items.Count.Equals(0))
                    {
                        listViewBod.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujBod(_trenutni.BodID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.Bod> _bodovi = (ObservableCollection<DB.Bod>)listViewBod.ItemsSource;
            //DB.Bod _bod = (DB.Bod)listViewBod.SelectedItem;

            //if (_bodovi != null && _bod != null)
            //{
                BodDetaljno _bodDetaljno = new BodDetaljno(this, true);
                //_bodDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _bodDetaljno.Owner = Window.GetWindow(this);
                _bodDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _bodDetaljno.ShowDialog();
            //}
        }
    }
}
