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
                new Models.Prodotto(){  IdProdotto=1, Nome = "MAGNUM Wafer in Acciaio Carbonio - PN 16-40 ANSI 150-300 in acciaio al carbonio", NomeEn= "Inglese MAGNUM Wafer in Acciaio Carbonio - PN 16-40 ANSI 150-300 in acciaio al carbonio", Descrizione="Descrizione MAGNUM Wafer in Acciaio Carbonio - PN 16-40 ANSI 150-300 in acciaio al carbonio",  DescrizioneEn="Inglese Descrizione MAGNUM Wafer in Acciaio Carbonio - PN 16-40 ANSI 150-300 in acciaio al carbonio", Codice = "Codice01", IdCategoria=7, Ordine = 1, Tipologia="valvola", ImmaginePlaceHolder = "https://picsum.photos/50/50?image=0" , VisibileApp = true },
new Models.Prodotto(){  IdProdotto=2, Nome = "MAGNUM Wafer in Acciaio Inox - PN 16-40 ANSI 150-300 in acciaio inox", NomeEn= "Inglese MAGNUM Wafer in Acciaio Inox - PN 16-40 ANSI 150-300 in acciaio inox", Descrizione="Descrizione MAGNUM Wafer in Acciaio Inox - PN 16-40 ANSI 150-300 in acciaio inox",  DescrizioneEn="Inglese Descrizione MAGNUM Wafer in Acciaio Inox - PN 16-40 ANSI 150-300 in acciaio inox", Codice = "Codice02", IdCategoria=7, Ordine = 2, Tipologia= "valvola", ImmaginePlaceHolder = "https://picsum.photos/50/50?image=3" , VisibileApp = true },
new Models.Prodotto(){  IdProdotto=3, Nome = "accessorio bbp - bobina per iso solenoide valvola", NomeEn= "bbp - coils for iso solenoid valv", Descrizione="descrizione accessorio bbp - bobina per iso solenoide valvola",  DescrizioneEn="Inglese descrizione accessorio bbp - bobina per iso solenoide valvola", Codice = "Codice03", IdCategoria=13, Ordine = 1, Tipologia="attuatore", ImmaginePlaceHolder = "https://picsum.photos/50/50?image=5" , VisibileApp = true },
new Models.Prodotto(){  IdProdotto=4, Nome = "accessorio mc30 - bobina mc30 plug and socket ita", NomeEn= "mc30 - coil mc30 plug and socket", Descrizione="descrizione accessorio mc30 - bobina mc30 plug and socket ita",  DescrizioneEn="Inglese descrizione accessorio mc30 - bobina mc30 plug and socket ita", Codice = "Codice04", IdCategoria=13, Ordine = 2, Tipologia="attuatore", ImmaginePlaceHolder = "https://picsum.photos/50/50?image=8" , VisibileApp = true },
new Models.Prodotto(){  IdProdotto=5, Nome = "accessorio mc30 - cnomo bobina mc30 plug and socket ita", NomeEn= "mc30 - cnomo coil mc30 plug and socket", Descrizione="descrizione accessorio mc30 - cnomo bobina mc30 plug and socket ita",  DescrizioneEn="Inglese descrizione accessorio mc30 - cnomo bobina mc30 plug and socket ita", Codice = "Codice05", IdCategoria=13, Ordine = 3, Tipologia="attuatore", ImmaginePlaceHolder = "https://picsum.photos/50/50?image=10" , VisibileApp = true },
new Models.Prodotto(){  IdProdotto=6, Nome = "dac - attuatore pneumatico doppio effetto da in a105", 
                    NomeEn= "dac - double acting pneumatic actuator da a105", Descrizione="descrizione dac - attuatore pneumatico doppio effetto da in a105",  DescrizioneEn="Inglese descrizione dac - attuatore pneumatico doppio effetto da in a105", Codice = "Codice06", IdCategoria=12, Ordine = 10, Tipologia="attuatore", ImmaginePlaceHolder = "https://picsum.photos/50/50?image=15" , VisibileApp = true },
new Models.Prodotto(){  IdProdotto=7, Nome = "da - attuatore pneumatico doppio effetto da inox cf8m microfuso", NomeEn= "da - double acting pneumatic actuator da type inox precision casting", Descrizione="descrizioneda - attuatore pneumatico doppio effetto da inox cf8m microfuso",  DescrizioneEn="Inglese descrizioneda - attuatore pneumatico doppio effetto da inox cf8m microfuso", Codice = "Codice07", IdCategoria=12, Ordine = 2, Tipologia="attuatore", ImmaginePlaceHolder = "https://picsum.photos/50/50?image=20" , VisibileApp = true },

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
            var _item = items.Where((Models.Prodotto arg) => arg.IdProdotto == item.IdProdotto).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Prodotto arg) => arg.IdProdotto == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Prodotto> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdProdotto == id));
        }

        public async Task<IEnumerable<Models.Prodotto>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
