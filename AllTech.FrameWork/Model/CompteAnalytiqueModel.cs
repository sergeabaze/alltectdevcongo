using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class CompteAnalytiqueModel : ViewModelBase
    {
        private int idCompteAnalytique;
        private string numerocompte;
        private string code;
        private string libelle;

      

      
        CompteAnalytique dale = null;
       public CompteAnalytiqueModel()
       {
           dale = new CompteAnalytique(DataProviderObject.GetStringConnection);
       }

       #region Properties

       public string Code
       {
           get { return code; }
           set { code = value;
             this.OnPropertyChanged("Code");
           }
       }

       public string Libelle
       {
           get { return libelle; }
           set { libelle = value;
           this.OnPropertyChanged("Libelle");
           }
       }

       public int IdCompteAnalytique
       {
           get { return idCompteAnalytique; }
           set { idCompteAnalytique = value;
           this.OnPropertyChanged("IdCompteAnalytique");
           }
       }


       public string Numerocompte
       {
           get { return numerocompte; }
           set { numerocompte = value;
           this.OnPropertyChanged("Numerocompte");
           }
       }
        #endregion

       #region Methods

       public CompteAnalytiqueModel ModelCompteAnal_SelectById(int id)
       {
           var compte = dale.SelectByid(id);
           CompteAnalytiqueModel cmpt = null;
           if (compte != null)
           {
               cmpt = new CompteAnalytiqueModel();
               cmpt.IdCompteAnalytique = compte.IdCompteAnalytique;
               cmpt.Numerocompte = compte.Numerocompte;
               cmpt.Code = compte.Code;
               cmpt.Libelle = compte.Libelle;
           }
           return cmpt;
       }


       public List<CompteAnalytiqueModel> ModelCompteAnal_SelectAll(int idSite)
       {
           var compteDale = dale.SelectAll(idSite);

           List<CompteAnalytiqueModel> listes = null;
           CompteAnalytiqueModel cmpt = null;
           if (compteDale != null && compteDale.Count > 0)
           {
               

               listes = new List<CompteAnalytiqueModel>();
               CompteAnalytiqueModel cmpts = new CompteAnalytiqueModel();
               cmpts.IdCompteAnalytique = 0;
               cmpts.Libelle = "...";
               cmpts.Numerocompte = "...";

               listes.Add(cmpts);

               foreach (CompteAnalytique compte in compteDale)
               {
                   cmpt = new CompteAnalytiqueModel();
                   cmpt.IdCompteAnalytique = compte.IdCompteAnalytique;
                   cmpt.Code = compte.Code;
                   cmpt.Numerocompte = compte.Numerocompte;
                   cmpt.Libelle = compte.Libelle;
                   listes.Add(cmpt);
               }

           }

           return listes;
       }


       public bool ModelCompteGeneral_Insert(CompteAnalytiqueModel compte, int idSite)
       {
           bool values = false;
           CompteAnalytique cmpt = new CompteAnalytique();
           cmpt.IdCompteAnalytique = compte.IdCompteAnalytique;
           cmpt.Numerocompte = compte.Numerocompte;
           cmpt.Code = compte.Code;
           cmpt.Libelle = compte.Libelle;
           if (dale.Insert(cmpt, idSite))
               values = true;
           else values = false;
           return values;
       }

       public bool ModelCompteAnal_Update(CompteAnalytiqueModel compte)
       {
           bool values = false;
           CompteAnalytique cmpt = new CompteAnalytique();
           cmpt.IdCompteAnalytique = compte.IdCompteAnalytique;
           cmpt.Numerocompte = compte.Numerocompte;
           cmpt.Code = compte.Code;
           cmpt.Libelle = compte.Libelle;
           if (dale.Update(cmpt))
               values = true;
           else values = false;
           return values;
       }

       public bool ModelCompteAnal_Delete(int id)
       {
           bool values = false;
           if (dale.Delete(id))
               values = true;
           else values = false;
           return values;

       }
       #endregion
    }
}
