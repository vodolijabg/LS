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

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for TipAutomobilaDetaljno.xaml
    /// </summary>
    public partial class TipAutomobilaDetaljno : Page
    {
        //za navigaciju
        bool PrvoOtvaranjeStrane = true;

        public TipAutomobilaDetaljno()
        {
            InitializeComponent();
        }
        public TipAutomobilaDetaljno(Baza.TipAutomobila tipAutomobila): this()
        {
            this.WindowTitle = tipAutomobila.ModelAutomobila.Proizvodjac.Naziv + " - " + tipAutomobila.ModelAutomobila.OpisTabela.Opis + " - " +
                                    tipAutomobila.OpisTabela.Opis;

            gridDetaljno.DataContext = tipAutomobila;

        }
    }
}
