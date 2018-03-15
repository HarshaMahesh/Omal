using System;
using Xamarin.Forms;

namespace Omal.CustomControls
{
    public class CualevaNavigationPage: NavigationPage
    {
        public FileImageSource IconWhenIsDeactivated { get; set; }
        public FileImageSource IconWhenIsActive { get; set; }

        public void SetIconActive()
        {
            if (IconWhenIsActive != null) Icon = IconWhenIsActive;
        }

        public void SetIconNoActive()
        {
            if (IconWhenIsDeactivated != null) Icon = IconWhenIsDeactivated;
        }

        public CualevaNavigationPage()
        {
        }

        public CualevaNavigationPage(Page root):base(root)
        {
        }

    }
}
