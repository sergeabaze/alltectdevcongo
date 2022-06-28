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
using AllTech.FacturationModule.Views.Modal;
using System.Threading;
using AllTech.FrameWork.Global;
using System.Data;
using AllTech.FrameWork.Utils;
using System.Windows.Data;

namespace AllTech.FacturationModule.ViewModel
{
    public class DataRefPrestationViewModel : ViewModelBase
    {
        #region FIELDS

        Window WpfParent;

        private readonly IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
        public  readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;

        private RelayCommand newCommand;
        private RelayCommand newDetailCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand deleteDetailCommand;
        private RelayCommand savedetailCommand;
        private RelayCommand showdetailCommand;
        private RelayCommand showDevisesCommand;
        private RelayCommand showfactureInfoCommand;
        private RelayCommand showRechercheCommand;

        private RelayCommand closeCommand;


        ProduitModel _produitService;
        ProduitModel _produitSelected;
        ProduitModel _produitActive;

     
        ObservableCollection<ProduitModel> _produitList;
        ObservableCollection<DetailProductModel> detailProduits;

    
        ObservableCollection<ProduitModel> _cacheProduitList;
        private ObservableCollection<CheckedListItem<string>> productFilters = new ObservableCollection<CheckedListItem<string>>();
        private CollectionViewSource viewSource =new CollectionViewSource ();

        
   



        LangueModel _language;
        ObservableCollection<LangueModel> _languageList;
        LangueModel _languageSelected;

        ObservableCollection<LangueModel> _langueList;
        LangueModel _langueSelected;

       

        List<ClientModel> _clientList;
        ClientModel clientService;
        ClientModel _clientSelected;

        List<ClientModel> _clientprodList;
        ClientModel _clientProdSelected;

      

        DetailProductModel _detailService;
        DetailProductModel _detailselected;

      
        List<DetailProductModel> _detailList;

        SocieteModel societeCourante;
       
        ParametresModel _parametersDatabase;
        UtilisateurModel userConnected;
        DroitModel _currentDroit;

        bool btnNewVisible;
        bool btnSaveVisible;
        bool btnDeleteVisible;

        bool txtEnable_a;
        bool txtEnable_b;

        int idOldlangue = 0;
        string nomProduitRecherche;

        bool iscmblanguebtnVisible;
        bool istxtRechercheBtnVisible;

        int langueSelectIndex;

      

        Window localwindow;
        bool isRadProduitActive;
        bool isRadProduitArchive;

      
        #endregion

        #region CONSTRUCTOR



        public DataRefPrestationViewModel(Window window)
        {
            //_regionManager = regionManager;
            //_container = container;
            localwindow = window;
            ProgressBarVisibility = false;
            //_injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
            _produitService = new ProduitModel();
            clientService = new ClientModel();
            _detailService = new DetailProductModel();
            TxtEnable_a = false;
            TxtEnable_b = false;
            _language = new LangueModel();
            societeCourante = GlobalDatas.DefaultCompany;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;

            if (CacheDatas.ui_currentdroitProduitInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("produits")) ?? new DroitModel();
                CacheDatas.ui_currentdroitProduitInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitProduitInterface;

                 loadRight();

                 GlobalDatas.IdDataRefArchiveDatas = false;
                 if (CurrentDroit.Lecture || CurrentDroit.Developpeur)
                loadDatas();
          
         
        }

        #endregion

        #region PROPERTIES


        #region ENABLE TEXTFIELD

        public bool TxtEnable_a
        {
            get { return txtEnable_a; }
            set { txtEnable_a = value;
            this.OnPropertyChanged("TxtEnable_a");
            }
        }


        public bool TxtEnable_b
        {
            get { return txtEnable_b; }
            set { txtEnable_b = value;
            this.OnPropertyChanged("TxtEnable_b");
            }
        }
        #endregion

        #region VISIBLE BUTTON
        
       

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

        public bool IsRadProduitArchive
        {
            get { return isRadProduitArchive; }
            set { isRadProduitArchive = value;
            if (value)
            {
                GlobalDatas.IdDataRefArchiveDatas = true;
                if (LangueSelected!=null )
                chargementProduits(LangueSelected.Id);
            }
            else GlobalDatas.IdDataRefArchiveDatas = false;
            this.OnPropertyChanged("IsRadProduitArchive");
            }
        }

