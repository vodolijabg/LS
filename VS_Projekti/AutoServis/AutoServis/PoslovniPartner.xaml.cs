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
    /// Interaction logic for PoslovniPartner.xaml
    /// </summary>
    public partial class PoslovniPartner : PageFunction<object>//Page
    {
        private App.Stanje StanjeTrenutno = App.Stanje.Osnovno;
        private string SifraTrenutna = "";

        Baza.LavAutoDataContext LavAutoDataContext = null;
        Baza.PoslovniPartner PrikaziPoslovniPartner = null;

        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        public PoslovniPartner()
        {
            InitializeComponent();
        }

        public PoslovniPartner(bool prikaziOdaberi):this()
        {
            buttonOdaberi.Visibility = Visibility.Visible;
        }

        public PoslovniPartner(bool prikaziOdaberi, Baza.PoslovniPartner poslovniPartner): this()
        {
            buttonOdaberi.Visibility = Visibility.Visible;
            this.PrikaziPoslovniPartner = poslovniPartner;
        }

        private void DajListuMesto()
        {
            IQueryable<Baza.Mesto> _upit = (from p in LavAutoDataContext.Mestos
                                            select p).OrderBy(w => w.Naziv); ;
            try
            {
                ObservableCollection<Baza.Mesto> _lista = new ObservableCollection<Baza.Mesto>(_upit.ToList());

                if (!_lista.Count.Equals(0))
                {
                    _lista.Insert(0, new Baza.Mesto());
                }

                comboBoxMestoLista.ItemsSource = _lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju mesta", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void UStanje(App.Stanje stanje)
        {
            StanjeTrenutno = stanje;

            textBoxID.IsEnabled = false;
            textBoxSifra.IsEnabled = !AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruPoslovniPartner && (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxSkracenNaziv.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxPunNaziv.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxPIB.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxMaticniBroj.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxZiroRacun.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            comboBoxMestoLista.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxAdresa.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxKontaktOsoba1.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxTelefon1.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxEMail1.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxKontaktOsoba2.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxTelefon2.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxEMail2.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxFaks.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            buttonUnesi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdi.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);
            buttonOdustani.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);
            buttonMesto.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);
            
            buttonNadji.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);

            buttonOdaberi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);

            listBoxLista.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            if (stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena))
            {
                if (!AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruPoslovniPartner)
                {
                    textBoxSifra.Focus();
                }
                else
                {
                    textBoxSkracenNaziv.Focus();
                }
            }
            if (stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno))
            {
                buttonUnesi.Focus();
            }
        }

        private void IsprazniDetalje()
        {
            textBoxID.Text = "";
            textBoxSifra.Text = "";
            textBoxSkracenNaziv.Text = "";
            textBoxPunNaziv.Text = "";
            textBoxPIB.Text = "";
            textBoxMaticniBroj.Text = "";
            textBoxZiroRacun.Text = "";
            comboBoxMestoLista.SelectedItem = null;
            textBoxAdresa.Text = "";
            textBoxKontaktOsoba1.Text = "";
            textBoxTelefon1.Text = "";
            textBoxEMail1.Text = "";
            textBoxKontaktOsoba2.Text = "";
            textBoxTelefon2.Text = "";
            textBoxEMail2.Text = "";
            textBoxFaks.Text = "";

        }

        private void UpdateBindingSource()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateSource();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateSource();
            BindingExpression _bindingExpressionSkracenNaziv = textBoxSkracenNaziv.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSkracenNaziv.UpdateSource();
            BindingExpression _bindingExpressionPunNaziv = textBoxPunNaziv.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPunNaziv.UpdateSource();
            BindingExpression _bindingExpressionPIB = textBoxPIB.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPIB.UpdateSource();
            BindingExpression _bindingExpressionMaticniBroj = textBoxMaticniBroj.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionMaticniBroj.UpdateSource();
            BindingExpression _bindingExpressionZiroRacun = textBoxZiroRacun.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionZiroRacun.UpdateSource();
            //mesto
            BindingExpression _bindingExpressionAdresa = textBoxAdresa.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionAdresa.UpdateSource();
            BindingExpression _bindingExpressionKontaktOsoba1 = textBoxKontaktOsoba1.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionKontaktOsoba1.UpdateSource();
            BindingExpression _bindingExpressionTelefon1 = textBoxTelefon1.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionTelefon1.UpdateSource();
            BindingExpression _bindingExpressionEMail1 = textBoxEMail1.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionEMail1.UpdateSource();
            BindingExpression _bindingExpressionKontaktOsoba2 = textBoxKontaktOsoba2.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionKontaktOsoba2.UpdateSource();
            BindingExpression _bindingExpressionTelefon2 = textBoxTelefon2.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionTelefon2.UpdateSource();
            BindingExpression _bindingExpressionEMail2 = textBoxEMail2.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionEMail2.UpdateSource();
            BindingExpression _bindingExpressionFaks = textBoxFaks.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionFaks.UpdateSource();


        }

        private void UpdateBindingTarget()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateTarget();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateTarget();
            BindingExpression _bindingExpressionSkracenNaziv = textBoxSkracenNaziv.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSkracenNaziv.UpdateTarget();
            BindingExpression _bindingExpressionPunNaziv = textBoxPunNaziv.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPunNaziv.UpdateTarget();
            BindingExpression _bindingExpressionPIB = textBoxPIB.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPIB.UpdateTarget();
            BindingExpression _bindingExpressionMaticniBroj = textBoxMaticniBroj.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionMaticniBroj.UpdateTarget();
            BindingExpression _bindingExpressionZiroRacun = textBoxZiroRacun.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionZiroRacun.UpdateTarget();
            //mesto
            BindingExpression _bindingExpressionAdresa = textBoxAdresa.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionAdresa.UpdateTarget();
            BindingExpression _bindingExpressionKontaktOsoba1 = textBoxKontaktOsoba1.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionKontaktOsoba1.UpdateTarget();
            BindingExpression _bindingExpressionTelefon1 = textBoxTelefon1.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionTelefon1.UpdateTarget();
            BindingExpression _bindingExpressionEMail1 = textBoxEMail1.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionEMail1.UpdateTarget();
            BindingExpression _bindingExpressionKontaktOsoba2 = textBoxKontaktOsoba2.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionKontaktOsoba2.UpdateTarget();
            BindingExpression _bindingExpressionTelefon2 = textBoxTelefon2.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionTelefon2.UpdateTarget();
            BindingExpression _bindingExpressionEMail2 = textBoxEMail2.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionEMail2.UpdateTarget();
            BindingExpression _bindingExpressionFaks = textBoxFaks.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionFaks.UpdateTarget();

            //TODO - UpdateBindingTarget listBoxLista
            listBoxLista.Items.Refresh();
            comboBoxMestoLista.Items.Refresh();


        }

        private bool DefinisanKriterijumPretrage()
        {
            if (!(bool)radioButtonPretragaPrikaziSve.IsChecked && textBoxPretragaSkracenNaziv.Text.Trim().Length.Equals(0) && textBoxPretragaPIB.Text.Trim().Length.Equals(0))
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
                DajListuMesto();

                IQueryable<Baza.PoslovniPartner> _upit;
                IQueryable<Baza.PoslovniPartner> _trenutni;
                IQueryable<Baza.PoslovniPartner> _vrati;

                #region Upit
                //ako je definisan kriterijum pretrage
                if (DefinisanKriterijumPretrage())
                {
                    if ((bool)radioButtonPretragaSkracenNaziv.IsChecked)
                    {
                        _upit = (from p in LavAutoDataContext.PoslovniPartners
                                 where p.SkracenNaziv.Contains(textBoxPretragaSkracenNaziv.Text)
                                 select p).OrderBy(w => w.SkracenNaziv);
                    }
                    else if ((bool)radioButtonPretragaPIB.IsChecked)
                    {
                        _upit = (from p in LavAutoDataContext.PoslovniPartners
                                 where p.PIB.ToString().Contains(textBoxPretragaPIB.Text)
                                 select p).OrderBy(w => w.SkracenNaziv);
                    }
                    else
                    {
                        _upit = (from p in LavAutoDataContext.PoslovniPartners
                                 select p).OrderBy(w => w.SkracenNaziv);
                    } 
                }
                //ako nije definisan kriterijum pretrage prikazi samo trenutne
                else
                {
                    List<int> _trenutnoPrikazani = new List<int>();

                    foreach (var item in listBoxLista.Items)
                    {
                        Baza.PoslovniPartner _item = (Baza.PoslovniPartner)item;

                        _trenutnoPrikazani.Add(_item.PoslovniPartner_ID);
                    }

                    _upit = (from p in LavAutoDataContext.PoslovniPartners
                             where _trenutnoPrikazani.Contains(p.PoslovniPartner_ID)
                             select p).OrderBy(w => w.SkracenNaziv);
                }

                if (StanjeTrenutno.Equals(App.Stanje.Unos) || StanjeTrenutno.Equals(App.Stanje.Izmena))
                {
                    _trenutni = (from p in LavAutoDataContext.PoslovniPartners
                                 where p.Sifra == textBoxSifra.Text
                                 select p).OrderBy(w => w.SkracenNaziv);

                    _vrati = _trenutni.Union(_upit).OrderBy(w => w.SkracenNaziv);

                }
                else
                {
                    _vrati = _upit;
                } 
                #endregion

                try
                {
                    ObservableCollection<Baza.PoslovniPartner> _lista = new ObservableCollection<Baza.PoslovniPartner>(_vrati.ToList());

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
                    Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)gridDetaljno.DataContext;
                    SifraTrenutna = _trenutni.Sifra;
                }

                DajSve();
                PrikaziTrenutni();
            }
        }

        private void PrikaziTrenutni()
        {
            SifraTrenutna.Trim();
            if (SifraTrenutna != "")
            {
                ObservableCollection<Baza.PoslovniPartner> _lista = (ObservableCollection<Baza.PoslovniPartner>)listBoxLista.ItemsSource;
                bool _postoji = false;

                if (!listBoxLista.Items.Count.Equals(0))
                {
                    foreach (Baza.PoslovniPartner item in _lista)
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

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (PrvoOtvaranjeStrane)
            {
                PrvoOtvaranjeStrane = false;

                LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

                if (LavAutoDataContext.DatabaseExists())
                {
                    DajListuMesto();
                }
                else
                {
                    MessageBox.Show("Neuspešna konekcija na bazu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (PrikaziPoslovniPartner == null)
                {
                    ObservableCollection<Baza.PoslovniPartner> _lista = new ObservableCollection<Baza.PoslovniPartner>();
                    listBoxLista.ItemsSource = _lista;
                }
                else
                {
                    IQueryable<Baza.PoslovniPartner> _upit = (from p in LavAutoDataContext.PoslovniPartners
                                                              where p.PoslovniPartner_ID == PrikaziPoslovniPartner.PoslovniPartner_ID
                                                              select p).Take(1);

                    try
                    {
                        ObservableCollection<Baza.PoslovniPartner> _lista = new ObservableCollection<Baza.PoslovniPartner>(_upit.ToList());
                        listBoxLista.ItemsSource = _lista;

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška pri čitanju poslovnog partnera", MessageBoxButton.OK, MessageBoxImage.Error);
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
            }
            else if ((StanjeTrenutno != App.Stanje.Izmena) && (StanjeTrenutno != App.Stanje.Unos))
            {
                if (!listBoxLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)gridDetaljno.DataContext;
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
                Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }

            ObservableCollection<Baza.PoslovniPartner> _lista = (ObservableCollection<Baza.PoslovniPartner>)listBoxLista.ItemsSource;

            Baza.PoslovniPartner _novi = new Baza.PoslovniPartner();

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
                LavAutoDataContext.PoslovniPartners.DeleteOnSubmit((Baza.PoslovniPartner)gridDetaljno.DataContext);

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
            if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruPoslovniPartner)
            {
                try
                {
                    LavAutoDataContext.ResetujBrojac("PoslovniPartner", "PoslovniPartner_ID");
                    textBoxSifra.Text = LavAutoDataContext.DajSledeciIdentity("PoslovniPartner").ToString();
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
                    LavAutoDataContext.ResetujBrojac("PoslovniPartner", "PoslovniPartner_ID");
                }
                catch (Exception) { }
            }
            #endregion

            UpdateBindingSource();

            if (App.ImaLiGreskiValidacijeTextBox(gridDetaljno))
            {
                //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruPoslovniPartner)
                {
                    textBoxSifra.Text = "";
                }
                return;
            }

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                LavAutoDataContext.PoslovniPartners.InsertOnSubmit((Baza.PoslovniPartner)gridDetaljno.DataContext);
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }
            catch (Exception ex)
            {
                if (StanjeTrenutno.Equals(App.Stanje.Unos))
                {
                    //TODO .... Nisam nasao drugi nacin da izbacim objekat iz kolekcije Inserts nego da ga dodam i u kolekciju Deletes (ChangeSet changeSet = LavAutoDataContext.GetChangeSet();. Kako uopste moze da se otkaze unos, izmena ili brisanje
                    //LavAutoDataContext.PoslovniPartners.DeleteOnSubmit((Baza.PoslovniPartner)gridDetaljno.DataContext);

                    //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                    if (AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruPoslovniPartner)
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
            ObservableCollection<Baza.PoslovniPartner> _lista = (ObservableCollection<Baza.PoslovniPartner>)listBoxLista.ItemsSource;

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.PoslovniPartner)gridDetaljno.DataContext);
            }
            else if (StanjeTrenutno.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)gridDetaljno.DataContext;
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
                Baza.PoslovniPartner _trenutni = (Baza.PoslovniPartner)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }
            DajSve();
            PrikaziTrenutni();
        }

        private void buttonZatvori_Click(object sender, RoutedEventArgs e)
        {

        }

        private void buttonMesto_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Mesto());
        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton _trenutni = (RadioButton)sender;

            if (_trenutni.Name == radioButtonPretragaSkracenNaziv.Name)
            {
                //if ((bool)_trenutni.IsChecked)
                //{
                    textBoxPretragaSkracenNaziv.IsEnabled = true;

                    textBoxPretragaPIB.IsEnabled = false;
                    textBoxPretragaPIB.Text = "";
                //}
                //else
                //{
                //    textBoxPretragaSkracenNaziv.IsEnabled = false;
                //    textBoxPretragaSkracenNaziv.Text = "";

                //    textBoxPretragaPIB.IsEnabled = true;
                //}
            }
            else if (_trenutni.Name == radioButtonPretragaPIB.Name)
            {
                //if ((bool)_trenutni.IsChecked)
                //{
                    textBoxPretragaSkracenNaziv.IsEnabled = false;
                    textBoxPretragaSkracenNaziv.Text = "";

                    textBoxPretragaPIB.IsEnabled = true;
                //}
                //else
                //{
                //    textBoxPretragaSkracenNaziv.IsEnabled = true;

                //    textBoxPretragaPIB.IsEnabled = false;
                //    textBoxPretragaPIB.Text = "";

                //}

            }
            else if (_trenutni.Name == radioButtonPretragaPrikaziSve.Name)
            {
                //if ((bool)_trenutni.IsChecked)
                //{
                    textBoxPretragaSkracenNaziv.IsEnabled = false;
                    textBoxPretragaSkracenNaziv.Text = "";

                    textBoxPretragaPIB.IsEnabled = false;
                    textBoxPretragaPIB.Text = "";

                //}
                //else
                //{
                //    textBoxPretragaSkracenNaziv.IsEnabled = false;
                //    textBoxPretragaSkracenNaziv.Text = "";

                //    textBoxPretragaPIB.IsEnabled = false;
                //    textBoxPretragaPIB.Text = "";

                //}

            }
        }

        private void buttonOdaberi_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(new ReturnEventArgs<Object>(listBoxLista.SelectedItem));
        }
    }
}
