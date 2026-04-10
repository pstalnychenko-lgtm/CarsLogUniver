using CarsLogWorkig.Models;

namespace CarsLogWorkig.ViewModels
{
    public class AppStateService
    {
        public User? CurrentUser { get; set; }
        public Vehicle? SelectedVehicle { get; set; }

        public bool IsLoggedIn => CurrentUser != null;

        public bool IsOwner => CurrentUser is Owner;
        public bool IsDriver => CurrentUser is Driver;
        public bool IsAdmin => CurrentUser is Admin;
        public bool IsSuperAdmin => CurrentUser is SuperAdmin;
    }
}
