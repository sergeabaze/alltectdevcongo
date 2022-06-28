using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FACTURATION_DAL.Model
{
   public  class Societe
    {
       public int IdSociete { get; set; }
       public string Libelle { get; set; }
       public string RaisonSocial { get; set; }
       public string TitreManager { get; set; }
       public string NomManager { get; set; }
       public string NumContribualbe { get; set; }
       public string NumImmatriculation { get; set; }
       public string SigleSite { get; set; }

       public string Pays { get; set; }
       public string Capital { get; set; }
       public string Ville { get; set; }
       public string Adesse_1 { get; set; }
       public string Adresse_2 { get; set; }
       public string BoitePostal { get; set; }
       public string Telephone { get; set; }
       public string Faxe { get; set; }
       public string SiteIntenet { get; set; }
       public string Rc { get; set; }
       public string NumeroNif { get; set; }
       public string Regime { get; set; }
       public string TaxeNu { get; set; }
       public byte[] LogoSociete { get; set; }
       public byte[] LogoPiedPage { get; set; }
    }
}
