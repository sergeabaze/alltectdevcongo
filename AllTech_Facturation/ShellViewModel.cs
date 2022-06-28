
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using AllTech.FrameWork.Services;
using AllTech_Facturation.Views;
using System.Windows.Input;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Region;
using AllTech.FrameWork.Views;
using System.Windows;
using AllTech.FacturationModule.Views;
using System.Xml.Linq;
using AllTech.FrameWork.Global;
using AllTech.FrameWork.Model;
using System.ComponentModel;
using System.Threading;
using AllTech.FacturationModule.Views.Modal;
using AllTech.FrameWork.Logger;
using Microsoft.Practices.ServiceLocation;
using Microsoft.Practices.Prism.Logging;
using AllTech.FrameWork.Utils;
using AllTech_Facturation.Report;
using AllTech_Facturation.LocalApps;
using System.Collections;
using Multilingue.Resources;
using AllTech.FrameWork.Helpers;


namespace AllTech_Facturation
{
    /// <summary>
    /// Classe dinjection des Methodes formulaire principale
    /// </summary>
    public class ShellViewModel : ViewModelBase
    {
        #region Fields

        private  Window wpfParent;
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IUnityContainer _container;
        ILoggerFacade _logger;
        private IInjectSingleViewService _injectSingleViewService;
        UserLoggin logginView = null;

        private string _userName;
        private string _password;
        private string _newPassword;

      
       
        private string _firstNamePropertyName;
        private string _lastNamePropertyName;
       

         private RelayCommand logginCommand;
         private RelayCommand logginOfCommand;
         private RelayCommand cancelCommand;
         private RelayCommand exitCommand;
         private RelayCommand datasRefsCommand;
         private RelayCommand parameterCommand;
         private RelayCommand facturationCommand;
         private RelayCommand historicfacturationCommand;
         private RelayCommand JournalventeCommand;
         private RelayCommand sortiefacturationCommand;
         private RelayCommand journalVenteCommand;
         private RelayCommand adminCommand;
         private RelayCommand aproposCommand;
         private RelayCommand userGuideCommand;
         private RelayCommand administrationCommand;

         private RelayCommand rebusFacturesCommand;
         private RelayCommand societeCommand;
         private RelayCommand userCommand;
         private RelayCommand produitCommand;
         private RelayCommand factureCommand;
         private RelayCommand clientCommand;

         private RelayCommand normaleStyleCommand;
         private RelayCommand customStyleCommand;

        private Cursor mouseCursor;
        private bool _isWorking = false;
       
        string defaultPass = string.Empty;


          bool _borderVisibility;
          bool _progressBarVisibility;
          bool labelmsgBarVisibility;
          bool _warningVisibility;
          bool _parametersVisibility;
          bool _sortieFactureVisibility;
          bool _factureVisibility;
          bool _datarefVisibility;
          bool _historicFactureVisibility;
          bool _administrationVisibility;
          bool _menuOperationVisibility;
          bool _menuManagementVisibility;
          bool _menuDataPreparationVisibility;
          bool _rebusFactureVisibility;

          bool _separatormanagementVisibility;
          bool _separatorOperationVisibility;
          bool _setnewpasswordvisibility;
          bool _separatorSortieVisibility;
          bool _separatorDataVisibility;
          bool btnLogginEnabled;
          bool valuesCmdloggin = true;
          bool _AdminVisibility;
          bool _JournalVenteVisibility;

          bool _toolbarVisibility;
          bool _separatortoolB_1;
          bool _separatortoolB_2;
          bool _separatortoolB_3;
          bool _separatortoolB_4;

          bool _separatorDataref_1;
          bool _separatorDataref_2;
          bool _separatorDataref_3;
          bool _separatorDataref_4;
          bool _separatorDatarefAdmin;
          bool _separatorJv;

          bool _menuDatarefSocieteVisibility;
          bool _menuDatarefuserVisibility;
          bool _menuDatarefProduitVisibility;
          bool _menuDatarefClientVisibility;
          bool _menuDatarefFactureVisibility;
          bool _comboboxlisteDbVisibility;
          int _comboboxlisteDbIndex;

        

       
        

          bool isgetdroitfacture = false;
         
          FactureModel factureservice;
          LigneFactureModel _ligneFacture;
          List<LigneFactureModel> _LigneFatureListe;
          private UtilisateurModel _userSelected;

        
          private UtilisateurModel userService;
          SocieteModel societeservice;
          SocieteModel currentcompany;
          ParametresModel _parametersDatabase;
          private bool isnewwLoggin;

       
          string error;
          string version;
          bool isAdonetVisible;
          bool isproduction;
          bool islocation;

          //TimerModel _timer =new TimerModel ();
          //private int _completedCount = 0;
          //string timerValue;
          //private double percentElapsed;
          //TimeSpan duration;

          object contenRegion;
          ProfileDateModel profileDateSelected;
          Databaseinfo databaseSelected;
         UtilisateurModel  UserConnected ;
         DroitModel CurrentDroit;
        #endregion

        #region Constructor
         
        public ShellViewModel(Window parent)
        {
            _borderVisibility = false;
            wpfParent = parent;
            labelmsgBarVisibility = false;
            _warningVisibility = false;
            Setnewpasswordvisibility = false;
            EventArgs e = new EventArgs();

           
            Communicator.userControlName += new Communicator.MyEventHandler(updateContenRegion);
            _injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
            GlobalDatas.DisplayLanguage = (Hashtable)Application.Current.TryFindResource("contentFile");
            try
            {
               
                GlobalDatas.urlDossierimages = Utils.getUrlDossierimages();
                ParametersDatabase = GlobalDatas.dataBasparameter;

            }
            catch (Exception ex)
            {
                MessageBox.Show(GlobalDatas.DisplayLanguage["vmLoading_MsgErreurChrgt"].ToString(),
                 GlobalDatas.DisplayLanguage["vmLoading_MsgErreurCrgtTitre"].ToString(),
                  MessageBoxButton.OK,
                  MessageBoxImage.Exclamation);
              
                return;
            }
          
            InitializeModel();
            FactureVisibility = false;
            initLabels();
         
            if (ParametersDatabase.Dejautiliser == "0")
            {
                // nouvelle installation, necessite configuration
                loadLogginView();
                BtnLogginEnabled = true;
                IsnewwLoggin = true;
                GlobalDatas.isConnectionOk = false;
                //Utils.logUserActions("", "Sysfact -- Beta -- 0.0");
                //Utils.logUserActions("Nouvelle installation ", "");
            }
            else
            {
                int duree = 0;

                #region Teste Connection


                List<Databaseinfo> listeDb = null;

                // tester la connection
                try
                {
                    Utils.logConnection("Vérification existence base de données " + DateTime.Now, "");
                    if (Helper.IsConnected(ref listeDb))
                    {

                        IsnewwLoggin = false;
                        //teste connection
                        //  GlobalDatas.isConnectionOk = true;
                        //loadcompany();
                        BtnLogginEnabled = true;
                        // GlobalDatas.isConnectionOk = true;
                        DatabaseList = listeDb;

                        if (DatabaseList.Count > 1)
                        {
                            Databaseinfo currentDbname = DatabaseList.FirstOrDefault(d => d.ISfefault == true);
                            if (currentDbname != null)
                                DatabaseSelected = currentDbname;
                            ComboboxlisteDbVisibility = true;
                        }
                        else
                        {
                            ComboboxlisteDbVisibility = false;

                            if (DatabaseList.Count == 1)
                                DatabaseSelected = DatabaseList[0];
                        }

                       // userService = new UtilisateurModel();
                       // societeservice = new SocieteModel();

                        loadLogginView();
                    }
                    else
                    {

                    }
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = null;
                    if (ex.InnerException != null)
                    {
                        if (ex.InnerException.Message.Contains("Access denied for user "))
                        {
                            view = new CustomExceptionView();
                            GlobalDatas.isConnectionOk = false;
                            IsnewwLoggin = true;
                           // loadLogginView();
                            BtnLogginEnabled = true;
                            ComboboxlisteDbVisibility = false;
                            view.Title = "INFORMATION ERREURE CONNECTION";
                            view.ViewModel.Message = string.Format("Access à la base de donnée non authorisée Au compte  {0}/{1}", GlobalDatas.dataBasparameter.Utilisateur, GlobalDatas.dataBasparameter.Motpasse);
                            view.ShowDialog();

                            Utils.logConnection("Erreur de connection  " + ex.Message + " " + DateTime.Now, "");

                        }
                        else
                        {
                            view = new CustomExceptionView();
                            GlobalDatas.isConnectionOk = false;
                            IsnewwLoggin = true;
                           // loadLogginView();
                            BtnLogginEnabled = true;
                            ComboboxlisteDbVisibility = false;
                            view.Title = "INFORMATION ERREURE CONNECTION";
                            view.ViewModel.Message = ex.InnerException.Message;
                            view.ShowDialog();

                            Utils.logConnection("Erreur de connection  " + ex.Message + " " + DateTime.Now, "");
                            
                        }
                    }
                    else
                    {
                       // CustomExceptionView view = new CustomExceptionView();
                        // view.Owner = Application.Current.MainWindow;
                        view = new CustomExceptionView();
                        IsWorking = false;
                        view.Title = "INFORMATION ERREURE CONNECTION";
                        view.ViewModel.Message = "Echect tentative de connection";
                        view.ShowDialog();
                        Utils.logConnection("Erreur de connection  "+ex.Message  +" "+ DateTime.Now, "");
                        ComboboxlisteDbVisibility = false;
                      
                    }

                    Utils.logConnection(string.Format("Erreur de connection  parametres  connection: serveur :{0} user:{1} password:{2}", GlobalDatas.dataBasparameter.Nomserveur, GlobalDatas.dataBasparameter.Utilisateur, GlobalDatas.dataBasparameter.Motpasse), "");
                 
                    EventArgs e1 = new EventArgs();
                    Communicator evet = new Communicator();
                    evet.OnChangeCloseView(e1);
                   
                }
                #endregion

      
              

            }
        
            _progressBarVisibility = false;
           // BorderVisibility = true;
        }

