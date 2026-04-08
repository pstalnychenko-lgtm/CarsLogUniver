using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IHasPassword
    {
        string Password { get; }
        void ChangePassword(Guid requestingUserId, string newPassword);
    }
}
