using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using System.Windows.Input;
using AllTech.FrameWork.Model;
using System.Windows;
using AllTech.FrameWork.Global;
using System.ComponentModel;
using AllTech.FrameWork.Views;

namespace AllTech.FacturationModule.Views.Modal
{
    public class ComptaOhadaViewModel : ViewModelBase
    {
        private RelayCommand addComptaOhadaParamCommand;
        private RelayCommand saveComptaOhadaParamCommand;
        private RelayCommand deleteComptaOhadaParamCommand;

        private RelayCommand addLibelleCommand;

        private CompteOhadaModel service;
        List<CompteOhadaModel> comptesOhadas;
        CompteOhadaModel compteOhadaSelected;

        List<CompteLibelleOhadaModel> compteOhadaWitLibelles;

        List<CompteLibelleOhadaModel> cmbCompteLibelles;
        CompteLibelleOhadaModel cmbCompteLibelleSelect;

       

        SocieteModel societeCourante;
        Window localwindow;
        UtilisateurModel userConnected;
        int indexComptalibelleType;

      
     
        public ComptaOhadaViewModel(Window window)
          {
              service = new CompteOhadaModel();
              societeCourante = GlobalDatas.DefaultCompany;
              UserConnected = GlobalDatas.currentUser;
              localwindow = window;
              loadObjet();
          }


        #region properties

        public int IndexComptalibelleType
        {
            get { return indexComptalibelleType; }
            set { indexComptalibelleType = value;
              
            this.OnPropertyChanged("IndexComptalibelleType");
            }
        }

        public List<CompteLibelleOhadaModel> CmbCompteLibelles
        {
            get { return cmbCompteLibelles; }
            set { cmbCompteLibelles = value;
            this.OnPropertyChanged("CmbCompteLibelles");
            }
        }


        public CompteLibelleOhadaModel CmbCompteLibelleSelect
        {
            get { return cmbCompteLibelleSelect; }
            set { cmbCompteLibelleSelect = value;
            this.OnPropertyChanged("CmbCompteLibelleSelect");
            }
        }

        public List<CompteLibelleOhadaModel> CompteOhadaWitLibelles
        {
            get { return compteOhadaWitLibelles; }
            set { compteOhadaWitLibelles = value;
            this.OnPropertyChanged("CompteOhadaWitLibelles");
            
            }
        }

        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set { userConnected = value;
            this.OnPropertyChanged("UserConnected");
            }
        }

      public List<CompteOhadaModel> ComptesOhadas
      {
          get { return comptesOhadas; }
          set { comptesOhadas = value;
          this.OnPropertyChanged("ComptesOhadas");
          }
      }

      public CompteOhadaModel CompteOhadaSelected
      {
          get { return compteOhadaSelected; }
          set 
          { compteOhadaSelected = value;
          //if (value != null && value.Id > 0)
          //{
          //    for (int i = 0; i < CmbCompteLibelles.Count;i++ )
          //        if (value.IdlibelleType == CmbCompteLibelles[i].ID)
          //        {
          //            IndexComptalibelleType = i;
          //            break;
          //        }
          //}
          this.OnPropertyChanged("CompteOhadaSelected");
          }
      }
        #endregion

        #region Icommand

      //private RelayCommand addComptaOhadaParamCommand;
      //private RelayCommand saveComptaOhadaParamCommand;
      //private RelayCommand deleteComptaOhadaParamCommand;
      //   private RelayCommand addLibelleCommand;

      public ICommand AddLibelleCommand
      {
          get
          {
              if (this.addLibelleCommand == null)
              {
                  this.addLibelleCommand = new RelayCommand(param => this.canNewLibelle());
              }
              return this.addLibelleCommand;
          }
      }

