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
    /// Interaction logic for ServisnaKnjizica.xaml
    /// </summary>
    public partial class ServisnaKnjizica : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        Servis.PonudaDetaljno ponudaDetaljno;
        Servis.RadniNalogDetaljno radniNalogDetaljno;

        public ServisnaKnjizica()
        {
            InitializeComponent();
            textBoxTraziZa.Focus();
        }

        public ServisnaKnjizica(Servis.PonudaDetaljno ponudaDetaljno) : this()
        {
            this.ponudaDetaljno = ponudaDetaljno;
        }

        public ServisnaKnjizica(Servis.RadniNalogDetaljno radniNalogDetaljno)
            : this()
        {
            this.radniNalogDetaljno = radniNalogDetaljno;
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
                listViewServisnaKnjizica.ItemsSource = dBProksi.DajSveServisnaKnjizica((bool)radioButtonFizickoLice.IsChecked ? "FizickoLice" : "PoslovniPartner");

                if (!listViewServisnaKnjizica.Items.Count.Equals(0))
                {
                    listViewServisnaKnjizica.SelectedIndex = 0;
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

        private void SelektujServisnaKnjizica(int servisnaKnjizicaID)
        {
            ObservableCollection<DB.ServisnaKnjizica> _servisneKnjizice = (ObservableCollection<DB.ServisnaKnjizica>)listViewServisnaKnjizica.ItemsSource;
            bool _postoji = false;

            if (!listViewServisnaKnjizica.Items.Count.Equals(0))
            {
                foreach (DB.ServisnaKnjizica item in _servisneKnjizice)
                {
                    if (item.ServisnaKnjizicaID.Equals(servisnaKnjizicaID))
                    {
                        listViewServisnaKnjizica.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewServisnaKnjizica.SelectedIndex = 0;
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
                    listViewServisnaKnjizica.ItemsSource = new ObservableCollection<DB.ServisnaKnjizica>();

                    UStanje(App.Stanje.Osnovno);
                }

                //za ulaz sa PoslovniPartnerDetaljno i FizickoLiceDetaljno
                if (App.ServisnaKnjizicaPartnerID != -1 && App.ServisnaKnjizicaVrstaPartnera != "")
                {
                    listViewServisnaKnjizica.ItemsSource = dBProksi.DajSveServisnaKnjizicaZaPartnera(App.ServisnaKnjizicaPartnerID, App.ServisnaKnjizicaVrstaPartnera);

                    if (!listViewServisnaKnjizica.Items.Count.Equals(0))
                    {
                        listViewServisnaKnjizica.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);

                    }

                    if (App.ServisnaKnjizicaVrstaPartnera == "PoslovniPartner")
                    {
                        radioButtonPoslovniPartner.IsChecked = true;
                    }
                    else
                    {
                        radioButtonFizickoLice.IsChecked = true;
                    }

                    App.ServisnaKnjizicaPartnerID = -1;
                    App.ServisnaKnjizicaVrstaPartnera = "";

                    textBoxTraziZa.Text = "";
                }

                //ako sam usao sa Ponude i ako je vec odabrana knjizica
                if (ponudaDetaljno != null && ponudaDetaljno.textBoxServisnaKnjizica.Text.Trim() != "")
                {
                    try
                    {
                        listViewServisnaKnjizica.ItemsSource = dBProksi.NadjiServisnuKnjizicu((int)ponudaDetaljno.textBoxServisnaKnjizica.Tag);


                        if (!listViewServisnaKnjizica.Items.Count.Equals(0))
                        {
                            listViewServisnaKnjizica.SelectedIndex = 0;

                            DB.ServisnaKnjizica _servisnaKnjizica = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;
                            if (_servisnaKnjizica.FizickoLiceID != null)
                            {
                                radioButtonFizickoLice.IsChecked = true;
                                radioButtonPoslovniPartner.IsChecked = false;
                            }
                            else
                            {
                                radioButtonFizickoLice.IsChecked = false;
                                radioButtonPoslovniPartner.IsChecked = true;
                            }

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

                //ako sam usao sa RadnogNaloga i ako je vec odabrana knjizica
                if (radniNalogDetaljno != null && radniNalogDetaljno.textBoxServisnaKnjizica.Text.Trim() != "")
                {
                    try
                    {
                        listViewServisnaKnjizica.ItemsSource = dBProksi.NadjiServisnuKnjizicu((int)radniNalogDetaljno.textBoxServisnaKnjizica.Tag);


                        if (!listViewServisnaKnjizica.Items.Count.Equals(0))
                        {
                            listViewServisnaKnjizica.SelectedIndex = 0;

                            DB.ServisnaKnjizica _servisnaKnjizica = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;
                            if (_servisnaKnjizica.FizickoLiceID != null)
                            {
                                radioButtonFizickoLice.IsChecked = true;
                                radioButtonPoslovniPartner.IsChecked = false;
                            }
                            else
                            {
                                radioButtonFizickoLice.IsChecked = false;
                                radioButtonPoslovniPartner.IsChecked = true;
                            }

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
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            Window.GetWindow(this).Title = "Lav - ServisnaKnjizica";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            ServisnaKnjizicaDetaljno _servisnaKnjizicaDetaljno = new ServisnaKnjizicaDetaljno(this, false, (ponudaDetaljno != null || radniNalogDetaljno != null) ? false : true);
            //_servisnaKnjizicaDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _servisnaKnjizicaDetaljno.Owner = Window.GetWindow(this);
            _servisnaKnjizicaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _servisnaKnjizicaDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.ServisnaKnjizica> _servisneKnjizice = (ObservableCollection<DB.ServisnaKnjizica>)listViewServisnaKnjizica.ItemsSource;
            DB.ServisnaKnjizica _servisnaKnjizica = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;

            if (_servisneKnjizice != null && _servisnaKnjizica != null)
            {
                ServisnaKnjizicaDetaljno _servisnaKnjizicaDetaljno = new ServisnaKnjizicaDetaljno(this, true, (ponudaDetaljno != null || radniNalogDetaljno != null) ? false : true);
                //_servisnaKnjizicaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _servisnaKnjizicaDetaljno.Owner = Window.GetWindow(this);
                _servisnaKnjizicaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _servisnaKnjizicaDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiServisnaKnjizica((DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem);
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
                    if (comboBoxServisnaKnjizicaKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.ServisnaKnjizica> _servisneKnjizice = (ObservableCollection<DB.ServisnaKnjizica>)listViewServisnaKnjizica.ItemsSource;

                        listViewServisnaKnjizica.ItemsSource = dBProksi.OsveziServisnaKnjizica(_servisneKnjizice);

                        if (!listViewServisnaKnjizica.Items.Count.Equals(0))
                        {
                            listViewServisnaKnjizica.SelectedIndex = 0;
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
                        listViewServisnaKnjizica.ItemsSource = dBProksi.NadjiServisnuKnjizicu(((ComboBoxItem)comboBoxServisnaKnjizicaKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim(), (bool)radioButtonFizickoLice.IsChecked ? "FizickoLice" : "PoslovniPartner");

                        if (!listViewServisnaKnjizica.Items.Count.Equals(0))
                        {
                            listViewServisnaKnjizica.SelectedIndex = 0;
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
                DB.ServisnaKnjizica _trenutni = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;

                ObservableCollection<DB.ServisnaKnjizica> _servisnaKnjizica = (ObservableCollection<DB.ServisnaKnjizica>)listViewServisnaKnjizica.ItemsSource;

                if (!_servisnaKnjizica.Count.Equals(0))
                {
                    listViewServisnaKnjizica.ItemsSource = dBProksi.OsveziServisnaKnjizica(_servisnaKnjizica);

                    if (_trenutni != null)
                    {
                        SelektujServisnaKnjizica(_trenutni.ServisnaKnjizicaID);
                    }

                    if (listViewServisnaKnjizica.Items.Count.Equals(0))
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
                DB.ServisnaKnjizica _trenutni = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxServisnaKnjizicaKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewServisnaKnjizica.ItemsSource = dBProksi.NadjiServisnuKnjizicu(((ComboBoxItem)comboBoxServisnaKnjizicaKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim(), (bool)radioButtonFizickoLice.IsChecked ? "FizickoLice" : "PoslovniPartner");

                    if (!listViewServisnaKnjizica.Items.Count.Equals(0))
                    {
                        listViewServisnaKnjizica.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujServisnaKnjizica(_trenutni.ServisnaKnjizicaID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {         
            if (ponudaDetaljno != null)
            {
                DB.ServisnaKnjizica _servisnaKnjizica = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;

                ponudaDetaljno.textBoxServisnaKnjizica.Text = _servisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " + _servisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " + _servisnaKnjizica.TipAutomobila.OpisTabela.Opis;
                ponudaDetaljno.textBoxServisnaKnjizica.Tag = _servisnaKnjizica.ServisnaKnjizicaID;
                ponudaDetaljno.textBoxServisnaKnjizicaSifra.Text = _servisnaKnjizica.Sifra;

                Window.GetWindow(this).Close();
            }
            else if (radniNalogDetaljno != null)
            {
                DB.ServisnaKnjizica _servisnaKnjizica = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;

                radniNalogDetaljno.textBoxServisnaKnjizica.Text = _servisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " + _servisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " + _servisnaKnjizica.TipAutomobila.OpisTabela.Opis;
                radniNalogDetaljno.textBoxServisnaKnjizica.Tag = _servisnaKnjizica.ServisnaKnjizicaID;

                Window.GetWindow(this).Close();
            }
            else
            {
                //ObservableCollection<DB.ServisnaKnjizica> _servisneKnjizice = (ObservableCollection<DB.ServisnaKnjizica>)listViewServisnaKnjizica.ItemsSource;
                //DB.ServisnaKnjizica _servisnaKnjizica = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;

                //if (_servisneKnjizice != null && _servisnaKnjizica != null)
                //{
                ServisnaKnjizicaDetaljno _servisnaKnjizicaDetaljno = new ServisnaKnjizicaDetaljno(this, true, (ponudaDetaljno != null || radniNalogDetaljno != null) ? false : true);
                //_servisnaKnjizicaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _servisnaKnjizicaDetaljno.Owner = Window.GetWindow(this);
                _servisnaKnjizicaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _servisnaKnjizicaDetaljno.ShowDialog();
                //}
            }
        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            listViewServisnaKnjizica.ItemsSource = new ObservableCollection<DB.ServisnaKnjizica>();

            UStanje(App.Stanje.Osnovno);
        }

        
    }
}
