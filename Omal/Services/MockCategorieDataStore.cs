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
                new Models.Categoria(){   Codice="CAT", Descrizione="", DescrizioneEn="", IdCategoria=1, IdPadre=0, Nome="Prima", NomeEn ="First" , Ordine=0, Tipologia="Valvola", VisibileApp=false   },
                new Models.Categoria(){   Codice="CAT", Descrizione="", DescrizioneEn="", IdCategoria=4, IdPadre=0, Nome="Prima ZZ", NomeEn ="First ZZ" , Ordine=0, Tipologia="Valvola", VisibileApp=false   },
                new Models.Categoria(){   Codice="CAT1", Descrizione="", DescrizioneEn="", IdCategoria=2, IdPadre=1, Nome="Seconda", NomeEn ="Second" , Ordine=0, Tipologia="Valvola", VisibileApp=false   },
                new Models.Categoria(){   Codice="CAT2", Descrizione="", DescrizioneEn="", IdCategoria=3, IdPadre=2, Nome="Terza", NomeEn ="Terza" , Ordine=0, Tipologia="Valvola", VisibileApp=false   }
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
