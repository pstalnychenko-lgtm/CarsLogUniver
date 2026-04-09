using System;

namespace CarsLogWorkigVS.Interfaces
{
    public interface IChangePassword
    {
        void ChangePassword(Guid requestingUserId, string newPassword);
    }
}
