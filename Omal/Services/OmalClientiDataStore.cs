﻿using System;
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
    public class OmalClientiDataStore : IDataStore<Models.Cliente>
    {
        List<Models.Cliente> items;
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();

        public OmalClientiDataStore()
        {
            items = new List<Models.Cliente>();
            client = new HttpClient();
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.Cliente item)
        {
            var url = string.Format("{0}{1}?tabella=clienti", App.BackendUrl, "webservicei.php");
            if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
            var formContent = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("IDToken", App.CurToken.token),
                new KeyValuePair<string, string>("RagioneSociale", item.RagioneSociale),
                new KeyValuePair<string, string>("CognomeNome", item.CognomeNome),
                new KeyValuePair<string, string>("CodiceFiscale", item.CodiceFiscale),
                new KeyValuePair<string, string>("Piva", item.Piva),
                new KeyValuePair<string, string>("Email", item.Email),
                new KeyValuePair<string, string>("Telefono", item.Telefono),
                new KeyValuePair<string, string>("Fax", item.Fax),
                new KeyValuePair<string, string>("Indirizzo", item.Indirizzo),
                new KeyValuePair<string, string>("Cap", item.Cap),
                new KeyValuePair<string, string>("Citta", item.Citta),
                new KeyValuePair<string, string>("Provincia", item.Provincia),
                new KeyValuePair<string, string>("Nazione", item.Nazione),
                new KeyValuePair<string, string>("societapersona", "societa"),
            });
            var response = await client.PostAsync(url, formContent);
            var json = await response.Content.ReadAsStringAsync();
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            var risposta = JsonConvert.DeserializeAnonymousType(json, new { data = new List<ResponseClienti>() }, jsonSerializerSettings).data.FirstOrDefault();
            if (risposta.HasError == 0 && risposta.IDCliente.HasValue)
                item.IDCliente = risposta.IDCliente.Value;
            items.Add(item);
            return await Task.FromResult(risposta);
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.Cliente item)
        {
            var url = string.Format("{0}{1}?tabella=clienti", App.BackendUrl, "webservicei.php");
            if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
            var formContent = new FormUrlEncodedContent(new[]
           {
                new KeyValuePair<string, string>("IDToken", App.CurToken.token),
                new KeyValuePair<string, string>("IDCliente", item.IDCliente.ToString()),
                new KeyValuePair<string, string>("RagioneSociale", item.RagioneSociale),
                new KeyValuePair<string, string>("CognomeNome", item.CognomeNome),
                new KeyValuePair<string, string>("CodiceFiscale", item.CodiceFiscale),
                new KeyValuePair<string, string>("Piva", item.Piva),
                new KeyValuePair<string, string>("Email", item.Email),
                new KeyValuePair<string, string>("Telefono", item.Telefono),
                new KeyValuePair<string, string>("Fax", item.Fax),
                new KeyValuePair<string, string>("Indirizzo", item.Indirizzo),
                new KeyValuePair<string, string>("Cap", item.Cap),
                new KeyValuePair<string, string>("Citta", item.Citta),
                new KeyValuePair<string, string>("Provincia", item.Provincia),
                new KeyValuePair<string, string>("Nazione", item.Nazione),
                new KeyValuePair<string, string>("societapersona", "societa"),
            });
            var response = await client.PostAsync(url, formContent);
            var json = await response.Content.ReadAsStringAsync();
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            var risposta = JsonConvert.DeserializeAnonymousType(json, new { data = new List<ResponseClienti>() }, jsonSerializerSettings).data.FirstOrDefault();
            if (risposta.HasError ==0)
            {
                var _item = items.Where((Models.Cliente arg) => arg.IDCliente == item.IDCliente).FirstOrDefault();
                items.Remove(_item);
                items.Add(item);
            }
            return await Task.FromResult(risposta);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Cliente arg) => arg.IDCliente == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Cliente> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IDCliente == id));
        }

        public async Task<IEnumerable<Models.Cliente>> GetItemsAsync(bool forceRefresh = false)
        {
            if (items == null || items.Count == 0)
            {
                items = await Connection.Table<Models.Cliente>().ToListAsync();
            }
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=clienti", App.BackendUrl, "webservice.php");
                if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
                var json = await client.GetStringAsync(url);
                items = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new { data = new List<Models.Cliente>() }).data);
                foreach (var item in items)
                    Connection.InsertOrReplaceAsync(item);
            }
            return items;
        }


    }
}
