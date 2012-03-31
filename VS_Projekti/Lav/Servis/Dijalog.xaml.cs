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

namespace Servis
{
    /// <summary>
    /// Interaction logic for Dialog.xaml
    /// </summary>
    public partial class Dijalog : Window
    {
        public Dijalog(string naslov, string poruka)
        {
            InitializeComponent();
            this.Title = naslov;
            textBlockPoruka.Text = poruka;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
