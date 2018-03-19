using System;
using Android.Graphics;
using Android.Views;
using Omal.CustomControls;
using Omal.Droid.CustomControls;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android.AppCompat;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(CualevaNavigationPageRenderAndroid))]
namespace Omal.Droid.CustomControls
{
    public class CualevaNavigationPageRenderAndroid: NavigationPageRenderer
    {
        public CualevaNavigationPageRenderAndroid()
        {
        }

        private Android.Support.V7.Widget.Toolbar toolbar;


        public override void OnViewAdded(Android.Views.View child)
        {
            base.OnViewAdded(child);

            if (child.GetType() == typeof(Android.Support.V7.Widget.Toolbar))
            {
                toolbar = (Android.Support.V7.Widget.Toolbar)child;
               
            
                toolbar.ChildViewAdded += Toolbar_ChildViewAdded;
            }
        }

        void Toolbar_ChildViewAdded(object sender, Android.Views.ViewGroup.ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();

            if (e.Child.GetType() == typeof(Android.Widget.TextView))
            {
                var textView = (Android.Widget.TextView)e.Child;
                var spaceFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "fonts/Montserrat-Regular.ttf");
                textView.Typeface = spaceFont;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
            // If your app is not using the AppCompatTextView, you don't need this check
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatTextView))
            {
                var textView = (Android.Support.V7.Widget.AppCompatTextView)e.Child;
                var spaceFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "fonts/Montserrat-Regular.ttf");
                textView.Typeface = spaceFont;
                toolbar.ChildViewAdded -= Toolbar_ChildViewAdded;
            }
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.ActionMenuView))
            {
                var aa = (Android.Support.V7.Widget.ActionMenuView)e.Child;
                aa.ChildViewAdded += Toolbar1_ChildViewAdded;

               
            }
        }

        private void Toolbar1_ChildViewAdded(object sender, ChildViewAddedEventArgs e)
        {
            var view = e.Child.GetType();
            if (e.Child.GetType() == typeof(Android.Support.V7.Widget.AppCompatImageView))
            {
                var amiv = (Android.Support.V7.Widget.AppCompatImageView)e.Child;

            }
            if (e.Child.GetType() == typeof(Android.Support.V7.View.Menu.ActionMenuItemView))
            {
                var amiv = (Android.Support.V7.View.Menu.ActionMenuItemView)e.Child;
                var spaceFont = Typeface.CreateFromAsset(Xamarin.Forms.Forms.Context.ApplicationContext.Assets, "fonts/Montserrat-Regular.ttf");
                ((Android.Support.V7.Widget.AppCompatTextView)amiv).Typeface = spaceFont;
            }

        }
    }

   

}



