using System;
using Android.Content;
using Android.Views;
using Omal.Droid.CustomControls;
using Xamarin.Forms;


[assembly: ExportRenderer(typeof(Xamarin.Forms.TextCell), typeof(TextViewCellRenderer))]
namespace Omal.Droid.CustomControls
{
    public class TextViewCellRenderer : Xamarin.Forms.Platform.Android.TextCellRenderer
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
                var extendedViewCell = sender as TextCell;
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
