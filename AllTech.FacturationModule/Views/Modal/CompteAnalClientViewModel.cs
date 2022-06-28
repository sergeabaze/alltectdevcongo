using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using System.Windows;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;
using System.Windows.Input;
using System.ComponentModel;
using AllTech.FrameWork.Views;

namespace AllTech.FacturationModule.Views.Modal
{
    public class CompteAnalClientViewModel : ViewModelBase
    {

         #region Fields

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand compteAnalListCommand;

        SocieteModel societeCourante;
        CompteAnalClientModel compteservice;
        CompteAnalClientModel compteSelected;
        List<CompteAnalClientModel> comptes;

        CompteAnalytiqueModel compteAnalService;
        List<CompteAnalytiqueModel> compteAnalytiquesList;
        List<CompteAnalytiqueModel> compteAnalytiquesListCache;

     
        CompteAnalytiqueModel compteAnalityqueselected;

      

        Window localwindow;
        int clientselectedId;
        string clientName;

        int compteAnalListIndex;

      
     
        #endregion

        #region Construnctor

      
        public CompteAnalClientViewModel(Window window, int idClient, string nomClient)
        {
            societeCourante = GlobalDatas.DefaultCompany;
            compteservice = new CompteAnalClientModel();
            compteAnalService = new CompteAnalytiqueModel();
            clientselectedId = idClient;
            ClientName = nomClient;
            localwindow = window;

            loadObjet();
        }

        #endregion

        #region properties

        public int CompteAnalListIndex
        {
            get { return compteAnalListIndex; }
            set { compteAnalListIndex = value;
            this.OnPropertyChanged("CompteAnalListIndex");
            }
        }

        public List<CompteAnalytiqueModel> CompteAnalytiquesListCache
        {
            get { return compteAnalytiquesListCache; }
            set { compteAnalytiquesListCache = value;
            this.OnPropertyChanged("CompteAnalytiquesListCache");
            }
        }

        public List<CompteAnalytiqueModel> CompteAnalytiquesList
        {
            get { return compteAnalytiquesList; }
            set { compteAnalytiquesList = value;
            this.OnPropertyChanged("CompteAnalytiquesList");
            }
        }

        public CompteAnalytiqueModel CompteAnalityqueselected
        {
            get { return compteAnalityqueselected; }
            set { compteAnalityqueselected = value;
            if (value != null)
            {
                if (CompteSelected != null)
                {
                    CompteSelected.IdCompteAnal = value.IdCompteAnalytique;
                }
                else
                {
                    CompteSelected = new CompteAnalClientModel();
                    CompteSelected.IdCompteAnal = value.IdCompteAnalytique;
                }
            }
              this.OnPropertyChanged("CompteAnalityqueselected");
            }
        }

        public string ClientName
        {
            get { return clientName; }
            set { clientName = value;
            this.OnPropertyChanged("ClientName");
            }
        }

        public CompteAnalClientModel CompteSelected
        {
            get { return compteSelected; }
            set
            {
                compteSelected = value;
                if (value != null)
                {
                    int i = 0;

                    if (CompteAnalytiquesListCache != null && CompteAnalytiquesListCache.Count > 0)
                    {
                        foreach (var cmpt in CompteAnalytiquesListCache)
                        {
                            if (cmpt.IdCompteAnalytique == value.IdCompteAnal)
                            {
                                CompteAnalListIndex = i;
                                break;
                            }
                            else i++;
                        }
                              
                    }
                }
                this.OnPropertyChanged("CompteSelected");
            }
        }

        public List<CompteAnalClientModel> Comptes
        {
            get { return comptes; }
            set
            {
                comptes = value;
                this.OnPropertyChanged("Comptes");
            }
        }
        #endregion

        #region Commands
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

        public ICommand DeleteCommand
        {
            get
            {
                if (this.deleteCommand == null)
                {
                    this.deleteCommand = new RelayCommand(param => this.canDelete(), param => this.canExecute());
                }
                return this.deleteCommand;
            }


        }

        //

        public ICommand CompteAnalListCommand
        {
            get
            {
                if (this.compteAnalListCommand == null)
                {
                    this.compteAnalListCommand = new RelayCommand(param => this.canShowAnalList(), param => this.canExecuteList());
                }
                return this.compteAnalListCommand;
            }


        }

        #endregion


        #region Methods

