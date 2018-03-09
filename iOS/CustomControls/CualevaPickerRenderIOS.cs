using System;
using System.Drawing;
using CoreGraphics;
using Omal.CustomControls;
using Omal.iOS.CustomControls.PickerWithIcon.iOS;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(CualevaPicker), typeof(CualevaPickerRenderIOS))]
namespace Omal.iOS.CustomControls
{

namespace PickerWithIcon.iOS
    {
        public class CualevaPickerRenderIOS: PickerRenderer
        {

            protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
            {
                base.OnElementChanged(e);

                var element = (CualevaPicker)this.Element;

                if (this.Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                {
                    var locImage = UIImage.FromBundle(element.Image);
                    var downarrow = new UIImageView(locImage)
                    {
                        // Indent it 10 pixels from the left.
                        Frame = new RectangleF(0, 0, 40, 40)
                    };
                    Control.RightViewMode = UITextFieldViewMode.Always;
                    var imageView = new UIView(new CGRect(0, 0, 40, 40));
                    imageView.AddSubview(downarrow);
                    Control.RightView = imageView;
                }
            }
        }


    }

}
