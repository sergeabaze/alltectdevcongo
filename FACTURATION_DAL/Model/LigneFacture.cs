using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class LigneFacture
    {

       public long IdLigneFacture { get; set; }
       public long IdFacture { get; set; }
       public int IdProduit { get; set; }
       public int IdDetailProduit { get; set; }
       public int IdUtilisateur { get; set; }
       public int IdUtililModif { get; set; }
       public int IdSite { get; set; }
       public int IdClient { get; set; }
       public int IdStatut { get; set; }
     
       public string Description { get; set; }
       public decimal  Quantite { get; set; }
       public bool  Exonere { get; set; }
       public bool SpecialFacture { get; set; }
       public decimal  Prixunit { get; set; }
       public decimal   MontantHT { get; set; }
       public Int32 Idexploitation { get; set; }
       public DateTime?  DateCreation { get; set; }
       public DateTime? DateModif { get; set; }
       public bool isdlete  { get; set; }
       private Facture currentFacture;
       private Utilisateur currentUtilisateur;
       private Produit currentProduit;
       private ProduitDetail currentDetailProduit;

       //new facture valider
       public string LibelleProduit { get; set; }
       public decimal QuantiteUnitaireDet { get; set; }
       public decimal PrixUnitaireDet { get; set; }
       public bool IsProrata { get; set; }
       public string LibelleCompteproduit { get; set; }
       public Int32 IdExoneration { get; set; }
       public string Libelleexoneration { get; set; }
       public string TauxProrata { get; set; }
       public string TauxTva { get; set; }
       public bool IsPecialFacture { get; set; }
       public string CompteGeneProduit { get; set; }
       public string CodeCompteGeneProduit { get; set; }
       public string CompteAnalytique { get; set; }
       public string CodeCompteAnalytique { get; set; }
       public string ProduitLabel { get; set; }
       public string DetailProduitLAbel { get; set; }

       public ProduitDetail CurrentDetailProduit
       {
           get { return currentDetailProduit; }
           set { currentDetailProduit = value; }
       }

       public Produit CurrentProduit
       {
           get { return currentProduit; }
           set { currentProduit = value; }
       }
       

       public Utilisateur CurrentUtilisateur
       {
           get { return currentUtilisateur; }
           set { currentUtilisateur = value; }
       }
       

       public Facture CurrentFacture
       {
           get { return currentFacture; }
           set { currentFacture = value; }
       }
       
    }
}
