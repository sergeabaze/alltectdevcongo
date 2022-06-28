using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
  public   class OverviewFacture
    {

      public int Idice { get; set; }
      public Int64 Idfacture { get; set; }
      public int IdClient { get; set; }
      public int Iduser { get; set; }
      public string NumeroFacture { get; set; }
      public string NomClient { get; set; }
      public string statut { get; set; }
      public int IdStatut { get; set; }
      public DateTime Dateoperation { get; set; }
    }
}
