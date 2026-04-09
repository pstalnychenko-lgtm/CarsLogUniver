using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleFuelEntries
    {
        List<FuelEntry> FuelEntries { get; }
    }
}
