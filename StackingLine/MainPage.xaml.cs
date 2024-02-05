
using System.Collections.ObjectModel;

namespace StackingLine
{
    public partial class MainPage : ContentPage
    {
        private ViewModel viewmodel;
        public bool check { get; set; }
        public MainPage()
        {
            InitializeComponent();
            viewmodel = new ViewModel();
            BindingContext=viewmodel;
            
        }

      
    }
    public class Model
    {
        public String? HeroName { get; set; }
        public double PowerLevel { get; set; }
    }
    public class ViewModel
    {
        public List<Model> Data1 { get; set; }
        public List<Model> Data2 { get; set; }

        public List<Model> Data3 { get; set; }
        public List<Model> Data4 { get; set; }

        public List<Model> Data5 { get; set; }
        public List<int> colo {get;set ;}
        public List<int> insideSeries { get; set; }


        public ViewModel()
        {
            
            Data1 = new List<Model>();
            Data2 = new List<Model>();
            Data3 = new List<Model>();
            Data4 = new List<Model>();
            Data5 = new List<Model>();

            insideSeries = new List<int>();
            insideSeries.Add(0);
            insideSeries.Add(3);
            colo = new List<int> ();
            colo.Add(1);
            colo.Add(4);

            Data1.Add(new Model { HeroName = "Luffy", PowerLevel = 150 });
            Data1.Add(new Model { HeroName = "Naruto", PowerLevel =30});
            Data1.Add(new Model { HeroName = "Asta", PowerLevel = 45 });
            Data1.Add(new Model { HeroName = "Vegeta", PowerLevel = 60 });
            Data1.Add(new Model { HeroName = "Goku", PowerLevel = 75 });
            Data1.Add(new Model { HeroName = "Saitama", PowerLevel = 90 });





            Data2.Add(new Model { HeroName = "Luffy", PowerLevel = 300 });
            Data2.Add(new Model { HeroName = "Naruto", PowerLevel = 60 });
            Data2.Add(new Model { HeroName = "Asta", PowerLevel = 90 });
            Data2.Add(new Model { HeroName = "Vegeta", PowerLevel =120 });
            Data2.Add(new Model { HeroName = "Goku", PowerLevel = 150 });
            Data2.Add(new Model { HeroName = "Saitama", PowerLevel = 180 });




            Data3.Add(new Model { HeroName = "Luffy", PowerLevel = 150 });
            Data3.Add(new Model { HeroName = "Naruto", PowerLevel = 100 });
            Data3.Add(new Model { HeroName = "Asta", PowerLevel = 150 });
            Data3.Add(new Model { HeroName = "Vegeta", PowerLevel = 200 });
            Data3.Add(new Model { HeroName = "Goku", PowerLevel = 250 });
            Data3.Add(new Model { HeroName = "Saitama", PowerLevel = 300 });




            Data4.Add(new Model { HeroName = "Luffy", PowerLevel =180 });
            Data4.Add(new Model { HeroName = "Naruto", PowerLevel = 160 });
            Data4.Add(new Model { HeroName = "Asta", PowerLevel = 240 });
            Data4.Add(new Model { HeroName = "Vegeta", PowerLevel = 300 });
            Data4.Add(new Model { HeroName = "Goku", PowerLevel = 360 });
            Data4.Add(new Model { HeroName = "Saitama", PowerLevel = 420 });



            Data5.Add(new Model { HeroName = "Luffy", PowerLevel = 200 });
            Data5.Add(new Model { HeroName = "Naruto", PowerLevel = 200 });
            Data5.Add(new Model { HeroName = "Asta", PowerLevel = 300 });
            Data5.Add(new Model { HeroName = "Vegeta", PowerLevel = 400 });
            Data5.Add(new Model { HeroName = "Goku", PowerLevel = 500 });
            Data5.Add(new Model { HeroName = "Saitama", PowerLevel = 600 });





        }


    }
}
