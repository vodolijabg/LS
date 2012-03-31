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
    /// Interaction logic for ArtikalDetaljno.xaml
    /// </summary>
    public partial class ArtikalDetaljno : Window
    {
        DB.DBProksi dBProksi;
        int artikalID;
        string brojZaPretragu;

        public ArtikalDetaljno(int artikalID, string brojZaPretragu)
        {
            InitializeComponent();

            this.artikalID = artikalID;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                dBProksi = new DB.DBProksi(Konfiguracija.KonekcioniString);

                listViewArtikal.ItemsSource = dBProksi.DajBrojeveZaArtikal(artikalID);

                
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, "Greška", MessageBoxButton.OK, MessageBoxImage.Error);
            }
        }

        private void listViewArtikal_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            //for (int i = 0; i < listViewArtikal.Items.Count; i++)
            //{
            //    //ovo radi samo za one redove koji se vide u listi. Ako lista ima scroll bar za donje redove ce vratiti null
            //    //radice i ako se skrol bar povuce do dole pa se onda ovo pozove na izvrsenje
                
            //    ListViewItem lv = (ListViewItem)listViewArtikal.ItemContainerGenerator.ContainerFromItem(listViewArtikal.Items[i]);
            //    lv.Background = Brushes.Black;
            //    //MessageBox.Show(lv.ToString());
            //}
        }
    }
}
