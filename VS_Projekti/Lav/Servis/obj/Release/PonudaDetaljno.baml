   M S B A M L   `   `   `  ���� g  cMicrosoft.ReportViewer.WinForms, Version=10.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3atSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinFormsMicrosoft.Reporting.WinForms  B >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis    Servis.PonudaDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     crptSclr-namespace:Microsoft.Reporting.WinForms;assembly=Microsoft.ReportViewer.WinForms     &localclr-namespace:Servis     -     � Title$  PonudaDetaljno�� � SizeToContent$ Manual=� � 
ResizeMode$ 	CanResize=� � 	Resources  )  %listViewItem_MouseDoubleClick_Handler �� 
TargetType �� Property �� Value��%7   &	        �� " ��.�-   ��  � ��$ Stretch=�.���� -   .@��� -   $��tabItemDokument��$��Ponuda��.��� -   $��
gridPonuda�����	#FF9CAAC1 � RowDefinitions �� $
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
��Auto ��� .+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1��$��	10,3,10,3q�$��Center=�Servisna knjižica:�� $��0��$��2��$��	10,3,10,3q�$��Center=�Ponudu uneo:�� $��0��$��3��$��	10,3,10,3q�$��Center=�	Napomena:�� $��2��$��0��$��	10,3,10,3q�$��Center=�Način zahteva za ponudu:�� $��2��$��1��$��	10,3,10,3q�$��Center=�Preuzima lično:�� $��2��$��2��$��	10,3,10,3q�$��Center=�Obavesti telefonom:�� $��2��$��3��$��	10,3,10,3q�$��Center=�Pošalji e-mail:�� $��2��$��4��$��	10,3,10,3q�$��Center=�Pošalji sms obaveštenje:� $��1��$��0�� �� $��2* ��� $��3* �.+��� -   $��	textBoxID��$��0��$��0��$��3,3,3,3q�{�.  $	��150������ 	 �� Path$	 PonudaID�
 �� Mode$
 TwoWay=� �� UpdateSourceTrigger$ Explicit=��� -   $��textBoxVreme��$��1��$��0��$��3,3,3,3q�{�.  .������ $	 Vreme�$
 TwoWay=�$ Explicit=� �� ConverterParameter$ g�� �� 	Converter &   Servis.DateTimeToStringConverter  .��� -   $��gridServisnaKnjizica��$��1��$��1�� �� $��* ��� $��4* ��� $
��Auto ��� $
��Auto �.+��� -	   $"��textBoxServisnaKnjizicaSifra��$��0��$��0��$��3,3,3,3q� �� 
IsReadOnly .  .������ $	 ServisnaKnjizica.Sifra�$ Explicit=�$
 TwoWay=�.���� -
   $��textBoxServisnaKnjizica��$��1��$��0��$��3,3,3,3q� . $	��150��.����`�  `� Mode$ TwoWay=� `� UpdateSourceTrigger$ Explicit=� `� 	Converter ,  &Servis.ConcatenateThreeStringConverter  .1��� $F	 @ServisnaKnjizica.TipAutomobila.ModelAutomobila.Proizvodjac.Naziv��� $D	 >ServisnaKnjizica.TipAutomobila.ModelAutomobila.OpisTabela.Opis��� $4	 .ServisnaKnjizica.TipAutomobila.OpisTabela.Opis�.��
 � Tag �� $	 ServisnaKnjizicaID�$ Explicit=�$
 TwoWay=�.���� -   $��buttonServisnaKnjizica��$��2��$��0��$��3,3,3,3q�$��23��.��...�� -   $'��!buttonFizickoLiceServisnaKnjizica��$��3��$��0��$��3,3,3,3q�$��23��.��+�� -   $��textBoxRadnik��$��1��$��2��$��3,3,3,3q� . .������ $	 Radnik.Nadimak�$ Explicit=�$
 TwoWay=�.���� -   $��textBoxNapomena��$��1��$��3��$��2��$��3,3,3,3q� �� MaxLines$ 2��$
��Left=� �� 	MaxLength$	 500�� �� TextWrapping$
 Wrap=� �� AcceptsReturn .  " �� VerticalScrollBarVisibility$ Visible=�.������  �� ElementName$ textBoxRadnik��$	 ActualWidth�$
 OneWay=�.������ $	 Napomena�$
 TwoWay=�$ Explicit=� (  "Servis.PrazanStringToNullConverter  .���� -   $"��comboBoxNacinZahtevaZaPonudu��$��3��$��0��$��3,3,3,3q� w� TextPath$ Naziv������ .R��� ���� $	 Naziv�$
 OneWay=� �� SelectedItem �� $ 
gridPonuda��$&	  DataContext.NacinZahtevaZaPonudu�$
 TwoWay=�$ Explicit=� @  :Servis.NacinZahtevaZaPonuduToNacinZahtevaZaPonuduConverter  �� $��3��$��1�� �� Orientation$ 
Horizontal=�.+��� -   $��checkBoxPreuzimaLicno��$��Center=�$��3,3,3,3q� d� 	IsChecked �� $	 PreuzimaLicno�$
 TwoWay=�$ Explicit=��� $��Center=�$��3,3,13,3q�	Preuzeto:�� -   $��checkBoxPreuzeoLicno��$��Center=�$��3,3,3,3q� �� $	 PreuzeoLicnoU�$
 TwoWay=�$ Explicit=� $  Servis.DateTimeToBoolConverter  �� -   $��textBlockPreuzeoLicnoU��$��Center=�$��3,3,3,3q����� $	 PreuzeoLicnoU�$
 TwoWay=�$ Explicit=�$ g��   �� $��3��$��2��$ 
Horizontal=�.+��� -   $��checkBoxObavestiTelefonom��$��Center=�$��3,3,3,3q� �� $	 ObavestiTelefonom�$
 TwoWay=�$ Explicit=��� $��Center=�$��3,3,3,3q�Obavešten:�� -   $ ��checkBoxObavestenTelefonom��$��Center=�$��3,3,3,3q� �� $	 ObavestenTelefonomU�$
 TwoWay=�$ Explicit=�   �� -   $"��textBlockObavestenTelefonomU��$��Center=�$��3,3,3,3q����� $	 ObavestenTelefonomU�$
 TwoWay=�$ Explicit=�$ g��   �� $��3��$��3��$ 
Horizontal=�.+��� -   $��checkBoxPosaljiEMail��$��Center=�$��3,3,3,3q� �� $	 PosaljiEMail�$
 TwoWay=�$ Explicit=��� $��Center=�$��3,3,27,3q�	Poslat:�� -   $��checkBoxPoslatEMail��$��Center=�$��3,3,3,3q� �� $	 PoslatEMailU�$
 TwoWay=�$ Explicit=�   �� -   $��textBlockPoslatEMailU��$��Center=�$��3,3,3,3q����� $	 PoslatEMailU�$
 TwoWay=�$ Explicit=�$ g��   �� $��3��$��4��$ 
Horizontal=�.+��� -   $#��checkBoxPesaljiSmsObavestenje��$��Center=�$��3,3,3,3q�{�.   �� $	 PosaljiSMSObavestenje�$
 TwoWay=�$ Explicit=��� $��Center=�$��3,3,21,3q�
Poslato:�� -   $#��checkBoxPeslatoSmsObavestenje��$��Center=�$��3,3,3,3q�{�.   �� $	 PoslatoSMSObavestenjeU�$
 TwoWay=�$ Explicit=�   �� -   $%��textBlockPoslatoSmsObavestenjeU��$��Center=�$��3,3,3,3q����� $	 PoslatoSMSObavestenjeU�$
 TwoWay=�$ Explicit=�$ g��   �� -   $��buttonSacuvajINovi��$��4��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -   $��buttonSacuvaj��$��4��$��1��$��4q�$��60��.��
Sačuvaj�� -   $��buttonSacuvajIZatvori��$��4��$��2��$��4q�$��60��.��Sačuvaj & Zatvori�� -   $��buttonKreirajRadniNalog��$��4��$��3��$��4q�$��60��.��Kreiraj Radni nalog�� $��0��$��6��$��6��$ 
Horizontal=�$��Stretch=�$��0,10,0,0q�$��28������  �� 
StartPoint$
 0, 0&� �� EndPoint$	 0,1&�.���  � Color$ #FFBD69��  � Offset$  0Q�� $ #FFAC42��$  0.40625Q�� $ #FB8C3C��$  0.40625Q�� $ #FED364��$  1Q�.+��� -    $��buttonDodajStavkaUsluga��.���� $��Images/Add.ico��! � ToolTip$! Dodaj���� -!   $��buttonIzmeniStavkaUsluga��.���� $��Images/Edit.ico��$! Izmeni���� -"   $��buttonObrisiStavkaUsluga��.���� $��Images/Delete.ico��$! Obrisi���� -#   $��buttonOsveziStavkaUsluga��.���� $��Images/Refresh.ico��$! Osvezi���� -$   $��listViewStavkaUsluga��$��0��$��7��$��6��$��0q�#��[  " �� SelectionMode$" Single=�# �� View# �� $ �� AllowsColumnReorder$ . P��� % �� Width$% 0������ $��ID��$
��Left=�& �� CellTemplate& �� .R��� ���� $	 StavkaUslugaID��� $	% 100������ $��Vrsta usluge��$
��Left=�& �� .R��� ���� $	 Usluga.VrstaUsluge.Naziv��� $	% 150������ $��Nosilac grupe��$
��Left=�& �� .R��� ���� $	 Usluga.NosilacGrupe.Naziv��� $% 50������ $
��Nivo��$
��Left=�& �� .R��� ���� $	 Usluga.Nivo.Naziv��� $	% 100������ $��Pozicija��$
��Left=�& �� .R��� ���� $	 Usluga.Pozicija.Naziv��� $	% 100������ $��	Količina��$
��Left=�& �� .R��� ���� $	 UslugaKolicina��� $	% 100������ $��Cena bez poreza��$
��Left=�& �� .R��� $��Right=�$��0,0,10,0q����� $	 UslugaCenaBezPoreza� %  Servis.DecimalToStringConverter  �� $	% 100������ $��Poreska stopa��$
��Left=�& �� .R��� ���� $$	 PoreskaStopa.VrednostProcenata��� $% 0������ $��Status��$
��Left=�& �� .R��� ���� $	 Status��� $% 0������ $��Vreme promene��$
��Left=�& �� .R��� ���� $	 VremePromene�$ g��   �� $% 0������ $��Korisnički nalog��$
��Left=�& �� .R��� ���� $	 KorisnickiNalog��� $��0��$��9��$��6��$ 
Horizontal=�$��Stretch=�$��0,0,0,0q�$��28������ $
 0, 0&�$	 0,1&�.��� $ #FFBD69��$  0Q�� $ #FFAC42��$  0.40625Q�� $ #FB8C3C��$  0.40625Q�� $ #FED364��$  1Q�.+��� -%   $��buttonDodajStavkaArtikal��.���� $��Images/Add.ico��$! Dodaj���� -&   $��buttonIzmeniStavkaArtikal��.���� $��Images/Edit.ico��$! Izmeni���� -'   $��buttonObrisiStavkaArtikal��.���� $��Images/Delete.ico��$! Obrisi���� -(   $��buttonOsveziStavkaArtikal��.���� $��Images/Refresh.ico��$! Osvezi���� -)   $��listViewStavkaArtikal��$��0��$��10��$��6��$��0q�#��[  $" Single=�# �� $ . P��� $% 0������ $��ID��$
��Left=�& �� .R��� ���� $	 StavkaArtikalID��� $	% 150������ $��Nosilac grupe��$
��Left=�& �� .R��� ���� $	 NosilacGrupe.Naziv��� $	% 150������ $��
Dobavljač��$
��Left=�& �� .R��� ��`�  &   Servis.DajNazivPartneraConverter  ' `� ConverterParameter$' -��.1��� $"	 PoslovniPartner.SkracenNaziv��� $	 KorisnikPrograma.Naziv��� $	% 150������ $��Proizvodjač��$
��Left=�& �� .R��� ���� $	 ArtikalProizvodjacNaziv��� $	% 150������ $��Broj proizvodjača��$
��Left=�& �� .R��� ���� $	 ArtikalBrojProizvodjaca��� $	% 150������ $��Naziv��$
��Left=�& �� .R��� ���� $	 ArtikalNaziv��� $	% 100������ $��	Količina��$
��Left=�& �� .R��� ���� $	 ArtikalKolicina��� $	% 100������ $��Cena bez poreza��$
��Left=�& �� .R��� $��Right=�$��0,0,10,0q����� $	 ArtikalCenaBezPoreza�   �� $	% 100������ $��Cena sa porezom��$
��Left=�& �� .R��� $��Right=�$��0,0,10,0q���`�  "  Servis.DajCenuSaPDVConverter  .1��� $	 ArtikalCenaBezPoreza�$
 OneWay=��� $$	 PoreskaStopa.VrednostProcenata�$
 OneWay=��� $% 0������ $��Status��$
��Left=�& �� .R��� ���� $	 Status��� $% 0������ $��Vreme promene��$
��Left=�& �� .R��� ���� $	 VremePromene�$ g��   �� $% 0������ $��Korisnički nalog��$
��Left=�& �� .R��� ���� $	 KorisnickiNalog��� -*   $��tabItemStampa��$��Štampa��.���  �� $
��Auto ��� $��* � �� $��* �.+��� -+   $��buttonPosaljiEMail��$��0��$��0��$��0,3,0,3q�.��%#Pošalji ponudu na e-mail korisnika7	  1System.Windows.Forms.Integration.WindowsFormsHost	  $��0��$��1��( 	  Child.( /
   )Microsoft.Reporting.WinForms.ReportViewer
  -,   reportViewerPonuda��