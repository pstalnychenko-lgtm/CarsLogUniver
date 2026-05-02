using System.ComponentModel;
using System.Runtime.CompilerServices;
using CarsLogWorkig.Models;

namespace CarsLogWorkig.ViewModels
{
    public class DashboardViewModel : INotifyPropertyChanged
    {
        private readonly AppStateService _appState;
        private readonly VehicleViewModel _vehicleViewModel;

        private decimal _totalExpenses;
        private decimal _totalFuelCost;
        private Vehicle _selectedVehicle;
        private string _greeting;
        private int _vehicleCount;
        private bool _hasLastVehicle;
        private string _lastVehicleName;
        private string _lastVehiclePlate;
        private string _lastVehicleMileage;

        public event PropertyChangedEventHandler PropertyChanged;

        public DashboardViewModel(AppStateService appState, VehicleViewModel vehicleViewModel)
        {
            _appState = appState;
            _vehicleViewModel = vehicleViewModel;
        }

        public decimal TotalExpenses
        {
            get => _totalExpenses;
            set { _totalExpenses = value; OnPropertyChanged(); }
        }

        public decimal TotalFuelCost
        {
            get => _totalFuelCost;
            set { _totalFuelCost = value; OnPropertyChanged(); }
        }

        public Vehicle SelectedVehicle
        {
            get => _selectedVehicle;
            set { _selectedVehicle = value; OnPropertyChanged(); }
        }

        public string Greeting
        {
            get => _greeting;
            set { _greeting = value; OnPropertyChanged(); }
        }

        public int VehicleCount
        {
            get => _vehicleCount;
            set { _vehicleCount = value; OnPropertyChanged(); }
        }

        public bool HasLastVehicle
        {
            get => _hasLastVehicle;
            set 
            { 
                _hasLastVehicle = value; 
                OnPropertyChanged(); 
                OnPropertyChanged(nameof(HasNoVehicles));
            }
        }
        
        public bool HasNoVehicles => !HasLastVehicle;

        public string LastVehicleName
        {
            get => _lastVehicleName;
            set { _lastVehicleName = value; OnPropertyChanged(); }
        }

        public string LastVehiclePlate
        {
            get => _lastVehiclePlate;
            set { _lastVehiclePlate = value; OnPropertyChanged(); }
        }

        public string LastVehicleMileage
        {
            get => _lastVehicleMileage;
            set { _lastVehicleMileage = value; OnPropertyChanged(); }
        }

        public void LoadData()
        {
            if (_appState.IsLoggedIn)
                Greeting = $"Вітаємо, {_appState.CurrentUser?.FullName}!";
            else
                Greeting = "Вітаємо, Гість!";

            var vehicles = _vehicleViewModel.Vehicles;
            VehicleCount = vehicles?.Count ?? 0;

            if (vehicles != null && vehicles.Count > 0)
            {
                SelectedVehicle = vehicles[^1];
                LastVehicleName = $"{SelectedVehicle.Brand} {SelectedVehicle.Model}";
                LastVehiclePlate = SelectedVehicle.PlateNumber;
                LastVehicleMileage = _vehicleViewModel.FormatMileage(SelectedVehicle.CurrentMileage);
                HasLastVehicle = true;
            }
            else
            {
                SelectedVehicle = null;
                LastVehicleName = string.Empty;
                LastVehiclePlate = string.Empty;
                LastVehicleMileage = string.Empty;
                HasLastVehicle = false;
            }
        }

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
