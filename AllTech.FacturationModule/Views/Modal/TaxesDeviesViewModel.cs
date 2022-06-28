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
   public  class TaxesDeviesViewModel:ViewModelBase 
    {

        public IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
     
       

        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

        private RelayCommand newDCommand;
        private RelayCommand saveDCommand;
        private RelayCommand deleteDCommand;
       
        private RelayCommand newLCommand;
        private RelayCommand saveLCommand;
        private RelayCommand deleteLCommand;
        private RelayCommand closeCommand;

        TaxeModel taxeService;
        TaxeModel _taxeSelected;
        List<TaxeModel> _taxeList;

       
        DeviseModel _deviseSelected;
        DeviseModel deviseService;
        List<DeviseModel> deviseList;

        LangueModel _language;
        ObservableCollection<LangueModel> _languageList;
        LangueModel _languageSelected;
        SocieteModel societeCourante;
        DroitModel _currentDroit;
        UtilisateurModel userConnected;
        Window loacalwindow;
        public bool isAction = false;

        public TaxesDeviesViewModel(Window window)
       {

         
          ProgressBarVisibility = false;
          loacalwindow = window;
          taxeService = new TaxeModel();
          deviseService = new DeviseModel();
          _language = new LangueModel();
          societeCourante = GlobalDatas.DefaultCompany;
          UserConnected = GlobalDatas.currentUser;
         
          if (CacheDatas.ui_currentdroitClientInterface == null)
          {
              CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
              CacheDatas.ui_currentdroitClientInterface = CurrentDroit;
          }
          else CurrentDroit = CacheDatas.ui_currentdroitClientInterface;

          loadDevies();
          loadTaxes();
          LoadLanguages();
       }

        #region PROPERTIES


        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set
            {
                userConnected = value;
                OnPropertyChanged("UserConnected");
            }
        }
        public DroitModel CurrentDroit
        {
            get { return _currentDroit; }
            set
            {
                _currentDroit = value;
                OnPropertyChanged("CurrentDroit");
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
                OnPropertyChanged("ProgressBarVisibility");
            }
        }


        public List<DeviseModel> DeviseList
        {
            get { return deviseList; }
            set { deviseList = value;
            OnPropertyChanged("DeviseList");
            }
        }

        public DeviseModel DeviseSelected
        {
            get { return _deviseSelected; }
            set { _deviseSelected = value;
            OnPropertyChanged("DeviseSelected");
            }
        }

        public TaxeModel TaxeSelected
        {
            get { return _taxeSelected; }
            set { _taxeSelected = value;
            OnPropertyChanged("TaxeSelected");
            }
        }

        public List<TaxeModel> TaxeList
        {
            get { return _taxeList; }
            set { _taxeList = value;
            OnPropertyChanged("TaxeList");
            }
        }

        public LangueModel LanguageSelected
        {
            get { return _languageSelected; }
            set
            {
                _languageSelected = value;
               // if (value != null)
                   // loadDatasModel(value.Id);
                this.OnPropertyChanged("LanguageSelected");
            }
        }

        public ObservableCollection<LangueModel> LanguageList
        {
            get { return _languageList; }
            set
            {
                _languageList = value;
                this.OnPropertyChanged("LanguageList");
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

       //Devises command

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

       

        public ICommand SaveLCommand
        {
            get
            {
                if (this.saveLCommand == null)
                {
                    this.saveLCommand = new RelayCommand(param => this.canLSave(), param => this.canLExecuteSave());
                }
                return this.saveLCommand;
            }
        }

        public ICommand DeleteLCommand
        {
            get
            {
                if (this.deleteLCommand == null)
                {
                    this.deleteLCommand = new RelayCommand(param => this.canLDelete(), param => this.canLExecute());
                }
                return this.deleteLCommand;
            }
        }

        public ICommand NewLCommand
        {
            get
            {
                if (this.newLCommand == null)
                {
                    this.newLCommand = new RelayCommand(param => this.canLNew());
                }
                return this.newLCommand;
            }


        }
        #endregion


      
        #region METHODS


        void loadTaxes()
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (CacheDatas.ui_ClientTaxes == null)
                    {
                        TaxeList = taxeService.Taxe_SELECT(0, societeCourante.IdSociete);
                        CacheDatas.ui_ClientTaxes = TaxeList;
                    }
                    else TaxeList = CacheDatas.ui_ClientTaxes;


                    
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
                    view.Owner = loacalwindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();

        }

        void loadDevies()
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (CacheDatas.ui_ClientDevises == null)
                    {
                        DeviseList = deviseService.Devise_SELECT(societeCourante.IdSociete);
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
                    view.Owner = loacalwindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();

        }
          void LoadLanguages()
         {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                    LanguageList = _language.LANGUE_SELECT(0);

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
                    view.Owner = loacalwindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();

        }

        private void canClose()
        {
        }

        private void canDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
           messageBox.Owner = loacalwindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer cette taxe?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    taxeService.Taxe_DELETE(TaxeSelected.ID_Taxe);
                   // loadTaxes();
                    TaxeList = taxeService.Taxe_SELECT(0, societeCourante.IdSociete);
                    CacheDatas.ui_ClientTaxes = TaxeList;
                    TaxeSelected = null;
                    isAction = true;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = loacalwindow;
                    view.Title = "INFORMATION SUPPRESSION";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }
        }

        bool canExecute()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
            {
                if (TaxeSelected != null)
                    if (TaxeSelected.ID_Taxe>0)
                    values = true;
            }
            return values;

           
        }


        private void canNew()
        {
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
            {
                _taxeSelected = new TaxeModel();
                TaxeSelected = _taxeSelected;
            }
        }

        private void canSave()
        {
            try
            {
                TaxeSelected.IdSite = societeCourante.IdSociete;
                taxeService.Taxe_ADD(TaxeSelected);
                //loadTaxes();
                TaxeList = taxeService.Taxe_SELECT(0, societeCourante.IdSociete);
                CacheDatas.ui_ClientTaxes = TaxeList;
                TaxeSelected = null;
                isAction = true;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = loacalwindow;
                view.Title = "Warning Message Add Taxe";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecuteSave()
        {

            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
            {
                if (TaxeSelected != null)
                        values = true;
            }
            return values;
           
        }

       //devises Methods --------------------------------
        

        private void canDDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = loacalwindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = " Suppression de la taxe?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    deviseService.Devise_DELETE(DeviseSelected.ID_Devise);
                    //loadDevies();
                    DeviseList = deviseService.Devise_SELECT(societeCourante.IdSociete);
                    CacheDatas.ui_ClientDevises = DeviseList;
                    DeviseSelected = null;
                    isAction = true;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = loacalwindow;
                    view.Title = "INFORMATION SUPPRESSION ";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }
        }

        bool canDExecute()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
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
                DeviseSelected = _deviseSelected;
            }
        }

        private void canDSave()
        {
            try
            {
                DeviseSelected.IdSite = societeCourante.IdSociete;
                deviseService.Devise_ADD(DeviseSelected);
                //loadDevies();
                DeviseList = deviseService.Devise_SELECT(societeCourante.IdSociete);
                CacheDatas.ui_ClientDevises = DeviseList;
                DeviseSelected = null;
                isAction = true;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = loacalwindow;
                view.Title = "Warning Message Add Taxe";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
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

        //******************************** language methods


        private void canLNew()
        {
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur || CurrentDroit.Proprietaire )
            {
                _languageSelected = new LangueModel();
                LanguageSelected = _languageSelected;
            }
        }
        private void canLSave()
        {
            try
            {
                _language.LANGUE_ADD(LanguageSelected);
                LoadLanguages();
                LanguageSelected = null;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = loacalwindow;
                view.Title = "Warning Message Add Taxe";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canLExecuteSave()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
            {
                if (LanguageSelected != null)
                    values = true;
            }
            return values;

           
        }


        private void canLDelete()
        {
             StyledMessageBoxView messageBox = new StyledMessageBoxView();
           // messageBox.Owner = loacalwindow;
            messageBox.Title = "Delete Language Informations";
            messageBox.ViewModel.Message = "Are you sure you want to Delete this ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    _language.LANGUE_DELETE(LanguageSelected.Id);
                    LanguageSelected = null;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                   // view.Owner = loacalwindow;
                    view.Title = "Warning Message Delete Devise";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }

        }

        bool canLExecute()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Developpeur || CurrentDroit.Proprietaire)
            {
                if (LanguageSelected != null)
                    values = true;
            }
            return values;
           
        }

        #endregion
    }
}
