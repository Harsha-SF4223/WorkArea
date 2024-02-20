using Microsoft.Maui.Controls.Internals;
using Syncfusion.Maui.Charts;
using System.Collections.ObjectModel;

namespace StackingLine
{
    public partial class MainPage : TabbedPage
    {
        private ViewModel viewmodel;
        public  List<Brush> list { get; set; }
        public List<int> index_list_start { get; set; }
        public List<int> index_list_end {  get; set; }
        public MainPage()
        {
            InitializeComponent();
            viewmodel = new ViewModel();
            BindingContext= viewmodel;
            list = new List<Brush> { 
                Colors.Red,
                Colors.Green,
                Colors.Blue,
                Colors.Yellow,
                Colors.Orange,
                Colors.Orchid,

            };

            index_list_start = new List<int>();
            index_list_end = new List<int>();
            
            index_list_start.Add(1);
            index_list_start.Add(1);


            index_list_end.Add(3);
            index_list_end.Add(4);


          /*  lineserie3.SelectedIndexes = index_list_start;
            lineserie3.SelectionBrush = Colors.Green;*/

           //check_Series_Selection.SelectedIndexes = index_list_start;
          //  check_Series_Selection.SelectionBrush = Colors.Yellow;

            check_100_line_Series_Selection.SelectionBrush = Colors.Yellow;
            check_100_line_Series_Selection.SelectedIndexes = index_list_start;


            /*
                        line_100_series2.SelectionBrush = Colors.Orange;
                        line_100_series2.SelectedIndexes = index_list_start;*/

           check_track_ball.CornerRadius = 50;
           check_track_ball.FontAttributes = FontAttributes.Italic;
           check_track_ball.FontSize = 20;
            check_track_ball.LabelFormat = "before_click";

        }



        private void Button_Clicked(object sender, EventArgs e)
        {
            check_test.Title = "MAc";
            /*line1.ItemsSource = viewmodel.Data6;
            line2.ItemsSource = viewmodel.Data7;
            line3.ItemsSource = viewmodel.Data8;
            line4.ItemsSource = viewmodel.Data9;
            line5.ItemsSource = viewmodel.Data10;*/

           check_track_ball.CornerRadius = 50;
            check_track_ball.LabelFormat = "end";
            check_track_ball.FontSize = 50;
            check_track_ball.FontAttributes = FontAttributes.Italic;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            line1.PaletteBrushes = list;
            line2.PaletteBrushes = list;
            /*line3.PaletteBrushes = list;
            line4.PaletteBrushes = list;*/
            line5.PaletteBrushes = list;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            line1.ShowTrackballLabel = true;
            line2.ShowTrackballLabel = true;
            line3.ShowTrackballLabel = true;
            line4.ShowTrackballLabel = true;
            line5.ShowTrackballLabel = true;
            
        
        
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
            line1.EnableTooltip = true;
            line2.EnableTooltip = true;
           /* line3.EnableTooltip = true;
            line4.EnableTooltip = true;*/
            line5.EnableTooltip = true;
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            line1.ShowDataLabels = true;
            line2.ShowDataLabels = true;
            line3.ShowDataLabels = true;
            line4.ShowDataLabels = true;
            line5.ShowDataLabels = true;

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
            line1.TooltipTemplate = data;
        }
        private void Button_Clicked1(object sender, EventArgs e)
        {
            check1.ItemsSource = viewmodel.Data6;
            check2.ItemsSource = viewmodel.Data7;
            check3.ItemsSource = viewmodel.Data8;
            check4.ItemsSource = viewmodel.Data9;
            check5.ItemsSource = viewmodel.Data10;

        }

        private void Button_Clicked_12(object sender, EventArgs e)
        {
            check1.PaletteBrushes = list;
            check2.PaletteBrushes = list;
            check3.PaletteBrushes = list;
            check4.PaletteBrushes = list;
            check5.PaletteBrushes = list;
        }

        private void Button_Clicked_23(object sender, EventArgs e)
        {
            check1.ShowTrackballLabel = true;
            check2.ShowTrackballLabel = true;
            check3.ShowTrackballLabel = true;
            check4.ShowTrackballLabel = true;
            check5.ShowTrackballLabel = true;
        }

        private void Button_Clicked_34(object sender, EventArgs e)
        {
            check1.ShowMarkers = true;
            check2.ShowMarkers = true;
            check3.ShowMarkers = true;
            check4.ShowMarkers = true;
            check5.ShowMarkers = true;

        }

        private void Button_Clicked_45(object sender, EventArgs e)
        {
            check1.EnableTooltip = true;
            check2.EnableTooltip = true;
            check3.EnableTooltip = true;
            check4.EnableTooltip = true;
            check5.EnableTooltip = true;
        }

        private void Button_Clicked_56(object sender, EventArgs e)
        {
            check1.ShowDataLabels = true;
            check2.ShowDataLabels = true;
            check3.ShowDataLabels = true;
            check4.ShowDataLabels = true;
            check5.ShowDataLabels = true;

        }

        private void Button_Clicked_67(object sender, EventArgs e)
        {
            check_100_line_Series_Selection.SelectedIndexes = index_list_end;
            check_100_line_Series_Selection.SelectionBrush = Colors.Purple;



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
            line1.TooltipTemplate = data;


        }
    }
    public class Model
    {
        public String? HeroName { get; set; }
        public double PowerLevel { get; set; }
        public String? PowerLevel1 { get; set; }

    }
    public class ViewModel
    {
        public List<Model> Data1 { get; set; }
        public List<Model> Data2 { get; set; }

        public List<Model> Data3 { get; set; }
        public List<Model> Data4 { get; set; }

        public List<Model> Data5 { get; set; }

        public List<Model> Data6 { get; set; }
        public List<Model> Data7 { get; set; }

