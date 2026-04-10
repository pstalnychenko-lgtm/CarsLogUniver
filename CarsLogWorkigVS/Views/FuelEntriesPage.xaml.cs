using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class FuelEntriesPage : ContentPage
    {
        private readonly AppStateService _appState;

        public FuelEntriesPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            FuelCollection.ItemsSource = v?.FuelEntries ?? new List<CarsLogWorkig.Models.FuelEntry>();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddFuelEntryPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
