using System;

namespace CarsLogWorkig.Models
{
    public class FuelEntry
    {
        public Guid Id { get; init; } = Guid.NewGuid();

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

        public FuelsType FuelType { get; private set; }

        public DateTime FuelDate { get; private set; } // дата заправки

        public decimal Liters { get; private set; }

        public decimal PricePerLiter { get; private set; } // ціна за літр

        public decimal TotalCost { get; private set; }
        // загальна вартість заправки (розраховується як Liters * PricePerLiter)

        /* Обчислювана властивість: витрата л/100км
           Розраховується відносно попереднього запису — заповнюється при збереженні */
        public decimal? FuelConsumptionPer100Km { get; private set; }

        public FuelEntry(string gasStation, FuelsType fuelType, DateTime fuelDate,
                         decimal liters, decimal pricePerLiter)
        {
            GasStation = gasStation;
            FuelType = fuelType;
            FuelDate = fuelDate;
            Liters = liters;
            PricePerLiter = pricePerLiter;
            TotalCost = liters * pricePerLiter;
        }
    }

    public enum FuelsType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid
    }
}
