using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
  public class JournalVentesDates
    {

      public int ID { get; set; }
      public int IdSite { get; set; }
      public DateTime DateDebut { get; set; }
      public DateTime DateFin { get; set; }
      public string NumeroJournal { get; set; }
      public int? NbreFactures { get; set; }
    }
}
