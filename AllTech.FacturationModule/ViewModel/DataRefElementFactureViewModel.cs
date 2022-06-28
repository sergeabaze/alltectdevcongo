using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using Microsoft.Practices.Prism.Events;
using AllTech.FrameWork.Services;

using System.Windows.Input;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Region;
using AllTech.FrameWork.Views;
using System.Windows;
using AllTech.FacturationModule.Views.UCFacture;

namespace AllTech.FacturationModule.ViewModel
{
    public class DataRefElementFactureViewModel : ViewModelBase
    {

        private RelayCommand langueCommand;
        private RelayCommand objetCommand;
        private RelayCommand statutCommand;
        private RelayCommand departementCommand;
        private RelayCommand termePaiementCommand;

        object factureContentRegion;

        int   viewCurrent=0;

        public DataRefElementFactureViewModel()
        {


        }

        #region Proriétés

        public object FactureContentRegion
        {
            get { return factureContentRegion; }
            set
            {
                factureContentRegion = value;
                OnPropertyChanged("FactureContentRegion");
            }
        }
        #endregion

        #region Icommand

        public RelayCommand LangueCommand
        {

            get
            {
                if (this.langueCommand == null)
                {
                    this.langueCommand = new RelayCommand(param => this.canShowLangueUC());
                }
                return this.langueCommand;
            }

        }

        public RelayCommand ObjetCommand
        {

            get
            {
                if (this.objetCommand == null)
                {
                    this.objetCommand = new RelayCommand(param => this.canShowObjetUC());
                }
                return this.objetCommand;
            }

        }

        public RelayCommand StatutCommand
        {

            get
            {
                if (this.statutCommand == null)
                {
                    this.statutCommand = new RelayCommand(param => this.canShowStatutUC());
                }
                return this.statutCommand;
            }

        }

        public RelayCommand DepartementCommand
        {

            get
            {
                if (this.departementCommand == null)
                {
                    this.departementCommand = new RelayCommand(param => this.canShowdepartementUC());
                }
                return this.departementCommand;
            }

        }

        public RelayCommand TermePaiementCommand
        {

            get
            {
                if (this.termePaiementCommand == null)
                {
                    this.termePaiementCommand = new RelayCommand(param => this.canShowTermeUC());
                }
                return this.termePaiementCommand;
            }

        }

        
        #endregion

        #region Methods from icommand

        void canShowLangueUC()
        {
            try
            {
                if (viewCurrent != 1)
                {
                    Uc_langue viewLangue = new Uc_langue();
                    viewCurrent = 1;
                    FactureContentRegion = viewLangue;
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE  LANGUE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void canShowObjetUC()
        {
            try
            {
                if (viewCurrent != 2)
                {
                    Uc_Objet viewLangue = new Uc_Objet();
                    viewCurrent = 2;
                    FactureContentRegion = viewLangue;
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE  OBJET FACTURE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void canShowStatutUC()
        {
            try
            {
                if (viewCurrent != 3)
                {
                    Uc_statut viewLangue = new Uc_statut();
                    viewCurrent = 3;
                    FactureContentRegion = viewLangue;
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE  STATUT FACTURE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void canShowdepartementUC()
        {
            try
            {
                if (viewCurrent != 4)
                {
                    uc_departement viewLangue = new uc_departement();
                    viewCurrent = 4;
                    FactureContentRegion = viewLangue;
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE DEPARTEMENT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        void canShowTermeUC()
        {
            try
            {
                if (viewCurrent != 5)
                {
                    Uc_TermePaiement viewLangue = new Uc_TermePaiement();
                    viewCurrent = 5;
                    FactureContentRegion = viewLangue;
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "ERREURE DE CHARGEMENT VUE TERME PAIEMENT";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }
        }

        //
        #endregion
    }
}
