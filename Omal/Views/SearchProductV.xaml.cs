
using Xamarin.Forms;

namespace Omal.Views
{
    public partial class SearchProductV : ContentPage
    {
        ViewModels.SearchProductVM viewModel;

        public SearchProductV()
        {
            InitializeComponent();
            BindingContext = viewModel = new ViewModels.SearchProductVM();
            viewModel.Navigation = Navigation;
            viewModel.CurPage = this;
        }

        public SearchProductV(Models.Prodotto curProdotto) : this()
        {
            viewModel.CurProdotto = curProdotto;
        }
    }
}
