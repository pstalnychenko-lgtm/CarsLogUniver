using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class RegisterPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vm;
        private readonly DatabaseService _db;

        public RegisterPage(AppStateService appState, VehicleViewModel vm, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _vm = vm;
            _db = db;
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            var firstName = FirstNameEntry.Text?.Trim();
            var lastName  = LastNameEntry.Text?.Trim();
            var login     = LoginEntry.Text?.Trim();
            var email     = EmailEntry.Text?.Trim();
            var phone     = PhoneEntry.Text?.Trim();
            var password  = PasswordEntry.Text;
            var confirm   = ConfirmPasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(firstName) || string.IsNullOrWhiteSpace(lastName) ||
                string.IsNullOrWhiteSpace(login)     || string.IsNullOrWhiteSpace(phone)    ||
                string.IsNullOrWhiteSpace(password))
            {
                ShowError("Заповніть всі обов'язкові поля.");
                return;
            }

            if (password != confirm)
            {
                ShowError("Паролі не збігаються.");
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

            // Перевірка вимог до паролю (ті ж що і в моделі User)
            if (password.Length < 8)
            {
                ShowError("Пароль має містити щонайменше 8 символів.");
                return;
            }
            if (!password.Any(char.IsUpper))
            {
                ShowError("Пароль має містити хоча б одну велику літеру.");
                return;
            }
            if (!password.Any(char.IsDigit))
            {
                ShowError("Пароль має містити хоча б одну цифру.");
                return;
            }

            var existing = await _db.GetUserByLoginAsync(login);
            if (existing != null)
            {
                ShowError("Користувач з таким логіном вже існує.");
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
                    var driver = new Driver(firstName, lastName, phone,
                        "DRV-0000", "ТСЦ", DateTime.Now.AddYears(3), true, BloodType.A_Positive);
                    driver.ChangeLogin(login);
                    if (!string.IsNullOrWhiteSpace(email)) driver.ChangeEmail(email);
                    newUser = driver;
                }

                await _db.SaveUserWithPasswordAsync(newUser, password);
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
