   M S B A M L   `   `   `  ����O  Kmscorlib, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5
      E AAutoServis, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null'clr-namespace:AutoServis
AutoServis 5         AutoServis.Usluga   5      W SPresentationCore, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     8x,http://schemas.microsoft.com/winfx/2006/xaml    5	      4sys&clr-namespace:System;assembly=mscorlib    5
      (localclr-namespace:AutoServis    5      -   5          N� Title$  Usluga��5       N� WindowTitle$ Usluga��6    N� 	KeepAlive .  L� RemoveFromJournal . .��5   
   �  � RowDefinitions 5      �� 5      $
��Auto �6    60   �� 5      $
��Auto �6    60   �� 5      6"    � ColumnDefinitions 5      �� 5      $	��200 �6#   61   �� 5      $
��Auto �6#   62   �� 5      6&   .+�5!      �� $��0��6   $��0��6"   $��3��62      Pozadina#��[  6F   $��5,5,5,0q�6h   ._�5#      9� $��Pretraga��6    9� 
IsExpanded . $��12(�6;   .��5$      �  5&      �� 5'      $
��Auto �6+   6:   �� 5(      6-    5+      �� 5,      6+   �� 5-      6*   .+�50      	� -   5        $%��radioButtonPretragaNosilacGrupe��50   ?   $��0��6"   $��0��6/   $��4q�6f   .��6�   Nosilac Grupe6�   �� 51      -   5        $!��textBoxPretragaNosilacGrupe��51   ;   $��0��6   $��1��6+   $��4q�6^   {�.  6}   	� 53      -   5        $#��radioButtonPretragaPrikaziSve��53   ?   $��1��6"   $��0��6/   $��4q�6d    d� 	IsChecked$
 TrueY�6�   .��6�   Prikaži sve6�   �� 55      -   5        $��buttonNadji��55   \   $��1��6   $��1��6*   $��23��6:   $��23��6F   $��4q�6Q   $
��Left=�6�   .��6�   ...6�   57      58      59      �� 5;      $��1��6   $��0��6"   $��3��62   #��[  6F   $��5,5,5,0q�6h   ._�5<      �  5=      �� 5>      $
��Auto �6'   66   �� 5?      6*   .+�5A      �� $��0��6   $��0��6)   $��4q�69   $��Odaberi Vrstu Usluge��6D   $
��Bold"�6`   6t   � 5C      $��1��6   $��2��6'    5D      �� 5E      6.   �� 5F      $
��Auto �6+   6:   .+�5I      �� -   5        $��comboBoxVrstaUslugeLista��5I   M   $��0��6   $��0��6/   $��3,3,3,3q�6<    w� TextPath$ Naziv��6m   ��5J      �� 5K      .R�5L   "   �� ��5M   "   �� 5N   &   	 �� Path$	 Naziv�6.   
 �� Mode$
 OneWay=�6;    �� ValidationRules 5O   *   $  AutoServis.ObavezanPodatakRule  5P   .   6J   5R   '   5T   #   5U      ��5X      , &AutoServis.AlternatingRowStyleSelector  5Y        ObojiSvakiDrugiRed   AlternateStyle# [ 6@   5c      �� 5e      -   5        $��buttonVrstaUsluge��5e   V   $��1��6   $��0��6-   $��3,3,3,3q�6:   $��23��6K   $y�	Collapsed=�6�   .��6�   ...6�   5g      5i      5k      �� 5m      $��0��6   $��2��6%   #��[  62   ._�5n      �� -   5        $��listBoxLista��5n   +   $��3,3,3,3q�6   $ NosilacGrupe.Naziv��6?    �� 
GroupStyle 5o      (  "System.Windows.Controls.GroupStyle  5p         HeaderTemplate 5q      �� 5r      .R�5s   "   �� $
��Bold"�5t   )   
�������
���   �$��0,0,0,0q�5v   )    �� Padding$ 2q�6:   ��5s   ,   �� $
	 Name�5w   #   5x      5{      ��5}      �� 5~      .R�5      �� ��6(   �� $	 
