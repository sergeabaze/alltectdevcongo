using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using AllTech.FrameWork.Services;
using System.Windows.Input;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Region;
using System.ComponentModel;
using AllTech.FrameWork.Views;
using System.Windows;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Model;
using System.Windows.Data;
using System.Threading;
using System.Data;
using AllTech.FrameWork.Global;
using System.Windows.Controls;
using Multilingue.Resources;
using AllTech.FacturationModule.Views.Modal;


namespace AllTech.FacturationModule.ViewModel
{
   public  class DataRefUtilisateurViewModel:ViewModelBase 
    {

       #region FIELDS
        
      
       
      
     
        private ListCollectionView _defaultView;

     
        private bool isBusy;
        bool _progressBarVisibility;
        bool _isCheckedAll;
        bool isuserValide = true;
        bool isUserProfileVisible;
        bool isProfileVisible;
        private string liseteselectionner;
        private Cursor mouseCursor;
        string filtertexte;

    
        private RelayCommand newUserCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand deleteUserActiveCommand;
        private RelayCommand initPassCommand;
        private RelayCommand lockPassCommand;
        private RelayCommand refreshLockCommand;
        private RelayCommand refreshValideCommand;
        private RelayCommand closeCommand;
        private RelayCommand searchByNameCommand;

       //vues utilisateurs
        private RelayCommand addVueCommand;
        private RelayCommand updateVueCommand;
        private RelayCommand updateDroitCommand;
        private RelayCommand deleteVueCommand;
        private RelayCommand cancelVueCommand;

       //vues profile
        private RelayCommand addVueProfileCommand;
        private RelayCommand updateDroitProfileCommand;
        private RelayCommand deleteVueProfileCommand;
        private RelayCommand cancelVueProfileCommand;

      

        private ObservableCollection<UtilisateurModel> _users;
        private ObservableCollection<UtilisateurModel> cacheUsers;

    
        private UtilisateurModel _userSelected;
        private UtilisateurModel _userdroitSelected;
        private UtilisateurModel _userProfileSelected;
        private UtilisateurModel _userActiv;

       
     
        private UtilisateurModel userService;

        private DroitModel droitservice;
        Dictionary<ClassvuesModel, List<ClassvuesModel>> listeVues;
         List<DroitModel> listeVuesUsers;
         List<DroitModel> alllisteVuesUsers;
         List<DroitModel> listeSousVues;

         DroitModel sousvueSelected;
         DroitModel vueUserSelected;
         DroitModel droitUserSelected;
         DroitModel newVueSelected;

       
         List<DroitModel> droitUserItem;
     
        List<ClassvuesModel> listVuewNotChild;
        List<ClassvuesModel> cachelistVuewNotChild;
        List<ClassvuesModel> newlisteSousVues;


        ClassvuesModel vueSelected;
        ClassvuesModel newsouVueSelected;


        ProfileModel profileservice;
        ProfileModel _profileSelected;
        List<ProfileModel> _profileList;
        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;
        ProfileDateModel profileDateservice;
        ProfileDateModel profileDateSelected;

       // droits profile
        ProfileModel _profileDroitSelected;
        List<ClassvuesModel> listProfileVuewNotChild;
        List<ClassvuesModel> ListProfileVuesCache=null ;
        List<ClassvuesModel> newlisteprofileSousVues;
        ClassvuesModel vueProfileSelected;
        ClassvuesModel souVueProfileSelected;
        List<DroitModel> listeVuesProfile;
        List<DroitModel> listeProfileSousVues;
        List<DroitModel> vuewListeDroitUser;

        DroitModel vueProfileDroitSelected;
        DroitModel souVueProfileDroitSelected;
        List<DroitModel> droitprofileItem;


        DroitModel droitProfileItemSelect;

       

       bool btnNewVisible;
       bool btnSaveVisible;
       bool btnDeleteVisible;
       bool btnInitVisible;
       bool isfieldEnabled;
       bool checkingTemporaire;
       bool isSousVueCmbVisible;
       bool isSousVueProfileCmbVisible;

       bool isdroitSuperEnable;
       bool isdroitDeveloppeurEnable;
       bool isvisibleBtnCancel;

       bool isEnablelecture;
       bool isEnableEcriture;
       bool isEnableEdition;
       bool isEnableSupression;
       bool isEnableImpression;
       bool isEnableExport;
       bool isEnableMarUp;
       bool isEnablVAlidation;
       bool isEnableSignature;
       bool isEnableStatutValidation;
       bool isEnableStatutSuspension;
       bool isEnableStatutSupression;
       bool isEnableStatutSortie;

       bool isEnableJvPreparation;
       bool isEnableJvSuppression;
       bool isEnableJvLecture;
       bool isEnableJvExportSage;

       bool isEnableArchiveLecture;
       bool isEnableArchiveExecuter;

       bool isEnableAdminImport;
       bool isEnableAdminExport;
   



     
 

       bool bordeprofileVisible;

       DateTime? startDate;
       DateTime? endDate;

       string labelCompte;
       string nouveauNomImages;
       int indexeProfile;
       bool checkTemporaireEnabled;
       SocieteModel societeCourante;

       bool isoperationProfilAdd = false;
       bool isoperationUserAdd = false;

       int totalUstilisateurs;
       Window localWindow;
       public bool IsAction = false;
        #endregion



        #region CONSTRUCTOR

       public DataRefUtilisateurViewModel(Window window)
         {
           //_regionManager = regionManager;
           //_container = container;
             localWindow = window;
           ProgressBarVisibility = false;
           LabelCompte = string.Empty;
           societeCourante = GlobalDatas.DefaultCompany;
           //_injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
           userService = new UtilisateurModel();
           profileservice  = new ProfileModel();
           droitservice = new DroitModel();
           profileDateservice = new ProfileDateModel();
           ParametersDatabase = GlobalDatas.dataBasparameter;
           UserConnected = GlobalDatas.currentUser;
           if (CacheDatas.ui_currentdroitUserInterface == null)
           {
               CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("utilisateurs")) ?? new DroitModel();
               CacheDatas.ui_currentdroitUserInterface = CurrentDroit;
           }
           else CurrentDroit = CacheDatas.ui_currentdroitUserInterface;
           loadDatas();
           loadRight();
           //Utils.logUserActions(string.Format("<-- UI Utilisateur --ouverture vue   par : {0}", UserConnected.Loggin), "");
         }


       #endregion
       
        #region PROPERTIES


       #region enable droits

       public bool IsEnablVAlidation
       {
           get { return isEnablVAlidation; }
           set { isEnablVAlidation = value; OnPropertyChanged("IsEnablVAlidation"); }

       }
       public bool IsEnableAdminExport
       {
           get { return isEnableAdminExport; }
           set { isEnableAdminExport = value; OnPropertyChanged("IsEnableAdminExport"); }
       }

       public bool IsEnableAdminImport
       {
           get { return isEnableAdminImport; }
           set { isEnableAdminImport = value; OnPropertyChanged("IsEnableAdminImport"); }
       }

       public bool IsEnableArchiveExecuter
       {
           get { return isEnableArchiveExecuter; }
           set { isEnableArchiveExecuter = value; OnPropertyChanged("IsEnableArchiveExecuter"); }
       }

       public bool IsEnableArchiveLecture
       {
           get { return isEnableArchiveLecture; }
           set { isEnableArchiveLecture = value; OnPropertyChanged("IsEnableArchiveLecture"); }
       }

       public bool IsEnableJvExportSage
       {
           get { return isEnableJvExportSage; }
           set { isEnableJvExportSage = value; OnPropertyChanged("IsEnableJvExportSage"); }
       }

       public bool IsEnableJvLecture
       {
           get { return isEnableJvLecture; }
           set { isEnableJvLecture = value; OnPropertyChanged("IsEnableJvLecture"); }
       }

       public bool IsEnableJvSuppression
       {
           get { return isEnableJvSuppression; }
           set { isEnableJvSuppression = value; OnPropertyChanged("IsEnableJvSuppression"); }
       }

       public bool IsEnableJvPreparation
       {
           get { return isEnableJvPreparation; }
           set { isEnableJvPreparation = value; OnPropertyChanged("IsEnableJvPreparation"); }
       }

       public bool IsEnableStatutSortie
       {
           get { return isEnableStatutSortie; }
           set { isEnableStatutSortie = value; OnPropertyChanged("IsEnableStatutSortie"); }
       }


       public bool IsEnableImpression
       {
           get { return isEnableImpression; }
           set { isEnableImpression = value; OnPropertyChanged("IsEnableImpression"); }
       }

       public bool IsEnableStatutSupression
       {
           get { return isEnableStatutSupression; }
           set { isEnableStatutSupression = value; OnPropertyChanged("IsEnableStatutSupression"); }
       }



       public bool IsEnableStatutSuspension
       {
           get { return isEnableStatutSuspension; }
           set { isEnableStatutSuspension = value; OnPropertyChanged("IsEnableStatutSuspension"); }
       }


       public bool IsEnableStatutValidation
       {
           get { return isEnableStatutValidation; }
           set { isEnableStatutValidation = value; OnPropertyChanged("IsEnableStatutValidation"); }
       }

       public bool IsEnableSignature
       {
           get { return isEnableSignature; }
           set { isEnableSignature = value; OnPropertyChanged("IsEnableSignature"); }
       }

       public bool IsEnableMarUp
       {
           get { return isEnableMarUp; }
           set { isEnableMarUp = value; OnPropertyChanged("IsEnableMarUp"); }
       }



       public bool IsEnableExport
       {
           get { return isEnableExport; }
           set { isEnableExport = value; OnPropertyChanged("IsEnableExport"); }
       }

       public bool IsEnableSupression
       {
           get { return isEnableSupression; }
           set { isEnableSupression = value; OnPropertyChanged("IsEnableSupression"); }
       }

       public bool IsEnableEdition
       {
           get { return isEnableEdition; }
           set { isEnableEdition = value; OnPropertyChanged("IsEnableEdition"); }
       }

       public bool IsEnableEcriture
       {
           get { return isEnableEcriture; }
           set { isEnableEcriture = value; OnPropertyChanged("IsEnableEcriture"); }
       }

       public bool IsEnablelecture
       {
           get { return isEnablelecture; }
           set { isEnablelecture = value; OnPropertyChanged("IsEnablelecture"); }
       }
       #endregion

        #region DRoits profile

       public bool IsdroitSuperEnable
       {
           get { return isdroitSuperEnable; }
           set { isdroitSuperEnable = value;
           OnPropertyChanged("IsdroitSuperEnable");
           }
       }


       public bool IsdroitDeveloppeurEnable
       {
           get { return isdroitDeveloppeurEnable; }
           set { isdroitDeveloppeurEnable = value;
           OnPropertyChanged("IsdroitDeveloppeurEnable");
           }
       }

       public List<DroitModel> DroitprofileItem
        {
            get { return droitprofileItem; }
            set { droitprofileItem = value;
            OnPropertyChanged("DroitprofileItem");
            }
        }

        public DroitModel DroitProfileItemSelect
        {
            get { return droitProfileItemSelect; }
            set { droitProfileItemSelect = value;
            OnPropertyChanged("DroitProfileItemSelect");
            }
        }


        public ProfileModel ProfileDroitSelected
        {
            get { return _profileDroitSelected; }
            set { _profileDroitSelected = value;
            if (value != null)
            {
                //ListProfileVuewNotChild = null;
                NewlisteprofileSousVues = null;
                IsSousVueProfileCmbVisible = false;
                ListeVuesProfile = null;
                ListeProfileSousVues = null;
                DroitprofileItem = null;
                NewDroitprofileItem = null;
              
                if (LoadVuesByProfileSelected(value))
                    ListeVuesProfile = value.Droit;
                
            }

            OnPropertyChanged("ProfileDroitSelected");
            }
        }


        public List<ClassvuesModel> ListProfileVuewNotChild
        {
            get { return listProfileVuewNotChild; }
            set { listProfileVuewNotChild = value;
            OnPropertyChanged("ListProfileVuewNotChild");
            }
        }


        public List<ClassvuesModel> NewlisteprofileSousVues
        {
            get { return newlisteprofileSousVues; }
            set { newlisteprofileSousVues = value;
            OnPropertyChanged("NewlisteprofileSousVues");
            }
        }

