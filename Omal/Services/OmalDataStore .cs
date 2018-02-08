using System;
using System.Collections.Generic;
using Omal.Models;

namespace Omal.Services
{
    public class OmalDataStore: IOmalDataStore
    {
        public OmalDataStore()
        {
            Prodotti = new OmalProdottiDataStore();
            Categorie = new OmalCategorieDataStore();
            Utenti = new MockUtentiDataStore();
            ProdottoGruppiMetadati = new OmalProdottoGruppiMetadatiDataStore();
            ProdottoMetadati = new OmalProdottoMetadatiDataStore();
            Clienti = new MockClientiDataStore();
            Valvole = new OmalValvoleDataStore();
            Attuatori = new MockAttuatoriDataStore();
            Ordini = new MockOrdiniDataStore();
            Carrello = new List<Models.Carrello>();
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
