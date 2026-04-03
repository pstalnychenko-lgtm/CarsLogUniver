using System;

namespace CarsLogWorkig.Models
{
    public interface ISuperAdmin : IUser
    {
        void CreateAdmin(User user);
        void RemoveAdmin(User user);
        void DeactivateAdmin(User user);

        void DeactivateUser(User user);
        void ActivateUser(User user);
        void AssignRole(User user, UserRole role);

        bool IsSuperAdmin();
    }
}
