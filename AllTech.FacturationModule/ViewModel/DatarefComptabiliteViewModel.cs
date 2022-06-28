using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using System.Windows.Input;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;
using System.ComponentModel;
using AllTech.FrameWork.Views;
using System.Windows;
using System.Data;
using AllTech.FacturationModule.Views.Modal;

namespace AllTech.FacturationModule.ViewModel
{
    public class DatarefComptabiliteViewModel : ViewModelBase
    {

        #region fields


        private RelayCommand newCommandCgen;
        private RelayCommand saveCommandCgen;
        private RelayCommand deleteCommandCgen;
        private RelayCommand closeCommand;
        private RelayCommand addDefaulCodeJournalVenteCommand;
        private RelayCommand addComptaOhadaCommand;

        public SocieteModel societeCourante;
        UtilisateurModel UserConnected;
        ParametresModel parametersDatabase;

      

        CompteGenralModel compteservice;
        CompteGenralModel compteGeneSelected;
        List<CompteGenralModel> compteGenerals;

        List<CompteOhadaModel> comptaOhadasfree;
        CompteOhadaModel comptaOhadaservice;
        CompteOhadaModel comptaOhadafreeSelected;


      
        Window localwindow;
        SettingsModel settingService;

        private RelayCommand newCACommand;
        private RelayCommand saveCACommand;
        private RelayCommand deleteCACommand;

        private RelayCommand addComptageneCommand;


        private RelayCommand saveComptaListeCommand;
        private RelayCommand deleteComptaListeCommand;

        private RelayCommand addComptaParamCommand;
        private RelayCommand saveComptaParamCommand;
        private RelayCommand deleteComptaParamCommand;



        
             
        CompteAnalytiqueModel compteCAservice;
        CompteAnalytiqueModel compteCASelected;
        List<CompteAnalytiqueModel> comptesCA;

        List<ComptaList> tableComptaliste;
        ComptaList rowComptaListeSelcted;
        string stringTextComptaChampList;
        string codeComptaChamplist;

        DataTable tableComptaChamparam;
        int cmbComptaListIndex;
        DataRowView rowParamSelect;
        DataRowView rowSelect;

      

        public bool isOperation;
        bool isCustomVisible;
        bool isRadioCustomChecked;
        bool isRadionDefaulChecked;

        int indexCompOhadafree;

      
        #endregion

        public DatarefComptabiliteViewModel(Window window)
        {
            societeCourante = GlobalDatas.DefaultCompany;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            compteservice = new CompteGenralModel();
            compteCAservice = new CompteAnalytiqueModel();
            settingService = new SettingsModel();
            comptaOhadaservice = new CompteOhadaModel();
            localwindow = window;
           // Load();
            LoadComptabiliteListe();
        }

        #region Properties

        public int IndexCompOhadafree
        {
            get { return indexCompOhadafree; }
            set { indexCompOhadafree = value;
            this.OnPropertyChanged("IndexCompOhadafree");
            }
        }

        public CompteOhadaModel ComptaOhadafreeSelected
        {
            get { return comptaOhadafreeSelected; }
            set { comptaOhadafreeSelected = value;
            this.OnPropertyChanged("ComptaOhadafreeSelected");
            }
        }


        public List<CompteOhadaModel> ComptaOhadasfree
        {
            get { return comptaOhadasfree; }
            set { comptaOhadasfree = value;
            this.OnPropertyChanged("ComptaOhadasfree");
            }
        }

        public DataRowView RowSelect
        {
            get { return rowSelect; }
            set { rowSelect = value;
           
            ComptaList compte = new ComptaList();
            compte.Id = Convert.ToInt32(value[1]);
            compte.Libelle = Convert.ToString(value[4]);
            compte.Code = value[6] != DBNull.Value ? Convert.ToString(value[6]) : "";
            StringTextComptaChampList = Convert.ToString(value[4]);
            RowComptaListeSelcted = compte;
            CodeComptaChamplist = compte.Code;
            this.OnPropertyChanged("RowSelect");
            }
        }

