   M S B A M L   `   `   `  ���� B  >Servis, Version=1.0.0.32, Culture=neutral, PublicKeyToken=nullclr-namespace:ServisServis      Servis.ExportRoban   W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35^ ZWindowsFormsIntegration, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     &localclr-namespace:Servis     -     N� Title$  ExportRoban�� N� 	KeepAlive . .���  � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ���  � ColumnDefinitions �� $
��Auto ��� $	��300���� $
��Auto �.+��� $��0��$��0��$��
10,10,5,10q�$��Center=�Odaberi fajl�� -   $��textBoxFajl��$��1��$��0��$��	5,10,5,10q� �� 
IsReadOnly . �� -   $��buttonOdaberiFajl��$��2��$��0��$��5,10,100,10q�$��28��$��28��.���� $��Images/Open.ico�� � ToolTip$ Odaberi fajl���� -   $��comboBoxZaImport��$��1��$��1��$��3,3,10,3q�.@���  �� 
IsSelected . .��Usluga�� -   $��checkBoxSamoMarkirane��$��1��$��2��$��3,3,10,3q� d� 	IsChecked$
 TrueY�.��Exportuje samo markirane�� -   $��checkBoxExportujZaglavlje��$��1��$��3��$��3,3,10,3q�$ FalseY�.��Exportuje zaglavlje�� $��0��$��4��$��
10,10,5,10q�$	��Top=�Exportuj fajl�� -   $��listBoxRezultat��$��1��$��4��$��	5,10,5,10q� �� SelectionMode$ Single=��� -   $��buttonExportujFajl��$��2��$��4��$��5,10,100,10q�$��28��$��28��$	��Top=�.���� $��Images/Upload.ico��$ Export��