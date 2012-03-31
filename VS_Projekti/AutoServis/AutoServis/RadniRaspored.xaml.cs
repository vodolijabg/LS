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
using System.ComponentModel;

using System.Threading;
using System.Globalization;

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for RadniRaspored.xaml
    /// </summary>
    public partial class RadniRaspored : PageFunction<String>
    {

        Baza.LavAutoDataContext LavAutoDataContext = null;

        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        //za drag-and-drop rasporeda
        ObservableCollection<Baza.StavkaUslugaRadniRaspored> ListaIzvor = null;

        Baza.Zaglavlje BazaZaglavlje = null;

        public RadniRaspored()
        {
            InitializeComponent();
        }

        public RadniRaspored(Baza.Zaglavlje radniNalog):this()
        {
            BazaZaglavlje = radniNalog;

            borderNajveci.Tag = radniNalog.Zaglavlje_ID;
        }

        private void DajListuRadnik()
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            if (LavAutoDataContext.DatabaseExists())
            {
                IQueryable<Baza.Radnik> _upit = (from p in LavAutoDataContext.Radniks
                                                 select p).OrderBy(w => w.Nadimak);
                try
                {

                    ObservableCollection<Baza.Radnik> _lista = new ObservableCollection<Baza.Radnik>(_upit.ToList());

                    //listBoxRadnikLista.ItemsSource = _lista;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Neuspešna konekcija na bazu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void DajSemuRadnoMesto(DateTime datum)
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            if (LavAutoDataContext.DatabaseExists())
            {
                IQueryable<Baza.RadnoMesto> _upitRadnoMestoSva = (from p in LavAutoDataContext.RadnoMestos
                                                                  select p).OrderBy(w => w.Naziv);

                IQueryable<Baza.RadnoVreme> _upitRadnoVremeSve = (from p in LavAutoDataContext.RadnoVremes
                                                                  select p).OrderBy(w => w.RadnoVreme_ID);

                IQueryable<Baza.StavkaUslugaRadniRaspored> _upitStavkaUslugaRadniRaspored = (from p in LavAutoDataContext.StavkaUslugaRadniRasporeds
                                                                                             where p.Datum == datum
                                                                                             & p.Status != Convert.ToChar("D")
                                                                                             select p).OrderBy(w => w.StavkaUslugaRadniRaspored_ID);
                try
                {
                    ObservableCollection<Baza.RadnoMesto> _listaRadnoMestoSva = new ObservableCollection<Baza.RadnoMesto>(_upitRadnoMestoSva.ToList());

                    ObservableCollection<Baza.RadnoVreme> _listaRadnoVremeSve = new ObservableCollection<Baza.RadnoVreme>(_upitRadnoVremeSve.ToList());

                    ObservableCollection<Baza.StavkaUslugaRadniRaspored> _listaStavkaUslugaRadniRasporedSve = new ObservableCollection<Baza.StavkaUslugaRadniRaspored>(_upitStavkaUslugaRadniRaspored.ToList());

                    listBoxRadnoMestoLista.ItemsSource = null;
                    listBoxRadnoMestoLista.Tag = null;
                    borderSema.Tag = null;

                    listBoxRadnoMestoLista.Items.Refresh();

                    listBoxRadnoMestoLista.Items.Clear();
                    listBoxRadnoMestoLista.ItemsSource = _listaRadnoMestoSva;

                    listBoxRadnoMestoLista.Tag = _listaRadnoVremeSve;
                    borderSema.Tag = _listaStavkaUslugaRadniRasporedSve;

                    textBlockSemaZaDatum.Text = "Šema za " + datum.ToString("d", CultureInfo.CurrentCulture);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            else
            {
                MessageBox.Show("Neuspešna konekcija na bazu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (PrvoOtvaranjeStrane)
            {
                PrvoOtvaranjeStrane = false;
                DajSemuRadnoMesto(DateTime.Now);
                buttonPrikazi.Content = "Prikaži za " + monthCalendarDatum.SelectionStart.ToString("d", CultureInfo.CurrentCulture);

                PrikaziPlaniranoVremeZaRadniNalog();
            }
            //else if ((StanjeTrenutno != App.Stanje.Izmena) && (StanjeTrenutno != App.Stanje.Unos))
            //{
            //    if (!listBoxLista.Items.Count.Equals(0))
            //    {
            //        //da bude prikazan posle osvezavanja podataka
            //        Baza.Radnik _trenutni = (Baza.Radnik)gridDetaljno.DataContext;
            //        SifraTrenutna = _trenutni.Sifra;

            //    }
            //    DajSve();

            //    PrikaziTrenutni();
            //}
        }

        private void monthCalendarDatum_DateSelected(object sender, System.Windows.Forms.DateRangeEventArgs e)
        {
            buttonPrikazi.Content = "Prikaži za " + monthCalendarDatum.SelectionStart.ToString("d", CultureInfo.CurrentCulture);
        }

        private void buttonPrikazi_Click(object sender, RoutedEventArgs e)
        {
            this.Cursor = Cursors.Wait;

            DajSemuRadnoMesto(monthCalendarDatum.SelectionStart);

            this.Cursor = Cursors.Arrow;
        }

        private void listBoxStavkaUslugaRadniRasporedLista_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            try
            {
                ListBox _list = (ListBox)sender;

                Baza.StavkaUslugaRadniRaspored _t = (Baza.StavkaUslugaRadniRaspored)_list.SelectedItem;

                ObservableCollection<Baza.StavkaUslugaRadniRaspored> _l = (ObservableCollection<Baza.StavkaUslugaRadniRaspored>)_list.ItemsSource;

                MessageBox.Show(_t.Radnik.Nadimak);

                //_l.Remove(_t);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            TextBlock _textBlockIzvor = (TextBlock)sender;

            if (_textBlockIzvor.FontWeight != FontWeights.Bold)
            {
                return;
            }

            ListaIzvor = (ObservableCollection<Baza.StavkaUslugaRadniRaspored>)((ListBox)_textBlockIzvor.Tag).ItemsSource;
            DragDrop.DoDragDrop(_textBlockIzvor, _textBlockIzvor.Text, DragDropEffects.Copy);
        }

        int DajStavkaUslugaRadniRaspored_ID(string vrednost)
        {
            char[] _zaObradu = vrednost.Remove(vrednost.Length - 1, 1).ToCharArray();

            StringBuilder _vrati = new StringBuilder();

            for (int i = _zaObradu.Length-1; i >= 0; i--)
            {
                if(!_zaObradu[i].Equals(Convert.ToChar("/")))
                {
                    _vrati.Insert(0, _zaObradu[i].ToString());
                }
                else
                {
                    break;
                }
            }

            if (_vrati.Length == 0)
            {
                return 0;
            }
            else
            {
                return Convert.ToInt32(_vrati.ToString());
            }

        }

        private void listBoxStavkaUslugaRadniRasporedLista_Drop(object sender, DragEventArgs e)
        {
            try
            {
                ListBox _listBoxDestinacija = (ListBox)sender;

                //iz taga ListBox uzimam string koji predstavlja sifru radnog vremena i naziv radnog mesta destinacije razdvojenih karakterom $
                string[] _sifraIRadnoMesto = _listBoxDestinacija.Tag.ToString().Split('$');

                //MessageBox.Show(_sifraIRadnoMesto[0]);
                //MessageBox.Show(_sifraIRadnoMesto[1]);

                ObservableCollection<Baza.StavkaUslugaRadniRaspored> _listaDestinacija = (ObservableCollection<Baza.StavkaUslugaRadniRaspored>)_listBoxDestinacija.ItemsSource;

                int _stavkaUslugaRadniRaspored_ID = DajStavkaUslugaRadniRaspored_ID(e.Data.GetData(DataFormats.Text).ToString());

                int _i = _listaDestinacija.Where(f => f.StavkaUslugaRadniRaspored_ID == _stavkaUslugaRadniRaspored_ID).Count();

                if (_listaDestinacija.Where(f => f.StavkaUslugaRadniRaspored_ID == _stavkaUslugaRadniRaspored_ID).Count() == 1)
                {
                    return;
                }

                int _indexOf = -1;

                try
                {
                    PromeniRasporedUsluzi(_stavkaUslugaRadniRaspored_ID, _sifraIRadnoMesto[1], _sifraIRadnoMesto[0]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri obradi zahteva podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                foreach (Baza.StavkaUslugaRadniRaspored item in ListaIzvor)
                {
                    if (item.StavkaUslugaRadniRaspored_ID == _stavkaUslugaRadniRaspored_ID)
                    {
                        _indexOf = ListaIzvor.IndexOf(item);
                        _listaDestinacija.Add(item);
                        break;
                    }
                }

                if (_indexOf != -1)
                {
                    ListaIzvor.RemoveAt(_indexOf);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        //lansira exceptions
        private void PromeniRasporedUsluzi(int stavkaUslugaRadniRaspored_ID, string noviRadnoMestoNaziv, string noviRadnoVremeSifra)
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            if (LavAutoDataContext.DatabaseExists())
            {
                try
                {
                    IQueryable<Baza.StavkaUslugaRadniRaspored> _upitStavkaUslugaRadniRaspored = (from p in LavAutoDataContext.StavkaUslugaRadniRasporeds
                                                                                                 where p.StavkaUslugaRadniRaspored_ID == stavkaUslugaRadniRaspored_ID
                                                                                                 select p).Take(1);

                    IQueryable<Baza.RadnoMesto> _upitRadnoMesto = (from p in LavAutoDataContext.RadnoMestos
                                                                   where p.Naziv == noviRadnoMestoNaziv
                                                                   select p).Take(1);

                    IQueryable<Baza.RadnoVreme> _upitRadnoVreme = (from p in LavAutoDataContext.RadnoVremes
                                                                   where p.Sifra == noviRadnoVremeSifra
                                                                   select p).Take(1);

                    DateTime _trenutnoVreme = DateTime.Now;
                    string _korisnickiNalog = LavAutoDataContext.DajKorisnickiNalog();
                    //Baza.Radnik _radnik = LavAutoDataContext.DajRadnikaZaKorisnickiNalog();

                    _upitStavkaUslugaRadniRaspored.First().RadnoMesto = _upitRadnoMesto.First();
                    _upitStavkaUslugaRadniRaspored.First().RadnoVreme = _upitRadnoVreme.First();
                    _upitStavkaUslugaRadniRaspored.First().VremePromene = _trenutnoVreme;
                    _upitStavkaUslugaRadniRaspored.First().Status = Convert.ToChar("U");
                    _upitStavkaUslugaRadniRaspored.First().KorisnickiNalog = _korisnickiNalog;

                    LavAutoDataContext.SubmitChanges();

                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
            else
            {
               throw new Exception("Neuspešna konekcija na bazu");
            }
 
        }

        void PrikaziPlaniranoVremeZaRadniNalog()
        {
            StringBuilder _sb = new StringBuilder();

            foreach (Baza.StavkaUsluga item in BazaZaglavlje.RadniNalog.StavkaUslugas)
            {
                StringBuilder _sb1 = new StringBuilder();
                foreach (Baza.StavkaUslugaRadniRaspored item1 in item.StavkaUslugaRadniRasporeds.OrderBy(o => o.Datum))
                {
                    _sb1.Append(item1.Datum.ToString("g", CultureInfo.CurrentCulture) + item1.RadnoMesto.Naziv + item1.RadnoVreme.Sifra);
                }
                _sb.AppendLine(_sb1.ToString());
            }

            textBlockSemaSazeta.Text = _sb.ToString() + "\n" + _sb.ToString();
        }
    }
}
