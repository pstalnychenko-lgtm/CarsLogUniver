using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IManagesDriverAssignment
    {
        void AssignDriverToVehicle(Vehicle vehicle, Driver driver);
        void RemoveDriverFromVehicle(Vehicle vehicle, Driver driver);
    }
}
