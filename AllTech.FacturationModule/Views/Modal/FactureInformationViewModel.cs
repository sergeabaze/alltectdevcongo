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
    public class FactureInformationViewModel : ViewModelBase
    {
        public IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
       private   IUnityContainer _container;
      

        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
      

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

        private RelayCommand newDCommand;
        private RelayCommand saveDCommand;
        private RelayCommand deleteDCommand;

        private RelayCommand closeCommand;


        LangueModel _language;
        ObservableCollection<LangueModel> _languageList;
        LangueModel _languageSelected;

        ObjetFactureModel objetservice;
        ObjetFactureModel _objetselected;
        ObservableCollection<ObjetFactureModel> _objetList;

        ExploitationFactureModel _exploitService;
        ExploitationFactureModel _exploitSelected;
        ObservableCollection<ExploitationFactureModel> _exploitList;
        SocieteModel societeCourante;
        bool istxtEnabled;

       public FactureInformationViewModel(IRegionManager regionManager, IUnityContainer container)
       {
       
           _regionManager = regionManager;
           _container = container;
          ProgressBarVisibility = false;
          societeCourante = GlobalDatas.DefaultCompany;
          _language = new LangueModel();
          _exploitService = new ExploitationFactureModel();
          objetservice = new ObjetFactureModel();

          loadlanguage();
          loadObjet();
          loadexploit();
       }


        #region PROPERTIES


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

       public bool IstxtEnabled
       {
           get { return istxtEnabled; }
           set { istxtEnabled = value;
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


       public LangueModel LanguageSelected
       {
           get { return _languageSelected; }
           set
           {
               _languageSelected = value;
               if (value != null)
               {
                   if (ExploitSelected != null)
                       ExploitSelected.IdLangue = value.Id;
                   //if (Objetselected != null)
                   //    Objetselected.IdLangue = value.Id;
               }
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


       public ObjetFactureModel Objetselected
       {
           get { return _objetselected; }
           set { _objetselected = value;
           IstxtEnabled = true;
           this.OnPropertyChanged("Objetselected");
           }
       }


       public ObservableCollection<ObjetFactureModel> ObjetList
       {
           get { return _objetList; }
           set { _objetList = value;
           this.OnPropertyChanged("ObjetList");
           }
       }

       public ExploitationFactureModel ExploitSelected
       {
           get { return _exploitSelected; }
           set { _exploitSelected = value ;
           this.OnPropertyChanged("ExploitSelected");
           }
       }

       public ObservableCollection<ExploitationFactureModel> ExploitList
       {
           get { return _exploitList; }
           set { _exploitList = value;
           this.OnPropertyChanged("ExploitList");
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
        #endregion


        #region METHODS

       void loadlanguage()
       {
           BackgroundWorker worker = new BackgroundWorker();
           this.IsBusy = true;
           ProgressBarVisibility = true;
           worker.DoWork += (o, args) =>
           {
               try
               {
                   LanguageList  =_language .LANGUE_SELECT (0);
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
                   view.Owner = Application.Current.MainWindow;
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

           };

           worker.RunWorkerAsync();
       }

       #region Objet facture

       void loadObjet()
       {

           BackgroundWorker worker = new BackgroundWorker();
           this.IsBusy = true;
           ProgressBarVisibility = true;
           worker.DoWork += (o, args) =>
           {
               try
               {
                   ObjetList = objetservice.OBJECT_FACTURE_GETLISTE(societeCourante.IdSociete );
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
                  // view.Owner = Application.Current.MainWindow;
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
              
           };

           worker.RunWorkerAsync();
       }


       private void canNew()
       {
           _objetselected = new ObjetFactureModel();
           Objetselected = _objetselected;
           IstxtEnabled = true;
       }
      

       private void canDelete()
       {
           StyledMessageBoxView messageBox = new StyledMessageBoxView();
           //messageBox.Owner = Application.Current.MainWindow;
           messageBox.Title = "Delete Objet invoce Informations";
           messageBox.ViewModel.Message = "Are you sure you want to Delete this ?";
           if (messageBox.ShowDialog().Value == true)
           {
               try
               {
                   IsBusy = true;
                   objetservice.OBJECT_FACTURE_DELETE(Objetselected.IdObjet);
                   loadObjet();
                   Objetselected = null;
                   IsBusy = false;
                   IstxtEnabled = false;
               }
               catch (Exception ex)
               {
                   CustomExceptionView view = new CustomExceptionView();
                   //view.Owner = Application.Current.MainWindow;
                   view.Title = "Warning Message Delete Taxes";
                   view.ViewModel.Message = ex.Message;
                   view.ShowDialog();
                   IsBusy = false;
                   this.MouseCursor = null;
               }
           }
       }
       bool canExecute()
       {
           return true;
       }

       private void canSave()
       {
           try
           {
               IsBusy = true;
               if (Objetselected.IdObjet == 0)
               {
                   //if (Objetselected.IdLangue != 0)
                   //{
                       objetservice.OBJECT_FACTURE_ADD(Objetselected,societeCourante .IdSociete );
                       loadObjet();
                       Objetselected = null;
                       IsBusy = false;
                       IstxtEnabled = false;
                       LanguageSelected = null;
                       LanguageList = null;
                       LanguageList = _language.LANGUE_SELECT(0);
                   //}
                   //else
                   //    MessageBox.Show("Selected language");
               }
               else
               {
                   objetservice.OBJECT_FACTURE_ADD(Objetselected,societeCourante .IdSociete );
                   loadObjet();
                   Objetselected = null;
                   IstxtEnabled = false;
                   IsBusy = false;
                   LanguageSelected = null;
                   LanguageList = null;
                   LanguageList = _language.LANGUE_SELECT(0);
               }
           }
           catch (Exception ex)
           {
               CustomExceptionView view = new CustomExceptionView();
               //view.Owner = Application.Current.MainWindow;
               view.Title = "Warning Message Add Objet invoice";
               view.ViewModel.Message = ex.Message;
               view.ShowDialog();
               IsBusy = false;
               this.MouseCursor = null;
           }
       }
       bool canExecuteSave()
       {
           return Objetselected!=null ?true :false ;
       }
       #endregion


       #region exploitataion facture


       void loadexploit()
       {

           BackgroundWorker worker = new BackgroundWorker();
           this.IsBusy = true;
           ProgressBarVisibility = true;
           worker.DoWork += (o, args) =>
           {
               try
               {
                   ExploitList = _exploitService.EXPLOITATION_FACTURE_GETLISTE(societeCourante .IdSociete);
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
                   //view.Owner = Application.Current.MainWindow;
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

           };

           worker.RunWorkerAsync();
       }


       private void canClose()
       {
       }


       private void canDDelete()
       {
           StyledMessageBoxView messageBox = new StyledMessageBoxView();
          // messageBox.Owner = Application.Current.MainWindow;
           messageBox.Title = "Delete Exploitation invoce Informations";
           messageBox.ViewModel.Message = "Are you sure you want to Delete this ?";
           if (messageBox.ShowDialog().Value == true)
           {
               try
               {
                   IsBusy = true;
                   _exploitService.EXPLOITATION_FACTURE_DELETE (ExploitSelected.IdExploitation );

                   loadexploit();
                   ExploitSelected  = null;
                   IsBusy = false;
               }
               catch (Exception ex)
               {
                   CustomExceptionView view = new CustomExceptionView();
                  // view.Owner = Application.Current.MainWindow;
                   view.Title = "Warning Message Delete Taxes";
                   view.ViewModel.Message = ex.Message;
                   view.ShowDialog();
                   IsBusy = false;
                   this.MouseCursor = null;
               }
           }
       }
       bool canDExecute()
       {
           return ExploitSelected !=null ?true :false ;
       }

       private void canDNew()
       {
           _exploitSelected = new ExploitationFactureModel();
           ExploitSelected = _exploitSelected;
       }


       private void canDSave()
       {
           try
           {
               IsBusy = true;
               if (ExploitSelected.IdLangue == 0)
               {
                   if (ExploitSelected.IdExploitation != 0)
                   {
                       _exploitService.EXPLOITATION_FACTURE_ADD(ExploitSelected);
                       loadexploit();
                       ExploitSelected = null;
                       IsBusy = false;
                   }
                   else
                       MessageBox.Show(" selected laguage");
               }
               else
               {
                   _exploitService.EXPLOITATION_FACTURE_ADD(ExploitSelected);
                   loadexploit();
                   ExploitSelected = null;
                   IsBusy = false;
               }
           }
           catch (Exception ex)
           {
               CustomExceptionView view = new CustomExceptionView();
               //view.Owner = Application.Current.MainWindow;
               view.Title = "Warning Message Add Objet invoice";
               view.ViewModel.Message = ex.Message;
               view.ShowDialog();
               IsBusy = false;
               this.MouseCursor = null;
           }
       }
       bool canDExecuteSave()
       {
           return ExploitSelected!=null ?true :false ;
       }

       #endregion
        #endregion
    }
}