        public ClassvuesModel VueProfileSelected
        {
            get { return vueProfileSelected; }
            set { vueProfileSelected = value;

            if (value != null)
            {
                if (value.SousVues !=null && value.SousVues.Count > 0)
                {
                    NewlisteprofileSousVues = value.SousVues;
                    IsSousVueProfileCmbVisible = true;
                    isoperationProfilAdd = true;
                }
                else
                {
                    NewlisteprofileSousVues = null;
                    SouVueProfileSelected = null;
                    IsSousVueProfileCmbVisible = false;
                    isoperationProfilAdd = true;
                }
            }
            OnPropertyChanged("VueProfileSelected");
            }
        }


        public ClassvuesModel SouVueProfileSelected
        {
            get { return souVueProfileSelected; }
            set { souVueProfileSelected = value;
            isoperationProfilAdd = true;
            OnPropertyChanged("SouVueProfileSelected");
            }
        }

        public List<DroitModel> ListeVuesProfile
        {
            get { return listeVuesProfile; }
            set { listeVuesProfile = value;
            OnPropertyChanged("ListeVuesProfile");
            }
        }

        DroitModel newDroitprofileItem;

        public DroitModel NewDroitprofileItem
        {
            get { return newDroitprofileItem; }
            set { newDroitprofileItem = value;
            OnPropertyChanged("NewDroitprofileItem");
            }
        }
        //ListeProfileSousVues
        public DroitModel VueProfileDroitSelected
        {
            get { return vueProfileDroitSelected; }
            set { vueProfileDroitSelected = value;
            if (value != null)
            {
                if (value.SousDroits != null)
                {
                    if (value.SousDroits.Count == 0)
                    {
                        DroitprofileItem = null;
                        NewDroitprofileItem = null;
                       // List<DroitModel> items = new List<DroitModel>();
                        NewDroitprofileItem = value;
                       // items.Add(value);
                      //  DroitprofileItem = items;

                        ListeProfileSousVues = null;
                        if (CurrentDroit != null)
                        {
                            if (NewDroitprofileItem==null )
                                loadForms(value.IdVues);
                            else loadForms(NewDroitprofileItem.IdVues);
                           
                        }

                    }
                    else
                    {
                        NewDroitprofileItem = null;
                        ListeProfileSousVues = null;
                        ListeProfileSousVues = value.SousDroits;
                    }
                }

            }

            OnPropertyChanged("VueProfileDroitSelected");
            }
        }

        public List<DroitModel> ListeProfileSousVues
        {
            get { return listeProfileSousVues; }
            set { listeProfileSousVues = value;
            OnPropertyChanged("ListeProfileSousVues");
            }
        }

      
        public DroitModel SouVueProfileDroitSelected
        {
            get { return souVueProfileDroitSelected; }
            set { souVueProfileDroitSelected = value;
            NewDroitprofileItem = null;

            NewDroitprofileItem = value;

            if (CurrentDroit != null)
            {
                
                    if (NewDroitprofileItem != null)
                    {
                            if (VueProfileDroitSelected!=null )
                                loadForms(value.IdSouVue);
                    }
                
            }
            OnPropertyChanged("SouVueProfileDroitSelected");
            }
        }

        #endregion

        #region REGION VISIBLE ENABLE


        public bool IsProfileVisible
        {
            get { return isProfileVisible; }
            set
            {
                isProfileVisible = value;
                OnPropertyChanged("IsProfileVisible");
            }
        }
        public bool IsUserProfileVisible
        {
            get { return isUserProfileVisible; }
            set
            {
                isUserProfileVisible = value;
                OnPropertyChanged("IsUserProfileVisible");
            }
        }

        public bool IsSousVueProfileCmbVisible
        {
            get { return isSousVueProfileCmbVisible; }
            set { isSousVueProfileCmbVisible = value;
            OnPropertyChanged("IsSousVueProfileCmbVisible");
            }
        }

        public bool IsSousVueCmbVisible
        {
            get { return isSousVueCmbVisible; }
            set { isSousVueCmbVisible = value;
            OnPropertyChanged("IsSousVueCmbVisible");
            }
        }

        #endregion

        #region DROITS USERS

        public List<DroitModel> VuewListeDroitUser
        {
            get { return vuewListeDroitUser; }
            set { vuewListeDroitUser = value;
               // value[0].SousDroits
            OnPropertyChanged("VuewListeDroitUser");
            }
        }
     

        public DroitModel SousvueSelected
        {
            get { return sousvueSelected; }
            set { sousvueSelected = value ;
            //List<DroitModel> items = new List<DroitModel>();
            //items.Add(value);
            //DroitUserItem = items;
            NewDroitprofileItem = value;

            if (CurrentDroit != null)
            {
                //if (CurrentDroit.Super)
                //{
                if (VueUserSelected != null)
                {
                    if (NewDroitprofileItem != null)
                    {
                        loadForms(NewDroitprofileItem.IdSouVue);
                    }
                }
                //    {
                //      
               // }
            }
            OnPropertyChanged("SousvueSelected");
            }
        }



        public List<DroitModel> ListeSousVues
        {
            get { return listeSousVues; }
            set { listeSousVues = value;
            OnPropertyChanged("ListeSousVues");
            }
        }

        public ClassvuesModel NewSouVueSelected
        {
            get { return newsouVueSelected; }
            set { newsouVueSelected = value;
            isoperationUserAdd = true;
            OnPropertyChanged("NewSouVueSelected");
            
            }
        }


        public List<ClassvuesModel> NewlisteSousVues
        {
            get { return newlisteSousVues; }
            set { newlisteSousVues = value;
            OnPropertyChanged("NewlisteSousVues");
            
            }
        }


        public DroitModel NewVueSelected
        {
            get { return newVueSelected; }
            set { newVueSelected = value;
            OnPropertyChanged("NewVueSelected");
            }
        }

        public List<DroitModel> DroitUserItem
        {
            get { return droitUserItem; }
            set { droitUserItem = value;
            OnPropertyChanged("DroitUserItem");
            }
        }

        public DroitModel DroitUserSelected
        {
            get { return droitUserSelected; }
            set { droitUserSelected = value;
            OnPropertyChanged("DroitUserSelected");
            }
        }
       
        public DroitModel VueUserSelected
        {
            get { return vueUserSelected; }
            set { vueUserSelected = value;
            if (value != null)
            {
                ListeSousVues = null;

                if (value.SousDroits.Count == 0)
                {
                    //List<DroitModel> items = new List<DroitModel>();
                    ////value.Iduser = UserdroitSelected.Id;
                    //items.Add(value);
                    //DroitUserItem = items;
                    NewDroitprofileItem = value;

                    if (CurrentDroit != null)
                        {
                            if (NewDroitprofileItem != null)
                            loadForms(NewDroitprofileItem.IdVues);
                           // if (NewDroitprofileItem != null)
                           // {
                                //if (CurrentDroit.Super)
                                //{
                               
                                //}
                            //}
                        }
                }
                else
                {
                    NewDroitprofileItem = null;
                    ListeSousVues = value.SousDroits;
                    foreach (DroitModel dr in ListeSousVues)
                    {
                        if (dr.Iduser == 0)
                            dr.Statut = "Profile";
                        else dr.Statut = "Utilisateur";
                    }

                }
            }
            OnPropertyChanged("VueUserSelected");
            }
        }

        public  List<DroitModel> ListeVuesUsers
        {
            get { return listeVuesUsers; }
            set { listeVuesUsers = value;
            OnPropertyChanged("ListeVuesUsers");
            }
        }

        public List<DroitModel> AlllisteVuesUsers
        {
            get { return alllisteVuesUsers; }
            set { alllisteVuesUsers = value;
            OnPropertyChanged("AlllisteVuesUsers");
            }
        }
       
        public UtilisateurModel UserProfileSelected
        {
            get { return _userProfileSelected; }
            set { _userProfileSelected = value;
            OnPropertyChanged("UserProfileSelected");
            }
        }

       
        public UtilisateurModel UserdroitSelected
        {
            get { return _userdroitSelected; }
            set { _userdroitSelected = value;
            IsSousVueCmbVisible = false;
            DroitUserItem = null;
            ListeSousVues = null;

           // List<DroitModel> newliste = droitservice.GetListdroit(value.IdProfile, value.Id);

            foreach (DroitModel dr in UserdroitSelected.Profile.Droit)
            {
                if (dr.Iduser == 0)
                    dr.Statut = "Profile";
                else dr.Statut = "Utilisateur";
            }

            LoadUserVues(value);
            ListeVuesUsers = value.Profile.Droit;
            

            //if (CachelistVuewNotChild != null)
            //{
            //    List<ClassvuesModel> newCache = new List<ClassvuesModel>();
            //    foreach (var cls in CachelistVuewNotChild)
            //        newCache.Add(cls );

            //    foreach (var  vs in CachelistVuewNotChild)
            //    {
            //        if (vs.SousVues.Count == 0)
            //        {
            //            if (ListeVuesUsers.Exists(v => v.IdVues == vs.IdVue))
            //                newCache.Remove(newCache.Find(vv => vv.IdVue == vs.IdVue));
            //        }
            //        else
            //        {
            //            var vueSelect = ListeVuesUsers.Find(v => v.IdVues == vs.IdVue);
            //            if (vueSelect != null)
            //            {
            //                List<DroitModel> sousDroits = vueSelect.SousDroits;

            //                var  sousVuest = newCache.Find(sv => sv.IdVue == vs.IdVue);
            //                if (sousVuest != null)
            //                {
            //                    List<ClassvuesModel> sousvues = sousVuest.SousVues;
            //                    ClassvuesModel newVues = newCache.Find(sv => sv.IdVue == vs.IdVue);
            //                    newCache.Remove(newVues);
            //                    foreach (DroitModel dr in sousDroits)
            //                    {
            //                        newVues.SousVues.Remove(newVues.SousVues.Find(sse => sse.IdVue == dr.IdSouVue));
            //                    }
            //                    newCache.Add(newVues);
            //                }
            //            }

            //        }
            //    }
               // ListVuewNotChild = null;
               // ListVuewNotChild = newCache;
           // }
            OnPropertyChanged("UserdroitSelected");
            }
        }

        public List<ClassvuesModel> ListVuewNotChild
        {
            get { return listVuewNotChild; }
            set { listVuewNotChild = value;
            OnPropertyChanged("ListVuewNotChild");

            }
        }

        public List<ClassvuesModel> CachelistVuewNotChild
        {
            get { return cachelistVuewNotChild; }
            set { cachelistVuewNotChild = value;
            OnPropertyChanged("CachelistVuewNotChild");
            }
        }

        public ClassvuesModel VueSelected
        {
            get { return vueSelected; }
            set { vueSelected = value ;
            if (value != null)
            {
                if (value.SousVues != null)
                {
                    if (value.SousVues.Count > 0)
                    {
                        NewlisteSousVues = value.SousVues;
                        IsSousVueCmbVisible = true;
                        isoperationUserAdd = true;
                    }
                    else
                    {
                        IsSousVueCmbVisible = false;
                        NewlisteSousVues = null;
                        NewVueSelected = null;
                        isoperationUserAdd = true;
                    }
                }
            }
                

            OnPropertyChanged("VueSelected");
            }
        }

        #endregion

        #region USER CREATION

        public UtilisateurModel UserActiv
        {
            get { return _userActiv; }
            set { _userActiv = value;
            OnPropertyChanged("UserActiv");
            }
        }



        public bool IsvisibleBtnCancel
        {
            get { return isvisibleBtnCancel; }
            set
            {
                isvisibleBtnCancel = value;
                OnPropertyChanged("IsvisibleBtnCancel");
            }
        }
        public int TotalUstilisateurs
        {
            get { return totalUstilisateurs; }
            set { totalUstilisateurs = value;
            OnPropertyChanged("TotalUstilisateurs");
            }
        }

     

        public bool CheckTemporaireEnabled
        {
            get { return checkTemporaireEnabled; }
            set { checkTemporaireEnabled = value;
            OnPropertyChanged("CheckTemporaireEnabled");
            }
        }

        public bool CheckingTemporaire
        {
            get { return checkingTemporaire; }
            set { checkingTemporaire = value;
            if (value)
            {

                if (ProfileDateSelected != null)
                {
                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                    //messageBox.Owner = localWindow ;
                    messageBox.Title = "Information ";
                    messageBox.ViewModel.Message = "Ce Compte Est temporaraire Voulez vous l'annuler";
                    if (messageBox.ShowDialog().Value == true)
                    {
                        profileservice.Profile_Add_Date(-1, UserSelected.Id, ProfileSelected.IdProfile, StartDate, EndDate);
                        ProfileDateSelected = null;
                        BordeprofileVisible = false;
                        CheckTemporaireEnabled = false;
                        LabelCompte = "";
                        CheckingTemporaire = false;
                        StartDate = null;
                        EndDate = null;
                    }
                    else
                        BordeprofileVisible = true;
                }
                else
                {
                    BordeprofileVisible = true ;
                }
            }
            OnPropertyChanged("CheckingTemporaire");
            }
        }
        public DateTime? StartDate
        {
            get { return startDate; }
            set { startDate = value;
            OnPropertyChanged("StartDate");
            }
        }

