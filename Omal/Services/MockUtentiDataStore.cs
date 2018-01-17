using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Omal.Models;

namespace Omal.Services
{
    public class MockUtentiDataStore : IUtentiDataStore
    {
        List<Models.Utente> items;

        public MockUtentiDataStore()
        {
            items = new List<Models.Utente>();
            var mockItems = new List<Models.Utente>
            {
                new Models.Utente(){   Cognome= "Cavedaghi", Email="luca.cavedaghi@cualeva.com", IdUtente=1, Nome="Luca", Password="Luca" },
                new Models.Utente(){ Cognome="Cattozzo", Nome="Elio", Email="elio.cattozzo@docsmarshal.com", IdUtente=2, Password="elio" },

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

        public Utente Login(string email, string password)
        {
            return GetItemsAsync().Result.FirstOrDefault(x => string.Equals(x.Email, email, StringComparison.InvariantCultureIgnoreCase) && string.Equals(x.Password, password));
        }
    }
}
