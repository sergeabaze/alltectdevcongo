   M S B A M L   `   `   `  ���� L  HAllTech.FrameWork, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nulldDclr-namespace:AllTech.FrameWork.UIControl;assembly=AllTech.FrameWorkAllTech.FrameWork.UIControl  ` \Microsoft.Practices.Prism, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35zSclr-namespace:Microsoft.Practices.Prism.Commands;assembly=Microsoft.Practices.Prism"Microsoft.Practices.Prism.Commands dDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWorkAllTech.FrameWork.Converter  M IWPFAutoCompleteBox, Version=1.0.0.2, Culture=neutral, PublicKeyToken=nullS<clr-namespace:WPFAutoCompleteBox;assembly=WPFAutoCompleteBoxWPFAutoCompleteBox pJclr-namespace:AllTech.FrameWork.ValidationRules;assembly=AllTech.FrameWork!AllTech.FrameWork.ValidationRules  O KNumericUpDownControl, Version=1.0.0.0, Culture=neutral, PublicKeyToken=nullY@clr-namespace:NumericUpDownControl;assembly=NumericUpDownControlNumericUpDownControl O Kmscorlib, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e0891&clr-namespace:System;assembly=mscorlibSystem \@clr-namespace:AllTech.FrameWork.Utils;assembly=AllTech.FrameWorkAllTech.FrameWork.Utils   AllTech.FacturationModule6   0AllTech.FacturationModule.Views.Facturation_view   n jSAPBusinessObjects.WPF.ViewerShared, Version=13.0.3500.0, Culture=neutral, PublicKeyToken=692fbea5521e1304W SPresentationCore, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35R NWindowsBase, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35\	 XPresentationFramework, Version=4.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35F 9http://schemas.microsoft.com/winfx/2006/xaml/presentation    	 R
 NSystem.Xaml, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b77a5c561934e089:x,http://schemas.microsoft.com/winfx/2006/xaml 
   	 Jmc;http://schemas.openxmlformats.org/markup-compatibility/2006 
   	 @d2http://schemas.microsoft.com/expression/blend/2008 
   	 YselectorDclr-namespace:AllTech.FrameWork.UIControl;assembly=AllTech.FrameWork 
   	 hCommandsSclr-namespace:Microsoft.Practices.Prism.Commands;assembly=Microsoft.Practices.Prism 
   	 ]localVisibleDclr-namespace:AllTech.FrameWork.Converter;assembly=AllTech.FrameWork 
   	 Qcomplete<clr-namespace:WPFAutoCompleteBox;assembly=WPFAutoCompleteBox 
   	 a
validationJclr-namespace:AllTech.FrameWork.ValidationRules;assembly=AllTech.FrameWork 
   	 Supdown@clr-namespace:NumericUpDownControl;assembly=NumericUpDownControl 
   	 6sys&clr-namespace:System;assembly=mscorlib 
   	 XtxBoxComple@clr-namespace:AllTech.FrameWork.Utils;assembly=AllTech.FrameWork 
   	 -   $
��Auto��$
��Auto��  � 	Resources  9   3AllTech.FrameWork.Converter.BoolVisibilityConverter   visibled/   )AllTech.FrameWork.Converter.DateConverter  dateConvert2   ,AllTech.FrameWork.Converter.DecimalConverter  decimalConvert��%-   &	        &	      &	 
           .��� -    �Name$ 
