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
                new Models.Categoria () { Codice="Codice03", Descrizione ="Descrizione valvola a sfera", DescrizioneEn="Ball valves description", IdCategoria=3, IdPadre = 0, Nome ="Valvole a sfera", NomeEn="Ball valves", Ordine=1, Tipologia="valvola", VisibileApp=true},
new Models.Categoria () { Codice="Codice04", Descrizione ="Descrizione valvola a farfalla", DescrizioneEn="Butterfly valves description", IdCategoria=4, IdPadre = 0, Nome ="Valvole a farfalla", NomeEn="Butterfly valves", Ordine=2, Tipologia="valvola", VisibileApp=true},
new Models.Categoria () { Codice="Codice05", Descrizione ="Descrizione valvole magnum", DescrizioneEn="Magnum valves description", IdCategoria=5, IdPadre = 0, Nome ="Valvole Magnum", NomeEn="Magnum valves", Ordine=3, Tipologia="valvola", VisibileApp=true},
new Models.Categoria () { Codice="Codice06", Descrizione ="Descrizione valvole a sfera in PVC", DescrizioneEn="PVC ball valves description", IdCategoria=6, IdPadre = 3, Nome ="Valvole a sfera in PVC", NomeEn="PVC ball valves", Ordine=1, Tipologia="valvola", VisibileApp=true},
new Models.Categoria () { Codice="Codice07", Descrizione ="Descrizione Magnum wafer", DescrizioneEn="Magnum wafer description", IdCategoria=7, IdPadre = 5, Nome ="Magnum Wafer", NomeEn="Magnum Wafer", Ordine=1, Tipologia="valvola", VisibileApp=true},
new Models.Categoria () { Codice="Codice08", Descrizione ="Descrizione Magnum split wafer", DescrizioneEn="Magnum split wafer description", IdCategoria=8, IdPadre = 5, Nome ="Magnum split wafer", NomeEn="Magnum split wafer", Ordine=2, Tipologia="valvola", VisibileApp=true},
new Models.Categoria () { Codice="Codice09", Descrizione ="Descrizione Magnum split wafer 3 vie 4 guarnizioni", DescrizioneEn="Magnum split wafer 3 wey 4 seals description", IdCategoria=9, IdPadre = 8, Nome ="Magnum split wafer 3 vie 4 guarnizioni", NomeEn="Magnum split wafer 3 wey 4 seals", Ordine=3, Tipologia="valvola", VisibileApp=true},
new Models.Categoria () { Codice="Codice10", Descrizione ="Descrizione accessori attuatori", DescrizioneEn="Actuators Accessories description", IdCategoria=10, IdPadre = 0, Nome ="Attuatori Accessori", NomeEn="Accessories", Ordine=4, Tipologia="attuatore", VisibileApp=true},
new Models.Categoria () { Codice="Codice11", Descrizione ="Descrizione attuatori elettrici", DescrizioneEn="Electrical Actuators description", IdCategoria=11, IdPadre = 0, Nome ="Attuatori Elettrici", NomeEn="Electrical", Ordine=5, Tipologia="attuatore", VisibileApp=true},
new Models.Categoria () { Codice="Codice12", Descrizione ="Descrizione attuatori pneumatici", DescrizioneEn="Pneumatic actuators description", IdCategoria=12, IdPadre = 0, Nome ="Attuatori pneumatici", NomeEn="Pneumatic", Ordine=6, Tipologia="attuatore", VisibileApp=true},
new Models.Categoria () { Codice="Codice13", Descrizione ="Descrizione Elettrovalvole e bobine", DescrizioneEn="Solenoid and coils description", IdCategoria=13, IdPadre = 10, Nome ="Elettrovalvole e bobine", NomeEn="Solenoid and coils", Ordine=1, Tipologia="attuatore", VisibileApp=true},
new Models.Categoria () { Codice="Codice20", Descrizione ="Descrizione attuatori pneumatici in acciaio carbonio", DescrizioneEn="Carbon steel description", IdCategoria=20, IdPadre = 12, Nome ="attuatori pneumatici in acciaio carbonio", NomeEn="Carbon steel", Ordine=1, Tipologia="attuatore", VisibileApp=true},
new Models.Categoria () { Codice="Codice21", Descrizione ="Descrizione attuatori pneumatici in acciaio inox", DescrizioneEn="Sailess steel description", IdCategoria=21, IdPadre = 12, Nome ="attuatori pneumatici in acciaio inox", NomeEn="Sailess steel", Ordine=2, Tipologia="attuatore", VisibileApp=true},
            };

            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.Categoria item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Categoria item)
        {
            var _item = items.Where((Models.Categoria arg) => arg.IdCategoria == item.IdCategoria).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Categoria arg) => arg.IdCategoria == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Categoria> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdCategoria == id));
        }

        public async Task<IEnumerable<Models.Categoria>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
