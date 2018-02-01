using System;
using CoreGraphics;
using Omal.CustomControls;
using Omal.iOS.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;


[assembly: ExportRenderer(typeof(CualevaRoundedEntry), typeof(CualevaRoundedEntryRenderIOS))]
namespace Omal.iOS.CustomControls
{
    public class CualevaRoundedEntryRenderIOS: EntryRenderer
    {
        public CualevaRoundedEntryRenderIOS()
        {
        }

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null)
            {
                Control.Layer.CornerRadius = 15;
    //            Control.Layer.BorderWidth = 3f;
//                Control.Layer.BorderColor = Color.White.ToCGColor();
                Control.LeftView = new UIKit.UIView(new CGRect(0, 0, 10, 0));
                Control.LeftViewMode = UIKit.UITextFieldViewMode.Always;
            }
        }
    }
}
