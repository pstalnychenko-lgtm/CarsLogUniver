using System;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord 
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        public DateTime DateOfService { get; private set; }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            private set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new ArgumentException("Опис сервісного обслуговування не може бути порожнім.");
                if (value.Trim().Length > 1000)
                    throw new ArgumentException("Опис не може перевищувати 1000 символів.");
                _description = value.Trim();
            }
        }

        private decimal _cost;
        public decimal Cost
        {
            get => _cost;
            set
            {
                if (value < 0)
                    throw new ArgumentException("Вартість обслуговування не може бути від'ємною.");
                _cost = value;
            }
        }

        public uint MileageAtService { get; private set; } 

        public ServiceRecord(DateTime dateOfService, string description, uint mileageAtService, decimal cost)
        {
            DateOfService = dateOfService;
            Description = description;
            MileageAtService = mileageAtService;
            Cost = cost;
        }

        public string GetFormattedCost() => $"{_cost:N2} грн";

        public override string ToString() =>
            $"[Сервіс] {DateOfService:dd.MM.yyyy} | {_description} | {GetFormattedCost()} | Пробіг: {MileageAtService} км";
    }
}
