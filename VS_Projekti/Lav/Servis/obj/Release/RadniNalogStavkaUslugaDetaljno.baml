   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  +    %Servis.RadniNalogStavkaUslugaDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis       � Title$$  RadniNalogStavkaUslugaDetaljno�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.��� -   $ ��gridRadniNalogStavkaUsluga�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��200 ��� $
��Auto �.+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1��$��	10,3,10,3q�$��Center=�	Usluga:�� $��0��$��2��$��	10,3,10,3q�$��Center=�
Količina:�� $��0��$��3��$��	10,3,10,3q�$��Center=�Cena bez poreza:�� $��0��$��4��$��	10,3,10,3q�$��Center=�Poreska stopa:�� $��0��$��5��$��	10,3,10,3q�$��Center=�Predviđeno vreme (Minuta):�� $��0��$��6��$��	10,3,10,3q�$��Center=�	Status:�� $��0��$��7��$��	10,3,10,3q�$��Center=�Utrošeno vreme (Minuta):�� $��0��$��8��$��	10,3,10,3q�$��Center=�	Napomena:�� -   $��	textBoxID��$��1��$��0��$��3,3,3,3q�{�.  $	��150������  �� Path$ StavkaUslugaID� �� Mode$ TwoWay=� �� UpdateSourceTrigger$ Explicit=�� -   $��
gridUsluga��$��1��$��1�� �� �� $
��Auto �.+��� -   $��textBoxUsluga��$��0��$��0��$��3,3,3,3q� �� 
IsReadOnly . $	��200��.����`� 	 `� Mode$	 TwoWay=�
 `� UpdateSourceTrigger$
 Explicit=� `� 	Converter +   %Servis.ConcatenateFourStringConverter  .1��� $ Usluga.VrstaUsluge.Naziv��� $ Usluga.NosilacGrupe.Naziv��� $ Usluga.Nivo.Naziv��� $ Usluga.Pozicija.Naziv�.��
 � Tag �� $ UslugaID�$ Explicit=�$ TwoWay=�.���� -   $��buttonUsluga��$��1��$��0��$��3,3,3,3q�$��23��.��...�� -   $��textBoxUslugaKolicina��$��1��$��2��$��3,3,3,3q�.������ $ UslugaKolicina�$ Explicit=�$ TwoWay=� �� 	Converter %   Servis.DecimalToStringConverter  .���� -   $��textBoxCenaBezPoreza��$��1��$��3��$��3,3,3,3q� .  .������ $ UslugaCenaBezPoreza�$ Explicit=�$ TwoWay=�   .���� -   $��textBoxPoreskaStopa��$��1��$��4��$��3,3,3,3q� . .������ $$ PoreskaStopa.VrednostProcenata�$ Explicit=�$ TwoWay=�.�� �� $ UslugaPoreskaStopa_ID�$ Explicit=�$ TwoWay=�.���� -	   $#��textBoxPredvidjenoVremeMinuta��$��1��$��5��$��3,3,3,3q� . .������ $3 -RadniNalogStavkaUsluga.PredvidjenoVremeMinuta�$ Explicit=�$ TwoWay=�.���� -
   $��comboBoxRadniNalogStatus��$��1��$��6��$��3,3,3,3q� w� TextPath$ Naziv������ .R��� ���� $ Naziv�$ OneWay=� �� SelectedItem ��  �� ElementName$  gridRadniNalogStavkaUsluga��$9 3DataContext.RadniNalogStavkaUsluga.RadniNalogStatus�$ TwoWay=�$ Explicit=��� -   $ ��textBoxUtrosenoVremeMinuta��$��1��$��7��$��3,3,3,3q� .  .������ $0 *RadniNalogStavkaUsluga.UtrosenoVremeMinuta�$ Explicit=�$ TwoWay=�.���� -   $��textBoxNapomena��$��1��$��14��$��3,3,3,3q� �� MinLines$ 5�� �� MaxLines$ 5��$
��Left=� �� 	MaxLength$	 500�� �� TextWrapping$
 Wrap=� �� AcceptsReturn .  " �� VerticalScrollBarVisibility$ Visible=�.������ $ 	textBoxID��$ ActualWidth�$ OneWay=�.������ $% RadniNalogStavkaUsluga.Napomena�$ Explicit=�$ TwoWay=� (   "Servis.PrazanStringToNullConverter  .���� -   $��buttonSacuvajINovi��$��2��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -   $��buttonSacuvaj��$��2��$��1��$��4q�$��60��.��
Sačuvaj�� -   $��buttonSacuvajIZatvori��$��2��$��2��$��4q�$��60��.��Sačuvaj & Zatvori