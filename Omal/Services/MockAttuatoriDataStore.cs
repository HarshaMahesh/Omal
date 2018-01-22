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
    public class MockAttuatoriDataStore : IDataStore<Models.Attuatore>
    {
        List<Models.Attuatore> items;

        public MockAttuatoriDataStore()
        {
            items = new List<Models.Attuatore>();
            var mockItems = new List<Models.Attuatore>
            {
                new Attuatore(){ Codice_Articolo="", Giacenza=0, IdCodiceAttuatore=1, IdProdotto=1, immagine_placeholder="", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="", Valore_iso="", Valore_misura="" },
            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.Attuatore item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Attuatore item)
        {
            var _item = items.Where((Models.Attuatore arg) => arg.IdCodiceAttuatore == item.IdCodiceAttuatore).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Attuatore arg) => arg.IdCodiceAttuatore == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Attuatore> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdCodiceAttuatore == id));
        }

        public async Task<IEnumerable<Models.Attuatore>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
