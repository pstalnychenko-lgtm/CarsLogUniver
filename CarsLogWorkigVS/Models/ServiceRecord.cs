using System;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public DateTime DateOfService { get; private set; }

        private string _description;
        public string Description
        {
            get => _description;
            private set
            {
                if (string.IsNullOrEmpty(value))
                    return;
                else
                    _description = value;
            }
        }

        public uint MileageAtService { get; private set; }

        public uint NextServiceMileage { get; private set; }

        public DateTime NextServiceDate { get; private set; }

        private decimal _cost;
        public decimal Cost
        {
            get => _cost;
            set => _cost = value < 0 ? 0 : value;
        }

        public ServiceRecord(DateTime dateOfService, string description, uint mileageAtService,
                              uint nextServiceMileage, DateTime nextServiceDate, decimal cost)
        {
            DateOfService = dateOfService;
            Description = description;
            MileageAtService = mileageAtService;
            NextServiceMileage = nextServiceMileage;
            NextServiceDate = nextServiceDate;
            Cost = cost;
        }
    }
}
