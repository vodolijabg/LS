   M S B A M L   `   `   `  ����[  WSystem.Windows.Forms, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089Y@clr-namespace:System.Windows.Forms;assembly=System.Windows.FormsSystem.Windows.Forms  5      B >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis 5      P LMaskedTextBox.Control, Version=1.0.0.0, Culture=neutral, PublicKeyToken=null\Bclr-namespace:MaskedTextBox.Control;assembly=MaskedTextBox.ControlMaskedTextBox.Control 5          Servis.FizickoLiceDetaljno   5   	   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Owf@clr-namespace:System.Windows.Forms;assembly=System.Windows.Forms     5      &localclr-namespace:Servis     5      SctrlBclr-namespace:MaskedTextBox.Control;assembly=MaskedTextBox.Control     5        � Title$  FizickoLiceDetaljno��5       � SizeToContent$ WidthAndHeight=�6!    � 
ResizeMode$ NoResize=�6A   .��5      � -   5        $��gridFizickoLice��5       � RowDefinitions 5
   
   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,   �� 5      $
��Auto �6   6,    � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $	��200��6   60   �� 5      $
��Auto �6   6.   .+�5   
   �� $��0��6   $��0��6$   $��	10,3,10,3q�61   $��Center=�6D   ID:6d   �� 5   
   $��0��6   $��1��6%    �� Orientation$ 
Horizontal=�62   .+�5      �� $��	10,3,10,3q�6   $��Center=�6,   	Šifra:6P   �� 5       -   5        $��checkBoxGenerisiSifru��5        � ToolTip$ Unesi šifru��64   $��Center=�6J    d� 	IsChecked$ FalseY�6e   6z   5!      �� 5"   
   $��0��6   $��2��6$   $��	10,3,10,3q�61   $��Center=�6D   Ime:6e   �� 5#   
   $��0��6   $��3��6$   $��	10,3,10,3q�61   $��Center=�6D   
Prezime:6i   �� 5$   
   $��0��6   $��4��6$   $��	10,3,10,3q�61   $��Center=�6D   Kupac:6g   �� 5%   
   $��0��6   $��5��6$   $��	10,3,10,3q�61   $��Center=�6D   Mesto:6g   �� 5&   
   $��0��6   $��6��6$   $��	10,3,10,3q�61   $��Center=�6D   	Adresa:6h   �� 5'   
   $��0��6   $��7��6$   $��	10,3,10,3q�61   $��Center=�6D   
Telefon 1:6k   �� 5(   
   $��0��6   $��8��6$   $��	10,3,10,3q�61   $��Center=�6D   
Telefon 2:6k   �� 5*   
   $��0��6   $��9��6$   $��	10,3,10,3q�61   $��Center=�6D   	E-mail:6h   �� 5,   
   -   5        $��	textBoxID��5,   @   $��1��6   $��0��6"   $��3,3,3,3q�6/   {�.  $	��150��6�   ��6Q   ��  �� Path$ FizickoLiceID�	 �� Mode$	 TwoWay=�
 �� UpdateSourceTrigger$
 Explicit=�6�   �� 5-   
   -   5        $��textBoxSifra��5-   A   $��1��6   $��1��6#   $��3,3,3,3q�60    �� 	MaxLength$ 50��6V   $	��150��6�   {�6e   ��  �� ElementName$ checkBoxGenerisiSifru��$ 	IsChecked�.��6�   ��5.      �� 5/      $ Sifra�6   $
 Explicit=�6'   $	 TwoWay=�6F   50      .��51      52      �� 53   
   -   5        $��
textBoxIme��53   @   $��1��6   $��2��6"   $��3,3,3,3q�6/   $ 50��6R   .��6a   ��54      �� 55      $	 Ime�6   $
 Explicit=�6%   $	 TwoWay=�6D   56      .��57      58      �� 59   
   -   5        $��textBoxPrezime��59   @   $��1��6   $��3��6"   $��3,3,3,3q�6/   $ 50��6V   .��6f   ��5:      �� 5;      $ Prezime�6   $
 Explicit=�6)   $	 TwoWay=�6H    �� 	Converter 5<      (  "Servis.PrazanStringToNullConverter  5=      6>   5?      .��5@      5A      �� 5B   
   -   5        $��checkBoxRegistrovanKupac��5B   A   $��1��6   $��4��6#   $��3,3,3,3q�60    5C      �� 5D      $ RegistrovanKupac�6   $
 Explicit=�62   $	 TwoWay=�6Q   5E      5G      �� 5H   
   -   5        $��comboBoxMesto��5H   A   $��1��6   $��5��6#   $��3,3,3,3q�60    w� TextPath$ Naziv��6V   ��5I      �� 5J      .R�5K      �� ��5L      �� 5M      $ Naziv�6&   $	 OneWay=�63   6D   5O      5P       �� SelectedItem 5W      �� 5X      $ gridFizickoLice��6   $ DataContext.Mesto�68   $	 TwoWay=�6Q   $
 Explicit=�6_    5Y      "  Servis.MestoToMestoConverter  5Z      68   5\      5_      �� 5`   
   -	   5        $��textBoxAdresa��5`   @   $��1��6   $��6��6"   $��3,3,3,3q�6/   $	 100��6U   .��6e   ��5a      �� 5b      $ Adresa�6   $	 TwoWay=�6(   $
 Explicit=�66   5c      .��5d      5e      )  #MaskedTextBox.Control.MaskedTextBox  5r   
   -
   5        $��textBoxTelefon1��5r   K   $��1��6   $��7��6-   $��3,0,3,0q�6:      	InputMask$ iii/ii-ii-iiiiiiii��6b   .��6�   ��5s      �� 5t      $ Telefon1�6   $	 TwoWay=�6*   $
 Explicit=�68    �� ConverterParameter$ ___/__-__-________��5u   `    5v      !  Servis.TelefonMaskKonverter  5w      67   5y      .��5z   '   5{        5�   
   -   5        $��textBoxTelefon2��5�   K   $��1��6   $��8��6-   $��3,0,3,0q�6:   $ iii/ii-ii-iiiiiiii��6b   .��6�   ��5�      �� 5�      $ Telefon2�6   $	 TwoWay=�6*   $
 Explicit=�68   $ ___/__-__-________��5�   `    5�        5�      67   5�      .��5�   '   5�      �� 5�   
   -   5        $��textBoxEMail��5�   @   $��1��6   $��9��6"   $��3,3,3,3q�6/   $ 50��6T   .��6c   ��5�      �� 5�      $ EMail�6   $	 TwoWay=�6'   $
 Explicit=�65    5�        5�      6>   5�      .��5�      5�      �� 5�   
   -   5        $��buttonSacuvajINovi��5�   G   $��2��6   $��0��6!   $��4q�6.   $��60��69   .��6�   Sačuvaj & Novi6�   �� 5�   
   -   5        $��buttonSacuvaj��5�   G   $��2��6   $��1��6!   $��4q�6.   $��60��69   .��6x   
Sačuvaj6�   �� 5�   
   -   5        $��buttonSacuvajIZatvori��5�   G   $��2��6   $��2��6!   $��4q�6.   $��60��69   .��6�   Sačuvaj & Zatvori6�   �� 5�   
   -   5        $��buttonServisnaKnjizica��5�   G   $��2��6   $��3��6!   $��4q�6.   $��60��69   .��6�   ->Servisna knjizica5�      �� 5�   
   -   5        $��buttonPonuda��5�   G   $��2��6   $��4��6!   $��4q�6.   $��60��69   .��6v   
->Ponuda5�      �� 5�   
   -   5        $��buttonRadniNalog��5�   G   $��2��6   $��5��6!   $��4q�6.   $��60��69   .��6~   ->Radni nalog5�      �� 5�   
   -   5        $��
buttonTest��5�   G   $��2��6   $��6��6!   $��4q�6.   $��60��69   $y�	Collapsed=�6r   .��6�   Test5�      5�      5�      