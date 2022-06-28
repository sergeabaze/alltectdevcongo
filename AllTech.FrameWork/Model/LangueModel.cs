using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;
using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Model
{
    public class LangueModel : ViewModelBase
    {
       public int Id { get; set; }
       public string  Libelle { get; set; }
       public string Shortname { get; set; }

       Facturation DAL = null;

       public LangueModel()
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
       }


       public ObservableCollection<LangueModel> LANGUE_SELECT(int id)
       {
           ObservableCollection<LangueModel> languesModel = new ObservableCollection<LangueModel>();
           try
           {
               List<Langue> langues = DAL.GetALLLangue ( );
               if (langues != null)
               {
                   foreach (var ll in langues)
                   {
                       languesModel.Add(new LangueModel { Id =ll .IdLangue , Libelle =ll .Libelle, Shortname =ll .Shorname   });
                   }
                      
               }

               return languesModel;

           }
           catch (Exception de)
           {
               return null;
               throw new Exception(de.Message);
           }

       }

       public LangueModel LANGUE_SELECTBYID(int id)
       {
           LangueModel newLangue = null;
           try
           {
               Langue langues = DAL.GetLANGUEBY_ID(id );
               if (langues != null)
               {
                   newLangue = new LangueModel { Id = langues.IdLangue, Libelle = langues.Libelle, Shortname = langues.Shorname  };

               }

               return newLangue;

           }
           catch (Exception de)
           {
               return null;
               throw new Exception(de.Message);
           }

       }

       public bool  LANGUE_ADD(LangueModel langue)
       {
           try
           {
               Langue l = new Langue { IdLangue = langue.Id, Libelle = langue.Libelle, Shorname = langue.Shortname  };
               DAL.language_ADD (l);
               return true ;

           }
           catch (Exception de)
           {
              
               throw new Exception(de.Message);
           }

       }

       public bool LANGUE_DELETE(int id)
       {
           try
           {
               
               DAL.language_DELETE (id) ;
               return true;

           }
           catch (Exception de)
           {

               throw new Exception(de.Message);
           }

       }

       LangueModel convertFrom(Langue langue)
       {
           LangueModel l = null;
           if (l != null)
           {
               l = new LangueModel {  Id  = langue.IdLangue, Libelle = langue.Libelle,  Shortname  = langue.Shorname  };
           }
           return l;
       }
    }

    
}
