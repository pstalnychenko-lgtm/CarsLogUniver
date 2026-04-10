using CarsLogWorkigVS.Views;

namespace CarsLogWorkigVS
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(RegistrationOrLogInPage), typeof(RegistrationOrLogInPage));
            Routing.RegisterRoute(nameof(LoginPage), typeof(LoginPage));
            Routing.RegisterRoute(nameof(RegisterPage), typeof(RegisterPage));
            Routing.RegisterRoute(nameof(DashboardPage), typeof(DashboardPage));
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
    }
}
