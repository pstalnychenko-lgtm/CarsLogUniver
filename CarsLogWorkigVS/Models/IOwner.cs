using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public interface IOwner : IUser
    {
        string FirstNameByOwner { get; }
        string LastNameByOwner { get; }
        string PhoneByOwner { get; }
        string AddressByOwner { get; } // Адреса власника
        DateTime DateOfPurchaseTheCar { get; } // Дата покупки авто
        string DateOfPurchaseTheCarFormatted { get; } // Форматована дата покупки авто
        List<Vehicle> Vehicles { get; } // Список транспортних засобів, якими володіє власник


        // Методи для управління транспортними засобами власника
        void AddVehicle(Vehicle vehicle); 
        void RemoveVehicle(Vehicle vehicle);


        // Методи для управління водіями, які можуть керувати транспортними засобами власника
        void AssignDriverToVehicle(Vehicle vehicle, Driver driver);
        void RemoveDriverFromVehicle(Vehicle vehicle, Driver driver);


        // Методи для управління нотатками, витратами, документами, записами про обслуговування, заправками та журналами поїздок для транспортних засобів власника
        void AddNoteToVehicle(Vehicle vehicle, Note note);
        void AddExpenseToVehicle(Vehicle vehicle, Expense expense);
        void AddDocumentToVehicle(Vehicle vehicle, Document document);
        void AddServiceRecord(Vehicle vehicle, ServiceRecord record);
        void AddFuelEntry(Vehicle vehicle, FuelEntry entry);
        void AddTripLog(Vehicle vehicle, TripLog tripLog);

        // Метод для перевірки, чи є власником певного транспортного засобу
        bool IsVehicleOwner(Vehicle vehicle);
    }
}
