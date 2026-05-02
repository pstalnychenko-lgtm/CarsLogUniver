using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IUserRoleManager
    {
        void ActivateUser(User user); 
        void DeactivateUser(User user); 
        void AssignRole(User user, UserRole role); 
    }
}