        public bool IsRadProduitActive
        {
            get { return isRadProduitActive; }
            set { isRadProduitActive = value;
            if (value)
            {
                GlobalDatas.IdDataRefArchiveDatas = false;
                if (LangueSelected != null)
                chargementProduits(LangueSelected.Id);
            }
            else GlobalDatas.IdDataRefArchiveDatas = true;
            this.OnPropertyChanged("IsRadProduitActive");
            }
        }


        public ProduitModel ProduitActive
        {
            get { return _produitActive; }
            set { _produitActive = value;
            this.OnPropertyChanged("ProduitActive");
            }
        }


        public ObservableCollection<DetailProductModel> DetailProduits
        {
            get { return detailProduits; }
            set { detailProduits = value;
            this.OnPropertyChanged("DetailProduits");
            }
        }

        public CollectionViewSource ViewSource
        {
            get { return viewSource; }
            set { viewSource = value; }
        }

        public ObservableCollection<CheckedListItem<string>> ProductFilters
        {
            get { return productFilters; }
            set { productFilters = value; }
        }

        public int LangueSelectIndex
        {
            get { return langueSelectIndex; }
            set { langueSelectIndex = value;
            this.OnPropertyChanged("LangueSelectIndex");
            }
        }


        public bool IstxtRechercheBtnVisible
        {
            get { return istxtRechercheBtnVisible; }
            set { istxtRechercheBtnVisible = value;
            this.OnPropertyChanged("IstxtRechercheBtnVisible");
            }
        }

        public bool IscmblanguebtnVisible
        {
            get { return iscmblanguebtnVisible; }
            set { iscmblanguebtnVisible = value;
            this.OnPropertyChanged("IscmblanguebtnVisible");
            }
        }

        public string NomProduitRecherche
        {
            get { return nomProduitRecherche; }
            set { nomProduitRecherche = value;
            if (string.IsNullOrEmpty(value))
                IstxtRechercheBtnVisible = false;
            else IstxtRechercheBtnVisible = true;

                     this.OnPropertyChanged("NomProduitRecherche");
            }
        }

        public ObservableCollection<LangueModel> LangueList
        {
            get { return _langueList; }
            set
            {
                _langueList = value;
                this.OnPropertyChanged("LangueList");
            }
        }


