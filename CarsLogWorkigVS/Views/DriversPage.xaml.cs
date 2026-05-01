using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class DriversPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public DriversPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) { DriversCollection.ItemsSource = new List<Driver>(); return; }

            var driverIds = await _db.GetDriverIdsForVehicleAsync(v.Id.ToString());
            v.Drivers.Clear();
            foreach (var driverId in driverIds)
            {
                var user = await _db.GetUserByIdAsync(driverId);
                if (user is Driver driver)
                    v.Drivers.Add(driver);
            }
            DriversCollection.ItemsSource = v.Drivers.ToList();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddDriverPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
