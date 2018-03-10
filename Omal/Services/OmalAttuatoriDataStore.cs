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
    public class OmalAttuatoriDataStore : IDataStore<Models.Attuatore>
    {
        List<Models.Attuatore> items;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();
        HttpClient client;

        public OmalAttuatoriDataStore()
        {
            items = new List<Models.Attuatore>();
            client = new HttpClient();

        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.Attuatore item)
        {
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.Attuatore item)
        {
            var _item = items.Where((Models.Attuatore arg) => arg.idcodiceattuatore == item.idcodiceattuatore).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Attuatore arg) => arg.idcodiceattuatore == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Attuatore> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idcodiceattuatore == id));
        }

        public async Task<IEnumerable<Models.Attuatore>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items == null || items.Count == 0)
            {
                items = await Connection.Table<Models.Attuatore>().OrderBy(x => x.ordine).ToListAsync();
            }
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=attuatori", App.BackendUrl, "webservice.php");
                if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
                var json = await client.GetStringAsync(url);
                items = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new { Data = new List<Models.Attuatore>() }).Data);
                foreach (var item in items)
                    Connection.InsertOrReplaceAsync(item);
            }
            return items;
        }


    }
}
