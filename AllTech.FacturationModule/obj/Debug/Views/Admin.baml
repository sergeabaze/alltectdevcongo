   M S B A M L   `   `   `  ����]  YInfragistics.Samples.Shared, Version=12.1.1000.1010, Culture=neutral, PublicKeyToken=null�Wclr-namespace:Infragistics.Samples.Shared.Controls;assembly=Infragistics.Samples.Shared$Infragistics.Samples.Shared.Controls  5      L HAllTech.FrameWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter 5      ` \Infragistics.Samples.Framework, Version=12.1.1000.1010, Culture=neutral, PublicKeyToken=nullwTclr-namespace:Infragistics.Samples.Framework;assembly=Infragistics.Samples.FrameworkInfragistics.Samples.Framework 5       AllTech.FacturationModule+   %AllTech.FacturationModule.Views.Admin   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      lcontrolsWclr-namespace:Infragistics.Samples.Shared.Controls;assembly=Infragistics.Samples.Shared     5      ]localVisibleDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork     5      |	 xInfragisticsWPF4.Controls.Menus.XamMenu.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbw
 sInfragisticsWPF4.DataVisualization.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb� �InfragisticsWPF4.Controls.Editors.XamComboEditor.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbe aInfragisticsWPF4.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbq mInfragisticsWPF4.DataManager.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb| xInfragisticsWPF4.Controls.Grids.XamGrid.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb7ig$http://schemas.infragistics.com/xaml 	 
     5	      BigPrim/http://schemas.infragistics.com/xaml/primitives 	    5
      ligFrameworkTclr-namespace:Infragistics.Samples.Framework;assembly=Infragistics.Samples.Framework 	    5      $	��300��5      $	��300��6   $
��auto��6.   $
��auto��6;     � 	Resources  5      9  3AllTech.FrameWork.Converter.BoolVisibilityConverter   visibled/  )AllTech.FrameWork.Converter.DateConverter  convertDate��%:   &	        &	        5   
   6/     5   
   6%   5      .��5      � .+�5   
   �� $��Maintenance��6   $��12(�6)   $��5,5,13,5q�69     GroupBoxStyle1#���  6K   .��5      ]� .+�5      �� $	��Top=�6   ._�5      `� .�5      c� -   5         c�Name$ 
toolbAdmin��5   "   $
��auto��66   .@�5      ��  � ToolTip$ fermer Formulaire��6%   $��40��6B   $��30��6M   ���	#FF966E6E��6Y   �� *CloseCommand+��5   "   �� 5   &    
 cancel �� ImageSource# [ 61   5      �� 5      �� 5       
������ $! management base de données��6�   $��40��6�   $��30��6�   ���	#FF966E6Ey�6@   �� *Isvisiblebtndatabase+ �� 	Converter# [  ��6�   �� *AdminDatabaseCommand+��5!   "   �� 5"   &    �� 
TargetType" ��6,   .�5#   *   ��  �� Property �@�Cursor �� Value$ Arrow��6C   5%   '   .��5'   "   ��   	imagetool#��[ 6(   ��5(   &   �� 5)   *   " ��60    �� Triggers 5*   .   P� 5+   2   	 P� Property	 � {�
 P� Value
 .  . �5,   6   ��  �@J�Opacity$	 0.3Q�6P   5-   3   5/   +   51   #   53      54      55      56      �� 58      $��Bottom=�6   
