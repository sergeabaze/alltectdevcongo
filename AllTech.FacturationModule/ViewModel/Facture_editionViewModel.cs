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
using Multilingue.Resources;
using AllTech.FrameWork.Utils;

namespace AllTech.FacturationModule.ViewModel
{
    public class Facture_editionViewModel : ViewModelBase
    {


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
        private RelayCommand destroyCommand;
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
        public List<FactureModel> TotalFacturesCreer = null;
        LigneFactureModel _ligneFacture;
        public LigneFactureModel ligneFactureService;
        List<LigneFactureModel> _LigneFatureListe;

        CultureInfo currentCulture = null;

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
        List<TaxeModel> _costTaxeList;
        TaxeModel _costTaxeSelected;

     

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
        List <DetailProductModel> selectDetailListeProduit;

        DetailProductModel detailService;
        ObservableCollection<DetailProductModel> detailListProdclient;
        ObservableCollection<DetailProductModel> detailListProdclientSelected;

        List<DetailProductModel> newDetailListeProduit;
        DetailProductModel detailProduitSelected;
        List<ProduitModel> newlisteProduit;
        List<DetailProductModel> cachnewDetailListeProduit;

        LangueModel langueservice;


        double fTotalHt;
        double ftotalTTC;
        double ftotaltva;
        string montanttva;
        string tauxMargeBenef;
        double montantMargeBenef;

      

        double fTotalProrata;
        string montantProrata;
        bool isValidateDate;
        bool isEnabledchkDateValidate;
        bool detailProdEnable;
        bool clientEnable;
        bool culumndetailVisible;

        bool factureOnLoad = true;

        bool isDaysCalcul;
        decimal newPrixUnit;
        double newNewQte;
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
        bool isVivisibleDestroyCommand;
        bool isdblClickoperation;
        bool checkValiderReadQty;
        bool visibilityValiderReadQty;

        string valeurDalidation = string.Empty;

        List<LigneFactureModel> newLine = null;
        UtilisateurModel userConnected;

        public static FactureModel factureListeSelected;

        LigneCommand lignecourante;
        List<LigneCommand> ligneCommandList;
        public List<LigneCommand> CacheLigneCommandList;
        float qtyselect;
        string newQtySelect;

       
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
        bool isenableTaxeList;
        bool isenableCostaxe;
        bool isJourLimiteValable = false;
     
     
        ProduitModel oldProduitSelected;
        LigneFactureModel oldLigneFacture;
        FactureModel actualFacture;

        ClientModel clientCourant = null;

        string afficheResume;
        string libelleDetailItems;
        bool enableAfficheResume;
        string afficheStatut;
        string afficheStatutProduit;

        int? clientIndex;
        int? objetIndex;
        int? exploitationIndex;
        int? departementindex;
        int? produitindex;
        double curOldQty;
        double curoldPu;
        bool curIsExonere;
        bool curIsProrata;
        bool isnewFacture;

        bool isnewClient = false;
        int idSocieteCourant = 0;

        bool isdatevalidationsexist;
        bool isReloadFacture;

        HashSet<Int32> IdProduits;
        bool isEnableCmbProduit;
        private bool isnewFactureEdite;
        public bool isDeleteFacture =false ;
        public bool IsOperation;
        bool isModeFacturation;
        DateTime? dateMoisPrestation;
        bool enabledate;
        bool isReload;

        bool  backgroundColorClient;
        bool isOperationExploitation = false;
        DroitModel _currentDroit;

        DataTable versionFactureCurrent;

        DateTime? dateAuditCreation;
        DateTime? dateAuditValidation;
        DateTime? dateAuditModification;
        DateTime? dateAuditSortie;
        int indexeTaxe;
       

     

        bool isVisibleButtonCancel;
        bool modeFacturenormale;
        bool modeFactureAvoir;
        bool modeFacturenormaleEnable;
        bool modeFactureAvoirEnable;
      
       ObservableCollection<StatutModel> StatutFacture=null ;
       Dictionary<int, string> exploitationFields=new Dictionary<int,string> ();

       string listexp;
       string listExpid;
       int indexeCostTaxe;
       bool isEnableMarge;

     

       OverviewFactureModel orverviewSelecte;
       bool isOperationClosing;
      public  bool isFactureOperation;
       bool isligneitemsoperation;
       bool onloading = false;
       bool isloading = false;

       int indexProduit;
        #endregion


        public Facture_editionViewModel(FactureModel _factureSelected)
        {
            clientIndex = null;
            objetIndex = null;
            exploitationIndex = null;
            departementindex = null;
            // _container = container;
            ProgressBarVisibility = false;
            IsEnabledClient = false;
            CmbEnabled = false;
            ExonereEnable = false;
            CulumndetailVisible = false;
            EnableAfficheResume = false;
            isReloadFacture = false;
            orverviewSelecte = new OverviewFactureModel();
            currentCulture = CultureInfo.CurrentCulture;
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
            langueservice = new LangueModel();
            TotalFacturesCreer = new List<FactureModel>();
            societeCourante = GlobalDatas.DefaultCompany;
            isnewFactureEdite = false ;
            IsOperation = false;
             isFactureOperation=false;
             isligneitemsoperation=false;
             IndexProduit = -1;
             IsenableTaxeList = true;

            UserConnected = GlobalDatas.currentUser;

           
                    // database parameters from init file
                    loadDataBaseData();

                    droitFormulaire = UserConnected.Profile.Droit.Find(p => p.IdVues == 4);
                    if (droitFormulaire != null)
                    {
                        if (droitFormulaire.Lecture || droitFormulaire.Developpeur)
                        {

                            FatureCurrent = _factureSelected;
                            if (FatureCurrent != null && FatureCurrent.IdFacture > 0)
                                // Utils.logUserActions(string.Format("<-- UI edition facture --ouverture  facture :{0}  par : {1}", FatureCurrent.NumeroFacture, UserConnected.Nom +"  "+( UserConnected.Prenom !="" ?UserConnected.Prenom:"" )), "");

                                if (UserConnected.Profile.ShortName == "sadmin")
                                {
                                    IsVivisibleDestroyCommand = true;
                                }


                            //type facture
                            ModeFacturenormale = true;
                            ModeFacturenormaleEnable = true;
                            ModeFactureAvoirEnable = true;

                            isJourLimiteValable = true;
                            IsenableCostaxe = true;
                            IsenableTaxeList = true;
                            onloading = true;


                            if (GlobalDatas.IsArchiveSelected)
                            {
                                IsEnableCmbProduit = false;
                            }
                            if (droitFormulaire.Developpeur || droitFormulaire.Marge)
                            {
                                IsEnableMarge = true;
                            }
                        }
                    }
                    else
                    {
                        throw new Exception("Vous n'avez pas les droit sur la Vue Création facture");
                    }
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
                if (CacheDatas.ui_Statut == null)
                {
                    StatutFacture = statutservice.STATUT_FACTURE_GETLISTE();
                    CacheDatas.ui_Statut = StatutFacture;
                }
                else StatutFacture = CacheDatas.ui_Statut;

               
                if (value != null && value.IdFacture>0 )
                {
                    DeviseSelected = value.CurrentDevise;
                    //facture a editer
                    ActualFacture = null;
                    FactureCache = value;
                    isupdateinvoice = false;
                    isFactureExist = true;
                    isReload = true;
                    if (value.CurrentStatut != null)
                    {
                        if (int.Parse ( value.CurrentStatut.CourtDesc)==7)
                            ModeFactureAvoir = true;
                        else ModeFacturenormale = true;

                    }
                    //if (FatureCurrent .IdStatut >=3)
                    IsEnabledClient = false;
                    IsPorata = FatureCurrent.IsProrata;
                   
                    idSocieteCourant = FatureCurrent.IdSite;

                    DateMoisPrestation = value.MoisPrestation;
                    isReload = false;
                    CmbEnabled = true;
                    Listexp = value.ExploitationList;
                    ListExpid = value.ExploitationIds;
                    ExploitationFields = new Dictionary<int, string>();
                    if (!string.IsNullOrEmpty(Listexp))
                    {
                        string[] tabListe = Listexp.Split(new char[] { ';' });

                        if (!string.IsNullOrEmpty(ListExpid))
                        {
                            string[] tabId = ListExpid.Split(new char[] { ';' });

                          
                            int ig = 0;
                            foreach (string tv in tabListe)
                            {
                                ExploitationFields.Add(int.Parse(tabId[ig]), tv);
                                ig++;
                            }
                        }
                        else ExploitationFields.Add(value.IdExploitation, Listexp);
                    }
                   
                   // loadDatasCurrentfact();
                    isloading = true;
                    factureOnLoad = true;
                    newloadinfdatarefs();

                    isOperationpossible = false;
                  
                  
                }
                else
                {
                    // nouvelle facture avec client
                    // nouvelle facture 
                  
                    if (value != null && value.MoisPrestation.HasValue)
                    DateMoisPrestation = value.MoisPrestation;
                    CmbEnabled = false;
                    isFactureExist = false;
                    IsEnabledClient = true;
                    ActualFacture = null;
                    FactureCache = null;
                    isupdateinvoice = false;
                    BackgroundColorClient = true ;
                    isloading = false;
                   // LoadDefaultTaxes();
                  
                  
                    
                    AfficheStatut = "";
                   
                    ChkDateValidate = false;
                    IsEnabledchkDateValidate = false;
                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    idSocieteCourant = societeCourante.IdSociete;
                    Dictionary<int, string> valExploit = new Dictionary<int, string>();

                  

                    loadDatasnewFacture();

                    //type opération
                    operation = "creation";
                    //LoadDateAudit();
                   // Utils.logUserActions(string.Format("<-- UI edition facture --ouverture nouvelle facture par {0}", UserConnected.Loggin), "");
                }

                this.OnPropertyChanged("FatureCurrent");
            }
        }

