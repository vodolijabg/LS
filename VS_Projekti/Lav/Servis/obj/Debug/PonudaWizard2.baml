   M S B A M L   `   `   `  ����O  Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem  5      B >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis 5
         Servis.PonudaWizard2   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      6sys&clr-namespace:System;assembly=mscorlib     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      &localclr-namespace:Servis     5
      -   5          N� Title$  PonudaWizard2��5       N� 	KeepAlive . .��5      � -   5        $��gridRadniNalog��5      ���	#FF9CAAC1 � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $��* �6   6)    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $��* �6   6,   .+�5   
   �� $��0��6   $��0��6%   $��
10,30,10,3q�62   $��Center=�6F   Tip:6h   �� 5   
   $��0��6   $��1��6%   $��	10,3,10,3q�62   $��Center=�6E   	Napomena:6l   � 5   
   -   5        $��gridTip��5      $��1��6   $��0��6.   $��
10,30,10,3q�6;    5      �� 5      6&   �� 5      $
��Auto �6#   62   .+�5"      �� -   5        $��
textBoxTip��5"   D   $��0,0,3,0q�6   $��0��6'   $��0��67    �� 
IsReadOnly . .��6i   .��5%   "   .��5/   #   50      �� 52      -   5        $��	buttonTip��52   N   $��1��6   $��0��6%   $��0,0,3,0q�62   $��23��6C   .��6w   ...6|   54      �� 5?   
   -   5        $��textBoxNapomena��5?   B   $��1��6   $��1��6"   $��	10,3,10,3q�6/    �� MaxLines$ 2��6Y    �� 	MaxLength$	 500��6f    �� AcceptsReturn . " �� VerticalScrollBarVisibility$ Visible=�6�   $	��300��6�   .��6�   .��5@      �� 	 �� Path$	 Napomena�6   
 �� Mode$
 TwoWay=�6&    �� UpdateSourceTrigger$ Explicit=�64    �� 	Converter 5A      (  "Servis.PrazanStringToNullConverter  5B      6:   5D      5E      �� 5F   
   $��Right=�6   $��1��61   $��2��6A    �� Orientation$ 
Horizontal=�6N   $��0,30,0,5q�6g   $��Bottom=�6y   .+�5G      �� -   5        $��buttonNazad��5G   /   $��4q�6   $��60��6!   .��6\   	<<Nazad6k   �� 5H      -   5        $��buttonZavrsi��5H   /   $��4q�6   $��60��6!    �� 	IsDefault . .��6p   	Završi6x   �� 5I      -	   5        $��buttonOtkazi��5I   /   $��4q�6   $��60��6!   .��6^   	Otkaži6f   5J      5M      5N      