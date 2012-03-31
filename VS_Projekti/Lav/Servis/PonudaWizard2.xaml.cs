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
    /// Interaction logic for PonudaWizard2.xaml
    /// </summary>
    public partial class PonudaWizard2 : PageFunction<String>
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;
        PonudaWizard ponudaWizard;
        DB.DBProksi dBProksi;

        public PonudaWizard2(PonudaWizard ponudaWizard)
        {
            InitializeComponent();
            this.ponudaWizard = ponudaWizard;
        }

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                prvoOtvaranjeStrane = false;

                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            }
        }

        private void buttonNazad_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(new ReturnEventArgs<String>());
        }

        private void buttonOtkazi_Click(object sender, RoutedEventArgs e)
        {
            Window.GetWindow(this).Close();
        }

        private void buttonTip_Click(object sender, RoutedEventArgs e)
        {
            NavigationWindow _naw = new NavigationWindow();
            _naw.Content = new Vozilo(this);
            //sakrijem strelice za nazed i napred
            _naw.ShowsNavigationUI = false;
            _naw.Owner = Window.GetWindow(this);
            _naw.WindowStartupLocation = WindowStartupLocation.CenterOwner;

            if (Window.GetWindow(ponudaWizard.ponuda).WindowState == WindowState.Normal)
            {
                _naw.Width = Window.GetWindow(ponudaWizard.ponuda).ActualWidth;
                _naw.Height = Window.GetWindow(ponudaWizard.ponuda).ActualHeight;
            }
            else
            {
                _naw.WindowState = Window.GetWindow(ponudaWizard.ponuda).WindowState;
            }

            _naw.ShowDialog();
        }

        private void buttonZavrsi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (textBoxTip.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Odaberi tip automobila.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else
                {
                    ObservableCollection<DB.ServisnaKnjizica> servisnaKnjizica = dBProksi.DajServisnaKnjizica(App.fizickoLicePonudaWizard.FizickoLiceID, Convert.ToInt32(textBoxTip.Tag));

                    if (servisnaKnjizica.Count.Equals(0))
                    {
                        DB.ServisnaKnjizica _servisnaKnjizica = new DB.ServisnaKnjizica
                        {
                            FizickoLiceID = App.fizickoLicePonudaWizard.FizickoLiceID,
                            TipAutomobilaID = Convert.ToInt32(textBoxTip.Tag),
                            ABS = false,
                            PS = false,
                            AC = false
                        };
                        dBProksi.UnesiServisnaKnjizica(_servisnaKnjizica);                        

                        //PonudaDetaljno _ponudaDetaljno = new PonudaDetaljno(ponudaWizard.ponuda, _servisnaKnjizica);
                        ////_ponudaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                        //_ponudaDetaljno.Owner = Window.GetWindow(ponudaWizard.ponuda);
                        //_ponudaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                        //if (Window.GetWindow(ponudaWizard.ponuda).WindowState == WindowState.Normal)
                        //{
                        //    _ponudaDetaljno.Width = Window.GetWindow(ponudaWizard.ponuda).ActualWidth;
                        //    _ponudaDetaljno.Height = Window.GetWindow(ponudaWizard.ponuda).ActualHeight;
                        //}
                        //else
                        //{
                        //    _ponudaDetaljno.WindowState = Window.GetWindow(ponudaWizard.ponuda).WindowState;
                        //}
                        //_ponudaDetaljno.ShowDialog();

                        DB.KorisnikPrograma _korisnikPrograma = dBProksi.DajKorisnikPrograma();

                        DB.Ponuda _ponuda = new DB.Ponuda
                        {
                            KorisnikProgramaID = _korisnikPrograma.KorisnikProgramaID,
                            ServisnaKnjizicaID = Convert.ToInt32(_servisnaKnjizica.ServisnaKnjizicaID),
                            RadnikID = App.Radnik.RadnikID,
                            Vreme = DateTime.Now,
                            NacinZahtevaZaPonuduID = App.nacinZahtevaZaPonuduWizard.NacinZahtevaZaPonuduID,
                            Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                            Status = 'I',
                            VremePromene = DateTime.Now,
                            KorisnickiNalog = App.Radnik.Nadimak
                        };

                        dBProksi.UnesiPonuda(_ponuda);

                        ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)ponudaWizard.ponuda.listViewPonuda.ItemsSource;
                        _ponude.Add(_ponuda);
                        ponudaWizard.ponuda.listViewPonuda.SelectedItem = _ponuda;
                        ponudaWizard.ponuda.UStanje(App.Stanje.Detaljno);

                        Window.GetWindow(this).Close();
                    }
                    else if (servisnaKnjizica.Count.Equals(1))
                    {
                        MessageBoxResult _rezultat = MessageBox.Show("U bazi postoji servisna knjižica za odabrano fizičko lice i tip automobila" +
                        "\nDa koristite postojeću servisnu knjižicu odaberite Yes, da odustanete odaberite No.",
                            "Upozorenje",
                            MessageBoxButton.YesNo, MessageBoxImage.Question);

                        if (_rezultat == MessageBoxResult.Yes)
                        {
                            //PonudaDetaljno _ponudaDetaljno = new PonudaDetaljno(ponudaWizard.ponuda, servisnaKnjizica.First());
                            ////_ponudaDetaljno.WindowStyle = WindowStyle.ToolWindow;
                            //_ponudaDetaljno.Owner = Window.GetWindow(ponudaWizard.ponuda);
                            //_ponudaDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                            //if (Window.GetWindow(ponudaWizard.ponuda).WindowState == WindowState.Normal)
                            //{
                            //    _ponudaDetaljno.Width = Window.GetWindow(ponudaWizard.ponuda).ActualWidth;
                            //    _ponudaDetaljno.Height = Window.GetWindow(ponudaWizard.ponuda).ActualHeight;
                            //}
                            //else
                            //{
                            //    _ponudaDetaljno.WindowState = Window.GetWindow(ponudaWizard.ponuda).WindowState;
                            //}
                            //_ponudaDetaljno.ShowDialog();

                            DB.KorisnikPrograma _korisnikPrograma = dBProksi.DajKorisnikPrograma();

                            DB.Ponuda _ponuda = new DB.Ponuda
                            {
                                KorisnikProgramaID = _korisnikPrograma.KorisnikProgramaID,
                                ServisnaKnjizicaID = Convert.ToInt32(servisnaKnjizica.First().ServisnaKnjizicaID),
                                RadnikID = App.Radnik.RadnikID,
                                Vreme = DateTime.Now,
                                NacinZahtevaZaPonuduID = App.nacinZahtevaZaPonuduWizard.NacinZahtevaZaPonuduID,
                                Napomena = textBoxNapomena.Text.Trim() == "" ? null : textBoxNapomena.Text.Trim(),
                                Status = 'I',
                                VremePromene = DateTime.Now,
                                KorisnickiNalog = App.Radnik.Nadimak
                            };

                            dBProksi.UnesiPonuda(_ponuda);

                            ObservableCollection<DB.Ponuda> _ponude = (ObservableCollection<DB.Ponuda>)ponudaWizard.ponuda.listViewPonuda.ItemsSource;
                            _ponude.Add(_ponuda);
                            ponudaWizard.ponuda.listViewPonuda.SelectedItem = _ponuda;
                            ponudaWizard.ponuda.UStanje(App.Stanje.Detaljno);

                            Window.GetWindow(this).Close();
                        }
                    }
                    else if (servisnaKnjizica.Count > 1)
                    {
                        //nemoguc dogadjaj, kombinacija FizickoLice, TipAutomobila je UC
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }


    }
}
