   M S B A M L   `   `   `  ����B  >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  5      $    Servis.PonudaRadniNalogWizard1   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:Servis     5      -   5          N� Title$  PonudaRadniNalogWizard1��5       N� 	KeepAlive . .��5	      � -   5        $��gridRadniNalog��5	      ���	#FF9CAAC1 � RowDefinitions 5
   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $
��Auto �6   6/   �� 5   
   6   .+�5   
   �� $��0��6   $��0��6%   $��
10,30,10,3q�62   $��Center=�6F   Servisna knjižica:6v   �� 5   
   $��0��6   $��1��6$   $��	10,3,10,3q�61   $��Center=�6D   Kilometraža:6m   �� 5   
   $��0��6   $��2��6$   $��	10,3,10,3q�61   $��Center=�6D   Registarski broj:6r   �� 5   
   $��0��6   $��3��6$   $��	10,3,10,3q�61   $��Center=�6D   Datum registracije:6t   �� 5   
   $��0��6   $��4��6$   $��	10,3,10,3q�61   $��Center=�6D   	Napomena:6j   �� 5    
   -   5        $��textBoxServisnaKnjizica��5    B   $��1��6   $��0��6#   $��3,30,3,3q�60    �� 
IsReadOnly . $	��150��6t   .��6�   ��5!      `� 5"       `� Mode$ TwoWay=�6    `� UpdateSourceTrigger$ Explicit=�6-    `� 	Converter 5#      ,   &Servis.ConcatenateThreeStringConverter  5$      6B   .1�5'      ��  �� Path$F @ServisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv�6   6h   �� 5(      $D >ServisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis�6   6f   �� 5)      $4 .ServisnaKnjizica.TipAutomobila.OpisTabela.Opis�6   6V   5*      .��5+      
	 � Tag	 5,      �� 5-      $ ServisnaKnjizicaID�6   
 �� UpdateSourceTrigger$
 Explicit=�64    �� Mode$ TwoWay=�6S   5.      .��5/      50      �� 53   
   -   5        $��textBoxNapomena��53   [   $��1��6   $��4��6"   $��3,3,3,3q�60    �� MinLines$ 2��6A    �� MaxLines$ 2��6N    �� 	MaxLength$	 500��6s    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn .  " �� VerticalScrollBarVisibility$ Visible=�6�   $	��250��6�   .��6�   ��54      �� 55       �� ElementName$ textBoxServisnaKnjizica��6   $ ActualWidth�6A   $ OneWay=�6T   6d   .��56      ��57      �� 58      $ Napomena�6   $ TwoWay=�6*   $
 Explicit=�68    �� 	Converter 59      (   "Servis.PrazanStringToNullConverter  5:      6>   5<      .��5=      5>      �� 5?   
   -   5        $��textBoxKilometraza��5?   @   $��1��6   $��1��6"   $��3,3,3,3q�6/   .��6[   ��5@      �� 5A      $ Kilometraza�6   $
 Explicit=�6-   $ TwoWay=�6L    5B      !   Servis.IntToStringConverter  5C      67   5E      .��5F      5G      �� 5I   
   -   5        $��textBoxRegistarskiBroj��5I   @   $��1��6   $��2��6"   $��3,3,3,3q�6/   $ 15��6^   .��6m   ��5J      �� 5K      $ RegistarskiBroj�6   $
 Explicit=�61   $ TwoWay=�6P    5L        5M      6>   5O      .��5P      5Q      (  "System.Windows.Controls.DatePicker  5S   
   -   5        $!��datePickerDatumRegistracije��5S   3   $��1��6   $��3��6&   $��3,3,3,3q�6V      SelectedDate 6g   �� $ DatumRegistracije�$ TwoWay=�$
 Explicit=�6�   �� 5U   
   $��Right=�6   $��1��61   $��5��6A    �� Orientation$ 
Horizontal=�6N   $��0,30,5,5q�6g   $��Bottom=�6y   .+�5W      �� -   5        $��buttonDalje��5W   /   $��4q�6   $��60��6!   .��6\   	Dalje>>6k   �� 5X      -	   5        $��buttonOtkazi��5X   /   $��4q�6   $��60��6!   .��6^   	Otkaži6f   5Y      5\      5]      