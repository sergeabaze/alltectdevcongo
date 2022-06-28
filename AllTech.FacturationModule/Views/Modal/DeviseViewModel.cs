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
    public class DeviseViewModel : ViewModelBase
    {
        #region FIELDS
       
        private RelayCommand newDCommand;
        private RelayCommand saveDCommand;
        private RelayCommand deleteDCommand;
        DeviseModel _deviseSelected;
        DeviseModel deviseService;
        List<DeviseModel> deviseList;
        DroitModel _currentDroit;
        UtilisateurModel userConnected;
        SocieteModel societeCourante;
        bool txtEnable;
        Window localWindow;

        #endregion


        public DeviseViewModel(Window window)
        {
          
        
            deviseService = new DeviseModel();
            societeCourante = GlobalDatas.DefaultCompany;
            UserConnected = GlobalDatas.currentUser;
            localWindow = window;
            //if (UserConnected != null)
            //    CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
            //else CurrentDroit = new DroitModel();

            if (CacheDatas.ui_currentdroitClientInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
                CacheDatas.ui_currentdroitClientInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitClientInterface;

            loadDevies();
            _deviseSelected = new DeviseModel();
            DeviseSelected = _deviseSelected;
        }

        #region PROPERTIES


        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set { userConnected = value;
            OnPropertyChanged("UserConnected");
            }
        }
        public DroitModel CurrentDroit
        {
            get { return _currentDroit; }
            set { _currentDroit = value;
            OnPropertyChanged("CurrentDroit");
            }
        }


        public bool TxtEnable
        {
            get { return txtEnable; }
            set { txtEnable = value;
            OnPropertyChanged("TxtEnable");
            }
        }

        public List<DeviseModel> DeviseList
        {
            get { return deviseList; }
            set
            {
                deviseList = value;
                OnPropertyChanged("DeviseList");
            }
        }

        public DeviseModel DeviseSelected
        {
            get { return _deviseSelected; }
            set
            {
                _deviseSelected = value;
                TxtEnable = true;
                OnPropertyChanged("DeviseSelected");
            }
        }
        #endregion

        #region ICOMMAND


        public ICommand SaveDCommand
        {
            get
            {
                if (this.saveDCommand == null)
                {
                    this.saveDCommand = new RelayCommand(param => this.canDSave(), param => this.canDExecuteSave());
                }
                return this.saveDCommand;
            }
        }

        public ICommand NewDCommand
        {
            get
            {
                if (this.newDCommand == null)
                {
                    this.newDCommand = new RelayCommand(param => this.canDNew());
                }
                return this.newDCommand;
            }


        }

        public ICommand DeleteDCommand
        {
            get
            {
                if (this.deleteDCommand == null)
                {
                    this.deleteDCommand = new RelayCommand(param => this.canDDelete(), param => this.canDExecute());
                }
                return this.deleteDCommand;
            }


        }
        #endregion

        #region METHODS

        void loadDevies()
        {
            BackgroundWorker worker = new BackgroundWorker();
            //this.IsBusy = true;
            //ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (CacheDatas.ui_ClientDevises == null)
                    {
                        DeviseList = deviseService.DeviseClient_SELECT(societeCourante.IdSociete);
                        CacheDatas.ui_ClientDevises = DeviseList;
                    }
                    else DeviseList = CacheDatas.ui_ClientDevises;

                   
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
                    view.Owner = localWindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();
                    //this.MouseCursor = null;
                    //this.IsBusy = false;
                }
               
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();

        }


        private void canDDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localWindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = " Suppression de la Dévise?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    deviseService.DeviseClient_DELETE(DeviseSelected.ID_Devise);
                    DeviseList = deviseService.DeviseClient_SELECT(societeCourante.IdSociete);
                    CacheDatas.ui_ClientDevises = DeviseList;
                    DeviseSelected = null;
                    TxtEnable = false;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = localWindow;
                    view.Title = "INFORMATION SUPPRESSION ";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    //IsBusy = false;
                    //this.MouseCursor = null;
                }
            }
        }

        bool canDExecute()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Developpeur || CurrentDroit.Proprietaire )
            {
                if (DeviseSelected != null)
                    if (DeviseSelected.ID_Devise > 0)
                        values = true;
            }
            return values;
        }


        private void canDNew()
        {
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
            {
                _deviseSelected = new DeviseModel();
                TxtEnable = true;
                DeviseSelected = _deviseSelected;
            }
        
        }

        private void canDSave()
        {
            try
            {
                DeviseSelected.IdSite = societeCourante.IdSociete;
                deviseService.DeviseClient_ADD(DeviseSelected);
                DeviseList = deviseService.DeviseClient_SELECT(societeCourante.IdSociete);
                CacheDatas.ui_ClientDevises = DeviseList;
                DeviseSelected = null;
                TxtEnable = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localWindow;
                view.Title = "INFORMATION MESSAGE ADD";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                //IsBusy = false;
                //this.MouseCursor = null;
            }
        }




        bool canDExecuteSave()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
            {
                if (DeviseSelected != null)
                        values = true;
            }
            return values;

           
        }

        #endregion
    }
}
