using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;
using AllTech.FrameWork.Global;
using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Model
{
    public class ObjetFactureModel : ViewModelBase
    {

        public int IdObjet { get; set; }
        private int idobjetGen;
        private  string libelle;
        public Int32  IdClient { get; set; }
        private ObjetGenericModel objetGeneric;
        private ClientModel client;
        private Boolean isobjectselect;
        private Boolean isNewObject;

        Facturation DAL = null;

        public ObjetFactureModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            IdObjet = 0;
            
        }


        #region PROPERTIES

        public Boolean IsNewObject
        {
            get { return isNewObject; }
            set { isNewObject = value;
            this.OnPropertyChanged("IsNewObject");
            }
        }

        public Boolean Isobjectselect
        {
            get { return isobjectselect; }
            set { isobjectselect = value;
            this.OnPropertyChanged("Isobjectselect");
            }
        }

        public ObjetGenericModel ObjetGeneric
        {
            get { return objetGeneric; }
            set { objetGeneric = value;
            this.OnPropertyChanged("ObjetGeneric");
            }
        }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value;
            this.OnPropertyChanged("Libelle");
            }
        }

     
        public ClientModel Client
        {
            get { return client; }
            set
            {
                client = value;
                this.OnPropertyChanged("Client");
            }
        }

        public int IdobjetGen
        {
            get { return idobjetGen; }
            set { idobjetGen = value;
            this.OnPropertyChanged("IdobjetGen");
            }
        }
       

        #endregion

        #region METHODS

        public ObservableCollection<ObjetFactureModel> OBJECT_FACTURE_GETLISTE(Int32 idSite)
        {
            ObservableCollection<ObjetFactureModel> factures = new ObservableCollection<ObjetFactureModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetFacture> obj = DAL.GetAll_OBJET_FACTURE (idSite);
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {
                       

                        ObjetFactureModel fmodel = Converfrom(exp);
                        fmodel.Client = ConvertFromClient(exp.Client);
                        factures.Add(fmodel);

                    }
                }
                factures.Sort(ob => ob.Libelle);
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<ObjetFactureModel> OBJECT_FACTURE_GETLISTE_Archive(Int32 idSite)
        {
            ObservableCollection<ObjetFactureModel> factures = new ObservableCollection<ObjetFactureModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetFacture> obj = DAL.GetAll_OBJET_FACTURE(idSite);
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {


                        ObjetFactureModel fmodel = Converfrom(exp);
                        fmodel.Client = ConvertFromClient(exp.Client);
                        factures.Add(fmodel);

                    }
                }
                factures.Sort(ob => ob.Libelle);
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public ObservableCollection<ObjetFactureModel> OBJECT_FACTURE_GETLISTEByIdLanguage(int idLanguage,Int32 idSite)
        {
            ObservableCollection<ObjetFactureModel> factures = new ObservableCollection<ObjetFactureModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetFacture> exploits = DAL.GetAll_OBJET_FACTUREBYLangue (idLanguage,idSite );
                if (exploits.Count > 0)
                {
                    ObjetFactureModel firstObjet = new ObjetFactureModel { IdObjet =0, Libelle="..." };
                    firstObjet.ObjetGeneric = convertToObjetGeneric(new ObjetGenerique { IdObjetg = 0, Libelle = "......." });
                    factures.Add(firstObjet);
                    foreach (var exp in exploits)
                    {
                        ObjetFactureModel fmodel = Converfrom(exp);
                        fmodel.Client = ConvertFromClient(exp.Client);
                        fmodel.ObjetGeneric = convertToObjetGeneric(exp.ObjetGeneric);
                        factures.Add(fmodel);

                    }
                }
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<ObjetFactureModel> OBJECT_FACTURE_GETLISTEByIdLanguage_Archive(int idLanguage, Int32 idSite)
        {
            ObservableCollection<ObjetFactureModel> factures = new ObservableCollection<ObjetFactureModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetFacture> exploits = DAL.GetAll_OBJET_Archive_FACTUREBYLangue(idLanguage, idSite);
                if (exploits.Count > 0)
                {
                    ObjetFactureModel firstObjet = new ObjetFactureModel { IdObjet = 0, Libelle = "..." };
                    firstObjet.ObjetGeneric = convertToObjetGeneric(new ObjetGenerique { IdObjetg = 0, Libelle = "......." });
                    factures.Add(firstObjet);
                    foreach (var exp in exploits)
                    {
                        ObjetFactureModel fmodel = Converfrom(exp);
                        fmodel.Client = ConvertFromClient(exp.Client);
                        fmodel.ObjetGeneric = convertToObjetGeneric(exp.ObjetGeneric);
                        factures.Add(fmodel);

                    }
                }
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

      

        public ObservableCollection<ObjetFactureModel> OBJECT_FACTURE_GETLISTEByCLIENT( Int32 idSite,Int32 idClient)
        {
            ObservableCollection<ObjetFactureModel> factures = new ObservableCollection<ObjetFactureModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetFacture> exploits = DAL.GetAll_OBJET_FACTUREBYCLIENT(idSite, idClient);
                ObjetFactureModel firstObjet = new ObjetFactureModel { IdObjet =0, Libelle = "......." };

                if (exploits.Count > 0)
                {
                    firstObjet.ObjetGeneric = convertToObjetGeneric(new ObjetGenerique { IdObjetg=0, Libelle ="......." });
                    factures.Add(firstObjet);
                    foreach (var exp in exploits)
                    {
                        ObjetFactureModel fmodel = Converfrom(exp);
                        fmodel.Client = ConvertFromClient(exp.Client);
                        fmodel.ObjetGeneric = convertToObjetGeneric(exp.ObjetGeneric);
                        factures.Add(fmodel);

                    }
                }else
                    factures.Add(firstObjet);

                factures.Sort(ob => ob.Libelle);

                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public ObservableCollection<ObjetFactureModel> OBJECT_FACTURE_GETLISTEByCLIENT_Archive(Int32 idSite, Int32 idClient)
        {
            ObservableCollection<ObjetFactureModel> factures = new ObservableCollection<ObjetFactureModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetFacture> exploits = DAL.GetAll_OBJET_Archive_FACTUREBYCLIENT(idSite, idClient);
                ObjetFactureModel firstObjet = new ObjetFactureModel { IdObjet = 0, Libelle = "......." };

                if (exploits.Count > 0)
                {
                    firstObjet.ObjetGeneric = convertToObjetGeneric(new ObjetGenerique { IdObjetg = 0, Libelle = "......." });
                    factures.Add(firstObjet);
                    foreach (var exp in exploits)
                    {
                        ObjetFactureModel fmodel = Converfrom(exp);
                        fmodel.Client = ConvertFromClient(exp.Client);
                        fmodel.ObjetGeneric = convertToObjetGeneric(exp.ObjetGeneric);
                        factures.Add(fmodel);

                    }
                }
                else
                    factures.Add(firstObjet);

                factures.Sort(ob => ob.Libelle);

                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }




        public ObjetFactureModel OBJECT_FACTURE_GETLISTEByID(int id, Int32 idSite)
        {
            ObjetFactureModel facture = null ;
            LangueModel llangue = new LangueModel();

            try
            {
                ObjetFacture exp = DAL.GetAll_OBJET_FACTUREBYID(id,idSite );

                if (exp != null)
                {
                    facture = Converfrom(exp);
                    facture.Client = ConvertFromClient(exp.Client);
                    facture.ObjetGeneric = convertToObjetGeneric(exp.ObjetGeneric);
                    
                }

                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public ObjetFactureModel OBJECT_FACTURE_GETLISTEByID_Archive(int id, Int32 idSite)
        {
            ObjetFactureModel facture = null;
            LangueModel llangue = new LangueModel();

            try
            {
                ObjetFacture exp = DAL.GetAll_OBJET_Archive_FACTUREBYID(id, idSite);

                if (exp != null)
                {
                    facture = Converfrom(exp);
                    facture.Client = ConvertFromClient(exp.Client);
                    facture.ObjetGeneric = convertToObjetGeneric(exp.ObjetGeneric);

                }

                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool OBJECT_FACTURE_ADD(ObjetFactureModel facture,Int32 idSite)
        {

            try
            {
                if (facture !=null )
                 DAL.OBJET_FACTURE_ADD (ConvertTo (facture),idSite );

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

      


        public int  OBJECT_FACTURE_DELETE(int  id)
        {

            try
            {
               return  DAL.OBJET_FACTURE_DELETE(id);
            }
            catch (Exception de)
            {
                throw new DALException (de.Message);
            }
        }

        #endregion

        #region BUSNESS METHOD
        ObjetGenericModel convertTo(ObjetGenerique obj)
        {
            ObjetGenericModel newobjet = new ObjetGenericModel
            {
                IdObjetg = obj.IdObjetg,
                IdLangue = obj.IdLangue,
                IdSite = obj.IdSite,
                Libelle = obj.Libelle,
                Compte = obj.Compte
            };
            return newobjet;
        }

        ObjetGenericModel convertToObjetGeneric(ObjetGenerique obj)
        {
            ObjetGenericModel newobjet = new ObjetGenericModel
            {
                IdObjetg = obj.IdObjetg,
                IdLangue = obj.IdLangue,
                IdSite = obj.IdSite,
                Libelle = obj.Libelle
            };
            return newobjet;
        }

        ObjetFactureModel Converfrom(ObjetFacture efacture)
        {
            ObjetFactureModel newFact = null;
            if (efacture != null)
                newFact = new ObjetFactureModel
                {
                     IdObjet  = efacture.IdObjet ,
                    Libelle = efacture.Libelle,
                     IdClient = efacture.IdClient ,
                     IdobjetGen =efacture .IdobjetGen , 
                     Isobjectselect =false , 
                     IsNewObject =false 


                };
            return newFact;

        }

        ObjetFacture ConvertTo(ObjetFactureModel fact)
        {
            ObjetFacture newFact = null;
            if (fact != null)
                newFact = new ObjetFacture
                {
                     IdObjet = fact.IdObjet ,
                     Libelle = fact.Libelle, 
                     IdClient = fact.IdClient , 
                     IdobjetGen =fact .IdobjetGen 
                };
            return newFact;

        }

        ClientModel ConvertFromClient(Client client)
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
                    TermeDescription = client.TermeDescription,
                    BoitePostal = client.BoitePostal,
                    IdSite = client.IdSite,
                    IdCompte = client.IdCompte,
                    IdExonere = client.IdExonere,
                    NumemroImat = client.NumeroImatriculation,

                    TermeNombre = client.TermeNombre
                };
            return newClient;

        }
        #endregion
    }
}
