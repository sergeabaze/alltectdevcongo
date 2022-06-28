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
using AllTech.FacturationModule.Views.Modal;
using System.Data;
using AllTech.FacturationModule.Views;
using AllTech.FacturationModule.Report;
using System.Collections;
using System.Threading;
using System.Globalization;

namespace AllTech.FacturationModule.ViewModel
{
    public class Facturation_viewModel : ViewModelBase
    {
        public IRegionManager _regionManager;
        //private readonly IEventAggregator eventAggregator;
        public IUnityContainer _container;
        private IInjectSingleViewService _injectSingleViewService;

        #region FIELDS


        private bool isBusy;
        bool _progressBarVisibility;
        private Cursor mouseCursor;
        string filtertexte;

        bool isProduct;
        bool isPrestation;
        bool isPorata;

        bool porataselected = false;



        private RelayCommand newCommand;
        private RelayCommand saveCommand;
        private RelayCommand deleteCommand;
        private RelayCommand closeCommand;
        private RelayCommand addDatasCommand;
        private RelayCommand objectSelectedCommand;
        private RelayCommand showTaxesCommand;
        private RelayCommand printCommand;
        private RelayCommand newlineCommand;
        private RelayCommand deletelineCommand;

        FactureModel _fatureCurrent;


        FactureModel factureservice;
        FactureModel FactureCache;
        LigneFactureModel _ligneFacture;
        public LigneFactureModel ligneFactureService;
        List<LigneFactureModel> _LigneFatureListe;



        public ClientModel clientservice;
        ClientModel _clientSelected;
        ObservableCollection<ClientModel> _clientList;
        ObservableCollection<ClientModel> _newclientList;



        ObjetFactureModel objetservice;
        ObjetFactureModel _objetSelected;
        ObservableCollection<ObjetFactureModel> _objetList;

        DepartementModel depService;
        DepartementModel _depselected;
        List<DepartementModel> _departementList;





        ExploitationFactureModel exploitationservice;
        ExploitationFactureModel _exploitationSelected;


        ObservableCollection<ExploitationFactureModel> _exploitationList;




        EnteteFactureModel enteteservice;
        EnteteFactureModel _enteteSelected;
        List<EnteteFactureModel> _enteteList;

        TaxeModel taxeService;
        TaxeModel _taxePorataSelected;
        List<TaxeModel> _taxePorataList;

        TaxeModel _taxeSelected;
        List<TaxeModel> _taxeList;

        StatutModel statutservice;
        DeviseModel _deviseSelected;
        DeviseModel _deviseConvert;


        DeviseModel deviseService;
        List<DeviseModel> deviseList;

        ParametresModel _parametersDatabase;
        ProduitModel produitservice;
        ProduitModel _produitSelected;
        ObservableCollection<ProduitModel> _produiList;
        ObservableCollection<ProduitModel> _cacheProduiList;

        DetailProductModel detailService;
        ObservableCollection<DetailProductModel> detailListProdclient;
        ObservableCollection<DetailProductModel> detailListProdclientSelected;

        List<DetailProductModel> newDetailListeProduit;
        DetailProductModel detailProduitSelected;
        List<ProduitModel> newlisteProduit;



        double fTotalHt;
        double ftotalTTC;
        double ftotaltva;
        string montanttva;

        double fTotalProrata;
        string montantProrata;





        bool isValidateDate;
        bool isEnabledchkDateValidate;
        bool detailProdEnable;
        bool clientEnable;
        bool culumndetailVisible;

        bool factureOnLoad = true;






        bool chkDateValidate;

        bool cmbEnabled;
        bool isEnabledfinalValidation;
        bool isEnableOutValidation;
        bool isEnabledSuspendedValidation;
        bool chkDateOutValidate;
        bool chkDateSuspended;

        bool chkNonValable;
        bool isEnabledNonValable;


        bool isEnabledSuspendedVisible;
        bool isEnableOutVisible;

        bool isEnabledClient;
        bool btnAddDetailEnable;
        bool btnDelDetailEnable;

        bool btnCloseVisible;
        bool isdetailExiste;
        bool issaveUpdatedata = true;
        bool isOperationpossible = false;
        bool isdblClickoperation;




        string valeurDalidation = string.Empty;

        List<LigneFactureModel> newLine = null;
        UtilisateurModel userConnected;

        public static FactureModel factureListeSelected;

        LigneCommand lignecourante;
        List<LigneCommand> ligneCommandList;
        public List<LigneCommand> CacheLigneCommandList;
        float qtyselect;
        SocieteModel societeCourante;
        SocieteModel societeservice;


        object idNewfacture = null;
        bool isAddInvoice = false;
        bool isupdateinvoice = false;
        bool isFactureExist = false;

        public DroitModel droitFormulaire;
        StatutModel currentStatut;
        StatutModel statutService;
        string operation = string.Empty;
        decimal prixUnitaireselected;

        decimal detailPrixunit = 0;
        int idDetailPode = 0;
        bool isPrixunitReadOnly;

        bool isItemsExonere;
        bool exonereEnable;
        bool isProrataEnabled;
        bool isproratable;
        bool isenableObjet;




        ProduitModel oldProduitSelected;
        LigneFactureModel oldLigneFacture;
        FactureModel actualFacture;

        ClientModel clientCourant = null;

        string afficheResume;
        string libelleDetailItems;


        bool enableAfficheResume;
        string afficheStatut;
        string afficheStatutProduit;

        int clientIndex;
        int objetIndex;
        int exploitationIndex;



        double curOldQty;
        double curoldPu;
        bool curIsExonere;
        bool curIsProrata;

        bool isnewClient = false;
        int idSocieteCourant = 0;

        bool isdatevalidationsexist;
        bool  isReloadFacture ;
        HashSet<Int32> IdProduits;
        #endregion



        public Facturation_viewModel(IRegionManager regionManager, IUnityContainer container, FactureModel _factureSelected)
        {
            _regionManager = regionManager;
            clientIndex = -1;
            _container = container;
            ProgressBarVisibility = false;
            IsEnabledClient = false;
            CmbEnabled = false;
            ExonereEnable = false;
            CulumndetailVisible = false;
            EnableAfficheResume = false;
            isReloadFacture = false;
            // database parameters from init file
            loadDataBaseData();


            _injectSingleViewService = new InjectSingleViewService(_regionManager, _container);
            enteteservice = new EnteteFactureModel();
            exploitationservice = new ExploitationFactureModel();
            objetservice = new ObjetFactureModel();
            clientservice = new ClientModel();
            factureservice = new FactureModel();
            produitservice = new ProduitModel();
            statutservice = new StatutModel();
            detailService = new DetailProductModel();
            ligneFactureService = new LigneFactureModel();
            droitFormulaire = new DroitModel();
            taxeService = new TaxeModel();
            deviseService = new DeviseModel();
            statutService = new StatutModel();
            depService = new DepartementModel();
            societeservice = new SocieteModel();
            societeCourante = GlobalDatas.DefaultCompany;

            UserConnected = GlobalDatas.currentUser;
            if (societeCourante != null)
            {
                FatureCurrent = _factureSelected;
                if (FatureCurrent.IdFacture < 0)
                    FatureCurrent.IdFacture = 0;
            }
            //recuperation des droits de la vue(4) facture
            droitFormulaire = UserConnected.Profile.Droit.Find(p => p.IdVues == 4) ??
                UserConnected.Profile.Droit.Find(p => p.LibelleVue.ToLower().Contains("new facture"));


        }


        #region PROPERTIES


        #region FACTURE TRAITEMENT

        public FactureModel FatureCurrent
        {
            //edition facture et chargement
            get { return _fatureCurrent; }
            set
            {
                _fatureCurrent = value;

                try
                {
                    if (value != null && value.IdClient > 0)
                    {
                        //facture a editer
                        ActualFacture = null;
                        FactureCache = value;
                        isupdateinvoice = false;
                        isFactureExist = true;
                        //if (FatureCurrent .IdStatut >=3)
                        IsEnabledClient = false;
                        IsPorata = FatureCurrent.IsProrata;
                        TaxeSelected = taxeService.Taxe_SELECTById(FatureCurrent.IdTaxe, societeCourante.IdSociete);
                        if (societeCourante != null)
                        {
                            if (societeCourante.IdSociete == FatureCurrent.IdSite)
                                idSocieteCourant = societeCourante.IdSociete;
                            else
                                idSocieteCourant = societeservice.Get_SOCIETE_BYID(FatureCurrent.IdSite).IdSociete;
                            loadDatas();
                        }
                        CmbEnabled = true;
                    }
                    else
                    {
                        // nouvelle facture

                        CmbEnabled = false;
                        isFactureExist = false;
                        IsEnabledClient = true;
                        ActualFacture = null;
                        FactureCache = null;
                        isupdateinvoice = false;

                        StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "1");
                        if (newStatut != null)
                            CurrentStatut = newStatut;
                        AfficheStatut = "";
                        TaxeSelected = taxeService.Taxe_SELECTById(ParametersDatabase.Idtva, societeCourante.IdSociete);
                        //grisage de toutes les etapes
                        ChkDateValidate = false;
                        IsEnabledchkDateValidate = false;
                        IsEnableOutValidation = false;
                        IsEnabledSuspendedValidation = false;
                        idSocieteCourant = societeCourante.IdSociete;
                        loadDatas();

                        //type opération
                        operation = "creation";
                    }
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "ERREUR CHARGEMENT FACTURE";
                    view.ViewModel.Message = ex.InnerException.ToString();
                    view.ShowDialog();
                }

                this.OnPropertyChanged("FatureCurrent");
            }
        }


        public float Qtyselect
        {
            get { return qtyselect; }
            set
            {
                qtyselect = value;
                if (value > 0)
                {
                    if (ProduitSelected != null)
                    {
                        //si le prix unitaire existe
                        if (ProduitSelected != null && ClientSelected != null)
                        {
                            idDetailPode = 0;
                            if (LigneFacture != null)
                            {
                                if (DetailListProdclientSelected != null && DetailListProdclientSelected.Count > 0)
                                {
                                    if (DetailListProdclientSelected.Count == 1)
                                    {
                                        if (LigneFacture.Quantite > (decimal)DetailListProdclientSelected[0].Quantite)
                                        {
                                            PrixUnitaireselected = (decimal)DetailListProdclientSelected[0].Prixunitaire;
                                            detailPrixunit = PrixUnitaireselected;
                                            idDetailPode = DetailListProdclientSelected[0].IdDetail;
                                            //AfficheStatutProduit=DetailListProdclient[0].

                                            Isproratable = DetailListProdclientSelected[0].Isprorata;
                                            IsItemsExonere = DetailListProdclientSelected[0].Exonerer;
                                            IsPrixunitReadOnly = false;
                                        }
                                        else
                                        {
                                            PrixUnitaireselected = ProduitSelected.PrixUnitaire;
                                            idDetailPode = 0;
                                            //LigneFacture.PrixUnitaire = 0;
                                            IsPrixunitReadOnly = false;
                                            Isproratable = false;
                                            if (ClientSelected.Exonerere != null)
                                            {
                                                if (ClientSelected.Exonerere.CourtDesc == "part")
                                                {
                                                    Isproratable = DetailListProdclientSelected[0].Isprorata;
                                                    IsItemsExonere = DetailListProdclientSelected[0].Exonerer;
                                                    PrixUnitaireselected = (decimal)DetailListProdclientSelected[0].Prixunitaire;
                                                    detailPrixunit = PrixUnitaireselected;
                                                    idDetailPode = DetailListProdclientSelected[0].IdDetail;
                                                }
                                            }

                                        }


                                    }
                                    else
                                    {
                                        for (int i = 0; i < DetailListProdclientSelected.Count; i++)
                                        {
                                            if (LigneFacture.Quantite <= (decimal)DetailListProdclientSelected[i].Quantite)
                                            {
                                                if (i == 0)
                                                {


                                                    if (LigneFacture.Quantite < (decimal)DetailListProdclientSelected[i].Quantite)
                                                    {
                                                        PrixUnitaireselected = ProduitSelected.PrixUnitaire;
                                                        idDetailPode = 0;
                                                        Isproratable = false;
                                                        IsItemsExonere = false;
                                                        IsPrixunitReadOnly = false;

                                                        if (ClientSelected.Exonerere != null)
                                                        {
                                                            if (ClientSelected.Exonerere.CourtDesc == "part")
                                                            {
                                                                Isproratable = DetailListProdclientSelected[0].Isprorata;
                                                                IsItemsExonere = DetailListProdclientSelected[0].Exonerer;
                                                                PrixUnitaireselected = (decimal)DetailListProdclientSelected[0].Prixunitaire;
                                                                detailPrixunit = PrixUnitaireselected;
                                                                idDetailPode = DetailListProdclientSelected[0].IdDetail;
                                                            }
                                                        }

                                                    }
                                                    else
                                                    {
                                                        PrixUnitaireselected = (decimal)DetailListProdclientSelected[i].Prixunitaire;
                                                        idDetailPode = DetailListProdclientSelected[i].IdDetail;
                                                        detailPrixunit = PrixUnitaireselected;
                                                        Isproratable = DetailListProdclientSelected[i].Isprorata;
                                                        IsItemsExonere = DetailListProdclientSelected[i].Exonerer;
                                                        IsPrixunitReadOnly = true;
                                                    }
                                                    break;
                                                }
                                                else
                                                {
                                                    if (LigneFacture.Quantite == (decimal)DetailListProdclientSelected[i].Quantite)
                                                    {
                                                        PrixUnitaireselected = (decimal)DetailListProdclientSelected[i].Prixunitaire;
                                                        idDetailPode = DetailListProdclientSelected[i].IdDetail;
                                                        detailPrixunit = PrixUnitaireselected;
                                                        Isproratable = DetailListProdclientSelected[i].Isprorata;
                                                        IsItemsExonere = DetailListProdclientSelected[i].Exonerer;
                                                        IsPrixunitReadOnly = true;
                                                    }

                                                    else
                                                    {
                                                        PrixUnitaireselected = (decimal)DetailListProdclientSelected[i - 1].Prixunitaire;
                                                        idDetailPode = DetailListProdclientSelected[i - 1].IdDetail;
                                                        Isproratable = DetailListProdclientSelected[i - 1].Isprorata;
                                                        IsItemsExonere = DetailListProdclientSelected[i - 1].Exonerer;
                                                        detailPrixunit = PrixUnitaireselected;
                                                        IsPrixunitReadOnly = true;
                                                    }
                                                    break;
                                                }

                                            }
                                            else
                                            {

                                                if (i == DetailListProdclientSelected.Count - 1)
                                                {
                                                    PrixUnitaireselected = (decimal)DetailListProdclientSelected[i].Prixunitaire;
                                                    idDetailPode = DetailListProdclientSelected[i].IdDetail;
                                                    Isproratable = DetailListProdclientSelected[i].Isprorata;
                                                    IsItemsExonere = DetailListProdclientSelected[i].Exonerer;
                                                    detailPrixunit = PrixUnitaireselected;
                                                    IsPrixunitReadOnly = true;
                                                }
                                                IsPrixunitReadOnly = true;
                                            }


                                        }
                                    }

                                }
                                else
                                {
                                    // pad de détail 
                                    IsdetailExiste = true;
                                    IsPrixunitReadOnly = false;
                                    idDetailPode = 0;
                                    PrixUnitaireselected = ProduitSelected.PrixUnitaire;
                                }
                            }




                        }

                    }
                }

                this.OnPropertyChanged("Qtyselect");
            }
        }

        #endregion

        #region TOTAL FACTURE

        public double FTotalHt
        {
            get { return fTotalHt; }
            set
            {
                fTotalHt = value;
                OnPropertyChanged("FTotalHt");
            }
        }


        public double FtotalTTC
        {
            get { return ftotalTTC; }
            set
            {
                ftotalTTC = value;
                OnPropertyChanged("FtotalTTC");
            }
        }

        public double Ftotaltva
        {
            get { return ftotaltva; }
            set
            {
                ftotaltva = value;
                OnPropertyChanged("Ftotaltva");
            }
        }


        public string Montanttva
        {
            get { return montanttva; }
            set
            {
                montanttva = value;
                OnPropertyChanged("Montanttva");
            }
        }

        public double FTotalProrata
        {
            get { return fTotalProrata; }
            set
            {
                fTotalProrata = value;
                OnPropertyChanged("FTotalProrata");
            }
        }


        public string MontantProrata
        {
            get { return montantProrata; }
            set
            {
                montantProrata = value;
                OnPropertyChanged("MontantProrata");
            }
        }



        #endregion

        #region DEPARTEMENT

