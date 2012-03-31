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
using System.ComponentModel;
using System.Collections.ObjectModel;

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for ArtikalDetaljno.xaml
    /// </summary>
    public partial class ArtikalDetaljno : PageFunction<Object>
    {
        Baza.LavAutoDataContext LavAutoDataContext = null;
        Baza.Artikal BazaArtikal = null;
        //za navigaciju
        //bool PrvoOtvaranjeStrane = true;
        bool OdaberiCenovnikDobavljaca = false;

        public ArtikalDetaljno()
        {
            InitializeComponent();
        }

        public ArtikalDetaljno(Baza.Artikal artikal, bool odaberiCenovnikDobavljaca):this()
        {
            this.BazaArtikal = artikal;
            this.OdaberiCenovnikDobavljaca = odaberiCenovnikDobavljaca;

            LavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);

            

            this.WindowTitle = artikal.OpisTabela.Opis;

            try
            {
                var _vezaArtikalKriterijumVrednost = (from v in LavAutoDataContext.VezaArtikalKriterijums
                                                      join k in LavAutoDataContext.Kriterijums
                                                      on v.Kriterijum_ID equals k.Kriterijum_ID
                                                      where v.Vrednost != null && v.Artikal_ID == artikal.Artikal_ID
                                                      select new
                                                      {
                                                          Kriterijum = k.OpisTabela.Opis,
                                                          Vrednost = v.Vrednost
                                                      }).Distinct().OrderBy(o => o.Kriterijum);

                var _vezaArtikalKriterijumOpis = (from v in LavAutoDataContext.VezaArtikalKriterijums
                                                  join k in LavAutoDataContext.Kriterijums
                                                  on v.Kriterijum_ID equals k.Kriterijum_ID
                                                  where v.Opis_ID != null && v.Artikal_ID == artikal.Artikal_ID
                                                  select new
                                                  {
                                                      Kriterijum = k.OpisTabela.Opis,
                                                      Vrednost = v.OpisTabela.Opis
                                                  }).Distinct().OrderBy(o => o.Kriterijum);

                var _kriterijum = _vezaArtikalKriterijumOpis.Union(_vezaArtikalKriterijumVrednost);

                listBoxInformacijeOProizvodu.ItemsSource = _kriterijum;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju kriterijuma", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                var _brojevi = (from v in LavAutoDataContext.VezaArtikalBrojZaPretragus
                                join k in LavAutoDataContext.VrstaBrojaZaPretragus
                                on v.VrstaBrojaZaPretragu_ID equals k.VrstaBrojaZaPretragu_ID
                                join p in LavAutoDataContext.Proizvodjacs 
                                on v.Proizvodjac_ID equals p.Proizvodjac_ID into temp
                                from t in temp.DefaultIfEmpty()
                                where v.Artikal_ID == artikal.Artikal_ID && v.VrstaBrojaZaPretragu_ID==3
                                select new
                                {
                                    Broj = v.BrojZaPrikazivanje,
                                    Proizvodjac = t.Naziv,
                                    VrstaBroja = k.Naziv
                                }).Distinct().OrderBy(o => o.Proizvodjac);



                listBoxBrojevi.ItemsSource = _brojevi;

                //ICollectionView view = CollectionViewSource.GetDefaultView(listBoxBrojevi.ItemsSource);
                //view.GroupDescriptions.Add(new PropertyGroupDescription("VrstaBroja"));

                //view.SortDescriptions.Add(
                //new SortDescription("VrstaBroja", ListSortDirection.Ascending));

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju brojeva", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                IQueryable<Baza.TipAutomobila> _ugradnja = (from t in LavAutoDataContext.TipAutomobilas
                                                            join u in LavAutoDataContext.Ugradnjas
                                                            on t.TipAutomobila_ID equals u.TipAutomobila_ID
                                                            where u.Artikal_ID == artikal.Artikal_ID
                                                            select t).OrderBy(w => w.ModelAutomobila.Proizvodjac.Naziv);

                ObservableCollection<Baza.TipAutomobila> _lista = new ObservableCollection<Baza.TipAutomobila>(_ugradnja.ToList());
                listViewTipAutomobila.ItemsSource = _lista;

                if (!listViewTipAutomobila.Items.Count.Equals(0))
                {
                    listViewTipAutomobila.SelectedIndex = 0;
                }


            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju tipova automobila", MessageBoxButton.OK, MessageBoxImage.Error);
            }

            try
            {
                var _dobavljacKorisnikPrograma = (from v in LavAutoDataContext.VezaArtikalDobavljacs
                                                  join a in LavAutoDataContext.Artikals
                                                  on v.Artikal_ID equals a.Artikal_ID
                                                  join k in LavAutoDataContext.KorisnikProgramas
                                                  on v.KorisnikPrograma_ID equals k.KorisnikPrograma_ID
                                                  where v.Artikal_ID == artikal.Artikal_ID
                                                  select new
                                                  {
                                                      v.VezaArtikalDobavljac_ID,
                                                      artikal.Artikal_ID,
                                                      Dobavljac = k.Naziv,
                                                      v.CenaBezPoreza,
                                                      a.PoreskaStopa_ID,
                                                      PoreskaStopaVrednost = a.PoreskaStopa.VrednostProcenata,
                                                      CenaSaPorezom = v.CenaBezPoreza * ((Convert.ToDecimal(a.PoreskaStopa.VrednostProcenata) / 100) + 1),
                                                      v.DatumAzuriranja,
                                                      v.KolicinaNaStanju
                                                  }).Distinct();

                var _dobavljacPoslovniPartner = (from v in LavAutoDataContext.VezaArtikalDobavljacs
                                                 join a in LavAutoDataContext.Artikals
                                                 on v.Artikal_ID equals a.Artikal_ID
                                                 join p in LavAutoDataContext.PoslovniPartners
                                                 on v.PoslovniPartner_ID equals p.PoslovniPartner_ID
                                                 where v.Artikal_ID == artikal.Artikal_ID
                                                 select new
                                                 {
                                                     v.VezaArtikalDobavljac_ID,
                                                     artikal.Artikal_ID,
                                                     Dobavljac = p.SkracenNaziv,
                                                     v.CenaBezPoreza,
                                                     a.PoreskaStopa_ID,
                                                     PoreskaStopaVrednost = a.PoreskaStopa.VrednostProcenata,
                                                     CenaSaPorezom = v.CenaBezPoreza * ((Convert.ToDecimal(a.PoreskaStopa.VrednostProcenata) / 100) + 1),
                                                     v.DatumAzuriranja,
                                                     v.KolicinaNaStanju
                                                 }).Distinct().OrderBy(o => o.CenaBezPoreza);

                var _dobavljaci = _dobavljacKorisnikPrograma.Union(_dobavljacPoslovniPartner);

                

                
                listViewDobavljaci.ItemsSource = _dobavljaci;

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška pri čitanju kriterijuma", MessageBoxButton.OK, MessageBoxImage.Error);
            }
                                   
        }

        private void buttonVoziloDetaljno_Click(object sender, RoutedEventArgs e)
        {
            Button _buttonVoziloDetaljno = (Button)sender;
            Baza.TipAutomobila _tipAutomobila = (Baza.TipAutomobila)_buttonVoziloDetaljno.Tag;

            NavigationService _navigationService = NavigationService.GetNavigationService(this);
            _navigationService.Navigate(new TipAutomobilaDetaljno(_tipAutomobila));

        }

        private void listViewTipAutomobila_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            try
            {
                Baza.TipAutomobila _trenutni = (Baza.TipAutomobila)listViewTipAutomobila.SelectedItem;

                var _vezaUgradnjaKriterijumVrednost = (from v in LavAutoDataContext.VezaUgradnjaKriterijums
                                                       join k in LavAutoDataContext.Kriterijums
                                                       on v.Kriterijum_ID equals k.Kriterijum_ID
                                                       where v.Vrednost != null && v.UgradnjaArtikal_ID == BazaArtikal.Artikal_ID
                                                       && v.UgradnjaTipAutomobila_ID == _trenutni.TipAutomobila_ID
                                                       select new
                                                       {
                                                           Opis = "Uslov ugradnje",
                                                           Kriterijum = k.OpisTabela.Opis,
                                                           Vrednost = v.Vrednost
                                                       }).Distinct().OrderBy(o => o.Kriterijum);

                var _vezaUgradnjaKriterijumOpis = (from v in LavAutoDataContext.VezaUgradnjaKriterijums
                                                   join k in LavAutoDataContext.Kriterijums
                                                   on v.Kriterijum_ID equals k.Kriterijum_ID
                                                   where v.Opis_ID != null && v.UgradnjaArtikal_ID == BazaArtikal.Artikal_ID
                                                   && v.UgradnjaTipAutomobila_ID == _trenutni.TipAutomobila_ID
                                                   select new
                                                   {
                                                       Opis = "Uslov ugradnje",
                                                       Kriterijum = k.OpisTabela.Opis,
                                                       Vrednost = v.OpisTabela.Opis
                                                   }).Distinct().OrderBy(o => o.Kriterijum);

                var _kriterijum = _vezaUgradnjaKriterijumOpis.Union(_vezaUgradnjaKriterijumVrednost);

                listBoxUslovUgradnje.ItemsSource = _kriterijum;

                ICollectionView view = CollectionViewSource.GetDefaultView(listBoxUslovUgradnje.ItemsSource);
                view.GroupDescriptions.Add(new PropertyGroupDescription("Opis"));

                //view.SortDescriptions.Add(
                //new SortDescription("VrstaBroja", ListSortDirection.Ascending));
            }
            catch (Exception ex)
            {

                MessageBox.Show(ex.Message, "Greška pri čitanju kriterijuma ugradnje", MessageBoxButton.OK, MessageBoxImage.Error);
            } 
        }

        private void listViewDobavljaci_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (OdaberiCenovnikDobavljaca)
            {
                OnReturn(new ReturnEventArgs<Object>(listViewDobavljaci.SelectedItem));
            }
        }

        private void TextBox_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            TextBox _trenutni = (TextBox)sender;
            _trenutni.Select(0, _trenutni.Text.Length);
        }
        
    }
}
