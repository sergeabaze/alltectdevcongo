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
using System.Xml.Linq;
using AllTech.FrameWork.Global;
using AllTech.FrameWork.Model;
using System.Data;
using AllTech.FrameWork.Utils;

namespace AllTech.FacturationModule.ViewModel
{
    public class ParametersViewModel : ViewModelBase
    {

        #region FIELDS
        
      
        private readonly IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
       // private readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand saveArchiveCommand;
        private RelayCommand addDeviseCommand;
        private RelayCommand addTvaCommand;
        private RelayCommand addPasswordCommand;
        private RelayCommand addPaginationCommand;
        private RelayCommand closeCommand;
        private RelayCommand addPathCommand;
        private RelayCommand addPathBackLockCommand;

        private RelayCommand addmailFromCommand;
        private RelayCommand addMailToCommand;
        private RelayCommand addSmtpCommand;
        private RelayCommand addpathLogFilepCommand;
        private RelayCommand addPortSmtpCommand;

        private RelayCommand addLogginStmpCommand;
        private RelayCommand addPassWordSmtpCommand;
        private RelayCommand addDefaultLanguageCommand;
        private RelayCommand addDefaultJourLimiteCommand;
        private RelayCommand addDefaulExportDBCommand;
       

        private XElement parameter;

        ParametresModel _parametresService;
        ParametresModel _currentParametres;

        DeviseModel deviseservice;
        DeviseModel deviseOld;

     
        DeviseModel _deviseCurrent;
        List<DeviseModel> _deviseList;


      

        TaxeModel taxeService;
        TaxeModel taxeOld;

       
        TaxeModel _taxeSelected;
        List<TaxeModel> _taxeList;

        currentdevisesTaxe _oldtaxesDevise;

        LocalTaxe _taxeLocal;
        LocalDevise _deviseLocal;
        DefaulTpass defaulPasse;

        DroitModel droitFormulaire=null ;
        UtilisateurModel userConnected;

        bool connexionEnabled;
        SocieteModel societeCourante;
        SettingsModel settingService;
        List<SettingsModel> listeconfigurations;

        bool isConnected;

        List<DisplayLangues> displaylangues;
        DisplayLangues displaylangueSelect;
        int indexDevise;
        int indexTaxe;
        int indexLangue;
        private int indexLimiteJour;
        List<JourLimite> listeJourLimite;
        JourLimite jourlimiteSelect;
        int? intLimiteJourfacturation;
        bool enableLimitJourFacturation;

      
     
        bool visibleParemetres;
        #endregion

        public ParametersViewModel()
        {
            //_regionManager = regionManager;
            //_container = container;
            _currentParametres = new ParametresModel();
            CurrentParametres = GlobalDatas.dataBasparameter;
            societeCourante = GlobalDatas.DefaultCompany;
            ProgressBarVisibility = false;
            IsConnected = false;
            _oldtaxesDevise = new currentdevisesTaxe();
            //_injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
            settingService = new SettingsModel();
            _parametresService = new ParametresModel();

            if (CurrentParametres.Dejautiliser == "0")
            {
                connexionEnabled = true;
                parameter = XElement.Parse(Utils.GetParameterFiles().ToString());
                IsConnected = false;
                VisibleParemetres = false;
            }
            else
            {
                

                if (GlobalDatas.isConnectionOk)
                {
                    IsConnected = true;

                    userConnected = GlobalDatas.currentUser;
                    VisibleParemetres = true;
                    if (userConnected != null)
                    {
                        deviseservice = new DeviseModel();
                        taxeService = new TaxeModel();

                        droitFormulaire = userConnected.Profile.Droit.Find(p => p.IdVues == 7) ?? userConnected.Profile.Droit.Find(p => p.LibelleVue.ToLower().Contains("parameters"));
                        ListeJourLimite = GetJourList;

                        loadParameters();

                        loadRight();

                     
                        IndexLimiteJour = -1;
                    }
                    else
                    {
                        connexionEnabled = true;
                        parameter = XElement.Parse(Utils.GetParameterFiles().ToString());
                    }
                }
                else
                {
                    connexionEnabled = true;
                    parameter = XElement.Parse(Utils.GetParameterFiles().ToString());
                }

            }

           // Utils.logUserActions("Ouverture interface Parametres ", "");
        }

        #region PROPERTIES

        public bool EnableLimitJourFacturation
        {
            get { return enableLimitJourFacturation; }
            set { enableLimitJourFacturation = value;
            this.OnPropertyChanged("EnableLimitJourFacturation");
            }
        }

        public int? IntLimiteJourfacturation
        {
            get { return intLimiteJourfacturation; }
            set
            {
                intLimiteJourfacturation = value;
            if (value.HasValue)
            {
                if (value.Value >= 0 && value.Value <= 31)
                {
                    CurrentParametres.JourLimiteFacturation = value.Value;
                   
                }
                else
                    MessageBox.Show("Vous devez saisire une valeure entre [1-31]");
            }
            this.OnPropertyChanged("IntLimiteJourfacturation");
            }
        }

        public int IndexLimiteJour
        {
            get { return indexLimiteJour; }
            set
            {
                indexLimiteJour = value;
                this.OnPropertyChanged("IndexLimiteJour");
            }
        }

        public JourLimite JourlimiteSelect
        {
            get { return jourlimiteSelect; }
            set
            {
                jourlimiteSelect = value;
                if (value != null)
                    CurrentParametres.JourLimiteFacturation = value.NbreJour;
                this.OnPropertyChanged("JourlimiteSelect");
            }
        }
        public List<JourLimite> ListeJourLimite
        {
            get { return listeJourLimite; }
            set
            {
                listeJourLimite = value;
                this.OnPropertyChanged("ListeJourLimite");
            }
        }

