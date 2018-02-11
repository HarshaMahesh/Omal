using System;
using System.Collections.Generic;
using System.Linq;

namespace Omal.ViewModels
{
    public class GruppoMetadatiProdottoDetailVM:BaseVM
    {
        public string CurTitle
        {
            get { 
                if (CurGruppoProdottoMetadati == null)
                    return "Dettaglio";
                else
                {
                    return App.CurLang=="IT"?CurGruppoProdottoMetadati.gruppo_metadati_it : CurGruppoProdottoMetadati.gruppo_metadati_en;
                }
            }        
        }

        Models.ProdottoGruppiMetadati _CurGruppoProdottoMetadati = null;
        public Models.ProdottoGruppiMetadati CurGruppoProdottoMetadati
        {
            set
            {
                if (_CurGruppoProdottoMetadati != value)
                {
                    _CurGruppoProdottoMetadati = value;
                    LoadContent();
                    OnPropertyChanged();
                }
            }
            get
            {
                return _CurGruppoProdottoMetadati;
            }
        }

        string _ContentHtml = "";
        public string ContentHtml
        {
            get
            {
                return _ContentHtml;
                    
            }
            set
            {
                _ContentHtml = value;
                OnPropertyChanged();
            }
        }
        bool loadContentHtml = false;

        async void LoadContent()
        {
            if (_CurGruppoProdottoMetadati == null) return;
            if (!loadContentHtml)
            {
                loadContentHtml = true;
                var elementi = await DataStore.ProdottoMetadati.GetItemsAsync();
                elementi = elementi.Where(x => x.idgruppometadato == CurGruppoProdottoMetadati.idgruppometadato).OrderBy(x => x.ordine).ToList();
                List<string> Elementi = new List<String>();

                // recupero l'elenco dei metadati
                foreach (var elemento in elementi)
                {
                    string immagine = App.CurLang == "IT" ? elemento.immagine_metadati_it : elemento.immagine_metadati_en;
                    string testo = App.CurLang == "IT" ? elemento.testo_esteso_metadati_it : elemento.testo_esteso_metadati_en;
                    string curElemento = string.Empty;
                    if (!string.IsNullOrWhiteSpace(immagine)) curElemento = string.Format("<P ALIGN='CENTER'><img src='{0}' /></P>", immagine);
                    if (!string.IsNullOrWhiteSpace(testo)) curElemento += testo;
                    if (!string.IsNullOrWhiteSpace(curElemento)) Elementi.Add(curElemento.Trim());
                }
                _ContentHtml = string.Join("<hr />", Elementi);
                // recupero il prodotto di riferimento (sarà il titolo)
                var prodotto = await DataStore.Prodotti.GetItemAsync(CurGruppoProdottoMetadati.idprodotto);
                string titolo = string.Empty;
                if (App.CurLang == "IT")
                    titolo = prodotto.nome;
                else
                    titolo = prodotto.nome_en;
                _ContentHtml = string.Format("<P ALIGN='CENTER'>{0}</P>{1}", titolo, _ContentHtml);
                ContentHtml = _ContentHtml;
                loadContentHtml = false;
            }
        }

        public GruppoMetadatiProdottoDetailVM()
        {
        }
    }
}
