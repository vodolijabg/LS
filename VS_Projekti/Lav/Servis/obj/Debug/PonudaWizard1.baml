   M S B A M L   `   `   `  ����P  LMaskedTextBox.Control, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:MaskedTextBox.Control;assembly=MaskedTextBox.ControlMaskedTextBox.Control  5      B >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis 5         Servis.PonudaWizard1   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      SctrlBclr-namespace:MaskedTextBox.Control;assembly=MaskedTextBox.Control     5      &localclr-namespace:Servis     5      -   5          N� Title$  PonudaWizard1��5	       N� 	KeepAlive . .��5      � -   5        $��gridFizickoLice��5      ���	#FF9CAAC1 � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $��* �6   6,   .+�5   
   �� $��0��6   $��0��6%   $��
10,30,10,3q�62   $��Center=�6F   Ime:6h   �� 5   
   $��0��6   $��1��6%   $��	10,3,10,3q�62   $��Center=�6E   
Prezime:6k   �� 5   
   $��0��6   $��2��6$   $��	10,3,10,3q�61   $��Center=�6D   
Telefon 1:6k   �� 5   
   $��0��6   $��3��6$   $��	10,3,10,3q�61   $��Center=�6D   Način zahteva za ponudu:6y   �� 5   
   -   5        $��
textBoxIme��5   A   $��1��6   $��0��6"   $��3,30,5,3q�6/    �� 	MaxLength$ 50��6S   .��6b   ��5       �� 5!       �� Path$	 Ime�6    �� Mode$ OneWay=�6%    �� UpdateSourceTrigger$ Explicit=�63   6T   .��5"      5#      �� 5$   
   -   5        $��textBoxPrezime��5$   @   $��1��6   $��1��6"   $��3,3,5,3q�6/   $ 50��6V   .��6e   ��5%      �� 5&      $ Prezime�6   $ OneWay=�6)   $ Explicit=�67   6X   .��5'      5(      )   #MaskedTextBox.Control.MaskedTextBox  5.   
   -   5        $��textBoxTelefon1��5.   K   $��1��6   $��2��6-   $��3,0,3,0q�6:      	InputMask$ iii/ii-ii-iiiiiiii��6b   .��6�   ��5/      �� 50      $ Telefon1�6   $ TwoWay=�6*   $ Explicit=�68   	 �� ConverterParameter$	 ___/__-__-________��51   `   
 �� 	Converter
 52      !  Servis.TelefonMaskKonverter  53      67   55      .��56   '   57      �� 59   
   -   5        $"��comboBoxNacinZahtevaZaPonudu��59   A   $��1��6   $��3��6#   $��3,3,3,3q�60    w� TextPath$ Naziv��6e   ��5:      �� 5;      .R�5<      �� ��5=      �� 5>      $ Naziv�6&   $ OneWay=�63   6D   5@      5A       �� SelectedItem 5H      �� 5I       �� ElementName$ 
gridPonuda��6   $&  DataContext.NacinZahtevaZaPonudu�63   $ TwoWay=�6[   $ Explicit=�6i   
 5J      @  :Servis.NacinZahtevaZaPonuduToNacinZahtevaZaPonuduConverter  5K      6V   5M      5P      �� 5R   
   $��Right=�6   $��1��61   $��4��6A    �� Orientation$ 
Horizontal=�6N   $��0,30,0,5q�6g   $��Bottom=�6y   .+�5T      �� -   5        $��buttonDalje��5T   /   $��4q�6   $��60��6!    �� 	IsDefault . .��6n   	Dalje>>6}   �� 5U      -   5        $��buttonOtkazi��5U   /   $��4q�6   $��60��6!   .��6^   	Otkaži6f   5V      5Y      5Z      