using System;
using System.Collections.Generic;
using System.Globalization;

namespace Omal.Common
{
    public class DataSourceConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value == null) return false;
            //return ((IList<object>)value).Count == 0;
            return true;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            return value;
        }
    }
}
