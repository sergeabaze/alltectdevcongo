   M S B A M L   `   `   `  ����L  HAllTech.FrameWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter  5      \@clr-namespace:AllTech.FrameWork.Utils;assembly=AllTech.FrameWorkAllTech.FrameWork.Utils  5       AllTech.FacturationModule9   3AllTech.FacturationModule.Views.UCFacture.Uc_langue   5      \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5      R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5      Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006     5      @d2http://schemas.microsoft.com/expression/blend/2008     5      ]localVisibleDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork     5      Ucontrols@clr-namespace:AllTech.FrameWork.Utils;assembly=AllTech.FrameWork     5      $
��auto��5	         � 	Resources  5
      9   3AllTech.FrameWork.Converter.BoolVisibilityConverter   visibled5   /AllTech.FrameWork.Converter.BackgroundConverter  convertBack  myItemStyle �� 
TargetType �� Property �� Value �� 	Converter �� RelativeSource��%�   &	        &	      &	 &     7    5   
   6/     5   
   6,   �� 5   
   " ��6$   .�5      ��  � �� 5      �� 5      8    6   �� *Self+5      5      5      5      .��5      � $
��auto��6   $
��auto��6   .+�5   
   ]� ���	#FFE2E2E2.+�5      `� $	��Top=�6   $��0,0,0,0q�6/   $
��Auto��6@   .�5      c� $	��700��6   .@�6&   .@�5      ² �� 
������ $��30��66   $��30��6A   ��6M   �� *NewLCommand+.��5      ��   new#��[ 6    5      �� 5      
������  � ToolTip$ Sauvegarder��66   $��30��6m   $��30��6x   ��6L   �� *SaveLCommand+.��5      ��   save#��[ 6    5      �� 5       
������ $ 	Supprimer��66   $��30��6J   $��30��6U   ��6a   �� *DeleteLCommand+.��5!      ��  
 delete#��[ 6    5"      �� 5#      �� 5$      $��Liste langues��6    $
��Bold"�65   
�������$��13(�6Z   $
��50,0q�6h   5%      5&      �� 5'       �� Orientation$ 
Horizontal=�6   $	��Top=�62   
�������$��5q�6Z   .+�5(      �� 
��� � �$��5,0,0,0q�6+   $��25��6<   $
��Bold"�6H   $��Libellé langue :��6[   $	��100��6v   �� 5)      $	��150��5*   *   $��3q�66   
�������$��1q�6Y   ��5)      ��  �� Path$ LanguageSelected.Libelle�	 �� UpdateSourceTrigger$	 PropertyChanged=�5*   Y   �� 5+      
��� � �$��5,0,0,0q�6+   $��25��6<   $
��Bold"�6H   $��Court Libellé :��6[   $	��100��6u   �� 5,      $	��150��6   $��25��6&   
�������$��1q�6J   ��5-   *   �� $  LanguageSelected.Shortname�$	 PropertyChanged=�5.      .   (AllTech.FrameWork.Utils.SortableListView  5/      -   5        
  Name$
 LanguageView��5/   @   $��Bottom=�6(   $	��Top=�6V   $	��410��6p   $
��Left=�50   '   $	��220��6C   $ �� IsSynchronizedWithCurrentItem$
 TrueY�51   '   $
��10,0q�6L   #��[ 52   '    �� AlternationCount$ 2��54   L   ��6'   �� *LanguageList+ �� View 56      �� 57      P�58      �� 59      $��Libellé��6[    �� Width$	 200��6l    �� DisplayMemberBinding 6-   �� $ Libelle�6l   �� 5:      $��Court libéllé��6]   $	 200��6t    6-   �� $ 	Shortname�6t   5<      5>      5?      5@      5A      