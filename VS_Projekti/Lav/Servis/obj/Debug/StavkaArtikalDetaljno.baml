   M S B A M L   `   `   `  ����B  >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  5      "    Servis.StavkaArtikalDetaljno   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      &localclr-namespace:Servis     5      -   5          � Title$  StavkaArtikalDetaljno��5       � SizeToContent$ WidthAndHeight=�6$    � 
ResizeMode$ NoResize=�6D   .��5      � -   5        $��gridStavkaArtikal��5       � RowDefinitions 5	   
   �� 5
      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      6"   �� 5      $
��Auto �6   6.   .+�5   
   �� $��0��6   $��0��6$   $��	10,3,10,3q�61   $��Center=�6D   ID:6d   �� 5   
   $��0��6   $��1��6$   $��	10,3,10,3q�61   $��Center=�6D   Dobavljač:6k   �� 5   
   $��0��6   $��2��6$   $��	10,3,10,3q�61   $��Center=�6D   
Artikal:6i   �� 5   
   $��0��6   $��3��6$   $��	10,3,10,3q�61   $��Center=�6D   
Količina:6j   �� 5   
   $��0��6   $��4��6$   $��	10,3,10,3q�61   $��Center=�6D   Cena bez poreza:6q   �� 5   
   $��0��6   $��5��6$   $��	10,3,10,3q�61   $��Center=�6D   Poreska stopa:6o   �� 5    
   -   5        $��textBlockNosilacGrupe��5    _   $��0��6   $��6��6$   $��	10,3,10,3q�61   $��Center=�6D   Nosilac grupe:6�   �� 5"   
   -   5        $��	textBoxID��5"   @   $��1��6   $��0��6"   $��3,3,3,3q�6/   {�.  $	��150��6�   ��6Q   ��  �� Path$ StavkaArtikalID� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ Explicit=�6�   �� 5#   
   -   5        $��textBoxDobavljac��5#   @   $��1��6   $��1��6"   $��3,3,3,3q�6/    �� 
IsReadOnly . .��6j   ��5$      `� 5%      	 `� 	Converter	 5&      &    Servis.DajNazivPartneraConverter  5'      6<   
 `� ConverterParameter$
 -��5)   Y   .1�5*      �� $" PoslovniPartner.SkracenNaziv�6   6D   �� 5+      $ KorisnikPrograma.Naziv�6   6>   5,      .��5-      
 � Tag 5.      `� 5/      	 50      #   Servis.DajIDPartneraConverter  51      69   $
 ZaTag��53   ]   .1�54      �� $ PoslovniPartnerID�6   69   �� 55      $ KorisnikProgramaID�6   6:   56      .��57      58      � 5<   
   -   5        $��gridArtikal��5<      $��1��6"   $��2��62    5=      �� 5>      6&   �� 5?      $
��Auto �6#   62   .+�5B      �� -   5        $��textBoxArtikal��5B   E   $��0��6   $��0��6'   $��3,3,3,3q�64    . $	��200��6�   .��6�   ��5C      `� 5D       `� Mode$ TwoWay=�6#    `� UpdateSourceTrigger$ Explicit=�61   	 5E      ,   &Servis.ConcatenateThreeStringConverter  5F      6F   $
 ConcatenateArtikal��5H   n   .1�5I      �� $ ArtikalProizvodjacNaziv�6"   6C   �� 5J      $ ArtikalBrojProizvodjaca�6"   6C   �� 5K      $ ArtikalNaziv�6"   68   5L      .��5M        5N      `� 5O      $ TwoWay=�6#   $ Explicit=�61   	 5P      +   %Servis.ConcatenateFourStringConverter  5Q      6E   $
 $��5S   ]   .1�5T      �� $ ArtikalBrojProizvodjaca�6"   6C   �� 5U      $ ArtikalProizvodjacNaziv�6"   6C   �� 5V      $ ArtikalProizvodjacID�6"   6@   �� 5W      $ ArtikalNaziv�6"   68   5X      .��5Y      5Z      �� 5\      -   5        $��buttonArtikal��5\   N   $��1��6   $��0��6%   $��3,3,3,3q�62   $��23��6C   .��6   ...6�   5^      �� 5`   
   -	   5        $��textBoxKolicina��5`   @   $��1��6   $��3��6"   $��3,3,3,3q�6/   .��6X   ��5a      �� 5b      $ ArtikalKolicina�6   $ Explicit=�61   $ TwoWay=�6P    �� 	Converter 5c      %   Servis.DecimalToStringConverter  5d      6;   5f      .��5g      5h      �� 5i   
   -
   5        $��textBoxCenaBezPoreza��5i   @   $��1��6   $��4��6"   $��3,3,3,3q�6/    .  .��6o   ��5j      �� 5k      $ ArtikalCenaBezPoreza�6   $ Explicit=�66   $ TwoWay=�6U    5l        5m      6;   5o      .��5p      5q      �� 5r   
   -   5        $��textBoxPoreskaStopa��5r   @   $��1��6   $��5��6"   $��3,3,3,3q�6/    . .��6m   ��5s      �� 5t      $$ PoreskaStopa.VrednostProcenata�6   $ Explicit=�6@   $ TwoWay=�6_   6p   .��5u       5v      �� 5w      $ ArtikalPoreskaStopaID�6   $ Explicit=�67   $ TwoWay=�6V   5x      .��5y      5z      �� 5�   
   -   5        $��comboBoxNosilacGrupe��5�   A   $��1��6   $��6��6#   $��3,3,3,3q�60    w� TextPath$ Naziv��6]   ��5�      �� 5�      .R�5�      �� ��5�      �� 5�      $ Naziv�6&   $ OneWay=�63   6D   5�      5�       �� SelectedItem 5�      �� 5�       �� ElementName$ 
gridUsluga��6   $ DataContext.NosilacGrupe�63   $ TwoWay=�6S   $ Explicit=�6a   6�   5�      �� 5�   
   -   5        $��buttonSacuvajINovi��5�   G   $��2��6   $��0��6!   $��4q�6.   $��60��69   .��6�   Sačuvaj & Novi6�   �� 5�   
   -   5        $��buttonSacuvaj��5�   G   $��2��6   $��1��6!   $��4q�6.   $��60��69   .��6x   
Sačuvaj6�   �� 5�   
   -   5        $��buttonSacuvajIZatvori��5�   G   $��2��6   $��2��6!   $��4q�6.   $��60��69   .��6�   Sačuvaj & Zatvori6�   5�      5�      