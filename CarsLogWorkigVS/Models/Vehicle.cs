using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Vehicle
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string PlateNumber { get; set; }

        public string Vin { get; set; }// Ідентифікаційний номер транспортного засобу

        public string Brand { get; set; }

        public string Model { get; set; }

        public string Color { get; set; }

        public string BodyType { get; set; }

        public uint EngineVolumeCc { get; set; } // Об'єм двигуна в кубічних сантиметрах

        public FuelsType FuelType { get; set; }// Тип палива

        public decimal FuelTankCapacity { get; set; }// Ємність паливного бака в літрах

        public DateTime YearOfRelease { get; set; }

        public DateTime CarReleaseDate { get; set; }

        public uint CurrentMileage { get; set; }

        public string Notes { get; set; }

        // Навігаційні властивості
        public Owner Owner { get; set; }

        public List<Driver> Drivers { get; set; } = new List<Driver>();

        public List<Document> Documents { get; set; } = new List<Document>();

        public List<FuelEntry> FuelEntries { get; set; } = new List<FuelEntry>();

        public List<ServiceRecord> ServiceRecords { get; set; } = new List<ServiceRecord>();

        public List<VehicleComponent> Components { get; set; } = new List<VehicleComponent>();

        public List<Note> Notess { get; set; } = new List<Note>();

        public List<TripLog> TripLogs { get; set; } = new List<TripLog>();

        public List<Expense> Expenses { get; set; } = new List<Expense>();
    }
}
