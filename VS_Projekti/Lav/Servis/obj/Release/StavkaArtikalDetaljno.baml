   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  "    Servis.StavkaArtikalDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis     -     � Title$  StavkaArtikalDetaljno�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.��� -   $��gridStavkaArtikal�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1��$��	10,3,10,3q�$��Center=�Dobavljač:�� $��0��$��2��$��	10,3,10,3q�$��Center=�
Artikal:�� $��0��$��3��$��	10,3,10,3q�$��Center=�
Količina:�� $��0��$��4��$��	10,3,10,3q�$��Center=�Cena bez poreza:�� $��0��$��5��$��	10,3,10,3q�$��Center=�Poreska stopa:�� -   $��textBlockNosilacGrupe��$��0��$��6��$��	10,3,10,3q�$��Center=�Nosilac grupe:�� -   $��	textBoxID��$��1��$��0��$��3,3,3,3q�{�.  $	��150������  �� Path$ StavkaArtikalID� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ Explicit=��� -   $��textBoxDobavljac��$��1��$��1��$��3,3,3,3q� �� 
IsReadOnly . .����`� 	 `� 	Converter	 &    Servis.DajNazivPartneraConverter  
 `� ConverterParameter$
 -��.1��� $" PoslovniPartner.SkracenNaziv��� $ KorisnikPrograma.Naziv�.��
 � Tag `� 	 #   Servis.DajIDPartneraConverter  $
 ZaTag��.1��� $ PoslovniPartnerID��� $ KorisnikProgramaID�.��� -   $��gridArtikal��$��1��$��2�� �� �� $
��Auto �.+��� -   $��textBoxArtikal��$��0��$��0��$��3,3,3,3q� . $	��200��.����`�  `� Mode$ TwoWay=� `� UpdateSourceTrigger$ Explicit=�	 ,   &Servis.ConcatenateThreeStringConverter  $
 ConcatenateArtikal��.1��� $ ArtikalProizvodjacNaziv��� $ ArtikalBrojProizvodjaca��� $ ArtikalNaziv�.�� `� $ TwoWay=�$ Explicit=�	 +   %Servis.ConcatenateFourStringConverter  $
 $��.1��� $ ArtikalBrojProizvodjaca��� $ ArtikalProizvodjacNaziv��� $ ArtikalProizvodjacID��� $ ArtikalNaziv�.���� -   $��buttonArtikal��$��1��$��0��$��3,3,3,3q�$��23��.��...�� -	   $��textBoxKolicina��$��1��$��3��$��3,3,3,3q�.������ $ ArtikalKolicina�$ Explicit=�$ TwoWay=� �� 	Converter %   Servis.DecimalToStringConverter  .���� -
   $��textBoxCenaBezPoreza��$��1��$��4��$��3,3,3,3q� .  .������ $ ArtikalCenaBezPoreza�$ Explicit=�$ TwoWay=�   .���� -   $��textBoxPoreskaStopa��$��1��$��5��$��3,3,3,3q� . .������ $$ PoreskaStopa.VrednostProcenata�$ Explicit=�$ TwoWay=�.�� �� $ ArtikalPoreskaStopaID�$ Explicit=�$ TwoWay=�.���� -   $��comboBoxNosilacGrupe��$��1��$��6��$��3,3,3,3q� w� TextPath$ Naziv������ .R��� ���� $ Naziv�$ OneWay=� �� SelectedItem ��  �� ElementName$ 
gridUsluga��$ DataContext.NosilacGrupe�$ TwoWay=�$ Explicit=��� -   $��buttonSacuvajINovi��$��2��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -   $��buttonSacuvaj��$��2��$��1��$��4q�$��60��.��
Sačuvaj�� -   $��buttonSacuvajIZatvori��$��2��$��2��$��4q�$��60��.��Sačuvaj & Zatvori