using System;

namespace CarsLogWorkig.Models
{
    public class Expense
    {
        private readonly Guid _id = Guid.NewGuid();
        public Guid Id => _id;

        public ExpenseCategory Category { get; private set; }

        private decimal _amount;
        public decimal Amount
        {
            get => _amount;
            private set
            {
                if (value < 0)
                    throw new ArgumentException("Сума витрат не може бути від'ємною.");
                _amount = value;
            }
        }

        private string _description = string.Empty;
        public string Description
        {
            get => _description;
            private set
            {
                if (value != null && value.Trim().Length > 1000)
                    throw new ArgumentException("Опис не може перевищувати 1000 символів.");
                _description = value?.Trim() ?? string.Empty;
            }
        }

        public DateTime ExpenseDate { get; private set; }

        public Guid VehicleId { get; private set; }

        public Expense(ExpenseCategory category, decimal amount, DateTime date, string description, Guid vehicleId)
        {
            Category = category;
            Amount = amount;
            ExpenseDate = date;
            Description = description;
            VehicleId = vehicleId;
        }

        public string GetFormattedAmount() => $"{_amount:N2} грн";

        public override string ToString() =>
            $"[{Category}] {GetFormattedAmount()} | {ExpenseDate:dd.MM.yyyy} | {_description}";
    }

    public enum ExpenseCategory
    {
        Fuel, Service, Insurance, Fine, Parking, Washing, TireChange, Other
    }
}
