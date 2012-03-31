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
using System.Globalization;

namespace Servis
{
    /// <summary>
    /// Interaction logic for StavkaUslugaDetaljno.xaml
    /// </summary>
    public partial class StavkaUslugaDetaljno : Window
    {
        public App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.PonudaDetaljno ponudaDetaljno;

        public StavkaUslugaDetaljno(Servis.PonudaDetaljno ponudaDetaljno, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.ponudaDetaljno = ponudaDetaljno;

            if (izmeniTrenutni)
            {
                gridStavkaUsluga.DataContext = (DB.StavkaUsluga)ponudaDetaljno.listViewStavkaUsluga.SelectedItem;
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
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u polje Cena bez poreza.");
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
                else
                {
                    if (stanje == App.Stanje.Unos)
                    {
                        DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                        {
                            PonudaID = ((DB.Ponuda)ponudaDetaljno.gridPonuda.DataContext).PonudaID,
                            UslugaID = Convert.ToInt32(textBoxUsluga.Tag.ToString()),
                            UslugaKolicina = Convert.ToInt32(textBoxUslugaKolicina.Text.Trim()),
                            UslugaCenaBezPoreza = Convert.ToDecimal(textBoxCenaBezPoreza.Text.Trim(), App.cultureInfo),
                            UslugaPoreskaStopa_ID = Convert.ToInt32(textBoxPoreskaStopa.Tag.ToString()),
                            Status = 'I',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };


                        dBProksi.UnesiStavkaUsluga(_stavkaUsluga);

                                                
                        DB.Ponuda _ponuda = (DB.Ponuda)ponudaDetaljno.gridPonuda.DataContext;
                        _ponuda.StavkaUslugas.Add(_stavkaUsluga);
                        ponudaDetaljno.listViewStavkaUsluga.SelectedItem = _stavkaUsluga;

                        textBoxID.Text = _stavkaUsluga.StavkaUslugaID.ToString();

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.StavkaUsluga _stavkaUsluga = new DB.StavkaUsluga
                        {
                            StavkaUslugaID = Convert.ToInt32(textBoxID.Text),
                            PonudaID = ((DB.Ponuda)ponudaDetaljno.gridPonuda.DataContext).PonudaID,
                            UslugaID = Convert.ToInt32(textBoxUsluga.Tag.ToString()),
                            UslugaKolicina = Convert.ToInt32(textBoxUslugaKolicina.Text.Trim()),
                            UslugaCenaBezPoreza = Convert.ToDecimal(textBoxCenaBezPoreza.Text.Trim(), App.cultureInfo),
                            UslugaPoreskaStopa_ID = Convert.ToInt32(textBoxPoreskaStopa.Tag.ToString()),
                            Status = 'U',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };

                        dBProksi.IzmeniStavkaUsluga(_stavkaUsluga, (DB.StavkaUsluga)gridStavkaUsluga.DataContext);
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
                    ponudaDetaljno.buttonOsveziStavkaUsluga_Click(null, null);
                    ponudaDetaljno.SelektujStavkaUsluga(Convert.ToInt32(textBoxID.Text));
                    gridStavkaUsluga.DataContext = ponudaDetaljno.listViewStavkaUsluga.SelectedItem;

                    gridStavkaUsluga.DataContext = null;

                    stanje = App.Stanje.Unos;

                    ponudaDetaljno.UStanjeStavkaUsluga(App.Stanje.Detaljno);
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
                    ponudaDetaljno.buttonOsveziStavkaUsluga_Click(null, null);
                    ponudaDetaljno.SelektujStavkaUsluga(Convert.ToInt32(textBoxID.Text));
                    gridStavkaUsluga.DataContext = ponudaDetaljno.listViewStavkaUsluga.SelectedItem;

                    stanje = App.Stanje.Izmena;

                    ponudaDetaljno.UStanjeStavkaUsluga(App.Stanje.Detaljno);

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
                    ponudaDetaljno.buttonOsveziStavkaUsluga_Click(null, null);
                    ponudaDetaljno.SelektujStavkaUsluga(Convert.ToInt32(textBoxID.Text));
                    gridStavkaUsluga.DataContext = ponudaDetaljno.listViewStavkaUsluga.SelectedItem;

                    stanje = App.Stanje.Izmena;
                    
                    ponudaDetaljno.UStanjeStavkaUsluga(App.Stanje.Detaljno);
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
            _naw.Owner = Window.GetWindow(ponudaDetaljno);
            _naw.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (Window.GetWindow(ponudaDetaljno).WindowState == WindowState.Normal)
            {
                _naw.Width = Window.GetWindow(ponudaDetaljno).ActualWidth;
                _naw.Height = Window.GetWindow(ponudaDetaljno).ActualHeight;
            }
            else
            {
                _naw.WindowState = Window.GetWindow(ponudaDetaljno).WindowState;
            }

            _naw.ShowDialog();
        }

        private void textBoxUsluga_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        public void SacuvajINovi()
        {
            try
            {
                if (Sacuvaj())
                {
                    string _kolicina = textBoxUslugaKolicina.Text;

                    ponudaDetaljno.buttonOsveziStavkaUsluga_Click(null, null);
                    ponudaDetaljno.SelektujStavkaUsluga(Convert.ToInt32(textBoxID.Text));
                    gridStavkaUsluga.DataContext = ponudaDetaljno.listViewStavkaUsluga.SelectedItem;

                    gridStavkaUsluga.DataContext = null;

                    stanje = App.Stanje.Unos;

                    ponudaDetaljno.UStanjeStavkaUsluga(App.Stanje.Detaljno);

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
