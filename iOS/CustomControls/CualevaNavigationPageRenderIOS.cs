
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;
using Omal.iOS.CustomControls;

[assembly: ExportRenderer(typeof(NavigationPage), typeof(Omal.iOS.CustomControls.CualevaNavigationPageRenderIOS))]
namespace Omal.iOS.CustomControls
{
    public class CualevaNavigationPageRenderIOS : NavigationRenderer
    {
        protected override void OnElementChanged(VisualElementChangedEventArgs e)
        {
            base.OnElementChanged(e);

            if (e.NewElement != null)
            {
                var att = new UITextAttributes();
                att.Font = UIFont.FromName("Montserrat-Bold", 22);
                att.TextColor = UIColor.White;
                UINavigationBar.Appearance.TintColor = UIColor.White;
                UINavigationBar.Appearance.SetTitleTextAttributes(att);
                UINavigationBar.Appearance.BarTintColor = Color.FromHex("#60A5D1").ToUIColor();

                var att2 = new UITextAttributes();
                att2.Font = UIFont.FromName("Montserrat-Bold", 14);
                att2.TextColor = UIColor.White;
                UIBarButtonItem.Appearance.SetTitleTextAttributes(att2,
                UIControlState.Normal);
                UIBarButtonItem.Appearance.SetTitleTextAttributes(att2,
                    UIControlState.Highlighted);

            }

        }
    }
}
    

