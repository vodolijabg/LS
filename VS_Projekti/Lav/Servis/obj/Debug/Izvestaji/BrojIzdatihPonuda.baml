   M S B A M L   `   `   `  ����g  cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms  5      
 Servis(   "Servis.Izvestaji.BrojIzdatihPonuda   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      crptSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms     5        N� Title$  BrojIzdatihPonuda��5       N� 	KeepAlive . .��5
      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6.   �� 5      $��* �6   6+   .+�5   
   �� $��0��6   $��0��6$   $��	10,3,10,3q�62   $��Vremenski opseg��6E   $��Center=�6\   6y   �� 5   
   $��0��6   $��1��6$   $��	10,3,10,3q�62   $��Sortiraj po��6E   $��Center=�6X   6u   (  "System.Windows.Controls.DatePicker  5   
   -   5        $��datePickerDatumOd��5   2   $��1��6   $��0��6%   $��3,3,3,3q�6K   6_     5   
   -   5        $��datePickerDatumDo��5   2   $��2��6   $��0��6%   $��3,3,3,3q�6K   6_   �� 5    
   -   5        $��comboBoxSortirajPo��5    A   $��1��6   $��1��6#   $��0,2,5,2q�60    �� SelectedIndex$ 0��6[   .@�5!      �� .��6   Šifra6"   �� 5"      .��6   	Nadimak6$   �� 5#      .��6   Broj ponuda6(   5$      �� 5&   
   -   5        $��checkBoxVeceOdNule��5&   A   $��2��6   $��1��6#   $��3,3,3,3q�60   $��Right=�6[    d� 	IsChecked$
 TrueY�6w   .��6�   >06�   �� 5(   
   -   5        $��buttonPrikazi��5(   @   $��3��6   $��0��6!   $��10,3,0,3q�6.    �� 	IsDefault . .��6�   
Prikaži6�   7  1System.Windows.Forms.Integration.WindowsFormsHost  5)   
   $��0��6   $��2��6+   $��5��68      Child. 5*      /   )Microsoft.Reporting.WinForms.ReportViewer  -   5        reportViewerIzvestaj��5*   ?   5+      5,      5-      