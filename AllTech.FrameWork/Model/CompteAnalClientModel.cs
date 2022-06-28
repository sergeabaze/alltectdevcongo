using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class CompteAnalClientModel : ViewModelBase
    {
        public int Id {get;set;}
        private int idClient;
        private int idCompteAnal;
        private CompteAnalytiqueModel compteAnalyTique;
        private List<CompteAnalClientModel> compteAnalyTiques=new List<CompteAnalClientModel> ();

     
        CompteAnalClient dale = null;
       public CompteAnalClientModel()
       {
           dale = new CompteAnalClient(DataProviderObject.GetStringConnection);
       }

       public CompteAnalClientModel(int idClient)
       {
         CompteAnalClient compteDale=  new CompteAnalClient(idClient,DataProviderObject.GetStringConnection);
         List<CompteAnalClientModel> liste = new List<CompteAnalClientModel>();

         if (compteDale.CompteAnalyTiques != null)
         {
             CompteAnalClientModel compteModel = null;
             foreach (CompteAnalClient compte in compteDale.CompteAnalyTiques)
             {
                 compteModel = new CompteAnalClientModel();
                 compteModel.Id = compte.Id;
                 compteModel.IdClient = compte.IdClient;
                 compteModel.IdCompteAnal = compte.IdCompteAnal;
                 compteModel.CompteAnalyTique = new CompteAnalytiqueModel
                 {
                       IdCompteAnalytique = compte.CompteAnalytique.IdCompteAnalytique,
                      Numerocompte = compte.CompteAnalytique.Numerocompte
                 };
                 compteAnalyTiques.Add(compteModel);
             }
         }
       }


     #region Properties

       public List<CompteAnalClientModel> CompteAnalyTiques
       {
           get { return compteAnalyTiques; }
           set { compteAnalyTiques = value;
           this.OnPropertyChanged("CompteAnalyTiques");
           
           }
       }

       public int IdClient
       {
           get { return idClient; }
           set { idClient = value;
           this.OnPropertyChanged("IdClient");
           }
       }

       public int IdCompteAnal
       {
           get { return idCompteAnal; }
           set { idCompteAnal = value;
           this.OnPropertyChanged("IdCompteAnal");
           }
       }
       public CompteAnalytiqueModel CompteAnalyTique
       {
           get { return compteAnalyTique; }
           set { compteAnalyTique = value;
           this.OnPropertyChanged("CompteAnalyTique");
           }
       }
        #endregion

     #region methods

       public List<CompteAnalClientModel> SelectByClientId(int idClient)
       {
           List<CompteAnalClientModel> liste = new List<CompteAnalClientModel>();
           List<CompteAnalClient> comptes = dale.SelectByClientid(idClient);
           CompteAnalClientModel compteModel = null;

           if (comptes != null)
           {
               foreach (CompteAnalClient compte in comptes)
               {
                   compteModel = new CompteAnalClientModel();
                   compteModel.Id = compte.Id;
                   compteModel.IdClient = compte.IdClient;
                   compteModel.IdCompteAnal = compte.IdCompteAnal;
                   compteModel.CompteAnalyTique = new CompteAnalytiqueModel 
                   {
                       IdCompteAnalytique = compte.CompteAnalytique.IdCompteAnalytique,
                       Numerocompte = compte.CompteAnalytique.Numerocompte
                   };
                   liste.Add(compteModel);
               }
           }
           return liste;
       }



       public bool Insert(CompteAnalClientModel compte)
       {
           CompteAnalClient compteDale = new CompteAnalClient();
           compteDale.Id = compte.Id;
           compteDale.IdClient = compte.IdClient;
           compteDale.IdCompteAnal = compte.IdCompteAnal;

           if (dale.Insert(compteDale))
               return true;
           else return false;

       }
       public bool Update(CompteAnalClientModel compte)
       {
           CompteAnalClient compteDale = new CompteAnalClient();
           compteDale.Id = compte.Id;
           compteDale.IdClient = compte.IdClient;
           compteDale.IdCompteAnal = compte.IdCompteAnal;

           if (dale.Update(compteDale))
               return true;
           else return false;

       }

       public bool Delete(int compteId)
       {
           if (dale.Delete (compteId))
               return true;
           else return false;

       }
        #endregion

    }
}
