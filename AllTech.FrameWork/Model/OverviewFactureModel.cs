using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class OverviewFactureModel : ViewModelBase
    {
        #region Fields

     
        public int Idice { get; set; }
        private Int64 idfacture;
        private int idClient;
        public int IdStatut { get; set; }
        private int iduser;
        public string NumeroFacture { get; set; }
        public string NomClient { get; set; }
        public string statut { get; set; }
        public DateTime Dateoperation { get; set; }

        Facturation DAL = null;
        #endregion

        public OverviewFactureModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region Properties
        public Int64 Idfacture
        {
            get { return idfacture; }
            set { idfacture = value;
            this.OnPropertyChanged("Idfacture");
            }
        }

        public int IdClient
        {
            get { return idClient; }
            set { idClient = value;
            this.OnPropertyChanged("IdClient");
            }
        }

        public int Iduser
        {
            get { return iduser; }
            set { iduser = value;
            this.OnPropertyChanged("Iduser");
            }
        }
        #endregion

        #region Methods


        public List<OverviewFactureModel> GetOverview(int iduserConnect)
        {
            List<OverviewFactureModel> liste = new List<OverviewFactureModel>();
            List<OverviewFacture> listemodel = DAL.GetAll_OVERVIEW(iduserConnect);
            OverviewFactureModel ovv = null;
            foreach (OverviewFacture ov in listemodel)
            {
                ovv = new OverviewFactureModel();
                ovv.Idfacture = ov.Idfacture;
                ovv.NumeroFacture = ov.NumeroFacture;
               // ovv.IdClient = ov.IdClient;
                ovv.NomClient = ov.NomClient;
                ovv.statut = ov.statut;
                ovv.IdStatut = ov.IdStatut;
                ovv.Dateoperation = ov.Dateoperation;
                liste.Add(ovv);
            }
            return liste;
        }

        public bool OverViewADD(OverviewFactureModel ov)
        {
            OverviewFacture ovv = new OverviewFacture();
         
            ovv.Idfacture = ov.Idfacture;
            ovv.Idice = ov.Idice;
            ovv.IdClient = ov.IdClient;
            ovv.Iduser = ov.Iduser;
            if (DAL.OVERVIEW_ADD(ovv))
                return true;
            else return false;
          
        }
        #endregion
    }
}
