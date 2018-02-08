using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

#if !DEBUG
[assembly: XamlCompilation(XamlCompilationOptions.Compile)]
#endif
namespace Omal
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = false;
        public static string BackendUrl = "http://demo.timmagine.com/omal/http/Admin/exe/";
        public static string CurLang = "";
        public static Models.Utente CurUser = null;
        public static Models.Ordine CurOrdine = new Models.Ordine();
        public static string CurToken = "";

        public App()
        {
            InitializeComponent();

            if (UseMockDataStore)
                DependencyService.Register<Services.MockOmalDataStore>();
            else
                DependencyService.Register<Services.OmalDataStore>();
            if (Device.RuntimePlatform == Device.iOS)
                MainPage = new Views.WelcomeV();
            else
            {
                MainPage = new Views.WelcomeV();
            }
        }
    }
}
