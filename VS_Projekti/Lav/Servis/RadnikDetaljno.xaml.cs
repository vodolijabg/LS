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

namespace Servis
{
    /// <summary>
    /// Interaction logic for RadnikDetaljno.xaml
    /// </summary>
    public partial class RadnikDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.Radnik radnik;

        public RadnikDetaljno(Servis.Radnik radnik, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.radnik = radnik;


            try
            {
                ObservableCollection<DB.Mesto> _mesta = new ObservableCollection<DB.Mesto>(dBProksi.DajSvaMesta().ToList());

                if (!_mesta.Count.Equals(0))
                {
                    _mesta.Insert(0, new DB.Mesto());
                }

                comboBoxMesto.ItemsSource = _mesta;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (izmeniTrenutni)
            {
                gridRadnik.DataContext = (DB.Radnik)radnik.listViewRadnik.SelectedItem;
                
                //stvarno ne znam sto nece da sam selektuje mesto pa moram ovako (vidi binding za SelectetItem)
                foreach (DB.Mesto item in comboBoxMesto.Items)
                {
                    if (item.MestoID == ((DB.Radnik)gridRadnik.DataContext).MestoID)
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
        }

        public bool Sacuvaj()
        {
            try
            {
                //za proveru tipa podataka
                Int64 _JMBG;

                if ((bool)checkBoxGenerisiSifru.IsChecked && textBoxSifra.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Šifra.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxNadimak.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Nadimak.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if(textBoxJMBG.Text.Trim() != "" && !Int64.TryParse(textBoxJMBG.Text, out _JMBG))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje JMBG.");
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
                        DB.Radnik _radnik = new DB.Radnik
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Ime = textBoxIme.Text.Trim() == "" ? null : textBoxIme.Text.Trim(),
                            Prezime = textBoxPrezime.Text.Trim() == "" ? null : textBoxPrezime.Text.Trim(),
                            ImeOca = textBoxImeOca.Text.Trim() == "" ? null : textBoxImeOca.Text.Trim(),
                            Nadimak = textBoxNadimak.Text.Trim() == "" ? null : textBoxNadimak.Text.Trim(),
                            DatumRodjenja = datePickerDatumRodjenja.SelectedDate == null ? null : datePickerDatumRodjenja.SelectedDate,
                            Adresa = textBoxAdresa.Text.Trim() == "" ? null : textBoxAdresa.Text.Trim(),
                            Telefon = textBoxTelefon.Text.Trim() == "" ? null : textBoxTelefon.Text.Trim(),
                            ZaposlenOd = datePickerZaposlenOd.SelectedDate == null ? null : datePickerZaposlenOd.SelectedDate,
                            Raspored = textBoxRaspored.Text.Trim() == "" ? null : textBoxRaspored.Text.Trim(),
                        };
                        if (textBoxJMBG.Text.Trim() != "")
	                    {
		                     _radnik.JMBG = Convert.ToInt64(textBoxJMBG.Text.Trim());
	                    }
                        if (comboBoxMesto.SelectedItem != null)
	                    {
                            DB.Mesto _mesto = (DB.Mesto)comboBoxMesto.SelectedItem;

                            if(_mesto.Naziv == null)
                            {
                                _radnik.Mesto = null;
                            }
                            else
                            {
                                _radnik.MestoID = _mesto.MestoID;
                            }
	                    }


                        dBProksi.UnesiRadnika(_radnik);

                        ObservableCollection<DB.Radnik> _radnici = (ObservableCollection<DB.Radnik>)radnik.listViewRadnik.ItemsSource;
                        _radnici.Add(_radnik);
                        radnik.listViewRadnik.SelectedItem = _radnik;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.Radnik _radnik = new DB.Radnik
                        {
                            RadnikID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Ime = textBoxIme.Text.Trim() == "" ? null : textBoxIme.Text.Trim(),
                            Prezime = textBoxPrezime.Text.Trim() == "" ? null : textBoxPrezime.Text.Trim(),
                            ImeOca = textBoxImeOca.Text.Trim() == "" ? null : textBoxImeOca.Text.Trim(),
                            Nadimak = textBoxNadimak.Text.Trim() == "" ? null : textBoxNadimak.Text.Trim(),
                            DatumRodjenja = datePickerDatumRodjenja.SelectedDate == null ? null : datePickerDatumRodjenja.SelectedDate,
                            Adresa = textBoxAdresa.Text.Trim() == "" ? null : textBoxAdresa.Text.Trim(),
                            Telefon = textBoxTelefon.Text.Trim() == "" ? null : textBoxTelefon.Text.Trim(),
                            ZaposlenOd = datePickerZaposlenOd.SelectedDate == null ? null : datePickerZaposlenOd.SelectedDate,
                            Raspored = textBoxRaspored.Text.Trim() == "" ? null : textBoxRaspored.Text.Trim(),
                        };
                        if (textBoxJMBG.Text.Trim() != "")
                        {
                            _radnik.JMBG = Convert.ToInt64(textBoxJMBG.Text.Trim());
                        }
                        if (comboBoxMesto.SelectedItem != null)
                        {
                            DB.Mesto _mesto = (DB.Mesto)comboBoxMesto.SelectedItem;

                            if (_mesto.Naziv == null)
                            {
                                _radnik.Mesto = null;
                            }
                            else
                            {
                                _radnik.MestoID = _mesto.MestoID;
                            }
                        }

                        dBProksi.IzmeniRadnika(_radnik, (DB.Radnik)gridRadnik.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujRadnika(int radnikID)
        {
            ObservableCollection<DB.Radnik> _radnici = (ObservableCollection<DB.Radnik>)radnik.listViewRadnik.ItemsSource;
            bool _postoji = false;

            if (!radnik.listViewRadnik.Items.Count.Equals(0))
            {
                foreach (DB.Radnik item in _radnici)
                {
                    if (item.RadnikID.Equals(radnikID))
                    {
                        radnik.listViewRadnik.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    radnik.listViewRadnik.SelectedIndex = 0;
                }
            }

        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.Radnik _trenutni = (DB.Radnik)radnik.listViewRadnik.SelectedItem;

                ObservableCollection<DB.Radnik> _radnici = (ObservableCollection<DB.Radnik>)radnik.listViewRadnik.ItemsSource;

                if (!_radnici.Count.Equals(0))
                {
                    radnik.listViewRadnik.ItemsSource = dBProksi.OsveziRadnike(_radnici);

                    if (_trenutni != null)
                    {
                        SelektujRadnika(_trenutni.RadnikID);
                    }
                }
                gridRadnik.DataContext = (DB.Radnik)radnik.listViewRadnik.SelectedItem;
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
                    radnik.UStanje(App.Stanje.Detaljno);
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

                    gridRadnik.DataContext = null;
                    stanje = App.Stanje.Unos;
                    radnik.UStanje(App.Stanje.Detaljno);
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

                    radnik.UStanje(App.Stanje.Detaljno);
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
