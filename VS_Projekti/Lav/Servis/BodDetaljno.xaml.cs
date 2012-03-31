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
using System.Globalization;

namespace Servis
{
    /// <summary>
    /// Interaction logic for BodDetaljno.xaml
    /// </summary>
    public partial class BodDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.Bod bod;

        public BodDetaljno(Servis.Bod bod, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.bod = bod;

            if (izmeniTrenutni)
            {
                gridBod.DataContext = (DB.Bod)this.bod.listViewBod.SelectedItem;
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
                //za proveru tipa podataka
                decimal _vrednost;

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
                else if (textBoxVrednost.Text.Trim().Equals(""))
                {
                    Dijalog _dialog = new Dijalog("Obavezan podatak", "Unesi vrednost za polje Vrednost.");
                    //_dialog.WindowStyle = WindowStyle.ToolWindow;
                    _dialog.Owner = Window.GetWindow(this);
                    _dialog.WindowStartupLocation = WindowStartupLocation.CenterOwner;
                    _dialog.ShowDialog();
                    return false;
                }
                else if (textBoxVrednost.Text.Trim() != "" && !decimal.TryParse(textBoxVrednost.Text, out _vrednost))
                {
                    Dijalog _dialog = new Dijalog("Pogrešan format", "Unesi decimalni broj za polje Vrednost.");
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
                        DB.Bod _bod = new DB.Bod
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                            Vrednost = Convert.ToDecimal(textBoxVrednost.Text, App.cultureInfo)
                        };

                        dBProksi.UnesiBod(_bod);

                        ObservableCollection<DB.Bod> _bodovi = (ObservableCollection<DB.Bod>)bod.listViewBod.ItemsSource;
                        _bodovi.Add(_bod);
                        bod.listViewBod.SelectedItem = _bod;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.Bod _bodOrginal = (DB.Bod)gridBod.DataContext;

                        DB.Bod _bod = new DB.Bod
                        {
                            BodID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                            Vrednost = Convert.ToDecimal(textBoxVrednost.Text, App.cultureInfo)
                        };

                        dBProksi.IzmeniBod(_bod, _bodOrginal);

                        if (_bod.Vrednost != _bodOrginal.Vrednost)
                        {
                            dBProksi.MarkirajUsluguZaExport("Bod", _bod.BodID);
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

        private void SelektujBod(int bodID)
        {
            ObservableCollection<DB.Bod> _bodovi = (ObservableCollection<DB.Bod>)bod.listViewBod.ItemsSource;
            bool _postoji = false;

            if (!bod.listViewBod.Items.Count.Equals(0))
            {
                foreach (DB.Bod item in _bodovi)
                {
                    if (item.BodID.Equals(bodID))
                    {
                        bod.listViewBod.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    bod.listViewBod.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.Bod _trenutni = (DB.Bod)bod.listViewBod.SelectedItem;

                ObservableCollection<DB.Bod> _bodovi = (ObservableCollection<DB.Bod>)bod.listViewBod.ItemsSource;

                if (!_bodovi.Count.Equals(0))
                {
                    bod.listViewBod.ItemsSource = dBProksi.OsveziBod(_bodovi);

                    if (_trenutni != null)
                    {
                        SelektujBod(_trenutni.BodID);
                    }
                }
                gridBod.DataContext = (DB.Bod)bod.listViewBod.SelectedItem;
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
                    gridBod.DataContext = null;

                    stanje = App.Stanje.Unos;
                    bod.UStanje(App.Stanje.Detaljno);
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
                    bod.UStanje(App.Stanje.Detaljno);
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

                    bod.UStanje(App.Stanje.Detaljno);
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
