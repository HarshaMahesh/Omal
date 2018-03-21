using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Omal.Models;
using Omal.Persistence;
using Plugin.Connectivity;
using Xamarin.Forms;

namespace Omal.Services
{
    public class OmalPDFDataStore
    {
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();

        public OmalPDFDataStore()
        {

            client = new HttpClient();

        }
        public async Task<IEnumerable<Models.PDF>> GetItemsAsync(int idProdotto)
        {
            if (CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?", App.BackendUrl, "webservicepdf.php");
                url += string.Format("idprodotto={0}", idProdotto);
                url += string.Format("&lingua={0}", App.CurLang.ToLower());
                var json = await client.GetStringAsync(url);
                return await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new List<Models.PDF>()));
            }
            return new List<Models.PDF>();
        }
    }
}