        public DateTime? EndDate
        {
            get { return endDate; }
            set { endDate = value;
            OnPropertyChanged("EndDate");
            }
        }
       


        public ProfileDateModel ProfileDateSelected
        {
            get { return profileDateSelected; }
            set { profileDateSelected = value;
            OnPropertyChanged("ProfileDateSelected");
            }
        }

        public int IndexeProfile
        {
            get { return indexeProfile; }
            set { indexeProfile = value;
            OnPropertyChanged("IndexeProfile");
            }
        }

        public string LabelCompte
        {
            get { return labelCompte; }
            set { labelCompte = value;
            OnPropertyChanged("LabelCompte");
            }
        }

        public string NouveauNomImages
        {
            get { return nouveauNomImages; }
            set { nouveauNomImages = value;
            OnPropertyChanged("NouveauNomImages");
            }
        }

        public bool BordeprofileVisible
        {
            get { return bordeprofileVisible; }
            set { bordeprofileVisible = value;
            OnPropertyChanged("BordeprofileVisible");
            }
        }


        public string Liseteselectionner
        {
            get { return liseteselectionner; }
            set { liseteselectionner = value;
            OnPropertyChanged("Liseteselectionner");
            }
        }

       public bool BtnNewVisible
        {
            get { return btnNewVisible; }
            set { btnNewVisible = value;
            OnPropertyChanged ("BtnNewVisible");}
        }
        public bool BtnSaveVisible
        {
            get { return btnSaveVisible; }
            set { btnSaveVisible = value;
            OnPropertyChanged ("BtnSaveVisible");}
        }
        public bool BtnDeleteVisible
        {
            get { return btnDeleteVisible; }
            set { btnDeleteVisible = value;
            OnPropertyChanged ("BtnDeleteVisible");}
        }
        public bool BtnInitVisible
        {
            get { return btnInitVisible; }
            set { btnInitVisible = value;
            OnPropertyChanged ("BtnInitVisible");}
        }

        public bool IsfieldEnabled
        {
            get { return isfieldEnabled; }
            set { isfieldEnabled = value;
             OnPropertyChanged ("IsfieldEnabled");
            }
        }


        public DroitModel CurrentDroit
        {
            get { return _currentDroit; }
            set { _currentDroit = value;
            OnPropertyChanged("CurrentDroit");
            }
        }

