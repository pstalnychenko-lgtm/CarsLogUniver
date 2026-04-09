using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleExpenses
    {
        List<Expense> Expenses { get; }
        decimal GetTotalExpenses();
    }
}
