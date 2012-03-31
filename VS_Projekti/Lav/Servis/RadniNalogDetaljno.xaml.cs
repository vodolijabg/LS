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
using System.Globalization;
using Microsoft.Win32;
using System.IO;
using System.ServiceModel;

namespace Servis
{
    /// <summary>
    /// Interaction logic for RadniNalogDetaljno.xaml
    /// </summary>
    public partial class RadniNalogDetaljno : Window
    {
        App.Stanje stanje;
        bool zakljucajNalog = false;

        DB.DBProksi dBProksi;
        Servis.RadniNalog radniNalog;

        public RadniNalogDetaljno()
        {
            InitializeComponent();
        }

        public RadniNalogDetaljno(Servis.RadniNalog radniNalog, bool izmeniTrenutni): this()
        {
            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.radniNalog = radniNalog;

            if (izmeniTrenutni)
            {
                gridRadniNalog.DataContext = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;

                listViewStavkaUsluga.ItemsSource = ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

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

                if (((DB.RadniNalog)gridRadniNalog.DataContext).Zakljucan)
                {
                    UStanjeZaglavlje(App.Stanje.IzgasiSve);
                    UStanjeStavkaUsluga(App.Stanje.IzgasiSve);
                    UStanjeStavkaArtikal(App.Stanje.IzgasiSve);
                }

            }
            else
            {
                stanje = App.Stanje.Unos;

                UStanjeStavkaUsluga(App.Stanje.IzgasiSve);
                UStanjeStavkaArtikal(App.Stanje.IzgasiSve);
            }
        }

        private void UStanjeZaglavlje(App.Stanje stanje)
        {
            textBoxID.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            textBoxServisnaKnjizica.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            buttonServisnaKnjizica.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            textBoxRadnik.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            textBoxVreme.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            textBoxNapomena.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            textBoxKilometraza.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            textBoxRegistarskiBroj.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            datePickerDatumRegistracije.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            textBoxPredvidjenoVremeMinuta.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            buttonSacuvajINovi.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            buttonSacuvaj.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            buttonSacuvajIZatvori.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            buttonRezervisiDelove.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
            buttonZakljucaj.IsEnabled = !stanje.Equals(App.Stanje.IzgasiSve);
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
                int _kilometraza;
                int _predvidjenoVreme;

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
                else if (textBoxKilometraza.Text.Trim() != "" && !Int32.TryParse(textBoxKilometraza.Text, out _kilometraza))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Kilometraza.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxPredvidjenoVremeMinuta.Text.Trim() != "" && !Int32.TryParse(textBoxPredvidjenoVremeMinuta.Text, out _predvidjenoVreme))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Predvidjeno vreme.");
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
                        DB.RadniNalog _radniNalog = new DB.RadniNalog
                        {
                            KorisnikProgramaID = _korisnikPrograma.KorisnikProgramaID,
                            ServisnaKnjizicaID = Convert.ToInt32(textBoxServisnaKnjizica.Tag.ToString()),
                            RadnikID = App.Radnik.RadnikID,
                            Vreme = DateTime.Now,
                            PredvidjenoVremeMinuta = textBoxPredvidjenoVremeMinuta.Text.Trim() == "" ? 0 : Convert.ToInt32(textBoxPredvidjenoVremeMinuta.Text.Trim()),
                            AutomatskiDodeliPredvidjenoVreme = false,
                            RegistarskiBroj = textBoxRegistarskiBroj.Text.Trim() == "" ? null : textBoxRegistarskiBroj.Text.Trim(),
                            DatumRegistracije = datePickerDatumRegistracije.SelectedDate == null ? null : datePickerDatumRegistracije.SelectedDate,
                            Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                            RezervisaniDelovi = false,
                            Zakljucan = zakljucajNalog,
                            Status = 'I',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };
                        if (textBoxKilometraza.Text.Trim() != "")
                        {
                            _radniNalog.Kilometraza = Convert.ToInt32(textBoxKilometraza.Text.Trim());
                        }

                        dBProksi.UnesiRadniNalog(_radniNalog);

