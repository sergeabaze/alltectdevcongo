using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;
using AllTech.FrameWork.Global;
using System.Collections.ObjectModel;
using System.Data;

namespace AllTech.FrameWork.Model
{
public class FactureModel : ViewModelBase
    {

        #region Fileds

        public long IdFacture { get; set; }
        public long NumeroLigne { get; set; }
        private string numeroFacture;
        private DateTime? moisPrestation;

        private int idObjetFacture;
        private int idExploitation;

        private int idClient;
        private int idTaxe;
        private int idDevise;
        private int idStatut;
        private int idModePaiement;
        private bool isProrata;
        public int IdModifierPar { get; set; }
        public int IdCreerpar { get; set; }
        public int IdSite { get; set; }
        public int IdDepartement { get; set; }
        private DateTime? dateCloture;
        private DateTime? dateEcheance;
        private DateTime? dateCreation;
        private DateTime? dateSortie;
        private DateTime? dateSuspension;
        private DateTime? dateDepot;
        private DateTime? dateNonValable;
        private DateTime? datePaiement;
        private DateTime? dateFacture;
        public DateTime? DateModif { get; set; }
        private string jourFinEcheance;
        private string label_objet;
        private string label_Dep;
        public int IdClientLangue { get; set; }
        public int IdClientExoneration { get; set; }
        public int IdProrata { get; set; }
        private string backGround;
        private bool  clienOk;
        private int? maregeBeneficiaireId;
        private string orderNumber;
        public string CompteTier { get; set; }
        public string CompteAnalytique { get; set; }

      
    
        public string ClientbackGround { get; set; }
        public bool IsCheck { get; set; }
        public int   icon { get; set; }
        private double totalTTC;
        private double totalHT;
        private Nullable<double> totalPRORATA;
        private Nullable<double> totalTVA;
        public Nullable<double> TotalMarge { get; set; }
        private string exploitationIds;
        private string exploitationList;
        public string LibelleUser { get; set; }
        public string LibelleClient { get; set; }
        public string LibelleStatut { get; set; }
        public string LibelleBackgorund { get; set; }
        public string LibelleClientOk { get; set; }
        public string LibelleClientBackgorund { get; set; }
        public string LibelleClientObjet { get; set; }
         
       

        //new; facture valider

        public string LibelleDepartement { get; set; }
        public string LibelleUserNom { get; set; }
        public string LibelleUserPrenom { get; set; }
        public string TauxMargeBeneficiaire { get; set; }
        public string LibelleTauxTaxe { get; set; }
        public string LibelleDeviseConversion { get; set; }
        public string DeviseTAux { get; set; }

        private TaxeModel currentMarge;
      
        private ObjetFactureModel currentObjetFacture;
        private ExploitationFactureModel currentExploitation;
        private ClientModel currentClient;
        private UtilisateurModel userCreate;
        private UtilisateurModel userUpdate;

       
        private TaxeModel currentTaxe;
        private DeviseModel currentDevise;
        private StatutModel currentStatut;
        private ModePaiementModel currentModePaiement;
        private DepartementModel currentDepartement;

        public bool isfactureValide { get; set; }
     

       
        #endregion

        Facturation DAL = null;

        public FactureModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
            IdFacture = 0;
            moisPrestation  = null;
            jourFinEcheance = string.Empty;
        }

        #region Properties

        public string OrderNumber
        {
            get { return orderNumber; }
            set { orderNumber = value;
            this.OnPropertyChanged("OrderNumber");
            }
        }

        public TaxeModel CurrentMarge
        {
            get { return currentMarge; }
            set { currentMarge = value; }
        }
        public int? MaregeBeneficiaireId
        {
            get { return maregeBeneficiaireId; }
            set { maregeBeneficiaireId = value;
            this.OnPropertyChanged("MaregeBeneficiaireId");
            }
        }

        public string ExploitationIds
        {
            get { return exploitationIds; }
            set { exploitationIds = value;
            this.OnPropertyChanged("ExploitationIds");
            }
        }


        public string ExploitationList
        {
            get { return exploitationList; }
            set { exploitationList = value;
            this.OnPropertyChanged("ExploitationList");
            }
        }

        public double TotalHT
        {
            get { return totalHT; }
            set { totalHT = value;
            this.OnPropertyChanged("TotalHT");
            }
        }


        public Nullable<double> TotalTVA
        {
            get { return totalTVA; }
            set { totalTVA = value;
            this.OnPropertyChanged("TotalTVA");
            }
        }


        public Nullable<double> TotalPRORATA
        {
            get { return totalPRORATA; }
            set { totalPRORATA = value;
            this.OnPropertyChanged("TotalPRORATA");
            }
        }

        public DateTime? DateFacture
        {
            get { return dateFacture; }
            set { dateFacture = value;
            this.OnPropertyChanged("DateFacture");
            }
        }

        public double TotalTTC
        {
            get { return totalTTC; }
            set { totalTTC = value;
            this.OnPropertyChanged("TotalTTC");
            }
        }

        public bool  ClienOk
        {
            get { return clienOk; }
            set { clienOk = value;
            this.OnPropertyChanged("ClienOk");
            }
        }

        public string Label_Dep
        {
            get { return label_Dep; }
            set
            {
                label_Dep = value;
                this.OnPropertyChanged("Label_Dep");
            }
        }


        public DepartementModel CurrentDepartement
        {
            get { return currentDepartement; }
            set { currentDepartement = value;
             this.OnPropertyChanged("CurrentDepartement");
            }
            
        }

        public string Label_objet
        {
            get { return label_objet; }
            set { label_objet = value;
            this.OnPropertyChanged("Label_objet");
            }
        }

        public string BackGround
        {
            get { return backGround; }
            set { backGround = value;
            this.OnPropertyChanged("BackGround");
            }
        }

        public ExploitationFactureModel CurrentExploitation
        {
            get { return currentExploitation; }
            set { currentExploitation = value;
            this.OnPropertyChanged("CurrentExploitation");
            }
        }
        public DateTime? DateDepot
        {
            get { return dateDepot; }
            set { dateDepot = value;
            this.OnPropertyChanged("DateDepot");
            }
        }
        public int IdExploitation
        {
            get { return idExploitation; }
            set { idExploitation = value;
            this.OnPropertyChanged("IdExploitation");
            }
        }

        public string JourFinEcheance
        {
            get { return jourFinEcheance; }
            set { jourFinEcheance = value;
            this.OnPropertyChanged("JourFinEcheance");
            }
        }


        public bool IsProrata
        {
            get { return isProrata; }
            set { isProrata = value;
            this.OnPropertyChanged("IsProrata");
            }
        }

        public DateTime? DateSortie
        {
            get { return dateSortie; }
            set { dateSortie = value;
            this.OnPropertyChanged("DateSortie");
            }
        }

        public DateTime? DateNonValable
        {
            get { return dateNonValable; }
            set { dateNonValable = value;
            this.OnPropertyChanged("DateNonValable");
            }
        }


        public DateTime? DateSuspension
        {
            get { return dateSuspension; }
            set { dateSuspension = value;
            this.OnPropertyChanged("DateSuspension");
            }
        }


        public StatutModel CurrentStatut
        {
            get { return currentStatut; }
            set { currentStatut = value ;
            this.OnPropertyChanged("CurrentStatut");
            }
        }

        public DateTime? DateCreation
        {
            get { return dateCreation; }
            set { dateCreation = value;
            this.OnPropertyChanged("DateCreation");
            }
        }
        public ModePaiementModel CurrentModePaiement
        {
            get { return currentModePaiement; }
            set { currentModePaiement = value;
            this.OnPropertyChanged("CurrentModePaiement");
            }
        }

        public DeviseModel CurrentDevise
        {
            get { return currentDevise; }
            set { currentDevise = value;
            this.OnPropertyChanged("CurrentDevise");
            }
        }

        public TaxeModel CurrentTaxe
        {
            get { return currentTaxe; }
            set { currentTaxe = value;
            this.OnPropertyChanged("CurrentTaxe");
            }
        }


        public UtilisateurModel UserCreate
        {
            get { return userCreate; }
            set { userCreate = value ;
            this.OnPropertyChanged("UserCreate");
            }

        }
        public ClientModel CurrentClient
        {
            get { return currentClient; }
            set { currentClient = value ;
            this.OnPropertyChanged("CurrentClient");
            }

        }
       

        public ObjetFactureModel CurrentObjetFacture
        {
            get { return currentObjetFacture; }
            set { currentObjetFacture = value;
            this.OnPropertyChanged("CurrentObjetFacture");
            }

        }

