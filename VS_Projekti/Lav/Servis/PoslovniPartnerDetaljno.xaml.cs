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
    /// Interaction logic for PoslovniPartnerDetaljno.xaml
    /// </summary>
    public partial class PoslovniPartnerDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.PoslovniPartner poslovniPartner;
        Servis.Artikal artikal;

        public PoslovniPartnerDetaljno()
        {
            InitializeComponent();
        }

        public PoslovniPartnerDetaljno(Servis.PoslovniPartner poslovniPartner, bool izmeniTrenutni, bool dozvoliNavigaciju) : this()
        {
            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.poslovniPartner = poslovniPartner;


            try
            {
                ObservableCollection<DB.Mesto> _mesta = new ObservableCollection<DB.Mesto>(dBProksi.DajSvaMesta().ToList());

                if (!_mesta.Count.Equals(0))
                {
                    _mesta.Insert(0, new DB.Mesto());
                }

                comboBoxMesto.ItemsSource = _mesta.OrderBy(m => m.Naziv); ;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            try
            {
                ObservableCollection<DB.NacinOrganizacijeFirme> _naciniOrganizacijeFirme = new ObservableCollection<DB.NacinOrganizacijeFirme>(dBProksi.DajSveNacinOrganizacijeFirme().ToList());

                //if (!_naciniOrganizacijeFirme.Count.Equals(0))
                //{
                //    _naciniOrganizacijeFirme.Insert(0, new DB.NacinOrganizacijeFirme());
                //}

                comboBoxNacinOrganizacijeFirme.ItemsSource = _naciniOrganizacijeFirme;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (izmeniTrenutni)
            {
                gridPoslovniPartner.DataContext = (DB.PoslovniPartner)poslovniPartner.listViewPoslovniPartner.SelectedItem;

                //stvarno ne znam sto nece da sam selektuje mesto pa moram ovako (vidi binding za SelectetItem)
                foreach (DB.Mesto item in comboBoxMesto.Items)
                {
                    if (item.MestoID == ((DB.PoslovniPartner)gridPoslovniPartner.DataContext).MestoID)
                    {
                        comboBoxMesto.SelectedItem = item;
                        break;
                    }
                }
                foreach (DB.NacinOrganizacijeFirme item in comboBoxNacinOrganizacijeFirme.Items)
                {
                    if (item.NacinOrganizacijeFirmeID == ((DB.PoslovniPartner)gridPoslovniPartner.DataContext).NacinOrganizacijeFirmeID)
                    {
                        comboBoxNacinOrganizacijeFirme.SelectedItem = item;
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

        public PoslovniPartnerDetaljno(Servis.Artikal artikal, int poslovniPartnerID)
            : this()
        {
            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.artikal = artikal;

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
            try
            {
                ObservableCollection<DB.NacinOrganizacijeFirme> _naciniOrganizacijeFirme = new ObservableCollection<DB.NacinOrganizacijeFirme>(dBProksi.DajSveNacinOrganizacijeFirme().ToList());

                //if (!_naciniOrganizacijeFirme.Count.Equals(0))
                //{
                //    _naciniOrganizacijeFirme.Insert(0, new DB.NacinOrganizacijeFirme());
                //}

                comboBoxNacinOrganizacijeFirme.ItemsSource = _naciniOrganizacijeFirme;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            gridPoslovniPartner.DataContext = dBProksi.NadjiPoslovniPartner("ID", poslovniPartnerID.ToString()).First();

            //stvarno ne znam sto nece da sam selektuje mesto pa moram ovako (vidi binding za SelectetItem)
            foreach (DB.Mesto item in comboBoxMesto.Items)
            {
                if (item.MestoID == ((DB.PoslovniPartner)gridPoslovniPartner.DataContext).MestoID)
                {
                    comboBoxMesto.SelectedItem = item;
                    break;
                }
            }
            foreach (DB.NacinOrganizacijeFirme item in comboBoxNacinOrganizacijeFirme.Items)
            {
                if (item.NacinOrganizacijeFirmeID == ((DB.PoslovniPartner)gridPoslovniPartner.DataContext).NacinOrganizacijeFirmeID)
                {
                    comboBoxNacinOrganizacijeFirme.SelectedItem = item;
                    break;
                }
            }

            //stanje = App.Stanje.Izmena;
            buttonSacuvajINovi.Visibility = Visibility.Collapsed;
            buttonSacuvaj.Visibility = Visibility.Collapsed;
            buttonSacuvajIZatvori.Visibility = Visibility.Collapsed;

            buttonServisnaKnjizica.Visibility = Visibility.Collapsed;
            buttonPonuda.Visibility = Visibility.Collapsed;
            buttonRadniNalog.Visibility = Visibility.Collapsed;

        }

        public bool Sacuvaj()
        {
            try
            {
                //za proveru tipa podataka
                int _PIB;

                if ((bool)checkBoxGenerisiSifru.IsChecked && textBoxSifra.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Šifra.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxSkracenNaziv.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Skraćen naziv.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxPIB.Text.Trim() != "" && !Int32.TryParse(textBoxPIB.Text, out _PIB))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje PIB.");
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
                        DB.PoslovniPartner _poslovniPartner = new DB.PoslovniPartner
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            SkracenNaziv = textBoxSkracenNaziv.Text.Trim() == "" ? null : textBoxSkracenNaziv.Text.Trim(),
                            PunNaziv = textBoxPunNaziv.Text.Trim() == "" ? null : textBoxPunNaziv.Text.Trim(),
                            ZiroRacun = textBoxZiroRacun.Text.Trim() == "" ? null : textBoxZiroRacun.Text.Trim(),
                            Adresa = textBoxAdresa.Text.Trim() == "" ? null : textBoxAdresa.Text.Trim(),
                            KontaktOsoba1 = textBoxKontaktOsoba1.Text.Trim() == "" ? null : textBoxKontaktOsoba1.Text.Trim(),
                            Telefon1 = textBoxTelefon1.Text.Trim() == "" ? null : textBoxTelefon1.Text.Trim(),
                            EMail1 = textBoxEMail1.Text.Trim() == "" ? null : textBoxEMail1.Text.Trim(),
                            KontaktOsoba2 = textBoxKontaktOsoba2.Text.Trim() == "" ? null : textBoxKontaktOsoba2.Text.Trim(),
                            Telefon2 = textBoxTelefon2.Text.Trim() == "" ? null : textBoxTelefon2.Text.Trim(),
                            EMail2 = textBoxEMail2.Text.Trim() == "" ? null : textBoxEMail2.Text.Trim(),
                            Faks = textBoxFaks.Text.Trim() == "" ? null : textBoxFaks.Text.Trim(),
                            
                        };
                        if (textBoxPIB.Text.Trim() != "")
                        {
                            _poslovniPartner.PIB = Convert.ToInt32(textBoxPIB.Text.Trim());
                        }
                        if (textBoxMaticniBroj.Text.Trim() != "")
                        {
                            _poslovniPartner.MaticniBroj = textBoxMaticniBroj.Text.Trim();
                        }
                        if (comboBoxNacinOrganizacijeFirme.SelectedItem != null)
                        {
                            DB.NacinOrganizacijeFirme _nacinOrganizacijeFirme = (DB.NacinOrganizacijeFirme)comboBoxNacinOrganizacijeFirme.SelectedItem;

                            if (_nacinOrganizacijeFirme.Naziv == null)
                            {
                                _poslovniPartner.NacinOrganizacijeFirme = null;
                            }
                            else
                            {
                                _poslovniPartner.NacinOrganizacijeFirmeID = _nacinOrganizacijeFirme.NacinOrganizacijeFirmeID;
                            }
                        }
                        if (comboBoxMesto.SelectedItem != null)
                        {
                            DB.Mesto _mesto = (DB.Mesto)comboBoxMesto.SelectedItem;

                            if (_mesto.Naziv == null)
                            {
                                _poslovniPartner.Mesto = null;
                            }
                            else
                            {
                                _poslovniPartner.MestoID = _mesto.MestoID;
                            }
                        }


                        dBProksi.UnesiPoslovniPartner(_poslovniPartner);

                        ObservableCollection<DB.PoslovniPartner> _poslovniPartneri = (ObservableCollection<DB.PoslovniPartner>)poslovniPartner.listViewPoslovniPartner.ItemsSource;
                        _poslovniPartneri.Add(_poslovniPartner);
                        poslovniPartner.listViewPoslovniPartner.SelectedItem = _poslovniPartner;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.PoslovniPartner _poslovniPartner = new DB.PoslovniPartner
                        {
                            PoslovniPartnerID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            SkracenNaziv = textBoxSkracenNaziv.Text.Trim() == "" ? null : textBoxSkracenNaziv.Text.Trim(),
                            PunNaziv = textBoxPunNaziv.Text.Trim() == "" ? null : textBoxPunNaziv.Text.Trim(),
                            ZiroRacun = textBoxZiroRacun.Text.Trim() == "" ? null : textBoxZiroRacun.Text.Trim(),
                            Adresa = textBoxAdresa.Text.Trim() == "" ? null : textBoxAdresa.Text.Trim(),
                            KontaktOsoba1 = textBoxKontaktOsoba1.Text.Trim() == "" ? null : textBoxKontaktOsoba1.Text.Trim(),
                            Telefon1 = textBoxTelefon1.Text.Trim() == "" ? null : textBoxTelefon1.Text.Trim(),
                            EMail1 = textBoxEMail1.Text.Trim() == "" ? null : textBoxEMail1.Text.Trim(),
                            KontaktOsoba2 = textBoxKontaktOsoba2.Text.Trim() == "" ? null : textBoxKontaktOsoba2.Text.Trim(),
                            Telefon2 = textBoxTelefon2.Text.Trim() == "" ? null : textBoxTelefon2.Text.Trim(),
                            EMail2 = textBoxEMail2.Text.Trim() == "" ? null : textBoxEMail2.Text.Trim(),
                            Faks = textBoxFaks.Text.Trim() == "" ? null : textBoxFaks.Text.Trim(),

                        };
                        if (textBoxPIB.Text.Trim() != "")
                        {
                            _poslovniPartner.PIB = Convert.ToInt32(textBoxPIB.Text.Trim());
                        }
                        if (textBoxMaticniBroj.Text.Trim() != "")
                        {
                            _poslovniPartner.MaticniBroj = textBoxMaticniBroj.Text.Trim();
                        }
                        if (comboBoxNacinOrganizacijeFirme.SelectedItem != null)
                        {
                            DB.NacinOrganizacijeFirme _nacinOrganizacijeFirme = (DB.NacinOrganizacijeFirme)comboBoxNacinOrganizacijeFirme.SelectedItem;

                            if (_nacinOrganizacijeFirme.Naziv == null)
                            {
                                _poslovniPartner.NacinOrganizacijeFirme = null;
                            }
                            else
                            {
                                _poslovniPartner.NacinOrganizacijeFirmeID = _nacinOrganizacijeFirme.NacinOrganizacijeFirmeID;
                            }
                        }
                        if (comboBoxMesto.SelectedItem != null)
                        {
                            DB.Mesto _mesto = (DB.Mesto)comboBoxMesto.SelectedItem;

                            if (_mesto.Naziv == null)
                            {
                                _poslovniPartner.Mesto = null;
                            }
                            else
                            {
                                _poslovniPartner.MestoID = _mesto.MestoID;
                            }
                        }


                        dBProksi.IzmeniPoslovniPartner(_poslovniPartner, (DB.PoslovniPartner)gridPoslovniPartner.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujPoslovniPartner(int poslovniPartnerID)
        {
            ObservableCollection<DB.PoslovniPartner> _poslovniPartneri = (ObservableCollection<DB.PoslovniPartner>)poslovniPartner.listViewPoslovniPartner.ItemsSource;
            bool _postoji = false;

            if (!poslovniPartner.listViewPoslovniPartner.Items.Count.Equals(0))
            {
                foreach (DB.PoslovniPartner item in _poslovniPartneri)
                {
                    if (item.PoslovniPartnerID.Equals(poslovniPartnerID))
                    {
                        poslovniPartner.listViewPoslovniPartner.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    poslovniPartner.listViewPoslovniPartner.SelectedIndex = 0;
                }
            }

        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.PoslovniPartner _trenutni = (DB.PoslovniPartner)poslovniPartner.listViewPoslovniPartner.SelectedItem;

                ObservableCollection<DB.PoslovniPartner> _poslovniPartneri = (ObservableCollection<DB.PoslovniPartner>)poslovniPartner.listViewPoslovniPartner.ItemsSource;

                if (!_poslovniPartneri.Count.Equals(0))
                {
                    poslovniPartner.listViewPoslovniPartner.ItemsSource = dBProksi.OsveziPoslovniPartner(_poslovniPartneri);

                    if (_trenutni != null)
                    {
                        SelektujPoslovniPartner(_trenutni.PoslovniPartnerID);
                    }
                }
                gridPoslovniPartner.DataContext = (DB.PoslovniPartner)poslovniPartner.listViewPoslovniPartner.SelectedItem;
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
                    poslovniPartner.UStanje(App.Stanje.Detaljno);
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

                    gridPoslovniPartner.DataContext = null;
                    stanje = App.Stanje.Unos;
                    poslovniPartner.UStanje(App.Stanje.Detaljno);
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

                    poslovniPartner.UStanje(App.Stanje.Detaljno);
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
                Pocetna _pocetna;
                if (poslovniPartner != null)
                {
                    _pocetna = (Pocetna)Window.GetWindow(this.poslovniPartner).FindName("windowPocetna");
                }
                else if (artikal != null)
                {
                    _pocetna = (Pocetna)Window.GetWindow(this.artikal).FindName("windowPocetna");
                }
                else
                {
                    return;
                }

                _pocetna.PrikaziFormu("ServisnaKnjizica.xaml");

                App.ServisnaKnjizicaPartnerID = Convert.ToInt32(textBoxID.Text);
                App.ServisnaKnjizicaVrstaPartnera = "PoslovniPartner";
                this.Close();

            }
        }

        private void buttonPonuda_Click(object sender, RoutedEventArgs e)
        {
            if (!textBoxID.Text.Trim().Equals(""))
            {
                Pocetna _pocetna;
                if (poslovniPartner != null)
                {
                    _pocetna = (Pocetna)Window.GetWindow(this.poslovniPartner).FindName("windowPocetna");
                }
                else if (artikal != null)
                {
                    _pocetna = (Pocetna)Window.GetWindow(this.artikal).FindName("windowPocetna");
                }
                else
                {
                    return;
                }

                _pocetna.PrikaziFormu("Ponuda.xaml");

                App.PonudaPartnerID = Convert.ToInt32(textBoxID.Text);
                App.PonudaVrstaPartnera = "PoslovniPartner";
                this.Close();
            }
        }

        private void buttonRadniNalog_Click(object sender, RoutedEventArgs e)
        {
            if (!textBoxID.Text.Trim().Equals(""))
            {
                Pocetna _pocetna;
                if (poslovniPartner != null)
                {
                    _pocetna = (Pocetna)Window.GetWindow(this.poslovniPartner).FindName("windowPocetna");
                }
                else if (artikal != null)
                {
                    _pocetna = (Pocetna)Window.GetWindow(this.artikal).FindName("windowPocetna");
                }
                else
                {
                    return;
                }

                _pocetna.PrikaziFormu("RadniNalog.xaml");

                App.RadniNalogPartnerID = Convert.ToInt32(textBoxID.Text);
                App.RadniNalogVrstaPartnera = "PoslovniPartner";
                this.Close();
            }
        }
    }
}
