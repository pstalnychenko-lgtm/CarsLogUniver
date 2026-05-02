using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class ReportPage : ContentPage
    {
        private readonly VehicleViewModel _vm;

        public ReportPage(VehicleViewModel vm)
        {
            InitializeComponent(); 
            _vm = vm;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
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
