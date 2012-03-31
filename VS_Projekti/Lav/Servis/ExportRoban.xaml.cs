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
using System.Collections.ObjectModel;

namespace Servis
{
    /// <summary>
    /// Interaction logic for ExportRoban.xaml
    /// </summary>
    public partial class ExportRoban : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public ExportRoban()
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
            }

            Window.GetWindow(this).Title = "Lav - ExportRoban";
        }

        private void buttonOdaberiFajl_Click(object sender, RoutedEventArgs e)
        {
            SaveFileDialog _sacuvajFajlSaveFileDialog = new SaveFileDialog();
            _sacuvajFajlSaveFileDialog.Filter = "TXT fajl (*.txt)|*.txt";
            _sacuvajFajlSaveFileDialog.Title = "Odaberi fajl";
            _sacuvajFajlSaveFileDialog.RestoreDirectory = true;
            _sacuvajFajlSaveFileDialog.FileName = "US20001";

            if (_sacuvajFajlSaveFileDialog.ShowDialog() == true)
            {
                textBoxFajl.Text = _sacuvajFajlSaveFileDialog.FileName;
            }
        }        
        
        private void buttonExportujFajl_Click(object sender, RoutedEventArgs e)
        {
            listBoxRezultat.Items.Clear();

            if (textBoxFajl.Text.Trim().Equals(""))
            {
                MessageBox.Show("Odaberi fajl.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                return;
            }

            ObservableCollection<DB.Usluga> _uslugeZaExport = dBProksi.DajUslugeZaExport((bool)checkBoxSamoMarkirane.IsChecked);

            StreamWriter _streamWriter = new StreamWriter(textBoxFajl.Text.Trim());

            try
            {
                int _ukupanBrojRedova = 0;
                this.Cursor = Cursors.Wait;

                DateTime _pocetak = DateTime.Now;
                TimeSpan _vremeTrajanja;

                if ((bool)checkBoxExportujZaglavlje.IsChecked)
                {
                    _streamWriter.WriteLine("Sifra" + "\t" + "Naziv" + "\t" + "CenaBezPDV" + "\t" + "PoreskaStopa" + "\t" + "NormaSati");
                 }

                foreach (DB.Usluga item in _uslugeZaExport)
                {
                    _ukupanBrojRedova++;

                    string _sifra = item.Sifra;
                    string _naziv = item.VrstaUsluge.Naziv + " " + item.NosilacGrupe.Naziv + " " + item.Nivo.Naziv + " " + item.Pozicija.Naziv;
                    if (_naziv.Length > 44)
                    {
                        _naziv = _naziv.Substring(0, 44);
                    }

                    _naziv = Helper.ZameniSrpskeEngleskimKarakterima(_naziv);

                    string _cenaBezPDV = (item.Bod.Vrednost * item.BrojBodova).ToString("0.00", new System.Globalization.CultureInfo("en-US"));
                    string _poreskaStopa = item.PoreskaStopaID.ToString();
                    string _normaSati = (item.NormaMinuta / 60).ToString() + "." + (item.NormaMinuta % 60).ToString("00");

                    if (item.ZaExport)
                    {
                        dBProksi.MarkirajUsluguExportovanom(item.UslugaID); 
                    }

                    _streamWriter.WriteLine(_sifra.PadRight(6, ' ') + "\t" + _naziv.PadRight(44, ' ') + "\t" + _cenaBezPDV.PadRight(14, ' ') + "\t" + _poreskaStopa + "\t" + _normaSati.PadRight(5, ' '));
                }

                _vremeTrajanja = (DateTime.Now - _pocetak);


                listBoxRezultat.Items.Add("Rezultat za export fajla: " + textBoxFajl.Text.Trim());
                listBoxRezultat.Items.Add("Ukupan broj redova = " + _ukupanBrojRedova);
                listBoxRezultat.Items.Add("Vreme exporta = " + _vremeTrajanja);
                listBoxRezultat.Items.Add("-------------------------------------------------------");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                _streamWriter.Close();
                this.Cursor = Cursors.Arrow;
            }
        }
    }
}
