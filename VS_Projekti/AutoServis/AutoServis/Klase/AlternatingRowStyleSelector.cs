using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;

namespace AutoServis
{
    //svaki drugi red u ListBoxu da bude plav
    class AlternatingRowStyleSelector : StyleSelector
    {
        public Style DefaultStyle
        {
            get;
            set;
        }
        public Style AlternateStyle
        {
            get;
            set;
        }

        // Track the row index.
        private int i = 0;
        public override Style SelectStyle(object item, DependencyObject container)
        {
            // Reset the counter if this is the first item.
            ItemsControl ctrl = ItemsControl.ItemsControlFromItemContainer(container);
            if (item == ctrl.Items[0])
            {
                i = 0;
            }
            i++;
            // Choose between the two styles based on the current position.
            if (i % 2 == 1)
            {
                return DefaultStyle;
            }
            else
            {
                return AlternateStyle;
            }
        }
    }
}
