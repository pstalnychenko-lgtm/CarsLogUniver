using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IAsignedRoleWithAdminAndSAdminForUser
    {
        void ActivateUser(User user);
        void DeactivateUser(User user);
        void AssignRole(User user, UserRole role);
    }
}
