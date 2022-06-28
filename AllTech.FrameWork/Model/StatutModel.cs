using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL.Model;
using FACTURATION_DAL;
using AllTech.FrameWork.PropertyChange;
using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Model
{
    public class StatutModel : ViewModelBase
    {

        public int IdStatut { get; set; }
        private string libelle;
        private int idLangue;
        private LangueModel langues;
        private string courtDesc;


        Facturation DAL = null;

        public StatutModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            IdStatut = 0;
            libelle = string.Empty;
            courtDesc = string.Empty;
        }


        #region PROPERTIES

        public string Libelle
        {
            get { return libelle; }
            set
            {
                libelle = value;
                this.OnPropertyChanged("Libelle");
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

        public LangueModel Langues
        {
            get { return langues; }
            set
            {
                langues = value;
                this.OnPropertyChanged("Langues");
            }
        }

        public string CourtDesc
        {
            get { return courtDesc; }
            set
            {
                courtDesc = value;
                this.OnPropertyChanged("CourtDesc");
            }
        }

        #endregion


        #region METHODS

        public ObservableCollection<StatutModel> STATUT_FACTURE_GETLISTE()
        {
            ObservableCollection<StatutModel> factures = new ObservableCollection<StatutModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<StatutFacture> obj = DAL.GetAll_STATUT_FACTURE ();
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {
                        //LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);
                        StatutModel fmodel = Converfrom(exp);
                        fmodel.Langues = new LangueModel { Id = exp.Llangue.IdLangue, Libelle = exp.Llangue.Libelle, Shortname = exp.Llangue.Shorname };
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


        public ObservableCollection<StatutModel> STATUT_FACTURE_GETLISTEByIdLanguage(int idLanguage)
        {
            ObservableCollection<StatutModel> factures = new ObservableCollection<StatutModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<StatutFacture> exploits = DAL.GetAll_STATUT_FACTUREBYLangue(idLanguage);
                if (exploits.Count > 0)
                {
                    foreach (var exp in exploits)
                    {
                       // LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);

                        StatutModel fmodel = Converfrom(exp);
                        fmodel.Langues = new LangueModel { Id = exp.Llangue.IdLangue, Libelle = exp.Llangue.Libelle, Shortname = exp.Llangue.Shorname };
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


        public StatutModel STATUT_FACTURE_GETLISTEByID(int id)
        {
            StatutModel statut = null;
            LangueModel llangue = new LangueModel();

            try
            {
                StatutFacture  exp = DAL.GetAll_STATUT_FACTUREBYID(id);

                if (exp != null)
                {
                    statut = Converfrom(exp);
                    statut.Langues = new LangueModel { Id = exp.Llangue.IdLangue, Libelle = exp.Llangue.Libelle, Shortname = exp.Llangue.Shorname };
                   
                }

                return statut;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public bool STATUT_FACTURE_ADD(StatutModel statut)
        {

            try
            {
                if (statut != null)
                    DAL.STATUT_FACTURE_ADD(ConvertTo(statut));

                return true;

            }
            catch (Exception de)
            {
                throw new DALException (de.Message);
            }
        }

        public bool STATUT_FACTURE_DELETE(int id)
        {

            try
            {
                DAL.STATUT_FACTURE_DELETE (id);

                return true;

            }
            catch (Exception de)
            {
                throw new DALException(de.Message);
            }
        }
        #endregion


        #region BUISNESS METHODS


        StatutModel  Converfrom(StatutFacture  efacture)
        {
            StatutModel newFact = null;
            if (efacture != null)
                newFact = new StatutModel
                {
                      IdStatut   = efacture.IdStatut ,
                      IdLangue = efacture.IdLangue,
                      Libelle = efacture.Libelle,
                      CourtDesc = efacture.ShortName 


                };
            return newFact;

        }

        StatutFacture ConvertTo(StatutModel fact)
        {
            StatutFacture newFact = null;
            if (fact != null)
                newFact = new StatutFacture
                {
                     IdStatut  = fact.IdStatut ,
                    IdLangue = fact.IdLangue,
                    Libelle = fact.Libelle, 
                    ShortName =fact .CourtDesc 
                };
            return newFact;

        }
        #endregion


    }
}
