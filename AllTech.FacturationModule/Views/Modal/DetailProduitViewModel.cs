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

namespace AllTech.FacturationModule.Views.Modal
{
    public class DetailProduitViewModel : ViewModelBase
    {


        #region FIELDS
        
       
        private readonly IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
        private readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand closeCommand;
        private RelayCommand annulerExploitCommand;

        private RelayCommand annulerProrataCommand;
        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;

        ObservableCollection<DetailProductModel> _detailProduitlist=new ObservableCollection<DetailProductModel> ();
        ObservableCollection<DetailProductModel> _cacheDetailProduitlist;
         DetailProductModel _detailProduitSelect;
         DetailProductModel detailService;
         DetailProductModel detailExiste;
         ProduitModel produitselected;

         ObservableCollection<ClientModel> _clientList;
         ClientModel clientService;
         ClientModel _clientSelected;
         SocieteModel societeCourante;
         ExploitationFactureModel exploitationservice;
         ExploitationFactureModel _exploitationSelected;
       
         ObservableCollection<ExploitationFactureModel> _exploitationList;
         ObservableCollection<ExploitationFactureModel> _cacheExploitationList;

         bool checkAppliqueExploit;

       
         bool txtEnable_a;
         bool txtEnable_taxes;
         bool txtEnable_prorata;
         bool txtEnable_label;
         public  bool isDoubleclick;
      
         bool isproataSelected;
         bool isExonereSelected;
         string labelExonere;
         float  quantiteChoisie;
         int cmbExploitationIndex;
         bool isSpecialFacture;
         bool isload;

         UtilisateurModel userConnected;
         DroitModel _currentDroit;

        #endregion
         Window localwindow;

         public DetailProduitViewModel( ProduitModel produit, Window window)
        {
            TxtEnable_a = false;
            produitselected = produit;
            ProgressBarVisibility = false;
            clientService = new ClientModel();
            detailService = new DetailProductModel();
            societeCourante = GlobalDatas.DefaultCompany;
            localwindow = window;
            UserConnected = GlobalDatas.currentUser;
            //CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("produits")) ?? new DroitModel();
            if (CacheDatas.ui_currentdroitProduitInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("produits")) ?? new DroitModel();
                CacheDatas.ui_currentdroitProduitInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitProduitInterface;

            exploitationservice = new ExploitationFactureModel();
            //_injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
            loadDatas();
           // Utils.logUserActions(string.Format("<-- UI Détail Produit -- par : {0}",  UserConnected.Loggin), "");

             _detailProduitSelect=new DetailProductModel ();
             DetailProduitSelect = _detailProduitSelect;
             QuantiteChoisie = 0;
        }

        #region PROPERTIES

        #region ENABLE TEXTFIELD

        public bool TxtEnable_prorata
        {
            get { return txtEnable_prorata; }
            set { txtEnable_prorata = value;
            this.OnPropertyChanged("TxtEnable_prorata");
            }
        }


        public bool TxtEnable_a
        {
            get { return txtEnable_a; }
            set
            {
                txtEnable_a = value;
                this.OnPropertyChanged("TxtEnable_a");
            }
        }

        public bool TxtEnable_taxes
        {
            get { return txtEnable_taxes; }
            set { txtEnable_taxes = value;
            this.OnPropertyChanged("TxtEnable_taxes");
            }
        }

        public bool TxtEnable_label
        {
            get { return txtEnable_label; }
            set { txtEnable_label = value;
            this.OnPropertyChanged("TxtEnable_label");
            }
        }
       
        #endregion

       public  bool isteste;

