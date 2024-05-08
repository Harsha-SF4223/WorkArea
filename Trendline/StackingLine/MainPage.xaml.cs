using Microsoft.Maui.Controls.Internals;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;
using StackingLine;
using Microsoft.Maui.Controls;
using System.Diagnostics.Metrics;

namespace StackingLine
{
    public partial class MainPage : TabbedPage
    {
        private StackedLineViewModel viewmodel;
        public List<Brush> list { get; set; }
        public List<int> index_list_start { get; set; }
        public List<int> index_list_end { get; set; }
        public MainPage()
        {
            InitializeComponent();
          /*  trend.TrendLines = new ChartTrendLineCollection()
            {
                new TrendLine(),
            };*/
            

            /*  lineserie3.SelectedIndexes = index_list_start;
              lineserie3.SelectionBrush = Colors.Green;*/

            //check_Series_Selection.SelectedIndexes = index_list_start;
            //  check_Series_Selection.SelectionBrush = Colors.Yellow;

            /*check_100_line_Series_Selection.SelectionBrush = Colors.Yellow;
            check_100_line_Series_Selection.SelectedIndexes = index_list_start;
*/

            /*
                        line_100_series2.SelectionBrush = Colors.Orange;
                        line_100_series2.SelectedIndexes = index_list_start;*/

            /* check_track_ball.CornerRadius = 50;
             check_track_ball.FontAttributes = FontAttributes.Italic;
             check_track_ball.FontSize = 20;
              check_track_ball.LabelFormat = "before_click";
  */
        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            
           // check_test.Title = "MAc";
            /*line1.ItemsSource = viewmodel.Data6;
            line2.ItemsSource = viewmodel.Data7;
            line3.ItemsSource = viewmodel.Data8;
            line4.ItemsSource = viewmodel.Data9;
            line5.ItemsSource = viewmodel.Data10;*/

            /*  check_track_ball.CornerRadius = 50;
               check_track_ball.LabelFormat = "end";
               check_track_ball.FontSize = 50;
               check_track_ball.FontAttributes = FontAttributes.Italic;
       */
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            /*line1.PaletteBrushes = list;
            line2.PaletteBrushes = list;
            *//*line3.PaletteBrushes = list;
            line4.PaletteBrushes = list;*//*
            line5.PaletteBrushes = list;*/
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
          /*  line1.ShowTrackballLabel = true;
            line2.ShowTrackballLabel = true;
            line3.ShowTrackballLabel = true;
            line4.ShowTrackballLabel = true;
            line5.ShowTrackballLabel = true;*/



        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            /*   line1.ShowMarkers = true;
               line2.ShowMarkers = true;
              *//* line3.ShowMarkers = true;
               line4.ShowMarkers = true;*//*
               line5.ShowMarkers = true;*/

        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
         /*   line1.EnableTooltip = true;
            line2.EnableTooltip = true;
            *//* line3.EnableTooltip = true;
             line4.EnableTooltip = true;*//*
            line5.EnableTooltip = true;*/
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
         /*   line1.ShowDataLabels = true;
            line2.ShowDataLabels = true;
            line3.ShowDataLabels = true;
            line4.ShowDataLabels = true;
            line5.ShowDataLabels = true;*/

        }

        private void Button_Clicked_6(object sender, EventArgs e)
        {
            //check_Series_Selection.SelectedIndexes = index_list_end;
            //          check_Series_Selection.SelectionBrush = Colors.Purple;
        }

        private void Button_Clicked_7(object sender, EventArgs e)
        {
            /* lineserie1.SelectedIndex = 0;
             lineserie1.SelectionBrush=Colors.Red;

             lineserie3.SelectionBrush = Colors.Yellow;
             lineserie3.SelectedIndexes = index_list_end;*/

            var data = new DataTemplate(() =>
            {
                Label value = new Label();
                value.Text = "check mac";
                value.FontSize = 20;
                return value;
            });
            //line1.TooltipTemplate = data;
        }
        private void Button_Clicked1(object sender, EventArgs e)
        {
            /* check1.ItemsSource = viewmodel.Data6;
             check2.ItemsSource = viewmodel.Data7;
             check3.ItemsSource = viewmodel.Data8;
             check4.ItemsSource = viewmodel.Data9;
             check5.ItemsSource = viewmodel.Data10;
 */
        }

