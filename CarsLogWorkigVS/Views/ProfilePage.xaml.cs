using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;
using CarsLogWorkigVS.Services;

namespace CarsLogWorkigVS.Views
{
    public partial class ProfilePage : ContentPage
    {
        private readonly AppStateService _appState;

        public ProfilePage(AppStateService appState)
        {
            InitializeComponent(); 
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            var user = _appState.CurrentUser;
            
            if (App.NavigationService != null)
            {
                var history = App.NavigationService.GetHistory();
                HistoryLabel.Text = string.Join(" → ", history.Select(h => h.Split('/').Last()));
            }

            if (user == null)
            {
                FullNameLabel.Text = "Гість";
                RoleLabel.Text = "Гість";
                return;
            }

            FullNameLabel.Text = user.FullName;
            RoleLabel.Text = user.Role.ToString(); 
            EmailLabel.Text = string.IsNullOrWhiteSpace(user.Email) ? "Не вказано" : user.Email;
            PhoneLabel.Text = string.IsNullOrWhiteSpace(user.Phone) ? "Не вказано" : user.Phone;
            RegDateLabel.Text = user.DateOfRegistration.ToString("dd.MM.yyyy"); 
            StatusLabel.Text = user.IsActive.ToString(); 

            if (user is Owner owner)
            {
                OwnerInfoFrame.IsVisible = true;
                AddressLabel.Text = owner.Address;
                VehicleCountLabel.Text = owner.Vehicles.Count.ToString(); 
            }

            if (user is Driver driver)
            {
                DriverInfoFrame.IsVisible = true;
                LicenseLabel.Text = driver.LicenseNumber;
                LicenseExpiryLabel.Text = driver.DateOfLicenseFormatted;
                BloodTypeLabel.Text = driver.BloodType.ToString(); 
            }
        }

        private async void OnLogoutClicked(object sender, EventArgs e)
        {
            bool confirm = await DisplayAlert("Вихід", "Вийти з акаунту?", "Так", "Скасувати"); 
            if (!confirm) return;

            _appState.CurrentUser = null;
            _appState.SelectedVehicle = null;
            await Shell.Current.GoToAsync($"//{nameof(RegistrationOrLogInPage)}"); 
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync(".."));
    }
}
