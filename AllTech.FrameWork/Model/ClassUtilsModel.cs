using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
   public  class ClassUtilsModel
    {
       static IFacturation DAL = null;

       public static DataTable ListeLangue()
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.ListeLangue();
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       #region License
       
     
       public static DataTable LicenseListComputer(int idSite)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.LicenseSelectListComputer (idSite);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public static DataTable LicenseSelect(int idSite, string nomMachine, string disquedur, Guid guid)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.LicenseSelect(idSite, nomMachine, disquedur, guid);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public static int  LicenseSelectAll(int idSite)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.LicenseSelectAll(idSite);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }


       public static bool LicenseADD(int idSite, string   NomMachine, string numeroLicense, string disquedur, Guid guid, string hostName, string ipName)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.LicenseADD(idSite, NomMachine, numeroLicense, disquedur, guid, hostName, ipName);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public static bool LicenseACTIVE(int idSite, string NomMachine, bool isActive)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.LicenseACTIVE(idSite, NomMachine, isActive);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public static bool LicenseUPDATE(int idSite, string   NomMachine, string numeroLicense, string disquedur, Guid guid)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.LicenseUPDATE(idSite, NomMachine, numeroLicense, disquedur, guid);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public static bool LicenseLASTCONNECTION(int idSite, string   NomMachine, string numeroLicense, string disquedur, Guid guid)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.LicenseLASTCONNECTION(idSite, NomMachine, numeroLicense, disquedur, guid);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       #endregion

       #region BACK UP



       public static  DataTable BackUpBD_SELECT(int ID)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.BackUpBD_SELECT(ID);

       }


       public static bool BackUpBD_ADD(Int32 ID, string CreateBY, string ModeExportimport, string BackupDump, string FromDB, string ToDB)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.BackUpBD_ADD( ID,  CreateBY,  ModeExportimport,  BackupDump,  FromDB,  ToDB);

       }


       public static bool BackUpBD_DELETE(Int32 ID)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.BackUpBD_DELETE(ID);

       }





       public static bool BackUp_CleanupTables(Int32 idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.BackUp_Clean_tables(idSite);
           
       }

       public static bool BackUp_LogDELETE(Int32 ID)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.BackUpLoggDelete(ID);

       }


       public static bool BackUp_LogADD(ref int id,int idSite, DateTime dateFrom, DateTime dateTo, string backUpby, string observation,
            Int32 stat1, Int32 stat2, Int32 stat3, Int32 stat4, Int32 stat5)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.BackUpLoggAdd(ref id, idSite,  dateFrom,  dateTo,backUpby,  observation,
             stat1,  stat2,stat3,  stat4,  stat5);

       }

       public static string BackUpdatabase()
       {
           return DataProviderObject.GetStringConnection;
           //DAL = (Facturation)DataProviderObject.FacturationDal;

       }


       public static DataSet BackUpLoggListe(int idSite)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.BackUpList(idSite);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       public static DataTable BackUpMaxMinDateFacture(int idSite)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.FacturationDal;
               return DAL.BackUmaxMindate(idSite);
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
       }

       //public static bool BackUpLoggAdd(int idSite, DateTime dateFrom, DateTime dateTo, string backUpby, string observation)
       //{
       //    bool valReturn = false;
       //    try
       //    {
       //        DAL = (Facturation)DataProviderObject.FacturationDal;
       //        valReturn = DAL.BackUpLoggAdd(idSite, dateFrom, dateTo, backUpby, observation);
       //    }
       //    catch (Exception ex)
       //    {
       //        throw new Exception(ex.Message);
       //    }
       //    return valReturn;
       //}
       #endregion

       #region ARCHIVES

       public static DataTable GetListeFacturesUpdates(int idSite)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.GetListFactures(idSite);

       }

       public static bool Updates_New_Datas(Int64 idFacture, int idSite)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.UpdateDatas_from_database(idFacture,idSite);

       }


       public static DataTable ArchiveSelecte(int idSite, DateTime periodeDebut, DateTime periodeFin)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.ArchiveSelecte(idSite, periodeDebut, periodeFin);

       }

       public static DataTable ArchiveGenerate_List(int idSite, DateTime periodeDebut, DateTime periodeFin, bool estValider, bool estEncours, bool EstSortie, bool EstAvoir, bool EstSupendu, string cheminLog)
       {
              DAL = (Facturation)DataProviderObject.FacturationDal;
              return DAL.ArchiveGenerate_List(idSite, periodeDebut, periodeFin, estValider, estEncours, EstSortie, EstAvoir, EstSupendu, cheminLog);
       }

       public static bool ArchiveGenerate(Int64 idfacture, Int32 idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.ArchiveGenerate(idfacture, idSite);
       }

       public static DataTable ArchiveSelecteLignesFact(Int64 idFacture)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.ArchiveSelecteLignesFactures(idFacture);

       }


       public static DataSet Previous_free_Products(int idSite)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.Archive_previous_FreeProduct(idSite);

       }

       public static bool free_product_delete(int idSite, Int32 idProduit)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.Archive_previous_FreeProductsDelete(idSite, idProduit);

       }


       public static DataTable Previous_free_Customer(int idSite)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.Archive_previous_FreeClients(idSite);

       }

       public static bool free_Customer_delete(int idSite, Int32 idClient)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.Archive_previous_FreeClientsDelete(idSite, idClient);

       }



       public static bool ArchiveDelete(int idSite, DateTime periodeDebut, DateTime periodeFin)
       {

           DAL = (Facturation)DataProviderObject.FacturationDal;
           return DAL.ArchiveDelete(idSite, periodeDebut, periodeFin);

       }

       public static bool OBJECT_ADD(int id, Int32 idSite, int idlangue, string libelle)
       {

           try
           {
               DAL = (Facturation)DataProviderObject.GetStringArchive;
               DAL.OBJET_ARCHIVE_ADD(id, idSite, libelle, idlangue);
               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public static bool OBJECT_FACTURE_ADD(int id, Int32 idSite, int idClient, int idobjet)
       {

           try
           {
               DAL = (Facturation)DataProviderObject.GetStringArchive;
               DAL.OBJET_FACTURE_ARCHIVE_ADD(id, idobjet, idClient, idSite);
               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public static bool EXPLOITATION_FACTURE_ADD(ExploitationFactureModel exploitation)
       {

           try
           {
               DAL = (Facturation)DataProviderObject.GetStringArchive;
               DAL.ARCHIVE_EXPLOITATION_ADD(ConvertTo(exploitation));
               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public static bool TAXE_ADD(TaxeModel taxe)
       {

           try
           {
               DAL = (Facturation)DataProviderObject.GetStringArchive;
               DAL.TaxeADD(converTo(taxe));
               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public static bool CLIENT_ADD(int IdClient, int IdLangue, int Idporata, int IdSite, int IdExonere, int IdCompte, int IdDeviseFact, int IdTerme, string NumeroImatriculation,
              string TermeDescription, int TermeNombre, string Rue1, string Rue2, string NomClient, string NumeroContribuable, string Ville, string BoitePostal, string DateEcheance)
       {
           try
           {
               DAL = (Facturation)DataProviderObject.GetStringArchive;
               DAL.ARCHIVE_CLIENT_ADD(IdClient, IdLangue, Idporata, IdSite, IdExonere, IdCompte, IdDeviseFact, IdTerme, NumeroImatriculation,
                   TermeDescription, TermeNombre, Rue1, Rue2, NomClient, NumeroContribuable, Ville, BoitePostal, DateEcheance);
               return true;
           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }
       #endregion


       #region MyRegion

       static Taxe converTo(TaxeModel taxe)
       {
           Taxe newDevise = null;
           if (taxe != null)
               newDevise = new Taxe { ID_Taxe = taxe.ID_Taxe, Libelle = taxe.Libelle, Taux = taxe.Taux, IdSite = taxe.IdSite };
           return newDevise;
       }

       static ExploitationFacture ConvertTo(ExploitationFactureModel fact)
       {
           ExploitationFacture newFact = null;
           if (fact != null)
               newFact = new ExploitationFacture
               {
                   IdExploitation = fact.IdExploitation,
                   IdLangue = fact.IdLangue,
                   Libelle = fact.Libelle,
                   IdClient = fact.IdClient,
                   IdSite = fact.IdSite
               };
           return newFact;

       }
       #endregion

    }
}
