   M S B A M L   `   `   `  ����
  Servis    Servis.NivoDetaljno   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5        � Title$  NivoDetaljno��5       � SizeToContent$ WidthAndHeight=�6    � 
ResizeMode$ NoResize=�6:   .��5      � -   5        $��gridNivo��5       � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5	      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      6"   �� 5      $
��Auto �6   6.   .+�5   
   �� $��0��6   $��0��6$   $��	10,3,10,3q�61   $��Center=�6D   ID:6d   �� 5   
   $��0��6   $��1��6%    �� Orientation$ 
Horizontal=�62   .+�5      �� $��	10,3,10,3q�6   $��Center=�6,   	Šifra:6P   �� 5      -   5        $��checkBoxGenerisiSifru��5       � ToolTip$ Unesi šifru��64   $��Center=�6J    d� 	IsChecked$ FalseY�6e   6z   5      �� 5   
   $��0��6   $��2��6$   $��	10,3,10,3q�61   $��Center=�6D   Naziv:6g   �� 5   
   -   5        $��	textBoxID��5   @   $��1��6   $��0��6"   $��3,3,3,3q�6/   {�.  $	��150��6�   ��6Q   ��  �� Path$ NivoID�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 Explicit=�6�   �� 5   
   -   5        $��textBoxSifra��5   A   $��1��6   $��1��6#   $��3,3,3,3q�60    �� 	MaxLength$ 50��6V   $	��150��6�   {�6e   ��  �� ElementName$ checkBoxGenerisiSifru��$ 	IsChecked�.��6�   ��5      �� 5      $ Sifra�6   $
 Explicit=�6'   $	 TwoWay=�6F   5       .��5!      5"      �� 5#   
   -   5        $��textBoxNaziv��5#   @   $��1��6   $��2��6"   $��3,3,3,3q�6/   $ 50��6T   .��6d   ��5$      �� 5%      $ Naziv�6   $
 Explicit=�6'   $	 TwoWay=�6F   5)      .��5*      5+      �� 5-   
   -   5        $��buttonSacuvajINovi��5-   G   $��2��6   $��0��6!   $��4q�6.   $��60��69   .��6�   Sačuvaj & Novi6�   �� 5.   
   -   5        $��buttonSacuvaj��5.   G   $��2��6   $��1��6!   $��4q�6.   $��60��69   .��6x   
Sačuvaj6�   �� 5/   
   -   5        $��buttonSacuvajIZatvori��5/   G   $��2��6   $��2��6!   $��4q�6.   $��60��69   .��6�   Sačuvaj & Zatvori6�   52      53      