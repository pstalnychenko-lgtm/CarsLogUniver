using CarsLogWorkig.Models;
using CarsLogWorkig.ViewModels;

namespace CarsLogWorkigVS.Views
{
    public partial class AdminPanelPage : ContentPage
    {
        private readonly AppStateService _appState;
        private Admin? _admin;
        private SuperAdmin? _superAdmin;

        public AdminPanelPage(AppStateService appState)
        {
            InitializeComponent(); 
            _appState = appState;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing(); 
            var user = _appState.CurrentUser;

            if (user is SuperAdmin sa)
            {
                _superAdmin = sa;
                _admin = sa;
                AdminContent.IsVisible = true;
                AccessDeniedFrame.IsVisible = false;
                AdminInfoLabel.Text = $"SuperAdmin: {sa.FullName}";
                SuperAdminSection.IsVisible = true;
                SuperAdminFrame.IsVisible = true;
            }
            else if (user is Admin a)
            {
                _admin = a;
                AdminContent.IsVisible = true;
                AccessDeniedFrame.IsVisible = false;
                AdminInfoLabel.Text = $"Admin: {a.FullName}";
            }
            else
            {
                AdminContent.IsVisible = false;
                AccessDeniedFrame.IsVisible = true;
            }
        }

        private async void OnActivateUserTapped(object sender, TappedEventArgs e)
        {
            if (_admin == null) return;
            var name = await DisplayPromptAsync("Активація", "ПІБ користувача (для демонстрації):"); 
            if (string.IsNullOrWhiteSpace(name)) return;

            var mockUser = new Owner(name.Split(' ').First(), name.Split(' ').Last(), "+380000000000", "Не вказано", DateTime.Now.AddYears(-1)); 
            _admin.ActivateUser(mockUser); 
            ShowResult($"Користувача '{mockUser.FullName}' активовано. Статус: {mockUser.IsActive}"); 
        }

        private async void OnDeactivateUserTapped(object sender, TappedEventArgs e)
        {
            if (_admin == null) return;
            var name = await DisplayPromptAsync("Деактивація", "ПІБ користувача (для демонстрації):"); 
            if (string.IsNullOrWhiteSpace(name)) return;

            var mockUser = new Driver("Тест", "Водій", "+380000000000", "DRV-000", "ТСЦ", DateTime.Now.AddYears(2), true, BloodType.A_Positive); 
            try
            {
                _admin.DeactivateUser(mockUser); 
                ShowResult($"Користувача деактивовано. Статус: {mockUser.IsActive}"); 
            }
            catch (Exception ex)
            {
                ShowResult(ex.Message, isError: true); 
            }
        }

        private async void OnAssignRoleTapped(object sender, TappedEventArgs e)
        {
            if (_admin == null) return;
            string role = await DisplayActionSheet("Оберіть роль", "Скасувати", null, "Owner", "Driver"); 
            if (role == null || role == "Скасувати") return;

            var mockUser = new Driver("Тест", "Користувач", "+380000000000", "DRV-001", "ТСЦ", DateTime.Now.AddYears(2), true, BloodType.B_Positive); 
            var newRole = role == "Owner" ? UserRole.Owner : UserRole.Driver;
            _admin.AssignRole(mockUser, newRole); 
            ShowResult($"Роль '{newRole}' призначено успішно."); 
        }

        private async void OnCreateAdminTapped(object sender, TappedEventArgs e)
        {
            if (_superAdmin == null) return;
            var name = await DisplayPromptAsync("Новий адмін", "ПІБ (для демонстрації):"); 
            if (string.IsNullOrWhiteSpace(name)) return;

            var parts = name.Trim().Split(' '); 
            var mockUser = new Driver(parts.First(), parts.LastOrDefault() ?? "User", "+380000000000", "DRV-099", "ТСЦ", DateTime.Now.AddYears(2), true, BloodType.O_Positive); 
            _superAdmin.CreateAdmin(mockUser); 
            ShowResult($"Роль Admin надано: {mockUser.FullName} — {mockUser.Role}"); 
        }

        private async void OnRemoveAdminTapped(object sender, TappedEventArgs e)
        {
            if (_superAdmin == null) return;
            bool confirm = await DisplayAlert("Підтвердження", "Зняти права адміна?", "Так", "Скасувати"); 
            if (!confirm) return;

            var mockAdmin = new Driver("Адмін", "Тест", "+380000000000", "DRV-100", "ТСЦ", DateTime.Now.AddYears(2), true, BloodType.A_Negative); 
            mockAdmin.ChangeRole(UserRole.Admin); 
            _superAdmin.RemoveAdmin(mockAdmin); 
            ShowResult($"Права знято. Поточна роль: {mockAdmin.Role}"); 
        }

        private void ShowResult(string msg, bool isError = false)
        {
            ResultLabel.Text = msg;
            ResultLabel.TextColor = isError
                ? Microsoft.Maui.Graphics.Color.FromArgb("#FF3B30")
                : Microsoft.Maui.Graphics.Color.FromArgb("#34C759"); 
            ResultLabel.IsVisible = true;
        }

        private async void OnBackClicked(object sender, EventArgs e) =>
            await (App.NavigationService?.GoBackAsync() ?? Shell.Current.GoToAsync("..")); 
    }
}