        public string CodeComptaChamplist
        {
            get { return codeComptaChamplist; }
            set { codeComptaChamplist = value;
            this.OnPropertyChanged("CodeComptaChamplist");
            }
        }
        public ParametresModel ParametersDatabase
        {
            get { return parametersDatabase; }
            set { parametersDatabase = value;
            this.OnPropertyChanged("ParametersDatabase");
            }
        }
        public bool IsCustomVisible
        {
            get { return isCustomVisible; }
            set { isCustomVisible = value;
            this.OnPropertyChanged("IsCustomVisible");
            }
        }

        public DataRowView RowParamSelect
        {
            get { return rowParamSelect; }
            set { rowParamSelect = value;
            this.OnPropertyChanged("RowParamSelect");
            }
        }

        public int CmbComptaListIndex
        {
            get { return cmbComptaListIndex; }
            set { cmbComptaListIndex = value;
            this.OnPropertyChanged("CmbComptaListIndex");
            }
        }

        public string StringTextComptaChampList
        {
            get { return stringTextComptaChampList; }
            set { stringTextComptaChampList = value;
            this.OnPropertyChanged("StringTextComptaChampList");
            }
        }

        public DataTable TableComptaChamparam
        {
            get { return tableComptaChamparam; }
            set { tableComptaChamparam = value;
            this.OnPropertyChanged("TableComptaChamparam");
            }
        }

        public ComptaList RowComptaListeSelcted
        {
            get { return rowComptaListeSelcted; }
            set { rowComptaListeSelcted = value;
            if (value != null)
            {
                StringTextComptaChampList = value.Libelle;
                CodeComptaChamplist = value.Code;
            }

            this.OnPropertyChanged("RowComptaListeSelcted");
            }
        } 

        public List <ComptaList> TableComptaliste
        {
            get { return tableComptaliste; }
            set { tableComptaliste = value;
            this.OnPropertyChanged("TableComptaliste");
            }
        }

        public CompteAnalytiqueModel CompteCASelected
        {
            get { return compteCASelected; }
            set { compteCASelected = value;
            if (value != null)
            {
                StringTextComptaChampList = value.Libelle;
            }
            this.OnPropertyChanged("CompteCASelected");
            }
        }


        public List<CompteAnalytiqueModel> ComptesCA
        {
            get { return comptesCA; }
            set { comptesCA = value;
            this.OnPropertyChanged("ComptesCA");
            }
        }

        public CompteGenralModel CompteGeneSelected
        {
            get { return compteGeneSelected; }
            set
            {
                compteGeneSelected = value;
                this.OnPropertyChanged("CompteGeneSelected");
            }
        }

        public List<CompteGenralModel> CompteGenerals
        {
            get { return compteGenerals; }
            set
            {
                compteGenerals = value;
                this.OnPropertyChanged("CompteGenerals");
            }
        }

        #endregion

        #region Icommand

        public ICommand AddComptageneCommand
        {
            get
            {
                if (this.addComptageneCommand == null)
                {
                    this.addComptageneCommand = new RelayCommand(param => this.AddCmptaGene(), param => this.canAddExecuteAddCpmteGene());
                }
                return this.addComptageneCommand;
            }
        }

        //   private RelayCommand addComptageneCommand;
        public ICommand AddComptaOhadaCommand
        {
            get
            {
                if (this.addComptaOhadaCommand == null)
                {
                    this.addComptaOhadaCommand = new RelayCommand(param => this.canAddCmptOhada());
                }
                return this.addComptaOhadaCommand;
            }
        }


        public ICommand NewCommandCgen
        {
            get
            {
                if (this.newCommandCgen == null)
                {
                    this.newCommandCgen = new RelayCommand(param => this.canNewCgen(), param => this.canNewExecuteCgen());
                }
                return this.newCommandCgen;
            }
        }

        public ICommand SaveCommandCgen
        {
            get
            {
                if (this.saveCommandCgen == null)
                {
                    this.saveCommandCgen = new RelayCommand(param => this.canSaveCgen(), param => this.canExecuteSaveCgen());
                }
                return this.saveCommandCgen;
            }
        }

