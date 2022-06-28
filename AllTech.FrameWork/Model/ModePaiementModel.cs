using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL.Model;
using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Model
{
    public class ModePaiementModel : ViewModelBase
    {

        public int IdMode { get; set; }
        private string libelle;
        private int idLangue;
        private LangueModel langues;

        Facturation DAL = null;

        public ModePaiementModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            IdMode = 0;
            libelle = string.Empty;
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

        #endregion

        #region METHODS


        public ObservableCollection<ModePaiementModel > MODE_PAIEMENT_GETLISTE()
        {
            ObservableCollection<ModePaiementModel> factures = new ObservableCollection<ModePaiementModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ModePaiement > obj = DAL.GetAll_MODE_PAIEMENT ();
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {
                        LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);

                        ModePaiementModel fmodel = Converfrom(exp);
                        fmodel.Langues = newl;
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


        public ObservableCollection<ModePaiementModel > MODE_PAIEMENT_GETLISTEByIdLanguage(int idLanguage)
        {
            ObservableCollection<ModePaiementModel> factures = new ObservableCollection<ModePaiementModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ModePaiement > exploits = DAL.GetAll_MODEPAIEMENT_BYLangue(idLanguage);
                if (exploits.Count > 0)
                {
                    foreach (var exp in exploits)
                    {
                        LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);

                        ModePaiementModel fmodel = Converfrom(exp);
                        fmodel.Langues = newl;
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


        public ModePaiementModel MODE_PAIEMENT_GETLISTEByID(int id)
        {
            ModePaiementModel facture = null;
            LangueModel llangue = new LangueModel();

            try
            {
                ModePaiement  exp = DAL.GetAll_MODE_PAIEMENTBYID (id);

                if (exp != null)
                {
                    facture = Converfrom(exp);
                    LangueModel newl = llangue.LANGUE_SELECTBYID(facture.IdLangue);
                    facture.Langues = newl;
                }

                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool MODE_PAIEMENT_ADD(ModePaiementModel mode)
        {

            try
            {
                if (mode != null)
                    DAL.MODE_PAIEMENT_ADD (ConvertTo(mode));

                return true;

            }
            catch (Exception de)
            {
                throw new DALException(de.Message);
            }
        }

        public bool MODE_PAIEMENT_DELETE(int id)
        {

            try
            {
                DAL.MODE_PAIEMENT_DELETE (id);

                return true;

            }
            catch (Exception de)
            {
                throw new DALException(de.Message);
            }
        }
        #endregion


        #region BUISNESS METHODS

        ModePaiementModel Converfrom(ModePaiement mode)
        {
            ModePaiementModel newFact = null;
            if (mode != null)
                newFact = new ModePaiementModel
                {
                     IdMode  = mode.IdMode ,
                    IdLangue = mode.IdLangue,
                    Libelle = mode.Libelle


                };
            return newFact;

        }

        ModePaiement ConvertTo(ModePaiementModel mode)
        {
            ModePaiement newFact = null;
            if (mode != null)
                newFact = new ModePaiement
                {
                     IdMode = mode.IdMode ,
                    IdLangue = mode.IdLangue,
                    Libelle = mode.Libelle
                };
            return newFact;

        }
        #endregion
    }
}
