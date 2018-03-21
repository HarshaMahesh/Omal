using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Omal.Models;
using Omal.Persistence;
using Xamarin.Forms;

namespace Omal.Services
{
    public class OmalUtentiDataStore : IUtentiDataStore
    {
        List<Models.Utente> items;
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();


        public OmalUtentiDataStore()
        {
            items = new List<Models.Utente>();
            client = new HttpClient();
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

        public async Task<Models.Token> Login(string email, string password)
        {
            /*
idtoken "38"
token   "3YGSHWCUA7"
dataora_inserimento "2018-02-14 21:45:28"
dataora_scadenza    "2018-02-14 23:45:28"
IDUtente    "2"
dataora_server  "2018-02-14 21:45:28"
email_utente    "info@omal.it"
NomeUtente  "OMAL SpA"
            */
            var url = string.Format("{0}{1}?token=1&user={2}&pass={3}", App.BackendUrl, "login.php",email,password);
            var json = await client.GetStringAsync(url);
            try
            {
                var utente = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new { data = new List<tok>() }).data[0].tokens[0]);
                utente.DifferenzaOrarioServerDispositivo = (utente.dataora_server - DateTime.Now);
                return utente;
            }
            catch (Exception ex)
            {
                
            }
            return null;
        }

        public Task<IEnumerable<Utente>> GetLastItemsUpdatesAsync()
        {
            throw new NotImplementedException();
        }

        class tok
        {
            public List<Models.Token> tokens { get; set; }
        }



    }

}
