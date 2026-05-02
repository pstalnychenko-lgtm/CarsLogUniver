using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class DashboardPage : ContentPage
    {
        private readonly DashboardViewModel _vm;

        public DashboardPage(AppStateService appState, VehicleViewModel vehicleViewModel)
        {
            InitializeComponent(); 
            _vm = new DashboardViewModel(appState, vehicleViewModel);
            BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            _vm.LoadData(); 
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
