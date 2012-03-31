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
    /// Interaction logic for KorisnikPrograma.xaml
    /// </summary>
    public partial class KorisnikPrograma : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public KorisnikPrograma()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {

                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                prvoOtvaranjeStrane = false;
                try
                {
                    ObservableCollection<DB.Mesto> _mesta = new ObservableCollection<DB.Mesto>(dBProksi.DajSvaMesta().ToList());

                    comboBoxMesto.ItemsSource = _mesta;

                    gridKorisnikPrograma.DataContext = dBProksi.DajKorisnikPrograma();

                    if (gridKorisnikPrograma.DataContext != null)
                    {
                        foreach (DB.Mesto item in comboBoxMesto.Items)
                        {
                            if (item.MestoID == ((DB.KorisnikPrograma)gridKorisnikPrograma.DataContext).MestoID)
                            {
                                comboBoxMesto.SelectedItem = item;
                                break;
                            }
                        } 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }

            Window.GetWindow(this).Title = "Lav - KorisnikPrograma";
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
                else if (textBoxNaziv.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Naziv.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxPIB.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje PIB.");
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
                else if (textBoxMaticniBroj.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Matični broj.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxZiroRacun.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Žiro račun.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (comboBoxMesto.SelectedItem == null)
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi Mesto.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxAdresa.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za Adresa.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxTelefon.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Telefon.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxFaks.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Faks.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxEMail.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje E-mail.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else
                {
                    DB.KorisnikPrograma _korisnikPrograma = new DB.KorisnikPrograma
                    {
                        Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                        Naziv = textBoxNaziv.Text.Trim(),
                        PIB = Convert.ToInt32(textBoxPIB.Text.Trim()),
                        MaticniBroj = textBoxMaticniBroj.Text.Trim(),
                        ZiroRacun = textBoxZiroRacun.Text.Trim(),
                        MestoID = ((DB.Mesto)comboBoxMesto.SelectedItem).MestoID,
                        Adresa = textBoxAdresa.Text.Trim(),
                        Telefon = textBoxTelefon.Text.Trim(),
                        Faks = textBoxFaks.Text.Trim(),
                        EMail = textBoxEMail.Text.Trim()
                    };

                    if(textBoxID.Text.Trim().Equals(""))//unos
                    {
                        dBProksi.UnesiKorisnikPrograma(_korisnikPrograma);
                    }
                    else//izmena
                    {
                        _korisnikPrograma.KorisnikProgramaID = Convert.ToInt32(textBoxID.Text.Trim());

                        dBProksi.IzmeniKorisnikPrograma(_korisnikPrograma, (DB.KorisnikPrograma)gridKorisnikPrograma.DataContext);
                    }

                    gridKorisnikPrograma.DataContext = _korisnikPrograma;
                }

                return true;
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
                Sacuvaj();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }
    }
}