        public List<Model> Data8 { get; set; }
        public List<Model> Data9 { get; set; }

        public List<Model> Data10 { get; set; }
        public List<int> colo {get;set ;}
        public List<int> insideSeries { get; set; }


        public ViewModel()
        {
            
            Data1 = new List<Model>();
            Data2 = new List<Model>();
            Data3 = new List<Model>();
            Data4 = new List<Model>();
            Data5 = new List<Model>();
            Data6 = new List<Model>();
            Data7 = new List<Model>();
            Data8 = new List<Model>();
            Data9 = new List<Model>();
            Data10 = new List<Model>();

            insideSeries = new List<int>();
            insideSeries.Add(0);
            insideSeries.Add(3);
            colo = new List<int> ();
            colo.Add(1);
            colo.Add(4);

            Data1.Add(new Model { HeroName = "Luffy", PowerLevel = 150        ,PowerLevel1="testing" });
            Data1.Add(new Model { HeroName = "Naruto", PowerLevel =30         ,PowerLevel1="testing" });
            Data1.Add(new Model { HeroName = "Asta", PowerLevel = 45          ,PowerLevel1="testing" });
            Data1.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 60,PowerLevel1="testing" });
            Data1.Add(new Model { HeroName = "Goku", PowerLevel = 75          ,PowerLevel1="testing" });
            Data1.Add(new Model { HeroName = "Saitama", PowerLevel = 90       ,PowerLevel1="testing" });





            Data2.Add(new Model { HeroName = "Luffy", PowerLevel = 300 });
            Data2.Add(new Model { HeroName = "Naruto", PowerLevel = 60 });
            Data2.Add(new Model { HeroName = "Asta", PowerLevel = 90 });
            Data2.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel =120 });
            Data2.Add(new Model { HeroName = "Goku", PowerLevel = 150 });
            Data2.Add(new Model { HeroName = "Saitama", PowerLevel = 180 });




            Data3.Add(new Model { HeroName = "Luffy", PowerLevel = 50 });
            Data3.Add(new Model { HeroName = "Naruto", PowerLevel = 100 });
            Data3.Add(new Model { HeroName = "Asta", PowerLevel = 150 });
            Data3.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 200 });
            Data3.Add(new Model { HeroName = "Goku", PowerLevel = 250 });
            Data3.Add(new Model { HeroName = "Saitama", PowerLevel = 300 });




            Data4.Add(new Model { HeroName = "Luffy", PowerLevel =180 });
            Data4.Add(new Model { HeroName = "Naruto", PowerLevel = 160 });
            Data4.Add(new Model { HeroName = "Asta", PowerLevel = 240 });
            Data4.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 300 });
            Data4.Add(new Model { HeroName = "Goku", PowerLevel = 360 });
            Data4.Add(new Model { HeroName = "Saitama", PowerLevel = 420 });



            Data5.Add(new Model { HeroName = "Luffy", PowerLevel = 200 });
            Data5.Add(new Model { HeroName = "Naruto", PowerLevel = 250 });
            Data5.Add(new Model { HeroName = "Asta", PowerLevel = 300 });
            Data5.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 400 });
            Data5.Add(new Model { HeroName = "Goku", PowerLevel = 500 });
            Data5.Add(new Model { HeroName = "Saitama", PowerLevel = 600 });


            Data6.Add(new Model { HeroName = "Luffy", PowerLevel = 200 });
            Data6.Add(new Model { HeroName = "Naruto", PowerLevel = 400 });
            Data6.Add(new Model { HeroName = "Asta", PowerLevel = 600 });
            Data6.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 800 });
            Data6.Add(new Model { HeroName = "Goku", PowerLevel = 1000 });
            Data6.Add(new Model { HeroName = "Saitama", PowerLevel = 1200 });





            Data7.Add(new Model { HeroName = "Luffy", PowerLevel = 300 });
            Data7.Add(new Model { HeroName = "Naruto", PowerLevel = 600 });
            Data7.Add(new Model { HeroName = "Asta", PowerLevel = 900 });
            Data7.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 1200 });
            Data7.Add(new Model { HeroName = "Goku", PowerLevel = 1500 });
            Data7.Add(new Model { HeroName = "Saitama", PowerLevel = 1800 });




            Data8.Add(new Model { HeroName = "Luffy", PowerLevel = 400 });
            Data8.Add(new Model { HeroName = "Naruto", PowerLevel = 800 });
            Data8.Add(new Model { HeroName = "Asta", PowerLevel = 1600 });
            Data8.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 2000 });
            Data8.Add(new Model { HeroName = "Goku", PowerLevel = 2400 });
            Data8.Add(new Model { HeroName = "Saitama", PowerLevel = 2800 });




            Data9.Add(new Model { HeroName = "Luffy", PowerLevel = 500 });
            Data9.Add(new Model { HeroName = "Naruto", PowerLevel = 1000 });
            Data9.Add(new Model { HeroName = "Asta", PowerLevel = 1500 });
            Data9.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 2000 });
            Data9.Add(new Model { HeroName = "Goku", PowerLevel = 2500 });
            Data9.Add(new Model { HeroName = "Saitama", PowerLevel = 420 });



            Data10.Add(new Model { HeroName = "Luffy", PowerLevel = 600 });
            Data10.Add(new Model { HeroName = "Naruto", PowerLevel = 1200 });
            Data10.Add(new Model { HeroName = "Asta", PowerLevel = 1800 });
            Data10.Add(new Model { HeroName = "Toji Fushiguro", PowerLevel = 2400 });
            Data10.Add(new Model { HeroName = "Goku", PowerLevel = 3000 });
            Data10.Add(new Model { HeroName = "Saitama", PowerLevel = 3600 });


        }


    }
}
