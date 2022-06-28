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
using System.Collections.ObjectModel;
using System.Data;
using AllTech.FacturationModule.Views.Modal;
using System.Threading;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views.Modal
{
  public   class ExonerationViewModel : ViewModelBase
    {
        #region FIELDS
        
      
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;

        private RelayCommand newExoCommand;
        private RelayCommand saveExoCommand;
        private RelayCommand deleteExoCommand;

      
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;
        UtilisateurModel userConnected;
        SocieteModel societeCourante;

        bool btnNewVisible;
       public  bool IsAction = false;

     
        ExonerationModel exonereService;
        ExonerationModel exonereCourant;
        List<ExonerationModel> exonereList;
        Window localwindow;
        #endregion

        public ExonerationViewModel(Window window)
        {
        
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            localwindow = window;
            //if (UserConnected != null)
            //    CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
            //else CurrentDroit = new DroitModel();

            if (CacheDatas.ui_currentdroitClientInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
                CacheDatas.ui_currentdroitClientInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitClientInterface;

            societeCourante = GlobalDatas.DefaultCompany;
            exonereService = new ExonerationModel();
          
            LoadExonerer();
            exonereCourant = new ExonerationModel();
            ExonereCourant = exonereCourant;
        }


        #region PROPERTIES

        public DroitModel CurrentDroit
        {
            get { return _currentDroit; }
            set
            {
                _currentDroit = value;
                OnPropertyChanged("CurrentDroit");
            }
        }

        public bool BtnNewVisible
        {
            get { return btnNewVisible; }
            set { btnNewVisible = value;
            OnPropertyChanged("BtnNewVisible");
            }
        }

        public ParametresModel ParametersDatabase
        {
            get { return _parametersDatabase; }
            set
            {
                _parametersDatabase = value;
                OnPropertyChanged("ParametersDatabase");
            }
        }

        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set
            {
                userConnected = value;
                OnPropertyChanged("UserConnected");
            }
        }

        public ExonerationModel ExonereCourant
        {
            get { return exonereCourant; }
            set
            {
                exonereCourant = value;
                BtnNewVisible = true;
                OnPropertyChanged("ExonereCourant");

            }
        }


        public List<ExonerationModel> ExonereList
        {
            get { return exonereList; }
            set
            {
                exonereList = value;
                OnPropertyChanged("ExonereList");

            }
        }
        #endregion

        #region ICOMMAND

        public ICommand SaveExoCommand
        {
            get
            {
                if (this.saveExoCommand == null)
                {
                    this.saveExoCommand = new RelayCommand(param => this.canSaveExoner(), param => this.canExecuteSaveExoner());
                }
                return this.saveExoCommand;
            }
        }

        public ICommand NewExoCommand
        {
            get
            {
                if (this.newExoCommand == null)
                {
                    this.newExoCommand = new RelayCommand(param => this.canNewExoner());
                }
                return this.newExoCommand;
            }
        }

        public ICommand DeleteExoCommand
        {
            get
            {
                if (this.deleteExoCommand == null)
                {
                    this.deleteExoCommand = new RelayCommand(param => this.canDeleteExoner(), param => this.canExecuteDeleteExoner());
                }
                return this.deleteExoCommand;
            }
        }
        #endregion

        #region METHODS

      


        void LoadExonerer()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (societeCourante != null)
                    {
                        if (CacheDatas.ui_ClientExonerations == null)
                        {
                            ExonereList = exonereService.EXONERATION_SELECT();
                            CacheDatas.ui_ClientExonerations = ExonereList;
                        }
                        else ExonereList = CacheDatas.ui_ClientExonerations;

                       
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
                    view.Owner = localwindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();

                }


            };

            worker.RunWorkerAsync();
        }


        private void canNewExoner()
        {
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
            {
                exonereCourant = new ExonerationModel();
                ExonereCourant = exonereCourant;
                BtnNewVisible = true;
            }
        }


        private void canSaveExoner()
        {
            try
            {
                exonereService.EXONERATION_ADD(ExonereCourant);
                ExonereList = exonereService.EXONERATION_SELECT();
                CacheDatas.ui_ClientExonerations = ExonereList;
                ExonereCourant = null;
                BtnNewVisible = false;
                IsAction = true;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               view.Owner = localwindow;
                view.Title = "Information De Sauvegarde";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecuteSaveExoner()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
            {
                if (ExonereCourant != null)
                    values = true;
            }
            return values;
        }

        private void canDeleteExoner()
        {
           
                 StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = " Suppression de l'exonération ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    exonereService.EXONERATION_DELETE(ExonereCourant.ID);
                    ExonereList = exonereService.EXONERATION_SELECT();
                    CacheDatas.ui_ClientExonerations = ExonereList;
                    ExonereCourant = null;
                    BtnNewVisible = false;
                    IsAction = true;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localwindow;
                    view.Title = "Information De Sauvegarde";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();

                }

            }
           
        
        }

        bool canExecuteDeleteExoner()
        {
               bool values = false;
               if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Proprietaire)
               {
                   if (ExonereCourant != null)
                       if (ExonereCourant.ID > 0)
                           values = true;
               }
               return values;
        }

        #endregion
    }



}
