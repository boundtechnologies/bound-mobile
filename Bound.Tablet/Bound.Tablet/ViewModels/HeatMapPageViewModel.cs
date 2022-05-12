namespace Bound.Tablet.ViewModels
{
    public class HeatMapPageViewModel : BaseViewModel
    {
        public string Machine1 { get; set; } = "ShouldersOverHead.png";

        bool _isRefreshing;
        public bool IsRefreshing
        {
            get { return _isRefreshing; }
            set => SetProperty(ref _isRefreshing, value);
        }
    }
}