                public ParametresModel ParametersDatabase
                {
                  get { return _parametersDatabase; }
                  set { _parametersDatabase = value;
                  OnPropertyChanged("ParametersDatabase");
                  }
                }

        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set { userConnected = value ;
            OnPropertyChanged("UserConnected");
            }
        }

      

        public string ViewName
        {
            get { return "Utilisateurs"; }
        }

        public bool IsBusy
        {
            get
            {
                return this.isBusy;
            }
            private set
            {
                this.isBusy = value;
                this.OnPropertyChanged("IsBusy");
            }
        }

        public Cursor MouseCursor
        {
            get
            {
                return this.mouseCursor;
            }
            private set
            {
                this.mouseCursor = value;
                this.OnPropertyChanged("MouseCursor");
            }
        }

        public bool ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }

        public bool IsCheckedAll
        {
            get { return _isCheckedAll; }
            set { _isCheckedAll = value;
            OnPropertyChanged("IsCheckedAll");
            }
        }
        public string Filtertexte
        {
            get { return filtertexte; }
            set
            {
                filtertexte = value;
                if ( value != string.Empty)
                {
                    IsvisibleBtnCancel = true;
                }
                else IsvisibleBtnCancel = false;
                //filtertexte = value;
                // if (value != null || value != string.Empty)
                // filter(value);
               // DefaultView.SortDescriptions.Add(new SortDescription("Id", ListSortDirection.Ascending));
                //if (DefaultView.CurrentItem != null) WindowTitle = ((Order)DefaultView.CurrentItem).Name;
                this.OnPropertyChanged("Filtertexte");
            }
        }

        public ListCollectionView DefaultView
        {
            get { return _defaultView; }
            set { _defaultView = value;
            this.OnPropertyChanged("DefaultView");
            }
        }

        public ObservableCollection<UtilisateurModel> Users
        {
            get { return _users; }
            set { _users = value;
            this.OnPropertyChanged("Users");
            }
        }

        public UtilisateurModel UserSelected
        {
            get { return _userSelected; }
            set { _userSelected = value ;
         

            if (value != null)
            {
                IsfieldEnabled = true;
                LabelCompte = string.Empty;
                CheckTemporaireEnabled = true;
                if (value.Photo != string.Empty)
                {
                    if (value.Id > 0)
                    {
                        if (value.Photo.IndexOfAny(new char[] { '\\' }) == -1)
                            value.Photo = CommonModule.GetImage(value.Photo);
                    }
                
                }
               
                if (value.Id > 0)
                {
                    if (value.IdProfile >0)
                    {
                        ////value.
                        ProfileDateSelected = profileDateservice.GetProfileDate(UserSelected.Id, value.IdProfile);
                        if (ProfileDateSelected != null)
                        {
                            if (UserSelected.IsLockCount)
                            {
                                LabelCompte = "Ce Compte est Vérouillé";
                            }
                            else
                            {
                                TimeSpan differenceDate =(DateTime) ProfileDateSelected.Datefin - DateTime.Parse(DateTime.Now.ToShortDateString());
                                if (differenceDate.TotalDays == 0)
                                {
                                    
                                    LabelCompte = "Dernier jour de Validitier";
                                }
                                else if(differenceDate.TotalDays < 0)
                                {
                                    LabelCompte = "Ce Compte est Expiré et sera Vérouillé et sera verouillé";
                                    userService.Utilsateur_LockAcount(UserSelected.Id);
                                    UserSelected = null;
                                    ProfileSelected = null;
                                    IsfieldEnabled = false;
                                    CheckTemporaireEnabled = false;
                                    Users = userService.UTILISATEUR_SELECT(false);
                                }
                                else LabelCompte = string.Format("Jours De Validité Restant  : {0}", differenceDate.TotalDays);

                            }
                                 

                                  StartDate = ProfileDateSelected.Datedebut;
                                  EndDate = ProfileDateSelected.Datefin;
                                  BordeprofileVisible = true;
                        }
                        else
                        {
                            StartDate = null;
                            EndDate = null;
                            BordeprofileVisible = false ;
                        }

                       
                    }
                }
                IndexeProfile = ProfileList.FindIndex(p => p.IdProfile == value.IdProfile);
                       

            }
            this.OnPropertyChanged("UserSelected");
            }
        }


        public ProfileModel ProfileSelected
        {
            get { return _profileSelected; }
            set { _profileSelected = value;

            if (value != null)
            {
                if (UserSelected != null)
                {
                    UserSelected.IdProfile = value.IdProfile;
                //    if (value.ShortName.ToLower().Contains("it"))
                //    {
                //        if (isuserValide)
                //            userService.Utilsateur_LockAcount(UserSelected.Id);
                //    }
                //    else { BordeprofileVisible = false; LabelCompte = ""; };
                }
                //Users = CacheUsers;
           }
            this.OnPropertyChanged("ProfileSelected");
            }
        }


        public List<ProfileModel> ProfileList
        {
            get { return _profileList; }
            set { _profileList = value;
            this.OnPropertyChanged("ProfileList");
            }
        }

        public ObservableCollection<UtilisateurModel> CacheUsers
        {
            get { return cacheUsers; }
            set { cacheUsers = value;
            this.OnPropertyChanged("CacheUsers");
            }
        }

        #endregion
        #endregion

        #region ICOMMAND

        #region USER


      // 

        public ICommand SearchByNameCommand
        {
            get
            {
                if (this.searchByNameCommand == null)
                {
                    this.searchByNameCommand = new RelayCommand(param => this.canSearchUser(), param => this.canExecuteSearchUser());
                }
                return this.searchByNameCommand;
            }
        }


        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(param => this.canSaveUser(), param => this.canExecuteSaveUser());
                }
                return this.saveCommand;
            }
        }
        

        public ICommand NewUserCommand
        {
            get
            {
                if (this.newUserCommand == null)
                {
                    this.newUserCommand = new RelayCommand(param => this.canNewuser());
                }
                return this.newUserCommand;
            }
        }

        //public ICommand NewCommand
        //{
        //    get
        //    {
        //        if (this.newCommand == null)
        //        {
        //            this.newCommand = new RelayCommand(param => this.canNewProduit());
        //        }
        //        return this.newCommand;
        //    }
        //}

        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(param => this.canDeleteUser(), param => this.canExecuteDeleteUser());
                }
                return this.deleteCommand;
            }


        }

        public ICommand DeleteUserActiveCommand
        {
            get
            {
                if (this.deleteUserActiveCommand == null)
                {
                    this.deleteUserActiveCommand = new RelayCommand(param => this.canDeleteUserActif(), param => this.canExecuteDeleteUserActif());
                }
                return this.deleteUserActiveCommand;
            }


        }

        //
        public RelayCommand CloseCommand
        {
            get
            {
                if (this.closeCommand == null)
                {
                    this.closeCommand = new RelayCommand(param => this.canClose());
                }
                return this.closeCommand;
            }

        }

        public ICommand InitPassCommand
        {
            get
            {
                if (this.initPassCommand == null)
                {
                    this.initPassCommand = new RelayCommand(param => this.canInitPass(param), param => this.canExecuteInitPassd());
                }
                return this.initPassCommand;
            }


        }

        public ICommand LockPassCommand
        {
            get
            {
                if (this.lockPassCommand == null)
                {
                    this.lockPassCommand = new RelayCommand(param => this.canLockPass(), param => this.canExecuteLockPassd());
                }
                return this.lockPassCommand;
            }


        }


        public ICommand RefreshLockCommand
        {
            get
            {
                if (this.refreshLockCommand == null)
                {
                    this.refreshLockCommand = new RelayCommand(param => this.canRefreshLockListUser(), param => this.canExecuteRefreshLockist());
                }
                return this.refreshLockCommand;
            }


        }

        public ICommand RefreshValideCommand
        {
            get
            {
                if (this.refreshValideCommand == null)
                {
                    this.refreshValideCommand = new RelayCommand(param => this.canRefreshValideListUser(), param => this.canExecuteRefreshValideList());
                }
                return this.refreshValideCommand;
            }


        }
        #endregion

        #region USER VUES



        public RelayCommand AddVueCommand
        {
            get
            {
                if (this.addVueCommand == null)
                {
                    this.addVueCommand = new RelayCommand(param => this.canAddvue(), param => this.canExecuteAddVue());
                }
                return this.addVueCommand;
            }
        }

        public RelayCommand UpdateVueCommand
        {
            get
            {
                if (this.updateVueCommand == null)
                {
                    this.updateVueCommand = new RelayCommand(param => this.canUpdateVue(), param => this.canExecuteUpdateVue());
                }
                return this.updateVueCommand;
            }
        }


        public RelayCommand UpdateDroitCommand
        {
            get
            {
                if (this.updateDroitCommand == null)
                {
                    this.updateDroitCommand = new RelayCommand(param => this.canUpdateDroits(), param => this.canExecuteUpdateDroits());
                }
                return this.updateDroitCommand;
            }
        }


        public RelayCommand DeleteVueCommand
        {
            get
            {
                if (this.deleteVueCommand == null)
                {
                    this.deleteVueCommand = new RelayCommand(param => this.canDeleteVue(param), param => this.canExecuteDeleteVue());
                }
                return this.deleteVueCommand;
            }
        }

      

        public RelayCommand CancelVueCommand
        {
            get
            {
                if (this.cancelVueCommand == null)
                {
                    this.cancelVueCommand = new RelayCommand(param => this.canCancelVue(), param => this.canExecuteCancelVue());
                }
                return this.cancelVueCommand;
            }
        }

        #endregion

        #region PROFILE VUES

        public RelayCommand AddVueProfileCommand
        {
            get
            {
                if (this.addVueProfileCommand == null)
                {
                    this.addVueProfileCommand = new RelayCommand(param => this.canAddProfileVue(), param => this.canExecuteAddProfileVue());
                }
                return this.addVueProfileCommand;
            }
        }


        public RelayCommand UpdateDroitProfileCommand
        {
            get
            {
                if (this.updateDroitProfileCommand == null)
                {
                    this.updateDroitProfileCommand = new RelayCommand(param => this.canUpdateProfilevue(), param => this.canExecuteUpdateProfileVue());
                }
                return this.updateDroitProfileCommand;
            }
        }

        public RelayCommand DeleteVueProfileCommand
        {
            get
            {
                if (this.deleteVueProfileCommand == null)
                {
                    this.deleteVueProfileCommand = new RelayCommand(param => this.canDeleteProfileVue(param), param => this.canExecuteDeleteProfileVue());
                }
                return this.deleteVueProfileCommand;
            }
        }

        public RelayCommand CancelVueProfileCommand
        {
            get
            {
                if (this.cancelVueProfileCommand == null)
                {
                    this.cancelVueProfileCommand = new RelayCommand(param => this.canCancelProfileVue(), param => this.canExecuteCancelProfileVue());
                }
                return this.cancelVueProfileCommand;
            }
        }

       //;
        #endregion
          
        #endregion

        #region METHODS

        #region REGION LOAD

        void loadRight()
        {
            if (CurrentDroit != null)
            {
                if ( CurrentDroit.Developpeur || CurrentDroit.Ecriture )
                {
                    IsfieldEnabled = true;
                    BtnInitVisible = true;
                    CheckTemporaireEnabled = true;
                    IsProfileVisible = false ;
                    IsUserProfileVisible = true;
                }
                else
                {
                   
                    IsfieldEnabled = false;
                    BtnInitVisible = true ;
                    CheckTemporaireEnabled = false;
                    IsProfileVisible = false;
                    IsUserProfileVisible = false;
                }

                if (UserConnected.Profile.ShortName.ToLower().Equals("opm"))
                {
                    if (CurrentDroit.Ecriture)
                    {
                        IsUserProfileVisible =true ;
                    }

                }

                if (CurrentDroit.Ecriture || CurrentDroit.Developpeur )
                {
                    IsProfileVisible = true;
                    IsUserProfileVisible = true;
                }
               


            }
        }



    
        public bool Contains(object de)
        {
            UtilisateurModel user = de as UtilisateurModel;
           
            return true;
        }

        public void refreshdata()
        {
             this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                   Users = userService.UTILISATEUR_SELECT();
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + ";" + "ERREUR CHARGEMENT UTILISATEURS";
                }
            };
            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    // view.Owner = localWindow;
                    view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                    view.ViewModel.Message = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    Utils.logUserActions(string.Format("<-- UI Utilisateur --Erreur lors du chargement    par : {0} - {1}", UserConnected.Loggin, args.Result.ToString()), "");

                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();
        }

        void loadDatas()
        {
            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                   // if (CacheDatas.ui_UsersCache == null)
                   // {
                        Users = userService.UTILISATEUR_SELECT();
                        CacheDatas.ui_UsersCache = Users;
                 

                   

                }
                catch (Exception ex)
                {
                    args.Result = ex.Message +  ";" + "ERREUR CHARGEMENT UTILISATEURS";
                }

                try
                {

                   // if (CacheDatas.ui_profilCache == null)
                   // {
                        ProfileList = profileservice.GetProfile();
                      //  CacheDatas.ui_profilCache = ProfileList;
                   // }
                   // else ProfileList = CacheDatas.ui_profilCache;

                    if (CurrentDroit != null)
                    {
                       

                        if (!CurrentDroit.Developpeur)
                        {
                            if (ProfileList != null)
                            {
                                ProfileModel profiles = ProfileList.Find(pr => pr.Libelle.ToLower().Contains("dev")) ?? new ProfileModel();
                                if (profiles.IdProfile > 0)
                                    ProfileList.Remove(profiles);
                            }
                        }
                       

                    }
                    
                    
            
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT PROFILES";

                }

                try
                {
                    if (CurrentDroit != null)
                    {
                        if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur  )
                        {
                            listeVues = droitservice.ListeVues();
                            List<ClassvuesModel> lListe = new List<ClassvuesModel>();
                            ListProfileVuesCache = new List<ClassvuesModel>();
                            if (listeVues != null)
                            {
                                foreach (var vue in listeVues)
                                {
                                    lListe.Add(new ClassvuesModel { IdVue = vue.Key.IdVue, Libelle = vue.Key.Libelle, SousVues = vue.Value });
                                    ListProfileVuesCache.Add(new ClassvuesModel { IdVue = vue.Key.IdVue, Libelle = vue.Key.Libelle, SousVues = vue.Value });
                                }
                                ListVuewNotChild = lListe;
                                ListProfileVuewNotChild = lListe;
                                CachelistVuewNotChild = lListe;
                                
                            }
                        }
                    }
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT DES VUES";

                }

              



            };
            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                   // view.Owner = localWindow;
                    view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                    view.ViewModel.Message = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    Utils.logUserActions(string.Format("<-- UI Utilisateur --Erreur lors du chargement    par : {0} - {1}", UserConnected.Loggin, args.Result.ToString()), "");
                   
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();
        }

        #endregion

        #region USER CREATE
        
       
        #region REGION ADD
        void canNewuser()
        {
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                _userSelected = new UtilisateurModel();
                UserSelected = _userSelected;
                IsfieldEnabled = true;
                IndexeProfile = -1;
                CheckTemporaireEnabled = false;
                LabelCompte = "";
                UtilisateurEditModal view = new UtilisateurEditModal();
                view.Owner = localWindow;
                view.DataContext = this;
                view.ShowDialog();
            }

        }
        

        private void canSaveUser()
        {
            this.IsBusy = true;
            ProgressBarVisibility = true;
            try
            {
                if (UserSelected.Id == 0)
                {
                    if (ProfileSelected != null)
                    {
                        if (!string.IsNullOrEmpty(NouveauNomImages))
                        {
                            string nomImage = NouveauNomImages.Substring(NouveauNomImages.LastIndexOf("\\") + 1);
                            UserSelected.Photo = UserSelected.Nom.Trim() + DateTime.Now.Year + nomImage;
                            CommonModule.SaveImages(NouveauNomImages, UserSelected.Nom);
                        }
                        else UserSelected.Photo = string.Empty;

                        UserSelected.Password = ParametersDatabase.DeaultPassword;
                        UserSelected.IdSite = societeCourante.IdSociete;
                        if (!string.IsNullOrEmpty(UserSelected.Loggin))
                        {
                            int valeurId = userService.UTILISATEUR_INSERT(UserSelected);

                            if (BordeprofileVisible)
                            {
                                profileservice.Profile_Add_Date(0, valeurId, ProfileSelected.IdProfile, StartDate, EndDate);
                                LabelCompte = "Ce compte crée est temporarire";
                            }



                        

                            Utils.logUserActions(string.Format("<-- UI Utilisateur -- Création dun nouvel utilisateur {0}   par : {1}", UserSelected.Nom +""+UserSelected.Loggin  , UserConnected.Loggin), "");
                            UserSelected = null;
                            ProfileSelected = null;
                            IsfieldEnabled = false;
                            LabelCompte = "";
                            CheckTemporaireEnabled = false;
                            StartDate = null;
                            IsAction = true;
                          
                        }
                        else
                        {
                            CustomExceptionView view = new CustomExceptionView();
                           // view.Owner = localWindow;
                            view.Title = "Information Creation utilisateur";
                            view.ViewModel.Message = "Le loggin est obligatoire";
                            view.ShowDialog();
                        }
                    }
                    else
                    {
                        CustomExceptionView view = new CustomExceptionView();
                       // view.Owner = localWindow;
                        view.Title = "Information Creation utilisateur";
                        view.ViewModel.Message = "Le Profile est obligatoire";
                        view.ShowDialog();
                    }
                        


                }
                else
                {
                    // update user

                    if (!string.IsNullOrEmpty(NouveauNomImages))
                    {
                        string nomImage = NouveauNomImages.Substring(NouveauNomImages.LastIndexOf("\\") + 1);
                        UserSelected.Photo = UserSelected.Nom.Trim() + DateTime.Now.Year + nomImage;
                        CommonModule.SaveImages(NouveauNomImages, UserSelected.Nom);
                    }
                    else UserSelected.Photo = string.Empty;
                    if (UserSelected.IdSite == 0)
                        UserSelected.IdSite = societeCourante.IdSociete;

                                    userService.UTILISATEUR_UPDATE(UserSelected);
                                    if (BordeprofileVisible)
                                    {
                                        profileservice.Profile_Add_Date((ProfileDateSelected != null ? ProfileDateSelected.ID : 0), UserSelected.Id, ProfileSelected.IdProfile, StartDate, EndDate);
                                    }

                                    Utils.logUserActions(string.Format("<-- UI Utilisateur -- Mise jour de l'utilisateur {0}   par : {1}", UserSelected.Nom + "" + UserSelected.Loggin, UserConnected.Loggin), "");
                                    BordeprofileVisible = false;
                                    UserSelected = null;
                                    ProfileSelected = null;
                                    LabelCompte = "";
                                    CheckTemporaireEnabled = false;
                                    IsAction = true;
                                   
                }


                Users = userService.UTILISATEUR_SELECT(false);
                if (Users != null)
                {
                    CacheUsers = Users;
                    CacheDatas.ui_UsersCache = null;
                    CacheDatas.ui_UsersCache = Users;
                    TotalUstilisateurs = CacheUsers.Count;
                }
                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               view.Owner = localWindow;
                view.Title = "Information De Sauvegarde";
                view.ViewModel.Message ="Une erreur est survenu lors de la mise jour/ insertion de lutilisateur";
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
                Utils.logUserActions(string.Format("<-- UI Utilisateur --Erreur Mise jour utilisateur   par : {1} ---- {2}", UserSelected.Nom + "" + UserSelected.Loggin, UserConnected.Loggin, ex.Message), "");
            }
        }

        bool canExecuteSaveUser()
        {
            bool values = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (UserSelected != null)
                    values = true;
            }
            return values;

        }

        #endregion

        #region REGION DELETE





        private void canDeleteUser()
        {


            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localWindow;
            messageBox.Title = ConstStrings.Get("msgSuppressiontitre");
            messageBox.ViewModel.Message = ConstStrings.Get("msgSuppression");
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                            userService.UTILISATEUR_DELETE(UserSelected.Id);
                          
                            //if (Users != null)
                            //{
                            //    CacheUsers = Users;
                            //    CacheDatas.ui_UsersCache = null;
                            //    CacheDatas.ui_UsersCache = Users;

                            //    TotalUstilisateurs = CacheUsers.Count;
                            //    IsAction = true;
                            //}
                            Utils.logUserActions(string.Format("<-- UI Utilisateur --Suppression de l'utilisateur  :{0}  par : {1}", UserSelected.Nom, UserConnected.Loggin), "");

                            UserSelected = null;
                            IsfieldEnabled = false;
                            Users = userService.UTILISATEUR_SELECT(false);
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localWindow;
                    view.Title = "Information De Suppression";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                    Utils.logUserActions(string.Format("<-- UI Utilisateur -- Erreur de Suppression de l'utilisateur  :{0}  par : {1}", UserSelected.Nom, UserConnected.Loggin), "");
                }
            }


        }

        bool canExecuteDeleteUser()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (UserSelected != null)
                    if (UserSelected.Id > 0)
                    values = true;
            }
            return values;
        }


        private void canDeleteUserActif()
        {


            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localWindow;
            messageBox.Title = ConstStrings.Get("msgSuppressiontitre");
            messageBox.ViewModel.Message = ConstStrings.Get("msgSuppression"); 
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    userService.UTILISATEUR_DELETE(UserActiv.Id);
                  
                    Utils.logUserActions(string.Format("<-- UI Utilisateur --Suppression de l'utilisateur  :{0}  par : {1}", UserActiv.Nom, UserActiv.Loggin), "");
                    UserActiv = null;
                    IsfieldEnabled = false;
                    Users = userService.UTILISATEUR_SELECT(false);
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                     view.Owner = localWindow;
                    view.Title = ConstStrings.Get("msgSuppressiontitre");
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                    Utils.logUserActions(string.Format("<-- UI Utilisateur -- Erreur de Suppression de l'utilisateur  :{0}  par : {1}", UserActiv.Nom, UserActiv.Loggin), "");
                }
            }


        }

        bool canExecuteDeleteUserActif()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (UserActiv != null)
                    if (UserActiv.Id > 0)
                        values = true;
            }
            return values;
        }
        #endregion


  
        private void canClose()
        {
            //_injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);
        }

       


        #region FILTER
        
  
        void filter(string values)
        {
            if (Users != null || Users.Count > 0)
            {
                DataTable newTable = new DataTable();
                DataColumn col1 = new DataColumn("id", typeof(Int32));
                DataColumn col2 = new DataColumn("nom", typeof(string));
                DataColumn col4 = new DataColumn("prenom", typeof(string));
                DataColumn col5 = new DataColumn("fonction", typeof(string));
                DataColumn col6 = new DataColumn("loggin", typeof(string));
                DataColumn col7 = new DataColumn("password", typeof(string));
                DataColumn col8 = new DataColumn("profile", typeof(string));
                DataColumn col9 = new DataColumn("idprofile", typeof(string));
                newTable.Columns.Add(col1);
                newTable.Columns.Add(col2);
                newTable.Columns.Add(col4);
                newTable.Columns.Add(col5);
                newTable.Columns.Add(col6);
                newTable.Columns.Add(col7);
                newTable.Columns.Add(col8);
                newTable.Columns.Add(col9);

                DataRow row = null;

                foreach (UtilisateurModel  sm in CacheUsers)
                {
                    row = newTable.NewRow();
                    row[0] = sm.Id ;
                    row[1] = sm.Nom ;
                    row[2] = sm.Prenom  ;
                    row[3] = sm.Fonction ;
                    row[4] = sm.Loggin ;
                    row[5] = sm.Password ;
                    row[6] = sm.Profile .Libelle ;
                    row[7] = sm.IdProfile ;
                  
                    newTable.Rows.Add(row);

                }

                DataRow[] nlignes = newTable.Select(string.Format("nom like '{0}%'", values.Trim()));
                DataTable filterDatatable = newTable.Clone();
                foreach (DataRow rows in nlignes)
                    filterDatatable.ImportRow(rows);

                UtilisateurModel  fm;
                ObservableCollection<UtilisateurModel> newliste = new ObservableCollection<UtilisateurModel>();

                foreach (DataRow r in filterDatatable.Rows)
                {
                    fm = new UtilisateurModel()
                    {
                         Id  = Int32.Parse(r[0].ToString()),
                         Nom  = r[1].ToString(),
                         Prenom  =r[2].ToString(),
                         Fonction  = r[3].ToString(),
                         Loggin  = r[4].ToString(),
                          Password  = r[5].ToString(),
                         Profile = CacheUsers.First(f => f.IdProfile == int.Parse(r[7].ToString())).Profile ,
                         IdProfile  =int .Parse ( r[7].ToString()),
                    };
                    newliste.Add(fm);
                }
                Users  = newliste;
            
            }
            else
            {
                loadDatas ();
               
            }
        }

        void canSearchUser()
        {
            if (CacheDatas.ui_UsersCache != null)
            {
                if (!string.IsNullOrEmpty(Filtertexte))
                {
                    ObservableCollection<UtilisateurModel> userss = new ObservableCollection<UtilisateurModel>();
                    var querys = CacheDatas.ui_UsersCache.Where(u => u.Nom.Contains(Filtertexte));
                    foreach (var u in querys)
                        userss.Add(u);
                    Users = userss;
                }
                else Users = CacheDatas.ui_UsersCache;
            }
        }

        bool canExecuteSearchUser()
        {
            return true;// string.IsNullOrEmpty(Filtertexte) ? false : true;
        }

        #endregion

        #region REGION INIT PASSWORD

        private void canInitPass(object param)
        {
            try
            {
                userService.UTILISATEUR_INIT_PASSWORD(UserSelected.Id, ParametersDatabase.DeaultPassword, UserConnected.IdProfile);
               

                ProgressBarVisibility = true;
                if (!isuserValide)
                    Users = userService.UTILISATEUR_SELECT(true);
                else
                    Users = userService.UTILISATEUR_SELECT(false);
                CacheUsers = Users;
                ProgressBarVisibility = false;
                CheckTemporaireEnabled = false;
                IsBusy = false;
                LabelCompte = "";
                Utils.logUserActions(string.Format("<-- UI Utilisateur --re initialisation de l'utilisateur  :{0}  par : {1}", UserSelected.Nom, UserConnected.Loggin), "");
                UserSelected = null;

                MessageBox.Show("Mot de Passe Re initialiser");
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               view.Owner = localWindow;
                view.Title = "Information De Initialisation mot passe";
                view.ViewModel.Message ="Erreur <<3003>> est survenue lors de la re initialisation de ce mot de passe";
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
                Utils.logUserActions(string.Format("<-- UI Utilisateur -- Erreur <<3003>> de Suppression de l'utilisateur  :{0}  par : {1} ---{2}", UserSelected.Nom, UserConnected.Loggin, ex.Message ), "");
            }
           
            
        }

        bool canExecuteInitPassd()
        {
            bool values = false;
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
            {
                if (UserSelected != null)
                    if (UserSelected.Id > 0)
                        values = true;
            }
            return values;

           
        }
        #endregion

        #region REGION LOCK PASS

        protected void canLockPass()
        {
             StyledMessageBoxView messageBox = new StyledMessageBoxView();
                messageBox.Owner = localWindow;
                messageBox.Title = "Information De Suppression";
                messageBox.ViewModel.Message = "Ce Compte sera Vérouiller et ne pourra plus être Utiliser";
                if (messageBox.ShowDialog().Value == true)
                {
                    this.IsBusy = true;
                   
                    try
                    {
                        userService.Utilsateur_LockAcount (UserSelected .Id );
                        UserSelected = null;
                        ProgressBarVisibility = true;
                        Users = userService.UTILISATEUR_SELECT(false);
                        CacheUsers = Users;
                        ProgressBarVisibility = false;
                        CheckTemporaireEnabled = false;
                        LabelCompte = "Compte Verouiller";
                        IsBusy = false;

                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = localWindow;
                        view.Title = "Information De Vérouillage";
                        view.ViewModel.Message ="erreure <<3003> survenu lors du verouillage du compte";
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                        Utils.logUserActions(string.Format("<-- UI Utilisateur -- Erreur <<3004>> de verouillage de l'utilisateur  :{0}  par : {1} ---{2}", UserSelected.Nom, UserConnected.Loggin, ex.Message), "");
                    }
                }

        }

        bool canExecuteLockPassd()
        {
            bool values = false;
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
            {
                if (UserSelected != null)
                    //if (UserSelected.Id > 0)
                        values = true;
            }
            return values;

           
        }

        #endregion


        #region REGION REFRESH
        
      
        protected void canRefreshLockListUser()
        {
            try
            {
                isuserValide = false;
                ProgressBarVisibility = true;
                Users = userService.UTILISATEUR_SELECT(true);
                CacheUsers = Users;
                Liseteselectionner = "Utilisateurs Désactivés ";
                ProgressBarVisibility = false ;
                CheckTemporaireEnabled = false;
                TotalUstilisateurs = CacheUsers.Count;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = localWindow;
                view.Title = "Information De Rafraichissement";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        bool canExecuteRefreshLockist()
        {
            return true;
        }

        private void canRefreshValideListUser()
        {
            try
            {
                isuserValide = true;
                ProgressBarVisibility = true;
                Users = userService.UTILISATEUR_SELECT(false);
                CacheUsers = Users;
                Liseteselectionner = "Utilisateurs Actifs";
                TotalUstilisateurs = CacheUsers.Count;
                ProgressBarVisibility = false;
                CheckTemporaireEnabled = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = localWindow;
                view.Title = "Information De Rafraichissement";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        bool canExecuteRefreshValideList()
        {
            return true  ;
        }

        #endregion
        #endregion


        #region USER VUES

        void canAddvue()
        {
            //VueSelected
            //NewSouVueSelected
            List<DroitModel> oldviewCache = ListeVuesUsers;
            var teste = NewSouVueSelected;
            var newTeste = VueSelected;
            if (NewSouVueSelected != null)
            {
                //traitement dun sous item
                DroitModel currentvue = ListeVuesUsers.Find(v => v.IdVues == VueSelected.IdVue);
                if (currentvue != null)
                {
                      if (!currentvue.SousDroits .Exists (s=>s.IdSouVue ==NewSouVueSelected.IdVue )){
                          DroitModel newSVues = new DroitModel();
                          newSVues.ID = 0;
                          newSVues.IdVues = VueSelected.IdVue;
                          newSVues.Iduser = UserdroitSelected.Id;
                          newSVues.LibelleVue = VueSelected.Libelle;
                          newSVues.LibelleSouVue = NewSouVueSelected.Libelle;
                          newSVues.IdSouVue = NewSouVueSelected.IdVue;
                          newSVues.BackGround ="#FF77AED8";
                          newSVues.Lecture = false;
                          newSVues.Ecriture = false;

                          newSVues.Super = false;
                          newSVues.Suppression = false;
                          newSVues.Developpeur = false;
                          newSVues.Testeur = false;
                          newSVues.Validation = false;
                          newSVues.Statut = "Nouveau";
                          ListeVuesUsers.Find(v => v.IdVues == VueSelected.IdVue).SousDroits.Add(newSVues);
                          if (ListeSousVues == null)
                              ListeSousVues = ListeVuesUsers.Find(v => v.IdVues == VueSelected.IdVue).SousDroits;
                          else
                          {
                              var localsouvue = ListeSousVues;
                              ListeSousVues = null;
                              ListeSousVues = localsouvue;

                          }

                          var localSousvues = NewlisteSousVues;
                          localSousvues.Remove(NewSouVueSelected);
                          NewlisteSousVues = null;
                          NewlisteSousVues = localSousvues;
                          isoperationUserAdd = false;
                      }
                }
            }
            else
            {
                //traitement dun Item
                DroitModel currentvue = ListeVuesUsers.Find(v => v.IdVues == VueSelected.IdVue);
                if (currentvue == null)
                {
                    currentvue = new DroitModel();
                    currentvue.ID = 0;
                    currentvue.IdVues = VueSelected.IdVue;
                    currentvue.Iduser = UserdroitSelected.Id;
                    currentvue.LibelleVue = VueSelected.Libelle;
                     currentvue.BackGround ="#FF77AED8";
                    currentvue.IdSouVue = 0;
                    currentvue.Lecture = false;
                    currentvue.Ecriture = false;
                    currentvue.Super = false;
                    currentvue.Suppression = false;
                    currentvue.Developpeur = false;
                    currentvue.Testeur = false;
                    currentvue.Validation = false;
                    currentvue.Statut = "Nouveau";
                    oldviewCache.Add(currentvue);
                    ListeVuesUsers = null;

                    ListeVuesUsers = oldviewCache;

                    //suppression de la vue ajoutée dans la liste des vues existante
                    List<ClassvuesModel> newlisteVues = ListVuewNotChild;
                    ClassvuesModel cls = newlisteVues.Find(cl => cl.IdVue == VueSelected.IdVue);
                    newlisteVues.Remove(cls);
                    ListVuewNotChild = null;
                    ListVuewNotChild = newlisteVues;
                    isoperationUserAdd = false;
                }
                else
                {
                    MessageBox.Show(" Cette vue existe deja pour ce utilisateur");
                }
            }

         
        }

        bool canExecuteAddVue()
        {
            bool values = false;
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur )
            {
                if (VueSelected != null)
                    if (UserdroitSelected != null)
                        if (isoperationUserAdd == true)
                    values = true;
            }
            return values;
           
        }


        void canUpdateVue()
        {
            try
            {

                //var vueUpdate = DroitUserItem.FirstOrDefault();
               // if (DroitUserItem != null)
                //{
                    if (NewDroitprofileItem.Iduser == 0)
                    {
                        NewDroitprofileItem.ID = 0;
                        NewDroitprofileItem.Iduser = UserdroitSelected.Id;
                    }
                    else
                    {
                       // NewDroitprofileItem.ID = NewDroitprofileItem.IdUserDroits;
                    }
                  
                        droitservice.Droit_Users_Profile_Add(NewDroitprofileItem);
                        var vue = VueSelected;
                        var sousvue = SousvueSelected;

                        if (NewDroitprofileItem.IdSouVue == 0)
                        {
                            MessageBox.Show("Droit vue mise jour ");
                        }
                        else
                        {
                            MessageBox.Show("Droit sous vue mise jour ");
                            ListeSousVues = null;
                        }

                        var nUsers = userService.UTILISATEUR_SELECT();
                       // List<DroitModel> newlisteDroits = droitservice.GetListdroit(UserdroitSelected.IdProfile, UserdroitSelected.Id);
                        if (VueUserSelected.SousDroits.Count > 0)
                        {

                          //  if (UserdroitSelected != null)
                                UserdroitSelected = nUsers.First(u => u.Id == UserdroitSelected.Id);

                            //UserdroitSelected.Profile.Droit = newlisteDroits;
                           // UserdroitSelected = UserdroitSelected;
                            VueUserSelected = UserdroitSelected.Profile.Droit.Find(sv => sv.IdVues == 6);

                            // ListeSousVues = null;
                            //ListeSousVues = UserdroitSelected.Profile.Droit.Find(sv => sv.IdVues == 6).SousDroits;
                        }
                        else
                        {

                           // if (UserdroitSelected != null)
                                UserdroitSelected = nUsers.First(u => u.Id == UserdroitSelected.Id);
                          //  UserdroitSelected.Profile.Droit = newlisteDroits;
                            //UserdroitSelected = UserdroitSelected;
                            ListeSousVues = null;
                        }
                    
                        DroitUserItem = null;
                        //List<DroitModel> newliste = droitservice.GetListdroit(UserdroitSelected.IdProfile, UserdroitSelected.Id);
                        //if (newliste != null)
                        //{
                        //    foreach (DroitModel dr in newliste)
                        //    {
                        //        if (dr.Iduser == 0)
                        //            dr.Statut = "Profile";
                        //        else dr.Statut = "utilisateur";
                        //    }
                        //    ListeVuesUsers = newliste;
                        //    if (CachelistVuewNotChild != null)
                        //    {
                        //        List<ClassvuesModel> newCache = new List<ClassvuesModel>();
                        //        foreach (var cls in CachelistVuewNotChild)
                        //            newCache.Add(cls);

                        //        foreach (var vs in CachelistVuewNotChild)
                        //        {
                        //            if (vs.SousVues.Count == 0)
                        //            {
                        //                if (ListeVuesUsers.Exists(v => v.IdVues == vs.IdVue))
                        //                    newCache.Remove(newCache.Find(vv => vv.IdVue == vs.IdVue));
                        //            }
                        //            else
                        //            {
                        //                var ndroit = ListeVuesUsers.Find(v => v.IdVues == vs.IdVue);
                        //                if (ndroit != null && ndroit.SousDroits .Count >0)
                        //                {
                        //                    List<DroitModel> sousDroits = ndroit.SousDroits;
                        //                    List<ClassvuesModel> sousvues = newCache.Find(sv => sv.IdVue == vs.IdVue).SousVues;
                        //                    ClassvuesModel newVues = newCache.Find(sv => sv.IdVue == vs.IdVue);
                        //                    newCache.Remove(newVues);
                        //                    foreach (DroitModel dr in sousDroits)
                        //                    {
                        //                        newVues.SousVues.Remove(newVues.SousVues.Find(sse => sse.IdVue == dr.IdSouVue));
                        //                    }
                        //                    newCache.Add(newVues);
                        //                }
                        //               // List<DroitModel> sousDroits = ListeVuesUsers.Find(v => v.IdVues == vs.IdVue).SousDroits;
                                        

                        //            }

                        //            ListVuewNotChild = null;
                        //            ListVuewNotChild = newCache;
                        //            isoperationUserAdd = true;
                        //        }
                        //    }
                        //}


                   
                //}
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = localWindow;
                view.Title = "ERREURE SAUVEGARDE VUE-USER";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
                Utils.logUserActions(string.Format("<-- UI Utilisateur --Erreur <<3005>>  de sauvegarde vue utilisateur   par : {1} ---- {2}", UserdroitSelected.Nom + "" + UserdroitSelected.Loggin, UserConnected.Loggin, ex.Message), "");
            }

        }

        bool canExecuteUpdateVue()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Developpeur || CurrentDroit.Ecriture)
            {
                if ( NewDroitprofileItem != null)
                   // if (DroitUserItem.FirstOrDefault().Iduser > 0 || UserdroitSelected!=null )
                            values = true;
            }
            return values;

           
        }

        void canUpdateDroits()
        {

        }

        bool canExecuteUpdateDroits()
        {
           

            return true;
        }

        void canDeleteVue(object param)
        {
            if (NewDroitprofileItem.Iduser == 0)
            {
                MessageBox.Show("la modification du profile nes pas authorisé sur cette vue \n reporter vous à la vue Profile");
                return;
            }

            var result = MessageBox.Show("Voulez vous Supprimez cette Vue à ce utilisateur?",
                                                       "Confirmer Suppression", MessageBoxButton.OKCancel, MessageBoxImage.Question);

            if (result.Equals(MessageBoxResult.OK))
            {
                try
                {
                    var user = VueUserSelected;
                    var vue = NewDroitprofileItem;


                    droitservice.Droits_Delete(vue.ID, 0, vue.Iduser);

                  
                    MessageBox.Show("Vue Supprimer de ce prifile utilisateur");

                    var nUsers = userService.UTILISATEUR_SELECT();
                   // List<DroitModel> newlisteDroits = droitservice.GetListdroit(UserdroitSelected.IdProfile, UserdroitSelected.Id);
                    if (VueUserSelected.SousDroits.Count > 0)
                    {

                        //if (UserdroitSelected != null)
                            UserdroitSelected = nUsers.First(u => u.Id == UserdroitSelected.Id);
                       // UserdroitSelected.Profile.Droit = newlisteDroits;
                       // UserdroitSelected = UserdroitSelected;

                        VueUserSelected = UserdroitSelected.Profile.Droit.Find(sv => sv.IdVues == 6);

                        // ListeSousVues = null;
                        //ListeSousVues = UserdroitSelected.Profile.Droit.Find(sv => sv.IdVues == 6).SousDroits;
                    }
                    else
                    {

                       // if (UserdroitSelected != null)
                           // UserdroitSelected.Profile.Droit = newlisteDroits;
                        UserdroitSelected = nUsers.First(u => u.Id == UserdroitSelected.Id);
                        ListeSousVues = null;
                    }

                    DroitUserItem = null;
                    isoperationUserAdd = true;

                    //foreach (DroitModel dr in newliste)
                    //{
                    //    if (dr.Iduser == 0)
                    //        dr.Statut = "Profile";
                    //    else dr.Statut = "utilisateur";
                    //}

                    //ListeVuesUsers = newliste;
                    //if (CachelistVuewNotChild != null)
                    //{
                    //    List<ClassvuesModel> newCache = new List<ClassvuesModel>();
                    //    foreach (var cls in CachelistVuewNotChild)
                    //        newCache.Add(cls);

                    //    foreach (var vs in CachelistVuewNotChild)
                    //    {
                    //        if (vs.SousVues.Count == 0)
                    //        {
                    //            if (ListeVuesUsers.Exists(v => v.IdVues == vs.IdVue))
                    //                newCache.Remove(newCache.Find(vv => vv.IdVue == vs.IdVue));
                    //        }
                    //        else
                    //        {
                    //            var droit = ListeVuesUsers.Find(v => v.IdVues == vs.IdVue);
                    //            if (droit != null && droit .SousDroits .Count >0)
                    //            {
                    //                List<DroitModel> sousDroits = droit.SousDroits;
                    //                List<ClassvuesModel> sousvues = newCache.Find(sv => sv.IdVue == vs.IdVue).SousVues;
                    //                ClassvuesModel newVues = newCache.Find(sv => sv.IdVue == vs.IdVue);
                    //                if (newVues!=null )
                    //                newCache.Remove(newVues);
                    //                foreach (DroitModel dr in sousDroits)
                    //                {
                    //                    newVues.SousVues.Remove(newVues.SousVues.Find(sse => sse.IdVue == dr.IdSouVue));
                    //                }
                    //                newCache.Add(newVues);
                    //            }
                    //            //List<DroitModel> sousDroits = ListeVuesUsers.Find(v => v.IdVues == vs.IdVue).SousDroits;
                               

                    //        }
                    //    }
                    //    ListVuewNotChild = null;
                    //    ListVuewNotChild = newCache;
                      
                   // }

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                   // view.Owner = localWindow;
                    view.Title = "ERREURE SUPPRESSION VUES-UTILISATEUR";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    //IsBusy = false;
                    //this.MouseCursor = null;
                }

            }
        }

        bool canExecuteDeleteVue()
        {
            bool values = false;
            if ( CurrentDroit.Suppression || CurrentDroit.Developpeur )
            {
                if( NewDroitprofileItem != null)
                    if (NewDroitprofileItem.ID > 0)
                        values = true;
            }
            return values;

           
        }

        void canCancelVue()
        {

            if (SousvueSelected != null)
            {
                 var result = MessageBox.Show("Voulez vous Annuler cette opération ?",
                                                       "Confirmer Annulation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                 if (result.Equals(MessageBoxResult.OK))
                 {
                     try
                     {
                         var oldListe = ListeSousVues;
                         var oldListVuewNotChild = ListVuewNotChild;


                         ClassvuesModel cl = new ClassvuesModel { IdVue = SousvueSelected.IdSouVue, Libelle = SousvueSelected.LibelleSouVue, SousVues = null };
                         oldListVuewNotChild.Find(v => v.IdVue == SousvueSelected.IdVues).SousVues.Add(cl);
                         oldListe.Remove(SousvueSelected);
                         ListeSousVues = null;
                         ListeSousVues = oldListe;

                         ListVuewNotChild = null;
                         ListVuewNotChild = oldListVuewNotChild;
                         DroitUserItem = null;
                         isoperationUserAdd = true;
                     }
                     catch (Exception ex)
                     {
                         CustomExceptionView view = new CustomExceptionView();
                       //  view.Owner = localWindow;
                         view.Title = "ERREURE ANNULATION Sous-vue";
                         view.ViewModel.Message = ex.Message;
                         view.ShowDialog();
                     }

                 }
            }
            else
            {
                if (VueUserSelected != null)
                {
                     var result = MessageBox.Show("Voulez vous Annuler cette operation?",
                                                       "Confirmer Annulation", MessageBoxButton.OKCancel,MessageBoxImage.Question );

                     if (result.Equals(MessageBoxResult.OK))
                     {
                         try { 
                         var oldListe = ListeVuesUsers;
                         var oldListVuewNotChild = ListVuewNotChild;
                         oldListe.Remove(VueUserSelected);
                         ClassvuesModel cl = new ClassvuesModel { IdVue = VueUserSelected.IdVues, Libelle = VueUserSelected.LibelleVue, SousVues =null  };
                         ListeVuesUsers = null;
                         ListeVuesUsers = oldListe;

                         oldListVuewNotChild.Add(cl);
                         ListVuewNotChild = null;
                         ListVuewNotChild = oldListVuewNotChild;
                         DroitUserItem = null;
                         isoperationUserAdd =true ;
                         }
                         catch (Exception ex)
                         {
                             CustomExceptionView view = new CustomExceptionView();
                           // view.Owner = localWindow;
                             view.Title = "ERREURE ANNULATION VUE";
                             view.ViewModel.Message = ex.Message;
                             view.ShowDialog();
                         }
                     }
                }

            }
           

        }


        bool canExecuteCancelVue()
        {
            bool valuesreturn = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur  )
            {
                if (VueUserSelected != null)
                {


                    if (VueUserSelected.SousDroits.Count == 0)
                    {
                        if (VueUserSelected.ID == 0)
                            valuesreturn = true;
                        else valuesreturn = false;

                    }
                    else valuesreturn = false;

                }
                if (SousvueSelected != null)
                {
                    if (SousvueSelected.ID == 0)
                        valuesreturn = true;
                    else valuesreturn = false;
                }
            }
            return valuesreturn;
        }
        #endregion

        #region USER PROFILE


        void canAddProfileVue()
        {
            List<DroitModel> oldviewCache = ListeVuesProfile;

            if (SouVueProfileSelected != null)
            {
                //traitement dun sous item
               // DroitProfileItemSelect de;
                DroitModel currentvue = ListeVuesProfile.Find(v => v.IdVues == VueProfileSelected.IdVue );
                if (currentvue != null)
                {
                    if (!currentvue.SousDroits.Exists(s => s.IdSouVue == SouVueProfileSelected.IdVue))
                    {
                        DroitModel newSVues = new DroitModel();
                        newSVues.ID = 0;
                        newSVues.IdVues = VueProfileSelected.IdVue;
                        // id profile
                        newSVues.IProfile = ProfileDroitSelected.IdProfile;
                        newSVues.Iduser = 0;
                        newSVues.LibelleVue = VueProfileSelected.Libelle;
                        newSVues.LibelleSouVue = SouVueProfileSelected.Libelle;
                        newSVues.IdSouVue = SouVueProfileSelected.IdVue;
                        newSVues.BackGround = "#FF77AED8";
                        newSVues.Lecture = false;
                        newSVues.Ecriture = false;

                        newSVues.Super = false;
                        newSVues.Suppression = false;
                        newSVues.Developpeur = false;
                        newSVues.Testeur = false;
                        newSVues.Validation = false;
                        newSVues.Statut = "Nouveau";

                        ListeVuesProfile.Find(v => v.IdVues == VueProfileSelected.IdVue).SousDroits.Add(newSVues);
                        if (ListeProfileSousVues == null)
                            ListeProfileSousVues = ListeVuesProfile.Find(v => v.IdVues == VueProfileSelected.IdVue).SousDroits;
                        else
                        {
                            var localsource = ListeProfileSousVues;
                            ListeProfileSousVues = null;
                            ListeProfileSousVues = localsource;

                        }

                        var localsousvues = NewlisteprofileSousVues;
                        localsousvues.Remove(SouVueProfileSelected);
                        NewlisteprofileSousVues = null;
                        NewlisteprofileSousVues = localsousvues;
                        isoperationProfilAdd = false;
                    }
                }
                else
                {
                    //premiere sous vue
                    DroitModel newSVues = new DroitModel();
                    newSVues.ID = 0;
                    newSVues.IdVues = VueProfileSelected.IdVue;
                    // id profile
                    newSVues.IProfile = ProfileDroitSelected.IdProfile;
                    newSVues.Iduser = 0;
                    newSVues.LibelleVue = VueProfileSelected.Libelle;
                    newSVues.LibelleSouVue = SouVueProfileSelected.Libelle;
                    newSVues.IdSouVue = SouVueProfileSelected.IdVue;
                    newSVues.BackGround = "#FF77AED8";
                    newSVues.Lecture = false;
                    newSVues.Ecriture = false;

                    newSVues.Super = false;
                    newSVues.Suppression = false;
                    newSVues.Developpeur = false;
                    newSVues.Testeur = false;
                    newSVues.Validation = false;
                    newSVues.Statut = "Nouveau";

                    var oldListevues = ListeVuesProfile;
                    //DroitModel newSouvue = new DroitModel { ID = 0, IdVues = VueProfileSelected.IdVue, LibelleVue = VueProfileSelected.Libelle, IProfile =ProfileDroitSelected.IdProfile, IdSouVue = SouVueProfileSelected.IdVue, LibelleSouVue = SouVueProfileSelected.Libelle  };
                    List <DroitModel> listeSouvue=new List<DroitModel> ();
                    listeSouvue.Add(newSVues);
                    DroitModel newVue = new DroitModel { ID = 0, IdVues = VueProfileSelected.IdVue, LibelleVue = VueProfileSelected.Libelle, IProfile = ProfileDroitSelected.IdProfile, SousDroits = listeSouvue };
                    oldListevues.Add(newVue);
                    ListeVuesProfile = null;
                    ListeVuesProfile = oldListevues;
                }
                     
                      

            }
            else
            {
                //traitement dun  item
               
                    DroitModel currentvue = ListeVuesProfile==null ?null : ListeVuesProfile.Find(v => v.IdVues == VueProfileSelected.IdVue);

                if (currentvue == null)
                {
                    currentvue = new DroitModel();
                    currentvue.ID = 0;
                    currentvue.IProfile =  ProfileDroitSelected.IdProfile;
                    currentvue.IdVues = VueProfileSelected.IdVue;
                    currentvue.Iduser =0;
                    currentvue.LibelleVue = VueProfileSelected.Libelle;
                    currentvue.IdSouVue = 0;
                    currentvue.BackGround = "#FF77AED8";
                    currentvue.Lecture = false;
                    currentvue.Ecriture = false;
                    currentvue.Super = false;
                    currentvue.Suppression = false;
                    currentvue.Developpeur = false;
                    currentvue.Testeur = false;
                    currentvue.Validation = false;
                    currentvue.Developpeur = false;
                    currentvue.Statut = "Nouveau";
                    if (oldviewCache == null) oldviewCache = new List<DroitModel>();
                      oldviewCache.Add(currentvue);
                   ListeVuesProfile = null;
                   ListeVuesProfile = oldviewCache;

                   List<ClassvuesModel> newlisteVues = ListProfileVuewNotChild;
                   ClassvuesModel cls = newlisteVues.Find(cl => cl.IdVue == VueProfileSelected.IdVue);
                   newlisteVues.Remove(cls);
                   ListProfileVuewNotChild = null;
                   ListProfileVuewNotChild = newlisteVues;
                   isoperationProfilAdd = false ;
                }

            }

        }


        bool canExecuteAddProfileVue()
        {
            bool values = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (VueProfileSelected != null)
                    if (ProfileDroitSelected != null)
                        if (isoperationProfilAdd == true)
                            values = true;
            }
            
            return values;
        }


        void canUpdateProfilevue()
        {
            try
            {
              //  var vueUpdate = NewDroitprofileItem.FirstOrDefault();
                if (NewDroitprofileItem != null)
                {
                    NewDroitprofileItem.Iduser = 0;
                    droitservice.Droit_Users_Profile_Add(NewDroitprofileItem);


                    if (NewDroitprofileItem.ID == 0)
                        MessageBox.Show("Nouvelle vue ajouter au profile ");
                    if (NewDroitprofileItem.ID > 0)
                        MessageBox.Show("Vue  du profile mise jour ");



                    //  DroitprofileItem = null;

                    List<DroitModel> newliste = droitservice.GetListdroit(NewDroitprofileItem.IProfile);
                    if (NewDroitprofileItem.IdSouVue == 0)
                    {
                        //traitement vues
                        ProfileDroitSelected.Droit = newliste;
                        ProfileDroitSelected = ProfileDroitSelected;
                    }
                    else
                    {
                        var vueselect = VueProfileDroitSelected;

                        ProfileDroitSelected.Droit = newliste;
                        ProfileDroitSelected = ProfileDroitSelected;
                        LoadVuesByProfileSelected(ProfileDroitSelected);

                        vueselect.SousDroits = newliste.Find(sd => sd.IdVues == 6).SousDroits;
                        VueProfileDroitSelected = vueselect;
                        // traitement sous vue
                        // VueProfileDroitSelected
                    }

                    //if (NewDroitprofileItem.IdSouVue > 0)
                    //    ListeProfileSousVues = null;

                    //if (newliste.Count > 0)
                    //{
                    //    ListeVuesProfile = newliste;
                    //    if (CachelistVuewNotChild != null)
                    //    {
                    //        List<ClassvuesModel> newCache = new List<ClassvuesModel>();
                    //        foreach (var cls in CachelistVuewNotChild)
                    //            newCache.Add(cls);
                    //        foreach (var vs in CachelistVuewNotChild)
                    //        {
                    //            if (vs.SousVues.Count == 0)
                    //            {
                    //                if (ListeVuesProfile.Exists(v => v.IdVues == vs.IdVue))
                    //                    newCache.Remove(newCache.Find(vv => vv.IdVue == vs.IdVue));
                    //            }
                    //            else
                    //            {
                    //                var myprofile = ListeVuesProfile.Find(v => v.IdVues == vs.IdVue);
                    //                if (myprofile != null)
                    //                {
                    //                    List<DroitModel> sousDroits = myprofile.SousDroits;

                    //                    List<ClassvuesModel> sousvues = newCache.Find(sv => sv.IdVue == vs.IdVue).SousVues;
                    //                    ClassvuesModel newVues = newCache.Find(sv => sv.IdVue == vs.IdVue);
                    //                    newCache.Remove(newVues);
                    //                    foreach (DroitModel dr in sousDroits)
                    //                    {
                    //                        newVues.SousVues.Remove(newVues.SousVues.Find(sse => sse.IdVue == dr.IdSouVue));
                    //                    }
                    //                    newCache.Add(newVues);
                    //                }
                    //            }
                    //        }
                    //        ListProfileVuewNotChild = null;
                    //        ListProfileVuewNotChild = newCache;
                    isoperationProfilAdd = true;
                    NewDroitprofileItem = null;
                    //}
                }
                    
                
        

              
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = localWindow;
                view.Title = "Information De Modification Profile";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecuteUpdateProfileVue()
        {
            bool values = false;
            if ( CurrentDroit.Developpeur || CurrentDroit.Ecriture)
            {
                if (NewDroitprofileItem != null )
                            values = true;
            }
            return values;

            
        }

        void canDeleteProfileVue(object param)
        {
               var result = MessageBox.Show("Voulez vous Supprimez cette Vue à ce Profile?",
                                                       "Confirmer Suppression", MessageBoxButton.OKCancel);

               if (result.Equals(MessageBoxResult.OK))
               {
                   try
                   {
                       var profile = ProfileDroitSelected;
                       //var vue=DroitprofileItem.FirstOrDefault ();
                     

                       droitservice.Droits_Delete(NewDroitprofileItem.ID, profile.IdProfile, 0);
                     
                       MessageBox.Show("Vue Supprimer de ce profile Supprimer");
                        List<DroitModel> newliste = droitservice.GetListdroit(profile.IdProfile);
                       if (NewDroitprofileItem.IdSouVue == 0)
                       {
                           //traitement vues
                           profile.Droit=newliste;
                           ProfileDroitSelected = profile;
                       }
                       else
                       {
                           var vueselect = VueProfileDroitSelected;

                           profile.Droit = newliste;
                           ProfileDroitSelected = profile;
                           LoadVuesByProfileSelected(ProfileDroitSelected);

                           vueselect.SousDroits = newliste.Find(sd=>sd.IdVues==6).SousDroits;
                           VueProfileDroitSelected = vueselect;
                           // traitement sous vue
                          // VueProfileDroitSelected
                       }

                       NewDroitprofileItem = null;


                      
                       //if (newliste.Count > 0)
                       //{
                       //    // si modification sous vue
                       //    if (VueProfileDroitSelected.SousDroits.Count > 0)
                       //    {
                       //        var vueUpdatefile = ListeVuesProfile.Find(v => v.IdVues == VueProfileDroitSelected.IdVues);
                       //        //ListeProfileSousVues = vueUpdatefile.SousDroits;
                       //        VueProfileDroitSelected = vueUpdatefile;
                       //    }else
                       //       ListeVuesProfile = newliste;

                       //    if (CachelistVuewNotChild != null)
                       //    {
                       //        List<ClassvuesModel> newCache = new List<ClassvuesModel>();
                       //        foreach (var cls in CachelistVuewNotChild)
                       //            newCache.Add(cls);
                       //        foreach (var vs in CachelistVuewNotChild)
                       //        {
                       //            if (vs.SousVues.Count == 0)
                       //            {
                       //                if (ListeVuesProfile.Exists(v => v.IdVues == vs.IdVue))
                       //                    newCache.Remove(newCache.Find(vv => vv.IdVue == vs.IdVue));
                       //            }
                       //            else
                       //            {
                       //                var myprofile = ListeVuesProfile.Find(v => v.IdVues == vs.IdVue);
                       //                if (myprofile != null)
                       //                {
                       //                    List<DroitModel> sousDroits = myprofile.SousDroits;

                       //                    List<ClassvuesModel> sousvues = newCache.Find(sv => sv.IdVue == vs.IdVue).SousVues;
                       //                    ClassvuesModel newVues = newCache.Find(sv => sv.IdVue == vs.IdVue);
                       //                    newCache.Remove(newVues);
                       //                    foreach (DroitModel dr in sousDroits)
                       //                    {
                       //                        newVues.SousVues.Remove(newVues.SousVues.Find(sse => sse.IdVue == dr.IdSouVue));
                       //                    }
                       //                    newCache.Add(newVues);
                                          
                       //                }
                       //            }
                       //        }
                       //        ListProfileVuewNotChild = null;
                       //        ListProfileVuewNotChild = newCache;
                       //        NewDroitprofileItem = null;
                       //    }
                       //}
                       isoperationProfilAdd = true;
                   }
                   catch (Exception ex)
                   {
                       CustomExceptionView view = new CustomExceptionView();
                      // view.Owner = localWindow;
                       view.Title = "Information De Suppression vue";
                       view.ViewModel.Message = ex.Message;
                       view.ShowDialog();
                       IsBusy = false;
                       this.MouseCursor = null;
                   }
               }

        }

        bool canExecuteDeleteProfileVue()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (NewDroitprofileItem != null )
                    if (NewDroitprofileItem.ID > 0 )
                    values = true;
            }
            return values;

           
        }


        void canCancelProfileVue()
        {
            if (SouVueProfileDroitSelected != null)
            {
                 var result = MessageBox.Show("Voulez vous Annuler cette opération ?",
                                                       "Confirmer Annulation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                 if (result.Equals(MessageBoxResult.OK))
                 {

                     var oldListe = ListeProfileSousVues;
                     var oldListVuewNotChild = ListProfileVuewNotChild;


                     ClassvuesModel cl = new ClassvuesModel { IdVue = SouVueProfileDroitSelected.IdSouVue, Libelle = SouVueProfileDroitSelected.LibelleSouVue, SousVues = null };
                     oldListVuewNotChild.Find(v => v.IdVue == SouVueProfileDroitSelected.IdVues).SousVues.Add(cl);
                     oldListe.Remove(SouVueProfileDroitSelected);
                     ListeProfileSousVues = null;
                     ListeProfileSousVues = oldListe;

                     ListProfileVuewNotChild = null;
                     ListProfileVuewNotChild = oldListVuewNotChild;
                     NewDroitprofileItem = null;
                     isoperationProfilAdd = true;
                 }

            }
            else
            {
                if (VueProfileDroitSelected != null)
                {
                     var result = MessageBox.Show("Voulez vous Annuler cette opération ?",
                                                       "Confirmer Annulation", MessageBoxButton.OKCancel, MessageBoxImage.Question);

                     if (result.Equals(MessageBoxResult.OK))
                     {
                         var oldListe = ListeVuesProfile;
                         var oldListVuewNotChild = ListProfileVuewNotChild;

                         oldListe.Remove(VueProfileDroitSelected);

                         ClassvuesModel cl = new ClassvuesModel { IdVue = VueProfileDroitSelected.IdVues, Libelle = VueProfileDroitSelected.LibelleVue, SousVues = null };
                         ListeVuesProfile = null;
                         ListeVuesProfile = oldListe;

                         oldListVuewNotChild.Add(cl);
                         ListProfileVuewNotChild = null;
                         ListProfileVuewNotChild = oldListVuewNotChild;
                         NewDroitprofileItem = null;
                         isoperationProfilAdd  = true;

                     }

                }

            }
           
        }

        bool canExecuteCancelProfileVue()
        {
            bool valuesreturn = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (VueProfileDroitSelected != null)
                {


                    if (VueProfileDroitSelected.SousDroits.Count == 0)
                    {
                        if (VueProfileDroitSelected.ID == 0)
                            valuesreturn = true;
                        else valuesreturn = false;

                    }
                    else valuesreturn = false;

                }
                if (SouVueProfileDroitSelected != null)
                {
                    if (SouVueProfileDroitSelected.ID == 0)
                        valuesreturn = true;
                    else valuesreturn = false;
                }
            }
            return valuesreturn;
        }
        #endregion

        #endregion

        #region business methos

        bool LoadVuesByProfileSelected(ProfileModel ProfileDroitSelected)
        {
            try
            {
                listeVues = droitservice.ListeVues();
                List<ClassvuesModel> lListe = new List<ClassvuesModel>();
                if (listeVues != null)
                {
                    foreach (var vue in listeVues)
                    {
                        lListe.Add(new ClassvuesModel { IdVue = vue.Key.IdVue, Libelle = vue.Key.Libelle, SousVues = vue.Value });

                    }
                }
                CachelistVuewNotChild = lListe;

                foreach (DroitModel vf in ProfileDroitSelected.Droit)
                {


                    var vuesExiste = CachelistVuewNotChild.Find(v => v.IdVue == vf.IdVues);

                    if (vf.SousDroits.Count > 0)
                    {
                        if (vf.SousDroits.Count == vuesExiste.SousVues.Count)
                            CachelistVuewNotChild.Remove(vuesExiste);
                        else
                        {
                            foreach (DroitModel ssf in vf.SousDroits)
                            {
                                var itemS = CachelistVuewNotChild.Find(v => v.IdVue == vf.IdVues).SousVues.FirstOrDefault(it => it.IdVue == ssf.IdSouVue);
                                if (itemS != null)
                                    CachelistVuewNotChild.Find(v => v.IdVue == vf.IdVues).SousVues.Remove(itemS);

                            }
                        }

                        var svd = vuesExiste.SousVues.FirstOrDefault(svv => svv.IdVue == vf.IdSouVue);
                        vuesExiste.SousVues.Remove(svd);
                        CachelistVuewNotChild.Remove(vuesExiste);
                        CachelistVuewNotChild.Add(vuesExiste);
                    }
                    else
                        CachelistVuewNotChild.Remove(vuesExiste);


                }

                foreach (ClassvuesModel svf in CachelistVuewNotChild)
                {
                    if (svf.SousVues.Count > 0)
                    {
                        var vuesCourantes = ProfileDroitSelected.Droit.FirstOrDefault(sd => sd.IdVues == svf.IdVue);
                        if (vuesCourantes != null)
                        {
                            var sousvuesCourantes = vuesCourantes.SousDroits;
                            foreach (DroitModel svc in sousvuesCourantes)
                            {
                                var svSelcted = svf.SousVues.FirstOrDefault(sd => sd.IdVue == svc.IdVues);
                                if (svSelcted != null)
                                    svf.SousVues.Remove(svSelcted);


                            }
                        }
                    }
                }


                ListProfileVuewNotChild = CachelistVuewNotChild;
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }


        bool LoadUserVues(UtilisateurModel UserdroitSelected)
        {
            try
            {
                listeVues = droitservice.ListeVues();
                List<ClassvuesModel> lListe = new List<ClassvuesModel>();
                if (listeVues != null)
                {
                    foreach (var vue in listeVues)
                    {
                        lListe.Add(new ClassvuesModel { IdVue = vue.Key.IdVue, Libelle = vue.Key.Libelle, SousVues = vue.Value });

                    }
                }
                CachelistVuewNotChild = lListe;

                foreach (DroitModel vf in UserdroitSelected.Profile.Droit)
                {


                    var vuesExiste = CachelistVuewNotChild.Find(v => v.IdVue == vf.IdVues);

                    if (vf.SousDroits.Count > 0)
                    {
                        if (vf.SousDroits.Count == vuesExiste.SousVues.Count)
                            CachelistVuewNotChild.Remove(vuesExiste);
                        else
                        {
                            foreach (DroitModel ssf in vf.SousDroits)
                            {
                                var itemS = CachelistVuewNotChild.Find(v => v.IdVue == vf.IdVues).SousVues.FirstOrDefault(it => it.IdVue == ssf.IdSouVue);
                                if (itemS != null)
                                    CachelistVuewNotChild.Find(v => v.IdVue == vf.IdVues).SousVues.Remove(itemS);

                            }
                        }

                        var svd = vuesExiste.SousVues.FirstOrDefault(svv => svv.IdVue == vf.IdSouVue);
                        vuesExiste.SousVues.Remove(svd);
                        CachelistVuewNotChild.Remove(vuesExiste);
                        CachelistVuewNotChild.Add(vuesExiste);
                    }
                    else
                        CachelistVuewNotChild.Remove(vuesExiste);


                }

                foreach (ClassvuesModel svf in CachelistVuewNotChild)
                {
                    if (svf.SousVues.Count > 0)
                    {
                        var vuesCourantes = UserdroitSelected.Profile.Droit.FirstOrDefault(sd => sd.IdVues == svf.IdVue);
                        if (vuesCourantes != null)
                        {
                            var sousvuesCourantes = vuesCourantes.SousDroits;
                            foreach (DroitModel svc in sousvuesCourantes)
                            {
                                var svSelcted = svf.SousVues.FirstOrDefault(sd => sd.IdVue == svc.IdVues);
                                if (svSelcted != null)
                                    svf.SousVues.Remove(svSelcted);


                            }
                        }
                    }
                }

                ListVuewNotChild = null;
                ListVuewNotChild = CachelistVuewNotChild;
               
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        void loadForms(Int32 idVues)
        {

            switch (idVues)
            {
                case 1:// Administration(export/Import Db)
                    {
                        IsEnableAdminExport = true;
                        IsEnableAdminImport = true;

                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;

                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = false;
                        IsEnableEdition = false;
                        IsEnableEcriture = false;
                        IsEnablelecture = false;
                      
                        break;
                    }

                case 4:// new facture
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = true;

                        IsEnableSignature = false;
                        IsEnableMarUp = true;
                        IsEnableImpression = true;
                        IsEnableExport = false;
                        IsEnableSupression = true;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;

                       
                        break;
                    }

                case 5:// historic facture
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                      
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = true;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = true;
                        IsEnableStatutSortie = true;
                        IsEnableStatutSupression = true;
                        IsEnableStatutSuspension = true;
                        IsEnableStatutValidation = true;
                        
                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = true;
                        IsEnableExport = true;
                        IsEnableSupression = true;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;
                       
                        break;
                    }

                case 6:// datareferences
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = false;
                        IsEnableEdition = false;
                        IsEnableEcriture = false;
                        IsEnablelecture = false;
                        IsEnableAdminExport = false;
                        break;
                    }

                case 7:// Parametres
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = false;
                        IsEnableSupression = false;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;
                      
                        break;
                    }

                case 8:// Sortie facture
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = true;
                        IsEnableSupression = false;
                        IsEnableEdition = true;
                        IsEnableEcriture = false;
                        IsEnablelecture = true;
                       
                        break;
                    }

                case 10:// dataref-societe
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = true;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = false;
                        IsEnableSupression = true;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;
                      
                        break;
                    }

                case 11:// dataref-users
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = false;
                        IsEnableSupression = true;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;
                     
                        break;
                    }
                case 12:// dataref-Produits
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = false;
                        IsEnableSupression = true;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;
                       
                        break;
                    }
                case 13:// dataref-Clients
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = false;
                        IsEnableSupression = true;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;
                      
                        break;
                    }
                case 14:// dataref-Facture/Compta
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;
                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableExport = false;
                        IsEnableSupression = true;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;
                       
                        break;
                    }

                case 15:// Journal vente
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;
                        IsEnableArchiveExecuter = false;
                        IsEnableArchiveLecture = false;

                        IsEnableJvExportSage = true;
                        IsEnableJvLecture = true;
                        IsEnableJvSuppression = true;
                        IsEnableJvPreparation = true;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableSupression = false;
                        IsEnableExport = true;
                        IsEnableEdition = false;
                        IsEnableEcriture = false;
                        IsEnablelecture = false;
                       
                        break;
                    }

                case 16:// admin/backup(maintenance)
                    {
                        IsEnableAdminExport = false;
                        IsEnableAdminImport = false;

                        IsEnableArchiveExecuter = true;
                        IsEnableArchiveLecture = true;

                        IsEnableJvExportSage = false;
                        IsEnableJvLecture = false;
                        IsEnableJvSuppression = false;
                        IsEnableJvPreparation = false;

                        IsEnablVAlidation = false;
                        IsEnableStatutSortie = false;
                        IsEnableStatutSupression = false;
                        IsEnableStatutSuspension = false;
                        IsEnableStatutValidation = false;

                        IsEnableSignature = false;
                        IsEnableMarUp = false;
                        IsEnableImpression = false;
                        IsEnableSupression = false;
                        IsEnableExport = false;
                        IsEnableEdition = false;
                        IsEnableEcriture = false;
                        IsEnablelecture = false;
                      
                        break;
                    }
                case 55:// default truel for all
                    {
                        IsEnableAdminExport = true;
                        IsEnableAdminImport = true;

                        IsEnableArchiveExecuter = true;
                        IsEnableArchiveLecture = true;
                        IsEnableJvExportSage = true;
                        IsEnableJvLecture = true;
                        IsEnableJvSuppression = true;
                        IsEnableJvPreparation = true;

                        IsEnablVAlidation = true;
                        IsEnableStatutSortie = true;
                        IsEnableStatutSupression = true;
                        IsEnableStatutSuspension = true;
                        IsEnableStatutValidation = true;

                        IsEnableSignature = true;
                        IsEnableMarUp = true;
                        IsEnableImpression = true;
                        IsEnableExport = true;
                        IsEnableEdition = true;
                        IsEnableEcriture = true;
                        IsEnablelecture = true;
                        IsEnableSupression = true;
                        break;
                    }
            }
                
        }
        #endregion

    }
}
