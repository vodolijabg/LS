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
    /// Interaction logic for Mesto.xaml
    /// </summary>
    public partial class Mesto : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public Mesto()
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
                listViewMesto.ItemsSource = dBProksi.DajSvaMesta();

                if (!listViewMesto.Items.Count.Equals(0))
                {
                    listViewMesto.SelectedIndex = 0;
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

        private void SelektujMesto(int mestoID)
        {
            ObservableCollection<DB.Mesto> _mesta = (ObservableCollection<DB.Mesto>)listViewMesto.ItemsSource;
            bool _postoji = false;

            if (!listViewMesto.Items.Count.Equals(0))
            {
                foreach (DB.Mesto item in _mesta)
                {
                    if (item.MestoID.Equals(mestoID))
                    {
                        listViewMesto.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewMesto.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - Mesto";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            MestoDetaljno _mestoDetaljno = new MestoDetaljno(this, false);
            //_mestoDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _mestoDetaljno.Owner = Window.GetWindow(this);
            _mestoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _mestoDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.Mesto> _mesta = (ObservableCollection<DB.Mesto>)listViewMesto.ItemsSource;
            DB.Mesto _mesto = (DB.Mesto)listViewMesto.SelectedItem;

            if (_mesta != null && _mesto != null)
            {
                MestoDetaljno _mestoDetaljno = new MestoDetaljno(this, true);
                //_mestoDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _mestoDetaljno.Owner = Window.GetWindow(this);
                _mestoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _mestoDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiMesto((DB.Mesto)listViewMesto.SelectedItem);
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
                    if (comboBoxMestoKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.Mesto> _mesta = (ObservableCollection<DB.Mesto>)listViewMesto.ItemsSource;

                        listViewMesto.ItemsSource = dBProksi.OsveziMesta(_mesta);

                        if (!listViewMesto.Items.Count.Equals(0))
                        {
                            listViewMesto.SelectedIndex = 0;
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
                        listViewMesto.ItemsSource = dBProksi.NadjiMesta(((ComboBoxItem)comboBoxMestoKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewMesto.Items.Count.Equals(0))
                        {
                            listViewMesto.SelectedIndex = 0;
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
                DB.Mesto _trenutni = (DB.Mesto)listViewMesto.SelectedItem;

                ObservableCollection<DB.Mesto> _mesta = (ObservableCollection<DB.Mesto>)listViewMesto.ItemsSource;

                if (!_mesta.Count.Equals(0))
                {
                    listViewMesto.ItemsSource = dBProksi.OsveziMesta(_mesta);

                    if (_trenutni != null)
                    {
                        SelektujMesto(_trenutni.MestoID);
                    }
                    if (listViewMesto.Items.Count.Equals(0))
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
                DB.Mesto _trenutni = (DB.Mesto)listViewMesto.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();
                }
                else if (comboBoxMestoKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewMesto.ItemsSource = dBProksi.NadjiMesta(((ComboBoxItem)comboBoxMestoKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewMesto.Items.Count.Equals(0))
                    {
                        listViewMesto.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujMesto(_trenutni.MestoID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.Mesto> _mesta = (ObservableCollection<DB.Mesto>)listViewMesto.ItemsSource;
            //DB.Mesto _mesto = (DB.Mesto)listViewMesto.SelectedItem;

            //if (_mesta != null && _mesto != null)
            //{
                MestoDetaljno _mestoDetaljno = new MestoDetaljno(this, true);
                //_mestoDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _mestoDetaljno.Owner = Window.GetWindow(this);
                _mestoDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _mestoDetaljno.ShowDialog();
            //}
        }

    }
}
