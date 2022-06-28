using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Facture
    {

        public long  IdFacture { get; set; }
        public string NumeroFacture { get; set; }
        public DateTime?  MoisPrestation { get; set; }
        public bool EstSupprimer { get; set; }
        public int IdObjetFacture { get; set; }
        public int Idexploitation { get; set; }
        public int IdClient { get; set; }
        public int IdTaxe { get; set; }
        public int IdDevise { get; set; }
        public int IdStatut { get; set; }
        public int IdModePaiement { get; set; }
        public int IdModifierPar { get; set; }
        public int IdCreerpar { get; set; }
        public int IdSite { get; set; }
        public int IdProrata { get; set; }
        public int IdClientLangue { get; set; }
        public int IdClientExoneration { get; set; }
        public int? MaregeBeneficiaireId { get; set; }
       
        public int? IdDepartement { get; set; }
        public bool  IsProrata { get; set; }
        public double  TotalTTC { get; set; }
        public double TotalHT{ get; set; }
        public Nullable<double> TotalMarge { get; set; }
        public Nullable<double> TotalTVA { get; set; }
        public Nullable<double> TotalPRORATA { get; set; }
        public string  LibelleObjet { get; set; }
        public string ExploitationIds { get; set; }
        public string ExploitationList { get; set; }
        public DateTime  DateCreation { get; set; }
        public DateTime DateModification { get; set; }
        public DateTime DateEcheance { get; set; }
        public DateTime? DateCloture { get; set; }
        public DateTime? DateSortie { get; set; }
        public DateTime? DateSuspension { get; set; }
        public DateTime? DateDepot { get; set; }
        public DateTime? DateNonValide { get; set; }
        public DateTime? DatePaiement { get; set; }
        public DateTime? DateFacture { get; set; }
        public string  JourFinEcheance { get; set; }
        public bool  factureValide { get; set; }
        private ObjetFacture currentObjetFacture;
        private ExploitationFacture currentExploitationFacture;
        private Client currentClient;
        private Utilisateur userCreate;
        private Utilisateur userModified;
        private StatutFacture currentStatut;
        private Taxe currentTaxe;
        public string LibelleUser { get; set; }
        public string LibelleClientObjet { get; set; }
        public string LibelleClient { get; set; }
        public string OrderNumber { get; set; }
        public string CompteTier { get; set; }
        public string CompteAnalytique { get; set; }

        //new; facture valider
       
        public string LibelleDepartement { get; set; }
        public string LibelleUserNom { get; set; }
        public string LibelleUserPrenom { get; set; }
        public string TauxMargeBeneficiaire { get; set; }
        public string LibelleStatut { get; set; }
        public string LibelleTauxTaxe { get; set; }
        public string LibelleDeviseConversion { get; set; }
        public string DeviseTaux { get; set; }

       
       private Taxe currentMarge;

        public string LibelleBackgorund { get; set; }
        public string LibelleClientOk { get; set; }
        public string LibelleClientBackgorund { get; set; }
       
       

      
        private Devise currentDevise;
        private ModePaiement currentModePaiement;
        private Departement currentDepartement; 
        public  string Libelle_Dep { get; set; }

      
        

        #region Propeties

        public Taxe CurrentMarge
        {
            get { return currentMarge; }
            set { currentMarge = value; }
        }

        public ModePaiement CurrentModePaiement
        {
            get { return currentModePaiement; }
            set { currentModePaiement = value; }
        }

        
        
      
        public Devise CurrentDevise
        {
            get { return currentDevise; }
            set { currentDevise = value; }
        }


        public Taxe CurrentTaxe
        {
            get { return currentTaxe; }
            set { currentTaxe = value; }
        }

        public StatutFacture CurrentStatut
        {
            get { return currentStatut; }
            set { currentStatut = value; }
        }

        public Utilisateur UserModified
        {
            get { return userModified; }
            set { userModified = value; }
        }

        public Utilisateur UserCreate
        {
            get { return userCreate; }
            set { userCreate = value; }
        }

        public Client CurrentClient
        {
            get { return currentClient; }
            set { currentClient = value; }
        }

        public ExploitationFacture CurrentExploitationFacture
        {
            get { return currentExploitationFacture; }
            set { currentExploitationFacture = value; }
        }

        public ObjetFacture CurrentObjetFacture
        {
            get { return currentObjetFacture; }
            set { currentObjetFacture = value; }
        }


        public Departement CurrentDepartement
        {
            get { return currentDepartement; }
            set { currentDepartement = value; }
        }

        #endregion



    }
}
