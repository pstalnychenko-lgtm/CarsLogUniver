using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public interface IVehicle
    {
        Guid Id { get; }
        string PlateNumber { get; }
        string Vin { get; }
        string Brand { get; }
        string Model { get; }
        string Color { get; }
        string BodyType { get; }
        uint EngineVolumeCc { get; }
        decimal FuelTankCapacity { get; }
        FuelsType FuelType { get; }
        DateTime YearOfRelease { get; }
        DateTime CarReleaseDate { get; }
        uint CurrentMileage { get; set; }
        string Notes { get; set; }
        Owner Owner { get; }

        List<Driver> Drivers { get; }
        List<Document> Documents { get; }
        List<FuelEntry> FuelEntries { get; }
        List<ServiceRecord> ServiceRecords { get; }
        List<VehicleComponent> Components { get; }
        List<Note> Notess { get; }
        List<TripLog> TripLogs { get; }
        List<Expense> Expenses { get; }

        uint GetTotalDistance();
        decimal GetTotalExpenses();
        string ToString();

        void ChangePlateNumber(string newPlateNumber);
        void ChangeVin(string newVin);
        void ChangeBrand(string newBrand);
        void ChangeModel(string newModel);
        void ChangeColor(string newColor);
        void ChangeBodyType(string newBodyType);
        void ChangeEngineVolumeCc(uint newEngineVolumeCc);
        void ChangeFuelTankCapacity(decimal newCapacity);
        void ChangeFuelType(FuelsType newFuelType);
        void ChangeYearOfRelease(DateTime newYear);
        void ChangeCarReleaseDate(DateTime newDate);
        void ChangeCurrentMileage(uint newMileage);
        void ChangeNotes(string newNotes);
        void ChangeOwner(Owner newOwner);
    }
}