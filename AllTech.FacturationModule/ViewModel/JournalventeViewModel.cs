using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using System.Windows.Input;
using AllTech.FrameWork.Utils;
using System.ComponentModel;
using AllTech.FrameWork.Views;
using System.Windows;
using System.Data;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;
using AllTech.FacturationModule.Views;
using System.IO;
using AllTech.FacturationModule.Report;
using Multilingue.Resources;
using System.Threading;

namespace AllTech.FacturationModule.ViewModel
{
    public class JournalventeViewModel : ViewModelBase
    {

        #region Fields

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteHstCommand;
        private RelayCommand importCommand;
        private RelayCommand displayCommand;
        private RelayCommand displayHstCommand;
        private RelayCommand exportCommand;
        private RelayCommand closeCommand;
        private RelayCommand printCommand;
      

        private RelayCommand searchByCommand;
        private RelayCommand updateTousCommand;
        
        public SocieteModel societeCourante;
        UtilisateurModel UserConnected;
       public  ParametresModel ParametersDatabase;
        DroitModel CurrentDroit;


        DateTime? dateDebutSelected;
        DateTime? dateFinSelected;
        DateTime? dateFin;
        DateTime? dateDebut;
        DateTime DateSelected;

        bool IsGenererate = false;
        private bool isBusy;
        bool _progressBarVisibility;
        bool _progressBarToexportVisibility;
        private Cursor mouseCursor;

        string messageGenerate;

        PeriodeDateJv periodeSelected;
        List<PeriodeDateJv> periodeListes;

        PeriodeMoisJv periodeMoisSelected;
        List<PeriodeMoisJv> periodeMoisListes;

       

        List<JournalventesDatesModel> historicJournal;
        JournalventesDatesModel historicJournalSelected;
        JournalventesDatesModel historicService;
        JournalVentesModel jvService;
        JournalVentesModel jvHistorictSelected;
        List<JournalVentesModel> jvHistoricList;
        List<JournalVentesModel> jvHistoricListCache;

       

        JournalVentesGroupeModel jornalVenteService;
        List<JournalVentesGroupeModel> journalVenteHstList;
        JournalVentesGroupeModel journalVenteSelected;
      
        DataTable factureslistByPeriodes;
        DataRef_JournalVente fliste;
        string nomDossier;
        ProgressWindow progress;
        Window localwindow;
        string numeroNoteCredit;
        string numfactureSelected;
        string numerofactureSearchby;

       
        bool isTextBoxEnable;
        bool isVisibleCancelNomClient;

        bool isModeObjet;
        bool isModeProduit;
        bool isVisibleLogCompta;
        List<LogJournalvente> logJournalventeList;

        string nobreFactureSelectionnees;
        int? jourLimiteversement;
        
  
        #endregion


        public JournalventeViewModel(DataRef_JournalVente controls, Window window)
        {
            societeCourante = GlobalDatas.DefaultCompany;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            historicService = new JournalventesDatesModel();
            jornalVenteService = new JournalVentesGroupeModel();
            fliste = controls;
            localwindow = window;
            NomDossier = ParametersDatabase.PathJournalVente;
            jvService = new JournalVentesModel();
            //CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("Journal Ventes"));
           CurrentDroit = UserConnected.Profile.Droit.Find(p => p.IdVues == 15);
           
           if (CurrentDroit.Lecture || CurrentDroit.Developpeur)
           {
               loadperiodes();
               if (ParametersDatabase.JvModeSelect)
               {
                   IsModeObjet = true;
                   IsModeProduit = false;
               }
               else
               {
                   IsModeObjet = false;
                   IsModeProduit = true;
               }
           }

           jourLimiteversement = GlobalDatas.dataBasparameter.JourLimiteFacturation;
        }


        #region Properties

        public List<PeriodeMoisJv> PeriodeMoisListes
        {
            get { return periodeMoisListes; }
            set { periodeMoisListes = value;
            this.OnPropertyChanged("PeriodeMoisListes");
            }
        }

        public PeriodeMoisJv PeriodeMoisSelected
        {
            get { return periodeMoisSelected; }
            set { periodeMoisSelected = value;
            if (PeriodeSelected != null && value != null)
            {
                DateSelected = DateTime.Parse("01/" + PeriodeMoisSelected.Mois + "/" + PeriodeSelected.Annee);
                loadHistoric();
            }
            this.OnPropertyChanged("PeriodeMoisSelected");
            }
        }

        public string NobreFactureSelectionnees
        {
            get { return nobreFactureSelectionnees; }
            set
            {
                nobreFactureSelectionnees = value;
                this.OnPropertyChanged("NobreFactureSelectionnees");
            }
        }

        public List<LogJournalvente> LogJournalventeList
        {
            get { return logJournalventeList; }
            set { logJournalventeList = value;
            this.OnPropertyChanged("LogJournalventeList");
            }
        }

        public bool IsVisibleLogCompta
        {
            get { return isVisibleLogCompta; }
            set { isVisibleLogCompta = value;
            this.OnPropertyChanged("IsVisibleLogCompta");
            }
        }

        public bool IsModeObjet
        {
            get { return isModeObjet; }
            set { isModeObjet = value;
            this.OnPropertyChanged("IsModeObjet");
            }
        }


        public bool IsModeProduit
        {
            get { return isModeProduit; }
            set { isModeProduit = value;
            this.OnPropertyChanged("IsModeProduit");
            }
        }

        public List<JournalVentesModel> JvHistoricListCache
        {
            get { return jvHistoricListCache; }
            set { jvHistoricListCache = value; }
        }


        public bool IsVisibleCancelNomClient
        {
            get { return isVisibleCancelNomClient; }
            set { isVisibleCancelNomClient = value;
            this.OnPropertyChanged("IsVisibleCancelNomClient");
            }
        }

        public string NumerofactureSearchby
        {
            get { return numerofactureSearchby; }
            set { numerofactureSearchby = value;
          
            this.OnPropertyChanged("NumerofactureSearchby");
            }
        }
        public bool IsTextBoxEnable
        {
            get { return isTextBoxEnable; }
            set { isTextBoxEnable = value;
            this.OnPropertyChanged("IsTextBoxEnable");
            }
        }

        public string NumfactureSelected
        {
            get { return numfactureSelected; }
            set { numfactureSelected = value;
            this.OnPropertyChanged("NumfactureSelected");
            }
        }

