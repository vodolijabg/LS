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

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for Konfiguracija.xaml
    /// </summary>
    public partial class Konfiguracija : Page
    {
        public Konfiguracija()
        {
            InitializeComponent();
        }
        private void UStanje(App.Stanje stanje)
        {
            textBoxKonekcioniString.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruMesto.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruRadnik.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruFizickoLice.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruNacinZahtevaZaPonudu.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruNosilacGrupe.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruRadnoMesto.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruRadniNalogStatus.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruBod.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruPoslovniPartner.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruNivo.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruVrstaUsluge.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruUsluga.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            checkBoxAutomatskiDodeliSifruServisnaKnjizica.IsEnabled = stanje.Equals(App.Stanje.Izmena);

            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdi.IsEnabled = stanje.Equals(App.Stanje.Izmena);
            buttonOdustani.IsEnabled = stanje.Equals(App.Stanje.Izmena);

        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            int _renderingTier = (RenderCapability.Tier >> 16);

            textBlockRenderingTier.Text = _renderingTier.ToString();

            if (_renderingTier == 0)
            { 
                //Rendering Tier 0. The video card will not provide any hardware acceleration. This corresponds to a DirectX version level of less than 7.0.
            }
            else if (_renderingTier == 1)
            {  
                //Rendering Tier 1. The video card can provide partial hardware acceleration. This corresponds to a DirectX version level greater than 7.0 but less than 9.0.
            }
            else if (_renderingTier == 2)
            { 
                //Rendering Tier 2. All features that can be hardware accelerated will be. This corresponds to a DirectX version level greater than or equal to 9.0.
            }

            PrikaziKonfiguraciju();
            UStanje(App.Stanje.Detaljno);
        }

        void PrikaziKonfiguraciju()
        {
            textBoxKonekcioniString.Text = AutoServis.Properties.Settings.Default.KonekcioniString;
            checkBoxAutomatskiDodeliSifruMesto.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruMesto;
            checkBoxAutomatskiDodeliSifruRadnik.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnik;
            checkBoxAutomatskiDodeliSifruFizickoLice.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruFizickoLice;
            checkBoxAutomatskiDodeliSifruNacinZahtevaZaPonudu.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNacinZahtevaZaPonudu;
            checkBoxAutomatskiDodeliSifruNosilacGrupe.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNosilacGrupe;
            checkBoxAutomatskiDodeliSifruRadnoMesto.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnoMesto;
            checkBoxAutomatskiDodeliSifruRadniNalogStatus.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadniNalogStatus;
            checkBoxAutomatskiDodeliSifruBod.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruBod;
            checkBoxAutomatskiDodeliSifruPoslovniPartner.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruPoslovniPartner;
            
            checkBoxAutomatskiDodeliSifruNivo.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNivo;
            checkBoxAutomatskiDodeliSifruVrstaUsluge.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruVrstaUsluge;
            checkBoxAutomatskiDodeliSifruUsluga.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruUsluga;
            checkBoxAutomatskiDodeliSifruServisnaKnjizica.IsChecked = AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruServisnaKnjizica;
        }

        void SacuvajKonfiguraciju()
        {
            AutoServis.Properties.Settings.Default.KonekcioniString = textBoxKonekcioniString.Text;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruMesto = (bool)checkBoxAutomatskiDodeliSifruMesto.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnik = (bool)checkBoxAutomatskiDodeliSifruRadnik.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruFizickoLice = (bool)checkBoxAutomatskiDodeliSifruFizickoLice.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNacinZahtevaZaPonudu = (bool)checkBoxAutomatskiDodeliSifruNacinZahtevaZaPonudu.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNosilacGrupe = (bool)checkBoxAutomatskiDodeliSifruNosilacGrupe.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnoMesto = (bool)checkBoxAutomatskiDodeliSifruRadnoMesto.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadniNalogStatus = (bool)checkBoxAutomatskiDodeliSifruRadniNalogStatus.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruBod = (bool)checkBoxAutomatskiDodeliSifruBod.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruPoslovniPartner = (bool)checkBoxAutomatskiDodeliSifruPoslovniPartner.IsChecked;
            
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNivo = (bool)checkBoxAutomatskiDodeliSifruNivo.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruVrstaUsluge = (bool)checkBoxAutomatskiDodeliSifruVrstaUsluge.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruUsluga = (bool)checkBoxAutomatskiDodeliSifruUsluga.IsChecked;
            AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruServisnaKnjizica = (bool)checkBoxAutomatskiDodeliSifruServisnaKnjizica.IsChecked;

            AutoServis.Properties.Settings.Default.Save();
        }


        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            UStanje(App.Stanje.Izmena);
        }

        private void buttonPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            SacuvajKonfiguraciju();
            UStanje(App.Stanje.Detaljno);
        }

        private void buttonOdustani_Click(object sender, RoutedEventArgs e)
        {
            UStanje(App.Stanje.Detaljno);
        }
    }
}
