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
                new Models.ProdottoMetadati () { IdGruppoMetadato =1,  IdMetadato=1, ImmagineMetadatiEn="https://picsum.photos/50/50?image=35", ImmagineMetadatiIt ="https://picsum.photos/50/50?image=40", Ordine=1, TestoEstesoMetadatiEn="Inglese Descrizione completa delle caratterisctiche del prodotto 1", TestoEstesoMetadatiIt="Descrizione completa delle caratterisctiche del prodotto 1"},
                new Models.ProdottoMetadati () { IdGruppoMetadato =1,  IdMetadato=4, ImmagineMetadatiEn="https://picsum.photos/50/50?image=35", ImmagineMetadatiIt ="https://picsum.photos/50/50?image=40", Ordine=1, TestoEstesoMetadatiEn="Inglese Descrizione completa delle caratterisctiche del prodotto 1", TestoEstesoMetadatiIt="Nuovo testo esteso"},
                new Models.ProdottoMetadati () { IdGruppoMetadato =2,  IdMetadato=2, ImmagineMetadatiEn="https://picsum.photos/50/50?image=45", ImmagineMetadatiIt ="https://picsum.photos/50/50?image=50", Ordine=2, TestoEstesoMetadatiEn="Inglese Descrizione completa dei benefit del prodotto 1", TestoEstesoMetadatiIt="Italiano Descrizione completa dei benefit del prodotto 1"},
                new Models.ProdottoMetadati () { IdGruppoMetadato =10,  IdMetadato=3, ImmagineMetadatiEn="https://picsum.photos/50/50?image=55", ImmagineMetadatiIt ="https://picsum.photos/50/50?image=60", Ordine=3, TestoEstesoMetadatiEn="Inglese Descrizione dei materiali del prodotto 2", TestoEstesoMetadatiIt="Descrizione dei materiali del prodotto 2"},
            };
            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.ProdottoMetadati item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.ProdottoMetadati item)
        {
            var _item = items.Where((Models.ProdottoMetadati arg) => arg.IdMetadato == item.IdMetadato).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.ProdottoMetadati arg) => arg.IdMetadato == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.ProdottoMetadati> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdMetadato == id));
        }

        public async Task<IEnumerable<Models.ProdottoMetadati>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
