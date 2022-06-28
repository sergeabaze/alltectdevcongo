using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;
using System.Windows.Input;
using AllTech.FrameWork.Utils;
using System.ComponentModel;
using System.Data;
using AllTech.FrameWork.Views;
using System.Windows.Forms;
using System.Windows;

using System.Collections.ObjectModel;
using AllTech.FacturationModule.Views.Modal;
using AllTech.FacturationModule.Views;



namespace AllTech.FacturationModule.ViewModel
{
    public class AdminViewModel : ViewModelBase
    {
        #region FIELDS

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand runArchiveCommand;
        private RelayCommand cancelArchiveCommand;

        private RelayCommand cancelFreecliSelectCommand;
        private RelayCommand runFreecliSelectCommand;


        private RelayCommand runFreeProductCommand;
        private RelayCommand cancelFreeProductCommand;

        private RelayCommand closeCommand;
        private RelayCommand adminDatabaseCommand;
        private RelayCommand fichierRepertoireCommand;
        
        private RelayCommand updateScripteAdminDatabaseCommand;
        private RelayCommand updateDatasAdminDatabaseCommand;
        ParametresModel _parametresService;
        ParametresModel _currentParametres;

        DroitModel droitFormulaire = null;
        UtilisateurModel userConnected;
        SocieteModel societeCourante;
        UtilisateurModel UserConnected;

        bool tabItemBackUp;
        bool tabitemComputer;
        List<Computer> computeLists;
        Computer computerSelected;
        DataTable dtbleBackLogs;
        DateTime dateMin;
        DateTime dateMax;
        DateTime? datefrom;
        DateTime? dateTo;

        ArchivePeview archivePrieviewSelect;
        List<ArchivePeview> archivePrieviewSelects;
        List<ArchivePeview> archivePrieviewList;
        string messageTraitement;
        string colorMessage;
        DroitModel CurrentDroit;
       
        private bool isBusy;
        private bool isloading;

       
        bool _progressBarVisibility;
        bool _progressBarloadVisibility;
        bool _progressBarVisibilityUpdate;
        bool _progressBarProductVisibility;

      
        private bool isBusyUpdate;

      

        private System.Windows.Input.Cursor mouseCursor;
        bool butonEnable;
        bool butonCancelRunnBAckupEnable;
        bool deleteAllFreeclient;

      

      

        List<ListeArchive> listeArchives;
        AdminModalBackUp viewModal = null;
        AdminFreeCustomer customView = null;
        AdminFreeProduct viewporduct = null;


        List<ListeFreeClient> previousFreeCustomers;
        List<ListeFreeClient> previousFreeCustomersselects = new List<ListeFreeClient>();
        ListeFreeClient previousFreeCustomersSelect;

        List<FreeProduct> previousFreeProduct;
        FreeProduct prviousfreeproductSelected;
        List<FreeProduct> freeproductListselect=new List<FreeProduct> ();

       
        bool butonProductEnable;
        bool butonCancelfreeproduct;
        bool deletecheckAllFreeproduct;
        bool isvisiblebtndatabase;

       
      
       

      
        ProgressWindow progress;
        string CheminFichier;
        bool IsfileSelect;
        #endregion

        #region CONSTRUCTOR

        public AdminViewModel()
        {
            _currentParametres = new ParametresModel();
            CurrentParametres = GlobalDatas.dataBasparameter;
            societeCourante = GlobalDatas.DefaultCompany;
            UserConnected = GlobalDatas.currentUser;
           // var listeVues = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("maintenance")).SousDroits ;
            CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("archivage"));
          
            ButonCancelRunnBAckupEnable = false;
            ButonProductEnable = true;
            ButonEnable = true;
            LoadBackUpLog();

            if (CurrentDroit.Developpeur)
                Isvisiblebtndatabase = true;
        }

        #endregion

        #region PROPERTIES

        public bool Isvisiblebtndatabase
        {
            get { return isvisiblebtndatabase; }
            set { isvisiblebtndatabase = value;
            this.OnPropertyChanged("Isvisiblebtndatabase");
            }
        }

        public List<FreeProduct> FreeproductListselect
        {
            get { return freeproductListselect; }
            set { freeproductListselect = value;
            this.OnPropertyChanged("FreeproductListselect");
            }
        }

        public bool DeletecheckAllFreeproduct
        {
            get { return deletecheckAllFreeproduct; }
            set { deletecheckAllFreeproduct = value;
            if (value)
            {
                if (PreviousFreeProduct != null && PreviousFreeProduct.Count > 0)
                {
                    FreeproductListselect=null;
                    FreeproductListselect=PreviousFreeProduct;

                    for (int i = 0; i < PreviousFreeProduct.Count - 1; i++)
                    {
                        FreeproductListselect[i].IsChecked = true;
                       // FreeproductListselect.Add(PreviousFreeProduct[i]);
                    }

                    PreviousFreeProduct = null;
                    PreviousFreeProduct = FreeproductListselect;
                   

                    //foreach (FreeProduct prod in PreviousFreeProduct)
                    //{
                    //    prod.IsChecked = true;

                    //    if (!FreeproductListselect.Exists(p=>p.Id==prod.Id))
                    //    FreeproductListselect.Add(prod);
                    //}
                    //PreviousFreeProduct = FreeproductListselect;
                   
                }

            }
            else
            {
                if (PreviousFreeProduct != null && PreviousFreeProduct.Count > 0)
                {
                    foreach (FreeProduct prod in PreviousFreeProduct)
                    {
                        prod.IsChecked = false;
                        if (FreeproductListselect.Exists(p => p.Id == prod.Id))
                            FreeproductListselect.Remove(prod);
                    }

                    for (int i = 0; i < PreviousFreeProduct.Count - 1; i++)
                        PreviousFreeProduct[i].IsChecked = false;

                    PreviousFreeProduct = PreviousFreeProduct;
                }

            }
            this.OnPropertyChanged("DeletecheckAllFreeproduct");
            }
        }
        public bool ProgressBarProductVisibility
        {
            get { return _progressBarProductVisibility; }
            set { _progressBarProductVisibility = value;
            this.OnPropertyChanged("ProgressBarProductVisibility");
            }
        }

