using System;

namespace CarsLogWorkig.Models
{
    public class Vehicle
    {
        
        private string _plateNumber { get; set; }

        private string _brand { get; set; }

        private string _model { get; set; }

        private DateTime _yearOfRelease;

        public FuelType FuelType { get; set; }

        public int CurrentMileage { get; set; }

        
        public string FullName;
    }

    public enum FuelType
    {
        Gasoline,
        Diesel,
        Electric,
        Hybrid,
        LPG
    }
}