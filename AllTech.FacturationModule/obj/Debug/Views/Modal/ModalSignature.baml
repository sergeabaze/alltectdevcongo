   M S B A M L   `   `   `  ����`  \Microsoft.Practices.Prism, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35zSclr-namespace:Microsoft.Practices.Prism.Commands;assembly=Microsoft.Practices.Prism"Microsoft.Practices.Prism.Commands  5      L HAllTech.FrameWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter 5      dDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter 5       AllTech.FacturationModule:   4AllTech.FacturationModule.Views.Modal.ModalSignature   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   hCommandsSclr-namespace:Microsoft.Practices.Prism.Commands;assembly=Microsoft.Practices.Prism     5      ]localVisibleDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork     5      ]imgConverterDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork     5        � Title$   ��5   	   $	��300��6   $	��361��6    � WindowStartupLocation$ CenterOwner=�6+    � WindowStyle$
 None=�6O    � Icon$@ :/AllTech.FacturationModule;component/images/statistics.png��6b    � 	Resources 5	      9  3AllTech.FrameWork.Converter.BoolVisibilityConverter   visibled6  0AllTech.FrameWork.Converter.BinaryImageConverter  
convertimg��%:   &	        &	        5
   
   6/     5   
   6,   5      .��5      � $	��293��6   .+�5   
   �� $��	Signature��6   $��10,0,0,0q�6%   $
��Auto��67   $
��Bold"�6D   ���	#FF373737$��24(�6m   $��Corbel)�6{   �� 5   
   ._�5      �  � RowDefinitions 5      �� 5      $
��Auto �6$   �� 5      $
��Auto �6$   .+�5      `� $��0��6   $��0,30,0,0q�6,   $
��Auto��6>   .�5      c� .@�5      �� 
������  � ToolTip$ Sauvegarde ��6=   $��30��6T   $��30��6_   ��6k   �� *SaveCommand+��5      �� 5   "    �� 
TargetType" ��6(   .�5   &   ��  �� Property �@�Cursor	 �� Value$	 Arrow��6?   5   #   .��5      ��   save#��[ 6$   ��5    "   �� 5!   &   " ��6,   
 �� Triggers
 5"   *   P� 5#   .    P� Property � {� P� Value .  . �5$   2   ��  �@J�Opacity$		 0.3Q�6L   5%   /   5'   '   5)      5*      �� 5+      �� 5,      -   5         ��Name$ 
btnDossier��5,   !   
������ $��30��6P   $��30��6[   $ Ouvrire dossier ��6g   ��5-      �� 5.   "   " ��6(   .�5/   &   ��  �@�Cursor$	 Arrow��6?   51   #   .��53      ��   openbook#��[ 6$   ��54   "   �� 55   &   " ��6,   
 56   *   P� 57   .    � {� .  . �58   2   ��  �@J�Opacity$		 0.3Q�6L   59   /   5;   '   5=      5>      �� 5?      �� 5A      
������ $��30��6<   $��30��6G   $ Suppression ��6S   ��6j   �� *DeleteCommand+��5B      �� 5C   "   " ��6(   .�5D   &   ��  �@�Cursor$	 Arrow��6?   5F   #   .��5H      ��  
 delete#��[ 6$   ��5I   "   �� 5J   &   " ��6,   
 5K   *   P� 5L   .    � {� .  . �5M   2   ��  �@J�Opacity$		 0.3Q�6L   5N   /   5P   '   5R      5S      �� 5T      �� 5U      -   5        
������ $ Close ��5U   =   $��0,2,0,0q�6N   $	��Top=�5V      $��30��6-   $��30��68   $��Right=�6D   ��5X      �� 5Y   "   " ��6(   .�5Z   &   ��  �@�Cursor$	 Arrow��6?   5\   #   .��5^      ��  
 cancel#��[ 6$   $
��Fill=�6F   5_      5`      5a      �� 5b      $��1��6   ._�5c      �� -   5         ��Name$ imagesignature��5d      ��5c      �� *	Signature+ �� 	Converter# [  �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�5d      5f      5g      5i      5j      5k      