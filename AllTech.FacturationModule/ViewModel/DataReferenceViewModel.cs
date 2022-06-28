using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Prism.Events;
using Microsoft.Practices.Unity;
using AllTech.FrameWork.Services;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Region;
using AllTech.FacturationModule.Views;
using AllTech.FrameWork.Views;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;

using System.Windows;
using AllTech.FrameWork.Utils;
using System.Windows.Controls;

namespace AllTech.FacturationModule.ViewModel
{
    public class DataReferenceViewModel : ViewModelBase
    {
        #region FIELDS
        
      
        private readonly IRegionManager _regionManager;
        private readonly IEventAggregator eventAggregator;
        private readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private RelayCommand showViewProduitCommand;
        private RelayCommand showViewPrestationCommand;
        private RelayCommand showViewSocieteCommand;
        private RelayCommand showViewClientCommand;
        private RelayCommand showViewUserCommand;
        private RelayCommand showViewCompanyCommand;
        private RelayCommand closeCommand;

        private string viewName;

        bool btnCloseVisible;
        SocieteModel societeCourante;
        UtilisateurModel userConnected;

        ParametresModel _parametersDatabase;


        DroitModel _currentDroit;
        object donneesRegion;
        object clientRegion;
        object produitRegion;
        object userRegion;
        object companyRegion;
        object cmptabiliteRegion;



        bool isMenuCompanyVisible;
        bool isMenuproductVisible;
        bool isMenuUsersVisible;
        bool isMenuClientVisible;
        bool isMenufacturesVisible;
        bool isMenuComptabiliteVisible;
        bool isCompanyLoader;
        bool isUsersLoader;
        bool isProductLoader;
        bool isClientsLoader;
        bool isDatarefBillLoader;
        bool isComptaLoader;
     

       

        object company;
        int indexCompany;
        int indexUsers;
        int indexProduit;
        int indexClient;
        int indexFacture;
        Window controls;
      

       
        #endregion

        public DataReferenceViewModel( Window _controls)
       {
           controls = _controls;
           societeCourante = GlobalDatas.DefaultCompany;
           ParametersDatabase = GlobalDatas.dataBasparameter;
           UserConnected = GlobalDatas.currentUser;
           CurrentDroit = UserConnected.Profile.Droit.Find(d =>d.IdVues ==6);
           loadViwDisplay();
          // LoadView();
           loadNewView();


       }

        #region PROPERTIES

        #region Content region

        public object CmptabiliteRegion
        {
            get { return cmptabiliteRegion; }
            set { cmptabiliteRegion = value;
            OnPropertyChanged("CmptabiliteRegion");
            }
        }

        public object DonneesRegion
        {
            get { return donneesRegion; }
            set
            {
                donneesRegion = value;

                OnPropertyChanged("DonneesRegion");
            }
        }

        public object ClientRegion
        {
            get { return clientRegion; }
            set
            {
                clientRegion = value;
                OnPropertyChanged("ClientRegion");
            }
        }

        public object ProduitRegion
        {
            get { return produitRegion; }
            set
            {
                produitRegion = value;
                OnPropertyChanged("ProduitRegion");
            }
        }

        public object UserRegion
        {
            get { return userRegion; }
            set
            {
                userRegion = value;
                OnPropertyChanged("UserRegion");
            }
        }

        public object CompanyRegion
        {
            get { return companyRegion; }
            set
            {
                companyRegion = value;
                OnPropertyChanged("CompanyRegion");
            }
        }

        #endregion

        public object Company
        {
            get { return company; }
            set { company = value;
            this.OnPropertyChanged("Company");
            }
        }

        public bool IsMenufacturesVisible
        {
            get { return isMenufacturesVisible; }
            set
            {
                isMenufacturesVisible = value;
                this.OnPropertyChanged("IsMenufacturesVisible");
            }
        }

        public bool IsMenuClientVisible
        {
            get { return isMenuClientVisible; }
            set
            {
                isMenuClientVisible = value;
                this.OnPropertyChanged("IsMenuClientVisible");
            }
        }

        public bool IsMenuUsersVisible
        {
            get { return isMenuUsersVisible; }
            set
            {
                isMenuUsersVisible = value;
                this.OnPropertyChanged("IsMenuUsersVisible");
            }
        }

        public bool IsMenuproductVisible
        {
            get { return isMenuproductVisible; }
            set
            {
                isMenuproductVisible = value;
                this.OnPropertyChanged("IsMenuproductVisible");
            }
        }

        public bool IsMenuCompanyVisible
        {
            get { return isMenuCompanyVisible; }
            set
            {
                isMenuCompanyVisible = value;
                this.OnPropertyChanged("IsMenuCompanyVisible");
            }
        }

        public bool IsMenuComptabiliteVisible
        {
            get { return isMenuComptabiliteVisible; }
            set { isMenuComptabiliteVisible = value;
            this.OnPropertyChanged("IsMenuComptabiliteVisible");
            }
        }


