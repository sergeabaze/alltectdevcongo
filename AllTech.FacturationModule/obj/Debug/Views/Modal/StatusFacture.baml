   M S B A M L   `   `   `  ����L  HAllTech.FrameWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter  5      ` \Microsoft.Practices.Prism, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35zSclr-namespace:Microsoft.Practices.Prism.Commands;assembly=Microsoft.Practices.Prism"Microsoft.Practices.Prism.Commands 5   
   \@clr-namespace:AllTech.FrameWork.Utils;assembly=AllTech.FrameWorkAllTech.FrameWork.Utils  5   
    AllTech.FacturationModule9   3AllTech.FacturationModule.Views.Modal.StatusFacture   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ]localVisibleDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork     5      hCommandsSclr-namespace:Microsoft.Practices.Prism.Commands;assembly=Microsoft.Practices.Prism     5   
   Ucontrols@clr-namespace:AllTech.FrameWork.Utils;assembly=AllTech.FrameWork     5   
     � Title$   ��5   
   $
��Auto��6   $	��520��6!    � WindowStartupLocation$ CenterOwner=�6-    � WindowStyle$
 None=�6Q    � Icon$; 5/AllTech.FacturationModule;component/images/mydoc.ico��6d    � 	Resources 5	      9   3AllTech.FrameWork.Converter.BoolVisibilityConverter   visibled5   /AllTech.FrameWork.Converter.BackgroundConverter  convertBack  myItemStyle �� 
TargetType �� Property �� Value �� 	Converter	 �� RelativeSource��%�   &	        &	      &	 &     7    5
   
   6/     5   
   6,   �� 5   
   " ��6$   .�5      ��  � �� 5      �� 5      8   	 6   �� *Self+5      5      5      5      .��5      � .+�5   
   �� $��Statuts Facture��6   $��10,0,0,0q�6+   $
��Auto��6=   $
��Bold"�5      ���	#FF373737$��24(�6:   $��Corbel)�6H   �� 5   
   -   5        
������ 
 � ToolTip$
 Close ��5   -   $��
465,12,0,0q�6>   $	��Top=�5      $��30��6-   $��25��68   $
��Left=�6D   ��5      �� 5      " ��6   .�5      ��  �@�Cursor$ Arrow��6/   5      .��5!      ��  
 cancel#��[ 6   $
��Fill=�66   $��30��6E   5"      �� 5#   
   $��0,40,0,0q�6   ._�5$      � $	��222��6    � RowDefinitions 5%      �� 5&      $
��Auto �6$   �� 5'      $
��Auto �6$   �� 5(      $
��Auto �6$   .+�5,      `� $��0��6   $��0,0,0,0q�6,   $
��Auto��6=   .�5-      c� .@�5.      �� 
������ $��30��6:   $��30��6E   $

 New ��6Q   .  (Microsoft.Practices.Prism.Commands.Click   Command 6b   �� *
NewCommand+��5/      �� 50   "   " ��6(   .�51   &   ��  �@�Cursor$ Arrow��6?   53   #   .��55      ��   new#��[ 6$   56      �� 57      �� 58      
������ $
 Save ��6:   $��30��6y   $��30��6�    6J   �� *SaveCommand+��59      �� 5:   "   " ��6(   .�5;   &   ��  �@�Cursor$ Arrow��6?   5=   #   .��5?      ��   save#��[ 6$   5@      �� 5A      �� 5B      
������ $��30��6<   $��30��6G   $
 Delete ��6S    6e   �� *DeleteCommand+��5C      �� 5D   "   " ��6(   .�5E   &   ��  �@�Cursor$ Arrow��6?   5G   #   .��5I      ��  
 delete#��[ 6$   5J      5L      5M      �� 5N      $��1��6   
�������._�5O      �  5P      �� 5Q      $
��Auto �6,   �� 5R      $
��Auto �6,    � ColumnDefinitions 5T      �� 5U      $
��Auto �6/   �� 5V      $
��Auto �6/   �� 5W      $
��Auto �6/   �� 5X      $
��Auto �6/   �� 5Y      $
��Auto �6/   .+�5[      �� $��0��6!   $��5��6/   $��3q�6C   
����  �$��2q�6a   ._�5\      � $	��487��6#    5]   "   �� 5^   &   $
��Auto �64   �� 5_   &   $
��Auto �64    5a   "   �� 5b   &   $
��Auto �67   �� 5c   &   $
��Auto �67   �� 5d   &   $
��Auto �67   �� 5e   &   $
��Auto �67   �� 5f   &   $
��Auto �67   .+�5h   "   �� $��Designation��6,   $	��100��6@   $��25��6L   $��0��6X   $��0��6e   �� 5i   "   $��2,0,0,3q�6*   $��0��6d   
����  �$��25��6�   $��1��6�   $	��180��6�   ��6<   �� *StatutSelected.Libelle+6�   �� 5j   "   $��langue��6,   $��0��6:   $��3��6G   $��	20,0,10,3q�6W   �� 5k   "    �� 
IsEditable . 
����  �$	��120��6u   $��25��6�   $��0��6�   $��4��6�   $��0,0,2,3q�6�   ��6+   �� *LanguageList+ �� SelectedItem 5l   $   �� *Languageselected+��5m   &   �� 5n   *   .R�5o   .   �� ��68   �� *	Libelle+5p   +   5r   #   5s      5t      5u      5v      .   (AllTech.FrameWork.Utils.SortableListView  5x      -   5          Name$ 
DetailView��5x   :   $��2��6-   $	��147��6N   $��Stretch=�5y   0   $��Stretch=�6N   $ �� IsSynchronizedWithCurrentItem$
 TrueY�5z   0    �� AlternationCount$ 2��5{      #��[ 5}      $��0,0,0,53q�5~   S   ��60   �� *
StatutList+ �� 
GroupStyle 5�      (  "System.Windows.Controls.GroupStyle  5�         ContainerStyle 5�      �� 5�   "   " ��6(   .�5�   &   ��  � ��$ 0,0,0,5q�5�   1   5�   #   5�       �� View 5�      �� 5�      P�5�      �� 5�   "   $��Designation��5�   5    �� Width$	 200��6J    �� DisplayMemberBinding 5�   1   ��  �� Path$ Libelle�5�   J   �� 5�   "   $��langue��5�   5   $	 150��6E    5�   1   �� $ Langues .Libelle�5�   E   5�      5�      5�      5�      5�      5�      