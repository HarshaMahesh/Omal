using System;
using CoreAnimation;
using CoreGraphics;
using Omal.CustomControls;
using Omal.iOS.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CualevaGradientContentPage), typeof(CualevaGradientContentPageRenderIOS))]
namespace Omal.iOS.CustomControls
{
   
    public class CualevaGradientContentPageRenderIOS:PageRenderer
    {
        public CualevaGradientContentPageRenderIOS()
        {
        }

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null) // perform initial setup
            {
                CualevaGradientContentPage page = e.NewElement as CualevaGradientContentPage;
                var gradientLayer = new CAGradientLayer();
                gradientLayer.Frame = View.Bounds;
                gradientLayer.Colors = new CGColor[] { page.StartColor.ToCGColor(), page.EndColor.ToCGColor() };
                View.Layer.InsertSublayer(gradientLayer, 0);
            }
        }

    }
}
