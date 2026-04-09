using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleTripLogs
    {
        List<TripLog> TripLogs { get; }
    }
}
