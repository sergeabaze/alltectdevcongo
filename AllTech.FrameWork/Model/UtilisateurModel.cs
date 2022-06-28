using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using System.Collections.ObjectModel;
using FACTURATION_DAL;
using FACTURATION_DAL.Model ;
using System.ComponentModel;

namespace AllTech.FrameWork.Model
{
    public class UtilisateurModel : ViewModelBase
    {

        public int Id{get ;set; }
        public bool IsLockCount { get; set; }
        private string nom;
        private string prenom;
        private string fonction;
        private string loggin;
        private string password;
        private string  photo;
        private Int32 idProfile;
        private ProfileModel profile;
        private Int32 idSite;


        Facturation DAL=null  ;

        public UtilisateurModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            
        }

        public UtilisateurModel(int _id,string _nom,string _prenom,string _fonction)
        {
            this.Id = _id;
            this.nom = _nom;
            this.prenom = _prenom;
            this.fonction = _fonction;
            profile = null;

        }

        public UtilisateurModel(string _nom, string _prenom, string _fonction)
        {
            this.Id = 0;
            this.nom = _nom;
            this.prenom = _prenom;
            this.fonction = _fonction;

        }

        public UtilisateurModel(int _id,string _loggin, string _motPass)
        {
            this.Id = 0;
            this.loggin  = _loggin ;
            this.password  = _motPass ;
           

        }

        #region Properties

        public string  Photo
        {
            get { return photo; }
            set { photo = value;
            this.OnPropertyChanged("Photo");
            }
        }

        public Int32 IdSite
        {
            get { return idSite; }
            set { idSite = value;
            this.OnPropertyChanged("IdSite");
            }
        }


        public ProfileModel Profile
        {
            get { return profile; }
            set { profile = value;
            this.OnPropertyChanged("Profile");
            }
        }
        public string Nom
        {
            get { return nom; }
            set { nom = value;
            this.OnPropertyChanged("Nom");
            }
        }

        public Int32 IdProfile
        {
            get { return idProfile; }
            set { idProfile = value;
            this.OnPropertyChanged("IdProfile");
            }
        }

        public string Prenom
        {
            get { return prenom; }
            set
            {
                prenom = value;
                this.OnPropertyChanged("Prenom");
            }
        }

        public string Password
        {
            get { return password; }
            set { password = value;
            this.OnPropertyChanged("Password");
            }
        }

        public string Loggin
        {
            get { return loggin; }
            set { loggin = value;
            this.OnPropertyChanged("Loggin");
            }
        }
        public string Fonction
        {
            get { return fonction; }
            set { fonction = value;
            this.OnPropertyChanged("Fonction");
            }
        }
        

        #endregion

       

        #region Methods

        public ObservableCollection<UtilisateurModel> UTILISATEUR_SELECT(bool mode)
        {
            ObservableCollection<UtilisateurModel> users = new ObservableCollection<UtilisateurModel>();
            ProfileModel profileSelected = new ProfileModel();
            try
            {
               List <Utilisateur> liste= DAL.GetAllUtilisateur(mode);
               if (liste != null)
               {
                   foreach (var user in liste)
                   {
                       UtilisateurModel util = Converfrom(user);
                       util.Profile = profileSelected.GetProfileByID(util.IdProfile);
                       users.Add(util);
                   }
               }
              
                return users;
             
            }
            catch (Exception de)
            {
                return null;
                throw new Exception(de.Message);
            }
           
        }

        public ObservableCollection<UtilisateurModel> UTILISATEUR_SELECT()
        {
            ObservableCollection<UtilisateurModel> users = new ObservableCollection<UtilisateurModel>();
            ProfileModel profileSelected = new ProfileModel();
            try
            {
                List<Utilisateur> liste = DAL.GetAllUtilisateur();
                if (liste != null)
                {
                    foreach (var user in liste)
                    {


                        UtilisateurModel util = Converfrom(user);
                       
                        ProfileModel profile = new ProfileModel
                        {
                            IdProfile = user.Profile.idProfile,
                            Libelle = user.Profile.Libelle,
                            ShortName = user.Profile.ShortName
                        };

                        List<DroitModel> listemodelDroit = new List<DroitModel>();
                        foreach (Droit dr in user.Profile.ListeDroits)
                        {
                            DroitModel newdroit = Convertfromdroit(dr);
                            foreach (Droit ssd in dr.SousDroits)
                                newdroit.SousDroits.Add(Convertfromdroit(ssd));

                            listemodelDroit.Add(newdroit);
                        }

                        profile.Droit = listemodelDroit;
                        util.Profile = profile;
                       // util.Profile = profileSelected.GetProfileByID(util.IdProfile);
                        users.Add(util);
                    }
                }

                return users;

            }
            catch (Exception de)
            {
                return null;
                throw new Exception(de.Message);
            }

        }


        public UtilisateurModel UTILISATEUR_SELECTByID(int id)
        {
            UtilisateurModel users = null;
            ProfileModel profileSelected = new ProfileModel();
            try
            {
                Utilisateur userModel = DAL.GetUtilisateur_ById(id);
                if (userModel != null)
                {

                    users = Converfrom(userModel);
                      //  util.Profile = profileSelected.GetProfileByID(util.IdProfile);
                      //  users.Add(util);
                    
                }

                return users;

            }
            catch (Exception de)
            {
                return null;
                throw new Exception(de.Message);
            }

        }