        public bool VisibleParemetres
        {
            get { return visibleParemetres; }
            set
            {
                visibleParemetres = value;
                this.OnPropertyChanged("VisibleParemetres");
            }

        }
        public int IndexLangue
        {
            get { return indexLangue; }
            set
            {
                indexLangue = value;
                this.OnPropertyChanged("IndexLangue");
            }
        }
        public List<DisplayLangues> Displaylangues
        {
            get { return displaylangues; }
            set
            {
                displaylangues = value;
                this.OnPropertyChanged("Displaylangues");
            }
        }
        public int IndexTaxe
        {
            get { return indexTaxe; }
            set
            {
                indexTaxe = value;
                this.OnPropertyChanged("IndexTaxe");
            }
        }
        public int IndexDevise
        {
            get { return indexDevise; }
            set
            {
                indexDevise = value;
                this.OnPropertyChanged("IndexDevise");
            }
        }

        public DisplayLangues DisplaylangueSelect
        {
            get { return displaylangueSelect; }
            set
            {
                displaylangueSelect = value;
                if (value != null)
                {
                    if (!value.ID.Contains("en") && !value.ID.Contains("fr"))
                    {
                        MessageBox.Show("Langues non disponible");
                        value = null;
                    }
                }
                this.OnPropertyChanged("DisplaylangueSelect");
            }
        }
        public bool IsConnected
        {
            get { return isConnected; }
            set { isConnected = value;
               this.OnPropertyChanged("IsConnected");
            }
        }

        public List<SettingsModel> Listeconfigurations
        {
            get { return listeconfigurations; }
            set { listeconfigurations = value;
                
            this.OnPropertyChanged("Listeconfigurations");
            }
        }

        public bool ConnexionEnabled
        {
            get { return connexionEnabled; }
            set { connexionEnabled = value;
            this.OnPropertyChanged("ConnexionEnabled");
            }
        }

        public DefaulTpass DefaulPasse
        {
            get { return defaulPasse; }
            set { defaulPasse = value;
            this.OnPropertyChanged("DefaulPasse");
            }

        } 
        public TaxeModel TaxeOld
        {
            get { return taxeOld; }
            set { taxeOld = value;
            this.OnPropertyChanged("TaxeOld");
            }
        }

        public DeviseModel DeviseOld
        {
            get { return deviseOld; }
            set { deviseOld = value;
            this.OnPropertyChanged("DeviseOld");
            }
        }

        public XElement Parameter
        {
            get { return parameter; }
            set { parameter = value;
            this.OnPropertyChanged("Parameter");
            }
        }

        public ParametresModel CurrentParametres
        {
            get { return _currentParametres; }
            set { _currentParametres = value;
            this.OnPropertyChanged("CurrentParametres");
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
                // if (value != null || value != string.Empty)
                // filter(value);

                this.OnPropertyChanged("Filtertexte");
            }
        }

        public DeviseModel DeviseCurrent
        {
            get { return _deviseCurrent; }
            set { _deviseCurrent = value ;
            this.OnPropertyChanged("DeviseCurrent");
            }
        }

        public List<DeviseModel> DeviseList
        {
            get { return _deviseList; }
            set { _deviseList = value;
            this.OnPropertyChanged("DeviseList");
            }
        }

        public TaxeModel TaxeSelected
        {
            get { return _taxeSelected; }
            set { _taxeSelected = value ;
            this.OnPropertyChanged("TaxeSelected");
            }
        }


        public List<TaxeModel> TaxeList
        {
            get { return _taxeList; }
            set { _taxeList = value;
            this.OnPropertyChanged("TaxeList");
            }
        }


        public currentdevisesTaxe OldtaxesDevise
        {
            get { return _oldtaxesDevise; }
            set { _oldtaxesDevise = value;
            this.OnPropertyChanged("OldtaxesDevise");
            }
        }

        public LocalTaxe TaxeLocal
        {
            get { return _taxeLocal; }
            set { _taxeLocal = value;
            this.OnPropertyChanged("TaxeLocal");
            }
        }


        public LocalDevise DeviseLocal
        {
            get { return _deviseLocal; }
            set { _deviseLocal = value;
            this.OnPropertyChanged("DeviseLocal");
            }
        }
        #endregion

        #region ICOMMAND

