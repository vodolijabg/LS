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

namespace Servis
{
    /// <summary>
    /// Interaction logic for Pocetna.xaml
    /// </summary>
    public partial class Pocetna : Window
    {
        public Pocetna()
        {
            InitializeComponent();
        }

        private void windowPocetna_Loaded(object sender, RoutedEventArgs e)
        {
            textBlockRadnik.Text = "Prijavljen radnik: " + App.Radnik.Nadimak;

            try
            {
                this.Cursor = Cursors.Wait;

                PrikaziFormu("Artikal.xaml");

            }
            catch (Exception)
            {

            }
            finally
            {
                this.Cursor = Cursors.Arrow;
            }
        }


        public void PrikaziFormu(string imeForme)
        {
            try
            {
                bool _postojiForma = false;

                //ako je forma trenutno prikazana
                if (frameForme.Source != null && frameForme.Source.ToString() == imeForme)
                {
                    return;
                }
                //ako korisnik moze da ide unapred
                if (frameForme.CanGoForward)
                {
                    foreach (JournalEntry entry in frameForme.ForwardStack)
                    {
                        //ako postoji forma 
                        if (entry.Source.ToString() == imeForme)
                        {
                            //pozicioniraj se na formu
                            while (frameForme.Source.ToString() != imeForme)
                            {
                                frameForme.GoForward();
                            }
                            _postojiForma = true;
                            break;
                        }
                    }
                }
                //ako korisnik moze da ide unazad
                if (frameForme.CanGoBack)
                {
                    foreach (JournalEntry entry in frameForme.BackStack)
                    {
                        //ako postoji forma 
                        if (entry.Source.ToString() == imeForme)
                        {
                            //pozicioniraj se na formu
                            while (frameForme.Source.ToString() != imeForme)
                            {
                                frameForme.GoBack();
                            }
                            _postojiForma = true;
                            break;

                        }
                    }
                }

                if (!_postojiForma)
                {
                    NavigationService _navigation = this.frameForme.NavigationService;
                    _navigation.Navigate(new System.Uri(imeForme, UriKind.RelativeOrAbsolute));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listBoxItemMesto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Mesto.xaml");
        }

        private void listBoxItemRadnik_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Radnik.xaml");
        }

        private void listBoxItemRadnoMesto_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("RadnoMesto.xaml");
        }

        private void listBoxItemNacinZahtevaZaPonudu_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("NacinZahtevaZaPonudu.xaml");
        }

        private void listBoxItemRadniNalogStatus_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("RadniNalogStatus.xaml");
        }

        private void listBoxItemNosilacGrupe_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("NosilacGrupe.xaml");
        }

        private void listBoxItemBod_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Bod.xaml");
        }

        private void listBoxItemNivo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Nivo.xaml");
        }

        private void listBoxItemVrstaUsluge_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("VrstaUsluge.xaml");
        }

        private void listBoxItemUsluga_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Usluga.xaml");
        }

        private void listBoxItemNacinOrganizacijeFirme_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("NacinOrganizacijeFirme.xaml");
        }

        private void listBoxItemFizickoLice_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("FizickoLice.xaml");
        }

        private void listBoxItemPoslovniPartner_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("PoslovniPartner.xaml");
        }

        private void listBoxItemVozilo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Vozilo.xaml");
        }

        private void listBoxItemServisnaKnjizica_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("ServisnaKnjizica.xaml");
        }

        private void listBoxItemArtikal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Artikal.xaml");
        }

        private void listBoxItemImportCenovnikaTD_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("ImportCenovnikaTD.xaml");
        }

        private void listBoxItemKorisnikPrograma_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("KorisnikPrograma.xaml");
        }

        private void listBoxItemPodesavanja_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Podesavanja.xaml");
        }


        private void listBoxItemPonuda_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Ponuda.xaml");
        }

        private void listBoxItemImportRoban_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("ImportRoban.xaml");
        }

        private void listBoxItemExportRoban_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("ExportRoban.xaml");
        }

        private void listBoxItemRadniNalog_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("RadniNalog.xaml");
        }

        private void listBoxItemPozicija_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu("Pozicija.xaml");
        }

        private void listBoxItemBrojIzdatihPonuda_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu(@"Izvestaji\BrojIzdatihPonuda.xaml");
        }

        private void listBoxItemPorudzbenica_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            PrikaziFormu(@"Izvestaji\Porudzbenica.xaml");
        }
    }
}
