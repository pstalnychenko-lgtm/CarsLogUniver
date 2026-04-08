using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasBloodType
    {
        BloodType BloodType { get; }
        string GetBloodType();
        void ChangeBloodType(BloodType newBloodType);
        List<BloodType> GetCompatibleDonorTypes();
    }
}
