using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FACTURATION_DAL.Model;
using AllTech.FrameWork.PropertyChange;

namespace AllTech.FrameWork.Model
{
    public class CompteTiersModel : ViewModelBase
    {

        public int IdCompteT { get; set; }
        private string numeroCompte;
        public int IdClient { get; set; }
        public int IdSite { get; set; }

        CompteTiers dale = null;
        public CompteTiersModel()
        {
            dale = new CompteTiers(DataProviderObject.GetStringConnection);
        }

        #region Properties
        public string NumeroCompte
        {
            get { return numeroCompte; }
            set { numeroCompte = value;
            this.OnPropertyChanged("NumeroCompte");
            }
        }
        #endregion

        #region Methods

        public CompteTiersModel SelectById(int idCompte)
        {
            CompteTiersModel compte = null;
            CompteTiers cmpt = dale.SelectByid(idCompte);
            if (cmpt != null)
            {
                compte = new CompteTiersModel();
                compte.IdCompteT = cmpt.IdCompteT;
                compte.IdClient = cmpt.IdClient;
                compte.IdSite = cmpt.IdSite;
                compte.NumeroCompte = cmpt.NumeroCompte;
                      
            }
            return compte;
        }

        public List<CompteTiersModel> SelectAll(int idSite)
        {
            List<CompteTiersModel> comptes = new List<CompteTiersModel>();
            List<CompteTiers> cmpts = dale.SelectAll(idSite);
            if (cmpts != null && cmpts.Count > 0)
            {
                CompteTiersModel compte = null;
                foreach (CompteTiers cmpt in cmpts)
                {
                    compte = new CompteTiersModel();
                    compte.IdCompteT = cmpt.IdCompteT;
                    compte.IdClient = cmpt.IdClient;
                    compte.IdSite = cmpt.IdSite;
                    compte.NumeroCompte = cmpt.NumeroCompte;
                    comptes.Add(compte);

                }
            }
             
            return comptes;
        }


        public List<CompteTiersModel> SelectByclient(int idSite,int idClient)
        {
            List<CompteTiersModel> comptes = new List<CompteTiersModel>();
            List<CompteTiers> cmpts = dale.SelectByClient (idSite,idClient);
            if (cmpts != null && cmpts.Count > 0)
            {
                CompteTiersModel compte = null;
                foreach (CompteTiers cmpt in cmpts)
                {
                    compte = new CompteTiersModel();
                    compte.IdCompteT = cmpt.IdCompteT;
                    compte.IdClient = cmpt.IdClient;
                    compte.IdSite = cmpt.IdSite;
                    compte.NumeroCompte = cmpt.NumeroCompte;
                    comptes.Add(compte);

                }
            }

            return comptes;
        }

        public List<CompteTiersModel> SelectByclient_Archive(int idSite, int idClient)
        {
            List<CompteTiersModel> comptes = new List<CompteTiersModel>();
            List<CompteTiers> cmpts = dale.SelectByClient_Archive(idSite, idClient);
            if (cmpts != null && cmpts.Count > 0)
            {
                CompteTiersModel compte = null;
                foreach (CompteTiers cmpt in cmpts)
                {
                    compte = new CompteTiersModel();
                    compte.IdCompteT = cmpt.IdCompteT;
                    compte.IdClient = cmpt.IdClient;
                    compte.IdSite = cmpt.IdSite;
                    compte.NumeroCompte = cmpt.NumeroCompte;
                    comptes.Add(compte);

                }
            }

            return comptes;
        }


        public List<CompteTiersModel> SelectByExploitation(int idSite, int idClient)
        {
            List<CompteTiersModel> comptes = new List<CompteTiersModel>();
            List<CompteTiers> cmpts = dale.SelectByClient(idSite, idClient);
            if (cmpts != null && cmpts.Count > 0)
            {
                CompteTiersModel compte = null;
                foreach (CompteTiers cmpt in cmpts)
                {
                    compte = new CompteTiersModel();
                    compte.IdCompteT = cmpt.IdCompteT;
                    compte.IdClient = cmpt.IdClient;
                    compte.IdSite = cmpt.IdSite;
                    compte.NumeroCompte = cmpt.NumeroCompte;
                    comptes.Add(compte);

                }
            }

            return comptes;
        }


        public bool Insert(CompteTiersModel cmpt, int idsite)
        {
            CompteTiers compte = new CompteTiers();
            compte.IdCompteT = cmpt.IdCompteT;
            compte.IdClient = cmpt.IdClient;
            compte.IdSite = cmpt.IdSite;
            compte.NumeroCompte = cmpt.NumeroCompte;
            if (dale.Insert(compte, idsite))
                return true;
            else return false;

        }
        //

        public bool Update(CompteTiersModel cmpt)
        {
            CompteTiers compte = new CompteTiers();
            compte.IdCompteT = cmpt.IdCompteT;
            compte.IdClient = cmpt.IdClient;
            compte.IdSite = cmpt.IdSite;
            compte.NumeroCompte = cmpt.NumeroCompte;
            if (dale.Update(compte))
                return true;
            else return false;

        }

        public bool Update_Archive(CompteTiersModel cmpt)
        {
            CompteTiers compte = new CompteTiers();
            compte.IdCompteT = cmpt.IdCompteT;
            compte.IdClient = cmpt.IdClient;
            compte.IdSite = cmpt.IdSite;
            compte.NumeroCompte = cmpt.NumeroCompte;
            if (dale.Update_Archive(compte))
                return true;
            else return false;

        }

        public bool Delete(int idcompte)
        {
            if (dale.Delete(idcompte))
                return true;
            else return false;
        }

        #endregion
    }
}
