using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public interface IOwner : IUser
    {
        string AddressByOwner { get; }
        DateTime DateOfPurchaseTheCar { get; }
        string DateOfPurchaseTheCarFormatted { get; }
        List<Vehicle> Vehicles { get; }

        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);

        void AssignDriverToVehicle(Vehicle vehicle, Driver driver);
        void RemoveDriverFromVehicle(Vehicle vehicle, Driver driver);

        void AddNoteToVehicle(Vehicle vehicle, Note note);
        void AddExpenseToVehicle(Vehicle vehicle, Expense expense);
        void AddDocumentToVehicle(Vehicle vehicle, Document document);
        void AddServiceRecord(Vehicle vehicle, ServiceRecord record);
        void AddFuelEntry(Vehicle vehicle, FuelEntry entry);
        void AddTripLog(Vehicle vehicle, TripLog tripLog);

        bool IsVehicleOwner(Vehicle vehicle);
    }
}
