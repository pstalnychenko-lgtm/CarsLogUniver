using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class TripLogsPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public TripLogsPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) { TripCollection.ItemsSource = new List<TripLog>(); return; }

            var entities = await _db.GetTripLogsAsync(v.Id.ToString());
            v.TripLogs.Clear();
            foreach (var e in entities)
            {
                try
                {
                    var trip = new TripLog(e.TripDate, e.DeparturePoint, e.Destination,
                        (TripPurpose)e.Purpose, (uint)e.StartMileage, (uint)e.EndMileage, e.Notes);
                    v.TripLogs.Add(trip);
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Помилка", ex.Message, "OK");
                }
            }
            TripCollection.ItemsSource = v.TripLogs.ToList();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddTripLogPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
