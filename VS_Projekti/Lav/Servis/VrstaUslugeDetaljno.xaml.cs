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
    /// Interaction logic for VrstaUslugeDetaljno.xaml
    /// </summary>
    public partial class VrstaUslugeDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.VrstaUsluge vrstaUsluge;

        public VrstaUslugeDetaljno(Servis.VrstaUsluge vrstaUsluge, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.vrstaUsluge = vrstaUsluge;

            if (izmeniTrenutni)
            {
                gridVrstaUsluge.DataContext = (DB.VrstaUsluge)this.vrstaUsluge.listViewVrstaUsluge.SelectedItem;
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
                        DB.VrstaUsluge _vrstaUsluge = new DB.VrstaUsluge
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim()
                        };

                        dBProksi.UnesiVrstaUsluge(_vrstaUsluge);

                        ObservableCollection<DB.VrstaUsluge> _vrsteUsluge = (ObservableCollection<DB.VrstaUsluge>)vrstaUsluge.listViewVrstaUsluge.ItemsSource;
                        _vrsteUsluge.Add(_vrstaUsluge);
                        vrstaUsluge.listViewVrstaUsluge.SelectedItem = _vrstaUsluge;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.VrstaUsluge _vrstaUslugeOrginal = (DB.VrstaUsluge)gridVrstaUsluge.DataContext;

                        DB.VrstaUsluge _vrstaUsluge = new DB.VrstaUsluge
                        {
                            VrstaUslugeID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                        };

                        dBProksi.IzmeniVrstaUsluge(_vrstaUsluge, _vrstaUslugeOrginal);

                        if (_vrstaUsluge.Naziv != _vrstaUslugeOrginal.Naziv)
                        {
                            dBProksi.MarkirajUsluguZaExport("VrstaUsluge", _vrstaUsluge.VrstaUslugeID);
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

        private void SelektujVrstaUsluge(int vrstaUslugeID)
        {
            ObservableCollection<DB.VrstaUsluge> _vrsteUsluge = (ObservableCollection<DB.VrstaUsluge>)vrstaUsluge.listViewVrstaUsluge.ItemsSource;
            bool _postoji = false;

            if (!vrstaUsluge.listViewVrstaUsluge.Items.Count.Equals(0))
            {
                foreach (DB.VrstaUsluge item in _vrsteUsluge)
                {
                    if (item.VrstaUslugeID.Equals(vrstaUslugeID))
                    {
                        vrstaUsluge.listViewVrstaUsluge.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    vrstaUsluge.listViewVrstaUsluge.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.VrstaUsluge _trenutni = (DB.VrstaUsluge)vrstaUsluge.listViewVrstaUsluge.SelectedItem;

                ObservableCollection<DB.VrstaUsluge> _vrsteUsluge = (ObservableCollection<DB.VrstaUsluge>)vrstaUsluge.listViewVrstaUsluge.ItemsSource;

                if (!_vrsteUsluge.Count.Equals(0))
                {
                    vrstaUsluge.listViewVrstaUsluge.ItemsSource = dBProksi.OsveziVrstaUsluge(_vrsteUsluge);

                    if (_trenutni != null)
                    {
                        SelektujVrstaUsluge(_trenutni.VrstaUslugeID);
                    }
                }
                gridVrstaUsluge.DataContext = (DB.VrstaUsluge)vrstaUsluge.listViewVrstaUsluge.SelectedItem;
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
                    gridVrstaUsluge.DataContext = null;

                    stanje = App.Stanje.Unos;
                    vrstaUsluge.UStanje(App.Stanje.Detaljno);
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
                    vrstaUsluge.UStanje(App.Stanje.Detaljno);
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

                    vrstaUsluge.UStanje(App.Stanje.Detaljno);
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
