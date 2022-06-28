using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class CompteGenralModel : ViewModelBase
    {
        public int idCompteGen;
        public string libelle;
        private string code;
      

        CompteGeneral dale = null;

       public CompteGenralModel()
       {
           dale = new CompteGeneral(DataProviderObject.GetStringConnection);
       }

        #region Properties

     


       public int IdCompteGen
       {
           get { return idCompteGen; }
           set { idCompteGen = value;
           this.OnPropertyChanged("IdCompteGen");
           }
       }

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
        #endregion

        #region Methods

       public CompteGenralModel ModelCompteGeneral_SelectById(int id)
       {
           var compte = dale.SelectByid(id);
           CompteGenralModel cmpt = null;
           if (compte != null)
           {
               cmpt = new CompteGenralModel();
               cmpt.IdCompteGen = compte.IdCompteGen;
               cmpt.Libelle = compte.Libelle;
               cmpt.Code = compte.Code;
               
           }

           return cmpt;
       }


       public List<CompteGenralModel> ModelCompteGeneral_SelectAll(int idSite)
       {
           var compteDale = dale.SelectAll(idSite);
           
           List<CompteGenralModel> listes = null;
           CompteGenralModel cmpt = null;
           if (compteDale != null && compteDale.Count > 0)
           {
               listes = new List<CompteGenralModel>();
               foreach (CompteGeneral compte in compteDale)
               {
                   cmpt = new CompteGenralModel();
                   cmpt.IdCompteGen = compte.IdCompteGen;
                   cmpt.Libelle = compte.Libelle;
                   cmpt.Code = compte.Code;
                 
                   listes.Add(cmpt);
               }
              
           }

           return listes;
       }


       public bool ModelCompteGeneral_Insert( int idSite, int idcompteohada)
       {
           bool values = false;
           if (dale.Insert(idSite, idcompteohada))
               values = true;
           else values = false;
           return values;
       }

       public bool ModelCompteGeneral_Update(int id, int idCompteOhada)
       {
           bool values = false;

           if (dale.Update(id, idCompteOhada))
               values = true;
           else values = false;
           return values;
       }

    

       public bool ModelCompteGeneral_Delete(int id)
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
