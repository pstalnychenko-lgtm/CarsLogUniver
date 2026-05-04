using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class DashboardPage : ContentPage
    {
        private readonly DashboardViewModel _vm;
        private readonly AppStateService _appState;

        public DashboardPage(AppStateService appState, VehicleViewModel vehicleViewModel)
        {
            InitializeComponent();
            _appState = appState;
            _vm = new DashboardViewModel(appState, vehicleViewModel);
            BindingContext = _vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            _vm.LoadData();
            ApplyRbac();
        }

        private void ApplyRbac()
        {
            bool canManage = _appState.CanManageVehicles;
            bool canReport = _appState.CanViewReports;
            bool canDrivers = _appState.CanManageDrivers;
            bool canAdmin = _appState.CanAccessAdminPanel;
            bool canExpenses = _appState.CanViewExpenses;
            bool isDriver = _appState.IsDriver;

            ReportTile.IsVisible = canReport;
            DriversTile.IsVisible = canDrivers;
            AdminTile.IsVisible = canAdmin;
            ExpensesTab.IsVisible = canExpenses;
            AddVehicleButton.IsVisible = canManage;
            DriverInfoBanner.IsVisible = isDriver;
            SecondRowGrid.IsVisible = canDrivers || canAdmin;
            ViewingModeBanner.IsVisible = _appState.IsViewingAsOther;

            RoleBadgeLabel.Text = _appState.CurrentUser?.Role switch
            {
                CarsLogWorkig.Models.UserRole.Owner => "Власник · повний доступ",
                CarsLogWorkig.Models.UserRole.Driver => "Водій · обмежений доступ",
                CarsLogWorkig.Models.UserRole.Admin => "Адміністратор · керування системою",
                _ => ""
            };
        }

        private async void OnVehiclesClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(VehicleListPage));

        private async void OnReportClicked(object sender, EventArgs e)
        {
            if (!_appState.CanViewReports)
            {
                await DisplayAlert("Доступ заборонено", "Звіти доступні лише для Власника та Адміна.", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(ReportPage));
        }

        private async void OnDriversClicked(object sender, EventArgs e)
        {
            if (!_appState.CanManageDrivers)
            {
                await DisplayAlert("Доступ заборонено", "Керування водіями доступне лише для Власника та Адміна.", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(DriversPage));
        }

        private async void OnAdminClicked(object sender, EventArgs e)
        {
            if (!_appState.CanAccessAdminPanel)
            {
                await DisplayAlert("Доступ заборонено", "Адмін-панель доступна лише для Адміна.", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(AdminPanelPage));
        }

        private async void OnLastVehicleTapped(object sender, TappedEventArgs e) =>
            await Shell.Current.GoToAsync(nameof(VehicleDetailPage));

        private async void OnAddVehicleClicked(object sender, EventArgs e)
        {
            if (!_appState.CanManageVehicles)
            {
                await DisplayAlert("Доступ заборонено", "Додавання авто доступне лише для Власника та Адміна.", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(AddVehiclePage));
        }

        private async void OnFuelClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(FuelEntriesPage));

        private async void OnExpensesClicked(object sender, EventArgs e)
        {
            if (!_appState.CanViewExpenses)
            {
                await DisplayAlert("Доступ заборонено", "Витрати доступні лише для Власника та Адміна.", "OK");
                return;
            }
            await Shell.Current.GoToAsync(nameof(ExpensesPage));
        }

        private async void OnProfileClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(ProfilePage));

        private void OnReturnToAdminClicked(object sender, EventArgs e)
        {
            if (Shell.Current is AppShell shell)
            {
                shell.OnReturnToAdminClicked(sender, e);
            }
        }
    }
}
