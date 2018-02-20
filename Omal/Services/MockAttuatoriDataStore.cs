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
                new Attuatore(){ codice_articolo="CodiceArticolo_01", giacenza=0, idcodiceattuatore=1, idprodotto=3, immagine_placeholder="https://picsum.photos/50/50?image=74", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 01", valore_iso="Valore Iso 01", valore_misura="Valore misura 01" },
new Attuatore(){ codice_articolo="CodiceArticolo_01", giacenza=0, idcodiceattuatore=1, idprodotto=3, immagine_placeholder="https://picsum.photos/50/50?image=75", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 01", valore_iso="Valore Iso 02", valore_misura="Valore misura 02" },
new Attuatore(){ codice_articolo="CodiceArticolo_01", giacenza=0, idcodiceattuatore=1, idprodotto=3, immagine_placeholder="https://picsum.photos/50/50?image=76", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 02", valore_iso="Valore Iso 01", valore_misura="Valore misura 01" },
new Attuatore(){ codice_articolo="CodiceArticolo_02", giacenza=0, idcodiceattuatore=2, idprodotto=4, immagine_placeholder="https://picsum.photos/50/50?image=77", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 02", valore_iso="Valore Iso 01", valore_misura="Valore misura 02" },
new Attuatore(){ codice_articolo="CodiceArticolo_02", giacenza=0, idcodiceattuatore=2, idprodotto=4, immagine_placeholder="https://picsum.photos/50/50?image=78", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 01", valore_iso="Valore Iso 03", valore_misura="Valore misura 02" },
new Attuatore(){ codice_articolo="CodiceArticolo_03", giacenza=0, idcodiceattuatore=3, idprodotto=5, immagine_placeholder="https://picsum.photos/50/50?image=79", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 01", valore_iso="Valore Iso 01", valore_misura="Valore misura 01" },
new Attuatore(){ codice_articolo="CodiceArticolo_03", giacenza=0, idcodiceattuatore=3, idprodotto=5, immagine_placeholder="https://picsum.photos/50/50?image=80", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 01", valore_iso="Valore Iso 02", valore_misura="Valore misura 01" },
new Attuatore(){ codice_articolo="CodiceArticolo_03", giacenza=0, idcodiceattuatore=3, idprodotto=5, immagine_placeholder="https://picsum.photos/50/50?image=81", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 01", valore_iso="Valore Iso 03", valore_misura="Valore misura 02" },
new Attuatore(){ codice_articolo="CodiceArticolo_03", giacenza=0, idcodiceattuatore=3, idprodotto=5, immagine_placeholder="https://picsum.photos/50/50?image=82", note_footer="", note_footer_en ="", ordine=0, Prezzo=0, url_3d="", url_download="", valore_coppia="Valore Coppia 01", valore_iso="Valore Iso 04", valore_misura="Valore misura 03" },
 
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
            var _item = items.Where((Models.Attuatore arg) => arg.idcodiceattuatore == item.idcodiceattuatore).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Attuatore arg) => arg.idcodiceattuatore == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Attuatore> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idcodiceattuatore == id));
        }

        public async Task<IEnumerable<Models.Attuatore>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
