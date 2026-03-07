using System;
using System.Collections.Generic;
using System.Text;

namespace CarsLogWorkig.Models


{
    public class VehicleComponent
    {
        private string _partName;

        public Guid Id { get; private set; }
        public string PartName
        {
            get => _partName;
            set => _partName = string.IsNullOrWhiteSpace(value) ? throw new ArgumentException() : value;
        }
        public string Brand { get; set; }
        public int InstallationMileage { get; set; }

        

     }
}
