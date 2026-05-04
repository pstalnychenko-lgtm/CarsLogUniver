using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class ReportPage : ContentPage
    {
        private readonly VehicleViewModel _vm;
        private readonly AppStateService _appState;

        public ReportPage(VehicleViewModel vm, AppStateService appState)
        {
            InitializeComponent();
            _vm = vm;
            _appState = appState;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            if (!_appState.CanViewReports)
            {
                await DisplayAlert("Доступ заборонено", "Звіти доступні лише для Власника та Адміна.", "OK");
                await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));
                return;
            }
            BuildReport();
        }

        private void BuildReport()
        {
            var vehicles = _vm.Vehicles;
            TotalVehiclesLabel.Text = vehicles.Count.ToString();

            var weekAgo = DateTime.Now.AddDays(-7);
            int weekTrips = 0;
            decimal totalExpenses = 0;

            foreach (var v in vehicles)
            {
                weekTrips += v.TripLogs.Count(t => t.TripDate >= weekAgo);
                totalExpenses += v.GetTotalExpenses();
            }

            WeekTripsLabel.Text = weekTrips.ToString();
            TotalExpensesLabel.Text = _vm.FormatAmount(totalExpenses);
            VehicleReportCollection.ItemsSource = vehicles;
            FullReportLabel.Text = _vm.GenerateWeeklyReport();
        }

        private void OnRefreshClicked(object sender, EventArgs e) => BuildReport();

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));
    }
}
