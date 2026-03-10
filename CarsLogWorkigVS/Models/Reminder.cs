using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponent
    {
    
        public Guid Id { get; private set; } 

        private string _partName { get; set; }

        public int InstallationMileage { get; set; }

        private bool _іsExpired;

       
    }
}