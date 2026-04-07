using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IAddsTripLog
    {
        void AddTripLog(Vehicle vehicle, TripLog tripLog);
    }
}