        public ICommand DeleteCommandCgen
        {
            get
            {
                if (this.deleteCommandCgen == null)
                {
                    this.deleteCommandCgen = new RelayCommand(param => this.canDeleteCgen(), param => this.canExecuteDeleteCgen());
                }
                return this.deleteCommandCgen;
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

        #region Compta gene
        
       

        public ICommand SaveComptaListeCommand
        {
            get
            {
                if (this.saveComptaListeCommand == null)
                {
                    this.saveComptaListeCommand = new RelayCommand(param => this.canSaveComptaliste(), param => this.canExecuteSaveComptaList());
                }
                return this.saveComptaListeCommand;
            }
        }

        public ICommand DeleteComptaListeCommand
        {
            get
            {
                if (this.deleteComptaListeCommand == null)
                {
                    this.deleteComptaListeCommand = new RelayCommand(param => this.canDeleteComptaliste(), param => this.canExecuteDeleteComptaList());
                }
                return this.deleteComptaListeCommand;
            }
        }

        public ICommand AddComptaParamCommand
        {
            get
            {
                if (this.addComptaParamCommand == null)
                {
                    this.addComptaParamCommand = new RelayCommand(param => this.canAddComptaParam(), param => this.canExecuteAddComptaParam());
                }
                return this.addComptaParamCommand;
            }
        }

        public ICommand SaveComptaParamCommand
        {
            get
            {
                if (this.saveComptaParamCommand == null)
                {
                    this.saveComptaParamCommand = new RelayCommand(param => this.canSaveComptaParam(), param => this.canExecuteSaveComptaParam());
                }
                return this.saveComptaParamCommand;
            }
        }

        public RelayCommand AddDefaulCodeJournalVenteCommand
        {
            get
            {
                if (this.addDefaulCodeJournalVenteCommand == null)
                {
                    this.addDefaulCodeJournalVenteCommand = new RelayCommand(param => this.canAddJournalV(), param => this.canExecuteJournalV());
                }
                return this.addDefaulCodeJournalVenteCommand;
            }

        }

        #endregion

       

        #region Region analytique

        public ICommand NewCACommand
        {
            get
            {
                if (this.newCACommand == null)
                {
                    this.newCACommand = new RelayCommand(param => this.canNewCA());
                }
                return this.newCACommand;
            }
        }

        public ICommand SaveCACommand
        {
            get
            {
                if (this.saveCACommand == null)
                {
                    this.saveCACommand = new RelayCommand(param => this.canSaveCA(), param => this.canExecuteSaveCA());
                }
                return this.saveCACommand;
            }


        }

        public ICommand DeleteCACommand
        {
            get
            {
                if (this.deleteCACommand == null)
                {
                    this.deleteCACommand = new RelayCommand(param => this.canDeleteCA(), param => this.canExecuteCA());
                }
                return this.deleteCACommand;
            }


        }
        #endregion


        #endregion

        #region methods

        public  void LoadComptabiliteListe()
        {
            //BackgroundWorker worker = new BackgroundWorker();

            //worker.DoWork += (o, args) =>
            //{
                try
                {
                    DataTable QueryTable = ComptabiliteModel.GetComptaGene_Liste();
                    if (QueryTable !=null )
                    {

                        // chargement liste des elemnets de champs export
                        List<ComptaList> liste = new List<ComptaList>();
                        ComptaList compte = null;
                        foreach (DataRow row in QueryTable.Rows)
                        {
                            compte = new ComptaList();
                            compte.Id = Convert.ToInt32(row[0]);
                            compte.Libelle = Convert.ToString(row[1]);
                            compte.Code = row[2] != DBNull.Value ? Convert.ToString(row[2]) : "";
                            liste.Add(compte);
                        }
                        TableComptaliste = liste;
                     
                    }

                    TableComptaChamparam = ComptabiliteModel.GetComptaGene_Param_Liste();
                    if (TableComptaChamparam.Rows != null && TableComptaChamparam.Rows.Count > 0)
                    {
                        foreach (DataRow row in TableComptaChamparam.Rows)
                        {
                            var rr = TableComptaliste.FirstOrDefault(c => c.Id == Convert.ToInt32(row["IdChamps"]));
                            if (rr !=null )
                            TableComptaliste.Remove(rr);
                        }

                        if (TableComptaliste.Count == 0){
                            TableComptaliste = null;

                        }
                        
                    }
                    CmbComptaListIndex = -1;

                    ComptaOhadasfree = comptaOhadaservice.selectAll_Free(societeCourante.IdSociete);

                    CompteGenerals = compteservice.ModelCompteGeneral_SelectAll(societeCourante.IdSociete);
                    IndexCompOhadafree = -1;
                    ComptesCA = compteCAservice.ModelCompteAnal_SelectAll(societeCourante.IdSociete);
                   
                }
                catch (Exception ex)
                {
                   // args.Result = ex.Message + " ;" + ex.InnerException;
                    CustomExceptionView view = new CustomExceptionView();
                    view.Title = "INFORMATION ERREURE CHARGEMENT ";
                    view.Owner = localwindow;
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                }

            //};
            //worker.RunWorkerCompleted += (o, args) =>
            //{
            //    if (args.Result != null)
            //    {
            //        CustomExceptionView view = new CustomExceptionView();
            //        view.Title = "INFORMATION ERREURE CHARGEMENT ";
            //        view.Owner = localwindow;
            //        view.ViewModel.Message = "Problème survenu lors du chargement des comptes Generaux";
            //        view.ShowDialog();

            //    }


            //};

            //worker.RunWorkerAsync();
        }

        public void LoadCallBack()
        {
            ComptaOhadasfree = comptaOhadaservice.selectAll_Free(societeCourante.IdSociete);
            CompteGenerals = compteservice.ModelCompteGeneral_SelectAll(societeCourante.IdSociete);
            IndexCompOhadafree = -1;
        }

        void Load()
        {
            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, args) =>
            {
                try
                {
                   // CompteGenerals = compteservice.ModelCompteGeneral_SelectAll(societeCourante.IdSociete);

                   // ComptesCA = compteCAservice.ModelCompteAnal_SelectAll(societeCourante.IdSociete);
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
                    view.Title = "INFORMATION ERREURE CHARGEMENT COMPTE GENERAL";
                    view.Owner = localwindow;
                    view.ViewModel.Message = "Problème survenu lors du chargement des comptes Generaux";
                    view.ShowDialog();

                }


            };

            worker.RunWorkerAsync();
        }

        #region Compte general


        void AddCmptaGene()
        {
           // ComptaOhadafreeSelected

        }

        bool canAddExecuteAddCpmteGene()
        {
            return ComptaOhadafreeSelected !=null ?true :false;
        }

        void canNewCgen()
        {
            CompteGeneSelected =new CompteGenralModel ();
            CompteGeneSelected.IdCompteGen = 0;
          
        }

        bool canNewExecuteCgen()
        {
            return true;
        }


        void canSaveCgen()
        {
            try
            {
                
                    //if (string.IsNullOrEmpty(CompteGeneSelected.Libelle) && string.IsNullOrEmpty(CompteGeneSelected.Code))
                    //    throw new Exception("un numero est exigé et son code");

                    compteservice.ModelCompteGeneral_Insert(societeCourante.IdSociete, ComptaOhadafreeSelected.Id);
                   
                
               
                CompteGenerals = compteservice.ModelCompteGeneral_SelectAll(societeCourante.IdSociete);
                ComptaOhadasfree = comptaOhadaservice.selectAll_Free(societeCourante.IdSociete);
                IndexCompOhadafree = -1;
                ComptaOhadafreeSelected = null;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION MISE JOUR";
                view.ViewModel.Message = "erreure lors de la création / modification de ce compte";
                view.ShowDialog();
                //   Utils.logUserActions(string.Format("<-- UI Compte --Erreur création compte {0}  : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
               // Utils.logUserActions(string.Format("<-- UI Compte Analytique -- Message : {0} ", ex.Message), "");

            }
        }

        bool canExecuteSaveCgen()
        {
            return ComptaOhadafreeSelected != null ? (ComptaOhadafreeSelected .Id>0?true:false) : false;
        }

        void canDeleteCgen()
        {
            try
            {
             StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION  DE SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer ce Compte ?";
            if (messageBox.ShowDialog().Value == true)
            {
                compteservice.ModelCompteGeneral_Delete(CompteGeneSelected.IdCompteGen);

                CompteGenerals = compteservice.ModelCompteGeneral_SelectAll(societeCourante.IdSociete);
                CompteGeneSelected = null;
            }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION SUPPRESSION Compe General";
                view.ViewModel.Message = ex.Message;// "erreure lors de la création / modification de ce compte";
                view.ShowDialog();
                //   Utils.logUserActions(string.Format("<-- UI Compte --Erreur création compte {0}  : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
                //Utils.logUserActions(string.Format("<-- UI Compte Analytique -- Message : {0} ", ex.Message), "");

            }

        }

        bool canExecuteDeleteCgen()
        {
            return CompteGeneSelected != null ? true : false;
        }
      
        void canSaveFacture()
        {

        }

        bool canExecuteSavefacture()
        {
            return true;
        }



        bool canClose()
        {
            return true;
        }

        #endregion


        #region Region compte analytique



        void canNewCA()
        {
            compteCASelected = new CompteAnalytiqueModel();
            CompteCASelected = compteCASelected;
        }

        void canSaveCA()
        {
            try
            {
                if (!string.IsNullOrEmpty(CompteCASelected.Numerocompte))
                {
                    if (CompteCASelected.IdCompteAnalytique == 0)
                        compteCAservice.ModelCompteGeneral_Insert(CompteCASelected, societeCourante.IdSociete);
                    else
                        compteCAservice.ModelCompteAnal_Update(CompteCASelected);
                    ComptesCA = compteCAservice.ModelCompteAnal_SelectAll(societeCourante.IdSociete);
                    //loadObjet();
                    CompteCASelected = null;
                    //Isoperation = true;
                }
                else MessageBox.Show("le libelle est un champ obligatoire");

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION MISE JOUR";
                view.ViewModel.Message = "erreure lors de la création / modification de ce compte";
                view.ShowDialog();
                //   Utils.logUserActions(string.Format("<-- UI Compte --Erreur création compte {0}  : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
                //Utils.logUserActions(string.Format("<-- UI Compte Analytique -- Message : {0} ", ex.Message), "");

            }
        }

        bool canExecuteSaveCA()
        {
            return CompteCASelected != null ? true : false;
        }

        void canDeleteCA()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION  DE SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer ce Compte ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    compteCAservice.ModelCompteAnal_Delete(CompteCASelected.IdCompteAnalytique);
                    ComptesCA = compteCAservice.ModelCompteAnal_SelectAll(societeCourante.IdSociete);
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();

                    view.Title = "INFORMATION ERREURE MISE JOUR SUPPRESSION";
                    if (ex.Message.Contains("CONSTRAINT"))
                        view.ViewModel.Message = "Impossible de Supprimer ce Compte \n il est déja Attribuer à un Client";
                    else
                        view.ViewModel.Message = " un probleme est survenu lors de la suppression de ce compte , contactez l'administrateur";
                    view.ShowDialog();
                    //Utils.logUserActions(string.Format("<-- UI Compte -- Erreur lors de la Suppression du compte {0}  interface  par : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
                    Utils.logUserActions(string.Format("<-- UI Compte -- message  : {0}", ex.Message), "");
                }
            }

        }

        bool canExecuteCA()
        {
            return true;
        }

        #endregion

        #region Comptabilite
        // edition libelle

        void canSaveComptaliste()
        {
            try
            {
                int id = 0;
                int inCode = 0;
                if (RowComptaListeSelcted != null)
                    id = RowComptaListeSelcted.Id;
                if (!int.TryParse(CodeComptaChamplist, out inCode))
                    throw new Exception("le code Nécessite une valuer numérique");

                ComptabiliteModel.GetComptaGene_Add(id, StringTextComptaChampList, inCode);
                StringTextComptaChampList = string.Empty;
                CodeComptaChamplist =string.Empty ;
                RowComptaListeSelcted = null;
                LoadComptabiliteListe();
                 
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
             

            }

        }

        bool canExecuteSaveComptaList()
        {
            return string.IsNullOrEmpty(StringTextComptaChampList)?false:true;
        }

        void canDeleteComptaliste()
        {
            try
            {
               
                ComptabiliteModel.GetComptaGene_Delete(RowComptaListeSelcted.Id);
                StringTextComptaChampList = string.Empty;
                RowComptaListeSelcted = null;
                LoadComptabiliteListe();
               
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();


            }

        }

        bool canExecuteDeleteComptaList()
        {
            return RowComptaListeSelcted != null ? true : false; ;
        }


        // ajouetr un champ selectionne dans la liste
        void canAddComptaParam()
        {
           // RowComptaListeSelcted

            DataRow row = TableComptaChamparam.NewRow();
            row["ID"] =0;
            row["IdChamps"] = RowComptaListeSelcted.Id;
            row["Libelle"] = RowComptaListeSelcted.Libelle;
            row["etat"] = 0;
            TableComptaChamparam.Rows.Add(row);
            TableComptaChamparam.AcceptChanges();
            TableComptaChamparam = TableComptaChamparam;

            CmbComptaListIndex = -1;
            // filtre 
            var newListe = TableComptaliste.FindAll(c => c.Id != RowComptaListeSelcted.Id);
            //DataView dv = TableComptaliste.DefaultView;
            //dv.RowFilter = string.Format("ID <>'{0}'", RowComptaListeSelcted[0]);
            //DataTable newdtbl = dv.ToTable();
            TableComptaliste = newListe;
        }

        bool canExecuteAddComptaParam()
        {
            return RowComptaListeSelcted !=null ?true :false;
        }

        /// <summary>
        ///   validation du fichier de parametres
        /// </summary>
        void canSaveComptaParam()
        {
            try
            {
                foreach (DataRow row in TableComptaChamparam.Rows)
                {
                    if (Convert.ToInt32(row["etat"]) == 0)
                    ComptabiliteModel.GetComptaGene_Param_Add(Convert.ToInt32(row["ID"]), Convert.ToInt32(row["IdChamps"]), Convert.ToInt32(row["taille"]), Convert.ToInt32(row["Positions"]));
                }
                TableComptaChamparam = ComptabiliteModel.GetComptaGene_Param_Liste();
                isOperation = false;
                // 
            }catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE INFORMATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();


            }
        }

        bool canExecuteSaveComptaParam()
        {
            return isOperation;
        }
        #endregion

        #region Region ohada

        void canAddCmptOhada()
        {
            WmodalcomptaOhada view = new WmodalcomptaOhada(localwindow);
            view.Owner = localwindow;
            view.ShowDialog();
        }
        


        #endregion


        #endregion

        // modification journal vente
        #region Journal vente
        
     
        void canAddJournalV()
        {


            try
            {
                SettingsModel config = new SettingsModel { Code = "codeJnlvente", Libelle = ParametersDatabase.CodeJournalVente, IdSite = societeCourante.IdSociete };
                settingService.Configuration_Add(config);
                MessageBox.Show(" Modification code journal vente  éffectutée");
                // rechargement
                List<SettingsModel> listeconfig = settingService.Configuration_List(societeCourante.IdSociete);
                SettingsModel codejournal = listeconfig.Find(sr => sr.Code == "codeJnlvente");
                if (codejournal != null)
                    ParametersDatabase.CodeJournalVente = codejournal.Libelle;
                GlobalDatas.dataBasparameter.CodeJournalVente = codejournal.Libelle;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREUR MODIFICATION CODE JOURNAL VENTE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
             
            }


        }

        bool canExecuteJournalV()
        {
            bool values = true;
            //if (droitFormulaire != null)
            //{
            //    if (droitFormulaire.Ecriture || droitFormulaire.Super || droitFormulaire.Proprietaire)
            //    {
            //        values = true;
            //    }
            //}
            return values;
        }

        #endregion
    }

    public class ComptaList
    {
        public int Id { get; set; }
        public string  Libelle { get; set; }
        public string Code { get; set; }
    }
}
