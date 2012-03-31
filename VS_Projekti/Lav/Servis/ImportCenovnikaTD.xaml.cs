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
using Microsoft.Win32;
using System.IO;
using System.Globalization;
using System.Windows.Threading;

namespace Servis
{
    /// <summary>
    /// Interaction logic for ImportCenovnika.xaml
    /// </summary>
    public partial class ImportCenovnikaTD : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        NumberFormatInfo decimalFormatProvider;

        DB.DBProksi dBProksi;

        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);


        public ImportCenovnikaTD()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                decimalFormatProvider = new NumberFormatInfo();
                decimalFormatProvider.NumberDecimalSeparator = ",";

                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                prvoOtvaranjeStrane = false;
            }

            Window.GetWindow(this).Title = "Lav - ImportCenovnikaTD";

        }

        private void buttonOdaberiFajl_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog _odaberiFajlOpenFileDialog = new OpenFileDialog();
            _odaberiFajlOpenFileDialog.Filter = "TXT fajl (*.txt)|*.txt";
            _odaberiFajlOpenFileDialog.Title = "Odaberi fajl";
            _odaberiFajlOpenFileDialog.RestoreDirectory = true;

            if (_odaberiFajlOpenFileDialog.ShowDialog() == true)
            {
                textBoxFajl.Text = _odaberiFajlOpenFileDialog.FileName;
            }

        }

        private void buttonImportujFajl_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFajl.Text.Equals("") || !File.Exists(textBoxFajl.Text))
            {
                MessageBox.Show("Odaberi fajl.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            //listBoxRezultat.Items.Clear();

            if (MessageBox.Show(String.Format("Potvrdi import {0} iz fajla {1}", (bool)radioButtonKorisnikPrograma.IsChecked ? "cenovnika Korisnika programa" : "cenovnika Poslovnih partnera", textBoxFajl.Text), "Import", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                int _ukupanBrojRedova = 0;
                int _brojUnetih = 0;
                int _brojGresaka = 0;
                int _brojNeNadjenih = 0;

                char _karakter;

                int _kolona = 1;

                string _poslovniPartner = "";//_kolona = 1
                string _proizvodjac = "";//_kolona = 2
                string _brojProizvodjaca = "";//_kolona = 3
                string _cenaBezPoreza = "";//_kolona = 4
                string _kolicinaNaStanju = "";//_kolona = 5

                this.Cursor = Cursors.Wait;

                DateTime _pocetak = DateTime.Now;
                TimeSpan _vremeTrajanja;

                string _cenovnikFilePath = textBoxFajl.Text;
                string _cenovnikGreskaFilePath = _cenovnikFilePath.Insert(_cenovnikFilePath.Length - 4, "_Greska");
                string _cenovnikNijeNadjenoFilePath = _cenovnikFilePath.Insert(_cenovnikFilePath.Length - 4, "_NijeNadjeno");

                StreamReader _brojacRedovaStreamReader = new StreamReader(_cenovnikFilePath);
                StreamReader _cenovnikStreamReader = new StreamReader(_cenovnikFilePath);
                StreamWriter _greskaStreamWriter = new StreamWriter(_cenovnikGreskaFilePath);
                StreamWriter _nijeNadjenoStreamWriter = new StreamWriter(_cenovnikNijeNadjenoFilePath);

                bool _resetujBrojac = true;

                try
                {

                    int _ubr = 0;
                    while (_brojacRedovaStreamReader.ReadLine() != null)
                    {
                        _ubr++;
                    }
                    _brojacRedovaStreamReader.Close();

                    progressBarStatus.Minimum = 0;
                    progressBarStatus.Maximum = _ubr;
                    progressBarStatus.Value = 0;

                    progressBarStatus.Visibility = System.Windows.Visibility.Visible;
                    textBlockStatus.Visibility = System.Windows.Visibility.Visible;
                    textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Visible;


                    textBlockStatusUkupnoRedova.Text = String.Format("Od ukupno: {0}", _ubr.ToString());

                    //Create a new instance of our ProgressBar Delegate that points to the ProgressBar's SetValue method.
                    UpdateProgressBarDelegate updatePbDelegate =
                        new UpdateProgressBarDelegate(progressBarStatus.SetValue);




                    while (_cenovnikStreamReader.Peek() >= 0)
                    {
                        _karakter = (char)_cenovnikStreamReader.Read();

                        if (_karakter.Equals('\t'))
                        {
                            _kolona++;
                        }
                        else
                        {
                            if (_karakter.Equals('\r') || _karakter.Equals('\n'))
                            {
                                //zato sto posle \r dolazi \n pa vec prazne stringove prazni ponovo 
                                //proverava se kolona preko koje se vrsi update
                                if (_poslovniPartner != "")
                                {
                                    string _c = _poslovniPartner + "\t" + _proizvodjac + "\t" + _brojProizvodjaca + "\t" + _cenaBezPoreza + "\t" + _kolicinaNaStanju;

                                    _ukupanBrojRedova++;

                                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                                    Dispatcher.Invoke(updatePbDelegate,
                                                        System.Windows.Threading.DispatcherPriority.Background,
                                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });
                                    try
                                    {
                                        int _i = (bool)radioButtonPoslovniPartner.IsChecked
                                            ? dBProksi.UnesiCenuDobavljacaTD(_poslovniPartner, _proizvodjac, _brojProizvodjaca, Convert.ToDecimal(_cenaBezPoreza, decimalFormatProvider), Convert.ToDecimal(_kolicinaNaStanju, decimalFormatProvider), _resetujBrojac)
                                            : dBProksi.UnesiCenuKorisnikaProgramaTD(_poslovniPartner, _proizvodjac, _brojProizvodjaca, Convert.ToDecimal(_cenaBezPoreza, decimalFormatProvider), Convert.ToDecimal(_kolicinaNaStanju, decimalFormatProvider), _resetujBrojac);

                                        //da samo jednom resetuje brojac, na pocetku
                                        if (_resetujBrojac)
                                        {
                                            _resetujBrojac = false;
                                        }

                                        if (_i == -1)
                                        {
                                            _nijeNadjenoStreamWriter.WriteLine(_c.ToCharArray());
                                            _brojNeNadjenih++;
                                        }
                                        else
                                        {
                                            _brojUnetih++;
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        //Zato sto ako se uvek desi greska nikad nece podesiti na false gore
                                        _resetujBrojac = false;

                                        _brojGresaka++;

                                        _c = _c + "\t" + ex.Message;

                                        _greskaStreamWriter.WriteLine(_c.ToCharArray());
                                    }
                                }
                                _poslovniPartner = "";//_kolona = 1
                                _proizvodjac = "";//_kolona = 2
                                _brojProizvodjaca = "";//_kolona = 3
                                _cenaBezPoreza = "";//_kolona = 4
                                _kolicinaNaStanju = "";//_kolona = 5

                                _kolona = 1;

                            }
                            else
                            {
                                switch (_kolona)
                                {
                                    case 1:
                                        _poslovniPartner = _poslovniPartner + _karakter.ToString();
                                        break;
                                    case 2:
                                        _proizvodjac = _proizvodjac + _karakter.ToString();
                                        break;
                                    case 3:
                                        _brojProizvodjaca = _brojProizvodjaca + _karakter.ToString();
                                        break;
                                    case 4:
                                        _cenaBezPoreza = _cenaBezPoreza + _karakter.ToString();
                                        break;
                                    case 5:
                                        _kolicinaNaStanju = _kolicinaNaStanju + _karakter.ToString();
                                        break;
                                }
                            }
                        }

                    }
                    _vremeTrajanja = (DateTime.Now - _pocetak);


                    listBoxRezultat.Items.Add("Rezultat za import fajla: " + _cenovnikFilePath);
                    listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                    listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                    listBoxRezultat.Items.Add("Broj ne nađenih = " + _brojNeNadjenih + (_brojNeNadjenih != 0 ? ". Redovi koji sadrže nepoznate artikle nalaze se u fajlu: " + _cenovnikNijeNadjenoFilePath : ""));
                    listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Artikli koji nisu importovani zbog greške nalaze se u fajlu: " + _cenovnikGreskaFilePath : ""));
                    listBoxRezultat.Items.Add("Vreme importa cenovnika = " + _vremeTrajanja);
                    listBoxRezultat.Items.Add("-------------------------------------------------------");

                    _greskaStreamWriter.Close();
                    _nijeNadjenoStreamWriter.Close();

                    if (_brojNeNadjenih.Equals(0))
                    {
                        File.Delete(_cenovnikNijeNadjenoFilePath);
                    }
                    if (_brojGresaka.Equals(0))
                    {
                        File.Delete(_cenovnikGreskaFilePath);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                finally
                {
                    _brojacRedovaStreamReader.Close();
                    _cenovnikStreamReader.Close();
                    _greskaStreamWriter.Close();
                    _nijeNadjenoStreamWriter.Close();

                    textBlockStatus.Text = "";
                    textBlockStatusUkupnoRedova.Text = "";
                    progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                    textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                    textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                    this.Cursor = Cursors.Arrow;
                }
            }
        }
    }
}
