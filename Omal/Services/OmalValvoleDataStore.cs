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
    public class OmalValvoleDataStore : IDataStore<Models.Valvola>
    {
        List<Models.Valvola> items;
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();

        public OmalValvoleDataStore()
        {
            items = new List<Models.Valvola>();
            client = new HttpClient();
        }

        public async Task<bool> AddItemAsync(Models.Valvola item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Valvola item)
        {
            var _item = items.Where((Models.Valvola arg) => arg.idcodicevalvola == item.idcodicevalvola).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Valvola arg) => arg.idcodicevalvola == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Valvola> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idcodicevalvola == id));
        }

        public async Task<IEnumerable<Models.Valvola>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items == null || items.Count == 0)
            {
                items = await Connection.Table<Models.Valvola>().OrderBy(x => x.ordine).ToListAsync();
            }
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=valvole", App.BackendUrl, "webservice.php");
                if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
                var json = await client.GetStringAsync(url);
                items = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new { Data = new List<Models.Valvola>() }).Data);
                foreach (var item in items)
                    Connection.InsertOrReplaceAsync(item);
                return items;
            }
            return items;
        }


    }
}
