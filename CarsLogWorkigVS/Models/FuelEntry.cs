using System;

namespace CarsLogWorkig.Models
{
    public class FuelEntry
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public string GasStation { get; set; }

        public FuelsType FuelType { get; set; }

        public DateTime FuelDate { get; set; }

        public decimal Liters { get; set; }

        public decimal PricePerLiter { get; set; } // ціна за літр

        public decimal TotalCost { get; set; }

        public bool IsFullTank { get; set; }

        /* Обчислювана властивість: витрата л/100км
         Розраховується відносно попереднього запису — заповнюється при збереженні*/
        public decimal? FuelConsumptionPer100Km { get; set; }
    }

    public enum FuelsType
    {
        Petrol,
        Diesel,
        Electric,
        Hybrid
    }
}
