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

    }
}
