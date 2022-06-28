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
    public class ObjetGenericModel : ViewModelBase
    {

        public int IdObjetg { get; set; }
        private string libelle;
        private string compte;

        private int idLangue;
        private int idSite;
        private LangueModel langue;

     

        Facturation DAL = null;

        public ObjetGenericModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            IdObjetg = 0;
        }

      

     

        #region PROPERTIES


        public string Compte
        {
            get { return compte; }
            set { compte = value;
            this.OnPropertyChanged("Compte");
            }
        }

        public LangueModel Langue
        {
            get { return langue; }
            set { langue = value; }
        }

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value;
            this.OnPropertyChanged("Libelle");
            }
        }

        public int IdLangue
        {
            get { return idLangue; }
            set { idLangue = value;
            this.OnPropertyChanged("IdLangue");
            }
        }

        public int IdSite
        {
            get { return idSite; }
            set { idSite = value;
            this.OnPropertyChanged("IdSite");
            }
        }
        #endregion

        #region METHODS


        public ObservableCollection<ObjetGenericModel> OBJECT_GENERIC_GETLISTE(Int32 idSite)
        {
            ObservableCollection<ObjetGenericModel> objets = new ObservableCollection<ObjetGenericModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetGenerique > obj = DAL.GetAll_OBJET_GENERIQUEBYSITE(idSite);
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {
                        LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);
                        ObjetGenericModel fmodel = convertTo(exp);
                        fmodel.Langue  = newl;
                        objets.Add(fmodel);

                    }
                }
                return objets;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public ObservableCollection<ObjetGenericModel> OBJECT_GENERIC_GETLISTE_Archive(Int32 idSite)
        {
            ObservableCollection<ObjetGenericModel> objets = new ObservableCollection<ObjetGenericModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetGenerique> obj = DAL.GetAll_OBJET_GENERIQUEBYSITE_Archive(idSite);
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {
                        LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);
                        ObjetGenericModel fmodel = convertTo(exp);
                        fmodel.Langue = newl;
                        objets.Add(fmodel);

                    }
                }
                return objets;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<ObjetGenericModel> GetAll_OBJET_GENERIQUEBY_NON_Client(Int32 idClient, Int32 idSite, Int32 idlangue)
        {
            ObservableCollection<ObjetGenericModel> objets = new ObservableCollection<ObjetGenericModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetGenerique> obj = DAL.GetAll_OBJET_GENERIQUEBY_NON_Client(idClient, idSite, idlangue);
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {
                        // LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);
                        ObjetGenericModel fmodel = convertTo(exp);
                        // fmodel.Langue = newl;
                        objets.Add(fmodel);
                    }
                }
                return objets;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<ObjetGenericModel> OBJECT_GENERIC_BYLANGUE(Int32 idSite,Int32 idLangue)
        {
            ObservableCollection<ObjetGenericModel> objets = new ObservableCollection<ObjetGenericModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetGenerique> obj = DAL.GetAll_OBJET_GENERIQUELangue(idSite, idLangue);
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {
                        LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);
                        ObjetGenericModel fmodel = convertTo(exp);
                        fmodel.Langue = newl;
                        objets.Add(fmodel);
                    }
                }
                return objets;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public ObservableCollection<ObjetGenericModel> OBJECT_GENERIC_BYLANGUE_Archive(Int32 idSite, Int32 idLangue)
        {
            ObservableCollection<ObjetGenericModel> objets = new ObservableCollection<ObjetGenericModel>();
            LangueModel llangue = new LangueModel();
            try
            {
                List<ObjetGenerique> obj = DAL.GetAll_OBJET_GENERIQUELangue_Archive(idSite, idLangue);
                if (obj.Count > 0)
                {
                    foreach (var exp in obj)
                    {
                        LangueModel newl = llangue.LANGUE_SELECTBYID(exp.IdLangue);
                        ObjetGenericModel fmodel = convertTo(exp);
                        fmodel.Langue = newl;
                        objets.Add(fmodel);
                    }
                }
                return objets;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObjetGenericModel OBJECT_GENERIC_BYID(int id)
        {
            ObjetGenericModel objet = null;
            LangueModel llangue = new LangueModel();

            try
            {
                ObjetGenerique  exp = DAL.GetAll_OBJET_GENERIQUEBYID (id);

                if (exp != null)
                {
                    objet = convertTo(exp);
                    LangueModel newl = llangue.LANGUE_SELECTBYID(objet.IdLangue);
                    //facture.Client = ConvertFromClient(exp.Client);
                    objet.Langue  = newl;
                }

                return objet;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObjetGenericModel OBJECT_GENERIC_BYID_Archive(int id)
        {
            ObjetGenericModel objet = null;
            LangueModel llangue = new LangueModel();

            try
            {
                ObjetGenerique exp = DAL.GetAll_OBJET_GENERIQUEBYID_Archive(id);

                if (exp != null)
                {
                    objet = convertTo(exp);
                    LangueModel newl = llangue.LANGUE_SELECTBYID(objet.IdLangue);
                    //facture.Client = ConvertFromClient(exp.Client);
                    objet.Langue = newl;
                }

                return objet;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public DataTable OBJECT_GENERIC_BY_ID(int id, bool isArchive)
        {
           return  DAL.GetAll_OBJET_GENERIQUEBYID (id ,true );
        }

        public bool OBJECT_GENERIC_ADD(ObjetGenericModel  objet)
        {

            try
            {
                if (objet != null)
                    DAL.OBJET_GENERIQUE_ADD(convertFrom(objet));

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool OBJECT_GENERIC_DELETE(int id)
        {

            try
            {
                DAL.OBJET_GEERIQUE_DELETE (id);

                return true;

            }
            catch (Exception de)
            {
                throw new DALException(de.Message);
            }
        }


        #endregion

        #region BUSNESS METHODS

        ObjetGenericModel convertTo(ObjetGenerique obj)
        {
            ObjetGenericModel newobjet = new ObjetGenericModel 
            { 
                 IdObjetg =obj .IdObjetg , 
                 IdLangue =obj.IdLangue , 
                 IdSite =obj .IdSite, 
                 Libelle =obj .Libelle, 
                 Compte=obj.Compte  
            };
            return newobjet;
        }

        ObjetGenerique convertFrom(ObjetGenericModel obj)
        {
            ObjetGenerique newobjet = new ObjetGenerique
            {
                IdObjetg = obj.IdObjetg,
                IdLangue = obj.IdLangue,
                IdSite = obj.IdSite,
                Libelle = obj.Libelle, 
                Compte=obj.Compte
            };
            return newobjet;
        }


        #endregion

    }
}
