   M S B A M L   `   `   `  ���� 
  Servis    Servis.Pocetna   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     -   $��windowPocetna��  � Title$	  Lav��$	��700��$
��1000�� � WindowStartupLocation$ CenterScreen=� � 	Resources    
OutlookTab �� 
TargetType � 	Resources  Key �� Color �� 
StartPoint �� EndPoint � Color	 � Offset
 �� 
TargetType �� Property �� Value �� Triggers ]� 
Conditions �� Property �� Value  ButtonNormalBrush  ButtonHoverBrush P� Property P� Value  ButtonSelectedBrush  CaptionBrush  ButtonPressedBrush J� SnapsToDevicePixels  BorderBrush �� Stretch	 �� X2 K� IsItemsHost �� SelectedContent��%,  &	        7  7  7  7  7  7  7  7  7  7  7  �� " �� ���� LabelHighlightBrush $ #FFFFFF���� CaptionBrush $ #15428B���� BorderBrush $ #6593CF���� 
LabelBrush $
 0, 0&�$	 0,1&�.��� $ #E3EFFF��$	 0Q�� $ #AFD2FF��$	 1Q��� ButtonNormalBrush $
 0, 0&�$	 0,1&�.��� $ #E3EFFF��$	 0Q�� $ #C4DDFF��$	 0.40625Q�� $ #ADD1FF��$	 0.40625Q�� $ #C0DBFF��$	 1Q��� ButtonSelectedBrush $
 0, 0&�$	 0,1&�.��� $ #FFD9AA��$	 0Q�� $ #FFBB6E��$	 0.40625Q�� $ #FFAB3F��$	 0.40625Q�� $ #FEE17A��$	 1Q��� ButtonPressedBrush $
 0, 0&�$	 0,1&�.��� $ #FFBD69��$	 0Q�� $ #FFAC42��$	 0.40625Q�� $ #FB8C3C��$	 0.40625Q�� $ #FED364��$	 1Q��� ButtonHoverBrush $
 0, 0&�$	 0,1&�.��� $ #FFFEE4��$	 0Q�� $ #FFE8A7��$	 0.40625Q�� $ #FFD767��$	 0.40625Q�� $ #FFE69E��$	 1Q��� '��       "
 ��.���  � �� �� " �� ]�  ��  �@��
IsSelected .  ��  �@J�IsMouseOver .  /��� 8    � ��]�  ��  �@��
IsSelected .  ��  �@J�IsMouseOver . /��� 8   � ��P�  �@��
IsSelected . . ���  � ��
 �   ��� 8   � ��P�  �@��
IsSelected .  . ��� 8   � ��]�  ��  �@��
IsSelected . ��  �@J�IsMouseOver . /��� 8   � ��.R�� #��z��$��32�� . .+��� 8�� $	��Top=�$
 Fill=�$ 1�� . �� $��5,0,5,0q�$��Tahoma)�$	��8pt(�$
��Bold"�8�� #��z��$��Center=�.R��� 8�� $��1q� . ._�]� .+��� $��Bottom=� . � $	��Top=�$��28��8��  . .+��� $��Tahoma)�8��	 $��Center=�$	��5,0q�$��18(�$
��Bold"�Servis automobila�� 8��
 $��Bottom=�$ 1��$
 Fill=��� #��z .��� ���	#FFBCCFE0 � RowDefinitions �� $��25 ���  � ColumnDefinitions �� $	��180 ��� $
��Auto �$��3���� .+��� $��0��$��0��$��3�� �� Orientation$ 
Horizontal=�$��Stretch=�$��0q�$��25������ $
 0, 0&�$	 0,1&�.��� $ #FFBD69��$	 0Q�� $ #FFAC42��$	 0.40625Q�� $ #FB8C3C��$	 0.40625Q�� $ #FED364��$	 1Q�.+��� -   $��textBlockRadnik��$��Center=�$��10,0,0,0q��� -   $��tabControlMeni��$��0��$��1��#��[  .@��� $��	Dokumenti�� �� 
IsSelected . .���� $��0q�.@��� -   $��listBoxItemPonuda��$��3q�.��Ponuda�� -   $��listBoxItemRadniNalog��$��3q�.��Radni nalog�� $��	Šifarnik�� . .���� $��0q�.@��� -   $��listBoxItemVozilo��$��3q�.��Vozila�� -   $��listBoxItemArtikal��$��3q�.��	Artikal�� -   $ ��listBoxItemPoslovniPartner��$��3q�.��Poslovni partner�� -	   $��listBoxItemFizickoLice��$��3q�.��Fizičko lice�� -
   $!��listBoxItemServisnaKnjizica��$��3q�.��Servisna knjižica�� $��
Izveštaji�� . .���� $��0q�.@��� -   $"��listBoxItemBrojIzdatihPonuda��$��3q�.��Broj izdatih ponuda�� -   $��listBoxItemPorudzbenica��$��3q�.��Porudžbenica�� $��Sistemski katalog��.���� $��0q�.@��� -   $��listBoxItemMesto��$��3q�.��Mesto�� -   $'��!listBoxItemNacinOrganizacijeFirme��$��3q�.��Način organizacije firme�� -   $��listBoxItemRadnik��$��3q�.��Radnik�� -   $��listBoxItemRadnoMesto��$��3q�.��Radno mesto�� -   $%��listBoxItemNacinZahtevaZaPonudu��$��3q�.��Način zahteva za ponudu�� -   $!��listBoxItemRadniNalogStatus��$��3q�.��Radni nalog status�� -   $��listBoxItemNosilacGrupe��$��3q�.��Nosilac grupe�� -   $��listBoxItemBod��$��3q�.��Bod�� -   $��listBoxItemNivo��$��3q�.��Nivo�� -   $��listBoxItemPozicija��$��3q�.��
Pozicija�� -   $��listBoxItemVrstaUsluge��$��3q�.��Vrsta usluge�� -   $��listBoxItemUsluga��$��3q�.��Usluga�� $��Sistem��.���� $��0q�.@��� -   $��listBoxItemPodesavanja��$��3q�.��Podešavanja�� -   $��listBoxItemImportRoban��$��3q�.��Import Roban�� -   $��listBoxItemExportRoban��$��3q�.��Export Roban�� -   $"��listBoxItemImportCenovnikaTD��$��3q�.��Import cenovnika TD�� -   $!��listBoxItemKorisnikPrograma��$��3q�.��Korisnik programa�� $��1��$��1��$��3��$��Stretch=�$��Center=� � -   $��
frameForme��$��2��$��1��
�������  � NavigationUIVisibility$ Visible=