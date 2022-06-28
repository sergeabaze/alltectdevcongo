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
using AllTech.FrameWork.Global;
using System.Collections.ObjectModel;
using AllTech.FacturationModule.Views.Modal;
using System.Data;
using AllTech.FacturationModule.Views;
using AllTech.FacturationModule.Report;
using System.Collections;
using System.Threading;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Xml.Linq;
using System.Xml;
using AllTech.FrameWork.Utils;
using AllTech.FrameWork.Helpers;
using System.Windows.Controls;

namespace AllTech.FacturationModule.ViewModel
{
    public class AdministrationDataViewModel : ViewModelBase
    {

        #region FIELDS
      
        public IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
        public IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private  BackgroundWorker worker;

        private RelayCommand saveCommand;
        private RelayCommand closeCommand;
        private RelayCommand refreshImportCommand;
        private RelayCommand importCommand;
        private RelayCommand exportCommand;
        private RelayCommand loadlogImportCommand;
        private RelayCommand loadlogExportCommand;
        private RelayCommand logReExportCommand;
        private RelayCommand deleteFileCommand;
        private RelayCommand sendMailCommand;
        private RelayCommand fichierRepertoireCommand;
        private RelayCommand fichierReClearCommand;

         private RelayCommand exportDBrCommand;
         private RelayCommand importDBrCommand;
         private RelayCommand cancelimportDBrCommand;

        private bool isBusy;
        private bool isBusyImport;

       
        bool _progressBarVisibility;
        private Cursor mouseCursor;

        public SocieteModel societeCourante;
        UtilisateurModel UserConnected;
        ParametresModel ParametersDatabase;
        DroitModel CurrentDroit;
        FileInfo[] listeFiles;
        DateTime? inDateDebut;
        DateTime? inDatefin;
        LogExportIportModel inportExportService;

        List<NewLogFile> listeLog;
        NewLogFile currentLogSelected;

        string cheminFichier;
        bool isfileSelect;
        AdministrationDatas localview;
      
       
     

        LignesFichiers fichierSelected;
        List<LignesFichiers> listeFichierSelecteds;

      
        List<LignesFichiers> listeFichiersDossiers;
        ObservableCollection<FactureModel> factures;

        double currentProgressImport;
        double currentProgressExport;
       
        bool isProgressExportVisibled;
        bool isProgressInportVisibled;
        bool cmdRExportVisibility;
        List<logFileListe> logExportList;
        List<logFileListe> logImortList;
      

        public event EventHandler WorkStarted;
        public event EventHandler WorkEnded;
        string messageFinal;
        string nobreEnregistrement;
        string messagerefreshing;
        ProgressWindow progress;
        SocieteModel societeService;

        List<Databaseinfo> listeDatabases;
        Databaseinfo databaseSelected;
        Databaseinfo cmbDatabaseSelected;

      
        List<Tables> listeTableDB;

        bool progressBarDbVisibility;
        bool btnDbexportEnable;
        bool isBusyDb;
        bool btnImportenable;
        string pathImportDB;

        DataTable tableHistoricLog;
        DataRow rowHistoricSelected;

      
       

        BackgroundWorker workerExpDb = null;
        BackgroundWorker workerImportDb = null;

        bool progressBarIMportvisibility;
        bool isImportBusy;
        string pathSaveAs;
        bool isImportCancel;

      
      
       
        #endregion

        #region Constructeur


