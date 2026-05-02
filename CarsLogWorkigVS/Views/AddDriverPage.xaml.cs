using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class AddDriverPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;
        private readonly DatabaseService _db;

        public AddDriverPage(AppStateService appState, VehicleViewModel vm, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
            _db = db;
            BloodTypePicker.SelectedIndex = 0;
            ExpiryDatePicker.MinimumDate = DateTime.Now.AddDays(1);
            ExpiryDatePicker.Date = DateTime.Now.AddYears(3);
        }

        private async void OnSaveClicked(object sender, EventArgs e)
        {
            var vehicle = _appState.SelectedVehicle;

            var firstName = FirstNameEntry.Text?.Trim();
            var lastName = LastNameEntry.Text?.Trim();
            var phone = PhoneEntry.Text?.Trim();
            var license = LicenseEntry.Text?.Trim();
            var issuedBy = IssuedByEntry.Text?.Trim();

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName))
            { ShowError("Вкажіть ім'я та прізвище."); return; }

            if (!_vm.ValidatePhone(phone ?? "")) { ShowError(_vm.LastError); return; }

            if (string.IsNullOrWhiteSpace(license)) { ShowError("Вкажіть номер посвідчення."); return; }
            if (string.IsNullOrWhiteSpace(issuedBy)) { ShowError("Вкажіть орган видачі."); return; }

            var bloodType = BloodTypePicker.SelectedIndex switch
            {
                0 => BloodType.A_Positive,
                1 => BloodType.A_Negative,
                2 => BloodType.B_Positive,
                3 => BloodType.B_Negative,
                4 => BloodType.AB_Positive,
                5 => BloodType.AB_Negative,
                6 => BloodType.O_Positive,
                _ => BloodType.O_Negative
            };

            try
            {
                var driver = new Driver(
                    firstName: firstName,
                    lastName: lastName,
                    phone: phone!,
                    licenseNumber: license,
                    licenseIssuedBy: issuedBy,
                    licenseExpiryDate: (DateTime)ExpiryDatePicker.Date,
                    medicalCertStatus: MedCertSwitch.IsToggled,
                    bloodType: bloodType
                );

                if (vehicle != null && _appState.CurrentUser is Owner owner)
                    owner.AssignDriverToVehicle(vehicle, driver);
                else if (vehicle != null)
                    vehicle.Drivers.Add(driver);

                await _db.SaveUserAsync(driver);
                if (vehicle != null)
                    await _db.LinkDriverToVehicleAsync(vehicle.Id.ToString(), driver.Id.ToString());

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
