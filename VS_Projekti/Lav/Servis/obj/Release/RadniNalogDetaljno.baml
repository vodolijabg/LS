   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  g cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms     Servis.RadniNalogDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis     crptSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms     -     � Title$  RadniNalogDetaljno�� � SizeToContent$ Manual=� � 
ResizeMode$ 	CanResize=� � 	Resources  )  %listViewItem_MouseDoubleClick_Handler �� 
TargetType �� Property �� Value��%7   &	        �� " ��.�-   ��  � ��$ Stretch=�.���� -   .@��� -   $��tabItemDokument��$��
RadniNalog��.��� -   $��gridRadniNalog�����	#FF9CAAC1 � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $��* ��� $
��Auto ��� $
��Auto ��� $��* � � ColumnDefinitions �� $
��Auto ��� $	��250 ��� $
��Auto ��� $	��250 ��� $
��Auto ��� .+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1��$��	10,3,10,3q�$��Center=�Servisna knjižica:�� $��0��$��2��$��	10,3,10,3q�$��Center=�Ponudu uneo:�� $��0��$��3��$��	10,3,10,3q�$��Center=�Vreme kreiranja:�� $��2��$��0��$��	10,3,10,3q�$��Center=�Kilometraža:�� $��2��$��1��$��	10,3,10,3q�$��Center=�Registarski broj:�� $��2��$��2��$��	10,3,10,3q�$��Center=�Datum registracije:�� $��2��$��3��$��	10,3,10,3q�$��Center=�Predvidjeno vreme (minuta):�� $��0��$��4��$��	10,3,10,3q�$��Center=�	Napomena:�� -   $��	textBoxID��$��1��$��0��$��3,3,3,3q�{�.  $	��150������ 	 �� Path$	 RadniNalogID�
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ Explicit=�� -   $��gridServisnaKnjizica��$��1��$��1�� �� �� $
��Auto �.+��� -   $��textBoxServisnaKnjizica��$��0��$��0��$��3,3,3,3q� �� 
IsReadOnly . $	��150��.����`�  `� Mode$ TwoWay=� `� UpdateSourceTrigger$ Explicit=� `� 	Converter ,   &Servis.ConcatenateThreeStringConverter  .1��� $F	 @ServisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv��� $D	 >ServisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis��� $4	 .ServisnaKnjizica.TipAutomobila.OpisTabela.Opis�.��
 � Tag �� $	 ServisnaKnjizicaID�$ Explicit=�$
 TwoWay=�.���� -	   $��buttonServisnaKnjizica��$��1��$��0��$��3,3,3,3q�$��23��.��...�� -
   $��textBoxRadnik��$��1��$��2��$��3,3,3,3q� . .������ $	 Radnik.Nadimak�$ Explicit=�$
 TwoWay=�.���� -   $��textBoxVreme��$��1��$��3��$��3,3,3,3q� . .������ $	 Vreme�$
 TwoWay=�$ Explicit=� �� ConverterParameter$ g�� �� 	Converter &    Servis.DateTimeToStringConverter  .���� -   $��textBoxNapomena��$��1��$��4��$��3��$��3,3,3,3q� �� MaxLines$ 2��$
��Left=� �� 	MaxLength$	 500�� �� TextWrapping$
 Wrap=� �� AcceptsReturn .  " �� VerticalScrollBarVisibility$ Visible=�$	��650��$	��650��.������ $	 Napomena�$
 TwoWay=�$ Explicit=� (   "Servis.PrazanStringToNullConverter  .���� -   $��textBoxKilometraza��$��3��$��0��$��3,3,3,3q�.������ $	 Kilometraza�$ Explicit=�$
 TwoWay=� !   Servis.IntToStringConverter  .���� -   $��textBoxRegistarskiBroj��$��3��$��1��$��3,3,3,3q�$ 15��.������ $	 RegistarskiBroj�$ Explicit=�$
 TwoWay=�   .��(  "System.Windows.Controls.DatePicker  -   $!��datePickerDatumRegistracije��$��3��$��2��$��3,3,3,3q�   SelectedDate �� $	 DatumRegistracije�$
 TwoWay=�$ Explicit=��� -   $#��textBoxPredvidjenoVremeMinuta��$��3��$��3��$��3,3,3,3q� . .����`� $ Explicit=�$ TwoWay=� /   )Servis.DajPredvidjenoVremeMinutaConverter  .1��� $	 StavkaUslugas�.���� -   $��buttonSacuvajINovi��$��4��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -   $��buttonSacuvaj��$��4��$��1��$��4q�$��60��.��
Sačuvaj�� -   $��buttonSacuvajIZatvori��$��4��$��2��$��4q�$��60��.��Sačuvaj & Zatvori�� -   $��buttonRezervisiDelove��$��4��$��3��$��4q�$��60��.��Rezerviši delove�� -   $��buttonZakljucaj��$��4��$��4��$��4q�$��60��.��
Zaključaj�� $��0��$��6��$��6�� �� Orientation$ 
Horizontal=�$��Stretch=�$��0,10,0,0q�$��28������  �� 
StartPoint$
 0, 0&� �� EndPoint$	 0,1&�.���  � Color$ #FFBD69�� � Offset$ 0Q�� $ #FFAC42��$ 0.40625Q�� $ #FB8C3C��$ 0.40625Q�� $ #FED364��$ 1Q�.+��� -   $��buttonDodajStavkaUsluga��.���� $��Images/Add.ico�� � ToolTip$ Dodaj���� -   $��buttonIzmeniStavkaUsluga��.���� $��Images/Edit.ico��$ Izmeni���� -   $��buttonObrisiStavkaUsluga��.���� $��Images/Delete.ico��$ Obrisi���� -   $��buttonOsveziStavkaUsluga��.���� $��Images/Refresh.ico��$ Osvezi���� -   $��listViewStavkaUsluga��$��0��$��7��$��6��$��0q�#��[   �� SelectionMode$ Single=�  �� View  �� ! �� AllowsColumnReorder! . P��� " �� Width$" 50������ $��ID��$
��Left=�# �� CellTemplate# �� .R��� ���� $	 StavkaUslugaID��� $	" 100������ $��Vrsta usluge��$
��Left=�# �� .R��� ���� $	 Usluga.VrstaUsluge.Naziv��� $	" 150������ $��Nosilac grupe��$
��Left=�# �� .R��� ���� $	 Usluga.NosilacGrupe.Naziv��� $	" 100������ $
��Nivo��$
��Left=�# �� .R��� ���� $	 Usluga.Nivo.Naziv��� $	" 100������ $��Status��$
��Left=�# �� .R��� ���� $3	 -RadniNalogStavkaUsluga.RadniNalogStatus.Naziv��� $	" 160������ $ ��Predvidjeno vreme (Minuta)��$
��Left=�# �� .R��� ���� $3	 -RadniNalogStavkaUsluga.PredvidjenoVremeMinuta��� $	" 150������ $��Utrošeno vreme (Minuta)��$
��Left=�# �� .R��� ���� $0	 *RadniNalogStavkaUsluga.UtrosenoVremeMinuta��� $	" 100������ $��	Količina��$
��Left=�# �� .R��� ���� $	 UslugaKolicina��� $	" 100������ $��Cena bez poreza��$
��Left=�# �� .R��� $��Right=�$��0,0,10,0q����� $	 UslugaCenaBezPoreza� %   Servis.DecimalToStringConverter  �� $	" 100������ $��Poreska stopa��$
��Left=�# �� .R��� ���� $$	 PoreskaStopa.VrednostProcenata��� $" 0������ $��Status��$
��Left=�# �� .R��� ���� $	 Status��� $" 0������ $��Vreme promene��$
��Left=�# �� .R��� ���� $	 VremePromene�$ g��   �� $" 0������ $��Korisnički nalog��$
��Left=�# �� .R��� ���� $	 KorisnickiNalog��� $��0��$��9��$��6��$ 
Horizontal=�$��Stretch=�$��0,0,0,0q�$��28������ $
 0, 0&�$	 0,1&�.��� $ #FFBD69��$ 0Q�� $ #FFAC42��$ 0.40625Q�� $ #FB8C3C��$ 0.40625Q�� $ #FED364��$ 1Q�.+��� -   $��buttonDodajStavkaArtikal��.���� $��Images/Add.ico��$ Dodaj���� -   $��buttonIzmeniStavkaArtikal��.���� $��Images/Edit.ico��$ Izmeni���� -   $��buttonObrisiStavkaArtikal��.���� $��Images/Delete.ico��$ Obrisi���� -   $��buttonOsveziStavkaArtikal��.���� $��Images/Refresh.ico��$ Osvezi���� -   $��listViewStavkaArtikal��$��0��$��10��$��6��$��0q�#��[  $ Single=�  �� ! . P��� $" 50������ $��ID��$
��Left=�# �� .R��� ���� $	 StavkaArtikalID��� $	" 150������ $��
Dobavljač��$
��Left=�# �� .R��� ��`�  &    Servis.DajNazivPartneraConverter  $ `� ConverterParameter$$ -��.1��� $"	 PoslovniPartner.SkracenNaziv��� $	 KorisnikPrograma.Naziv��� $	" 150������ $��Proizvodjač��$
��Left=�# �� .R��� ���� $	 ArtikalProizvodjacNaziv��� $	" 150������ $��Broj proizvodjača��$
��Left=�# �� .R��� ���� $	 ArtikalBrojProizvodjaca��� $	" 150������ $��Naziv��$
��Left=�# �� .R��� ���� $	 ArtikalNaziv��� $	" 100������ $��	Količina��$
��Left=�# �� .R��� ���� $	 ArtikalKolicina��� $	" 100������ $��Cena bez poreza��$
��Left=�# �� .R��� $��Right=�$��0,0,10,0q����� $	 ArtikalCenaBezPoreza�   �� $	" 100������ $��Cena sa porezom��$
��Left=�# �� .R��� $��Right=�$��0,0,10,0q���`�  "	   Servis.DajCenuSaPDVConverter	  .1��� $	 ArtikalCenaBezPoreza�$
 OneWay=��� $$	 PoreskaStopa.VrednostProcenata�$
 OneWay=��� $" 0������ $��Poreska stopa��$
��Left=�# �� .R��� ���� $$	 PoreskaStopa.VrednostProcenata��� $" 0������ $��Status��$
��Left=�# �� .R��� ���� $	 Status��� $" 0������ $��Vreme promene��$
��Left=�# �� .R��� ���� $	 VremePromene�$ g��   �� $" 0������ $��Korisnički nalog��$
��Left=�# �� .R��� ���� $	 KorisnickiNalog��� -    $��tabItemStampa��$��Štampa��.��7
  1System.Windows.Forms.Integration.WindowsFormsHost
  % 
  Child.% /  )Microsoft.Reporting.WinForms.ReportViewer  -!   reportViewerRadniNalog��