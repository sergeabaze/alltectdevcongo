using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Profile
    {
        public int idProfile { get; set; }
        public string Libelle { get; set; }
        public string ShortName { get; set; }
       
        private List<Droit> listeDroits = new List<Droit>();

        public List<Droit> ListeDroits
        {
            get { return listeDroits; }
            set { listeDroits = value; }
        }
       
       
    }
}
