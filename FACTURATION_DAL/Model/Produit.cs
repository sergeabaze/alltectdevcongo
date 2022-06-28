using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Produit
    {

        public int IdProduit { get; set; }
        public string Libelle { get; set; }
        public int IdLangue { get; set; }
        public int IdSite { get; set; }
        public decimal PrixUnitaire { get; set; }
        private Langue _llangue;
        public string CompteOhada { get; set; }
        public string CompteExonere { get; set; }
        public int ModeFacturation { get; set; }
        private List<ProduitDetail> detailProduit = new List<ProduitDetail>();

        public List<ProduitDetail> DetailProduit
        {
            get { return detailProduit; }
            set { detailProduit = value; }
        }

        public Langue Llangue
        {
            get { return _llangue; }
            set { _llangue = value; }
        }

    }
}
