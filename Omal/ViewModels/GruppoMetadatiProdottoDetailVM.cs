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
                    return App.CurLang=="IT"?CurGruppoProdottoMetadati.gruppo_metadati_it.ToUpper() : CurGruppoProdottoMetadati.gruppo_metadati_en.ToUpper();
                }
            }        
        }

        Models.Prodotto _CurProdotto = null;
        public Models.Prodotto CurProdotto
        {
            get
            {
                return _CurProdotto;
            }
            set
            {
                if (_CurProdotto != value)
                {
                    _CurProdotto = value;
                    OnPropertyChanged();
                    OnPropertyChanged("GruppoMetadati");
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
                    if (_CurGruppoProdottoMetadati != null)
                    {}
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
                elementi = elementi.Where(x => x.idgruppometadato == CurGruppoProdottoMetadati.idgruppometadato && x.idProdotto == CurGruppoProdottoMetadati.idprodotto).OrderBy(x => x.ordine).ToList();
                List<string> Elementi = new List<String>();

                // recupero l'elenco dei metadati
                foreach (var elemento in elementi)
                {
                    string immagine = App.CurLang == "IT" ? elemento.immagine_metadati_it : elemento.immagine_metadati_en;
                    string testo = App.CurLang == "IT" ? elemento.testo_esteso_metadati_it : elemento.testo_esteso_metadati_en;
                    string curElemento = string.Empty;
                    if (!string.IsNullOrWhiteSpace(immagine)) curElemento = string.Format("<P ALIGN='CENTER'><a href='{1}'><img Height='100' src='{0}' /></a></P>", immagine, string.Format("local_{0}",immagine));
                    if (!string.IsNullOrWhiteSpace(testo)) curElemento += testo;
                    if (!string.IsNullOrWhiteSpace(curElemento)) Elementi.Add(curElemento.Trim());
                }
                _ContentHtml = string.Join("</ br>", Elementi);
                // recupero il prodotto di riferimento (sarà il titolo)
                CurProdotto = await DataStore.Prodotti.GetItemAsync(CurGruppoProdottoMetadati.idprodotto);
                string titolo = string.Empty;
              /*  if (App.CurLang == "IT")
                    titolo = prodotto.nome;
                else
                    titolo = prodotto.nome_en;*/


            //    _ContentHtml = string.Format(@"<p style=""color:#004899;background-color:#EAEAEA"" align='center'><b>{0}</b></p>{1}", titolo ,_ContentHtml);
                var baseStr = @"<html><head><style type=""text/css"">" +
                @" @font-face {
                    font-family: Montserrat-Regular;"
                    + @"src: url(""file:///android_asset/fonts/Montserrat-Regular.ttf"")" +
           @"}
table {width: 100% ! important; margin: 0 auto !important;}
table td {border: 1px solid #ccc; padding: 5px; }
table tr:first-child td {color: #174288; font-weight: bold; text-align: center;}
table tr:nth-child(odd) td{
           background:#EAEAEA;
}
table tr:nth-child(even) td{
            background:#FFFFFF;
}
.metadato-testo strong {color: #174288;}
.metadato-titoloimmagine {color: #174288; font-weight: bold; text-align: center;}
.metadato-testo table {width: 100% ! important; margin: 0 auto !important;}
.metadato-testo table td {border: 1px solid #ccc;}
.metadato-testo table tr:first-child td {color: #174288; font-weight: bold; text-align: center;}
.metadato-testo table .fr-highlighted {color: #174288; font-weight: bold;}
table .fr-highlighted {color: #174288; font-weight: bold;}
        body {
                font-family: Montserrat-Regular;
                text - align: justify;
            }
    </style>
</head>
<body>" + _ContentHtml +
   @"</body>
   </html>";
                ContentHtml = baseStr;
                loadContentHtml = false;
            }
        }

        public GruppoMetadatiProdottoDetailVM()
        {
        }
    }
}
