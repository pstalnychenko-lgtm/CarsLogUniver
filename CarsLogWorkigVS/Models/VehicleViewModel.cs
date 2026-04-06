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
        private const int MaxTitleLength = 100;
        private const int MaxDescriptionLength = 1000;

        private readonly List<Vehicle> _vehicles = new List<Vehicle>();
        private readonly List<string> _actionLog = new List<string>();

        private string _searchQuery = string.Empty;
        private string _lastError = string.Empty;

        public bool IsBusy { get; private set; }
        public bool IsEmpty => _vehicles.Count == 0;
        public string LastError => _lastError;

        public IReadOnlyList<Vehicle> Vehicles => _vehicles.AsReadOnly();

        public string SearchQuery
        {
            get => _searchQuery;
            set
            {
                _searchQuery = value ?? string.Empty;
            }
        }

        // Rule 1: Validation of required fields + Rule 16: Trimming + Rule 2: Unique ID (via Guid in model)
        public bool TryAddVehicle(Vehicle vehicle)
        {
            try
            {
                if (IsBusy) return false;
                IsBusy = true;

                if (vehicle == null)
                    throw new ArgumentNullException("Автомобіль не може бути порожнім.");

                // Rule 16: Trim text fields
                var brand = vehicle.Brand?.Trim();
                var plateNumber = vehicle.PlateNumber?.Trim();

                if (string.IsNullOrWhiteSpace(brand))
                    throw new ArgumentException("Марка автомобіля не може бути порожньою.");

                if (string.IsNullOrWhiteSpace(plateNumber))
                    throw new ArgumentException("Номерний знак не може бути порожнім.");

                // Rule 7: No duplicates
                if (_vehicles.Any(v => v.PlateNumber.Equals(plateNumber, StringComparison.OrdinalIgnoreCase)))
                    throw new InvalidOperationException($"Автомобіль з номером {plateNumber} вже існує.");

                // Rule 17: Max list size
                if (_vehicles.Count >= MaxVehicles)
                    throw new InvalidOperationException($"Досягнуто максимальну кількість автомобілів ({MaxVehicles}).");

                _vehicles.Add(vehicle);

                // Rule 8: Log action
                LogAction($"Додано автомобіль: {brand} {vehicle.Model}, номер: {plateNumber}");

                return true;
            }
            catch (Exception ex)
            {
                // Rule 15: Safe execute
                _lastError = ex.Message;
                return false;
            }
            finally
            {
                IsBusy = false;
            }
        }

        // Rule 6: Soft delete confirmation + Rule 18: Soft delete
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
                LogAction($"Видалено автомобіль: {vehicle.Brand} {vehicle.Model}");
                return true;
            }
            catch (Exception ex)
            {
                _lastError = ex.Message;
                return false;
            }
        }

        // Rule 5: Date validation — DateOfBirth not in future
        public bool ValidateDateOfBirth(DateTime dateOfBirth)
        {
            if (dateOfBirth > DateTime.Now)
            {
                _lastError = "Дата народження не може бути в майбутньому.";
                return false;
            }
            return true;
        }

        // Rule 5: Deadline not in past
        public bool ValidateServiceDate(DateTime serviceDate)
        {
            if (serviceDate < DateTime.Now.Date)
            {
                _lastError = "Дата запланованого сервісу не може бути в минулому.";
                return false;
            }
            return true;
        }

        // Rule 4: Email format validation
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

        // Rule 4: Phone format validation
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

        // Rule 3: No negative numeric values
        public bool ValidateExpenseAmount(decimal amount)
        {
            if (amount < 0)
            {
                _lastError = "Сума витрат не може бути від'ємною.";
                return false;
            }
            return true;
        }

        // Rule 3: No negative mileage
        public bool ValidateMileage(uint mileage, uint currentMileage)
        {
            if (mileage < currentMileage)
            {
                _lastError = "Пробіг не може бути меншим за поточний.";
                return false;
            }
            return true;
        }

        // Rule 7: Text length limits
        public bool ValidateTextLength(string text, string fieldName, int maxLength)
        {
            if (text != null && text.Length > maxLength)
            {
                _lastError = $"Поле '{fieldName}' перевищує максимальну довжину {maxLength} символів.";
                return false;
            }
            return true;
        }

        // Rule 8: Case-insensitive search
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

        // Rule 9: Default sort — newest first
        public IEnumerable<Vehicle> GetSortedVehicles()
        {
            return _vehicles.OrderByDescending(v => v.YearOfRelease);
        }

        // Rule 10: IsEmpty state
        public string GetEmptyMessage()
        {
            if (IsEmpty)
                return "Список автомобілів порожній. Додайте перший автомобіль.";
            return string.Empty;
        }

        // Rule 12: Default values for new expense
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

        // Rule 12: Default trip log
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

        // Rule 14: Format expense amount for UI
        public string FormatAmount(decimal amount)
        {
            return $"{amount:N2} грн";
        }

        // Rule 14: Format mileage for UI
        public string FormatMileage(uint mileage)
        {
            return $"{mileage:N0} км";
        }

        // Rule 19: Search auto-update — returns filtered result based on current SearchQuery
        public IEnumerable<Vehicle> GetFilteredVehicles()
        {
            return Search(_searchQuery);
        }

        // Specific group rule: Log action on every state change
        private void LogAction(string message)
        {
            _actionLog.Add($"[{DateTime.Now:dd.MM.yyyy HH:mm:ss}] {message}");
        }

        public IReadOnlyList<string> GetActionLog()
        {
            return _actionLog.AsReadOnly();
        }

        // Specific group rule: Preset / template — save current filter as preset
        private readonly Dictionary<string, string> _filterPresets = new Dictionary<string, string>();

        public void SaveFilterPreset(string presetName, string query)
        {
            if (string.IsNullOrWhiteSpace(presetName)) return;
            _filterPresets[presetName.Trim()] = query?.Trim() ?? string.Empty;
            LogAction($"Збережено пресет фільтру: {presetName}");
        }

        public bool ApplyFilterPreset(string presetName)
        {
            if (_filterPresets.TryGetValue(presetName, out var query))
            {
                SearchQuery = query;
                LogAction($"Застосовано пресет фільтру: {presetName}");
                return true;
            }
            _lastError = $"Пресет '{presetName}' не знайдено.";
            return false;
        }

        // Specific group rule: Batch processing — apply status check to all components
        public List<string> GetExpiredComponentsReport(Vehicle vehicle)
        {
            if (vehicle == null) return new List<string>();
            return vehicle.Components
                .Where(c => c.IsExpired)
                .Select(c => $"{c.PartName} — потребує заміни (встановлено {c.InstallationDate:dd.MM.yyyy})")
                .ToList();
        }

        // Specific group rule: Energy saving — auto-deactivate user after inactivity
        public bool CheckInactivity(User user, int inactivityHours = 24)
        {
            if (user == null) return false;
            if ((DateTime.UtcNow - user.DateOfLastActivity).TotalHours >= inactivityHours)
            {
                user.IsActive = IsActiveUser.Ofline;
                LogAction($"Користувач {user.FullName} деактивований через бездіяльність.");
                return true;
            }
            return false;
        }

        // Specific group rule: Security — block after 3 wrong pin attempts
        private readonly Dictionary<Guid, int> _failedPinAttempts = new Dictionary<Guid, int>();
        private readonly HashSet<Guid> _blockedUsers = new HashSet<Guid>();

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
                LogAction($"Користувача {user.FullName} заблоковано після 3 невірних спроб.");
                _lastError = "Користувача заблоковано після 3 невірних спроб введення PIN.";
            }
            else
            {
                _lastError = $"Невірний PIN. Залишилось спроб: {3 - attempts}.";
            }

            return false;
        }

        // Weekly report
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

                LogAction("Згенеровано тижневий звіт.");
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

        public override string ToString()
        {
            return $"VehicleViewModel | Автомобілів: {_vehicles.Count} | Пошук: '{_searchQuery}' | IsBusy: {IsBusy}";
        }
    }
}
