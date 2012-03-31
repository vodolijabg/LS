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

namespace Servis
{
    /// <summary>
    /// Interaction logic for NacinOrganizacijeFirme.xaml
    /// </summary>
    public partial class NacinOrganizacijeFirme : Page
    {
        //za navigaciju
        bool prvoOtvaranjeStrane = true;

        DB.DBProksi dBProksi;

        public NacinOrganizacijeFirme()
        {
            InitializeComponent();

            textBoxTraziZa.Focus();
        }

        public void UStanje(App.Stanje stanje)
        {
            buttonDodaj.IsEnabled = stanje.Equals(App.Stanje.Detaljno) || stanje.Equals(App.Stanje.Osnovno);
            buttonIzmeni.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonObrisi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
            buttonOsvezi.IsEnabled = stanje.Equals(App.Stanje.Detaljno);
        }

        private void DajSve()
        {
            try
            {
                listViewNacinOrganizacijeFirme.ItemsSource = dBProksi.DajSveNacinOrganizacijeFirme();

                if (!listViewNacinOrganizacijeFirme.Items.Count.Equals(0))
                {
                    listViewNacinOrganizacijeFirme.SelectedIndex = 0;
                    UStanje(App.Stanje.Detaljno);
                }
                else
                {
                    UStanje(App.Stanje.Osnovno);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void SelektujNacinOrganizacijeFirme(int nacinOrganizacijeFirmeID)
        {
            ObservableCollection<DB.NacinOrganizacijeFirme> _nacinOrganizacijeFirme = (ObservableCollection<DB.NacinOrganizacijeFirme>)listViewNacinOrganizacijeFirme.ItemsSource;
            bool _postoji = false;

            if (!listViewNacinOrganizacijeFirme.Items.Count.Equals(0))
            {
                foreach (DB.NacinOrganizacijeFirme item in _nacinOrganizacijeFirme)
                {
                    if (item.NacinOrganizacijeFirmeID.Equals(nacinOrganizacijeFirmeID))
                    {
                        listViewNacinOrganizacijeFirme.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    listViewNacinOrganizacijeFirme.SelectedIndex = 0;
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (prvoOtvaranjeStrane)
            {
                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                prvoOtvaranjeStrane = false;
                DajSve();
            }

            Window.GetWindow(this).Title = "Lav - NacinOrganizacijeFirme";
        }

        private void buttonDodaj_Click(object sender, RoutedEventArgs e)
        {
            NacinOrganizacijeFirmeDetaljno _nacinOrganizacijeFirmeDetaljno = new NacinOrganizacijeFirmeDetaljno(this, false);
            //_nacinOrganizacijeFirmeDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _nacinOrganizacijeFirmeDetaljno.Owner = Window.GetWindow(this);
            _nacinOrganizacijeFirmeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _nacinOrganizacijeFirmeDetaljno.ShowDialog();
        }

        private void buttonIzmeni_Click(object sender, RoutedEventArgs e)
        {
            ObservableCollection<DB.NacinOrganizacijeFirme> _naciniOrganizacijeFirme = (ObservableCollection<DB.NacinOrganizacijeFirme>)listViewNacinOrganizacijeFirme.ItemsSource;
            DB.NacinOrganizacijeFirme _nacinOrganizacijeFirme = (DB.NacinOrganizacijeFirme)listViewNacinOrganizacijeFirme.SelectedItem;

            if (_naciniOrganizacijeFirme != null && _nacinOrganizacijeFirme != null)
            {
                NacinOrganizacijeFirmeDetaljno _nacinOrganizacijeFirmeDetaljno = new NacinOrganizacijeFirmeDetaljno(this, true);
                //_nacinOrganizacijeFirmeDetaljno.WindowStyle = WindowStyle.ToolWindow;
                _nacinOrganizacijeFirmeDetaljno.Owner = Window.GetWindow(this);
                _nacinOrganizacijeFirmeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                _nacinOrganizacijeFirmeDetaljno.ShowDialog();
            }
        }

        private void buttonObrisi_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Obriši?", "Potvrdi brisanje", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                try
                {
                    dBProksi.ObrisiNacinOrganizacijeFirme((DB.NacinOrganizacijeFirme)listViewNacinOrganizacijeFirme.SelectedItem);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {
                    DajSve();
                }
                else
                {
                    //ako ima filter ali nije odabrana kolona ne prijavljuj gresku, osvezi podatke
                    if (comboBoxNacinOrganizacijeFirmeKolone.SelectedItem == null)
                    {
                        ObservableCollection<DB.NacinOrganizacijeFirme> _nacinOrganizacijeFirme = (ObservableCollection<DB.NacinOrganizacijeFirme>)listViewNacinOrganizacijeFirme.ItemsSource;

                        listViewNacinOrganizacijeFirme.ItemsSource = dBProksi.OsveziNacinOrganizacijeFirme(_nacinOrganizacijeFirme);

                        if (!listViewNacinOrganizacijeFirme.Items.Count.Equals(0))
                        {
                            listViewNacinOrganizacijeFirme.SelectedIndex = 0;
                            UStanje(App.Stanje.Detaljno);
                        }
                        else
                        {
                            UStanje(App.Stanje.Osnovno);
                        }
                        //inace pretrazi
                    }
                    else
                    {
                        listViewNacinOrganizacijeFirme.ItemsSource = dBProksi.NadjiNacinOrganizacijeFirme(((ComboBoxItem)comboBoxNacinOrganizacijeFirmeKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                        if (!listViewNacinOrganizacijeFirme.Items.Count.Equals(0))
                        {
                            listViewNacinOrganizacijeFirme.SelectedIndex = 0;
                            UStanje(App.Stanje.Detaljno);
                        }
                        else
                        {
                            UStanje(App.Stanje.Osnovno);
                        }
                    }
                }
            }
        }

        private void buttonOsvezi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.NacinOrganizacijeFirme _trenutni = (DB.NacinOrganizacijeFirme)listViewNacinOrganizacijeFirme.SelectedItem;

                ObservableCollection<DB.NacinOrganizacijeFirme> _naciniOrganizacijeFirme = (ObservableCollection<DB.NacinOrganizacijeFirme>)listViewNacinOrganizacijeFirme.ItemsSource;

                if (!_naciniOrganizacijeFirme.Count.Equals(0))
                {
                    listViewNacinOrganizacijeFirme.ItemsSource = dBProksi.OsveziNacinOrganizacijeFirme(_naciniOrganizacijeFirme);

                    if (_trenutni != null)
                    {
                        SelektujNacinOrganizacijeFirme(_trenutni.NacinOrganizacijeFirmeID);
                    }
                    if (listViewNacinOrganizacijeFirme.Items.Count.Equals(0))
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Detaljno);
                    }

                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonNadji_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                DB.NacinOrganizacijeFirme _trenutni = (DB.NacinOrganizacijeFirme)listViewNacinOrganizacijeFirme.SelectedItem;

                //ako nema filtera prikazi sve
                if (textBoxTraziZa.Text.Trim() == "")
                {

                    DajSve();

                }
                else if (comboBoxNacinOrganizacijeFirmeKolone.SelectedItem == null)
                {
                    MessageBox.Show("Odaberite polje po kome se pretražuje.", "Obavezan podatak", MessageBoxButton.OK, MessageBoxImage.Warning);
                    return;
                }
                else
                {
                    listViewNacinOrganizacijeFirme.ItemsSource = dBProksi.NadjiNacinOrganizacijeFirme(((ComboBoxItem)comboBoxNacinOrganizacijeFirmeKolone.SelectedItem).Content.ToString(), textBoxTraziZa.Text.Trim());

                    if (!listViewNacinOrganizacijeFirme.Items.Count.Equals(0))
                    {
                        listViewNacinOrganizacijeFirme.SelectedIndex = 0;
                        UStanje(App.Stanje.Detaljno);
                    }
                    else
                    {
                        UStanje(App.Stanje.Osnovno);
                    }
                }
                if (_trenutni != null)
                {
                    SelektujNacinOrganizacijeFirme(_trenutni.NacinOrganizacijeFirmeID);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewItem_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //ObservableCollection<DB.NacinOrganizacijeFirme> _nacinOrganizacijeFirmei = (ObservableCollection<DB.NacinOrganizacijeFirme>)listViewNacinOrganizacijeFirme.ItemsSource;
            //DB.NacinOrganizacijeFirme _nacinOrganizacijeFirme = (DB.NacinOrganizacijeFirme)listViewNacinOrganizacijeFirme.SelectedItem;

            //if (_nacinOrganizacijeFirmei != null && _nacinOrganizacijeFirme != null)
            //{
            NacinOrganizacijeFirmeDetaljno _nacinOrganizacijeFirmeDetaljno = new NacinOrganizacijeFirmeDetaljno(this, true);
            //_nacinOrganizacijeFirmeDetaljno.WindowStyle = WindowStyle.ToolWindow;
            _nacinOrganizacijeFirmeDetaljno.Owner = Window.GetWindow(this);
            _nacinOrganizacijeFirmeDetaljno.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            _nacinOrganizacijeFirmeDetaljno.ShowDialog();
            //}
        }

    }
}
