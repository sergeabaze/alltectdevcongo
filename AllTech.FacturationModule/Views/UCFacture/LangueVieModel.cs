using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using AllTech.FrameWork.Command;
using System.Windows.Input;
using AllTech.FrameWork.Global;
using AllTech.FrameWork.Model;
using System.Collections.ObjectModel;
using AllTech.FrameWork.Views;
using System.Windows.Forms;
using System.ComponentModel;

namespace AllTech.FacturationModule.Views.UCFacture
{
    public class LangueVieModel : ViewModelBase
    {

        private RelayCommand newLCommand;
        private RelayCommand saveLCommand;
        private RelayCommand deleteLCommand;

        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;
        SocieteModel societeCourante;

        LangueModel _language;
        ObservableCollection<LangueModel> _languageList;
      
        LangueModel _languageSelected;
        int indexLangue;
        ObservableCollection<LangueModel> _languagedisplayList;
        LangueModel _languageDisplaySelected;
      

        public LangueVieModel()
        {
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            if (CacheDatas.ui_currentdroitFactureElementInterface == null)
            {
                CurrentDroit = UserConnected.Profile.Droit.Find(d => d.LibelleVue.ToLower().Contains("data reference")).SousDroits.Find(sd => sd.LibelleSouVue.Contains("factures")) ?? new DroitModel();
                CacheDatas.ui_currentdroitFactureElementInterface = CurrentDroit;
            }
            else CurrentDroit = CacheDatas.ui_currentdroitFactureElementInterface;
                societeCourante = GlobalDatas.DefaultCompany;
            _language = new LangueModel();
            loadlanguage();
        }

        #region Proprietes

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
                    // if (Objetselected != null)
                    // Objetselected.IdLangue = value.Id;
                    //loadObjet(value.Id);
                }
                this.OnPropertyChanged("LanguageDisplaySelected");
            }
        }


        public int IndexLangue
        {
            get { return indexLangue; }
            set
            {
                indexLangue = value;
                this.OnPropertyChanged("IndexLangue");
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
                    //if (Objetselected != null)
                       // Objetselected.IdLangue = value.Id;
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
        #endregion

        #region Icommand
        public ICommand SaveLCommand
        {
            get
            {
                if (this.saveLCommand == null)
                {
                    this.saveLCommand = new RelayCommand(param => this.canLSave(), param => this.canLExecuteSave());
                }
                return this.saveLCommand;
            }
        }

        public ICommand DeleteLCommand
        {
            get
            {
                if (this.deleteLCommand == null)
                {
                    this.deleteLCommand = new RelayCommand(param => this.canLDelete(), param => this.canLExecute());
                }
                return this.deleteLCommand;
            }
        }

        public ICommand NewLCommand
        {
            get
            {
                if (this.newLCommand == null)
                {
                    this.newLCommand = new RelayCommand(param => this.canLNew());
                }
                return this.newLCommand;
            }


        }
        #endregion

        #region Methods

        void loadlanguage()
        {
             BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, args) =>
            {
                try
                {
                    LanguageList = _language.LANGUE_SELECT(0);
                   
                  
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
                    view.Title = "ERREUR CHARGEMENT LANGUE";
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();

                }


            };

            worker.RunWorkerAsync();
        }
        private void canLNew()
        {
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
            {
                _languageSelected = new LangueModel();
                LanguageSelected = _languageSelected;
            }
        }
        private void canLSave()
        {
            try
            {
                _language.LANGUE_ADD(LanguageSelected);
                loadlanguage();
                LanguageSelected = null;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = Application.Current.MainWindow;
                view.Title = "Warning Message Add Taxe";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                //IsBusy = false;
                //this.MouseCursor = null;
            }
        }

        bool canLExecuteSave()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture)
            {
                if (LanguageSelected != null)
                    values = true;

            }
            return values;
        }


        private void canLDelete()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
           // messageBox.Owner = Application.Current.MainWindow;
            messageBox.Title = "SUPPRESSION INFORMATION LANGUES";
            messageBox.ViewModel.Message = "voulez vous supprimez cette langue?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    _language.LANGUE_DELETE(LanguageSelected.Id);
                    LanguageSelected = null;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    //view.Owner = Application.Current.MainWindow;
                    view.Title = "MESSAGE SUPPRESSION LANGUE";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                    //IsBusy = false;
                    //this.MouseCursor = null;
                }
            }

        }

        bool canLExecute()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Proprietaire)
            {
                if (LanguageSelected != null)
                    if (LanguageSelected.Id > 0)
                        values = true;

            }
            return values;


        }
        #endregion
    }
}
