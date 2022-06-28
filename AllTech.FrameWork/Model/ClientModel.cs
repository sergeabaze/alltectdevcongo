using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Global;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;
using System.Collections.ObjectModel;
using System.Data;

namespace AllTech.FrameWork.Model
{
   public  class ClientModel: ViewModelBase
    {
        #region Fields
        
      

        public int IdClient { get; set; }
        private string codeClient;
        private string nomClient;
        private string numeroContribuable;
        private string ville;
        private string rue1;
        private string rue2;
        private int idLangue;
        private Int32 termeNombre;
        private string termeDescription;
        private int idporata;
        private string dateEcheance;
        private string boitePostal;
        private int idExonere;
        private int idCompte;
        private int idSite;
        private int idDeviseFact;
        private int idDeviseConversion;
        private int idTerme;
        private int? idCompteTiers;
        private string numemroImat;
        private bool isActive;
        private string compteproduit;

     
        private LangueModel  _llangue;
        private TaxeModel porata;
        private CompteModel compte;
        private ExonerationModel exonerere;
        private LibelleTermeModel libelleTerme;
        private List<CompteAnalClientModel> compteAnalytique;
        private DeviseModel deviseConversion;
        private DeviseModel deviseFacture;

      
        private CompteTiersModel compteTiers;
        private string nomClientCompta;

        // public bool IsDeviseFacture { get; set; }
        

        Facturation DAL = null;
        #endregion

        public ClientModel()
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           IdClient = 0;
           nomClient = string.Empty;
           numeroContribuable = string.Empty;
           ville = string.Empty;
           rue1 = string.Empty;
           rue2 = string.Empty;
           termeNombre = 0;
           termeDescription = string.Empty;
          
       }

      

        #region PROPERTIES

        public string Compteproduit
        {
            get { return compteproduit; }
            set { compteproduit = value;
            this.OnPropertyChanged("Compteproduit");
            }
        }

        public DeviseModel DeviseFacture
        {
            get { return deviseFacture; }
            set { deviseFacture = value;
            this.OnPropertyChanged("IdDeviseConversion");
            }
        }

        public DeviseModel DeviseConversion
        {
            get { return deviseConversion; }
            set { deviseConversion = value;
            this.OnPropertyChanged("IdDeviseConversion");
            }
        }


        public int IdDeviseConversion
        {
            get { return idDeviseConversion; }
            set { idDeviseConversion = value;

            this.OnPropertyChanged("IdDeviseConversion");
            }
        }

        public string CodeClient
        {
            get { return codeClient; }
            set { codeClient = value; 
             this.OnPropertyChanged("CodeClient");
            }
            
        }

        public string NomClientCompta
        {
            get { return nomClientCompta; }
            set { nomClientCompta = value;
            this.OnPropertyChanged("IsActive");
            }
        }

       public bool IsActive
       {
           get { return isActive; }
           set { isActive = value;
           this.OnPropertyChanged("IsActive");
           }
       }
       public CompteTiersModel CompteTiers
       {
           get { return compteTiers; }
           set { compteTiers = value;
           this.OnPropertyChanged("CompteTiers");
           }
       }

       public List<CompteAnalClientModel> CompteAnalytique
       {
           get { return compteAnalytique; }
           set { compteAnalytique = value;
           this.OnPropertyChanged("CompteAnalytique");
           }
       }


       public int? IdCompteTiers
       {
           get { return idCompteTiers; }
           set
           {
               idCompteTiers = value;
               this.OnPropertyChanged("IdCompteTiers");
           }
       }
      

       public LibelleTermeModel LibelleTerme
       {
           get { return libelleTerme; }
           set { libelleTerme = value;
           this.OnPropertyChanged("LibelleTerme");
           }
       }

       public int IdTerme
       {
           get { return idTerme; }
           set { idTerme = value;
           this.OnPropertyChanged("IdTerme");
           }
       }