        public DepartementModel Depselected
        {
            get { return _depselected; }
            set
            {
                _depselected = value;
                if (value != null)
                {
                    if (FatureCurrent != null)
                    {
                        FatureCurrent.IdDepartement = value.IdDep;
                    }
                }
                OnPropertyChanged("Depselected");
            }
        }

        public List<DepartementModel> DepartementList
        {
            get { return _departementList; }
            set
            {
                _departementList = value;
                OnPropertyChanged("DepartementList");
            }
        }
        #endregion


        #region OLD VALUES

        public bool CurIsProrata
        {
            get { return curIsProrata; }
            set
            {
                curIsProrata = value;
                OnPropertyChanged("CurIsProrata");
            }
        }

        public bool CurIsExonere
        {
            get { return curIsExonere; }
            set
            {
                curIsExonere = value;
                OnPropertyChanged("CurIsExonere");
            }
        }

        public double CurOldQty
        {
            get { return curOldQty; }
            set
            {
                curOldQty = value;
                OnPropertyChanged("CurOldQty");
            }
        }


        public double CuroldPu
        {
            get { return curoldPu; }
            set
            {
                curoldPu = value;
                OnPropertyChanged("CuroldPu");
            }
        }
        #endregion

        #region COMMON

        public bool IsdblClickoperation
        {
            get { return isdblClickoperation; }
            set
            {
                isdblClickoperation = value;
                this.OnPropertyChanged("IsdblClickoperation");
            }
        }

        public bool CulumndetailVisible
        {
            get { return culumndetailVisible; }
            set
            {
                culumndetailVisible = value;
                this.OnPropertyChanged("CulumndetailVisible");
            }
        }

        public string LibelleDetailItems
        {
            get { return libelleDetailItems; }
            set
            {
                libelleDetailItems = value;
                this.OnPropertyChanged("LibelleDetailItems");
            }
        }

