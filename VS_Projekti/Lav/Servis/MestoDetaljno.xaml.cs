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

using System.Data.Linq;



namespace Servis
{
    /// <summary>
    /// Interaction logic for MestoDetaljno.xaml
    /// </summary>
    public partial class MestoDetaljno : Window
    {
        App.Stanje stanje;

        DB.DBProksi dBProksi;
        Servis.Mesto mesto;


        public MestoDetaljno(Servis.Mesto mesto, bool izmeniTrenutni)
        {
            InitializeComponent();

            dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);
            this.mesto = mesto;

            if (izmeniTrenutni)
            {
                gridMesto.DataContext = (DB.Mesto)this.mesto.listViewMesto.SelectedItem;
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
                        DB.Mesto _mesto = new DB.Mesto
                        {
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                            PozivniBroj = textBoxPozivniBroj.Text.Trim() == "" ? null : textBoxPozivniBroj.Text.Trim(),
                            PostanskiBroj = textBoxPostanskiBroj.Text.Trim() == "" ? null : textBoxPostanskiBroj.Text.Trim(),
                        };

                        dBProksi.UnesiMesto(_mesto);

                        ObservableCollection<DB.Mesto> _mesta = (ObservableCollection<DB.Mesto>)mesto.listViewMesto.ItemsSource;
                        _mesta.Add(_mesto);
                        mesto.listViewMesto.SelectedItem = _mesto;

                        stanje = App.Stanje.Izmena;
                    }
                    else //if (stanje == App.Stanje.Izmena)
                    {
                        DB.Mesto _mesto = new DB.Mesto
                        {
                            MestoID = Convert.ToInt32(textBoxID.Text),
                            Sifra = textBoxSifra.Text.Trim() == "" ? null : textBoxSifra.Text.Trim(),
                            Naziv = textBoxNaziv.Text.Trim(),
                            PozivniBroj = textBoxPozivniBroj.Text.Trim() == "" ? null : textBoxPozivniBroj.Text.Trim(),
                            PostanskiBroj = textBoxPostanskiBroj.Text.Trim() == "" ? null : textBoxPostanskiBroj.Text.Trim(),
                        };

                        dBProksi.IzmeniMesto(_mesto, (DB.Mesto)gridMesto.DataContext);
                    }
                }

                return true;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void SelektujMesto(int mestoID)
        {
            ObservableCollection<DB.Mesto> _mesta = (ObservableCollection<DB.Mesto>)mesto.listViewMesto.ItemsSource;
            bool _postoji = false;

            if (!mesto.listViewMesto.Items.Count.Equals(0))
            {
                foreach (DB.Mesto item in _mesta)
                {
                    if (item.MestoID.Equals(mestoID))
                    {
                        mesto.listViewMesto.SelectedItem = item;
                        _postoji = true;
                        break;
                    }
                }
                if (!_postoji)
                {
                    mesto.listViewMesto.SelectedIndex = 0;
                }
            }
        }

        private void Osvezi()
        {
            try
            {
                //samo osvezava podatke o trenutnim
                DB.Mesto _trenutni = (DB.Mesto)mesto.listViewMesto.SelectedItem;

                ObservableCollection<DB.Mesto> _mesta = (ObservableCollection<DB.Mesto>)mesto.listViewMesto.ItemsSource;

                if (!_mesta.Count.Equals(0))
                {
                    mesto.listViewMesto.ItemsSource = dBProksi.OsveziMesta(_mesta);

                    if (_trenutni != null)
                    {
                        SelektujMesto(_trenutni.MestoID);
                    }
                }
                gridMesto.DataContext = (DB.Mesto)mesto.listViewMesto.SelectedItem;
            }
            catch (Exception ex)
            {
                throw ex;
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
                    mesto.UStanje(App.Stanje.Detaljno);
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void buttonSacuvajINovi_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (Sacuvaj())
                {
                    Osvezi();
                    gridMesto.DataContext = null;

                    stanje = App.Stanje.Unos;
                    mesto.UStanje(App.Stanje.Detaljno);
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

                    mesto.UStanje(App.Stanje.Detaljno);
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
