using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL.Model;
using System.Data;
using System.Collections.ObjectModel;
using MySql.Data.MySqlClient;

namespace FACTURATION_DAL
{
   public  interface IFacturation
    {
        #region UTILISATEUR

        List<Utilisateur> GetAllUtilisateur(bool mode);
        List<Utilisateur>  GetAllUtilisateur();
        Utilisateur GetUtilisateurLoggin(string loggin, string motPasse);
        Utilisateur GetUtilisateur_ById(int id);
        Int32  UTILISATEURADD(Utilisateur user);
        bool UTILISATEURDELETE(int Iduser);
        List<Profile> GetUtilisateurProfile();
        Profile GetUtilisateurProfileById(int idProfile);
        List<Droit> GetList_DROITS(int idprofile, int idUser);
        List<Droit> GetList_DROITS_Admin(int idprofile, int idUser);
        List<Droit> GetList_DROITS(int idprofile);

        bool Utilisateur_InitPassword(int idUser, string defaultpass, int idprofileAdmin);
        bool Utilisateur_LogName(string userloggin);
        Utilisateur GetUtilisateurLoggin_New(string loggin, string motPasse, string nouveauPass);
        bool Utilisateur_IsAcountLock(string userloggin, string password);
        bool Utilisateur_LockAccount(int idUser);
        bool ProfileUpdatedate(int id, int idprofile, int idUser, DateTime? dateDebut, DateTime? dateFin);

        Utilisateur GetUtilisateur_Archive_ById(int id);
       List<Utilisateur> GetAllUtilisateur_Archive(bool modeUser);
        #endregion

        #region ADMINISTRATION UTILISATEUR

        Dictionary<ClasseVues, List<ClasseVues>> GetListeVues();
        List<Droit> Get_DROITS_PROFILE(int idprofile);
        List<Droit> Get_DROITS_USER(int idprofile, int idUser);
        bool DROITS_DELETE(int Id, int idprofile, int iduser);
        bool DROITS_USER_ADD(Droit droit);
        bool DROITS_PROFILS_ADD(Droit droit);

        #endregion

        #region CLIENT

        bool CLIENT_ADD(ref Int32 ID,Client client);
        bool CLIENT_ADD_Archive(Int32 IDcLient, Int32 IdComptetiers, string nomClientCompta, Int32 idSite);
        bool CLIENT_DELETE(int Id);
        Client GetALL_CLIENTByID(int idClient);
        Client GetALL_CLIENTByID(int idClient,Int32  idSite);
        DataTable GetCLIENTByID(int idClient, Int32 idsite);
        List<Client> GetALL_CLIENT(Int32 idsite, bool isActif);
       // List<Client> GetALL_CLIENT(Int32 idsite);
      
        List<Client> GetALL_CLIENTBY(Int32 idsite, string nomClient, string Ville);
        bool ARCHIVE_CLIENT_ADD(int IdClient, int IdLangue, int Idporata, int IdSite, int IdExonere, int IdCompte, int IdDeviseFact, int IdTerme, string NumeroImatriculation,
              string TermeDescription, int TermeNombre, string Rue1, string Rue2, string NomClient, string NumeroContribuable, string Ville, string BoitePostal,string  DateEcheance);
        bool CLIENT_ACTIF(int IdClient, int idSite, bool isActive);
        List<Client> GetALL_CLIENT_FACTURER(Int32 idsite, int mode, int archive, DateTime dateFrom, DateTime DateTo);

        DataTable GetCLIENT_Archive_ByID(int idClient, Int32 idsite, Int64 idFacture);
        Client GetALL_CLIENTByIDArchive(int idClient, Int64 idFacture);
        Client GetALL_CLIENT_ArchiveByID(int idClient, Int32 idsite, Int64 idFacture);
        List<Client> GetALL_CLIENT_ARCHIVES(Int32 idsite, bool isActif);
        List<Client> GetALL_CLIENT_NOT_MONTHLYCREATE(Int32 idsite);

