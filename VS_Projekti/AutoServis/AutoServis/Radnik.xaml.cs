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
    /// Interaction logic for Radnik.xaml
    /// </summary>
    public partial class Radnik : Page
    {
        private App.Stanje StanjeTrenutno = App.Stanje.Osnovno;
        private string SifraTrenutna = "";

        Baza.LavAutoDataContext LavAutoDataContext = null;

        //za navigaciju
        bool PrvoOtvaranjeStrane = true;


        public Radnik()
        {
            InitializeComponent();
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
            textBoxSifra.IsEnabled = !AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnik && (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxIme.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxPrezime.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxImeOca.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxNadimak.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxJMBG.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxDatumRodjenja.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            comboBoxMestoLista.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxAdresa.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxTelefon.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxZaposlenOd.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxRaspored.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            buttonUnesi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdi.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);
            buttonOdustani.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);
            buttonMesto.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);


            listBoxLista.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            if (stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena))
            {
                if (!AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnik)
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
            textBoxImeOca.Text = "";
            textBoxNadimak.Text = "";
            textBoxDatumRodjenja.Text = "";
            comboBoxMestoLista.SelectedItem = null;
            textBoxAdresa.Text = "";
            textBoxTelefon.Text = "";
            textBoxZaposlenOd.Text = "";
            textBoxRaspored.Text = "";
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
            BindingExpression _bindingExpressionImeOca = textBoxImeOca.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionImeOca.UpdateSource();
            BindingExpression _bindingExpressionNadimak = textBoxNadimak.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNadimak.UpdateSource();
            BindingExpression _bindingExpressionJMBG = textBoxJMBG.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionJMBG.UpdateSource();
            BindingExpression _bindingExpressionDatumRodjenja = textBoxDatumRodjenja.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionDatumRodjenja.UpdateSource();
            //mesto
            BindingExpression _bindingExpressionAdresa = textBoxAdresa.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionAdresa.UpdateSource();
            BindingExpression _bindingExpressionTelefon = textBoxTelefon.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionTelefon.UpdateSource();
            BindingExpression _bindingExpressionZaposlenOd = textBoxZaposlenOd.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionZaposlenOd.UpdateSource();
            BindingExpression _bindingExpressionRaspored = textBoxRaspored.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionRaspored.UpdateSource();
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
            BindingExpression _bindingExpressionImeOca = textBoxImeOca.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionImeOca.UpdateTarget();
            BindingExpression _bindingExpressionNadimak = textBoxNadimak.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNadimak.UpdateTarget();
            BindingExpression _bindingExpressionJMBG = textBoxJMBG.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionJMBG.UpdateTarget();
            BindingExpression _bindingExpressionDatumRodjenja = textBoxDatumRodjenja.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionDatumRodjenja.UpdateTarget();
            //mesto
            BindingExpression _bindingExpressionAdresa = textBoxAdresa.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionAdresa.UpdateTarget();
            BindingExpression _bindingExpressionTelefon = textBoxTelefon.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionTelefon.UpdateTarget();
            BindingExpression _bindingExpressionZaposlenOd = textBoxZaposlenOd.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionZaposlenOd.UpdateTarget();
            BindingExpression _bindingExpressionRaspored = textBoxRaspored.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionRaspored.UpdateTarget();

            //TODO - UpdateBindingTarget listBoxLista
            listBoxLista.Items.Refresh();
            comboBoxMestoLista.Items.Refresh();

        }

        private void DajSve()
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            DajListuMesto();

            if (LavAutoDataContext.DatabaseExists())
            {
                IQueryable<Baza.Radnik> _upit = (from p in LavAutoDataContext.Radniks
                                                select p).OrderBy(w => w.Nadimak);
                try
                {

                    ObservableCollection<Baza.Radnik> _lista = new ObservableCollection<Baza.Radnik>(_upit.ToList());

                    listBoxLista.ItemsSource = _lista;

                    //TODO...Resi ovo!!!
                    //Kad promenim vrednost u bazi i pozovem ovu metodu jednostavno nece drugacije da osvezi podatke u detaljima
                    //LavAutoDataContext.Refresh(RefreshMode.OverwriteCurrentValues, (ObservableCollection<Baza.Radnik>)listBoxLista.ItemsSource);

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
                ObservableCollection<Baza.Radnik> _lista = (ObservableCollection<Baza.Radnik>)listBoxLista.ItemsSource;
                bool _postoji = false;

                if (!listBoxLista.Items.Count.Equals(0))
                {
                    foreach (Baza.Radnik item in _lista)
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

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (PrvoOtvaranjeStrane)
            {
                PrvoOtvaranjeStrane = false;
                DajSve();
            }
            else if ((StanjeTrenutno != App.Stanje.Izmena) && (StanjeTrenutno != App.Stanje.Unos))
            {
                if (!listBoxLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.Radnik _trenutni = (Baza.Radnik)gridDetaljno.DataContext;
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
                Baza.Radnik _trenutni = (Baza.Radnik)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }

            ObservableCollection<Baza.Radnik> _lista = (ObservableCollection<Baza.Radnik>)listBoxLista.ItemsSource;

            Baza.Radnik _novi = new Baza.Radnik();

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
                LavAutoDataContext.Radniks.DeleteOnSubmit((Baza.Radnik)gridDetaljno.DataContext);

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
            if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnik)
            {
                try
                {
                    LavAutoDataContext.ResetujBrojac("Radnik", "Radnik_ID");
                    textBoxSifra.Text = LavAutoDataContext.DajSledeciIdentity("Radnik").ToString();
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
                    LavAutoDataContext.ResetujBrojac("Radnik", "Radnik_ID");
                }
                catch (Exception) { }
            }
            #endregion

            UpdateBindingSource();

            if (App.ImaLiGreskiValidacijeTextBox(gridDetaljno))
            {
                //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnik)
                {
                    textBoxSifra.Text = "";
                }
                return;
            }

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                LavAutoDataContext.Radniks.InsertOnSubmit((Baza.Radnik)gridDetaljno.DataContext);
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                Baza.Radnik _trenutni = (Baza.Radnik)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }
            catch (Exception ex)
            {
                if (StanjeTrenutno.Equals(App.Stanje.Unos))
                {
                    //TODO .... Nisam nasao drugi nacin da izbacim objekat iz kolekcije Inserts nego da ga dodam i u kolekciju Deletes (ChangeSet changeSet = LavAutoDataContext.GetChangeSet();. Kako uopste moze da se otkaze unos, izmena ili brisanje
                    //LavAutoDataContext.Radniks.DeleteOnSubmit((Baza.Radnik)gridDetaljno.DataContext);

                    //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                    if (AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruRadnik)
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
            ObservableCollection<Baza.Radnik> _lista = (ObservableCollection<Baza.Radnik>)listBoxLista.ItemsSource;

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.Radnik)gridDetaljno.DataContext);
            }
            else if (StanjeTrenutno.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.Radnik _trenutni = (Baza.Radnik)gridDetaljno.DataContext;
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
                Baza.Radnik _trenutni = (Baza.Radnik)gridDetaljno.DataContext;
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
    }
}
