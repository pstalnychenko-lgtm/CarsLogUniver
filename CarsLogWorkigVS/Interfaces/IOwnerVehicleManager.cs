using CarsLogWorkig.Models;
using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IOwnerVehicleManager
    {
        void AddFuelEntry(Vehicle vehicle, FuelEntry entry); 
        void AddServiceRecord(Vehicle vehicle, ServiceRecord record); 
        void AddTripLog(Vehicle vehicle, TripLog tripLog); 
        void AddDocumentToVehicle(Vehicle vehicle, Document document); 
        void AddExpenseToVehicle(Vehicle vehicle, Expense expense); 
        void AddNoteToVehicle(Vehicle vehicle, Note note); 
        void ChangeAddress(string newAddress); 
        void AddVehicle(Vehicle vehicle); 
        void ChangeDateOfPurchaseTheCar(DateTime newDate); 
        void AssignDriverToVehicle(Vehicle vehicle, Driver driver); 
        void RemoveDriverFromVehicle(Vehicle vehicle, Driver driver); 
        void RemoveVehicle(Vehicle vehicle); 
    }
}
