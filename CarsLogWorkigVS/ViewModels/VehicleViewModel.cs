using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Text.RegularExpressions;
using CarsLogWorkig.Models;

namespace CarsLogWorkig.ViewModels
{
    public class VehicleViewModel : INotifyPropertyChanged
    {
        private const int MaxVehicles = 64;
        private readonly ObservableCollection<Vehicle> _vehicles = new ObservableCollection<Vehicle>(); 
        private readonly List<string> _actionLog = new List<string>(); 
        private readonly Dictionary<string, string> _filterPresets = new Dictionary<string, string>(); 
        private readonly Dictionary<Guid, int> _failedPinAttempts = new Dictionary<Guid, int>(); 
        private readonly HashSet<Guid> _blockedUsers = new HashSet<Guid>(); 

        private string _searchQuery = string.Empty;
        private string _lastError = string.Empty;
        private bool _isBusy;

        private readonly CarsLogWorkigVS.Database.DatabaseService _db;
        private readonly AppStateService _appState;

        public VehicleViewModel() { }

        public VehicleViewModel(CarsLogWorkigVS.Database.DatabaseService db, AppStateService appState)
        {
            _db = db;
            _appState = appState;
        }

        public async Task LoadVehiclesAsync()
        {
            if (_db == null || _appState?.CurrentUser == null) return;
            IsBusy = true;
            try
            {
                var ownerId = _appState.CurrentUser.Id.ToString();
                var entities = await _db.GetVehiclesForOwnerAsync(ownerId);
                foreach (var entity in entities)
                {
                    if (_vehicles.Any(v => v.Id.ToString() == entity.Id)) continue;
                    if (_appState.CurrentUser is Owner owner)
                    {
                        var vehicle = new Vehicle(entity.PlateNumber, entity.Vin, entity.Brand, entity.Model, entity.Color, entity.BodyType, (uint)entity.EngineVolumeCc, (FuelsType)entity.FuelType, entity.FuelTankCapacity, entity.YearOfRelease, entity.CarReleaseDate, owner);
                        vehicle.ChangeCurrentMileage((uint)entity.CurrentMileage);
                        if (!string.IsNullOrEmpty(entity.GeneralNotes)) vehicle.ChangeGeneralNotes(entity.GeneralNotes);
                        TryAddVehicle(vehicle);
                    }
                }
                OnPropertyChanged(nameof(FilteredVehicles));
            }
            finally
            {
                IsBusy = false;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public bool IsBusy
        {
            get => _isBusy;
            private set
            {
                if (_isBusy != value)
                {
                    _isBusy = value;
                    OnPropertyChanged();
                }
            }
        }

        public bool IsEmpty => _vehicles.Count == 0;

        public string LastError
        {
            get => _lastError;
            private set
            {
                if (_lastError != value)
                {
                    _lastError = value;
                    OnPropertyChanged();
                }
            }
        }

        public ObservableCollection<Vehicle> Vehicles => _vehicles; 
        
        public IEnumerable<Vehicle> FilteredVehicles => GetFilteredVehicles();

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                var val = value ?? string.Empty;
                if (_searchQuery != val)
                {
                    _searchQuery = val;
                    OnPropertyChanged();
                    OnPropertyChanged(nameof(FilteredVehicles));
                }
            }
        }

