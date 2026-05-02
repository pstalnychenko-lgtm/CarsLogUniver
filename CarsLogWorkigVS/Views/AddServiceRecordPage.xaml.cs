using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class AddServiceRecordPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public AddServiceRecordPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
            var v = appState.SelectedVehicle;
            if (v != null) MileageEntry.Text = v.CurrentMileage.ToString();
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var vehicle = _appState.SelectedVehicle;
            if (vehicle == null) { ShowError("Автомобіль не вибрано."); return; }

            var desc = DescriptionEditor.Text?.Trim();
            if (string.IsNullOrWhiteSpace(desc)) { ShowError("Вкажіть опис обслуговування."); return; }

            if (!uint.TryParse(MileageEntry.Text, out var mileage))
            { ShowError("Введіть коректний пробіг."); return; }

            if (!decimal.TryParse(CostEntry.Text?.Replace(',', '.'), out var cost) || cost < 0)
            { ShowError("Введіть коректну вартість."); return; }

            try
            {
                var record = new ServiceRecord(
                    dateOfService: (DateTime)ServiceDatePicker.Date,
                    description: desc,
                    mileageAtService: mileage,
                    cost: cost
                );

                if (_appState.CurrentUser is Owner owner)
                    owner.AddServiceRecord(vehicle, record);
                else
                    vehicle.ServiceRecords.Add(record);

                await _db.SaveServiceRecordAsync(vehicle.Id.ToString(), record);
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