        //public DataRef_Company Company
        //{
        //    get { return this._container.Resolve<DataRef_Company>(); }
            
        //}

        public int IndexCompany
        {
            get { return indexCompany; }
            set { indexCompany = value;
            this.OnPropertyChanged("IndexCompany");
            }
        }

        public int IndexUsers
        {
            get { return indexUsers; }
            set { indexUsers = value;
            this.OnPropertyChanged("IndexUsers");
            }
        }

        public int IndexProduit
        {
            get { return indexProduit; }
            set { indexProduit = value;
            this.OnPropertyChanged("IndexProduit");
            }
        }

        public int IndexClient
        {
            get { return indexClient; }
            set { indexClient = value;
            this.OnPropertyChanged("IndexClient");
            }
        }

        public int IndexFacture
        {
            get { return indexFacture; }
            set { indexFacture = value;
            this.OnPropertyChanged("IndexFacture");
            }
        }

       

        public ParametresModel ParametersDatabase
        {
            get { return _parametersDatabase; }
            set { _parametersDatabase = value;
            this.OnPropertyChanged("ParametersDatabase");
            }
        }


        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set { userConnected = value;
            this.OnPropertyChanged("UserConnected");
            }
        }

        public DroitModel CurrentDroit
        {
            get { return _currentDroit; }
            set { _currentDroit = value;
            this.OnPropertyChanged("CurrentDroit");
            }
        }


        public string ViewName
        {
            get { return viewName; }
            set { viewName = value;
            this.OnPropertyChanged("ViewName");
            }
        }

        public bool BtnCloseVisible
        {
            get { return btnCloseVisible; }
            set { btnCloseVisible = value;
            this.OnPropertyChanged("BtnCloseVisible");
            }
        }
        #endregion


        #region ICOMMAND

        //show customer
        public RelayCommand ShowViewProduitCommand
        {
            get
            {

                if (this.showViewProduitCommand == null)
                {
                    this.showViewProduitCommand = new RelayCommand(param => this.canShowProduit_view());
                }
                return this.showViewProduitCommand;
            }

        }

        public RelayCommand ShowViewPrestationCommand
        {
            get
            {

                if (this.showViewPrestationCommand == null)
                {
                    this.showViewPrestationCommand = new RelayCommand(param => this.canShowPrestation_view());
                }
                return this.showViewPrestationCommand;
            }

        }


        public RelayCommand ShowViewSocieteCommand
        {
            get
            {

                if (this.showViewSocieteCommand == null)
                {
                    this.showViewSocieteCommand = new RelayCommand(param => this.canShowSociete_view());
                }
                return this.showViewSocieteCommand;
            }

        }

        public RelayCommand ShowViewClientCommand
        {
            get
            {

                if (this.showViewClientCommand == null)
                {
                    this.showViewClientCommand = new RelayCommand(param => this.canShowClient_view());
                }
                return this.showViewClientCommand;
            }

        }

        public RelayCommand ShowViewUserCommand
        {
            get
            {

                if (this.showViewUserCommand == null)
                {
                    this.showViewUserCommand = new RelayCommand(param => this.canShowUser_view());
                }
                return this.showViewUserCommand;
            }

        }
        //

        public RelayCommand ShowViewCompanyCommand
        {
            get
            {

                if (this.showViewCompanyCommand == null)
                {
                    this.showViewCompanyCommand = new RelayCommand(param => this.canShowCompany_view());
                }
                return this.showViewCompanyCommand;
            }

        }

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

        #endregion

        #region METHODS

        public void IsLoaderUsers()
        {
            if (!isUsersLoader)
            {
                DataRefUtilisateur Views = new DataRefUtilisateur(controls);
                UserRegion = Views;
                isUsersLoader = true;
            }
        }

        public void IsLoaderProducts()
        {
            if (!isProductLoader)
            {
                DatarefClient view = new DatarefClient(controls);
                ProduitRegion = view;
                isProductLoader = true;
            }
        }

        public void IsLoaderClients()
        {
            if (!isClientsLoader)
            {
                DataRef_Customers views = new DataRef_Customers(controls);
                ClientRegion = views;
                isClientsLoader = true;
            }
        }

        public void IsLoaderDatarefBill()
        {
            if (!isDatarefBillLoader)
            {
                DatarefInvoice view = new DatarefInvoice(controls);
                DonneesRegion = view;
                isDatarefBillLoader = true;
            }
        }

        public void IsLoaderAccount()
        {
            if (!isComptaLoader)
            {
                DataRef_Comptabilite view = new DataRef_Comptabilite(controls);
                CmptabiliteRegion = view;
                isComptaLoader = true;
            }
        }

       


        void loadNewView()
        {
            if (IsMenuCompanyVisible)
            {
                DataRef_Company Views = new DataRef_Company();
                CompanyRegion = Views;
                isCompanyLoader = true;
            }
        }