        public bool TryAddVehicle(Vehicle vehicle)
        {
            try
            {
                if (IsBusy) return false;
                IsBusy = true;

                if (vehicle == null)
                    throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 

                var brand = vehicle.Brand?.Trim(); 
                var plateNumber = vehicle.PlateNumber?.Trim(); 

                if (string.IsNullOrWhiteSpace(brand))
                    throw new ArgumentException("Марка автомобіля не може бути порожньою."); 

                if (string.IsNullOrWhiteSpace(plateNumber))
                    throw new ArgumentException("Номерний знак не може бути порожнім."); 

                if (_vehicles.Any(v => v.PlateNumber.Equals(plateNumber, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException($"Автомобіль з номером {plateNumber} вже існує."); 

                if (_vehicles.Count >= MaxVehicles)
                    throw new InvalidOperationException($"Досягнуто максимальну кількість автомобілів ({MaxVehicles})."); 

                _vehicles.Add(vehicle);
                OnPropertyChanged(nameof(IsEmpty));
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public bool TryRemoveVehicle(Guid vehicleId, bool confirmed)
        {
            try
            {
                if (!confirmed)
                    throw new InvalidOperationException("Потрібне підтвердження видалення."); 

                var vehicle = _vehicles.FirstOrDefault(v => v.Id == vehicleId); 
                if (vehicle == null)
                    throw new ArgumentException("Автомобіль не знайдено."); 

                _vehicles.Remove(vehicle);
                OnPropertyChanged(nameof(IsEmpty));
                return true;
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return false;
            }
        }

        public bool ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now)
            {
                LastError = "Дата народження не може бути в майбутньому.";
                return false;
            }
            return true;
        }

        public bool ValidateServiceDate(DateTime serviceDate)
        {
            if (serviceDate < DateTime.Now.Date)
            {
                LastError = "Дата запланованого сервісу не може бути в минулому.";
                return false;
            }
            return true;
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                LastError = "Email не може бути порожнім.";
                return false;
            }
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$"); 
            if (!emailRegex.IsMatch(email))
            {
                LastError = "Невірний формат Email.";
                return false;
            }
            return true;
        }

        public bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                LastError = "Телефон не може бути порожнім.";
                return false;
            }
            var phoneRegex = new Regex(@"^\+?[\d\s\-]{7,15}$"); 
            if (!phoneRegex.IsMatch(phone))
            {
                LastError = "Невірний формат номера телефону.";
                return false;
            }
            return true;
        }

        public bool ValidateExpenseAmount(decimal amount)
        {
            if (amount < 0)
            {
                LastError = "Сума витрат не може бути від'ємною.";
                return false;
            }
            return true;
        }

        public bool ValidateMileage(uint mileage, uint currentMileage)
        {
            if (mileage < currentMileage)
            {
                LastError = "Пробіг не може бути меншим за поточний.";
                return false;
            }
            return true;
        }

        public bool ValidateTextLength(string text, string fieldName, int maxLength)
        {
            if (text != null && text.Length > maxLength)
            {
                LastError = $"Поле '{fieldName}' перевищує максимальну довжину {maxLength} символів.";
                return false;
            }
            return true;
        }

        public IEnumerable<Vehicle> Search(string query)
        {
            if (string.IsNullOrWhiteSpace(query))
                return GetSortedVehicles(); 

            var q = query.Trim().ToLowerInvariant(); 
            return _vehicles
                .Where(v =>
                    v.Brand.ToLowerInvariant().Contains(q) ||
                    v.Model.ToLowerInvariant().Contains(q) ||
                    v.PlateNumber.ToLowerInvariant().Contains(q))
                .OrderByDescending(v => v.YearOfRelease); 
        }

        public IEnumerable<Vehicle> GetSortedVehicles()
        {
            return _vehicles.OrderByDescending(v => v.YearOfRelease); 
        }

        public string GetEmptyMessage()
        {
            if (IsEmpty)
                return "Список автомобілів порожній. Додайте перший автомобіль.";
            return string.Empty;
        }

        public Expense CreateDefaultExpense(Guid vehicleId)
        {
            return new Expense(
                category: ExpenseCategory.Other,
                amount: 0,
                date: DateTime.Now,
                description: "Новий запис",
                vehicleId: vehicleId
            ); 
        }

        public TripLog CreateDefaultTripLog(uint currentMileage)
        {
            return new TripLog(
                tripDate: DateTime.Now,
                departurePoint: "Не вказано",
                destination: "Не вказано",
                purpose: TripPurpose.Personal,
                startMileage: currentMileage,
                endMileage: currentMileage,
                notes: string.Empty
            ); 
        }

