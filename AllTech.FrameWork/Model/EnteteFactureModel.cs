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
    public class EnteteFactureModel : ViewModelBase
    {

        public int IdEntete { get; set; }
        private  string libelle { get; set; }
        private  int idLangue { get; set; }

        Facturation DAL = null;

        public EnteteFactureModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            IdEntete = 0;
            libelle = string.Empty;
        }

        #region PROPERTIES

        public string Libelle
        {
            get { return libelle; } 
            set { libelle=value ; }
        }

        public int IdLangue
        {
            get { return idLangue; }
            set { idLangue = value; }
        }

        #endregion

        #region METHODS

        public ObservableCollection<EnteteFactureModel> ENTETE_FACTURE_GETLISTE()
        {
            ObservableCollection<EnteteFactureModel> factures = new ObservableCollection<EnteteFactureModel>();
            try
            {
                factures.Add(new EnteteFactureModel { IdLangue=1, Libelle ="mon entete 1", IdEntete =1  });
                factures.Add(new EnteteFactureModel { IdLangue = 2, Libelle = " my start entete", IdEntete = 2});
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<EnteteFactureModel> ENTETE_FACTURE_GETLISTEByIdLanguage(int idLanguage)
        {
            ObservableCollection<EnteteFactureModel> factures = new ObservableCollection<EnteteFactureModel>();
            try
            {
                factures.Add(new EnteteFactureModel { IdLangue = 1, Libelle = "mon entete 1", IdEntete = 1 });
                factures.Add(new EnteteFactureModel { IdLangue = 2, Libelle = " my start entete", IdEntete = 2 });
                return factures; 

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


      

        public EnteteFactureModel ENTETE_FACTURE_GETLISTEByID(long id)
        {
            EnteteFactureModel facture = new EnteteFactureModel();
            try
            {
                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool ENTETE_FACTURE_INSERT(EnteteFactureModel facture)
        {

            try
            {
                // DAL.UTILISATEURADD(ConvertTo(user));

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool ENTETE_FACTURE_UPDATE(EnteteFactureModel facture)
        {

            try
            {
                // DAL.UTILISATEURADD(ConvertTo(user));

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public bool ENTETE_FACTURE_DELETE(long id)
        {

            try
            {
                // DAL.UTILISATEURADD(ConvertTo(user));

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }
        #endregion

        #region BUSNESS METHOD

        EnteteFactureModel Converfrom(EnteteFacture efacture)
        {
            EnteteFactureModel newFact = null;
            if (efacture != null)
                newFact = new EnteteFactureModel
                {
                    IdEntete = efacture.IdEntete ,
                    IdLangue = efacture.IdLangue ,
                    libelle = efacture.Libelle 
                     

                };
            return newFact;

        }

        EnteteFacture ConvertTo(EnteteFactureModel fact)
        {
            EnteteFacture newFact = null;
            if (fact != null)
                newFact = new EnteteFacture
                {
                    IdEntete = fact.IdEntete,
                    IdLangue = fact.IdLangue,
                   Libelle = fact.Libelle    
                };
            return newFact;

        }
        #endregion


    }
}
