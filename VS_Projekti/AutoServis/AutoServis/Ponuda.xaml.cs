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
using System.Globalization;
using System.ComponentModel;

using System.Data.SqlClient;

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for Ponuda.xaml
    /// </summary>
    public partial class Ponuda : Page//PageFunction<String>
    {
        private App.Stanje StanjeTrenutnoZaglavlje = App.Stanje.Osnovno;
        private App.Stanje StanjeTrenutnoStavka = App.Stanje.Osnovno;

        private int IDTrenutniZaglavlje = 0;
        private int IDTrenutniStavkaUsluga = 0;

        Baza.LavAutoDataContext LavAutoDataContext = null;

        //za navigaciju
        bool PrvoOtvaranjeStrane = true;
        bool PromeniVrednostiUsluge = false;

        public Ponuda()
        {
            InitializeComponent();
        }

        private void DajListuPoreskaStopa()
        {
            IQueryable<Baza.PoreskaStopa> _upit = (from p in LavAutoDataContext.PoreskaStopas
                                                   select p).OrderBy(w => w.VrednostProcenata); ;
            try
            {
                ObservableCollection<Baza.PoreskaStopa> _listaUslugaPoreskaStopa = new ObservableCollection<Baza.PoreskaStopa>(_upit.ToList());
                comboBoxUslugaPoreskaStopaLista.ItemsSource = _listaUslugaPoreskaStopa;

                ObservableCollection<Baza.PoreskaStopa> _listaArtikalPoreskaStopa = new ObservableCollection<Baza.PoreskaStopa>(_upit.ToList());

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju poreske stope", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void DajListuNacinZahtevaZaPonudu()
        {
            IQueryable<Baza.NacinZahtevaZaPonudu> _upit = (from p in LavAutoDataContext.NacinZahtevaZaPonudus
                                                           //where p.NacinZahtevaZaPonudu_ID==-1
                                                           select p).OrderBy(w => w.Naziv);
            try
            {
                ObservableCollection<Baza.NacinZahtevaZaPonudu> _lista = new ObservableCollection<Baza.NacinZahtevaZaPonudu>(_upit.ToList());

                comboBoxNacinZahtevaZaPonuduLista.ItemsSource = _lista;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju načina zahteva za ponudu", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void UStanjeZaglavlje(App.Stanje stanje)
        {
            StanjeTrenutnoZaglavlje = stanje;

            textBoxZaglavljeID.IsEnabled = false;
            textBoxServisnaKnjizica.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxServisnaKnjizicaPartner.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxServisnaKnjizicaTipAutomobila.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxDatumDokumenta.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonServisnaKnjizica.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxNapomena.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            comboBoxNacinZahtevaZaPonuduLista.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            
            checkBoxPreuzimaLicno.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            checkBoxPreuzeoLicno.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxPreuzeoLicnoU.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            
            checkBoxObavestiTelefonom.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            checkBoxObavestenTelefonom.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxObavestenTelefonomU.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            
            checkBoxPosaljiSMSObavestenje.IsEnabled = (stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos));
            textBoxPoslatoSMSObavestenjeU.IsEnabled = false;

            buttonUnesiZaglavlje.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeniZaglavlje.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdiZaglavlje.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);
            buttonOdustaniZaglavlje.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisiZaglavlje.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsveziZaglavlje.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);

            buttonServisnaKnjizica.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            
            buttonNacinZahtevaZaPonudu.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);


            if (stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena))
            {
                buttonServisnaKnjizica.Focus();
            }
            if (stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno))
            {
                buttonUnesiZaglavlje.Focus();
            }

            tabItemPretraga.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            tabItemStavke.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            tabItemStampa.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
        }

        private void UStanjeStavka(App.Stanje stanje)
        {
            if (stanje == App.Stanje.Izmena || stanje == App.Stanje.Unos)
            {
                PromeniVrednostiUsluge = true;
            }
            else
            {
                PromeniVrednostiUsluge = false;
            }

            StanjeTrenutnoStavka = stanje;

            textBoxStavkaUslugaID.IsEnabled = false;
            textBoxUsluga.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxUslugaNosilacGrupe.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxUslugaNivo.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonUsluga.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxUslugaCenaBezPoreza.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            comboBoxUslugaPoreskaStopaLista.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            textBoxUslugaKolicina.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            listBoxStavkaArtikalLista.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);

            buttonDodajArtikal.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisiArtikal.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            
            listBoxStavkaUslugaLista.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);



            buttonUnesiStavka.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeniStavka.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonPotvrdiStavka.IsEnabled = stanje.Equals(App.Stanje.Unos) || stanje.Equals(App.Stanje.Izmena);
            buttonOdustaniStavka.IsEnabled = stanje.Equals(App.Stanje.Izmena) || stanje.Equals(App.Stanje.Unos);
            buttonObrisiStavka.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsveziStavka.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NeuspesnaKonekcija);


            tabItemPretraga.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NePostojiZaglavlje);
            tabItemZaglavlje.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NePostojiZaglavlje);
            tabItemStampa.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno) || stanje.Equals(App.Stanje.NePostojiZaglavlje);

        }

        private void UpdateBindingSourceZaglavlje()
        {
            BindingExpression _bindingExpressionZaglavljeID = textBoxZaglavljeID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionZaglavljeID.UpdateSource();

            BindingExpression _bindingExpressionServisnaKnjizica = textBoxServisnaKnjizica.GetBindingExpression(TextBox.TagProperty);
            _bindingExpressionServisnaKnjizica.UpdateSource();            
            
            BindingExpression _bindingExpressionDatumDokumenta = textBoxDatumDokumenta.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionDatumDokumenta.UpdateSource();
            BindingExpression _bindingExpressionNapomenaVlasnika = textBoxNapomena.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionNapomenaVlasnika.UpdateSource();

            BindingExpression _bindingExpressionNacinZahtevaZaPonudu = comboBoxNacinZahtevaZaPonuduLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            _bindingExpressionNacinZahtevaZaPonudu.UpdateSource();

            BindingExpression _bindingExpressionPreuzimaLicno = checkBoxPreuzimaLicno.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionPreuzimaLicno.UpdateSource();
            BindingExpression _bindingExpressionPreuzeoLicnoU = textBoxPreuzeoLicnoU.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPreuzeoLicnoU.UpdateSource();

            BindingExpression _bindingExpressionObavestiTelefonom = checkBoxObavestiTelefonom.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionObavestiTelefonom.UpdateSource();
            BindingExpression _bindingExpressionObavestenTelefonomU = textBoxObavestenTelefonomU.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionObavestenTelefonomU.UpdateSource();

            BindingExpression _bindingExpressionPosaljiSMSObavestenje = checkBoxPosaljiSMSObavestenje.GetBindingExpression(CheckBox.IsCheckedProperty);
            _bindingExpressionPosaljiSMSObavestenje.UpdateSource();
            BindingExpression _bindingExpressionPoslatoSMSObavestenjeU = textBoxPoslatoSMSObavestenjeU.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionPoslatoSMSObavestenjeU.UpdateSource();

        }

        private void UpdateBindingSourceStavka()
        {
            BindingExpression _bindingExpressionStavkaUslugaID = textBoxStavkaUslugaID.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionStavkaUslugaID.UpdateSource();

            BindingExpression _bindingExpressionUsluga = textBoxUsluga.GetBindingExpression(TextBox.TagProperty);
            _bindingExpressionUsluga.UpdateSource();
            BindingExpression _bindingExpressionUslugaCenaBezPoreza = textBoxUslugaCenaBezPoreza.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionUslugaCenaBezPoreza.UpdateSource();
            BindingExpression _bindingExpressionUslugaPoreskaStopa = comboBoxUslugaPoreskaStopaLista.GetBindingExpression(ComboBox.SelectedItemProperty);
            _bindingExpressionUslugaPoreskaStopa.UpdateSource();
            BindingExpression _bindingExpressionUslugaKolicina = textBoxUslugaKolicina.GetBindingExpression(TextBox.TextProperty);
            _bindingExpressionUslugaKolicina.UpdateSource();

        }

        private void IsprazniDetaljeZaglavlje()
        {
            textBoxZaglavljeID.Text = "";
            textBoxServisnaKnjizica.Tag = null;

            textBoxDatumDokumenta.Text = "";

            textBoxNapomena.Text = "";

            comboBoxNacinZahtevaZaPonuduLista.SelectedItem = null;

            checkBoxPreuzimaLicno.IsChecked = false;
            checkBoxPreuzeoLicno.IsChecked = false;
            textBoxPreuzeoLicnoU.Text = "";

            checkBoxObavestiTelefonom.IsChecked = false;
            checkBoxObavestenTelefonom.IsChecked = false;
            textBoxObavestenTelefonomU.Text = "";

            checkBoxPosaljiSMSObavestenje.IsChecked = false;
            textBoxPoslatoSMSObavestenjeU.Text = "";

        }

        private void IsprazniDetaljeStavka()
        {
            textBoxStavkaUslugaID.Text = "";
            textBoxUsluga.Tag = null;
            textBoxUslugaCenaBezPoreza.Text = "";
            comboBoxUslugaPoreskaStopaLista.SelectedItem = null;
            textBoxUslugaKolicina.Text = "";
        }

        private void PrikaziTrenutniZaglavlje()
        {
            if (IDTrenutniZaglavlje != 0)
            {
                ObservableCollection<Baza.Zaglavlje> _lista = (ObservableCollection<Baza.Zaglavlje>)listBoxZaglavljeLista.ItemsSource;
                bool _postoji = false;

                if (!listBoxZaglavljeLista.Items.Count.Equals(0))
                {
                    foreach (Baza.Zaglavlje item in _lista)
                    {
                        if (item.Zaglavlje_ID.Equals(IDTrenutniZaglavlje))
                        {
                            listBoxZaglavljeLista.SelectedItem = item;
                            _postoji = true;
                            break;
                        }
                    }
                    if (!_postoji)
                    {
                        listBoxZaglavljeLista.SelectedIndex = 0;
                    }
                }
                IDTrenutniZaglavlje = 0;
            }

        }

        private void PrikaziTrenutniStavka()
        {
            if (IDTrenutniStavkaUsluga != 0)
            {
                Baza.Zaglavlje _zaglavlje = (Baza.Zaglavlje)gridZaglavlje.DataContext;

                if (_zaglavlje == null)
                {
                    listBoxStavkaUslugaLista.ItemsSource = null;
                    return;
                }

                bool _postoji = false;

                if (!listBoxStavkaUslugaLista.Items.Count.Equals(0))
                {
                    foreach (Baza.StavkaUsluga item in _zaglavlje.Ponuda.StavkaUslugas)
                    {
                        if (item.StavkaUsluga_ID.Equals(IDTrenutniStavkaUsluga))
                        {
                            listBoxStavkaUslugaLista.SelectedItem = item;
                            _postoji = true;
                            break;
                        }
                    }
                    if (!_postoji)
                    {
                        listBoxStavkaUslugaLista.SelectedIndex = 0;
                    }
                }
                IDTrenutniStavkaUsluga = 0;
            }

        }

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (PrvoOtvaranjeStrane)
            {
                radioButtonFizickoLice.IsChecked = true;
                radioButton_Click(radioButtonFizickoLice, null);

                radioButtonPretragaID.IsChecked = true;
                radioButton_Click(radioButtonPretragaID, null);

                PrvoOtvaranjeStrane = false;

                LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

                DajListuNacinZahtevaZaPonudu();

                ObservableCollection<Baza.Zaglavlje> _zaglavlje = new ObservableCollection<Baza.Zaglavlje>();
                listBoxZaglavljeLista.ItemsSource = _zaglavlje;

                UStanjeZaglavlje(App.Stanje.Osnovno);
                UStanjeStavka(App.Stanje.NePostojiZaglavlje);

                textBoxPretragaID.Focus();

            }
            else if ((tabItemZaglavlje.IsSelected) && (StanjeTrenutnoZaglavlje != App.Stanje.Izmena) && (StanjeTrenutnoZaglavlje != App.Stanje.Unos))
            {
                if (!listBoxZaglavljeLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.Zaglavlje _trenutni = (Baza.Zaglavlje)gridZaglavlje.DataContext;
                    IDTrenutniZaglavlje = _trenutni.Zaglavlje_ID;
                }

                DajSve();
                PrikaziTrenutniZaglavlje();
            }
            else if ((tabItemStavke.IsSelected) && (StanjeTrenutnoStavka != App.Stanje.Izmena) && (StanjeTrenutnoStavka != App.Stanje.Unos))
            {
                if (!listBoxStavkaUslugaLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.Zaglavlje _trenutnoZaglavlje = (Baza.Zaglavlje)gridZaglavlje.DataContext;
                    IDTrenutniZaglavlje = _trenutnoZaglavlje.Zaglavlje_ID;

                    Baza.StavkaUsluga _trenutnaStavka = (Baza.StavkaUsluga)gridStavkaDetaljno.DataContext;
                    IDTrenutniStavkaUsluga = _trenutnaStavka.StavkaUsluga_ID;

                }
                DajSve();

                PrikaziTrenutniZaglavlje();
                PrikaziTrenutniStavka();
            }

        }

        //Z
        private void buttonUnesiZaglavlje_Click(object sender, RoutedEventArgs e)
        {

            if (!listBoxZaglavljeLista.Items.Count.Equals(0))
            {
                //da znam odakle sam posao
                Baza.Zaglavlje _trenutni = (Baza.Zaglavlje)gridZaglavlje.DataContext;
                IDTrenutniZaglavlje = _trenutni.Zaglavlje_ID;
            }

            ObservableCollection<Baza.Zaglavlje> _lista = (ObservableCollection<Baza.Zaglavlje>)listBoxZaglavljeLista.ItemsSource;

            Baza.Zaglavlje _novoZaglavlje = new Baza.Zaglavlje();
            Baza.Ponuda _novaPonuda = new Baza.Ponuda();
            _novoZaglavlje.Ponuda = _novaPonuda;

            _lista.Add(_novoZaglavlje);

            listBoxZaglavljeLista.SelectedItem = _novoZaglavlje;

            IsprazniDetaljeZaglavlje();

            UStanjeZaglavlje(App.Stanje.Unos);

            textBoxDatumDokumenta.Text = DateTime.Now.ToString("g", CultureInfo.CurrentCulture);

        }
        //S
        private void buttonUnesiStavka_Click(object sender, RoutedEventArgs e)
        {
            Baza.StavkaUsluga _trenutni = null;
            if (!listBoxStavkaUslugaLista.Items.Count.Equals(0))
            {
                //da znam odakle sam posao
                _trenutni = (Baza.StavkaUsluga)gridStavkaDetaljno.DataContext;

               
                IDTrenutniStavkaUsluga = _trenutni.StavkaUsluga_ID;
            }

            ObservableCollection<Baza.StavkaUsluga> _lista = (ObservableCollection<Baza.StavkaUsluga>)listBoxStavkaUslugaLista.ItemsSource;

            Baza.StavkaUsluga _novaStavka = new Baza.StavkaUsluga();

            _lista.Add(_novaStavka);

            listBoxStavkaUslugaLista.SelectedItem = _novaStavka;

            IsprazniDetaljeStavka();

            UStanjeStavka(App.Stanje.Unos);

            //if (_trenutni != null)
            //{
            //    textBoxUsluga.Tag = _trenutni.Usluga;

            //    textBoxUslugaCenaBezPoreza.Text = (_trenutni.Usluga.Bod.Vrednost * _trenutni.Usluga.BrojBodova).ToString("##.00").ToString(CultureInfo.CurrentCulture);
            //    comboBoxUslugaPoreskaStopaLista.SelectedItem = _trenutni.Usluga.PoreskaStopa;
            //    textBoxUslugaKolicina.Text = _trenutni.UslugaKolicina.ToString(CultureInfo.CurrentCulture);
            //}
        }

        //Z
        private void buttonIzmeniZaglavlje_Click(object sender, RoutedEventArgs e)
        {
            UStanjeZaglavlje(App.Stanje.Izmena);
        }
        //S
        private void buttonIzmeniStavka_Click(object sender, RoutedEventArgs e)
        {
            UStanjeStavka(App.Stanje.Izmena);
        }

        //Z
        private void buttonObrisiZaglavlje_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                //LavAutoDataContext.Zaglavljes.DeleteOnSubmit((Baza.Zaglavlje)gridZaglavlje.DataContext);
                Baza.Zaglavlje _trenutni = (Baza.Zaglavlje)gridZaglavlje.DataContext;

                _trenutni.VremePromene = DateTime.Now;
                _trenutni.KorisnickiNalog = LavAutoDataContext.DajKorisnickiNalog();
                _trenutni.Status = Convert.ToChar("D");

                _trenutni.Ponuda.VremePromene = DateTime.Now;
                _trenutni.Ponuda.KorisnickiNalog = LavAutoDataContext.DajKorisnickiNalog();
                _trenutni.Ponuda.Status = Convert.ToChar("D");

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
        //S
        private void buttonObrisiStavka_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Baza.StavkaUsluga _trenutni = (Baza.StavkaUsluga)gridStavkaDetaljno.DataContext;

                try
                {
                    DateTime _vremePromene = DateTime.Now;
                    string _korisnickiNalog = LavAutoDataContext.DajKorisnickiNalog();

                    _trenutni.VremePromene = _vremePromene;
                    _trenutni.KorisnickiNalog = _korisnickiNalog;
                    _trenutni.Status = Convert.ToChar("D");

                    foreach (Baza.StavkaArtikal item in _trenutni.StavkaArtikals)
                    {
                        item.VremePromene = _vremePromene;
                        item.KorisnickiNalog = _korisnickiNalog;
                        item.Status = Convert.ToChar("D");
                    }

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

        //Z
        private void buttonPotvrdiZaglavlje_Click(object sender, RoutedEventArgs e)
        {
            UpdateBindingSourceZaglavlje();

            if (App.ImaLiGreskiValidacijeTextBox(gridZaglavlje))
            {
                return;
            }
            if (App.ImaLiGreskiValidacijeTextBox(gridServisnaKnjizica))
            {
                return;
            }            
            if (App.ImaLiGreskiValidacijeComboBox(gridNacinZahtevaZaPonudu))
            {
                return;
            }

            try
            {
                LavAutoDataContext.ResetujBrojac("Zaglavlje", "Zaglavlje_ID");
            }
            catch (Exception)
            {
            }

            Baza.Zaglavlje _trenutni = (Baza.Zaglavlje)gridZaglavlje.DataContext;


            DateTime _trenutnoVreme = DateTime.Now;
            string _korisnickiNalog;
            Baza.Radnik _radnik;
            Baza.KorisnikPrograma _korisnikPrograma;

            try
            {
                _korisnickiNalog = LavAutoDataContext.DajKorisnickiNalog();
                _radnik = LavAutoDataContext.DajRadnikaZaKorisnickiNalog();
                _korisnikPrograma = LavAutoDataContext.DajKorisnikPrograma();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri upisu podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            _trenutni.VremePromene = _trenutnoVreme;
            _trenutni.KorisnickiNalog = _korisnickiNalog;

            _trenutni.Ponuda.VremePromene = _trenutnoVreme;
            _trenutni.Ponuda.KorisnickiNalog = _korisnickiNalog;


            if (StanjeTrenutnoZaglavlje.Equals(App.Stanje.Izmena))
            {
                _trenutni.Status = Convert.ToChar("U");
                _trenutni.Ponuda.Status = Convert.ToChar("U");
            }
            if (StanjeTrenutnoZaglavlje.Equals(App.Stanje.Unos))
            {
                _trenutni.KorisnikPrograma = _korisnikPrograma;
                _trenutni.Status = Convert.ToChar("I");
                _trenutni.Ponuda.Radnik = _radnik;
                _trenutni.Ponuda.Status = Convert.ToChar("I");


                LavAutoDataContext.Zaglavljes.InsertOnSubmit((Baza.Zaglavlje)gridZaglavlje.DataContext);
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                IDTrenutniZaglavlje = _trenutni.Zaglavlje_ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri upisu podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DajSve();
            PrikaziTrenutniZaglavlje();
        }
        //S
        private void buttonPotvrdiStavka_Click(object sender, RoutedEventArgs e)
        {
            PromeniVrednostiUsluge = false;

            UpdateBindingSourceStavka();

            if (App.ImaLiGreskiValidacijeComboBox(gridUsluga))
            {
                return;
            }
            if (App.ImaLiGreskiValidacijeTextBox(gridStavkaDetaljno))
            {
                return;
            }
            //if (App.ImaLiGreskiValidacijeTextBox(gridStavkaDetaljnoArtikal))
            //{
            //    return;
            //}

            try
            {
                LavAutoDataContext.ResetujBrojac("StavkaUsluga", "StavkaUsluga_ID");
                LavAutoDataContext.ResetujBrojac("StavkaArtikal", "StavkaArtikal_ID");
            }
            catch (Exception)
            {
            }


            Baza.Zaglavlje _trenutnoZaglavlje = (Baza.Zaglavlje)gridZaglavlje.DataContext;
            Baza.StavkaUsluga _trenutnaStavka = (Baza.StavkaUsluga)gridStavkaDetaljno.DataContext;

            _trenutnoZaglavlje.Ponuda.StavkaUslugas.Add(_trenutnaStavka);

            DateTime _trenutnoVreme = DateTime.Now;
            string _korisnickiNalog;
            Baza.Radnik _radnik;

            try
            {
                _korisnickiNalog = LavAutoDataContext.DajKorisnickiNalog();
                _radnik = LavAutoDataContext.DajRadnikaZaKorisnickiNalog();

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri upisu podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }


            _trenutnaStavka.VremePromene = _trenutnoVreme;
            _trenutnaStavka.KorisnickiNalog = _korisnickiNalog;

            if (StanjeTrenutnoStavka.Equals(App.Stanje.Izmena))
            {
                _trenutnaStavka.Status = Convert.ToChar("U");
            }
            if (StanjeTrenutnoStavka.Equals(App.Stanje.Unos))
            {
                _trenutnaStavka.Status = Convert.ToChar("I");

                LavAutoDataContext.StavkaUslugas.InsertOnSubmit(_trenutnaStavka);
            }

            ChangeSet _changeSet = LavAutoDataContext.GetChangeSet();

            foreach (object c in _changeSet.Inserts)
            {
                try
                {
                    Baza.StavkaArtikal _trenutni = (Baza.StavkaArtikal)c;
                    //zato sto se brise StavkaArtikal podesavanjem Statusa na "D" u buttonObrisiArtikal_Click
                    if (_trenutni.Status != Convert.ToChar("D"))
                    {
                        _trenutni.VremePromene = _trenutnoVreme;
                        _trenutni.KorisnickiNalog = _korisnickiNalog;
                        _trenutni.Status = Convert.ToChar("I");
                    }

                }
                catch (Exception)
                {
                }
            }
            foreach (object c in _changeSet.Updates)
            {
                try
                {
                    Baza.StavkaArtikal _trenutni = (Baza.StavkaArtikal)c;
                    //zato sto se brise StavkaArtikal podesavanjem Statusa na "D" u buttonObrisiArtikal_Click
                    if (_trenutni.Status != Convert.ToChar("D"))
                    {
                        _trenutni.VremePromene = _trenutnoVreme;
                        _trenutni.KorisnickiNalog = _korisnickiNalog;
                        _trenutni.Status = Convert.ToChar("U");
                    }
                }
                catch (Exception)
                {
                }
            }

            try
            {
                LavAutoDataContext.SubmitChanges();

                //da bude prikazan posle osvezavanja podataka
                IDTrenutniZaglavlje = _trenutnoZaglavlje.Zaglavlje_ID;
                IDTrenutniStavkaUsluga = _trenutnaStavka.StavkaUsluga_ID;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri upisu podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }

            DajSve();
            PrikaziTrenutniZaglavlje();
            PrikaziTrenutniStavka();
        }

        //Z
        private void buttonOdustaniZaglavlje_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<Baza.Zaglavlje> _lista = (ObservableCollection<Baza.Zaglavlje>)listBoxZaglavljeLista.ItemsSource;

            if (StanjeTrenutnoZaglavlje.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.Zaglavlje)gridZaglavlje.DataContext);
            }
            else if (StanjeTrenutnoZaglavlje.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.Zaglavlje _trenutni = (Baza.Zaglavlje)gridZaglavlje.DataContext;
                IDTrenutniZaglavlje = _trenutni.Zaglavlje_ID;
            }

            DajSve();
            PrikaziTrenutniZaglavlje();
        }
        //S
        private void buttonOdustaniStavka_Click(object sender, RoutedEventArgs e)
        {
            PromeniVrednostiUsluge = false;

            ObservableCollection<Baza.StavkaUsluga> _lista = (ObservableCollection<Baza.StavkaUsluga>)listBoxStavkaUslugaLista.ItemsSource;

            if (StanjeTrenutnoStavka.Equals(App.Stanje.Unos))
            {
                _lista.Remove((Baza.StavkaUsluga)gridStavkaDetaljno.DataContext);
            }
            else if (StanjeTrenutnoStavka.Equals(App.Stanje.Izmena))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.StavkaUsluga _trenutnaStavka = (Baza.StavkaUsluga)gridStavkaDetaljno.DataContext;
                IDTrenutniStavkaUsluga = _trenutnaStavka.StavkaUsluga_ID;
            }

            //da bude prikazan posle osvezavanja podataka
            Baza.Zaglavlje _trenutnoZaglavlje = (Baza.Zaglavlje)gridZaglavlje.DataContext;
            IDTrenutniZaglavlje = _trenutnoZaglavlje.Zaglavlje_ID;

            DajSve();
            PrikaziTrenutniZaglavlje();
            PrikaziTrenutniStavka();
        }

        //Z
        private void buttonOsveziZaglavlje_Click(object sender, RoutedEventArgs e)
        {
            if (!listBoxZaglavljeLista.Items.Count.Equals(0))
            {
                //da bude prikazan posle osvezavanja podataka
                Baza.Zaglavlje _trenutni = (Baza.Zaglavlje)gridZaglavlje.DataContext;
                IDTrenutniZaglavlje = _trenutni.Zaglavlje_ID;
            }

            DajSve();
            PrikaziTrenutniZaglavlje();

            
        }
        //S
        private void buttonOsveziStavka_Click(object sender, RoutedEventArgs e)
        {
            //da bude prikazan posle osvezavanja podataka
            Baza.Zaglavlje _trenutnoZaglavlje = (Baza.Zaglavlje)gridZaglavlje.DataContext;
            IDTrenutniZaglavlje = _trenutnoZaglavlje.Zaglavlje_ID;

            if (!listBoxStavkaUslugaLista.Items.Count.Equals(0))
            {
                Baza.StavkaUsluga _trenutnaStavka = (Baza.StavkaUsluga)gridStavkaDetaljno.DataContext;
                IDTrenutniStavkaUsluga = _trenutnaStavka.StavkaUsluga_ID;
            }
            DajSve();

            PrikaziTrenutniZaglavlje();
            PrikaziTrenutniStavka();
        }

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            if (!DefinisanKriterijumPretrage())
            {
                MessageBox.Show("Unesi kriterijum za pretragu", "Upozorenje", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
            else
            {
                if (!listBoxZaglavljeLista.Items.Count.Equals(0))
                {
                    //da bude prikazan posle osvezavanja podataka
                    Baza.Zaglavlje _trenutni = (Baza.Zaglavlje)gridZaglavlje.DataContext;
                    IDTrenutniZaglavlje = _trenutni.Zaglavlje_ID;
                }

                DajSve();
                PrikaziTrenutniZaglavlje();
            }
        }


        private bool DefinisanKriterijumPretrage()
        {
            if (textBoxFizickoLicePretraga.Tag==null && textBoxPoslovniPartnerPretraga.Tag==null && textBoxPretragaID.Text.Trim().Length.Equals(0))
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

            DajListuNacinZahtevaZaPonudu();
            DajListuPoreskaStopa();

            if (LavAutoDataContext.DatabaseExists())
            {
                IQueryable<Baza.Zaglavlje> _upit;
                IQueryable<Baza.Zaglavlje> _trenutni;
                IQueryable<Baza.Zaglavlje> _vrati;

                #region Upit

                if ((bool)radioButtonFizickoLice.IsChecked)
                {
                    //ako je definisan kriterijum pretrage
                    if (DefinisanKriterijumPretrage())
                    {
                        if ((bool)radioButtonPretragaID.IsChecked)
                        {
                            _upit = (from p in LavAutoDataContext.Zaglavljes
                                     where p.Zaglavlje_ID.ToString().Contains(textBoxPretragaID.Text)
                                     && p.ServisnaKnjizica.FizickoLice_ID != null
                                     && p.Status != Convert.ToChar("D")
                                     && p.Ponuda.Status != Convert.ToChar("D")
                                     //orderby p.Zaglavlje_ID descending
                                     select p);
                        }
                        else
                        {
                            Baza.FizickoLice _uslov = (Baza.FizickoLice)textBoxFizickoLicePretraga.Tag;

                            _upit = (from p in LavAutoDataContext.Zaglavljes
                                     where p.ServisnaKnjizica.FizickoLice_ID == _uslov.FizickoLice_ID
                                     && p.Status != Convert.ToChar("D")
                                     && p.Ponuda.Status != Convert.ToChar("D")
                                     select p);
                        }
                    }
                    //ako nije definisan kriterijum pretrage prikazi samo trenutne
                    else
                    {
                        List<int> _trenutnoPrikazani = new List<int>();

                        foreach (var item in listBoxZaglavljeLista.Items)
                        {
                            Baza.Zaglavlje _item = (Baza.Zaglavlje)item;

                            _trenutnoPrikazani.Add(_item.Zaglavlje_ID);
                        }

                        _upit = (from p in LavAutoDataContext.Zaglavljes
                                 where _trenutnoPrikazani.Contains(p.Zaglavlje_ID)
                                 && p.ServisnaKnjizica.FizickoLice_ID != null
                                 && p.Status != Convert.ToChar("D")
                                     && p.Ponuda.Status != Convert.ToChar("D")
                                 select p);
                    }
                }
                else //if ((bool)radioButtonPravnoLice.IsChecked)
                {
                    //ako je definisan kriterijum pretrage
                    if (DefinisanKriterijumPretrage())
                    {
                        if ((bool)radioButtonPretragaID.IsChecked)
                        {
                            _upit = (from p in LavAutoDataContext.Zaglavljes
                                     where p.Zaglavlje_ID.ToString().Contains(textBoxPretragaID.Text)
                                     && p.ServisnaKnjizica.PoslovniPartner_ID != null
                                     && p.Status != Convert.ToChar("D")
                                     && p.Ponuda.Status != Convert.ToChar("D")
                                     select p);
                        }
                        else
                        {
                            Baza.PoslovniPartner _uslov = (Baza.PoslovniPartner)textBoxPoslovniPartnerPretraga.Tag;

                            _upit = (from p in LavAutoDataContext.Zaglavljes
                                     where p.ServisnaKnjizica.PoslovniPartner_ID == _uslov.PoslovniPartner_ID
                                     && p.Status != Convert.ToChar("D")
                                     && p.Ponuda.Status != Convert.ToChar("D")
                                     select p);
                        }
                    }
                    //ako nije definisan kriterijum pretrage prikazi samo trenutne
                    else
                    {
                        List<int> _trenutnoPrikazani = new List<int>();

                        foreach (var item in listBoxZaglavljeLista.Items)
                        {
                            Baza.Zaglavlje _item = (Baza.Zaglavlje)item;

                            _trenutnoPrikazani.Add(_item.Zaglavlje_ID);
                        }

                        _upit = (from p in LavAutoDataContext.Zaglavljes
                                 where _trenutnoPrikazani.Contains(p.Zaglavlje_ID)
                                 && p.ServisnaKnjizica.PoslovniPartner_ID != null
                                 && p.Status != Convert.ToChar("D")
                                     && p.Ponuda.Status != Convert.ToChar("D")
                                 select p);
                    }

                }

                //ako sam zavrsio unos ili izmenu prikazi ga bez obzira da li ispunjava kriterijum pretrage
                //if (StanjeTrenutnoZaglavlje.Equals(App.Stanje.Unos) || StanjeTrenutnoZaglavlje.Equals(App.Stanje.Izmena))
                //{

                //vrati trenutni bez obzira na kriterijum pretrage
                if (!tabItemPretraga.IsSelected)
                {
                    _trenutni = (from p in LavAutoDataContext.Zaglavljes
                                 where p.Zaglavlje_ID.ToString() == textBoxZaglavljeID.Text
                                 && p.Status != Convert.ToChar("D")
                                     && p.Ponuda.Status != Convert.ToChar("D")
                                 select p);


                    _vrati = _trenutni.Union(_upit); 
                }
                else
                {
                    _vrati = _upit;
                }
                #endregion

                try
                {

                    ObservableCollection<Baza.Zaglavlje> _lista = new ObservableCollection<Baza.Zaglavlje>(_vrati.ToList());

                    listBoxZaglavljeLista.ItemsSource = null;
                    listBoxZaglavljeLista.ItemsSource = _lista;

                    ICollectionView view = CollectionViewSource.GetDefaultView(listBoxZaglavljeLista.ItemsSource);
                    //view.GroupDescriptions.Add(new PropertyGroupDescription("Ime"));

                    view.SortDescriptions.Add(new SortDescription("Zaglavlje_ID", ListSortDirection.Descending));


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

            if (!listBoxZaglavljeLista.Items.Count.Equals(0))
            {
                listBoxZaglavljeLista.SelectedIndex = 0;
                UStanjeZaglavlje(App.Stanje.Detaljno);

                if (!listBoxStavkaUslugaLista.Items.Count.Equals(0))
                {
                    listBoxStavkaUslugaLista.SelectedIndex = 0;
                    UStanjeStavka(App.Stanje.Detaljno);
                }
                else
                {
                    UStanjeStavka(App.Stanje.Osnovno);
                }
            }
            else
            {
                UStanjeZaglavlje(App.Stanje.Osnovno);
                UStanjeStavka(App.Stanje.NePostojiZaglavlje);
            }

        }


        private void checkBox_Click(object sender, RoutedEventArgs e)
        {
            CheckBox _trenutni = (CheckBox)sender;

            if (_trenutni.Name == checkBoxPreuzeoLicno.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                    textBoxPreuzeoLicnoU.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                }
                else
                {
                    textBoxPreuzeoLicnoU.Text = "";
                }

            }
            else if (_trenutni.Name == checkBoxObavestenTelefonom.Name)
            {
                if ((bool)_trenutni.IsChecked)
                {
                    textBoxObavestenTelefonomU.Text = DateTime.Now.ToString("dd.MM.yyyy HH:mm");
                }
                else
                {
                    textBoxObavestenTelefonomU.Text = "";
                }

            }
        }

        
        private void listBoxZaglavljeLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox _lista = (ListBox)sender;

            if (_lista.Items.Count.Equals(0))
            {
                listBoxStavkaUslugaLista.ItemsSource = null;
                return;
            }

            Baza.Zaglavlje _trenutnoZaglavlje = (Baza.Zaglavlje)_lista.SelectedItem;

            if (_trenutnoZaglavlje != null)
            {
                ObservableCollection<Baza.StavkaUsluga> _listaStavka = new ObservableCollection<Baza.StavkaUsluga>(_trenutnoZaglavlje.Ponuda.StavkaUslugas.ToList().Where(p => p.Status != Convert.ToChar("D")));
                listBoxStavkaUslugaLista.ItemsSource = _listaStavka;

                if (_trenutnoZaglavlje.ServisnaKnjizica != null)
                {
                    ObservableCollection<Baza.ServisnaKnjizica> _listaServisnaKnjizica = new ObservableCollection<Baza.ServisnaKnjizica>();
                    _listaServisnaKnjizica.Add(_trenutnoZaglavlje.ServisnaKnjizica);
                    textBoxServisnaKnjizica.Tag = _listaServisnaKnjizica.First();

                }
            }

            if (!listBoxStavkaUslugaLista.Items.Count.Equals(0))
            {
                listBoxStavkaUslugaLista.SelectedIndex = 0;
                UStanjeStavka(App.Stanje.Detaljno);
            }
            else
            {
                UStanjeStavka(App.Stanje.Osnovno);
            }
        }

        private void listBoxStavkaUslugaLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ListBox _lista = (ListBox)sender;

            if (_lista.Items.Count.Equals(0))
            {
                listBoxStavkaArtikalLista.ItemsSource = null;
                return;
            }

            Baza.StavkaUsluga _trenutaStavka = (Baza.StavkaUsluga)_lista.SelectedItem;

            if (_trenutaStavka != null)
            {
                ObservableCollection<Baza.StavkaArtikal> _listaStavkaArtikal = new ObservableCollection<Baza.StavkaArtikal>(_trenutaStavka.StavkaArtikals.ToList().Where(p => p.Status != Convert.ToChar("D")));
                
                if (listBoxStavkaArtikalLista.ItemsSource == null)
                {
                    listBoxStavkaArtikalLista.Items.Clear();

                }
                listBoxStavkaArtikalLista.ItemsSource = _listaStavkaArtikal;
            }
        }


        private void buttonNacinZahtevaZaPonudu_Click(object sender, RoutedEventArgs e)
        {
            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new NacinZahtevaZaPonudu());

        }

        private void buttonUsluga_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxUsluga.Tag == null)
            {
                Usluga _usluga = new Usluga(true);
                _usluga.Return += new ReturnEventHandler<object>(_usluga_Return);
                this.NavigationService.Navigate(_usluga);

            }
            //ako je vec odabran prikazi ga
            else
            {
                Baza.Usluga _trenutni = (Baza.Usluga)textBoxUsluga.Tag;

                Usluga _usluga = new Usluga(true, _trenutni);
                _usluga.Return += new ReturnEventHandler<object>(_usluga_Return);
                this.NavigationService.Navigate(_usluga);
            }

        }

        void _usluga_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                Baza.Usluga _usluga = (Baza.Usluga)e.Result;

                IQueryable<Baza.Usluga> _upit = (from p in LavAutoDataContext.Uslugas
                                                           where p.Usluga_ID == _usluga.Usluga_ID
                                                           select p).Take(1);

                try
                {
                    ObservableCollection<Baza.Usluga> _lista = new ObservableCollection<Baza.Usluga>(_upit.ToArray());
                    textBoxUsluga.Tag = _lista.First();

                    textBoxUslugaCenaBezPoreza.Text = (_lista.First().Bod.Vrednost * _lista.First().BrojBodova).ToString("##.00").ToString(CultureInfo.CurrentCulture);

                    comboBoxUslugaPoreskaStopaLista.SelectedItem = _lista.First().PoreskaStopa;

                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void radioButton_Click(object sender, RoutedEventArgs e)
        {
            RadioButton _trenutni = (RadioButton)sender;

            if (_trenutni.Name == radioButtonPretragaPartner.Name)
            {
                textBoxFizickoLicePretraga.IsEnabled = true;
                buttonPretragaFizickoLice.IsEnabled = true;

                textBoxPoslovniPartnerPretraga.IsEnabled = true;
                buttonPretragaPoslovniPartner.IsEnabled = true;

                textBoxPretragaID.Text = "";
                textBoxPretragaID.IsEnabled = false;
            }
            else if (_trenutni.Name == radioButtonPretragaID.Name)
            {
                textBoxFizickoLicePretraga.IsEnabled = false;
                textBoxFizickoLicePretraga.Tag = null;


                buttonPretragaFizickoLice.IsEnabled = false;

                textBoxPoslovniPartnerPretraga.IsEnabled = false;
                textBoxPoslovniPartnerPretraga.Tag = null;

                buttonPretragaPoslovniPartner.IsEnabled = false;

                textBoxPretragaID.IsEnabled = true;
            }
            else if (_trenutni.Name == radioButtonFizickoLice.Name)
            {
                gridPretragaPoslovniPartner.Visibility = Visibility.Collapsed;
                textBoxPoslovniPartnerPretraga.Tag = null;

                gridPretragaFizickoLice.Visibility = Visibility.Visible;

                listBoxZaglavljeLista.ItemsSource = null;
                ObservableCollection<Baza.Zaglavlje> _zaglavlje = new ObservableCollection<Baza.Zaglavlje>();
                listBoxZaglavljeLista.ItemsSource = _zaglavlje;

            }
            else if (_trenutni.Name == radioButtonPravnoLice.Name)
            {
                gridPretragaPoslovniPartner.Visibility = Visibility.Visible;

                gridPretragaFizickoLice.Visibility = Visibility.Collapsed;
                textBoxFizickoLicePretraga.Tag = null;

                listBoxZaglavljeLista.ItemsSource = null;
                ObservableCollection<Baza.Zaglavlje> _zaglavlje = new ObservableCollection<Baza.Zaglavlje>();
                listBoxZaglavljeLista.ItemsSource = _zaglavlje;

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


        private void buttonServisnaKnjizica_Click(object sender, RoutedEventArgs e)
        {
            if (textBoxServisnaKnjizica.Tag==null)
            {
                ServisnaKnjizica _servisnaKnjizica = new ServisnaKnjizica(true);
                _servisnaKnjizica.Return += new ReturnEventHandler<object>(_servisnaKnjizica_Return);
                this.NavigationService.Navigate(_servisnaKnjizica);

            }
            //ako je vec odabran prikazi ga
            else
            {
                Baza.ServisnaKnjizica _trenutni = (Baza.ServisnaKnjizica)textBoxServisnaKnjizica.Tag;

                ServisnaKnjizica _servisnaKnjizica = new ServisnaKnjizica(true, _trenutni);
                _servisnaKnjizica.Return += new ReturnEventHandler<object>(_servisnaKnjizica_Return);
                this.NavigationService.Navigate(_servisnaKnjizica);
            }
        }

        void _servisnaKnjizica_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                Baza.ServisnaKnjizica _servisnaKnjizica = (Baza.ServisnaKnjizica)e.Result;

                IQueryable<Baza.ServisnaKnjizica> _upit = (from p in LavAutoDataContext.ServisnaKnjizicas
                                                           where p.ServisnaKnjizica_ID == _servisnaKnjizica.ServisnaKnjizica_ID
                                                           select p).Take(1);

                try
                {
                    ObservableCollection<Baza.ServisnaKnjizica> _lista = new ObservableCollection<Baza.ServisnaKnjizica>(_upit.ToArray());
                    textBoxServisnaKnjizica.Tag = _lista.First();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }


        private void buttonZatvori_Click(object sender, RoutedEventArgs e)
        {
        }

        private void TabControl_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            TabControl _tab = (TabControl)sender;

            TabItem _trenutiItem = (TabItem)_tab.SelectedItem;

            if (_trenutiItem != null && _trenutiItem.Header == tabItemStampa.Header)
            {

                ////LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

                ////DataLoadOptions _dlo = new DataLoadOptions();
                ////_dlo.LoadWith<Baza.Ponuda>(c => c.Zaglavlje);
                ////LavAutoDataContext.LoadOptions = _dlo;

                //IQueryable<Baza.Zaglavlje> _upitZaglavlje = (from p in LavAutoDataContext.Zaglavljes
                //                                 select p).Where(p => p.Zaglavlje_ID == 1);

                //IQueryable<Baza.Ponuda> _upitPonuda = (from p in LavAutoDataContext.Ponudas
                //                                select p).Where(p => p.Ponuda_ID==1);

                //IQueryable<Baza.StavkaUsluga> _upitStavke = (from p in LavAutoDataContext.StavkaUslugas
                //                                 select p).Where(p => p.Ponuda_ID == 1);




                ////Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceZaglavlje = new Microsoft.Reporting.WinForms.ReportDataSource("Zaglavlje", _upitZaglavlje);
                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSourcePonuda = new Microsoft.Reporting.WinForms.ReportDataSource("Baza_Ponuda", _upitPonuda);
                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSourcePonudaStavkaUsluga = new Microsoft.Reporting.WinForms.ReportDataSource("System_Data_Linq_EntitySet_1", _upitStavke);
                
                ////reportDataSource.Value = productBindingSource;

                //this.reportViewerPonuda.LocalReport.DataSources.Clear();
                ////this.reportViewerPonuda.LocalReport.DataSources.Add(reportDataSourceZaglavlje);
                //this.reportViewerPonuda.LocalReport.DataSources.Add(reportDataSourcePonuda);
                //this.reportViewerPonuda.LocalReport.DataSources.Add(reportDataSourcePonudaStavkaUsluga);

                //this.reportViewerPonuda.LocalReport.ReportEmbeddedResource = "AutoServis.Report1.rdlc";

                //this.reportViewerPonuda.RefreshReport();

                ///////////////////////////////////////////////////////////////////////////////////////////

                //LavAutoDataSet _lavAutoDataSet = new LavAutoDataSet();
                ////_lavAutoDataSet.KorisnikPrograma.


                //SqlConnection _konekcijaSqlConnection = new SqlConnection(AutoServis.Properties.Settings.Default.KonekcioniString);
                //using (_konekcijaSqlConnection)
                //{
                //    SqlDataAdapter _korisnikPrograma = new SqlDataAdapter(new SqlCommand("select * from KorisnikPrograma where KorisnikPrograma_ID = 1",_konekcijaSqlConnection));
                //    SqlDataAdapter _zaglavlje = new SqlDataAdapter(new SqlCommand("select * from Zaglavlje where Zaglavlje_ID = 1",_konekcijaSqlConnection));
                //    SqlDataAdapter _ponuda = new SqlDataAdapter(new SqlCommand("select * from Ponuda where Ponuda_ID = 1",_konekcijaSqlConnection));
                //    SqlDataAdapter _stavkaUsluga = new SqlDataAdapter(new SqlCommand("select * from StavkaUsluga where Ponuda_ID = 1",_konekcijaSqlConnection));

                //    try
                //    {
                //        _konekcijaSqlConnection.Open();

                //        _korisnikPrograma.Fill(_lavAutoDataSet.KorisnikPrograma);
                //        _zaglavlje.Fill(_lavAutoDataSet.Zaglavlje);
                //        _ponuda.Fill(_lavAutoDataSet.Ponuda);
                //        _stavkaUsluga.Fill(_lavAutoDataSet.StavkaUsluga);


                //    }
                //    catch (Exception ex)
                //    {
                //        MessageBox.Show(ex.Message);
                //    }
                //    finally
                //    {
                //        _konekcijaSqlConnection.Close();
                //    }
                //}

                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceKorisnikPrograma = new Microsoft.Reporting.WinForms.ReportDataSource("LavAutoDataSet_KorisnikPrograma", _lavAutoDataSet.KorisnikPrograma);
                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceZaglavlje = new Microsoft.Reporting.WinForms.ReportDataSource("LavAutoDataSet_Zaglavlje", _lavAutoDataSet.Zaglavlje);
                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSourcePonuda = new Microsoft.Reporting.WinForms.ReportDataSource("LavAutoDataSet_Ponuda", _lavAutoDataSet.Ponuda);
                //Microsoft.Reporting.WinForms.ReportDataSource reportDataSourceStavkaUsluga = new Microsoft.Reporting.WinForms.ReportDataSource("LavAutoDataSet_StavkaUsluga", _lavAutoDataSet.StavkaUsluga);
                
                //this.reportViewerPonuda.LocalReport.DataSources.Clear();
                //this.reportViewerPonuda.LocalReport.DataSources.Add(reportDataSourceKorisnikPrograma);
                //this.reportViewerPonuda.LocalReport.DataSources.Add(reportDataSourceZaglavlje);
                //this.reportViewerPonuda.LocalReport.DataSources.Add(reportDataSourcePonuda);
                //this.reportViewerPonuda.LocalReport.DataSources.Add(reportDataSourceStavkaUsluga);

                //this.reportViewerPonuda.LocalReport.ReportEmbeddedResource = "AutoServis.Report1.rdlc";

                //this.reportViewerPonuda.RefreshReport();


            }
        }

        private void buttonDodajArtikal_Click(object sender, RoutedEventArgs e)
        {
            Artikal _artikal = new Artikal(true);
            _artikal.Return += new ReturnEventHandler<object>(_artikal_Return);
            this.NavigationService.Navigate(_artikal);
        }

        void _artikal_Return(object sender, ReturnEventArgs<object> e)
        {
            if (e != null)
            {
                int _vezaArtikalDobavljac_ID = -1;
                int _artikal_ID = -1;
                string _dobavljac = "";
                decimal _cenaBezPoreza = -1;
                int _poreskaStopa_ID = -1;
                int _poreskaStopaVrednost = -1;
                decimal _cenaSaPorezom = -1;
                DateTime _datumAzuriranja = DateTime.Now;
                int _kolicinaNaStanju = -1;

                System.Reflection.PropertyInfo[] myPropertyInfo = e.Result.GetType().GetProperties();

                for (int i = 0; i < myPropertyInfo.Length; i++)
                {
                    switch (myPropertyInfo[i].Name.ToString())
                    {
                        case "VezaArtikalDobavljac_ID":
                            _vezaArtikalDobavljac_ID = (int)myPropertyInfo[i].GetValue(e.Result, null);
                            break;

                        case "Artikal_ID":
                            _artikal_ID = (int)myPropertyInfo[i].GetValue(e.Result, null);
                            break;
                        case "Dobavljac":
                            _dobavljac = myPropertyInfo[i].GetValue(e.Result, null).ToString();
                            break;
                        case "CenaBezPoreza":
                            _cenaBezPoreza = (decimal)myPropertyInfo[i].GetValue(e.Result, null);
                            break;
                        case "PoreskaStopa_ID":
                            _poreskaStopa_ID = (int)myPropertyInfo[i].GetValue(e.Result, null);
                            break;
                        case "PoreskaStopaVrednost":
                            _poreskaStopaVrednost = (int)myPropertyInfo[i].GetValue(e.Result, null);
                            break;
                        case "CenaSaPorezom":
                            _cenaSaPorezom = (decimal)myPropertyInfo[i].GetValue(e.Result, null);
                            break;
                        case "DatumAzuriranja":
                            _datumAzuriranja = (DateTime)myPropertyInfo[i].GetValue(e.Result, null);
                            break;
                        case "KolicinaNaStanju":
                            _kolicinaNaStanju = (int)myPropertyInfo[i].GetValue(e.Result, null);
                            break;
                        default:
                            throw new Exception("Greska pri citanju podataka o dobavljacima (_artikal_Return)");
                    }
                }

                IQueryable<Baza.VezaArtikalDobavljac> _upitVezaArtikalDobavljac = (from p in LavAutoDataContext.VezaArtikalDobavljacs
                                                                                   where p.VezaArtikalDobavljac_ID == _vezaArtikalDobavljac_ID
                                                                                   select p).Take(1);

                ObservableCollection<Baza.VezaArtikalDobavljac> _listaVezaArtikalDobavljac = null;
                try
                {
                    _listaVezaArtikalDobavljac = new ObservableCollection<Baza.VezaArtikalDobavljac>(_upitVezaArtikalDobavljac.ToList());
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                Baza.StavkaUsluga _trenutniStavkaUsluga = (Baza.StavkaUsluga)gridStavkaDetaljno.DataContext;

                ObservableCollection<Baza.StavkaArtikal> _listaStavkaArtikal = (ObservableCollection<Baza.StavkaArtikal>)listBoxStavkaArtikalLista.ItemsSource;

                foreach (Baza.StavkaArtikal item in _listaStavkaArtikal)
                {
                    if (item.VezaArtikalDobavljac_ID == _listaVezaArtikalDobavljac.First().VezaArtikalDobavljac_ID)
                    {
                        MessageBox.Show("Artikal dobavljača koji ste pokušali da dodate u dokumentu već postoji", "Greška pri dodavanju artikla dobavljača", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }
                }

                Baza.StavkaArtikal _novaStavkaArtikal = new Baza.StavkaArtikal();

                _novaStavkaArtikal.VezaArtikalDobavljac = _listaVezaArtikalDobavljac.First();
                _novaStavkaArtikal.StavkaUsluga = _trenutniStavkaUsluga;
                _novaStavkaArtikal.ArtikalCenaBezPoreza = _listaVezaArtikalDobavljac.First().CenaBezPoreza;
                _novaStavkaArtikal.ArtikalKolicina = 1;
                _novaStavkaArtikal.ArtikalPoreskaStopa_ID = _listaVezaArtikalDobavljac.First().Artikal.PoreskaStopa_ID;

                _listaStavkaArtikal.Add(_novaStavkaArtikal);

                //foreach (Baza.StavkaArtikal item in _listaStavkaArtikal)
                //{
                //    MessageBox.Show(item.StavkaUsluga.StavkaUsluga_ID.ToString());
                //}


                //listBoxStavkaUslugaLista.SelectedItem = _novaStavkaArtikal;


                //IQueryable<Baza.Artikal> _upit = (from p in LavAutoDataContext.Artikals
                //                                  where p.Artikal_ID == _artikal_ID
                //                                  select p).Take(1);

                //try
                //{
                //    ObservableCollection<Baza.Artikal> _lista = new ObservableCollection<Baza.Artikal>(_upit.ToArray());
                //    textBoxArtikal.Tag = _lista.First();
                //}
                //catch (Exception ex)
                //{
                //    MessageBox.Show(ex.Message, "Greška pri čitanju podataka", MessageBoxButton.OK, MessageBoxImage.Error);
                //}

                //textBoxArtikalCenaBezPoreza.Text = _cenaBezPoreza.ToString(CultureInfo.CurrentCulture);

                //if (!comboBoxArtikalPoreskaStopaLista.Items.Count.Equals(0))
                //{
                //    comboBoxArtikalPoreskaStopaLista.SelectedItem = _upit.First().PoreskaStopa;
                //}
            }
        }

        private void buttonObrisiArtikal_Click_1(object sender, RoutedEventArgs e)
        {

        }


        private void buttonObrisiArtikal_Click(object sender, RoutedEventArgs e)
        {
            Baza.StavkaArtikal _trenutniStavkaArtikal = (Baza.StavkaArtikal)listBoxStavkaArtikalLista.SelectedItem;

            try
            {
                _trenutniStavkaArtikal.VremePromene = DateTime.Now;
                _trenutniStavkaArtikal.KorisnickiNalog = LavAutoDataContext.DajKorisnickiNalog();
                _trenutniStavkaArtikal.Status = Convert.ToChar("D");
            }
            catch (Exception)
            {
            }

            ObservableCollection<Baza.StavkaArtikal> _listaStavkaArtikal = (ObservableCollection<Baza.StavkaArtikal>)listBoxStavkaArtikalLista.ItemsSource;
            _listaStavkaArtikal.Remove(_trenutniStavkaArtikal);

        }

        private void textBoxUslugaArtikal_GotFocus(object sender, RoutedEventArgs e)
        {
            TextBox _trenutni = (TextBox)sender;
            int _uslugaArtikal_ID = (int)_trenutni.Tag;

            ObservableCollection<Baza.StavkaArtikal> _listaStavkaArtikal = (ObservableCollection<Baza.StavkaArtikal>)listBoxStavkaArtikalLista.ItemsSource;

            listBoxStavkaArtikalLista.SelectedItem = _listaStavkaArtikal.Select(p => p.StavkaArtikal_ID == _uslugaArtikal_ID).First();

            foreach (Baza.StavkaArtikal item in listBoxStavkaArtikalLista.Items)
            {
                if (item.StavkaArtikal_ID == _uslugaArtikal_ID)
                {
                    listBoxStavkaArtikalLista.SelectedItem = item;
                    break;
                }
            }

        }

        

    }
}