        #region version

        void Loadversion()
        {
            int duree = 0;
            switch (ParametersDatabase.CodeCourant)
            {
                case "dev":
                    {
                        duree = ParametersDatabase.DureeDev != string.Empty ? Convert.ToInt32(ParametersDatabase.DureeDev) : 0;
                        Version = string.Format("{0} -- Mode Dev -- 2.0", DatabaseSelected.DatabaseNAme.ToLowerInvariant());
                        isproduction = false;
                        islocation = false;
                        break;
                    }

                case "prod":
                    {
                        duree = ParametersDatabase.DureeProd != string.Empty ? Convert.ToInt32(ParametersDatabase.DureeProd) : 0;
                        Version = string.Format("{0} --Production -- 2.0", DatabaseSelected.DatabaseNAme.ToLowerInvariant());
                        isproduction = true;
                        islocation = false;

                        try
                        {
                            // Utils.logConnection("", Version);
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            view.Owner = Application.Current.MainWindow;
                            view.Title = GlobalDatas.DisplayLanguage["vmLoading_MsgErreurCrgtTitre"].ToString();
                            view.ViewModel.Message = GlobalDatas.DisplayLanguage["vmLoading_MsgErreurChrgt"].ToString() + ex.Message;
                            view.ShowDialog();
                        }

                        break;
                    }

                case "teste":
                    {


                        duree = ParametersDatabase.DureeTeste != string.Empty ? Convert.ToInt32(ParametersDatabase.DureeTeste) : 0;
                        Version = string.Format("{0} --Beta Test -- 2.0", DatabaseSelected.DatabaseNAme.ToLowerInvariant());
                        isproduction = false;
                        islocation = false;
                        try
                        {
                            //Utils.logConnection("", Version);
                            //Utils.logUserActions( "",Version);
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            view.Owner = Application.Current.MainWindow;
                            view.Title = GlobalDatas.DisplayLanguage["vmLoading_MsgErreurCrgtTitre"].ToString();
                            view.ViewModel.Message = GlobalDatas.DisplayLanguage["vmLoading_MsgErreurChrgt"].ToString() + ex.Message;
                            view.ShowDialog();
                        }
                        break;
                    }

                case "location":
                    {
                        duree = ParametersDatabase.DureeLocaton != string.Empty ? Convert.ToInt32(ParametersDatabase.DureeLocaton) : 0;
                        isproduction = false;
                        islocation = true;

                        try
                        {
                            //Utils.logConnection("", "Sysfact -- Beta -- 0.0");
                            //Utils.logUserActions("","Sysfact -- Beta -- 0.0");
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            view.Owner = Application.Current.MainWindow;
                            view.Title = "GENERATION FICHIER LOG";
                            view.ViewModel.Message = "Probleme de génération du fichier Log" + ex.Message;
                            view.ShowDialog();
                        }

                        break;
                    }
            }
        }

        #endregion
          #endregion

        #region Properties

        public int ComboboxlisteDbIndex
        {
            get { return _comboboxlisteDbIndex; }
            set { _comboboxlisteDbIndex = value;
            OnPropertyChanged("ComboboxlisteDbIndex");
            }
        }


        List<Databaseinfo> databaseList;

        public List<Databaseinfo> DatabaseList
        {
            get { return databaseList; }
            set { databaseList = value;
            OnPropertyChanged("DatabaseList");
            }
        }


        public Databaseinfo DatabaseSelected
        {
            get { return databaseSelected; }
            set { databaseSelected = value;
            if (value != null)
            {
                GlobalDatas.ConnectionString = string.Format("SERVER={0};UID='{1}'; PASSWORD='{2}';database='{3}'", ParametersDatabase.Nomserveur, ParametersDatabase.Utilisateur, ParametersDatabase.Motpasse, value.DatabaseNAme.ToLower()); ;
                ParametersDatabase.NomBd = value.DatabaseNAme.ToLower();
                Loadversion();
            }
            else
                return;
         
            OnPropertyChanged("DatabaseSelected");
            }
        }


        public bool ComboboxlisteDbVisibility
        {
            get { return _comboboxlisteDbVisibility; }
            set { _comboboxlisteDbVisibility = value;
            OnPropertyChanged("ComboboxlisteDbVisibility");
            }
        }

        public bool SeparatorJv
        {
            get { return _separatorJv; }
            set { _separatorJv = value;
              OnPropertyChanged("SeparatorJv");
            }
        }

        public bool JournalVenteVisibility
        {
            get { return _JournalVenteVisibility; }
            set { _JournalVenteVisibility = value;
            OnPropertyChanged("JournalVenteVisibility");
            }
        }


        public ProfileDateModel ProfileDateSelected
        {
            get { return profileDateSelected; }
            set { profileDateSelected = value;
            OnPropertyChanged("ProfileDateSelected");
            }
        }


        public object ContenRegion
        {
            get { return contenRegion; }
            set { contenRegion = value;
            OnPropertyChanged("ContenRegion");
            }
        }

        public bool IsnewwLoggin
        {
            get { return isnewwLoggin; }
            set { isnewwLoggin = value;
            OnPropertyChanged("IsnewwLoggin");
            }
        }

        public bool SeparatorDatarefAdmin
        {
            get { return _separatorDatarefAdmin; }
            set { _separatorDatarefAdmin = value;
            OnPropertyChanged("SeparatorDatarefAdmin");
            }
        }

        public bool AdminVisibility
        {
            get { return _AdminVisibility; }
            set { _AdminVisibility = value;
            OnPropertyChanged("AdminVisibility");
            }
        }
        //public string TimerValue
        //{
        //    get
        //    {
        //        return timerValue;
        //    }

        //    set
        //    {
        //        if (timerValue != value)
        //        {
        //            timerValue = value;
        //            OnPropertyChanged("TimerValue");
        //        }
        //    }
        //}

        //public TimeSpan Duration
        //{
        //    get
        //    {
        //        return _timer.Duration;
        //    }

        //    set
        //    {
        //        if (_timer.Duration == value)
        //            return;
        //        _timer.Duration = value;
        //        OnPropertyChanged("Duration");
        //    }
        //}

        //public double PercentElapsed
        //{
        //    get
        //    {
        //        return percentElapsed;
        //    }
        //    set
        //    {
        //        if (value != percentElapsed)
        //        {
        //            percentElapsed = value;
        //            OnPropertyChanged("PercentElapsed");
        //        }
        //    }
        //}

        //public int CompletedCount
        //{
        //    get
        //    {
        //        return _completedCount;
        //    }
        //    private set
        //    {
        //        if (_completedCount == value)
        //            return;
        //        _completedCount = value;
        //        OnPropertyChanged("CompletedCount");
        //    }
        //}
        public bool IsAdonetVisible
        {
            get { return isAdonetVisible; }
            set { isAdonetVisible = value;
            OnPropertyChanged("IsAdonetVisible");
            }
        }

        public string Error
        {
            get { return error; }
            set { error = value;
            OnPropertyChanged("Error");
            }
        }

        public bool IsWorking
        {
            get { return _isWorking; }
            set
            {
                _isWorking = value;
                OnPropertyChanged("IsWorking");

            }
        }

        public string Version
        {
            get { return version; }
            set { version = value;
            OnPropertyChanged("Version");
            }
        }
       

