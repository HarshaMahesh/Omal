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
    public class MockProdottoGruppiMetadatiDataStore : IDataStore<Models.ProdottoGruppiMetadati>
    {
        List<Models.ProdottoGruppiMetadati> items;

        public MockProdottoGruppiMetadatiDataStore()
        {
            items = new List<Models.ProdottoGruppiMetadati>();
            var mockItems = new List<Models.ProdottoGruppiMetadati>
            {
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Features", GruppoMetadatiIt ="Specifiche", IdGruppoMetadato = 1, IdProdotto=1, Ordine=1},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Features", GruppoMetadatiIt ="Specifiche", IdGruppoMetadato = 7, IdProdotto=2, Ordine=1},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Benefits", GruppoMetadatiIt ="Benefits", IdGruppoMetadato = 2, IdProdotto=1, Ordine=2},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Benefits", GruppoMetadatiIt ="Benefits", IdGruppoMetadato = 8, IdProdotto=2, Ordine=2},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Dimensions", GruppoMetadatiIt ="Ingombri", IdGruppoMetadato = 3, IdProdotto=1, Ordine=3},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Dimensions", GruppoMetadatiIt ="Ingombri", IdGruppoMetadato = 9, IdProdotto=2, Ordine=3},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Materials", GruppoMetadatiIt ="Materiali", IdGruppoMetadato = 4, IdProdotto=1, Ordine=4},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Materials", GruppoMetadatiIt ="Materiali", IdGruppoMetadato = 10, IdProdotto=2, Ordine=4},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Diagrams", GruppoMetadatiIt ="Diagrammi", IdGruppoMetadato = 5, IdProdotto=1, Ordine=5},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Diagrams", GruppoMetadatiIt ="Diagrammi", IdGruppoMetadato = 11, IdProdotto=2, Ordine=5},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Specifications", GruppoMetadatiIt ="Specifiche", IdGruppoMetadato = 6, IdProdotto=1, Ordine=6},
                new Models.ProdottoGruppiMetadati(){ GruppoMetadatiEn="Specifications", GruppoMetadatiIt ="Specifiche", IdGruppoMetadato = 12, IdProdotto=2, Ordine=6},
            };
            foreach (var item in mockItems)
            {
                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.ProdottoGruppiMetadati item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.ProdottoGruppiMetadati item)
        {
            var _item = items.Where((Models.ProdottoGruppiMetadati arg) => arg.IdGruppoMetadato == item.IdGruppoMetadato).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.ProdottoGruppiMetadati arg) => arg.IdGruppoMetadato == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.ProdottoGruppiMetadati> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdGruppoMetadato == id));
        }

        public async Task<IEnumerable<Models.ProdottoGruppiMetadati>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
