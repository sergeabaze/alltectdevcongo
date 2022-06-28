using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class ProduitDetail
    {
       public int IdDetail { get; set; }
       public int IdClient { get; set; }
       public int IdProduit { get; set; }
       public float Quantite { get; set; }
       public bool  SpecialFact { get; set; }
       public string  NomProduit { get; set; }
       public decimal  PrixUnitaire { get; set; }
       public bool  Exonerer { get; set; }
       public bool IsProrata { get; set; }
       public string CompteOhada { get; set; }
       public int IdSite { get; set; }
       public int IdExploitation { get; set; }

     
       private Client client = new Client();

       public Client Client
       {
           get { return client; }
           set { client = value; }
       }

       private Produit produit;

       public Produit Produit
       {
           get { return produit; }
           set { produit = value; }
       }

       ExploitationFacture exploitation;

       public ExploitationFacture Exploitation
       {
           get { return exploitation; }
           set { exploitation = value; }
       }
    }
}