�������._�5:      ]� .+�5;   "   �� $	��Top=�6)   5=   #   �� 5>   "   $"��Archivage Données Courantes��6+   $	��Top=�6P   
����  �$
��Bold"�6w   .��5?   &   �  � RowDefinitions 5@   *   �� 5A   .   $
��auto �6<   �� 5B   .   $
��auto �6<   �� 5C   .   $
��auto �6<    � ColumnDefinitions 5E   *   �� 5F   .   $
��auto �6?   �� 5G   .   $
��auto �6?   �� 5H   .   $
��auto �6?   �� 5I   0   $
��auto �6A   .+�5K   *   �� $��Période début��60   
���   �$��1��6[   $��0��6h   $��5q�6x   (  "System.Windows.Controls.DatePicker  5L   *   -   5          Name$ dateFrom��5L   ]   $��1��65   $��5q�6B   $��1��6M      DisplayDateStart 6o   �� *	DateMin+   DisplayDateEnd 6�   �� *	DateMax+   SelectedDate 6�   �� *
Datefrom+�� 5M   *   $��Période fin��60   
���   �$��2��6Y   $��0��6f   $��5q�6v     5N   *   -   5        $ dateTo��5N   ]   $��2��65   $��1��6B   $��5q�6R    6m   �� *	DateMax+ 6�   �� *	DateMin+ 6�   �� *DateTo+�� 5O   *   $��2��61   
������ $ archive datas��6W   $��2��6p   $��40��6�   $��30��6�   ��6�   �� *SaveCommand+��5P   .   �� 5Q   2   " ��68   .�5R   6   ��  �@�Cursor$ Arrow��6O   5T   3   .��5V   .   ��   run#��[ 64   $
��Fill=�6R   ��5W   2   �� 5X   6   " ��6<    5Y   :   P� 5Z   >   	 � {�
 .  . �5[   B   ��  �@J�Opacity$	 0.3Q�6\   5\   ?   5^   7   5`   /   5a   +   5c      5d   #   �� 5f      $��Historiques��6#   $��Bottom=�68   $
��Bold"�6P   .��5g      �  5h   "   �� 5i   &   $
��auto �64   �� 5j   &   $
��auto �64   �� 5k   &   $
��auto �64   .+�5m   "   � $��0��6.   $��Right=�6;   $	��120��6W   $��20��6c   $��2q�6o    � IsIndeterminate 6z   �� *	Isloading+y�6�   �� *ProgressBarloadVisibility+# [  )  #Infragistics.Controls.Grids.XamGrid  5n   "   -   5          Name$ GridHistorique��5n   .      AutoGenerateColumns .     ColumnWidthE  ?Infragistics.Controls.Grids.Primitives.ColumnWidthTypeConverter$ * 6b   $��1��6r      IsAlternateRowsEnabled . $
��auto��5o   V   $	��600��6c   $	��300��6r   $
��auto��6�      RowHover$
 None=�5p   ^      ItemsSource 67   �� *ListeArchives+   Columns 5s   &   .  (Infragistics.Controls.Grids.ColumnLayout  5t   *   ,  &Infragistics.Controls.Grids.ColumnBase
   Key$ Details��6:      HeaderVisibility�  �Infragistics.NullableEnumTypeConverter`1[[System.Windows.Visibility, PresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35]]$
 true 6I      Columns 5u   .   ,	  &Infragistics.Controls.Grids.TextColumn	  5w   2   $ Libelle��6@   (
  "Infragistics.Controls.Grids.Column 
  WidthM  GInfragistics.Controls.Grids.Primitives.NullableColumnWidthTypeConverter$
 auto 6N      HeaderTemplate 5x   6   �� 5y   :   .R�5z   >   �� $��Statut libelle facture��6H   5{   ;   5}   3   	  5   2   $ Nombre��6@   $
 auto 6M    5�   6   �� 5�   :   .R�5�   >   �� $��Nombre factures��6H   5�   ;   5�   3   5�   +   	  5�   *   $ DateFrom��68    
  MinimumWidth$	 100Q�6G   $
 auto 6Z   
   Tag$ 1��6h     
  ValueConverter#  [ 6q   ! 
  ValueConverterParameter$! dd/MM/yyyy ��6�    5�   .   �� 5�   2   .R�5�   6   �� $��Période début��6@   5�   3   5�   +   	  5�   *   $ DateTo��68   $	 100Q�6E   $
 auto 6X   $ 1��6f   #  [ 6o   $! dd/MM/yyyy ��6�    5�   .   �� 5�   2   .R�5�   6   �� $��Période Fin��6@   5�   3   5�   +   	  5�   *   $ 
DateBackup��68   $	 100Q�6I   $
 auto 6\   $ 1��6j    5�   .   �� 5�   2   .R�5�   6   �� $��Date Archivage��6@   5�   3   5�   +   	  5�   *   $ Observation��68   $	 100Q�6J   $
 auto 6]   $ 1��6k    5�   .   �� 5�   2   .R�5�   6   �� $��Observations��6@   5�   3   5�   +   	  5�   *   $ BackUpBy��68   $	 100Q�6G   $
 auto 6Z   $ 1��6h    5�   .   �� 5�   2   .R�5�   6   �� $��Archivé Par��6@   5�   3   5�   +   5�   #   5�      5�      5�      5�      5�      5�      5�      5�      