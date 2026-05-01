namespace CarsLogWorkigVS.Views
{
    public partial class RegistrationOrLogInPage : ContentPage
    {
        public RegistrationOrLogInPage()
        {
            InitializeComponent(); 
        }

        private async void OnLoginClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(LoginPage)); 
        }

        private async void OnRegisterClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(RegisterPage)); 
        }

        private async void OnGuestClicked(object sender, EventArgs e)
        {
            await Shell.Current.GoToAsync(nameof(DashboardPage)); 
        }
    }
}