        public bool IsSpecialFacture
        {
            get { return isSpecialFacture; }
            set { 
                isSpecialFacture = value;
            if (!value)
            {
                if (!isteste)
                {
                    if (DetailProduitSelect != null)
                    {
                        if (DetailProduitSelect.IdDetail > 0)
                        {
                            if (detailService.IS_DETAIL_EXIST_FACTURE(societeCourante.IdSociete, DetailProduitSelect.IdDetail))
                            {
                                 StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                messageBox.Owner = Application.Current.MainWindow;
                                messageBox.Title = "MODIFICATION MODE FACTURATION";
                                messageBox.ViewModel.Message = "Ce Produit a deja fait l'objet dune facturation \n Voulez vous modier quand même ?";
                                if (messageBox.ShowDialog().Value == true)
                                {
                                    isteste = true;
                                   // IsSpecialFacture = true;
                                    DetailProduitSelect.Specialfact = false;
                                }
                                else
                                {


                                    isteste = true;
                                    IsSpecialFacture = true;
                                }
                            }
                            else
                                DetailProduitSelect.Specialfact = value;
                        }else
                            DetailProduitSelect.Specialfact = value;


                    }
                }

            }else 
            DetailProduitSelect.Specialfact = value;

            this.OnPropertyChanged("IsSpecialFacture");
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

     

        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set
            {
                userConnected = value;
                OnPropertyChanged("UserConnected");
            }
        }


        public bool CheckAppliqueExploit
        {
            get { return checkAppliqueExploit; }
            set { checkAppliqueExploit = value;
            this.OnPropertyChanged("CheckAppliqueExploit");
            }
        }

        public ObservableCollection<ExploitationFactureModel> CacheExploitationList
        {
            get { return _cacheExploitationList; }
            set { _cacheExploitationList = value;
            this.OnPropertyChanged("CacheExploitationList");
            }
        }

     


        public ExploitationFactureModel ExploitationSelected
        {
            get { return _exploitationSelected; }
            set { _exploitationSelected = value;

            if (value != null)
            {
                if (!isDoubleclick)
                {
                   // if (Produitselected != null)
                        //if (Clientselected != null)
                            //if (DetailProduitlist.ToList().Exists(p => p.IdExploitation == value.IdExploitation && p.IdProduit == Produitselected.IdProduit && p.IdClient == Clientselected.IdClient))
                            //{
                            //    MessageBox.Show(" Cette exploitation est déja associer à un produit de ce client");
                            //    ExploitationSelected = null;
                            //    //CmbExploitationIndex = -1;
                            //    ExploitationList = null;
                            //    ExploitationList = CacheExploitationList;
                            //}
                            
                             
                }
            }
            this.OnPropertyChanged("ExploitationSelected");
            }
        }

        public ObservableCollection<ExploitationFactureModel> ExploitationList
        {
            get { return _exploitationList; }
            set { _exploitationList = value;
            this.OnPropertyChanged("ExploitationList");
            }
        }

        public float  QuantiteChoisie
        {
            get { return quantiteChoisie; }
            set { quantiteChoisie = value;
           
            this.OnPropertyChanged("QuantiteChoisie");
            }
        }

        public string LabelExonere
        {
            get { return labelExonere; }
            set { labelExonere ="Client : "+ value;
            this.OnPropertyChanged("LabelExonere");
            }
        }

        public DetailProductModel DetailExiste
        {
            get { return detailExiste; }
            set { detailExiste = value;
            this.OnPropertyChanged("DetailExiste");
            }
        }

        bool valide = false;
        bool toujoursvalide = false;

        public bool IsExonereSelected
        {
            get { return isExonereSelected; }
            set { isExonereSelected = value;
            if (value)
            {
                if (Clientselected != null && Clientselected.Exonerere != null)
                {
                    //uniquement pour client partiel

                    if (Clientselected.Exonerere.CourtDesc == "part")
                    {
                        if (IsproataSelected)
                        {
                            if (value)
                            {
                                if (toujoursvalide)
                                {
                                    IsExonereSelected = false;
                                    DetailProduitSelect.Exonerer = false;
                                    toujoursvalide = true;
                                }
                            }
                            else
                            {


                                // si  produit exonere et prorata , impossibe de decocher exonere ds la bd
                                if (DetailProduitSelect != null)
                                {

                                    if (DetailProduitSelect.IdDetail > 0)
                                    {
                                        if (DetailProduitSelect.Isprorata && DetailProduitSelect.Exonerer)
                                        {
                                            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                            messageBox.Owner =localwindow;
                                            messageBox.Title = "MESSAGE INFORMATION ";
                                            messageBox.ViewModel.Message = "Le prorata est déja appliqué à ce produit exonéré\n cette action annulera le prorata aussi ?";
                                            if (messageBox.ShowDialog().Value == true)
                                            {
                                                DetailProduitSelect.Isprorata = false;
                                                DetailProduitSelect.Exonerer = false;
                                                IsproataSelected = false;
                                            }
                                            else
                                            {
                                                IsExonereSelected = true;
                                                toujoursvalide = false;
                                            }
                                        }
                                    }
                                    else
                                    {
                                        if (!valide)
                                        {
                                            MessageBox.Show("Le prorata  est déja associer à ce produit \n annuler d'abord le prorata ");
                                            valide = true;
                                            IsExonereSelected = false;

                                        }
                                    }
                                }

                            }
                        }
                        else
                        {
                            DetailProduitSelect.Exonerer = value;
                        }
                    }
                }
            }

            this.OnPropertyChanged("IsExonereSelected");
            }
        }


        public bool IsproataSelected
        {
            get { return isproataSelected; }
            set { isproataSelected = value;
            if (value)
            {
                if (DetailProduitSelect != null)
                {
                    if (value)
                    {
                        if (Clientselected != null && Clientselected.Exonerere !=null )
                        {
                            if (Clientselected.Exonerere.CourtDesc== "part")
                                //uniquement pour client partiel
                              
                                
                                if (DetailProduitSelect.Exonerer)
                                {
                                    DetailProduitSelect.Isprorata = value;

                                }
                                else
                                {
                                    DetailProduitSelect.Isprorata = false;
                                    MessageBox.Show("Le prorata ne peut pas être appliqué\n à ce produit  non exonéré");
                                    IsproataSelected = false;
                                }


                        }

                    }
                        

                }
            }
            else
            {
                if (DetailProduitSelect != null )
                {
                    if (DetailProduitSelect.IdDetail == 0)
                    {
                        DetailProduitSelect.Isprorata = false;
                        //DetailProduitSelect.Exonerer = false;
                    }
                    else DetailProduitSelect.Isprorata = false;
                }
                valide = false;
            }
            this.OnPropertyChanged("IsproataSelected");
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


        public ObservableCollection<DetailProductModel> DetailProduitlist
        {
            get { return _detailProduitlist; }
            set { _detailProduitlist = value;
            this.OnPropertyChanged("DetailProduitlist");
            }
        }

        public DetailProductModel DetailProduitSelect
        {
            get { return _detailProduitSelect; }
            set { _detailProduitSelect = value;

            TxtEnable_a = true ;
            if (value != null)
            {
                IsproataSelected = value.Isprorata;
                IsExonereSelected = value.Exonerer;
                    TxtEnable_taxes=true ;
                    QuantiteChoisie = value.Quantite;
                    IsSpecialFacture = value.Specialfact;
            }else
                IsSpecialFacture = false ;
       //isteste

            this.OnPropertyChanged("DetailProduitSelect");
            }
        }


        public ProduitModel Produitselected
        {
            get { return produitselected; }
            set { produitselected = value;
              
            this.OnPropertyChanged("Produitselected");
            }
        }

        public ClientModel Clientselected
        {
            get { return _clientSelected; }
            set
            {
                _clientSelected = value;
                if (value != null)
                {
                    LabelExonere = value.Exonerere.Libelle;
                    if (DetailProduitSelect != null)
                        DetailProduitSelect.IdClient = value.IdClient;

                    if (Clientselected.Exonerere.CourtDesc == "non")
                    {
                        // client non exonere : exonere=false,prorata=false
                        TxtEnable_taxes = false;
                        TxtEnable_prorata = false;
                    }
                    else if (Clientselected.Exonerere.CourtDesc == "exo")
                    {
                        string valTeste = string.Empty;
                        // client exonéré exonere=false,prorata=true
                        TxtEnable_taxes = false;
                        TxtEnable_prorata = false;
                        if (value.Porata != null)
                        {
                            valTeste = value.Porata.Taux.Replace('%', ' ');
                            if (!valTeste.Contains(".") || !valTeste.Contains(","))
                            {
                                if (double.Parse(valTeste) == 0)
                                {
                                    TxtEnable_prorata = false;
                                    IsproataSelected = false;
                                }
                                else
                                {
                                    TxtEnable_prorata = true;
                                    IsproataSelected = true;
                                }



                            }
                        }



                    }
                    else
                    {
                        string valTeste = string.Empty;

                        valTeste = value.Porata.Taux.Replace('%', ' ');
                        if (!valTeste.Contains(".") || !valTeste.Contains(","))
                        {
                            if (double.Parse(valTeste) == 0)
                            {
                                TxtEnable_prorata = false;
                                IsproataSelected = false;
                            }
                            else
                            {
                                TxtEnable_prorata = true;
                                //IsproataSelected = true;
                            }




                        }

                        TxtEnable_taxes = true;
                        //TxtEnable_prorata = true ;
                    }

                    try
                    {

                        ExploitationList = exploitationservice.EXPLOITATION_FACTURE_CLIENT_LIST(societeCourante.IdSociete, value.IdClient);
                        CacheExploitationList = ExploitationList;
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner =localwindow;
                        view.Title = "INFORMATION DETAIL PRODUI CHARGEMENT LIST EXPLOIATION";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                    }

                }

                this.OnPropertyChanged("Clientselected");
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

        public ObservableCollection<DetailProductModel> CacheDetailProduitlist
        {
            get { return _cacheDetailProduitlist; }
            set { _cacheDetailProduitlist = value;
            this.OnPropertyChanged("CacheDetailProduitlist");
            }
        }
        #endregion

        #region ICOMMAND


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
        public RelayCommand AnnulerExploitCommand
        {
            get
            {
                if (this.annulerExploitCommand == null)
                {
                    this.annulerExploitCommand = new RelayCommand(param => this.canAnnulerExploiClose(), param => this.canExecuteAnnulerExploit());
                }
                return this.annulerExploitCommand;
            }

        }

        //

       

        #endregion

        #region METHODS

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
                    if (Produitselected != null)
                        DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                    CacheDetailProduitlist = DetailProduitlist;


                    ObservableCollection<ClientModel> listes = clientService.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                    var query = from nl in listes.AsEnumerable()
                                where nl.IdLangue == Produitselected.IdLangue
                                select nl;
                    ObservableCollection<ClientModel> lst = new ObservableCollection<ClientModel>();
                    if (query != null)
                    {
                        foreach (ClientModel cli in query)
                        {
                            lst.Add(cli );
                        }
                    }
                    ClientList = lst;
                 
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
                    view.Title = " INFORMATION ERREUR CHARGEMENT  PRODUITS";
                    view.ViewModel.Message ="Un Problème est survenu lors du chargement des produits";
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    Utils.logUserActions(string.Format("<-- UI  Détail Produit ERREUR CHARGEMET  -- par : {0}", UserConnected.Loggin), "");
                    Utils.logUserActions(string.Format("<-- UI Détail Produit--Message : {0}", args.Result.ToString()), "");
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

        #region EXPLOITATION

        void canAnnulerExploiClose()
        {
             StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner =localwindow;
            messageBox.Title = "MESSAGE ANNULATION EXPLOITATION";
            messageBox.ViewModel.Message = "Voulez vous annuler l'exploitation à ce produit?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    detailService.DETAIL_PRODUIT_ANNULE_EXPLOITATION(DetailProduitSelect.IdDetail, DetailProduitSelect.IdExploitation, societeCourante.IdSociete);

                    for (int i = 0; i < 20; i += 5)
                        Thread.Sleep(100);
                    DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                    DetailProduitSelect = null;
                    ClientList = null;
                    IsExonereSelected = false;
                    IsproataSelected = false;
                    ExploitationList = null;
                    ExploitationSelected = null;
                    ExploitationList = CacheExploitationList;
                    CheckAppliqueExploit = false;

                    ObservableCollection<ClientModel> listes = clientService.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                    var query = from nl in listes.AsEnumerable()
                                where nl.IdLangue == Produitselected.IdLangue
                                select nl;
                    ObservableCollection<ClientModel> lst = new ObservableCollection<ClientModel>();
                    if (query != null)
                    {

                        foreach (ClientModel cli in query)
                        {
                            lst.Add(cli);
                        }

                    }
                    ClientList = lst;
                    //ClientList = clientService.CLIENT_GETLISTE(societeCourante.IdSociete);
                    Clientselected = null;
                    TxtEnable_a = false;
                    QuantiteChoisie = 0;
                    Utils.logUserActions(string.Format("<-- UI  Détail ProduitAnnulation de l'exploitation du produit{0}  -- par : {1}",Produitselected.Libelle , UserConnected.Loggin), "");
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                   
                    view.Title = "INFORMATION ANNULATION  EXPLOITATION";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    Utils.logUserActions(string.Format("<-- UI  Détail Produit ERREUR Annulation exploitation  -- par : {0}", UserConnected.Loggin), "");
                    Utils.logUserActions(string.Format("<-- UI Détail Produit--Message : {0}", ex.Message ), "");

                }
            }

        }

        bool canExecuteAnnulerExploit()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression )
            {
                if (DetailProduitSelect != null)
                {
                    if (DetailProduitSelect.IdExploitation > 0)
                        values = true;
                }
            }
            return values;
        }


        #endregion

        private void canNewProduit()
        {
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur  )
            {
                _detailProduitSelect = new DetailProductModel();
                DetailProduitSelect = _detailProduitSelect;
                TxtEnable_a = true;
                ClientList = null;
                Clientselected = null;
                IsExonereSelected = false;
                IsproataSelected = false;
                ExploitationList = null;

                ObservableCollection<ClientModel> listes = clientService.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                var query = from nl in listes.AsEnumerable()
                            where nl.IdLangue == Produitselected.IdLangue
                            select nl;
                ObservableCollection<ClientModel> lst = new ObservableCollection<ClientModel>();
                if (query != null)
                {
                    foreach (ClientModel cli in query)
                    {
                        lst.Add(cli);
                    }
                }
                ClientList = lst;
            }
            
        }



