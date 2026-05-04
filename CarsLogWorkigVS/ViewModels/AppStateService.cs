using CarsLogWorkig.Models;

namespace CarsLogWorkig.ViewModels
{
    public class AppStateService
    {
        public User? CurrentUser { get; set; }
        public User? RealUser { get; set; }
        public User? ViewingUser { get; set; }
        public Vehicle? SelectedVehicle { get; set; }

        public bool IsLoggedIn => CurrentUser != null;
        public bool IsViewingAsOther => RealUser != null;

        public bool IsOwner => CurrentUser is Owner;
        public bool IsDriver => CurrentUser is Driver;
        public bool IsAdmin => CurrentUser is Admin;
        public bool IsSuperAdmin => CurrentUser is SuperAdmin;

        public bool CanManageVehicles => IsOwner || IsAdmin || IsSuperAdmin;
        public bool CanAddFuelAndTrips => IsOwner || IsDriver;
        public bool CanViewReports => IsOwner || IsAdmin || IsSuperAdmin;
        public bool CanManageDrivers => IsOwner || IsAdmin || IsSuperAdmin;
        public bool CanAccessAdminPanel => IsAdmin || IsSuperAdmin;
        public bool CanViewDocumentsAndNotes => IsOwner || IsDriver;
        public bool CanViewExpenses => IsOwner || IsAdmin || IsSuperAdmin;
    }
}
