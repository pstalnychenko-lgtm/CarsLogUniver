using System;

namespace CarsLogWorkig.Models
{
    public class Vehicle
    {
        
        private string _plateNumber { get; set; }

        private string _brand { get; set; }

        private string _model { get; set; }

        private DateTime _yearOfRelease;

        private uint _currentMileage { get; set; }

        private List<FuelEntry> _fuelEntries { get; set; }

        private DateTime _carReleaseDate;

    }

    
}