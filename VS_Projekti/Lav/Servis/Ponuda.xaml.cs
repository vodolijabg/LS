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
    /// Interaction logic for Ponuda.xaml
    /// </summary>
    public partial class Ponuda : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public Ponuda()
        {
            InitializeComponent();
            textBoxTraziPonudaID.Focus();
        }

        public void UStanje(App.Stanje stanje)
        {
            buttonDodajWizard.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonDodaj.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
        }

        private void DajSve()
        {
            try
            {
                listViewPonuda.ItemsSource = dBProksi.DajSvePonude();

                if (!listViewPonuda.Items.Count.Equals(0))
                {
                    listViewPonuda.SelectedIndex = 0;
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

        internal void SelektujPonudu(int ponudaID)
        {
            ObservableCollection<DB.Ponuda> _ponuda = (ObservableCollection<DB.Ponuda>)listViewPonuda.ItemsSource;
            bool _postoji = false;

            if (!listViewPonuda.Items.Count.Equals(0))
            {
                foreach (DB.Ponuda item in _ponuda)
                {
                    if (item.PonudaID.Equals(ponudaID))
                    {
                        listViewPonuda.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewPonuda.SelectedIndex = 0;
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
                    listViewPonuda.ItemsSource = new ObservableCollection<DB.Ponuda>();

                    UStanje(App.Stanje.Osnovno);
                }
                //za ulaz sa PoslovniPartnerDetaljno i FizickoLiceDetaljno i ServisnaKnjizicaDetaljno
                if (App.PonudaPartnerID != -1 && App.PonudaVrstaPartnera != "")
                {
                    if (App.PonudaServisnaKnjizicaID == -1)
                    {
                        listViewPonuda.ItemsSource = dBProksi.DajSvePonudeZaPartnera(App.PonudaPartnerID, App.PonudaVrstaPartnera);
                    }
                    else
                    {
                        listViewPonuda.ItemsSource = dBProksi.DajSvePonudeZaServisnuKnjizicu(App.PonudaServisnaKnjizicaID);
                    }

                    if (!listViewPonuda.Items.Count.Equals(0))
                    {
                        listViewPonuda.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);

                    }


                    App.PonudaPartnerID = -1;
                    App.PonudaVrstaPartnera = "";
                    App.PonudaServisnaKnjizicaID = -1;

                    textBoxTraziPonudaID.Text = "";
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Window.GetWindow(this).Title = "Lav - Ponuda";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            PonudaDetaljno _ponudaDetaljno = new PonudaDetaljno(this, false);
            //_ponudaDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _ponudaDetaljno.Owner = Window.GetWindow(this);
            _ponudaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            if (Window.GetWindow(this).WindowState == WindowState.Normal)
            {
                _ponudaDetaljno.Width = Window.GetWindow(this).ActualWidth;
                _ponudaDetaljno.Height = Window.GetWindow(this).ActualHeight;
            }
            else
            {
                _ponudaDetaljno.WindowState = Window.GetWindow(this).WindowState;
            }
            _ponudaDetaljno.ShowDialog();

        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {       

            ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)listViewPonuda.ItemsSource;
            DB.Ponuda _ponuda = (DB.Ponuda)listViewPonuda.SelectedItem;

            if (_ponude != null && _ponuda != null)
            {
                PonudaDetaljno _ponudaDetaljno = new PonudaDetaljno(this, true);
                //_ponudaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _ponudaDetaljno.Owner = Window.GetWindow(this);
                _ponudaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                if (Window.GetWindow(this).WindowState == WindowState.Normal)
                {
                    _ponudaDetaljno.Width = Window.GetWindow(this).ActualWidth;
                    _ponudaDetaljno.Height = Window.GetWindow(this).ActualHeight;
                }
                else
                {
                    _ponudaDetaljno.WindowState = Window.GetWindow(this).WindowState;
                }
                _ponudaDetaljno.ShowDialog();
            }

        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiPonudu((DB.Ponuda)listViewPonuda.SelectedItem, App.Radnik);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //ako nema filtera osvezi 
                if (textBoxTraziPonudaID.Text.Trim() == "")
                {
                    ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)listViewPonuda.ItemsSource;

                    listViewPonuda.ItemsSource = dBProksi.OsveziPonude(_ponude);

                    if (!listViewPonuda.Items.Count.Equals(0))
                    {
                        listViewPonuda.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                else
                {
                    listViewPonuda.ItemsSource = dBProksi.NadjiPonude("", textBoxTraziPonudaID.Text.Trim().ToString(), null, null, "", null, null);

                    if (!listViewPonuda.Items.Count.Equals(0))
                    {
                        listViewPonuda.SelectedIndex = 0;
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
                DB.Ponuda _trenutni = (DB.Ponuda)listViewPonuda.SelectedItem;

                ObservableCollection<DB.Ponuda> _ponuda = (ObservableCollection<DB.Ponuda>)listViewPonuda.ItemsSource;

                if (!_ponuda.Count.Equals(0))
                {
                    listViewPonuda.ItemsSource = dBProksi.OsveziPonude(_ponuda);

                    if (_trenutni != null)
                    {
                        SelektujPonudu(_trenutni.PonudaID);
                    }

                    if (listViewPonuda.Items.Count.Equals(0))
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
            //ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)listViewPonuda.ItemsSource;
            //DB.Ponuda _ponuda = (DB.Ponuda)listViewPonuda.SelectedItem;

            //if (_ponude != null && _ponuda != null)
            //{
            PonudaDetaljno _ponudaDetaljno = new PonudaDetaljno(this, true);
            //_ponudaDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _ponudaDetaljno.Owner = Window.GetWindow(this);
            _ponudaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (Window.GetWindow(this).WindowState == WindowState.Normal)
            {
                _ponudaDetaljno.Width = Window.GetWindow(this).ActualWidth;
                _ponudaDetaljno.Height = Window.GetWindow(this).ActualHeight;
            }
            else
            {
                _ponudaDetaljno.WindowState = Window.GetWindow(this).WindowState;
            }

            _ponudaDetaljno.ShowDialog();

            //}
        }        
        

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.Ponuda _trenutni = (DB.Ponuda)listViewPonuda.SelectedItem;

                //ako ima filter pretrazi
                if (textBoxTraziPonudaID.Text.Trim() != "")
                {
                    listViewPonuda.ItemsSource = dBProksi.NadjiPonude("", textBoxTraziPonudaID.Text.ToString(), null, null, "", null, null);

                    if (!listViewPonuda.Items.Count.Equals(0))
                    {
                        listViewPonuda.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);

                        if (_trenutni != null)
                        {
                            SelektujPonudu(_trenutni.PonudaID);
                        }
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);

                        textBoxTraziPonudaID.Text = "";

                        //ako za zadatu PonudaID nema redova otvori prozor za detaljnu pretragu 
                        PonudaPretraga _ponudaPretraga = new PonudaPretraga(this);
                        //_ponudaPretraga.WindowStyle = WindowStyle.ToolWindow;
                        _ponudaPretraga.Owner = Window.GetWindow(this);
                        _ponudaPretraga.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        _ponudaPretraga.ShowDialog();

                    }
                }
                //inace otvori detaljnu pretragu
                else
                {
                    textBoxTraziPonudaID.Text = "";

                    PonudaPretraga _ponudaPretraga = new PonudaPretraga(this);
                    //_ponudaPretraga.WindowStyle = WindowStyle.ToolWindow;
                    _ponudaPretraga.Owner = Window.GetWindow(this);
                    _ponudaPretraga.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _ponudaPretraga.ShowDialog();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        
        //private void radioButton_Click(object sender, RoutedEventArgs e)
        //{
        //    listViewPonuda.ItemsSource = new ObservableCollection<DB.Ponuda>();

        //    UStanje(App.Stanje.Osnovno);
        //}

        private void buttonDodajWizard_Click(object sender, RoutedEventArgs e)
        {
            PonudaWizard _ponudaWizard = new PonudaWizard(this);
            _ponudaWizard.Owner = Window.GetWindow(this);
            _ponudaWizard.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _ponudaWizard.ShowDialog();
        }
    }
}
