using System;
using System.Collections.Generic;
using CarsLogWorkigVS.Interfaces;


namespace CarsLogWorkig.Models
{
    public class FuelEntry :
        ISetFuelType,
        ICreateGasStation
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        private string _gasStationName = string.Empty;
        public string GasStationName
        {
            get => _gasStationName;
            set
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
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Адреса заправки не може бути порожньою.");
                if (value.Trim().Length > 200)
                    throw new ArgumentException("Адреса заправки не може перевищувати 200 символів.");
                _gasStationAddress = value.Trim();
            }
        }
        public void CreateGasStationAddress(string gasStationAddress)
        {
            GasStationAddress = gasStationAddress;
        }
        public void CreateGasStation(string gasStationName, string gasStationAddress)
        {
            GasStationName = gasStationName;
            GasStationAddress = gasStationAddress;
        }   
        
        
        void ICreateGasStation.CreateGasStationName()
        {
            throw new NotImplementedException("Use CreateGasStation(string gasStationName, string gasStationAddress) or set GasStationName property.");
        }

        void ICreateGasStation.CreateGasStationAddress()
        {
            throw new NotImplementedException();
        }

        void ICreateGasStation.UpdateGasStationName()
        {
            throw new NotImplementedException();
        }

        void ICreateGasStation.UpdateGasStationAddress()
        {
            throw new NotImplementedException();
        }

        public FuelsType FuelType { get; set; }

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

        public FuelEntry(string gasStationName, FuelsType fuelType,
                         decimal liters, decimal pricePerLiter)
        {
            GasStationName = gasStationName;
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
