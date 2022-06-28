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
    public class ObjetViewModel : ViewModelBase
    {
        #region fields
        private RelayCommand newObjCommand;
        private RelayCommand saveObjCommand;
        private RelayCommand deleteObjCommand;

        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;
        SocieteModel societeCourante;

        ClientModel clientService;
        ClientModel currentClient;

        ObjetGenericModel objetservice;
        ObjetGenericModel _objetselected;
        ObservableCollection<ObjetGenericModel> _objetList;
        int indexLangue;
        ObservableCollection<LangueModel> _languagedisplayList;
        LangueModel _languageDisplaySelected;
        ObservableCollection<LangueModel> _languageList;

        List<ClientModel> listeClients;
        LangueModel _languageSelected;
        LangueModel _language;

        #endregion

        public ObjetViewModel()
        {
            societeCourante = GlobalDatas.DefaultCompany;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            objetservice = new ObjetGenericModel();
            _language = new LangueModel();
            if (CacheDatas.ui_currentdroitFactureElementInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("factures")) ?? new DroitModel();
                CacheDatas.ui_currentdroitFactureElementInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitFactureElementInterface;

            loadlanguage();
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

        public ObjetGenericModel Objetselected
        {
            get { return _objetselected; }
            set
            {
                _objetselected = value;
                this.OnPropertyChanged("Objetselected");
            }
        }


        public ObservableCollection<ObjetGenericModel> ObjetList
        {
            get { return _objetList; }
            set
            {
                _objetList = value;
                this.OnPropertyChanged("ObjetList");
            }
        }

        public ClientModel CurrentClient
        {
            get { return currentClient; }
            set
            {
                currentClient = value;
                //if (value != null)
                   // Objetselected.IdClient = value.IdClient;
                this.OnPropertyChanged("CurrentClient");
            }
        }

        public List<ClientModel> ListeClients
        {
            get { return listeClients; }
            set
            {
                listeClients = value;

                this.OnPropertyChanged("ListeClients");
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
                    if (Objetselected != null)
                        Objetselected.IdLangue = value.Id;
                    //    //  loadObjet(value.Id );
                }

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

        public ObservableCollection<LangueModel> LanguagedisplayList
        {
            get { return _languagedisplayList; }
            set
            {
                _languagedisplayList = value;
                this.OnPropertyChanged("LanguagedisplayList");
            }
        }
       
        public LangueModel LanguageDisplaySelected
        {
            get { return _languageDisplaySelected; }
            set
            {
                _languageDisplaySelected = value;
                if (value != null)
                {
                   
                    loadObjet(value.Id);
                }
                this.OnPropertyChanged("LanguageDisplaySelected");
            }
        }
        #endregion

        #region icommand

        public ICommand SaveObjCommand
        {
            get
            {
                if (this.saveObjCommand == null)
                {
                    this.saveObjCommand = new RelayCommand(param => this.canSaveObj(), param => this.canExecuteSaveObj());
                }
                return this.saveObjCommand;
            }
        }

        public ICommand NewObjCommand
        {
            get
            {
                if (this.newObjCommand == null)
                {
                    this.newObjCommand = new RelayCommand(param => this.canNewObjet());
                }
                return this.newObjCommand;
            }


        }

        public ICommand DeleteObjCommand
        {
            get
            {
                if (this.deleteObjCommand == null)
                {
                    this.deleteObjCommand = new RelayCommand(param => this.canDeleteObj(), param => this.canExecuteObj());
                }
                return this.deleteObjCommand;
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
                    LanguagedisplayList = _language.LANGUE_SELECT(0);
                    if (LanguagedisplayList != null)
                        LanguageList = LanguagedisplayList;
                    else LanguageList = new ObservableCollection<LangueModel>();
                 
                  
                }
                catch (Exception ex)
                {
                    args.Result = ex.Message ;
                  
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
        void loadObjet(Int32 idlangue)
        {

            BackgroundWorker worker = new BackgroundWorker();

            worker.DoWork += (o, args) =>
            {
                try
                {
                    if (societeCourante != null)
                        //ObjetList = objetservice.OBJECT_GENERIC_GETLISTE(societeCourante.IdSociete);
                        ObjetList = objetservice.OBJECT_GENERIC_BYLANGUE(societeCourante.IdSociete, idlangue);
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
                 //   view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();

                }


            };

            worker.RunWorkerAsync();
        }


        private void canNewObjet()
        {
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire )
            {
                _objetselected = new ObjetGenericModel();
                Objetselected = _objetselected;
            }
        }


        private void canDeleteObj()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
           // messageBox.Owner = Application.Current.MainWindow;
            messageBox.Title = "INFORMATION SUPPRESSION";
            messageBox.ViewModel.Message = "Voulez vous supprimer Cet Objet ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {

                    objetservice.OBJECT_GENERIC_DELETE(Objetselected.IdObjetg);
                    loadObjet(Objetselected.IdLangue);
                    Objetselected = null;

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    //view.Owner = Application.Current.MainWindow;
                    view.Title = "INFORMATION SUPPRESSION";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();

                }
            }
        }
        bool canExecuteObj()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Proprietaire)
            {
                if (Objetselected != null)
                    if (Objetselected.IdObjetg > 0)
                        values = true;

            }
            return values;


        }

        private void canSaveObj()
        {
            try
            {

                if (Objetselected.IdObjetg == 0)
                {
                    if (LanguageSelected != null)
                    {

                        if (societeCourante != null)
                            if (Objetselected.IdObjetg == 0)
                                Objetselected.IdSite = societeCourante.IdSociete;
                        Objetselected.IdLangue = LanguageSelected.Id;

                        objetservice.OBJECT_GENERIC_ADD(Objetselected);
                        loadObjet(Objetselected.IdLangue);
                        Objetselected = null;
                        LanguageSelected = null;

                    }
                    else
                        MessageBox.Show("Présier la langue");
                }
                else
                {
                    //if (Objetselected != null && Objetselected.IdClient == 0)
                    //    Objetselected.IdClient = currentClient.IdClient;

                    objetservice.OBJECT_GENERIC_ADD(Objetselected);
                    loadObjet(Objetselected.IdLangue);
                    Objetselected = null;
                    LanguageSelected = null;
                    currentClient = null;

                }
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = Application.Current.MainWindow;
                view.Title = "INFORMATION DE MISE JOUR";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }
        bool canExecuteSaveObj()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
            {
                if (Objetselected != null)
                    values = true;

            }
            return values;


        }

        #endregion
    }
}
