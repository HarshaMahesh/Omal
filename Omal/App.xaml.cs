using System;

using Xamarin.Forms;

namespace Omal
{
    public partial class App : Application
    {
        public static bool UseMockDataStore = true;
        public static string BackendUrl = "https://localhost:5000";
        public static string CurLang = "";
        public static Models.Utente CurUser = null;
        public static Models.Ordine CurOrdine = new Models.Ordine();

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
                MainPage = new Views.WelcomeV();
        }
    }
}
