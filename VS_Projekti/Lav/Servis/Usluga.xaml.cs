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
    /// Interaction logic for Usluga.xaml
    /// </summary>
    public partial class Usluga : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        Servis.StavkaUslugaDetaljno stavkaUslugaDetaljno;
        Servis.RadniNalogStavkaUslugaDetaljno radniNalogStavkaUslugaDetaljno;

        public Usluga()
        {
            InitializeComponent();
            textBoxTraziZa.Focus();
        }

        public Usluga(Servis.StavkaUslugaDetaljno stavkaUslugaDetaljno): this()            
        {
            this.stavkaUslugaDetaljno = stavkaUslugaDetaljno;

            if (stavkaUslugaDetaljno.stanje == App.Stanje.Unos)
            {
                contextMenuDodaj.Visibility = System.Windows.Visibility.Visible;
            }
        }
        public Usluga(Servis.RadniNalogStavkaUslugaDetaljno radniNalogStavkaUslugaDetaljno) : this()
        {
            this.radniNalogStavkaUslugaDetaljno = radniNalogStavkaUslugaDetaljno;

            if (radniNalogStavkaUslugaDetaljno.stanje == App.Stanje.Unos)
            {
                contextMenuDodaj.Visibility = System.Windows.Visibility.Visible;
            }
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
                listViewUsluga.ItemsSource = dBProksi.DajSveUsluge();

                if (!listViewUsluga.Items.Count.Equals(0))
                {
                    listViewUsluga.SelectedIndex = 0;
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

        private void SelektujUslugu(int uslugaID)
        {
            ObservableCollection<DB.Usluga> _usluge = (ObservableCollection<DB.Usluga>)listViewUsluga.ItemsSource;
            bool _postoji = false;

            if (!listViewUsluga.Items.Count.Equals(0))
            {
                foreach (DB.Usluga item in _usluge)
                {
                    if (item.UslugaID.Equals(uslugaID))
                    {
                        listViewUsluga.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewUsluga.SelectedIndex = 0;
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

                DajSve();
            }

            //ako sam usao sa StavkaUslugaDetaljno i ako je vec odabrana usluga
            if (stavkaUslugaDetaljno != null && stavkaUslugaDetaljno.textBoxUsluga.Text.Trim() != "")
            {
                try
                {
                    listViewUsluga.ItemsSource = dBProksi.NadjiUsluga((int)stavkaUslugaDetaljno.textBoxUsluga.Tag);


                    if (!listViewUsluga.Items.Count.Equals(0))
                    {
                        listViewUsluga.SelectedIndex = 0;

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
            //ako sam usao sa RadniNalogStavkaUslugaDetaljno i ako je vec odabrana usluga
            if (radniNalogStavkaUslugaDetaljno != null && radniNalogStavkaUslugaDetaljno.textBoxUsluga.Text.Trim() != "")
            {
                try
                {
                    listViewUsluga.ItemsSource = dBProksi.NadjiUsluga((int)radniNalogStavkaUslugaDetaljno.textBoxUsluga.Tag);


                    if (!listViewUsluga.Items.Count.Equals(0))
                    {
                        listViewUsluga.SelectedIndex = 0;

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

            Window.GetWindow(this).Title = "Lav - Usluga";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            UslugaDetaljno _uslugaDetaljno = new UslugaDetaljno(this, false);
            //_uslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _uslugaDetaljno.Owner = Window.GetWindow(this);
            _uslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _uslugaDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.Usluga> _usluge = (ObservableCollection<DB.Usluga>)listViewUsluga.ItemsSource;
            DB.Usluga _usluga = (DB.Usluga)listViewUsluga.SelectedItem;

            if (_usluge != null && _usluga != null)
            {
                UslugaDetaljno _uslugaDetaljno = new UslugaDetaljno(this, true);
                //_uslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _uslugaDetaljno.Owner = Window.GetWindow(this);
                _uslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _uslugaDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiUslugu((DB.Usluga)listViewUsluga.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {
                    DajSve();
                }
                else
                {
                    //ako ima filter ali nije odabrana kolona ne prijavljuj gresku, osvezi podatke
                    if (comboBoxUslugaKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.Usluga> _usluge = (ObservableCollection<DB.Usluga>)listViewUsluga.ItemsSource;

                        listViewUsluga.ItemsSource = dBProksi.OsveziUsluge(_usluge);

                        if (!listViewUsluga.Items.Count.Equals(0))
                        {
                            listViewUsluga.SelectedIndex = 0;
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
                        listViewUsluga.ItemsSource = dBProksi.NadjiUsluge(((ComboBoxItem)comboBoxUslugaKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewUsluga.Items.Count.Equals(0))
                        {
                            listViewUsluga.SelectedIndex = 0;
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
                DB.Usluga _trenutni = (DB.Usluga)listViewUsluga.SelectedItem;

                ObservableCollection<DB.Usluga> _usluge = (ObservableCollection<DB.Usluga>)listViewUsluga.ItemsSource;

                if (!_usluge.Count.Equals(0))
                {
                    listViewUsluga.ItemsSource = dBProksi.OsveziUsluge(_usluge);

                    if (_trenutni != null)
                    {
                        SelektujUslugu(_trenutni.UslugaID);
                    }

                    if (listViewUsluga.Items.Count.Equals(0))
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
                DB.Usluga _trenutni = (DB.Usluga)listViewUsluga.SelectedItem;
                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {
                    DajSve();
                }
                else if (comboBoxUslugaKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewUsluga.ItemsSource = dBProksi.NadjiUsluge(((ComboBoxItem)comboBoxUslugaKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewUsluga.Items.Count.Equals(0))
                    {
                        listViewUsluga.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujUslugu(_trenutni.UslugaID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (stavkaUslugaDetaljno != null)
            {
                DB.Usluga _usluga = (DB.Usluga)listViewUsluga.SelectedItem;

                stavkaUslugaDetaljno.textBoxUsluga.Text = _usluga.VrstaUsluge.Naziv + " " + _usluga.NosilacGrupe.Naziv + " " + _usluga.Nivo.Naziv + " " + _usluga.Pozicija.Naziv;
                stavkaUslugaDetaljno.textBoxUsluga.Tag = _usluga.UslugaID;

                stavkaUslugaDetaljno.textBoxCenaBezPoreza.Text = (_usluga.BrojBodova * _usluga.Bod.Vrednost).ToString("##.00", App.cultureInfo);

                stavkaUslugaDetaljno.textBoxPoreskaStopa.Text = _usluga.PoreskaStopa.VrednostProcenata.ToString("##.00", App.cultureInfo);
                stavkaUslugaDetaljno.textBoxPoreskaStopa.Tag = _usluga.PoreskaStopaID;

                Window.GetWindow(this).Close();
            }
            else if (radniNalogStavkaUslugaDetaljno != null)
            {
                DB.Usluga _usluga = (DB.Usluga)listViewUsluga.SelectedItem;

                radniNalogStavkaUslugaDetaljno.textBoxUsluga.Text = _usluga.VrstaUsluge.Naziv + " " + _usluga.NosilacGrupe.Naziv + " " + _usluga.Nivo.Naziv;
                radniNalogStavkaUslugaDetaljno.textBoxUsluga.Tag = _usluga.UslugaID;

                radniNalogStavkaUslugaDetaljno.textBoxCenaBezPoreza.Text = (_usluga.BrojBodova * _usluga.Bod.Vrednost).ToString("##.00", App.cultureInfo);

                radniNalogStavkaUslugaDetaljno.textBoxPoreskaStopa.Text = _usluga.PoreskaStopa.VrednostProcenata.ToString("##.00", App.cultureInfo);
                radniNalogStavkaUslugaDetaljno.textBoxPoreskaStopa.Tag = _usluga.PoreskaStopaID;
                radniNalogStavkaUslugaDetaljno.textBoxPredvidjenoVremeMinuta.Text = _usluga.NormaMinuta.ToString();

                Window.GetWindow(this).Close();
            }
            else
            {
                //ObservableCollection<DB.Usluga> _usluge = (ObservableCollection<DB.Usluga>)listViewUsluga.ItemsSource;
                //DB.Usluga _usluga = (DB.Usluga)listViewUsluga.SelectedItem;

                //if (_usluge != null && _usluga != null)
                //{
                UslugaDetaljno _uslugaDetaljno = new UslugaDetaljno(this, true);
                //_uslugaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _uslugaDetaljno.Owner = Window.GetWindow(this);
                _uslugaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _uslugaDetaljno.ShowDialog();
                //} 

            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DB.Usluga _usluga = (DB.Usluga)listViewUsluga.SelectedItem;

            if (_usluga == null)
            {
                MessageBox.Show("Odaberi uslugu", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else if (stavkaUslugaDetaljno != null)
            {
                Int32 _kolicina;

                if (stavkaUslugaDetaljno.textBoxUslugaKolicina.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else if (!Int32.TryParse(stavkaUslugaDetaljno.textBoxUslugaKolicina.Text, out _kolicina))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else
                {
                    try
                    {
                        this.Cursor = Cursors.Wait;

                        stavkaUslugaDetaljno.textBoxUsluga.Text = _usluga.VrstaUsluge.Naziv + " " + _usluga.NosilacGrupe.Naziv + " " + _usluga.Nivo.Naziv + " " + _usluga.Pozicija.Naziv;
                        stavkaUslugaDetaljno.textBoxUsluga.Tag = _usluga.UslugaID;

                        stavkaUslugaDetaljno.textBoxCenaBezPoreza.Text = (_usluga.BrojBodova * _usluga.Bod.Vrednost).ToString("##.00");

                        stavkaUslugaDetaljno.textBoxPoreskaStopa.Text = _usluga.PoreskaStopa.VrednostProcenata.ToString();
                        stavkaUslugaDetaljno.textBoxPoreskaStopa.Tag = _usluga.PoreskaStopaID;
                        

                        stavkaUslugaDetaljno.SacuvajINovi();

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
            }
            else if (radniNalogStavkaUslugaDetaljno != null)
            {
                Int32 _kolicina;

                if (radniNalogStavkaUslugaDetaljno.textBoxUslugaKolicina.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else if (!Int32.TryParse(radniNalogStavkaUslugaDetaljno.textBoxUslugaKolicina.Text, out _kolicina))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else
                {
                    try
                    {
                        this.Cursor = Cursors.Wait;

                        radniNalogStavkaUslugaDetaljno.textBoxUsluga.Text = _usluga.VrstaUsluge.Naziv + " " + _usluga.NosilacGrupe.Naziv + " " + _usluga.Nivo.Naziv + " " + _usluga.Pozicija.Naziv;
                        radniNalogStavkaUslugaDetaljno.textBoxUsluga.Tag = _usluga.UslugaID;

                        radniNalogStavkaUslugaDetaljno.textBoxCenaBezPoreza.Text = (_usluga.BrojBodova * _usluga.Bod.Vrednost).ToString("##.00");

                        radniNalogStavkaUslugaDetaljno.textBoxPoreskaStopa.Text = _usluga.PoreskaStopa.VrednostProcenata.ToString();
                        radniNalogStavkaUslugaDetaljno.textBoxPoreskaStopa.Tag = _usluga.PoreskaStopaID;
                        radniNalogStavkaUslugaDetaljno.textBoxPredvidjenoVremeMinuta.Text = _usluga.NormaMinuta.ToString();

                        radniNalogStavkaUslugaDetaljno.SacuvajINovi();

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
            }

        }
    }
}
