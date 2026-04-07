using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasSex
    {
        UserSex CurrentSex { get; }
        void ChangeSex(UserSex newSex);
    }
}
