using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasVehicleOwner
    {
        Owner Owner { get; }
        void ChangeOwner(Owner newOwner);
    }
}
