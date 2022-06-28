using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
  public   class Utilisateur
    {
      public int IdUtilisateur { get; set; }
      public string Nom { get; set; }
      public string Prenom { get; set; }
      public string Fonction { get; set; }
      public string Loggin { get; set; }
      public string Password { get; set; }
      public Int32  IdProfil { get; set; }
      public string  Photo { get; set; }
      public bool  EstVerouiller { get; set; }
      public DateTime  DateConnection { get; set; }
      public int IdSite { get; set; }
      private Profile _profile = new Profile();

      public Profile Profile
      {
          get { return _profile; }
          set { _profile = value; }
      }
    }
}
