using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;

        public RegisterPage(AppStateService appState, VehicleViewModel vm)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var firstName = FirstNameEntry.Text?.Trim();
            var lastName = LastNameEntry.Text?.Trim();
            var login = LoginEntry.Text?.Trim();
            var email = EmailEntry.Text?.Trim();
            var phone = PhoneEntry.Text?.Trim();
            var password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(phone))
            {
                ShowError("Заповніть всі обов'язкові поля.");
                return;
            }

            if (!_vm.ValidateEmail(email ?? ""))
            {
                ShowError(_vm.LastError);
                return;
            }

            if (!_vm.ValidatePhone(phone))
            {
                ShowError(_vm.LastError);
                return;
            }

            try
            {
                bool isOwner = RolePicker.SelectedIndex != 1;

                User newUser;
                if (isOwner)
                {
                    var owner = new Owner(firstName, lastName, phone, "Не вказано", DateTime.Now.AddYears(-1));
                    owner.ChangeLogin(login);
                    if (!string.IsNullOrWhiteSpace(email)) owner.ChangeEmail(email);
                    newUser = owner;
                }
                else
                {
                    var driver = new Driver(firstName, lastName, phone, "DRV-0000", "ТСЦ", DateTime.Now.AddYears(3), true, BloodType.A_Positive);
                    driver.ChangeLogin(login);
                    if (!string.IsNullOrWhiteSpace(email)) driver.ChangeEmail(email);
                    newUser = driver;
                }

                _appState.CurrentUser = newUser;
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
            catch (Exception ex)
            {
                ShowError(ex.Message);
            }
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

        private void ShowError(string message)
        {
            ErrorLabel.Text = message;
            ErrorLabel.IsVisible = true;
        }
    }
}
