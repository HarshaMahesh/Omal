using System;
using System.Collections.Generic;
using Omal.Models;

namespace Omal.Services
{
    public class OmalDataStore: IOmalDataStore
    {
        public OmalDataStore()
        {
            
        }

        public IDataStore<Prodotto> Prodotti{ get;  set; }
        public IDataStore<Categoria> Categorie { get;  set; }
        public IDataStore<ProdottoGruppiMetadati> ProdottoGruppiMetadati { get;  set; }
        public IDataStore<ProdottoMetadati> ProdottoMetadati { get;  set; }
        public IDataStore<Cliente> Clienti { get;  set; }
        public IUtentiDataStore Utenti { get; set; }
        public IDataStore<Valvola> Valvole { get; set; }
        public IDataStore<Attuatore> Attuatori { get; set; }
        public IDataStore<Ordine> Ordini { get; set; }
        public List<Carrello> Carrello { get; set; }
    }
}
