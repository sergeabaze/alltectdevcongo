using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Model;
using System.Collections.ObjectModel;
using System.Windows.Input;
using AllTech.FrameWork.Global;
using System.ComponentModel;
using System.Data;
using AllTech.FrameWork.Views;
using System.Windows;
using AllTech.FrameWork.Utils;


namespace AllTech.FacturationModule.Views.Modal
{
    public class CompteModalClientViewModel : ViewModelBase
    {
        #region fields

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
        private RelayCommand annulerProrataCommand;
        private RelayCommand listeObjetClientCommand;
        private RelayCommand listeCopteTiersCommand;
        private RelayCommand updateSuiviProduitCommand;
        private RelayCommand cancelSuiviProduitCommand;
        private RelayCommand addObjetClientDatasCommand;
        private RelayCommand saveExploitationCommand;
        private RelayCommand newExploitationCommand;

        ClientModel _clientSelected;
        ClientModel _clientservice;
        ObservableCollection<ClientModel> _clientList;
        ObservableCollection<ClientModel> _cacheClientList;
     

        LangueModel _language;
        ObservableCollection<LangueModel> _languageList;
        LangueModel _languageSelected;

        LibelleTermeModel libelleService;
        LibelleTermeModel libelleSelected;

        List<LibelleTermeModel> _libelleList;

        CompteTiersModel compteTierService;
        CompteTiersModel compteTierSelected;
        List<CompteTiersModel> compteTiersList;

      

        TaxeModel taxeService;
        TaxeModel _taxePorataSelected;
        List<TaxeModel> _taxePorataList;

        CompteModel compteservice;
        CompteModel compteSelected;
        List<CompteModel> compteList;

        ExonerationModel exonservice;
        ExonerationModel exonerateSelected;
        List<ExonerationModel> exonerateList;

        ExploitationFactureModel _exploitService;
        ExploitationFactureModel _exploitSelected;
        ObservableCollection<ExploitationFactureModel> _exploitList;

        SocieteModel societeCourante;
        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;

        DeviseModel deviseSelected;
        DeviseModel deviseService;
        List<DeviseModel> deviseList;
        List<DeviseModel> deviseFactureList;
        DeviseModel deviseFactureSelected;

      

        CompteGenralModel compteGeneService;
        List<CompteGenralModel> compteGeneListe;
        CompteGenralModel compteGeneSelected;

        CompteAnalytiqueModel compteAnalSelected;
        List<CompteAnalytiqueModel> comptesAnalList;
        CompteAnalytiqueModel compteanalService;


        ObjetGenericModel objetgenService;
        ObjetGenericModel objetGenSelected;
        ObservableCollection<ObjetGenericModel> _objetGenList;

        ObjetFactureModel objetservice;
        ObjetFactureModel _objeSelected;
        ObservableCollection<ObjetFactureModel> _objetList;

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
        int deviseConversionIndex;

        int compteIndex;
        int prorataIndex;
        int compteGeneIndex;
        int compteTiersIndex;
        int langueIndex;
        int compteanalIndex;

        int termelibelleIndex;

        int nbreClients;
        bool isClientActif;
        bool clientActifVisible;
        bool isloading;
        string exploitationLibelle;
        bool isVisibleCancelExploitation;
        bool isEnableClientdetail;
        bool isVisibleObject;

       

        DataTable tableProduitByClients;
        DataRow rowProduitselect;
        ProduitModel produitservice;

        public static bool IsOperation;

      
       
        #endregion

          Window localwindow;

          public CompteModalClientViewModel(Window _localwindow,ClientModel client)
        {
            localwindow = _localwindow;
            IsPorataEnabled = false;
            exonerateIndex = -1;
            ProrataIndex = -1;
            compteIndex = -1;
            deviseFactIndex = -1;
            deviseConversionIndex = -1;
          
            CompteGeneIndex = -1;
           

            _language = new LangueModel();
            _clientservice = new ClientModel();
            taxeService = new TaxeModel();
            compteservice = new CompteModel();
            exonservice = new ExonerationModel();
            deviseService = new DeviseModel();
            compteGeneService = new CompteGenralModel();
            libelleService = new LibelleTermeModel();
            compteTierService = new CompteTiersModel();
            produitservice = new ProduitModel();
            societeCourante = GlobalDatas.DefaultCompany;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            objetgenService = new ObjetGenericModel();
            objetservice = new ObjetFactureModel();
            _exploitService = new ExploitationFactureModel();
            compteanalService = new CompteAnalytiqueModel();

            if (CacheDatas.ui_currentdroitClientInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
                CacheDatas.ui_currentdroitClientInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitClientInterface;

            BtnDeleteVisible = true;
            BtnInitVisible = true;
            BtnNewVisible = true;
            BtnSaveVisible = true;

           // loadDatas();
            ClientSelected = client;
           loadDatas();
         
            //LoadIndex();
            isloading = false;
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur)
                ClientActifVisible = true;
            else ClientActifVisible = false;
            if (ClientSelected!=null )
            IsClientActif = ClientSelected.IsActive;
            IsOperation = false;
        }

        #region Properties

         