        private void Button_Clicked_12(object sender, EventArgs e)
        {/*
            check1.PaletteBrushes = list;
            check2.PaletteBrushes = list;
            check3.PaletteBrushes = list;
            check4.PaletteBrushes = list;
            check5.PaletteBrushes = list;*/
        }

        private void Button_Clicked_23(object sender, EventArgs e)
        {
            /*check1.ShowTrackballLabel = true;
            check2.ShowTrackballLabel = true;
            check3.ShowTrackballLabel = true;
            check4.ShowTrackballLabel = true;
            check5.ShowTrackballLabel = true;
     */
        }

        private void Button_Clicked_34(object sender, EventArgs e)
        {
            /*  check1.ShowMarkers = true;
              check2.ShowMarkers = true;
              check3.ShowMarkers = true;
              check4.ShowMarkers = true;
              check5.ShowMarkers = true;
  */
        }

        private void Button_Clicked_45(object sender, EventArgs e)
        {
            /*  check1.EnableTooltip = true;
              check2.EnableTooltip = true;
              check3.EnableTooltip = true;
              check4.EnableTooltip = true;
              check5.EnableTooltip = true;
        */
        }

        private void Button_Clicked_56(object sender, EventArgs e)
        {
            /*  check1.ShowDataLabels = true;
              check2.ShowDataLabels = true;
              check3.ShowDataLabels = true;
              check4.ShowDataLabels = true;
              check5.ShowDataLabels = true;
  */
        }

        private void Button_Clicked_67(object sender, EventArgs e)
        {
            /*  check_100_line_Series_Selection.SelectedIndexes = index_list_end;
              check_100_line_Series_Selection.SelectionBrush = Colors.Purple;

  */

        }

        private void Button_Clicked_78(object sender, EventArgs e)
        {

            /*            line_100_series1.SelectedIndex = 4;
                        line_100_series1.SelectionBrush = Colors.Black;


                        line_100_series2.SelectionBrush = Colors.Blue;
                        line_100_series2.SelectedIndexes = index_list_end;*/

            var data = new DataTemplate(() =>
            {
                Label value = new Label();
                value.Text = "check mac";
                value.FontSize = 20;
                return value;
            });
          //  line1.TooltipTemplate = data;


        }

        private void Button_Clicked_8(object sender, EventArgs e)
        {

          /*  trend1.Stroke = Brush.Orange;
            trend2.Stroke = Brush.Green;
            trend1.StrokeWidth = 20;
            trend2.StrokeWidth = 10;*/
            
            
        }

        private void rangeSlider_ValueChanging(object sender, Syncfusion.Maui.Sliders.SliderValueChangingEventArgs e)
        {
            // Round the value to the nearest whole number
            int roundedValue = (int)Math.Round(e.NewValue);

            // Set the rounded value back to the slider
            //rangeSlider.Value = roundedValue;

        }

        private void rangeSlider_ValueChanged(object sender, Syncfusion.Maui.Sliders.SliderValueChangedEventArgs e)
        {
            int roundedValue = (int)Math.Round(e.NewValue);

            // Set the rounded value back to the slider
            //rangeSlider.Value = roundedValue;
        }
    }
    public class Model
    {
        public String? HeroName { get; set; }
        public double Value { get; set; }
        public double PowerLevel { get; set; }

        public String? PowerLevel1 { get; set; }
        public DateTime DateText { get; set; }
        public string? Month { get; set; }
        public double Father { get; set; }
        public double Mother { get; set; }

        public double Son { get; set; }

        public double Daughter { get; set; }
        public Model()
        {

        }
        public Model(double father, string month, double mother, double son, double daughter)
        {
            Father = father;
            Mother = mother;
            Month = month;
            Son = son;
            Daughter = daughter;
        }
    }
    
}
