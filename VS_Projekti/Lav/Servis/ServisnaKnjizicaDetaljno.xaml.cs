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
using System.Windows.Shapes;
using System.Collections.ObjectModel;
using System.Windows.Navigation;

namespace Servis
{
    /// <summary>
    /// Interaction logic for ServisnaKnjizicaDetaljno.xaml
    /// </summary>
    public partial class ServisnaKnjizicaDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.ServisnaKnjizica servisnaKnjizica;

        public ServisnaKnjizicaDetaljno(Servis.ServisnaKnjizica servisnaKnjizica, bool izmeniTrenutni, bool dozvoliNavigaciju)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.servisnaKnjizica = servisnaKnjizica;

            if (izmeniTrenutni)
            {
                gridServisnaKnjizica.DataContext = (DB.ServisnaKnjizica)this.servisnaKnjizica.listViewServisnaKnjizica.SelectedItem;
                stanje = App.Stanje.Izmena;
            }
            else
            {
                stanje = App.Stanje.Unos;
            }

            buttonPonuda.Visibility = dozvoliNavigaciju == false ? Visibility.Hidden : Visibility.Visible;
            buttonRadniNalog.Visibility = dozvoliNavigaciju == false ? Visibility.Hidden : Visibility.Visible;
        }

        public bool Sacuvaj()
        {
            try
            {
                int _godiste;
                int _kilometraza;

                if ((bool)checkBoxGenerisiSifru.IsChecked && textBoxSifra.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Šifra.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxPartner.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi partnera.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxTip.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi tip.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxGodiste.Text.Trim() != "" && !Int32.TryParse(textBoxGodiste.Text, out _godiste))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Godište.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxKilometraza.Text.Trim() != "" && !Int32.TryParse(textBoxKilometraza.Text, out _kilometraza))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Kilometraža.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else
                {
                    if (stanje == App.Stanje.Unos)
                    {
                        DB.ServisnaKnjizica _servisnaKnjizica = new DB.ServisnaKnjizica
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            BrojSasije = textBoxBrojSasije.Text.Trim() == "" ? null : textBoxBrojSasije.Text.Trim(),
                            BrojMotora = textBoxBrojMotora.Text.Trim() == "" ? null : textBoxBrojMotora.Text.Trim(),
                            //RegistarskiBroj = textBoxRegistarskiBroj.Text.Trim() == "" ? null : textBoxRegistarskiBroj.Text.Trim(),
                            RegistarskiBroj = Klase.RegistarskiBroj.Odmaskiraj(textBoxRegistarskiBroj.Text.Trim()) == "" ? null : Klase.RegistarskiBroj.Odmaskiraj(textBoxRegistarskiBroj.Text.Trim()).ToUpper(),
                            DimenzijaGuma = textBoxDimenzijaGuma.Text.Trim() == "" ? null : textBoxDimenzijaGuma.Text.Trim(),
                            DatumRegistracije = datePickerDatumRegistracije.SelectedDate == null ? null : datePickerDatumRegistracije.SelectedDate,
                            ABS = (bool)checkBoxABS.IsChecked,
                            PS = (bool)checkBoxPS.IsChecked,
                            AC = (bool)checkBoxAC.IsChecked,
                            Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                        };
                        if (textBoxGodiste.Text.Trim() != "")
                        {
                            _servisnaKnjizica.Godiste = Convert.ToInt32(textBoxGodiste.Text.Trim());
                        }
                        if (textBoxKilometraza.Text.Trim() != "")
                        {
                            _servisnaKnjizica.Kilometraza = Convert.ToInt32(textBoxKilometraza.Text.Trim());
                        }

                        if ((bool)this.servisnaKnjizica.radioButtonFizickoLice.IsChecked)
                        {
                            _servisnaKnjizica.FizickoLiceID = Convert.ToInt32(textBoxPartner.Tag);
                        }
                        else //if ((bool)this.servisnaKnjizica.radioButtonPoslovniPartner.IsChecked)
                        {
                            _servisnaKnjizica.PoslovniPartnerID = Convert.ToInt32(textBoxPartner.Tag);
                        }

                        _servisnaKnjizica.TipAutomobilaID = Convert.ToInt32(textBoxTip.Tag);

                        dBProksi.UnesiServisnaKnjizica(_servisnaKnjizica);

                        ObservableCollection<DB.ServisnaKnjizica> _servisnaKnjizice = (ObservableCollection<DB.ServisnaKnjizica>)servisnaKnjizica.listViewServisnaKnjizica.ItemsSource;
                        _servisnaKnjizice.Add(_servisnaKnjizica);
                        servisnaKnjizica.listViewServisnaKnjizica.SelectedItem = _servisnaKnjizica;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.ServisnaKnjizica _servisnaKnjizica = new DB.ServisnaKnjizica
                        {
                            ServisnaKnjizicaID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            BrojSasije = textBoxBrojSasije.Text.Trim() == "" ? null : textBoxBrojSasije.Text.Trim(),
                            BrojMotora = textBoxBrojMotora.Text.Trim() == "" ? null : textBoxBrojMotora.Text.Trim(),
                            //RegistarskiBroj = textBoxRegistarskiBroj.Text.Trim() == "" ? null : textBoxRegistarskiBroj.Text.Trim(),
                            RegistarskiBroj = Klase.RegistarskiBroj.Odmaskiraj(textBoxRegistarskiBroj.Text.Trim()) == "" ? null : Klase.RegistarskiBroj.Odmaskiraj(textBoxRegistarskiBroj.Text.Trim()).ToUpper(),
                            DimenzijaGuma = textBoxDimenzijaGuma.Text.Trim() == "" ? null : textBoxDimenzijaGuma.Text.Trim(),
                            DatumRegistracije = datePickerDatumRegistracije.SelectedDate == null ? null : datePickerDatumRegistracije.SelectedDate,
                            ABS = (bool)checkBoxABS.IsChecked,
                            PS = (bool)checkBoxPS.IsChecked,
                            AC = (bool)checkBoxAC.IsChecked,
                            Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                        };

                        if (textBoxGodiste.Text.Trim() != "")
                        {
                            _servisnaKnjizica.Godiste = Convert.ToInt32(textBoxGodiste.Text.Trim());
                        }
                        if (textBoxKilometraza.Text.Trim() != "")
                        {
                            _servisnaKnjizica.Kilometraza = Convert.ToInt32(textBoxKilometraza.Text.Trim());
                        }

                        if ((bool)this.servisnaKnjizica.radioButtonFizickoLice.IsChecked)
                        {
                            _servisnaKnjizica.FizickoLiceID = Convert.ToInt32(textBoxPartner.Tag);
                        }
                        else //if ((bool)this.servisnaKnjizica.radioButtonPoslovniPartner.IsChecked)
                        {
                            _servisnaKnjizica.PoslovniPartnerID = Convert.ToInt32(textBoxPartner.Tag);
                        }

                        _servisnaKnjizica.TipAutomobilaID = Convert.ToInt32(textBoxTip.Tag);

                        
                        dBProksi.IzmeniServisnaKnjizica(_servisnaKnjizica, (DB.ServisnaKnjizica)gridServisnaKnjizica.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujServisnaKnjizica(int servisnaKnjizicaID)
        {
            ObservableCollection<DB.ServisnaKnjizica> _servisneKnjizice = (ObservableCollection<DB.ServisnaKnjizica>)servisnaKnjizica.listViewServisnaKnjizica.ItemsSource;
            bool _postoji = false;

            if (!servisnaKnjizica.listViewServisnaKnjizica.Items.Count.Equals(0))
            {
                foreach (DB.ServisnaKnjizica item in _servisneKnjizice)
                {
                    if (item.ServisnaKnjizicaID.Equals(servisnaKnjizicaID))
                    {
                        servisnaKnjizica.listViewServisnaKnjizica.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    servisnaKnjizica.listViewServisnaKnjizica.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.ServisnaKnjizica _trenutni = (DB.ServisnaKnjizica)servisnaKnjizica.listViewServisnaKnjizica.SelectedItem;

                ObservableCollection<DB.ServisnaKnjizica> _servisneKnjizice = (ObservableCollection<DB.ServisnaKnjizica>)servisnaKnjizica.listViewServisnaKnjizica.ItemsSource;

                if (!_servisneKnjizice.Count.Equals(0))
                {
                    servisnaKnjizica.listViewServisnaKnjizica.ItemsSource = dBProksi.OsveziServisnaKnjizica(_servisneKnjizice);

                    if (_trenutni != null)
                    {
                        SelektujServisnaKnjizica(_trenutni.ServisnaKnjizicaID);
                    }
                }
                gridServisnaKnjizica.DataContext = (DB.ServisnaKnjizica)servisnaKnjizica.listViewServisnaKnjizica.SelectedItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void buttonSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {
                    Osvezi();

                    stanje = App.Stanje.Izmena;
                    servisnaKnjizica.UStanje(App.Stanje.Detaljno);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSacuvajINovi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {
                    Osvezi();
                    gridServisnaKnjizica.DataContext = null;

                    stanje = App.Stanje.Unos;
                    servisnaKnjizica.UStanje(App.Stanje.Detaljno);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSacuvajIZatvori_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {
                    Osvezi();

                    servisnaKnjizica.UStanje(App.Stanje.Detaljno);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonTip_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow _naw = new NavigationWindow();
            _naw.Content = new Vozilo(this);
            //sakrijem strelice za nazed i napred
            _naw.ShowsNavigationUI = false;
            _naw.Owner = Window.GetWindow(this);
            _naw.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (Window.GetWindow(servisnaKnjizica).WindowState == WindowState.Normal)
            {
                _naw.Width = Window.GetWindow(servisnaKnjizica).ActualWidth;
                _naw.Height = Window.GetWindow(servisnaKnjizica).ActualHeight;
            }
            else
            {
                _naw.WindowState = Window.GetWindow(servisnaKnjizica).WindowState;
            }

            _naw.ShowDialog();
        }

        private void textBoxTip_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonPartner_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow _naw = new NavigationWindow();

            if ((bool)servisnaKnjizica.radioButtonFizickoLice.IsChecked)
            {
                _naw.Content = new FizickoLice(this);
            }
            else if ((bool)servisnaKnjizica.radioButtonPoslovniPartner.IsChecked)
            {
                _naw.Content = new PoslovniPartner(this);
            }

            //sakrijem strelice za nazed i napred
            _naw.ShowsNavigationUI = false;
            _naw.Owner = Window.GetWindow(this);
            _naw.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (Window.GetWindow(servisnaKnjizica).WindowState == WindowState.Normal)
            {
                _naw.Width = Window.GetWindow(servisnaKnjizica).ActualWidth;
                _naw.Height = Window.GetWindow(servisnaKnjizica).ActualHeight;
            }
            else
            {
                _naw.WindowState = Window.GetWindow(servisnaKnjizica).WindowState;
            }


            _naw.ShowDialog();
        }

        private void textBoxPartner_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //buttonPartner_Click(null, null);
        }

        private void buttonPonuda_Click(object sender, RoutedEventArgs e)
        {
            
            if (!textBoxID.Text.Trim().Equals(""))
            {
                DB.ServisnaKnjizica _servisnaKnjizica = (DB.ServisnaKnjizica)gridServisnaKnjizica.DataContext;

                Pocetna _pocetna = (Pocetna)Window.GetWindow(this.servisnaKnjizica).FindName("windowPocetna");

                _pocetna.PrikaziFormu("Ponuda.xaml");


                App.PonudaPartnerID = (bool)this.servisnaKnjizica.radioButtonPoslovniPartner.IsChecked ? (int)_servisnaKnjizica.PoslovniPartnerID : (int)_servisnaKnjizica.FizickoLiceID;
                App.PonudaVrstaPartnera = (bool)this.servisnaKnjizica.radioButtonPoslovniPartner.IsChecked ? "PoslovniPartner" : "FizickoLice";
                App.PonudaServisnaKnjizicaID = Convert.ToInt32(textBoxID.Text.Trim());
                this.Close();
            }
        }

        private void buttonRadniNalog_Click(object sender, RoutedEventArgs e)
        {
            if (!textBoxID.Text.Trim().Equals(""))
            {
                DB.ServisnaKnjizica _servisnaKnjizica = (DB.ServisnaKnjizica)gridServisnaKnjizica.DataContext;

                Pocetna _pocetna = (Pocetna)Window.GetWindow(this.servisnaKnjizica).FindName("windowPocetna");

                _pocetna.PrikaziFormu("RadniNalog.xaml");


                App.RadniNalogPartnerID = (bool)this.servisnaKnjizica.radioButtonPoslovniPartner.IsChecked ? (int)_servisnaKnjizica.PoslovniPartnerID : (int)_servisnaKnjizica.FizickoLiceID;
                App.RadniNalogVrstaPartnera = (bool)this.servisnaKnjizica.radioButtonPoslovniPartner.IsChecked ? "PoslovniPartner" : "FizickoLice";
                App.RadniNalogServisnaKnjizicaID = Convert.ToInt32(textBoxID.Text.Trim());
                this.Close();
            }
        }




    }
}
