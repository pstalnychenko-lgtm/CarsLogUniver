using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Owner : User, IOwner
    {
        private string _firstNameByOwner;
        public string FirstNameByOwner
        {
            get => _firstNameByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _firstNameByOwner = value;
            }
        }

        private string _lastNameByOwner;
        public string LastNameByOwner
        {
            get => _lastNameByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _lastNameByOwner = value;
            }
        }

        private string _phoneByOwner;
        public string PhoneByOwner
        {
            get => _phoneByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _phoneByOwner = value;
            }
        }

        private string _addressByOwner;
        public string AddressByOwner
        {
            get => _addressByOwner;
            private set
            {
                if (!string.IsNullOrEmpty(value))
                    _addressByOwner = value;
            }
        }

        public DateTime DateOfPurchaseTheCar { get; private set; }
        public string DateOfPurchaseTheCarFormatted => DateOfPurchaseTheCar.ToString("dd.MM.yyyy");

        public List<Vehicle> Vehicles { get; private set; } = new List<Vehicle>();

        public Owner(string firstNameByOwner, string lastNameByOwner, string phoneByOwner,
                     string addressByOwner, DateTime dateOfPurchaseTheCar)
        {
            FirstNameByOwner = firstNameByOwner;
            LastNameByOwner = lastNameByOwner;
            PhoneByOwner = phoneByOwner;
            AddressByOwner = addressByOwner;
            DateOfPurchaseTheCar = dateOfPurchaseTheCar;
        }



        // Методи для управління транспортними засобами власника
        public void AddVehicle(Vehicle vehicle) 
        {
            if (vehicle == null)
                throw new ArgumentNullException("Автомобіль не може бути порожнім.");
            if (vehicle.Owner.Id != this.Id)
                throw new InvalidOperationException("Цей автомобіль належить іншому власнику.");
            if (!Vehicles.Contains(vehicle))
                Vehicles.Add(vehicle);
        }



        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
        public void RemoveVehicle(Vehicle vehicle)
        {
            if (vehicle == null)
                throw new ArgumentNullException("Автомобіль не може бути порожнім.");
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            Vehicles.Remove(vehicle);
        }


        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
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


        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
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


        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
        public void AddNoteToVehicle(Vehicle vehicle, Note note)
        {
            if (vehicle == null || note == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.Notess.Add(note);
        }


        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
        public void AddExpenseToVehicle(Vehicle vehicle, Expense expense)
        {
            if (vehicle == null || expense == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.Expenses.Add(expense);
        }


        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
        public void AddDocumentToVehicle(Vehicle vehicle, Document document)
        {
            if (vehicle == null || document == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.Documents.Add(document);
        }



        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
        public void AddServiceRecord(Vehicle vehicle, ServiceRecord record)
        {
            if (vehicle == null || record == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.ServiceRecords.Add(record);
        }


        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
        public void AddFuelEntry(Vehicle vehicle, FuelEntry entry)
        {
            if (vehicle == null || entry == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.FuelEntries.Add(entry);
        }


        // Методи для управління водіями, нотатками, витратами, документами та іншими аспектами автомобіля
        public void AddTripLog(Vehicle vehicle, TripLog tripLog)
        {
            if (vehicle == null || tripLog == null) return;
            if (!IsVehicleOwner(vehicle))
                throw new InvalidOperationException("Ви не є власником цього автомобіля.");
            vehicle.TripLogs.Add(tripLog);
        }

        public bool IsVehicleOwner(Vehicle vehicle) // Метод для перевірки, чи є власником конкретного автомобіля
        {
            if (vehicle == null) return false;
            return vehicle.Owner.Id == this.Id;
        }
    }
}
