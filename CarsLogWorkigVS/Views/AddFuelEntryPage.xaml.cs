using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class AddFuelEntryPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public AddFuelEntryPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
            FuelPicker.SelectedIndex = 0;
        }

        private void OnAmountChanged(object sender, TextChangedEventArgs e)
        {
            decimal.TryParse(LitersEntry.Text?.Replace(',', '.'), out var liters);
            decimal.TryParse(PriceEntry.Text?.Replace(',', '.'), out var price);
            TotalLabel.Text = $"{liters * price:N2} грн";
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var vehicle = _appState.SelectedVehicle;
            if (vehicle == null) { ShowError("Автомобіль не вибрано."); return; }

            var name = StationNameEntry.Text?.Trim();
            var address = StationAddressEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(name)) { ShowError("Вкажіть назву АЗС."); return; }

            if (!decimal.TryParse(LitersEntry.Text?.Replace(',', '.'), out var liters) || liters <= 0)
            { ShowError("Вкажіть коректну кількість літрів."); return; }

            if (!decimal.TryParse(PriceEntry.Text?.Replace(',', '.'), out var price) || price < 0)
            { ShowError("Вкажіть коректну ціну."); return; }

            var fuelType = FuelPicker.SelectedIndex switch
            {
                1 => FuelsType.Diesel,
                2 => FuelsType.Electric,
                3 => FuelsType.Hybrid,
                _ => FuelsType.Petrol
            };

            try
            {
                var entry = new FuelEntry(
                    gasStationName: name,
                    gasStationAddress: string.IsNullOrWhiteSpace(address) ? "Не вказано" : address,
                    fuelType: fuelType,
                    liters: liters,
                    pricePerLiter: price
                );

                if (_appState.CurrentUser is Owner owner)
                    owner.AddFuelEntry(vehicle, entry);
                else
                    vehicle.FuelEntries.Add(entry);

                await _db.SaveFuelEntryAsync(vehicle.Id.ToString(), entry);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await Shell.Current.GoToAsync("..");

        private void ShowError(string msg)
        {
            ErrorLabel.Text = msg;
            ErrorLabel.IsVisible = true;
        }
    }
}
