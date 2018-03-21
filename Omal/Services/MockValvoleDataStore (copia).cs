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
                new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_01", codice_kit="", codice_valvola="CociceValvola_01", giacenza=0, idcodicevalvola=1, idprodotto=1, immagine_placeholder="https://picsum.photos/50/50?image=64", note_footer="", note_footer_en="", ordine=0, Prezzo=6, url_3d="http://www.qa.partcommunity.com/cgi-bin/plogger.asp?part=%7B%24CADENAS_DATA%5C23d-libs%5Comal%5Cvalves%5Cautomated%5Cangle_seats_valve%5Cmanual%5Cares_manual_drive_asmtab.prj%7D%2C190%20%7BVTYP%3D3D%7D%2C%7BSTYP%3DASSTAB%7D%2C%7BNB%3DJ4M0G0003%7D%2C%7BLINA%3DJ4M0G0003%7D%2C%7BNN%3DAres%20manual%20drive%7D%2C%7BDATE%3D11.02.2015%2014%3A39%3A45%7D%2C%7BGEOMDATE%3D27.11.2013%2008%3A30%3A43%7D%2C%7BVERSION%3D%7D%2C%7BLINEID%3D10%7D%2C%7BWBVAR%3DMV%2CATEX%7D%2C%7BWBNUMERIC%3D%7D%20%20%7BNB%3DJ4M0G0003%7D%2C%7BLINA%3DJ4M0G0003%7D%2C%7BKT%3DDUMMY%7D%2C%7BER%3DThreaded%20ends%20ISO%20228%2F1%20%28STD%29%7D%2C%7BMV%3D3%7D%2C%7BATEX%3D%7D%2C%7BPPATH%3Dares_manual_drive_asmtpl.prj%7D&firm=OMAL_APP&language=italian&external=1", url_download="", valore_azionamento="Asse libero", valore_dn="DN 15", valore_hmm="", valore_inch="", valore_materiale="A351 CF8M", valore_nmm="", valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){  codice_articolo ="", codice_attuatore="CodiceAttuatore_02", codice_kit="", codice_valvola="CociceValvola_02", giacenza=0, idcodicevalvola=2, idprodotto=1, immagine_placeholder="https://picsum.photos/50/50?image=65", note_footer="", note_footer_en="", ordine=0, Prezzo=1.2, url_3d="", url_download="", valore_azionamento="Asse libero", valore_dn="DN 15", valore_hmm="", valore_inch="", valore_materiale="351 CF8", valore_nmm="", valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_03", codice_kit="", codice_valvola="CociceValvola_03", giacenza=0, idcodicevalvola=3, idprodotto=1, immagine_placeholder="https://picsum.photos/50/50?image=66", note_footer="", note_footer_en="", ordine=0, Prezzo=6.3, url_3d="", url_download="", valore_azionamento="Con leva", valore_dn="DN 15", valore_hmm="", valore_inch="", valore_materiale="351 CF8", valore_nmm="", valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_04", codice_kit="", codice_valvola="CociceValvola_04", giacenza=0, idcodicevalvola=4, idprodotto=1, immagine_placeholder="https://picsum.photos/50/50?image=67", note_footer="", note_footer_en="", ordine=0, Prezzo=4.5, url_3d="", url_download="", valore_azionamento="Con leva", valore_dn="DN 100", valore_hmm="", valore_inch="", valore_materiale="A479 tp.316", valore_nmm="", valore_pesokg="", valore_pnansi="ANSI 150"}, 
new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_05", codice_kit="", codice_valvola="CociceValvola_05", giacenza=0, idcodicevalvola=5, idprodotto=1, immagine_placeholder="https://picsum.photos/50/50?image=68", note_footer="", note_footer_en="", ordine=0, Prezzo=9.2, url_3d="", url_download="", valore_azionamento="Doppio effetto anodizzato", valore_dn="DN 65", valore_hmm="", valore_inch="", valore_materiale="DUPLEX (a richiesta)", valore_nmm="", valore_pesokg="", valore_pnansi="PN 25-40"}, 
new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_01", codice_kit="", codice_valvola="CociceValvola_01", giacenza=0, idcodicevalvola=6, idprodotto=2, immagine_placeholder="https://picsum.photos/50/50?image=69", note_footer="", note_footer_en="", ordine=0, Prezzo=1.2, url_3d="", url_download="", valore_azionamento="Asse libero", valore_dn="DN 15", valore_hmm="", valore_inch="", valore_materiale="A351 CF8M", valore_nmm="", valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_02", codice_kit="", codice_valvola="CociceValvola_02", giacenza=0, idcodicevalvola=7, idprodotto=2, immagine_placeholder="https://picsum.photos/50/50?image=70", note_footer="", note_footer_en="", ordine=0, Prezzo=1.1, url_3d="", url_download="", valore_azionamento="Asse libero", valore_dn="DN 15", valore_hmm="", valore_inch="", valore_materiale="351 CF8", valore_nmm="", valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_03", codice_kit="", codice_valvola="CociceValvola_03", giacenza=0, idcodicevalvola=8, idprodotto=2, immagine_placeholder="https://picsum.photos/50/50?image=71", note_footer="", note_footer_en="", ordine=0, Prezzo=4.4, url_3d="", url_download="", valore_azionamento="Con leva", valore_dn="DN 15", valore_hmm="", valore_inch="", valore_materiale="351 CF8", valore_nmm="", valore_pesokg="", valore_pnansi="PN 16-40"}, 
new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_04", codice_kit="", codice_valvola="CociceValvola_04", giacenza=0, idcodicevalvola=9, idprodotto=2, immagine_placeholder="https://picsum.photos/50/50?image=72", note_footer="", note_footer_en="", ordine=0, Prezzo=5.2, url_3d="", url_download="", valore_azionamento="Con leva", valore_dn="DN 100", valore_hmm="", valore_inch="", valore_materiale="A479 tp.316", valore_nmm="", valore_pesokg="", valore_pnansi="ANSI 150"}, 
new Valvola(){ codice_articolo ="", codice_attuatore="CodiceAttuatore_05", codice_kit="", codice_valvola="CociceValvola_05", giacenza=0, idcodicevalvola=10, idprodotto=2, immagine_placeholder="https://picsum.photos/50/50?image=73", note_footer="", note_footer_en="", ordine=0, Prezzo=1.1, url_3d="", url_download="", valore_azionamento="Doppio effetto anodizzato", valore_dn="DN 65", valore_hmm="", valore_inch="", valore_materiale="DUPLEX (a richiesta)", valore_nmm="", valore_pesokg="", valore_pnansi="PN 25-40"}, 
            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<Models.ResponseBase> AddItemAsync(Models.Valvola item)
        {
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<Models.ResponseBase> UpdateItemAsync(Models.Valvola item)
        {
            var _item = items.Where((Models.Valvola arg) => arg.idcodicevalvola == item.idcodicevalvola).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(new Models.ResponseBase());
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Valvola arg) => arg.idcodicevalvola == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Valvola> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.idcodicevalvola == id));
        }

        public async Task<IEnumerable<Models.Valvola>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }

        public Task<IEnumerable<Valvola>> GetLastItemsUpdatesAsync()
        {
            throw new NotImplementedException();
        }
    }
}
