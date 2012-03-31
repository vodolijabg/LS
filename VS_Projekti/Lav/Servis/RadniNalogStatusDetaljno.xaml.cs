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
    /// Interaction logic for RadniNalogStatusDetaljno.xaml
    /// </summary>
    public partial class RadniNalogStatusDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.RadniNalogStatus radniNalogStatus;

        public RadniNalogStatusDetaljno(Servis.RadniNalogStatus radniNalogStatus, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.radniNalogStatus = radniNalogStatus;

            if (izmeniTrenutni)
            {
                gridRadniNalogStatus.DataContext = (DB.RadniNalogStatus)this.radniNalogStatus.listViewRadniNalogStatus.SelectedItem;
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
                        DB.RadniNalogStatus _radniNalogStatus = new DB.RadniNalogStatus
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim()
                        };

                        dBProksi.UnesiRadniNalogStatus(_radniNalogStatus);

                        ObservableCollection<DB.RadniNalogStatus> _radniNalogStatusi = (ObservableCollection<DB.RadniNalogStatus>)radniNalogStatus.listViewRadniNalogStatus.ItemsSource;
                        _radniNalogStatusi.Add(_radniNalogStatus);
                        radniNalogStatus.listViewRadniNalogStatus.SelectedItem = _radniNalogStatus;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.RadniNalogStatus _radniNalogStatus = new DB.RadniNalogStatus
                        {
                            RadniNalogStatusID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                        };

                        dBProksi.IzmeniRadniNalogStatus(_radniNalogStatus, (DB.RadniNalogStatus)gridRadniNalogStatus.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujRadniNalogStatus(int radniNalogStatusID)
        {
            ObservableCollection<DB.RadniNalogStatus> _radniNalogStatusi = (ObservableCollection<DB.RadniNalogStatus>)radniNalogStatus.listViewRadniNalogStatus.ItemsSource;
            bool _postoji = false;

            if (!radniNalogStatus.listViewRadniNalogStatus.Items.Count.Equals(0))
            {
                foreach (DB.RadniNalogStatus item in _radniNalogStatusi)
                {
                    if (item.RadniNalogStatusID.Equals(radniNalogStatusID))
                    {
                        radniNalogStatus.listViewRadniNalogStatus.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    radniNalogStatus.listViewRadniNalogStatus.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.RadniNalogStatus _trenutni = (DB.RadniNalogStatus)radniNalogStatus.listViewRadniNalogStatus.SelectedItem;

                ObservableCollection<DB.RadniNalogStatus> _radniNalogStatusi = (ObservableCollection<DB.RadniNalogStatus>)radniNalogStatus.listViewRadniNalogStatus.ItemsSource;

                if (!_radniNalogStatusi.Count.Equals(0))
                {
                    radniNalogStatus.listViewRadniNalogStatus.ItemsSource = dBProksi.OsveziRadniNalogStatus(_radniNalogStatusi);

                    if (_trenutni != null)
                    {
                        SelektujRadniNalogStatus(_trenutni.RadniNalogStatusID);
                    }
                }
                gridRadniNalogStatus.DataContext = (DB.RadniNalogStatus)radniNalogStatus.listViewRadniNalogStatus.SelectedItem;
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
                    gridRadniNalogStatus.DataContext = null;

                    stanje = App.Stanje.Unos;
                    radniNalogStatus.UStanje(App.Stanje.Detaljno);
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
                    radniNalogStatus.UStanje(App.Stanje.Detaljno);
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

                    radniNalogStatus.UStanje(App.Stanje.Detaljno);
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