        public string FormatAmount(decimal amount) => $"{amount:N2} грн";

        public string FormatMileage(uint mileage) => $"{mileage:N0} км";

        public IEnumerable<Vehicle> GetFilteredVehicles() => Search(_searchQuery); 

        public IReadOnlyList<string> GetActionLog() => _actionLog.AsReadOnly(); 

        public void SaveFilterPreset(string presetName, string query)
        {
            if (string.IsNullOrWhiteSpace(presetName)) return;
            _filterPresets[presetName.Trim()] = query?.Trim() ?? string.Empty;
        }

        public bool ApplyFilterPreset(string presetName)
        {
            if (_filterPresets.TryGetValue(presetName, out var query))
            {
                SearchQuery = query;
                return true;
            }
            LastError = $"Пресет '{presetName}' не знайдено.";
            return false;
        }

        public List<string> GetExpiredComponentsReport(Vehicle vehicle)
        {
            if (vehicle == null) return new List<string>(); 
            return vehicle.Components
                .Where(c => c.IsExpired)
                .Select(c => $"{c.PartName} — потребує заміни (встановлено {c.InstallationDate:dd.MM.yyyy})")
                .ToList(); 
        }

        public bool CheckInactivity(User user, int inactivityHours = 24)
        {
            if (user == null) return false;
            if ((DateTime.UtcNow - user.DateOfLastActivity).TotalHours >= inactivityHours)
            {
                user.IsActive = IsActiveUser.Offline;
                return true;
            }
            return false;
        }

        public bool TryVerifyPin(User user, string enteredPin, string correctPin)
        {
            if (user == null) return false;

            if (_blockedUsers.Contains(user.Id))
            {
                LastError = "Користувача заблоковано через забагато невірних спроб.";
                return false;
            }

            if (enteredPin == correctPin)
            {
                _failedPinAttempts[user.Id] = 0;
                return true;
            }

            _failedPinAttempts.TryGetValue(user.Id, out int attempts); 
            attempts++;
            _failedPinAttempts[user.Id] = attempts;

            if (attempts >= 3)
            {
                _blockedUsers.Add(user.Id); 
                LastError = "Користувача заблоковано після 3 невірних спроб введення PIN.";
            }
            else
            {
                LastError = $"Невірний PIN. Залишилось спроб: {3 - attempts}.";
            }

            return false;
        }

        public string GenerateWeeklyReport()
        {
            try
            {
                IsBusy = true;
                var sb = new StringBuilder(); 
                var weekAgo = DateTime.Now.AddDays(-7); 

                sb.AppendLine($"Звіт за тиждень ({weekAgo:dd.MM.yyyy} – {DateTime.Now:dd.MM.yyyy})"); 
                sb.AppendLine($"Всього автомобілів: {_vehicles.Count}"); 

                decimal totalExpenses = 0;
                int totalTrips = 0;

                foreach (var v in _vehicles)
                {
                    var weekExpenses = v.Expenses
                        .Where(e => e != null)
                        .Sum(e => e.Amount); 
                    totalExpenses += weekExpenses;
                    totalTrips += v.TripLogs.Count(t => t.TripDate >= weekAgo); 
                }

                sb.AppendLine($"Загальні витрати: {FormatAmount(totalExpenses)}"); 
                sb.AppendLine($"Поїздок за тиждень: {totalTrips}"); 
                return sb.ToString(); 
            }
            catch (Exception ex)
            {
                LastError = ex.Message;
                return string.Empty;
            }
            finally
            {
                IsBusy = false;
            }
        }

        public override string ToString() =>
            $"VehicleViewModel | Автомобілів: {_vehicles.Count} | Пошук: '{_searchQuery}' | IsBusy: {IsBusy}";
    }
}