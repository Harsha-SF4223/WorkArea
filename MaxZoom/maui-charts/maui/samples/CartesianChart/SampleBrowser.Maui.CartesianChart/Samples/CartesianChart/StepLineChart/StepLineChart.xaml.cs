﻿using SampleBrowser.Maui.Base;
using Syncfusion.Maui.Charts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SampleBrowser.Maui.CartesianChart.SfCartesianChart
{
	public partial class StepLineChart : SampleView
	{
		public StepLineChart ()
		{
			InitializeComponent ();
		}

        public override void OnAppearing()
        {
            base.OnAppearing();
#if IOS
            if (IsCardView)
            {
                Chart.WidthRequest = 350;
                Chart.HeightRequest = 400;
                Chart.VerticalOptions = LayoutOptions.Start;
            }
#endif

            hyperLinkLayout.IsVisible = !IsCardView;
            if (!IsCardView)
            {
                XAxis.Title = new ChartAxisTitle() { Text = "Year" };
                YAxis.Title = new ChartAxisTitle() { Text = "Electricity Produced" };
            }
        }

        public override void OnDisappearing()
        {
            base.OnDisappearing();
            Chart.Handler?.DisconnectHandler();
        }
    }
}