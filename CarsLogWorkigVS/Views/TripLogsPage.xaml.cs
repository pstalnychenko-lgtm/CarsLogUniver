using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class TripLogsPage : ContentPage
    {
        private readonly AppStateService _appState;

        public TripLogsPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            TripCollection.ItemsSource = _appState.SelectedVehicle?.TripLogs
                ?? new List<CarsLogWorkig.Models.TripLog>();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddTripLogPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
