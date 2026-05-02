using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class AddTripLogPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;
        private readonly DatabaseService _db;

        public AddTripLogPage(AppStateService appState, VehicleViewModel vm, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
            _db = db;
            PurposePicker.SelectedIndex = 0;
            TripDatePicker.MaximumDate = DateTime.Now;
            TripDatePicker.Date = DateTime.Now;

            var v = appState.SelectedVehicle;
            if (v != null)
            {
                StartMileageEntry.Text = v.CurrentMileage.ToString();
                EndMileageEntry.Text = v.CurrentMileage.ToString();
            }
        }

        private void OnMileageChanged(object sender, TextChangedEventArgs e)
        {
            uint.TryParse(StartMileageEntry.Text, out var start);
            uint.TryParse(EndMileageEntry.Text, out var end);
            DistanceLabel.Text = end >= start ? $"{end - start} км" : "0 км";
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var vehicle = _appState.SelectedVehicle;
            if (vehicle == null) { ShowError("Автомобіль не вибрано."); return; }

            var dep = DepartureEntry.Text?.Trim();
            var dest = DestinationEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(dep) || string.IsNullOrWhiteSpace(dest))
            { ShowError("Вкажіть точку відправлення та призначення."); return; }

            if (!uint.TryParse(StartMileageEntry.Text, out var start))
            { ShowError("Невірний початковий пробіг."); return; }

            if (!uint.TryParse(EndMileageEntry.Text, out var end) || end < start)
            { ShowError("Кінцевий пробіг не може бути меншим за початковий."); return; }

            if (!_vm.ValidateMileage(end, vehicle.CurrentMileage))
            { ShowError(_vm.LastError); return; }

            var purpose = PurposePicker.SelectedIndex switch
            {
                1 => TripPurpose.Business,
                2 => TripPurpose.Service,
                3 => TripPurpose.Other,
                _ => TripPurpose.Personal
            };

            try
            {
                var trip = new TripLog(
                    tripDate: (DateTime)TripDatePicker.Date,
                    departurePoint: dep,
                    destination: dest,
                    purpose: purpose,
                    startMileage: start,
                    endMileage: end,
                    notes: NotesEditor.Text?.Trim() ?? string.Empty
                );

                if (_appState.CurrentUser is Owner owner)
                    owner.AddTripLog(vehicle, trip);
                else
                    vehicle.TripLogs.Add(trip);

                vehicle.ChangeCurrentMileage(end);
                await _db.SaveTripLogAsync(vehicle.Id.ToString(), trip);
                await _db.SaveVehicleAsync(vehicle);
                await Shell.Current.GoToAsync("..");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));

        private void ShowError(string msg)
        {
            ErrorLabel.Text = msg;
            ErrorLabel.IsVisible = true;
        }
    }
}