        #endregion

        #region COMPTE
        List<Compte> GetAll_COMPTESite(int idSite);
    
        Compte Get_COMPTEBYID(Int32 id);
        List<Compte> GetAll_COMPTE();
        object  COMPTE_ADD(Compte compte);
        bool COMPTE_DELETE(int Id);

        Compte Get_COMPTEBYIDArchive(Int32 id);
        List<Compte> GetAll_COMPTESiteArchive(int idSite);
        List<Compte> GetAll_COMPTEARchive();
        #endregion

        #region EXONERATION

        List<Exoneration> GetAll_EXONERATION();
        Exoneration Get_EXONERATIONBYID(Int32 id);
        bool EXONERATION_ADD(Exoneration exo);
        bool EXONERATION_DELETE(int Id);
        #endregion

        #region LANGUE
        List<Langue> GetALLLangue();
        Langue GetLANGUEBY_ID(int id);
        bool language_DELETE(Int32 id);
        bool language_ADD(Langue langue);
            
        #endregion

        #region OBJET FACTURE

        List<ObjetFacture> GetAll_OBJET_FACTURE(Int32 idSite);
        List<ObjetFacture> GetAll_OBJET_FACTUREBYLangue(Int32 idlangue, Int32 idSite);
        ObjetFacture GetAll_OBJET_FACTUREBYID(Int32 id, Int32 idSite);
        
        List<ObjetFacture> GetAll_OBJET_FACTUREBYCLIENT(Int32 id, Int32 idClient);
        bool OBJET_FACTURE_ADD(ObjetFacture objFacture,Int32 idSite);
        int  OBJET_FACTURE_DELETE(int Id);

        List<ObjetFacture> GetAll_OBJET_Archive_FACTURE(Int32 idSite);
        List<ObjetFacture> GetAll_OBJET_Archive_FACTUREBYLangue(Int32 idlangue, Int32 idSite);
        List<ObjetFacture> GetAll_OBJET_Archive_FACTUREBYByClient(Int32 idSite, Int32 idClient);
        ObjetFacture GetAll_OBJET_Archive_FACTUREBYID(Int32 id, Int32 idSite);
        ObjetFacture GetAll_OBJET_Archive_FACTUREBYID(Int32 id);
        List<ObjetFacture> GetAll_OBJET_Archive_FACTUREBYCLIENT(Int32 id, Int32 idClient);
        #endregion

        #region OBJET GENERIQUE

        List<ObjetGenerique> GetAll_OBJET_GENERIQUEBYSITE(Int32 idSite);
        List<ObjetGenerique> GetAll_OBJET_GENERIQUELangue(Int32 idlangue, Int32 idSite);
        List<ObjetFacture> GetAll_OBJET_FACTUREBYByClient(Int32 idSite, Int32 idClient);
        ObjetGenerique GetAll_OBJET_GENERIQUEBYID(Int32 id);
        DataTable GetAll_OBJET_GENERIQUEBYID(Int32 id, bool isArchive);
        bool OBJET_GENERIQUE_ADD(ObjetGenerique objFacture);
        bool OBJET_GEERIQUE_DELETE(int Id);

        DataTable GetAll_OBJET_GENERIQUEBYID_Archive(Int32 id, bool isArchive);
        ObjetGenerique GetAll_OBJET_GENERIQUEBYID_Archive(Int32 id);
        List<ObjetGenerique> GetAll_OBJET_GENERIQUELangue_Archive(Int32 idSite, Int32 idlangue);
        List<ObjetGenerique> GetAll_OBJET_GENERIQUEBY_NON_Client(Int32 idClient, Int32 idSite, Int32 idlangue);
        List<ObjetGenerique> GetAll_OBJET_GENERIQUEBYSITE_Archive(Int32 idSite);
        #endregion

        #region LIBELLE  TERME

