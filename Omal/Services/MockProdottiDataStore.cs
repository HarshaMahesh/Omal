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
    public class MockProdottiDataStore : IDataStore<Models.Prodotto>
    {
        List<Models.Prodotto> items;

        public MockProdottiDataStore()
        {
            items = new List<Models.Prodotto>();
            var mockItems = new List<Models.Prodotto>
            {
                new Models.Prodotto(){  idprodotto=1, nome = "MAGNUM Wafer in Acciaio Carbonio - PN 16-40 ANSI 150-300 in acciaio al carbonio", nome_en= "Inglese MAGNUM Wafer in Acciaio Carbonio - PN 16-40 ANSI 150-300 in acciaio al carbonio", descrizione="Descrizione MAGNUM Wafer in Acciaio Carbonio - PN 16-40 ANSI 150-300 in acciaio al carbonio",  descrizione_en="Inglese Descrizione MAGNUM Wafer in Acciaio Carbonio - PN 16-40 ANSI 150-300 in acciaio al carbonio", codice = "Codice01", idcategoria=7, ordine = 1, tipologia="valvola", immagine_placeholder = "https://picsum.photos/50/50?image=0" , visibile_app = 1 },
                new Models.Prodotto(){  idprodotto=2, nome = "MAGNUM Wafer in Acciaio Inox - PN 16-40 ANSI 150-300 in acciaio inox", nome_en= "Inglese MAGNUM Wafer in Acciaio Inox - PN 16-40 ANSI 150-300 in acciaio inox", descrizione="Descrizione MAGNUM Wafer in Acciaio Inox - PN 16-40 ANSI 150-300 in acciaio inox",  descrizione_en="Inglese Descrizione MAGNUM Wafer in Acciaio Inox - PN 16-40 ANSI 150-300 in acciaio inox", codice = "Codice02", idcategoria=7, ordine = 2, tipologia= "valvola", immagine_placeholder = "https://picsum.photos/50/50?image=3" , visibile_app = 1 },
                new Models.Prodotto(){  idprodotto=3, nome = "accessorio bbp - bobina per iso solenoide valvola", nome_en= "bbp - coils for iso solenoid valv", descrizione="descrizione accessorio bbp - bobina per iso solenoide valvola",  descrizione_en="Inglese descrizione accessorio bbp - bobina per iso solenoide valvola", codice = "Codice03", idcategoria=13, ordine = 1, tipologia="attuatore", immagine_placeholder = "https://picsum.photos/50/50?image=5" , visibile_app = 1 },
                new Models.Prodotto(){  idprodotto=4, nome = "accessorio mc30 - bobina mc30 plug and socket ita", nome_en= "mc30 - coil mc30 plug and socket", descrizione="descrizione accessorio mc30 - bobina mc30 plug and socket ita",  descrizione_en="Inglese descrizione accessorio mc30 - bobina mc30 plug and socket ita", codice = "Codice04", idcategoria=13, ordine = 2, tipologia="attuatore", immagine_placeholder = "https://picsum.photos/50/50?image=8" , visibile_app = 1 },
                new Models.Prodotto(){  idprodotto=6, nome = "dac - attuatore pneumatico doppio effetto da in a105",  nome_en= "dac - double acting pneumatic actuator da a105", descrizione="descrizione dac - attuatore pneumatico doppio effetto da in a105",  descrizione_en="Inglese descrizione dac - attuatore pneumatico doppio effetto da in a105", codice = "Codice06", idcategoria=12, ordine = 10, tipologia="attuatore", immagine_placeholder = "https://picsum.photos/50/50?image=15" , visibile_app = 1 },
                new Models.Prodotto(){  idprodotto=7, nome = "da - attuatore pneumatico doppio effetto da inox cf8m microfuso", nome_en= "da - double acting pneumatic actuator da type inox precision casting", descrizione="descrizioneda - attuatore pneumatico doppio effetto da inox cf8m microfuso",  descrizione_en="Inglese descrizioneda - attuatore pneumatico doppio effetto da inox cf8m microfuso", codice = "Codice07", idcategoria=12, ordine = 2, tipologia="attuatore", immagine_placeholder = "https://picsum.photos/50/50?image=20" , visibile_app = 1 },

            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.Prodotto item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Prodotto item)
        {
            var _item = items.Where((Models.Prodotto arg) => arg.idprodotto == item.idprodotto).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Prodotto arg) => arg.idprodotto == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Prodotto> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idprodotto == id));
        }

        public async Task<IEnumerable<Models.Prodotto>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
