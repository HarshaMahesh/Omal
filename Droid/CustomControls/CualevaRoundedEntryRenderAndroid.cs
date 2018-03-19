using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Content;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Android.Util;
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
        CualevaRoundedEntry ELEMENT;
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
            ELEMENT = Element as CualevaRoundedEntry;
            UpdateBackground(entryEx);
            UpdatePadding(entryEx);
            UpdateTextAlighnment(entryEx);
            var editText = this.Control;
            if (!string.IsNullOrEmpty(entryEx.Image))
            {
                editText.SetCompoundDrawablesWithIntrinsicBounds(GetDrawable(entryEx.Image), null, null, null);
            }
            editText.CompoundDrawablePadding = 25;
          
        }

        private BitmapDrawable GetDrawable(string imageEntryImage)
        {
            try
            {
                int resID = Resources.GetIdentifier(imageEntryImage, "drawable", this.Context.PackageName);
                var drawable = ContextCompat.GetDrawable(this.Context, resID);
                var bitmap = ((BitmapDrawable)drawable).Bitmap;

                return new BitmapDrawable(Resources, Bitmap.CreateScaledBitmap(bitmap, ELEMENT.ImageHeight * 2, ELEMENT.ImageWidth * 2, true));
            }
            catch (Exception ex)
            {
                return null;
            }

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
                e.PropertyName == CualevaRoundedEntry.CualevaRoundedEntryBackgroundColorProperty.PropertyName ||
                e.PropertyName == CualevaRoundedEntry.ImageProperty.PropertyName
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
    
            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(entryEx.BorderRadius);
            gradientDrawable.SetStroke((int)entryEx.BorderWidth, entryEx.BorderColor.ToAndroid());
            gradientDrawable.SetColor(entryEx.CualevaRoundedEntryBackgroundColor.ToAndroid());

            if (!string.IsNullOrWhiteSpace(entryEx.Image))
            {
                List<Drawable> layers = new List<Drawable>();
                layers.Add(gradientDrawable);

                LayerDrawable layerDrawable = new LayerDrawable(layers.ToArray());
                layerDrawable.SetLayerInset(0, 0, 0, 0, 0);
                Control.SetBackground(layerDrawable);
            } else
                Control.SetBackground(gradientDrawable);
        }

       

       

       

        private void UpdatePadding(CualevaRoundedEntry entryEx)
        {
            int paddingToAdd = 0;
           
            Control.SetPadding((int)Forms.Context.ToPixels(entryEx.LeftPadding + paddingToAdd), 0,
                (int)Forms.Context.ToPixels(entryEx.RightPadding), 0);
        }

        public static float pxFromDp(Context context, float dp)
        {
            return dp * context.Resources.DisplayMetrics.Density;
        }

        public static float dpFromPx(Context context, float px)
        {
            return px / context.Resources.DisplayMetrics.Density;
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
