using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IViewsAsUser
    {
        UserViewSession StartViewAs(User targetUser);        
        UserViewSession? CurrentViewSession { get; }
        void EndViewAs(UserViewSession session);

    }
}
