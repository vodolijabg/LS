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

namespace Servis
{
    /// <summary>
    /// Interaction logic for ImportRoban.xaml
    /// </summary>
    public partial class ImportRoban : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        NumberFormatInfo decimalFormatProvider;

        DB.DBProksi dBProksi;

        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);


        public ImportRoban()
        {
            InitializeComponent();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                decimalFormatProvider = new NumberFormatInfo();
                decimalFormatProvider.NumberDecimalSeparator = ".";

                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
                prvoOtvaranjeStrane = false;
            }
            Window.GetWindow(this).Title = "Lav - ImportRoban";

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

            if (MessageBox.Show(String.Format("Potvrdi import {0} iz fajla {1}", comboBoxZaImport.Text, textBoxFajl.Text), "Import", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    switch (comboBoxZaImport.Text)
                    {
                        case "Komercijalista":
                            ImportKomercijalista();
                            break;
                        case "Uslužni radnik":
                            ImportUsluzniRadnik();
                            break;
                        case "Poslovni partner":
                            ImportPoslovniPartner();
                            break;
                        case "Akumulator":
                            ImportAkumulator();
                            break;
                        case "Ulje":
                            ImportUlje();
                            break;
                        case "Guma":
                            ImportGuma();
                            break;
                        case "Roba":
                            ImportRoba();
                            break;
                        case "Izmeni Broj proizvodjaca i Proizvodjaca preko Sifre (Roban)":
                            UpdateRoba();
                            break;                        
                        case "Zalihe":
                            ImportZalihe();
                            break;
                        case "Obriši artikle preko ArtikalID":
                            ObrisiArtikal();
                            break;
                        default:
                            throw new Exception(String.Format("Nije definisana metoda za import {0}", comboBoxZaImport.Text));
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void UpdateRoba()
        {
            int _ukupanBrojRedova = 0;
            int _brojIzmenjenih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);

            string _red;

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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(3))
                    {
                        try
                        {
                            int _status = dBProksi.IzmeniRobuRoban(_kolone[0].Trim(), _kolone[1].Trim(), _kolone[2].Trim());

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            if (_status == 1)
                            {
                                _brojIzmenjenih++;
                            }
                            
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;

                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 3 kolone.");
                    }
                }
                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj izmenjenih = " + _brojIzmenjenih);
                listBoxRezultat.Items.Add("Broj gresaka = " + _brojGresaka);
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");


                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
                _greskaStreamWriter.Close();

                textBlockStatus.Text = "";
                textBlockStatusUkupnoRedova.Text = "";

                progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                this.Cursor = Cursors.Arrow;
            }
        }


        private void ImportKomercijalista()
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);

            string _red;
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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });


                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(2))
                    {
                        try
                        {
                            //dodaje se k- zato sto se iz dve tabele iz Robana Komercijalisti i UsluzniRadnici slivaju podaci u jednu Radnici.
                            dBProksi.UnesiRadnikaRoban("K-" + _kolone[0].Trim(), _kolone[1].Trim(), _resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            _brojUnetih++;
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;

                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 2 kolone.");
                    }
                }

                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Komercijalisti koji nisu importovani zbog greške nalaze se u fajlu: " + _greskaFilePath : ""));
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");

                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
                _greskaStreamWriter.Close();

                textBlockStatus.Text = "";
                textBlockStatusUkupnoRedova.Text = "";

                progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                this.Cursor = Cursors.Arrow;
            }
        }

        private void ImportUsluzniRadnik()
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);

            string _red;
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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(2))
                    {
                        try
                        {
                            //dodaje se k- zato sto se iz dve tabele iz Robana Komercijalisti i UsluzniRadnici slivaju podaci u jednu Radnici.
                            dBProksi.UnesiRadnikaRoban("R-" + _kolone[0].Trim(), _kolone[1].Trim(), _resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            _brojUnetih++;
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;

                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 2 kolone.");
                    }
                }

                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Usluzni radnici koji nisu importovani zbog greške nalaze se u fajlu: " + _greskaFilePath : ""));
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");

                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
                _greskaStreamWriter.Close();

                textBlockStatus.Text = "";
                textBlockStatusUkupnoRedova.Text = "";

                progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                this.Cursor = Cursors.Arrow;
            }
        }


        private void ImportPoslovniPartner()
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojIzmenjenih = 0;
            int _brojNepoznatih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);

            string _red;
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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(10))
                    {
                        try
                        {
                            int _status = dBProksi.UnesiPoslovniPartnerRoban(_kolone[0].Trim(), _kolone[1].Trim(), _kolone[2].Trim(), _kolone[3].Trim(), _kolone[4].Trim(), _kolone[5].Trim(), _kolone[6].Trim(), _kolone[7].Trim(), _kolone[8].Trim(), _kolone[9].Trim(), _resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            if (_status == 1)
                            {
                                _brojUnetih++;
                            }
                            else if (_status == 2)
                            {
                                _brojIzmenjenih++;
                            }
                            else
                            {
                                _brojNepoznatih++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;

                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 10 kolona.");
                    }
                }

                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                listBoxRezultat.Items.Add("Broj izmenjenih = " + _brojIzmenjenih);
                listBoxRezultat.Items.Add("Broj nepoznatih = " + _brojNepoznatih);
                listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Poslovni partneri koji nisu importovani zbog greške nalaze se u fajlu: " + _greskaFilePath : ""));
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");


                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
                _greskaStreamWriter.Close();

                textBlockStatus.Text = "";
                textBlockStatusUkupnoRedova.Text = "";

                progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                this.Cursor = Cursors.Arrow;
            }
        }                        
        
        private void ImportAkumulator()
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojIzmenjenihKorisnikPrograma = 0;
            int _brojIzmenjenihTD = 0;
            int _brojNepoznatih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);

            string _red;
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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(8))
                    {
                        try
                        {
                            int _status = dBProksi.UnesiAkumulatorRoban(_kolone[0].Trim(), _kolone[1].Trim(), _kolone[2].Trim(), Convert.ToInt32(_kolone[3].Trim()), _kolone[4].Trim(), Convert.ToInt32(_kolone[5].Trim()), Convert.ToDecimal(_kolone[6].Trim(), decimalFormatProvider), Convert.ToDecimal(_kolone[7].Trim(), decimalFormatProvider), _resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            if (_status == 1)
                            {
                                _brojUnetih++;
                            }
                            else if (_status == 2)
                            {
                                _brojIzmenjenihKorisnikPrograma++;
                            }
                            else if (_status == 3)
                            {
                                _brojIzmenjenihTD++;
                            }
                            else
                            {
                                _brojNepoznatih++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;

                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 8 kolona.");
                    }
                }
                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                listBoxRezultat.Items.Add("Broj izmenjenih KP = " + _brojIzmenjenihKorisnikPrograma);
                listBoxRezultat.Items.Add("Broj izmenjenih TD = " + _brojIzmenjenihTD);
                listBoxRezultat.Items.Add("Broj nepoznatih = " + _brojNepoznatih);
                listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Akumulatori koji nisu importovani zbog greške nalaze se u fajlu: " + _greskaFilePath : ""));
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");

                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
                _greskaStreamWriter.Close();

                textBlockStatus.Text = "";
                textBlockStatusUkupnoRedova.Text = "";

                progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                this.Cursor = Cursors.Arrow;
            }
        }

        private void ImportUlje()
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojIzmenjenihKorisnikPrograma = 0;
            int _brojIzmenjenihTD = 0;
            int _brojNepoznatih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);

            string _red;
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


                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(8))
                    {
                        try
                        {
                            int _status = dBProksi.UnesiUljeRoban(_kolone[0].Trim(), _kolone[1].Trim(), _kolone[2].Trim(), Convert.ToInt32(_kolone[3].Trim()), _kolone[4].Trim(), _kolone[5].Trim(), Convert.ToDecimal(_kolone[6].Trim(), decimalFormatProvider), Convert.ToDecimal(_kolone[7].Trim(), decimalFormatProvider), _resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            if (_status == 1)
                            {
                                _brojUnetih++;
                            }
                            else if (_status == 2)
                            {
                                _brojIzmenjenihKorisnikPrograma++;
                            }
                            else if (_status == 3)
                            {
                                _brojIzmenjenihTD++;
                            }
                            else
                            {
                                _brojNepoznatih++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;

                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 8 kolona.");
                    }
                }
                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                listBoxRezultat.Items.Add("Broj izmenjenih KP = " + _brojIzmenjenihKorisnikPrograma);
                listBoxRezultat.Items.Add("Broj izmenjenih TD = " + _brojIzmenjenihTD);
                listBoxRezultat.Items.Add("Broj nepoznatih = " + _brojNepoznatih);
                listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Ulja koja nisu importovana zbog greške nalaze se u fajlu: " + _greskaFilePath : ""));
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");


                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
                _greskaStreamWriter.Close();

                textBlockStatus.Text = "";
                textBlockStatusUkupnoRedova.Text = "";

                progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                this.Cursor = Cursors.Arrow;
            }
        }

        private void ImportGuma()
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojIzmenjenihKorisnikPrograma = 0;
            int _brojIzmenjenihTD = 0;
            int _brojNepoznatih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);

            string _red;
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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(10))
                    {
                        try
                        {
                            int _status = dBProksi.UnesiGumuRoban(_kolone[0].Trim(), _kolone[1].Trim(), _kolone[2].Trim(), Convert.ToInt32(_kolone[3].Trim()), _kolone[4].Trim(), _kolone[5].Trim(), _kolone[6].Trim(), _kolone[7].Trim(), Convert.ToDecimal(_kolone[8].Trim(), decimalFormatProvider), Convert.ToDecimal(_kolone[9].Trim(), decimalFormatProvider), _resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            if (_status == 1)
                            {
                                _brojUnetih++;
                            }
                            else if (_status == 2)
                            {
                                _brojIzmenjenihKorisnikPrograma++;
                            }
                            else if (_status == 3)
                            {
                                _brojIzmenjenihTD++;
                            }
                            else
                            {
                                _brojNepoznatih++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;

                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 10 kolona.");
                    }
                }
                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                listBoxRezultat.Items.Add("Broj izmenjenih KP = " + _brojIzmenjenihKorisnikPrograma);
                listBoxRezultat.Items.Add("Broj izmenjenih TD = " + _brojIzmenjenihTD);
                listBoxRezultat.Items.Add("Broj nepoznatih = " + _brojNepoznatih);
                listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Gume koja nisu importovana zbog greške nalaze se u fajlu: " + _greskaFilePath : ""));
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");

                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
                _greskaStreamWriter.Close();

                textBlockStatus.Text = "";
                textBlockStatusUkupnoRedova.Text = "";

                progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                this.Cursor = Cursors.Arrow;
            }
        }

        private void ImportRoba()
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojIzmenjenihKorisnikPrograma = 0;
            int _brojIzmenjenihTD = 0;
            int _brojNepoznatih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);

            string _red;

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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(8))
                    {
                        try
                        {
                            int _status = dBProksi.UnesiRobuRoban(_kolone[0].Trim(), _kolone[1].Trim(), _kolone[2].Trim(), Convert.ToInt32(_kolone[3].Trim()), _kolone[4].Trim(), _kolone[5].Trim(), Convert.ToDecimal(_kolone[6].Trim(), decimalFormatProvider), Convert.ToDecimal(_kolone[7].Trim(), decimalFormatProvider), _resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            if (_status == 1)
                            {
                                _brojUnetih++;
                            }
                            else if (_status == 2)
                            {
                                _brojIzmenjenihKorisnikPrograma++;
                            }
                            else if (_status == 3)
                            {
                                _brojIzmenjenihTD++;
                            }
                            else
                            {
                                _brojNepoznatih++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;


                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 8 kolona.");
                    }
                }
                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                listBoxRezultat.Items.Add("Broj izmenjenih KP = " + _brojIzmenjenihKorisnikPrograma);
                listBoxRezultat.Items.Add("Broj izmenjenih TD = " + _brojIzmenjenihTD);
                listBoxRezultat.Items.Add("Broj nepoznatih = " + _brojNepoznatih);
                listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Roba koja nije importovana zbog greške nalaze se u fajlu: " + _greskaFilePath : ""));
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");


                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
                _greskaStreamWriter.Close();

                textBlockStatus.Text = "";
                textBlockStatusUkupnoRedova.Text = "";

                progressBarStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatus.Visibility = System.Windows.Visibility.Collapsed;
                textBlockStatusUkupnoRedova.Visibility = System.Windows.Visibility.Collapsed;


                this.Cursor = Cursors.Arrow;
            }
        }

        private void ImportZalihe()
        {
            int _ukupanBrojRedova = 0;
            int _brojUnetih = 0;
            int _brojGresaka = 0;
            int _brojNeNadjenih = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");
            string _nijeNadjenoFilePath = _filePath.Insert(_filePath.Length - 4, "_NijeNadjeno");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);
            StreamWriter _nijeNadjenoStreamWriter = new StreamWriter(_nijeNadjenoFilePath);

            string _red;

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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(4))
                    {
                        try
                        {

                            int _i = dBProksi.UnesiZalihe(_kolone[1].Trim(), Convert.ToDecimal(_kolone[2].Trim(), decimalFormatProvider), Convert.ToDecimal(_kolone[3].Trim(), decimalFormatProvider), _resetujBrojac);

                            //da samo jednom resetuje brojac, na pocetku
                            if (_resetujBrojac)
                            {
                                _resetujBrojac = false;
                            }

                            if (_i == -1)
                            {
                                _nijeNadjenoStreamWriter.WriteLine(_red);
                                _brojNeNadjenih++;
                            }
                            else
                            {
                                _brojUnetih++;
                            }
                        }
                        catch (Exception ex)
                        {
                            _resetujBrojac = true;

                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 4 kolone.");
                    }
                }

                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za import fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj unetih = " + _brojUnetih);
                listBoxRezultat.Items.Add("Broj ne nađenih = " + _brojNeNadjenih + (_brojNeNadjenih != 0 ? ". Redovi koji sadrže nepoznate artikle nalaze se u fajlu: " + _nijeNadjenoFilePath : ""));
                listBoxRezultat.Items.Add("Broj grešaka = " + _brojGresaka + (_brojGresaka != 0 ? ". Zalihe koje nisu importovani zbog greške nalaze se u fajlu: " + _greskaFilePath : ""));
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");


                _greskaStreamWriter.Close();
                _nijeNadjenoStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
                if (_brojNeNadjenih.Equals(0))
                {
                    File.Delete(_nijeNadjenoFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
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


        private void ObrisiArtikal()
        {
            int _ukupanBrojRedova = 0;
            int _brojNeNadjenih = 0;
            int _brojGresaka = 0;

            this.Cursor = Cursors.Wait;

            DateTime _pocetak = DateTime.Now;
            TimeSpan _vremeTrajanja;

            string _filePath = textBoxFajl.Text;
            string _greskaFilePath = _filePath.Insert(_filePath.Length - 4, "_Greska");
            string _nijeNadjenoFilePath = _filePath.Insert(_filePath.Length - 4, "_NijeNadjeno");

            StreamReader _brojacRedovaStreamReader = new StreamReader(_filePath);
            StreamReader _streamReader = new StreamReader(_filePath);
            StreamWriter _greskaStreamWriter = new StreamWriter(_greskaFilePath);
            StreamWriter _nijeNadjenoStreamWriter = new StreamWriter(_nijeNadjenoFilePath);

            string _red;

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

                while ((_red = _streamReader.ReadLine()) != null)
                {
                    _ukupanBrojRedova++;

                    textBlockStatus.Text = _ukupanBrojRedova.ToString();

                    Dispatcher.Invoke(updatePbDelegate,
                                        System.Windows.Threading.DispatcherPriority.Background,
                                            new object[] { ProgressBar.ValueProperty, Convert.ToDouble(_ukupanBrojRedova) });

                    string[] _kolone = _red.Split(new char[] { '\t' });

                    if (_kolone.Count().Equals(1))
                    {
                        try
                        {
                            int _status = dBProksi.ObrisiArtikal(Convert.ToInt32(_kolone[0].Trim()));

                            if (_status == 0)
                            {
                                _brojNeNadjenih++;
                                _nijeNadjenoStreamWriter.WriteLine(_red);
                            }
                        }
                        catch (Exception ex)
                        {
                            _brojGresaka++;
                            _greskaStreamWriter.WriteLine(_red + "\t" + ex.Message);
                        }
                    }
                    else
                    {
                        string _redGreska = "";
                        int _brojKolonaGreska = 0;

                        foreach (string s in _kolone)
                        {
                            if (_brojKolonaGreska == 0)
                            {
                                _redGreska += s;
                            }
                            else
                            {
                                _redGreska += "\t" + s;
                            }
                            _brojKolonaGreska++;
                        }

                        _brojGresaka++;
                        _greskaStreamWriter.WriteLine(_redGreska + "\t" + "Red nema tacno 1 kolonu.");
                    }
                }
                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za obradu fajla: " + _filePath);
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Broj ne nadjenih = " + _brojNeNadjenih);
                listBoxRezultat.Items.Add("Broj gresaka = " + _brojGresaka);
                listBoxRezultat.Items.Add("Vreme importa = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");


                _greskaStreamWriter.Close();

                if (_brojGresaka.Equals(0))
                {
                    File.Delete(_greskaFilePath);
                }
                if (_brojNeNadjenih.Equals(0))
                {
                    File.Delete(_nijeNadjenoFilePath);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _brojacRedovaStreamReader.Close();
                _streamReader.Close();
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
