using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;
using System.Collections.ObjectModel;
using System.Data;

namespace AllTech.FrameWork.Model
{
    public class TaxeModel : ViewModelBase
    {

        public int ID_Taxe { get; set; }
        private string libelle;
        private string taux;
        private int idSite;
        private bool taxeDefault;

       
     


        Facturation DAL = null;
        public TaxeModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            ID_Taxe = 0;
            libelle = string.Empty;
            taux = "0%";
        }

        public TaxeModel(int _id,string _libelle,string _taux)
        {
            
            ID_Taxe = _id ;
            libelle =_libelle  ;
            taux = _taux;
        }

        public TaxeModel(int _id)
        {

            ID_Taxe = _id;
            libelle =string .Empty ;
            taux =string .Empty ;
        }

        #region PROPERTIES

        public bool TaxeDefault
        {
            get { return taxeDefault; }
            set { taxeDefault = value;
            this.OnPropertyChanged("TaxeDefault");
            }
        }

        public int IdSite
        {
            get { return idSite; }
            set { idSite = value;
            this.OnPropertyChanged("IdSite");
            }
        }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value;
            this.OnPropertyChanged("Libelle");
            }
        }

        public string Taux
        {
            get { return taux; }
            set { taux = value;
            this.OnPropertyChanged("Taux");
            }
        }

        #endregion

        #region METHODS

        public List<TaxeModel> Taxe_SELECT(int id, int IdSite)
        {
           List  <TaxeModel > taxes  = new List <TaxeModel >();
            try
            {
                List<Taxe> taxefrom = DAL.Taxes_SELECT(id, IdSite);
                if (taxefrom != null)
                 foreach (var tx in taxefrom )
                     taxes .Add (Convertfrom (tx)); 

                
                return taxes ;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public List<TaxeModel> Taxes_SELECTARCHIVE(int id, int idSite)
        {
            List<TaxeModel> taxes = new List<TaxeModel>();
            List<Taxe> taxefrom = DAL.Taxes_SELECTARCHIVE(id, idSite);
            if (taxefrom != null)
                foreach (var tx in taxefrom)
                    taxes.Add(Convertfrom(tx));


            return taxes;
        }

        public TaxeModel Taxes_SELECTByIdArchive(int id, int idSite)
        {
            TaxeModel taxeSelect = null;
            Taxe taxefrom = DAL.Taxes_SELECTByIdArchive(id, idSite);
            if (taxefrom != null)
                taxeSelect = Convertfrom(taxefrom);


            return taxeSelect;
        }
         

      
        public TaxeModel Taxe_SELECTById(int id,int IdSite)
        {
            TaxeModel taxeSelect = null;
            try
            {
                Taxe taxefrom = DAL.Taxes_SELECTById(id, IdSite);
                if (taxefrom != null)
                      taxeSelect=Convertfrom(taxefrom);


                return taxeSelect;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public DataTable Taxe_SELECTByDataTable(int idsites)
        {
           DataTable table=new DataTable ();
            try
            {
                table = DAL.Taxes_SELECTByDataTAble(idsites);
                return table;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public DataTable Taxes_SELECTByDataTAbleArchive(int idsites)
        {
           return DAL.Taxes_SELECTByDataTAbleArchive(idsites);
        }



        public List<TaxeModel> Taxe_SELECTByRef(string inRefLabel,int idSite)
        {
            List<TaxeModel> taxes = new List<TaxeModel>();
            try
            {
                List<Taxe> taxefrom = DAL.Taxes_SELECTByRef(inRefLabel,idSite);
                if (taxefrom != null)
                {
                    TaxeModel t = new TaxeModel { ID_Taxe=0, Libelle="...", Taux="...", TaxeDefault=false };
                    taxes.Add(t);
                    foreach (var dt in taxefrom)
                            taxes.Add(Convertfrom(dt));
                    taxes.OrderBy(tt=>tt.ID_Taxe);
                }

                return taxes;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public List<TaxeModel> Taxe_SELECTByRef_Archive(string inRefLabel, int idSite)
        {
            List<TaxeModel> taxes = new List<TaxeModel>();
            try
            {
                List<Taxe> taxefrom = DAL.Taxes_SELECTByRef_Archive(inRefLabel, idSite);
                if (taxefrom != null)
                {
                    TaxeModel t = new TaxeModel { ID_Taxe = 0, Libelle = "...", Taux = "...", TaxeDefault = false };
                    taxes.Add(t);
                    foreach (var dt in taxefrom)
                        taxes.Add(Convertfrom(dt));
                    taxes.OrderBy(tt => tt.ID_Taxe);
                }

                return taxes;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }


        public List<TaxeModel> Taxe_SELECTByRef(string inRefLabel)
        {
            List<TaxeModel> taxes = new List<TaxeModel>();
            try
            {
                List<Taxe> taxefrom = DAL.Taxes_SELECT(0, idSite);
                if (taxefrom != null)
                {
                    List<Taxe> newTaxes = taxefrom.FindAll(t => t.Libelle == inRefLabel);

                    if (newTaxes != null)
                        foreach (var dt in newTaxes)
                            taxes.Add(Convertfrom(dt));
                }

                return taxes;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }


        
        public bool Taxe_ADD(TaxeModel  taxe)
        {

            try
            {
                DAL.TaxeADD( converTo (taxe));
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Taxe_DELETE(int id)
        {

            try
            {

                DAL.TaxeDELETE(id);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


       

        #endregion

        #region BUISNESS METHOD

        TaxeModel  Convertfrom(Taxe  taxe)
        {
            TaxeModel  newdevise = null;
            if (taxe  != null)
                newdevise = new TaxeModel {  ID_Taxe  = taxe.ID_Taxe , Libelle = taxe.Libelle, Taux = taxe.Taux, IdSite =taxe .IdSite, TaxeDefault=taxe.Taxedefault  };
            return newdevise;

        }


        Taxe  converTo(TaxeModel  taxe)
        {
            Taxe  newDevise = null;
            if (taxe  != null)
                newDevise = new Taxe  { ID_Taxe= taxe.ID_Taxe , Libelle = taxe.Libelle, Taux = taxe.Taux, IdSite =taxe .IdSite , Taxedefault=taxe.TaxeDefault };
            return newDevise;
        }
        #endregion


    }
}
