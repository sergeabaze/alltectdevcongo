using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Devise
    {

       public int ID_Devise { get; set; }
       public string Libelle { get; set; }
       public string Taux { get; set; }
       public int IdSite { get; set; }
       public bool IsDefault { get; set; }
       public string Sigle { get; set; }
     


    }
}
