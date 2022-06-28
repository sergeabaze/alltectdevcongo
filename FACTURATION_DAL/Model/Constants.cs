using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL
{
    /// <summary>
    /// Classe Permettant de lister tes Procedure Stocker
    /// 
    /// </summary>
   internal  class Constants
    {

       internal const int  TimeOut = 100;

        #region PRODUITS

          internal const string PS_PRODUIT_SELECT = "PS_PRODUIT_SELECT";
          internal const string PS_PRODUIT_ADD = "PS_PRODUIT_ADD";
          internal const string PS_PRODUIT_DELETE = "PS_PRODUIT_DELETE";
          internal const string PS_PRODUIT_SELECTBY = "PS_ProduitSelectByName";
       
        #endregion

        #region DETAIL PRODUIT

          internal const string PS_DETAIL_PRODUIT_ADD = "PS_DETAIL_PRODUIT_ADD";
          internal const string F_DETAIL_PRODUIT_FACT_EXIST = "PS_DETAIL_PRODUIT_FACT_EXIST";
          internal const string PS_DETAIL_PRODUIT_SELECT = "PS_DETAIL_PRODUIT_SELECT";
          internal const string PS_DETAIL_PRODUIT_ANNUEL_EXPLOIT = "PS_DETAIL_PRODUIT_ANNUEL_EXPLOIT";
          internal const string PS_DETAIL_PRODUIT_DELETE = "PS_DETAIL_PRODUIT_DELETE";
        
          #endregion

        #region SATUT FACTURE

          internal const string PS_STATUT_FACTURE_DELETE = "PS_STATUT_FACTURE_DELETE";
          internal const string PS_STATUT_FACTURE_SELECT = "PS_STATUT_FACTURE_SELECT";
          internal const string PS_STATUT_FACTURE_ADD = "PS_STATUT_FACTURE_ADD";

          #endregion


        #region PRESTATION

          #endregion

        #region UTILISATEUR

        internal const string GetAllUtilisateur = "PS_UTILISATEUR_SELECT";
        internal const string GetUtilisateurById = "GetUtilisateurById";
        internal const string GetUtilisateurLoggin = "PS_UTILISATEUR_LOGGIN";
        internal const string UtilisateurInsert = "PS_UTILISATEUR_ADD";
        internal const string UtilisateurDelete = "PS_UTILISATEUR_DELETE";
        internal const string UtilisateurUpdateLogg = "PS_UTILISATEUR_UPDATELOGGIN";
        internal const string PS_PROFILE_SELECT = "PS_PROFILE_SELECT";
       
        internal const string PS_UTILISATEUR_INITPASSWORD = "PS_UTILISATEUR_INITPASSWORD";
        internal const string PS_UTILISATEUR_LOGGIN_NEW = "PS_UTILISATEUR_LOGGIN_NEW";
        internal const string PS_UTILISATEUR_LOG_NAME = "PS_UTILISATEUR_LOG_NAME";
        internal const string PS_UTILISATEUR_LOCKCOMPTE = "PS_UTILISATEUR_LOCKCOMPTE";
        internal const string PS_UTILISATEUR_COMPTISLOCK = "PS_UTILISATEUR_COMPTISLOCK";
        internal const string PS_PROFILE_ADD_DATE = "PS_PROFILE_ADD_DATE";
        internal const string PS_PROFILE_DATE_SELECT = "PS_PROFILE_DATE_SELECT";
        #endregion

        #region Administration utilisateurs

        internal const string PS_VUES_SELECT = "PS_VUES_SELECT";
        internal const string PS_DROIT_VUES_PROFILE = "PS_DROIT_VUES_PROFILE";
        internal const string PS_DROIT_VUES_USER = "PS_DROIT_VUES_USER";
        internal const string PS_DROIT_USER_ADD = "PS_DROIT_USER_ADD";
        internal const string PS_DROIT_PROFILE_ADD = "PS_DROIT_PROFILE_ADD";
        internal const string PS_DROIT_DELETE = "PS_DROIT_DELETE";

        #endregion

        #region CLIENT

        internal const string PS_CLIENT_ADD = "PS_CLIENT_ADD";
        internal const string PS_CLIENT_DELETE = "PS_CLIENT_DELETE";
        internal const string PS_CLIENT_SELECT = "PS_CLIENT_SELECT";
        internal const string PS_ClientActive = "PS_ClientActive";
        internal const string PS_CLIENT_SELECT_FACTURE = "PS_CLIENT_SELECT_FACTURE";
        #endregion

        #region COMPTE

        internal const string PS_COMPTE_SELECT = "PS_COMPTE_SELECT";
        internal const string PS_COMPTE_ADD = "PS_COMPTE_ADD";
        internal const string PS_COMPTE_DELETE = "PS_COMPTE_DELETE";
        #endregion

        #region LIBELE TERME

        internal const string PS_LIBELLE_TERME_SELECT = "PS_LIBELLE_TERME_SELECT";
        internal const string PS_LIBELLE_TERME_DELETE = "PS_LIBELLE_TERME_DELETE";
        internal const string PS_LIBELLE_TERME_ADD = "PS_LIBELLE_TERME_ADD";

        #endregion

        #region EXONERATION

        internal const string PS_EXONERATION_SELECT = "PS_EXONERATION_SELECT";
        internal const string PS_EXONERATION_DELETE = "PS_EXONERATION_DELETE";
        internal const string PS_EXONERATION_ADD = "PS_EXONERATION_ADD";
        #endregion

        #region OBJET FACTURE

        internal const string PS_OBJET_FACTURE_ADD = "PS_OBJET_FACTURE_ADD";
        internal const string PS_OBJET_FACTURE_DELETE = "PS_OBJET_FACTURE_DELETE";
        internal const string PS_OBJET_FACTURE_SELECT = "PS_OBJET_FACTURE_SELECT";
        #endregion

        #region OBJET GENERIC

        internal const string PS_OBJET_ADD = "PS_OBJET_ADD";
        internal const string PS_OBJET_SELECT = "PS_OBJET_SELECT";
        internal const string PS_OBJET_DELETE = "PS_OBJET_DELETE";

        #endregion

        #region EXPLOITATION FACTURE

        internal const string PS_EXPLOTATION_FACTURE_ADD = "PS_EXPLOTATION_FACTURE_ADD";
        internal const string PS_EXPLOTATION_FACTURE_DELETE = "PS_EXPLOTATION_FACTURE_DELETE";
        internal const string PS_EXPLOTATION_FACTURE_SELECT = "PS_EXPLOTATION_FACTURE_SELECT";
        #endregion

        #region SOCIETE

        internal const string PS_SOCIETE_ADD = "PS_SOCIETE_ADD";
        internal const string PS_SOCIETE_DELETE = "PS_SOCIETE_DELETE";
        internal const string PS_SOCIETE_SELECT = "PS_SOCIETE_SELECT";
        internal const string PS_SOCIETE_SELECT_DEFAULT = "PS_SOCIETE_SELECT_DEFAULT";
        internal const string PS_SOCIETE_DEL_LOGO = "PS_SOCIETE_DEL_LOGO";

        internal const string PS_SOCIETE_CREATE_SIGNATURE = "PS_SOCIETE_CREATE_SIGNATURE";
        internal const string PS_SOCIETE_SELECT_SIGNATURE = "PS_SOCIETE_SELECT_SIGNATURE";
        internal const string PS_SOCIETE_DELETE_SIGNATURE = "PS_SOCIETE_DELETE_SIGNATURE";
        #endregion

        #region MODE PAIEMENT

        internal const string PS_MODE_PAIEMENT_SELECT = "PS_MODE_PAIEMENT_SELECT";
        internal const string PS_MODE_PAIEMENT_DELETE = "PS_MODE_PAIEMENT_DELETE";
        internal const string PS_MODE_PAIEMENT_ADD = "PS_MODE_PAIEMENT_ADD";
        #endregion

        #region TAXES DEVISES
        internal const string PS_DEVISES_ADD = "PS_DEVISES_ADD";
        internal const string PS_DEVISES_DELETE = "PS_DEVISES_DELETE";
        internal const string PS_DEVISES_SELECT = "PS_DEVISES_SELECT";
        internal const string PS_TAXES_ADD = "PS_TAXES_ADD";
        internal const string PS_TAXES_DELETES = "PS_TAXES_DELETES";
        internal const string PS_TAXES_SELECT = "PS_TAXES_SELECT";
        internal const string PS_TAXES_SELECTBY_REF = "PS_TAXES_SELECTBY_REF";
        #endregion


        #region LANGUE

        internal const string GetLanguage = "PS_LANGUAGE_SELECT";
        internal const string PS_LANGUAGE_ADD = "PS_LANGUAGE_ADD";
        internal const string PS_LANGUAGE_DELETE = "PS_LANGUAGE_DELETE";

        #endregion

        #region FACTURE

        internal const string PS_FACTURE_ADD = "PS_FACTURE_ADD";
        internal const string PS_FactureAvoirADD = "PS_FactureAvoirADD";
        internal const string PS_FACTURE_UPDATE = "PS_FACTURE_UPDATE";
        internal const string PS_FACTURE_DELETE = "PS_FACTURE_DELETE";
        internal const string PS_FACTURE_SELECT = "PS_FACTURE_SELECT";
        internal const string PS_FACTURE_DATECLOTURE = "PS_FACTURE_DATECLOTURE";
        internal const string PS_FACTURE_DATE_SUPENSION = "PS_FACTURE_SUPENSION";
        internal const string PS_FACTURE_DATE_SORTIE = "PS_FACTURE_DATE_SORTIE";
        internal const string PS_FACTURE_DATE_PAIEMENT = "PS_FACTURE_DATE_PAIEMENT";  
        internal const string PS_FACTURE_ADD_NEW = "PS_FACTURE_ADD_NEW";
        internal const string PS_FACTURE_SEARCH_BYDATE = "PS_FACTURE_SEARCH_BYDATE";
        internal const string PS_FACTURE_ADDNUM_VALIDATE = "PS_FACTURE_ADDNUM_VALIDATE";
        internal const string PS_FACTURE_CLOSING = "PS_FACTURE_CLOSING";
        internal const string PS_FACTURE_UPDATE_NEW = "PS_FACTURE_UPDATE_NEW";
        internal const string PS_FACTURE_UPDATE_AVOIR = "PS_FACTURE_UPDATE_AVOIR";

        internal const string PS_FACTURE_A_SUPPRIMER = "PS_FACTURE_A_SUPPRIMER";
        internal const string PS_FACTURES_SORTIES_RESEARCH = "PS_FACTURES_SORTIES_RESEARCH";

       //new 
        internal const string Ps_FactureListBySite = "Ps_FactureListBySite";
        internal const string Ps_FactureListeByDate = "Ps_FactureListeByDate";
        internal const string Ps_FactureGetVersionById = "Ps_FactureGetVersionById";
        internal const string Ps_FactureListById = "Ps_FactureListById";
      
        #endregion

        #region LIGNE FACTURE
        internal const string PS_LIGNE_FACTURE_ADD = "PS_LIGNE_FACTURE_ADD";
        internal const string PS_LIGNE_FACTURE_DELETE = "PS_LIGNE_FACTURE_DELETE";
        internal const string PS_LIGNE_FACTURE_SELECT = "PS_LIGNE_FACTURE_SELECT";
        internal const string PS_DESCRIPTION_LIGNE_SELECT = "PS_DESCRIPTION_LIGNE_SELECT";
        #endregion

        #region REPORT
        internal const string PS_REPORT_CLIENT = "PS_REPORT_CLIENT";
        internal const string PS_REPORT_SOCIETE = "PS_REPORT_SOCIETE";
        internal const string PS_REPORT_PIEDPAGE = "PS_REPORT_PIEDPAGE";
        internal const string PS_LIBELLE_FACTURE = "PS_LIBELLE_FACTURE";
        internal const string PS_REPORT_FACTURE = "PS_REPORT_FACTURE";
        internal const string PS_REPORT_LIGNEFACTURE = "PS_REPORT_LIGNEFACTURE";
        internal const string PS_REPORT_LIGNEFACTURE_NONEXO = "PS_REPORT_LIGNEFACTURE_NONEXO";
        //
        #endregion

        #region Detail Product
       
        #endregion

        #region LIGNE FACTURE

        #endregion

        #region DEPARTEMENT

        internal const string PS_DEPARTEMENT_ADD = "PS_DEPARTEMENT_ADD";
        internal const string PS_DEPARTEMENT_DELETE = "PS_DEPARTEMENT_DELETE";
        internal const string PS_DEPARTEMENT_SELECT = "PS_DEPARTEMENT_SELECT";

        #endregion

        #region LOG DATAS
        internal const string PS_LOG_IMPORT_SELECT ="PS_LOG_IMPORT_SELECT";
        internal const string PS_LOG_IMPORT_ADD = "PS_LOG_IMPORT_ADD";
        internal const string PS_LOG_EXPORT_ADD = "PS_LOG_EXPORT_ADD";
        internal const string PS_LOG_EXPORT_SELECT ="PS_LOG_EXPORT_SELECT";

        internal const string PS_EXPORT_SELECT = "PS_EXPORT_SELECT";
        internal const string PS_IMPORT_ADD = "PS_IMPORT_ADD";
        internal const string PS_LOG_EXPORT_DELETE = "PS_LOG_EXPORT_DELETE";
        internal const string PS_LOG_IMPORT_DELETE = "PS_LOG_IMPORT_DELETE";

        internal const string PS_EXTRACTFILE_ADD = "PS_EXTRACTFILE_ADD";
        internal const string PS_EXTRACTFILE_DELETE = "PS_EXTRACTFILE_DELETE";
        internal const string PS_EXTRACTFILE_SELECT = "PS_EXTRACTFILE_SELECT";
          
        #endregion

        #region Parametres
             internal const string PS_PARAMETER_ADD ="PS_PARAMETER_ADD";
             internal const string PS_PARAMETER_SELECT = "PS_PARAMETER_SELECT";

             internal const string PS_CONFIG_DELETE = "PS_CONFIG_DELETE";
             internal const string PS_CONFIG_SELECT = "PS_CONFIG_SELECT";
             internal const string PS_CONFIG_ADD = "PS_CONFIG_ADD";
        #endregion

          

        #region COMPUTER LOGGIN
             internal const string PS_COMPUTER_CONNECT_ADD = "PS_COMPUTER_CONNECT_ADD";
             internal const string PS_COMPUTER_CONNECT_SELECT = "PS_COMPUTER_CONNECT_SELECT";
             internal const string PS_COMPUTER_CONNECT_DELETE = "PS_COMPUTER_CONNECT_DELETE";
         
        #endregion

        #region LICENSES
             internal const string PS_LICENSE_ADD = "PS_LICENSE_ADD";
             internal const string PS_LICENSE_SELECT = "PS_LICENSE_SELECT";
             internal const string PS_LICENSE_UPDATE = "PS_LICENSE_UPDATE";
             internal const string PS_LICENSE_UPDATE_DATECONECTION = "PS_LICENSE_UPDATE_DATECONECTION";
             internal const string PS_LICENSE_SELECTALL = "PS_LICENSE_SELECTALL";
             internal const string PS_LICENSE_SELECTLIST = "PS_LICENSE_SELECTLIST";
             internal const string PS_LICENSE_ACTIVE = "PS_LICENSE_ACTIVE";

             #endregion

        #region Display langues
             internal const string PS_LISTELANGUE_SELECT = "PS_LISTELANGUE_SELECT";
             internal const string PS_GET_DETAIL_DISPLAYVIEWS = "PS_GET_DETAIL_DISPLAYVIEWS";
             #endregion

        #region BACK Up
             internal const string PS_BACKUP_LOG_SELECT = "PS_BACKUP_LOG_SELECT";
             internal const string PS_BACKUP_LOG_ADD = "PS_BACKUP_LOG_ADD";
             internal const string PS_FactureSelectMinMaxDateCreate = "PS_FactureSelectMinMaxDateCreate";
             internal const string PS_ARCHIVE_SELECT = "PS_ARCHIVE_SELECT";
             internal const string PS_ARCHIVE_DELETE = "PS_ARCHIVE_DELETE";

             #endregion

        #region Comptabilite

             internal const string PS_JV_DATE_ADD = "PS_JlVente_DateInsert";
             internal const string PS_JV_DATE_SELECT = "PS_JlVente_DateSelect";
             internal const string PS_JV_DATE_DELETE = "PS_JlVente_DateDelete";
             internal const string PS_JV_DATE_PERIODE = "PS_JlVente_DatePeriodes";
             internal const string PS_JV_DATE_PERIODE_ISExport = "PS_JlVente_DateIsExport";
             internal const string PS_JV_HSTCMPT= "PS_JournalVente_selectGenerated";
             internal const string PS_JV_HSTCMPANAL = "PS_JournalVente_selectGeneratedAnalytique";
             internal const string PS_JV_HSTCMPGROUPE = "PS_JournalVente_selectGeneratedGrouby";
             internal const string PS_JV_ExportGererate = "PS_JournalVente_ToxExport";
             internal const string PS_JV_ExportUpdate = "PS_JournalVente_ToxExportUpdate";

             internal const string PS_CMPTA_LISTE_SELECT = "PS_CmptaGeneListeChampSelect";
             internal const string PS_CMPTA_LISTE_DELETE = "PS_CmptaGeneListeChampDelete";
             internal const string PS_CMPTA_LISTE_ADD = "PS_CmptaGeneListeChampAdd";

             internal const string PS_CMPTA_CHAMP_SELECT = "PS_CmptaGeneChampSelect";
             internal const string PS_CMPTA_CHAMP_DELETE = "PS_CmptaGeneChampDelete";
             internal const string PS_CMPTA_CHAMP_ADD = "PS_CmptaGeneChampAdd";

             internal const string PS_CMPTA_CHAMP_PARAMETRE = "PS_ComptaGeneChampsParametres";



       
        #endregion

    }
}
