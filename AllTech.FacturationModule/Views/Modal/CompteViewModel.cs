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

namespace AllTech.FacturationModule.Views.Modal
{
    public class CompteViewModel : ViewModelBase
    {
        #region FIELDS
       
        public IRegionManager _regionManager;
       // private IInjectSingleViewService _injectSingleViewService;

        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;
        UtilisateurModel UserConnected = null;
        DroitModel droitSurFormulaireCurant=null ;

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

        SocieteModel societeCourante;
        CompteModel compteService;
        CompteModel compteSelected;
        List<CompteModel> compteList;

        bool istxtEnabled;
        Window localwindow;
        #endregion

        public CompteViewModel(Window window)
        {
            compteService = new CompteModel();
            societeCourante = GlobalDatas.DefaultCompany;
            UserConnected = GlobalDatas.currentUser;
            localwindow = window;
            if (CacheDatas.ui_currentdroitClientInterface == null)
            {
                droitSurFormulaireCurant = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
                CacheDatas.ui_currentdroitClientInterface = droitSurFormulaireCurant;
            }
            else droitSurFormulaireCurant=CacheDatas.ui_currentdroitClientInterface ;

            loadObjet();
           // Utils.logUserActions(string.Format("<-- UI Compte --ouverture  interface  par : {0}", UserConnected.Loggin), "");
            compteSelected = new CompteModel();
            CompteSelected = compteSelected;
        }


        #region PROPRIETES

        public CompteModel CompteSelected
        {
            get { return compteSelected; }
            set { compteSelected = value;
            IstxtEnabled = true;
            this.OnPropertyChanged("CompteSelected");
            }
        }

          public bool IstxtEnabled
        {
            get { return istxtEnabled; }
            set { istxtEnabled = value;
            this.OnPropertyChanged("IstxtEnabled");
            }
        }


        public List<CompteModel> CompteList
        {
            get { return compteList; }
            set { compteList = value;
            this.OnPropertyChanged("CompteList");
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
        #endregion


        #region METHODS

        void loadObjet()
        {

            BackgroundWorker worker = new BackgroundWorker();
           
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (societeCourante != null)
                    {
                        if (CacheDatas.ui_ClientCompte == null)
                        {
                            CompteList = compteService.COMPTE_SELECT();
                            CacheDatas.ui_ClientCompte = CompteList;
                        }
                        else CompteList = CacheDatas.ui_ClientCompte;
                        
                    }
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
                    view.Title = "INFORMATION ERREURE CHARGEMENT COMPTES";
                    view.Owner = localwindow;
                    view.ViewModel.Message ="Problème survenu lors du chargement des comptes";
                    view.ShowDialog();
                    Utils.logUserActions(string.Format("<-- UI Compte -- Erreur lors du chargement des  du comptes    par : {0}", UserConnected.Loggin), "");
                    Utils.logUserActions(string.Format("<-- UI Compte -- message   : {0}", args.Result.ToString()), "");
                    
                }
               

            };

            worker.RunWorkerAsync();
        }


        private void canNew()
        {
            if (droitSurFormulaireCurant.Super || droitSurFormulaireCurant.Ecriture || droitSurFormulaireCurant.Proprietaire )
            {
                compteSelected = new CompteModel();
                CompteSelected = compteSelected;

                IstxtEnabled = true;
            }
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
                    compteService.COMPTE_DELETE(CompteSelected.ID);
                    CompteList = compteService.COMPTE_SELECT();
                    CacheDatas.ui_ClientCompte = CompteList;
                    IstxtEnabled = false;
                    CompteSelected = null;
                    Utils.logUserActions(string.Format("<-- UI Compte --Suppression du compte {0}  interface  par : {1}",CompteSelected.NumeroCompte, UserConnected.Loggin), "");
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    
                    view.Title = "INFORMATION ERREURE MISE JOUR SUPPRESSION";
                    if (ex.Message.Contains("CONSTRAINT"))
                        view.ViewModel.Message = "Impossible de Supprimer ce Compte \n il est déja Attribuer à un Client";
                    else 
                    view.ViewModel.Message =" un probleme est survenu lors de la suppression de ce compte , contactez l'administrateur";
                    view.ShowDialog();
                    Utils.logUserActions(string.Format("<-- UI Compte -- Erreur lors de la Suppression du compte {0}  interface  par : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
                    Utils.logUserActions(string.Format("<-- UI Compte -- message  : {0}", ex.Message ), "");
                }
            }
        }
        bool canExecute()
        {
            bool valuesReturn = false;
            if (droitSurFormulaireCurant.Super || droitSurFormulaireCurant.Suppression || droitSurFormulaireCurant.Proprietaire)
            {
               if ( CompteSelected != null) 
                  valuesReturn = true;
                else   valuesReturn = false ;
            }
            return valuesReturn;
        }

        private void canSave()
        {
            try
            {
                compteService.COMPTE_ADD(CompteSelected);
                CompteList = compteService.COMPTE_SELECT();
                CacheDatas.ui_ClientCompte = CompteList;
                CompteSelected = null;
                IstxtEnabled = false;
               // Utils.logUserActions(string.Format("<-- UI Compte --Création du ou misa jour du compte {0}  interface  par : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               view.Owner = localwindow;
                view.Title = "INFORMATION MISE JOUR";
                view.ViewModel.Message ="erreure lors de la création / modification de ce compte";
                view.ShowDialog();
                Utils.logUserActions(string.Format("<-- UI Compte --Erreur création compte {0}  : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
                Utils.logUserActions(string.Format("<-- UI Compte -- Message : {0} ", ex.Message ), "");
              
            }
        }
        bool canExecuteSave()
        {
            bool valuesReturn = false;
            if (droitSurFormulaireCurant.Super || droitSurFormulaireCurant.Ecriture || droitSurFormulaireCurant.Proprietaire)
            {
                if (CompteSelected != null)
                    valuesReturn = true;
            }
            return valuesReturn;
        }
        #endregion
    }
}
