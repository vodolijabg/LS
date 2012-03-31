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
using System.Globalization;

namespace Servis
{
    /// <summary>
    /// Interaction logic for RadniNalogStavkaUslugaDetaljno.xaml
    /// </summary>
    public partial class RadniNalogStavkaUslugaDetaljno : Window
    {
        public App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.RadniNalogDetaljno radniNalogDetaljno;

        public RadniNalogStavkaUslugaDetaljno(Servis.RadniNalogDetaljno radniNalogDetaljno, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.radniNalogDetaljno = radniNalogDetaljno;

            try
            {
                ObservableCollection<DB.RadniNalogStatus> _radniNalogStatus = new ObservableCollection<DB.RadniNalogStatus>(dBProksi.DajSveRadniNalogStatus().ToList());

                comboBoxRadniNalogStatus.ItemsSource = _radniNalogStatus;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (izmeniTrenutni)
            {
                gridRadniNalogStavkaUsluga.DataContext = (DB.StavkaUsluga)radniNalogDetaljno.listViewStavkaUsluga.SelectedItem;

                //stvarno ne znam sto nece da sam selektuje mesto pa moram ovako (vidi binding za SelectetItem)
                foreach (DB.RadniNalogStatus item in comboBoxRadniNalogStatus.Items)
                {
                    if (item.RadniNalogStatusID == ((DB.StavkaUsluga)gridRadniNalogStavkaUsluga.DataContext).RadniNalogStavkaUsluga.RadniNalogStatusID)
                    {
                        comboBoxRadniNalogStatus.SelectedItem = item;
                        break;
                    }
                }

                stanje = App.Stanje.Izmena;
            }
        }

        public bool Sacuvaj()
        {
            try
            {
                //za proveru tipa podataka
                Int32 _kolicina;
                decimal _cenaBezPoreza;
                Int32 _predvidjenoVremeMinuta;
                Int32 _utrosenoVremeMinuta;

                if (textBoxUsluga.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi uslugu.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxUslugaKolicina.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (!Int32.TryParse(textBoxUslugaKolicina.Text, out _kolicina))
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
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u Cena bez poreza.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (!Decimal.TryParse(textBoxCenaBezPoreza.Text, out _cenaBezPoreza))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Cena bez poreza.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxPredvidjenoVremeMinuta.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u polje Predviđeno vreme (Minuta).");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (!Int32.TryParse(textBoxPredvidjenoVremeMinuta.Text, out _predvidjenoVremeMinuta))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Predviđeno vreme (Minuta).");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxRadniNalogStatus.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi Radni nalog status.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxUtrosenoVremeMinuta.Text.Trim() != "" && 
                            !Int32.TryParse(textBoxUtrosenoVremeMinuta.Text, out _utrosenoVremeMinuta))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Utrošeno vreme (Minuta).");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxUtrosenoVremeMinuta.Text.Trim() == "" &&
                    ((DB.RadniNalogStatus)comboBoxRadniNalogStatus.SelectedItem).RadniNalogStatusID.ToString() == Konfiguracija.RadniNalogStatusIDZavrsen)
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Utrošeno vreme (Minuta).");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxUtrosenoVremeMinuta.Text.Trim() != "" &&
                       Int32.TryParse(textBoxUtrosenoVremeMinuta.Text, out _utrosenoVremeMinuta))
                {
                    List<DB.RadniNalogStatus> _radniNalogStatusZavrsenLista = (from u in (ObservableCollection<DB.RadniNalogStatus>)comboBoxRadniNalogStatus.ItemsSource
                                                                               where u.RadniNalogStatusID.ToString() == Konfiguracija.RadniNalogStatusIDZavrsen
                                                                               select u).Take(1).ToList();
                    string _radniNalogStatusZavrsen;
                    if (_radniNalogStatusZavrsenLista.Count() > 0)
                    {
                        _radniNalogStatusZavrsen = _radniNalogStatusZavrsenLista.First().Naziv;
                    }
                    else
                    {
                        _radniNalogStatusZavrsen = Konfiguracija.RadniNalogStatusIDZavrsen;
                    }

                    string _poruka = string.Format("Vrednost u polje Utrošeno vreme (Minuta) može se upisati samo za \nRadni nalog status = {0}", _radniNalogStatusZavrsen);

                    if (((DB.RadniNalogStatus)comboBoxRadniNalogStatus.SelectedItem).RadniNalogStatusID.ToString() != Konfiguracija.RadniNalogStatusIDZavrsen)
                    {
                        Dijalog _dialog = new Dijalog("Greška", _poruka);
                        //_dialog.WindowStyle = WindowStyle.ToolWindow;
                        _dialog.Owner = Window.GetWindow(this);
                        _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        _dialog.ShowDialog();
                        return false;
                    }
                }

