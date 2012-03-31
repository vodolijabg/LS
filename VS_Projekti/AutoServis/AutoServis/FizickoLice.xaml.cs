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
using System.ComponentModel;

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for FizickoLice.xaml
    /// </summary>
    public partial class FizickoLice : PageFunction<object>//Page
    {
        private App.Stanje StanjeTrenutno = App.Stanje.Osnovno;
        private string SifraTrenutna = "";

        Baza.LavAutoDataContext LavAutoDataContext = null;
        Baza.FizickoLice PrikaziFizickoLice = null;

        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        public FizickoLice()
        {
            InitializeComponent();
        }

        public FizickoLice(bool prikaziOdaberi):this()
        {
            buttonOdaberi.Visibility = Visibility.Visible;
        }

        public FizickoLice(bool prikaziOdaberi, Baza.FizickoLice fizickoLice): this()
        {
            buttonOdaberi.Visibility = Visibility.Visible;
            this.PrikaziFizickoLice = fizickoLice;
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
            textBoxSifra.IsEnabled = !AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruFizickoLice && (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxIme.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxPrezime.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            checkBoxRegistrovanKupac.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            comboBoxMestoLista.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxAdresa.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxTelefon.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxEMail.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

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
                if (!AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruFizickoLice)
                {
                    textBoxSifra.Focus();
                }
                else
                {
                    textBoxIme.Focus();
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
            textBoxIme.Text = "";
            textBoxPrezime.Text = "";
            checkBoxRegistrovanKupac.IsChecked = false;
            comboBoxMestoLista.SelectedItem = null;
            textBoxAdresa.Text = "";
            textBoxTelefon.Text = "";
            textBoxEMail.Text = "";
        }

        private void UpdateBindingSource()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateSource();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateSource();
            BindingExpression _bindingExpressionIme = textBoxIme.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionIme.UpdateSource();
            BindingExpression _bindingExpressionPrezime = textBoxPrezime.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPrezime.UpdateSource();
            BindingExpression _bindingExpressionRegistrovanKupac = checkBoxRegistrovanKupac.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionRegistrovanKupac.UpdateSource();
            //mesto
            BindingExpression _bindingExpressionAdresa = textBoxAdresa.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionAdresa.UpdateSource();
            BindingExpression _bindingExpressionTelefon = textBoxTelefon.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionTelefon.UpdateSource();
            BindingExpression _bindingExpressionEMail = textBoxEMail.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionEMail.UpdateSource();
        }

        private void UpdateBindingTarget()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateTarget();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateTarget();
            BindingExpression _bindingExpressionIme = textBoxIme.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionIme.UpdateTarget();
            BindingExpression _bindingExpressionPrezime = textBoxPrezime.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPrezime.UpdateTarget();
            BindingExpression _bindingExpressionRegistrovanKupac = checkBoxRegistrovanKupac.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionRegistrovanKupac.UpdateTarget();
            //mesto
            BindingExpression _bindingExpressionAdresa = textBoxAdresa.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionAdresa.UpdateTarget();
            BindingExpression _bindingExpressionTelefon = textBoxTelefon.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionTelefon.UpdateTarget();
            BindingExpression _bindingExpressionEMail = textBoxEMail.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionEMail.UpdateTarget();

            //TODO - UpdateBindingTarget listBoxLista
            listBoxLista.Items.Refresh();
            comboBoxMestoLista.Items.Refresh();

        }

        private bool DefinisanKriterijumPretrage()
        {
            if (!(bool)radioButtonPretragaPrikaziSve.IsChecked && textBoxPretragaIme.Text.Trim().Length.Equals(0) && textBoxPretragaTelefon.Text.Trim().Length.Equals(0))
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

                IQueryable<Baza.FizickoLice> _upit;
                IQueryable<Baza.FizickoLice> _trenutni;
                IQueryable<Baza.FizickoLice> _vrati;

                #region Upit
                //ako je definisan kriterijum pretrage
                if (DefinisanKriterijumPretrage())
                {
                    if ((bool)radioButtonPretragaIme.IsChecked)
                    {
                        _upit = (from p in LavAutoDataContext.FizickoLices
                                 where p.Ime.Contains(textBoxPretragaIme.Text)
                                 select p).OrderBy(w => w.Ime);
                    }
                    else if ((bool)radioButtonPretragaTelefon.IsChecked)
                    {
                        _upit = (from p in LavAutoDataContext.FizickoLices
                                 where p.Telefon.Contains(textBoxPretragaTelefon.Text)
                                 select p).OrderBy(w => w.Ime);
                    }
                    else
                    {
                        _upit = (from p in LavAutoDataContext.FizickoLices
                                 select p).OrderBy(w => w.Ime);
                    } 
                }
                //ako nije definisan kriterijum pretrage prikazi samo trenutne
                else
                {
                    List<int> _trenutnoPrikazani = new List<int>();

                    foreach (var item in listBoxLista.Items)
                    {
                        Baza.FizickoLice _item = (Baza.FizickoLice)item;

                        _trenutnoPrikazani.Add(_item.FizickoLice_ID);
                    }

                    _upit = (from p in LavAutoDataContext.FizickoLices
                             where _trenutnoPrikazani.Contains(p.FizickoLice_ID)
                             select p).OrderBy(w => w.Ime);
                }


                if (StanjeTrenutno.Equals(App.Stanje.Unos) || StanjeTrenutno.Equals(App.Stanje.Izmena))
                {
                    _trenutni = (from p in LavAutoDataContext.FizickoLices
                                 where p.Sifra.Equals(textBoxSifra.Text)
                                 select p).OrderBy(w => w.Ime);

                    _vrati = _trenutni.Union(_upit).OrderBy(w => w.Ime);

                }
                else
                {
                    _vrati = _upit;
                } 
                #endregion
                  
                try
                {
                    ObservableCollection<Baza.FizickoLice> _lista = new ObservableCollection<Baza.FizickoLice>(_vrati.ToList());

                    listBoxLista.ItemsSource = _lista;

                    ICollectionView view = CollectionViewSource.GetDefaultView(listBoxLista.ItemsSource);
                    view.GroupDescriptions.Add(new PropertyGroupDescription("Ime"));

                    view.SortDescriptions.Add(new SortDescription("Ime", ListSortDirection.Ascending));
                    

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
                    Baza.FizickoLice _trenutni = (Baza.FizickoLice)gridDetaljno.DataContext;
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
                ObservableCollection<Baza.FizickoLice> _lista = (ObservableCollection<Baza.FizickoLice>)listBoxLista.ItemsSource;
                bool _postoji = false;

                if (!listBoxLista.Items.Count.Equals(0))
                {
                    foreach (Baza.FizickoLice item in _lista)
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

                if (PrikaziFizickoLice == null)
                {
                    ObservableCollection<Baza.FizickoLice> _lista = new ObservableCollection<Baza.FizickoLice>();
                    listBoxLista.ItemsSource = _lista;
                }
                else
                {
                    IQueryable<Baza.FizickoLice> _upit = (from p in LavAutoDataContext.FizickoLices
                                                              where p.FizickoLice_ID == PrikaziFizickoLice.FizickoLice_ID
                                                              select p).Take(1);

                    try
                    {
                        ObservableCollection<Baza.FizickoLice> _lista = new ObservableCollection<Baza.FizickoLice>(_upit.ToList());
                        listBoxLista.ItemsSource = _lista;

                        ICollectionView view = CollectionViewSource.GetDefaultView(listBoxLista.ItemsSource);
                        view.GroupDescriptions.Add(new PropertyGroupDescription("Ime"));

                        view.SortDescriptions.Add(new SortDescription("Ime", ListSortDirection.Ascending));

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


            }
            else if ((StanjeTrenutno != App.Stanje.Izmena) && (StanjeTrenutno != App.Stanje.Unos))
            {
                if (!listBoxLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.FizickoLice _trenutni = (Baza.FizickoLice)gridDetaljno.DataContext;
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
                Baza.FizickoLice _trenutni = (Baza.FizickoLice)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }

            ObservableCollection<Baza.FizickoLice> _lista = (ObservableCollection<Baza.FizickoLice>)listBoxLista.ItemsSource;

            Baza.FizickoLice _novi = new Baza.FizickoLice();

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
                LavAutoDataContext.FizickoLices.DeleteOnSubmit((Baza.FizickoLice)gridDetaljno.DataContext);

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
            if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruFizickoLice)
            {
                try
                {
                    LavAutoDataContext.ResetujBrojac("FizickoLice", "FizickoLice_ID");
                    textBoxSifra.Text = LavAutoDataContext.DajSledeciIdentity("FizickoLice").ToString();
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
                    LavAutoDataContext.ResetujBrojac("FizickoLice", "FizickoLice_ID");
                }
                catch (Exception) { }
            }
            #endregion

            UpdateBindingSource();

            if (App.ImaLiGreskiValidacijeTextBox(gridDetaljno))
            {
                //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruFizickoLice)
                {
                    textBoxSifra.Text = "";
                }
                return;
            }

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                LavAutoDataContext.FizickoLices.InsertOnSubmit((Baza.FizickoLice)gridDetaljno.DataContext);
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                Baza.FizickoLice _trenutni = (Baza.FizickoLice)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }
            catch (Exception ex)
            {
                if (StanjeTrenutno.Equals(App.Stanje.Unos))
                {
                    //TODO .... Nisam nasao drugi nacin da izbacim objekat iz kolekcije Inserts nego da ga dodam i u kolekciju Deletes (ChangeSet changeSet = LavAutoDataContext.GetChangeSet();. Kako uopste moze da se otkaze unos, izmena ili brisanje
                    //LavAutoDataContext.FizickoLices.DeleteOnSubmit((Baza.FizickoLice)gridDetaljno.DataContext);

                    //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                    if (AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruFizickoLice)
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
            ObservableCollection<Baza.FizickoLice> _lista = (ObservableCollection<Baza.FizickoLice>)listBoxLista.ItemsSource;

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.FizickoLice)gridDetaljno.DataContext);
            }
            else if (StanjeTrenutno.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.FizickoLice _trenutni = (Baza.FizickoLice)gridDetaljno.DataContext;
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
                Baza.FizickoLice _trenutni = (Baza.FizickoLice)gridDetaljno.DataContext;
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

            if (_trenutni.Name == radioButtonPretragaIme.Name)
            {
                //if ((bool)_trenutni.IsChecked)
                //{
                    textBoxPretragaIme.IsEnabled = true;

                    textBoxPretragaTelefon.IsEnabled = false;
                    textBoxPretragaTelefon.Text = "";
                //}
                //else
                //{
                //    textBoxPretragaIme.IsEnabled = false;
                //    textBoxPretragaIme.Text = "";

                //    textBoxPretragaTelefon.IsEnabled = true;
                //}
            }
            else if (_trenutni.Name == radioButtonPretragaTelefon.Name)
            {
                //if ((bool)_trenutni.IsChecked)
                //{
                    textBoxPretragaIme.IsEnabled = false;
                    textBoxPretragaIme.Text = "";

                    textBoxPretragaTelefon.IsEnabled = true;
                //}
                //else
                //{
                //    textBoxPretragaIme.IsEnabled = true;

                //    textBoxPretragaTelefon.IsEnabled = false;
                //    textBoxPretragaTelefon.Text = "";

                //}

            }
            else if (_trenutni.Name == radioButtonPretragaPrikaziSve.Name)
            {
                //if ((bool)_trenutni.IsChecked)
                //{
                    textBoxPretragaIme.IsEnabled = false;
                    textBoxPretragaIme.Text = "";

                    textBoxPretragaTelefon.IsEnabled = false;
                    textBoxPretragaTelefon.Text = "";

                //}
                //else
                //{
                //    textBoxPretragaIme.IsEnabled = false;
                //    textBoxPretragaIme.Text = "";

                //    textBoxPretragaTelefon.IsEnabled = false;
                //    textBoxPretragaTelefon.Text = "";

                //}

            }
        }

        private void buttonOdaberi_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(new ReturnEventArgs<Object>(listBoxLista.SelectedItem));
        }


    }
}
