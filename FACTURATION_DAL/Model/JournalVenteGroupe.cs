using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
  public   class JournalVenteGroupe
    {
        public int  Id { get; set; }
        public string CodeJournal { get; set; }
        public DateTime Datefacture { get; set; }
        public string Numerofacture { get; set; }
        public double? MontantDebit { get; set; }
        public double? MontantCredit { get; set; }
        public string Libellefacture { get; set; }
        public string LibelleStatut { get; set; }
        public string LibelleStatutOperation { get; set; }
        public Int64 IdFactures { get; set; }
        public int IdStatut { get; set; }
       
   

        private List<JournalVentes> journalVentes = new List<JournalVentes>();
       
        public List<JournalVentes> JournalVentes
        {
            get { return journalVentes; }
            set { journalVentes = value; }
        }
    }
}