       public int IdExonere
       {
           get { return idExonere; }
           set { idExonere = value;
           this.OnPropertyChanged("IdExonere");
           }
       }
       public int IdCompte
       {
           get { return idCompte; }
           set { idCompte = value;
           this.OnPropertyChanged("IdCompte");
           }
       }

       public int IdSite
       {
           get { return idSite; }
           set { idSite = value;
           this.OnPropertyChanged("IdSite");
           }
       }

       public string NumemroImat
       {
           get { return numemroImat; }
           set { numemroImat = value;
           this.OnPropertyChanged("NumemroImat");
           }
       }
       public CompteModel Compte
       {
           get { return compte; }
           set { compte = value;
           this.OnPropertyChanged("Compte");
           }
       }


       public ExonerationModel Exonerere
       {
           get { return exonerere; }
           set { exonerere = value;
           this.OnPropertyChanged("Exonerere");
           }
       }

       public TaxeModel Porata
       {
           get { return porata; }
           set { porata = value; 
             this.OnPropertyChanged("Porata");
           }

       }

      

       public string BoitePostal
       {
           get { return boitePostal; }
           set { boitePostal = value; 
             this.OnPropertyChanged("BoitePostal");
           }
       }

       public LangueModel Llangue
       {
           get { return _llangue; }
           set
           {
               _llangue = value;
               this.OnPropertyChanged("Llangue");
           }
       }

       public string NomClient
       {
           get { return nomClient; }
           set { nomClient = value;
           this.OnPropertyChanged("NomClient");
           }
       }
     

       public string NumeroContribuable
       {
           get { return numeroContribuable; }
           set { numeroContribuable = value;
           this.OnPropertyChanged("NumeroContribuable");
           }
       }
      

       public string Ville
       {
           get { return ville; }
           set { ville = value;
           this.OnPropertyChanged("Ville");
           }
       }
     

       public string Rue1
       {
           get { return rue1; }
           set { rue1 = value;
           this.OnPropertyChanged("Rue1");
           }
       }
      

       public string Rue2
       {
           get { return rue2; }
           set { rue2 = value;
           this.OnPropertyChanged("Rue2");
           }
       }
     

       public int IdLangue
       {
           get { return idLangue; }
           set { idLangue = value;
           this.OnPropertyChanged("IdLangue");
           }
       }
       

     

       public Int32 TermeNombre
       {
           get { return termeNombre; }
           set
           {
               termeNombre = value;
               this.OnPropertyChanged("TermeNombre");
           }
       }

       public string  TermeDescription
       {
           get { return termeDescription; }
           set
           {
               termeDescription = value;
               this.OnPropertyChanged("TermeDescription");
           }
       }

       public int  Idporata
       {
           get { return idporata; }
           set
           {
               idporata = value;
               this.OnPropertyChanged("Idporata");
           }
       }

       public string DateEcheance
       {
           get { return dateEcheance; }
           set
           {
               dateEcheance = value;
               this.OnPropertyChanged("DateEcheance");
           }
       }

       public int IdDeviseFact
       {
           get { return idDeviseFact; }
           set { idDeviseFact = value;
           this.OnPropertyChanged("IdDeviseFact");
           }
       }
        #endregion


        #region METHODS


