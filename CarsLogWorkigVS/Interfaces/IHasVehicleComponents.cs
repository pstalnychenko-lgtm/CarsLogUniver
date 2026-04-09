using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleComponents
    {
        List<VehicleComponent> Components { get; }
    }
}
