using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using Microsoft.Practices.Prism.Regions;
using Microsoft.Practices.Unity;
using AllTech.FrameWork.Services;
using AllTech.FrameWork.Command;
using System.Windows.Input;
using AllTech.FacturationModule.Views;
using System.Windows;
using AllTech.FrameWork.Model;
using System.Collections.ObjectModel;
using System.ComponentModel;
using AllTech.FrameWork.Views;
using AllTech.FrameWork.Global;
using System.Data;
using AllTech.FrameWork.Region;
using AllTech.FacturationModule.Report;

namespace AllTech.FacturationModule.ViewModel
{
    public class FacturationListeViewModel : ViewModelBase
    {

        #region FIELDS
        
     

        public  readonly IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
        public  readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;
        string filtertexteFacture;
        DateTime? filterDateDebut;

        DataTable newTable = null;

        long ligneCourante = 0;
        int maxlignes = 10;
        int totalLignes;
        int ligneDebut;
        int ligneFin;
        int nbreligneCourante = 0;


     
        private RelayCommand newCommand;
        private RelayCommand editCommand;
        private RelayCommand deleteCommand;
        private RelayCommand closeCommand;
        private  RelayCommand lendToCommand;
        private RelayCommand previousCommand;
        private RelayCommand nextCommand;

        private RelayCommand firstCommand;
        private RelayCommand lasstCommand;

        private RelayCommand serchByDateCommand;
      

        private RelayCommand imprimerCommand;
        private RelayCommand imprimerToutCommand;
        private RelayCommand exportCommand;
        private RelayCommand exportALLCommand;
        private RelayCommand validationCommand;
        private RelayCommand sortieCommand;
        private RelayCommand suspensionCommand;
        private RelayCommand nonValalableCommand;

        private RelayCommand selectAllCommand;
        private RelayCommand deselectAllCommand;

       public  FactureModel factureService;
        ObservableCollection<FactureModel> _facturesListe;
        ObservableCollection<FactureModel> _cacheFacturesListe;
        ObservableCollection<FactureModel> _cacheFacturesListeRecherche;
        ObservableCollection<FactureModel> _refreshcacheFacturesListeRecherche;

     

     
        ObservableCollection<FactureModel> _filterFacturesListe;
        ObservableCollection<FactureModel> _filterByCliFacturesListe;
        ObservableCollection<FactureModel> _filterByDateFacturesListe;
        ObservableCollection<FactureModel> listeTeste = new ObservableCollection<FactureModel>();

    
        FactureModel _factureSelected;
        DateTime dateSelected;

    
        LangueModel _langueService;
        LangueModel _langueSelected;

        LibelleModel libelleService;
        LibelleModel _libelleListe;
        bool isEnablePrint;
        bool checkCancelDateFilter;

        bool checkFactureCreation;
        bool checkFactureEncours;
        bool checkFactureValider;
        public   SocieteModel societeCourante;
        List<SocieteModel> societeListe;
        SocieteModel currentSociete;
        SocieteModel societeService;
        StatutModel statutservice;
        ObservableCollection<ClientModel> listeClientFacture;
        ClientModel clientFactireselect;
        ClientModel clientService;

        List<ClientChecked> listeClient;
      

       

        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;

        private bool isFactureCheck;
        private bool filtreEnable;

        bool isNonValide;
        bool isValide;
        bool enbameBtnNonValable;
        bool enbameBtnValide;
        bool enbameBtnSuspendre;
        bool enableBtnSortie;
        bool cmbStevisible;
        int indexSelected;
        int idsociete = 0;
        int nbrepages;
        int numeroPageDebut;
        bool isClientChecked;
        DateTime? dateDebut;

      
        DateTime? dateFin;

        bool islineItemChecked;



        #endregion





        public FacturationListeViewModel(IRegionManager regionManager, IUnityContainer container)
        {
            _regionManager = regionManager;
            _container = container;
            _injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
            ProgressBarVisibility = false;
            factureService = new FactureModel();
            libelleService = new LibelleModel();
            societeService = new SocieteModel();
            statutservice = new StatutModel();
            clientService = new ClientModel();
            societeCourante = GlobalDatas.DefaultCompany;
            idsociete = societeCourante.IdSociete;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("historic"));
            maxlignes = ParametersDatabase.PaginationHtrc !=string .Empty ?int .Parse ( ParametersDatabase.PaginationHtrc):10;
            loadDatas(societeCourante.IdSociete);
            loadWright();
         
        }

        #region PROPERTIES

        #region COMMON

        public bool IslineItemChecked
        {
            get { return islineItemChecked; }
            set { islineItemChecked = value;
            OnPropertyChanged("IslineItemChecked");
            }
        }

        public ClientModel ClientFactireselect
        {
            get { return clientFactireselect; }
            set
            {
                clientFactireselect = value;
                OnPropertyChanged("ClientFactireselect");
            }
        }

        public ObservableCollection<ClientModel> ListeClientFacture
        {
            get { return listeClientFacture; }
            set { listeClientFacture = value;
            OnPropertyChanged("ListeClientFacture");
            }
        }

        public DateTime? DateDebut
        {
            get { return dateDebut; }
            set { dateDebut = value;
            OnPropertyChanged("DateDebut");
            }
        }

        public DateTime? DateFin
        {
            get { return dateFin; }
            set { dateFin = value;
            OnPropertyChanged("DateFin");
            }
        }

        public List<ClientChecked> ListeClient
        {
            get { return listeClient; }
            set { listeClient = value;
            OnPropertyChanged("ListeClient");
            }
        }

        public int Nbrepages
        {
            get { return nbrepages; }
            set { nbrepages = value;
            OnPropertyChanged("Nbrepages");
            }
        }

        public int NumeroPageDebut
        {
            get { return numeroPageDebut; }
            set { numeroPageDebut = value;
            OnPropertyChanged("NumeroPageDebut");
            }
        }

        public int IndexSelected
        {
            get { return indexSelected; }
            set { indexSelected = value;
            OnPropertyChanged("IndexSelected");
            }
        }

        public List<SocieteModel> SocieteListe
        {
            get { return societeListe; }
            set
            {
                societeListe = value;
                OnPropertyChanged("SocieteListe");
            }
        }


        public SocieteModel CurrentSociete
        {
            get { return currentSociete; }
            set
            {
                currentSociete = value;
                if (value != null)
                {
                    idsociete = value.IdSociete;
                    loadDatas(idsociete);
                    societeCourante = value;
                }
                OnPropertyChanged("CurrentSociete");
            }
        }

