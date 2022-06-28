using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL.Model;
using FACTURATION_DAL;

namespace AllTech.FrameWork.Model
{
    public class ProfileDateModel : ViewModelBase
    {
        public int ID { get; set; }
        public DateTime? Datedebut;
        public DateTime? Datefin;
        public int IdProfile { get; set; }
        public int IdUser { get; set; }
        Facturation DAL = null;
        public ProfileDateModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }

        #region PROPERTIES
        
        #endregion

        #region METHODS

        public ProfileDateModel  GetProfileDate(int iduser,int idprofile)
        {

            ProfileDateModel profile = null;

            try
            {
               ProfileDate   nprofil = DAL.GetProfileDate (iduser,idprofile);
                if (nprofil != null)
                {
                    profile = new ProfileDateModel 
                    { 
                        ID = nprofil.Id,
                        IdProfile = nprofil.IdProfile,
                        IdUser = nprofil.idUser,
                        Datedebut = nprofil.Datedebut,
                        Datefin = nprofil.DateFin 
                    };
                      
                    }

                return profile;

            }
            catch (Exception de)
            {
                return null;
                throw new Exception(de.Message);
            }
        }
        #endregion
    }
}
