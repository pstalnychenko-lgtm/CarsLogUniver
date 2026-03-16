using System;

namespace CarsLogWorkig.Models
{
    public class Expense // Клас для запису витрат, пов'язаних з автомобілем
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public ExpenseCategory Category { get; private set; } // Категорія витрат

        public decimal Amount { get; private set; } // Сума витрат

        public DateTime Date { get; private set; }

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

        // Навігаційна властивість
        public Guid VehicleId { get; private set; }

        public Expense(ExpenseCategory category, decimal amount, DateTime date, string description, Guid vehicleId)
        {
            Category = category;
            Amount = amount;
            Date = date;
            Description = description;
            VehicleId = vehicleId;
        }
    }

    public enum ExpenseCategory
    {
        Fuel,
        Service,
        Insurance,
        Fine,
        Parking,
        Washing,
        TireChange,
        Other
    }
}
