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
    /// Interaction logic for FizickoLice.xaml
    /// </summary>
    public partial class FizickoLice : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        Servis.ServisnaKnjizicaDetaljno servisnaKnjizicaDetaljno;

        public FizickoLice()
        {
            InitializeComponent();

            textBoxTraziZa.Focus();
        }

        public FizickoLice(Servis.ServisnaKnjizicaDetaljno servisnaKnjizicaDetaljno):this()
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
                listViewFizickoLice.ItemsSource = dBProksi.DajSveFizickoLice();

                if (!listViewFizickoLice.Items.Count.Equals(0))
                {
                    listViewFizickoLice.SelectedIndex = 0;
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

        private void SelektujFizickoLice(int fizickoLiceID)
        {
            ObservableCollection<DB.FizickoLice> _fizickaLica = (ObservableCollection<DB.FizickoLice>)listViewFizickoLice.ItemsSource;
            bool _postoji = false;

            if (!listViewFizickoLice.Items.Count.Equals(0))
            {
                foreach (DB.FizickoLice item in _fizickaLica)
                {
                    if (item.FizickoLiceID.Equals(fizickoLiceID))
                    {
                        listViewFizickoLice.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewFizickoLice.SelectedIndex = 0;
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
                listViewFizickoLice.ItemsSource = new ObservableCollection<DB.FizickoLice>();

                UStanje(App.Stanje.Osnovno);

                //ako sam usao sa servisne knjizice i ako je vec odabran partner
                if (servisnaKnjizicaDetaljno != null && servisnaKnjizicaDetaljno.textBoxPartner.Text.Trim() != "")
                {
                    try
                    {
                        //int _fizickoLiceID = Convert.ToInt32(servisnaKnjizicaDetaljno.textBoxPartner.Tag);

                        //foreach (DB.FizickoLice item in listViewFizickoLice.Items)
                        //{
                        //    if (item.FizickoLiceID.Equals(_fizickoLiceID))
                        //    {
                        //        listViewFizickoLice.SelectedItem = item;
                        //        break;
                        //    }
                        //}

                        listViewFizickoLice.ItemsSource = dBProksi.NadjiFizickaLica("ID", servisnaKnjizicaDetaljno.textBoxPartner.Tag.ToString());

                        if (!listViewFizickoLice.Items.Count.Equals(0))
                        {
                            listViewFizickoLice.SelectedIndex = 0;
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

            Window.GetWindow(this).Title = "Lav - FizickoLice";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            FizickoLiceDetaljno _fizickoLiceDetaljno = new FizickoLiceDetaljno(this, false, servisnaKnjizicaDetaljno != null ? false : true);
            //_fizickoLiceDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _fizickoLiceDetaljno.Owner = Window.GetWindow(this);
            _fizickoLiceDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _fizickoLiceDetaljno.ShowDialog();

        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.FizickoLice> _fizickaLica = (ObservableCollection<DB.FizickoLice>)listViewFizickoLice.ItemsSource;
            DB.FizickoLice _fizickoLice = (DB.FizickoLice)listViewFizickoLice.SelectedItem;

            if (_fizickaLica != null && _fizickoLice != null)
            {
                FizickoLiceDetaljno _fizickoLiceDetaljno = new FizickoLiceDetaljno(this, true, servisnaKnjizicaDetaljno != null ? false : true);
                //_fizickoLiceDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _fizickoLiceDetaljno.Owner = Window.GetWindow(this);
                _fizickoLiceDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _fizickoLiceDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiFizickoLice((DB.FizickoLice)listViewFizickoLice.SelectedItem);
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
                    if (comboBoxFizickoLiceKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.FizickoLice> _fizicakoLica = (ObservableCollection<DB.FizickoLice>)listViewFizickoLice.ItemsSource;

                        listViewFizickoLice.ItemsSource = dBProksi.OsveziFizickaLica(_fizicakoLica);

                        if (!listViewFizickoLice.Items.Count.Equals(0))
                        {
                            listViewFizickoLice.SelectedIndex = 0;
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
                        listViewFizickoLice.ItemsSource = dBProksi.NadjiFizickaLica(((ComboBoxItem)comboBoxFizickoLiceKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewFizickoLice.Items.Count.Equals(0))
                        {
                            listViewFizickoLice.SelectedIndex = 0;
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
                DB.FizickoLice _trenutni = (DB.FizickoLice)listViewFizickoLice.SelectedItem;

                ObservableCollection<DB.FizickoLice> _fizickaLica = (ObservableCollection<DB.FizickoLice>)listViewFizickoLice.ItemsSource;

                if (!_fizickaLica.Count.Equals(0))
                {
                    listViewFizickoLice.ItemsSource = dBProksi.OsveziFizickaLica(_fizickaLica);

                    if (_trenutni != null)
                    {
                        SelektujFizickoLice(_trenutni.FizickoLiceID);
                    }
                    if (listViewFizickoLice.Items.Count.Equals(0))
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
                DB.FizickoLice _trenutni = (DB.FizickoLice)listViewFizickoLice.SelectedItem;
                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {
                    DajSve();
                }
                else if (comboBoxFizickoLiceKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewFizickoLice.ItemsSource = dBProksi.NadjiFizickaLica(((ComboBoxItem)comboBoxFizickoLiceKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewFizickoLice.Items.Count.Equals(0))
                    {
                        listViewFizickoLice.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujFizickoLice(_trenutni.FizickoLiceID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (servisnaKnjizicaDetaljno == null)
            {
                //ObservableCollection<DB.FizickoLice> _fizickaLica = (ObservableCollection<DB.FizickoLice>)listViewFizickoLice.ItemsSource;
                //DB.FizickoLice _fizickoLice = (DB.FizickoLice)listViewFizickoLice.SelectedItem;

                //if (_fizickaLica != null && _fizickoLice != null)
                //{
                FizickoLiceDetaljno _fizickoLiceDetaljno = new FizickoLiceDetaljno(this, true, servisnaKnjizicaDetaljno != null ? false : true);
                //_fizickoLiceDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _fizickoLiceDetaljno.Owner = Window.GetWindow(this);
                _fizickoLiceDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _fizickoLiceDetaljno.ShowDialog();
                //}
            }
            else
            {
                DB.FizickoLice _fizickoLice = (DB.FizickoLice)listViewFizickoLice.SelectedItem;

                servisnaKnjizicaDetaljno.textBoxPartner.Text = _fizickoLice.Ime;
                servisnaKnjizicaDetaljno.textBoxPartner.Tag = _fizickoLice.FizickoLiceID;

                Window.GetWindow(this).Close();
            }
        }

    }
}
