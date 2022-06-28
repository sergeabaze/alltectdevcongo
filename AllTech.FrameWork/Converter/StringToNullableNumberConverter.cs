using System;
using System.Text;
using System.Windows.Data;
using System.Globalization;

namespace AllTech.FrameWork.Converter
{
    public class StringToNullableNumberConverter : IValueConverter
    {

        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;
            decimal result;
            if (decimal.TryParse(stringValue, out result))
            {
                if (result > 0)
                    return result;
                else return null;
            }
            return null ;

          
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            string stringValue = value as string;
            if (stringValue != null)
            {
                //if (targetType == typeof(int?))
                //{
                //    int result;
                //    if (int.TryParse(stringValue, out result))
                //        return result;

                //    return null;
                //}

                if (targetType == typeof(decimal?))
                {
                    decimal result;
                    if (decimal.TryParse(stringValue, out result))
                    {
                        if (result > 0)
                            return result;
                        else return null;
                    }

                    return null;
                }
            }

            return value;
        }
    }
}
