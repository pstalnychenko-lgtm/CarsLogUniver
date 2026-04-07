using CarsLogWorkig.Models;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasRole
    {
        UserRole Role { get; }
        void ChangeRole(UserRole role);
    }
}
