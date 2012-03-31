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
using Microsoft.Reporting.WinForms;
using System.IO;
using System.Net.Mail;
using System.Net;
using System.Net.Sockets;
using System.Globalization;

namespace Servis
{
    /// <summary>
    /// Interaction logic for PonudaDetaljno.xaml
    /// </summary>
    public partial class PonudaDetaljno : Window
    {       

        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.Ponuda ponuda;        

        public PonudaDetaljno()
        {
            InitializeComponent();

            //string s = App.cultureInfo.Name;
        }

        public PonudaDetaljno(Servis.Ponuda ponuda, bool izmeniTrenutni) : this()
        {
            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.ponuda = ponuda;

            try
            {
                ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = new ObservableCollection<DB.NacinZahtevaZaPonudu>(dBProksi.DajSveNacinZahtevaZaPonudu().ToList());

                comboBoxNacinZahtevaZaPonudu.ItemsSource = _naciniZahtevaZaPonudu;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (izmeniTrenutni)
            {
                gridPonuda.DataContext = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;

                listViewStavkaUsluga.ItemsSource = ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

                //stvarno ne znam sto nece da sam selektuje pa moram ovako (vidi binding za SelectetItem)
                foreach (DB.NacinZahtevaZaPonudu item in comboBoxNacinZahtevaZaPonudu.Items)
                {
                    if (item.NacinZahtevaZaPonuduID == ((DB.Ponuda)gridPonuda.DataContext).NacinZahtevaZaPonuduID)
                    {
                        comboBoxNacinZahtevaZaPonudu.SelectedItem = item;
                        break;
                    }
                }

                stanje = App.Stanje.Izmena;

                if (listViewStavkaUsluga.Items.Count.Equals(0))
                {
                    UStanjeStavkaUsluga(App.Stanje.Osnovno);
                    UStanjeStavkaArtikal(App.Stanje.IzgasiSve);
                }
                else
                {
                    UStanjeStavkaUsluga(App.Stanje.Detaljno);
                    listViewStavkaUsluga.SelectedIndex = 0;
                }
            }
            else
            {
                stanje = App.Stanje.Unos;

                UStanjeStavkaUsluga(App.Stanje.IzgasiSve);
                UStanjeStavkaArtikal(App.Stanje.IzgasiSve);
            }
        }

        public void UStanjeStavkaUsluga(App.Stanje stanje)
        {
            buttonDodajStavkaUsluga.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeniStavkaUsluga.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonObrisiStavkaUsluga.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsveziStavkaUsluga.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
        }

        public void UStanjeStavkaArtikal(App.Stanje stanje)
        {
            buttonDodajStavkaArtikal.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeniStavkaArtikal.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonObrisiStavkaArtikal.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsveziStavkaArtikal.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
        }

        private bool Sacuvaj()
        {
            try
            {
                DB.KorisnikPrograma _korisnikPrograma = dBProksi.DajKorisnikPrograma();

                if (_korisnikPrograma == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Korisnik programa nije definisan");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxServisnaKnjizica.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi servisnu knjižicu.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxNacinZahtevaZaPonudu.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi Način zahteva za ponudu.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (!(bool)checkBoxPreuzimaLicno.IsChecked && !(bool)checkBoxObavestiTelefonom.IsChecked && !(bool)checkBoxPosaljiEMail.IsChecked && !(bool)checkBoxPesaljiSmsObavestenje.IsChecked)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi bar jedan Način obaveštavanja korisnika.");
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
                        DateTime? _preuzeoLicnoU = null;
                        if ((bool)checkBoxPreuzeoLicno.IsChecked) _preuzeoLicnoU = Convert.ToDateTime(textBlockPreuzeoLicnoU.Text, App.cultureInfo);
                        DateTime? _obavestenTelefonomU = null;
                        if ((bool)checkBoxObavestenTelefonom.IsChecked) _obavestenTelefonomU = Convert.ToDateTime(textBlockObavestenTelefonomU.Text, App.cultureInfo);
                        DateTime? _poslatoSMSObavestenjeU = null;
                        if ((bool)checkBoxPeslatoSmsObavestenje.IsChecked) _poslatoSMSObavestenjeU = Convert.ToDateTime(textBlockPoslatoSmsObavestenjeU.Text, App.cultureInfo);
                        DateTime? _poslatEMailU = null;
                        if ((bool)checkBoxPoslatEMail.IsChecked) _poslatEMailU = Convert.ToDateTime(textBlockPoslatEMailU.Text, App.cultureInfo);


                        DB.Ponuda _ponuda = new DB.Ponuda
                        {
                            KorisnikProgramaID = _korisnikPrograma.KorisnikProgramaID,
                            ServisnaKnjizicaID = Convert.ToInt32(textBoxServisnaKnjizica.Tag.ToString()),
                            RadnikID = App.Radnik.RadnikID,
                            Vreme = DateTime.Now,
                            NacinZahtevaZaPonuduID = ((DB.NacinZahtevaZaPonudu)comboBoxNacinZahtevaZaPonudu.SelectedItem).NacinZahtevaZaPonuduID,
                            PreuzimaLicno = (bool)checkBoxPreuzimaLicno.IsChecked,
                            PreuzeoLicnoU = _preuzeoLicnoU, //(bool)checkBoxPreuzeoLicno.IsChecked ? Convert.ToDateTime(textBlockPreuzeoLicnoU.Text, App.cultureInfo) : null,
                            ObavestiTelefonom = (bool)checkBoxObavestiTelefonom.IsChecked,
                            ObavestenTelefonomU = _obavestenTelefonomU, //(bool)checkBoxObavestenTelefonom.IsChecked ? Convert.ToDateTime(textBlockObavestenTelefonomU.Text, App.cultureInfo) : null,
                            PosaljiSMSObavestenje = (bool)checkBoxPesaljiSmsObavestenje.IsChecked,
                            PoslatoSMSObavestenjeU = _poslatoSMSObavestenjeU, //(bool)checkBoxPeslatoSmsObavestenje.IsChecked ? Convert.ToDateTime(textBlockPoslatoSmsObavestenjeU.Text, App.cultureInfo) : null,
                            PosaljiEMail = (bool)checkBoxPosaljiEMail.IsChecked,
                            PoslatEMailU = _poslatEMailU,
                            Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                            Status = 'I',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog=App.Radnik.Nadimak
                        };

                        dBProksi.UnesiPonuda(_ponuda);

                        ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)ponuda.listViewPonuda.ItemsSource;
                        _ponude.Add(_ponuda);
                        ponuda.listViewPonuda.SelectedItem = _ponuda;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DateTime? _preuzeoLicnoU = null;
                        if ((bool)checkBoxPreuzeoLicno.IsChecked) _preuzeoLicnoU = Convert.ToDateTime(textBlockPreuzeoLicnoU.Text, App.cultureInfo);
                        DateTime? _obavestenTelefonomU = null;
                        if ((bool)checkBoxObavestenTelefonom.IsChecked) _obavestenTelefonomU = Convert.ToDateTime(textBlockObavestenTelefonomU.Text, App.cultureInfo);
                        DateTime? _poslatoSMSObavestenjeU = null;
                        if ((bool)checkBoxPeslatoSmsObavestenje.IsChecked) _poslatoSMSObavestenjeU = Convert.ToDateTime(textBlockPoslatoSmsObavestenjeU.Text, App.cultureInfo);
                        DateTime? _poslatEMailU = null;
                        try
                        {
                            if ((bool)checkBoxPoslatEMail.IsChecked) _poslatEMailU = Convert.ToDateTime(textBlockPoslatEMailU.Text, App.cultureInfo);
                        }
                        catch (Exception ex)
                        {
                            throw new Exception("Ovu gresku prijavi Oliveru obavezno: Greska prilikom konvertovanja vrednost " + textBlockPoslatEMailU.Text + " u DateTime :" + ex);
                        }

                        DB.Ponuda _orginalPonuda = (DB.Ponuda)gridPonuda.DataContext;

                        DB.Ponuda _ponuda = new DB.Ponuda
                        {
                            PonudaID = Convert.ToInt32(textBoxID.Text.Trim()),
                            KorisnikProgramaID = _korisnikPrograma.KorisnikProgramaID,
                            ServisnaKnjizicaID = Convert.ToInt32(textBoxServisnaKnjizica.Tag.ToString()),
                            RadnikID = _orginalPonuda.Radnik.RadnikID,  //App.Radnik.RadnikID, //ponudu je dao radnik koji ju je inicvijalno uneo
                            Vreme = Convert.ToDateTime(textBoxVreme.Text.Trim(), App.cultureInfo),
                            NacinZahtevaZaPonuduID = ((DB.NacinZahtevaZaPonudu)comboBoxNacinZahtevaZaPonudu.SelectedItem).NacinZahtevaZaPonuduID,
                            PreuzimaLicno = (bool)checkBoxPreuzimaLicno.IsChecked,
                            PreuzeoLicnoU = _preuzeoLicnoU, //(bool)checkBoxPreuzeoLicno.IsChecked ? Convert.ToDateTime(textBlockPreuzeoLicnoU.Text, App.cultureInfo) : null,
                            ObavestiTelefonom = (bool)checkBoxObavestiTelefonom.IsChecked,
                            ObavestenTelefonomU = _obavestenTelefonomU, //(bool)checkBoxObavestenTelefonom.IsChecked ? Convert.ToDateTime(textBlockObavestenTelefonomU.Text, App.cultureInfo) : null,
                            PosaljiSMSObavestenje = (bool)checkBoxPesaljiSmsObavestenje.IsChecked,
                            PoslatoSMSObavestenjeU = _poslatoSMSObavestenjeU, //(bool)checkBoxPeslatoSmsObavestenje.IsChecked ? Convert.ToDateTime(textBlockPoslatoSmsObavestenjeU.Text, App.cultureInfo) : null,
                            PosaljiEMail = (bool)checkBoxPosaljiEMail.IsChecked,
                            PoslatEMailU = _poslatEMailU,
                            Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                            Status = 'U',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };

                        dBProksi.IzmeniPonuda(_ponuda, _orginalPonuda); //(DB.Ponuda)gridPonuda.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujPonuda(int ponudaID)
        {
            ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)ponuda.listViewPonuda.ItemsSource;
            bool _postoji = false;

            if (!ponuda.listViewPonuda.Items.Count.Equals(0))
            {
                foreach (DB.Ponuda item in _ponude)
                {
                    if (item.PonudaID.Equals(ponudaID))
                    {
                        ponuda.listViewPonuda.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    ponuda.listViewPonuda.SelectedIndex = 0;
                }
            }
        }

        public void SelektujStavkaUsluga(int stavkaUslugaID)
        {
            IEnumerable<DB.StavkaUsluga> _stavkaUslugaLista = (IEnumerable<DB.StavkaUsluga>)listViewStavkaUsluga.ItemsSource;
            bool _postoji = false;

            if (!listViewStavkaUsluga.Items.Count.Equals(0))
            {
                foreach (DB.StavkaUsluga item in _stavkaUslugaLista)
                {
                    if (item.StavkaUslugaID.Equals(stavkaUslugaID))
                    {
                        listViewStavkaUsluga.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewStavkaUsluga.SelectedIndex = 0;
                }
            }
        }

        public void SelektujStavkaArtikal(int stavkaArtikalID)
        {
            IEnumerable<DB.StavkaArtikal> _stavkaArtikalLista = (IEnumerable<DB.StavkaArtikal>)listViewStavkaArtikal.ItemsSource;
            bool _postoji = false;

            if (!listViewStavkaArtikal.Items.Count.Equals(0))
            {
                foreach (DB.StavkaArtikal item in _stavkaArtikalLista)
                {
                    if (item.StavkaArtikalID.Equals(stavkaArtikalID))
                    {
                        listViewStavkaArtikal.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewStavkaArtikal.SelectedIndex = 0;
                }
            }
        }

        public void OsveziPonuda()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.Ponuda _trenutni = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;

                ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)ponuda.listViewPonuda.ItemsSource;

                if (!_ponude.Count.Equals(0))
                {
                    ponuda.listViewPonuda.ItemsSource = dBProksi.OsveziPonuda(_ponude);

                    if (_trenutni != null)
                    {
                        SelektujPonuda(_trenutni.PonudaID);
                    }
                    gridPonuda.DataContext = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;
                }
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
                    DB.Ponuda _trenutnaPonuda = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;
                    DB.StavkaUsluga _trenutnaStavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;
                    DB.StavkaArtikal _trenutnaStavkaArtikal = (DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem;

                    OsveziPonuda();

                    ponuda.UStanje(App.Stanje.Detaljno);

                    //stanje = App.Stanje.Izmena;
                    //ponuda.UStanje(App.Stanje.Detaljno);

                    //UStanjeStavkaUsluga(App.Stanje.Osnovno);
                    //UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                    //ako je i nakon osvezavanja podataka ostala selektovana ista ponuda
                    if (ponuda.listViewPonuda.SelectedItem != null && ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).PonudaID == _trenutnaPonuda.PonudaID)
                    {
                        listViewStavkaUsluga.ItemsSource = ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

                        if (listViewStavkaUsluga.Items.Count.Equals(0))
                        {
                            UStanjeStavkaUsluga(App.Stanje.Osnovno);
                            UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                            listViewStavkaArtikal.ItemsSource = null;

                        }
                        else if (!listViewStavkaUsluga.Items.Count.Equals(0) && listViewStavkaArtikal.Items.Count.Equals(0))
                        {
                            UStanjeStavkaUsluga(App.Stanje.Detaljno);

                            //listViewStavkaUsluga.SelectedIndex = 0;

                            SelektujStavkaUsluga(_trenutnaStavkaUsluga.StavkaUslugaID);

                            UStanjeStavkaArtikal(App.Stanje.Osnovno);

                        }
                        else 
                        {
                            UStanjeStavkaUsluga(App.Stanje.Detaljno);

                            //listViewStavkaUsluga.SelectedIndex = 0;

                            SelektujStavkaUsluga(_trenutnaStavkaUsluga.StavkaUslugaID);
                            SelektujStavkaArtikal(_trenutnaStavkaArtikal.StavkaArtikalID);
                        }
                    }
                    else
                    {
                        gridPonuda.DataContext = null;
                        listViewStavkaUsluga.ItemsSource = null;
                        listViewStavkaArtikal.ItemsSource = null;

                        comboBoxNacinZahtevaZaPonudu.SelectedItem = null;


                        stanje = App.Stanje.Unos;

                        MessageBox.Show("Ponudu je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
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
                    OsveziPonuda();

                    ponuda.UStanje(App.Stanje.Detaljno);

                    gridPonuda.DataContext = null;
                    listViewStavkaUsluga.ItemsSource = null;
                    listViewStavkaArtikal.ItemsSource = null;

                    stanje = App.Stanje.Unos;
                    ponuda.UStanje(App.Stanje.Detaljno);

                    UStanjeStavkaUsluga(App.Stanje.IzgasiSve);
                    UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                    comboBoxNacinZahtevaZaPonudu.SelectedItem = null;
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
                    OsveziPonuda();

                    ponuda.UStanje(App.Stanje.Detaljno);

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
            NavigationWindow _naw = new NavigationWindow();

            _naw.Content = new ServisnaKnjizica(this);

            //sakrijem strelice za nazed i napred
            _naw.ShowsNavigationUI = false;
            _naw.Owner = Window.GetWindow(this);
            _naw.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (Window.GetWindow(ponuda).WindowState == WindowState.Normal)
            {
                _naw.Width = Window.GetWindow(ponuda).ActualWidth;
                _naw.Height = Window.GetWindow(ponuda).ActualHeight;
            }
            else
            {
                _naw.WindowState = Window.GetWindow(ponuda).WindowState;
            }
            _naw.ShowDialog();
        }

        private void textBoxServisnaKnjizica_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox _trenutni = (CheckBox)sender;

            if (_trenutni.Name == checkBoxPreuzimaLicno.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                }
                else
                {
                    checkBoxPreuzeoLicno.IsChecked = false;
                    textBlockPreuzeoLicnoU.Text = "";
                }
            }
            else if (_trenutni.Name == checkBoxPreuzeoLicno.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                    checkBoxPreuzimaLicno.IsChecked = true;
                    textBlockPreuzeoLicnoU.Text = DateTime.Now.ToString(App.cultureInfo);
                }
                else
                {
                    textBlockPreuzeoLicnoU.Text = "";
                }
            }
            else if (_trenutni.Name == checkBoxObavestiTelefonom.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                }
                else
                {
                    checkBoxObavestenTelefonom.IsChecked = false;
                    textBlockObavestenTelefonomU.Text = "";
                }
            }
            else if (_trenutni.Name == checkBoxObavestenTelefonom.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                    checkBoxObavestiTelefonom.IsChecked = true;
                    textBlockObavestenTelefonomU.Text = DateTime.Now.ToString(App.cultureInfo);
                }
                else
                {
                    textBlockObavestenTelefonomU.Text = "";
                }
            }
            else if (_trenutni.Name == checkBoxPesaljiSmsObavestenje.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                }
                else
                {
                    checkBoxPeslatoSmsObavestenje.IsChecked = false;
                    textBlockPoslatoSmsObavestenjeU.Text = "";
                }
            }
            else if (_trenutni.Name == checkBoxPeslatoSmsObavestenje.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                    checkBoxPesaljiSmsObavestenje.IsChecked = true;
                    textBlockPoslatoSmsObavestenjeU.Text = DateTime.Now.ToString(App.cultureInfo);
                }
                else
                {
                    textBlockPoslatoSmsObavestenjeU.Text = "";
                }
            }

            else if (_trenutni.Name == checkBoxPosaljiEMail.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                }
                else
                {
                    checkBoxPoslatEMail.IsChecked = false;
                    textBlockPoslatEMailU.Text = "";
                }
            }
            else if (_trenutni.Name == checkBoxPoslatEMail.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                    checkBoxPosaljiEMail.IsChecked = true;
                    textBlockPoslatEMailU.Text = DateTime.Now.ToString(App.cultureInfo);
                }
                else
                {
                    textBlockPoslatEMailU.Text = "";
                }
            }
            
        }

        private void listViewStavkaUsluga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;

            if (_stavkaUsluga != null)
            {
                listViewStavkaArtikal.ItemsSource = new ObservableCollection<DB.StavkaArtikal>(_stavkaUsluga.StavkaArtikals.ToList()).Where(f => f.Status != 'D').OrderBy(o => o.NosilacGrupe.Naziv).ThenBy(o => o.ArtikalCenaBezPoreza);
                
                if (listViewStavkaArtikal.Items.Count.Equals(0))
                {
                    UStanjeStavkaArtikal(App.Stanje.Osnovno);
                }
                else
                {
                    UStanjeStavkaArtikal(App.Stanje.Detaljno);
                    listViewStavkaArtikal.SelectedIndex = 0;
                }
            }
        }



        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl _tab = (TabControl)sender;
            TabItem _trenutiItem = (TabItem)_tab.SelectedItem;

            DB.Ponuda _ponuda = (DB.Ponuda)gridPonuda.DataContext;

            reportViewerPonuda.Reset();

            if (_trenutiItem != null && _trenutiItem.Header == tabItemStampa.Header && _ponuda != null)
            {
                decimal _totalMinSaPDV_Ponuda = 0;
                decimal _totalMaxSaPDV_Ponuda = 0;

                //OLIVER
                //decimal _totalMinSaPDV_StavkaArtikalGrupisana = 0;
                //decimal _totalMaxSaPDV_StavkaArtikalGrupisana = 0;

                List<DB.StampaPonuda> _stampaPonudaLista = new List<DB.StampaPonuda>();
                List<DB.StampaStavkaUsluga> _stampaStavkaUslugaLista = new List<DB.StampaStavkaUsluga>();
                List<DB.StampaStavkaArtikal> _stampaStavkaArtikalLista = new List<DB.StampaStavkaArtikal>();
                List<DB.StampaStavkaArtikalGrupisana> _stampaStavkaArtikalGrupisanaLista = new List<DB.StampaStavkaArtikalGrupisana>();

                #region Napuni _stampaPonudaLista - uvek ima jedan red

                string _telefon = "";
                TelefonMaskKonverterReadOnly _telefonKonverter = new TelefonMaskKonverterReadOnly();
                if (_ponuda.ServisnaKnjizica.PoslovniPartnerID != null)
                {
                    _telefon = (string)_telefonKonverter.Convert(_ponuda.ServisnaKnjizica.PoslovniPartner.Telefon1, typeof(string), "___/__-__-________", new System.Globalization.CultureInfo("en-US"));
                }
                else 
                {
                    _telefon = (string)_telefonKonverter.Convert(_ponuda.ServisnaKnjizica.FizickoLice.Telefon1, typeof(string), "___/__-__-________", new System.Globalization.CultureInfo("en-US"));
                }

                string _registarskiBroj = "";
                if (_ponuda.ServisnaKnjizica.RegistarskiBroj != "")
                {
                    _registarskiBroj = (string)_telefonKonverter.Convert(_ponuda.ServisnaKnjizica.RegistarskiBroj, typeof(string), "__ ___ ____________", new System.Globalization.CultureInfo("en-US"));
                }

                DB.StampaPonuda _stampaPonuda = new DB.StampaPonuda
                {
                    PonudaID = _ponuda.PonudaID,
                    ServisnaKnjizicaSifra = _ponuda.ServisnaKnjizica.Sifra,
                    PartnerNaziv = _ponuda.ServisnaKnjizica.PoslovniPartnerID != null ? _ponuda.ServisnaKnjizica.PoslovniPartner.SkracenNaziv : _ponuda.ServisnaKnjizica.FizickoLice.Ime + " " + _ponuda.ServisnaKnjizica.FizickoLice.Prezime,
                    NacinZahtevaZaPonudu = _ponuda.NacinZahtevaZaPonudu.Naziv,
                    VremeZahtevaZaPonudu = _ponuda.Vreme,
                    RadnikSifra = _ponuda.Radnik.Sifra,
                    RadnikNadimak = _ponuda.Radnik.Nadimak,
                    Napomena = _ponuda.Napomena,

                    Ime = _ponuda.ServisnaKnjizica.PoslovniPartnerID != null ? _ponuda.ServisnaKnjizica.PoslovniPartner.KontaktOsoba1 : _ponuda.ServisnaKnjizica.FizickoLice.Ime + " " + _ponuda.ServisnaKnjizica.FizickoLice.Prezime,

                    Telefon = _telefon, //_ponuda.ServisnaKnjizica.PoslovniPartnerID != null ? _ponuda.ServisnaKnjizica.PoslovniPartner.Telefon1 : _ponuda.ServisnaKnjizica.FizickoLice.Telefon1,
                    
                    EMail = _ponuda.ServisnaKnjizica.PoslovniPartnerID != null ? _ponuda.ServisnaKnjizica.PoslovniPartner.EMail1 : _ponuda.ServisnaKnjizica.FizickoLice.EMail,

                    RegistarskiBroj = _registarskiBroj, //5_ponuda.ServisnaKnjizica.RegistarskiBroj,

                    TipAutomobila = _ponuda.ServisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " +
                                    _ponuda.ServisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " +
                                    _ponuda.ServisnaKnjizica.TipAutomobila.OpisTabela.Opis
                };

                _stampaPonudaLista.Add(_stampaPonuda); 
                #endregion

                //za svaku uslugu
                foreach (DB.StavkaUsluga itemU in _ponuda.StavkaUslugas.Where(f => f.Status != 'D'))
                {
                    //vrednost usluge ulazi i u Min i u Max vrednost Totala izvestaja
                    decimal _vrednostSaPDVStavkaUsluga = (itemU.UslugaCenaBezPoreza + ((itemU.UslugaCenaBezPoreza / 100) * itemU.PoreskaStopa.VrednostProcenata)) * itemU.UslugaKolicina;
                    //Convert.ToDecimal da zaokruzim na dve decimale
                    _totalMaxSaPDV_Ponuda += Convert.ToDecimal(_vrednostSaPDVStavkaUsluga.ToString(".00"), App.cultureInfo);
                    _totalMinSaPDV_Ponuda += Convert.ToDecimal(_vrednostSaPDVStavkaUsluga.ToString(".00"), App.cultureInfo);

                    //ako Usluga nema artikala
                    if (itemU.StavkaArtikals.Count().Equals(0))
                    {
                        DB.StampaStavkaUsluga _stavkaUslugaStampa = new DB.StampaStavkaUsluga
                        {
                            StavkaUslugaID = itemU.StavkaUslugaID,
                            Usluga = itemU.Usluga.VrstaUsluge.Naziv + " " + itemU.Usluga.NosilacGrupe.Naziv + " " + itemU.Usluga.Nivo.Naziv + " " + itemU.Usluga.Pozicija.Naziv,
                            UslugaKolicina = itemU.UslugaKolicina,
                            UslugaCenaBezPoreza = itemU.UslugaCenaBezPoreza,
                            UslugaPoreskaStopaVrednost = itemU.PoreskaStopa.VrednostProcenata
                        };
                        _stampaStavkaUslugaLista.Add(_stavkaUslugaStampa);
                    }
                    else
                    {
                        #region Rasporedi artikle u odgovarajucu listu, _stampaStavkaArtikalLista ili _stampaStavkaArtikalGrupisanaLista
                        foreach (DB.StavkaArtikal itemA in itemU.StavkaArtikals.Where(f => f.Status != 'D'))
                        {
                            //broj jedinstvenih nosioca grupe u StavkaArtikals za uslugu
                            int _brojJedinstvenihNosilacGrupe = (from u in itemU.StavkaArtikals
                                                                 where u.Status != 'D' && u.StavkaUslugaID == itemU.StavkaUslugaID
                                                                 select u.NosilacGrupeID).Distinct().Count();

                            if (_brojJedinstvenihNosilacGrupe <= 1)
                            {
                                DB.StampaStavkaArtikal _stavkaStampaArtikal = new DB.StampaStavkaArtikal
                                {
                                    StavkaArtikalID = itemA.StavkaArtikalID,
                                    NosilacGrupe = itemA.NosilacGrupe.Naziv,
                                    ArtikalNaziv = itemA.ArtikalNaziv,
                                    ArtikalBrojProizvodjaca = itemA.ArtikalBrojProizvodjaca,
                                    ArtikalProizvodjacNaziv = itemA.ArtikalProizvodjacNaziv,
                                    ArtikalKolicina = itemA.ArtikalKolicina,
                                    ArtikalCenaBezPoreza = itemA.ArtikalCenaBezPoreza,
                                    ArtikalPoreskaStopaVrednost = itemA.PoreskaStopa.VrednostProcenata,
                                    StavkaUslugaID = itemU.StavkaUslugaID,
                                    Usluga = itemU.Usluga.VrstaUsluge.Naziv + " " + itemU.Usluga.NosilacGrupe.Naziv + " " + itemU.Usluga.Nivo.Naziv + " " + itemU.Usluga.Pozicija.Naziv,
                                    UslugaKolicina = itemU.UslugaKolicina,
                                    UslugaCenaBezPoreza = itemU.UslugaCenaBezPoreza,
                                    UslugaPoreskaStopaVrednost = itemU.PoreskaStopa.VrednostProcenata
                                };
                                _stampaStavkaArtikalLista.Add(_stavkaStampaArtikal);
                            }
                            else
                            {
                                DB.StampaStavkaArtikalGrupisana _stavkaStampaArtikalGrupisana = new DB.StampaStavkaArtikalGrupisana
                                {
                                    StavkaArtikalID = itemA.StavkaArtikalID,
                                    NosilacGrupe = itemA.NosilacGrupe.Naziv,
                                    ArtikalNaziv = itemA.ArtikalNaziv,
                                    ArtikalBrojProizvodjaca = itemA.ArtikalBrojProizvodjaca,
                                    ArtikalProizvodjacNaziv = itemA.ArtikalProizvodjacNaziv,
                                    ArtikalKolicina = itemA.ArtikalKolicina,
                                    ArtikalCenaBezPoreza = itemA.ArtikalCenaBezPoreza,
                                    ArtikalPoreskaStopaVrednost = itemA.PoreskaStopa.VrednostProcenata,
                                    StavkaUslugaID = itemU.StavkaUslugaID,
                                    Usluga = itemU.Usluga.VrstaUsluge.Naziv + " " + itemU.Usluga.NosilacGrupe.Naziv + " " + itemU.Usluga.Nivo.Naziv + " " + itemU.Usluga.Pozicija.Naziv,
                                    UslugaKolicina = itemU.UslugaKolicina,
                                    UslugaCenaBezPoreza = itemU.UslugaCenaBezPoreza,
                                    UslugaPoreskaStopaVrednost = itemU.PoreskaStopa.VrednostProcenata
                                    //TotalMaxSaPDV 
                                    //TotalMinSaPDV
                                };
                                _stampaStavkaArtikalGrupisanaLista.Add(_stavkaStampaArtikalGrupisana);
                            }
                        }
                        #endregion
                    }
                }


                //za StampaStavkaArtikal dodaj vrednost za _totalMinSaPDV_Ponuda i _totalMaxSaPDV_Ponuda
                List<int> _stampaStavkaArtikalLista_ObradjeneUsluge = new List<int>();
                foreach (DB.StampaStavkaArtikal item in _stampaStavkaArtikalLista)
                {
                    var _upit = from u in _stampaStavkaArtikalLista
                                where u.StavkaUslugaID == item.StavkaUslugaID
                                select new
                                {
                                    VrednostSaPdv = (u.ArtikalCenaBezPoreza + ((u.ArtikalCenaBezPoreza / 100) * u.ArtikalPoreskaStopaVrednost)) * u.ArtikalKolicina
                                };

                    if (!_stampaStavkaArtikalLista_ObradjeneUsluge.Contains(item.StavkaUslugaID))
                    {
                        _totalMaxSaPDV_Ponuda += Convert.ToDecimal(_upit.Max(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                        _totalMinSaPDV_Ponuda += Convert.ToDecimal(_upit.Min(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                    }
                    _stampaStavkaArtikalLista_ObradjeneUsluge.Add(item.StavkaUslugaID);
                }


                //za StampaStavkaArtikalGrupisana dodaj vrednost za _totalMinSaPDV_Ponuda i _totalMaxSaPDV_Ponuda
                List<int> _stampaStavkaArtikalGrupisanaLista_ObradjeneUsluge = new List<int>();
                List<string> _stampaStavkaArtikalGrupisanaLista_ObradjeniNosiociGrupe = new List<string>();
                foreach (DB.StampaStavkaArtikalGrupisana item in _stampaStavkaArtikalGrupisanaLista)
                {
                    //Unesi vrednosti za _stampaStavkaArtikalGrupisanaLista.TotalMaxSaPDV i _stampaStavkaArtikalGrupisanaLista.TotalMinSaPDV
                    var _upitVrednostArtikal = from u in _stampaStavkaArtikalGrupisanaLista
                                               where u.StavkaUslugaID == item.StavkaUslugaID && u.NosilacGrupe == item.NosilacGrupe
                                               select new
                                               {
                                                   VrednostSaPdv = (u.ArtikalCenaBezPoreza + ((u.ArtikalCenaBezPoreza / 100) * u.ArtikalPoreskaStopaVrednost)) * u.ArtikalKolicina
                                               };

                    var _upitVrednostUsluga = from u in _stampaStavkaArtikalGrupisanaLista
                                               where u.StavkaUslugaID == item.StavkaUslugaID && u.NosilacGrupe == item.NosilacGrupe
                                               select new
                                               {
                                                   VrednostSaPdv = (u.UslugaCenaBezPoreza + ((u.UslugaCenaBezPoreza / 100) * u.UslugaPoreskaStopaVrednost)) * u.UslugaKolicina
                                               };
                                
                    //ako nema ni usluge ni nosioca grupe
                    if (!_stampaStavkaArtikalGrupisanaLista_ObradjeneUsluge.Contains(item.StavkaUslugaID) &&
                        !_stampaStavkaArtikalGrupisanaLista_ObradjeniNosiociGrupe.Contains(item.NosilacGrupe)
                        )
                    {
                        //OLIVER
                        //_totalMaxSaPDV_StavkaArtikalGrupisana += Convert.ToDecimal(_upitVrednostArtikal.Max(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                        //_totalMinSaPDV_StavkaArtikalGrupisana += Convert.ToDecimal(_upitVrednostArtikal.Min(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                        //OLIVER
                        //_totalMaxSaPDV_StavkaArtikalGrupisana += Convert.ToDecimal(_upitVrednostUsluga.Max(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                        //_totalMinSaPDV_StavkaArtikalGrupisana += Convert.ToDecimal(_upitVrednostUsluga.Min(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                        _totalMaxSaPDV_Ponuda += Convert.ToDecimal(_upitVrednostArtikal.Max(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                        _totalMinSaPDV_Ponuda += Convert.ToDecimal(_upitVrednostArtikal.Min(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);                      
                    }//ako ima usluge a nema nosioca grupe
                    else if (_stampaStavkaArtikalGrupisanaLista_ObradjeneUsluge.Contains(item.StavkaUslugaID) &&
                            !_stampaStavkaArtikalGrupisanaLista_ObradjeniNosiociGrupe.Contains(item.NosilacGrupe)
                            )
                    {
                        //OLIVER
                        //_totalMaxSaPDV_StavkaArtikalGrupisana += Convert.ToDecimal(_upitVrednostArtikal.Max(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                        //_totalMinSaPDV_StavkaArtikalGrupisana += Convert.ToDecimal(_upitVrednostArtikal.Min(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);

                        _totalMaxSaPDV_Ponuda += Convert.ToDecimal(_upitVrednostArtikal.Max(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                        _totalMinSaPDV_Ponuda += Convert.ToDecimal(_upitVrednostArtikal.Min(a => a.VrednostSaPdv).ToString(".00"), App.cultureInfo);
                    }

                    _stampaStavkaArtikalGrupisanaLista_ObradjeneUsluge.Add(item.StavkaUslugaID);
                    _stampaStavkaArtikalGrupisanaLista_ObradjeniNosiociGrupe.Add(item.NosilacGrupe);
                }

                foreach (DB.StampaStavkaArtikalGrupisana item in _stampaStavkaArtikalGrupisanaLista)
                {
                    decimal _totalArtikalMinSaPdv = 0;
                    decimal _totalArtikalMaxSaPdv = 0;

                    decimal _uslugaSaPdv = (item.UslugaCenaBezPoreza + ((item.UslugaCenaBezPoreza / 100) * item.UslugaPoreskaStopaVrednost)) * item.UslugaKolicina;

                    var _nosiociGrupeZaUslugu = from u in _stampaStavkaArtikalGrupisanaLista
                                                where u.StavkaUslugaID == item.StavkaUslugaID
                                               select new
                                               {
                                                   NosilacGrupe = u.NosilacGrupe
                                               };

                    foreach (var s in _nosiociGrupeZaUslugu.Distinct())
                    {
                        var _upit = from u in _stampaStavkaArtikalGrupisanaLista
                                    where u.NosilacGrupe.ToString() == s.NosilacGrupe.ToString()
                                    select u;

                        var _upitVrednostArtikal = from u in _upit
                                                   select new
                                                   {
                                                       VrednostSaPdv = (u.ArtikalCenaBezPoreza + ((u.ArtikalCenaBezPoreza / 100) * u.ArtikalPoreskaStopaVrednost)) * u.ArtikalKolicina
                                                   };

                        _totalArtikalMinSaPdv += _upitVrednostArtikal.Min(a => a.VrednostSaPdv);
                        _totalArtikalMaxSaPdv += _upitVrednostArtikal.Max(a => a.VrednostSaPdv);
                    }






                    item.TotalMinSaPDV = Convert.ToDecimal((_uslugaSaPdv + _totalArtikalMinSaPdv).ToString(".00"), App.cultureInfo);
                    item.TotalMaxSaPDV = Convert.ToDecimal((_uslugaSaPdv + _totalArtikalMaxSaPdv).ToString(".00"), App.cultureInfo);

                }

                //OLIVER
                //foreach (DB.StampaStavkaArtikalGrupisana item in _stampaStavkaArtikalGrupisanaLista)
                //{
                //    item.TotalMaxSaPDV = _totalMaxSaPDV_StavkaArtikalGrupisana;
                //    item.TotalMinSaPDV = _totalMinSaPDV_StavkaArtikalGrupisana;
                //}

                _stampaPonuda.TotalMinSaPDV = _totalMinSaPDV_Ponuda;
                _stampaPonuda.TotalMaxSaPDV = _totalMaxSaPDV_Ponuda;

                #region Prikazi izvestaj

                reportViewerPonuda.LocalReport.ReportEmbeddedResource = "Servis.PonudaStampa.rdlc";
                reportViewerPonuda.ProcessingMode = ProcessingMode.Local;

                ReportDataSource _reportDataSourcePonuda = new ReportDataSource("DS_StampaPonuda", _stampaPonudaLista);
                ReportDataSource _reportDataSourceStavka = new ReportDataSource("DS_StampaStavkaArtikal", _stampaStavkaArtikalLista);
                ReportDataSource _reportDataSourceStavkaGrupisana = new ReportDataSource("DS_StampaStavkaArtikalGrupisana", _stampaStavkaArtikalGrupisanaLista);
                ReportDataSource _reportDataSourceStavkaUsluga = new ReportDataSource("DS_StampaStavkaUsluga", _stampaStavkaUslugaLista);

                reportViewerPonuda.LocalReport.DataSources.Add(_reportDataSourcePonuda);
                reportViewerPonuda.LocalReport.DataSources.Add(_reportDataSourceStavka);
                reportViewerPonuda.LocalReport.DataSources.Add(_reportDataSourceStavkaGrupisana);
                reportViewerPonuda.LocalReport.DataSources.Add(_reportDataSourceStavkaUsluga);
                reportViewerPonuda.SetDisplayMode(DisplayMode.PrintLayout);
                reportViewerPonuda.ZoomMode = ZoomMode.PageWidth;

                reportViewerPonuda.RefreshReport(); 
                #endregion
            }
        }



              
        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender.ToString().EndsWith("DB.StavkaUsluga"))
            {
                DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;

                if (_stavkaUsluga != null)
                {
                    StavkaUslugaDetaljno _stavkaUslugaDetaljno = new StavkaUslugaDetaljno(this, true);
                    //_stavkaUslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                    _stavkaUslugaDetaljno.Owner = Window.GetWindow(this);
                    _stavkaUslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _stavkaUslugaDetaljno.ShowDialog();
                }

            }
            else if (sender.ToString().EndsWith("DB.StavkaArtikal"))
            {
                DB.StavkaArtikal _stavkaUsluga = (DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem;

                if (_stavkaUsluga != null)
                {
                    StavkaArtikalDetaljno _stavkaArtikalDetaljno = new StavkaArtikalDetaljno(this, true);
                    //_stavkaArtikalDetaljno.WindowStyle = WindowStyle.ToolWindow;
                    _stavkaArtikalDetaljno.Owner = Window.GetWindow(this);
                    _stavkaArtikalDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _stavkaArtikalDetaljno.ShowDialog();
                }
            }
            else
            {
                MessageBox.Show("Nepoznat tip: " + sender.ToString(), "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        


        private void buttonDodajStavkaUsluga_Click(object sender, RoutedEventArgs e)
        {
            StavkaUslugaDetaljno _stavkaUslugaDetaljno = new StavkaUslugaDetaljno(this, false);
            //_stavkaUslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _stavkaUslugaDetaljno.Owner = Window.GetWindow(this);
            _stavkaUslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _stavkaUslugaDetaljno.ShowDialog();
        }
        private void buttonIzmeniStavkaUsluga_Click(object sender, RoutedEventArgs e)
        {
            DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;

            if (_stavkaUsluga != null)
            {
                StavkaUslugaDetaljno _stavkaUslugaDetaljno = new StavkaUslugaDetaljno(this, true);
                //_stavkaUslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _stavkaUslugaDetaljno.Owner = Window.GetWindow(this);
                _stavkaUslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _stavkaUslugaDetaljno.ShowDialog();
            }
        }
        private void buttonObrisiStavkaUsluga_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiStavkaUsluga((DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem, App.Radnik);

                    DB.Ponuda _trenutnaPonuda = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;

                    OsveziPonuda();

                    //ako je i nakon osvezavanja podataka ostala selektovana ista ponuda
                    if (ponuda.listViewPonuda.SelectedItem != null && ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).PonudaID == _trenutnaPonuda.PonudaID)
                    {
                        listViewStavkaUsluga.ItemsSource = ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

                        if (listViewStavkaUsluga.Items.Count.Equals(0))
                        {
                            UStanjeStavkaUsluga(App.Stanje.Osnovno);
                            UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                            listViewStavkaArtikal.ItemsSource = null;

                        }
                        else
                        {
                            UStanjeStavkaUsluga(App.Stanje.Detaljno);
                            listViewStavkaUsluga.SelectedIndex = 0;
                        }
                    }
                    else
                    {
                        gridPonuda.DataContext = null;
                        listViewStavkaUsluga.ItemsSource = null;
                        listViewStavkaArtikal.ItemsSource = null;

                        comboBoxNacinZahtevaZaPonudu.SelectedItem = null;


                        stanje = App.Stanje.Unos;

                        MessageBox.Show("Ponudu je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }
        public void buttonOsveziStavkaUsluga_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.Ponuda _trenutni = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;
                DB.StavkaUsluga _trenutnaStavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;
                DB.StavkaArtikal _trenutnaStavkaArtikal = (DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem;

                OsveziPonuda();

                //ako je i nakon osvezavanja podataka ostala selektovana ista ponuda
                if (ponuda.listViewPonuda.SelectedItem != null && ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).PonudaID == _trenutni.PonudaID)
                {
                    listViewStavkaUsluga.ItemsSource = ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

                    if (listViewStavkaUsluga.Items.Count.Equals(0))
                    {
                        UStanjeStavkaUsluga(App.Stanje.Osnovno);
                        UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                        listViewStavkaArtikal.ItemsSource = null;
                    }
                    else
                    {
                        UStanjeStavkaUsluga(App.Stanje.Detaljno);

                        if (_trenutnaStavkaUsluga != null)
                        {
                            SelektujStavkaUsluga(_trenutnaStavkaUsluga.StavkaUslugaID);
                        }

                        if (_trenutnaStavkaArtikal != null)
                        {
                            SelektujStavkaArtikal(_trenutnaStavkaArtikal.StavkaArtikalID);
                        }

                    }
                }
                else
                {
                    gridPonuda.DataContext = null;
                    listViewStavkaUsluga.ItemsSource = null;
                    listViewStavkaArtikal.ItemsSource = null;

                    comboBoxNacinZahtevaZaPonudu.SelectedItem = null;

                    stanje = App.Stanje.Unos;

                    MessageBox.Show("Ponudu je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }


        private void buttonDodajStavkaArtikal_Click(object sender, RoutedEventArgs e)
        {
            StavkaArtikalDetaljno _stavkaArtikalDetaljno = new StavkaArtikalDetaljno(this, false);
            //_stavkaArtikalDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _stavkaArtikalDetaljno.Owner = Window.GetWindow(this);
            _stavkaArtikalDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _stavkaArtikalDetaljno.ShowDialog();
        }
        private void buttonIzmeniStavkaArtikal_Click(object sender, RoutedEventArgs e)
        {
            DB.StavkaArtikal _stavkaUsluga = (DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem;

            if (_stavkaUsluga != null)
            {
                StavkaArtikalDetaljno _stavkaArtikalDetaljno = new StavkaArtikalDetaljno(this, true);
                //_stavkaArtikalDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _stavkaArtikalDetaljno.Owner = Window.GetWindow(this);
                _stavkaArtikalDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _stavkaArtikalDetaljno.ShowDialog();
            }
        }
        public void buttonOsveziStavkaArtikal_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.Ponuda _trenutni = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;
                DB.StavkaUsluga _trenutnaStavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;
                DB.StavkaArtikal _trenutnaStavkaArtikal = (DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem;

                OsveziPonuda();

                //ako je i nakon osvezavanja podataka ostala selektovana ista ponuda
                if (ponuda.listViewPonuda.SelectedItem != null && ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).PonudaID == _trenutni.PonudaID)
                {
                    listViewStavkaUsluga.ItemsSource = ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

                    if (listViewStavkaUsluga.Items.Count.Equals(0))
                    {
                        UStanjeStavkaUsluga(App.Stanje.Osnovno);
                        UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                        listViewStavkaArtikal.ItemsSource = null;
                    }
                    else
                    {
                        UStanjeStavkaUsluga(App.Stanje.Detaljno);

                        if (_trenutnaStavkaUsluga != null)
                        {
                            SelektujStavkaUsluga(_trenutnaStavkaUsluga.StavkaUslugaID);
                        }

                        if (_trenutnaStavkaArtikal != null)
                        {
                            SelektujStavkaArtikal(_trenutnaStavkaArtikal.StavkaArtikalID);
                        }

                    }
                }
                else
                {
                    gridPonuda.DataContext = null;
                    listViewStavkaUsluga.ItemsSource = null;
                    listViewStavkaArtikal.ItemsSource = null;

                    comboBoxNacinZahtevaZaPonudu.SelectedItem = null;

                    stanje = App.Stanje.Unos;

                    MessageBox.Show("Ponudu je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
        }
        private void buttonObrisiStavkaArtikal_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiStavkaArtikal((DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem, App.Radnik);

                    DB.Ponuda _trenutnaPonuda = (DB.Ponuda)ponuda.listViewPonuda.SelectedItem;
                    DB.StavkaUsluga _trenutnaStavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;

                    OsveziPonuda();

                    //ako je i nakon osvezavanja podataka ostala selektovana ista ponuda
                    if (ponuda.listViewPonuda.SelectedItem != null && ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).PonudaID == _trenutnaPonuda.PonudaID)
                    {
                        listViewStavkaUsluga.ItemsSource = ((DB.Ponuda)ponuda.listViewPonuda.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

                        if (listViewStavkaUsluga.Items.Count.Equals(0))
                        {
                            UStanjeStavkaUsluga(App.Stanje.Osnovno);
                            UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                            listViewStavkaArtikal.ItemsSource = null;
                        }
                        else
                        {
                            UStanjeStavkaUsluga(App.Stanje.Detaljno);

                            SelektujStavkaUsluga(_trenutnaStavkaUsluga.StavkaUslugaID);
                        }
                    }
                    else
                    {
                        gridPonuda.DataContext = null;
                        listViewStavkaUsluga.ItemsSource = null;
                        listViewStavkaArtikal.ItemsSource = null;

                        comboBoxNacinZahtevaZaPonudu.SelectedItem = null;


                        stanje = App.Stanje.Unos;

                        MessageBox.Show("Ponudu je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

 
        private void buttonKreirajRadniNalog_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxID.Text.Trim() != "")
            {
                PonudaRadniNalogWizard _carobnjakPonudaRadniNalog = new PonudaRadniNalogWizard(this);
                _carobnjakPonudaRadniNalog.Owner = Window.GetWindow(this);
                _carobnjakPonudaRadniNalog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _carobnjakPonudaRadniNalog.ShowDialog();
            }
        }


        private void Window_StateChanged(object sender, EventArgs e)
        {
            //Window.GetWindow(ponuda).WindowState = this.WindowState;
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window.GetWindow(ponuda).Visibility = System.Windows.Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(ponuda).Visibility = System.Windows.Visibility.Hidden;
        }


        private void buttonPosaljiEMail_Click(object sender, RoutedEventArgs e)
        {

            EMailPonuda _eMailPonuda = new EMailPonuda(this);
            _eMailPonuda.Owner = Window.GetWindow(this);
            _eMailPonuda.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _eMailPonuda.ShowDialog();

        }

        bool doEvent = true;
        private void textBoxServisnaKnjizica_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (doEvent)
            {
                if (e.Key == Key.Enter)
                {
                    if (((TextBox)sender).Text.Trim().Equals(""))
                    {
                        textBoxServisnaKnjizica.Tag = null;
                    }
                    else
                    {
                        try
                        {
                            DB.ServisnaKnjizica _servisnaKnjizica = DajServisnaKnjizica(((TextBox)sender).Text.Trim());

                            if (_servisnaKnjizica == null)
                            {
                                textBoxServisnaKnjizica.Tag = null;
                                MessageBox.Show("Servisna knjižica ne postoji.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                //zato sto kad kliknes enter da zatvoris ovaj msgbox....
                                doEvent = false;
                            }
                            else
                            {
                                buttonServisnaKnjizica.Focus();

                                textBoxServisnaKnjizica.Text = _servisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " + _servisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " + _servisnaKnjizica.TipAutomobila.OpisTabela.Opis;
                                textBoxServisnaKnjizica.Tag = _servisnaKnjizica.ServisnaKnjizicaID;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                            doEvent = false;
                        }
                    }
                }
            }
            else
            {
                doEvent = true;
            }

        }

        private void textBoxServisnaKnjizica_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {           
            //if (((TextBox)sender).Text.Trim().Equals(""))
            //{
            //    textBoxServisnaKnjizica.Tag = null;
            //}
            //else
            //{
            //    try
            //    {
            //        DB.ServisnaKnjizica _servisnaKnjizica = DajServisnaKnjizica(((TextBox)sender).Text.Trim());

            //        if (_servisnaKnjizica == null)
            //        {
            //            textBoxServisnaKnjizica.Tag = null;
            //            e.Handled = true;
            //            MessageBox.Show("Servisna knjižica ne postoji.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);                        
            //        }
            //        else
            //        {
            //            textBoxServisnaKnjizica.Text = _servisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " + _servisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " + _servisnaKnjizica.TipAutomobila.OpisTabela.Opis;
            //            textBoxServisnaKnjizica.Tag = _servisnaKnjizica.ServisnaKnjizicaID;
            //        }
            //    }
            //    catch (Exception ex)
            //    {
            //        textBoxServisnaKnjizica.Tag = null;
            //        e.Handled = true;
            //        MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);                       
            //    }
            //}
        }

        private DB.ServisnaKnjizica DajServisnaKnjizica(string sifra)
        {
            try
            {
                ObservableCollection<DB.ServisnaKnjizica> _servisnaKnjizicaLista = dBProksi.DajServisnaKnjizica(sifra);

                if (_servisnaKnjizicaLista.Count.Equals(0))
                {
                    return null;
                }
                else
                {
                    return _servisnaKnjizicaLista.First();
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }


        }

        private void textBoxServisnaKnjizicaSifra_PreviewKeyUp(object sender, KeyEventArgs e)
        {
            if (doEvent)
            {
                if (e.Key == Key.Enter)
                {
                    if (((TextBox)sender).Text.Trim().Equals(""))
                    {
                        textBoxServisnaKnjizica.Tag = null;
                    }
                    else
                    {
                        try
                        {
                            DB.ServisnaKnjizica _servisnaKnjizica = DajServisnaKnjizica(((TextBox)sender).Text.Trim());

                            if (_servisnaKnjizica == null)
                            {
                                textBoxServisnaKnjizica.Tag = null;
                                MessageBox.Show("Servisna knjižica ne postoji.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                                //zato sto kad kliknes enter da zatvoris ovaj msgbox....
                                doEvent = false;
                            }
                            else
                            {
                                buttonServisnaKnjizica.Focus();

                                textBoxServisnaKnjizica.Text = _servisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " + _servisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " + _servisnaKnjizica.TipAutomobila.OpisTabela.Opis;
                                textBoxServisnaKnjizica.Tag = _servisnaKnjizica.ServisnaKnjizicaID;
                                textBoxServisnaKnjizicaSifra.Text = _servisnaKnjizica.Sifra;
                            }
                        }
                        catch (Exception ex)
                        {
                            MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                            doEvent = false;
                        }
                    }
                }
            }
            else
            {
                doEvent = true;
            }
        }

        private void textBoxServisnaKnjizicaSifra_PreviewLostKeyboardFocus(object sender, KeyboardFocusChangedEventArgs e)
        {
            if (((TextBox)sender).Text.Trim().Equals(""))
            {
                textBoxServisnaKnjizica.Tag = null;
            }
            else
            {
                try
                {
                    DB.ServisnaKnjizica _servisnaKnjizica = DajServisnaKnjizica(((TextBox)sender).Text.Trim());

                    if (_servisnaKnjizica == null)
                    {
                        textBoxServisnaKnjizica.Tag = null;
                        e.Handled = true;
                        MessageBox.Show("Servisna knjižica ne postoji.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    else
                    {
                        textBoxServisnaKnjizica.Text = _servisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " + _servisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " + _servisnaKnjizica.TipAutomobila.OpisTabela.Opis;
                        textBoxServisnaKnjizica.Tag = _servisnaKnjizica.ServisnaKnjizicaID;
                        textBoxServisnaKnjizicaSifra.Text = _servisnaKnjizica.Sifra;
                    }
                }
                catch (Exception ex)
                {
                    textBoxServisnaKnjizica.Tag = null;
                    e.Handled = true;
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonFizickoLiceServisnaKnjizica_Click(object sender, RoutedEventArgs e)
        {
            FizickoLiceServisnaKnjizicaDetaljno _fizickoLiceServisnaKnjizicaDetaljno = new FizickoLiceServisnaKnjizicaDetaljno(this);
            //_ponudaPretraga.WindowStyle = WindowStyle.ToolWindow;
            _fizickoLiceServisnaKnjizicaDetaljno.Owner = Window.GetWindow(this);
            _fizickoLiceServisnaKnjizicaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _fizickoLiceServisnaKnjizicaDetaljno.ShowDialog();
        }
    }
}
