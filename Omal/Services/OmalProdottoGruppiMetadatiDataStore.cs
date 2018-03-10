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
    public class OmalProdottoGruppiMetadatiDataStore : IDataStore<Models.ProdottoGruppiMetadati>
    {
        List<Models.ProdottoGruppiMetadati> items;
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();

        public OmalProdottoGruppiMetadatiDataStore()
        {
            items = new List<Models.ProdottoGruppiMetadati>();
            client = new HttpClient();
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.ProdottoGruppiMetadati item)
        {
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.ProdottoGruppiMetadati item)
        {
            var _item = items.Where((Models.ProdottoGruppiMetadati arg) => arg.idgruppometadato == item.idgruppometadato).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.ProdottoGruppiMetadati arg) => arg.idgruppometadato == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.ProdottoGruppiMetadati> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idgruppometadato == id));
        }

        public async Task<IEnumerable<Models.ProdottoGruppiMetadati>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items == null || items.Count == 0)
            {
                items = await Connection.Table<Models.ProdottoGruppiMetadati>().OrderBy(x => x.ordine).ToListAsync();
            }
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=gruppi_metadati", App.BackendUrl, "webservice.php");
                if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
                var json = await client.GetStringAsync(url);
                var tmp = await Task.Run(() => JsonConvert.DeserializeObject<List<Models.FkProdottoGruppiMetadati>>(json));
                items = tmp.Where(x => x.idgruppometadato.HasValue).Select(x => new ProdottoGruppiMetadati
                { dataora_modifica = x.dataora_modifica, gruppo_metadati_en = x.gruppo_metadati_en, gruppo_metadati_it = x.gruppo_metadati_it, idgruppometadato = x.idgruppometadato.Value, idprodotto = x.idprodotto.Value, ordine =x.ordine }).ToList();
                if (forceRefresh)
                {
                    await Connection.DropTableAsync<Models.ProdottoGruppiMetadati>();
                    await Connection.CreateTableAsync<Models.ProdottoGruppiMetadati>();
                }
                foreach (var item in items)
                    await Connection.InsertOrReplaceAsync(item);
                return items;
            }
            return items;
        }


    }
}
