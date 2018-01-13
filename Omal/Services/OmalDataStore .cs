using System;
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
        public IDataStore<Utente> Utenti { get; set; }
    }
}
