using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class VehicleListPage : ContentPage
    {
        private readonly VehicleViewModel _vm;
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public VehicleListPage(VehicleViewModel vm, AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _vm = vm;
            _appState = appState;
            _db = db;
        }

        protected override async void OnAppearing()
        {
            base.OnAppearing();
            await LoadVehiclesFromDb();
            VehiclesCollection.ItemsSource = _vm.GetFilteredVehicles().ToList();
        }

        private async Task LoadVehiclesFromDb()
        {
            if (_appState.CurrentUser == null) return;
            var ownerId = _appState.CurrentUser.Id.ToString();
            var entities = await _db.GetVehiclesForOwnerAsync(ownerId);

            foreach (var entity in entities)
            {
                var alreadyLoaded = _vm.Vehicles.Any(v => v.Id.ToString() == entity.Id);
                if (alreadyLoaded) continue;

                if (_appState.CurrentUser is Owner owner)
                {
                    try
                    {
                        var vehicle = new Vehicle(
                            entity.PlateNumber, entity.Vin, entity.Brand, entity.Model,
                            entity.Color, entity.BodyType, (uint)entity.EngineVolumeCc,
                            (FuelsType)entity.FuelType, entity.FuelTankCapacity,
                            entity.YearOfRelease, entity.CarReleaseDate, owner
                        );
                        vehicle.ChangeCurrentMileage((uint)entity.CurrentMileage);
                        if (!string.IsNullOrEmpty(entity.GeneralNotes))
                            vehicle.ChangeGeneralNotes(entity.GeneralNotes);
                        _vm.TryAddVehicle(vehicle);
                    }
                    catch { }
                }
            }
        }

        private void OnSearchChanged(object sender, TextChangedEventArgs e)
        {
            _vm.SearchQuery = e.NewTextValue;
            VehiclesCollection.ItemsSource = _vm.GetFilteredVehicles().ToList();
        }

        private async void OnVehicleSelected(object sender, SelectionChangedEventArgs e)
        {
            if (e.CurrentSelection.FirstOrDefault() is Vehicle vehicle)
            {
                _appState.SelectedVehicle = vehicle;
                ((CollectionView)sender).SelectedItem = null;
                await Shell.Current.GoToAsync(nameof(VehicleDetailPage));
            }
        }

        private async void OnAddClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync(nameof(AddVehiclePage));

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");
    }
}
