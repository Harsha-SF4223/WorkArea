using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StackingLine
{
    public class StackedLineViewModel
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
        public List<int> colo { get; set; }
        public List<int> insideSeries { get; set; }

        public ObservableCollection<Model> Data { get; }
        public ObservableCollection<Brush> PaletteBrushes { get; set; }

        public StackedLineViewModel()
        {
            colo = new List<int>();
            colo.Add(1);
            colo.Add(3);
            colo.Add(5);
            PaletteBrushes = new ObservableCollection<Brush>()
            {
               new SolidColorBrush(Colors.Red),
                 new SolidColorBrush(Colors.Black),
                 new SolidColorBrush(Colors.Blue),
                 new SolidColorBrush(Colors.Green),
                 new SolidColorBrush(Colors.Yellow)
            };
            Data = new ObservableCollection<Model>()
            {
                 new Model(double.NaN,"Jan",double.NaN,300,400),
                new Model(double.NaN,"Mar",200,300,400),
                    
                new Model(double.NaN,"May",200,300,400),
                new Model(double.NaN,"Jul",200,300,400),
                new Model(double.NaN,"Sep",200,300,400),
                new Model(double.NaN,"Nov",200,300,400),

            };



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
            colo = new List<int>();
            colo.Add(1);
            colo.Add(4);

            Data1.Add(new Model { HeroName = "Father", Value = 10});
            Data1.Add(new Model { HeroName = "Mother", Value = 11 });
            Data1.Add(new Model { HeroName = "Son", Value =8});
            Data1.Add(new Model { HeroName = "Daughter ", Value = 9 });
            Data1.Add(new Model { HeroName = "Brother ", Value = 14 });



            Data2.Add(new Model { HeroName = "Father", Value = 15 });
            Data2.Add(new Model { HeroName = "Mother", Value = 16 });
            Data2.Add(new Model { HeroName = "Son", Value = 15 });
            Data2.Add(new Model { HeroName = "Daughter ", Value = 15 });
            Data2.Add(new Model { HeroName = "Brother ", Value = 18 });




            Data3.Add(new Model { HeroName = "Father", Value = 17 });
            Data3.Add(new Model { HeroName = "Mother", Value = 18 });
            Data3.Add(new Model { HeroName = "Son", Value = 20 });
            Data3.Add(new Model { HeroName = "Daughter ", Value = 15 });
            Data3.Add(new Model { HeroName = "Brother ", Value = 19 });



            Data4.Add(new Model { HeroName = "Father", Value = 20 });
            Data4.Add(new Model { HeroName = "Mother", Value = 22 });
            Data4.Add(new Model { HeroName = "Son", Value = 24 });
            Data4.Add(new Model { HeroName = "Daughter ", Value = 19 });
            Data4.Add(new Model { HeroName = "Brother ", Value = 22 });




            /*   Data6.Add(new Model { HeroName = "Luffy", PowerLevel = 200 });
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
   */

        }


    }
}
