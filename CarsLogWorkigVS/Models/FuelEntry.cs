using System;

namespace CarsLogWorkig.Models
{
    public class FuelEntry // Клас для запису інформації про заправку автомобіля
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор запису про заправку

        private string _gasStation;
        public string GasStation // назва заправки
        {
            get => _gasStation;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _gasStation = value;
            }
        }

        public FuelsType FuelType { get; private set; } // тип палива

        public DateTime FuelDate { get; private set; } // дата заправки

        public decimal PricePerLiter { get; private set; } // ціна за літр

        public decimal TotalCost { get; private set; }
        
        /* загальна вартість заправки (розраховується як Liters * PricePerLiter)
 Обчислювана властивість: витрата л/100км

           Розраховується відносно попереднього запису — заповнюється при збереженні */
        public decimal? FuelConsumptionPer100Km { get; private set; }// палива на 100 км

        public FuelEntry(string gasStation, FuelsType fuelType, DateTime fuelDate,
                         decimal liters, decimal pricePerLiter) // конструктор для створення запису про заправку
        {
            GasStation = gasStation;
            FuelType = fuelType;
            FuelDate = fuelDate;
            PricePerLiter = pricePerLiter;
            TotalCost = liters * pricePerLiter;
        }
    }

    public enum FuelsType // тип палива
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid
    }
}