        public ParametresModel ParametersDatabase
        {
            get { return _parametersDatabase; }
            set
            {
                _parametersDatabase = value;
                this.OnPropertyChanged("ParametersDatabase");
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

        public bool IsPorata
        {
            get { return isPorata; }
            set
            {
                isPorata = value;
                if (value)
                {
                    if (FatureCurrent != null)
                        FatureCurrent.IsProrata = true;
                }
                else
                {
                    if (FatureCurrent != null)
                        FatureCurrent.IsProrata = false;
                }
                this.OnPropertyChanged("IsPorata");
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

        public bool ProgressBarVisibility
        {
            get { return _progressBarVisibility; }
            set
            {
                _progressBarVisibility = value;
                OnPropertyChanged("ProgressBarVisibility");
            }
        }

        public bool IsProduct
        {
            get { return isProduct; }
            set
            {
                isProduct = value;
                OnPropertyChanged("IsProduct");
            }
        }

        public int ClientIndex
        {
            get { return clientIndex; }
            set
            {
                if (FatureCurrent.IdClient > 0)
                {
                    if (value > 0)
                        clientIndex = value;
                }
                OnPropertyChanged("ClientIndex");
            }
        }

        public int ObjetIndex
        {
            get { return objetIndex; }
            set
            {
                objetIndex = value;
                OnPropertyChanged("ObjetIndex");
            }
        }


        public int ExploitationIndex
        {
            get { return exploitationIndex; }
            set
            {
                exploitationIndex = value;
                OnPropertyChanged("ExploitationIndex");
            }
        }

        public bool IsenableObjet
        {
            get { return isenableObjet; }
            set
            {
                isenableObjet = value;
                OnPropertyChanged("IsenableObjet");
            }
        }


        public ObservableCollection<DetailProductModel> DetailListProdclientSelected
        {
            get { return detailListProdclientSelected; }
            set
            {
                detailListProdclientSelected = value;
                OnPropertyChanged("DetailListProdclientSelected");
            }
        }

        public bool IsdetailExiste
        {
            get { return isdetailExiste; }
            set
            {
                isdetailExiste = value;
                OnPropertyChanged("IsdetailExiste");
            }
        }

        public string AfficheStatutProduit
        {
            get { return afficheStatutProduit; }
            set
            {
                afficheStatutProduit = value;
                OnPropertyChanged("AfficheStatutProduit");
            }
        }

        public string AfficheStatut
        {
            get { return afficheStatut; }
            set
            {
                afficheStatut = value;
                OnPropertyChanged("AfficheStatut");
            }
        }

        public bool DetailProdEnable
        {
            get { return detailProdEnable; }
            set
            {
                detailProdEnable = value;
                OnPropertyChanged("DetailProdEnable");
            }
        }

        public string AfficheResume
        {
            get { return afficheResume; }
            set
            {
                afficheResume = value;
                if (value == "0")
                    EnableAfficheResume = false;
                else
                    EnableAfficheResume = true;
                OnPropertyChanged("AfficheResume");
            }
        }

        public bool EnableAfficheResume
        {
            get { return enableAfficheResume; }
            set
            {
                enableAfficheResume = value;
                OnPropertyChanged("EnableAfficheResume");
            }
        }




        public bool IsItemsExonere
        {
            get { return isItemsExonere; }
            set
            {
                isItemsExonere = value;


                OnPropertyChanged("IsItemsExonere");
            }
        }

        public bool BtnAddDetailEnable
        {
            get { return btnAddDetailEnable; }
            set
            {
                btnAddDetailEnable = value;
                OnPropertyChanged("BtnAddDetailEnable");
            }
        }


        public bool BtnDelDetailEnable
        {
            get { return btnDelDetailEnable; }
            set
            {
                btnDelDetailEnable = value;
                OnPropertyChanged("BtnDelDetailEnable");
            }
        }

        public FactureModel ActualFacture
        {
            get { return actualFacture; }
            set
            {
                actualFacture = value;
                OnPropertyChanged("ActualFacture");
            }
        }


        public bool BtnCloseVisible
        {
            get { return btnCloseVisible; }
            set
            {
                btnCloseVisible = value;
                OnPropertyChanged("BtnCloseVisible");
            }
        }

        public bool IsPrixunitReadOnly
        {
            get { return isPrixunitReadOnly; }
            set
            {
                isPrixunitReadOnly = value;
                OnPropertyChanged("IsPrixunitReadOnly");
            }
        }

        public ObservableCollection<DetailProductModel> DetailListProdclient
        {
            get { return detailListProdclient; }
            set
            {
                detailListProdclient = value;
                OnPropertyChanged("DetailListProdclient");
            }
        }

        public decimal PrixUnitaireselected
        {
            get { return prixUnitaireselected; }
            set
            {
                prixUnitaireselected = value;
                this.OnPropertyChanged("PrixUnitaireselected");
            }
        }

        public bool IsEnabledClient
        {
            get { return isEnabledClient; }
            set
            {
                isEnabledClient = value;
                this.OnPropertyChanged("IsEnabledClient");
            }
        }

        public StatutModel CurrentStatut
        {
            get { return currentStatut; }
            set
            {
                currentStatut = value;
                this.OnPropertyChanged("CurrentStatut");
            }
        }

        public bool IsEnabledchkDateValidate
        {
            get { return isEnabledchkDateValidate; }
            set
            {
                isEnabledchkDateValidate = value;
                this.OnPropertyChanged("IsEnabledchkDateValidate");
            }
        }

        public bool IsEnabledSuspendedVisible
        {
            get { return isEnabledSuspendedVisible; }
            set
            {
                isEnabledSuspendedVisible = value;
                this.OnPropertyChanged("IsEnabledSuspendedVisible");
            }
        }


        public bool IsEnableOutVisible
        {
            get { return isEnableOutVisible; }
            set
            {
                isEnableOutVisible = value;
                this.OnPropertyChanged("IsEnableOutVisible");
            }
        }

        #endregion

        #region CHECKVAlidationSortie



        public bool ChkDateOutValidate
        {
            //date sortie
            get { return chkDateOutValidate; }
            set
            {
                chkDateOutValidate = value;
                ActualFacture = FatureCurrent;
                if (value)
                {
                    if (FatureCurrent.IdFacture > 0)
                    {
                        if (FatureCurrent.DateSuspension == null)
                        {
                            FactureModel newFact = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                            if (newFact.DateSuspension == null)
                            {
                                FatureCurrent.DateSortie = newFact.DateSortie != null ? newFact.DateSortie : DateTime.Now;
                                //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(4);
                                StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "4");
                                CurrentStatut = newStatut;
                                operation = "sortie";
                            }
                            else
                            {
                                MessageBox.Show("L'annulation de la suspension de cette facture est necessaire");
                                ChkDateOutValidate = false;
                                ChkDateSuspended = true;
                            }
                        }
                        else
                        {
                            MessageBox.Show("Cette Facture a Eté Suspendu, il est nécessaire de d'abord la valider");
                            operation = string.Empty;
                            ChkDateOutValidate = false;
                        }
                    }
                }
                else
                {
                    //annuler la sortie existante
                    if (FatureCurrent.DateSortie != null)
                        operation = "sortie";
                    FatureCurrent.DateSortie = null;
                }
                this.OnPropertyChanged("ChkDateOutValidate");
            }
        }


        public bool IsEnableOutValidation
        {
            get { return isEnableOutValidation; }
            set
            {
                isEnableOutValidation = value;
                this.OnPropertyChanged("IsEnableOutValidation");
            }
        }

        #endregion

        #region CHEChVALIDATION Suspendu


        public bool ChkDateSuspended
        {
            get { return chkDateSuspended; }
            set
            {
                chkDateSuspended = value;
                ActualFacture = FatureCurrent;
                if (value)
                {
                    if (!factureOnLoad)
                    {
                        if (FatureCurrent.IdFacture > 0)
                        {
                            if (FatureCurrent.DateSortie == null)
                            {
                                FactureModel newFact = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                if (newFact.DateSortie == null)
                                {
                                    FatureCurrent.DateSuspension = newFact.DateSuspension != null ? newFact.DateSuspension : DateTime.Now;
                                    // StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(4);
                                    StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "5");
                                    CurrentStatut = newStatut;
                                    operation = "supension";
                                }
                                else
                                {
                                    MessageBox.Show(" l'annulation de la sortie de cette facture est necessaire");
                                    chkDateSuspended = false;
                                    ChkDateOutValidate = true;

                                }
                            }
                            else
                            {
                                MessageBox.Show("Cette Facture A déja fait l'objet dune Sortie, donc ne peut Pas Estre Suspendu");
                                operation = string.Empty;
                                chkDateSuspended = false;
                            }
                        }
                    }
                }
                else
                {
                    if (FatureCurrent.DateSuspension != null)
                        operation = "supension";
                    FatureCurrent.DateSuspension = null;
                }
                this.OnPropertyChanged("ChkDateSuspended");
            }
        }

        public bool IsEnabledSuspendedValidation
        {
            get { return isEnabledSuspendedValidation; }
            set
            {
                isEnabledSuspendedValidation = value;
                this.OnPropertyChanged("IsEnabledSuspendedValidation");
            }
        }

        #endregion


        #region CHECK REGION

        public bool ChkNonValable
        {
            get { return chkNonValable; }
            set
            {
                chkNonValable = value;
                this.OnPropertyChanged("ChkNonValable");
            }
        }


        public bool IsEnabledNonValable
        {
            get { return isEnabledNonValable; }
            set
            {
                isEnabledNonValable = value;
                this.OnPropertyChanged("IsEnabledNonValable");
            }
        }


        public bool IsEnabledfinalValidation
        {
            get { return isEnabledfinalValidation; }
            set
            {
                isEnabledfinalValidation = value;
                this.OnPropertyChanged("IsEnabledfinalValidation");
            }
        }




        public bool CmbEnabled
        {
            get { return cmbEnabled; }
            set
            {
                cmbEnabled = value;
                this.OnPropertyChanged("CmbEnabled");
            }
        }


        public bool ChkDateValidate
        {
            get { return chkDateValidate; }
            set
            {
                chkDateValidate = value;
                this.OnPropertyChanged("ChkDateValidate");
            }
        }

        public UtilisateurModel UserConnected
        {
            get { return userConnected; }
            set
            {
                userConnected = value;
                this.OnPropertyChanged("UserConnected");
            }
        }

        #endregion

        #region Validate Date devalidation



        public bool IsValidateDate
        {
            get { return isValidateDate; }
            set
            {
                isValidateDate = value;
                int stat = 0;
                ActualFacture = FatureCurrent;
                StatutModel newCurrentStatut = FatureCurrent.CurrentStatut ?? statutservice.STATUT_FACTURE_GETLISTEByID(FatureCurrent.IdStatut);



                if (value)
                {
                    if (int.Parse(newCurrentStatut.CourtDesc) == 1 && value)
                    {
                        valeurDalidation = "encours";
                        //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                        StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                        CurrentStatut = newStatut;
                        isEnabledchkDateValidate = value;
                    }
                    else if (int.Parse(newCurrentStatut.CourtDesc) == 2)
                    {
                        valeurDalidation = "validation";
                        //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(3);
                        StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");
                        CurrentStatut = newStatut;
                        operation = "validation";
                        isEnabledchkDateValidate = value;
                    }
                    else if (int.Parse(newCurrentStatut.CourtDesc) == 5)
                    {
                        // facture suspendu sans validation ou sortie
                        if (valeurDalidation == "devalide")
                        {
                            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                            messageBox.Owner = Application.Current.MainWindow;
                            messageBox.Title = "INFORMATION  STATUT FACTURE";
                            messageBox.ViewModel.Message = "Cette Action va Annuler le Statut Suspension actuelle de cette facture  ?";
                            if (messageBox.ShowDialog().Value == true)
                            {
                                operation = "validation";
                                valeurDalidation = "devalide";
                                CurrentStatut = newCurrentStatut;
                                isEnabledchkDateValidate = value;
                                isOperationpossible = true;
                            }
                        }
                    }
                    else if (int.Parse(newCurrentStatut.CourtDesc) == 6)
                    {
                        if (valeurDalidation == "devalide")
                        {
                            if (droitFormulaire.Proprietaire)
                            {
                                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                messageBox.Owner = Application.Current.MainWindow;
                                messageBox.Title = "INFORMATION  STATUT FACTURE";
                                messageBox.ViewModel.Message = "Cette Action  modifier le statut non Valide au au Statut Valide, voulez vous continuer  ?";
                                if (messageBox.ShowDialog().Value == true)
                                {
                                    operation = "validation";
                                    valeurDalidation = "devalide";
                                    CurrentStatut = newCurrentStatut;
                                    isEnabledchkDateValidate = value;
                                    isOperationpossible = true;
                                }
                            }
                            else
                            {
                                MessageBox.Show(" Vous n'avez pas assez de droits pour effectuer cette Operation");
                                IsEnabledchkDateValidate = false;
                            }
                        }
                    }



                }
                else
                {
                    //décloture
                    if (int.Parse(newCurrentStatut.CourtDesc) <= 2)
                    {
                        operation = "validation";
                        valeurDalidation = "validation";
                        //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                        StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                        CurrentStatut = newStatut;
                    }
                    else
                    {
                        if (int.Parse(newCurrentStatut.CourtDesc) == 3)
                        {
                            if (droitFormulaire.Proprietaire)
                            {
                                operation = "validation";
                                valeurDalidation = "devalide";
                                // StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                                StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                CurrentStatut = newCurrentStatut;
                                MessageBox.Show("Facture Déja Valider \n Cette action va la dévalider ");
                                IsEnabledchkDateValidate = true;
                                isOperationpossible = true;
                            }
                            else
                            {
                                MessageBox.Show("Facture Déja Valider \n Vous navez pas assez de droits pour la dévalider ");
                                IsEnabledchkDateValidate = false;
                            }
                        }
                        else
                            //{
                            if (int.Parse(newCurrentStatut.CourtDesc) == 4)
                            {
                                if (droitFormulaire.Proprietaire)
                                {
                                    MessageBox.Show(" Cette facture est déja sortie, Modification impossible");
                                    IsEnabledchkDateValidate = true;
                                    isOperationpossible = false;
                                }
                                else
                                {
                                    MessageBox.Show("Facture Déja Sortie \n Vous navez pas assez de droits pour effectuer cette Modification ");
                                    IsEnabledchkDateValidate = false;
                                }
                            }
                            else
                                if (int.Parse(newCurrentStatut.CourtDesc) == 5)
                                {
                                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                    messageBox.Owner = Application.Current.MainWindow;
                                    messageBox.Title = "INFORMATION  STATUT FACTURE";
                                    messageBox.ViewModel.Message = "Cette Action va Annuler le Statut Suspension actuelle de cette facture  ?";
                                    if (messageBox.ShowDialog().Value == true)
                                    {
                                        operation = "validation";
                                        valeurDalidation = "devalide";
                                        //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                                        //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                        CurrentStatut = newCurrentStatut;
                                        IsEnabledchkDateValidate = true;
                                        isOperationpossible = true;
                                    }
                                }
                                else
                                    if (int.Parse(newCurrentStatut.CourtDesc) == 6)
                                    {
                                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                        messageBox.Owner = Application.Current.MainWindow;
                                        messageBox.Title = "INFORMATION  STATUT FACTURE";
                                        messageBox.ViewModel.Message = "Cette Action va annuler le Statut Non valide actuelle de cette facture  ?";
                                        if (messageBox.ShowDialog().Value == true)
                                        {
                                            operation = "validation";
                                            valeurDalidation = "devalide";
                                            //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                                            StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                            CurrentStatut = newCurrentStatut;
                                            IsEnabledchkDateValidate = true;
                                            isOperationpossible = true;
                                        }
                                    }
                                    else operation = "impossible";




                    }


                }




                this.OnPropertyChanged("IsValidateDate");
            }
        }

        #endregion


        #region CLIENtREGION

        public ObservableCollection<ClientModel> ClientList
        {
            get { return _clientList; }
            set
            {
                _clientList = value;
                this.OnPropertyChanged("ClientList");
            }
        }

        public ClientModel ClientSelected
        {

            get { return _clientSelected; }
            set
            {

                if (value != null)
                {

                    try
                    {
                        isnewClient = true;
                        if (LigneCommandList != null)
                        {
                            if (LigneCommandList.Count > 0)
                            {
                                if (LigneCommandList[0].ID == 0)
                                {
                                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                    messageBox.Owner = Application.Current.MainWindow;
                                    messageBox.Title = "MESSAGE INFORMATION SUPPRESSION";
                                    messageBox.ViewModel.Message = "Attention ce Changement de Client va Annuler les Détails de factures Actuels ?, Confirmer ?";
                                    if (messageBox.ShowDialog().Value == true)
                                    {
                                       
                                       
                                        LigneFacture = null;
                                        PrixUnitaireselected = 0;
                                        IsItemsExonere = false;
                                        ExonereEnable = false;
                                        _clientSelected = value;
                                        FTotalHt = 0;
                                        Ftotaltva = 0;
                                        FTotalProrata = 0;
                                        MontantProrata = "";
                                        Montanttva = "";
                                        FtotalTTC = 0;
                                        isnewClient = true;

                                        FatureCurrent.JourFinEcheance = string.Empty;
                                        FatureCurrent.MoisPrestation = null;
                                        FatureCurrent.IdDepartement = 0;
                                        FatureCurrent.IdDevise = 0;
                                        FatureCurrent.IdExploitation = 0;
                                        FatureCurrent.IdObjetFacture = 0;
                                        FatureCurrent.IdStatut = 0;
                                        FatureCurrent.IdTaxe = 0;
                                        FatureCurrent.Label_Dep = string.Empty;
                                        FatureCurrent.NumeroFacture = string.Empty;
                                        FatureCurrent.NumeroLigne = 0;
                                        FatureCurrent.TotalTTC = 0;

                                        LigneCommandList = null;
                                        CacheLigneCommandList = null;
                                        //FactureModel ffacture = new FactureModel();
                                        ////FatureCurrent = new FactureModel();
                                        //FatureCurrent = ffacture;
                                    }
                                    else isnewClient = false;
                                }
                            }
                        }
                        else
                        {
                            _clientSelected = value;
                            FatureCurrent.MoisPrestation = null;
                            FatureCurrent.JourFinEcheance = string.Empty;
                        }
                        if (isnewClient)
                            _clientSelected = value;

                        LigneFacture = new LigneFactureModel();
                        Lignecourante = new LigneCommand();
                        PrixUnitaireselected = 0;
                        AfficheStatutProduit = "";
                        FatureCurrent.IdClient = value.IdClient;
                        ActualFacture = FatureCurrent;
                        loadLanguageLientselejctInfo();
                        CmbEnabled = true;
                        DeviseConvert = deviseService.Devise_SELECTById(value.IdDeviseFact, societeCourante.IdSociete);

                        if (DeviseSelected == null)
                            DeviseSelected = deviseService.Devise_SELECTById(ParametersDatabase.IdDevise, societeCourante.IdSociete);


                        TaxePorataSelected = taxeService.Taxe_SELECTById(value.Idporata, societeCourante.IdSociete);

                        if (FatureCurrent.IdFacture > 0)
                            loadFactureInformation();

                        DepartementList = depService.Departement_SELECT(societeCourante.IdSociete);
                        clientCourant = value;
                        if (ClientSelected.Exonerere != null)
                        {
                            if (ClientSelected.Exonerere.CourtDesc == "part" || ClientSelected.Exonerere.CourtDesc == "exo")
                            {
                                IsProrataEnabled = true;

                            }
                            else
                            {
                                IsProrataEnabled = false;

                            }

                        }
                        if (ClientSelected.Exonerere.CourtDesc == "part")
                            CulumndetailVisible = true;
                        else CulumndetailVisible = false;
                        //if (value.IdExonere == 122001 || value.IdExonere == 122003)
                        //    IsProrataEnabled = true;
                        //else IsProrataEnabled = false;

                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "Message";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                    }


                }
                else
                {
                    if (FatureCurrent != null && FatureCurrent.IdClient > 0)
                    {
                        loadFactureInformation();
                        factureOnLoad = false;
                    }
                    else
                        _clientSelected = value;
                }
                

                this.OnPropertyChanged("ClientSelected");
            }
        }
        #endregion

        #region EXPLOITATAION REGION

        public ObservableCollection<ExploitationFactureModel> ExploitationList
        {
            get { return _exploitationList; }
            set
            {
                _exploitationList = value;
                this.OnPropertyChanged("ExploitationList");
            }
        }

        public ExploitationFactureModel ExploitationSelected
        {
            get { return _exploitationSelected; }
            set
            {
                _exploitationSelected = value;
                if (value != null)
                {
                    if (FatureCurrent != null)
                        FatureCurrent.IdExploitation = value.IdExploitation;
                }
                this.OnPropertyChanged("ExploitationSelected");
            }
        }
        #endregion

        #region OBJET REGION

        public ObservableCollection<ObjetFactureModel> ObjetList
        {
            get { return _objetList; }
            set
            {
                _objetList = value;
                this.OnPropertyChanged("ObjetList");
            }
        }

        public ObjetFactureModel ObjetSelected
        {
            get { return _objetSelected; }
            set
            {
                _objetSelected = value;
                if (value != null)
                {
                    if (FatureCurrent != null)
                        FatureCurrent.IdObjetFacture = value.IdObjet;

                    if (value.IdObjet == 0)
                        IsenableObjet = true;
                    else IsenableObjet = false;
                }
                else IsenableObjet = false;


                this.OnPropertyChanged("ObjetSelected");
            }
        }
        #endregion

        #region PRODUIT REGION

        public List<DetailProductModel> NewDetailListeProduit
        {
            get { return newDetailListeProduit; }
            set
            {
                newDetailListeProduit = value;
                this.OnPropertyChanged("NewDetailListeProduit");
            }
        }


        public DetailProductModel DetailProduitSelected
        {
            get { return detailProduitSelected; }
            set
            {
                detailProduitSelected = value;
                this.OnPropertyChanged("DetailProduitSelected");
            }
        }



        public List<ProduitModel> NewlisteProduit
        {
            get { return newlisteProduit; }
            set
            {
                newlisteProduit = value;
                this.OnPropertyChanged("NewlisteProduit");
            }
        }

        public ProduitModel ProduitSelected
        {
            get { return _produitSelected; }
            set
            {
                _produitSelected = value;

                if (value != null)
                {
                    if (value.IdProduit != 0)
                    {
                        if (LigneCommandList != null)
                        {
                            var de = OldLigneFacture;

                            if (LigneCommandList.Exists(p => p.IdProduit == value.IdProduit) && !IsdblClickoperation)
                            {
                                if (MessageBox.Show("Ce Produit Existe déja dans cette commande, Voulez vous Ajoutez",
                                "Confirm ", MessageBoxButton.OKCancel, MessageBoxImage.Question) == MessageBoxResult.OK)
                                {
                                    if (value.PrixUnitaire > 0)
                                    {
                                        PrixUnitaireselected = value.PrixUnitaire;
                                        if (LigneFacture != null)
                                            LigneFacture.Quantite = 0;
                                    }
                                    DetailListProdclientSelected = detailService.DETAIL_PRODUIT_GETLISTE(value.IdProduit, ClientSelected.IdClient);
                                    if (ClientSelected != null)
                                    {
                                        if (ClientSelected.Exonerere != null)
                                        {
                                            if (ClientSelected.Exonerere.CourtDesc == "part")
                                            {
                                                if (DetailListProdclientSelected != null && DetailListProdclientSelected.Count > 0)
                                                {
                                                    AfficheStatutProduit = "";
                                                    IsdetailExiste = true;
                                                    DetailProdEnable = true;
                                                    IsPrixunitReadOnly = false;
                                                }
                                                else
                                                {
                                                    AfficheStatutProduit = "Le détail de ce produit est nécessaire";
                                                    IsdetailExiste = false;
                                                    DetailProdEnable = false;
                                                    IsPrixunitReadOnly = true;
                                                }
                                            }
                                            else
                                            {
                                                IsdetailExiste = true;
                                                DetailProdEnable = true;
                                            }
                                        }
                                    }

                                }//fin if
                                else
                                {
                                    PrixUnitaireselected = 0;
                                    ProduitSelected = null;
                                }


                            }
                            else
                            {
                                if (value.PrixUnitaire > 0)
                                {
                                    PrixUnitaireselected = value.PrixUnitaire;
                                    if (LigneFacture != null)
                                        LigneFacture.Quantite = 0;
                                }
                                DetailListProdclientSelected = detailService.DETAIL_PRODUIT_GETLISTE(value.IdProduit, ClientSelected.IdClient);
                                if (ClientSelected != null)
                                {
                                    if (ClientSelected.Exonerere != null)
                                    {
                                        if (ClientSelected.Exonerere.CourtDesc == "part")
                                        {
                                            if (DetailListProdclientSelected != null && DetailListProdclientSelected.Count > 0)
                                            {
                                                AfficheStatutProduit = "";
                                                IsdetailExiste = true;
                                                DetailProdEnable = true;
                                                IsPrixunitReadOnly = false;
                                            }
                                            else
                                            {
                                                AfficheStatutProduit = "Le détail de ce produit est nécessaire";
                                                IsdetailExiste = false;
                                                DetailProdEnable = false;
                                                IsPrixunitReadOnly = true;
                                            }
                                        }
                                        else
                                        {
                                            IsdetailExiste = true;
                                            DetailProdEnable = true;
                                        }
                                    }
                                }


                            }
                        }
                        else
                        {

                            if (value.PrixUnitaire > 0)
                            {
                                PrixUnitaireselected = value.PrixUnitaire;
                                if (LigneFacture != null)
                                    LigneFacture.Quantite = 0;
                            }

                            DetailListProdclientSelected = detailService.DETAIL_PRODUIT_GETLISTE(value.IdProduit, ClientSelected.IdClient);
                            if (ClientSelected != null)
                            {
                                if (ClientSelected.Exonerere != null)
                                {
                                    if (ClientSelected.Exonerere.CourtDesc == "part")
                                    {
                                        if (DetailListProdclientSelected != null && DetailListProdclientSelected.Count > 0)
                                        {
                                            AfficheStatutProduit = "";
                                            IsdetailExiste = true;
                                            DetailProdEnable = true;
                                            IsPrixunitReadOnly = false;
                                            

                                        }
                                        else
                                        {
                                            AfficheStatutProduit = "Le détail de ce produit est nécessaire";
                                            IsdetailExiste = false;
                                            DetailProdEnable = false;
                                            IsPrixunitReadOnly = true;
                                        }
                                    }
                                    else
                                    {
                                        IsdetailExiste = true;
                                        DetailProdEnable = true;
                                    }
                                }
                            }

                        }
                    }
                    else
                    {
                        AfficheStatutProduit = string.Empty;
                        if (LigneFacture!=null )
                        LigneFacture.Quantite = 0;
                        PrixUnitaireselected = 0;
                    }

                }




                ActualFacture = FatureCurrent;

                if (isupdateinvoice)
                {
                    if (ClientSelected != null)
                    {

                        if (ClientSelected.Exonerere != null)
                        {
                            if (ClientSelected.Exonerere.CourtDesc == "exo")
                                ExonereEnable = false;
                            else if (ClientSelected.Exonerere.CourtDesc == "non")
                                ExonereEnable = false;
                            else ExonereEnable = true;

                            //if (ClientSelected.Exonerere.ID == 122002)
                            //    ExonereEnable = false;
                            //else if (ClientSelected.Exonerere.ID == 122003)
                            //    ExonereEnable = false;
                            //else ExonereEnable = true;


                        }
                    }

                }
                else
                {
                    DetailProdEnable = true;
                }

                Lignecourante = new LigneCommand();




                OnPropertyChanged("ProduitSelected");
            }
        }

        public ObservableCollection<ProduitModel> ProduiList
        {
            get { return _produiList; }
            set
            {
                _produiList = value;
                OnPropertyChanged("ProduiList");
            }
        }

        public ObservableCollection<ProduitModel> CacheProduiList
        {
            get { return _cacheProduiList; }
            set
            {
                _cacheProduiList = value;
                OnPropertyChanged("CacheProduiList");
            }
        }

        public ProduitModel OldProduitSelected
        {
            get { return oldProduitSelected; }
            set
            {
                oldProduitSelected = value;
                //ProduitSelected = new ProduitModel();
                //ProduitSelected.PrixUnitaire = value.PrixUnitaire;
                //ProduitSelected.IdProduit = value.IdProduit;
                // PrixUnitaireselected = value.PrixUnitaire;
                isupdateinvoice = true;
                this.OnPropertyChanged("OldProduitSelected");
            }
        }
        #endregion

        #region TAXES REGION

        public bool IsProrataEnabled
        {
            get { return isProrataEnabled; }
            set
            {
                isProrataEnabled = value;


                this.OnPropertyChanged("IsProrataEnabled");
            }
        }

        public bool Isproratable
        {
            get { return isproratable; }
            set
            {
                isproratable = value;
                this.OnPropertyChanged("Isproratable");
            }
        }

        public bool ExonereEnable
        {
            get { return exonereEnable; }
            set
            {
                exonereEnable = value;
                OnPropertyChanged("ExonereEnable");
            }
        }


        public List<DeviseModel> DeviseList
        {
            get { return deviseList; }
            set
            {
                deviseList = value;
                OnPropertyChanged("DeviseList");
            }
        }

        public DeviseModel DeviseSelected
        {
            get { return _deviseSelected; }
            set
            {
                _deviseSelected = value;
                OnPropertyChanged("DeviseSelected");
            }
        }

        public DeviseModel DeviseConvert
        {
            get { return _deviseConvert; }
            set
            {
                _deviseConvert = value;
                OnPropertyChanged("DeviseConvert");
            }
        }

        public TaxeModel TaxeSelected
        {
            get { return _taxeSelected; }
            set
            {
                _taxeSelected = value;
                OnPropertyChanged("TaxeSelected");
            }
        }

        public List<TaxeModel> TaxeList
        {
            get { return _taxeList; }
            set
            {
                _taxeList = value;
                OnPropertyChanged("TaxeList");
            }
        }

        public TaxeModel TaxePorataSelected
        {
            get { return _taxePorataSelected; }
            set
            {
                _taxePorataSelected = value;
                OnPropertyChanged("TaxePorataSelected");
            }
        }

        public List<TaxeModel> TaxePorataList
        {
            get { return _taxeList; }
            set
            {
                _taxeList = value;
                OnPropertyChanged("TaxePorataList");
            }
        }

        #endregion

        public bool IsPrestation
        {
            get { return isPrestation; }
            set
            {
                isPrestation = value;
                OnPropertyChanged("IsPrestation");
            }
        }


        public string Filtertexte
        {
            get { return filtertexte; }
            set
            {
                filtertexte = value;
                // if (value != null || value != string.Empty)
                // filter(value);

                this.OnPropertyChanged("Filtertexte");
            }
        }




        #region ENTETE FACTURE ERGION
        public List<EnteteFactureModel> EnteteList
        {
            get { return _enteteList; }
            set
            {
                _enteteList = value;
                this.OnPropertyChanged("EnteteList");
            }
        }

        public EnteteFactureModel EnteteSelected
        {
            get { return _enteteSelected; }
            set
            {
                _enteteSelected = value;
                this.OnPropertyChanged("EnteteSelected");
            }
        }

        #endregion

        #region LIGNE COMMANDE REGION

        public List<LigneFactureModel> LigneFatureListe
        {
            get { return _LigneFatureListe; }
            set
            {
                _LigneFatureListe = value;
                if (value != null && value.Count > 0)
                    ConvertDataItems(value);

                this.OnPropertyChanged("LigneFatureListe");
            }
        }

        public LigneFactureModel LigneFacture
        {
            get { return _ligneFacture; }
            set
            {
                _ligneFacture = value;

                this.OnPropertyChanged("LigneFacture");
            }
        }



        public LigneFactureModel OldLigneFacture
        {
            get { return oldLigneFacture; }
            set
            {
                oldLigneFacture = value;
                isupdateinvoice = true;
                LigneFacture = value;
                if (value.IdDetailProduit == 0)
                {
                    PrixUnitaireselected = (decimal)value.PrixUnitaire;
                }
                else
                {
                    //    if (value.PrixUnitaire == 0)
                    //        PrixUnitaireselected = (decimal)detailService.DETAIL_PRODUIT_GETBYID(value.IdDetailProduit).Prixunitaire;
                    //    else PrixUnitaireselected = (decimal)value.PrixUnitaire;
                    PrixUnitaireselected = (decimal)value.PrixUnitaire;
                }
                ActualFacture = FatureCurrent;

                IsItemsExonere = value.Exonere;
                this.OnPropertyChanged("OldLigneFacture");
            }
        }





        public List<LigneCommand> LigneCommandList
        {
            get { return ligneCommandList; }
            set
            {
                ligneCommandList = value;
                OnPropertyChanged("LigneCommandList");
            }
        }

        public LigneCommand Lignecourante
        {
            get { return lignecourante; }
            set
            {
                lignecourante = value;
                OnPropertyChanged("Lignecourante");
            }
        }
        #endregion

        #endregion

        #region ICOMMAND


        public ICommand SaveCommand
        {
            get
            {
                if (this.saveCommand == null)
                {
                    this.saveCommand = new RelayCommand(param => this.canSaveFacture(param), param => this.canExecuteSavefacture());
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
                    this.newCommand = new RelayCommand(param => this.canNew(), param => this.canExecutenew());
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
                    this.deleteCommand = new RelayCommand(param => this.canDelete(), param => this.canExecuteDeletefacture());
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

        public RelayCommand AddDatasCommand
        {
            get
            {
                if (this.addDatasCommand == null)
                {
                    this.addDatasCommand = new RelayCommand(param => this.canAddData(), param => this.canExecuteAddDatas());
                }
                return this.addDatasCommand;
            }

        }

        public RelayCommand ObjectSelectedCommand
        {
            get
            {
                if (this.objectSelectedCommand == null)
                {
                    this.objectSelectedCommand = new RelayCommand(param => this.canObjectselected(param));
                }
                return this.objectSelectedCommand;
            }

        }

        public RelayCommand ShowTaxesCommand
        {
            get
            {
                if (this.showTaxesCommand == null)
                {
                    this.showTaxesCommand = new RelayCommand(param => this.canShowTaxe());
                }
                return this.showTaxesCommand;
            }

        }
        //
        public RelayCommand PrintCommand
        {
            get
            {
                if (this.printCommand == null)
                {
                    this.printCommand = new RelayCommand(param => this.canPrint(), param => canExecutePrint());
                }
                return this.printCommand;
            }

        }

        // 

        public RelayCommand NewlineCommand
        {
            get
            {
                if (this.newlineCommand == null)
                {
                    this.newlineCommand = new RelayCommand(param => this.canNewLine(), param => canExecuteAjout());
                }
                return this.newlineCommand;
            }

        }

        public RelayCommand DeletelineCommand
        {
            get
            {
                if (this.deletelineCommand == null)
                {
                    this.deletelineCommand = new RelayCommand(param => this.canDeleteLine(), param => canExecuteDeleteline());
                }
                return this.deletelineCommand;
            }

        }


        // private RelayCommand deletelineCommand;
        //
        #endregion

        #region METHODS

        #region LOAD REGION

        void loadnewinvoiceAutorization()
        {

            // on recupere la page
            // initialisation du statut(statut creation)
            StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(1);
            if (newStatut != null)
            {
                CurrentStatut = newStatut;
            }


        }

        void newShowInformation()
        {
            if (UserConnected != null)
            {
                if (UserConnected.Profile.ShortName.ToLower() == "admin" || UserConnected.Profile.ShortName.ToLower() == "admin")
                {
                    // administrateur ou validateur
                    ChkDateValidate = false;
                    IsEnabledchkDateValidate = false;

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;


                }

                if (UserConnected.Profile.ShortName == "op")
                {
                    // operateur principale
                    ChkDateValidate = false;
                    IsEnabledchkDateValidate = false;

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                }

                if (UserConnected.Profile.ShortName == "tt")
                {
                    // testeur
                    ChkDateValidate = false;
                    IsEnabledchkDateValidate = true;
                    IsEnableOutValidation = false;
                    IsEnableOutVisible = true;
                    IsEnabledSuspendedValidation = false;
                    IsEnabledSuspendedVisible = false;

                }
                if (UserConnected.Profile.ShortName == "ops")
                {
                    //operateur sortie
                    ChkDateValidate = false;
                    IsEnabledchkDateValidate = true;
                    IsEnableOutValidation = false;
                    IsEnableOutVisible = true;
                    IsEnabledSuspendedValidation = false;
                    IsEnabledSuspendedVisible = false;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;
                }
            }
        }



        void loadautorisation()
        {
            //if (ClientSelected != null)
            //    CmbEnabled = true;
            //else CmbEnabled = false;

            if (UserConnected != null)
            {
                if (UserConnected.Profile.ShortName.ToLower() == "admin" || UserConnected.Profile.ShortName.ToLower() == "sadmin")
                {
                    // administrateur
                    ChkDateValidate = true;
                    IsEnabledchkDateValidate = true;

                    IsEnableOutValidation = true;
                    IsEnabledSuspendedValidation = true;


                }

                if (UserConnected.Profile.ShortName == "op")
                {
                    // operateur principale
                    ChkDateValidate = false;
                    IsEnabledchkDateValidate = false;

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                }

                if (UserConnected.Profile.ShortName == "tt")
                {
                    // testeur
                    ChkDateValidate = true;
                    IsEnabledchkDateValidate = true;

                    IsEnableOutValidation = true;
                    IsEnabledSuspendedValidation = true;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                }
                if (UserConnected.Profile.ShortName == "ops")
                {
                    //operateur sortie
                    ChkDateValidate = false;
                    IsEnabledchkDateValidate = false;

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;
                }
            }
        }


        void loadOperationValidation()
        {
            StatutModel currentStatut = FatureCurrent.CurrentStatut ?? statutservice.STATUT_FACTURE_GETLISTEByID(FatureCurrent.IdStatut);

            if (currentStatut != null && FatureCurrent.IdFacture != 0)
            {
                if (int.Parse(currentStatut.CourtDesc) == 1)
                {

                    loadautorisation();


                    IsValidateDate = false;

                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;
                    // valeurDalidation = "encours";
                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                    AfficheStatut = "Encours Validation:";
                    FatureCurrent.DateCloture = FatureCurrent.DateCreation;

                    if (UserConnected.Profile.ShortName == "op" ||
                        UserConnected.Profile.ShortName == "admin" ||
                        UserConnected.Profile.ShortName == "sadmin")
                        IsEnabledchkDateValidate = true;

                    operation = "validation";
                    isOperationpossible = true;

                }
                else if (int.Parse(currentStatut.CourtDesc) == 2)
                {
                    //if (FatureCurrent.DateCloture == null)
                    //{
                    loadautorisation();
                    //valeurDalidation = "validation";
                    AfficheStatut = "A Valider:";
                    IsValidateDate = false;

                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;

                    BtnAddDetailEnable = true;
                    BtnDelDetailEnable = true;

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;

                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                    FatureCurrent.DateCloture = FatureCurrent.DateCreation;

                    if (UserConnected.Profile.ShortName == "op" ||
                     UserConnected.Profile.ShortName == "admin" ||
                     UserConnected.Profile.ShortName == "sadmin")
                        IsEnabledchkDateValidate = true;
                    else IsEnabledchkDateValidate = false;


                    operation = "validation";
                    isOperationpossible = true;


                }




                if (int.Parse(currentStatut.CourtDesc) == 3)
                {

                    AfficheStatut = "Dévalider:";
                    loadautorisation();

                    IsValidateDate = true;
                    FatureCurrent.DateCloture = FatureCurrent.DateCloture;
                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;
                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    isdatevalidationsexist = true;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                    if (UserConnected.Profile.ShortName == "op" ||
                           UserConnected.Profile.ShortName == "admin" ||
                           UserConnected.Profile.ShortName == "sadmin")
                        IsEnabledchkDateValidate = true;
                    else IsEnabledchkDateValidate = false;

                    if (UserConnected.Profile.ShortName == "admin" || UserConnected.Profile.ShortName == "sadmin")
                    {
                        BtnAddDetailEnable = true;

                    }

                    operation = "validation";
                    isOperationpossible = false;

                }

                if (int.Parse(currentStatut.CourtDesc) == 4)
                {
                    //sortie
                    loadautorisation();
                    BtnAddDetailEnable = false;
                    IsEnabledchkDateValidate = false;
                    if (FatureCurrent.DateCloture != null)
                    {
                        IsValidateDate = true;
                        //FatureCurrent.DateCloture
                    }
                    isdatevalidationsexist = true;
                    ChkDateOutValidate = true;
                    ChkDateSuspended = false;

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;

                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                    if (UserConnected.Profile.ShortName == "admin" || UserConnected.Profile.ShortName == "sadmin")
                        IsEnabledchkDateValidate = true;
                    else IsEnabledchkDateValidate = false;
                    //valeurDalidation = "devalide";
                    AfficheStatut = "Devalider:";
                    operation = "validation";
                    isOperationpossible = false;
                }

                if (int.Parse(currentStatut.CourtDesc) == 5)
                {
                    //supendu
                    loadautorisation();
                    BtnAddDetailEnable = false;
                    BtnDelDetailEnable = false;

                    ChkDateOutValidate = false;
                    ChkDateSuspended = true;

                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                    if (FatureCurrent.DateCloture != null)
                    {
                        IsValidateDate = true;
                        AfficheStatut = "Devalider:";
                        isdatevalidationsexist = true;
                    }
                    else
                    {
                        FatureCurrent.DateCloture = FatureCurrent.DateCreation;
                        AfficheStatut = "Encours Validation:";
                        isdatevalidationsexist = false;
                    }

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;

                    if (UserConnected.Profile.ShortName == "admin" || UserConnected.Profile.ShortName == "sadmin")
                        IsEnabledchkDateValidate = true;
                    else IsEnabledchkDateValidate = false;
                    //ChkDateSuspended = true;
                    valeurDalidation = "devalide";



                    operation = "validation";
                    isOperationpossible = false;
                }


                if (int.Parse(currentStatut.CourtDesc) == 6)
                {
                    //non valable
                    loadautorisation();
                    BtnAddDetailEnable = false;

                    if (FatureCurrent.DateCloture != null)
                    {
                        IsValidateDate = true;
                        AfficheStatut = "Devalider:";
                        isdatevalidationsexist = true;
                    }
                    else
                    {
                        FatureCurrent.DateCloture = FatureCurrent.DateCreation;
                        AfficheStatut = "Encours Validation:";
                        isdatevalidationsexist = false;
                    }

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;

                    ChkNonValable = true;
                    IsEnabledNonValable = false;

                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;

                    if (UserConnected.Profile.ShortName == "admin" || UserConnected.Profile.ShortName == "sadmin")
                        IsEnabledchkDateValidate = true;
                    else IsEnabledchkDateValidate = false;
                    //ChkDateSuspended = true;
                    valeurDalidation = "devalide";

                    operation = "validation";
                    isOperationpossible = false;


                }



            }


        }



        void ConvertDataItems(List<LigneFactureModel> items)
        {
            //LigneCommandList
            double ligneht = 0;
            double ligneht_lign_exo = 0;
            string modeExoneration = string.Empty;

            if (FatureCurrent.CurrentClient.Exonerere == null)
            {
                ExonerationModel exoService = new ExonerationModel();
                modeExoneration = exoService.EXONERATION_SELECTById(FatureCurrent.CurrentClient.IdExonere).CourtDesc;
            }
            else modeExoneration = FatureCurrent.CurrentClient.Exonerere.CourtDesc;


            List<LigneCommand> liste = new List<LigneCommand>();
            foreach (var newLine in items)
            {
                LigneCommand ligne = new LigneCommand();
                ligne.ID = newLine.IdLigneFacture;
                ligne.IdProduit = newLine.IdProduit;
                ligne.Description = newLine.Description;
                ligne.Idetail = newLine.IdDetailProduit;
                ligne.quantite = (decimal)newLine.Quantite;
                ligne.IdSite = newLine.IdSite;
                if (newLine.IdDetailProduit > 0)
                {
                    if (newLine.PrixUnitaire == 0)
                    {
                        ligne.PrixUnit = (decimal)detailService.DETAIL_PRODUIT_GETBYID(newLine.IdDetailProduit).Prixunitaire;
                    }
                    else ligne.PrixUnit = (decimal)newLine.PrixUnitaire;
                }

                else
                {
                    if (newLine.PrixUnitaire == 0)
                    {
                        ligne.PrixUnit = produitservice.Produit_SELECTBY_ID(newLine.IdProduit).PrixUnitaire;
                    }
                    else ligne.PrixUnit = (decimal)newLine.PrixUnitaire;
                }


                ligne.Produit = produitservice.Produit_SELECTBY_ID(newLine.IdProduit).Libelle;
                ligne.montantHt = newLine.MontanHT;
                ligne.estExonere = newLine.Exonere;

                if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2)
                    ligne.IsdeletedEnabled = true;
                else
                    ligne.IsdeletedEnabled = false;
                if (modeExoneration == "exo")
                {
                    ligne.LibelleDetail = "";
                }
                else if (modeExoneration == "non")
                {
                    ligne.LibelleDetail = "";
                }
                else if (modeExoneration == "part")
                {

                    decimal [] tab = AllTech.FrameWork.Global.Utils.GetTaxe_From(newLine, TaxePorataSelected.Taux,
                                      FatureCurrent.CurrentTaxe.Taux, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                    if (tab != null)
                    {
                        if (tab[0] != 0)
                            ligne.LibelleDetail = string.Format("Tva :{0}", tab[0]);
                        else if (tab[1] != 0) ligne.LibelleDetail = string.Format("Prorata :{0}", tab[1]); ;


                    }
                }



                liste.Add(ligne);

                #region MONTAN FACTURE



                if (!string.IsNullOrEmpty(modeExoneration))
                {
                    object[] tabretour = null;


                    if (modeExoneration == "exo")
                    {
                        tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(items, TaxePorataSelected.Taux,
                            FatureCurrent.CurrentTaxe.Taux, true, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));

                    }
                    else
                        if (modeExoneration == "non")
                        {
                            tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(items, TaxePorataSelected.Taux,
                                FatureCurrent.CurrentTaxe.Taux, false, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));

                        }
                        else
                            if (modeExoneration == "part")
                            {
                                tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(items, TaxePorataSelected.Taux,
                                    FatureCurrent.CurrentTaxe.Taux, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));


                            }

                    if (tabretour != null)
                    {
                        Montanttva = tabretour[0].ToString();
                        Ftotaltva = double.Parse(tabretour[1].ToString());
                        MontantProrata = tabretour[2].ToString();
                        FTotalProrata = double.Parse(tabretour[3].ToString());
                        FTotalHt = double.Parse(tabretour[4].ToString());
                        FtotalTTC = double.Parse(tabretour[5].ToString());
                    }


                }
                #endregion



            }



            LigneCommandList = liste;
            CacheLigneCommandList = LigneCommandList;

        }

        void loadLanguageLientselejctInfo()
        {
            if (ClientSelected != null)
            {
                try
                {
                    ObjetList = objetservice.OBJECT_FACTURE_GETLISTEByCLIENT(societeCourante.IdSociete, ClientSelected.IdClient);
                    // ObjetList = objetservice.OBJECT_FACTURE_GETLISTEByIdLanguage(ClientSelected.IdLangue, societeCourante.IdSociete);
                    ExploitationList = exploitationservice.EXPLOITATION_FACTURE_CLIENT_LIST(societeCourante.IdSociete, ClientSelected.IdClient);

                    //ProduiList = produitservice.Produit_SELECTBY_ID_Language(ClientSelected.IdLangue, societeCourante.IdSociete);
                    //CacheProduiList = ProduiList;
                    NewDetailListeProduit = detailService.DETAIL_PRODUIT_BYCLIENT(ClientSelected.IdClient, societeCourante.IdSociete);
                    IdProduits = new HashSet<int>();
                    if (NewDetailListeProduit != null)
                    {
                        foreach (DetailProductModel det in NewDetailListeProduit)
                            IdProduits.Add(det.IdProduit);
                        ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
                        foreach (int id in IdProduits)
                            newListeProduie.Add(NewDetailListeProduit.Find(p => p.IdProduit == id).Produit);
                        ProduiList = newListeProduie;
                        CacheProduiList = ProduiList;
                    }
                    else
                    {
                        MessageBox.Show("Ce Client ne Posssède pad de produits rattachés", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                    DeviseConvert = deviseService.Devise_SELECTById(ClientSelected.IdDeviseFact, societeCourante.IdSociete);
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "Warning";
                    view.ViewModel.Message = ex.Message;
                    view.ShowDialog();
                }
            }
        }

        void loadDatas()
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                    DeviseSelected = deviseService.Devise_SELECTById(ParametersDatabase.IdDevise, societeCourante.IdSociete);
                    TaxeSelected = taxeService.Taxe_SELECTById(ParametersDatabase.Idtva, societeCourante.IdSociete);
                    if (issaveUpdatedata)
                        ClientList = clientservice.CLIENT_GETLISTE(idSocieteCourant);
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
                    view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();

        }

        void loadDataBaseData()
        {
            BackgroundWorker worker = new BackgroundWorker();
            this.IsBusy = true;
            ProgressBarVisibility = true;
            worker.DoWork += (o, args) =>
            {
                try
                {
                    ParametersDatabase = GlobalDatas.dataBasparameter;
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
                    view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                }
                else
                {
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    ProgressBarVisibility = false;
                }
                //this.OnPropertyChanged("ListEmployees");
            };

            worker.RunWorkerAsync();

        }


        void loadFactureInformation()
        {
            //BackgroundWorker worker = new BackgroundWorker();
            //this.IsBusy = true;
            //ProgressBarVisibility = true;
            //worker.DoWork += (o, args) =>
            //{
            try
            {


                LigneFatureListe = ligneFactureService.LIGNE_FACTURE_BYIDFActure(FatureCurrent.IdFacture);
                DeviseConvert = deviseService.Devise_SELECTById(FatureCurrent.CurrentClient.IdDeviseFact, societeCourante.IdSociete);
                loadautorisation();
                loadOperationValidation();
            }
            catch (Exception ex)
            {
                //args.Result = ex.Message + " ;" + ex.InnerException;
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
            }


            //};
            //worker.RunWorkerCompleted += (o, args) =>
            //{
            //    if (args.Result != null)
            //    {
            //        CustomExceptionView view = new CustomExceptionView();
            //        view.Owner = Application.Current.MainWindow;
            //        view.ViewModel.Message = args.Result.ToString();
            //        view.ShowDialog();
            //        this.MouseCursor = null;
            //        this.IsBusy = false;
            //    }
            //    else
            //    {
            //        this.MouseCursor = null;
            //        this.IsBusy = false;
            //        ProgressBarVisibility = false;
            //    }
            //    //this.OnPropertyChanged("ListEmployees");
            //};

            //worker.RunWorkerAsync();

        }



        void getDatas(DataTable tbleListe, string libelle, string objet)
        {
            DataRow[] nlignes = tbleListe.Select(string.Format("Libelle like '{0}%'", libelle.Trim()));
            DataTable filterDatatable = tbleListe.Clone();
            foreach (DataRow rows in nlignes)
                filterDatatable.ImportRow(rows);

            if (objet == "tva")
            {
                TaxeList = null;
                List<TaxeModel> newListe = new List<TaxeModel>();
                foreach (DataRow row in filterDatatable.Rows)
                    newListe.Add(new TaxeModel { ID_Taxe = int.Parse(row[0].ToString()), Libelle = row[1].ToString(), Taux = row[2].ToString() });
                TaxeList = newListe;

            }

            if (objet == "porata")
            {
                TaxePorataList = null;
                List<TaxeModel> newListe = new List<TaxeModel>();
                foreach (DataRow row in filterDatatable.Rows)
                    newListe.Add(new TaxeModel { ID_Taxe = int.Parse(row[0].ToString()), Libelle = row[1].ToString(), Taux = row[2].ToString() });
                TaxePorataList = newListe;

            }
        }
        #endregion

        #region REGION OPERATIONS


        private void canShowTaxe()
        {
            WTaxeDevises vf = _container.Resolve<WTaxeDevises>();
            vf.Owner = Application.Current.MainWindow;
            vf.ShowDialog();
            TaxeList = taxeService.Taxe_SELECT(0, societeCourante.IdSociete );
            DeviseList = deviseService.Devise_SELECT(societeCourante.IdSociete);


        }

        #region LIGNE FACTURE AJOUT

        bool canExecuteAjout()
        {
            return LigneFacture != null ? (IsdetailExiste ? true : false) : false;
        }

        double totaligne_ht_tva = 0;
        double totalihne_ht_prorata = 0;

        // ajout nouvelle ligne facture crée sur le grid
        private void canNewLine()
        {
            decimal qtiteTeste = 0;
            if (ProduitSelected != null)
            {
                if (LigneFacture != null)
                {
                    if (decimal.TryParse(LigneFacture.Quantite.ToString(), out  qtiteTeste))
                        if (qtiteTeste > 0)
                        {

                            if (CacheLigneCommandList == null)
                            {
                                CacheLigneCommandList = new List<LigneCommand>();
                            }
                            Lignecourante = new LigneCommand();

                            if (!isupdateinvoice)
                            {

                                #region NEW ITEM

                                Lignecourante.ID = 0;
                                Lignecourante.Description = LigneFacture.Description;
                                Lignecourante.Produit = ProduitSelected.Libelle;
                                Lignecourante.IdProduit = ProduitSelected.IdProduit;
                                Lignecourante.Idetail = idDetailPode;

                                Lignecourante.PrixUnit = PrixUnitaireselected;
                                Lignecourante.estExonere = IsItemsExonere;
                                Lignecourante.quantite = LigneFacture.Quantite;

                                Lignecourante.tva = ""; //taxeService.Taxe_SELECTById(ParametersDatabase.Idtva).Taux;
                                Lignecourante.situation = 1;
                                Lignecourante.IsdeletedEnabled = false;

                                if (idDetailPode == 0)
                                {
                                    Lignecourante.montantHt = (Lignecourante.quantite * Lignecourante.PrixUnit);
                                    Lignecourante.Idetail = 0;
                                }
                                else
                                {
                                    Lignecourante.montantHt = (Lignecourante.quantite * detailPrixunit);
                                    Lignecourante.Idetail = idDetailPode;
                                }


                                #region MONTANT FACTURE

                                double valprorata = 0;
                                double valTva = 0;

                                FTotalHt += (double)Lignecourante.montantHt;

                                if (clientCourant.Exonerere != null)
                                {
                                    if (clientCourant.Exonerere.CourtDesc == "non")
                                    {
                                        //client non exonere, paie taxe, pas de prorara
                                        if (clientCourant.Llangue != null)
                                        {
                                            if (clientCourant.Llangue.Id == 1)
                                            {
                                                valTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace ('.',',').Trim(),
                                                CultureInfo.CreateSpecificCulture("fr-FR")) / 100);

                                            }
                                            else
                                            {
                                                valTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                               CultureInfo.CreateSpecificCulture("en-US")) / 100);

                                            }

                                            Lignecourante.LibelleDetail = "";
                                        }

                                        double calcul = valTva * FTotalHt;
                                        calcul = Math.Round(calcul, 0);
                                        Ftotaltva = calcul;
                                        FTotalProrata += 0;
                                        MontantProrata = "";

                                        Montanttva = TaxeSelected.Taux;
                                        FtotalTTC = (Ftotaltva + FTotalHt);
                                    }
                                    else if (clientCourant.Exonerere.CourtDesc == "exo")
                                    {
                                        Montanttva = "(exonere)" + TaxeSelected.Taux;
                                        Ftotaltva += 0;
                                        double valp = 0;
                                        MontantProrata = TaxePorataSelected.Taux;

                                        if (clientCourant.Llangue != null)
                                        {
                                            if (ClientSelected.Llangue.Id == 1)
                                            {
                                                valprorata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                CultureInfo.CreateSpecificCulture("fr-FR")) / 100);

                                            }
                                            else if (clientCourant.Llangue.Id == 2)
                                            {
                                                valprorata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                               CultureInfo.CreateSpecificCulture("en-US")) / 100);

                                            }

                                            Lignecourante.LibelleDetail = "";
                                        }


                                        double calcul = valprorata * FTotalHt;
                                        calcul = Math.Round(calcul, 0);
                                        FTotalProrata = calcul;

                                        FtotalTTC = (FTotalHt + FTotalProrata);
                                    }
                                    else if (clientCourant.Exonerere.CourtDesc == "part")
                                    {
                                        //exonereation partiel
                                        //Montanttva = TaxeSelected.Taux ;

                                        if (IsItemsExonere)
                                        {
                                            //pas de taxe,  prorata

                                            if (Isproratable)
                                            {
                                                totalihne_ht_prorata += (double)Lignecourante.montantHt;
                                                MontantProrata = TaxePorataSelected.Taux;
                                                totaligne_ht_tva += 0;
                                                if (string.IsNullOrEmpty(Montanttva))
                                                    Montanttva = "(exonere)" + TaxeSelected.Taux;
                                            }
                                            else
                                            {
                                                totaligne_ht_tva += 0;
                                                totalihne_ht_prorata += 0;
                                                if (string.IsNullOrEmpty(Montanttva))
                                                    Montanttva = "(exonere)" + TaxeSelected.Taux;
                                                if (string.IsNullOrEmpty(MontantProrata))
                                                    MontantProrata = "";
                                            }
                                        }
                                        else if (!IsItemsExonere)
                                        {
                                            if (string.IsNullOrEmpty(MontantProrata))
                                                MontantProrata = "";

                                            totaligne_ht_tva += (double)Lignecourante.montantHt;
                                            totalihne_ht_prorata += 0;
                                            Montanttva = TaxeSelected.Taux;
                                        }



                                        double calculProrata = 0;
                                        double calculTva = 0;

                                        if (ClientSelected.Llangue != null)
                                        {
                                            if (ClientSelected.Llangue.Id == 1)
                                            {
                                                if (Isproratable)
                                                {
                                                    calculProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                    CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * totalihne_ht_prorata;
                                                    FTotalProrata = Math.Round(calculProrata, 0);
                                                    Lignecourante.LibelleDetail = string.Format("Prorata :{0}", Math.Round(calculProrata, 0));
                                                }
                                                else
                                                {
                                                    calculProrata = 0;
                                                    if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                        Lignecourante.LibelleDetail = "";
                                                }

                                                if (!IsItemsExonere)
                                                {
                                                    calculTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                        CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * totaligne_ht_tva;
                                                    Ftotaltva = Math.Round(calculTva, 0);
                                                    Lignecourante.LibelleDetail = string.Format("Tva :{0}", Math.Round(calculTva, 0));
                                                }
                                                else
                                                {
                                                    calculTva = 0;
                                                    if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                        Lignecourante.LibelleDetail = "";
                                                }
                                            }
                                            else if (ClientSelected.Llangue.Id == 2)
                                            {
                                                if (Isproratable)
                                                {
                                                    calculProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                   CultureInfo.CreateSpecificCulture("en-US")) / 100) * totalihne_ht_prorata;
                                                    FTotalProrata = Math.Round(calculProrata, 0);

                                                    Lignecourante.LibelleDetail = string.Format("Prorata :{0}", Math.Round(calculProrata, 0));
                                                }
                                                else
                                                {
                                                    calculProrata = 0;
                                                    if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                        Lignecourante.LibelleDetail = "";
                                                }

                                                if (!IsItemsExonere)
                                                {
                                                    calculTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                       CultureInfo.CreateSpecificCulture("en-US")) / 100) * totaligne_ht_tva;
                                                    Ftotaltva = Math.Round(calculTva, 0);
                                                    Lignecourante.LibelleDetail = string.Format("Tva :{0}", Math.Round(calculTva, 0));
                                                }
                                                else
                                                {
                                                    calculTva = 0;
                                                    if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                        Lignecourante.LibelleDetail = "";
                                                }
                                            }
                                        }



                                        FtotalTTC = (FTotalHt + calculTva + calculProrata);


                                    }
                                }
                                #endregion



                                CacheLigneCommandList.Add(Lignecourante);
                                LigneCommandList = null;
                                LigneCommandList = CacheLigneCommandList;
                                LigneFacture = new LigneFactureModel(); ;
                                Lignecourante = null;
                                ProduitSelected = null;
                                PrixUnitaireselected = 0;
                                ActualFacture = FatureCurrent;
                                isupdateinvoice = false;
                                //ProduiList = null;

                                #endregion
                            }
                            else
                            {
                                // traitement des modifications de la ligne selectionner dans le grid

                                if (FatureCurrent.IdFacture > 0)
                                {
                                    #region UPDATE DATABASE ITEM
                                    // modification une ligne en base
                                    if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 3)
                                    {

                                        if (CacheLigneCommandList != null && CacheLigneCommandList.Count > 0)
                                        {
                                            LigneCommand ligne = CacheLigneCommandList.Find(l => l.ID == LigneFacture.IdLigneFacture);
                                            if (ligne != null)
                                            {
                                                CacheLigneCommandList.Remove(ligne);

                                                ligne.Description = LigneFacture.Description;
                                                ligne.tva = FatureCurrent.CurrentTaxe.Taux;
                                                // ligne.Idetail = OldLigneFacture.IdDetailProduit;
                                                if (LigneFacture.IdProduit != ProduitSelected.IdProduit)
                                                {
                                                    ligne.Produit = ProduitSelected.Libelle;
                                                    ligne.IdProduit = ProduitSelected.IdProduit;
                                                }
                                                else
                                                {
                                                    ligne.Produit = OldProduitSelected.Libelle;
                                                    ligne.IdProduit = OldProduitSelected.IdProduit;
                                                }

                                                ligne.quantite = LigneFacture.Quantite;

                                                if (LigneFacture.Exonere != IsItemsExonere)
                                                    ligne.estExonere = IsItemsExonere;
                                                else
                                                    ligne.estExonere = LigneFacture.Exonere;


                                                //if (PrixUnitaireselected <= 0)
                                                //    return;

                                                if (idDetailPode > 0)
                                                {
                                                    if (LigneFacture.PrixUnitaire == 0)
                                                        ligne.PrixUnit =detailService.DETAIL_PRODUIT_GETBYID(idDetailPode).Prixunitaire;
                                                    else ligne.PrixUnit = PrixUnitaireselected;

                                                    if ( idDetailPode != ligne.Idetail)
                                                        ligne.Idetail = idDetailPode;
                                                }
                                                else
                                                {
                                                    if (PrixUnitaireselected > 0)
                                                        ligne.PrixUnit = PrixUnitaireselected;
                                                    else
                                                        ligne.PrixUnit =LigneFacture.PrixUnitaire;
                                                    ligne.Idetail = 0;

                                                }
                                               


                                                ligne.IsdeletedEnabled = false;

                                                ligne.montantHt = ligne.quantite * ligne.PrixUnit;





                                                //traitement ancien produit
                                                double oldHt_ligne = CurOldQty * CuroldPu;

                                                double oldTva = 0;
                                                double oldProrata = 0;
                                                if (clientCourant.Exonerere != null)
                                                {
                                                    if (clientCourant.Exonerere.CourtDesc == "non")
                                                    {
                                                        if (clientCourant.Llangue.Id == 1)
                                                        {
                                                            oldTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                            CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * oldHt_ligne;
                                                            oldTva = Math.Round(oldTva, 0);
                                                        }
                                                        else
                                                        {
                                                            oldTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                           CultureInfo.CreateSpecificCulture("en-US")) / 100) * oldHt_ligne;
                                                            oldTva = Math.Round(oldTva, 0);
                                                        }
                                                    }
                                                    if (clientCourant.Exonerere.CourtDesc == "exo")
                                                    {
                                                        if (ClientSelected.Llangue.Id == 1)
                                                        {
                                                            oldProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                            CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * oldHt_ligne;
                                                            oldProrata = Math.Round(oldProrata, 0);
                                                        }
                                                        else if (clientCourant.Llangue.Id == 2)
                                                        {
                                                            oldProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                           CultureInfo.CreateSpecificCulture("en-US")) / 100) * oldHt_ligne;
                                                            oldProrata = Math.Round(oldProrata, 0);
                                                        }

                                                    }

                                                    if (clientCourant.Exonerere.CourtDesc == "part")
                                                    {
                                                        if (CurIsExonere)
                                                        {
                                                            if (CurIsProrata)
                                                            {
                                                                if (ClientSelected.Llangue.Id == 1)
                                                                {
                                                                    oldProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                                    CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * oldHt_ligne;
                                                                    oldProrata = Math.Round(oldProrata, 0);
                                                                }
                                                                else if (clientCourant.Llangue.Id == 2)
                                                                {
                                                                    oldProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                                   CultureInfo.CreateSpecificCulture("en-US")) / 100) * oldHt_ligne;
                                                                    oldProrata = Math.Round(oldProrata, 0);
                                                                }
                                                            }
                                                        }
                                                        else
                                                        {
                                                            if (clientCourant.Llangue.Id == 1)
                                                            {
                                                                oldTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                                CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * oldHt_ligne;
                                                                oldTva = Math.Round(oldTva, 0);
                                                            }
                                                            else
                                                            {
                                                                oldTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                               CultureInfo.CreateSpecificCulture("en-US")) / 100) * oldHt_ligne;
                                                                oldTva = Math.Round(oldTva, 0);
                                                            }
                                                        }
                                                    }

                                                    double oldTTC = oldHt_ligne + oldTva + oldProrata;
                                                    FTotalProrata -= oldProrata;
                                                    Ftotaltva -= (oldTva > 0 ? oldTva : 0);
                                                    FtotalTTC -= (oldTTC > 0 ? oldTTC : 0);
                                                    FTotalHt -= oldHt_ligne;



                                                    #region MONTANT FACTRE


                                                    double valTva = 0;
                                                    double valprorata = 0;

                                                    FTotalHt +=(double ) ligne.montantHt;

                                                    if (clientCourant.Exonerere.CourtDesc == "non")
                                                    {
                                                        //client non exonere

                                                        if (clientCourant.Llangue != null)
                                                        {
                                                            if (clientCourant.Llangue.Id == 1)
                                                            {
                                                                valTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                                CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * FTotalHt;
                                                                valTva = Math.Round(valTva, 0);
                                                            }
                                                            else
                                                            {
                                                                valTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                               CultureInfo.CreateSpecificCulture("en-US")) / 100) * FTotalHt;
                                                                valTva = Math.Round(valTva, 0);
                                                            }


                                                        }

                                                        // double calcul = (double.Parse(ligne.tva.Replace('%', ' ').Trim()) / 100) * FTotalHt;
                                                        Ftotaltva = valTva;
                                                        MontantProrata = "";
                                                        FTotalProrata += 0;
                                                        Montanttva = TaxeSelected.Taux;

                                                        FtotalTTC = (Ftotaltva + FTotalHt + FTotalProrata);
                                                        Lignecourante.LibelleDetail = "";
                                                    }
                                                    else if (clientCourant.Exonerere.CourtDesc == "exo")
                                                    {
                                                        Montanttva = "(exonere)" + TaxeSelected.Taux;
                                                        Ftotaltva += 0;

                                                        MontantProrata = TaxePorataSelected.Taux;

                                                        if (clientCourant.Llangue != null)
                                                        {
                                                            if (ClientSelected.Llangue.Id == 1)
                                                            {
                                                                valprorata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                                CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * FTotalHt;
                                                                valprorata = Math.Round(valprorata, 0);
                                                            }
                                                            else if (clientCourant.Llangue.Id == 2)
                                                            {
                                                                valprorata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                               CultureInfo.CreateSpecificCulture("en-US")) / 100) * FTotalHt;
                                                                valprorata = Math.Round(valprorata, 0);
                                                            }
                                                        }

                                                        // double calcul = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Trim()) / 100) * FTotalHt;
                                                        FTotalProrata = valprorata;

                                                        FtotalTTC = (Ftotaltva + FTotalHt + FTotalProrata);
                                                        Lignecourante.LibelleDetail = "";
                                                    }
                                                    else if (clientCourant.Exonerere.CourtDesc == "part")
                                                    {
                                                        Montanttva = TaxeSelected.Taux;



                                                        if (IsItemsExonere)
                                                        {
                                                            if (Isproratable)
                                                            {
                                                                totalihne_ht_prorata += (double)Lignecourante.montantHt;
                                                                MontantProrata = TaxePorataSelected.Taux;
                                                                totaligne_ht_tva += 0;
                                                                Montanttva = "";
                                                            }
                                                            else
                                                            {
                                                                totaligne_ht_tva += 0;
                                                                totalihne_ht_prorata += 0;
                                                                Montanttva = "";
                                                            }

                                                        }
                                                        else if (!IsItemsExonere)
                                                        {
                                                            //taxe et pas de prorata
                                                            if (!Isproratable)
                                                            {
                                                                totaligne_ht_tva += (double)Lignecourante.montantHt;
                                                                totalihne_ht_prorata += 0;
                                                                Montanttva = TaxeSelected.Taux;
                                                            }



                                                        }

                                                        double calculProrata = 0;
                                                        double calculTva = 0;

                                                        if (ClientSelected.Llangue != null)
                                                        {
                                                            if (ClientSelected.Llangue.Id == 1)
                                                            {
                                                                if (Isproratable)
                                                                {
                                                                    calculProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                                    CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * totalihne_ht_prorata;
                                                                    FTotalProrata = Math.Round(calculProrata, 0);
                                                                    Lignecourante.LibelleDetail = string.Format("Prorata :{0}", Math.Round(calculProrata, 0));
                                                                }
                                                                else
                                                                {
                                                                    calculProrata = 0;
                                                                    if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                                        Lignecourante.LibelleDetail = "";
                                                                }

                                                                if (!IsItemsExonere)
                                                                {
                                                                    calculTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                                        CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * totaligne_ht_tva;
                                                                    Ftotaltva = Math.Round(calculTva, 0);
                                                                    Lignecourante.LibelleDetail = string.Format("Tva :{0}", Math.Round(calculTva, 0));
                                                                }
                                                                else
                                                                {
                                                                    calculTva = 0;
                                                                    if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                                        Lignecourante.LibelleDetail = "";
                                                                }
                                                            }
                                                            else if (ClientSelected.Llangue.Id == 2)
                                                            {
                                                                if (Isproratable)
                                                                {
                                                                    calculProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                                   CultureInfo.CreateSpecificCulture("en-US")) / 100) * totalihne_ht_prorata;
                                                                    FTotalProrata = Math.Round(calculProrata, 0);
                                                                    Lignecourante.LibelleDetail = string.Format("Prorata :{0}", Math.Round(calculProrata, 0));
                                                                }
                                                                else
                                                                {
                                                                    calculProrata = 0;
                                                                    if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                                        Lignecourante.LibelleDetail = "";
                                                                }

                                                                if (!IsItemsExonere)
                                                                {
                                                                    calculTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                                       CultureInfo.CreateSpecificCulture("en-US")) / 100) * totaligne_ht_tva;
                                                                    Ftotaltva = Math.Round(calculTva, 0);
                                                                    Lignecourante.LibelleDetail = string.Format("Tva :{0}", Math.Round(calculTva, 0));
                                                                }
                                                                else
                                                                {
                                                                    calculTva = 0;
                                                                    if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                                        Lignecourante.LibelleDetail = "";
                                                                }
                                                            }
                                                        }

                                                        FtotalTTC = (Ftotaltva + FTotalHt + FTotalProrata);


                                                    }

                                                    #endregion
                                                }

                                              
                                                CacheLigneCommandList.Add(ligne);

                                            }


                                            LigneCommandList = null;
                                            LigneCommandList = CacheLigneCommandList;
                                            LigneFacture = null;
                                            PrixUnitaireselected = 0;
                                            IsItemsExonere = false;
                                            ExonereEnable = false;
                                            isupdateinvoice = false;
                                        }
                                    }
                                    #endregion
                                }
                                else
                                {
                                    #region UPDATE NEW ITEM


                                    // modification dune ligne non en core sauvegarder
                                    if (CacheLigneCommandList != null && CacheLigneCommandList.Count > 0)
                                    {

                                        LigneCommand ligne = CacheLigneCommandList.Find(l => l.IdProduit == LigneFacture.IdProduit);
                                        if (ligne != null)
                                        {
                                            CacheLigneCommandList.Remove(ligne);

                                            ligne.Description = LigneFacture.Description;
                                            ligne.tva = "";
                                            // ligne.Idetail = OldLigneFacture.IdDetailProduit;
                                            if (LigneFacture.IdProduit != ProduitSelected.IdProduit)
                                            {
                                                ligne.Produit = ProduitSelected.Libelle;
                                                ligne.IdProduit = ProduitSelected.IdProduit;
                                            }
                                            else
                                            {
                                                //ligne.Produit = oldLigneFacture.
                                                ligne.Produit = OldProduitSelected.Libelle;
                                                ligne.IdProduit = OldProduitSelected.IdProduit;
                                            }

                                            ligne.quantite = (decimal)LigneFacture.Quantite;

                                           

                                            if (idDetailPode > 0)
                                            {
                                                if (LigneFacture.PrixUnitaire == 0)
                                                    ligne.PrixUnit = (decimal)detailService.DETAIL_PRODUIT_GETBYID(idDetailPode).Prixunitaire;
                                                else ligne.PrixUnit = PrixUnitaireselected;

                                                if (idDetailPode != ligne.Idetail)
                                                    ligne.Idetail = idDetailPode;
                                            }
                                            else
                                            {
                                                if (PrixUnitaireselected > 0)
                                                    ligne.PrixUnit = PrixUnitaireselected;
                                                else
                                                    ligne.PrixUnit = (decimal)LigneFacture.PrixUnitaire;
                                                ligne.Idetail = 0;
                                            }

                                            ligne.IsdeletedEnabled = false;

                                            ligne.montantHt =ligne.quantite * ligne.PrixUnit;



                                            string modeExoneration = string.Empty;

                                            //traitement ancien produit
                                            double oldHt_ligne = CurOldQty * CuroldPu;

                                            double oldTva = 0;
                                            double oldProrata = 0;
                                            if (clientCourant.Exonerere == null)
                                            {
                                                ExonerationModel exoService = new ExonerationModel();
                                                modeExoneration = exoService.EXONERATION_SELECTById(clientCourant.IdExonere).CourtDesc;
                                            }
                                            else modeExoneration = clientCourant.Exonerere.CourtDesc;


                                            if (modeExoneration == "non")
                                            {
                                                if (clientCourant.Llangue.Id == 1)
                                                {
                                                    oldTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                    CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * oldHt_ligne;
                                                    oldTva = Math.Round(oldTva, 0);
                                                }
                                                else
                                                {
                                                    oldTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                   CultureInfo.CreateSpecificCulture("en-US")) / 100) * oldHt_ligne;
                                                    oldTva = Math.Round(oldTva, 0);
                                                }
                                                Lignecourante.LibelleDetail = "";
                                            }
                                            if (modeExoneration == "exo")
                                            {
                                                if (ClientSelected.Llangue.Id == 1)
                                                {
                                                    oldProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                    CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * oldHt_ligne;
                                                    oldProrata = Math.Round(oldProrata, 0);
                                                }
                                                else if (clientCourant.Llangue.Id == 2)
                                                {
                                                    oldProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                   CultureInfo.CreateSpecificCulture("en-US")) / 100) * oldHt_ligne;
                                                    oldProrata = Math.Round(oldProrata, 0);
                                                }
                                                Lignecourante.LibelleDetail = "";

                                            }

                                            if (modeExoneration == "part")
                                            {
                                                if (CurIsExonere)
                                                {
                                                    if (CurIsProrata)
                                                    {
                                                        if (ClientSelected.Llangue.Id == 1)
                                                        {
                                                            if (Isproratable)
                                                            {
                                                                oldProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                                CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * oldHt_ligne;
                                                                oldProrata = Math.Round(oldProrata, 0);
                                                            }
                                                        }
                                                        else if (clientCourant.Llangue.Id == 2)
                                                        {
                                                            if (Isproratable)
                                                            {

                                                                oldProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                               CultureInfo.CreateSpecificCulture("en-US")) / 100) * oldHt_ligne;
                                                                oldProrata = Math.Round(oldProrata, 0);
                                                            }
                                                        }
                                                    }
                                                }
                                                else
                                                {
                                                    if (clientCourant.Llangue.Id == 1)
                                                    {
                                                        if (Isproratable)
                                                        {
                                                            oldTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                            CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * oldHt_ligne;
                                                            oldTva = Math.Round(oldTva, 0);
                                                        }
                                                    }
                                                    else
                                                    {
                                                        oldTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                       CultureInfo.CreateSpecificCulture("en-US")) / 100) * oldHt_ligne;
                                                        oldTva = Math.Round(oldTva, 0);
                                                    }
                                                }
                                            }

                                            double oldTTC = oldHt_ligne + oldTva + oldProrata;
                                            FTotalProrata -= oldProrata;
                                            Ftotaltva -= (oldTva > 0 ? oldTva : 0);
                                            FtotalTTC -= (oldTTC > 0 ? oldTTC : 0);
                                            FTotalHt -= oldHt_ligne;


                                            #region MONTANT FACTRE


                                            double valTva = 0;
                                            double valprorata = 0;

                                            FTotalHt += (double)ligne.montantHt;

                                            if (modeExoneration == "non")
                                            {
                                                //client non exonere

                                                if (clientCourant.Llangue != null)
                                                {
                                                    if (clientCourant.Llangue.Id == 1)
                                                    {
                                                        valTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                        CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * FTotalHt;
                                                        valTva = Math.Round(valTva, 0);
                                                    }
                                                    else
                                                    {
                                                        valTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                       CultureInfo.CreateSpecificCulture("en-US")) / 100) * FTotalHt;
                                                        valTva = Math.Round(valTva, 0);
                                                    }
                                                }

                                                // double calcul = (double.Parse(ligne.tva.Replace('%', ' ').Trim()) / 100) * FTotalHt;
                                                Ftotaltva = valTva;
                                                MontantProrata = "";
                                                FTotalProrata += 0;
                                                Montanttva = TaxeSelected.Taux;

                                                FtotalTTC = (Ftotaltva + FTotalHt + FTotalProrata);
                                                Lignecourante.LibelleDetail = "";
                                            }
                                            else if (modeExoneration == "exo")
                                            {
                                                Montanttva = "(exonere)" + TaxeSelected.Taux;
                                                Ftotaltva += 0;

                                                MontantProrata = TaxePorataSelected.Taux;

                                                if (clientCourant.Llangue != null)
                                                {
                                                    if (ClientSelected.Llangue.Id == 1)
                                                    {
                                                        valprorata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                        CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * FTotalHt;
                                                        valprorata = Math.Round(valprorata, 0);
                                                    }
                                                    else if (clientCourant.Llangue.Id == 2)
                                                    {
                                                        valprorata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                       CultureInfo.CreateSpecificCulture("en-US")) / 100) * FTotalHt;
                                                        valprorata = Math.Round(valprorata, 0);
                                                    }
                                                }

                                                // double calcul = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Trim()) / 100) * FTotalHt;
                                                FTotalProrata = valprorata;

                                                FtotalTTC = (Ftotaltva + FTotalHt + FTotalProrata);
                                                Lignecourante.LibelleDetail = "";
                                            }
                                            else if (modeExoneration == "part")
                                            {
                                                Montanttva = TaxeSelected.Taux;



                                                if (IsItemsExonere)
                                                {
                                                    if (Isproratable)
                                                    {
                                                        totalihne_ht_prorata += (double)Lignecourante.montantHt;
                                                        MontantProrata = TaxePorataSelected.Taux;
                                                        totaligne_ht_tva += 0;
                                                        Montanttva = "";
                                                    }
                                                    else
                                                    {
                                                        totaligne_ht_tva += 0;
                                                        totalihne_ht_prorata += 0;
                                                        Montanttva = "";
                                                    }

                                                }
                                                else if (!IsItemsExonere)
                                                {
                                                    //taxe et pas de prorata
                                                    if (!Isproratable)
                                                    {
                                                        totaligne_ht_tva += (double)Lignecourante.montantHt;
                                                        totalihne_ht_prorata += 0;
                                                        Montanttva = TaxeSelected.Taux;
                                                    }



                                                }

                                                double calculProrata = 0;
                                                double calculTva = 0;

                                                if (ClientSelected.Llangue != null)
                                                {
                                                    if (ClientSelected.Llangue.Id == 1)
                                                    {
                                                        if (Isproratable)
                                                        {
                                                            calculProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                            CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * totalihne_ht_prorata;
                                                            FTotalProrata = Math.Round(calculProrata, 0);
                                                            Lignecourante.LibelleDetail = string.Format("Prorata :{0}", Math.Round(calculProrata, 0));
                                                        }
                                                        else
                                                        {
                                                            calculProrata = 0;
                                                            if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                                Lignecourante.LibelleDetail = "";
                                                        }
                                                        if (!IsItemsExonere)
                                                        {
                                                            calculTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace('.', ',').Trim(),
                                                                CultureInfo.CreateSpecificCulture("fr-FR")) / 100) * totaligne_ht_tva;
                                                            Ftotaltva = Math.Round(calculTva, 0);
                                                            Lignecourante.LibelleDetail = string.Format("Tva :{0}", Math.Round(calculTva, 0));
                                                        }
                                                        else
                                                        {
                                                            calculTva = 0;
                                                            if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                                Lignecourante.LibelleDetail = "";
                                                        }
                                                    }
                                                    else if (ClientSelected.Llangue.Id == 2)
                                                    {
                                                        if (Isproratable)
                                                        {
                                                            calculProrata = (double.Parse(TaxePorataSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                           CultureInfo.CreateSpecificCulture("en-US")) / 100) * totalihne_ht_prorata;
                                                            FTotalProrata = Math.Round(calculProrata, 0);
                                                            Lignecourante.LibelleDetail = string.Format("Prorata :{0}", Math.Round(calculProrata, 0));
                                                        }
                                                        else
                                                        {
                                                            calculProrata = 0;
                                                            if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                                Lignecourante.LibelleDetail = "";
                                                        }

                                                        if (!IsItemsExonere)
                                                        {
                                                            calculTva = (double.Parse(TaxeSelected.Taux.Replace('%', ' ').Replace(',', '.').Trim(),
                                                               CultureInfo.CreateSpecificCulture("en-US")) / 100) * totaligne_ht_tva;
                                                            Ftotaltva = Math.Round(calculTva, 0);
                                                        }
                                                        else
                                                        {
                                                            calculTva = 0;
                                                            if (string.IsNullOrEmpty(Lignecourante.LibelleDetail))
                                                                Lignecourante.LibelleDetail = "";
                                                        }
                                                    }
                                                }

                                                FtotalTTC = (Ftotaltva + FTotalHt + FTotalProrata);


                                            }

                                            #endregion


                                         
                                            CacheLigneCommandList.Add(ligne);

                                        }


                                        LigneCommandList = null;
                                        LigneCommandList = CacheLigneCommandList;
                                        //LigneFacture = null;
                                        LigneFacture = new LigneFactureModel(); ;
                                        PrixUnitaireselected = 0;
                                        IsItemsExonere = false;
                                        ExonereEnable = false;
                                        isupdateinvoice = false;
                                    }
                                    #endregion
                                }

                            }

                        }
                        else
                            MessageBox.Show("Quantité doit etre Supérieur à [0] !", "Probleme de Quantité", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }

        }


        #endregion

        #region ADD FACTURE

        // annuler facture
        private void canNew()
        {
            if (FatureCurrent != null && FatureCurrent.IdClient > 0)
            {
                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                messageBox.Owner = Application.Current.MainWindow;
                messageBox.Title = "MISE JOUR FACTURE";
                messageBox.ViewModel.Message = "Attention cette operation va annuler celle En cours ?";
                if (messageBox.ShowDialog().Value == true)
                {
                    FatureCurrent = new FactureModel();
                    LigneCommandList = new List<LigneCommand>();
                    CacheLigneCommandList = null;
                    ActualFacture = new FactureModel();
                    ProduitSelected = null;
                    LigneFacture = null;
                    PrixUnitaireselected = 0;
                    ObjetList = null;
                    ObjetSelected = null;
                    ExploitationList = null;
                    FTotalHt = 0;
                    FtotalTTC = 0;
                    Montanttva = "";
                    Ftotaltva = 0;
                    isReloadFacture = false;
                    FTotalProrata = 0;
                    MontantProrata = "";
                    ClientList = null;
                    if (societeCourante != null)
                        ClientList = clientservice.CLIENT_GETLISTE(societeCourante.IdSociete);
                    DeviseSelected = null;
                    DeviseConvert = null;
                    Depselected = null;
                    IsProrataEnabled = true;
                    isFactureExist = true;
                    IsEnabledClient = true;
                    isupdateinvoice = false;
                    AfficheStatutProduit = "";
                    IsdetailExiste = false;
                    DetailProdEnable = false;
                }
            }

        }
        bool canExecutenew()
        {
            return true;
        }


        bool canExecuteSavefacture()
        {
            bool isvalue = false;
            if (ActualFacture != null)
            {
                if (ActualFacture.IdFacture > 0)
                {
                    if (isOperationpossible)
                        isvalue = true;
                    else isvalue = false;
                    //if (ActualFacture.IdStatut == 2)
                    //{
                    //    if (ActualFacture.ClienOk)
                    //        isvalue = true;
                    //    else isvalue = false;
                    //}
                }
                else
                    isvalue = true;
            }
            else
            {
                isvalue = false;
            }
            return isvalue;
            //return ActualFacture != null ? (ActualFacture.ClienOk==true ) : false;
        }

        private void canSaveFacture(object param)
        {
            List<LigneFactureModel> listItems = null;
            object[] tabretour = null;
            string dernierefactureMax = null;
            string nouvelleFactureGeneree = null;

            if ((droitFormulaire.Super || droitFormulaire.Validation))
            {
                try
                {
                    IsBusy = true;


                    if (FatureCurrent.IdFacture == 0)
                    {
                        if (operation == "creation")
                        {
                            if (FatureCurrent.IdClient > 0)
                            {
                                if (LigneCommandList != null && LigneCommandList.Count > 0)
                                {
                                    // création de la facture



                                    if (ExploitationSelected != null)
                                        FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;
                                    if (UserConnected != null)
                                        FatureCurrent.IdCreerpar = UserConnected.Id;
                                    if (ParametersDatabase != null)
                                        FatureCurrent.IdDevise = ParametersDatabase.IdDevise;
                                    if (ParametersDatabase != null)
                                        FatureCurrent.IdTaxe = ParametersDatabase.Idtva;
                                    if (societeCourante != null)
                                        FatureCurrent.IdSite = societeCourante.IdSociete;
                                    FatureCurrent.IsProrata = IsPorata;
                                    if (Depselected == null)
                                        FatureCurrent.IdDepartement = depService.Departemnt_SELECTById(1).IdDep;



                                    if (FatureCurrent.MoisPrestation != null)
                                    {
                                        if (LigneCommandList != null)
                                        {


                                            listItems = TraitementLigneCommande();

                                            #region TOTALFACTURE

                                            if (ClientSelected.Exonerere != null)
                                            {

                                                if (ClientSelected.Exonerere.CourtDesc == "exo")
                                                {
                                                    if (ClientSelected.Porata != null)
                                                        tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, ClientSelected.Porata.Taux,
                                                            null, true, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                                }
                                                if (ClientSelected.Exonerere.CourtDesc == "non")
                                                {
                                                    if (TaxeSelected != null)
                                                        tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, null,
                                                            TaxeSelected.Taux, false, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                                }
                                                if (ClientSelected.Exonerere.CourtDesc == "part")
                                                {
                                                    tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(listItems,
                                                        ClientSelected.Porata.Taux, TaxeSelected.Taux, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                                }

                                                if (tabretour != null)
                                                    FatureCurrent.TotalTTC = (double)tabretour[5];
                                                else FatureCurrent.TotalTTC = 0;
                                            }
                                            #endregion

                                            FatureCurrent.IdStatut = CurrentStatut.IdStatut;
                                            idNewfacture = factureservice.FACTURE_ADD_LIGNEGFACTURE(FatureCurrent, listItems);

                                            for (int i = 0; i < 50; i += 5)
                                                Thread.Sleep(100);

                                            if (idNewfacture != null)
                                            {
                                                //rechergement facture 
                                                FatureCurrent = factureservice.GET_FACTURE_BYID(long.Parse(idNewfacture.ToString()));
                                                isAddInvoice = true;
                                                isReloadFacture = true ;
                                            }
                                            PrixUnitaireselected = 0;

                                            issaveUpdatedata = false;
                                            loadFactureInformation();
                                        }
                                        else
                                            MessageBox.Show("Cette  facture ne peut pas etre créer, Pas de ligne de factures Crées !", "PROBLEME CREATION FACTURE", MessageBoxButton.OK, MessageBoxImage.Error);

                                    }
                                    else
                                        MessageBox.Show("Préciser la date du mois de prestation", "PROBLEME CREATION FACTURE", MessageBoxButton.OK, MessageBoxImage.Warning);
                                      

                                }else
                                    MessageBox.Show("lignes de factures requisent  !", "PROBLEME CREATION FACTURE", MessageBoxButton.OK, MessageBoxImage.Error);
                            
                            }
                        }


                    }
                    else
                    {
                        if (operation == "validation")
                        {
                            
                            // validation de lutilisateur,check box cocher dun facture non cloturer

                            if (valeurDalidation == "encours")
                            {
                                if (IsValidateDate)
                                {

                                    FatureCurrent.IdStatut = CurrentStatut.IdStatut;
                                    FatureCurrent.IdClient = FatureCurrent.CurrentClient.IdClient;
                                    if (LigneCommandList != null)
                                        listItems = TraitementLigneCommande();
                                    else return;
                                    FatureCurrent.IdModifierPar = UserConnected.Id;
                                    FatureCurrent.isfactureValide = true;
                                    if (ExploitationSelected != null)
                                        FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;

                                    #region TOTALFACTURE

                                    if (ClientSelected.Exonerere != null)
                                    {

                                        if (ClientSelected.Exonerere.CourtDesc == "exo")
                                        {
                                            if (ClientSelected.Porata != null)
                                                tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, ClientSelected.Porata.Taux,
                                                    null, true, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                        }
                                        if (ClientSelected.Exonerere.CourtDesc == "non")
                                        {
                                            if (TaxeSelected != null)
                                                tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, null,
                                                    TaxeSelected.Taux, false, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                        }
                                        if (ClientSelected.Exonerere.CourtDesc == "part")
                                        {
                                            tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(listItems,
                                                ClientSelected.Porata.Taux, TaxeSelected.Taux, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                        }

                                        if (tabretour != null)
                                            FatureCurrent.TotalTTC = (double)tabretour[5];
                                        else FatureCurrent.TotalTTC = 0;
                                    }
                                    #endregion


                                    if (factureservice.FACTURE_UPDATE(ref  dernierefactureMax, ref nouvelleFactureGeneree, FatureCurrent))
                                    {

                                        foreach (var lt in listItems)
                                            ligneFactureService.LIGNE_FACTURE_ADD(lt);

                                        for (int i = 0; i < 50; i += 5)
                                            Thread.Sleep(100);

                                        FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                        isReloadFacture = true;

                                        ActualFacture = null;
                                        IsBusy = false;
                                        issaveUpdatedata = false;
                                        loadFactureInformation();
                                    }
                                }
                                else
                                {


                                    if (FatureCurrent.IdStatut == 1)
                                    {
                                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                        messageBox.Owner = Application.Current.MainWindow;
                                        messageBox.Title = "MISE JOUR FACTURE";
                                        messageBox.ViewModel.Message = "Voulez vous confirmer cette Validation en cours?";
                                        if (messageBox.ShowDialog().Value == true)
                                        {
                                            //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                                            FatureCurrent.IdStatut = CurrentStatut.IdStatut;
                                            if (LigneCommandList != null)
                                                listItems = TraitementLigneCommande();
                                            else return;
                                            FatureCurrent.IdModifierPar = UserConnected.Id;
                                            if (ExploitationSelected != null)
                                                FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;
                                            FatureCurrent.IdClient = FatureCurrent.CurrentClient.IdClient;
                                            FatureCurrent.isfactureValide = true;

                                            #region TOTALFACTURE

                                            if (ClientSelected.Exonerere != null)
                                            {

                                                if (ClientSelected.Exonerere.CourtDesc == "exo")
                                                {
                                                    if (ClientSelected.Porata != null)
                                                        tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, ClientSelected.Porata.Taux,
                                                            null, true, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                                }
                                                if (ClientSelected.Exonerere.CourtDesc == "non")
                                                {
                                                    if (TaxeSelected != null)
                                                        tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, null,
                                                            TaxeSelected.Taux, false, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                                }
                                                if (ClientSelected.Exonerere.CourtDesc == "part")
                                                {
                                                    tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(listItems,
                                                        ClientSelected.Porata.Taux, TaxeSelected.Taux, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                                }

                                                if (tabretour != null)
                                                    FatureCurrent.TotalTTC = (double)tabretour[5];
                                                else FatureCurrent.TotalTTC = 0;
                                            }
                                            #endregion

                                            if (factureservice.FACTURE_UPDATE(ref  dernierefactureMax, ref nouvelleFactureGeneree, FatureCurrent))
                                            {

                                                foreach (var lt in listItems)
                                                    ligneFactureService.LIGNE_FACTURE_ADD(lt);



                                                for (int i = 0; i < 50; i += 5)
                                                    Thread.Sleep(100);

                                                FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                isReloadFacture = true;
                                                ActualFacture = null;
                                                IsBusy = false;
                                                issaveUpdatedata = false;
                                                loadFactureInformation();
                                            }
                                        }
                                        else
                                        {
                                            // simple validation sans changement statut
                                            if (LigneCommandList != null)
                                                listItems = TraitementLigneCommande();
                                            else return;

                                            FatureCurrent.isfactureValide = false;
                                            if (factureservice.FACTURE_UPDATE(ref  dernierefactureMax, ref nouvelleFactureGeneree, FatureCurrent))
                                            {

                                                foreach (var lt in listItems)
                                                    ligneFactureService.LIGNE_FACTURE_ADD(lt);

                                                for (int i = 0; i < 50; i += 5)
                                                    Thread.Sleep(100);

                                                FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                ActualFacture = null;
                                                isReloadFacture = true;
                                                IsBusy = false;
                                                issaveUpdatedata = false;
                                                loadFactureInformation();
                                            }
                                        }

                                    }
                                }
                            }

                            else if (valeurDalidation == "validation")
                            {

                                if (IsValidateDate)
                                {
                                    //cloture de la facture
                                    FatureCurrent.IdStatut = CurrentStatut.IdStatut;

                                    if (FatureCurrent.IdSite == societeCourante.IdSociete)
                                    {
                                        //modifie que les données du site
                                        FatureCurrent.IdClient = FatureCurrent.CurrentClient.IdClient;
                                        if (LigneCommandList != null)
                                            listItems = TraitementLigneCommande();
                                        else return;

                                        #region TOTALFACTURE

                                        if (ClientSelected.Exonerere != null)
                                        {

                                            if (ClientSelected.Exonerere.CourtDesc == "exo")
                                            {
                                                if (ClientSelected.Porata != null)
                                                    tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, ClientSelected.Porata.Taux,
                                                        null, true, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                            }
                                            if (ClientSelected.Exonerere.CourtDesc == "non")
                                            {
                                                if (TaxeSelected != null)
                                                    tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, null,
                                                        TaxeSelected.Taux, false, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                            }
                                            if (ClientSelected.Exonerere.CourtDesc == "part")
                                            {
                                                tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(listItems,
                                                    ClientSelected.Porata.Taux, TaxeSelected.Taux, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                            }

                                            if (tabretour != null)
                                                FatureCurrent.TotalTTC = (double)tabretour[5];
                                            else FatureCurrent.TotalTTC = 0;
                                        }
                                        #endregion

                                        if (ExploitationSelected != null)
                                            FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;
                                        FatureCurrent.IdModifierPar = UserConnected.Id;
                                        FatureCurrent.DateCloture = FactureCache.DateCloture;
                                        FatureCurrent.isfactureValide = false;
                                        if (factureservice.FACTURE_UPDATE(ref  dernierefactureMax, ref nouvelleFactureGeneree, FatureCurrent))
                                        {



                                            foreach (var lt in listItems)
                                                ligneFactureService.LIGNE_FACTURE_ADD(lt);
                                        }
                                    }


                                    //modification statut facture
                                    factureservice.FACTURE_VALIDATION(FatureCurrent.IdFacture, DateTime.Now, FatureCurrent.IdStatut, UserConnected.Id, true);

                                    ActualFacture = null;

                                    FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                    isReloadFacture = true;
                                    IsBusy = false;
                                    issaveUpdatedata = false;
                                    loadFactureInformation();
                                }
                                else
                                {
                                    //simple validation
                                    if (FatureCurrent.IdSite == societeCourante.IdSociete)
                                    {
                                        if (LigneCommandList != null)
                                            listItems = TraitementLigneCommande();
                                        else return;
                                        if (ExploitationSelected != null)
                                            FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;
                                        FatureCurrent.IdModifierPar = UserConnected.Id;
                                        FatureCurrent.DateCloture = FactureCache.DateCloture;
                                        FatureCurrent.isfactureValide = false;

                                        #region TOTALFACTURE

                                        if (ClientSelected.Exonerere != null)
                                        {

                                            if (ClientSelected.Exonerere.CourtDesc == "exo")
                                            {
                                                if (ClientSelected.Porata != null)
                                                    tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, ClientSelected.Porata.Taux,
                                                        null, true, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                            }
                                            if (ClientSelected.Exonerere.CourtDesc == "non")
                                            {
                                                if (TaxeSelected != null)
                                                    tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, null,
                                                        TaxeSelected.Taux, false, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                            }
                                            if (ClientSelected.Exonerere.CourtDesc == "part")
                                            {
                                                tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(listItems,
                                                    ClientSelected.Porata.Taux, TaxeSelected.Taux, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"));
                                            }

                                            if (tabretour != null)
                                                FatureCurrent.TotalTTC = (double)tabretour[5];
                                            else FatureCurrent.TotalTTC = 0;
                                        }
                                        #endregion

                                        if (factureservice.FACTURE_UPDATE(ref  dernierefactureMax, ref nouvelleFactureGeneree, FatureCurrent))
                                        {


                                            foreach (var lt in listItems)
                                                ligneFactureService.LIGNE_FACTURE_ADD(lt);

                                            ActualFacture = null;

                                            FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                            isReloadFacture = true;
                                            IsBusy = false;
                                            issaveUpdatedata = false;
                                            loadFactureInformation();
                                        }
                                    }

                                }

                            }

                            else if (valeurDalidation == "devalide")
                            {
                                if (IsEnabledchkDateValidate)
                                {
                                    if (int.Parse(CurrentStatut.CourtDesc) == 3)
                                    {
                                        // devalider facture valider
                                        if (droitFormulaire.Proprietaire)
                                        {
                                            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                            messageBox.Owner = Application.Current.MainWindow;
                                            messageBox.Title = "INFORMATION MODIFICATION STATUT FACTURE";
                                            messageBox.ViewModel.Message = "Cette Action va Annuler le Statut sortie actuelle de cette facture  ?";
                                            if (messageBox.ShowDialog().Value == true)
                                            {
                                                StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");

                                                factureservice.FACTURE_VALIDATION(FatureCurrent.IdFacture, DateTime.Now, newStatut.IdStatut, UserConnected.Id, false);

                                                for (int i = 0; i < 50; i += 5)
                                                    Thread.Sleep(100);

                                                FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                isReloadFacture = true;
                                                ActualFacture = null;
                                                IsBusy = false;
                                                issaveUpdatedata = false;
                                                loadFactureInformation();
                                            }
                                        }
                                        else MessageBox.Show("Vous navez pas assez de droits pour effectuer cette Operation", "PROBLEME DE DROITS", MessageBoxButton.OK, MessageBoxImage.Warning);

                                    }
                                    else
                                        if (int.Parse(CurrentStatut.CourtDesc) == 4)
                                        {
                                            // devalider facture sortie, uniquement le owner
                                            if (droitFormulaire.Proprietaire)
                                            {
                                                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                                messageBox.Owner = Application.Current.MainWindow;
                                                messageBox.Title = "INFORMATION MODIFICATION STATUT FACTURE";
                                                messageBox.ViewModel.Message = "Cette Action va Annuler le Statut sortie actuelle de cette facture  ?";
                                                if (messageBox.ShowDialog().Value == true)
                                                {
                                                    StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");
                                                    factureservice.FACTURE_SORTIE(FatureCurrent.IdFacture, DateTime.Now, newStatut.IdStatut, UserConnected.Id, false);

                                                    for (int i = 0; i < 50; i += 5)
                                                        Thread.Sleep(100);

                                                    FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                    isReloadFacture = true;
                                                    ActualFacture = null;
                                                    IsBusy = false;
                                                    issaveUpdatedata = false;
                                                    loadFactureInformation();
                                                }

                                            }
                                            else MessageBox.Show("Vous navez pas assez de droits pour effectuer cette Operation", "PROBLEME DE DROITS", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        }
                                        else if (int.Parse(CurrentStatut.CourtDesc) == 5)
                                        {
                                            //devalider facture suspendu
                                            StatutModel newStatut = null;
                                            if (isdatevalidationsexist)
                                            {
                                                //cloturer puis suspendu
                                                newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");
                                                factureservice.FACTURE_SUSPENSION(FatureCurrent.IdFacture, newStatut.IdStatut, UserConnected.Id, false);

                                            }
                                            else
                                            {
                                                // pas cloturer avant suspension
                                                newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                                factureservice.FACTURE_SUSPENSION(FatureCurrent.IdFacture, newStatut.IdStatut, UserConnected.Id, false);
                                            }


                                            for (int i = 0; i < 50; i += 5)
                                                Thread.Sleep(100);

                                            FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                            ActualFacture = null;
                                            IsBusy = false;
                                            issaveUpdatedata = false;
                                            loadFactureInformation();

                                            //factureservice.FACTURE_SUSPENSION(FatureCurrent.IdFacture, newStatut.IdStatut, false);
                                        }
                                        else if (int.Parse(CurrentStatut.CourtDesc) == 6)
                                        {
                                            StatutModel newStatut = null;
                                            //devalider facture non valable
                                            if (droitFormulaire.Proprietaire)
                                            {
                                                if (isdatevalidationsexist)
                                                {
                                                    newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");
                                                    factureservice.FACTURE_NONVALABLE(FatureCurrent.IdFacture, newStatut.IdStatut, UserConnected.Id, false);
                                                }
                                                else
                                                {
                                                    newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                                    factureservice.FACTURE_NONVALABLE(FatureCurrent.IdFacture, newStatut.IdStatut, UserConnected.Id, false);
                                                }

                                               
                                                for (int i = 0; i < 50; i += 5)
                                                    Thread.Sleep(100);

                                                FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                isReloadFacture = true;
                                                ActualFacture = null;
                                                IsBusy = false;
                                                issaveUpdatedata = false;
                                                loadFactureInformation();
                                            }
                                            else MessageBox.Show("Vous navez pas assez de droits pour effectuer cette Operation", "PROBLEME DE DROITS", MessageBoxButton.OK, MessageBoxImage.Warning);
                                        }


                                }


                            }

                        }

                    }

                    IsBusy = false;


                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "MESSAGE ERREUR MISE JOUR FACTURE";
                    view.ViewModel.Message ="Erreure <<10006>> Impossible de mettre à jour Cette facture"+ex .Message ;
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }
            }

        }


        #endregion


       

        #region SUPPRESSION FACTURE



        private void canDelete()
        {
                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                messageBox.Owner = Application.Current.MainWindow;
                messageBox.Title = "INFORMATION  SUPPRESSION";
                messageBox.ViewModel.Message = "Voulez vous supprimer cette Facture?";
                if (messageBox.ShowDialog().Value == true)
                {
                    try
                    {
                        this.IsBusy = true;
                        if (CacheLigneCommandList != null)
                            foreach (var ligne in CacheLigneCommandList)
                                _ligneFacture.LIGNE_FACTURE_DELETE(ligne.ID);

                        factureservice.FACTURE_DELETE(FatureCurrent.IdFacture, 0, societeCourante.IdSociete, UserConnected.Id );
                        ActualFacture = null;
                        FatureCurrent = new FactureModel();
                        FtotalTTC = 0;
                        Ftotaltva = 0;
                        FTotalHt = 0;
                        ObjetList = null;
                        ObjetSelected = null;
                        ExploitationList = null;
                        Montanttva = "";
                        DeviseSelected = null;
                        DeviseConvert = null;
                        issaveUpdatedata = false;
                        LigneCommandList = null;
                        this.IsBusy = false;
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "Message SUPPRESSION FACTURE";
                        view.ViewModel.Message = "Erreure <<10005>> Problème survenu lors de la tentative de suppression de la facture" +ex .Message ;
                        view.ShowDialog();
                        IsBusy = false;
                        this.MouseCursor = null;
                    }

                }
           
        }
        bool canExecuteDeletefacture()
        {
            bool values = false;
            if (FatureCurrent != null)
            {
                if (FatureCurrent.IdFacture > 0)
                {
                    if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) < 2)
                    {
                        if (droitFormulaire.Super || droitFormulaire.Suppression)
                            values = true;
                        else
                            values = false;

                    }
                    else
                    {
                        if (droitFormulaire.Proprietaire )
                            values = true;
                        else
                            values = false;
                    }
                  
                }
            }
            else
                values = false;
            return values;
            
            //return FatureCurrent != null ?
            //    (FatureCurrent.IdFacture > 0 ? (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) < 2 ? ((droitFormulaire.Super || droitFormulaire.Suppression) ? true : droitFormulaire.Super ?true :false ) : false) : false) : false;
        }

        #endregion

        #region IMPRESSION FACTURE



        private void canPrint()
        {
            //if (droitFormulaire.Impression)
            //{
            IsBusy = true;
            try {
            string mode = string.Empty;
            DataTable tclient = ReportModel.GetReportClient(FatureCurrent.IdClient);
            DataTable tableSociete = ReportModel.GetReportSociete();
            DataTable tablePiedPage = ReportModel.GetReporPiedPage();
            DataTable tableLibelle = ReportModel.GetLibelle(FatureCurrent.CurrentClient.IdLangue);
            DataTable tablefacture = ReportModel.GetFacture(FatureCurrent.IdFacture);

            if (FatureCurrent.CurrentClient.Exonerere == null)
            {
                ExonerationModel exo = new ExonerationModel();
                mode = exo.EXONERATION_SELECTById(FatureCurrent.CurrentClient.IdExonere).CourtDesc;
            }
            else mode = FatureCurrent.CurrentClient.Exonerere.CourtDesc;


            if (mode == "part")
            {
                DataTable tableLignefactures = ReportModel.GetLigneFacture_nonExo(FatureCurrent.IdFacture);
                formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefactures, tableLibelle, 3);
                vf.ShowDialog();
            }
            else
            {
                int newMode;
                if (mode == "non")
                    newMode = 1;
                else newMode = 2;
                DataTable tableLignefacture = ReportModel.GetLigneFacture(FatureCurrent.IdFacture);
                formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefacture, tableLibelle, newMode);
                vf.ShowDialog();
            }

            IsBusy = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE IMPRESSION FACTURE";
                view.ViewModel.Message = "Erreur <<10003>> Problème survenu lors de l'ipression ";
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }

        }

        bool canExecutePrint()
        {
            return FatureCurrent != null ? (FatureCurrent.IdFacture > 0 ? true : false) : false;
        }

        #endregion

        // delete line items

        #region SUPPRESSION LIGNE ITEMS


        private void canDeleteLine()
        {

            if (LigneCommandList != null)
            {
                if (OldLigneFacture.IdLigneFacture == 0)
                {
                    if (OldLigneFacture != null)
                    {

                        LigneCommand cmd = LigneCommandList.Find(od => od.IdProduit == OldLigneFacture.IdProduit);
                        if (cmd != null)
                        {
                            LigneCommandList.Remove(cmd);
                            List<LigneCommand> newliste = LigneCommandList;
                            LigneCommandList = null;
                            LigneCommandList = newliste;
                            CacheLigneCommandList = newliste;
                            isupdateinvoice = true;
                        }
                    }
                }
                else
                {
                    if (droitFormulaire.Super || droitFormulaire.Suppression)
                    {

                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = " INFORMATION SUPPRESSION LIGNE FACTURE";
                        messageBox.ViewModel.Message = "Voulez vous Supprimer Cette Ligne de Facture";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            try
                            {
                                ligneFactureService.LIGNE_FACTURE_DELETE(OldLigneFacture.IdLigneFacture);
                                List<LigneFactureModel> newliste = ligneFactureService.LIGNE_FACTURE_BYIDFActure(FatureCurrent.IdFacture);
                                LigneCommand cm = LigneCommandList.Find(de => de.ID == OldLigneFacture.IdLigneFacture);
                                //if (cm != null)
                                //ajouter un eventuel nouvell elemen non saisie
                                //    LigneCommandList.Remove(cm);
                                //if (LigneCommandList.Count > 0)
                                //{
                                //    foreach (var de in LigneCommandList)
                                //    {

                                //    }
                                //}

                                ConvertDataItems(newliste);
                            }
                            catch (Exception ex)
                            {
                                CustomExceptionView view = new CustomExceptionView();
                                view.Owner = Application.Current.MainWindow;
                                view.Title = "ERREURE DE SUPPRESSION";
                                view.ViewModel.Message = "Erreure <<10004>> probléme survenu lors de la tentative de suppression de ce item";
                                view.ShowDialog();

                            }
                        }

                        else
                            MessageBox.Show("Pas assez de privileges pour effectuer cette opération","PROBLEME DE DROITS",MessageBoxButton.OK , MessageBoxImage.Error );

                    }
                }

            }

        }

        bool canExecuteDeleteline()
        {
            string mode = string.Empty;
            if (FatureCurrent.CurrentStatut == null)
                mode = CurrentStatut.CourtDesc;
            else mode = FatureCurrent.CurrentStatut.CourtDesc;

            return LigneFacture != null ? (int.Parse(mode) <= 2 ? (FatureCurrent.IdSite == societeCourante.IdSociete ? true : false) : false) : false;
        }

        private void canClose()
        {
            if ((LigneCommandList != null && LigneCommandList.Count > 0) || (FatureCurrent != null && FatureCurrent.IdClient > 0))
            {
                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                messageBox.Owner = Application.Current.MainWindow;
                messageBox.Title = " INFORMATION SUPPRESSIONLIGNE FACTURE";
                messageBox.ViewModel.Message = "Attention la fermeture Va annuler l'opération en cours , voulez vous continuer ?";
                if (messageBox.ShowDialog().Value == true)
                {
                    _injectSingleViewService.ClearViewsFromRegion(RegionNames.ContentRegion);
                    UserAffiche uaffiche = _container.Resolve<UserAffiche>();


                    IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion,
                                                         () => uaffiche);
                }

            }
            else
            {
                _injectSingleViewService.ClearViewsFromRegion(RegionNames.ContentRegion);
                UserAffiche uaffiche = _container.Resolve<UserAffiche>();


                IRegionManager rightRegion = _regionManager.RegisterViewWithRegion(RegionNames.ContentRegion,
                                                     () => uaffiche);
            }
        }

      
        #endregion

        private void canObjectselected(object param)
        {
            object val = param;
        }
        private void canAddData()
        {

        }

        bool canExecuteAddDatas()
        {
            return true;
        }
        #endregion

        #endregion

        #region Helper


        List<LigneFactureModel> TraitementLigneCommande()
        {
            List<LigneFactureModel> listItems = new List<LigneFactureModel>();
            foreach (var ligne in LigneCommandList)
            {
                LigneFactureModel newligne = new LigneFactureModel
                {
                    IdLigneFacture = ligne.ID,
                    IdFacture = FatureCurrent.IdFacture,
                    IdProduit = ligne.IdProduit,
                    IdUtilisateur = FatureCurrent.IdCreerpar,
                    IdDetailProduit = ligne.Idetail,
                    Description = ligne.Description,
                    Quantite = ligne.quantite,
                    PrixUnitaire = ligne.PrixUnit,
                    Exonere = ligne.estExonere,
                    MontanHT = ligne.montantHt,
                    IdSite = ligne.IdSite 
                };
                listItems.Add(newligne);
            }
            return listItems;
        }


        #endregion
    }



    //public class LigneCommand
    //{
    //    public long ID { get; set; }
    //    public int IdProduit { get; set; }
    //    public int Idetail { get; set; }
    //    public string Produit { get; set; }
    //    public string Description { get; set; }
    //    public decimal quantite { get; set; }
    //    public decimal PrixUnit { get; set; }
    //    public bool estExonere { get; set; }
    //    public bool estprorata { get; set; }
    //    public string remise { get; set; }
    //    public decimal montantRem { get; set; }
    //    public decimal montantHt { get; set; }
    //    public string tva { get; set; }
    //    public decimal Montanttc { get; set; }
    //    public int situation { get; set; }
    //    public bool IsdeletedEnabled { get; set; }
    //    public string LibelleDetail { get; set; }
    //    public Int32 IdSite { get; set; }
    //    public bool  SpecialMode { get; set; }
    //}
}
