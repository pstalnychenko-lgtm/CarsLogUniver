using CarsLogWorkigVS.Views;

namespace CarsLogWorkigVS
{
    public partial class AppShell : Shell
    {
        private CarsLogWorkig.ViewModels.AppStateService? _appState => 
            Handler?.MauiContext?.Services.GetService<CarsLogWorkig.ViewModels.AppStateService>();

        public AppShell()
        {
            InitializeComponent(); 

            App.NavigationService?.SubscribeToNavigated();

            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage)); 
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage)); 
            Routing.RegisterRoute(nameof(VehicleListPage), typeof(VehicleListPage)); 
            Routing.RegisterRoute(nameof(VehicleDetailPage), typeof(VehicleDetailPage)); 
            Routing.RegisterRoute(nameof(AddVehiclePage), typeof(AddVehiclePage)); 
            Routing.RegisterRoute(nameof(FuelEntriesPage), typeof(FuelEntriesPage)); 
            Routing.RegisterRoute(nameof(AddFuelEntryPage), typeof(AddFuelEntryPage)); 
            Routing.RegisterRoute(nameof(TripLogsPage), typeof(TripLogsPage)); 
            Routing.RegisterRoute(nameof(AddTripLogPage), typeof(AddTripLogPage)); 
            Routing.RegisterRoute(nameof(ServiceRecordsPage), typeof(ServiceRecordsPage)); 
            Routing.RegisterRoute(nameof(AddServiceRecordPage), typeof(AddServiceRecordPage)); 
            Routing.RegisterRoute(nameof(ExpensesPage), typeof(ExpensesPage)); 
            Routing.RegisterRoute(nameof(AddExpensePage), typeof(AddExpensePage)); 
            Routing.RegisterRoute(nameof(DocumentsPage), typeof(DocumentsPage)); 
            Routing.RegisterRoute(nameof(AddDocumentPage), typeof(AddDocumentPage)); 
            Routing.RegisterRoute(nameof(NotesPage), typeof(NotesPage)); 
            Routing.RegisterRoute(nameof(AddNotePage), typeof(AddNotePage)); 
            Routing.RegisterRoute(nameof(DriversPage), typeof(DriversPage)); 
            Routing.RegisterRoute(nameof(AddDriverPage), typeof(AddDriverPage)); 
            Routing.RegisterRoute(nameof(ComponentsPage), typeof(ComponentsPage)); 
            Routing.RegisterRoute(nameof(ProfilePage), typeof(ProfilePage)); 
            Routing.RegisterRoute(nameof(ReportPage), typeof(ReportPage)); 
            Routing.RegisterRoute(nameof(AdminPanelPage), typeof(AdminPanelPage)); 
        }

        protected override void OnNavigated(ShellNavigatedEventArgs args)
        {
            base.OnNavigated(args);
            UpdateAdminBanner();
        }

        private void UpdateAdminBanner()
        {
            var state = _appState;
            if (state == null) return;
            bool isViewing = state.IsViewingAsOther;
            AdminBanner.IsVisible = isViewing;
        }

        internal async void OnReturnToAdminClicked(object sender, EventArgs e)
        {
            var state = _appState;
            if (state == null || state.RealUser == null) return;

            string password = await DisplayPromptAsync("Повернення", "Введіть пароль Супер Адміна для підтвердження:", "ОК", "Скасувати", "Пароль", -1, Keyboard.Numeric, "");
            
            if (string.IsNullOrEmpty(password)) return;

            var db = Handler?.MauiContext?.Services.GetService<CarsLogWorkigVS.Database.DatabaseService>();
            if (db != null)
            {
                bool isValid = await db.VerifyPasswordAsync(state.RealUser.Login, password);
                if (!isValid)
                {
                    await DisplayAlert("Помилка", "Невірний пароль. Спробуйте ще раз.", "ОК");
                    return;
                }
            }

            state.CurrentUser = state.RealUser;
            state.RealUser = null;
            
            UpdateAdminBanner();
            await GoToAsync($"//{nameof(DashboardPage)}");
            await DisplayAlert("Повернення", $"Ви знову в сесії: {state.CurrentUser.FullName}", "ОК");
        }
    }
}
