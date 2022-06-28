using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech_Facturation.Ds
{
  public   class Dataprovider
    {

        private static FactureDataset _ds;

        public static FactureDataset Ds
        {
            get {
                if (_ds == null)
                    _ds = new FactureDataset();
                return Dataprovider._ds; }
           
        }
    }
}
