   M S B A M L   `   `   `  ����L  HAllTech.FrameWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter  5   
    AllTech.FacturationModule=   7AllTech.FacturationModule.Views.DataReferencesViewModal   5   	   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     5   	   R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     5   	   ]localConvertDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork     5   
   $
��auto��5   	     � WindowStyle$  SingleBorderWindow=�6   $
��auto��67    � WindowStartupLocation$ CenterOwner=�6E    � 	Resources 5      9   3AllTech.FrameWork.Converter.BoolVisibilityConverter   visibled=   7AllTech.FrameWork.Converter.MenuBoolVisibilityConverter  menuvisibled��%:   &	        &	        5   
   6/     5   
   63   5	      .��5
      � ��5   
   �� 5       �� EndPoint$ 0.5,1&�6#    �� 
StartPoint$ 0.5,0&�64   .��5      �  � Color$ 	#FFFFFFFF��6    � Offset$ 0Q�61   � 5      $ #FCFFF5��6   $ 0.992Q�6/   � 5      $ #3E606F��6   $ 0.185Q�6/   5      .+�5   
   �� $��DONNEES DE REFERENCES��6   $��50,0,0,0q�60   $
��Auto��6B   $
��Bold"�6O   ���	#FF373737$��24(�6x   $��Corbel)�6�   ]� 5   
   $��0,0,0,0q�6   .+�5      �� $	��Top=�6   ._�5      �� -   5         ��Name$ btnClose��5       � ToolTip$ fermer Formulaire��6+   $��40��6H   $��30��6S   $��Right=�5      y�65   �� 	 �� Path$	 BtnCloseVisible�
 �� 	Converter#
 [  ��5      �� 5       �� 
TargetType" ��6    .�5      ��  �� Property �@�Cursor �� Value$ Arrow��67   5      ��5      �� 5       
 cancel �� ImageSource# [ 6%   5       5!      �� 5"      $	��Top=�6   $��3,10,0,0q�6*    �� CornerRadius$ 12,12,12,12��6>   ._�5$      �� $
��Auto��5%      $
��Auto��6+   $��Stretch=�69   ��5$      Z� .@�5'      �� $��	Société��6   y�63   �� $	 IsMenuCompanyVisible�#
 [ .��5(      �� -   5         ��Name$ 	vieCopany��5(   +   ��6>   �� *CompanyRegion+5)      �� 5*      $��Utilisateur��6   y�67   �� $	 IsMenuUsersVisible�#
 [ .��5+      �� -   5        $ viewUser��5+   +   ��6=   �� *
UserRegion+5,      �� 5-      $��Produits��6   y�63   �� $	 IsMenuproductVisible�#
 [ .��5.      �� -   5        $ viewProduct��5.   +   ��6@   �� *ProduitRegion+5/      �� 50      $��Clients��6   y�62   �� $	 IsMenuClientVisible�#
 [ .��51      �� -   5        $ 
viewClient��51   +   ��6?   �� *ClientRegion+52      �� 53      $��Données de factures��6   y�6>   �� $	 IsMenufacturesVisible�#
 [ .��54      �� -   5        $ viewDonnéesref��54   +   ��6C   �� *DonneesRegion+55      5D      5F      5G      5H      5I      