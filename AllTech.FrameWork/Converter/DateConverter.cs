using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace AllTech.FrameWork.Converter
{
   public  class DateConverter:IValueConverter
    {

        public object Convert(object value, Type targetType,
                   object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string) || value == null ||
                parameter == null)
            {
                return DependencyProperty.UnsetValue;
            }

            string format = string.Format("{{0:{0}}}", parameter);

            return string.Format(culture, format, value);
        }



        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            //throw new NotImplementedException();
            string strValues = value.ToString();
            DateTime resultDateTime;
            if (DateTime.TryParse(strValues, out resultDateTime))
            {
                return resultDateTime;
            }

            // object values = value;
            return value;
        }
    }
}
