using System;
using System.Drawing;
using CoreGraphics;
using Omal.CustomControls;
using Omal.iOS.CustomControls;
using UIKit;
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

        #region Parent override

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || Element == null)
                return;
            Control.BorderStyle = UITextBorderStyle.None;
            UpdateBorderWidth();
            UpdateBorderColor();
            UpdateBorderRadius();
            UpdateLeftPadding();
            UpdateRightPadding();
            UpdateImage();
            SetFont();
            Control.ClipsToBounds = true;
            ResizeHeight();
            UpdateCualevaRoundedEntryBackgroundColor();
        }

        protected override void OnElementPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (this.Element == null)
                return;
            if (e.PropertyName == CualevaRoundedEntry.BorderWidthProperty.PropertyName)
            {
                UpdateBorderWidth();
            }
            else if (e.PropertyName == CualevaRoundedEntry.BorderColorProperty.PropertyName)
            {
                UpdateBorderColor();
            }
            else if (e.PropertyName == CualevaRoundedEntry.BorderRadiusProperty.PropertyName)
            {
                UpdateBorderRadius();
            }
            else if (e.PropertyName == CualevaRoundedEntry.LeftPaddingProperty.PropertyName)
            {
                UpdateLeftPadding();
            }
            else if (e.PropertyName == CualevaRoundedEntry.RightPaddingProperty.PropertyName)
            {
                UpdateRightPadding();
            }
            else if (e.PropertyName == CualevaRoundedEntry.FontProperty.PropertyName)
            {
                SetFont();
            }
            else if (e.PropertyName == CualevaRoundedEntry.CualevaRoundedEntryBackgroundColorProperty.PropertyName)
            {
                UpdateCualevaRoundedEntryBackgroundColor();
            }
            else if (e.PropertyName == CualevaRoundedEntry.ImageProperty.PropertyName)
                UpdateImage();
                
                
        }

        #endregion

        #region Utility methods

        private void ResizeHeight()
        {
            if (Element.HeightRequest >= 0) return;

            var height = Math.Max(Bounds.Height,
                new UITextField
                {
                    Font = Control.Font
                }.IntrinsicContentSize.Height);

            Control.Frame = new RectangleF(0.0f,
                                           0.0f,
                                           (float)Element.Width,
                                           (float)height);

            Element.HeightRequest = height;
        }

        private void SetFont()
        {
            var view = this.Element as CualevaRoundedEntry;
            UIFont uiFont;
            if (view.Font != Font.Default &&
                (uiFont = view.Font.ToUIFont()) != null)
                Control.Font = uiFont;
            else if (view.Font == Font.Default)
                Control.Font = UIFont.SystemFontOfSize(17f);
        }

        private void UpdateCualevaRoundedEntryBackgroundColor()
        {
            var entryEx = this.Element as CualevaRoundedEntry;
            Control.Layer.BackgroundColor = entryEx.CualevaRoundedEntryBackgroundColor.ToCGColor();
        }

        private void UpdateImage()
        {
            var entryEx = this.Element as CualevaRoundedEntry;
            if (string.IsNullOrWhiteSpace(entryEx.Image)) return;
            var locImage = UIImage.FromBundle(entryEx.Image);
            var downarrow = new UIImageView(locImage)
            {
                // Indent it 10 pixels from the left.
                Frame = new RectangleF((entryEx.BorderRadius  /2)-10, 0, 40, 40 )
            };
            Control.LeftViewMode = UITextFieldViewMode.Always;
            var imageView = new UIView(new CGRect(0, 0, 40, 40));
            imageView.AddSubview(downarrow);
            Control.LeftView = imageView;

        }


        private void UpdateBorderWidth()
        {
            var entryEx = this.Element as CualevaRoundedEntry;
            Control.Layer.BorderWidth = entryEx.BorderWidth;
        }

        private void UpdateBorderColor()
        {
            var entryEx = this.Element as CualevaRoundedEntry;
            Control.Layer.BorderColor = entryEx.BorderColor.ToUIColor().CGColor;
        }

        private void UpdateBorderRadius()
        {
            var entryEx = this.Element as CualevaRoundedEntry;
            Control.Layer.CornerRadius = (nfloat)entryEx.BorderRadius;
        }

        private void UpdateLeftPadding()
        {
            var entryEx = this.Element as CualevaRoundedEntry;
            var daSommare = 0;
            if (entryEx.BorderRadius > 0)
            {
                daSommare =(int) entryEx.BorderRadius;
            }
            if (!string.IsNullOrWhiteSpace(entryEx.Image))
            {
                var locImage = UIImage.FromBundle(entryEx.Image);
                daSommare += (int)locImage.CGImage.Width;

            }
            var leftPaddingView = new UIView(new CGRect(0, 0, entryEx.LeftPadding + daSommare, 0));
            Control.LeftView = leftPaddingView;
            Control.LeftViewMode = UITextFieldViewMode.Always;
        }

        private void UpdateRightPadding()
        {
            var entryEx = this.Element as CualevaRoundedEntry;
            var rightPaddingView = new UIView(new CGRect(0, 0, entryEx.RightPadding, 0));
            Control.RightView = rightPaddingView;
            Control.RightViewMode = UITextFieldViewMode.Always;
        }

        #endregion
    }
}
