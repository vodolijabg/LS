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
    /// Interaction logic for VoziloDetaljno.xaml
    /// </summary>
    public partial class VoziloDetaljno : Window
    {
        public VoziloDetaljno(DB.TipAutomobila tipAutomobila)
        {
            InitializeComponent();

            this.Title = tipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " - " + tipAutomobila.ModelAutomobila.OpisTabela.Opis + " - " +
                        tipAutomobila.OpisTabela.Opis;

            gridDetaljno.DataContext = tipAutomobila;
        }
    }
}
