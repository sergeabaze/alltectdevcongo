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
using AllTech.FacturationModule.Views.Modal;
using System.Globalization;


namespace AllTech.FacturationModule.ViewModel
{
    public class DatarefInvoiceViewModel : ViewModelBase
    {
        #region CHAMPS
        
      
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;


        private RelayCommand newCCommand;
        private RelayCommand saveCCommand;
        private RelayCommand deleteCCommand;

      


        private RelayCommand newObjCommand;
        private RelayCommand saveObjCommand;
        private RelayCommand deleteObjCommand;

        private RelayCommand newDCommand;
        private RelayCommand saveDCommand;
        private RelayCommand deleteDCommand;

        private RelayCommand newLCommand;
        private RelayCommand saveLCommand;
        private RelayCommand deleteLCommand;


        private RelayCommand newsCommand;
        private RelayCommand savesCommand;
        private RelayCommand deletesCommand;

        private RelayCommand showDevisesCommand;


        // terme paiement
        private RelayCommand newsTermeCommand;
        private RelayCommand saveTermeCommand;
        private RelayCommand deleteTermeCommand;

        private RelayCommand newsTaxeCommand;
        private RelayCommand saveTaxeCommand;
        private RelayCommand deleteTaxeCommand;

        private RelayCommand newsDeviseCommand;
        private RelayCommand saveDeviseCommand;
        private RelayCommand deleteDeviseCommand;

        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;

        bool btnNewVisible;
        bool btnSaveVisible;
        bool btnDeleteVisible;

        bool btnTermeNewVisible;
        bool btnTermeDeleteVisible;

        bool btnStatutNewVisible;
        bool btnStatutDeleteVisible;
        bool isDefaulTaxeenable;

      

       


       public  CompteModel compteService;
        CompteModel compteCourant;
        List<CompteModel> comptListe;
        SocieteModel societeCourante;

        StatutModel statutservice;
        StatutModel _statutSelected;
        ObservableCollection<StatutModel> _statutList;


        LangueModel _language;
        ObservableCollection<LangueModel> _languageList;
        ObservableCollection<LangueModel> _languageStatList;
        LangueModel _languageSelected;
        LangueModel _languageStatSelected;
        int indexLangue;
        ObservableCollection<LangueModel> _languagedisplayList;
        LangueModel _languageDisplaySelected;
        List<ClientModel> listeClients;

        ClientModel clientService;
        ClientModel currentClient;

        ObjetGenericModel objetservice;
        ObjetGenericModel _objetselected;
        ObservableCollection<ObjetGenericModel> _objetList;

      
        DepartementModel depService;
        DepartementModel depSelected;
        List<DepartementModel> departementList;

        TaxeModel taxeSelected;
        List<TaxeModel> taxeListes;
        DeviseModel deviseService;
        DeviseModel deviseSelected;
        List<DeviseModel> deviseList;

      
      
        TaxeModel taxeservice;

        bool isTvaradio;
        bool isProrataradio;
        bool ismargeSodexoadio;
        bool isTaxeDefaulChecked;
        bool? isDeviseDefaulChecked;
        bool isDevisecheckedEnable;

       

        LibelleTermeModel termeService;
        LibelleTermeModel termeSelected;
        List<LibelleTermeModel> termeList;
        LangueModel _langueTermeSelected;
        int langueIndex;
       public  bool isdoubleclickGrid;
      

        Window localwindow;
        #endregion


        public DatarefInvoiceViewModel(Window window)
        {
           

            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("factures")) ?? new DroitModel();
            societeCourante = GlobalDatas.DefaultCompany;
            compteService = new CompteModel();
            termeService = new LibelleTermeModel();
            localwindow = window;
           
            _language = new LangueModel();
            depService = new DepartementModel();
            objetservice = new ObjetGenericModel(); 
            statutservice = new StatutModel();
            clientService = new ClientModel();
            taxeservice = new TaxeModel();
            deviseService = new DeviseModel();
            loadRight();
            loadlanguage();
            //LoadStatut();
           // loadexploit();
           // LoadDevises();
            BtnDeleteVisible = true;
            BtnNewVisible = true;
            BtnSaveVisible = true;
            IsProrataradio=false;
            IsmargeSodexoadio=false;
            IsTvaradio = false;
            IsDefaulTaxeenable = false;
        }

        #region PROPERTIES

        #region COMMON

        public ObservableCollection<LangueModel> LanguagedisplayList
        {
            get { return _languagedisplayList; }
            set { _languagedisplayList = value;
            this.OnPropertyChanged("LanguagedisplayList");
            }
        }

