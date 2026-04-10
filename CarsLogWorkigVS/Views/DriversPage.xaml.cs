using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class DriversPage : ContentPage
    {
        private readonly AppStateService _appState;

        public DriversPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            DriversCollection.ItemsSource = _appState.SelectedVehicle?.Drivers
                ?? new List<CarsLogWorkig.Models.Driver>();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddDriverPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
