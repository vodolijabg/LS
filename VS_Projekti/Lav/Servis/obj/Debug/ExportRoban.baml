   M S B A M L   `   `   `  ����B  >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  5          Servis.ExportRoban   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      &localclr-namespace:Servis     5      -   5          N� Title$  ExportRoban��5       N� 	KeepAlive . .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5	      $
��Auto �6   6,   �� 5
      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      6     � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $	��300��6   60   �� 5      $
��Auto �6   6.   .+�5   
   �� $��0��6   $��0��6$   $��
10,10,5,10q�61   $��Center=�6E   Odaberi fajl6o   �� 5   
   -   5        $��textBoxFajl��5   B   $��1��6   $��0��6"   $��	5,10,5,10q�6/    �� 
IsReadOnly . 6i   �� 5   
   -   5        $��buttonOdaberiFajl��5   C   $��2��6   $��0��6!   $��5,10,100,10q�6.   $��28��6|   $��28��6�   .��5      �� $��Images/Open.ico��6    � ToolTip$ Odaberi fajl��6-   6F   5      �� 5   
   -   5        $��comboBoxZaImport��5   B   $��1��6   $��1��6#   $��3,3,10,3q�60   .@�5      ��  �� 
IsSelected . .��6-   Usluga65   5      �� 5   
   -   5        $��checkBoxSamoMarkirane��5   B   $��1��6   $��2��6#   $��3,3,10,3q�60    d� 	IsChecked$
 TrueY�6_   .��6q   Exportuje samo markirane6�   �� 5   
   -   5        $��checkBoxExportujZaglavlje��5   B   $��1��6   $��3��6#   $��3,3,10,3q�60   $ FalseY�6c   .��6v   Exportuje zaglavlje6�   �� 5!   
   $��0��6   $��4��6$   $��
10,10,5,10q�61   $	��Top=�6E   Exportuj fajl6m   �� 5"   
   -   5        $��listBoxRezultat��5"   B   $��1��6   $��4��6"   $��	5,10,5,10q�6/    �� SelectionMode$ Single=�6Z   6s   �� 5#   
   -   5        $��buttonExportujFajl��5#   C   $��2��6   $��4��6!   $��5,10,100,10q�6.   $��28��6   $��28��6�   $	��Top=�6�   .��5$      �� $��Images/Upload.ico��6   $ Export��6/   6B   5%      5&      5'      