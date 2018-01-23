using System;
using System.Collections.Generic;

namespace Omal.Services
{
    public interface IOmalDataStore
    {
        Omal.IDataStore<Models.Prodotto> Prodotti  { get;  set; }
        Omal.IDataStore<Models.Categoria> Categorie { get; set; }
        Omal.IDataStore<Models.ProdottoGruppiMetadati> ProdottoGruppiMetadati { get; set; }
        Omal.IDataStore<Models.ProdottoMetadati> ProdottoMetadati { get; set; }
        Omal.IDataStore<Models.Cliente> Clienti { get; set; }
        Omal.IDataStore<Models.Ordine> Ordini { get; set; }
        Omal.IDataStore<Models.Valvola> Valvole { get; set; }
        Omal.IDataStore<Models.Attuatore> Attuatori { get; set; }
        Omal.IUtentiDataStore Utenti { get; set; }
        List<Models.Carrello>  Carrello { get; set; }

    }
}
