using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Utils;
using System.Data;

namespace AllTech.FrameWork.Model
{
    public class LigneFactureModel : ViewModelBase
    {

        public long IdLigneFacture { get; set; }
        private long idFacture;
        private int idProduit;
        private int idDetailProduit;
        private int idUtilisateur;
        public  int IdUtilUpdate { get; set; }
        private string description;
        private decimal quantite;
        private decimal  prixUnitaire;
        private Int32 idExploitation;
        private decimal montanHT;
        public int IdSite { get; set; }
        public bool IsDelete { get; set; }
        public DateTime? Datecreate { get; set; }
        public DateTime? DateModif { get; set; }
        public int IdClient { get; set; }
        public int IdStatut { get; set; }
        private bool specialFacture;

      
    

        //new ,facture valider
        public string LibelleProduit { get; set; }
        public decimal QuantiteUnitaireDet { get; set; }
        public decimal PrixUnitaireDet { get; set; }
        public bool IsProrata { get; set; }
        public string LibelleCompteproduit { get; set; }
        public Int32 IdExoneration { get; set; }
        public string Libelleexoneration { get; set; }
        public string TauxProrata { get; set; }
        public string TauxTva { get; set; }
        public bool IsPecialFacture { get; set; }
        public string CompteGeneProduit { get; set; }
        public string CodeCompteGeneProduit { get; set; }
        public string CompteAnalytique { get; set; }
        public string CodeCompteAnalytique { get; set; }
        public string ProduitLabel { get; set; }
        public string DetailProduitLAbel { get; set; }

        private bool exonere;
        private FactureModel currentFacture;
        private UtilisateurModel currentUtilisateur;
        private ProduitModel currentProduit;
        private DetailProductModel currentDetailproduit;


        Facturation DAL = null;

        public LigneFactureModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            IdLigneFacture = 0;
            quantite = 0;
        }

      

        #region PROPERTIES

        public bool SpecialFacture
        {
            get { return specialFacture; }
            set { specialFacture = value;
            this.OnPropertyChanged("SpecialFacture");
            }
        }

        public Int32 IdExploitation
        {
            get { return idExploitation; }
            set { idExploitation = value;
            this.OnPropertyChanged("IdExploitation");
            }
        }

        public DetailProductModel CurrentDetailproduit
        {
            get { return currentDetailproduit; }
            set { currentDetailproduit = value;
            this.OnPropertyChanged("CurrentDetailproduit");
            }
        }


        public decimal PrixUnitaire
        {
            get { return prixUnitaire; }
            set { prixUnitaire = value;
            this.OnPropertyChanged("PrixUnitaire");
            }
        }


        public decimal MontanHT
        {
            get { return montanHT; }
            set { montanHT = value;
            this.OnPropertyChanged("MontanHT");
            }
        }

     


        public FactureModel CurrentFacture
        {
            get { return currentFacture; }
            set
            {
                currentFacture = value;
                this.OnPropertyChanged("CurrentFacture");
            }
        }

        public UtilisateurModel CurrentUtilisateur
        {
            get { return currentUtilisateur; }
            set
            {
                currentUtilisateur = value;
                this.OnPropertyChanged("CurrentUtilisateur");
            }
        }

        public ProduitModel CurrentProduit
        {
            get { return currentProduit; }
            set
            {
                currentProduit = value;
                this.OnPropertyChanged("CurrentProduit");
            }
        }

        public int IdUtilisateur
        {
            get { return idUtilisateur; }
            set { idUtilisateur = value;
            this.OnPropertyChanged("IdUtilisateur");
            }
        }

        public long IdFacture
        {
            get { return idFacture; }
            set { idFacture = value;
            this.OnPropertyChanged("IdFacture");
            }
        }

        public int IdDetailProduit
        {
            get { return idDetailProduit; }
            set { idDetailProduit = value;
            this.OnPropertyChanged("IdDetailProduit");
            }
        }

        public int IdProduit
        {
            get { return idProduit; }
            set { idProduit = value;
            this.OnPropertyChanged("IdProduit");
            }
        }

        public string Description
        {
            get { return description; }
            set { description = value;
            this.OnPropertyChanged("Description");
            }
        }

        public decimal Quantite
        {
            get { return quantite; }
            set { quantite = value;
            this.OnPropertyChanged("Quantite");
            }
        }

      

        public bool Exonere
        {
            get { return exonere; }
            set { exonere = value;
            this.OnPropertyChanged("Exonere");
            }
        }


        #endregion

        #region METHODS


