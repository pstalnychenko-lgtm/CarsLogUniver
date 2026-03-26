using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Vehicle
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string _plateNumber;
        public string PlateNumber
        {
            get => _plateNumber;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _plateNumber = value;
            }
        }

        private string _vin;
        public string Vin
        {
            get => _vin;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _vin = value;
            }
        }

        private string _brand;
        public string Brand
        {
            get => _brand;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _brand = value;
            }
        }

        private string _model;
        public string Model
        {
            get => _model;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _model = value;
            }
        }

        private string _color;
        public string Color
        {
            get => _color;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _color = value;
            }
        }

        private string _bodyType;
        public string BodyType
        {
            get => _bodyType;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _bodyType = value;
            }
        }

        public uint EngineVolumeCc { get; private set; }

        public FuelsType FuelType { get; private set; }

        public decimal FuelTankCapacity { get; private set; }

        public DateTime YearOfRelease { get; private set; }

        public DateTime CarReleaseDate { get; private set; } = DateTime.Now;

        public uint CurrentMileage { get; set; }

        private string _notes;
        public string Notes
        {
            get => _notes;
            set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _notes = value;
            }
        }

        public Owner Owner { get; private set; }

        public List<Driver> Drivers { get; private set; } = new List<Driver>();

        public List<Document> Documents { get; private set; } = new List<Document>();

        public List<FuelEntry> FuelEntries { get; private set; } = new List<FuelEntry>();

        public List<ServiceRecord> ServiceRecords { get; private set; } = new List<ServiceRecord>();

        public List<VehicleComponent> Components { get; private set; } = new List<VehicleComponent>();

        public List<Note> Notess { get; private set; } = new List<Note>();

        public List<TripLog> TripLogs { get; private set; } = new List<TripLog>();

        public List<Expense> Expenses { get; private set; } = new List<Expense>();

        public Vehicle(string plateNumber, string vin, string brand, string model, string color,
                        string bodyType, uint engineVolumeCc, FuelsType fuelType, decimal fuelTankCapacity,
                        DateTime yearOfRelease, DateTime carReleaseDate, Owner owner)
        {
            PlateNumber = plateNumber;
            Vin = vin;
            Brand = brand;
            Model = model;
            Color = color;
            BodyType = bodyType;
            EngineVolumeCc = engineVolumeCc;
            FuelType = fuelType;
            FuelTankCapacity = fuelTankCapacity;
            YearOfRelease = yearOfRelease;
            CarReleaseDate = carReleaseDate;
            Owner = owner;
        }
    }
}
