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
using System.Data.SqlClient;

namespace Servis
{
    /// <summary>
    /// Interaction logic for DBKonekcija.xaml
    /// </summary>
    public partial class DBKonekcija : Window
    {
        Podesavanja podesavanja;


        public DBKonekcija()
        {
            InitializeComponent();

        }

        public DBKonekcija(Podesavanja podesavanja) : this()
        {
            this.podesavanja = podesavanja;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            SqlConnectionStringBuilder _konekcija;

            if (podesavanja == null)
            {
                _konekcija = new SqlConnectionStringBuilder(Konfiguracija.KonekcioniString);
            }
            else if (podesavanja.textBoxKonekcioniString.Text != "")
            {
                _konekcija = new SqlConnectionStringBuilder(podesavanja.textBoxKonekcioniString.Text);
            }
            else
            {
                _konekcija = new SqlConnectionStringBuilder(Konfiguracija.KonekcioniString);
            }

            textBoxServer.Text = _konekcija.DataSource;
            textBoxBaza.Text = _konekcija.InitialCatalog;

            if (!_konekcija.IntegratedSecurity)
            {
                comboBoxAutentifikacija.SelectedIndex = 1;
                textBoxKorisnickoIme.Text = _konekcija.UserID;
                textBoxLozinka.Password = _konekcija.Password;
            }
        }

        string DajKonekcioniString()
        {
            SqlConnectionStringBuilder _konekcija = new SqlConnectionStringBuilder();

            _konekcija.DataSource = textBoxServer.Text;
            _konekcija.InitialCatalog = textBoxBaza.Text;

            if (comboBoxAutentifikacija.SelectedIndex == 0)
            {
                _konekcija.IntegratedSecurity = true;
            }
            else
            {
                _konekcija.IntegratedSecurity = false;
                _konekcija.UserID = textBoxKorisnickoIme.Text.Trim();
                _konekcija.Password = textBoxLozinka.Password.Trim();
            }

            return _konekcija.ToString();

        }

        private void buttonSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            Konfiguracija.KonekcioniString = DajKonekcioniString();

            if (podesavanja != null)
            {
                podesavanja.textBoxKonekcioniString.Text = Konfiguracija.KonekcioniString; 
            }

            this.Close();
        }

        private void buttonTestKonekcije_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                using (SqlConnection _konekcija = new SqlConnection(DajKonekcioniString()))
                {
                    this.Cursor = Cursors.Wait;

                    _konekcija.Open();

                    MessageBox.Show("Test konekcije uspešan.", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);

                }
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

        private void comboBoxAutentifikacija_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (((ComboBoxItem)comboBoxAutentifikacija.SelectedItem).Content != null)
            {
                if (comboBoxAutentifikacija.SelectedIndex == 0)
                {
                    textBoxKorisnickoIme.Text = "";
                    textBoxKorisnickoIme.IsEnabled = false;

                    textBoxLozinka.Password = "";
                    textBoxLozinka.IsEnabled = false;
                }
                else
                {
                    textBoxKorisnickoIme.IsEnabled = true;
                    textBoxLozinka.IsEnabled = true;
                }
            }
        }

        
    }
}
