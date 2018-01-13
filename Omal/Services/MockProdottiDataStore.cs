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
    public class MockProdottiDataStore : IDataStore<Models.Prodotto>
    {
        List<Models.Prodotto> items;

        public MockProdottiDataStore()
        {
            items = new List<Models.Prodotto>();
            var mockItems = new List<Models.Prodotto>
            {
                new Models.Prodotto(){  IdProdotto=1, Nome = "Primo", NomeEn= "First", Descrizione="Descrizione Primo",  DescrizioneEn="First Description", Codice = "Uno", IdCategoria=3, Ordine = 0, Tipologia="Valvola", ImmaginePlaceHolder = "https://picsum.photos/50/50?image=0" , VisibileApp = true }
            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.Prodotto item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Prodotto item)
        {
            var _item = items.Where((Models.Prodotto arg) => arg.IdProdotto == item.IdProdotto).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Prodotto arg) => arg.IdProdotto == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Prodotto> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdProdotto == id));
        }

        public async Task<IEnumerable<Models.Prodotto>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
