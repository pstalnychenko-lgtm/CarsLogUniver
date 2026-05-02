using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Database;

namespace CarsLogWorkigVS.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly AppStateService _appState;
        private readonly DatabaseService _db;

        public LoginPage(AppStateService appState, DatabaseService db)
        {
            InitializeComponent();
            _appState = appState;
            _db = db;
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            var login = LoginEntry.Text?.Trim();
            var password = PasswordEntry.Text;

            if (string.IsNullOrWhiteSpace(login) || string.IsNullOrWhiteSpace(password))
            {
                ShowError("Заповніть усі поля.");
                return;
            }

            var user = await _db.GetUserByLoginAsync(login);

            if (user == null)
            {
                ShowError("Користувача з таким логіном не знайдено.");
                return;
            }

            var passwordValid = await _db.VerifyPasswordAsync(login, password);
            if (!passwordValid)
            {
                ShowError("Невірний пароль.");
                return;
            }

            _appState.CurrentUser = user;
            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));

        private async void OnGoToRegisterTapped(object sender, TappedEventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage));
        }

        private void ShowError(string message)
        {
            ErrorLabel.Text = message;
            ErrorLabel.IsVisible = true;
        }
    }
}
