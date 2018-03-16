using System;
using Xamarin.Forms;

namespace Omal.Converters
{
    public class GruppoMetadatiConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
        {
            if (value == null) return string.Empty;
            if (App.CurLang == "IT")
                return ((Models.ProdottoGruppiMetadati)value).gruppo_metadati_it.ToUpper();
            else
                return ((Models.ProdottoGruppiMetadati)value).gruppo_metadati_en.ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
