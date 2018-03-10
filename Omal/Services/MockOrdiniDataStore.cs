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
    public class MockOrdiniDataStore : IDataStore<Models.Ordine>
    {
        List<Models.Ordine> items;

        public MockOrdiniDataStore()
        {
            items = new List<Models.Ordine>();
            var mockItems = new List<Models.Ordine>
            {

            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.Ordine item)
        {
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.Ordine item)
        {
            var _item = items.Where((Models.Ordine arg) => arg.IdOrdine == item.IdOrdine).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Ordine arg) => arg.IdOrdine == id).FirstOrDefault();
            items.Remove(_item);
            return await Task.FromResult(true);
        }

        public async Task<Models.Ordine> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdOrdine == id));
        }

        public async Task<IEnumerable<Models.Ordine>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
