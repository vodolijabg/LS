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
    /// Interaction logic for PozicijaDetaljno.xaml
    /// </summary>
    public partial class PozicijaDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.Pozicija pozicija;

        public PozicijaDetaljno(Servis.Pozicija pozicija, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.pozicija = pozicija;

            if (izmeniTrenutni)
            {
                gridPozicija.DataContext = (DB.Pozicija)this.pozicija.listViewPozicija.SelectedItem;
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
                        DB.Pozicija _pozicija = new DB.Pozicija
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim()
                        };

                        dBProksi.UnesiPozicija(_pozicija);

                        ObservableCollection<DB.Pozicija> _pozicijaLista = (ObservableCollection<DB.Pozicija>)pozicija.listViewPozicija.ItemsSource;
                        _pozicijaLista.Add(_pozicija);
                        pozicija.listViewPozicija.SelectedItem = _pozicija;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.Pozicija _pozicijaOrginal = (DB.Pozicija)gridPozicija.DataContext;

                        DB.Pozicija _pozicija = new DB.Pozicija
                        {
                            PozicijaID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                        };

                        dBProksi.IzmeniPozicija(_pozicija, _pozicijaOrginal);

                        if (_pozicija.Naziv != _pozicijaOrginal.Naziv)
                        {
                            dBProksi.MarkirajUsluguZaExport("Pozicija", _pozicija.PozicijaID);
                        }
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujPozicija(int pozicijaID)
        {
            ObservableCollection<DB.Pozicija> _pozicijaLista = (ObservableCollection<DB.Pozicija>)pozicija.listViewPozicija.ItemsSource;
            bool _postoji = false;

            if (!pozicija.listViewPozicija.Items.Count.Equals(0))
            {
                foreach (DB.Pozicija item in _pozicijaLista)
                {
                    if (item.PozicijaID.Equals(pozicijaID))
                    {
                        pozicija.listViewPozicija.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    pozicija.listViewPozicija.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.Pozicija _trenutni = (DB.Pozicija)pozicija.listViewPozicija.SelectedItem;

                ObservableCollection<DB.Pozicija> _pozicijaLista = (ObservableCollection<DB.Pozicija>)pozicija.listViewPozicija.ItemsSource;

                if (!_pozicijaLista.Count.Equals(0))
                {
                    pozicija.listViewPozicija.ItemsSource = dBProksi.OsveziPozicija(_pozicijaLista);

                    if (_trenutni != null)
                    {
                        SelektujPozicija(_trenutni.PozicijaID);
                    }
                }
                gridPozicija.DataContext = (DB.Pozicija)pozicija.listViewPozicija.SelectedItem;
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
                    gridPozicija.DataContext = null;

                    stanje = App.Stanje.Unos;
                    pozicija.UStanje(App.Stanje.Detaljno);
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
                    pozicija.UStanje(App.Stanje.Detaljno);
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

                    pozicija.UStanje(App.Stanje.Detaljno);
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
