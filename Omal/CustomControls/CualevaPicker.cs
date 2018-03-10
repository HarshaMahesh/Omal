using System;
using Xamarin.Forms;

namespace Omal.CustomControls
{
    public class CualevaPicker : Picker
    {
        public static readonly BindableProperty ImageProperty = BindableProperty.Create(nameof(Image), typeof(string), typeof(CualevaPicker), string.Empty);

        public string Image
        {
            get { return (string)GetValue(ImageProperty); }
            set { SetValue(ImageProperty, value); }
        }

        public static readonly BindableProperty FontFamilyProperty =
            BindableProperty.Create("FontFamily", typeof(string), typeof(CualevaPicker), string.Empty, BindingMode.OneWay);

        public string FontFamily
        {
            get
            {
                return (string)GetValue(FontFamilyProperty);
            }
            set
            {
                SetValue(FontFamilyProperty, value);
            }
        }

        public static readonly BindableProperty FontSizeProperty =
               BindableProperty.Create("FontSize",
                   typeof(float),
                                       typeof(CualevaPicker), 15f);

        public float FontSize
        {
            get
            {
                return (float)this.GetValue(FontSizeProperty);
            }
            set
            {
                this.SetValue(FontSizeProperty, value);
            }
        }



        public static BindableProperty BorderColorProperty = BindableProperty.Create<CualevaRoundedEntry, Color>(o => o.BorderColor, Color.Transparent);

        public Color BorderColor
        {
            get { return (Color)GetValue(BorderColorProperty); }
            set { SetValue(BorderColorProperty, value); }
        }

        public static BindableProperty CualevaRoundedEntryBackgroundColorProperty = BindableProperty.Create<CualevaRoundedEntry, Color>(o => o.CualevaRoundedEntryBackgroundColor, Color.Transparent);

        public Color CualevaRoundedEntryBackgroundColor
        {
            get { return (Color)GetValue(CualevaRoundedEntryBackgroundColorProperty); }
            set { SetValue(CualevaRoundedEntryBackgroundColorProperty, value); }
        }


        public static BindableProperty BorderWidthProperty = BindableProperty.Create<CualevaRoundedEntry, float>(o => o.BorderWidth, 0);

        public float BorderWidth
        {
            get { return (float)GetValue(BorderWidthProperty); }
            set { SetValue(BorderWidthProperty, value); }
        }

        public static BindableProperty BorderRadiusProperty = BindableProperty.Create<CualevaRoundedEntry, float>(o => o.BorderRadius, 0);

        public float BorderRadius
        {
            get { return (float)GetValue(BorderRadiusProperty); }
            set { SetValue(BorderRadiusProperty, value); }
        }

    }
}
