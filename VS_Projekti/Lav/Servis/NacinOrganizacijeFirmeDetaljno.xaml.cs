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
using System.Windows.Shapes;
using System.Collections.ObjectModel;

namespace Servis
{
    /// <summary>
    /// Interaction logic for NacinOrganizacijeFirmeDetaljno.xaml
    /// </summary>
    public partial class NacinOrganizacijeFirmeDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.NacinOrganizacijeFirme nacinOrganizacijeFirme;

        public NacinOrganizacijeFirmeDetaljno(Servis.NacinOrganizacijeFirme nacinOrganizacijeFirme, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.nacinOrganizacijeFirme = nacinOrganizacijeFirme;

            if (izmeniTrenutni)
            {
                gridNacinOrganizacijeFirme.DataContext = (DB.NacinOrganizacijeFirme)this.nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.SelectedItem;
                stanje = App.Stanje.Izmena;
            }
            else
            {
                stanje = App.Stanje.Unos;
            }

        }

        public bool Sacuvaj()
        {
            try
            {
                if ((bool)checkBoxGenerisiSifru.IsChecked && textBoxSifra.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Šifra.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxNaziv.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Naziv.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else
                {
                    if (stanje == App.Stanje.Unos)
                    {
                        DB.NacinOrganizacijeFirme _nacinOrganizacijeFirme = new DB.NacinOrganizacijeFirme
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim()
                        };

                        dBProksi.UnesiNacinOrganizacijeFirme(_nacinOrganizacijeFirme);

                        ObservableCollection<DB.NacinOrganizacijeFirme> _naciniOrganizacijeFirme = (ObservableCollection<DB.NacinOrganizacijeFirme>)nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.ItemsSource;
                        _naciniOrganizacijeFirme.Add(_nacinOrganizacijeFirme);
                        nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.SelectedItem = _nacinOrganizacijeFirme;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.NacinOrganizacijeFirme _nacinOrganizacijeFirme = new DB.NacinOrganizacijeFirme
                        {
                            NacinOrganizacijeFirmeID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                        };

                        dBProksi.IzmeniNacinOrganizacijeFirme(_nacinOrganizacijeFirme, (DB.NacinOrganizacijeFirme)gridNacinOrganizacijeFirme.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujNacinOrganizacijeFirme(int nacinOrganizacijeFirmeID)
        {
            ObservableCollection<DB.NacinOrganizacijeFirme> _naciniOrganizacijeFirme = (ObservableCollection<DB.NacinOrganizacijeFirme>)nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.ItemsSource;
            bool _postoji = false;

            if (!nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.Items.Count.Equals(0))
            {
                foreach (DB.NacinOrganizacijeFirme item in _naciniOrganizacijeFirme)
                {
                    if (item.NacinOrganizacijeFirmeID.Equals(nacinOrganizacijeFirmeID))
                    {
                        nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.NacinOrganizacijeFirme _trenutni = (DB.NacinOrganizacijeFirme)nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.SelectedItem;

                ObservableCollection<DB.NacinOrganizacijeFirme> _naciniOrganizacijeFirme = (ObservableCollection<DB.NacinOrganizacijeFirme>)nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.ItemsSource;

                if (!_naciniOrganizacijeFirme.Count.Equals(0))
                {
                    nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.ItemsSource = dBProksi.OsveziNacinOrganizacijeFirme(_naciniOrganizacijeFirme);

                    if (_trenutni != null)
                    {
                        SelektujNacinOrganizacijeFirme(_trenutni.NacinOrganizacijeFirmeID);
                    }
                }
                gridNacinOrganizacijeFirme.DataContext = (DB.NacinOrganizacijeFirme)nacinOrganizacijeFirme.listViewNacinOrganizacijeFirme.SelectedItem;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        private void buttonSacuvajINovi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {
                    Osvezi();
                    gridNacinOrganizacijeFirme.DataContext = null;

                    stanje = App.Stanje.Unos;
                    nacinOrganizacijeFirme.UStanje(App.Stanje.Detaljno);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSacuvaj_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {
                    Osvezi();

                    stanje = App.Stanje.Izmena;
                    nacinOrganizacijeFirme.UStanje(App.Stanje.Detaljno);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void buttonSacuvajIZatvori_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {
                    Osvezi();

                    nacinOrganizacijeFirme.UStanje(App.Stanje.Detaljno);
                    this.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

    }
}
