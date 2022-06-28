using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using System.Collections.ObjectModel;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class DetailProductModel : ViewModelBase
    {

        public int IdDetail { get; set; }
        private int idClient;
        private int   idProduit;
        private float  quantite;
        private string nomProduit;
        private decimal  prixunitaire;
        private bool exonerer;
        private bool isprorata;
        private string compteOhada;
        private string backGround;
        private int idSite;
        private int idExploitation;
        private bool isCheck;
        private bool specialfact;

        private ProduitModel produit;
        private ClientModel customer = new ClientModel();
        private ExploitationFactureModel exploitation;

     
        Facturation DAL = null;

        public DetailProductModel()
        {
            this.IdDetail = 0;
            this.idClient = 0;
            this.quantite = 0;
            this.prixunitaire = 0;
            exonerer = false;
            isprorata = false;
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region PROPERTIES

        public bool Specialfact
        {
            get { return specialfact; }
            set { specialfact = value;
            this.OnPropertyChanged("Specialfact");
            }
        }

        public bool IsCheck
        {
            get { return isCheck; }
            set { isCheck = value;
            this.OnPropertyChanged("IsCheck");
            }
        }

        public ProduitModel Produit
        {
            get { return produit; }
            set { produit = value; }
        }

        public int IdExploitation
        {
            get { return idExploitation; }
            set { idExploitation = value;
            this.OnPropertyChanged("IdExploitation");
            }
        }

        public int IdSite
        {
            get { return idSite; }
            set { idSite = value;
            this.OnPropertyChanged("IdSite");
            }
        }

        public string CompteOhada
        {
            get { return compteOhada; }
            set { compteOhada = value;
            this.OnPropertyChanged("CompteOhada");
            }
        }

        public string BackGround
        {
            get { return backGround; }
            set { backGround = value;
            this.OnPropertyChanged("BackGround");
            }
        }

        public bool Isprorata
        {
            get { return isprorata; }
            set { isprorata = value;
            this.OnPropertyChanged("Isprorata");
            }
        }


        public bool Exonerer
        {
            get { return exonerer; }
            set { exonerer = value;
            this.OnPropertyChanged("Exonerer");
            }
        }

        public int   IdProduit
        {
            get { return idProduit; }
            set { idProduit = value;
            this.OnPropertyChanged("IdProduit");
            }
        }

        public int IdClient
        {
            get { return idClient; }
            set { idClient = value;
            this.OnPropertyChanged("IdClient");
            }
        }

        public decimal  Prixunitaire
        {
            get { return prixunitaire; }
            set
            {
                prixunitaire = value;
                this.OnPropertyChanged("Prixunitaire");
            }
        }

        public float  Quantite
        {
            get { return quantite; }
            set { quantite = value;
            this.OnPropertyChanged("Quantite");
            }
        }

        public ClientModel Customer
        {
            get { return customer; }
            set
            {
                customer = value;

                this.OnPropertyChanged("Customer");
            }
        }

        public ExploitationFactureModel Exploitation
        {
            get { return exploitation; }
            set { exploitation = value;
            this.OnPropertyChanged("Customer");
            }
        }



        public string NomProduit
        {
            get { return nomProduit; }
            set { nomProduit = value;
            this.OnPropertyChanged("NomProduit");
            }
        }
        #endregion

        #region METHOD

        public bool IS_DETAIL_EXIST_FACTURE(int idSite, Int32 idDetail)
        {
            try
            {
                return DAL.DETAIL_PRODUIT_FACT_EXIST(idSite, idDetail);
            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<DetailProductModel > DETAIL_PRODUIT_GETLISTE(int idPproduit,int idClient)
        {
            ObservableCollection<DetailProductModel> mdetails = new ObservableCollection<DetailProductModel>();
            ClientModel clientexecute = new ClientModel();
            try
            {
                List<ProduitDetail> details = DAL.GetAll_DETAIL_PRODUITBY_PRODUIT(idPproduit,idClient);
                if (details.Count > 0)
                {
                    foreach (ProduitDetail det in details)
                    {
                        DetailProductModel newProd = ConvertFrom(det);
                        newProd.BackGround = "Black";
                        newProd.Customer = ConvertFromClient(det.Client); 
                        newProd .Produit=ConverfromProduit (det .Produit );
                        newProd.Exploitation = ConverExploitationfrom(det.Exploitation);
                        mdetails.Add(newProd);
                    }
                }
                return mdetails;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<DetailProductModel> DETAIL_PRODUIT_GETLISTE_Archive(int idPproduit, int idClient)
        {
            ObservableCollection<DetailProductModel> mdetails = new ObservableCollection<DetailProductModel>();
            ClientModel clientexecute = new ClientModel();
            try
            {
                List<ProduitDetail> details = DAL.GetAll_DETAIL_PRODUITBY_PRODUIT_Archive(idPproduit, idClient);
                if (details.Count > 0)
                {
                    foreach (ProduitDetail det in details)
                    {
                        DetailProductModel newProd = ConvertFrom(det);
                        newProd.BackGround = "Black";
                        newProd.Customer = ConvertFromClient(det.Client);
                        newProd.Produit = ConverfromProduit(det.Produit);
                        newProd.Exploitation = ConverExploitationfrom(det.Exploitation);
                        mdetails.Add(newProd);
                    }
                }
                return mdetails;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public List <DetailProductModel> DETAIL_PRODUIT_BYCLIENT( int idClient, Int32 idSite)
        {
            List<DetailProductModel> mdetails = new List<DetailProductModel>();
            ClientModel clientexecute = new ClientModel();
            try
            {
                List<ProduitDetail> details = DAL.GetAll_DETAIL_PRODUITBY_CLIENT(idClient, idSite);
                if (details.Count > 0)
                {
                    foreach (ProduitDetail det in details)
                    {
                        DetailProductModel newProd = ConvertFrom(det);
                        newProd.BackGround = "Black";
                        newProd.Customer = ConvertFromClient(det.Client);
                        newProd.Produit = ConverfromProduit(det.Produit);
                        mdetails.Add(newProd);
                    }
                }
                return mdetails;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public List<DetailProductModel> DETAIL_PRODUIT_BYCLIENT_Archive(int idClient, Int32 idSite)
        {
            List<DetailProductModel> mdetails = new List<DetailProductModel>();
            ClientModel clientexecute = new ClientModel();
            try
            {
                List<ProduitDetail> details = DAL.GetAll_DETAIL_PRODUITBY_CLIENT_Archive(idClient, idSite);
                if (details.Count > 0)
                {
                    foreach (ProduitDetail det in details)
                    {
                        DetailProductModel newProd = ConvertFrom(det);
                        newProd.BackGround = "Black";
                        newProd.Customer = ConvertFromClient(det.Client);
                        newProd.Produit = ConverfromProduit(det.Produit);
                        mdetails.Add(newProd);
                    }
                }
                return mdetails;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public DetailProductModel DETAIL_PRODUIT_GETBYID(int id )
        {
            DetailProductModel newProd = null;
            ClientModel clientexecute = new ClientModel();
            try
            {
                ProduitDetail detail = DAL.GetAll_DETAIL_PRODUITBy_Id (id );
                if (detail !=null )
                {
                     newProd = ConvertFrom(detail);
                    newProd.Customer = ConvertFromClient(detail.Client); //clientexecute.CLIENT_GETLISTE_BYID(newProd.idClient);
                    newProd.Produit = ConverfromProduit(detail.Produit);
                }
                return newProd;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public DetailProductModel DETAIL_PRODUIT_GETBYID_Archive(int id)
        {
            DetailProductModel newProd = null;
            ClientModel clientexecute = new ClientModel();
            try
            {
                ProduitDetail detail = DAL.GetAll_DETAIL_PRODUITBy_Id_Archive(id);
                if (detail != null)
                {
                    newProd = ConvertFrom(detail);
                    newProd.Customer = ConvertFromClient(detail.Client); //clientexecute.CLIENT_GETLISTE_BYID(newProd.idClient);
                    newProd.Produit = ConverfromProduit(detail.Produit);
                }
                return newProd;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public bool DETAIL_PRODUIT_ADD(DetailProductModel detail)
        {

            try
            {
                if (detail != null)
                    DAL.PRODUIT_DETAIL_ADD(ConvertTo(detail));
                return true;

            }
            catch (Exception de)
            {
                throw new DALException(de.Message);
            }
        }


        public bool DETAIL_PRODUIT_ANNULE_EXPLOITATION(int id,int idExploitation, int idSite)
        {

            try
            {
                if (id > 0)
                    DAL.DETAIL_PRODUIT_ANNULER_EXPLOITATION(id, idExploitation, idSite);
                return true;

            }
            catch (Exception de)
            {
                throw new DALException(de.Message);
            }
        }


        public bool DETAIL_PRODUIT_DELETE(int id )
        {

            try
            {
                if (id > 0)
                    DAL.DETAIL_PRODUIT_DELETE(id);
                return true;

            }
            catch (Exception de)
            {
                throw new DALException (de.Message);
            }
        }


        #endregion

        #region BUSNESS METHOD

        ExploitationFactureModel ConverExploitationfrom(ExploitationFacture efacture)
        {
            ExploitationFactureModel newFact = null;
            if (efacture != null)
                newFact = new ExploitationFactureModel
                {
                    IdExploitation = efacture.IdExploitation,
                    IdLangue = efacture.IdLangue,
                    Libelle = efacture.Libelle,
                    IdClient = efacture.IdClient,
                    IdSite = efacture.IdSite


                };
            return newFact;

        }

        ProduitModel ConverfromProduit(Produit prod)
        {
            ProduitModel newProd = null;
            if (prod != null)
                newProd = new ProduitModel
                {
                    IdProduit = prod.IdProduit,
                    IdLangue = prod.IdLangue,
                    Libelle = prod.Libelle,
                    PrixUnitaire = prod.PrixUnitaire,
                    IdSite = prod.IdSite,
                    CompteOhada = prod.CompteOhada,
                    CompteExonere=prod.CompteExonere

                };
            return newProd;

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
                    TermeNombre = client.TermeNombre,
                    TermeDescription = client.TermeDescription,
                    BoitePostal = client.BoitePostal,
                    IdCompte =client .IdCompte , 
                    IdSite =client .IdSite ,
                    IdExonere =client .IdExonere, 
                    NumemroImat =client .NumeroImatriculation  


                };
            return newClient;

        }

        DetailProductModel ConvertFrom(ProduitDetail det)
        {
            DetailProductModel newDet=null ;
            if (det != null)
                newDet = new DetailProductModel { 
                    IdDetail =det .IdDetail,
                    IdProduit =det .IdProduit,
                    IdClient =det .IdClient, 
                    Exonerer =det .Exonerer,  NomProduit=det .NomProduit ,
                    Prixunitaire =det .PrixUnitaire ,
                    Quantite =det .Quantite , 
                    Isprorata =det .IsProrata,
                    CompteOhada =det .CompteOhada ,
                    IdSite =det .IdSite ,
                    IdExploitation =det .IdExploitation , 
                    isCheck =false , 
                    Specialfact =det .SpecialFact 
                };
            return newDet;
        }

        ProduitDetail ConvertTo(DetailProductModel det)
        {
            ProduitDetail newProd=null ;
            if (det != null)
                newProd = new ProduitDetail {
                    IdDetail =det .IdDetail , 
                    IdClient =det .IdClient ,
                    IdProduit =det .IdProduit , 
                    Quantite =det .Quantite , NomProduit =det .NomProduit ,
                    PrixUnitaire =det .Prixunitaire,
                    Exonerer =det .Exonerer,
                    IsProrata =det .Isprorata, 
                    CompteOhada =det .CompteOhada ,
                    IdSite =det .IdSite  ,
                    IdExploitation =det .IdExploitation ,
                    SpecialFact =det .Specialfact 
                };
            return newProd;
        }

        #endregion
    }
}
