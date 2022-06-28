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
using AllTech.FrameWork.Global;
using System.Threading;
using AllTech.FacturationModule.Views.Modal;


namespace AllTech.FacturationModule.ViewModel
{
    public class DatarefCompanyViewModel : ViewModelBase
    {
        #region FIELDS
        
      
        private readonly IRegionManager _regionManager;
        private readonly IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        private bool isBusy;
     
        private Cursor mouseCursor;
      

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand savedetailCommand;
        private RelayCommand showSignatureCommand;
        private RelayCommand closeCommand;
        private RelayCommand delLogoCommand;
        private RelayCommand delLogoPiedPageCommand;

        SocieteModel societeService;
        SocieteModel currentSociete;
        List<SocieteModel> societeListe;

     

        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;
        UtilisateurModel userConnected;

        bool btnSaveVisible;
        bool btnDeleteVisible;
        bool cmbStevisible;
        bool isVisiblebtnSignature;
        private int idexSociete;
        Window localwindow;
        byte[] signature;
        string messageSignature;

       
        #endregion

        #region CONSTRUCTOR



        public DatarefCompanyViewModel()
       {
           //localwindow = window;
           societeService = new SocieteModel();
           ParametersDatabase = GlobalDatas.dataBasparameter;
           UserConnected = GlobalDatas.currentUser;
           if (CacheDatas.ui_currentdroitCompanyInterface == null)
           {
               CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("societe")) ?? new DroitModel();
               CacheDatas.ui_currentdroitCompanyInterface = CurrentDroit;
           }else CurrentDroit= CacheDatas.ui_currentdroitCompanyInterface;

           currentSociete = GlobalDatas.DefaultCompany;
           if (CurrentDroit.Developpeur || CurrentDroit.Lecture)
           {
               loadDatas();
               loadRight();
           }

           if (CurrentDroit.Developpeur || CurrentDroit.Signature)
               isVisiblebtnSignature = true;
           else
               isVisiblebtnSignature = false;
       }

        #endregion

        #region PROPERTIES

        public string MessageSignature
        {
            get { return messageSignature; }
            set { messageSignature = value;
            OnPropertyChanged("MessageSignature");
            }
        }

        public byte[] Signature
        {
            get { return signature; }
            set { signature = value; }
        }

        public int IdexSociete
        {
            get { return idexSociete; }
            set { idexSociete = value;
            OnPropertyChanged("IdexSociete");
            }
        }

        public bool CmbStevisible
        {
            get { return cmbStevisible; }
            set { cmbStevisible = value;
          
            OnPropertyChanged("CmbStevisible");
            }
        }

        public bool IsVisiblebtnSignature
        {
            get { return isVisiblebtnSignature; }
            set
            {
                isVisiblebtnSignature = value;
                OnPropertyChanged("IsVisiblebtnSignature");
            }
        }


