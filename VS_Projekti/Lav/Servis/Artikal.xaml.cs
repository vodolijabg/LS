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
using System.ComponentModel;
using System.Collections.ObjectModel;
using System.Data.Linq;
using System.Windows.Threading;

namespace Servis
{
    /// <summary>
    /// Interaction logic for Artikal.xaml
    /// </summary>
    public partial class Artikal : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;
        bool prvoOtvaranjePretragaRoba = true;
        bool prvoOtvaranjePretragaAkumulatora = true;
        bool prvoOtvaranjePretragaUlja = true;
        bool prvoOtvaranjePretragaGuma = true;

        double najpovoljnijiDobavljacHeaderWidth = 0;
        double brojProizvodjacaHeaderWidth = 0;



        DB.DBProksi dBProksi;

        Servis.StavkaArtikalDetaljno stavkaArtikalDetaljno;

        delegate void napuniIzvorZaPadajuceListeDelegat();
        delegate void napuniPadajuceListeDelegat();


        public Artikal()
        {
            InitializeComponent();
        }

        public Artikal(Servis.StavkaArtikalDetaljno stavkaArtikalDetaljno) : this()
        {
            this.stavkaArtikalDetaljno = stavkaArtikalDetaljno;

            if (stavkaArtikalDetaljno.stanje == App.Stanje.Unos)
            {
                contextMenuDodaj.Visibility = System.Windows.Visibility.Visible;
            }
        }

