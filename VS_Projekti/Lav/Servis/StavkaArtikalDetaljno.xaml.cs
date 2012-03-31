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
using System.Windows.Navigation;
using System.Collections.ObjectModel;
using System.Globalization;

namespace Servis
{
    /// <summary>
    /// Interaction logic for StavkaArtikalDetaljno.xaml
    /// </summary>
    public partial class StavkaArtikalDetaljno : Window
    {
        public App.Stanje stanje;

        DB.DBProksi dBProksi;
        public Servis.PonudaDetaljno ponudaDetaljno;
        Servis.RadniNalogDetaljno radniNalogDetaljno;

        public StavkaArtikalDetaljno()
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

        }

        public StavkaArtikalDetaljno(Servis.PonudaDetaljno ponudaDetaljno, bool izmeniTrenutni) : this()   
        {
            this.ponudaDetaljno = ponudaDetaljno;

            try
            {
                ObservableCollection<DB.NosilacGrupe> _nosiociGrupe = new ObservableCollection<DB.NosilacGrupe>(dBProksi.DajSveNosilacGrupe().ToList());
                comboBoxNosilacGrupe.ItemsSource = _nosiociGrupe;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            //referenca na StavkaUsluga, koristim samo za status IsEnable kontrole textBoxGrupisanje
            //gridStavkaArtikal.Tag = (DB.StavkaUsluga)ponudaDetaljno.listViewStavkaUsluga.SelectedItem;

            if (izmeniTrenutni)
            {
                gridStavkaArtikal.DataContext = (DB.StavkaArtikal)ponudaDetaljno.listViewStavkaArtikal.SelectedItem;
                stanje = App.Stanje.Izmena;

                //stvarno ne znam sto nece da sam selektuje pa moram ovako (vidi binding za SelectetItem)
                foreach (DB.NosilacGrupe item in comboBoxNosilacGrupe.Items)
                {
                    if (item.NosilacGrupeID == ((DB.StavkaArtikal)gridStavkaArtikal.DataContext).NosilacGrupeID)
                    {
                        comboBoxNosilacGrupe.SelectedItem = item;
                        break;
                    }
                }
            }


        }
        
        public StavkaArtikalDetaljno(Servis.RadniNalogDetaljno radniNalogDetaljno, bool izmeniTrenutni): this()
        {
            this.radniNalogDetaljno = radniNalogDetaljno;

            try
            {
                ObservableCollection<DB.NosilacGrupe> _nosiociGrupe = new ObservableCollection<DB.NosilacGrupe>(dBProksi.DajSveNosilacGrupe().ToList());
                comboBoxNosilacGrupe.ItemsSource = _nosiociGrupe;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (izmeniTrenutni)
            {
                gridStavkaArtikal.DataContext = (DB.StavkaArtikal)radniNalogDetaljno.listViewStavkaArtikal.SelectedItem;
                stanje = App.Stanje.Izmena;

                //stvarno ne znam sto nece da sam selektuje pa moram ovako (vidi binding za SelectetItem)
                foreach (DB.NosilacGrupe item in comboBoxNosilacGrupe.Items)
                {
                    if (item.NosilacGrupeID == ((DB.StavkaArtikal)gridStavkaArtikal.DataContext).NosilacGrupeID)
                    {
                        comboBoxNosilacGrupe.SelectedItem = item;
                        break;
                    }
                }
            }

            comboBoxNosilacGrupe.Visibility = System.Windows.Visibility.Collapsed;
            textBlockNosilacGrupe.Visibility = System.Windows.Visibility.Collapsed;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
        }      

        private void buttonArtikal_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow _naw = new NavigationWindow();

            _naw.Content = new Artikal(this);

            //sakrijem strelice za nazed i napred
            _naw.ShowsNavigationUI = false;
            if (ponudaDetaljno != null)
            {
                _naw.Owner = Window.GetWindow(ponudaDetaljno);
                if (Window.GetWindow(ponudaDetaljno).WindowState == WindowState.Normal)
                {
                    _naw.Width = Window.GetWindow(ponudaDetaljno).ActualWidth;
                    _naw.Height = Window.GetWindow(ponudaDetaljno).ActualHeight;
                }
                else
                {
                    _naw.WindowState = Window.GetWindow(ponudaDetaljno).WindowState;
                }
            }
            else if (radniNalogDetaljno != null)
            {
                _naw.Owner = Window.GetWindow(radniNalogDetaljno);
                if (Window.GetWindow(radniNalogDetaljno).WindowState == WindowState.Normal)
                {
                    _naw.Width = Window.GetWindow(radniNalogDetaljno).ActualWidth;
                    _naw.Height = Window.GetWindow(radniNalogDetaljno).ActualHeight;
                }
                else
                {
                    _naw.WindowState = Window.GetWindow(radniNalogDetaljno).WindowState;
                }
            }

            _naw.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            
            _naw.ShowDialog();
        }

        private void textBoxArtikal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        public bool Sacuvaj()
        {
            try
            {
                //za proveru tipa podataka
                Int32 _kolicina;
                decimal _cenaBezPoreza;

                if (textBoxArtikal.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi artikal.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxKolicina.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (!Int32.TryParse(textBoxKolicina.Text, out _kolicina))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxCenaBezPoreza.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u polje Cena bez poreza.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (!Decimal.TryParse(textBoxCenaBezPoreza.Text, out _cenaBezPoreza))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje polje Cena bez poreza.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else
                {
                    if (comboBoxNosilacGrupe.SelectedItem == null)
                    {
                        DB.NosilacGrupe _nosilacGrupe = null;

                        if (ponudaDetaljno != null)
                        {
                            _nosilacGrupe = ((DB.StavkaUsluga)ponudaDetaljno.listViewStavkaUsluga.SelectedItem).Usluga.NosilacGrupe;
                        }
                        else if (radniNalogDetaljno != null)
                        {
                            _nosilacGrupe = ((DB.StavkaUsluga)radniNalogDetaljno.listViewStavkaUsluga.SelectedItem).Usluga.NosilacGrupe;
                        }


                        foreach (DB.NosilacGrupe item in comboBoxNosilacGrupe.Items)
                        {
                            if (item.NosilacGrupeID == _nosilacGrupe.NosilacGrupeID)
                            {
                                comboBoxNosilacGrupe.SelectedItem = item;
                                break;
                            }
                        }
                    }

                    string[] _nizArtikal = textBoxArtikal.Tag.ToString().Split("$".ToCharArray());
                    string _brojProizvodjaca = _nizArtikal[0].ToString();
                    string _proizvodjacNaziv = _nizArtikal[1].ToString();
                    Int16 _proizvodjacID = Convert.ToInt16(_nizArtikal[2]);
                    string _artikalNaziv = _nizArtikal[3].ToString();

                    string[] _nizDobavljac = textBoxDobavljac.Tag.ToString().Split("$".ToCharArray());
                    //jedan je -1
                    int _poslovniPartnerID = Convert.ToInt32(_nizDobavljac[0]);
                    int _korisnikProgramaID = Convert.ToInt32(_nizDobavljac[1]);

                    if (stanje == App.Stanje.Unos)
                    {
                        DB.StavkaArtikal _stavkaArtikal = new DB.StavkaArtikal
                        {
                            ArtikalKolicina = Convert.ToInt32(textBoxKolicina.Text.Trim()),
                            ArtikalCenaBezPoreza = Convert.ToDecimal(textBoxCenaBezPoreza.Text.Trim(), App.cultureInfo),
                            ArtikalPoreskaStopaID = Convert.ToInt32(textBoxPoreskaStopa.Tag.ToString(), App.cultureInfo),
                            ArtikalNaziv = _artikalNaziv,
                            ArtikalBrojProizvodjaca = _brojProizvodjaca,
                            ArtikalProizvodjacNaziv = _proizvodjacNaziv,
                            ArtikalProizvodjacID = _proizvodjacID,
                            Status = 'I',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };
                        if (ponudaDetaljno != null)
                        {
                            _stavkaArtikal.StavkaUslugaID = ((DB.StavkaUsluga)ponudaDetaljno.listViewStavkaUsluga.SelectedItem).StavkaUslugaID;
                        }
                        else if (radniNalogDetaljno != null)
                        {
                            _stavkaArtikal.StavkaUslugaID = ((DB.StavkaUsluga)radniNalogDetaljno.listViewStavkaUsluga.SelectedItem).StavkaUslugaID;
                        }

                        if (comboBoxNosilacGrupe.SelectedItem == null)
                        {
                            _stavkaArtikal.NosilacGrupeID = ((DB.StavkaUsluga)ponudaDetaljno.listViewStavkaUsluga.SelectedItem).Usluga.NosilacGrupeID;
                        }
                        else
                        {
                            _stavkaArtikal.NosilacGrupeID = ((DB.NosilacGrupe)comboBoxNosilacGrupe.SelectedItem).NosilacGrupeID;
                        }

                        if (_poslovniPartnerID != -1)
                        {
                            _stavkaArtikal.PoslovniPartnerID = _poslovniPartnerID;
                        }
                        if (_korisnikProgramaID != -1)
                        {
                            _stavkaArtikal .KorisnikProgramaID= _korisnikProgramaID;
                        }

                        dBProksi.UnesiStavkaArtikal(_stavkaArtikal);

                        if (ponudaDetaljno != null)
                        {
                            DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)ponudaDetaljno.listViewStavkaUsluga.SelectedItem;
                            _stavkaUsluga.StavkaArtikals.Add(_stavkaArtikal);
                            ponudaDetaljno.listViewStavkaArtikal.SelectedItem = _stavkaArtikal;
                        }
                        else if (radniNalogDetaljno != null)
                        {
                            DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)radniNalogDetaljno.listViewStavkaUsluga.SelectedItem;
                            _stavkaUsluga.StavkaArtikals.Add(_stavkaArtikal);
                            radniNalogDetaljno.listViewStavkaArtikal.SelectedItem = _stavkaArtikal;
                        }

                        textBoxID.Text = _stavkaArtikal.StavkaArtikalID.ToString();

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.StavkaArtikal _stavkaArtikal = new DB.StavkaArtikal
                        {
                            StavkaArtikalID = Convert.ToInt32(textBoxID.Text),
                            ArtikalKolicina = Convert.ToInt32(textBoxKolicina.Text.Trim()),
                            ArtikalCenaBezPoreza = Convert.ToDecimal(textBoxCenaBezPoreza.Text.Trim(), App.cultureInfo),
                            ArtikalPoreskaStopaID = Convert.ToInt32(textBoxPoreskaStopa.Tag.ToString()),
                            ArtikalNaziv = _artikalNaziv,
                            ArtikalBrojProizvodjaca = _brojProizvodjaca,
                            ArtikalProizvodjacNaziv = _proizvodjacNaziv,
                            ArtikalProizvodjacID = _proizvodjacID,
                            Status = 'U',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };
                        if (ponudaDetaljno != null)
                        {
                            _stavkaArtikal.StavkaUslugaID = ((DB.StavkaUsluga)ponudaDetaljno.listViewStavkaUsluga.SelectedItem).StavkaUslugaID;
                        }
                        else if (radniNalogDetaljno != null)
                        {
                            _stavkaArtikal.StavkaUslugaID = ((DB.StavkaUsluga)radniNalogDetaljno.listViewStavkaUsluga.SelectedItem).StavkaUslugaID;
                        }

                        if (comboBoxNosilacGrupe.SelectedItem == null)
                        {
                            _stavkaArtikal.NosilacGrupeID = ((DB.StavkaUsluga)ponudaDetaljno.listViewStavkaUsluga.SelectedItem).Usluga.NosilacGrupeID;
                        }
                        else
                        {
                            _stavkaArtikal.NosilacGrupeID = ((DB.NosilacGrupe)comboBoxNosilacGrupe.SelectedItem).NosilacGrupeID;
                        }

                        if (_poslovniPartnerID != -1)
                        {
                            _stavkaArtikal.PoslovniPartnerID = _poslovniPartnerID;
                        }
                        if (_korisnikProgramaID != -1)
                        {
                            _stavkaArtikal.KorisnikProgramaID = _korisnikProgramaID;
                        }

                        if (ponudaDetaljno != null)
                        {
                            dBProksi.IzmeniStavkaArtikal(_stavkaArtikal, (DB.StavkaArtikal)ponudaDetaljno.listViewStavkaArtikal.SelectedItem);
                        }
                        else if (radniNalogDetaljno != null)
                        {
                            dBProksi.IzmeniStavkaArtikal(_stavkaArtikal, (DB.StavkaArtikal)radniNalogDetaljno.listViewStavkaArtikal.SelectedItem);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void buttonSacuvajINovi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {
                    comboBoxNosilacGrupe.SelectedItem = null;

                    if (ponudaDetaljno != null)
                    {
                        ponudaDetaljno.buttonOsveziStavkaArtikal_Click(null, null);
                        ponudaDetaljno.SelektujStavkaArtikal(Convert.ToInt32(textBoxID.Text));
                        gridStavkaArtikal.DataContext = ponudaDetaljno.listViewStavkaArtikal.SelectedItem;

                        gridStavkaArtikal.DataContext = null;
                        stanje = App.Stanje.Unos;

                        ponudaDetaljno.UStanjeStavkaArtikal(App.Stanje.Detaljno);
                    }
                    else if (radniNalogDetaljno != null)
                    {
                        radniNalogDetaljno.buttonOsveziStavkaArtikal_Click(null, null);
                        radniNalogDetaljno.SelektujStavkaArtikal(Convert.ToInt32(textBoxID.Text));
                        gridStavkaArtikal.DataContext = radniNalogDetaljno.listViewStavkaArtikal.SelectedItem;

                        gridStavkaArtikal.DataContext = null;
                        stanje = App.Stanje.Unos;

                        radniNalogDetaljno.UStanjeStavkaArtikal(App.Stanje.Detaljno);
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {

                    if (ponudaDetaljno != null)
                    {
                        ponudaDetaljno.buttonOsveziStavkaArtikal_Click(null, null);
                        ponudaDetaljno.SelektujStavkaArtikal(Convert.ToInt32(textBoxID.Text));
                        gridStavkaArtikal.DataContext = ponudaDetaljno.listViewStavkaArtikal.SelectedItem;

                        stanje = App.Stanje.Izmena;

                        ponudaDetaljno.UStanjeStavkaArtikal(App.Stanje.Detaljno);
                    }
                    else if (radniNalogDetaljno != null)
                    {
                        radniNalogDetaljno.buttonOsveziStavkaArtikal_Click(null, null);
                        radniNalogDetaljno.SelektujStavkaArtikal(Convert.ToInt32(textBoxID.Text));
                        gridStavkaArtikal.DataContext = radniNalogDetaljno.listViewStavkaArtikal.SelectedItem;

                        stanje = App.Stanje.Izmena;

                        radniNalogDetaljno.UStanjeStavkaArtikal(App.Stanje.Detaljno);
                    }
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
                    if (ponudaDetaljno != null)
                    {
                        ponudaDetaljno.buttonOsveziStavkaArtikal_Click(null, null);
                        ponudaDetaljno.SelektujStavkaArtikal(Convert.ToInt32(textBoxID.Text));
                        gridStavkaArtikal.DataContext = ponudaDetaljno.listViewStavkaArtikal.SelectedItem;

                        stanje = App.Stanje.Izmena;

                        ponudaDetaljno.UStanjeStavkaArtikal(App.Stanje.Detaljno);
                    }
                    else if (radniNalogDetaljno != null)
                    {
                        radniNalogDetaljno.buttonOsveziStavkaArtikal_Click(null, null);
                        radniNalogDetaljno.SelektujStavkaArtikal(Convert.ToInt32(textBoxID.Text));
                        gridStavkaArtikal.DataContext = radniNalogDetaljno.listViewStavkaArtikal.SelectedItem;

                        stanje = App.Stanje.Izmena;

                        radniNalogDetaljno.UStanjeStavkaArtikal(App.Stanje.Detaljno);
                    }

                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public void SacuvajINovi()
        {
            try
            {
                if (Sacuvaj())
                {
                    //comboBoxNosilacGrupe.SelectedItem = null;

                    if (ponudaDetaljno != null)
                    {
                        string _kolicina = textBoxKolicina.Text;

                        ponudaDetaljno.buttonOsveziStavkaArtikal_Click(null, null);
                        ponudaDetaljno.SelektujStavkaArtikal(Convert.ToInt32(textBoxID.Text));
                        gridStavkaArtikal.DataContext = ponudaDetaljno.listViewStavkaArtikal.SelectedItem;

                        gridStavkaArtikal.DataContext = null;
                        stanje = App.Stanje.Unos;

                        ponudaDetaljno.UStanjeStavkaArtikal(App.Stanje.Detaljno);

                        textBoxKolicina.Text = _kolicina;
                    }
                    else if (radniNalogDetaljno != null)
                    {
                        string _kolicina = textBoxKolicina.Text;

                        radniNalogDetaljno.buttonOsveziStavkaArtikal_Click(null, null);
                        radniNalogDetaljno.SelektujStavkaArtikal(Convert.ToInt32(textBoxID.Text));
                        gridStavkaArtikal.DataContext = radniNalogDetaljno.listViewStavkaArtikal.SelectedItem;

                        gridStavkaArtikal.DataContext = null;
                        stanje = App.Stanje.Unos;

                        radniNalogDetaljno.UStanjeStavkaArtikal(App.Stanje.Detaljno);

                        textBoxKolicina.Text = _kolicina;
                    }                    
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
 
    }
}
