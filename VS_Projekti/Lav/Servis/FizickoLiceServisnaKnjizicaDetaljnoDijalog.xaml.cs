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
    /// Interaction logic for FizickoLiceServisnaKnjizicaDetaljnoDijalog.xaml
    /// </summary>
    public partial class FizickoLiceServisnaKnjizicaDetaljnoDijalog : Window
    {
        FizickoLiceServisnaKnjizicaDetaljno fizickoLiceServisnaKnjizicaDetaljno;
        ObservableCollection<DB.FizickoLice> fizickoLiceLista;

        public FizickoLiceServisnaKnjizicaDetaljnoDijalog(FizickoLiceServisnaKnjizicaDetaljno fizickoLiceServisnaKnjizicaDetaljno, ObservableCollection<DB.FizickoLice> fizickoLiceLista)
        {
            InitializeComponent();

            this.fizickoLiceServisnaKnjizicaDetaljno = fizickoLiceServisnaKnjizicaDetaljno;
            this.fizickoLiceLista = fizickoLiceLista;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //checkBoxFizickoLice.Content = fizickoLice.Ime + " " + fizickoLice.Prezime + "[" + fizickoLice.Telefon1 + "]";
            listViewFizickoLice.ItemsSource = fizickoLiceLista;
        }

        private void listViewFizickoLice_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            DB.FizickoLice _fizickoLice = (DB.FizickoLice)listViewFizickoLice.SelectedItem;

            if (_fizickoLice != null)
            {
                listViewServisnaKnjizica.ItemsSource = new ObservableCollection<DB.ServisnaKnjizica>(_fizickoLice.ServisnaKnjizicas.ToList());
            }            
        }

        private void buttonZavrsi_Click(object sender, RoutedEventArgs e)
        {
            DB.FizickoLice fizickoLice = (DB.FizickoLice)listViewFizickoLice.SelectedItem;
            DB.ServisnaKnjizica servisnaKnjzica = (DB.ServisnaKnjizica)listViewServisnaKnjizica.SelectedItem;

            if (fizickoLiceLista != null)
            {
                fizickoLiceServisnaKnjizicaDetaljno.gridFizickoLice.DataContext = fizickoLice;
            }

            if (servisnaKnjzica != null)
            {
                fizickoLiceServisnaKnjizicaDetaljno.gridServisnaKnjizica.DataContext = servisnaKnjzica;
            }

            this.Close();
        }
    }
}