          public bool IsVisibleObject
          {
              get { return isVisibleObject; }
              set { isVisibleObject = value;
              this.OnPropertyChanged("IsVisibleObject");
              }
          }
          public bool IsEnableClientdetail
          {
              get { return isEnableClientdetail; }
              set { isEnableClientdetail = value;
              this.OnPropertyChanged("IsEnableClientdetail");
              }
          }

          public ObjetFactureModel ObjeSelected
          {
              get { return _objeSelected; }
              set
              {
                  _objeSelected = value;
                  this.OnPropertyChanged("ObjeSelected");
              }
          }

          public ObservableCollection<ObjetFactureModel> ObjetList
          {
              get { return _objetList; }
              set
              {
                  _objetList = value;
                  this.OnPropertyChanged("ObjetList");
              }
          }

          public ObservableCollection<ObjetGenericModel> ObjetGenList
          {
              get { return _objetGenList; }
              set
              {
                  _objetGenList = value;
                  this.OnPropertyChanged("ObjetGenList");
              }
          }

          public ObjetGenericModel ObjetGenSelected
          {
              get { return objetGenSelected; }
              set
              {
                  objetGenSelected = value;
                  this.OnPropertyChanged("ObjetGenSelected");
              }
          }

          #region Exploitation


          public int CompteanalIndex
          {
              get { return compteanalIndex; }
              set
              {
                  compteanalIndex = value;
                  this.OnPropertyChanged("CompteanalIndex");
              }
          }

          public CompteAnalytiqueModel CompteAnalSelected
          {
              get { return compteAnalSelected; }
              set
              {
                  compteAnalSelected = value;
                  this.OnPropertyChanged("CompteAnalSelected");
              }
          }


          public List<CompteAnalytiqueModel> ComptesAnalList
          {
              get { return comptesAnalList; }
              set
              {
                  comptesAnalList = value;
                  this.OnPropertyChanged("ComptesAnalList");
              }
          }


          public string ExploitationLibelle
          {
              get { return exploitationLibelle; }
              set
              {
                  exploitationLibelle = value;
                  if (!string.IsNullOrEmpty(value))
                      IsVisibleCancelExploitation = true;
                  else IsVisibleCancelExploitation = false;

                  this.OnPropertyChanged("ExploitationLibelle");
              }
          }

          public ExploitationFactureModel ExploitSelected
          {
              get { return _exploitSelected; }
              set
              {

                  _exploitSelected = value;
                  if (value != null)
                  {
                      ExploitationLibelle = value.Libelle;
                      if (ComptesAnalList != null && ComptesAnalList.Count > 0)
                          CompteanalIndex = ComptesAnalList.ToList().FindIndex(e => e.IdCompteAnalytique == value.IdCompteTiers);
                  }
                  this.OnPropertyChanged("ExploitSelected");
              }
          }

          public bool IsVisibleCancelExploitation
          {
              get { return isVisibleCancelExploitation; }
              set
              {
                  isVisibleCancelExploitation = value;
                  this.OnPropertyChanged("IsVisibleCancelExploitation");
              }
          }



          public ObservableCollection<ExploitationFactureModel> ExploitList
          {
              get { return _exploitList; }
              set
              {
                  _exploitList = value;
                  this.OnPropertyChanged("ExploitList");
              }
          }
          #endregion


          public DataRow RowProduitselect
          {
              get { return rowProduitselect; }
              set { rowProduitselect = value;
              this.OnPropertyChanged("RowProduitselect");
              }
          }

          public DataTable TableProduitByClients
          {
              get { return tableProduitByClients; }
              set { tableProduitByClients = value;
              this.OnPropertyChanged("TableProduitByClients");
              }
          }


          public bool IsClientActif
          {
              get { return isClientActif; }
              set
              {
                  isClientActif = value;
                  if (isloading)
                  {
                      if (ClientSelected != null)
                      {
                          if (ClientSelected.IdClient > 0)
                          {
                              if (value)
                                  SetClientActif(true);
                              else
                              {
                                  StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                  messageBox.Owner = localwindow;
                                  messageBox.Title = "INFORMATION  DESACTIVATIOn CLIENT";
                                  // messageBox.Icon = MessageBoxImage.Question;
                                  messageBox.ViewModel.Message = "Cette Action va Désactiver le ce client, Confirmez vous ?";
                                  if (messageBox.ShowDialog().Value == true)
                                  {
                                      SetClientActif(false);
                                      IsOperation = true;
                                  }
                              }
                          }
                          else value = false;

                      }
                  
                  }
                  //if (ClientSelected != null)
                  //{
                  //   // if (isloading)
                  //   // {
                  //        if (ClientSelected.IdClient > 0)
                  //        {
                  //            if (value)
                  //                SetClientActif(true);
                  //            else SetClientActif(false);
                  //        }
                  //        else value = false;
                  //    }
                 // }
                 isloading = true;
                  this.OnPropertyChanged("IsClientActif");

              }
          }


          public bool ClientActifVisible
          {
              get { return clientActifVisible; }
              set
              {
                  clientActifVisible = value;
                  this.OnPropertyChanged("ClientActifVisible");
              }
          }

          public int CompteTiersIndex
          {
              get { return compteTiersIndex; }
              set { compteTiersIndex = value;
              this.OnPropertyChanged("CompteTiersIndex");
              }
          }

          public CompteTiersModel CompteTierSelected
          {
              get { return compteTierSelected; }
              set { compteTierSelected = value;
              if (ClientSelected != null)
                  ClientSelected.IdCompteTiers = value.IdCompteT;
              this.OnPropertyChanged("CompteTierSelected");
              }
          }


