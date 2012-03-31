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
    /// Interaction logic for NosilacGrupeDetaljno.xaml
    /// </summary>
    public partial class NosilacGrupeDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.NosilacGrupe nosilacGrupe;

        public NosilacGrupeDetaljno(Servis.NosilacGrupe nosilacGrupe, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.nosilacGrupe = nosilacGrupe;

            if (izmeniTrenutni)
            {
                gridNosilacGrupe.DataContext = (DB.NosilacGrupe)this.nosilacGrupe.listViewNosilacGrupe.SelectedItem;
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
                        DB.NosilacGrupe _nosilacGrupe = new DB.NosilacGrupe
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim()
                        };

                        dBProksi.UnesiNosilacGrupe(_nosilacGrupe);

                        ObservableCollection<DB.NosilacGrupe> _nosiociGrupe = (ObservableCollection<DB.NosilacGrupe>)nosilacGrupe.listViewNosilacGrupe.ItemsSource;
                        _nosiociGrupe.Add(_nosilacGrupe);
                        nosilacGrupe.listViewNosilacGrupe.SelectedItem = _nosilacGrupe;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.NosilacGrupe _nosilacGrupeOrginal = (DB.NosilacGrupe)gridNosilacGrupe.DataContext;

                        DB.NosilacGrupe _nosilacGrupe = new DB.NosilacGrupe
                        {
                            NosilacGrupeID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                        };

                        dBProksi.IzmeniNosilacGrupe(_nosilacGrupe, _nosilacGrupeOrginal);

                        if (_nosilacGrupe.Naziv != _nosilacGrupeOrginal.Naziv)
                        {
                            dBProksi.MarkirajUsluguZaExport("NosilacGrupe", _nosilacGrupe.NosilacGrupeID);
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

        private void SelektujNosilacGrupe(int nosilacGrupeID)
        {
            ObservableCollection<DB.NosilacGrupe> _nosiociGrupe = (ObservableCollection<DB.NosilacGrupe>)nosilacGrupe.listViewNosilacGrupe.ItemsSource;
            bool _postoji = false;

            if (!nosilacGrupe.listViewNosilacGrupe.Items.Count.Equals(0))
            {
                foreach (DB.NosilacGrupe item in _nosiociGrupe)
                {
                    if (item.NosilacGrupeID.Equals(nosilacGrupeID))
                    {
                        nosilacGrupe.listViewNosilacGrupe.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    nosilacGrupe.listViewNosilacGrupe.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.NosilacGrupe _trenutni = (DB.NosilacGrupe)nosilacGrupe.listViewNosilacGrupe.SelectedItem;

                ObservableCollection<DB.NosilacGrupe> _nosiociGrupe = (ObservableCollection<DB.NosilacGrupe>)nosilacGrupe.listViewNosilacGrupe.ItemsSource;

                if (!_nosiociGrupe.Count.Equals(0))
                {
                    nosilacGrupe.listViewNosilacGrupe.ItemsSource = dBProksi.OsveziNosilacGrupe(_nosiociGrupe);

                    if (_trenutni != null)
                    {
                        SelektujNosilacGrupe(_trenutni.NosilacGrupeID);
                    }
                }
                gridNosilacGrupe.DataContext = (DB.NosilacGrupe)nosilacGrupe.listViewNosilacGrupe.SelectedItem;
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
                    gridNosilacGrupe.DataContext = null;

                    stanje = App.Stanje.Unos;
                    nosilacGrupe.UStanje(App.Stanje.Detaljno);
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
                    nosilacGrupe.UStanje(App.Stanje.Detaljno);
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

                    nosilacGrupe.UStanje(App.Stanje.Detaljno);
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