Nivo.Naziv�6K   5�      ��5�        5�      # [ 6<   5�      5�      �� 5�      $��1��6   $��2��6+   $��3��68   $��Stretch=�6B   $��Center=�6^   6~   �� 5�      $��2��6   $��2��6&   #��[  63   ._�5�      � -	   5        $��gridDetaljno��5�       � DataContext 6+   ��  �� ElementName$ listBoxLista��$	 SelectedItem� 5�      �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68   �� 5�      $
��Auto �6(   68    5�      �� 5�      $
��Auto �6+   6:   �� 5�      6.   .+�5�      �� $��0��6    $��0��60   $��3,3,10,3q�6=   $��Center=�6O   ID:6o   �� 5�      $��0��6    $��1��60   $��3,3,10,3q�6=   $��Center=�6O   	Šifra:6r   �� 5�      $��0��6    $��3��60   $��3,3,10,3q�6=   $��Center=�6O   Nivo:6q   �� 5�      $��0��6    $��4��60   $��3,3,10,3q�6=   $��Center=�6O   Nosilac Grupe:6z   �� 5�      $��0��6    $��5��60   $��3,3,10,3q�6=   $��Center=�6O   Norma (Sati,Minuta):6�   �� 5�      $��0��6    $��6��60   $��3,3,10,3q�6=   $��Center=�6O   Bod:6p   �� 5�      $��0��6    $��7��60   $��3,3,10,3q�6=   $��Center=�6O   Broj Bodova:6x   �� 5�      $��0��6    $��8��60   $��3,3,10,3q�6=   $��Center=�6O   Poreska Stopa:6z   �� 5�      -
   5        $��	textBoxID��5�   L   $��1��6   $��0��6.   $��3,3,3,3q�6;   ��6]   �� $	 	Usluga_ID�$
 TwoWay=� �� UpdateSourceTrigger$ Explicit=�6�   �� 5�      -   5        $��textBoxSifra��5�   L   $��1��6   $��1��6.   $��3,3,3,3q�6;    �� 	MaxLength$ 50��6a   .��6p   ��5�      �� 5�      $	 Sifra�6&   $ Explicit=�63   $
 TwoWay=�6R    5�   "     5�   &   6B   5�      .��5�   (   5�      � 5�      $��1��6   $��3��6+    5�      �� 5�      6.   �� 5�      $
��Auto �6+   6:   .+�5�      �� -   5        $��comboBoxNivoLista��5�   M   $��0��6   $��0��6/   $��3,3,3,3q�6<   $ Naziv��6f   ��5�      �� 5�      .R�5�   "   �� ��5�   "   �� 5�   &   $	 Naziv�6.   $
 OneWay=�6;    5�   *     5�   .   6J   5�   '   5�   #   5�      ��5�        5�      # [ 6@    �� SelectedItem 5�      �� 5�      $ listBoxLista��6&   $	 SelectedItem.Nivo�6A   $
 TwoWay=�6Z   $ Explicit=�6h    5�   "   )  #AutoServis.ObavezanPodatakListaRule  5�   &   6G   5�      5�      �� 5�      -   5        $��
