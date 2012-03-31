using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;

namespace AutoServis
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        //svi koriste ovu instancu za pristup bazi -- JER JE ZEZNUTO BAS
        //private static Baza.LavAutoDataContext lavAutoDataContext = new Baza.LavAutoDataContext(AutoServis.Properties.Settings.Default.KonekcioniString);
        //public static Baza.LavAutoDataContext LavAutoDataContext
        //{
        //    get { return lavAutoDataContext; }
        //}

        public enum Stanje { Unos, Izmena, Brisanje, Detaljno, Osnovno, NeuspesnaKonekcija, NePostojiZaglavlje, IzgasiSve }

        public static bool ImaLiGreskiValidacijeTextBox(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                TextBox element = child as TextBox;

                if (element == null)
                    continue;

                if (Validation.GetHasError(element))
                    return true;

                // Check the children of this object for errors.
                ImaLiGreskiValidacijeTextBox(element);
            }
            return false;
        }

        public static bool ImaLiGreskiValidacijeComboBox(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                ComboBox element = child as ComboBox;

                if (element == null)
                    continue;

                if (Validation.GetHasError(element))
                    return true;

                // Check the children of this object for errors.
                ImaLiGreskiValidacijeComboBox(element);
            }
            return false;
        }

        public static bool ImaLiGreskiValidacijeListBox(DependencyObject obj)
        {
            foreach (object child in LogicalTreeHelper.GetChildren(obj))
            {
                ListBoxItem element = child as ListBoxItem;

                if (element == null)
                    continue;

                if (Validation.GetHasError(element))
                    return true;

                // Check the children of this object for errors.
                ImaLiGreskiValidacijeListBox(element);
            }
            return false;
        }

        //public static bool ImaLiGreskiValidacijeTextBox(DependencyObject obj)
        //{
        //    foreach (object child in LogicalTreeHelper.GetChildren(obj))
        //    {
        //        TextBox element = child as TextBox;

        //        if (element == null)
        //            continue;

        //        if (Validation.GetHasError(element))
        //            return true;

        //        // Check the children of this object for errors.
        //        ImaLiGreskiValidacijeTextBox(element);
        //    }
        //    return false;
        //}

        
    }
}
