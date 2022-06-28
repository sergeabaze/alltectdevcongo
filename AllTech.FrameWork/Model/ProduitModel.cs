using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Global;
using FACTURATION_DAL.Model;
using FACTURATION_DAL;
using System.Data;

namespace AllTech.FrameWork.Model
{
    public class ProduitModel : ViewModelBase
    {
        public int IdProduit { get; set; }
        private string libelle;
        private int idLangue;
        private int idSite;
        private decimal prixUnitaire;
        private string compteOhada;
        private string compteExonere;
        public int ModeFacturation { get; set; }

      
     

        private LangueModel langues;
        private ObservableCollection<DetailProductModel> detailProd = new ObservableCollection<DetailProductModel>();

      
    

        Facturation DAL = null;
        public ProduitModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region PROPERTIES

        public string CompteExonere
        {
            get { return compteExonere; }
            set { compteExonere = value;
            this.OnPropertyChanged("CompteExonere");
            }
        }


        public string CompteOhada
        {
            get { return compteOhada; }
            set { compteOhada = value;
            this.OnPropertyChanged("CompteOhada");
            }
        }

        public int IdSite
        {
            get { return idSite; }
            set { idSite = value;
            this.OnPropertyChanged("IdSite");
            }
        }

        public int IdLangue
        {
            get { return idLangue; }
            set
            {
                idLangue = value;
                this.OnPropertyChanged("IdLangue");
            }
        }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value;
            this.OnPropertyChanged("Libelle");
            }
        }

        public LangueModel Langues
        {
            get { return langues; }
            set
            {
                langues = value;
                this.OnPropertyChanged("Langues");
            }
        }


        public decimal PrixUnitaire
        {
            get { return prixUnitaire; }
            set { prixUnitaire = value;
            this.OnPropertyChanged("PrixUnitaire");
            }
        }

        public ObservableCollection<DetailProductModel> DetailProd
        {
            get { return detailProd; }
            set { detailProd = value;
            this.OnPropertyChanged("DetailProd");
            }
        }
        #endregion

        #region METHODS

        public  DataTable Produit_SELECT_ByClient(Int32 idSite,int idClient)
        {
           
            try
            {
                return DAL.GetAll_PRODUIT_ByCLIENT(idSite, idClient);
            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public ObservableCollection<ProduitModel> Produit_SELECT(Int32 idSite)
        {
            ObservableCollection<ProduitModel> produits = new ObservableCollection<ProduitModel>();
            DetailProductModel detProd = new DetailProductModel();
            LangueModel llangue = new LangueModel();
            try
            {
                List<Produit> prods = DAL.GetAll_PRODUIT(idSite);
                if (prods.Count > 0)
                {
                    foreach (Produit pr in prods)
                    {
                       
                        ProduitModel newProd = Converfrom(pr);
                        newProd .Langues =llangue.LANGUE_SELECTBYID(pr.IdLangue);
                        newProd.DetailProd = ConvertFromdetails(pr.DetailProduit);
                        produits.Add(newProd);
                    }
                }
               // produits.Sort(p=>p.Libelle );
                return produits;
             
            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
           
        }

          public ObservableCollection<ProduitModel> Produit_SELECT_Archive(Int32 idSite)
          {
              ObservableCollection<ProduitModel> produits = new ObservableCollection<ProduitModel>();
              DetailProductModel detProd = new DetailProductModel();
              LangueModel llangue = new LangueModel();
              try
              {
                  List<Produit> prods = DAL.GetAll_PRODUIT_Archive(idSite);
                  if (prods.Count > 0)
                  {
                      foreach (Produit pr in prods)
                      {

                          ProduitModel newProd = Converfrom(pr);
                          newProd.Langues = llangue.LANGUE_SELECTBYID(pr.IdLangue);
                          newProd.DetailProd = ConvertFromdetails(pr.DetailProduit);
                          produits.Add(newProd);
                      }
                  }
                  // produits.Sort(p=>p.Libelle );
                  return produits;

              }
              catch (Exception de)
              {
                  throw new Exception(de.Message);
              }

          }

          public ObservableCollection<ProduitModel> Produit_SEARCH(Int32 idSite, string libelle)
          {
              ObservableCollection<ProduitModel> produits = new ObservableCollection<ProduitModel>();
              DetailProductModel detProd = new DetailProductModel();
              LangueModel llangue = new LangueModel();
              try
              {
                  List<Produit> prods = DAL.Get_PRODUISEARCH(idSite, libelle);
                  if (prods.Count > 0)
                  {
                      foreach (Produit pr in prods)
                      {
                          ProduitModel newProd = Converfrom(pr);
                          newProd.Langues = llangue.LANGUE_SELECTBYID(pr.IdLangue);
                          newProd.DetailProd = detProd.DETAIL_PRODUIT_GETLISTE(newProd.IdProduit, 0);
                          produits.Add(newProd);
                      }

                  }
                  return produits;

              }
              catch (Exception de)
              {
                  throw new Exception(de.Message);
              }
          }




        public ProduitModel Produit_SELECTBY_ID(int id)
        {
            ProduitModel produit =null ;
            DetailProductModel detProd = new DetailProductModel();
            LangueModel llangue = new LangueModel();
            try
            {
                Produit prod = DAL.Get_PRODUITBYID(id);
                if (prod != null)
                {
                    produit = Converfrom (prod);
                    produit.Langues = llangue.LANGUE_SELECTBYID(produit.IdLangue);
                    produit.DetailProd = produit.DetailProd = ConvertFromdetails(prod.DetailProduit );// detProd.DETAIL_PRODUIT_GETLISTE(produit.IdProduit, 0);

                }
                return produit;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public ProduitModel Produit_SELECTBY_ID_Archive(int id)
        {
            ProduitModel produit = null;
            DetailProductModel detProd = new DetailProductModel();
            LangueModel llangue = new LangueModel();
            try
            {
                Produit prod = DAL.Get_PRODUITBYID_Archive(id);
                if (prod != null)
                {
                    produit = Converfrom(prod);
                    produit.Langues = llangue.LANGUE_SELECTBYID(produit.IdLangue);
                    produit.DetailProd = produit.DetailProd = ConvertFromdetails(prod.DetailProduit);// detProd.DETAIL_PRODUIT_GETLISTE(produit.IdProduit, 0);

                }
                return produit;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public ProduitModel Produit_SELECTBY_ID(int id,Int32 idSite)
        {
            ProduitModel produit = null;
            DetailProductModel detProd = new DetailProductModel();
            LangueModel llangue = new LangueModel();
            try
            {
                Produit prod = DAL.Get_PRODUITBYID(id, idSite);
                if (prod != null)
                {
                    produit = Converfrom(prod);
                    produit.Langues = llangue.LANGUE_SELECTBYID(produit.IdLangue);
                    produit.DetailProd = produit.DetailProd = ConvertFromdetails(prod.DetailProduit);// detProd.DETAIL_PRODUIT_GETLISTE(produit.IdProduit, 0);

                }
                return produit;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ProduitModel Produit_SELECTBY_ID_Archive(int id, Int32 idSite)
        {
            ProduitModel produit = null;
            DetailProductModel detProd = new DetailProductModel();
            LangueModel llangue = new LangueModel();
            try
            {
                Produit prod = DAL.Get_PRODUITBYID_Archive(id, idSite);
                if (prod != null)
                {
                    produit = Converfrom(prod);
                    produit.Langues = llangue.LANGUE_SELECTBYID(produit.IdLangue);
                    produit.DetailProd = produit.DetailProd = ConvertFromdetails(prod.DetailProduit);// detProd.DETAIL_PRODUIT_GETLISTE(produit.IdProduit, 0);

                }
                return produit;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


       


        public ObservableCollection<ProduitModel> Produit_SELECTBY_ID_Language(int id,Int32 idSite)
        {
            ObservableCollection<ProduitModel> produits = new ObservableCollection<ProduitModel>();
            DetailProductModel detProd = new DetailProductModel();
            LangueModel llangue = new LangueModel();
            try
            {
                List<Produit> prods = DAL.GetAll_PRODUIT_BYLangue(id, idSite);
                if (prods.Count > 0)
                {
                    Produit pp = prods[0];
                    ProduitModel p = new ProduitModel { IdProduit = 0, Libelle = ".....", IdLangue = pp.IdLangue, IdSite = pp.IdSite  };
                    produits.Add(p );
                    foreach (Produit pr in prods)
                    {

                        ProduitModel newProd = Converfrom(pr);
                        newProd.Langues = llangue.LANGUE_SELECTBYID(pr.IdLangue);
                        newProd.DetailProd = ConvertFromdetails(pr.DetailProduit);// detProd.DETAIL_PRODUIT_GETLISTE(newProd.IdProduit, 0);
                        produits.Add(newProd);
                    }
                }
                produits.Sort(p => p.Libelle);
                return produits;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }



        public ObservableCollection<ProduitModel> Produit_SELECTBY_Language_Archive(int id, Int32 idSite)
        {
            ObservableCollection<ProduitModel> produits = new ObservableCollection<ProduitModel>();
            DetailProductModel detProd = new DetailProductModel();
            LangueModel llangue = new LangueModel();
            try
            {
                List<Produit> prods = DAL.GetAll_PRODUIT_BYLangue_Archive(id, idSite);
                if (prods.Count > 0)
                {
                    Produit pp = prods[0];
                    ProduitModel p = new ProduitModel { IdProduit = 0, Libelle = ".....", IdLangue = pp.IdLangue, IdSite = pp.IdSite };
                    produits.Add(p);
                    foreach (Produit pr in prods)
                    {

                        ProduitModel newProd = Converfrom(pr);
                        newProd.Langues = llangue.LANGUE_SELECTBYID(pr.IdLangue);
                        newProd.DetailProd = ConvertFromdetails(pr.DetailProduit);// detProd.DETAIL_PRODUIT_GETLISTE(newProd.IdProduit, 0);
                        produits.Add(newProd);
                    }
                }
                produits.Sort(p => p.Libelle);
                return produits;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<ProduitModel> Produit_SELECTBY_ID_Language(int id, Int32 idSite,bool isUpdate)
        {
            ObservableCollection<ProduitModel> produits = new ObservableCollection<ProduitModel>();
            DetailProductModel detProd = new DetailProductModel();
            LangueModel llangue = new LangueModel();
            try
            {
                List<Produit> prods = DAL.GetAll_PRODUIT_BYLangue(id, idSite);
                if (prods.Count > 0)
                {
                    Produit pp = prods[0];
                    ProduitModel p = new ProduitModel { IdProduit = 0, Libelle = ".....", IdLangue = pp.IdLangue, IdSite = pp.IdSite };
                    produits.Add(p);
                    foreach (Produit pr in prods)
                    {

                        ProduitModel newProd = Converfrom(pr);
                        newProd.Langues = llangue.LANGUE_SELECTBYID(pr.IdLangue);
                        newProd.DetailProd = ConvertFromdetails(pr.DetailProduit);// detProd.DETAIL_PRODUIT_GETLISTE(newProd.IdProduit, 0);
                        produits.Add(newProd);
                    }
                }
                // produits.Sort(p => p.Libelle);
                return produits;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Produit_ADD(ProduitModel produit)
        {

            try
            {

                if (produit != null)
                {
                    DAL.PRODUIT_ADD(ConvertTo(produit));
                }
                return true ;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Produit_Suivi_Add(int ID, int idClient, int idProduit, int idSite, bool isParam)
        {

            try
            {
               return  DAL.PRODUIT_SUIVI_ADD( ID,  idClient,  idProduit,  idSite,  isParam);

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Produit_DELETE(int id)
        {

            try
            {
                if (id >0)
                DAL.PRODUIT_DELETE(id);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        #region BUSNESS METHOD

        ObservableCollection<DetailProductModel> ConvertFromdetails(List<ProduitDetail> det)
        {
            DetailProductModel detail = null;
            ObservableCollection<DetailProductModel> newDet = new ObservableCollection<DetailProductModel>();
            if (det != null)
                foreach (var ed in det)
                {
                    detail = new DetailProductModel
                {
                    IdDetail = ed.IdDetail,
                    IdProduit = ed.IdProduit,
                    IdClient = ed.IdClient,
                    Exonerer = ed.Exonerer,
                    NomProduit = ed.NomProduit,
                    Prixunitaire = ed.PrixUnitaire,
                    Quantite = ed.Quantite, 
                    CompteOhada =ed .CompteOhada, IdSite =ed .IdSite ,
                    Specialfact =ed .SpecialFact 

                };
                    newDet.Add(detail);
                }
            return newDet;
        }

        DetailProductModel ConvertFromdetail(ProduitDetail det)
        {
            DetailProductModel detail = null;

            if (det != null)
            {

                detail = new DetailProductModel
                {
                    IdDetail = det.IdDetail,
                    IdProduit = det.IdProduit,
                    IdClient = det.IdClient,
                    Exonerer = det.Exonerer,
                    NomProduit = det.NomProduit,
                    Prixunitaire = det.PrixUnitaire,
                    Quantite = det.Quantite,
                    CompteOhada = det.CompteOhada,
                    IdSite = det.IdSite,
                    Specialfact = det.SpecialFact

                };
            }

            return detail;
        }


        ProduitModel  Converfrom(Produit prod)
        {
            ProduitModel newProd = null;
            if (prod != null)
                newProd = new ProduitModel
                {
                     IdProduit  = prod .IdProduit , 
                    IdLangue=prod .IdLangue ,
                     Libelle =prod .Libelle ,
                     PrixUnitaire =prod .PrixUnitaire, 
                     IdSite =prod .IdSite  ,
                     CompteOhada =prod .CompteOhada ,
                    CompteExonere=prod.CompteExonere
                   
                };
            return newProd;

        }

        Produit  ConvertTo(ProduitModel prod)
        {
            Produit newProd = null;
            if (prod != null)
                newProd = new Produit
                {
                    IdProduit = prod.IdProduit,
                      IdLangue =prod .IdLangue ,
                        Libelle =prod .Libelle ,
                         PrixUnitaire =prod .PrixUnitaire , 
                         IdSite =prod .IdSite ,
                         CompteOhada =prod .CompteOhada ,
                         CompteExonere=prod.CompteExonere, 
                         ModeFacturation=prod.ModeFacturation
                };
            return newProd;

        }
        #endregion
        
        

        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion
        #endregion
    }
}
