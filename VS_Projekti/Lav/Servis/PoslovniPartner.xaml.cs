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
    /// Interaction logic for PoslovniPartner.xaml
    /// </summary>
    public partial class PoslovniPartner : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        Servis.ServisnaKnjizicaDetaljno servisnaKnjizicaDetaljno;

        public PoslovniPartner()
        {
            InitializeComponent();

            textBoxTraziZa.Focus();
        }

        public PoslovniPartner(Servis.ServisnaKnjizicaDetaljno servisnaKnjizicaDetaljno) : this()
        {
            this.servisnaKnjizicaDetaljno = servisnaKnjizicaDetaljno;
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
                listViewPoslovniPartner.ItemsSource = dBProksi.DajSvePoslovniPartner();

                if (!listViewPoslovniPartner.Items.Count.Equals(0))
                {
                    listViewPoslovniPartner.SelectedIndex = 0;
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

        private void SelektujPoslovniPartner(int poslovniPartnerID)
        {
            ObservableCollection<DB.PoslovniPartner> _poslovniPartneri = (ObservableCollection<DB.PoslovniPartner>)listViewPoslovniPartner.ItemsSource;
            bool _postoji = false;

            if (!listViewPoslovniPartner.Items.Count.Equals(0))
            {
                foreach (DB.PoslovniPartner item in _poslovniPartneri)
                {
                    if (item.PoslovniPartnerID.Equals(poslovniPartnerID))
                    {
                        listViewPoslovniPartner.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewPoslovniPartner.SelectedIndex = 0;
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
                //Zbog potencijalno velikog broja slogova necu puniti tabelu inicijalno
                //DajSve();
                listViewPoslovniPartner.ItemsSource = new ObservableCollection<DB.PoslovniPartner>();

                UStanje(App.Stanje.Osnovno);

                //ako sam usao sa servisne knjizice i ako je vec odabran partner
                if (servisnaKnjizicaDetaljno != null && servisnaKnjizicaDetaljno.textBoxPartner.Text.Trim() != "")
                {
                    try
                    {
                        //int _poslovniPartnerID = Convert.ToInt32(servisnaKnjizicaDetaljno.textBoxPartner.Tag);

                        //foreach (DB.PoslovniPartner item in listViewPoslovniPartner.Items)
                        //{
                        //    if (item.PoslovniPartnerID.Equals(_poslovniPartnerID))
                        //    {
                        //        listViewPoslovniPartner.SelectedItem = item;
                        //        break;
                        //    }
                        //}

                        listViewPoslovniPartner.ItemsSource = dBProksi.NadjiPoslovniPartner("ID", servisnaKnjizicaDetaljno.textBoxPartner.Tag.ToString());

                        if (!listViewPoslovniPartner.Items.Count.Equals(0))
                        {
                            listViewPoslovniPartner.SelectedIndex = 0;
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
             }

            Window.GetWindow(this).Title = "Lav - PoslovniPartner";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            PoslovniPartnerDetaljno _poslovniPartnerDetaljno = new PoslovniPartnerDetaljno(this, false, servisnaKnjizicaDetaljno != null ? false : true);
            //_poslovniPartnerDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _poslovniPartnerDetaljno.Owner = Window.GetWindow(this);
            _poslovniPartnerDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _poslovniPartnerDetaljno.ShowDialog();

        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.PoslovniPartner> _poslovniPartneri = (ObservableCollection<DB.PoslovniPartner>)listViewPoslovniPartner.ItemsSource;
            DB.PoslovniPartner _poslovniPartner = (DB.PoslovniPartner)listViewPoslovniPartner.SelectedItem;

            if (_poslovniPartneri != null && _poslovniPartner != null)
            {
                PoslovniPartnerDetaljno _poslovniPartnerDetaljno = new PoslovniPartnerDetaljno(this, true, servisnaKnjizicaDetaljno != null ? false : true);
                //_poslovniPartnerDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _poslovniPartnerDetaljno.Owner = Window.GetWindow(this);
                _poslovniPartnerDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _poslovniPartnerDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiPoslovniPartner((DB.PoslovniPartner)listViewPoslovniPartner.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {
                    //DajSve();
                    buttonOsvezi_Click(null, null);
                }
                else
                {
                    //ako ima filter ali nije odabrana kolona ne prijavljuj gresku, osvezi podatke
                    if (comboBoxPoslovniPartnerKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.PoslovniPartner> _poslovniPartneri = (ObservableCollection<DB.PoslovniPartner>)listViewPoslovniPartner.ItemsSource;

                        listViewPoslovniPartner.ItemsSource = dBProksi.OsveziPoslovniPartner(_poslovniPartneri);

                        if (!listViewPoslovniPartner.Items.Count.Equals(0))
                        {
                            listViewPoslovniPartner.SelectedIndex = 0;
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
                        listViewPoslovniPartner.ItemsSource = dBProksi.NadjiPoslovniPartner(((ComboBoxItem)comboBoxPoslovniPartnerKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewPoslovniPartner.Items.Count.Equals(0))
                        {
                            listViewPoslovniPartner.SelectedIndex = 0;
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
                DB.PoslovniPartner _trenutni = (DB.PoslovniPartner)listViewPoslovniPartner.SelectedItem;

                ObservableCollection<DB.PoslovniPartner> _poslovniPartneri = (ObservableCollection<DB.PoslovniPartner>)listViewPoslovniPartner.ItemsSource;

                if (!_poslovniPartneri.Count.Equals(0))
                {
                    listViewPoslovniPartner.ItemsSource = dBProksi.OsveziPoslovniPartner(_poslovniPartneri);

                    if (_trenutni != null)
                    {
                        SelektujPoslovniPartner(_trenutni.PoslovniPartnerID);
                    }

                    if (listViewPoslovniPartner.Items.Count.Equals(0))
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

                DB.PoslovniPartner _trenutni = (DB.PoslovniPartner)listViewPoslovniPartner.SelectedItem;
                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {
                    DajSve();
                }
                else if (comboBoxPoslovniPartnerKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewPoslovniPartner.ItemsSource = dBProksi.NadjiPoslovniPartner(((ComboBoxItem)comboBoxPoslovniPartnerKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewPoslovniPartner.Items.Count.Equals(0))
                    {
                        listViewPoslovniPartner.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }

                if (_trenutni != null)
                {
                    SelektujPoslovniPartner(_trenutni.PoslovniPartnerID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (servisnaKnjizicaDetaljno == null)
            {
                //ObservableCollection<DB.PoslovniPartner> _poslovniPartneri = (ObservableCollection<DB.PoslovniPartner>)listViewPoslovniPartner.ItemsSource;
                //DB.PoslovniPartner _poslovniPartner = (DB.PoslovniPartner)listViewPoslovniPartner.SelectedItem;

                //if (_poslovniPartneri != null && _poslovniPartner != null)
                //{
                PoslovniPartnerDetaljno _poslovniPartnerDetaljno = new PoslovniPartnerDetaljno(this, true, servisnaKnjizicaDetaljno != null ? false : true);
                //_poslovniPartnerDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _poslovniPartnerDetaljno.Owner = Window.GetWindow(this);
                _poslovniPartnerDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _poslovniPartnerDetaljno.ShowDialog();
                //}
            }
            else
            {
                DB.PoslovniPartner _poslovniPartner = (DB.PoslovniPartner)listViewPoslovniPartner.SelectedItem;

                servisnaKnjizicaDetaljno.textBoxPartner.Text = _poslovniPartner.SkracenNaziv;
                servisnaKnjizicaDetaljno.textBoxPartner.Tag = _poslovniPartner.PoslovniPartnerID;

                Window.GetWindow(this).Close();

            }
        }
    }
}
