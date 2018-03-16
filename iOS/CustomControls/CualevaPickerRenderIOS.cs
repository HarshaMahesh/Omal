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
                SetFont();
                UpdateBorderWidth();
                UpdateBorderColor();
                UpdateBorderRadius();
                UpdateCualevaRoundedEntryBackgroundColor();
            }

            protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
            {
                base.OnElementPropertyChanged(sender, e);
                if (this.Element == null)
                    return;
                if ((e.PropertyName == CualevaPicker.FontFamilyProperty.PropertyName) ||
                    (e.PropertyName == CualevaPicker.FontSizeProperty.PropertyName))
                {
                    SetFont();
                } else if (e.PropertyName == CualevaPicker.CualevaRoundedEntryBackgroundColorProperty.PropertyName)
                {
                    UpdateCualevaRoundedEntryBackgroundColor();
                }  if (e.PropertyName == CualevaPicker.BorderWidthProperty.PropertyName)
                {
                    UpdateBorderWidth();
                }
                else if (e.PropertyName == CualevaPicker.BorderColorProperty.PropertyName)
                {
                    UpdateBorderColor();
                }
                else if (e.PropertyName == CualevaPicker.BorderRadiusProperty.PropertyName)
                {
                    UpdateBorderRadius();
                }
            }

            private void UpdateBorderWidth()
            {
                var entryEx = this.Element as CualevaPicker;
                Control.Layer.BorderWidth = entryEx.BorderWidth;
            }

            private void UpdateBorderColor()
            {
                var entryEx = this.Element as CualevaPicker;
                Control.Layer.BorderColor = entryEx.BorderColor.ToUIColor().CGColor;
            }

            private void UpdateBorderRadius()
            {
                var entryEx = this.Element as CualevaPicker;
                Control.Layer.CornerRadius = (nfloat)entryEx.BorderRadius;
            }

            private void UpdateCualevaRoundedEntryBackgroundColor()
            {
                var entryEx = this.Element as CualevaPicker;
                Control.Layer.BackgroundColor = entryEx.CualevaRoundedEntryBackgroundColor.ToCGColor();
            }

            private void SetFont()
            {
                var view = this.Element as CualevaPicker;
                var fontSize = Font.Default.FontSize;
                if (view.FontSize != 0)
                    fontSize = view.FontSize;
                    
                if (!string.IsNullOrWhiteSpace(view.FontFamily))
                {
                    UIFont uiFont;
                    uiFont = UIFont.FromName(view.FontFamily, (nfloat)fontSize);
                    Control.Font = uiFont;

                }

            }


        }


    }

}
