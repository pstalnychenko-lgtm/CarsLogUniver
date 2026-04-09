using CarsLogWorkig.Models;
using System.Collections.Generic;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IGetCompatibleDonorTypes
    {
        List<BloodType> GetCompatibleDonorTypes();
    }
}
