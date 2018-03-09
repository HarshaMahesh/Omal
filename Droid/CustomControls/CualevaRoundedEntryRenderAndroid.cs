using System;
using System.Collections.Generic;
using System.ComponentModel;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
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
                var ly = GetDrawable(entryEx.Image, entryEx.BorderRadius);
                if (ly != null)
                    layers.Add(ly);
                LayerDrawable layerDrawable = new LayerDrawable(layers.ToArray());
                layerDrawable.SetLayerInset(0, 0, 0, 0, 0);
                Control.SetBackground(layerDrawable);
            } else
                Control.SetBackground(gradientDrawable);
        }

       

        private BitmapDrawable GetDrawable(string imagePath, double borderRadius)
        {
            BitmapDrawable result=null;
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            try
            {
                var drawable = ContextCompat.GetDrawable(this.Context, resID);
                var bitmap = ((BitmapDrawable)drawable).Bitmap;

                result = new BitmapDrawable(Resources, padBitmap(Bitmap.CreateScaledBitmap(bitmap, 70, 70, true), borderRadius));
                result.Gravity = Android.Views.GravityFlags.Left;
                return result;
            }
            catch (Exception ex)
            {
                // eccezione silente
            }
            return result;
        }

        public  Bitmap padBitmap(Bitmap bitmap, double borderRadius)
        {
            int paddingX = Convert.ToInt32(borderRadius);
            int paddingY=0;


            Bitmap paddedBitmap = Bitmap.CreateBitmap(
                bitmap.Width + paddingX,
                bitmap.Height + paddingY,Bitmap.Config.Argb8888);

            Canvas canvas = new Canvas(paddedBitmap);
            canvas.DrawColor(Android.Graphics.Color.Transparent);
            canvas.DrawBitmap(
                bitmap,
                paddingX / 2,
                paddingY / 2, new Paint(PaintFlags.FilterBitmap));

            return paddedBitmap;
        }

        private void UpdatePadding(CualevaRoundedEntry entryEx)
        {
            int paddingToAdd = 0;
            if (!string.IsNullOrWhiteSpace(entryEx.Image))
            {
                var image = GetDrawable(entryEx.Image, entryEx.BorderRadius);
                if (image != null)
                    paddingToAdd = 20;
            }
            Control.SetPadding((int)Forms.Context.ToPixels(entryEx.LeftPadding + paddingToAdd), 0,
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
