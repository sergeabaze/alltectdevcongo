using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using System.Windows.Input;
using AllTech.FrameWork.Model;
using AllTech.FrameWork.Views;
using System.Windows;
using AllTech.FrameWork.Global;


namespace AllTech.FacturationModule.Views.Modal
{
    public class ModalSignatureViewModel : ViewModelBase
    {
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;

      
        SocieteModel societeService;
        SocieteModel currentcompany;
        DroitModel _currentDroit;

     
        UtilisateurModel userConnected;
        byte[] signature;

        public byte[] Signature
        {
            get { return signature; }
            set { signature = value;
            OnPropertyChanged("Signature");
            }
        }

        public DroitModel CurrentDroit
        {
            get { return _currentDroit; }
            set { _currentDroit = value;
            OnPropertyChanged("CurrentDroit");
            }
        }

        public ModalSignatureViewModel()
        {
            societeService = new SocieteModel();
            currentcompany = GlobalDatas.DefaultCompany;
            userConnected = GlobalDatas.currentUser;
            if (userConnected!=null )
                CurrentDroit = userConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("societe")) ?? new DroitModel();

            byte[] signat = societeService.Get_SOCIETE_SIGNATURE();
            if (signat != null)
                Signature = signat;

        }


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

        void canSave()
        {
            try
            {
                if (Signature!=null )
                    if (currentcompany!=null )
                    societeService.SOCIETE_SIGNATURE_ADD(currentcompany.IdSociete , Signature);
                MessageBox.Show("Signature Sauvegarder");

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                //view.Owner = Application.Current.MainWindow;
                view.Title = "Information De Sauvegarde Signature";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
               
            }
        }

        bool canExecuteSave()
        {
            bool values = false;
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Super || CurrentDroit.Developpeur || CurrentDroit.Ecriture)
                    values = true;
            }
            return values;
        }


        void canDelete()
        {
            try
            {
                if (Signature != null)
                    if (currentcompany!=null )
                    societeService.SOCIETE_SIGNATURE_DELETE(currentcompany.IdSociete );

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = Application.Current.MainWindow;
                view.Title = "Information De Suppression Signature";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecuteDelete()
        {
              bool values = false;
              if (CurrentDroit != null)
              {
                  if (CurrentDroit.Super || CurrentDroit.Developpeur || CurrentDroit.Ecriture)
                      if (Signature != null)
                      values = true;
              }
              return values;
        }
        #endregion
    }
}
