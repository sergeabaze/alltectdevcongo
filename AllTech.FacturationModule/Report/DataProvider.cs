using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.FacturationModule.Report
{
   public  class DataProvider
    {

       private static DataSetFacture _ds;

       public static DataSetFacture Ds
        {
            get {
                if (_ds == null)
                    _ds = new DataSetFacture();
                return DataProvider._ds; }
            
        }
    }
}
