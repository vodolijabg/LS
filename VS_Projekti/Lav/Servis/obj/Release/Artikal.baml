   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis      Servis.Artikal   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis     -     N� Title$  Artikal�� N� 	KeepAlive .  � 	Resources    HeaderTemplateArrowUp F�Name  HeaderTemplateArrowDown ! listViewItem_Pozadina_Handler �� 
TargetType �� Property �� Value �� RelativeSource �� 	Converter'   !Servis.BackgroundConverterArtikal ) %listViewItem_MouseDoubleClick_Handler��%�  &	        &	 {     &	 �     &	 O    �� .R�]� .+��� $��Center=����� F� $ arrow��$��1��
�������+����@KL ��� ���� ��� р�� @KL �@KL ��� 	�� .R�]� .+��� $��Center=����� F� $ arrow��$��1��
�������+����@KL @KL р�� ��� ���� @KL �@KL @KL 	�� " ��.���  � ��$ Stretch=���  � �� ��  �� *Self+   �� " ��.�-   .��� 	 � ColumnDefinitions	 �� �� 
 � RowDefinitions
 �� $
��Auto ��� $��2* ��� $
��Auto ��� $��* ��� $
��Auto �.+��� -   $��0��$��0��$��2��.@��� $��Pretraga po brojevima��.��� 	 �� �� .+�� $��0��$��0�� � OverridesDefaultStyle .  	 �� $
��Auto ��� �� $
��Auto �
 �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��
10,10,0,10q�$��NoWrap=�Broj za pretragu�� -   $��comboBoxBrojZaPretragu��$��1��$��0��$��
10,10,5,10q� �� 
IsEditable . �� -   $��buttonNadji�� �� 	IsDefault . $��2��$��0��$��23��$��23��$��
5,10,10,10q�.���� $��Images/Search.ico�� � ToolTip$ Nađi��� $��1��$��1��	 �� $
��Auto ��� $
��Auto �.+��� -   $��checkBoxSlicnoTrazenje��$��0��$��	10,0,0,17q�$
��Left=�.��Slično traženje�� -   $��checkBoxSamoSaCenom��$��1��$��10,0,0,0q�$
��Left=� d� 	IsChecked$
 TrueY�.��Samo sa cenom�� $��1��$��0�� �� Orientation$ 
Horizontal=�.+��� .+��� -   $��checkBoxBiloKojiBroj��$��
10,10,10,0q�$
 TrueY�.��Bilo koji broj�� -	   $��checkBoxBrojProizvodjaca��$��	10,3,10,0q�.��Broj proizvođača�� -
   $��checkBoxOEBroj��$��	10,3,10,0q�.��	OE broj�� .+��� -   $��checkBoxKoriscenBroj��$��
10,10,10,0q�.��Korišćen broj�� -   $��checkBoxUporedniBroj��$��	10,3,10,0q�.��Uporedni broj�� -   $��checkBoxEanBroj��$��	10,3,10,0q�.��
Ean broj�� $��Pretraga roba��.��� 	 �� �� .+�� $��0��$��0�� .  	 �� $
��Auto ��� �� $
��Auto ��� �� $
��Auto �
 �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��	10,10,3,0q�$��NoWrap=�Šifra�� -   $��textBoxSifra��$��1��$��0��$��	3,10,3,10q��� $��0��$��1��$��
