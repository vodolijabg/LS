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
    /// Interaction logic for PonudaWizard.xaml
    /// </summary>
    public partial class PonudaWizard : Window
    {
        public Ponuda ponuda;

        public PonudaWizard(Ponuda ponuda)
        {
            InitializeComponent();

            this.ponuda = ponuda;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            App.fizickoLicePonudaWizard = new DB.FizickoLice();
            App.nacinZahtevaZaPonuduWizard = null;

            NavigationService _navigation = this.framePonudaWizard.NavigationService;
            _navigation.Navigate(new PonudaWizard1(this));
        }
    }
}
