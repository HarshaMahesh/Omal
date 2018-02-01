using System;
using Xamarin.Forms;
using Omal.CustomControls;
using Omal.Droid.CustomControls;
using Xamarin.Forms.Platform.Android;
using Android.Graphics.Drawables;
using Android.Graphics;
using Android.Content;

[assembly: ExportRenderer(typeof(CualevaRoundedEntry), typeof(CualevaRoundedEntryRenderAndroid))]
namespace Omal.Droid.CustomControls
{
    public class CualevaRoundedEntryRenderAndroid: EntryRenderer
    {
        
        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                var gradientDrawable = new GradientDrawable();
                gradientDrawable.SetCornerRadius(60f);
                gradientDrawable.SetStroke(5, Android.Graphics.Color.DeepPink);
                gradientDrawable.SetColor(Xamarin.Forms.Color.FromHex("#60A5D1").ToAndroid());

                Control.SetBackground(gradientDrawable);
                var typeface = Typeface.Create("Montserrat-Bold.ttf", Android.Graphics.TypefaceStyle.Normal);
                Control.Typeface = typeface;

                Control.SetPadding(50, Control.PaddingTop, Control.PaddingRight,
                    Control.PaddingBottom);
            }
        }
    }
}
