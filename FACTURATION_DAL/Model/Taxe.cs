using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Taxe
    {
        public int ID_Taxe { get; set; }
        public string Libelle { get; set; }
        public string Taux { get; set; }
        public int IdSite { get; set; }
        public bool Taxedefault { get; set; }

    }
}