        public LangueModel LangueSelected
        {
            get { return _langueSelected; }
            set
            {
                _langueSelected = value;
                if (value != null)
                {
                    chargementProduits(value.Id);

                    if (ProduitSelected != null)
                        ProduitSelected.IdLangue = value.Id;
                    IscmblanguebtnVisible = true;
                }
                else IscmblanguebtnVisible = false;

                this.OnPropertyChanged("LangueSelected");
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

        public List<DetailProductModel> DetailList
        {
            get { return _detailList; }
            set { _detailList = value;
            this.OnPropertyChanged("DetailList");
            }
        }

        public DetailProductModel Detailselected
        {
            get { return _detailselected; }
            set
            {
                _detailselected = value;
                this.OnPropertyChanged("Detailselected");
            }
        }


        public ClientModel Clientselected
        {
            get { return _clientSelected; }
            set
            {
                _clientSelected = value;

            this.OnPropertyChanged("Clientselected");
            }
        }

      

        public List<ClientModel> ClientList
        {
            get { return _clientList; }
            set { _clientList = value;
            this.OnPropertyChanged("ClientList");
            }
        }


        public List<ClientModel> ClientprodList
        {
            get { return _clientprodList; }
            set { _clientprodList = value;
            this.OnPropertyChanged("ClientprodList");
            }
        }


        public ClientModel ClientProdSelected
        {
            get { return _clientProdSelected; }
            set { _clientProdSelected = value ;
            this.OnPropertyChanged("ClientProdSelected");
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
               
                TxtEnable_a = true ;
                TxtEnable_b = true ;
                if (value != null)
                {
                    idOldlangue = value.IdLangue;
                    DetailProductModel detService=new DetailProductModel ();
                    DetailProduits = detService.DETAIL_PRODUIT_GETLISTE(value.IdProduit, 0);
                }

                this.OnPropertyChanged("ProduitSelected");
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
                    //loadDatasModel(value.Id);
                    if (ProduitSelected != null)
                        ProduitSelected.IdLangue = value.Id;
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

        public ObservableCollection<ProduitModel> CacheProduitList
        {
            get { return _cacheProduitList; }
            set { _cacheProduitList = value;
            this.OnPropertyChanged("CacheProduitList");
            }
        }

       


        #endregion

        #region ICOMMAND



        public ICommand ShowRechercheCommand
        {
            get
            {
                if (this.showRechercheCommand == null)
                {
                    this.showRechercheCommand = new RelayCommand(param => this.canShowRecherche(), param => this.canExecuteRecherche());
                }
                return this.showRechercheCommand;
            }
        }

        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(param => this.canSave(), param => this.canExecuteSave());
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
                    this.newCommand = new RelayCommand(param => this.canNew());
                }
                return this.newCommand;
            }


        }

        public ICommand NewDetailCommand
        {
            get
            {
                if (this.newDetailCommand == null)
                {
                    this.newDetailCommand = new RelayCommand(param => this.canNewDetail());
                }
                return this.newDetailCommand;
            }


        }

        public ICommand DeleteDetailCommand
        {
            get
            {
                if (this.deleteDetailCommand == null)
                {
                    this.deleteDetailCommand = new RelayCommand(param => this.canDeleteDetail(), param => this.canExecuteDeleteDetail());
                }
                return this.deleteDetailCommand;
            }


        }

        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(param => this.canDelete(), param => this.canExecuteDelete());
                }
                return this.deleteCommand;
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
        //

        public ICommand SavedetailCommand
        {
            get
            {
                if (this.savedetailCommand == null)
                {
                    this.savedetailCommand = new RelayCommand(param => this.canSaveDet(), param => this.canExecuteDetail());
                }
                return this.savedetailCommand;
            }


        }

        //showDevisesCommand

        public RelayCommand ShowdetailCommand
        {
            get
            {
                if (this.showdetailCommand == null)
                {
                    this.showdetailCommand = new RelayCommand(param => this.canShowDetail(param));
                }
                return this.showdetailCommand;
            }

        }

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

        public RelayCommand ShowfactureInfoCommand
        {
            get
            {
                if (this.showfactureInfoCommand == null)
                {
                    this.showfactureInfoCommand = new RelayCommand(param => this.canShowFactureinfo());
                }
                return this.showfactureInfoCommand;
            }

        }

        //
        #endregion

        #region METHODS

