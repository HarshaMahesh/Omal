using System;
using CoreAnimation;
using CoreGraphics;
using Omal.CustomControls;
using Omal.iOS.CustomControls;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CualevaGradientContentPage), typeof(CualevaGradientContentPageRenderIOS))]
namespace Omal.iOS.CustomControls
{
   
    public class CualevaGradientContentPageRenderIOS:PageRenderer
    {
		public override void DidRotate(UIInterfaceOrientation fromInterfaceOrientation)
		{
            base.DidRotate(fromInterfaceOrientation);
            manageGradient(Element);

		}

        CualevaGradientContentPage Element;

		public CualevaGradientContentPageRenderIOS()
        {
            
        }

        CAGradientLayer gradientLayer;

        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.OldElement == null) // perform initial setup
            {
                Element = e.NewElement as CualevaGradientContentPage;
                manageGradient(Element);

            }
        }

        private void manageGradient(CualevaGradientContentPage page)
        {
            if (page == null) return;
            if (gradientLayer == null) gradientLayer = new CAGradientLayer();
            gradientLayer.Frame = View.Bounds;
            gradientLayer.Colors = new CGColor[] { page.StartColor.ToCGColor(), page.EndColor.ToCGColor() };
            View.Layer.InsertSublayer(gradientLayer, 0);
        }
    }
}
