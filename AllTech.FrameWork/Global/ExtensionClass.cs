using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Collections.ObjectModel;

namespace AllTech.FrameWork.Global
{
   public static  class ExtensionClass
    {

        public static bool IsIsAlphanumeric(this string source)
        {
            Regex pattern = new Regex("[^0-9a-zA-Z]");
            return !pattern.IsMatch(source);
        }

        public static bool Isnumeric(this string source)
        {
            Regex pattern = new Regex("[^0-9]");
            return !pattern.IsMatch(source);
        }


        public static void Sort<TSource, TKey>(this ObservableCollection<TSource> source, Func<TSource, TKey> keySelector)
        {
            if (source == null) return;

            Comparer<TKey> comparer = Comparer<TKey>.Default;

            for (int i = source.Count - 1; i >= 0; i--)
            {
                for (int j = 1; j <= i; j++)
                {
                    TSource o1 = source[j - 1];
                    TSource o2 = source[j];
                    if (comparer.Compare(keySelector(o1), keySelector(o2)) > 0)
                    {
                        source.Remove(o1);
                        source.Insert(j, o1);
                    }
                }
            }
        }
    }
}
