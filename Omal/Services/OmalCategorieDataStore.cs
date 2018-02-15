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
    public class OmalCategorieDataStore : IDataStore<Models.Categoria>
    {

        List<Models.Categoria> items = null;
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();
        public OmalCategorieDataStore()
        {
            client = new HttpClient();
            items = new List<Models.Categoria>();
        }

        public async Task<bool> AddItemAsync(Models.Categoria item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Categoria item)
        {
            var _item = items.Where((Models.Categoria arg) => arg.idCategoria == item.idCategoria).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Categoria arg) => arg.idCategoria == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Categoria> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idCategoria == id));
        }

        public async Task<IEnumerable<Models.Categoria>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items == null || items.Count == 0)
            {
               items =  await Connection.Table<Models.Categoria>().OrderBy(x => x.ordine).ToListAsync();
            }
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=categorie", App.BackendUrl, "webservice.php");
                if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.idtoken);
                var json = await client.GetStringAsync( url);
                items = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json,new {Data = new List<Models.Categoria>()}).Data);
                foreach (var item in items)
                    Connection.InsertOrReplaceAsync(item);
            }
            return items;
        }


    }
}
