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
    /// Interaction logic for NivoDetaljno.xaml
    /// </summary>
    public partial class NivoDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.Nivo nivo;

        public NivoDetaljno(Servis.Nivo nivo, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.nivo = nivo;

            if (izmeniTrenutni)
            {
                gridNivo.DataContext = (DB.Nivo)this.nivo.listViewNivo.SelectedItem;
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
                        DB.Nivo _nivo = new DB.Nivo
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim()
                        };

                        dBProksi.UnesiNivo(_nivo);

                        ObservableCollection<DB.Nivo> _nivoi = (ObservableCollection<DB.Nivo>)nivo.listViewNivo.ItemsSource;
                        _nivoi.Add(_nivo);
                        nivo.listViewNivo.SelectedItem = _nivo;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.Nivo _nivoOrginal = (DB.Nivo)gridNivo.DataContext;

                        DB.Nivo _nivo = new DB.Nivo
                        {
                            NivoID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                        };

                        dBProksi.IzmeniNivo(_nivo, _nivoOrginal);

                        if (_nivo.Naziv != _nivoOrginal.Naziv)
                        {
                            dBProksi.MarkirajUsluguZaExport("Nivo", _nivo.NivoID);
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

        private void SelektujNivo(int nivoID)
        {
            ObservableCollection<DB.Nivo> _nivoi = (ObservableCollection<DB.Nivo>)nivo.listViewNivo.ItemsSource;
            bool _postoji = false;

            if (!nivo.listViewNivo.Items.Count.Equals(0))
            {
                foreach (DB.Nivo item in _nivoi)
                {
                    if (item.NivoID.Equals(nivoID))
                    {
                        nivo.listViewNivo.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    nivo.listViewNivo.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.Nivo _trenutni = (DB.Nivo)nivo.listViewNivo.SelectedItem;

                ObservableCollection<DB.Nivo> _nivoi = (ObservableCollection<DB.Nivo>)nivo.listViewNivo.ItemsSource;

                if (!_nivoi.Count.Equals(0))
                {
                    nivo.listViewNivo.ItemsSource = dBProksi.OsveziNivo(_nivoi);

                    if (_trenutni != null)
                    {
                        SelektujNivo(_trenutni.NivoID);
                    }
                }
                gridNivo.DataContext = (DB.Nivo)nivo.listViewNivo.SelectedItem;
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
                    gridNivo.DataContext = null;

                    stanje = App.Stanje.Unos;
                    nivo.UStanje(App.Stanje.Detaljno);
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
                    nivo.UStanje(App.Stanje.Detaljno);
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

                    nivo.UStanje(App.Stanje.Detaljno);
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
