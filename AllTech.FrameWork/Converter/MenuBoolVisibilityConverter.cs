using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace AllTech.FrameWork.Converter
{
   public  class MenuBoolVisibilityConverter:IValueConverter
    {
       public MenuBoolVisibilityConverter() { }

       #region IValueConverter Members

       public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
       {
           bool bValue = (bool)value;
           if (bValue)
               return Visibility.Visible;
           else
               return Visibility.Collapsed ;
       }

       public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
       {
           Visibility visibility = (Visibility)value;

           if (visibility == Visibility.Visible)
               return true;
           else
               return false;
       }
       #endregion
    }
}