        List<Libelle_Terme> GetAll_LIBELLE(int idlangue);
        List<Libelle_Terme> GetAll_LIBELLEArchive(int idlangue);
        Libelle_Terme Get_LIBELLEById(int id);
        Libelle_Terme Get_LIBELLE_ArchiveById(int id);
      

        #endregion

        #region EXPLOITATION FACTURE

        List<OverviewFacture> GetAll_OVERVIEW(int idUser);
        bool OVERVIEW_ADD(OverviewFacture ov);
        List<Facture> GetAll_FACTURENEW(Int32 idSite, bool modefacture);
        List<ExploitationFacture> GetAll_EXPLOITATION_FACTURE(Int32 idSite);
        List<ExploitationFacture> GetAll_EXPLOITATION_FACTUREBYLangue(Int32 idlangue, Int32 idSite);
        List<ExploitationFacture> GetAll_EXPLOITATION_BYCLIENT(Int32 idSite, Int32 idClient);
        ExploitationFacture GetAll_EXPLOITATION_FACTUREBYID(Int32 id, Int32 idSite);
        ExploitationFacture GetAll_EXPLOITATION_FACTUREBYID(Int32 id);
        bool EXPLOITATION_FACTURE_ADD(ExploitationFacture objFacture);
        bool EXPLOITATION_FACTURE_ADD_ARCHIVE(ExploitationFacture objFacture);
        bool EXPLOITATION_FACTUREE_ADD(Int64 idFacture, int idexploitation, int mode);
        int  EXPLOITATION_FACTURE_DELETE(int Id);

        ExploitationFacture GetAll_EXPLOITATION_Archive_FACTUREBYID(int id, Int32 idSite);
        ExploitationFacture GetAll_EXPLOITATION_Archive_FACTUREBYID(int id);
        List<ExploitationFacture> GetAll_EXPLOITATION_Archive_FACTUREBYLangue(Int32 idlangue, Int32 idSite);
        List<ExploitationFacture> GetAll_EXPLOITATION_Archive_FACTURE(Int32 idSite);
        List<ExploitationFacture> GetAll_EXPLOITATION_Archive_BYCLIENT(Int32 idSite, Int32 idClient);
        #endregion   #endregion

        #region SOCIETE
        bool SOCIETE_DELETE();
        bool SOCIETE_ADD(Societe societe);
        Societe Get_SOCIETEById(int id);
        Societe Get_SOCIETE_FEFAULT();
        List<Societe> GetAll_SOCIETE_LIST();
       bool  SOCIETE_DELETE_LOGO(int type, int idSociete);

        bool SOCIETE_DELETE_SIGNATURE(Int32 idste);
        bool SOCIETE_ADD_SIGNATURE(Int32 idste, byte[] signature);
        byte[] Get_SOCIETE_SIGNATURE();
        #endregion

        #region MODE PAIEMENT

        bool MODE_PAIEMENT_DELETE(int Id);
        bool MODE_PAIEMENT_ADD(ModePaiement objFacture);
        ModePaiement GetAll_MODE_PAIEMENTBYID(Int32 id);
        List<ModePaiement> GetAll_MODEPAIEMENT_BYLangue(Int32 idlangue);
        List<ModePaiement> GetAll_MODE_PAIEMENT();
        #endregion

        #region PRODUIT

        bool PRODUIT_DELETE(int Id);
        bool PRODUIT_ADD(Produit objFacture);
        Produit Get_PRODUITBYID(Int32 id);
        Produit Get_PRODUITBYID(Int32 id, Int32 idSite);
        List<Produit> GetAll_PRODUIT_BYLangue(Int32 idlangue, Int32 idSite);
        List<Produit> GetAll_PRODUIT(Int32 idSite);
        List<Produit> Get_PRODUISEARCH(Int32 idSite, string libelle);