        #region Region Loading
        
    
        void loadRight()
        {
            if ( CurrentDroit.Ecriture ||  CurrentDroit.Proprietaire)
            BtnDeleteVisible = true;

            //if (CurrentDroit != null)
            //{
            //    if (CurrentDroit.Super)
            //    {

            //        BtnDeleteVisible = true;
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

        void chargementProduits(int id)
        {
            try
            {

                 
           
                    IsBusy = true;
                    ProgressBarVisibility = true;
                    if (!GlobalDatas.IdDataRefArchiveDatas)
                        ProduitList = _produitService.Produit_SELECTBY_ID_Language(id, societeCourante.IdSociete);
                else
                        ProduitList = _produitService.Produit_SELECTBY_Language_Archive(id, societeCourante.IdSociete);
              
                        IsBusy = false;
                        ProgressBarVisibility = false;
               

                      
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.ViewModel.Message = ex.Message.ToString();
                view.ShowDialog();
                IsBusy = false;
                ProgressBarVisibility = false;
            }
        }

        void chargementProduits(int id,bool isUpdate)
        {
            try
            {

                if (societeCourante != null)
                {
                    IsBusy = true;
                    ProgressBarVisibility = true;

                    ProduitList = _produitService.Produit_SELECTBY_ID_Language(id, societeCourante.IdSociete);
                    CacheDatas.ui_ProduitProduits = ProduitList;
                    IsBusy = false;
                    ProgressBarVisibility = false;
                }

                


            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.ViewModel.Message = ex.Message.ToString();
                view.ShowDialog();
                IsBusy = false;
                ProgressBarVisibility = false;
            }
        }


        void loadDatas()
        {
            try
            {
                if (LanguageList == null)
                {
                    LanguageList = _language.LANGUE_SELECT(0);
                    LangueList = LanguageList;
                    List<LangueModel> liste=LangueList.ToList ();

                    string langue = GlobalDatas.dataBasparameter.DefaulLanguage;
                    if (!string.IsNullOrEmpty(langue))
                    {
                        langue = langue.Substring(0, 2);
                        LangueSelectIndex = liste.FindIndex(l => l.Shortname.ToLower().Contains(langue.ToLower ()));
                    }
                    else
                        LangueSelectIndex = 0;
                    
                }
             
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "ERREUR CHARGEMENT LANGUE Vues-->Frm Produit";
                view.ViewModel.Message = ex.Message .ToString();
                view.ShowDialog();
                Utils.logUserActions(string.Format("<--UI Produits-- erreure lors du chargement des produits " ), "");
            }

          
        }


      


        


        #endregion


        private void canShowDetail(object param)
        {
            if (  CurrentDroit.Edition || CurrentDroit.Developpeur  )
            {
                DeataiProduit vf = new DeataiProduit(ProduitSelected);
                vf.Owner = localwindow;
                vf.ShowDialog();
            }
            else
            {
                MessageBox.Show("Pas assez de privileges pour editer ce produit");
            }
        }

        void LoadDetailProduct()
        {

        }

        private void canClose()
        {
            _injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);
        }

        private void canNewDetail()
        {
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur )
            {
                _produitSelected = new ProduitModel();
                ProduitSelected = _produitSelected;
                LangueSelected = null;
                TxtEnable_a = true;
                LangueSelectIndex = -1;
                //TxtEnable_b = true ;
            }
        }
        private void canNew()
        {

            ProduitEditModale view = new ProduitEditModale(localwindow);
            ProduitSelected = new ProduitModel(); ;
            DetailProduits = null;
            view.Owner = localwindow;
            view.DataContext = this;
            view.ShowDialog();
        }

