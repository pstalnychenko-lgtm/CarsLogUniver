using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleDrivers
    {
        List<Driver> Drivers { get; }
    }
}
