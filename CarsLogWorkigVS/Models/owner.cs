using CarsLogWorkigVS.Interfaces;
using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Owner : User, IManageDetailsWithOwner 
    {
        public DateTime DateOfPurchaseTheCar { get; private set; }
        public string DateOfPurchaseTheCarFormatted => DateOfPurchaseTheCar.ToString("dd.MM.yyyy"); 

        public void ChangeDateOfPurchaseTheCar(DateTime newDate)
        {
            if (newDate > DateTime.Now)
                throw new ArgumentException("Дата покупки не може бути в майбутньому."); 
            if (DateOfPurchaseTheCar == newDate)
                throw new ArgumentException("Ця дата покупки вже встановлена."); 
            DateOfPurchaseTheCar = newDate;
        }

        public List<Vehicle> Vehicles { get; private set; } = new List<Vehicle>(); 

        public Owner(string firstName, string lastName, string phone,
                     string addressByOwner, DateTime dateOfPurchaseTheCar)
        {
            ChangeFirstName(firstName); 
            ChangeLastName(lastName); 
            ChangePhone(phone); 
            ChangeAddress(addressByOwner); 

            if (dateOfPurchaseTheCar > DateTime.Now)
                throw new ArgumentException("Дата купівлі авто не може бути в майбутньому."); 
            DateOfPurchaseTheCar = dateOfPurchaseTheCar;
        }

        public void AddVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (vehicle.Owner.Id != this.Id)
                throw new InvalidOperationException("Цей автомобіль належить іншому власнику."); 
            if (!Vehicles.Contains(vehicle))
                Vehicles.Add(vehicle); 
        }

        public void RemoveVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля."); 
            Vehicles.Remove(vehicle); 
        }

        public void AssignDriverToVehicle(Vehicle vehicle, Driver driver)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (driver == null)
                throw new ArgumentNullException(nameof(driver), "Водій не може бути порожнім."); 
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
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (driver == null)
                throw new ArgumentNullException(nameof(driver), "Водій не може бути порожнім."); 
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля."); 
            vehicle.Drivers.Remove(driver); 
        }

        public void AddNoteToVehicle(Vehicle vehicle, Note note)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (note == null)
                throw new ArgumentNullException(nameof(note), "Нотатка не може бути порожньою."); 
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля."); 
            vehicle.Notes.Add(note); 
        }

        public void AddExpenseToVehicle(Vehicle vehicle, Expense expense)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (expense == null)
                throw new ArgumentNullException(nameof(expense), "Витрата не може бути порожньою."); 
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля."); 
            vehicle.Expenses.Add(expense); 
        }

        public void AddDocumentToVehicle(Vehicle vehicle, Document document)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (document == null)
                throw new ArgumentNullException(nameof(document), "Документ не може бути порожнім."); 
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля."); 
            vehicle.Documents.Add(document); 
        }

        public void AddServiceRecord(Vehicle vehicle, ServiceRecord record)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (record == null)
                throw new ArgumentNullException(nameof(record), "Запис обслуговування не може бути порожнім."); 
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля."); 
            vehicle.ServiceRecords.Add(record); 
        }

        public void AddFuelEntry(Vehicle vehicle, FuelEntry entry)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (entry == null)
                throw new ArgumentNullException(nameof(entry), "Запис заправки не може бути порожнім."); 
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля."); 
            vehicle.FuelEntries.Add(entry); 
        }

        public void AddTripLog(Vehicle vehicle, TripLog tripLog)
        {
            if (vehicle == null)
                throw new ArgumentNullException(nameof(vehicle), "Автомобіль не може бути порожнім."); 
            if (tripLog == null)
                throw new ArgumentNullException(nameof(tripLog), "Журнал поїздки не може бути порожнім."); 
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
            $"[Власник] {FullName} | Адреса: {Address} | Авто: {Vehicles.Count} шт.";
    }
}