          public List<CompteTiersModel> CompteTiersList
          {
              get { return compteTiersList; }
              set {
                  if (value != null && value.Count>0)
                    compteTiersList = value;
              this.OnPropertyChanged("CompteTiersList");
              }
          }


        #region COMMON

        //public ClientModel ClientSelected
        //{
        //    get { return _clientSelected; }
        //    set { _clientSelected = value;
        //    if (value != null)
        //    {

        //    }

        //    this.OnPropertyChanged("ClientSelected");
        //    }
        //}

        public int NbreClients
        {
            get { return nbreClients; }
            set
            {
                nbreClients = value;
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

        public int LangueIndex
        {
            get { return langueIndex; }
            set { langueIndex = value;
            this.OnPropertyChanged("LangueIndex");
            }
        }

        public int TermelibelleIndex
        {
            get { return termelibelleIndex; }
            set
            {
               
                termelibelleIndex = value;
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


        public int DeviseConversionIndex
        {
            get { return deviseConversionIndex; }
            set { deviseConversionIndex = value;
            this.OnPropertyChanged("DeviseConversionIndex");
            }
        }

        #endregion

        #region CLIENT REGION

        public ClientModel ClientSelected
        {
            get { return _clientSelected; }
            set
            {
                _clientSelected = value;
                if (value != null)
                {
                    if (value.IdClient > 0)
                    {
                        IsEnableClientdetail = true;
                                IsenableFields = true;
                                ClientActifVisible = true;
                        //        LibelleList = libelleService.GetLibelle_List(value.IdLangue);
                        //        if (LibelleList.Count > 0)
                        //        {
                        //            int ter = 0;
                        //            if (this.LibelleList != null)
                        //            {
                        //                foreach (var val in this.LibelleList)
                        //                {
                        //                    if (val.ID == this.ClientSelected.IdTerme)
                        //                    {
                        //                        TermelibelleIndex = ter;
                        //                        break;
                        //                    }

                        //                    ter++;
                        //                }
                        //            }
                    }
                    else
                    {
                        IsenableFields = true;
                        ClientActifVisible = false;
                        _clientSelected.NomClient = null;
                    }
                //            //TermelibelleIndex = CommonModule.GetindexeComboBoxLibelleTerme(LibelleList, value.IdTerme);
                   }
                   else
                    {
                       IsenableFields = true;
                       value = new ClientModel();
                    }



                //}
              
                this.OnPropertyChanged("ClientSelected");
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

        #region Comptabilite

        public int CompteGeneIndex
        {
            get { return compteGeneIndex; }
            set
            {
                compteGeneIndex = value;
                this.OnPropertyChanged("CompteGeneIndex");
            }
        }

        public List<CompteGenralModel> CompteGeneListe
        {
            get { return compteGeneListe; }
            set
            {
                compteGeneListe = value;
                this.OnPropertyChanged("CompteGeneListe");
            }
        }

        public CompteGenralModel CompteGeneSelected
        {
            get { return compteGeneSelected; }
            set
            {
                compteGeneSelected = value;
                this.OnPropertyChanged("CompteGeneSelected");
            }
        }

        #endregion

        #region PRODUIT REGION

        //public ObservableCollection<ProduitModel> ProduitList
        //{
        //    get { return _produitList; }
        //    set
        //    {
        //        _produitList = value;
        //        this.OnPropertyChanged("ProduitList");
        //    }
        //}

        //public ProduitModel ProduitSelected
        //{
        //    get { return _produitSelected; }
        //    set
        //    {
        //        _produitSelected = value;


        //        this.OnPropertyChanged("ProduitSelected");

        //    }
        //}
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
                if (value != null)
                    LibelleList = libelleService.GetLibelle_List(value.Id);
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
                        if ( value.CourtDesc.Contains("exo"))//si client exonere ou partiellement
                            IsPorataEnabled = true;
                        else
                        {
                            //TaxePorataSelected = null;
                            IsPorataEnabled = true; //client non exonere
                        }
                    }
                this.OnPropertyChanged("ExonerateSelected");
            }
        }

        public List<ExonerationModel> ExonerateList
        {
            get { return exonerateList; }
            set
            {
                exonerateList = value;
                this.OnPropertyChanged("ExonerateList");
            }
        }
        #endregion

        #region DEVISE REGION



        public DeviseModel DeviseSelected
        {
            get { return deviseSelected; }
            set
            {
                deviseSelected = value;
                if (value != null)
                    if (ClientSelected != null)
                        ClientSelected.IdDeviseConversion = value.ID_Devise;
                this.OnPropertyChanged("DeviseSelected");
            }
        }

        public List<DeviseModel> DeviseList
        {
            get { return deviseList; }
            set
            {
                deviseList = value;
                this.OnPropertyChanged("DeviseList");
            }
        }

        public DeviseModel DeviseFactureSelected
        {
            get { return deviseFactureSelected; }
            set
            {
                deviseFactureSelected = value;

                if (value != null)
                    if (ClientSelected != null)
                        ClientSelected.IdDeviseFact = value.ID_Devise;

                this.OnPropertyChanged("DeviseFactureSelected");
            }
        }

        public List<DeviseModel> DeviseFactureList
        {
            get { return deviseFactureList; }
            set
            {
                deviseFactureList = value;
                this.OnPropertyChanged("DeviseFactureList");
            }
        }

        #endregion

        #region Others
        
      

        public bool IsPorataEnabled
        {
            get { return isPorataEnabled; }
            set
            {
                isPorataEnabled = value;
                this.OnPropertyChanged("IsPorataEnabled");

            }
        }

        public bool IsenableFields
        {
            get { return isenableFields; }
            set
            {
                isenableFields = value;
                this.OnPropertyChanged("IsenableFields");
            }
        }

        public LibelleTermeModel LibelleSelected
        {
            get { return libelleSelected; }
            set
            {
                libelleSelected = value;
                this.OnPropertyChanged("LibelleSelected");
            }
        }

        public List<LibelleTermeModel> LibelleList
        {
            get { return _libelleList; }
            set
            {
                _libelleList = value;
                this.OnPropertyChanged("LibelleList");
            }
        }

        public bool IcheckExonarate
        {
            get { return _icheckExonarate; }
            set
            {
                _icheckExonarate = value;

                if (_clientSelected != null)
                    // _clientSelected.Exonere = value;

                    this.OnPropertyChanged("IcheckExonarate");
            }
        }

        #endregion
        #endregion

        #region Command


      

        // save client
        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(param => this.canSaveCustomer(), param => this.canExecuteSaveCustomer());
                }
                return this.saveCommand;
            }
        }