        public UtilisateurModel UserUpdate
        {
            get { return userUpdate; }
            set { userUpdate = value;
            this.OnPropertyChanged("UserUpdate");
            }
        }
        public string NumeroFacture
        {
            get { return numeroFacture; }
            set
            {
                numeroFacture = value;
                this.OnPropertyChanged("NumeroFacture");
            }
        }

        public DateTime? MoisPrestation
        {
            get { return moisPrestation; }
            set { moisPrestation = value;
                if (value !=null )
            JourFinEcheance = ((DateTime)value).Day.ToString ();
            this.OnPropertyChanged("MoisPrestation");
            }
        }

        public int IdDevise
        {
            get { return idDevise; }
            set { idDevise = value;
            this.OnPropertyChanged("IdDevise");
            }
        }


        public int IdModePaiement
        {
            get { return idModePaiement; }
            set { idModePaiement = value;
            this.OnPropertyChanged("IdModePaiement");
            }

        }
        public int IdStatut
        {
            get { return idStatut; }
            set { idStatut = value;
            this.OnPropertyChanged("IdStatut");
            }
        }

        public int IdClient
        {
            get { return idClient; }
            set { idClient = value;
            this.OnPropertyChanged("IdClient");
            }
        }

        public int IdTaxe
        {
            get { return idTaxe; }
            set { idTaxe = value;
            this.OnPropertyChanged("IdTaxe");
            }
        }

        public DateTime? DateCloture
        {
            get { return dateCloture; }
            set { dateCloture = value;
            this.OnPropertyChanged("DateCloture");
            }
        }

        public DateTime? DateEcheance
        {
            get { return dateEcheance; }
            set { dateEcheance = value;
            this.OnPropertyChanged("DateEcheance");
            }
        }

        public DateTime? DatePaiement
        {
            get { return datePaiement; }
            set { datePaiement = value;
            this.OnPropertyChanged("DatePaiement");
            }
        }


      

        public int   IdObjetFacture
        {
            get { return idObjetFacture; }
            set
            {
                idObjetFacture = value;
                this.OnPropertyChanged("IdObjetFacture");
            }
        }

       

        //public int  IdEnteteFacture
        //{
        //    get { return idEnteteFacture; }
        //    set
        //    {
        //        idEnteteFacture = value;
        //        this.OnPropertyChanged("IdEnteteFacture");
        //    }
        //}

      
        #endregion

        #region Methods

        public static bool IsExist_Facture_Archive(int idSite)
        {
            Facturation nDAL = (Facturation)DataProviderObject.FacturationDal;
            return nDAL.IsfactureArchiveExists(idSite);

        }


        public ObservableCollection<FactureModel> GetAll_FACTURE_Archive(Int32 idSite, DateTime periode)
        {
            ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
            List<Facture> nfactures = DAL.GetAll_FACTURE_Archive(idSite, periode);
            foreach (var f in nfactures)
            {
                FactureModel nf = ConverfromNew(f);
                factures.Add(nf);
            }
            return factures;
        }