        List<Produit> GetAll_PRODUIT_Archive(Int32 idSite);
        List<Produit> GetAll_PRODUIT_BYLangue_Archive(Int32 idlangue, Int32 idSite);
        Produit Get_PRODUITBYID_Archive(Int32 id);
        Produit Get_PRODUITBYID_Archive(Int32 id, Int32 idSite);
        DataTable GetAll_PRODUIT_ByCLIENT(Int32 idSite, int idClient);
        bool PRODUIT_SUIVI_ADD(int ID, int idClient, int idProduit, int idSite, bool isParam);
      
        #endregion

        #region DETAIL PRODUIT
        List<ProduitDetail> GetAll_DETAIL_PRODUITBY_PRODUIT(int idProduit, int idClient);
        ProduitDetail  GetAll_DETAIL_PRODUITBy_Id(int id);
        List<ProduitDetail> GetAll_DETAIL_PRODUITBY_CLIENT(int idClient, Int32 idSite);
        bool DETAIL_PRODUIT_DELETE(int Id);
        bool PRODUIT_DETAIL_ADD(ProduitDetail detailProd);
        bool DETAIL_PRODUIT_ANNULER_EXPLOITATION(int Id, int idExploit, int idSite);
        bool DETAIL_PRODUIT_FACT_EXIST(int idsite, int idDetailProduit);

        List<ProduitDetail> GetAll_DETAIL_PRODUITBY_PRODUIT_Archive(int idProduit, int idClient);
        List<ProduitDetail> GetAll_DETAIL_PRODUITBY_CLIENT_Archive(int idClient, Int32 idSite);
        ProduitDetail GetAll_DETAIL_PRODUITBy_Id_Archive(int id);
        #endregion

        #region STATUT FACTURE

        List<StatutFacture> GetAll_STATUT_FACTURE();
        StatutFacture GetAll_STATUT_FACTUREBYID(Int32 id);
        List<StatutFacture> GetAll_STATUT_FACTUREBYLangue(Int32 idlangue);
        bool STATUT_FACTURE_DELETE(int Id);
        bool STATUT_FACTURE_ADD(StatutFacture objFacture);
        #endregion

        #region TAXES DEVISES

        List<Taxe> Taxes_SELECT(int id, int idSite);
        List<Taxe> Taxes_SELECTARCHIVE(int id, int idSite);
        Taxe Taxes_SELECTByIdArchive(int id, int idSite);
        DataTable Taxes_SELECTByDataTAbleArchive(int idSite);

        Taxe Taxes_SELECTById(int id, int idSite);
        DataTable Taxes_SELECTByDataTAble(int idSite);
        List<Taxe> Taxes_SELECTByRef(string reference, int idSite);
        List<Taxe> Taxes_SELECTByRef_Archive(string reference, int idSite);
       
        bool TaxeADD(Taxe taxe);
        bool TaxeDELETE(int Id);

        List<Devise> GetAllDevise(int idsite);
        List<Devise> GetAllDeviseArchive(int idsite);
        Devise GetAllDeviseArchiveById(int id, int idSite);
        Devise GetAllDeviseById(int id, int idSite);
        bool DeviseADD(Devise devise);
        bool DeviseDELETE(int Id);


        List<Devise> GetAllDeviseClient(int idsite);
        Devise GetAllDeviseClientById(int id, int idSite);
        bool DeviseClientADD(Devise devise);
        bool DeviseClientDELETE(int Id);

        #endregion

        #region FACTURE

        DataRow FactureByID(Int64 ID);
        DataTable FactureVersion(Int64 idFacture);
        DataTable FactureList(Int32 idSite);
        DataTable FactureListeByDate(Int32 idSite, DateTime dateDebut, DateTime dateFin, int idClient);
        List<Facture> SEARCH_FACTURE_BYNUM(string numFacture, string mode);
        List<Facture> GetAll_FACTURE(Int32 idSite);
        Facture GetAll_FACTUREBYID(long id);
        Facture GetAll_FACTUREBYID(long id, Int32 idSite);
        bool FACTURE_ADD(Facture objFacture, out  object idFacture);
        //bool FACTURE_UPDATE(Facture objFacture);
        bool FACTURE_UPDATE(ref string oldfactureMax, ref string newfacture, Facture objFacture);
        List<Facture> LIST_FACTURE_SORTIE(Int32 idSite, DateTime dateDebut, DateTime dateFin, int mode);

