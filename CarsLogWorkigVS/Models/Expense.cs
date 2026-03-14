using System;

namespace CarsLogWorkig.Models
{
    public class Expense // Клас для запису витрат, пов'язаних з автомобілем
    {
        public Guid Id { get; init; } = Guid.NewGuid();

        public ExpenseCategory Category { get; set; } // Категорія витрат

        public decimal Amount { get; set; } // Сума витрат

        public DateTime Date { get; set; }

        public string Description { get; set; }

        // Навігаційна властивість
        public Guid VehicleId { get; set; }
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
