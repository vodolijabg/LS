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
using System.Windows.Navigation;

namespace Servis
{
    /// <summary>
    /// Interaction logic for CarobnjakPonudaRadniNalog.xaml
    /// </summary>
    public partial class PonudaRadniNalogWizard : Window
    {
        Servis.PonudaDetaljno ponudaDetaljno;

        public PonudaRadniNalogWizard(Servis.PonudaDetaljno ponudaDetaljno)
        {
            InitializeComponent();

            this.ponudaDetaljno = ponudaDetaljno;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            NavigationService _navigation = this.framePonudaRadniNalogWizard.NavigationService;
            _navigation.Navigate(new PonudaRadniNalogWizard1(ponudaDetaljno));
        }

    }
}
