   M S B A M L   `   `   `  ����  AllTech.FacturationModuleA    ;AllTech.FacturationModule.Views.Modal.ClientModalParametres   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   | xInfragisticsWPF4.Controls.Menus.XamMenu.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbw sInfragisticsWPF4.DataVisualization.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb� �InfragisticsWPF4.Controls.Editors.XamComboEditor.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbe	 aInfragisticsWPF4.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cbq
 mInfragisticsWPF4.DataManager.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb| xInfragisticsWPF4.Controls.Grids.XamGrid.v17.2, Version=17.2.20172.1000, Culture=neutral, PublicKeyToken=7dd5c3163f2cd0cb7ig$http://schemas.infragistics.com/xaml    	 
  5        � Title$  ClientModalParametres��5   	   $
��auto��6'   $
��auto��65    � WindowStartupLocation$ CenterOwner=�6B   .��5      � .+�5   
   �� $��Suivi Produit du client��6   $��10,0,0,0q�63   $
��Auto��6E   $
��Bold"�6R   ���	#FF373737$��24(�5   (   $��Corbel)�66   ]� 5	   
   .+�5
      `� $	��Top=�6   $
��0,25q�62   .�5      c� -   5         c�Name$ toolBarMain��5      $
��auto��6/   .@�5      �� 
������  � ToolTip$ Sauvegarder ��68   $��8��6O   $��30��6�   $��30��6�   ��6\   �� *UpdateSuiviProduitCommand+��5      �� 5       �� 
TargetType" ��6$   .�5   "   ��  �� Property �@�Cursor �� Value$ Arrow��6;   5      .��5      ��    save#��[  6    ��5      �� 5   "   " ��6(    �� Triggers 5   &   P� 5   *    P� Property � {�	 P� Value	 .  . �5   .   ��  �@J�Opacity$	 0.3Q�6H   5   +   5   #   5      5      �� 5       �� 5!      
������ $��30��69   $��30��6D   $��10��6P   $ Suppression ��6^   ��6u   �� *CancelSuiviProduitCommand+��5"      �� 5#      " ��6$   .�5$   "   ��  �@�Cursor$ Arrow��6;   5&      .��5(      ��  
 delete#��[ 6    ��5)      �� 5*   "   " ��6(    5+   &   P� 5,   *    � {�	 .  . �5-   .   ��  �@J�Opacity$	 0.3Q�6H   5.   +   50   #   52      53      �� 54      �� 55      -   5        
������ $ Close ��55   9   $	��Top=�56      $��30��65   $��30��6@   $��Right=�6L   ��58      �� 59      " ��6$   .�5:   "   ��  �@�Cursor$ Arrow��6;   5<      .��5>      ��  
 cancel#��[ 6    $
��Fill=�6B   5?      5@      5A      �� 5B      $"��Produits associés au client��6   
����  �$
��Bold"�6N   .��5C      � 
 � RowDefinitions
 5D      �� 5E      $
��auto �6(   �� 5F      $
��auto �6(   .+�5H      ��  �� Orientation$ 
Horizontal=�6!   $��0��6:   .+�5I      �� $��Client :��6    
���   �$	��0,0q�6F   �� 5J      ��6$   �� *ClientActif.NomClient+5L      )  #Infragistics.Controls.Grids.XamGrid  5N      -   5          Name$ gridproduits��5N   /   $��1��6"      AutoGenerateColumns .     IsAlternateRowsEnabled . $	��100��5O   X   $	��200��6h      ColumnWidthE  ?Infragistics.Controls.Grids.Primitives.ColumnWidthTypeConverter$ * 6w      	RowHeightC  =Infragistics.Controls.Grids.Primitives.RowHeightTypeConverter$ 25 6�      RowHover$	 Row=�6�      ItemsSource 5N   E   �� *ListeProduitsuivis+   
ActiveItem 5P   <   �� *ProduiSuiviSelect+ �� Mode$ TwoWay=�   ColumnResizingSettings 5Q      8  2Infragistics.Controls.Grids.ColumnResizingSettings  5R         AllowColumnResizing$ 	Immediate=�68   5T         RowSelectorSettings 5W      5  /Infragistics.Controls.Grids.RowSelectorSettings  5X      -   5        rowSelectorSettings��4  .Infragistics.Controls.Grids.VisualSettingsBase   
Visibility$ Visible=�5X   R      PagerSettings 5Z      /  )Infragistics.Controls.Grids.PagerSettings  5[         AllowPaging$ Bottom=�6/      PageSize$ 20��6D      Columns 5]      ,  &Infragistics.Controls.Grids.TextColumn  5`      ,	  &Infragistics.Controls.Grids.ColumnBase
 	  Key$ Libelle��6,   (
  "Infragistics.Controls.Grids.Column 
  WidthM  GInfragistics.Controls.Grids.Primitives.NullableColumnWidthTypeConverter$
 auto 6<    	  HeaderTemplate 5a   "   �� 5b   &   .R�5c   *   �� $��Produit ��64   5d   '   5f        5h      $ CompteOhada��6,   $
 auto 6>    5i   "   �� 5j   &   .R�5k   *   �� $��Compte Soumis��64   5l   '   5n        5q      $ CompteExonere��6,   $
 auto 6@    5r   "   �� 5s   &   .R�5t   *   �� $��Compte Non Soumis��64   5u   '   5w      0  *Infragistics.Controls.Grids.TemplateColumn  5�      $
 auto 60   $ IsParameter��6>     
  IsFilterable  .   5�   "   �� 5�   &   .R�5�   *   �� $��Produit est Suivi��64   5�   '   !   ItemTemplate! 5�   "   �� 5�   &   .R�5�   *   �� " ��Name$" chkitems��6p   -   5        # d� 	IsChecked# 5�   �   �� *IsParameter+$ �� UpdateSourceTrigger$$ PropertyChanged=�$ TwoWay=�5�   '   5�      5�      5�      5�      5�      5�      5�      