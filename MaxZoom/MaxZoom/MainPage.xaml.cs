using Microsoft.Maui.Controls.Shapes;

namespace MaxZoom
{
    public partial class MainPage : ContentPage
    {
        

        public MainPage()
        {
            InitializeComponent();
        }

        private void Button_Clicked(object sender, EventArgs e)
        {
            check_factor.ZoomFactor = 0.3;
        }

        private void Button_Clicked_1(object sender, EventArgs e)
        {
            check_zoom.EnablePinchZooming = true;
        }

        private void Button_Clicked_2(object sender, EventArgs e)
        {
            check_zoom.EnablePinchZooming = false;
        }

        private void Button_Clicked_3(object sender, EventArgs e)
        {
            check_zoom.EnableDoubleTap = true;
        }

        private void Button_Clicked_4(object sender, EventArgs e)
        {
            check_zoom.EnableDoubleTap = false;
        }

        private void Button_Clicked_5(object sender, EventArgs e)
        {
            check_zoom.EnableSelectionZooming= true;
        }

        private void Button_Clicked_6(object sender, EventArgs e)
        {
            check_zoom.EnableSelectionZooming = false;
        }
        private void Button_Clicked_7(object sender, EventArgs e)
        {
            check_zoom.MaximumZoomLevel = 2;
        }
        private void Button_Clicked_8(object sender, EventArgs e)
        {
            check_zoom.MaximumZoomLevel = 100;
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
        public ViewModel()
        {

            Data1 = new List<Model>();
            Data2 = new List<Model>();
            Data3 = new List<Model>();

            Data1.Add(new Model { HeroName = "Father", PowerLevel = 150 });
            Data1.Add(new Model { HeroName = "Mother", PowerLevel = 30 });
            Data1.Add(new Model { HeroName = "Daughter", PowerLevel = 45 });
            Data1.Add(new Model { HeroName = "Son", PowerLevel = 60 });

            Data2.Add(new Model { HeroName = "Father", PowerLevel = 300 });
            Data2.Add(new Model { HeroName = "Mother", PowerLevel = 60 });
            Data2.Add(new Model { HeroName = "Daughter", PowerLevel = 90 });
            Data2.Add(new Model { HeroName = "Son", PowerLevel = 120 });

            Data3.Add(new Model { HeroName = "Father", PowerLevel = 150 });
            Data3.Add(new Model { HeroName = "Mother", PowerLevel = 100 });
            Data3.Add(new Model { HeroName = "Daughter", PowerLevel = 150 });
            Data3.Add(new Model { HeroName = "Son", PowerLevel = 200 });
        }
    }
}
