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

namespace Servis
{
    /// <summary>
    /// Interaction logic for Vozila.xaml
    /// </summary>
    public partial class Vozilo : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        Servis.ServisnaKnjizicaDetaljno servisnaKnjizicaDetaljno;
        Servis.PonudaWizard2 ponudaWizard2;
        FizickoLiceServisnaKnjizicaDetaljno fizickoLiceServisnaKnjizicaDetaljno;

        public Vozilo()
        {
            InitializeComponent();
        }

        public Vozilo(Servis.ServisnaKnjizicaDetaljno servisnaKnjizicaDetaljno): this()
        {
            this.servisnaKnjizicaDetaljno = servisnaKnjizicaDetaljno;
        }

        public Vozilo(Servis.FizickoLiceServisnaKnjizicaDetaljno fizickoLiceServisnaKnjizicaDetaljno)
            : this()
        {
            this.fizickoLiceServisnaKnjizicaDetaljno = fizickoLiceServisnaKnjizicaDetaljno;
        }

        public Vozilo(Servis.PonudaWizard2 ponudaWizard2)
            : this()
        {
            this.ponudaWizard2 = ponudaWizard2;
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                prvoOtvaranjeStrane = false;

                try
                {
                    comboBoxProizvodjacAutomobila.ItemsSource = dBProksi.DajSveProizvodjac();

                    if (comboBoxProizvodjacAutomobila.Items.Count>0)
                    {
                        comboBoxProizvodjacAutomobila.SelectedIndex = 0; 
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                //ako sam usao sa servisne knjizice i ako je vec odabrano vozilo
                if (servisnaKnjizicaDetaljno != null && servisnaKnjizicaDetaljno.textBoxTip.Text.Trim() != "")
                {
                    DB.TipAutomobila _tipAutomobila = dBProksi.DajTipAutomobila(Convert.ToInt32(servisnaKnjizicaDetaljno.textBoxTip.Tag));
                    try
                    {
                        foreach (DB.Proizvodjac item in comboBoxProizvodjacAutomobila.Items)
                        {
                            if (item.Proizvodjac_ID.Equals(_tipAutomobila.ModelAutomobila.Proizvodjac_ID))
                            {
                                comboBoxProizvodjacAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                        foreach (DB.ModelAutomobila item in comboBoxModelAutomobila.Items)
                        {
                            if (item.ModelAutomobila_ID.Equals(_tipAutomobila.ModelAutomobila_ID))
                            {
                                comboBoxModelAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                        foreach (DB.TipAutomobila item in listViewTipAutomobila.Items)
                        {
                            if (item.TipAutomobila_ID.Equals(_tipAutomobila.TipAutomobila_ID))
                            {
                                listViewTipAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška pri čitanju tipa automobila", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

                if (ponudaWizard2 != null && ponudaWizard2.textBoxTip.Text.Trim() != "")
                {
                    DB.TipAutomobila _tipAutomobila = dBProksi.DajTipAutomobila(Convert.ToInt32(ponudaWizard2.textBoxTip.Tag));
                    try
                    {
                        foreach (DB.Proizvodjac item in comboBoxProizvodjacAutomobila.Items)
                        {
                            if (item.Proizvodjac_ID.Equals(_tipAutomobila.ModelAutomobila.Proizvodjac_ID))
                            {
                                comboBoxProizvodjacAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                        foreach (DB.ModelAutomobila item in comboBoxModelAutomobila.Items)
                        {
                            if (item.ModelAutomobila_ID.Equals(_tipAutomobila.ModelAutomobila_ID))
                            {
                                comboBoxModelAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                        foreach (DB.TipAutomobila item in listViewTipAutomobila.Items)
                        {
                            if (item.TipAutomobila_ID.Equals(_tipAutomobila.TipAutomobila_ID))
                            {
                                listViewTipAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška pri čitanju tipa automobila", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }

                if (fizickoLiceServisnaKnjizicaDetaljno != null && fizickoLiceServisnaKnjizicaDetaljno.textBoxTipSK.Text.Trim() != "")
                {
                    DB.TipAutomobila _tipAutomobila = dBProksi.DajTipAutomobila(Convert.ToInt32(fizickoLiceServisnaKnjizicaDetaljno.textBoxTipSK.Tag));
                    try
                    {
                        foreach (DB.Proizvodjac item in comboBoxProizvodjacAutomobila.Items)
                        {
                            if (item.Proizvodjac_ID.Equals(_tipAutomobila.ModelAutomobila.Proizvodjac_ID))
                            {
                                comboBoxProizvodjacAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                        foreach (DB.ModelAutomobila item in comboBoxModelAutomobila.Items)
                        {
                            if (item.ModelAutomobila_ID.Equals(_tipAutomobila.ModelAutomobila_ID))
                            {
                                comboBoxModelAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                        foreach (DB.TipAutomobila item in listViewTipAutomobila.Items)
                        {
                            if (item.TipAutomobila_ID.Equals(_tipAutomobila.TipAutomobila_ID))
                            {
                                listViewTipAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška pri čitanju tipa automobila", MessageBoxButton.OK, MessageBoxImage.Error);
                    }

                }


            }

            Window.GetWindow(this).Title = "Lav - Vozila";
        }

        private bool otvoriPadajucuListu = false;
        private int prolaz = 0;
        private void comboBoxProizvodjacAutomobila_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxProizvodjacAutomobila.SelectedItem != null)
            {
                DB.Proizvodjac _trenutniProizvodjac = (DB.Proizvodjac)comboBoxProizvodjacAutomobila.SelectedItem;

                try
                {
                    comboBoxModelAutomobila.ItemsSource = dBProksi.DajModelAutomobilaZaProizvodjac(_trenutniProizvodjac.Proizvodjac_ID);

                    if (comboBoxModelAutomobila.Items.Count > 0)
                    {
                        comboBoxModelAutomobila.SelectedIndex = 0;
                    }

                    if (!otvoriPadajucuListu)
                    {
                        if (servisnaKnjizicaDetaljno != null && prolaz < 1)
                        {
                            prolaz++;
                        }
                        else
                        {
                            otvoriPadajucuListu = true;
                        }
                    }
                    else
                    {
                        comboBoxModelAutomobila.IsDropDownOpen = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void comboBoxModelAutomobila_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxModelAutomobila.SelectedItem != null)
            {
                DB.ModelAutomobila _trenutniModelAutomobila = (DB.ModelAutomobila)comboBoxModelAutomobila.SelectedItem;

                try
                {
                    listViewTipAutomobila.ItemsSource = dBProksi.DajTipAutomobilaZaModel(_trenutniModelAutomobila.ModelAutomobila_ID);

                    if (listViewTipAutomobila.Items.Count > 0)
                    {
                        listViewTipAutomobila.SelectedIndex = 0;
                    }

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonVoziloDetaljno_Click(object sender, RoutedEventArgs e)
        {
            Button _buttonVoziloDetaljno = (Button)sender;
            DB.TipAutomobila _tipAutomobila = (DB.TipAutomobila)_buttonVoziloDetaljno.Tag;

            VoziloDetaljno _voziloDetaljno = new VoziloDetaljno(_tipAutomobila);
            //_voziloDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _voziloDetaljno.Owner = Window.GetWindow(this);
            _voziloDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _voziloDetaljno.ShowDialog();


        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (servisnaKnjizicaDetaljno != null)
            {
                DB.TipAutomobila _tipAutomobila = (DB.TipAutomobila)listViewTipAutomobila.SelectedItem;

                servisnaKnjizicaDetaljno.textBoxTip.Text = _tipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " +
                                                                    _tipAutomobila.ModelAutomobila.OpisTabela.Opis + " " +
                                                                        _tipAutomobila.OpisTabela.Opis;

                servisnaKnjizicaDetaljno.textBoxTip.Tag = _tipAutomobila.TipAutomobila_ID;

                Window.GetWindow(this).Close();
            }
            else if (ponudaWizard2 != null)
            {
                DB.TipAutomobila _tipAutomobila = (DB.TipAutomobila)listViewTipAutomobila.SelectedItem;

                ponudaWizard2.textBoxTip.Text = _tipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " +
                                                                    _tipAutomobila.ModelAutomobila.OpisTabela.Opis + " " +
                                                                        _tipAutomobila.OpisTabela.Opis;

                ponudaWizard2.textBoxTip.Tag = _tipAutomobila.TipAutomobila_ID;

                Window.GetWindow(this).Close();
            }
            else if (fizickoLiceServisnaKnjizicaDetaljno != null)
            {
                DB.TipAutomobila _tipAutomobila = (DB.TipAutomobila)listViewTipAutomobila.SelectedItem;

                fizickoLiceServisnaKnjizicaDetaljno.textBoxTipSK.Text = _tipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " " +
                                                                    _tipAutomobila.ModelAutomobila.OpisTabela.Opis + " " +
                                                                        _tipAutomobila.OpisTabela.Opis;

                fizickoLiceServisnaKnjizicaDetaljno.textBoxTipSK.Tag = _tipAutomobila.TipAutomobila_ID;

                Window.GetWindow(this).Close();
            }

        }

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            Int32 _tdBroj;
            //ako nema filtera 
            if (textBoxTDBroj.Text.Trim() == "")
            {
                Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi TD Broj.");
                //_dialog.WindowStyle = WindowStyle.ToolWindow;
                _dialog.Owner = Window.GetWindow(this);
                _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _dialog.ShowDialog();
                return;
            }
            else if (!Int32.TryParse(textBoxTDBroj.Text, out _tdBroj))
            {
                Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi broj za polje TD Broj.");
                //_dialog.WindowStyle = WindowStyle.ToolWindow;
                _dialog.Owner = Window.GetWindow(this);
                _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _dialog.ShowDialog();
                return ;
            }
            else
            {                
                try
                {
                    otvoriPadajucuListu = false;
                    textBoxTDBroj.Text = "";

                    DB.TipAutomobila _tipAutomobila = dBProksi.DajTipAutomobila(_tdBroj);

                    foreach (DB.Proizvodjac item in comboBoxProizvodjacAutomobila.Items)
                    {
                        if (item.Proizvodjac_ID.Equals(_tipAutomobila.ModelAutomobila.Proizvodjac_ID))
                        {
                            comboBoxProizvodjacAutomobila.SelectedItem = item;
                            break;
                        }
                    }

                    foreach (DB.ModelAutomobila item in comboBoxModelAutomobila.Items)
                    {
                        if (item.ModelAutomobila_ID.Equals(_tipAutomobila.ModelAutomobila_ID))
                        {
                            comboBoxModelAutomobila.SelectedItem = item;
                            break;
                        }
                    }

                    foreach (DB.TipAutomobila item in listViewTipAutomobila.Items)
                    {
                        if (item.TipAutomobila_ID.Equals(_tipAutomobila.TipAutomobila_ID))
                        {
                            listViewTipAutomobila.SelectedItem = item;
                            break;
                        }
                    }

                    

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju tipa automobila", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
    }
}