        public string NewQtySelect
        {
            get { return newQtySelect; }
            set {
                
                CultureInfo currentCulture = CultureInfo.CurrentCulture;
                
                newQtySelect = value;

                if (value.Contains("."))
                {
                    isDaysCalcul = false;
                    Qtyselect = float.Parse(value.Replace(",",
                                                  currentCulture.NumberFormat.NumberDecimalSeparator).Trim().Replace(".",
                                                  currentCulture.NumberFormat.NumberDecimalSeparator).Trim(),
                                                   currentCulture) ;
                  
                    //Qtyselect = 1;
                }
                else if (value.Contains("/"))
                {
                    isDaysCalcul = true;

                    string[] tabQty = value.Split(new char[] { '/' });
                    double nbrejour = double.Parse(tabQty[0]);
                    double dureeTotal = double.Parse(tabQty[1]);
                    if (nbrejour > dureeTotal)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "ERREURE DE CALCULE QUATITE SAISIE";
                        view.ViewModel.Message = " le nombre de jour travaillé ne doit pas etre supérieur à la durée total";
                        view.ShowDialog();
                    }
                    else
                    {
                        if (nbrejour < dureeTotal)
                        {
                            long resteDiv = 0;

                            long nbreDiv = Math.DivRem((long)dureeTotal, (long)nbrejour, out resteDiv);
                            if (resteDiv > 5)
                                nbreDiv++;

                            newPrixUnit = Math.Round(PrixUnitaireselected / nbreDiv, 2);
                            string newQte = nbrejour + "." + dureeTotal;
                            newNewQte = double.Parse(newQte.Replace(".", currentCulture.NumberFormat.NumberDecimalSeparator), currentCulture);

                            // PrixUnitaireselected = (decimal)listeDetailProduitselect[i - 1].Prixunitaire;
                            //idDetailPode = listeDetailProduitselect[i - 1].IdDetail;
                            //detailPrixunit = PrixUnitaireselected;
                            //Isproratable = listeDetailProduitselect[i - 1].Isprorata;
                            //IsItemsExonere = listeDetailProduitselect[i - 1].Exonerer;
                            //IsPrixunitReadOnly = true;

                        }
                    }

                   
                }
                else
                {
                    isDaysCalcul = false;
                    Qtyselect =float .Parse (value);
                }
              
          
            this.OnPropertyChanged("NewQtySelect");
            }
        }


        public float Qtyselect
        {
           
            get { return qtyselect; }
            set
            {
                List<DetailProductModel> listeDetailProduitselect =null ;
                qtyselect = value;
                double jourfinprest = 0;
                double moyenCalc = 0;
                if (value > 0)
                {
                    if (ProduitSelected != null)
                    {
                        //si le prix unitaire existe
                        //if (ProduitSelected != null && ClientSelected != null)
                        //{
                       
                        if (LigneFacture != null)
                        {
                            if (SelectDetailListeProduit != null && SelectDetailListeProduit.Count > 0)
                            {
                                    // on recupere la liste des détail des produits
                                listeDetailProduitselect = SelectDetailListeProduit; 
                                //if (isOperationExploitation)
                                //{
                                //    listeDetailProduitselect = SelectDetailListeProduit;  //NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == FatureCurrent.IdExploitation);
                                //}
                                //else
                                //{
                                //    listeDetailProduitselect = SelectDetailListeProduit;// NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                //}
                                    if (listeDetailProduitselect != null && listeDetailProduitselect.Count > 0)
                                    {
                                        if (listeDetailProduitselect.Count > 1)
                                        {
                                            // parcourir la liste
                                            // si produt en mode facturation , calcul moyenne
                                            if (listeDetailProduitselect.Exists(dr => dr.Specialfact == true))
                                            {
                                                if (!string.IsNullOrEmpty(FatureCurrent.JourFinEcheance))
                                                {
                                                    AfficheStatutProduit = string.Empty;
                                                    PrixUnitaireselected = 0;
                                                    idDetailPode = 0;
                                                    detailPrixunit = 0;
                                                    long resteDiv = 0;

                                                    jourfinprest = double.Parse(FatureCurrent.JourFinEcheance);
                                                    long nbreDiv = Math.DivRem((long )LigneFacture.Quantite, (long)jourfinprest, out resteDiv);

                                                    //moyenCalc = (double)LigneFacture.Quantite / jourfinprest;
                                                    //moyenCalc = Math.Round(moyenCalc, 2);

                                                    moyenCalc = (double)nbreDiv;
                                                    AfficheStatutProduit = string.Format("{0} {1}", AfficheStatutProduit, " Qte moyenne :" + moyenCalc);

                                                    for (int i = 0; i < listeDetailProduitselect.Count; i++)
                                                    {
                                                        if (moyenCalc <= (double)listeDetailProduitselect[i].Quantite)
                                                        {
                                                            if (i == 0)
                                                            {
                                                                if (moyenCalc == (double)listeDetailProduitselect[i].Quantite)
                                                                {
                                                                    PrixUnitaireselected = (decimal)listeDetailProduitselect[0].Prixunitaire;
                                                                    idDetailPode = listeDetailProduitselect[0].IdDetail;
                                                                    Isproratable = listeDetailProduitselect[0].Isprorata;
                                                                    IsItemsExonere = listeDetailProduitselect[0].Exonerer;
                                                                    detailPrixunit = PrixUnitaireselected;
                                                                    IsPrixunitReadOnly = true;
                                                                    DetailProdEnable = false;
                                                                    IsModeFacturation = listeDetailProduitselect[0].Specialfact;
                                                                    if (LigneCommandList != null && LigneCommandList.Count >= 1)
                                                                        isOperationpossible = true ;
                                                                    break;
                                                                }
                                                                else
                                                                {
                                                                    PrixUnitaireselected = 0;
                                                                    AfficheStatutProduit = "Cette opération renvoi une moyenne inférieur à la borne minimale " + moyenCalc + " < " + listeDetailProduitselect[i].Quantite;
                                                                    if (LigneCommandList != null && LigneCommandList.Count >1)
                                                                     isOperationpossible = false;
                                                                    break;
                                                                }

                                                            }
                                                            else
                                                            {
                                                                if (moyenCalc == (double )listeDetailProduitselect[i].Quantite)
                                                                {
                                                                    PrixUnitaireselected = (decimal)listeDetailProduitselect[i].Prixunitaire;
                                                                    idDetailPode = listeDetailProduitselect[i].IdDetail;
                                                                    detailPrixunit = PrixUnitaireselected;
                                                                    Isproratable = listeDetailProduitselect[i].Isprorata;
                                                                    IsItemsExonere = listeDetailProduitselect[i].Exonerer;
                                                                    IsModeFacturation = listeDetailProduitselect[i].Specialfact;
                                                                    IsPrixunitReadOnly = true;
                                                                    DetailProdEnable = false;
                                                                    break;

                                                                }
                                                                else
                                                                {

                                                                    PrixUnitaireselected = (decimal)listeDetailProduitselect[i - 1].Prixunitaire;
                                                                    idDetailPode = listeDetailProduitselect[i - 1].IdDetail;
                                                                    detailPrixunit = PrixUnitaireselected;
                                                                    Isproratable = listeDetailProduitselect[i - 1].Isprorata;
                                                                    IsItemsExonere = listeDetailProduitselect[i - 1].Exonerer;
                                                                    IsModeFacturation = listeDetailProduitselect[i - 1].Specialfact;
                                                                    IsPrixunitReadOnly = true;
                                                                    DetailProdEnable = false;

                                                                    break;
                                                                }

                                                            }
                                                           

                                                        }
                                                        else
                                                        {

                                                            if (i == listeDetailProduitselect.Count - 1)
                                                            {
                                                                PrixUnitaireselected = (decimal)listeDetailProduitselect[i].Prixunitaire;
                                                                idDetailPode = listeDetailProduitselect[i].IdDetail;
                                                                detailPrixunit = PrixUnitaireselected;
                                                                Isproratable = listeDetailProduitselect[i].Isprorata;
                                                                IsItemsExonere = listeDetailProduitselect[i].Exonerer;
                                                                IsModeFacturation = listeDetailProduitselect[i].Specialfact;
                                                                IsPrixunitReadOnly = true;
                                                                DetailProdEnable = false;

                                                            }

                                                        }

                                                    }//fin for
                                                  

                                                }
                                                else
                                                {
                                                    CustomExceptionView view = new CustomExceptionView();
                                                    view.Owner = Application.Current.MainWindow;
                                                    view.Title = "MESSAGE ERREUR MISE JOUR FACTURE";
                                                    view.ViewModel.Message = "Le Jour fin de prestation est nécessaire pour ce produit";
                                                    view.ShowDialog();
                                                    LigneFacture.Quantite = 0;
                                                }
                                            }

                                            else
                                            {
                                                for (int i = 0; i < listeDetailProduitselect.Count; i++)
                                                {
                                                    if (LigneFacture.Quantite <= (decimal)listeDetailProduitselect[i].Quantite)
                                                    {
                                                        if (i == 0)
                                                        {
                                                            PrixUnitaireselected = (decimal)listeDetailProduitselect[i].Prixunitaire;
                                                            idDetailPode = listeDetailProduitselect[0].IdDetail;
                                                            Isproratable = listeDetailProduitselect[0].Isprorata;
                                                            IsItemsExonere = listeDetailProduitselect[0].Exonerer;
                                                            detailPrixunit = PrixUnitaireselected;
                                                            IsPrixunitReadOnly = true;
                                                            DetailProdEnable = false;
                                                            IsModeFacturation = listeDetailProduitselect[0].Specialfact;
                                                            break;

                                                        }
                                                        else
                                                        {
                                                            if (LigneFacture.Quantite == (decimal)listeDetailProduitselect[i].Quantite)
                                                            {
                                                                PrixUnitaireselected = (decimal)listeDetailProduitselect[i].Prixunitaire;
                                                                idDetailPode = listeDetailProduitselect[i].IdDetail;
                                                                detailPrixunit = PrixUnitaireselected;
                                                                Isproratable = listeDetailProduitselect[i].Isprorata;
                                                                IsItemsExonere = listeDetailProduitselect[i].Exonerer;
                                                                IsModeFacturation = listeDetailProduitselect[i].Specialfact;
                                                                IsPrixunitReadOnly = true;
                                                                DetailProdEnable = false;
                                                                break;
                                                            }
                                                            else
                                                            {

                                                                PrixUnitaireselected = (decimal)listeDetailProduitselect[i - 1].Prixunitaire;
                                                                idDetailPode = listeDetailProduitselect[i - 1].IdDetail;
                                                                detailPrixunit = PrixUnitaireselected;
                                                                Isproratable = listeDetailProduitselect[i - 1].Isprorata;
                                                                IsItemsExonere = listeDetailProduitselect[i - 1].Exonerer;
                                                                IsModeFacturation = listeDetailProduitselect[i - 1].Specialfact;
                                                                IsPrixunitReadOnly = true;
                                                                DetailProdEnable = false;
                                                                break;

                                                            }

                                                        }
                                                      

                                                    }
                                                    else
                                                    {

                                                        if (i == listeDetailProduitselect.Count - 1)
                                                        {
                                                            PrixUnitaireselected = (decimal)listeDetailProduitselect[i].Prixunitaire;
                                                            idDetailPode = listeDetailProduitselect[i].IdDetail;
                                                            detailPrixunit = PrixUnitaireselected;
                                                            Isproratable = listeDetailProduitselect[i].Isprorata;
                                                            IsItemsExonere = listeDetailProduitselect[i].Exonerer;
                                                            IsModeFacturation = listeDetailProduitselect[i].Specialfact;
                                                            IsPrixunitReadOnly = true;
                                                            DetailProdEnable = false;

                                                        }

                                                    }

                                                }//fin for
                                            }//fin traitement sans facturation

                                        }
                                    }//

                                

                            }

                        }




                    }

                }
                else
                {
                    PrixUnitaireselected = 0;
                    idDetailPode = 0;
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
                    isFactureOperation = true;
                    isOperationpossible = true;
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

        public int IndexProduit
        {
            get { return indexProduit; }
            set
            {
                indexProduit = value;
                this.OnPropertyChanged("IndexProduit");

            }
        }


        public bool IsEnableMarge
        {
            get { return isEnableMarge; }
            set { isEnableMarge = value;
            this.OnPropertyChanged("IsEnableMarge");
            }
        }

        public int IndexeCostTaxe
        {
            get { return indexeCostTaxe; }
            set { indexeCostTaxe = value;
            this.OnPropertyChanged("IndexeCostTaxe");
            }
        }


        public string TauxMargeBenef
        {
            get { return tauxMargeBenef; }
            set { tauxMargeBenef = value;
            this.OnPropertyChanged("TauxMargeBenef");
            }
        }


        public double MontantMargeBenef
        {
            get { return montantMargeBenef; }
            set { montantMargeBenef = value;
            this.OnPropertyChanged("MontantMargeBenef");
            }
        }

        public bool IsenableCostaxe
        {
            get { return isenableCostaxe; }
            set { isenableCostaxe = value;
            this.OnPropertyChanged("IsenableCostaxe");
            }
        }


        public bool IsenableTaxeList
        {
            get { return isenableTaxeList; }
            set { isenableTaxeList = value;
            this.OnPropertyChanged("IsenableTaxeList");
            }
        }

        public int IndexeTaxe
        {
            get { return indexeTaxe; }
            set { indexeTaxe = value;
            this.OnPropertyChanged("IndexeTaxe");
            }
        }

        public bool IsOperationClosing
        {
            get { return isOperationClosing; }
            set { isOperationClosing = value;
            this.OnPropertyChanged("IsOperationClosing");
            }
        }

        public bool IsVisibleButtonCancel
        {
            get { return isVisibleButtonCancel; }
            set { isVisibleButtonCancel = value;
            this.OnPropertyChanged("IsVisibleButtonCancel");
            }
        }


        public DateTime? DateAuditCreation
        {
            get { return dateAuditCreation; }
            set { dateAuditCreation = value;
            this.OnPropertyChanged("DateAuditCreation");
            }
        }


        public DateTime? DateAuditModification
        {
            get { return dateAuditModification; }
            set { dateAuditModification = value;
            this.OnPropertyChanged("DateAuditModification");
            }
        }


        public DateTime? DateAuditSortie
        {
            get { return dateAuditSortie; }
            set { dateAuditSortie = value;
            this.OnPropertyChanged("DateAuditSortie");
            }
        }


        public DateTime? DateAuditValidation
        {
            get { return dateAuditValidation; }
            set { dateAuditValidation = value;
            this.OnPropertyChanged("DateAuditValidation");
            }
        }
        

        public bool  BackgroundColorClient
        {
            get { return backgroundColorClient; }
            set { backgroundColorClient = value;
            this.OnPropertyChanged("BackgroundColorClient");
            }
        }

        public bool Enabledate
        {
            get { return enabledate; }
            set { enabledate = value;
            this.OnPropertyChanged("Enabledate");
            }
        }


        public DateTime? DateMoisPrestation
        {
            get { return dateMoisPrestation; }
            set { dateMoisPrestation = value;

            if (FatureCurrent != null)
            {
                if (!isReload)
                {
                    if (FatureCurrent.IdFacture > 0 && FatureCurrent.MoisPrestation.HasValue)
                    {
                        // vérification si mode de facturation
                        //parcour lites
                        if (LigneCommandList != null)
                        {
                            foreach (LigneCommand items in LigneCommandList)
                            {
                                List<DetailProductModel> liste = NewDetailListeProduit.FindAll(dt => dt.IdProduit == items.IdProduit);
                                if (liste != null)
                                {
                                    if (liste.Exists(e => e.Specialfact))
                                    {
                                        CustomExceptionView view = new CustomExceptionView();
                                        view.Owner = Application.Current.MainWindow;
                                        view.Title = "MODIFICATION MOIS DE PRESTATION";
                                        view.ViewModel.Message = string.Format("Cette date a été tenue compte lors du traitement du produit {0} \n Vous devez mettre à jour ce calcul", items.Produit);
                                        view.ShowDialog();
                                        FatureCurrent.MoisPrestation = value;
                                        if (LigneCommandList != null && LigneCommandList.Count > 1)
                                            isOperationpossible = false;
                                        break;
                                    }
                                    else
                                        FatureCurrent.MoisPrestation = value;
                                }
                                else
                                    FatureCurrent.MoisPrestation = value;
                            }
                        }

                    }
                    else
                    {
                        FatureCurrent.MoisPrestation = value;

                        //if (testeFacturationvalaible())
                        //{
                        //    isJourLimiteValable = true;
                        //}
                        //else isJourLimiteValable = false;
                      

                    }
                }
                else
                    FatureCurrent.MoisPrestation = value;
            }
            isFactureOperation = true;
            isOperationpossible = true;
            this.OnPropertyChanged("DateMoisPrestation");
            }
        }

        public bool IsModeFacturation
        {
            get { return isModeFacturation; }
            set { isModeFacturation = value;
            this.OnPropertyChanged("IsModeFacturation");
            }
        }

        public bool IsnewFactureEdite
        {
            get { return isnewFactureEdite; }
            set { isnewFactureEdite = value;
            this.OnPropertyChanged("IsnewFactureEdite");
            }
        }

        public bool IsVivisibleDestroyCommand
        {
            get { return isVivisibleDestroyCommand; }
            set { isVivisibleDestroyCommand = value;
            this.OnPropertyChanged("IsVivisibleDestroyCommand");
            }
        }

        public bool IsEnableCmbProduit
        {
            get { return isEnableCmbProduit; }
            set { isEnableCmbProduit = value;
            this.OnPropertyChanged("IsEnableCmbProduit");
            }
        }


        public bool IsdblClickoperation
        {
            get { return isdblClickoperation; }
            set
            {
                isdblClickoperation = value;
                isOperationpossible = true;
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

        public int? ClientIndex
        {
            get { return clientIndex; }
            set
            {
                //if (FatureCurrent.IdClient >= 0)
                //{
                    //if (value >= 0)
                        clientIndex = value;
               // }
                OnPropertyChanged("ClientIndex");
            }
        }

        public int? ObjetIndex
        {
            get { return objetIndex; }
            set
            {
                objetIndex = value;
                OnPropertyChanged("ObjetIndex");
            }
        }


        public int? ExploitationIndex
        {
            get { return exploitationIndex; }
            set
            {
                exploitationIndex = value;
                OnPropertyChanged("ExploitationIndex");
            }
        }

        public int? Departementindex
        {
            get { return departementindex; }
            set
            {
                departementindex = value;
                OnPropertyChanged("Departementindex");
            }
        }


        public int? Produitindex
        {
            get { return produitindex; }
            set
            {
                produitindex = value;
                OnPropertyChanged("Produitindex");
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
                if (!factureOnLoad)
                {
                    chkDateOutValidate = value;
                    ActualFacture = FatureCurrent;
                    if (value)
                    {
                        if (FatureCurrent.IdFacture > 0)
                        {
                            if (!FatureCurrent.DateSuspension.HasValue)
                            {
                                if (FatureCurrent.IdStatut == 14003)
                                {
                                    StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "4");
                                    CurrentStatut = newStatut;
                                    operation = "sortie";
                                    isOperationpossible = true;
                                    valeurDalidation = "devalide";
                                }
                                else
                                {
                                    isOperationpossible = false;
                                    operation = "";
                                    return;
                                }
                               // FactureModel newFact = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                //if (newFact.DateSuspension == null)
                                //{
                                  //  FatureCurrent.DateSortie = newFact.DateSortie != null ? newFact.DateSortie : DateTime.Now;
                                    //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(4);
                                 
                                  
                                //    operation = "sortie";
                                //}
                                //else
                                //{
                                //    MessageBox.Show("L'annulation de la suspension de cette facture est necessaire");
                                //    ChkDateOutValidate = false;
                                //    ChkDateSuspended = true;
                                //}
                            }
                            else
                            {
                               // MessageBox.Show("Cette Facture a été Suspendue, il est nécessaire de d'abord la valider");
                                operation = string.Empty;
                                ChkDateOutValidate = false;
                                return;
                            }
                        }
                    }
                    
                    isOperationpossible = true;
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
                if (!factureOnLoad)
                {
                    if (FatureCurrent.IdFacture > 0)
                    {
                        if (!FatureCurrent.DateSortie.HasValue)
                        {

                            if (value)
                            {
                                if (FatureCurrent.IdStatut <= 14003)
                                {
                                    StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "5");
                                    CurrentStatut = newStatut;
                                    operation = "supension";
                                    isOperationpossible = true;

                                    valeurDalidation = "devalide";
                                }
                            }
                            else
                            {
                                isOperationpossible = false;
                                operation = "";
                            }

                       
                           

                            //FactureModel newFact = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                            //if (newFact.DateSortie == null)
                            //{
                            //    FatureCurrent.DateSuspension = newFact.DateSuspension != null ? newFact.DateSuspension : DateTime.Now;
                            //    // StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(4);
                            //    StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "5");
                            //    CurrentStatut = newStatut;
                            //    operation = "supension";
                            //}
                            //else
                            //{
                            //    MessageBox.Show(" l'annulation de la sortie de cette facture est necessaire");
                            //    chkDateSuspended = false;
                            //    ChkDateOutValidate = true;

                            //}
                        }
                        else
                        {
                            MessageBox.Show("Cette Facture A déja fait l'objet dune Sortie, donc ne peut Pas Estre Suspendu");
                            operation = string.Empty;
                            chkDateSuspended = false;
                        }
                    }
                }
                else
                {
                    chkDateSuspended = value;
                    isOperationpossible = true;
                }
              
                //if (value)
                //{
                //    if (!factureOnLoad)
                //    {
                        
                //        }
                //    }
                //}
                //else
                //{
                //    if (!isnewFacture)
                //    {
                //        if (FatureCurrent.DateSuspension != null)
                //            operation = "supension";
                //        FatureCurrent.DateSuspension = null;
                //    }
                //}
                //isOperationpossible = true;
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


        #region CHECK REGION non valable

        public bool ChkNonValable
        {
            get { return chkNonValable; }
            set
            {
                chkNonValable = value;

                if (!factureOnLoad)
                {
                    if (value)
                    {
                        if (FatureCurrent.IdFacture > 0)
                        {
                            if (FatureCurrent.IdStatut == 14003)
                            {
                                StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "6");
                                CurrentStatut = newStatut;
                                operation = "nonvalable";
                                valeurDalidation = "devalide";
                                isOperationpossible = true;
                            }
                            else
                            {
                                operation = "";
                                isOperationpossible = false;
                                return;
                            }

                        }


                    }
                    else return;

                }
               

               
              
                this.OnPropertyChanged("ChkNonValable");
            }
        }


        public bool IsEnabledNonValable
        {
            get { return isEnabledNonValable; }
            set
            {
                isEnabledNonValable = value;
                isOperationpossible = true;
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
                isOperationpossible = true;
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
              
                int stat = 0;
                ActualFacture = FatureCurrent;
                StatutModel newCurrentStatut = FatureCurrent.CurrentStatut ?? statutservice.STATUT_FACTURE_GETLISTEByID(FatureCurrent.IdStatut);
                isValidateDate = value;


                if (value)
                {
                    if (int.Parse(newCurrentStatut.CourtDesc) == 1 && value)
                    {
                        valeurDalidation = "encours";
                        //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                        StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                        CurrentStatut = newStatut;
                        isEnabledchkDateValidate = value;
                        isOperationpossible = true;
                        isValidateDate = value;
                      
                    }
                    else if (int.Parse(newCurrentStatut.CourtDesc) == 2)
                    {
                        //if (BackgroundColorClient)
                        //{
                            valeurDalidation = "validation";
                            //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(3);
                            StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");
                            CurrentStatut = newStatut;
                            operation = "validation";
                            isEnabledchkDateValidate = value;
                            isOperationpossible = true;
                            isValidateDate = value;
                        //}
                        //else
                        //{
                        //    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        //    messageBox.Owner = Application.Current.MainWindow;
                        //    messageBox.Title = "INFORMATION  STATUT FACTURE";
                        //    messageBox.ViewModel.Message = "Impossible de valider la facture de ce Client \n ses informations ne sont pas complètes";
                        //    messageBox.ShowDialog();
                        //    isEnabledchkDateValidate = false;
                        //    operation = "validation";
                        //    valeurDalidation = "nonvalide";
                        //    isOperationpossible = false;
                        //    value = false;
                        //}

                    }
                    else if (int.Parse(newCurrentStatut.CourtDesc) == 5)
                    {
                        // facture suspendu sans validation ou sortie
                        if (valeurDalidation == "devalide")
                        {
                            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                            messageBox.Owner = Application.Current.MainWindow;
                            messageBox.Title = "INFORMATION  STATUT FACTURE";
                            messageBox.ViewModel.Message = "Cette Action va Annuler le Statut <<Suspension>>  actuelle de cette facture  ?";
                            if (messageBox.ShowDialog().Value == true)
                            {
                                operation = "validation";
                                valeurDalidation = "devalide";
                                CurrentStatut = newCurrentStatut;
                                isEnabledchkDateValidate = value;
                                isOperationpossible = true;
                                isValidateDate = value;
                            }
                            else isValidateDate = false;
                        }
                    }
                    else if (int.Parse(newCurrentStatut.CourtDesc) == 6)
                    {
                        if (valeurDalidation == "devalide")
                        {
                            if (droitFormulaire.Super || droitFormulaire.Developpeur || droitFormulaire.Validation || droitFormulaire.Proprietaire)
                            {
                                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                messageBox.Owner = Application.Current.MainWindow;
                                messageBox.Title = "INFORMATION  STATUT FACTURE";
                                messageBox.ViewModel.Message = "Cette Action va modifier le statut <<non Valide>>  au Statut Valide, voulez vous continuer  ?";
                                if (messageBox.ShowDialog().Value == true)
                                {
                                    operation = "validation";
                                    valeurDalidation = "devalide";
                                    CurrentStatut = newCurrentStatut;
                                    isEnabledchkDateValidate = value;
                                    isOperationpossible = true;
                                    isValidateDate = value;
                                }
                            }
                            else
                            {
                                MessageBox.Show(" Vous n'avez pas assez de privilèges pour effectuer cette Operation");
                                IsEnabledchkDateValidate = false;
                                isValidateDate = false;
                            }
                        }
                    }



                }
                else
                {
                    //décloture

                    if (!isnewFacture)
                    {
                        if (int.Parse(newCurrentStatut.CourtDesc) <= 2)
                        {
                            operation = "validation";
                            if (int.Parse(newCurrentStatut.CourtDesc)==1)
                            valeurDalidation = "encours";
                            else valeurDalidation = "validation";
                           // isOperationpossible = true;
                            //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                            if (!valeurDalidation.Equals("nonvalide"))
                            {
                                StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                CurrentStatut = newStatut;
                            }
                            isValidateDate = value;
                        }
                        else
                        {

                            if (int.Parse(newCurrentStatut.CourtDesc) == 3)
                            {
                                //if (droitFormulaire.Super)
                                //{
                                //    operation = "validation";
                                //    valeurDalidation = "devalide";
                                //    // StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                                //    StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                //    CurrentStatut = newCurrentStatut;
                                //    MessageBox.Show("Facture déja valider \n Cette action va la dévalider ");
                                //    IsEnabledchkDateValidate = true;
                                //   // isOperationpossible = true;
                                //}
                                //else
                                //{
                                    MessageBox.Show("Facture déja valider \n Vous navez pas assez de privilèges pour la dévalider ");
                                    IsEnabledchkDateValidate = false;
                                    isValidateDate = true;
                               // }
                            }
                            else
                                //{
                                if (int.Parse(newCurrentStatut.CourtDesc) == 4)
                                {
                                    if (droitFormulaire.Super)
                                    {
                                        MessageBox.Show(" Cette facture est déja sortie, modification impossible");
                                        IsEnabledchkDateValidate = true;
                                       // isOperationpossible = false;
                                        isValidateDate = true;
                                    }
                                    else
                                    {
                                        MessageBox.Show("Facture déja sortie \n Vous navez pas assez de privilèges pour effectuer cette modification ");
                                        IsEnabledchkDateValidate = false;
                                        isValidateDate =true;
                                    }
                                }
                                else
                                    if (int.Parse(newCurrentStatut.CourtDesc) == 5)
                                    {
                                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                        messageBox.Owner = Application.Current.MainWindow;
                                        messageBox.Title = "INFORMATION  STATUT FACTURE";
                                        messageBox.ViewModel.Message = "Cette action va annuler le Statut Suspension actuelle de cette facture  ?";
                                        if (messageBox.ShowDialog().Value == true)
                                        {
                                            operation = "validation";
                                            valeurDalidation = "devalide";
                                            //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                                            //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                            CurrentStatut = newCurrentStatut;
                                            IsEnabledchkDateValidate = true;
                                            isValidateDate = value;
                                           // isOperationpossible = true;
                                        }
                                        else isValidateDate = false;
                                    }
                                    else
                                        if (int.Parse(newCurrentStatut.CourtDesc) == 6)
                                        {
                                            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                            messageBox.Owner = Application.Current.MainWindow;
                                            messageBox.Title = "INFORMATION  STATUT FACTURE";
                                            messageBox.ViewModel.Message = "Cette action va annuler le atatut <<Non valide>> actuelle de cette facture  ?";
                                            if (messageBox.ShowDialog().Value == true)
                                            {
                                                operation = "validation";
                                                valeurDalidation = "devalide";
                                                //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTEByID(2);
                                                StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "2");
                                                CurrentStatut = newCurrentStatut;
                                                IsEnabledchkDateValidate = true;
                                                isValidateDate = value;
                                               // isOperationpossible = true;
                                            }
                                        }
                                        else operation = "impossible";




                        }
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
                    isnewClient = true;
                    isnewFacture = false;

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

                                    FatureCurrent = new FactureModel();
                                    LigneFacture = null;
                                    PrixUnitaireselected = 0;
                                    IsItemsExonere = false;
                                    ExonereEnable = false;
                                    ProduiList = null;
                                    _clientSelected = value;
                                    FTotalHt = 0;
                                    Ftotaltva = 0;
                                    FTotalProrata = 0;
                                    MontantProrata = "";
                                    Montanttva = "";
                                    FtotalTTC = 0;
                                    LigneCommandList = null;
                                    CacheLigneCommandList = null;
                                    isnewClient = true;
                                    Listexp = string.Empty;
                                    ListExpid = string.Empty;
                                    ExploitationFields = new Dictionary<int, string>();
                                    isligneitemsoperation = false;
                                    isFactureOperation = false;
                                }
                                else isnewClient = false;
                            }
                        }
                    }
                    else
                    {
                        _clientSelected = value;
                    }
                    if (isnewClient)
                    {
                        _clientSelected = value;

                        if (FatureCurrent == null || FatureCurrent.IdFacture == 0)
                        {
                            //DeviseModel devise = deviseService.Devise_SELECT(societeCourante.IdSociete).Find(d => d.IsDefault == true);
                            DeviseSelected = value.DeviseFacture ?? deviseService.Devise_SELECT(societeCourante.IdSociete).Find(d => d.IsDefault == true);

                            loadDataClientselected();
                        }
                        CmbEnabled = true;
                        if (LigneCommandList!=null )
                        if (LigneCommandList.Count <= 0)
                        {
                            FatureCurrent.MoisPrestation = null;
                            DateMoisPrestation = null;
                            FatureCurrent.JourFinEcheance = string.Empty;
                        }
                    }

                    LigneFacture = new LigneFactureModel();
                    Lignecourante = new LigneCommand();
                    PrixUnitaireselected = 0;
                    AfficheStatutProduit = "";
                    FatureCurrent.IdClient = value.IdClient;
                    ActualFacture = FatureCurrent;
                    //loadLanguageLientselejctInfo();
                    isFactureOperation = true;
                    isOperationpossible = true;
                    DeviseConvert = value.DeviseConversion;

                    //if (value.DeviseConversion == null)
                    //    DeviseConvert = deviseService.Devise_SELECTById(value.IdDeviseConversion, societeCourante.IdSociete);
                    //else
                    //    DeviseConvert = FatureCurrent.CurrentClient != null ? FatureCurrent.CurrentClient.Devise : value.Devise;


                    TaxePorataSelected = value.Porata ?? new TaxeModel ();// taxeService.Taxe_SELECTById(value.Idporata, societeCourante.IdSociete);

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
                    if (ClientSelected.Exonerere != null)
                    {
                        if (ClientSelected.Exonerere.CourtDesc == "part")
                            CulumndetailVisible = true;
                        else CulumndetailVisible = false;
                    }

                 
                }
                else
                {
                    if (FatureCurrent != null && FatureCurrent.IdClient > 0)
                    {
                         //loadFactureInformation();
                        factureOnLoad = false;
                    }
                    else
                        _clientSelected = value;
                    BackgroundColorClient = false ;
                }
                Enabledate = true;
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
            //set
            //{
            //    //NewDetailListeProduit
            //    _exploitationSelected = value;
            //    if (value != null)
            //    {
            //        if (FatureCurrent != null)
            //            FatureCurrent.IdExploitation = value.IdExploitation;

            //        //CacheProduiList
            //        if (value.IdExploitation == 0)
            //        {
            //            var defaulList = NewDetailListeProduit.FindAll(p => p.IdExploitation == 0);
            //            if (defaulList != null && defaulList.Count > 0)
            //            {
            //                IdProduits = new HashSet<int>();
            //                PrixUnitaireselected = 0;
            //                AfficheStatutProduit = string.Empty;
            //                if (LigneFacture != null)
            //                    LigneFacture.Quantite = 0;
            //                ObservableCollection<ProduitModel> newListeProduits = new ObservableCollection<ProduitModel>();
            //                foreach (DetailProductModel det in defaulList)
            //                    IdProduits.Add(det.IdProduit);

            //                foreach (int id in IdProduits)
            //                    newListeProduits.Add(defaulList.Find(p => p.IdProduit == id).Produit);

            //                ProduiList = newListeProduits;
            //                CacheProduiList = ProduiList;
            //                isOperationExploitation = true;

            //            }
            //            else
            //            {
            //                ProduiList = new ObservableCollection<ProduitModel>();
            //                CacheProduiList = ProduiList;
            //                isOperationExploitation = false;
            //            }


            //        }
            //        else
            //        {
            //            if (ProduiList != null && ProduiList.Count > 0)
            //            {
            //                List<DetailProductModel> dets = new List<DetailProductModel>();
            //                foreach (var lst in NewDetailListeProduit)
            //                    dets.Add(lst);
            //                //NewDetailListeProduit = null;
            //                //NewDetailListeProduit = dets;
            //                IdProduits = new HashSet<int>();
            //                PrixUnitaireselected = 0;
            //                AfficheStatutProduit = string.Empty;
            //                if (LigneFacture != null)
            //                    LigneFacture.Quantite = 0;

            //                // NewDetailListeProduit = CachnewDetailListeProduit;
            //                var newListe = NewDetailListeProduit.FindAll(p => p.IdExploitation == value.IdExploitation);
            //                var newListeDefault = NewDetailListeProduit.FindAll(p => p.IdExploitation == 0);

            //                ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
            //                if (newListe.Count == 0)
            //                {
            //                    // liste que les produits qui ne sont jamais 
            //                    var oDoublonList = NewDetailListeProduit.FindAll(op => op.IdExploitation != 0);

            //                    foreach (var inDet in oDoublonList)
            //                    {
            //                        var elDet = newListeDefault.Find(pr => pr.IdProduit == inDet.IdProduit);
            //                        if (elDet != null)
            //                        {
            //                            newListeDefault.Remove(elDet);
            //                        }

            //                    }
            //                    newListe = newListeDefault;
            //                }
            //                else
            //                {
            //                    foreach (var newDet in newListe)
            //                    {
            //                        var detProdPlus = newListeDefault.Find(dtm => dtm.IdProduit == newDet.IdProduit && dtm.IdExploitation != value.IdExploitation);
            //                        if (detProdPlus != null)
            //                            newListeDefault.Remove(detProdPlus);
            //                    }

            //                    foreach (var dt in newListeDefault)
            //                    {
            //                        newListe.Add(dt);
            //                    }
            //                }

            //                if (newListe.Count > 0)
            //                {
            //                    foreach (DetailProductModel det in newListe)
            //                        IdProduits.Add(det.IdProduit);
            //                    foreach (int id in IdProduits)
            //                        newListeProduie.Add(newListe.Find(p => p.IdProduit == id).Produit);

            //                    ProduiList = newListeProduie;
            //                    CacheProduiList = ProduiList;
            //                    isOperationExploitation = true;
            //                }
            //                else
            //                {
            //                    // var prodWithexploit = NewDetailListeProduit.FindAll(np => np.IdExploitation == 0);
            //                    foreach (DetailProductModel det in newListe)
            //                        IdProduits.Add(det.IdProduit);
            //                    foreach (int id in IdProduits)
            //                        newListeProduie.Add(newListe.Find(p => p.IdProduit == id).Produit);

            //                    ProduiList = newListeProduie;
            //                    CacheProduiList = ProduiList;
            //                    isOperationExploitation = false;
            //                }




            //            }
            //            else
            //            {
            //                // produite avec exploitation
            //                // IsEnableCmbProduit = false ;
            //                IdProduits = new HashSet<int>();
            //                var newListe = NewDetailListeProduit.FindAll(p => p.IdExploitation == value.IdExploitation);
            //                // var newListeDefault = null;// NewDetailListeProduit.FindAll(p => p.IdExploitation == 0);
            //                ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
            //                if (newListe.Count > 0)
            //                {
            //                    foreach (DetailProductModel det in newListe)
            //                        IdProduits.Add(det.IdProduit);
            //                    foreach (int id in IdProduits)
            //                        newListeProduie.Add(newListe.Find(p => p.IdProduit == id).Produit);

            //                    ProduiList = newListeProduie;
            //                    CacheProduiList = ProduiList;
            //                    isOperationExploitation = true;
            //                    IsEnableCmbProduit = true;
            //                    IsdetailExiste = true;

            //                }
            //            }
            //        }
            //    }
            //    this.OnPropertyChanged("ExploitationSelected");
            //}



            set
            {
                //NewDetailListeProduit
                _exploitationSelected = value;
                if (value != null)
                {
                    if (FatureCurrent != null)
                        FatureCurrent.IdExploitation = value.IdExploitation;

                    //CacheProduiList
                    //si pas affiche tous les produits client
                    if (value.IdExploitation == 0)
                    {
                        var defaulList = NewDetailListeProduit.FindAll(p => p.IdExploitation == 0);
                        if (defaulList != null && defaulList.Count > 0)
                        {
                            IdProduits = new HashSet<int>();
                            PrixUnitaireselected = 0;
                            AfficheStatutProduit = string.Empty;
                            if (LigneFacture != null)
                                LigneFacture.Quantite = 0;
                            ObservableCollection<ProduitModel> newListeProduits = new ObservableCollection<ProduitModel>();
                            foreach (DetailProductModel det in defaulList)
                                IdProduits.Add(det.IdProduit);

                            foreach (int id in IdProduits)
                                newListeProduits.Add(defaulList.Find(p => p.IdProduit == id).Produit);

                            ProduiList = newListeProduits;
                            CacheProduiList = ProduiList;
                            isOperationExploitation = true;

                        }
                        else
                        {
                            ProduiList =new ObservableCollection<ProduitModel> ();
                            CacheProduiList = ProduiList;
                            isOperationExploitation = false ;
                        }
                       

                    }
                    else
                    {
                        //if (ProduiList != null && ProduiList.Count > 0)
                        //{
                        //    List<DetailProductModel> dets = new List<DetailProductModel>();
                        //    foreach (var lst in NewDetailListeProduit)
                        //        dets.Add(lst);
                        //    //NewDetailListeProduit = null;
                        //    //NewDetailListeProduit = dets;
                        //    IdProduits = new HashSet<int>();
                        //    PrixUnitaireselected = 0;
                        //    AfficheStatutProduit = string.Empty;
                        //    if (LigneFacture != null)
                        //        LigneFacture.Quantite = 0;

                        //    // NewDetailListeProduit = CachnewDetailListeProduit;
                        //    var newListe = NewDetailListeProduit.FindAll(p => p.IdExploitation == value.IdExploitation);
                        //    var newListeDefault = NewDetailListeProduit.FindAll(p => p.IdExploitation == 0);
                        //    ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
                        //    if (newListe.Count == 0)
                        //    {
                                
                        //        newListe = newListeDefault;
                        //    }
                        //    else
                        //    {

                        //    }

                        //    foreach (DetailProductModel det in newListe)
                        //        IdProduits.Add(det.IdProduit);
                        //    foreach (int id in IdProduits)
                        //        newListeProduie.Add(newListe.Find(p => p.IdProduit == id).Produit);

                        //    ProduiList = newListeProduie;
                        //    CacheProduiList = ProduiList;
                        //    isOperationExploitation = true;
                        //}
                        //else
                        //{
                            // produite avec exploitation
                            // IsEnableCmbProduit = false ;

                            IdProduits = new HashSet<int>();
                        //  si explotation chargement ses prod et les prod sans exploitation
                             var newListe = NewDetailListeProduit.FindAll(p => p.IdExploitation == value.IdExploitation);
                             var newListeDefault = NewDetailListeProduit.FindAll(p => p.IdExploitation == 0);

                            ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
                            if (newListe.Count > 0)
                            {
                                foreach (DetailProductModel det in newListe)
                                    IdProduits.Add(det.IdProduit);

                                  foreach (DetailProductModel det in newListeDefault)
                                    IdProduits.Add(det.IdProduit);

                                foreach (int id in IdProduits)
                                    newListeProduie.Add(NewDetailListeProduit.Find(p => p.IdProduit == id).Produit);

                                ProduiList = newListeProduie;
                                CacheProduiList = ProduiList;
                                isOperationExploitation = true;
                                IsEnableCmbProduit = true;
                                IsdetailExiste = true;

                            }
                            else
                            {
                                // pas dexploitation lié , produits dun client
                                // si une exploitation nest pas relier à un produit, on charge les produit sans expxploitation
                                var defaulList = NewDetailListeProduit.FindAll(p => p.IdExploitation == 0);
                                if (defaulList != null && defaulList.Count > 0)
                                {
                                    IdProduits = new HashSet<int>();
                                    PrixUnitaireselected = 0;
                                    AfficheStatutProduit = string.Empty;
                                    if (LigneFacture != null)
                                        LigneFacture.Quantite = 0;
                                    ObservableCollection<ProduitModel> newListeProduits = new ObservableCollection<ProduitModel>();
                                    foreach (DetailProductModel det in defaulList)
                                        IdProduits.Add(det.IdProduit);

                                    foreach (int id in IdProduits)
                                        newListeProduits.Add(defaulList.Find(p => p.IdProduit == id).Produit);

                                    ProduiList = newListeProduits;
                                    CacheProduiList = ProduiList;
                                    isOperationExploitation = true;

                                }
                                else
                                {
                                    ProduiList = new ObservableCollection<ProduitModel>();
                                    CacheProduiList = ProduiList;
                                    isOperationExploitation = false;
                                    IsEnableCmbProduit = false;
                                    IsdetailExiste = false;
                                    MessageBox.Show("Pas De produits sans exploitation");
                                }
                            }
                       // }
                    }

                    isFactureOperation = true;
                    isOperationpossible = true;
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
                    isFactureOperation = true;
                }
                else IsenableObjet = false;


                this.OnPropertyChanged("ObjetSelected");
            }
        }
        #endregion

        #region PRODUIT REGION

        public List<DetailProductModel> CachnewDetailListeProduit
        {
            get { return cachnewDetailListeProduit; }
            set { cachnewDetailListeProduit = value;
            this.OnPropertyChanged("CachnewDetailListeProduit");
            }
        }

        public List<DetailProductModel> NewDetailListeProduit
        {
            get { return newDetailListeProduit; }
            set { newDetailListeProduit = value;
            this.OnPropertyChanged("NewDetailListeProduit");
            }
        }

        public List<DetailProductModel> SelectDetailListeProduit
        {
            get { return selectDetailListeProduit; }
            set
            {
                selectDetailListeProduit = value;
                this.OnPropertyChanged("SelectDetailListeProduit");
            }
        }


        public DetailProductModel DetailProduitSelected
        {
            get { return detailProduitSelected; }
            set { detailProduitSelected = value;
            this.OnPropertyChanged("DetailProduitSelected");
            }
        }



        public List<ProduitModel> NewlisteProduit
        {
            get { return newlisteProduit; }
            set { newlisteProduit = value;
            this.OnPropertyChanged("NewlisteProduit");
            }
        }

        public ProduitModel ProduitSelected
        {
            get { return _produitSelected; }
            set

            {
                if (isloading)
                    _produitSelected = null;
                else
                _produitSelected = value;

                VisibilityValiderReadQty = false;
                List<DetailProductModel> listeproduits;
               // if (value != null)
                //{
                    if (ProduitSelected != null)
                    {

                        if (LigneCommandList != null)
                        {
                            // si produit deja selectionner
                            if (LigneCommandList.Exists(p => p.IdProduit == value.IdProduit) && !IsdblClickoperation)
                            {

                                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                messageBox.Owner = Application.Current.MainWindow;
                                messageBox.Title = "MESSAGE INFORMATION ";
                                messageBox.ViewModel.Message = "Ce Produit Existe déja dans cette commande, Voulez vous Ajouter";
                                if (messageBox.ShowDialog().Value == true)
                                {

                                    if (NewDetailListeProduit != null && NewDetailListeProduit.Count > 0)
                                    {
                                         //listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit);

                                        if (ExploitationSelected == null)
                                        {
                                            listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                        }
                                        else
                                        {
                                            if (ExploitationSelected.IdExploitation == 0)
                                                listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                            else
                                            {
                                                listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == ExploitationSelected.IdExploitation);
                                                if (listeproduits.Count == 0)
                                                {
                                                    //Default team
                                                    listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                                }

                                            }
                                        }
                                        SelectDetailListeProduit = listeproduits;

                                        if (listeproduits.Count == 1)
                                        {
                                            PrixUnitaireselected = (decimal)listeproduits[0].Prixunitaire;
                                            IsModeFacturation = listeproduits[0].Specialfact;
                                            if (LigneFacture != null)
                                            {
                                                LigneFacture.Quantite = (decimal)listeproduits[0].Quantite;
                                                if (PrixUnitaireselected == 1)
                                                {
                                                    IsPrixunitReadOnly = false; // prix modifiable
                                                    DetailProdEnable = true;// qte non  modifiable
                                                }
                                                else
                                                {
                                                    IsPrixunitReadOnly = true; // prix modifiable
                                                    DetailProdEnable = false ;// qte   modifiable
                                                    VisibilityValiderReadQty = false;

                                                }
                                            }
                                            //DetailProdEnable = false ;// qte modifiable
                                            idDetailPode = listeproduits[0].IdDetail;
                                            Isproratable = listeproduits[0].Isprorata;
                                            IsItemsExonere = listeproduits[0].Exonerer;
                                            detailPrixunit = (decimal)listeproduits[0].Prixunitaire ;

                                            if (listeproduits[0].Specialfact == true)
                                            {
                                                AfficheStatutProduit = "Ce Produit fera l'objet dun calcul moyen";
                                                IsModeFacturation = true;
                                            }
                                            else
                                            {
                                                AfficheStatutProduit = string.Empty;
                                                IsModeFacturation = false;
                                            }
                                        }
                                        else
                                        {
                                            DetailProdEnable = false;
                                            PrixUnitaireselected = 0;
                                          
                                            if (LigneFacture != null)
                                                LigneFacture.Quantite = 0;

                                          //  PrixUnitaireselected = 0;
                                            Communicator cm = new Communicator();
                                            EventArgs e11 = new EventArgs();
                                            cm.OnChangeClearQuantity(e11);

                                            IsModeFacturation = listeproduits.Exists(dr => dr.Specialfact == true);
                                          //  DetailProdEnable = false;

                                            if (IsModeFacturation)
                                                AfficheStatutProduit = "ce produit fera l'objet dun calcul moyen";
                                            else AfficheStatutProduit = string.Empty;
                                        }
                                    }

                                 
                                }
                                else
                                {
                                    // pad de modification du produit en cours
                                }

                            }
                            else
                            {

                                // liste contenant deja des produits

                                if (NewDetailListeProduit != null && NewDetailListeProduit.Count > 0)
                                {
                                   // listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit);

                                    if (ExploitationSelected == null)
                                    {
                                        listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                    }
                                    else
                                    {
                                        if (ExploitationSelected.IdExploitation == 0)
                                            listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                        else
                                        {
                                            listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == ExploitationSelected.IdExploitation);
                                            if (listeproduits.Count == 0)
                                            {
                                                //Default team
                                                listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                            }

                                        }
                                    }

                                    SelectDetailListeProduit = listeproduits;

                                    if (listeproduits.Count == 1)
                                    {
                                        if (listeproduits[0].IdExploitation > 0)
                                        {
                                           
                                                PrixUnitaireselected = (decimal)listeproduits[0].Prixunitaire;
                                                if (LigneFacture != null)
                                                {
                                                    LigneFacture.Quantite = (decimal)listeproduits[0].Quantite;
                                                    IsModeFacturation = listeproduits[0].Specialfact;
                                                    if (PrixUnitaireselected == 1)
                                                    {
                                                        IsPrixunitReadOnly = false; // prix modifiable
                                                        DetailProdEnable = true;// qte non  modifiable
                                                    }
                                                    else
                                                    {
                                                        IsPrixunitReadOnly = true; // prix modifiable
                                                        DetailProdEnable = false ;// qte  modifiable
                                                        VisibilityValiderReadQty = false;
                                                    }
                                                }
                                               // DetailProdEnable =false ;// qte modifiable
                                                idDetailPode = listeproduits[0].IdDetail;
                                                Isproratable = listeproduits[0].Isprorata;
                                                IsItemsExonere = listeproduits[0].Exonerer;
                                                detailPrixunit = (decimal)listeproduits[0].Prixunitaire ;

                                          
                                        }
                                        else
                                        {
                                            // pas dexploitation rattaché

                                            PrixUnitaireselected = (decimal)listeproduits[0].Prixunitaire;
                                            if (LigneFacture != null)
                                            {
                                                LigneFacture.Quantite = (decimal)listeproduits[0].Quantite;
                                                IsModeFacturation = listeproduits[0].Specialfact;
                                                if (PrixUnitaireselected == 1)
                                                {
                                                    IsPrixunitReadOnly = false; // prix modifiable
                                                    DetailProdEnable = true;// qte non  modifiable
                                                }
                                                else
                                                {
                                                    IsPrixunitReadOnly = true; // prix non modifiable
                                                    VisibilityValiderReadQty = false;
                                                    DetailProdEnable = false ;// qte  modifiable
                                                }
                                            }
                                           // DetailProdEnable =false ;// qte modifiable
                                           

                                        }

                                        idDetailPode = listeproduits[0].IdDetail;
                                        Isproratable = listeproduits[0].Isprorata;
                                        IsItemsExonere = listeproduits[0].Exonerer;
                                        detailPrixunit = (decimal)listeproduits[0].Prixunitaire;

                                        if (IsModeFacturation)
                                        {
                                            AfficheStatutProduit = "ce produit fera l'objet dun calcul moyen";
                                            IsModeFacturation = true;
                                        }
                                        else
                                        {
                                            AfficheStatutProduit = string.Empty;
                                            IsModeFacturation = false;
                                        }
                                    }
                                    else
                                    {
                                        DetailProdEnable = false;
                                        //if (ExploitationSelected != null)
                                        //{
                                        //    //if (isOperationExploitation)
                                        //    //{
                                        //    //    listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == FatureCurrent.IdExploitation);
                                        //    //}
                                        //    //else
                                        //    //{
                                        //    //    listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);

                                        //    //}

                                        //    PrixUnitaireselected = (decimal)listeproduits[0].Prixunitaire;
                                        //    if (LigneFacture != null)
                                        //    {
                                        //        LigneFacture.Quantite = (decimal)listeproduits[0].Quantite;
                                        //        IsModeFacturation = listeproduits[0].Specialfact;
                                        //        if (PrixUnitaireselected == 1)
                                        //        {
                                        //            IsPrixunitReadOnly = false; // prix modifiable
                                        //            DetailProdEnable = true;// qte non  modifiable
                                        //        }
                                        //        else
                                        //        {
                                        //            IsPrixunitReadOnly = true; // prix modifiable
                                        //            DetailProdEnable = false;// qte  modifiable
                                        //            VisibilityValiderReadQty = false;
                                        //        }
                                        //    }

                                        //    idDetailPode = listeproduits[0].IdDetail;
                                        //    Isproratable = listeproduits[0].Isprorata;
                                        //    IsItemsExonere = listeproduits[0].Exonerer;
                                        //    detailPrixunit = (decimal)listeproduits[0].Prixunitaire;

                                        //}
                                        //else
                                        //{
                                        //    MessageBox.Show("Préciser l'exploitation auquel le produit est rattaché");
                                        //}

                                        PrixUnitaireselected = 0;
                                        Communicator cm = new Communicator();
                                        EventArgs e11 = new EventArgs();
                                        cm.OnChangeClearQuantity(e11);

                                        IsModeFacturation = listeproduits.Exists(dr => dr.Specialfact == true);
                                        DetailProdEnable = false;

                                        if (IsModeFacturation)
                                            AfficheStatutProduit = "ce produit fera l'objet dun calcul moyen";
                                        else AfficheStatutProduit = string.Empty;
                                    
                                    }
                                }
                            }

                        }
                        else
                        {
                            // nouveau produit
                            if (NewDetailListeProduit != null && NewDetailListeProduit.Count > 0)
                            {

                                if (ExploitationSelected == null)
                                {
                                    listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                }
                                else
                                {
                                    if (ExploitationSelected.IdExploitation == 0)
                                        listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == 0);
                                    else
                                    {
                                        listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation == ExploitationSelected.IdExploitation);
                                        if (listeproduits.Count == 0)
                                        {
                                            //Default team
                                            listeproduits = NewDetailListeProduit.FindAll(p => p.IdProduit == ProduitSelected.IdProduit && p.IdExploitation ==0);
                                        }
                                        
                                    }
                                }
                                SelectDetailListeProduit = listeproduits;
                                 
                                IsPrixunitReadOnly = false;
                                DetailProdEnable = false;

                                if (listeproduits.Count == 1)
                                {
                                    PrixUnitaireselected = (decimal)listeproduits[0].Prixunitaire;
                                    IsModeFacturation = listeproduits[0].Specialfact;

                                    if (LigneFacture != null)
                                    {
                                        LigneFacture.Quantite = (decimal)listeproduits[0].Quantite;

                                        if (PrixUnitaireselected == 1)
                                        {
                                            IsPrixunitReadOnly = false; // prix modifiable
                                            DetailProdEnable = true;// qte non  modifiable
                                        }
                                        else
                                        {
                                            IsPrixunitReadOnly = true; // prix non  modifiable
                                            VisibilityValiderReadQty = false;
                                        }
                                    }

                                    idDetailPode = listeproduits[0].IdDetail;
                                    Isproratable = listeproduits[0].Isprorata;
                                    IsItemsExonere = listeproduits[0].Exonerer;
                                    detailPrixunit = (decimal)listeproduits[0].Prixunitaire;

                                   // IsModeFacturation = listeproduits[0].Specialfact == true;
                                    if (listeproduits[0].Specialfact == true)
                                    {
                                        AfficheStatutProduit = "ce produit fera l'objet dun calcul moyen";
                                        IsModeFacturation = true;
                                    }
                                    else
                                    {
                                        AfficheStatutProduit = string.Empty;
                                        IsModeFacturation = false;
                                    }
                                }
                                else
                                {
                                   
                                        DetailProdEnable = false;
                                       

                                         if (listeproduits.Count > 1)
                                         {
                                             PrixUnitaireselected = 0;
                                             Communicator cm = new Communicator();
                                             EventArgs e11 = new EventArgs();
                                             cm.OnChangeClearQuantity(e11);

                                             IsModeFacturation = listeproduits.Exists(dr => dr.Specialfact == true);
                                             DetailProdEnable = false;

                                             if (IsModeFacturation)
                                                 AfficheStatutProduit = "ce produit fera l'objet dun calcul moyen";
                                             else AfficheStatutProduit = string.Empty;
                                         }

                                      
                                    
                               }

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



                                }
                            }

                        }
                       

                        Lignecourante = new LigneCommand();

                    }
                    
               // }

                //if (value!=null )
                //{
                //      List<DetailProductModel> listeDetailProduitselect=NewDetailListeProduit.FindAll(p => p.IdProduit == value.IdProduit);
                //      if (listeDetailProduitselect.Count > 0)
                //      {
                //          if (listeDetailProduitselect[0].Specialfact)
                //              AfficheStatutProduit = "ce produit fera l'objet dun calcul moyen";
                //          else AfficheStatutProduit = string.Empty;
                //      }
                  
                //}
                   

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
                isupdateinvoice = true;
                if (FatureCurrent.CurrentStatut != null)
                    if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2)
                    {
                        DetailProdEnable =false ;
                        IsEnableCmbProduit = true;
                        IsdetailExiste = true;
                    }
                this.OnPropertyChanged("OldProduitSelected");
            }
        }
        #endregion

        #region TAXES REGION

        public List<TaxeModel> CostTaxeList
        {
            get { return _costTaxeList; }
            set { _costTaxeList = value;
            this.OnPropertyChanged("CostTaxeList");
            }
        }


        public TaxeModel CostTaxeSelected
        {
            get { return _costTaxeSelected; }
            set { _costTaxeSelected = value;
            isFactureOperation = true;
            isOperationpossible = true;
            if (value != null)
            {
                if (value.ID_Taxe == 0)
                {
                    TauxMargeBenef = "";
                    FtotalTTC -= MontantMargeBenef;
                    MontantMargeBenef = 0;


                    if (onloading)
                    {
                        // onloading = false;
                        isFactureOperation = false;
                        isligneitemsoperation = false;
                        isOperationpossible = false;
                    }

                    onloading = false;
                    CostTaxeSelected = null;
                }
                else
                {
                    if (onloading)
                    {
                        if (FatureCurrent.MaregeBeneficiaireId != null)
                            TauxMargeBenef = value.Taux;



                        onloading = false;
                        isFactureOperation = false;
                        isligneitemsoperation = false;
                        isOperationpossible = false;
                    }
                    else
                    {
                        if (LigneCommandList != null && LigneCommandList.Count > 0)
                        {

                            TauxMargeBenef = value.Taux;
                            MontantMargeBenef = GetTaxeFr(value.Taux, currentCulture) * FTotalHt;
                            FtotalTTC += MontantMargeBenef;

                        }
                    }
                }
            }
          
            this.OnPropertyChanged("CostTaxeSelected");
            }
        }

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
                isOperationpossible = true;
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

        public bool ModeFacturenormaleEnable
        {
            get { return modeFacturenormaleEnable; }
            set { modeFacturenormaleEnable = value;
            isOperationpossible = true;
            OnPropertyChanged("ModeFacturenormaleEnable");
            
            }
        }
       

        public bool ModeFactureAvoirEnable
        {
            get { return modeFactureAvoirEnable; }
            set { modeFactureAvoirEnable = value;
            isOperationpossible = true;
            OnPropertyChanged("ModeFactureAvoirEnable");
            
            }
        }


        public string ListExpid
        {
            get { return listExpid; }
            set { listExpid = value;
            OnPropertyChanged("ListExpid");
            }
        }

        public string Listexp
        {
            get { return listexp; }
            set { listexp = value;
            OnPropertyChanged("Listexp");
            }
        }

        public Dictionary<int, string> ExploitationFields
        {
            get { return exploitationFields; }
            set { exploitationFields = value;
            OnPropertyChanged("ExploitationFields");
            }
        }

        public bool ModeFacturenormale
        {
            get { return modeFacturenormale; }
            set { modeFacturenormale = value;
            OnPropertyChanged("ModeFacturenormale");
            }
        }


        public bool ModeFactureAvoir
        {
            get { return modeFactureAvoir; }
            set { modeFactureAvoir = value;
            OnPropertyChanged("ModeFactureAvoir");
            }
        }

        public DroitModel CurrentDroit
        {
            get { return _currentDroit; }
            set { _currentDroit = value;
            OnPropertyChanged("CurrentDroit");
            }
        }


        public bool CheckValiderReadQty
        {
            get { return checkValiderReadQty; }
            set { checkValiderReadQty = value;
                 if (value )
            DetailProdEnable = true;
                 else DetailProdEnable = false ;

            OnPropertyChanged("CheckValiderReadQty");
            }
        }


        public bool VisibilityValiderReadQty
        {
            get { return visibilityValiderReadQty; }
            set { visibilityValiderReadQty = value;
            OnPropertyChanged("VisibilityValiderReadQty");
            }
        }

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
                //ActualFacture = fact;

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

        //destroyCommand

        public RelayCommand DestroyCommand
        {
            get
            {
                if (this.destroyCommand == null)
                {
                    this.destroyCommand = new RelayCommand(param => this.canDestroy(), param => canExecuteDestroy());
                }
                return this.destroyCommand;
            }

        }

        #endregion

        #region METHODS


        #region LOAD REGION

        #region LOAD AUTORISATION

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
                //

                if (droitFormulaire.Developpeur || droitFormulaire.Super || droitFormulaire.Ecriture || droitFormulaire.Proprietaire)
                {
                    IsVivisibleDestroyCommand = true;
                }
                if (droitFormulaire.Developpeur || droitFormulaire.Super || droitFormulaire.Ecriture || droitFormulaire.Proprietaire)
                {
                    // administrateur ou validateur
                    ChkDateValidate = false;
                    IsEnabledchkDateValidate = false;

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                    IsVivisibleDestroyCommand = false ;
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
                //if (UserConnected.Profile.ShortName.ToLower() == "admin" || UserConnected.Profile.ShortName.ToLower() == "sadmin")
                //{
                if (droitFormulaire.Developpeur || droitFormulaire.Super || droitFormulaire.Proprietaire)
                {// administrateur
                    ChkDateValidate = true;
                    IsEnabledchkDateValidate = true;
                    IsEnableOutValidation = true;
                    IsEnabledSuspendedValidation = true;


                }
                else if (droitFormulaire.Ecriture)
                {
                    // operateur principale
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
                   
                    ChkNonValable = false;
                    IsEnabledNonValable = false;
                    Enabledate = true;

                    AfficheStatut = "Encours Validation:";
                    FatureCurrent.DateCloture = FatureCurrent.DateCreation;

                    if (droitFormulaire.Developpeur ||
                        droitFormulaire.Super ||
                        droitFormulaire.Ecriture || droitFormulaire.Proprietaire)
                    {
                        IsEnabledchkDateValidate = true;
                        IsEnabledSuspendedValidation = true;
                    }
                    else
                    {
                        IsEnabledchkDateValidate = false;
                        IsEnabledSuspendedValidation = false;
                    }

                    operation = "validation";
                    isOperationpossible = false;

                }
                else if (int.Parse(currentStatut.CourtDesc) == 2)
                {
                    //if (FatureCurrent.DateCloture == null)
                    //{
                    loadautorisation();
                    //valeurDalidation = "validation";
                    AfficheStatut = "A Valider:";
                    IsValidateDate = false;
                    Enabledate = true;
                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;

                    BtnAddDetailEnable = true;
                    BtnDelDetailEnable = true;

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;

                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                    FatureCurrent.DateCloture = FatureCurrent.DateModif;


                    if (droitFormulaire.Developpeur ||
                       droitFormulaire.Super ||
                       droitFormulaire.Ecriture || droitFormulaire.Proprietaire)
                    {
                        IsEnabledchkDateValidate = true;
                        IsEnabledSuspendedValidation = true;
                    }

                    else
                    {
                        IsEnabledchkDateValidate = false;
                        IsEnabledSuspendedValidation = false;
                    }


                    operation = "validation";
                    isOperationpossible = false;


                }else
                if (int.Parse(currentStatut.CourtDesc) == 3)
                {

                    AfficheStatut = "Valider et cloturé:";
                    loadautorisation();
                    IsValidateDate = true;
                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;
                  
                  //  IsEnableOutValidation = true;
                  
                    isdatevalidationsexist = true;
                    ChkNonValable = false;

                        if (droitFormulaire.Developpeur ||
                           droitFormulaire.Super ||
                           droitFormulaire.Validation || droitFormulaire.Proprietaire)
                        {
                            BtnAddDetailEnable = true;
                            IsEnabledSuspendedValidation = true;
                            IsEnabledNonValable = true;
                            IsEnableOutValidation = true;

                        }
                        else
                        {
                            IsEnableOutValidation = false;
                            IsEnabledSuspendedValidation = false;
                            IsEnabledNonValable = false;
                        }

                    operation = "validation";
                    isOperationpossible = false;

                }
                else 
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
                    IsEnabledNonValable = false;
                   
                    ChkNonValable = false;
                
                    if (droitFormulaire.Developpeur ||
                       droitFormulaire.Validation || droitFormulaire.Proprietaire)
                        IsEnabledchkDateValidate = true;
                    else 
                        IsEnabledchkDateValidate = false;
                    //valeurDalidation = "devalide";
                    AfficheStatut = "Sortie:";
                    operation = "validation";
                    isOperationpossible = false;
                }
                else
                if (int.Parse(currentStatut.CourtDesc) == 5)
                {
                    //supendu
                    loadautorisation();
                    BtnAddDetailEnable = false;
                    BtnDelDetailEnable = false;

                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;

                    ChkNonValable = false;
                    IsEnabledNonValable = false;


                    IsEnabledNonValable = false;
                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;


                    //if (droitFormulaire.Developpeur ||
                    //   droitFormulaire.Super ||
                    //   droitFormulaire.Validation || droitFormulaire.Proprietaire)
                    //{

                    //}
                    //    IsEnabledchkDateValidate = true;
                    //else 
                        IsEnabledchkDateValidate = false;
                    //ChkDateSuspended = true;
                    valeurDalidation = "devalide";



                    operation = "validation";
                    isOperationpossible = false;
                }
                else
                if (int.Parse(currentStatut.CourtDesc) == 6)
                {
                    //non valable
                    loadautorisation();
                    BtnAddDetailEnable = false;
                    AfficheStatut = "Non Valable:";
                    //if (FatureCurrent.DateCloture != null)
                    //{
                    //    IsValidateDate = true;
                    //    AfficheStatut = "Devalider:";
                    //    isdatevalidationsexist = true;
                    //}
                    //else
                    //{
                    //    FatureCurrent.DateCloture = FatureCurrent.DateCreation;
                    //    AfficheStatut = "Encours Validation:";
                    //    isdatevalidationsexist = false;
                    //}

                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;

                    ChkNonValable = false;
                    IsEnabledNonValable = false;

                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;

                    //if (UserConnected.Profile.ShortName == "admin" || UserConnected.Profile.ShortName == "sadmin")
                    //if (droitFormulaire.Developpeur ||
                    //   droitFormulaire.Super ||
                    //   droitFormulaire.Validation || droitFormulaire.Proprietaire)
                    //    IsEnabledchkDateValidate = true;
                    //else 
                        IsEnabledchkDateValidate = false;
                    //ChkDateSuspended = true;
                    valeurDalidation = "devalider";

                    operation = "validation";
                    isOperationpossible = false;


                }
                else if (int.Parse(currentStatut.CourtDesc) == 7)
                {
                    // facture avoir
                    AfficheStatut = "Facture Avoir:";
                    FatureCurrent.DateCloture = FatureCurrent.DateCreation;
                    IsValidateDate = true;
                    operation = "validation";
                    valeurDalidation = "avoir";
                    isOperationpossible = false;
               
                    ChkDateOutValidate = false;
                    ChkDateSuspended = false;
                    IsEnableOutValidation = false;
                    IsEnabledSuspendedValidation = false;
                    isdatevalidationsexist = true;
                    ChkNonValable = false;
                    IsEnabledNonValable = false;
                    IsEnabledchkDateValidate = false;
                    BtnAddDetailEnable = true;
                }



            }

        }
        #endregion

        #region LOAD DATAS

        void LoadDateAudit()
        {
            if (FatureCurrent != null)
            {
                switch (FatureCurrent.IdStatut)
                {
                    case 14001:
                        {
                            DateAuditCreation = FatureCurrent.DateCreation;
                            DateAuditModification = FatureCurrent.DateModif;
                            DateAuditSortie = null;
                            DateAuditValidation = null;
                            break;
                        }

                    case 14002:
                        {
                            DateAuditCreation = FatureCurrent.DateCreation;
                            DateAuditModification = FatureCurrent.DateModif;
                            DateAuditSortie = null;
                            DateAuditValidation = FatureCurrent.DateCreation;
                            break;
                        }

                    case 14003:
                        {
                            DateAuditCreation = FatureCurrent.DateCreation;
                            DateAuditModification = FatureCurrent.DateModif;
                            DateAuditSortie = null;
                            DateAuditValidation = FatureCurrent.DateCloture ;
                            break;
                        }

                    case 14004:
                        {
                            DateAuditCreation = FatureCurrent.DateCreation;
                            DateAuditModification = FatureCurrent.DateModif;
                            DateAuditSortie = FatureCurrent.DateSortie ;
                            DateAuditValidation = FatureCurrent.DateCloture;
                            break;
                        }


                }
             
             
               
              
               

            }
            else
            {
                DateAuditCreation =null ;
                DateAuditModification = null ;
                DateAuditSortie = null;
                DateAuditValidation = null;
            }

        }
        void ConvertDataItems(List<LigneFactureModel> items)
        {
            double ligneht = 0;
            double ligneht_lign_exo = 0;
            string modeExoneration = string.Empty;
            try
            {

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
                    ligne.dateModif = newLine.DateModif;
                    ligne.IdExploitation = newLine.IdExploitation;
                    DetailProductModel detailproduit = NewDetailListeProduit.Find(dt => dt.IdDetail == newLine.IdDetailProduit);

                    if (newLine.Quantite.ToString().Contains(currentCulture.NumberFormat.NumberDecimalSeparator))
                    {

                        ligne.quantite = (decimal)newLine.Quantite;
                        if (detailproduit.Specialfact )
                        ligne.PrixUnit = (decimal)newLine.PrixUnitaire;
                    }
                    else
                    {
                        ligne.quantite = (decimal)newLine.Quantite;

                        if (newLine.CurrentDetailproduit.Prixunitaire == 1)
                            ligne.PrixUnit = (decimal)newLine.PrixUnitaire;
                        else
                            ligne.PrixUnit = (decimal)newLine.CurrentDetailproduit.Prixunitaire;
                    }

                    ligne.IdSite = newLine.IdSite;

                    ligne.Produit = newLine.CurrentProduit.Libelle;   //produitservice.Produit_SELECTBY_ID(newLine.IdProduit).Libelle;
                    ligne.montantHt = newLine.MontanHT;
                    ligne.estExonere = newLine.Exonere;
                    ligne.estprorata = newLine.CurrentDetailproduit.Isprorata;

                    if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2)
                        ligne.IsdeletedEnabled = true;
                    else
                        ligne.IsdeletedEnabled = false;

                 
                   
                    if (newLine.Quantite.ToString().Contains(currentCulture.NumberFormat.NumberDecimalSeparator))
                        ligne.quantite = newLine.Quantite;

                    liste.Add(ligne);
                }

                #region MONTAN FACTURE


                if (!string.IsNullOrEmpty(modeExoneration))
                {
                    object[] tabretour = null;
                   // LangueModel currentlanguage = langueservice.LANGUE_SELECTBYID(FatureCurrent.CurrentClient.IdLangue);
                    if (FatureCurrent.TotalHT == 0)
                    {
                        if (modeExoneration == "exo")
                        {
                            tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(items, (FatureCurrent.CurrentClient.Porata != null ? FatureCurrent.CurrentClient.Porata.Taux : "0%"),
                                FatureCurrent.CurrentTaxe.Taux, true, (FatureCurrent.CurrentClient.IdLangue == 1 ? "fr" : "en"), NewDetailListeProduit);

                        }
                        else
                            if (modeExoneration == "non")
                            {
                                tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(items, (FatureCurrent.CurrentClient.Porata != null ? FatureCurrent.CurrentClient.Porata.Taux : "0%"),
                                    FatureCurrent.CurrentTaxe.Taux, false, (FatureCurrent.CurrentClient.IdLangue == 1 ? "fr" : "en"), NewDetailListeProduit);

                            }
                           

                        if (tabretour != null)
                        {
                            Montanttva =tabretour[0].ToString() ;
                            Ftotaltva = double.Parse(tabretour[1].ToString());
                            MontantProrata = tabretour[2].ToString();
                            FTotalProrata = double.Parse(tabretour[3].ToString());
                            FTotalHt = double.Parse(tabretour[4].ToString());
                            
                            FtotalTTC = double.Parse(tabretour[5].ToString());
                        }
                    }
                    else
                    {
                        if (modeExoneration == "exo")
                        {
                            Ftotaltva =FatureCurrent.TotalTVA !=null? FatureCurrent.TotalTVA.Value:0;
                            Montanttva = string.Format("exonerate({0})", FatureCurrent.CurrentTaxe.Taux);
                        }
                        else
                        {
                            Ftotaltva =FatureCurrent.TotalTVA !=null? FatureCurrent.TotalTVA.Value:0;
                            Montanttva =FatureCurrent.CurrentTaxe !=null ? FatureCurrent.CurrentTaxe.Taux:"";

                            if (FatureCurrent.CurrentClient != null)
                                FTotalProrata = FatureCurrent.TotalPRORATA.HasValue == true ? FatureCurrent.TotalPRORATA.Value : 0;
                        }

                        
                     
                        MontantProrata = FatureCurrent.CurrentClient.Porata.Taux ;

                     
                        FTotalHt = FatureCurrent.TotalHT;
                        FtotalTTC = FatureCurrent.TotalTTC;
                       // double val =Math.Abs(FatureCurrent.TotalTTC);

                        if (FatureCurrent.TotalMarge.HasValue)
                        {
                            MontantMargeBenef = FatureCurrent.TotalMarge.Value;
                            if (FatureCurrent.CurrentMarge != null)
                                TauxMargeBenef = FatureCurrent.CurrentMarge.Taux;
                            else TauxMargeBenef = CostTaxeSelected.Taux;
                        }
                        
                        
                      

                    }


                }
                #endregion


                LigneCommandList = liste;
                CacheLigneCommandList = LigneCommandList;

            }
            catch (Exception ex)
            {
                throw new Exception("Erreure  <<10001>> , Problème survenu lors du chargement  des lignes de facture");
              
            }

        }

        void loadLanguageLientselejctInfo()
        {
            if (ClientSelected != null)
            {
                try
                {
                    this.IsBusy = true;
                    ObjetList = objetservice.OBJECT_FACTURE_GETLISTEByIdLanguage(ClientSelected.IdLangue, societeCourante.IdSociete);
                    ExploitationList = exploitationservice.EXPLOITATION_FACTURE_CLIENT_LIST(societeCourante.IdSociete, ClientSelected.IdClient);

                   // ProduiList = produitservice.Produit_SELECTBY_ID_Language(ClientSelected.IdLangue, societeCourante.IdSociete);
                   // CacheProduiList = ProduiList;
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
                        IsEnableCmbProduit = true ;
                        IsdetailExiste = true;

                        if (FatureCurrent !=null && FatureCurrent.IdDepartement > 0)
                          Produitindex = ProduiList.ToList().FindIndex(p => p.IdProduit == FatureCurrent.IdDepartement);
                    }
                    else
                    {
                        MessageBox.Show("Ce Client ne Posssède pad de produits rattachés", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        IsEnableCmbProduit = false  ;
                        IsdetailExiste = false ;
                    }
                    DeviseConvert = deviseService.Devise_SELECTById(ClientSelected.IdDeviseFact, societeCourante.IdSociete);
                    this.IsBusy = false;

                    Utils.logUserActions("<-- interface edition facture --Chargement produits dun client   ", "");
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message ="Erreure <<10002> Impossible de Charger les données entête Facture";
                    view.ShowDialog();
                    this.MouseCursor = null;
                    this.IsBusy = false;
                    Utils.logUserActions("<-- interface edition facture --Erreure chargement produits de   "+ex.Message , "");
                }
            }
        }

        void loadDataClientselected()
        {
            try
            {
                this.IsBusy = true;
               // if (ObjetList==null )
              

                ObjetList = objetservice.OBJECT_FACTURE_GETLISTEByCLIENT(societeCourante.IdSociete, ClientSelected.IdClient);

                ExploitationFactureModel newexploitation = new ExploitationFactureModel { IdExploitation=0, Libelle="Aucune", IdSite=societeCourante.IdSociete };
                ExploitationList = exploitationservice.EXPLOITATION_FACTURE_CLIENT_LIST(societeCourante.IdSociete, ClientSelected.IdClient);
                ExploitationList.Add(newexploitation);
              
                
                    NewDetailListeProduit = detailService.DETAIL_PRODUIT_BYCLIENT(ClientSelected.IdClient, societeCourante.IdSociete);
                // produit sans exploitations
                  var newNewDetailProduitlist=  NewDetailListeProduit.FindAll(np => np.IdExploitation == 0);
                    cachnewDetailListeProduit = new List<DetailProductModel>();
                    foreach (var lst in newNewDetailProduitlist)
                        cachnewDetailListeProduit.Add(lst);
                    ProduiList = null;
                    CacheProduiList = null;

                    IdProduits = new HashSet<int>();
                    if (NewDetailListeProduit != null && NewDetailListeProduit.Count > 0)
                    {
                        if (newNewDetailProduitlist != null && newNewDetailProduitlist.Count > 0)
                        {
                            // elimine doublons
                            foreach (DetailProductModel det in newNewDetailProduitlist)
                                IdProduits.Add(det.IdProduit);

                            //nouvelle liste des produits sans doublons
                            ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
                            foreach (int id in IdProduits)
                                newListeProduie.Add(newNewDetailProduitlist.Find(p => p.IdProduit == id).Produit);


                            ProduiList = newListeProduie;
                            IndexProduit = -1;
                            CacheProduiList = ProduiList;
                            IsEnableCmbProduit = true;
                            IsdetailExiste = true;
                        }
                        else
                        {
                            //pad de produit sans exploitation
                            IsEnableCmbProduit = false   ;
                        }
                    }
                    else
                    {
                        MessageBox.Show("Ce Client ne Posssède pad de produits rattachés", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                        IsEnableCmbProduit = false;
                        IsdetailExiste = false;
                    }

              

                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message = ex.Message;
                view.ShowDialog();
                this.MouseCursor = null;
                this.IsBusy = false;
            }
        }

        // nouveau client selectionner 
        void loadDatasnewFacture()
        {
            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, args) =>
          {
              try
              {
                  this.IsBusy = true;
                  ProgressBarVisibility = true;
                  if (onloading)
                  {
                      TaxeList = taxeService.Taxe_SELECTByRef("tva", societeCourante.IdSociete);
                 
                 // DeviseModel devise = deviseService.Devise_SELECT(societeCourante.IdSociete).Find(d => d.IsDefault == true);
                 // DeviseSelected = devise;
                  
                     
                      DepartementList = depService.Departement_SELECT(societeCourante.IdSociete);
                  }

                 // if (DeviseSelected ==null )
                 // DeviseSelected = deviseService.Devise_SELECT(societeCourante.IdSociete).Find(d => d.IsDefault == true);
              

                  ClientList = clientservice.CLIENT_GETLISTE(idSocieteCourant, true);
                  CostTaxeList = taxeService.Taxe_SELECTByRef("marge", societeCourante.IdSociete);

                  CurrentStatut = StatutFacture.First(f => f.CourtDesc == "1");
                 

                  this.IsBusy = false;

                  //Utils.logUserActions("<-- interface edition facture --Chargement données  de reférences dun client ", "");
              }
              catch (Exception ex)
              {
                  args.Result =string.Format("{0} ; {1} ", ex.Message , ex.InnerException );

                 
              }

          };

            worker.RunWorkerCompleted += (o, args) =>
              {

                  if (args.Result != null)
                  {
                      CustomExceptionView view = new CustomExceptionView();
                      view.Owner = Application.Current.MainWindow;
                      view.Title = args.Result.ToString();
                      view.ViewModel.Message = "ERREUR CHARGEMENT LIST";
                      view.ShowDialog();
                      this.MouseCursor = null;
                      this.IsBusy = false;
                      ProgressBarVisibility = false;
                      Utils.logUserActions("<-- UI Edition Erreur lors du chargement    " + args.Result.ToString(), "");
                  }
                  else
                  {
                      if (FatureCurrent != null && FatureCurrent.IdClient > 0)
                          ClientIndex = ClientList.ToList().FindIndex(c => c.IdClient == FatureCurrent.IdClient);
                      if (TaxeList != null)
                          IndexeTaxe = TaxeList.FindIndex(t => t.TaxeDefault == true);
                      else
                      {
                          TaxeList = taxeService.Taxe_SELECTByRef("tva", societeCourante.IdSociete);
                          IndexeTaxe = TaxeList.FindIndex(t => t.TaxeDefault == true);
                      }

                      onloading = false;
                      //juste garde li du produit ds id departement
                      //if (FatureCurrent !=null && FatureCurrent.IdDepartement > 0)
                        //  Produitindex = ProduiList.ToList().FindIndex(p => p.IdProduit == FatureCurrent.IdDepartement);
                      this.IsBusy = false;
                      ProgressBarVisibility = false;
                  }
              };

            worker.RunWorkerAsync();

          
        }

        // rechargement 

        void loadingCurrenttaxes()
        {
            try
            {
                if (FatureCurrent != null)
                {

                    if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004 || GlobalDatas.IsArchiveSelected)
                    {
                        if (!isReloadFacture)
                        {
                            TaxeList = taxeService.Taxes_SELECTARCHIVE(0, societeCourante.IdSociete).FindAll(t => t.Libelle.ToLower().Contains("tva"));
                            CostTaxeList = taxeService.Taxe_SELECTByRef("marge", societeCourante.IdSociete);
                            if (FatureCurrent.MaregeBeneficiaireId.HasValue)
                                if (FatureCurrent.MaregeBeneficiaireId > 0)
                                    IndexeCostTaxe = CostTaxeList.FindIndex(t => t.ID_Taxe == FatureCurrent.MaregeBeneficiaireId.Value);
                                else IndexeCostTaxe = -1;
                           // CostTaxeSelected
                            // DeviseModel devise = deviseService.Devise_Archive_SELECT(societeCourante.IdSociete).Find(d => d.ID_Devise == FatureCurrent.IdDevise);
                            IndexeTaxe = TaxeList.FindIndex(t => t.ID_Taxe == FatureCurrent.IdTaxe);
                            // DeviseSelected = devise;
                        }
                    }
                    else
                    {
                        if (!isReloadFacture)
                        {
                            TaxeList = taxeService.Taxe_SELECTByRef("tva", societeCourante.IdSociete);
                            CostTaxeList = taxeService.Taxe_SELECTByRef("marge", societeCourante.IdSociete);
                            if (FatureCurrent.MaregeBeneficiaireId > 0)
                                IndexeCostTaxe = CostTaxeList.FindIndex(t => t.ID_Taxe == FatureCurrent.MaregeBeneficiaireId.Value);
                            else IndexeCostTaxe = -1;

                          
                            //  DeviseModel devise = deviseService.Devise_SELECT(societeCourante.IdSociete).Find(d => d.IsDefault == true);
                            //  DeviseSelected = devise;
                            IndexeTaxe = TaxeList.FindIndex(t => t.TaxeDefault == true);
                        }
                    }
                }

            }
            catch (Exception ex)
            {
              //  MessageBox.Show("Probleme de chargement Liste Taxes ! ");
                throw new Exception(ex.Message);
                Utils.logUserActions(string.Format("<-- UI edition facture --> Erreur de chargement Liste taxes par défault {0}  par : {1}", ex.Message, UserConnected.Loggin), "");
            }
        }

        void LoadDefaultTaxes()
        {
            try
            {
                //TaxeList = taxeService.Taxe_SELECTByRef("tva",societeCourante.IdSociete);
                //CostTaxeList = taxeService.Taxe_SELECTByRef("marge", societeCourante.IdSociete);
                //DeviseModel devise = deviseService.Devise_SELECT(societeCourante.IdSociete).Find(d=>d.IsDefault==true);
                //DeviseSelected = devise;

                //if (CacheDatas.deviseDefault == null)
                //{
                //    if (deviseService != null && ParametersDatabase != null)
                //    {
                //        DeviseSelected = deviseService.Devise_SELECTById(ParametersDatabase.IdDevise, societeCourante.IdSociete);
                //        CacheDatas.deviseDefault = DeviseSelected;
                //    }
                //}
                //else DeviseSelected = CacheDatas.deviseDefault;

               // IndexeTaxe = TaxeList.FindIndex(t => t.TaxeDefault==true);
              
            }
            catch (Exception ex)
            {
                MessageBox.Show("Probleme de chargement Liste Taxes ! ");
                //return;
                Utils.logUserActions(string.Format("<-- UI edition facture --> Erreur de chargement Liste taxes par défault {0}  par : {1}", ex.Message, UserConnected.Loggin), "");
            }
        }


        void newloadinfdatarefs()
        {


            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += (o, args) =>
            {
                try
                {
                    ProgressBarVisibility = true;
                    IsBusy = true;

                    if (  GlobalDatas.IsArchiveSelected)
                    {
                        LoadClientValideARchive();
                    }
                    else
                    {
                        LoadClient();
                        LoadObjet();
                        LoadExploitation();
                        LoadDepartement();

                        LoadProduit(FatureCurrent.IdClient);
                        LoadCLignefactures();

                    }

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
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "ERREUR CHARGEMENT DONNES DE REFERENCE";
                    view.ViewModel.Message = args.Result.ToString();
                    view.ShowDialog();

                    ProgressBarVisibility = false;
                    IsBusy = false;
                    Communicator com = new Communicator();
                    EventArgs e2 = new EventArgs();
                    com.OnChangeCloseWindow(e2);

                    Utils.logUserActions("<-- UI Edition Erreur lors du chargement données references    " + args.Result.ToString(), "");
                }
                else
                {


                    //loadautorisation();
                    //loadOperationValidation();

                    //if (!isReloadFacture)
                    //{
                    //    Departementindex = DepartementList.FindIndex(d => d.IdDep == FatureCurrent.IdDepartement);

                    //    ClientIndex = ClientList.ToList().FindIndex(cl => cl.IdClient == FatureCurrent.IdClient);

                    //    ObjetIndex = ObjetList.ToList().FindIndex(od => od.IdObjet == FatureCurrent.IdObjetFacture);

                    //    ExploitationIndex = ExploitationList.ToList().FindIndex(ex => ex.IdExploitation == FatureCurrent.IdExploitation);

                    //    if (FatureCurrent.MaregeBeneficiaireId != null)
                    //        IndexeCostTaxe = CostTaxeList.FindIndex(t => t.ID_Taxe == FatureCurrent.MaregeBeneficiaireId);
                    //    IndexProduit = -1;

                   // }


                    try
                    {
                        loadingCurrenttaxes();

                        Loadversion();

                        if (CacheDatas.ui_Statut == null)
                        {
                            StatutFacture = statutservice.STATUT_FACTURE_GETLISTE();
                            CacheDatas.ui_Statut = StatutFacture;
                        }
                        else StatutFacture = CacheDatas.ui_Statut;


                        if (  GlobalDatas.IsArchiveSelected)
                        {
                            LoadObjetValicdArchive();
                            LoaExploitationArchive();
                            LoadDepartementArchives();

                       
                            LoadProduitArchives();
                            //LoadDeviseClientArchive();
                            LoadLignefactureArchivevalidate();
                        }
                        else
                        {
                           


                        }

                        InitIndexValues();
                    }
                    catch (Exception ex)
                    {
                        CustomExceptionView view = new CustomExceptionView();
                        view.Owner = Application.Current.MainWindow;
                        view.Title = "ERREUR CHARGEMENT DONNES DE REFERENCE";
                        view.ViewModel.Message = ex.Message;
                        view.ShowDialog();
                        Communicator com = new Communicator();
                        EventArgs e2 = new EventArgs();
                        com.OnChangeCloseWindow(e2);
                    }

                    ProgressBarVisibility = false;
                    IsBusy = false;
                    isloading = false;
                }
            };

            worker.RunWorkerAsync();
        }

        void InitIndexValues()
        {
              loadautorisation();
                    loadOperationValidation();

                    if (!isReloadFacture)
                    {
                        Departementindex = DepartementList.FindIndex(d => d.IdDep == FatureCurrent.IdDepartement);

                        ClientIndex = ClientList.ToList().FindIndex(cl => cl.IdClient == FatureCurrent.IdClient);

                        ObjetIndex = ObjetList.ToList().FindIndex(od => od.IdObjet == FatureCurrent.IdObjetFacture);

                        ExploitationIndex = ExploitationList.ToList().FindIndex(ex => ex.IdExploitation == FatureCurrent.IdExploitation);

                        if (FatureCurrent.MaregeBeneficiaireId != null)
                            IndexeCostTaxe = CostTaxeList.FindIndex(t => t.ID_Taxe == FatureCurrent.MaregeBeneficiaireId);
                        IndexProduit = -1;
                    }

                    factureOnLoad = false;
        }
    


      void loadDatasCurrentfact()
        {
            // default data configurate
           
            if (FatureCurrent != null)
            {
                if (FatureCurrent.IdClient >0)
                {
                  

                    #region Devise

                 
                    //try
                    //{

                    //    if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004 || GlobalDatas.IsArchiveSelected)
                    //        {
                    //            DeviseSelected = FatureCurrent.CurrentDevise ?? deviseService.Devise_ArchiveSELECTById(ParametersDatabase.IdDevise, FatureCurrent.IdSite);
                    //        }
                    //        else
                    //        {
                    //            if (CacheDatas.deviseDefault == null)
                    //            {
                    //                if (!isReloadFacture)
                    //                {
                    //                    if (deviseService != null && ParametersDatabase != null)
                    //                    {
                    //                        DeviseSelected = FatureCurrent.CurrentDevise ?? deviseService.Devise_SELECTById(ParametersDatabase.IdDevise, FatureCurrent.IdSite);

                    //                        CacheDatas.deviseDefault = DeviseSelected;
                    //                    }
                    //                }
                    //            }
                    //            else
                    //            {
                    //                DeviseSelected = CacheDatas.deviseDefault;
                    //            }

                    //        }
                        
                       
                       


                    //}
                    //catch (Exception ex)
                    //{
                    //    MessageBox.Show("Probleme de chargement de la dévise par défaut ! " + deviseService);
                    //    //return;
                    //    Utils.logUserActions(string.Format("<-- UI edition facture --> Erreur de chargement dévise par défault {0}  par : {1}", ex.Message , UserConnected.Loggin), "");
                    //}

                    #endregion

                    #region Version

                  
                    try
                    {
                        //versionFactureCurrent = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show("Probleme de chargement version facture  courante " + ex.Message);

                    }
                    #endregion

                   
                    //this.IsBusy = true;
                    //ProgressBarVisibility = true;

                    #region DefaulTaxe

                    
                    //try
                    //    {
                    //        if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004)
                    //        {
                    //            TaxeSelected = FatureCurrent.CurrentTaxe ?? taxeService.Taxes_SELECTByIdArchive(ParametersDatabase.Idtva, FatureCurrent.IdSite);
                    //        }
                    //        else
                    //        {
                    //            if (CacheDatas.taxeDefault == null)
                    //            {
                    //                if (!isReloadFacture)
                    //                {
                    //                    if (taxeService != null && ParametersDatabase != null)
                    //                    {
                    //                        TaxeSelected = FatureCurrent.CurrentTaxe ?? taxeService.Taxe_SELECTById(ParametersDatabase.Idtva, FatureCurrent.IdSite);
                    //                        CacheDatas.taxeDefault = TaxeSelected;
                    //                    }
                    //                }
                    //            }
                    //            else TaxeSelected = CacheDatas.taxeDefault;
                    //        }
                    //    }
                    //    catch (Exception ex)
                    //    {
                    //        CustomExceptionView view = new CustomExceptionView();
                    //        // view.Owner = Application.Current.MainWindow;
                    //        view.ViewModel.Message = "Erreure <<100190>> erreure survenue lors du chargement des taxes ";
                    //        view.ShowDialog();
                    //        return;
                    //    }


                    #endregion

                    #region ClientList

                    try
                        {
                            if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004 || GlobalDatas.IsArchiveSelected)
                            {
                                if (issaveUpdatedata)
                                    if (!isReloadFacture)
                                ClientList = clientservice.CLIENT_Archive_GETLISTE(FatureCurrent.IdSite, true);
                            }
                            else
                            {
                                if (issaveUpdatedata)
                                    if (!isReloadFacture)
                                        ClientList = clientservice.CLIENT_GETLISTE(FatureCurrent.IdSite, true);
                            }
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            // view.Owner = Application.Current.MainWindow;
                            view.ViewModel.Message = "Erreure <<101190>> erreure survenue lors du chargement des  des clients ";
                            view.ShowDialog();
                            return;
                        }

                    #endregion

                    #region ObjetList

                   

                    try
                    {
                        if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004 || GlobalDatas.IsArchiveSelected)
                        {

                            ObjetList = objetservice.OBJECT_FACTURE_GETLISTEByCLIENT_Archive(FatureCurrent.IdSite, FatureCurrent.IdClient); ;
                                
                        }
                        {
                            if (!isReloadFacture)
                                ObjetList = objetservice.OBJECT_FACTURE_GETLISTEByCLIENT(FatureCurrent.IdSite, FatureCurrent.IdClient);
                        }
                    }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            // view.Owner = Application.Current.MainWindow;
                            view.ViewModel.Message = "Erreure <<102190>> erreure survenue lors du chargement des objets du client ";
                            view.ShowDialog();
                            return;
                        }
                    #endregion

                    #region Exploitation List

                   
                    try
                        {

                            //if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004 || GlobalDatas.IsArchiveSelected)
                            //{
                            //    ExploitationFactureModel newexploitation = new ExploitationFactureModel { IdExploitation = 0, Libelle = "Aucune", IdSite = societeCourante.IdSociete };
                            //    ExploitationList = exploitationservice.EXPLOITATION_FACTURE_CLIENT_LIST_Archive(FatureCurrent.IdSite, FatureCurrent.IdClient);
                            //    ExploitationList.Add(newexploitation);
                            //}
                            //else
                            //{
                            //    if (!isReloadFacture)
                            //    {
                            //        ExploitationFactureModel newexploitation = new ExploitationFactureModel { IdExploitation = 0, Libelle = "Aucune", IdSite = societeCourante.IdSociete };
                            //        ExploitationList = exploitationservice.EXPLOITATION_FACTURE_CLIENT_LIST(FatureCurrent.IdSite, FatureCurrent.IdClient);
                            //        ExploitationList.Add(newexploitation);

                            //    }
                            //}
                            
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            // view.Owner = Application.Current.MainWindow;
                            view.ViewModel.Message = "Erreure <<103190>> erreure survenue lors du chargement des des exploitations du client ";
                            view.ShowDialog();
                            return;
                        }
                    #endregion

                    #region Produi te detail list

                 
                    try
                        {
                            if (!isReloadFacture)
                            {
                                if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004 || GlobalDatas.IsArchiveSelected)
                                {
                                    NewDetailListeProduit = detailService.DETAIL_PRODUIT_BYCLIENT_Archive(FatureCurrent.IdClient, FatureCurrent.IdSite);
                                    if (NewDetailListeProduit != null && NewDetailListeProduit.Count > 0)
                                    {
                                        IdProduits = new HashSet<int>();
                                        CachnewDetailListeProduit = new List<DetailProductModel>();
                                        foreach (var lst in NewDetailListeProduit)
                                            CachnewDetailListeProduit.Add(lst);

                                        foreach (DetailProductModel det in NewDetailListeProduit)
                                            IdProduits.Add(det.IdProduit);

                                        ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
                                        foreach (int id in IdProduits)
                                            newListeProduie.Add(NewDetailListeProduit.Find(p => p.IdProduit == id).Produit);
                                        ProduiList = newListeProduie;
                                        CacheProduiList = ProduiList;

                                        if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2)
                                        {
                                            DetailProdEnable = true;
                                            IsEnableCmbProduit = true;
                                            IsdetailExiste = true;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ce Client ne Posssède Plus de produits rattachés, merci de le faire d'abord", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                                else
                                {


                                    NewDetailListeProduit = detailService.DETAIL_PRODUIT_BYCLIENT(FatureCurrent.IdClient, FatureCurrent.IdSite);
                                    if (NewDetailListeProduit != null && NewDetailListeProduit.Count > 0)
                                    {
                                        IdProduits = new HashSet<int>();
                                        CachnewDetailListeProduit = new List<DetailProductModel>();
                                        foreach (var lst in NewDetailListeProduit)
                                            CachnewDetailListeProduit.Add(lst);

                                        foreach (DetailProductModel det in NewDetailListeProduit)
                                            IdProduits.Add(det.IdProduit);

                                        ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
                                        foreach (int id in IdProduits)
                                            newListeProduie.Add(NewDetailListeProduit.Find(p => p.IdProduit == id).Produit);
                                        ProduiList = newListeProduie;
                                        CacheProduiList = ProduiList;

                                        if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2)
                                        {
                                            DetailProdEnable = true;
                                            IsEnableCmbProduit = true;
                                            IsdetailExiste = true;
                                        }
                                    }
                                    else
                                    {
                                        MessageBox.Show("Ce Client ne Posssède Plus de produits rattachés, merci de le faire d'abord", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    }
                                }
                            }
                           
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            // view.Owner = Application.Current.MainWindow;
                            view.ViewModel.Message = "Erreure <<104190>> erreure survenue lors du chargement des produits ";
                            view.ShowDialog();
                            return;
                        }
                    #endregion

                    #region Departement liste

                  
                       try
                        {
                            if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004 || GlobalDatas.IsArchiveSelected)
                            {
                               
                            }
                            else
                            {
                                if (!isReloadFacture)
                                    DepartementList = depService.Departement_SELECT(societeCourante.IdSociete);
                            }
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            // view.Owner = Application.Current.MainWindow;
                            view.ViewModel.Message = "Erreure <<105190>> erreure survenue lors du chargement des départements ";
                            view.ShowDialog();
                            return;
                        }

                    #endregion

                    #region Devise coversion

                   
                    try
                        {
                            if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004 || GlobalDatas.IsArchiveSelected)
                            {
                                if (!isReloadFacture)
                                {
                                    if (FatureCurrent.CurrentClient.DeviseConversion == null)
                                        DeviseConvert = deviseService.Devise_ArchiveSELECTById(FatureCurrent.CurrentClient.IdDeviseConversion, societeCourante.IdSociete);
                                    else DeviseConvert = FatureCurrent.CurrentClient.DeviseConversion;
                                }
                            }
                            else
                            {
                                if (!isReloadFacture)
                                {
                                    if (FatureCurrent.CurrentClient.DeviseConversion == null)
                                        DeviseConvert = deviseService.Devise_SELECTById(FatureCurrent.CurrentClient.IdDeviseConversion, societeCourante.IdSociete);
                                    else DeviseConvert = FatureCurrent.CurrentClient.DeviseConversion;
                                }
                            }
                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            // view.Owner = Application.Current.MainWindow;
                            view.ViewModel.Message = "Erreure <<100180>> erreure survenue lors du chargement des devises ";
                            view.ShowDialog();
                            return;
                        }

                    #endregion

                    #region Ligne Factures List

                  
                    try
                        {

                            if (FatureCurrent != null && FatureCurrent.CurrentClient != null)
                            {
                                if (GlobalDatas.IsArchiveSelected)
                                {
                                    LigneFatureListe = ligneFactureService.LIGNE_FACTURE_BYIDFActure_Archive(FatureCurrent.IdFacture);
                                }
                                else
                                {
                                    if (FatureCurrent.IdStatut == 14003 || FatureCurrent.IdStatut == 14004)
                                    {
                                        LigneFatureListe = ligneFactureService.LIGNE_FACTURE_BYIDFActure_Validate(FatureCurrent.IdFacture);
                                    }
                                    else
                                    {
                                        //var result = ligneFactureService.LIGNE_FACTURE_BYIDFActure(FatureCurrent.IdFacture);
                                        LigneFatureListe = ligneFactureService.LIGNE_FACTURE_BYIDFActure(FatureCurrent.IdFacture);
                                    }
                                }
                               
                            }

                            


                        }
                        catch (Exception ex)
                        {
                            CustomExceptionView view = new CustomExceptionView();
                            // view.Owner = Application.Current.MainWindow;
                            view.ViewModel.Message = "Erreure <<107190>> erreure survenue lors du chargement des collections ";
                            view.ShowDialog();
                        }
                      #endregion

                         loadautorisation();
                            loadOperationValidation();
                    if (!isReloadFacture)
                    {
                        Departementindex = DepartementList.FindIndex(d => d.IdDep == FatureCurrent.IdDepartement);

                        clientIndex = ClientList.ToList().FindIndex(cl => cl.IdClient == FatureCurrent.IdClient);

                        ObjetIndex = ObjetList.ToList().FindIndex(o => o.IdObjet == FatureCurrent.IdObjetFacture);

                        ExploitationIndex = ExploitationList.ToList().FindIndex(ex => ex.IdExploitation == FatureCurrent.IdExploitation);

                        if (FatureCurrent.MaregeBeneficiaireId != null)
                            IndexeCostTaxe = CostTaxeList.FindIndex(t => t.ID_Taxe == FatureCurrent.MaregeBeneficiaireId);
                    }


                   
                }
                else
                {
                    CustomExceptionView view = new CustomExceptionView();
                   // view.Owner = Application.Current.MainWindow;
                    view.ViewModel.Message = "Erreure <<100090>> Impossible de traiter cette facturre son client n'a pas été Chargé \n merci de reselectionné ";
                    view.ShowDialog();
                }
            }
            else
            {
                CustomExceptionView view = new CustomExceptionView();
               // view.Owner = Application.Current.MainWindow;
                view.ViewModel.Message = "Erreure <<1000010>> Impossible de traiter cette facture, pas de onnées retrouvées \n";
                view.ShowDialog();

               
            }

        }

        void loadDataBaseData()
        {
           
                try
                {
                    ParametersDatabase = GlobalDatas.dataBasparameter;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                          
                            view.ViewModel.Message ="Problème <<2001>> survenu lors du chargement des parametres de bases";
                            view.ShowDialog();
                            this.MouseCursor = null;
                            this.IsBusy = false;
                            Utils.logUserActions("UI edition facture --> erreur <2001> lors du chargement parametres de base: " + ex.Message + "" + ex.InnerException.ToString(), "");
                        
                }



        }


        void loadFactureInformation()
        {

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
                // view.Owner = Application.Current.MainWindow;
                view.Title = "INFORMATION ERREURE CHARGEMENT LIGNES FACTURES";
                view.ViewModel.Message = "Erreur <10005> lors du chargement des lignes de factures";
                view.ShowDialog();
                Utils.logUserActions("UI edition facture --> erreur <10005> lors du chargemnt lignes factures: " + ex.Message + "" + ex.InnerException.ToString(), "");
            }
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

        #endregion

        #region OPERATION REGION

        private void canShowTaxe()
        {
            

        }

        #region LIGNE FACTURE AJOUT

        double totaligne_ht_tva = 0;
        double totalihne_ht_prorata = 0;

        bool canExecuteAjout()
        {
            bool values = false;
            if (droitFormulaire.Developpeur || droitFormulaire.Ecriture  || droitFormulaire.Edition)
            {
                if (LigneFacture != null)
                {
                    if (IsdetailExiste)
                    {
                        if (FatureCurrent.CurrentStatut != null)
                        {
                            if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2||  int.Parse (FatureCurrent.CurrentStatut.CourtDesc)==7 )
                            {
                                if (PrixUnitaireselected > 0)
                                    values = true;
                                else values = false;
                            }
                            else values = false;
                        }
                        else
                            values = true;
                    }


                }
                else
                    values = false;
                if (LigneCommandList != null && LigneCommandList.Count == 0)
                {
                    ModeFactureAvoirEnable = true;
                    ModeFacturenormaleEnable = true;
                }
            }

            if (GlobalDatas.IsArchiveSelected)
                values = false;
            return values;//LigneFacture != null ? (IsdetailExiste ? ((int.Parse (FatureCurrent.CurrentStatut.CourtDesc)<2)?true :false ) : false) : false;
        }

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
                                try
                                {

                                    #region NEW ITEM

                                    Lignecourante.ID = 0;
                                    Lignecourante.Description = LigneFacture.Description ?? string.Empty;
                                    Lignecourante.Produit = ProduitSelected.Libelle;
                                    Lignecourante.IdProduit = ProduitSelected.IdProduit;
                                    Lignecourante.Idetail = idDetailPode;
                                    Lignecourante.SpecialMode = IsModeFacturation;
                                       
                                    if (!isDaysCalcul)
                                        Lignecourante.PrixUnit = PrixUnitaireselected;
                                    else Lignecourante.PrixUnit = newPrixUnit;

                                    Lignecourante.estExonere = IsItemsExonere;
                                    Lignecourante.estprorata = Isproratable;

                                    if (!isDaysCalcul)
                                        Lignecourante.quantite = (decimal)LigneFacture.Quantite;
                                    else Lignecourante.quantite = (decimal)newNewQte;

                                    Lignecourante.tva = ""; //taxeService.Taxe_SELECTById(ParametersDatabase.Idtva).Taux;
                                    Lignecourante.situation = 1;
                                    Lignecourante.IsdeletedEnabled = false;
                                    Lignecourante.IdSite = societeCourante.IdSociete;
                                   // NewDetailListeProduit.FindAll(p => p.IdExploitation == value.IdExploitation)
                                    int idexploit = ExploitationSelected != null ? ExploitationSelected.IdExploitation : 0;
                                    var listExp = NewDetailListeProduit.FindAll(p => p.IdExploitation == idexploit);
                                    if (listExp.Count>0)
                                        Lignecourante.IdExploitation = idexploit;
                                    else Lignecourante.IdExploitation = 0;

                                    if (CacheLigneCommandList.Count == 0)
                                        Lignecourante.IdExploitation = idexploit;
                                    else
                                    {
                                        if (CacheLigneCommandList.Exists(ex => ex.IdExploitation == idexploit))
                                                Lignecourante.IdExploitation = idexploit;
                                        else
                                        {
                                            if (listExp.Count == 0)
                                                Lignecourante.IdExploitation = CacheLigneCommandList[0].IdExploitation;
                                            else Lignecourante.IdExploitation = idexploit;
                                        }
                                    }

                                    double valprorata = 0;
                                    double valTva = 0;
                                    double result = 0;

                                    if (ModeFactureAvoir)
                                    {
                                        Lignecourante.quantite = -Lignecourante.quantite;
                                        if (!isDaysCalcul)
                                            Lignecourante.montantHt = Lignecourante.quantite * PrixUnitaireselected;
                                        else
                                            Lignecourante.montantHt = newPrixUnit;

                                        #region Montant

                                        FTotalHt = (double)CacheLigneCommandList.Sum(l => l.montantHt);
                                        FTotalHt += (double)Lignecourante.montantHt;

                                        if (DeviseSelected.Taux.Equals("0"))
                                            FTotalHt = Math.Round(FTotalHt,0);

                                        if (clientCourant.Exonerere != null)
                                        {

                                         

                                            if (clientCourant.Exonerere.CourtDesc.Equals("non"))
                                            {
                                                #region Non Exonere
                                                // valTva = GetTaxeFr(TaxeSelected.Taux);
                                                double centimes;

                                                valTva = GetTaxeFr(TaxeSelected.Taux, currentCulture);
                                                Lignecourante.LibelleDetail = "";

                                                if (DeviseSelected.Taux.Equals("0"))
                                                    Ftotaltva = Math.Round((valTva * FTotalHt), 0);
                                                else
                                                  Ftotaltva = Math.Round((valTva * FTotalHt), 2);

                                                //centimes
                                                MontantProrata = TaxePorataSelected.Taux;
                                                valprorata = GetTaxeFr(TaxePorataSelected.Taux, currentCulture);
                                                centimes = valprorata * Ftotaltva;
                                                if (DeviseSelected.Taux.Equals("0"))
                                                FTotalProrata = Math.Round(centimes, 0);
                                                else
                                                    FTotalProrata = Math.Round(centimes, 2);

                                                Montanttva = TaxeSelected.Taux;
                                                FtotalTTC = (Ftotaltva + FTotalHt + FTotalProrata);

                                            #endregion
                                            }
                                            else if (clientCourant.Exonerere.CourtDesc.Equals("exo"))
                                            {
                                                #region Exonere


                                                Montanttva = string.Format("({0}) {1}", (clientCourant.IdLangue == 1 ? "Exonere" : "Exonerate"), TaxeSelected.Taux);
                                                Ftotaltva += 0;
                                                double valp = 0;
                                                double calcul;


                                                // si prorata
                                                if (TaxePorataSelected.ID_Taxe > 0)
                                                {
                                                    MontantProrata = TaxePorataSelected.Taux;
                                                   // valprorata = GetTaxeFr(TaxePorataSelected.Taux, currentCulture);
                                                    //calcul = valprorata * FTotalHt;
                                                    //calcul = Math.Round(calcul, 0);
                                                    

                                                }
                                                else
                                                {
                                                    MontantProrata = "";
                                                    valprorata = 0;
                                                    calcul = 0;
                                                }

                                                Lignecourante.LibelleDetail = "";
                                               

                                                FtotalTTC =FTotalHt;




                                                #endregion
                                            }
                                           
                                        }

                                        #endregion
                                    }
                                    else
                                    {
                                        if (!isDaysCalcul)
                                            Lignecourante.montantHt = Lignecourante.quantite * PrixUnitaireselected;
                                        else
                                            Lignecourante.montantHt = newPrixUnit;

                                        #region Montant

                                        FTotalHt = (double)CacheLigneCommandList.Sum(l => l.montantHt);
                                        FTotalHt += (double)Lignecourante.montantHt;

                                        if (DeviseSelected.Taux.Equals("0"))
                                            FTotalHt = Math.Round(FTotalHt, 0);

                                        if (clientCourant.Exonerere != null)
                                        {
                                          

                                            if (clientCourant.Exonerere.CourtDesc.Equals("non"))
                                            {
                                              #region Non Exonere
                                                // valTva = GetTaxeFr(TaxeSelected.Taux);

                                                valTva = GetTaxeFr(TaxeSelected.Taux, currentCulture);
                                                Lignecourante.LibelleDetail = "";
                                              //  Ftotaltva = Math.Round((valTva * FTotalHt), 2);

                                                if (DeviseSelected.Taux.Equals("0"))
                                                    Ftotaltva = Math.Round((valTva * FTotalHt), 0);
                                                else
                                                    Ftotaltva = Math.Round((valTva * FTotalHt), 2);

                                                // calcule centimes
                                                var centime = GetTaxeFr(TaxePorataSelected.Taux, currentCulture) ;
                                                if (DeviseSelected.Taux.Equals("0"))
                                                FTotalProrata = Math.Round((centime*Ftotaltva), 0);
                                                else
                                                    FTotalProrata = Math.Round((centime * Ftotaltva), 2);

                                             

                                                MontantProrata = TaxePorataSelected.Taux;

                                                Montanttva = TaxeSelected.Taux;
                                                FtotalTTC = (Ftotaltva + FTotalHt + FTotalProrata);
                                                 #endregion
                                            }
                                            else if (clientCourant.Exonerere.CourtDesc.Equals("exo"))
                                            {
                                                // pas de tva et centimes
                                               #region Exonere


                                                Montanttva = string.Format("({0}) {1}", (clientCourant.IdLangue == 1 ? "Exonere" : "Exonerate"), TaxeSelected.Taux);
                                                Ftotaltva += 0;
                                                double valp = 0;
                                                double calcul;


                                                // si prorata
                                                if (TaxePorataSelected.ID_Taxe > 0)
                                                {
                                                    MontantProrata = TaxePorataSelected.Taux;
                                                    calcul = 0;
                                                }
                                                else
                                                {
                                                    MontantProrata = "";
                                                    valprorata = 0;
                                                    calcul = 0;
                                                }


                                               // Lignecourante.LibelleDetail = "";
                                                FTotalProrata = calcul;

                                                FtotalTTC = FTotalHt;




                                                #endregion
                                            }
                                           
                                           
                                        }

                                        #endregion
                                        

                                    }

                                        CacheLigneCommandList.Add(Lignecourante);
                                        LigneCommandList = null;
                                        LigneCommandList = CacheLigneCommandList;
                                        LigneFacture = new LigneFactureModel();
                                        Lignecourante = null;
                                        ProduitSelected = null;
                                        PrixUnitaireselected = 0;
                                        ActualFacture = FatureCurrent;
                                        isupdateinvoice = false;
                                        isDaysCalcul = false;
                                        newNewQte = 0;
                                        newPrixUnit = 0;
                                        IsModeFacturation = false;
                                        //ProduiList = null;
                                        AfficheStatutProduit = string.Empty;
                                       
                                    
                                    #endregion

                                  
                                    if (ExploitationFields.Count == 0)
                                    {
                                        if (ExploitationSelected != null)
                                        {
                                            ExploitationFields.Add(ExploitationSelected.IdExploitation, ExploitationSelected.Libelle);
                                            Listexp = ExploitationSelected.Libelle;
                                            ListExpid = ExploitationSelected.IdExploitation.ToString();
                                        }
                                    }
                                    else
                                    {
                                        if (ExploitationSelected != null)
                                        {
                                            if (!ExploitationFields.ContainsKey(ExploitationSelected.IdExploitation))
                                            {

                                                ExploitationFields.Add(ExploitationSelected.IdExploitation, ExploitationSelected.Libelle);
                                                Listexp = Listexp + ";" + ExploitationSelected.Libelle;
                                                ListExpid = ListExpid + ";" + ExploitationSelected.IdExploitation.ToString();
                                            }
                                        }
                                    }

                                    isFactureOperation = true;
                                

                                    IsOperationClosing = true;
                                    isligneitemsoperation = true;
                                    onloading = false;
                                }
                                catch (Exception ex)
                                {
                                    CustomExceptionView view = new CustomExceptionView();
                                    view.Owner = Application.Current.MainWindow;
                                    view.Title = "ERREURE AJOUT LIGNE FACTURE";
                                    view.ViewModel.Message ="Erreur <<2002>Impossible d'ajouter cette ligne de facture" ;
                                    view.ShowDialog();
                                    Utils.logUserActions("Erreure <<2002>> : " + ex.Message +" par "+userConnected.Loggin  , " ");
                                }
                            }
                            else
                            {
                                // traitement des modifications de la ligne selectionner dans le grid

                                if (FatureCurrent.IdFacture > 0)
                                {
                                    try
                                    {
                                        #region UPDATE DATABASE ITEM
                                        // modification une ligne deja  en base

                                        if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 3 || int.Parse(FatureCurrent.CurrentStatut.CourtDesc)==7)
                                        {

                                            if (CacheLigneCommandList != null && CacheLigneCommandList.Count > 0)
                                            {
                                                LigneCommand ligne = CacheLigneCommandList.Find(l => l.ID == LigneFacture.IdLigneFacture);
                                                if (ligne != null)
                                                {
                                                    CacheLigneCommandList.Remove(ligne);
                                                    if (ligne.Description != null)
                                                    {
                                                        if (!ligne.Description.Equals(LigneFacture.Description))
                                                        {
                                                            ligne.Description = LigneFacture.Description;
                                                            ligne.dateModif = DateTime.Now;
                                                        }
                                                    }
                                                    else ligne.Description = LigneFacture.Description ?? string .Empty ;
                                                    ligne.tva = FatureCurrent.CurrentTaxe.Taux;

                                                    ligne.Idetail = idDetailPode;

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

                                                    ligne.SpecialMode = IsModeFacturation;
                                                  

                                                    if (LigneFacture.Exonere != IsItemsExonere)
                                                        ligne.estExonere = IsItemsExonere;
                                                    else
                                                        ligne.estExonere = LigneFacture.Exonere;

                                                    if (!isDaysCalcul)
                                                       ligne.quantite = (decimal)LigneFacture.Quantite;
                                                    else ligne.quantite = (decimal)newNewQte;

                                                    // facture avoir
                                                      // if (ModeFactureAvoir)
                                                          // ligne.quantite = -ligne.quantite;
                                                   

                                                   // ligne.IdExploitation =ExploitationSelected !=null? ExploitationSelected.IdExploitation:0;

                                                    int idexploit = ExploitationSelected != null ? ExploitationSelected.IdExploitation : 0;
                                                    var listExp = NewDetailListeProduit.FindAll(p => p.IdExploitation == idexploit);
                                                    if (listExp.Count > 0)
                                                        ligne.IdExploitation = idexploit;
                                                    else ligne.IdExploitation = 0;

                                                    if (CacheLigneCommandList.Count == 0)
                                                        ligne.IdExploitation = idexploit;
                                                    else
                                                    {
                                                        if (CacheLigneCommandList.Exists(ex => ex.IdExploitation == idexploit))
                                                            ligne.IdExploitation = idexploit;
                                                        else
                                                        {
                                                            if (listExp.Count == 0)
                                                                ligne.IdExploitation = CacheLigneCommandList[0].IdExploitation;
                                                            else ligne.IdExploitation = idexploit;
                                                        }
                                                    }


                                                    if (!isDaysCalcul)
                                                        ligne.PrixUnit = PrixUnitaireselected;
                                                    else ligne.PrixUnit = newPrixUnit;

                                                  //  ligne.estprorata = Isproratable;
                                                    ligne.IsdeletedEnabled = false;
                                                   // if (IsModeFacturation)
                                                       // ligne.montantHt =  ligne.PrixUnit;
                                                   // else 
                                                 

                                                    ligne.montantHt = ligne.quantite * ligne.PrixUnit;
                                                    ligne.dateModif = DateTime.Now;
                                                    FTotalHt = (double)CacheLigneCommandList.Sum(l => l.quantite * l.PrixUnit);
                                                   
                                                

                                                    double valTva = 0;
                                                    double oldProrata = 0;

                                                    FTotalHt = (double)CacheLigneCommandList.Sum(l => l.montantHt);
                                                    FTotalHt += (double)ligne.montantHt;

                                                    if (clientCourant.Exonerere.CourtDesc.Equals("non"))
                                                    {
                                                        #region Non Exonere

                                                        valTva = GetTaxeFr(TaxeSelected.Taux, currentCulture) * FTotalHt;
                                                        valTva = Math.Round(valTva, 2);
                                                        Montanttva = TaxeSelected.Taux;
                                                        Ftotaltva = valTva;

                                                        // calcule centimes
                                                        var centime = GetTaxeFr(TaxePorataSelected.Taux, currentCulture) * Ftotaltva;
                                                        FTotalProrata = Math.Round(centime, 2);
                                                        MontantProrata = TaxePorataSelected.Taux;

                                                        FtotalTTC = valTva + FTotalHt + FTotalProrata;

                                                        #endregion

                                                    }
                                                    else if (clientCourant.Exonerere.CourtDesc.Equals("exo"))
                                                    {
                                                        valTva = 0;

                                                        #region Exonere 
                                                        
                                                       
                                                        Montanttva = string.Format("({0}) {1}", (clientCourant.IdLangue == 1 ? "Exonere" : "Exonerate"), TaxeSelected.Taux);
                                                        Ftotaltva += 0;
                                                        if (TaxePorataSelected.ID_Taxe > 0)
                                                        {
                                                            MontantProrata = TaxePorataSelected.Taux;
                                                          
                                                        }
                                                        else
                                                        {
                                                            MontantProrata = "";
                                                          
                                                           
                                                        }
                                                        FTotalProrata = 0;

                                                        FtotalTTC =  FTotalHt;

                                                        #endregion
                                                    }
                                               

                                                  
                                                  

                                                    CacheLigneCommandList.Add(ligne);

                                                }


                                                LigneCommandList = null;
                                                LigneCommandList = CacheLigneCommandList;
                                                LigneFacture = new LigneFactureModel ();
                                                PrixUnitaireselected = 0;
                                                IsItemsExonere = false;
                                                ExonereEnable = false;
                                                isupdateinvoice = false;
                                                isDaysCalcul = false ;
                                                newNewQte = 0;
                                                newPrixUnit = 0;
                                                AfficheStatutProduit = string.Empty;
                                            }
                                        }
                                        #endregion



                                        isFactureOperation = true;
                                        IsOperationClosing = true;
                                        isligneitemsoperation = true;
                                        onloading = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        CustomExceptionView view = new CustomExceptionView();
                                        view.Owner = Application.Current.MainWindow;
                                        view.Title = "ERREURE MISE JOUR NOUVELLE LIGNE FACTURE";
                                        view.ViewModel.Message ="Erreure <<2003>> : mise jour de la ligne impossible, vérifier vos données saisies";
                                        view.ShowDialog();
                                        Utils.logUserActions("Erreure <<2003>> : " + ex.Message + " par " + userConnected.Loggin, " ");
                                    }
                                }
                                else
                                {
                                    try
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
                                                ligne.estprorata = Isproratable;
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
                                                    ligne.IdSite = societeCourante.IdSociete;
                                                }

                                                ligne.SpecialMode = IsModeFacturation;
                                                //ligne.quantite = (decimal)LigneFacture.Quantite;
                                                //ligne.PrixUnit = PrixUnitaireselected;

                                                if (!isDaysCalcul)
                                                    ligne.quantite = (decimal)LigneFacture.Quantite;
                                                else ligne.quantite = (decimal)newNewQte;

                                                // facture avoir
                                             
                                                if (!isDaysCalcul)
                                                    ligne.PrixUnit = PrixUnitaireselected;
                                                else ligne.PrixUnit = newPrixUnit;

                                                //ligne.IdExploitation =ExploitationSelected !=null ? ExploitationSelected.IdExploitation:0;

                                                int idexploit = ExploitationSelected != null ? ExploitationSelected.IdExploitation : 0;
                                                var listExp = NewDetailListeProduit.FindAll(p => p.IdExploitation == idexploit);
                                                if (listExp.Count > 0)
                                                    ligne.IdExploitation = idexploit;
                                                else ligne.IdExploitation = 0;

                                                if (CacheLigneCommandList.Count == 0)
                                                    ligne.IdExploitation = idexploit;
                                                else
                                                {
                                                    if (CacheLigneCommandList.Exists(ex => ex.IdExploitation == idexploit))
                                                        ligne.IdExploitation = idexploit;
                                                    else
                                                    {
                                                        if (listExp.Count == 0)
                                                            ligne.IdExploitation = CacheLigneCommandList[0].IdExploitation;
                                                        else ligne.IdExploitation = idexploit;
                                                    }
                                                }

                                                ligne.IsdeletedEnabled = false;
                                               // if (IsModeFacturation)
                                                   // ligne.montantHt = ligne.PrixUnit;
                                               // else 
                                                ligne.montantHt =ligne.quantite * ligne.PrixUnit;
                                                ligne.Idetail = idDetailPode;
                                                if (ModeFactureAvoir)
                                                    ligne.montantHt = -ligne.montantHt;

                                                string modeExoneration = string.Empty;

                                                double valTva = 0;
                                                double oldProrata = 0;

                                                FTotalHt = (double)CacheLigneCommandList.Sum(l => l.montantHt);
                                                FTotalHt += (double)ligne.montantHt;

                                                if (clientCourant.Exonerere.CourtDesc.Equals("non"))
                                                {
                                                    #region Non Exonere

                                                    valTva = GetTaxeFr(TaxeSelected.Taux, currentCulture) * FTotalHt;
                                                    valTva = Math.Round(valTva, 2);
                                                    Montanttva = TaxeSelected.Taux;
                                                    Ftotaltva = valTva;

                                                    // calcule centimes
                                                    var centime = GetTaxeFr(TaxePorataSelected.Taux, currentCulture) * Ftotaltva;
                                                    FTotalProrata = Math.Round(centime, 2);
                                                    MontantProrata = TaxePorataSelected.Taux;


                                                    FtotalTTC = valTva + FTotalHt + FTotalProrata;

                                                    #endregion

                                                }
                                                else if (clientCourant.Exonerere.CourtDesc.Equals("exo"))
                                                {
                                                    valTva = 0;
                                                    #region Exonere


                                                    Montanttva = string.Format("({0}) {1}", (clientCourant.IdLangue == 1 ? "Exonere" : "Exonerate"), TaxeSelected.Taux);
                                                    Ftotaltva += 0;

                                                    if (TaxePorataSelected.ID_Taxe > 0)
                                                    {
                                                        
                                                        MontantProrata = TaxePorataSelected.Taux;
                                                    }
                                                    else
                                                    {
                                                        MontantProrata = "";
                                                        FTotalProrata = 0;
                                                    }

                                                    FtotalTTC =  FTotalHt;

                                                    #endregion
                                                }
                                            



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
                                            isDaysCalcul = false ;
                                            newNewQte = 0;
                                            newPrixUnit = 0;
                                            AfficheStatutProduit = string.Empty;
                                        }
                                        #endregion

                                        IsOperationClosing = true;
                                        isligneitemsoperation = true;
                                        isFactureOperation = true;
                                        onloading = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        CustomExceptionView view = new CustomExceptionView();
                                        view.Owner = Application.Current.MainWindow;
                                        view.Title = "ERREURE MODIFICATION LIGNE FACTURE";
                                        view.ViewModel.Message = ex.Message;
                                        view.ShowDialog();
                                    }
                                }
                               
                            }

                            if (CostTaxeSelected != null)
                            {
                                TauxMargeBenef = CostTaxeSelected.Taux;
                                MontantMargeBenef = GetTaxeFr(CostTaxeSelected.Taux, currentCulture) * FTotalHt;
                                FtotalTTC += MontantMargeBenef;
                            }

                        }
                        else
                            MessageBox.Show("Quantité doit etre Supérieur à [0] !", "Probleme de Quantité", MessageBoxButton.OK, MessageBoxImage.Warning);

                     ModeFactureAvoirEnable = false;
                     ModeFacturenormaleEnable = false;
                    isOperationpossible = true;
                         
                }
            }
        }

        #endregion


        #region FACTURE ADD

        // annuler facture
        private void canNew()
        {
                getDatas();
           
        }

           void getDatas()
            {

                if (droitFormulaire.Developpeur || droitFormulaire.Ecriture  )
            {
                if (FatureCurrent != null && FatureCurrent.IdClient > 0)
                {
                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                    messageBox.Owner = Application.Current.MainWindow;
                    messageBox.Title = "ANNULATION ";
                    messageBox.ViewModel.Message = "Attention cette operation va annuler celle En cours ?";
                    if (messageBox.ShowDialog().Value == true)
                    {

                        #region new region 

                       
                        try
                        {
                            ClientIndex =null;
                           ObjetIndex  = null;
                            ExploitationIndex = null;
                            //Departementindex = -1;
                            // _container = container;
                            ProgressBarVisibility = false;
                            IsEnabledClient = false;
                           
                            CulumndetailVisible = false;
                            EnableAfficheResume = false;
                            isReloadFacture = false;
                           // orverviewSelecte = new OverviewFactureModel();
                           // currentCulture = CultureInfo.CurrentCulture;
                          
                            TotalFacturesCreer = new List<FactureModel>();
                            isnewFactureEdite = false;
                            IsOperation = false;
                            isFactureOperation = false;
                            isligneitemsoperation = false;

                            ModeFacturenormale = true;
                            ModeFacturenormaleEnable = true;
                            ModeFactureAvoirEnable = true;

                            isJourLimiteValable = true;
                            IsenableCostaxe = true;
                            IsenableTaxeList = false;
                            onloading = false;

                           // CmbEnabled = false;
                            isFactureExist = false;
                          
                          
                            //loadDatasnewFacture();
                            factureOnLoad = true;


                            LigneFacture = null;
                            PrixUnitaireselected = 0;
                            ObjetList = null;
                            ObjetSelected = null;
                            ExploitationList = null;
                            FTotalHt = 0;
                            FtotalTTC = 0;
                            Montanttva = "";
                            Ftotaltva = 0;
                            FTotalProrata = 0;
                            TauxMargeBenef = "";
                            MontantMargeBenef = 0;
                            MontantProrata = "";
                            ClientSelected = null;
                            ClientList = null;
                          
                           
                            //ClientIndex = -1;
                           
                            //DeviseConvert = null;
                            Depselected = null;
                            IsProrataEnabled = true;

                         
                           
                          
                            IsdetailExiste = false;
                            DetailProdEnable = false;
                            isReloadFacture = false;
                            isnewFacture = true;
                            //ModeFacturenormale = true;
                            DateMoisPrestation = null;
                            if (IsValidateDate)
                                IsValidateDate = false;
                            ChkDateOutValidate = false;
                            if (ChkDateSuspended)
                                ChkDateSuspended = false;
                            if (ChkNonValable)
                                ChkNonValable = false;
                            Listexp = string.Empty;
                            ListExpid = string.Empty;
                            ModeFacturenormale = true;
                            ModeFactureAvoirEnable = true;
                            ModeFacturenormaleEnable = true;
                            IsOperationClosing = false;
                            ExploitationFields = new Dictionary<int, string>();
                            isligneitemsoperation = false;
                            isFactureOperation = false;
                            //CostTaxeSelected = null;
                            CostTaxeSelected = null;
                            CostTaxeList = null;
                            //// LoadDefaultTaxes();
                          
                            //FatureCurrent = new FactureModel();

                            LigneCommandList = new List<LigneCommand>();
                            CacheLigneCommandList = null;
                            ActualFacture = new FactureModel();
                            ProduitSelected = null;

                            FactureModel _factureSelected = new FactureModel();
                            FatureCurrent = _factureSelected;

                            isReload = false;
                        }
                        catch (Exception ex)
                        {
                            StyledMessageBoxView messageBoxy = new StyledMessageBoxView();
                            messageBoxy.Owner = Application.Current.MainWindow;
                            messageBoxy.Title = "INITIALISATION FACTURE";
                            messageBoxy.ViewModel.Message ="erreur :"+ ex.Message;
                            messageBoxy.ShowDialog();
                        }

                        #endregion
                        // IndexeTaxe = TaxeList.FindIndex(t=>t.TaxeDefault==true);
                    }
                }
            }
            }
            
        

        bool canExecutenew()
        {
            if (droitFormulaire.Developpeur || droitFormulaire.Ecriture )
            {
                return GlobalDatas.IsArchiveSelected == true ? false : (isJourLimiteValable == true ? true : false);
            }
            else
                return false;
        }
        //
        bool canExecuteSavefacture()
        {
            bool isvalue = false;
            if (droitFormulaire.Developpeur || (droitFormulaire.Ecriture && FatureCurrent.IdFacture==0) || (droitFormulaire.Edition && FatureCurrent.IdFacture>0))
            {


                if (FatureCurrent != null)
                {
                    if (FatureCurrent.IdFacture > 0)
                    {
                        if (isOperationpossible)
                            // if (isligneitemsoperation || isFactureOperation)
                            isvalue = true;
                        else if (isligneitemsoperation || isFactureOperation)
                            isvalue = true;
                        else
                            isvalue = false;

                        if (FatureCurrent.IdStatut >= 14003 && FatureCurrent.IdStatut<14007)
                            isvalue = false;
                    }
                    else
                    {
                        if (isJourLimiteValable)
                        {
                            if (isligneitemsoperation && isFactureOperation)
                                isvalue = true;
                        }
                        else isvalue = false;

                    }
                }
                else
                {

                    isvalue = false;


                }

                if (GlobalDatas.IsArchiveSelected)
                    isvalue = false;
            }

            return isvalue;
            //return ActualFacture != null ? (ActualFacture.ClienOk==true ) : false;
        }

        private void canSaveFacture(object param)
        {
             
                getSaveDatas();
            
        }


        void getSaveDatas()
        {
            bool isfactureUpdate = false;
            bool isligneFactureUpdate = false;
            string oldfactureMax = null;
            string newFactureGeneree = null;
            IsOperation = true;
            string idexp = null;
            string liblexp = null;

            List<LigneFactureModel> listItems = null;
            object[] tabretour = null;
            switch (FatureCurrent.IdFacture)
            {
                case 0:
                    {
                        
                            #region Nouvelle Facture



                            if (operation == "creation")
                            {
                                if (FatureCurrent.IdClient > 0)
                                {
                                    if (LigneCommandList == null || LigneCommandList.Count == 0)
                                    {
                                        MessageBox.Show(ConstStrings.Get("factureVM_NewFacture_ErrorrequireItemsMsg") , "PROBLEME CREATION FACTURE", MessageBoxButton.OK, MessageBoxImage.Error);
                                        return;
                                    }
                                    try
                                    {
                                        // création de la facture
                                        bool isNormaAvoir = false;
                                        if (ExploitationSelected != null)
                                            if (ExploitationSelected.IdExploitation > 0)
                                                FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;

                                        if (UserConnected != null)
                                            FatureCurrent.IdCreerpar = UserConnected.Id;
                                        if (DeviseSelected != null)
                                            FatureCurrent.IdDevise = DeviseSelected.ID_Devise;
                                        else
                                        {
                                            MessageBox.Show(ConstStrings.Get("factureVM_NewFacture_requireDeviseMsg"),"NOTIFICATION",MessageBoxButton.OK, MessageBoxImage.Error);
                                            return;
                                        }
                                        if (TaxeSelected != null)
                                            FatureCurrent.IdTaxe =TaxeSelected.ID_Taxe;
                                        else
                                        {
                                            MessageBox.Show(ConstStrings.Get("factureVM_NewFacture_requireTaxeMsg") ,"NOTIFICATION",MessageBoxButton.OK, MessageBoxImage.Error);
                                            return;
                                        }

                                        if (societeCourante != null)
                                            FatureCurrent.IdSite = societeCourante.IdSociete;
                                        FatureCurrent.IsProrata = IsPorata;
                                        if (Depselected == null)
                                        {
                                            var dep= depService.Departemnt_SELECTById(1);
                                            FatureCurrent.IdDepartement = dep != null ? dep.IdDep : 0;
                                        }
                                        if (!FatureCurrent.DateCreation.HasValue)
                                            FatureCurrent.DateCreation = DateTime.Now;
                                        else
                                        {
                                            string courtDate = FatureCurrent.DateCreation.Value.ToShortDateString();
                                            int h = DateTime.Now.Hour;
                                            int mn = DateTime.Now.Minute;
                                            int ss = DateTime.Now.Second;
                                            string fana = string.Format("{0} {1}:{2}:{3}", courtDate, h.ToString().PadLeft(2, '0'), mn.ToString().PadLeft(2, '0'), ss.ToString().PadLeft(2, '0'));
                                            FatureCurrent.DateCreation = DateTime.Parse(fana);
                                        }


                                        if (FatureCurrent.MoisPrestation == null)
                                        {
                                            MessageBox.Show( ConstStrings.Get("factureVM_NewFacture_requireDateMsg"), "INFORMATION", MessageBoxButton.OK, MessageBoxImage.Warning);
                                            return;
                                        }

                                        if (LigneCommandList == null)
                                        {
                                            MessageBox.Show(ConstStrings.Get("factureVM_NewFacture_ErrorrequireItemsMsg") , "INFORMATIONE", MessageBoxButton.OK, MessageBoxImage.Error);
                                            return;
                                        }


                                        listItems = TraitementLigneCommande();

                                        #region TOTALFACTURE

                                      
                                        FatureCurrent.TotalTTC =FtotalTTC;
                                        FatureCurrent.TotalHT=FTotalHt;
                                        FatureCurrent.TotalTVA = Ftotaltva;
                                        FatureCurrent.TotalPRORATA = FTotalProrata;

                                        if (CostTaxeSelected != null)
                                        {
                                            FatureCurrent.MaregeBeneficiaireId = CostTaxeSelected.ID_Taxe;
                                            FatureCurrent.TotalMarge = MontantMargeBenef;
                                        }
                                      
                                        //FatureCurrent.TotalHT = double.Parse(tabretour[4].ToString());
                                        //FatureCurrent.TotalTVA = double.Parse(tabretour[1].ToString());
                                        //FatureCurrent.TotalPRORATA = double.Parse(tabretour[3].ToString());
                                        //    }
                                        //    else FatureCurrent.TotalTTC = 0;
                                        //}
                                        #endregion

                                        if (ModeFacturenormale)
                                        {
                                            FatureCurrent.IdStatut = CurrentStatut.IdStatut;
                                            isNormaAvoir = true;
                                            if (CostTaxeSelected != null)
                                            FatureCurrent.TauxMargeBeneficiaire = CostTaxeSelected.Taux;
                                        }
                                        else
                                        {
                                            //facture avoir
                                            StatutModel newStatut = StatutFacture.First(f => f.CourtDesc == "7");
                                            FatureCurrent.IdStatut = newStatut.IdStatut;
                                            if (CostTaxeSelected != null)
                                                FatureCurrent.TauxMargeBeneficiaire = CostTaxeSelected.Taux;
                                            isNormaAvoir = false;
                                        }
                                     

                                        FatureCurrent.ExploitationIds = ListExpid;
                                        FatureCurrent.ExploitationList = Listexp;

                                        
                                        idNewfacture = factureservice.FACTURE_ADD_LIGNEGFACTURE(FatureCurrent, listItems, isNormaAvoir);

                                        //vérification montant facture
                                        factureservice.VERIF_FACTURE_HHTC(FatureCurrent.IdFacture);
                                       // IsOperation = true;
                                        isfactureUpdate = true;
                                        IsnewFactureEdite = true;
                                        CacheDatas.lastUpdatefacture = null;
                                        
                                        //ExploitationFields
                                        Listexp = null;
                                        ListExpid = null;
                                        isligneFactureUpdate = true;

                                       // for (int i = 0; i < 50; i += 5)
                                            //Thread.Sleep(100);
                                       
                                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                        messageBox.Owner = Application.Current.MainWindow;
                                        messageBox.Title = ConstStrings.Get("factureVM_titreNewfacture");
                                        messageBox.ViewModel.Message = ConstStrings.Get("factureVM_msgNewfacture");
                                        messageBox.ShowDialog();

                                        if (idNewfacture != null)
                                        {
                                            //rechergement facture
                                            isReloadFacture = true;
                                            issaveUpdatedata = false;
                                            if (isNormaAvoir)
                                            FatureCurrent = factureservice.GET_FACTURE_BYID(long.Parse(idNewfacture.ToString()));
                                            else
                                               FatureCurrent = factureservice.GET_FACTUREVALIDE_BYID(long.Parse(idNewfacture.ToString()),societeCourante.IdSociete,0);

                                            if (ListExpid != null)
                                            {
                                                string[] idsexp = ListExpid.Split(new char[] { ';' });
                                                if (idsexp.Length > 0)
                                                {
                                                    for (int i = 0; i < idsexp.Length; i++)
                                                        exploitationservice.EXPLOITATION_FACTUREE_ADD(FatureCurrent.IdFacture, int.Parse(idsexp[i]), 0);
                                                }
                                            }
                                            isAddInvoice = true;
                                            DetailProdEnable = false;
                                            ListExpid = FatureCurrent.ExploitationIds;
                                            Listexp = FatureCurrent.ExploitationList;
                                             if (ModeFacturenormale)
                                             Utils.logUserActions("<--; 1; UI edition facture --> Création Nouvelle facture : " + FatureCurrent.NumeroFacture  + " Par :" + userConnected.Loggin, " ");
                                            else
                                                 Utils.logUserActions("<--; 1; UI edition facture --> Création Nouvelle facture avoir : " + FatureCurrent.NumeroFacture + " Par :" + userConnected.Loggin, " ");

                                            //archivage facture avoir
                                             //if (!isNormaAvoir)
                                             //{
                                             //    FatureCurrent.LibelleUserNom = UserConnected.Nom;
                                             //    FatureCurrent.LibelleUserPrenom = UserConnected.Prenom;
                                                  
                                             //    factureservice.FACTURE_VALIDATION(FatureCurrent.IdFacture, DateTime.Now, 14007, UserConnected.Id, true, FatureCurrent);
                                             //}

                                        }
                                        PrixUnitaireselected = 0;
                                       // ExploitationFields = null;
                                        TotalFacturesCreer.Add(FatureCurrent);
                                      

                                        isFactureOperation = false;
                                        isligneitemsoperation = false;
                                    }
                                    catch (Exception ex)
                                    {
                                        CustomExceptionView view = new CustomExceptionView();
                                        view.Owner = Application.Current.MainWindow;
                                        view.Title = ConstStrings.Get("factureVM_titreNewfacture_error");
                                        view.ViewModel.Message = ConstStrings.Get("factureVM_msgNewfacture_error") + ex.Message;
                                            Utils.logUserActions("<--; 5; UI edition Creation facture --> Erreure de création facture par :" + userConnected.Loggin +" \n"+ ex.Message, " ");
                                        view.ShowDialog();
                                        IsBusy = false;
                                        this.MouseCursor = null;
                                    }
                                }
                            }
                            #endregion
                            IsOperationClosing = true;
                            isOperationpossible = false;
                           // Utils.logUserActions(string.Format("<-- Ui Edition facture : -- Creéation nouvelle facture Num: {0} client :{1} crée par : {2}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom), "");
                      
                        break;
                    }
                default:
                    {

                        switch (operation)
                        {

                            case "validation":
                                {
                                    // nouveau statut de mise jour
                                    if (valeurDalidation == "encours")
                                    {
                                        #region Facture en creation pour encours validation


                                       
                                            try
                                            {
                                                if (IsValidateDate)
                                                {
                                                    #region Facture Creation pour encours validation avec modif statut




                                                    #region Version


                                                    //versionFactureCurrent
                                                    DataTable dtblVersion = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
                                                    if (versionFactureCurrent != null && versionFactureCurrent.Rows.Count > 0)
                                                    {
                                                        if (versionFactureCurrent.Rows[0]["version"] != DBNull.Value && dtblVersion.Rows[0]["version"] != DBNull.Value)
                                                        {
                                                            DateTime oldTimeVersion = Convert.ToDateTime(versionFactureCurrent.Rows[0]["version"]);
                                                            DateTime newTimeVersion = Convert.ToDateTime(dtblVersion.Rows[0]["version"]);

                                                            TimeSpan differenceVersion = oldTimeVersion - newTimeVersion;
                                                            if (differenceVersion.TotalMilliseconds != 0)
                                                            {
                                                                StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                                                messageBox.Owner = Application.Current.MainWindow;
                                                                messageBox.Title = "INFORMATION  ACCESS CONCURRENTIEL";
                                                                messageBox.ViewModel.Message = string.Format("Cette facture vient de faire l' objet d'une modification par \n l'utilisateur :  {0},  date modificaion {1} \n statut courant : {2} \n voulez vous néamoins modifier ", dtblVersion.Rows[0]["Nom"].ToString() + " " + dtblVersion.Rows[0]["Prenom"].ToString(), dtblVersion.Rows[0]["Date_Modification"].ToString(), dtblVersion.Rows[0]["statuts"].ToString());
                                                                if (messageBox.ShowDialog().Value == false)
                                                                {
                                                                    isReloadFacture = false;
                                                                    issaveUpdatedata = false;
                                                                    IsOperation = false;
                                                                    return;
                                                                }
                                                            }
                                                        }
                                                    }

                                                    #endregion

                                                    #region TOTALFACTURE

                                                    FatureCurrent.TotalTTC = FtotalTTC;
                                                    FatureCurrent.TotalHT = FTotalHt;
                                                    FatureCurrent.TotalTVA = Ftotaltva;
                                                    FatureCurrent.TotalPRORATA = FTotalProrata;

                                                    if (CostTaxeSelected != null)
                                                    {
                                                        FatureCurrent.MaregeBeneficiaireId = CostTaxeSelected.ID_Taxe;
                                                        FatureCurrent.TotalMarge = MontantMargeBenef;
                                                    }

                                                    #endregion


                                                    #region Operation

                                                    // si modification facture
                                                    if (LigneCommandList == null )
                                                    {
                                                        MessageBox.Show("Cette facture ne possede pas de Lignes commande", "NOTIFICATION", MessageBoxButton.OK, MessageBoxImage.Error);
                                                        return;
                                                    }
                                                    FatureCurrent.IdStatut = CurrentStatut.IdStatut;
                                                    // FatureCurrent.IdClient = FatureCurrent.CurrentClient.IdClient;

                                                    if (FatureCurrent == null || FatureCurrent.IdDepartement == 0)
                                                        if (Depselected == null)
                                                        {
                                                            var dep = depService.Departemnt_SELECTById(1);
                                                            FatureCurrent.IdDepartement = dep != null ? dep.IdDep : 0;
                                                        }
                                                       
                                                    FatureCurrent.ExploitationIds = ListExpid;
                                                    FatureCurrent.ExploitationList = Listexp;

                                                    FatureCurrent.IdModifierPar = UserConnected.Id;
                                                    FatureCurrent.isfactureValide = true;
                                                    if (ExploitationSelected != null)
                                                        FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;
                                                    if (isFactureOperation)
                                                    {
                                                        factureservice.FACTURE_UPDATE(ref oldfactureMax, ref newFactureGeneree, FatureCurrent);

                                                        if (!string.IsNullOrEmpty(ListExpid))
                                                        {

                                                            string[] idsexp = ListExpid.Split(new char[] { ';' });
                                                            if (idsexp.Length > 0)
                                                            {
                                                                for (int i = 0; i < idsexp.Length; i++)
                                                                    exploitationservice.EXPLOITATION_FACTUREE_ADD(FatureCurrent.IdFacture, int.Parse(idsexp[i]), 1);
                                                            }

                                                        }
                                                        isfactureUpdate = true;
                                                    }
                                                    else
                                                    {
                                                        // update status
                                                        factureservice.FACTURE_UPDATE_WITHOUUPDATE(ref oldfactureMax, ref newFactureGeneree, FatureCurrent);
                                                        factureservice.FACTURE_UPDATE_STATUS(FatureCurrent.IdFacture, CurrentStatut.IdStatut, FatureCurrent.IdModifierPar, 1);
                                                        isfactureUpdate = true;
                                                        isFactureOperation = true;
                                                    }
                                                    // si items modifier
                                                    if (isligneitemsoperation)
                                                    {
                                                        if (LigneCommandList != null)
                                                            listItems = TraitementLigneCommande();
                                                        else return;

                                                        foreach (var lt in listItems)
                                                        {
                                                            lt.TauxTva = string.Empty;
                                                            lt.IdClient = 0;
                                                            lt.IdStatut = FatureCurrent.IdStatut;
                                                            ligneFactureService.LIGNE_FACTURE_ADD(lt);
                                                        }

                                                        isligneFactureUpdate = true;
                                                    }


                                                    if (isligneitemsoperation || isFactureOperation)
                                                    {


                                                        ExploitationFields = null;
                                                        Listexp = null;
                                                        ListExpid = null;
                                                        //vérification montant facture
                                                        factureservice.VERIF_FACTURE_HHTC(FatureCurrent.IdFacture);

                                                        StyledMessageBoxView messageBoxd = new StyledMessageBoxView();
                                                        messageBoxd.Owner = Application.Current.MainWindow;
                                                        messageBoxd.Title = "MISE JOUR FACTURE";
                                                        messageBoxd.ViewModel.Message = string.Format("Facture :{0}  Mise jour", FatureCurrent.NumeroFacture);
                                                        messageBoxd.ShowDialog();
                                                        isReloadFacture = true;
                                                        issaveUpdatedata = false;
                                                        FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                        ListExpid = FatureCurrent.ExploitationIds;
                                                        Listexp = FatureCurrent.ExploitationList;

                                                        ActualFacture = null;
                                                        IsBusy = false;

                                                       
                                                        // Utils.logUserActions(string.Format("<--Ui edition facture : --> facture [{0}] éditer par : {1}  ", FatureCurrent.NumeroFacture, UserConnected.Nom), "");
                                                        IsOperationClosing = true;
                                                        isFactureOperation = false;
                                                        isligneitemsoperation = false;
                                                        if (!string.IsNullOrEmpty(oldfactureMax))
                                                        {
                                                            Utils.logUserActions(string.Format("<--;21;UI  edition facture--> facture [{0}] Validée, changement de  statut <<validation>>  par : {1}  ", FatureCurrent.NumeroFacture, UserConnected.Nom), "");
                                                            Utils.logUserActions(string.Format("<--;21;UI  edition facture--> Derniere Facture Validée: {0} , Nouvelle facture {1}  par : {2}  ", oldfactureMax, newFactureGeneree, UserConnected.Nom), "");

                                                        }
                                                        else
                                                        {
                                                            Utils.logUserActions("*************************************************************************************", "");
                                                            Utils.logUserActions("*****  Nouvelle Numérotation pour la nouvelle année" + DateTime.Now, "");
                                                            Utils.logUserActions("*************************************************************************************", "");
                                                            Utils.logUserActions(string.Format("<-- ;22;UI  edition facture--> facture  modifier statut <<validation>> nouvo numero: {0} par : {1}  ", newFactureGeneree, UserConnected.Nom), "");
                                                        }

                                                    }

                                                    #endregion
                                                    //loadFactureInformation();

                                                    #endregion
                                                }
                                                else
                                                {
                                                    #region Facture Creation pour encours validation sans modif statut

                                                    if (isligneitemsoperation || isFactureOperation)
                                                    {
                                                        if (FatureCurrent.IdSite == societeCourante.IdSociete)
                                                        {
                                                            if ((droitFormulaire.Developpeur || droitFormulaire.Ecriture || droitFormulaire.Super || droitFormulaire.Proprietaire))
                                                            {
                                                                // if (int.Parse ( FatureCurrent.CurrentStatut.CourtDesc) == 1)
                                                                //



                                                                #region Version



                                                                DataTable dtblVersion = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
                                                                if (versionFactureCurrent != null && versionFactureCurrent.Rows.Count > 0)
                                                                {
                                                                    if (versionFactureCurrent.Rows[0]["version"] != DBNull.Value && dtblVersion.Rows[0]["version"] != DBNull.Value)
                                                                    {
                                                                        DateTime oldTimeVersion = Convert.ToDateTime(versionFactureCurrent.Rows[0]["version"]);
                                                                        DateTime newTimeVersion = Convert.ToDateTime(dtblVersion.Rows[0]["version"]);

                                                                        TimeSpan differenceVersion = oldTimeVersion - newTimeVersion;
                                                                        if (differenceVersion.TotalMilliseconds != 0)
                                                                        {
                                                                            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                                                            messageBox.Owner = Application.Current.MainWindow;
                                                                            messageBox.Title = "INFORMATION  ACCESS CONCURRENTIEL";
                                                                            messageBox.ViewModel.Message = string.Format("Cette facture vient de faire l' objet d'une modification par \n l'utilisateur :  {0},  date modificaion {1} \n statut courant : {2} \n voulez vous néamoins modifier ", dtblVersion.Rows[0]["Nom"].ToString() + " " + dtblVersion.Rows[0]["Prenom"].ToString(), dtblVersion.Rows[0]["Date_Modification"].ToString(), dtblVersion.Rows[0]["statuts"].ToString());
                                                                            if (messageBox.ShowDialog().Value == false)
                                                                            {
                                                                                isReloadFacture = false;
                                                                                issaveUpdatedata = false;
                                                                                IsOperation = false;
                                                                                return;
                                                                            }
                                                                        }
                                                                    }
                                                                }
                                                                #endregion



                                                                #region TOTALFACTURE

                                                                //if (ClientSelected.Exonerere != null)
                                                                //{

                                                                //    if (ClientSelected.Exonerere.CourtDesc == "exo")
                                                                //    {
                                                                //        if (ClientSelected.Porata != null)
                                                                //            tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, ClientSelected.Porata.Taux,
                                                                //                null, true, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"), NewDetailListeProduit);
                                                                //    }
                                                                //    if (ClientSelected.Exonerere.CourtDesc == "non")
                                                                //    {
                                                                //        if (TaxeSelected != null)
                                                                //            tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_Exonere(listItems, null,
                                                                //                TaxeSelected.Taux, false, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"), NewDetailListeProduit);
                                                                //    }
                                                                //    if (ClientSelected.Exonerere.CourtDesc == "part")
                                                                //    {
                                                                //        tabretour = AllTech.FrameWork.Global.Utils.GetTotal_TTC_ExonerePartiel(listItems,
                                                                //            ClientSelected.Porata.Taux, TaxeSelected.Taux, (ClientSelected.Llangue != null ? (ClientSelected.Llangue.Id == 1 ? "fr" : "en") : "o"), NewDetailListeProduit);
                                                                //    }

                                                                //    if (tabretour != null)
                                                                //        FatureCurrent.TotalTTC = double.Parse(tabretour[5].ToString());
                                                                //    else FatureCurrent.TotalTTC = 0;
                                                                //}
                                                                FatureCurrent.TotalTTC = FtotalTTC;
                                                                FatureCurrent.TotalHT = FTotalHt;
                                                                FatureCurrent.TotalTVA = Ftotaltva;
                                                                FatureCurrent.TotalPRORATA = FTotalProrata;

                                                                if (CostTaxeSelected != null)
                                                                {
                                                                    FatureCurrent.MaregeBeneficiaireId = CostTaxeSelected.ID_Taxe;
                                                                    FatureCurrent.TotalMarge = MontantMargeBenef;
                                                                }
                                                                else
                                                                {
                                                                    FatureCurrent.TotalMarge = 0;
                                                                    FatureCurrent.MaregeBeneficiaireId = 0;
                                                                }
                                                                #endregion

                                                                #region Operation


                                                                if (LigneCommandList == null && LigneCommandList.Count == 0)
                                                                {
                                                                    MessageBox.Show("Cette facture ne possede pas de Lignes commande", "NOTIFICATION", MessageBoxButton.OK, MessageBoxImage.Error);
                                                                    return;
                                                                }


                                                                if (isFactureOperation)
                                                                {
                                                                    if (ExploitationSelected != null)
                                                                        FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;
                                                                    FatureCurrent.IdModifierPar = UserConnected.Id;

                                                                 
                                                                        var dep = depService.Departemnt_SELECTById(1);
                                                                        FatureCurrent.IdDepartement = dep != null ? dep.IdDep : 0;
                                                                  
                                                                   // FatureCurrent.IdDepartement = depService.Departemnt_SELECTById(1).IdDep;
                                                                    FatureCurrent.isfactureValide = false;

                                                                    // if (int.Parse(CurrentStatut.CourtDesc) == 1 || int.Parse(CurrentStatut.CourtDesc) == 2)
                                                                    FatureCurrent.DateCloture = null;

                                                                    FatureCurrent.ExploitationIds = ListExpid;
                                                                    FatureCurrent.ExploitationList = Listexp;

                                                                    factureservice.FACTURE_UPDATE(ref oldfactureMax, ref newFactureGeneree, FatureCurrent);

                                                                    if (ListExpid != null)
                                                                    {
                                                                        string[] idsexp = ListExpid.Split(new char[] { ';' });
                                                                        if (idsexp.Length > 0)
                                                                        {
                                                                            for (int i = 0; i < idsexp.Length; i++)
                                                                                exploitationservice.EXPLOITATION_FACTUREE_ADD(FatureCurrent.IdFacture, int.Parse(idsexp[i]), 1);
                                                                        }
                                                                        isfactureUpdate = true;
                                                                    }
                                                                }
                                                                //{


                                                                if (isligneitemsoperation)
                                                                {
                                                                    if (LigneCommandList != null)
                                                                        listItems = TraitementLigneCommande();

                                                                    foreach (var lt in listItems)
                                                                        ligneFactureService.LIGNE_FACTURE_ADD(lt);
                                                                    isligneFactureUpdate = true;
                                                                }

                                                                if (isligneitemsoperation || isFactureOperation)
                                                                {


                                                                    // for (int i = 0; i < 50; i += 5)
                                                                    // Thread.Sleep(100);
                                                                    //vérification montant facture
                                                                    factureservice.VERIF_FACTURE_HHTC(FatureCurrent.IdFacture);

                                                                    ActualFacture = null;
                                                                    isReloadFacture = true;
                                                                    issaveUpdatedata = false;
                                                                    FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                                    ListExpid = FatureCurrent.ExploitationIds;
                                                                    Listexp = FatureCurrent.ExploitationList;
                                                                    IsBusy = false;

                                                                    //OverviewFactureModel overview = new OverviewFactureModel();
                                                                    //overview.Idice = 1;
                                                                    //overview.Idfacture = FatureCurrent.IdFacture;
                                                                    //overview.IdClient = FatureCurrent.IdClient;
                                                                    //overview.Iduser = UserConnected.Id;

                                                                    StyledMessageBoxView messageBoxe = new StyledMessageBoxView();
                                                                    messageBoxe.Owner = Application.Current.MainWindow;
                                                                    messageBoxe.Title = "MISE JOUR FACTURE";
                                                                    messageBoxe.ViewModel.Message = string.Format("Facture :{0}  Mise jour", FatureCurrent.NumeroFacture);


                                                                    Utils.logUserActions(string.Format("<--UI edition facture  mise jour facture [{0}] éditer par : {1}  ", FatureCurrent.NumeroFacture, UserConnected.Nom), "");
                                                                    messageBoxe.ShowDialog();
                                                                    IsOperationClosing = true;
                                                                    isFactureOperation = false;
                                                                    isligneitemsoperation = false;
                                                                    // loadFactureInformation();
                                                                }
                                                                #endregion

                                                            }
                                                        }
                                                    }
                                                   
                                                    #endregion

                                                }

                                            }
                                            catch (Exception ex)
                                            {
                                                CustomExceptionView view = new CustomExceptionView();
                                                view.Owner = Application.Current.MainWindow;
                                                view.Title = ConstStrings.Get("factureVM_Updatefacturetitre");
                                                view.ViewModel.Message = ConstStrings.Get("factureVM_UpdatefactureErrorMsg") + ex.Message;

                                                view.ShowDialog();
                                                IsBusy = false;
                                                this.MouseCursor = null;
                                                Utils.logUserActions(string.Format("<--;55;Ui Edition facture : -- Erreur modification facture Num: {0} client :{1} crée par : {2} {3}--> ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom, ex.Message), "");

                                            }
                                       
                                        #endregion
                                    }
                                    else if (valeurDalidation == "validation")
                                    {
                                        #region Update et Validation


                                        if (IsValidateDate)
                                        {
                                            bool testeReload = false;

                                            //cloture de la facture
                                           
                                                FatureCurrent.IdStatut = CurrentStatut.IdStatut;
                                                //if (BackgroundColorClient)
                                                if (FatureCurrent.IdSite == societeCourante.IdSociete)
                                                {
                                                    //modifie que les données du site
                                                    FatureCurrent.IdClient = FatureCurrent.CurrentClient.IdClient;
                                                    if (LigneCommandList != null)
                                                        listItems = TraitementLigneCommande();
                                                    else return;
                                                    try
                                                    {
                                                        #region Version


                                                        DataTable dtblVersion = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
                                                        if (versionFactureCurrent != null && versionFactureCurrent.Rows.Count > 0)
                                                        {
                                                            if (versionFactureCurrent.Rows[0]["version"] != DBNull.Value && dtblVersion.Rows[0]["version"] != DBNull.Value)
                                                            {
                                                                DateTime oldTimeVersion = Convert.ToDateTime(versionFactureCurrent.Rows[0]["version"]);
                                                                DateTime newTimeVersion = Convert.ToDateTime(dtblVersion.Rows[0]["version"]);

                                                                TimeSpan differenceVersion = oldTimeVersion - newTimeVersion;
                                                                if (differenceVersion.TotalMilliseconds != 0)
                                                                {
                                                                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                                                    messageBox.Owner = Application.Current.MainWindow;
                                                                    messageBox.Title = "INFORMATION  ACCESS CONCURRENTIEL";
                                                                    messageBox.ViewModel.Message = string.Format("Cette facture vient de faire l' objet d'une modification par \n l'utilisateur :  {0},  date modificaion {1} \n statut courant : {2} \n voulez vous néamoins modifier ", dtblVersion.Rows[0]["Nom"].ToString() + " " + dtblVersion.Rows[0]["Prenom"].ToString(), dtblVersion.Rows[0]["Date_Modification"].ToString(), dtblVersion.Rows[0]["statuts"].ToString());
                                                                    if (messageBox.ShowDialog().Value == false)
                                                                    {
                                                                        isReloadFacture = false;
                                                                        issaveUpdatedata = false;
                                                                        IsOperation = false;
                                                                        testeReload = false;

                                                                        return;
                                                                    }
                                                                    else testeReload = true;
                                                                }
                                                            }
                                                        }
                                                        #endregion
                                                      

                                                        #region TOTALFACTURE



                                                        FatureCurrent.TotalTTC = FtotalTTC;
                                                        FatureCurrent.TotalHT = FTotalHt;
                                                        FatureCurrent.TotalTVA = Ftotaltva;
                                                        FatureCurrent.TotalPRORATA = FTotalProrata;

                                                        if (CostTaxeSelected != null)
                                                        {
                                                            FatureCurrent.MaregeBeneficiaireId = CostTaxeSelected.ID_Taxe;
                                                            FatureCurrent.TotalMarge = MontantMargeBenef;
                                                            FatureCurrent.TauxMargeBeneficiaire = CostTaxeSelected.Taux;
                                                        }

                                                        #endregion

                                                        if (LigneCommandList == null )
                                                        {
                                                            MessageBox.Show("Cette facture ne possede pas de Lignes commande", "NOTIFICATION", MessageBoxButton.OK, MessageBoxImage.Error);
                                                            return;
                                                        }

                                                        if (ExploitationSelected != null)
                                                            FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;
                                                        FatureCurrent.IdModifierPar = UserConnected.Id;
                                                        if (int.Parse(CurrentStatut.CourtDesc) == 1 || int.Parse(CurrentStatut.CourtDesc) == 2)
                                                            FatureCurrent.DateCloture = null;

                                                        // FatureCurrent.DateCloture = FactureCache.DateCloture;
                                                        FatureCurrent.isfactureValide = false;
                                                        if (FatureCurrent == null || FatureCurrent.IdDepartement == 0)
                                                        {
                                                           
                                                                var dep = depService.Departemnt_SELECTById(1);
                                                            if (dep != null)
                                                            {
                                                                FatureCurrent.IdDepartement = dep.IdDep;
                                                                FatureCurrent.LibelleDepartement = dep.Libelle;
                                                            }
                                                        }

                                                        FatureCurrent.ExploitationIds = ListExpid;
                                                        FatureCurrent.ExploitationList = Listexp;

                                                        // FatureCurrent.LibelleSIte = societeCourante.RaisonSocial;
                                                        FatureCurrent.LibelleClient =ClientSelected !=null ?ClientSelected.NomClient:string.Empty ;
                                                        FatureCurrent.LibelleClientObjet = ObjetSelected != null ? ObjetSelected.Libelle : string.Empty;
                                                        FatureCurrent.LibelleUserNom = FatureCurrent.UserCreate != null ? FatureCurrent.UserCreate.Nom : new UtilisateurModel().UTILISATEUR_SELECTByID(FatureCurrent.IdCreerpar).Nom?? string.Empty;
                                                        FatureCurrent.LibelleUserPrenom = UserConnected.Nom;


                                                        FatureCurrent.LibelleTauxTaxe = TaxeSelected.Taux;
                                                        FatureCurrent.DeviseTAux = DeviseSelected.Taux;
                                                        FatureCurrent.LibelleDeviseConversion = DeviseSelected.Libelle;
                                                       // FatureCurrent
                                                     
                                                        FatureCurrent.LibelleStatut = CurrentStatut.Libelle;

                                                        if (CostTaxeSelected != null)
                                                        {
                                                            FatureCurrent.MaregeBeneficiaireId = CostTaxeSelected.ID_Taxe;
                                                            FatureCurrent.TotalMarge = MontantMargeBenef;
                                                        }
                                                        else
                                                        {
                                                            FatureCurrent.TotalMarge = null;
                                                            FatureCurrent.MaregeBeneficiaireId = null;
                                                            FatureCurrent.TauxMargeBeneficiaire = null;
                                                        }

                                                        if (factureservice.FACTURE_UPDATE(ref oldfactureMax, ref newFactureGeneree, FatureCurrent))
                                                        {
                                                            isfactureUpdate = true;
                                                            Listexp = null;
                                                            ListExpid = null;
                                                            //if (ListExpid != null)
                                                            //{
                                                            foreach (var lt in listItems)
                                                            {
                                                                lt.TauxTva = TaxeSelected.Taux;
                                                                lt.IdClient = FatureCurrent.IdClient;
                                                                lt.IdStatut = FatureCurrent.IdStatut;
                                                             
                                                                ligneFactureService.LIGNE_FACTURE_ADD(lt);
                                                            }

                                                            isligneFactureUpdate = true;
                                                            AfficheStatut = "Valider et cloturé:";
                                                            IsValidateDate = true;
                                                            IsEnableOutValidation = false;
                                                            IsEnabledSuspendedValidation = false;
                                                            IsEnabledNonValable = false;
                                                            IsEnabledchkDateValidate = false;
                                                             isOperationpossible=false;
                                                            isligneitemsoperation=false;
                                                            isFactureOperation=false;
                           
                                                         
                                                            factureservice.FACTURE_VALIDATION(FatureCurrent.IdFacture, DateTime.Now, FatureCurrent.IdStatut, UserConnected.Id, true, null);

                                                            //vérification montant facture
                                                            factureservice.VERIF_FACTURE_HHTC(FatureCurrent.IdFacture);

                                                            //OverviewFactureModel overview = new OverviewFactureModel();
                                                            //overview.Idice = 1;
                                                            //overview.Idfacture = FatureCurrent.IdFacture;
                                                            //overview.IdClient = FatureCurrent.IdClient;
                                                            //overview.Iduser = UserConnected.Id;

                                                            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                                            messageBox.Owner = Application.Current.MainWindow;
                                                            messageBox.Title = ConstStrings.Get("factureVM_msgNewfacture");
                                                            messageBox.ViewModel.Message = string.Format("Facture :{0}  Mise jour", FatureCurrent.NumeroFacture);
                                                            messageBox.ShowDialog();
                                                            Utils.logUserActions(string.Format("<--3; UI Edition facture : --  VAlidation facture avoir Num: {0} client :{1} crée par : {2}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom), "");
                                                            IsOperationClosing = true;
                                                        }
                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        CustomExceptionView view = new CustomExceptionView();
                                                        view.Owner = Application.Current.MainWindow;
                                                        view.Title = ConstStrings.Get("factureVM_Updatefacturetitre");
                                                        view.ViewModel.Message = ConstStrings.Get("factureVM_UpdatefactureErrorMsg");
                                                        IsOperationClosing = true;
                                                        view.ShowDialog();
                                                        IsBusy = false;
                                                        this.MouseCursor = null;
                                                        Utils.logUserActions(string.Format("<-- ;55;Ui Edition facture : -- Erreur modification facture Num: {0} client :{1} crée par : {2}-- {3}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom, ex.Message), "");
                                                    }
                                                }


                                                //modification statut facture


                                                ActualFacture = null;
                                                isReloadFacture = true;
                                                issaveUpdatedata = false;
                                                //FatureCurrent = factureservice.GET_FACTUREVALIDE_BYID(FatureCurrent.IdFacture,20001,0);

                                           

                                        }
                                        else
                                        {
                                            //simple validation statut creation
                                           
                                               
                                                    try
                                                    {

                                                        if (LigneCommandList == null )
                                                        {
                                                            MessageBox.Show("Cette facture ne possede pas de Lignes commande", "NOTIFICATION", MessageBoxButton.OK, MessageBoxImage.Error);
                                                            return;
                                                        }

                                                        if (LigneCommandList != null)
                                                            listItems = TraitementLigneCommande();
                                                        else return;
                                                        if (ExploitationSelected != null)
                                                            FatureCurrent.IdExploitation = ExploitationSelected.IdExploitation;
                                                        FatureCurrent.IdModifierPar = UserConnected.Id;

                                                        if (int.Parse(CurrentStatut.CourtDesc) == 1 || int.Parse(CurrentStatut.CourtDesc) == 2)
                                                            FatureCurrent.DateCloture = null;

                                                        #region Version



                                                        DataTable dtblVersion = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
                                                        if (versionFactureCurrent != null && versionFactureCurrent.Rows.Count > 0)
                                                        {
                                                            if (versionFactureCurrent.Rows[0]["version"] != DBNull.Value && dtblVersion.Rows[0]["version"] != DBNull.Value)
                                                            {
                                                                DateTime oldTimeVersion = Convert.ToDateTime(versionFactureCurrent.Rows[0]["version"]);
                                                                DateTime newTimeVersion = Convert.ToDateTime(dtblVersion.Rows[0]["version"]);

                                                                TimeSpan differenceVersion = oldTimeVersion - newTimeVersion;
                                                                if (differenceVersion.TotalMilliseconds != 0)
                                                                {
                                                                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                                                    messageBox.Owner = Application.Current.MainWindow;
                                                                    messageBox.Title = "INFORMATION  ACCESS CONCURRENTIEL";
                                                                    messageBox.ViewModel.Message = string.Format("Cette facture vient de faire l' objet d'une modification par \n l'utilisateur :  {0},  date modificaion {1} \n statut courant : {2} \n voulez vous néamoins modifier ", dtblVersion.Rows[0]["Nom"].ToString() + " " + dtblVersion.Rows[0]["Prenom"].ToString(), dtblVersion.Rows[0]["Date_Modification"].ToString(), dtblVersion.Rows[0]["statuts"].ToString());
                                                                    if (messageBox.ShowDialog().Value == false)
                                                                    {
                                                                        isReloadFacture = false;
                                                                        issaveUpdatedata = false;
                                                                        IsOperation = false;
                                                                        return;
                                                                    }
                                                                }
                                                            }
                                                        }

                                                        #endregion

                                                        // FatureCurrent.DateCloture = FactureCache.DateCloture;
                                                        if (FatureCurrent == null || FatureCurrent.IdDepartement == 0)
                                                            FatureCurrent.IdDepartement = depService.Departemnt_SELECTById(1).IdDep;
                                                        FatureCurrent.isfactureValide = false;

                                                        #region TOTALFACTURE

                                                       
                                                        FatureCurrent.TotalTTC = FtotalTTC;
                                                        FatureCurrent.TotalHT = FTotalHt;
                                                        FatureCurrent.TotalTVA = Ftotaltva;
                                                        FatureCurrent.TotalPRORATA = FTotalProrata;

                                                        if (CostTaxeSelected != null)
                                                        {
                                                            FatureCurrent.MaregeBeneficiaireId = CostTaxeSelected.ID_Taxe;
                                                            FatureCurrent.TotalMarge = MontantMargeBenef;
                                                        }
                                                        else
                                                        {
                                                            FatureCurrent.TotalMarge = null;
                                                            FatureCurrent.MaregeBeneficiaireId = null;
                                                            FatureCurrent.TauxMargeBeneficiaire = null;
                                                        }
                                                        #endregion

                                                        #region Operation



                                                        FatureCurrent.ExploitationIds = ListExpid;
                                                        FatureCurrent.ExploitationList = Listexp;

                                                        if (factureservice.FACTURE_UPDATE(ref oldfactureMax, ref newFactureGeneree, FatureCurrent))
                                                        {
                                                            isfactureUpdate = true;


                                                            foreach (var lt in listItems)
                                                                ligneFactureService.LIGNE_FACTURE_ADD(lt);
                                                            isligneFactureUpdate = true;
                                                            if (ListExpid != null)
                                                            {
                                                                string[] idsexp = ListExpid.Split(new char[] { ';' });
                                                                if (idsexp.Length > 0)
                                                                {
                                                                    for (int i = 0; i < idsexp.Length; i++)
                                                                        exploitationservice.EXPLOITATION_FACTUREE_ADD(FatureCurrent.IdFacture, int.Parse(idsexp[i]), 1);
                                                                }
                                                            }

                                                            //vérification montant facture
                                                            factureservice.VERIF_FACTURE_HHTC(FatureCurrent.IdFacture);

                                                         

                                                            ActualFacture = null;
                                                            isReloadFacture = true;
                                                            issaveUpdatedata = false;
                                                            FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                            ListExpid = FatureCurrent.ExploitationIds;
                                                            Listexp = FatureCurrent.ExploitationList;
                                                            IsBusy = false;

                                                        

                                                            StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                                            messageBox.Owner = Application.Current.MainWindow;
                                                            messageBox.Title = ConstStrings.Get("factureVM_msgNewfacture");
                                                            messageBox.ViewModel.Message = string.Format("Facture :{0}  Mise jour", FatureCurrent.NumeroFacture);


                                                            Utils.logUserActions(string.Format("<--UI edition facture  mise jour facture [{0}] éditer par : {1}  ", FatureCurrent.NumeroFacture, UserConnected.Nom), "");
                                                            messageBox.ShowDialog();
                                                            IsOperationClosing = true;
                                                            // loadFactureInformation();
                                                        #endregion

                                                        }

                                                    }
                                                    catch (Exception ex)
                                                    {
                                                        CustomExceptionView view = new CustomExceptionView();
                                                        view.Owner = Application.Current.MainWindow;
                                                        view.Title = ConstStrings.Get("factureVM_Updatefacturetitre");
                                                        view.ViewModel.Message = ConstStrings.Get("factureVM_UpdatefactureErrorMsg") + ex.Message;
                                                        IsOperationClosing = false;

                                                        IsBusy = false;
                                                        this.MouseCursor = null;
                                                        Utils.logUserActions(string.Format("<-- ;55;Ui Edition facture : -- Erreur modification facture Num: {0} client :{1} crée par : {2}-- {3}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom, ex.Message), "");
                                                        view.ShowDialog();
                                                    }
                                            

                                            
                                        }



                                        #endregion

                                    }
                                    else if (valeurDalidation == "avoir")
                                    {

                                        #region Facture Avoir


                                       
                                            try
                                            {
                                                if (LigneCommandList == null)
                                                {
                                                    MessageBox.Show("Cette facture ne possede pas de Lignes commande", "NOTIFICATION", MessageBoxButton.OK, MessageBoxImage.Error);
                                                    return;
                                                }
                                                
                                                FatureCurrent.TotalTTC = FtotalTTC;
                                                FatureCurrent.TotalHT = FTotalHt;
                                                FatureCurrent.TotalTVA = Ftotaltva;
                                                FatureCurrent.TotalPRORATA = FTotalProrata;

                                                if (CostTaxeSelected != null)
                                                {
                                                    FatureCurrent.MaregeBeneficiaireId = CostTaxeSelected.ID_Taxe;
                                                    FatureCurrent.TotalMarge = MontantMargeBenef;
                                                    FatureCurrent.TauxMargeBeneficiaire = CostTaxeSelected.Taux;
                                                }

                                                if (LigneCommandList != null)
                                                    listItems = TraitementLigneCommande();
                                                else return;

                                                var dep = depService.Departemnt_SELECTById(1);
                                                if (dep != null)
                                                {
                                                    FatureCurrent.IdDepartement = dep.IdDep;
                                                    FatureCurrent.LibelleDepartement = dep.Libelle;
                                                }

                                                    FatureCurrent.ExploitationIds = ListExpid;
                                                    FatureCurrent.ExploitationList = Listexp;
                                                    FatureCurrent.IdExploitation = ExploitationSelected == null ? ExploitationSelected.IdExploitation : ExploitationSelected.IdExploitation;

                                                    if (factureservice.FACTURE_UPDATE(FatureCurrent))
                                                    {
                                                        isfactureUpdate = true;
                                                        foreach (var lt in listItems)
                                                        {
                                                            lt.IdStatut = FatureCurrent.IdStatut;
                                                            ligneFactureService.LIGNE_FACTURE_ADD(lt);
                                                        }
                                                        isligneFactureUpdate = true;
                                                        if (ListExpid != null)
                                                        {
                                                            string[] idsexp = ListExpid.Split(new char[] { ';' });
                                                            if (idsexp.Length > 0)
                                                            {
                                                                for (int i = 0; i < idsexp.Length; i++)
                                                                    exploitationservice.EXPLOITATION_FACTUREE_ADD(FatureCurrent.IdFacture, int.Parse(idsexp[i]), 1);
                                                            }
                                                        }

                                                        FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);

                                                        ListExpid = FatureCurrent.ExploitationIds;
                                                        Listexp = FatureCurrent.ExploitationList;

                                                        //OverviewFactureModel overview = new OverviewFactureModel();
                                                        //overview.Idice = 1;
                                                        //overview.Idfacture = FatureCurrent.IdFacture;
                                                        //overview.IdClient = FatureCurrent.IdClient;
                                                        //overview.Iduser = UserConnected.Id;

                                                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                                        messageBox.Owner = Application.Current.MainWindow;
                                                        messageBox.Title = "MISE JOUR FACTURE Avoir";
                                                        messageBox.ViewModel.Message = string.Format("Facture Avoir :{0}  Mise jour", FatureCurrent.NumeroFacture);
                                                        messageBox.ShowDialog();

                                                        FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                                        IsOperationClosing = true;
                                                        Utils.logUserActions(string.Format("<--2; UI Edition facture : --  modification facture avoir Num: {0} client :{1} crée par : {2}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom), "");
                                                    }
                                                

                                            }
                                            catch (Exception ex)
                                            {
                                                CustomExceptionView view = new CustomExceptionView();
                                                view.Owner = Application.Current.MainWindow;
                                                view.Title = "MESSAGE ERREURE MODIFICATION FACTURE AVOIR";
                                                view.ViewModel.Message = "Erreure <<10008>> Echec  Modification  facture avoir " + ex.Message;
                                                IsOperationClosing = false;
                                                IsBusy = false;
                                                this.MouseCursor = null;
                                                Utils.logUserActions(string.Format("<-- ;55;Ui Edition facture : -- Erreur modification facture avor Num: {0} client :{1} crée par : {2}-- {3}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom, ex.Message), "");
                                                view.ShowDialog();
                                            }



                                        #endregion

                                    }
                                   
                                    break;
                                }


                            case "sortie":
                                {
                                    try
                                    {

                                    StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                    messageBox.Owner = Application.Current.MainWindow;
                                    messageBox.Title = "INFORMATION MODIFICATION STATUT FACTURE";
                                    messageBox.ViewModel.Message = "Valider cette sortie  à la date courante  ?";
                                    if (messageBox.ShowDialog().Value == true)
                                    {

                                        DataTable dtblVersion = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
                                        if (versionFactureCurrent != null && versionFactureCurrent.Rows.Count > 0)
                                        {
                                            if (versionFactureCurrent.Rows[0]["version"] != DBNull.Value && dtblVersion.Rows[0]["version"] != DBNull.Value)
                                            {
                                                DateTime oldTimeVersion = Convert.ToDateTime(versionFactureCurrent.Rows[0]["version"]);
                                                DateTime newTimeVersion = Convert.ToDateTime(dtblVersion.Rows[0]["version"]);

                                                TimeSpan differenceVersion = oldTimeVersion - newTimeVersion;
                                                if (differenceVersion.TotalMilliseconds != 0)
                                                {
                                                    StyledMessageBoxView messageBoxs = new StyledMessageBoxView();
                                                    messageBoxs.Owner = Application.Current.MainWindow;
                                                    messageBoxs.Title = "INFORMATION  ACCESS CONCURRENTIEL";
                                                    messageBox.ViewModel.Message = string.Format("Cette facture vient de faire l' objet d'une modification par \n l'utilisateur :  {0},  date modificaion {1} \n statut courant : {2} \n voulez vous néamoins modifier ", dtblVersion.Rows[0]["Nom"].ToString() + " " + dtblVersion.Rows[0]["Prenom"].ToString(), dtblVersion.Rows[0]["Date_Modification"].ToString(), dtblVersion.Rows[0]["statuts"].ToString());
                                                    if (messageBoxs.ShowDialog().Value == false)
                                                    {
                                                        isReloadFacture = false;
                                                        issaveUpdatedata = false;
                                                        IsOperation = false;
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                        // StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");
                                        factureservice.FACTURE_SORTIE(FatureCurrent.IdFacture, DateTime.Now, CurrentStatut.IdStatut, UserConnected.Id, true);

                                        //for (int i = 0; i < 50; i += 5)
                                        //    Thread.Sleep(100);

                                        StyledMessageBoxView messageBoxd = new StyledMessageBoxView();
                                        messageBoxd.Owner = Application.Current.MainWindow;
                                        messageBoxd.Title = "MISE JOUR FACTURE";
                                        messageBoxd.ViewModel.Message = string.Format("Facture :{0}  Mise jour Statut Sortie", FatureCurrent.NumeroFacture);
                                        messageBoxd.ShowDialog();

                                        FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                        isReloadFacture = true;
                                        ActualFacture = null;
                                        IsBusy = false;
                                        issaveUpdatedata = false;
                                        IsOperationClosing = true;
                                        //loadFactureInformation();
                                    }

                                    //}
                                    // else MessageBox.Show("Vous navez pas assez de droits pour effectuer cette Operation", "PROBLEME DE DROITS", MessageBoxButton.OK, MessageBoxImage.Warning);
                                    Utils.logUserActions(string.Format("<-- edition facture  update facture [{0}] modification statut << statut sortie>> éditer par : {1}  ", FatureCurrent.IdFacture, UserConnected.Nom), "");

                                      }
                                      catch (Exception ex)
                                      {
                                          CustomExceptionView view = new CustomExceptionView();
                                          view.Owner = Application.Current.MainWindow;
                                          view.Title = "MESSAGE ECHEC MOIFICATION STATUT FACTURE";
                                          view.ViewModel.Message = "Erreure <<10006>> Echec  modification du statut  facture " + ex.Message;
                                          Utils.logUserActions(string.Format("<-- ;55;Ui Edition facture : -- Erreur modification facture statut sortie: {0} client :{1} crée par : {2}-- {3}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom, ex.Message), "");
                                          view.ShowDialog();
                                          IsBusy = false;
                                          this.MouseCursor = null;
                                      }

                                    break;
                                }
                            case "supension":
                                {
                                   try{

                                       StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                    messageBox.Owner = Application.Current.MainWindow;
                                    messageBox.Title = "INFORMATION MODIFICATION STATUT FACTURE";
                                    messageBox.ViewModel.Message = "Valider cette  Suspension   ?";
                                    if (messageBox.ShowDialog().Value == true)
                                    {
                                        DataTable dtblVersion = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
                                        if (versionFactureCurrent != null && versionFactureCurrent.Rows.Count > 0)
                                        {
                                            if (versionFactureCurrent.Rows[0]["version"] != DBNull.Value && dtblVersion.Rows[0]["version"] != DBNull.Value)
                                            {
                                                DateTime oldTimeVersion = Convert.ToDateTime(versionFactureCurrent.Rows[0]["version"]);
                                                DateTime newTimeVersion = Convert.ToDateTime(dtblVersion.Rows[0]["version"]);

                                                TimeSpan differenceVersion = oldTimeVersion - newTimeVersion;
                                                if (differenceVersion.TotalMilliseconds != 0)
                                                {
                                                    StyledMessageBoxView messageBoxs = new StyledMessageBoxView();
                                                    messageBoxs.Owner = Application.Current.MainWindow;
                                                    messageBoxs.Title = "INFORMATION  ACCESS CONCURRENTIEL";
                                                    messageBoxs.ViewModel.Message = string.Format("Cette facture vient de faire l' objet d'une modification par \n l'utilisateur :  {0},  date modificaion {1} \n statut courant : {2} \n voulez vous néamoins modifier ", dtblVersion.Rows[0]["Nom"].ToString() + " " + dtblVersion.Rows[0]["Prenom"].ToString(), dtblVersion.Rows[0]["Date_Modification"].ToString(), dtblVersion.Rows[0]["statuts"].ToString());
                                                    if (messageBoxs.ShowDialog().Value == false)
                                                    {
                                                        isReloadFacture = false;
                                                        issaveUpdatedata = false;
                                                        IsOperation = false;
                                                        return;
                                                    }
                                                }
                                            }
                                        }
                                        //newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "3");
                                        factureservice.FACTURE_SUSPENSION(FatureCurrent.IdFacture, FatureCurrent.IdStatut, UserConnected.Id, true);

                                        FatureCurrent.LibelleUserNom = UserConnected.Nom;
                                        FatureCurrent.LibelleUserPrenom = UserConnected.Prenom;
                                        if (CostTaxeSelected != null)
                                            FatureCurrent.TauxMargeBeneficiaire = CostTaxeSelected.Taux;




                                        StyledMessageBoxView messageBoxr = new StyledMessageBoxView();
                                        messageBoxr.Owner = Application.Current.MainWindow;
                                        messageBoxr.Title = "MISE JOUR STATUT FACTURE";
                                        messageBoxr.ViewModel.Message = string.Format("Facture :{0} Suspendu ", FatureCurrent.NumeroFacture);
                                        messageBoxr.ShowDialog();
                                        IsOperationClosing = true;
                                    }

                                   }
                                   catch (Exception ex)
                                   {
                                       CustomExceptionView view = new CustomExceptionView();
                                       view.Owner = Application.Current.MainWindow;
                                       view.Title = "MESSAGE ERREUR MISE JOUR FACTURE";
                                       view.ViewModel.Message = "Erreure <<10005>> Echec modification statut supendu de la  facture " + ex.Message;
                                      // Utils.logUserActions(string.Format("<-- ;55;Ui Edition facture : -- Erreur modification statut facture Suspendu : {0} client :{1} crée par : {2}-- {3}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom, ex.Message), "");
                                       view.ShowDialog();

                                       Utils.logUserActions(string.Format("<-- ;55;Ui Edition facture; Erreur lors du changement statut facture <<suspension>>  facture par : {0} ;{1}  ", UserConnected.Nom, ex.Message), "");
                                 


                                       IsBusy = false;
                                       this.MouseCursor = null;
                                   }
                                    break;
                                }

                               case "nonvalable":
                                {
                                    try { 
                                     StyledMessageBoxView messageBox = new StyledMessageBoxView();
                                    messageBox.Owner = Application.Current.MainWindow;
                                    messageBox.Title = "INFORMATION MODIFICATION STATUT FACTURE";
                                    messageBox.ViewModel.Message = "Valider cette Facture Non Valable ?";
                                    if (messageBox.ShowDialog().Value == true)
                                    {
                                        DataTable dtblVersion = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
                                        if (versionFactureCurrent != null && versionFactureCurrent.Rows.Count > 0)
                                        {
                                            if (versionFactureCurrent.Rows[0]["version"] != DBNull.Value && dtblVersion.Rows[0]["version"] != DBNull.Value)
                                            {
                                                DateTime oldTimeVersion = Convert.ToDateTime(versionFactureCurrent.Rows[0]["version"]);
                                                DateTime newTimeVersion = Convert.ToDateTime(dtblVersion.Rows[0]["version"]);

                                                TimeSpan differenceVersion = oldTimeVersion - newTimeVersion;
                                                if (differenceVersion.TotalMilliseconds != 0)
                                                {
                                                    StyledMessageBoxView messageBoxs = new StyledMessageBoxView();
                                                    messageBoxs.Owner = Application.Current.MainWindow;
                                                    messageBoxs.Title = "INFORMATION  ACCESS CONCURRENTIEL";
                                                    messageBoxs.ViewModel.Message = string.Format("Cette facture vient de faire l' objet d'une modification par \n l'utilisateur :  {0},  date modificaion {1} \n statut courant : {2} \n voulez vous néamoins modifier ", dtblVersion.Rows[0]["Nom"].ToString() + " " + dtblVersion.Rows[0]["Prenom"].ToString(), dtblVersion.Rows[0]["Date_Modification"].ToString(), dtblVersion.Rows[0]["statuts"].ToString());
                                                    if (messageBoxs.ShowDialog().Value == false)
                                                    {
                                                        isReloadFacture = false;
                                                        issaveUpdatedata = false;
                                                        IsOperation = false;
                                                        return;
                                                    }
                                                }
                                            }
                                        }

                                        //newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "5");
                                        factureservice.FACTURE_NONVALABLE(FatureCurrent.IdFacture, CurrentStatut.IdStatut, UserConnected.Id, true);


                                        StyledMessageBoxView messageBoxy = new StyledMessageBoxView();
                                        messageBoxy.Owner = Application.Current.MainWindow;
                                        messageBoxy.Title = "MISE JOUR STATUT FACTURE";
                                        messageBoxy.ViewModel.Message = string.Format("Facture :{0}  au statut non valable", FatureCurrent.NumeroFacture);
                                        messageBoxy.ShowDialog();


                                        isReloadFacture = true;
                                        issaveUpdatedata = false;
                                        FatureCurrent = factureservice.GET_FACTURE_BYID(FatureCurrent.IdFacture);
                                        IsOperationClosing = true;
                                        ActualFacture = null;
                                        IsBusy = false;
                                    }

                                    }
                                    catch (Exception ex)
                                    {
                                        CustomExceptionView view = new CustomExceptionView();
                                        view.Owner = Application.Current.MainWindow;
                                        view.Title = "MESSAGE ERREUR MISE JOUR FACTURE";
                                        view.ViewModel.Message = "Erreure <<10006>> Echec modification au statut Non Valable  facture " + ex.Message;
                                       // Utils.logUserActions(string.Format("<-- ;55;Ui Edition facture : -- Erreur modification sataut Non valable Num: {0} client :{1} crée par : {2}-- {3}  ", FatureCurrent.NumeroFacture, FatureCurrent.CurrentClient.NomClient, UserConnected.Nom, ex.Message), "");
                                        Utils.logUserActions(string.Format("<-- ;55;Ui Edition facture; Erreur lors du changement statut facture <<Non valide>>  facture par : {0} ;{1} ", UserConnected.Nom, ex.Message), "");
                                   
                                        view.ShowDialog();
                                        IsBusy = false;
                                        this.MouseCursor = null;
                                    }
                                    break;
                                }
                          

                        }
                        break;
                       
                    }

              
            }
           
               
        }
            
        

        #endregion

        #region FACTURE SUPPRESSION

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
                    //if (CacheLigneCommandList != null)
                    //    foreach (var ligne in CacheLigneCommandList)
                    //        _ligneFacture.LIGNE_FACTURE_DELETE(ligne.ID);

                    factureservice.FACTURE_DELETE(FatureCurrent.IdFacture, FatureCurrent.IdStatut==14007?1:0, societeCourante.IdSociete, userConnected.Id);
                    //StatutModel newStatut = statutservice.STATUT_FACTURE_GETLISTE().First(f => f.CourtDesc == "5");
                    //factureservice.FACTURE_SUSPENSION(FatureCurrent.IdFacture, newStatut.IdStatut, UserConnected.Id, true);
                   
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
                    IsOperation = true;
                    isDeleteFacture = true;
                    this.IsBusy = false;
                    CacheDatas.lastUpdatefacture = null;
                    Utils.logUserActions(string.Format("<-- edition facture  update facture [{0}] , suppresion  par : {1}  ", FatureCurrent.IdFacture, UserConnected.Nom), "");
                    IsOperationClosing = true;
                    Communicator com = new Communicator();
                    EventArgs e2 = new EventArgs();
                    com.OnChangeCloseWindow(e2);

                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "Message SUPPRESSION FACTURE";
                    view.ViewModel.Message = "Erreure <<10005>> Problème survenu lors de la tentative de suppression de la facture";
                    view.ShowDialog();
                    IsBusy = false;
                    IsOperationClosing = false;
                    this.MouseCursor = null;
                    Utils.logUserActions(string.Format("<-- edition facture Erreur - fatal de suppression facture : {0} par : {1}  ", FatureCurrent.IdFacture, UserConnected.Nom), "");
                
                }


            }

        }

        bool canExecuteDeletefacture()
        {
            bool values = false;

            //if (societeCourante.IdSociete == GlobalDatas.DefaultCompany.IdSociete)
            //{
            if (droitFormulaire.Developpeur || droitFormulaire.Suppression  )
            {
                if (FatureCurrent != null)
                {
                    if (FatureCurrent.IdFacture > 0)
                    {
                        if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2 || int.Parse(FatureCurrent.CurrentStatut.CourtDesc)==7)
                        {
                            values = true;
                        }
                        else
                        {
                            if (droitFormulaire.Developpeur)
                                values = true;
                            else
                                values = false;
                        }

                    }
                }
                else
                    values = false;

               

                if (GlobalDatas.IsArchiveSelected)
                    values = false;
            }
            return values;
            //return FatureCurrent != null ?
            //    (FatureCurrent.IdFacture > 0 ? (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) < 2 ? ((droitFormulaire.Developpeur || droitFormulaire.Suppression) ? true : false) : false) : false) : false;
        }
        #endregion

        #region FACTURE IMPRESSION

        private void canPrint()
        {
            IsBusy = true;
            string mode = string.Empty;
            try
            {
                if (GlobalDatas.IsArchiveSelected)
                {
                    DataSet tablefacture = ReportModel.GetFacture_archive(FatureCurrent.IdFacture, FatureCurrent.IdSite);
                    DataTable tclient = ReportModel.GetReportClientArchive(FatureCurrent.IdClient, FatureCurrent.IdFacture);
                    DataTable tableSociete = ReportModel.GetReportSocieteArchive(FatureCurrent.IdSite);
                    DataTable tablePiedPage = ReportModel.GetReporPiedPage(FatureCurrent.IdSite);
                    DataTable tableLibelle = ReportModel.GetLibelleArchive(FatureCurrent.IdClientLangue);

                    //DataTable tableLignefacture = ReportModel.GetLigneFacture(FactureSelected.IdFacture);
                    ModalReport view = new ModalReport(tablefacture.Tables[0], tablefacture.Tables[1], tclient, tableSociete, tablePiedPage, tableLibelle);
                    view.ShowDialog();
                }
                else
                {


               if (FatureCurrent.IdStatut >= 14003 )
                {

                   DataSet tablefacture = ReportModel.GetFactureNew(FatureCurrent.IdFacture, FatureCurrent.IdSite, (FatureCurrent.IdStatut==14007?1:0));

                    DataTable tclient = ReportModel.GetReportClientArchive(FatureCurrent.IdClient, FatureCurrent.IdFacture);
                    DataTable tableSociete = ReportModel.GetReportSocieteArchive(FatureCurrent.IdSite);
                    DataTable tablePiedPage = ReportModel.GetReporPiedPage(FatureCurrent.IdSite);
                    DataTable tableLibelle = ReportModel.GetLibelleArchive(FatureCurrent.IdClientLangue);

                 
                     ModalReport view = new ModalReport(tablefacture.Tables[0], tablefacture.Tables[1], tclient, tableSociete, tablePiedPage, tableLibelle);
                    view.ShowDialog();
            


                }
                else
                {
                    DataTable tclient = ReportModel.GetReportClient(FatureCurrent.IdClient);
                    DataTable tableSociete = ReportModel.GetReportSociete(FatureCurrent.IdSite);
                    DataTable tablePiedPage = ReportModel.GetReporPiedPage(FatureCurrent.IdSite);
                    DataTable tableLibelle = ReportModel.GetLibelle(FatureCurrent.CurrentClient.IdLangue);
                    DataTable tablefacture = ReportModel.GetFacture(FatureCurrent.IdFacture);

                    if (FatureCurrent.CurrentClient.Exonerere == null)
                    {
                        ExonerationModel exo = new ExonerationModel();
                        mode = exo.EXONERATION_SELECTById(FatureCurrent.CurrentClient.IdExonere).CourtDesc;
                    }
                    else mode = FatureCurrent.CurrentClient.Exonerere.CourtDesc;


                        DataTable tableLignefacture = ReportModel.GetLigneFacture(FatureCurrent.IdFacture);
                        ModalReport view = new ModalReport(tablefacture, tableLignefacture, tclient, tableSociete, tablePiedPage, tableLibelle);
                       // formeExonereNonExoner vf = new formeExonereNonExoner(tclient, tableSociete, tablePiedPage, tablefacture, tableLignefacture, tableLibelle, 1);
                        view.ShowDialog();
                    
                }
                }

                this.IsBusy = false;
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "MESSAGE IMPRESSION FACTURE";
                view.ViewModel.Message ="Erreur <<10003>> Problème survenu lors de l'ipression ";
                Utils.logUserActions(string.Format("<--10003 ; Impression facture [{0}] ,  Ereure : {1}  ", FatureCurrent.IdFacture, ex.Message), "");
                view.ShowDialog();
                IsBusy = false;
                this.MouseCursor = null;
            }
        }

        bool canExecutePrint()
        {
            bool values = false;

            if (droitFormulaire.Developpeur || droitFormulaire.Impression  )
            {
                if (FatureCurrent != null)
                    if (FatureCurrent.IdFacture > 0)
                        values = true;

            }
            return values;
        }

        #endregion

        #region LIGNE FACTURE SUPPRESSION


        private void canDeleteLine()
        {
            string modeExoneration = string.Empty;

            if (FatureCurrent.CurrentClient != null)
                modeExoneration = FatureCurrent.CurrentClient.Exonerere.CourtDesc;
            else modeExoneration = clientCourant.Exonerere.CourtDesc;

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
                            LigneFacture = new LigneFactureModel(); ;
                            isupdateinvoice = true;
                            PrixUnitaireselected = 0;
                            isupdateinvoice = false;
                            Lignecourante = null;
                            ProduitSelected = null;
                            IndexProduit = -1;
                            Communicator com = new Communicator();
                            EventArgs e2 = new EventArgs();
                            com.OnChangeClearQuantity(e2);

                            #region NEW Total Facture

                            double valprorata = 0;
                            double valTva = 0;
                            if (LigneCommandList.Count > 0)
                            {
                                FtotalTTC = 0;
                                FTotalProrata = 0;
                                Ftotaltva = 0;
                                FTotalHt = 0;
                                totalihne_ht_prorata = 0;
                                totaligne_ht_tva = 0;

                                switch (modeExoneration)
                                {
                                    case "exo":
                                        {
                                            FTotalHt = (double)LigneCommandList.Sum(l => l.montantHt);
                                            Montanttva = string.Format("({0}) {1}", (clientCourant.IdLangue == 1 ? "Exonere" : "Exonerate"), TaxeSelected.Taux);

                                            if (TaxePorataSelected.ID_Taxe > 0)
                                            {
                                              
                                                MontantProrata = TaxePorataSelected.Taux;
                                                FTotalProrata = 0;
                                            }
                                            else
                                            {
                                                MontantProrata = "";
                                                FTotalProrata = 0;
                                            }
                                            FtotalTTC =  FTotalHt;
                                            break;
                                        }

                                    case "non":
                                        {
                                            FTotalHt = (double)LigneCommandList.Sum(l => l.montantHt);
                                            valTva = GetTaxeFr(TaxeSelected.Taux, currentCulture) * FTotalHt;
                                            valTva = Math.Round(valTva, 2);
                                            Montanttva = TaxeSelected.Taux;
                                            Ftotaltva = valTva;

                                            // calcule centimes
                                            var centime = GetTaxeFr(TaxePorataSelected.Taux, currentCulture) * Ftotaltva;
                                            FTotalProrata = Math.Round(centime, 2);
                                            MontantProrata = TaxePorataSelected.Taux;


                                            FtotalTTC = valTva + FTotalHt + FTotalProrata;
                                            break;
                                        }

                                   
                                }

                                if (CostTaxeSelected != null)
                                {
                                    TauxMargeBenef = CostTaxeSelected.Taux;
                                    MontantMargeBenef = GetTaxeFr(CostTaxeSelected.Taux, currentCulture) * FTotalHt;
                                    FtotalTTC += MontantMargeBenef;
                                }

                           
                            }
                            else
                            {
                                FtotalTTC = 0;
                                FTotalProrata = 0;
                                Ftotaltva = 0;
                                FTotalHt = 0;

                                MontantProrata = "";
                                Montanttva = "";
                                TauxMargeBenef = "";
                                MontantMargeBenef = 0;

                                ModeFacturenormaleEnable = true;
                                ModeFactureAvoirEnable = true;
                               
                            }

                            #endregion

                            IsOperationClosing = false;
                            isFactureOperation = true;
                            isligneitemsoperation = true;
                            isFactureOperation = true;

                        
                        }
                    }
                }
                else
                {
                    if (droitFormulaire.Developpeur || droitFormulaire.Suppression )
                    {


                        StyledMessageBoxView messageBox = new StyledMessageBoxView();
                        messageBox.Owner = Application.Current.MainWindow;
                        messageBox.Title = " INFORMATION SUPPRESSION LIGNE FACTURE";
                        messageBox.ViewModel.Message = "Voulez vous Supprimer Cette Ligne de Facture";
                        if (messageBox.ShowDialog().Value == true)
                        {
                            try
                            {
                                //LigneCommandList
                               // if (LigneCommandList.Count > 1)
                                List<LigneFactureModel> newliste = null;

                                    ligneFactureService.LIGNE_FACTURE_DELETE(OldLigneFacture.IdLigneFacture, 0);
                                     newliste = ligneFactureService.LIGNE_FACTURE_BYIDFActure(FatureCurrent.IdFacture);
                                

                                    //LigneCommand cm = LigneCommandList.Find(de => de.ID == OldLigneFacture.IdLigneFacture);
                                   ConvertDataItems(newliste);
                                    LigneFacture = new LigneFactureModel(); 

                                    isupdateinvoice = true;
                                    PrixUnitaireselected = 0;
                                    isupdateinvoice = false;
                                    Lignecourante = null;
                                    ProduitSelected = null;
                                    IsOperationClosing = false;
                                    IndexProduit = -1;
                                    Communicator com = new Communicator();
                                    EventArgs e2 = new EventArgs();
                                    com.OnChangeClearQuantity(e2);

                                    if (newliste.Count > 0)
                                    {
                                        switch (modeExoneration)
                                        {
                                            case "exo":
                                                {
                                                    FTotalHt = (double)LigneCommandList.Sum(l => l.montantHt);
                                                    Montanttva = string.Format("({0}) {1}", (clientCourant.IdLangue == 1 ? "Exonere" : "Exonerate"), TaxeSelected.Taux);
                                                    if (TaxePorataSelected.ID_Taxe > 0)
                                                    {
                                                       
                                                        MontantProrata = TaxePorataSelected.Taux;
                                                        FTotalProrata = 0;
                                                    }
                                                    else
                                                    {
                                                        MontantProrata = "";
                                                        FTotalProrata = 0;
                                                    }
                                                    FtotalTTC = FTotalProrata + FTotalHt;
                                                    break;
                                                }

                                            case "non":
                                                {
                                                    FTotalHt = (double)LigneCommandList.Sum(l => l.montantHt);
                                                    double valTva = GetTaxeFr(TaxeSelected.Taux, currentCulture) * FTotalHt;
                                                    valTva = Math.Round(valTva, 2);
                                                    Montanttva = TaxeSelected.Taux;
                                                    Ftotaltva = valTva;

                                                    // calcule centimes
                                                    var centime = GetTaxeFr(TaxePorataSelected.Taux, currentCulture) * Ftotaltva;
                                                    FTotalProrata += Math.Round(centime, 2);
                                                    MontantProrata = TaxePorataSelected.Taux;

                                                    MontantProrata = "";
                                                    FTotalProrata = 0;
                                                    FtotalTTC = valTva + FTotalHt;
                                                    break;
                                                }

                                           
                                        }

                                      

                                        if (CostTaxeSelected != null)
                                        {
                                            TauxMargeBenef = CostTaxeSelected.Taux;
                                            MontantMargeBenef = GetTaxeFr(CostTaxeSelected.Taux, currentCulture) * FTotalHt;
                                            FtotalTTC += MontantMargeBenef;
                                        }
                                    }
                                    else
                                    {
                                        FtotalTTC = 0;
                                        FTotalProrata = 0;
                                        Ftotaltva = 0;
                                        FTotalHt = 0;

                                        MontantProrata = "";
                                        Montanttva = "";
                                        TauxMargeBenef = "";
                                        MontantMargeBenef = 0;

                                        ModeFacturenormaleEnable = true;
                                        ModeFactureAvoirEnable = true;
                                    }

                                  isligneitemsoperation = true;
                               
                                  isFactureOperation = true;
                            }
                            catch (Exception ex)
                            {
                                CustomExceptionView view = new CustomExceptionView();
                                view.Owner = Application.Current.MainWindow;
                                view.Title = "ERREURE DE SUPPRESSION";
                                view.ViewModel.Message ="Erreure <<10004>> probléme survenu lors de la tentative de suppression de ce item";
                                Utils.logUserActions(string.Format("<--10004 ; tentative suppression ligne facture [{0}] ,  Ereure : {1}  ", OldLigneFacture.IdLigneFacture, ex.Message), "");
                                view.ShowDialog();

                            }
                        }

                       
                    }
                    else
                        MessageBox.Show("Pas assez de privileges pour effectuer cette opération", "PROBLEME DE DROITS", MessageBoxButton.OK, MessageBoxImage.Error);

                }

            }

        }

        bool canExecuteDeleteline()
        {
            bool values = false;
            if (droitFormulaire.Developpeur || droitFormulaire.Suppression)
            {
               
                string mode = string.Empty;

                if (FatureCurrent != null && FatureCurrent.IdFacture == 0)
                    values = true;

                if (OldLigneFacture != null && (FatureCurrent.IdStatut <= 14002 || FatureCurrent.IdStatut == 14007))
                    values = true;
                else values = false;

                if (GlobalDatas.IsArchiveSelected)
                    values = false;


              
            }
            return values;
        }
        #endregion

        #region FACTURE DESTRUCTION FACTURE

        void canDestroy()
        {
            StyledMessageBoxView messageBox = new StyledMessageBoxView();
            messageBox.Owner = Application.Current.MainWindow;
            messageBox.Title = "INFORMATION  SUPPRESSION DEFINITIVE";
            messageBox.ViewModel.Message = "Voulez vous supprimer définitvement cette facture?";
            if (messageBox.ShowDialog().Value == true)
            {

                try
                {
                    this.IsBusy = true;

                    factureservice.FACTURE_DELETE(FatureCurrent.IdFacture, 1, societeCourante.IdSociete, UserConnected.Id );
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
                    CacheDatas.lastUpdatefacture = null;
                    IsOperation = true;
                    isDeleteFacture = true;
                    Utils.logUserActions(string.Format("<-- edition facture   suppression definitive de la facture [{0}] par : {1}  ", FatureCurrent.IdFacture, UserConnected.Nom), "");
                    IsOperationClosing =false;
                }
                catch (Exception ex)
                {
                    CustomExceptionView view = new CustomExceptionView();
                    view.Owner = Application.Current.MainWindow;
                    view.Title = "Message SUPPRESSION DEFINITIVE FACTURE";
                    view.ViewModel.Message = "Erreure <<10005>> Problème survenu lors de la tentative de suppression definitve de la facture";
                    Utils.logUserActions(string.Format("<--10005 ; tentative suppression facture [{0}] ,  Ereure : {1}  ", FatureCurrent.IdFacture, ex.Message), "");
                    view.ShowDialog();
                    IsBusy = false;
                    this.MouseCursor = null;
                }

            }

        }

        bool canExecuteDestroy()
        {
            bool values = false;
            if (droitFormulaire.Developpeur || droitFormulaire.Suppression )
            {
                if (FatureCurrent != null)
                {
                    if (FatureCurrent.IdFacture > 0)
                    {
                        if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) < 2)
                        {
                            if (droitFormulaire.Developpeur  || droitFormulaire.Proprietaire)
                                values = true;
                            else
                                values = false;

                        }
                        else
                        {
                            if (droitFormulaire.Super)
                                values = true;
                            else
                                values = false;
                        }

                    }
                }
                else
                    values = false;
            }

            if (GlobalDatas.IsArchiveSelected)
                values = false;

            return values;
        }
        #endregion

        private void canClose()
        {
        }

        private void canAddData()
        {

        }

        bool canExecuteAddDatas()
        {
            return true;
        }

        private void canObjectselected(object param)
        {
            object val = param;
        }






        #endregion


        #region Helper


        void Loadversion()
        {
            versionFactureCurrent = FactureModel.ModelFactureVersion(FatureCurrent.IdFacture);
        }


        void LoadDepartement()
        {
            if (!isReloadFacture)
                DepartementList = depService.Departement_SELECT(societeCourante.IdSociete);
        }


        void LoadClient()
        {
            if (issaveUpdatedata)
                if (!isReloadFacture)
                    ClientList = clientservice.CLIENT_GETLISTE(FatureCurrent.IdSite, true);
        }

        void LoadDevise()
        {
            if (!isReloadFacture)
            {
                if (FatureCurrent.CurrentClient.DeviseConversion == null)
                    DeviseConvert = deviseService.Devise_SELECTById(FatureCurrent.CurrentClient.IdDeviseConversion, societeCourante.IdSociete);
                else DeviseConvert = FatureCurrent.CurrentClient.DeviseConversion;
            }
        }
        void LoadObjet()
        {
            if (!isReloadFacture)
                ObjetList = objetservice.OBJECT_FACTURE_GETLISTEByCLIENT(FatureCurrent.IdSite, FatureCurrent.IdClient);

        }
        void LoadExploitation()
        {
            if (!isReloadFacture)
            {
                try
                {
                    ExploitationFactureModel newexploitation = new ExploitationFactureModel { IdExploitation = 0, Libelle = "Aucune", IdSite = societeCourante.IdSociete };
                    ExploitationList = exploitationservice.EXPLOITATION_FACTURE_CLIENT_LIST(FatureCurrent.IdSite, FatureCurrent.IdClient);
                    ExploitationList.Add(newexploitation);
                }
                catch (Exception ex)
                {
                    throw new Exception("Chargement exploitation "+ex.Message);
                }

            }

        }
       

        void LoadProduit(int idClient)
        {
            try
            {
                NewDetailListeProduit = detailService.DETAIL_PRODUIT_BYCLIENT(FatureCurrent.IdClient, FatureCurrent.IdSite);

                if (NewDetailListeProduit != null && NewDetailListeProduit.Count > 0)
                {
                    IdProduits = new HashSet<int>();
                    CachnewDetailListeProduit = new List<DetailProductModel>();
                    foreach (var lst in NewDetailListeProduit)
                        CachnewDetailListeProduit.Add(lst);

                    foreach (DetailProductModel det in NewDetailListeProduit)
                        IdProduits.Add(det.IdProduit);

                    ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
                    foreach (int id in IdProduits)
                        newListeProduie.Add(NewDetailListeProduit.Find(p => p.IdProduit == id).Produit);
                    ProduiList = newListeProduie;
                    CacheProduiList = ProduiList;
                    IndexProduit = -1;

                    if (FatureCurrent.CurrentStatut != null && int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2)
                    {
                        DetailProdEnable = true;
                        IsEnableCmbProduit = true;
                        IsdetailExiste = true;
                    }
                    else
                    {
                        DetailProdEnable = true;
                        IsEnableCmbProduit = true;
                        IsdetailExiste = true;
                    }
                }
                else
                {
                    throw new Exception("les produits de cette facture N'ont pas put se charger\n si le problème persite merci de contactez l'Administrateur");
                    // MessageBox.Show("Ce Client ne Posssède Plus de produits rattachés, merci de le faire d'abord", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        void LoadCLignefactures()
        {
            LigneFatureListe = ligneFactureService.LIGNE_FACTURE_BYIDFActure(FatureCurrent.IdFacture);
        }
      
        void LoadLignefactureArchivevalidate()
        {
            if (GlobalDatas.IsArchiveSelected)
            {
                LigneFatureListe = ligneFactureService.LIGNE_FACTURE_BYIDFActure_Archive(FatureCurrent.IdFacture);
            }
            else
            {
                LigneFatureListe = ligneFactureService.LIGNE_FACTURE_BYIDFActure_Validate(FatureCurrent.IdFacture);
            }
        }
        void LoadDeviseClientArchive()
        {
            if (!isReloadFacture)
            {
                if (FatureCurrent.CurrentClient.DeviseConversion == null)
                    DeviseConvert = deviseService.Devise_ArchiveSELECTById(FatureCurrent.CurrentClient.IdDeviseConversion, societeCourante.IdSociete);
                else DeviseConvert = FatureCurrent.CurrentClient.DeviseConversion;
            }
        }
        void LoadDepartementArchives()
        {
            if (!isReloadFacture)
            DepartementList = depService.Departement_SELECT_Archive(societeCourante.IdSociete);
        }
        void LoadProduitArchives()
        {
            if (!isReloadFacture)
            {
                try
                {
                    NewDetailListeProduit = detailService.DETAIL_PRODUIT_BYCLIENT_Archive(FatureCurrent.IdClient, FatureCurrent.IdSite);
                    if (NewDetailListeProduit != null && NewDetailListeProduit.Count > 0)
                    {
                        IdProduits = new HashSet<int>();
                        CachnewDetailListeProduit = new List<DetailProductModel>();
                        foreach (var lst in NewDetailListeProduit)
                            CachnewDetailListeProduit.Add(lst);

                        foreach (DetailProductModel det in NewDetailListeProduit)
                            IdProduits.Add(det.IdProduit);

                        ObservableCollection<ProduitModel> newListeProduie = new ObservableCollection<ProduitModel>();
                        foreach (int id in IdProduits)
                            newListeProduie.Add(NewDetailListeProduit.Find(p => p.IdProduit == id).Produit);
                        ProduiList = newListeProduie;
                        CacheProduiList = ProduiList;
                        IndexProduit = -1;
                        if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) <= 2 )
                        {
                            DetailProdEnable = true;
                            IsEnableCmbProduit = true;
                            IsdetailExiste = true;
                        }

                        if (int.Parse(FatureCurrent.CurrentStatut.CourtDesc) == 7)
                        {
                            DetailProdEnable = true;
                            IsEnableCmbProduit = true;
                            IsdetailExiste = true;
                        }

                    }
                    else
                    {
                        throw new Exception("les produits de cette facture n'ont pas put se charger\n si le problème persite merci de contactez l'Administrateur");
                        // MessageBox.Show("les produits de cette facture N'ont pas puis se charge\n sie le problème persite merci de le faire d'abord", "", MessageBoxButton.OK, MessageBoxImage.Warning);
                    }
                }
                catch (Exception ex)
                {
                    throw new Exception(ex.Message);
                }
            }
        }
        void LoaExploitationArchive()
        {
            if (!isReloadFacture)
            {
                ExploitationFactureModel newexploitation = new ExploitationFactureModel { IdExploitation = 0, Libelle = "Aucune", IdSite = societeCourante.IdSociete };
                ExploitationList = exploitationservice.EXPLOITATION_FACTURE_CLIENT_LIST_Archive(FatureCurrent.IdSite, FatureCurrent.IdClient);
                ExploitationList.Add(newexploitation);
            }
        }
        void LoadObjetValicdArchive()
        {
            if (!isReloadFacture)
            ObjetList = objetservice.OBJECT_FACTURE_GETLISTEByCLIENT_Archive(FatureCurrent.IdSite, FatureCurrent.IdClient);
        }

        void LoadClientValideARchive()
        {
            if (issaveUpdatedata)
                if (!isReloadFacture)
                    ClientList = clientservice.CLIENT_Archive_GETLISTE(FatureCurrent.IdSite, true);
        }

        bool testeFacturationvalaible()
        {

            if (GlobalDatas.dataBasparameter.JourLimiteFacturation > 0)
            {
                if (DateMoisPrestation.HasValue)
                {
                    if (DateMoisPrestation.Value.Month >= DateTime.Now.Month && DateMoisPrestation.Value.Year == DateTime.Now.Year)
                    {
                        return true;
                    }
                    else
                    {
                        int jourMois = DateTime.Now.Day;
                        if (jourMois < GlobalDatas.dataBasparameter.JourLimiteFacturation)
                            return true;
                        else
                        {
                            Communicator ctr = new Communicator();
                            ctr.Message = ConstStrings.Get("facture_Msg_Jourlimite"); //"facture_Msg_Jourlimite""The Creating Invoice periode is Expirate , please Inform your manager";
                            EventArgs e1 = new EventArgs();
                            ctr.OnChangePopUp(e1);
                        }

                    }
                }
            }
            else
                return true;

                return false;
            
        }

        FactureModel GetFactureByID(Int64? IDFacture)
        {
            try
            {
                return new FactureModel().GET_FACTURE_BYID((Int64)IDFacture);
            }
            catch (Exception ex)
            {
                CustomExceptionView view = new CustomExceptionView();
                view.Owner = Application.Current.MainWindow;
                view.Title = "PROBLEME DE CHARGEMENT FACTURE";
                view.ViewModel.Message = "Problème survenu lors du chargement de la facture courante";
                view.ShowDialog();
                return null;
            }
        }
        List<LigneFactureModel>  TraitementLigneCommande()
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
                    IdSite = ligne.IdSite ,
                    DateModif = ligne.dateModif,
                    IdExploitation = ligne.IdExploitation ,
                    TauxTva = TaxeSelected.Taux,
                    SpecialFacture = ligne.SpecialMode,
                    ProduitLabel = ligne.Produit
                     
                    
                };
                listItems.Add(newligne);
            }
            return listItems;
        }


       

        static double GetTaxeFr(string Taux, CultureInfo culture)
        {
            string valuesWithout =Taux.Replace('%', ' ');
            double values;
            double valuestry;

            if (valuesWithout.Contains(".") || valuesWithout.Contains(","))
            {
                values = (double.Parse(valuesWithout.Replace(",",
                                                   culture.NumberFormat.NumberDecimalSeparator).Trim().Replace(".",
                                                   culture.NumberFormat.NumberDecimalSeparator).Trim(),
                                                    culture) / 100);
              
            }
            else
            {
                if (double.TryParse(valuesWithout, out valuestry))
                {
                    values = valuestry / 100;
                }
                else
                    throw new Exception(string.Format("Impossible de formatter la valeur  [{0}]  dans cette culture {1}",Taux, culture.Name));
              
            }
            return values;
        }
        #endregion

        #endregion
    }
    public class LigneCommand
    {
        public long ID { get; set; }
        public int IdProduit { get; set; }
        public int Idetail { get; set; }
        public string Produit { get; set; }
        public string Description { get; set; }
        public decimal quantite { get; set; }
        public decimal PrixUnit { get; set; }
        public bool estExonere { get; set; }
        public bool estprorata { get; set; }
        public string remise { get; set; }
        public decimal montantRem { get; set; }
        public decimal montantHt { get; set; }
        public string tva { get; set; }
        public decimal Montanttc { get; set; }
        public int situation { get; set; }
        public bool IsdeletedEnabled { get; set; }
        public string LibelleDetail { get; set; }
        public Int32 IdSite { get; set; }
        public bool SpecialMode { get; set; }
        public DateTime?  dateModif { get; set; }
        public int IdExploitation { get; set; }
    }

}
