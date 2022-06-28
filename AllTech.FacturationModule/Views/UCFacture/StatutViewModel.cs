using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.Views;
using System.Windows.Forms;
using System.ComponentModel;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using AllTech.FrameWork.Model;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Global;
using System.Windows.Input;

namespace AllTech.FacturationModule.Views.UCFacture
{
    public class StatutViewModel : ViewModelBase
    {
        #region fields
        private RelayCommand newsCommand;
        private RelayCommand savesCommand;
        private RelayCommand deletesCommand;

        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;
        SocieteModel societeCourante;


        StatutModel statutservice;
        StatutModel _statutSelected;
        ObservableCollection<StatutModel> _statutList;
        LangueModel _language;
        ObservableCollection<LangueModel> _languageStatList;
        LangueModel _languageStatSelected;


        #endregion

       public StatutViewModel()
       {
           societeCourante = GlobalDatas.DefaultCompany;
           ParametersDatabase = GlobalDatas.dataBasparameter;
           UserConnected = GlobalDatas.currentUser;
           statutservice = new StatutModel();
           _language = new LangueModel();
           if (CacheDatas.ui_currentdroitFactureElementInterface == null)
           {
               CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("factures")) ?? new DroitModel();
               CacheDatas.ui_currentdroitFactureElementInterface = CurrentDroit;
           }
           else CurrentDroit = CacheDatas.ui_currentdroitFactureElementInterface;
           LoadStatut();
       }

        #region Properties

       public DroitModel CurrentDroit
       {
           get { return _currentDroit; }
           set
           {
               _currentDroit = value;
               OnPropertyChanged("CurrentDroit");
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

       public StatutModel StatutSelected
       {
           get { return _statutSelected; }
           set
           {
               _statutSelected = value;

               this.OnPropertyChanged("StatutSelected");
           }
       }


       public ObservableCollection<StatutModel> StatutList
       {
           get { return _statutList; }
           set
           {
               _statutList = value;
               this.OnPropertyChanged("StatutList");
           }
       }

       public ObservableCollection<LangueModel> LanguageStatList
       {
           get { return _languageStatList; }
           set
           {
               _languageStatList = value;
               this.OnPropertyChanged("LanguageStatList");
           }
       }

       public LangueModel LanguageStatSelected
       {
           get { return _languageStatSelected; }
           set
           {
               _languageStatSelected = value;
               if (value != null)
               {
                   if (StatutSelected != null)
                   {
                       StatutSelected.IdLangue = value.Id;
                   }
               }
               this.OnPropertyChanged("LanguageStatSelected");
           }
       }

        #endregion

        #region icommand

       public ICommand SavesCommand
       {
           get
           {
               if (this.savesCommand == null)
               {
                   this.savesCommand = new RelayCommand(param => this.cansSave(), param => this.cansExecuteSave());
               }
               return this.savesCommand;
           }
       }

       public ICommand NewsCommand
       {
           get
           {
               if (this.newsCommand == null)
               {
                   this.newsCommand = new RelayCommand(param => this.cansNew());
               }
               return this.newsCommand;
           }


       }

       public ICommand DeletesCommand
       {
           get
           {
               if (this.deletesCommand == null)
               {
                   this.deletesCommand = new RelayCommand(param => this.cansDelete(), param => this.cansExecuteDelete());
               }
               return this.deletesCommand;
           }


       }
        #endregion

        #region methods

       void loadlanguage()
       {
           BackgroundWorker worker = new BackgroundWorker();

           worker.DoWork += (o, args) =>
           {
               try
               {
                   LanguageStatList = _language.LANGUE_SELECT(0);

               }
               catch (Exception ex)
               {
                   args.Result = ex.Message;

               }
           };
           worker.RunWorkerCompleted += (o, args) =>
           {
               if (args.Result != null)
               {
                   CustomExceptionView view = new CustomExceptionView();
                   // view.Owner = Application.Current.MainWindow;
                   view.Title = "ERREURE CHARGEMENT LANGUE";
                   view.ViewModel.Message = args.Result.ToString();
                   view.ShowDialog();

               }


           };

           worker.RunWorkerAsync();
       }

       public void LoadStatut()
       {

           try
           {
               StatutList = statutservice.STATUT_FACTURE_GETLISTE();

           }
           catch (Exception ex)
           {
               CustomExceptionView view = new CustomExceptionView();
               //view.Owner = Application.Current.MainWindow;
               view.Title = "ERREUR CHARGEMENT LISTE  STATUT";
               view.ViewModel.Message = ex.Message;
               view.ShowDialog();
           }




       }
       private void cansNew()
       {
           if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
           {
               _statutSelected = new StatutModel();
               StatutSelected = _statutSelected;
           }
       }

       private void cansSave()
       {
           try
           {
               if (StatutSelected.IdStatut > 0)
               {
                   statutservice.STATUT_FACTURE_ADD(StatutSelected);
                   StatutSelected = null;
                   LanguageStatSelected = null;
                   LoadStatut();
               }
           }
           catch (Exception ex)
           {
               CustomExceptionView view = new CustomExceptionView();
              // view.Owner = Application.Current.MainWindow;
               view.Title = "INFORMATION SAUVEGARDE STATUT";
               view.ViewModel.Message = ex.Message;
               view.ShowDialog();
               //IsBusy = false;
               //this.MouseCursor = null;
           }
       }

       bool cansExecuteSave()
       {
           bool values = false;
           if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
           {
               if (StatutSelected != null)
                   values = true;
           }
           return values;
       }


       private void cansDelete()
       {
           StyledMessageBoxView messageBox = new StyledMessageBoxView();
           //messageBox.Owner = Application.Current.MainWindow;
           messageBox.Title = "INFORMAION SUPPRESSION STATUT";
           messageBox.ViewModel.Message = "Voulez Vous Supprimer Ce Statut ?";
           if (messageBox.ShowDialog().Value == true)
           {
               try
               {
                   statutservice.STATUT_FACTURE_DELETE(StatutSelected.IdStatut);
                   StatutList = statutservice.STATUT_FACTURE_GETLISTE();
                   StatutSelected = null;
                   LanguageStatSelected = null;
               }
               catch (Exception ex)
               {
                   CustomExceptionView view = new CustomExceptionView();
                  // view.Owner = Application.Current.MainWindow;
                   view.Title = "INFORMATION SUPPRESSION STATUT";
                   view.ViewModel.Message = ex.Message;
                   view.ShowDialog();

               }
           }

       }

       bool cansExecuteDelete()
       {
           bool values = false;
           if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Proprietaire)
           {
               if (StatutSelected != null)
                   if (StatutSelected.IdStatut > 0)
                       values = true;
           }
           return values;


       }
        #endregion
    }
}
