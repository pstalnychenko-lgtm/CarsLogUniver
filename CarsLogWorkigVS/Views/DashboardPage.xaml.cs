using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class DashboardPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;

        public DashboardPage(AppStateService appState, VehicleViewModel vm)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            Refresh();
        }

        private void Refresh()
        {
            if (_appState.IsLoggedIn)
                GreetingLabel.Text = $"Вітаємо, {_appState.CurrentUser!.FullName}!";
            else
                GreetingLabel.Text = "Вітаємо, Гість!";

            var vehicles = _vm.Vehicles;
            VehicleCountLabel.Text = vehicles.Count.ToString();

            if (vehicles.Count > 0)
            {
                var last = vehicles[^1];
                LastVehicleName.Text = $"{last.Brand} {last.Model}";
                LastVehiclePlate.Text = last.PlateNumber;
                LastVehicleMileage.Text = _vm.FormatMileage(last.CurrentMileage);
                LastVehicleFrame.IsVisible = true;
                EmptyVehicleFrame.IsVisible = false;
            }
            else
            {
                LastVehicleFrame.IsVisible = false;
                EmptyVehicleFrame.IsVisible = true;
            }
        }

        private async void OnVehiclesClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(VehicleListPage));

        private async void OnReportClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(ReportPage));

        private async void OnDriversClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(DriversPage));

        private async void OnAdminClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AdminPanelPage));

        private async void OnLastVehicleTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(VehicleDetailPage));

        private async void OnAddVehicleClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddVehiclePage));

        private async void OnFuelClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(FuelEntriesPage));

        private async void OnExpensesClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(ExpensesPage));

        private async void OnProfileClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(ProfilePage));
    }
}
