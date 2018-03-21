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
            Utenti = new OmalUtentiDataStore();
            ProdottoGruppiMetadati = new OmalProdottoGruppiMetadatiDataStore();
            ProdottoMetadati = new OmalProdottoMetadatiDataStore();
            Clienti = new OmalClientiDataStore();
            Valvole = new OmalValvoleDataStore();
            Attuatori = new OmalAttuatoriDataStore();
            Ordini = new OmalOrdiniDataStore();
            Carrello = new List<Models.Carrello>();
            Pdf = new OmalPDFDataStore();
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
        public Omal.Services.OmalPDFDataStore Pdf { get; set; }
    }
}
