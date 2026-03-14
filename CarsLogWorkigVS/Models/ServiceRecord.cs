using System;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime DateOfService { get; set; }

        public string Description { get; set; }

        public uint MileageAtService { get; set; }

        public uint NextServiceMileage { get; set; }

        public DateTime NextServiceDate { get; set; }

        private decimal _cost;
        public decimal Cost
        {
            get => _cost;
            set => _cost = value < 0 ? 0 : value;
        }
    }
}
