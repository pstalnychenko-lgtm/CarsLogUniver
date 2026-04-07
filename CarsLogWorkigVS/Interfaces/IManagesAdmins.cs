using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IManagesAdmins
    {
        void CreateAdmin(User user);
        void RemoveAdmin(User user);
        void DeactivateAdmin(User user);
    }
}
