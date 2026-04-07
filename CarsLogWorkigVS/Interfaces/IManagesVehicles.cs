using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IManagesVehicles
    {
        void AddVehicle(Vehicle vehicle);
        void RemoveVehicle(Vehicle vehicle);
        bool IsVehicleOwner(Vehicle vehicle);
    }
}
