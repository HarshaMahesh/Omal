using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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
            item.CodiceOrdine = Guid.NewGuid().ToString("N");
            item.IDUtente = App.CurUser.IdUtente;
            var url = string.Format("{0}{1}?tabella=ordinicarrelli", App.BackendUrl, "webservicei.php");
            if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);

            var Kiavi = new List<KeyValuePair<string,string>>();
            Kiavi.AddRange(new[]
           {
                new KeyValuePair<string, string>("IDToken", App.CurToken.token),
                new KeyValuePair<string, string>("IdCliente", item.IdCliente.ToString()),
                new KeyValuePair<string, string>("CodiceOrdine", item.CodiceOrdine),
                new KeyValuePair<string, string>("DataInizio", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss")),
                new KeyValuePair<string, string>("DataFine", item.DataFine.HasValue?item.DataFine.Value.ToString("yyyy-MM-dd HH:mm:ss"):""),
                new KeyValuePair<string, string>("DataEliminazione", ""),
                new KeyValuePair<string, string>("Note", item.Note),
                new KeyValuePair<string, string>("TotaleConSconto", item.TotaleConSconto.ToString("f2")),
                new KeyValuePair<string, string>("Totale", item.Totale.ToString("f2")),
                new KeyValuePair<string, string>("Sconto", item.Sconto.ToString("f2")),
                new KeyValuePair<string, string>("Annullato", "0"),
                new KeyValuePair<string, string>("Stato", ((int)item.Stato).ToString()),
            });
            int i = 0;

            foreach (var carrello in item.carrelli)
            {
                Kiavi.AddRange(new[]
                {
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][Tipologia]",i), carrello.Tipologia),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][IdArticolo]",i), carrello.IdArticolo.ToString()),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][CodiceArticolo]",i), carrello.CodiceArticolo),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][DescrizioneCarrello_It]",i), carrello.DescrizioneCarrello_It),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][DescrizioneCarrello_En]",i), carrello.DescrizioneCarrello_En),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][PrezzoUnitario]",i), carrello.PrezzoUnitario.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][PrezzoUnitarioScontato]",i), carrello.PrezzoUnitarioScontato.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][Sconto]",i), carrello.Sconto.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][Qta]",i), carrello.Qta.ToString()),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][IdProdotto]",i), carrello.IdProdotto.ToString())
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
            if (item == null) throw new ArgumentNullException("item");
            item.IDUtente = App.CurUser.IdUtente;
            if (string.IsNullOrWhiteSpace(item.CodiceOrdine)) return await AddItemAsync(item);
            var url = string.Format("{0}{1}?tabella=ordinicarrelli", App.BackendUrl, "webservicei.php");
            if (App.CurToken != null) url += string.Format("&token={0}", App.CurToken.token);

            var Kiavi = new List<KeyValuePair<string, string>>();
            Kiavi.AddRange(new[]
           {
                new KeyValuePair<string, string>("IDToken", App.CurToken.token),
                new KeyValuePair<string, string>("IdCliente", item.IdCliente.ToString()),
                new KeyValuePair<string, string>("CodiceOrdine", item.CodiceOrdine),
                new KeyValuePair<string, string>("Note", item.Note),
                new KeyValuePair<string, string>("TotaleConSconto", item.TotaleConSconto.ToString("f2")),
                new KeyValuePair<string, string>("Totale", item.Totale.ToString("f2")),
                new KeyValuePair<string, string>("Sconto", item.Sconto.ToString("f2")),
                new KeyValuePair<string, string>("Stato", ((int)item.Stato).ToString()),
            });
            if (item.IdOrdine != 0) Kiavi.Add(new KeyValuePair<string, string>("IdOrdine", item.IdOrdine.ToString()));
            if (item.DataInizio.HasValue) Kiavi.Add(new KeyValuePair<string, string>("DataInizio", item.DataInizio.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            if (item.DataFine.HasValue) Kiavi.Add(new KeyValuePair<string, string>("DataFine", item.DataFine.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            if (item.DataEliminazione.HasValue) Kiavi.Add(new KeyValuePair<string, string>("DataEliminazione", item.DataEliminazione.Value.ToString("yyyy-MM-dd HH:mm:ss")));
            Kiavi.Add(new KeyValuePair<string, string>("Annullato", (item.Stato == Models.Enums.EOrdineStato.ordineAnnullato) ? "1" : "0"));
            int i = 0;
            foreach (var carrello in item.carrelli)
            {
                Kiavi.AddRange(new[]
                {
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][Tipologia]",i), carrello.Tipologia),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][IdArticolo]",i), carrello.IdArticolo.ToString()),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][CodiceArticolo]",i), carrello.CodiceArticolo),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][DescrizioneCarrello_It]",i), carrello.DescrizioneCarrello_It),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][DescrizioneCarrello_En]",i), carrello.DescrizioneCarrello_En),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][PrezzoUnitario]",i), carrello.PrezzoUnitario.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][PrezzoUnitarioScontato]",i), carrello.PrezzoUnitarioScontato.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][Sconto]",i), carrello.Sconto.ToString("f2")),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][Qta]",i), carrello.Qta.ToString()),
                    new KeyValuePair<string, string>(string.Format("carrelli[{0}][IdProdotto]",i), carrello.IdProdotto.ToString())
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
                JsonSerializerSettings settings = new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    MissingMemberHandling = MissingMemberHandling.Ignore,
                    Formatting = Formatting.None,
                    DateFormatHandling = DateFormatHandling.IsoDateFormat,
                    Converters = new List<JsonConverter> { new DecimalConverter() }
                };
                items = await Task.Run(() => JsonConvert.DeserializeAnonymousType(json, new { Data = new List<Models.Ordine>() }).Data);
                if (forceRefresh)
                {
                    await Connection.DropTableAsync<Models.Ordine>();
                    await Connection.CreateTableAsync<Models.Ordine>();
                }
                foreach (var item in items)
                    Connection.InsertOrReplaceAsync(item);
            }
            var clienti = await Connection.Table<Models.Cliente>().ToListAsync();
            foreach (var item in items)
            {
                var cliente = clienti.FirstOrDefault(x => x.IDCliente == item.IdCliente);
                if (cliente != null) item.ClienteRagSoc = cliente.RagioneSociale;
            }
            return items;
        }

        class DecimalConverter : JsonConverter
        {
            public override bool CanConvert(Type objectType)
            {
                return (objectType == typeof(double?) || objectType == typeof(double) ||  objectType == typeof(decimal) || objectType == typeof(decimal?) || objectType ==  typeof(int) || objectType == typeof(int?));
            }

            public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
            {
                JToken token = JToken.Load(reader);
                if (token.Type == JTokenType.Float || token.Type == JTokenType.Integer)
                {
                    return token.ToObject<decimal>();
                }
                if (token.Type == JTokenType.String)
                {
                    // customize this to suit your needs
                    var valore = Decimal.Parse(token.ToString(),System.Globalization.CultureInfo.GetCultureInfo("en-US"));
                    return valore;
                }
                if (token.Type == JTokenType.Null && objectType == typeof(decimal?))
                {
                    return null;
                }
                throw new JsonSerializationException("Unexpected token type: " +
                                                      token.Type.ToString());
            }

            public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
            {
                throw new NotImplementedException();
            }
        }
    }
}