        private void canSave()
        {
            try
            {
                IsBusy = true;

                if (ProduitSelected.IdProduit == 0)
                {
                    if (GlobalDatas.IdDataRefArchiveDatas)
                    {
                        MessageBox.Show("Vous êtes dans la base d'archives et ne pouvez créer ");
                        return;
                    }

                    if (LangueSelected != null)
                    {
                        ProduitSelected.IdLangue = LangueSelected.Id;
                        ProduitSelected.ModeFacturation = 0;
                        if (societeCourante != null)
                            ProduitSelected.IdSite = societeCourante.IdSociete;
                       // ProduitSelected.CompteOhada = "101";
                        ProduitSelected.PrixUnitaire = 0;
                        _produitService.Produit_ADD(ProduitSelected);

                        Utils.logUserActions(string.Format("<--UI Produits-- produit {0} créer par {1} ",ProduitSelected.Libelle ,UserConnected.Nom ), "");
                      
                        //for (int i = 0; i < 50; i += 5)
                        //    Thread.Sleep(100);
                        //loadDatas();
                       // chargementProduits(ProduitSelected.IdLangue);
                        ProduitList = _produitService.Produit_SELECTBY_ID_Language(ProduitSelected.IdLangue, societeCourante.IdSociete,true);
                        CacheDatas.ui_ProduitProduits = ProduitList;
                        ProduitSelected = null;
                        LangueSelected = null;
                        TxtEnable_a = false;
                        TxtEnable_b = false;
                    }
                    else
                        MessageBox.Show("Sélectionner Une langue");
                }
                else
                {
                    bool isupdatable = false;
                    // update
                   ObservableCollection< DetailProductModel> detail = _detailService.DETAIL_PRODUIT_GETLISTE(ProduitSelected.IdProduit, 0);

                   if (detail != null && detail .Count >0)
                   {
                       if (idOldlangue != ProduitSelected.IdLangue)
                         isupdatable = false ;
                       else isupdatable = true;
                   }
                   else isupdatable = true;

                   if (isupdatable)
                   {
                       ProduitSelected.PrixUnitaire = 0;

                       if (!GlobalDatas.IdDataRefArchiveDatas)
                       {
                           ProduitSelected.ModeFacturation = 0;
                           _produitService.Produit_ADD(ProduitSelected);
                           // Utils.logUserActions(string.Format("<--UI Produits-- produit {0} Mise jour  par {1} ", ProduitSelected.Libelle, UserConnected.Nom), "");
                           // for (int i = 0; i < 50; i += 5)
                           //  Thread.Sleep(50);
                           //chargementProduits(ProduitSelected.IdLangue);
                           ProduitList = _produitService.Produit_SELECTBY_ID_Language(ProduitSelected.IdLangue, societeCourante.IdSociete, true);
                           CacheDatas.ui_ProduitProduits = ProduitList;
                       }
                       else
                       {
                           ProduitSelected.ModeFacturation = 1;
                           _produitService.Produit_ADD(ProduitSelected);
                           ProduitList = _produitService.Produit_SELECTBY_Language_Archive(ProduitSelected.IdLangue, societeCourante.IdSociete);
                       }
                       ProduitSelected = null;
                       //LangueSelected = null;
                       TxtEnable_a = false;
                       TxtEnable_b = false;
                      // loadDatas();
                   }
                   else MessageBox.Show("Ce Produit Possède Déja Des détails, Sa langue ne peut plus etre modifiée");
                }
                IsBusy = false;
                this.MouseCursor = null;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION DE SAUVEGARDE";
                view.ViewModel.Message ="Probleme Survenu lors de la manipulation de ce produit";
                view.ShowDialog();
                Utils.logUserActions(string.Format("<--UI Produits--Erreure lors de la manipulation du produit {0} créer par {1} : erreure {2}", ProduitSelected.Libelle, UserConnected.Nom,ex.Message ), "");
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecuteSave()
        {
            bool values = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (ProduitSelected != null)
                    values = true;
            }

            return values;
        }
        //ProduitActive
        private void canDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION DE SUPPRESSION PRODUIT";
            messageBox.ViewModel.Message = "Voulez Vous Supprimer ce produit?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {

                    _produitService.Produit_DELETE(ProduitActive.IdProduit);
                    //chargementProduits(ProduitSelected.IdLangue);
                    Utils.logUserActions(string.Format("<--UI Produits-- Suppression du produit {0}  par {1} ", ProduitActive.Libelle, UserConnected.Nom), "");
                    ProduitList = _produitService.Produit_SELECTBY_ID_Language(ProduitActive.IdLangue, societeCourante.IdSociete);
                    CacheDatas.ui_ProduitProduits = ProduitList;
                  
                    ProduitActive = null;
                    //TxtEnable_a = false;
                   // TxtEnable_b = false;
                    //loadDatas();
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "INFORMATION DE SUPPRESSION";
                    if (ex.Message.ToLower().Contains("fk_t_ligne_facture__t_detail_produit"))
                        view.ViewModel.Message="Impossible de supprimer Ce Produit \n il est toujours Réference à une ligne de produit";
                    else if (ex.Message.ToLower().Contains("fk_t_detail_produit__t_produit"))
                        view.ViewModel.Message = "Impossible de supprimer Ce Produit \n il est toujours Réferencé au moins à un client";
                    else 
                        view.ViewModel.Message = ex.Message;
                    Utils.logUserActions(string.Format("<--UI Produits--erreure lors de la suppression du produit {0} créer par {1} -- {2}", ProduitActive.Libelle, UserConnected.Nom, ex.Message), "");
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }
        }

        bool canExecuteDelete()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (ProduitActive != null)
                    if (ProduitActive.IdProduit > 0)
                        values = true;
            }

            return values;



        }



        private void canDeleteDetail()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION DE SUPPRESSION PRODUIT";
            messageBox.ViewModel.Message = "Voulez Vous Supprimer ce produit?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {

                    _produitService.Produit_DELETE(ProduitSelected.IdProduit);
                    //chargementProduits(ProduitSelected.IdLangue);
                    ProduitList = _produitService.Produit_SELECTBY_ID_Language(ProduitSelected.IdLangue, societeCourante.IdSociete);
                    CacheDatas.ui_ProduitProduits = ProduitList;
                    Utils.logUserActions(string.Format("<--UI Produits-- Suppression du produit {0}  par {1} ", ProduitSelected.Libelle, UserConnected.Nom), "");
                    ProduitSelected = null;
                    TxtEnable_a = false;
                    // TxtEnable_b = false;
                    //loadDatas();
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "INFORMATION DE SUPPRESSION";
                    if (ex.Message.ToLower().Contains("fk_t_ligne_facture__t_detail_produit"))
                        view.ViewModel.Message = "Impossible de supprimer Ce Produit \n il est toujours Réference à une ligne de produit";
                    else if (ex.Message.ToLower().Contains("fk_t_detail_produit__t_produit"))
                        view.ViewModel.Message = "Impossible de supprimer Ce Produit \n il est toujours Réferencé au moins à un client";
                    else
                        view.ViewModel.Message = ex.Message;
                    Utils.logUserActions(string.Format("<--UI Produits--erreure lors de la suppression du produit {0} créer par {1} -- {2}", ProduitSelected.Libelle, UserConnected.Nom, ex.Message), "");
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }
        }
        bool canExecuteDeleteDetail()
        {
            bool values = false;
            if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (ProduitSelected != null)
                    if (_produitSelected.IdProduit > 0)
                        values = true;
            }

            return values;



        }



        private void canShowDevise()
        {
            WTaxeDevises vf = _container.Resolve<WTaxeDevises>();
            vf.Owner = localwindow;
            vf.ShowDialog();
        }

        private void canShowFactureinfo()
        {
            FactureInformation vf = _container.Resolve<FactureInformation>();
            vf.Owner = localwindow;
            vf.ShowDialog();
        }

        private void canSaveDet()
        {

        }

        bool canExecuteDetail()
        {
            return true;
        }

        #region IMPORT/Export
         //SpreadsheetInfo.SetLicense((string)Session[Common.SESSION_GEMBOX]);
          string tiTle = string.Empty;
     //       ExcelWorksheet ws = null;
     //       object[] ENTETE = null;
     //       string sheetTitle = string.Empty;

     //       ExcelFile ef = new ExcelFile();

     //       //Report Header
     //       tiTle = "Delivery not Confirmed";
     //       ENTETE = new object[10] { "Branch", "Customer", "Delivery Ticket#", "Delivery Ticket Creation Date","Box ID", "Delivery Date",
     //                                                 "Place of Delivery", "Mode of Transport", "Delivery Tickets Comments", "Warehouse Location"  };

     //       ws = ef.Worksheets.Add(tiTle);
     //for (int i = 0; i < ENTETE.Length; i++)
     //           {
     //               ws.Columns[i].AutoFit(1, ws.Rows[i + 2], ws.Rows[ws.Rows.Count - 1]);
     //               var newWidth = ws.Columns[i].Width;

     //               if (i == 0 && newWidth < (20 * 256))
     //                   ws.Columns[0].Width = 20 * 256;
     //               if (i == 1 && newWidth < (20 * 256))
     //                   ws.Columns[1].Width = 20 * 256;

     //               //Specifically
     //               ws.Columns[2].Width = 20 * 256;
     //               ws.Columns[3].Width = 15 * 256;
     //               ws.Columns[4].Width = 15 * 256;
     //               ws.Columns[5].Width = 15 * 256;
     //               if (i == 6 && newWidth < (17 * 256))
     //                   ws.Columns[6].Width = 17 * 256;
     //               if (i == 7 && newWidth < (17 * 256))
     //                   ws.Columns[7].Width = 17 * 256;
     //               if (i == 8 && newWidth < (17 * 256))
     //                   ws.Columns[8].Width = 17 * 256;
     //               if (i == 9 && newWidth < (17 * 256))
     //                   ws.Columns[9].Width = 17 * 256;
     //           }
        #endregion
        // recherche

        #region Region recherche
        
      

        private void canShowRecherche()
        {
            try
            {
                if (!string.IsNullOrEmpty(NomProduitRecherche))
                {
                    if(!GlobalDatas.IdDataRefArchiveDatas)
                    ProduitList = _produitService.Produit_SEARCH(societeCourante.IdSociete, NomProduitRecherche);
                    //ProduitList = null;
                    //ObservableCollection<ProduitModel> prods = new ObservableCollection<ProduitModel>();
                    //if (produit != null)
                    //    prods.Add(produit);
                    //ProduitList = prods;

                    //if (ProduitList != null)
                    //{
                    //    foreach (string libelle in ProduitList.Select(w => w.Libelle).Distinct().OrderBy(w => w))
                    //    {
                    //        productFilters.Add(new CheckedListItem<string> { Item = libelle, IsChecked = true });
                    //    }
                    //    ViewSource.Source = ProduitList;
                    //}
                }
                else
                {
                    if (CacheDatas.ui_ProduitProduits !=null )
                   ProduitList= CacheDatas.ui_ProduitProduits ;
                }

                   // CacheDatas.ui_ProduitProduits = ProduitList;

                                //Create a list of filters
               


            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.ViewModel.Message = ex.Message.ToString();
                view.ShowDialog();
            }

        }

        bool canExecuteRecherche()
        {
            return true;
        }

        #endregion



        #endregion


        #region FILTER


        void filter(string values)
        {
            //if (ProduitList != null || ProduitList.Count > 0)
            //{
            //    DataTable newTable = new DataTable();
            //    DataColumn col1 = new DataColumn("IdProduit", typeof(Int32));
            //    DataColumn col2 = new DataColumn("libelle", typeof(string));
            //    DataColumn col4 = new DataColumn("idLangue", typeof(Int32 ));
            //    DataColumn col5 = new DataColumn("idSite", typeof(Int32 ));
            //    DataColumn col6 = new DataColumn("prixUnitaire", typeof(decimal ));
            //    DataColumn col7 = new DataColumn("compteOhada", typeof(string));
               
            //    newTable.Columns.Add(col1);
            //    newTable.Columns.Add(col2);
            //    newTable.Columns.Add(col4);
            //    newTable.Columns.Add(col5);
            //    newTable.Columns.Add(col6);
            //    newTable.Columns.Add(col7);
               

            //    DataRow row = null;

            //    foreach (ProduitModel sm in CacheProduitList)
            //    {
            //        row = newTable.NewRow();
            //        row[0] = sm.IdProduit ;
            //        row[1] = sm.Libelle ;
            //        row[2] = sm.IdLangue ;
            //        row[3] = sm.IdSite ;
            //        row[4] = sm.PrixUnitaire ;
            //        row[5] = "";
                   

            //        newTable.Rows.Add(row);

            //    }

            //    DataRow[] nlignes = newTable.Select(string.Format("nom like '{0}%'", values.Trim()));
            //    DataTable filterDatatable = newTable.Clone();
            //    foreach (DataRow rows in nlignes)
            //        filterDatatable.ImportRow(rows);

            //    UtilisateurModel fm;
            //    ObservableCollection<UtilisateurModel> newliste = new ObservableCollection<UtilisateurModel>();

            //    foreach (DataRow r in filterDatatable.Rows)
            //    {
            //        fm = new UtilisateurModel()
            //        {
            //            Id = Int32.Parse(r[0].ToString()),
            //            Nom = r[1].ToString(),
            //            Prenom = r[2].ToString(),
            //            Fonction = r[3].ToString(),
            //            Loggin = r[4].ToString(),
            //            Password = r[5].ToString(),
            //            Profile = CacheProduitList.First(f => f.IdProfile == int.Parse(r[7].ToString())).Profile,
            //            IdProfile = int.Parse(r[7].ToString()),
            //        };
            //        newliste.Add(fm);
            //    }
            //    Users = newliste;

            //}
            //else
            //{
            //    loadDatas();

            //}
        }

        #endregion
    }
}
