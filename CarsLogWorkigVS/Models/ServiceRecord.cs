using System;

namespace CarsLogWorkig.Models
{
    public class ServiceRecord // Клас для зберігання інформації про сервісне обслуговування автомобіля
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор запису про сервісне обслуговування

        public DateTime DateOfService { get; private set; }// Дата проведення сервісного обслуговування

        private string _description;
        public string Description //    Опис проведеного сервісного обслуговування
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
        public decimal Cost //  Вартість сервісного обслуговування
        {
            get => _cost;
            set => _cost = value < 0 ? 0 : value;
        } // Вартість сервісного обслуговування не може бути від'ємною

        public ServiceRecord(DateTime dateOfService, string description, uint mileageAtService,
                               decimal cost)
        {
            DateOfService = dateOfService;
            Description = description;
            Cost = cost;
        }
    }
}