buttonNivo��5�   V   $��1��6   $��0��6-   $��3,3,3,3q�6:   $��23��6K   $y�Visible=�6�   .��6�   ...6�   5�      � 5�      $��1��6   $��4��6+    5�      �� 5�      6.   �� 5�      $
��Auto �6+   6:   .+�5�      �� -   5        $��comboBoxNosilacGrupeLista��5�   M   $��0��6   $��0��6/   $��3,3,3,3q�6<   $ Naziv��6n   ��5�      �� 5�      .R�5�   "   �� ��5�   "   �� 5�   &   $	 Naziv�6.   $
 OneWay=�6;    5�   *     5�   .   6J   5�   '   5�   #   5�      ��5�        5�      # [ 6@    5�      �� 5�      $ listBoxLista��6&   $	 SelectedItem.NosilacGrupe�6A   $
 TwoWay=�6b   $ Explicit=�6p    5�   "     5�   &   6G   5�      5�      �� 5     -   5        $��buttonNosilacGrupe��5  V   $��1��6   $��0��6-   $��3,3,3,3q�6:   $��23��6K   .��6�   ...6�   5     �� 5     -   5        $��textBoxNormaSatiMinuta��5  H   $��1��6   $��5��6*   $��3,3,3,3q�67   $ 6��6f   .��6t   ��5     �� 5     $	 NormaSatiMinuta�6&   $ Explicit=�6=   $
 TwoWay=�6\    �� 	Converter 5  "   )  #AutoServis.DecimalToStringConverter  5	  &   6G    5  "     5  &   6B     AutoServis.IsDecimalRule  5  &   6<   5     .��5  (   5     � 5     $��1��6   $��6��6+    5     �� 5     62   �� 5     $
��Auto �6/   6>   .+�5     �� -   5        $��comboBoxBodLista��5  Q   $��0��6#   $��0��63   $��3,3,3,3q�6@   $ Naziv��6i   ��5     �� 5     .R�5  "   �� ��5  "   �� 5  &   $	 Naziv�6.   $
 OneWay=�6;    5   *     5!  .   6J   5#  '   5%  #   5&     ��5)       5*     # [ 6@    5-     �� 5.     $ listBoxLista��6&   $	 SelectedItem.Bod�6A   $
 TwoWay=�6Y   $ Explicit=�6g    5/  "     50  &   6G   52     54     �� 56     -   5        $��	buttonBod��56  V   $��1��6   $��0��6-   $��3,3,3,3q�6:   $��23��6K   .��6   ...6�   58     �� 5;     -   5        $��textBoxBrojBodova��5;  L   $��1��6   $��7��6.   $��3,3,3,3q�6;   $ 19��6e   .��6t   ��5<     �� 5=     $	 
BrojBodova�6&   $ Explicit=�68   $
 TwoWay=�6W    5>  "     5?  &   6G    5A  "     5B  &   6B     5C  &   6<   5E     .��5F  (   5G     �� 5H     -   5        $��comboBoxPoreskaStopaLista��5H  M   $��1��6   $��8��6/   $��3,3,3,3q�6<   $ VrednostProcenata��6n   ��5I     �� 5J     .R�5K  "   �� ��5L  "   �� 5M  &   $	 VrednostProcenata�6.   $
 OneWay=�6G    5N  *     5O  .   6J   5Q  '   5S  #   5T     ��5W       5X     # [ 6@    5[     �� 5\     $ listBoxLista��6&   $	 SelectedItem.PoreskaStopa�6A   $
 TwoWay=�6b   $ Explicit=�6p    5]  "     5^  &   6G   5`     5b     �� 5d     $��1��6   $��9��6-     PozadinaDugmici#��[ 6:   ._�5e     ��  �� Orientation$ 
Horizontal=�6&   .+�5f     �� -   5        $��buttonUnesi��5f  D   $��3,0,3,0q�6%   $��60��66   .��6s   Unesi6z   �� 5g     -   5        $��buttonIzmeni��5g  D   $��3,0,3,0q�6%   $��60��66   .��6t   Izmeni6|   �� 5h     -   5        $��buttonPotvrdi��5h  D   $��3,0,3,0q�6%   $��60��66   .��6u   	Potvrdi6~   �� 5i     -   5        $��buttonOdustani��5i  D   $��3,0,3,0q�6%   $��60��66   .��6w   
Odustani6�   �� 5j     -   5        $��buttonObrisi��5j  D   $��3,0,3,0q�6%   $��60��66   .��6s   	Obriši6{   �� 5k     -   5        $��buttonOsvezi��5k  D   $��3,0,3,0q�6%   $��60��66   .��6s   	Osveži6{   �� 5l     -   5        $��buttonOdaberi��5l  D   $��3,0,3,0q�6%   $��60��66   $y�	Collapsed=�6u   .��6�   	Odaberi6�   5n     5o     5p     5q     5r     5v     