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
using AllTech.FrameWork.Model;
using System.Collections.ObjectModel;
using System.Data;
using AllTech.FacturationModule.Views.Modal;
using System.Threading;
using AllTech.FrameWork.Global;
using AllTech.FacturationModule.Views;
using AllTech.FrameWork.Utils;

namespace AllTech.FacturationModule.ViewModel
{
    public class DatarefViewModel : ViewModelBase
    {

        #region FIELDS
       
       
       
        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand closeCommand;
        private RelayCommand refreshCommand;
        private RelayCommand listTaxeCommand;
        private RelayCommand listLanguageCommand;
        private RelayCommand listCompteCommand;
        private RelayCommand listexploitationCommand;
        private RelayCommand listexonerationCommand;
        private RelayCommand listeDeviseCommand;
        private RelayCommand listeCompteGeneCommand;
        private RelayCommand listeCompteAnalClientCommand;
        private RelayCommand listeCompteAnalCommand;
        private RelayCommand searchByCommand;
        private RelayCommand listeObjetClientCommand;
        private RelayCommand editContextCommand;
        private RelayCommand parameterContextCommand;
        private RelayCommand deleteContextCommand;
        private RelayCommand updateSuiviProduitCommand;
        private RelayCommand cancelSuiviProduitCommand;

       
        

        ProduitModel _produitService;
        ProduitModel _produitSelected;
        ObservableCollection<ProduitModel> _produitList;
        DataTable newTable = null;
        private RelayCommand annulerProrataCommand;
     
        LangueModel _language;
        ObservableCollection<LangueModel> _languageList;
        LangueModel _languageSelected;

        ClientModel _clientservice;
        ClientModel _clientActiveSelected;

      
        ClientModel _clientSelected;
        ClientModel _clientActif;

     
        ObservableCollection<ClientModel> _clientList;
        ObservableCollection<ClientModel> _cacheClientList;

        LibelleTermeModel libelleService;
        LibelleTermeModel libelleSelected;
        
       List<LibelleTermeModel> _libelleList;

    
   

        TaxeModel taxeService;
        TaxeModel _taxePorataSelected;
        List<TaxeModel> _taxePorataList;

        CompteModel compteservice;
        CompteModel compteSelected;
        List<CompteModel> compteList;

        ExonerationModel exonservice;
        ExonerationModel exonerateSelected;
        List<ExonerationModel> exonerateList;

      

        SocieteModel societeCourante;
        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;

        DeviseModel deviseSelected;
        DeviseModel deviseService;
        List<DeviseModel> deviseList;

        CompteGenralModel compteGeneService;
        List<CompteGenralModel> compteGeneListe;
        CompteGenralModel compteGeneSelected;


        bool _icheckExonarate;
        bool _icheckOfshore;
        bool isPorataEnabled;

        bool btnNewVisible;
        bool btnSaveVisible;
        bool btnDeleteVisible;
        bool btnInitVisible;
        bool isenableFields;

        int exonerateIndex;
        int deviseFactIndex;
        int compteIndex;
        int prorataIndex;
        int compteGeneIndex;
        int termelibelleIndex;

        int nbreClients;

        Window localwindow;
        DataRef_Customers fliste = null;
        string nomClient;
        string nomVille;

        bool isVisibleCancelNomClient;


        bool isVisibleCancelVille;
        bool isRadClientActive;
        bool isRadClientInactive;
        bool isVisibleRadInactif;
        bool isloading;

        bool isRadClientArchive;
        bool isRadClientVisibleArchive;

       

        List<produisuivi> listeProduitsuivis;
        produisuivi produiSuiviSelect;
        List<produisuivi> listeProduitsuiviUpdate = new List<produisuivi>();
        ProduitModel produitService;

        public bool isloadingParam = true;
        #endregion

        #region CONSTRUCTOR



