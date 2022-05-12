using Autofac;
using Bound.Common.Helpers;
using Bound.Tablet.Interfaces;
using Bound.Tablet.Services.Interfaces;
using Bound.Tablet.ViewModels;
using System.ComponentModel;
using System.Diagnostics;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace Bound.Tablet.Views
{
    [DesignTimeVisible(true)]
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class AuthenticationPage : ContentPage
    {
        private readonly IAuthenticationService _authenticationService;

        public AuthenticationPage()
        {
            //Device.BeginInvokeOnMainThread(async () =>
            //    {
            //        var internet = InternetHelpers.CheckIfBoundBusinessIsOnline();

            //        if (!internet)
            //        {
            //            await DisplayAlert($"No internet connection found on you device", "Check you connection on your device", "Ok");
            //        }
            //    });


            var notificationManager = DependencyService.Get<INotificationManager>();
            notificationManager.ScheduleNotification("title", "test");

            InitializeComponent();

            _authenticationService = DependencyService.Get<IAuthenticationService>();


            BindingContext = new AuthenticationViewModel(_authenticationService);
        }
        //protected async override void OnAppearing()
        //{
        //    //var configuration = new MqttConfiguration();
        //    //_client = await MqttClient.CreateAsync(Constants.HostMqttBroker, configuration);
        //    //await _client.ConnectAsync();

        //    //await _client.SubscribeAsync(Constants.StartDeviceTopic, MqttQualityOfService.AtMostOnce);
        //    //_client.MessageStream.Subscribe(message => SendMessageToStartDeviceAsync(message));
        //}

        //private void SendMessageToStartDeviceAsync(MqttApplicationMessage message)
        //{
        //    string messageAsString = Encoding.UTF8.GetString(message.Payload);

        //    Device.BeginInvokeOnMainThread(async () =>
        //    {
        //        await DisplayAlert($"Message recieved on topic: {message.Topic}", messageAsString, "Ok");
        //    });
        //}

        protected override bool OnBackButtonPressed()
        {
            base.OnBackButtonPressed();
            return true;
        }
    }
}