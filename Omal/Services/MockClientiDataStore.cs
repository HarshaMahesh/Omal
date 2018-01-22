﻿using System;
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
    public class MockClientiDataStore : IDataStore<Models.Cliente>
    {
        List<Models.Cliente> items;

        public MockClientiDataStore()
        {
            items = new List<Models.Cliente>();
            var mockItems = new List<Models.Cliente>
            {
                new Cliente(){ Cap="25087", CFiscale="03793970983", Città="Salò", Email="luca.cavedaghi@cualeva.com", Fax="", IdCliente=1, IdUtenteReferente=0, Indirizzo="P.zza Vittorio Emanuele II", Nazione="Italia", PIva ="03793970983", Provincia="Brescia", RagioneSociale="Cualeva S.r.l", Telefono="+39 0365 189 6342"},
                new Cliente(){ Cap="25087", CFiscale="02608470981", Città="Salò", Email="info@timmagine.com", Fax="+39 0365 1871301", IdCliente=2, IdUtenteReferente=0, Indirizzo="Via Giulio Natta, 10", Nazione="Italia", PIva ="02608470981", Provincia="Brescia", RagioneSociale="TImmagine S.r.l", Telefono="+39 0365 1871300"}
            };

            foreach (var item in mockItems)
            {

                items.Add(item);
            }
        }

        public async Task<bool> AddItemAsync(Models.Cliente item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Models.Cliente item)
        {
            var _item = items.Where((Models.Cliente arg) => arg.IdCliente == item.IdCliente).FirstOrDefault();
            items.Remove(_item);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(int id)
        {
            var _item = items.Where((Models.Cliente arg) => arg.IdCliente == id).FirstOrDefault();
            items.Remove(_item);

            return await Task.FromResult(true);
        }

        public async Task<Models.Cliente> GetItemAsync(int id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.IdCliente == id));
        }

        public async Task<IEnumerable<Models.Cliente>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }


    }
}