        //new client
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

        //delete client
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
                    this.listTaxeCommand = new RelayCommand(param => this.canShowTaxes(), param => this.canExecuteProrata());
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
                    this.listCompteCommand = new RelayCommand(param => this.canShowCompte(), param => this.canExecuteShowCompte());
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

        public RelayCommand ListeCopteTiersCommand
        {
            get
            {
                if (this.listeCopteTiersCommand == null)
                {
                    this.listeCopteTiersCommand = new RelayCommand(param => this.canShowCmpteTiers());
                }
                return this.listeCopteTiersCommand;
            }

        }

        public ICommand AddObjetClientDatasCommand
        {
            get
            {
                if (this.addObjetClientDatasCommand == null)
                {
                    this.addObjetClientDatasCommand = new RelayCommand(param => this.canAddobjet(), param => this.canExecuteAddObjet());
                }
                return this.addObjetClientDatasCommand;
            }


        }

        public RelayCommand SaveExploitationCommand
        {
            get
            {
                if (this.saveExploitationCommand == null)
                {
                    this.saveExploitationCommand = new RelayCommand(param => this.canSaveExploitation(), param => this.canExecuteExploitation());
                }
                return this.saveExploitationCommand;
            }

        }

        public ICommand NewExploitationCommand
        {
            get
            {
                if (this.newExploitationCommand == null)
                {
                    this.newExploitationCommand = new RelayCommand(param => this.canNewExploitation());
                }
                return this.newExploitationCommand;
            }


        }

        #endregion

        #region Methods

        #region LOAD REGION


