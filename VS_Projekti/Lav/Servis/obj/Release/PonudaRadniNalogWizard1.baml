   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  $    Servis.PonudaRadniNalogWizard1   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     @d2http://schemas.microsoft.com/expression/blend/2008     &localclr-namespace:Servis     -     N� Title$  PonudaRadniNalogWizard1�� N� 	KeepAlive . .��� -   $��gridRadniNalog�����	#FF9CAAC1 � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� .+��� $��0��$��0��$��
10,30,10,3q�$��Center=�Servisna knjižica:�� $��0��$��1��$��	10,3,10,3q�$��Center=�Kilometraža:�� $��0��$��2��$��	10,3,10,3q�$��Center=�Registarski broj:�� $��0��$��3��$��	10,3,10,3q�$��Center=�Datum registracije:�� $��0��$��4��$��	10,3,10,3q�$��Center=�	Napomena:�� -   $��textBoxServisnaKnjizica��$��1��$��0��$��3,30,3,3q� �� 
IsReadOnly . $	��150��.����`�  `� Mode$ TwoWay=� `� UpdateSourceTrigger$ Explicit=� `� 	Converter ,   &Servis.ConcatenateThreeStringConverter  .1���  �� Path$F @ServisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv��� $D >ServisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis��� $4 .ServisnaKnjizica.TipAutomobila.OpisTabela.Opis�.��
	 � Tag	 �� $ ServisnaKnjizicaID�
 �� UpdateSourceTrigger$
 Explicit=� �� Mode$ TwoWay=�.���� -   $��textBoxNapomena��$��1��$��4��$��3,3,3,3q� �� MinLines$ 2�� �� MaxLines$ 2�� �� 	MaxLength$	 500�� �� TextWrapping$
 Wrap=� �� AcceptsReturn .  " �� VerticalScrollBarVisibility$ Visible=�$	��250��.������  �� ElementName$ textBoxServisnaKnjizica��$ ActualWidth�$ OneWay=�.������ $ Napomena�$ TwoWay=�$
 Explicit=� �� 	Converter (   "Servis.PrazanStringToNullConverter  .���� -   $��textBoxKilometraza��$��1��$��1��$��3,3,3,3q�.������ $ Kilometraza�$
 Explicit=�$ TwoWay=� !   Servis.IntToStringConverter  .���� -   $��textBoxRegistarskiBroj��$��1��$��2��$��3,3,3,3q�$ 15��.������ $ RegistarskiBroj�$
 Explicit=�$ TwoWay=�   .��(  "System.Windows.Controls.DatePicker  -   $!��datePickerDatumRegistracije��$��1��$��3��$��3,3,3,3q�   SelectedDate �� $ DatumRegistracije�$ TwoWay=�$
 Explicit=��� $��Right=�$��1��$��5�� �� Orientation$ 
Horizontal=�$��0,30,5,5q�$��Bottom=�.+��� -   $��buttonDalje��$��4q�$��60��.��	Dalje>>�� -	   $��buttonOtkazi��$��4q�$��60��.��	Otkaži