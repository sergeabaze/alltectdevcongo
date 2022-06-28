using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace AllTech.FrameWork.Utils
{
   public  class RegionControlHelper
    {
       static Dictionary<Type, List<string>> _regionControls = new Dictionary<Type, List<string>>(10);

       public static List<string> GetRegions(Type type)
       {
           if (_regionControls.ContainsKey(type))
               return _regionControls[type];
           return null;
       }
    }
}
