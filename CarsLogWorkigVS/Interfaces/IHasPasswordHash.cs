using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasPasswordHash
    {
        string PasswordHash { get; }
        void ChangePassword(Guid requestingUserId, string newPassword);
    }
}
