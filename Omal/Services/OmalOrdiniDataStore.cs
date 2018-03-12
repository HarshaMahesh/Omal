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
    public class OmalOrdiniDataStore : IDataStore<Models.Ordine>
    {
        List<Models.Ordine> items;
        HttpClient client;
        public SQLite.SQLiteAsyncConnection Connection => DependencyService.Get<ISQLiteDb>().GetConnection();

        public OmalOrdiniDataStore()
        {
            items = new List<Models.Ordine>();
            client = new HttpClient();
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.Ordine item)
        {
            if (String.IsNullOrWhiteSpace(item.CodiceOrdine)) throw new ArgumentNullException("CodiceOrdine vuoto");
            var url = string.Format("{0}{1}?tabella=ordinicarrelli", App.BackendUrl, "webservicei.php");
            if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);

            var Kiavi = new List<KeyValuePair<string,string>>();
            Kiavi.AddRange(new[]
           {
                new KeyValuePair<string, string>("IDToken", App.CurToken.token),
                new KeyValuePair<string, string>("IDCliente", item.IdCliente.ToString()),
                new KeyValuePair<string, string>("codice_ordine", item.CodiceOrdine),
                new KeyValuePair<string, string>("datainizio_ordine", DateTime.Now.ToString("yyyyMMdd")),
                new KeyValuePair<string, string>("datafine_ordine", ""),
                new KeyValuePair<string, string>("dataeliminazione_ordine", ""),
                new KeyValuePair<string, string>("note_ordine", ""),
                new KeyValuePair<string, string>("TotaleOrdine", item.Totale.ToString("f2")),
                new KeyValuePair<string, string>("TotaleOrdinePartenza", item.Totale.ToString("f2")),
                new KeyValuePair<string, string>("Sconto", item.Sconto.ToString("f2")),
                new KeyValuePair<string, string>("Annullato", "0"),
                new KeyValuePair<string, string>("Stato", ((int)item.Stato).ToString()),
            });
            int i = 0;
            foreach (var carrello in item.carrelli)
            {
                Kiavi.AddRange(new[]
                {
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][tipologia]",i), carrello.Tipologia),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][idarticolo]",i), carrello.IdArticolo.ToString()),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][codice_articolo]",i), carrello.CodiceArticolo),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][descrizionecarrello_it]",i), carrello.DescrizioneCarrello_It),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][descrizionecarrello_en]",i), carrello.DescrizioneCarrello_En),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][prezzo_cadaunoPartenza]",i), carrello.PrezzoUnitario.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][prezzo_cadauno]",i), carrello.PrezzoUnitarioScontato.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][Sconto]",i), carrello.Sconto.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][quantita]",i), carrello.Qta.ToString()),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][IDProdotto]",i), carrello.IdProdotto.ToString())
                });
                i++;
            }
            var formContent = new FormUrlEncodedContent(Kiavi.ToArray());

            string contenuto = String.Join(",", Kiavi.Select(x => string.Format("{0}={1}", x.Key, x.Value)));
            var response = await client.PostAsync(url, formContent);
            var json = await response.Content.ReadAsStringAsync();
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            var risposta = JsonConvert.DeserializeAnonymousType(json, new { data = new List<ResponseOrdini>() }, jsonSerializerSettings).data.FirstOrDefault();
            if (risposta.HasError == 0 && risposta.IDOrdine.HasValue)
            {
                item.IdOrdine = risposta.IDOrdine.Value;
                foreach (var carrello in item.carrelli)
                    carrello.IdOrdine = item.IdOrdine;
                item.jsonCarrelliSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(item.carrelli);
                items.Add(item);
                await Connection.InsertOrReplaceAsync(item);
            }
            return await Task.FromResult(risposta);
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.Ordine item)
        {
            var url = string.Format("{0}{1}?tabella=ordinicarrelli", App.BackendUrl, "webservicei.php");
            if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);

            var Kiavi = new List<KeyValuePair<string, string>>();
            Kiavi.AddRange(new[]
           {
                new KeyValuePair<string, string>("IDToken", App.CurToken.token),
                new KeyValuePair<string, string>("IDCliente", item.IdCliente.ToString()),
                new KeyValuePair<string, string>("codice_ordine", item.CodiceOrdine),
                new KeyValuePair<string, string>("note_ordine", ""),
                new KeyValuePair<string, string>("TotaleOrdine", item.Totale.ToString("f2")),
                new KeyValuePair<string, string>("TotaleOrdinePartenza", item.Totale.ToString("f2")),
                new KeyValuePair<string, string>("Sconto", item.Sconto.ToString("f2")),
                new KeyValuePair<string, string>("Stato", ((int)item.Stato).ToString()),
            });
            if (item.DataInizio.HasValue) Kiavi.Add(new KeyValuePair<string, string>("datainizio_ordine", item.DataInizio.Value.ToString("yyyyMMdd")));
            if (item.DataFine.HasValue) Kiavi.Add(new KeyValuePair<string, string>("datafine_ordine", item.DataFine.Value.ToString("yyyyMMdd")));
            if (item.DataEliminazione.HasValue) Kiavi.Add(new KeyValuePair<string, string>("dataeliminazione_ordine", item.DataEliminazione.Value.ToString("yyyyMMdd")));
            Kiavi.Add(new KeyValuePair<string, string>("Annullato", (item.Stato == Models.Enums.EOrdineStato.ordineAnnullato) ? "1" : "0"));
            int i = 0;
            foreach (var carrello in item.carrelli)
            {
                Kiavi.AddRange(new[]
                {
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][tipologia]",i), carrello.Tipologia),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][idarticolo]",i), carrello.IdArticolo.ToString()),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][codice_articolo]",i), carrello.CodiceArticolo),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][descrizionecarrello_it]",i), carrello.DescrizioneCarrello_It),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][descrizionecarrello_en]",i), carrello.DescrizioneCarrello_En),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][prezzo_cadaunoPartenza]",i), carrello.PrezzoUnitario.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][prezzo_cadauno]",i), carrello.PrezzoUnitarioScontato.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][Sconto]",i), carrello.Sconto.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][quantita]",i), carrello.Qta.ToString()),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][IDProdotto]",i), carrello.IdProdotto.ToString())
                });
                i++;
            }
            var formContent = new FormUrlEncodedContent(Kiavi.ToArray());
            var response = await client.PostAsync(url, formContent);
            var json = await response.Content.ReadAsStringAsync();
            var jsonSerializerSettings = new JsonSerializerSettings();
            jsonSerializerSettings.MissingMemberHandling = MissingMemberHandling.Ignore;
            var risposta = JsonConvert.DeserializeAnonymousType(json, new { data = new List<ResponseOrdini>() }, jsonSerializerSettings).data.FirstOrDefault();
            if (risposta.HasError == 0)
            {
                item.jsonCarrelliSerialized = Newtonsoft.Json.JsonConvert.SerializeObject(item.carrelli);
                await Connection.UpdateAsync(item);
            }
            return await Task.FromResult(risposta);
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
            if (items == null || items.Count == 0)
            {
                items = await Connection.Table<Models.Ordine>().OrderBy(x => x.CodiceOrdine).ToListAsync();
            }
            if ((items.Count == 0 || forceRefresh) && CrossConnectivity.Current.IsConnected)
            {
                var url = string.Format("{0}{1}?tabella=ordini", App.BackendUrl, "webservice.php");
                if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);
                var json = await client.GetStringAsync(url);
                items = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new { Data = new List<Models.Ordine>() }).Data);
                foreach (var item in items)
                    Connection.InsertOrReplaceAsync(item);
                return items;
            }
            var clienti = await Connection.Table<Models.Cliente>().ToListAsync();
            foreach (var item in items)
            {
                var cliente = clienti.FirstOrDefault(x => x.IDCliente == item.IdCliente);
                if (cliente != null) item.ClienteRagSoc = cliente.RagioneSociale;
            }
            return items;
        }


    }
}
