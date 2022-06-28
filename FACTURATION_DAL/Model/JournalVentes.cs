using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class JournalVentes
    {
        public int Id { get; set; }
        public int IdClient { get; set; }
        public int IdProduit { get; set; }
        public string NumCompteGeneral { get; set; }
        public string NumeroCompteTiers { get; set; }
        public string NumeroCompteAnalytique { get; set; }
        public string LibelleMontant { get; set; }
        public string LibelleSectionAnal { get; set; }
        public string LibelleOpertion { get; set; }
        public string LibelleClient { get; set; }
        public string StatutOperation { get; set; }
        public string CodeJournal { get; set; }
        public string NumeroJournal { get; set; }
        public string NumeroFacture { get; set; }
        public DateTime Datefacture { get; set; }
        public double? MontantDebit { get; set; }
        public double? MontantCredit { get; set; }
        public string Ordre { get; set; }
        public int? IdStatut { get; set; }
        public int? IdDate { get; set; }

       
       
     
     
    }
}
