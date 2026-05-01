using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class AddVehiclePage : ContentPage
    {
        private readonly VehicleViewModel _vm;
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public AddVehiclePage(VehicleViewModel vm, AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _vm = vm;
            _appState = appState;
            _db = db;
            BodyTypePicker.SelectedIndex = 0;
            FuelTypePicker.SelectedIndex = 0;
            YearPicker.MaximumDate = DateTime.Now;
            YearPicker.Date = DateTime.Now;
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var plate = PlateEntry.Text?.Trim();
            var vin = VinEntry.Text?.Trim();
            var brand = BrandEntry.Text?.Trim();
            var model = ModelEntry.Text?.Trim();
            var color = ColorEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(plate) || string.IsNullOrWhiteSpace(vin) ||
                string.IsNullOrWhiteSpace(brand) || string.IsNullOrWhiteSpace(model))
            {
                ShowError("Заповніть всі обов'язкові поля (*).");
                return;
            }

            if (!uint.TryParse(EngineEntry.Text, out var engine) || engine == 0)
            {
                ShowError("Введіть коректний об'єм двигуна.");
                return;
            }

            if (!decimal.TryParse(TankEntry.Text?.Replace(',', '.'), out var tank) || tank <= 0)
            {
                ShowError("Введіть коректний об'єм бака.");
                return;
            }

            var bodyType = BodyTypePicker.SelectedItem?.ToString() ?? "Седан";
            var fuelTypeIndex = FuelTypePicker.SelectedIndex;
            var fuelType = fuelTypeIndex switch
            {
                1 => FuelsType.Diesel,
                2 => FuelsType.Electric,
                3 => FuelsType.Hybrid,
                _ => FuelsType.Petrol
            };

            try
            {
                Owner owner;
                if (_appState.CurrentUser is Owner o)
                    owner = o;
                else
                    owner = new Owner("Гість", "Користувач", "+380000000000", "Не вказано", DateTime.Now.AddYears(-1));

                var vehicle = new Vehicle(
                    plateNumber: plate,
                    vin: vin,
                    brand: brand,
                    model: model,
                    color: string.IsNullOrWhiteSpace(color) ? "Не вказано" : color,
                    bodyType: bodyType,
                    engineVolumeCc: engine,
                    fuelType: fuelType,
                    fuelTankCapacity: tank,
                    yearOfRelease: (DateTime)YearPicker.Date,
                    carReleaseDate: (DateTime)YearPicker.Date,
                    owner: owner
                );

                if (uint.TryParse(MileageEntry.Text, out var mileage))
                    vehicle.ChangeCurrentMileage(mileage);

                if (_vm.TryAddVehicle(vehicle))
                {
                    await _db.SaveVehicleAsync(vehicle);
                    _appState.SelectedVehicle = vehicle;
                    await Shell.Current.GoToAsync("..");
                }
                else
                {
                    ShowError(_vm.LastError);
                }
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");

        private void ShowError(string message)
        {
            ErrorLabel.Text = message;
            ErrorLabel.IsVisible = true;
        }
    }
}
