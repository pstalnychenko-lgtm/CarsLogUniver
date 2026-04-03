using System;

namespace CarsLogWorkig.Models
{
    
    public interface IAdmin : IUser
    {
        void DeactivateUser(User user);
        void ActivateUser(User user);
        void AssignRole(User user, UserRole role);
        bool CanViewUserDetails(User user);
    }
}
