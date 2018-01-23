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
    public class MockValvoleDataStore : IDataStore<Models.Valvola>
    {
        List<Models.Valvola> items;

        public MockValvoleDataStore()
        {
            items = new List<Models.Valvola>();
            var mockItems = new List<Models.Valvola>
            {
                new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_01", Codice_Kit="", Codice_Valvola="CociceValvola_01", Giacenza=0, IdCodiceValvola=1, IdProdotto=1, immagine_placeholder="https://picsum.photos/50/50?image=64", note_footer="", note_footer_en="", Ordine=0, Prezzo=6, url3d="", urlDownload="", valore_azionamento="Asse libero", valore_dn="DN 15", Valore_hmm="", valore_inch="", valore_materiale="A351 CF8M", Valore_nmm="", Valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_02", Codice_Kit="", Codice_Valvola="CociceValvola_02", Giacenza=0, IdCodiceValvola=2, IdProdotto=1, immagine_placeholder="https://picsum.photos/50/50?image=65", note_footer="", note_footer_en="", Ordine=0, Prezzo=1.2, url3d="", urlDownload="", valore_azionamento="Asse libero", valore_dn="DN 15", Valore_hmm="", valore_inch="", valore_materiale="351 CF8", Valore_nmm="", Valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_03", Codice_Kit="", Codice_Valvola="CociceValvola_03", Giacenza=0, IdCodiceValvola=3, IdProdotto=1, immagine_placeholder="https://picsum.photos/50/50?image=66", note_footer="", note_footer_en="", Ordine=0, Prezzo=6.3, url3d="", urlDownload="", valore_azionamento="Con leva", valore_dn="DN 15", Valore_hmm="", valore_inch="", valore_materiale="351 CF8", Valore_nmm="", Valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_04", Codice_Kit="", Codice_Valvola="CociceValvola_04", Giacenza=0, IdCodiceValvola=4, IdProdotto=1, immagine_placeholder="https://picsum.photos/50/50?image=67", note_footer="", note_footer_en="", Ordine=0, Prezzo=4.5, url3d="", urlDownload="", valore_azionamento="Con leva", valore_dn="DN 100", Valore_hmm="", valore_inch="", valore_materiale="A479 tp.316", Valore_nmm="", Valore_pesokg="", valore_pnansi="ANSI 150"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_05", Codice_Kit="", Codice_Valvola="CociceValvola_05", Giacenza=0, IdCodiceValvola=5, IdProdotto=1, immagine_placeholder="https://picsum.photos/50/50?image=68", note_footer="", note_footer_en="", Ordine=0, Prezzo=9.2, url3d="", urlDownload="", valore_azionamento="Doppio effetto anodizzato", valore_dn="DN 65", Valore_hmm="", valore_inch="", valore_materiale="DUPLEX (a richiesta)", Valore_nmm="", Valore_pesokg="", valore_pnansi="PN 25-40"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_01", Codice_Kit="", Codice_Valvola="CociceValvola_01", Giacenza=0, IdCodiceValvola=6, IdProdotto=2, immagine_placeholder="https://picsum.photos/50/50?image=69", note_footer="", note_footer_en="", Ordine=0, Prezzo=1.2, url3d="", urlDownload="", valore_azionamento="Asse libero", valore_dn="DN 15", Valore_hmm="", valore_inch="", valore_materiale="A351 CF8M", Valore_nmm="", Valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_02", Codice_Kit="", Codice_Valvola="CociceValvola_02", Giacenza=0, IdCodiceValvola=7, IdProdotto=2, immagine_placeholder="https://picsum.photos/50/50?image=70", note_footer="", note_footer_en="", Ordine=0, Prezzo=1.1, url3d="", urlDownload="", valore_azionamento="Asse libero", valore_dn="DN 15", Valore_hmm="", valore_inch="", valore_materiale="351 CF8", Valore_nmm="", Valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_03", Codice_Kit="", Codice_Valvola="CociceValvola_03", Giacenza=0, IdCodiceValvola=8, IdProdotto=2, immagine_placeholder="https://picsum.photos/50/50?image=71", note_footer="", note_footer_en="", Ordine=0, Prezzo=4.4, url3d="", urlDownload="", valore_azionamento="Con leva", valore_dn="DN 15", Valore_hmm="", valore_inch="", valore_materiale="351 CF8", Valore_nmm="", Valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_04", Codice_Kit="", Codice_Valvola="CociceValvola_04", Giacenza=0, IdCodiceValvola=9, IdProdotto=2, immagine_placeholder="https://picsum.photos/50/50?image=72", note_footer="", note_footer_en="", Ordine=0, Prezzo=5.2, url3d="", urlDownload="", valore_azionamento="Con leva", valore_dn="DN 100", Valore_hmm="", valore_inch="", valore_materiale="A479 tp.316", Valore_nmm="", Valore_pesokg="", valore_pnansi="ANSI 150"}, 
new Valvola(){ Codice_Articolo ="", Codice_Attuatore="CodiceAttuatore_05", Codice_Kit="", Codice_Valvola="CociceValvola_05", Giacenza=0, IdCodiceValvola=10, IdProdotto=2, immagine_placeholder="https://picsum.photos/50/50?image=73", note_footer="", note_footer_en="", Ordine=0, Prezzo=1.1, url3d="", urlDownload="", valore_azionamento="Doppio effetto anodizzato", valore_dn="DN 65", Valore_hmm="", valore_inch="", valore_materiale="DUPLEX (a richiesta)", Valore_nmm="", Valore_pesokg="", valore_pnansi="PN 25-40"}, 
            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.Valvola item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Valvola item)
        {
            var _item = items.Where((Models.Valvola arg) => arg.IdCodiceValvola == item.IdCodiceValvola).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Valvola arg) => arg.IdCodiceValvola == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Valvola> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdCodiceValvola == id));
        }

        public async Task<IEnumerable<Models.Valvola>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
