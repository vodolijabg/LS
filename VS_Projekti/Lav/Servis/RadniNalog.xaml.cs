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
    /// Interaction logic for RadniNalog.xaml
    /// </summary>
    public partial class RadniNalog : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public RadniNalog()
        {
            InitializeComponent();
            textBoxTraziRadniNalogID.Focus();
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
                listViewRadniNalog.ItemsSource = dBProksi.DajSveRadniNalog();

                if (!listViewRadniNalog.Items.Count.Equals(0))
                {
                    listViewRadniNalog.SelectedIndex = 0;
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

        internal void SelektujRadniNalog(int radniNalogID)
        {
            ObservableCollection<DB.RadniNalog> _radniNalog = (ObservableCollection<DB.RadniNalog>)listViewRadniNalog.ItemsSource;
            bool _postoji = false;

            if (!listViewRadniNalog.Items.Count.Equals(0))
            {
                foreach (DB.RadniNalog item in _radniNalog)
                {
                    if (item.RadniNalogID.Equals(radniNalogID))
                    {
                        listViewRadniNalog.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewRadniNalog.SelectedIndex = 0;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
                if (prvoOtvaranjeStrane)
                {
                    dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                    prvoOtvaranjeStrane = false;
                    //Zbog potencijalno velikog broja slogova necu puniti tabelu inicijalno
                    //DajSve();
                    listViewRadniNalog.ItemsSource = new ObservableCollection<DB.RadniNalog>();

                    UStanje(App.Stanje.Osnovno);
                }

                //za ulaz sa PoslovniPartnerDetaljno i FizickoLiceDetaljno i ServisnaKnjizicaDetaljno
                if (App.RadniNalogPartnerID != -1 && App.RadniNalogVrstaPartnera != "")
                {
                    if (App.RadniNalogServisnaKnjizicaID == -1)
                    {
                        listViewRadniNalog.ItemsSource = dBProksi.DajSveRadneNalogeZaPartnera(App.RadniNalogPartnerID, App.RadniNalogVrstaPartnera);
                    }
                    else
                    {
                        listViewRadniNalog.ItemsSource = dBProksi.DajSveRadneNalogeZaServisnuKnjizicu(App.RadniNalogServisnaKnjizicaID);
                    }

                    if (!listViewRadniNalog.Items.Count.Equals(0))
                    {
                        listViewRadniNalog.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);

                    }
                    App.RadniNalogPartnerID = -1;
                    App.RadniNalogVrstaPartnera = "";
                    App.RadniNalogServisnaKnjizicaID = -1;

                    textBoxTraziRadniNalogID.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Window.GetWindow(this).Title = "Lav - RadniNalog";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            RadniNalogDetaljno _radniNalogDetaljno = new RadniNalogDetaljno(this, false);
            _radniNalogDetaljno.Owner = Window.GetWindow(this);
            _radniNalogDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (Window.GetWindow(this).WindowState == WindowState.Normal)
            {
                _radniNalogDetaljno.Width = Window.GetWindow(this).ActualWidth;
                _radniNalogDetaljno.Height = Window.GetWindow(this).ActualHeight;
            }
            else
            {
                _radniNalogDetaljno.WindowState = Window.GetWindow(this).WindowState;
            }
            _radniNalogDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.RadniNalog> _radniNalogLista = (ObservableCollection<DB.RadniNalog>)listViewRadniNalog.ItemsSource;
            DB.RadniNalog _radniNalog = (DB.RadniNalog)listViewRadniNalog.SelectedItem;

            if (_radniNalogLista != null && _radniNalog != null)
            {
                RadniNalogDetaljno _radniNalogDetaljno = new RadniNalogDetaljno(this, true);
                //_ponudaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _radniNalogDetaljno.Owner = Window.GetWindow(this);
                _radniNalogDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (Window.GetWindow(this).WindowState == WindowState.Normal)
                {
                    _radniNalogDetaljno.Width = Window.GetWindow(this).ActualWidth;
                    _radniNalogDetaljno.Height = Window.GetWindow(this).ActualHeight;
                }
                else
                {
                    _radniNalogDetaljno.WindowState = Window.GetWindow(this).WindowState;
                }
                _radniNalogDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiRadniNalog((DB.RadniNalog)listViewRadniNalog.SelectedItem, App.Radnik);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //ako nema filtera osvezi 
                if (textBoxTraziRadniNalogID.Text.Trim() == "")
                {
                    ObservableCollection<DB.RadniNalog> _ponude = (ObservableCollection<DB.RadniNalog>)listViewRadniNalog.ItemsSource;

                    listViewRadniNalog.ItemsSource = dBProksi.OsveziRadniNalog(_ponude);

                    if (!listViewRadniNalog.Items.Count.Equals(0))
                    {
                        listViewRadniNalog.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                else
                {
                    listViewRadniNalog.ItemsSource = dBProksi.NadjiRadniNalog("", textBoxTraziRadniNalogID.Text.Trim().ToString(), null, null, "", null, null, Convert.ToInt32(Konfiguracija.RadniNalogStatusIDZavrsen));

                    if (!listViewRadniNalog.Items.Count.Equals(0))
                    {
                        listViewRadniNalog.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
            }
        }

        private void buttonOsvezi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.RadniNalog _trenutni = (DB.RadniNalog)listViewRadniNalog.SelectedItem;

                ObservableCollection<DB.RadniNalog> _radniNalog = (ObservableCollection<DB.RadniNalog>)listViewRadniNalog.ItemsSource;

                if (!_radniNalog.Count.Equals(0))
                {
                    listViewRadniNalog.ItemsSource = dBProksi.OsveziRadniNalog(_radniNalog);

                    if (_trenutni != null)
                    {
                        SelektujRadniNalog(_trenutni.RadniNalogID);
                    }

                    if (listViewRadniNalog.Items.Count.Equals(0))
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

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            RadniNalogDetaljno _radniNalogDetaljno = new RadniNalogDetaljno(this, true);
            _radniNalogDetaljno.Owner = Window.GetWindow(this);
            _radniNalogDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (Window.GetWindow(this).WindowState == WindowState.Normal)
            {
                _radniNalogDetaljno.Width = Window.GetWindow(this).ActualWidth;
                _radniNalogDetaljno.Height = Window.GetWindow(this).ActualHeight;
            }
            else
            {
                _radniNalogDetaljno.WindowState = Window.GetWindow(this).WindowState;
            }

            _radniNalogDetaljno.ShowDialog();
        }

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.RadniNalog _trenutni = (DB.RadniNalog)listViewRadniNalog.SelectedItem;

                //ako ima filter pretrazi
                if (textBoxTraziRadniNalogID.Text.Trim() != "")
                {
                    listViewRadniNalog.ItemsSource = dBProksi.NadjiRadniNalog("", textBoxTraziRadniNalogID.Text.ToString(), null, null, "", null, null, Convert.ToInt32(Konfiguracija.RadniNalogStatusIDZavrsen));

                    if (!listViewRadniNalog.Items.Count.Equals(0))
                    {
                        listViewRadniNalog.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);

                        if (_trenutni != null)
                        {
                            SelektujRadniNalog(_trenutni.RadniNalogID);
                        }
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);

                        textBoxTraziRadniNalogID.Text = "";

                        //ako za zadatu PonudaID nema redova otvori prozor za detaljnu pretragu 
                        RadniNalogPretraga _radniNalogPretraga = new RadniNalogPretraga(this);
                        //_ponudaPretraga.WindowStyle = WindowStyle.ToolWindow;
                        _radniNalogPretraga.Owner = Window.GetWindow(this);
                        _radniNalogPretraga.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        _radniNalogPretraga.ShowDialog();

                    }
                }
                //inace otvori detaljnu pretragu
                else
                {
                    textBoxTraziRadniNalogID.Text = "";

                    RadniNalogPretraga _radniNalogPretraga = new RadniNalogPretraga(this);
                    //_ponudaPretraga.WindowStyle = WindowStyle.ToolWindow;
                    _radniNalogPretraga.Owner = Window.GetWindow(this);
                    _radniNalogPretraga.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _radniNalogPretraga.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
  
    }
}