        public FactureModel GET_FACTURE_BYID_Archive(long id, int idSite)
        {
            FactureModel facture = null;
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();

            try
            {
                Facture nf = DAL.GetAll_FACTUREVALIDATE_BYID(id, idSite, 1);

                if (nf != null)
                {
                    facture = Converfrom(nf);

                    if (nf.CurrentStatut.ShortName == "1")
                        facture.backGround = "Gray";
                    if (nf.CurrentStatut.ShortName == "2")
                        facture.backGround = "#FF77AED8";
                    if (nf.CurrentStatut.ShortName == "3")
                        facture.backGround = "#FFD1C683";
                    if (nf.CurrentStatut.ShortName == "4")
                        facture.backGround = "#FF8888C4";
                    if (nf.CurrentStatut.ShortName == "5")
                        facture.backGround = "#FF9CBE44";
                    if (nf.CurrentStatut.ShortName == "6")
                        facture.backGround = "Red";
                    if (nf.CurrentStatut.ShortName == "7")
                        facture.backGround = "Red";
                    facture.ClienOk = true;
                    facture.ClientbackGround = "White";


                    //if (Global.Utils.IsClientComplet(nf.CurrentClient))
                    //{
                    //    facture.ClienOk = true;
                    //    facture.ClientbackGround = "White";
                    //}
                    //else
                    //{
                    //    facture.ClienOk = false;
                    //    facture.ClientbackGround = "#FFF2A99E";
                    //}

                    if (nf.CurrentStatut.ShortName == "4")
                    {
                        facture.icon = 4;
                    }
                    if (nf.CurrentStatut.ShortName == "1" || nf.CurrentStatut.ShortName == "2" || nf.CurrentStatut.ShortName == "3")
                    {
                        facture.icon = 2;
                    }
                    if (nf.CurrentStatut.ShortName == "5" || nf.CurrentStatut.ShortName == "6" || nf.CurrentStatut.ShortName == "7")
                    {
                        facture.icon = 3;
                    }

                    facture.CurrentClient = ConvertFromClient(nf.CurrentClient);
                    facture.CurrentClient.Compte = ConvertfromCmpt(nf.CurrentClient.Ccompte);
                    facture.CurrentClient.Exonerere = ConvertfromExo(nf.CurrentClient.Exonerate);
                  
                    facture.CurrentClient.DeviseConversion = Convertfromdevise(nf.CurrentClient.DeviseConversion);
                    facture.CurrentClient.DeviseFacture = Convertfromdevise(nf.CurrentClient.DeviseFacture);
                    facture.CurrentClient.Porata = ConvertfromProrata(nf.CurrentClient.Porata);
                    // facture.CurrentClient.LibelleTerme = convertfromLibelle(nf.CurrentClient.LibelleTerme);
                    facture.CurrentClient.Llangue = new LangueModel { Id = nf.CurrentClient.Llangue.IdLangue, Libelle = nf.CurrentClient.Llangue.Libelle, Shortname = nf.CurrentClient.Llangue.Shorname };
                    facture.CurrentDevise = ConvertfromDevise(nf.CurrentDevise);
                    facture.CurrentExploitation = Converfromexploit(nf.CurrentExploitationFacture);
                    facture.CurrentMarge = ConvertfromTaxe(nf.CurrentMarge);
                    facture.CurrentModePaiement = ConverfromModeP(nf.CurrentModePaiement);
                    facture.CurrentObjetFacture = ConverfromObjet(nf.CurrentObjetFacture);
                    facture.CurrentStatut = ConverfromStatut(nf.CurrentStatut);
                    facture.CurrentTaxe = ConvertfromTaxe(nf.CurrentTaxe);
                    facture.UserCreate = ConverfromUser(nf.UserCreate);
                    facture.UserUpdate = ConverfromUser(nf.UserModified);
                    facture.CurrentExploitation = Converfromexploit(nf.CurrentExploitationFacture);
                    facture.CurrentDepartement = ConvertFromddept(nf.CurrentDepartement);
                }
                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<FactureModel> SEARCH_LIST_FACTURE_Archive(Int32 idSite, DateTime dateDebut, DateTime dateFin, Int32 idclient)
        {
            ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();
            long numeroLigne = 0;
            try
            {
                List<Facture> nfactures = DAL.SEARCH_LIST_FACTURE_Archive(idSite, dateDebut, dateFin, idclient);
                if (nfactures.Count > 0)
                {
                    foreach (var f in nfactures)
                    {
                        FactureModel nf = ConverfromNew(f);
                        factures.Add(nf);
                    }
                   
                }
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public static DataTable Facture_Periode_Archive(Int32 idSite)
        {
            Facturation nDAL = (Facturation)DataProviderObject.FacturationDal;
            return nDAL.Facture_Periode_Archive(idSite);

        }

        #region Archives
          
        #endregion

        public static DataRow  ModelFactureByID(Int64 ID)
        {
            Facturation nDAL = (Facturation)DataProviderObject.FacturationDal;
            return nDAL.FactureByID(ID);

        }

        public static DataTable ModelFactureVersion(Int64 idfacture)
        {
            Facturation nDAL = (Facturation)DataProviderObject.FacturationDal;
            return nDAL.FactureVersion(idfacture);

        }

        public static DataTable ModelFactureList(Int32 idSite)
        {
           Facturation nDAL= (Facturation)DataProviderObject.FacturationDal;
          return  nDAL.FactureList(idSite);
           
        }

        public static DataTable ModelFactureListByDate(Int32 idSite, DateTime dateDebut, DateTime dateFin, Int32 idclient)
        {
            Facturation nDAL = (Facturation)DataProviderObject.FacturationDal;
            return nDAL.FactureListeByDate(idSite, dateDebut, dateFin, idclient);

        }

        public ObservableCollection<FactureModel> FACTURE_GETLISTE_NEW(Int32 idSite,bool modeFacture)
        {
            ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
            List<Facture> nfactures = DAL.GetAll_FACTURENEW(idSite, modeFacture);
            foreach (var f in nfactures)
            {
                FactureModel nf = ConverfromNew(f);
                factures.Add(nf);
            }
            return factures;
        }

        public ObservableCollection<FactureModel> FACTURE_GETLISTE(Int32 idSite)
        {
            ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect=new ObjetFactureModel ();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();
           // Global.Utils.logUserActions(" <-- Dbrequette Chargement liste des  factures  <<FACTURE_GETLISTE>> ID site [" + idSite +"", "");
         
            long numeroLigne = 0;
           
            try
            {
                List<Facture> nfactures = DAL.GetAll_FACTURE(idSite);
                if (nfactures.Count > 0)
                {
                    foreach (var f in nfactures)
                    {

                        FactureModel nf = Converfrom(f);

                        if (f.CurrentStatut.ShortName == "1")
                            nf.backGround = "Gray";
                        if (f.CurrentStatut.ShortName == "2")
                            nf.backGround = "#FF77AED8";
                        if (f.CurrentStatut.ShortName == "3")
                            nf.backGround = "#FFD1C683";
                        if (f.CurrentStatut.ShortName == "4")
                            nf.backGround = "#FF8888C4";
                        if (f.CurrentStatut.ShortName == "5")
                            nf.backGround = "#FF9CBE44";

                        if (f.CurrentStatut.ShortName == "6")
                            nf.backGround = "Red";
                        if (f.CurrentStatut.ShortName == "7")
                            nf.backGround = "Red";
                        nf.ClienOk = true;
                        nf.ClientbackGround = "White";
                        nf.IsCheck = false;
                        //if (Global.Utils.IsClientComplet(f.CurrentClient))
                        //{
                        //    nf.ClienOk = true;
                        //    nf.ClientbackGround = "White";
                        //}
                        //else
                        //{
                        //    nf.ClienOk = false;
                        //    nf.ClientbackGround = "#FFF2A99E";
                        //}

                        if (f.CurrentStatut.ShortName == "4")
                        {
                            nf.icon = 4;
                        }
                        if (f.CurrentStatut.ShortName == "1" || f.CurrentStatut.ShortName == "2" || f.CurrentStatut.ShortName == "3")
                        {
                            nf.icon = 2;
                        }
                        if (f.CurrentStatut.ShortName == "5" || f.CurrentStatut.ShortName == "6" || f.CurrentStatut.ShortName == "7")
                        {
                            nf.icon = 3;
                        }

                       // nf.CurrentClient = ConvertFromClient(f.CurrentClient);
                       // nf.CurrentClient.Compte = ConvertfromCmpt(f.CurrentClient.Ccompte);
                       // nf.CurrentClient.Exonerere = ConvertfromExo(f.CurrentClient.Exonerate);
                       // nf.CurrentClient.Devise = Convertfromdevise(f.CurrentClient.Devise);
                       // nf.CurrentClient.Porata = ConvertfromProrata(f.CurrentClient.Porata);
                       //// nf.CurrentClient.LibelleTerme = convertfromLibelle(f.CurrentClient.LibelleTerme);
                       // nf.CurrentClient.Llangue = new LangueModel { Id = f.CurrentClient.Llangue.IdLangue, Libelle = f.CurrentClient.Llangue.Libelle, Shortname = f.CurrentClient.Llangue.Shorname };

                       // nf.CurrentDevise = ConvertfromDevise(f.CurrentDevise);
                       // nf.CurrentExploitation = Converfromexploit(f.CurrentExploitationFacture);
                       // nf.CurrentModePaiement = ConverfromModeP(f.CurrentModePaiement);
                       // nf.CurrentObjetFacture = ConverfromObjet(f.CurrentObjetFacture);
                        nf.CurrentStatut = ConverfromStatut(f.CurrentStatut);
                       // nf.CurrentTaxe = ConvertfromTaxe(f.CurrentTaxe);
                       // nf.UserCreate = ConverfromUser(f.UserCreate);
                       // nf.CurrentDepartement = ConvertFromddept(f.CurrentDepartement);
                       // nf.NumeroLigne = numeroLigne;

                        factures.Add(nf);
                        numeroLigne++;
                    }
                   
                }
                else 
                {
                   
                }
               
         
                return factures;

            }
            catch (Exception de)
            {
               
         
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<FactureModel> FACTURE_GETLISTE(Int32 idSite,int liste)
        {
            ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();
            LigneFactureModel lignfacture = new LigneFactureModel();
            List<LigneFactureModel> items = null;
            long numeroLigne = 0;
            //object[] tabretour = null;
            try
            {
                List<Facture> nfactures = DAL.GetAll_FACTURE(idSite);
                if (nfactures.Count > 0)
                {
                    foreach (var f in nfactures)
                    {

                        FactureModel nf = Converfrom(f);
                       

                        if (f.CurrentStatut.ShortName == "1")
                            nf.backGround = "Gray";
                        if (f.CurrentStatut.ShortName == "2")
                            nf.backGround = "#FF77AED8";
                        if (f.CurrentStatut.ShortName == "3")
                            nf.backGround = "#FFD1C683";
                        if (f.CurrentStatut.ShortName == "4")
                            nf.backGround = "#FF8888C4";
                        if (f.CurrentStatut.ShortName == "5")
                            nf.backGround = "#FF9CBE44";
                        if (f.CurrentStatut.ShortName == "6")
                            nf.backGround = "Red";
                        if (f.CurrentStatut.ShortName == "7")
                            nf.backGround = "Red";
                        nf.ClienOk = true;
                        nf.ClientbackGround = "White";

                        //if (Global.Utils.IsClientComplet(f.CurrentClient))
                        //{
                        //    nf.ClienOk = true;
                        //    nf.ClientbackGround = "White";
                        //}
                        //else
                        //{
                        //    nf.ClienOk = false;
                        //    nf.ClientbackGround = "#FFF2A99E";
                        //}

                        if (f.CurrentStatut.ShortName == "4")
                        {
                            nf.icon = 4;
                        }
                        if (f.CurrentStatut.ShortName == "1" || f.CurrentStatut.ShortName == "2" || f.CurrentStatut.ShortName == "3")
                        {
                            nf.icon = 2;
                        }
                        if (f.CurrentStatut.ShortName == "5" || f.CurrentStatut.ShortName == "6" || f.CurrentStatut.ShortName == "7")
                        {
                            nf.icon = 3;
                        }

                        nf.CurrentClient = ConvertFromClient(f.CurrentClient);
                        nf.CurrentClient.Compte = ConvertfromCmpt(f.CurrentClient.Ccompte);
                        nf.CurrentClient.Exonerere = ConvertfromExo(f.CurrentClient.Exonerate);
                      

                        nf.CurrentClient.DeviseConversion = Convertfromdevise(f.CurrentClient.DeviseConversion);
                        nf.CurrentClient.DeviseFacture = Convertfromdevise(f.CurrentClient.DeviseFacture);
                        nf.CurrentClient.Porata = ConvertfromProrata(f.CurrentClient.Porata);
                       // nf.CurrentClient.LibelleTerme = convertfromLibelle(f.CurrentClient.LibelleTerme);
                        nf.CurrentClient.Llangue = new LangueModel { Id = f.CurrentClient.Llangue.IdLangue, Libelle = f.CurrentClient.Llangue.Libelle, Shortname = f.CurrentClient.Llangue.Shorname };
                        nf.CurrentDevise = ConvertfromDevise(f.CurrentDevise);
                        nf.CurrentExploitation = Converfromexploit(f.CurrentExploitationFacture);
                        nf.CurrentModePaiement = ConverfromModeP(f.CurrentModePaiement);
                        nf.CurrentObjetFacture = ConverfromObjet(f.CurrentObjetFacture);
                        nf.CurrentStatut = ConverfromStatut(f.CurrentStatut);
                        nf.CurrentTaxe = ConvertfromTaxe(f.CurrentTaxe);
                        nf.UserCreate = ConverfromUser(f.UserCreate);
                        nf.CurrentDepartement = ConvertFromddept(f.CurrentDepartement);
                        nf.NumeroLigne = numeroLigne;

                        factures.Add(nf);
                        numeroLigne++;
                    }
                }
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<FactureModel> FACTURE_SORTIE_GETLISTE(Int32 idSite, DateTime datedebut,DateTime datefin,int mode)
        {
            ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();
            LigneFactureModel lignfacture = new LigneFactureModel();
            List<LigneFactureModel> items = null;
            long numeroLigne = 0;
            //object[] tabretour = null;
            try
            {
                List<Facture> nfactures = DAL.LIST_FACTURE_SORTIE(idSite, datedebut, datefin,mode);
                if (nfactures.Count > 0)
                {
                    foreach (var f in nfactures)
                    {

                        FactureModel nf = Converfrom(f);



                        if (f.CurrentStatut.ShortName == "3")
                            nf.backGround = "#FFD1C683";
                        if (f.CurrentStatut.ShortName == "4")
                            nf.backGround = "#FF8888C4";
                        if (f.CurrentStatut.ShortName == "5")
                            nf.backGround = "#FF9CBE44";
                        if (f.CurrentStatut.ShortName == "6")
                            nf.backGround = "Red";
                        if (f.CurrentStatut.ShortName == "7")
                            nf.backGround = "Red";
                       // if (Global.Utils.IsClientComplet(f.CurrentClient))
                       // {
                            nf.ClienOk = true;
                            nf.ClientbackGround = "White";
                       // }
                       // else
                       // {
                          //  nf.ClienOk = false;
                          //  nf.ClientbackGround = "#FFF2A99E";
                       // }

                        if (f.CurrentStatut.ShortName == "4")
                        {
                            nf.icon = 4;
                        }
                        if (f.CurrentStatut.ShortName == "1" || f.CurrentStatut.ShortName == "2" || f.CurrentStatut.ShortName == "3")
                        {
                            nf.icon = 2;
                        }
                        if (f.CurrentStatut.ShortName == "5" || f.CurrentStatut.ShortName == "6" || f.CurrentStatut.ShortName == "7")
                        {
                            nf.icon = 3;
                        }

                        if (f.CurrentClient != null)
                        {
                            nf.CurrentClient = ConvertFromClient(f.CurrentClient);
                            nf.CurrentClient.Compte = ConvertfromCmpt(f.CurrentClient.Ccompte);
                            nf.CurrentClient.Exonerere = ConvertfromExo(f.CurrentClient.Exonerate);
                            
                            nf.CurrentClient.DeviseConversion = Convertfromdevise(f.CurrentClient.DeviseConversion);
                            nf.CurrentClient.DeviseFacture = Convertfromdevise(f.CurrentClient.DeviseFacture);
                            nf.CurrentClient.Porata = ConvertfromProrata(f.CurrentClient.Porata);
                            // nf.CurrentClient.LibelleTerme = convertfromLibelle(f.CurrentClient.LibelleTerme);
                            nf.CurrentClient.Llangue = new LangueModel { Id = f.CurrentClient.Llangue.IdLangue, Libelle = f.CurrentClient.Llangue.Libelle, Shortname = f.CurrentClient.Llangue.Shorname };
                        }
                         nf.CurrentDevise = ConvertfromDevise(f.CurrentDevise);
                        nf.CurrentExploitation = Converfromexploit(f.CurrentExploitationFacture);
                        nf.CurrentModePaiement = ConverfromModeP(f.CurrentModePaiement);
                        nf.CurrentObjetFacture = ConverfromObjet(f.CurrentObjetFacture);
                        nf.CurrentStatut = ConverfromStatut(f.CurrentStatut);
                        nf.CurrentTaxe = ConvertfromTaxe(f.CurrentTaxe);
                        nf.UserCreate = ConverfromUser(f.UserCreate);
                        nf.CurrentDepartement = ConvertFromddept(f.CurrentDepartement);
                        nf.NumeroLigne = numeroLigne;

                        factures.Add(nf);
                        numeroLigne++;
                    }
                }
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<FactureModel> FACTURE_SEARCH(Int32 idSite, DateTime dateDebut, DateTime dateFin, Int32 idclient)
        {
            ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();
            long numeroLigne = 0;
            try
            {
                List<Facture> nfactures = DAL.SEARCH_LIST_FACTURE(idSite, dateDebut, dateFin, idclient);
                if (nfactures.Count > 0)
                {
                    foreach (var f in nfactures)
                    {
                        FactureModel nf = ConverfromNew(f);
                        factures.Add(nf);
                    }
                    //foreach (var f in nfactures)
                    //{
                    //    FactureModel nf = Converfrom(f);

                    //    if (f.CurrentStatut.ShortName == "1")
                    //        nf.backGround = "Gray";
                    //    if (f.CurrentStatut.ShortName == "2")
                    //        nf.backGround = "#FF77AED8";
                    //    if (f.CurrentStatut.ShortName == "3")
                    //        nf.backGround = "#FFD1C683";
                    //    if (f.CurrentStatut.ShortName == "4")
                    //        nf.backGround = "#FF8888C4";
                    //    if (f.CurrentStatut.ShortName == "5")
                    //        nf.backGround = "#FF9CBE44";
                    //    if (f.CurrentStatut.ShortName == "6")
                    //        nf.backGround = "Red";
                    //    if (f.CurrentStatut.ShortName == "7")
                    //        nf.backGround = "Red";
                    //    //#FF9CBE44

                    //    nf.ClienOk = true;
                    //    nf.ClientbackGround = "White";
                        
                    //    //if (Global.Utils.IsClientComplet(f.CurrentClient))
                    //    //{
                    //    //    nf.ClienOk = true;
                    //    //    nf.ClientbackGround = "White";
                    //    //}
                    //    //else
                    //    //{
                    //    //    nf.ClienOk = false;
                    //    //    nf.ClientbackGround = "#FFF2A99E";
                    //    //}

                    //    if (f.CurrentStatut.ShortName == "4")
                    //    {
                    //        nf.icon = 4;
                    //    }
                    //    if (f.CurrentStatut.ShortName == "1" || f.CurrentStatut.ShortName == "2" || f.CurrentStatut.ShortName == "3")
                    //    {
                    //        nf.icon = 2;
                    //    }
                    //    if (f.CurrentStatut.ShortName == "5" || f.CurrentStatut.ShortName == "6" || f.CurrentStatut.ShortName == "7")
                    //    {
                    //        nf.icon = 3;
                    //    }


                    //    nf.CurrentClient = ConvertFromClient(f.CurrentClient);
                    //    nf.CurrentClient.Compte = ConvertfromCmpt(f.CurrentClient.Ccompte);
                    //    nf.CurrentClient.Exonerere = ConvertfromExo(f.CurrentClient.Exonerate);
                    //    nf.CurrentClient.Devise = Convertfromdevise(f.CurrentClient.Devise);
                    //    nf.CurrentClient.Porata = ConvertfromProrata(f.CurrentClient.Porata);
                    //    nf.CurrentClient.LibelleTerme = convertfromLibelle(f.CurrentClient.LibelleTerme);
                    //    nf.CurrentClient.Llangue = new LangueModel { Id = f.CurrentClient.Llangue.IdLangue, Libelle = f.CurrentClient.Llangue.Libelle, Shortname = f.CurrentClient.Llangue.Shorname };
                    //    nf.CurrentDevise = ConvertfromDevise(f.CurrentDevise);
                    //    nf.CurrentExploitation = Converfromexploit(f.CurrentExploitationFacture);
                    //    nf.CurrentModePaiement = ConverfromModeP(f.CurrentModePaiement);
                    //    nf.CurrentObjetFacture = ConverfromObjet(f.CurrentObjetFacture);
                    //    nf.CurrentStatut = ConverfromStatut(f.CurrentStatut);
                    //    nf.CurrentTaxe = ConvertfromTaxe(f.CurrentTaxe);
                    //    nf.UserCreate = ConverfromUser(f.UserCreate);
                    //    nf.CurrentExploitation = Converfromexploit(f.CurrentExploitationFacture);
                    //    nf.CurrentDepartement = ConvertFromddept(f.CurrentDepartement);
                    //    nf.NumeroLigne = numeroLigne;
                    //    factures.Add(nf);
                    //    numeroLigne++;
                    //}
                }
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public ObservableCollection<FactureModel> FACTURE_SEARCH_BYFACTURE(string numFacture, string mode)
        {
            ObservableCollection<FactureModel> factures = new ObservableCollection<FactureModel>();
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();
            long numeroLigne = 0;
            try
            {
                List<Facture> nfactures = DAL.SEARCH_FACTURE_BYNUM(numFacture, mode);
                if (nfactures.Count > 0)
                {
                    foreach (var f in nfactures)
                    {
                        FactureModel nf = ConverfromNew(f);
                        factures.Add(nf);
                    }

                }
                return factures;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public FactureModel GET_FACTUREVALIDE_BYID(long id, int idSite, int Mode)
        {
            FactureModel facture = null;
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();

            try
            {
                Facture nf = DAL.GetAll_FACTUREVALIDATE_BYID(id, idSite, Mode);

                if (nf != null)
                {
                    facture = Converfrom(nf);

                    if (nf.CurrentStatut.ShortName == "1")
                        facture.backGround = "Gray";
                    if (nf.CurrentStatut.ShortName == "2")
                        facture.backGround = "#FF77AED8";
                    if (nf.CurrentStatut.ShortName == "3")
                        facture.backGround = "#FFD1C683";
                    if (nf.CurrentStatut.ShortName == "4")
                        facture.backGround = "#FF8888C4";
                    if (nf.CurrentStatut.ShortName == "5")
                        facture.backGround = "#FF9CBE44";
                    if (nf.CurrentStatut.ShortName == "6")
                        facture.backGround = "Red";
                    if (nf.CurrentStatut.ShortName == "7")
                        facture.backGround = "Red";
                    facture.ClienOk = true;
                    facture.ClientbackGround = "White";


                    //if (Global.Utils.IsClientComplet(nf.CurrentClient))
                    //{
                    //    facture.ClienOk = true;
                    //    facture.ClientbackGround = "White";
                    //}
                    //else
                    //{
                    //    facture.ClienOk = false;
                    //    facture.ClientbackGround = "#FFF2A99E";
                    //}

                    if (nf.CurrentStatut.ShortName == "4")
                    {
                        facture.icon = 4;
                    }
                    if (nf.CurrentStatut.ShortName == "1" || nf.CurrentStatut.ShortName == "2" || nf.CurrentStatut.ShortName == "3")
                    {
                        facture.icon = 2;
                    }
                    if (nf.CurrentStatut.ShortName == "5" || nf.CurrentStatut.ShortName == "6" || nf.CurrentStatut.ShortName == "7")
                    {
                        facture.icon = 3;
                    }

                    facture.CurrentClient = ConvertFromClient(nf.CurrentClient);
                    facture.CurrentClient.Compte = ConvertfromCmpt(nf.CurrentClient.Ccompte);
                    facture.CurrentClient.Exonerere = ConvertfromExo(nf.CurrentClient.Exonerate);


                    facture.CurrentClient.DeviseConversion = Convertfromdevise(nf.CurrentClient.DeviseConversion);
                    facture.CurrentClient.DeviseFacture = Convertfromdevise(nf.CurrentClient.DeviseFacture);
                    facture.CurrentClient.Porata = ConvertfromProrata(nf.CurrentClient.Porata);
                   // facture.CurrentClient.LibelleTerme = convertfromLibelle(nf.CurrentClient.LibelleTerme);
                    facture.CurrentClient.Llangue = new LangueModel { Id = nf.CurrentClient.Llangue.IdLangue, Libelle = nf.CurrentClient.Llangue.Libelle, Shortname = nf.CurrentClient.Llangue.Shorname };
                    facture.CurrentDevise = ConvertfromDevise(nf.CurrentDevise);
                    facture.CurrentExploitation = Converfromexploit(nf.CurrentExploitationFacture);
                    facture.CurrentModePaiement = ConverfromModeP(nf.CurrentModePaiement);
                    facture.CurrentObjetFacture = ConverfromObjet(nf.CurrentObjetFacture);
                    facture.CurrentStatut = ConverfromStatut(nf.CurrentStatut);
                    facture.CurrentTaxe = ConvertfromTaxe(nf.CurrentTaxe);
                    facture.CurrentMarge = ConvertfromTaxe(nf.CurrentMarge);

                    facture.UserCreate = ConverfromUser(nf.UserCreate);
                    facture.UserUpdate = ConverfromUser(nf.UserModified);
                    facture.CurrentExploitation = Converfromexploit(nf.CurrentExploitationFacture);
                    facture.CurrentDepartement = ConvertFromddept(nf.CurrentDepartement);
                }
                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public FactureModel GET_FACTURE_BYID(long  id)
        {
            FactureModel facture = null;
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();

            try
            {
                Facture nf = DAL.GetAll_FACTUREBYID(id);
                if (nf != null)
                {
                    facture = Converfrom(nf);

                    if (nf.CurrentStatut.ShortName == "1")
                        facture.backGround = "Gray";
                    if (nf.CurrentStatut.ShortName == "2")
                        facture.backGround = "#FF77AED8";
                    if (nf.CurrentStatut.ShortName == "3")
                        facture.backGround = "#FFD1C683";
                    if (nf.CurrentStatut.ShortName == "4")
                        facture.backGround = "#FF8888C4";
                    if (nf.CurrentStatut.ShortName == "5")
                        facture.backGround = "#FF9CBE44";
                    if (nf.CurrentStatut.ShortName == "6")
                        facture.backGround = "Red";
                    if (nf.CurrentStatut.ShortName == "7")
                        facture.backGround = "Red";
                    facture.ClienOk = true;
                    facture.ClientbackGround = "White";


                    //if (Global.Utils.IsClientComplet(nf.CurrentClient))
                    //{
                    //    facture.ClienOk = true;
                    //    facture.ClientbackGround = "White";
                    //}
                    //else
                    //{
                    //    facture.ClienOk = false;
                    //    facture.ClientbackGround = "#FFF2A99E";
                    //}

                    if (nf.CurrentStatut.ShortName == "4")
                    {
                        facture.icon = 4;
                    }
                    if (nf.CurrentStatut.ShortName == "1" || nf.CurrentStatut.ShortName == "2" || nf.CurrentStatut.ShortName == "3")
                    {
                        facture.icon = 2;
                    }
                    if (nf.CurrentStatut.ShortName == "5" || nf.CurrentStatut.ShortName == "6" || nf.CurrentStatut.ShortName == "7")
                    {
                        facture.icon = 3;
                    }
                   
                    facture.CurrentClient = ConvertFromClient(nf.CurrentClient);
                    facture.CurrentClient.Compte = ConvertfromCmpt(nf.CurrentClient.Ccompte);
                    facture.CurrentClient.Exonerere = ConvertfromExo(nf.CurrentClient.Exonerate);
                   
                    facture.CurrentClient.DeviseConversion = Convertfromdevise(nf.CurrentClient.DeviseConversion);
                    facture.CurrentClient.DeviseFacture = Convertfromdevise(nf.CurrentClient.DeviseFacture);

                    facture.CurrentClient.Porata = ConvertfromProrata(nf.CurrentClient.Porata);
                    facture.CurrentMarge = ConvertfromTaxe(nf.CurrentMarge);
                   // facture.CurrentClient.LibelleTerme = convertfromLibelle(nf.CurrentClient.LibelleTerme);
                    facture.CurrentClient.Llangue = new LangueModel { Id = nf.CurrentClient.Llangue.IdLangue, Libelle = nf.CurrentClient.Llangue.Libelle, Shortname = nf.CurrentClient.Llangue.Shorname };
                    facture.CurrentDevise = ConvertfromDevise(nf.CurrentDevise);
                    //facture.CurrentExploitation = Converfromexploit(nf.CurrentExploitationFacture);
                    //facture.CurrentModePaiement = ConverfromModeP(nf.CurrentModePaiement);
                    //facture.CurrentObjetFacture = ConverfromObjet(nf.CurrentObjetFacture);
                    facture.CurrentStatut = ConverfromStatut(nf.CurrentStatut);
                    facture.CurrentTaxe = ConvertfromTaxe(nf.CurrentTaxe);
               
                    //facture.UserCreate = ConverfromUser(nf.UserCreate);
                    //facture.UserUpdate = ConverfromUser(nf.UserModified);
                    //facture.CurrentExploitation = Converfromexploit(nf.CurrentExploitationFacture);
                    //facture.CurrentDepartement = ConvertFromddept(nf.CurrentDepartement);
                }
                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public FactureModel GET_FACTURE_BYID(long id,Int32 idSite)
        {
            FactureModel facture = null;
            ClientModel clientSelect = new ClientModel();
            DeviseModel decSelect = new DeviseModel();
            ExploitationFactureModel exploitSelect = new ExploitationFactureModel();
            ModePaiementModel modePaieSelect = new ModePaiementModel();
            ObjetFactureModel objetSelect = new ObjetFactureModel();
            StatutModel statuSelect = new StatutModel();
            TaxeModel taxeSelect = new TaxeModel();

            try
            {
                Facture nf = DAL.GetAll_FACTUREBYID(id, idSite);
                if (nf != null)
                {
                    facture = Converfrom(nf);

                    if (nf.CurrentStatut.ShortName == "1")
                        facture.backGround = "Gray";
                    if (nf.CurrentStatut.ShortName == "2")
                        facture.backGround = "#FF77AED8";
                    if (nf.CurrentStatut.ShortName == "3")
                        facture.backGround = "#FFD1C683";
                    if (nf.CurrentStatut.ShortName == "4")
                        facture.backGround = "#FF8888C4";
                    if (nf.CurrentStatut.ShortName == "5")
                        facture.backGround = "#FF9CBE44";

                    if (nf.CurrentStatut.ShortName == "6")
                        facture.backGround = "Red";
                    if (nf.CurrentStatut.ShortName == "7")
                        facture.backGround = "Red";
                    facture.ClienOk = true;
                    facture.ClientbackGround = "White";

                    //if (Global.Utils.IsClientComplet(nf.CurrentClient))
                    //{
                    //    facture.ClienOk = true;
                    //    facture.ClientbackGround = "White";
                    //}
                    //else
                    //{
                    //    facture.ClienOk = false;
                    //    facture.ClientbackGround = "Red";
                    //}

                    if (nf.CurrentStatut.ShortName == "4")
                    {
                        facture.icon =4 ;
                    }
                    if (nf.CurrentStatut.ShortName == "1" || nf.CurrentStatut.ShortName == "2" || nf.CurrentStatut.ShortName == "3")
                    {
                        facture.icon = 2;
                    }
                    if (nf.CurrentStatut.ShortName == "5" || nf.CurrentStatut.ShortName == "6" || nf.CurrentStatut.ShortName == "7")
                    {
                        facture.icon = 3;
                    }
                    
                      
                    

                   
                    facture.CurrentClient = ConvertFromClient(nf.CurrentClient);
                    facture.CurrentClient.Compte = ConvertfromCmpt(nf.CurrentClient.Ccompte);
                    facture.CurrentClient.Exonerere = ConvertfromExo(nf.CurrentClient.Exonerate);
                  
                    facture.CurrentClient.DeviseConversion = Convertfromdevise(nf.CurrentClient.DeviseConversion);
                    facture.CurrentClient.DeviseFacture = Convertfromdevise(nf.CurrentClient.DeviseFacture);
                    facture.CurrentClient.Porata = ConvertfromProrata(nf.CurrentClient.Porata);
                   // facture.CurrentClient.LibelleTerme = convertfromLibelle(nf.CurrentClient.LibelleTerme);
                    facture.CurrentClient.Llangue = new LangueModel { Id = nf.CurrentClient.Llangue.IdLangue, Libelle = nf.CurrentClient.Llangue.Libelle, Shortname = nf.CurrentClient.Llangue.Shorname };
                    facture.CurrentDevise = ConvertfromDevise(nf.CurrentDevise);
                    facture.CurrentExploitation = Converfromexploit(nf.CurrentExploitationFacture);
                    facture.CurrentModePaiement = ConverfromModeP(nf.CurrentModePaiement);
                    facture.CurrentObjetFacture = ConverfromObjet(nf.CurrentObjetFacture);
                    facture.CurrentStatut = ConverfromStatut(nf.CurrentStatut);
                    facture.CurrentTaxe = ConvertfromTaxe(nf.CurrentTaxe);
                    facture.UserCreate = ConverfromUser(nf.UserCreate);
                    facture.CurrentExploitation = Converfromexploit(nf.CurrentExploitationFacture);
                    facture.CurrentDepartement = ConvertFromddept(nf.CurrentDepartement);
                   

                }
                return facture;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

      

        public bool FACTURE_ADD(FactureModel facture,out  object idFacture )
        {
            object newidfacture = null;
            try
            {
                if (facture != null)
                    DAL.FACTURE_ADD(ConvertTo(facture), out  newidfacture);
                idFacture = newidfacture;
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

     //public bool FACTURE_UPDATE(Facture objFacture)
       // {

        public bool FACTURE_UPDATE(ref string oldfactureMax,ref string newfacture, FactureModel facture)
        {
            
            try
            {
                if (facture != null)
                  DAL.FACTURE_UPDATE(ref oldfactureMax, ref newfacture,ConvertTo(facture));
               
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool FACTURE_UPDATE_WITHOUUPDATE(ref string oldfactureMax, ref string newfacture, FactureModel facture)
        {

            try
            {
                if (facture != null)
                    DAL.FACTURE_UPDATE_WITHOUT_UPDATE(ref oldfactureMax, ref newfacture, ConvertTo(facture));

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool FACTURE_UPDATE( FactureModel facture)
        {

            try
            {
                if (facture != null)
                    DAL.FACTURE_UPDATE( ConvertTo(facture));

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool FACTURE_ADD_LOG(FactureModel facture)
        {

            try
            {
                if (facture != null)
                {
                    Facture newFacture = ConvertTo(facture);
                    newFacture.DateCreation =(DateTime) facture.DateCreation;
                    DAL.FACTURE_ADD_IMPORT(newFacture);
                }

                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public object  FACTURE_ADD_LIGNEGFACTURE(FactureModel facture, List <LigneFactureModel> lignefac,bool esNormalAvoir)
        {
            object val = null;
            try
            {
                if (facture != null)
                    val = DAL.FACTURE_ADD_ITEMS(ConvertTo(facture), ConvertToLigne(lignefac), esNormalAvoir);

                return val;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public bool FACTURE_DELETE(long id, int estDel, int idSite, int idUser)
        {

            try
            {
                if (id > 0)
                    DAL.FACTURE_DELETE(id, estDel, idSite, idUser);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool FACTURE_VALIDATION(long id, DateTime dateclose, Int32 idstatut,int userModif, bool ops,FactureModel facture)
        {

            try
            {
                if (id > 0)
                    DAL.FACTURE_CLOSING(id, dateclose, idstatut, userModif, ops, ConvertTo(facture));
                return true;

            }
            catch (Exception des)
            {
                throw new Exception(des.Message);
            }
        }

        //public bool FACTURE_CLOSING_UPDATE(FactureModel facture,MySqlTransaction transaction)
        //{

        //    try
        //    {
               
        //            DAL.FACTURE_CLOSING_UPDATE(ConvertTo(facture));
        //        return true;

        //    }
        //    catch (Exception de)
        //    {
        //        throw new Exception(de.Message);
        //    }
        //}


        public bool FACTURE_NONVALABLE(long id, Int32 idstatut, int userModif, bool ops)
        {

            try
            {
                if (id > 0)
                    DAL.FACTURE_NONVALABLE(id, idstatut, userModif, ops);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool FACTURE_SORTIE(long id, DateTime date, Int32 idstatut, int userModif, bool ops)
        {

            try
            {
                if (id > 0)
                    DAL.FACTURE_SORTIE(id, date, idstatut, userModif, ops);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool FACTURE_SUSPENSION(long id, Int32 idstatut, int userModif, bool ops)
        {

            try
            {
                if (id > 0)
                    DAL.FACTURE_SUSPENSION(id, DateTime.Now, idstatut, userModif, ops);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public bool FACTURE_PAIEMENT(long id, DateTime date, int userModif, bool ops)
        {

            try
            {
                if (id > 0)
                    DAL.FACTURE_PAIEMENT(id, date, userModif, ops);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }

        public bool FACTURE_UPDATE_STATUS(long id, Int32 idstatut, int userModif, Int32 ops)
        {

            try
            {
                if (id > 0)
                    DAL.FACTURE_UPDATE_STATUS(id, idstatut, userModif, ops);
                return true;

            }
            catch (Exception de)
            {
                throw new Exception(de.Message);
            }
        }


        public  DataTable LISTE_FACTUTE_TO_DEL(int idSite)
        {
           
            try
            {
                DataTable table = DAL.LISTE_FACTURE_TO_DEL (idSite);
                return table;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }

        public void VERIF_FACTURE_HHTC(Int64 idFActure)
        {

            try
            {
                 DAL.FACTURE_ADD_VerifHTTC(idFActure);
               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }

        }
        #endregion

        #region BUSNESS METHOD

         DepartementModel ConvertFromddept(Departement dep)
        {
            DepartementModel newdep = null;
            if (dep != null)
            {
                newdep = new DepartementModel { IdDep = dep.IdDepartement, Libelle = dep.Libelle, CourtLibelle = dep.CourtLibelle, Autre = dep.Autre , IdSite=dep .IdSite  };
            }
            return newdep;
        }
       List <LigneFacture> ConvertToLigne(List <LigneFactureModel> fact)
        {
            List <LigneFacture> newFact = new List<LigneFacture> ();
            LigneFacture ligne = null;
            if (fact != null)
                foreach (var ll in fact)
                {
                    ligne = new LigneFacture
                    {
                        IdLigneFacture = ll.IdLigneFacture,
                        Description = ll.Description,
                        Exonere = ll.Exonere,
                        IdFacture = ll.IdFacture,
                        IdProduit = ll.IdProduit,
                        IdDetailProduit = ll.IdDetailProduit,
                        IdUtilisateur = ll.IdUtilisateur,
                        Quantite = (decimal)ll.Quantite, 
                        Prixunit =ll .PrixUnitaire ,
                        MontantHT =ll .MontanHT ,
                         Idexploitation=ll.IdExploitation, 
                         SpecialFacture=ll.SpecialFacture, 
                         ProduitLabel=ll.ProduitLabel
                    };
                    newFact.Add(ligne);
                }
            return newFact;

        }


        UtilisateurModel ConverfromUser(Utilisateur user)
        {
            UtilisateurModel newUser = null;
            if (user != null)
                newUser = new UtilisateurModel
                {
                    Id = user.IdUtilisateur,
                    Nom = user.Nom,
                    Prenom = user.Prenom,
                    Fonction = user.Fonction,
                    Loggin = user.Loggin,
                    IdProfile = user.IdProfil,
                    Password = user.Password, IdSite =user .IdSite 
                };
            return newUser;

        }

        TaxeModel ConvertfromTaxe(Taxe taxe)
        {
            TaxeModel newdevise = null;
            if (taxe != null)
                newdevise = new TaxeModel { ID_Taxe = taxe.ID_Taxe, Libelle = taxe.Libelle, Taux = taxe.Taux, IdSite =taxe .IdSite, TaxeDefault=taxe.Taxedefault  };
            return newdevise;

        }

        DepartementModel ConvertFrom(Departement dep)
        {
            DepartementModel newdep = null;
            if (dep != null)
            {
                newdep = new DepartementModel { IdDep = dep.IdDepartement, Libelle = dep.Libelle, CourtLibelle = dep.CourtLibelle, Autre = dep.Autre,IdSite  =dep .IdSite  };
            }
            return newdep;
        }

        StatutModel ConverfromStatut(StatutFacture efacture)
        {
            StatutModel newFact = null;
            if (efacture != null)
                newFact = new StatutModel
                {
                    IdStatut = efacture.IdStatut,
                    IdLangue = efacture.IdLangue,
                    Libelle = efacture.Libelle,
                    CourtDesc = efacture.ShortName 


                };
            return newFact;

        }

        ObjetFactureModel ConverfromObjet(ObjetFacture efacture)
        {
            ObjetFactureModel newFact = null;
            if (efacture != null)
                newFact = new ObjetFactureModel
                {
                    IdObjet = efacture.IdObjet,
                    Libelle = efacture.Libelle,
                    IdClient = efacture.IdClient,
                    IdobjetGen = efacture.IdobjetGen 


                };
            return newFact;

        }

        ModePaiementModel ConverfromModeP(ModePaiement mode)
        {
            ModePaiementModel newFact = null;
            if (mode != null)
                newFact = new ModePaiementModel
                {
                    IdMode = mode.IdMode,
                    IdLangue = mode.IdLangue,
                    Libelle = mode.Libelle,


                };
            return newFact;

        }

        ExploitationFactureModel Converfromexploit(ExploitationFacture efacture)
        {
            ExploitationFactureModel newFact = null;
            if (efacture != null)
                newFact = new ExploitationFactureModel
                {
                    IdExploitation = efacture.IdExploitation,
                    IdLangue = efacture.IdLangue,
                    Libelle = efacture.Libelle, 
                    IdSite =efacture .IdSite , 
                    IdClient =efacture .IdClient 


                };
            return newFact;

        }
        DeviseModel ConvertfromDevise(Devise devise)
        {
            DeviseModel newdevise = null;
            if (devise != null)
                newdevise = new DeviseModel { ID_Devise = devise.ID_Devise, Libelle = devise.Libelle, Taux = devise.Taux, IdSite =devise.IdSite, IsDefault=devise.IsDefault  };
            return newdevise;

        }

        FactureModel  Converfrom(Facture facture)
        {
            FactureModel newFact = null;
            if (facture != null)
                newFact = new FactureModel
                {
                    IdFacture = facture.IdFacture,
                    IdCreerpar = facture.IdCreerpar,

                    IdModifierPar = facture.IdModifierPar,
                    IdDevise = facture.IdDevise,
                    DateCreation = facture.DateCreation,
                    IdModePaiement = facture.IdModePaiement,
                    IdObjetFacture = facture.IdObjetFacture,
                    IdStatut = facture.IdStatut,
                    IdTaxe = facture.IdTaxe,
                    MoisPrestation = facture.MoisPrestation,
                    DateCloture = facture.DateCloture,
                    IdClient = facture.IdClient,
                    NumeroFacture = facture.NumeroFacture,
                    DateEcheance = facture.DateEcheance,
                    DateFacture = facture.DateFacture,
                    DateSortie = facture.DateSortie,
                    DateSuspension = facture.DateSuspension,
                    IdSite = facture.IdSite,
                    IsProrata = facture.IsProrata,
                     IdClientLangue=facture.IdClientLangue,
                    JourFinEcheance = facture.JourFinEcheance,
                    DateDepot = facture.DateDepot,
                    IdExploitation = facture.Idexploitation,
                    isfactureValide = facture.factureValide,
                    Label_objet = facture.LibelleObjet,
                    Label_Dep = facture.Libelle_Dep,
                    IdDepartement = (int)facture.IdDepartement,
                    TotalTTC = facture.TotalTTC,
                    TotalHT = facture.TotalHT,
                    TotalPRORATA = facture.TotalPRORATA,
                    TotalTVA = facture.TotalTVA,
                    DateNonValable = facture.DateNonValide,
                    DatePaiement = facture.DatePaiement,
                    DateModif = facture.DateModification,
                    ExploitationIds = facture.ExploitationIds,
                    ExploitationList = facture.ExploitationList,
                    LibelleUser = facture.LibelleUser,
                    LibelleBackgorund = facture.LibelleClientBackgorund,
                    LibelleClient = facture.LibelleClient,
                    LibelleClientBackgorund = facture.LibelleClientBackgorund,
                    LibelleClientOk = facture.LibelleClientOk,
                    LibelleStatut = facture.LibelleStatut,
                    LibelleClientObjet = facture.LibelleClientObjet,
                    LibelleDepartement = LibelleDepartement,
                    //LibelleSIte = facture.LibelleStatut,
                    //LibelleDevise = facture.LibelleDevise,
                    LibelleUserNom = facture.LibelleUserNom,
                    LibelleUserPrenom = facture.LibelleUserPrenom,
                    //LibelleDeviseConversion = facture.LibelleDeviseConversion,
                    //IdDeviseconversion = facture.IdDeviseconversion,
                    //Idlangue = facture.Idlangue,
                    //LibelleTermeJour = facture.LibelleTermeJour,
                    //LibelleTerme = facture.LibelleTerme,
                    MaregeBeneficiaireId = facture.MaregeBeneficiaireId,
                    TotalMarge = facture.TotalMarge,
                    TauxMargeBeneficiaire = facture.TauxMargeBeneficiaire,
                    //LibelleTaxe = facture.LibelleTaxe,
                    //LibelleTauxTaxe = facture.LibelleTauxTaxe,
                    LibelleDeviseConversion = facture.LibelleDeviseConversion,
                    LibelleTauxTaxe = facture.LibelleTauxTaxe,
                    DeviseTAux = facture.DeviseTaux,
                    IdProrata = facture.IdProrata,
                    OrderNumber = facture.OrderNumber,
                    CompteAnalytique = facture.CompteAnalytique,
                    CompteTier = facture.CompteTier
                    //LibelleTauxProtata=facture.LibelleTauxProtata
                    
                };
      
            return newFact;

        }

        FactureModel ConverfromNew(Facture facture)
        {
            FactureModel newFact = null;
            if (facture != null)
                newFact = new FactureModel
                {
                    IdFacture = facture.IdFacture,
                    IdCreerpar = facture.IdCreerpar,
                     IsCheck=false,
                    //IdModifierPar = facture.IdModifierPar,
                    //IdDevise = facture.IdDevise,
                    DateCreation = facture.DateCreation,
                    //IdModePaiement = facture.IdModePaiement,
                    IdObjetFacture = facture.IdObjetFacture,
                    IdStatut = facture.IdStatut,
                    IdTaxe = facture.IdTaxe,
                    MoisPrestation = facture.MoisPrestation,
                    DateCloture = facture.DateCloture,
                    IdClient = facture.IdClient,
                    NumeroFacture = facture.NumeroFacture,
                    //DateEcheance = facture.DateEcheance,
                    DateFacture = facture.DateFacture,
                    DateSortie = facture.DateSortie,
                    DateSuspension = facture.DateSuspension,
                    IdSite = facture.IdSite,
                    IsProrata = facture.IsProrata,
                     LibelleDepartement=facture.Libelle_Dep,
                    JourFinEcheance = facture.JourFinEcheance,
                    DateDepot = facture.DateDepot,
                    IdExploitation = facture.Idexploitation,
                     IdClientLangue=facture.IdClientLangue,
                      IdClientExoneration=facture.IdClientExoneration,
                    ExploitationIds = facture.ExploitationIds,
                    ExploitationList = facture.ExploitationList,
                     
                    //isfactureValide = facture.factureValide,
                    //Label_objet = facture.LibelleObjet,
                    Label_Dep = facture.Libelle_Dep,
                    IdDepartement = (int)facture.IdDepartement,
                    TotalTTC = facture.TotalTTC,
                    TotalHT = facture.TotalHT,
                    TotalPRORATA = facture.TotalPRORATA,
                    TotalTVA = facture.TotalTVA,
                    DateNonValable = facture.DateNonValide,
                    DatePaiement = facture.DatePaiement,
                    //DateModif = facture.DateModification,
                    //ExploitationIds = facture.ExploitationIds,
                    //ExploitationList = facture.ExploitationList,
                    LibelleUser = facture.LibelleUser,
                    LibelleBackgorund = facture.LibelleBackgorund,
                    LibelleClient = facture.LibelleClient,
                    LibelleClientBackgorund = facture.LibelleClientBackgorund,
                    LibelleClientOk = facture.LibelleClientOk,
                    LibelleStatut = facture.LibelleStatut,
                    LibelleClientObjet = facture.LibelleClientObjet,
                    MaregeBeneficiaireId=facture.MaregeBeneficiaireId,
                    TotalMarge = facture.TotalMarge,
                    TauxMargeBeneficiaire = facture.TauxMargeBeneficiaire,
                    //LibelleTaxe = facture.LibelleTaxe,
                    LibelleDeviseConversion = facture.LibelleDeviseConversion,
                    LibelleTauxTaxe = facture.LibelleTauxTaxe,
                    DeviseTAux = facture.DeviseTaux,
                    IdProrata = facture.IdProrata,
                    OrderNumber=facture.OrderNumber,
                    CompteAnalytique = facture.CompteAnalytique,
                    CompteTier = facture.CompteTier
                   // LibelleTauxProtata = facture.LibelleTauxProtata

                };
            return newFact;

        }

        Facture ConvertTo(FactureModel fact)
        {
            Facture newFact = null;
            if (fact  != null)
                newFact = new Facture
                {
                    IdFacture = fact.IdFacture,
                    IdCreerpar = fact.IdCreerpar,
                    DateCreation =fact.DateCreation.Value    ,
                    IdModifierPar = fact.IdModifierPar,
                    IdDevise = fact.IdDevise,
                    IdModePaiement = fact.IdModePaiement,
                    IdObjetFacture = fact.IdObjetFacture,
                    IdStatut = fact.IdStatut,
                    IdTaxe = fact.IdTaxe,
                    MoisPrestation = fact.MoisPrestation,
                    DateCloture = fact.DateCloture,
                    IdClient = fact.IdClient,
                    NumeroFacture = fact.NumeroFacture,
                    DateEcheance =fact.DateEcheance !=null ?(DateTime)fact.DateEcheance:DateTime .Now  ,
                    DateSortie = fact.DateSortie,
                    DateSuspension = fact.DateSuspension , 
                    DateFacture =DateTime .Now  ,
                    IdSite =fact .IdSite ,
                    IsProrata =fact .IsProrata, 
                    JourFinEcheance =fact .JourFinEcheance , 
                    DateDepot =fact .DateDepot , 
                    Idexploitation =fact .IdExploitation ,
                    factureValide =fact .isfactureValide , 
                    LibelleObjet =fact .Label_objet , 
                     Libelle_Dep  =fact .Label_Dep  ,
                     IdDepartement =fact .IdDepartement , 
                     TotalTTC =fact .TotalTTC,
                    TotalHT = fact.TotalHT,
                    TotalPRORATA = fact.TotalPRORATA,
                    TotalTVA = fact.TotalTVA,
                     DateNonValide =fact .DateNonValable , 
                     DatePaiement =fact .DatePaiement  , 
                     ExploitationIds=fact.ExploitationIds, 
                     ExploitationList=fact.ExploitationList,
                      MaregeBeneficiaireId=fact.MaregeBeneficiaireId,
                    
                    LibelleUser = fact.LibelleUser,
                    LibelleBackgorund = fact.LibelleClientBackgorund,
                    LibelleClient = fact.LibelleClient,
                    LibelleClientBackgorund = fact.LibelleClientBackgorund,
                    LibelleClientOk = fact.LibelleClientOk,
                    LibelleStatut = fact.LibelleStatut,
                    LibelleClientObjet = fact.LibelleClientObjet,
                    LibelleDepartement = LibelleDepartement,
                    //LibelleSIte = fact.LibelleSIte,
                    //LibelleDevise = fact.LibelleDevise,
                    LibelleUserNom = fact.LibelleUserNom,
                    LibelleUserPrenom = fact.LibelleUserPrenom,
                    LibelleDeviseConversion = fact.LibelleDeviseConversion,
                    LibelleTauxTaxe = fact.LibelleTauxTaxe,
                    DeviseTaux = fact.DeviseTAux,
                    //Idlangue = fact.Idlangue,
                    //LibelleTermeJour = fact.LibelleTermeJour,
                    //LibelleTerme = fact.LibelleTerme,
                    TotalMarge = fact.TotalMarge,
                    TauxMargeBeneficiaire = fact.TauxMargeBeneficiaire,
                    //LibelleTaxe = fact.LibelleTaxe,
                  
                    IdProrata = fact.IdProrata,
                    OrderNumber=fact.OrderNumber
                    //LibelleTauxProtata = fact.LibelleTauxProtata

 
                };
            return newFact;

        }

        ClientModel ConvertFromClient(Client client)
        {
            ClientModel newClient = null;
            if (client != null)
                newClient = new ClientModel
                {
                    IdClient = client.IdClient,
                    IdLangue = client.IdLangue,
                    NomClient = client.NomClient,
                    NumeroContribuable = client.NumeroContribuable,
                    Rue1 = client.Rue1,
                    Rue2 = client.Rue2,
                    Ville = client.Ville,
                    DateEcheance = client.DateEcheance,
                    Idporata = client.Idporata,
                     TermeNombre = client.TermeNombre,
                    TermeDescription = client.TermeDescription,
                    BoitePostal = client.BoitePostal,
                    IdCompte = client.IdCompte,
                    IdSite = client.IdSite,
                    IdExonere = client.IdExonere,
                    NumemroImat = client.NumeroImatriculation , 
                    IdTerme =client .IdTerme, 
                    IdDeviseFact =client .IdDeviseFact  ,
                     IsActive=client.IsActive, NomClientCompta=client.NomClientCompta,
                     IdDeviseConversion=client.IdConversionFact, Compteproduit=client.CompteProduit
                   
                  
                    
                };
            return newClient;

        }

         LibelleTermeModel convertfromLibelle(Libelle_Terme libelle)
        {
            LibelleTermeModel newdevise = null;
            if (libelle != null)
                newdevise = new LibelleTermeModel
                {
                    ID = libelle.ID,
                    Desciption = libelle.Desciption,
                    CourtDescription = libelle.CourtDesc,
                    Jour = libelle.Jour
                };
            return newdevise;
        }

         TaxeModel ConvertfromProrata(Taxe taxe)
        {
            TaxeModel newdevise = null;
            if (taxe != null)
                newdevise = new TaxeModel { ID_Taxe = taxe.ID_Taxe, Libelle = taxe.Libelle, Taux = taxe.Taux, TaxeDefault=taxe.Taxedefault };
            return newdevise;

        }

         DeviseModel Convertfromdevise(Devise devise)
        {
            DeviseModel newdevise = null;
            if (devise != null)
                newdevise = new DeviseModel { ID_Devise = devise.ID_Devise, Libelle = devise.Libelle, Taux = devise.Taux, IdSite =devise.IdSite, IsDefault=devise.IsDefault  };
            return newdevise;

        }

         ExonerationModel ConvertfromExo(Exoneration exo)
        {
            ExonerationModel newExo = null;
            if (exo != null)
                newExo = new ExonerationModel { ID = exo.ID, Libelle = exo.Libelle, CourtDesc = exo.ShortName };
            return newExo;

        }

         CompteModel ConvertfromCmpt(Compte compte)
        {
            CompteModel comptes = null;
            if (compte != null)
                comptes = new CompteModel
                {
                    ID = compte.ID,
                    IDSite = compte.IDSite,
                    NumeroCompte = compte.NumeroCompte,
                    NomBanque = compte.NomBanque,
                    Ville = compte.Ville,
                    Agence = compte.Agence,
                    BoitePostal = compte.BoitePostal,
                    Pays = compte.Pays,
                    Rue = compte.Rue,
                    Telephone = compte.Telephone,
                    Quartier = compte.Quartier
                };
            return comptes;

        }
       
        #endregion
    }
}
