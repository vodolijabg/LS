   M S B A M L   `   `   `  ����B  >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  5   	       Servis.EMail   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   &localclr-namespace:Servis     5   	   -   5          � Title$  EMail��5       � SizeToContent$ WidthAndHeight=�6    � 
ResizeMode$ NoResize=�63   .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5	      $
��Auto �6   6,   �� 5
      $
��Auto �6   6,   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $	��200��6   60   .+�5   
   �� $��0��6   $��0��6$   $��	10,3,10,3q�61   $��Center=�6D   E-mail adresa:6o   �� 5   
   $��0��6   $��1��6$   $��	10,3,10,3q�61   $��Center=�6D   Ime:6e   �� 5   
   $��0��6   $��2��6$   $��	10,3,10,3q�61   $��Center=�6D   
Lozinka:6i   �� 5   
   -   5        $��textBoxEmailAdresa��5   A   $��1��6   $��0��6"   $��3,3,10,3q�6/   6^   �� 5   
   -   5        $��
textBoxIme��5   A   $��1��6   $��1��6"   $��3,3,10,3q�6/   6V   G� 5   
   -   5        $��textBoxLozinka��5   F   $��1��6   $��2��6'   $��3,3,10,3q�64    G� PasswordChar$ *��6\   6o   �� 5   
   $��0��6   $��2��6%   $��3��69    �� Orientation$ 
Horizontal=�6F   $��
0,10,10,10q�6_   $��Right=�6s   .+�5      �� -   5        $��
buttonTest��5   /   $��4q�6   $��60��6!   .��6Z   Test6`   �� 5      -   5        $��buttonSacuvaj��5   /   $��4q�6   $��60��6!   .��6`   
Sačuvaj6i   5       5#      5$      