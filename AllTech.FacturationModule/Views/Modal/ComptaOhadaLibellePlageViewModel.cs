using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using System.Windows.Input;
using AllTech.FrameWork.Model;
using System.ComponentModel;
using AllTech.FrameWork.Views;
using System.Windows;
using AllTech.FrameWork.Global;

namespace AllTech.FacturationModule.Views.Modal
{
    public class ComptaOhadaLibellePlageViewModel : ViewModelBase
    {
        private RelayCommand addCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

        CompteLibelleOhadaModel compteSelect;
        List<CompteLibelleOhadaModel> comptelist;
       
        CompteOhadaModel compteService;
        SocieteModel societeCourante;
        Window localwindow;
        UtilisateurModel userConnected;

       public ComptaOhadaLibellePlageViewModel(Window window)
       {
           compteService = new CompteOhadaModel();
           localwindow = window;
           societeCourante=GlobalDatas.DefaultCompany;
           loadObjet();
       }

        #region Region Properties

       public CompteLibelleOhadaModel CompteSelect
       {
           get { return compteSelect; }
           set { compteSelect = value;
              
           this.OnPropertyChanged("CompteSelect");
           }
       }


       public List<CompteLibelleOhadaModel> Comptelist
       {
           get { return comptelist; }
           set { comptelist = value;
           this.OnPropertyChanged("Comptelist");
           }
       }
        #endregion

        #region Region ICommand

       public ICommand AddCommand
       {
           get
           {
               if (this.addCommand == null)
               {
                   this.addCommand = new RelayCommand(param => this.canNewCmptaOhada());
               }
               return this.addCommand;
           }
       }
       public ICommand SaveCommand
       {
           get
           {
               if (this.saveCommand == null)
               {
                   this.saveCommand = new RelayCommand(param => this.canSaveComptaOhada(), param => this.canExecuteSaveComptaOhada());
               }
               return this.saveCommand;
           }
       }

       public ICommand DeleteCommand
       {
           get
           {
               if (this.deleteCommand == null)
               {
                   this.deleteCommand = new RelayCommand(param => this.canDeleteCmptaOhada(), param => this.canExecuteDeleteCmptOhada());
               }
               return this.deleteCommand;
           }
       }
        #endregion

        #region Region Methods

       void loadObjet()
       {

           BackgroundWorker worker = new BackgroundWorker();

           worker.DoWork += (o, args) =>
           {
               try
               {
                 
                   Comptelist = compteService.selectAllLibelleType();
                 
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
                   view.Title = "INFORMATION ERREURE CHARGEMENT ";
                   view.Owner = localwindow;
                   view.ViewModel.Message = "Problème survenu lors du chargement des comptes";
                   view.ShowDialog();

               }


           };

           worker.RunWorkerAsync();
       }


       void canNewCmptaOhada()
       {
           CompteSelect = new CompteLibelleOhadaModel();
           CompteSelect.ID = 0;

       }


       void canSaveComptaOhada()
       {
           try
           {
                   if (string.IsNullOrEmpty(CompteSelect.libelle))
                   {
                       MessageBox.Show("la plage du compte est un champ requis");
                       return;
                   }

                   compteService.CompteElement_Insert(CompteSelect, societeCourante.IdSociete);
             

               loadObjet();
               CompteSelect = null;
           }
           catch (Exception ex)
           {
               CustomExceptionView view = new CustomExceptionView();
               view.Owner = localwindow;
               view.Title = "INFORMATION MISE JOUR";
               view.ViewModel.Message = ex.Message;
               view.ShowDialog();
               //Utils.logUserActions(string.Format("<-- UI Compte --Erreur création compte {0}  : {1}", CompteOhadaSelected.Libelle, UserConnected.Loggin), "");
               // Utils.logUserActions(string.Format("<-- UI Compte Ohada -- Message : {0} ", ex.Message), "");

           }

       }

       bool canExecuteSaveComptaOhada()
       {
           return CompteSelect!=null ;
       }



       void canDeleteCmptaOhada()
       {
           try
           {
               StyledMessageBoxView messageBox = new StyledMessageBoxView();
               messageBox.Owner = localwindow;
               messageBox.Title = "INFORMATION  DE SUPPRESSION";
               messageBox.ViewModel.Message = "Voulez vous supprimer ce libelle ?";
               if (messageBox.ShowDialog().Value == true)
               {
                   compteService.CompteElement_Delete(CompteSelect.ID);
                   loadObjet();
                   CompteSelect = null;
               }

           }
           catch (Exception ex)
           {
               CustomExceptionView view = new CustomExceptionView();

               view.Title = "INFORMATION ERREURE  SUPPRESSION";
               if (ex.Message.Contains("CONSTRAINT"))
                   view.ViewModel.Message = "Impossible de Supprimer ce Compte \n il est déja Attribuer à un Client";
               else
                   view.ViewModel.Message = " un probleme est survenu lors de la suppression de ce compte , contactez l'administrateur";
              // Utils.logUserActions(string.Format("<-- UI Compte -- Erreur lors de la Suppression du compte {0}  interface  par : {1}", CompteOhadaSelected.Libelle, UserConnected.Loggin), "");
               //Utils.logUserActions(string.Format("<-- UI Compte -- message  : {0}", ex.Message), "");
               view.ShowDialog();

           }
       }

       bool canExecuteDeleteCmptOhada()
       {
           return CompteSelect != null ? (CompteSelect.ID>0? true:false) : false;
       }
        #endregion
    }
}
