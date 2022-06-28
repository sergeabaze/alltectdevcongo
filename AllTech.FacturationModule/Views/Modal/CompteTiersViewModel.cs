using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Model;
using System.Windows;
using AllTech.FrameWork.Global;
using System.Windows.Input;
using System.ComponentModel;
using AllTech.FrameWork.Views;

namespace AllTech.FacturationModule.Views.Modal
{
    public class CompteTiersViewModel : ViewModelBase
    {
        #region Fields

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

        SocieteModel societeCourante;
        CompteTiersModel compteservice;
        CompteTiersModel compteGeneSelected;
        List<CompteTiersModel> compteGenerals;

        Window localwindow;
        private bool isOperation = false;

      
        int idClient = 0;

      
        #endregion

        public CompteTiersViewModel(Window window, int clientId)
        {
            societeCourante = GlobalDatas.DefaultCompany;
            compteservice = new CompteTiersModel();
            localwindow = window;
            idClient = clientId;


            loadObjet();
        }


        #region REGION PROPERTIES

        public bool IsOperation
        {
            get { return isOperation; }
            set { isOperation = value;
            this.OnPropertyChanged("IsOperation");
            }
        }

        public CompteTiersModel CompteGeneSelected
        {
            get { return compteGeneSelected; }
            set
            {
                compteGeneSelected = value;
                this.OnPropertyChanged("CompteGeneSelected");
            }
        }

        public List<CompteTiersModel> CompteGenerals
        {
            get { return compteGenerals; }
            set
            {
                compteGenerals = value;
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

        #region Methods


        void loadObjet()
        {

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, args) =>
            {
                try
                {
                    if(!GlobalDatas.IdDataRefArchiveDatas)
                    CompteGenerals = compteservice.SelectByclient(societeCourante.IdSociete, idClient);
                    else
                        CompteGenerals = compteservice.SelectByclient_Archive(societeCourante.IdSociete, idClient);
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
            compteGeneSelected = new CompteTiersModel();
            CompteGeneSelected = compteGeneSelected;

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
                    compteservice.Delete(CompteGeneSelected.IdCompteT);
                   
                    loadObjet();
                    CompteGeneSelected = null;
                    IsOperation = true;
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
                CompteGeneSelected.IdClient = idClient;
                if (!string.IsNullOrEmpty(CompteGeneSelected.NumeroCompte))
                {
                    if (!GlobalDatas.IdDataRefArchiveDatas)
                    {
                        if (CompteGeneSelected.IdCompteT == 0)
                            compteservice.Insert(CompteGeneSelected, societeCourante.IdSociete);
                        else
                        {

                            compteservice.Update(CompteGeneSelected);

                        }
                    }else
                           compteservice.Update_Archive(CompteGeneSelected);

                    loadObjet();
                    CompteGeneSelected = null;
                    IsOperation = true;
                }
               
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
