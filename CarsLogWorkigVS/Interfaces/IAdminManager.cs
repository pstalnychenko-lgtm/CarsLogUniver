using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IAdminManager
    {
        void CreateAdmin(User user); 
        void DeactivateAdmin(User user); 
        void RemoveAdmin(User user); 
        UserViewSession StartViewAs(User targetUser); 
        UserViewSession? CurrentViewSession { get; }
        void EndViewAs(UserViewSession session); 
    }
}
