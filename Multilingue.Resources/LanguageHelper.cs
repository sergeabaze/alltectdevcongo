using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace Multilingue.Resources
{
   static public class LanguageHelper
    {

       static LanguageHelper()
       {
           //CultureManager.UICulture = Thread.CurrentThread.CurrentCulture;
           //CultureManager.UICulture = new CultureInfo(GlobalDatas.defaultLanguage);
           // Thread.CurrentThread.CurrentCulture = new CultureInfo(GlobalDatas.defaultLanguage); ;
           // Thread.CurrentThread.CurrentUICulture = new CultureInfo(GlobalDatas.defaultLanguage);
           // FrameworkElement.LanguageProperty.OverrideMetadata(
           //typeof(FrameworkElement),
           //new FrameworkPropertyMetadata(
           //    XmlLanguage.GetLanguage(CultureInfo.CurrentCulture.IetfLanguageTag)));

           //MessageBox.Show(GlobalDatas.defaultLanguage+"  "+ Thread.CurrentThread.CurrentCulture.Name );
           //if (string .IsNullOrEmpty ( GlobalDatas.cultureCode))
           //    Language.Culture = System.Globalization.CultureInfo.CurrentCulture;
           //else 
           //Language.Culture = System.Globalization.CultureInfo.CreateSpecificCulture(GlobalDatas.cultureCode);
           Langues.Culture = Thread.CurrentThread.CurrentUICulture;
           // MessageBox.Show("final culture" + Language.Culture.Name + " menu fichier :" + MnFichier);
           //Language.Culture = System.Globalization.CultureInfo.CreateSpecificCulture("fr-FR");
           //System.Globalization.CultureManager
       }
       public static void inittread()
       {
           Langues.Culture = Thread.CurrentThread.CurrentUICulture;
       }

       static public string LblLogin
       {
           get { return Langues.lblLogin; }
       }

       static public string LblNewPassword
       {
           get { return Langues.lblNewPassword; }
       }

       static public string LblPassword
       {
           get { return Langues.lblPassword; }
       }

       static public string Lblselecttextlogin
       {
           get { return Langues.lblselecttextlogin; }
       }

       static public string LblselecttextPassword
       {
           get { return Langues.lblselecttextPassword; }
       }

       static public string TooltipbtnAnnul
       {
           get { return Langues.tooltipbtnAnnul; }
       }

       static public string TooltipDeconecter
       {
           get { return Langues.tooltipDeconecter; }
       }

       static public string TooltipHst
       {
           get { return Langues.tooltipHst; }
       }

       static public string MNuAdmin
       {
           get { return Langues.mNuAdmin ; }
       }
       static public string MNuAdmin_Admin
       {
           get { return Langues.mNuAdmin_Admin; }
       }
       static public string MNuAdmin_Datarefs
       {
           get { return Langues.mNuAdmin_Datarefs; }
       }
       static public string MNuAdmin_Parametres
       {
           get { return Langues.mNuAdmin_Parametres; }
       }
       static public string MNuAide
       {
           get { return Langues.mNuAide; }
       }

       static public string MNuApropos
       {
           get { return Langues.mNuApropos; }
       }

       static public string MNuFile
       {
           get { return Langues.mNuFile; }
       }
       static public string MNuFile_fermer
       {
           get { return Langues.mNuFile_fermer; }
       }
       static public string MNuFile_quitter
       {
           get { return Langues.mNuFile_quitter; }
       }
       static public string MNuOperation
       {
           get { return Langues.mNuOperation; }
       }
       static public string MNuOperation_Extract
       {
           get { return Langues.mNuOperation_Extract; }
       }
       static public string MNuOperation_Hst
       {
           get { return Langues.mNuOperation_Hst; }
       }
       static public string MNuOperation_Sorties
       {
           get { return Langues.mNuOperation_Sorties; }
       }

       //
       static public string MNuOperation_Jventes
       {
           get { return Langues.MNuOperation_Jv; }
       }

       static public string MNuAdminAdmin_Datarefs
       {
           get { return Langues.mNuAdminAdmin_Datarefs; }
       }

       static public string ToolbarLangueEn
       {
           get { return Langues.toolbarLangueEn; }
       }

       static public string ToolbarLangueEs
       {
           get { return Langues.toolbarLangueEs; }
       }
       static public string ToolbarLangueFr
       {
           get { return Langues.toolbarLangueFr; }
       }
       static public string ToolbarLanguePt
       {
           get { return Langues.toolbarLanguePt; }
       }
       static public string ToolbarPopUpCpmtTemp
       {
           get { return Langues.toolbarPopUpCpmtTemp; }
       }

       static public string ToolbarPopUpLicense_tooltip
       {
           get { return Langues.toolbarPopUpLicense_tooltip; }
       }

       static public string ToolbarPopUpMsInstal
       {
           get { return Langues.toolbarPopUpMsInstal; }
       }

       static public string ToolbarProfile
       {
           get { return Langues.toolbarProfile; }
       }

       static public string ToolbarUserConnecter
       {
           get { return Langues.toolbarUserConnecter; }
       }

       #region User affiche region
		 
	
       static public string UserAfficheTitre
       {
           get { return Langues.userAfficheTitre; }
       }

       static public string LblUserAfficheCours
       {
           get { return Langues.lblUserAfficheCours; }
       }

       static public string LblUserAfficheCreated
       {
           get { return Langues.lblUserAfficheCreated; }
       }

       static public string LblUserAfficheError
       {
           get { return Langues.lblUserAfficheError; }
       }

       static public string LblUserAfficheErrorTitre
       {
           get { return Langues.lblUserAfficheErrorTitre; }
       }

       static public string LblUserAfficheSorties
       {
           get { return Langues.lblUserAfficheSorties; }
       }

       static public string LblUserAfficheSuspendu
       {
           get { return Langues.lblUserAfficheSuspendu; }
       }

       static public string LblUserAfficheValidee
       {
           get { return Langues.lblUserAfficheValidee; }
       }

       #endregion

       #region Facture Region
       
    

       static public string factureTabItemAudit
       {
           get { return Langues.factureTabItemAudit; }
       }

       static public string factureTabItemTitre
       {
           get { return Langues.factureTabItemTitre; }
       }

       static public string factureTopbarTooltipCancel
       {
           get { return Langues.factureTopbarTooltipCancel; }
       }

       static public string factureTopbarTooltipClose
       {
           get { return Langues.factureTopbarTooltipClose; }
       }

       static public string factureTopbarTooltipNew
       {
           get { return Langues.factureTopbarTooltipNew; }
       }

       static public string factureTopbarTooltipPrint
       {
           get { return Langues.factureTopbarTooltipPrint; }
       }

       static public string factureTopbarTooltipSave
       {
           get { return Langues.factureTopbarTooltipSave; }
       }

       static public string factureTopbarTooltipSupprime
       {
           get { return Langues.factureTopbarTooltipSupprime; }
       }

       static public string factureInfoEnteteTitre
       {
           get { return Langues.factureInfoEnteteTitre; }
       }

       static public string factureEnteteClient
       {
           get { return Langues.factureEnteteClient; }
       }

       static public string factureEnteteConverse
       {
           get { return Langues.factureEnteteConverse; }
       }
       static public string factureEnteteDateCreate
       {
           get { return Langues.factureEnteteDateCreate; }
       }
       static public string factureEnteteDateDepot
       {
           get { return Langues.factureEnteteDateDepot; }
       }
       static public string factureEnteteDept
       {
           get { return Langues.factureEnteteDept; }
       }


       static public string factureEnteteDeviseFact
       {
           get { return Langues.factureEnteteDeviseFact; }
       }
       static public string factureEnteteexploit
       {
           get { return Langues.factureEnteteexploit; }
       }


       static public string factureEnteteFactyreNum
       {
           get { return Langues.factureEnteteFactyreNum; }
       }

       static public string factureEnteteJourFin
       {
           get { return Langues.factureEnteteJourFin; }
       }

       static public string factureEnteteMoisP
       {
           get { return Langues.factureEnteteMoisP; }
       }
       static public string factureEnteteNonValide
       {
           get { return Langues.factureEnteteNonValide; }
       }

       static public string factureEnteteObjet
       {
           get { return Langues.factureEnteteObjet; }
       }

       static public string factureEnteteObjetNew
       {
           get { return Langues.factureEnteteObjetNew; }
       }

       static public string factureEnteteSortie
       {
           get { return Langues.factureEnteteSortie; }
       }

       static public string factureEnteteStatutClient
       {
           get { return Langues.factureEnteteStatutClient; }
       }
       static public string factureEnteteStatutFacture
       {
           get { return Langues.factureEnteteStatutFacture; }
       }
       static public string factureEnteteSuspension
       {
           get { return Langues.factureEnteteSuspension; }
       }
       static public string factureEnteteTotalFacture
       {
           get { return Langues.factureEnteteTotalFacture; }
       }
       static public string factureGridDesc
       {
           get { return Langues.factureGridDesc; }
       }
       static public string factureGridMontantht
       {
           get { return Langues.factureGridMontantht; }
       }
      

        static public string factureGridPrixunit
       {
           get { return Langues.factureGridPrixunit; }
       }

       static public string factureGridProd
       {
           get { return Langues.factureGridProd; }
       }

       static public string factureGridQuantite
       {
           get { return Langues.factureGridQuantite; }
       }

       static public string factureGridtaxe
       {
           get { return Langues.factureGridtaxe; }
       }

       static public string factureLigneDelete
       {
           get { return Langues.factureLigneDelete; }

       }

       static public string factureLigneqte
       {
           get { return Langues.factureLigneqte; }

       }

       static public string factureLigneDesc
       {
           get { return Langues.factureLigneDesc; }
       }

       static public string factureLigneLignes
       {
           get { return Langues.factureLigneLignes; }
       }

       static public string factureLignePrixUnit
       {
           get { return Langues.factureLignePrixUnit; }
       }


       static public string factureLigneProduit
       {
           get { return Langues.factureLigneProduit; }
       }

       static public string factureLigneTitre
       {
           get { return Langues.factureLigneTitre; }
       }

       static public string factureLigneTooltibtnAdd
       {
           get { return Langues.factureLigneTooltibtnAdd; }
       }


       static public string factureAuditCrePar
       {
           get { return Langues.factureAuditCrePar; }
       }

       static public string factureAuditDatecreation
       {
           get { return Langues.factureAuditDatecreation; }
       }

       static public string factureAuditDateModif
       {
           get { return Langues.factureAuditDateModif; }
       }

       static public string factureAuditDateValide
       {
           get { return Langues.factureAuditDateValide; }
       }
       static public string factureAuditModifpar
       {
           get { return Langues.factureAuditModifpar; }
       }

       static public string factureAuditSortie
       {
           get { return Langues.factureAuditSortie; }
       }

      
     


       #endregion

       #region Historic Region
       static public string titreFormulaire
       {
           get { return Langues.titreFormulaire ; }
       }

       static public string toolbaToolTip_fermer
       {
           get { return Langues.toolbaToolTip_fermer; }
       }

       static public string toolbaToolTip_refresh
       {
           get { return Langues.toolbaToolTip_refresh; }
       }

       static public string RecherchebuttonToolTip
       {
           get { return Langues.RecherchebuttonToolTip; }
       }

       static public string toolbaTitreListe
       {
           get { return Langues.toolbaTitreListe; }
       }

       static public string RechercheCmbClient
       {
           get { return Langues.RechercheCmbClient; }
       }

       static public string splitBoutonFiltre
       {
           get { return Langues.splitBoutonFiltre; }
       }
       static public string splitBoutonFiltreByClient
       {
           get { return Langues.splitBoutonFiltreByClient; }
       }
       static public string splitBoutonFiltreByClient_tooltip
       {
           get { return Langues.splitBoutonFiltreByClient_tooltip; }
       }
       static public string splitBoutonFiltreByDate
       {
           get { return Langues.splitBoutonFiltreByDate; }
       }
       static public string splitBoutonFiltreByDate_tooltip
       {
           get { return Langues.splitBoutonFiltreByDate_tooltip; }
       }

       static public string splitBoutonFiltreByFacture
       {
           get { return Langues.splitBoutonFiltreByFacture; }
       }

       static public string splitBoutonFiltreByFacture_tooltip
       {
           get { return Langues.splitBoutonFiltreByFacture_tooltip; }
       }
       static public string splitBoutonImpressions
       {
           get { return Langues.splitBoutonImpressions; }
       }

       static public string splitBoutonImpressionsExport
       {
           get { return Langues.splitBoutonImpressionsExport; }
       }
       static public string splitBoutonImpressionsExport_List
       {
           get { return Langues.splitBoutonImpressionsExport_List; }
       }
       static public string splitBoutonImpressionsExport_select
       {
           get { return Langues.splitBoutonImpressionsExport_select; }
       }
       static public string splitBoutonImpressionsExportTooltip
       {
           get { return Langues.splitBoutonImpressionsExportTooltip; }
       }
       static public string splitBoutonImpressionsprint
       {
           get { return Langues.splitBoutonImpressionsprint; }
       }
       static public string splitBoutonImpressionsprintList
       {
           get { return Langues.splitBoutonImpressionsprintList; }
       }
       static public string splitBoutonOperation
       {
           get { return Langues.splitBoutonOperation; }
       }
       static public string splitBoutonOperationTooltipNonValide
       {
           get { return Langues.splitBoutonOperationTooltipNonValide; }
       }
       static public string splitBoutonOperationTooltipSortie
       {
           get { return Langues.splitBoutonOperationTooltipSortie; }
       }
       static public string splitBoutonOperationTooltipSuspension
       {
           get { return Langues.splitBoutonOperationTooltipSuspension; }
       }

       static public string splitBoutonOperationTooltipValidation
       {
           get { return Langues.splitBoutonOperationTooltipValidation; }
       }

       static public string splitBoutonRechercheDateDebut
       {
           get { return Langues.splitBoutonRechercheDateDebut; }
       }

       static public string splitBoutonRechercheDateFin
       {
           get { return Langues.splitBoutonRechercheDateFin; }
       }

       static public string splitBoutonRecherches
       {
           get { return Langues.splitBoutonRecherches; }
       }

       static public string gridbtnDeselect
       {
           get { return Langues.gridbtnDeselect; }
       }

       static public string gridbtnselect
       {
           get { return Langues.gridbtnselect; }
       }

       static public string gridClient
       {
           get { return Langues.gridClient; }
       }

       static public string gridDatecreation
       {
           get { return Langues.gridDatecreation; }
       }

       static public string gridDatevalidation
       {
           get { return Langues.gridDatevalidation; }
       }

       static public string gridMois
       {
           get { return Langues.gridMois; }
       }

       static public string gridNumfacture
       {
           get { return Langues.gridNumfacture; }
       }

       static public string gridObjet
       {
           get { return Langues.gridObjet; }
       }

       static public string gridPrepareBy
       {
           get { return Langues.gridPrepareBy; }
       }

       static public string gridSataut
       {
           get { return Langues.gridSataut; }
       }

       static public string gridTotal
       {
           get { return Langues.gridTotal; }
       }

      


        #endregion

        #region Shell error
       static public string shellVm_MsgBadMotpTitre
       {
           get { return Langues.shellVm_MsgBadMotpTitre; }
       }

       static public string shellVm_MsgBadLogginTitre
       {
           get { return Langues.shellVm_MsgBadLogginTitre; }
       }

       static public string shellVm_MsgBadLoggincontent
       {
           get { return Langues.shellVm_MsgBadLoggincontent; }
       }

       static public string shellVm_MsgBadMotpcontent
       {
           get { return Langues.shellVm_MsgBadMotpcontent; }
       }

       static public string shellVm_MsgCompteLockTitre
       {
           get { return Langues.shellVm_MsgCompteLockTitre; }
       }

       static public string shellVm_MsgCompteLockcontent
       {
           get { return Langues.shellVm_MsgCompteLockcontent; }
       }


       static public string shellVm_MsgTitreCompteStatus
       {
           get { return Langues.shellVm_MsgTitreCompteStatus; }
       }

       static public string shellVm_MsgcontentCompteStatus
       {
           get { return Langues.shellVm_MsgcontentCompteStatus; }
       }


       static public string Fiche_Client
       {
           get { return Langues.fiche_Client; }
       }

       static public string fiche_Produit
       {
           get { return Langues.fiche_Produit; }
       }

       static public string fiche_Utilisateur
       {
           get { return Langues.fiche_Utilisateur; }
       }

       
        #endregion
    }
}
