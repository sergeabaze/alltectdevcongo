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
using AllTech.FrameWork.Utils;


namespace AllTech.FacturationModule.ViewModel
{
    public class FactureSortieViewModel : ViewModelBase
    {
        #region FIELDS
        
     
        public readonly IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
        public readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private RelayCommand saveCommand;
        private RelayCommand cancelCommand;
        //private RelayCommand deleteCommand;
        private RelayCommand closeCommand;
        private RelayCommand rechercheCommand;
        private RelayCommand paiementCommand;
        private RelayCommand annulerPaiementCommand;
        private RelayCommand exportCommand;

        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;
        DroitModel _currentDroit;
        UtilisateurModel userConnected;

     

        FactureModel factureService;
        FactureModel _factureSelected;
        ObservableCollection<FactureModel> _facturesListe;
        ObservableCollection<FactureModel> _cacheFacturesListe;
        StatutModel statutservice;


        bool isEnablePrint;
        bool isCheckAll;
        bool btnSaveVisible;
        bool btnCalcelvisible;
        bool isCancelResearch;
        bool isShowAllInvoiceValidate;

       
   
        DateTime? dateSelected;
        DateTime? datePaiement;
        DateTime? dateDebut;
        DateTime? dateFin;

        bool cmbStevisible;
        List<SocieteModel> societeListe;
        SocieteModel societeService;
        SocieteModel currentSociete;

        public SocieteModel societeCourante;
        bool isResearchByDate = false;
        #endregion

        #region CONSTRUCTOR
        
       
        public FactureSortieViewModel()
       {
           //_regionManager = regionManager;
           //_container = container;
           
           //_injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
           ProgressBarVisibility = false;
           factureService = new FactureModel();
           statutservice = new StatutModel();
          
           societeCourante = GlobalDatas.DefaultCompany;
           UserConnected = GlobalDatas.currentUser;
           if (UserConnected != null)
               CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("sortie facture"));
           else CurrentDroit = new  DroitModel ();

           if (CurrentDroit.Lecture || CurrentDroit.Developpeur)
           {
               loadDatas();
               loadRight();
           }
             //try
             //{
             //    Utils.logUserActions(string.Format("<--UI Sortie Factures Ouverture "), "");
             //}
             //catch (Exception ex)
             //{
             //    MessageBox.Show("Probleme survenue lors du loggin de laction dans le fichier Log");
             //}
       }

        #endregion

        #region PROPERIES

