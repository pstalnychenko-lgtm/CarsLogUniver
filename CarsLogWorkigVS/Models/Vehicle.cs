using System;
using System.Collections.Generic;

namespace CarsLogWorkig.Models
{
    public class Vehicle
    {
        public Guid Id { get; init; } = Guid.NewGuid(); // унікальний ідентифікатор транспортного засобу

        private string _plateNumber;
        public string PlateNumber // Номерний знак транспортного засобу
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
        public string Vin // Ідентифікаційний номер транспортного засобу
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
        public string Brand // Марка транспортного засобу   
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
        public string Model// Модель транспортного засобу
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
        public string Color// Колір транспортного засобу
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
        public string BodyType// Тип кузова (наприклад, седан, хетчбек, позашляховик тощо)
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

        public uint EngineVolumeCc { get; private set; } // Об'єм двигуна в кубічних сантиметрах

        public FuelsType FuelType { get; private set; } // Тип палива

        public decimal FuelTankCapacity { get; private set; } // Ємність паливного бака в літрах

        public DateTime YearOfRelease { get; private set; } // Рік випуску автомобіля 

        public DateTime CarReleaseDate { get; private set; } = DateTime.Now; // Дата випуску автомобіля
        public uint CurrentMileage { get; set; } // залишено set — оновлюється під час експлуатації

        private string _notes;
        public string Notes// додаткова інформація або коментарі про транспортний засіб, які можуть бути корисними для власника або водіїв
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



// ---------------------------------------------------------------------------------------------------------------------------------------------------



        public Owner Owner { get; private set; }// Навігаційна властивість для власника транспортного засобу

        public List<Driver> Drivers { get; private set; } = new List<Driver>(); // Навігаційна властивість для водіїв,
                                                                                // які керують транспортним засобом

        public List<Document> Documents { get; private set; } = new List<Document>();/* Навігаційна властивість для документів, 
                                                                                        пов'язаних з транспортним засобом*/

        public List<FuelEntry> FuelEntries { get; private set; } = new List<FuelEntry>();// Навігаційна властивість для записів про
                                                                                         // заправки транспортного засобу

        public List<ServiceRecord> ServiceRecords { get; private set; } = new List<ServiceRecord>();// Навігаційна властивість
                                                                                                    // для записів про сервісне
                                                                                                    // обслуговування транспортного засобу

        public List<VehicleComponent> Components { get; private set; } = new List<VehicleComponent>();// Навігаційна властивість для
                                                                                                      // компонентів
                                                                                                      // транспортного засобу
                                                                                                      // (наприклад, шини, акумулятор,
                                                                                                      // гальмівні колодки тощо)

        public List<Note> Notess { get; private set; } = new List<Note>();

        public List<TripLog> TripLogs { get; private set; } = new List<TripLog>();

        public List<Expense> Expenses { get; private set; } = new List<Expense>();




        // ---------------------------------------------------------------------------------------------------------------------------------------------------  



        public Vehicle(string plateNumber, string vin, string brand, string model, string color,
                        string bodyType, uint engineVolumeCc, FuelsType fuelType, decimal fuelTankCapacity,
                        DateTime yearOfRelease, DateTime carReleaseDate, Owner owner) // конструктор для створення нового транспортного засобу
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
