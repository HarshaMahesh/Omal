using System;
using Xamarin.Forms;

namespace Omal.Converters
{
    public class OrderStatusConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            if (value == null || !Enum.IsDefined(typeof(Models.Enums.EOrdineStato), value)) throw new InvalidOperationException("Models.Enums.EOrdineStato");
            switch ((Models.Enums.EOrdineStato)value)
            {
                case Models.Enums.EOrdineStato.bozza: return App.Traduzioni.StrOrdineBozza;
                case Models.Enums.EOrdineStato.ordineAnnullato: return App.Traduzioni.StrOrdineStatoAnnullato;
                case Models.Enums.EOrdineStato.ordineEvaso: return App.Traduzioni.StrOrdineEvaso;
                case Models.Enums.EOrdineStato.ordineInviato: return App.Traduzioni.StrOrdineInviato;
                default:
                    break;
            }

            return ((string)value).ToUpper();
        }

        public object ConvertBack(object value, Type targetType, object parameter,
            System.Globalization.CultureInfo culture)
        {
            throw new NotSupportedException();
        }
    }
}
