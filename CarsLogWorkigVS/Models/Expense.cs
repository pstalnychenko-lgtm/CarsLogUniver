using System;

namespace CarsLogWorkig.Models
{
    public class Expense // Клас для запису витрат, пов'язаних з автомобілем
    {
        public Guid Id { get; init; } = Guid.NewGuid();// унікальний ідентифікатор запису про витрати

        public ExpenseCategory Category { get; private set; } // Категорія витрат

        public decimal Amount { get; private set; } // Сума витрат

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
        public Guid VehicleId { get; private set; }// Ідентифікатор автомобіля, до якого належить витрата

        public Expense(ExpenseCategory category, decimal amount, DateTime date, string description, Guid vehicleId)/*конструктор для створення
                                                                                                                     запису про витрати*/
        {
            Category = category;
            Amount = amount;
            Description = description;
            VehicleId = vehicleId;
        }
    }

    public enum ExpenseCategory// Категорії витрат
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
