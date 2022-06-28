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
using System.Data;

namespace AllTech.FacturationModule.Views.Modal
{
  public   class ObjetClientListViewModel: ViewModelBase
    {
        public IRegionManager _regionManager;
       

        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand addDatasCommand;

        SocieteModel societeCourante;

        ObjetFactureModel  objetservice;
        ObjetFactureModel _objeSelected;
        ObservableCollection<ObjetFactureModel> _objetList;
        ObservableCollection<ObjetFactureModel> _cacheObjetList;

        ObjetGenericModel objetgenService;
        ObjetGenericModel objetGenSelected;
        ObservableCollection<ObjetGenericModel> _objetGenList;
        DroitModel _currentDroit;
        UtilisateurModel userConnected;
        ClientModel currentClient;

        Window localwindow;
        

        public ObjetClientListViewModel(Window window)
        {
            localwindow = window;
            UserConnected = GlobalDatas.currentUser;
            societeCourante = GlobalDatas.DefaultCompany;

            if (CacheDatas.ui_currentdroitClientInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("client")) ?? new DroitModel();
                CacheDatas.ui_currentdroitClientInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitClientInterface;

          
            //_language = new LangueModel();
            objetservice = new ObjetFactureModel();
            objetgenService = new ObjetGenericModel();

          
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

        public ObservableCollection<ObjetGenericModel> ObjetGenList
        {
            get { return _objetGenList; }
            set { _objetGenList = value;
            this.OnPropertyChanged("ObjetGenList");
            }
        }

        public ObjetGenericModel ObjetGenSelected
        {
            get { return objetGenSelected; }
            set { objetGenSelected = value ;
            this.OnPropertyChanged("ObjetGenSelected");
            }
        }

        public ObjetFactureModel ObjeSelected
        {
            get { return _objeSelected; }
            set { _objeSelected = value ;
            this.OnPropertyChanged("ObjeSelected");
            }
        }

        public ObservableCollection<ObjetFactureModel> ObjetList
        {
            get { return _objetList; }
            set { _objetList = value;
            this.OnPropertyChanged("ObjetList");
            }
        }

        public ObservableCollection<ObjetFactureModel> CacheObjetList
        {
            get { return _cacheObjetList; }
            set { _cacheObjetList = value;
            this.OnPropertyChanged("CacheObjetList");
            }
        }

        public ClientModel CurrentClient
        {
            get { return currentClient; }
            set { currentClient = value;
            if (value != null)
                loadeObjet();

            //if (ObjetList != null && ObjetList.Count >0)
                 loadListeObjetste();
            this.OnPropertyChanged("CurrentClient");
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
        //

        public ICommand AddDatasCommand
        {
            get
            {
                if (this.addDatasCommand == null)
                {
                    this.addDatasCommand = new RelayCommand(param => this.canAdd(), param => this.canExecuteAdd());
                }
                return this.addDatasCommand;
            }


        }
        #endregion

      
        #region METHODS

        void loadListeObjetste()
        {

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (CurrentClient != null)
                    {
                        List<ObjetGenericModel> newlistGen = new List<ObjetGenericModel>();
                        ObservableCollection<ObjetGenericModel> listeGen = objetgenService.OBJECT_GENERIC_BYLANGUE(societeCourante.IdSociete, CurrentClient.IdLangue);
                        if (listeGen != null)
                        {
                            foreach (ObjetGenericModel obj in listeGen)
                                newlistGen.Add(obj);

                            ObservableCollection<ObjetGenericModel> newListe = new ObservableCollection<ObjetGenericModel>();
                            if (ObjetList != null)
                            {
                                foreach (ObjetFactureModel objcli in ObjetList)
                                {
                                    if (objcli.IdobjetGen > 0)
                                    {
                                        var objetcRemove = newlistGen.Find(obc => obc.IdObjetg == objcli.IdobjetGen);
                                        listeGen.Remove(objetcRemove);
                                    }
                                }
                            }
                            ObjetGenList = listeGen;
                        }
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
                   view.Owner = localwindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();

                }


            };

            worker.RunWorkerAsync();
        }

        void loadeObjet()
        {

            //BackgroundWorker worker = new BackgroundWorker();

            //worker.DoWork += (o, args) =>
            //{
                try
                {
                    if (CurrentClient != null)
                    {
                        ObservableCollection<ObjetFactureModel> listeObjetUser = objetservice.OBJECT_FACTURE_GETLISTEByCLIENT(societeCourante.IdSociete, CurrentClient.IdClient);
                        if (listeObjetUser != null && listeObjetUser.Count > 0)
                        {
                            foreach (ObjetFactureModel objt in listeObjetUser)
                                objt.Isobjectselect = false;

                            ObjetList = listeObjetUser;
                        }
                    }

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Title = "INFORMATION ERREUR CHARGEMENT OBJETS";
                    view.Owner = localwindow;
                    view.ViewModel.Message = ex .Message.ToString();
                    view.ShowDialog();
                }

            
        }

        void canNew()
        {

        }

        void canSave()
        {
            try
            {
                if (ObjeSelected != null)
                {
                    // mise jour
                    objetservice.OBJECT_FACTURE_ADD(ObjeSelected, societeCourante.IdSociete);

                }
                else
                {
                    // nouvelle insertion
                    List<ObjetFactureModel> listObjectSelected = (from ob in ObjetList
                                                                  where ob.Isobjectselect == true
                                                                  select ob).ToList();


                    if (listObjectSelected.Count >0)
                    {
                         foreach (ObjetFactureModel objselect in listObjectSelected)
                             objetservice.OBJECT_FACTURE_ADD(objselect, societeCourante.IdSociete);
                    }
                }

                ObjeSelected = null;
                ObjetList = null;
                loadeObjet();
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE INFORMATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }
        bool canExecuteSave()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (ObjeSelected != null)
                    values = true;
                if (ObjetList != null)
                {
                    if (ObjetList.Count(d => d.IsNewObject == true) > 0)
                        values = true;
                }
            }
           
            return values;
        }

        void canDelete()
        {
            try
            {
                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                messageBox.Owner = localwindow;
                messageBox.Title = "MESSAGE INFORMATION SUPPRESSION";
                messageBox.ViewModel.Message = "Voulez vous supprimer ce objet ?";
                if (messageBox.ShowDialog().Value == true)
                {
                   int codeMessage= objetservice.OBJECT_FACTURE_DELETE(ObjeSelected.IdObjet );
                   if (codeMessage == 110)
                   {
                       ObjeSelected = null;
                       ObjetList = null;
                       loadeObjet();
                   }
                   else
                   {
                       CustomExceptionView view = new CustomExceptionView();
                       view.Owner = localwindow;
                       view.Title = "MESSAGE INFORMATION SUPPRESSION OBJET";
                       view.ViewModel.Message = "Cet Objet ne pas être supprimer, il est déja utilisé dans une facture";
                       view.ShowDialog();

                   }
                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = localwindow;
                view.Title = "MESSAGE INFORMATION";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecute()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Developpeur)
            {
                if (ObjeSelected != null)
                    values = true;
            }
            return values;

           
        }

        void canAdd()
        {

            ObjetFactureModel objetAdd = new ObjetFactureModel();
            objetAdd.IdObjet =0;
            objetAdd.IdobjetGen = ObjetGenSelected.IdObjetg;
            objetAdd.IdClient = CurrentClient.IdClient;
            objetAdd.Isobjectselect = true ;
            objetAdd.IsNewObject = true;
            objetAdd.ObjetGeneric = ObjetGenSelected;

            ObjetList.Add(objetAdd);

            ObjetGenList.Remove(ObjetGenSelected);

            ObjeSelected = null;
            
               

            
        }

        bool canExecuteAdd()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Developpeur)
            {
                if (ObjetGenSelected != null)
                    values = true;
            }
            return values;

            
        }
        #endregion
    }
}
