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
   public  class DepartementModel:ViewModelBase 
    {


        public int IdDep { get; set; }
        private string libelle;
        private string courtLibelle;
        private string autre;
        private int idSite;

      
     

        Facturation DAL = null;

        public DepartementModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region PROPERTIES

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

        public string CourtLibelle
        {
            get { return courtLibelle; }
            set { courtLibelle = value;
            this.OnPropertyChanged("CourtLibelle");
            }
        }

        public string Autre
        {
            get { return autre; }
            set { autre = value;
            this.OnPropertyChanged("Autre");
            }
        }
        #endregion

        #region METHODS
        public List<DepartementModel> Departement_SELECT(int idsite)
        {
            List<DepartementModel> deps = new List<DepartementModel>();

            try
            {

                List<Departement> depFrom = DAL.GetAll_DEPARTMENTS(idsite);
                if (depFrom != null)
                {
                    foreach (var dev in depFrom)
                        deps.Add(ConvertFrom(dev));
                }
                return deps ;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public List<DepartementModel> Departement_SELECT_Archive(int idsite)
        {
            List<DepartementModel> deps = new List<DepartementModel>();

            try
            {

                List<Departement> depFrom = DAL.GetAll_Archive_DEPARTMENTS(idsite);
                if (depFrom != null)
                {
                    foreach (var dev in depFrom)
                        deps.Add(ConvertFrom(dev));
                }
                return deps;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }


        public DepartementModel  Departemnt_SELECTById(int id)
        {
            DepartementModel currentDep = null;
            try
            {

                Departement  depfrom = DAL.Get_DEPARTMENTSByID (id);
                if (depfrom != null)
                    currentDep = ConvertFrom(depfrom);

                return currentDep;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }

        public DepartementModel Departemnt_SELECTById_Archive(int id)
        {
            DepartementModel currentDep = null;
            try
            {

                Departement depfrom = DAL.Get_Archive_DEPARTMENTSByID(id);
                if (depfrom != null)
                    currentDep = ConvertFrom(depfrom);

                return currentDep;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }

        }



        public bool Departement_ADD(DepartementModel dep)
        {

            try
            {

                DAL.DEPARTEMENT_ADD(ConverTo(dep));
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public bool Departement_DELETE(int id)
        {

            try
            {
                DAL.DEPARTEMENT_DELETE(id);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }
        #endregion

        #region BUISNESS METHODS

        DepartementModel ConvertFrom(Departement dep)
        {
            DepartementModel newdep = null;
            if (dep != null)
            {
                newdep = new DepartementModel { IdDep =dep .IdDepartement, Libelle =dep .Libelle , CourtLibelle =dep .CourtLibelle , Autre =dep .Autre, IdSite =dep .IdSite   };
            }
            return newdep;
        }

        Departement ConverTo(DepartementModel dep)
        {
            Departement newdep = null;
            if (dep != null)
            {
                newdep = new Departement { IdDepartement = dep.IdDep , Libelle = dep.Libelle, CourtLibelle = dep.CourtLibelle, Autre = dep.Autre, IdSite =dep .IdSite  };
            }
            return newdep;
        }

        #endregion
    }
}
