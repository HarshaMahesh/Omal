using System;
using Omal.Persistence;
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
        public static AppResources.Traduzioni Traduzioni = new AppResources.Traduzioni(); 
        public static string BackendUrl = "http://demo.timmagine.com/omal/http/Admin/exe/";
        public static string CurLang
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("APPLANG"))
                    return Application.Current.Properties["APPLANG"].ToString();
                return string.Empty;
            }
            set
            {
                Application.Current.Properties["APPLANG"] = value;
            }
        
        }
        public static Models.Utente CurUser = null;
        public static Models.Token CurToken = null;
        public static Models.Ordine CurOrdine = new Models.Ordine();
        public static DateTime? LastUpdate 
        {
            get
            {
                if (Application.Current.Properties.ContainsKey("LASTDBUPDATE"))
                {
                    if (!string.IsNullOrWhiteSpace(Application.Current.Properties["LASTDBUPDATE"].ToString()))
                        return new DateTime(
                            Convert.ToInt32(Application.Current.Properties["LASTDBUPDATE"].ToString().Substring(0, 4)),
                            Convert.ToInt32(Application.Current.Properties["LASTDBUPDATE"].ToString().Substring(4, 2)),
                            Convert.ToInt32(Application.Current.Properties["LASTDBUPDATE"].ToString().Substring(6, 2)));
                    else
                        return null;
                }
                return null;
            }

            set
            {
                if (value.HasValue)
                    Application.Current.Properties["LASTDBUPDATE"] = value.Value.ToString("yyyyMMdd");
                else
                    Application.Current.Properties["LASTDBUPDATE"] = string.Empty;
                Application.Current.SavePropertiesAsync();
            }

        }






        public App()
        {
            InitializeComponent();
            ConnectAndCreateSqlDb();
            if (UseMockDataStore)
                DependencyService.Register<Services.MockOmalDataStore>();
            else
                DependencyService.Register<Services.OmalDataStore>();
            MainPage = new Views.WelcomeV();
        }

        private void ConnectAndCreateSqlDb()
        {

            var _connection = DependencyService.Get<ISQLiteDb>().GetConnection();
            _connection.CreateTableAsync<Models.Attuatore>();
            _connection.CreateTableAsync<Models.Categoria>();
            _connection.CreateTableAsync<Models.Prodotto>();
            _connection.CreateTableAsync<Models.Valvola>();
            _connection.CreateTableAsync<Models.Cliente>();
            _connection.CreateTableAsync<Models.ProdottoMetadati>();
            _connection.CreateTableAsync<Models.ProdottoGruppiMetadati>();
            _connection.CreateTableAsync<Models.Ordine>();
        }
    }
}
