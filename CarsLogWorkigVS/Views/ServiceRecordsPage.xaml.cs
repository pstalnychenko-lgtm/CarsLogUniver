using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class ServiceRecordsPage : ContentPage
    {
        private readonly AppStateService _appState;

        public ServiceRecordsPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            ServiceCollection.ItemsSource = _appState.SelectedVehicle?.ServiceRecords
                ?? new List<CarsLogWorkig.Models.ServiceRecord>();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddServiceRecordPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
