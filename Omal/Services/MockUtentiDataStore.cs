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
    public class MockUtentiDataStore : IDataStore<Models.Utente>
    {
        List<Models.Utente> items;

        public MockUtentiDataStore()
        {
            items = new List<Models.Utente>();
            var mockItems = new List<Models.Utente>
            {
                new Models.Utente(){   Cognome= "Cavedaghi", Email="luca.cavedaghi@cualeva.com", IdUtente=1, Nome="Luca", Password="Luca" }
            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.Utente item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Utente item)
        {
            var _item = items.Where((Models.Utente arg) => arg.IdUtente == item.IdUtente).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Utente arg) => arg.IdUtente == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Utente> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdUtente == id));
        }

        public async Task<IEnumerable<Models.Utente>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
