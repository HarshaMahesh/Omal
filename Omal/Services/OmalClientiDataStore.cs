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
    public class OmalClientiDataStore : IDataStore<Models.Cliente>
    {
        List<Models.Cliente> items;
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();

        public OmalClientiDataStore()
        {
            items = new List<Models.Cliente>();
            client = new HttpClient();
        }

        public async Task<bool> AddItemAsync(Models.Cliente item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Cliente item)
        {
            var _item = items.Where((Models.Cliente arg) => arg.IDCliente == item.IDCliente).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Cliente arg) => arg.IDCliente == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Cliente> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IDCliente == id));
        }

        public async Task<IEnumerable<Models.Cliente>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items == null || items.Count == 0)
            {
                items = await Connection.Table<Models.Cliente>().ToListAsync();
            }
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=clienti", App.BackendUrl, "webservice.php");
                if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
                var json = await client.GetStringAsync(url);
                items = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new { data = new List<Models.Cliente>() }).data);
                foreach (var item in items)
                    Connection.InsertOrReplaceAsync(item);
            }
            return items;
        }


    }
}
