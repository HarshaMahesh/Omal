using System;
using Android.Content;
using Android.Views;
using Omal.Droid.CustomControls;
using Xamarin.Forms;


[assembly: ExportRenderer(typeof(Xamarin.Forms.ViewCell), typeof(CustomViewCellRenderer))]
namespace Omal.Droid.CustomControls
{
    public class CustomViewCellRenderer : Xamarin.Forms.Platform.Android.ViewCellRenderer
    {
        private Android.Views.View _cellCore;
        private bool _selected;
        protected override Android.Views.View GetCellCore(Cell item,
                                                      Android.Views.View convertView,
                                                      ViewGroup parent,
                                                      Context context)
        {
            _cellCore = base.GetCellCore(item, convertView, parent, context);
            _selected = false;
            return _cellCore;
        }

        protected override void OnCellPropertyChanged(object sender, System.ComponentModel.PropertyChangedEventArgs args)
        {
            base.OnCellPropertyChanged(sender, args);
            if (args.PropertyName == "IsSelected")
            {
                _selected = !_selected;
                var extendedViewCell = sender as ViewCell;
                if (_selected)
                {
                    _cellCore.SetBackgroundColor(Android.Graphics.Color.WhiteSmoke);
                }
                else
                {
                    _cellCore.SetBackgroundColor(Android.Graphics.Color.Transparent);
                }
            }
        }
    }
}
