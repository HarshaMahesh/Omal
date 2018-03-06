using System;
using Xamarin.Forms;

namespace Omal.CustomControls
{
    public class CualevaRoundedEntry: Entry
    {
        public CualevaRoundedEntry()
        {
        }

        #region Properties


        public static BindableProperty FontProperty = BindableProperty.Create<CualevaRoundedEntry, Font>(o => o.Font, Font.Default);
        public Font Font
        {
            get { return (Font)GetValue(FontProperty); }
            set { SetValue(FontProperty, value); }
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

        public static BindableProperty LeftPaddingProperty = BindableProperty.Create<CualevaRoundedEntry, int>(o => o.LeftPadding, 5);

        public int LeftPadding
        {
            get { return (int)GetValue(LeftPaddingProperty); }
            set { SetValue(LeftPaddingProperty, value); }
        }

        public static BindableProperty RightPaddingProperty = BindableProperty.Create<CualevaRoundedEntry, int>(o => o.RightPadding, 5);

        public int RightPadding
        {
            get { return (int)GetValue(RightPaddingProperty); }
            set { SetValue(RightPaddingProperty, value); }
        }

        #endregion
    }
}
