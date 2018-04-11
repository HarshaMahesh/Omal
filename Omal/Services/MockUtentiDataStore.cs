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
                new Models.Utente(){  Email="luca.cavedaghi@cualeva.com", IdUtente=1, NomeUtente="Luca"},
                new Models.Utente(){  NomeUtente="Elio", Email="elio.cattozzo@docsmarshal.com", IdUtente=2},
            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.Utente item)
        {
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.Utente item)
        {
            var _item = items.Where((Models.Utente arg) => arg.IdUtente == item.IdUtente).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
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

        public Task<Models.Token> Login(string email, string password)
        {
            throw new NotImplementedException();
        }

        public Task<IEnumerable<Utente>> GetLastItemsUpdatesAsync()
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> RecoverPassword(string email)
        {
            throw new NotImplementedException();
        }

        public Task<ResponseBase> UpdateCurUtente(string userName, string emailBackOffice, string password, string passwordConfirm)
        {
            throw new NotImplementedException();
        }
    }
}
