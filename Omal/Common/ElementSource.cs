using System;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Omal.Common
{
    [ContentProperty("ElementName")]
    public class ElementSource : Xamarin.Forms.Xaml.IMarkupExtension
    {
        public string ElementName { get; set; }

        public object ProvideValue(IServiceProvider serviceProvider)
        {
            var rootProvider = serviceProvider.GetService(typeof(IRootObjectProvider)) as IRootObjectProvider;
            if (rootProvider == null)
                return null;
            var root = rootProvider.RootObject as Element;
            if (root == null)
                return null;
            return root.FindByName<Element>(ElementName);
        }
    }
}
