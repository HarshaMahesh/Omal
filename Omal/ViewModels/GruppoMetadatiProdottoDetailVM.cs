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
                    return App.CurLang=="IT"?CurGruppoProdottoMetadati.GruppoMetadatiIt : CurGruppoProdottoMetadati.GruppoMetadatiEn;
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
                if (string.IsNullOrWhiteSpace(_ContentHtml))
                {
                    if (_CurGruppoProdottoMetadati != null)
                    {
                        List<string> Elementi = new List<String>();
                        _ContentHtml = string.Empty;
                        // recupero l'elenco dei metadati
                        var elementi =DataStore.ProdottoMetadati.GetItemsAsync().Result.Where(x => x.IdGruppoMetadato == CurGruppoProdottoMetadati.IdGruppoMetadato).OrderBy(x => x.Ordine).ToList();
                        foreach (var elemento in elementi)
                        {
                            string immagine = App.CurLang == "IT" ? elemento.ImmagineMetadatiIt : elemento.ImmagineMetadatiEn;
                            string testo = App.CurLang == "IT" ? elemento.TestoEstesoMetadatiIt : elemento.TestoEstesoMetadatiEn;
                            string curElemento = string.Empty;
                            if (!string.IsNullOrWhiteSpace(immagine)) curElemento = string.Format("<P ALIGN='CENTER'><img src='{0}' /></P>", immagine);
                            if (!string.IsNullOrWhiteSpace(testo)) curElemento += testo;
                            if (!string.IsNullOrWhiteSpace(curElemento)) Elementi.Add(curElemento.Trim());
                        }
                        _ContentHtml = string.Join("<hr />", Elementi);
                        // recupero il prodotto di riferimento (sarà il titolo)
                        var prodotto = DataStore.Prodotti.GetItemAsync(CurGruppoProdottoMetadati.IdProdotto);
                        string titolo = string.Empty;
                        if (App.CurLang == "IT")
                            titolo = prodotto.Result.Nome;
                        else
                            titolo = prodotto.Result.NomeEn;
                        _ContentHtml = string.Format("<P ALIGN='CENTER'>{0}</P>{1}", titolo, _ContentHtml);
                    }
                }
                return _ContentHtml;
            }

        }

        public GruppoMetadatiProdottoDetailVM()
        {
        }
    }
}
