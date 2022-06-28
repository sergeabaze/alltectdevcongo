using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Global;
using System.Windows.Input;
using System.ComponentModel;
using AllTech.FrameWork.Views;
using System.Windows;


namespace AllTech.FacturationModule.Views.Modal
{
    public class CompteGeneViewModel : ViewModelBase
    {

        #region Fields

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

        SocieteModel societeCourante;
        CompteGenralModel compteservice;
        CompteGenralModel compteGeneSelected;
        List<CompteGenralModel> compteGenerals;
        UtilisateurModel userConnected;
        Window localwindow;
        public bool IsOperation = false;

        #endregion

        public CompteGeneViewModel(Window window)
        {
            societeCourante = GlobalDatas.DefaultCompany;
            UserConnected = GlobalDatas.currentUser;
            compteservice = new CompteGenralModel();
            localwindow = window;

            loadObjet();
        }

        #region REGION PROPERTIES

        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set
            {
                userConnected = value;
                this.OnPropertyChanged("UserConnected");
            }
        }

        public CompteGenralModel CompteGeneSelected
        {
            get { return compteGeneSelected; }
            set { compteGeneSelected = value;
            this.OnPropertyChanged("CompteGeneSelected");
            }
        }

        public List<CompteGenralModel> CompteGenerals
        {
            get { return compteGenerals; }
            set { compteGenerals = value;
            this.OnPropertyChanged("CompteGenerals");
            }
        }

        #endregion

        #region REGION COMAND

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

        #region REGION METHODS

        void loadObjet()
        {

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, args) =>
            {
                try
                {
                    CompteGenerals = compteservice.ModelCompteGeneral_SelectAll(societeCourante.IdSociete);
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


        private void canNew()
        {
           // if (droitSurFormulaireCurant.Super || droitSurFormulaireCurant.Ecriture || droitSurFormulaireCurant.Proprietaire)
           // {
                compteGeneSelected = new  CompteGenralModel ();
                CompteGeneSelected = compteGeneSelected;

               // IstxtEnabled = true;
           // }
        }


        private void canDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION DE SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer ce Compte ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    compteservice.ModelCompteGeneral_Delete(CompteGeneSelected.IdCompteGen);
                    //compteService.COMPTE_DELETE(CompteSelected.ID);
                    //CompteList = compteService.COMPTE_SELECT();
                    //CacheDatas.ui_ClientCompte = CompteList;
                    //IstxtEnabled = false;
                    loadObjet();
                    CompteGeneSelected = null;
                    IsOperation = true;
                   // Utils.logUserActions(string.Format("<-- UI Compte --Suppression du compte {0}  interface  par : {1}", CompteGeneSelected.Libelle, UserConnected.Loggin), "");
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();

                    view.Title = "INFORMATION ERREURE  SUPPRESSION";
                    if (ex.Message.Contains("CONSTRAINT"))
                        view.ViewModel.Message = "Impossible de Supprimer ce Compte \n il est déja Attribuer à un Client";
                    else
                        view.ViewModel.Message = " un probleme est survenu lors de la suppression de ce compte , contactez l'administrateur";
                    view.ShowDialog();
                    //Utils.logUserActions(string.Format("<-- UI Compte -- Erreur lors de la Suppression du compte {0}  interface  par : {1}", CompteSelected.NumeroCompte, UserConnected.Loggin), "");
                    //Utils.logUserActions(string.Format("<-- UI Compte -- message  : {0}", ex.Message), "");
                }
            }
        }
        bool canExecute()
        {
            bool valuesReturn = false;
            //if (droitSurFormulaireCurant.Super || droitSurFormulaireCurant.Suppression || droitSurFormulaireCurant.Proprietaire)
            //{
            if (compteGeneSelected != null)
                    valuesReturn = true;
                else valuesReturn = false;
            //}
            return valuesReturn;
        }

        private void canSave()
        {
            try
            {
                //if (CompteGeneSelected.IdCompteGen == 0)
                //    compteservice.ModelCompteGeneral_Insert(CompteGeneSelected, societeCourante.IdSociete);
                //else
                //    compteservice.ModelCompteGeneral_Update(CompteGeneSelected);
                //loadObjet();
                //CompteGeneSelected = null;
                IsOperation = true;
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
            if (CompteGeneSelected != null)
                    valuesReturn = true;
           // }
            return valuesReturn;
        }
        #endregion
    }
}