        public bool CmbStevisible
        {
            get { return cmbStevisible; }
            set { cmbStevisible = value;
            OnPropertyChanged("CmbStevisible");
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
      
        public DateTime DateSelected
        {
            get { return dateSelected; }
            set { dateSelected = value;
            this.OnPropertyChanged("DateSelected");
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

        #endregion

          public bool IsValide
          {
              get { return isValide; }
              set { isValide = value;
              OnPropertyChanged("IsValide");
              }
          }


          public bool EnbameBtnValide
          {
              get { return enbameBtnValide; }
              set { enbameBtnValide = value;
              OnPropertyChanged("EnbameBtnValide");
              }
          }

          public bool EnableBtnSortie
          {
              get { return enableBtnSortie; }
              set { enableBtnSortie = value;
              OnPropertyChanged("EnableBtnSortie");
              }
          }

          public bool EnbameBtnSuspendre
          {
              get { return enbameBtnSuspendre; }
              set { enbameBtnSuspendre = value;
              OnPropertyChanged("EnbameBtnSuspendre");
              }
          }


          public bool EnbameBtnNonValable
          {
              get { return enbameBtnNonValable; }
              set { enbameBtnNonValable = value;
              OnPropertyChanged("IsNonValide");
              }
          }

          public bool IsNonValide
          {
              get { return isNonValide; }
              set { isNonValide = value;
              OnPropertyChanged("IsNonValide");
              }
          }

          public bool FiltreEnable
          {
              get { return filtreEnable; }
              set { filtreEnable = value;
              OnPropertyChanged("FiltreEnable");
              }
          }

          public bool CheckFactureCreation
          {
              get { return checkFactureCreation; }
              set { checkFactureCreation = value;
              if (value)
              {

              }
              OnPropertyChanged("CheckFactureCreation");
              }
          }

          public bool IsFactureCheck
          {
              get { return isFactureCheck; }
              set
              {
                  isFactureCheck = value;
                  this.OnPropertyChanged("IsFactureCheck");
              }
          }

          public bool CheckFactureEncours
          {
              get { return checkFactureEncours; }
              set { checkFactureEncours = value;
              OnPropertyChanged("CheckFactureEncours");
              }
          }

          public bool CheckFactureValider
          {
              get { return checkFactureValider; }
              set { checkFactureValider = value;
              OnPropertyChanged("CheckFactureValider");
              }
          }

       

          public ObservableCollection<FactureModel> CacheFacturesListeRecherche
          {
              get { return _cacheFacturesListeRecherche; }
              set { _cacheFacturesListeRecherche = value;
              OnPropertyChanged("CacheFacturesListeRecherche");
              }
          }


          public int TotalLignes
          {
              get { return totalLignes; }
              set { totalLignes = value;
              OnPropertyChanged("TotalLignes");
              }
          }

          public int LigneDebut
          {
              get { return ligneDebut; }
              set { ligneDebut = value;
              OnPropertyChanged("LigneDebut");
              }
          }

          public int LigneFin
          {
              get { return ligneFin; }
              set { ligneFin = value;
              OnPropertyChanged("LigneFin");
              }
          }


          public ObservableCollection<FactureModel> FacturesListe
          {
              get { return _facturesListe; }
              set { _facturesListe = value;
              OnPropertyChanged("FacturesListe");
              }
          }

          public bool CheckCancelDateFilter
          {
              get { return checkCancelDateFilter; }
              set { checkCancelDateFilter = value;
              if (value)
              {
                  if (CacheFacturesListe == null)
                  {
                      // CommonModule.AdDatasInDatatable(FilterByDateFacturesListe, newTable);
                      // FacturesListe = CacheFacturesListe;


                      loadDatas(idsociete);
                      CheckCancelDateFilter = false;
                      FilterDateDebut = null;
                  }
                  else
                  {
                      ligneCourante = 0;
                      var query = from lst in CacheFacturesListe.AsEnumerable()
                                  where lst.NumeroLigne <= maxlignes - 1
                                  select lst;
                      listeTeste.Clear();

                      foreach (var f in query)
                      {
                          listeTeste.Add(f);
                          ligneCourante++;
                      }
                      FacturesListe = listeTeste;
                      CacheFacturesListeRecherche = listeTeste;
                      LigneDebut = 1;
                      LigneFin = (int)ligneCourante;
                  }
              }
              OnPropertyChanged("CheckCancelDateFilter");
              }
          }

          public void RefreshListe()
          {
              loadDatas(societeCourante.IdSociete);
          }
          public FactureModel FactureSelected
          {
              get { return _factureSelected; }
              set { _factureSelected = value ;
              GlobalDatas.currentFacture = value;
              if (value != null)
              {
                  if (value.IdStatut == 7)
                  {
                      IsNonValide = false;
                     // MessageBox.Show("Cette facture est Deja Non valable");
                  }
                  else IsNonValide = true;
                  if (value.IdStatut == 1)
                      IsValide = false;
                  else IsValide = true;


              }
              OnPropertyChanged("FactureSelected");
              }
          }
          public LibelleModel LibelleListe
          {
              get { return _libelleListe; }
              set { _libelleListe = value;
              OnPropertyChanged("LibelleListe");
              }
          }

          public ObservableCollection<FactureModel> FilterFacturesListe
          {
              get { return _filterFacturesListe; }
              set { _filterFacturesListe = value;
              OnPropertyChanged("FilterFacturesListe");
              }
          }

          public ObservableCollection<FactureModel> CacheFacturesListe
          {
              get { return _cacheFacturesListe; }
              set { _cacheFacturesListe = value;
              OnPropertyChanged("CacheFacturesListe");
              }
          }
          public ObservableCollection<FactureModel> FilterByCliFacturesListe
          {
              get { return _filterByCliFacturesListe; }
              set { _filterByCliFacturesListe = value;
              OnPropertyChanged("FilterByCliFacturesListe");
              }
          }

          public ObservableCollection<FactureModel> FilterByDateFacturesListe
          {
              get { return _filterByDateFacturesListe; }
              set { _filterByDateFacturesListe = value;
              OnPropertyChanged("FilterByDateFacturesListe");
              }
          }


          public ObservableCollection<FactureModel> RefreshcacheFacturesListeRecherche
          {
              get { return _refreshcacheFacturesListeRecherche; }
              set { _refreshcacheFacturesListeRecherche = value;
              if (value != null)
              {
                  
                  
                            listeTeste = new ObservableCollection<FactureModel>();
                            var query = from lst in value.AsEnumerable()
                                         where lst.NumeroLigne <= maxlignes - 1
                                         select lst;
                            if (query != null)
                            {
                                foreach (var f in query)
                                {
                                    listeTeste.Add(f);
                                    ligneCourante++;
                                }

                                int part_entier = value.Count / maxlignes;
                                int reste = value.Count % maxlignes;

                                if (reste > 0)
                                    Nbrepages = part_entier + 1;
                                else Nbrepages = part_entier;

                                NumeroPageDebut = 1;

                                FacturesListe = null;
                                CacheFacturesListe = null;

                                FacturesListe = listeTeste;
                                CacheFacturesListe = value;
                                CacheFacturesListeRecherche = listeTeste;
                                newTable = CommonModule.SetDataTableFacture();

                                TotalLignes = CacheFacturesListe.Count == 0 ? 0 : CacheFacturesListe.Count;
                                LigneDebut = CacheFacturesListe.Count == 0 ? 0 : 1;
                                LigneFin = (int)ligneCourante;
                                FiltreEnable = true;

                                List<ClientChecked> newClientList = new List<ClientChecked>();
                                ClientChecked item = null;
                                foreach (var fact in CacheFacturesListe)
                                {
                                    if (!newClientList.Exists(cli => cli.idClient == fact.CurrentClient.IdClient))
                                    {
                                        item = new ClientChecked { idClient = fact.CurrentClient.IdClient, IsClientChecked = true, nomClient = fact.CurrentClient.NomClient };
                                        newClientList.Add(item);
                                    }
                                }
                                ListeClient = newClientList;
                            }
                       
                        //else
                        //{
                        //    int ligne = 0;
                        //    var queryd = from fr in value.AsEnumerable()
                        //                 where fr.NumeroLigne > LigneDebut && fr.NumeroLigne <= ligneCourante
                        //                 select fr;

                        //    foreach (var f in queryd)
                        //    {
                        //        listeTeste.Add(f);
                        //        ligne++;
                        //    }
                        //    nbreligneCourante = ligne;

                        //    ligneCourante = ligne + ligneCourante;
                        //    FacturesListe = listeTeste;
                        //    LigneFin = (int)ligneCourante;
                        //    CacheFacturesListeRecherche = listeTeste;

                        //}
                    
              }


              OnPropertyChanged("RefreshcacheFacturesListeRecherche");
              }
          }


          #region FILTER REGION

          public string Filtertexte
          {
              //filtre par client
              get { return filtertexte; }
              set
              {
                  filtertexte = value;
                  if (value != null || value != string.Empty)
                  {
                      filter(value);
                  }

                  this.OnPropertyChanged("Filtertexte");
              }
          }

          public DateTime? FilterDateDebut
          {
              get { return filterDateDebut; }
              set { filterDateDebut = value;
              if (value != null)
                  filterByDate((DateTime)value);
              this.OnPropertyChanged("FilterDateDebut");
              }
          }

          public string FiltertexteFacture
          {
              get { return filtertexteFacture; }
              set
              {
                  filtertexteFacture = value;
                  if (value != null || value != string.Empty)
                      filterByFactureNum(value);

                  this.OnPropertyChanged("FiltertexteFacture");
              }
          }


      

          #endregion

        #endregion


        #region ICOMMAND

          public ICommand EditCommand
          {
              get
              {
                  if (this.editCommand == null)
                  {
                      this.editCommand = new RelayCommand(param => this.canEdit(), param => this.canExecuteEdit());
                  }
                  return this.editCommand;
              }
          }

          
        public ICommand NewCommand
        {
            get
            {
                if (this.newCommand == null)
                {
                    this.newCommand = new RelayCommand(param => this.canNewFacture());
                }
                return this.newCommand;
            }


        }

        public ICommand LendToCommand
        {
            get
            {
                if (this.lendToCommand == null)
                {
                    this.lendToCommand = new RelayCommand(param => this.canShowdetailFacture());
                }
                return this.lendToCommand;
            }


        }

        public ICommand CloseCommand
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

        public RelayCommand SerchByDateCommand
        {
            get
            {
                if (this.serchByDateCommand == null)
                {
                    this.serchByDateCommand = new RelayCommand(param => this.canSearchByDate(param), param => this.canExecuteSearchBydate());
                }
                return this.serchByDateCommand;
            }

        }

        public RelayCommand PreviousCommand
        {
            get
            {
                if (this.previousCommand == null)
                {
                    this.previousCommand = new RelayCommand(param => this.canPrevious(), param => this.canExecutePrevious());
                }
                return this.previousCommand;
            }

        }

        public RelayCommand FirstCommand
        {
            get
            {
                if (this.firstCommand == null)
                {
                    this.firstCommand = new RelayCommand(param => this.canFirst(), param => this.canExecuteFirst());
                }
                return this.firstCommand;
            }

        }


        public RelayCommand NextCommand
        {
            get
            {
                if (this.nextCommand == null)
                {
                    this.nextCommand = new RelayCommand(param => this.canNext(), param => this.canExecuteNext());
                }
                return this.nextCommand;
            }

        }

       

        public RelayCommand LasstCommand
        {
            get
            {
                if (this.lasstCommand == null)
                {
                    this.lasstCommand = new RelayCommand(param => this.canLast(), param => this.canExecuteLast());
                }
                return this.lasstCommand;
            }

        }


        public RelayCommand ImprimerCommand
        {
            get
            {
                if (this.imprimerCommand == null)
                {
                    this.imprimerCommand = new RelayCommand(param => this.canImprimeNext(), param => this.canImprimeExecuteNext());
                }
                return this.imprimerCommand;
            }

        }

        //imprimerToutCommand

        public RelayCommand ImprimerToutCommand
        {
            get
            {
                if (this.imprimerToutCommand == null)
                {
                    this.imprimerToutCommand = new RelayCommand(param => this.canImprimeTout(), param => this.canImprimeToutExecute());
                }
                return this.imprimerToutCommand;
            }

        }


        public RelayCommand ExportCommand
        {
            get
            {
                if (this.exportCommand == null)
                {
                    this.exportCommand = new RelayCommand(param => this.canExport(), param => this.canExecuteExportExcel());
                }
                return this.exportCommand;
            }

        }

        //

        public RelayCommand ExportALLCommand
        {
            get
            {
                if (this.exportALLCommand == null)
                {
                    this.exportALLCommand = new RelayCommand(param => this.canExportAll(), param => this.canExecuteExportAllExcel());
                }
                return this.exportALLCommand;
            }

        }

        public RelayCommand ValidationCommand
        {
            get
            {
                if (this.validationCommand == null)
                {
                    this.validationCommand = new RelayCommand(param => this.canValidation(), param => this.canExecuteValidation());
                }
                return this.validationCommand;
            }

        }

        public RelayCommand NonValalableCommand
        {
            get
            {
                if (this.nonValalableCommand == null)
                {
                    this.nonValalableCommand = new RelayCommand(param => this.canNonValable(), param => this.canExecuteNonValable());
                }
                return this.nonValalableCommand;
            }

        }

        public RelayCommand SuspensionCommand
        {
            get
            {
                if (this.suspensionCommand == null)
                {
                    this.suspensionCommand = new RelayCommand(param => this.canSuspension(), param => this.canExecuteSuspension());
                }
                return this.suspensionCommand;
            }

        }

        public RelayCommand SortieCommand
        {
            get
            {
                if (this.sortieCommand == null)
                {
                    this.sortieCommand = new RelayCommand(param => this.canSortie(), param => this.canExecuteSortie());
                }
                return this.sortieCommand;
            }

        }

       // private RelayCommand selectAllCommand;
       // private RelayCommand deselectAllCommand;

        public RelayCommand SelectAllCommand
        {
            get
            {
                if (this.selectAllCommand == null)
                {
                    this.selectAllCommand = new RelayCommand(param => this.canSelectAll(), param => this.canExecuteSelectAll());
                }
                return this.selectAllCommand;
            }

        }

        public RelayCommand DeselectAllCommand
        {
            get
            {
                if (this.deselectAllCommand == null)
                {
                    this.deselectAllCommand = new RelayCommand(param => this.canDeselectAll(), param => this.canExecuteDeselectAll());
                }
                return this.deselectAllCommand;
            }

        }

        #endregion

        #region METHODS

      

        #region Region Load
        
    
        void loadWright()
        {
            if (CurrentDroit.Super || CurrentDroit.Proprietaire)
            {
                EnbameBtnValide = true;
                EnableBtnSortie = true;
                EnbameBtnSuspendre = false ;
                EnbameBtnNonValable = false ;
               
                if (GlobalDatas.listeCompany !=null )
                    if (GlobalDatas.listeCompany.Count > 1)
                    {
                        SocieteListe = GlobalDatas.listeCompany;
                        CmbStevisible = true;
                    }
            }
            if (CurrentDroit.Proprietaire)
            {
                EnbameBtnValide = true;
                EnbameBtnSuspendre = true;
                EnbameBtnNonValable = true;
            }
        }

        void loadDatas(int idSite)
        {
            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;

            worker.DoWork += (o, args) =>
            {
                try
                {
                    ligneCourante = 0;
                    if (societeCourante != null)
                    {
                        ObservableCollection<FactureModel> liste = factureService.FACTURE_GETLISTE(idSite, 1);

                        listeTeste = new ObservableCollection<FactureModel>();
                        // pagination (première page)
                        if (liste != null)
                        {
                            var query = from lst in liste.AsEnumerable()
                                        where  lst.NumeroLigne <= maxlignes - 1
                                        select lst;
                            if (query != null)
                            {
                                foreach (var f in query)
                                {
                                    listeTeste.Add(f);
                                    ligneCourante++;
                                }


                                int part_entier = liste.Count / maxlignes;
                                int reste = liste.Count % maxlignes;

                                if (reste > 0)
                                    Nbrepages = part_entier + 1;
                                else Nbrepages = part_entier;
                               
                                NumeroPageDebut = 1;

                                FacturesListe = listeTeste;
                                CacheFacturesListe = liste;
                                CacheFacturesListeRecherche = listeTeste;
                                newTable = CommonModule.SetDataTableFacture();

                                TotalLignes = CacheFacturesListe.Count == 0 ? 0 : CacheFacturesListe.Count;
                                LigneDebut =CacheFacturesListe.Count == 0 ? 0:1;
                                LigneFin = (int)ligneCourante;
                                FiltreEnable = true;
                                List<ClientChecked> newClientList = new List<ClientChecked>();
                                ClientChecked item = null;
                                foreach (var fact in CacheFacturesListe)
                                {
                                    if (!newClientList.Exists(cli => cli.idClient == fact.CurrentClient.IdClient))
                                    {
                                        item = new ClientChecked { idClient = fact.CurrentClient.IdClient, IsClientChecked = true, nomClient = fact.CurrentClient.NomClient };
                                        newClientList.Add(item);
                                    }
                                }
                                ListeClient = newClientList;

                            }
                        }
                    }
                 
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LIST FACTURES";
                }

                try
                {
                    ListeClientFacture = clientService.CLIENT_GETLISTE_FACTURER(societeCourante.IdSociete);

                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LIST CLIENTS FACTURER";
                }

            };
            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                    view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
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

        private void  canShowdetailFacture(){
            int val = 0;
        }


        #endregion

        void canSelectAll()
        {
            var newList = FacturesListe;

            foreach (var facture in newList)
                facture.IsCheck = true;
            FacturesListe = null;
            FacturesListe = newList;


        }

        bool canExecuteSelectAll()
        {
            bool values = false;
            if (FacturesListe != null)
            {
                if (FacturesListe.FirstOrDefault(f => f.IsCheck == false ) != null)
                    values = true;
            }
            return values;
        }

        void canDeselectAll()
        {
            var newList = FacturesListe;

            foreach (var facture in newList)
                facture.IsCheck = false ;
            FacturesListe = null;
            FacturesListe = newList;
        }

        bool canExecuteDeselectAll()
        {
            bool values = false;
            if (FacturesListe != null)
            {
                if (FacturesListe.FirstOrDefault(f => f.IsCheck == true) !=null )
                    values = true;
            }
            return values;
        }

        #region Region EDITE FACTURE
        
      

        private void canEdit()
        {
            WFacturationModal_vie logginView = _container.Resolve<WFacturationModal_vie>();
            //WFacturationModal_vie vf = new WFacturationModal_vie();
            logginView.Owner = Application.Current.MainWindow;
            //logginView.FactureSelected = FactureSelected;
            GlobalDatas.currentFacture =null;
            FactureSelected = null;
            //Facturation_viewModel.factureListeSelected = FactureSelected;
            logginView.ShowDialog();
        }

        bool canExecuteEdit()

        {
            return FactureSelected != null ? true : false;
        }
        private void canClose()
        {
            _injectSingleViewService.ClearViewsFromRegion(RegionNames.ContentRegion);
            UserAffiche uaffiche = _container.Resolve<UserAffiche>();


            IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion,
                                                 () => uaffiche);
        }

        #endregion

        #region Region PAGING

        void canFirst()
        {
            listeTeste = new ObservableCollection<FactureModel>();
            if (ligneCourante >= maxlignes)
            {
                LigneFin = maxlignes;
                LigneDebut =1 ;
                int ligne = 0;

                var query = from lst in CacheFacturesListe.AsEnumerable()
                            where lst.NumeroLigne <= maxlignes - 1
                            select lst;

                foreach (var f in query)
                {
                    listeTeste.Add(f);
                    ligne++;
                }

                nbreligneCourante = ligne;
                ligneCourante = ligne;
                FacturesListe = listeTeste;
                CacheFacturesListeRecherche = listeTeste;
                NumeroPageDebut = 1;
            }

        }

        bool canExecuteFirst()
        {
            return FacturesListe != null ? ((LigneDebut == 0 || LigneDebut == 1) ? false : true) : false;
           // return true;
        }
       

        private void canPrevious()
        {
            if (ligneCourante >= maxlignes)
            {
                listeTeste = new ObservableCollection<FactureModel>();
                this.IsBusy = true;
                ProgressBarVisibility = true;
             

                ligneCourante = ligneCourante - nbreligneCourante;
                LigneFin =(int) ligneCourante;
                LigneDebut =(int) (ligneCourante - maxlignes)+1;

                var query = from fr in CacheFacturesListe.AsEnumerable()
                            where fr.NumeroLigne > ((ligneCourante <= maxlignes ? ligneCourante : ligneCourante - 1) - maxlignes) && fr.NumeroLigne <= ligneCourante - 1 
                            select fr;
                int ligne = 0;
                foreach (var f in query)
                {
                    listeTeste.Add(f);
                    ligne++;
                }
                nbreligneCourante = ligne;
                //ligneCourante = ligne;
                FacturesListe = listeTeste;
                CacheFacturesListeRecherche = listeTeste;
                NumeroPageDebut -= 1;
            }

            this.IsBusy = false ;
            ProgressBarVisibility = false ;
        }

        bool canExecutePrevious()
        {

           return FacturesListe != null ? ((LigneDebut == 0 || LigneDebut == 1) ? false : true) : false;
          // return true ;
          
        }


        private void canNext()
        {
            if (ligneCourante-1 < CacheFacturesListe.Count)
            {
                //listeTeste.Clear();
                listeTeste = new ObservableCollection<FactureModel>();
                this.IsBusy = true;
                ProgressBarVisibility = true;
                LigneDebut =(int) ligneCourante+1;
               
                int ligne = 0;
                var query = from fr in CacheFacturesListe.AsEnumerable()
                            where fr.NumeroLigne > ligneCourante-1 && fr.NumeroLigne <= ((ligneCourante-1) + maxlignes)
                            select fr;

           
                foreach (var f in query)
                {
                        listeTeste.Add(f);
                        ligne++;
                }
                nbreligneCourante = ligne;

                ligneCourante = ligne +ligneCourante;
                FacturesListe = listeTeste;
                LigneFin =(int) ligneCourante;
                CacheFacturesListeRecherche = listeTeste;
                NumeroPageDebut += 1;

                this.IsBusy = false;
                ProgressBarVisibility = false;

            }
        }

        bool canExecuteNext()
        {
            return FacturesListe != null ? (CacheFacturesListe.Count > 1 ? 
               ((LigneFin >= CacheFacturesListe.Count || LigneFin == CacheFacturesListe.Count) ? false : true ) : false) : false;
           // return true;
           
        }

        void canLast()
        {
            if (ligneCourante - 1 < CacheFacturesListe.Count)
            {
                this.IsBusy = true;
                ProgressBarVisibility = true;
                listeTeste = new ObservableCollection<FactureModel>();

                int reste = CacheFacturesListe.Count % maxlignes;
                int ligne = 0;

                if (reste > 0)
                {
                   var   query = from fr in CacheFacturesListe.AsEnumerable()
                                where fr.NumeroLigne > ((CacheFacturesListe.Count - reste)-1) && fr.NumeroLigne <= CacheFacturesListe.Count 
                                select fr;
                   foreach (var f in query)
                   {
                       listeTeste.Add(f);
                       ligne++;
                   }
                   LigneDebut = (CacheFacturesListe.Count - reste)+1;
                   ligneCourante = CacheFacturesListe.Count ;
                   NumeroPageDebut = (CacheFacturesListe.Count / maxlignes) + 1;
                }
                else
                {
                    var query = from fr in CacheFacturesListe.AsEnumerable()
                                where fr.NumeroLigne > ((CacheFacturesListe.Count - maxlignes)-1)   && fr.NumeroLigne <= CacheFacturesListe.Count
                                select fr;
                    foreach (var f in query)
                    {
                        listeTeste.Add(f);
                        ligne++;
                    }
                    LigneDebut = (CacheFacturesListe.Count - maxlignes)+1;
                    ligneCourante = CacheFacturesListe.Count;
                    NumeroPageDebut = (CacheFacturesListe.Count / maxlignes);
                }
                nbreligneCourante = ligne;
                FacturesListe = listeTeste;
                LigneFin = CacheFacturesListe.Count;
            }

            this.IsBusy = false;
            ProgressBarVisibility = false;

        }

        bool canExecuteLast()
        {
            return FacturesListe != null ? (CacheFacturesListe.Count > 1 ?
               ((LigneFin >= CacheFacturesListe.Count || LigneFin == CacheFacturesListe.Count) ? false : true) : false) : false;
            //return true;
        }

        #endregion

        #region SEARCH BY DATE
        
      
        private void canSearchByDate(object param)
        {

           this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            Int32 idClient = 0;
            if (ClientFactireselect != null)
                idClient = ClientFactireselect.IdClient;
            else idClient = -1;

            worker.DoWork += (o, args) =>
            {
                try
                {
                    ObservableCollection<FactureModel> liste = factureService.FACTURE_SEARCH(societeCourante.IdSociete, DateDebut.Value, DateFin.Value, idClient);
                    listeTeste = new ObservableCollection<FactureModel>();
                    if (liste != null)
                    {
                        Nbrepages = 0;
                        ligneCourante = 0;
                        var query = from lst in liste.AsEnumerable()
                                    where lst.NumeroLigne <= maxlignes - 1
                                    select lst;
                        if (query != null)
                        {
                            foreach (var f in query)
                            {
                                listeTeste.Add(f);
                                ligneCourante++;
                            }


                            int part_entier = liste.Count / maxlignes;
                            int reste = liste.Count % maxlignes;

                            if (reste > 0)
                                Nbrepages = part_entier + 1;
                            else Nbrepages = part_entier;

                            NumeroPageDebut = 1;
                            FacturesListe = null;
                            CacheFacturesListe = null;
                            CacheFacturesListeRecherche = null;

                            FacturesListe = listeTeste;
                            CacheFacturesListe = liste;
                            CacheFacturesListeRecherche = listeTeste;
                            newTable = CommonModule.SetDataTableFacture();

                            TotalLignes = CacheFacturesListe.Count == 0 ? 0 : CacheFacturesListe.Count;
                            LigneDebut = CacheFacturesListe.Count == 0 ? 0 : 1;
                            LigneFin = (int)ligneCourante;
                            FiltreEnable = true;
                            List<ClientChecked> newClientList = new List<ClientChecked>();
                            ClientChecked item = null;
                            foreach (var fact in CacheFacturesListe)
                            {
                                if (!newClientList.Exists(cli => cli.idClient == fact.CurrentClient.IdClient))
                                {
                                    item = new ClientChecked { idClient = fact.CurrentClient.IdClient, IsClientChecked = true, nomClient = fact.CurrentClient.NomClient };
                                    newClientList.Add(item);
                                }
                            }
                            ListeClient = newClientList;
                        }
                    }
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LIST FACTURES";
                }


              

            };
            worker.RunWorkerCompleted += (o, args) =>
            {
                if (args.Result != null)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                    view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
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
        bool canExecuteSearchBydate()
        {
            return DateDebut.HasValue == true ? (DateFin.HasValue == true ? true : false) : false;
        }

        #endregion

        private void canNewFacture()
        {
            //WFacturationModal_vie vf = new WFacturationModal_vie();
            WFacturationModal_vie logginView = _container.Resolve<WFacturationModal_vie>();
            logginView.Owner = Application.Current.MainWindow;
            GlobalDatas.currentFacture =null;
            logginView.ShowDialog();
        }



        #region IMPRESSION

        void canImprimeTout()
        {

            SetdataTaSet();

            DataTable tclient = null;
            DataTable tableSociete = null;
            DataTable tablePiedPage = null;
            DataTable tableLibelle = null;
            DataTable tablefacture = null;
            DataTable tableLignefactures = null;
            string mode = string.Empty;
            try
            {
               var  query = (from chk in CacheFacturesListeRecherche.AsEnumerable()
                         where chk.IsCheck == true
                         select chk).ToList();
               if (query.Count <= 0)
               {
                   MessageBox.Show("Pas de Facture sélectionnées Pour l'impression");
               }
               else
               {
                   IsBusy = true;
                   System.Drawing.Printing.PrintDocument printDocument = null;
                   reportClientPartiel rptPartiel = null;
                   reportWithoutPorata rptNonProrata = null;
                   reportExonere rptProrata = null;



                   foreach (FactureModel facture in query)
                   {
                       SetdataTaSet();
                       tclient = null;
                       tableSociete = null;
                       tablePiedPage = null;
                       tableLibelle = null;
                       tablefacture = null;
                       tableLignefactures = null;

                       mode = facture.CurrentClient.Exonerere.CourtDesc;
                       tclient = ReportModel.GetReportClient(facture.IdClient);
                       tableSociete = ReportModel.GetReportSociete();
                       tablePiedPage = ReportModel.GetReporPiedPage();
                       tableLibelle = ReportModel.GetLibelle(facture.CurrentClient.IdLangue);
                       tablefacture = ReportModel.GetFacture(facture.IdFacture);

                        foreach (DataRow row in tclient.Rows)
                            DataProvider.Ds.TableClient.ImportRow(row);

                        foreach (DataRow row in tableSociete.Rows)
                            DataProvider.Ds.Table_Societe.ImportRow(row);

                        foreach (DataRow row in tablePiedPage.Rows)
                            DataProvider.Ds.TPiedpagefacture.ImportRow(row);

                        foreach (DataRow row in tablefacture.Rows)
                            DataProvider.Ds.TableFacture.ImportRow(row);

                      

                        foreach (DataRow row in tableLibelle.Rows)
                            DataProvider.Ds.Tlibelle.ImportRow(row);

                        if (mode == "part")
                        {
                             tableLignefactures = ReportModel.GetLigneFacture_nonExo(facture.IdFacture);
                            foreach (DataRow row in tableLignefactures.Rows)
                                DataProvider.Ds.TableligneFacture.ImportRow(row);
                            rptPartiel = new reportClientPartiel();
                            rptPartiel.SetDataSource(DataProvider.Ds);
                            printDocument = new System.Drawing.Printing.PrintDocument();
                            rptPartiel.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                            rptPartiel.PrintToPrinter(1, true, 0, 0);


                        }
                        else
                        {
                            //int newMode = 0;

                          
                             tableLignefactures = ReportModel.GetLigneFacture(facture.IdFacture);
                            foreach (DataRow row in tableLignefactures.Rows)
                                DataProvider.Ds.TableligneFacture.ImportRow(row);

                            if (mode == "non")
                            {
                                 rptNonProrata = new reportWithoutPorata();
                                rptNonProrata.SetDataSource(DataProvider.Ds);
                                printDocument = new System.Drawing.Printing.PrintDocument();
                                rptNonProrata.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                                rptNonProrata.PrintToPrinter(1, true, 0, 0);

                            }
                            else
                            {
                                 rptProrata = new reportExonere();
                                rptProrata.SetDataSource(DataProvider.Ds);
                                printDocument = new System.Drawing.Printing.PrintDocument();
                                rptProrata.PrintOptions.PrinterName = printDocument.PrinterSettings.PrinterName;
                                rptProrata.PrintToPrinter(1, true, 0, 0);

                            }
                               // newMode = 1;
                          //  else newMode = 2;
                        }

                   }
               }

             
                MessageBox.Show("Fin impression des factures!"); 
                // See more at: http://codingresolved.com/discussion/18/print-report-without-preview-/p1#sthash.lRE3MXKC.dpuf

                IsBusy = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message ="Probleme survenu lors de l'impression de la liste de factures"+ex.Message ;
                view.ShowDialog();
                IsBusy = false;
            }
        }

        void SetdataTaSet()
        {
            DataProvider.Ds.TableClient.Clear();
            DataProvider.Ds.Table_Societe.Clear();
            DataProvider.Ds.TPiedpagefacture.Clear();
            DataProvider.Ds.TableFacture.Clear();
            DataProvider.Ds.TableligneFacture.Clear();
            DataProvider.Ds.Tlibelle.Clear();
        }

        bool canImprimeToutExecute()
        {
            bool values = false;
            if (FacturesListe != null && FacturesListe.Count > 0)
            {
                foreach (var item in FacturesListe)
                    if (item.IsCheck == true )
                    {
                        values = true;
                        break;
                    }
            }
            else values = false;
            return values;
        }

        void canImprimeNext()
        {
            string mode = string.Empty;

            if (CurrentDroit.Super || CurrentDroit.Impression )
            {
                try
                {
                    IsBusy = true;

                    DataTable tclient = ReportModel.GetReportClient(FactureSelected.IdClient);
                    DataTable tableSociete = ReportModel.GetReportSociete();
                    DataTable tablePiedPage = ReportModel.GetReporPiedPage();
                    DataTable tableLibelle = ReportModel.GetLibelle(FactureSelected.CurrentClient.IdLangue);
                    DataTable tablefacture = ReportModel.GetFacture(FactureSelected.IdFacture);

                    if (FactureSelected.CurrentClient.Exonerere == null)
                    {
                        ExonerationModel exo = new ExonerationModel();
                        mode = exo.EXONERATION_SELECTById(FactureSelected.CurrentClient.IdExonere).CourtDesc;
                    }
                    else mode = FactureSelected.CurrentClient.Exonerere.CourtDesc;



                    if (mode == "part")
                    {
                        DataTable tableLignefactures = ReportModel.GetLigneFacture_nonExo(FactureSelected.IdFacture);
                        formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefactures, tableLibelle, 3);
                        vf.ShowDialog();
                    }
                    else
                    {
                        int newMode = 0;

                        if (mode == "non")
                            newMode = 1;
                        else newMode = 2;
                        DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureSelected.IdFacture);
                        formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefacture, tableLibelle, newMode);
                        vf.ShowDialog();
                    }

                    IsBusy = false;


                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();

                    this.IsBusy = false;
                }
            }
        

        }
       

        bool canImprimeExecuteNext()
        {
            return FactureSelected!=null ? true :false ;
        }
        #endregion

        #region REGION EXPORT
        
     
        void canExport()
        {
            try
            {
                IsBusy = true;
               
                List <FactureModel> query = (from chk in CacheFacturesListeRecherche.AsEnumerable()
                            where chk.IsCheck == true
                            select chk).ToList ();


                if (query.Count >0)
                {
                    //societeCourante
                    CommonModule.ExportToExcel(query, societeCourante);
                }
                else
                {
                     //export la ligne paginer

                    List<FactureModel> queryAll = (from chk in CacheFacturesListeRecherche.AsEnumerable()
                                                
                                                select chk).ToList();
                    CommonModule.ExportToExcel(queryAll, societeCourante);
                }
                IsBusy = false ;

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE ERREUR EXPORT EXCEL SELECTION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

                this.IsBusy = false;
            }
        }

        bool canExecuteExportExcel()
        {
            return FacturesListe != null ?( FacturesListe.Count > 0?true :false  ): false;
        }

        void canExportAll()
        {
            try
            {
                IsBusy = true;
              
                CommonModule.ExportToExcel(CacheFacturesListe.ToList (), societeCourante);
                this.IsBusy = false;
              
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Title = "MESSAGE ERREUR EXPORT EXCEL TOUT";
                view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

                this.IsBusy = false;
            }
        }

        bool canExecuteExportAllExcel()
        {
            return CacheFacturesListe != null ? (CacheFacturesListe.Count > 0 ? true : false) : false;
        }
        #endregion

        #region REGION VALIDATION 
        
      

        void canValidation()
        {
            List<FactureModel> query=null ;
            this.IsBusy = true ;

            bool modifDatas = false;
            try
            {
                 query = (from chk in CacheFacturesListeRecherche.AsEnumerable()
                                            where chk.IsCheck == true
                                            select chk).ToList();

                if (query.Count <= 0)
                {
                    MessageBox.Show("Pas de Facture sélectionnées Pour cette opération");
                }
                else
                {

                      StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION  VALIDATION";
                        messageBox.ViewModel.Message = "Confirmez la Validation ?";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            bool isvalueSelected = false;

                          
                            string numfacture = string .Empty ;
                            string factureDejaValider=string .Empty ;
                             string factureDejaSortie=string .Empty ;
                             string factureDejaSuspendu = string.Empty;
                             string factureDejaNonValable = string.Empty;
                             string factureNonValide = string.Empty;
                             bool isErrorMessage = false;

                            foreach (FactureModel facture in query)
                            {
                                if (facture.ClienOk == false)
                                {
                                    numfacture = numfacture + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }

                                if (facture.CurrentStatut.CourtDesc == "3")
                                {
                                    factureDejaValider = factureDejaValider + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.CurrentStatut.CourtDesc == "4")
                                {
                                    factureDejaSortie = factureDejaSortie + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.CurrentStatut.CourtDesc == "5")
                                {
                                    factureDejaSuspendu = factureDejaSuspendu + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.CurrentStatut.CourtDesc == "6")
                                {
                                    factureDejaNonValable = factureDejaNonValable + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (int.Parse(facture.CurrentStatut.CourtDesc) == 1)
                                {
                                    factureNonValide = factureNonValide + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }                               
                            }

                            if (isErrorMessage)
                            {
                                string messageError = "les factures suivantes ne pourront pas être Validées";
                                if (!string.IsNullOrEmpty(numfacture))
                                  messageError=  messageError + "\n" + numfacture +"\n Informations du Client Incompletes" ;

                                if (!string.IsNullOrEmpty(factureNonValide))
                                    messageError = messageError + "\n" + factureNonValide + "\n Encore en création";

                                if (!string.IsNullOrEmpty(factureDejaValider))
                                    messageError = messageError + "\n" + factureDejaValider + "\n Factures Validées";
                                if (!string.IsNullOrEmpty(factureDejaSortie))
                                    messageError = messageError + "\n" + factureDejaSortie + "\n Factures Sortie";
                                if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                    messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures Suspendus";

                                if (!string.IsNullOrEmpty(factureDejaNonValable))
                                    messageError = messageError + "\n" + factureDejaNonValable + "\n Non Valable";

                                MessageBox.Show(messageError);
                            }


                            StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");

                            foreach (FactureModel facture in query)
                            {
                                if (facture.ClienOk)
                                {
                                    if (facture.CurrentStatut .CourtDesc  == "2")
                                    {
                                        factureService.FACTURE_VALIDATION(facture.IdFacture, DateTime.Now, newStatut.IdStatut, UserConnected.Id, true);
                                        FactureModel factureUpdate = factureService.GET_FACTURE_BYID(facture.IdFacture);

                                        

                                        FacturesListe.Remove(facture);
                                        //CacheFacturesListe.Remove(facture);
                                        //CacheFacturesListeRecherche.Remove(facture);

                                        FacturesListe.Add(factureUpdate);

                                        FacturesListe.OrderByDescending(f=>f.DateCloture );//.Sort(f => f.DateCloture );
                                       

                                        modifDatas = false ;
                                    }
                                }
                            }

                           
                        }

                    modifDatas = false;
                }


              
               
                this.IsBusy = false;

                FactureSelected = null;

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

                this.IsBusy = false;
            }


        }

        void refreshDatas()
        {
        }

        bool canExecuteValidation()
        {
            bool values = false;
            if (FacturesListe != null && FacturesListe.Count > 0)
            {
                foreach (var item in  FacturesListe)
                    if (item.IsCheck == true)
                    {
                        values= true;
                        break;
                    }
            }
            else values= false;
            return values;
            //return (IsValide == true ? (EnbameBtnValide == true ? true : false) : false);
        }

        #endregion

        #region Region SORTIE

        void canSortie()
        {
            List<FactureModel> query = null;
            try
            {

                query = (from chk in CacheFacturesListeRecherche.AsEnumerable()
                         where chk.IsCheck == true
                         select chk).ToList();
                if (query.Count <= 0)
                {
                    MessageBox.Show("Pas de Facture sélectionnées Pour cette opération");
                }
                else
                {
                     StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION  VALIDATION SORTIE";
                        messageBox.ViewModel.Message = "Confirmez la sortie ?";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            //vérification si client ok
                            string numfacture = string.Empty;
                            string factureDejaValider = string.Empty;
                            string factureDejaSortie = string.Empty;
                            string factureDejaSuspendu = string.Empty;
                            string factureDejaNonValable = string.Empty;
                            string factureNonValide = string.Empty;
                            bool isErrorMessage = false;


                            foreach (FactureModel facture in query)
                            {
                                if (facture.ClienOk == false)
                                {
                                    numfacture = numfacture + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }

                                if (facture.CurrentStatut.CourtDesc == "3")
                                {
                                    factureDejaValider = factureDejaValider + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.CurrentStatut.CourtDesc == "4")
                                {
                                    factureDejaSortie = factureDejaSortie + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.CurrentStatut.CourtDesc == "5")
                                {
                                    factureDejaSuspendu = factureDejaSuspendu + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.CurrentStatut.CourtDesc == "6")
                                {
                                    factureDejaNonValable = factureDejaNonValable + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (int.Parse(facture.CurrentStatut.CourtDesc) == 1)
                                {
                                    factureNonValide = factureNonValide + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }


                            }


                            if (isErrorMessage)
                            {
                                string messageError = "les factures suivantes ne pourront pas être Validées";
                                if (!string.IsNullOrEmpty(numfacture))
                                    messageError = messageError + "\n" + numfacture + "\n Client Incomplet";

                                if (!string.IsNullOrEmpty(factureNonValide))
                                    messageError = messageError + "\n" + factureNonValide + "\n En Cours de Validation";

                                if (!string.IsNullOrEmpty(factureDejaSortie))
                                    messageError = messageError + "\n" + factureDejaSortie + "\n Factures deja Sorties";
                                if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                    messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures Suspendus";

                                if (!string.IsNullOrEmpty(factureDejaNonValable))
                                    messageError = messageError + "\n" + factureDejaNonValable + "\n Non Valide";

                                MessageBox.Show(messageError);
                            }

                 
                            List<FactureModel> queryDateSortie = (from nn in query.AsEnumerable()
                                                           where nn.DateSortie== null 
                                                           select nn).ToList();

                            StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "4");
                            foreach (FactureModel facture in queryDateSortie)
                            {
                                if (facture.DateCloture != null)
                                {

                                    factureService.FACTURE_SORTIE(facture.IdFacture, DateTime.Now, newStatut.IdStatut, UserConnected.Id, true);
                                    FactureModel factureUpdate = factureService.GET_FACTURE_BYID(facture.IdFacture);

                                    FacturesListe.Remove(facture);
                                    FacturesListe.Add(factureUpdate);
                                    FacturesListe.OrderByDescending(f => f.DateSortie );

                                    //CacheFacturesListe.Remove(facture);
                                    //CacheFacturesListe.Add(factureUpdate);
                                    //CacheFacturesListeRecherche.Remove(facture);
                                    //CacheFacturesListeRecherche.Add(factureUpdate);

                                }
                            }
                            

                        }
                }

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

              
            }



        }

        bool canExecuteSortie()
        {
           // return FactureSelected !=null ?(IsValide == true?true :false) :false ;
            bool values = false;
            if (FacturesListe != null && FacturesListe.Count > 0)
            {
                foreach (var item in FacturesListe)
                    if (item.IsCheck == true && item.CurrentStatut.CourtDesc == "3")
                    {
                        values = true;
                        break;
                    }
            }
            else values = false;
            return values;
        }

        #endregion

        #region REGION SUSPENSION

        void canSuspension()
        {
            bool modifDatas = false;
            try
            {


                List<FactureModel> querySuspension = (from chk in CacheFacturesListeRecherche.AsEnumerable()
                                                   where chk.IsCheck == true
                                                   select chk).ToList();

                            if (querySuspension.Count <= 0)
                            {
                                MessageBox.Show("Pas de Facture sélectionnées Pour cette opération");
                            }
                            else
                            {
                                // StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(5);
                                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                messageBox.Owner = Application.Current.MainWindow;
                                messageBox.Title = "INFORMATION  SUSPENSION FACTURE";
                                messageBox.ViewModel.Message = "Confirmez action  de suspension , Voulez Vous continuez ?";
                                if (messageBox.ShowDialog().Value == true)
                                {
                                   
                                    string numfacture = string.Empty;
                                    string factureDejaValider = string.Empty;
                                    string factureDejaSortie = string.Empty;
                                    string factureDejaSuspendu = string.Empty;
                                    string factureDejaNonValable = string.Empty;
                                    string factureNonValide = string.Empty;
                                    bool isErrorMessage = false;

                                    foreach (FactureModel facture in querySuspension)
                                    {
                                      
                                        if (facture.CurrentStatut.CourtDesc == "4")
                                        {
                                            factureDejaSortie = factureDejaSortie + "\n" + facture.NumeroFacture;
                                            isErrorMessage = true;
                                        }
                                        if (facture.CurrentStatut.CourtDesc == "5")
                                        {
                                            factureDejaSuspendu = factureDejaSuspendu + "\n" + facture.NumeroFacture;
                                            isErrorMessage = true;
                                        }
                                        if (facture.CurrentStatut.CourtDesc == "6")
                                        {
                                            factureDejaNonValable = factureDejaNonValable + "\n" + facture.NumeroFacture;
                                            isErrorMessage = true;
                                        }
                                       
                                    }

                                    if (isErrorMessage)
                                    {
                                        string messageError = "les factures suivantes ne pourront pas être Validées";
                                     
                                        if (!string.IsNullOrEmpty(factureDejaSortie))
                                            messageError = messageError + "\n" + factureDejaSortie + "\n Factures deja Sortie";
                                        if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                            messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures deja Suspendus";

                                        if (!string.IsNullOrEmpty(factureDejaNonValable))
                                            messageError = messageError + "\n" + factureDejaNonValable + "\n Factures Non Valide";

                                        MessageBox.Show(messageError);
                                    }





                                    StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "5");
                                foreach (FactureModel facture in querySuspension)
                                {
                                    if (int.Parse(facture.CurrentStatut.CourtDesc) != 4)
                                    {
                                        if (int.Parse(facture.CurrentStatut.CourtDesc) != 5)
                                        {
                                            if (int.Parse(facture.CurrentStatut.CourtDesc) != 6)
                                            {
                                                factureService.FACTURE_SUSPENSION(facture.IdFacture, newStatut.IdStatut, UserConnected.Id, true);
                                                FactureModel factureUpdate = factureService.GET_FACTURE_BYID(facture.IdFacture);

                                                FacturesListe.Remove(facture);
                                                //CacheFacturesListe.Remove(facture);
                                                //CacheFacturesListeRecherche.Remove(facture);

                                                FacturesListe.Add(factureUpdate);
                                                FacturesListe.OrderByDescending(f => f.DateSuspension  );
                                                //CacheFacturesListe.Add(factureUpdate);
                                                //CacheFacturesListeRecherche.Add(factureUpdate);

                                                modifDatas = false;
                                            }
                                        }
                                    }

                                }

                                modifDatas = false;

                            }
                   
                        }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

                this.IsBusy = false;
            }
        }

        bool canExecuteSuspension()
        {
            bool values = false;
            if (FacturesListe != null && FacturesListe.Count > 0)
            {
                foreach (var item in FacturesListe)
                    if (item.IsCheck == true && item.CurrentStatut.CourtDesc == "3")
                    {
                        values = true;
                        break;
                    }
            }
            else values = false;
            return values;
           // return FacturesListe != null ? (FacturesListe.Count > 0 ? (FactureSelected != null ? (EnbameBtnSuspendre==true ?true :false ) : false) : false) : false;
        }
        #endregion

        #region REGION NON VALABLE

        void canNonValable()
        {
            bool modifDatas = false;
            try
            {

                List<FactureModel> queryNonValable = (from chk in CacheFacturesListeRecherche.AsEnumerable()
                                                      where chk.IsCheck == true
                                                      select chk).ToList();

                if (queryNonValable.Count <= 0)
                {
                    MessageBox.Show("Pas de Facture sélectionnées Pour cette opération");
                }
                else { 
                 StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION  STATUT NON VALABLE";
                        messageBox.ViewModel.Message = "Confirmer Action de Facture non  valide,\n Voulez Vous continuez ?";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            string numfacture = string.Empty;
                            string factureDejaValider = string.Empty;
                            string factureDejaSortie = string.Empty;
                            string factureDejaSuspendu = string.Empty;
                            string factureDejaNonValable = string.Empty;
                            string factureNonValide = string.Empty;
                            bool isErrorMessage = false;

                            foreach (FactureModel facture in queryNonValable)
                            {
                                if (facture.CurrentStatut.CourtDesc == "4")
                                {
                                    factureDejaSortie = factureDejaSortie + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }

                                if (facture.CurrentStatut.CourtDesc == "5")
                                {
                                    factureDejaSuspendu = factureDejaSuspendu + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }

                                if (facture.CurrentStatut.CourtDesc == "6")
                                {
                                    factureDejaNonValable = factureDejaNonValable + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }

                            }

                            if (isErrorMessage)
                            {
                                string messageError = "les factures suivantes ne pourront pas être Validées";
                              
                                if (!string.IsNullOrEmpty(factureDejaSortie))
                                    messageError = messageError + "\n" + factureDejaSortie + "\n Factures Deja Sortie";
                                if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                    messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures Deja Suspendus";

                                if (!string.IsNullOrEmpty(factureDejaNonValable))
                                    messageError = messageError + "\n" + factureDejaNonValable + "\n Factures Deja Non Valide";

                                MessageBox.Show(messageError);
                            }

                            StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "6");
                            foreach (FactureModel facture in queryNonValable)
                            {
                                if (int.Parse(facture.CurrentStatut.CourtDesc) != 6)
                                {
                                    if (int.Parse(facture.CurrentStatut.CourtDesc) != 5)
                                    {
                                        if (int.Parse(facture.CurrentStatut.CourtDesc) != 4)
                                        {
                                            factureService.FACTURE_NONVALABLE(facture.IdFacture, newStatut.IdStatut, UserConnected.Id, true);
                                            FactureModel factureUpdate = factureService.GET_FACTURE_BYID(facture.IdFacture);
                                            FacturesListe.Remove(facture);
                                            FacturesListe.Add(factureUpdate);
                                            FacturesListe.OrderByDescending(f => f.DateNonValable );

                                           
                                        }
                                    }

                                }
                               
                            }

                              
                           }
                        }

                       
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

                this.IsBusy = false;
            }
        }

        bool canExecuteNonValable()
        {
           // return FacturesListe != null ? (FacturesListe.Count > 0 ? (IsNonValide == true ? (EnbameBtnNonValable == true ? true : false) : false) : false) : false;
            bool values = false;
            if (FacturesListe != null && FacturesListe.Count > 0)
            {
                foreach (var item in FacturesListe)
                    if (item.IsCheck == true && !item.CurrentStatut.CourtDesc.Equals ("4"))
                    {
                        values = true;
                        break;
                    }
            }
            else values = false;
            return values;
        }
        #endregion


        #region Filtre Zone


        //filtre par client
       public  void filter(string values)
        {
      

            if (FacturesListe != null || FacturesListe.Count > 0)
            {
                if (newTable != null)
                {
                    newTable.Clear();

                    if (string.IsNullOrEmpty(FiltertexteFacture))
                    {
                        if (FilterDateDebut == null)
                        {
                            // si pas de filtre facture e pas de filtre date donc filtre par client uniquement
                            CommonModule.AdDatasInDatatable(CacheFacturesListeRecherche, newTable);
                        }
                        else
                            CommonModule.AdDatasInDatatable(FilterByDateFacturesListe, newTable);
                    }
                    else
                    {
                        if (FilterDateDebut == null)
                            // si pas de filtre facture e pas de filtre date donc filtre par client uniquement
                            CommonModule.AdDatasInDatatable(FilterFacturesListe, newTable);
                        else CommonModule.AdDatasInDatatable(FilterByDateFacturesListe, newTable);
                    }


                    DataRow[] nlignes = newTable.Select(string.Format("client like '{0}%'", values.Trim()));
                    if (nlignes.Length == 0)
                        nlignes = newTable.Select(string.Format("client like '%{0}'", values.Trim()));
                    if (nlignes.Length == 0)
                        nlignes = newTable.Select(string.Format("client like '%{0}%'", values.Trim()));

                    DataTable filterDatatable = newTable.Clone();
                    foreach (DataRow rows in nlignes)
                        filterDatatable.ImportRow(rows);

                    FactureModel fm;
                    ObservableCollection<FactureModel> newliste = new ObservableCollection<FactureModel>();

                    foreach (DataRow r in filterDatatable.Rows)
                    {
                        fm = new FactureModel()
                        {
                            IdFacture = Int64.Parse(r[0].ToString()),
                            NumeroFacture = r[1].ToString(),
                            MoisPrestation = DateTime.Parse(r[2].ToString()),
                            IdObjetFacture = Int32.Parse(r[3].ToString()),

                            IdClient = Int32.Parse(r[4].ToString()),
                            // Profile = CacheUsers.First(f => f.IdProfile == int.Parse(r[7].ToString())).Profile,
                            IdTaxe = int.Parse(r[5].ToString()),
                            IdDevise = Int32.Parse(r[6].ToString()),
                            IdStatut = Int32.Parse(r[7].ToString()),
                            IdModePaiement = int.Parse(r[8].ToString()),
                            IdCreerpar = Int32.Parse(r[9].ToString()),
                            IdModifierPar = Int32.Parse(r[10].ToString()),
                            DateCloture = r[12] !=DBNull .Value  ? DateTime.Parse(r[12].ToString()):(DateTime?)null  ,
                            DateEcheance = r[13] != DBNull.Value ? DateTime.Parse(r[13].ToString()) : (DateTime?)null,
                            DateCreation = r[14] != DBNull.Value ? DateTime.Parse(r[14].ToString()) : (DateTime?)null,
                            CurrentClient = CacheFacturesListe.First(cl => cl.IdClient == Int32.Parse(r[4].ToString())).CurrentClient,
                            CurrentDevise = CacheFacturesListe.First(cl => cl.IdDevise == Int32.Parse(r[6].ToString())).CurrentDevise,

                            CurrentModePaiement = CacheFacturesListe.First(cl => cl.IdModePaiement == Int32.Parse(r[8].ToString())).CurrentModePaiement,
                            CurrentObjetFacture = CacheFacturesListe.First(cl => cl.IdObjetFacture == Int32.Parse(r[3].ToString())).CurrentObjetFacture,
                            CurrentStatut = CacheFacturesListe.First(cl => cl.IdStatut == Int32.Parse(r[7].ToString())).CurrentStatut,
                            CurrentTaxe = CacheFacturesListe.First(cl => cl.IdTaxe == Int32.Parse(r[5].ToString())).CurrentTaxe,
                            UserCreate = CacheFacturesListe.First(cl => cl.IdCreerpar == Int32.Parse(r[9].ToString())).UserCreate,
                            ClienOk = bool.Parse(r[24].ToString()),
                            BackGround = r[23].ToString(),
                            ClientbackGround = r[25].ToString(),
                            NumeroLigne = Int32.Parse(r[26].ToString()),
                            TotalTTC = double.Parse(r[27].ToString()),
                            icon = int.Parse(r[28].ToString()),
                            IdSite = int.Parse(r[29].ToString())



                        };
                        newliste.Add(fm);
                    }
                    FacturesListe = newliste;
                    FilterByCliFacturesListe = newliste;
                   
                }

            }
            else
            {
                loadDatas(idsociete);

            }
        }

        void filterByDate(DateTime  values)
        {


            if (FacturesListe != null || FacturesListe.Count > 0)
            {
                newTable.Clear();

                if (string.IsNullOrEmpty(FiltertexteFacture))
                {
                    if (string.IsNullOrEmpty(Filtertexte))
                    {
                        // si pas de filtre
                        CommonModule.AdDatasInDatatable(CacheFacturesListeRecherche, newTable);
                    }
                    else
                    {
                        //si filtre deja par client uniquement
                        CommonModule.AdDatasInDatatable(FilterByCliFacturesListe, newTable);
                    }
                }
                else
                {
                    // filtre par num facture
                    if (!string.IsNullOrEmpty(Filtertexte))
                        CommonModule.AdDatasInDatatable(FilterByCliFacturesListe, newTable);
                    else CommonModule.AdDatasInDatatable(FilterFacturesListe, newTable);


                }
                

                DataRow[] nlignes = newTable.Select(string.Format("datecreation like'%{0}%' ", values.ToShortDateString ()));
                DataTable filterDatatable = newTable.Clone();
                foreach (DataRow rows in nlignes)
                    filterDatatable.ImportRow(rows);

                FactureModel fm;
                ObservableCollection<FactureModel> newliste = new ObservableCollection<FactureModel>();

                foreach (DataRow r in filterDatatable.Rows)
                {
                    fm = new FactureModel()
                    {
                        IdFacture = Int64.Parse(r[0].ToString()),
                        NumeroFacture = r[1].ToString(),
                        MoisPrestation = DateTime.Parse(r[2].ToString()),
                        IdObjetFacture = Int32.Parse(r[3].ToString()),

                        IdClient = Int32.Parse(r[4].ToString()),
                        // Profile = CacheUsers.First(f => f.IdProfile == int.Parse(r[7].ToString())).Profile,
                        IdTaxe = int.Parse(r[5].ToString()),
                        IdDevise = Int32.Parse(r[6].ToString()),
                        IdStatut = Int32.Parse(r[7].ToString()),
                        IdModePaiement = int.Parse(r[8].ToString()),
                        IdCreerpar = Int32.Parse(r[9].ToString()),
                        IdModifierPar = Int32.Parse(r[10].ToString()),
                        DateCloture = r[12] != DBNull.Value ? DateTime.Parse(r[12].ToString()) : (DateTime?)null,
                        DateEcheance = r[13] != DBNull.Value ? DateTime.Parse(r[13].ToString()) : (DateTime?)null,
                        DateCreation = r[14] != DBNull.Value ? DateTime.Parse(r[14].ToString()) : (DateTime?)null,
                        CurrentClient = CacheFacturesListe.First(cl => cl.IdClient == Int32.Parse(r[4].ToString())).CurrentClient,
                        CurrentDevise = CacheFacturesListe.First(cl => cl.IdDevise == Int32.Parse(r[6].ToString())).CurrentDevise,

                        CurrentModePaiement = CacheFacturesListe.First(cl => cl.IdModePaiement == Int32.Parse(r[8].ToString())).CurrentModePaiement,
                        CurrentObjetFacture = CacheFacturesListe.First(cl => cl.IdObjetFacture == Int32.Parse(r[3].ToString())).CurrentObjetFacture,
                        CurrentStatut = CacheFacturesListe.First(cl => cl.IdStatut == Int32.Parse(r[7].ToString())).CurrentStatut,
                        CurrentTaxe = CacheFacturesListe.First(cl => cl.IdTaxe == Int32.Parse(r[5].ToString())).CurrentTaxe,
                        UserCreate = CacheFacturesListe.First(cl => cl.IdCreerpar == Int32.Parse(r[9].ToString())).UserCreate,
                        ClienOk = bool.Parse(r[24].ToString()),
                        BackGround = r[23].ToString(),
                        ClientbackGround = r[25].ToString(),
                        NumeroLigne = Int32.Parse(r[26].ToString()),
                        TotalTTC = double.Parse(r[27].ToString()),
                        icon = int.Parse(r[28].ToString()),
                        IdSite = int.Parse(r[29].ToString())


                    };
                    newliste.Add(fm);
                }
                FacturesListe = newliste;
                FilterByDateFacturesListe = newliste;

            }
            else
            {
                loadDatas(idsociete);

            }
        }



        void filterByFactureNum(string values)
        {


            if (FacturesListe != null || FacturesListe.Count > 0)
            {
                newTable.Clear();

                if (string.IsNullOrEmpty(Filtertexte))
                {
                    if (FilterDateDebut == null)
                    {
                        // si pas de filtre facture e pas de filtre date donc filtre par client uniquement
                        CommonModule.AdDatasInDatatable(CacheFacturesListeRecherche, newTable);
                    }
                    else
                        CommonModule.AdDatasInDatatable(FilterByDateFacturesListe, newTable);
                }
                else
                {
                    if (FilterDateDebut == null)
                    {
                        CommonModule.AdDatasInDatatable(FilterByCliFacturesListe, newTable);
                    }
                    else
                        CommonModule.AdDatasInDatatable(FilterByDateFacturesListe, newTable);
                   

                }

                DataRow[] nlignes = newTable.Select(string.Format("numfact like '{0}%'", values.Trim()));
                if (nlignes.Length == 0)
                    nlignes = newTable.Select(string.Format("numfact like '%{0}'", values.Trim()));
                 if (nlignes.Length == 0)
                    nlignes = newTable.Select(string.Format("numfact like '%{0}%'", values.Trim()));
                
                
                DataTable filterDatatable = newTable.Clone();
                foreach (DataRow rows in nlignes)
                    filterDatatable.ImportRow(rows);

                FactureModel fm;
                ObservableCollection<FactureModel> newliste = new ObservableCollection<FactureModel>();

                foreach (DataRow r in filterDatatable.Rows)
                {
                    fm = new FactureModel()
                    {
                        IdFacture = Int64.Parse(r[0].ToString()),
                        NumeroFacture = r[1].ToString(),
                        MoisPrestation = DateTime.Parse(r[2].ToString()),
                        IdObjetFacture = Int32.Parse(r[3].ToString()),

                        IdClient = Int32.Parse(r[4].ToString()),
                        // Profile = CacheUsers.First(f => f.IdProfile == int.Parse(r[7].ToString())).Profile,
                        IdTaxe = int.Parse(r[5].ToString()),
                        IdDevise = Int32.Parse(r[6].ToString()),
                        IdStatut = Int32.Parse(r[7].ToString()),
                        IdModePaiement = int.Parse(r[8].ToString()),
                        IdCreerpar = Int32.Parse(r[9].ToString()),
                        IdModifierPar = Int32.Parse(r[10].ToString()),
                        DateCloture = r[12] != DBNull.Value ? DateTime.Parse(r[12].ToString()) : (DateTime?)null,
                        DateEcheance = r[13] != DBNull.Value ? DateTime.Parse(r[13].ToString()) : (DateTime?)null,
                        DateCreation = r[14] != DBNull.Value ? DateTime.Parse(r[14].ToString()) : (DateTime?)null,
                        CurrentClient = CacheFacturesListe.First(cl => cl.IdClient == Int32.Parse(r[4].ToString())).CurrentClient,
                        CurrentDevise = CacheFacturesListe.First(cl => cl.IdDevise == Int32.Parse(r[6].ToString())).CurrentDevise,

                        CurrentModePaiement = CacheFacturesListe.First(cl => cl.IdModePaiement == Int32.Parse(r[8].ToString())).CurrentModePaiement,
                        CurrentObjetFacture = CacheFacturesListe.First(cl => cl.IdObjetFacture == Int32.Parse(r[3].ToString())).CurrentObjetFacture,
                        CurrentStatut = CacheFacturesListe.First(cl => cl.IdStatut == Int32.Parse(r[7].ToString())).CurrentStatut,
                        CurrentTaxe = CacheFacturesListe.First(cl => cl.IdTaxe == Int32.Parse(r[5].ToString())).CurrentTaxe,
                        UserCreate = CacheFacturesListe.First(cl => cl.IdCreerpar == Int32.Parse(r[9].ToString())).UserCreate ,
                        ClienOk = bool.Parse(r[24].ToString()),
                        BackGround = r[23].ToString(), ClientbackGround =r[25].ToString (),
                        NumeroLigne = Int32.Parse(r[26].ToString()),
                        TotalTTC = double.Parse(r[27].ToString()),
                        icon = int.Parse(r[28].ToString()),
                        IdSite = int.Parse(r[29].ToString())

                    };
                    newliste.Add(fm);
                }
                FacturesListe = newliste;
                FilterFacturesListe = newliste;

            }
            else
            {
                loadDatas(idsociete);

            }
        }

        #endregion

        #endregion

    }

    public class ClientChecked
    {
        public bool IsClientChecked { get; set; }
        public int idClient { get; set; }
        public string nomClient { get; set; }
    }
}
