using System;

using Android.App;
using Android.Content;
using Android.Content.PM;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Android.OS;
using Plugin.Permissions;
using Android.Graphics;
using System.Reflection;
using Xamarin.Forms.Platform.Android;
using Omal.Droid.CustomControls;
using Naxam.Controls.Platform.Droid;

namespace Omal.Droid
{
    [Activity(Label = "Omal", Icon = "@drawable/icon", Theme = "@style/MyTheme", MainLauncher = true, ConfigurationChanges = ConfigChanges.ScreenSize | ConfigChanges.Orientation)]
    public class MainActivity : global::Xamarin.Forms.Platform.Android.FormsAppCompatActivity
    {
        protected override void OnCreate(Bundle bundle)
        {
            // Nascondo la status bar this.Window.AddFlags(WindowManagerFlags.Fullscreen);
            if (Build.VERSION.SdkInt >= BuildVersionCodes.Lollipop)
            {
                Window.AddFlags(WindowManagerFlags.DrawsSystemBarBackgrounds);
                Window.SetStatusBarColor(Android.Graphics.Color.ParseColor("#004899"));
            }
            TabLayoutResource = Resource.Layout.Tabbar;
            ToolbarResource = Resource.Layout.Toolbar;
            SetupBottomTabs();
            //WorkaroundRegisterDefaultButtonCustomRenderer();
            base.OnCreate(bundle);

            global::Xamarin.Forms.Forms.Init(this, bundle);
            Xamarin.FormsMaps.Init(this, bundle);
            LoadApplication(new App());

        }

        void SetupBottomTabs()
        {
            BottomTabbedRenderer.BottomBarHeight = 50;
            BottomTabbedRenderer.Typeface = Typeface.CreateFromAsset(Assets, "fonts/Montserrat-Bold.ttf");
            BottomTabbedRenderer.FontSize = 12f;
            var stateList = new Android.Content.Res.ColorStateList(
                new int[][] {
                    new int[] { Android.Resource.Attribute.StateChecked
                },
                new int[] { Android.Resource.Attribute.StateEnabled
                }
                },
                new int[] {
                    Color.White, //Selected
                    Color.Gray //Normal
                });
            BottomTabbedRenderer.ItemTextColor = stateList;
            BottomTabbedRenderer.ItemIconTintList = stateList;
            BottomTabbedRenderer.ItemPadding = new Xamarin.Forms.Thickness(3);
            BottomTabbedRenderer.IconSize = 26;
            BottomTabbedRenderer.ShouldUpdateSelectedIcon = true;



            /*   var stateList = new Android.Content.Res.ColorStateList(
                   new int[][] {
                       new int[] { Android.Resource.Attribute.StateChecked
                   },
                   new int[] { Android.Resource.Attribute.StateEnabled
                   }
                   },
                   new int[] {
                       Color.Red, //Selected
                       Color.White //Normal
                   });

               BottomTabbedRenderer.BackgroundColor = new Color(0x9C, 0x27, 0xB0);
               BottomTabbedRenderer.FontSize = 12f;
               BottomTabbedRenderer.IconSize = 16;
               BottomTabbedRenderer.ItemTextColor = stateList;
               BottomTabbedRenderer.ItemIconTintList = stateList;
               BottomTabbedRenderer.Typeface = Typeface.CreateFromAsset(Assets, "fonts/DINEngschriftStd.otf");
               BottomTabbedRenderer.ItemSpacing = 4;
               //BottomTabbedRenderer.ItemPadding = new Xamarin.Forms.Thickness(6);
               BottomTabbedRenderer.BottomBarHeight = 56;
               BottomTabbedRenderer.ItemAlign = ItemAlignFlags.Center;
               BottomTabbedRenderer.ShouldUpdateSelectedIcon = true;
               BottomTabbedRenderer.MenuItemIconSetter = (menuItem, iconSource, selected) => {


               };*/
        }

        private void WorkaroundRegisterDefaultButtonCustomRenderer()
        {
            var appCompact = typeof(Xamarin.Forms.Platform.Android.FormsAppCompatActivity);

            var renderersAdded = appCompact.GetField("_renderersAdded", BindingFlags.Instance | BindingFlags.NonPublic);

            renderersAdded.SetValue(this, true);

            var registerHandlerForDefaultRenderer = appCompact.GetMethod("RegisterHandlerForDefaultRenderer", BindingFlags.Instance | BindingFlags.NonPublic);

            if (registerHandlerForDefaultRenderer != null)
            {
                registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.Button), typeof(CustomButtonCompatRenderer), typeof(ButtonRenderer) });
                //registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.Button), typeof(Xamarin.Forms.Platform.Android.AppCompat.ButtonRenderer), typeof(ButtonRenderer) });

                // register the other default renderers
                registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.NavigationPage), typeof(Xamarin.Forms.Platform.Android.AppCompat.NavigationPageRenderer), typeof(NavigationRenderer) });
                registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.TabbedPage), typeof(Xamarin.Forms.Platform.Android.AppCompat.TabbedPageRenderer), typeof(TabbedRenderer) });
                registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.MasterDetailPage), typeof(Xamarin.Forms.Platform.Android.AppCompat.MasterDetailPageRenderer), typeof(MasterDetailRenderer) });
                registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.Switch), typeof(Xamarin.Forms.Platform.Android.AppCompat.SwitchRenderer), typeof(SwitchRenderer) });
                registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.Picker), typeof(Xamarin.Forms.Platform.Android.AppCompat.PickerRenderer), typeof(PickerRenderer) });
                registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.Frame), typeof(Xamarin.Forms.Platform.Android.AppCompat.FrameRenderer), typeof(FrameRenderer) });
                registerHandlerForDefaultRenderer.Invoke(this, new[] { typeof(Xamarin.Forms.CarouselPage), typeof(Xamarin.Forms.Platform.Android.AppCompat.CarouselPageRenderer), typeof(CarouselPageRenderer) });
            }
        }

        protected override void OnActivityResult(int requestCode, Result resultCode, Intent data)
        {
            base.OnActivityResult(requestCode, resultCode, data);
        }

        public override void OnRequestPermissionsResult(int requestCode, string[] permissions, [GeneratedEnum] Android.Content.PM.Permission[] grantResults)
        {
            base.OnRequestPermissionsResult(requestCode, permissions, grantResults);
            PermissionsImplementation.Current.OnRequestPermissionsResult(requestCode, permissions, grantResults);
        }
    }
}
