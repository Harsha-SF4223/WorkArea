 
using Syncfusion.Maui.Charts;

namespace StackingAreaMarker
{
    public partial class MainPage : ContentPage
    {
        int count = 0;

        public MainPage()
        {
            InitializeComponent();
        }
        private void Button_Clicked1(object sender, EventArgs e)
        {/*
            ViewModel c = new ViewModel();
            StackingAreaSeries a = new StackingAreaSeries();
            
            a.ItemsSource = c.Data1;
            a.XBindingPath = "Name";
            a.YBindingPath = "Value4";
           

            chart1.Series.Add(a);
            */

            /*serie1.YAxisName = "Y2";
            serie3.YAxisName = "Y2";*/

            //  serie6.YAxisName = "Y2";
            // serie6.YAxisName = "Y3";
            /*    serie2.XAxisName = "X2";
                serie4.XAxisName = "X2";
                serie6.XAxisName = "X2";*/
            /*serie2.IsVisible = false;
            serie4.IsVisible = false;
            serie5.IsVisible = false;
            serie6.IsVisible = false;*/
            /*marker1.Type = ShapeType.Hexagon;
            marker1.Height = 50;
            marker1.Width = 50;
            marker2.Type = ShapeType.Cross;
            marker2.Height = 60;
            marker2.Width = 60;
            marker4.Type = ShapeType.Plus;
            marker4.Height = 70;
            marker4.Width = 70;*/
            ViewModel a=new ViewModel();

            //marker3.Height = 50;
     
        }
        private void Button_Clicked2(object sender, EventArgs e)
        {
            //    chart1.IsTransposed = false;
            /*  ViewModel c = new ViewModel();
              StackingColumnSeries b = new StackingColumnSeries();
              b.ItemsSource = c.Data1;
              b.XBindingPath = "Name";
              b.YBindingPath = "Value5";
              chart1.Series.Add(b);*/
          /*  serie4.IsVisible= false;
            serie2.IsVisible = false;*/


        }
        private void Button_Clicked3(object sender, EventArgs e)
        {
            // chart1.Series.Remove(serie1);


             

        }

    }
    public class Model
    {
        public Model(DateTime name, double value)
        {
            Year = name;
            Value = value;

        }
        public Model()
        { }
        public Model(string name, double value, double value1, double value2, double value3)
        {
            Name = name;
            Value = value;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
        }

        public Model(string name, double value, double value1, double value2, double value3, double value4, double value5)
        {
            Name = name;
            Value = value;
            Value1 = value1;
            Value2 = value2;
            Value3 = value3;
            Value4 = value4;
            Value5 = value5;
        }

        public DateTime Year { get; set; }

        public string Name { get; set; }
        public string Name1 { get; set; }

        public double Value { get; set; }

        public double Value1 { get; set; }

        public double Value2 { get; set; }

        public double Value3 { get; set; }
        public double Value4 { get; set; }
        public double Value5 { get; set; }


    }

    public class ViewModel
    {
        public List<Model> Data1 { get; set; }
        public List<Model> Data2 { get; set; }
        public List<Model> Data3 { get; set; }
        public List<Model> Data4 { get; set; }

        public List<Brush> Data5 { get; set; }

        public ViewModel()
        {
            Data5 = new List<Brush>
            {
            Colors.Red,
            Colors.Green,
            Colors.Yellow,
            Colors.Blue,



            };

            Data1 = new List<Model> 
           /* Data1.Add(new Model("Alex", 4, 10, 19, 35,50,100));
            Data1.Add(new Model("Adam", 4, 7, 10.2, 70,100,200));
            Data1.Add(new Model("Fiona", double.NaN, double.NaN, double.NaN, 105,150,300));
            Data1.Add(new Model("Adam", 4.5, 15, 8, 140,200,400));
            Data1.Add(new Model("Gavin", 4.9, 6, 9, 175,150,350));
            Data1.Add(new Model("Grace", 80, 91, 240, 210,80,300));
            Data1.Add(new Model("Brian", 100, 210, 280, 245,50,200));*/


            /*Data1.Add(new Model("Alex", 10, 11, 40, 35, 50, 100));
            Data1.Add(new Model("Adam", 15, 25, 80, 70, 100, 200));
            Data1.Add(new Model("Fiona", 20, 41, 120, 105, 150, 300));
            Data1.Add(new Model("Adam", 45, 41, 160, 140, 200, 400));
            Data1.Add(new Model("Gavin", 49, 80, 200, 175, 150, 350));
            Data1.Add(new Model("Grace", 80, 91, 240, 210, 80, 300));
            Data1.Add(new Model("Brian", 100, 210, 280, 245, 50, 200));*/
             
{
   new Model { Name = "Alex", Value1 = 29, Value2 = 11, Value3 = 40, Value4 = 35, Value5 = 50,  },
    new Model { Name = "Adam", Value1 = 15, Value2 = 25, Value3 = 80, Value4 = 70, Value5 = 100,   },
    new Model { Name = "Fiona", Value1 = 20, Value2 = 41, Value3 = 120, Value4 = 105, Value5 = 150  },
    new Model { Name = "Adam", Value1 = 45, Value2 = 41, Value3 = 160, Value4 = 140, Value5 = 200  },
    new Model { Name = "Gavin", Value1 = 49, Value2 = 80, Value3 = 200, Value4 = 175, Value5 = 150 },
    new Model { Name = "Grace", Value1 = 49, Value2 = 80 , Value3 = 240, Value4 = 210, Value5 = 80 },
    new Model { Name = "Brian",Value1 = 69,  Value2 = 60,  Value3 = 280, Value4 = 245, Value5 = 50 }
};

        }
    }
}
