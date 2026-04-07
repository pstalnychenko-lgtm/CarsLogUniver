using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasBloodType
    {
        BloodType BloodType { get; }
        string GetBloodType();
    }
}
