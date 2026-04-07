using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using CarsLogWorkig.Models;

namespace CarsLogWorkig.ViewModels
{
    public class VehicleViewModel
    {
        private const int MaxVehicles = 50;

        private readonly List<Vehicle> _vehicles = new List<Vehicle>();
        private readonly List<string> _actionLog = new List<string>();
        private readonly Dictionary<string, string> _filterPresets = new Dictionary<string, string>();
        private readonly Dictionary<Guid, int> _failedPinAttempts = new Dictionary<Guid, int>();
        private readonly HashSet<Guid> _blockedUsers = new HashSet<Guid>();

        private string _searchQuery = string.Empty;
        private string _lastError = string.Empty;

        public bool IsBusy { get; private set; }
        public bool IsEmpty => _vehicles.Count == 0;
        public string LastError => _lastError;
        public IReadOnlyList<Vehicle> Vehicles => _vehicles.AsReadOnly();

        public string SearchQuery
        {
            get => _searchQuery;
            set { _searchQuery = value ?? string.Empty; }
        }

        public bool TryAddVehicle(Vehicle vehicle)
        {
            try
            {
                if (IsBusy) return false;
                IsBusy = true;

                if (vehicle == null)
                    throw new ArgumentNullException("Автомобіль не може бути порожнім.");

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
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
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
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        public bool ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now)
            {
                _lastError = "Дата народження не може бути в майбутньому.";
                return false;
            }
            return true;
        }

        public bool ValidateServiceDate(DateTime serviceDate)
        {
            if (serviceDate < DateTime.Now.Date)
            {
                _lastError = "Дата запланованого сервісу не може бути в минулому.";
                return false;
            }
            return true;
        }

        public bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
            {
                _lastError = "Email не може бути порожнім.";
                return false;
            }
            var emailRegex = new Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
            {
                _lastError = "Невірний формат Email.";
                return false;
            }
            return true;
        }

        public bool ValidatePhone(string phone)
        {
            if (string.IsNullOrWhiteSpace(phone))
            {
                _lastError = "Телефон не може бути порожнім.";
                return false;
            }
            var phoneRegex = new Regex(@"^\+?[\d\s\-]{7,15}$");
            if (!phoneRegex.IsMatch(phone))
            {
                _lastError = "Невірний формат номера телефону.";
                return false;
            }
            return true;
        }

        public bool ValidateExpenseAmount(decimal amount)
        {
            if (amount < 0)
            {
                _lastError = "Сума витрат не може бути від'ємною.";
                return false;
            }
            return true;
        }

        public bool ValidateMileage(uint mileage, uint currentMileage)
        {
            if (mileage < currentMileage)
            {
                _lastError = "Пробіг не може бути меншим за поточний.";
                return false;
            }
            return true;
        }

        public bool ValidateTextLength(string text, string fieldName, int maxLength)
        {
            if (text != null && text.Length > maxLength)
            {
                _lastError = $"Поле '{fieldName}' перевищує максимальну довжину {maxLength} символів.";
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

        public TripLog CreateDefaultTripLog()
        {
            return new TripLog(
                tripDate: DateTime.Now,
                departurePoint: string.Empty,
                destination: string.Empty,
                purpose: TripPurpose.Personal,
                startMileage: 0,
                endMileage: 0,
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
            _lastError = $"Пресет '{presetName}' не знайдено.";
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
                _lastError = "Користувача заблоковано через забагато невірних спроб.";
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
                _lastError = "Користувача заблоковано після 3 невірних спроб введення PIN.";
            }
            else
            {
                _lastError = $"Невірний PIN. Залишилось спроб: {3 - attempts}.";
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
                _lastError = ex.Message;
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
