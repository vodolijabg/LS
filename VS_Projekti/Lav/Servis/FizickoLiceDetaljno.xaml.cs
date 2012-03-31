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
using System.Text.RegularExpressions;

namespace Servis
{
    /// <summary>
    /// Interaction logic for FizickoLiceDetaljno.xaml
    /// </summary>
    public partial class FizickoLiceDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.FizickoLice fizickoLice;

        private bool DaLiJeIspravanTelefonskiBroj(string mobilniBroj)
        {
           
            //string _dozvoljeniKarakteri = "0123456789";
            //bool _ispravnaPoruka = true;

            //if (mobilniBroj != "" && mobilniBroj.StartsWith("0") && mobilniBroj.Length >= 9 && mobilniBroj.Length <= 11)
            //{
            //    for (int i = 0; i < mobilniBroj.Length; i++)
            //    {
            //        if (!_dozvoljeniKarakteri.Contains(mobilniBroj[i].ToString()))
            //        {
            //            _ispravnaPoruka = false;
            //            break;
            //        }
            //    }
            //}
            //else
            //{
            //    _ispravnaPoruka = false;
            //}

            //return _ispravnaPoruka;

            return true;
        }

        public FizickoLiceDetaljno(DB.FizickoLice fizickoLice)
        {
            InitializeComponent();

            try
            {
                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                ObservableCollection<DB.Mesto> _mesta = new ObservableCollection<DB.Mesto>(dBProksi.DajSvaMesta().ToList());

                if (!_mesta.Count.Equals(0))
                {
                    _mesta.Insert(0, new DB.Mesto());
                }

                comboBoxMesto.ItemsSource = _mesta.OrderBy(m => m.Naziv);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            gridFizickoLice.DataContext = fizickoLice;

            //stvarno ne znam sto nece da sam selektuje mesto pa moram ovako (vidi binding za SelectetItem)
            foreach (DB.Mesto item in comboBoxMesto.Items)
            {
                if (item.MestoID == ((DB.FizickoLice)gridFizickoLice.DataContext).MestoID)
                {
                    comboBoxMesto.SelectedItem = item;
                    break;
                }
            }

            buttonSacuvaj.Visibility = Visibility.Collapsed;
            buttonSacuvajINovi.Visibility = Visibility.Collapsed;
            buttonSacuvajIZatvori.Visibility = Visibility.Collapsed;
            buttonServisnaKnjizica.Visibility = Visibility.Collapsed;
            buttonPonuda.Visibility = Visibility.Collapsed;
            buttonRadniNalog.Visibility = Visibility.Collapsed;



        }

        public FizickoLiceDetaljno(Servis.FizickoLice fizickoLice, bool izmeniTrenutni, bool dozvoliNavigaciju)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.fizickoLice = fizickoLice;
            try
            {
                ObservableCollection<DB.Mesto> _mesta = new ObservableCollection<DB.Mesto>(dBProksi.DajSvaMesta().ToList());

                if (!_mesta.Count.Equals(0))
                {
                    _mesta.Insert(0, new DB.Mesto());
                }

                comboBoxMesto.ItemsSource = _mesta.OrderBy(m => m.Naziv);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (izmeniTrenutni)
            {
                gridFizickoLice.DataContext = (DB.FizickoLice)fizickoLice.listViewFizickoLice.SelectedItem;

                //stvarno ne znam sto nece da sam selektuje mesto pa moram ovako (vidi binding za SelectetItem)
                foreach (DB.Mesto item in comboBoxMesto.Items)
                {
                    if (item.MestoID == ((DB.FizickoLice)gridFizickoLice.DataContext).MestoID)
                    {
                        comboBoxMesto.SelectedItem = item;
                        break;
                    }
                }

                stanje = App.Stanje.Izmena;
            }
            else
            {
                stanje = App.Stanje.Unos;
            }

            buttonServisnaKnjizica.Visibility = dozvoliNavigaciju == false ? Visibility.Hidden : Visibility.Visible;
            buttonPonuda.Visibility = dozvoliNavigaciju == false ? Visibility.Hidden : Visibility.Visible;
            buttonRadniNalog.Visibility = dozvoliNavigaciju == false ? Visibility.Hidden : Visibility.Visible;



        }

        public bool Sacuvaj()
        {          
            try
            {
                if ((bool)checkBoxGenerisiSifru.IsChecked && textBoxSifra.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Šifra.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxIme.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Ime.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()).Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Telefon.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                //else if (!DaLiJeIspravanTelefonskiBroj(Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim())))
                //{
                //    Dijalog _dialog = new Dijalog("Neispravan format", "Broj telefona može sadržati samo brojeve, \nmora pocinjati sa nulom \ni imati najmanje 9 a najviše 11 cifara. \n\nUNOŠENJE BROJA U ISPRAVNOM FORMATU JE VEOMA VAŽNO!!!\nZa svako fizičko lice unesite samo jedan broj telefona.");
                //    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                //    _dialog.Owner = Window.GetWindow(this);
                //    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                //    _dialog.ShowDialog();
                //    return false;
                //}
                else
                {
                    if (stanje == App.Stanje.Unos)
                    {
                        DB.FizickoLice _fizickoLice = new DB.FizickoLice
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Ime = textBoxIme.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxIme.Text.Trim()),
                            Prezime = textBoxPrezime.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxPrezime.Text.Trim()),
                            RegistrovanKupac = (bool)checkBoxRegistrovanKupac.IsChecked,
                            Adresa = textBoxAdresa.Text.Trim() == "" ? null : textBoxAdresa.Text.Trim(),
                            Telefon1 = Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()) == "" ? null : Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()),
                            Telefon2 = Klase.Telefon.Odmaskiraj(textBoxTelefon2.Text.Trim()) == "" ? null : Klase.Telefon.Odmaskiraj(textBoxTelefon2.Text.Trim()),
                            EMail = textBoxEMail.Text.Trim() == "" ? null : textBoxEMail.Text.Trim(),
                        };
                        if (comboBoxMesto.SelectedItem != null)
                        {
                            DB.Mesto _mesto = (DB.Mesto)comboBoxMesto.SelectedItem;

                            if (_mesto.Naziv == null)
                            {
                                _fizickoLice.Mesto = null;
                            }
                            else
                            {
                                _fizickoLice.MestoID = _mesto.MestoID;
                            }
                        }


