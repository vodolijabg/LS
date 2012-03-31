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

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for TipAutomobila.xaml
    /// </summary>
    public partial class TipAutomobila : PageFunction<Object>
    {
        Baza.LavAutoDataContext LavAutoDataContext = null;
        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        //ako je False, na dupli klik se ulazi na drvo i dalje na delove za to vozilo.
        //ako je True, na dupli klik se selektovani vraca na formu koja je pozvala ...
        bool OdaberiVozilo = false;
        Baza.TipAutomobila PrikaziTipAutomobila = null;

        public TipAutomobila()
        {
            InitializeComponent();
        }

        public TipAutomobila(bool odaberiVozilo): this()
        {
            this.OdaberiVozilo = odaberiVozilo;
        }

        public TipAutomobila(bool odaberiVozilo, Baza.TipAutomobila tipAutomobila): this()
        {
            this.OdaberiVozilo = odaberiVozilo;
            PrikaziTipAutomobila = tipAutomobila;
        }

        private void DajListuProizvodjacAutomobila()
        {
            IQueryable<Baza.Proizvodjac> _upit = (from p in LavAutoDataContext.Proizvodjacs
                                                  join m in LavAutoDataContext.ModelAutomobilas on p.Proizvodjac_ID equals m.Proizvodjac_ID
                                                  select p).Distinct().OrderBy(w => w.Naziv);
            try
            {
                ObservableCollection<Baza.Proizvodjac> _lista = new ObservableCollection<Baza.Proizvodjac>(_upit.ToList());

                comboBoxProizvodjacAutomobilaLista.ItemsSource = _lista;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju proizvođača automobila", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            if (comboBoxProizvodjacAutomobilaLista.Items.Count > 0)
            {
                comboBoxProizvodjacAutomobilaLista.SelectedIndex = 0;
            }

        }

        private void PageFunction_Loaded(object sender, RoutedEventArgs e)
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);
            
            //zato sto se kod navigacije ovaj dogadjaj uvek okida pa ne mogu da zadrzim trenutno stanje forme
            if (PrvoOtvaranjeStrane)
            {
                PrvoOtvaranjeStrane = false;

                DajListuProizvodjacAutomobila();

                //ako sam usao sa servisne knjizice i ako je vec odabrano vozilo
                if ((OdaberiVozilo == true) && (PrikaziTipAutomobila != null))
                {
                    try
                    {
                        foreach (Baza.Proizvodjac item in comboBoxProizvodjacAutomobilaLista.Items)
                        {
                            if (item.Proizvodjac_ID.Equals(PrikaziTipAutomobila.ModelAutomobila.Proizvodjac_ID))
                            {
                                comboBoxProizvodjacAutomobilaLista.SelectedItem = item;
                                break;
                            }
                        }

                        foreach (Baza.ModelAutomobila item in comboBoxModelAutomobilaLista.Items)
                        {
                            if (item.ModelAutomobila_ID.Equals(PrikaziTipAutomobila.ModelAutomobila_ID))
                            {
                                comboBoxModelAutomobilaLista.SelectedItem = item;
                                break;
                            }
                        }

                        foreach (Baza.TipAutomobila item in listViewTipAutomobila.Items)
                        {
                            if (item.TipAutomobila_ID.Equals(PrikaziTipAutomobila.TipAutomobila_ID))
                            {
                                listViewTipAutomobila.SelectedItem = item;
                                break;
                            }
                        }

                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message, "Greška pri čitanju tipa automobila", MessageBoxButton.OK, MessageBoxImage.Error);
                    }                    
                    
                }
            }

            
        }

        private void comboBoxProizvodjacAutomobilaLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxProizvodjacAutomobilaLista.SelectedItem != null)
            {
                Baza.Proizvodjac _trenutniProizvodjac = (Baza.Proizvodjac)comboBoxProizvodjacAutomobilaLista.SelectedItem;

                IQueryable<Baza.ModelAutomobila> _upit = (from p in LavAutoDataContext.ModelAutomobilas
                                                          where p.Proizvodjac_ID == _trenutniProizvodjac.Proizvodjac_ID
                                                          select p).OrderBy(w => w.OpisTabela.Opis);
                try
                {
                    ObservableCollection<Baza.ModelAutomobila> _lista = new ObservableCollection<Baza.ModelAutomobila>(_upit.ToList());

                    comboBoxModelAutomobilaLista.ItemsSource = _lista;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju modela automobila", MessageBoxButton.OK, MessageBoxImage.Error);
                }

                if (comboBoxModelAutomobilaLista.Items.Count > 0)
                {
                    comboBoxModelAutomobilaLista.SelectedIndex = 0;
                } 
            }
        }

        private void comboBoxModelAutomobilaLista_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (comboBoxModelAutomobilaLista.SelectedItem != null)
            {
                Baza.ModelAutomobila _trenutniModel = (Baza.ModelAutomobila)comboBoxModelAutomobilaLista.SelectedItem;

                IQueryable<Baza.TipAutomobila> _upit = (from t in LavAutoDataContext.TipAutomobilas
                                                            where t.ModelAutomobila_ID == _trenutniModel.ModelAutomobila_ID
                                                        select t).OrderBy(w => w.OpisTabela.Opis);
                try
                {
                    ObservableCollection<Baza.TipAutomobila> _lista = new ObservableCollection<Baza.TipAutomobila>(_upit.ToList());
                    listViewTipAutomobila.ItemsSource = _lista;
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Greška pri čitanju tipova automobila", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private void buttonVoziloDetaljno_Click(object sender, RoutedEventArgs e)
        {
            Button _buttonVoziloDetaljno = (Button)sender;
            Baza.TipAutomobila _tipAutomobila = (Baza.TipAutomobila)_buttonVoziloDetaljno.Tag;

            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new TipAutomobilaDetaljno(_tipAutomobila));
        }

        private void listViewTipAutomobila_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OdaberiVozilo)
            {
                //vrati odabrani tip na stranu sa koje je pozvana
                Baza.TipAutomobila _trenutni = (Baza.TipAutomobila)listViewTipAutomobila.SelectedItem;
                OnReturn(new ReturnEventArgs<Object>(_trenutni));
                
            }
            else
            {
                this.Cursor = Cursors.Wait;

                Baza.TipAutomobila _trenutni = (Baza.TipAutomobila)listViewTipAutomobila.SelectedItem;

                //udji u drvo
                NavigationService _navigationService = NavigationService.GetNavigationService(this);
                _navigationService.Navigate(new Stablo(_trenutni));

                this.Cursor = Cursors.Arrow;
            }
        }
    }
}
