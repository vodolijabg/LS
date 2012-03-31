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
    /// Interaction logic for Test.xaml
    /// </summary>
    public partial class Test : Window
    {
        public Test()
        {
            InitializeComponent();
        }

        public void Povecaj(int i)
        {
            textBlock1.Text = i.ToString();
            this.Refresh();
        }

        private delegate void UpdateProgressBarDelegate(System.Windows.DependencyProperty dp, Object value);


        private void button1_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show(Helper.DajStringSaVelikimPocetnimSlovom("OLIVER BRADONJIC"));


        }
    }
}
