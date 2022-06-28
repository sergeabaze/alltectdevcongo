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
using AllTech.FrameWork.Global;
using System.Windows.Input;

namespace AllTech.FacturationModule.Views.UCFacture
{
   public  class DepartementViewModel: ViewModelBase
   {

       #region fields
       private RelayCommand newDCommand;
       private RelayCommand saveDCommand;
       private RelayCommand deleteDCommand;
       UtilisateurModel userConnected;
       ParametresModel _parametersDatabase;
       DroitModel _currentDroit;
       SocieteModel societeCourante;

       DepartementModel depService;
       DepartementModel depSelected;
       List<DepartementModel> departementList;
       #endregion


       public DepartementViewModel()
       {
           societeCourante = GlobalDatas.DefaultCompany;
           ParametersDatabase = GlobalDatas.dataBasparameter;
           UserConnected = GlobalDatas.currentUser;
           depService = new DepartementModel();

           if (CacheDatas.ui_currentdroitFactureElementInterface == null)
           {
               CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("factures")) ?? new DroitModel();
               CacheDatas.ui_currentdroitFactureElementInterface = CurrentDroit;
           }
           else CurrentDroit = CacheDatas.ui_currentdroitFactureElementInterface;
           loadexploit();
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

       public DepartementModel DepSelected
       {
           get { return depSelected; }
           set
           {
               depSelected = value;
               OnPropertyChanged("DepSelected");
           }
       }


       public List<DepartementModel> DepartementList
       {
           get { return departementList; }
           set
           {
               departementList = value;
               OnPropertyChanged("DepartementList");
           }
       }
        #endregion

        #region icommand
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

        #region methods

       void loadexploit()
       {


           try
           {
               if (societeCourante != null)
                   DepartementList = depService.Departement_SELECT(societeCourante.IdSociete);
           }
           catch (Exception ex)
           {

               CustomExceptionView view = new CustomExceptionView();
              // view.Owner = Application.Current.MainWindow;
               view.Title = "ERREUR CHARGEMENT LISTE EXPLOITATIONS";
               view.ViewModel.Message = ex.Message;
               view.ShowDialog();
           }


       }

       private void canDNew()
       {
           if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
           {
               depSelected = new DepartementModel();
               DepSelected = depSelected;
           }
       }

       // new or update
       private void canDSave()
       {
           try
           {
               DepSelected.IdSite = societeCourante.IdSociete;
               depService.Departement_ADD(DepSelected);
               DepSelected = null;
               loadexploit();

           }
           catch (Exception ex)
           {
               CustomExceptionView view = new CustomExceptionView();
              // view.Owner = Application.Current.MainWindow;
               view.Title = "INFORMATION DE MISE JOUR DEPARTEMENT";
               view.ViewModel.Message = ex.Message;
               view.ShowDialog();

           }
       }
       bool canDExecuteSave()
       {
           bool values = false;
           if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
           {
               if (DepSelected != null)
                   values = true;

           }
           return values;
       }


       private void canClose()
       {
       }


       private void canDDelete()
       {
           StyledMessageBoxView messageBox = new StyledMessageBoxView();
          // messageBox.Owner = Application.Current.MainWindow;
           messageBox.Title = "INFORMATION DE SUPPRESSION";
           messageBox.ViewModel.Message = "Voulez Vous Supprimez Cet Objet ?";
           if (messageBox.ShowDialog().Value == true)
           {
               try
               {
                   depService.Departement_DELETE(DepSelected.IdDep);
                   loadexploit();
                   DepSelected = null;

               }
               catch (Exception ex)
               {
                   CustomExceptionView view = new CustomExceptionView();
                 //  view.Owner = Application.Current.MainWindow;
                   view.Title = "INFORMATION DE SUPPRESSION";
                   view.ViewModel.Message = ex.Message;
                   view.ShowDialog();

               }
           }
       }
       bool canDExecute()
       {
           bool values = false;
           if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Proprietaire)
           {
               if (DepSelected != null)
                   if (DepSelected.IdDep > 0)
                       values = true;

           }
           return values;
       }

        #endregion
    }
}