        public ObservableCollection<LigneFactureModel > LIGNE_FACTURE_GETLISTE()
        {
            ObservableCollection<LigneFactureModel> factures = new ObservableCollection<LigneFactureModel>();
            try
            {
                
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

       


        public List <LigneFactureModel> LIGNE_FACTURE_BYIDFActure(long idFacture)
        {
            ObservableCollection<LigneFactureModel> factures = new ObservableCollection<LigneFactureModel>();
            List <LigneFactureModel> facturess = new List<LigneFactureModel>();
              
            try
            {
                LigneFactureModel ligne = null;
                List<LigneFacture> lgf = DAL.GetAll_LIGNE_FACTURE(idFacture);
                if (lgf.Count > 0)
                {
                    foreach (LigneFacture lf in lgf)
                    {
                        ligne = Converfrom(lf);
                        ligne.CurrentProduit = ConverfromProduit(lf.CurrentProduit);
                        ligne.CurrentDetailproduit = ConvertFromdetailProd(lf .CurrentDetailProduit );
                        facturess.Add(ligne);
                        
                    }
                }

                List<LigneFactureModel> allfacture= facturess.FindAll (l=>l.IdFacture ==idFacture );

                return allfacture;
            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public List<LigneFactureModel> LIGNE_FACTURE_BYIDFActure_Validate(long idFacture)
        {
            ObservableCollection<LigneFactureModel> factures = new ObservableCollection<LigneFactureModel>();
            List<LigneFactureModel> facturess = new List<LigneFactureModel>();

            try
            { 
                LigneFactureModel ligne = null;
               var lgf = DAL.GetAll_LIGNE_FACTURE_Validate(idFacture);
                if (lgf.Count > 0)
                {
                    foreach (LigneFacture lf in lgf)
                    {
                        ligne = Converfrom(lf);
                        ligne.CurrentProduit = ConverfromProduit(lf.CurrentProduit);
                        ligne.CurrentDetailproduit = ConvertFromdetailProd(lf.CurrentDetailProduit);
                        facturess.Add(ligne);

                    }
                }

                List<LigneFactureModel> allfacture = facturess.FindAll(l => l.IdFacture == idFacture);

                return allfacture;
            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public List<LigneFactureModel> LIGNE_FACTURE_BYIDFActure_Archive(long idFacture)
        {
            ObservableCollection<LigneFactureModel> factures = new ObservableCollection<LigneFactureModel>();
            List<LigneFactureModel> facturess = new List<LigneFactureModel>();

            try
            {
                LigneFactureModel ligne = null;
                List<LigneFacture> lgf = DAL.GetAll_LIGNE_FACTURE_Archive(idFacture);
                if (lgf.Count > 0)
                {
                    foreach (LigneFacture lf in lgf)
                    {
                        ligne = Converfrom(lf);
                        ligne.CurrentProduit = ConverfromProduit(lf.CurrentProduit);
                        ligne.CurrentDetailproduit = ConvertFromdetailProd(lf.CurrentDetailProduit);
                        facturess.Add(ligne);

                    }
                }

                List<LigneFactureModel> allfacture = facturess.FindAll(l => l.IdFacture == idFacture);

                return allfacture;
            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }




        public LigneFactureModel LIGNE_FACTURE_GETLISTE_ByID(long id)
        {
            LigneFactureModel facture = null;
            try
            {
                LigneFacture fn = DAL.Get_LIGNE_FACTUREById(id);
                if (fn != null)
                    facture = Converfrom(fn);
                facture.CurrentProduit = ConverfromProduit(fn.CurrentProduit);
                facture.CurrentDetailproduit = ConvertFromdetailProd(fn.CurrentDetailProduit);
                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public LigneFactureModel LIGNE_FACTURE_GETLISTE_ByID_Archive(long id)
        {
            LigneFactureModel facture = null;
            try
            {
                LigneFacture fn = DAL.Get_LIGNE_FACTUREById_Archive(id);
                if (fn != null)
                    facture = Converfrom(fn);
                facture.CurrentProduit = ConverfromProduit(fn.CurrentProduit);
                facture.CurrentDetailproduit = ConvertFromdetailProd(fn.CurrentDetailProduit);
                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public LigneFactureModel LIGNE_FACTURE_GETLISTE_ByID_Validate(long id)
        {
            LigneFactureModel facture = null;
            try
            {
                LigneFacture fn = DAL.Get_LIGNE_FACTUREById_Validate(id);
                if (fn != null)
                    facture = Converfrom(fn);
                facture.CurrentProduit = ConverfromProduit(fn.CurrentProduit);
                facture.CurrentDetailproduit = ConvertFromdetailProd(fn.CurrentDetailProduit);
                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public List<AutoCompleteEntry> LIGNE_FACTURE_DESCRIPTION(string valeur)
        {
            List<AutoCompleteEntry> liste = new List<AutoCompleteEntry>();
            try
            {
                DataTable lble = DAL.GEt_DESCRIPTION_LIGNE(valeur);
                if (lble != null)
                {
                    foreach (DataRow row in lble.Rows )
                    {
                        liste.Add(new AutoCompleteEntry(row["valeur"].ToString(), null));
                    }
                }
                return liste ;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool LIGNE_FACTURE_ADD(LigneFactureModel  ligneFacture)
        {

            try
            {
                if (ligneFacture != null)
                    DAL.LIGNE_FACTURE_ADD(ConvertTo(ligneFacture));

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

      
        public bool LIGNE_FACTURE_DELETE(long id,int idMode)
        {

            try
            {
                DAL.LIGNE_FACTURE_DELETE(id, idMode);

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        #endregion

        #region BUSNESS METHOD

        LigneFactureModel Converfrom(LigneFacture lfacture)
        {
            LigneFactureModel newFact = null;
            if (lfacture != null)
                newFact = new LigneFactureModel
                {
                    IdLigneFacture = lfacture.IdLigneFacture,
                    Description = lfacture.Description,
                    Exonere = lfacture.Exonere,
                    IdFacture = lfacture.IdFacture,
                    IdDetailProduit =lfacture.IdDetailProduit ,
                    IdUtilisateur =lfacture.IdUtilisateur ,
                    IdProduit = lfacture.IdProduit,
                    Quantite = lfacture.Quantite, 
                    PrixUnitaire =lfacture .Prixunit , 
                    MontanHT =lfacture .MontantHT ,
                    IdSite = lfacture.IdSite, 
                    IdUtilUpdate=lfacture.IdUtililModif, 
                    IsDelete =lfacture.isdlete, 
                    Datecreate =lfacture.DateCreation , 
                    DateModif =lfacture.DateModif   ,
                   IdExploitation=lfacture.Idexploitation,
                   SpecialFacture=lfacture.SpecialFacture,

                    LibelleProduit = lfacture.LibelleProduit,
                    QuantiteUnitaireDet = lfacture.QuantiteUnitaireDet, 
                    PrixUnitaireDet=lfacture.PrixUnitaireDet, 
                    IsProrata=lfacture.IsProrata,
                    LibelleCompteproduit = lfacture.LibelleCompteproduit,
                    IdExoneration = lfacture.IdExoneration,
                    Libelleexoneration = lfacture.Libelleexoneration,
                    TauxProrata = lfacture.TauxProrata,
                    TauxTva = lfacture.TauxTva,
                    IsPecialFacture = lfacture.IsPecialFacture,
                    CompteGeneProduit = lfacture.CompteGeneProduit,
                    CodeCompteGeneProduit = lfacture.CodeCompteGeneProduit,
                    CompteAnalytique = lfacture.CompteAnalytique,
                    CodeCompteAnalytique = lfacture.CodeCompteAnalytique

                };
            return newFact;

        }

        LigneFacture ConvertTo(LigneFactureModel fact)
        {
            LigneFacture newFact = null;
            if (fact != null)
                newFact = new LigneFacture
                {
                    IdLigneFacture = fact.IdLigneFacture,
                    Description = fact.Description,
                    Exonere = fact.Exonere,
                    IdFacture = fact.IdFacture,
                    IdProduit = fact.IdProduit,
                    IdDetailProduit = fact.IdDetailProduit,
                    IdUtilisateur = fact.IdUtilisateur,
                    Quantite =(decimal) fact.Quantite,
                    Prixunit =fact .PrixUnitaire ,
                    MontantHT =fact .MontanHT , 
                    IdSite =fact .IdSite ,
                    DateModif=fact.DateModif ,
                   Idexploitation=fact.IdExploitation,
                 SpecialFacture=fact.SpecialFacture,

                    LibelleProduit = fact.LibelleProduit,
                    QuantiteUnitaireDet = fact.QuantiteUnitaireDet,
                    PrixUnitaireDet = fact.PrixUnitaireDet,
                    IsProrata = fact.IsProrata,
                    LibelleCompteproduit = fact.LibelleCompteproduit,
                    IdExoneration = fact.IdExoneration,
                    Libelleexoneration = fact.Libelleexoneration,
                    TauxProrata = fact.TauxProrata,
                    TauxTva = fact.TauxTva,
                    IsPecialFacture = fact.IsPecialFacture,
                    CompteGeneProduit = fact.CompteGeneProduit,
                    CodeCompteGeneProduit = fact.CodeCompteGeneProduit,
                    CompteAnalytique = fact.CompteAnalytique,
                    CodeCompteAnalytique = fact.CodeCompteAnalytique,
                    IdClient = fact.IdClient,
                    IdStatut = fact.IdStatut,
                    ProduitLabel=fact.ProduitLabel,
                 
                    
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

        DetailProductModel ConvertFromdetailProd(ProduitDetail det)
        {
            DetailProductModel newDet = null;
            if (det != null)
                newDet = new DetailProductModel
                {
                    IdDetail = det.IdDetail,
                    IdProduit = det.IdProduit,
                    IdClient = det.IdClient,
                    Exonerer = det.Exonerer,
                    NomProduit = det.NomProduit,
                    Prixunitaire = det.PrixUnitaire,
                    Quantite = det.Quantite,
                    Isprorata = det.IsProrata,
                    CompteOhada = det.CompteOhada,
                    IdSite = det.IdSite,
                    IdExploitation =det.IdExploitation , 
                    Specialfact=det.SpecialFact 
                };
            return newDet;
        }
        #endregion
    }
}
