using System;
using Foundation;
using Omal.CustomControls;
using Omal.iOS.CustomControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(UnderlineLabel), typeof(UnderlineRendererIOS))]
namespace Omal.iOS.CustomControls
{
    public class UnderlineRendererIOS : LabelRenderer
    {
        protected override void OnElementChanged(ElementChangedEventArgs<Label> e)
        {
            base.OnElementChanged(e);
            if (this.Control != null)
            {
                if (e.NewElement != null)
                {
                    var label = (UnderlineLabel)this.Element;
                    this.Control.AttributedText = new NSAttributedString(label.Text, underlineStyle: NSUnderlineStyle.Single);
                }
            }

        }
    }
}
