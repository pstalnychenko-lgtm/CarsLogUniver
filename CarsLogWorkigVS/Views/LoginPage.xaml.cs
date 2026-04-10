using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class LoginPage : ContentPage
    {
        private readonly AppStateService _appState;

        public LoginPage(AppStateService appState)
        {
            InitializeComponent();
            _appState = appState;
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

            var owner = new Owner("Іван", "Петренко", "+380671234567", "м. Київ", DateTime.Now.AddYears(-2));
            try
            {
                owner.ChangeLogin(login);
            }
            catch { }

            _appState.CurrentUser = owner;

            await Shell.Current.GoToAsync($"//{nameof(DashboardPage)}");
        }

        private async void OnBackClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync("..");
        }

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
