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
using System.Globalization;
using System.Threading;

namespace Servis
{
    /// <summary>
    /// Interaction logic for Prijava.xaml
    /// </summary>
    public partial class Prijava : Window
    {
        public Prijava()
        {
            InitializeComponent();
            textBoxKorisnickoIme.Text = Konfiguracija.KorisnickoIme;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            textBoxLozinka.Focus();
            textBoxLozinka.SelectAll();
        }

        private void buttonPrijava_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Cursor = Cursors.Wait;

                try
                {
                    using (SqlConnection _konekcija = new SqlConnection(Konfiguracija.KonekcioniString))
                    {
                        _konekcija.Open();
                    }
                }
                catch (Exception)
                {
                    //ovde pozovi 
                    DBKonekcija _dBKonekcija = new DBKonekcija();
                    //_dBKonekcija.WindowStyle = WindowStyle.ToolWindow;
                    _dBKonekcija.Owner = Window.GetWindow(this);
                    _dBKonekcija.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dBKonekcija.ShowDialog();

                    //MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                DB.DBProksi dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                DB.Radnik _radnik = dBProksi.DajRadnika(textBoxKorisnickoIme.Text, textBoxLozinka.Password);

                if (_radnik == null)
                {
                    throw new Exception("Nepoznat korisnik.");
                }
                else
                {
                    App.Radnik = _radnik;
                    Konfiguracija.KorisnickoIme = textBoxKorisnickoIme.Text;

                    Konfiguracija.Lozinka = textBoxLozinka.Password;

                    Pocetna _pocetna = new Pocetna();

                    _pocetna.Show();
                }

                App.cultureInfo = CultureInfo.CurrentCulture;

                //Thread.CurrentThread.CurrentCulture = App.cultureInfo;
                //Thread.CurrentThread.CurrentUICulture = App.cultureInfo;

                

                this.Close();


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

    }
}
