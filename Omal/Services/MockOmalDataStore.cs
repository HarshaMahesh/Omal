﻿using System;
using Omal.Models;

namespace Omal.Services
{
    public class MockOmalDataStore: IOmalDataStore
    {
        public MockOmalDataStore()
        {
            Prodotti = new MockProdottiDataStore();
            Categorie = new MockCategorieDataStore();
            Utenti = new MockUtentiDataStore();
            ProdottoGruppiMetadati = new MockProdottoGruppiMetadatiDataStore();
            ProdottoMetadati = new MockProdottoMetadatiDataStore();
            Clienti = new MockClientiDataStore();
            Valvole = new MockValvoleDataStore();
            Attuatori = new MockAttuatoriDataStore();
        }

        public IDataStore<Prodotto> Prodotti{ get;  set; }
        public IDataStore<Categoria> Categorie { get;  set; }
        public IDataStore<ProdottoGruppiMetadati> ProdottoGruppiMetadati { get;  set; }
        public IDataStore<ProdottoMetadati> ProdottoMetadati { get;  set; }
        public IDataStore<Cliente> Clienti { get;  set; }
        public IDataStore<Valvola> Valvole { get; set; }
        public IDataStore<Attuatore> Attuatori { get; set; }
        public IUtentiDataStore Utenti { get; set; }
    }
}
