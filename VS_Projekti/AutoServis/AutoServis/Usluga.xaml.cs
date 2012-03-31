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
    /// Interaction logic for Usluga.xaml
    /// </summary>
    public partial class Usluga : PageFunction<object>//Page
    {
        private App.Stanje StanjeTrenutno = App.Stanje.Osnovno;
        private string SifraTrenutna = "";

        Baza.LavAutoDataContext LavAutoDataContext = null;
        Baza.Usluga PrikaziFizickoLice = null;

        //za navigaciju
        bool PrvoOtvaranjeStrane = true;


        public Usluga()
        {
            InitializeComponent();
        }
        public Usluga(bool prikaziOdaberi): this()
        {
            buttonOdaberi.Visibility = Visibility.Visible;
        }

        public Usluga(bool prikaziOdaberi, Baza.Usluga usluga): this()
        {
            buttonOdaberi.Visibility = Visibility.Visible;
            this.PrikaziFizickoLice = usluga;
        }

        private void DajListuBod()
        {

            IQueryable<Baza.Bod> _upit = (from p in LavAutoDataContext.Bods
                                            select p).OrderBy(w => w.Naziv); ;
            try
            {
                ObservableCollection<Baza.Bod> _lista = new ObservableCollection<Baza.Bod>(_upit.ToList());

                comboBoxBodLista.ItemsSource = _lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju boda", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DajListuVrstaUsluge()
        {

            IQueryable<Baza.VrstaUsluge> _upit = (from p in LavAutoDataContext.VrstaUsluges
                                                  select p).OrderBy(w => w.VrstaUsluge_ID); ;
            try
            {
                ObservableCollection<Baza.VrstaUsluge> _lista = new ObservableCollection<Baza.VrstaUsluge>(_upit.ToList());

                comboBoxVrstaUslugeLista.ItemsSource = _lista;

                if (!comboBoxVrstaUslugeLista.Items.Count.Equals(0))
                {
                    comboBoxVrstaUslugeLista.SelectedIndex = 0;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju vrste usluge", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DajListuNivoUsluge()
        {

            IQueryable<Baza.Nivo> _upit = (from p in LavAutoDataContext.Nivos
                                           select p).OrderBy(w => w.Naziv); ;
            try
            {
                ObservableCollection<Baza.Nivo> _lista = new ObservableCollection<Baza.Nivo>(_upit.ToList());

                comboBoxNivoLista.ItemsSource = _lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju nivoa", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DajListuNosilacGrupe()
        {

            IQueryable<Baza.NosilacGrupe> _upit = (from p in LavAutoDataContext.NosilacGrupes
                                                   select p).OrderBy(w => w.Naziv); ;
            try
            {
                ObservableCollection<Baza.NosilacGrupe> _lista = new ObservableCollection<Baza.NosilacGrupe>(_upit.ToList());

                comboBoxNosilacGrupeLista.ItemsSource = _lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju nosioca grupe", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DajListuPoreskaStopa()
        {
            IQueryable<Baza.PoreskaStopa> _upit = (from p in LavAutoDataContext.PoreskaStopas
                                            select p).OrderBy(w => w.VrednostProcenata); ;
            try
            {
                ObservableCollection<Baza.PoreskaStopa> _lista = new ObservableCollection<Baza.PoreskaStopa>(_upit.ToList());

                comboBoxPoreskaStopaLista.ItemsSource = _lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju poreske stope", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void UStanje(App.Stanje stanje)
        {
            if (comboBoxVrstaUslugeLista.SelectedItem == null)
            {
                stanje = App.Stanje.IzgasiSve;
            }

            StanjeTrenutno = stanje;

            textBoxID.IsEnabled = false;
            textBoxSifra.IsEnabled = !AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruUsluga && (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));

            comboBoxVrstaUslugeLista.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija) || stanje.Equals(App.Stanje.IzgasiSve);
            
            comboBoxNivoLista.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            comboBoxNosilacGrupeLista.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));

            textBoxNormaSatiMinuta.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            comboBoxBodLista.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxBrojBodova.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            comboBoxPoreskaStopaLista.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));

            buttonUnesi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdi.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);
            buttonOdustani.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);
            

            buttonVrstaUsluge.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);
            buttonNivo.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);
            buttonNosilacGrupe.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);
            buttonBod.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);

            buttonNadji.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            buttonOdaberi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);


            listBoxLista.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            if (stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena))
            {
                if (!AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruUsluga)
                {
                    textBoxSifra.Focus();
                }
                else
                {
                    comboBoxVrstaUslugeLista.Focus();
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
            //comboBoxVrstaUslugeLista.SelectedItem = null;
            comboBoxNivoLista.SelectedItem = null;
            comboBoxNosilacGrupeLista.SelectedItem = null;
            textBoxNormaSatiMinuta.Text = "";
            comboBoxBodLista.SelectedItem = null;
            textBoxBrojBodova.Text = "";
            comboBoxPoreskaStopaLista.SelectedItem = null;
        }

        private void UpdateBindingSource()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateSource();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateSource();

            //BindingExpression _bindingExpressionVrstaUsluge = comboBoxVrstaUslugeLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            //_bindingExpressionVrstaUsluge.UpdateSource();

            BindingExpression _bindingExpressionNivo = comboBoxNivoLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            _bindingExpressionNivo.UpdateSource();
            BindingExpression _bindingExpressionNosilacGrupe = comboBoxNosilacGrupeLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            _bindingExpressionNosilacGrupe.UpdateSource();
            BindingExpression _bindingExpressionNormaSatiMinuta = textBoxNormaSatiMinuta.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNormaSatiMinuta.UpdateSource();
            BindingExpression _bindingExpressionBod = comboBoxBodLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            _bindingExpressionBod.UpdateSource();
            BindingExpression _bindingExpressionBrojBodova = textBoxBrojBodova.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionBrojBodova.UpdateSource();
            BindingExpression _bindingExpressionPoreskaStopa = comboBoxPoreskaStopaLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            _bindingExpressionPoreskaStopa.UpdateSource();
        }

        private bool DefinisanKriterijumPretrage()
        {
            if (!(bool)radioButtonPretragaPrikaziSve.IsChecked && textBoxPretragaNosilacGrupe.Text.Trim().Length.Equals(0))
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

            DajListuNivoUsluge();
            DajListuNosilacGrupe();
            DajListuBod();
            DajListuPoreskaStopa();

            if (LavAutoDataContext.DatabaseExists())
            {
                IQueryable<Baza.Usluga> _upit;
                IQueryable<Baza.Usluga> _trenutni;
                IQueryable<Baza.Usluga> _vrati;

                #region Upit
                Baza.VrstaUsluge _trenutniVrstaUsluge = null;

                if (comboBoxVrstaUslugeLista.SelectedItem != null)
                {
                    _trenutniVrstaUsluge = (Baza.VrstaUsluge)comboBoxVrstaUslugeLista.SelectedItem;
                }
                else
                {
                    MessageBox.Show("Odaberi vrstu usluge", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //ako je definisan kriterijum pretrage
                if (DefinisanKriterijumPretrage())
                {
                    if ((bool)radioButtonPretragaNosilacGrupe.IsChecked)
                    {
                        _upit = (from p in LavAutoDataContext.Uslugas
                                 where p.NosilacGrupe.Naziv.Contains(textBoxPretragaNosilacGrupe.Text)
                                 && p.VrstaUsluge_ID == _trenutniVrstaUsluge.VrstaUsluge_ID
                                 select p).OrderBy(w => w.NosilacGrupe.Naziv);
                    }
                    else
                    {
                        _upit = (from p in LavAutoDataContext.Uslugas
                                 where p.VrstaUsluge_ID == _trenutniVrstaUsluge.VrstaUsluge_ID
                                 select p).OrderBy(w => w.NosilacGrupe.Naziv);
                    }
                }
                //ako nije definisan kriterijum pretrage prikazi samo trenutne
                else
                {
                    List<int> _trenutnoPrikazani = new List<int>();

                    foreach (var item in listBoxLista.Items)
                    {
                        Baza.Usluga _item = (Baza.Usluga)item;

                        _trenutnoPrikazani.Add(_item.Usluga_ID);
                    }

                    _upit = (from p in LavAutoDataContext.Uslugas
                             where _trenutnoPrikazani.Contains(p.Usluga_ID)
                             && p.VrstaUsluge_ID == _trenutniVrstaUsluge.VrstaUsluge_ID
                             select p).OrderBy(w => w.NosilacGrupe.Naziv);
                }

                //ako sam zavrsio unos ili izmenu prikazi ga bez obzira da li ispunjava kriterijum pretrage
                if (StanjeTrenutno.Equals(App.Stanje.Unos) || StanjeTrenutno.Equals(App.Stanje.Izmena))
                {
                    _trenutni = (from p in LavAutoDataContext.Uslugas
                                 where p.Sifra == textBoxSifra.Text
                                 select p).OrderBy(w => w.NosilacGrupe.Naziv);


                    _vrati = _trenutni.Union(_upit).OrderBy(w => w.Sifra);
                }
                else
                {
                    _vrati = _upit;
                }
                #endregion

                //IQueryable<Baza.Usluga> _upit = (from p in LavAutoDataContext.Uslugas
                //                                          select p).OrderBy(w => w.NosilacGrupe.Naziv);
                try
                {

                    ObservableCollection<Baza.Usluga> _lista = new ObservableCollection<Baza.Usluga>(_vrati.ToList());

                    listBoxLista.ItemsSource = _lista;

                    ICollectionView view = CollectionViewSource.GetDefaultView(listBoxLista.ItemsSource);
                    view.GroupDescriptions.Add(new PropertyGroupDescription("NosilacGrupe.Naziv"));
                    //view.GroupDescriptions.Add(new PropertyGroupDescription("VrstaUsluge.Naziv"));
                    //view.SortDescriptions.Add(new SortDescription("NosilacGrupe.Naziv", ListSortDirection.Ascending));

                    //TODO...Resi ovo!!!
                    //Kad promenim vrednost u bazi i pozovem ovu metodu jednostavno nece drugacije da osvezi podatke u detaljima
                    //LavAutoDataContext.Refresh(RefreshMode.OverwriteCurrentValues, (ObservableCollection<Baza.Usluga>)listBoxLista.ItemsSource);

                    //UpdateBindingTarget();
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

        private void PrikaziTrenutni()
        {
            SifraTrenutna.Trim();
            if (SifraTrenutna != "")
            {
                ObservableCollection<Baza.Usluga> _lista = (ObservableCollection<Baza.Usluga>)listBoxLista.ItemsSource;
                bool _postoji = false;

                if (!listBoxLista.Items.Count.Equals(0))
                {
                    foreach (Baza.Usluga item in _lista)
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
                    //samo ovde se puni
                    DajListuVrstaUsluge();

                    DajListuNivoUsluge();
                    DajListuNosilacGrupe();
                    DajListuBod();
                    DajListuPoreskaStopa();
                }
                else
                {
                    MessageBox.Show("Neuspešna konekcija na bazu", "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (PrikaziFizickoLice == null)
                {
                    ObservableCollection<Baza.Usluga> _lista = new ObservableCollection<Baza.Usluga>();
                    listBoxLista.ItemsSource = _lista;
                }
                else
                {
                    IQueryable<Baza.Usluga> _upit = (from p in LavAutoDataContext.Uslugas
                                                     where p.Usluga_ID == PrikaziFizickoLice.Usluga_ID
                                                     select p).Take(1);

                    try
                    {
                        ObservableCollection<Baza.Usluga> _lista = new ObservableCollection<Baza.Usluga>(_upit.ToList());
                        listBoxLista.ItemsSource = _lista;

                        ICollectionView view = CollectionViewSource.GetDefaultView(listBoxLista.ItemsSource);
                        view.GroupDescriptions.Add(new PropertyGroupDescription("NosilacGrupe.Naziv"));

                        view.SortDescriptions.Add(new SortDescription("NosilacGrupe.Naziv", ListSortDirection.Ascending));

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška pri čitanju usluge", MessageBoxButton.OK, MessageBoxImage.Error);
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

                //DajSve();
            }
            else if ((StanjeTrenutno != App.Stanje.Izmena) && (StanjeTrenutno != App.Stanje.Unos))
            {
                if (comboBoxVrstaUslugeLista.SelectedItem == null)
                {
                    return;
                }

                if (!listBoxLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.Usluga _trenutni = (Baza.Usluga)gridDetaljno.DataContext;
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
                Baza.Usluga _trenutni = (Baza.Usluga)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }

            ObservableCollection<Baza.Usluga> _lista = (ObservableCollection<Baza.Usluga>)listBoxLista.ItemsSource;

            Baza.Usluga _novi = new Baza.Usluga();

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
                LavAutoDataContext.Uslugas.DeleteOnSubmit((Baza.Usluga)gridDetaljno.DataContext);

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
            if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruUsluga)
            {
                try
                {
                    LavAutoDataContext.ResetujBrojac("Usluga", "Usluga_ID");
                    textBoxSifra.Text = LavAutoDataContext.DajSledeciIdentity("Usluga").ToString();
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
                    LavAutoDataContext.ResetujBrojac("Usluga", "Usluga_ID");
                }
                catch (Exception) { }
            }
            #endregion

            UpdateBindingSource();

            Baza.Usluga _trenutnaUsluga = (Baza.Usluga)gridDetaljno.DataContext;
            Baza.VrstaUsluge _trenutniVrstaUsluge = (Baza.VrstaUsluge)comboBoxVrstaUslugeLista.SelectedItem;
            _trenutnaUsluga.VrstaUsluge_ID = _trenutniVrstaUsluge.VrstaUsluge_ID;

            if (App.ImaLiGreskiValidacijeTextBox(gridDetaljno) || App.ImaLiGreskiValidacijeComboBox(gridDetaljno))
            {
                //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruUsluga)
                {
                    textBoxSifra.Text = "";
                }
                return;
            }
            

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                LavAutoDataContext.Uslugas.InsertOnSubmit((Baza.Usluga)gridDetaljno.DataContext);
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                //Baza.Usluga _trenutni = (Baza.Usluga)gridDetaljno.DataContext;
                SifraTrenutna = _trenutnaUsluga.Sifra;
            }
            catch (Exception ex)
            {
                if (StanjeTrenutno.Equals(App.Stanje.Unos))
                {
                    //TODO .... Nisam nasao drugi nacin da izbacim objekat iz kolekcije Inserts nego da ga dodam i u kolekciju Deletes (ChangeSet changeSet = LavAutoDataContext.GetChangeSet();. Kako uopste moze da se otkaze unos, izmena ili brisanje
                    //LavAutoDataContext.Uslugas.DeleteOnSubmit((Baza.Usluga)gridDetaljno.DataContext);

                    //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                    if (AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruUsluga)
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
            ObservableCollection<Baza.Usluga> _lista = (ObservableCollection<Baza.Usluga>)listBoxLista.ItemsSource;

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.Usluga)gridDetaljno.DataContext);
            }
            else if (StanjeTrenutno.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.Usluga _trenutni = (Baza.Usluga)gridDetaljno.DataContext;
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
                Baza.Usluga _trenutni = (Baza.Usluga)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }
            DajSve();

            PrikaziTrenutni();
        }

        private void buttonZatvori_Click(object sender, RoutedEventArgs e)
        {

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
                    Baza.Usluga _trenutni = (Baza.Usluga)gridDetaljno.DataContext;
                    SifraTrenutna = _trenutni.Sifra;
                }
                DajSve();

                PrikaziTrenutni();
            }
        }

        private void buttonBod_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Bod());
        }

        private void buttonVrstaUsluge_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new VrstaUsluge());
        }

        private void buttonNivo_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Nivo());
        }

        private void buttonNosilacGrupe_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new NosilacGrupe());

        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton _trenutni = (RadioButton)sender;

            if (_trenutni.Name == radioButtonPretragaNosilacGrupe.Name)
            {
                textBoxPretragaNosilacGrupe.IsEnabled = true;
            }
            else if (_trenutni.Name == radioButtonPretragaPrikaziSve.Name)
            {
                textBoxPretragaNosilacGrupe.IsEnabled = false;
                textBoxPretragaNosilacGrupe.Text = "";
            }
        }

        private void comboBoxVrstaUslugeLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            listBoxLista.ItemsSource = null;
            listBoxLista.ItemsSource = new ObservableCollection<Baza.Usluga>();
            UStanje(App.Stanje.Osnovno);
        }

        private void buttonOdaberi_Click(object sender, RoutedEventArgs e)
        {
            OnReturn(new ReturnEventArgs<Object>(listBoxLista.SelectedItem));
        }

    }
}
