   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis      Servis.FizickoLice   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis     -     N� Title$  FizickoLice�� N� 	KeepAlive .  � 	Resources  )  %listViewItem_MouseDoubleClick_Handler �� 
TargetType �� Property �� Value��%7   &	        �� " ��.�-   ��  � ��$ Stretch=�.���  � RowDefinitions �� $
��auto ���  � ColumnDefinitions �� .+��� $��0��$��0�� �� Orientation$ 
Horizontal=�$��Stretch=�$��0q�$��28������ 	 �� 
StartPoint$
	 0, 0&�
 �� EndPoint$	
 0,1&�.���  � Color$ #E3EFFF�� � Offset$ 0Q�� $ #C4DDFF��$ 0.40625Q�� $ #ADD1FF��$ 0.40625Q�� $ #C0DBFF��$ 1Q�.+��� -   $��buttonDodaj��.���� $��Images/Add.ico�� � ToolTip$ Dodaj���� -   $��buttonIzmeni��.���� $��Images/Edit.ico��$ Izmeni���� -   $��buttonObrisi��.���� $��Images/Delete.ico��$ Obrisi���� -   $��buttonOsvezi��.���� $��Images/Refresh.ico��$ Osvezi���� $��Center=�$��10,0,5,0q�	Traži za�� -   $��textBoxTraziZa��$	��100��$��0,2,0,2q��� $��Center=�$��3,0,3,0q�u�� -   $��comboBoxFizickoLiceKolone��$��0,2,5,2q�$	��150�� �� SelectedIndex$ 2��.@��� .��ID�� .��Šifra�� .��Ime�� .��	Prezime�� .��Mesto�� .��Adresa�� .��
Telefon1�� .��E-mail�� .��
Raspored�� -	   $��buttonNadji�� �� 	IsDefault . .���� $��Images/Search.ico��$ Nadji���� -
   $��listViewFizickoLice��$��1��$��0,3,3,3q� �� SelectionMode$ Single=�#��[   �� View ��  �� AllowsColumnReorder . P���  �� Width$ 50������ $��ID��$
��Left=� �� CellTemplate �� .R��� ����  �� Path$ FizickoLiceID��� $ 50������ $��Šifra��$
��Left=� �� .R��� ���� $ Sifra��� $	 100������ $	��Ime��$
��Left=� �� .R��� ���� $	 Ime��� $	 100������ $��Prezime��$
��Left=� �� .R��� ���� $ Prezime��� $ 50������ $��Kupac��$
��Left=� �� .R��� {�.   d� 	IsChecked �� $ RegistrovanKupac��� $	 100������ $��Mesto��$
��Left=� �� .R��� ���� $ Mesto.Naziv��� $	 200������ $��Adresa��$
��Left=� �� .R��� ���� $ Adresa��� $	 100������ $��	Telefon 1��$
��Left=� �� .R��� ���� $ Telefon1� �� ConverterParameter$ ___/__-__-________�� �� 	Converter )   #Servis.TelefonMaskKonverterReadOnly  �� $	 100������ $��	Telefon 2��$
��Left=� �� .R��� ���� $ Telefon2�$ ___/__-__-________��   �� $	 100������ $��E-mail��$
��Left=� �� .R��� ���� $ EMail� &    Servis.DateTimeToStringConverter  