using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Vehicle
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string _plateNumber = string.Empty;
        public string PlateNumber
        {
            get => _plateNumber;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Номерний знак не може бути порожнім.");
                if (value.Trim().Length > 10)
                    throw new ArgumentException("Номерний знак не може перевищувати 10 символів.");
                _plateNumber = value.Trim();
            }
        }

        private string _vin = string.Empty;
        public string Vin
        {
            get => _vin;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("VIN не може бути порожнім.");
                _vin = value.Trim();
            }
        }

        private string _brand = string.Empty;
        public string Brand
        {
            get => _brand;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Марка автомобіля не може бути порожньою.");
                if (value.Trim().Length > 50)
                    throw new ArgumentException("Марка не може перевищувати 50 символів.");
                _brand = value.Trim();
            }
        }

        private string _model = string.Empty;
        public string Model
        {
            get => _model;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Модель автомобіля не може бути порожньою.");
                if (value.Trim().Length > 50)
                    throw new ArgumentException("Модель не може перевищувати 50 символів.");
                _model = value.Trim();
            }
        }

        private string _color = string.Empty;
        public string Color
        {
            get => _color;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Колір не може бути порожнім.");
                _color = value.Trim();
            }
        }

        private string _bodyType = string.Empty;
        public string BodyType
        {
            get => _bodyType;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Тип кузова не може бути порожнім.");
                _bodyType = value.Trim();
            }
        }

        private uint _engineVolumeCc;
        public uint EngineVolumeCc
        {
            get => _engineVolumeCc;
            private set
            {
                if (value == 0)
                    throw new ArgumentException("Об'єм двигуна не може бути нульовим.");
                _engineVolumeCc = value;
            }
        }

        private decimal _fuelTankCapacity;
        public decimal FuelTankCapacity
        {
            get => _fuelTankCapacity;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Об'єм баку не може бути від'ємним або нульовим.");
                _fuelTankCapacity = value;
            }
        }

        public FuelsType FuelType { get; private set; }
        public DateTime YearOfRelease { get; private set; }
        public DateTime CarReleaseDate { get; private set; }
        public uint CurrentMileage { get; set; }

        private string _notes = string.Empty;
        public string Notes
        {
            get => _notes;
            set
            {
                if (value != null && value.Trim().Length > 1000)
                    throw new ArgumentException("Нотатки не можуть перевищувати 1000 символів.");
                _notes = value?.Trim() ?? string.Empty;
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
            if (owner == null)
                throw new ArgumentNullException("Власник не може бути порожнім.");

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

        public uint GetTotalDistance()
        {
            uint total = 0;
            foreach (var trip in TripLogs)
                total += trip.DistanceKm;
            return total;
        }

        public decimal GetTotalExpenses()
        {
            decimal total = 0;
            foreach (var expense in Expenses)
                total += expense.Amount;
            return total;
        }

        public override string ToString() =>
            $"{_brand} {_model} | Номер: {_plateNumber} | Пробіг: {CurrentMileage} км | Власник: {Owner.FullName}";
    }
}