        public bool BtnLogginEnabled
        {
            get { return btnLogginEnabled; }
            set { btnLogginEnabled = value;
            OnPropertyChanged("BtnLogginEnabled");

            }
        }

        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                BtnLogginEnabled = true;
                Error = "";
                WarningVisibility = false;
                OnPropertyChanged("UserName");
            }
        }

        public string Password
        {
            get { return _password; }
            set
            {
                _password = value;

                if (!string.IsNullOrEmpty(value))
                {
                    if (value == ParametersDatabase.DeaultPassword)
                    {
                        if (!string.IsNullOrEmpty(UserName))
                            if (UserName != ParametersDatabase.DeaultUser)
                          Setnewpasswordvisibility = true;
                    }
                }
                else
                    Setnewpasswordvisibility = false;
                    
                
                OnPropertyChanged("Password");
            }
        }

        public string NewPassword
        {
            get { return _newPassword; }
            set { _newPassword = value;
            OnPropertyChanged("NewPassword");
            
            }
        }
        public bool BorderVisibility
        {
            get { return _borderVisibility; }
            set
            {
                _borderVisibility = value;
                this.OnPropertyChanged("BorderVisibility");
            }
        }

        public ParametresModel ParametersDatabase
        {
            get { return _parametersDatabase; }
            set
            {
                _parametersDatabase = value;
                OnPropertyChanged("ParametersDatabase");
            }
        }

        public bool MenuDataPreparationVisibility
        {
            get { return _menuDataPreparationVisibility; }
            set { _menuDataPreparationVisibility = value;
            OnPropertyChanged("MenuDataPreparationVisibility");
            }
        }

        public bool SortieFactureVisibility
        {
            get { return _sortieFactureVisibility; }
            set { _sortieFactureVisibility = value;
            OnPropertyChanged("SortieFactureVisibility");
            }
        }
        public List<LigneFactureModel> LigneFatureListe
        {
            get { return _LigneFatureListe; }
            set { _LigneFatureListe = value;
            OnPropertyChanged("LigneFatureListe");
            }
        }

        public bool Setnewpasswordvisibility
        {
            get { return _setnewpasswordvisibility; }
            set { _setnewpasswordvisibility = value;
            this.OnPropertyChanged("Setnewpasswordvisibility");
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

        public bool SeparatorDataVisibility
        {
            get { return _separatorDataVisibility; }
            set { _separatorDataVisibility = value;
            OnPropertyChanged("SeparatorDataVisibility");
            }
        }

        public bool RebusFactureVisibility
        {
            get { return _rebusFactureVisibility; }
            set { _rebusFactureVisibility = value;
            OnPropertyChanged("RebusFactureVisibility");
            }
        }

        public bool SeparatorSortieVisibility
        {
            get { return _separatorSortieVisibility; }
            set
            {
                _separatorSortieVisibility = value;
                OnPropertyChanged("SeparatorSortieVisibility");
            }
        }

        public bool SeparatortoolB_1
        {
            get { return _separatortoolB_1; }
            set { _separatortoolB_1 = value;
            OnPropertyChanged("SeparatortoolB_1");
            }
        }


        public bool SeparatortoolB_2
        {
            get { return _separatortoolB_2; }
            set { _separatortoolB_2 = value;
            OnPropertyChanged("SeparatortoolB_2");
            }
        }

        public bool SeparatortoolB_4
        {
            get { return _separatortoolB_4; }
            set
            {
                _separatortoolB_4 = value;
                OnPropertyChanged("SeparatortoolB_4");
            }
        }


        public bool SeparatortoolB_3
        {
            get { return _separatortoolB_3; }
            set { _separatortoolB_3 = value;
            OnPropertyChanged("SeparatortoolB_3");
            }
        }

        public bool SeparatorDataref_1
        {
            get { return _separatorDataref_1; }
            set { _separatorDataref_1 = value;
            OnPropertyChanged("SeparatorDataref_1");
            }
        }

        public bool SeparatorDataref_3
        {
            get { return _separatorDataref_3; }
            set
            {
                _separatorDataref_3 = value;
                OnPropertyChanged("SeparatorDataref_3");
            }
        }

        public bool SeparatorDataref_2
        {
            get { return _separatorDataref_2; }
            set
            {
                _separatorDataref_2 = value;
                OnPropertyChanged("SeparatorDataref_2");
            }
        }

        public bool SeparatorDataref_4
        {
            get { return _separatorDataref_4; }
            set
            {
                _separatorDataref_4 = value;
                OnPropertyChanged("SeparatorDataref_4");
            }
        }
      
        public bool WarningVisibility
        {
            get { return _warningVisibility; }
            set { _warningVisibility = value;
            OnPropertyChanged("WarningVisibility");
            }
        }


        public bool LabelmsgBarVisibility
        {
            get { return labelmsgBarVisibility; }
            set { labelmsgBarVisibility = value;
            OnPropertyChanged("LabelmsgBarVisibility");
            }
        }

        public bool FactureVisibility
        {
            get { return _factureVisibility; }
            set { _factureVisibility = value;
            OnPropertyChanged("FactureVisibility");
            }
        }


        public bool HistoricFactureVisibility
        {
            get { return _historicFactureVisibility; }
            set { _historicFactureVisibility = value;
            OnPropertyChanged("HistoricFactureVisibility");
            }
        }

        bool journalventeToolbarVisibility;

        public bool JournalventeToolbarVisibility
        {
            get { return journalventeToolbarVisibility; }
            set { journalventeToolbarVisibility = value;
            OnPropertyChanged("JournalventeToolbarVisibility");
            }
        }


        public bool ParametersVisibility
        {
            get { return _parametersVisibility; }
            set { _parametersVisibility = value;
            OnPropertyChanged("ParametersVisibility");
            }
        }


        public bool DatarefVisibility
        {
            get { return _datarefVisibility; }
            set { _datarefVisibility = value;
            OnPropertyChanged("DatarefVisibility");
            }
        }

        public bool AdministrationVisibility
        {
            get { return _administrationVisibility; }
            set { _administrationVisibility = value;
            OnPropertyChanged("AdministrationVisibility");
            }
        }

        public bool MenuOperationVisibility
        {
            get { return _menuOperationVisibility; }
            set { _menuOperationVisibility = value;
            OnPropertyChanged("MenuOperationVisibility");
            }
        }


        public bool MenuManagementVisibility
        {
            get { return _menuManagementVisibility; }
            set { _menuManagementVisibility = value;
            OnPropertyChanged("MenuManagementVisibility");
            }
        }

        public bool SeparatormanagementVisibility
        {
            get { return _separatormanagementVisibility; }
            set { _separatormanagementVisibility = value;
            OnPropertyChanged("SeparatormanagementVisibility");
            }
        }


        public bool SeparatorOperationVisibility
        {
            get { return _separatorOperationVisibility; }
            set { _separatorOperationVisibility = value;
            OnPropertyChanged("SeparatorOperationVisibility");
            }
        }


        public bool MenuDatarefuserVisibility
        {
            get { return _menuDatarefuserVisibility; }
            set
            {
                _menuDatarefuserVisibility = value;
                OnPropertyChanged("MenuDatarefuserVisibility");
            }
        }

        public bool MenuDatarefProduitVisibility
        {
            get { return _menuDatarefProduitVisibility; }
            set
            {
                _menuDatarefProduitVisibility = value;
                OnPropertyChanged("MenuDatarefProduitVisibility");
            }
        }

        public bool MenuDatarefClientVisibility
        {
            get { return _menuDatarefClientVisibility; }
            set
            {
                _menuDatarefClientVisibility = value;
                OnPropertyChanged("MenuDatarefClientVisibility");
            }
        }

        public bool MenuDatarefFactureVisibility
        {
            get { return _menuDatarefFactureVisibility; }
            set
            {
                _menuDatarefFactureVisibility = value;
                OnPropertyChanged("MenuDatarefFactureVisibility");
            }
        }

        public bool MenuDatarefSocieteVisibility
        {
            get { return _menuDatarefSocieteVisibility; }
            set
            {
                _menuDatarefSocieteVisibility = value;
                OnPropertyChanged("MenuDatarefSocieteVisibility");
            }
        }

        public bool ToolbarVisibility
        {
            get { return _toolbarVisibility; }
            set { _toolbarVisibility = value;
            OnPropertyChanged("ToolbarVisibility");
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
                OnPropertyChanged("MouseCursor");

            }
        }

        public UtilisateurModel UserSelected
        {
            get { return _userSelected; }
            set { _userSelected = value  ;
            OnPropertyChanged("UserSelected");
            }
        }

        #endregion

        #region ICOMMAND

        public RelayCommand AdminCommand
        {

            get
            {
                if (this.adminCommand == null)
                {
                    this.adminCommand = new RelayCommand(param => this.canShowAdmin());
                }
                return this.adminCommand;
            }


        }

         public RelayCommand LogginCommand
           {

                 get
                {
                    if (this.logginCommand == null)
                    {
                        this.logginCommand = new RelayCommand(param => this.canLoggin(), param => this.canExecuteLoggin());
                    }
                    return this.logginCommand;
                }
           
           }

         public RelayCommand LogginOfCommand
         {
             get
             {
                 if (this.logginOfCommand == null)
                 {
                     this.logginOfCommand = new RelayCommand(param => this.canLogOf());
                 }
                 return this.logginOfCommand;
             }

         }


         public RelayCommand CancelCommand
         {

             get
             {
                 if (this.cancelCommand == null)
                 {
                     this.cancelCommand = new RelayCommand(param => this.canCel());
                 }
                 return this.cancelCommand;
             }

         }


         public RelayCommand ExitCommand
         {

             get
             {
                 if (this.exitCommand == null)
                 {
                     this.exitCommand = new RelayCommand(param => this.canExit());
                 }
                 return this.exitCommand;
             }

         }


         public RelayCommand DatasRefsCommand
         {

             get
             {
                 if (this.datasRefsCommand == null)
                 {
                     this.datasRefsCommand = new RelayCommand(param => this.canLoadDataRefs());
                 }
                 return this.datasRefsCommand;
             }

         }

         public RelayCommand ParameterCommand
         {

             get
             {
                 if (this.parameterCommand == null)
                 {
                     this.parameterCommand = new RelayCommand(param => this.canShowParameter());
                 }
                 return this.parameterCommand;
             }

         }
         //

         public RelayCommand FacturationCommand
         {

             get
             {
                 if (this.facturationCommand == null)
                 {
                     this.facturationCommand = new RelayCommand(param => this.canShowFacturation(), param => this.canDExecuteShowNewFActure());
                 }
                 return this.facturationCommand;
             }

         }
         //

         public RelayCommand HistoricfacturationCommand
         {

             get
             {
                 if (this.historicfacturationCommand == null)
                 {
                     this.historicfacturationCommand = new RelayCommand(param => this.canShowHistoFacturation(), param => this.canDExecuteShowHst());
                 }
                 return this.historicfacturationCommand;
             }

         }
         // private RelayCommand JournalventeCommand;

      


         public RelayCommand SortiefacturationCommand
         {

             get
             {
                 if (this.sortiefacturationCommand == null)
                 {
                     this.sortiefacturationCommand = new RelayCommand(param => this.canShowSortieFacturation(), param => this.canDExecuteShowSorties());
                 }
                 return this.sortiefacturationCommand;
             }

         }
         //
         public RelayCommand JournalVenteCommand
         {

             get
             {
                 if (this.journalVenteCommand == null)
                 {
                     this.journalVenteCommand = new RelayCommand(param => this.canShowJournalVente(), param => this.canDExecuteShowJvente());
                 }
                 return this.journalVenteCommand;
             }

         }


         public RelayCommand AproposCommand
         {

             get
             {
                 if (this.aproposCommand == null)
                 {
                     this.aproposCommand = new RelayCommand(param => this.canShowApropos());
                 }
                 return this.aproposCommand;
             }

         }

         public RelayCommand AdministrationCommand
         {

             get
             {
                 if (this.administrationCommand == null)
                 {
                     this.administrationCommand = new RelayCommand(param => this.canShowAdministration());
                 }
                 return this.administrationCommand;
             }

         }

         //

         public RelayCommand RebusFacturesCommand
         {

             get
             {
                 if (this.rebusFacturesCommand == null)
                 {
                     this.rebusFacturesCommand = new RelayCommand(param => this.canShowFactureRebus());
                 }
                 return this.rebusFacturesCommand;
             }

         }

        
         //affiche les elements dentete de factures;

         public RelayCommand FactureCommand
         {
             get
             {
                 if (this.factureCommand == null)
                 {
                     this.factureCommand = new RelayCommand(param => this.canShowFactureElement());
                 }
                 return this.factureCommand;
             }
         }


         public RelayCommand ProduitCommand
         {
             get
             {
                 if (this.produitCommand == null)
                 {
                     this.produitCommand = new RelayCommand(param => this.canShowProduits());
                 }
                 return this.produitCommand;
             }
         }

         //userGuideCommand
         public RelayCommand UserGuideCommand
         {
             get
             {
                 if (this.userGuideCommand == null)
                 {
                     this.userGuideCommand = new RelayCommand(param => this.canShowUserGuide());
                 }
                 return this.userGuideCommand;
             }
         }

         public RelayCommand UserCommand
         {
             get
             {
                 if (this.userCommand == null)
                 {
                     this.userCommand = new RelayCommand(param => this.canShowUser());
                 }
                 return this.userCommand;
             }
         }

         public RelayCommand SocieteCommand
         {
             get
             {
                 if (this.societeCommand == null)
                 {
                     this.societeCommand = new RelayCommand(param => this.canShowSociete());
                 }
                 return this.societeCommand;
             }
         }

         public RelayCommand ClientCommand
         {
             get
             {
                 if (this.clientCommand == null)
                 {
                     this.clientCommand = new RelayCommand(param => this.canShowClient());
                 }
                 return this.clientCommand;
             }
         }
         //   private RelayCommand normaleStyleCommand;
        // private RelayCommand customStyleCommand;

         public RelayCommand NormaleStyleCommand
         {
              get
            {
                if (this.normaleStyleCommand == null)
                {
                    this.normaleStyleCommand = new RelayCommand(param => this.ChangeSkin(param), param => true);
                }
                return this.normaleStyleCommand;
            }

           
         }

        
        #endregion

        #region DATA Error Info Menbers
         //public string Error
         //{
         //    get { throw new NotImplementedException(); }
         //}


         //public string this[string columName]
         //{
         //    get
         //    {
         //        if (columName == "UserName")
         //        {
         //            if (string.IsNullOrEmpty(UserName))
         //            {
         //                return "Le loggin Ne doit Pas etre vide";
         //            }
         //        }

         //        if (columName == "Password")
         //        {
         //            if (string.IsNullOrEmpty( Password))
         //            {
         //                return "Le Mot de Passe Ne doit Pas etre vide";
         //            }
         //        }
         //        return "";

         //    }
         //}
         #endregion

        #region Private Methodes


         void loadcompany()
         {
             
                 try
                 {
                   
                    currentcompany=societeservice.Get_SOCIETE_DEFAULT();
                    GlobalDatas.DefaultCompany = currentcompany;
                   // Utils.logConnection("Chargement de la société par défaul réussie", "");
                    GlobalDatas.listeCompany = societeservice.Get_SOCIETE_LIST();
                   // BtnLogginEnabled = true ;
                  //  GlobalDatas.isConnectionOk = true;

                    if (GlobalDatas.listeCompany != null)
                    {
                        if (GlobalDatas.listeCompany.Count > 1)
                            Utils.logConnection("Chargement Liste des sociétés", "");
                    }
                   
                 }
                 catch (Exception ex)
                 {
                     
                     if (ex.Message.Contains("Access denied "))
                     {
                         MessageBox.Show("Impossible de se connecter à la base de données\n Vérifier les paramètres de connexion");
                         BtnLogginEnabled = true;
                         //isnewwLoggin = true;
                     }
                     else
                         MessageBox.Show(ex.Message.ToString());
                         //view.ViewModel.Message = ex.Message.ToString();
                     Utils.logConnection("Erreur lors Chargement  des sociétés ->" +ex.Message  , "");
                    
                     this.MouseCursor = null;
                     GlobalDatas.isConnectionOk = false;
                 }
                 this.MouseCursor = null;

                    ProgressBarVisibility = false;


           

             //worker.RunWorkerAsync();
         }
         void loadLogginView()
         {
             //ContenRegion = _container.Resolve<UserLoggin>();
                 logginView = new UserLoggin () ;
                 //logginView.setnewpasswordvisibility = false;
                 logginView.ViewModel = this;
                 ContenRegion = logginView;
                 //IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion,
                 //                                     () => logginView);
            

         }

         private void InitializeModel()
        {
            _userName ="";
            _password ="";
            _newPassword = string.Empty;
            //_newpassword = string.Empty;

        }

         private void InitializePropertyNames()
         {
             _firstNamePropertyName = this.GetPropertyName(p => p.UserName);
             _lastNamePropertyName = this.GetPropertyName(p => p.Password);
            // _otherNamePropertyName = this.GetPropertyName(p => p._newpassword);
         }
        #endregion

        #region METHODS

         protected void updateContenRegion(object sender,EventArgs e)
         {

             UserAffiche uaffiche = new UserAffiche();
             ContenRegion = uaffiche;
             FactureVisibility = false ;
             SeparatortoolB_1 = false ;

         }

         
      #region Loggin methods
         
        
         
      private void canLoggin(){

          Utils.logConnection(" debut fonction loggins", "");
          UtilisateurModel userConnected = null;
          bool isVerifloggin = false;
          try
          {
              this.MouseCursor = Cursors.Wait;
            
             IsWorking = true;
             
              if (isnewwLoggin)
              {
                 
                  userService = new UtilisateurModel();
                  //nouvelle connection apres reinitialisation 
               
                  if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                  {
                      if (UserName == ParametersDatabase.DeaultUser)
                      {
                          //if (DatabaseList != null && DatabaseList.Count>0)
                          if (Password == ParametersDatabase.DeaultPassword)
                          {
                              Utils.logConnection("Modification des parametres base données @Admin", "");
                              //Utils.logUserActions("Connection interface de parametres :" + RegionNames.ContentRegion, "");
                             // _injectSingleViewService.ClearViewsFromRegion(RegionNames.ContentRegion);
                              ContenRegion = null;

                              BorderVisibility = true;
                              Error = "";
                              WarningVisibility = false;
                              MenuManagementVisibility = true;
                              ParametersVisibility = true;
                              //IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion,
                              //                               () => uaffiche);
                              IsWorking = false;
                              IsnewwLoggin = false;
                          }
                          else
                          {
                              Error = "Erreur mot de passe";
                              WarningVisibility = true;
                              Utils.logUserActions(string.Format("Echec Connection mot passe incorrect [{0}] ", Password), "");
                          }
                      }
                      else
                      {
                          Error = "Erreur loggin";
                          WarningVisibility = true;
                          Utils.logUserActions(string.Format("Echec Connection loggin  incorrect [{0}] ", UserName), "");
                      }
                  }

              }
              else
              {
                 // Utils.logConnection(" teste connection à la bd", "");
                  
                  ////ParametersDatabase = GlobalDatas.dataBasparameter;

                  bool verifNewPassWord = false;
                  userService = new UtilisateurModel();
               

                  if (!string.IsNullOrEmpty(UserName) && !string.IsNullOrEmpty(Password))
                  {
                      if (UserName == ParametersDatabase.DeaultUser)
                      {
                          if (Password == ParametersDatabase.DeaultPassword)
                          {
                            
                              
                              BorderVisibility = true;
                              ContenRegion = null;
                              Error = "";
                              WarningVisibility = false;
                              MenuManagementVisibility = true;
                              ParametersVisibility = true;
                                        
                              IsWorking = false;
                          }
                          else
                          {
                              Error = "Erreur de mot de passe";
                              WarningVisibility = true;
                              Utils.logConnection(string.Format("Echec Connection mot passe incorrect [{0}] ", UserName), "");
                          }


                      }
                     else
                      {

                          if (userService.ExistUtilsateurLogginName(UserName))
                         {
                             if (!string.IsNullOrEmpty(Password) && Password == ParametersDatabase.DeaultPassword)
                             {
                                 if (!string.IsNullOrEmpty(NewPassword))
                                 {
                                   
                                     userConnected = userService.GetUtilisateur_newLogg(UserName, Password, NewPassword);
                                     verifNewPassWord = true;
                                     Utils.logConnection(" Modification mot de passe réussie ", "");
                                 }
                                 else
                                 {
                                     CustomExceptionView view = new CustomExceptionView();
                                     view.Owner = Application.Current.MainWindow;
                                     IsWorking = false;
                                     view.Title = GlobalDatas.DisplayLanguage["shellVm_MsgSaisireMotpTitre"].ToString();
                                     view.ViewModel.Message = GlobalDatas.DisplayLanguage["shellVm_MsgSaisireMotpcontent"].ToString();
                                     view.ShowDialog();
                                     userConnected = null;
                                     verifNewPassWord = false;
                                 }
                             }
                             else
                             {
                                 Utils.logConnection("tentative de connection  ", "");
                                 userConnected = userService.UTILISATEUR_LOGGIN(UserName, Password);
                                 verifNewPassWord = true;
                                 isVerifloggin = true;
                                
                                 

                             }

                             bool verifIsOk = false;
                             if (userConnected != null)
                             {

                                 if (!userConnected.IsLockCount)
                                 {
                                     // verifie si compte temporarire
                                     //isTemporaryAccount(userConnected.Id , userConnected.IdProfile )
                                     ProfileDateModel profileDateservice = new ProfileDateModel();
                                    
                                     ProfileDateSelected = profileDateservice.GetProfileDate(userConnected.Id, userConnected.IdProfile);
                                     if (ProfileDateSelected != null)
                                     {
                                           TimeSpan differenceDate = (DateTime)ProfileDateSelected.Datefin - DateTime.Parse(DateTime.Now.ToShortDateString());
                                           if (differenceDate.TotalDays < 0)
                                           {
                                               CustomExceptionView view = new CustomExceptionView();
                                               view.Owner = Application.Current.MainWindow;
                                               IsWorking = false;
                                               view.Title = LanguageHelper.shellVm_MsgTitreCompteStatus;
                                               IsWorking = false;
                                               view.ViewModel.Message = LanguageHelper.shellVm_MsgcontentCompteStatus;
                                               view.ShowDialog();
                                              valuesCmdloggin = false;
                                               userService.Utilsateur_LockAcount(UserSelected.Id);
                                               ProfileDateSelected = null;
                                               verifIsOk = false;
                                               return;
                                              
                                           }
                                          else
                                            verifIsOk = true;

                                     }else
                                         verifIsOk = true;
                                     if (string.IsNullOrEmpty(userConnected.Nom))
                                         verifIsOk = false;
                                    

                                     if (verifIsOk)
                                     {
                                        // Utils.logUserActions(string.Format("utilisateur Connecter   incorrect [{0}] ", userConnected.Nom + " " + userConnected.Prenom), "");

                                   

                                         valuesCmdloggin = true;
                                         GlobalDatas.currentUser = userConnected;

                                         UserSelected = userConnected;
                                         CurrentDroit = UserSelected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("historic"));
                                         factureservice = new FactureModel();
                                        // Utils.logConnection(" chargement des vues de l'utilisateur ", "");

                                         #region VERIFICATIOn DROIT

                                         SetVisibledRight(userConnected.Profile.ShortName, userConnected.Profile.Droit);

                                         societeservice = new SocieteModel();
                                         currentcompany = societeservice.Get_SOCIETE_DEFAULT();
                                         GlobalDatas.DefaultCompany = currentcompany;
                                         Utils.GetParametersFromDatabase(currentcompany.IdSociete);
                                         #endregion

                                         IsWorking = false;
                                         IsAdonetVisible = true;
                                         ProgressBarVisibility = true;
                                         for (int i = 0; i < 50; i += 5)
                                         {
                                             logginView.LBInfos = "Chargement des paramètres";
                                             logginView.ValueProgressBar = i;
                                             Thread.Sleep(100);
                                         }


                                         //CrystalReportFacture rpt = new CrystalReportFacture();

                                        // Utils.logConnection(" chargement du runtime chrystal report ", "");
                                         ProgressBarVisibility = false;
                                         IsAdonetVisible = false;
                                        // Utils.logConnection(" chargement Ecran accueille et Historique des factures ", "");



                                         UserAffiche uaffiche = new UserAffiche();
                                         logginView.ViewModel = this;


                                         ContenRegion = uaffiche;
                                         BorderVisibility = true;
                                         ToolbarVisibility = true;
                                         CleanCache(); // netoyage du cache des données
                                         Utils.logConnection("Connection à la base de données réussie  [  " + userConnected.Nom + "]", "");
                                         Utils.logConnection("-------------------------------- ", "");
                                         //chargement societe par default
                                        
                                             EventArgs e1 = new EventArgs();
                                             Shell.OnChange(e1);
                                         
                                     }

                                 }
                                 else
                                 {
                                     CustomExceptionView view = new CustomExceptionView();
                                     view.Owner = Application.Current.MainWindow;
                                     IsWorking = false;
                                     view.Title = LanguageHelper.shellVm_MsgCompteLockTitre;
                                     view.ViewModel.Message = LanguageHelper.shellVm_MsgCompteLockcontent;
                                     view.ShowDialog();
                                    valuesCmdloggin = false;

                                     Utils.logConnection(" Connection réussie ,mais compte vérouillé utilisateur ", "");
                                 }
                             }
                             else
                             {
                                 if (verifNewPassWord)
                                 {
                                     CustomExceptionView view = new CustomExceptionView();
                                     view.Owner = Application.Current.MainWindow;
                                     view.Title = LanguageHelper.shellVm_MsgBadMotpTitre;
                                     IsWorking = false;
                                     view.ViewModel.Message =LanguageHelper.shellVm_MsgBadMotpcontent;
                                     view.ShowDialog();
                                     Utils.logConnection("Echec connection , mot passe incorrect<<loggin>>  ", "");
                                 }
                             }
                         }
                          else
                          {
                              CustomExceptionView view = new CustomExceptionView();
                              view.Owner = Application.Current.MainWindow;
                              view.Title = LanguageHelper.shellVm_MsgBadLogginTitre;
                              IsWorking = false;
                              view.ViewModel.Message = LanguageHelper.shellVm_MsgBadLoggincontent;
                              view.ShowDialog();
                              Utils.logConnection("Echec connection , loggin incorrect<<loggin>>  ", "");
                          }

                      }
                  

                  }
                  IsWorking = false;
              }

          }
          catch (Exception ex)
          {
              CustomExceptionView view = new CustomExceptionView();
              view.Owner = Application.Current.MainWindow;
              //view.Title = GlobalDatas.DisplayLanguage["shellVm_MsgErrorConnectTitre"].ToString();
              view.ViewModel.Message = ex.Message;
              IsWorking = false;
              //if (ex.Message.Contains("Access denied "))
              //    view.ViewModel.Message = GlobalDatas.DisplayLanguage["shellVm_MsgErrorConnectcontent1"].ToString();
              //else
              //    view.ViewModel.Message = GlobalDatas.DisplayLanguage["shellVm_MsgErrorConnectcontent2"].ToString();
              view.ShowDialog();
              if (!isVerifloggin)
                  Utils.logConnection("Fatal error Echec connection , loggin incorrect<<loggin>>  " +ex.Message , "");
          }
      
      }

        bool canExecuteLoggin(){
            return valuesCmdloggin;
            
        }

        private void canLogOf()
        {
           
            BorderVisibility = false;
            UserLoggin logginView = new UserLoggin();
            logginView.ViewModel= this;
           
            Password = string.Empty;
            NewPassword = string.Empty;
          
            ContenRegion = logginView;
            ToolbarVisibility = false;
            if (ParametersDatabase.Dejautiliser == "1")
            {
                isnewwLoggin = false;
                Utils.logConnection("Deconnection ,   ", "");
               
            }
            SetVisibleMenuFalse();
        }

        private void canCel()
        {

            UserName = string.Empty;
            Password  =string .Empty ;
            NewPassword = string.Empty;
            valuesCmdloggin = true;
        }

         #endregion


        // modifier le style
        private void ChangeSkin(object param)
        {
            ResourceDictionary dict = Application.Current.Resources.MergedDictionaries[0];
            //ResourceDictionary dict = new ResourceDictionary();

            if (param.Equals("Shiny"))
            {
              //  dict.Source = new Uri("pack://application:,,,/AdventureWorks.Resources;component/Themes/ShinyBlue.xaml");
                dict.Source = new Uri("..\\Resources\\Global.xaml",
                               UriKind.Relative);
            }
            else
            {
                dict.Source = new Uri("..\\Resources\\ShinyBlue.xaml",
                           UriKind.Relative);

              //  dict.Source = new Uri("pack://application:,,,/AdventureWorks.Resources;component/Themes/BureauBlue.xaml");
            }

           
        }

        private void canLoadDataRefs()
        {
           
            try
            {
                //DataReferencesViewModal view = new DataReferencesViewModal(wpfParent);
                DataReference_Views view = new DataReference_Views(wpfParent);
                ContenRegion = view;
                FactureVisibility = false;
               // view.Owner = Application.Current.MainWindow;
               // view.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE DATAREFS";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
           
        }

        void canShowAdmin()
        {
            try
            {

                Admin view = new Admin();
                ContenRegion = view;
                FactureVisibility = false;
                SeparatortoolB_1 = false;
                FactureVisibility = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE ADMINISTRATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        private void canShowParameter()
        {
            try
            {
                
                Parameters view = new Parameters();
                ContenRegion = view;
                FactureVisibility = false;
                SeparatortoolB_1 = false;
                FactureVisibility = false;
                
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE PARAMETRES";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        private void canShowFacturationss()
        {
           
            try
            {
                
                    FactureModel newFacture = new FactureModel();
                    newFacture.IdFacture = 0;
                    NewFactureEdition view = new NewFactureEdition(null ,0,null);
                    view.Owner = Application.Current.MainWindow;
                   
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE NOUVELLE FACTURE";
                view.ViewModel.Message ="erreure inattendues san conséquence, cliquer à nouveau";
                view.ShowDialog();
            }
        
        }

        bool canDExecuteShowNewFActure()
        {
            bool val = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
                {
                    if (GlobalDatas.IsArchiveSelected)
                        val = false;
                    else
                        val = true;
                }
            }
            return val;
            
        }
        // show facture modale
        private void canShowFacturation()
        {
            try
            {
                bool testeValues = true;

               
                    //{
                    FactureModel newFacture = new FactureModel();
                    newFacture.IdFacture = 0;
                    NewFactureEdition view = new NewFactureEdition(null, 0, null);
                    view.Owner = Application.Current.MainWindow;
                    //GlobalDatas.currentFacture = null;
                    view.ShowDialog();




                    EventRefreshGridHistoric hst = new EventRefreshGridHistoric();
                    //if (view.facturesListes != null && view.facturesListes.Count > 0)
                    //    hst.facture = view.facturesListes;
                    //else hst.facture = null;
                    if (view.isOperation)
                        hst.typeOperation = "new";
                    else hst.facture = null;
                    EventArgs e1 = new EventArgs();
                    hst.OnChangeList(e1);
              
                

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE CHARGEMENT VUE FACTURE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        private void canShowSortieFacturation()
        {
            try
            {
                
                Facturation_Sortie view = new Facturation_Sortie();
                ContenRegion = view;
                FactureVisibility = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE CHARGEMENT VUE SORTIE FACTURE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        bool canDExecuteShowSorties()
        {
            if (GlobalDatas.IsArchiveSelected)
                return false;
            else
                return true;
        }

        private void canShowHistoFacturation()
        {
            try
            {

                Facturation_List view = new Facturation_List(wpfParent);
                ContenRegion = view;
                if (isgetdroitfacture)
                {
                    FactureVisibility = true;
                    SeparatortoolB_1 = true;
                }
                
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE HISTORIC DES FACTURES";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        bool canDExecuteShowHst()
        {
            
                return true;
        }


        void canShowJournalVente()
        {
            try
            {
                FactureVisibility = false;
                DataRef_JournalVente view = new DataRef_JournalVente(wpfParent);
                ContenRegion = view;
                //if (isgetdroitfacture)
                //{
                //    FactureVisibility = true;
                //    SeparatortoolB_1 = true;
                //}

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE HISTORIC DES FACTURES";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        bool canDExecuteShowJvente()
        {
            if (GlobalDatas.IsArchiveSelected)
                return false;
            else
                return true;
        }


        private void canShowApropos()
        {
            About vf = new About();
            vf.Owner = Application.Current.MainWindow;
            vf.ShowDialog();
        }

        private void canShowAdministration()
        {

           
            try
            {
                
                AdministrationDatas view = new AdministrationDatas() ;
                ContenRegion = view;
                FactureVisibility = false ;
                SeparatortoolB_1 = false ;
               
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE ADMINISTRATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }

          

        }

        void canShowFactureRebus()
        {
            FactureVisibility = false;
            Facture_rebus view = new Facture_rebus();
            view.Owner = Application.Current.MainWindow;
            view.ShowInTaskbar = false;
            view.Topmost = true;
            view.ResizeMode = ResizeMode.CanResizeWithGrip;
           view.ShowDialog();

        }

        #region Data references
        
      
        void canShowSociete()
        {
            try
            {
               // New_Dataref_Company view = new New_Dataref_Company(wpfParent);
                //view.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE SOCIETE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void canShowUser()
        {
            try
            {
               // new_Dataref_Users view = new new_Dataref_Users(wpfParent);
                //view.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE UTILISATEUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void canShowProduits()
        {
            try
            {
                //New_Dataref_Produits view = new New_Dataref_Produits(wpfParent);
                //view.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE PRODUIT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void canShowFactureElement()
        {
            try
            {
                New_Dataref_Elementfactures view = new New_Dataref_Elementfactures();
                view.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE ENTETE FACTURE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void canShowClient()
        {
            try
            {
                //New_Dataref_Client view = new New_Dataref_Client(wpfParent);
                //view.ShowDialog();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE CLIENT ";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }
        #endregion

        void canShowUserGuide()
        {
            MessageBox.Show("Pas encore disponible ");
        }

        private void canExit()
        {


            if (MessageBox.Show(LanguageHelper.MNuFile_quitter, "Confirm ", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                Application.Current.Shutdown();
        
        }

        #endregion

        #region Region Others


        bool testerConection()
        {
            return true;
        }
      
        void initLabels()
        {
            HistoricFactureVisibility = false;
            DatarefVisibility = false;
            ParametersVisibility = false;
            MenuOperationVisibility = false;
            MenuManagementVisibility = false;
            SeparatorOperationVisibility = false;
            SeparatormanagementVisibility = false;
            AdministrationVisibility = false;
            SeparatorDataVisibility = false;
            AdminVisibility = false;
            SeparatorDatarefAdmin = false;
            JournalVenteVisibility = false;
            JournalventeToolbarVisibility = false;

            _menuDatarefSocieteVisibility = false;
            _menuDatarefuserVisibility = false;
            _menuDatarefProduitVisibility = false;
            _menuDatarefClientVisibility = false;
            _menuDatarefFactureVisibility = false;


            _separatorDataref_1 = false;
            _separatorDataref_2 = false;
            _separatorDataref_3 = false;
            _separatorDataref_4 = false;
            SeparatorJv = false;

            ToolbarVisibility = false;
            SeparatortoolB_2 = false;
            SeparatortoolB_1 = false;
            SeparatorDataref_4 = false;
        }

        void CleanCache()
        {
          

            CacheDatas.ui_currentdroitUserInterface = null;
            CacheDatas.ui_profilCache = null;
            CacheDatas.ui_UsersCache = null;
            CacheDatas.ui_currentdroitCompanyInterface = null;
            CacheDatas.ui_currentdroitFistoricFacturesInterface = null;

            CacheDatas.ui_currentdroitClientInterface = null;
            CacheDatas.ui_ClientCompte = null;
            CacheDatas.ui_ClientExonerations = null;
            CacheDatas.ui_ClientDevises = null;
            CacheDatas.ui_ClientClients = null;
            CacheDatas.ui_ProduitProduits = null;
            CacheDatas.ui_ClientTaxes = null;
            CacheDatas.ui_ClientObjetS = null;
            CacheDatas.ui_ClientExploitations = null;

            CacheDatas.lastUpdatefacture = null;
            CacheDatas.Listefactures = null;
            CacheDatas.ui_Statut = null;
            CacheDatas.ui_Hst_factureClient = null;

            CacheDatas.ui_currentdroitFactureElementInterface = null;
            CacheDatas.taxeDefault = null;
            CacheDatas.deviseDefault = null;
        }
        #endregion

        #region SET VISIBLE BY RIGHT

        void SetVisibleMenuFalse()
        {
            FactureVisibility = false ;
            MenuOperationVisibility = false;
            SeparatortoolB_1 = false;
            HistoricFactureVisibility = false;
            SeparatorOperationVisibility = false;
            SeparatortoolB_2 = false;
            DatarefVisibility = false;
            ParametersVisibility = false;
            MenuManagementVisibility = false;
            SortieFactureVisibility = false;
            SeparatorSortieVisibility = false;
            SeparatormanagementVisibility = false;
            MenuDataPreparationVisibility = false;
            SeparatorDataVisibility = false;
            AdministrationVisibility = false;
            JournalVenteVisibility = false;
            JournalventeToolbarVisibility = false;

            SeparatorDataref_1 = false;
            SeparatorDataref_2 = false;
            SeparatorDataref_3 = false;
            SeparatortoolB_4 = false;

            MenuDatarefSocieteVisibility = false;
            MenuDatarefuserVisibility = false;
            MenuDatarefProduitVisibility = false;
            MenuDatarefClientVisibility = false;
            MenuDatarefFactureVisibility = false;

          
        }

        void SetVisibledRight(string profile, List<DroitModel> Droit)
        {
           
            switch (profile.ToLower().Trim())
            {
                case "admin":
                    {
                         //compte administrateur
                        if (Droit.Exists(dr => dr.IdVues == 4))
                        {
                           // FactureVisibility = true;
                            MenuOperationVisibility = true;
                            SeparatortoolB_1 = false ;
                            isgetdroitfacture = true;

                        }


                        if (Droit.Exists(dr => dr.IdVues == 5))
                        {
                            MenuOperationVisibility = true;

                            HistoricFactureVisibility = true  ;
                            SeparatorOperationVisibility = true;
                            SeparatortoolB_2 = true;
                        }

                        if (Droit.Exists(dr => dr.IdVues ==15))
                        {
                            JournalVenteVisibility = true;
                            JournalventeToolbarVisibility = true;
                            SeparatortoolB_4 = true;
                            SeparatorJv = true;
                            
                        }

                        if (Droit.Exists(dr => dr.IdVues == 6))
                        {
                            bool iscompany = false;
                            bool isUser = false;

                            List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;
                            DatarefVisibility = true;
                            MenuManagementVisibility = true;
                            if (sousVues != null)
                            {
                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                                {
                                    MenuDatarefSocieteVisibility = true;
                                    iscompany = true;
                                }
                                else iscompany = false;


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                                {
                                    MenuDatarefuserVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_1 = true;
                                    isUser = true;
                                }
                                else isUser = false;

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                                {
                                    MenuDatarefProduitVisibility = true;
                                    if ((iscompany && isUser) || (!iscompany && isUser))
                                    SeparatorDataref_2 = true;
                                }

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                                {
                                    MenuDatarefClientVisibility = true;
                                    if (iscompany)
                                    SeparatorDataref_3 = true;
                                }


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                                {
                                    MenuDatarefFactureVisibility = true;
                                    SeparatorDataref_4 = true;
                                }

                            }

                        }


                        if (Droit.Exists(dr => dr.IdVues == 16))
                        {
                            SeparatorDatarefAdmin = true;
                            AdminVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 7))
                        {
                            ParametersVisibility = true;
                            if (!MenuManagementVisibility)
                            MenuManagementVisibility = true;

                        }


                        if (Droit.Exists(dr => dr.IdVues == 8))
                        {
                            SortieFactureVisibility = true  ;
                            SeparatorSortieVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 1))
                        {
                            AdministrationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (ParametersVisibility && DatarefVisibility)
                            SeparatormanagementVisibility = true;
                        if (HistoricFactureVisibility && FactureVisibility)
                            SeparatorOperationVisibility = true;
                        if ((SortieFactureVisibility || HistoricFactureVisibility) && AdministrationVisibility)
                            SeparatorDataVisibility = true;

                        SeparatortoolB_3 = true;
                        RebusFactureVisibility = true;

                        break;
                    }

                case "dev":
                    {
                        //compte administrateur
                        if (Droit.Exists(dr => dr.IdVues == 4))
                        {
                            // FactureVisibility = true;
                            MenuOperationVisibility = true;
                            SeparatortoolB_1 = false;
                            isgetdroitfacture = true;

                        }


                        if (Droit.Exists(dr => dr.IdVues == 5))
                        {
                            HistoricFactureVisibility = true;
                            SeparatorOperationVisibility = true;
                            SeparatortoolB_2 = true;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 15))
                        {
                            JournalVenteVisibility = true;
                            SeparatorJv = true;
                            JournalventeToolbarVisibility = true;
                            SeparatortoolB_4 = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 6))
                        {
                            bool iscompany = false;
                            bool isUser = false;

                            List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;
                            DatarefVisibility = true;
                            MenuManagementVisibility = true;
                            if (sousVues != null)
                            {
                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                                {
                                    MenuDatarefSocieteVisibility = true;
                                    iscompany = true;
                                }
                                else iscompany = false;


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                                {
                                    MenuDatarefuserVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_1 = true;
                                    isUser = true;
                                }
                                else isUser = false;

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                                {
                                    MenuDatarefProduitVisibility = true;
                                    if ((iscompany && isUser) || (!iscompany && isUser))
                                        SeparatorDataref_2 = true;
                                }

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                                {
                                    MenuDatarefClientVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_3 = true;
                                }


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                                {
                                    MenuDatarefFactureVisibility = true;
                                    SeparatorDataref_4 = true;
                                }

                            }

                        }


                        if (Droit.Exists(dr => dr.IdVues == 16))
                        {
                            SeparatorDatarefAdmin = true;
                            AdminVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 7))
                        {
                            ParametersVisibility = true;
                            if (!MenuManagementVisibility)
                                MenuManagementVisibility = true;

                        }


                        if (Droit.Exists(dr => dr.IdVues == 8))
                        {
                            SortieFactureVisibility = true;
                            SeparatorSortieVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 1))
                        {
                            AdministrationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (ParametersVisibility && DatarefVisibility)
                            SeparatormanagementVisibility = true;
                        if (HistoricFactureVisibility && FactureVisibility)
                            SeparatorOperationVisibility = true;
                        if ((SortieFactureVisibility || HistoricFactureVisibility) && AdministrationVisibility)
                            SeparatorDataVisibility = true;

                        SeparatortoolB_3 = true;
                        RebusFactureVisibility = true;

                        break;
                    }

                case "sadmin":
                    {
                        // compte Proprietaires  owner

                        if (Droit.Exists(dr => dr.IdVues == 4))
                        {
                           // FactureVisibility = true;
                            MenuOperationVisibility = true;
                            SeparatortoolB_1 = false ;
                              isgetdroitfacture = true  ;
                        }


                        if (Droit.Exists(dr => dr.IdVues == 5))
                        {
                            HistoricFactureVisibility = true  ;
                            SeparatorOperationVisibility = true;
                            SeparatortoolB_2 = true;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 6))
                        {
                            bool iscompany = false;
                            bool isUser = false;

                            List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;
                            DatarefVisibility = true;
                            MenuManagementVisibility = true;
                            if (sousVues != null)
                            {
                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                                {
                                    MenuDatarefSocieteVisibility = true;
                                    iscompany = true;
                                }
                                else iscompany = false;


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                                {
                                    MenuDatarefuserVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_1 = true;
                                    isUser = true;
                                }
                                else isUser = false;

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                                {
                                    MenuDatarefProduitVisibility = true;
                                    if ((iscompany && !isUser) || (!iscompany && isUser))
                                        SeparatorDataref_2 = true;
                                }

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                                {
                                    MenuDatarefClientVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_3 = true;
                                }


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                                {
                                    MenuDatarefFactureVisibility = true;
                                    SeparatorDataref_4 = true;
                                }

                            }

                        }


                        if (Droit.Exists(dr => dr.IdVues == 15))
                        {
                            JournalVenteVisibility = true;
                            SeparatorJv = true;
                            JournalventeToolbarVisibility = true;
                            SeparatortoolB_4 = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 7))
                        {
                            ParametersVisibility = true;
                            if (!MenuManagementVisibility)
                            MenuManagementVisibility = true;

                        }


                        if (Droit.Exists(dr => dr.IdVues == 8))
                        {
                            SortieFactureVisibility = true  ;
                            SeparatorSortieVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 9))
                        {
                            MenuDataPreparationVisibility = true;
                            SeparatorDataVisibility = true;

                        }
                        if (Droit.Exists(dr => dr.IdVues == 16))
                        {
                            SeparatorDatarefAdmin = true;
                            AdminVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 1))
                        {
                            AdministrationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (ParametersVisibility && DatarefVisibility)
                            SeparatormanagementVisibility = true;
                        if (HistoricFactureVisibility && FactureVisibility)
                            SeparatorOperationVisibility = true;
                        SeparatortoolB_3 = true;
                        RebusFactureVisibility = true;
                        break;
                    }
                case "opm":
                    {
                        // compte master user  utilisateur principale

                        if (Droit.Exists(dr => dr.IdVues == 4))
                        {
                            //FactureVisibility = true;
                            MenuOperationVisibility = true;
                            SeparatortoolB_1 = false ;
                            isgetdroitfacture=true ;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 5))
                        {
                            HistoricFactureVisibility = true  ;
                            MenuOperationVisibility = true;

                            SeparatortoolB_2 = true;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 6))
                        {
                            bool iscompany = false;
                            bool isUser = false;

                            List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;
                            DatarefVisibility = true;
                            MenuManagementVisibility = true;
                            if (sousVues != null)
                            {
                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                                {
                                    MenuDatarefSocieteVisibility = true;
                                    iscompany = true;
                                }
                                else iscompany = false;


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                                {
                                    MenuDatarefuserVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_1 = true;
                                    isUser = true;
                                }
                                else isUser = false;

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                                {
                                    MenuDatarefProduitVisibility = true;
                                    if ((iscompany && !isUser) || (!iscompany && isUser))
                                        SeparatorDataref_2 = true;
                                }

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                                {
                                    MenuDatarefClientVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_3 = true;
                                }


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                                {
                                    MenuDatarefFactureVisibility = true;
                                    SeparatorDataref_4 = true;
                                }

                            }

                        }

                        //if (Droit.Exists(dr => dr.IdVues == 6))
                        //{
                        //    // List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;
                        //    DatarefVisibility = true;
                        //    MenuManagementVisibility = true;

                        //    //if (sousVues != null)
                        //    //{
                        //    //    if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                        //    //    {
                        //    //        MenuDatarefSocieteVisibility = true;
                        //    //        iscompany = true;
                        //    //    }
                        //    //    else iscompany = false;


                        //    //    if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                        //    //    {
                        //    //        MenuDatarefuserVisibility = true;
                        //    //        if (iscompany)
                        //    //            SeparatorDataref_1 = true;
                        //    //        isUser = true;
                        //    //    }
                        //    //    else isUser = false;

                        //    //    if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                        //    //    {
                        //    //        MenuDatarefProduitVisibility = true;
                        //    //        if ((iscompany && isUser) || (!iscompany && isUser))
                        //    //        SeparatorDataref_2 = true;
                        //    //    }

                        //    //    if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                        //    //    {
                        //    //        MenuDatarefClientVisibility = true;
                        //    //        if (iscompany)
                        //    //        SeparatorDataref_3 = true;
                        //    //    }


                        //    //    if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                        //    //    {
                        //    //        MenuDatarefFactureVisibility = true;
                        //    //        SeparatorDataref_4 = true;
                        //    //    }

                        //    //}//

                        //}


                        if (Droit.Exists(dr => dr.IdVues == 15))
                        {
                            JournalVenteVisibility = true;
                            SeparatorJv = true;
                            JournalventeToolbarVisibility = true;
                            SeparatortoolB_4 = true;

                        }
                        if (Droit.Exists(dr => dr.IdVues == 16))
                        {
                            SeparatorDatarefAdmin = true;
                            AdminVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 7))
                        {
                            ParametersVisibility = false;
                            if (!MenuManagementVisibility)
                            MenuManagementVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 8))
                        {
                            SortieFactureVisibility = true  ;
                            SeparatorSortieVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 9))
                        {
                            MenuDataPreparationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 1))
                        {
                            AdministrationVisibility = true;
                            SeparatorDataVisibility = true;

                        }


                        if (ParametersVisibility && DatarefVisibility)
                            SeparatormanagementVisibility = true;
                        if (HistoricFactureVisibility && FactureVisibility)
                            SeparatorOperationVisibility = true;
                        break;
                    }

                case "op":
                    {
                        if (Droit.Exists(dr => dr.IdVues == 4))
                        {
                            //FactureVisibility = true;
                            MenuOperationVisibility = true;
                            SeparatortoolB_1 = false ;
                            isgetdroitfacture=true ;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 5))
                        {
                            HistoricFactureVisibility = true ;
                            MenuOperationVisibility = true;

                            SeparatortoolB_2 = true;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 6))
                        {
                            DatarefVisibility = true;
                            bool iscompany = false;
                            bool isUser = false;
                            MenuManagementVisibility = true;

                            List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;
                          
                            if (sousVues != null)
                            {
                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                                {
                                    MenuDatarefSocieteVisibility = true;
                                    iscompany = true;
                                }
                                else iscompany = false;


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                                {
                                    MenuDatarefuserVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_1 = true;
                                    isUser = true;
                                }
                                else isUser = false;

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                                {
                                    MenuDatarefProduitVisibility = true;
                                    if ((iscompany && !isUser) || (!iscompany && isUser))
                                        SeparatorDataref_2 = true;
                                }

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                                {
                                    MenuDatarefClientVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_3 = true;
                                }


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                                {
                                    MenuDatarefFactureVisibility = true;
                                    SeparatorDataref_4 = true;
                                }

                            }

                        }

                        if (Droit.Exists(dr => dr.IdVues == 7))
                        {
                            ParametersVisibility = false ;
                            if (!MenuManagementVisibility)
                            MenuManagementVisibility = true ;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 15))
                        {
                            JournalVenteVisibility = true;
                            SeparatorJv = true;
                            JournalventeToolbarVisibility = true;
                            SeparatortoolB_4 = true;

                        }
                        if (Droit.Exists(dr => dr.IdVues == 16))
                        {
                            SeparatorDatarefAdmin = true;
                            AdminVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 8))
                        {
                            SortieFactureVisibility = true  ;
                            SeparatorSortieVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 9))
                        {
                            MenuDataPreparationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 1))
                        {
                            AdministrationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (ParametersVisibility && DatarefVisibility)
                            SeparatormanagementVisibility = true;
                        if (HistoricFactureVisibility && FactureVisibility)
                            SeparatorOperationVisibility = true;
                        break;
                    }

                case "ops":
                    {

                        MenuOperationVisibility = true;

                        if (Droit.Exists(dr => dr.IdVues == 4))
                        {
                           // FactureVisibility = true;
                            SeparatortoolB_1 = false ;
                            isgetdroitfacture=true ;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 5))
                        {
                            HistoricFactureVisibility = true  ;
                            MenuOperationVisibility = true;

                            SeparatortoolB_2 = true;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 6))
                        {
                            bool iscompany = false;
                            bool isUser = false;
                            DatarefVisibility = true;
                            MenuManagementVisibility = true;

                            List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;

                            if (sousVues != null)
                            {
                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                                {
                                    MenuDatarefSocieteVisibility = true;
                                    iscompany = true;
                                }
                                else iscompany = false;


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                                {
                                    MenuDatarefuserVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_1 = true;
                                    isUser = true;
                                }
                                else isUser = false;

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                                {
                                    MenuDatarefProduitVisibility = true;
                                    if ((iscompany && !isUser) || (!iscompany && isUser))
                                        SeparatorDataref_2 = true;
                                }

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                                {
                                    MenuDatarefClientVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_3 = true;
                                }


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                                {
                                    MenuDatarefFactureVisibility = true;
                                    SeparatorDataref_4 = true;
                                }

                            }

                        }



                        if (Droit.Exists(dr => dr.IdVues == 7))
                        {
                            ParametersVisibility = true;
                            if (!MenuManagementVisibility)
                            MenuManagementVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 8))
                        {
                            SortieFactureVisibility = true ;
                            SeparatorSortieVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 9))
                        {
                            MenuDataPreparationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 1))
                        {
                            AdministrationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 15))
                        {
                            JournalVenteVisibility = true;
                            SeparatorJv = true;
                            JournalventeToolbarVisibility = true;
                            SeparatortoolB_4 = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 16))
                        {
                            SeparatorDatarefAdmin = true;
                            AdminVisibility = true;

                        }

                        if (ParametersVisibility && DatarefVisibility)
                            SeparatormanagementVisibility = true;
                        if (HistoricFactureVisibility && FactureVisibility)
                            SeparatorOperationVisibility = true;
                        break;
                    }

                case "Valide":
                    {
                        MenuOperationVisibility = true;

                        if (Droit.Exists(dr => dr.IdVues == 4))
                        {
                            //FactureVisibility = true;
                            //SeparatortoolB_1 = true;
                            isgetdroitfacture=true ;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 5))
                        {
                            HistoricFactureVisibility = true  ;
                            MenuOperationVisibility = true;

                            SeparatortoolB_2 = true;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 6))
                        {
                            bool iscompany = false;
                            bool isUser = false;

                            List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;
                            DatarefVisibility = true;
                            MenuManagementVisibility = true;
                            if (sousVues != null)
                            {
                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                                {
                                    MenuDatarefSocieteVisibility = true;
                                    iscompany = true;
                                }
                                else iscompany = false;


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                                {
                                    MenuDatarefuserVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_1 = true;
                                    isUser = true;
                                }
                                else isUser = false;

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                                {
                                    MenuDatarefProduitVisibility = true;
                                    if ((iscompany && !isUser) || (!iscompany && isUser))
                                        SeparatorDataref_2 = true;
                                }

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                                {
                                    MenuDatarefClientVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_3 = true;
                                }


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                                {
                                    MenuDatarefFactureVisibility = true;
                                    SeparatorDataref_4 = true;
                                }

                            }

                        }



                        if (Droit.Exists(dr => dr.IdVues == 7))
                        {
                            ParametersVisibility = true;
                            if (!MenuManagementVisibility)
                            MenuManagementVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 8))
                        {
                            SortieFactureVisibility = true;
                            SeparatorSortieVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 9))
                        {
                            MenuDataPreparationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 1))
                        {
                            AdministrationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 15))
                        {
                            JournalVenteVisibility = true;
                            SeparatorJv = true;
                            JournalventeToolbarVisibility = true;
                            SeparatortoolB_4 = true;

                        }
                        if (Droit.Exists(dr => dr.IdVues == 16))
                        {
                            SeparatorDatarefAdmin = true;
                            AdminVisibility = true;

                        }

                        if (ParametersVisibility && DatarefVisibility)
                            SeparatormanagementVisibility = true;
                        if (HistoricFactureVisibility && FactureVisibility)
                            SeparatorOperationVisibility = true;
                        break;
                    }

                case "compta":
                    {
                        //compte administrateur
                        if (Droit.Exists(dr => dr.IdVues == 4))
                        {
                            // FactureVisibility = true;
                            MenuOperationVisibility = true;
                            SeparatortoolB_1 = false;
                            isgetdroitfacture = true;

                        }


                        if (Droit.Exists(dr => dr.IdVues == 5))
                        {
                            HistoricFactureVisibility = true;
                            SeparatorOperationVisibility = true;
                            SeparatortoolB_2 = true;
                        }

                        if (Droit.Exists(dr => dr.IdVues == 15))
                        {
                            MenuOperationVisibility = true;
                            SeparatortoolB_1 = false;

                            JournalVenteVisibility = true;
                            SeparatorJv = true;
                            JournalventeToolbarVisibility = true;
                            SeparatortoolB_4 = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 6))
                        {
                            bool iscompany = false;
                            bool isUser = false;

                            List<DroitModel> sousVues = Droit.Find(dr => dr.IdVues == 6).SousDroits;
                            DatarefVisibility = true;
                            MenuManagementVisibility = true;
                            if (sousVues != null)
                            {
                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                                {
                                    MenuDatarefSocieteVisibility = true;
                                    iscompany = true;
                                }
                                else iscompany = false;


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("utilisateurs")))
                                {
                                    MenuDatarefuserVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_1 = true;
                                    isUser = true;
                                }
                                else isUser = false;

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("produits")))
                                {
                                    MenuDatarefProduitVisibility = true;
                                    if ((iscompany && isUser) || (!iscompany && isUser))
                                        SeparatorDataref_2 = true;
                                }

                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("client")))
                                {
                                    MenuDatarefClientVisibility = true;
                                    if (iscompany)
                                        SeparatorDataref_3 = true;
                                }


                                if (sousVues.Exists(idv => idv.LibelleSouVue.ToLower().Contains("factures")))
                                {
                                    MenuDatarefFactureVisibility = true;
                                    SeparatorDataref_4 = true;
                                }

                            }

                        }


                        if (Droit.Exists(dr => dr.IdVues == 16))
                        {
                            SeparatorDatarefAdmin = true;
                            AdminVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 7))
                        {
                            ParametersVisibility = true;
                            if (!MenuManagementVisibility)
                                MenuManagementVisibility = true;

                        }


                        if (Droit.Exists(dr => dr.IdVues == 8))
                        {
                            SortieFactureVisibility = true;
                            SeparatorSortieVisibility = true;

                        }

                        if (Droit.Exists(dr => dr.IdVues == 1))
                        {
                            AdministrationVisibility = true;
                            SeparatorDataVisibility = true;

                        }

                        if (ParametersVisibility && DatarefVisibility)
                            SeparatormanagementVisibility = true;
                        if (HistoricFactureVisibility && FactureVisibility)
                            SeparatorOperationVisibility = true;
                        if ((SortieFactureVisibility || HistoricFactureVisibility) && AdministrationVisibility)
                            SeparatorDataVisibility = true;

                        SeparatortoolB_3 = true;
                        RebusFactureVisibility = true;

                        break;
                    }

            }



        }
        #endregion

        #region Event Handler Timer
      
        #endregion

        #region Even temporary Account
        
      

        static void isTemporaryAccount(Int32 id, Int32 idProfile)
        {
            try
            {
                ProfileDateModel profileDateservice = new ProfileDateModel();
                var ProfileDateSelected = profileDateservice.GetProfileDate(id, idProfile);
                if (ProfileDateSelected != null)
                {
                    TimeSpan differenceDate =(DateTime) ProfileDateSelected.Datefin - DateTime.Parse(DateTime.Now.ToShortDateString());
                                if (differenceDate.TotalDays == 0)
                                {
                                    
                                    //LabelCompte = "Dernier jour de Validitier";
                                }
                                else if (differenceDate.TotalDays < 0)
                                {
                                   // LabelCompte = "Ce Compte est Expiré et sera Vérouillé et sera verouillé";
                                    UtilisateurModel use = new UtilisateurModel();
                                    use.Utilsateur_LockAcount(id );
                                }
                                //else 
                                    // LabelCompte = string.Format("Jours De Validité Restant  : {0}", differenceDate.TotalDays);
                
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE CHARGEMENT COMPTE TEMPORAIRE ";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }
        #endregion

    }
}
