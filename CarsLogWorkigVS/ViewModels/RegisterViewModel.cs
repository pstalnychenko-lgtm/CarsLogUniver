using System;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;
using CarsLogWorkigVS.Views;

namespace CarsLogWorkigVS.ViewModels
{
    public class RegisterViewModel : BaseViewModel
    {
        private readonly DatabaseService _db;
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vehicleVm;

        public RegisterViewModel(DatabaseService db, AppStateService appState, VehicleViewModel vehicleVm)
        {
            _db = db;
            _appState = appState;
            _vehicleVm = vehicleVm;
            RegisterCommand = new Command(async () => await ExecuteRegister());
        }

        private string _firstName = string.Empty;
        public string FirstName { get => _firstName; set => SetProperty(ref _firstName, value); }

        private string _lastName = string.Empty;
        public string LastName { get => _lastName; set => SetProperty(ref _lastName, value); }

        private string _login = string.Empty;
        public string Login { get => _login; set => SetProperty(ref _login, value); }

        private string _email = string.Empty;
        public string Email { get => _email; set => SetProperty(ref _email, value); }

        private string _phone = string.Empty;
        public string Phone { get => _phone; set => SetProperty(ref _phone, value); }

        private string _password = string.Empty;
        public string Password { get => _password; set => SetProperty(ref _password, value); }

        private string _confirmPassword = string.Empty;
        public string ConfirmPassword { get => _confirmPassword; set => SetProperty(ref _confirmPassword, value); }

        private int _roleIndex;
        public int RoleIndex { get => _roleIndex; set => SetProperty(ref _roleIndex, value); }

        private string _secretCode = string.Empty;
        public string SecretCode { get => _secretCode; set => SetProperty(ref _secretCode, value); }

        private string _errorMessage = string.Empty;
        public string ErrorMessage
        {
            get => _errorMessage;
            set
            {
                SetProperty(ref _errorMessage, value);
                OnPropertyChanged(nameof(HasError));
            }
        }

        public bool HasError => !string.IsNullOrEmpty(ErrorMessage);

        public ICommand RegisterCommand { get; }

        private async Task ExecuteRegister()
        {
            if (IsBusy) return;
            IsBusy = true;
            ErrorMessage = string.Empty;

            try
            {
                if (string.IsNullOrWhiteSpace(FirstName) || string.IsNullOrWhiteSpace(LastName) ||
                    string.IsNullOrWhiteSpace(Login) || string.IsNullOrWhiteSpace(Phone) ||
                    string.IsNullOrWhiteSpace(Password))
                {
                    ErrorMessage = "Заповніть всі обов'язкові поля.";
                    return;
                }

                if (Password != ConfirmPassword)
                {
                    ErrorMessage = "Паролі не збігаються.";
                    return;
                }

                if (!_vehicleVm.ValidateEmail(Email))
                {
                    ErrorMessage = _vehicleVm.LastError;
                    return;
                }

                if (!_vehicleVm.ValidatePhone(Phone))
                {
                    ErrorMessage = _vehicleVm.LastError;
                    return;
                }

                if (Password.Length < 8 || !Password.Any(char.IsUpper) || !Password.Any(char.IsDigit))
                {
                    ErrorMessage = "Пароль має бути мін. 8 символів, містити велику літеру та цифру.";
                    return;
                }

                var existing = await _db.GetUserByLoginAsync(Login);
                if (existing != null)
                {
                    ErrorMessage = "Користувач з таким логіном вже існує.";
                    return;
                }

                User newUser;

                if (SecretCode == "ADMIN123")
                {
                    newUser = new Admin(FirstName, LastName);
                }
                else if (SecretCode == "SUPER999")
                {
                    newUser = new SuperAdmin(FirstName, LastName);
                }
                else if (RoleIndex == 0)
                {
                    newUser = new Owner(FirstName, LastName, Phone, "Не вказано", DateTime.Now.AddYears(-1));
                }
                else
                {
                    newUser = new Driver(FirstName, LastName, Phone, "DRV-0000", "ТСЦ", DateTime.Now.AddYears(3), true, BloodType.A_Positive);
                }

                try { newUser.ChangeLogin(Login); } catch { }
                try { if (!string.IsNullOrWhiteSpace(Email)) newUser.ChangeEmail(Email); } catch { }
                try { newUser.ChangePhone(Phone); } catch { }

                await _db.SaveUserWithPasswordAsync(newUser, Password);
                _appState.CurrentUser = newUser;
                await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
