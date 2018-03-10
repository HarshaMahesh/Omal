using System;
using System.Collections.Generic;
using Android.Graphics;
using Android.Graphics.Drawables;
using Android.Support.V4.Content;
using Omal.CustomControls;
using Omal.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(CualevaPicker), typeof(CualevaPickerRenderAndroid))]

namespace Omal.Droid.CustomControls
{
    public class CualevaPickerRenderAndroid : PickerRenderer
    {
        CualevaPicker element;
     

        protected override void OnElementChanged(ElementChangedEventArgs<Picker> e)
        {
            base.OnElementChanged(e);

            element = (CualevaPicker)this.Element;

            if (Control != null && this.Element != null && !string.IsNullOrEmpty(element.Image))
                Control.Background = AddPickerStyles(element.Image);
            UpdateBackground(element);
        }

        public LayerDrawable AddPickerStyles(string imagePath)
        {
            element = (CualevaPicker)this.Element;
            ShapeDrawable border = new ShapeDrawable();

            border.Paint.Color = Android.Graphics.Color.Gray;
            border.SetPadding(10, 10, 10, 10);
            border.Paint.SetStyle(Paint.Style.Stroke);
          
       

            Drawable[] layers = { border, GetDrawable(imagePath,element.BorderRadius) };
            LayerDrawable layerDrawable = new LayerDrawable(layers);

            layerDrawable.SetLayerInset(0, 0, 0, 0, 0);

            return layerDrawable;
        }

       


        /*nuovo*/
        private void UpdateBackground(CualevaPicker entryEx)
        {

            var gradientDrawable = new GradientDrawable();
            gradientDrawable.SetCornerRadius(entryEx.BorderRadius);
            gradientDrawable.SetStroke((int)entryEx.BorderWidth, entryEx.BorderColor.ToAndroid());
            gradientDrawable.SetColor(entryEx.CualevaRoundedEntryBackgroundColor.ToAndroid());

            if (!string.IsNullOrWhiteSpace(entryEx.Image))
            {
                List<Drawable> layers = new List<Drawable>();
                layers.Add(gradientDrawable);
                var ly = GetDrawable(entryEx.Image,entryEx.BorderRadius);
                if (ly != null)
                    layers.Add(ly);
                LayerDrawable layerDrawable = new LayerDrawable(layers.ToArray());
                layerDrawable.SetLayerInset(0, 0, 0, 0, 0);
                Control.SetBackground(layerDrawable);
            }
            else
                Control.SetBackground(gradientDrawable);
        }



        private BitmapDrawable GetDrawable(string imagePath, double borderRadius)
        {

            BitmapDrawable result = null;
            int resID = Resources.GetIdentifier(imagePath, "drawable", this.Context.PackageName);
            try
            {
                var drawable = ContextCompat.GetDrawable(this.Context, resID);
                var bitmap = ((BitmapDrawable)drawable).Bitmap;

                result = new BitmapDrawable(Resources, padBitmap(Bitmap.CreateScaledBitmap(bitmap, 70, 70, true), borderRadius));
                result.Gravity = Android.Views.GravityFlags.Right;
                return result;
            }
            catch (Exception ex)
            {
                // eccezione silente
            }
            return result;
        }

        public Bitmap padBitmap(Bitmap bitmap, double borderRadius)
        {
            int paddingX = Convert.ToInt32(borderRadius);
            int paddingY = 0;


            Bitmap paddedBitmap = Bitmap.CreateBitmap(
                bitmap.Width + paddingX,
                bitmap.Height + paddingY, Bitmap.Config.Argb8888);

            Canvas canvas = new Canvas(paddedBitmap);
            canvas.DrawColor(Android.Graphics.Color.Transparent);
            canvas.DrawBitmap(
                bitmap,
                paddingX / 2,
                paddingY / 2, new Paint(PaintFlags.FilterBitmap));

            return paddedBitmap;
        }
    }
}