        public JournalVentesModel JvHistorictSelected
        {
            get { return jvHistorictSelected; }
            set { jvHistorictSelected = value;
            if (value != null && value.IdStatut == 14007 && int.Parse( value.Ordre)==3)
            {
                NumfactureSelected = value.NumeroFacture;
                IsTextBoxEnable = true;
            }
            else
            {
                IsTextBoxEnable = false;
            }
            this.OnPropertyChanged("JvHistorictSelected");
            }
        }

        public List<JournalVentesModel> JvHistoricList
        {
            get { return jvHistoricList; }
            set { jvHistoricList = value;
            this.OnPropertyChanged("JvHistoricList");
            }
        }

        public string NumeroNoteCredit
        {
            get { return numeroNoteCredit; }
            set { numeroNoteCredit = value;
            this.OnPropertyChanged("NumeroNoteCredit");
            }
        }

        public string NomDossier
        {
            get { return nomDossier; }
            set { nomDossier = value;
            this.OnPropertyChanged("NomDossier");
            }
        }

        public JournalVentesGroupeModel JournalVenteSelected
        {
            get { return journalVenteSelected; }
            set { journalVenteSelected = value;
            this.OnPropertyChanged("JournalVenteSelected");
            }
        }

        public List<JournalVentesGroupeModel> JournalVenteHstList
        {
            get { return journalVenteHstList; }
            set { journalVenteHstList = value;
            this.OnPropertyChanged("JournalVenteHstList");
            }
        }


        public DataTable FactureslistByPeriodes
        {
            get { return factureslistByPeriodes; }
            set { factureslistByPeriodes = value;
            this.OnPropertyChanged("FactureslistByPeriodes");
            }
        }

        public JournalventesDatesModel HistoricJournalSelected
        {
            get { return historicJournalSelected; }
            set { historicJournalSelected = value;
            this.OnPropertyChanged("HistoricJournalSelected");
            }
        }

        public List<JournalventesDatesModel> HistoricJournal
        {
            get { return historicJournal; }
            set
            {
                historicJournal = value;
                this.OnPropertyChanged("HistoricJournal");
            }
        }
        public List<PeriodeDateJv> PeriodeListes
        {
            get { return periodeListes; }
            set
            {
                periodeListes = value;
                this.OnPropertyChanged("PeriodeListes");
            }
        }

        public PeriodeDateJv PeriodeSelected
        {
            get { return periodeSelected; }
            set
            {
                periodeSelected = value; 
                if (value != null)
                {
                    PeriodeMoisListes=Loadperiodemois(value.Annee);
                   

                  
                }
                this.OnPropertyChanged("PeriodeSelected");
            }
        }



        public string MessageGenerate
        {
            get { return messageGenerate; }
            set
            {
                messageGenerate = value;
                this.OnPropertyChanged("MessageGenerate");
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
                this.OnPropertyChanged("ProgressBarVisibility");
            }
        }

        public bool ProgressBarToexportVisibility
        {
            get { return _progressBarToexportVisibility; }
            set
            {
                _progressBarToexportVisibility = value;
                this.OnPropertyChanged("ProgressBarToexportVisibility");
            }
        }

        

        public DateTime? DateDebutSelected
        {
            get { return dateDebutSelected; }
            set
            {
                dateDebutSelected = value;
                this.OnPropertyChanged("DateDebutSelected");
            }
        }


        public DateTime? DateFinSelected
        {
            get { return dateFinSelected; }
            set
            {
                dateFinSelected = value;
                this.OnPropertyChanged("DateFinSelected");
            }
        }


        public DateTime? DateDebut
        {
            get { return dateDebut; }
            set
            {
                dateDebut = value;
                this.OnPropertyChanged("DateDebut");
            }
        }


        public DateTime? DateFin
        {
            get { return dateFin; }
            set
            {
                dateFin = value;
                this.OnPropertyChanged("DateFin");

            }
        }

        //public bool IsVisibleCancelNomClient
        //{
        //    get { return isVisibleCancelNomClient; }
        //    set
        //    {
        //        isVisibleCancelNomClient = value;
        //        this.OnPropertyChanged("IsVisibleCancelNomClient");
        //    }
        //}

        #endregion

        #region Commands


        public ICommand ImportCommand
        {
            get
            {
                if (this.importCommand == null)
                {
                    this.importCommand = new RelayCommand(param => this.canGenrateJv(), param => this.canExecuteJv());
                }
                return this.importCommand;
            }
        }

        public ICommand DisplayCommand
        {
            get
            {
                if (this.displayCommand == null)
                {
                    this.displayCommand = new RelayCommand(param => this.canDisplayJv(), param => this.canExecuteDisplayJv());
                }
                return this.displayCommand;
            }
        }

        public ICommand DisplayHstCommand
        {
            get
            {
                if (this.displayHstCommand == null)
                {
                    this.displayHstCommand = new RelayCommand(param => this.canDisplayhstJv(), param => this.canExecuteDisplayHstJv());
                }
                return this.displayHstCommand;
            }
        }
        //
        public ICommand DleteHstCommand
        {
            get
            {
                if (this.deleteHstCommand == null)
                {
                    this.deleteHstCommand = new RelayCommand(param => this.candeleteHst(), param => this.canExecuteDeleteHstJv());
                }
                return this.deleteHstCommand;
            }


        }

        //