LayoutRoot��$	��702��$
��1052��.+��� $��Formulair Facture��$��12(�$��	19,7,0,12q�  GroupBoxStyle1#���  .��]� $
��Auto��$
��Auto��$��Center=�.+��� $	��Top=�._�`� .�c� .@��� -    ��Name$ cmdnewfacture��$��30��$��30�� � ToolTip$ Nouvelle Facture��.  (Microsoft.Practices.Prism.Commands.Click   Command �� *
NewCommand+.����   new#��[ �� $ Sauvegarder ��$��30��$��30�� �� *SaveCommand+����  �� 
TargetType" ��.���  �� Property �@�Cursor �� Value$ Arrow�� �� Triggers �� 	 �� Binding	 �� 
 �� Path$
 IsBusy� �� Value True.a���  �@�Cursor$
 Wait��.����   save#��[ ���� " �� P�  P� Property � {� P� Value .  . ���  �@J�Opacity$	 0.5Q��� $ Supprimer La facture��$��30��$��30�� �� *DeleteCommand+���� " ��.���  �@�Cursor$ Arrow�� �� 	 �� $
 IsBusy� True.a���  �@�Cursor$
 Wait��.����  
 delete#��[ ���� " �� P�  � {� .  . ���  �@J�Opacity$	 0.5Q��� �� $��30��$��30��$ Imprimer Facture�� �� *PrintCommand+���� " ��.���  �@�Cursor$ Arrow�� �� 	 �� $
 IsBusy� True.a���  �@�Cursor$
 Wait��.����  	 print#��[ ���� " �� P�  � {� .  . ���  �@J�Opacity$	 0.5Q��� $ fermer Formulaire��$��40��$��30�� �� *CloseCommand+y��� $
 BtnCloseVisible� �� 	Converter# [  ����  
 cancel �� ImageSource# [ �� $��Bottom=�._�� $
��Auto�� � RowDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $��0���� $��1��._��  �� $
��Auto ��� $
��Auto ��� $
��Auto �.+��� $$��Information Entête de facture��$��0��$��0,0,0,5q�
�������.���  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto � � ColumnDefinitions �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��205* �.+��� $��Client :��$��13(�
��� � �$
��Bold"�$��5,7,0,1q�$��57��$��28��$
��Left=��� -    ��Name$ 	cmbClient�� �� 
IsEditable . 
����  � �� DisplayMemberPath$ 	NomClient��$
��Left=�$��1��$	��220��$	��120��$��25��$��2��$��1,11,6,0q�{��� *IsEnabledClient+���� $
 
ClientList� �� SelectedItem �� *ClientSelected+�� $��Objet : ��$��1��$��13(�$��2,5,54,0q�
��� � �$
��Bold"�$��60���� -   $ cmbObjet�� .  �� IsTextSearchEnabled . 
����  �$��2��$ Libelle��$	��250��$��1��$��1��$
��Auto��$	��120��$��0,5,6,0q�$��25��{��� *
CmbEnabled+���� $
 	ObjetList� �� SelectedValue �� *ObjetSelected+�� $��Exploitation :��$��2��$��13(�$��4,5,22,0q�
��� � �$
��Bold"�$��90���� -   $ 
cmbexploit�� . $��2��$��1��$
��Auto��$	��250��
����  �$ Libelle��$��0,5,6,0q�$��25��$��2������ *ExploitationList+ �� *ExploitationSelected+�� $��Mois Prestation :��$��3��$��0��$��13(�$��0,5,0,0q�
��� � �$
��Bold"�( 	 "System.Windows.Controls.DatePicker  -     Name$ datePrestation��   SelectedDateFormat$ Short=�$��3��$��1��$��2��$	��221��$	��250��
����  �$��0,11,0,0q�   SelectedDate ��  �� Mode$ TwoWay=�$"
 FatureCurrent.MoisPrestation� �� UpdateSourceTrigger$ PropertyChanged=��� $��Statut Courant :��$��2��$��3��
��� � �$
��Bold"��� $��13(� �� TextAlignment$ Center=�$��Times New Roman)�
��� � �$��3��$��3��$��2��
�������$	��0,2q����� *%#FatureCurrent.CurrentStatut.Libelle+�� $��Jour Fin Prestation��$��4��$��0��
��� � �$
��Bold"���  �� 
IsReadOnly . $��Center=�$��4��$��1��$��2��$
��Auto��$	��250��
����  �$	��0,3q����� *FatureCurrent.JourFinEcheance+�� $��Date Dépot :��$��5��
��� � �$
��Bold"�$��25��$��	5,10,21,4q�$��90��  $��5��$��1��$��2��
����  �$	��225��$	��250��$	��Top=�$ Short=�$��2,11,0,0q� �� $ TwoWay=�$
 FatureCurrent.DateDepot�$ PropertyChanged=��� $��0��$��6��$��6��$��3��._��  �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $	��83* � �� $
��Auto ��� $
��Auto ��� $
��Auto ��� $
��Auto ��� $	��76* �.+��� $��Facture # :��$��0��$��0��
��� � �$
��Bold"�$��13(�$��0,0,0,3q�$��80���� $	��181��$	��150��$	��200��
����  �$��1��$��2,0,0,3q�
��� � �$
��Bold"�$��15(�$��2������ *FatureCurrent.NumeroFacture+�� $�� Validation :��$��1��$��0��$��13(�$��3,1,0,30q�$��2��$��90��$��90��
��� � �$
��Bold"�$
��Left=��� -    ��Name$ chkValidateDate��$��1��$��1��$��0,7,0,27q�$
��Left=�$��32��$��25��$��2��{��� *IsEnabledchkDateValidate+  d� 	IsChecked  �� *IsValidateDate+  -	   $ datevalidate��
����  �$	��Top=�$ Short=�$	��129��$	��250��$��1��$��2��$��25��$��22,0,0,0q�$
��Left=�{��� *ChkDateValidate+ �� $
 FatureCurrent.DateCloture�$ TwoWay=�$ PropertyChanged=�! �� ValidatesOnDataErrors! . �� $��Sortie :��$��2��$��0��
��� � �$
��Bold"�$��13(�$��0,0,0,0q�$��59��$��67���� $��2��$��1��$��0,7,0,8q�$��18��$��27��$
��Left=�  �� *ChkDateOutValidate+{��� *IsEnableOutValidation+�� $	��129��$��2��$��2��$	��180��
����  �$��25��$��22,0,0,3q����� *FatureCurrent.DateSortie+# [ " �� ConverterParameter$" 
dd/MM/yyyy��{��� *IsEnableOutValidation+�� $��Suspension :��$��3��$��0��
��� � �$
��Bold"�$��13(�$��0,0,0,0q�$
��Auto��$��90���� $��3��$��1��$��0,6,0,78q�$��27��$
��Left=�$��3��  �� *ChkDateSuspended+{��� *IsEnabledSuspendedValidation+�� 
����  �$	��129��$	��100��$	��180��$��3��$��2��$��25��$��22,1,0,1q����� *FatureCurrent.DateSuspension+# [ $" 
dd/MM/yyyy��{��� *IsEnabledSuspendedValidation+�� $��4��$��4��._�]� .+��� $��Total Facture��$	��Top=�
��� � �$
��Bold"�$��12(�$��Italic$��� $��Bottom=�._��  �� $
��auto ��� $
��auto ��� $
��auto � �� $
��auto ��� $
��auto ��� $
��auto ��� $
��auto ��� $��6* �.+��� 
����  �$
��Bold"�$��80��$��
Total HT :��$��0,6,0,0q�$��0��$��0���� $��2��$	��121��$��0,0,0,0q�$��Right=����� *
FTotalHt+�� 
����  �$
��Bold"�$��80��$��Tva :��$��1��$��0���� $��70��$��1��$��1������ *
Montanttva+�� $��0,0,2,0q�$	��119��$��1��$��2��$��Right=����� *	Ftotaltva+�� 
����  �$
��Bold"�$��
Total TT :��$��2��$��0���� $	��121��$��0,0,0,0q�$��2��$��2��$��Right=����� *	FtotalTTC+�� $��3��$��3��$��3��$��0��$��	20,0,0,0 q�.+��� # �� Orientation$# 
Horizontal=�.+��� $��Devise facture��
��� � �$
��Bold"��� $��30�� . ���� *DeviseSelected.Libelle+�� $��Devise Convertion��
��� � �$
��Bold"��� $��50�� . ���� *DeviseConvert.Libelle+�� $# 
Horizontal=�.+��� $��Prorata��$��12(�
��� � �$
��Bold"��� -
   $��
chkPoratas��$�� ��$��16��$��2q�  �� *
IsPorata+{��� *IsProrataEnabled+�� $	��Tva��$��12(�
��� � �$
��Bold"��� $��50�� . ���� *TaxeSelected.Taux+�� $#��Information Ligne de commande��$��1��$��0,10,0,0q�{��� *
CmbEnabled+.���  �� $
��Auto ��� $
��Auto �.+��� $��Produit ��$��0��$
��20,0q�
��� � �$
��Bold"��� $��Autre Description ��$��260,0,574,0q�
��� � �$
��Bold"�$	��165���� $��
Quantité ��$��500,0,403,0q�
��� � �$
��Bold"�$��96���� $��Prix Unitaire ��$��540,0,259,0q�
��� � �$
��Bold"�$��90���� -   $��	cmbChoice��$	��190��$��5,0,5,0q�
����  �$��1��$ Libelle�� . $��25��$��0��$
��Left=����� *
ProduiList+ �� *ProduitSelected+1   +AllTech.FrameWork.Utils.AutoCompleteTextBox  -   $��txtOther��$	��253��$	��300��$��1��$
��Left=�$   	DelayTime$	$ 500��%   	Threshold$% 2��$��25��$��	200,6,0,2q�%  NumericUpDownControl.NumericBox  -   &  Name$& txtQty��$��1��$��	0,5,400,3q�$��3��'   Minimum$' 0��(   Maximum$
( 1000��$��Right=�)   Value) �� *LigneFacture.Quantite+$ PropertyChanged=�$ TwoWay=��� -   $��txtUnitPrice��$��80��$��1��$��Right=�
����  �$��25��$��	0,6,310,2q�$��4�� �� *IsPrixunitReadOnly+���� *PrixUnitaireselected+$ PropertyChanged=�$ TwoWay=��� $��Exonere��$��1��$��
480,10,0,0q�$��70��  �� *IsItemsExonere+{��� *ExonereEnable+�� -   $��
btnAddgrid��$��30��$��30��$��	580,0,0,0q�$ Add Line Items��$��1��$��6��{��� *BtnAddDetailEnable+ �� *NewlineCommand+.����  	 new_add#��[	 ���� " �� P�  � {� .  . ���  �@J�Opacity$	 0.5Q��� $��Ligne de Commandes��
��� � �$��2��$��	0,5,0,-81q�
�������$
��Auto��.���� $	��228��._��  �� $
��Auto ��� $
��Auto ��� $
��245* � �� $
��auto ��� $
��auto �.+�& 	  System.Windows.Controls.DataGrid  -   *  Name$* Items��$��2��+   AutoGenerateColumns+ .  ,   CanUserAddRows, . -   CanUserDeleteRows- . ".   VerticalScrollBarVisibility$
. Auto=�/   
IsReadOnly/ . 0   SelectionMode$0 Single=�$��Stretch=�$��Center=�$	��186��$��0,9,0,30q�$
��Auto������ *LigneCommandList+1   Columns1 0	 	 *System.Windows.Controls.DataGridTextColumn	  ,
 	 &System.Windows.Controls.DataGridColumn2 
  Header$2 Designation Produit��3 
  Width5 	 /System.Windows.Controls.DataGridLengthConverter$	3 202 4 	  FontSize$4 14Q�1 	 +System.Windows.Controls.DataGridBoundColumn5   Binding5 �� *	Produit+6   ElementStyle6 �� " ��.���  � ��$
 Wrap=�7 
  HeaderStyle7 �� = 	 7System.Windows.Controls.Primitives.DataGridColumnHeader"  .���  � ��$ 13(���  � ��$
 Bold"���  � ��
 � � ���  � ��$ Center=�	  $2 Description��$	3 200 $4 13Q�5 �� *Description+6 �� " ��.���  � ��$
 Wrap=�7 �� "  .���  � ��$ 13(���  � ��$
 Bold"���  � ��
 � � ���  � ��$ Center=�	  $2 	Quantité��$	3 150 5 �� *
quantite+6 �� " ��.���  � ��$
 Wrap=�7 �� "  .���  � ��$ 13(���  � ��$
 Bold"���  � ��
 � � ���  � ��$ Center=�	  $2 Prix unitaire��$	3 100 5 �� *
PrixUnit+6 �� " ��.���  � ��$
 Wrap=�7 �� "  .���  � ��$ 13(���  � ��$
 Bold"���  � ��
 � � ���  � ��$ Center=�	  $2 
Montant HT��$	3 180 5 �� *	montantHt+6 �� " ��.���  � ��$
 Wrap=�7 �� "  .���  � ��$ 13(���  � ��$
 Bold"���  � ��
 � � ���  � ��$ Center=�4 	 .System.Windows.Controls.DataGridTemplateColumn  $2  ��8   CellTemplate8 �� .R��� -   $  Supprimer ligne de facture��$��15��{��� *IsdeletedEnabled+ �� *DeletelineCommand+9 �� RelativeSource9 �� : �� AncestorType":  ���� .����  
 
new_delete#��[
 �� $��2��$��
0,203,0,13q�._��� -   ; ��Name$; txtbloclCumul��$��100,0q�$��total��$��Right=�� $��60.061��$��248,58.633,0,0q�$	��Top=�$y�	Collapsed=�.+�F� '����0�     ѭ�� �� �%1�.�K��� 	$
��16.1��$��0,0,8.067,0q�< J� RenderTransformOrigin$)< #0.499999978361064,0.499999995889058&�= �� Stretch$
= Fill=�
���   �$��0��$	��Top=�$��Right=�$��12.29������ > �� EndPoint$> 0.466,2.201&�? �� 
StartPoint$? 0.5,0&�.��� @ � Color$@ 	#FF5F6061��A � Offset$A 1Q�� $@ 	#FF757576��$A 0.855Q�� $@ 	#FFD1D1D1��$
A 0.11Q�z�V� .~��� �� �� B �� Angle$B 90.087Q�T� C T� Y$C -6.04399277075815Q�D T� X$D 6.0531771644038841Q