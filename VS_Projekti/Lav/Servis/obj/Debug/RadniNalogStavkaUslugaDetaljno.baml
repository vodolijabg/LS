   M S B A M L   `   `   `  ����B  >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  5   	   +    %Servis.RadniNalogStavkaUslugaDetaljno   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   &localclr-namespace:Servis     5   	     � Title$$  RadniNalogStavkaUslugaDetaljno��5   	    � SizeToContent$ WidthAndHeight=�60    � 
ResizeMode$ NoResize=�6P   .��5      � -   5        $ ��gridRadniNalogStavkaUsluga��5       � RowDefinitions 5   
   �� 5	      $
��Auto �6   6,   �� 5
      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $	��200 �6   6-   �� 5      $
��Auto �6   6.   .+�5   
   �� $��0��6   $��0��6$   $��	10,3,10,3q�61   $��Center=�6D   ID:6d   �� 5   
   $��0��6   $��1��6$   $��	10,3,10,3q�61   $��Center=�6D   	Usluga:6h   �� 5   
   $��0��6   $��2��6$   $��	10,3,10,3q�61   $��Center=�6D   
Količina:6j   �� 5   
   $��0��6   $��3��6$   $��	10,3,10,3q�61   $��Center=�6D   Cena bez poreza:6q   �� 5   
   $��0��6   $��4��6$   $��	10,3,10,3q�61   $��Center=�6D   Poreska stopa:6o   �� 5   
   $��0��6   $��5��6$   $��	10,3,10,3q�61   $��Center=�6D   Predviđeno vreme (Minuta):6{   �� 5    
   $��0��6   $��6��6$   $��	10,3,10,3q�61   $��Center=�6D   	Status:6h   �� 5!   
   $��0��6   $��7��6$   $��	10,3,10,3q�61   $��Center=�6D   Utrošeno vreme (Minuta):6y   �� 5"   
   $��0��6   $��8��6$   $��	10,3,10,3q�61   $��Center=�6D   	Napomena:6j   �� 5$   
   -   5        $��	textBoxID��5$   @   $��1��6   $��0��6"   $��3,3,3,3q�6/   {�.  $	��150��6�   ��6Q   ��  �� Path$ StavkaUslugaID� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ Explicit=�6�   � 5&   
   -   5        $��
gridUsluga��5&      $��1��6!   $��1��61    5'      �� 5(      6&   �� 5)      $
��Auto �6#   62   .+�5,      �� -   5        $��textBoxUsluga��5,   E   $��0��6   $��0��6'   $��3,3,3,3q�64    �� 
IsReadOnly . $	��200��6�   .��6�   ��5-      `� 5.      	 `� Mode$	 TwoWay=�6#   
 `� UpdateSourceTrigger$
 Explicit=�61    `� 	Converter 5/      +   %Servis.ConcatenateFourStringConverter  50      6E   .1�53      �� $ Usluga.VrstaUsluge.Naziv�6"   6D   �� 54      $ Usluga.NosilacGrupe.Naziv�6"   6E   �� 55      $ Usluga.Nivo.Naziv�6"   6=   �� 56      $ Usluga.Pozicija.Naziv�6"   6A   57      .��58       
 � Tag 59      �� 5:      $ UslugaID�6   $ Explicit=�6.   $ TwoWay=�6M   5;      .��5<      5=      �� 5?      -   5        $��buttonUsluga��5?   N   $��1��6   $��0��6%   $��3,3,3,3q�62   $��23��6C   .��6}   ...6�   5A      �� 5C   
   -   5        $��textBoxUslugaKolicina��5C   @   $��1��6   $��2��6"   $��3,3,3,3q�6/   .��6^   ��5D      �� 5E      $ UslugaKolicina�6   $ Explicit=�60   $ TwoWay=�6O    �� 	Converter 5F      %   Servis.DecimalToStringConverter  5G      6;   5I      .��5J      5K      �� 5L   
   -   5        $��textBoxCenaBezPoreza��5L   @   $��1��6   $��3��6"   $��3,3,3,3q�6/    .  .��6o   ��5M      �� 5N      $ UslugaCenaBezPoreza�6   $ Explicit=�65   $ TwoWay=�6T    5O        5P      6;   5R      .��5S      5T      �� 5U   
   -   5        $��textBoxPoreskaStopa��5U   @   $��1��6   $��4��6"   $��3,3,3,3q�6/    . .��6m   ��5V      �� 5W      $$ PoreskaStopa.VrednostProcenata�6   $ Explicit=�6@   $ TwoWay=�6_   6p   .��5X       5Y      �� 5Z      $ UslugaPoreskaStopa_ID�6   $ Explicit=�67   $ TwoWay=�6V   5[      .��5\      5]      �� 5^   
   -	   5        $#��textBoxPredvidjenoVremeMinuta��5^   @   $��1��6   $��5��6"   $��3,3,3,3q�6/    . .��6w   ��5_      �� 5`      $3 -RadniNalogStavkaUsluga.PredvidjenoVremeMinuta�6   $ Explicit=�6O   $ TwoWay=�6n   5a      .��5b      5c      �� 5d   
   -
   5        $��comboBoxRadniNalogStatus��5d   A   $��1��6   $��6��6#   $��3,3,3,3q�60    w� TextPath$ Naziv��6a   ��5e      �� 5f      .R�5g      �� ��5h      �� 5i      $ Naziv�6&   $ OneWay=�63   6D   5k      5l       �� SelectedItem 5s      �� 5t       �� ElementName$  gridRadniNalogStavkaUsluga��6   $9 3DataContext.RadniNalogStavkaUsluga.RadniNalogStatus�6C   $ TwoWay=�6~   $ Explicit=�6�   5x      5{      �� 5}   
   -   5        $ ��textBoxUtrosenoVremeMinuta��5}   @   $��1��6   $��7��6"   $��3,3,3,3q�6/    .  .��6u   ��5~      �� 5      $0 *RadniNalogStavkaUsluga.UtrosenoVremeMinuta�6   $ Explicit=�6L   $ TwoWay=�6k   5�      .��5�      5�      �� 5�   
   -   5        $��textBoxNapomena��5�   B   $��1��6   $��14��6#   $��3,3,3,3q�61    �� MinLines$ 5��6Y    �� MaxLines$ 5��6f   $
��Left=�6s    �� 	MaxLength$	 500��6�    �� TextWrapping$
 Wrap=�6�    �� AcceptsReturn .  " �� VerticalScrollBarVisibility$ Visible=�6�   .��6�   ��5�      �� 5�      $ 	textBoxID��6   $ ActualWidth�63   $ OneWay=�6F   6V   .��5�      ��5�      �� 5�      $% RadniNalogStavkaUsluga.Napomena�6   $ Explicit=�6A   $ TwoWay=�6`    5�      (   "Servis.PrazanStringToNullConverter  5�      6>   5�      .��5�      5�      �� 5�   
   -   5        $��buttonSacuvajINovi��5�   G   $��2��6   $��0��6!   $��4q�6.   $��60��69   .��6�   Sačuvaj & Novi6�   �� 5�   
   -   5        $��buttonSacuvaj��5�   G   $��2��6   $��1��6!   $��4q�6.   $��60��69   .��6x   
Sačuvaj6�   �� 5�   
   -   5        $��buttonSacuvajIZatvori��5�   G   $��2��6   $��2��6!   $��4q�6.   $��60��69   .��6�   Sačuvaj & Zatvori6�   5�      5�      