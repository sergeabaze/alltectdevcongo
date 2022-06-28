using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Compte
    {

        public int ID { get; set; }
        public int  IDSite { get; set; }
        public string NumeroCompte { get; set; }
        public string NomBanque { get; set; }
        public string Ville { get; set; }
        public string Agence { get; set; }
        public string Rue { get; set; }
        public string BoitePostal { get; set; }
        public string Telephone { get; set; }
        public string Pays { get; set; }
        public string Quartier { get; set; }
    }
}
