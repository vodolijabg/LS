   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  P LMaskedTextBox.Control, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:MaskedTextBox.Control;assembly=MaskedTextBox.ControlMaskedTextBox.Control %    Servis.ServisnaKnjizicaDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis     <my-http://schemas.microsoft.com/wpf/2008/toolkit     SctrlBclr-namespace:MaskedTextBox.Control;assembly=MaskedTextBox.Control       � Title$  ServisnaKnjizicaDetaljno�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.��� -   $��gridServisnaKnjizica�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��250���� $
��Auto �.+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1�� �� Orientation$ 
Horizontal=�.+��� $��	10,3,10,3q�$��Center=�	Šifra:�� -   $��checkBoxGenerisiSifru�� � ToolTip$ Unesi šifru��$��Center=� d� 	IsChecked$ FalseY��� $��0��$��2��$��	10,3,10,3q�$��Center=�
Partner:�� $��0��$��3��$��	10,3,10,3q�$��Center=�Tip:�� $��0��$��4��$��	10,3,10,3q�$��Center=�	Tip Id:�� $��0��$��5��$��	10,3,10,3q�$��Center=�Broj šasije:�� $��0��$��6��$��	10,3,10,3q�$��Center=�Broj motora:�� $��0��$��7��$��	10,3,10,3q�$��Center=�	Godište:�� $��0��$��8��$��	10,3,10,3q�$��Center=�Kilometraža:�� $��0��$��9��$��	10,3,10,3q�$��Center=�Registarski broj:�� $��0��$��10��$��	10,3,10,3q�$��Center=�Dimenzija guma:�� $��0��$��11��$��	10,3,10,3q�$��Center=�Datum registracije:�� $��0��$��12��$��	10,3,10,3q�$��Center=�ABS:�� $��0��$��13��$��	10,3,10,3q�$��Center=�PS:�� $��0��$��14��$��	10,3,10,3q�$��Center=�AC:�� $��0��$��15��$��	10,3,10,3q�$��Center=�	Napomena:�� -   $��	textBoxID��$��1��$��0��$��3,3,3,3q�{�.  $	��150������  �� Path$ ServisnaKnjizicaID�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 Explicit=��� -   $��textBoxSifra��$��1��$��1��$��3,3,3,3q� �� 	MaxLength$ 50��{���  �� ElementName$ checkBoxGenerisiSifru��$ 	IsChecked�.������ $ Sifra�$
 Explicit=�$	 TwoWay=�.��� -   $��gridPartner��$��1��$��2�� �� �� $
��Auto �.+��� -   $��textBoxPartner��$��0��$��0��$��3,3,3,3q� �� 
IsReadOnly . .����`�  `� Mode$ OneWay=� `� UpdateSourceTrigger$ Explicit=� `� 	Converter &    Servis.DajNazivPartneraConverter   `� ConverterParameter$ -��.1��� $" PoslovniPartner.SkracenNaziv��� $ FizickoLice.Ime�.��
 � Tag `�  #   Servis.DajIDPartneraConverter  $ -��.1��� $ PoslovniPartnerID��� $ FizickoLiceID�.���� -   $��buttonPartner��$��1��$��0��$��3,3,3,3q�$��23��.��...� -   $��gridTip��$��1��$��3�� �� �� $
��Auto �.+��� -	   $��
textBoxTip��$��3,3,3,3q�$��0��$��0�� . .�� �� $ TipAutomobilaID�$	 OneWay=�$
 Explicit=�.��.����`� $ TwoWay=�$ Explicit=� ,   &Servis.ConcatenateThreeStringConverter  .1��� $5 /TipAutomobila.ModelAutomobila.Proizvodjac.Naziv��� $3 -TipAutomobila.ModelAutomobila.OpisTabela.Opis��� $# TipAutomobila.OpisTabela.Opis�.���� -
   $��	buttonTip��$��1��$��0��$��3,3,3,3q�$��23��.��...�� -   $��textBoxTipAutomobilaID��$��1��$��4��$��3,3,3,3q� . .������ $ TipAutomobilaID�$
 Explicit=�$	 TwoWay=�.���� -   $��textBoxBrojSasije��$��1��$��5��$��3,3,3,3q�$ 30��.������ $ 
BrojSasije�$
 Explicit=�$	 TwoWay=� �� 	Converter (   "Servis.PrazanStringToNullConverter  .���� -   $��textBoxBrojMotora��$��1��$��6��$��3,3,3,3q�$ 30��.������ $ 
BrojMotora�$	 TwoWay=�$
 Explicit=�   .���� -   $��textBoxGodiste��$��1��$��7��$��3,3,3,3q�$ 4��.������ $ Godiste�$	 TwoWay=�$
 Explicit=� !   Servis.IntToStringConverter  .���� -   $��textBoxKilometraza��$��1��$��8��$��3,3,3,3q�$ 7��.������ $ Kilometraza�$	 TwoWay=�$
 Explicit=�   .��)  #MaskedTextBox.Control.MaskedTextBox  -   $��textBoxRegistarskiBroj��$��1��$��9��$��3,0,3,0q�   	InputMask$ ww www wwwwwwwwwwww��.������ $ RegistarskiBroj�$	 TwoWay=�$
 Explicit=� �� ConverterParameter$ __ ___ ____________�� !   Servis.TelefonMaskKonverter  .���� -   $��textBoxDimenzijaGuma��$��1��$��10��$��3,3,3,3q�$ 50��.������ $ DimenzijaGuma�$
 Explicit=�$	 TwoWay=�   .��(  "System.Windows.Controls.DatePicker  -   $!��datePickerDatumRegistracije��$��1��$��11��$��3,3,3,3q�   SelectedDate �� $ DatumRegistracije�$	 TwoWay=�$
 Explicit=��� -   $��checkBoxABS��$��1��$��12��$��3,3,3,3q� �� $	 ABS�$
 Explicit=�$	 TwoWay=��� -   $��
checkBoxPS��$��1��$��13��$��3,3,3,3q� �� $ PS�$
 Explicit=�$	 TwoWay=��� -   $��
checkBoxAC��$��1��$��14��$��3,3,3,3q� �� $ AC�$
 Explicit=�$	 TwoWay=��� -   $��textBoxNapomena��$��1��$��15��$��3,3,3,3q� �� MinLines$ 5��$
��Left=�$	 500�� �� TextWrapping$
 Wrap=� �� AcceptsReturn .  " �� VerticalScrollBarVisibility$ Visible=�.������ $ textBoxSifra��$ ActualWidth�$	 OneWay=�.������ $ Napomena�$
 Explicit=�$	 TwoWay=�   .���� -   $��buttonSacuvajINovi��$��2��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -   $��buttonSacuvaj��$��2��$��1��$��4q�$��60��.��
Sačuvaj�� -   $��buttonSacuvajIZatvori��$��2��$��2��$��4q�$��60��.��Sačuvaj & Zatvori�� -   $��buttonPonuda��$��2��$��3��$��4q�$��60��.��
->Ponuda�� -   $��buttonRadniNalog��$��2��$��4��$��4q�$��60��.��->Radni nalog