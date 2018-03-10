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
    public class MockProdottoMetadatiDataStore : IDataStore<Models.ProdottoMetadati>
    {
        List<Models.ProdottoMetadati> items;

        public MockProdottoMetadatiDataStore()
        {
            items = new List<Models.ProdottoMetadati>();
            var mockItems = new List<Models.ProdottoMetadati>
            {
                new Models.ProdottoMetadati () { idgruppometadato =1,  idmetadato=1, immagine_metadati_en="https://picsum.photos/50/50?image=35", immagine_metadati_it ="https://picsum.photos/50/50?image=40", ordine=1, testo_esteso_metadati_en="Inglese Descrizione completa delle caratterisctiche del prodotto 1", testo_esteso_metadati_it="Descrizione completa delle caratterisctiche del prodotto 1"},
                new Models.ProdottoMetadati () { idgruppometadato =1,  idmetadato=4, immagine_metadati_en="https://picsum.photos/50/50?image=35", immagine_metadati_it ="https://picsum.photos/50/50?image=40", ordine=1, testo_esteso_metadati_en="Inglese Descrizione completa delle caratterisctiche del prodotto 1", testo_esteso_metadati_it="Nuovo testo esteso"},
                new Models.ProdottoMetadati () { idgruppometadato =2,  idmetadato=2, immagine_metadati_en="https://picsum.photos/50/50?image=45", immagine_metadati_it="https://picsum.photos/50/50?image=50", ordine=2, testo_esteso_metadati_en="Inglese Descrizione completa dei benefit del prodotto 1", testo_esteso_metadati_it="Italiano Descrizione completa dei benefit del prodotto 1"},
                new Models.ProdottoMetadati () { idgruppometadato =10,  idmetadato=3, immagine_metadati_en="https://picsum.photos/50/50?image=55", immagine_metadati_it ="https://picsum.photos/50/50?image=60", ordine=3, testo_esteso_metadati_en="Inglese Descrizione dei materiali del prodotto 2", testo_esteso_metadati_it="Descrizione dei materiali del prodotto 2"},
            };
            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.ProdottoMetadati item)
        {
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.ProdottoMetadati item)
        {
            var _item = items.Where((Models.ProdottoMetadati arg) => arg.idmetadato == item.idmetadato).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.ProdottoMetadati arg) => arg.idmetadato == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.ProdottoMetadati> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idmetadato == id));
        }

        public async Task<IEnumerable<Models.ProdottoMetadati>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
