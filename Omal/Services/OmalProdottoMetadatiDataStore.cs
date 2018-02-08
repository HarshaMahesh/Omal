using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Omal.Models;
using Plugin.Connectivity;

namespace Omal.Services
{
    public class OmalProdottoMetadatiDataStore : IDataStore<Models.ProdottoMetadati>
    {
        List<Models.ProdottoMetadati> items;
        HttpClient client = new HttpClient();
        public OmalProdottoMetadatiDataStore()
        {
            items = new List<Models.ProdottoMetadati>();
            client = new HttpClient();
        }

        public async Task<bool> AddItemAsync(Models.ProdottoMetadati item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.ProdottoMetadati item)
        {
            var _item = items.Where((Models.ProdottoMetadati arg) => arg.idmetadato == item.idmetadato).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.ProdottoMetadati arg) => arg.idmetadato == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.ProdottoMetadati> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idmetadato == id));
        }

        public async Task<IEnumerable<Models.ProdottoMetadati>> GetItemsAsync(bool forceRefresh = false)
        {
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=metadati", App.BackendUrl, "webservice.php");
                if (!string.IsNullOrWhiteSpace(App.CurToken)) url += string.Format("&token={0}", App.CurToken);
                var json = await client.GetStringAsync(url);
                var tmpiItems =  await Task.Run(() => JsonConvert.DeserializeObject<List<Models.FkProdottoMetadati>>(json));
                items = tmpiItems.Where(x => x.idgruppometadato.HasValue && x.idmetadato.HasValue).Select(x => new ProdottoMetadati
                { 
                    dataora_modifica = x.dataora_modifica,
                    idgruppometadato = x.idgruppometadato.Value,
                    idmetadato = x.idmetadato.Value,
                    immagine_metadati_en = x.immagine_metadati_en,
                    immagine_metadati_it = x.immagine_metadati_it,
                    testo_esteso_metadati_en = x.testo_esteso_metadati_en,
                    testo_esteso_metadati_it = x.testo_esteso_metadati_it,
                    ordine = x.ordine
                }).ToList();
            }
            return items;
        }


    }
}
