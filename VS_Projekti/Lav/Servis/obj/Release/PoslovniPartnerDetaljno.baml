   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  $    Servis.PoslovniPartnerDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis       � Title$  PoslovniPartnerDetaljno�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.��� -   $��gridPoslovniPartner�� � RowDefinitions �� $
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
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��300���� $
��Auto �.+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1�� �� Orientation$ 
Horizontal=�.+��� $��	10,3,10,3q�$��Center=�	Šifra:�� -   $��checkBoxGenerisiSifru�� � ToolTip$ Unesi šifru��$��Center=� d� 	IsChecked$ FalseY��� $��0��$��2��$��	10,3,10,3q�$��Center=�Skraćen naziv:�� $��0��$��3��$��	10,3,10,3q�$��Center=�
Pun Naziv:�� $��0��$��4��$��	10,3,10,3q�$��Center=�Način organizacije firme:�� $��0��$��5��$��	10,3,10,3q�$��Center=�PIB:�� $��0��$��6��$��	10,3,10,3q�$��Center=�Matični broj:�� $��0��$��7��$��	10,3,10,3q�$��Center=�Žiro račun:�� $��0��$��8��$��	10,3,10,3q�$��Center=�Mesto:�� $��0��$��9��$��	10,3,10,3q�$��Center=�	Adresa:�� $��0��$��10��$��	10,3,10,3q�$��Center=�Kontakt osoba 1:�� $��0��$��11��$��	10,3,10,3q�$��Center=�
Telefon 1:�� $��0��$��12��$��	10,3,10,3q�$��Center=�	E-mail 1:�� $��0��$��13��$��	10,3,10,3q�$��Center=�Kontakt osoba 2:�� $��0��$��14��$��	10,3,10,3q�$��Center=�
Telefon 2:�� $��0��$��15��$��	10,3,10,3q�$��Center=�	E-mail 2:�� $��0��$��16��$��	10,3,10,3q�$��Center=�Faks:�� -   $��	textBoxID��$��1��$��0��$��3,3,3,3q�{�.  $	��150������  �� Path$ PoslovniPartnerID�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 Explicit=��� -   $��textBoxSifra��$��1��$��1��$��3,3,3,3q� �� 	MaxLength$ 50��$	��150��{���  �� ElementName$ checkBoxGenerisiSifru��$ 	IsChecked�.������ $ Sifra�$
 Explicit=�$	 TwoWay=�.���� -   $��textBoxSkracenNaziv��$��1��$��2��$��3,3,3,3q�$ 50��.������ $ SkracenNaziv�$
 Explicit=�$	 TwoWay=�.���� -   $��textBoxPunNaziv��$��1��$��3��$��3,3,3,3q�$	 200��.������ $ PunNaziv�$	 TwoWay=�$
 Explicit=� �� 	Converter (   "Servis.PrazanStringToNullConverter  .���� -   $$��comboBoxNacinOrganizacijeFirme��$��1��$��4��$��3,3,3,3q� w� TextPath$ Naziv������ .R��� ���� $ Naziv�$	 OneWay=� �� SelectedItem �� $ gridPoslovniPartner��$( "DataContext.NacinOrganizacijeFirme�$	 TwoWay=�$
 Explicit=� "   Servis.MestoToMestoConverter  �� -   $��
textBoxPIB��$��1��$��5��$��3,3,3,3q�.������ $	 PIB�$	 TwoWay=�$
 Explicit=� !   Servis.IntToStringConverter  .���� -	   $��textBoxMaticniBroj��$��1��$��6��$��3,3,3,3q�.������ $ MaticniBroj�$	 TwoWay=�$
 Explicit=�.���� -
   $��textBoxZiroRacun��$��1��$��7��$��3,3,3,3q�$	 100��.������ $ 	ZiroRacun�$	 TwoWay=�$
 Explicit=�   .���� -   $��comboBoxMesto��$��1��$��8��$��3,3,3,3q�$ Naziv������ .R��� ���� $ Naziv�$	 OneWay=� �� $ gridPoslovniPartner��$ DataContext.Mesto�$	 TwoWay=�$
 Explicit=�   �� -   $��textBoxAdresa��$��1��$��9��$��3,3,3,3q�$	 100��.������ $ Adresa�$	 TwoWay=�$
 Explicit=�   .���� -   $��textBoxKontaktOsoba1��$��1��$��10��$��3,3,3,3q�$	 100��.������ $ KontaktOsoba1�$	 TwoWay=�$
 Explicit=�   .���� -   $��textBoxTelefon1��$��1��$��11��$��3,3,3,3q�$ 50��.������ $ Telefon1�$	 TwoWay=�$
 Explicit=�   .���� -   $��textBoxEMail1��$��1��$��12��$��3,3,3,3q�$	 100��.������ $ EMail1�$	 TwoWay=�$
 Explicit=�   .���� -   $��textBoxKontaktOsoba2��$��1��$��13��$��3,3,3,3q�$	 100��.������ $ KontaktOsoba2�$	 TwoWay=�$
 Explicit=�   .���� -   $��textBoxTelefon2��$��1��$��14��$��3,3,3,3q�$ 50��.������ $ Telefon2�$	 TwoWay=�$
 Explicit=�   .���� -   $��textBoxEMail2��$��1��$��15��$��3,3,3,3q�$	 100��.������ $ EMail2�$	 TwoWay=�$
 Explicit=�   .���� -   $��textBoxFaks��$��1��$��16��$��3,3,3,3q�$ 50��.������ $
 Faks�$	 TwoWay=�$
 Explicit=�   .���� -   $��buttonSacuvajINovi��$��2��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -   $��buttonSacuvaj��$��2��$��1��$��4q�$��60��.��
Sačuvaj�� -   $��buttonSacuvajIZatvori��$��2��$��2��$��4q�$��60��.��Sačuvaj & Zatvori�� -   $��buttonServisnaKnjizica��$��2��$��3��$��4q�$��60��.��->Servisna knjizica�� -   $��buttonPonuda��$��2��$��4��$��4q�$��60��.��
->Ponuda�� -   $��buttonRadniNalog��$��2��$��5��$��4q�$��60��.��->Radni nalog