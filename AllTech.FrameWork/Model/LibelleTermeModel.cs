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
    public class LibelleTermeModel : ViewModelBase
    {

        public int ID { get; set; }
        public Int32 Jour { get; set; }
        private string desciption;
        private string courtDescription;

      

        Facturation DAL = null;

       public LibelleTermeModel()
       {
           DAL = (Facturation)DataProviderObject.FacturationDal;
       }

        #region PROPERTIE


       public string Desciption
       {
           get { return desciption; }
           set { desciption = value;
           this.OnPropertyChanged("Desciption");
           }
       }

       public string CourtDescription
       {
           get { return courtDescription; }
           set
           {
               courtDescription = value;
               this.OnPropertyChanged("CourtDescription");
           }
       }

        #endregion

        #region METHODS

       public List<LibelleTermeModel> GetLibelle_List(int idlangue)
       {
           List<LibelleTermeModel> libelles = new List<LibelleTermeModel>();

           try
           {

               List<Libelle_Terme> devisefrom = DAL.GetAll_LIBELLE ( idlangue) ;
               if (devisefrom != null)
               {
                   foreach (var dev in devisefrom)
                   {
                       LibelleTermeModel terme = new LibelleTermeModel 
                       { 
                           ID = dev.ID, 
                           Desciption = dev.Desciption,
                           CourtDescription = dev.CourtDesc
                       };
                       libelles.Add(terme);
                   }
               }
               return libelles;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public List<LibelleTermeModel> GetLibelle_List_Archive(int idlangue)
       {
           List<LibelleTermeModel> libelles = new List<LibelleTermeModel>();

           try
           {

               List<Libelle_Terme> devisefrom = DAL.GetAll_LIBELLEArchive(idlangue);
               if (devisefrom != null)
               {
                   foreach (var dev in devisefrom)
                   {
                       LibelleTermeModel terme = new LibelleTermeModel
                       {
                           ID = dev.ID,
                           Desciption = dev.Desciption,
                           CourtDescription = dev.CourtDesc
                       };
                       libelles.Add(terme);
                   }
               }
               return libelles;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public LibelleTermeModel GetLibelle_ListById(Int32 id)
       {
           LibelleTermeModel libelles = null;

           try
           {

               Libelle_Terme devisefrom = DAL.Get_LIBELLEById (id);
               if (devisefrom != null)
               {

                   libelles = new LibelleTermeModel 
                   { 
                       ID = devisefrom.ID,
                       Desciption = devisefrom.Desciption,
                       CourtDescription = devisefrom.CourtDesc
                   };
                      
                   
               }
               return libelles;

           }
           catch (Exception de)
           {
               throw new Exception(de.Message);
           }
       }

       public bool LIBELLETERME_ADD(LibelleTermeModel libelle,int idsite)
       {
           bool valuesretturn = false;
           try
           {
               Libelle_Terme lib = new Libelle_Terme { ID = libelle.ID, Desciption = libelle.Desciption, CourtDesc = libelle .CourtDescription , Jour =libelle .Jour };

               DAL.LIBELLE_TERM_ADD(lib, idsite);

               valuesretturn = true;
           }
           catch (Exception de)
           {
               valuesretturn = false;
               throw new Exception(de.Message);
           }
           return valuesretturn;
       }


       public bool  LIBELLETERME_DELETE(int idLibelle)
       {
           bool valuesretturn = false;
           try
           {
               DAL.LIBELLE_DELETE(idLibelle);
               valuesretturn = true;
           }
           catch (Exception de)
           {
               valuesretturn = false;
               throw new Exception(de.Message);
           }
           return valuesretturn;
       }
        
        #endregion

        #region BUSNESS METHOD
       LibelleTermeModel convertfrom(Libelle_Terme libelle)
       {
           LibelleTermeModel newdevise = null;
           if (libelle != null)
               newdevise = new LibelleTermeModel { 
                   ID = libelle.ID, 
                   Desciption = libelle.Desciption,
                   CourtDescription = libelle.CourtDesc,
                   Jour = libelle.Jour  };
           return newdevise;
       }
         
        #endregion
    }
}