       public ObservableCollection<ClientModel> CLIENT_MONTHLY_CREATE(Int32 idsite)
       {
           ObservableCollection<ClientModel> mclients = new ObservableCollection<ClientModel>();
         
           try
           {
               List<Client> clients = DAL.GetALL_CLIENT_NOT_MONTHLYCREATE(idsite);
               if (clients.Count > 0)
               {
                   foreach (var cli in clients)
                   {
                       //LangueModel newl = llangue.LANGUE_SELECTBYID(cli.IdLangue);

                       ClientModel newclient = ConvertFrom(cli);
                       mclients.Add(newclient);
                   }
               }

              
               return mclients;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public ObservableCollection<ClientModel> CLIENT_GETLISTEBY(Int32 idsite,string nomClient=null , string ville=null )
       {
           ObservableCollection<ClientModel> mclients = new ObservableCollection<ClientModel>();
           LangueModel llangue = new LangueModel();
           TaxeModel taxeSelected = new TaxeModel();
           CompteModel model = new CompteModel();

           try
           {
               List<Client> clients = DAL.GetALL_CLIENTBY(idsite, nomClient, ville);
               if (clients.Count > 0)
               {
                   foreach (var cli in clients)
                   {
                       //LangueModel newl = llangue.LANGUE_SELECTBYID(cli.IdLangue);

                       ClientModel newclient = ConvertFrom(cli);
                       newclient.Llangue = convertFromLangue(cli.Llangue);
                       newclient.Porata = ConvertfromTaxe(cli.Porata);// taxeSelected.Taxe_SELECTById(cli.Idporata);
                       newclient.Compte = ConvertfromCompte(cli.Ccompte);
                       newclient.Exonerere = ConvertfromExonere(cli.Exonerate);
                       newclient.DeviseConversion = ConvertfromDevise(cli.DeviseConversion);
                       newclient.DeviseFacture = ConvertfromDevise(cli.DeviseFacture);


                       mclients.Add(newclient);
                   }
               }

               // mclients.Sort(n => n.NomClient);
               return mclients;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public ObservableCollection<ClientModel> CLIENT_GETLISTE(Int32 idsite,bool isActive)
       {
           ObservableCollection<ClientModel> mclients = new ObservableCollection<ClientModel>();
           LangueModel llangue=new LangueModel ();
           TaxeModel taxeSelected = new TaxeModel();
           CompteModel model=new CompteModel ();
          
           try
           {
               List<Client> clients = DAL.GetALL_CLIENT(idsite, isActive);
               if (clients.Count > 0)
               {
                  
                   foreach (var cli in clients)
                   {
                       //LangueModel newl = llangue.LANGUE_SELECTBYID(cli.IdLangue);

                     
                       ClientModel newclient = ConvertFrom(cli);
                       newclient.Llangue = convertFromLangue(cli.Llangue );
                       newclient.Porata = ConvertfromTaxe(cli.Porata);// taxeSelected.Taxe_SELECTById(cli.Idporata);
                       newclient.Compte = ConvertfromCompte(cli.Ccompte);
                       newclient.Exonerere = ConvertfromExonere(cli.Exonerate);
                       newclient.DeviseConversion = ConvertfromDevise(cli.DeviseConversion);
                       newclient.DeviseFacture = ConvertfromDevise(cli.DeviseFacture);

                    
                      
                       mclients.Add(newclient);
                   }
               }

              // mclients.Sort(n => n.NomClient);
               return mclients;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public ObservableCollection<ClientModel> CLIENT_Archive_GETLISTE(Int32 idsite, bool isActive)
       {
           ObservableCollection<ClientModel> mclients = new ObservableCollection<ClientModel>();
           LangueModel llangue = new LangueModel();
           TaxeModel taxeSelected = new TaxeModel();
           CompteModel model = new CompteModel();

           try
           {
               List<Client> clients = DAL.GetALL_CLIENT_ARCHIVES(idsite, isActive);
               if (clients.Count > 0)
               {

                   foreach (var cli in clients)
                   {
                       //LangueModel newl = llangue.LANGUE_SELECTBYID(cli.IdLangue);


                       ClientModel newclient = ConvertFrom(cli);
                       newclient.Llangue = convertFromLangue(cli.Llangue);
                       newclient.Porata = ConvertfromTaxe(cli.Porata);// taxeSelected.Taxe_SELECTById(cli.Idporata);
                       newclient.Compte = ConvertfromCompte(cli.Ccompte);
                       newclient.Exonerere = ConvertfromExonere(cli.Exonerate);
                       newclient.DeviseConversion = ConvertfromDevise(cli.DeviseConversion);
                       newclient.DeviseFacture = ConvertfromDevise(cli.DeviseFacture);

                      

                       mclients.Add(newclient);
                   }
               }

               // mclients.Sort(n => n.NomClient);
               return mclients;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       List<CompteAnalClientModel> GetCompteAnalytique(List<CompteAnalClient> comptes)
       {
           List<CompteAnalClientModel> listes = new List<CompteAnalClientModel>();
           CompteAnalClientModel compte = null;
           if (comptes != null && comptes.Count > 0)
           {
               foreach (CompteAnalClient cmpt in comptes)
               {
                   compte = new CompteAnalClientModel();
                   compte.Id = cmpt.Id;
                   compte.IdClient = cmpt.IdClient;
                   compte.CompteAnalyTique = new CompteAnalytiqueModel { IdCompteAnalytique = cmpt.CompteAnalytique.IdCompteAnalytique, Numerocompte = cmpt.CompteAnalytique.Numerocompte  };
                   listes.Add(compte);
               }
           }
           return listes;
       }

       public ObservableCollection<ClientModel> CLIENT_GETLISTE_FACTURER(Int32 idsite,int Mode,int IsArchive,DateTime DateFrom,DateTime DateTo)
       {
           ObservableCollection<ClientModel> mclients = new ObservableCollection<ClientModel>();
           LangueModel llangue = new LangueModel();
           TaxeModel taxeSelected = new TaxeModel();
           CompteModel model = new CompteModel();

           try
           {
               List<Client> clients = DAL.GetALL_CLIENT_FACTURER(idsite, Mode, IsArchive, DateFrom, DateTo);
               if (clients.Count > 0)
               {
                   ClientModel firste = new ClientModel { IdClient = 0, NomClient = "...", IdCompte = 0 };
                   mclients.Add(firste);
                   foreach (var cli in clients)
                   {
                       //LangueModel newl = llangue.LANGUE_SELECTBYID(cli.IdLangue);

                       ClientModel newclient = ConvertFrom(cli);
                       newclient.Llangue = convertFromLangue(cli.Llangue);
                       newclient.Porata = ConvertfromTaxe(cli.Porata);// taxeSelected.Taxe_SELECTById(cli.Idporata);
                       newclient.Compte = ConvertfromCompte(cli.Ccompte);
                       newclient.Exonerere = ConvertfromExonere(cli.Exonerate);
                       newclient.DeviseConversion = ConvertfromDevise(cli.DeviseConversion);
                       newclient.DeviseFacture = ConvertfromDevise(cli.DeviseFacture);
                       mclients.Add(newclient);
                   }
               }

             
               return mclients;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public ClientModel CLIENT_GETLISTE_BYID(int id, Int32 idsite)
       {
           ClientModel clients =null ;
            LangueModel llangue=new LangueModel ();
            TaxeModel taxeSelected = new TaxeModel();
            CompteModel model = new CompteModel();
            ExonerationModel exo = new ExonerationModel();
           try
           {
               Client client = DAL.GetALL_CLIENTByID(id,idsite );
               if (client != null)
               {
                   clients = ConvertFrom(client);
                   clients.Llangue = convertFromLangue(client.Llangue);
                   clients.Porata = clients.Porata = ConvertfromTaxe(client.Porata);// taxeSelected.Taxe_SELECTById(clients.Idporata);
                   clients.Compte = ConvertfromCompte(client.Ccompte);
                   clients.Exonerere = ConvertfromExonere(client.Exonerate);
                   clients.DeviseConversion = ConvertfromDevise(client.DeviseConversion);
                   clients.DeviseFacture = ConvertfromDevise(client.DeviseFacture);
                   
               }

                   return clients;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public ClientModel CLIENT_Archive_GETLISTE_BYID(int id, Int32 idsite, Int64 idfacture)
       {
           ClientModel clients = null;
           LangueModel llangue = new LangueModel();
           TaxeModel taxeSelected = new TaxeModel();
           CompteModel model = new CompteModel();
           ExonerationModel exo = new ExonerationModel();
           try
           {
               Client client = DAL.GetALL_CLIENT_ArchiveByID(id, idsite, idfacture);
               if (client != null)
               {
                   clients = ConvertFrom(client);
                   clients.Llangue = convertFromLangue(client.Llangue);
                   clients.Porata = clients.Porata = ConvertfromTaxe(client.Porata);// taxeSelected.Taxe_SELECTById(clients.Idporata);
                   clients.Compte = ConvertfromCompte(client.Ccompte);
                   clients.Exonerere = ConvertfromExonere(client.Exonerate);
                   clients.DeviseConversion = ConvertfromDevise(client.DeviseConversion);
                   clients.DeviseFacture = ConvertfromDevise(client.DeviseFacture);
                  

               }

               return clients;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public ClientModel CLIENT_GETLISTEByID(long id)
       {
          
           try
           {
              Client  clients = DAL.GetALL_CLIENTByID((int)id);
               return ConvertFrom (clients );

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public ClientModel CLIENT_Archive_GETLISTEByID(long id, Int64 idfacture)
       {

           try
           {
               Client clients = DAL.GetALL_CLIENTByIDArchive((int)id, idfacture);
               return ConvertFrom(clients);

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public DataTable CLIENT_GETByID(int id)
       {

           try
           {
               return DAL.GetCLIENTByID(id, 0);
           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public DataTable CLIENT_Archive_GETByID(int id,Int64 idFacture)
       {

           try
           {
               return DAL.GetCLIENT_Archive_ByID(id, 0, idFacture);
           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public bool CLIENT_ADD(ref Int32 ID,ClientModel client)
       {
           bool succes = false;
           try
           {
               if (client != null)
               {
                   DAL.CLIENT_ADD(ref ID,ConvertTo (client ));

                   succes= true;
               }

           }
           catch (Exception de)
           {
               throw new DALException (de.Message);
           }
           return succes;
       }

       public bool CLIENT_UPDATE_Archices(Int32 IDcLient, Int32 IdComptetiers, string nomClientCompta, Int32 idSite)
       {
           bool succes = false;
           try
           {

               DAL.CLIENT_ADD_Archive( IDcLient,  IdComptetiers,  nomClientCompta,  idSite);
                   succes = true;
               

           }
           catch (Exception de)
           {
               throw new DALException(de.Message);
           }
           return succes;
       }

     


       public bool CLIENT_ACTIF(int  idClient,int idSite,bool isActive)
       {

           bool succes = false;
           try
           {
                   DAL.CLIENT_ACTIF(idClient, idSite, isActive);
                   succes = true;
           }
           catch (Exception de)
           {
               throw new DALException(de.Message);
           }
           return succes;
       }

       public bool CLIENT_DELETE(int id)
       {

           bool succes = false;
           try
           {
               if (id > 0)
               {
                   DAL.CLIENT_DELETE(id);

                   succes = true;
               }

           }
           catch (Exception de)
           {
               throw new DALException(de.Message);
           }
           return succes;
       }

        #endregion

        #region BUSNESS METHOD

       LangueModel convertFromLangue(Langue langue)
       {
           LangueModel l = null;
           if (langue != null)
           {
               l = new LangueModel { Id = langue.IdLangue, Libelle = langue.Libelle, Shortname = langue.Shorname };
           }
           return l;
       }

       CompteModel ConvertfromCompte(Compte compte)
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
                   Telephone = compte.Telephone
               };
           return comptes;

       }

       ExonerationModel ConvertfromExonere(Exoneration exo)
       {
           ExonerationModel newExo = null;
           if (exo != null)
               newExo = new ExonerationModel { ID = exo.ID, Libelle = exo.Libelle , CourtDesc =exo .ShortName };
           return newExo;

       }

       TaxeModel ConvertfromTaxe(Taxe taxe)
       {
           TaxeModel newdevise = null;
           if (taxe != null)
               newdevise = new TaxeModel { ID_Taxe = taxe.ID_Taxe, Libelle = taxe.Libelle, Taux = taxe.Taux, IdSite =taxe .IdSite, TaxeDefault=taxe.Taxedefault  };
           return newdevise;

       }

       DeviseModel ConvertfromDevise(Devise devise)
       {
           DeviseModel newdevise = null;
           if (devise != null)
               newdevise = new DeviseModel { ID_Devise = devise.ID_Devise, Libelle = devise.Libelle, Taux = devise.Taux, IdSite =devise .IdSite, IsDefault=devise.IsDefault  };
           return newdevise;

       }

       ExploitationFactureModel ConverfromExploitation(ExploitationFacture efacture)
       {
           ExploitationFactureModel newFact = null;
           if (efacture != null)
               newFact = new ExploitationFactureModel
               {
                   IdExploitation = efacture.IdExploitation,
                   IdLangue = efacture.IdLangue,
                   Libelle = efacture.Libelle,
                   IdSite = efacture.IdSite


               };
           return newFact;

       }

       ClientModel ConvertFrom(Client client)
       {
           ClientModel newClient=null ;
           if (client != null)
               newClient = new ClientModel { IdClient = client.IdClient, 
                   IdLangue = client .IdLangue,
                    NomClient = client.NomClient.Trim () ,
                    NumeroContribuable = client.NumeroContribuable ,
                    IdCompteTiers = client.IdCompteTiers,
                    Rue1 =client .Rue1 ,
                    Rue2 =client .Rue2 ,
                    Ville =client .Ville, 
                    DateEcheance=client .DateEcheance ,
                    Idporata = client.Idporata, 
                    TermeDescription = client.TermeDescription,
                    BoitePostal = client.BoitePostal,
                    IdSite =client .IdSite, 
                    IdCompte =client .IdCompte,
                    IdExonere =client .IdExonere, 
                    NumemroImat =client .NumeroImatriculation ,   
                    TermeNombre = client.TermeNombre   ,
                     IdDeviseFact =client .IdDeviseFact, 
                     IdTerme =client .IdTerme  , 
                     IsActive=client.IsActive,
                    NomClientCompta = client.NomClientCompta,
                    CodeClient = client.CodeClient,
                    IdDeviseConversion=client.IdConversionFact, 
                    Compteproduit=client.CompteProduit
               };
           return newClient;

       }

       Client ConvertTo(ClientModel client)
       {
           Client newClient = null;
           if (client != null)
               newClient = new Client { IdClient =client .IdClient , 
                NomClient =client .NomClient , 
                Ville =client .Ville , 
                Rue2 =client .Rue2 ,
                Rue1 =client .Rue1 ,
                IdLangue =client .IdLangue ,
                NumeroContribuable =client .NumeroContribuable ,
                 IdCompteTiers = client.IdCompteTiers,
                BoitePostal = client.BoitePostal,
                TermeNombre = client.TermeNombre,
                TermeDescription = client.TermeDescription,
                DateEcheance = client.DateEcheance,
                Idporata = client.Idporata,
                IdSite = client.IdSite,
                IdCompte = client.IdCompte,
                IdExonere = client.IdExonere,
                NumeroImatriculation  = client.NumemroImat ,  
                IdDeviseFact =client .IdDeviseFact , 
                IdTerme =client .IdTerme ,
                NomClientCompta = client.NomClientCompta,
                CodeClient = client.CodeClient,
                IdConversionFact = client.IdDeviseConversion,
                CompteProduit=client.Compteproduit
                
               };
           return newClient;
       }


      
        #endregion
    }

  
}
