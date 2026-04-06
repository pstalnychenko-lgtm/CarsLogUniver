using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Owner : User, IOwner
    {
        private string _addressByOwner = string.Empty;
        public string AddressByOwner
        {
            get => _addressByOwner;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Адреса не може бути порожньою.");
                if (value.Trim().Length > 200)
                    throw new ArgumentException("Адреса не може перевищувати 200 символів.");
                _addressByOwner = value.Trim();
            }
        }

        public DateTime DateOfPurchaseTheCar { get; private set; }
        public string DateOfPurchaseTheCarFormatted => DateOfPurchaseTheCar.ToString("dd.MM.yyyy");

        public List<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        public Owner(string firstName, string lastName, string phone,
                     string addressByOwner, DateTime dateOfPurchaseTheCar)
        {
            FirstName = firstName;
            LastName = lastName;
            Phone = phone;
            AddressByOwner = addressByOwner;

            if (dateOfPurchaseTheCar > DateTime.Now)
                throw new ArgumentException("Дата купівлі авто не може бути в майбутньому.");
            DateOfPurchaseTheCar = dateOfPurchaseTheCar;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("Автомобіль не може бути порожнім.");
            if (vehicle.Owner.Id != this.Id)
                throw new InvalidOperationException("Цей автомобіль належить іншому власнику.");
            if (!Vehicles.Contains(vehicle))
                Vehicles.Add(vehicle);
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("Автомобіль не може бути порожнім.");
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            Vehicles.Remove(vehicle);
        }

        public void AssignDriverToVehicle(Vehicle vehicle, Driver driver)
        {
            if (vehicle == null)
                throw new ArgumentNullException("Автомобіль не може бути порожнім.");
            if (driver == null)
                throw new ArgumentNullException("Водій не може бути порожнім.");
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            if (!driver.IsLicenseValid())
                throw new InvalidOperationException("Посвідчення водія прострочене.");
            if (!vehicle.Drivers.Contains(driver))
                vehicle.Drivers.Add(driver);
        }

        public void RemoveDriverFromVehicle(Vehicle vehicle, Driver driver)
        {
            if (vehicle == null)
                throw new ArgumentNullException("Автомобіль не може бути порожнім.");
            if (driver == null)
                throw new ArgumentNullException("Водій не може бути порожнім.");
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.Drivers.Remove(driver);
        }

        public void AddNoteToVehicle(Vehicle vehicle, Note note)
        {
            if (vehicle == null || note == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.Notess.Add(note);
        }

        public void AddExpenseToVehicle(Vehicle vehicle, Expense expense)
        {
            if (vehicle == null || expense == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.Expenses.Add(expense);
        }

        public void AddDocumentToVehicle(Vehicle vehicle, Document document)
        {
            if (vehicle == null || document == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.Documents.Add(document);
        }

        public void AddServiceRecord(Vehicle vehicle, ServiceRecord record)
        {
            if (vehicle == null || record == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.ServiceRecords.Add(record);
        }

        public void AddFuelEntry(Vehicle vehicle, FuelEntry entry)
        {
            if (vehicle == null || entry == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.FuelEntries.Add(entry);
        }

        public void AddTripLog(Vehicle vehicle, TripLog tripLog)
        {
            if (vehicle == null || tripLog == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.TripLogs.Add(tripLog);
        }

        public bool IsVehicleOwner(Vehicle vehicle)
        {
            if (vehicle == null) return false;
            return vehicle.Owner.Id == this.Id;
        }

        public override string ToString() =>
            $"[Власник] {FullName} | Адреса: {_addressByOwner} | Авто: {Vehicles.Count} шт.";
    }
}
