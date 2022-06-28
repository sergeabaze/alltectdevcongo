using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class SettingsModel : ViewModelBase
    {
        private string code;

      
        private string libelle;

      
        public int IdSite { get; set; }

      IFacturation DAL = null;
      public SettingsModel()
      {
          DAL = (Facturation)DataProviderObject.FacturationDal;
      }

        #region PROPERTIES
      public string Libelle
      {
          get { return libelle; }
          set { libelle = value;
          this.OnPropertyChanged("Libelle");
          }
      }
      public string Code
      {
          get { return code; }
          set { code = value;
          this.OnPropertyChanged("Code");
          }
      }

        #endregion

        #region METHOD


      public List<SettingsModel> Configuration_List(Int32 idSite)
      {
          try
          {
              List<SettingsModel> listes = new List<SettingsModel>();
              List<Settings> listefrom = DAL.CONFIGURATION_SELECT(string.Empty, idSite);
              SettingsModel setting = null;
              foreach (Settings set in listefrom)
              {
                  setting = new SettingsModel { Code = set.Code, Libelle = set.Libelle, IdSite = set.IdSite };
                  listes.Add(setting);
              }
              return listes;

          }
          catch (Exception ex)
          {
              throw new DALException(ex.Message);
          }
            

      }


      public SettingsModel Configuration_ByCode(string code, Int32 idSite)
      {
          try
          {

              Settings listefrom = DAL.CONFIGURATION_SELECTBYCODE(code, idSite);
              SettingsModel setting = null;

              if (listefrom != null)
              {
                  setting = new SettingsModel { Code = listefrom.Code, Libelle = listefrom.Libelle, IdSite = listefrom.IdSite };
              }

              return setting;

          }
          catch (Exception ex)
          {
              throw new DALException(ex.Message);
          }


      }

      public bool Configuration_Add(SettingsModel set)
      {
          try
          {
              Settings newset = new Settings { Code = set.Code, Libelle = set.Libelle, IdSite = set.IdSite };
              DAL.CONFIGURATION_ADD(newset);

              return true;
          }
          catch (Exception ex)
          {
              throw new DALException(ex.Message);
          }
      }

      public bool Configuration_Delete(string code,int idSite)
      {
          try
          {

              DAL.CONFIGURATION_DELETE(code, idSite);

              return true;
          }
          catch (Exception ex)
          {
              throw new DALException(ex.Message);
          }
      }
        
        
        #endregion

        #region OTHERS

        #endregion
    }
}