        private void canSaveProduit()
        {
            bool isValuesok = false;
            bool isValueSpecialFact = false;
            bool isqtyevaluesOk = false;
            bool isPuInf = false;
            bool isExisteQteclient = false;
            bool isExisteQUniquePlage = false;

            try
            {
                IsBusy = true;
                if ((DetailProduitSelect.IdDetail == 0))
                {
                    // nouveau plage

                    List<DetailProductModel> detailListeSort = GetProduitFiltreByproduitByClient(Clientselected.IdClient, Produitselected.IdProduit);

                    if (Clientselected != null)
                    {
                        if (Clientselected.Exonerere != null)
                        {
                            if (Clientselected.Exonerere.CourtDesc == "exo")
                            {
                                DetailProduitSelect.Exonerer = true;
                                DetailProduitSelect.Isprorata = IsproataSelected;
                                isValuesok = true;
                                isqtyevaluesOk = true;
                                // isExisteQteclient = false;
                            }

                            if (Clientselected.Exonerere.CourtDesc == "non")
                            {
                                DetailProduitSelect.Exonerer = false;
                                DetailProduitSelect.Isprorata = false;
                                isValuesok = true;
                                isqtyevaluesOk = true;
                                //isExisteQteclient = false;
                            }
                        }

                        DetailProduitSelect.IdProduit = Produitselected.IdProduit;

                      

                        if (detailListeSort.Count > 0)
                        {
                            // verification quantité existante

                            if (detailListeSort.Exists(d => d.Quantite == QuantiteChoisie && d.Prixunitaire ==1 ) ||
                                detailListeSort.Exists(d => d.Prixunitaire == 1))
                            {
                                isExisteQUniquePlage = true;
                                DetailProduitlist.FirstOrDefault(d => d.Quantite == 1 && d.IdClient == Clientselected.IdClient).BackGround = "Red";
                                MessageBox.Show("Ce type de produit de qte=1 et prix unit=1 est déja défini");
                                return;
                            }
                            else
                            {
                                isExisteQUniquePlage = false;
                            }

                            if (!isExisteQUniquePlage)
                            {
                                


                                if (ExploitationSelected != null && ExploitationSelected.IdExploitation > 0)
                                {
                                    //avec explotation

                                    if (detailListeSort.Exists(d => d.Quantite == QuantiteChoisie && d.IdExploitation == ExploitationSelected.IdExploitation))
                                    {
                                        isExisteQteclient = true;
                                        MessageBox.Show("cette Quantité existe déja pour ce Client");
                                        DetailProduitlist.FirstOrDefault(d => d.Quantite == QuantiteChoisie && d.IdClient == Clientselected.IdClient).BackGround = "Red";
                                       
                                          return;
                                    }
                                    else
                                    {
                                        isExisteQteclient = false;
                                    }
                                }
                                else
                                {
                                    if (detailListeSort.Exists(d => d.Quantite == QuantiteChoisie && d.IdExploitation == 0))
                                    {
                                        isExisteQteclient = true;
                                        MessageBox.Show("cette Quantité existe déja pour ce Client");
                                        DetailProduitlist.FirstOrDefault(d => d.Quantite == QuantiteChoisie && d.IdClient == Clientselected.IdClient && d.IdExploitation == 0).BackGround = "Red";
                                        return;
                                    }
                                    else
                                    {
                                        isExisteQteclient = false;
                                    }
                                }

                               
                            }

                          

                        }

                      


                        if (CheckAppliqueExploit)
                            isExisteQteclient = false;

                        if (detailListeSort.Count > 0)
                        {
                            foreach (var det in detailListeSort)
                            {
                                if (det.Specialfact != DetailProduitSelect.Specialfact && det.IdClient == Clientselected.IdClient)
                                {
                                    isValueSpecialFact = false;
                                    break;
                                }
                                else isValueSpecialFact = true;
                            }
                        }
                        else isValueSpecialFact = true;



                        if (isValuesok)
                        {
                            if (isqtyevaluesOk)
                            {
                                if (quantiteChoisie > 0)
                                {
                                    if (!isExisteQUniquePlage)
                                    {
                                     // if (!isExisteQteclient)
                                      //{

                                        if (DetailProduitSelect.Prixunitaire > 0)
                                        {
                                            DetailProduitSelect.Quantite = quantiteChoisie;
                                            DetailProduitSelect.IdSite = societeCourante.IdSociete;
                                            if (ExploitationSelected != null )
                                            {
                                                if (CheckAppliqueExploit)
                                                {
                                                    if (isValueSpecialFact)
                                                    {
                                                        bool istraitmentOk = false;
                                                      
                                                   
                                                        if (detailListeSort.Count > 0)
                                                        {
                                                            
                                                                  
                                                                    DetailProduitSelect.IdExploitation = ExploitationSelected.IdExploitation;
                                                                    detailService.DETAIL_PRODUIT_ADD(DetailProduitSelect);
                                                                    istraitmentOk = true;
                                                               
                                                        }
                                                        else
                                                        {
                                                            DetailProduitSelect.IdExploitation = ExploitationSelected.IdExploitation;
                                                            detailService.DETAIL_PRODUIT_ADD(DetailProduitSelect);
                                                            istraitmentOk = true;
                                                        }
                                                        if (istraitmentOk)
                                                        {

                                                            for (int i = 0; i < 50; i += 5)
                                                                Thread.Sleep(100);
                                                            DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                                                            DetailProduitSelect = null;
                                                            ClientList = null;
                                                            IsExonereSelected = false;
                                                            IsproataSelected = false;
                                                            ExploitationList = null;
                                                            ExploitationSelected = null;
                                                            CheckAppliqueExploit = false;
                                                            ObservableCollection<ClientModel> listes = clientService.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                                                            var query = from nl in listes.AsEnumerable()
                                                                        where nl.IdLangue == Produitselected.IdLangue
                                                                        select nl;
                                                            ObservableCollection<ClientModel> lst = new ObservableCollection<ClientModel>();
                                                            if (query != null)
                                                            {

                                                                foreach (ClientModel cli in query)
                                                                {
                                                                    lst.Add(cli);
                                                                }

                                                            }
                                                            ClientList = lst;
                                                            //ClientList = clientService.CLIENT_GETLISTE(societeCourante.IdSociete);
                                                            Clientselected = null;
                                                            TxtEnable_a = false;
                                                            QuantiteChoisie = 0;
                                                            ExploitationList = CacheExploitationList;
                                                            Utils.logUserActions(string.Format("<-- UI  Détail Produit Ajout d'un nouveau produit{0}  -- par : {1}", Produitselected.Libelle, UserConnected.Loggin), "");
                                                        }
                                                        
                                                    }
                                                    else MessageBox.Show("Le mode de facturation de ce client ne correspond pas \n au mode d'interval existant pour ce client");
                                                }
                                                else MessageBox.Show("Vous avez décider d'appliquer l'exploitation\n Merci de la sélectionner");
                                            }
                                            else
                                            {
                                                // pas dexploitation

                                             
                                                if (isValueSpecialFact)
                                               {
                                                   


                                                   DetailProduitSelect.IdExploitation = 0;
                                                       detailService.DETAIL_PRODUIT_ADD(DetailProduitSelect);
                                                       for (int i = 0; i < 10; i += 5)
                                                           Thread.Sleep(100);
                                                       DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                                                       DetailProduitSelect = null;
                                                       ClientList = null;
                                                       IsExonereSelected = false;
                                                       IsproataSelected = false;
                                                       ExploitationList = null;
                                                       ExploitationSelected = null;
                                                       CheckAppliqueExploit = false;

                                                       ObservableCollection<ClientModel> listes = clientService.CLIENT_GETLISTE(societeCourante.IdSociete, true);
                                                       var query = from nl in listes.AsEnumerable()
                                                                   where nl.IdLangue == Produitselected.IdLangue
                                                                   select nl;
                                                       ObservableCollection<ClientModel> lst = new ObservableCollection<ClientModel>();
                                                       if (query != null)
                                                       {

                                                           foreach (ClientModel cli in query)
                                                           {
                                                               lst.Add(cli);
                                                           }

                                                       }
                                                       ClientList = lst;
                                                       //ClientList = clientService.CLIENT_GETLISTE(societeCourante.IdSociete);
                                                       Clientselected = null;
                                                       TxtEnable_a = false;
                                                       QuantiteChoisie = 0;
                                                       Utils.logUserActions(string.Format("<-- UI  Détail Produit Ajout d'un  nouveau produit{0} sans exploitation  -- par : {1}", Produitselected.Libelle, UserConnected.Loggin), "");
                                                   
                                              }
                                                else MessageBox.Show("Le mode de facturation de ce client ne correspond pas \n au mode d'interval existant pour ce client");
                                            }

                                        }
                                        else
                                            MessageBox.Show("Préciser le montant");
                                   // }
                                   // else
                                       // MessageBox.Show("cette Quantité existe déja pour ce Client");

                                    }
                                    else
                                        MessageBox.Show("Ce type de produit de qte=1 et prix unit=1 est déja défini");
                                }
                                else
                                    MessageBox.Show("Une Quantité [0] n'est  pas autorisé");
                            }
                            else
                                MessageBox.Show("Ce prix unitaire ne doit pas être inférieur à celui existant");
                        }
                        else
                            MessageBox.Show("Le détail de ce Produit ne Correspond pas à celui Existant");

                        IsBusy = false;
                    }
                    else
                        MessageBox.Show("Préciser Le client pour ce produit !");

                }
                else
                {
                    // mise jour dune plage
                    List<DetailProductModel> detailListeSort = GetProduitFiltreByproduitByClient(Clientselected.IdClient, Produitselected.IdProduit);
                    if (Clientselected.Exonerere.CourtDesc == "exo")
                    {
                        DetailProduitSelect.Exonerer = true;
                        DetailProduitSelect.Isprorata = IsproataSelected;
                        isValuesok = true;
                    }

                    if (Clientselected.Exonerere.CourtDesc == "non")
                    {
                        DetailProduitSelect.Exonerer = false;
                        DetailProduitSelect.Isprorata = false;
                        isValuesok = true;
                    }

                   

                    if (detailListeSort.Count > 0)
                    {
                        foreach (var det in detailListeSort)
                        {
                            if (det.Specialfact != DetailProduitSelect.Specialfact && det.IdClient == Clientselected.IdClient)
                            {
                                isValueSpecialFact = false;
                                break;
                            }
                            else isValueSpecialFact = true;
                        }
                    }
                    else isValueSpecialFact = true;




                    if (detailListeSort.Count > 0)
                    {
                        isExisteQUniquePlage = false;

                        if (detailListeSort.Exists(d => d.Quantite == QuantiteChoisie && d.Prixunitaire == 1) ||
                                detailListeSort.Exists(d => d.Prixunitaire == 1))
                        {
                            isExisteQUniquePlage = true;
                            DetailProduitlist.FirstOrDefault(d => d.Quantite == 1 && d.IdClient == Clientselected.IdClient).BackGround = "Red";
                        }
                        else
                        {
                            isExisteQUniquePlage = false;
                        }

                        if (!isExisteQUniquePlage)
                        {

                            if (detailListeSort.Exists(d => d.Quantite == QuantiteChoisie))
                            {
                                isExisteQteclient = true;
                                DetailProduitlist.FirstOrDefault(d => d.Quantite == QuantiteChoisie && d.IdClient == Clientselected.IdClient).BackGround = "Red";
                            }
                            else
                            {
                                isExisteQteclient = false;
                            }
                        }

                    }


                    if (isValuesok)
                    {
                        if (QuantiteChoisie > 0)
                        {
                            //if (!isExisteQteclient)
                            //{
                            if (DetailProduitSelect.Prixunitaire > 0)
                            {
                                if (ExploitationSelected != null)
                                {
                                    if ( CheckAppliqueExploit)
                                    {
                                       // if (isValueSpecialFact)
                                       // {
                                            DetailProduitSelect.IdExploitation = ExploitationSelected.IdExploitation;

                                            DetailProduitSelect.Quantite = QuantiteChoisie;
                                            DetailProduitSelect.IdSite = societeCourante.IdSociete;
                                            detailService.DETAIL_PRODUIT_ADD(DetailProduitSelect);
                                            //for (int i = 0; i < 50; i += 5)
                                            //    Thread.Sleep(50);


                                            DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                                            DetailProduitSelect = null;
                                            IsExonereSelected = false;
                                            IsproataSelected = false;
                                            QuantiteChoisie = 0;
                                            ExploitationList = null;
                                            ExploitationSelected = null;
                                            CheckAppliqueExploit = false;
                                            ExploitationList = CacheExploitationList;


                                       // }
                                       // else MessageBox.Show("Le mode de facturation de ce client ne correspond pas \n au mode de l'interval existant pour ce client");
                                    }
                                    else MessageBox.Show("Vous avez décider d'appliquer l'exploitation\n Merci de sélectionner");
                                }
                                else
                                {
                                  // if (isValueSpecialFact)
                                    //{
                                    DetailProduitSelect.Quantite = QuantiteChoisie;
                                    DetailProduitSelect.IdSite = societeCourante.IdSociete;
                                    DetailProduitSelect.IdExploitation = 0;
                                    detailService.DETAIL_PRODUIT_ADD(DetailProduitSelect);
                                    //for (int i = 0; i < 50; i += 5)
                                       // Thread.Sleep(50);


                                    DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                                    DetailProduitSelect = null;
                                    IsExonereSelected = false;
                                    IsproataSelected = false;
                                    QuantiteChoisie = 0;
                                    ExploitationList = null;
                                    ExploitationSelected = null;
                                    CheckAppliqueExploit = false;
                                    ExploitationList = CacheExploitationList;
                                    Utils.logUserActions(string.Format("<-- UI  Détail Produit modification d'un  produit{0} sans exploitation  -- par : {1}", Produitselected.Libelle , UserConnected.Loggin), "");
                                    // }
                                      //  else MessageBox.Show("Le mode de facturation de ce client ne correspond pas \n au mode d'interval existant pour ce client");

                                }
                            }
                            else
                                MessageBox.Show("Préciser le montant");
                            //}
                            //else
                            //    MessageBox.Show("cette Quantité existe déja pour ce Client");

                        }
                        else
                            MessageBox.Show("Une Quantité [0] n'est  pas autorisé");
                    }
                    else MessageBox.Show("Le détail de ce produit ne correspond pas à celui Existant");

                    IsBusy = false;
                }

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner =localwindow;
                view.Title = "INFORMATION SAUVEGARDE";
                view.ViewModel.Message = "problème survenu lors de la mise jour/ création du produit";
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
                Utils.logUserActions(string.Format("<-- UI Detail produit --D Erreur lors de la mise jour/insertion du produit {0}  interface  par : {1}", Produitselected.Libelle , UserConnected.Loggin), "");
                Utils.logUserActions(string.Format("<-- UI detail produit -- message  : {0}", ex.Message), "");
            }
        }

