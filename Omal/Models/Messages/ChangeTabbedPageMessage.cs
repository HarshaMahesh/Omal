using System;
using Xamarin.Forms;
namespace Omal.Models.Messages
{
    public class ChangeTabbedPageMessage
    {
        public ChangeTabbedPageMessage()
        {
        }

        public Enums.EPages SetPage { get; set; }
        public ContentPage PageToNavigate { get; set; }

    }
}