        public AdministrationDataViewModel(AdministrationDatas userconcontrol)
        {
        //    _regionManager = regionManager;
        //    _container = container;
        //    _injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
            localview = userconcontrol ;
            societeCourante = GlobalDatas.DefaultCompany;
            progress = new ProgressWindow();
            societeService = new SocieteModel();
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            listeFichierSelecteds = new List<LignesFichiers>();
            CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("administration"));
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Super)
                {
                    CmdRExportVisibility = true;
                }
            }
            IsProgressInportVisibled = true;
            BtnDbexportEnable = true;
            BtnImportenable = true;
           // GetLogFiles();
            infoFile();
        }

        #endregion

        #region PROPERTIES

        public DataRow RowHistoricSelected
        {
            get { return rowHistoricSelected; }
            set { rowHistoricSelected = value;
            this.OnPropertyChanged("RowHistoricSelected");
            }
        }

        public DataTable TableHistoricLog
        {
            get { return tableHistoricLog; }
            set { tableHistoricLog = value;
            this.OnPropertyChanged("TableHistoricLog");
            }
        }


        public bool IsImportCancel
        {
            get { return isImportCancel; }
            set { isImportCancel = value;
            this.OnPropertyChanged("IsImportCancel");
            }
        }

        public bool BtnImportenable
        {
            get { return btnImportenable; }
            set { btnImportenable = value;
            this.OnPropertyChanged("BtnImportenable");
            }
        }


        public string PathImportDB
        {
            get { return pathImportDB; }
            set { pathImportDB = value;
            this.OnPropertyChanged("PathImportDB");
            }
        }


        public bool IsImportBusy
        {
            get { return isImportBusy; }
            set { isImportBusy = value;
            this.OnPropertyChanged("IsImportBusy");
            }
        }

        public bool ProgressBarIMportvisibility
        {
            get { return progressBarIMportvisibility; }
            set { progressBarIMportvisibility = value;
            this.OnPropertyChanged("ProgressBarIMportvisibility");
            }
        }

        public Databaseinfo CmbDatabaseSelected
        {
            get { return cmbDatabaseSelected; }
            set { cmbDatabaseSelected = value;
            this.OnPropertyChanged("CmbDatabaseSelected");
            }
        }

        public bool IsBusyDb
        {
            get { return isBusyDb; }
            set { isBusyDb = value;
            this.OnPropertyChanged("IsBusyDb");
            }
        }

        public List<Tables> ListeTableDB
        {
            get { return listeTableDB; }
            set { listeTableDB = value;
            this.OnPropertyChanged("ListeTableDB");
            }
        }

        public bool BtnDbexportEnable
        {
            get { return btnDbexportEnable; }
            set { btnDbexportEnable = value;
            this.OnPropertyChanged("BtnDbexportEnable");
            }
        }


        public bool ProgressBarDbVisibility
        {
            get { return progressBarDbVisibility; }
            set
            {
                progressBarDbVisibility = value;
                this.OnPropertyChanged("ProgressBarDbVisibility");
            }
        }


        public Databaseinfo DatabaseSelected
        {
            get { return databaseSelected; }
            set { databaseSelected = value;
                if (value!=null )
                 ListeTableDB = value.TablesDb;
            this.OnPropertyChanged("DatabaseSelected");
            }
        }

        public string PathSaveAs
        {
            get { return pathSaveAs; }
            set { pathSaveAs = value;
            this.OnPropertyChanged("PathSaveAs");
            }
        }

        public List<Databaseinfo> ListeDatabases
        {
            get { return listeDatabases; }
            set { listeDatabases = value;

            this.OnPropertyChanged("ListeDatabases");
            }
        }


        #region COMMON

        public bool IsfileSelect
        {
            get { return isfileSelect; }
            set { isfileSelect = value;
            OnPropertyChanged("IsfileSelect");
            }
        }

        public string CheminFichier
        {
            get { return cheminFichier; }
            set { cheminFichier = value;
            if (string.IsNullOrEmpty(value))
            {
                if (value.EndsWith(".xml"))
                    IsfileSelect = false;
            }
            OnPropertyChanged("CheminFichier");
            }
        }

        public string Messagerefreshing
        {
            get { return messagerefreshing; }
            set { messagerefreshing = value;
            OnPropertyChanged("Messagerefreshing");
            }
        }

        public bool CmdRExportVisibility
        {
            get { return cmdRExportVisibility; }
            set { cmdRExportVisibility = value;
            OnPropertyChanged("CmdRExportVisibility");
            }
        }

        public string NobreEnregistrement
        {
            get { return nobreEnregistrement; }
            set { nobreEnregistrement = value;
            OnPropertyChanged("NobreEnregistrement");
            }
        }


        public string MessageFinal
        {
            get { return messageFinal; }
            set { messageFinal = value;
            OnPropertyChanged("MessageFinal");
            }
        }

        public ObservableCollection<FactureModel> Factures
        {
            get { return factures; }
            set { factures = value;
              OnPropertyChanged("Factures");
            }
        }

        public LignesFichiers FichierSelected
        {
            get { return fichierSelected; }
            set { fichierSelected = value;
            IsfileSelect = true;
            ListeFichierSelecteds.Add(value);
            OnPropertyChanged("FichierSelected");
            }
        }


        public List<LignesFichiers> ListeFichiersDossiers
        {
            get { return listeFichiersDossiers; }
            set { listeFichiersDossiers = value;
            OnPropertyChanged("ListeFichiersDossiers");
            }

        }
        public FileInfo[] ListeFiles
        {
            get { return listeFiles; }
            set { listeFiles = value;
            OnPropertyChanged("ListeFiles");
            }
        }
     
        public bool IsBusy
        {
            get { return isBusy; }
            set { isBusy = value;
            OnPropertyChanged("IsBusy");
            }
        }

        public bool IsBusyImport
        {
            get { return isBusyImport; }
            set { isBusyImport = value;
            OnPropertyChanged("IsBusyImport");
            }
        }

        public bool ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set { _progressBarVisibility = value;
            OnPropertyChanged("ProgressBarVisibility");
            }
        }



        public Cursor MouseCursor
        {
            get { return mouseCursor; }
            set { mouseCursor = value;
            OnPropertyChanged("MouseCursor");
            }
        }

        public DateTime? InDateDebut
        {
            get { return inDateDebut; }
            set { inDateDebut = value;
            OnPropertyChanged("InDateDebut");
            }
        }


        public DateTime? InDatefin
        {
            get { return inDatefin; }
            set { inDatefin = value;
            OnPropertyChanged("InDatefin");
            }
        }
      
        #endregion


        public List<NewLogFile> ListeLog
        {
            get { return listeLog; }
            set { listeLog = value;
            this.OnPropertyChanged("ListeLog");
            }
        }

        public List<LignesFichiers> ListeFichierSelecteds
        {
            get { return listeFichierSelecteds; }
            set { listeFichierSelecteds = value;
            this.OnPropertyChanged("ListeFichierSelecteds");
            }
        }


        public NewLogFile CurrentLogSelected
        {
            get { return currentLogSelected; }
            set { currentLogSelected = value;
            this.OnPropertyChanged("CurrentLogSelected");
            }
        }
        public double CurrentProgressImport
        {
            get { return currentProgressImport; }
            set
            {
                if (this.currentProgressImport != value)
                {
                    this.currentProgressImport = value;
                    this.OnPropertyChanged("CurrentProgressImport");
                }
            }
        }


        public double CurrentProgressExport
        {
            get { return currentProgressExport; }
            set
            {
                if (this.currentProgressExport != value)
                {
                    this.currentProgressExport = value;
                    this.OnPropertyChanged("CurrentProgressExport");
                }
            }
        }

        public bool IsProgressExportVisibled
        {
            get { return isProgressExportVisibled; }
            set { isProgressExportVisibled = value;
            this.OnPropertyChanged("IsProgressExportVisibled");
            }
        }


        public bool IsProgressInportVisibled
        {
            get { return isProgressInportVisibled; }
            set { isProgressInportVisibled = value;
            this.OnPropertyChanged("IsProgressInportVisibled");
            }
        }

        #endregion

        #region ICOMMAND

        public ICommand CancelimportDBrCommand
        {
            get
            {
                if (this.cancelimportDBrCommand == null)
                {
                    this.cancelimportDBrCommand = new RelayCommand(param => this.canCancelImportDb());
                }
                return this.cancelimportDBrCommand;
            }
        }

        //

        public ICommand ImportDBrCommand
        {
            get
            {
                if (this.importDBrCommand == null)
                {
                    this.importDBrCommand = new RelayCommand(param => this.canImportDb(), param => this.canExecuteImportDb());
                }
                return this.importDBrCommand;
            }
        }

        public ICommand ExportDBrCommand
        {
            get
            {
                if (this.exportDBrCommand == null)
                {
                    this.exportDBrCommand = new RelayCommand(param => this.canExportDb(), param => this.canExecuteExportDb());
                }
                return this.exportDBrCommand;
            }
        }


        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(param => this.canSaveFacture(), param => this.canExecuteSavefacture());
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
        public ICommand RefreshImportCommand
        {
            get
            {
                if (this.refreshImportCommand == null)
                {
                    this.refreshImportCommand = new RelayCommand(param => this.canRefreshDirectory());
                }
                return this.refreshImportCommand;
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

        //
        public ICommand FichierReClearCommand
        {
            get
            {
                if (this.fichierReClearCommand == null)
                {
                    this.fichierReClearCommand = new RelayCommand(param => this.canClearRepertoire(), param => this.canExeclean());
                }
                return this.fichierReClearCommand;
            }
        }

        public ICommand ImportCommandcanImportFile
        {
            get
            {
                if (this.importCommand == null)
                {
                    this.importCommand = new RelayCommand(param => this.canImportFile(), param => this.canExecuteimportFile());
                }
                return this.importCommand;
            }
        }

        public ICommand ExportCommand
        {
            get
            {
                if (this.exportCommand == null)
                {
                    this.exportCommand = new RelayCommand(param => this.canExportFile(), param => this.canExecuteExportFile());
                }
                return this.exportCommand;
            }
        }
       

        public ICommand LogReExportCommand
        {
            get
            {
                if (this.logReExportCommand == null)
                {
                    this.logReExportCommand = new RelayCommand(param => this.canReExportFile(), param => this.canExecuteReExportFile());
                }
                return this.logReExportCommand;
            }
        }

        public ICommand LoadlogImportCommand
        {
            get
            {
                if (this.loadlogImportCommand == null)
                {
                    this.loadlogImportCommand = new RelayCommand(param => this.canLoadLogFileImport());
                }
                return this.loadlogImportCommand;
            }
        }

        public ICommand LoadlogExportCommand
        {
            get
            {
                if (this.loadlogExportCommand == null)
                {
                    this.loadlogExportCommand = new RelayCommand(param => this.canLoadLogFileExport());
                }
                return this.loadlogExportCommand;
            }
        }

        //

        public ICommand DeleteFileCommand
        {
            get
            {
                if (this.deleteFileCommand == null)
                {
                    this.deleteFileCommand = new RelayCommand(param => this.canDeleteFile(), param => this.canExecuteDeleteFile());
                }
                return this.deleteFileCommand;
            }
        }

        public ICommand SendMailCommand
        {
            get
            {
                if (this.sendMailCommand == null)
                {
                    this.sendMailCommand = new RelayCommand(param => this.canSendMail(), param => this.canExecuteSendMail());
                }
                return this.sendMailCommand;
            }
        }

      
        #endregion

        #region METHODS

        #region SEND MAIL

        void canSendMail()
        {
            //ListeFichierSelecteds
            WModalmail vmail = new WModalmail(CurrentDroit, ListeFichierSelecteds);
            vmail.ShowDialog();
            if (vmail.isSendmail)
            {
                // mail partie archivahes des documents
                try
                {
                    string cheminBackUp = CommonModule.GetLogBAckUpPath(ParametersDatabase.PathBackUpLog);

                    foreach (var fichier in ListeFichierSelecteds)
                    {
                        //string nom = fichier.Nomfichier.Substring(0, fichier.Nomfichier.IndexOf("."));

                        FileStream destination = File.Create(cheminBackUp + "\\" + fichier.Nomfichier.Substring(0, fichier.Nomfichier.IndexOf(".")) + ".zip");
                        FileStream source = File.OpenRead(fichier.url);
                        GZipStream compStream = new GZipStream(destination, CompressionMode.Compress);

                        int theByte = source.ReadByte();
                        while (theByte != -1)
                        {
                            compStream.WriteByte((byte)theByte);
                            theByte = source.ReadByte();
                        }

                        //File.Copy(fichier.url, cheminBackUp + "\\" + fichier.Nomfichier);
                        compStream.Close();
                        compStream.Dispose();

                        source.Close();
                        source.Dispose();
                        string dateDebut = fichier.Nomfichier .Substring(fichier.Nomfichier .LastIndexOf("_") + 6, 8);
                        string datefin = fichier.Nomfichier.Substring(fichier.Nomfichier.LastIndexOf("_") + 14, 8);
                        string idSite = fichier.Nomfichier.Substring(fichier.Nomfichier.LastIndexOf("_") + 1, 5);
                        string periodes = string.Format("{0}-{1}", dateDebut.Substring(0, 2) + "/" + dateDebut.Substring(2, 2) + "/" + dateDebut.Substring(4),
                               datefin.Substring(0, 2) + "/" + dateDebut.Substring(2, 2) + "/" + dateDebut.Substring(4));

                        string dateExtractString = fichier.Nomfichier .Substring(fichier.Nomfichier .IndexOf("_") + 1, 8);
                        DateTime dateExtract = DateTime.Parse(dateExtractString.Substring(0, 2) + "/" + dateExtractString.Substring(2, 2) + "/" + dateExtractString.Substring(4));
                        LogExportIportModel.Extraction_ADD(0, dateExtract, DateTime.Today, DateTime.Today, periodes, int.Parse(idSite), societeCourante.IdSociete, "Fichier expedier par mail et archiver", 2, fichier.ToString().Trim());

                     
                        File.Delete(fichier.url);
                      
                    }
                }
                catch (IOException ex)
                {
                   
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "MESSAGE INFORMATION ARCHIVAGE FICHIER";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                }
                

            }
            //File.Copy(CheminFichier, cheminBackUp + "\\" + FichierSelected.Nomfichier);
          

        }


        bool canExecuteSendMail()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Ecriture   || CurrentDroit.Super || CurrentDroit.Proprietaire)
                {
                    if (FichierSelected != null)
                        values = true;
                }
            }
            return values;
        }
        #endregion

        #region SUPPRESSION FILES

        void canDeleteFile()
        {
             StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = Application.Current.MainWindow;
            messageBox.Title = "INFORMATION DE SUPPRESSION FICHIER";
            messageBox.ViewModel.Message = "Voulez Vous Supprimer ce fichier?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                       FileInfo fichier = new FileInfo(FichierSelected.url  );
                       fichier.Delete();
                       LogExportIportModel.Extraction_DELETE(0, societeCourante.IdSociete, FichierSelected.Nomfichier);
                       GetLogFiles();
                       canRefreshDirectory();
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "Information De Suppression fichiers";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                  
                }
            }

        }

        bool canExecuteDeleteFile()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Suppression   || CurrentDroit.Super || CurrentDroit.Proprietaire)
                {
                    if (FichierSelected != null)
                        values = true;
                }
            }
            return values;
        }

        #endregion

        #region REGION LOAD DATAS

        void infoFile()
        {
             BackgroundWorker worker = new BackgroundWorker();
             worker.DoWork += (o, args) =>
             {
            try
            {
                ListeDatabases = Helper.GetDatabases();

                TableHistoricLog = ClassUtilsModel.BackUpBD_SELECT(0);
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
                     view.ViewModel.Message = "Erreure :  "+args.Result;
                     view.ShowDialog();
                 }
                 else
                 {
                 }

             };

             worker.RunWorkerAsync();

        }

        #endregion

        #region Region RE EXTRACT

        void canReExportFile()
        {
           // BackgroundWorker worker = new BackgroundWorker();
            //worker.WorkerReportsProgress = true;

            if (progress != null) progress.Close();
            progress = new ProgressWindow();
            progress.Show();

            //worker.DoWork += (o, args) =>
            //{

                try
                {

                    MessageFinal = "Début traitement";

                   // IsProgressExportVisibled = true;
                    IsBusy = true;
                    bool fileExist = false;
                    int i = 0;
                    LigneFactureModel ligneService = new LigneFactureModel();
                    Dictionary<FactureModel, List<LigneFactureModel>> listesFacture = new Dictionary<FactureModel, List<LigneFactureModel>>();
                    StringBuilder fileName = new StringBuilder();

                    string[] tabDate = CurrentLogSelected.periode.Split(new char[] { '-' });
                    DateTime dateDebut = DateTime.Parse(tabDate[0]);
                    DateTime datefin = DateTime.Parse(tabDate[1]);
                    //FileInfo[] newListeFiles = CommonModule.GetListeFiles(ParametersDatabase.PathBackUpLog );
                    //for (int i = 0; i < newListeFiles.Length; i++)
                    //{
                    //    if (newListeFiles[i].Name.Equals(CurrentLogSelected.nomfichier))
                    //    {
                    //        fileExist = true;
                    //        break;
                    //    }
                    //}


                    //if (fileExist)
                    //{
                    var newFactures = new ObservableCollection<FactureModel>();
                        
                        //LogExportIportModel.Get_Export_ListeFacture(dateDebut, datefin, CurrentLogSelected.idSite );
                    progress.ProgressBarMaximum = newFactures.Count;
                    if (newFactures.Count > 0)
                    {
                        foreach (var facture in newFactures)
                        {
                            List<LigneFactureModel> items = ligneService.LIGNE_FACTURE_BYIDFActure(facture.IdFacture);
                            if (items != null && items.Count > 0)
                                listesFacture.Add(facture, items);

                            //worker.ReportProgress(i * 1);

                            progress.LBInfos = i + "%";
                            progress.ValueProgressBar = i;

                            i++;
                        }

                    }

                    string[] tabDates = CurrentLogSelected.periode.Split(new char[] { '-' });
                    DateTime datefrom = DateTime.Parse(tabDate[0]);
                    DateTime dateTo = DateTime.Parse(tabDate[1]);

                    fileName.Append(GlobalDatas.SIGLE);
                    fileName.Append("_");
                    fileName.Append(DateTime.Today.ToShortDateString().Replace("/", "").Trim());
                    fileName.Append("_");
                    fileName.Append(GlobalDatas.EXTRACT_VALIDE);
                    fileName.Append("_");
                    fileName.Append(societeCourante.IdSociete);
                    fileName.Append(datefrom.ToShortDateString().Replace("/", "").Trim());
                    fileName.Append(dateTo.ToShortDateString().Replace("/", "").Trim());
                    fileName.Append(".xml");

                    #region XML
		 
	

                    string chemin = CommonModule.GetLogPath(ParametersDatabase.PathLog);
                    using (FileStream stream = File.Create(chemin + "\\" + fileName.ToString()))
                    {
                        XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };
                        using (XmlWriter writer = XmlWriter.Create(stream, settings))
                        {
                            writer.WriteStartDocument();
                            writer.WriteStartElement("site");
                            writer.WriteStartElement("societe");
                            writer.WriteElementString("id", CurrentLogSelected.idSite.ToString () );
                            writer.WriteElementString("operation", "valide");
                            writer.WriteEndElement();

                            writer.WriteStartElement("factures");
                            foreach (var fact in listesFacture)
                            {
                                FactureModel newFacture = fact.Key;
                                List<LigneFactureModel> items = fact.Value;

                                writer.WriteStartElement("facture");
                                writer.WriteElementString("id", newFacture.IdFacture.ToString());
                                writer.WriteElementString("idObjet", newFacture.IdObjetFacture.ToString());
                                writer.WriteElementString("idExploitation", newFacture.IdExploitation.ToString());
                                writer.WriteElementString("idClient", newFacture.IdClient.ToString());
                                writer.WriteElementString("idTaxe", newFacture.IdTaxe.ToString());
                                writer.WriteElementString("idDevise", newFacture.IdDevise.ToString());
                                writer.WriteElementString("idStatut", newFacture.IdStatut.ToString());
                                writer.WriteElementString("idUserCreate", newFacture.IdCreerpar.ToString());
                                writer.WriteElementString("idUserMaj", newFacture.IdModifierPar.ToString());
                                writer.WriteElementString("idDepartement", newFacture.IdDepartement.ToString());
                                writer.WriteElementString("idSite", newFacture.IdSite.ToString());
                                writer.WriteElementString("idMode", newFacture.IdModePaiement.ToString());

                                writer.WriteElementString("numerofacture", newFacture.NumeroFacture);
                                writer.WriteElementString("moisPrestation", newFacture.MoisPrestation.Value.ToString());
                                writer.WriteElementString("dateCloture", newFacture.DateCloture.ToString());
                                writer.WriteElementString("dateEcheance", newFacture.DateEcheance.ToString());
                                writer.WriteElementString("datecreation", newFacture.DateCreation.ToString());
                                writer.WriteElementString("dateSortie", newFacture.DateSortie.ToString());
                                writer.WriteElementString("dateSuspension", newFacture.DateSuspension.ToString());
                                writer.WriteElementString("dateDepot", newFacture.DateDepot.ToString());
                                writer.WriteElementString("datenonValable", newFacture.DateNonValable.ToString());
                                writer.WriteElementString("datepaiement", newFacture.DatePaiement.ToString());
                                writer.WriteElementString("dateFacture", newFacture.DateFacture.ToString());//
                                writer.WriteElementString("dateModif", newFacture.DateModif.ToString());//

                                writer.WriteElementString("jourFin", newFacture.JourFinEcheance.ToString());
                                writer.WriteElementString("labelObjet", newFacture.Label_objet);
                                writer.WriteElementString("labeldept", newFacture.Label_Dep);
                                writer.WriteElementString("ttc", newFacture.TotalTTC.ToString());


                                if (items != null && items.Count > 0)
                                {
                                    writer.WriteStartElement("lignefactures");

                                    foreach (LigneFactureModel item in items)
                                    {

                                        writer.WriteStartElement("lignefacture");
                                        writer.WriteElementString("id", item.IdLigneFacture.ToString());
                                        writer.WriteElementString("idFacture", item.IdFacture.ToString());
                                        writer.WriteElementString("idproduit", item.IdProduit.ToString());
                                        writer.WriteElementString("iddetailProd", item.IdDetailProduit.ToString());

                                        writer.WriteElementString("description", item.Description);
                                        writer.WriteElementString("quantite", item.Quantite.ToString());
                                        writer.WriteElementString("prixu", item.PrixUnitaire.ToString());
                                        writer.WriteElementString("idSite", item.IdSite.ToString());
                                        writer.WriteElementString("datecreation", item.Datecreate.ToString());//
                                        writer.WriteElementString("dateModif", item.DateModif.ToString());//
                                        writer.WriteElementString("idUserCreate", item.IdUtilisateur.ToString());//
                                        writer.WriteElementString("idUserModif", item.IdUtilUpdate.ToString());//
                                        writer.WriteElementString("montantHt", item.MontanHT.ToString());//
                                        writer.WriteElementString("isModif", item.IsDelete.ToString());//

                                        //writer.WriteElementString("isSupprimer", item.e.ToString());
                                        writer.WriteEndElement();

                                    }

                                    writer.WriteEndElement();
                                }


                                writer.WriteEndElement();
                            }
                            writer.WriteEndElement();

                            writer.WriteEndElement();
                            writer.WriteEndDocument();

                        }
                    }

                  
                    //string idSite = CurrentLogSelected.nomfichier.Substring(CurrentLogSelected.nomfichier.LastIndexOf("_") + 1, 5);
                   // string dateExtractString = CurrentLogSelected.nomfichier.Substring(CurrentLogSelected.nomfichier .IndexOf("_") + 1, 8);
                    //DateTime dateExtract = DateTime.Parse(dateExtractString.Substring(0, 2) + "/" + dateExtractString.Substring(2, 2) + "/" + dateExtractString.Substring(4));
                    string periode = string.Format("{0}-{1}", dateDebut.ToShortDateString(), datefin.ToShortDateString());
                    LogExportIportModel.Extraction_ADD(0, DateTime.Today, DateTime.Today, dateDebut, periode, societeCourante.IdSociete, 0, "Fichier Valider Extrait", 3, fileName.ToString().Trim());
                    MessageFinal = "Fin traitement";

                   
                    //IsProgressExportVisibled = false;
                    IsBusy = false;

                    progress.Close();
                  
                    GetLogFiles();
                    canRefreshDirectory();

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "MESSAGE INFORMATION EXTRACTION DONNES VALIDEES";
                    view.ViewModel.Message = ex.Message ;
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                   
                }
            //};

            //worker.ProgressChanged += (o, args) =>
            //{
            //    progress.LBInfos = args.ProgressPercentage.ToString() + "%";
            //    progress.ValueProgressBar = args.ProgressPercentage;

            //};

           
            //    else
            //    {
            //        progress.Close();
            //        canLoadLogFileExport();
            //        canRefreshDirectory();
                   

            //        this.MouseCursor = null;
            //        this.IsBusy = false;
            //        //ProgressBarVisibility = false;
            //    }
            //};


            //worker.RunWorkerAsync();

                    #endregion
        }
        bool canExecuteReExportFile()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Execution  || CurrentDroit.Super )
                {
                    if (CurrentLogSelected != null)
                        if(CurrentLogSelected.id >0)
                            if (CurrentLogSelected.nomfichier !=string .Empty )
                           values = true;
                }
            }
            return values;

           
        }
        #endregion

        #region REGION EXTRACTION


        void canExportFile()
        {

          //  MessageFinal = "";
          //  IsProgressExportVisibled = true;
          //  IsBusy = true;
          // MyCallbackFunction();
          //  IsBusy = false;
          //  Messagerefreshing = string.Empty;
            try
            {
                DataSet datasetExtract = LogExportIportModel.Get_Export_ListeFacture((DateTime)InDateDebut, (DateTime)InDatefin, societeCourante.IdSociete);
                DataTable tablLangue=datasetExtract.Tables[8];
                DataTable tablProfileuserAd = datasetExtract.Tables[1];
                DataTable tablUserAdd = datasetExtract.Tables[2];
                DataTable tablProfiluserUpdate = datasetExtract.Tables[3];
                DataTable tabluserUpdate = datasetExtract.Tables[4];
                DataTable tablExoneration = datasetExtract.Tables[10];
                DataTable tablCompte = datasetExtract.Tables[11];
               // DataTable tabluserUpdate = datasetExtract.Tables[4];
                int nbreDonnées=0;
                foreach(DataTable table in datasetExtract.Tables)
                    nbreDonnées += table.Rows.Count;

                if (datasetExtract.Tables.Count > 0)
                {
                    BackgroundWorker worker = new BackgroundWorker();
                    worker.WorkerReportsProgress = true;
                    if (progress != null) progress.Close();
                    progress = new ProgressWindow();
                    progress.ProgressBarMinimum = 0;
                    progress.ProgressBarMaximum =nbreDonnées;
                    progress.Show();
                    worker.DoWork += (o, args) =>
                      {
                          try
                          {
                              StringBuilder fileName = new StringBuilder();
                              fileName.Append(GlobalDatas.SIGLE);
                              fileName.Append("_");
                              fileName.Append(DateTime.Today.ToShortDateString().Replace("/", "").Trim());
                              fileName.Append("_");
                              fileName.Append(GlobalDatas.EXTRACT);
                              fileName.Append("_");
                              fileName.Append(societeCourante.IdSociete);
                              fileName.Append(InDateDebut.Value.ToShortDateString().Replace("/", "").Trim());
                              fileName.Append(InDatefin.Value.ToShortDateString().Replace("/", "").Trim());

                              fileName.Append(".xml");
                              string chemin = CommonModule.GetLogPath(ParametersDatabase.PathLog);
                              using (FileStream stream = File.Create(chemin + "\\" + fileName.ToString()))
                              {
                                  XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

                                  int kk = 0;
                                  using (XmlWriter writer = XmlWriter.Create(stream, settings))
                                  {
                                      #region XML FILE

                                      #region Region Site

                                      writer.WriteStartDocument();
                                      writer.WriteStartElement("site");
                                      writer.WriteStartElement("societe");
                                      writer.WriteElementString("id", societeCourante.IdSociete.ToString());
                                      writer.WriteElementString("operation", "new");
                                      writer.WriteElementString("raisonSocial", societeCourante.RaisonSocial);
                                      writer.WriteElementString("nomManager", societeCourante.NomManager);
                                      writer.WriteElementString("titreManager", societeCourante.TitreManager);
                                      writer.WriteElementString("numeroContribuable", societeCourante.NomManager);
                                      writer.WriteElementString("numeroImmat", societeCourante.NumContribualbe);
                                      writer.WriteElementString("pays", societeCourante.Pays);
                                      writer.WriteElementString("ville", societeCourante.Ville);
                                      writer.WriteElementString("adress1", societeCourante.Adesse_1);
                                      writer.WriteElementString("adress2", societeCourante.Adresse_2);
                                      writer.WriteElementString("boitepostal", societeCourante.BoitePostal);
                                      writer.WriteElementString("telefone", societeCourante.Telephone);
                                      writer.WriteElementString("faxe", societeCourante.Faxe);
                                      writer.WriteElementString("sigle", societeCourante.SigleSite);
                                      writer.WriteElementString("siteweb", societeCourante.SiteIntenet);
                                      writer.WriteElementString("rc", societeCourante.Rc);
                                      writer.WriteEndElement();

                                      #endregion

                                      #region Region Langue
                                      kk++;
                                      writer.WriteStartElement("langues");
                                      foreach (DataRow rowL in tablLangue.Rows)
                                      {
                                          writer.WriteStartElement("langue");
                                          writer.WriteElementString("id", rowL["ID"].ToString());
                                          writer.WriteElementString("libelle", rowL["Libelle"].ToString() );
                                          writer.WriteElementString("Shortname", rowL["ShortName"].ToString() );
                                          writer.WriteEndElement();
                                          worker.ReportProgress(kk);

                                          kk++;
                                      }

                                      writer.WriteEndElement();
                                      #endregion

                                      #region Region users
                                    
                                      writer.WriteStartElement("profiles");
                                      foreach (DataRow row in tablProfileuserAd.Rows)
                                      {
                                          writer.WriteStartElement("profile");
                                          writer.WriteElementString("id",row["ID"].ToString());
                                          writer.WriteElementString("libelle",row["Libelle"].ToString() );
                                          writer.WriteElementString("Shortname", row["ShortName"].ToString()) ;
                                          writer.WriteEndElement();
                                          worker.ReportProgress(kk);

                                          kk++;
                                      }
                                     

                                      foreach (DataRow row in tablProfiluserUpdate.Rows)
                                      {
                                          writer.WriteStartElement("profile");
                                          writer.WriteElementString("id", row["ID"].ToString());
                                          writer.WriteElementString("libelle", row["Libelle"].ToString());
                                          writer.WriteElementString("Shortname", row["ShortName"].ToString());
                                          writer.WriteEndElement();
                                          worker.ReportProgress(kk);

                                          kk++;
                                      }
                                      writer.WriteEndElement();

                                      writer.WriteStartElement("utilisateurs");
                                      foreach (DataRow row in tablUserAdd.Rows)
                                      {
                                          writer.WriteStartElement("utilisateur");
                                          writer.WriteElementString("id",row["ID"].ToString() );
                                          writer.WriteElementString("nom",row["Nom"].ToString() );
                                          writer.WriteElementString("prenom",row["Prenom"].ToString() );
                                          writer.WriteElementString("fonction",row["Fonction"].ToString() );
                                          writer.WriteElementString("loggin", row["LogIn"].ToString());
                                          writer.WriteElementString("idprofile",row["ID_Profile"].ToString() );
                                          writer.WriteElementString("estverouiller",row["Estverouiller"].ToString() );
                                          writer.WriteElementString("dateVerouillage",row["DateVerouillage"].ToString() );
                                          writer.WriteElementString("idSite",row["ID_Site"].ToString() );
                                          writer.WriteEndElement();
                                          worker.ReportProgress(kk);

                                          kk++;
                                         
                                      }
                                    

                                      foreach (DataRow row in tabluserUpdate.Rows)
                                      {
                                          writer.WriteStartElement("utilisateur");
                                          writer.WriteElementString("id", row["ID"].ToString());
                                          writer.WriteElementString("nom", row["Nom"].ToString());
                                          writer.WriteElementString("prenom", row["Prenom"].ToString());
                                          writer.WriteElementString("fonction", row["Fonction"].ToString());
                                          writer.WriteElementString("loggin", row["LogIn"].ToString());
                                          writer.WriteElementString("idprofile", row["ID_Profile"].ToString());
                                          writer.WriteElementString("estverouiller", row["Estverouiller"].ToString());
                                          writer.WriteElementString("dateVerouillage", row["DateVerouillage"].ToString());
                                          writer.WriteElementString("idSite", row["ID_Site"].ToString());
                                          writer.WriteEndElement();
                                          worker.ReportProgress(kk);

                                          kk++;

                                         
                                      }
                                      writer.WriteEndElement();

                                      #endregion

                                      #region Region Exoneration

                                        writer.WriteStartElement("exonerations");
                                      foreach (DataRow row in tablExoneration.Rows)
                                        {
                                            writer.WriteStartElement("exoneration");
                                            writer.WriteElementString("id",row["ID"].ToString() );
                                            writer.WriteElementString("libelle", row["Libelle"].ToString());
                                            writer.WriteElementString("Shortname",row["ShortName"].ToString() );
                                            writer.WriteEndElement();

                                            worker.ReportProgress(kk);

                                            kk++;
                                        }
                                     writer.WriteEndElement();
                                      #endregion

                                     #region Region compte
                                         writer.WriteStartElement("comptes");
                              foreach (DataRow row in tablCompte.Rows)
                                {
                                    writer.WriteStartElement("compte");
                                    writer.WriteElementString("id",row["ID"].ToString() );
                                    writer.WriteElementString("idSite",row["ID_Site"].ToString() );
                                    writer.WriteElementString("numeroCompte", row["NumeroCompte"].ToString());
                                    writer.WriteElementString("nomBanque", row["Nom_banque"].ToString());
                                    writer.WriteElementString("ville",row["Ville"].ToString() );
                                    writer.WriteElementString("agence",row["Agence"].ToString());
                                    writer.WriteElementString("rue",row["Rue"].ToString() );
                                    writer.WriteElementString("telefone",row["Telephone"].ToString() );
                                    writer.WriteElementString("pays",row["Pays"].ToString() );
                                    writer.WriteElementString("quartier", row["quartier"].ToString());
                                    writer.WriteElementString("boitepostal", row["BoitePostal"].ToString());
                                    writer.WriteEndElement();
                                    worker.ReportProgress(kk);

                                    kk++;

                                  
                                }
                                writer.WriteEndElement();
                                     #endregion

                               writer.WriteEndDocument();
                                      #endregion
                                  }
                              }


                              //for (int i = 0; i <= nbreDonnées; i++)
                              //{
                              //    Thread.Sleep(100);
                              //    worker.ReportProgress(i);
                              //}

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
                         view.Owner = Application.Current.MainWindow;
                         view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                         view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                         view.ShowDialog();
                         this.MouseCursor = null;
                         this.IsBusy = false;
                         progress.Close();
                     }
                     else
                     {



                         MessageBox.Show(" fin traitement");
                         progress.Close();
                     }
                 };
                    worker.RunWorkerAsync();
                }
                else
                {
                    progress.Close();
                    MessageBox.Show("Pas de données à extraire");
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION DONNEES  EXTRACTION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        bool canExecuteExportFile()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Execution  || CurrentDroit.Super )
                {
                    if (InDateDebut.HasValue && InDatefin.HasValue)
                        values = true;
                }
            }
            return values;
        }
        #endregion

        #region REGION IMPORT

        void canImportFile()
        {
           // XElement parameter = XElement.Load(newPath);
            XElement document=null ;
            BackgroundWorker worker = new BackgroundWorker();
            bool isFileCorrect = false;
            worker.WorkerReportsProgress = true;
            if (progress != null) progress.Close();
            progress = new ProgressWindow();

              StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = Application.Current.MainWindow;
            messageBox.Title = "INFORMATION DE IMPORT FICHIER";
            messageBox.ViewModel.Message = "Voulez Vous importer ce fichier?";
            if (messageBox.ShowDialog().Value == true)
            {
                
                 progress.Show();


                 worker.DoWork += (o, args) =>
                 {
                     try
                     {
                         IsBusyImport = true;
                         IsProgressInportVisibled = true;
                         SocieteModel newSociete = new SocieteModel();
                         List<LangueModel> listelangue = new List<LangueModel>();
                         List<ProfileModel> listeProfile = new List<ProfileModel>();
                         List<UtilisateurModel> listeUtilisateur = new List<UtilisateurModel>();
                         List<ExonerationModel> listeExoneration = new List<ExonerationModel>();
                         List<CompteModel> listeCompte = new List<CompteModel>();
                         List<DeviseModel> listeDeviseClient = new List<DeviseModel>();
                         List<LibelleTermeModel> listeTerme = new List<LibelleTermeModel>();
                         List<TaxeModel> listeTaxeClient = new List<TaxeModel>();
                         List<ClientModel> listeClients = new List<ClientModel>();
                         List<ObjetFactureModel> listeObjets = new List<ObjetFactureModel>();
                         List<ObjetGenericModel> listeObjetGenerics = new List<ObjetGenericModel>();
                         List<ExploitationFactureModel> listeExploitations = new List<ExploitationFactureModel>();
                         List<TaxeModel> listeTaxe = new List<TaxeModel>();
                         List<DeviseModel> listeDevise = new List<DeviseModel>();
                         List<StatutModel> listeStatut = new List<StatutModel>();
                         List<DepartementModel> listeDepartement = new List<DepartementModel>();
                         List<ProduitModel> listeProduit = new List<ProduitModel>();
                         List<DetailProductModel> listeDetailProduit = new List<DetailProductModel>();

                         //List<FactureModel> listeFactures = new List<FactureModel>();
                         List<LigneFactureModel> listeLigneFacture = new List<LigneFactureModel>();
                         Dictionary<FactureModel, List<LigneFactureModel>> listeFactures = new Dictionary<FactureModel, List<LigneFactureModel>>();
                         string typeOperation = string.Empty;
                         string fichier = string.Empty;
                          
                         if (IsfileSelect)
                         {
                             if (FichierSelected.Nomfichier.StartsWith("SF") && FichierSelected.Nomfichier.EndsWith(".xml"))
                             {
                                // document = XElement.Load(FichierSelected.url);
                                 fichier = FichierSelected.Nomfichier;
                                 CheminFichier = FichierSelected.url;
                                 isFileCorrect = true;
                             }
                             else isFileCorrect = false;
                         }
                         else
                         {
                             if (CheminFichier.Contains("SF") && CheminFichier.EndsWith(".xml"))
                             {
                                 fichier = CheminFichier.Substring(CheminFichier.LastIndexOf("\\") + 1);
                                
                                isFileCorrect = true;

                             }  else isFileCorrect = false;
                            
                         }

                         if (  isFileCorrect)
                         {
                            

                             if (fichier.IndexOf("_") == 2 && ((fichier.LastIndexOf("_") == 15) || (fichier.LastIndexOf("_") == 16)))
                             {
                                 document = XElement.Load(CheminFichier);
                                 IsProgressInportVisibled = true;

                               #region Region SOCIETE


                         var querySocieteinfo = from ste in document.Elements()
                                                where ste.Name.ToString().Trim() == "societe".Trim()
                                                select ste;

                         foreach (var el in querySocieteinfo)
                         {
                             newSociete.IdSociete = el.Element("id").Value != "" ? int.Parse(el.Element("id").Value) : 0;
                             typeOperation = el.Element("operation").Value;
                             if (typeOperation.Contains("new"))
                             {

                                 newSociete.RaisonSocial = el.Element("raisonSocial").Value;
                                 newSociete.NomManager = el.Element("nomManager").Value;
                                 newSociete.TitreManager = el.Element("titreManager").Value;
                                 newSociete.NumContribualbe = el.Element("numeroContribuable").Value;
                                 newSociete.NumImmatriculation = el.Element("numeroImmat").Value;
                                 newSociete.Pays = el.Element("pays").Value;
                                 newSociete.Ville = el.Element("ville").Value;
                                 newSociete.Adesse_1 = el.Element("adress1").Value;
                                 newSociete.Adresse_2 = el.Element("adress2").Value;
                                 newSociete.BoitePostal = el.Element("boitepostal").Value;
                                 newSociete.Telephone = el.Element("telefone").Value;
                                 newSociete.Faxe = el.Element("faxe").Value;
                                 newSociete.SigleSite = el.Element("sigle").Value;
                                 newSociete.SiteIntenet = el.Element("siteweb").Value;
                                 newSociete.SiteIntenet = el.Element("rc").Value;
                             }

                         }


                         #endregion

                                if (typeOperation.Contains("new"))
                                 {

                                         #region Region LANGUE


                                         var querylangues = from p in document.Elements()
                                                            where p.Name == "langues"
                                                            select p;
                                         foreach (var qlangues in querylangues)
                                         {
                                             IEnumerable<XElement> elementsLangue = qlangues.Elements("langue");
                                             LangueModel newlangue = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newlangue = new LangueModel();
                                                 newlangue.Id = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newlangue.Libelle = l.Element("libelle").Value;
                                                 newlangue.Shortname = l.Element("Shortname").Value;
                                                 listelangue.Add(newlangue);
                                             }
                                         }

                                         #endregion

                                         #region Region PROFILE

                                         var queryProfile = from p in document.Elements()
                                                            where p.Name == "profiles"
                                                            select p;
                                         foreach (var queryobj in queryProfile)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("profile");
                                             ProfileModel newprofile = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newprofile = new ProfileModel();
                                                 newprofile.IdProfile = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newprofile.Libelle = l.Element("libelle").Value;
                                                 newprofile.ShortName = l.Element("Shortname").Value;
                                                 listeProfile.Add(newprofile);
                                             }
                                         }
                                         #endregion

                                         #region Region UTILISATEURS

                                         var queryUser = from p in document.Elements()
                                                         where p.Name == "utilisateurs"
                                                         select p;
                                         foreach (var queryobj in queryUser)
                                         {
                                             IEnumerable<XElement> elementsObjet = queryobj.Elements("utilisateur");
                                             UtilisateurModel newuser = null;

                                             foreach (XElement l in elementsObjet)
                                             {
                                                 newuser = new UtilisateurModel();
                                                 newuser.Id = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newuser.Nom = l.Element("nom").Value;
                                                 newuser.Prenom = l.Element("prenom").Value;
                                                 newuser.Fonction = l.Element("fonction").Value;
                                                 newuser.Loggin = l.Element("loggin").Value;
                                                 newuser.IdProfile = int.Parse(l.Element("idprofile").Value);
                                                 newuser.IdSite = int.Parse(l.Element("idSite").Value);
                                                 listeUtilisateur.Add(newuser);
                                             }
                                         }
                                         #endregion

                                         #region Region EXONERATION

                                         var queryExoneration = from p in document.Elements()
                                                                where p.Name == "exonerations"
                                                                select p;
                                         foreach (var queryobj in queryExoneration)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("exoneration");
                                             ExonerationModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new ExonerationModel();
                                                 newObjet.ID = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.Libelle = l.Element("libelle").Value;
                                                 newObjet.CourtDesc = l.Element("Shortname").Value;
                                                 listeExoneration.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region COMPTE

                                         var queryCompte = from p in document.Elements()
                                                           where p.Name == "comptes"
                                                           select p;
                                         foreach (var queryobj in queryCompte)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("compte");
                                             CompteModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new CompteModel();
                                                 newObjet.ID = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.IDSite = int.Parse(l.Element("idSite").Value);
                                                 newObjet.NumeroCompte = l.Element("numeroCompte").Value;
                                                 newObjet.NomBanque = l.Element("nomBanque").Value;
                                                 newObjet.Ville = l.Element("ville").Value;
                                                 newObjet.Agence = l.Element("agence").Value;
                                                 newObjet.Rue = l.Element("rue").Value;
                                                 newObjet.Telephone = l.Element("telefone").Value;
                                                 newObjet.Pays = l.Element("pays").Value;
                                                 newObjet.Quartier = l.Element("quartier").Value;
                                                 newObjet.BoitePostal = l.Element("boitepostal").Value;


                                                 listeCompte.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region DEVISE CLIENT

                                         var querydevises = from p in document.Elements()
                                                            where p.Name == "deviseClients"
                                                            select p;
                                         foreach (var queryobj in querydevises)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("deviseCli");
                                             DeviseModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new DeviseModel();
                                                 newObjet.ID_Devise = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.Libelle = l.Element("libelle").Value;
                                                 newObjet.Taux = l.Element("taux").Value;
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 listeDeviseClient.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region TAXES CLIENT
                                         var queryTAxeClients = from p in document.Elements()
                                                                where p.Name == "taxesClient"
                                                                select p;
                                         foreach (var queryobj in queryTAxeClients)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("taxeCli");
                                             TaxeModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new TaxeModel();
                                                 newObjet.ID_Taxe = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.Libelle = l.Element("libelle").Value;
                                                 newObjet.Taux = l.Element("taux").Value;
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 listeTaxeClient.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region TERMES

                                         var queryTermePaiement = from p in document.Elements()
                                                                  where p.Name == "termePaiement"
                                                                  select p;
                                         foreach (var queryobj in queryTermePaiement)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("terme");
                                             LibelleTermeModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new LibelleTermeModel();
                                                 newObjet.ID = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.Desciption = l.Element("description").Value;
                                                 newObjet.CourtDescription = l.Element("shortDesc").Value;
                                                 newObjet.Jour = l.Element("jour").Value != "" ? int.Parse(l.Element("jour").Value) : 0;
                                                 listeTerme.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region CLIENTS


                                         var queryClients = from p in document.Elements()
                                                            where p.Name == "clients"
                                                            select p;
                                         foreach (var queryobj in queryClients)
                                         {
                                             IEnumerable<XElement> elements = queryobj.Elements("client");
                                             ClientModel newObjet = null;

                                             foreach (XElement l in elements)
                                             {
                                                 newObjet = new ClientModel();
                                                 newObjet.IdClient = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 newObjet.IdLangue = int.Parse(l.Element("idLangue").Value);
                                                 newObjet.IdExonere = int.Parse(l.Element("idExoneration").Value);
                                                 newObjet.IdCompte = int.Parse(l.Element("idCompte").Value);
                                                 newObjet.IdDeviseFact = int.Parse(l.Element("idDevise").Value);
                                                 newObjet.IdTerme = int.Parse(l.Element("idTerme").Value);
                                                 newObjet.Idporata = int.Parse(l.Element("idTaxeProrata").Value);
                                                 newObjet.NomClient = l.Element("nomClient").Value;
                                                 newObjet.NumeroContribuable = l.Element("numContrib").Value;
                                                 newObjet.BoitePostal = l.Element("boitePostal").Value;
                                                 newObjet.NumemroImat = l.Element("numImmat").Value;
                                                 newObjet.Ville = l.Element("ville").Value;
                                                 newObjet.Rue1 = l.Element("rue1").Value;
                                                 newObjet.Rue2 = l.Element("rue2").Value;
                                                 newObjet.TermeNombre = l.Element("termeJour").Value != "" ? int.Parse(l.Element("termeJour").Value) : 0;
                                                 newObjet.TermeDescription = l.Element("termeDesc").Value;
                                                 newObjet.DateEcheance = l.Element("dateEcheance").Value;

                                                 listeClients.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region OBJEt GENERICS

                                         var queryObjetGeneric = from p in document.Elements()
                                                                 where p.Name == "objetsgenerics"
                                                                 select p;
                                         foreach (var queryobj in queryObjetGeneric)
                                         {
                                             IEnumerable<XElement> elementsFacture = queryobj.Elements("objetgeneric");
                                             ObjetGenericModel newObjet = null;

                                             foreach (XElement l in elementsFacture)
                                             {
                                                 newObjet = new ObjetGenericModel();
                                                 newObjet.IdObjetg = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.IdLangue = int.Parse(l.Element("idLangue").Value);
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 newObjet.Libelle = l.Element("libelle").Value;

                                                 listeObjetGenerics.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region OBJEt FACTURE

                                         var queryObjetFacture = from p in document.Elements()
                                                                 where p.Name == "objetsfactures"
                                                                 select p;
                                         foreach (var queryobj in queryObjetFacture)
                                         {
                                             IEnumerable<XElement> elementsFacture = queryobj.Elements("objetfacture");
                                             ObjetFactureModel newObjet = null;

                                             foreach (XElement l in elementsFacture)
                                             {
                                                 newObjet = new ObjetFactureModel();
                                                 newObjet.IdObjet = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.IdobjetGen = int.Parse(l.Element("idObjetGen").Value);
                                                 newObjet.IdClient = int.Parse(l.Element("idClient").Value);
                                                 newObjet.Libelle = l.Element("libelle").Value;

                                                 listeObjets.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region EXPLOITATION FACTURE

                                         var queryExploitation = from p in document.Elements()
                                                                 where p.Name == "exploitations"
                                                                 select p;
                                         foreach (var queryobj in queryExploitation)
                                         {
                                             IEnumerable<XElement> elementsFacture = queryobj.Elements("exploitation");
                                             ExploitationFactureModel newObjet = null;

                                             foreach (XElement l in elementsFacture)
                                             {
                                                 newObjet = new ExploitationFactureModel();
                                                 newObjet.IdExploitation = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.IdLangue = int.Parse(l.Element("idlangue").Value);
                                                 newObjet.IdSite = int.Parse(l.Element("idsite").Value);
                                                 newObjet.Libelle = l.Element("libelle").Value;

                                                 listeExploitations.Add(newObjet);
                                             }
                                         }
                                         #endregion


                                         #region Region DEVISE

                                         var querydevisesFact = from p in document.Elements()
                                                                where p.Name == "devises"
                                                                select p;
                                         foreach (var queryobj in querydevisesFact)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("devise");
                                             DeviseModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new DeviseModel();
                                                 newObjet.ID_Devise = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.Libelle = l.Element("libelle").Value;
                                                 newObjet.Taux = l.Element("taux").Value;
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 listeDevise.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region TAXES

                                         var queryTAxeFact = from p in document.Elements()
                                                             where p.Name == "taxes"
                                                             select p;
                                         foreach (var queryobj in queryTAxeFact)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("taxe");
                                             TaxeModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new TaxeModel();
                                                 newObjet.ID_Taxe = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.Libelle = l.Element("libelle").Value;
                                                 newObjet.Taux = l.Element("taux").Value;
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 listeTaxe.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region STATUT

                                         var queryStatut = from p in document.Elements()
                                                           where p.Name == "statuts"
                                                           select p;
                                         foreach (var queryobj in queryStatut)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("statut");
                                             StatutModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new StatutModel();
                                                 newObjet.IdStatut = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.IdLangue = int.Parse(l.Element("idlangue").Value);
                                                 newObjet.Libelle = l.Element("libelle").Value;
                                                 newObjet.CourtDesc = l.Element("shortDesc").Value;
                                                 listeStatut.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region DEPARTEMENT

                                         var queryDepartement = from p in document.Elements()
                                                                where p.Name == "departements"
                                                                select p;
                                         foreach (var queryobj in queryDepartement)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("departement");
                                             DepartementModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new DepartementModel();
                                                 newObjet.IdDep = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.Libelle = l.Element("libelle").Value;
                                                 newObjet.CourtLibelle = l.Element("shortDesc").Value;
                                                 newObjet.Autre = l.Element("autre").Value;
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 listeDepartement.Add(newObjet);
                                             }
                                         }
                                         #endregion

                                         #region Region PRODUIT

                                         var queryProduit = from p in document.Elements()
                                                            where p.Name == "produits"
                                                            select p;
                                         foreach (var queryobj in queryProduit)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("produit");
                                             ProduitModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new ProduitModel();
                                                 newObjet.IdProduit = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 newObjet.IdLangue = int.Parse(l.Element("idLangue").Value);
                                                 newObjet.Libelle = l.Element("libelle").Value;
                                                 newObjet.PrixUnitaire = decimal.Parse(l.Element("prixunitaire").Value);
                                                 newObjet.CompteOhada = l.Element("codeOhada").Value;

                                                 listeProduit.Add(newObjet);
                                             }
                                         
                                         }
                                         #endregion


                                         #region Region DETAIL PRODUIT

                                         var queryDetailProduit = from p in document.Elements()
                                                                  where p.Name == "detailProduits"
                                                                  select p;
                                         foreach (var queryobj in queryDetailProduit)
                                         {
                                             IEnumerable<XElement> elementsLangue = queryobj.Elements("detailProduit");
                                             DetailProductModel newObjet = null;

                                             foreach (XElement l in elementsLangue)
                                             {
                                                 newObjet = new DetailProductModel();
                                                 newObjet.IdDetail = l.Element("id").Value != "" ? int.Parse(l.Element("id").Value) : -1;
                                                 newObjet.IdClient = int.Parse(l.Element("idClient").Value);
                                                 newObjet.IdProduit = int.Parse(l.Element("idProduit").Value);
                                                 newObjet.Quantite = int.Parse(l.Element("quantite").Value);
                                                 newObjet.Prixunitaire = decimal.Parse(l.Element("prixunitaire").Value);
                                                 newObjet.Exonerer = bool.Parse(l.Element("isExonere").Value);
                                                 newObjet.Isprorata = bool.Parse(l.Element("isprorata").Value);
                                                 newObjet.IdSite = int.Parse(l.Element("idSite").Value);
                                                 newObjet.IdExploitation = int.Parse(l.Element("idExploitation").Value);
                                                 newObjet.Specialfact = bool.Parse(l.Element("specialfact").Value);
                                                 listeDetailProduit.Add(newObjet);
                                             }
                                         }
                                         #endregion
                         }


                         #region Region FACTURE ET ITEMS

                         var queryFacture = (from f in document.Elements()
                                            where f.Name == "factures"
                                            select f).ToList ();
                         foreach (var queryobj in queryFacture)
                         {
                             IEnumerable<XElement> elementsFacture = queryobj.Elements("facture");
                             FactureModel facture = null;
                             foreach (XElement l in elementsFacture)
                             {
                                 facture = new FactureModel();
                                 facture.IdFacture = l.Element("id").Value != "" ? long.Parse(l.Element("id").Value) : -1;
                                 facture.IdObjetFacture = l.Element("idObjet").Value !=string .Empty ? int.Parse(l.Element("idObjet").Value):0;
                                 facture.IdExploitation =l.Element("idExploitation").Value !=string .Empty ? int.Parse(l.Element("idExploitation").Value):0;
                                 facture.IdClient = l.Element("idClient").Value!=string .Empty ? int.Parse(l.Element("idClient").Value):0;
                                 facture.IdTaxe =l.Element("idTaxe").Value !=string .Empty ? int.Parse(l.Element("idTaxe").Value):0;
                                 facture.IdDevise =l.Element("idDevise").Value !=string .Empty ? int.Parse(l.Element("idDevise").Value):0;
                                 facture.IdStatut =l.Element("idStatut").Value !=string .Empty ? int.Parse(l.Element("idStatut").Value):0;
                                 facture.IdCreerpar =l.Element("idUserCreate").Value!=string .Empty ? int.Parse(l.Element("idUserCreate").Value):0;
                                 facture.IdDepartement =l.Element("idDepartement").Value!=string .Empty ? int.Parse(l.Element("idDepartement").Value):0;
                                 facture.IdSite =l.Element("idSite").Value!=string.Empty ?int.Parse(l.Element("idSite").Value):0;
                                 facture.IdModePaiement =l.Element("idMode").Value !=string .Empty ? int.Parse(l.Element("idMode").Value):0;
                                 facture.IdModifierPar =l.Element("idUserMaj").Value!=string .Empty ? int.Parse(l.Element("idUserMaj").Value):0;

                                 facture.NumeroFacture = l.Element("numerofacture").Value;
                                 facture.MoisPrestation = DateTime.Parse(l.Element("moisPrestation").Value);
                                 facture.DateCloture = l.Element("dateCloture").Value != "" ? DateTime.Parse(l.Element("dateCloture").Value) : (DateTime?)null;
                                 facture.DateEcheance = l.Element("dateEcheance").Value != "" ? DateTime.Parse(l.Element("dateEcheance").Value) : (DateTime?)null;
                                 facture.DateCreation = DateTime.Parse(l.Element("datecreation").Value);
                                 facture.DateSortie = l.Element("dateSortie").Value != "" ? DateTime.Parse(l.Element("dateSortie").Value) : (DateTime?)null;
                                 facture.DateSuspension = l.Element("dateSuspension").Value != "" ? DateTime.Parse(l.Element("dateSuspension").Value) : (DateTime?)null;
                                 facture.DateDepot = l.Element("dateDepot").Value != "" ? DateTime.Parse(l.Element("dateDepot").Value) : (DateTime?)null;
                                 facture.DateNonValable = l.Element("datenonValable").Value != "" ? DateTime.Parse(l.Element("datenonValable").Value) : (DateTime?)null;
                                 facture.DatePaiement = l.Element("datepaiement").Value != "" ? DateTime.Parse(l.Element("datepaiement").Value) : (DateTime?)null;
                                 facture.DateFacture = l.Element("dateFacture").Value != "" ? DateTime.Parse(l.Element("dateFacture").Value) : (DateTime?)null;
                                 facture.DateModif = l.Element("dateModif").Value != "" ? DateTime.Parse(l.Element("dateModif").Value) : (DateTime?)null;
                                 facture.JourFinEcheance = l.Element("jourFin").Value;
                                 facture.Label_objet = l.Element("labelObjet").Value;
                                 facture.Label_Dep = l.Element("labeldept").Value;
                                 facture.TotalTTC =l.Element("ttc").Value !=string .Empty ? double.Parse(l.Element("ttc").Value):0;

                                 XElement eleItems = l.Element("lignefactures");
                                 IEnumerable<XElement> xelementItems = eleItems.Elements("lignefacture");
                                 LigneFactureModel litem = null;
                                 listeLigneFacture = new List<LigneFactureModel>();
                                 foreach (XElement item in xelementItems)
                                 {
                                     litem = new LigneFactureModel();
                                     litem.IdLigneFacture = item.Element("id").Value != "" ? long.Parse(item.Element("id").Value) : -1;
                                     litem.IdFacture = long.Parse(item.Element("idFacture").Value);
                                     litem.IdProduit = int.Parse(item.Element("idproduit").Value);
                                     litem.IdDetailProduit = int.Parse(item.Element("iddetailProd").Value);

                                     litem.Description = item.Element("description").Value;
                                     litem.Quantite =item.Element("quantite").Value !=string .Empty ? int.Parse(item.Element("quantite").Value):0;
                                     litem.PrixUnitaire = item.Element("prixu").Value != string.Empty ? decimal.Parse(item.Element("prixu").Value) : 0;
                                     litem.IdSite = item.Element("idSite").Value != string.Empty ? int.Parse(item.Element("idSite").Value) : 0;
                                    
                                     DateTime result;
                                     if (item.Element("datecreation").Value != string.Empty)
                                     {
                                         if (DateTime.TryParse(item.Element("datecreation").Value, out  result))
                                             litem.Datecreate = result;
                                     }else 
                                     //litem.Datecreate =null (item.Element("datecreation").Value != string.Empty) ?
                                     //    DateTime.Parse(item.Element("datecreation").Value) : (DateTime?)null; //DateTime.Parse(item.Element("datecreation").Value);
                                     //object valuesDate = item.Element("dateModif").Value;

                                     if (item.Element("dateModif").Value != string.Empty)
                                     {
                                         if (DateTime.TryParse(item.Element("dateModif").Value, out result))
                                             litem.DateModif = result;
                                     }else 
                                     litem.DateModif = null; //(item.Element("dateModif").Value != string.Empty || item.Element("dateModif").Value !=null ) ? DateTime.Parse(item.Element("dateModif").Value) : (DateTime?)null;
                                     litem.IdUtilisateur = int.Parse(item.Element("idUserCreate").Value);

                                     object de = item.Element("idUserModif").Value;
                                     litem.IdUtilUpdate = item.Element("idUserModif").Value != null ? int.Parse(item.Element("idUserModif").Value) : 0;

                                     litem.MontanHT = decimal.Parse(item.Element("montantHt").Value);
                                     litem.IsDelete = bool.Parse(item.Element("isModif").Value);

                                     listeLigneFacture.Add(litem);
                                 }

                                 listeFactures.Add(facture, listeLigneFacture);

                             }
                         }
                         #endregion


                         #region Region ADD DATAS

                         SocieteModel societeService = new SocieteModel();
                         LangueModel langueService = new LangueModel();
                         ProfileModel profileService = new ProfileModel();
                         UtilisateurModel userservice = new UtilisateurModel();
                         CompteModel compteService = new CompteModel();
                         DeviseModel deviseService = new DeviseModel();
                         TaxeModel taxeService = new TaxeModel();
                         ClientModel clientservice = new ClientModel();
                         ObjetFactureModel objetService = new ObjetFactureModel();
                         ObjetGenericModel objetgenService = new ObjetGenericModel();
                         ExploitationFactureModel exploitationService = new ExploitationFactureModel();
                         DepartementModel depService = new DepartementModel();
                         ProduitModel produitService = new ProduitModel();
                         DetailProductModel detailService = new DetailProductModel();
                         FactureModel factureservice = new FactureModel();
                         LigneFactureModel ligneService = new LigneFactureModel();

                         if (typeOperation.Contains("new"))
                         {
                             societeService.SOCIETE_ADD(newSociete);
                             foreach (LangueModel l in listelangue)
                                 langueService.LANGUE_ADD(l);



                             foreach (UtilisateurModel user in listeUtilisateur)
                                 userservice.UTILISATEUR_INSERT(user);



                             foreach (CompteModel compte in listeCompte)
                                 compteService.COMPTE_ADD(compte);

                             foreach (DeviseModel devise in listeDeviseClient)
                                 deviseService.Devise_ADD(devise);
                             foreach (DeviseModel devise in listeDevise)
                                 deviseService.Devise_ADD(devise);
                             //proratas
                             foreach (TaxeModel taxeCli in listeTaxeClient)
                             {
                                 if (taxeCli.ID_Taxe > 0)
                                     taxeService.Taxe_ADD(taxeCli);
                             }
                             // taxes
                             foreach (TaxeModel taxe in listeTaxe)
                                 taxeService.Taxe_ADD(taxe);

                             //foreach (ClientModel client in listeClients)
                             //    clientservice.CLIENT_ADD(client);

                             foreach (var objg in listeObjetGenerics)
                                 objetgenService.OBJECT_GENERIC_ADD(objg);

                             foreach (ObjetFactureModel obj in listeObjets)
                                 objetService.OBJECT_FACTURE_ADD(obj, newSociete.IdSociete);

                             foreach (ExploitationFactureModel exploit in listeExploitations)
                                 exploitationService.EXPLOITATION_FACTURE_ADD(exploit);
                             foreach (DepartementModel dep in listeDepartement)
                                 depService.Departement_ADD(dep);

                             foreach (ProduitModel prod in listeProduit)
                                 produitService.Produit_ADD(prod);

                             foreach (DetailProductModel detail in listeDetailProduit)
                                 detailService.DETAIL_PRODUIT_ADD(detail);
                             int i = 0;

                             foreach (var factures in listeFactures)
                             {
                                 factureservice.FACTURE_ADD_LOG(factures.Key);
                                 List<LigneFactureModel> liste = factures.Value;

                                 foreach (LigneFactureModel ligne in liste)
                                 {
                                     ligneService.LIGNE_FACTURE_ADD(ligne);
                                 }

                                 worker.ReportProgress(i * 1);
                                 i++;
                             }

                             //if (CommonModule.GetDirectoryLog(ParametersDatabase.PathBackUpLog))
                             //{
                                 string cheminBackUp = CommonModule.GetLogBAckUpPath(ParametersDatabase.PathBackUpLog);
                                 if (File.Exists(cheminBackUp + "\\" + FichierSelected.Nomfichier))
                                     FichierSelected.Nomfichier = FichierSelected.Nomfichier;
                                 File.Copy(CheminFichier, cheminBackUp + "\\" + FichierSelected.Nomfichier);
                                 File.Delete(CheminFichier);
                             //}

                             string dateDebut = fichier.Substring(fichier.LastIndexOf("_") + 6, 8);
                             string datefin = fichier.Substring(fichier.LastIndexOf("_") + 14, 8);
                             string idSite = fichier.Substring(fichier.LastIndexOf("_") + 1, 5);

                             string periodes = string.Format("{0}-{1}", dateDebut.Substring(0, 2) + "/" + dateDebut.Substring(2, 2) + "/" + dateDebut.Substring(4),
                                datefin.Substring(0, 2) + "/" + dateDebut.Substring(2, 2) + "/" + dateDebut.Substring(4));
                             string dateExtractString=fichier.Substring(fichier.IndexOf ("_")+1,8);
                             DateTime dateExtract =DateTime.Parse ( dateExtractString.Substring(0, 2) + "/" + dateExtractString.Substring(2, 2) + "/" + dateExtractString.Substring(4));

                             LogExportIportModel.Extraction_ADD(0, dateExtract, DateTime.Today, DateTime.Today, periodes, int.Parse(idSite), societeCourante.IdSociete, "Fichier Importer et archiver", 2, fichier.ToString().Trim());

                             Messagerefreshing = "Fichier Importé";
                         }
                         else
                         {
                             //Operation d'intégration des données validés depuis un site centrale

                             if (ListeLog != null)
                             {
                                 if (ListeLog.Exists(f => f.nomfichier.Equals(fichier)))
                                 {
                                     if (newSociete.IdSociete == societeCourante.IdSociete)
                                     {
                                         foreach (var factures in listeFactures)
                                         {
                                             factureservice.FACTURE_ADD_LOG(factures.Key);
                                             List<LigneFactureModel> liste = factures.Value;

                                             foreach (LigneFactureModel ligne in liste)
                                             {
                                                 ligneService.LIGNE_FACTURE_ADD(ligne);
                                             }
                                         }

                                         //if (CommonModule.GetDirectoryLog(ParametersDatabase.PathBackUpLog))
                                         //{
                                         string cheminBackUp = CommonModule.GetLogBAckUpPath(ParametersDatabase.PathBackUpLog);
                                             if (File.Exists(cheminBackUp + "\\" + FichierSelected.Nomfichier))
                                                 FichierSelected.Nomfichier = "1_" + FichierSelected.Nomfichier;
                                             File.Copy(FichierSelected.url, cheminBackUp + "\\" + FichierSelected.Nomfichier);
                                             File.Delete(FichierSelected.url);
                                        // }

                                         string dateDebut = fichier.Substring(fichier.LastIndexOf("_") + 6, 8);
                                         string datefin = fichier.Substring(fichier.LastIndexOf("_") + 14, 8);
                                         string idSite = fichier.Substring(fichier.LastIndexOf("_") + 1, 5);

                                         string periodes = string.Format("{0}-{1}", dateDebut.Substring(0, 2) + "/" + dateDebut.Substring(2, 2) + "/" + dateDebut.Substring(4),
                                            datefin.Substring(0, 2) + "/" + dateDebut.Substring(2, 2) + "/" + dateDebut.Substring(4));
                                         string dateExtractString = fichier.Substring(fichier.IndexOf("_") + 1, 8);
                                         DateTime dateExtract = DateTime.Parse(dateExtractString.Substring(0, 2) + "/" + dateExtractString.Substring(2, 2) + "/" + dateExtractString.Substring(4));

                                         LogExportIportModel.Extraction_ADD(0, dateExtract, DateTime.Today, DateTime.Today, periodes, int.Parse(idSite), societeCourante.IdSociete, "Données validées Importer et archiver", 4, fichier.ToString().Trim());

                                         Messagerefreshing = "Fichier Importé";
                                     }
                                     else
                                         MessageBox.Show("Ces données ne sont pas conforme pour ce site");

                                 }
                                 else
                                     MessageBox.Show("Ce fichier de validation ne peut pas être traité car son fichier d'estraction n'existe pas","IMPORT FICHIER VALIDER");
                             }
                           
                         }

                         FichierSelected = null;
                         IsProgressInportVisibled = false;
                         CheminFichier = string.Empty;




                         #endregion

                        
                         IsBusyImport = false;
                        
                             }
                             else
                                 MessageBox.Show("Ce fichier ne respecte pas la spécification requise de Sysfact");
                         }
                         else
                             MessageBox.Show("Ce fichier ne respecte pas la spécification requise de Sysfact");

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
            }

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

                  
                        progress.Close();
                }
                else
                {
                    progress.Close();
                    if (isFileCorrect )
                    {
                        GetLogFiles();
                        canRefreshDirectory();

                    }
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    //ProgressBarVisibility = false;
                }
            };


            worker.RunWorkerAsync();

        }

        bool canExecuteimportFile()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Execution  || CurrentDroit.Super )
                {
                    if (FichierSelected != null ||( CheminFichier != null && CheminFichier.EndsWith (".xml")))
                        values = true;
                }
            }
            return values;
            
        }
        #endregion

        #region REGION REFRESH

        void canLoadLogFileExport()
        {
            GetLogFileExportList();
            ListeLog = null;
            
            if (logExportList!=null )
            NobreEnregistrement = logExportList.Count.ToString();
           

        }

        void canLoadLogFileImport()
        {
            GetLogFileImportist();
            ListeLog = null;
            
            if (ListeLog!=null )
            NobreEnregistrement = ListeLog.Count.ToString();

            

        }
      
        void canRefreshDirectory()
        {
            try
            {
                string extension = ".xml";
                List<LignesFichiers> newListe = new List<LignesFichiers>();
                LignesFichiers fichierCourant = null;
               // if(CommonModule.GetDirectoryLog(ParametersDatabase.PathLog  ))
                //{
                   FileInfo[] newListeFiles = CommonModule.GetListeFiles(ParametersDatabase.PathLog);
                   if (newListeFiles != null)
                   {
                       // System.Globalization.CultureInfo culture = new CultureInfo();
                       if (newListeFiles.Length > 0)
                       {

                           foreach (FileInfo fichier in newListeFiles)
                           {

                               fichierCourant = new LignesFichiers();
                               string ext = fichier.Directory.Extension;
                               DateTime dateCreation = fichier.CreationTime;
                               fichierCourant.Nomfichier = fichier.Name;
                               fichierCourant.dateFichiers = fichier.CreationTime.ToShortDateString();
                               fichierCourant.url = fichier.FullName;
                               fichierCourant.adresse = getSocietename(fichier.FullName);
                               string datedebut = fichier.Name.Substring(fichier.Name.LastIndexOf("_") + 6, 8);
                               string datefin = fichier.Name.Substring(fichier.Name.LastIndexOf("_") + 14, 8);
                               datedebut = datedebut.Substring(0, 2) + "/" + datedebut.Substring(2, 2) + "/" + datedebut.Substring(4);
                               datefin = datefin.Substring(0, 2) + "/" + datefin.Substring(2, 2) + "/" + datefin.Substring(4);

                               fichierCourant.periode = string.Format("{0} au {1}", string.Format(System.Globalization.CultureInfo.InvariantCulture, datedebut),
                                   string.Format(System.Globalization.CultureInfo.InvariantCulture, datefin));
                               // string datefins = FichierSelected.Nomfichier.Substring(13, 8);

                               newListe.Add(fichierCourant);


                           }
                           ListeFichiersDossiers = newListe;
                           Messagerefreshing = string.Empty;
                       }
                       else
                       {
                           Messagerefreshing = "Pas de Fichiers dans la pile !";
                           if (ListeFichiersDossiers != null)
                               ListeFichiersDossiers = null;
                       }
                   }
                   else Messagerefreshing = "Pas de Fichiers dans la pile !";
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION RAFRAICHISSEMENT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        string  getSocietename(string url)
        {
            string nomSite = string.Empty;
            XElement Ndocument = XElement.Load(url);
            var querySocieteinfo = from ste in Ndocument.Elements()
                                   where ste.Name.ToString().Trim() == "societe".Trim()
                                   select ste;
            foreach (var el in querySocieteinfo)
            {
                if (el.Element("operation").Value.Equals ("new"))
                nomSite = string.Format("{0}  {1}", el.Element("pays").Value, el.Element("ville").Value);
                else nomSite = string.Format("{0}  {1}", string .Empty , string .Empty );
            }
            return nomSite;
        }

        #endregion

        void canSaveFacture()
        {
           
        }

        #region REPERTOIR
        
      
        void canRepertoire()
        {
            try
            {
                Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
                dlg.FileName = "Document"; // Default file name
                dlg.DefaultExt = ".xml"; // Default file extension
                dlg.Filter = "Text documents (.xml)|*.xml"; // Filter files by extension

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
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION RESEARCH File";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }

        }

        bool canExecuteSavefacture()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.ExportDB || CurrentDroit.Developpeur )
                {
                        values = true;
                }
            }
            return values;
           
        }

        void canClearRepertoire()
        {
            if (!string.IsNullOrEmpty(CheminFichier))
            {
                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                messageBox.Owner = Application.Current.MainWindow;
                messageBox.Title = "INFORMATION DE SUPPRESSION CHEMEIN FICHIER";
                messageBox.ViewModel.Message = "Annuler le fichier actuel";
                if (messageBox.ShowDialog().Value == true)
                {
                    CheminFichier = string.Empty;
                }
            }
        }

        bool canExeclean()
        {
            if (string.IsNullOrEmpty(CheminFichier))
                return false;
            else return true;
        }


        #endregion


        #region Region export DB

        void canExportDb()
        {
           workerExpDb= new BackgroundWorker();
           ProgressBarDbVisibility = true;
            workerExpDb.WorkerReportsProgress = true;
            localview.progressbar.Maximum = DatabaseSelected.TablesDb.Count;
            int ikk = 0;
            IsBusyDb = true;
                        workerExpDb.DoWork += (o, args) =>
                        {
                            try
                            {
                                //nettoyage tables
                                ClassUtilsModel.BackUp_CleanupTables(societeCourante.IdSociete);
                                BtnDbexportEnable = false;
                                Helper.Export_DB(PathSaveAs, DatabaseSelected.TablesDb);

                                ikk++;
                                //workerExpDb.ReportProgress(ikk);
                            }

                            catch (Exception ex)
                            {
                              
                                args.Result = ex.Message;
                            }


                        };

                        //workerExpDb.ProgressChanged += (o, args) =>
                        //{

                        //    localview.ValueProgressBar = args.ProgressPercentage;
                        //    localview.LBInfos = args.ProgressPercentage.ToString() + "%";
                        //};

                        workerExpDb.RunWorkerCompleted += (o, args) =>
                                {
                                    if (args.Result != null)
                                    {
                                        CustomExceptionView view = new CustomExceptionView();
                                        view.Owner = System.Windows.Application.Current.MainWindow;
                                        view.Title = "ERREURE";
                                        view.ViewModel.Message = "Erreure Survenue lors de la création du Dump \n" + args.Result;
                                     
                                          ProgressBarDbVisibility  = false;
                                        BtnDbexportEnable = true;
                                        IsBusy = false;
                                        IsBusyDb = false;
                                        view.ShowDialog();
                                    }
                                    else
                                    {
                                        BtnDbexportEnable = true;
                                        ProgressBarDbVisibility = false;
                                        IsBusyDb = false;
                                        MessageBox.Show("Fin du traitement");
                                       
                                        try
                                        {
                                            ClassUtilsModel.BackUpBD_ADD(0, string.Format("{0}.{1}", UserConnected.Nom, UserConnected.Prenom), "Export", DatabaseSelected.DatabaseNAme, "", "");

                                           TableHistoricLog= ClassUtilsModel.BackUpBD_SELECT(0);
                                        }
                                        catch (Exception ed)
                                        {
                                          CustomExceptionView view = new CustomExceptionView();
                                        view.Owner = System.Windows.Application.Current.MainWindow;
                                        view.Title = " ERREURE LOG";
                                        view.ViewModel.Message = "Erreur :" + ed.Message;
                                        }

                                        ListeTableDB = null;
                                        DatabaseSelected = null;


                                    }
                                };

                        workerExpDb.RunWorkerAsync();

        }

        bool canExecuteExportDb()
        {
            bool values = true;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.ExportDB ||  CurrentDroit.Developpeur )
                {
                    if (string.IsNullOrEmpty(PathSaveAs))
                        values = false;
                    if (DatabaseSelected == null)
                        values = false;

                   
                }
            }
            return values;

          
           
           // return string.IsNullOrEmpty(PathSaveAs) == true ? false : (DatabaseSelected !=null ?true:false);
        }
        #endregion

        #region Region Import restore
        void canImportDb()
        {
            workerImportDb = new BackgroundWorker();
           ProgressBarIMportvisibility=true;
           IsImportBusy = true;
           IsImportCancel = true;
            workerImportDb.DoWork += (o, args) =>
              {
                  try
                  {
                      BtnImportenable = false;
                   
                      Helper.ImportDB(cmbDatabaseSelected.DatabaseNAme, PathImportDB);
                  }
                  catch (Exception ex)
                  {

                      args.Result = ex.Message;
                  }
              };

            workerImportDb.RunWorkerCompleted += (o, args) =>
             {
                 if (args.Result != null)
                 {
                     CustomExceptionView view = new CustomExceptionView();
                     view.Owner = System.Windows.Application.Current.MainWindow;
                     view.Title = "ERREURE";
                     view.ViewModel.Message = "Erreure Survenue lors de l'import \n" + args.Result;

                     ProgressBarIMportvisibility = false;
                     BtnDbexportEnable = true;
                     IsImportBusy = false;
                     BtnImportenable = true;
                     IsImportCancel = false;
                     view.ShowDialog();
                 }
                 else
                 {
                     BtnDbexportEnable = true;
                     ProgressBarIMportvisibility = false;
                     IsImportBusy = false;
                     BtnImportenable = true;
                     IsImportCancel = false;
                     MessageBox.Show("Fin du traitement");
                    
                     try
                     {
                         ClassUtilsModel.BackUpBD_ADD(0, string.Format("{0}.{1}", UserConnected.Nom, UserConnected.Prenom), "Import/ Restauration", cmbDatabaseSelected.DatabaseNAme,
                             PathImportDB.Substring((PathImportDB.LastIndexOf("\\"))), cmbDatabaseSelected.DatabaseNAme);
                       TableHistoricLog=  ClassUtilsModel.BackUpBD_SELECT(0);
                     }
                     catch (Exception ed)
                     {
                         CustomExceptionView view = new CustomExceptionView();
                         view.Owner = System.Windows.Application.Current.MainWindow;
                         view.Title = " ERREURE LOG";
                         view.ViewModel.Message = "Erreur :" + ed.Message;
                     }

                     ListeTableDB = null;
                     CmbDatabaseSelected = null;
                 }
             };

            workerImportDb.RunWorkerAsync();
            workerImportDb.WorkerSupportsCancellation = true;

        }

        bool canExecuteImportDb()
        {
            bool values = true;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.ImportDb  || CurrentDroit.Developpeur )
                {
                    if (cmbDatabaseSelected == null)
                        values= false;
                    if (string.IsNullOrEmpty(PathImportDB))
                        values= false;


                }
            }
            return values;

        }

        void canCancelImportDb()
        {
            workerImportDb.CancelAsync();
        }
        #endregion

        private void DoWork(object sender, DoWorkEventArgs e)
        {
            // do time-consuming work here, calling ReportProgress as and when you can
            for (int i = 0; i < 100; i += 5)
            {
                Thread.Sleep(100);
            }
        }

        private void ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            this.CurrentProgressExport  = e.ProgressPercentage;
        }

        void canClose()
        {
            Communicator ctr = new Communicator();
            ctr.contentVue = "admin";
            EventArgs e1 = new EventArgs();
            ctr.OnChangeText(e1);


            //_injectSingleViewService.ClearViewsFromRegion(RegionNames.ContentRegion);
            //UserAffiche uaffiche = _container.Resolve<UserAffiche>();


            //IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion,
            //                                     () => uaffiche);
        }

        #endregion

        #region METHODS Thread

        void MyCallbackFunction()
        {
            string messageResult = string.Empty;
            bool isAction = false;
            double maxProgressbar=0;
            BackgroundWorker worker = new BackgroundWorker();
            worker.WorkerReportsProgress = true;
            if (progress != null) progress.Close();
            progress = new ProgressWindow();
            ObservableCollection<FactureModel> newFactures = null;
            progress.ProgressBarMinimum = 0;
            

            Dictionary<FactureModel, List<LigneFactureModel>> listesFacture = new Dictionary<FactureModel, List<LigneFactureModel>>();
            LigneFactureModel ligneService = new LigneFactureModel();
            ProduitModel produitService = new ProduitModel();
            DetailProductModel detailService = new DetailProductModel();
            List<string> listeErrors = new List<string>();

            StringBuilder fileName = new StringBuilder();
            HashSet<int > objets = new HashSet<int>();
            HashSet<Int32> objetgenerics = new HashSet<int>();
            HashSet<Int32> exploitations = new HashSet<int>();
            HashSet<Int32> taxes = new HashSet<int>();
            HashSet<Int32> devises = new HashSet<int>();
            HashSet<Int32> statuts = new HashSet<int>();
            HashSet<Int32> clients = new HashSet<int>();
            HashSet<Int32> departements = new HashSet<int>();
            HashSet<Int32> utilisateursCreers = new HashSet<int>();
            HashSet<Int32> utilisateursModifs = new HashSet<int>();
            HashSet<Int32> profilesIds = new HashSet<int>();

            HashSet<Int32> comptesids = new HashSet<int>();
            HashSet<Int32> exonerationIds = new HashSet<int>();
            HashSet<Int32> deviseClientids = new HashSet<int>();
            HashSet<Int32> taxeProrataClientIds = new HashSet<int>();
            HashSet<Int32> termepaiementids = new HashSet<int>();
            HashSet<Int32> produitIds = new HashSet<int>();
            HashSet<Int32> detailProduitIds = new HashSet<int>();
            HashSet<Int32> langueIds = new HashSet<int>();



            List<ObjetFactureModel> listeObjets = new List<ObjetFactureModel>();
            List<ObjetGenericModel> listeObjetgenerics = new List<ObjetGenericModel>();
            List<ExploitationFactureModel> listeExploitataions = new List<ExploitationFactureModel>();
            List<TaxeModel> listeTaxes = new List<TaxeModel>();
            List<DeviseModel> listeDevises = new List<DeviseModel>();
            List<StatutModel> listeStatuts = new List<StatutModel>();
            List<ClientModel> listeClients = new List<ClientModel>();
            List<DepartementModel> listeDepartement = new List<DepartementModel>();
            List<UtilisateurModel> listeUtilisateurs = new List<UtilisateurModel>();

            List<CompteModel> listeCompte = new List<CompteModel>();
            List<ExonerationModel> listeExoneration = new List<ExonerationModel>();
            List<DeviseModel> listeDeviseClient = new List<DeviseModel>();
            List<TaxeModel> listeProrataClient = new List<TaxeModel>();
            List<LibelleTermeModel> listetermePaiement = new List<LibelleTermeModel>();
            List<LangueModel> listeLangue = new List<LangueModel>();
            List<ProfileModel> listeProfile = new List<ProfileModel>();

            List<ProduitModel> listeProduit = new List<ProduitModel>();
            List<DetailProductModel> listeDetailProd = new List<DetailProductModel>();

            // traitement à mettre dans un thread

            try
            {

                int i = 0;

                newFactures = new ObservableCollection<FactureModel>();
                progress.ProgressBarMaximum = newFactures.Count;

                this.IsBusy = true;
                progress.Show();


                if (InDateDebut.HasValue && InDatefin.HasValue)
                {
                  

                    IsProgressExportVisibled = true;
                    if (newFactures.Count > 0)
                    {
                      
                       
                        foreach (var facture in newFactures)
                        {
                            objets.Add(facture.IdObjetFacture);
                            exploitations.Add(facture.IdExploitation);
                            taxes.Add(facture.IdTaxe);
                            devises.Add(facture.IdDevise);
                            statuts.Add(facture.IdStatut);
                            clients.Add(facture.IdClient);

                            departements.Add(facture.IdDepartement);
                            utilisateursCreers.Add(facture.IdCreerpar);
                            utilisateursCreers.Add(facture.IdModifierPar);

                            List<LigneFactureModel> items = ligneService.LIGNE_FACTURE_BYIDFActure(facture.IdFacture);
                            if (items != null && items.Count > 0)
                                listesFacture.Add(facture, items);

                           
                            progress.LBInfos =i.ToString() + "%";
                           progress.ValueProgressBar =i ;
                            i++;
                        }


                        #region ELEMENT FACTURES



                        foreach (Int32 ob in departements)
                        {
                            var newobjet = newFactures.FirstOrDefault(ol => ol.IdDepartement == ob);
                            if (newobjet != null)
                            {
                                listeDepartement.Add(newobjet.CurrentDepartement);
                            }
                        }

                        foreach (Int32 ob in clients)
                        {
                            var newobjet = newFactures.FirstOrDefault(ol => ol.IdClient == ob);
                            if (newobjet != null)
                            {
                                listeClients.Add(newobjet.CurrentClient);
                                comptesids.Add(newobjet.CurrentClient.IdCompte);
                                exonerationIds.Add(newobjet.CurrentClient.IdExonere);
                                deviseClientids.Add(newobjet.CurrentClient.IdDeviseFact);
                                taxeProrataClientIds.Add(newobjet.CurrentClient.Idporata);
                                termepaiementids.Add(newobjet.CurrentClient.IdTerme);
                                langueIds.Add(newobjet.CurrentClient.IdLangue);
                            }
                        }
                        #region OBJET REF CLIENT
                        //

                        foreach (Int32 ob in langueIds)
                        {
                            var newobjet = listeClients.FirstOrDefault(l => l.IdLangue == ob);
                            if (newobjet != null)
                            {
                                listeLangue.Add(newobjet.Llangue);
                            }
                        }

                        foreach (Int32 ob in comptesids)
                        {
                            var newobjet = listeClients.FirstOrDefault(ol => ol.IdCompte == ob);
                            if (newobjet != null)
                            {
                                listeCompte.Add(newobjet.Compte);
                            }
                        }


                        foreach (Int32 ob in exonerationIds)
                        {
                            var newobjet = listeClients.FirstOrDefault(obj => obj.IdExonere == ob);
                            if (newobjet != null)
                            {
                                listeExoneration.Add(newobjet.Exonerere);
                            }
                        }

                        foreach (Int32 ob in deviseClientids)
                        {
                            var newobjet = listeClients.FirstOrDefault(obj => obj.IdDeviseFact == ob);
                            if (newobjet != null)
                            {
                                listeDeviseClient.Add(newobjet.DeviseConversion);
                            }
                        }

                        foreach (Int32 ob in taxeProrataClientIds)
                        {
                            var newobjet = listeClients.FirstOrDefault(obj => obj.Idporata == ob);
                            if (newobjet != null)
                            {
                                listeProrataClient.Add(newobjet.Porata);
                            }
                        }

                        foreach (Int32 ob in termepaiementids)
                        {
                            var newobjet = listeClients.FirstOrDefault(obj => obj.IdTerme == ob);
                            if (newobjet != null)
                            {
                                listetermePaiement.Add(newobjet.LibelleTerme);
                            }
                        }
                        #endregion

                        #region OBJET REF UTILISATEUR

                        foreach (Int32 ob in utilisateursCreers)
                        {
                            if (ob > 0)
                            {
                                var newobjet = newFactures.FirstOrDefault(obj => obj.IdCreerpar == ob);
                                if (newobjet != null)
                                {
                                    listeUtilisateurs.Add(newobjet.UserCreate);
                                    profilesIds.Add(newobjet.UserCreate.IdProfile);
                                }
                            }
                        }

                        foreach (Int32 ob in profilesIds)
                        {
                            var newobjet = listeUtilisateurs.FirstOrDefault(obj => obj.IdProfile == ob);
                            if (newobjet != null)
                            {
                                listeProfile.Add(newobjet.Profile);

                            }
                        }

                        #endregion



                        foreach (Int32 ob in statuts)
                        {
                            var newobjet = newFactures.FirstOrDefault(obj => obj.IdStatut == ob);
                            if (newobjet != null)
                            {
                                listeStatuts.Add(newobjet.CurrentStatut);
                            }
                        }

                        // traitement des objets
                        foreach (int ob in objets)
                        {
                            if (ob != 0)
                            {
                                FactureModel newobjet = newFactures.First(obj => obj.IdObjetFacture == ob);
                                //FactureModel  newobjet =newFactures.First (o => o.IdObjetFacture == ob);

                                if (newobjet != null)
                                {
                                    if (newobjet.CurrentObjetFacture != null)
                                    {
                                        ObjetFactureModel objFact = newobjet.CurrentObjetFacture;
                                        listeObjets.Add(objFact);
                                        listeObjetgenerics.Add(objFact.ObjetGeneric);
                                    }
                                }
                            }
                        }

                        foreach (Int32 ob in exploitations)
                        {
                            ExploitationFactureModel newobjet = newFactures.FirstOrDefault(obj => obj.IdExploitation == ob).CurrentExploitation;
                            if (newobjet != null)
                            {
                                listeExploitataions.Add(newobjet);
                            }
                        }
                        foreach (Int32 ob in taxes)
                        {
                            TaxeModel newobjet = newFactures.FirstOrDefault(obj => obj.IdTaxe == ob).CurrentTaxe;
                            if (newobjet != null)
                            {
                                listeTaxes.Add(newobjet);
                            }
                        }

                        foreach (Int32 ob in devises)
                        {
                            DeviseModel newobjet = newFactures.FirstOrDefault(obj => obj.IdDevise == ob).CurrentDevise;
                            if (newobjet != null)
                            {
                                listeDevises.Add(newobjet);
                            }
                        }

                        #endregion

                        #region ELEMENT ITEMS

                        if (listesFacture != null)
                        {
                            foreach (var lf in listesFacture)
                            {
                                List<LigneFactureModel> items = lf.Value;
                                foreach (LigneFactureModel item in items)
                                {
                                    produitIds.Add(item.IdProduit);
                                    detailProduitIds.Add(item.IdDetailProduit);

                                }
                            }
                        }

                        foreach (Int32 ob in detailProduitIds)
                        {
                            DetailProductModel newobjet = detailService.DETAIL_PRODUIT_GETBYID(ob);
                            if (newobjet != null)
                            {
                                listeDetailProd.Add(newobjet);
                                if (!listeProduit.Exists(po => po.IdProduit == newobjet.Produit.IdProduit))
                                    listeProduit.Add(newobjet.Produit);
                            }
                        }





                        #endregion
                    }

                   

                    if (newFactures.Count > 0)
                    {
                        fileName.Append(GlobalDatas.SIGLE);
                        fileName.Append("_");
                        fileName.Append(DateTime.Today.ToShortDateString().Replace("/", "").Trim());
                        fileName.Append("_");
                        fileName.Append(GlobalDatas.EXTRACT);
                        fileName.Append("_");
                        fileName.Append(societeCourante.IdSociete);
                        fileName.Append(InDateDebut.Value.ToShortDateString().Replace("/", "").Trim());
                        fileName.Append(InDatefin.Value.ToShortDateString().Replace("/", "").Trim());
                       
                        fileName.Append(".xml");

                        string chemin = CommonModule.GetLogPath(ParametersDatabase.PathLog);

                        using (FileStream stream = File.Create(chemin + "\\" + fileName.ToString()))
                        {
                            XmlWriterSettings settings = new XmlWriterSettings { Indent = true, Encoding = Encoding.UTF8 };

                            using (XmlWriter writer = XmlWriter.Create(stream, settings))
                            {
                                #region XML FILE
                                
                               
                                writer.WriteStartDocument();
                                writer.WriteStartElement("site");
                                //writer.WriteAttributeString("id", societeCourante.IdSociete.ToString());

                                writer.WriteStartElement("societe");
                                writer.WriteElementString("id", societeCourante.IdSociete.ToString());
                                writer.WriteElementString("operation", "new");
                                writer.WriteElementString("raisonSocial", societeCourante.RaisonSocial);
                                writer.WriteElementString("nomManager", societeCourante.NomManager);
                                writer.WriteElementString("titreManager", societeCourante.TitreManager);
                                writer.WriteElementString("numeroContribuable", societeCourante.NomManager);
                                writer.WriteElementString("numeroImmat", societeCourante.NumContribualbe);
                                writer.WriteElementString("pays", societeCourante.Pays);
                                writer.WriteElementString("ville", societeCourante.Ville);
                                writer.WriteElementString("adress1", societeCourante.Adesse_1);
                                writer.WriteElementString("adress2", societeCourante.Adresse_2);
                                writer.WriteElementString("boitepostal", societeCourante.BoitePostal);
                                writer.WriteElementString("telefone", societeCourante.Telephone);
                                writer.WriteElementString("faxe", societeCourante.Faxe);
                                writer.WriteElementString("sigle", societeCourante.SigleSite);
                                writer.WriteElementString("siteweb", societeCourante.SiteIntenet);
                                writer.WriteElementString("rc", societeCourante.Rc );
                                writer.WriteEndElement();


                                writer.WriteStartElement("langues");
                                foreach (LangueModel langue in listeLangue)
                                {
                                    writer.WriteStartElement("langue");
                                    writer.WriteElementString("id", langue.Id.ToString());
                                    writer.WriteElementString("libelle", langue.Libelle);
                                    writer.WriteElementString("Shortname", langue.Shortname);
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("profiles");
                                foreach (ProfileModel prof in listeProfile)
                                {
                                    writer.WriteStartElement("profile");
                                    writer.WriteElementString("id", prof.IdProfile.ToString());
                                    writer.WriteElementString("libelle", prof.Libelle);
                                    writer.WriteElementString("Shortname", prof.ShortName);
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("utilisateurs");
                                foreach (UtilisateurModel obj in listeUtilisateurs)
                                {
                                    writer.WriteStartElement("utilisateur");
                                    writer.WriteElementString("id", obj.Id.ToString());
                                    writer.WriteElementString("nom", obj.Nom);
                                    writer.WriteElementString("prenom", obj.Prenom);
                                    writer.WriteElementString("fonction", obj.Fonction);
                                    writer.WriteElementString("loggin", obj.Loggin);
                                    writer.WriteElementString("idprofile", obj.IdProfile.ToString());
                                    writer.WriteElementString("idSite", obj.IdSite .ToString());

                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("exonerations");
                                foreach (ExonerationModel obj in listeExoneration)
                                {
                                    writer.WriteStartElement("exoneration");
                                    writer.WriteElementString("id", obj.ID.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteElementString("Shortname", obj.CourtDesc);
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("comptes");
                                foreach (CompteModel obj in listeCompte)
                                {
                                    writer.WriteStartElement("compte");
                                    writer.WriteElementString("id", obj.ID.ToString());
                                    writer.WriteElementString("idSite", obj.IDSite.ToString());
                                    writer.WriteElementString("numeroCompte", obj.NumeroCompte);
                                    writer.WriteElementString("nomBanque", obj.NomBanque);
                                    writer.WriteElementString("ville", obj.Ville);
                                    writer.WriteElementString("agence", obj.Agence);
                                    writer.WriteElementString("rue", obj.Rue);
                                    writer.WriteElementString("telefone", obj.Telephone);
                                    writer.WriteElementString("pays", obj.Pays);
                                    writer.WriteElementString("quartier", obj.Quartier);
                                    writer.WriteElementString("boitepostal", obj.BoitePostal );
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("deviseClients");
                                foreach (DeviseModel obj in listeDeviseClient)
                                {
                                    writer.WriteStartElement("deviseCli");
                                    writer.WriteElementString("id", obj.ID_Devise.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteElementString("taux", obj.Taux);
                                    writer.WriteElementString("idSite", obj.IdSite.ToString () );
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("termePaiement");
                                foreach (LibelleTermeModel obj in listetermePaiement)
                                {
                                    writer.WriteStartElement("terme");
                                    writer.WriteElementString("id", obj.ID.ToString());
                                    writer.WriteElementString("description", obj.Desciption);
                                    writer.WriteElementString("shortDesc", obj.CourtDescription);
                                    writer.WriteElementString("jour", obj.Jour.ToString());
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("taxesClient");
                                foreach (TaxeModel obj in listeProrataClient)
                                {
                                    writer.WriteStartElement("taxeCli");
                                    writer.WriteElementString("id", obj.ID_Taxe.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteElementString("taux", obj.Taux);
                                    writer.WriteElementString("idSite", obj.IdSite.ToString());

                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("clients");
                                foreach (ClientModel obj in listeClients)
                                {
                                    writer.WriteStartElement("client");
                                    writer.WriteElementString("id", obj.IdClient.ToString());
                                    writer.WriteElementString("idSite", obj.IdSite.ToString());
                                    writer.WriteElementString("idLangue", obj.IdLangue .ToString());
                                    writer.WriteElementString("idExoneration", obj.IdExonere.ToString());
                                    writer.WriteElementString("idCompte", obj.IdCompte.ToString());
                                    writer.WriteElementString("idDevise", obj.IdDeviseFact.ToString());
                                    writer.WriteElementString("idTerme", obj.IdTerme.ToString());
                                    writer.WriteElementString("idTaxeProrata", obj.Idporata.ToString());
                                    writer.WriteElementString("nomClient", obj.NomClient);
                                    writer.WriteElementString("numContrib", obj.NumeroContribuable);
                                    writer.WriteElementString("numImmat", obj.NumemroImat);
                                    writer.WriteElementString("ville", obj.Ville);
                                    writer.WriteElementString("rue1", obj.Rue1);
                                    writer.WriteElementString("rue2", obj.Rue2);
                                    writer.WriteElementString("boitePostal", obj.BoitePostal );
                                    writer.WriteElementString("termeJour", obj.TermeNombre.ToString());
                                    writer.WriteElementString("termeDesc", obj.TermeDescription);
                                    writer.WriteElementString("dateEcheance", obj.DateEcheance);
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("objetsgenerics");
                                foreach (ObjetGenericModel  obj in listeObjetgenerics )
                                {
                                    if (obj != null)
                                    {
                                        writer.WriteStartElement("objetgeneric");
                                        writer.WriteElementString("id", obj.IdObjetg.ToString());
                                        writer.WriteElementString("idLangue", obj.IdLangue.ToString());
                                        writer.WriteElementString("idSite", obj.IdSite.ToString());
                                        writer.WriteElementString("libelle", obj.Libelle);
                                        writer.WriteEndElement();
                                    }
                                   
                                }
                                writer.WriteEndElement();


                                writer.WriteStartElement("objetsfactures");
                                foreach (ObjetFactureModel obj in listeObjets)
                                {
                                    if (obj != null)
                                    {
                                        writer.WriteStartElement("objetfacture");
                                        writer.WriteElementString("id", obj.IdObjet.ToString());
                                        writer.WriteElementString("idObjetGen", obj.IdobjetGen.ToString());
                                        writer.WriteElementString("idClient", obj.IdClient.ToString());
                                        writer.WriteElementString("libelle", obj.Libelle);
                                        writer.WriteEndElement();
                                    }
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("exploitations");
                                foreach (ExploitationFactureModel obj in listeExploitataions)
                                {
                                    writer.WriteStartElement("exploitation");
                                    writer.WriteElementString("id", obj.IdExploitation.ToString());
                                    writer.WriteElementString("idlangue", obj.IdLangue.ToString());
                                    writer.WriteElementString("idsite", obj.IdSite.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("taxes");
                                foreach (TaxeModel obj in listeTaxes)
                                {
                                    writer.WriteStartElement("taxe");
                                    writer.WriteElementString("id", obj.ID_Taxe.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteElementString("taux", obj.Taux);
                                    writer.WriteElementString("idSite", obj.IdSite.ToString());
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("devises");
                                foreach (DeviseModel obj in listeDevises)
                                {
                                    writer.WriteStartElement("devise");
                                    writer.WriteElementString("id", obj.ID_Devise.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteElementString("taux", obj.Taux);
                                    writer.WriteElementString("idSite", obj.IdSite.ToString());
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("statuts");
                                foreach (StatutModel obj in listeStatuts)
                                {
                                    writer.WriteStartElement("statut");
                                    writer.WriteElementString("id", obj.IdStatut.ToString());
                                    writer.WriteElementString("idlangue", obj.IdLangue.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteElementString("shortDesc", obj.CourtDesc);
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("departements");
                                foreach (DepartementModel obj in listeDepartement)
                                {
                                    writer.WriteStartElement("departement");
                                    writer.WriteElementString("id", obj.IdDep.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteElementString("shortDesc", obj.CourtLibelle);
                                    writer.WriteElementString("autre", obj.Autre);
                                    writer.WriteElementString("idSite", obj.IdSite.ToString());
                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("produits");
                                foreach (ProduitModel obj in listeProduit)
                                {
                                    writer.WriteStartElement("produit");
                                    writer.WriteElementString("id", obj.IdProduit.ToString());
                                    writer.WriteElementString("idSite", obj.IdSite.ToString());
                                    writer.WriteElementString("idLangue", obj.IdLangue.ToString());
                                    writer.WriteElementString("libelle", obj.Libelle);
                                    writer.WriteElementString("prixunitaire", obj.PrixUnitaire .ToString ());
                                    writer.WriteElementString("codeOhada", obj.CompteOhada );

                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("detailProduits");
                                foreach (DetailProductModel obj in listeDetailProd)
                                {
                                    writer.WriteStartElement("detailProduit");
                                    writer.WriteElementString("id", obj.IdDetail.ToString());
                                    writer.WriteElementString("idProduit", obj.IdProduit.ToString());
                                    writer.WriteElementString("idClient", obj.IdClient.ToString());
                                    writer.WriteElementString("quantite", obj.Quantite.ToString());
                                    writer.WriteElementString("prixunitaire", obj.Prixunitaire.ToString());
                                    writer.WriteElementString("isExonere", obj.Exonerer.ToString());
                                    writer.WriteElementString("isprorata", obj.Isprorata.ToString());
                                    writer.WriteElementString("idSite", obj.IdSite.ToString());
                                    writer.WriteElementString("idExploitation", obj.IdExploitation .ToString());
                                    writer.WriteElementString("specialfact", obj.Specialfact.ToString());

                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();

                                writer.WriteStartElement("factures");
                                foreach (var fact in listesFacture)
                                {
                                    FactureModel newFacture = fact.Key;
                                    List<LigneFactureModel> items = fact.Value;

                                    writer.WriteStartElement("facture");
                                    writer.WriteElementString("id", newFacture.IdFacture.ToString());
                                    writer.WriteElementString("idObjet", newFacture.IdObjetFacture.ToString());
                                    writer.WriteElementString("idExploitation", newFacture.IdExploitation.ToString());
                                    writer.WriteElementString("idClient", newFacture.IdClient.ToString());
                                    writer.WriteElementString("idTaxe", newFacture.IdTaxe.ToString());
                                    writer.WriteElementString("idDevise", newFacture.IdDevise.ToString());
                                    writer.WriteElementString("idStatut", newFacture.IdStatut.ToString());
                                    writer.WriteElementString("idUserCreate", newFacture.IdCreerpar.ToString());
                                    writer.WriteElementString("idUserMaj", newFacture.IdModifierPar.ToString());
                                    writer.WriteElementString("idDepartement", newFacture.IdDepartement.ToString());
                                    writer.WriteElementString("idSite", newFacture.IdSite.ToString());
                                    writer.WriteElementString("idMode", newFacture.IdModePaiement.ToString());

                                    writer.WriteElementString("numerofacture", newFacture.NumeroFacture);
                                    writer.WriteElementString("moisPrestation", newFacture.MoisPrestation.Value.ToString());
                                    writer.WriteElementString("dateCloture", newFacture.DateCloture.ToString());
                                    writer.WriteElementString("dateEcheance", newFacture.DateEcheance.ToString());
                                    writer.WriteElementString("datecreation", newFacture.DateCreation.ToString());
                                    writer.WriteElementString("dateSortie", newFacture.DateSortie.ToString());
                                    writer.WriteElementString("dateSuspension", newFacture.DateSuspension.ToString());
                                    writer.WriteElementString("dateDepot", newFacture.DateDepot.ToString());
                                    writer.WriteElementString("datenonValable", newFacture.DateNonValable.ToString());
                                    writer.WriteElementString("datepaiement", newFacture.DatePaiement.ToString());
                                    writer.WriteElementString("dateFacture", newFacture.DateFacture.ToString());//
                                    writer.WriteElementString("dateModif", newFacture.DateModif.ToString());//

                                    writer.WriteElementString("jourFin", newFacture.JourFinEcheance.ToString());
                                    writer.WriteElementString("labelObjet", newFacture.Label_objet);
                                    writer.WriteElementString("labeldept", newFacture.Label_Dep);
                                    writer.WriteElementString("ttc", newFacture.TotalTTC.ToString());
                                  


                                    if (items != null && items.Count > 0)
                                    {
                                        writer.WriteStartElement("lignefactures");

                                        foreach (LigneFactureModel item in items)
                                        {

                                            writer.WriteStartElement("lignefacture");
                                            writer.WriteElementString("id", item.IdLigneFacture.ToString());
                                            writer.WriteElementString("idFacture", item.IdFacture.ToString());
                                            writer.WriteElementString("idproduit", item.IdProduit.ToString());
                                            writer.WriteElementString("iddetailProd", item.IdDetailProduit.ToString());
                                            writer.WriteElementString("description", item.Description);
                                            writer.WriteElementString("quantite", item.Quantite.ToString());
                                            writer.WriteElementString("prixu", item.PrixUnitaire.ToString());
                                            writer.WriteElementString("idSite", item.IdSite.ToString());
                                            writer.WriteElementString("datecreation", item.Datecreate.ToString());//
                                            writer.WriteElementString("dateModif", item.DateModif.ToString());//
                                            writer.WriteElementString("idUserCreate", item.IdUtilisateur.ToString());//
                                            writer.WriteElementString("idUserModif", item.IdUtilUpdate.ToString());//
                                            writer.WriteElementString("montantHt", item.MontanHT.ToString());//
                                            writer.WriteElementString("isModif", item.IsDelete.ToString());//

                                            writer.WriteEndElement();

                                        }

                                        writer.WriteEndElement();
                                    }


                                    writer.WriteEndElement();
                                }
                                writer.WriteEndElement();


                                writer.WriteEndElement();



                                writer.WriteEndDocument();

                                #endregion

                                isAction = true;
                            }

                        }

                        string periode=string.Format("{0}-{1}", InDateDebut.Value.ToShortDateString(), InDatefin.Value.ToShortDateString());
                        LogExportIportModel.Extraction_ADD(0, DateTime.Today, DateTime.Today, DateTime.Today, periode, societeCourante.IdSociete, 0, "Fichier Extrait", 1, fileName.ToString().Trim());
                      
                        MessageFinal = "Fin traitement";
                        messageResult = "Extraction réalisée avec succes";
                        IsProgressExportVisibled = false;
                        InDateDebut=null ;
                        InDatefin = null;

                        progress.Close();
                        GetLogFiles();
                        canRefreshDirectory();
                        MessageFinal = messageResult;
                        this.MouseCursor = null;
                        this.IsBusy = false;
                    }
                    else
                    {

                        MessageBox.Show(" Pas de Données pour cette période");
                        messageResult = string.Empty;
                    }

                    }
              
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION EXTRACTION FICHIER";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                //    return;
                this.MouseCursor = null;
                this.IsBusy = false;
                MessageFinal = "Erreur dans l'exécution du traitement";
            }

            //};
            //worker.ProgressChanged += (o, args) =>
            //{
            //    progress.LBInfos = args.ProgressPercentage.ToString ()+"%";
            //    progress.ValueProgressBar =args.ProgressPercentage ;

            //};
            
            
            //worker.RunWorkerCompleted += (o, args) =>
            //{
            //    if (args .Result != null)
            //    {
            //        CustomExceptionView view = new CustomExceptionView();
            //        view.Owner = Application.Current.MainWindow;
            //        view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
            //        view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
            //        view.ShowDialog();
            //        MessageFinal = "Erreur dans l'exécution du traitement";
            //        this.MouseCursor = null;
            //        this.IsBusy = false;
            //    }
            //    else
            //    {
            //        progress.Close();
            //        if (isAction)
            //        {
            //            GetLogFiles();
            //            canRefreshDirectory();
            //        }
            //        MessageFinal = messageResult;
            //        this.MouseCursor = null;
            //        this.IsBusy = false;
                    
            //    }
            //};


            //worker.RunWorkerAsync();


        }
        #endregion

        void GetLogFiles()
        {
            try
            {
                DataTable table = LogExportIportModel.Extraction_Select (societeCourante.IdSociete);
                if (table != null && table.Rows.Count > 0)
                {
                    NewLogFile log = null;
                    List<NewLogFile> newListeLog = new List<NewLogFile>();

                    foreach (DataRow row in table.Rows)
                    {
                        log = new NewLogFile();
                        log.id = int.Parse(row["id"].ToString());
                        log.DateExtraction = row["DateExtraction"] != DBNull.Value ? DateTime.Parse(row["DateExtraction"].ToString()) : (DateTime?)null;
                        log.periode = row["Periode_facture"].ToString();
                        log.DateImport = row["DateImport"] != DBNull.Value ? DateTime.Parse(row["DateImport"].ToString()) : (DateTime?)null;
                        log.DateExtract_valider = row["DateExtract_valider"] != DBNull.Value ? DateTime.Parse(row["DateExtract_valider"].ToString()) : (DateTime?)null;
                        log.idSite = Int32.Parse(row["Site_Ops"].ToString());
                        log.statut = row["Statut"].ToString();
                        log.nomfichier = row["nomFichier"].ToString();
                        log.idStatut = Int32.Parse(row["idStatut"].ToString());
                        log.idSiteMaj = row["SiteOpsMaj"] != DBNull.Value ? int.Parse(row["SiteOpsMaj"].ToString()) : 0;
                        if (log.idSite == societeCourante.IdSociete)
                            log.siteorigine = societeCourante.Ville;

                        if (log.idSiteMaj > 0)
                        {
                            SocieteModel ste = societeService.Get_SOCIETE_BYID(log.idSiteMaj);
                            if (ste != null)
                                log.siteMaj = ste.Ville;
                        }

                        newListeLog.Add(log);

                    }
                    ListeLog = newListeLog;

                }
                else ListeLog = null;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION DONNEES IMPORT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void GetLogFileImportist()
        {

            try
            {
                logFileListe log = null;
                List<logFileListe> newListeLog = new List<logFileListe>();
                ListeLog = null;
                NobreEnregistrement = string.Empty;
                DataTable table = LogExportIportModel.GetLogDataImport_Select(societeCourante.IdSociete);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        log = new logFileListe();

                        log.id = row["id"] != DBNull.Value ? int.Parse(row["id"].ToString()) : 0;
                        log.dateOps = row["DateOperation"] != DBNull.Value ? DateTime.Parse(row["DateOperation"].ToString()) : (DateTime?)null;
                        log.periode = row["PeriodeFacture"].ToString();
                        log.siteOps = row["Site_ops"].ToString();
                        log.statut = row["Statut"].ToString();
                        log.ville = row["Ville"].ToString();
                        log.dateValide = row["DateValidation"] != DBNull.Value ? DateTime.Parse(row["DateValidation"].ToString()) : (DateTime?)null;
                        log.nomfichier = row["nomFichier"] != DBNull.Value ? row["nomFichier"].ToString() : string.Empty;
                        newListeLog.Add(log);
                    }
                    logImortList = new List<logFileListe>();
                    logImortList = newListeLog;
                   // NobreEnregistrement = newListeLog.Count.ToString();
                }

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION DONNEES IMPORT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }

            //
        }

        void GetLogFileExportList()
        {
            try
            {
                logFileListe log = null;
                List<logFileListe> newListeLog = new List<logFileListe>();
                ListeLog = null;
                NobreEnregistrement = string.Empty;
                DataTable table = LogExportIportModel.GetLogDataExport_Select(societeCourante.IdSociete);
                if (table != null && table.Rows.Count > 0)
                {
                    foreach (DataRow row in table.Rows)
                    {
                        log = new logFileListe();

                        log.id = row["id"] != DBNull.Value ? int.Parse(row["id"].ToString()) : 0;
                        log.dateOps = row["DateOperation"] != DBNull.Value ? DateTime.Parse(row["DateOperation"].ToString()) : (DateTime?)null;
                        log.periode = row["PeriodeFacture"].ToString();
                        log.siteOps = row["Site_ops"].ToString();
                        log.statut = row["Statut"].ToString();
                        log.ville = row["Ville"].ToString();
                        log.dateValide = row["DateValidation"] != DBNull.Value ? DateTime.Parse(row["DateValidation"].ToString()) : (DateTime?)null;
                        log.nomfichier = row["nomFichier"] != DBNull.Value ? row["nomFichier"].ToString() : string.Empty;
                        newListeLog.Add(log);
                    }
                    logExportList = new List<logFileListe>();
                    logExportList = newListeLog;

                   // ListeLog = newListeLog;
                    //NobreEnregistrement = newListeLog.Count.ToString();
                }

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION DONNEES EXPORT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }
        
    }

    public class LignesFichiers
    {
        public bool IsClick { get; set; }
        public  string Nomfichier { get; set; }
        public string dateFichiers { get; set; }
        public string url { get; set; }
        public string adresse { get; set; }
        public string periode { get; set; }
    }

    public class logFileListe
    {
        public int id { get; set; }
        public DateTime? dateOps { get; set; }
        public DateTime? dateValide { get; set; }
        public string periode { get; set; }
        public string ville { get; set; }
        public string siteOps { get; set; }
        public string statut { get; set; }
        public string nomfichier { get; set; }


    }

    public class NewLogFile
    {
        public int id { get; set; }
        public DateTime? DateExtraction { get; set; }
        public DateTime? DateImport { get; set; }
        public DateTime? DateExtract_valider { get; set; }
        public string periode { get; set; }
        public string statut { get; set; }
        public Int32  idSite { get; set; }
        public Int32 idStatut { get; set; }
        public Int32 idSiteMaj { get; set; }
        public string nomfichier { get; set; }
        public string siteorigine { get; set; }
        public string siteMaj { get; set; }

    }
}
