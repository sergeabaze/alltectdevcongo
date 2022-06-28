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
using System.Collections.ObjectModel;

namespace AllTech.FacturationModule.Views.UCFacture
{
    public class TermeViewModel : ViewModelBase
    {
        #region fields
        private RelayCommand newsTermeCommand;
        private RelayCommand saveTermeCommand;
        private RelayCommand deleteTermeCommand;

        UtilisateurModel userConnected;
        ParametresModel _parametersDatabase;
        DroitModel _currentDroit;
        SocieteModel societeCourante;

        LibelleTermeModel termeService;
        LibelleTermeModel termeSelected;
        List<LibelleTermeModel> termeList;
        LangueModel _langueTermeSelected;
        LangueModel _languageStatSelected;
        ObservableCollection<LangueModel> _languageStatList;
        LangueModel _language;

        #endregion


        public TermeViewModel()
        {
            societeCourante = GlobalDatas.DefaultCompany;
            ParametersDatabase = GlobalDatas.dataBasparameter;
            UserConnected = GlobalDatas.currentUser;
            termeService = new LibelleTermeModel();
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

        public LangueModel LangueTermeSelected
        {
            get { return _langueTermeSelected; }
            set
            {
                _langueTermeSelected = value;
                if (value != null)
                {
                    var termes = termeService.GetLibelle_List(value.Id);
                    TermeList = termes;
                    TermeSelected = null;
                }

                this.OnPropertyChanged("LangueTermeSelected");
            }
        }

        public LibelleTermeModel TermeSelected
        {
            get { return termeSelected; }
            set
            {
                termeSelected = value;
                this.OnPropertyChanged("TermeSelected");
            }
        }

        public List<LibelleTermeModel> TermeList
        {
            get { return termeList; }
            set
            {
                termeList = value;
                this.OnPropertyChanged("TermeList");
            }
        }

        public LangueModel LanguageStatSelected
        {
            get { return _languageStatSelected; }
            set
            {
                _languageStatSelected = value;
                //if (value != null)
                //{
                //    if (StatutSelected != null)
                //    {
                //        StatutSelected.IdLangue = value.Id;
                //    }
                //}
                this.OnPropertyChanged("LanguageStatSelected");
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
        #endregion

        #region icommand

        public ICommand NewsTermeCommand
        {
            get
            {
                if (this.newsTermeCommand == null)
                {
                    this.newsTermeCommand = new RelayCommand(param => this.canNewTerme());
                }
                return this.newsTermeCommand;
            }
        }

        public ICommand SaveTermeCommand
        {
            get
            {
                if (this.saveTermeCommand == null)
                {
                    this.saveTermeCommand = new RelayCommand(param => this.canSaveTerme(), param => this.canExecuteSaveTerme());
                }
                return this.saveTermeCommand;
            }
        }

        public ICommand DeleteTermeCommand
        {
            get
            {
                if (this.deleteTermeCommand == null)
                {
                    this.deleteTermeCommand = new RelayCommand(param => this.canDeleteTerme(), param => this.canExecuteDeleteTerme());
                }
                return this.deleteTermeCommand;
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
                    view.Title = "ERREUR CHARGEMENT LANGUE";
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();

                }


            };

            worker.RunWorkerAsync();
        }

        private void canNewTerme()
        {
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
            {
                TermeSelected = null;
            }

        }

        private void canSaveTerme()
        {
            try
            {
                if (TermeSelected.ID > 0)
                {
                    termeService.LIBELLETERME_ADD(TermeSelected, societeCourante.IdSociete);

                    var termes = termeService.GetLibelle_List(LangueTermeSelected.Id);
                    TermeList = termes;
                    TermeSelected = null;
                }


            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                //view.Owner = Application.Current.MainWindow;
                view.Title = "Information de sauvegarde terme paiement";
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();

            }
        }

        bool canExecuteSaveTerme()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Ecriture || CurrentDroit.Proprietaire)
            {
                if (TermeSelected != null)
                    values = true;

            }
            return values;



        }

        private void canDeleteTerme()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            //messageBox.Owner = Application.Current.MainWindow;
            messageBox.Title = "INFORMAION SUPPRESSION TERME PAIEMENT";
            messageBox.ViewModel.Message = "Voulez Vous Supprimer ce terme ?";
            if (messageBox.ShowDialog().Value == true)
            {
                try
                {
                    termeService.LIBELLETERME_DELETE(TermeSelected.ID);
                    TermeSelected = null;
                    var termes = termeService.GetLibelle_List(LangueTermeSelected.Id);
                    TermeList = termes;

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                   // view.Owner = Application.Current.MainWindow;
                    view.Title = "Information de suppression terme Paiement";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();

                }
            }
        }

        bool canExecuteDeleteTerme()
        {
            bool values = false;
            if (CurrentDroit.Super || CurrentDroit.Suppression || CurrentDroit.Proprietaire)
            {
                if (TermeSelected != null)
                    if (TermeSelected.ID > 0)
                        values = true;

            }
            return values;
        }
        #endregion
    }
}
