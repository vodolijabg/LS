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
using System.Data.SqlClient;

namespace Servis
{
    /// <summary>
    /// Interaction logic for Podesavanja.xaml
    /// </summary>
    public partial class Podesavanja : Page
    {
        public Podesavanja()
        {
            InitializeComponent();
        }

        private void buttonOdaberiBazu_Click(object sender, RoutedEventArgs e)
        {
            DBKonekcija _dBKonekcija = new DBKonekcija(this);
            //_dBKonekcija.WindowStyle = WindowStyle.ToolWindow;
            _dBKonekcija.Owner = Window.GetWindow(this);
            _dBKonekcija.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _dBKonekcija.ShowDialog();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            textBoxKonekcioniString.Text = Konfiguracija.KonekcioniString;

            textBoxEMail.Text = Konfiguracija.EMailAdresa;

            string _vrstaCeneUCenovniku = Konfiguracija.VrstaCeneUCenovniku;

            if (_vrstaCeneUCenovniku == "SaPDV")
            {
                comboBoxVrstaCeneUCenovniku.SelectedIndex = 0;
            }
            else 
            {
                comboBoxVrstaCeneUCenovniku.SelectedIndex = 1;
            }

            Window.GetWindow(this).Title = "Lav - Podesavanja";
        }

        private void buttonOdaberiKorisnika_Click(object sender, RoutedEventArgs e)
        {
            //KorisnickiNalog _korisnickiNalog = new KorisnickiNalog(this);
            //_korisnickiNalog.WindowStyle = WindowStyle.ToolWindow;
            //_korisnickiNalog.Owner = Window.GetWindow(this);
            //_korisnickiNalog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            //_korisnickiNalog.ShowDialog();

        }

        private void comboBoxVrstaCeneUCenovniku_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxVrstaCeneUCenovniku.SelectedItem != null)
            {
                try
                {
                    if (((ComboBoxItem)comboBoxVrstaCeneUCenovniku.SelectedItem).Content.ToString() == "Sa PDV")
                    {
                        Konfiguracija.VrstaCeneUCenovniku = "SaPDV";
                    }
                    if (((ComboBoxItem)comboBoxVrstaCeneUCenovniku.SelectedItem).Content.ToString() == "Bez PDV")
                    {
                        Konfiguracija.VrstaCeneUCenovniku = "BezPDV";
                    }                  

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void buttonPodesiEMail_Click(object sender, RoutedEventArgs e)
        {
            EMail _email = new EMail(this);
            //_dBKonekcija.WindowStyle = WindowStyle.ToolWindow;
            _email.Owner = Window.GetWindow(this);
            _email.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _email.ShowDialog();
        }
    }
}
