using System;

namespace CarsLogWorkig.Models
{
    public class FuelEntry
    {
        private readonly Guid _id = Guid.NewGuid(); 
        public Guid Id => _id;

        private string _gasStationName = string.Empty;
        public string GasStationName
        {
            get => _gasStationName;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Назва заправки не може бути порожньою."); 
                if (value.Trim().Length > 100)
                    throw new ArgumentException("Назва заправки не може перевищувати 100 символів."); 
                _gasStationName = value.Trim(); 
            }
        }

        private string _gasStationAddress = string.Empty;
        public string GasStationAddress
        {
            get => _gasStationAddress;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Адреса заправки не може бути порожньою."); 
                if (value.Trim().Length > 200)
                    throw new ArgumentException("Адреса заправки не може перевищувати 200 символів."); 
                _gasStationAddress = value.Trim(); 
            }
        }

        public void SetGasStation(string gasStationName, string gasStationAddress)
        {
            if (gasStationName is null) throw new ArgumentNullException(nameof(gasStationName)); 
            if (gasStationAddress is null) throw new ArgumentNullException(nameof(gasStationAddress)); 

            GasStationName = gasStationName;
            GasStationAddress = gasStationAddress;
        }

        public void ChangeGasStationName(string newName)
        {
            if (newName is null) throw new ArgumentNullException(nameof(newName)); 
            if (_gasStationName == newName.Trim())
                throw new ArgumentException("Ця назва заправки вже встановлена."); 
            GasStationName = newName;
        }

        public void ChangeGasStationAddress(string newAddress)
        {
            if (newAddress is null) throw new ArgumentNullException(nameof(newAddress)); 
            if (_gasStationAddress == newAddress.Trim())
                throw new ArgumentException("Ця адреса заправки вже встановлена."); 
            GasStationAddress = newAddress;
        }

        public FuelsType FuelType { get; private set; }

        public void SetFuelType(FuelsType fuelType)
        {
            FuelType = fuelType;
        }

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

        public FuelEntry(string gasStationName, string gasStationAddress,
                         FuelsType fuelType, decimal liters, decimal pricePerLiter)
        {
            if (gasStationName is null) throw new ArgumentNullException(nameof(gasStationName)); 
            if (gasStationAddress is null) throw new ArgumentNullException(nameof(gasStationAddress)); 

            GasStationName = gasStationName;
            GasStationAddress = gasStationAddress;
            FuelType = fuelType;
            Liters = liters;
            PricePerLiter = pricePerLiter;
            TotalCost = liters * pricePerLiter;
        }

        public string GetFormattedTotalCost() => $"{TotalCost:N2} грн";

        public override string ToString() =>
            $"[{FuelType}] {_gasStationName} | {_liters} л × {_pricePerLiter:N2} грн = {GetFormattedTotalCost()}";
    }

    public enum FuelsType { Petrol, Diesel, Electric, Hybrid }
}