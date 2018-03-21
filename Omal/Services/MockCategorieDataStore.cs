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
    public class MockCategorieDataStore : IDataStore<Models.Categoria>
    {
        List<Models.Categoria> items;

        public MockCategorieDataStore()
        {
            items = new List<Models.Categoria>();
            var mockItems = new List<Models.Categoria>
            {
                new Models.Categoria () { codice="Codice03", descrizione ="Descrizione valvola a sfera", descrizione_en="Ball valves description", idCategoria=3, idPadre = 0, nome ="Valvole a sfera", nome_en="Ball valves", ordine=1, tipologia="valvola", visibile_app=1},
                new Models.Categoria () { codice="Codice04", descrizione ="Descrizione valvola a farfalla", descrizione_en="Butterfly valves description", idCategoria=4, idPadre = 0, nome ="Valvole a farfalla", nome_en="Butterfly valves", ordine=2, tipologia="valvola", visibile_app=1},
                new Models.Categoria () { codice="Codice05", descrizione ="Descrizione valvole magnum", descrizione_en="Magnum valves description", idCategoria=5, idPadre = 0, nome ="Valvole Magnum", nome_en="Magnum valves", ordine=3, tipologia="valvola", visibile_app=1},
                new Models.Categoria () { codice="Codice06", descrizione ="Descrizione valvole a sfera in PVC", descrizione_en="PVC ball valves description", idCategoria=6, idPadre = 3, nome ="Valvole a sfera in PVC", nome_en="PVC ball valves", ordine=1, tipologia="valvola", visibile_app=1},
                new Models.Categoria () { codice="Codice07", descrizione ="Descrizione Magnum wafer", descrizione_en="Magnum wafer description", idCategoria=7, idPadre = 5, nome ="Magnum Wafer", nome_en="Magnum Wafer", ordine=1, tipologia="valvola", visibile_app=1},
                new Models.Categoria () { codice="Codice08", descrizione ="Descrizione Magnum split wafer", descrizione_en="Magnum split wafer description", idCategoria=8, idPadre = 5, nome ="Magnum split wafer", nome_en="Magnum split wafer", ordine=2, tipologia="valvola", visibile_app=1},
                new Models.Categoria () { codice="Codice09", descrizione ="Descrizione Magnum split wafer 3 vie 4 guarnizioni", descrizione_en="Magnum split wafer 3 wey 4 seals description", idCategoria=9, idPadre = 8, nome ="Magnum split wafer 3 vie 4 guarnizioni", nome_en="Magnum split wafer 3 wey 4 seals", ordine=3, tipologia="valvola", visibile_app=1},
                new Models.Categoria () { codice="Codice10", descrizione ="Descrizione accessori attuatori", descrizione_en="Actuators Accessories description", idCategoria=10, idPadre = 0, nome ="Attuatori Accessori", nome_en="Accessories", ordine=4, tipologia="attuatore", visibile_app=1},
new Models.Categoria () { codice="Codice11", descrizione ="Descrizione attuatori elettrici", descrizione_en="Electrical Actuators description", idCategoria=11, idPadre = 0, nome ="Attuatori Elettrici", nome_en="Electrical", ordine=5, tipologia="attuatore", visibile_app=1},
new Models.Categoria () { codice="Codice12", descrizione ="Descrizione attuatori pneumatici", descrizione_en="Pneumatic actuators description", idCategoria=12, idPadre = 0, nome ="Attuatori pneumatici", nome_en="Pneumatic", ordine=6, tipologia="attuatore", visibile_app=1},
new Models.Categoria () { codice="Codice13", descrizione ="Descrizione Elettrovalvole e bobine", descrizione_en="Solenoid and coils description", idCategoria=13, idPadre = 10, nome ="Elettrovalvole e bobine", nome_en="Solenoid and coils", ordine=1, tipologia="attuatore", visibile_app=1},
new Models.Categoria () { codice="Codice20", descrizione ="Descrizione attuatori pneumatici in acciaio carbonio", descrizione_en="Carbon steel description", idCategoria=20, idPadre = 12, nome ="attuatori pneumatici in acciaio carbonio", nome_en="Carbon steel", ordine=1, tipologia="attuatore", visibile_app=1},
new Models.Categoria () { codice="Codice21", descrizione ="Descrizione attuatori pneumatici in acciaio inox", descrizione_en="Sailess steel description", idCategoria=21, idPadre = 12, nome ="attuatori pneumatici in acciaio inox", nome_en="Sailess steel", ordine=2, tipologia="attuatore", visibile_app=1},
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.Categoria item)
        {
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.Categoria item)
        {
            var _item = items.Where((Models.Categoria arg) => arg.idCategoria == item.idCategoria).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Categoria arg) => arg.idCategoria == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Categoria> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idCategoria == id));
        }

        public async Task<IEnumerable<Models.Categoria>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public Task<IEnumerable<Categoria>> GetLastItemsUpdatesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
