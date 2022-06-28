using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL.Model;
using FACTURATION_DAL;

namespace AllTech.FrameWork.Model
{
    public class ProfileModel : ViewModelBase
    {
        public int IdProfile { get; set; }
        private string libelle;
        private string shortName;
       

      

      

        public string Libelle
        {
            get { return libelle; }
            set { libelle = value;
            this.OnPropertyChanged("Libelle");
            }
        }
        private List<DroitModel> droit = new List<DroitModel>();

        public List<DroitModel> Droit
        {
            get { return droit; }
            set { droit = value;
            this.OnPropertyChanged("Droit");
            }
        }

        public string ShortName
        {
            get { return shortName; }
            set { shortName = value;
            this.OnPropertyChanged("ShortName");
            }
        }

       
        
      

        Profile profile;
        Facturation DAL = null;
       public ProfileModel()
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
       }

       public List<ProfileModel> GetProfile()
       {
           DroitModel droitselected=new DroitModel ();
           List<ProfileModel>  listes=new List<ProfileModel> ();
           try
           {
               List<Profile> nprofil = DAL.GetUtilisateurProfile();
               if (nprofil != null)
               {
                   foreach (var p in nprofil)
                   {
                       ProfileModel prof = new ProfileModel ();

                       prof.IdProfile = p.idProfile;
                       prof.Libelle = p.Libelle;
                       prof.ShortName = p.ShortName;
                       prof.Droit = Convertfromdroit(p.ListeDroits);// droitselected.GetListdroit(p.idProfile);
                     
                       
                       listes.Add(prof);
                   }
               }
               return listes;

           }
           catch (Exception de)
           {
               return null;
               throw new Exception(de.Message);
           }
       }


       public ProfileModel GetProfileByID (int idProfile)
       {
           ProfileModel profile = null;
           try
           {
                DroitModel droitselected=new DroitModel ();

               Profile mprofile = DAL.GetUtilisateurProfileById(idProfile);
               if (mprofile != null)
                   profile = new ProfileModel { 
                       IdProfile = mprofile.idProfile,
                       Libelle = mprofile.Libelle, 
                        ShortName =mprofile.ShortName , 
                     
                       Droit = Convertfromdroit(mprofile.ListeDroits)// droitselected.GetListdroit (mprofile .idProfile )
                   };
               return profile;

           }
           catch (Exception de)
           {
               return null;
               throw new Exception(de.Message);
           }
       }

       public ProfileModel GetProfileByID(int idProfile,int idUser)
       {
           ProfileModel profile = null;
           try
           {
               DroitModel droitselected = new DroitModel();

               Profile mprofile = DAL.GetUtilisateurProfileById(idProfile, idUser);
               if (mprofile != null)
                   profile = new ProfileModel
                   {
                       IdProfile = mprofile.idProfile,
                       Libelle = mprofile.Libelle,
                       ShortName = mprofile.ShortName,

                       Droit = Convertfromdroit(mprofile.ListeDroits)//droitselected.GetListdroit(mprofile.idProfile)
                   };
               return profile;

           }
           catch (Exception de)
           {
               return null;
               throw new Exception(de.Message);
           }
       }



       public bool Profile_Add_Date(int id,int iduser, int idprofile,DateTime ? dateDebut,DateTime? datefin)
       {
           try
           {
               return DAL.ProfileUpdatedate(id, idprofile, iduser, dateDebut, datefin);
           }
           catch (Exception de)
           {

               throw new Exception(de.Message);
           }
       }

        #region Business

     List <DroitModel> Convertfromdroit(List< Droit> droits)
       {
           List<DroitModel> liste = new List<DroitModel>();
           foreach (Droit d in droits)
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
                   JvSuppression = d.JvSuppression,
                   SousDroits = Convertfromdroit_conv(d.SousDroits)
               };
               liste.Add(droit);
           }
           return liste;
       }

     List<DroitModel> Convertfromdroit_conv(List<Droit> droits)
     {
         List<DroitModel> liste = new List<DroitModel>();
         foreach (Droit d in droits)
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
             liste.Add(droit);
         }
         return liste;
     }
        #endregion
    }
}