                        ObservableCollection<DB.RadniNalog> _radniNalogLista = (ObservableCollection<DB.RadniNalog>)radniNalog.listViewRadniNalog.ItemsSource;
                        _radniNalogLista.Add(_radniNalog);
                        radniNalog.listViewRadniNalog.SelectedItem = _radniNalog;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.RadniNalog _radniNalog = new DB.RadniNalog
                        {
                            RadniNalogID = Convert.ToInt32(textBoxID.Text.Trim()),
                            KorisnikProgramaID = _korisnikPrograma.KorisnikProgramaID,
                            ServisnaKnjizicaID = Convert.ToInt32(textBoxServisnaKnjizica.Tag.ToString()),
                            RadnikID = App.Radnik.RadnikID,
                            Vreme = DateTime.Now,
                            PredvidjenoVremeMinuta = textBoxPredvidjenoVremeMinuta.Text.Trim() == "" ? 0 : Convert.ToInt32(textBoxPredvidjenoVremeMinuta.Text.Trim()),
                            RegistarskiBroj = textBoxRegistarskiBroj.Text.Trim() == "" ? null : textBoxRegistarskiBroj.Text.Trim(),
                            DatumRegistracije = datePickerDatumRegistracije.SelectedDate == null ? null : datePickerDatumRegistracije.SelectedDate,
                            Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                            RezervisaniDelovi = false,
                            Zakljucan = zakljucajNalog,
                            Status = 'U',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };
                        if (textBoxKilometraza.Text.Trim() != "")
                        {
                            _radniNalog.Kilometraza = Convert.ToInt32(textBoxKilometraza.Text.Trim());
                        }

