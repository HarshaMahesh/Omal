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
    public class MockAttuatoriDataStore : IDataStore<Models.Attuatore>
    {
        List<Models.Attuatore> items;

        public MockAttuatoriDataStore()
        {
            items = new List<Models.Attuatore>();
            var mockItems = new List<Models.Attuatore>
            {
                new Attuatore(){ Codice_Articolo="CodiceArticolo_01", Giacenza=0, IdCodiceAttuatore=1, IdProdotto=3, immagine_placeholder="https://picsum.photos/50/50?image=74", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 01", Valore_iso="Valore Iso 01", Valore_misura="Valore misura 01" },
new Attuatore(){ Codice_Articolo="CodiceArticolo_01", Giacenza=0, IdCodiceAttuatore=1, IdProdotto=3, immagine_placeholder="https://picsum.photos/50/50?image=75", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 01", Valore_iso="Valore Iso 02", Valore_misura="Valore misura 02" },
new Attuatore(){ Codice_Articolo="CodiceArticolo_01", Giacenza=0, IdCodiceAttuatore=1, IdProdotto=3, immagine_placeholder="https://picsum.photos/50/50?image=76", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 02", Valore_iso="Valore Iso 01", Valore_misura="Valore misura 01" },
new Attuatore(){ Codice_Articolo="CodiceArticolo_02", Giacenza=0, IdCodiceAttuatore=2, IdProdotto=4, immagine_placeholder="https://picsum.photos/50/50?image=77", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 02", Valore_iso="Valore Iso 01", Valore_misura="Valore misura 02" },
new Attuatore(){ Codice_Articolo="CodiceArticolo_02", Giacenza=0, IdCodiceAttuatore=2, IdProdotto=4, immagine_placeholder="https://picsum.photos/50/50?image=78", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 01", Valore_iso="Valore Iso 03", Valore_misura="Valore misura 02" },
new Attuatore(){ Codice_Articolo="CodiceArticolo_03", Giacenza=0, IdCodiceAttuatore=3, IdProdotto=5, immagine_placeholder="https://picsum.photos/50/50?image=79", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 01", Valore_iso="Valore Iso 01", Valore_misura="Valore misura 01" },
new Attuatore(){ Codice_Articolo="CodiceArticolo_03", Giacenza=0, IdCodiceAttuatore=3, IdProdotto=5, immagine_placeholder="https://picsum.photos/50/50?image=80", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 01", Valore_iso="Valore Iso 02", Valore_misura="Valore misura 01" },
new Attuatore(){ Codice_Articolo="CodiceArticolo_03", Giacenza=0, IdCodiceAttuatore=3, IdProdotto=5, immagine_placeholder="https://picsum.photos/50/50?image=81", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 01", Valore_iso="Valore Iso 03", Valore_misura="Valore misura 02" },
new Attuatore(){ Codice_Articolo="CodiceArticolo_03", Giacenza=0, IdCodiceAttuatore=3, IdProdotto=5, immagine_placeholder="https://picsum.photos/50/50?image=82", note_footer="", note_footer_en ="", Ordine=0, Prezzo=0, url3d="", urlDownload="", Valore_coppia="Valore Coppia 01", Valore_iso="Valore Iso 04", Valore_misura="Valore misura 03" },
 
            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.Attuatore item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Attuatore item)
        {
            var _item = items.Where((Models.Attuatore arg) => arg.IdCodiceAttuatore == item.IdCodiceAttuatore).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Attuatore arg) => arg.IdCodiceAttuatore == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Attuatore> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdCodiceAttuatore == id));
        }

        public async Task<IEnumerable<Models.Attuatore>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
