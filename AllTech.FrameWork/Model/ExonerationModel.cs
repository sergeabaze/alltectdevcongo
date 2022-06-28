using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
   public  class ExonerationModel: ViewModelBase
   {

       public int ID { get; set; }
       private string libelle;
       private string courtDesc;

       Facturation DAL = null;

       public ExonerationModel()
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
           ID = 0;
           libelle = string.Empty;
       }

       #region PROPERTIES

       public string Libelle
       {
           get { return libelle; }
           set { libelle = value;
           this.OnPropertyChanged("Libelle");
           }
       }

       public string CourtDesc
       {
           get { return courtDesc; }
           set { courtDesc = value;
           this.OnPropertyChanged("CourtDesc");
           }
       }

       #endregion

       #region METHOD

       public List<ExonerationModel> EXONERATION_SELECT()
       {
           List<ExonerationModel> exons = new List<ExonerationModel>();

           try
           {
               List<Exoneration> devisefrom = DAL.GetAll_EXONERATION ();
               if (devisefrom != null)
               {
                   foreach (var dev in devisefrom)
                       exons.Add(Convertfrom(dev));
               }
               return exons;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }

       }


       public ExonerationModel  EXONERATION_SELECTById(int id)
       {
           ExonerationModel currentExon = null;
           try
           {

               Exoneration  exons = DAL.Get_EXONERATIONBYID(id);
               if (exons != null)
                   currentExon = Convertfrom(exons);

               return currentExon;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }

       }


       public bool EXONERATION_ADD(ExonerationModel  exoneration)
       {

           try
           {

               DAL.EXONERATION_ADD(ConvertTo(exoneration));
               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }


       public bool EXONERATION_DELETE(int id)
       {

           try
           {
               DAL.EXONERATION_DELETE(id);
               return true;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }
       #endregion

       #region BUISNESS METHOD

       ExonerationModel   Convertfrom(Exoneration  exo)
       {
           ExonerationModel newExo = null;
           if (exo != null)
               newExo = new ExonerationModel { ID = exo.ID, Libelle = exo.Libelle, CourtDesc =exo .ShortName   };
           return newExo;

       }

       Exoneration ConvertTo(ExonerationModel  exo)
       {
           Exoneration newExo = null;
           if (exo != null)
               newExo = new Exoneration { ID = exo.ID, Libelle = exo.Libelle, ShortName =exo .CourtDesc  };
           return newExo;

       }
       #endregion
   }
}
