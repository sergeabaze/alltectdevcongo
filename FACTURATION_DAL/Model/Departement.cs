using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Departement
    {

        public int IdDepartement { get; set; }
        public string Libelle { get; set; }
        public string CourtLibelle { get; set; }
        public string Autre { get; set; }
        public int IdSite { get; set; }
    }
}