10,10,3,10q�$��NoWrap=�Naziv�� -   $��textBoxNaziv��$��1��$��1��$��	3,10,3,10q�$��3���� $��2��$��0��$��
10,10,3,10q�$��NoWrap=�Proizvođač�� -   $��comboBoxProizvodjacRoba��$��3��$��0��$��
10,10,3,10q� .   w� TextPath$ Naziv��$��23������ .R��� ����  �� Path$ Naziv� �� Mode$ OneWay=��� -   $��buttonNadjiRoba�� . $��4��$��1��$��23��$��23��$��
10,10,3,10q�.���� $��Images/Search.ico��$ Nađi���� $��Pretraga akumulatora��.��� 	 �� �� .+�� $��0��$��0�� .  	 �� $
��Auto ��� �� $
��Auto �
 �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��	10,10,3,0q�$��NoWrap=�Proizvođač�� -   $$��comboBoxProizvodjacAkumulatora��$��1��$��0��$��3,10,3,0q� .  $ Naziv��$��23������ .R��� ���� $ Naziv�$ OneWay=��� $��0��$��1��$��
10,10,3,10q�$��NoWrap=�Amperaža (± 5Ah)�� -   $ ��textBoxAmperazaAkumulatora��$��1��$��1��$��	3,10,3,10q��� -   $��buttonNadjiAkumulator�� . $��2��$��1��$��23��$��23��$��
10,10,3,10q�.���� $��Images/Search.ico��$ Nađi���� $��Pretraga ulja��.��� 	 �� �� .+�� $��0��$��0�� .  	 �� $
��Auto ��� �� $
��Auto �
 �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��	10,10,3,0q�$��NoWrap=�Proizvođač�� -   $��comboBoxProizvodjacUlja��$��1��$��0��$��3,10,3,0q� .  $ Naziv��$��23������ .R��� ���� $ Naziv�$ OneWay=��� $��0��$��1��$��
10,10,3,10q�$��NoWrap=�
Viskozitet�� -   $��comboBoxViskozitetUlja��$��1��$��1��$��	3,10,3,10q� .  $ Vrednost������ .R��� ���� $ Vrednost�$ OneWay=��� -   $��buttonNadjiUlje�� . $��2��$��1��$��23��$��23��$��
10,10,3,10q�.���� $��Images/Search.ico��$ Nađi���� $��Pretraga guma��.��� 	 �� �� .+�� $��0��$��0�� .  	 �� $
��Auto ��� �� $
��Auto ��� �� $
��Auto �
 �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0��$��0��$��	10,10,3,0q�$��NoWrap=�Proizvođač�� -   $��comboBoxProizvodjacGuma��$��1��$��0��$��3,10,3,0q� .  $ Naziv��$��23������ .R��� ���� $ Naziv�$ OneWay=��� $��0��$��1��$��
10,10,3,10q�$��NoWrap=�Namena�� -   $��comboBoxNamenaGuma��$��1��$��1��$��	3,10,3,10q� .  $ Vrednost������ .R��� ���� $ Vrednost�$ OneWay=��� $��2��$��0��$��	10,10,3,0q�$��NoWrap=�Sezona�� -   $��comboBoxSezonaGuma��$��3��$��0��$��3,10,3,0q� .  $ Vrednost��$��23������ .R��� ���� $ Vrednost�$ OneWay=��� $��2��$��1��$��
10,10,3,10q�$��NoWrap=�	Dimenzija�� -   $��textBoxDimenzijaGume��$��3��$��1��$��	3,10,3,10q��� -   $��buttonNadjiGumu�� . $��4��$��1��$��23��$��23��$��
10,10,3,10q�.���� $��Images/Search.ico��$ Nađi���� -   $��listViewArtikal��$��0��$��2��$��1��$��0,3,3,3q� �� SelectionMode$ Single=�#��[  �� View ��  �� AllowsColumnReorder . P���  �� CellTemplate �� .R��� $��buttonArtikalDetaljno��-   $��0q�
 � Tag �� .��...��  �� Width$ 0������ $��Izvor��$
��Left=�$ IzvorPodatakaID�� �� .R��� ���� $ IzvorPodatakaID��� $ 0������ $��ID��$
��Left=�$ 
Artikal_ID�� �� .R��� ���� $ 
Artikal_ID��� $	 100������ $��Proizvođač��$
��Left=�$ Proizvodjac.Naziv�� �� .R��� $��textBlockProizvodjac�� �� TextAlignment$
 Left=����� $ Proizvodjac.Naziv��� $	 150������ $��Broj proizvođača��$