        bool canExecuteSaveProduit()
        {
            bool values = false;
            if ( CurrentDroit.Ecriture || CurrentDroit.Developpeur )
            {
                if (DetailProduitSelect != null)
                    values = true;
            }
            return values;
        }


        private void canDeleteProduit()
        {
              StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner =localwindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous Supprimer ce Détail ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                   
                           detailService.DETAIL_PRODUIT_DELETE(DetailProduitSelect.IdDetail);

                           DetailProduitSelect = null;
                           DetailProduitlist = detailService.DETAIL_PRODUIT_GETLISTE(Produitselected.IdProduit, 0);
                           DetailProduitSelect = null;
                           IsExonereSelected = false;
                           IsproataSelected = false;
                           TxtEnable_a = false;
                           QuantiteChoisie = 0;

                           ExploitationList = null;
                           ExploitationSelected = null;
                           CheckAppliqueExploit = false;
                           ExploitationList = CacheExploitationList;
                   
                  
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner =localwindow;
                    view.Title = "INFORMATION SUPPRESSION";
                    if (ex.Message.ToLower().Contains("fk_t_ligne_facture_t_detail_produit"))
                        view.ViewModel.Message = "Impossible de supprimer Ce Produit \n il est toujours Réference à une facture";
                    
                    else 
                    view.ViewModel.Message ="Problème lors de la suppression du produit ";
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;

                    Utils.logUserActions(string.Format("<-- UI Detail produit --Erreur lors de la suppression du produit {0}  interface  par : {1}", Produitselected.Libelle, UserConnected.Loggin), "");
                    Utils.logUserActions(string.Format("<-- UI detail produit -- message   : {0}", ex.Message), "");
                }
            }
        }


       

        bool canExecuteDeleteProduit()
        {
            bool values = false;
            if ( CurrentDroit.Suppression || CurrentDroit.Developpeur )
            {
                if (DetailProduitSelect != null)
                    if (DetailProduitSelect.IdDetail >0)
                    values = true;
            }
            return values;

            
        }


        void filter(string values)
        {
            if (DetailProduitlist != null || DetailProduitlist.Count > 0)
            {
                DataTable newTable = new DataTable();
                DataColumn col1 = new DataColumn("id", typeof(Int32));
                DataColumn col2 = new DataColumn("idclient", typeof(Int32));
                DataColumn col4 = new DataColumn("idproduit", typeof(Int32));
                DataColumn col5 = new DataColumn("quantite", typeof(int ));
                DataColumn col6 = new DataColumn("nomproduit", typeof(string));
                DataColumn col7 = new DataColumn("prixUnit", typeof(double ));
                DataColumn col8 = new DataColumn("exonere", typeof(bool ));
                DataColumn col9 = new DataColumn("nomclient", typeof(string));
                DataColumn col10 = new DataColumn("prorata", typeof(bool ));
              
                newTable.Columns.Add(col1);
                newTable.Columns.Add(col2);
                newTable.Columns.Add(col4);
                newTable.Columns.Add(col5);
                newTable.Columns.Add(col6);
                newTable.Columns.Add(col7);
                newTable.Columns.Add(col8);
                newTable.Columns.Add(col9);
                newTable.Columns.Add(col10);
              

                DataRow row = null;

                foreach (DetailProductModel  sm in CacheDetailProduitlist )
                {
                    row = newTable.NewRow();
                    row[0] = sm.IdDetail ;
                    row[1] = sm.IdClient ;
                    row[2] = sm.IdProduit ;
                    row[3] = sm.Quantite ;
                    row[4] = sm.NomProduit ;
                    row[5] = sm.Prixunitaire ;
                    row[6] = sm.Exonerer ;
                    row[7] = sm.Customer.NomClient ;
                    row[8] = sm.Isprorata ;
                    newTable.Rows.Add(row);

                }

                DataRow[] nlignes = newTable.Select(string.Format("nomclient like '{0}%'", values.Trim()));
                DataTable filterDatatable = newTable.Clone();
                foreach (DataRow rows in nlignes)
                    filterDatatable.ImportRow(rows);

                DetailProductModel  fm;
                ObservableCollection<DetailProductModel> newliste = new ObservableCollection<DetailProductModel>();

                foreach (DataRow r in filterDatatable.Rows)
                {
                    fm = new DetailProductModel()
                    {
                         IdDetail   = Int32.Parse(r[0].ToString()),
                         IdClient  = Int32.Parse(r[1].ToString()),
                         IdProduit  =Int32.Parse( r[2].ToString()),
                         Quantite  =Int32.Parse( r[3].ToString()),
                         NomProduit  = r[4].ToString(),
                         Prixunitaire  =decimal.Parse( r[5].ToString()),
                         Exonerer =bool.Parse( r[6].ToString()), Isprorata =bool.Parse( r[8].ToString()),
                      Customer =CacheDetailProduitlist .First (p=>p .IdDetail ==Int32.Parse(r[0].ToString())).Customer 
                    };
                    newliste.Add(fm);
                }
                DetailProduitlist  = newliste;

            }
            else
            {
                DetailProduitlist = CacheDetailProduitlist;
                //loadDatas();

            }
        }
        #endregion

        #region BUINESS METHODS
        List<DetailProductModel> GetProduitFiltreByproduitByClient(Int32 idClient, Int32 iDProduit)
        {
            List<DetailProductModel> liste = null;

            liste = (from dt in DetailProduitlist
                     where dt.IdClient == idClient
                                   && dt.IdProduit == iDProduit
                                   orderby dt.Quantite ascending
                                   select dt).ToList();
            return liste;
        }

        #endregion
    }
}
