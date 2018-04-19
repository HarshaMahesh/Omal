using System;
using Newtonsoft.Json;
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
        public static string BackendUrl = "https://app.omal.it/Admin/exe/";
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
        static Models.Utente _CurUser = null;
        public static Models.Utente CurUser
        {
            get
            {
                if (_CurUser == null)
                    if (CurToken != null)  
                        _CurUser = new Models.Utente() { Email = CurToken.email_utente, IdUtente = CurToken.IDUtente, NomeUtente = CurToken.NomeUtente };
                return _CurUser;
            }
            set
            {
                _CurUser = value;
            }
        }



        static Models.Token _CurToken;
        public static Models.Token CurToken {
            get
            {
                try
                {
                    if (_CurToken == null)
                    {
                        if (Application.Current.Properties.ContainsKey("CurTokenSerialized"))
                        {
                            if (!string.IsNullOrWhiteSpace(Application.Current.Properties["CurTokenSerialized"].ToString()))
                                _CurToken = JsonConvert.DeserializeObject<Models.Token>(Application.Current.Properties["CurTokenSerialized"].ToString());
                        }
                    }
                }
                catch (Exception)
                {
                    // eccezione silente
                }
                return _CurToken;
            }

            set
            {
                _CurToken = value;
                if (value == null) 
                    Application.Current.Properties["CurTokenSerialized"] = string.Empty;
                else
                    Application.Current.Properties["CurTokenSerialized"] = Newtonsoft.Json.JsonConvert.SerializeObject(value);
                Application.Current.SavePropertiesAsync();
            }

        }
        public static bool LangIsIT
        {
            get
            {
                return !String.IsNullOrWhiteSpace(CurLang) && string.Equals(CurLang.ToUpper(), "IT");
            }
        }

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