��Left=�$ BrojProizvodjaca�� �� .R��� $��1��$��	10,3,10,3q�$��Center=�$
��Left=� �� 
IsReadOnly . $��0q�.������ $ BrojProizvodjaca�$ OneWay=�.������  �� ElementName$ textBlockProizvodjac��$ SelectedItem.Background�.������ $ textBlockProizvodjac��$ 
Foreground�.���� $	 100������ $��Šifra��$
��Left=�$ Sifra�� �� .R��� $��1��$��0,3,10,3q�$��Center=�$
��Left=� . $��0q�.������ $ Sifra�$ OneWay=�.������ $ textBlockProizvodjac��$ SelectedItem.Background�.������ $ textBlockProizvodjac��$ 
Foreground�.���� $	 200������ $��Naziv��$
��Left=�$ OpisTabela.Opis�� �� .R��� ���� $ OpisTabela.Opis��� $	 100������ $
��Cena��$
��Left=�$  NajpovoljnijiDobavljacCena�� �� .R��� $��Right=�$��0,0,10,0q����� $  NajpovoljnijiDobavljacCena� %   Servis.DecimalToStringConverter  �� $	 110������ $��Količina na stanju��$
��Left=�$$ NajpovoljnijiDobavljacKolicina�� �� .R��� $��Right=�$��0,0,10,0q����� $$ NajpovoljnijiDobavljacKolicina�   �� $	 150������ $��Najpovoljniji dobavljač��$
��Left=�$! NajpovoljnijiDobavljacNaziv�� �� .R��� ���� $! NajpovoljnijiDobavljacNaziv��� $	 200������ $��Napomena��$
��Left=�$ Napomena�� �� .R��� ���� $ Napomena��� $��0��$��2��$��2��$��3��$��Center=�$��Stretch=��� -   $��listViewDobavljaci��$��0��$��2��$��3��$��0,3,3,3q�$ Single=�#��[  ��  . P���  �� .R��� $��buttonDobavljacDetaljno��-    $��0q� �� .��...y��� $ buttonDobavljacDetaljno��$	 Tag� 5   /Servis.DaLiJeDobavljacKorisnikProgramaConverter  �� $	 150������ $��
Dobavljač��$
��Left=� �� .R��� ��`�  `� 	Converter &    Servis.DajNazivPartneraConverter   `� ConverterParameter$ -��.1��� $" PoslovniPartner.SkracenNaziv��� $ KorisnikPrograma.Naziv��� $	 100������ $
��Cena��$
��Left=� �� .R��� $��Right=�$��0,0,10,0q����� $
 Cena�   �� $	 110������ $��Količina na stanju��$
��Left=� �� .R��� ���� $ KolicinaNaStanju�   �� $	 150������ $��Datum ažuriranja��$
��Left=� �� .R��� ���� $ DatumAzuriranja� �� ConverterParameter$ g�� &    Servis.DateTimeToStringConverter    � ContextMenu  �� -!   $��contextMenuDodaj��$y�Hidden=�.@�m� -"   $��Dodaj���� $��0��$��4��$��2��$ 
Horizontal=�$��Stretch=�$��0q�$��30������ ! �� 
StartPoint$
! 0, 0&�" �� EndPoint$	" 0,1&�.��� # � Color$# #E3EFFF��$ � Offset$$ 0Q�� $# #C4DDFF��$$ 0.40625Q�� $# #ADD1FF��$$ 0.40625Q�� $# #C0DBFF��$$ 1Q�.+��� $��Center=�$��10,0,0,0q�)'Rezultat pretrage - ukupan broj redova:�� -#   $��textBlockBrojRedova��$��Center=�$��10,0,0,0q����� $ listViewArtikal��$ Items.Count