                if (stanje == App.Stanje.Unos)
                {
                    DateTime _vremeUnosa = DateTime.Now;

                    DB.RadniNalogStavkaUsluga _radniNalogStavkaUsluga = new DB.RadniNalogStavkaUsluga
                    {
                        PredvidjenoVremeMinuta = Convert.ToInt32(textBoxPredvidjenoVremeMinuta.Text.ToString()),
                        RadniNalogStatusID = ((DB.RadniNalogStatus)comboBoxRadniNalogStatus.SelectedItem).RadniNalogStatusID,
                        Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                        Status = 'I',
                        VremePromene = _vremeUnosa,
                        KorisnickiNalog = App.Radnik.Nadimak
                    };
                    if (textBoxUtrosenoVremeMinuta.Text.Trim() != "")
                    {
                        _radniNalogStavkaUsluga.UtrosenoVremeMinuta = Convert.ToInt32(textBoxUtrosenoVremeMinuta.Text.ToString());
                    }
                    //else
                    //{
                    //    _radniNalogStavkaUsluga.UtrosenoVremeMinuta = null;
                    //}

                    DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                    {
                        RadniNalogID = ((DB.RadniNalog)radniNalogDetaljno.gridRadniNalog.DataContext).RadniNalogID,
                        UslugaID = Convert.ToInt32(textBoxUsluga.Tag.ToString()),
                        UslugaKolicina = Convert.ToInt32(textBoxUslugaKolicina.Text.Trim()),
                        UslugaCenaBezPoreza = Convert.ToDecimal(textBoxCenaBezPoreza.Text.Trim(), App.cultureInfo),
                        UslugaPoreskaStopa_ID = Convert.ToInt32(textBoxPoreskaStopa.Tag.ToString()),
                        Status = 'I',
                        VremePromene = _vremeUnosa,
                        KorisnickiNalog = App.Radnik.Nadimak
                    };

                    _stavkaUsluga.RadniNalogStavkaUsluga = _radniNalogStavkaUsluga;


                    dBProksi.UnesiRadniNalogStavkaUsluga(_stavkaUsluga);


                    DB.RadniNalog _radniNalog = (DB.RadniNalog)radniNalogDetaljno.gridRadniNalog.DataContext;
                    _radniNalog.StavkaUslugas.Add(_stavkaUsluga);
                    radniNalogDetaljno.listViewStavkaUsluga.SelectedItem = _stavkaUsluga;

                    textBoxID.Text = _stavkaUsluga.StavkaUslugaID.ToString();

                    stanje = App.Stanje.Izmena;
                }
                else //if (stanje == App.Stanje.Izmena)
                {
                    DateTime _vremePromene = DateTime.Now;
                    DB.RadniNalogStavkaUsluga _radniNalogStavkaUsluga = new DB.RadniNalogStavkaUsluga
                    {
                        PredvidjenoVremeMinuta = Convert.ToInt32(textBoxPredvidjenoVremeMinuta.Text.ToString()),
                        RadniNalogStatusID = ((DB.RadniNalogStatus)comboBoxRadniNalogStatus.SelectedItem).RadniNalogStatusID,
                        Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                        Status = 'U',
                        VremePromene = _vremePromene,
                        KorisnickiNalog = App.Radnik.Nadimak
                    };
                    if (textBoxUtrosenoVremeMinuta.Text.Trim() != "")
                    {
                        _radniNalogStavkaUsluga.UtrosenoVremeMinuta = Convert.ToInt32(textBoxUtrosenoVremeMinuta.Text.ToString());
                    }

                    DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                    {
                        StavkaUslugaID = Convert.ToInt32(textBoxID.Text),
                        RadniNalogID = ((DB.RadniNalog)radniNalogDetaljno.gridRadniNalog.DataContext).RadniNalogID,
                        UslugaID = Convert.ToInt32(textBoxUsluga.Tag.ToString()),
                        UslugaKolicina = Convert.ToInt32(textBoxUslugaKolicina.Text.Trim()),
                        UslugaCenaBezPoreza = Convert.ToDecimal(textBoxCenaBezPoreza.Text.Trim(), App.cultureInfo),
                        UslugaPoreskaStopa_ID = Convert.ToInt32(textBoxPoreskaStopa.Tag.ToString()),
                        Status = 'U',
                        VremePromene = DateTime.Now,
                        KorisnickiNalog = App.Radnik.Nadimak,
                        RadniNalogStavkaUsluga = _radniNalogStavkaUsluga
                    };

                    dBProksi.IzmeniRadniNalogStavkaUsluga(_stavkaUsluga, (DB.StavkaUsluga)gridRadniNalogStavkaUsluga.DataContext);
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
                    radniNalogDetaljno.buttonOsveziStavkaUsluga_Click(null, null);
                    radniNalogDetaljno.SelektujStavkaUsluga(Convert.ToInt32(textBoxID.Text));
                    radniNalogDetaljno.DataContext = radniNalogDetaljno.listViewStavkaUsluga.SelectedItem;

                    gridRadniNalogStavkaUsluga.DataContext = null;

                    stanje = App.Stanje.Unos;

                    radniNalogDetaljno.UStanjeStavkaUsluga(App.Stanje.Detaljno);
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
                    radniNalogDetaljno.buttonOsveziStavkaUsluga_Click(null, null);
                    radniNalogDetaljno.SelektujStavkaUsluga(Convert.ToInt32(textBoxID.Text));
                    radniNalogDetaljno.DataContext = radniNalogDetaljno.listViewStavkaUsluga.SelectedItem;

                    stanje = App.Stanje.Izmena;

                    radniNalogDetaljno.UStanjeStavkaUsluga(App.Stanje.Detaljno);

                    this.Close();
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
                    radniNalogDetaljno.buttonOsveziStavkaUsluga_Click(null, null);
                    radniNalogDetaljno.SelektujStavkaUsluga(Convert.ToInt32(textBoxID.Text));
                    gridRadniNalogStavkaUsluga.DataContext = radniNalogDetaljno.listViewStavkaUsluga.SelectedItem;

                    stanje = App.Stanje.Izmena;

                    radniNalogDetaljno.UStanjeStavkaUsluga(App.Stanje.Detaljno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void buttonUsluga_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow _naw = new NavigationWindow();

            _naw.Content = new Usluga(this);

            //sakrijem strelice za nazed i napred
            _naw.ShowsNavigationUI = false;
            _naw.Owner = Window.GetWindow(radniNalogDetaljno);
            _naw.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (Window.GetWindow(radniNalogDetaljno).WindowState == WindowState.Normal)
            {
                _naw.Width = Window.GetWindow(radniNalogDetaljno).ActualWidth;
                _naw.Height = Window.GetWindow(radniNalogDetaljno).ActualHeight;
            }
            else
            {
                _naw.WindowState = Window.GetWindow(radniNalogDetaljno).WindowState;
            }

            _naw.ShowDialog();
        }

        private void textBoxUsluga_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
        //    NavigationWindow _naw = new NavigationWindow();

        //    _naw.Content = new Usluga(this);

        //    //sakrijem strelice za nazed i napred
        //    _naw.ShowsNavigationUI = false;
        //    _naw.Owner = Window.GetWindow(radniNalogDetaljno);
        //    _naw.WindowStartupLocation = WindowStartupLocation.CenterOwner;

        //    if (Window.GetWindow(radniNalogDetaljno).WindowState == WindowState.Normal)
        //    {
        //        _naw.Width = Window.GetWindow(radniNalogDetaljno).ActualWidth;
        //        _naw.Height = Window.GetWindow(radniNalogDetaljno).ActualHeight;
        //    }
        //    else
        //    {
        //        _naw.WindowState = Window.GetWindow(radniNalogDetaljno).WindowState;
        //    }

        //    _naw.ShowDialog();
        }

        public void SacuvajINovi()
        {
            try
            {
                if (Sacuvaj())
                {
                    string _kolicina = textBoxUslugaKolicina.Text;

                    radniNalogDetaljno.buttonOsveziStavkaUsluga_Click(null, null);
                    radniNalogDetaljno.SelektujStavkaUsluga(Convert.ToInt32(textBoxID.Text));
                    gridRadniNalogStavkaUsluga.DataContext = radniNalogDetaljno.listViewStavkaUsluga.SelectedItem;

                    gridRadniNalogStavkaUsluga.DataContext = null;

                    stanje = App.Stanje.Unos;

                    radniNalogDetaljno.UStanjeStavkaUsluga(App.Stanje.Detaljno);

                    textBoxUslugaKolicina.Text = _kolicina;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    
    
    }
}