        bool FACTURE_CLOSING(long id, DateTime dateCloture, int idstatut, int iduserModif, bool ops, Facture facture);
        bool FACTURE_DELETE(long Id, int estDel, int idSite, int idUser);
        bool FACTURE_SUSPENSION(long id, DateTime dateCloture, int idstatut,int iduserModif, bool ops);
        bool FACTURE_SORTIE(long id, DateTime dateCloture, int idstatut,int iduserModif, bool ops);
        bool FACTURE_PAIEMENT(long id, DateTime dateCloture,int iduserModif, bool ops);
        bool FACTURE_NONVALABLE(long id, int idstatut,int iduserModif, bool ops);
        object FACTURE_ADD_ITEMS(Facture objFacture, List<LigneFacture> items, bool estNormalAvoir);
        void FACTURE_ADD_VerifHTTC(Int64 idfacture);
        List<Facture> SEARCH_LIST_FACTURE(Int32 idSite, DateTime dateDebut, DateTime dateFin, int idClient);
        bool FACTURE_UPDATE_WITHOUT_UPDATE(ref string oldfactureMax, ref string newfacture, Facture objFacture);
        bool FACTURE_UPDATE(Facture objFacture);
       // bool FACTURE_CLOSING_UPDATE(Facture objFacture, MySqlTransaction transaction);
        Facture GetAll_FACTUREVALIDATE_BYID(Int64 idFacture, int idSite, int Mode);
        DataTable LISTE_FACTURE_TO_DEL(int idsite);
        bool FACTURE_UPDATE_STATUS(long id, int idstatut, int iduserModif, int ops);

        bool IsfactureArchiveExists(int idSite);
        List<Facture> GetAll_FACTURE_Archive(Int32 idSite, DateTime periode);
        List<Facture> SEARCH_LIST_FACTURE_Archive(Int32 idSite, DateTime dateDebut, DateTime dateFin, int idClient);
        DataTable Facture_Periode_Archive(int IdSite);
        #endregion

        #region LIGNE FACTURE

        List<LigneFacture> GetAll_LIGNE_FACTURE(long idFacture);
        LigneFacture Get_LIGNE_FACTUREById(long idLigne);
        bool LIGNE_FACTURE_ADD(LigneFacture objFacture);
        bool LIGNE_FACTURE_DELETE(long Id,int mode);
        DataTable GEt_DESCRIPTION_LIGNE(string valeur);

        LigneFacture Get_LIGNE_FACTUREById_Validate(long idLigne);
        List<LigneFacture> GetAll_LIGNE_FACTURE_Validate(long idFacture);

        LigneFacture Get_LIGNE_FACTUREById_archive(long idLigne);
        LigneFacture Get_LIGNE_FACTUREById_Archive(long idLigne);
        List<LigneFacture> GetAll_LIGNE_FACTURE_Archive(long idFacture);
        #endregion

        #region LOG DATAS
        DataTable Extraction_Select(int idsite);
        bool EXtractionFile_ADD(int id, DateTime dateExtraction, DateTime dateImport, DateTime dateValide, string periode, int idSite,
            int idSiteMaj, string statut, int ordreStatut, string nomFichier);
        bool EXtractionFile_DELETE(int id, int idSite, string fichier);

        bool LogDataExport_ADD(int id, string periode, int idSite, string statut, string nomFichier);
        DataTable LogDatas_Export_Select(int idSite);
        bool LogDataImport_ADD(int id, string periode, int idSite, string statut, string nomFichier);

        DataTable LogDatas_Import_Select(int idSite);
        DataSet Get_Export_ListeFacture(DateTime dateDebut, DateTime dateFin, Int32 idSite);
        bool  SetImport_Facture_UPDATE(Facture objFacture);
        bool LogDataIMPORT_DELETE(int id, int idSite);
        bool LogDataExport_DELETE(int id, int idSite);
        #endregion


