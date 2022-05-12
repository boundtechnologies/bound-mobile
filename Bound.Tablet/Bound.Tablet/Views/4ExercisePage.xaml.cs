using Bound.Tablet.Services.Interfaces;
using Bound.Tablet.ViewModels;
using Devicemanager.API.Managers;
using MedGame.UI.Mobile.Interfaces;
using Microsoft.Azure.Devices;
using System.ComponentModel;
using System.Diagnostics;
using System.Net.Http;
using System.Threading;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bound.Tablet.Views
{
    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExercisePage : ContentPage
    {
        private readonly IAudioService _audioService;

        public ExercisePage()
        {
            InitializeComponent();
            BindingContext = new ExercisePageViewModel();
            _audioService = DependencyService.Get<IAudioService>();

            LabelTitle.Text = App.DeviceData.MachineName;
        }
        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }


        private async void ButtonStart_Clicked(object sender, System.EventArgs e)
        {
            //for (int i = 0; i < 4; i++)
            //{
            //    _audioService.PlayAudioFile("beep.mp3");
            //    Thread.Sleep(1000);
            //}

            IoTHubManager ioTHubManager = new IoTHubManager();
            var device = await ioTHubManager.Get(App.DeviceData.MachineName);
            await ioTHubManager.SendStartRequestToDevice(device);
            Debug.WriteLine("Device started: " + App.DeviceData.MachineName);

        }
        private async void ButtonStop_Clicked(object sender, System.EventArgs e)
        {
            IoTHubManager ioTHubManager = new IoTHubManager();
            var device = await ioTHubManager.Get(App.DeviceData.MachineName);
            await ioTHubManager.SendStopRequestToDevice(device);
            Debug.WriteLine("Device stopped: " + App.DeviceData.MachineName);
        }

        private void ImageButtonBack_Clicked(object sender, System.EventArgs e)
        {
            Application.Current.MainPage = new ExercisesPage();
        }
    }
}