        public LangueModel LanguageDisplaySelected
        {
            get { return _languageDisplaySelected; }
            set { _languageDisplaySelected = value;
            if (value != null)
            {
                if (Objetselected == null)
                    loadObjet(value.Id);
                else
                {
                    if (string.IsNullOrEmpty(Objetselected.Libelle))
                    {
                        loadObjet(value.Id);
                        Objetselected.IdLangue = value.Id;
                    }
                    else
                    {

                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = localwindow;
                        messageBox.Title = "INFORMATION ";
                        messageBox.ViewModel.Message = "Voulez vous annuler ce choix ?";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            Objetselected = null;
                            loadObjet(value.Id);
                        }else 
                        Objetselected.IdLangue = value.Id;
                    }
                    // Objetselected.IdLangue = value.Id;
                }
               
            }
            this.OnPropertyChanged("LanguageDisplaySelected");
            }
        }


        public int IndexLangue
        {
            get { return indexLangue; }
            set { indexLangue = value;
            this.OnPropertyChanged("IndexLangue");
            }
        }

        public LangueModel LanguageSelected
        {
            get { return _languageSelected; }
            set
            {
                _languageSelected = value;
                if (value != null)
                {
                if (Objetselected != null)
                   Objetselected.IdLangue = value.Id;
                //    //  loadObjet(value.Id );
                }
               
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

        public bool BtnStatutNewVisible
        {
            get { return btnStatutNewVisible; }
            set { btnStatutNewVisible = value;
            OnPropertyChanged("BtnStatutNewVisible");
            }
        }


        public bool BtnStatutDeleteVisible
        {
            get { return btnStatutDeleteVisible; }
            set { btnStatutDeleteVisible = value;
            OnPropertyChanged("BtnStatutDeleteVisible");
            }
        }

        public bool BtnTermeDeleteVisible
        {
            get { return btnTermeDeleteVisible; }
            set { btnTermeDeleteVisible = value;
            OnPropertyChanged("BtnTermeDeleteVisible");
            }
        }

        public bool BtnTermeNewVisible
        {
            get { return btnTermeNewVisible; }
            set { btnTermeNewVisible = value;
            OnPropertyChanged("BtnTermeNewVisible");
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


        #endregion

        #region CLIENt

        public ClientModel CurrentClient
        {
            get { return currentClient; }
            set { currentClient = value ;
            //if (value != null)
            //    Objetselected.IdClient = value.IdClient;
            this.OnPropertyChanged("CurrentClient");
            }
        }

        public List<ClientModel> ListeClients
        {
            get { return listeClients; }
            set { listeClients = value;
                
            this.OnPropertyChanged("ListeClients");
            }
        }
        #endregion

        #region TERME PAIEMENT

        public LangueModel LangueTermeSelected
        {
            get { return _langueTermeSelected; }
            set { _langueTermeSelected = value;
            if (value != null)
            {
                var termes = termeService.GetLibelle_List(value.Id);
                TermeList = termes;
                TermeSelected = null;
            }

            this.OnPropertyChanged("LangueTermeSelected");
            }
        }

        public LibelleTermeModel TermeSelected
        {
            get { return termeSelected; }
            set { termeSelected = value  ;
            this.OnPropertyChanged("TermeSelected");
            }
        }

        public List<LibelleTermeModel> TermeList
        {
            get { return termeList; }
            set { termeList = value;
            this.OnPropertyChanged("TermeList");
            }
        }
         
        #endregion

        #region OBJEt FACTURE

        public int LangueIndex
        {
            get { return langueIndex; }
            set { langueIndex = value;
            this.OnPropertyChanged("LangueIndex");
            }
        }

        public ObjetGenericModel  Objetselected
        {
            get { return _objetselected; }
            set
            {
                _objetselected = value   ;
                this.OnPropertyChanged("Objetselected");
            }
        }


        public ObservableCollection<ObjetGenericModel> ObjetList
        {
            get { return _objetList; }
            set
            {
                _objetList = value;
                this.OnPropertyChanged("ObjetList");
            }
        }

        #endregion

        #region EXPLOITATION

        public DepartementModel DepSelected
        {
            get { return depSelected; }
            set { depSelected = value  ;
            OnPropertyChanged("DepSelected");
            }
        }


        public List<DepartementModel> DepartementList
        {
            get { return departementList; }
            set { departementList = value;
            OnPropertyChanged("DepartementList");
            }
        }
        #endregion

        #region TAXES

        public bool IsDefaulTaxeenable
        {
            get { return isDefaulTaxeenable; }
            set { isDefaulTaxeenable = value;
            OnPropertyChanged("IsDefaulTaxeenable");
            }
        }

        public bool IsTaxeDefaulChecked
        {
            get { return isTaxeDefaulChecked; }
            set {
                if (value)
                {
                    if (TaxeListes.Exists(t => t.TaxeDefault == true))
                    {
                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION MODIFICATION TAXE";
                        messageBox.ViewModel.Message = string.Format("Il existe déja Une valeur par défaut définie pour cette Tva\n Voulez vous définire une nouvelle Valeur Par défaut ?");
                        if (messageBox.ShowDialog().Value == true)
                        {
                            isTaxeDefaulChecked = value;
                            if (TaxeSelected != null)
                            {
                                TaxeListes.Find(t => t.TaxeDefault == true).TaxeDefault = false;
                                TaxeSelected.TaxeDefault = true;
                                isTaxeDefaulChecked = value;
                            }
                        }
                       
                    }
                    else {
                        if (TaxeSelected != null)
                            TaxeSelected.TaxeDefault = true;
                        isTaxeDefaulChecked = value;
                    }
                    
                }
                else
                {
                   
                    isTaxeDefaulChecked = value;
                }

            OnPropertyChanged("IsTaxeDefaulChecked");
            }
        }

        public bool IsmargeSodexoadio
        {
            get { return ismargeSodexoadio; }
            set { ismargeSodexoadio = value;
            if (value)
                LoadMaregeSodexo();
            IsDefaulTaxeenable = false;
            OnPropertyChanged("IsmargeSodexoadio");
            }
        }

        public bool IsProrataradio
        {
            get { return isProrataradio; }
            set { isProrataradio = value;
            if (value)
                LoadProrata();
            IsDefaulTaxeenable = false;
            OnPropertyChanged("IsProrataradio");
            }
        }

        public bool IsTvaradio
        {
            get { return isTvaradio; }
            set { isTvaradio = value;
                if (value)
            LoadTva();
                IsDefaulTaxeenable = true;
            OnPropertyChanged("IsTvaradio");
            }
        }


        public TaxeModel TaxeSelected
        {
            get { return taxeSelected; }
            set { taxeSelected = value;
            if (value != null)
            {
                IsTaxeDefaulChecked = false;
                //if (value.ID_Taxe > 0)
                //{
                //    if (value.TaxeDefault)
                //}
            }
            OnPropertyChanged("TaxeSelected");
            }
        }

        public List<TaxeModel> TaxeListes
        {
            get { return taxeListes; }
            set { taxeListes = value;
            OnPropertyChanged("TaxeListes");
            }
        }
        #endregion

        #region Devises

        public bool IsDevisecheckedEnable
        {
            get { return isDevisecheckedEnable; }
            set { isDevisecheckedEnable = value;
            OnPropertyChanged("IsDevisecheckedEnable");
            }
        }

        public bool? IsDeviseDefaulChecked
        {
            get { return isDeviseDefaulChecked; }
            set {
                if (value.HasValue)
                {
                    if (DeviseSelected != null && DeviseSelected.ID_Devise > 0)
                    {
                        if (!string.IsNullOrEmpty(DeviseSelected.Libelle))
                        {
                            if (DeviseList.Exists(d => d.IsDefault == true))
                            {
                                StyledMessageBoxView messageBox = new StyledMessageBoxView();

                                messageBox.Owner = Application.Current.MainWindow;
                                messageBox.Title = "INFORMATION MODIFICATION DEVISE";
                                messageBox.ViewModel.Message = string.Format("Il existe déja Une valeur par défaut définie pour cette Dévise\n Voulez vous définire une nouvelle Valeur Par défaut ?");
                                if (messageBox.ShowDialog().Value == true)
                                {
                                    DeviseList.Find(d => d.IsDefault == true).IsDefault = false;

                                    DeviseList.Find(d => d.ID_Devise == DeviseSelected.ID_Devise).IsDefault = true;
                                    DeviseSelected.IsDefault = value.Value;
                                    isDeviseDefaulChecked = value;
                                }
                            }
                            else
                            {
                                isDeviseDefaulChecked = value;
                                DeviseSelected.IsDefault = value.Value;
                            }
                        }
                        else
                            MessageBox.Show(" le libéllé de dévise est oblgatoire");
                    }
                    else isDeviseDefaulChecked = value;

                  
                   
                }
                else isDeviseDefaulChecked = false;
            OnPropertyChanged("IsDeviseDefaulChecked");
            }
        }

        public DeviseModel DeviseSelected
        {
            get { return deviseSelected; }
            set {
                if (value != null)
                {
                    IsDevisecheckedEnable = true;
                   // IsDeviseDefaulChecked = value.IsDefault;
                }
                else IsDevisecheckedEnable = false;

                deviseSelected = value;
            OnPropertyChanged("DeviseSelected");
            }
        }

        public List<DeviseModel> DeviseList
        {
            get { return deviseList; }
            set { deviseList = value;
            OnPropertyChanged("DeviseList");
            }
        }

        #endregion

        #region Statut

       public StatutModel StatutSelected
       {
           get { return _statutSelected; }
           set
           {
               _statutSelected = value;
              
               this.OnPropertyChanged("StatutSelected");
           }
       }


       public ObservableCollection<StatutModel> StatutList
       {
           get { return _statutList; }
           set
           {
               _statutList = value;
               this.OnPropertyChanged("StatutList");
           }
       }

       public ObservableCollection<LangueModel> LanguageStatList
       {
           get { return _languageStatList; }
           set { _languageStatList = value;
           this.OnPropertyChanged("LanguageStatList");
           }
       }

       public LangueModel LanguageStatSelected
       {
           get { return _languageStatSelected; }
           set { _languageStatSelected = value;
           if (value != null)
           {
               if (StatutSelected != null)
               {
                   StatutSelected.IdLangue = value.Id;
               }
           }
           this.OnPropertyChanged("LanguageStatSelected");
           }
       }
       #endregion

        #endregion

        #region ICOMMAND


        #region TERME PAIEMENT

       public ICommand NewsTermeCommand
       {
           get
           {
               if (this.newsTermeCommand == null)
               {
                   this.newsTermeCommand = new RelayCommand(param => this.canNewTerme());
               }
               return this.newsTermeCommand;
           }
       }

       public ICommand SaveTermeCommand
       {
           get
           {
               if (this.saveTermeCommand == null)
               {
                   this.saveTermeCommand = new RelayCommand(param => this.canSaveTerme(), param => this.canExecuteSaveTerme());
               }
               return this.saveTermeCommand;
           }
       }

       public ICommand DeleteTermeCommand
       {
           get
           {
               if (this.deleteTermeCommand == null)
               {
                   this.deleteTermeCommand = new RelayCommand(param => this.canDeleteTerme(), param => this.canExecuteDeleteTerme());
               }
               return this.deleteTermeCommand;
           }
       }
        #endregion

        #region EXPLOITATION FAcTURE

        public ICommand SaveDCommand
        {
            get
            {
                if (this.saveDCommand == null)
                {
                    this.saveDCommand = new RelayCommand(param => this.canDSave(), param => this.canDExecuteSave());
                }
                return this.saveDCommand;
            }
        }

        public ICommand NewDCommand
        {
            get
            {
                if (this.newDCommand == null)
                {
                    this.newDCommand = new RelayCommand(param => this.canDNew());
                }
                return this.newDCommand;
            }


        }

        public ICommand DeleteDCommand
        {
            get
            {
                if (this.deleteDCommand == null)
                {
                    this.deleteDCommand = new RelayCommand(param => this.canDDelete(), param => this.canDExecute());
                }
                return this.deleteDCommand;
            }


        }
        #endregion

        #region OBJET FACTURE

        public ICommand SaveObjCommand
        {
            get
            {
                if (this.saveObjCommand == null)
                {
                    this.saveObjCommand = new RelayCommand(param => this.canSaveObj(), param => this.canExecuteSaveObj());
                }
                return this.saveObjCommand;
            }
        }

        public ICommand NewObjCommand
        {
            get
            {
                if (this.newObjCommand == null)
                {
                    this.newObjCommand = new RelayCommand(param => this.canNewObjet());
                }
                return this.newObjCommand;
            }


        }

        public ICommand DeleteObjCommand
        {
            get
            {
                if (this.deleteObjCommand == null)
                {
                    this.deleteObjCommand = new RelayCommand(param => this.canDeleteObj(), param => this.canExecuteObj());
                }
                return this.deleteObjCommand;
            }


        }
        

        #endregion

        #region TAXES

        public RelayCommand ShowDevisesCommand
        {
            get
            {
                if (this.showDevisesCommand == null)
                {
                    this.showDevisesCommand = new RelayCommand(param => this.canShowDevise());
                }
                return this.showDevisesCommand;
            }

        }

        public ICommand NewsTaxeCommand
        {
            get
            {
                if (this.newsTaxeCommand == null)
                {
                    this.newsTaxeCommand = new RelayCommand(param => this.canNewTaxe());
                }
                return this.newsTaxeCommand;
            }
        }

        public ICommand SaveTaxeCommand
        {
            get
            {
                if (this.saveTaxeCommand == null)
                {
                    this.saveTaxeCommand = new RelayCommand(param => this.canSaveTaxe(), param => this.canExecuteSavetaxe());
                }
                return this.saveTaxeCommand;
            }
        }

        public ICommand DeleteTaxeCommand
        {
            get
            {
                if (this.deleteTaxeCommand == null)
                {
                    this.deleteTaxeCommand = new RelayCommand(param => this.canDeleteTaxe(), param => this.canExecuteDeleteTaxe());
                }
                return this.deleteTaxeCommand;
            }
        }

     
        #endregion

        #region Devises
       

        public ICommand NewsDeviseCommand
        {
            get
            {
                if (this.newsDeviseCommand == null)
                {
                    this.newsDeviseCommand = new RelayCommand(param => this.canNewDevises());
                }
                return this.newsDeviseCommand;
            }
        }

        public ICommand SaveDeviseCommand
        {
            get
            {
                if (this.saveDeviseCommand == null)
                {
                    this.saveDeviseCommand = new RelayCommand(param => this.canSaveDevise(), param => this.canExecuteSaveDevise());
                }
                return this.saveDeviseCommand;
            }
        }

        public ICommand DeleteDeviseCommand
        {
            get
            {
                if (this.deleteDeviseCommand == null)
                {
                    this.deleteDeviseCommand = new RelayCommand(param => this.canDeleteDevise(), param => this.canExecuteDeleteDevise());
                }
                return this.deleteDeviseCommand;
            }
        }

        #endregion

        #region Langues

        public ICommand SaveLCommand
        {
            get
            {
                if (this.saveLCommand == null)
                {
                    this.saveLCommand = new RelayCommand(param => this.canLSave(), param => this.canLExecuteSave());
                }
                return this.saveLCommand;
            }
        }

        public ICommand DeleteLCommand
        {
            get
            {
                if (this.deleteLCommand == null)
                {
                    this.deleteLCommand = new RelayCommand(param => this.canLDelete(), param => this.canLExecute());
                }
                return this.deleteLCommand;
            }
        }

        public ICommand NewLCommand
        {
            get
            {
                if (this.newLCommand == null)
                {
                    this.newLCommand = new RelayCommand(param => this.canLNew());
                }
                return this.newLCommand;
            }


        }
        #endregion

        #region STATUT

        public ICommand SavesCommand
        {
            get
            {
                if (this.savesCommand == null)
                {
                    this.savesCommand = new RelayCommand(param => this.cansSave(), param => this.cansExecuteSave());
                }
                return this.savesCommand;
            }
        }

        public ICommand NewsCommand
        {
            get
            {
                if (this.newsCommand == null)
                {
                    this.newsCommand = new RelayCommand(param => this.cansNew());
                }
                return this.newsCommand;
            }


        }

        public ICommand DeletesCommand
        {
            get
            {
                if (this.deletesCommand == null)
                {
                    this.deletesCommand = new RelayCommand(param => this.cansDelete(), param => this.cansExecuteDelete());
                }
                return this.deletesCommand;
            }


        }
        #endregion

        #endregion

        #region METHODS

        void LoadTva()
        {
            try
            {
                TaxeListes = taxeservice.Taxe_SELECTByRef("tva", societeCourante.IdSociete);
                //IndexeTaxe = TaxeList.FindIndex(t => t.ID_Taxe == TaxeSelected.ID_Taxe);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probleme de chargement Liste Taxes ! "+ex.Message);
                //return;
               // Utils.logUserActions(string.Format("<-- UI edition facture --> Erreur de chargement Liste taxes par défault {0}  par : {1}", ex.Message, UserConnected.Loggin), "");
            }
        }

       public  void LoadDevises()
        {
            try
            {
                if (DeviseList == null )
                DeviseList =deviseService.Devise_SELECT( societeCourante.IdSociete);
                //IndexeTaxe = TaxeList.FindIndex(t => t.ID_Taxe == TaxeSelected.ID_Taxe);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probleme de chargement Liste Dévises ! " + ex.Message);
                //return;
                // Utils.logUserActions(string.Format("<-- UI edition facture --> Erreur de chargement Liste taxes par défault {0}  par : {1}", ex.Message, UserConnected.Loggin), "");
            }
        }

       public void refreshDevises()
       {
           try
           {
               DeviseList = deviseService.Devise_SELECT(societeCourante.IdSociete);
               //IndexeTaxe = TaxeList.FindIndex(t => t.ID_Taxe == TaxeSelected.ID_Taxe);
           }
           catch (Exception ex)
           {
               MessageBox.Show("Probleme de chargement Liste Dévises ! " + ex.Message);
               //return;
               // Utils.logUserActions(string.Format("<-- UI edition facture --> Erreur de chargement Liste taxes par défault {0}  par : {1}", ex.Message, UserConnected.Loggin), "");
           }
       }

        void LoadProrata()
        {
            try
            {
                TaxeListes = taxeservice.Taxe_SELECTByRef("centimes", societeCourante.IdSociete);
                //IndexeTaxe = TaxeList.FindIndex(t => t.ID_Taxe == TaxeSelected.ID_Taxe);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probleme de chargement Liste Taxes ! " + ex.Message);
                //return;
                // Utils.logUserActions(string.Format("<-- UI edition facture --> Erreur de chargement Liste taxes par défault {0}  par : {1}", ex.Message, UserConnected.Loggin), "");
            }
        }

        void LoadMaregeSodexo()
        {
            try
            {
                TaxeListes = taxeservice.Taxe_SELECTByRef("marge", societeCourante.IdSociete);
                //IndexeTaxe = TaxeList.FindIndex(t => t.ID_Taxe == TaxeSelected.ID_Taxe);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probleme de chargement Liste Taxes ! " + ex.Message);
                //return;
                // Utils.logUserActions(string.Format("<-- UI edition facture --> Erreur de chargement Liste taxes par défault {0}  par : {1}", ex.Message, UserConnected.Loggin), "");
            }
        }

        void loadRight()
        {
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Super || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
                {

                    BtnDeleteVisible = true;
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

        void loadlanguage()
        {
            BackgroundWorker worker = new BackgroundWorker();
         
            worker.DoWork += (o, args) =>
            {
                try
                {
                    LanguageList = _language.LANGUE_SELECT(0);
                    if (LanguageList != null)
                    {
                        LanguageStatList = LanguageList;
                        LanguagedisplayList = LanguageList;
                        LangueIndex = -1;
                    }
                  
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LANGUE";
                  
                }

                try
                {
                  
                   // ListeClients = clientService.CLIENT_GETLISTE(societeCourante.IdSociete).ToList();
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT CLIENTS";
                  
                }

                try
                {
                    //if (societeCourante != null)
                      //  DepartementList = depService.Departement_SELECT(societeCourante.IdSociete);
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + "" + "ERREUR CHARGEMENT LISTE DEPARTEMENTS";
                   
                }


              



            };
            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = args.Result.ToString ().Substring (args.Result.ToString ().LastIndexOf (";"));
                    view.ViewModel.Message = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                    view.ShowDialog();

                }


            };

            worker.RunWorkerAsync();
        }

        public void LoadDepartmentDatas()
        {
            try
            {
                if (DepartementList == null)
                {
                    if (societeCourante != null)
                        DepartementList = depService.Departement_SELECT(societeCourante.IdSociete);
                    else throw new Exception("Pas société pour ce dépatement");
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "ERREURE CHARGEMENT DEPARTEMENTS";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }


        public void LoadTermeDatas()
        {
            try
            {



                //if (TermeList == null)
                //{
                //    if 
                //    TermeList = termeService.GetLibelle_List(LangueTermeSelected.Id);
                //}
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "ERREURE CHARGEMENT DEPARTEMENTS";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        #region Langues

        private void canLNew()
        {
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur )
            {
                _languageSelected = new LangueModel();
                LanguageSelected = _languageSelected;
            }
        }
        private void canLSave()
        {
            try
            {
                _language.LANGUE_ADD(LanguageSelected);
                loadlanguage();
                LanguageSelected = null;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "Warning Message Add Taxe";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                //IsBusy = false;
                //this.MouseCursor = null;
            }
        }

        bool canLExecuteSave()
        {
            bool values = false;
            if (CurrentDroit.Developpeur || CurrentDroit.Ecriture)
            {
                if (LanguageSelected != null)
                    values = true;

            }
            return values;
        }


        private void canLDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "SUPPRESSION INFORMATION LANGUES";
            messageBox.ViewModel.Message = "voulez vous supprimez cette langue?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    _language.LANGUE_DELETE(LanguageSelected.Id);
                    LanguageSelected = null;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "MESSAGE SUPPRESSION LANGUE";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    //IsBusy = false;
                    //this.MouseCursor = null;
                }
            }

        }

        bool canLExecute()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (LanguageSelected != null)
                    if (LanguageSelected .Id >0)
                    values = true;

            }
            return values;

            
        }

        #endregion

        #region Terme Paiement



        private void canNewTerme()
        {
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                TermeSelected = null;
            }

        }

        private void canSaveTerme()
        {
            try
            {
                if (TermeSelected.ID > 0)
                {
                    termeService.LIBELLETERME_ADD(TermeSelected, societeCourante.IdSociete);

                    var termes = termeService.GetLibelle_List(LangueTermeSelected.Id );
                    TermeList = termes;
                    TermeSelected = null;
                }
               
              
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "Information de sauvegarde terme paiement";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecuteSaveTerme()
        {
            bool values = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (TermeSelected != null)
                    values = true;

            }
            return values;

           
           
        }

        private void canDeleteTerme()
        {
             StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMAION SUPPRESSION TERME PAIEMENT";
            messageBox.ViewModel.Message = "Voulez Vous Supprimer ce terme ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    termeService.LIBELLETERME_DELETE(TermeSelected.ID);
                    TermeSelected = null;
                    var termes = termeService.GetLibelle_List(LangueTermeSelected.Id);
                    TermeList = termes;

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "Information de suppression terme Paiement";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();

                }
            }
        }

        bool canExecuteDeleteTerme()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (TermeSelected != null)
                    if (TermeSelected.ID  > 0)
                        values = true;

            }
            return values;
        }
        #endregion

        #region Departements

        void loadexploit()
        {

           
                try
                {
                    if (societeCourante!=null )
                    DepartementList = depService.Departement_SELECT(societeCourante.IdSociete );
                }
                catch (Exception ex)
                {
                   
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "ERREUR CHARGEMENT LISTE EXPLOITATIONS";
                    view.ViewModel.Message =ex.Message ;
                    view.ShowDialog();
                }

           
        }

        private void canDNew()
        {
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                depSelected = new DepartementModel();
                DepSelected = depSelected;
            }
        }

        // new or update
        private void canDSave()
        {
            try
            {
                DepSelected.IdSite = societeCourante.IdSociete;
                depService.Departement_ADD(DepSelected);
                DepSelected = null;
                loadexploit();

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION DE MISE JOUR DEPARTEMENT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }
        bool canDExecuteSave()
        {
            bool values = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (DepSelected != null)
                    values = true;

            }
            return values;
        }


        private void canClose()
        {
        }


        private void canDDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION DE SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez Vous Supprimez Cet Objet ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    depService.Departement_DELETE(DepSelected.IdDep);
                    loadexploit();
                    DepSelected  = null;
                  
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "INFORMATION DE SUPPRESSION";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                  
                }
            }
        }
        bool canDExecute()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (DepSelected != null)
                    if (DepSelected.IdDep  > 0)
                        values = true;

            }
            return values;  
        }

       


      
        #endregion

        #region OBJET FACTURE

        void loadObjet(Int32 idlangue)
        {

            BackgroundWorker worker = new BackgroundWorker();
           
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (societeCourante !=null )
                    //ObjetList = objetservice.OBJECT_GENERIC_GETLISTE(societeCourante.IdSociete);
                    ObjetList = objetservice.OBJECT_GENERIC_BYLANGUE(societeCourante.IdSociete, idlangue);

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
                  
                }
               

            };

            worker.RunWorkerAsync();
        }


        private void canNewObjet()
        {
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                _objetselected = new ObjetGenericModel();
                Objetselected = _objetselected;
                LangueIndex = -1;
            }
        }


        private void canDeleteObj()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer Cet Objet ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                   
                    objetservice.OBJECT_GENERIC_DELETE(Objetselected.IdObjetg );
                    loadObjet(Objetselected.IdLangue);
                    Objetselected = null;
                    LangueIndex = -1;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "INFORMATION SUPPRESSION";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                   
                }
            }
        }
        bool canExecuteObj()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (Objetselected != null)
                    if (Objetselected.IdObjetg  > 0)
                        values = true;

            }
            return values; 

          
        }

        private void canSaveObj()
        {
            try
            {
               
                if (Objetselected.IdObjetg  == 0)
                {
                    //if (LanguageDisplaySelected != null)
                    //{
                       
                            if (societeCourante != null)
                                if (Objetselected.IdObjetg  == 0)
                                    Objetselected.IdSite = societeCourante.IdSociete;
                            if (Objetselected.IdLangue == 0)
                                Objetselected.IdLangue = LanguageDisplaySelected.Id;

                            objetservice.OBJECT_GENERIC_ADD (Objetselected);
                            loadObjet(Objetselected.IdLangue);
                            Objetselected = null;
                            LanguageSelected = null;
                            LangueIndex = -1;
                   // }
                    //else
                    //    MessageBox.Show("Présier la langue");
                }
                else
                {
                    //if (Objetselected != null && Objetselected.IdClient == 0)
                    //    Objetselected.IdClient = currentClient.IdClient;
                     
                    objetservice.OBJECT_GENERIC_ADD (Objetselected);
                    loadObjet(Objetselected.IdLangue );
                    Objetselected = null;
                    LanguageDisplaySelected = null;
                    LangueIndex = -1;
                    //currentClient = null;
                   
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION DE MISE JOUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
              
            }
        }
        bool canExecuteSaveObj()
        {
            bool values = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (Objetselected != null)
                        values = true;

            }
            return values;

          
        }

        #endregion

        #region TAXES

        private void canShowDevise()
        {
            WTaxeDevises vf = _container.Resolve<WTaxeDevises>();
            vf.Owner = localwindow;
            vf.ShowDialog();

        }

        void canNewTaxe()
        {
            TaxeSelected = new TaxeModel();
            TaxeSelected.ID_Taxe = 0;

        }

        void canSaveTaxe()
        {
            try
            {
                if (TaxeSelected.ID_Taxe == 0)
                {
                    if (IsProrataradio)
                        TaxeSelected.Libelle = "centimes";
                    if (IsTvaradio)
                        TaxeSelected.Libelle = "Tva";
                    if (IsmargeSodexoadio)
                        TaxeSelected.Libelle = "Marge";
                    TaxeSelected.IdSite = societeCourante.IdSociete;
                }
               
                taxeservice.Taxe_ADD(TaxeSelected);

                if (IsProrataradio)
                    LoadProrata(); ;
                if (IsTvaradio)
                {
                   
                    if (IsTaxeDefaulChecked)
                    {
                        SettingsModel settingService = new SettingsModel();
                        SettingsModel config = new SettingsModel { Code = "dtva", Libelle = TaxeSelected.ID_Taxe.ToString(), IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);
                        ParametersDatabase.Idtva = 0;
                        ParametersDatabase.Idtva = TaxeSelected.ID_Taxe;

                        MessageBox.Show(" Modification de la Tva par défaut Pris en compte");
                    }

                    LoadTva();
                }
                if (IsmargeSodexoadio)
                    LoadMaregeSodexo();
                TaxeSelected = null;

                IsTaxeDefaulChecked = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION DE MISE JOUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecuteSavetaxe()
        {
            bool val = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (TaxeSelected != null)
                {
                    if (IsProrataradio || IsmargeSodexoadio || IsTvaradio)
                        val = true;

                }
            }
            return val;
        }

        void canDeleteTaxe()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer Cette taxe ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {

                    taxeservice.Taxe_DELETE(TaxeSelected.ID_Taxe);

                    if (IsProrataradio)
                        LoadProrata(); ;
                    if (IsTvaradio)
                        LoadTva();
                    if (IsmargeSodexoadio)
                        LoadMaregeSodexo();
                    TaxeSelected = null;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "INFORMATION SUPPRESSION";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();

                }
            }

        }

        bool canExecuteDeleteTaxe()
        {
            bool val = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (TaxeSelected != null)
                {
                    if (TaxeSelected.ID_Taxe > 0)
                        val = true;

                }
            }
            return val;
        }
        #endregion

        #region Region Devises

        void canNewDevises()
        {
            deviseSelected =new DeviseModel();
            DeviseSelected = deviseSelected;
            DeviseSelected.ID_Devise = 0;
            IsDeviseDefaulChecked = false;
            IsDevisecheckedEnable = true;
        }

        void canSaveDevise()
        {
            CultureInfo culture= CultureInfo.CurrentCulture;
            try
            {
                double valdevise = 0;
                if (IsDeviseDefaulChecked.HasValue)
                    DeviseSelected.IsDefault = IsDeviseDefaulChecked.Value;
                DeviseSelected.IdSite = societeCourante.IdSociete;
                if (string.IsNullOrEmpty(DeviseSelected.Libelle) && string.IsNullOrEmpty(DeviseSelected.Taux))
                    return;

                var valtest=DeviseSelected.Taux.Replace(",", culture.NumberFormat.NumberDecimalSeparator).Replace(",", culture.NumberFormat.NumberDecimalSeparator);

                //if (double.TryParse(valtest,r, out valdevise))
                //{

                //}
                //else
                //{
                //    MessageBox.Show("le Taux de conversion est une valeur numérique");
                //    return;
                //}
                if (DeviseList != null && DeviseList.Count > 0)
                {
                    if (DeviseSelected.ID_Devise == 0)
                    {
                        if (DeviseList.Exists(d => d.Libelle.ToLower().Contains(DeviseSelected.Libelle.ToLower())))
                        {
                            MessageBox.Show("Cette dévise existe déja ");
                            return;
                        }
                    }
                }
                
                deviseService.Devise_ADD(DeviseSelected);

                refreshDevises();
                DeviseSelected = null;
                IsDeviseDefaulChecked = null;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION DE MISE JOUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecuteSaveDevise()
        {
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
                return DeviseSelected != null ? true : false;
            else return false;
        }

        void canDeleteDevise()
        {
            try
            {
                 StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer Cette Dévise ?";
            if (messageBox.ShowDialog().Value == true)
            {
                if (DeviseSelected.IsDefault)
                {
                    MessageBox.Show(" Définir d'abord la nouvelle valeur par défaut");
                    return;
                }

                deviseService.Devise_DELETE(DeviseSelected.ID_Devise);
                refreshDevises();
                DeviseSelected = null;
            }

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION DE SUPPRESSION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecuteDeleteDevise()
        {
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
                return DeviseSelected != null ? (DeviseSelected.ID_Devise > 0) : false;
            else return false;
        }
        #endregion

        #region STATUT REGION


        public void LoadStatut()
        {
            
                try
                {
                    StatutList = statutservice.STATUT_FACTURE_GETLISTE();

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "ERREUR CHARGEMENT LISTE  STATUT";
                    view.ViewModel.Message =ex.Message ;
                    view.ShowDialog();
                }



          
        }
        private void cansNew()
        {
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                _statutSelected = new StatutModel();
                StatutSelected = _statutSelected;
            }
        }

        private void cansSave()
        {
            try
            {
                if (StatutSelected.IdStatut > 0)
                {
                    statutservice.STATUT_FACTURE_ADD(StatutSelected);
                    StatutSelected = null;
                    LanguageStatSelected = null;
                    LoadStatut();
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION SAUVEGARDE STATUT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                //IsBusy = false;
                //this.MouseCursor = null;
            }
        }

        bool cansExecuteSave()
        {
            bool values = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (StatutSelected != null)
                    values = true;
            }
            return values;
        }


        private void cansDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMAION SUPPRESSION STATUT";
            messageBox.ViewModel.Message = "Voulez Vous Supprimer Ce Statut ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    statutservice.STATUT_FACTURE_DELETE(StatutSelected.IdStatut);
                    StatutList = statutservice.STATUT_FACTURE_GETLISTE();
                    StatutSelected = null;
                    LanguageStatSelected = null;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "INFORMATION SUPPRESSION STATUT";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                  
                }
            }

        }

        bool cansExecuteDelete()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (StatutSelected != null)
                    if (StatutSelected.IdStatut >0)
                    values = true;
            }
            return values;

           
        }
        #endregion

        #endregion
    }
}
