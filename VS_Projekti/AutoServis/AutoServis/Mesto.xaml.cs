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
    /// Interaction logic for Mesto.xaml
    /// </summary>
    public partial class Mesto : Page
    {
        private App.Stanje StanjeTrenutno = App.Stanje.Osnovno;
        private string SifraTrenutna = "";

        Baza.LavAutoDataContext LavAutoDataContext = null;

        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        public Mesto()
        {
            InitializeComponent();
        }

        private void UStanje(App.Stanje stanje)
        {
            StanjeTrenutno = stanje;

            textBoxID.IsEnabled = false;
            textBoxSifra.IsEnabled = !AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruMesto && (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxNaziv.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxPozivniBroj.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxPostanskiBroj.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            buttonUnesi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdi.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);
            buttonOdustani.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);

            listBoxLista.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            if (stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena))
            {
                if (!AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruMesto)
                {
                    textBoxSifra.Focus();
                }
                else
                {
                    textBoxNaziv.Focus();
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
            textBoxNaziv.Text = "";
            textBoxPozivniBroj.Text = "";
            textBoxPostanskiBroj.Text = "";
        }

        private void UpdateBindingSource()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateSource();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateSource();
            BindingExpression _bindingExpressionNaziv = textBoxNaziv.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNaziv.UpdateSource();
            BindingExpression _bindingExpressionPozivniBroj = textBoxPozivniBroj.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPozivniBroj.UpdateSource();
            BindingExpression _bindingExpressionPostanskiBroj = textBoxPostanskiBroj.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPostanskiBroj.UpdateSource();
        }

        private void UpdateBindingTarget()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateTarget();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateTarget();
            BindingExpression _bindingExpressionNaziv = textBoxNaziv.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNaziv.UpdateTarget();
            BindingExpression _bindingExpressionPozivniBroj = textBoxPozivniBroj.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPozivniBroj.UpdateTarget();
            BindingExpression _bindingExpressionPostanskiBroj = textBoxPostanskiBroj.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPostanskiBroj.UpdateTarget();

            //TODO - UpdateBindingTarget listBoxLista
            listBoxLista.Items.Refresh();
        }

        private void DajSve()
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            if (LavAutoDataContext.DatabaseExists())
            {
                IQueryable<Baza.Mesto> _upit = (from p in LavAutoDataContext.Mestos
                                                       select p).OrderBy(w => w.Naziv);
                try
                {

                    ObservableCollection<Baza.Mesto> _lista = new ObservableCollection<Baza.Mesto>(_upit.ToList());

                    listBoxLista.ItemsSource = _lista;

                    //TODO...Resi ovo!!!
                    //Kad promenim vrednost u bazi i pozovem ovu metodu jednostavno nece drugacije da osvezi podatke u detaljima
                    //LavAutoDataContext.Refresh(RefreshMode.OverwriteCurrentValues, (ObservableCollection<Baza.Mesto>)listBoxLista.ItemsSource);

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
                ObservableCollection<Baza.Mesto> _lista = (ObservableCollection<Baza.Mesto>)listBoxLista.ItemsSource;
                bool _postoji = false;

                if (!listBoxLista.Items.Count.Equals(0))
                {
                    foreach (Baza.Mesto item in _lista)
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
                    Baza.Mesto _trenutni = (Baza.Mesto)gridDetaljno.DataContext;
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
                Baza.Mesto _trenutni = (Baza.Mesto)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }

            ObservableCollection<Baza.Mesto> _lista = (ObservableCollection<Baza.Mesto>)listBoxLista.ItemsSource;

            Baza.Mesto _novi = new Baza.Mesto();

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
                LavAutoDataContext.Mestos.DeleteOnSubmit((Baza.Mesto)gridDetaljno.DataContext);

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
            if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruMesto)
            {
                try
                {
                    LavAutoDataContext.ResetujBrojac("Mesto", "Mesto_ID");
                    textBoxSifra.Text = LavAutoDataContext.DajSledeciIdentity("Mesto").ToString();
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
                    LavAutoDataContext.ResetujBrojac("Mesto", "Mesto_ID");
                }
                catch (Exception) { }
            }
            #endregion

            UpdateBindingSource();

            if (App.ImaLiGreskiValidacijeTextBox(gridDetaljno))
            {
                //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruMesto)
                {
                    textBoxSifra.Text = "";
                }
                return;
            }

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                LavAutoDataContext.Mestos.InsertOnSubmit((Baza.Mesto)gridDetaljno.DataContext);
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                Baza.Mesto _trenutni = (Baza.Mesto)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }
            catch (Exception ex)
            {
                if (StanjeTrenutno.Equals(App.Stanje.Unos))
                {
                    //TODO .... Nisam nasao drugi nacin da izbacim objekat iz kolekcije Inserts nego da ga dodam i u kolekciju Deletes (ChangeSet changeSet = LavAutoDataContext.GetChangeSet();. Kako uopste moze da se otkaze unos, izmena ili brisanje
                    //LavAutoDataContext.Mestos.DeleteOnSubmit((Baza.Mesto)gridDetaljno.DataContext);

                    //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                    if (AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruMesto)
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
            ObservableCollection<Baza.Mesto> _lista = (ObservableCollection<Baza.Mesto>)listBoxLista.ItemsSource;

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.Mesto)gridDetaljno.DataContext);
            }
            else if (StanjeTrenutno.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.Mesto _trenutni = (Baza.Mesto)gridDetaljno.DataContext;
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
                Baza.Mesto _trenutni = (Baza.Mesto)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;

            }
            DajSve();

            PrikaziTrenutni();

        }

        private void buttonZatvori_Click(object sender, RoutedEventArgs e)
        {
        }
    }
}
