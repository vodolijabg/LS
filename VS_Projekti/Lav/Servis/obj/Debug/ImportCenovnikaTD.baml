   M S B A M L   `   `   `  ����B  >Servis, Version=1.0.0.34, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis  5          Servis.ImportCenovnikaTD   5      W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      &localclr-namespace:Servis     5      -   5          N� Title$  ImportCenovnikaTD��5       N� 	KeepAlive . .��5      �  � RowDefinitions 5   
   �� 5      $
��Auto �6   6,   �� 5	      $
��Auto �6   6,   �� 5
      $
��Auto �6   6,   �� 5      6     � ColumnDefinitions 5   
   �� 5      $
��Auto �6   6.   �� 5      $	��300��6   60   �� 5      $
��Auto �6   6.   .+�5   
   �� $��0��6   $��0��6$   $��
10,10,5,10q�61   $��Center=�6E   Odaberi fajl6o   �� 5   
   -   5        $��textBoxFajl��5   B   $��1��6   $��0��6"   $��	5,10,5,10q�6/    �� 
IsReadOnly . 6i   �� 5   
   -   5        $��buttonOdaberiFajl��5   C   $��2��6   $��0��6!   $��5,10,100,10q�6.   $��28��6|   $��28��6�   .��5      �� $��Images/Open.ico��6    � ToolTip$ Odaberi fajl��6-   6F   5      �� 5   
   $��1��6   $��1��6%   $��5q�62    �� Orientation$ 
Horizontal=�6=   .+�5      	� -   5        $ ��radioButtonPoslovniPartner��5   6   $��3q�6    d� 	IsChecked$
 TrueY�6%   .��6X   Poslovni partner6j   	� 5      -   5        $!��radioButtonKorisnikPrograma��5   %   $��3q�6   .��6H   Korisnik programa6[   5      �� 5   
   -   5        $��textBlockStatus��5   1   $��0��6   $��2��6$   $��3q�6H   $��Right=�6S   $��Center=�6o   $y�	Collapsed=�6�   6�   �� 5   
   -   5        $!��textBlockStatusUkupnoRedova��5   1   $��2��6   $��2��6$   $��3q�6T   $
��Left=�6_   $��Center=�6z   $y�	Collapsed=�6�   6�   � 5!   
   -   5        $��progressBarStatus��5!   ?   $��1��6   $��2��6&   $��5q�64   $��25��6X   $y�	Collapsed=�6d   �� 5#   
   $��0��6   $��3��6$   $��
10,10,5,10q�61   $	��Top=�6E   Rezultat importa6p   �� 5$   
   -	   5        $��listBoxRezultat��5$   B   $��1��6   $��3��6"   $��	5,10,5,10q�6/    �� SelectionMode$ Single=�6Z   6s   �� 5%   
   -
   5        $��buttonImportujFajl��5%   C   $��2��6   $��3��6!   $��5,10,100,10q�6.   $��28��6   $��28��6�   $	��Top=�6�   .��5&      �� $��Images/Upload.ico��6   $ Uvezi cenovnik��6/   6J   5'      5)      5*      