        //addDefaultJourLimiteCommand
        public ICommand AddDefaultJourLimiteCommand
        {
            get
            {
                if (this.addDefaultJourLimiteCommand == null)
                {
                    this.addDefaultJourLimiteCommand = new RelayCommand(param => this.canSaveJourLimite(), param => this.canExecuteSaveJourLimite());
                }
                return this.addDefaultJourLimiteCommand;
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

        public ICommand SaveArchiveCommand
        {
            get
            {
                if (this.saveArchiveCommand == null)
                {
                    this.saveArchiveCommand = new RelayCommand(param => this.canSaveArchive(), param => this.canExecuteSaveArchive());
                }
                return this.saveArchiveCommand;
            }
        }

        public ICommand AddTvaCommand
        {
            get
            {
                if (this.addTvaCommand == null)
                {
                    this.addTvaCommand = new RelayCommand(param => this.canAddTva(), param => this.canExecuteTva());
                }
                return this.addTvaCommand;
            }


        }

        public ICommand AddDeviseCommand
        {
            get
            {
                if (this.addDeviseCommand == null)
                {
                    this.addDeviseCommand = new RelayCommand(param => this.canaddDevise(), param => this.canExecuteDevise());
                }
                return this.addDeviseCommand;
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

        public RelayCommand AddPasswordCommand
        {
            get
            {
                if (this.addPasswordCommand == null)
                {
                    this.addPasswordCommand = new RelayCommand(param => this.canAddPassword(), param => this.canExecutePassword());
                }
                return this.addPasswordCommand;
            }

        }

        //

        public RelayCommand AddPaginationCommand
        {
            get
            {
                if (this.addPaginationCommand == null)
                {
                    this.addPaginationCommand = new RelayCommand(param => this.canAddPagination(), param => this.canExecutePagination());
                }
                return this.addPaginationCommand;
            }

        }

        public RelayCommand AddPathCommand
        {
            get
            {
                if (this.addPathCommand == null)
                {
                    this.addPathCommand = new RelayCommand(param => this.canAddPath(), param => this.canExecutePath());
                }
                return this.addPathCommand;
            }

        }

        public RelayCommand AddPathBackLockCommand
        {
            get
            {
                if (this.addPathBackLockCommand == null)
                {
                    this.addPathBackLockCommand = new RelayCommand(param => this.canAddPathBAckUp(), param => this.canExecutePathBackUp());
                }
                return this.addPathBackLockCommand;
            }


        }

        public RelayCommand AddmailFromCommand
        {
            get
            {
                if (this.addmailFromCommand == null)
                {
                    this.addmailFromCommand = new RelayCommand(param => this.canAddMailFrom());
                }
                return this.addmailFromCommand;
            }

        }

        public RelayCommand AddSmtpCommand
        {
            get
            {
                if (this.addSmtpCommand == null)
                {
                    this.addSmtpCommand = new RelayCommand(param => this.canAddStmp());
                }
                return this.addSmtpCommand;
            }

        }

        public RelayCommand AddMailToCommand
        {
            get
            {
                if (this.addMailToCommand == null)
                {
                    this.addMailToCommand = new RelayCommand(param => this.canAddMailTo());
                }
                return this.addMailToCommand;
            }

        }

        public RelayCommand AddPortSmtpCommand
        {
            get
            {
                if (this.addPortSmtpCommand == null)
                {
                    this.addPortSmtpCommand = new RelayCommand(param => this.canAddPort());
                }
                return this.addPortSmtpCommand;
            }

        }

        //private RelayCommand addLogginStmpCommand;
        //private RelayCommand addPassWordSmtpCommand;

        public RelayCommand AddLogginStmpCommand
        {
            get
            {
                if (this.addLogginStmpCommand == null)
                {
                    this.addLogginStmpCommand = new RelayCommand(param => this.canAddLogginStmp());
                }
                return this.addLogginStmpCommand;
            }

        }

        public RelayCommand AddPassWordSmtpCommand
        {
            get
            {
                if (this.addPassWordSmtpCommand == null)
                {
                    this.addPassWordSmtpCommand = new RelayCommand(param => this.canAddPasswordStmp());
                }
                return this.addPassWordSmtpCommand;
            }

        }

        public RelayCommand AddpathLogFilepCommand
        {
            get
            {
                if (this.addpathLogFilepCommand == null)
                {
                    this.addpathLogFilepCommand = new RelayCommand(param => this.canAddPahFileLog(), param => this.canExecutePathLog());
                }
                return this.addpathLogFilepCommand;
            }

        }


        public RelayCommand AddDefaultLanguageCommand
        {
            get
            {
                if (this.addDefaultLanguageCommand == null)
                {
                    this.addDefaultLanguageCommand = new RelayCommand(param => this.canAddLanguage(), param => this.canExecuteLanguage());
                }
                return this.addDefaultLanguageCommand;
            }

        }

        public RelayCommand AddDefaulExportDBCommand
        {
            get
            {
                if (this.addDefaulExportDBCommand == null)
                {
                    this.addDefaulExportDBCommand = new RelayCommand(param => this.canAddexportDbName(), param => this.canExecuteExportDbName());
                }
                return this.addDefaulExportDBCommand;
            }

        }


        //   private RelayCommand addDefaulExportDBCommand;
       

        //
        #endregion


        #region METHODS

        void loadRight()
        {
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Ecriture|| droitFormulaire.Developpeur )
                {
                    ConnexionEnabled = true;
                    EnableLimitJourFacturation = true;
                    ConnexionEnabled = true;
                   // BtnSaveVisible = true;
                }
                else
                {

                    if (droitFormulaire.Ecriture || droitFormulaire.Developpeur)
                    {
                       // BtnSaveVisible = true;
                        ConnexionEnabled = false;
                    }
                }
            }
        }


        void loadParameters()
        {

            try
            {
                if (IsConnected)
                {
                    List<SettingsModel> listeconfig = settingService.Configuration_List(societeCourante.IdSociete);
                    if (listeconfig != null && listeconfig.Count > 0)
                        Listeconfigurations = listeconfig;

                    DataTable tableLangues = ClassUtilsModel.ListeLangue();
                    if (tableLangues != null && tableLangues.Rows.Count > 0)
                    {
                        List<DisplayLangues> languesAffiches = new List<DisplayLangues>();
                        foreach (DataRow row in tableLangues.Rows)
                        {
                            languesAffiches.Add(new DisplayLangues { ID = row["ID"].ToString(), Label = row["libelle"].ToString(), CodeCulture = row["codeculture"].ToString() });
                        }
                        Displaylangues = languesAffiches;
                    }
                }

                parameter = XElement.Parse(Utils.GetParameterFiles().ToString());

                #region CONFIG DATABASE

                // lectures parametres depuis le fichier
                var queryDatabase = from p in parameter.Elements()
                                    where (string)p.Attribute("id").Value == "connectionString"
                                    select p;
                foreach (var de in queryDatabase)
                {
                    CurrentParametres.Nomserveur = de.Element("server").Value;
                    CurrentParametres.Port = "3306";
                    CurrentParametres.Utilisateur = de.Element("username").Value;
                    CurrentParametres.NomBd = de.Element("nameBd").Value;
                    CurrentParametres.Motpasse = de.Element("password").Value;
                }


                var queryDatabaseArchive = from p in parameter.Elements()
                                           where (string)p.Attribute("id").Value == "connectionStringArchives"
                                           select p;
                foreach (var de in queryDatabaseArchive)
                {
                    CurrentParametres.NomserveurArchive = de.Element("server").Value;
                    CurrentParametres.PortArchive = "3306";
                    CurrentParametres.UtilisateurArchive = de.Element("username").Value;
                    CurrentParametres.NomBdArchive = de.Element("nameBd").Value;
                    CurrentParametres.MotpasseArchive = de.Element("password").Value;


                }

                //if (IsConnected)
                //{
                //    // lectures parametres depuis la base de données

                //    SettingsModel serveur = Listeconfigurations.Find(sr => sr.Code == "server");
                //    SettingsModel username = Listeconfigurations.Find(sr => sr.Code == "username");
                //    SettingsModel port = Listeconfigurations.Find(sr => sr.Code == "port");
                //    SettingsModel bd = Listeconfigurations.Find(sr => sr.Code == "nameBd");
                //    SettingsModel password = Listeconfigurations.Find(sr => sr.Code == "password");

                //    if (serveur != null)
                //        CurrentParametres.Nomserveur = serveur.Libelle;
                //    if (serveur != null)
                //        CurrentParametres.Port = port.Libelle;
                //    if (serveur != null)
                //        CurrentParametres.Utilisateur = username.Libelle;
                //    if (serveur != null)
                //        CurrentParametres.NomBd = bd.Libelle;
                //    if (serveur != null)
                //        CurrentParametres.Motpasse = password.Libelle;

                //}

                #endregion

                #region CONFIG PASSWORD


                var queryDefaultPass = from p in parameter.Elements()
                                       where (string)p.Attribute("id").Value == "user"
                                       select p;
                foreach (var de in queryDefaultPass)
                {
                    CurrentParametres.DeaultPassword = de.Element("dpassword").Value;
                    CurrentParametres.DeaultUser = de.Element("duser").Value;
                }

                if (IsConnected)
                {
                    SettingsModel password = Listeconfigurations.Find(sr => sr.Code == "dpassword");
                    if (password != null)
                        CurrentParametres.DeaultPassword = password.Libelle;
                }

                #endregion


                #region CONFIG DEVISE TVA




                //var queryDevises = from p in parameter.Elements()
                //                   where (string)p.Attribute("id").Value == "devises"
                //                   select p;
                //foreach (var de in queryDevises)
                //{
                //    CurrentParametres.Idtva = int.Parse(de.Element("dtva").Value);
                //    CurrentParametres.IdDevise = int.Parse(de.Element("ddevise").Value);


                //}


                //if (IsConnected)
                //{
                //    SettingsModel deviseId = Listeconfigurations.Find(sr => sr.Code == "ddevise");
                //    SettingsModel tvaId = Listeconfigurations.Find(sr => sr.Code == "dtva");

                //    if (deviseId != null)
                //        CurrentParametres.IdDevise = Int32.Parse(deviseId.Libelle);
                //    if (tvaId != null)
                //        CurrentParametres.Idtva = Int32.Parse(tvaId.Libelle);

                //}

                #endregion

                var queryConfig = from p in parameter.Elements()
                                  where (string)p.Attribute("id").Value == "config"
                                  select p;
                foreach (var de in queryConfig)
                {
                    CurrentParametres.Dejautiliser = de.Element("dejaUtiliser").Value;
                    CurrentParametres.CodeCourant = de.Element("mode").Value;
                    // CurrentParametres.DureeMode = de.Element("duree").Value;


                }


                #region CONFIG BASES


                var queryBase = from p in parameter.Elements()
                                where (string)p.Attribute("id").Value == "bases"
                                select p;
                foreach (var de in queryBase)
                {
                    CurrentParametres.DureeDev = de.Element("dev").Value;
                    CurrentParametres.DureeProd = de.Element("prod").Value;
                    CurrentParametres.DureeLocaton = de.Element("location").Value;
                    CurrentParametres.DureeTeste = de.Element("teste").Value;
                    CurrentParametres.DossierImages = de.Element("dossierImages").Value;
                    CurrentParametres.PaginationHtrc = de.Element("nombrePagination").Value;
                    CurrentParametres.MailFrom = de.Element("mailfrom").Value;
                    CurrentParametres.MailTo = de.Element("mailTo").Value;
                    CurrentParametres.Smtp = de.Element("smtp").Value;
                   

                }

                var queryBasePath = from log in parameter.Elements()
                                    where (string)log.Attribute("id").Value == "log"
                                    select log;
                foreach (var de in queryBasePath)
                {
                    //CurrentParametres.PathLog = de.Element("backUpLog").Value;
                    //CurrentParametres.PathBackUpLog = de.Element("backUpLog").Value;
                    CurrentParametres.CheminFichierPath = de.Element("pathlog").Value;
                }

                if (IsConnected)
                {
                    SettingsModel dossierImage = Listeconfigurations.Find(sr => sr.Code == "dossierImages");
                    SettingsModel pagination = Listeconfigurations.Find(sr => sr.Code == "nombrePagination");
                    SettingsModel mailFrom = Listeconfigurations.Find(sr => sr.Code == "mailfrom");
                    SettingsModel mailTo = Listeconfigurations.Find(sr => sr.Code == "mailTo");
                    SettingsModel smtp = Listeconfigurations.Find(sr => sr.Code == "smtp");
                    SettingsModel pathlogFile = Listeconfigurations.Find(sr => sr.Code == "pathLogFile");
                    SettingsModel portSmtp = Listeconfigurations.Find(sr => sr.Code == "portsmtp");
                    SettingsModel logginSmtp = Listeconfigurations.Find(sr => sr.Code == "loggingsmtp");
                    SettingsModel passwordSmtp = Listeconfigurations.Find(sr => sr.Code == "passwdsmtp");
                    SettingsModel codejournal = Listeconfigurations.Find(sr => sr.Code == "codeJnlvente");

                    if (dossierImage != null)
                        CurrentParametres.DossierImages = dossierImage.Libelle;
                    if (pagination != null)
                        CurrentParametres.PaginationHtrc = pagination.Libelle;
                    if (mailFrom != null)
                        CurrentParametres.MailFrom = mailFrom.Libelle;
                    if (mailTo != null)
                        CurrentParametres.MailTo = mailTo.Libelle;
                    if (smtp != null)
                        CurrentParametres.Smtp = smtp.Libelle;
                    if (pathlogFile != null)
                        CurrentParametres.CheminFichierPath = pathlogFile.Libelle;
                    if (portSmtp != null)
                        CurrentParametres.PortSmtp = portSmtp.Libelle;
                    if (codejournal != null)
                        CurrentParametres.CodeJournalVente = codejournal.Libelle;

                }

                // rafraichier le fichier log
                if (!GlobalDatas.dataBasparameter.Nomserveur.Equals (CurrentParametres.Nomserveur))
                  GlobalDatas.dataBasparameter.Nomserveur = CurrentParametres.Nomserveur;

                if (!GlobalDatas.dataBasparameter.Motpasse.Equals(CurrentParametres.Motpasse))
                    GlobalDatas.dataBasparameter.Motpasse = CurrentParametres.Motpasse;

                if (!GlobalDatas.dataBasparameter.NomBd.Equals(CurrentParametres.NomBd))
                    GlobalDatas.dataBasparameter.NomBd = CurrentParametres.NomBd;

                if (!GlobalDatas.dataBasparameter.Motpasse.Equals(CurrentParametres.Motpasse))
                    GlobalDatas.dataBasparameter.Motpasse = CurrentParametres.Motpasse;

                if (!GlobalDatas.dataBasparameter.DeaultPassword.Equals(CurrentParametres.DeaultPassword))
                    GlobalDatas.dataBasparameter.DeaultPassword = CurrentParametres.DeaultPassword;

                if (!GlobalDatas.dataBasparameter.DeaultUser.Equals(CurrentParametres.DeaultUser))
                    GlobalDatas.dataBasparameter.DeaultUser = CurrentParametres.DeaultUser;
               
                if (!GlobalDatas.dataBasparameter.CheminFichierPath.Equals(CurrentParametres.CheminFichierPath))
                    GlobalDatas.dataBasparameter.CheminFichierPath = CurrentParametres.CheminFichierPath;

                if (Int32 .Parse ( GlobalDatas.dataBasparameter.PaginationHtrc)!=Int32.Parse(CurrentParametres.PaginationHtrc))
                    GlobalDatas.dataBasparameter.PaginationHtrc = CurrentParametres.PaginationHtrc;

                if (!GlobalDatas.dataBasparameter.MailTo.Equals(CurrentParametres.MailTo))
                    GlobalDatas.dataBasparameter.MailTo = CurrentParametres.MailTo;

                if (CurrentParametres.CodeJournalVente!=null )
                  if (!GlobalDatas.dataBasparameter.CodeJournalVente.Equals(CurrentParametres.CodeJournalVente))
                    GlobalDatas.dataBasparameter.CodeJournalVente = CurrentParametres.CodeJournalVente;
               

                #endregion

                #region CONFIG PATH



                var queryBasePathlog = from log in parameter.Elements()
                                    where (string)log.Attribute("id").Value == "log"
                                    select log;
                foreach (var de in queryBasePathlog)
                {
                    CurrentParametres.CheminFichierPath = de.Element("import").Value;
                    CurrentParametres.PathBackUpLog = de.Element("backUpLog").Value;
                    CurrentParametres.PathLog = de.Element("pathlog").Value;
                }

                if (IsConnected)
                {
                   // SettingsModel pathImport = Listeconfigurations.FirstOrDefault(sr => sr.Code == "import");
                    SettingsModel pathBacklog = Listeconfigurations.FirstOrDefault(sr => sr.Code == "backUpLog");
                    SettingsModel pathFilelog = Listeconfigurations.FirstOrDefault(sr => sr.Code == "pathLogFile");

                    SettingsModel jourlimite = Listeconfigurations.Find(sr => sr.Code == "jourlimite");
                    SettingsModel expornameDb = Listeconfigurations.Find(sr => sr.Code == "dbNameExport");


                    //if (pathImport != null)
                       // CurrentParametres.CheminFichierPath = pathImport.Libelle;
                    if (pathBacklog != null)
                        CurrentParametres.PathBackUpLog  = pathBacklog.Libelle;
                    if (pathFilelog != null)
                        CurrentParametres.PathLog = pathFilelog.Libelle;

                    if (expornameDb != null)
                       CurrentParametres.NomFichierExport = expornameDb.Libelle;


                    if (jourlimite != null)
                    {
                        CurrentParametres.JourLimiteFacturation = Int32.Parse(jourlimite.Libelle);
                        IntLimiteJourfacturation = CurrentParametres.JourLimiteFacturation;

                        // IndexLimiteJour = ListeJourLimite.FindIndex(l => l.NbreJour == CurrentParametres.JourLimiteFacturation);
                    }
                    else IntLimiteJourfacturation = 0;
                }

                #endregion

              
                    if (Displaylangues != null && Displaylangues.Count > 0)
                    {
                        int i = 0;
                        foreach (DisplayLangues l in Displaylangues)
                        {
                            if (CurrentParametres.DefaulLanguage.Equals(l.CodeCulture))
                            {
                                IndexLangue = i;
                                break;
                            }
                            else i++;
                        }
                    }

                    

                    GlobalDatas.dataBasparameter = CurrentParametres;
                
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Warning Message Chargement Parametres";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
                Utils.logUserActions(" UI Parametres ->Erreur Chargement parametres " +ex.Message , "");
            }
        }

        #region Region Update Parameter File
        
     
        private void canClose()
        {
            try
            {
                Communicator ctr = new Communicator();
                ctr.contentVue = "param";
                EventArgs e1 = new EventArgs();
                
                ctr.OnChangeText(e1);

                //_injectSingleViewService.ClearViewsFromRegion(RegionNames.ContentRegion);
               // UserAffiche uaffiche = _container.Resolve<UserAffiche>();


                //IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion,
                //                                     () => uaffiche);
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE RESUME";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }

        }
        private void canSave()
        {
            try
            {
              

                    foreach (var ele in parameter.Elements())
                    {
                        if (ele.Attribute("id").Value == "connectionString")
                        {
                            ele.Element("server").SetValue(CurrentParametres.Nomserveur);
                            // CurrentParametres.Port = "3306";
                            ele.Element("username").SetValue(CurrentParametres.Utilisateur);
                            ele.Element("nameBd").SetValue(CurrentParametres.NomBd);
                            ele.Element("password").SetValue(CurrentParametres.Motpasse);
                        }
                    }

                    if (CurrentParametres.Dejautiliser == "0")
                    {
                        foreach (var ele in parameter.Elements())
                        {
                            if (ele.Attribute("id").Value == "config")
                            {
                                ele.Element("dejaUtiliser").SetValue("1");
                            }
                        }


                    }
                    parameter.Save(Utils.getfileName());

                   // loadParameters();
                    MessageBox.Show(" Modification des Parametres Prisent en compte, \n veuillez vous Déconnectez Puis vous reconnectez");
                //}
                loadParameters();
                 

               
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Warning Message Sauvegarde Parameter File";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }

        }

        bool canExecuteSave()
        {
            bool values = false;
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur)
                {
                    if (CurrentParametres != null)
                        values = true;
                }
            }
            else values = true;
            return values;
        }

        void canSaveArchive()
        {

            try
            {

                foreach (var ele in parameter.Elements())
                {
                    if (ele.Attribute("id").Value == "connectionStringArchives")
                    {
                        ele.Element("server").SetValue(CurrentParametres.NomserveurArchive);
                        // CurrentParametres.Port = "3306";
                        ele.Element("username").SetValue(CurrentParametres.UtilisateurArchive);
                        ele.Element("nameBd").SetValue(CurrentParametres.NomBdArchive);
                        ele.Element("password").SetValue(CurrentParametres.MotpasseArchive);
                    }
                }


                parameter.Save(Utils.getfileName());

                // loadParameters();
                MessageBox.Show(" Modification des Parametres Prisent en compte, \n veuillez vous Déconnectez Puis vous reconnectez");
                //}
                loadParameters();

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Warning ! Message Sauvegarde Parameter File";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }

        }

        bool canExecuteSaveArchive()
        {
            return CurrentParametres != null ? true : false;
        }
        #endregion

        #region Region Save in database
        
      
        private void canAddPassword()
        {
            try
            {
                if (droitFormulaire != null)
                {
                    if (droitFormulaire.Edition  || droitFormulaire.Developpeur )
                    {
                        SettingsModel config = new SettingsModel { Code = "dpassword", Libelle = CurrentParametres.DeaultPassword, IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);

                        MessageBox.Show(" Modification du mot de passe par défaut Pris en compte");

                        loadParameters();

                    }
                }
                else
                {
                    SettingsModel config = new SettingsModel { Code = "dpassword", Libelle = CurrentParametres.DeaultPassword, IdSite = societeCourante.IdSociete };
                    settingService.Configuration_Add(config);

                    MessageBox.Show(" Modification du mot de passe par défaut Pris en compte");

                    loadParameters();
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Warning Message Save Parameter File";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecutePassword()
        {
            bool values = false;
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur)
                {
                    if (CurrentParametres != null)
                        if (!string.IsNullOrEmpty(CurrentParametres.DeaultPassword))
                            values = true;
                }
            }
            else
            {
                if (CurrentParametres != null)
                    if (!string.IsNullOrEmpty(CurrentParametres.DeaultPassword))
                        values = true;

            }
            return values;
        }


        private void canAddTva()
        {
            try
            {
                //SettingsModel config = new SettingsModel { Code = "dtva", Libelle = TaxeSelected.ID_Taxe.ToString(), IdSite = societeCourante.IdSociete };
                //settingService.Configuration_Add(config);
                //CurrentParametres.Idtva = 0;
                //CurrentParametres.Idtva = TaxeSelected.ID_Taxe;

                //MessageBox.Show(" Modification de la Tva par défaut Pris en compte");
                //loadParameters();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Warning Message Save Parameter File";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        

        }

        bool canExecuteTva()
        {
            bool values = false;

            if (droitFormulaire != null)
            {
                if ( droitFormulaire.Ecriture || droitFormulaire.Developpeur )
                {
                    if (TaxeSelected != null)
                        if (TaxeSelected.ID_Taxe > 0)
                            values = true;
                }
            }
            else
            {
                if (TaxeSelected != null)
                    if (TaxeSelected.ID_Taxe > 0)
                        values = true;
            }
            return values;
        }

        private void canaddDevise()
        {
            
                //try
                //{

                //    SettingsModel config = new SettingsModel { Code = "ddevise", Libelle = DeviseCurrent.ID_Devise.ToString (), IdSite = societeCourante.IdSociete };
                //    settingService.Configuration_Add(config);
                //   // CurrentParametres.Idtva = int.Parse(de.Element("dtva").Value);
                //    CurrentParametres.IdDevise = 0;
                //    CurrentParametres.IdDevise = DeviseCurrent.ID_Devise;

                //    MessageBox.Show(" Modification de la Devise par défaut Pris en compte");
                //    loadParameters();
                //}
                //catch (Exception ex)
                //{
                //    CustomExceptionView view = new CustomExceptionView();
                //    view.Owner = Application.Current.MainWindow;
                //    view.Title = "Warning Message Save Parameter File";
                //    view.ViewModel.Message = ex.Message;
                //    view.ShowDialog();
                //    IsBusy = false;
                //    this.MouseCursor = null;
                //}
            }
        bool canExecuteDevise()
        {
            bool values=false ;
            if (IsConnected)
            {
                if (droitFormulaire != null)
                {
                    if (droitFormulaire.Ecriture || droitFormulaire.Developpeur )
                    {
                        if (DeviseCurrent != null)
                            values = true;
                    }
                }
                else
                {
                    if (DeviseCurrent != null)
                        values = true;
                }
            }
          
            return values;

        }

      

        void canAddPagination()
        {
            
                try
                {
                    SettingsModel config = new SettingsModel { Code = "nombrePagination", Libelle = CurrentParametres.PaginationHtrc, IdSite = societeCourante.IdSociete };
                    settingService.Configuration_Add(config);

                    MessageBox.Show(" Modification de la Pagination  Prise en compte");
                    loadParameters();
                  

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "Warning Message Save Parameter File";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            
        }

        bool canExecutePagination()
        {
            bool values = false;
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur)
                {
                    if (CurrentParametres != null)
                        if (!string.IsNullOrEmpty(CurrentParametres.PaginationHtrc))
                            values = true;
                }
            }
            else
            {
                if (CurrentParametres != null)
                    if (!string.IsNullOrEmpty(CurrentParametres.PaginationHtrc))
                        values = true;

            }
            return values;
        }

        void canAddPath()
        {
           
                try
                {
                    // CurrentParametres.CheminFichierPath = de.Element("import").Value;

                    foreach (var ele in parameter.Elements())
                    {
                        if (ele.Attribute("id").Value == "log")
                        {
                            ele.Element("import").SetValue(CurrentParametres.CheminFichierPath);
                          
                        }
                    }

                    parameter.Save(Utils.getfileName());
                    //SettingsModel config = new SettingsModel { Code = "import", Libelle = CurrentParametres.CheminFichierPath, IdSite = societeCourante.IdSociete };
                    //settingService.Configuration_Add(config);
                    MessageBox.Show(" Modification du chemin du dossier du chemin import pris en compte");
                    loadParameters();
                   
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "Warning Message Save Path  File";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            

        }

        bool canExecutePath()
        {
            bool values = false;
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur)
                {
                    if (CurrentParametres != null)
                        if (!string.IsNullOrEmpty(CurrentParametres.CheminFichierPath))
                            values = true;
                }
            }
            else
            {
                if (CurrentParametres != null)
                    if (!string.IsNullOrEmpty(CurrentParametres.CheminFichierPath))
                        values = true;
            }
            return values;
        }

        void canAddPathBAckUp()
        {
           
                try
                {
                    SettingsModel config = new SettingsModel { Code = "backUpLog", Libelle = CurrentParametres.PathBackUpLog, IdSite = societeCourante.IdSociete };
                    settingService.Configuration_Add(config);
                    MessageBox.Show(" Modification du chemin du dossier de back up défaut Pris en compte");
                    loadParameters();
                    
                
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "Warning Message Save Path File";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            

        }

        bool canExecutePathBackUp()
        {
            bool values = false;
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur)
                {
                    if (CurrentParametres != null)
                        if (!string.IsNullOrEmpty(CurrentParametres.PathBackUpLog))
                            values = true;
                }
            }
            else
            {
                if (CurrentParametres != null)
                    if (!string.IsNullOrEmpty(CurrentParametres.PathBackUpLog))
                        values = true;
            }
            return values;
        }

        void canAddMailFrom()
        {
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition | droitFormulaire.Developpeur )
                {
                    try
                    {
                        SettingsModel config = new SettingsModel { Code = "mailfrom", Libelle = CurrentParametres.MailFrom, IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);

                        MessageBox.Show(" Modification du mail par défaut Pris en compte");
                        loadParameters();
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "Warning Message modification mail";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                    }

                }
            }
        }


