using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class DroitModel : ViewModelBase
    {
        IFacturation  DAL = null;
        public DroitModel() 
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region PROPERTIES
        public int IdUserDroits { get; set; }

        bool marge;

        public bool Marge
        {
            get { return marge; }
            set { marge = value;
            this.OnPropertyChanged("Marge");
            }
        }
      
        public bool ArchiveExecute
        {
            get
            {
                return archiveExecute;
            }
            set
            {
                archiveExecute = value;
                this.OnPropertyChanged("ArchiveExecute");
            }
        }
        public bool ArchiveView
        {
            get
            {
                return archiveView;
            }
            set
            {
                archiveView = value;
                this.OnPropertyChanged("ArchiveView");
            }
        }
        bool archiveView;
        bool archiveExecute;
        bool importDb;
        bool exportDB;
        public bool ExportDB
        {
            get
            {
                return exportDB;
            }
            set
            {
                exportDB = value;
                this.OnPropertyChanged("ExportDB");
            }
        }
        public int ID { get; set; }
      

        public bool ImportDb
        {
            get
            {
                return importDb;
            }
            set
            {
                importDb = value;
                this.OnPropertyChanged("ImportDb");
            }
        }

        private int iProfile;
        public int IProfile
        {
            get { return iProfile; }
            set { iProfile = value;
            this.OnPropertyChanged("IProfile");
            }
        }
        private int iduser;

        public int Iduser
        {
            get { return iduser; }
            set { iduser = value;
            this.OnPropertyChanged("Iduser");
            }
        }
        private int idVues;

        public int IdVues
        {
            get { return idVues; }
            set { idVues = value;
            this.OnPropertyChanged("IdVues");
            }
        }


        private int idSouVue;

        public int IdSouVue
        {
            get { return idSouVue; }
            set { idSouVue = value;
            this.OnPropertyChanged("IdSouVue");
            }
        }
        private string libelleVue;

        public string LibelleVue
        {
            get { return libelleVue; }
            set { libelleVue = value;
            this.OnPropertyChanged("LibelleVue");
            }
        }

        private string libelleSouVue;
        public string LibelleSouVue
        {
            get { return libelleSouVue; }
            set
            {
                libelleSouVue = value;
                this.OnPropertyChanged("LibelleSouVue");
            }
        }

        private bool lecture;

        public bool Lecture
        {
            get { return lecture; }
            set { lecture = value;
            this.OnPropertyChanged("Lecture");
            }
        }
        private bool ecriture;

        public bool Ecriture
        {
            get { return ecriture; }
            set { ecriture = value;
            this.OnPropertyChanged("Ecriture");
            }
        }
        private bool suppression;

        public bool Suppression
        {
            get { return suppression; }
            set { suppression = value;
            this.OnPropertyChanged("Suppression");
            }
        }
        private bool edition;

        public bool Edition
        {
            get { return edition; }
            set { edition = value;
            this.OnPropertyChanged("Edition");
            }
        }
        private bool validation;

        public bool Validation
        {
            get { return validation; }
            set { validation = value;
            this.OnPropertyChanged("Validation");
            }
        }
        private bool impression;

        public bool Impression
        {
            get { return impression; }
            set { impression = value;
            this.OnPropertyChanged("Impression");
            }
        }
        private bool super;

        public bool Super
        {
            get { return super; }
            set { super = value;
            this.OnPropertyChanged("Super");
            }
        }
        private bool testeur;

        public bool Testeur
        {
            get { return testeur; }
            set { testeur = value;
            this.OnPropertyChanged("Testeur");
            }
        }
        private bool proprietaire;

        public bool Proprietaire
        {
            get { return proprietaire; }
            set { proprietaire = value;
            this.OnPropertyChanged("Proprietaire");
            }
        }

        private bool execution;

        public bool Execution
        {
            get { return execution; }
            set
            {
                execution = value;
                this.OnPropertyChanged("Execution");
            }
        }

        private bool developpeur;

        public bool Developpeur
        {
            get { return developpeur; }
            set { developpeur = value;
            this.OnPropertyChanged("Developpeur");
            }
        }

        bool extraction;

        public bool Extraction
        {
            get { return extraction; }
            set { extraction = value;
            this.OnPropertyChanged("Extraction");
            }
        }
        bool signature;

        public bool Signature
        {
            get { return signature; }
            set { signature = value;
            this.OnPropertyChanged("Signature");
            }
        }
        bool masterUser;

        public bool MasterUser
        {
            get { return masterUser; }
            set { masterUser = value;
            this.OnPropertyChanged("MasterUser");
            }
        }


       

        private string statut;

        public string Statut
        {
            get { return statut; }
            set { statut = value;
            this.OnPropertyChanged("Statut");
            }
        }

        private bool isCheckvue;

        public bool IsCheckvue
        {
            get { return isCheckvue; }
            set { isCheckvue = value;
            this.OnPropertyChanged("IsCheckvue");
            }
        }

        private List<DroitModel> sousDroits = new List<DroitModel>();

        public List<DroitModel> SousDroits
        {
            get { return sousDroits; }
            set { sousDroits = value;
            this.OnPropertyChanged("SousDroits");
            }
        }

        public string backGround;
        public string BackGround
        {
            get { return backGround; }
            set
            {
                backGround = value;
                this.OnPropertyChanged("BackGround");
            }
        }

        bool isNewItem;

        public bool IsNewItem
        {
            get { return isNewItem; }
            set { isNewItem = value;
            this.OnPropertyChanged("IsNewItem");
            }
        }

        private bool  isEnableVue;

        public bool  IsEnableVue
        {
            get { return isEnableVue; }
            set
            {
                isEnableVue = value;
                this.OnPropertyChanged("IsEnableVue");
            }
        }

        private bool isEnableSuperVue;

        public bool IsEnableSuperVue
        {
            get { return isEnableSuperVue; }
            set
            {
                isEnableSuperVue = value;
                this.OnPropertyChanged("IsEnableSuperVue");
            }
        }

        private bool isEnableAllVue;

        public bool IsEnableAllVue
        {
            get { return isEnableAllVue; }
            set
            {
                isEnableAllVue = value;
                this.OnPropertyChanged("IsEnableAllVue");
            }
        }

        private bool isEnableExecuteVue;

        public bool IsEnableExecuteVue
        {
            get { return isEnableExecuteVue; }
            set
            {
                isEnableExecuteVue = value;
                this.OnPropertyChanged("IsEnableExecuteVue");
            }
        }
        private bool isEnableUserGlobalSiteVue;
        public bool IsEnableUserGlobalSiteVue
        {
            get { return isEnableUserGlobalSiteVue; }
            set
            {
                isEnableUserGlobalSiteVue = value;
                this.OnPropertyChanged("IsEnableUserGlobalSiteVue");
            }
        }

        private bool statutSuspension;
        private bool statutSortie;
        private bool statutSuppression;
        private bool jvpreparation;
        private bool jvSuppression;
        private bool jvLecture;
        private bool jvExport;

        public bool JvExport
        {
            get { return jvExport; }
            set { jvExport = value; this.OnPropertyChanged("JvExport"); }
        }

        public bool JvLecture
        {
            get { return jvLecture; }
            set { jvLecture = value; this.OnPropertyChanged("JvLecture"); }
        }

        public bool JvSuppression
        {
            get { return jvSuppression; }
            set { jvSuppression = value; this.OnPropertyChanged("JvSuppression"); }
        }

        public bool Jvpreparation
        {
            get { return jvpreparation; }
            set { jvpreparation = value; this.OnPropertyChanged("Jvpreparation"); }
        }


        public bool StatutSuppression
        {
            get { return statutSuppression; }
            set { statutSuppression = value; this.OnPropertyChanged("StatutSuppression"); }
        }

        public bool StatutSortie
        {
            get { return statutSortie; }
            set { statutSortie = value; this.OnPropertyChanged("StatutSortie"); }
        }

        public bool StatutSuspension
        {
            get { return statutSuspension; }
            set { statutSuspension = value; this.OnPropertyChanged("StatutSuspension"); }
        }


        #endregion


        #region METHODS

        public Dictionary<ClassvuesModel, List<ClassvuesModel>> ListeVues()
        {
            Dictionary<ClassvuesModel, List<ClassvuesModel>> listevues = new Dictionary<ClassvuesModel, List<ClassvuesModel>>();

            try
            {
                Dictionary<ClasseVues, List<ClasseVues>> newListe = DAL.GetListeVues();
                foreach (var lt in newListe)
                {
                    ClassvuesModel lm = new ClassvuesModel { IdVue =lt.Key.IdVue , Libelle =lt .Key .Libelle  };
                    List<ClassvuesModel> md = new List<ClassvuesModel>();
                    foreach (ClasseVues ve in lt.Value)
                    {
                        ClassvuesModel vee = new ClassvuesModel { IdVue =ve .IdVue , Libelle =ve .Libelle};
                        md.Add(vee);
                    }

                    listevues.Add(lm, md);
                }

                return listevues;
               
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
        }

      public List<DroitModel> GetListdroit(int idProfile)
        {
            List<DroitModel> droits = new List<DroitModel>();
            List<DroitModel> Sdroits = new List<DroitModel>();
            try
            {
                List<Droit> mdroits = DAL.Get_DROITS_PROFILE(idProfile);

                if (mdroits != null)
                {
                    foreach (Droit dr in mdroits)
                    {
                        DroitModel newDr = Convertfromdroit(dr);

                        foreach (Droit sdr in dr .SousDroits )
                            newDr.SousDroits.Add(Convertfromdroit(sdr));
                        droits.Add(newDr);
                    }
                }

                return droits;
                
            }
            catch (Exception ex)
            {
                throw new DALException(ex.Message);
            }
            
        }

      public List<DroitModel> GetListdroit(int idProfile,int idUser)
      {
          List<DroitModel> droits = new List<DroitModel>();
          List<DroitModel> Sdroits = new List<DroitModel>();
          try
          {
              List<Droit> mdroits = DAL.Get_DROITS_USER(idProfile, idUser);
              foreach (Droit dr in mdroits)
              {
                  DroitModel newDr = Convertfromdroit(dr);

                  foreach (Droit sdr in dr.SousDroits)
                      newDr.SousDroits.Add(Convertfromdroit(sdr));
                  droits.Add(newDr);
              }
             

              return droits;

          }
          catch (Exception ex)
          {
              throw new DALException(ex.Message);
          }

      }

      public bool Droit_Users_Profile_Add(DroitModel droit)
      {
          try
          {
              Droit dr = new Droit { 
                  ID = droit.ID,
                  IDutilisateur = droit.Iduser,
                  IProfile = droit.IProfile,
                  Ecriture = droit.Ecriture,
                  Edition = droit.Edition,
                  IdVues = droit.IdVues,
                  Impression = droit.Impression,
                  Lecture = droit.Lecture,
                  LibelleVue = droit.LibelleVue,
                  proprietaire = droit.Proprietaire,
                  Super = droit.Super,
                  Suppression = droit.Suppression,
                  Testeur = droit.Testeur,
                  Validation = droit.Validation ,
                  execution = droit.Execution,
                  developpeur = droit.Developpeur,
                  ArchiveExecute = droit.ArchiveExecute,
                  ArchiveView = droit.ArchiveView,
                  ImportDb = droit.ImportDb,
                  ExportDB = droit.ExportDB,
                  Marge = droit.Marge,
                  IDSousVue =droit .IdSouVue , IdUserDroits=droit.IdUserDroits,
                  LibelleSouVue =droit .LibelleSouVue , Signature=droit.Signature,
                   Extraction=droit.Extraction, MasterUser=droit.MasterUser,
                  StatutSortie = droit.StatutSortie,
                  StatutSuppression = droit.StatutSuppression,
                  StatutSuspension = droit.StatutSuspension,
                  JvExport = droit.JvExport,
                  JvLecture = droit.JvLecture,
                  Jvpreparation = droit.Jvpreparation,
                  JvSuppression = droit.JvSuppression
              };

              if (droit.Iduser == 0) DAL.DROITS_PROFILS_ADD(dr);
              else DAL.DROITS_USER_ADD(dr);

              return true;
          }
          catch (Exception ex)
          {
              throw new DALException(ex.Message);
          }
      }


      public bool Droits_Delete(int id , int idprofile,int iduser)
      {
          bool values = false;
          try
          {
              DAL.DROITS_DELETE(id, idprofile, iduser);
              values = true;
          }
          catch (Exception ex)
          {
              throw new DALException(ex.Message);
          }
          return values;
      }
        #endregion

      DroitModel Convertfromdroit(Droit d)
      {
          DroitModel droit = new DroitModel
          {
              ID = d.ID,
              IdVues = d.IdVues,
              Lecture = d.Lecture,
              Ecriture = d.Ecriture,
              Edition = d.Edition,
              Impression = d.Impression,
              LibelleVue = d.LibelleVue,
              Super = d.Super,
              Suppression = d.Suppression,
              Testeur = d.Testeur,
              Validation = d.Validation,
              IProfile = d.IProfile,
              Proprietaire = d.proprietaire,
              Iduser = d.IDutilisateur,
              Execution = d.execution,
              Developpeur = d.developpeur,
              ArchiveExecute = d.ArchiveExecute,
              ArchiveView = d.ArchiveView,
              ImportDb = d.ImportDb,
              ExportDB = d.ExportDB,
              Marge = d.Marge,
              IdSouVue = d.IDSousVue,
              IdUserDroits = d.IdUserDroits,
              LibelleSouVue = d.LibelleSouVue,
              Signature = d.Signature,
              Extraction = d.Extraction,
              MasterUser = d.MasterUser,
              StatutSortie = d.StatutSortie,
              StatutSuppression = d.StatutSuppression,
              StatutSuspension = d.StatutSuspension,
              JvExport = d.JvExport,
              JvLecture = d.JvLecture,
              Jvpreparation = d.Jvpreparation,
              JvSuppression = d.JvSuppression
          };
          return droit;
      }
    }

    public class ClassvuesModel
    {
        public int IdVue { get; set; }
        public string Libelle { get; set; }

        private List<ClassvuesModel> sousVues = new List<ClassvuesModel>();

        public List<ClassvuesModel> SousVues
        {
            get { return sousVues; }
            set { sousVues = value; }
        }
    }
}