        public DatarefViewModel(Window controls,DataRef_Customers forme )
        {
            localwindow = controls;
            fliste = forme;
           // localwindow = window;
            ProgressBarVisibility = false;
            IsPorataEnabled = false;
            exonerateIndex=-1;
            ProrataIndex = -1;
            compteIndex = -1;
            deviseFactIndex = -1;
            termelibelleIndex = -1;
            CompteGeneIndex = -1;
            produitService = new ProduitModel();
           // _injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
           // _produitService = new ProduitModel();
            EvenRefreshGridDataRef.EventRefreshList+=new EvenRefreshGridDataRef.MyEventHandler(EvenRefreshGridDataRef_EventRefreshList); 

           _language = new LangueModel();
            _clientservice = new ClientModel();
            taxeService = new TaxeModel();
            compteservice = new CompteModel();
            exonservice = new ExonerationModel();
            deviseService = new DeviseModel();
            compteGeneService = new CompteGenralModel();
            libelleService = new LibelleTermeModel();
            societeCourante = GlobalDatas.DefaultCompany;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;

            if (CacheDatas.ui_currentdroitClientInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
                CacheDatas.ui_currentdroitClientInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitClientInterface;

            newTable = CommonModule.SetDataTableClient();
            GlobalDatas.IdDataRefArchiveDatas = false;
            if (CurrentDroit.Lecture || CurrentDroit.Developpeur)
            {
                loadDatas();

                loadRight();

                BtnNewVisible = true;

                _clientSelected = new ClientModel();
                ClientSelected = _clientSelected;

                IsRadClientActive = true;
            }
        }

        #endregion

        #region PROPERTIES

        #region COMMON

        public bool IsRadClientArchive
        {
            get { return isRadClientArchive; }
            set { isRadClientArchive = value;
            if (value)
                loadDatasArchivesValidate();
            this.OnPropertyChanged("IsRadClientArchive");
            }
        }


        public bool IsRadClientVisibleArchive
        {
            get { return isRadClientVisibleArchive; }
            set { isRadClientVisibleArchive = value;
            this.OnPropertyChanged("IsRadClientVisibleArchive");
            }
        }


        public bool IsVisibleCancelNomClient
        {
            get { return isVisibleCancelNomClient; }
            set { isVisibleCancelNomClient = value;
            this.OnPropertyChanged("IsVisibleCancelNomClient");
            }
        }

        public bool IsVisibleCancelVille
        {
            get { return isVisibleCancelVille; }
            set { isVisibleCancelVille = value;
            this.OnPropertyChanged("IsVisibleCancelVille");
            }
        }
        public string NomClient
        {
            get { return nomClient; }
            set { nomClient = value;
            if (string.IsNullOrEmpty(value))
                IsVisibleCancelNomClient = false;
            else IsVisibleCancelNomClient = true;

            this.OnPropertyChanged("NomClient");
            }
        }


        public string NomVille
        {
            get { return nomVille; }
            set { nomVille = value;
            if (string.IsNullOrEmpty(value))
                IsVisibleCancelVille = false;
            else IsVisibleCancelVille = true;

            this.OnPropertyChanged("NomVille");
            }
        }

        public int NbreClients
        {
            get { return nbreClients; }
            set { nbreClients = value;
            this.OnPropertyChanged("NbreClients");
            }
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

        public bool BtnNewVisible
        {
            get { return btnNewVisible; }
            set
            {
                btnNewVisible = value;
                OnPropertyChanged("BtnNewVisible");
            }
        }
        public bool BtnSaveVisible
        {
            get { return btnSaveVisible; }
            set
            {
                btnSaveVisible = value;
                OnPropertyChanged("BtnSaveVisible");
            }
        }
        public bool BtnDeleteVisible
        {
            get { return btnDeleteVisible; }
            set
            {
                btnDeleteVisible = value;
                OnPropertyChanged("BtnDeleteVisible");
            }
        }
        public bool BtnInitVisible
        {
            get { return btnInitVisible; }
            set
            {
                btnInitVisible = value;
                OnPropertyChanged("BtnInitVisible");
            }
        }

        public DroitModel CurrentDroit
        {
            get { return _currentDroit; }
            set
            {
                _currentDroit = value;
                OnPropertyChanged("CurrentDroit");
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

        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set
            {
                userConnected = value;
                OnPropertyChanged("UserConnected");
            }
        }

        #endregion

        #region INDEX COMBOBOX

        //int deviseFactIndex;
        //int compteIndex;
        //int prorataIndex;

        public int TermelibelleIndex
        {
            get { return termelibelleIndex; }
            set { termelibelleIndex = value;
            this.OnPropertyChanged("TermelibelleIndex");
            }
        }


        public int ExonerateIndex
        {
            get { return exonerateIndex; }
            set
            {
                exonerateIndex = value;
                this.OnPropertyChanged("ExonerateIndex");
            }
        }

        public int ProrataIndex
        {
            get { return prorataIndex; }
            set
            {
                prorataIndex = value;
                this.OnPropertyChanged("ProrataIndex");
            }
        }

        public int CompteIndex
        {
            get { return compteIndex; }
            set
            {
                compteIndex = value;
                this.OnPropertyChanged("CompteIndex");
            }
        }
        public int DeviseFactIndex
        {
            get { return deviseFactIndex; }
            set
            {
                deviseFactIndex = value;
                this.OnPropertyChanged("DeviseFactIndex");
            }
        }

        #endregion

        #region Comptabilite

        public int CompteGeneIndex
        {
            get { return compteGeneIndex; }
            set { compteGeneIndex = value;
            this.OnPropertyChanged("CompteGeneIndex");
            }
        }

        public List<CompteGenralModel> CompteGeneListe
        {
            get { return compteGeneListe; }
            set { compteGeneListe = value;
            this.OnPropertyChanged("CompteGeneListe");
            }
        }

        public CompteGenralModel CompteGeneSelected
        {
            get { return compteGeneSelected; }
            set { compteGeneSelected = value;
            this.OnPropertyChanged("CompteGeneSelected");
            }
        }

        #endregion

        public produisuivi ProduiSuiviSelect
        {
            get { return produiSuiviSelect; }
            set
            {
                produiSuiviSelect = value;
                this.OnPropertyChanged("ProduiSuiviSelect");
            }
        }

        public List<produisuivi> ListeProduitsuivis
        {
            get { return listeProduitsuivis; }
            set
            {
                listeProduitsuivis = value;
                this.OnPropertyChanged("ListeProduitsuivis");
            }
        }


        public List<produisuivi> ListeProduitsuiviUpdate
        {
            get { return listeProduitsuiviUpdate; }
            set
            {
                listeProduitsuiviUpdate = value;
                this.OnPropertyChanged("ListeProduitsuiviUpdate");
            }
        }

        public ClientModel ClientActif
        {
            get { return _clientActif; }
            set { _clientActif = value;
            this.OnPropertyChanged("ClientActif");
            }
        }

        public ClientModel ClientActiveSelected
        {
            get { return _clientActiveSelected; }
            set { _clientActiveSelected = value;
            this.OnPropertyChanged("ClientActiveSelected");
            }
        }

        public bool IsVisibleRadInactif
        {
            get { return isVisibleRadInactif; }
            set { isVisibleRadInactif = value;
            this.OnPropertyChanged("IsVisibleRadInactif");
            }
        }

        public bool IsRadClientActive
        {
            get { return isRadClientActive; }
            set { isRadClientActive = value;
            if (isloading)
            {
                if (value)
                {
                    loadDatasActif();
                }
            }
            isloading = true;
            this.OnPropertyChanged("IsRadClientActive");
            }
        }


        public bool IsRadClientInactive
        {
            get { return isRadClientInactive; }
            set { isRadClientInactive = value;
            if (value)
            {
                loadDatasInactif();
            }
            this.OnPropertyChanged("IsRadClientInactive");
            }
        }

        public bool IsPorataEnabled
        {
            get { return isPorataEnabled; }
            set { isPorataEnabled = value; 
            this.OnPropertyChanged("IsPorataEnabled");
            
            }
        }

        public bool IsenableFields
        {
            get { return isenableFields; }
            set { isenableFields = value;
            this.OnPropertyChanged("IsenableFields");
            }
        }

        public LibelleTermeModel LibelleSelected
        {
            get { return libelleSelected; }
            set { libelleSelected = value ;
            this.OnPropertyChanged("LibelleSelected");
            }
        }
 
        public List<LibelleTermeModel> LibelleList
        {
            get { return _libelleList; }
            set { _libelleList = value;
            this.OnPropertyChanged("LibelleList");
            }
        }

        public bool IcheckExonarate
        {
            get { return _icheckExonarate; }
            set { _icheckExonarate = value;
            
                if (_clientSelected != null)
                   // _clientSelected.Exonere = value;
            
            this.OnPropertyChanged("IcheckExonarate");
            }
        }

       
     

      

        public string Filtertexte
        {
            get { return filtertexte; }
            set
            {
                filtertexte = value;
               if (value != null || value != string.Empty)
                    filter(value);

                this.OnPropertyChanged("Filtertexte");
            }
        }

        public ObservableCollection<ClientModel> CacheClientList
        {
            get { return _cacheClientList; }
            set
            {
                _cacheClientList = value;
                this.OnPropertyChanged("CacheClientList");
            }
        }


        #region CLIENT REGION
        public ClientModel ClientSelected
        {
            get { return _clientSelected; }
            set
            {
                _clientSelected = value ;
                if (value != null)
                {
                    IsenableFields = true;
                    LibelleList = libelleService.GetLibelle_List(value .IdLangue);
                    if (LibelleList.Count >0)
                     TermelibelleIndex= CommonModule.GetindexeComboBoxLibelleTerme(LibelleList, value.IdTerme);

                }
                this.OnPropertyChanged("ClientSelected");
            }
        }

        public ObservableCollection<ClientModel> ClientList
        {
            get { return _clientList; }
            set
            {
                _clientList = value;
                this.OnPropertyChanged("ClientList");
            }
        }
        #endregion

        #region PRODUIT REGION

        public ObservableCollection<ProduitModel> ProduitList
        {
            get { return _produitList; }
            set
            {
                _produitList = value;
                this.OnPropertyChanged("ProduitList");
            }
        }

        public ProduitModel ProduitSelected
        {
            get { return _produitSelected; }
            set
            {
                _produitSelected = value;


                this.OnPropertyChanged("ProduitSelected");

            }
        }
        #endregion

        #region LANGUE REGION
        public LangueModel LanguageSelected
        {
            get { return _languageSelected; }
            set
            {
                _languageSelected = value;

                if (ClientSelected != null && value != null)
                    ClientSelected.IdLangue = value.Id;
                if (value !=null )
                LibelleList = libelleService.GetLibelle_List(value .Id);
                this.OnPropertyChanged("LanguageSelected");
            }
        }

        public ObservableCollection<LangueModel> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                this.OnPropertyChanged("LanguageList");
            }
        }
        #endregion

        #region TAXE REGION
        public TaxeModel TaxePorataSelected
        {
            get { return _taxePorataSelected; }
            set
            {
                _taxePorataSelected = value;
                if (ClientSelected != null)
                {
                    if (value != null)
                    {
                        ClientSelected.Idporata = value.ID_Taxe;
                    }
                }
                

                this.OnPropertyChanged("TaxePorataSelected");
            }
        }


        public List<TaxeModel> TaxePorataList
        {
            get { return _taxePorataList; }
            set
            {
                _taxePorataList = value;
                this.OnPropertyChanged("TaxePorataList");
            }
        }
        #endregion

        #region COMPTE REGION
        public CompteModel CompteSelected
        {
            get { return compteSelected; }
            set
            {
                compteSelected = value;
                if (ClientSelected != null)
                    if (value != null)
                        ClientSelected.IdCompte = value.ID;
                this.OnPropertyChanged("CompteSelected");
            }
        }


        public List<CompteModel> CompteList
        {
            get { return compteList; }
            set
            {
                compteList = value;
                this.OnPropertyChanged("CompteList");
            }
        }
        #endregion

        #region EXONERATION REGION

        public ExonerationModel ExonerateSelected
        {
            get { return exonerateSelected; }
            set
            {
                exonerateSelected = value;
                if (ClientSelected != null)
                    if (value != null)
                    {
                        ClientSelected.IdExonere = value.ID;
                        if (value.CourtDesc.Contains ("part") || value.CourtDesc.Contains ("exo"))//si client exonere ou partiellement
                            IsPorataEnabled = true;
                        else
                        {
                            TaxePorataSelected = null;
                            IsPorataEnabled = false; //client non exonere
                        }
                    }
                this.OnPropertyChanged("ExonerateSelected");
            }
        }

        public List<ExonerationModel> ExonerateList
        {
            get { return exonerateList; }
            set { exonerateList = value;
            this.OnPropertyChanged("ExonerateList");
            }
        }
        #endregion

        #region DEVISE REGION

        public DeviseModel DeviseSelected
        {
            get { return deviseSelected; }
            set { deviseSelected = value;
            if (value != null)
                if (ClientSelected != null)
                    ClientSelected.IdDeviseFact = value.ID_Devise;
            this.OnPropertyChanged("DeviseSelected");
            }
        }

        public List<DeviseModel> DeviseList
        {
            get { return deviseList; }
            set { deviseList = value;
            this.OnPropertyChanged("DeviseList");
            }
        }

        #endregion

        #endregion
       
        #region ICOMMAND

        public ICommand CancelSuiviProduitCommand
        {
            get
            {
                if (this.cancelSuiviProduitCommand == null)
                {
                    this.cancelSuiviProduitCommand = new RelayCommand(param => this.canCancelProduit(), param => this.canExecuteCancelProduit());
                }
                return this.cancelSuiviProduitCommand;
            }
        }

        public ICommand UpdateSuiviProduitCommand
        {
            get
            {
                if (this.updateSuiviProduitCommand == null)
                {
                    this.updateSuiviProduitCommand = new RelayCommand(param => this.canUpdateProduit(), param => this.canExecuteUpdateProduit());
                }
                return this.updateSuiviProduitCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(param => this.canSaveProduit(), param => this.canExecuteSaveProduit());
                }
                return this.saveCommand;
            }
        }

