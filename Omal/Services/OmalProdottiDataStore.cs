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
    public class OmalProdottiDataStore : IDataStore<Models.Prodotto>
    {
        List<Models.Prodotto> items;
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();

        public OmalProdottiDataStore()
        {
            items = new List<Models.Prodotto>();
            client = new HttpClient();
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.Prodotto item)
        {
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.Prodotto item)
        {
            var _item = items.Where((Models.Prodotto arg) => arg.idprodotto == item.idprodotto).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);
            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Prodotto arg) => arg.idprodotto == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Prodotto> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idprodotto == id));
        }

        public async Task<IEnumerable<Models.Prodotto>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items == null || items.Count == 0)
            {
                items = await Connection.Table<Models.Prodotto>().OrderBy(x => x.ordine).ToListAsync();
            }
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=prodotti", App.BackendUrl, "webservice.php");
                if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
                var json = await client.GetStringAsync(url);
                items = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new { Data = new List<Models.Prodotto>() }).Data);
                if (forceRefresh)
                {
                    await Connection.DropTableAsync<Models.Prodotto>();
                    await Connection.CreateTableAsync<Models.Prodotto>();
                }
                foreach (var item in items)
                    Connection.InsertOrReplaceAsync(item);
            }
            return items;

        }


    }
}
