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
using System.Globalization;

namespace Servis
{
    /// <summary>
    /// Interaction logic for UslugaDetaljno.xaml
    /// </summary>
    public partial class UslugaDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.Usluga usluga;

        public UslugaDetaljno(Servis.Usluga usluga, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.usluga = usluga;


            try
            {
                ObservableCollection<DB.VrstaUsluge> _vrsteUsluge = new ObservableCollection<DB.VrstaUsluge>(dBProksi.DajSveVrstaUsluge().ToList());
                comboBoxVrstaUsluge.ItemsSource = _vrsteUsluge;

                ObservableCollection<DB.NosilacGrupe> _nosiociGrupe = new ObservableCollection<DB.NosilacGrupe>(dBProksi.DajSveNosilacGrupe().ToList());
                comboBoxNosilacGrupe.ItemsSource = _nosiociGrupe;

                ObservableCollection<DB.Nivo> _nivoi = new ObservableCollection<DB.Nivo>(dBProksi.DajSveNivo().ToList());
                comboBoxNivo.ItemsSource = _nivoi;

                ObservableCollection<DB.Bod> _bodovi = new ObservableCollection<DB.Bod>(dBProksi.DajSveBod().ToList());
                comboBoxBod.ItemsSource = _bodovi;

                ObservableCollection<DB.PoreskaStopa> _poreskeStope = new ObservableCollection<DB.PoreskaStopa>(dBProksi.DajSvePoreskeStope().ToList());
                comboBoxPoreskaStopa.ItemsSource = _poreskeStope;

                ObservableCollection<DB.Pozicija> _pozicija = new ObservableCollection<DB.Pozicija>(dBProksi.DajSvePozicija().ToList());
                comboBoxPozicija.ItemsSource = _pozicija;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (izmeniTrenutni)
            {
                gridUsluga.DataContext = (DB.Usluga)usluga.listViewUsluga.SelectedItem;

                //stvarno ne znam sto nece da sam selektuje pa moram ovako (vidi binding za SelectetItem)
                foreach (DB.VrstaUsluge item in comboBoxVrstaUsluge.Items)
                {
                    if (item.VrstaUslugeID == ((DB.Usluga)gridUsluga.DataContext).VrstaUslugeID)
                    {
                        comboBoxVrstaUsluge.SelectedItem = item;
                        break;
                    }
                }
                foreach (DB.NosilacGrupe item in comboBoxNosilacGrupe.Items)
                {
                    if (item.NosilacGrupeID == ((DB.Usluga)gridUsluga.DataContext).NosilacGrupeID)
                    {
                        comboBoxNosilacGrupe.SelectedItem = item;
                        break;
                    }
                }
                foreach (DB.Nivo item in comboBoxNivo.Items)
                {
                    if (item.NivoID == ((DB.Usluga)gridUsluga.DataContext).NivoID)
                    {
                        comboBoxNivo.SelectedItem = item;
                        break;
                    }
                }
                foreach (DB.Pozicija item in comboBoxPozicija.Items)
                {
                    if (item.PozicijaID == ((DB.Usluga)gridUsluga.DataContext).PozicijaID)
                    {
                        comboBoxPozicija.SelectedItem = item;
                        break;
                    }
                }
                foreach (DB.Bod item in comboBoxBod.Items)
                {
                    if (item.BodID == ((DB.Usluga)gridUsluga.DataContext).BodID)
                    {
                        comboBoxBod.SelectedItem = item;
                        break;
                    }
                }
                foreach (DB.PoreskaStopa item in comboBoxPoreskaStopa.Items)
                {
                    if (item.PoreskaStopaID == ((DB.Usluga)gridUsluga.DataContext).PoreskaStopaID)
                    {
                        comboBoxPoreskaStopa.SelectedItem = item;
                        break;
                    }
                }

                //textBoxVrednost.Text = (((DB.Usluga)gridUsluga.DataContext).BrojBodova * ((DB.Usluga)gridUsluga.DataContext).Bod.Vrednost).ToString();

                stanje = App.Stanje.Izmena;
            }
            else
            {
                stanje = App.Stanje.Unos;
            }
        }

        public bool Sacuvaj()
        {
            try
            {
                //za proveru tipa podataka
                Int32 _normaMinuta;
                decimal _brojBodova;

                if ((bool)checkBoxGenerisiSifru.IsChecked && textBoxSifra.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Šifra.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxVrstaUsluge.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi vrstu usluge.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxNosilacGrupe.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi nosioca grupe.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxNivo.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi nivo.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxPozicija.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi poziciju.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxNormaMinuta.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Norma (Minuta).");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (!Int32.TryParse(textBoxNormaMinuta.Text, out _normaMinuta))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Norma (Minuta).");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }

                else if (textBoxBrojBodova.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Broj (Količina).");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (!decimal.TryParse(textBoxBrojBodova.Text, out _brojBodova))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Broj (Količina).");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxBod.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi bod.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxPoreskaStopa.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi poresku stopu.");
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
                        DB.Usluga _usluga = new DB.Usluga
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            VrstaUslugeID = ((DB.VrstaUsluge)comboBoxVrstaUsluge.SelectedItem).VrstaUslugeID,
                            NosilacGrupeID = ((DB.NosilacGrupe)comboBoxNosilacGrupe.SelectedItem).NosilacGrupeID,
                            NivoID = ((DB.Nivo)comboBoxNivo.SelectedItem).NivoID,
                            NormaMinuta = Convert.ToInt32(textBoxNormaMinuta.Text.Trim()),
                            BrojBodova = Convert.ToDecimal(textBoxBrojBodova.Text.Trim(), App.cultureInfo),
                            BodID = ((DB.Bod)comboBoxBod.SelectedItem).BodID,
                            PoreskaStopaID = ((DB.PoreskaStopa)comboBoxPoreskaStopa.SelectedItem).PoreskaStopaID,
                            PozicijaID = ((DB.Pozicija)comboBoxPozicija.SelectedItem).PozicijaID,
                            ZaExport = true
                        };

                        dBProksi.UnesiUslugu(_usluga);

                        ObservableCollection<DB.Usluga> _usluge = (ObservableCollection<DB.Usluga>)usluga.listViewUsluga.ItemsSource;
                        _usluge.Add(_usluga);
                        usluga.listViewUsluga.SelectedItem = _usluga;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.Usluga _uslugaOrginal = (DB.Usluga)gridUsluga.DataContext;
                            
                        DB.Usluga _usluga = new DB.Usluga
                        {
                            UslugaID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            //VrstaUsluge = (DB.VrstaUsluge)comboBoxVrstaUsluge.SelectedItem,
                            //NosilacGrupe = (DB.NosilacGrupe)comboBoxNosilacGrupe.SelectedItem,
                            //Nivo = (DB.Nivo)comboBoxNivo.SelectedItem,
                            NormaMinuta = Convert.ToInt32(textBoxNormaMinuta.Text.Trim()),
                            BrojBodova = Convert.ToDecimal(textBoxBrojBodova.Text.Trim(), App.cultureInfo),
                            //Bod = (DB.Bod)comboBoxBod.SelectedItem,
                            //PoreskaStopa = (DB.PoreskaStopa)comboBoxPoreskaStopa.SelectedItem
                            //Pozicija = (DB.Pozicija)comboBoxPozicija.SelectedItem,
                            ZaExport = _uslugaOrginal.ZaExport
                        };

                        if (comboBoxVrstaUsluge.SelectedItem != null)
                        {
                            DB.VrstaUsluge _vrstaUsluge = (DB.VrstaUsluge)comboBoxVrstaUsluge.SelectedItem;

                            if (_vrstaUsluge.Naziv == null)
                            {
                                _usluga.VrstaUsluge = null;
                            }
                            else
                            {
                                _usluga.VrstaUslugeID = _vrstaUsluge.VrstaUslugeID;

                                if (_uslugaOrginal.VrstaUsluge.Naziv != _vrstaUsluge.Naziv)
                                {
                                    _usluga.ZaExport = true;
                                }
                            }
                        }
                        if (comboBoxNosilacGrupe.SelectedItem != null)
                        {
                            DB.NosilacGrupe _nosilacGrupe = (DB.NosilacGrupe)comboBoxNosilacGrupe.SelectedItem;

                            if (_nosilacGrupe.Naziv == null)
                            {
                                _usluga.NosilacGrupe = null;
                            }
                            else
                            {
                                _usluga.NosilacGrupeID = _nosilacGrupe.NosilacGrupeID;

                                if (_uslugaOrginal.NosilacGrupe.Naziv != _nosilacGrupe.Naziv)
                                {
                                    _usluga.ZaExport = true;
                                }
                            }
                        }
                        if (comboBoxNivo.SelectedItem != null)
                        {
                            DB.Nivo _nivo = (DB.Nivo)comboBoxNivo.SelectedItem;

                            if (_nivo.Naziv == null)
                            {
                                _usluga.Nivo = null;
                            }
                            else
                            {
                                _usluga.NivoID = _nivo.NivoID;

                                if (_uslugaOrginal.Nivo.Naziv != _nivo.Naziv)
                                {
                                    _usluga.ZaExport = true;
                                }
                            }
                        }

                        if (comboBoxPozicija.SelectedItem != null)
                        {
                            DB.Pozicija _pozicija = (DB.Pozicija)comboBoxPozicija.SelectedItem;

                            if (_pozicija.Naziv == null)
                            {
                                _usluga.Nivo = null;
                            }
                            else
                            {
                                _usluga.PozicijaID = _pozicija.PozicijaID;

                                if (_uslugaOrginal.Nivo.Naziv != _pozicija.Naziv)
                                {
                                    _usluga.ZaExport = true;
                                }
                            }
                        }

                        if (comboBoxBod.SelectedItem != null)
                        {
                            DB.Bod _bod = (DB.Bod)comboBoxBod.SelectedItem;

                            if (_bod.Naziv == null)
                            {
                                _usluga.Bod = null;
                            }
                            else
                            {
                                _usluga.BodID = _bod.BodID;

                                if (_uslugaOrginal.Bod.Vrednost != _bod.Vrednost)
                                {
                                    _usluga.ZaExport = true;
                                }
                            }
                        }
                        if (comboBoxPoreskaStopa.SelectedItem != null)
                        {
                            DB.PoreskaStopa _poreskaStopa = (DB.PoreskaStopa)comboBoxPoreskaStopa.SelectedItem;

                            if (_poreskaStopa.Sifra == null)
                            {
                                _usluga.PoreskaStopa = null;
                            }
                            else
                            {
                                _usluga.PoreskaStopaID = _poreskaStopa.PoreskaStopaID;

                                if (_uslugaOrginal.PoreskaStopa.VrednostProcenata != _poreskaStopa.VrednostProcenata)
                                {
                                    _usluga.ZaExport = true;
                                }
                            }
                        }


                        if (
                            _usluga.Sifra != _uslugaOrginal.Sifra ||
                            //_usluga.VrstaUsluge.Naziv != _uslugaOrginal.VrstaUsluge.Naziv ||
                            //_usluga.NosilacGrupe.Naziv != _uslugaOrginal.NosilacGrupe.Naziv ||
                            //_usluga.Nivo.Naziv != _uslugaOrginal.Nivo.Naziv ||
                            //_usluga.Bod.Vrednost != _uslugaOrginal.Bod.Vrednost ||
                            _usluga.BrojBodova != _uslugaOrginal.BrojBodova ||
                            _usluga.PoreskaStopaID != _uslugaOrginal.PoreskaStopaID ||
                            _usluga.NormaMinuta != _uslugaOrginal.NormaMinuta
                            )
                        {
                            _usluga.ZaExport = true;
                        }

                        dBProksi.IzmeniUslugu(_usluga, _uslugaOrginal);

                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujUslugu(int uslugaID)
        {
            ObservableCollection<DB.Usluga> _usluge = (ObservableCollection<DB.Usluga>)usluga.listViewUsluga.ItemsSource;
            bool _postoji = false;

            if (!usluga.listViewUsluga.Items.Count.Equals(0))
            {
                foreach (DB.Usluga item in _usluge)
                {
                    if (item.UslugaID.Equals(uslugaID))
                    {
                        usluga.listViewUsluga.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    usluga.listViewUsluga.SelectedIndex = 0;
                }
            }

        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.Usluga _trenutni = (DB.Usluga)usluga.listViewUsluga.SelectedItem;

                ObservableCollection<DB.Usluga> _radnici = (ObservableCollection<DB.Usluga>)usluga.listViewUsluga.ItemsSource;

                if (!_radnici.Count.Equals(0))
                {
                    usluga.listViewUsluga.ItemsSource = dBProksi.OsveziUsluge(_radnici);

                    if (_trenutni != null)
                    {
                        SelektujUslugu(_trenutni.UslugaID);
                    }
                }
                gridUsluga.DataContext = (DB.Usluga)usluga.listViewUsluga.SelectedItem;
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
                    usluga.UStanje(App.Stanje.Detaljno);
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

                    gridUsluga.DataContext = null;
                    stanje = App.Stanje.Unos;
                    usluga.UStanje(App.Stanje.Detaljno);
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

                    usluga.UStanje(App.Stanje.Detaljno);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

    }
}