        #region REPORT

        DataTable ReportClient(int idclient);
        DataTable ReportClientArchive(int idclient, Int64 idFacture);
        DataTable ReportSociete(int id);
        DataTable ReportSocieteArchive(int id);
        DataTable ReportPiedPAge(int idSociete);
        DataTable ReportPiedPAgeArchive(int idSociete);
        DataTable ReportLIBELLEArchive(int idlangue);
        DataTable ReportLIBELLE(int idlangue);
        DataTable ReportFACTURE(long  idfacture);
        DataSet ReportFACTURENEW(long idfacture, int idSite);
        DataSet ReportFACTURE_AVOIR(long idfacture, int idSite);
        DataSet ReportFACTURE_Archive(long idfacture, int idSite);
        DataTable ReportLIGNEFACTURE(long idfacture);
        DataTable ReportLIGNEFACTURE_NONEXO_PARTIEL(long idfacture);
        double ReportMontantFactureLettre();
       
        #endregion

        #region DEAPRTEMENT
        List<Departement> GetAll_DEPARTMENTS(int idSite);
        Departement Get_DEPARTMENTSByID(int idDept);
        bool DEPARTEMENT_ADD(Departement dep);
        bool DEPARTEMENT_DELETE(int Id);

        List<Departement> GetAll_Archive_DEPARTMENTS(int idSite);
        Departement Get_Archive_DEPARTMENTSByID(int idDept);
        #endregion

        #region CONFIGURATION

        bool CONFIGURATION_ADD(Settings config);
        bool CONFIGURATION_DELETE(string code, Int32 idSite);
        List<Settings> CONFIGURATION_SELECT(string code, Int32 idSite);
        Settings CONFIGURATION_SELECTBYCODE(string code, Int32 idSite);
        #endregion

        #region PARAMETRES_LICENSE

        object[] PARAMETRES_SELECTBYMODE(string mode);
        bool PARAMETRES_ADD(string mode, string valeur, DateTime? datedebut);
        #endregion

        #region COMPUTER CONNECT
        bool Computer_Connect_Delete(string NomMachine);
        bool Computer_Connect_ADD(string  NomMachine, string nomUtilisateur);
        DataTable Computer_connect_select();
        #endregion

        #region BACK Up

        DataSet BackUpList(int idSite);
        DataTable BackUmaxMindate(int idSite);
      
        DataTable ArchiveSelecte(int idSite, DateTime periodeDebut, DateTime periodeFin);
        DataTable ArchiveGenerate_List(int idSite, DateTime periodeDebut, DateTime periodeFin, bool estValider, bool estEncours, bool EstSortie, bool EstAvoir, bool EstSupendu, string cheminLog);
        bool ArchiveGenerate(Int64 IdFacture, Int32 idSite);
        
        
        bool ArchiveDelete(int idSite, DateTime periodeDebut, DateTime periodeFin);
        DataTable ArchiveSelecteLignesFactures(Int64 idfacture);
        bool OBJET_ARCHIVE_ADD(int id, Int32 idSite, string libelle, int idLangue);
        bool OBJET_FACTURE_ARCHIVE_ADD(int id, int idOjetGen, int idClient, int idSte);
        bool ARCHIVE_EXPLOITATION_ADD(ExploitationFacture objFacture);
        bool ARCHIVE_TAXE_ADD(Taxe taxe);
        DataTable Archive_previous_FreeClients(int idsite);
        bool Archive_previous_FreeClientsDelete(int idsite, Int32 inidClient);
        bool Archive_previous_FreeProductsDelete(int idsite, Int32 inidProduit);
        DataSet Archive_previous_FreeProduct(int idsite);
        #endregion

        #region COMPTABILITE

        DataTable GetComptaGene_Liste_ChampFichier();