        void loadRight()
        {
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Super || CurrentDroit.Developpeur)
                {

                    BtnDeleteVisible = true;
                    BtnInitVisible = true;
                    BtnNewVisible = true;
                    BtnSaveVisible = true;
                }
                else
                {
                    if (CurrentDroit.Suppression)
                    {
                        BtnDeleteVisible = true;

                    }
                    if (CurrentDroit.Ecriture)
                    {
                        BtnSaveVisible = true;
                        BtnNewVisible = true;
                    }
                }
            }
        }

      

        void loadDatas()
        {
            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;


           
            worker.DoWork += (o, args) =>
            {
                if (ClientSelected != null)
                {

                    if (!GlobalDatas.IdDataRefArchiveDatas)
                    {
                        try
                        {
                            // ProduitList = _produitService.Produit_SELECTBY_ID_Language(idLanguage);
                            LanguageList = _language.LANGUE_SELECT(0);
                            LangueIndex = -1;

                      
                            if (ClientSelected.IdClient > 0)
                                LibelleList = libelleService.GetLibelle_List(ClientSelected.IdLangue);

                            if (LibelleList!=null)
                                TermelibelleIndex = -1;
                      

                            CompteList = compteservice.COMPTE_SELECT();
                            CacheDatas.ui_ClientCompte = CompteList;

                            if (CompteList.Count > 0)
                                CompteIndex = -1;

                            ExonerateList = exonservice.EXONERATION_SELECT();
                            CacheDatas.ui_ClientExonerations = ExonerateList;

                            DeviseList = deviseService.DeviseClient_SELECT(societeCourante.IdSociete);
                            CacheDatas.ui_ClientDevises = DeviseList;

                            DeviseFactureList = deviseService.Devise_SELECT(societeCourante.IdSociete);

                            TaxePorataList = taxeService.Taxe_SELECTByRef("centimes", societeCourante.IdSociete);

                            CompteTiersList = compteTierService.SelectByclient(societeCourante.IdSociete, ClientSelected.IdClient);
                            ComptesAnalList = compteanalService.ModelCompteAnal_SelectAll(societeCourante.IdSociete);

                        }
                        catch (Exception ex)
                        {
                           // throw new Exception("Erreur Chargement Compte Tiers :" + ex.Message);
                            args.Result = ex.Message;
                        }
                    }
                    else if (GlobalDatas.IdDataRefArchiveDatas)
                    {
                        try
                        {
                            LanguageList = _language.LANGUE_SELECT(0);
                            LangueIndex = -1;

                            LibelleList = libelleService.GetLibelle_List_Archive(ClientSelected.IdLangue);
                            CompteList = compteservice.COMPTE_SELECT_Archive();
                            ExonerateList = exonservice.EXONERATION_SELECT();

                            DeviseList = deviseService.Devise_Archive_SELECT(societeCourante.IdSociete);
                            DeviseFactureList = deviseService.Devise_Archive_SELECT(societeCourante.IdSociete);

                            TaxePorataList = taxeService.Taxe_SELECTByRef_Archive("centimes", societeCourante.IdSociete);

                            CompteTiersList = compteTierService.SelectByclient_Archive(societeCourante.IdSociete, ClientSelected.IdClient);

                            if (LibelleList.Count > 0)
                                TermelibelleIndex = -1;
                        }

                        catch (Exception ex)
                        {
                           // throw new Exception("Erreur Chargement Archives :" + ex.Message);

                        }

                    }
                }

               

            };

            worker.RunWorkerCompleted += (o, args) =>
            {
               
                  
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "Erreur Chargement Liste objets";
                    view.ViewModel.Message = args.Result.ToString() ;
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

                initVAlues();
                loadeObjet();
                LoadExploitation();
            };

            worker.RunWorkerAsync();
          
        }

        void initVAlues()
        {
            if (ClientSelected != null)
            {
                if (ClientSelected.IdClient > 0)
                {
                    // TermelibelleIndex = 1;






                    if (this.LanguageList != null)
                        LangueIndex = LanguageList.ToList().FindIndex(l => l.Id == ClientSelected.IdLangue);
                      


                    //int dd = 0;
                    if (this.CompteList != null)
                    {
                        CompteIndex = CompteList.FindIndex(t => t.ID == ClientSelected.IdCompte);


                    }


                    //int j = 0;
                    if (this.ExonerateList != null)
                    {
                        ExonerateIndex = ExonerateList.FindIndex(t => t.ID == ClientSelected.IdExonere);

                    }


                    //int ee = 0;
                    if (this.DeviseList != null)
                    {
                        DeviseConversionIndex = DeviseList.FindIndex(t => t.ID_Devise == ClientSelected.IdDeviseConversion);


                    }

                     if (this.DeviseFactureList != null)
                        DeviseFactIndex = DeviseFactureList.FindIndex(t => t.ID_Devise == ClientSelected.IdDeviseFact);


                    //int d = 0;
                    if (this.TaxePorataList != null)
                    {
                        ProrataIndex = TaxePorataList.FindIndex(t => t.ID_Taxe == ClientSelected.Idporata);

                    }



                    //int idgene = 0;

                    //if (this.CompteTiersList != null && this.ClientSelected.IdCompteTiers != null)
                    //{
                    //    CompteTiersIndex = CompteTiersList.FindIndex(t => t.IdCompteT == ClientSelected.IdCompteTiers);

                    //}


                    if (LibelleList.Count > 0)
                    {
                        TermelibelleIndex = LibelleList.FindIndex(t => t.ID == ClientSelected.IdTerme);


                    }


                }
            }

           
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
                    view.Owner = localwindow;
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

        public void loadeObjet()
        {

            //BackgroundWorker worker = new BackgroundWorker();

            //worker.DoWork += (o, args) =>
            //{
            try
            {
                if (ClientSelected != null && ClientSelected.IdClient > 0)
                {
                    ObservableCollection<ObjetFactureModel> listeObjetUser = objetservice.OBJECT_FACTURE_GETLISTEByCLIENT(societeCourante.IdSociete, ClientSelected.IdClient);
                    if (listeObjetUser != null && listeObjetUser.Count > 0)
                    {
                        foreach (ObjetFactureModel objt in listeObjetUser)
                            objt.Isobjectselect = false;

                        ObjetList = listeObjetUser;
                    }
                    //ObjetList = objetservice.OBJECT_GENERIC_BYLANGUE(societeCourante.IdSociete, idlangue);
                }

                var liste = objetgenService.GetAll_OBJET_GENERIQUEBY_NON_Client(ClientSelected.IdClient, societeCourante.IdSociete, 1).ToList().FindAll(f => f.IdLangue == ClientSelected.IdLangue);
                ObservableCollection<ObjetGenericModel> newListe = new ObservableCollection<ObjetGenericModel>();

                foreach (ObjetGenericModel ob in liste)
                    newListe.Add(ob);

                ObjetGenList = newListe;

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Title = "INFORMATION ERREUR CHARGEMENT OBJETS";
                view.Owner = localwindow;
                view.ViewModel.Message = ex.Message.ToString();
                view.ShowDialog();
            }


        }

        public void LoadExploitation()
        {
            try
            {
                if (!GlobalDatas.IdDataRefArchiveDatas)
                {
                    if (ClientSelected != null && ClientSelected.IdClient > 0)
                        ExploitList = _exploitService.EXPLOITATION_FACTURE_CLIENT_LIST(societeCourante.IdSociete, ClientSelected.IdClient);
                }
                else
                    ExploitList = _exploitService.EXPLOITATION_FACTURE_CLIENT_LIST_Archive(societeCourante.IdSociete, ClientSelected.IdClient);
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Title = "INFORMATION ERREUR CHARGEMENT EXPLOITATION";
                view.Owner = localwindow;
                view.ViewModel.Message = ex.Message.ToString();
                view.ShowDialog();
            }
        }



        #endregion

        void SetClientActif(bool values)
        {
            try
            {
                _clientservice.CLIENT_ACTIF(ClientSelected.IdClient, societeCourante.IdSociete, values);
                ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                CacheDatas.ui_ClientClients = null;
                CacheDatas.ui_ClientClients = ClientList;
                CacheClientList = null;
                CacheClientList = ClientList;
                NbreClients = ClientList.Count;
                CacheDatas.lastUpdatefacture = null;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE INFORMATION AJOUT/MISE JOUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }

        }
        int i = 0;
        void LoadIndex()
        {

            if (this.LanguageList != null)
            {
                foreach (var langue in this.LanguageList)
                {
                    if (langue.Id == this.ClientSelected.IdLangue)
                    {
                        LangueIndex = i;
                        break;
                    }

                    i++;
                }
            }
        }

        #region OPERATION REGION

        void canRefreshListeClient()
        {
            loadDatas();
        }

        bool canExecuteRefresh()
        {
            return true;
        }


        private void canClose()
        {
            //_injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);
        }

        private void canNewProduit()
        {
            //nouveau client
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                _clientSelected = new ClientModel();
                ClientSelected = _clientSelected;
               // LanguageList = null;
               // LanguageSelected = null;
               // ExonerateList = null;
                ExonerateSelected = null;
               // DeviseList = null;
                DeviseSelected = null;
                DeviseFactureSelected = null;
                LibelleSelected = null;
               // LibelleList = null;
                TaxePorataList = null;
                TaxePorataSelected = null;
              
              //  CompteList = null;
                CompteSelected = null;
                LibelleSelected = null;
                IsEnableClientdetail = false;
                LangueIndex = -1;
                DeviseFactIndex = -1;
                DeviseConversionIndex = -1;
                CompteIndex = -1;
                CompteTiersIndex = -1;
                ObjetList = null;
                ExploitList = null;
                IsClientActif = false;
                ClientActifVisible = false;
               // LibelleList = null;
                //loadDatas();
            }
            //LanguageList = _language.LANGUE_SELECT(0);


        }

        // save client
        private void canSaveCustomer()
        {
            Int32 IDInsert = 0;

            try
            {
                this.IsBusy = true;
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

                                if (ClientSelected.TermeNombre > 0)
                                {
                                    if (LibelleSelected != null)
                                    {
                                       // if (!IsPorataEnabled)
                                         //   ClientSelected.Idporata = 0;

                                        if (DeviseSelected == null)
                                            ClientSelected.IdDeviseConversion = ParametersDatabase.IdDevise;

                                        if (DeviseFactureSelected == null)
                                            ClientSelected.IdDeviseFact = ParametersDatabase.IdDevise;
                                     
                                       


                                        // ClientSelected.IdDeviseFact = DeviseList.Find(d => d.ID_Devise == ParametersDatabase.IdDevise).ID_Devise;
                                        ClientSelected.IdSite = societeCourante.IdSociete;
                                        ClientSelected.IdTerme = LibelleSelected.ID;

                                        _clientservice.CLIENT_ADD(ref IDInsert, ClientSelected);
                                        //loadDatas();


                                        IsOperation = true;
                                        ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete,true);
                                        ClientSelected = ClientList.FirstOrDefault(cli => cli.IdClient == IDInsert);

                                       // CacheDatas.ui_ClientClients = null;
                                       // CacheDatas.ui_ClientClients = ClientList;
                                      //  CacheClientList = null;
                                       // CacheClientList = ClientList;

                                      
                                       
                                      //  CacheDatas.lastUpdatefacture = null;
                                        MessageBox.Show("Nouveau Client Crée", "Message ", MessageBoxButton.OK, MessageBoxImage.Information);
                                        RefreshGrid();
                                       // ClientSelected
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
                    
                    if (LibelleSelected != null)
                        // if (ClientSelected.IdTerme == 0)
                        ClientSelected.IdTerme = LibelleSelected.ID;
                   // if (ClientSelected.IdExonere == 170003)
                       // ClientSelected.Idporata = 0;

                    _clientservice.CLIENT_ADD(ref IDInsert,ClientSelected);
                    //ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                    
                   // CacheDatas.ui_ClientClients = null;
                  //  CacheDatas.ui_ClientClients = ClientList;
                    //CacheClientList = null;
                    //CacheClientList = ClientList;
                   // CacheDatas.lastUpdatefacture = null;
                    RefreshGrid();
                   
               // }
                    IsOperation = true;

                    MessageBox.Show("Client mise jour", "Message ", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                // methode add and update

                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE INFORMATION AJOUT/MISE JOUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecuteSaveCustomer()
        {
            bool values = false;
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (ClientSelected != null)
                    values = true;
            }
            return values;

        }

        void RefreshGrid()
        {
            EvenRefreshGridDataRef action = new EvenRefreshGridDataRef();
            EventArgs e1 = new EventArgs();
            if (CompteModalClientViewModel.IsOperation)
                action.typeOperation = "event";
            else action.typeOperation = "non";
            action.OnChangeList(e1);
        }

        private void canDeleteProduit()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "MESSAGE INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez Supprimer cet Client?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    this.IsBusy = true;
                    _clientservice.CLIENT_DELETE(ClientSelected.IdClient);
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
                    ClientList = _clientservice.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                    CacheDatas.ui_ClientClients = null;
                    CacheDatas.ui_ClientClients = ClientList;
                    CacheClientList = null;
                    CacheClientList = ClientList;
                    this.IsBusy = false;
                    IsOperation = true;
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

        bool canExecuteDeleteProduit()
        {
            bool values = false;
            if ( CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (ClientSelected != null)
                    if (ClientSelected.IdClient > 0)
                        values = true;
            }
            return values;

        }

        #endregion

        #region ANNULER Centies

        void canAnnulerPro()
        {

            ClientSelected.Idporata = 0;
            ProrataIndex = -1;
            IsOperation = true;
        }

        bool canExecuteAnnulerPro()
        {
            bool values = false;
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur)
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

        bool canExecuteShowObjets()
        {
            return ClientSelected != null ? (ClientSelected.IdClient > 0 ? true : false) : false;
        }
        #endregion


        #region Modal Taxes

        private void canShowTaxes()
        {
            TaxeModel items = null;
            if (!GlobalDatas.IdDataRefArchiveDatas)
            {
                WTaxeDevises vf = new WTaxeDevises();
                vf.Owner = localwindow;
                vf.ShowDialog();
                if (vf.IsAction)
                {
                    // if (TaxePorataSelected != null)
                    // items = TaxePorataSelected;

                    // DataTable newliste = taxeService.Taxe_SELECTByDataTable(societeCourante.IdSociete);
                    //getDatas(newliste, "p", "porata");

                    TaxePorataList = taxeService.Taxe_SELECTByRef("prorata", societeCourante.IdSociete);

                    if (TaxePorataList != null && ClientSelected.Idporata > 0)
                        ProrataIndex = TaxePorataList.FindIndex(p => p.ID_Taxe == ClientSelected.Idporata);
                    // ProrataIndex = CommonModule.GetindexeComboBoxProrata(TaxePorataList, items.ID_Taxe);
                }
            }

        }

        bool canExecuteProrata()
        {
            return GlobalDatas.IdDataRefArchiveDatas == false ? true : false;
        }
        #endregion

        #region Modal Language


        private void canShowLaguage()
        {
            if (!GlobalDatas.IdDataRefArchiveDatas)
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
            if (!GlobalDatas.IdDataRefArchiveDatas)
            {
                CompteModel items = null;
                Compte vf = new Compte(); ;
                vf.Owner = localwindow;
                vf.ShowDialog();

                // if (CompteSelected != null)
                // items = CompteSelected;

                 CompteList = compteservice.COMPTE_SELECT();
                // CompteSelected = CompteList.Find(cc => cc.ID == ClientSelected.IdCompte);
                 if (CompteList != null && ClientSelected.IdCompte>0)
                 CompteIndex = CompteList.FindIndex(c => c.ID == ClientSelected.IdCompte); // CommonModule.GetindexeComboBoxCompte(CompteList, items.ID);
            }
        }

        bool canExecuteShowCompte()
        {
            return GlobalDatas.IdDataRefArchiveDatas == false ? true : false;
        }

        #endregion

        #region Modal Exploitation


        private void canShowExploitation()
        {
            Exploitation vf = new Exploitation(ClientSelected);
            vf.Owner = localwindow;
            if (ClientSelected != null)
            {
                if (ClientSelected.IdClient == 0)
                    ClientSelected.IdClient = 77777;
             

            }
            vf.ShowDialog();

        }
        bool canExecuteShowExpl()
        {
            return ClientSelected != null ? (ClientSelected.IdClient > 0 ? true: false) : false;
        }

        #endregion

        #region Modal Exoneration


        void canShowExonerartion()
        {
            
                ExonerationModel items = null;

                Exoneration vf = new Exoneration();
                vf.Owner = localwindow;
                vf.ShowDialog();
                if (vf.IsACtion)
                {
                    ExonerateList = exonservice.EXONERATION_SELECT();
                    //else ExonerateList = CacheDatas.ui_ClientExonerations;
                    if (ExonerateList != null && ClientSelected.IdExonere > 0)
                        ExonerateIndex = ExonerateList.FindIndex(e => e.ID == ClientSelected.IdExonere);// CommonModule.GetindexeComboBoxExoneration(ExonerateList, items.ID);
                }
            
        }

        bool canExecuteExoneration()
        {
            return GlobalDatas.IdDataRefArchiveDatas == false ? true : false; ;
        }

        #endregion

        #region Modal Devise


        void canShowDevise()
        {
           
            
                DeviseModel items = null;
                Devises vf = new Devises();
                vf.Owner = localwindow;
                vf.ShowDialog();

                //if (DeviseSelected != null)
                //    items = DeviseSelected;

                //if (CacheDatas.ui_ClientDevises == null)
                DeviseList = deviseService.DeviseClient_SELECT(societeCourante.IdSociete);
                //else DeviseList = CacheDatas.ui_ClientDevises;

                if (DeviseList != null)
                    DeviseFactIndex = DeviseList.FindIndex(d => d.ID_Devise == ClientSelected.IdDeviseFact); // CommonModule.GetindexeComboBoxDeviseFacturation(DeviseList, items.ID_Devise);
            
        }

        bool canExecuteShowDevise()
        {
            return GlobalDatas.IdDataRefArchiveDatas==false?true:false;
        }

        #endregion

        #region Comptabilite

        void canShowCmpteGene()
        {
            CompteGeneral view = new CompteGeneral();
            view.Owner = localwindow;
            view.ShowDialog();
            if (view.IsOperationAction)
                CompteGeneListe = compteGeneService.ModelCompteGeneral_SelectAll(societeCourante.IdSociete);
        }

        bool canExecuteCompteGene()
        {
            return true;
        }


        void canShowCmpteAnalCli()
        {
            //CompteAnalytiqueClient view = new CompteAnalytiqueClient(ClientSelected.IdClient, ClientSelected.NomClient);
            //view.Owner = localwindow;
            //view.ShowDialog();

            CompteAnalityques view = new CompteAnalityques();
            view.Owner = localwindow;
            view.ShowDialog();
            if (view.IsOperationAction)
                ComptesAnalList = compteanalService.ModelCompteAnal_SelectAll(societeCourante.IdSociete);

            //if (view.IsOperationAction)
               // loadObjet();
            //CompteAnalityques view = new CompteAnalityques();
            //view.Owner = localwindow;
            //view.ShowDialog();
            //if (view.IsOperationAction)
            //    loadObjet();
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

        void canShowCmpteTiers()
        {
            CompteTiersModale view = new CompteTiersModale(ClientSelected.IdClient);
            view.Owner = localwindow;
            view.ShowDialog();
            if (!GlobalDatas.IdDataRefArchiveDatas)
                CompteTiersList = compteTierService.SelectByclient(societeCourante.IdSociete, ClientSelected.IdClient);
               
            else
    
                CompteTiersList = compteTierService.SelectByclient_Archive(societeCourante.IdSociete, ClientSelected.IdClient);


            if (CompteTiersList != null && ClientSelected.IdCompteTiers.HasValue)
                CompteTiersIndex = CompteTiersList.FindIndex(t => t.IdCompteT == ClientSelected.IdCompteTiers);

        }
        #endregion

        #endregion


        #region Add Objet

        void canAddobjet()
        {
            try
            {
                ObjetFactureModel objetAdd = new ObjetFactureModel();
                objetAdd.IdObjet = 0;
                objetAdd.IdobjetGen = ObjetGenSelected.IdObjetg;
                objetAdd.IdClient = ClientSelected.IdClient;
                objetAdd.Isobjectselect = true;
                objetAdd.IsNewObject = true;


                objetservice.OBJECT_FACTURE_ADD(objetAdd, societeCourante.IdSociete);
                loadeObjet();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE INFORMATION AJOUT/MISE JOUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                Utils.logUserActions(string.Format("<--UI ;5001; Client ;--erreure ajout Objet de facture  {0}   par {1} -- {2}", ObjetGenSelected.Libelle, UserConnected.Nom, ex.Message), "");
            }
        }

        bool canExecuteAddObjet()
        {
            if (ClientSelected == null)
                return false;
            if (ClientSelected != null && ClientSelected.IdClient == 0)
                return false;

            return ObjetGenSelected != null ? true : false;
        }
        #endregion

        #region Add exploitation

        void canNewExploitation()
        {
            ExploitSelected = new ExploitationFactureModel();
        }

        void canSaveExploitation()
        {
            try
            {


                ExploitSelected.IdClient = ClientSelected.IdClient;
                ExploitSelected.IdSite = societeCourante.IdSociete;
                ExploitSelected.IdLangue = ClientSelected.IdLangue;
                ExploitSelected.Libelle = ExploitationLibelle;
                if (CompteAnalSelected != null)
                    ExploitSelected.IdCompteTiers = CompteAnalSelected.IdCompteAnalytique;

                _exploitService.EXPLOITATION_FACTURE_ADD(ExploitSelected);

                ExploitList = _exploitService.EXPLOITATION_FACTURE_CLIENT_LIST(societeCourante.IdSociete, ClientSelected.IdClient);
                ExploitationLibelle = null;
                ExploitSelected = null;
                CompteAnalSelected = null;
                CompteanalIndex = 0;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Title = "INFORMATION ERREUR AJOUT EXPLOITATION";
                view.Owner = localwindow;
                view.ViewModel.Message = ex.Message.ToString();
                view.ShowDialog();
                Utils.logUserActions(string.Format("<--UI ;5001; Client ;--erreure ajout Objet Exploitation  {0}   par {1} -- {2}", ExploitationLibelle, UserConnected.Nom, ex.Message), "");

            }
        }

        bool canExecuteExploitation()
        {
            if (ClientSelected == null)
                return false;
            if (ClientSelected != null && ClientSelected.IdClient == 0)
                return false;

            if (ExploitSelected != null)
            {
                if (!string.IsNullOrEmpty(ExploitationLibelle))
                    return true;
                else return false;
            }
            else
                return false;

            // return ExploitSelected != null ? (string.IsNullOrEmpty(ExploitationLibelle) ? false : true) : true;
        }
        #endregion

      
        #endregion
    }

   
}
