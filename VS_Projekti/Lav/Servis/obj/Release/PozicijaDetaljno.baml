   M S B A M L   `   `   `  ���� 
  Servis    Servis.PozicijaDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml       � Title$  PozicijaDetaljno�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.��� -   $��gridPozicija�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1�� �� Orientation$ 
Horizontal=�.+��� $��	10,3,10,3q�$��Center=�	Šifra:�� -   $��checkBoxGenerisiSifru�� � ToolTip$ Unesi šifru��$��Center=� d� 	IsChecked$ FalseY��� $��0��$��2��$��	10,3,10,3q�$��Center=�Naziv:�� -   $��	textBoxID��$��1��$��0��$��3,3,3,3q�{�.  $	��150������  �� Path$ 
PozicijaID�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 Explicit=��� -   $��textBoxSifra��$��1��$��1��$��3,3,3,3q� �� 	MaxLength$ 50��$	��150��{���  �� ElementName$ checkBoxGenerisiSifru��$ 	IsChecked�.������ $ Sifra�$
 Explicit=�$	 TwoWay=�.���� -   $��textBoxNaziv��$��1��$��2��$��3,3,3,3q�$ 50��.������ $ Naziv�$
 Explicit=�$	 TwoWay=�.���� -   $��buttonSacuvajINovi��$��2��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -   $��buttonSacuvaj��$��2��$��1��$��4q�$��60��.��
Sačuvaj�� -   $��buttonSacuvajIZatvori��$��2��$��2��$��4q�$��60��.��Sačuvaj & Zatvori