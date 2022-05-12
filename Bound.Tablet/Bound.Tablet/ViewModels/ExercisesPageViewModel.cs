using System.Threading.Tasks;
using System.Windows.Input;
using Bound.Tablet.Models;
using Bound.Tablet.Services.Interfaces;
using Bound.Tablet.Views;
using Xamarin.Forms;

namespace Bound.Tablet.ViewModels
{
    public class ExercisesPageViewModel : BaseViewModel
    {
        public ICommand ShowMachinePage { get; }

        bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set => SetProperty(ref _isRefreshing, value);
        }

        public ExercisesPageViewModel()
        {
            ShowMachinePage = new Command(() =>
            {
                IsRefreshing = true;
                //await Authentication();
                App.User = new User();
                Application.Current.MainPage = new ExercisePage();

                IsRefreshing = false;
            });
        }
    }
}