        void canAddMailTo()
        {
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur)
                {
                    try
                    {
                        SettingsModel config = new SettingsModel { Code = "mailTo", Libelle = CurrentParametres.MailTo, IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);

                        MessageBox.Show(" Modification du mail de destination par défaut Pris en compte");
                        loadParameters();
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "Warning Message modification mail";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                    }

                }
            }
        }

        void canAddStmp()
        {
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur)
                {
                    try
                    {
                        SettingsModel config = new SettingsModel { Code = "smtp", Libelle = CurrentParametres.Smtp, IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);

                        MessageBox.Show(" Modification du serveur Smtp  par défaut Pris en compte");
                        loadParameters();
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "Warning Message modification Serveur MAil";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                    }

                }
            }
        }



        void canAddPahFileLog()
        {
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition  || droitFormulaire.Developpeur )
                {
                    try
                    {
                        SettingsModel config = new SettingsModel { Code = "pathLogFile", Libelle = CurrentParametres.PathLog, IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);

                        MessageBox.Show(" Modification du chemin pris en compte");
                        loadParameters();
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "ERREUR MODIFICATION CHEMIN FICHIER LOG";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                    }

                }
            }
        }

        void canAddPort()
        {
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition  || droitFormulaire.Developpeur )
                {
                    try
                    {
                      SettingsModel config = new SettingsModel { Code = "portsmtp", Libelle = CurrentParametres.PortSmtp, IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);
                          MessageBox.Show(" Modification du port");
                        loadParameters();
                     }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "ERREUR MODIFICATION PORT SMTP";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                    }
                    
                }
            }

        }
        void canAddLogginStmp()
        {
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition  || droitFormulaire.Developpeur )
                {
                    try
                    {
                        SettingsModel config = new SettingsModel { Code = "loggingsmtp", Libelle = CurrentParametres.LogginSmtp, IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);
                        MessageBox.Show(" Modification du loggin port");
                        loadParameters();
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "ERREUR MODIFICATION LOGGIN SMTP";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                    }
                }
            }

        }


        void canAddPasswordStmp()
        {
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur )
                {
                    try
                    {
                        SettingsModel config = new SettingsModel { Code = "passwdsmtp", Libelle = CurrentParametres.PasswordSmtp, IdSite = societeCourante.IdSociete };
                        settingService.Configuration_Add(config);
                        MessageBox.Show(" Modification du mot de passe port");
                        loadParameters();
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "ERREUR MODIFICATION PASSWORD SMTP";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                    }
                }
            }

        }

        bool canExecutePathLog()
        {
            return CurrentParametres != null ? (string.IsNullOrEmpty(CurrentParametres.PathLog) ? false : true) : false;
        }


        void canAddLanguage()
        {

            try
            {

                foreach (var ele in parameter.Elements())
                {
                    if (ele.Attribute("id").Value.Equals("devises"))
                    {
                        ele.Element("defaultlangue").SetValue(DisplaylangueSelect.CodeCulture);
                        GlobalDatas.defaultLanguage = DisplaylangueSelect.CodeCulture;
                        parameter.Save(Utils.getfileName());

                        MessageBox.Show(" Modification de la langue par default");

                        //  LanguageHelper.LanguageHelperAdd();

                        break;
                    }
                }


                //}
                // parameter.Save(Utils.getfileName());

                //SettingsModel config = new SettingsModel { Code = "defaultLangue", Libelle =DisplaylangueSelect.ID , IdSite = societeCourante.IdSociete };
                //settingService.Configuration_Add(config);
                //MessageBox.Show(" Modification de la langue par default");
                //if (Displaylangues != null && Displaylangues.Count > 0)
                //{
                //    int i = 0;
                //    foreach (DisplayLangues l in Displaylangues)
                //    {
                //        if (DisplaylangueSelect.ID.Equals(l.ID))
                //        {
                //            IndexLangue = i;
                //            GlobalDatas.defaultLanguage = DisplaylangueSelect.ID;
                //            GlobalDatas.cultureCode = l.CodeCulture;
                //            break;
                //        }
                //        else i++;
                //    }

                //}

                // GlobalDatas.cultureCode =

                // DisplaylangueSelect = null;
                // loadParameters();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREUR MODIFICATION LANGUE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }



        }
        bool canExecuteLanguage()
        {
            bool values = false;
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition  || droitFormulaire.Developpeur )
                {
                    values = true;
                }
            }
            return values;
        }

        void canSaveJourLimite()
        {
            try
            {
                SettingsModel config = new SettingsModel { Code = "jourlimite", Libelle = CurrentParametres.JourLimiteFacturation.ToString(), IdSite = societeCourante.IdSociete };
                settingService.Configuration_Add(config);

                MessageBox.Show(" Modification jour limite facturation Pris en compte");

                loadParameters();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Warning Message Save Parameter File";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecuteSaveJourLimite()
        {
            bool values = false;
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition  || droitFormulaire.Developpeur )
                {
                    if (CurrentParametres!=null )
                        if (CurrentParametres.JourLimiteFacturation>0)
                    values = true;
                }
            }
            return values;
        }

        void canAddexportDbName()
        {
            try
            {
                SettingsModel config = new SettingsModel { Code = "dbNameExport", Libelle = CurrentParametres.NomFichierExport.ToString(), IdSite = societeCourante.IdSociete };
                settingService.Configuration_Add(config);

                MessageBox.Show(" Modification Prise en compte");

                loadParameters();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Warning Message Save Parameter File";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecuteExportDbName()
        {
            bool values = false;
            if (droitFormulaire != null)
            {
                if (droitFormulaire.Edition || droitFormulaire.Developpeur  )
                {
                    if (CurrentParametres != null)
                        if (!string.IsNullOrEmpty( CurrentParametres.NomFichierExport))
                            values = true;
                }
            }
            return values;
        }

        #endregion

        #region Autres


        public List<JourLimite> GetJourList
        {
            get
            {
                List<JourLimite> liste = new List<JourLimite>();
                for (int i = 1; i <= 31; i++)
                    liste.Add(new JourLimite { NbreJour = i });
                return liste;
            }
        }
        #endregion

        #endregion

    }

    public class currentdevisesTaxe
    {
        public int idDevise { get; set; }
        public string devise { get; set; }
        public int idTaxe { get; set; }
        public string taxe { get; set; }
    }

    public class LocalDevise
    {
        public int idDevise { get; set; }
        public string devise { get; set; }
       
    }

    public class LocalTaxe
    {
       
        public int idTaxe { get; set; }
        public string taxe { get; set; }
    }

    public class DefaulTpass
    {
        public string MotPasse { get; set; }
    }

    public class DisplayLangues
    {
        public string ID { get; set; }
        public string Label { get; set; }
        public string CodeCulture { get; set; }
    }

    public class JourLimite
    {
        public int NbreJour { get; set; }
    }
}