        private void NapuniPadajuceListe()
        {
            try
            {
                ObservableCollection<DB.Proizvodjac> _padajucaListaProizvodjacRobeRoban = dBProksi.DajPadajucuListuProizvodjacRobeRoban();

                ObservableCollection<DB.PadajucaListaViskozitetUljaRoban> _padajucaListaViskozitetUljaRoban = dBProksi.DajPadajucuListuViskozitetUljaRoban();
                ObservableCollection<DB.PadajucaListaProizvodjaciUljaRoban> _padajucaListaProizvodjaciUljaRoban = dBProksi.DajPadajucuListuProizvodjacUljaRoban();

                ObservableCollection<DB.PadajucaListaProizvodjaciAkumulatoraRoban> _padajucaListaProizvodjaciAkumulatoraRoban = dBProksi.DajPadajucuListuProizvodjacAkumulatoraRoban();

                ObservableCollection<DB.PadajucaListaProizvodjaciGumaRoban> _padajucaListaProizvodjaciGumaRoban = dBProksi.DajPadajucuListuProizvodjacGumaRoban();
                ObservableCollection<DB.PadajucaListaNamenaGumaRoban> _padajucaListaNamenaGumaRoban = dBProksi.DajPadajucuListuNemanaGumaRoban();
                ObservableCollection<DB.PadajucaListaSezonaGumaRoban> _padajucaListaSezonaGumaRoban = dBProksi.DajPadajucuListuSezonaGumaRoban();


                if (!_padajucaListaProizvodjacRobeRoban.Count.Equals(0))
                {
                    _padajucaListaProizvodjacRobeRoban.Insert(0, new DB.Proizvodjac());
                }
                if (!_padajucaListaViskozitetUljaRoban.Count.Equals(0))
                {
                    _padajucaListaViskozitetUljaRoban.Insert(0, new DB.PadajucaListaViskozitetUljaRoban());
                }
                if (!_padajucaListaProizvodjaciUljaRoban.Count.Equals(0))
                {
                    _padajucaListaProizvodjaciUljaRoban.Insert(0, new DB.PadajucaListaProizvodjaciUljaRoban());
                }
                if (!_padajucaListaProizvodjaciAkumulatoraRoban.Count.Equals(0))
                {
                    _padajucaListaProizvodjaciAkumulatoraRoban.Insert(0, new DB.PadajucaListaProizvodjaciAkumulatoraRoban());
                }
                if (!_padajucaListaProizvodjaciGumaRoban.Count.Equals(0))
                {
                    _padajucaListaProizvodjaciGumaRoban.Insert(0, new DB.PadajucaListaProizvodjaciGumaRoban());
                }
                if (!_padajucaListaNamenaGumaRoban.Count.Equals(0))
                {
                    _padajucaListaNamenaGumaRoban.Insert(0, new DB.PadajucaListaNamenaGumaRoban());
                }
                if (!_padajucaListaSezonaGumaRoban.Count.Equals(0))
                {
                    _padajucaListaSezonaGumaRoban.Insert(0, new DB.PadajucaListaSezonaGumaRoban());
                }

                comboBoxProizvodjacRoba.ItemsSource = _padajucaListaProizvodjacRobeRoban;
                comboBoxViskozitetUlja.ItemsSource = _padajucaListaViskozitetUljaRoban;
                comboBoxProizvodjacUlja.ItemsSource = _padajucaListaProizvodjaciUljaRoban;
                comboBoxProizvodjacAkumulatora.ItemsSource = _padajucaListaProizvodjaciAkumulatoraRoban;
                comboBoxProizvodjacGuma.ItemsSource = _padajucaListaProizvodjaciGumaRoban;
                comboBoxNamenaGuma.ItemsSource = _padajucaListaNamenaGumaRoban;
                comboBoxSezonaGuma.ItemsSource = _padajucaListaSezonaGumaRoban;


                if (!_padajucaListaProizvodjacRobeRoban.Count.Equals(0))
                {
                    comboBoxProizvodjacRoba.SelectedIndex = 0;
                }
                if (!_padajucaListaViskozitetUljaRoban.Count.Equals(0))
                {
                    comboBoxViskozitetUlja.SelectedIndex = 0;
                }
                if (!_padajucaListaProizvodjaciUljaRoban.Count.Equals(0))
                {
                    comboBoxProizvodjacUlja.SelectedIndex = 0;
                }
                if (!_padajucaListaProizvodjaciAkumulatoraRoban.Count.Equals(0))
                {
                    comboBoxProizvodjacAkumulatora.SelectedIndex = 0;
                }
                if (!_padajucaListaProizvodjaciGumaRoban.Count.Equals(0))
                {
                    comboBoxProizvodjacGuma.SelectedIndex = 0;
                }
                if (!_padajucaListaNamenaGumaRoban.Count.Equals(0))
                {
                    comboBoxNamenaGuma.SelectedIndex = 0;
                }
                if (!_padajucaListaSezonaGumaRoban.Count.Equals(0))
                {
                    comboBoxSezonaGuma.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private string DajOciscenString(string stringZaCiscenje)
        {
            string[] _karakteriZaIzbacivanje = { ".", ",", "-", " ", "/" };

            StringBuilder _sb = new StringBuilder(stringZaCiscenje);

            foreach (string _s in _karakteriZaIzbacivanje)
            {
                _sb.Replace(_s, "");
            }

            return _sb.ToString();
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                if (System.IO.File.Exists(System.IO.Directory.GetCurrentDirectory() + @"\BrojZaPretraguIstorija.xml"))
                {
                    System.Xml.Serialization.XmlSerializer _xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                    System.IO.TextReader r = new System.IO.StreamReader(System.IO.Directory.GetCurrentDirectory() + @"\BrojZaPretraguIstorija.xml");
                    comboBoxBrojZaPretragu.ItemsSource = (List<string>)_xmlSerializer.Deserialize(r);
                    r.Close();
                }
                else
                {
                    comboBoxBrojZaPretragu.ItemsSource = new List<string>();
                }

                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                prvoOtvaranjeStrane = false;

                foreach (GridViewColumn item in ((GridView)listViewArtikal.View).Columns)
                {
                    if (item.Header != null && item.Header.ToString() == "System.Windows.Controls.GridViewColumnHeader: Najpovoljniji dobavljač")
                    {
                        najpovoljnijiDobavljacHeaderWidth = item.Width;
                    }
                    if (item.Header != null && item.Header.ToString() == "System.Windows.Controls.GridViewColumnHeader: Broj proizvođača")
                    {
                        brojProizvodjacaHeaderWidth = item.Width;
                    }
                }

                //ako je dosao sa StavkaArtikalDetaljno
                if (stavkaArtikalDetaljno != null && stavkaArtikalDetaljno.textBoxArtikal.Text.Trim() != "")
                {
                    try
                    {
                        string[] _nizArtikal = stavkaArtikalDetaljno.textBoxArtikal.Tag.ToString().Split("$".ToCharArray());
                        string _brojProizvodjaca = _nizArtikal[0].ToString();
                        string _proizvodjacNaziv = _nizArtikal[1].ToString();
                        Int16 _proizvodjacID = Convert.ToInt16(_nizArtikal[2]);
                        string _artikalNaziv = _nizArtikal[3].ToString();

                        string[] _nizDobavljac = stavkaArtikalDetaljno.textBoxDobavljac.Tag.ToString().Split("$".ToCharArray());
                        int _poslovniPartner = Convert.ToInt32(_nizDobavljac[0]);
                        int _korisnikPrograma = Convert.ToInt32(_nizDobavljac[1]);

                        listViewArtikal.ItemsSource = dBProksi.NadjiArtikal(_brojProizvodjaca, _proizvodjacNaziv).Distinct();


                        foreach (DB.Artikal item in listViewArtikal.Items)
                        {
                            bool _nadjenArtikalDobavljaca = false;
                            listViewArtikal.SelectedItem = item;


                            foreach (DB.VezaArtikalDobavljac item1 in listViewDobavljaci.Items)
                            {
                                if (_korisnikPrograma != -1)
                                {
                                    if (_korisnikPrograma == item1.KorisnikProgramaID)
                                    {
                                        listViewDobavljaci.SelectedItem = item1;
                                        _nadjenArtikalDobavljaca = true;
                                        break;
                                    }
                                }
                                else if (_poslovniPartner != -1)
                                {
                                    if (_poslovniPartner == item1.PoslovniPartnerID)
                                    {
                                        listViewDobavljaci.SelectedItem = item1;
                                        _nadjenArtikalDobavljaca = true;
                                        break;
                                    }
                                }
                            }

                            if (_nadjenArtikalDobavljaca)
                            {
                                break;
                            }
                        }


                    }
                    catch (Exception)
                    {
                    }
                }

                //NapuniPadajuceListe();
                //this.Dispatcher.BeginInvoke(DispatcherPriority.Background, new napuniPadajuceListeDelegat(NapuniPadajuceListe));

                //napuniIzvorZaPadajuceListeDelegat _delegat = new napuniIzvorZaPadajuceListeDelegat(ZapocniNapuniIzvorZaPadajuceListe);
                //AsyncCallback _callback = new AsyncCallback(ZavrsiNapuniIzvorZaPadajuceListe);
                //_delegat.BeginInvoke(_callback, null);

            }

            Window.GetWindow(this).Title = "Lav - Artikal";
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

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {

        }

        private void listViewArtikal_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DB.Artikal _artikal = (DB.Artikal)listViewArtikal.SelectedItem;

            //_vezaArtikalDobavljac = _vezaArtikalDobavljacLista.OrderByDescending(o1 => o1.KorisnikProgramaID).ThenBy(o2 => o2.CenaBezPoreza).First();

            if (_artikal != null)
            {
                listViewDobavljaci.ItemsSource = new ObservableCollection<DB.VezaArtikalDobavljac>(_artikal.VezaArtikalDobavljacs.ToList()).OrderByDescending(o1 => o1.KorisnikProgramaID).ThenBy(o2 => o2.Cena);
            }

        }

        private void buttonDobavljacDetaljno_Click(object sender, RoutedEventArgs e)
        {
            Button _buttonDobavljacDetaljno = (Button)sender;
            DB.VezaArtikalDobavljac _vezaArtikalDobavljac = (DB.VezaArtikalDobavljac)_buttonDobavljacDetaljno.Tag;

            if (_vezaArtikalDobavljac.PoslovniPartnerID != null)
            {
                PoslovniPartnerDetaljno _poslovniPartnerDetaljno = new PoslovniPartnerDetaljno(this, (int)_vezaArtikalDobavljac.PoslovniPartnerID);
                //_poslovniPartnerDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _poslovniPartnerDetaljno.Owner = Window.GetWindow(this);
                _poslovniPartnerDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _poslovniPartnerDetaljno.ShowDialog();
            }
        }


        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //ako nema filtera 
                if (comboBoxBrojZaPretragu.Text.Trim() == "")
                {
                    MessageBox.Show("Unesi uslov u broj za pretragu.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewDobavljaci.ItemsSource = null;

                    this.Cursor = Cursors.Wait;

                    listViewArtikal.ItemsSource = dBProksi.NadjiArtikal(DajOciscenString(comboBoxBrojZaPretragu.Text), (bool)checkBoxSlicnoTrazenje.IsChecked, (bool)checkBoxSamoSaCenom.IsChecked,
                                                                            (bool)checkBoxBiloKojiBroj.IsChecked, (bool)checkBoxBrojProizvodjaca.IsChecked, (bool)checkBoxOEBroj.IsChecked,
                                                                                (bool)checkBoxKoriscenBroj.IsChecked, (bool)checkBoxUporedniBroj.IsChecked, (bool)checkBoxEanBroj.IsChecked)
                                                                                .Distinct();

                    //ICollectionView _view = CollectionViewSource.GetDefaultView(listViewArtikal.ItemsSource);
                    //_view.SortDescriptions.Add(new SortDescription("Proizvodjac.Naziv", ListSortDirection.Ascending));
                }

                if (listViewArtikal.Items.Count.Equals(0))
                {
                    MessageBox.Show("Za zadati uslov nije pronađen ni jedan artikal.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (sortirajPo != "")
                    {
                        Sort(sortirajPo, lastDirection);
                    }
                    else
                    {
                        Sort("Proizvodjac.Naziv", ListSortDirection.Ascending);
                    }

                    List<string> _brojZaPretraguIstorija = (List<string>)comboBoxBrojZaPretragu.ItemsSource;


                    //ako broj postoji u listi obrisi ga
                    while (!_brojZaPretraguIstorija.IndexOf(comboBoxBrojZaPretragu.Text).Equals(-1))
                    {
                        _brojZaPretraguIstorija.RemoveAt(_brojZaPretraguIstorija.IndexOf(comboBoxBrojZaPretragu.Text));
                    }

                    //dodaj broj na prvo mesto
                    _brojZaPretraguIstorija.Insert(0, comboBoxBrojZaPretragu.Text);

                    comboBoxBrojZaPretragu.ItemsSource = _brojZaPretraguIstorija.Take(10).ToList();

                    try
                    {
                        System.Xml.Serialization.XmlSerializer _xmlSerializer = new System.Xml.Serialization.XmlSerializer(typeof(List<string>));
                        System.IO.TextWriter _textWriter = new System.IO.StreamWriter(System.IO.Directory.GetCurrentDirectory() + @"\BrojZaPretraguIstorija.xml");
                        _xmlSerializer.Serialize(_textWriter, (List<string>)comboBoxBrojZaPretragu.ItemsSource);
                        _textWriter.Close();
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

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

        private void buttonNadjiAkumulator_Click(object sender, RoutedEventArgs e)
        {
            //if (textBoxAmperazaAkumulatora.Text.Trim() == "" && comboBoxProizvodjacAkumulatora.SelectedItem == null)
            //{
            //    MessageBox.Show("Unesi uslov za pretragu.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            //else
            //{
            try
            {
                int _amperazaAkumulatora = 0;
                if (textBoxAmperazaAkumulatora.Text.Trim() != "" && !Int32.TryParse(textBoxAmperazaAkumulatora.Text, out _amperazaAkumulatora))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Amperaza.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return;
                }

                listViewDobavljaci.ItemsSource = null;

                this.Cursor = Cursors.Wait;

                DB.PadajucaListaProizvodjaciAkumulatoraRoban _proizvodjacAkumulatora = (DB.PadajucaListaProizvodjaciAkumulatoraRoban)comboBoxProizvodjacAkumulatora.SelectedItem;

                int? _i1 = null;
                int? _i2 = null;

                listViewArtikal.ItemsSource = dBProksi.NadjiAkumulatorRoban(
                    _proizvodjacAkumulatora != null && _proizvodjacAkumulatora.Naziv != null ? _proizvodjacAkumulatora.Proizvodjac_ID : _i1,
                    textBoxAmperazaAkumulatora.Text.Trim() != "" ? _amperazaAkumulatora : _i2
                    ).Distinct();

                ICollectionView _view = CollectionViewSource.GetDefaultView(listViewArtikal.ItemsSource);

                if (listViewArtikal.Items.Count.Equals(0))
                {
                    MessageBox.Show("Za zadati uslov nije pronađen ni jedan artikal.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (sortirajPo != "")
                    {
                        Sort(sortirajPo, lastDirection);
                    }
                    else
                    {
                        Sort("Proizvodjac.Naziv", ListSortDirection.Ascending);
                    }
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
            //}
        }

        private void buttonNadjiUlje_Click(object sender, RoutedEventArgs e)
        {
            //if (comboBoxProizvodjacUlja.SelectedItem == null && comboBoxViskozitetUlja.SelectedItem == null)
            //{
            //    MessageBox.Show("Unesi uslov za pretragu.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            //else
            //{
            try
            {
                listViewDobavljaci.ItemsSource = null;

                this.Cursor = Cursors.Wait;

                DB.PadajucaListaProizvodjaciUljaRoban _proizvodjacUlja = (DB.PadajucaListaProizvodjaciUljaRoban)comboBoxProizvodjacUlja.SelectedItem;
                DB.PadajucaListaViskozitetUljaRoban _viskozitetUlja = (DB.PadajucaListaViskozitetUljaRoban)comboBoxViskozitetUlja.SelectedItem;

                int? _i = null;

                listViewArtikal.ItemsSource = dBProksi.NadjiUljeRoban(
                    _proizvodjacUlja != null && _proizvodjacUlja.Naziv != null ? _proizvodjacUlja.Proizvodjac_ID : _i,
                    _viskozitetUlja != null && _viskozitetUlja.Vrednost != null ? _viskozitetUlja.Vrednost : null
                    ).Distinct();

                ICollectionView _view = CollectionViewSource.GetDefaultView(listViewArtikal.ItemsSource);

                if (listViewArtikal.Items.Count.Equals(0))
                {
                    MessageBox.Show("Za zadati uslov nije pronađen ni jedan artikal.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (sortirajPo != "")
                    {
                        Sort(sortirajPo, lastDirection);
                    }
                    else
                    {
                        Sort("Proizvodjac.Naziv", ListSortDirection.Ascending);
                    }
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
            //}
        }

        private void buttonNadjiGumu_Click(object sender, RoutedEventArgs e)
        {
            //if (comboBoxProizvodjacGuma.SelectedItem == null && comboBoxNamenaGuma.SelectedItem == null && comboBoxSezonaGuma.SelectedItem == null && textBoxDimenzijaGume.Text == "")
            //{
            //    MessageBox.Show("Unesi uslov za pretragu.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            //    return;
            //}
            //else
            //{
            try
            {
                listViewDobavljaci.ItemsSource = null;

                this.Cursor = Cursors.Wait;

                DB.PadajucaListaProizvodjaciGumaRoban _proizvodjacGuma = (DB.PadajucaListaProizvodjaciGumaRoban)comboBoxProizvodjacGuma.SelectedItem;
                DB.PadajucaListaNamenaGumaRoban _namenaGuma = (DB.PadajucaListaNamenaGumaRoban)comboBoxNamenaGuma.SelectedItem;
                DB.PadajucaListaSezonaGumaRoban _sezonaGuma = (DB.PadajucaListaSezonaGumaRoban)comboBoxSezonaGuma.SelectedItem;

                int? _i = null;

                listViewArtikal.ItemsSource = dBProksi.NadjiGumuRoban(
                    _proizvodjacGuma != null && _proizvodjacGuma.Naziv != null ? _proizvodjacGuma.Proizvodjac_ID : _i,
                    _namenaGuma != null && _namenaGuma.Vrednost != null ? _namenaGuma.Vrednost : null,
                    _sezonaGuma != null && _sezonaGuma.Vrednost != null ? _sezonaGuma.Vrednost : null,
                    textBoxDimenzijaGume.Text != "" ? textBoxDimenzijaGume.Text : null
                    ).Distinct();

                ICollectionView _view = CollectionViewSource.GetDefaultView(listViewArtikal.ItemsSource);

                if (listViewArtikal.Items.Count.Equals(0))
                {
                    MessageBox.Show("Za zadati uslov nije pronađen ni jedan artikal.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (sortirajPo != "")
                    {
                        Sort(sortirajPo, lastDirection);
                    }
                    else
                    {
                        Sort("Proizvodjac.Naziv", ListSortDirection.Ascending);
                    }
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
            //}
        }

        private void buttonNadjiRoba_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //if (textBoxSifra.Text.Trim() == "" && textBoxNaziv.Text.Trim() == "" && comboBoxProizvodjacRoba.SelectedItem == null)
                //{
                //    MessageBox.Show("Unesi uslov za pretragu.", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
                //    return;
                //}
                //else
                //{
                listViewDobavljaci.ItemsSource = null;

                this.Cursor = Cursors.Wait;

                DB.Proizvodjac _proizvodjacRobe = (DB.Proizvodjac)comboBoxProizvodjacRoba.SelectedItem;
                short? _i = null;



                listViewArtikal.ItemsSource = dBProksi.NadjiRobuRoban(
                    _proizvodjacRobe != null && _proizvodjacRobe.Naziv != null ? _proizvodjacRobe.Proizvodjac_ID : _i,
                    textBoxSifra.Text, textBoxNaziv.Text);

                ICollectionView _view = CollectionViewSource.GetDefaultView(listViewArtikal.ItemsSource);


                if (listViewArtikal.Items.Count.Equals(0))
                {
                    MessageBox.Show("Za zadati uslov nije pronađen ni jedan artikal.", "Obaveštenje", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                else
                {
                    if (sortirajPo != "")
                    {
                        Sort(sortirajPo, lastDirection);
                    }
                    else
                    {
                        Sort("Proizvodjac.Naziv", ListSortDirection.Ascending);
                    }
                }
                //}
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


        private void buttonArtikalDetaljno_Click(object sender, RoutedEventArgs e)
        {
            Button _buttonArtikalDetaljno = (Button)sender;
            DB.Artikal _artikal = (DB.Artikal)_buttonArtikalDetaljno.Tag;

            ArtikalDetaljno _artikalDetaljno = new ArtikalDetaljno(_artikal.Artikal_ID, DajOciscenString(comboBoxBrojZaPretragu.Text));
            //_artikalDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _artikalDetaljno.Owner = Window.GetWindow(this);
            _artikalDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _artikalDetaljno.ShowDialog();
        }

        private void tabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                TabControl _tab = (TabControl)sender;
                TabItem _trenutiItem = (TabItem)_tab.SelectedItem;

                if (_trenutiItem != null)
                {
                    if (_trenutiItem.Header.ToString() == "Pretraga po brojevima")
                    {
                        foreach (GridViewColumn item in ((GridView)listViewArtikal.View).Columns)
                        {
                            if (item.Header != null && item.Header.ToString() == "System.Windows.Controls.GridViewColumnHeader: Najpovoljniji dobavljač")
                            {
                                if (najpovoljnijiDobavljacHeaderWidth > 0)
                                {
                                    item.Width = najpovoljnijiDobavljacHeaderWidth;
                                }
                            }
                            if (item.Header != null && item.Header.ToString() == "System.Windows.Controls.GridViewColumnHeader: Broj proizvođača")
                            {
                                if (brojProizvodjacaHeaderWidth > 0)
                                {
                                    item.Width = brojProizvodjacaHeaderWidth;
                                }
                            }
                        }

                    }
                    else
                    {
                        foreach (GridViewColumn item in ((GridView)listViewArtikal.View).Columns)
                        {
                            if (item.Header != null && item.Header.ToString() == "System.Windows.Controls.GridViewColumnHeader: Najpovoljniji dobavljač")
                            {
                                item.Width = 0;
                            }
                            if (item.Header != null && item.Header.ToString() == "System.Windows.Controls.GridViewColumnHeader: Broj proizvođača")
                            {

                                item.Width = 0;
                            }
                        }
                    }



                    listViewArtikal.ItemsSource = new ObservableCollection<DB.Artikal>();
                    listViewDobavljaci.ItemsSource = null;

                    if (_trenutiItem.Header.ToString() == "Pretraga roba" && prvoOtvaranjePretragaRoba)
                    {
                        prvoOtvaranjePretragaRoba = false;
                        this.Cursor = Cursors.Wait;

                        ObservableCollection<DB.Proizvodjac> _padajucaListaProizvodjaciRobeRoban = dBProksi.DajPadajucuListuProizvodjacRobeRoban();

                        if (!_padajucaListaProizvodjaciRobeRoban.Count.Equals(0))
                        {
                            _padajucaListaProizvodjaciRobeRoban.Insert(0, new DB.Proizvodjac());
                        }
                        comboBoxProizvodjacRoba.ItemsSource = _padajucaListaProizvodjaciRobeRoban;

                        if (!_padajucaListaProizvodjaciRobeRoban.Count.Equals(0))
                        {
                            comboBoxProizvodjacAkumulatora.SelectedIndex = 0;
                        }
                    }
                    else if (_trenutiItem.Header.ToString() == "Pretraga akumulatora" && prvoOtvaranjePretragaAkumulatora)
                    {
                        prvoOtvaranjePretragaAkumulatora = false;
                        this.Cursor = Cursors.Wait;


                        ObservableCollection<DB.PadajucaListaProizvodjaciAkumulatoraRoban> _padajucaListaProizvodjaciAkumulatoraRoban = dBProksi.DajPadajucuListuProizvodjacAkumulatoraRoban();

                        if (!_padajucaListaProizvodjaciAkumulatoraRoban.Count.Equals(0))
                        {
                            _padajucaListaProizvodjaciAkumulatoraRoban.Insert(0, new DB.PadajucaListaProizvodjaciAkumulatoraRoban());
                        }
                        comboBoxProizvodjacAkumulatora.ItemsSource = _padajucaListaProizvodjaciAkumulatoraRoban;

                        if (!_padajucaListaProizvodjaciAkumulatoraRoban.Count.Equals(0))
                        {
                            comboBoxProizvodjacAkumulatora.SelectedIndex = 0;
                        }
                    }
                    else if (_trenutiItem.Header.ToString() == "Pretraga ulja" && prvoOtvaranjePretragaUlja)
                    {
                        prvoOtvaranjePretragaUlja = false;
                        this.Cursor = Cursors.Wait;


                        ObservableCollection<DB.PadajucaListaViskozitetUljaRoban> _padajucaListaViskozitetUljaRoban = dBProksi.DajPadajucuListuViskozitetUljaRoban();
                        ObservableCollection<DB.PadajucaListaProizvodjaciUljaRoban> _padajucaListaProizvodjaciUljaRoban = dBProksi.DajPadajucuListuProizvodjacUljaRoban();

                        if (!_padajucaListaViskozitetUljaRoban.Count.Equals(0))
                        {
                            _padajucaListaViskozitetUljaRoban.Insert(0, new DB.PadajucaListaViskozitetUljaRoban());
                        }
                        if (!_padajucaListaProizvodjaciUljaRoban.Count.Equals(0))
                        {
                            _padajucaListaProizvodjaciUljaRoban.Insert(0, new DB.PadajucaListaProizvodjaciUljaRoban());
                        }

                        comboBoxViskozitetUlja.ItemsSource = _padajucaListaViskozitetUljaRoban;
                        comboBoxProizvodjacUlja.ItemsSource = _padajucaListaProizvodjaciUljaRoban;


                        if (!_padajucaListaViskozitetUljaRoban.Count.Equals(0))
                        {
                            comboBoxViskozitetUlja.SelectedIndex = 0;
                        }
                        if (!_padajucaListaProizvodjaciUljaRoban.Count.Equals(0))
                        {
                            comboBoxProizvodjacUlja.SelectedIndex = 0;
                        }
                    }
                    else if (_trenutiItem.Header.ToString() == "Pretraga guma" && prvoOtvaranjePretragaGuma)
                    {
                        prvoOtvaranjePretragaGuma = false;
                        this.Cursor = Cursors.Wait;


                        ObservableCollection<DB.PadajucaListaProizvodjaciGumaRoban> _padajucaListaProizvodjaciGumaRoban = dBProksi.DajPadajucuListuProizvodjacGumaRoban();
                        ObservableCollection<DB.PadajucaListaNamenaGumaRoban> _padajucaListaNamenaGumaRoban = dBProksi.DajPadajucuListuNemanaGumaRoban();
                        ObservableCollection<DB.PadajucaListaSezonaGumaRoban> _padajucaListaSezonaGumaRoban = dBProksi.DajPadajucuListuSezonaGumaRoban();

                        if (!_padajucaListaProizvodjaciGumaRoban.Count.Equals(0))
                        {
                            _padajucaListaProizvodjaciGumaRoban.Insert(0, new DB.PadajucaListaProizvodjaciGumaRoban());
                        }
                        if (!_padajucaListaNamenaGumaRoban.Count.Equals(0))
                        {
                            _padajucaListaNamenaGumaRoban.Insert(0, new DB.PadajucaListaNamenaGumaRoban());
                        }
                        if (!_padajucaListaSezonaGumaRoban.Count.Equals(0))
                        {
                            _padajucaListaSezonaGumaRoban.Insert(0, new DB.PadajucaListaSezonaGumaRoban());
                        }

                        comboBoxProizvodjacGuma.ItemsSource = _padajucaListaProizvodjaciGumaRoban;
                        comboBoxNamenaGuma.ItemsSource = _padajucaListaNamenaGumaRoban;
                        comboBoxSezonaGuma.ItemsSource = _padajucaListaSezonaGumaRoban;

                        if (!_padajucaListaProizvodjaciGumaRoban.Count.Equals(0))
                        {
                            comboBoxProizvodjacGuma.SelectedIndex = 0;
                        }
                        if (!_padajucaListaNamenaGumaRoban.Count.Equals(0))
                        {
                            comboBoxNamenaGuma.SelectedIndex = 0;
                        }
                        if (!_padajucaListaSezonaGumaRoban.Count.Equals(0))
                        {
                            comboBoxSezonaGuma.SelectedIndex = 0;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            finally
            {
                if (this.Cursor == Cursors.Wait)
                {
                    this.Cursor = Cursors.Arrow;
                }
            }
        }

        private void comboBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            //bubbling event koji ne zelim da ide UP posto ga uhvati tabControl_SelectionChanged
            e.Handled = true;
        }

        GridViewColumnHeader lastHeaderClicked = null;
        ListSortDirection lastDirection = ListSortDirection.Ascending;
        string sortirajPo = "";

        void GridViewColumnHeader_Click(object sender, RoutedEventArgs e)
        {
            GridViewColumnHeader headerClicked = e.OriginalSource as GridViewColumnHeader;
            ListSortDirection direction;

            if (headerClicked != null && headerClicked.Tag != null)
            {
                if (lastHeaderClicked != null)
                {
                    lastHeaderClicked.Column.HeaderTemplate = null;
                }

                if (headerClicked != lastHeaderClicked)
                {
                    direction = ListSortDirection.Ascending;
                }
                else
                {
                    if (lastDirection == ListSortDirection.Ascending)
                    {
                        direction = ListSortDirection.Descending;
                    }
                    else
                    {
                        direction = ListSortDirection.Ascending;
                    }
                }

                sortirajPo = headerClicked.Tag.ToString();
                Sort(sortirajPo, direction);

                if (direction == ListSortDirection.Ascending)
                {
                    headerClicked.Column.HeaderTemplate =
                      Resources["HeaderTemplateArrowUp"] as DataTemplate;
                }
                else
                {
                    headerClicked.Column.HeaderTemplate =
                      Resources["HeaderTemplateArrowDown"] as DataTemplate;
                }

                lastHeaderClicked = headerClicked;
                lastDirection = direction;
            }
        }

        private void Sort(string sortBy, ListSortDirection direction)
        {
            listViewArtikal.Items.SortDescriptions.Clear();
            SortDescription sd = new SortDescription(sortBy, direction);
            listViewArtikal.Items.SortDescriptions.Add(sd);
            listViewArtikal.Items.Refresh();
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (stavkaArtikalDetaljno != null)
            {
                DB.VezaArtikalDobavljac _vezaArtikalDobavljac = (DB.VezaArtikalDobavljac)listViewDobavljaci.SelectedItem;

                stavkaArtikalDetaljno.textBoxDobavljac.Text = _vezaArtikalDobavljac.PoslovniPartnerID != null ? _vezaArtikalDobavljac.PoslovniPartner.SkracenNaziv : _vezaArtikalDobavljac.KorisnikPrograma.Naziv;
                if (_vezaArtikalDobavljac.PoslovniPartnerID != null)
                {
                    stavkaArtikalDetaljno.textBoxDobavljac.Tag = _vezaArtikalDobavljac.PoslovniPartnerID + "$" + "-1";
                }
                else if (_vezaArtikalDobavljac.KorisnikProgramaID != null)
                {
                    stavkaArtikalDetaljno.textBoxDobavljac.Tag = "-1" + "$" + _vezaArtikalDobavljac.KorisnikProgramaID;
                }

                stavkaArtikalDetaljno.textBoxArtikal.Text = _vezaArtikalDobavljac.Artikal.Proizvodjac.Naziv + " [" + _vezaArtikalDobavljac.Artikal.BrojProizvodjaca + "] - " + _vezaArtikalDobavljac.Artikal.OpisTabela.Opis;
                stavkaArtikalDetaljno.textBoxArtikal.Tag = _vezaArtikalDobavljac.Artikal.BrojProizvodjaca + "$" + _vezaArtikalDobavljac.Artikal.Proizvodjac.Naziv + "$" + _vezaArtikalDobavljac.Artikal.Proizvodjac_ID + "$" + _vezaArtikalDobavljac.Artikal.OpisTabela.Opis;

                if (Konfiguracija.VrstaCeneUCenovniku == "SaPDV")
                {
                    decimal d100 = 100;
                    decimal d118 = 118;
                    decimal d108 = 108;

                    decimal _poreskaStopa = 1;
                    if (_vezaArtikalDobavljac.Artikal.PoreskaStopa_ID == 3)//18%
                    {

                        _poreskaStopa = Convert.ToDecimal(d118 / d100, App.cultureInfo);
                    }
                    else if (_vezaArtikalDobavljac.Artikal.PoreskaStopa_ID == 4)//8%
                    {
                        _poreskaStopa = Convert.ToDecimal(d108 / d100, App.cultureInfo);
                    }

                    stavkaArtikalDetaljno.textBoxCenaBezPoreza.Text = (_vezaArtikalDobavljac.Cena / _poreskaStopa).ToString("##.00", App.cultureInfo);
                }
                else
                {
                    stavkaArtikalDetaljno.textBoxCenaBezPoreza.Text = _vezaArtikalDobavljac.Cena.ToString("##.00", App.cultureInfo);
                }


                stavkaArtikalDetaljno.textBoxPoreskaStopa.Text = _vezaArtikalDobavljac.Artikal.PoreskaStopa.VrednostProcenata.ToString("##.00", App.cultureInfo);
                stavkaArtikalDetaljno.textBoxPoreskaStopa.Tag = _vezaArtikalDobavljac.Artikal.PoreskaStopa_ID;

                Window.GetWindow(this).Close();
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            DB.VezaArtikalDobavljac _vezaArtikalDobavljac = (DB.VezaArtikalDobavljac)listViewDobavljaci.SelectedItem;

            if (_vezaArtikalDobavljac == null)
            {
                MessageBox.Show("Odaberi artikal dobavljača", "Informacija", MessageBoxButton.OK, MessageBoxImage.Information);
            }
            else
            {

                Int32 _kolicina;

                if (stavkaArtikalDetaljno.textBoxKolicina.Text.Trim() == "")
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost u polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else if (!Int32.TryParse(stavkaArtikalDetaljno.textBoxKolicina.Text, out _kolicina))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje Količina.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                }
                else
                {
                    try
                    {
                        this.Cursor = Cursors.Wait;

                        //DB.VezaArtikalDobavljac _vezaArtikalDobavljac = (DB.VezaArtikalDobavljac)listViewDobavljaci.SelectedItem;

                        stavkaArtikalDetaljno.textBoxDobavljac.Text = _vezaArtikalDobavljac.PoslovniPartnerID != null ? _vezaArtikalDobavljac.PoslovniPartner.SkracenNaziv : _vezaArtikalDobavljac.KorisnikPrograma.Naziv;
                        if (_vezaArtikalDobavljac.PoslovniPartnerID != null)
                        {
                            stavkaArtikalDetaljno.textBoxDobavljac.Tag = _vezaArtikalDobavljac.PoslovniPartnerID + "$" + "-1";
                        }
                        else if (_vezaArtikalDobavljac.KorisnikProgramaID != null)
                        {
                            stavkaArtikalDetaljno.textBoxDobavljac.Tag = "-1" + "$" + _vezaArtikalDobavljac.KorisnikProgramaID;
                        }

                        stavkaArtikalDetaljno.textBoxArtikal.Text = _vezaArtikalDobavljac.Artikal.Proizvodjac.Naziv + " [" + _vezaArtikalDobavljac.Artikal.BrojProizvodjaca + "] - " + _vezaArtikalDobavljac.Artikal.OpisTabela.Opis;
                        stavkaArtikalDetaljno.textBoxArtikal.Tag = _vezaArtikalDobavljac.Artikal.BrojProizvodjaca + "$" + _vezaArtikalDobavljac.Artikal.Proizvodjac.Naziv + "$" + _vezaArtikalDobavljac.Artikal.Proizvodjac_ID + "$" + _vezaArtikalDobavljac.Artikal.OpisTabela.Opis;

                        if (Konfiguracija.VrstaCeneUCenovniku == "SaPDV")
                        {
                            decimal _poreskaStopa = 1;
                            if (_vezaArtikalDobavljac.Artikal.PoreskaStopa_ID == 3)//18%
                            {
                                _poreskaStopa = Convert.ToDecimal("1,18");
                            }
                            else if (_vezaArtikalDobavljac.Artikal.PoreskaStopa_ID == 4)//8%
                            {
                                _poreskaStopa = Convert.ToDecimal("1,08");
                            }

                            stavkaArtikalDetaljno.textBoxCenaBezPoreza.Text = (_vezaArtikalDobavljac.Cena / _poreskaStopa).ToString("##.00");
                        }
                        else
                        {
                            stavkaArtikalDetaljno.textBoxCenaBezPoreza.Text = _vezaArtikalDobavljac.Cena.ToString("##.00");
                        }


                        stavkaArtikalDetaljno.textBoxPoreskaStopa.Text = _vezaArtikalDobavljac.Artikal.PoreskaStopa.VrednostProcenata.ToString();
                        stavkaArtikalDetaljno.textBoxPoreskaStopa.Tag = _vezaArtikalDobavljac.Artikal.PoreskaStopa_ID;

                        stavkaArtikalDetaljno.SacuvajINovi();

                        //decimal _poreskaStopa = 1;
                        //if (_vezaArtikalDobavljac.Artikal.PoreskaStopa_ID == 3)//18%
                        //{
                        //    _poreskaStopa = Convert.ToDecimal("1,18");
                        //}
                        //else if (_vezaArtikalDobavljac.Artikal.PoreskaStopa_ID == 4)//8%
                        //{
                        //    _poreskaStopa = Convert.ToDecimal("1,08");
                        //}

                        //DB.StavkaArtikal _stavkaArtikal = new DB.StavkaArtikal
                        //{
                        //    StavkaUslugaID = ((DB.StavkaUsluga)stavkaArtikalDetaljno.ponudaDetaljno.listViewStavkaUsluga.SelectedItem).StavkaUslugaID,
                        //    ArtikalKolicina = Convert.ToInt32(stavkaArtikalDetaljno.textBoxKolicina.Text.Trim()),
                        //    ArtikalCenaBezPoreza = Convert.ToDecimal((_vezaArtikalDobavljac.Cena / _poreskaStopa).ToString("##.00")),
                        //    ArtikalPoreskaStopaID = _vezaArtikalDobavljac.Artikal.PoreskaStopa_ID,
                        //    ArtikalNaziv = _vezaArtikalDobavljac.Artikal.OpisTabela.Opis,
                        //    ArtikalBrojProizvodjaca = _vezaArtikalDobavljac.Artikal.BrojProizvodjaca,
                        //    ArtikalProizvodjacNaziv = _vezaArtikalDobavljac.Artikal.Proizvodjac.Naziv,
                        //    ArtikalProizvodjacID = _vezaArtikalDobavljac.Artikal.Proizvodjac_ID,
                        //    Status = 'I',
                        //    VremePromene = DateTime.Now,
                        //    KorisnickiNalog = App.Radnik.Nadimak
                        //};

                        //if (stavkaArtikalDetaljno.comboBoxNosilacGrupe.SelectedItem == null)
                        //{
                        //    _stavkaArtikal.NosilacGrupeID = ((DB.StavkaUsluga)stavkaArtikalDetaljno.ponudaDetaljno.listViewStavkaUsluga.SelectedItem).Usluga.NosilacGrupeID;
                        //}
                        //else
                        //{
                        //    _stavkaArtikal.NosilacGrupeID = ((DB.NosilacGrupe)stavkaArtikalDetaljno.comboBoxNosilacGrupe.SelectedItem).NosilacGrupeID;
                        //}

                        //if (_vezaArtikalDobavljac.PoslovniPartnerID != null)
                        //{
                        //    _stavkaArtikal.PoslovniPartnerID = _vezaArtikalDobavljac.PoslovniPartnerID;
                        //}
                        //else if (_vezaArtikalDobavljac.KorisnikProgramaID != null)
                        //{
                        //    _stavkaArtikal.KorisnikProgramaID = _vezaArtikalDobavljac.KorisnikProgramaID;
                        //}

                        //dBProksi.UnesiStavkaArtikal(_stavkaArtikal);

                        //DB.StavkaUsluga _stavkaUsluga = (DB.StavkaUsluga)stavkaArtikalDetaljno.ponudaDetaljno.listViewStavkaUsluga.SelectedItem;
                        //_stavkaUsluga.StavkaArtikals.Add(_stavkaArtikal);
                        //stavkaArtikalDetaljno.ponudaDetaljno.listViewStavkaArtikal.SelectedItem = _stavkaArtikal;
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
    }
}
