   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  !    Servis.StavkaUslugaDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis       � Title$  StavkaUslugaDetaljno�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.��� -   $��gridStavkaUsluga�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1��$��	10,3,10,3q�$��Center=�	Usluga:�� $��0��$��2��$��	10,3,10,3q�$��Center=�
Količina:�� $��0��$��3��$��	10,3,10,3q�$��Center=�Cena bez poreza:�� $��0��$��4��$��	10,3,10,3q�$��Center=�Poreska stopa:�� -   $��	textBoxID��$��1��$��0��$��3,3,3,3q�{�.  $	��150������  �� Path$ StavkaUslugaID� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ Explicit=�� -   $��
gridUsluga��$��1��$��1�� �� �� $
��Auto �.+��� -   $��textBoxUsluga��$��0��$��0��$��3,3,3,3q� �� 
IsReadOnly . $	��200��.����`� 	 `� Mode$	 TwoWay=�
 `� UpdateSourceTrigger$
 Explicit=� `� 	Converter +   %Servis.ConcatenateFourStringConverter  .1��� $ Usluga.VrstaUsluge.Naziv��� $ Usluga.NosilacGrupe.Naziv��� $ Usluga.Nivo.Naziv��� $ Usluga.Pozicija.Naziv�.��
 � Tag �� $ UslugaID�$ Explicit=�$ TwoWay=�.���� -   $��buttonUsluga��$��1��$��0��$��3,3,3,3q�$��23��.��...�� -   $��textBoxUslugaKolicina��$��1��$��2��$��3,3,3,3q�.������ $ UslugaKolicina�$ Explicit=�$ TwoWay=� �� 	Converter %   Servis.DecimalToStringConverter  .���� -   $��textBoxCenaBezPoreza��$��1��$��3��$��3,3,3,3q� .  .������ $ UslugaCenaBezPoreza�$ Explicit=�$ TwoWay=�   .���� -   $��textBoxPoreskaStopa��$��1��$��4��$��3,3,3,3q� . .������ $$ PoreskaStopa.VrednostProcenata�$ Explicit=�$ TwoWay=�.�� �� $ UslugaPoreskaStopa_ID�$ Explicit=�$ TwoWay=�.���� -	   $��buttonSacuvajINovi��$��2��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -
   $��buttonSacuvaj��$��2��$��1��$��4q�$��60��.��
Sačuvaj�� -   $��buttonSacuvajIZatvori��$��2��$��2��$��4q�$��60��.��Sačuvaj & Zatvori