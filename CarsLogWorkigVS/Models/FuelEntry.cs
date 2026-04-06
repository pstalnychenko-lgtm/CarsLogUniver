using System;

namespace CarsLogWorkig.Models
{
    public class FuelEntry
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        private string _gasStation = string.Empty;
        public string GasStation
        {
            get => _gasStation;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва заправки не може бути порожньою.");
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Назва заправки не може перевищувати 100 символів.");
                _gasStation = value.Trim();
            }
        }

        public FuelsType FuelType { get; private set; }
        public DateTime FuelDate { get; private set; }

        private decimal _pricePerLiter;
        public decimal PricePerLiter
        {
            get => _pricePerLiter;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Ціна за літр не може бути від'ємною.");
                _pricePerLiter = value;
            }
        }

        private decimal _liters;
        public decimal Liters
        {
            get => _liters;
            private set
            {
                if (value <= 0)
                    throw new ArgumentException("Кількість літрів має бути більше нуля.");
                _liters = value;
            }
        }

        public decimal TotalCost { get; private set; }
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

        public string GetFormattedTotalCost() => $"{TotalCost:N2} грн";

        public override string ToString() =>
            $"[{FuelType}] {_gasStation} | {_liters} л × {_pricePerLiter:N2} грн = {GetFormattedTotalCost()} | {FuelDate:dd.MM.yyyy}";
    }

    public enum FuelsType { Petrol, Diesel, Electric, Hybrid }
}
