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

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for Nivo.xaml
    /// </summary>
    public partial class Nivo : Page
    {
        private App.Stanje StanjeTrenutno = App.Stanje.Osnovno;
        private string SifraTrenutna = "";

        Baza.LavAutoDataContext LavAutoDataContext = null;

        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        public Nivo()
        {
            InitializeComponent();
        }

        private void UStanje(App.Stanje stanje)
        {
            StanjeTrenutno = stanje;

            textBoxID.IsEnabled = false;
            textBoxSifra.IsEnabled = !AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNivo && (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxNaziv.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            buttonUnesi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdi.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);
            buttonOdustani.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);

            listBoxLista.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);

            if (stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena))
            {
                if (!AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNivo)
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
        }

        private void UpdateBindingSource()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateSource();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateSource();
            BindingExpression _bindingExpressionNaziv = textBoxNaziv.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNaziv.UpdateSource();
        }

        private void UpdateBindingTarget()
        {
            BindingExpression _bindingExpressionID = textBoxID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionID.UpdateTarget();
            BindingExpression _bindingExpressionSifra = textBoxSifra.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionSifra.UpdateTarget();
            BindingExpression _bindingExpressionNaziv = textBoxNaziv.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNaziv.UpdateTarget();

            //TODO - UpdateBindingTarget listBoxLista
            listBoxLista.Items.Refresh();

        }

        private void DajSve()
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            if (LavAutoDataContext.DatabaseExists())
            {
                IQueryable<Baza.Nivo> _upit = (from p in LavAutoDataContext.Nivos
                                                      select p).OrderBy(w => w.Naziv);
                try
                {

                    ObservableCollection<Baza.Nivo> _lista = new ObservableCollection<Baza.Nivo>(_upit.ToList());

                    listBoxLista.ItemsSource = _lista;

                    //TODO...Resi ovo!!!
                    //Kad promenim vrednost u bazi i pozovem ovu metodu jednostavno nece drugacije da osvezi podatke u detaljima
                    //LavAutoDataContext.Refresh(RefreshMode.OverwriteCurrentValues, (ObservableCollection<Baza.Nivo>)listBoxLista.ItemsSource);

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
                ObservableCollection<Baza.Nivo> _lista = (ObservableCollection<Baza.Nivo>)listBoxLista.ItemsSource;
                bool _postoji = false;

                if (!listBoxLista.Items.Count.Equals(0))
                {
                    foreach (Baza.Nivo item in _lista)
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
                    Baza.Nivo _trenutni = (Baza.Nivo)gridDetaljno.DataContext;
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
                Baza.Nivo _trenutni = (Baza.Nivo)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }

            ObservableCollection<Baza.Nivo> _lista = (ObservableCollection<Baza.Nivo>)listBoxLista.ItemsSource;

            Baza.Nivo _novi = new Baza.Nivo();

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
                LavAutoDataContext.Nivos.DeleteOnSubmit((Baza.Nivo)gridDetaljno.DataContext);

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
            if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNivo)
            {
                try
                {
                    LavAutoDataContext.ResetujBrojac("Nivo", "Nivo_ID");
                    textBoxSifra.Text = LavAutoDataContext.DajSledeciIdentity("Nivo").ToString();
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
                    LavAutoDataContext.ResetujBrojac("Nivo", "Nivo_ID");
                }
                catch (Exception) { }
            }
            #endregion

            UpdateBindingSource();

            if (App.ImaLiGreskiValidacijeTextBox(gridDetaljno))
            {
                //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                if (StanjeTrenutno.Equals(App.Stanje.Unos) && AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNivo)
                {
                    textBoxSifra.Text = "";
                }
                return;
            }

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                LavAutoDataContext.Nivos.InsertOnSubmit((Baza.Nivo)gridDetaljno.DataContext);
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                Baza.Nivo _trenutni = (Baza.Nivo)gridDetaljno.DataContext;
                SifraTrenutna = _trenutni.Sifra;
            }
            catch (Exception ex)
            {
                if (StanjeTrenutno.Equals(App.Stanje.Unos))
                {
                    //TODO .... Nisam nasao drugi nacin da izbacim objekat iz kolekcije Inserts nego da ga dodam i u kolekciju Deletes (ChangeSet changeSet = LavAutoDataContext.GetChangeSet();. Kako uopste moze da se otkaze unos, izmena ili brisanje
                    //LavAutoDataContext.Nivos.DeleteOnSubmit((Baza.Nivo)gridDetaljno.DataContext);

                    //Ako je stanje unos a i automatsko je dodeljivanje sifre obrisi sifru koja je automatski generisana
                    if (AutoServis.Properties.Settings.Default.AutomatskiDodeliSifruNivo)
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
            ObservableCollection<Baza.Nivo> _lista = (ObservableCollection<Baza.Nivo>)listBoxLista.ItemsSource;

            if (StanjeTrenutno.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.Nivo)gridDetaljno.DataContext);
            }
            else if (StanjeTrenutno.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.Nivo _trenutni = (Baza.Nivo)gridDetaljno.DataContext;
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
                Baza.Nivo _trenutni = (Baza.Nivo)gridDetaljno.DataContext;
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
