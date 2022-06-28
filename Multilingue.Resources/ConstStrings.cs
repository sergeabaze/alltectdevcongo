using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Multilingue.Resources
{
   public  class ConstStrings
    {
        public static string Get(string name)
        {
            return GetDefault(name);
        }
        static string GetDefault(string name)
        {
            PropertyInfo pi = typeof(Multilingue.Resources.Langues).GetProperty(name);
            return string.Format("{0}", pi.GetValue(null, new object[] { }));
        }
    }
}
