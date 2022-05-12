using System.Diagnostics;
using System.Threading.Tasks;
using System.Windows.Input;
using Bound.Tablet.Dots.Authentication;
using Bound.Tablet.Models;
using Bound.Tablet.Services.Interfaces;
using Bound.Tablet.Views;
using Newtonsoft.Json;
using Xamarin.Forms;

namespace Bound.Tablet.ViewModels
{
    public class AuthenticationViewModel : BaseViewModel
    {
        readonly IAuthenticationService _authenticationService;
        public ICommand AuthenticationCommand { get; }

        public string Email { get; set; }

        public string Password { get; set; }

        public string ImageNFC { get; set; } = "nfc1.png";

        bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set => SetProperty(ref _isRefreshing, value);
        }

        public AuthenticationViewModel()
        { }

        public AuthenticationViewModel(IAuthenticationService authenticationService)
        {
            _authenticationService = authenticationService;

            //Temporary to avoid having to enter it every time
            Email = "test@boundtechnologies.com";
            Password = "Dej858591";

            IsRefreshing = false;
            AuthenticationCommand = new Command(async () =>
            {
                IsRefreshing = true;
                await Authentication();
                Application.Current.MainPage = new HeatMapPage();
                IsRefreshing = false;
            });
        }

        public async Task Authentication()
        {
            IsBusy = true;
            Debug.WriteLine("Authentication started");

            var savedUser = Xamarin.Essentials.Preferences.Get("user", "");
            var savedTokens = Xamarin.Essentials.Preferences.Get("tokens", "");

            if (!string.IsNullOrEmpty(savedUser) && !string.IsNullOrEmpty(savedTokens))
            {
                App.User= JsonConvert.DeserializeObject<User>(savedUser);
                App.Tokens= JsonConvert.DeserializeObject<Tokens>(savedTokens);
                
                App.DeviceData = new DeviceData()
                {
                    ObjectId = App.User.ObjectId
                }; 
                
                Debug.WriteLine("User found on device");
                return;
            }

            if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
            {
                await Application.Current.MainPage.DisplayAlert("Error", "AppResources.AuthenticationNoUsernamePasswordAlertText", "AppResources.OK");
                Debug.WriteLine("Some input is wrong");
            }

            var AuthenticationResult = await _authenticationService.AuthenticationAsync(Email, Password);
            await Application.Current.MainPage.DisplayAlert("Token:", AuthenticationResult.AccessToken, "OK");

            App.User = new User()
            {
                Email = AuthenticationResult.Email,
                FirstName = AuthenticationResult.FirstName,
                LastName = AuthenticationResult.LastName,
                ObjectId = AuthenticationResult.ObjectId,
                Role=AuthenticationResult.Role
            };

            App.Tokens = new Tokens()
            {
                AccessToken = AuthenticationResult.AccessToken,
                RefreshToken = AuthenticationResult.RefreshToken
            };

            var userAsJSON = JsonConvert.SerializeObject(App.User);
            var tokensAsJSON = JsonConvert.SerializeObject(App.Tokens);

            Xamarin.Essentials.Preferences.Set("user", userAsJSON);
            Xamarin.Essentials.Preferences.Set("tokens", tokensAsJSON);

            Debug.WriteLine("Authentication finnished");
            Debug.WriteLine("User is saved on device");


            IsBusy = false;
        }
    }
}