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
using System.Data.Linq;

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for ServisnaKnjizica.xaml
    /// </summary>
    public partial class ServisnaKnjizica : PageFunction<Object>
    {
        private App.Stanje StanjeTrenutno = App.Stanje.Osnovno;
        private string SifraTrenutna = "";

        Baza.LavAutoDataContext LavAutoDataContext = null;
        Baza.ServisnaKnjizica PrikaziServisnuKnjizicu = null;


        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        public ServisnaKnjizica()
        {
            InitializeComponent();
        }

        public ServisnaKnjizica(bool odaberiServisnuKnjizicu): this()
        {
            buttonOdaberi.Visibility = Visibility.Visible;
        }

        public ServisnaKnjizica(bool odaberiServisnuKnjizicu, Baza.ServisnaKnjizica servisnaKnjizica): this()
        {
            this.PrikaziServisnuKnjizicu = servisnaKnjizica;

            buttonOdaberi.Visibility = Visibility.Visible;
        }

        private void UStanje(App.Stanje stanje)
        {
            StanjeTrenutno = stanje;

            textBoxID.IsEnabled = false;
            textBoxSifra.IsEnabled = !AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruServisnaKnjizica && (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            
            //comboBoxFizickoLiceLista.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxFizickoLice.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            //comboBoxPoslovniPartnerLista.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxPoslovniPartner.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            //comboBoxTipAutomobilaLista.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxTipAutomobila.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);


            textBoxBrojSasije.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxBrojMotora.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxGodiste.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxKilometraza.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxRegistarskiBroj.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxDatumRegistracije.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            
            checkBoxABS.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            checkBoxPS.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            checkBoxAC.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            
            textBoxNapomena.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            buttonUnesi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdi.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);
            buttonOdustani.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);

            buttonFizickoLice.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonPoslovniPartner.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonTipAutomobila.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            buttonTipAutomobila.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);

            buttonNadji.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            radioButtonFizickoLice.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            radioButtonPravnoLice.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            listBoxLista.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            if (stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena))
            {
                if (!AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruPoslovniPartner)
                {
                    textBoxSifra.Focus();
                }
                else
                {
                    
                }
            }
            if (stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno))
            {
                buttonUnesi.Focus();
            }
        }

        private void UpdateBindingSource()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateSource();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateSource();

            if ((bool)radioButtonFizickoLice.IsChecked)
            {
                //BindingExpression _bindingExpressionFizickoLice = comboBoxFizickoLiceLista.GetBindingExpression(ComboBox.SelectedItemProperty);
                //_bindingExpressionFizickoLice.UpdateSource();
                BindingExpression _bindingExpressionFizickoLice = textBoxFizickoLice.GetBindingExpression(TextBox.TagProperty);
                _bindingExpressionFizickoLice.UpdateSource();
            }

            if ((bool)radioButtonPravnoLice.IsChecked)
            {
                //BindingExpression _bindingExpressionPoslovniPartner = comboBoxPoslovniPartnerLista.GetBindingExpression(ComboBox.SelectedItemProperty);
                //_bindingExpressionPoslovniPartner.UpdateSource();
                BindingExpression _bindingExpressionPoslovniPartner = textBoxPoslovniPartner.GetBindingExpression(TextBox.TagProperty);
                _bindingExpressionPoslovniPartner.UpdateSource();

            }

            //BindingExpression _bindingExpressionTipAutomobila = comboBoxTipAutomobilaLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            //_bindingExpressionTipAutomobila.UpdateSource();
            BindingExpression _bindingExpressionTipAutomobila = textBoxTipAutomobila.GetBindingExpression(TextBox.TagProperty);
            _bindingExpressionTipAutomobila.UpdateSource();


            BindingExpression _bindingExpressionBrojSasije = textBoxBrojSasije.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionBrojSasije.UpdateSource();
            BindingExpression _bindingExpressionBrojMotora = textBoxBrojMotora.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionBrojMotora.UpdateSource();
            BindingExpression _bindingExpressionGodiste = textBoxGodiste.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionGodiste.UpdateSource();
            BindingExpression _bindingExpressionKilometraza = textBoxKilometraza.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionKilometraza.UpdateSource();
            BindingExpression _bindingExpressionRegistarskiBroj = textBoxRegistarskiBroj.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionRegistarskiBroj.UpdateSource();
            BindingExpression _bindingExpressionDatumRegistracije = textBoxDatumRegistracije.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionDatumRegistracije.UpdateSource();

            BindingExpression _bindingExpressionABS = checkBoxABS.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionABS.UpdateSource();
            BindingExpression _bindingExpressionPS = checkBoxPS.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionPS.UpdateSource();
            BindingExpression _bindingExpressionAC = checkBoxAC.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionAC.UpdateSource();

            BindingExpression _bindingExpressionNapomena = textBoxNapomena.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNapomena.UpdateSource();
        }

        private void UpdateBindingTarget1()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateTarget();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateTarget();


            //BindingExpression _bindingExpressionFizickoLice = comboBoxFizickoLiceLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            //_bindingExpressionFizickoLice.UpdateTarget();
            BindingExpression _bindingExpressionFizickoLice = textBoxFizickoLice.GetBindingExpression(TextBox.TagProperty);
            _bindingExpressionFizickoLice.UpdateTarget();

            //BindingExpression _bindingExpressionPoslovniPartner = comboBoxPoslovniPartnerLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            //_bindingExpressionPoslovniPartner.UpdateTarget();
            BindingExpression _bindingExpressionPoslovniPartner = textBoxPoslovniPartner.GetBindingExpression(TextBox.TagProperty);
            _bindingExpressionPoslovniPartner.UpdateTarget();



            //BindingExpression _bindingExpressionTipAutomobila = comboBoxTipAutomobilaLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            //_bindingExpressionTipAutomobila.UpdateTarget();
            BindingExpression _bindingExpressionTipAutomobila = textBoxTipAutomobila.GetBindingExpression(TextBox.TagProperty);
            _bindingExpressionTipAutomobila.UpdateTarget();

            BindingExpression _bindingExpressionBrojSasije = textBoxBrojSasije.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionBrojSasije.UpdateTarget();
            BindingExpression _bindingExpressionBrojMotora = textBoxBrojMotora.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionBrojMotora.UpdateTarget();
            BindingExpression _bindingExpressionGodiste = textBoxGodiste.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionGodiste.UpdateTarget();
            BindingExpression _bindingExpressionKilometraza = textBoxKilometraza.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionKilometraza.UpdateTarget();
            BindingExpression _bindingExpressionRegistarskiBroj = textBoxRegistarskiBroj.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionRegistarskiBroj.UpdateTarget();
            BindingExpression _bindingExpressionDatumRegistracije = textBoxDatumRegistracije.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionDatumRegistracije.UpdateTarget();

            BindingExpression _bindingExpressionABS = checkBoxABS.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionABS.UpdateTarget();
            BindingExpression _bindingExpressionPS = checkBoxPS.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionPS.UpdateTarget();
            BindingExpression _bindingExpressionAC = checkBoxAC.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionAC.UpdateTarget();

            BindingExpression _bindingExpressionNapomena = textBoxNapomena.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNapomena.UpdateTarget();
        }

        private void IsprazniDetalje()
        {
            textBoxID.Text = "";
            textBoxSifra.Text = "";
            //comboBoxFizickoLiceLista.ItemsSource = null;
            textBoxFizickoLice.Tag = null;
            //comboBoxPoslovniPartnerLista.ItemsSource = null;
            textBoxPoslovniPartner.Tag = null;
            //comboBoxTipAutomobilaLista.ItemsSource = null;
            textBoxTipAutomobila.Tag = null;

            textBoxBrojSasije.Text = "";
            textBoxBrojMotora.Text = "";
            textBoxGodiste.Text = "";
            textBoxKilometraza.Text = "";
            textBoxRegistarskiBroj.Text = "";
            textBoxDatumRegistracije.Text = "";
            checkBoxABS.IsChecked = false;
            checkBoxPS.IsChecked = false;
            checkBoxAC.IsChecked = false;
            textBoxNapomena.Text = "";
        }

        private void PrikaziTrenutni()
        {
            if (SifraTrenutna != null)
            {
                SifraTrenutna.Trim();
                if (SifraTrenutna != "")
                {
                    ObservableCollection<Baza.ServisnaKnjizica> _lista = (ObservableCollection<Baza.ServisnaKnjizica>)listBoxLista.ItemsSource;
                    bool _postoji = false;

                    if (!listBoxLista.Items.Count.Equals(0))
                    {
                        foreach (Baza.ServisnaKnjizica item in _lista)
                        {
                            if (item.Sifra.Equals(SifraTrenutna))
                            {
                                listBoxLista.SelectedItem = item;
                                _postoji = true;
                                break;
                            }
                        }
                        if (!_postoji)
                        {
                            listBoxLista.SelectedIndex = 0;
                        }
                    }
                    SifraTrenutna = "";
                }
            }
        }

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (PrvoOtvaranjeStrane)
            {
                radioButtonFizickoLice.IsChecked = true;
                radioButton_Click(radioButtonFizickoLice, null);

                radioButtonPretragaSifra.IsChecked = true;
                radioButton_Click(radioButtonPretragaSifra, null);

                PrvoOtvaranjeStrane = false;

                LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);


                if (PrikaziServisnuKnjizicu == null)
                {
                    ObservableCollection<Baza.ServisnaKnjizica> _lista = new ObservableCollection<Baza.ServisnaKnjizica>();
                    listBoxLista.ItemsSource = _lista;
                }
                else
                {
                    IQueryable<Baza.ServisnaKnjizica> _upit = (from p in LavAutoDataContext.ServisnaKnjizicas
                                                               where p.ServisnaKnjizica_ID == PrikaziServisnuKnjizicu.ServisnaKnjizica_ID
                                                               select p).Take(1);

                    try
                    {
                        ObservableCollection<Baza.ServisnaKnjizica> _lista = new ObservableCollection<Baza.ServisnaKnjizica>(_upit.ToList());
                        listBoxLista.ItemsSource = _lista;
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška pri čitanju fizičkog lica", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }


                if (!listBoxLista.Items.Count.Equals(0))
                {
                    listBoxLista.SelectedIndex = 0;
                    UStanje(App.Stanje.Detaljno);
                }
                else
                {
                    UStanje(App.Stanje.Osnovno);
                }

                textBoxPretragaSifra.Focus();

            }
            else if ((StanjeTrenutno != App.Stanje.Izmena) && (StanjeTrenutno != App.Stanje.Unos))
            {
                if (!listBoxLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.ServisnaKnjizica _trenutni = (Baza.ServisnaKnjizica)gridDetaljno.DataContext;
                    SifraTrenutna = _trenutni.Sifra;

                }
                DajSve();
                PrikaziTrenutni();
            }
        }

        private void buttonUnesi_Click(object sender, RoutedEventArgs e)
        {
            if (!listBoxLista.Items.Count.Equals(0))
            {
                //da znam odakle sam posao
                Baza.ServisnaKnjizica _trenutni = (Baza.ServisnaKnjizica)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }

            ObservableCollection<Baza.ServisnaKnjizica> _lista = (ObservableCollection<Baza.ServisnaKnjizica>)listBoxLista.ItemsSource;

            Baza.ServisnaKnjizica _novi = new Baza.ServisnaKnjizica();

            _lista.Add(_novi);

            listBoxLista.SelectedItem = _novi;

            IsprazniDetalje();

            UStanje(App.Stanje.Unos);
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            UStanje(App.Stanje.Izmena);
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                LavAutoDataContext.ServisnaKnjizicas.DeleteOnSubmit((Baza.ServisnaKnjizica)gridDetaljno.DataContext);

                try
                {
                    LavAutoDataContext.SubmitChanges();

                }
                catch (Exception ex)
                {
                    //TODO ovde imam problem
                    //ako pukne imam slog u nizu Deleted
                    //da ispraznim (ChangeSet changeSet = LavAutoDataContext.GetChangeSet();. Kako uopste moze da se otkaze unos, izmena ili brisanje
                    LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

                    MessageBox.Show(ex.Message, "Greška pri brisanju", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
                DajSve();
            }
        }

        private void buttonPotvrdi_Click(object sender, RoutedEventArgs e)
        {
            #region GenerisiSifru
            //Ako je stanje unos a i automatsko je dodeljivanje sifre
            if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruServisnaKnjizica)
            {
                try
                {
                    LavAutoDataContext.ResetujBrojac("ServisnaKnjizica", "ServisnaKnjizica_ID");
                    textBoxSifra.Text = LavAutoDataContext.DajSledeciIdentity("ServisnaKnjizica").ToString();
                }
                catch (Exception ex)
                {
                    textBoxSifra.Text = "";
                    MessageBox.Show(ex.Message, "Greška pri generisanju šifre", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }
            }
            else
            {
                try
                {
                    LavAutoDataContext.ResetujBrojac("ServisnaKnjizica", "ServisnaKnjizica_ID");
                }
                catch (Exception) { }
            }
            #endregion

            UpdateBindingSource();

            //da prodje kroz sve
            bool _imaLiGreskiValidacije = false;

            if (App.ImaLiGreskiValidacijeTextBox(gridDetaljno))
            {
                _imaLiGreskiValidacije = true;
            }
            if (gridFizickoLice.IsVisible && App.ImaLiGreskiValidacijeTextBox(gridFizickoLice))
            {
                _imaLiGreskiValidacije = true;
            }
            if (gridPoslovniPartner.IsVisible && App.ImaLiGreskiValidacijeTextBox(gridPoslovniPartner))
            {
                _imaLiGreskiValidacije = true;
            }
            if (App.ImaLiGreskiValidacijeTextBox(gridTipAutomobila))
            {
                _imaLiGreskiValidacije = true;
            }

            if (_imaLiGreskiValidacije)
            {
                //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruServisnaKnjizica)
                {
                    textBoxSifra.Text = "";
                }
                return;
            }

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                LavAutoDataContext.ServisnaKnjizicas.InsertOnSubmit((Baza.ServisnaKnjizica)gridDetaljno.DataContext);
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                Baza.ServisnaKnjizica _trenutni = (Baza.ServisnaKnjizica)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }
            catch (Exception ex)
            {
                if (StanjeTrenutno.Equals(App.Stanje.Unos))
                {
                    //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                    if (AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruServisnaKnjizica)
                    {
                        textBoxSifra.Text = "";
                    }
                }

                MessageBox.Show(ex.Message, "Greška pri upisu podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DajSve();
            PrikaziTrenutni();
        }

        private void buttonOdustani_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Baza.ServisnaKnjizica> _lista = (ObservableCollection<Baza.ServisnaKnjizica>)listBoxLista.ItemsSource;

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.ServisnaKnjizica)gridDetaljno.DataContext);
            }
            else if (StanjeTrenutno.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.ServisnaKnjizica _trenutni = (Baza.ServisnaKnjizica)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }

            DajSve();
            PrikaziTrenutni();
        }

        private void buttonOsvezi_Click(object sender, RoutedEventArgs e)
        {
            if (!listBoxLista.Items.Count.Equals(0))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.ServisnaKnjizica _trenutni = (Baza.ServisnaKnjizica)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;

            }
            DajSve();
            PrikaziTrenutni();
        }

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            if (!DefinisanKriterijumPretrage())
            {
                MessageBox.Show("Unesi kriterijum za pretragu", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (!listBoxLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.ServisnaKnjizica _trenutni = (Baza.ServisnaKnjizica)gridDetaljno.DataContext;
                    SifraTrenutna = _trenutni.Sifra;

                }

                DajSve();
                PrikaziTrenutni();
            }

        }


        private bool DefinisanKriterijumPretrage()
        {
            if (textBoxFizickoLicePretraga.Tag == null && textBoxPoslovniPartnerPretraga.Tag == null && textBoxPretragaSifra.Text.Trim().Length.Equals(0))
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        private void DajSve()
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);


            if (LavAutoDataContext.DatabaseExists())
            {
                IQueryable<Baza.ServisnaKnjizica> _upit;
                IQueryable<Baza.ServisnaKnjizica> _trenutni;
                IQueryable<Baza.ServisnaKnjizica> _vrati;

                #region Upit

                if((bool)radioButtonFizickoLice.IsChecked)
                {
                    //ako je definisan kriterijum pretrage
                    if (DefinisanKriterijumPretrage())
                    {
                        if ((bool)radioButtonPretragaSifra.IsChecked)
                        {
                            _upit = (from p in LavAutoDataContext.ServisnaKnjizicas
                                     where p.Sifra.Contains(textBoxPretragaSifra.Text)
                                     && p.FizickoLice != null
                                     select p).OrderBy(w => w.Sifra);
                        }
                        else
                        {
                            Baza.FizickoLice _uslov = (Baza.FizickoLice)textBoxFizickoLicePretraga.Tag;

                            _upit = (from p in LavAutoDataContext.ServisnaKnjizicas
                                     where p.FizickoLice_ID == _uslov.FizickoLice_ID
                                     select p).OrderBy(w => w.Sifra);
                        }
                    }
                    //ako nije definisan kriterijum pretrage prikazi samo trenutne
                    else
                    {
                        List<int> _trenutnoPrikazani = new List<int>();

                        foreach (var item in listBoxLista.Items)
                        {
                            Baza.ServisnaKnjizica _item = (Baza.ServisnaKnjizica)item;

                            _trenutnoPrikazani.Add(_item.ServisnaKnjizica_ID);
                        }

                        _upit = (from p in LavAutoDataContext.ServisnaKnjizicas
                                 where _trenutnoPrikazani.Contains(p.ServisnaKnjizica_ID)
                                 && p.FizickoLice != null
                                 select p).OrderBy(w => w.Sifra);
                    }
                }
                else //if ((bool)radioButtonPravnoLice.IsChecked)
                {
                    //ako je definisan kriterijum pretrage
                    if (DefinisanKriterijumPretrage())
                    {
                        if ((bool)radioButtonPretragaSifra.IsChecked)
                        {
                            _upit = (from p in LavAutoDataContext.ServisnaKnjizicas
                                     where p.Sifra.Contains(textBoxPretragaSifra.Text)
                                     && p.PoslovniPartner != null
                                     select p).OrderBy(w => w.Sifra);
                        }
                        else
                        {
                            Baza.PoslovniPartner _uslov = (Baza.PoslovniPartner)textBoxPoslovniPartnerPretraga.Tag;

                            _upit = (from p in LavAutoDataContext.ServisnaKnjizicas
                                     where p.PoslovniPartner_ID == _uslov.PoslovniPartner_ID
                                     select p).OrderBy(w => w.Sifra);
                        } 
                    }
                    //ako nije definisan kriterijum pretrage prikazi samo trenutne
                    else
                    {
                        List<int> _trenutnoPrikazani = new List<int>();

                        foreach (var item in listBoxLista.Items)
                        {
                            Baza.ServisnaKnjizica _item = (Baza.ServisnaKnjizica)item;

                            _trenutnoPrikazani.Add(_item.ServisnaKnjizica_ID);
                        }

                        _upit = (from p in LavAutoDataContext.ServisnaKnjizicas
                                 where _trenutnoPrikazani.Contains(p.ServisnaKnjizica_ID)
                                 && p.PoslovniPartner != null
                                 select p).OrderBy(w => w.Sifra);
                    }

                }

                //ako sam zavrsio unos ili izmenu prikazi ga bez obzira da li ispunjava kriterijum pretrage
                if (StanjeTrenutno.Equals(App.Stanje.Unos) || StanjeTrenutno.Equals(App.Stanje.Izmena))
                {
                    _trenutni = (from p in LavAutoDataContext.ServisnaKnjizicas
                                 where p.Sifra == textBoxSifra.Text
                                 select p).OrderBy(w => w.Sifra);


                    _vrati = _trenutni.Union(_upit).OrderBy(w => w.Sifra);

                }
                else
                {
                    _vrati = _upit;
                }
                #endregion

                try
                {
                    ObservableCollection<Baza.ServisnaKnjizica> _lista = new ObservableCollection<Baza.ServisnaKnjizica>(_vrati.ToList());

                    listBoxLista.ItemsSource = null;
                    listBoxLista.ItemsSource = _lista;
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

            if (!listBoxLista.Items.Count.Equals(0))
            {
                listBoxLista.SelectedIndex = 0;
                UStanje(App.Stanje.Detaljno);
            }
            else
            {
                UStanje(App.Stanje.Osnovno);
            }
        }


        private void buttonTipAutomobila_Click(object sender, RoutedEventArgs e)
        {
            //if (comboBoxTipAutomobilaLista.Items.Count.Equals(0))
            //{
            //    TipAutomobila _tipAutomobila = new TipAutomobila(true);
            //    _tipAutomobila.Return += new ReturnEventHandler<object>(tipAutomobila_Return);
            //    this.NavigationService.Navigate(_tipAutomobila);

            //}
            ////ako je vec odabran prikazi ga
            //else
            //{
            //    Baza.TipAutomobila _trenutni = (Baza.TipAutomobila)comboBoxTipAutomobilaLista.SelectedItem;

            //    TipAutomobila _tipAutomobila = new TipAutomobila(true, _trenutni);
            //    _tipAutomobila.Return += new ReturnEventHandler<object>(tipAutomobila_Return);
            //    this.NavigationService.Navigate(_tipAutomobila);
            //}
            if (textBoxTipAutomobila.Tag == null)
            {
                TipAutomobila _tipAutomobila = new TipAutomobila(true);
                _tipAutomobila.Return += new ReturnEventHandler<object>(tipAutomobila_Return);
                this.NavigationService.Navigate(_tipAutomobila);

            }
            //ako je vec odabran prikazi ga
            else
            {
                Baza.TipAutomobila _trenutni = (Baza.TipAutomobila)textBoxTipAutomobila.Tag;

                TipAutomobila _tipAutomobila = new TipAutomobila(true, _trenutni);
                _tipAutomobila.Return += new ReturnEventHandler<object>(tipAutomobila_Return);
                this.NavigationService.Navigate(_tipAutomobila);
            }
        }

        void tipAutomobila_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                Baza.TipAutomobila _tipAutomobila = (Baza.TipAutomobila)e.Result;

                IQueryable<Baza.TipAutomobila> _upit = (from p in LavAutoDataContext.TipAutomobilas
                                                        where p.TipAutomobila_ID == _tipAutomobila.TipAutomobila_ID
                                                        select p).Take(1);

                try
                {
                    ObservableCollection<Baza.TipAutomobila> _lista = new ObservableCollection<Baza.TipAutomobila>(_upit.ToArray());
                    //comboBoxTipAutomobilaLista.ItemsSource = _lista;
                    //comboBoxTipAutomobilaLista.SelectedItem = _upit.First();
                    textBoxTipAutomobila.Tag = _lista.First();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void buttonFizickoLice_Click(object sender, RoutedEventArgs e)
        {
            //if (comboBoxFizickoLiceLista.Items.Count.Equals(0))
            //{
            //    FizickoLice _fizickoLice = new FizickoLice(true);
            //    _fizickoLice.Return += new ReturnEventHandler<object>(_fizickoLice_Return);
            //    this.NavigationService.Navigate(_fizickoLice);

            //}
            ////ako je vec odabran prikazi ga
            //else
            //{
            //    Baza.FizickoLice _trenutni = (Baza.FizickoLice)comboBoxFizickoLiceLista.SelectedItem;

            //    FizickoLice _fizickoLice = new FizickoLice(true, _trenutni);
            //    _fizickoLice.Return += new ReturnEventHandler<object>(_fizickoLice_Return);
            //    this.NavigationService.Navigate(_fizickoLice);
            //}
            if (textBoxFizickoLice.Tag == null)
            {
                FizickoLice _fizickoLice = new FizickoLice(true);
                _fizickoLice.Return += new ReturnEventHandler<object>(_fizickoLice_Return);
                this.NavigationService.Navigate(_fizickoLice);

            }
            //ako je vec odabran prikazi ga
            else
            {
                Baza.FizickoLice _trenutni = (Baza.FizickoLice)textBoxFizickoLice.Tag;

                FizickoLice _fizickoLice = new FizickoLice(true, _trenutni);
                _fizickoLice.Return += new ReturnEventHandler<object>(_fizickoLice_Return);
                this.NavigationService.Navigate(_fizickoLice);
            }
        }

        void _fizickoLice_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                Baza.FizickoLice _fizickoLice = (Baza.FizickoLice)e.Result;

                IQueryable<Baza.FizickoLice> _upit = (from p in LavAutoDataContext.FizickoLices
                                                      where p.FizickoLice_ID == _fizickoLice.FizickoLice_ID
                                                      select p).Take(1);

                try
                {
                    ObservableCollection<Baza.FizickoLice> _lista = new ObservableCollection<Baza.FizickoLice>(_upit.ToArray());
                    //comboBoxFizickoLiceLista.ItemsSource = _lista;
                    //comboBoxFizickoLiceLista.SelectedItem = _upit.First();
                    textBoxFizickoLice.Tag = _upit.First();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void buttonPoslovniPartner_Click(object sender, RoutedEventArgs e)
        {
            //if (comboBoxPoslovniPartnerLista.Items.Count.Equals(0))
            //{
            //    PoslovniPartner _poslovniPartner = new PoslovniPartner(true);
            //    _poslovniPartner.Return += new ReturnEventHandler<object>(_poslovniPartner_Return);
            //    this.NavigationService.Navigate(_poslovniPartner);
            //}
            ////ako je vec odabran prikazi ga
            //else
            //{
            //    Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)comboBoxPoslovniPartnerLista.SelectedItem;

            //    PoslovniPartner _poslovniPartner = new PoslovniPartner(true, _trenutni);
            //    _poslovniPartner.Return += new ReturnEventHandler<object>(_poslovniPartner_Return);
            //    this.NavigationService.Navigate(_poslovniPartner);
            //}
            if (textBoxPoslovniPartner.Tag == null)
            {
                PoslovniPartner _poslovniPartner = new PoslovniPartner(true);
                _poslovniPartner.Return += new ReturnEventHandler<object>(_poslovniPartner_Return);
                this.NavigationService.Navigate(_poslovniPartner);
            }
            //ako je vec odabran prikazi ga
            else
            {
                Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)textBoxPoslovniPartner.Tag;

                PoslovniPartner _poslovniPartner = new PoslovniPartner(true, _trenutni);
                _poslovniPartner.Return += new ReturnEventHandler<object>(_poslovniPartner_Return);
                this.NavigationService.Navigate(_poslovniPartner);

            }
        }

        void _poslovniPartner_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                Baza.PoslovniPartner _poslovniPartner = (Baza.PoslovniPartner)e.Result;

                IQueryable<Baza.PoslovniPartner> _upit = (from p in LavAutoDataContext.PoslovniPartners
                                                          where p.PoslovniPartner_ID == _poslovniPartner.PoslovniPartner_ID
                                                          select p).Take(1);

                try
                {
                    ObservableCollection<Baza.PoslovniPartner> _lista = new ObservableCollection<Baza.PoslovniPartner>(_upit.ToArray());
                    //comboBoxPoslovniPartnerLista.ItemsSource = _lista;
                    //comboBoxPoslovniPartnerLista.SelectedItem = _upit.First();
                    //comboBoxPoslovniPartnerLista.Items.Refresh();
                    textBoxPoslovniPartner.Tag = _lista.First();



                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void buttonPretragaFizickoLice_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxFizickoLicePretraga.Tag==null)
            {
                FizickoLice _pretragaFizickoLice = new FizickoLice(true);
                _pretragaFizickoLice.Return += new ReturnEventHandler<object>(_pretragaFizickoLice_Return);
                this.NavigationService.Navigate(_pretragaFizickoLice);

            }
            //ako je vec odabran prikazi ga
            else
            {
                Baza.FizickoLice _trenutni = (Baza.FizickoLice)textBoxFizickoLicePretraga.Tag;

                FizickoLice _fizickoLice = new FizickoLice(true, _trenutni);
                _fizickoLice.Return += new ReturnEventHandler<object>(_pretragaFizickoLice_Return);
                this.NavigationService.Navigate(_fizickoLice);
            }
        }

        void _pretragaFizickoLice_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                Baza.FizickoLice _fizickoLice = (Baza.FizickoLice)e.Result;

                ObservableCollection<Baza.FizickoLice> _lista = new ObservableCollection<Baza.FizickoLice>();
                _lista.Add(_fizickoLice);

                //comboBoxPretragaFizickoLiceLista.ItemsSource = _lista;
                //comboBoxPretragaFizickoLiceLista.SelectedItem = _fizickoLice;
                textBoxFizickoLicePretraga.Tag = _lista.First();
            }
        }


        private void buttonPretragaPoslovniPartner_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxPoslovniPartnerPretraga.Tag==null)
            {
                PoslovniPartner _pretragaPoslovniPartner = new PoslovniPartner(true);
                _pretragaPoslovniPartner.Return += new ReturnEventHandler<object>(_pretragaPoslovniPartner_Return);
                this.NavigationService.Navigate(_pretragaPoslovniPartner);

            }
            //ako je vec odabran prikazi ga
            else
            {
                Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)textBoxPoslovniPartnerPretraga.Tag;

                PoslovniPartner _pretragaPoslovniPartner = new PoslovniPartner(true, _trenutni);
                _pretragaPoslovniPartner.Return += new ReturnEventHandler<object>(_pretragaPoslovniPartner_Return);
                this.NavigationService.Navigate(_pretragaPoslovniPartner);
            }

        }

        void _pretragaPoslovniPartner_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                Baza.PoslovniPartner _poslovniPartner = (Baza.PoslovniPartner)e.Result;

                ObservableCollection<Baza.PoslovniPartner> _lista = new ObservableCollection<Baza.PoslovniPartner>();
                _lista.Add(_poslovniPartner);

                textBoxPoslovniPartnerPretraga.Tag = _lista.First();
            }
        }


        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton _trenutni = (RadioButton)sender;

            if (_trenutni.Name == radioButtonPretragaPartner.Name)
            {
                //comboBoxPretragaFizickoLiceLista.IsEnabled = true;
                textBoxFizickoLicePretraga.IsEnabled = true;
                buttonPretragaFizickoLice.IsEnabled = true;

                textBoxPoslovniPartnerPretraga.IsEnabled = true;
                buttonPretragaPoslovniPartner.IsEnabled = true;

                textBoxPretragaSifra.Text = "";
                textBoxPretragaSifra.IsEnabled = false;
            }
            else if (_trenutni.Name == radioButtonPretragaSifra.Name)
            {
                //comboBoxPretragaFizickoLiceLista.IsEnabled = false;
                //comboBoxPretragaFizickoLiceLista.ItemsSource = null;
                textBoxFizickoLicePretraga.IsEnabled = false;
                textBoxFizickoLicePretraga.Tag = null;
                

                buttonPretragaFizickoLice.IsEnabled = false;

                textBoxPoslovniPartnerPretraga.IsEnabled = false;
                textBoxPoslovniPartnerPretraga.Tag = null;

                buttonPretragaPoslovniPartner.IsEnabled = false;

                textBoxPretragaSifra.IsEnabled = true;
            }
            else if (_trenutni.Name == radioButtonFizickoLice.Name)
            {
                gridPretragaPoslovniPartner.Visibility = Visibility.Collapsed;
                textBoxPoslovniPartnerPretraga.Tag = null;

                gridPretragaFizickoLice.Visibility = Visibility.Visible;
                //
                gridPoslovniPartner.Visibility = Visibility.Collapsed;
                //comboBoxPoslovniPartnerLista.ItemsSource = null;
                textBoxPoslovniPartner.Tag = null;

                gridFizickoLice.Visibility = Visibility.Visible;

                listBoxLista.ItemsSource = null;
                ObservableCollection<Baza.ServisnaKnjizica> _lista = new ObservableCollection<Baza.ServisnaKnjizica>();
                listBoxLista.ItemsSource = _lista;

                UStanje(App.Stanje.Osnovno);

            }
            else if (_trenutni.Name == radioButtonPravnoLice.Name)
            {
                gridPretragaPoslovniPartner.Visibility = Visibility.Visible;

                gridPretragaFizickoLice.Visibility = Visibility.Collapsed;
                //comboBoxPretragaFizickoLiceLista.ItemsSource = null;
                textBoxFizickoLicePretraga.Tag = null;

                //
                gridPoslovniPartner.Visibility = Visibility.Visible;

                gridFizickoLice.Visibility = Visibility.Collapsed;
                //comboBoxFizickoLiceLista.ItemsSource = null;
                textBoxFizickoLice.Tag = null;

                listBoxLista.ItemsSource = null;
                ObservableCollection<Baza.ServisnaKnjizica> _lista = new ObservableCollection<Baza.ServisnaKnjizica>();
                listBoxLista.ItemsSource = _lista;

                UStanje(App.Stanje.Osnovno);

            }
        }

        private void listBoxLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox _lista = (ListBox)sender;


            Baza.ServisnaKnjizica _trenutni = (Baza.ServisnaKnjizica)_lista.SelectedItem;

            if (_trenutni==null)
            {
                return;
            }


            if (_trenutni.FizickoLice != null)
            {
                ObservableCollection<Baza.FizickoLice> _listaFizickoLice = new ObservableCollection<Baza.FizickoLice>();
                _listaFizickoLice.Add(_trenutni.FizickoLice);
                //comboBoxFizickoLiceLista.ItemsSource = _listaFizickoLice;
                textBoxFizickoLice.Tag = _listaFizickoLice.First();

            }

            if (_trenutni.PoslovniPartner != null)
            {
                ObservableCollection<Baza.PoslovniPartner> _listaPoslovniPartner = new ObservableCollection<Baza.PoslovniPartner>();
                _listaPoslovniPartner.Add(_trenutni.PoslovniPartner);
                //comboBoxPoslovniPartnerLista.ItemsSource = _listaPoslovniPartner;
                textBoxPoslovniPartner.Tag = _listaPoslovniPartner.First();

            }

            if (_trenutni.TipAutomobila != null)
            {
                ObservableCollection<Baza.TipAutomobila> _listaTipAutomobila = new ObservableCollection<Baza.TipAutomobila>();
                _listaTipAutomobila.Add(_trenutni.TipAutomobila);
                //comboBoxTipAutomobilaLista.ItemsSource = _listaTipAutomobila;
                textBoxTipAutomobila.Tag = _listaTipAutomobila.First();
            }

        }

        private void buttonOdaberi_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(new ReturnEventArgs<Object>(listBoxLista.SelectedItem));
        }

        
    }
}
