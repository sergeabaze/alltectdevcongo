using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Client
    {

        public int IdClient { get; set; }
        public string NomClient { get; set; }
        public string CodeClient { get; set; }
        public string NumeroContribuable { get; set; }
        public string Ville { get; set; }
        public string BoitePostal { get; set; }
        public string Rue1 { get; set; }
        public string Rue2 { get; set; }
        public Int32  TermeNombre { get; set; }
        public string TermeDescription { get; set; }
        public int? IdCompteTiers { get; set; }
        public string NumeroImatriculation { get; set; }
        public int  Idporata { get; set; }
        public int IdDeviseFact { get; set; }
        public int IdConversionFact { get; set; }
        public int IdTerme { get; set; }
        public string   DateEcheance { get; set; }
        public bool  OffShore { get; set; }
        public bool IsActive { get; set; }
       
        public string NomClientCompta { get; set; }
        public Int32  IdLangue { get; set; }
        public Int32 IdExonere { get; set; }
        public Int32 IdCompte { get; set; }
        public Int32 IdSite { get; set; }
        public Int32 IdCompteGen { get; set; }
        private Langue _llangue;
        private Taxe _porata;
        private Compte _ccompte;
        private Exoneration _exonerate;
        private Devise _deviseConversion;
        public string CompteProduit { get; set; }

      
        private Devise _deviseFacture;

      
        private Libelle_Terme libelleTerme;
        private CompteTiers compteTiers;
        private List<CompteAnalClient> comptesAnalityques;


        public List<CompteAnalClient> ComptesAnalityques
        {
            get { return comptesAnalityques; }
            set { comptesAnalityques = value; }
        }
        public CompteTiers CompteTiers
        {
            get { return compteTiers; }
            set { compteTiers = value; }
        }
        

        public Libelle_Terme LibelleTerme
        {
            get { return libelleTerme; }
            set { libelleTerme = value; }
        }

        public Devise DeviseConversion
        {
            get { return _deviseConversion; }
            set { _deviseConversion = value; }
        }

        public Devise DeviseFacture
        {
            get { return _deviseFacture; }
            set { _deviseFacture = value; }
        }

        public Exoneration Exonerate
        {
            get { return _exonerate; }
            set { _exonerate = value; }
        }


        public Compte Ccompte
        {
            get { return _ccompte; }
            set { _ccompte = value; }
        }
      
        public Taxe Porata
        {
            get { return _porata; }
            set { _porata = value; }
        }

        public Langue Llangue
        {
            get { return _llangue; }
            set { _llangue = value; }
        }

    }
}