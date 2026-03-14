using System;

namespace CarsLogWorkig.Models
{
    public class VehicleComponent
    {
        private string _partName { get; set; }

        private uint _installationMileage { get; set; }

        private bool _іsExpired; // Чи потребує заміни (t or f)
        private DateTime _installationDate { get; set; }
    }
}