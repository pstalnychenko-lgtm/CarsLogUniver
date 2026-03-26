using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Owner : User, IOwner // клас для зберігання інформації про власника автомобіля, успадковує від класу User
    {
        private string _firstNameByOwner;
        public string FirstNameByOwner // властивість для зберігання імені власника автомобіля
        {
            get => _firstNameByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _firstNameByOwner = value;
            }
        }

        private string _lastNameByOwner;
        public string LastNameByOwner // властивість для зберігання прізвища власника автомобіля
        {
            get => _lastNameByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _lastNameByOwner = value;
            }
        }

        private string _phoneByOwner;
        public string PhoneByOwner // властивість для зберігання номера телефону власника автомобіля
        {
            get => _phoneByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _phoneByOwner = value;
            }
        }

        private string _addressByOwner;
        public string AddressByOwner // властивість для зберігання адреси власника автомобіля
        {
            get => _addressByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _addressByOwner = value;
            }
        }

        public DateTime DateOfPurchaseTheCar { get; private set; }
        public string DateOfPurchaseTheCarFormatted => DateOfPurchaseTheCar.ToString("dd.MM.yyyy"); // дата покупки автомобіля

        public Owner(string firstNameByOwner, string lastNameByOwner, string phoneByOwner, string addressByOwner,
            DateTime dateOfPurchaseTheCar) // конструктор для створення запису про власника автомобіля
        {
            FirstNameByOwner = firstNameByOwner;
            LastNameByOwner = lastNameByOwner;
            PhoneByOwner = phoneByOwner;
            AddressByOwner = addressByOwner;
            DateOfPurchaseTheCar = dateOfPurchaseTheCar;
        }

        // Дії власника 

        public void AddDriverToVehicle(Vehicle vehicle, Driver driver) // Додати водія до автомобіля
        {
            if (vehicle != null && driver != null && !vehicle.Drivers.Contains(driver))
                vehicle.Drivers.Add(driver);
        }

        public void RemoveDriverFromVehicle(Vehicle vehicle, Driver driver) // Видалити водія з автомобіля
        {
            if (vehicle != null && driver != null)
                vehicle.Drivers.Remove(driver);
        }

        public void AddNoteToVehicle(Vehicle vehicle, Note note) // Додати нотатку до автомобіля
        {
            if (vehicle != null && note != null)
                vehicle.Notess.Add(note);
        }

        public void AddExpenseToVehicle(Vehicle vehicle, Expense expense) // Додати витрату до автомобіля
        {
            if (vehicle != null && expense != null)
                vehicle.Expenses.Add(expense);
        }

        public void AddDocumentToVehicle(Vehicle vehicle, Document document) // Додати документ до автомобіля
        {
            if (vehicle != null && document != null)
                vehicle.Documents.Add(document);
        }

        public void AddServiceRecord(Vehicle vehicle, ServiceRecord record) // Додати запис про сервісне обслуговування
        {
            if (vehicle != null && record != null)
                vehicle.ServiceRecords.Add(record);
        }

        public bool IsVehicleOwner(Vehicle vehicle) // Перевірити, чи є власником конкретного авто
        {
            if (vehicle == null) return false;
            return vehicle.Owner.Id == this.Id;
        }
    }
}
