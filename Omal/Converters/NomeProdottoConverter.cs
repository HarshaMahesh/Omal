using System;
using Xamarin.Forms;

namespace Omal.Converters
{
    public class NomeProdottoConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (App.CurLang == "IT")
                return ((Models.Prodotto)value).nome;
            else
                return ((Models.Prodotto)value).nome_en;
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
