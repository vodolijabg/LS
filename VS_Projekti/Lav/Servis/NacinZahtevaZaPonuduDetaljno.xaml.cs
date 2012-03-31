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
    /// Interaction logic for NacinZahtevaZaPonuduDetaljno.xaml
    /// </summary>
    public partial class NacinZahtevaZaPonuduDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.NacinZahtevaZaPonudu nacinZahtevaZaPonudu;

        public NacinZahtevaZaPonuduDetaljno(Servis.NacinZahtevaZaPonudu nacinZahtevaZaPonudu, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.nacinZahtevaZaPonudu = nacinZahtevaZaPonudu;

            if (izmeniTrenutni)
            {
                gridNacinZahtevaZaPonudu.DataContext = (DB.NacinZahtevaZaPonudu)this.nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.SelectedItem;
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
                        DB.NacinZahtevaZaPonudu _nacinZahtevaZaPonudu = new DB.NacinZahtevaZaPonudu
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim()
                        };

                        dBProksi.UnesiNacinZahtevaZaPonudu(_nacinZahtevaZaPonudu);

                        ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = (ObservableCollection<DB.NacinZahtevaZaPonudu>)nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.ItemsSource;
                        _naciniZahtevaZaPonudu.Add(_nacinZahtevaZaPonudu);
                        nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.SelectedItem = _nacinZahtevaZaPonudu;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.NacinZahtevaZaPonudu _nacinZahtevaZaPonudu = new DB.NacinZahtevaZaPonudu
                        {
                            NacinZahtevaZaPonuduID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                        };

                        dBProksi.IzmeniNacinZahtevaZaPonudu(_nacinZahtevaZaPonudu, (DB.NacinZahtevaZaPonudu)gridNacinZahtevaZaPonudu.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujNacinZahtevaZaPonudu(int nacinZahtevaZaPonuduID)
        {
            ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = (ObservableCollection<DB.NacinZahtevaZaPonudu>)nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.ItemsSource;
            bool _postoji = false;

            if (!nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.Items.Count.Equals(0))
            {
                foreach (DB.NacinZahtevaZaPonudu item in _naciniZahtevaZaPonudu)
                {
                    if (item.NacinZahtevaZaPonuduID.Equals(nacinZahtevaZaPonuduID))
                    {
                        nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.NacinZahtevaZaPonudu _trenutni = (DB.NacinZahtevaZaPonudu)nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.SelectedItem;

                ObservableCollection<DB.NacinZahtevaZaPonudu> _naciniZahtevaZaPonudu = (ObservableCollection<DB.NacinZahtevaZaPonudu>)nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.ItemsSource;

                if (!_naciniZahtevaZaPonudu.Count.Equals(0))
                {
                    nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.ItemsSource = dBProksi.OsveziNacinZahtevaZaPonudu(_naciniZahtevaZaPonudu);

                    if (_trenutni != null)
                    {
                        SelektujNacinZahtevaZaPonudu(_trenutni.NacinZahtevaZaPonuduID);
                    }
                }
                gridNacinZahtevaZaPonudu.DataContext = (DB.NacinZahtevaZaPonudu)nacinZahtevaZaPonudu.listViewNacinZahtevaZaPonudu.SelectedItem;
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
                    gridNacinZahtevaZaPonudu.DataContext = null;

                    stanje = App.Stanje.Unos;
                    nacinZahtevaZaPonudu.UStanje(App.Stanje.Detaljno);
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
                    nacinZahtevaZaPonudu.UStanje(App.Stanje.Detaljno);
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

                    nacinZahtevaZaPonudu.UStanje(App.Stanje.Detaljno);
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
