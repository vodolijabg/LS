   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis      Servis.DBKonekcija   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis     -     � Title$  DBKonekcija�� � SizeToContent$ WidthAndHeight=� � 
ResizeMode$ NoResize=�.���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $	��200��.+��� $��0��$��0��$��	10,3,10,3q�$��Center=�	Server:�� $��0��$��1��$��	10,3,10,3q�$��Center=�Baza:�� $��0��$��2��$��	10,3,10,3q�$��Center=�Autentifikacija:�� $��0��$��3��$��	20,3,10,3q�$��Center=�Korisničko ime:�� $��0��$��4��$��	20,3,10,3q�$��Center=�
Lozinka:�� -   $��textBoxServer��$��1��$��0��$��3,3,10,3q��� -   $��textBoxBaza��$��1��$��1��$��3,3,10,3q��� -   $��comboBoxAutentifikacija��$��1��$��2��$��3,3,10,3q�.@���  �� 
IsSelected . .��Windows autentifikacija�� .��SQL autentifikacija�� -   $��textBoxKorisnickoIme��$��1��$��3��$��	13,3,10,3q�{�.  G� -   $��textBoxLozinka��$��1��$��4��$��	13,3,10,3q�{�.   G� PasswordChar$ *���� $��0��$��2��$��5�� �� Orientation$ 
Horizontal=�$��
0,10,10,10q�$��Right=�.+��� -   $��buttonTestKonekcije��$��4q�$��60��.��Test konekcije�� -   $��buttonSacuvaj��$��4q�$��60��.��
Sačuvaj