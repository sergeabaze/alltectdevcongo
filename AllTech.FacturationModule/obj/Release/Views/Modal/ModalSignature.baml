   M S B A M L   `   `   `  ���� `  \Microsoft.Practices.Prism, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35zSclr-namespace:Microsoft.Practices.Prism.Commands;assembly=Microsoft.Practices.Prism"Microsoft.Practices.Prism.Commands  L HAllTech.FrameWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter dDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter  AllTech.FacturationModule:   4AllTech.FacturationModule.Views.Modal.ModalSignature   \ XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation     R NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml     hCommandsSclr-namespace:Microsoft.Practices.Prism.Commands;assembly=Microsoft.Practices.Prism     ]localVisibleDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork     ]imgConverterDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork       � Title$   ��$	��300��$	��361�� � WindowStartupLocation$ CenterOwner=� � WindowStyle$
 None=� � Icon$@ :/AllTech.FacturationModule;component/images/statistics.png������  �� EndPoint$ 0.5,1&� �� 
StartPoint$ 0.5,0&�.���  � Color$ 	#FFFFFFFF�� � Offset$ 0Q�� $ #FCFFF5��$ 0.992Q�� $ #3E606F��$ 0.185Q� � 	Resources 9  3AllTech.FrameWork.Converter.BoolVisibilityConverter   visibled6  0AllTech.FrameWork.Converter.BinaryImageConverter  
convertimg��%   &	        &	          .��� $	��293��.+��� $��	Signature��$��10,0,0,0q�$
��Auto��$
��Bold"����	#FF373737$��24(�$��Corbel)��� -   
������ 	 � ToolTip$	 Close ��$��0,2,10,0q�$	��Top=�$��30��$��25��$��Right=����� 
 �� 
TargetType"
 ��.���  �� Property �@�Cursor �� Value$ Arrow��.����  
 cancel#��[ $
��Fill=�$��30���� ._��  � RowDefinitions �� $
��Auto ��� $
��Auto �.+�`� $��0��$��0,30,0,0q�$
��Auto��.�c� $	��300��.@��� 
������ $	 Sauvegarde ��$��30��$��30������ *SaveCommand+.����   save#��[ ���� "
 �� �� Triggers P�  P� Property � {� P� Value .  . ���  �@J�Opacity$	 0.3Q��� �� -    ��Name$ 
btnDossier��
������ $��30��$��30��$	 Ouvrire dossier ��.����   openbook#��[ ���� "
 �� P�  � {� .  . ���  �@J�Opacity$	 0.3Q��� �� 
������ $��30��$��30��$	 Suppression ������ *DeleteCommand+.����  
 delete#��[ ���� "
 �� P�  � {� .  . ���  �@J�Opacity$	 0.3Q��� $��1��._��� -    ��Name$ imagesignature������ *	Signature+ �� 	Converter# [  �� UpdateSourceTrigger$ PropertyChanged=� �� Mode$ TwoWay=