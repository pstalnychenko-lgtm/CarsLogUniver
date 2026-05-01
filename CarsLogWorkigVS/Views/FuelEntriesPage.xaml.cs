using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class FuelEntriesPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public FuelEntriesPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            var v = _appState.SelectedVehicle;
            if (v == null) { FuelCollection.ItemsSource = new List<FuelEntry>(); return; }

            var entities = await _db.GetFuelEntriesAsync(v.Id.ToString());
            v.FuelEntries.Clear();
            foreach (var e in entities)
            {
                try
                {
                    var entry = new FuelEntry(e.GasStationName, e.GasStationAddress,
                        (FuelsType)e.FuelType, e.Liters, e.PricePerLiter);
                    v.FuelEntries.Add(entry);
                }
                catch { }
            }
            FuelCollection.ItemsSource = v.FuelEntries.ToList();
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddFuelEntryPage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
