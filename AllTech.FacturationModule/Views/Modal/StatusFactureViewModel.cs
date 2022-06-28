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

namespace AllTech.FacturationModule.Views.Modal
{
   public  class StatusFactureViewModel : ViewModelBase
    {

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand closeCommand;

        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;


        StatutModel statutservice;
        StatutModel _statutSelected;
        ObservableCollection<StatutModel> _statutList;

        LangueModel langageService;
        LangueModel _languageselected;
        ObservableCollection<LangueModel> _languageList;

     

     


       public StatusFactureViewModel()
       {

           statutservice = new StatutModel();
           langageService = new LangueModel();
           loadDatas();
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

       public StatutModel StatutSelected
       {
           get { return _statutSelected; }
           set { _statutSelected = value  ;
           this.OnPropertyChanged("StatutSelected");
           }
       }


       public ObservableCollection<StatutModel> StatutList
       {
           get { return _statutList; }
           set { _statutList = value;
           this.OnPropertyChanged("StatutList");
           }
       }

       public LangueModel Languageselected
       {
           get { return _languageselected; }
           set { _languageselected = value ;
           if (StatutSelected != null)
           {
              
               if (StatutSelected.IdStatut != 0)
               {
                 StatutList =statutservice.STATUT_FACTURE_GETLISTEByIdLanguage(value.Id);
               }else
                   StatutSelected.IdLangue = value.Id;
           }else
               StatutList = statutservice.STATUT_FACTURE_GETLISTEByIdLanguage(value.Id);
           this.OnPropertyChanged("Languageselected");
           }
       }


       public ObservableCollection<LangueModel> LanguageList
       {
           get { return _languageList; }
           set { _languageList = value;
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
                   this.deleteCommand = new RelayCommand(param => this.canDelete(), param => this.canExecuteDelete());
               }
               return this.deleteCommand;
           }


       }
        #endregion

        #region METHODS

       void loadDatas()
       {
           
           BackgroundWorker worker = new BackgroundWorker();
           this.IsBusy = true;
         
           worker.DoWork += (o, args) =>
           {
               try
               {
                   StatutList = statutservice.STATUT_FACTURE_GETLISTE();
                   LanguageList = langageService.LANGUE_SELECT(0);
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
                  
               }
               //this.OnPropertyChanged("ListEmployees");
           };

           worker.RunWorkerAsync();
       }

       private void canNew()
       {
           _statutSelected = new StatutModel();
           StatutSelected = _statutSelected;
       }

       private void canSave()
       {
           try
           {
               statutservice.STATUT_FACTURE_ADD(StatutSelected);
               StatutList = statutservice.STATUT_FACTURE_GETLISTE();
               StatutSelected = null;
           }
           catch (Exception ex)
           {
               CustomExceptionView view = new CustomExceptionView();
              // view.Owner = Application.Current.MainWindow;
               view.Title = "Warning Message Add Status Invoice";
               view.ViewModel.Message = ex.Message;
               view.ShowDialog();
               IsBusy = false;
               this.MouseCursor = null;
           }
       }

       bool canExecuteSave()
       {
           return StatutSelected != null ? true : false;
       }


       private void canDelete()
       {
           StyledMessageBoxView messageBox = new StyledMessageBoxView();
           // messageBox.Owner = Application.Current.MainWindow;
            messageBox.Title = "Delete Detail Product Informations";
            messageBox.ViewModel.Message = "Are you sure you want to Delete this  detail?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    statutservice.STATUT_FACTURE_DELETE(StatutSelected.IdStatut);
                    StatutList = statutservice.STATUT_FACTURE_GETLISTE();
                    StatutSelected = null;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                   // view.Owner = Application.Current.MainWindow;
                    view.Title = "Warning Message Delete Detail Product";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }

       }

       bool canExecuteDelete()
       {
           return StatutSelected !=null ?true :false ;
       }
        #endregion



    }
}
