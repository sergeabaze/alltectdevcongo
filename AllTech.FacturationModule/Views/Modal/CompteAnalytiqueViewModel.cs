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
    public class CompteAnalytiqueViewModel : ViewModelBase
    {

        #region Fields

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

        SocieteModel societeCourante;
        CompteAnalytiqueModel compteservice;
        CompteAnalytiqueModel compteSelected;
        List<CompteAnalytiqueModel> comptes;

        Window localwindow;

        public bool Isoperation = false;

        #endregion

        #region Construnctor

       
        public CompteAnalytiqueViewModel(Window window)
        {
            societeCourante = GlobalDatas.DefaultCompany;
            compteservice = new CompteAnalytiqueModel();
            localwindow = window;

            loadObjet();
        }

        #endregion

        #region properties

        public CompteAnalytiqueModel CompteSelected
        {
            get { return compteSelected; }
            set
            {
                compteSelected = value;
                this.OnPropertyChanged("CompteSelected");
            }
        }

        public List<CompteAnalytiqueModel> Comptes
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
        #endregion


        #region Methods

        void loadObjet()
        {

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, args) =>
            {
                try
                {
                    Comptes = compteservice.ModelCompteAnal_SelectAll(societeCourante.IdSociete);
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
                    view.Title = "INFORMATION ERREURE CHARGEMENT COMPTE ANALITYQUE";
                    view.Owner = localwindow;
                    view.ViewModel.Message = "Problème survenu lors du chargement des comptes Analytiques";
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
            compteSelected =new CompteAnalytiqueModel ();
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
                    compteservice.ModelCompteAnal_Delete(CompteSelected.IdCompteAnalytique);
                    //compteService.COMPTE_DELETE(CompteSelected.ID);
                    //CompteList = compteService.COMPTE_SELECT();
                    //CacheDatas.ui_ClientCompte = CompteList;
                    //IstxtEnabled = false;
                    loadObjet();
                    CompteSelected = null;
                    Isoperation = true;
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
                if (!string.IsNullOrEmpty(CompteSelected.Numerocompte))
                {
                    if (CompteSelected.IdCompteAnalytique == 0)
                        compteservice.ModelCompteGeneral_Insert(CompteSelected, societeCourante.IdSociete);
                    else
                        compteservice.ModelCompteAnal_Update(CompteSelected);
                    loadObjet();
                    CompteSelected = null;
                    Isoperation = true;
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
        #endregion

    }
}
