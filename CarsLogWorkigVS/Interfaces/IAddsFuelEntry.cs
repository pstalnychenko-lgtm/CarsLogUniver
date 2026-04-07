using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IAddsFuelEntry
    {
        void AddFuelEntry(Vehicle vehicle, FuelEntry entry);
    }
}
