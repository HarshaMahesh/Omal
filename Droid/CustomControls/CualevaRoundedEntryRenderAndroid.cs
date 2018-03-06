using System;
using System.ComponentModel;
using Android.Graphics.Drawables;
using Android.Views;
using Omal.CustomControls;
using Omal.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CualevaRoundedEntry), typeof(CualevaRoundedEntryRendererAndroid))]
namespace Omal.Droid.CustomControls
{

        public class CualevaRoundedEntryRendererAndroid : Xamarin.Forms.Platform.Android.EntryRenderer
    {
        #region Private fields and properties

        private BorderRenderer _renderer;
        private const GravityFlags DefaultGravity = GravityFlags.CenterVertical;

        #endregion

        #region Parent override

        protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
        {
            base.OnElementChanged(e);
            if (e.OldElement != null || this.Element == null)
                return;
            Control.Gravity = DefaultGravity;
            var entryEx = Element as CualevaRoundedEntry;
            UpdateBackground(entryEx);
            UpdatePadding(entryEx);
            UpdateTextAlighnment(entryEx);
        }

        protected override void OnElementPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            base.OnElementPropertyChanged(sender, e);
            if (Element == null)
                return;
            var entryEx = Element as CualevaRoundedEntry;
            if (e.PropertyName == CualevaRoundedEntry.BorderWidthProperty.PropertyName ||
                e.PropertyName == CualevaRoundedEntry.BorderColorProperty.PropertyName ||
                e.PropertyName == CualevaRoundedEntry.BorderRadiusProperty.PropertyName ||
                e.PropertyName == CualevaRoundedEntry.BackgroundColorProperty.PropertyName ||
                e.PropertyName == CualevaRoundedEntry.CualevaRoundedEntryBackgroundColorProperty.PropertyName 
               )
            {
                UpdateBackground(entryEx);
            }
            else if (e.PropertyName == CualevaRoundedEntry.LeftPaddingProperty.PropertyName ||
                     e.PropertyName == CualevaRoundedEntry.RightPaddingProperty.PropertyName)
            {
                UpdatePadding(entryEx);
            }
            else if (e.PropertyName == Entry.HorizontalTextAlignmentProperty.PropertyName)
            {
                UpdateTextAlighnment(entryEx);
            }
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            if (disposing)
            {
                if (_renderer != null)
                {
                    _renderer.Dispose();
                    _renderer = null;
                }
            }
        }

        #endregion

        #region Utility methods

        private void UpdateBackground(CualevaRoundedEntry entryEx)
        {
         /*   if (_renderer != null)
            {
                _renderer.Dispose();
                _renderer = null;
            }
            _renderer = new BorderRenderer();
            Control.Background = _renderer.GetBorderBackground(entryEx.BorderColor, entryEx.BackgroundColor, entryEx.BorderWidth, entryEx.BorderRadius);
*/
            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(entryEx.BorderRadius);
            gradientDrawable.SetStroke((int)entryEx.BorderWidth, entryEx.BorderColor.ToAndroid());
            gradientDrawable.SetColor(entryEx.CualevaRoundedEntryBackgroundColor.ToAndroid());
            Control.SetBackground(gradientDrawable);


        

        }

        private void UpdatePadding(CualevaRoundedEntry entryEx)
        {
            Control.SetPadding((int)Forms.Context.ToPixels(entryEx.LeftPadding), 0,
                (int)Forms.Context.ToPixels(entryEx.RightPadding), 0);
        }

        private void UpdateTextAlighnment(CualevaRoundedEntry entryEx)
        {
            var gravity = DefaultGravity;
            switch (entryEx.HorizontalTextAlignment)
            {
                case Xamarin.Forms.TextAlignment.Start:
                    gravity |= GravityFlags.Start;
                    break;
                case Xamarin.Forms.TextAlignment.Center:
                    gravity |= GravityFlags.CenterHorizontal;
                    break;
                case Xamarin.Forms.TextAlignment.End:
                    gravity |= GravityFlags.End;
                    break;
            }
            Control.Gravity = gravity;
        }

        #endregion
        }

}
