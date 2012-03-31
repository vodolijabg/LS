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

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for Artikal.xaml
    /// </summary>
    public partial class Artikal : PageFunction<object>
    {
        Baza.LavAutoDataContext LavAutoDataContext = null;
        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        //da znam da li je ulaz na formu sa neke druge da bi se odabrao artikal
        bool OdaberiArtikal = false;
        Baza.Artikal PrikaziArtikal = null;

        public Artikal()
        {
            InitializeComponent();
        }

        public Artikal (ObservableCollection<Baza.Artikal> lista): this()
        {

            listBoxListaProizvoda.ItemsSource = lista;


            ICollectionView view = CollectionViewSource.GetDefaultView(listBoxListaProizvoda.ItemsSource);
            view.GroupDescriptions.Add(new PropertyGroupDescription("Proizvodjac.Naziv"));

            view.SortDescriptions.Add(
            new SortDescription("OpisTabela.Opis", ListSortDirection.Ascending));

        }

        public Artikal(bool odaberiArtikal):this()
        {
            this.OdaberiArtikal = odaberiArtikal;
        }

        public Artikal(bool odaberiArtikal, Baza.Artikal artikal) : this()
        {
            //TODO prikazi artikal

            this.OdaberiArtikal = odaberiArtikal;
            PrikaziArtikal = artikal;
        }

        private void DajListuVrstaBrojaZaPretragu()
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            try
            {
                IQueryable<Baza.VrstaBrojaZaPretragu> _upit = (from p in LavAutoDataContext.VrstaBrojaZaPretragus
                                                               select p).OrderBy(w => w.Naziv);

                ObservableCollection<Baza.VrstaBrojaZaPretragu> _lista = new ObservableCollection<Baza.VrstaBrojaZaPretragu>(_upit.ToList());

                //listBoxVrstaBrojaZaPretraguLista.ItemsSource = _lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju mesta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {            
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (PrvoOtvaranjeStrane)
            {
                PrvoOtvaranjeStrane = false;
                int _i = 0;
                foreach (string var in AutoServis.Properties.Settings.Default.DesetPoslednjihUspesnihPretraga)
                {
                    comboBoxBrojZaPretragu.Items.Insert(_i, var);
                    _i++;
                }


                if (PrikaziArtikal != null)
                {
                    LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

                    IQueryable<Baza.Artikal> _upit = (from p in LavAutoDataContext.Artikals
                                                      where p.Artikal_ID == PrikaziArtikal.Artikal_ID
                                                      select p).Take(1);

                    try
                    {
                        ObservableCollection<Baza.Artikal> _lista = new ObservableCollection<Baza.Artikal>(_upit.ToList());
                        listBoxListaProizvoda.ItemsSource = _lista;

                        listBoxListaProizvoda.SelectedItem = _upit.First();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška pri čitanju artikla", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    
                }
            }

        }

        private string DajOciscenString(string stringZaCiscenje)
        {
            string[] _karakteriZaIzbacivanje = { ".", ",", "-", " ", "/" };

            StringBuilder _sb = new StringBuilder(stringZaCiscenje.Trim());

            foreach (string _s in _karakteriZaIzbacivanje)
            {
                _sb.Replace(_s, "");
            }

            return _sb.ToString();
        }

        private void checkBoxVrstaBroja_Click(object sender, RoutedEventArgs e)
        {
            CheckBox _trenutni = (CheckBox)sender;

            if (_trenutni.Name == "checkBoxBiloKojiBroj")
            {
                if ((bool)_trenutni.IsChecked)
                {
                    checkBoxBrojProizvodjaca.IsChecked = false;
                    checkBoxOEBroj.IsChecked = false;
                    checkBoxKoriscenBroj.IsChecked = false;
                    checkBoxUporedniBroj.IsChecked = false;
                    checkBoxEanBroj.IsChecked = false;

                    checkBoxBrojProizvodjaca.IsEnabled = false;
                    checkBoxOEBroj.IsEnabled = false;
                    checkBoxKoriscenBroj.IsEnabled = false;
                    checkBoxUporedniBroj.IsEnabled = false;
                    checkBoxEanBroj.IsEnabled = false;
                }
                else
                {
                    checkBoxBrojProizvodjaca.IsChecked = true;

                    checkBoxBrojProizvodjaca.IsEnabled = true;
                    checkBoxOEBroj.IsEnabled = true;
                    checkBoxKoriscenBroj.IsEnabled = true;
                    checkBoxUporedniBroj.IsEnabled = true;
                    checkBoxEanBroj.IsEnabled = true;

                }
            }
            else
            {
                if (
                    !(bool)checkBoxBrojProizvodjaca.IsChecked
                    && !(bool)checkBoxOEBroj.IsChecked
                    && !(bool)checkBoxKoriscenBroj.IsChecked
                    && !(bool)checkBoxUporedniBroj.IsChecked
                    && !(bool)checkBoxEanBroj.IsChecked
                    )
                {
                    checkBoxBiloKojiBroj.IsChecked = true;

                    checkBoxBrojProizvodjaca.IsChecked = false;
                    checkBoxOEBroj.IsChecked = false;
                    checkBoxKoriscenBroj.IsChecked = false;
                    checkBoxUporedniBroj.IsChecked = false;
                    checkBoxEanBroj.IsChecked = false;

                    checkBoxBrojProizvodjaca.IsEnabled = false;
                    checkBoxOEBroj.IsEnabled = false;
                    checkBoxKoriscenBroj.IsEnabled = false;
                    checkBoxUporedniBroj.IsEnabled = false;
                    checkBoxEanBroj.IsEnabled = false;

                }
            }
        }

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            string _ociscenBrojZaPretragu = DajOciscenString(comboBoxBrojZaPretragu.Text);
            string _brojZaPretragu = comboBoxBrojZaPretragu.Text;

            if (_ociscenBrojZaPretragu != "")
            {
                this.Cursor = Cursors.Wait;

                LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

                IQueryable<Baza.Artikal> _upit;


                //ako se pretrazuje po svim vrstama brojeva
                if ((bool)checkBoxBiloKojiBroj.IsChecked)
                {
                    //ako je slicna pretraga
                    if ((bool)checkBoxSlicnoTrazenje.IsChecked)
                    {
                        if ((bool)checkBoxSamoSaCenom.IsChecked)
                        {
                            _upit = (from p in LavAutoDataContext.Artikals
                                     join v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                     on p.Artikal_ID equals v.Artikal_ID
                                     join d in LavAutoDataContext.VezaArtikalDobavljacs
                                     on p.Artikal_ID equals d.Artikal_ID
                                     where v.BrojZaPretragu.Contains(_ociscenBrojZaPretragu)
                                     select p).Distinct().Take(500);
                        }
                        else
                        {
                            _upit = (from p in LavAutoDataContext.Artikals
                                     join v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                     on p.Artikal_ID equals v.Artikal_ID
                                     where v.BrojZaPretragu.Contains(_ociscenBrojZaPretragu)
                                     select p).Distinct().Take(500);
                        }

                    }
                    else
                    {

                        if ((bool)checkBoxSamoSaCenom.IsChecked)
                        {
                            _upit = (from p in LavAutoDataContext.Artikals
                                     join v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                     on p.Artikal_ID equals v.Artikal_ID
                                     join d in LavAutoDataContext.VezaArtikalDobavljacs
                                     on p.Artikal_ID equals d.Artikal_ID
                                     where v.BrojZaPretragu == _ociscenBrojZaPretragu
                                     select p).Distinct().Take(500);

                        }
                        else
                        {
                            _upit = (from p in LavAutoDataContext.Artikals
                                     join v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                     on p.Artikal_ID equals v.Artikal_ID
                                     where v.BrojZaPretragu == _ociscenBrojZaPretragu
                                     select p).Distinct().Take(500);
                        }

                    }
                }
                else
                {

                    List<byte> _vrstaBrojaZaPretragu = new List<byte>();


                    if ((bool)checkBoxBrojProizvodjaca.IsChecked)
                    {
                        _vrstaBrojaZaPretragu.Add(1);
                    }
                    if ((bool)checkBoxOEBroj.IsChecked)
                    {
                        _vrstaBrojaZaPretragu.Add(3);
                    }
                    if ((bool)checkBoxUporedniBroj.IsChecked)
                    {
                        _vrstaBrojaZaPretragu.Add(4);
                    }
                    if ((bool)checkBoxKoriscenBroj.IsChecked)
                    {
                        _vrstaBrojaZaPretragu.Add(2);
                    }
                    if ((bool)checkBoxEanBroj.IsChecked)
                    {
                        _vrstaBrojaZaPretragu.Add(5);
                    }

                    if ((bool)checkBoxSlicnoTrazenje.IsChecked)
                    {
                        if ((bool)checkBoxSamoSaCenom.IsChecked)
                        {
                            _upit = (from p in LavAutoDataContext.Artikals
                                     join v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                     on p.Artikal_ID equals v.Artikal_ID
                                     join d in LavAutoDataContext.VezaArtikalDobavljacs
                                     on p.Artikal_ID equals d.Artikal_ID
                                     where v.BrojZaPretragu.Contains(_ociscenBrojZaPretragu)
                                     && _vrstaBrojaZaPretragu.Contains(v.VrstaBrojaZaPretragu_ID)
                                     select p).Distinct().Take(500);

                        }
                        else
                        {
                            _upit = (from p in LavAutoDataContext.Artikals
                                     join v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                     on p.Artikal_ID equals v.Artikal_ID
                                     where v.BrojZaPretragu.Contains(_ociscenBrojZaPretragu)
                                     && _vrstaBrojaZaPretragu.Contains(v.VrstaBrojaZaPretragu_ID)
                                     select p).Distinct().Take(500);
                        }
                    }
                    else
                    {
                        if ((bool)checkBoxSamoSaCenom.IsChecked)
                        {
                            _upit = (from p in LavAutoDataContext.Artikals
                                     join v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                     on p.Artikal_ID equals v.Artikal_ID
                                     join d in LavAutoDataContext.VezaArtikalDobavljacs
                                     on p.Artikal_ID equals d.Artikal_ID
                                     where v.BrojZaPretragu == _ociscenBrojZaPretragu
                                     && _vrstaBrojaZaPretragu.Contains(v.VrstaBrojaZaPretragu_ID)
                                     select p).Distinct().Take(500);

                        }
                        else
                        {
                            _upit = (from p in LavAutoDataContext.Artikals
                                     join v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                     on p.Artikal_ID equals v.Artikal_ID
                                     where v.BrojZaPretragu == _ociscenBrojZaPretragu
                                     && _vrstaBrojaZaPretragu.Contains(v.VrstaBrojaZaPretragu_ID)
                                     select p).Distinct().Take(500);
                        }

                    }
                }
                try
                {

                    ObservableCollection<Baza.Artikal> _lista = new ObservableCollection<Baza.Artikal>(_upit.ToList());

                    listBoxListaProizvoda.ItemsSource = _lista;


                    ICollectionView view = CollectionViewSource.GetDefaultView(listBoxListaProizvoda.ItemsSource);
                    view.GroupDescriptions.Add(new PropertyGroupDescription("Proizvodjac.Naziv"));

                    view.SortDescriptions.Add(
                    new SortDescription("OpisTabela.Opis", ListSortDirection.Ascending));


                    //ako je rezultat nula artikala obavesti korisnika
                    if (_lista.Count().Equals(0))
                    {
                        MessageBox.Show("Za zadati broj nije nađen ni jedan artikal. ", "Artikal",  MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                    else
                    {
                        List<int> _zaBrisanje = new List<int>();

                        foreach (var item in comboBoxBrojZaPretragu.Items)
                        {
                            if (item.ToString() == _brojZaPretragu)
                            {
                                _zaBrisanje.Add(comboBoxBrojZaPretragu.Items.IndexOf(item));
                            }
                        }

                        foreach (int item in _zaBrisanje)
                        {
                            comboBoxBrojZaPretragu.Items.RemoveAt(item);
                        }
                        
                        comboBoxBrojZaPretragu.Items.Insert(0,_brojZaPretragu);
                        comboBoxBrojZaPretragu.Text = _brojZaPretragu;



                        AutoServis.Properties.Settings.Default.DesetPoslednjihUspesnihPretraga.Clear();

                        int _i = 0;
                        foreach (var item in comboBoxBrojZaPretragu.Items)
                        {
                            AutoServis.Properties.Settings.Default.DesetPoslednjihUspesnihPretraga.Add(item.ToString());
                            _i++;

                            if (_i == 10)
                                break;
                        }

                        AutoServis.Properties.Settings.Default.Save();

                    }

                }
                catch (Exception ex)
                {
                    this.Cursor = Cursors.Arrow;

                    MessageBox.Show(ex.Message, "Greška pri pretrazi artikala", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                this.Cursor = Cursors.Arrow;

                
            }
        }

        //ne koristi se
        private void listBoxListaProizvoda_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ne koristi se jer se artikal odabere na cenovniku preko dobavljaca jer na ponudu ne moze ici artikal koji nema cenu
            //if (OdaberiArtikal)
            //{
            //    Baza.Artikal _trenutni = (Baza.Artikal)listBoxListaProizvoda.SelectedItem;
            //    OnReturn(new ReturnEventArgs<Object>(_trenutni));
            //}
        }

        //----------------------------------------------------------------------
        private void buttonArtikalDetaljno_Click(object sender, RoutedEventArgs e)
        {
            Button _buttonArtikalDetaljno = (Button)sender;
            Baza.Artikal _artikal = (Baza.Artikal)_buttonArtikalDetaljno.Tag;

            ArtikalDetaljno _artikalDetaljno = new ArtikalDetaljno(_artikal, OdaberiArtikal);
            _artikalDetaljno.Return += new ReturnEventHandler<object>(_artikalDetaljno_Return);
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(_artikalDetaljno);
        }

        void _artikalDetaljno_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                OnReturn(new ReturnEventArgs<Object>(e.Result));
            }
        }


    }
}