        public ObservableCollection<UtilisateurModel> UTILISATEUR_SELECT_Archive(bool mode)
        {
            ObservableCollection<UtilisateurModel> users = new ObservableCollection<UtilisateurModel>();
            ProfileModel profileSelected = new ProfileModel();
            try
            {
                List<Utilisateur> liste = DAL.GetAllUtilisateur_Archive(mode);
                if (liste != null)
                {
                    foreach (var user in liste)
                    {
                        UtilisateurModel util = Converfrom(user);
                        util.Profile = profileSelected.GetProfileByID(util.IdProfile);
                        users.Add(util);
                    }
                }

                return users;

            }
            catch (Exception de)
            {
                return null;
                throw new Exception(de.Message);
            }

        }


        public UtilisateurModel UTILISATEUR_LOGGIN(string loggin,string password)
        {
            UtilisateurModel userModel = new UtilisateurModel();
            ProfileModel profileSelected = new ProfileModel();
            try
            {
                Utilisateur user = DAL.GetUtilisateurLoggin(loggin, password);
                if (user != null)
                {

                    userModel = Converfrom(user);
                    ProfileModel profile=new ProfileModel
                    { 
                        IdProfile =user.Profile .idProfile  ,
                        Libelle =user .Profile .Libelle ,
                        ShortName =user .Profile .ShortName 
                    };

                    List<DroitModel> listemodelDroit = new List<DroitModel>();
                    foreach (Droit dr in user.Profile.ListeDroits)
                    {
                        DroitModel newdroit = Convertfromdroit(dr);
                        foreach (Droit ssd in dr.SousDroits)
                            newdroit.SousDroits.Add(Convertfromdroit(ssd));

                        listemodelDroit.Add(newdroit);
                    }

                    profile.Droit = listemodelDroit;
                    userModel.Profile = profile;
                }
                return userModel;

            }
            catch (Exception de)
            {
                return null;
                throw new Exception(de.Message);
            }
        }

        public bool ExistUtilsateurLogginName(string loggin)
        {
            try
            {
                return DAL.Utilisateur_LogName(loggin);
            }
            catch (Exception de)
            {
             
                throw new Exception(de.Message);
            }
        }

        /// <summary>
        /// vérification verouillage compte
        /// </summary>
        /// <param name="loggin"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public bool ExistUtilsateurAcountLock(string loggin,string password)
        {
            try
            {
                return DAL.Utilisateur_IsAcountLock(loggin, password);
            }
            catch (Exception de)
            {

                throw new Exception(de.Message);
            }
        }


        public bool Utilsateur_LockAcount(int idUser)
        {
            try
            {
                return DAL.Utilisateur_LockAccount(idUser);
            }
            catch (Exception de)
            {

                throw new Exception(de.Message);
            }
        }

        public UtilisateurModel GetUtilisateur_newLogg(string loggin,string oldpass,string newpass)
        {
            UtilisateurModel userModel = new UtilisateurModel();
            ProfileModel profileSelected = new ProfileModel();
            try
            {
               Utilisateur user= DAL.GetUtilisateurLoggin_New(loggin, oldpass, newpass);
               if (user != null)
               {
                  userModel= Converfrom(user);
                   userModel.Profile = profileSelected.GetProfileByID(userModel.IdProfile);
               }

               return userModel;
            }
            catch (Exception de)
            {

                throw new Exception(de.Message);
            }
        }

        public Int32  UTILISATEUR_INSERT(UtilisateurModel user)
        {
            Int32 val = 0;
            try
            {
               val = DAL.UTILISATEURADD(ConvertTo(user));

               return val;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool UTILISATEUR_UPDATE(UtilisateurModel user)
        {

            try
            {
                DAL.UTILISATEURADD(ConvertTo(user));

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool UTILISATEUR_INIT_PASSWORD(int idUser,string defPass, int idAdminprofil)
        {

            try
            {
                DAL.Utilisateur_InitPassword(idUser, defPass, idAdminprofil);

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public bool UTILISATEUR_DELETE(int id)
        {

            try
            {
                DAL.UTILISATEURDELETE(id);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }
        
        #endregion

        #region BUSNESS METHOD

        UtilisateurModel Converfrom(Utilisateur user)
        {
              UtilisateurModel newUser =null ;
            if (user !=null )
             newUser = new UtilisateurModel { Id = user.IdUtilisateur,
                                                              Nom = user.Nom,
                                                              Prenom = user.Prenom,
                                                              Fonction = user.Fonction,
                                                              Loggin = user.Loggin,
                                                                 idProfile  =user .IdProfil ,
                                                              Password = user.Password , 
                                                              Photo=user .Photo , 
                                                              IsLockCount =user .EstVerouiller , 
                                                              IdSite =user .IdSite 
            };
            return newUser;

        }

        Utilisateur ConvertTo(UtilisateurModel user)
        {
            Utilisateur newUser = null;
            if (user != null)
                newUser = new Utilisateur { IdUtilisateur = user.Id,
                                            Nom = user.Nom,
                                            Fonction = user.Fonction,
                                            Prenom = user.Prenom,
                                            Loggin = user.Loggin,
                                             IdProfil =user .idProfile ,
                                            Password = user.Password ,
                                            DateConnection =DateTime .Now ,
                                            Photo =user .Photo , IdSite =user .IdSite 
                };
            return newUser;

        }


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
                IdSouVue = d.IDSousVue, 
                Developpeur=d.developpeur, ArchiveExecute=d.ArchiveExecute,
                ArchiveView=d.ArchiveView,ImportDb=d.ImportDb,ExportDB=d.ExportDB,Marge=d.Marge,
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
        #endregion


        #region Protected Methods

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
        }
        #endregion
    }
}
