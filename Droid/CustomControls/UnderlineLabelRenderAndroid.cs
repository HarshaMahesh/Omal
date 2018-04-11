using System;
using Android.Graphics;
using Omal.CustomControls;
using Omal.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(UnderlineLabel), typeof(UnderlineRendererAndroid))]
namespace Omal.Droid.CustomControls
{
    public class UnderlineRendererAndroid : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (this.Control != null)
            {
                Control.PaintFlags = PaintFlags.UnderlineText;
            }

        }
    }
}