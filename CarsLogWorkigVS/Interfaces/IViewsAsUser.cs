using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IViewsAsUser
    {
        UserViewSession StartViewAs(User targetUser);
        void EndViewAs(UserViewSession session);
        bool IsInViewAsMode { get; }
        UserViewSession CurrentViewSession { get; }
    }
}
