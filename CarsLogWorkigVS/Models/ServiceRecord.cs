using System;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор запису про сервісне обслуговування

        public DateTime DateOfService { get; private set; }// Дата проведення сервісного обслуговування

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

        private decimal _cost;
        public decimal Cost
        {
            get => _cost;
            set => _cost = value < 0 ? 0 : value;
        }

        public ServiceRecord(DateTime dateOfService, string description, uint mileageAtService,
                               decimal cost)
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
