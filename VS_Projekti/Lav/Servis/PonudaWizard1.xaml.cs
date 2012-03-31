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
    /// Interaction logic for PonudaWizard1.xaml
    /// </summary>
    public partial class PonudaWizard1 : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;
        DB.DBProksi dBProksi;
        PonudaWizard ponudaWizard;

        public PonudaWizard1(PonudaWizard ponudaWizard)
        {
            InitializeComponent();

            this.ponudaWizard = ponudaWizard;
        }

        private bool DaLiJeIspravanTelefonskiBroj(string mobilniBroj)
        {
            string _dozvoljeniKarakteri = "0123456789";
            bool _ispravnaPoruka = true;

            if (mobilniBroj != "" && mobilniBroj.StartsWith("0") && mobilniBroj.Length >= 9 && mobilniBroj.Length <= 11)
            {
                for (int i = 0; i < mobilniBroj.Length; i++)
                {
                    if (!_dozvoljeniKarakteri.Contains(mobilniBroj[i].ToString()))
                    {
                        _ispravnaPoruka = false;
                        break;
                    }
                }
            }
            else
            {
                _ispravnaPoruka = false;
            }

            return _ispravnaPoruka;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                gridFizickoLice.DataContext = App.fizickoLicePonudaWizard;

                prvoOtvaranjeStrane = false;

                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                try
                {
                    ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = new ObservableCollection<DB.NacinZahtevaZaPonudu>(dBProksi.DajSveNacinZahtevaZaPonudu().ToList());

                    comboBoxNacinZahtevaZaPonudu.ItemsSource = _naciniZahtevaZaPonudu;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }        
        
        private void buttonOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void buttonDalje_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxIme.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Ime.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else if (textBoxTelefon1.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Telefon.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                //else if (!DaLiJeIspravanTelefonskiBroj(textBoxTelefon1.Text.Trim()))
                //{
                //    Dijalog _dialog = new Dijalog("Neispravan format", "Broj telefona može sadržati samo brojeve, \nmora pocinjati sa nulom \ni imati najmanje 9 a najviše 11 cifara. \n\nUNOŠENJE BROJA U ISPRAVNOM FORMATU JE VEOMA VAŽNO!!!\nZa svako fizičko lice unesite samo jedan broj telefona.");
                //    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                //    _dialog.Owner = Window.GetWindow(this);
                //    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                //    _dialog.ShowDialog();
                //}
                else if (comboBoxNacinZahtevaZaPonudu.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi Način zahteva za ponudu.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else
                {
                    App.nacinZahtevaZaPonuduWizard = (DB.NacinZahtevaZaPonudu)comboBoxNacinZahtevaZaPonudu.SelectedItem;

                    App.fizickoLicePonudaWizard.Ime = Helper.DajStringSaVelikimPocetnimSlovom(textBoxIme.Text.Trim());
                    App.fizickoLicePonudaWizard.Prezime = textBoxPrezime.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxPrezime.Text.Trim());
                    App.fizickoLicePonudaWizard.Telefon1 = Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim());

                    ObservableCollection<DB.FizickoLice> fizickoLiceLista = dBProksi.DajFizickoLice(Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()));

                    DB.FizickoLice _fizickoLice;

                    if (fizickoLiceLista.Count.Equals(0))
                    {
                        App.fizickoLicePonudaWizard.Ime = Helper.DajStringSaVelikimPocetnimSlovom(textBoxIme.Text.Trim());
                        App.fizickoLicePonudaWizard.Prezime = textBoxPrezime.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxPrezime.Text.Trim());
                        App.fizickoLicePonudaWizard.Telefon1 = Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim());
                        dBProksi.UnesiFizickoLice(App.fizickoLicePonudaWizard);

                        this.NavigationService.Navigate(new PonudaWizard2(ponudaWizard));
                        PonudaWizard2 _ponudaWizard2 = new PonudaWizard2(ponudaWizard);
                        _ponudaWizard2.Return += new ReturnEventHandler<string>(_ponudaWizard2_Return);
                        this.NavigationService.Navigate(_ponudaWizard2);
                    }
                    else if (fizickoLiceLista.Count.Equals(1))
                    {
                        _fizickoLice = new DB.FizickoLice
                        {
                            FizickoLiceID = fizickoLiceLista.First().FizickoLiceID,
                            Sifra = fizickoLiceLista.First().Sifra,
                            Ime = Helper.DajStringSaVelikimPocetnimSlovom(textBoxIme.Text.Trim()), //fizickoLiceLista.First().Ime,
                            Prezime = textBoxPrezime.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxPrezime.Text.Trim()),
                            RegistrovanKupac = fizickoLiceLista.First().RegistrovanKupac,
                            MestoID = fizickoLiceLista.First().MestoID,
                            Adresa = fizickoLiceLista.First().Adresa,
                            Telefon1 = Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()), // fizickoLiceLista.First().Telefon,
                            EMail = fizickoLiceLista.First().EMail
                        };

                        //App.fizickoLicePonudaWizard.FizickoLiceID = _fizickoLice.FizickoLiceID;
                        //App.fizickoLicePonudaWizard.Sifra = _fizickoLice.Sifra;
                        //App.fizickoLicePonudaWizard.Ime = _fizickoLice.Ime;
                        //App.fizickoLicePonudaWizard.Prezime = _fizickoLice.Prezime;
                        //App.fizickoLicePonudaWizard.RegistrovanKupac = _fizickoLice.RegistrovanKupac;
                        //App.fizickoLicePonudaWizard.MestoID = _fizickoLice.MestoID;
                        //App.fizickoLicePonudaWizard.Adresa = _fizickoLice.Adresa;
                        //App.fizickoLicePonudaWizard.Telefon = _fizickoLice.Telefon;
                        //App.fizickoLicePonudaWizard.EMail = _fizickoLice.EMail;

                        if (App.fizickoLicePonudaWizard.FizickoLiceID == fizickoLiceLista.First().FizickoLiceID) 
                        {
                            //identicni su
                            if (App.fizickoLicePonudaWizard.Ime == fizickoLiceLista.First().Ime)
                            {
                                this.NavigationService.Navigate(new PonudaWizard2(ponudaWizard));
                                PonudaWizard2 _ponudaWizard2 = new PonudaWizard2(ponudaWizard);
                                _ponudaWizard2.Return += new ReturnEventHandler<string>(_ponudaWizard2_Return);
                                this.NavigationService.Navigate(_ponudaWizard2);
                            }
                            //izmeni ime postojecem i NASTAVI da ga koristis
                            else if (App.fizickoLicePonudaWizard.Ime != fizickoLiceLista.First().Ime)
                            {
                                MessageBoxResult _rezultat = MessageBox.Show("U bazi postoji fizičko lice " + fizickoLiceLista.First().Ime + " sa istim brojem telefona." +
                                                                                "\nDa promenite ime postojećem i nastavite dalje odaberite Yes, da odustanete odaberite No.",
                                                                                    "Upozorenje",
                                                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                                if (_rezultat == MessageBoxResult.Yes)
                                {
                                    dBProksi.IzmeniFizickoLice(_fizickoLice, fizickoLiceLista.First());

                                    App.fizickoLicePonudaWizard.FizickoLiceID = _fizickoLice.FizickoLiceID;
                                    App.fizickoLicePonudaWizard.Sifra = _fizickoLice.Sifra;
                                    App.fizickoLicePonudaWizard.Ime = _fizickoLice.Ime;
                                    App.fizickoLicePonudaWizard.Prezime = _fizickoLice.Prezime;
                                    App.fizickoLicePonudaWizard.RegistrovanKupac = _fizickoLice.RegistrovanKupac;
                                    App.fizickoLicePonudaWizard.MestoID = _fizickoLice.MestoID;
                                    App.fizickoLicePonudaWizard.Adresa = _fizickoLice.Adresa;
                                    App.fizickoLicePonudaWizard.Telefon1 = _fizickoLice.Telefon1;
                                    App.fizickoLicePonudaWizard.EMail = _fizickoLice.EMail;

                                    this.NavigationService.Navigate(new PonudaWizard2(ponudaWizard));
                                    PonudaWizard2 _ponudaWizard2 = new PonudaWizard2(ponudaWizard);
                                    _ponudaWizard2.Return += new ReturnEventHandler<string>(_ponudaWizard2_Return);
                                    this.NavigationService.Navigate(_ponudaWizard2);
                                }
                            }
                        }
                        else if (App.fizickoLicePonudaWizard.FizickoLiceID != fizickoLiceLista.First().FizickoLiceID)
                        {
                            //koristi postojeceg
                            if (App.fizickoLicePonudaWizard.Ime == fizickoLiceLista.First().Ime)
                            {
                                MessageBoxResult _rezultat = MessageBox.Show("U bazi postoji fizičko lice sa istim imenom i brojem telefona." +
                                                                                   "\nDa koristite postojećeg korisnika odaberite Yes, da odustanete odaberite No.",
                                                                                       "Upozorenje",
                                                                                       MessageBoxButton.YesNo, MessageBoxImage.Question);
                                if (_rezultat == MessageBoxResult.Yes)
                                {
                                    App.fizickoLicePonudaWizard.FizickoLiceID = _fizickoLice.FizickoLiceID;
                                    App.fizickoLicePonudaWizard.Sifra = _fizickoLice.Sifra;
                                    App.fizickoLicePonudaWizard.Ime = _fizickoLice.Ime;
                                    App.fizickoLicePonudaWizard.Prezime = _fizickoLice.Prezime;
                                    App.fizickoLicePonudaWizard.RegistrovanKupac = _fizickoLice.RegistrovanKupac;
                                    App.fizickoLicePonudaWizard.MestoID = _fizickoLice.MestoID;
                                    App.fizickoLicePonudaWizard.Adresa = _fizickoLice.Adresa;
                                    App.fizickoLicePonudaWizard.Telefon1 = _fizickoLice.Telefon1;
                                    App.fizickoLicePonudaWizard.EMail = _fizickoLice.EMail;


                                    this.NavigationService.Navigate(new PonudaWizard2(ponudaWizard));
                                    PonudaWizard2 _ponudaWizard2 = new PonudaWizard2(ponudaWizard);
                                    _ponudaWizard2.Return += new ReturnEventHandler<string>(_ponudaWizard2_Return);
                                    this.NavigationService.Navigate(_ponudaWizard2);
                                }
                            }
                            //promeni ime postojecem i koristi ga
                            else if (App.fizickoLicePonudaWizard.Ime != fizickoLiceLista.First().Ime)
                            {
                                MessageBoxResult _rezultat = MessageBox.Show("U bazi postoji fizičko lice " + fizickoLiceLista.First().Ime + " sa istim brojem telefona." +
                                                                                "\nDa promenite ime postojećem i koristite ga odaberite Yes, da odustanete odaberite No.",
                                                                                    "Upozorenje",
                                                                                    MessageBoxButton.YesNo, MessageBoxImage.Question);

                                if (_rezultat == MessageBoxResult.Yes)
                                {                                  
                                    dBProksi.IzmeniFizickoLice(_fizickoLice, fizickoLiceLista.First());

                                    App.fizickoLicePonudaWizard.FizickoLiceID = _fizickoLice.FizickoLiceID;
                                    App.fizickoLicePonudaWizard.Sifra = _fizickoLice.Sifra;
                                    App.fizickoLicePonudaWizard.Ime = _fizickoLice.Ime;
                                    App.fizickoLicePonudaWizard.Prezime = _fizickoLice.Prezime;
                                    App.fizickoLicePonudaWizard.RegistrovanKupac = _fizickoLice.RegistrovanKupac;
                                    App.fizickoLicePonudaWizard.MestoID = _fizickoLice.MestoID;
                                    App.fizickoLicePonudaWizard.Adresa = _fizickoLice.Adresa;
                                    App.fizickoLicePonudaWizard.Telefon1 = _fizickoLice.Telefon1;
                                    App.fizickoLicePonudaWizard.EMail = _fizickoLice.EMail;

                                    this.NavigationService.Navigate(new PonudaWizard2(ponudaWizard));
                                    PonudaWizard2 _ponudaWizard2 = new PonudaWizard2(ponudaWizard);
                                    _ponudaWizard2.Return += new ReturnEventHandler<string>(_ponudaWizard2_Return);
                                    this.NavigationService.Navigate(_ponudaWizard2);
                                }
                            }
                        }
                    }
                    else if (fizickoLiceLista.Count > 1)
                    {
                        //nemoguc dogadjaj, Telefon je UC u bazi
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        void _ponudaWizard2_Return(object sender, ReturnEventArgs<string> e)
        {
            //throw new NotImplementedException();
        }

        private void textBoxTelefon_LostFocus(object sender, RoutedEventArgs e)
        {

        }
    }
}