        public bool BtnSaveVisible
        {
            get { return btnSaveVisible; }
            set
            {
                btnSaveVisible = value;
                OnPropertyChanged("BtnSaveVisible");
            }
        }
        public bool BtnDeleteVisible
        {
            get { return btnDeleteVisible; }
            set
            {
                btnDeleteVisible = value;
                OnPropertyChanged("BtnDeleteVisible");
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


        public SocieteModel CurrentSociete
        {
            get { return currentSociete; }
            set { currentSociete = value  ;
            this.OnPropertyChanged("CurrentSociete");
            }
        }

        public List<SocieteModel> SocieteListe
        {
            get { return societeListe; }
            set { societeListe = value;
            OnPropertyChanged("SocieteListe");
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

        public RelayCommand ShowSignatureCommand
        {
            get
            {
                if (this.showSignatureCommand == null)
                {
                    this.showSignatureCommand = new RelayCommand(param => this.canShowModalSignature());
                }
                return this.showSignatureCommand;
            }

        }

        //

        public RelayCommand DelLogoPiedPageCommand
        {
            get
            {
                if (this.delLogoPiedPageCommand == null)
                {
                    this.delLogoPiedPageCommand = new RelayCommand(param => this.canDeletelPiedPage(param), param => this.canExecuteDeleLPiedPage());
                }
                return this.delLogoPiedPageCommand;
            }

        }

        public RelayCommand DelLogoCommand
        {
            get
            {
                if (this.delLogoCommand == null)
                {
                    this.delLogoCommand = new RelayCommand(param => this.canDeletelogo(param), param => this.canExecuteDeleLogo());
                }
                return this.delLogoCommand;
            }

        }
        #endregion


        #region METHODS

        void loadRight()
        {
            if (CurrentDroit != null)
            {
                if (CurrentDroit.Super)
                {

                    BtnDeleteVisible = true;
                    BtnSaveVisible = true;
                    //
                }
                else
                {
                    if (CurrentDroit.Suppression)
                    {
                        BtnDeleteVisible = true;

                    }
                    if (CurrentDroit.Ecriture)
                    {
                        BtnSaveVisible = true;

                    }
                }
            }

               
        }

        private void canClose()
        {
            _injectSingleViewService.ClearViewsFromRegion(RegionNames.ViewRegion);
        }

        void setDefaulIndex()
        {
            int i=0;
            foreach (var ste in societeListe)
            {
                if (ste.IdSociete == CurrentSociete.IdSociete)
                {
                    idexSociete = i;
                    break;
                }
                    i++;
            }
        }

        void loadDatas()
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
           
            worker.DoWork += (o, args) =>
            {
                try
                {
                    
                    if (GlobalDatas.listeCompany != null)
                        SocieteListe = GlobalDatas.listeCompany;// societeService.Get_SOCIETE_LIST();
                    if (societeListe !=null )
                        if (SocieteListe.Count > 1)
                        {
                            CmbStevisible = true;
                        }
                        else
                        {
                            byte[] signat = societeService.Get_SOCIETE_SIGNATURE();
                            if (signat != null)
                            {
                                Signature = signat;
                                MessageSignature = "Signature en cours";
                            }
                            else MessageSignature = "";
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
                    view.Owner =localwindow;
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
        private void canSave()
        {
            try
            {
                this.IsBusy = true;
                if (!string.IsNullOrEmpty(CurrentSociete.SigleSite))
                {
                    societeService.SOCIETE_ADD(CurrentSociete);
                    MessageBox.Show("Informations Mise à jour");
                    CurrentSociete = null;
                    for (int i = 0; i < 50; i += 5)
                        Thread.Sleep(100);

                    CurrentSociete = societeService.Get_SOCIETE_BYID(0);

                    this.IsBusy = false;
                }
                else MessageBox.Show("Le sigle est obligatoire, vous devez le renseigner");

            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner =localwindow;
                view.Title = "MESSAGE INFORMATION MISEJOUR SOCIETE";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecuteSave()
        {
            bool values = false;
            //if (SocieteListe.Count ==1)
            //{
            if ( CurrentDroit.Developpeur || CurrentDroit.Ecriture )
                {
                    if (CurrentSociete != null)
                        values = true;
                }
          
            return values;
        }
        private void canNew()
        {
            currentSociete = new SocieteModel();
            CurrentSociete = currentSociete;
        }
        private void canDelete()
        {
           
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner =localwindow;
            messageBox.Title = "MESSAGE INFORMATION SUPPRESSION SOCIETE";
            messageBox.ViewModel.Message = "Voulez vous supprimer Cette Société ?\n Cette Action est Assez Dagereuse pour la validation des factures des autres sites";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    this.IsBusy = true;
                    societeService.SOCIETE_DELETE();
                    CurrentSociete = null;
                   MessageBox.Show("Suppression Réussite");
                    this.IsBusy = false ;
                }
                catch (Exception ex)
                {
                    string message = string.Empty;

                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner =localwindow;
                    view.Title = "Warning Message Delete Product";
                    if (ex.Message.Contains("constraint fails"))
                        view.ViewModel.Message = "Impossible de Supprimer cette societe, des données y sont associées";
                    else
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
            if (SocieteListe == null)
            {

                if ( CurrentDroit.Suppression || CurrentDroit.Developpeur )
                {
                    if (CurrentSociete != null)
                        values = true;
                }
            }
           
            return values;
        }

        void canShowModalSignature()
        {
            ModalSignature views = new ModalSignature();
            views.Owner =localwindow;
            views.ShowDialog();
        }


        void canDeletelogo(object param)
        {
            //int type =int .Parse ( param.ToString ()) ;
            try
            {
                
                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                    messageBox.Owner =localwindow;
                    messageBox.Title = "MESSAGE INFORMATION SUPPRESSION LOGO";
                    messageBox.ViewModel.Message = "Voulez vous supprimer ce logo de lentreprise";
                    if (messageBox.ShowDialog().Value == true)
                    {
                        societeService.SOCIETE_DELETE_LOGO(2, CurrentSociete.IdSociete);
                        for (int i = 0; i < 50; i += 5)
                            Thread.Sleep(100);
                        CurrentSociete = societeService.Get_SOCIETE_BYID(0);
                    }
                
            }
            catch (Exception ex)
            {
                string message = string.Empty;

                CustomExceptionView view = new CustomExceptionView();
                view.Owner =localwindow;
                view.Title = "Warning Message Suppression Logo";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
            
        }

        bool canExecuteDeleLogo()
        {
            bool values = false;
            if (SocieteListe != null)
            {
                if ( CurrentDroit.Suppression || CurrentDroit.Developpeur )
                {
                    if (CurrentSociete != null)
                        if (CurrentSociete.Logo != null)
                            values = true;
                }
            }
            return values;
        }

        void canDeletelPiedPage(object  param)
        {
            try
            {
                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                messageBox.Owner =localwindow;
                messageBox.Title = "MESSAGE INFORMATION SUPPRESSION LOGO";
                messageBox.ViewModel.Message = "Voulez vous supprimer ce logo de Pied page";
                if (messageBox.ShowDialog().Value == true)
                {
                    societeService.SOCIETE_DELETE_LOGO(1, CurrentSociete.IdSociete);
                    for (int i = 0; i < 50; i += 5)
                        Thread.Sleep(100);
                    CurrentSociete = societeService.Get_SOCIETE_BYID(0);
                }
            }
            catch (Exception ex)
            {
                string message = string.Empty;

                CustomExceptionView view = new CustomExceptionView();
                view.Owner =localwindow;
                view.Title = "Warning Message Suppression Logo";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }

        }

        bool canExecuteDeleLPiedPage()
        {
            bool values = false;
            if (SocieteListe != null)
            {
                if (CurrentDroit.Suppression || CurrentDroit.Developpeur)
                {
                    if (CurrentSociete != null)
                        if (CurrentSociete.LogoPiedPage != null)
                            values = true;
                }
            }
        

            return values;
        }
        #endregion

    }
}
