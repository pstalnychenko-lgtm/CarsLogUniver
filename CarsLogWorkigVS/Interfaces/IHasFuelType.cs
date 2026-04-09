using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasFuelType
    {
        FuelsType FuelType { get; }
        void ChangeFuelType(FuelsType newFuelType);
    }
}
