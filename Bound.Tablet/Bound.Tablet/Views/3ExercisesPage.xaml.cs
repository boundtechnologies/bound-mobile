using Bound.Tablet.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bound.Tablet.Views
{
    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisesPage : ContentPage
    {
        public ExercisesPage()
        {
            InitializeComponent();

            BindingContext = new ExercisesPageViewModel();
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }

        private void ImageButton_Clicked(object sender, System.EventArgs e)
        {
            ImageButton button = (ImageButton)sender;
            App.DeviceData.MachineName = button.Source.ToString().Substring(6);

            Debug.WriteLine("Machine selected: " + App.DeviceData.MachineName);

            Application.Current.MainPage = new ExercisePage();
        }

        private void ImageButtonBack_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new HeatMapPage();

        }
    }
}