        public bool ButonProductEnable
        {
            get { return butonProductEnable; }
            set { butonProductEnable = value;
            this.OnPropertyChanged("ButonProductEnable");
            }
        }


        public bool ButonCancelfreeproduct
        {
            get { return butonCancelfreeproduct; }
            set { butonCancelfreeproduct = value;
            this.OnPropertyChanged("ButonCancelfreeproduct");
            }
        }


        public List<FreeProduct> PreviousFreeProduct
        {
            get { return previousFreeProduct; }
            set { previousFreeProduct = value;
            this.OnPropertyChanged("PreviousFreeProduct");
            }
        }


        public FreeProduct PrviousfreeproductSelected
        {
            get { return prviousfreeproductSelected; }
            set { prviousfreeproductSelected = value;

            if (value != null)
            {
                if (value.IsChecked)
                {
                    if (!FreeproductListselect.Exists(p => p.Id == value.Id))
                    {
                        FreeproductListselect.Add(value);
                        // PreviousFreeProduct.Find(p => p.Id == value.Id).IsChecked = true;
                    }
                }
                else
                {
                    if (FreeproductListselect.Exists(p => p.Id == value.Id))
                    {
                        FreeproductListselect.Remove(value);
                        // PreviousFreeProduct.Find(p => p.Id == value.Id).IsChecked = false;
                    }
                }
            }

            this.OnPropertyChanged("PrviousfreeproductSelected");
            }
        }


        public bool DeleteAllFreeclient
        {
            get { return deleteAllFreeclient; }
            set { deleteAllFreeclient = value;

            if (value)
            {
                if (PreviousFreeCustomers != null && PreviousFreeCustomers.Count > 0)
                {
                    for (int i = 0; i < PreviousFreeCustomers.Count - 1; i++)
                    {
                        PreviousFreeCustomers[i].Checked = true;
                        PreviousFreeCustomersselects.Add(PreviousFreeCustomers[i]);
                    }
                }
            }
            else
            {
                if (PreviousFreeCustomers != null && PreviousFreeCustomers.Count > 0)
                {
                    for (int i = 0; i < PreviousFreeCustomers.Count - 1; i++)
                    {
                        PreviousFreeCustomers[i].Checked = false;
                        PreviousFreeCustomersselects.Remove(PreviousFreeCustomers[i]);
                    }
                }
            }
            this.OnPropertyChanged("DeleteAllFreeclient");
            }
        }

        public bool ButonCancelRunnBAckupEnable
        {
            get { return butonCancelRunnBAckupEnable; }
            set { butonCancelRunnBAckupEnable = value;
            this.OnPropertyChanged("ButonCancelRunnBAckupEnable");
            }
        }
        public bool IsBusyUpdate
        {
            get { return isBusyUpdate; }
            set { isBusyUpdate = value;
            this.OnPropertyChanged("IsBusyUpdate");
            }
        }

        public bool ProgressBarVisibilityUpdate
        {
            get { return _progressBarVisibilityUpdate; }
            set { _progressBarVisibilityUpdate = value;
            this.OnPropertyChanged("ProgressBarVisibilityUpdate");
            }
        }

        public List<ListeFreeClient> PreviousFreeCustomersselects
        {
            get { return previousFreeCustomersselects; }
            set { previousFreeCustomersselects = value;
            this.OnPropertyChanged("PreviousFreeCustomersselects");
            }
        }


        public ListeFreeClient PreviousFreeCustomersSelect
        {
            get { return previousFreeCustomersSelect; }
            set { 
                   if (value!=null ){
                       if (value.Checked)
                       {
                           if (PreviousFreeCustomersselects == null)
                               PreviousFreeCustomersselects = new List<ListeFreeClient>();
                           //if (value.IdStatut == 14001 || value.IdStatut == 14006)
                           //{
                           //    System.Windows.Forms.MessageBox.Show("Attention vous ne pouvez ajouter Ce Statut pour  archivage !", "Traitement Archivage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                           //    return;
                           //}
                           PreviousFreeCustomersselects.Add(value);
                       }
                       else
                       {
                           if (PreviousFreeCustomersselects.Exists(ar => ar.IDClient == value.IDClient))
                           {
                               ListeFreeClient arch = PreviousFreeCustomersselects.FirstOrDefault(ar => ar.IDClient == value.IDClient);
                               if (arch != null)
                               {
                                  // arch.Backcolor = "Blue";
                                   PreviousFreeCustomersselects.Remove(arch);
                               }
                           }
                       }
                   }
                
                previousFreeCustomersSelect = value;
            this.OnPropertyChanged("PreviousFreeCustomersSelect");
            }
        }
       


        public List<ListeFreeClient> PreviousFreeCustomers
        {
            get { return previousFreeCustomers; }
            set { previousFreeCustomers = value;
            this.OnPropertyChanged("PreviousFreeCustomers");
            }
        }

        public List<ListeArchive> ListeArchives
        {
            get { return listeArchives; }
            set { listeArchives = value;
            this.OnPropertyChanged("ListeArchives");
            }
        }

        public string ColorMessage
        {
            get { return colorMessage; }
            set { colorMessage = value;
            this.OnPropertyChanged("ColorMessage");
            }
        }

        public string MessageTraitement
        {
            get { return messageTraitement; }
            set { messageTraitement = value;
            this.OnPropertyChanged("MessageTraitement");
            }
        }

        public bool ButonEnable
        {
            get { return butonEnable; }
            set { butonEnable = value;
            this.OnPropertyChanged("ButonEnable");
            }
        }