      public ICommand AddComptaOhadaParamCommand
      {
          get
          {
              if (this.addComptaOhadaParamCommand == null)
              {
                  this.addComptaOhadaParamCommand = new RelayCommand(param => this.canNewCmptaOhada());
              }
              return this.addComptaOhadaParamCommand;
          }
      }
      public ICommand SaveComptaOhadaParamCommand
      {
          get
          {
              if (this.saveComptaOhadaParamCommand == null)
              {
                  this.saveComptaOhadaParamCommand = new RelayCommand(param => this.canSaveComptaOhada(), param => this.canExecuteSaveComptaOhada());
              }
              return this.saveComptaOhadaParamCommand;
          }
      }

      public ICommand DeleteComptaOhadaParamCommand
      {
          get
          {
              if (this.deleteComptaOhadaParamCommand == null)
              {
                  this.deleteComptaOhadaParamCommand = new RelayCommand(param => this.canDeleteCmptaOhada(), param => this.canExecuteDeleteCmptOhada());
              }
              return this.deleteComptaOhadaParamCommand;
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
                  //ComptesOhadas = service.selectAll(societeCourante.IdSociete);
                  CompteOhadaWitLibelles = service.SelectCompteOhadaByElelmentId();
                  CmbCompteLibelles = service.selectAllLibelleType();
                  IndexComptalibelleType = -1;
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
                  view.Title = "INFORMATION ERREURE CHARGEMENT COMPTE OHADA";
                  view.Owner = localwindow;
                  view.ViewModel.Message = "Problème survenu lors du chargement des comptes";
                  view.ShowDialog();

              }


          };

          worker.RunWorkerAsync();
      }


      void canNewLibelle()
      {
          ComptaOhadaLibellePlage view = new ComptaOhadaLibellePlage(localwindow);
          view.Owner = localwindow;
          view.ShowDialog();
          CmbCompteLibelles = service.selectAllLibelleType();
          IndexComptalibelleType = -1;
      }

      void canNewCmptaOhada()
      {
        //  CompteOhadaModel copte = new CompteOhadaModel();
          CompteOhadaSelected = new CompteOhadaModel();
      }

      void canSaveComptaOhada()
      {
          try
          {
              if (CompteOhadaSelected.Id == 0)
              {
                  if (CompteOhadaSelected == null)
                  {
                      MessageBox.Show("la plage du compte est un champ requis");
                      return;
                  }
                  CompteOhadaSelected.IdlibelleType = CmbCompteLibelleSelect.ID;
                  service.Insert(CompteOhadaSelected, societeCourante.IdSociete);
                
              }
              else
                  service.Update(CompteOhadaSelected);
              loadObjet();
              CompteOhadaSelected = null;
          }
          catch (Exception ex)
          {
              CustomExceptionView view = new CustomExceptionView();
              view.Owner = localwindow;
              view.Title = "INFORMATION MISE JOUR";
              view.ViewModel.Message = "erreure lors de la création / modification de ce compte";
              view.ShowDialog();
              Utils.logUserActions(string.Format("<-- UI Compte --Erreur création compte {0}  : {1}", CompteOhadaSelected.Libelle, UserConnected.Loggin), "");
              Utils.logUserActions(string.Format("<-- UI Compte Ohada -- Message : {0} ", ex.Message), "");

          }
      }

      bool canExecuteSaveComptaOhada()
      {
          return CompteOhadaSelected !=null ? true :false;
      }

      void canDeleteCmptaOhada()
      {
          try
          {
               StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = localwindow;
            messageBox.Title = "INFORMATION  DE SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer ce Compte ?";
            if (messageBox.ShowDialog().Value == true)
            {
                service.Delete(CompteOhadaSelected.Id);
                loadObjet();
                CompteOhadaSelected = null;
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
              Utils.logUserActions(string.Format("<-- UI Compte -- Erreur lors de la Suppression du compte {0}  interface  par : {1}", CompteOhadaSelected.Libelle, UserConnected.Loggin), "");
              //Utils.logUserActions(string.Format("<-- UI Compte -- message  : {0}", ex.Message), "");
              view.ShowDialog();

          }
      }

      bool canExecuteDeleteCmptOhada()
      {
          return CompteOhadaSelected != null ? (CompteOhadaSelected.Id>0 ?true :false) : false;
      }

        #endregion
    }

    public class ListLibelleType
    {
        
    }
}