        DataTable GetJournalVentesDates_Periodes(int idSite);
        List<JournalVentesDates> GetJournalVentesDates_List(int idSite, DateTime periode);
        bool JournalVenteDatesADD(ref int idreturn, JournalVentesDates jv);
        bool JournalVenteDatesDELETE(int id);
        DataTable GetJournalVentesListeByPeriode(int idSite, DateTime dateDebut, DateTime dateFin);
        bool InsertJournalVentes(int id, Int64 idFacture, int idClient);
        List<JournalVentes> GetJournalVente_Historiquegenerate(ref DataTable ListeFact,int idDate,string mode);
        List<JournalVentes> GetJournalVente_Search(ref DataTable ListeFact, string numeroFacture);
        bool JournalVenteHistoriqueUpdateNote(int idDate, int idLigne, string numero);
        bool GenerationJournalVentesGlobal(int idDate, DataRow row, bool isnextDay);
        bool DateJournalUpdate(int idDate, int nombrefActure);
       bool DateJournalInsert(ref int idDate,int nbreFacture, int idSite, DateTime dateDebut, DateTime dateFin);
       DataTable GetFactyreByIdjv(Int64 IdFacture);
       bool JournalVenteTesteDelete(int idDate);
       DataTable JournalVente_periodeMois(string Annee);

        DataTable JournalVenteGetListe(int idSite, DateTime dateDebut, DateTime dateFin);
       // List<JournalVentes> GetJournalVente_Historique(int idDate);
        //bool JournalVenteHistoriqueUpdateNote(int id, int idLigne, string numero);
        List<JournalVenteGroupe> GetJournalVente_HistoriqueGroupBy(int idDate);
        DataSet GetJournalListToExport(int idDate,string mode);
        bool JournalVenteToxexportupdate(int inIdDate);

        DataTable GetComptaGene_Param_Liste();
        bool GetComptaGene_Param_Add(int id, int idChamp, int taille, int position);
        bool GetComptaGene_Param_Delete(int id);
        DataTable GetComptaGene_Liste();
        bool GetComptaGene_Add(int id, string libelle,int code);
        bool GetComptaGene_Delete(int id);

        #endregion

        DataTable ListeLangue();
        DataTable ListeLabelInVieuw(int idlanguages, int idMenus);
        DataTable LicenseSelect(int idSite, string nomMachine, string diskdur, Guid gui);
        bool LicenseADD(int idSite, string NomMachine, string numeroLicense, string diskdur, Guid gui, string hostName, string ipName);
        bool LicenseUPDATE(int idSite, string NomMachine, string numeroLicense, string diskdur, Guid gui);
        bool LicenseLASTCONNECTION(int idSite, string NomMachine, string numeroLicense, string diskdur, Guid gui);
        int LicenseSelectAll(int idSite);
        bool LicenseACTIVE(int idSite, string NomMachine, bool isActive);
        DataTable LicenseSelectListComputer(int idSite);


        DataTable GetListFactures(int idSite);
        bool UpdateDatas_from_database(Int64 idFacture, Int32 idSite);
        bool BackUp_Clean_tables(Int32 idSite);
        bool BackUpBD_DELETE(Int32 ID);
        DataTable BackUpBD_SELECT(int ID);
        bool BackUpLoggAdd(ref Int32 id ,Int32 idSite, DateTime dateFrom, DateTime dateTo, string backUpby, string observation,
            Int32 stat1, Int32 stat2, Int32 stat3, Int32 stat4, Int32 stat5);
        bool BackUpLoggDelete(int id);
        bool BackUpBD_ADD(Int32 ID, string CreateBY, string ModeExportimport, string BackupDump, string FromDB, string ToDB);

        bool LogComptaDeLETE(int idDateJv);
        bool LogComptaAdd(int idDateJv, string numerofact, string MessageError, string typeMesage, bool valMessage);
        DataTable GetLogComptaByJv(int idDateJv, string typeMesage);
    }
}
