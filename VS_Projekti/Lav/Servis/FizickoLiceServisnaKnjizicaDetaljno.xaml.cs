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

namespace Servis
{
    /// <summary>
    /// Interaction logic for FizickoLiceServisnaKnjizicaDetaljno.xaml
    /// </summary>
    public partial class FizickoLiceServisnaKnjizicaDetaljno : Window
    {
        private PonudaDetaljno ponudaDetaljno;
        DB.DBProksi dBProksi;

        public FizickoLiceServisnaKnjizicaDetaljno(PonudaDetaljno ponudaDetaljno)
        {
            InitializeComponent();

            this.ponudaDetaljno = ponudaDetaljno;

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

            try
            {
                ObservableCollection<DB.Mesto> _mesta = new ObservableCollection<DB.Mesto>(dBProksi.DajSvaMesta().ToList());

                if (!_mesta.Count.Equals(0))
                {
                    _mesta.Insert(0, new DB.Mesto());
                }

                comboBoxMestoFL.ItemsSource = _mesta.OrderBy(m => m.Naziv);

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        public bool Sacuvaj()
        {
            try
            {
                int _godiste;
                int _kilometraza;

                if ((bool)checkBoxGenerisiSifruFL.IsChecked && textBoxSifraFL.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Fizičko lice - Šifra.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxImeFL.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Fizičko lice - Ime.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (Klase.Telefon.Odmaskiraj(textBoxTelefon1FL.Text.Trim()).Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Fizičko lice - Telefon1.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }

                else if ((bool)checkBoxGenerisiSifruSK.IsChecked && textBoxSifraSK.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Servisna knjižica - Šifra.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxTipSK.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi Servisna knjižica - tip.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxGodisteSK.Text.Trim() != "" && !Int32.TryParse(textBoxGodisteSK.Text, out _godiste))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Servisna knjižica - Godište.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxKilometrazaSK.Text.Trim() != "" && !Int32.TryParse(textBoxKilometrazaSK.Text, out _kilometraza))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Servisna knjižica - Kilometraža.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else
                {
                    DB.FizickoLice _fizickoLice = new DB.FizickoLice
                    {
                        Sifra = textBoxSifraFL.Text.Trim() == "" ? null : textBoxSifraFL.Text.Trim(),
                        Ime = textBoxImeFL.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxImeFL.Text.Trim()),
                        Prezime = textBoxPrezimeFL.Text.Trim() == "" ? null : Helper.DajStringSaVelikimPocetnimSlovom(textBoxPrezimeFL.Text.Trim()),
                        RegistrovanKupac = (bool)checkBoxRegistrovanKupacFL.IsChecked,
                        Adresa = textBoxAdresaFL.Text.Trim() == "" ? null : textBoxAdresaFL.Text.Trim(),
                        Telefon1 = Klase.Telefon.Odmaskiraj(textBoxTelefon1FL.Text.Trim()) == "" ? null : Klase.Telefon.Odmaskiraj(textBoxTelefon1FL.Text.Trim()),
                        Telefon2 = Klase.Telefon.Odmaskiraj(textBoxTelefon2FL.Text.Trim()) == "" ? null : Klase.Telefon.Odmaskiraj(textBoxTelefon2FL.Text.Trim()),
                        EMail = textBoxEMailFL.Text.Trim() == "" ? null : textBoxEMailFL.Text.Trim(),
                    };
                    if (comboBoxMestoFL.SelectedItem != null)
                    {
                        DB.Mesto _mesto = (DB.Mesto)comboBoxMestoFL.SelectedItem;

                        if (_mesto.Naziv == null)
                        {
                            _fizickoLice.Mesto = null;
                        }
                        else
                        {
                            _fizickoLice.MestoID = _mesto.MestoID;
                        }
                    }

                    if (textBoxIDFL.Text.Trim() == "")
                    {
                        dBProksi.UnesiFizickoLice(_fizickoLice);
                    }
                    else
                    {
                        _fizickoLice.FizickoLiceID = Convert.ToInt32(textBoxIDFL.Text);
                        dBProksi.IzmeniFizickoLice(_fizickoLice, (DB.FizickoLice)gridFizickoLice.DataContext);
                    }

                    DB.ServisnaKnjizica _servisnaKnjizica = new DB.ServisnaKnjizica
                    {
                        Sifra = textBoxSifraSK.Text.Trim() == "" ? null : textBoxSifraSK.Text.Trim(),
                        BrojSasije = textBoxBrojSasijeSK.Text.Trim() == "" ? null : textBoxBrojSasijeSK.Text.Trim(),
                        BrojMotora = textBoxBrojMotoraSK.Text.Trim() == "" ? null : textBoxBrojMotoraSK.Text.Trim(),
                        //RegistarskiBroj = textBoxRegistarskiBroj.Text.Trim() == "" ? null : textBoxRegistarskiBroj.Text.Trim(),
                        RegistarskiBroj = Klase.RegistarskiBroj.Odmaskiraj(textBoxRegistarskiBrojSK.Text.Trim()) == "" ? null : Klase.RegistarskiBroj.Odmaskiraj(textBoxRegistarskiBrojSK.Text.Trim()).ToUpper(),
                        DimenzijaGuma = textBoxDimenzijaGumaSK.Text.Trim() == "" ? null : textBoxDimenzijaGumaSK.Text.Trim(),
                        DatumRegistracije = datePickerDatumRegistracijeSK.SelectedDate == null ? null : datePickerDatumRegistracijeSK.SelectedDate,
                        ABS = (bool)checkBoxABSSK.IsChecked,
                        PS = (bool)checkBoxPSSK.IsChecked,
                        AC = (bool)checkBoxACSK.IsChecked,
                        Napomena = textBoxNapomenaSK.Text.Trim() == "" ? null : textBoxNapomenaSK.Text.Trim(),
                    };
                    if (textBoxGodisteSK.Text.Trim() != "")
                    {
                        _servisnaKnjizica.Godiste = Convert.ToInt32(textBoxGodisteSK.Text.Trim());
                    }
                    if (textBoxKilometrazaSK.Text.Trim() != "")
                    {
                        _servisnaKnjizica.Kilometraza = Convert.ToInt32(textBoxKilometrazaSK.Text.Trim());
                    }

                    _servisnaKnjizica.FizickoLiceID = _fizickoLice.FizickoLiceID;

                    _servisnaKnjizica.TipAutomobilaID = Convert.ToInt32(textBoxTipSK.Tag);

                    if (textBoxIDSK.Text.Trim() == "")
                    {
                        dBProksi.UnesiServisnaKnjizica(_servisnaKnjizica);
                    }
                    else
                    {
                        _servisnaKnjizica.ServisnaKnjizicaID = Convert.ToInt32(textBoxIDSK.Text);
                        dBProksi.IzmeniServisnaKnjizica(_servisnaKnjizica, (DB.ServisnaKnjizica)gridServisnaKnjizica.DataContext);
                    }

                    ponudaDetaljno.textBoxServisnaKnjizica.Text = _servisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " + _servisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis + " " + _servisnaKnjizica.TipAutomobila.OpisTabela.Opis;
                    ponudaDetaljno.textBoxServisnaKnjizica.Tag = _servisnaKnjizica.ServisnaKnjizicaID;
                    ponudaDetaljno.textBoxServisnaKnjizicaSifra.Text = _servisnaKnjizica.Sifra;
                    
                }
                
                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

      

        private void textBoxTelefonFL_LostFocus(object sender, RoutedEventArgs e)
        {
            TextBox tb = (TextBox)sender;

            ObservableCollection<DB.FizickoLice> fizickoLiceLista = new ObservableCollection<DB.FizickoLice>();

            if (tb.Name == textBoxTelefon1FL.Name && Klase.Telefon.Odmaskiraj(textBoxTelefon1FL.Text.Trim()) != "")
            {
                fizickoLiceLista = dBProksi.NadjiFizickaLicaZaTelefon(Klase.Telefon.Odmaskiraj(textBoxTelefon1FL.Text.Trim()));
            }
            else if (tb.Name == textBoxTelefon2FL.Name && Klase.Telefon.Odmaskiraj(textBoxTelefon2FL.Text.Trim()) != "")
            {
                fizickoLiceLista = dBProksi.NadjiFizickaLicaZaTelefon(Klase.Telefon.Odmaskiraj(textBoxTelefon2FL.Text.Trim()));
            }

            if (fizickoLiceLista.Count > 0)
            {
                FizickoLiceServisnaKnjizicaDetaljnoDijalog _fizickoLiceServisnaKnjizicaDetaljnoDijalog = new FizickoLiceServisnaKnjizicaDetaljnoDijalog(this, fizickoLiceLista);
                //_fizickoLiceServisnaKnjizicaDetaljnoDijalog.WindowStyle = WindowStyle.ToolWindow;
                //_fizickoLiceServisnaKnjizicaDetaljnoDijalog.ResizeMode = System.Windows.ResizeMode.CanResizeWithGrip;
                _fizickoLiceServisnaKnjizicaDetaljnoDijalog.Owner = this;
                _fizickoLiceServisnaKnjizicaDetaljnoDijalog.Width = this.Width;
                _fizickoLiceServisnaKnjizicaDetaljnoDijalog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _fizickoLiceServisnaKnjizicaDetaljnoDijalog.ShowDialog();
            }
        }

        private void textBoxTipSK_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void buttonTipSK_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow _naw = new NavigationWindow();
            _naw.Content = new Vozilo(this);
            //sakrijem strelice za nazed i napred
            _naw.ShowsNavigationUI = false;
            _naw.Owner = Window.GetWindow(this);
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

        private void buttonSacuvajIOdaberi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {


                    this.Close();
                    //MessageBox.Show("this.Close()");
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
