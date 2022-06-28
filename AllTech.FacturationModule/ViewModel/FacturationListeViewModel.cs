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
using AllTech.FrameWork.Utils;
using Multilingue.Resources;

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
        int maxPagging;

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
        private RelayCommand serchByNumCommand;
        private RelayCommand serchByDateArchiveCommand;
      
        private RelayCommand imprimerCommand;
        private RelayCommand imprimerToutCommand;
        private RelayCommand exportCommand;
        private RelayCommand exportALLCommand;
        private RelayCommand validationCommand;
        private RelayCommand sortieCommand;
        private RelayCommand suspensionCommand;
        private RelayCommand nonValalableCommand;
        private RelayCommand refreshLinesCommand;
        private RelayCommand contexMenuPrintCommand;
        private RelayCommand contexEditCommand;
        private RelayCommand contexDeleteCommand;
        private RelayCommand contexStatusvalideCommand;


        private RelayCommand selectAllCommand;
        private RelayCommand deselectAllCommand;

       public  FactureModel factureService;
        ObservableCollection<FactureModel> _facturesListe;
        ObservableCollection<FactureModel> _cacheFacturesListe;
        ObservableCollection<FactureModel> _cacheFacturesListeRecherche;
        ObservableCollection<FactureModel> _refreshcacheFacturesListeRecherche;
        DataTable _dtblCacheListeFacture;
        DataTable _olddtblCacheListeFacture;

      
        ObservableCollection<FactureModel> _filterFacturesListe;
        ObservableCollection<FactureModel> _filterByCliFacturesListe;
        ObservableCollection<FactureModel> _filterByDateFacturesListe;
        ObservableCollection<FactureModel> listeTeste = new ObservableCollection<FactureModel>();

        ObservableCollection<ClientModel> clientNonEncorefactures;
        ClientModel clientnonencoreFactSelect;

      
    
        FactureModel _factureSelected;
        FactureModel _factureActived;
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
        ObservableCollection<ClientModel> listeClientFactureNomFacture;

     
        ClientModel clientNonFactureSelect;


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
        bool overviewResumVisible;
        bool visibleWaitingValidate;
        bool visibleWaitingPrint;

     

      

        int indexSelected;
        int idsociete = 0;
        int nbrepages;
        int numeroPageDebut;
        bool isClientChecked;
        DateTime? dateDebut;
        DateTime? dateFin;

        bool islineItemChecked;

        DataTable dtTableFactures;
        DataRow factureRowSelect;
        Facturation_List fliste = null;
        OverviewFactureModel overviewService;
        List<OverviewFactureModel> overviews;
        OverviewFactureModel orverviewSelecte;
        bool isVisiblehistoric;
        bool hstFactireSupprimer;
        bool hstFactureNormal;

        //archives
        bool tabItemArchiveVisible;
        bool archiveActivate;
        string tabItemCrnText;
        PeriodeDateArch periodeSelected;
        List<PeriodeDateArch> periodeListes;
        bool isCheckArchiveFactures;
        bool isEnableCmbPeriodearchive;
        int indexArchivHisto;
        bool IsArchLoading = false;
        string messageDisplay;
        DateTime? dateArchDebut;
        DateTime? dateArchFin;
        string ctxmenuEnbaleBackGround;
        string numerofacture;
      
     
       
       
      
        #endregion

        #region CONSTRUCTORS


        public FacturationListeViewModel(Facturation_List controls)
        {
            fliste = controls;
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
            overviewService = new OverviewFactureModel();
            EventRefreshGridHistoric.EventRefreshList+=new EventRefreshGridHistoric.MyEventHandler(EventRefreshGridHistoric_EventRefreshList);
            TabItemCrnText = string.Empty;
            if (UserConnected != null)
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("historic"));
            else CurrentDroit = new DroitModel ();

            if (CacheDatas.ui_currentdroitFistoricFacturesInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("historic")) ?? new DroitModel();
                CacheDatas.ui_currentdroitFistoricFacturesInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitFistoricFacturesInterface;

           // maxlignes = ParametersDatabase.PaginationHtrc !=string .Empty ?int .Parse ( ParametersDatabase.PaginationHtrc):10;
            if ( CurrentDroit.Lecture || CurrentDroit.Developpeur)
            {
                loadDatas(societeCourante.IdSociete);
                loadWright();
            }
        
          

          
           //testeClientsNonFacturées();
        }

        #endregion

        #region PROPERTIES

        #region COMMON

        public string Numerofacture
        {
            get { return numerofacture; }
            set
            {
                numerofacture = value;
                OnPropertyChanged("Numerofacture");
            }
        }


        public bool OverviewResumVisible
        {
            get { return overviewResumVisible; }
            set { overviewResumVisible = value;
            OnPropertyChanged("OverviewResumVisible");
            }
        }

        public string CtxmenuEnbaleBackGround
        {
            get { return ctxmenuEnbaleBackGround; }
            set { ctxmenuEnbaleBackGround = value;
            OnPropertyChanged("CtxmenuEnbaleBackGround");
            }
        }

        public ObservableCollection<ClientModel> ListeClientFactureNomFacture
        {
            get { return listeClientFactureNomFacture; }
            set { listeClientFactureNomFacture = value;
            OnPropertyChanged("ListeClientFactureNomFacture");
            }
        }

        public ClientModel ClientNonFactureSelect
        {
            get { return clientNonFactureSelect; }
            set { clientNonFactureSelect = value;
            OnPropertyChanged("ClientNonFactureSelect");
            }
        }

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

        #region ARchives


          public DateTime? DateArchDebut
          {
              get { return dateArchDebut; }
              set { dateArchDebut = value;
              this.OnPropertyChanged("DateArchDebut");
              }
          }


          public DateTime? DateArchFin
          {
              get { return dateArchFin; }
              set { dateArchFin = value;
              this.OnPropertyChanged("DateArchFin");
              }
          }

          public string MessageDisplay
          {
              get { return messageDisplay; }
              set { messageDisplay = value;
              this.OnPropertyChanged("MessageDisplay");
              }
          }

          public int IndexArchivHisto
          {
              get { return indexArchivHisto; }
              set { indexArchivHisto = value;
              this.OnPropertyChanged("IndexArchivHisto");
              }
          }


          public bool IsEnableCmbPeriodearchive
          {
              get { return isEnableCmbPeriodearchive; }
              set { isEnableCmbPeriodearchive = value;
              this.OnPropertyChanged("IsEnableCmbPeriodearchive");
              }
          }

          public bool IsCheckArchiveFactures
          {
              get { return isCheckArchiveFactures; }
              set {
                  if (value)
                  {
                      IsEnableCmbPeriodearchive = true;
                      GlobalDatas.IsArchiveSelected = true;
                      LoadPeriodeArchives();
                      IndexArchivHisto=-1;
                      IsArchLoading = false;
                      
                  }
                  else
                  {
                      IsEnableCmbPeriodearchive = false;
                      GlobalDatas.IsArchiveSelected = false;
                      PeriodeSelected = null;
                      loadDatas(societeCourante.IdSociete);
                      IsArchLoading = false;
                      MessageDisplay = string.Empty;
                  }
                  isCheckArchiveFactures = value;
              this.OnPropertyChanged("IsCheckArchiveFactures");
              }
          }

          public List<PeriodeDateArch> PeriodeListes
          {
              get { return periodeListes; }
              set
              {
                  periodeListes = value;
                  this.OnPropertyChanged("PeriodeListes");
              }
          }

          public PeriodeDateArch PeriodeSelected
          {
              get { return periodeSelected; }
              set
              {
                  if (value != null)
                  {
                      //if (IsArchLoading)
                      //{
                      if (!value.Annee.Contains("."))
                      {
                          periodeSelected = value;
                          if (value != null)
                          {
                              DateSelected = DateTime.Parse("01/01/" + value.Annee);
                              loadHistoricArchive();
                              MessageDisplay = string.Format("{0} {1}", ConstStrings.Get("MsgArchive_Display"), value.Annee);
                          }
                      }
                      else
                      {
                        //PeriodeDateArch periodeFausse=  PeriodeListes.FirstOrDefault(r => r.Annee.Contains("."));
                        //if (periodeFausse!=null )
                        //PeriodeListes.Remove(periodeFausse);
                      }
                      //}
                      //else
                      //{
                      //    IsArchLoading = true;
                      //    periodeSelected = null;
                      //}
                  }

                  this.OnPropertyChanged("PeriodeSelected");
              }
          }

          public bool ArchiveActivate
          {
              get { return archiveActivate; }
              set {
                  if (value != null)
                  {
                      if (value)
                      {
                          GlobalDatas.IsArchiveSelected = true;
                      }
                      else
                      {
                          GlobalDatas.IsArchiveSelected = false;
                          loadDatas(societeCourante.IdSociete);
                      }
                  }
                  archiveActivate = value;
                  OnPropertyChanged("ArchiveActivate");
              }
          }

          public bool TabItemArchiveVisible
          {
              get { return tabItemArchiveVisible; }
              set
              {
                  tabItemArchiveVisible = value;
                  OnPropertyChanged("TabItemArchiveVisible");
              }
          }

          public string TabItemCrnText
          {
              get { return tabItemCrnText; }
              set { tabItemCrnText = value;
              OnPropertyChanged("TabItemCrnText");
              }
          }

          #endregion

          public bool VisibleWaitingPrint
          {
              get { return visibleWaitingPrint; }
              set { visibleWaitingPrint = value;
              OnPropertyChanged("VisibleWaitingPrint");
              }
          }

          public bool VisibleWaitingValidate
          {
              get { return visibleWaitingValidate; }
              set { visibleWaitingValidate = value;
              OnPropertyChanged("VisibleWaitingValidate");
              }
          }

          public bool IsVisiblehistoric
          {
              get { return isVisiblehistoric; }
              set { isVisiblehistoric = value;
              OnPropertyChanged("IsVisiblehistoric");
              }
          }


          public bool HstFactureNormal
          {
              get { return hstFactureNormal; }
              set { hstFactureNormal = value;
                   loadDatas(societeCourante.IdSociete);
              OnPropertyChanged("HstFactureNormal");
              }
          }


          public bool HstFactireSupprimer
          {
              get { return hstFactireSupprimer; }
              set { hstFactireSupprimer = value;
              FacturesListe = factureService.FACTURE_GETLISTE_NEW(societeCourante.IdSociete, true);
              CacheDatas.Listefactures = FacturesListe;
              OnPropertyChanged("HstFactireSupprimer");
              }
          }

          public FactureModel FactureActived
          {
              get { return _factureActived; }
              set { _factureActived = value;
              OnPropertyChanged("FactureActived");
              }
          }

          public List<OverviewFactureModel> Overviews
          {
              get { return overviews; }
              set { overviews = value;
              OnPropertyChanged("Overviews");
              }
          }


          public OverviewFactureModel OrverviewSelecte
          {
              get { return orverviewSelecte; }
              set { orverviewSelecte = value;
              OnPropertyChanged("OrverviewSelecte");
              }
          }


          public DataTable OlddtblCacheListeFacture
          {
              get { return _olddtblCacheListeFacture; }
              set { _olddtblCacheListeFacture = value;
              OnPropertyChanged("OlddtblCacheListeFacture");
              }
          }

          public DataTable DtblCacheListeFacture
          {
              get { return _dtblCacheListeFacture; }
              set { _dtblCacheListeFacture = value;
              OnPropertyChanged("DtblCacheListeFacture");
              }
          }
  

          public DataRow FactureRowSelect
          {
              get { return factureRowSelect; }
              set { factureRowSelect = value;
              OnPropertyChanged("FactureRowSelect");
              }
          }

          public DataTable DtTableFactures
          {
              get { return dtTableFactures; }
              set { dtTableFactures = value;
              OnPropertyChanged("DtTableFactures");
              }
          }

          public int MaxPagging
          {
              get { return maxPagging; }
              set { maxPagging = value;
              OnPropertyChanged("MaxPagging");
              }
          }


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

                      DtTableFactures = DtblCacheListeFacture;
                     // loadDatas(idsociete);
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
                            
                                FacturesListe = null;
                                CacheFacturesListe = null;

                                FacturesListe = value;
                                CacheFacturesListe = value;
                                CacheFacturesListeRecherche = value;
                               // newTable = CommonModule.SetDataTableFacture();

                                //TotalLignes = CacheFacturesListe.Count == 0 ? 0 : CacheFacturesListe.Count;
                                //LigneDebut = CacheFacturesListe.Count == 0 ? 0 : 1;
                                //LigneFin = (int)ligneCourante;
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
                              
                            }
                       
                       
                    
             // }


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

          public ICommand DeleteCommand
          {
              get
              {
                  if (this.deleteCommand == null)
                  {
                      this.deleteCommand = new RelayCommand(param => this.canDELETE(), param => this.canExecuteDELETE());
                  }
                  return this.deleteCommand;
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
        //

        public RelayCommand SerchByDateArchiveCommand
        {
            get
            {
                if (this.serchByDateArchiveCommand == null)
                {
                    this.serchByDateArchiveCommand = new RelayCommand(param => this.canSearchByDateArch(param), param => this.canExecuteSearchBydateArch());
                }
                return this.serchByDateArchiveCommand;
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

        //

        public RelayCommand SearchByNumCommand
        {
            get
            {
                if (this.serchByNumCommand == null)
                {
                    this.serchByNumCommand = new RelayCommand(param => this.canSearchByNumFact(), param => this.canExecuteSearchByNumFact());
                }
                return this.serchByNumCommand;
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
                    this.imprimerCommand = new RelayCommand(param => this.canImprimeOnly(), param => this.canImprimeExecuteNext());
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

        public RelayCommand RefreshLinesCommand
        {
            get
            {
                if (this.refreshLinesCommand == null)
                {
                    this.refreshLinesCommand = new RelayCommand(param => this.canRfresh(), param => canExecuteRefresh());
                }
                return this.refreshLinesCommand;
            }

        }

        #region ContexteMenu


        public RelayCommand ContexMenuPrintCommand
        {
            get
            {
                if (this.contexMenuPrintCommand == null)
                {
                    this.contexMenuPrintCommand = new RelayCommand(param => this.canPrintContext(), param => canExecutePrintContext());
                }
                return this.contexMenuPrintCommand;
            }

        }


        //public RelayCommand ContexEditCommand
        //{
        //    get
        //    {
        //        if (this.contexEditCommand == null)
        //        {
        //            this.contexEditCommand = new RelayCommand(param => this.canRfresh(), param => canExecuteRefresh());
        //        }
        //        return this.contexEditCommand;
        //    }

        //}



        public RelayCommand ContexDeleteCommand
        {
            get
            {
                if (this.contexDeleteCommand == null)
                {
                    this.contexDeleteCommand = new RelayCommand(param => this.canDELETE(), param => canExecuteDELETE());
                }
                return this.contexDeleteCommand;
            }

        }

        public RelayCommand ContexStatusvalideCommand
        {
            get
            {
                if (this.contexStatusvalideCommand == null)
                {
                    this.contexStatusvalideCommand = new RelayCommand(param => this.canRfresh(), param => canExecuteRefresh());
                }
                return this.contexStatusvalideCommand;
            }

        }
          
        #endregion

        #endregion

        #region METHODS

      

        #region Region Load

        void LoadingIsArchiveExist()
        {
            if (GlobalDatas.IsArchiveDatas)
            {
                if (CurrentDroit.Super || CurrentDroit.Proprietaire || CurrentDroit.Developpeur || CurrentDroit.ArchiveView)
                {
                    TabItemArchiveVisible = true;
                    TabItemCrnText = "HST fact";
                }
            }
        }
        void loadWright()
        {
            if (CurrentDroit.Super || CurrentDroit.Proprietaire  || CurrentDroit.Developpeur)
            {
                IsVisiblehistoric = true;
               
                if (GlobalDatas.listeCompany !=null )
                    if (GlobalDatas.listeCompany.Count > 1)
                    {
                        SocieteListe = GlobalDatas.listeCompany;
                        CmbStevisible = true;
                       
                    }
            }

           
        }

        void loadDatas(int idSite)
        {
            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = false;
            fliste.StartStopWait(true);

            worker.DoWork += (o, args) =>
            {
                try
                {
                    ligneCourante = 0;
                    if (societeCourante != null)
                    {
                        if (GlobalDatas.IsArchiveSelected)
                        {
                            FacturesListe = CacheDatas.Listefactures;
                        }
                        else
                        {
                            FacturesListe = factureService.FACTURE_GETLISTE_NEW(idSite, false);
                         //   CacheDatas.Listefactures = FacturesListe;
                            // Utils.logUserActions("<-- interface historique chargement liste factures du mois courant--   ", "");
                            if (!GlobalDatas.IsArchiveDatas)
                               GlobalDatas.IsArchiveDatas = FactureModel.IsExist_Facture_Archive(idSite);
                           
                        }
                       

                      

                    }

                }
                catch (Exception ex)
                {
                    Utils.logUserActions("<-- interface historique Erreure chargement liste factures du mois courant--   ", "");
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LIST FACTURES";
                }

                try
                {
                    ListeClientFacture = clientService.CLIENT_GETLISTE_FACTURER(societeCourante.IdSociete, 0, 0, DateTime.Now, DateTime.Now);

                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LIST CLIENTS FACTURER";
                }


                //
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
                    fliste.StartStopWait(false);
                    Utils.logUserActions("<-- UI HST Erreur lors du chargement    " + args.Result.ToString(), "");
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                    fliste.StartStopWait(false);

                    LoadingIsArchiveExist();
                  
                    // Utils.logUserActions("<-- UI HST factures  fin chargement liste factures du mois courant--   ", "");
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();



        }

        void loadDRefresh(int idSite)
        {
            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = false;
            fliste.StartStopWait(true);

            worker.DoWork += (o, args) =>
            {
                try
                {
                    ligneCourante = 0;
                        
                        FacturesListe = factureService.FACTURE_GETLISTE_NEW(idSite, false);
                        ListeClientFacture = clientService.CLIENT_GETLISTE_FACTURER(societeCourante.IdSociete, 0, 0, DateTime.Now, DateTime.Now);
                        CacheDatas.Listefactures = FacturesListe;
                      
                        IndexArchivHisto = -1;
                        MessageDisplay = string.Empty;
                    

                }
                catch (Exception ex)
                {
                    Utils.logUserActions("<-- interface historique Erreure chargement liste factures du mois courant--   ", "");
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LIST FACTURES";
                }

                try
                {
                    if (CacheDatas.ui_Hst_factureClient == null)
                    {
                        ListeClientFacture = clientService.CLIENT_GETLISTE_FACTURER(societeCourante.IdSociete, 0, 0, DateTime.Now, DateTime.Now);
                        CacheDatas.ui_Hst_factureClient = ListeClientFacture;
                    }
                    else ListeClientFacture = CacheDatas.ui_Hst_factureClient;

                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT LIST CLIENTS FACTURER";
                }


                //try
                //{
                //    Overviews = overviewService.GetOverview(UserConnected.Id);
                //}
                //catch (Exception ex)
                //{
                //    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT OVERVIEW FACTURE";
                //}



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
                    fliste.StartStopWait(false);
                    Utils.logUserActions("<-- UI HST Erreur lors du chargement    " + args.Result.ToString(), "");
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                    fliste.StartStopWait(false);
                    IsCheckArchiveFactures = false;
                    // Utils.logUserActions("<-- UI HST factures  fin chargement liste factures du mois courant--   ", "");
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();



        }

        void loadHistoricArchive()
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = false;
            fliste.StartStopWait(true);
            worker.DoWork += (o, args) =>
          {
              try
              {
                  FacturesListe = factureService.GetAll_FACTURE_Archive(societeCourante.IdSociete, DateSelected);

                  ListeClientFacture = clientService.CLIENT_GETLISTE_FACTURER(societeCourante.IdSociete, 0, 1, DateSelected, DateTime.Now);
              }
              catch (Exception ex)
              {
                  args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMEN ARCHIVES";
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
                  fliste.StartStopWait(false);
              }
              else
              {
                  this.MouseCursor = null;
                  this.IsBusy = false;
                  fliste.StartStopWait(false);
              }
          };

            worker.RunWorkerAsync();
        }

        void LoadPeriodeArchives()
        {
            try
            {
                DataTable table= FactureModel.Facture_Periode_Archive(societeCourante.IdSociete);
                List<PeriodeDateArch> liste=new List<PeriodeDateArch> ();
                liste.Add(new PeriodeDateArch { Annee="....." });
                foreach(DataRow row in table.Rows)
                    liste.Add(new PeriodeDateArch{ Annee=row["annee"] !=DBNull.Value? Convert.ToString( row["annee"]):string.Empty});
                PeriodeListes = liste;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title ="CHARGEMENT PERIODE ARCHIVE";
                view.ViewModel.Message =ex.Message;
                view.ShowDialog();
            }
        }

        private void  canShowdetailFacture(){
            int val = 0;
        }


        #endregion

        #region Others
        
    

        void canSelectAll()
        {
            var newList = FacturesListe;

            foreach (var facture in newList)
                facture.IsCheck = true;
            FacturesListe = null;
            FacturesListe = newList;

           // for (int i = 0; i <= FacturesListe.Count - 1; i++)
               // FacturesListe[i].IsCheck= true;
           // DtTableFactures.AcceptChanges();
         


        }

        bool canExecuteSelectAll()
        {
            bool values = false;
            if (FacturesListe != null)
            {
                if (FacturesListe.Count>0)
                {
                    if (FacturesListe.FirstOrDefault(r => (r.IsCheck != null ? r.IsCheck : false) == false) != null)
                        values = true;
                }
            }
            return values;
        }

        void canDeselectAll()
        {
            var newList = FacturesListe;

            foreach (var facture in newList)
                facture.IsCheck = false;
            FacturesListe = null;
            FacturesListe = newList;
            //for (int i = 0; i <= FacturesListe.Count - 1; i++)
            //    FacturesListe[i].IsCheck = false;
           // DtTableFactures.AcceptChanges();
        }

        bool canExecuteDeselectAll()
        {
            bool values = false;
            if (FacturesListe != null)
            {
                if (FacturesListe.FirstOrDefault(r => (r.IsCheck ) == true) != null)
                    values = true;
            }
            return values;
        }
        #endregion

        #region Region EDITE FACTURE



        private void canEdit()
        {

            //if (FactureActived.IdStatut <= 14002)
            //{
                NewFactureEdition edition = new NewFactureEdition(FactureActived.IdFacture, FactureActived.IdStatut, null);
                edition.Owner = Application.Current.MainWindow;
                edition.ShowDialog();
                if (edition.isOperation)
                {
                    FacturesListe = factureService.FACTURE_GETLISTE_NEW(societeCourante.IdSociete, false);

                }
           // }
           // else
           // {
               // canPrintContext();
            //}

            FactureActived = null;
        }

        bool canExecuteEdit()
        {
            bool values = false;

          

            if ( CurrentDroit.Edition || CurrentDroit.Developpeur)
                {
                    if (FactureActived != null && FactureActived.IdStatut <= 14002)
                    {
                            values = true;
                    }
                }
            //}
            return values;
        }
        private void canClose()
        {
            Communicator ctr = new Communicator();
            ctr.contentVue = "fhst";
            EventArgs e1 = new EventArgs();
            ctr.OnChangeText(e1);

        }

        #endregion


        #region REGion Suppression


        void canDELETE(){
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION  SUPPRESSION";
                        messageBox.ViewModel.Message = "Confirmez la Suppression ?";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            factureService.FACTURE_DELETE(FactureActived.IdFacture, FactureActived.IdStatut==14007?1:0, societeCourante.IdSociete, UserConnected.Id);

                            FacturesListe = factureService.FACTURE_GETLISTE_NEW(societeCourante.IdSociete,false);
                          
                            FactureActived = null;
                        }
        }
        bool canExecuteDELETE()
        {
            bool values = false;
           // CtxmenuEnbaleBackGround = "Transparent";
           
                if ( CurrentDroit.Suppression  || CurrentDroit.Developpeur)
                {
                    if (FactureActived != null && FactureActived.IdFacture>0)
                    {
                        if (FactureActived.IdStatut <= 14002 || FactureActived.IdStatut==14007)
                            values =  true ;
                       
                        
                    }
                }
         

            return values;
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

                //filtre client
                UpdateFiltreClient(FacturesListe);
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

                // filtre client
                UpdateFiltreClient(FacturesListe);
               
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

                //filtre client
                UpdateFiltreClient(FacturesListe);

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

                // filtre client
                UpdateFiltreClient(FacturesListe);
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

        private void canSearchByDateArch(object param)
        {

            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = false;
            Int32 idClient = 0;
            if (ClientFactireselect != null)
                idClient = ClientFactireselect.IdClient == 0 ? -1 : ClientFactireselect.IdClient;
            else idClient = -1;

            if (DateArchDebut.HasValue && DateArchFin.HasValue)
            {
                if (DateArchDebut.Value.Year != int.Parse(PeriodeSelected.Annee))
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "";
                    view.ViewModel.Message = "l'intervalle date doit  respecter la période [" + PeriodeSelected.Annee +" ]";
                    view.ShowDialog();
                    return;
                }

            }
            fliste.StartStopWait(true);
            worker.DoWork += (o, args) =>
            {
                try
                {
                    FacturesListe = factureService.SEARCH_LIST_FACTURE_Archive(societeCourante.IdSociete, DateArchDebut.Value, DateArchFin.Value, idClient);
                    ListeClientFacture = clientService.CLIENT_GETLISTE_FACTURER(societeCourante.IdSociete, 1, 1, DateArchDebut.Value, DateArchFin.Value);
                    CacheDatas.ui_Hst_factureClient = ListeClientFacture;

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
                    Utils.logUserActions("<--UI HSt factures -> Erreur chargement  par " + UserConnected.Loggin + "" + args.Result.ToString(), "");

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
        bool canExecuteSearchBydateArch()
        {
            if (CurrentDroit.Lecture || CurrentDroit.Developpeur)
            {
                return (DateArchDebut.HasValue && DateArchFin.HasValue) ? true : false;
            }
            else return false;
        }


        void canSearchByNumFact()
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = false;
            fliste.StartStopWait(true);
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (GlobalDatas.IsArchiveDatas)
                    {
                        FacturesListe = factureService.FACTURE_SEARCH_BYFACTURE(Numerofacture, "OUI");
                        ListeClientFacture = null;
                    }
                    else
                    {
                        FacturesListe = factureService.FACTURE_SEARCH_BYFACTURE(Numerofacture, "NON");
                        ListeClientFacture = null;
                    }

                    // ListeClientFacture = clientService.CLIENT_GETLISTE_FACTURER(societeCourante.IdSociete, 0, 1, DateSelected, DateTime.Now);
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMEN FACTURE";
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
                    fliste.StartStopWait(false);
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    fliste.StartStopWait(false);
                }
            };

            worker.RunWorkerAsync();


        }

        bool canExecuteSearchByNumFact()
        {
            if (CurrentDroit.Lecture || CurrentDroit.Developpeur)
                return true;
            return false;
        }



        private void canSearchByDate(object param)
        {

            this.MouseCursor = Cursors.Wait;
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = false;
            Int32 idClient = 0;
            if (ClientFactireselect != null)
                idClient = ClientFactireselect.IdClient == 0 ? -1 : ClientFactireselect.IdClient;
            else idClient = -1;
            fliste.StartStopWait(true);
            worker.DoWork += (o, args) =>
            {
                try
                {
                   FacturesListe = factureService.FACTURE_SEARCH(societeCourante.IdSociete, DateDebut.Value, DateFin.Value, idClient);

                   ListeClientFacture = clientService.CLIENT_GETLISTE_FACTURER(societeCourante.IdSociete, 1, 0, DateDebut.Value, DateFin.Value);
                   CacheDatas.ui_Hst_factureClient = ListeClientFacture;
                   

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
                    Utils.logUserActions("<--UI HSt factures -> Erreur chargement  par " + UserConnected.Loggin + "" + args.Result.ToString(), "");

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
        bool canExecuteSearchBydate()
        {
            if (CurrentDroit.Lecture || CurrentDroit.Developpeur)
            {
                return DateDebut.HasValue == true ? (DateFin.HasValue == true ? true : false) : false;
            }
            else return false;
        }

        #endregion

        private void canNewFacture()
        {
            //WFacturationModal_vie vf = new WFacturationModal_vie();
           // WFacturationModal_vie logginView = _container.Resolve<WFacturationModal_vie>();
            //logginView.Owner = Application.Current.MainWindow;
            //GlobalDatas.currentFacture =null;
           // logginView.ShowDialog();
          
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
            int ligneSelected =0;
            //try
            //{
               
                if (FacturesListe != null)
                    ligneSelected = FacturesListe.Count(f => f.IsCheck == true);
                else return;

                if (ligneSelected == 0)
                {
                    MessageBox.Show("Pas de Facture sélectionnées Pour l'impression");

                }
                else
                {

                    BackgroundWorker worker = new BackgroundWorker();
                    VisibleWaitingPrint = true;
                    worker.DoWork += (o, args) =>
                {

                    IsBusy = true;
                    this.MouseCursor = Cursors.Wait;
                    try
                    {
                        System.Drawing.Printing.PrintDocument printDocument = null;
                        reportClientPartiel rptPartiel = null;
                        //  reportWithoutPorata rptNonProrata = null;
                        NewReportExonere rptProrata = null;
                        List<FactureModel> FacturesListeFilter = FacturesListe.ToList().FindAll(f => f.IsCheck == true);


                        foreach (FactureModel facture in FacturesListeFilter)
                        {
                            SetdataTaSet();
                            SetNewdataTaSet();
                            tclient = null;
                            tableSociete = null;
                            tablePiedPage = null;
                            tableLibelle = null;
                            tablefacture = null;
                            tableLignefactures = null;

                            // mode = facture.CurrentClient.Exonerere.CourtDesc;
                            Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "debut chargement  impression en lot  facture " + FactureSelected.NumeroFacture), "");
                            if (GlobalDatas.IsArchiveSelected)
                            {
                                DataSet tablefactures = ReportModel.GetFacture_archive(facture.IdFacture, facture.IdSite);

                                DataTable tclients = ReportModel.GetReportClientArchive(facture.IdClient, facture.IdFacture);
                                DataTable tableSocietes = ReportModel.GetReportSocieteArchive(facture.IdSite);
                                DataTable tablePiedPages = ReportModel.GetReporPiedPage(facture.IdSite);
                                DataTable tableLibelles = ReportModel.GetLibelleArchive(facture.IdClientLangue);
                                tableLignefactures = tablefactures.Tables[1];
                                tablefacture = tablefactures.Tables[0];

                                foreach (DataRow row in tclients.Rows)
                                    DataProvider.Ds.TableClient.ImportRow(row);

                                foreach (DataRow row in tableSocietes.Rows)
                                    DataProvider.Ds.Table_Societe.ImportRow(row);

                                foreach (DataRow row in tablePiedPages.Rows)
                                    DataProvider.Ds.TPiedpagefacture.ImportRow(row);

                                foreach (DataRow row in tablefacture.Rows)
                                    DataProvider.Ds.DtblFacture.ImportRow(row);

                                foreach (DataRow row in tableLignefactures.Rows)
                                    DataProvider.Ds.DtblLigneFacture.ImportRow(row);

                                foreach (DataRow row in tableLibelles.Rows)
                                    DataProvider.Ds.Tlibelle.ImportRow(row);

                                ReportExonereNonExo report = new ReportExonereNonExo();
                                report.SetDataSource(DataProvider.Ds);
                                printDocument = new System.Drawing.Printing.PrintDocument();
                                report.PrintOptions.PrinterName = GlobalDatas.printerName ?? printDocument.PrinterSettings.PrinterName;
                                report.PrintToPrinter(1, true, 0, 0);
                                Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "fin impression en  lot  facture archive : " + FactureSelected.NumeroFacture), "");

                            }
                            else
                            {
                                if (facture.IdStatut >= 14003)
                                {
                                    Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "debut chargement de la BD en lot  facture valide : " + FactureSelected.NumeroFacture), "");

                                    DataSet tablefactures = ReportModel.GetFactureNew(facture.IdFacture, facture.IdSite, (facture.IdStatut == 14007 ? 1 : 0));
                                    DataTable tclients = ReportModel.GetReportClientArchive(facture.IdClient, facture.IdFacture);
                                    DataTable tableSocietes = ReportModel.GetReportSocieteArchive(facture.IdSite);
                                    DataTable tablePiedPages = ReportModel.GetReporPiedPage(facture.IdSite);
                                    DataTable tableLibelles = ReportModel.GetLibelleArchive(facture.IdClientLangue);
                                    tableLignefactures = tablefactures.Tables[1];
                                    tablefacture = tablefactures.Tables[0];

                                    foreach (DataRow row in tclients.Rows)
                                        DataProvider.Ds.TableClient.ImportRow(row);

                                    foreach (DataRow row in tableSocietes.Rows)
                                        DataProvider.Ds.Table_Societe.ImportRow(row);

                                    foreach (DataRow row in tablePiedPages.Rows)
                                        DataProvider.Ds.TPiedpagefacture.ImportRow(row);

                                    foreach (DataRow row in tablefacture.Rows)
                                        DataProvider.Ds.DtblFacture.ImportRow(row);

                                    foreach (DataRow row in tableLignefactures.Rows)
                                        DataProvider.Ds.DtblLigneFacture.ImportRow(row);

                                    foreach (DataRow row in tableLibelles.Rows)
                                        DataProvider.Ds.Tlibelle.ImportRow(row);
                                    //  Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "fin chargement en lot  facture valider : " + FactureSelected.NumeroFacture), "");

                                    ReportExonereNonExo report = new ReportExonereNonExo();
                                    report.SetDataSource(DataProvider.Ds);
                                    printDocument = new System.Drawing.Printing.PrintDocument();
                                    report.PrintOptions.PrinterName = GlobalDatas.printerName ?? printDocument.PrinterSettings.PrinterName;
                                    report.PrintToPrinter(1, true, 0, 0);
                                    // Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "fin impression impression en lot  facture valider : " + FactureSelected.NumeroFacture), "");

                                }
                                else if (facture.IdStatut < 14003)
                                {

                                    //Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "debut chargement  de la BD en lot  facture valide : " + FactureSelected.NumeroFacture), "");
                                    tclient = ReportModel.GetReportClient(facture.IdClient);
                                    tableSociete = ReportModel.GetReportSociete(facture.IdSite);
                                    tablePiedPage = ReportModel.GetReporPiedPage(facture.IdSite);
                                    tableLibelle = ReportModel.GetLibelle(facture.IdClientLangue);
                                    tablefacture = ReportModel.GetFacture(facture.IdFacture);

                                    foreach (DataRow row in tclient.Rows)
                                        DataProvider.Ds.TableClient.ImportRow(row);

                                    foreach (DataRow row in tableSociete.Rows)
                                        DataProvider.Ds.Table_Societe.ImportRow(row);

                                    foreach (DataRow row in tablePiedPage.Rows)
                                        DataProvider.Ds.TPiedpagefacture.ImportRow(row);





                                    foreach (DataRow row in tableLibelle.Rows)
                                        DataProvider.Ds.Tlibelle.ImportRow(row);

                                    if (facture.IdClientExoneration == 170001)
                                    {
                                        foreach (DataRow row in tablefacture.Rows)
                                            DataProvider.Ds.TableFacture.ImportRow(row);

                                        tableLignefactures = ReportModel.GetLigneFacture_nonExo(facture.IdFacture);
                                        foreach (DataRow row in tableLignefactures.Rows)
                                            DataProvider.Ds.TableligneFacture.ImportRow(row);
                                        rptPartiel = new reportClientPartiel();
                                        rptPartiel.SetDataSource(DataProvider.Ds);
                                        printDocument = new System.Drawing.Printing.PrintDocument();
                                        rptPartiel.PrintOptions.PrinterName = GlobalDatas.printerName ?? printDocument.PrinterSettings.PrinterName;
                                        rptPartiel.PrintToPrinter(1, true, 0, 0);


                                    }
                                    else
                                    {
                                        //int newMode = 0;


                                        foreach (DataRow row in tablefacture.Rows)
                                            DataProvider.Ds.DtblFacture.ImportRow(row);

                                        tableLignefactures = ReportModel.GetLigneFacture(facture.IdFacture);

                                        foreach (DataRow row in tableLignefactures.Rows)
                                            DataProvider.Ds.DtblLigneFacture.ImportRow(row);

                                        // Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "fin chargement  en lot  facture non encore valide : " + FactureSelected.NumeroFacture), "");
                                        //  NewNewExonereReport rpt = new NewNewExonereReport();
                                        // ModalReport view = new ModalReport(tablefacture, tableLignefacture, tclient, tableSociete, tablePiedPage, tableLibelle);
                                        ReportExonereNonExo rpt = new ReportExonereNonExo();
                                        rpt.SetDataSource(DataProvider.Ds);
                                        printDocument = new System.Drawing.Printing.PrintDocument();
                                        rpt.PrintOptions.PrinterName = GlobalDatas.printerName ?? printDocument.PrinterSettings.PrinterName;
                                        rpt.PrintToPrinter(1, true, 0, 0);
                                        //  Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "fin impression  en lot  facture non encore valide : " + FactureSelected.NumeroFacture), "");

                                    }
                                }




                            }



                        }
                    }
                    catch (Exception ex)
                    {
                        args.Result = ex.Message;
                    }
                };

                    worker.RunWorkerCompleted += (o, args) =>
                   {
                       if (args.Result != null)
                       {
                           CustomExceptionView view = new CustomExceptionView();
                           view.Owner = Application.Current.MainWindow;
                           view.Title = "Message Erreure Validation HST";
                           view.ViewModel.Message = "Probleme survenu lors de l'impression de la liste de factures" + args.Result.ToString(); ;
                           view.ShowDialog();
                               Utils.logUserActions(string.Format("<-- interface historique factures --Erreur  impression liste facture par : {0}  ", UserConnected.Nom), "");
                               Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", args.Result.ToString()), "");
                           this.MouseCursor = null;
                           this.IsBusy = false;
                           VisibleWaitingPrint = false;
                       }
                       else
                       {
                           MessageBox.Show("Fin impression des factures!");
                           this.MouseCursor = null;
                           this.IsBusy = false;
                           VisibleWaitingPrint = false;
                       }
                   };
                    worker.RunWorkerAsync();
            }


                    //
                    //// See more at: http://codingresolved.com/discussion/18/print-report-without-preview-/p1#sthash.lRE3MXKC.dpuf

                    //IsBusy = false;

                    //Utils.logUserActions(string .Format ( "<-- interface historique factures -- impression liste  factures par : {0}  ",UserConnected.Nom ), "");
                    //}
                    //catch (Exception ex)
                    //{
                    //    CustomExceptionView view = new CustomExceptionView();
                    //    view.Owner = Application.Current.MainWindow;
                    //    view.ViewModel.Message ="Probleme survenu lors de l'impression de la liste de factures"+ex.Message ;
                    //    view.ShowDialog();
                    //    Utils.logUserActions(string.Format("<-- interface historique factures --Erreur  impression liste facture par : {0}  ", UserConnected.Nom), "");
                    //    Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", ex.Message ), "");
                    //    IsBusy = false;
                    //}
                
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

        void SetNewdataTaSet()
        {
           
           
            DataProvider.Ds.DtblFacture.Clear();
            DataProvider.Ds.DtblLigneFacture.Clear();
           
        }

        bool canImprimeToutExecute()
        {
            bool values = false;
            if ( CurrentDroit.Impression || CurrentDroit.Developpeur)
            {

                if (FacturesListe != null && FacturesListe.Count > 0)
                {
                    foreach (FactureModel item in FacturesListe)
                        if (item.IsCheck == true)
                        {
                            values = true;
                            break;
                        }
                }
            }
            else values = false;
            return values;
        }

        void canImprimeOnly()
        {
            //try
            //{
              BackgroundWorker worker = new BackgroundWorker();
              VisibleWaitingPrint = true;
           worker.DoWork += (o, args) =>
          {

                IsBusy = true;
              this.MouseCursor = Cursors.Wait;
          try { 

                if (GlobalDatas.IsArchiveSelected)
                {
                   // Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "debut impression facture archive :" + FactureSelected.NumeroFacture), "");
                    DataSet tablefacture = ReportModel.GetFacture_archive(FactureSelected.IdFacture, FactureSelected.IdSite);
                    DataTable tclient = ReportModel.GetReportClientArchive(FactureSelected.IdClient, FactureSelected.IdFacture);
                    DataTable tableSociete = ReportModel.GetReportSocieteArchive(FactureSelected.IdSite);
                    DataTable tablePiedPage = ReportModel.GetReporPiedPage(FactureSelected.IdSite);
                    DataTable tableLibelle = ReportModel.GetLibelleArchive(FactureSelected.IdClientLangue);

                    //DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureSelected.IdFacture);
                    ModalReport view = new ModalReport(tablefacture.Tables[0], tablefacture.Tables[1], tclient, tableSociete, tablePiedPage, tableLibelle);
                    view.ShowDialog();
                }
                else
                {
                    if (FactureSelected.IdStatut >= 14003  )
                    {
                        //Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "debut chargement pour impression facture valider" + FactureSelected.NumeroFacture), "");

                        DataSet tablefacture = ReportModel.GetFactureNew(FactureSelected.IdFacture, FactureSelected.IdSite, (FactureSelected.IdStatut == 14007 ? 1 : 0));
                      

                         DataTable tclient = ReportModel.GetReportClientArchive(FactureSelected.IdClient, FactureSelected.IdFacture);
                        DataTable tableSociete = ReportModel.GetReportSocieteArchive(FactureSelected.IdSite);
                        DataTable tablePiedPage = ReportModel.GetReporPiedPage(FactureSelected.IdSite);
                        DataTable tableLibelle = ReportModel.GetLibelleArchive(FactureSelected.IdClientLangue);

                        //DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureSelected.IdFacture);
                       // Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "debut impression facture valider" + FactureSelected.NumeroFacture), "");
                        ModalReport view = new ModalReport(tablefacture.Tables[0], tablefacture.Tables[1], tclient, tableSociete, tablePiedPage, tableLibelle);
                        view.ShowDialog();
                  

                                Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "fin impression    facture valide : " + FactureSelected.NumeroFacture), "");
                            }
                            else canImprimeNext();
                        }
                     }
                    catch (Exception ex)
                    {
                        args.Result = ex.Message;
                    }
                };
                     worker.RunWorkerCompleted += (o, args) =>
                    {
                        if (args.Result != null)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            view.Owner = Application.Current.MainWindow;
                            view.Title = "Message Erreure Validation HST";
                            view.ViewModel.Message = "Echeck Impression Facture " + args.Result.ToString() ;
                            view.ShowDialog();
                            Utils.logUserActions(string.Format("<-- interface historique factures --Erreur  impression  facture par : {0} {1}  ", UserConnected.Nom, args.Result.ToString()) , "");
                            //    Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", ex.Message), "");
                            this.MouseCursor = null;
                            this.IsBusy = false;
                            VisibleWaitingPrint = false;

                        }
                        else
                        {
                            this.MouseCursor = null;
                            this.IsBusy = false;
                            VisibleWaitingPrint = false;


                        }

                    };
                     worker.RunWorkerAsync();

           
        }
        void canImprimeNext()
        {
            string mode = string.Empty;

          
                try
                {
                    IsBusy = true;
                    Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "debut chargement   facture non valide : " + FactureSelected.NumeroFacture), "");

                    DataTable tclient = ReportModel.GetReportClient(FactureSelected.IdClient);
                    DataTable tableSociete = ReportModel.GetReportSociete(FactureSelected.IdSite);
                    DataTable tablePiedPage = ReportModel.GetReporPiedPage(FactureSelected.IdSite);
                    DataTable tableLibelle = ReportModel.GetLibelle(FactureSelected.IdClientLangue);
                    DataTable tablefacture = ReportModel.GetFacture(FactureSelected.IdFacture);

                  


                    if (FactureSelected.IdClientExoneration ==170001)
                    {
                        DataTable tableLignefactures = ReportModel.GetLigneFacture_nonExo(FactureSelected.IdFacture);
                        formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefactures, tableLibelle, 3);
                        vf.ShowDialog();
                    }
                    else
                    {
                        

                        DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureSelected.IdFacture);
                        //formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefacture, tableLibelle, 1);
                        Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "debut impression facture non valider" + FactureSelected.NumeroFacture), "");
                        ModalReport view = new ModalReport(tablefacture, tableLignefacture, tclient, tableSociete, tablePiedPage, tableLibelle);
                        view.ShowDialog();
                        Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", "fin impression  facture non valide : " + FactureSelected.NumeroFacture), "");
                    }

                    IsBusy = false;
                   // Utils.logUserActions(string.Format("<-- interface historique factures -- impression  facture par : {0}  ", UserConnected.Nom), "");

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    Utils.logUserActions(string.Format("<-- interface historique factures --Erreur  impression  facture par : {0}  ", UserConnected.Nom), "");
                    Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", ex .Message ), "");
                    this.IsBusy = false;
                }
            
        

        }
       

        bool canImprimeExecuteNext()
        {
            bool returnValues = false;
            if ( CurrentDroit.Impression || CurrentDroit.Developpeur )
            {
                if (FactureSelected != null)
                    returnValues = true;
                else returnValues = false;
            }
            return returnValues;
        }

        void canPrintContext()
        {
            try
            {
                 IsBusy = true;

                 if (GlobalDatas.IsArchiveSelected)
                 {
                     DataSet tablefacture = ReportModel.GetFacture_archive(FactureActived.IdFacture, FactureActived.IdSite);
                     DataTable tclient = ReportModel.GetReportClientArchive(FactureActived.IdClient, FactureActived.IdFacture);
                     DataTable tableSociete = ReportModel.GetReportSocieteArchive(FactureActived.IdSite);
                     DataTable tablePiedPage = ReportModel.GetReporPiedPage(FactureActived.IdSite);
                     DataTable tableLibelle = ReportModel.GetLibelleArchive(FactureActived.IdClientLangue);

                     //DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureSelected.IdFacture);
                     ModalReport view = new ModalReport(tablefacture.Tables[0], tablefacture.Tables[1], tclient, tableSociete, tablePiedPage, tableLibelle);
                     view.ShowDialog();
                 }
                 else
                 {
                     if (FactureActived.IdStatut >= 14003 )
                     {

                         DataSet tablefacture = ReportModel.GetFactureNew(FactureActived.IdFacture, FactureActived.IdSite, (FactureActived.IdStatut==14007?1:0));

                         DataTable tclient = ReportModel.GetReportClientArchive(FactureActived.IdClient, FactureActived.IdFacture);
                         DataTable tableSociete = ReportModel.GetReportSocieteArchive(FactureActived.IdSite);
                         DataTable tablePiedPage = ReportModel.GetReporPiedPage(FactureActived.IdSite);
                         DataTable tableLibelle = ReportModel.GetLibelleArchive(FactureActived.IdClientLangue);

                         //DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureSelected.IdFacture);
                         ModalReport view = new ModalReport(tablefacture.Tables[0], tablefacture.Tables[1], tclient, tableSociete, tablePiedPage, tableLibelle);
                         view.ShowDialog();
                     }
                     else{
                        
                             DataTable tclient = ReportModel.GetReportClient(FactureActived.IdClient);
                             DataTable tableSociete = ReportModel.GetReportSociete(FactureActived.IdSite);
                             DataTable tablePiedPage = ReportModel.GetReporPiedPage(FactureActived.IdSite);
                             DataTable tableLibelle = ReportModel.GetLibelle(FactureActived.IdClientLangue);
                             DataTable tablefacture = ReportModel.GetFacture(FactureActived.IdFacture);



                             if (FactureActived.IdClientExoneration == 170001)
                             {
                                 DataTable tableLignefactures = ReportModel.GetLigneFacture_nonExo(FactureActived.IdFacture);
                                 formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefactures, tableLibelle, 3);
                                 vf.ShowDialog();
                             }
                             else
                             {
                                

                                 DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureActived.IdFacture);
                                // formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefacture, tableLibelle, 1);
                                 ModalReport view = new ModalReport(tablefacture, tableLignefacture, tclient, tableSociete, tablePiedPage, tableLibelle);
                                 view.ShowDialog();
                                
                             }
                         }

                     IsBusy = false;
                     //Utils.logUserActions(string.Format("<-- interface historique factures -- impression  facture par : {0}  ", UserConnected.Nom), "");
                 }
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    Utils.logUserActions(string.Format("<-- interface historique factures --Erreur  impression  facture {0} par : {1}  ",FactureActived.NumeroFacture, UserConnected.Nom), "");
                    Utils.logUserActions(string.Format("<-- interface historique factures -- {0}  ", ex .Message ), "");
                    this.IsBusy = false;
                }
        }
        bool canExecutePrintContext()
        {
            bool returnValues = false;
            if (CurrentDroit.Impression || CurrentDroit.Developpeur)
            {
                if (FactureActived != null)
                     returnValues = true;
                   
               
            }
            return returnValues;
        }


        #endregion

        #region REGION EXPORT
        
     
        void canExport()
        {
            //try
            //{
               

                //var query = (from chk in DtTableFactures.AsEnumerable()
                //             where Convert.ToBoolean(chk.Field<object>("IsCheck")) == true
                //             select chk).CopyToDataTable();
              BackgroundWorker worker = new BackgroundWorker();
                    VisibleWaitingPrint = true;
                    worker.DoWork += (o, args) =>
                {

                    IsBusy = true;
                    this.MouseCursor = Cursors.Wait;
            try{
                   int ligneSelected = FacturesListe.Count(f => f.IsCheck == true);

                        if (ligneSelected> 0)
                        {
                            //societeCourante
                            CommonModule.ExportToExcel(FacturesListe.Where(f => f.IsCheck == true), societeCourante);
                            // FacturesListe.Where(f => f.IsCheck == true)
                        }
                        else
                        {
                     
                        }
                        IsBusy = false ;
                        Utils.logUserActions("<--UI HSt factures -> Extraction factures par " + UserConnected.Loggin , "");
             }
            catch (Exception ex)
            {
                args.Result = ex.Message;
            }
          };
        worker.RunWorkerCompleted += (o, args) =>
                {
                    if (args.Result != null)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "Message Erreure Validation HST";
                        view.ViewModel.Message = "Probleme survenu lors de l'impression de la liste de factures" + args.Result.ToString() ;
                        view.ShowDialog();
                        Utils.logUserActions("<--UI HSt factures -> Erreur extraction facture  par " + UserConnected.Loggin + "" + args.Result.ToString(), "");
                        this.MouseCursor = null;
                        this.IsBusy = false;
                        VisibleWaitingPrint = false;
                    }
                    else
                    {
                        this.MouseCursor = null;
                        this.IsBusy = false;
                        VisibleWaitingPrint = false;
                    }

                };
        worker.RunWorkerAsync();

           
        }

        bool canExecuteExportExcel()
        {
            bool values = false;
            
              if (CurrentDroit.Execution || CurrentDroit.Developpeur)
              {

                  if (FacturesListe != null && FacturesListe.Count > 0)
                  {
                      foreach (FactureModel item in FacturesListe)
                          if (item.IsCheck == true)
                          {
                              values = true;
                              break;
                          }
                  }
                  else values = false;
              }
            return values;

           
        }

        void canExportAll()
        {
            BackgroundWorker worker = new BackgroundWorker();
            VisibleWaitingPrint = true;
            worker.DoWork += (o, args) =>
            {
            try
            {
                IsBusy = true;
                this.MouseCursor = Cursors.Wait;
                CommonModule.ExportToExcel(FacturesListe, societeCourante);
               // CommonModule.ExportToExcel(DtblCacheListeFacture);
              
              
            }
            catch (Exception ex)
            {
                args.Result = ex.Message;
            }

           };
             worker.RunWorkerCompleted += (o, args) =>
             {
                 if (args.Result != null)
                 {
                     CustomExceptionView view = new CustomExceptionView();
                     view.Owner = Application.Current.MainWindow;
                     view.Title = "MESSAGE ERREUR EXPORT EXCEL TOUT";
                     view.ViewModel.Message = args.Result.ToString();
                     view.ShowDialog();
                     this.MouseCursor = null;
                     this.IsBusy = false;
                     VisibleWaitingPrint = false;

                     Utils.logUserActions("<--UI HSt factures -> Erreur Export liste factures  par " + UserConnected.Loggin + "" + args.Result.ToString(), "");
                 }
                 else
                 {
                     this.MouseCursor = null;
                     this.IsBusy = false;
                     VisibleWaitingPrint = false;
                 }
             };
             worker.RunWorkerAsync();
        }

        bool canExecuteExportAllExcel()
        {
            return FacturesListe != null ? (FacturesListe.Count > 0 ? true : false) : false;
        }
        #endregion

        #region REGION VALIDATION 
        
      

        void canValidation()
        {

            List<FactureModel> query=null ;
            this.IsBusy = true ;

            bool modifDatas = false;
            //try
            //{
                int ligneSelected = FacturesListe.Count(f => f.IsCheck == true);

               //var  dtlquery = (from chk in DtTableFactures.AsEnumerable()
               //          where Convert.ToBoolean(chk["IsCheck"]) == true
               //                             select chk).CopyToDataTable ();

               if (ligneSelected <= 0)
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
                  BackgroundWorker worker = new BackgroundWorker();
                    VisibleWaitingValidate = true;
                     worker.DoWork += (o, args) =>
                      {

                          try { 
                          bool isvalueSelected = false;
                          IsBusy = true;
                          this.MouseCursor = Cursors.Wait;
                        
                          IEnumerable<FactureModel> FacturesListeFilter = FacturesListe.Where(f => f.IsCheck == true);


                          string numfacture = string.Empty;
                          string factureDejaValider = string.Empty;
                          string factureDejaSortie = string.Empty;
                          string factureDejaSuspendu = string.Empty;
                          string factureDejaNonValable = string.Empty;
                          string factureAvoir = string.Empty;
                          string factureNonValide = string.Empty;
                          bool isErrorMessage = false;

                          foreach (FactureModel facture in FacturesListeFilter)
                          {
                              //if (facture.ClienOk == false)
                              //{
                              //    numfacture = numfacture + "\n" + facture.NumeroFacture;
                              //    isErrorMessage = true;
                              //}

                              if (facture.IdStatut == 14003)
                              {
                                  factureDejaValider = factureDejaValider + "\n" + facture.NumeroFacture;
                                  isErrorMessage = true;
                              }
                              if (facture.IdStatut == 14004)
                              {
                                  factureDejaSortie = factureDejaSortie + "\n" + facture.NumeroFacture;
                                  isErrorMessage = true;
                              }
                              if (facture.IdStatut == 14005)
                              {
                                  factureDejaSuspendu = factureDejaSuspendu + "\n" + facture.NumeroFacture;
                                  isErrorMessage = true;
                              }
                              if (facture.IdStatut == 14006)
                              {
                                  factureDejaNonValable = factureDejaNonValable + "\n" + facture.NumeroFacture;
                                  isErrorMessage = true;
                              }
                              if (facture.IdStatut == 14007)
                              {
                                  factureAvoir = factureAvoir + "\n" + facture.NumeroFacture;
                                  isErrorMessage = true;
                              }
                              if (facture.IdStatut == 14001)
                              {
                                  factureNonValide = factureNonValide + "\n" + facture.NumeroFacture;
                                  isErrorMessage = true;
                              }
                          }

                          if (isErrorMessage)
                          {
                              bool isvalue = false;
                              string messageError = string.Empty;
                              if (ligneSelected == 1)
                                  messageError = "la facture suivante ne pourra pas être Validée";
                              else messageError = "les factures suivantes ne pourront pas être Validées";

                              //if (!string.IsNullOrEmpty(numfacture))
                              //{
                              //    messageError = messageError + "\n" + numfacture + "\n Informations du Client Incompletes";
                              //    isvalue = true;
                              //}
                              if (!string.IsNullOrEmpty(factureNonValide))
                              {
                                  messageError = string.Format("{0} \n {1}  {2}", messageError, factureNonValide, ConstStrings.Get("hst_facture_validate_14001"));
                                  isvalue = true;
                              }
                              if (!string.IsNullOrEmpty(factureDejaValider))
                              {
                                  messageError = string.Format("{0} \n {1}  {2}", messageError, factureDejaValider, "Facture deja valider");
                                  isvalue = true;
                              }
                              if (!string.IsNullOrEmpty(factureDejaSortie))
                              {
                                  messageError = string.Format("{0} \n {1}  {2}", messageError, factureDejaSortie, ConstStrings.Get("hst_facture_validate_14004"));
                                  //messageError = messageError + "\n" + factureDejaSortie + "\n Factures Sortie";
                                  isvalue = true;
                              }
                              if (!string.IsNullOrEmpty(factureDejaSuspendu))
                              {
                                  messageError = string.Format("{0} \n {1} \n {2}", messageError, factureDejaSuspendu, ConstStrings.Get("hst_facture_validate_14005"));
                                  // messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures Suspendus";
                                  isvalue = true;
                              }
                              if (!string.IsNullOrEmpty(factureDejaNonValable))
                              {
                                  messageError = string.Format("{0} \n {1} \n {2}", messageError, factureDejaNonValable, ConstStrings.Get("hst_facture_validate_14006"));
                                  // messageError = messageError + "\n" + factureDejaNonValable + "\n Non Valable";
                                  isvalue = true;
                              }

                              if (!string.IsNullOrEmpty(factureAvoir))
                              {
                                  messageError = string.Format("{0} \n {1} \n {2}", messageError, factureAvoir, ConstStrings.Get("hst_facture_validate_14007"));
                                  // messageError = messageError + "\n" + factureAvoir + "\n  Facture Avoir";
                                  isvalue = true;
                              }

                              if (isvalue)
                                  MessageBox.Show(messageError);
                          }


                          StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");


                          foreach (FactureModel facture in FacturesListeFilter)
                          {
                              if (facture.IsCheck)
                              {
                                  if (facture.IdStatut == 14002)
                                  {
                                      // facture.LibelleSIte = societeCourante.Libelle;

                                      UtilisateurModel userCreat = new UtilisateurModel().UTILISATEUR_SELECTByID(facture.IdCreerpar);
                                      facture.LibelleUserNom = userCreat.Nom;
                                      facture.LibelleUserPrenom = userCreat.Prenom;
                                      facture.IdStatut = newStatut.IdStatut;
                                      facture.TauxMargeBeneficiaire = facture.MaregeBeneficiaireId.HasValue ? new TaxeModel().Taxe_SELECTById(facture.MaregeBeneficiaireId.Value, societeCourante.IdSociete).Taux : string.Empty;
                                      factureService.FACTURE_VALIDATION(facture.IdFacture, DateTime.Now, newStatut.IdStatut, UserConnected.Id, true, facture);


                                      modifDatas = true;


                                  }
                              }
                          }

                          }
                          catch (Exception ex)
                          {
                              args.Result = ex.Message ;
                          }
                      };

                   worker.RunWorkerCompleted += (o, args) =>
                    {
                        if (args.Result != null)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            view.Owner = Application.Current.MainWindow;
                            view.Title = "Message Erreure Validation HST";
                            view.ViewModel.Message = view.Title = args.Result.ToString();
                            this.MouseCursor = null;
                            this.IsBusy = false;
                            VisibleWaitingValidate = false;
                            view.ShowDialog();

                            Utils.logUserActions(string.Format("<-- UI HST factures : {0}  ", args.Result), "");
               
                        }
                        else
                        {
                            this.MouseCursor = null;
                            this.IsBusy = false;
                           
                            //ProgressBarVisibility = false;
                            //fliste.StartStopWait(false);

                            //LoadingIsArchiveExist();

                            // Utils.logUserActions("<-- UI HST factures  fin chargement liste factures du mois courant--   ", "");

                            if (modifDatas)
                            {
                                Utils.logUserActions(string.Format("<-- UI HST factures -->  changement statut facture <<validation>>  facture par : {0}  ", UserConnected.Nom), "");

                                // CacheDatas.Listefactures = null;
                                if (DateDebut.HasValue && DateFin.HasValue)
                                    FacturesListe = factureService.FACTURE_SEARCH(societeCourante.IdSociete, DateDebut.Value, DateFin.Value, ClientFactireselect!=null ?ClientFactireselect.IdClient:-1);
                                else
                                FacturesListe = factureService.FACTURE_GETLISTE_NEW(societeCourante.IdSociete, false);
                                //CacheDatas.lastUpdatefacture = null;
                                //FacturesListe = null;
                                //FacturesListe = CacheDatas.Listefactures;
                                this.IsBusy = false;
                                //  Overviews = overviewService.GetOverview(UserConnected.Id);
                                FactureSelected = null;
                                VisibleWaitingValidate = false;

                            }
                        }
                    };
                      worker.RunWorkerAsync();

                 }
                }

              

               
                
           

            //}
            //catch (Exception ex)
            //{
            //    CustomExceptionView view = new CustomExceptionView();
            //    view.Owner = Application.Current.MainWindow;
            //    view.ViewModel.Message = ex.Message;
            //    view.ShowDialog();

            //    Utils.logUserActions(string.Format("<-- interface historique factures erreur --lors changement statut facture <<validation>>  facture par : {0}  ", UserConnected.Nom), "");
            //    Utils.logUserActions(string.Format("<-- UI HST factures : {0}  ", ex.Message ), "");
               
            //    this.IsBusy = false;
            //}


        }

        void refreshDatas()
        {
        }

        bool canExecuteValidation()
        {
            bool values = false;
           // if (societeCourante.IdSociete == GlobalDatas.DefaultCompany.IdSociete)
            //{
            if ( CurrentDroit.Validation || CurrentDroit.Developpeur )
            {
                if (FacturesListe != null && FacturesListe.Count > 0)
                {
                    if (FacturesListe.ToList().Exists(item=>item.IsCheck==true))
                        values = true;
                    //foreach (FactureModel item in FacturesListe)
                    //    if (item.IsCheck == true)
                    //    {
                    //        values = true;
                    //        break;
                    //    }
                }


                if (GlobalDatas.IsArchiveSelected)
                    values = false;
            }
            return values;
            //return (IsValide == true ? (EnbameBtnValide == true ? true : false) : false);
        }

        #endregion

        #region Region SORTIE

        void canSortie()
        {
            bool modifDatas = false;
            List<FactureModel> query = null;
            //try
            //{

              //var   querys = (from chk in DtTableFactures.AsEnumerable()
              //           where Convert.ToBoolean(chk["IsCheck"]) == true
              //           select chk).CopyToDataTable ();
                int ligneSelected = FacturesListe.Count(f => f.IsCheck == true);
                if (ligneSelected <= 0)
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


                         BackgroundWorker worker = new BackgroundWorker();
                              VisibleWaitingValidate = true;
                              worker.DoWork += (o, args) =>
                      {
                            //vérification si client ok
                            string numfacture = string.Empty;
                            string factureDejaValider = string.Empty;
                            string factureDejaSortie = string.Empty;
                            string factureDejaSuspendu = string.Empty;
                            string factureDejaNonValable = string.Empty;
                            string factureNonValide = string.Empty;
                            string factureAvoir = string.Empty;
                            bool isErrorMessage = false;
                            IsBusy = true;
                            this.MouseCursor = Cursors.Wait;

                           try { 

                            IEnumerable<FactureModel> FacturesListeFilter = FacturesListe.Where(f => f.IsCheck == true);

                            foreach (FactureModel facture in FacturesListeFilter)
                            {
                                //if (facture.ClienOk == false)
                                //{
                                //    numfacture = numfacture + "\n" + facture.NumeroFacture;
                                //    isErrorMessage = true;
                                //}

                                 if (facture.IdStatut == 14003)
                                {
                                    factureDejaValider =  factureDejaValider + "\n" + facture.NumeroFacture;
                                    isErrorMessage = false;
                                }
                                if (facture.IdStatut == 14004)
                                {
                                    factureDejaSortie = factureDejaSortie + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.IdStatut == 14005)
                                {
                                    factureDejaSuspendu = factureDejaSuspendu + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.IdStatut == 14006)
                                {
                                    factureDejaNonValable = factureDejaNonValable + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.IdStatut <= 14002)
                                {
                                    factureNonValide = factureNonValide + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }
                                if (facture.IdStatut == 14007)
                                {
                                    factureAvoir = factureAvoir + "\n" + facture.NumeroFacture;
                                    isErrorMessage = true;
                                }


                            }


                            if (isErrorMessage)
                            {
                                bool isvalues = false;
                                string messageError = "les factures suivantes ne pourront pas être  passées au statut Sortie";
                                if (!string.IsNullOrEmpty(factureNonValide))
                                {
                                   // messageError =string .Format("0}\n {1} \n {2}", messageError , factureNonValide , "En Cours de Validation");
                                    messageError = string.Format("{0} \n {1} \n {2}", messageError, factureNonValide, ConstStrings.Get("hst_facture_validate_14001"));
                                    isvalues = true;
                                }

                                if (!string.IsNullOrEmpty(factureDejaSortie))
                                {
                                    //messageError = string.Format("0}\n {1} \n {2}", messageError, factureDejaSortie, "Factures deja Sorties");
                                    messageError =  string.Format("{0} \n {1} \n {2}", messageError, factureDejaSortie, ConstStrings.Get("hst_facture_validate_14004"));
                                   // messageError = messageError + "\n" + factureDejaSortie + "\n Factures deja Sorties";
                                    isvalues = true;
                                }
                                if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                {
                                   // messageError = string.Format("0}\n {1} \n {2}", messageError, factureDejaSuspendu, "Factures Suspendus");
                                    messageError = string.Format("{0} \n {1} \n {2}", messageError, factureDejaSuspendu, ConstStrings.Get("hst_facture_validate_14005"));
                                   // messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures Suspendus";
                                    isvalues = true;
                                }
                                if (!string.IsNullOrEmpty(factureDejaNonValable))
                                {
                                    //messageError = string.Format("0}\n {1} \n {2}", messageError, factureDejaNonValable, "Factures Valide");
                                    messageError =  string.Format("{0} \n {1} \n {2}", messageError, factureDejaNonValable, ConstStrings.Get("hst_facture_validate_14002"));
                                   // messageError = messageError + "\n" + factureDejaNonValable + "\n Non Valide";
                                }
                                if (!string.IsNullOrEmpty(factureAvoir))
                                {
                                    messageError = string.Format("{0} \n {1} \n {2}", messageError, factureAvoir, ConstStrings.Get("hst_facture_validate_14007"));
                                    isvalues = true;
                                }

                                if (isvalues)
                                MessageBox.Show(messageError);
                            }

                            IEnumerable<FactureModel> FacturesListeFilterSortie = FacturesListeFilter.Where(f => !f.DateSortie.HasValue);
                 
                           //var  queryDateSortie = (from nn in querys.AsEnumerable()
                           //                                       where nn["DateSortie"] == DBNull.Value 
                           //                                select nn).CopyToDataTable ();

                            StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "4");
                            foreach (FactureModel facture in FacturesListeFilterSortie)
                            {
                                if (facture.IdStatut==14003 )
                                {
                                    factureService.FACTURE_SORTIE(facture.IdFacture, DateTime.Now, newStatut.IdStatut, UserConnected.Id, true);
                                   // FactureModel factureUpdate = factureService.GET_FACTURE_BYID(Convert.ToInt64(facture["ID"]));
                                    modifDatas = true;
                                }
                            }
                           

                           }
                           catch (Exception ex)
                           {
                               args.Result = ex.Message;
                           }
                      };
                     worker.RunWorkerCompleted += (o, args) =>
                    {
                        if (args.Result != null)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            view.Owner = Application.Current.MainWindow;
                            view.Title = "Message Erreure Validation HST";
                            view.ViewModel.Message = "Eceche Modification Statut facture " + args.Result.ToString(); ;
                           view.ShowDialog();
                           Utils.logUserActions(string.Format("<--;55;UI HST factures --Erreur lors du changement statut facture <<sortie>>  facture par : {0};{1}  ", UserConnected.Nom, args.Result.ToString()), "");
                            //   // Utils.logUserActions(string.Format("<-- UI HST factures : {0}  ", ex.Message ), "");
                            Utils.logUserActions(string.Format("<-- UI HST factures : {0}  ", args.Result), "");
                            this.MouseCursor = null;
                            this.IsBusy = false;
                            VisibleWaitingValidate = false;

                        }
                        else
                        {
                            if (modifDatas)
                            {
                                Utils.logUserActions(string.Format("<-- UI HST factures -- changement statut facture <<sortie>>  facture par : {0}  ", UserConnected.Nom), "");

                                if (DateDebut.HasValue && DateFin.HasValue)
                                    FacturesListe = factureService.FACTURE_SEARCH(societeCourante.IdSociete, DateDebut.Value, DateFin.Value, ClientFactireselect != null ? ClientFactireselect.IdClient : -1);
                                else
                                    FacturesListe = factureService.FACTURE_GETLISTE_NEW(societeCourante.IdSociete, false);


                            }

                            this.MouseCursor = null;
                            this.IsBusy = false;
                            VisibleWaitingValidate = false;
                        }

                    };
                     worker.RunWorkerAsync();

                        }
                }

            //}
            //catch (Exception ex)
            //{
            //    CustomExceptionView view = new CustomExceptionView();
            //    view.Owner = Application.Current.MainWindow;
            //    view.ViewModel.Message ="Eceche Modification Statut facture "+ ex.Message;
            //    view.ShowDialog();
            //    Utils.logUserActions(string.Format("<--;55;UI HST factures --Erreur lors du changement statut facture <<sortie>>  facture par : {0};{1}  ", UserConnected.Nom, ex.Message), "");
            //   // Utils.logUserActions(string.Format("<-- UI HST factures : {0}  ", ex.Message ), "");
            //}



        }

        bool canExecuteSortie()
        {
          
            bool values = false;
            //if (societeCourante.IdSociete == GlobalDatas.DefaultCompany.IdSociete)
           // {
            if ( CurrentDroit.StatutSortie || CurrentDroit.Developpeur )
            {
                if (FacturesListe != null && FacturesListe.Count > 0)
                {
                    if (FacturesListe.ToList().Exists(item => item.IsCheck == true && item.IdStatut == 14003))
                        values = true;

                    //foreach (FactureModel item in FacturesListe)
                    //    if (item.IsCheck == true && item.IdStatut == 14003 )
                    //    {
                    //        values = true;
                    //        break;
                    //    }
                }


             
                if (GlobalDatas.IsArchiveSelected)
                    values = false;
            }

            return values;
        }

        #endregion

        #region REGION SUSPENSION

        void canSuspension()
        {
            bool modifDatas = false;
           // try
           // {

                int ligneSelected = FacturesListe.Count(f => f.IsCheck == true);

                if (ligneSelected <= 0)
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
                        BackgroundWorker worker = new BackgroundWorker();
                        VisibleWaitingValidate = true;
                        worker.DoWork += (o, args) =>
                         {

                             try
                             {

                                 IsBusy = true;
                                 this.MouseCursor = Cursors.Wait;

                                 string numfacture = string.Empty;
                                 string factureDejaValider = string.Empty;
                                 string factureDejaSortie = string.Empty;
                                 string factureDejaSuspendu = string.Empty;
                                 string factureDejaNonValable = string.Empty;
                                 string factureNonValide = string.Empty;
                                 bool isErrorMessage = false;
                                 VisibleWaitingValidate = true;

                                 IEnumerable<FactureModel> FacturesListeFilter = FacturesListe.Where(f => f.IsCheck == true);

                                 foreach (FactureModel facture in FacturesListeFilter)
                                 {

                                     if (facture.IdStatut == 14004)
                                     {
                                         factureDejaSortie = factureDejaSortie + "\n" + facture.NumeroFacture;
                                         isErrorMessage = true;
                                     }
                                     if (facture.IdStatut == 14005)
                                     {
                                         factureDejaSuspendu = factureDejaSuspendu + "\n" + facture.NumeroFacture;
                                         isErrorMessage = true;
                                     }
                                     if (facture.IdStatut == 14006)
                                     {
                                         factureDejaNonValable = factureDejaNonValable + "\n" + facture.NumeroFacture;
                                         isErrorMessage = true;
                                     }




                                 }

                                 if (isErrorMessage)
                                 {
                                     bool isvalues = false;
                                     string messageError = "les factures suivantes ne pourront pas être Validées";

                                     if (!string.IsNullOrEmpty(factureDejaSortie))
                                     {
                                         messageError = messageError + "\n" + factureDejaSortie + "\n Factures deja Sortie";
                                         isvalues = true;
                                     }
                                     if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                     {
                                         messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures deja Suspendus";
                                         isvalues = true;
                                     }

                                     if (!string.IsNullOrEmpty(factureDejaNonValable))
                                     {
                                         messageError = messageError + "\n" + factureDejaNonValable + "\n Factures Non Valide";
                                         isvalues = true;
                                     }
                                     if (isvalues)
                                         MessageBox.Show(messageError);
                                 }

                                 StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "5");
                                 foreach (FactureModel facture in FacturesListeFilter)
                                 {
                                     if (facture.IdStatut == 14002 || facture.IdStatut == 14003)
                                     {
                                         factureService.FACTURE_SUSPENSION(facture.IdFacture, newStatut.IdStatut, UserConnected.Id, true);
                                         modifDatas = true;

                                     }

                                 }

                             }
                             catch (Exception ex)
                             {
                                 args.Result = ex.Message;
                             }
                         };

                        worker.RunWorkerCompleted += (o, args) =>
                      {
                          if (args.Result != null)
                          {
                              CustomExceptionView view = new CustomExceptionView();
                              view.Owner = Application.Current.MainWindow;
                              view.Title = "Message Erreure Suspension HST";
                              view.ViewModel.Message = view.Title = args.Result.ToString();
                              this.MouseCursor = null;
                              this.IsBusy = false;
                              VisibleWaitingValidate = false;
                              view.ShowDialog();
                              Utils.logUserActions(string.Format("<--;55;UI HST factures--Erreur lors du changement statut facture <<suspension>>  facture par : {0} ;{1} ", UserConnected.Nom, args.Result), "");
                              Utils.logUserActions(string.Format("<--UI HST factures : {0}  ", args.Result), "");

                           

                          }
                          else
                          {
                              this.MouseCursor = null;
                              this.IsBusy = false;

                              if (modifDatas)
                              {
                                  Utils.logUserActions(string.Format("<-- UI HST factures -- changement statut facture <<suspension>>  facture  par : {0}  ", UserConnected.Nom), "");

                                  if (DateDebut.HasValue && DateFin.HasValue)
                                      FacturesListe = factureService.FACTURE_SEARCH(societeCourante.IdSociete, DateDebut.Value, DateFin.Value, ClientFactireselect != null ? ClientFactireselect.IdClient : -1);
                                  else
                                      FacturesListe = factureService.FACTURE_GETLISTE_NEW(societeCourante.IdSociete, false);

                                  //  Overviews = overviewService.GetOverview(UserConnected.Id);
                                  //  modifDatas = false;
                              }
                              VisibleWaitingValidate = false;

                          }
                      };
                        worker.RunWorkerAsync();
                    }
                }
            
                
                               

                         

              

               

            //}
            //catch (Exception ex)
            //{
            //    CustomExceptionView view = new CustomExceptionView();
            //    view.Owner = Application.Current.MainWindow;
            //    view.ViewModel.Message ="Echec modification statut "+ ex.Message;
            //    view.ShowDialog();
            //    Utils.logUserActions(string.Format("<--;55;UI HST factures--Erreur lors du changement statut facture <<suspension>>  facture par : {0} ;{1} ", UserConnected.Nom, ex.Message), "");
            //   // Utils.logUserActions(string.Format("<--UI HST factures : {0}  ", ex.Message), "");


            //    this.IsBusy = false;
            //}
        }

        bool canExecuteSuspension()
        {
            bool values = false;
           // if (societeCourante.IdSociete == GlobalDatas.DefaultCompany.IdSociete)
            //{
            if ( CurrentDroit.StatutSuspension || CurrentDroit.Developpeur )
            {

                if (FacturesListe != null && FacturesListe.Count > 0)
                {
                    foreach (FactureModel item in FacturesListe)
                        if (item.IsCheck == true)
                        {
                            if (item.IdStatut == 14002 || item.IdStatut == 14003)
                                values = true;
                            break;
                        }
                }



                if (GlobalDatas.IsArchiveSelected)
                    values = false;
            }
            return values;
           // return FacturesListe != null ? (FacturesListe.Count > 0 ? (FactureSelected != null ? (EnbameBtnSuspendre==true ?true :false ) : false) : false) : false;
        }
        #endregion

        #region REGION NON VALABLE

        void canNonValable()
        {
            bool modifDatas = false;
            //try
            //{
                int ligneSelected = FacturesListe.Count(f => f.IsCheck == true);
                //var queryNonValable = (from chk in DtTableFactures.AsEnumerable()
                //                       where Convert.ToBoolean(chk["IsCheck"]) == true
                //                                      select chk).CopyToDataTable ();

                if (ligneSelected <= 0)
                {
                    MessageBox.Show("Pas de Facture sélectionnées Pour cette opération");
                }
                else
                {
                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                    messageBox.Owner = Application.Current.MainWindow;
                    messageBox.Title = "INFORMATION  STATUT NON VALABLE";
                    messageBox.ViewModel.Message = "Confirmer Action de Facture non  valide,\n Voulez Vous continuez ?";
                    if (messageBox.ShowDialog().Value == true)
                    {
                        BackgroundWorker worker = new BackgroundWorker();
                        VisibleWaitingValidate = true;
                        worker.DoWork += (o, args) =>
                       {
                           try
                           {
                              
                               string numfacture = string.Empty;
                               string factureDejaValider = string.Empty;
                               string factureDejaSortie = string.Empty;
                               string factureDejaSuspendu = string.Empty;
                               string factureDejaNonValable = string.Empty;
                               string factureNonValide = string.Empty;
                               bool isErrorMessage = false;
                               IsBusy = true;
                               this.MouseCursor = Cursors.Wait;

                               //VisibleWaitingValidate = true;


                               IEnumerable<FactureModel> FacturesListeFilter = FacturesListe.Where(f => f.IsCheck == true);

                               foreach (FactureModel facture in FacturesListeFilter)
                               {
                                   //    if (facture.IdStatut == 14003)
                                   //    {
                                   //        factureDejaValider = factureDejaValider + "\n" + facture.NumeroFacture;
                                   //        isErrorMessage = true;
                                   //    }
                                   if (facture.IdStatut == 14004)
                                   {
                                       factureDejaSortie = factureDejaSortie + "\n" + facture.NumeroFacture;
                                       isErrorMessage = true;
                                   }
                                   if (facture.IdStatut == 14005)
                                   {
                                       factureDejaSuspendu = factureDejaSuspendu + "\n" + facture.NumeroFacture;
                                       isErrorMessage = true;
                                   }
                                   if (facture.IdStatut == 14006)
                                   {
                                       factureDejaNonValable = factureDejaNonValable + "\n" + facture.NumeroFacture;
                                       isErrorMessage = true;
                                   }

                               }

                               if (isErrorMessage)
                               {
                                   bool isvalues = false;
                                   string messageError = "les factures suivantes ne pourront pas être Validées";

                                   if (!string.IsNullOrEmpty(factureDejaSortie))
                                   {
                                       messageError = messageError + "\n" + factureDejaSortie + "\n Factures Deja Sortie";
                                       isvalues = true;
                                   }
                                   if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                   {
                                       messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures Deja Suspendus";
                                       isvalues = true;
                                   }

                                   if (!string.IsNullOrEmpty(factureDejaNonValable))
                                   {
                                       messageError = messageError + "\n" + factureDejaNonValable + "\n Factures Deja Non Valide";
                                       isvalues = true;
                                   }

                                   if (isvalues)
                                       MessageBox.Show(messageError);
                               }

                               StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "6");
                               foreach (FactureModel facture in FacturesListeFilter)
                               {

                                   if (facture.IdStatut == 14003)
                                   {
                                       factureService.FACTURE_NONVALABLE(facture.IdFacture, newStatut.IdStatut, UserConnected.Id, true);
                                       modifDatas = true;

                                   }
                               }

                           }
                           catch (Exception ex)
                           {
                               args.Result = ex.Message;
                           }
                       };

                        worker.RunWorkerCompleted += (o, args) =>
                      {
                          if (args.Result != null)
                          {
                              CustomExceptionView view = new CustomExceptionView();
                              view.Owner = Application.Current.MainWindow;
                              view.Title = "Message Erreure Validation HST";
                              view.ViewModel.Message = view.Title = args.Result.ToString();
                              this.MouseCursor = null;
                              this.IsBusy = false;
                              VisibleWaitingValidate = false;
                              view.ShowDialog();

                              Utils.logUserActions(string.Format("<-- UI HST factures : {0}  ", args.Result), "");

                          }
                          else
                          {
                              if (modifDatas)
                              {
                                  Utils.logUserActions(string.Format("<--UI HST factures -- changement statut facture <<non valide>>  facture  par : {0}  ", UserConnected.Nom), "");

                                  if (DateDebut.HasValue && DateFin.HasValue)
                                      FacturesListe = factureService.FACTURE_SEARCH(societeCourante.IdSociete, DateDebut.Value, DateFin.Value, ClientFactireselect != null ? ClientFactireselect.IdClient : -1);
                                  else
                                      FacturesListe = factureService.FACTURE_GETLISTE_NEW(societeCourante.IdSociete, false);

                                 
                                  // Overviews = overviewService.GetOverview(UserConnected.Id);
                              }
                              this.MouseCursor = null;
                              this.IsBusy = false;
                              VisibleWaitingValidate = false;
                          }
                      };

                        worker.RunWorkerAsync();
                        
                    }
                }
        }

        bool canExecuteNonValable()
        {
          
            bool values = false;
            //if (societeCourante.IdSociete == GlobalDatas.DefaultCompany.IdSociete)
           // {
            if ( CurrentDroit.StatutSuppression || CurrentDroit.Developpeur )
            {
                if (FacturesListe != null && FacturesListe.Count > 0)
                {
                    foreach (FactureModel item in FacturesListe)
                        if (item.IsCheck == true && item.IdStatut == 14003)
                        {
                            values = true;
                            break;
                        }
                }


               

                if (GlobalDatas.IsArchiveSelected)
                    values = false;
            }
            return values;
        }
        #endregion



        #region REGION REFRESH GRIDVIEW



        void canRfresh()
        {

            loadDatas(societeCourante.IdSociete);

        }

        bool canExecuteRefresh()
        {
            return GlobalDatas.IsArchiveSelected==true?false:true;
        }
        #endregion


        #region Filtre Zone


        public void uncheckFilterClient(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (DtTableFactures != null || DtTableFactures.Rows .Count > 0)
                {
                    //FilterByCliFacturesListe = null;
                   // FilterByCliFacturesListe = FacturesListe;

                    DataTable dtabCurrent = DtblCacheListeFacture;
                    var de = OlddtblCacheListeFacture;


                    //var query = (from f in DtTableFactures.AsEnumerable()
                    //             where f.Field<string>("ClientFacture") ==value
                    //             select f).CopyToDataTable();

                   // DtTableFactures.Rows  //.FirstOrDefault(r => Convert.ToInt64(r["ID"]) == Convert.ToInt64(Row.Row["ID"]));

                    DataView dv = DtTableFactures.DefaultView;
                    dv.RowFilter = string.Format("ClientFacture <>'{0}'", value);
                    DataTable newdtbl = dv.ToTable();
                    DtTableFactures = newdtbl;
                    //foreach (DataRow row in DtTableFactures.Rows)
                    //{
                    //    if (row["ClientFacture"].ToString ().Equals (value ))
                    //      DtTableFactures.Rows.Remove(row);
                    //    DtTableFactures.AcceptChanges();
                    //}
                  

                  // ObservableCollection <FactureModel> newFactureListeListe = new ObservableCollection<FactureModel>();
                  //List<FactureModel> newListe= FacturesListe.ToList().FindAll(f => f.CurrentClient.NomClient!=value);
                  //if (newListe != null)
                  //{
                  //    foreach (FactureModel f in newListe)
                  //        newFactureListeListe.Add(f);
                  //}
                  //// var defaultFacture = FacturesListe.ToList();
                  // FacturesListe = null ;
                  // FacturesListe = newFactureListeListe;
                }

            }

        }

        public void checkFilterClient(string value)
        {
            if (!string.IsNullOrEmpty(value))
            {
                if (DtblCacheListeFacture != null)
                {
                    var query =( from f in DtblCacheListeFacture.AsEnumerable()
                                where f.Field<string>("ClientFacture").Equals(value)
                                select f).CopyToDataTable ();
                   // DtTableFactures.Rows.Clear();
                    foreach (DataRow row in query.Rows )
                        DtTableFactures.ImportRow (row );
                    DtTableFactures.AcceptChanges();
                   // List<FactureModel> newListe = CacheFacturesListeRecherche.ToList().FindAll(f => f.CurrentClient.NomClient.Equals(value));

                    //if (newListe != null)
                    //{
                    //    foreach (FactureModel f in newListe)
                    //        FacturesListe.Add(f);
                    //    //FacturesListe = null;
                    //}
                }
               
                    //= newFactureListeListe;
            }

        }


        //filtre par client saisie
        public void filter(string values)
        {
            if (!string.IsNullOrEmpty(values))
            {
                if (DtblCacheListeFacture != null && DtblCacheListeFacture.Rows.Count > 0)
                {
                    

                    DataRow[] nlignes = DtblCacheListeFacture.Select(string.Format("ClientFacture like '{0}%'", values.Trim()));
                    if (nlignes.Length == 0)
                        nlignes = DtblCacheListeFacture.Select(string.Format("ClientFacture like '%{0}'", values.Trim()));
                    if (nlignes.Length == 0)
                        nlignes = DtblCacheListeFacture.Select(string.Format("ClientFacture like '%{0}%'", values.Trim()));

                    DataTable filterDatatable = DtTableFactures.Clone();
                    foreach (DataRow rows in nlignes)
                        filterDatatable.ImportRow(rows);

                    DtTableFactures = filterDatatable;

                }

            }
            else
                DtTableFactures = DtblCacheListeFacture;
            
        }

        void filterByFactureNum(string values)
        {
            if (!string.IsNullOrEmpty(values))
            {
                if (DtblCacheListeFacture != null && DtblCacheListeFacture.Rows.Count > 0)
                {


                    DataRow[] nlignes = DtblCacheListeFacture.Select(string.Format("Numero_Facture like '{0}%'", values.Trim()));
                    if (nlignes.Length == 0)
                        nlignes = DtblCacheListeFacture.Select(string.Format("Numero_Facture like '%{0}'", values.Trim()));
                    if (nlignes.Length == 0)
                        nlignes = DtblCacheListeFacture.Select(string.Format("Numero_Facture like '%{0}%'", values.Trim()));

                    DataTable filterDatatable = DtTableFactures.Clone();
                    foreach (DataRow rows in nlignes)
                        filterDatatable.ImportRow(rows);

                    DtTableFactures = filterDatatable;

                }

            }
            else
                DtTableFactures = DtblCacheListeFacture;
        }


        void filterByDate(DateTime values)
        {

            DataRow[] nlignes = DtblCacheListeFacture.Select(string.Format("datecreation like'%{0}%' ", values.ToShortDateString ()));
            DataTable filterDatatable = DtTableFactures.Clone();
            foreach (DataRow rows in nlignes)
                filterDatatable.ImportRow(rows);
            DtTableFactures = filterDatatable;
        }




       public  void filtereee(string values)
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

        void filterByDateold(DateTime  values)
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



        void filterByFactureNumold(string values)
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



        protected void EventRefreshGridHistoric_EventRefreshList(object sender, EventArgs e)
        {
            EventRefreshGridHistoric data = sender as EventRefreshGridHistoric;

            if (!string.IsNullOrEmpty(data.typeOperation))
            {
                if (data.typeOperation.Equals("new") || data.typeOperation.Equals("update"))
                {
                    //operation nouvelle
                    loadDRefresh(societeCourante.IdSociete);
                }
            }
             else
            {
                // update
               // canRfresh();
                //DataRowView Row = data.facture as DataRowView;
                //DataRow newRow = FactureModel.ModelFactureByID(Convert.ToInt64(Row.Row["ID"]));

                //// var oldRow = DtTableFactures.Rows.Cast<DataRow>().FirstOrDefault(r => Convert .ToInt64 ( r["ID"]) ==Convert .ToInt64 ( Row.Row["ID"])); //.Select(string.Format("where ID = '{0}'", row.Row["ID"]));
                //DtTableFactures.Rows.Remove(Row.Row);
                //// DtTableFactures.Rows.Add(newRow);
                //DtTableFactures.ImportRow(newRow);
                //DtTableFactures.AcceptChanges();
                //DtTableFactures.ImportRow(Row.Row);
            }

        }

        void UpdateFiltreClient(ObservableCollection<FactureModel> listefactures)
        {
            List<ClientChecked> newClientList = new List<ClientChecked>();
            ClientChecked item = null;
            foreach (var fact in listefactures)
            {
                if (!newClientList.Exists(cli => cli.idClient == fact.CurrentClient.IdClient))
                {
                    item = new ClientChecked { idClient = fact.CurrentClient.IdClient, IsClientChecked = true, nomClient = fact.CurrentClient.NomClient };
                    newClientList.Add(item);
                }
            }
            ListeClient = newClientList;
        }

        void testeClientsNonFacturées()
        {
            BackgroundWorker worker = new BackgroundWorker();
            
            worker.DoWork += (o, args) =>
            {
            try
            {
                ListeClientFactureNomFacture = clientService.CLIENT_MONTHLY_CREATE(societeCourante.IdSociete);
                if (ListeClientFactureNomFacture != null && ListeClientFactureNomFacture.Count > 0)
                    OverviewResumVisible = true;
                else OverviewResumVisible = false;

                
                //if (listeClients != null && listeClients.Count>0)
                //{
                //    Communicator ctr = new Communicator();
                //    ctr.Clients = listeClients;
                //    EventArgs e1 = new EventArgs();
                //    ctr.OnChangeShowList(e1);
                //}
            }
            catch (Exception ex)
            {
                args.Result = ex.Message + " ;" + ex.InnerException + ";" + "ERREUR CHARGEMENT ";
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
                  
                  
                }
                else
                {

                }

            };

            worker.RunWorkerAsync();
        }

      

    }

    public class ClientChecked
    {
        public bool IsClientChecked { get; set; }
        public int idClient { get; set; }
        public string nomClient { get; set; }
    }

    public class FactureListe
    {
       public  Int64 ID { get; set; }
       public string numeroFacture { get; set; }
       public DateTime  dateCreation { get; set; }
       public string userCreate { get; set; }
       public string userUpdate { get; set; }

    }

    public class PeriodeDateArch
    {
        public string Annee { get; set; }
    }
}
