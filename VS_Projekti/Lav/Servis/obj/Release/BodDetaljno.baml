   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis      Servis.BodDetaljno   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis       � Title$  BodDetaljno�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.��� -   $��gridBod�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� �� $
��Auto �.+��� $��0��$��0��$��	10,3,10,3q�$��Center=�ID:�� $��0��$��1�� �� Orientation$ 
Horizontal=�.+��� $��	10,3,10,3q�$��Center=�	Šifra:�� -   $��checkBoxGenerisiSifru�� � ToolTip$ Unesi šifru��$��Center=� d� 	IsChecked$ FalseY��� $��0��$��2��$��	10,3,10,3q�$��Center=�Naziv:�� $��0��$��3��$��	10,3,10,3q�$��Center=�	Vrednost:�� -   $��	textBoxID��$��1��$��0��$��3,3,3,3q�{�.  $	��150������  �� Path$ BodID�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 Explicit=��� -   $��textBoxSifra��$��1��$��1��$��3,3,3,3q� �� 	MaxLength$ 50��$	��150��{���  �� ElementName$ checkBoxGenerisiSifru��$ 	IsChecked�.������ $ Sifra�$
 Explicit=�$	 TwoWay=�.���� -   $��textBoxNaziv��$��1��$��2��$��3,3,3,3q�$ 50��.������ $ Naziv�$
 Explicit=�$	 TwoWay=�.���� -   $��textBoxVrednost��$��1��$��3��$��3,3,3,3q�$ 50��.������ $ Vrednost�$
 Explicit=�$	 TwoWay=� �� 	Converter %   Servis.DecimalToStringConverter  .���� -   $��buttonSacuvajINovi��$��2��$��0��$��4q�$��60��.��Sačuvaj & Novi�� -   $��buttonSacuvaj��$��2��$��1��$��4q�$��60��.��
Sačuvaj�� -	   $��buttonSacuvajIZatvori��$��2��$��2��$��4q�$��60��.��Sačuvaj & Zatvori