        public SocieteModel CurrentSociete
        {
            get { return currentSociete; }
            set
            {
                currentSociete = value;
                if (value != null)
                    societeCourante = value;
                loadDatas();
                //societeCourante = GlobalDatas.DefaultCompany;
                OnPropertyChanged("CurrentSociete");
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

        public bool CmbStevisible
        {
            get { return cmbStevisible; }
            set
            {
                cmbStevisible = value;
                OnPropertyChanged("CmbStevisible");
            }
        }

        public bool IsShowAllInvoiceValidate
        {
            get { return isShowAllInvoiceValidate; }
            set { isShowAllInvoiceValidate = value;

            if (value)
            {
                loadDatas(3);
            }
            OnPropertyChanged("IsShowAllInvoiceValidate");
            }
        }

        public bool BtnCalcelvisible
        {
            get { return btnCalcelvisible; }
            set { btnCalcelvisible = value;
            this.OnPropertyChanged("BtnCalcelvisible");
            }
        }

        public bool IsCancelResearch
        {
            get { return isCancelResearch; }
            set { isCancelResearch = value;
            if (value)
            {
                if (CacheFacturesListe != null && CacheFacturesListe.Count > 0)
                    //FacturesListe = CacheFacturesListe;
                loadDatas();
                DateDebut = null;
                DateFin = null;
            }
            this.OnPropertyChanged("IsCancelResearch");
            }
        }

        public DateTime? DateDebut
        {
            get { return dateDebut; }
            set { dateDebut = value;
            this.OnPropertyChanged("DateDebut");
            }
        }

        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set { userConnected = value;
            this.OnPropertyChanged("UserConnected");
            }
        }

        public DateTime? DateFin
        {
            get { return dateFin; }
            set { dateFin = value;
            this.OnPropertyChanged("DateFin");
            }
        }

        public bool IsCheckAll
        {
            get { return isCheckAll; }
            set { isCheckAll = value;

            if (value)
            {
                ///afficher tous
                loadDatas();
            }
            else
            {
                //afficher uniquement les non sortie
                if (FacturesListe != null)
                {
                   ObservableCollection<FactureModel> ff=new ObservableCollection<FactureModel> ();
                    foreach (var f in FacturesListe)
                    {
                        if (f.IdStatut >= 3)
                            FacturesListe.Remove(f);
                        else ff.Add(f);
                    }
                    FacturesListe = ff;
                }

            }
            this.OnPropertyChanged("IsCheckAll");
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

        public bool BtnSaveVisible
        {
            get { return btnSaveVisible; }
            set
            {
                btnSaveVisible = value;
                OnPropertyChanged("BtnSaveVisible");
            }
        }

        public DateTime? DateSelected
        {
            get { 
                return dateSelected;
            
            }
            set
            {
                dateSelected = value;
                this.OnPropertyChanged("DateSelected");
            }

        }

        public DateTime? DatePaiement
        {
            get { return datePaiement; }
            set { datePaiement = value;
            this.OnPropertyChanged("DatePaiement");
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

         public ObservableCollection<FactureModel> FacturesListe
          {
              get { return _facturesListe; }
              set { _facturesListe = value;
              OnPropertyChanged("FacturesListe");
              }
          }

       
          public FactureModel FactureSelected
          {
              get { return _factureSelected; }
              set { _factureSelected = value ;
              //GlobalDatas.currentFacture = value;
              if (CacheFacturesListe == null)
              {
                  CacheFacturesListe = new ObservableCollection<FactureModel>();
                  if (value.IsCheck)
                  {
                     // if (value.ClienOk)
                          CacheFacturesListe.Add(value);
                  }
                  else CacheFacturesListe.Remove(value);
                 
              }
              else
              {
                  if (value != null)
                  {
                      if (value.IsCheck)
                      {
                          //if (value.ClienOk)
                          CacheFacturesListe.Add(value);
                          // else MessageBox.Show(" Ce client est incomplet");
                      }
                      else CacheFacturesListe.Remove(value);

                  }
              }
             // DateSelected = new DateTime();
              OnPropertyChanged("FactureSelected");
              }
          }

          public ObservableCollection<FactureModel> CacheFacturesListe
          {
              get { return _cacheFacturesListe; }
              set { _cacheFacturesListe = value;
             

              OnPropertyChanged("CacheFacturesListe");
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
                      this.saveCommand = new RelayCommand(param => this.canSave(), param => this.canExecuteSave());
                  }
                  return this.saveCommand;
              }
          }

          public ICommand CancelCommand
          {
              get
              {
                  if (this.cancelCommand == null)
                  {
                      this.cancelCommand = new RelayCommand(param => this.canCancelSortie(), param => this.canExecuteCancelSortie());
                  }
                  return this.cancelCommand;
              }
          }

          //

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

          public ICommand RechercheCommand
          {
              get
              {
                  if (this.rechercheCommand == null)
                  {
                      this.rechercheCommand = new RelayCommand(param => this.canRecherche(), param => this.canExecuteRecherche());
                  }
                  return this.rechercheCommand;
              }


          }

          public ICommand PaiementCommand
          {
              get
              {
                  if (this.paiementCommand == null)
                  {
                      this.paiementCommand = new RelayCommand(param => this.canPaiement(), param => this.canExecutePaiement());
                  }
                  return this.paiementCommand;
              }


          }

          public ICommand AnnulerPaiementCommand
          {
              get
              {
                  if (this.annulerPaiementCommand == null)
                  {
                      this.annulerPaiementCommand = new RelayCommand(param => this.canCancelPaiement(), param => this.canExecuteCancelPaiement());
                  }
                  return this.annulerPaiementCommand;
              }


          }

          public ICommand ExportCommand
          {
              get
              {
                  if (this.exportCommand == null)
                  {
                      this.exportCommand = new RelayCommand(param => this.canExport(), param => this.canExecuteExport());
                  }
                  return this.exportCommand;
              }


          }


          //
        #endregion


        #region METHODS

          #region LOAD REGION
          
      
          void loadRight()
          {
              if (CurrentDroit != null)
              {
                  if (CurrentDroit.Super || CurrentDroit.Developpeur || CurrentDroit.Proprietaire  )
                  {
                     
                     // BtnSaveVisible = true;
                      //BtnCalcelvisible = true;
                      societeService = new SocieteModel();
                     // SocieteListe = societeService.Get_SOCIETE_LIST();
                      if (GlobalDatas.listeCompany != null)
                          if (GlobalDatas.listeCompany.Count > 1)
                          {
                              SocieteListe = GlobalDatas.listeCompany;
                              CmbStevisible = true;
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
                  try
                  {

                   ObservableCollection<FactureModel> listeFacture = factureService.FACTURE_SORTIE_GETLISTE(societeCourante.IdSociete,DateTime.Now ,DateTime.Now ,1);
                
                   FacturesListe = listeFacture;
                   CacheFacturesListe = listeFacture;
                     
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
                      view.Owner = Application.Current.MainWindow;
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

          void loadDatas(int mode)
          {
              this.MouseCursor = Cursors.Wait;
              BackgroundWorker worker = new BackgroundWorker();
              this.IsBusy = true;
              ProgressBarVisibility = true;

              worker.DoWork += (o, args) =>
              {
                  try
                  {

                      ObservableCollection<FactureModel> listeFacture = factureService.FACTURE_SORTIE_GETLISTE(societeCourante.IdSociete, DateTime.Now, DateTime.Now, mode );
                      FacturesListe = listeFacture;
                      //CacheFacturesListe = listeFacture;

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
                      view.Owner = Application.Current.MainWindow;
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

          #endregion

          #region CLOSE REGION
          private void canClose()
          {
              Communicator ctr = new Communicator();
              ctr.contentVue = "fsortie";
              EventArgs e1 = new EventArgs();
              ctr.OnChangeText(e1);
                 
          }
          #endregion


        
         

          void AddFactureCheck()
          {

          }

          #region SORTIE REGION
          
        
          private void canSave()
          {
              bool testedate=true  ;
              try
              {
                  string factureDejaValider = string.Empty;
                  string factureDejaSuspendu = string.Empty;
                  string factureDejaNonValable = string.Empty;
                  string factureNonValide = string.Empty;
                  string factureDateIncorrect = string.Empty;
                  bool isErrorMessage = false;
                  bool isdateincorrect = false;

                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION  VALIDATION";
                        messageBox.ViewModel.Message = "Voulez vous Valider Ce(s) Sorties?";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            
                                StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "4");

                                var queryChecked = (from f in FacturesListe.AsEnumerable()
                                     where  f.IsCheck ==true 
                                     select f).ToList ();

                                if (queryChecked.Count > 0)
                                {
                                    foreach (var ligneFacture in queryChecked)
                                    {
                                    
                                            if (ligneFacture.CurrentStatut.IdStatut ==14003)
                                            {
                                                double differenceDate = (DateSelected.Value - (DateTime.Parse(ligneFacture.DateCloture.Value.ToShortDateString()))).TotalDays;
                                                if (differenceDate >= 0)
                                                {
                                                    factureService.FACTURE_SORTIE(ligneFacture.IdFacture, (DateTime)DateSelected, newStatut.IdStatut, UserConnected.Id, true);
                                                    testedate = true;
                                                    Utils.logUserActions(string.Format("<--UI Sortie Factures Ouverture-- modification statut <<sortie> de la facture  {0}  par   {1} ",ligneFacture.NumeroFacture , UserConnected.Loggin), "");
                                                }
                                                else
                                                {
                                                    factureDateIncorrect = factureDateIncorrect + "\n" + ligneFacture.NumeroFacture;
                                                    isdateincorrect = true;
                                                }
                                            }
                                            else
                                            {
                                                if (ligneFacture.CurrentStatut.IdStatut ==14004)
                                                {
                                                    factureDejaValider = factureDejaValider + "\n" + ligneFacture.NumeroFacture;
                                                    isErrorMessage = true;
                                                }
                                                if (ligneFacture.CurrentStatut.IdStatut == 14005)
                                                {
                                                    factureDejaSuspendu = factureDejaSuspendu + "\n" + ligneFacture.NumeroFacture;
                                                    isErrorMessage = true;
                                                }
                                                if (ligneFacture.CurrentStatut.IdStatut ==14006)
                                                {
                                                    factureDejaNonValable = factureDejaNonValable + "\n" + ligneFacture.NumeroFacture;
                                                    isErrorMessage = true;
                                                }
                                            }
                                           
                                      


                                    }

                                    if (isErrorMessage)
                                    {
                                        bool isvalues = false;
                                        string messageError = "les factures suivantes ne pourront pas être Validées";

                                        if (!string.IsNullOrEmpty(factureDejaValider))
                                        {
                                            messageError = messageError + "\n" + factureDejaValider + "\n Factures déja paier";
                                            isvalues = true;
                                        }

                                        if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                        {
                                            messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures Suspendus";
                                            isvalues = true;
                                        }

                                        if (!string.IsNullOrEmpty(factureDejaNonValable))
                                        {
                                            messageError = messageError + "\n" + factureDejaNonValable + "\n Non Valable";
                                            isvalues = true;
                                        }

                                        if (isdateincorrect)
                                        {
                                            messageError = messageError + "\n" + factureDateIncorrect + "\n La date sélectionné est inférieur à la date de validation";
                                            isvalues = true;
                                        }

                                        if (isvalues)
                                         MessageBox.Show(messageError);
                                    }
                                    else
                                    {
                                       
                                        if (isdateincorrect)
                                        {
                                            string messageError = "Problème lors de la validation de l'operation";
                                            messageError = messageError + "\n" + factureDateIncorrect + "\n La date sélectionné est inférieur à la date de validation";

                                            MessageBox.Show(messageError);
                                            testedate = false;
                                            return;
                                        }

                                    }

                                    if (testedate)
                                    {
                                        if (isResearchByDate)
                                        {
                                            FacturesListe = factureService.FACTURE_SORTIE_GETLISTE(societeCourante.IdSociete, DateDebut.Value, DateFin.Value, 2);
                                            //FacturesListe = listeFacture;
                                        }
                                        else
                                            loadDatas();

                                        CacheFacturesListe = null;
                                        DateSelected = null;
                                    }

                                }
                                else
                                {
                                    testedate = false;
                                    CustomExceptionView view = new CustomExceptionView();
                                    view.Owner = Application.Current.MainWindow;
                                    view.Title = "MESSAGE INFORMATION DATE VALIDATION";
                                    view.ViewModel.Message = "Pas de factures sélectionnées pour cette opération";
                                    view.ShowDialog();
                                }



                           
                        }
                  

              }
              catch (Exception ex)
              {
                  CustomExceptionView view = new CustomExceptionView();
                  view.Owner = Application.Current.MainWindow;
                  view.Title = "MESSAGE INFORMATION MODIFICATION";
                  view.ViewModel.Message = ex.Message;
                  view.ShowDialog();
                
              }
          }

          bool canExecuteSave()
          {
              bool values = false;
            
                  if ( CurrentDroit.Edition || CurrentDroit.Developpeur  )
                  {
                      if (FacturesListe != null)
                      {
                          var queryChecked = (from f in FacturesListe.AsEnumerable()
                                              where f.IsCheck == true
                                              select f).ToList();
                          if (queryChecked.Count > 0)
                              if (DateSelected.HasValue)
                                  values = true;
                      }
                  }
                  else values = false;
            

              return values;
             // return DateSelected.HasValue ? true : false;
              //return CacheFacturesListe != null ? (CacheFacturesListe.Count > 0 ? (DateSelected.HasValue ==true ?true :false ) : false) : false;
          }

          void canCancelSortie()
          {
              bool testeValues = false;
               StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION  IVALIDATION";
                        messageBox.ViewModel.Message = "Voulez vous Annuler la sortie de cette Facture?";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");

                            var queryChecked = (from f in FacturesListe.AsEnumerable()
                                                where int.Parse(f.CurrentStatut.CourtDesc) ==4
                                                     && f.IsCheck == true
                                                select f).ToList();
                            if (queryChecked.Count > 0)
                            {
                                foreach (var ligneFacture in queryChecked)
                                {
                                    factureService.FACTURE_SORTIE(ligneFacture.IdFacture, (DateSelected == null ? DateTime.Now : (DateTime)DateSelected), newStatut.IdStatut, UserConnected.Id, false);
                                    testeValues = true;
                                }

                                if (testeValues)
                                {
                                    loadDatas();
                                    CacheFacturesListe = null;
                                    //FactureSelected = null;
                                }
                            }
                            else
                            {
                                CustomExceptionView view = new CustomExceptionView();
                                view.Owner = Application.Current.MainWindow;
                                view.Title = "MESSAGE INFORMATION DATE VALIDATION";
                                view.ViewModel.Message = "Pas de factures sélectionnées pour cette opération";
                                view.ShowDialog();
                            }
                           
                          
                            
                           
                        }
          }

          bool canExecuteCancelSortie()
          {
              bool values = false;
              if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
              {
                  if (FacturesListe != null)
                  {
                      var queryChecked = (from f in FacturesListe.AsEnumerable()
                                          where f.IsCheck == true && f.DateSortie.HasValue
                                          select f).ToList();
                      if (queryChecked.Count > 0)
                          values = true;
                  }
              }
              return values;
              //return CacheFacturesListe != null ? (CacheFacturesListe.Count > 0 ? (FactureSelected != null ? (FactureSelected.DatePaiement == null ? (FactureSelected.DateSortie.HasValue==true  ?true :false ) : false) : false) : false) : false; ;
          }

          #endregion

          #region RESEARCH REGION

         

          void canRecherche()
          {
              if (DateDebut.HasValue && DateFin.HasValue)
              {
                  this.MouseCursor = Cursors.Wait;
                  BackgroundWorker worker = new BackgroundWorker();
                  this.IsBusy = true;
                  ProgressBarVisibility = true;
                  worker.DoWork += (o, args) =>
                {
                    try
                    {

                        ObservableCollection<FactureModel> listeFacture = factureService.FACTURE_SORTIE_GETLISTE(societeCourante.IdSociete, DateDebut.Value, DateFin.Value,2);
                        //ObservableCollection<FactureModel> newliste = new ObservableCollection<FactureModel>();

                        //var query = (from f in listeFacture.AsEnumerable()
                        //             where int.Parse(f.CurrentStatut.CourtDesc) >= 3
                        //                 && f.DateCreation.Value >= DateDebut.Value
                        //                  && f.DateCreation.Value <= DateFin.Value
                        //             select f);

                        //foreach (var fact in query)
                        //{

                           
                        //    newliste.Add(fact);
                        //}

                        FacturesListe = listeFacture;
                        CacheFacturesListe = listeFacture;
                        isResearchByDate = true;
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
                          view.Owner = Application.Current.MainWindow;
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
                      IsCancelResearch = false;
                  };

                  worker.RunWorkerAsync();

              }
              else MessageBox.Show("Préciser La période");


            
              
          }
          bool canExecuteRecherche()
          {
              bool values = false;
              if (CurrentDroit.Super || CurrentDroit.Lecture )
              {
                  if (FacturesListe != null)
                  {
                      if (DateDebut.HasValue == true)
                          if (DateFin.HasValue == true)
                              values = true;
                  }
                  //return FacturesListe != null ? (DateDebut.HasValue == true ? (DateFin.HasValue == true ? true : false) : false) : false;
              }
              else
                  values = false;
              return values;
          }

          #endregion

          #region PAIEMENT REGION


          void canPaiement()
          {
              bool testevalues = false;
              string factureDejaValider = string.Empty;
              string factureDejaSuspendu = string.Empty;
              string factureDejaNonValable = string.Empty;
              string factureNonValide = string.Empty;
              string factureDateIncorrect = string.Empty;
              bool isErrorMessage = false;
              bool isdateincorrect = false;

               StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION  IVALIDATION";
                        messageBox.ViewModel.Message = "Confirmer le Paiement de cette facture ?";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            var queryChecked = (from f in FacturesListe.AsEnumerable()
                                                    where  f.IsCheck == true
                                                select f).ToList();
                            if (queryChecked.Count > 0)
                            {
                                foreach (var ligneFacture in queryChecked)
                                {
                                   
                                        if (int.Parse(ligneFacture.CurrentStatut.CourtDesc) == 4)
                                        {
                                            double differenceDate = (DatePaiement.Value - (DateTime.Parse(ligneFacture.DateSortie.Value.ToShortDateString()))).TotalDays;
                                            if (differenceDate >= 0)
                                            {
                                                factureService.FACTURE_PAIEMENT(ligneFacture.IdFacture, DatePaiement.Value, UserConnected.Id, true);
                                                testevalues = true;
                                            }
                                            else
                                            {
                                                factureDateIncorrect = factureDateIncorrect + "\n" + ligneFacture.NumeroFacture;
                                                isdateincorrect = true;
                                            }
                                        }
                                        else
                                        {
                                            if (ligneFacture.CurrentStatut.CourtDesc == "3")
                                            {
                                                factureDejaValider = factureDejaValider + "\n" + ligneFacture.NumeroFacture;
                                                isErrorMessage = true;
                                            }
                                            if (ligneFacture.CurrentStatut.CourtDesc == "5")
                                            {
                                                factureDejaSuspendu = factureDejaSuspendu + "\n" + ligneFacture.NumeroFacture;
                                                isErrorMessage = true;
                                            }
                                            if (ligneFacture.CurrentStatut.CourtDesc == "6")
                                            {
                                                factureDejaNonValable = factureDejaNonValable + "\n" + ligneFacture.NumeroFacture;
                                                isErrorMessage = true;
                                            }
                                        }

                                      
                                  

                                }

                                if (isErrorMessage)
                                {
                                    string messageError = "les factures suivantes ne pourront pas être Validées";
                                    bool isvalues = false;
                                    if (!string.IsNullOrEmpty(factureDejaValider))
                                    {
                                        messageError = messageError + "\n" + factureDejaValider + "\n Factures Validées pas encore sortie";
                                        isvalues =true ;
                                    }

                                    if (!string.IsNullOrEmpty(factureDejaSuspendu))
                                    {
                                        messageError = messageError + "\n" + factureDejaSuspendu + "\n Factures Suspendus";
                                        isvalues = true;
                                    }

                                    if (!string.IsNullOrEmpty(factureDejaNonValable))
                                    {
                                        messageError = messageError + "\n" + factureDejaNonValable + "\n Non Valable";
                                        isvalues = true;
                                    }
                                    if (isdateincorrect)
                                    {
                                        messageError = messageError + "\n" + factureDateIncorrect + "\n La date sélectionné est inférieur à la date de sortie";
                                        isvalues = true;
                                    }
                                    MessageBox.Show(messageError);
                                    return;
                                }
                                else
                                {
                                    if (isdateincorrect)
                                    {
                                        string messageError = "les factures suivantes ne pourront pas être Validées";
                                        messageError = messageError + "\n" + factureDateIncorrect + "\n La date sélectionné est inférieur à la date de sortie";
                                        MessageBox.Show(messageError);
                                    }
                                }

                                if (testevalues)
                                {
                                    loadDatas();
                                    CacheFacturesListe = null;
                                    DatePaiement = null;
                                }

                            }
                            else
                            {
                                testevalues = false ;
                                CustomExceptionView view = new CustomExceptionView();
                                view.Owner = Application.Current.MainWindow;
                                view.Title = "MESSAGE INFORMATION DATE VALIDATION";
                                view.ViewModel.Message = "Pas de factures sélectionnées pour cette opération";
                                view.ShowDialog();
                            }

                           
                              
                        }
          }

        bool canExecutePaiement()
        {
            bool values = false;

           
                if ( CurrentDroit.Edition || CurrentDroit.Developpeur )
                {
                    if (FacturesListe != null)
                    {
                        var queryChecked = (from f in FacturesListe.AsEnumerable()
                                            where f.IsCheck == true && f.DateSortie.HasValue
                                            select f).ToList();
                        if (queryChecked.Count > 0)
                            if (DatePaiement.HasValue)
                                values = true;
                    }
          

                

            }
            return values;

          //  return DatePaiement.HasValue ? true : false;
           // return FactureSelected != null ? (FactureSelected.IdFacture > 0 ? (FactureSelected.DateSortie.HasValue==true ?true :false ) : false) : false;
        }

        void canCancelPaiement()
        {
            bool testeValues = false;
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = "INFORMATION  IVALIDATION";
                        messageBox.ViewModel.Message = "Confirmer l'annulation du Paiement de cette facture";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            var queryChecked = (from f in FacturesListe.AsEnumerable()
                                                where int.Parse(f.CurrentStatut.CourtDesc) == 4
                                                     && f.IsCheck == true
                                                select f).ToList();
                            if (queryChecked.Count > 0)
                            {
                                foreach (var ligneFacture in queryChecked)
                                {
                                    factureService.FACTURE_PAIEMENT(ligneFacture.IdFacture, DateTime.Now, UserConnected.Id, false);
                                    testeValues = true;
                                }
                            }
                            else
                            {
                                testeValues = false;
                            }

                            if (testeValues)
                            {
                                loadDatas();
                                //FactureSelected = null;
                                CacheFacturesListe = null;
                            }
                        }

        }

        bool canExecuteCancelPaiement()
        {
            //return FactureSelected != null ? (FactureSelected.IdFacture > 0 ? (FactureSelected.DatePaiement .HasValue==true ?true :false ) : false) : false;
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
            {
                if (FacturesListe != null)
                {
                    var queryChecked = (from f in FacturesListe.AsEnumerable()
                                        where f.IsCheck == true && f.DatePaiement.HasValue

                                        select f).ToList();
                    if (queryChecked.Count > 0)
                        values = true;
                }
            }
            return values;
        }

        #endregion

        #endregion


        #region REGION EXPORT

        void canExport()
        {
             try
            {
            CommonModule.ExportfactureSortiesToExcel(FacturesListe.ToList (), "", "");
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

        bool canExecuteExport()
        {
            if (CurrentDroit.Execution || CurrentDroit.Developpeur)
            {
                return FacturesListe != null ? true : false;
            }
            else return false;
        }
        #endregion

    }
}