        void loadViwDisplay()
        {
            if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("societe")))
            {

               // DataRef_Company Views = new DataRef_Company(_window);
               // CompanyRegion = Views;
                IsMenuCompanyVisible = true;
            }
            if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("utilisateurs")))
            {

                //DataRefUtilisateur Views = new DataRefUtilisateur(_window);
                //UserRegion = Views;
                IsMenuUsersVisible = true;
            }

            if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("produits")))
            {
                //DatarefPrestation view = new DatarefPrestation(_window);
                //ProduitRegion = view;
                IsMenuproductVisible = true;
            }

            if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("client")))
            {

                //DataRef_Produit views = new DataRef_Produit(_window);
                //ClientRegion = views;
                IsMenuClientVisible = true;

            }

            if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("factures")))
            {
                //DatarefInvoice view = new DatarefInvoice(_window);
                //DonneesRegion = view;

                IsMenufacturesVisible = true;
            }

            IsMenuComptabiliteVisible = true;
        }

        void LoadView()
        {
            Utils.logUserActions("<--données refs : --Debut chargement  ", "");
            //  Utils.logUserActions(string.Format("<-- edition facture : --Nouvelle facture  {0} client :{1} crée par : {2}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient , UserConnected.Nom), "");
                                 
            IsMenuCompanyVisible = false;
            
            try
            {
               // _injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);

                if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.ToLower().Contains("societe")))
                {
                    //  this._regionManager.RegisterViewWithRegion(RegionNames.TabItemCompany,
                    //  () => this._container.Resolve<DataRef_Company>());
                   // Company = this._container.Resolve<DataRef_Company>();
                   // IsMenuCompanyVisible = true;
                   // IndexCompany = index;
                    Utils.logUserActions("<--données refs : --Chargement vue société  ", "");
                }
                else
                {
                    IsMenuCompanyVisible = false;
                    Company = null;
                  
                }

                if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("utilisateurs")))
                {
                   
                    this._regionManager.RegisterViewWithRegion(RegionNames.TabItemUser,
                                                               () => this._container.Resolve<DataRefUtilisateur>());
                    IsMenuUsersVisible = true;
                    Utils.logUserActions("<--données refs : --Chargement vue utilisateurs  ", "");
                    
                }
                else {
                   
                    IsMenuUsersVisible = false; 
                }




                if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("produits")))
                {
                   
                   // this._regionManager.RegisterViewWithRegion(RegionNames.TabItemProduct,
                                                             // () => this._container.Resolve<DatarefPrestation>());
                    IsMenuproductVisible = true;
                    Utils.logUserActions("<--données refs : --Chargement vue produits  ", "");
                }
                else
                {
                    
                    IsMenuproductVisible = false;
                }

                if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("client")))
                {
                    
                    this._regionManager.RegisterViewWithRegion(RegionNames.TabItemClients,
                                                             () => this._container.Resolve<DataRef_Customers>());
                    IsMenuClientVisible = true;
                    Utils.logUserActions("<--données refs : --Chargement vue client ", "");
                    
                }
                else
                {
                    
                    IsMenuClientVisible = false;
                }

                if (CurrentDroit.SousDroits.Exists(idv => idv.LibelleSouVue.Contains("factures")))
                {
                   
                    this._regionManager.RegisterViewWithRegion(RegionNames.TabItemFacture,
                                                             () => this._container.Resolve<DatarefInvoice>());

                    IsMenufacturesVisible = true;
                   
                }
                else
                {
                   
                    IsMenufacturesVisible = false;
                }
                Utils.logUserActions("<--données refs : --Fin  chargement  avec success ", "");
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("Erreur Chargement vues Données de references "+ex.Message , "");
                Utils.logUserActions("<--données refs  Fatal erreure lors du  chargement  ", "");
            }


        }

        private void canShowProduit_view()
        {
            //show customer
            _injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);

            DataRef_Customers produitView = _container.Resolve<DataRef_Customers>();
            IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ViewRegion,
                                               () => produitView);
        }

        private void canShowPrestation_view()
        {
            _injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);

           // DatarefPrestation produitView = _container.Resolve<DatarefPrestation>();
           // IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ViewRegion,
                                           //    () => produitView);
        }

        private void canShowSociete_view()
        {

        }

        private void canShowClient_view()
        {

        }

        private void canClose()
        {
            //_injectSingleViewService.ClearViewsFromRegion(RegionNames.ContentRegion);
            //UserAffiche uaffiche = _container.Resolve<UserAffiche>();


            //IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion,
            //                                     () => uaffiche);

            Communicator ctr = new Communicator();
            ctr.contentVue = "admin";
            EventArgs e1 = new EventArgs();
            ctr.OnChangeText(e1);

        }

        private void canShowCompany_view()
        {
            _injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);

            //DataRef_Company userView = _container.Resolve<DataRef_Company>();
            //IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ViewRegion,
            //                                   () => userView);
        }

        private void canShowUser_view()
        {
            _injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);

            
            DataRefUtilisateur userView = _container.Resolve<DataRefUtilisateur>();
            IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ViewRegion,
                                               () => userView);
        }

        #endregion
    }
}