        public bool Isloading
        {
            get { return isloading; }
            set { isloading = value;
            this.OnPropertyChanged("Isloading");
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

        public System.Windows.Input.Cursor MouseCursor
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

        public bool ProgressBarloadVisibility
        {
            get { return _progressBarloadVisibility; }
            set
            {
                _progressBarloadVisibility = value;
                OnPropertyChanged("ProgressBarloadVisibility");
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

        public List<ArchivePeview> ArchivePrieviewList
        {
            get { return archivePrieviewList; }
            set { archivePrieviewList = value;
            OnPropertyChanged("ArchivePrieviewList");
            }
        }

        public ArchivePeview ArchivePrieviewSelect
        {
            get { return archivePrieviewSelect; }
            set { 
            if (value!=null )
            {
                if (value.Chechked)
                {
                    if (ArchivePrieviewSelects == null) 
                        ArchivePrieviewSelects = new List<ArchivePeview>();
                    if (value.IdStatut == 14001 || value.IdStatut == 14002)
                    {
                        System.Windows.Forms.MessageBox.Show("Attention vous ne pouvez ajouter Ce Statut pour  archivage !", "Traitement Archivage", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                        return;
                    }
                    ArchivePrieviewSelects.Add(value);
                    archivePrieviewSelect = value;
                }
                else
                {
                    if (ArchivePrieviewSelects != null)
                    {
                        if (!value.Chechked)
                        {
                            if (ArchivePrieviewSelects.Exists(ar => ar.IdStatut == value.IdStatut))
                            {
                                ArchivePeview arch = ArchivePrieviewSelects.FirstOrDefault(ar => ar.IdStatut == value.IdStatut);
                                if (arch != null)
                                {
                                    arch.Backcolor = "Blue";
                                    ArchivePrieviewSelects.Remove(arch);
                                }
                            }
                        }
                    }
                    else
                    {
                        ArchivePrieviewSelects = new List<ArchivePeview>();
                        ArchivePrieviewSelects.Add(value);
                    }
                    archivePrieviewSelect = value;

                }

            }
            OnPropertyChanged("ArchivePrieviewSelect");
            }
        }

        public List<ArchivePeview> ArchivePrieviewSelects
        {
            get { return archivePrieviewSelects; }
            set { archivePrieviewSelects = value;
            OnPropertyChanged("ArchivePrieviewSelects");
            }
        }

        public DateTime? DateTo
        {
            get { return dateTo; }
            set { dateTo = value;
            OnPropertyChanged("DateTo");
            }
        }
        public DateTime? Datefrom
        {
            get { return datefrom; }
            set { datefrom = value;
            OnPropertyChanged("Datefrom");
            }
        }

        public DateTime DateMin
        {
            get { return dateMin; }
            set { dateMin = value;
            OnPropertyChanged("DateMin");
            }
        }

        public DateTime DateMax
        {
            get { return dateMax; }
            set
            {
                dateMax = value;
                OnPropertyChanged("DateMax");
            }
        }

        public DataTable DtbleBackLogs
        {
            get { return dtbleBackLogs; }
            set { dtbleBackLogs = value;
            OnPropertyChanged("DtbleBackLogs");
            }
        }

        public List<Computer> ComputeLists
        {
            get { return computeLists; }
            set { computeLists = value;
            OnPropertyChanged("ComputeLists");
            }
        }

        public Computer ComputerSelected
        {
            get { return computerSelected; }
            set { computerSelected = value;
            OnPropertyChanged("ComputerSelected");
            }
        }

        public bool TabitemComputer
        {
            get { return tabitemComputer; }
            set { tabitemComputer = value;
            OnPropertyChanged("TabitemComputer");
            }
        }

        public bool TabItemBackUp
        {
            get { return tabItemBackUp; }
            set { tabItemBackUp = value;
            OnPropertyChanged("TabItemBackUp");
            }
        }

        public ParametresModel CurrentParametres
        {
            get { return _currentParametres; }
            set { _currentParametres = value;
            OnPropertyChanged("CurrentParametres");
            }
        }

        #endregion

        #region ICOMMAND

       // private RelayCommand cancelFreecliSelectCommand;

        // private RelayCommand runFreeProductCommand;
        // private RelayCommand cancelFreecliSelectCommand;


        public ICommand CancelFreeProductCommand
        {
            get
            {
                if (this.cancelFreeProductCommand == null)
                {
                    this.cancelFreeProductCommand = new RelayCommand(param => this.canCelFreeProduct());
                }
                return this.cancelFreeProductCommand;
            }
        }

        public ICommand RunFreeProductCommand
        {
            get
            {
                if (this.runFreeProductCommand == null)
                {
                    this.runFreeProductCommand = new RelayCommand(param => this.canRunFreeProductSave(), param => this.canExecuteRunFreeproduct());
                }
                return this.runFreeProductCommand;
            }
        }


        public ICommand CancelFreecliSelectCommand
        {
            get
            {
                if (this.cancelFreecliSelectCommand == null)
                {
                    this.cancelFreecliSelectCommand = new RelayCommand(param => this.canCelFreeClient());
                }
                return this.cancelFreecliSelectCommand;
            }
        }

        public ICommand CancelArchiveCommand
        {
            get
            {
                if (this.cancelArchiveCommand == null)
                {
                    this.cancelArchiveCommand = new RelayCommand(param => this.canCelRunMackUp());
                }
                return this.cancelArchiveCommand;
            }
        }

        public ICommand FichierRepertoireCommand
        {
            get
            {
                if (this.fichierRepertoireCommand == null)
                {
                    this.fichierRepertoireCommand = new RelayCommand(param => this.canRepertoire());
                }
                return this.fichierRepertoireCommand;
            }
        }


        public ICommand RunFreecliSelectCommand
        {
            get
            {
                if (this.runFreecliSelectCommand == null)
                {
                    this.runFreecliSelectCommand = new RelayCommand(param => this.canRunFreeSave(), param => this.canExecuteRunFree());
                }
                return this.runFreecliSelectCommand;
            }
        }

        public ICommand RunArchiveCommand
        {
            get
            {
                if (this.runArchiveCommand == null)
                {
                    this.runArchiveCommand = new RelayCommand(param => this.canRunArchiveSave(), param => this.canExecuteRunArchive());
                }
                return this.runArchiveCommand;
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

        public ICommand AdminDatabaseCommand
        {
            get
            {
                if (this.adminDatabaseCommand == null)
                {
                    this.adminDatabaseCommand = new RelayCommand(param => this.canSHowDatabaseAdmin(), param => this.canExecuteShowDatabaseAdmin());
                }
                return this.adminDatabaseCommand;
            }
        }

        public ICommand UpdateScripteAdminDatabaseCommand
        {
            get
            {
                if (this.updateScripteAdminDatabaseCommand == null)
                {
                    this.updateScripteAdminDatabaseCommand = new RelayCommand(param => this.canUpdateScriptDatabaseAdmin(), param => this.canExecuteUpdateScriptDatabaseAdmin());
                }
                return this.updateScripteAdminDatabaseCommand;
            }
        }

        public ICommand UpdateDatasAdminDatabaseCommand
        {
            get
            {
                if (this.updateDatasAdminDatabaseCommand == null)
                {
                    this.updateDatasAdminDatabaseCommand = new RelayCommand(param => this.canUpdateDataDatabaseAdmin(), param => this.canExecuteUpdateDatasDatabaseAdmin());
                }
                return this.updateDatasAdminDatabaseCommand;
            }
        }


       // private RelayCommand updateScripteAdminDatabaseCommand;
       // private RelayCommand updateDatasAdminDatabaseCommand;

      
        #endregion


        #region METHODS

        #region Load region

      

        void LoadBackUpLog()
        {
            BackgroundWorker worker = new BackgroundWorker();
             worker.DoWork += (o, args) =>
             {
                 MessageTraitement = string.Empty;
                // DtbleBackLogs = ClassUtilsModel.BackUpLoggListe(societeCourante.IdSociete);
                 ButonEnable = true;
                

                 DataTable dtblMinMax = ClassUtilsModel.BackUpMaxMinDateFacture(societeCourante.IdSociete);
                 if (dtblMinMax.Rows.Count > 0)
                 {
                     DateMin = Convert.ToDateTime(dtblMinMax.Rows[0]["dateMmin"]);
                     DateMax = Convert.ToDateTime(dtblMinMax.Rows[0]["dateMax"]);

                 }


             };
             worker.RunWorkerCompleted += (o, args) =>
             {
                 if (args.Result != null)
                 {
                     CustomExceptionView view = new CustomExceptionView();
                     // view.Owner = Application.Current.MainWindow;
                      view.ViewModel.Message = "Erreur lors du chargement liste des Liste Computer ";
                      view.ShowDialog();
                 }
                 else
                 {
                     LoadArchives();
                 }
                 
             };

             worker.RunWorkerAsync();

             LoadArchives();
        }

        void Load()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, args) =>
             {
                 try
                 {
                     DataTable tableResult = ClassUtilsModel.LicenseListComputer(societeCourante.IdSociete );
                     if (tableResult.Rows.Count > 0)
                     {
                         List<Computer> liste = new List<Computer>();
                         foreach (DataRow row in tableResult.Rows)
                         {
                             Computer computer = new Computer();
                             computer.IdSte =Convert.ToInt32 ( row["IdSte"]);
                             computer.Machine = Encoding.ASCII.GetString((byte[])row["machine"]);
                             computer.IpName = Encoding.ASCII.GetString((byte[])row["ipName"]);
                             computer.Hostname = Encoding.ASCII.GetString((byte[])row["hostName"]);
                             computer.Active =Convert.ToBoolean ( row["active"]);
                             liste.Add(computer);
                         }
                         ComputeLists = liste;
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
                     // view.Owner = Application.Current.MainWindow;
                   
                      view.ViewModel.Message = "Erreur lors du chargement liste des Liste Computer ";
                      view.ShowDialog();
                  }
                  else
                  {
                  }
              
              };

              worker.RunWorkerAsync();
      
        }

        #endregion

        #region Loading facture
        
       
        void canSave()
        {
            

        
            try
            {
               //chargement facture à archiver de la base courante
                MessageTraitement = string.Empty;
                ProgressBarVisibility = false;
                IsBusy = false;
                DataTable dtbArchResult = ClassUtilsModel.ArchiveSelecte(societeCourante.IdSociete, Datefrom.Value, DateTo.Value);
                  List<ArchivePeview>  liste=new List<ArchivePeview> ();
                if (dtbArchResult != null && dtbArchResult.Rows.Count > 0)
                {

                    foreach(DataRow row in dtbArchResult.Rows)
                        liste.Add(new ArchivePeview{
                            NombreFacture=Convert.ToInt32( row["NBRE_FACTURE"]),
                            LibelleStatut=Convert.ToString( row["StatutFacture"]),
                            IdStatut = Convert.ToInt32(row["ID_Statut"]),
                            Chechked = false,
                            IsEnbale =testeEnable( Convert.ToInt32(row["ID_Statut"])) 
                        });
                    ArchivePrieviewList = liste;
                    ArchivePrieviewSelects = new List<ArchivePeview>();
                    ButonEnable = true;
                    ButonCancelRunnBAckupEnable = false;
                    viewModal = new AdminModalBackUp();
                    viewModal.DataContext = this;
                    viewModal.Owner = System.Windows.Application.Current.MainWindow;
                    viewModal.ShowDialog();
                }else
                    System.Windows.Forms.MessageBox.Show("Pas de Facture à Archiver \n Poure la période définie!", "Traitement Archivage", MessageBoxButtons.OK, MessageBoxIcon.Information);

               // ClassUtilsModel.BackUpLoggAdd(societeCourante.IdSociete, Datefrom.Value, DateTo.Value, UserConnected.Nom, "sysfact->archive" + DateTime.Now.ToShortDateString());
                //System.Windows.Forms.MessageBox.Show("Base de données Archive mise à jour");

                //LoadBackUpLog();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                //view.Owner = this.;// Application.Current.MainWindow;
                view.Title = "";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        bool canExecuteSave()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.ArchiveExecute ||  CurrentDroit.Developpeur )
                {
                    if (Datefrom.HasValue && DateTo.HasValue)
                        if (Datefrom.Value <= DateTo.Value)
                        values = true;

                }
            }
            return values;
            
        }

        bool testeEnable( Int32? IdStatut)
        {
            if (!IdStatut.HasValue)
                return false;
            else
            {
                if (IdStatut.Value == 14003 || IdStatut.Value == 14004 || IdStatut.Value == 14005 || IdStatut.Value == 14006 || IdStatut.Value == 14007)
                    return true;
            }

                return false;

        }

        #endregion


        #region Run archive
          BackgroundWorker worker = new BackgroundWorker();
          int idlog = 0;

        void canRunArchiveSave()
        {
            bool Isfrre=false;

            DataTable tableList=null;
              
                try
                {
                     tableList = ClassUtilsModel.ArchiveGenerate_List(societeCourante.IdSociete, Datefrom.Value, DateTo.Value, (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14003))
                              , (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14006))
                              , (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14004))
                              , (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14007)),
                              (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14005)), UserConnected.Nom);
                    if (tableList != null && tableList.Rows.Count > 0)
                    {
                        worker.WorkerReportsProgress = true;
                       
                        viewModal.progressBar.Maximum = tableList.Rows.Count;
                       // progress.ProgressBarMaximum = 100;
                       // progress.Show();
                        int ikk = 0;

                        worker.DoWork += (o, args) =>
                        {
                            ButonEnable = false;
                            ProgressBarVisibility = true;
                            //IsBusy = true;
                            //MessageTraitement = "Début de  Traitement";
                            //ColorMessage = "Blue";
                            string cheminLog = string.Empty;
                            ButonCancelRunnBAckupEnable = true;

                            try
                            {

                               


                                foreach (DataRow row in tableList.Rows)
                                {
                                    if (row["IDFActure"] != DBNull.Value)
                                        ClassUtilsModel.ArchiveGenerate(Convert.ToInt64(row["IDFActure"]), societeCourante.IdSociete);
                                      
                                        ikk++;
                                   // worker.ReportProgress((int)(ikk / diviseur));
                                    worker.ReportProgress(ikk );
                                }

                              


                            }
                            catch (Exception ex)
                            {
                               
                                args.Result = ex.Message;
                            }


                        };

                        worker.ProgressChanged += (o, args) =>
                        {
                           // progress.LBInfos = args.ProgressPercentage.ToString() + "%";
                           // progress.ValueProgressBar = args.ProgressPercentage;
                            viewModal.ValueProgressBar = args.ProgressPercentage;
                            viewModal.LBInfos = args.ProgressPercentage.ToString() + "%";
                        };

                        worker.RunWorkerCompleted += (o, args) =>
                        {
                            if (args.Result != null)
                            {
                                CustomExceptionView view = new CustomExceptionView();
                                view.Owner = System.Windows.Application.Current.MainWindow;
                                view.Title = "ERREURE";
                                view.ViewModel.Message = "Erreure Survenue lors du traitement de l'Archivage \n" + args.Result;
                                MessageTraitement = "Echec traitement archivage";
                                ColorMessage = "Red";
                                ProgressBarVisibility = false;
                                IsBusy = false;

                                view.ShowDialog();
                                ClassUtilsModel.BackUp_LogDELETE(idlog);
                            }
                            else
                            {
                               // progress.Close();
                                viewModal.LBInfos = "100%";

                                System.Windows.Forms.MessageBox.Show("Fin de traitement de l'archivage  !", "Traitement Archivage", MessageBoxButtons.OK, MessageBoxIcon.Information);
                                ProgressBarVisibility = false;

                                ClassUtilsModel.BackUp_LogADD(ref idlog, societeCourante.IdSociete, Datefrom.Value, DateTo.Value, UserConnected.Nom, "Archivage effectué avec success"
                                       , (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14003) == true ? 14003 : 0)
                                 , (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14006) == true ? 14006 : 0)
                                 , (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14004) == true ? 14004 : 0)
                                 , (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14007) == true ? 14007 : 0)
                                 , (ArchivePrieviewSelects.Exists(st => st.IdStatut == 14005) == true ? 14005 : 0)
                                 );

                                //fermeture modale
                                // chargement produits libres
                                LoadFreeProduct();
                                if (PreviousFreeProduct != null && PreviousFreeProduct.Count > 0)
                                {

                                    DialogResult result = System.Windows.Forms.MessageBox.Show("Il existe des produits Libres  , Voulez vous les supprimer ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                                    if (result == DialogResult.Yes)
                                    {
                                       // fermeture modale archive
                                        viewModal.Close();

                                        ButonProductEnable = true;
                                        ProgressBarProductVisibility = false;

                                         viewporduct = new AdminFreeProduct();
                                        viewporduct.DataContext = this;
                                        viewporduct.Owner = System.Windows.Application.Current.MainWindow;
                                        viewporduct.ShowDialog();

                                    }
                                }

                                //chargement clients libres
                                LoadfreeClients();
                                if (PreviousFreeCustomers != null && PreviousFreeCustomers.Count > 0)
                                {
                                    ButonEnable = true;
                                    ProgressBarVisibility = false;
                                    DialogResult result = System.Windows.Forms.MessageBox.Show("Il existe des Clients non Factuées , Voulez vous les supprimer ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                                   
                                    if (result == DialogResult.Yes)
                                    {
                                       // viewModal.Close();

                                        customView = new AdminFreeCustomer();
                                        customView.Owner = System.Windows.Application.Current.MainWindow;
                                        customView.DataContext = this;
                                        customView.ShowDialog();
                                    }
                                    else
                                    {
                                        MessageTraitement = "Fin de Traitement Archivage Réussie";
                                        ColorMessage = "Green";
                                       
                                        ArchivePrieviewList = null;
                                        ButonEnable = false;
                                        ProgressBarVisibility = false;
                                        IsBusy = false;
                                        ButonCancelRunnBAckupEnable = false;


                                    }

                                  

                                }

                               
                                LoadArchives();
                            }

                        };

                        worker.RunWorkerAsync();
                        worker.WorkerSupportsCancellation = true;
                    }else
                        System.Windows.Forms.MessageBox.Show(" Pas de traitement  effectué");
                   
                }
                catch (Exception ex)
                {
                     
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = System.Windows.Application.Current.MainWindow; ;
                view.ViewModel.Message = "Erreur  lors du Chargement "+ex.Message;
                IsBusyUpdate = false;
                ProgressBarVisibilityUpdate = false;
                view.ShowDialog();
            


                }
              
                
               
        }
        bool canExecuteRunArchive()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.ArchiveExecute ||  CurrentDroit.Developpeur )
                {
                    if (ArchivePrieviewSelects !=null )
                        if (ArchivePrieviewSelects.Count > 0)
                            values = true;

                }
            }
            return values;

           
        }

        //kill the thread
        void canCelRunMackUp()
        {
            worker.CancelAsync();
        }
        #endregion

        #region Delete Free custom

        void canRunFreeSave()
        {
             BackgroundWorker worker = new BackgroundWorker();
            
                
                 MessageTraitement = "Début Traitement";
                 ColorMessage = "Blue";
                 ButonCancelRunnBAckupEnable = true;
                 worker.WorkerReportsProgress = true;
                 int ikk = 0;
                 customView.progreBarClient.Maximum = PreviousFreeCustomersselects.Count;

                 worker.DoWork += (o, args) =>
                 {
                 try
                 {
                     ButonEnable = false;
                     ProgressBarVisibility = true;
                     IsBusy = true;

                     // supprimete  DeleteAllFreeclient
                     ClientModel clientService = new ClientModel();
                     foreach (ListeFreeClient cli in PreviousFreeCustomersselects)
                     {
                         ClassUtilsModel.free_Customer_delete(cli.Idsite, cli.IDClient);
                        // clientService.CLIENT_ACTIF(cli.IDClient, cli.Idsite, false);
                         ikk++;
                         worker.ReportProgress(ikk);
                     }

                   
                 }

                 catch (Exception ex)
                 {
                     //CustomExceptionView view = new CustomExceptionView();
                     //view.Owner = this.;// Application.Current.MainWindow;
                     args.Result = ex.Message;
                 }
             };
             worker.ProgressChanged += (o, args) =>
             {
                 // progress.LBInfos = args.ProgressPercentage.ToString() + "%";
                 // progress.ValueProgressBar = args.ProgressPercentage;
                 viewModal.ValueProgressBar = args.ProgressPercentage;
                 viewModal.LBInfos ="Suppression clients"+ args.ProgressPercentage.ToString() + "%";
             };
             worker.RunWorkerCompleted += (o, args) =>
               {
                   if (args.Result != null)
                   {
                       CustomExceptionView view = new CustomExceptionView();
                       view.Owner = System.Windows.Application.Current.MainWindow;
                       view.Title = "ERREURE";
                       view.ViewModel.Message = "Erreure Survenue lors du traitement de l'Archivage \n" + args.Result;
                       MessageTraitement = "Echec traitement archivage";
                       ColorMessage = "Red";
                       ProgressBarVisibility = false;
                       IsBusy = false;
                       ButonCancelRunnBAckupEnable = false;
                       ButonEnable = true;
                       view.ShowDialog();
                   }
                   else
                   {

                       DialogResult result = System.Windows.Forms.MessageBox.Show(" Fin des operation  , Quittez ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                       if (result == DialogResult.Yes)
                       {
                           customView.Close();

                       }
                       else
                       {
                           ButonEnable = true;
                           ProgressBarVisibility = false;
                           IsBusy = false;
                           ButonCancelRunnBAckupEnable = false;
                       }
                     //  LoadArchives();
                      
                   }
               };
             worker.RunWorkerAsync();

        }

        bool canExecuteRunFree()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.ArchiveExecute ||  CurrentDroit.Developpeur )
                {
                    if (PreviousFreeCustomersselects != null)
                        if (PreviousFreeCustomersselects.Count > 0)
                            values = true;

                }
            }
            return values;

           
        }

        void canCelFreeClient()
        {

        }
      
        #endregion

        #region Delete free product
        BackgroundWorker workerProd = null;
           

        void canRunFreeProductSave()
        {
            workerProd = new BackgroundWorker();
            //FreeproductListselect
         
               MessageTraitement = "Début Traitement";
               ColorMessage = "Blue";
               ButonCancelfreeproduct = true;
               workerProd.WorkerReportsProgress = true;
               int ikk = 0;
               viewporduct.progreBarProduct.Maximum = FreeproductListselect.Count;
               workerProd.DoWork += (o, args) =>
               {

             try
                 {

                     ButonProductEnable = false;
                     ProgressBarProductVisibility = true;
                     IsBusy = true;
                
                     foreach (FreeProduct prod in FreeproductListselect)
                     {
                       ClassUtilsModel.free_product_delete(societeCourante.IdSociete,prod.Id);
                         ikk++;

                         workerProd.ReportProgress(ikk);
                     }

                   
                 }

                 catch (Exception ex)
                 {
                     //CustomExceptionView view = new CustomExceptionView();
                     //view.Owner = this.;// Application.Current.MainWindow;
                     args.Result = ex.Message;
                 }
             };
             workerProd.ProgressChanged += (o, args) =>
             {
                 // progress.LBInfos = args.ProgressPercentage.ToString() + "%";
                 // progress.ValueProgressBar = args.ProgressPercentage;
                 viewporduct.ValueProgressBar = args.ProgressPercentage;
                 viewporduct.LBInfos = "Suppression clients" + args.ProgressPercentage.ToString() + "%";
             };
             workerProd.RunWorkerCompleted += (o, args) =>
               {
                   if (args.Result != null)
                   {
                       CustomExceptionView view = new CustomExceptionView();
                       view.Owner = System.Windows.Application.Current.MainWindow;
                       view.Title = "ERREURE";
                       view.ViewModel.Message = "Erreure Survenue lors du traitement de l'Archivage \n" + args.Result;
                       MessageTraitement = "Echec traitement archivage";
                       ColorMessage = "Red";
                       ProgressBarProductVisibility = false;
                       IsBusy = false;
                       ButonProductEnable = true;
                       ButonCancelfreeproduct = false;
                       view.ShowDialog();
                   }
                   else
                   {

                       DialogResult result = System.Windows.Forms.MessageBox.Show(" Fin des operation  , Quittez ?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                       if (result == DialogResult.Yes)
                       {
                           viewporduct.Close();

                       }
                       else
                       {
                           ButonProductEnable = true;
                           ProgressBarProductVisibility = false;
                           IsBusy = false;
                           ButonCancelfreeproduct = false;
                           viewporduct.Close();
                       }
                      // LoadArchives();
                      
                   }
               };
             workerProd.RunWorkerAsync();
             workerProd.WorkerSupportsCancellation = true;
        }

        bool canExecuteRunFreeproduct()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.ArchiveExecute || CurrentDroit.Developpeur)
                {
                    if (FreeproductListselect != null)
                        if (FreeproductListselect.Count > 0)
                            values = true;

                }
            }
            return values;

          
        }

        void canCelFreeProduct(){
            workerProd.CancelAsync();
        }
        #endregion

        #region Modal database admin
        void canSHowDatabaseAdmin()
        {
            AdminDatabase view = new AdminDatabase();
            view.DataContext = this;
            view.Owner = System.Windows.Application.Current.MainWindow;
            view.ShowDialog();
        }

        bool canExecuteShowDatabaseAdmin()
        {

            if (CurrentDroit.Developpeur )
            return true;
            return false;
        }


        #endregion

        #region Update scripts


        void canUpdateScriptDatabaseAdmin()
        {
            try
            {
                //"localhost;3306;root;sysfact;serge_it"

            }
            catch (Exception ex)
            {

            }
        }

        bool canExecuteUpdateScriptDatabaseAdmin()
        {
            return string.IsNullOrEmpty(CheminFichier) ?false:true;
        }
        #endregion

        #region Update Datas

        void canUpdateDataDatabaseAdmin()
        {
            try
            {
                IsBusyUpdate = true;
                ProgressBarVisibilityUpdate = true;
                decimal diviseur = 0;
                DataTable listeTable = ClassUtilsModel.GetListeFacturesUpdates(societeCourante.IdSociete);
                if (listeTable != null && listeTable.Rows.Count > 0)
                {
                    System.Windows.Forms.MessageBox.Show("Nombre de factures :" + listeTable.Rows.Count);
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;
                    if (progress != null) progress.Close();
                    progress = new ProgressWindow();
                    progress.ProgressBarMinimum = 0;
                    diviseur =Math.Round(((decimal)listeTable.Rows.Count / 100),2);

                    progress.ProgressBarMaximum = 100;
                    progress.Show();
                    int ikk = 0;
                      worker.DoWork += (o, args) =>
                      {
                          try
                          {
                              foreach (DataRow row in listeTable.Rows)
                              {
                                  if (row["IDFacture"] !=DBNull.Value)
                                     ClassUtilsModel.Updates_New_Datas(Convert.ToInt64(row["IDFacture"]), societeCourante.IdSociete);
                                  ikk++;
                                   worker.ReportProgress((int)(ikk/diviseur));
                              }
                            
                          }

                          catch (Exception ex)
                          {

                              args.Result = ex.Message + " ;" + ex.InnerException + ";" + "MESSAGE INFORMATION IMPORT";
                          }
                      };

                      worker.ProgressChanged += (o, args) =>
                      {
                          progress.LBInfos = args.ProgressPercentage.ToString() + "%";
                          progress.ValueProgressBar = args.ProgressPercentage;

                      };
                      worker.RunWorkerCompleted += (o, args) =>
                      {
                          if (args.Result != null)
                          {
                              CustomExceptionView view = new CustomExceptionView();
                              view.Owner = System.Windows.Application.Current.MainWindow;
                              view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                              view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                              view.ShowDialog();
                              this.MouseCursor = null;
                              this.IsBusy = false;
                              progress.Close();
                          }
                          else
                          {



                              System.Windows.Forms.MessageBox.Show(" fin traitement");
                              progress.Close();
                          }
                      };
                      worker.RunWorkerAsync();

                }
                else
                    System.Windows.Forms.MessageBox.Show("il n'existe plus de données à modifier \n La base de données est A Jour");
                

              IsBusyUpdate = false;
              ProgressBarVisibilityUpdate = false;


            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = System.Windows.Application.Current.MainWindow; ;
                view.ViewModel.Message = "Erreur "+ex.Message;
                IsBusyUpdate = false;
                ProgressBarVisibilityUpdate = false;
                view.ShowDialog();
            }
        }

        bool canExecuteUpdateDatasDatabaseAdmin()
        {
            if (CurrentDroit.Developpeur)
            return true;
            return false;
        }
        #endregion


        void canClose()
        {
            Communicator ctr = new Communicator();
            ctr.contentVue = "admin";
            EventArgs e1 = new EventArgs();
            ctr.OnChangeText(e1);
        }

        #region reprtoire
        void canRepertoire()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".xml"; // Default file extension
                dlg.Filter = "Text documents (.sql)|*.sql"; // Filter files by extension

                // Show open file dialog box
                Nullable<bool> result = dlg.ShowDialog();

                // Process open file dialog box results
                if (result == true)
                {
                    // Open document
                    string filename = dlg.FileName;
                    CheminFichier = filename;
                    IsfileSelect = false;
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = System.Windows.Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION RESEARCH File";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }

        }
        #endregion
        #endregion

        #region UTILS


        void LoadfreeClients()
        {
            try
            {
                List<ListeFreeClient> liste = new List<ListeFreeClient>();
                DataTable tableClients = ClassUtilsModel.Previous_free_Customer(societeCourante.IdSociete);
                foreach (DataRow row in tableClients.Rows)
                {
                    ListeFreeClient cli = new ListeFreeClient();
                    cli.IDClient = Convert.ToInt32(row["ID"]);
                    cli.Idsite = Convert.ToInt32(row["ID_Site"]);
                    cli.NomClient = Convert.ToString(row["Nom_Client"]);
                    cli.Ville = Convert.ToString(row["Ville"]);
                    cli.Checked = false;
                    liste.Add(cli);
                }
                PreviousFreeCustomers = liste;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = System.Windows.Application.Current.MainWindow;
                view.Title = "ERREURE";
                view.ViewModel.Message = "Erreure Survenue chargement Client Libres \n" + ex.Message;

                view.ShowDialog();
            }
        }

        void LoadFreeProduct()
        {
            try
            {
                List<FreeProduct> liste = new List<FreeProduct>();
                DataSet tables = ClassUtilsModel.Previous_free_Products(societeCourante.IdSociete);

                foreach (DataRow row in tables.Tables[0].Rows)
                {

                   //var   querys = (from chk in tables.Tables[1].AsEnumerable()
                   //      where Convert.ToInt32(chk["ID_Produit"]) ==Convert.ToInt32(row["ID"])
                   //      select chk).CopyToDataTable ();

                    FreeProduct prod = new FreeProduct();
                    prod.Id = Convert.ToInt32(row["ID_Produit"]);
                    prod.Product = Convert.ToString(row["Libelle"]);
                    prod.IsChecked = false;
                    prod.Detailsproduct = null;// Loaddetailproduct((DataTable)querys);
                    prod.nbreDetail = 0;// prod.Detailsproduct.Count;
                  
                    liste.Add(prod);
                }

                PreviousFreeProduct= liste;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = System.Windows.Application.Current.MainWindow;
                view.Title = "ERREURE";
                view.ViewModel.Message = "Erreure Survenue chargement produis Libres \n" + ex.Message;
              
                view.ShowDialog();
            }
        }

         List< FreeDetailProduct> Loaddetailproduct(DataTable dtbl)
        {
            List<FreeDetailProduct> liste = new List<FreeDetailProduct>();
            foreach (DataRow row in dtbl.Rows)
            {
                FreeDetailProduct det = new FreeDetailProduct();
                det.Id = Convert.ToInt32(row["ID"]);
                det.IdProduit = Convert.ToInt32(row["ID_Produit"]);
                det.Qte = Convert.ToDouble(row["Quantite"]);
                det.PU = Convert.ToDouble(row["Prix_Unitaire"]);
                det.IsChecked = false;
                liste.Add(det);
            }

            return liste;
        }

        void LoadArchives()
        {
            BackgroundWorker worker = new BackgroundWorker();
                worker.DoWork += (o, args) =>
                {

            try
            {
                  

                ProgressBarloadVisibility = true;
                Isloading = true;

                DataSet tableHst = ClassUtilsModel.BackUpLoggListe(societeCourante.IdSociete);

                DataTable tableArchive = tableHst.Tables[0];
                DataTable tableDetails = tableHst.Tables[1];
                List<ListeArchive> liste = new List<ListeArchive>();
                foreach (DataRow row in tableArchive.Rows)
                {
                    ListeArchive arch = new ListeArchive();
                    arch.DateFrom = Convert.ToDateTime(row["periodeFrom"]);
                    arch.DateTo = Convert.ToDateTime(row["periodeTo"]);
                    arch.DateBackup = Convert.ToDateTime(row["dateBackUp"]);
                    arch.Observation = Convert.ToString(row["observation"]);
                    arch.BackUpBy = Convert.ToString(row["backUpBy"]);

                    DataView dv = tableDetails.DefaultView;
                    dv.RowFilter = string.Format("IdPeriode ='{0}'", Convert.ToString(row["IdPeriode"]));
                    DataTable newdtbl = dv.ToTable();
                    List<ListeArchiveDetails> details = new List<ListeArchiveDetails>();
                    ListeArchiveDetails arch1 = null;
                    foreach (DataRow rows in newdtbl.Rows)
                    {
                        arch1 = new ListeArchiveDetails();
                        arch1.IDPeriode = Convert.ToString(rows["IdPeriode"]);
                        arch1.Libelle = Convert.ToString(rows["Libelle"]);
                        arch1.Nombre = Convert.ToString(rows["NbreFact"]);
                        arch.Details.Add(arch1);
                    }
                   
                    liste.Add(arch);
                    //  tableDetails.Select(string.Format("IdPeriode='{0}'", Convert.ToString(row["IdPeriode"])));


                }
                ListeArchives = liste;
              
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
                        // view.Owner = Application.Current.MainWindow;
                        view.Title = "ERREURE";
                        view.ViewModel.Message = "Erreure Survenue lors du chargement de l'Archivage \n" + args.Error.Message;
                       
                        view.ShowDialog();
                    }


                    ProgressBarloadVisibility = false;
                    Isloading = false;

                   
                };

                worker.RunWorkerAsync();
        }

        #endregion
    }

    public class Computer
    {
        public int? IdSte { get; set; }
        public string  Machine { get; set; }
        public bool?  Active { get; set; }
        public string Hostname { get; set; }
        public string IpName { get; set; }
    }

    public class ArchivePeview
    {
        public int? NombreFacture { get; set; }
        public string LibelleStatut { get; set; }
        public string Message { get; set; }
        public string Backcolor { get; set; }
        public bool IsEnbale { get; set; }
        public int IdStatut { get; set; }
        public bool Chechked { get; set; }

    }

    public class ListeArchive
    {
        public DateTime DateFrom { get; set; }
        public DateTime DateTo { get; set; }
        public DateTime DateBackup { get; set; }
        public string Observation { get; set; }
        public string BackUpBy { get; set; }

        private List<ListeArchiveDetails> details=new List<ListeArchiveDetails> ();

        public List<ListeArchiveDetails> Details
        {
            get { return details; }
            set { details = value; }
        }
    }

    public class ListeArchiveDetails
    {
        public string IDPeriode { get; set; }
        public string Libelle { get; set; }
        public string Nombre { get; set; }
    }

    public class ListeFreeClient
    {
        public int  IDClient { get; set; }
        public int Idsite  { get; set; }
        public string NomClient { get; set; }
        public string Ville { get; set; }
        public bool Checked { get; set; }
    }

    public class FreeProduct
    {
        public int Id { get; set; }
        public string Product { get; set; }
        public int nbreDetail { get; set; }
        public bool IsChecked { get; set; }
        List<FreeDetailProduct> detailsproduct = new List<FreeDetailProduct>();

        public List<FreeDetailProduct> Detailsproduct
        {
            get { return detailsproduct; }
            set { detailsproduct = value; }
        }

    }

    public class FreeDetailProduct
    {
        public int Id { get; set; }
        public int IdProduit { get; set; }
        public double Qte { get; set; }
        public double PU { get; set; }
        public bool IsChecked { get; set; }
    }
}
