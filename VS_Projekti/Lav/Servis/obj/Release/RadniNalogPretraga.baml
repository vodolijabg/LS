   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis      Servis.RadniNalogPretraga   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis     -     � Title$  RadniNalogPretraga�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��150���� $
��Auto �.+��� $��0��$��0��$��	10,3,10,3q�$��Vrsta partnera:��$��Center=��� $��1��$��0��$��	10,3,10,3q�$	��ID:��$��Center=��� $��2��$��0��$��	10,3,10,3q�$��	Datum od:��$��Center=��� $��3��$��0��$��	10,3,10,3q�$��	Datum do:��$��Center=��� $��4��$��0��$��	10,3,10,3q�$��Partner:��$��Center=��� $��5��$��0��$��	10,3,10,3q�$��Ne završene:��$��Center=��� $��6��$��0��$��	10,3,10,3q�$��Radnik:��$��Center=��� -   $��comboBoxVrstaPartnera��$��0��$��1��$��3,3,3,3q�.@��� $�� �� �� 
IsSelected . �� .��Poslovni partner�� .��Fizičko lice�� -   $��	textBoxID��$��1��$��1��$��3,3,3,3q�(  "System.Windows.Controls.DatePicker  -   $��datePickerDatumOd��$��1��$��2��$��3,3,3,3q�  -   $��datePickerDatumDo��$��1��$��3��$��3,3,3,3q��� -   $��textBoxPartner��$��1��$��4��$��3,3,3,3q��� -   $��checkBoxOtvorene��$��1��$��5��$��3,3,3,3q��� -   $��comboBoxRadnik��$��1��$��6��$��3,3,3,3q� w� TextPath$ Nadimak������ .R��� ����  �� Path$ Nadimak� �� Mode$ OneWay=�	 �� SelectedItem	 �� 
 �� ElementName$
 
gridPonuda��$ DataContext.Radnik�$ TwoWay=� �� UpdateSourceTrigger$ Explicit=� �� 	Converter $   Servis.RadnikToRadnikConverter  �� -	   $��buttonNadji��$��2��$��0��$��4q�$��60��.��Nadji�� -
   $��buttonNadjiIZatvori��$��2��$��1��$��4q�$��60��.��Nadji & Zatvori