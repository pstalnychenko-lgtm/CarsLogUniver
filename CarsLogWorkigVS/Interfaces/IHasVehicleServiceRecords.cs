using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleServiceRecords
    {
        List<ServiceRecord> ServiceRecords { get; }
    }
}
