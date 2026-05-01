using CarsLogWorkig.Models;
using System;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IWorkedWithVehecle
    {
        void ChangePlateNumber(string newPlateNumber);   
        void ChangeCurrentMileage(uint newMileage);  
        void ChangeVin(string newVin);   
        void ChangeBrand(string newBrand);   
        void ChangeBodyType(string newBodyType);   
        void ChangeColor(string newColor);   
        void ChangeModel(string newModel);   
        void ChangeEngineVolumeCc(uint newEngineVolumeCc);   
        void ChangeFuelTankCapacity(decimal newCapacity);   
        void ChangeFuelType(FuelsType newFuelType);   
        void ChangeYearOfRelease(DateTime newYear);   
        void ChangeCarReleaseDate(DateTime newDate);   
        void ChangeGeneralNotes(string newNotes);   
        uint GetTotalDistance();   
        List<VehicleComponent> Components { get; }
        List<Document> Documents { get; }
        List<Driver> Drivers { get; }
        List<Expense> Expenses { get; }
        decimal GetTotalExpenses();   
        List<FuelEntry> FuelEntries { get; }
        List<Note> Notes { get; }
        void ChangeOwner(Owner newOwner);   
        List<ServiceRecord> ServiceRecords { get; }
        List<TripLog> TripLogs { get; }
    }
}