        public ICommand NewCommand
        {
            get
            {
                if (this.newCommand == null)
                {
                    this.newCommand = new RelayCommand(param => this.canNewProduit());
                }
                return this.newCommand;
            }


        }

        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(param => this.canDeleteProduit(), param => this.canExecuteDeleteProduit());
                }
                return this.deleteCommand;
            }


        }

        public ICommand RefreshCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.refreshCommand = new RelayCommand(param => this.canRefreshListeClient(), param => this.canExecuteRefresh());
                }
                return this.refreshCommand;
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


        public RelayCommand ListTaxeCommand
        {
            get
            {
                if (this.listTaxeCommand == null)
                {
                    this.listTaxeCommand = new RelayCommand(param => this.canShowTaxes());
                }
                return this.listTaxeCommand;
            }

        }

        public RelayCommand ListLanguageCommand
        {
            get
            {
                if (this.listLanguageCommand == null)
                {
                    this.listLanguageCommand = new RelayCommand(param => this.canShowLaguage());
                }
                return this.listLanguageCommand;
            }

        }
     

        public RelayCommand ListCompteCommand
        {
            get
            {
                if (this.listCompteCommand == null)
                {
                    this.listCompteCommand = new RelayCommand(param => this.canShowCompte());
                }
                return this.listCompteCommand;
            }

        }

        public RelayCommand ListexploitationCommand
        {
            get
            {
                if (this.listexploitationCommand == null)
                {
                    this.listexploitationCommand = new RelayCommand(param => this.canShowExploitation(), param => this.canExecuteShowExpl());
                }
                return this.listexploitationCommand;
            }

        }

     
        public RelayCommand ListexonerationCommand
        {
            get
            {
                if (this.listexonerationCommand == null)
                {
                    this.listexonerationCommand = new RelayCommand(param => this.canShowExonerartion(), param => this.canExecuteExoneration());
                }
                return this.listexonerationCommand;
            }

        }

        public RelayCommand ListeDeviseCommand
        {
            get
            {
                if (this.listeDeviseCommand == null)
                {
                    this.listeDeviseCommand = new RelayCommand(param => this.canShowDevise(), param => this.canExecuteShowDevise());
                }
                return this.listeDeviseCommand;
            }

        }

        public RelayCommand ListeObjetClientCommand
        {
            get
            {
                if (this.listeObjetClientCommand == null)
                {
                    this.listeObjetClientCommand = new RelayCommand(param => this.canShowObjet(), param => this.canExecuteShowObjets());
                }
                return this.listeObjetClientCommand;
            }
        }


        public RelayCommand AnnulerProrataCommand
        {
            get
            {
                if (this.annulerProrataCommand == null)
                {
                    this.annulerProrataCommand = new RelayCommand(param => this.canAnnulerPro(), param => this.canExecuteAnnulerPro());
                }
                return this.annulerProrataCommand;
            }

        }

       
        public RelayCommand ListeCompteGeneCommand
        {
            get
            {
                if (this.listeCompteGeneCommand == null)
                {
                    this.listeCompteGeneCommand = new RelayCommand(param => this.canShowCmpteGene(), param => this.canExecuteCompteGene());
                }
                return this.listeCompteGeneCommand;
            }

        }

        public RelayCommand ListeCompteAnalClientCommand
        {
            get
            {
                if (this.listeCompteAnalClientCommand == null)
                {
                    this.listeCompteAnalClientCommand = new RelayCommand(param => this.canShowCmpteAnalCli(), param => this.canExecuteCompteAnalCli());
                }
                return this.listeCompteAnalClientCommand;
            }

        }

        public RelayCommand ListeCompteAnalCommand
        {
            get
            {
                if (this.listeCompteAnalCommand == null)
                {
                    this.listeCompteAnalCommand = new RelayCommand(param => this.canShowCmpteAnal(), param => this.canExecuteCompteAnal());
                }
                return this.listeCompteAnalCommand;
            }

        }

        //

        public RelayCommand SearchByCommand
        {
            get
            {
                if (this.searchByCommand == null)
                {
                    this.searchByCommand = new RelayCommand(param => this.canSearchBy(), param => this.canExecuteSearchBy());
                }
                return this.searchByCommand;
            }

        }


        #region Contexte
       

        public RelayCommand EditContextCommand
        {
            get
            {
                if (this.editContextCommand == null)
                {
                    this.editContextCommand = new RelayCommand(param => this.canEditContext(), param => this.canExecuteEditcontext());
                }
                return this.editContextCommand;
            }

        }

        public RelayCommand ParameterContextCommand
        {
            get
            {
                if (this.parameterContextCommand == null)
                {
                    this.parameterContextCommand = new RelayCommand(param => this.canParamContext(), param => this.canExecuteParametrecontext());
                }
                return this.parameterContextCommand;
            }

        }


        public RelayCommand DeleteContextCommand
        {
            get
            {
                if (this.deleteContextCommand == null)
                {
                    this.deleteContextCommand = new RelayCommand(param => this.canDeleteContext(), param => this.canExecuteDeleteContext());
                }
                return this.deleteContextCommand;
            }

        }
        #endregion
        #endregion

        #region METHODS


        #region LOAD REGION
        
       
        void loadRight()
        {
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur || CurrentDroit.Proprietaire )
            {
                BtnDeleteVisible = true;
                isVisibleRadInactif = true;
            }
            //if (CurrentDroit != null)
            //{
            //    if (CurrentDroit.Super)
            //    {

            //        BtnDeleteVisible = true;
            //        BtnInitVisible = true;
            //        BtnNewVisible = true;
            //        BtnSaveVisible = true;
            //    }
            //    else
            //    {
            //        if (CurrentDroit.Suppression)
            //        {
            //            BtnDeleteVisible = true;

            //        }
            //        if (CurrentDroit.Ecriture)
            //        {
            //            BtnSaveVisible = true;
            //            BtnNewVisible = true;
            //        }
            //    }
            //}
        }

        protected void EvenRefreshGridDataRef_EventRefreshList(object sender, EventArgs e)
        {
            EvenRefreshGridDataRef data = sender as EvenRefreshGridDataRef;
            if (data.typeOperation == "event")
            {
                loadDatasRefresh();
            }
        }

       public  void loadDatas()
        {
            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
           // ProgressBarVisibility = true;
            fliste.StartStopWait(true);
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (societeCourante != null)
                    {
                       // if (CacheDatas.ui_ClientClients == null)
                        //{
                            ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete,true);
                           
                            //CacheDatas.ui_ClientClients = ClientList;
                       // }
                        //else ClientList = CacheDatas.ui_ClientClients;
                       
                       // CacheClientList = ClientList;
                        LanguageList = _language.LANGUE_SELECT(0);
                       // NbreClients = ClientList.Count;
                       // GlobalDatas.IdDataRefArchiveDatas =false;
                        GlobalDatas.IdDataRefArchiveDatas = false;
                    }
                }
                catch (Exception  ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException;
                }

                //try
                //{
                //    if (CacheDatas.ui_ClientCompte == null)
                //    {
                //        CompteList = compteservice.COMPTE_SELECT();
                //        CacheDatas.ui_ClientCompte = CompteList;
                //    }
                //    else CompteList = CacheDatas.ui_ClientCompte;
                //}
                //catch (Exception ex)
                //{
                //    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT COMPTES CLIENTS";

                //}

                //try
                //{
                //    if (CacheDatas.ui_ClientExonerations == null)
                //    {
                //        ExonerateList = exonservice.EXONERATION_SELECT();
                //        CacheDatas.ui_ClientExonerations = ExonerateList;
                //    }
                //    else ExonerateList = CacheDatas.ui_ClientExonerations;
                  
                //}
                //catch (Exception ex)
                //{
                //    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LISt EXONERATION CLIENTS";

                //}

               // try
                //{
                //    if (CacheDatas.ui_ClientDevises == null)
                //    {
                //        DeviseList = deviseService.Devise_SELECT(societeCourante.IdSociete);
                //        CacheDatas.ui_ClientDevises = DeviseList;
                //    }
                //    else DeviseList = CacheDatas.ui_ClientDevises;
                  
                //}
                //catch (Exception ex)
                //{
                //    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LISTES EXONERATION CLIENTS";

                //}

                //try
                //{

                //    DataTable newliste = taxeService.Taxe_SELECTByDataTable(societeCourante.IdSociete);

                //    if (newliste != null)
                //        getDatas(newliste, "p", "porata");
                //}
                //catch (Exception ex)
                //{
                //    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LIST TAXES";

                //}


                //try
                //{
                //    //if (CacheDatas.ui_ClientCompte == null)
                //    //{
                //    CompteGeneListe = compteGeneService.ModelCompteGeneral_SelectAll(societeCourante.IdSociete);
                //       // CacheDatas.ui_ClientCompte = CompteList;
                //   // }
                //   // else CompteList = CacheDatas.ui_ClientCompte;
                //}
                //catch (Exception ex)
                //{
                //    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT COMPTES GENERAUX";

                //}
               


            };
            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner =localwindow;
                    view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                    view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0,args.Result.ToString().LastIndexOf(";"));
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    fliste.StartStopWait(false);
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                    fliste.StartStopWait(false);
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();
        }

       public void loadDatasRefresh()
       {
           this.MouseCursor = Cursors.Wait;
           BackgroundWorker worker = new BackgroundWorker();
           this.IsBusy = true;
           // ProgressBarVisibility = true;
           fliste.StartStopWait(true);
           worker.DoWork += (o, args) =>
           {
               try
               {

                   if (!GlobalDatas.IdDataRefArchiveDatas)
                  
                       ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                   else
                       ClientList = _clientservice.CLIENT_Archive_GETLISTE(societeCourante.IdSociete, true);

                    
                   
               }
               catch (Exception ex)
               {
                   args.Result = ex.Message + " ;" + ex.InnerException;
               }

               


           };
           worker.RunWorkerCompleted += (o, args) =>
           {
               if (args.Result != null)
               {
                   CustomExceptionView view = new CustomExceptionView();
                   view.Owner = localwindow;
                   view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                   view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                   view.ShowDialog();
                   this.MouseCursor = null;
                   this.IsBusy = false;
                   fliste.StartStopWait(false);
               }
               else
               {
                   this.MouseCursor = null;
                   this.IsBusy = false;
                   ProgressBarVisibility = false;
                   fliste.StartStopWait(false);
               }
               //this.OnPropertyChanged("ListEmployees");
           };

           worker.RunWorkerAsync();
       }

       public void loadDatasInactif()
       {
           this.MouseCursor = Cursors.Wait;
           BackgroundWorker worker = new BackgroundWorker();
           this.IsBusy = true;
           // ProgressBarVisibility = true;
           fliste.StartStopWait(true);
           worker.DoWork += (o, args) =>
           {
               try
               {
                   
                   ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete, false);
                      
                       GlobalDatas.IdDataRefArchiveDatas = false;
               }
               catch (Exception ex)
               {
                   args.Result = ex.Message + " ;" + ex.InnerException;
               }

              
           };
           worker.RunWorkerCompleted += (o, args) =>
           {
               if (args.Result != null)
               {
                   CustomExceptionView view = new CustomExceptionView();
                   view.Owner = localwindow;
                   view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                   view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                   view.ShowDialog();
                   this.MouseCursor = null;
                   this.IsBusy = false;
                   fliste.StartStopWait(false);
               }
               else
               {
                   this.MouseCursor = null;
                   this.IsBusy = false;
                   ProgressBarVisibility = false;
                   fliste.StartStopWait(false);
               }
               //this.OnPropertyChanged("ListEmployees");
           };

           worker.RunWorkerAsync();
       }

       public void loadDatasActif()
       {
           this.MouseCursor = Cursors.Wait;
           BackgroundWorker worker = new BackgroundWorker();
           this.IsBusy = true;
           // ProgressBarVisibility = true;
           fliste.StartStopWait(true);
           worker.DoWork += (o, args) =>
           {
               try
               {
                   //if (societeCourante != null)
                  // {
                
                      // {
                           ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                           CacheDatas.ui_ClientClients = null;
                         //  CacheDatas.ui_ClientClients = ClientList;
                      // }
                      // else ClientList = CacheDatas.ui_ClientClients;

                       CacheClientList = ClientList;
                       // LanguageList = _language.LANGUE_SELECT(0);
                       // NbreClients = ClientList.Count;
                  // }
               }
               catch (Exception ex)
               {
                   args.Result = ex.Message + " ;" + ex.InnerException;
               }

              

           };
           worker.RunWorkerCompleted += (o, args) =>
           {
               if (args.Result != null)
               {
                   CustomExceptionView view = new CustomExceptionView();
                   view.Owner = localwindow;
                   view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                   view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                   view.ShowDialog();
                   this.MouseCursor = null;
                   this.IsBusy = false;
                   fliste.StartStopWait(false);
               }
               else
               {
                   this.MouseCursor = null;
                   this.IsBusy = false;
                   ProgressBarVisibility = false;
                   fliste.StartStopWait(false);
               }
               //this.OnPropertyChanged("ListEmployees");
           };

           worker.RunWorkerAsync();
       }

       public void loadDatasArchivesValidate()
       {
           this.MouseCursor = Cursors.Wait;
           BackgroundWorker worker = new BackgroundWorker();
           this.IsBusy = true;
           // ProgressBarVisibility = true;
           fliste.StartStopWait(true);
           worker.DoWork += (o, args) =>
           {
               try
               {
                  
                   ClientList = _clientservice.CLIENT_Archive_GETLISTE(societeCourante.IdSociete, true);
                  GlobalDatas.IdDataRefArchiveDatas = true;
                 
               }
               catch (Exception ex)
               {
                   args.Result = ex.Message + " ;" + ex.InnerException;
               }



           };
           worker.RunWorkerCompleted += (o, args) =>
           {
               if (args.Result != null)
               {
                   CustomExceptionView view = new CustomExceptionView();
                   view.Owner = localwindow;
                   view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                   view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                   view.ShowDialog();
                   this.MouseCursor = null;
                   this.IsBusy = false;
                   fliste.StartStopWait(false);
               }
               else
               {
                   this.MouseCursor = null;
                   this.IsBusy = false;
                   ProgressBarVisibility = false;
                   fliste.StartStopWait(false);
               }
               //this.OnPropertyChanged("ListEmployees");
           };

           worker.RunWorkerAsync();
       }

        void getDatas(DataTable tbleListe, string libelle, string objet)
        {
            DataRow[] nlignes = tbleListe.Select(string.Format("Libelle like '{0}%'", libelle.Trim()));
            DataTable filterDatatable = tbleListe.Clone();
            foreach (DataRow rows in nlignes)
                filterDatatable.ImportRow(rows);

                TaxePorataList = null;
                List<TaxeModel> newListe = new List<TaxeModel>();
                foreach (DataRow row in filterDatatable.Rows)
                    newListe.Add(new TaxeModel { ID_Taxe = int.Parse(row[0].ToString()), Libelle = row[1].ToString(), Taux = row[2].ToString() });
                TaxePorataList = newListe;
        }

       


        void loadDatasModel(int idLanguage)
        {
            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                     //ProduitList = _produitService.Produit_SELECTBY_ID_Language (idLanguage );
                     LanguageList = _language.LANGUE_SELECT(0);
                   
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException;
                }



            };
            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner =localwindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
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


        public void LoadproduitClients()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, args) =>
            {
                try
                {
                    List<produisuivi> liste = new List<produisuivi>();

                    DataTable table = produitService.Produit_SELECT_ByClient(ClientActif.IdSite, ClientActif.IdClient);

                        foreach (DataRow row in table.Rows)
                        {
                            produisuivi prod = new produisuivi();
                            prod.IDproduit = Convert.ToInt32(row["ID_produit"]);
                            prod.IdParam = row["ID_Param"] != DBNull.Value ? Convert.ToInt32(row["ID_Param"]) : 0;
                            prod.Libelle = Convert.ToString(row["Libelle"]);
                            prod.CompteExonere = Convert.ToString(row["CompteExonere"]);
                            prod.CompteOhada = Convert.ToString(row["compte_ohada"]);
                            prod.Isexonerate = Convert.ToBoolean(row["Exonerate"]);
                            prod.isProrata = Convert.ToBoolean(row["IsProrata"]);
                            prod.IsParameter = row["IsParameter"] != DBNull.Value ? Convert.ToBoolean(row["IsParameter"]) : false;
                            liste.Add(prod);
                        }

                        ListeProduitsuivis = liste;

                        isloadingParam = false;

                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException;
                }
            };

            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                    view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                    view.ShowDialog();

                }
                else
                {
                    //this.MouseCursor = null;
                    //this.IsBusy = false;
                    //ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");

               // initVAlues();
            };

            worker.RunWorkerAsync();
        }

        public void RefreshLoadingproduitClient()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, args) =>
            {
                try
                {
                    List<produisuivi> liste = new List<produisuivi>();

                    DataTable table = produitService.Produit_SELECT_ByClient(ClientActif.IdSite, ClientActif.IdClient);
                    foreach (DataRow row in table.Rows)
                    {
                        produisuivi prod = new produisuivi();
                        prod.IDproduit = Convert.ToInt32(row["ID_produit"]);
                        prod.IdParam = row["ID_Param"] != DBNull.Value ? Convert.ToInt32(row["ID_Param"]) : 0;
                        prod.Libelle = Convert.ToString(row["Libelle"]);
                        prod.CompteExonere = Convert.ToString(row["CompteExonere"]);
                        prod.CompteOhada = Convert.ToString(row["compte_ohada"]);
                        prod.Isexonerate = Convert.ToBoolean(row["Exonerate"]);
                        prod.isProrata = Convert.ToBoolean(row["IsProrata"]);
                        prod.IsParameter = row["IsParameter"] != DBNull.Value ? Convert.ToBoolean(row["IsParameter"]) : false;
                        liste.Add(prod);
                    }

                    ListeProduitsuivis = liste;
                    isloadingParam = false;
                 

                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException;
                }
            };

            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                    view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                    view.ShowDialog();

                }
                else
                {
                    //this.MouseCursor = null;
                    //this.IsBusy = false;
                    //ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");

               // initVAlues();
            };

            worker.RunWorkerAsync();
        }

        #endregion


        #region OPERATION REGION

        void canRefreshListeClient()
        {
            canSearchBy();
        }

        bool canExecuteRefresh()
        {
            return true;
        }
       

        private void canClose()
        {
            //_injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);
        }

        #region Save region


        private void canNewProduit()
        {
            //nouveau client
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur  )
            {

                
            }
            //LanguageList = _language.LANGUE_SELECT(0);


        }

        // save client
        private void canSaveProduit()
        {
            try
            {
                this.IsBusy = true ;
                if (ClientSelected.IdClient == 0)
                {
                    if (ClientSelected != null && ClientSelected.IdLangue > 0)
                    {
                        if (CompteSelected != null)
                        {
                            if (ExonerateSelected != null)
                            {
                                //if (DeviseSelected != null)
                                //{

                                    if (ClientSelected.TermeNombre >0)
                                    {
                                        if (LibelleSelected != null)
                                        {
                                            if (!IsPorataEnabled)
                                                ClientSelected.Idporata = 0;

                                            if (DeviseSelected == null)
                                                ClientSelected.IdDeviseFact = ParametersDatabase.IdDevise;

                                           // ClientSelected.IdDeviseFact = DeviseList.Find(d => d.ID_Devise == ParametersDatabase.IdDevise).ID_Devise;
                                            ClientSelected.IdSite = societeCourante.IdSociete;
                                            ClientSelected.IdTerme = LibelleSelected.ID;

                                            //_clientservice.CLIENT_ADD(ClientSelected);
                                            //loadDatas();

                                         
                                            ClientSelected = null;
                                            IsPorataEnabled = false;
                                            ExonerateSelected = null;
                                            ExonerateList = null;
                                            CompteSelected = null;
                                            CompteList = null;
                                            DeviseSelected = null;
                                            DeviseList = null;
                                            ClientSelected = null;
                                            LanguageSelected = null;
                                            LanguageList = null;
                                            LibelleSelected = null;
                                            LibelleList = null;
                                            //LibelleList = libelleService.GetLibelle_List(ClientSelected .IdLangue );
                                           // LanguageList = _language.LANGUE_SELECT(0);
                                            IsenableFields = false;
                                           // loadDatas();
                                            ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete,true);
                                            CacheDatas.ui_ClientClients = null ;
                                            CacheDatas.ui_ClientClients = ClientList;
                                            CacheClientList = null;
                                            CacheClientList = ClientList;
                                            NbreClients = ClientList.Count;
                                            CacheDatas.lastUpdatefacture = null;
                                        }
                                        else
                                            MessageBox.Show("Péciser le terme de paiement pour ce client", "Message ", MessageBoxButton.OK, MessageBoxImage.Stop);
                                    }
                                    else
                                        MessageBox.Show("Péciser le nombre terme", "Message ", MessageBoxButton.OK, MessageBoxImage.Stop);
                            }
                            else
                                MessageBox.Show("Péciser l'exonération du client", "Message ", MessageBoxButton.OK, MessageBoxImage.Stop);
                            //}
                            //else
                            //    MessageBox.Show("Péciser la dévise de Convertion", "Message ", MessageBoxButton.OK, MessageBoxImage.Stop);
                        }
                        else
                            MessageBox.Show("Préciser le Rib Sodexo de paiement du client", "Message ", MessageBoxButton.OK, MessageBoxImage.Stop);
                    }
                    else
                        MessageBox.Show("Vous devez choisire la langue", "Message ", MessageBoxButton.OK, MessageBoxImage.Stop);
                }
                else
                {
                    if (DeviseSelected == null && ClientSelected.IdDeviseFact == 0)
                        ClientSelected.IdDeviseFact = DeviseList.Find(d => d.ID_Devise == ParametersDatabase.IdDevise).ID_Devise;
                    else
                        if (DeviseSelected != null)
                        ClientSelected.IdDeviseFact = DeviseSelected.ID_Devise;
                    if (LibelleSelected != null)
                       // if (ClientSelected.IdTerme == 0)
                            ClientSelected.IdTerme = LibelleSelected.ID;
                    if (ClientSelected.IdExonere == 170003)
                        ClientSelected.Idporata = 0;

                    int id = 0;
                    _clientservice.CLIENT_ADD(ref id,ClientSelected);
                   
                  

                 

                    ExonerateSelected = null;
                    ExonerateList = null;
                    CompteSelected = null;
                    CompteList = null;
                    DeviseSelected = null;
                    DeviseList = null;
                    ClientSelected = null;
                    LibelleList = null;
                    IsenableFields = false;
                 
                   //loadDatas();
                    ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete,true);
                    CacheDatas.ui_ClientClients = null;
                    CacheDatas.ui_ClientClients = ClientList;
                    CacheClientList = null;
                    CacheClientList = ClientList;
                    CacheDatas.lastUpdatefacture = null;
                    
                }
                // methode add and update
                
                this.IsBusy = false;
            }
            catch (Exception ex){
                CustomExceptionView view = new CustomExceptionView();
                view.Owner =localwindow;
                view.Title = "MESSAGE INFORMATION AJOUT/MISE JOUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecuteSaveProduit()
        {
            bool values = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (ClientSelected != null)
                    values = true;
            }
            return values;
            
        }

        private void canDeleteProduit()
        {
             StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner =localwindow;
            messageBox.Title = "MESSAGE INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez Supprimer cet Client?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    this.IsBusy = true ;
                    _clientservice.CLIENT_DELETE(ClientActif.IdClient);
                    ExonerateSelected = null;
                    ExonerateList = null;
                    CompteSelected = null;
                    CompteList = null;
                    DeviseSelected = null;
                    DeviseList = null;
                    ClientSelected = null;
                    LibelleList = null;
                    IsenableFields = false;
                    LibelleSelected = null;
                    LibelleList = null;
                   // LibelleList = libelleService.GetLibelle_List(ClientSelected.IdLangue);
                   // loadDatas();
                    LanguageList = _language.LANGUE_SELECT(0);
                    ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete,true);
                    CacheDatas.ui_ClientClients = null;
                    CacheDatas.ui_ClientClients = ClientList;
                    CacheClientList = null;
                    CacheClientList = ClientList;
                    this.IsBusy = false;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner =localwindow;
                    view.Title = "MESSAGE INFORMATION SUPPRESSION";
                    if (ex.Message.Contains("constraint fails"))
                        view.ViewModel.Message = "Impossible de supprimer ce Client! Il est déja Associer Au moins a une Facture";
                    else 
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }
        }

        bool canExecuteDeleteProduit()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (ClientActif != null)
                    if (ClientActif.IdClient > 0)
                        values = true;
            }
            return values;

        }
        #endregion

        #region Recherche BY
        
      
        void canSearchBy()
        {

            if (string.IsNullOrEmpty(NomClient) && string.IsNullOrEmpty(NomVille))
            {
                loadDatas();
            }
            else
            {
                BackgroundWorker worker = new BackgroundWorker();
                this.IsBusy = true;
                // ProgressBarVisibility = true;
                fliste.StartStopWait(true);
                worker.DoWork += (o, args) =>
                {
                    try
                    {
                        //if (societeCourante != null)
                        //{
                        //    if (CacheDatas.ui_ClientClients == null)
                        //    {
                        ClientList = _clientservice.CLIENT_GETLISTEBY(societeCourante.IdSociete,NomClient,NomVille);

                        //    CacheDatas.ui_ClientClients = ClientList;
                        //}
                        //else ClientList = CacheDatas.ui_ClientClients;

                        CacheClientList = ClientList;
                        LanguageList = _language.LANGUE_SELECT(0);
                        // NbreClients = ClientList.Count;
                        //}
                    }
                    catch (Exception ex)
                    {
                        args.Result = ex.Message + " ;" + ex.InnerException;
                    }



                };
                worker.RunWorkerCompleted += (o, args) =>
                {
                    if (args.Result != null)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = localwindow;
                        view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                        view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                        view.ShowDialog();
                        this.MouseCursor = null;
                        this.IsBusy = false;
                        fliste.StartStopWait(false);
                    }
                    else
                    {
                        this.MouseCursor = null;
                        this.IsBusy = false;
                        ProgressBarVisibility = false;
                        fliste.StartStopWait(false);
                    }
                    //this.OnPropertyChanged("ListEmployees");
                };

                worker.RunWorkerAsync();
            }
        
        }

        bool canExecuteSearchBy()
        {
            if (CurrentDroit.Lecture || CurrentDroit.Developpeur)
                return true;
            else return false;
        }

        #endregion

        #region Contexte region

        void canEditContext()
        {
            WinModalClients view = new WinModalClients(ClientActif);
            view.Owner = localwindow;
            view.ShowDialog();

        }


        bool canExecuteEditcontext()
        {
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
                return ClientActif != null ? true : false;
            else return false;
        }


        void canParamContext()
        {
           // ClientModel client = ((Button)sender).CommandParameter as ClientModel;
            //DetailProduitClient vf = new DetailProduitClient(client);
            //vf.Owner = localwindow;
            //vf.ShowDialog();

            ClientModalParametres view = new ClientModalParametres();
           // DetailProduitClient view = new DetailProduitClient(client);
            view.Owner = localwindow;
            view.DataContext = this;
            view.ShowDialog();
        }

        bool canExecuteParametrecontext()
        {
            return ClientActif != null ? true : false; 
        }


        void canDeleteContext()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "MESSAGE INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez Supprimer cet Client?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                  
                    _clientservice.CLIENT_DELETE(ClientActif.IdClient);
                 
                    ClientActif = null;
                   
                   loadDatas();
                 
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "MESSAGE INFORMATION SUPPRESSION";
                    if (ex.Message.Contains("constraint fails"))
                        view.ViewModel.Message = "Impossible de supprimer ce Client! Il est déja Associer Au moins a une Facture";
                    else
                        view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }
        }

        bool canExecuteDeleteContext()
        {

            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (ClientActif != null)
                    if (ClientActif.IdClient > 0)
                        return true;
            }

            return false;
        }

            
        
        #endregion


        #region Suivi produit

        void canUpdateProduit()
        {
            try
            {
               
                foreach (produisuivi prod in ListeProduitsuiviUpdate)
                    produitService.Produit_Suivi_Add(prod.IdParam.Value, ClientActif.IdClient, prod.IDproduit.Value, ClientActif.IdSite, prod.IsParameter.Value);

                isloadingParam = true;

                 ListeProduitsuiviUpdate = null;
                 ListeProduitsuivis = null;
                List<produisuivi> listed = new List<produisuivi>();
                ListeProduitsuiviUpdate = listed;

                RefreshLoadingproduitClient();

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE INFORMATION";

                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }


        bool canExecuteUpdateProduit()
        {
            return ListeProduitsuiviUpdate != null ? (ListeProduitsuiviUpdate.Count > 0) ? true : false : false;
        }


        void canCancelProduit()
        {


            try
            {
                ListeProduitsuivis = null;
                List<produisuivi> liste = new List<produisuivi>();
                ListeProduitsuiviUpdate = liste;
                isloadingParam = true;
                RefreshLoadingproduitClient();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE INFORMATION";

                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecuteCancelProduit()
        {
            return ListeProduitsuiviUpdate != null ? (ListeProduitsuiviUpdate.Count > 0) ? true : false : false;
        }
        #endregion

        #endregion

        #region ANNULER PRORATA

        void canAnnulerPro()
        {
            
            ClientSelected.Idporata = 0;
            ProrataIndex = -1;

        }

        bool canExecuteAnnulerPro()
        {
            bool values = false;
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur )
            {
                if (ClientSelected != null)
                    if (TaxePorataSelected != null)
                        if (TaxePorataSelected.ID_Taxe > 0)
                            values = true;
            }
            return values;
        }
        #endregion


        #region SHOW MODAL REGION

        #region Modal Taxes
         
        #endregion


        #region Modal objet client

        private void canShowObjet()
        {
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
            {
                ObjetClientList vf = new ObjetClientList();
                vf.Owner = localwindow;
                if (ClientSelected != null)
                {
                    if (ClientSelected.IdClient == 0)
                        ClientSelected.IdClient = 77777;
                    vf.currentClient = ClientSelected;

                }
                vf.ShowDialog();
            }
        }

        bool canExecuteShowObjets()
        {
            return ClientSelected != null ? (ClientSelected.IdClient > 0 ? true : false) : false;
        }
        #endregion


        #region Modal Taxes

        private void canShowTaxes()
        {
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
            {
                TaxeModel items = null;

                WTaxeDevises vf = new WTaxeDevises();
                vf.Owner = localwindow;
                vf.ShowDialog();
                if (TaxePorataSelected != null)
                    items = TaxePorataSelected;

                DataTable newliste = taxeService.Taxe_SELECTByDataTable(societeCourante.IdSociete);
                getDatas(newliste, "p", "porata");

                if (items != null && TaxePorataList != null)
                    ProrataIndex = CommonModule.GetindexeComboBoxProrata(TaxePorataList, items.ID_Taxe);
            }

        }
        #endregion

        #region Modal Language
        
      
        private void canShowLaguage()
        {
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
            {
                WTaxeDevises vf = new WTaxeDevises();
                vf.Owner = localwindow;
                vf.ShowDialog();
                LanguageList = _language.LANGUE_SELECT(0);

            }
        }

     

        #endregion

        #region Modal Compte


        private void canShowCompte()
        {
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
            {

                CompteModel items = null;
                Compte vf = new Compte(); ;
                vf.Owner = localwindow;
                vf.ShowDialog();

                if (CompteSelected != null)
                    items = CompteSelected;

                CompteList = compteservice.COMPTE_SELECT();

                if (items != null && CompteList != null)
                    CompteIndex = CommonModule.GetindexeComboBoxCompte(CompteList, items.ID);
            }
            
        }

        #endregion

        #region Modal Exploitation


        private void canShowExploitation()
        {
            //Exploitation vf =new Exploitation();
            //vf.Owner = localwindow;
            //if (ClientSelected != null)
            //{
            //    if (ClientSelected.IdClient == 0)
            //        ClientSelected.IdClient = 77777;
            //    vf.currentClient = ClientSelected;
               
            //}
            //vf.ShowDialog();

        }
        bool canExecuteShowExpl()
        {
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
              return   ClientSelected != null ? (ClientSelected.IdClient > 0 ? true : false) : false;
            else return false;

           
        }

        #endregion

        #region Modal Exoneration


        void canShowExonerartion()
        {
            ExonerationModel items = null;
          
            Exoneration vf = new Exoneration ();
            vf.Owner = localwindow;
            vf.ShowDialog();

            if (ExonerateSelected != null)
                items = ExonerateSelected;
            if (CacheDatas.ui_ClientExonerations == null)
                ExonerateList = exonservice.EXONERATION_SELECT();
            else ExonerateList = CacheDatas.ui_ClientExonerations;

            if (items != null && ExonerateList !=null )
            ExonerateIndex = CommonModule .GetindexeComboBoxExoneration (ExonerateList,items.ID );
        }

        bool canExecuteExoneration()
        {
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
                return true;
            else return false;
        }

        #endregion

        #region Modal Devise


        void canShowDevise()
        {
            DeviseModel  items = null;
            Devises vf =new Devises();
            vf.Owner = localwindow;
            vf.ShowDialog();

            if (DeviseSelected  != null)
                items = DeviseSelected;

            if (CacheDatas.ui_ClientDevises == null)
                DeviseList = deviseService.Devise_SELECT(societeCourante.IdSociete);
            else DeviseList = CacheDatas.ui_ClientDevises;

            if (items != null && DeviseList != null)
                DeviseFactIndex = CommonModule.GetindexeComboBoxDeviseFacturation(DeviseList, items.ID_Devise);
        }

        bool canExecuteShowDevise()
        {
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
                return true;
            else return false;
        }

        #endregion

        #region Comptabilite
        void canShowCmpteGene()
        {
            CompteGeneral view = new CompteGeneral();
            view.Owner=localwindow;
            view.ShowDialog ();
        }

        bool canExecuteCompteGene()
        {
            if (CurrentDroit.Edition || CurrentDroit.Developpeur)
                return true;
            else return false;
        }


        void canShowCmpteAnalCli()
        {
            //CompteAnalytiqueClient view = new CompteAnalytiqueClient(;
            //view.Owner = localwindow;
            //view.ShowDialog();
        }

        bool canExecuteCompteAnalCli()
        {
            return true;
        }

        void canShowCmpteAnal()
        {

        }

        bool canExecuteCompteAnal()
        {
            return true;
        }
        #endregion

        #endregion


        #region Filter Region


        void filter(string values)
        {
            if (ClientList != null || ClientList.Count > 0)
            {
                newTable.Clear();

                DataRow row = null;

                foreach (ClientModel  sm in CacheClientList )
                {
                    row = newTable.NewRow();
                    row[0] = sm.IdClient ;
                    row[1] = sm.NomClient ;
                    row[2] = sm.NumeroContribuable ;
                    row[3] = sm.Ville ;
                    row[4] = sm.Rue1 ;
                    row[5] = sm.Rue2 ;
                    row[6] = sm.IdLangue ;
                    row[7] = sm.TermeNombre ;
                    row[8] = sm.TermeDescription ;
                    row[9] = sm.Idporata ;
                    row[10] = sm.DateEcheance ;
                    row[11] = sm.BoitePostal ;
                    row[12] = sm.IdExonere;
                    row[13] = sm.IdSite;
                    row[14] = sm.IdCompte;
                    row[15] = sm.NumemroImat;
                    row[16] = sm.IdDeviseFact ;
                    row[17] = sm.IdTerme ;
                   

                    newTable.Rows.Add(row);

                }

                DataRow[] nlignes = newTable.Select(string.Format("nom like '{0}%'", values.Trim()));
                DataTable filterDatatable = newTable.Clone();
                foreach (DataRow rows in nlignes)
                    filterDatatable.ImportRow(rows);

                ClientModel fm;
                ObservableCollection<ClientModel> newliste = new ObservableCollection<ClientModel>();

                foreach (DataRow r in filterDatatable.Rows)
                {
                    fm = new ClientModel()
                    {
                         IdClient  = Int32.Parse(r[0].ToString()),
                         NomClient  = r[1].ToString(),
                         NumeroContribuable  = r[2].ToString(),
                         Ville  = r[3].ToString(),
                         Rue1  = r[4].ToString(),
                         Rue2  = r[5].ToString(),
                       // Profile = CacheUsers.First(f => f.IdProfile == int.Parse(r[7].ToString())).Profile,
                         IdLangue  = int.Parse(r[6].ToString()),
                         TermeNombre =Int32 .Parse ( r[7].ToString()),
                         TermeDescription = r[8].ToString(),
                         Idporata =int .Parse ( r[9].ToString()),
                         DateEcheance = r[10].ToString(),
                         BoitePostal = r[11].ToString(),
                          IdExonere  = int.Parse(r[12].ToString()),
                          IdSite  = int.Parse(r[13].ToString()),
                          IdCompte  = int.Parse(r[14].ToString()),
                          NumemroImat  = r[15].ToString(), 
                          IdDeviseFact =int.Parse(r[16].ToString()), 
                          IdTerme =int.Parse(r[17].ToString()),
                         Porata = CacheClientList.First(cli => cli.Idporata == int.Parse(r[9].ToString())).Porata ,
                         Llangue = CacheClientList.First(l => l.IdLangue == int.Parse(r[6].ToString())).Llangue,
                         Compte = CacheClientList.First(c => c.IdCompte == int.Parse(r[14].ToString())).Compte ,
                         Exonerere = CacheClientList.First(e => e.IdExonere == int.Parse(r[12].ToString())).Exonerere,
                         DeviseConversion = CacheClientList.First(de => de.IdDeviseFact  == int.Parse(r[16].ToString())).DeviseConversion ,
                         LibelleTerme = CacheClientList.First(de => de.IdTerme  == int.Parse(r[17].ToString())).LibelleTerme  
                    };
                    newliste.Add(fm);
                }
                ClientList  = newliste;

            }
            else
            {
                loadDatas();

            }
        }

        #endregion
        #endregion
    }

    public class produisuivi
    {
        public int? IDproduit { get; set; }
        public int? IdParam { get; set; }
        public string Libelle { get; set; }
        public string CompteOhada { get; set; }
        public string CompteExonere { get; set; }
        public bool? Isexonerate { get; set; }
        public bool? isProrata { get; set; }
        public bool? IsParameter { get; set; }
    }
}
