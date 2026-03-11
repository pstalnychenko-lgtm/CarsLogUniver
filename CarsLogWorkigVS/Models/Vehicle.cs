using System;

namespace CarsLogWorkig.Models
{
    public class Vehicle
    {
        
        private string _plateNumber { get; set; }

        private string _brand { get; set; }

        private string _model { get; set; }

        private DateTime _yearOfRelease;

        public uint CurrentMileage { get; set; }

        public List<FuelEntry> FuelEntries { get; set; }


    }

    
}