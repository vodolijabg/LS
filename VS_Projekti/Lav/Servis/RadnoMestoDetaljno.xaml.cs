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
    /// Interaction logic for RadnoMestoDetaljno.xaml
    /// </summary>
    public partial class RadnoMestoDetaljno : Window
    {

        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.RadnoMesto radnoMesto;

        public RadnoMestoDetaljno(Servis.RadnoMesto mesto, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.radnoMesto = mesto;

            if (izmeniTrenutni)
            {
                gridRadnoMesto.DataContext = (DB.RadnoMesto)this.radnoMesto.listViewRadnoMesto.SelectedItem;
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
                        DB.RadnoMesto _radnoMesto = new DB.RadnoMesto
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim()
                        };

                        dBProksi.UnesiRadnoMesto(_radnoMesto);

                        ObservableCollection<DB.RadnoMesto> _radnaMesta = (ObservableCollection<DB.RadnoMesto>)radnoMesto.listViewRadnoMesto.ItemsSource;
                        _radnaMesta.Add(_radnoMesto);
                        radnoMesto.listViewRadnoMesto.SelectedItem = _radnoMesto;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.RadnoMesto _radnoMesto = new DB.RadnoMesto
                        {
                            RadnoMestoID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                        };

                        dBProksi.IzmeniRadnoMesto(_radnoMesto, (DB.RadnoMesto)gridRadnoMesto.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujRadnoMesto(int radnoMestoID)
        {
            ObservableCollection<DB.RadnoMesto> _radnaMesta = (ObservableCollection<DB.RadnoMesto>)radnoMesto.listViewRadnoMesto.ItemsSource;
            bool _postoji = false;

            if (!radnoMesto.listViewRadnoMesto.Items.Count.Equals(0))
            {
                foreach (DB.RadnoMesto item in _radnaMesta)
                {
                    if (item.RadnoMestoID.Equals(radnoMestoID))
                    {
                        radnoMesto.listViewRadnoMesto.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    radnoMesto.listViewRadnoMesto.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.RadnoMesto _trenutni = (DB.RadnoMesto)radnoMesto.listViewRadnoMesto.SelectedItem;

                ObservableCollection<DB.RadnoMesto> _radnaMesta = (ObservableCollection<DB.RadnoMesto>)radnoMesto.listViewRadnoMesto.ItemsSource;

                if (!_radnaMesta.Count.Equals(0))
                {
                    radnoMesto.listViewRadnoMesto.ItemsSource = dBProksi.OsveziRadnaMesta(_radnaMesta);

                    if (_trenutni != null)
                    {
                        SelektujRadnoMesto(_trenutni.RadnoMestoID);
                    }
                }
                gridRadnoMesto.DataContext = (DB.RadnoMesto)radnoMesto.listViewRadnoMesto.SelectedItem;
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
                    gridRadnoMesto.DataContext = null;

                    stanje = App.Stanje.Unos;
                    radnoMesto.UStanje(App.Stanje.Detaljno);
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
                    radnoMesto.UStanje(App.Stanje.Detaljno);
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

                    radnoMesto.UStanje(App.Stanje.Detaljno);
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