                        dBProksi.IzmeniRadniNalog(_radniNalog, (DB.RadniNalog)gridRadniNalog.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujRadniNalog(int radniNalogID)
        {
            ObservableCollection<DB.RadniNalog> _radniNalogLista = (ObservableCollection<DB.RadniNalog>)radniNalog.listViewRadniNalog.ItemsSource;
            bool _postoji = false;

            if (!radniNalog.listViewRadniNalog.Items.Count.Equals(0))
            {
                foreach (DB.RadniNalog item in _radniNalogLista)
                {
                    if (item.RadniNalogID.Equals(radniNalogID))
                    {
                        radniNalog.listViewRadniNalog.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    radniNalog.listViewRadniNalog.SelectedIndex = 0;
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

        public void OsveziRadniNalog()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.RadniNalog _trenutni = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;

                ObservableCollection<DB.RadniNalog> _radniNalogLista = (ObservableCollection<DB.RadniNalog>)radniNalog.listViewRadniNalog.ItemsSource;

                if (!_radniNalogLista.Count.Equals(0))
                {
                    radniNalog.listViewRadniNalog.ItemsSource = dBProksi.OsveziRadniNalog(_radniNalogLista);

                    if (_trenutni != null)
                    {
                        SelektujRadniNalog(_trenutni.RadniNalogID);
                    }
                    gridRadniNalog.DataContext = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;
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
                    DB.RadniNalog _trenutnaRadniNalog = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;
                    DB.StavkaUsluga _trenutnaStavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;
                    DB.StavkaArtikal _trenutnaStavkaArtikal = (DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem;

                    OsveziRadniNalog();

                    radniNalog.UStanje(App.Stanje.Detaljno);

                    //stanje = App.Stanje.Izmena;
                    //radniNalog.UStanje(App.Stanje.Detaljno);

                    //UStanjeStavkaUsluga(App.Stanje.Osnovno);
                    //UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                    //ako je i nakon osvezavanja podataka ostao selektovan isti radniNalog
                    if (radniNalog.listViewRadniNalog.SelectedItem != null && ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).RadniNalogID == _trenutnaRadniNalog.RadniNalogID)
                    {
                        listViewStavkaUsluga.ItemsSource = ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

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
                        gridRadniNalog.DataContext = null;
                        listViewStavkaUsluga.ItemsSource = null;
                        listViewStavkaArtikal.ItemsSource = null;

                        stanje = App.Stanje.Unos;

                        MessageBox.Show("RadniNalog je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                    OsveziRadniNalog();

                    radniNalog.UStanje(App.Stanje.Detaljno);

                    gridRadniNalog.DataContext = null;
                    listViewStavkaUsluga.ItemsSource = null;
                    listViewStavkaArtikal.ItemsSource = null;

                    stanje = App.Stanje.Unos;
                    radniNalog.UStanje(App.Stanje.Detaljno);

                    UStanjeStavkaUsluga(App.Stanje.IzgasiSve);
                    UStanjeStavkaArtikal(App.Stanje.IzgasiSve);
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
                    OsveziRadniNalog();

                    radniNalog.UStanje(App.Stanje.Detaljno);

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

            if (Window.GetWindow(radniNalog).WindowState == WindowState.Normal)
            {
                _naw.Width = Window.GetWindow(radniNalog).ActualWidth;
                _naw.Height = Window.GetWindow(radniNalog).ActualHeight;
            }
            else
            {
                _naw.WindowState = Window.GetWindow(radniNalog).WindowState;
            }


            _naw.ShowDialog();
        }

        private void textBoxServisnaKnjizica_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void listViewStavkaUsluga_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;

            if (_stavkaUsluga != null)
            {
                listViewStavkaArtikal.ItemsSource = new ObservableCollection<DB.StavkaArtikal>(_stavkaUsluga.StavkaArtikals.ToList()).Where(f => f.Status != 'D').OrderBy(o => o.ArtikalCenaBezPoreza);
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


        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender.ToString().EndsWith("DB.StavkaUsluga"))
            {
                if (!((DB.RadniNalog)gridRadniNalog.DataContext).Zakljucan)
                {
                    DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;

                    if (_stavkaUsluga != null)
                    {
                        RadniNalogStavkaUslugaDetaljno _radniNalogStavkaUslugaDetaljno = new RadniNalogStavkaUslugaDetaljno(this, true);
                        //_radniNalogStavkaUslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                        _radniNalogStavkaUslugaDetaljno.Owner = Window.GetWindow(this);
                        _radniNalogStavkaUslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        _radniNalogStavkaUslugaDetaljno.ShowDialog();
                    } 
                }

            }
            else if (sender.ToString().EndsWith("DB.StavkaArtikal"))
            {
                if (!((DB.RadniNalog)gridRadniNalog.DataContext).Zakljucan)
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
            }
            else
            {
                MessageBox.Show("Nepoznat tip: " + sender.ToString(), "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }        
        

        private void buttonDodajStavkaUsluga_Click(object sender, RoutedEventArgs e)
        {
            RadniNalogStavkaUslugaDetaljno _radniNalogStavkaUslugaDetaljno = new RadniNalogStavkaUslugaDetaljno(this, false);
            //_stavkaUslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _radniNalogStavkaUslugaDetaljno.Owner = Window.GetWindow(this);
            _radniNalogStavkaUslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _radniNalogStavkaUslugaDetaljno.ShowDialog();
        }
        private void buttonIzmeniStavkaUsluga_Click(object sender, RoutedEventArgs e)
        {
            DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;

            if (_stavkaUsluga != null)
            {
                RadniNalogStavkaUslugaDetaljno _radniNalogStavkaUslugaDetaljno = new RadniNalogStavkaUslugaDetaljno(this, true);
                //_radniNalogStavkaUslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _radniNalogStavkaUslugaDetaljno.Owner = Window.GetWindow(this);
                _radniNalogStavkaUslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _radniNalogStavkaUslugaDetaljno.ShowDialog();
            }
        }
        private void buttonObrisiStavkaUsluga_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiRadniNalogStavkaUsluga((DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem, ((DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem).RadniNalogStavkaUsluga, App.Radnik);

                    DB.RadniNalog _trenutniRadniNalog = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;

                    OsveziRadniNalog();

                    //ako je i nakon osvezavanja podataka ostala selektovana ista ponuda
                    if (radniNalog.listViewRadniNalog.SelectedItem != null && ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).RadniNalogID == _trenutniRadniNalog.RadniNalogID)
                    {
                        listViewStavkaUsluga.ItemsSource = ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

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
                        gridRadniNalog.DataContext = null;
                        listViewStavkaUsluga.ItemsSource = null;
                        listViewStavkaArtikal.ItemsSource = null;

                        stanje = App.Stanje.Unos;

                        MessageBox.Show("RadniNalog je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                DB.RadniNalog _trenutni = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;
                DB.StavkaUsluga _trenutnaStavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;
                DB.StavkaArtikal _trenutnaStavkaArtikal = (DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem;

                OsveziRadniNalog();

                //ako je i nakon osvezavanja podataka ostala selektovana ista RadniNalog
                if (radniNalog.listViewRadniNalog.SelectedItem != null && ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).RadniNalogID == _trenutni.RadniNalogID)
                {
                    listViewStavkaUsluga.ItemsSource = ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

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
                    gridRadniNalog.DataContext = null;
                    listViewStavkaUsluga.ItemsSource = null;
                    listViewStavkaArtikal.ItemsSource = null;

                    stanje = App.Stanje.Unos;

                    MessageBox.Show("Radni nalog je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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
                DB.RadniNalog _trenutni = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;
                DB.StavkaUsluga _trenutnaStavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;
                DB.StavkaArtikal _trenutnaStavkaArtikal = (DB.StavkaArtikal)listViewStavkaArtikal.SelectedItem;

                OsveziRadniNalog();

                //ako je i nakon osvezavanja podataka ostala selektovana ista ponuda
                if (radniNalog.listViewRadniNalog.SelectedItem != null && ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).RadniNalogID == _trenutni.RadniNalogID)
                {
                    listViewStavkaUsluga.ItemsSource = ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

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
                    gridRadniNalog.DataContext = null;
                    listViewStavkaUsluga.ItemsSource = null;
                    listViewStavkaArtikal.ItemsSource = null;

                    stanje = App.Stanje.Unos;

                    MessageBox.Show("Radni nalog je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
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

                    DB.RadniNalog _trenutniRadniNalog = (DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem;
                    DB.StavkaUsluga _trenutnaStavkaUsluga = (DB.StavkaUsluga)listViewStavkaUsluga.SelectedItem;

                    OsveziRadniNalog();

                    //ako je i nakon osvezavanja podataka ostala selektovana ista ponuda
                    if (radniNalog.listViewRadniNalog.SelectedItem != null && ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).RadniNalogID == _trenutniRadniNalog.RadniNalogID)
                    {
                        listViewStavkaUsluga.ItemsSource = ((DB.RadniNalog)radniNalog.listViewRadniNalog.SelectedItem).StavkaUslugas.Where(f => f.Status != 'D');

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
                        gridRadniNalog.DataContext = null;
                        listViewStavkaUsluga.ItemsSource = null;
                        listViewStavkaArtikal.ItemsSource = null;

                        stanje = App.Stanje.Unos;

                        MessageBox.Show("Radni nalog je obrisao drugi korisnik.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            Window.GetWindow(radniNalog).Visibility = System.Windows.Visibility.Visible;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(radniNalog).Visibility = System.Windows.Visibility.Hidden;
        }


        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TabControl _tab = (TabControl)sender;
                TabItem _trenutiItem = (TabItem)_tab.SelectedItem;

                DB.RadniNalog _radniNalog = (DB.RadniNalog)gridRadniNalog.DataContext;

                reportViewerRadniNalog.Reset();

                if (_trenutiItem != null && _trenutiItem.Header == tabItemStampa.Header && _radniNalog != null)
                {
                    decimal _totalMinSaPDV_Ponuda = 0;
                    decimal _totalMaxSaPDV_Ponuda = 0;

                    List<DB.StampaRadniNalog> _stampaRadniNalogLista = new List<DB.StampaRadniNalog>();
                    List<DB.StampaRadniNalogStavkaUsluga> _stampaRadniNalogStavkaUslugaLista = new List<DB.StampaRadniNalogStavkaUsluga>();
                    List<DB.StampaRadniNalogStavkaArtikal> _stampaRadniNalogStavkaArtikalLista = new List<DB.StampaRadniNalogStavkaArtikal>();

                    #region Napuni _stampaRadniNalogLista - uvek ima jedan red
                    DB.StampaRadniNalog _stampaRadniNalog = new DB.StampaRadniNalog
                    {
                        RadniNalogID = _radniNalog.RadniNalogID,
                        ServisnaKnjizicaSifra = _radniNalog.ServisnaKnjizica.Sifra,
                        TipAutomobila = _radniNalog.ServisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " +
                                        _radniNalog.ServisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " +
                                        _radniNalog.ServisnaKnjizica.TipAutomobila.OpisTabela.Opis,
                        PartnerNaziv = _radniNalog.ServisnaKnjizica.PoslovniPartnerID != null ? _radniNalog.ServisnaKnjizica.PoslovniPartner.SkracenNaziv : _radniNalog.ServisnaKnjizica.FizickoLice.Ime,
                        RadnikSifra = _radniNalog.Radnik.Sifra,
                        RadnikNadimak = _radniNalog.Radnik.Nadimak,
                        Vreme = _radniNalog.Vreme,
                        PredvidjenoVremeMinuta = 1700,
                        //Kilometraza = (int)_radniNalog.Kilometraza,
                        //DatumRegistracije = (DateTime)_radniNalog.DatumRegistracije,
                        RegistarskiBroj = _radniNalog.RegistarskiBroj,
                        Napomena = _radniNalog.Napomena
                    };

                    if (_radniNalog.Kilometraza != null)
                    {
                        _stampaRadniNalog.Kilometraza = (int)_radniNalog.Kilometraza;
                    }
                    if (_radniNalog.DatumRegistracije != null)
                    {
                        _stampaRadniNalog.DatumRegistracije = (DateTime)_radniNalog.DatumRegistracije;
                    }
                         
                    
                    _stampaRadniNalogLista.Add(_stampaRadniNalog);
                    #endregion

                    //za svaku uslugu
                    foreach (DB.StavkaUsluga itemU in _radniNalog.StavkaUslugas.Where(f => f.Status != 'D'))
                    {
                        //vrednost usluge ulazi i u Min i u Max vrednost Totala izvestaja
                        decimal _vrednostSaPDVStavkaUsluga = (itemU.UslugaCenaBezPoreza + ((itemU.UslugaCenaBezPoreza / 100) * itemU.PoreskaStopa.VrednostProcenata)) * itemU.UslugaKolicina;
                        //Convert.ToDecimal da zaokruzim na dve decimale
                        _totalMaxSaPDV_Ponuda += Convert.ToDecimal(_vrednostSaPDVStavkaUsluga.ToString(".00"), App.cultureInfo);
                        _totalMinSaPDV_Ponuda += Convert.ToDecimal(_vrednostSaPDVStavkaUsluga.ToString(".00"), App.cultureInfo);

                        //ako Usluga nema artikala
                        if (itemU.StavkaArtikals.Count().Equals(0))
                        {
                            DB.StampaRadniNalogStavkaUsluga _stampaRadniNalogStavkaUsluga = new DB.StampaRadniNalogStavkaUsluga
                            {
                                StavkaUslugaID = itemU.StavkaUslugaID,
                                Usluga = itemU.Usluga.VrstaUsluge.Naziv + " " + itemU.Usluga.NosilacGrupe.Naziv + " " + itemU.Usluga.Nivo.Naziv + " " + itemU.Usluga.Pozicija.Naziv,
                                UslugaKolicina = itemU.UslugaKolicina,
                                UslugaCenaBezPoreza = itemU.UslugaCenaBezPoreza,
                                UslugaPoreskaStopaVrednost = itemU.PoreskaStopa.VrednostProcenata,
                                RadniNalogStavkaUslugaID = itemU.RadniNalogStavkaUsluga.RadniNalogStavkaUslugaID,
                                PredvidjenoVremeMinuta = itemU.RadniNalogStavkaUsluga.PredvidjenoVremeMinuta,
                                //UtrosenoVremeMinuta = (int)itemU.RadniNalogStavkaUsluga.UtrosenoVremeMinuta,
                                RadniNalogStatus = itemU.RadniNalogStavkaUsluga.RadniNalogStatus.Naziv,
                                Napomena = itemU.RadniNalogStavkaUsluga.Napomena
                            };
                            if (itemU.RadniNalogStavkaUsluga.UtrosenoVremeMinuta != null)
                            {
                                _stampaRadniNalogStavkaUsluga.UtrosenoVremeMinuta = (int)itemU.RadniNalogStavkaUsluga.UtrosenoVremeMinuta;
                            }
                            _stampaRadniNalogStavkaUslugaLista.Add(_stampaRadniNalogStavkaUsluga);
                        }
                        else
                        {
                            #region Dodaj artikle u _stampaRadniNalogStavkaArtikalLista
                            foreach (DB.StavkaArtikal itemA in itemU.StavkaArtikals.Where(f => f.Status != 'D'))
                            {
                                DB.StampaRadniNalogStavkaArtikal _stampaRadniNalogStavkaArtikal = new DB.StampaRadniNalogStavkaArtikal
                                {
                                    StavkaArtikalID = itemA.StavkaArtikalID,
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
                                    UslugaPoreskaStopaVrednost = itemU.PoreskaStopa.VrednostProcenata,
                                    RadniNalogStavkaUslugaID = itemU.RadniNalogStavkaUsluga.RadniNalogStavkaUslugaID,
                                    PredvidjenoVremeMinuta = itemU.RadniNalogStavkaUsluga.PredvidjenoVremeMinuta,
                                    //UtrosenoVremeMinuta = (int)itemU.RadniNalogStavkaUsluga.UtrosenoVremeMinuta,
                                    RadniNalogStatus = itemU.RadniNalogStavkaUsluga.RadniNalogStatus.Naziv,
                                    Napomena = itemU.RadniNalogStavkaUsluga.Napomena
                                };
                                if (itemU.RadniNalogStavkaUsluga.UtrosenoVremeMinuta != null)
                                {
                                    _stampaRadniNalogStavkaArtikal.UtrosenoVremeMinuta = (int)itemU.RadniNalogStavkaUsluga.UtrosenoVremeMinuta;
                                }

                                _stampaRadniNalogStavkaArtikalLista.Add(_stampaRadniNalogStavkaArtikal);
                            }

                            #endregion
                        }
                    }


                    //za StampaRadniNalogStavkaArtikal dodaj vrednost za _totalMinSaPDV_Ponuda i _totalMaxSaPDV_Ponuda
                    List<int> _stampaStavkaArtikalLista_ObradjeneUsluge = new List<int>();
                    foreach (DB.StampaRadniNalogStavkaArtikal item in _stampaRadniNalogStavkaArtikalLista)
                    {
                        var _upit = from u in _stampaRadniNalogStavkaArtikalLista
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

                    //_stampaRadniNalog.TotalMinSaPDV = _totalMinSaPDV_Ponuda;
                    //_stampaRadniNalog.TotalMaxSaPDV = _totalMaxSaPDV_Ponuda;

                    #region Prikazi izvestaj

                    reportViewerRadniNalog.LocalReport.ReportEmbeddedResource = "Servis.RadniNalogStampa.rdlc";
                    reportViewerRadniNalog.ProcessingMode = ProcessingMode.Local;

                    ReportDataSource _reportDataSourceRadniNalog = new ReportDataSource("DS_StampaRadniNalog", _stampaRadniNalogLista);
                    ReportDataSource _reportDataSourceRadniNalogStavkaArtikal = new ReportDataSource("DS_StampaRadniNalogStavkaArtikal", _stampaRadniNalogStavkaArtikalLista);
                    ReportDataSource _reportDataSourceRadniNalogStavkaUsluga = new ReportDataSource("DS_StampaRadniNalogStavkaUsluga", _stampaRadniNalogStavkaUslugaLista);

                    reportViewerRadniNalog.LocalReport.DataSources.Add(_reportDataSourceRadniNalog);
                    reportViewerRadniNalog.LocalReport.DataSources.Add(_reportDataSourceRadniNalogStavkaArtikal);
                    reportViewerRadniNalog.LocalReport.DataSources.Add(_reportDataSourceRadniNalogStavkaUsluga);
                    reportViewerRadniNalog.SetDisplayMode(DisplayMode.PrintLayout);
                    reportViewerRadniNalog.ZoomMode = ZoomMode.PageWidth;

                    reportViewerRadniNalog.RefreshReport();
                    #endregion
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void buttonRezervisiDelove_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;

                buttonSacuvaj_Click(null, null);
                rezervisiDelove();
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


        private void buttonZakljucaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                zakljucajNalog = true;

                buttonSacuvaj_Click(null, null);

                //rezervisiDelove();

                zakljucajNalog = false;

                UStanjeZaglavlje(App.Stanje.IzgasiSve);
                UStanjeStavkaUsluga(App.Stanje.IzgasiSve);
                UStanjeStavkaArtikal(App.Stanje.IzgasiSve);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }
   
        private void rezervisiDelove()
        {

            List<string> _radniNalog = new List<string>();
            //MessageBox.Show("Opcija nije implementirana.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

            DB.RadniNalog _rn = (DB.RadniNalog)gridRadniNalog.DataContext;

            string _imeFajla = "NL" + _rn.RadniNalogID.ToString("000000");

            string _brojDokumenta = _rn.RadniNalogID.ToString("000000");
            string _mestoTroska = "AS1";
            string _datum = _rn.Vreme.ToString("yyyy.MM.dd");
            string _komercijalistaSifra = _rn.Radnik.Sifra;
            if (_komercijalistaSifra.StartsWith("K") || _komercijalistaSifra.StartsWith("R"))
            {
                //_komercijalistaSifra = _komercijalistaSifra.Substring(1, _komercijalistaSifra.Length - 2);
                _komercijalistaSifra = _komercijalistaSifra.Substring(2);
            }
            if (_komercijalistaSifra.Length > 2)
            {
                //neka pukne import u roban   
            }
            string _poslovniPartnerSifra = _rn.ServisnaKnjizica.FizickoLice != null ? "2" : _rn.ServisnaKnjizica.PoslovniPartner.Sifra;
            if (_poslovniPartnerSifra.Length > 5)
            {
                //neka pukne import u roban   
            }
            string _registarskiBroj = _rn.ServisnaKnjizica.RegistarskiBroj != null ? _rn.ServisnaKnjizica.RegistarskiBroj : "";
            if (_registarskiBroj.Length > 10)
            {
                _registarskiBroj = _registarskiBroj.Substring(0, 10);
            }
            string _brojServisneKnjizice = _rn.ServisnaKnjizicaID.ToString();
            if (_brojServisneKnjizice.Length > 6)
            {
                _brojServisneKnjizice = _brojServisneKnjizice.Substring(0, 6);
            }
            string _kilometraza = _rn.Kilometraza.ToString() != null ? _rn.Kilometraza.ToString() : "";
            if (_kilometraza.Length > 6)
            {
                _kilometraza = _kilometraza.Substring(0, 6);
            }
            string _tipAutomobilaID = _rn.ServisnaKnjizica.TipAutomobilaID.ToString();
            string _datumRegistracije = _rn.DatumRegistracije != null ? ((DateTime)_rn.DatumRegistracije).ToString("yyyy.MM.dd") : "";
            string _brojSasije = _rn.ServisnaKnjizica.BrojSasije != null ? _rn.ServisnaKnjizica.BrojSasije : "";
            if (_brojSasije.Length > 20)
            {
                _brojSasije = _brojSasije.Substring(0, 20);
            }
            string _brojMotora = _rn.ServisnaKnjizica.BrojMotora != null ? _rn.ServisnaKnjizica.BrojMotora : "";
            if (_brojMotora.Length > 20)
            {
                _brojMotora = _brojMotora.Substring(0, 20);
            }
            string _godiste = _rn.ServisnaKnjizica.Godiste != null ? _rn.ServisnaKnjizica.Godiste.ToString() : "";
            if (_godiste.Length > 7)
            {
                _godiste = _godiste.Substring(0, 7);
            }
            string _zapazanjeVlasnika = "";
            string _zapazanjeServisa = "";

            _radniNalog.Add(_brojDokumenta + "\t" + _mestoTroska + "\t" + _datum + "\t" + _komercijalistaSifra.PadRight(2, ' '));
            _radniNalog.Add(_poslovniPartnerSifra.PadRight(5, ' ') + "\t" + _registarskiBroj.PadRight(10, ' ') + "\t" + _brojServisneKnjizice.PadRight(6, ' ') + "\t" + _kilometraza.PadRight(6, ' '));
            _radniNalog.Add(_tipAutomobilaID + "\t" + _datumRegistracije + "\t" + _brojSasije.PadRight(20, ' ') + "\t" + _brojMotora.PadRight(20, ' '));
            _radniNalog.Add(_godiste.PadRight(7, ' ') + "\t" + _zapazanjeVlasnika + "\t" + _zapazanjeServisa + "\t" + "" + "\t");

            foreach (DB.StavkaUsluga itemU in _rn.StavkaUslugas.Where(f => f.Status != 'D'))
            {
                string _uslugaSifra = itemU.Usluga.Sifra;
                if (_uslugaSifra.Length > 6)
                {
                    _uslugaSifra = _uslugaSifra.Substring(0, 6);
                }

                string _stavkaUsluga = "1" + "\t" + _uslugaSifra.PadRight(6, ' ') + "\t" + itemU.UslugaKolicina.ToString("0.00", CultureInfo.InvariantCulture) + "\t" + itemU.UslugaCenaBezPoreza.ToString("0.00", CultureInfo.InvariantCulture);

                _radniNalog.Add(_stavkaUsluga);

                foreach (DB.StavkaArtikal itemA in itemU.StavkaArtikals.Where(f => f.Status != 'D'))
                {
                    ObservableCollection<DB.Artikal> _artikalLista = dBProksi.NadjiArtikal(itemA.ArtikalBrojProizvodjaca, itemA.ArtikalProizvodjacNaziv);

                    if(_artikalLista.Count != 1)
                    {
                        throw new Exception("Artikal ne postoji");
                    }

                    string _artikalSifra = _artikalLista.First().Sifra == null ? "-" + _artikalLista.First().BrojProizvodjaca : _artikalLista.First().Sifra;
                    if (_artikalSifra.Length > 10)
                    {
                        _artikalSifra = _artikalSifra.Substring(0, 10);
                    }

                    //string _stavkaArtikal = "2" + "\t" + _artikalSifra.PadRight(10, ' ') + "\t" + itemA.ArtikalKolicina.ToString("0.00", CultureInfo.InvariantCulture) + "\t" + itemA.ArtikalCenaBezPoreza.ToString("0.00", CultureInfo.InvariantCulture);

                    string _stavkaArtikal = "2" + "\t" + _artikalSifra.PadRight(10, ' ') + "\t" + itemA.ArtikalKolicina.ToString("0.00", CultureInfo.InvariantCulture) + "\t" + (itemA.ArtikalCenaBezPoreza + ((itemA.ArtikalCenaBezPoreza / 100) * itemA.PoreskaStopa.VrednostProcenata)).ToString("0.00", CultureInfo.InvariantCulture);


                    _radniNalog.Add(_stavkaArtikal);
                }

            }
            
            //RadniNalogExportPrint _radniNalogExportPrint = new RadniNalogExportPrint(_imeFajla, _radniNalog);
            ////_stavkaUslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
            //_radniNalogExportPrint.Owner = Window.GetWindow(this);
            //_radniNalogExportPrint.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //_radniNalogExportPrint.ShowDialog();

            //SaveFileDialog _sacuvajFajlSaveFileDialog = new SaveFileDialog();
            //_sacuvajFajlSaveFileDialog.Filter = "TXT fajl (*.txt)|*.txt";
            //_sacuvajFajlSaveFileDialog.Title = "Sačuvaj kao";
            //_sacuvajFajlSaveFileDialog.RestoreDirectory = true;
            //_sacuvajFajlSaveFileDialog.FileName = _imeFajla;

            //if (_sacuvajFajlSaveFileDialog.ShowDialog() == true)
            //{
                //textBoxFajl.Text = _sacuvajFajlSaveFileDialog.FileName;
                //StreamWriter _streamWriter = new StreamWriter(_sacuvajFajlSaveFileDialog.FileName);

            Servis.ExportRadniNalog.ExportRadniNalogWebServisClient client = new Servis.ExportRadniNalog.ExportRadniNalogWebServisClient();
                try
                {                                        
                    client.Endpoint.Address = new EndpointAddress("http://serverp/LavWS/ExportRadniNalogWebServis.svc");

                    client.ExportRadniNalog(_rn.RadniNalogID, _radniNalog.ToArray(), App.Radnik.RadnikID);

                    //foreach (string s in _radniNalog)
                    //{
                    //    _streamWriter.WriteLine(s);
                    //}

                    MessageBox.Show("Radni nalog uspešno poslat.", "Info", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    client.Close();
                }
            //}

        }
    }
}
