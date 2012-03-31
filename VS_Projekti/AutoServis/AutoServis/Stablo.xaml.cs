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
using System.ComponentModel;

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for Stablo.xaml
    /// </summary>
    public partial class Stablo : PageFunction<String>
    {
        Baza.LavAutoDataContext LavAutoDataContext = null;
        Baza.TipAutomobila BazaTipAutomobila = null;
        IEnumerable<Baza.Stablo> BazaStablo = null;
        IEnumerable<Baza.Artikal> BazaArtikal = null;
        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        public Stablo()
        {
            InitializeComponent();
        }

        public Stablo(Baza.TipAutomobila tipAutomobila)
            : this()
        {
            this.WindowTitle = tipAutomobila.ModelAutomobila.OpisTabela.Opis + " - " +
                                    tipAutomobila.OpisTabela.Opis;

            BazaTipAutomobila = tipAutomobila;

            DajListuStablo();

        }

        private void DajListuStablo()
        {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            IQueryable<Baza.Stablo> _upit = (from s in LavAutoDataContext.Stablos
                                              join vas in LavAutoDataContext.VezaArtikalStablos
                                              on s.Stablo_ID equals vas.Stablo_ID
                                              join u in LavAutoDataContext.Ugradnjas
                                              on vas.Artikal_ID equals u.Artikal_ID
                                              where u.TipAutomobila_ID == BazaTipAutomobila.TipAutomobila_ID
                                              && s.TipCvora_ID==1
                                              && s.Nivo > 1
                                              select s).Distinct().OrderBy(o => o.Opis_ID);


            try
            {
                BazaStablo = _upit.ToList();

                foreach (Baza.Stablo _stablo in BazaStablo.Where(f => f.Nivo == 2))
                {
                    TreeViewItem _treeViewItem = new TreeViewItem();
                    _treeViewItem.Tag = _stablo;
                    _treeViewItem.Header = _stablo.OpisTabela.Opis;
                    treeViewStablo.Items.Add(_treeViewItem);

                    //ako ima dece dodaj "*" da bi se prikazao expender
                    foreach (Baza.Stablo _stabloDeca in BazaStablo.Where(f => f.Nivo == _stablo.Nivo + 1))
                    {
                        if (_stabloDeca.Roditelj_ID == _stablo.Stablo_ID)
                        {
                            _treeViewItem.Items.Add("*");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju stabla", MessageBoxButton.OK, MessageBoxImage.Error);
            }

        }

        private void treeViewStablo_Expanded(object sender, RoutedEventArgs e)
        {
            TreeViewItem _item = (TreeViewItem)e.OriginalSource;
            _item.Items.Clear();

            if (_item.Tag is Baza.Stablo)
            {
                Baza.Stablo _stablo = (Baza.Stablo)_item.Tag;

                try
                {
                    foreach (Baza.Stablo nivo2 in BazaStablo.Where(f => f.Roditelj_ID == _stablo.Stablo_ID))
                    {
                        TreeViewItem _itemNovi = new TreeViewItem();
                        _itemNovi.Tag = nivo2;
                        _itemNovi.Header = nivo2.OpisTabela.Opis;
                        
                        _item.Items.Add(_itemNovi);

                        foreach (Baza.Stablo nivo3 in BazaStablo.Where(f => f.Nivo == nivo2.Nivo + 1))
                        {
                            if (nivo3.Roditelj_ID == nivo2.Stablo_ID)
                            {
                                _itemNovi.Items.Add("*");
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void treeViewStablo_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Baza.Stablo _stablo;
            try
            {
                TreeViewItem _treeViewItem = (TreeViewItem)treeViewStablo.SelectedItem;
                _stablo = (Baza.Stablo)_treeViewItem.Tag;
            }
            catch (Exception)
            {
                return;
            }

            this.Cursor = Cursors.Wait;

            try
            {
            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            BazaArtikal = (from vas in LavAutoDataContext.VezaArtikalStablos
                           join a in LavAutoDataContext.Artikals
                           on vas.Artikal_ID equals a.Artikal_ID
                           join u in LavAutoDataContext.Ugradnjas
                           on a.Artikal_ID equals u.Artikal_ID
                           where u.TipAutomobila_ID == BazaTipAutomobila.TipAutomobila_ID
                           && vas.Stablo_ID == _stablo.Stablo_ID

                           select a).Distinct();
            

                ObservableCollection<Baza.Artikal> _lista = new ObservableCollection<Baza.Artikal>();

                foreach (Baza.Artikal item in BazaArtikal)
                {
                    bool postoji = false;

                    foreach (Baza.Artikal item1 in _lista)
                    {
                        if (item.Opis_ID == item1.Opis_ID && item.Proizvodjac_ID==item1.Proizvodjac_ID)
                            postoji = true;
                    }

                    if (!postoji)
                        _lista.Add(item);
                }
                listBoxListaProizvoda.ItemsSource = _lista;

                ICollectionView view = CollectionViewSource.GetDefaultView(listBoxListaProizvoda.ItemsSource);
                view.GroupDescriptions.Add(new PropertyGroupDescription("Proizvodjac.Naziv"));

                view.SortDescriptions.Add(
                new SortDescription("Proizvodjac.Naziv", ListSortDirection.Ascending));
            }
            catch (Exception ex)
            {
                this.Cursor = Cursors.Arrow;

                MessageBox.Show(ex.Message, "Greška pri čitanju ugradnje iz stabla", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            this.Cursor = Cursors.Arrow;

        }

        private void Hyperlink_Click(object sender, RoutedEventArgs e)
        {
            Hyperlink _h = (Hyperlink)sender;
            //MessageBox.Show(_h.Tag.ToString());

            ObservableCollection<Baza.Artikal> _lista = new ObservableCollection<Baza.Artikal>();

            foreach (Baza.Artikal item in BazaArtikal)
            {
                if (item.Proizvodjac.Naziv == _h.Tag.ToString())
                {
                    _lista.Add(item);
                }
            }

            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Artikal(_lista));

        }

        private void listBoxListaProizvoda_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            Baza.Artikal _trenutni = (Baza.Artikal)listBoxListaProizvoda.SelectedItem;
            //MessageBox.Show(_trenutni.Proizvodjac.Naziv + "-" + _trenutni.OpisTabela.Opis);

            ObservableCollection<Baza.Artikal> _lista = new ObservableCollection<Baza.Artikal>();

            foreach (Baza.Artikal item in BazaArtikal)
            {
                if (item.Proizvodjac_ID == _trenutni.Proizvodjac_ID && item.Opis_ID == _trenutni.Opis_ID)
                {
                    _lista.Add(item);
                }
            }

            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new Artikal(_lista));

        }
    }
}