        public ICommand PrintCommand
        {
            get
            {
                if (this.printCommand == null)
                {
                    this.printCommand = new RelayCommand(param => this.canPrintCr(param), param => this.canExecutePrint());
                }
                return this.printCommand;
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

        //private RelayCommand updatelignejvCommand;
       // private RelayCommand updateTousCommand;

        public ICommand SearchByCommand
        {
            get
            {
                if (this.searchByCommand == null)
                {
                    this.searchByCommand = new RelayCommand(param => this.canUpdateligneJv(), param => this.canExecuteUpdateligneJv());
                }
                return this.searchByCommand;
            }

        }

        public ICommand UpdateTousCommand
        {
            get
            {
                if (this.updateTousCommand == null)
                {
                    this.updateTousCommand = new RelayCommand(param => this.canUpdateallJv(), param => this.canExecuteUpdateAllJv());
                }
                return this.updateTousCommand;
            }

        }

        #endregion

        #region Methods

        #region Loading

        void LoJournalventeForma(DataTable dtbLogView)
        {
            List<LogJournalvente> liste = new List<LogJournalvente>();
            foreach (DataRow row in dtbLogView.Rows)
            {
                liste.Add(new LogJournalvente { idJv = Convert.ToInt32(row["ID_Jvente"]), NumFacture = Convert.ToString(row["NumeroFacture"]),
                                                MessageError = Convert.ToString(row["MessageError"]),
                                                Datelog = Convert.ToDateTime(row["DateLog"]),
                                                TypeEventMsg = Convert.ToString(row["TypeMessage"]).Equals("gen") ? "Generation Jv" : "Export Jv",
                                                Colormessage = row["valMessage"] != DBNull.Value == null ? (Convert.ToBoolean(row["valMessage"]) != true ? "Red" : "Black") : "Black"
                });
            }

            LogJournalventeList = liste;
        }

        void loadperiodes()
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //worker.DoWork += (o, args) =>
            //    {
                    try
                    {

                        DataTable table = historicService.GetJournalVentesDates_Periodes(societeCourante.IdSociete);
                        if (table != null && table.Rows.Count > 0)
                        {
                            List<PeriodeDateJv> liste = new List<PeriodeDateJv>();
                            foreach (DataRow row in table.Rows)
                            {
                                liste.Add(new PeriodeDateJv { Annee = Convert.ToString(row["annee"]) });
                            }
                            PeriodeListes = liste;
                            if (PeriodeListes.Count == 1)
                            {
                                PeriodeSelected = PeriodeListes[0];
                                //periodes

                            }
                        }
                        else PeriodeListes = null;

                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title ="ERREUR CHARGEMENt PERIODE";
                        view.ViewModel.Message=ex.Message ;
                        view.ShowDialog();

                       // args.Result = ex.Message + " ;" + ex.InnerException + ";" + "MESSAGE INFORMATION CHARGEMENT PERIODES";
                    }
                //};
            //      worker.RunWorkerCompleted += (o, args) =>
            //     {
            //         if (args.Result != null)
            //         {
            //             CustomExceptionView view = new CustomExceptionView();
            //             view.Owner = Application.Current.MainWindow;
            //             view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
            //             view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
            //             view.ShowDialog();




            //         }
            //         else
            //         {


            //         }
            //     };

            //worker.RunWorkerAsync();
        }

        void loadHistoric()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (DateSelected != null)
                    {
                       HistoricJournal= historicService.GetJournalVentesDates_List(societeCourante.IdSociete, DateSelected);
                    }


                }
                catch (Exception ex)
                {

                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "MESSAGE INFORMATION IMPORT";
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




        #endregion

        #region Region affichage historic
        
       
        // chargement de historique
        void canDisplayhstJv()
        {
            BackgroundWorker worker = new BackgroundWorker();
            fliste.StartStopWait(true);
            worker.DoWork += (o, args) =>
            {
                DataTable rsltListefacture = null;
                try
                {
                    this.IsBusy = true;
                    // JournalVenteHstList= jornalVenteService.HistoricComptes(HistoricJournalSelected.ID);
                    JvHistoricList = jvService.GetListHistorique_jv(ref rsltListefacture, HistoricJournalSelected.ID, (ParametersDatabase.JvModeSelect == true ? "obj" : "prod"));
                    JvHistoricListCache = JvHistoricList;

                    //affichage du Log
                    if (CurrentDroit.Jvpreparation || CurrentDroit.Developpeur)
                    {

                        DataTable dtbLogView = ComptabiliteModel.GetLogCompta(HistoricJournalSelected.ID, "gen");
                        if (dtbLogView != null && dtbLogView.Rows.Count > 0)
                        {
                            IsVisibleLogCompta = true;
                            LoJournalventeForma(dtbLogView);
                        }
                        else IsVisibleLogCompta = false;
                    }
                }

                catch (Exception ex)
                {

                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "MESSAGE INFORMATION IMPORT";
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
                    //ProgressBarVisibility = false;
                }
            };


            worker.RunWorkerAsync();
        }

        bool canExecuteDisplayHstJv()
        {
            bool isvalues = false;

            if (CurrentDroit.JvLecture ||  CurrentDroit.Developpeur)
            {
                if (HistoricJournalSelected != null)
                    isvalues = true;
            }

            return isvalues;
        }

        #endregion

        #region Region creation du journal



        // generation du rapport
        void canGenrateJv()
        {
            bool value = false;
            bool isnextMois = false;

            if (PeriodeListes == null && HistoricJournal == null)
            {

                value = false;
            }
            else
            {
                if (PeriodeListes == null )
                {
                    MessageBox.Show(ConstStrings.Get("journalVente_NotifDateJournal_Msg"));
                    return;
                }
                else
                {
                    int codeD = DateDebutSelected.Value.ToShortDateString().GetHashCode();
                    int codeF = DateFinSelected.Value.ToShortDateString().GetHashCode();
                    // value = HistoricJournal.Exists(jv => jv.DateDebut.ToShortDateString().GetHashCode() == codeD && jv.DateFin.ToShortDateString().GetHashCode() == codeF);
                    value = false;
                }

            }
            if (!value)
            {
                if (string.IsNullOrEmpty(GlobalDatas.dataBasparameter.CodeJournalVente))
                {
                    MessageBox.Show(ConstStrings.Get("journalVente_NotifCode_Msg"));
                    return;
                }

               
                    isnextMois = false;
              

                // teste de factures

                DataTable tableListeFcature = null;
                int nbrefactSelected=0;
                int totalFactureTraitees=0;
                int totalFactureTraiteesErre = 0;

                try
                {
                    tableListeFcature = historicService.JournalVenteGetListe(societeCourante.IdSociete, DateDebutSelected.Value, DateFinSelected.Value);
                    nbrefactSelected=tableListeFcature.Rows.Count;
                    NobreFactureSelectionnees = "";
                    if (tableListeFcature != null)
                    {
                        if (nbrefactSelected > 0)
                        {
                            NobreFactureSelectionnees = string.Format(" Nombre de factures générées : {0}", nbrefactSelected.ToString());
                            ProgressBarVisibility = true;
                            int idDate = 0;
                          
                            if (historicService.DateJournalInsert(ref idDate, nbrefactSelected, societeCourante.IdSociete, DateDebutSelected.Value, DateFinSelected.Value))
                            {


                                BackgroundWorker worker = new BackgroundWorker();
                                int newIdGenerate = 0;
                                this.IsBusy = true;
                                DataRow rowfact = null;

                                string MessageFinaleTitreDebut = string.Format("----------------Début  Génération Journal vente  du {0}-------------------------------", DateTime.Now);
                                ComptabiliteModel.LogComptaAdd(idDate, "generation JV", MessageFinaleTitreDebut, "gen", false);

                                worker.DoWork += (o, args) =>
                                {
                                    foreach (DataRow row in tableListeFcature.Rows)
                                    {
                                       
                                        try
                                        {
                                           rowfact= historicService.GetFactureByIdjV(Convert.ToInt64(row["ID"])).Rows[0];
                                          // worker.ReportProgress(0);
                                           NobreFactureSelectionnees = string.Format("Facture {0} en cours traitement ", rowfact["Numerofacture"].ToString());
                                           historicService.JournalGlobaleGenerate(idDate, row, isnextMois);

                                          

                                            string messageerror = string.Format("Facture {0} Généré correctement client {1} -- totalTTc{2} ", rowfact["Numerofacture"], rowfact["Nom_client"], rowfact["totalTTC"]);
                                            ComptabiliteModel.LogComptaAdd(idDate, rowfact["Numerofacture"].ToString(), messageerror, "gen",true);
                                            totalFactureTraitees++;
                                        }
                                        catch (Exception ex)
                                        {
                                            IsVisibleLogCompta = true;
                                            string messageerror = string.Format("Erreure Génération Facture {0} ;  Facture  : {1} client {2} -- totalTTc{3}", ex.Message, rowfact["Numerofacture"], rowfact["Nom_client"], rowfact["totalTTC"]);
                                            ComptabiliteModel.LogComptaAdd(idDate, rowfact["Numerofacture"].ToString(), messageerror, "gen",false);
                                            totalFactureTraiteesErre++;
                                        }
                                    }
                                };
                                worker.ProgressChanged += (o, args) =>
                                {
                                   // NobreFactureSelectionnees = string.Format("Facture {0} ", rowfact["Numerofacture"].ToString());

                                    // progress.LBInfos = args.ProgressPercentage.ToString() + "%";
                                    // progress.ValueProgressBar = args.ProgressPercentage;
                                   // viewModal.ValueProgressBar = args.ProgressPercentage;
                                   // viewModal.LBInfos = args.ProgressPercentage.ToString() + "%";
                                };

                                worker.RunWorkerCompleted += (o, args) =>
                                {

                                    //if (args.Result != null)
                                   // {
                                        //CustomExceptionView view = new CustomExceptionView();
                                        //view.Owner = Application.Current.MainWindow;
                                        //view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                                        //view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                                        //view.ShowDialog();
                                        //this.MouseCursor = null;
                                        //this.IsBusy = false;
                                        //ProgressBarVisibility = false;

                                        //NobreFactureSelectionnees = "Annulation du traitement de ce journal de vente";
                                        //historicService.JournalVentesDELETE(newIdGenerate);

                                   // }
                                   // else
                                   // {
                                    NobreFactureSelectionnees = string.Format("Fin traitement, Total Générés :{0} ; Total en Erreurs : {1}", totalFactureTraitees, totalFactureTraiteesErre);
                                        //this.MouseCursor = null;
                                        try
                                        {
                                            historicService.DateJournalUpdate(idDate, totalFactureTraitees);
                                            this.IsBusy = false;
                                            ProgressBarVisibility = false;
                                            DataTable hstFact = null;
                                            if (DateSelected != null)
                                                HistoricJournal = historicService.GetJournalVentesDates_List(societeCourante.IdSociete, DateSelected);
                                            else HistoricJournal = historicService.GetJournalVentesDates_List(societeCourante.IdSociete, DateTime.Now);
                                            // on charge le gride
                                            HistoricJournalSelected = HistoricJournal.FirstOrDefault(jv => jv.ID == idDate);
                                            JournalVenteHstList = null;
                                            JvHistoricList = jvService.GetListHistorique_jv(ref hstFact, idDate, (ParametersDatabase.JvModeSelect == true ? "obj" : "prod"));
                                            JvHistoricListCache = JvHistoricList;

                                            DateDebutSelected = null;
                                            DateFinSelected = null;
                                            loadperiodes();

                                            if (PeriodeSelected != null)
                                                PeriodeMoisListes = Loadperiodemois(PeriodeSelected.Annee);

                                            //log
                                            if (CurrentDroit.Jvpreparation || CurrentDroit.Developpeur)
                                            {
                                                DataTable dtbLogView = ComptabiliteModel.GetLogCompta(idDate, "gen");
                                                if (dtbLogView != null && dtbLogView.Rows.Count > 0)
                                                {
                                                    IsVisibleLogCompta = true;
                                                    LoJournalventeForma(dtbLogView);
                                                }
                                                else IsVisibleLogCompta = false;
                                            }

                                            string MessageFinaleTitrefin = string.Format("----------------Fin  Génération Journal vente  du {0} -------------------------------", DateTime.Now);
                                            ComptabiliteModel.LogComptaAdd(idDate, "", MessageFinaleTitrefin, "gen", false);

                                            string MessageFinaleTitrefind = string.Format("-----------------------------------------------");
                                            ComptabiliteModel.LogComptaAdd(idDate, "", MessageFinaleTitrefind, "gen", false);

                                        }
                                        catch (Exception ex)
                                        {
                                            MessageBox.Show(ex.Message,"Erreure fin de traitement");
                                        }
                                   // }
                                };


                                worker.RunWorkerAsync();



                            }// inser date

                             


                           // Thread.Sleep(500);

                           // LoadGeretateJv(tableListeFcature);

                            NobreFactureSelectionnees = "";


                        }
                        else
                        {
                            throw new Exception("Pas de Nouvelle Factures à Traiter pour cette Plage");
                        }
                    }
                    else
                    {
                        throw new Exception("Pas de Nouvelle  Factures à Traiter pour cette Plage");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show( ex.Message, "ERREURE MESSAGE CREATION JV", MessageBoxButton.OK, MessageBoxImage.Error);
                }



            }
            else
            {
                MessageBox.Show(ConstStrings.Get("journalVente_NotifJvgenerateExist_Msg"));
            }



        }

        bool canExecuteJv()
        {
            bool values = false;
            if (CurrentDroit.Jvpreparation ||  CurrentDroit.Developpeur)
            {
                if (DateDebutSelected.HasValue && DateFinSelected.HasValue)
                    values = true;
            }
            return values;
        }


     



        #endregion

        #region Region suppression dun journal
        
     

        void candeleteHst()
        {
            //if (jvService.JournalVente_testeDelete(HistoricJournalSelected.ID))
            //{
            //    MessageBox.Show("Attention vous ne pouvez pas supprimer cet journal, \n il existe des factures ayant fait lobjet dun Export précédent","ERREUR SUPPRESSION JOURNAL DE VENTE",MessageBoxButton.OK,MessageBoxImage.Error);
            //    return;
            //}

              StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner =localwindow;
            messageBox.Title = "MESSAGE INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer ce journal ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {

                    historicService.JournalVentesDELETE(HistoricJournalSelected.ID);

                    StringBuilder fichier = new StringBuilder();
                    fichier.Append(HistoricJournalSelected.NumeroJournal);
                    fichier.Append(HistoricJournalSelected.DateDebut.ToShortDateString().Replace("/", "").Trim());
                    fichier.Append(HistoricJournalSelected.DateFin.ToShortDateString().Replace("/", "").Trim());
                    fichier.Append(societeCourante.IdSociete);
                    fichier.Append(".txt");
                    string cheminComplet = ParametersDatabase.PathJournalVente + @"\\" + fichier;
                    if (File.Exists(cheminComplet))
                        File.Delete(cheminComplet);
                    //suppressio du log journal
                    ComptabiliteModel.LogComptaDelete(HistoricJournalSelected.ID);
                    JvHistoricList = null;
                    JournalVenteHstList = null;
                    LogJournalventeList = null;
                    IsVisibleLogCompta = false;

                    HistoricJournal=null ;

                    if (DateSelected != null)
                       HistoricJournal = historicService.GetJournalVentesDates_List(societeCourante.IdSociete, DateSelected);
                    else HistoricJournal = historicService.GetJournalVentesDates_List(societeCourante.IdSociete, DateTime.Now);
                    if (HistoricJournal == null)
                        PeriodeListes = null;

                    if (PeriodeSelected!=null )
                    PeriodeMoisListes = Loadperiodemois(PeriodeSelected.Annee);
                 
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "MESSAGE INFORMATION SUPPRESSION";
                    if (ex.Message.Contains("constraint fails"))
                        view.ViewModel.Message = "Impossible de supprimer ce Journal! Il est déja Associer ";
                    else
                        view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }

        }

        bool canExecuteDeleteHstJv()
        {
            bool isvalues = false;
            if (CurrentDroit.JvSuppression ||  CurrentDroit.Developpeur )
            {
                if (HistoricJournalSelected!=null )
                   isvalues = true;
            }
            return isvalues;
        }

        #endregion

        #region Region Impression

        void canPrintCr(object param)
        {
            try
            {
                switch (param.ToString())
                {
                    case "crystal":
                        {

                            IsBusy = true;
                            DataSet dsTable = jvService.GetListToExport(HistoricJournalSelected.ID, (ParametersDatabase.JvModeSelect == true ? "obj" : "prod"));
                            DataTable jvTable = dsTable.Tables[0];

                            if (jvTable != null && jvTable.Rows.Count > 0)
                            {
                                FormreportCompta report = new FormreportCompta(jvTable);
                                report.ShowDialog();

                            }
                            break;
                        }
                    case "excel":
                        {
                            CommonModule.ExportJournalVenteToExcel(JvHistoricListCache, HistoricJournalSelected.DateDebut.ToShortDateString(), HistoricJournalSelected.DateFin.ToShortDateString(), HistoricJournalSelected.NumeroJournal);
                            break;
                        }
                }
                
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE INFORMATION EXPORT JOURNAL";
                    view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecutePrint()
        {
            bool isvalues = false;
            if (CurrentDroit.Execution  || CurrentDroit.Developpeur)
            {
                isvalues = JvHistoricListCache != null ? (JvHistoricListCache != null ? true : false) : false;
            }
            return isvalues;
        }

        #endregion

        #region Region Export

        // can export to text
        void canDisplayJv()
        {
            //DataTable dtlListeParams = LoadDataParameters();
           // if (dtlListeParams != null)
           // {

                if (!string.IsNullOrEmpty(ParametersDatabase.PathJournalVente))
                {
                    if (Directory.Exists(ParametersDatabase.PathJournalVente))
                    {

                        StringBuilder fichier = new StringBuilder();
                        fichier.Append(HistoricJournalSelected.NumeroJournal);
                        fichier.Append(HistoricJournalSelected.DateDebut.ToString("ddMMyyyy"));
                        fichier.Append(HistoricJournalSelected.DateFin.ToString("ddMMyyyy"));
                        fichier.Append(societeCourante.IdSociete);
                        fichier.Append(".txt");
                        string cheminComplet = ParametersDatabase.PathJournalVente + @"\\" + fichier;
                        ProgressBarToexportVisibility = true;
                        StreamWriter sw = null;

                        try
                        {
                            IsBusy = true;
                            bool isoperation = false;
                           
                              DataSet dsTable= jvService.GetListToExport(HistoricJournalSelected.ID, (ParametersDatabase.JvModeSelect == true ? "obj" : "prod"));
                              DataTable jvTable = dsTable.Tables[0];
                            if (jvTable != null && jvTable.Rows.Count > 0)
                            {
                                // vreation du fichier texte
                                if (!File.Exists(cheminComplet))
                                {
                                    isoperation = true;
                                }
                                else
                                {
                                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                    messageBox.Owner = Application.Current.MainWindow;
                                    messageBox.Title = "INFORMATION DE SUPPRESSION FICHIER";
                                    messageBox.ViewModel.Message = "Il existe déja un fichier généré pour ce journal\n Voulez vous le supprimer ?";
                                    if (messageBox.ShowDialog().Value == true)
                                    {
                                        isoperation = true;
                                        File.Delete(cheminComplet);
                                    }
                                    else
                                    {
                                        isoperation = false;
                                        ProgressBarToexportVisibility = false;
                                        IsBusy = false;
                                    }

                                }

                                if (isoperation)
                                {
                                    // parametre
                                   // DataTable tableConfig = ComptabiliteModel.GetComptaGene_Liste();
                                    sw = File.CreateText(cheminComplet);
                                   
                                    // sw.Write("\r\n{0}", "gescom");
                                    StringBuilder fichierTexte = null;
                                    
                                    decimal totalTTC = 0;
                                    decimal tva = 0;
                                    decimal centimes = 0;
                                    decimal totalHt = 0;

                                    DataTable dtblListe = dsTable.Tables[1];
                                    DataView dv = jvTable.DefaultView;
                                    int idDate = 0;
                                    int nbreFactValide = 0;
                                    int nbreFactEncours = 0;
                                    int nbreFactAvoir = 0;
                                    int nbreFactureErreurs = 0;
                                    bool isEncours=false;
                                    bool isValide = false;
                                    bool isAvoir = false;

                                    if (dtblListe.Rows.Count > 0)
                                    {
                                        idDate = Convert.ToInt32(dtblListe.Rows[0]["ID_DAte"]);

                                        string MessageFinaleTitreDebut = string.Format("----------------Début  Génération fichier Export  du {0} -------------------------------", DateTime.Now);
                                        ComptabiliteModel.LogComptaAdd(idDate, "", MessageFinaleTitreDebut, "exp", false);

                                        foreach (DataRow row in dtblListe.Rows)
                                        {
                                            dv.RowFilter = string.Format("ID_Facture ='{0}'", Convert.ToInt64(row["ID_Facture"]));
                                            DataTable newdtbl = dv.ToTable();
                                           // DataRow[] rows1 = jvTable.Select(string.Format("ID_Facture='{0}'", Convert.ToInt64(row["ID_Facture"])));


                                            switch (Convert.ToInt64(row["ID_Statut"]))
                                            {
                                                //case 14002:
                                                //    {
                                                        
                                                //        foreach (DataRow rowl in newdtbl.Rows)
                                                //        {
                                                //            if (Convert.ToInt32(row["ID_Objet"]) == 11)
                                                //            {
                                                //                if (Convert.ToInt32(rowl["Ordre"]) == 1)
                                                //                    totalTTC = Convert.ToDecimal(rowl["MontantDebit"]);

                                                //                if (Convert.ToInt32(rowl["Ordre"]) == 3)
                                                //                    totalHt += Convert.ToDecimal(rowl["MontantCredit"]);

                                                //                if (Convert.ToInt32(rowl["Ordre"]) == 2)
                                                //                    tva = Convert.ToDecimal(rowl["MontantCredit"]);
                                                //            }
                                                //            else
                                                //            {
                                                //                if (Convert.ToInt32(rowl["Ordre"]) == 4)
                                                //                    totalTTC = Convert.ToDecimal(rowl["MontantCredit"]);

                                                //                if (Convert.ToInt32(rowl["Ordre"]) == 2)
                                                //                    totalHt += Convert.ToDecimal(rowl["MontantCredit"]);

                                                //                if (Convert.ToInt32(rowl["Ordre"]) == 3)
                                                //                    tva = Convert.ToDecimal(rowl["MontantDebit"]);

                                                //            }
                                                //        }

                                                //        nbreFactEncours++;
                                                //        isEncours = true;
                                                //        isValide = false;
                                                //        isAvoir = false;
                                                //        break;
                                                //    }
                                                case 14003:
                                                    {
                                                        foreach (DataRow rowl in newdtbl.Rows)
                                                        {
                                                            if (Convert.ToInt32(rowl["Ordre"]) == 1)
                                                                totalTTC = Convert.ToDecimal(rowl["MontantDebit"]);

                                                            if (Convert.ToInt32(rowl["Ordre"]) == 2)
                                                                tva = Convert.ToDecimal(rowl["MontantCredit"]);

                                                            if (Convert.ToInt32(rowl["Ordre"]) == 3)
                                                                centimes += Convert.ToDecimal(rowl["MontantCredit"]);


                                                            if (Convert.ToInt32(rowl["Ordre"]) == 4)
                                                                totalHt += Convert.ToDecimal(rowl["MontantCredit"]);


                                                        }

                                                        nbreFactValide++;

                                                        isEncours = false;
                                                        isValide = true;
                                                        isAvoir = false;
                                                        break;
                                                    }
                                                case 14007:
                                                    {
                                                        foreach (DataRow rowl in newdtbl.Rows)
                                                        {
                                                            if (Convert.ToInt32(rowl["Ordre"]) == 1)
                                                                totalTTC =Math.Abs( Convert.ToDecimal(rowl["MontantCredit"]));

                                                            if (Convert.ToInt32(rowl["Ordre"]) == 2)
                                                                tva = Math.Abs(Convert.ToDecimal(rowl["MontantDebit"]));

                                                            if (Convert.ToInt32(rowl["Ordre"]) == 3)
                                                                centimes += Math.Abs(Convert.ToDecimal(rowl["MontantDebit"]));

                                                            if (Convert.ToInt32(rowl["Ordre"]) == 4)
                                                                totalHt += Math.Abs(Convert.ToDecimal(rowl["MontantDebit"]));

                                                            
                                                        }
                                                        nbreFactAvoir++;

                                                        isEncours = false;
                                                        isValide = false;
                                                        isAvoir = true;

                                                        break;
                                                    }
                                            }

                                            if (totalTTC == (totalHt + tva + centimes))
                                            {
                                                //facture équilibré
                                                CreateFacture(newdtbl, sw, Convert.ToInt64(row["ID_Statut"]));
                                            }
                                            else
                                            {
                                                //NumeroJournal
                                                nbreFactureErreurs++;

                                                if(isEncours)
                                                    nbreFactEncours--;

                                                if (isAvoir)
                                                    nbreFactAvoir--;

                                                if (isValide)
                                                    nbreFactValide--;


                                                string messageerror = string.Format("Cette Facture {0} N'est pas équilibré, elle n'a pas été prise en compte dans le déversement",row["Numerofacture"]);
                                                ComptabiliteModel.LogComptaAdd(Convert.ToInt32(row["ID_DAte"]), row["Numerofacture"].ToString(), messageerror,"exp",true);
                                                string messageerror1 = string.Format("Le montant total HTT {0}  est diéfferent du total HT :{1} + la TVA :{2} + centimes {3} =={4}", totalTTC, totalHt, tva, centimes, (totalHt + tva + centimes));
                                                ComptabiliteModel.LogComptaAdd(Convert.ToInt32(row["ID_DAte"]), row["Numerofacture"].ToString(), messageerror1,"exp",true);

                                               
                                            }
                                            totalTTC = 0;
                                            totalHt = 0;
                                            tva = 0;
                                            centimes = 0;
                                        }//fin traitement export

                                        string MessageFinaleTitre = string.Format("----------------Fin export Génération fichier Export  du {0}-------------------------------",DateTime.Now);
                                         ComptabiliteModel.LogComptaAdd(idDate, "", MessageFinaleTitre, "exp", false);
                                         string MessageFinaleTitre1=string.Format("Nombre de factures  Encours Validations Exportées :{0}",nbreFactEncours);
                                         ComptabiliteModel.LogComptaAdd(idDate, "", MessageFinaleTitre1, "exp", false);
                                           string MessageFinaleTitre2=string.Format("Nombre de factures  Validées Exportées :{0}",nbreFactValide);
                                           ComptabiliteModel.LogComptaAdd(idDate, "", MessageFinaleTitre2, "exp", false);
                                           string MessageFinaleTitre3 = string.Format("Nombre de factures  Avoir Exportées :{0}", nbreFactAvoir);
                                           ComptabiliteModel.LogComptaAdd(idDate, "", MessageFinaleTitre3, "exp", false);
                                           string MessageFinaleTitre4 = string.Format("Nombre de factures  En Erreures Non Exportées  :{0}", nbreFactureErreurs);
                                           ComptabiliteModel.LogComptaAdd(idDate, "", MessageFinaleTitre4, "exp", false);
                                        // log final

                                    }


                                    //foreach (DataRow row in jvTable.Rows)
                                    //{
                                    //    //DataRow[] rows1 = dtlListeParams.Select("positions='" + (j-1) + "'");
                                    //    //DataRow rowCurrent = rows1[0];


                                    //    fichierTexte = new StringBuilder();
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToDateTime(row["Datefacture"]).ToString("ddMMyy"), (int)tailleChamp.CHAMP1));
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["CodeJournal"]), (int)tailleChamp.CHAMP2));
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["CompteGene"]), (int)tailleChamp.CHAMP3));
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["CompteTiers"]), (int)tailleChamp.CHAMP4));
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["CompteAnalytique"]), (int)tailleChamp.CHAMP5));
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["LibelleSectionAnal"]), (int)tailleChamp.CHAMP6));
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["MontantDebit"]), (int)tailleChamp.CHAMP7));
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["MontantCredit"]), (int)tailleChamp.CHAMP8));
                                    //    fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["TypeCompte"]), (int)tailleChamp.CHAMP9));
                                    //    sw.WriteLine(fichierTexte);
                                    //  //  j++;

                                    //}

                                    sw.Close();
                                    sw.Dispose();

                                    // flag des factures
                                    if (jvService.JournalVenteToExportUpdate(HistoricJournalSelected.ID))
                                    {

                                        MessageBox.Show("Fichier généré avec succes", "MESSAGE GENERATION FICHIER VENTE", MessageBoxButton.OK, MessageBoxImage.Information);

                                        DataTable dtbLogView = ComptabiliteModel.GetLogCompta(HistoricJournalSelected.ID,"exp");
                                        if (dtbLogView != null && dtbLogView.Rows.Count > 0)
                                        {
                                            IsVisibleLogCompta = true;
                                            LoJournalventeForma(dtbLogView);
                                        }
                                        else IsVisibleLogCompta = false;
                                       
                                        ProgressBarToexportVisibility = false;
                                        IsBusy = false;

                                    }
                                    // 


                                }
                               

                            }
                            else
                            {
                                MessageBox.Show("Pas de données pour générer le fichier", "MESSAGE INFORMATION", MessageBoxButton.OK, MessageBoxImage.Hand);
                            }


                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            view.Owner = Application.Current.MainWindow;
                            view.Title = "Export Message";
                            view.ViewModel.Message = ex.Message;
                            view.ShowDialog();
                            ProgressBarToexportVisibility = false;
                            IsBusy = false;
                            sw.Dispose();
                            sw.Close();
                            if (File.Exists(cheminComplet))
                                File.Delete(cheminComplet);
                        }
                        // if (progress != null) progress.Close();
                        // progress = new ProgressWindow();
                        // BackgroundWorker worker = new BackgroundWorker();
                        // worker.WorkerReportsProgress = true;
                        // progress.ProgressBarMinimum = 0;

                        // // liste des factures
                        //// progress.ProgressBarMaximum = newFactures.Count;
                        // progress.Show();
                        // worker.DoWork += (o, args) =>
                        //  {
                        //      //foreach ()
                        //      //progress.LBInfos = i.ToString() + "%";
                        //      //progress.ValueProgressBar = i;
                        //      //i++;
                        //  };
                        // worker.ProgressChanged += (o, args) =>
                        // {
                        //     progress.LBInfos = args.ProgressPercentage.ToString() + "%";
                        //     progress.ValueProgressBar = args.ProgressPercentage;

                        // };

                        // worker.RunWorkerCompleted += (o, args) =>
                        // {
                        //     if (args.Result != null)
                        //     {
                        //         CustomExceptionView view = new CustomExceptionView();
                        //         view.Owner = Application.Current.MainWindow;
                        //         view.Title = args.Result.ToString().Substring(args.Result.ToString().LastIndexOf(";"));
                        //         view.ViewModel.Message = view.Title = args.Result.ToString().Substring(0, args.Result.ToString().LastIndexOf(";"));
                        //         view.ShowDialog();
                        //         this.MouseCursor = null;
                        //         this.IsBusy = false;


                        //         progress.Close();
                        //     }
                        //     else
                        //     {
                        //         progress.Close();

                        //         this.MouseCursor = null;
                        //         this.IsBusy = false;
                        //         //ProgressBarVisibility = false;
                        //     }
                        // };


                        // worker.RunWorkerAsync();
                    }
                    else
                    {
                        //StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        //messageBox.Owner = localwindow;
                        //messageBox.Title = "MESSAGE CREATION FICHIER";
                        //messageBox.ViewModel.Message = "Chemin du reprtoire invalide";
                        //messageBox.ShowDialog();
                        MessageBox.Show("Chemin du repertoire non valide", "MESSAGE INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                else
                {
                    //StyledMessageBoxView messageBox = new StyledMessageBoxView();
                    //messageBox.Owner = localwindow;
                    //messageBox.Title = "MESSAGE CREATION FICHIER";
                    //messageBox.ViewModel.Message = "Vous devez d'abord indiquer le reperoire de sauvegarde\n du fichier à générer";
                    //messageBox.ShowDialog();
                    MessageBox.Show("Vous devez d'abord indiquer le reperoire de sauvegarde\n du fichier à générer", "MESSAGE INFORMATION", MessageBoxButton.OK, MessageBoxImage.Information);
                }
           // }
            
        }

        bool canExecuteDisplayJv()
        {
            bool isvalues = false;
            if (CurrentDroit.JvExport || CurrentDroit.Developpeur)
            {
                if (HistoricJournalSelected != null)
                    isvalues = true;
            }
            return isvalues;
            // return HistoricJournalSelected !=null ?true :false ;
        }

        void CreateFacture(DataTable jvTable ,StreamWriter sw, Int64 statut)
        {
            foreach (DataRow row in jvTable.Rows)
            {
                //DataRow[] rows1 = dtlListeParams.Select("positions='" + (j-1) + "'");
                //DataRow rowCurrent = rows1[0];
                decimal mntDebit = 0;
                decimal mntCredit = 0;

                if (statut == 14007)
                {
                    mntDebit = row["MontantDebit"] != DBNull.Value ? Math.Abs(Convert.ToDecimal(row["MontantDebit"])) : 0;
                    mntCredit = row["MontantCredit"] != DBNull.Value ? Math.Abs(Convert.ToDecimal(row["MontantCredit"])) : 0;
                }
                else
                {
                    mntDebit = row["MontantDebit"] != DBNull.Value ? Convert.ToDecimal(row["MontantDebit"]) : 0;
                    mntCredit = row["MontantCredit"] != DBNull.Value ? Convert.ToDecimal(row["MontantCredit"]) : 0;

                }

                StringBuilder fichierTexte = new StringBuilder();
                fichierTexte.Append(FormatChampFlatFile(Convert.ToDateTime(row["Datefacture"]).ToString("ddMMyy"), (int)tailleChamp.CHAMP1));
                fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["CodeJournal"]), (int)tailleChamp.CHAMP2));
                fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["CompteGene"]), (int)tailleChamp.CHAMP3));
                fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["CompteTiers"]), (int)tailleChamp.CHAMP4));
                fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["CompteAnalytique"]), (int)tailleChamp.CHAMP5));
                fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["LibelleSectionAnal"]), (int)tailleChamp.CHAMP6));
                fichierTexte.Append(FormatChampFlatFile(mntDebit.ToString(), (int)tailleChamp.CHAMP7));
                fichierTexte.Append(FormatChampFlatFile(mntCredit.ToString(), (int)tailleChamp.CHAMP8));
                fichierTexte.Append(FormatChampFlatFile(Convert.ToString(row["TypeCompte"]), (int)tailleChamp.CHAMP9));
                sw.WriteLine(fichierTexte);
                //  j++;

            }
        }


        string FormatChampFlatFile(string valeur, Int32 longueurChamp)
        {
            valeur = valeur.Trim(); //.Replace('\r', ' ').Replace('\n', ' ');
            return (valeur.Length <= longueurChamp) ? valeur.PadRight(longueurChamp, ' ') : valeur.Substring(0, longueurChamp);
        }

      

        #endregion

        #region Region Update facture
        // recherche dans dans la collection
        void canUpdateligneJv()
        {
             BackgroundWorker worker = new BackgroundWorker();
            fliste.StartStopWait(true);
            worker.DoWork += (o, args) =>
            {

                try
                {
                    this.IsBusy = true;
                    DataTable rsltListefacture = null;
                    if (!string.IsNullOrEmpty(NumerofactureSearchby))
                    {
                        JvHistoricList = jvService.GetListHistorique_Search(ref rsltListefacture, NumerofactureSearchby);
                        IsVisibleLogCompta = false;
                        HistoricJournalSelected = null;
                    }
                    else
                        JvHistoricList = null;



                    this.IsBusy = false;
                }
                catch (Exception ex)
                {

                    args.Result = ex.Message + " ;" + ex.InnerException + ";" + "MESSAGE INFORMATION RECHERCHE";
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
                    //ProgressBarVisibility = false;
                }
            };


            worker.RunWorkerAsync();
        }

        bool canExecuteUpdateAllJv()
        {
            bool isvalues = false;
            if (CurrentDroit.Ecriture  || CurrentDroit.Developpeur )
            {
                if (NumfactureSelected != null)
                    isvalues = true;
            }
            return isvalues;


        }

        bool canExecuteUpdateligneJv()
        {

            bool isvalues = false;
            if (CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
               
                    isvalues = true;
            }
            return isvalues;
           
        }


        void canUpdateallJv()
        {
            try
            {
                DataTable rsltListefacture = null;
                jvService.JournalVenteHistoriqueUpdateNote(HistoricJournalSelected.ID, JvHistorictSelected.Id, NumeroNoteCredit);
               // canDisplayhstJv();
                JvHistoricList = jvService.GetListHistorique_jv(ref rsltListefacture, JvHistorictSelected.IdDate.Value, (ParametersDatabase.JvModeSelect == true ? "obj" : "prod"));
                JvHistoricListCache = JvHistoricList;

                NumeroNoteCredit = string.Empty;

                MessageBox.Show("Facture avoire mise jour","MISE jOUR FACTURE AVOIR",MessageBoxButton.OK,MessageBoxImage.Information);
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Export Message";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

      
        #endregion

        void canClose()
        {
            Communicator ctr = new Communicator();
            ctr.contentVue = "jv";
            EventArgs e1 = new EventArgs();
            ctr.OnChangeText(e1);
        }
        #endregion

        DataTable LoadDataParameters()
        {
            try
            {
                return   ComptabiliteModel.GetComptaGene_Param_Fichier();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "Export Message";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                return null;

            }
        }

        List<PeriodeMoisJv> Loadperiodemois(string annee)
        {
            List<PeriodeMoisJv> liste = null;
            try
            {
                DataTable tableMois = jvService.JournalVente_Periode_mois(PeriodeSelected.Annee);
                liste = loadMois(tableMois);
                
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erreur Import "+ex.Message,"IMPORT PERIODE MOIS");
            }
            return liste;
        }

        List<PeriodeMoisJv> loadMois(DataTable table)
        {
            List<PeriodeMoisJv> liste = new List<PeriodeMoisJv>();
            foreach (DataRow row in table.Rows)
                liste.Add(new PeriodeMoisJv { Mois = row["mois"].ToString(), MoisNom = moinString(row["mois"].ToString()) });

            return liste;
        }

        string moinString(string mois)
        {
            string val = string.Empty;

            switch (mois)
            {
                case "01": { val = "Janvier"; break; }
                case "02": { val = "Fevrier"; break; }
                case "03": { val = "Mars"; break; }
                case "04": { val = "Avril"; break; }
                case "05": { val = "Mai"; break; }
                case "06": { val = "Juin"; break; }
                case "07": { val = "Juillet"; break; }
                case "08": { val = "Aout"; break; }
                case "09": { val = "Septembre"; break; }
                case "10": { val = "Octobre"; break; }
                case "11": { val = "Novembre"; break; }
                case "12": { val = "Decembre"; break; }
            }
            return val;
        }
    }

   

    public class PeriodeDateJv
    {
        public string Annee { get; set; }
    }

    public class PeriodeMoisJv
    {
        public string Mois { get; set; }
        public string MoisNom { get; set; }
    }

    public class LogJournalvente:ViewModelBase
    {
    
        public int idJv { get; set; }
        public string NumFacture { get; set; }
        public string MessageError { get; set; }
        public string TypeEventMsg { get; set; }
        private string colormessage;

        public string Colormessage
        {
            get { return colormessage; }
            set { colormessage = value;
            this.OnPropertyChanged("Colormessage");
            }
        }
        public DateTime Datelog { get; set; }
    }

    public enum tailleChamp
    {
        CHAMP1=6,
        CHAMP2 = 6,
        CHAMP3 = 13,
        CHAMP4 = 17,
        CHAMP5 = 13,
        CHAMP6 = 35,
        CHAMP7 = 14,
        CHAMP8 = 14,
        CHAMP9 = 1
    }
}
