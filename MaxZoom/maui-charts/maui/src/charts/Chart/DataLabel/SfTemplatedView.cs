using Microsoft.Maui.Controls;
using Syncfusion.Maui.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Syncfusion.Maui.Charts
{
    /// <summary>
    /// Template view class
    /// </summary>
    internal abstract class SfTemplatedView : ContentView
    {
        public static readonly BindableProperty ItemTemplateProperty = BindableProperty.Create(nameof(ItemTemplate), typeof(DataTemplate), typeof(SfTemplatedView), null, BindingMode.Default, null, OnItemTemplateChanged);

        public DataTemplate ItemTemplate
        {
            get { return (DataTemplate)GetValue(ItemTemplateProperty); }
            set { SetValue(ItemTemplateProperty, value); }
        }
        
        public View? ContentView { get; set; }

        public SfTemplatedView()
        {

        }

        void SetContentView()
        {
            if (ItemTemplate != null)
            {
                if (ItemTemplate is DataTemplateSelector selector)
                {
                    DataTemplate selectedTemplate = selector.SelectTemplate(this.BindingContext, this);
                    ContentView = selectedTemplate.CreateContent() as View;
                }
                else
                {
                    ContentView = ItemTemplate.CreateContent() as View;
                }

                if (ContentView != null)
                {
                    //Removed SfView and added ContentView as the view not updated on autoscrolling during panning
                    //Children.Add(ContentView);
                    this.Content = ContentView;
                }
            }
        }


        private static void OnItemTemplateChanged(BindableObject bindable, object oldValue, object newValue)
        {
            if (bindable is SfTemplatedView view)
            {
                if(oldValue is not null || newValue == null)
                {
                    view.Content = null;
                }

                if(newValue != null)
                {
                    view.SetContentView();
                }
            }
        }       
    }
}
