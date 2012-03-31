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

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for Pocetna.xaml
    /// </summary>
    public partial class Pocetna : Page
    {
        public enum Stanje { Sistem, SistemskiKatalog, KorisnickiKatalog, Servis, Neutralno}
        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        Baza.LavAutoDataContext LavAutoDataContext = null;


        public Pocetna()
        {
            InitializeComponent();
        }

        private void UStanje(Stanje stanje)
        {
            //sve sakri
            buttonKonfiguracija.Visibility = Visibility.Collapsed;
            buttonMesto.Visibility = Visibility.Collapsed;
            buttonRadnik.Visibility = Visibility.Collapsed;
            buttonRadnoMesto.Visibility = Visibility.Collapsed;
            buttonNacinZahtevaZaPonudu.Visibility = Visibility.Collapsed;
            buttonNosilacGrupe.Visibility = Visibility.Collapsed;
            buttonRadniNalogStatus.Visibility = Visibility.Collapsed;
            buttonBod.Visibility = Visibility.Collapsed;
            buttonNivo.Visibility = Visibility.Collapsed;
            buttonVrstaUsluge.Visibility = Visibility.Collapsed;
            buttonUsluga.Visibility = Visibility.Collapsed;
            buttonFizickoLice.Visibility = Visibility.Collapsed;
            buttonPoslovniPartner.Visibility = Visibility.Collapsed;
            buttonTipAutomobila.Visibility = Visibility.Collapsed;
            buttonServisnaKnjizica.Visibility = Visibility.Collapsed;
            buttonArtikal.Visibility = Visibility.Collapsed;
            buttonPonuda.Visibility = Visibility.Collapsed;
            buttonRadniNalog.Visibility = Visibility.Collapsed;
            //buttonRadniRaspored.Visibility = Visibility.Collapsed;

            //a onda u zavisnosti od stanja prikazi samo koje treba
            if (stanje == Stanje.Sistem)
            {
                buttonKonfiguracija.Visibility = Visibility.Visible;

            }
            else if (stanje == Stanje.SistemskiKatalog)
            {
                buttonMesto.Visibility = Visibility.Visible;
                buttonRadnik.Visibility = Visibility.Visible;
                buttonRadnoMesto.Visibility = Visibility.Visible;
                buttonNacinZahtevaZaPonudu.Visibility = Visibility.Visible;
                buttonNosilacGrupe.Visibility = Visibility.Visible;
                buttonRadniNalogStatus.Visibility = Visibility.Visible;
                buttonBod.Visibility = Visibility.Visible;
                buttonNivo.Visibility = Visibility.Visible;
                buttonVrstaUsluge.Visibility = Visibility.Visible;
                buttonUsluga.Visibility = Visibility.Visible;

            }
            else if ((stanje == Stanje.KorisnickiKatalog))
            {
                buttonFizickoLice.Visibility = Visibility.Visible;
                buttonPoslovniPartner.Visibility = Visibility.Visible;
            }
            else if ((stanje == Stanje.Servis))
            {
                buttonTipAutomobila.Visibility = Visibility.Visible;
                buttonServisnaKnjizica.Visibility = Visibility.Visible;
                buttonArtikal.Visibility = Visibility.Visible;
                buttonPonuda.Visibility = Visibility.Visible;
                buttonRadniNalog.Visibility = Visibility.Visible;
                //buttonRadniRaspored.Visibility = Visibility.Visible;

            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (PrvoOtvaranjeStrane)
            {
                NavigationWindow _win = (NavigationWindow)Window.GetWindow(this);
                _win.WindowState = WindowState.Maximized;

                LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

                PrvoOtvaranjeStrane = false;
                UStanje(Stanje.Neutralno);
            }
        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            hyperlinkSistem.FontWeight = FontWeights.Normal;
            hyperlinkSistemskiKatalog.FontWeight = FontWeights.Normal;
            hyperlinkKorisnickiKatalog.FontWeight = FontWeights.Normal;
            hyperlinkServis.FontWeight = FontWeights.Normal;

            Hyperlink _trenutni = (Hyperlink)sender;
            _trenutni.FontWeight = FontWeights.SemiBold;

            if (_trenutni.Name.Equals("hyperlinkSistem"))
            {
                UStanje(Stanje.Sistem);
            }
            else if (_trenutni.Name.Equals("hyperlinkSistemskiKatalog"))
            {
                UStanje(Stanje.SistemskiKatalog);
            }
            else if (_trenutni.Name.Equals("hyperlinkKorisnickiKatalog"))
            {
                UStanje(Stanje.KorisnickiKatalog);
            }
            else if (_trenutni.Name.Equals("hyperlinkServis"))
            {
                UStanje(Stanje.Servis);
            }
        }

        private void OnMouseEnter(object sender, MouseEventArgs e)
        {
            Hyperlink _trenutni = (Hyperlink)sender;
            _trenutni.TextDecorations = TextDecorations.Underline;
        }

        private void OnMouseLeave(object sender, MouseEventArgs e)
        {
            Hyperlink _trenutni = (Hyperlink)sender;

            _trenutni.TextDecorations = null;
        }

        private void buttonMesto_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Mesto());
        }

        private void buttonRadnik_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Radnik());
            
        }

        private void buttonRadnoMesto_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new RadnoMesto());

        }

        private void buttonNacinZahtevaZaPonudu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new NacinZahtevaZaPonudu());

        }

        private void buttonRadniNalogStatus_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new RadniNalogStatus());

        }

        private void buttonNosilacGrupe_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new NosilacGrupe());

        }

        private void buttonBod_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Bod());

        }

        private void buttonNivo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Nivo());
        }

        private void buttonVrstaUsluge_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new VrstaUsluge());
        }


        private void buttonUsluga_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Usluga());

        }

        private void buttonFizickoLice_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new FizickoLice());
        }

        private void buttonPoslovniPartner_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new PoslovniPartner());

        }

        private void buttonTipAutomobila_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new TipAutomobila());

        }

        private void buttonServisnaKnjizica_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new ServisnaKnjizica());
        }

        private void buttonArtikal_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Artikal());

        }

        private void buttonKonfiguracija_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Konfiguracija());

        }

        private void buttonPonuda_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Ponuda());

        }

        private void buttonRadniNalog_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new RadniNalog());

        }

        private void buttonRadniRaspored_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new RadniRaspored());

        }


        
        
    }
}