                        dBProksi.UnesiFizickoLice(_fizickoLice);

                        ObservableCollection<DB.FizickoLice> _fizickaLica = (ObservableCollection<DB.FizickoLice>)fizickoLice.listViewFizickoLice.ItemsSource;
                        _fizickaLica.Add(_fizickoLice);
                        fizickoLice.listViewFizickoLice.SelectedItem = _fizickoLice;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.FizickoLice _fizickoLice = new DB.FizickoLice
                        {
                            FizickoLiceID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Ime = textBoxIme.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxIme.Text.Trim()),
                            Prezime = textBoxPrezime.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxPrezime.Text.Trim()),
                            RegistrovanKupac = (bool)checkBoxRegistrovanKupac.IsChecked,
                            Adresa = textBoxAdresa.Text.Trim() == "" ? null : textBoxAdresa.Text.Trim(),
                            Telefon1 = Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()) == "" ? null : Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()),
                            Telefon2 = Klase.Telefon.Odmaskiraj(textBoxTelefon2.Text.Trim()) == "" ? null : Klase.Telefon.Odmaskiraj(textBoxTelefon2.Text.Trim()),
                            EMail = textBoxEMail.Text.Trim() == "" ? null : textBoxEMail.Text.Trim(),
                        };
                        if (comboBoxMesto.SelectedItem != null)
                        {
                            DB.Mesto _mesto = (DB.Mesto)comboBoxMesto.SelectedItem;

                            if (_mesto.Naziv == null)
                            {
                                _fizickoLice.Mesto = null;
                            }
                            else
                            {
                                _fizickoLice.MestoID = _mesto.MestoID;
                            }
                        }

                        dBProksi.IzmeniFizickoLice(_fizickoLice, (DB.FizickoLice)gridFizickoLice.DataContext);
                    }
                }
        

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujFizickoLice(int fizickoLiceID)
        {
            ObservableCollection<DB.FizickoLice> _fizickaLica = (ObservableCollection<DB.FizickoLice>)fizickoLice.listViewFizickoLice.ItemsSource;
            bool _postoji = false;

            if (!fizickoLice.listViewFizickoLice.Items.Count.Equals(0))
            {
                foreach (DB.FizickoLice item in _fizickaLica)
                {
                    if (item.FizickoLiceID.Equals(fizickoLiceID))
                    {
                        fizickoLice.listViewFizickoLice.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    fizickoLice.listViewFizickoLice.SelectedIndex = 0;
                }
            }

        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.FizickoLice _trenutni = (DB.FizickoLice)fizickoLice.listViewFizickoLice.SelectedItem;

                ObservableCollection<DB.FizickoLice> _fizickaLica = (ObservableCollection<DB.FizickoLice>)fizickoLice.listViewFizickoLice.ItemsSource;

                if (!_fizickaLica.Count.Equals(0))
                {
                    fizickoLice.listViewFizickoLice.ItemsSource = dBProksi.OsveziFizickaLica(_fizickaLica);

                    if (_trenutni != null)
                    {
                        SelektujFizickoLice(_trenutni.FizickoLiceID);
                    }
                }
                gridFizickoLice.DataContext = (DB.FizickoLice)fizickoLice.listViewFizickoLice.SelectedItem;
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
                    fizickoLice.UStanje(App.Stanje.Detaljno);
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

                    gridFizickoLice.DataContext = null;
                    stanje = App.Stanje.Unos;
                    fizickoLice.UStanje(App.Stanje.Detaljno);
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

                    fizickoLice.UStanje(App.Stanje.Detaljno);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonServisnaKnjizica_Click(object sender, RoutedEventArgs e)
        {
            if (!textBoxID.Text.Trim().Equals(""))
            {
                Pocetna _pocetna = (Pocetna)Window.GetWindow(this.fizickoLice).FindName("windowPocetna");

                _pocetna.PrikaziFormu("ServisnaKnjizica.xaml");

                App.ServisnaKnjizicaPartnerID = Convert.ToInt32(textBoxID.Text);
                App.ServisnaKnjizicaVrstaPartnera = "FizickoLice";
                this.Close();
            }
        }

        private void buttonPonuda_Click(object sender, RoutedEventArgs e)
        {
            if (!textBoxID.Text.Trim().Equals(""))
            {
                Pocetna _pocetna = (Pocetna)Window.GetWindow(this.fizickoLice).FindName("windowPocetna");

                _pocetna.PrikaziFormu("Ponuda.xaml");

                App.PonudaPartnerID = Convert.ToInt32(textBoxID.Text);
                App.PonudaVrstaPartnera = "FizickoLice";
                this.Close();
            }
        }

        private void buttonRadniNalog_Click(object sender, RoutedEventArgs e)
        {
            if (!textBoxID.Text.Trim().Equals(""))
            {
                Pocetna _pocetna = (Pocetna)Window.GetWindow(this.fizickoLice).FindName("windowPocetna");

                _pocetna.PrikaziFormu("RadniNalog.xaml");

                App.RadniNalogPartnerID = Convert.ToInt32(textBoxID.Text);
                App.RadniNalogVrstaPartnera = "FizickoLice";
                this.Close();
            }
        }

        private void buttonTest_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text));
        }

        private void textBoxTelefon_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            ObservableCollection<DB.FizickoLice> fizickoLiceLista = new ObservableCollection<DB.FizickoLice>();

            if (tb.Name == textBoxTelefon1.Name && Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()) != "")
            {
                fizickoLiceLista = dBProksi.NadjiFizickaLicaZaTelefon(Klase.Telefon.Odmaskiraj(textBoxTelefon1.Text.Trim()));
            }
            else if (tb.Name == textBoxTelefon2.Name && Klase.Telefon.Odmaskiraj(textBoxTelefon2.Text.Trim()) != "")
            {
                fizickoLiceLista = dBProksi.NadjiFizickaLicaZaTelefon(Klase.Telefon.Odmaskiraj(textBoxTelefon2.Text.Trim()));
            }

            if (fizickoLiceLista.Count > 0)
            {
                StringBuilder _sb = new StringBuilder();                

                foreach (DB.FizickoLice item in fizickoLiceLista)
                {
                    if (textBoxID.Text != "")
                    {
                        if (item.FizickoLiceID.ToString() == textBoxID.Text)
                        {
                            break;
                        }
                    }
                    string _korisnik = item.Prezime != "" && item.Prezime != null ?  item.Ime + " " + item.Prezime : item.Ime;
                    _sb.Append( _korisnik + " sa ID = " + item.FizickoLiceID.ToString() + "\n");
                }

                if (_sb.Length > 0)
                {
                    _sb.Insert(0, "Broj telefona koji ste uneli već postoji i to kod \n\n");

                    MessageBoxResult _result = MessageBox.Show(_sb.ToString(), "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);

                }
            }
            
        }

    }
}
