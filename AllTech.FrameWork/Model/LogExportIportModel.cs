using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL;
using System.Data;
using System.Collections.ObjectModel;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
   public  class LogExportIportModel
    {
       static IFacturation DAL = null;

     

       public static DataTable Extraction_Select(int idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.Extraction_Select(idSite);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static bool Extraction_DELETE(int id, int idSite,string fichier)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DAL.EXtractionFile_DELETE(id, idSite, fichier);
               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public static bool Extraction_ADD(int id, DateTime dateExtraction, DateTime dateImport, DateTime dateValide, string periode, int idSite,
           int idSiteMaj, string statut, int ordreStatut, string nomFichier)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {

               DAL.EXtractionFile_ADD( id,  dateExtraction, dateImport, dateValide,  periode,  idSite,
            idSiteMaj, statut, ordreStatut,nomFichier);

               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public static DataTable GetLogDataExport_Select(int idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.LogDatas_Export_Select(idSite);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static DataTable GetLogDataImport_Select(int idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DataTable table = DAL.LogDatas_Import_Select(idSite);
               return table;
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }

       }

       public static bool SetExport_DELETE(int id, int idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DAL.LogDataExport_DELETE(id, idSite);
               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public static DataSet Get_Export_ListeFacture(DateTime dateDebut, DateTime dateFin, Int32 idSite)
       {
           ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
           FactureModel facture = null;
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               return DAL.Get_Export_ListeFacture(dateDebut, dateFin,idSite);
               //if (nfactures.Count > 0)
               //{
               //    foreach (var f in nfactures)
               //    {
               //        facture = new FactureModel();
               //        facture = Converfrom(f);

               //        if (f.CurrentClient != null)
               //            facture.CurrentClient = ConvertFromClient(f.CurrentClient);
               //        else facture.CurrentClient = new ClientModel();
               //        if (f.CurrentClient.Ccompte != null)
               //            facture.CurrentClient.Compte = ConvertfromCmpt(f.CurrentClient.Ccompte);
               //        else facture.CurrentClient.Compte = new CompteModel();
               //        if (f.CurrentClient.Exonerate != null)
               //            facture.CurrentClient.Exonerere = ConvertfromExo(f.CurrentClient.Exonerate);
               //        else facture.CurrentClient.Exonerere = new ExonerationModel();
               //        if (f.CurrentClient.Devise!=null )
               //        facture.CurrentClient.Devise = Convertfromdevise(f.CurrentClient.Devise);
               //        if (f.CurrentClient.Porata != null)
               //        facture.CurrentClient.Porata = ConvertfromProrata(f.CurrentClient.Porata);
               //        if (f.CurrentClient.LibelleTerme != null)
               //        facture.CurrentClient.LibelleTerme = convertfromLibelle(f.CurrentClient.LibelleTerme);
               //        if (f.CurrentClient.Llangue != null)
               //        facture.CurrentClient.Llangue  = convertFromLangue(f.CurrentClient.Llangue);
               //        if (f.CurrentDevise != null)
               //        facture.CurrentDevise = ConvertfromDevise(f.CurrentDevise);
               //        if (f.CurrentExploitationFacture != null)
               //        facture.CurrentExploitation = Converfromexploit(f.CurrentExploitationFacture);
               //        if (f.CurrentModePaiement != null)
               //        facture.CurrentModePaiement = ConverfromModeP(f.CurrentModePaiement);

               //        if (f.CurrentObjetFacture != null)
               //        {
               //            facture.CurrentObjetFacture = ConverfromObjet(f.CurrentObjetFacture);
               //            if (f.CurrentObjetFacture.ObjetGeneric != null)
               //                facture.CurrentObjetFacture.ObjetGeneric = new ObjetGenericModel
               //                {
               //                    IdObjetg = f.CurrentObjetFacture.ObjetGeneric.IdObjetg,
               //                    IdSite = f.CurrentObjetFacture.ObjetGeneric.IdSite,
               //                    IdLangue = f.CurrentObjetFacture.ObjetGeneric.IdLangue,
               //                    Libelle = f.CurrentObjetFacture.ObjetGeneric.Libelle
               //                };
               //            else facture.CurrentObjetFacture.ObjetGeneric = new ObjetGenericModel();

               //        }
               //        else facture.CurrentObjetFacture = new ObjetFactureModel();

                      
               //        if (f.CurrentStatut !=null )
               //        facture.CurrentStatut = ConverfromStatut(f.CurrentStatut);
               //        if (f.CurrentTaxe !=null )
               //        facture.CurrentTaxe = ConvertfromTaxe(f.CurrentTaxe);
               //        if (f.UserCreate != null)
               //        facture.UserCreate = ConverfromUser(f.UserCreate);
               //        ProfileModel prof = new ProfileModel
               //        {
               //            IdProfile = f.UserCreate.Profile .idProfile ,
               //            Libelle = f.UserCreate.Profile .Libelle ,
               //            ShortName = f.UserCreate.Profile .ShortName ,

               //            //Droit = droitselected.GetListdroit(p.idProfile)
               //        };
               //        facture.UserCreate.Profile = prof;
               //        if (f.CurrentDepartement !=null )
               //        facture.CurrentDepartement = ConvertFromddept(f.CurrentDepartement);
                       
               //        factures.Add(facture);

               //    }
               //}
           }
           catch (Exception ex)
           {
               throw new Exception(ex.Message);
           }
          
       }


       public static  bool SetImport_ADD(FactureModel facture)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               if (facture != null)
                   DAL.SetImport_Facture_UPDATE(ConvertTo(facture));

               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public static bool SetImport_DELETE(int id,int idSite)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {
               DAL.LogDataIMPORT_DELETE(id, idSite);

               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       /// <summary>
       /// fonction de log du  fichier généré pour export
       /// </summary>
       /// <param name="periode"></param>
       /// <param name="idSite"></param>
       /// <param name="statut"></param>
       /// <returns></returns>
       public static bool SetExport_ADDLogg(int id,string periode,int idSite,string statut,string nomFichier)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {

               DAL.LogDataExport_ADD(id, periode, idSite, statut, nomFichier);

               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public static bool SetImport_ADDLogg(int id,string periode, int idSite, string statut,string nomfichier)
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           try
           {

               DAL.LogDataImport_ADD(id, periode, idSite, statut, nomfichier);

               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


     



    #region BUISNESS METHOD

      static  LangueModel convertFromLangue(Langue langue)
       {
           LangueModel l = null;
           if (langue != null)
           {
               l = new LangueModel { Id = langue.IdLangue, Libelle = langue.Libelle, Shortname = langue.Shorname };
           }
           return l;
       }

      static  LibelleTermeModel convertfromLibelle(Libelle_Terme libelle)
       {
           LibelleTermeModel newdevise = null;
           if (libelle != null)
               newdevise = new LibelleTermeModel
               {
                   ID = libelle.ID,
                   Desciption = libelle.Desciption,
                   CourtDescription = libelle.CourtDesc,
                   Jour = libelle.Jour
               };
           return newdevise;
       }

      static  TaxeModel ConvertfromProrata(Taxe taxe)
       {
           TaxeModel newdevise = null;
           if (taxe != null)
               newdevise = new TaxeModel { ID_Taxe = taxe.ID_Taxe, Libelle = taxe.Libelle, Taux = taxe.Taux, IdSite =taxe .IdSite  };
           return newdevise;

       }

      static  DeviseModel Convertfromdevise(Devise devise)
       {
           DeviseModel newdevise = null;
           if (devise != null)
               newdevise = new DeviseModel { ID_Devise = devise.ID_Devise, Libelle = devise.Libelle, Taux = devise.Taux, IdSite =devise .IdSite  };
           return newdevise;

       }

      static  ExonerationModel ConvertfromExo(Exoneration exo)
       {
           ExonerationModel newExo = null;
           if (exo != null)
               newExo = new ExonerationModel { ID = exo.ID, Libelle = exo.Libelle, CourtDesc = exo.ShortName };
           return newExo;

       }

      static  CompteModel ConvertfromCmpt(Compte compte)
       {
           CompteModel comptes = null;
           if (compte != null)
               comptes = new CompteModel
               {
                   ID = compte.ID,
                   IDSite = compte.IDSite,
                   NumeroCompte = compte.NumeroCompte,
                   NomBanque = compte.NomBanque,
                   Ville = compte.Ville,
                   Agence = compte.Agence,
                   BoitePostal = compte.BoitePostal,
                   Pays = compte.Pays,
                   Rue = compte.Rue,
                   Telephone = compte.Telephone,
                   Quartier = compte.Quartier
               };
           return comptes;

       }

      static  DepartementModel ConvertFromddept(Departement dep)
       {
           DepartementModel newdep = null;
           if (dep != null)
           {
               newdep = new DepartementModel { IdDep = dep.IdDepartement, Libelle = dep.Libelle, CourtLibelle = dep.CourtLibelle, Autre = dep.Autre, IdSite =dep .IdSite  };
           }
           return newdep;
       }

      static  ClientModel ConvertFromClient(Client client)
       {
           ClientModel newClient = null;
           if (client != null)
               newClient = new ClientModel
               {
                   IdClient = client.IdClient,
                   IdLangue = client.IdLangue,
                   NomClient = client.NomClient,
                   NumeroContribuable = client.NumeroContribuable,
                   Rue1 = client.Rue1,
                   Rue2 = client.Rue2,
                   Ville = client.Ville,
                   DateEcheance = client.DateEcheance,
                   Idporata = client.Idporata,
                   TermeNombre = client.TermeNombre,
                   TermeDescription = client.TermeDescription,
                   BoitePostal = client.BoitePostal,
                   IdCompte = client.IdCompte,
                   IdSite = client.IdSite,
                   IdExonere = client.IdExonere,
                   NumemroImat = client.NumeroImatriculation,
                   IdTerme = client.IdTerme,
                   IdDeviseFact = client.IdDeviseFact


               };
           return newClient;

       }

      static  ModePaiementModel ConverfromModeP(ModePaiement mode)
       {
           ModePaiementModel newFact = null;
           if (mode != null)
               newFact = new ModePaiementModel
               {
                   IdMode = mode.IdMode,
                   IdLangue = mode.IdLangue,
                   Libelle = mode.Libelle,


               };
           return newFact;

       }

      static  ExploitationFactureModel Converfromexploit(ExploitationFacture efacture)
       {
           ExploitationFactureModel newFact = null;
           if (efacture != null)
               newFact = new ExploitationFactureModel
               {
                   IdExploitation = efacture.IdExploitation,
                   IdLangue = efacture.IdLangue,
                   Libelle = efacture.Libelle,
                   IdSite = efacture.IdSite,
                   IdClient = efacture.IdClient




               };
           return newFact;

       }
     static   DeviseModel ConvertfromDevise(Devise devise)
       {
           DeviseModel newdevise = null;
           if (devise != null)
               newdevise = new DeviseModel { ID_Devise = devise.ID_Devise, Libelle = devise.Libelle, Taux = devise.Taux,IdSite =devise .IdSite  };
           return newdevise;

       }

      static  ObjetFactureModel ConverfromObjet(ObjetFacture efacture)
       {
           ObjetFactureModel newFact = null;
           if (efacture != null)
               newFact = new ObjetFactureModel
               {
                   IdObjet = efacture.IdObjet,
                    IdobjetGen =efacture.IdobjetGen  ,
                   Libelle = efacture.Libelle,
                   IdClient=efacture .IdClient 

               };
           return newFact;

       }

      static  StatutModel ConverfromStatut(StatutFacture efacture)
       {
           StatutModel newFact = null;
           if (efacture != null)
               newFact = new StatutModel
               {
                   IdStatut = efacture.IdStatut,
                   IdLangue = efacture.IdLangue,
                   Libelle = efacture.Libelle,
                   CourtDesc = efacture.ShortName


               };
           return newFact;

       }
      static  TaxeModel ConvertfromTaxe(Taxe taxe)
       {
           TaxeModel newdevise = null;
           if (taxe != null)
               newdevise = new TaxeModel { ID_Taxe = taxe.ID_Taxe, Libelle = taxe.Libelle, Taux = taxe.Taux, IdSite =taxe .IdSite  };
           return newdevise;

       }
      static  UtilisateurModel ConverfromUser(Utilisateur user)
       {
           UtilisateurModel newUser = null;
           if (user != null)
               newUser = new UtilisateurModel
               {
                   Id = user.IdUtilisateur,
                   Nom = user.Nom,
                   Prenom = user.Prenom,
                   Fonction = user.Fonction,
                   Loggin = user.Loggin,
                   IdProfile = user.IdProfil,
                   Password = user.Password, IdSite =user .IdSite 
               };
           return newUser;

       }
       static  Facture ConvertTo(FactureModel fact)
       {
           Facture newFact = null;
           if (fact != null)
               newFact = new Facture
               {
                   IdFacture = fact.IdFacture,
                   IdCreerpar = fact.IdCreerpar,

                   IdModifierPar = fact.IdModifierPar,
                   IdDevise = fact.IdDevise,
                   IdModePaiement = fact.IdModePaiement,
                   IdObjetFacture = fact.IdObjetFacture,
                   IdStatut = fact.IdStatut,
                   IdTaxe = fact.IdTaxe,
                   MoisPrestation = fact.MoisPrestation,
                   DateCloture = fact.DateCloture,
                   IdClient = fact.IdClient,
                   NumeroFacture = fact.NumeroFacture,
                   DateEcheance = fact.DateEcheance != null ? (DateTime)fact.DateEcheance : DateTime.Now,
                   DateSortie = fact.DateSortie,
                   DateSuspension = fact.DateSuspension,
                   IdSite = fact.IdSite,
                   IsProrata = fact.IsProrata,
                   JourFinEcheance = fact.JourFinEcheance,
                   DateDepot = fact.DateDepot,
                   Idexploitation = fact.IdExploitation,
                   factureValide = fact.isfactureValide,
                   LibelleObjet = fact.Label_objet,
                   Libelle_Dep = fact.Label_Dep,
                   IdDepartement = fact.IdDepartement,
                   TotalTTC = fact.TotalTTC,
                   DateNonValide = fact.DateNonValable

               };
           return newFact;

       }

      static  FactureModel Converfrom(Facture facture)
       {
           FactureModel newFact = null;
           if (facture != null)
               newFact = new FactureModel
               {
                   IdFacture = facture.IdFacture,
                   IdCreerpar = facture.IdCreerpar,

                   IdModifierPar = facture.IdModifierPar,
                   IdDevise = facture.IdDevise,
                   DateCreation = facture.DateCreation,
                   IdModePaiement = facture.IdModePaiement,
                   IdObjetFacture = facture.IdObjetFacture,
                   IdStatut = facture.IdStatut,
                   IdTaxe = facture.IdTaxe,
                   MoisPrestation = facture.MoisPrestation,
                   DateCloture = facture.DateCloture,
                   IdClient = facture.IdClient,
                   NumeroFacture = facture.NumeroFacture,
                   DateEcheance = facture.DateEcheance,
                   DateSortie = facture.DateSortie,
                   DateSuspension = facture.DateSuspension,
                   IdSite = facture.IdSite,
                   IsProrata = facture.IsProrata,
                   JourFinEcheance = facture.JourFinEcheance,
                   DateDepot = facture.DateDepot,
                   IdExploitation = facture.Idexploitation,
                   isfactureValide = facture.factureValide,
                   Label_objet = facture.LibelleObjet,
                   Label_Dep = facture.Libelle_Dep,
                   IdDepartement = (int)facture.IdDepartement,
                   TotalTTC = facture.TotalTTC,
                   DateNonValable = facture.DateNonValide,
                   DateModif = facture.DateModification

               };
           return newFact;

       }
        #endregion

    }
}
