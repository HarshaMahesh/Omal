using System;
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
                    var downarrow = UIImage.FromBundle(element.Image);
                    Control.RightViewMode = UITextFieldViewMode.Always;
                    var imageView =new UIView(new CGRect(0, 0, downarrow.CGImage.Width+5, downarrow.CGImage.Height));
                    imageView.AddSubview(new UIImageView(downarrow));
                    Control.RightView = imageView;
                }
            }
        }
    }

}
