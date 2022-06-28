using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Data;
using System.Windows;

namespace AllTech.FrameWork.Converter
{
    public class DecimalConverter : IValueConverter
    {

        public object Convert(object value, Type targetType,
                  object parameter, System.Globalization.CultureInfo culture)
        {
            if (targetType != typeof(string) || value == null ||
                parameter == null)
            {
                return DependencyProperty.UnsetValue;
            }

            //string format = string.Format("{{0:#,##}", parameter);

            return  string.Format("{{0:#,##}", value);
        }

        public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            throw new NotImplementedException();
          
        }

    }
}
