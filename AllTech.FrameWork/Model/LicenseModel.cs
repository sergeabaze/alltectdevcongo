using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AllTech.FrameWork.PropertyChange;
using FACTURATION_DAL;
using FACTURATION_DAL.Model;

namespace AllTech.FrameWork.Model
{
    public class LicenseModel : ViewModelBase
    {

        public string mode { get; set; }
        public string Valeur { get; set; }
        public DateTime?  dateDebut { get; set; }

        Facturation DAL = null;

        public LicenseModel()
        {
            DAL = (Facturation)DataProviderObject.FacturationDal;
        }



        public bool PARAMS_ADD(LicenseModel license)
        {
            try
            {

                DAL.PARAMETRES_ADD(license.mode, license.Valeur, license.dateDebut );
                return true;

            }
            catch (Exception de)
            {

                throw new Exception(de.Message);
            }

        }

        public LicenseModel PARAMS_SELECT_BY_MOD(string mode)
        {
            try
            {

               object[] val= DAL.PARAMETRES_SELECTBYMODE(mode);
               LicenseModel license = new LicenseModel();
               license.mode = val[0].ToString ();
               license.Valeur = val[1].ToString();
               license.dateDebut =DateTime .Parse ( val[2].ToString());
               return license;

            }
            catch (Exception de)
            {

                throw new Exception(de.Message);
            }

        }

    }
}
