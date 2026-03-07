using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models
{
    public class Vehicle1
    {
        private string _plateNumber;
        
        private string _brand;
        
        private string _model;

        public Guid Id { get; private set; }
        
        public string PlateNumber
        {
            get => _plateNumber;
            set => _plateNumber = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        
        public string Brand
        {
            get => _brand;
            set => _brand = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        
        public string Model
        {
            get => _model;
            set => _model = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        
        public int Year { get; set; }
        
        public string FuelType { get; set; }
        public int CurrentMileage { get; set; }

        
    }
}