        void loadObjet()
        {

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, args) =>
            {
                try
                {
                    List<CompteAnalytiqueModel> listeTotalCompte = null;
                    CompteAnalClientModel cmpt=new CompteAnalClientModel (clientselectedId);
                    Comptes = cmpt.CompteAnalyTiques;
                   
                    listeTotalCompte = compteAnalService.ModelCompteAnal_SelectAll(societeCourante.IdSociete);
                    if (Comptes != null && Comptes.Count > 0)
                    {
                        foreach (CompteAnalClientModel compte in Comptes)
                        {

                            var cmptDelete = listeTotalCompte.FirstOrDefault(c => c.IdCompteAnalytique == compte.CompteAnalyTique.IdCompteAnalytique);
                            if (cmptDelete!=null )
                            listeTotalCompte.Remove(cmptDelete);
                           
                        }
                    }
                    CompteAnalytiquesList = listeTotalCompte;
                    CompteAnalytiquesListCache = compteAnalService.ModelCompteAnal_SelectAll(societeCourante.IdSociete);
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
                    view.Title = "INFORMATION ERREURE CHARGEMENT COMPTE ANALITYQUE/CLIENT";
                    view.Owner = localwindow;
                    view.ViewModel.Message = "Problème survenu lors du chargement des comptes anamityque / clients";
                    view.ShowDialog();
                    // Utils.logUserActions(string.Format("<-- UI Compte -- Erreur lors du chargement des  du comptes    par : {0}","d", "");
                    Utils.logUserActions(string.Format("<-- UI Compte -- message   : {0}", args.Result.ToString()), "");

                }


            };

            worker.RunWorkerAsync();
        }


        private void canNew()
        {
            // if (droitSurFormulaireCurant.Super || droitSurFormulaireCurant.Ecriture || droitSurFormulaireCurant.Proprietaire)
            // {
            compteSelected = new CompteAnalClientModel();
            CompteSelected = compteSelected;

            // IstxtEnabled = true;
            // }
        }


        private void canDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION ERREURE DE SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer ce Compte ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    compteservice.Delete(CompteSelected.Id);
                    //compteService.COMPTE_DELETE(CompteSelected.ID);
                    //CompteList = compteService.COMPTE_SELECT();
                    //CacheDatas.ui_ClientCompte = CompteList;
                    //IstxtEnabled = false;
                    loadObjet();
                    CompteSelected = null;
                    //Utils.logUserActions(string.Format("<-- UI Compte --Suppression du compte {0}  interface  par : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
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
        bool canExecute()
        {
            bool valuesReturn = false;
            //if (droitSurFormulaireCurant.Super || droitSurFormulaireCurant.Suppression || droitSurFormulaireCurant.Proprietaire)
            //{
            if (CompteSelected != null)
                valuesReturn = true;
            else valuesReturn = false;
            //}
            return valuesReturn;
        }

        private void canSave()
        {
            try
            {

                if (CompteSelected.Id == 0)
                {
                    CompteSelected.IdClient = clientselectedId;
                   // CompteSelected.IdCompteAnal = CompteAnalityqueselected.IdCompteAnalytique;
                    compteservice.Insert(CompteSelected);
                }
                else
                {
                  
                    compteservice.Update(CompteSelected);
                   
                }
                loadObjet();
                CompteSelected = null;
                //compteService.COMPTE_ADD(CompteSelected);
                //CompteList = compteService.COMPTE_SELECT();
                //CacheDatas.ui_ClientCompte = CompteList;
                //CompteSelected = null;
                //IstxtEnabled = false;
                //Utils.logUserActions(string.Format("<-- UI Compte --Création du ou misa jour du compte {0}  interface  par : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "INFORMATION MISE JOUR";
                view.ViewModel.Message = "erreure lors de la création / modification de ce compte";
                view.ShowDialog();
                //   Utils.logUserActions(string.Format("<-- UI Compte --Erreur création compte {0}  : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
                Utils.logUserActions(string.Format("<-- UI Compte general -- Message : {0} ", ex.Message), "");

            }
        }
        bool canExecuteSave()
        {
            bool valuesReturn = false;
            // if (droitSurFormulaireCurant.Super || droitSurFormulaireCurant.Ecriture || droitSurFormulaireCurant.Proprietaire)
            // {
            if (CompteSelected != null)
                valuesReturn = true;
            // }
            return valuesReturn;
        }

        void canShowAnalList()
        {
            CompteAnalityques view = new CompteAnalityques();
            view.Owner = localwindow;
            view.ShowDialog();
            if (view.IsOperationAction)
                loadObjet();

        }

        bool canExecuteList()
        {
            return true;
        }
        #endregion
    }
}
