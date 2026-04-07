using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IAddsVehicleExpense
    {
        void AddExpenseToVehicle(Vehicle vehicle, Expense expense);
    }
}
