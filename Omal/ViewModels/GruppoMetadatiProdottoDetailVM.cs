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
                    OnPropertyChanged();
                    OnPropertyChanged("ContentHtml");
                }
            }
            get
            {
                return _CurGruppoProdottoMetadati;
            }
        }

        string _ContentHtml = string.Empty;
        public string ContentHtml
        {
            get
            {
                if (string.IsNullOrWhiteSpace(_ContentHtml) && !loadContentHtml)
                    LoadContent();
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
            if (!loadContentHtml)
            {
                loadContentHtml = true;
                if (_CurGruppoProdottoMetadati != null)
                {
                    List<string> Elementi = new List<String>();
                    string _ContentHtmlTmp = string.Empty;
                    // recupero l'elenco dei metadati
                    var elementi = await DataStore.ProdottoMetadati.GetItemsAsync();
                    elementi = elementi.Where(x => x.idgruppometadato == CurGruppoProdottoMetadati.idgruppometadato).OrderBy(x => x.ordine).ToList();
                    foreach (var elemento in elementi)
                    {
                        string immagine = App.CurLang == "IT" ? elemento.immagine_metadati_it : elemento.immagine_metadati_en;
                        string testo = App.CurLang == "IT" ? elemento.testo_esteso_metadati_it : elemento.testo_esteso_metadati_en;
                        string curElemento = string.Empty;
                        if (!string.IsNullOrWhiteSpace(immagine)) curElemento = string.Format("<P ALIGN='CENTER'><img src='{0}' /></P>", immagine);
                        if (!string.IsNullOrWhiteSpace(testo)) curElemento += testo;
                        if (!string.IsNullOrWhiteSpace(curElemento)) Elementi.Add(curElemento.Trim());
                    }
                    _ContentHtmlTmp = string.Join("<hr />", Elementi);
                    // recupero il prodotto di riferimento (sarà il titolo)
                    var prodotto = await DataStore.Prodotti.GetItemAsync(CurGruppoProdottoMetadati.idprodotto);
                    string titolo = string.Empty;
                    if (App.CurLang == "IT")
                        titolo = prodotto.nome;
                    else
                        titolo = prodotto.nome_en;
                    _ContentHtmlTmp = string.Format("<P ALIGN='CENTER'>{0}</P>{1}", titolo, _ContentHtmlTmp);
                    ContentHtml = _ContentHtmlTmp;
                }
                loadContentHtml = false;
            }
        }

        public GruppoMetadatiProdottoDetailVM()
        {
        }
    }
}
