using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;
using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Model
{
    public class DeviseModel : ViewModelBase
    {

         public int ID_Devise { get; set; }
        private string libelle;
        private string taux;
        private int  idSite;
        private bool isDefault;
        private string sigle;

     

       
        Facturation DAL = null;
        public DeviseModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            ID_Devise = 0;
            libelle = string.Empty;
        }

        #region PROPERTIES

        public string Sigle
        {
            get { return sigle; }
            set { sigle = value;
            this.OnPropertyChanged("Sigle");
            }
        }


        public bool IsDefault
        {
            get { return isDefault; }
            set { isDefault = value;
            this.OnPropertyChanged("IsDefault");
            }
        }

        public int  IdSite
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

       public  List<DeviseModel> Devise_Archive_SELECT(int idsite)
        {
            List<DeviseModel> devises = new List<DeviseModel>();

            List<Devise> devisefrom = DAL.GetAllDeviseArchive(idsite); ;
            if (devisefrom != null)
            {
                foreach (var dev in devisefrom)
                    devises.Add(Convertfrom(dev));
            }
            return devises;
        }

        public List<DeviseModel> Devise_SELECT(int Idsite)
        {
            List<DeviseModel> devises = new List<DeviseModel>();
            
            try
            {

                List<Devise> devisefrom = DAL.GetAllDevise(Idsite); ;
                if (devisefrom != null)
                {
                    foreach (var dev in devisefrom )
                        devises.Add (Convertfrom (dev ));
                }
                return devises ;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public DeviseModel Devise_SELECTById(int id,int Idsite)
        {
            DeviseModel currentDevise = null;
            try
            {

                Devise devisefrom = DAL.GetAllDeviseById(id, Idsite);
                if (devisefrom != null)
                    currentDevise = Convertfrom(devisefrom);
                
                return currentDevise;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public DeviseModel Devise_ArchiveSELECTById(int id, int Idsite)
        {
            DeviseModel currentDevise = null;
            try
            {

                Devise devisefrom = DAL.GetAllDeviseArchiveById(id, Idsite);
                if (devisefrom != null)
                    currentDevise = Convertfrom(devisefrom);

                return currentDevise;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

      

    

        public bool Devise_ADD(DeviseModel  devise)
        {

            try
            {

                DAL.DeviseADD(converTo(devise));
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool Devise_DELETE(int id)
        {

            try
            {
                DAL.DeviseDELETE(id);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        // devise client

        public List<DeviseModel> DeviseClient_SELECT(int Idsite)
        {
            List<DeviseModel> devises = new List<DeviseModel>();

            try
            {

                List<Devise> devisefrom = DAL.GetAllDeviseClient(Idsite); ;
                if (devisefrom != null)
                {
                    foreach (var dev in devisefrom)
                        devises.Add(Convertfrom(dev));
                }
                return devises;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }


        public DeviseModel DeviseClient_SELECTById(int id, int Idsite)
        {
            DeviseModel currentDevise = null;
            try
            {

                Devise devisefrom = DAL.GetAllDeviseClientById(id, Idsite);
                if (devisefrom != null)
                    currentDevise = Convertfrom(devisefrom);

                return currentDevise;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public bool DeviseClient_ADD(DeviseModel devise)
        {

            try
            {

                DAL.DeviseClientADD(converTo(devise));
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool DeviseClient_DELETE(int id)
        {

            try
            {
                DAL.DeviseClientDELETE(id);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        #endregion

        #region BUISNESS METHOD

        DeviseModel Convertfrom(Devise devise)
        {
            DeviseModel newdevise=null ;
            if (devise != null)
                newdevise = new DeviseModel { ID_Devise =devise .ID_Devise , Libelle =devise .Libelle , Taux =devise .Taux,IdSite =devise.IdSite, IsDefault=devise.IsDefault, Sigle=devise.Sigle   };
            return newdevise;

        }


        Devise converTo(DeviseModel devise)
        {
            Devise newDevise=null ;
            if (devise != null)
                newDevise = new Devise { ID_Devise =devise .ID_Devise , Libelle =devise .Libelle , Taux =devise .Taux, IdSite =devise .IdSite , IsDefault=devise.IsDefault,Sigle=devise.Sigle  };
            return newDevise;
        }
        #endregion
    }
}
